// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
    /// <summary>
    /// Stores a Creature basic stats
    /// </summary>
    public class CreatureStat
    {
        public short StatId { get; set; }
        public short STR { get; set; }
        public short VIT { get; set; }
        public short DEX { get; set; }
        public short AGI { get; set; }
        public short INT { get; set; }
        public short MEN { get; set; }
        public short LUK { get; set; }

        /// <summary>
        /// Calculates stats
        /// </summary>
        /// <param name="level">object level</param>
        /// <param name="statId">base stats id</param>
        /// <param name="jobId">Job Id (for Players)</param>
        internal void LoadStat(short statId)
        {
            if (statId > 0)
            {
                this.StatId = StatId;

                this.STR = (short)Arcadia.StatResource[statId].STR;
                this.VIT = (short)Arcadia.StatResource[statId].VIT;
                this.DEX = (short)Arcadia.StatResource[statId].DEX;
                this.AGI = (short)Arcadia.StatResource[statId].AGI;
                this.INT = (short)Arcadia.StatResource[statId].INT;
                this.MEN = (short)Arcadia.StatResource[statId].MEN;
                this.LUK = (short)Arcadia.StatResource[statId].LUK;
            }
        }
    }

    /// <summary>
    /// Stores a Creature special stats
    /// </summary>
    public class CreatureAttribute
    {
        public short Critical { get; set; }
        public short CriticalPower { get; set; }
        public short PAttackRight { get; set; }
        public short PAttackLeft { get; set; }
        public short Defense { get; set; }
        public short BlockDefense { get; set; }
        public short MAttack { get; set; }
        public short MDefense { get; set; }
        public short AccuracyRight { get; set; }
        public short AccuracyLeft { get; set; }
        public short MagicAccuracy { get; set; }
        public short Evasion { get; set; }
        public short MagicEvasion { get; set; }
        public short BlockChance { get; set; }
        public short MoveSpeed { get; set; }
        public short AttackSpeed { get; set; }
        public short AttackRange { get; set; }
        public short MaxWeight { get; set; }
        public short CastingSpeed { get; set; }
        public short CoolTimeSpeed { get; set; }
        public short ItemChance { get; set; }
        public short HPRegenPercentage { get; set; }
        public short HPRegenPoint { get; set; }
        public short MPRegenPercentage { get; set; }
        public short MPRegenPoint { get; set; }

        /// <summary>
        /// Calculates attributes
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="jobId"></param>
        public void Calculate(CreatureStat stat, int level, bool isRanged = false, int jobId = 0)
        {
            // TODO : Calculate for left hand and apply equip/skill/buff bonus

            if (isRanged)
                this.PAttackLeft = (short)((6 / 5f) * stat.AGI + (11 / 5f) * stat.DEX + level);// + equip; 
            else
                this.PAttackLeft = (short)((14 / 5f) * stat.STR + level + 9); // + equip;

            this.AccuracyLeft = (short)((1 / 2f) * stat.DEX + level); // + equip;
            this.MAttack = (short)(2 * stat.INT + level); // + equip;
            this.Defense = (short)((5 / 3f) * stat.VIT + level); // +equip;
            this.Evasion = (short)((1 / 2f) * stat.AGI + level); // +equip;
            this.AttackSpeed = (short)(100 + (1 / 10f) * stat.AGI); //+equip

            this.MagicAccuracy = (short)((4 / 10f) * stat.MEN + (1 / 10f) * stat.DEX + level); // +equip
            this.MDefense = (short)(2 * stat.MEN + level); // + equip
            //this.MRes
            this.MoveSpeed = (short)(120); // + equip
            this.HPRegenPercentage = (short)(5); // + equip
            this.MPRegenPercentage = (short)(5); // + equip
            this.BlockChance = 0; // +equip
            this.Critical = (short)((1 / 5f) * stat.LUK + 3);
            this.CastingSpeed = (short)(100);
            this.HPRegenPoint = (short)(2 * level + 48);
            this.MPRegenPoint = (short)(4.1f * stat.MEN + 2 * level + 48);
            this.BlockDefense = (short)(0);
            this.CriticalPower = (short)(80);
            this.CoolTimeSpeed = (short)(100);
            this.MaxWeight = (short)(10 * stat.STR + 10 * level);
        }
    }
}
