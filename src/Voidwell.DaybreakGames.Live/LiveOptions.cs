﻿
using System.Collections.Generic;

namespace Voidwell.DaybreakGames.Live
{
    public class LiveOptions
    {
        public bool DisableCharacterUpdater { get; set; } = false;
        public bool DisableCensusMonitor { get; set; } = false;
        public IEnumerable<string> CensusWebsocketWorlds { get; set; }
        public IEnumerable<string> CensusWebsocketServices { get; set; }
        public IEnumerable<string> CensusWebsocketCharacters { get; set; }
        public IEnumerable<string> CensusWebsocketExperienceIds { get; set; } = new[]
        {
            "4",    // Heal Player
            "7",    // Revive
            "15",   // Control Point Defend
            "16",   // Control Point Attack
            "26",   // Vehicle Roadkill
            "270",  // Squad Spawn Beacon Kill
            "272",  // Convert Capture Point
            "337",  // Holiday Event NPC: Kill
            "338"   // Holiday Event NPC Gold: Kill
        };
    }
}
