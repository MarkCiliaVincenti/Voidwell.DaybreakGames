﻿using System.Collections.Generic;

namespace Voidwell.DaybreakGames.Domain.Models
{
    public class VehicleInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public IEnumerable<int> Factions { get; set; }
    }
}
