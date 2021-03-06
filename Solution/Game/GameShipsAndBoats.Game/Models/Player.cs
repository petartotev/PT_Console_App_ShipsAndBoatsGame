﻿using GameShipsAndBoats.Game.Core;
using GameShipsAndBoats.Game.Models.Common;
using GameShipsAndBoats.Game.Models.Contracts;
using System;
using System.Text;

namespace GameShipsAndBoats.Game.Models
{
    public class Player : IPlayer
    {
        private Battlefield playerBattlefield;
        private Battlefield opponentBattlefield;

        public Player()
        {
            this.playerBattlefield = BattlefieldGenerator.GenerateNewBattlefield();
            this.opponentBattlefield = new Battlefield();
        }

        public Battlefield PlayerBattlefield
        {
            get
            {
                return this.playerBattlefield;
            }
        }

        public Battlefield OpponentBattlefield
        {
            get
            {
                return this.opponentBattlefield;
            }
        }


        public void Attack(int row, int col, string vessel)
        {
            this.opponentBattlefield.SetSlot(row, col, vessel);
        }

        public void BotAttack(int row, int col, string vessel)
        {
            if (!BattlefieldValidator.CheckIfSlotIsInsideOfMatrix(row, col))
            {
                throw new ArgumentOutOfRangeException($"The coordinates of the battlefield are out of range!");
            }
            if (vessel == null)
            {
                throw new ArgumentNullException($"The coordinates of the battlefield are out of range!");
            }

            if (vessel == BattlefieldElements.slotBoat)
            {
                this.opponentBattlefield.SetBotOpponentSlotsAllAroundToDot(row, col);
            } // BOAT
            else if (BattlefieldElements.slotsVessels.Contains(vessel))
            {
                if (BattlefieldValidator.CheckIfSlotIsInTheMiddle(row, col))
                {
                    this.opponentBattlefield.SetBotOpponentSlotsDiagonalToDot(row, col);

                    if (vessel == BattlefieldElements.slotCarrier)
                    {
                        this.opponentBattlefield.SetBotOpponentSlotsIfAnotherCarrierAround(row, col);
                    }
                } // OTHER VESSEL IN THE MIDDLE
                else if (BattlefieldValidator.CheckIfSlotIsOnEdge(row, col))
                {
                    this.opponentBattlefield.SetBotOpponentSlotsVesselOnEdgeToDot(row, col, vessel);
                } // OTHER VESSEL ON EDGE
            } // OTHER VESSEL

            this.opponentBattlefield.SetSlot(row, col, vessel);
        }

        public bool CheckIfWinner()
        {
            int hitTargets = 0;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (BattlefieldElements.slotsVessels.Contains(this.opponentBattlefield.GetSlot(row, col)))
                    {
                        hitTargets++;
                    }
                }
            }

            // 20 SLOTS = TTTT + SSS + SSS + CC + CC + CC + B + B + B + B
            if (hitTargets == 20)
            {
                return true;
            }
            return false;
        }

        public string GetAttacked(int row, int col)
        {
            string slotResult = playerBattlefield.GetSlot(row, col);

            if (BattlefieldElements.slotsVessels.Contains(slotResult))
            {
                playerBattlefield.SetSlot(row, col, BattlefieldElements.slotHit);
                return slotResult;
            }
            else
            {
                playerBattlefield.SetSlot(row, col, BattlefieldElements.slotOpponentPlus);
                return BattlefieldElements.slotOpponentMinus;
            }
        }

        public string GetAttackMessage(string vessel)
        {
            StringBuilder attackMessage = new StringBuilder();
            switch (vessel)
            {
                case BattlefieldElements.slotTanker:
                    attackMessage.AppendLine($" hit a Tanker (TTTT)!");
                    break;
                case BattlefieldElements.slotSubmarine:
                    attackMessage.AppendLine($" hit a Submarine (SSS)!");
                    break;
                case BattlefieldElements.slotCarrier:
                    attackMessage.AppendLine($" hit a Carrier (CC)!");
                    break;
                case BattlefieldElements.slotBoat:
                    attackMessage.AppendLine($" hit a Boat (B)!");
                    break;
                case BattlefieldElements.slotHit:
                    attackMessage.AppendLine($" already hit that!");
                    break;
                default:
                    attackMessage.AppendLine($" didn't hit anything.");
                    break;
            }
            return attackMessage.ToString().TrimEnd();
        }

        public void MarkShipOnEdgeAsDestroyed(int row, int col, string vessel)
        {
            int lengthVessel = BattlefieldElements.GetVesselLength(vessel);

            if (BattlefieldElements.slotsVessels.Contains(vessel))
            {
                // TOP EDGE
                if (row == 0 && (col > 0 && col < 9))
                {
                    for (int i = 0; i < lengthVessel; i++)
                    {
                        this.playerBattlefield.SetSlot(i, col, BattlefieldElements.slotHit);
                    }
                }
                // BOTTOM EDGE
                else if (row == 9 && (col > 0 && col < 9))
                {
                    for (int i = 9; i > 9 - lengthVessel; i--)
                    {
                        this.playerBattlefield.SetSlot(i, col, BattlefieldElements.slotHit);
                    }
                }
                // LEFT EDGE
                else if (col == 0 && (row > 0 && row < 9))
                {
                    for (int i = 0; i < lengthVessel; i++)
                    {
                        this.playerBattlefield.SetSlot(row, i, BattlefieldElements.slotHit);
                    }
                }
                // RIGHT EDGE
                else if (col == 9 && (row > 0 && row < 9))
                {
                    for (int i = 9; i > 9 - lengthVessel; i--)
                    {
                        this.playerBattlefield.SetSlot(row, i, BattlefieldElements.slotHit);
                    }
                }
            }
        }
    }
}