﻿using System.Collections.Generic;

namespace GameShipsAndBoats.Game.Models.Common
{
    public static class BattlefieldElements
    {
        public const string slotOccuppied = ". ";
        public const string slotEmpty = "  ";
        public const string slotHidden = "/ ";

        public const string slotOpponentPlus = "+ ";
        public const string slotOpponentMinus = "- ";

        public const string slotBoat = "B ";
        public const string slotCarrier = "C ";
        public const string slotSubmarine = "S ";
        public const string slotTanker = "T ";

        public const string slotFull = "B ";

        public const string slotHit = "X ";

        public static readonly List<string> slotsVessels = new List<string> { slotBoat, slotCarrier, slotSubmarine, slotTanker, slotFull, slotHit };

        public static int GetVesselLength(string vessel)
        {
            int lengthVessel;

            switch (vessel)
            {
                case "T ":
                    lengthVessel = 4;
                    break;
                case "S ":
                    lengthVessel = 3;
                    break;
                case "C ":
                    lengthVessel = 2;
                    break;
                case "X ":
                case "B ":
                    lengthVessel = 1;
                    break;
                default:
                    lengthVessel = 0;
                    break;
            }

            return lengthVessel;
        }
    }
}
