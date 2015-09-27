using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Common;
using Game.Database.Structures;

namespace Game.Content
{
    public static class Arcadia
    {
        internal static int conType = 0;
        internal static string conString = string.Empty;
        public static List<DB_AuctionCategory> AuctionCategoryResource;
        public static List<DB_ItemEffect> ItemEffectResource;
        public static List<DB_String> StringResource;
        public static List<DB_MonsterSkill> MonsterSkillResource;

        internal static void LoadAuctionCategory()
        {
            AuctionCategoryResource = new List<DB_AuctionCategory>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM AuctionCategoryResource ORDER BY category_id, sub_category_id"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                AuctionCategoryResource.Add(
                                    new DB_AuctionCategory
                                    {
                                        category_id = (int)dbReader[0],
                                        sub_category_id = (int)dbReader[1],
                                        name_id = (int)dbReader[2],
                                        local_flag = (int)dbReader[3],
                                        item_group = (int)dbReader[4],
                                        item_class = (int)dbReader[5]
                                    });
                            }
                        }
                    }
                    catch (Exception ex) 
                    {
                        ConsoleUtils.ShowSQL(ex.Message);
                        return;
                    }
                    finally { dbCmd.Connection.Close(); }
                }
            }

            ConsoleUtils.ShowStatus("{0} entries loaded from AuctionCategoryResource", AuctionCategoryResource.Count);
        }

        internal static void LoadItemEffect()
        {
            ItemEffectResource = new List<DB_ItemEffect>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM ItemEffectResource ORDER BY id, ordinal_id"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                ItemEffectResource.Add(new DB_ItemEffect
                                {
                                     id = (int)dbReader[off++],
                                     ordinal_id = (int)dbReader[off++],
                                     tooltip_id = (int)dbReader[off++],
                                     effect_type = (byte)dbReader[off++],
                                     effect_id = (short)dbReader[off++],
                                     effect_level = (short)dbReader[off++],
                                     value_0 = (decimal)dbReader[off++],
                                     value_1 = (decimal)dbReader[off++],
                                     value_2 = (decimal)dbReader[off++],
                                     value_3 = (decimal)dbReader[off++],
                                     value_4 = (decimal)dbReader[off++],
                                     value_5 = (decimal)dbReader[off++],
                                     value_6 = (decimal)dbReader[off++],
                                     value_7 = (decimal)dbReader[off++],
                                     value_8 = (decimal)dbReader[off++],
                                     value_9 = (decimal)dbReader[off++],
                                     value_10 = (decimal)dbReader[off++],
                                     value_11 = (decimal)dbReader[off++],
                                     value_12 = (decimal)dbReader[off++],
                                     value_13 = (decimal)dbReader[off++],
                                     value_14 = (decimal)dbReader[off++],
                                     value_15 = (decimal)dbReader[off++],
                                     value_16 = (decimal)dbReader[off++],
                                     value_17 = (decimal)dbReader[off++],
                                     value_18 = (decimal)dbReader[off++],
                                     value_19 = (decimal)dbReader[off++],
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ConsoleUtils.ShowSQL(ex.Message);
                        return;
                    }
                    finally { dbCmd.Connection.Close(); }
                }
            }

            ConsoleUtils.ShowStatus("{0} entries loaded from ItemEffectResource", ItemEffectResource.Count);
        }

        internal static void LoadString()
        {
            StringResource = new List<DB_String>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM StringResource"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                StringResource.Add(
                                    new DB_String
                                    {
                                        name = (string)dbReader[0],
                                        group_id = (int)dbReader[1],
                                        code = (int)dbReader[2],
                                        value = (string)dbReader[3]
                                    });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ConsoleUtils.ShowSQL(ex.Message);
                        return;
                    }
                    finally { dbCmd.Connection.Close(); }
                }
            }

            ConsoleUtils.ShowStatus("{0} entries loaded from StringResource", StringResource.Count);
        }

        internal static void LoadMonsterSkill()
        {
            MonsterSkillResource = new List<DB_MonsterSkill>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM MonsterSkillResource ORDER BY id, sub_id"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                MonsterSkillResource.Add(new DB_MonsterSkill
                                {
                                     id = (int)dbReader[off++],
                                     sub_id = (int)dbReader[off++],
                                     trigger_1_type = (int)dbReader[off++],
                                     trigger_1_value_1 = (decimal)dbReader[off++],
                                     trigger_1_value_2 = (decimal)dbReader[off++],
                                     trigger_1_function = (string)dbReader[off++],
                                     trigger_2_type = (int)dbReader[off++],
                                     trigger_2_value_1 = (decimal)dbReader[off++],
                                     trigger_2_value_2 = (decimal)dbReader[off++],
                                     trigger_2_function = (string)dbReader[off++],
                                     trigger_3_type = (int)dbReader[off++],
                                     trigger_3_value_1 = (decimal)dbReader[off++],
                                     trigger_3_value_2 = (decimal)dbReader[off++],
                                     trigger_3_function = (string)dbReader[off++],
                                     trigger_4_type = (int)dbReader[off++],
                                     trigger_4_value_1 = (decimal)dbReader[off++],
                                     trigger_4_value_2 = (decimal)dbReader[off++],
                                     trigger_4_function = (string)dbReader[off++],
                                     trigger_5_type = (int)dbReader[off++],
                                     trigger_5_value_1 = (decimal)dbReader[off++],
                                     trigger_5_value_2 = (decimal)dbReader[off++],
                                     trigger_5_function = (string)dbReader[off++],
                                     trigger_6_type = (int)dbReader[off++],
                                     trigger_6_value_1 = (decimal)dbReader[off++],
                                     trigger_6_value_2 = (decimal)dbReader[off++],
                                     trigger_6_function = (string)dbReader[off++],
                                     skill1_id = (int)dbReader[off++],
                                     skill1_lv = (int)dbReader[off++],
                                     skill1_probability = (decimal)dbReader[off++],
                                     skill2_id = (int)dbReader[off++],
                                     skill2_lv = (int)dbReader[off++],
                                     skill2_probability = (decimal)dbReader[off++],
                                     skill3_id = (int)dbReader[off++],
                                     skill3_lv = (int)dbReader[off++],
                                     skill3_probability = (decimal)dbReader[off++],
                                     skill4_id = (int)dbReader[off++],
                                     skill4_lv = (int)dbReader[off++],
                                     skill4_probability = (decimal)dbReader[off++],
                                     skill5_id = (int)dbReader[off++],
                                     skill5_lv = (int)dbReader[off++],
                                     skill5_probability = (decimal)dbReader[off++],
                                     skill6_id = (int)dbReader[off++],
                                     skill6_lv = (int)dbReader[off++],
                                     skill6_probability = (decimal)dbReader[off++],
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ConsoleUtils.ShowSQL(ex.Message);
                        return;
                    }
                    finally { dbCmd.Connection.Close(); }
                }
            }

            ConsoleUtils.ShowStatus("{0} entries loaded from MonsterSkillResource", MonsterSkillResource.Count);
        }

        public static void Initialize(int _conType, string _conString)
        {
            conType = _conType;
            conString = _conString;

            LoadAuctionCategory();
            LoadItemEffect();
            LoadString();
            LoadMonsterSkill();
        }
    }
}
