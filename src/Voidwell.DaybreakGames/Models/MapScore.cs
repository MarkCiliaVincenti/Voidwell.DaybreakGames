﻿namespace Voidwell.DaybreakGames.Models
{
    public class MapScore
    {
        public OwnershipScoreFactions Territories { get; set; }
        public OwnershipScoreFactions ConnectedTerritories { get; set; }
        public OwnershipScoreFactions AmpStations { get; set; }
        public OwnershipScoreFactions TechPlants { get; set; }
        public OwnershipScoreFactions BioLabs { get; set; }
    }

    public class OwnershipScoreBreakdown
    {
        public OwnershipScoreBreakdown(int value, float percent)
        {
            Value = value;
            Percent = percent;
        }

        public int Value { get; private set; }
        public float Percent { get; private set; }
    }

    public class OwnershipScoreFactions
    {
        public OwnershipScoreFactions(int vsScore, int ncScore, int trScore, int neuturalScore)
        {
            float scoreSum = vsScore + ncScore + trScore + neuturalScore;
            Vs = new OwnershipScoreBreakdown(vsScore, vsScore / scoreSum);
            Nc = new OwnershipScoreBreakdown(ncScore, ncScore / scoreSum);
            Tr = new OwnershipScoreBreakdown(trScore, trScore / scoreSum);
            Neutural = new OwnershipScoreBreakdown(neuturalScore, neuturalScore / scoreSum);
        }

        public OwnershipScoreBreakdown Vs { get; private set; }
        public OwnershipScoreBreakdown Nc { get; private set; }
        public OwnershipScoreBreakdown Tr { get; private set; }
        public OwnershipScoreBreakdown Neutural { get; private set; }
    }
}