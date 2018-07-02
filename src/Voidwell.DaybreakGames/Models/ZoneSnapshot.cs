﻿using System;
using System.Collections.Generic;

namespace Voidwell.DaybreakGames.Models
{
    public class ZoneSnapshot
    {
        public DateTime Timestamp { get; set; }
        public int WorldId { get; set; }
        public int ZoneId { get; set; }
        public int? MetagameInstanceId { get; set; }
        public IEnumerable<MapOwnership> Ownership { get; set; }
    }
}
