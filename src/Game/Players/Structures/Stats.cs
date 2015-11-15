// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Content;
using Common;
using Game.Database.Structures;

namespace Game.Players.Structures
{
    public class PlayerStat : CreatureStat
    {
        public void Load(Player player)
        {
            if (player.PrevJobs[0].Id != 0)
            {
                AddStats(player.PrevJobs[0].Id, player.PrevJobs[0].Level, 1);
            }
            if (player.PrevJobs[1].Id != 0)
            {
                if (player.PrevJobs[1].Level > 40)
                {// Overbreed
                    AddStats(player.PrevJobs[1].Id, 40, 1);
                    AddStats(player.PrevJobs[1].Id, player.PrevJobs[1].Level, 2);
                }
                else
                {
                    AddStats(player.PrevJobs[1].Id, player.PrevJobs[1].Level, 1);
                }
            }

            if (player.Level > 40)
            {
                AddStats(player.Job, 40, 1);
                AddStats(player.Job, player.JobLevel, 2);
            }
            else
            {
                AddStats(player.JobLevel, player.JobLevel, 1);
            }

            player.Attributes.Calculate(player.Stats, player.Level);
        }

        private void AddStats(int jobId, int level, byte group)
        {
            switch (group)
            {
                case 1:
                    this.STR += (short)(Arcadia.StatResource[jobId].STR + level * Arcadia.JobLevelBonus[jobId].str_1);
                    this.VIT += (short)(Arcadia.StatResource[jobId].VIT + level * Arcadia.JobLevelBonus[jobId].vit_1);
                    this.DEX += (short)(Arcadia.StatResource[jobId].DEX + level * Arcadia.JobLevelBonus[jobId].dex_1);
                    this.AGI += (short)(Arcadia.StatResource[jobId].AGI + level * Arcadia.JobLevelBonus[jobId].agi_1);
                    this.INT += (short)(Arcadia.StatResource[jobId].INT + level * Arcadia.JobLevelBonus[jobId].int_1);
                    this.MEN += (short)(Arcadia.StatResource[jobId].MEN + level * Arcadia.JobLevelBonus[jobId].men_1);
                    this.LUK += (short)(Arcadia.StatResource[jobId].LUK + level * Arcadia.JobLevelBonus[jobId].luk_1);
                    break;

                case 2:
                    this.STR += (short)(level * Arcadia.JobLevelBonus[jobId].str_2);
                    this.VIT += (short)(level * Arcadia.JobLevelBonus[jobId].vit_2);
                    this.DEX += (short)(level * Arcadia.JobLevelBonus[jobId].dex_2);
                    this.AGI += (short)(level * Arcadia.JobLevelBonus[jobId].agi_2);
                    this.INT += (short)(level * Arcadia.JobLevelBonus[jobId].int_2);
                    this.MEN += (short)(level * Arcadia.JobLevelBonus[jobId].men_2);
                    this.LUK += (short)(level * Arcadia.JobLevelBonus[jobId].luk_2);
                    break;

                case 3:
                    this.STR += (short)(level * Arcadia.JobLevelBonus[jobId].str_3);
                    this.VIT += (short)(level * Arcadia.JobLevelBonus[jobId].vit_3);
                    this.DEX += (short)(level * Arcadia.JobLevelBonus[jobId].dex_3);
                    this.AGI += (short)(level * Arcadia.JobLevelBonus[jobId].agi_3);
                    this.INT += (short)(level * Arcadia.JobLevelBonus[jobId].int_3);
                    this.MEN += (short)(level * Arcadia.JobLevelBonus[jobId].men_3);
                    this.LUK += (short)(level * Arcadia.JobLevelBonus[jobId].luk_3);
                    break;
            }
        }
    }

    public class PlayerAttribute : CreatureAttribute
    {
        internal void ChangeAttribute(int attribute, short value1 = 0, short value2 = 0)
        {
            switch (attribute)
            {
                case 11: this.PAttackRight += value1; break; // TODO : Left Hand
                case 12: this.MAttack += value1; break;
                case 13: this.AccuracyRight += value1; break; // TODO : Left Hand
                case 14: this.AttackSpeed += value1; break;
                case 15: this.Defense += value1; break;
                case 16:  this.MDefense += value1; break;
                case 17: this.Evasion += value1; break;
                case 18: this.MoveSpeed += value1; break;
                case 19: this.BlockChance += value1; break;
                case 20: this.MaxWeight += value1; break;
                case 21: this.BlockDefense += value1; break;
                case 22: this.CastingSpeed += value1; break;  // This might be wrong
                case 23: this.MagicAccuracy += value1; break;
                case 24: this.MDefense += value1; break;
                case 25: this.CoolTimeSpeed += value1; break;
                case 33: this.MPRegenPoint += value1; break; // This might be wrong
                case 34: this.AttackRange += value1; break; // This might be wrong
            }
        }

        internal void Add(Item item)
        {
            DB_Item dbItem = Arcadia.ItemResource.Find(obj => obj.id == item.Code);

            this.ChangeAttribute(dbItem.base_type_0, (short)(dbItem.base_var1_0 + item.Level * dbItem.base_var2_0));
            this.ChangeAttribute(dbItem.base_type_1, (short)(dbItem.base_var1_1 + item.Level * dbItem.base_var2_1));
            this.ChangeAttribute(dbItem.base_type_2, (short)(dbItem.base_var1_2 + item.Level * dbItem.base_var2_2));

            this.ChangeAttribute(dbItem.opt_type_0, (short)dbItem.opt_var1_0, (short)dbItem.opt_var2_0);
            this.ChangeAttribute(dbItem.opt_type_1, (short)dbItem.opt_var1_1, (short)dbItem.opt_var2_1);
            this.ChangeAttribute(dbItem.opt_type_2, (short)dbItem.opt_var1_2, (short)dbItem.opt_var2_2);
            this.ChangeAttribute(dbItem.opt_type_3, (short)dbItem.opt_var1_3, (short)dbItem.opt_var2_3);
        }

        internal void Remove(Item item)
        {
            DB_Item dbItem = Arcadia.ItemResource.Find(obj => obj.id == item.Code);

            this.ChangeAttribute(dbItem.base_type_0, (short)(-1*(dbItem.base_var1_0 + item.Level * dbItem.base_var2_0)));
            this.ChangeAttribute(dbItem.base_type_1, (short)(-1*(dbItem.base_var1_1 + item.Level * dbItem.base_var2_1)));
            this.ChangeAttribute(dbItem.base_type_2, (short)(-1*(dbItem.base_var1_2 + item.Level * dbItem.base_var2_2)));

            this.ChangeAttribute(dbItem.opt_type_0, (short)-dbItem.opt_var1_0, (short)-dbItem.opt_var2_0);
            this.ChangeAttribute(dbItem.opt_type_1, (short)-dbItem.opt_var1_1, (short)-dbItem.opt_var2_1);
            this.ChangeAttribute(dbItem.opt_type_2, (short)-dbItem.opt_var1_2, (short)-dbItem.opt_var2_2);
            this.ChangeAttribute(dbItem.opt_type_3, (short)-dbItem.opt_var1_3, (short)-dbItem.opt_var2_3);
        }
    }
}
