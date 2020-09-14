﻿using System.Threading.Tasks;
using Voidwell.DaybreakGames.CensusStream;
using Voidwell.DaybreakGames.CensusStream.Models;
using Voidwell.DaybreakGames.Data.Repositories;
using Voidwell.DaybreakGames.Services.Planetside;

namespace Voidwell.DaybreakGames.App.CensusStream.EventProcessors
{
    [CensusEventProcessor("Death")]
    public class DeathProcessor: IEventProcessor<Death>
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICharacterService _characterService;
        private readonly ICharacterRatingService _characterRatingService;
        private readonly IPlayerMonitor _playerMonitor;

        public DeathProcessor(IEventRepository eventRepository, ICharacterService characterService,
            ICharacterRatingService characterRatingService, IPlayerMonitor playerMonitor)
        {
            _eventRepository = eventRepository;
            _characterService = characterService;
            _characterRatingService = characterRatingService;
            _playerMonitor = playerMonitor;
        }

        public async Task Process(Death payload)
        {
            var attackerOutfitIdTask = GetCharacterOutfitIdAsync(payload.AttackerCharacterId);
            var victimOutfitIdTask = GetCharacterOutfitIdAsync(payload.CharacterId);

            await Task.WhenAll(
                SetLastSeenAsync(payload),
                CalculateCharacterRatingsAsync(payload),
                attackerOutfitIdTask,
                victimOutfitIdTask
            );

            var dataModel = ToDataModel(payload, attackerOutfitIdTask?.Result, victimOutfitIdTask?.Result);

            await _eventRepository.AddAsync(dataModel);
        }

        private Task SetLastSeenAsync(Death payload)
        {
            return Task.WhenAll(
                _playerMonitor.SetLastSeenAsync(payload.AttackerCharacterId, payload.ZoneId.Value, payload.Timestamp),
                _playerMonitor.SetLastSeenAsync(payload.CharacterId, payload.ZoneId.Value, payload.Timestamp));
        }

        private bool IsValidCharacterId(string characterId)
        {
            return !string.IsNullOrWhiteSpace(characterId) && characterId.Length > 18;
        }

        private async Task<string> GetCharacterOutfitIdAsync(string characterId)
        {
            if (!IsValidCharacterId(characterId))
            {
                return null;
            }

            var outfit = await _characterService.GetCharactersOutfit(characterId);
            return outfit?.OutfitId;
        }

        private Task CalculateCharacterRatingsAsync(Death payload)
        {
            if (!IsValidCharacterId(payload.AttackerCharacterId) ||
                !IsValidCharacterId(payload.CharacterId) ||
                payload.AttackerCharacterId == payload.CharacterId)
            {
                return Task.CompletedTask;
            }

            return _characterRatingService.CalculateRatingAsync(payload.AttackerCharacterId, payload.CharacterId);
        }

        private Data.Models.Planetside.Events.Death ToDataModel(Death payload, string attackerOutfitId, string victimOutfitId)
        {
            return new Data.Models.Planetside.Events.Death
            {
                AttackerCharacterId = payload.AttackerCharacterId,
                AttackerFireModeId = payload.AttackerFireModeId,
                AttackerLoadoutId = payload.AttackerLoadoutId,
                AttackerVehicleId = payload.AttackerVehicleId,
                AttackerWeaponId = payload.AttackerWeaponId,
                AttackerOutfitId = attackerOutfitId,
                CharacterId = payload.CharacterId,
                CharacterLoadoutId = payload.CharacterLoadoutId,
                CharacterOutfitId = victimOutfitId,
                IsHeadshot = payload.IsHeadshot,
                Timestamp = payload.Timestamp,
                WorldId = payload.WorldId,
                ZoneId = payload.ZoneId.Value
            };
        }
    }
}
