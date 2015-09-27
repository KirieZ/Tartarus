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
        public static List<DB_CreateLevelBonus> CreatureLevelBonus;
        public static List<DB_EventArea> EventAreaResource;
        public static List<DB_DropGroup> DropGroupResource;
        public static List<DB_ItemEffect> ItemEffectResource;
        public static List<DB_Monster> MonsterResource;
        public static List<DB_MonsterDropTable> MonsterDropTableResource;
        public static List<DB_MonsterSkill> MonsterSkillResource;
        public static List<DB_SetItemEffect> SetItemEffectResource;
        public static List<DB_Summon> SummonResource;
        public static List<DB_String> StringResource;

        #region Load Methods

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

            ConsoleUtils.ShowNotice("{0} entries loaded from AuctionCategoryResource", AuctionCategoryResource.Count);
        }

        internal static void LoadCreatureBonus()
        {
            CreatureLevelBonus = new List<DB_CreateLevelBonus>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM CreatureLevelBonus"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                CreatureLevelBonus.Add(
                                    new DB_CreateLevelBonus
                                    {
                                        id = (int)dbReader[0],
                                        STR = (decimal)dbReader[1],
                                        VIT = (decimal)dbReader[2],
                                        DEX = (decimal)dbReader[3],
                                        AGI = (decimal)dbReader[4],
                                        INT = (decimal)dbReader[5],
                                        MEN = (decimal)dbReader[6],
                                        LUK = (decimal)dbReader[7]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from CreatureLevelBonus", CreatureLevelBonus.Count);
        }

        internal static void LoadDropGroup()
        {
            DropGroupResource = new List<DB_DropGroup>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM DropGroupResource"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                DropGroupResource.Add(
                                    new DB_DropGroup
                                    {
                                        id = (int)dbReader[off++],
                                        drop_item_id_00 = (int)dbReader[off++],
                                        drop_min_count_00 = (long)dbReader[off++],
                                        drop_max_count_00 = (long)dbReader[off++],
                                        drop_percentage_00 = (decimal)dbReader[off++],
                                        drop_item_id_01 = (int)dbReader[off++],
                                        drop_min_count_01 = (long)dbReader[off++],
                                        drop_max_count_01 = (long)dbReader[off++],
                                        drop_percentage_01 = (decimal)dbReader[off++],
                                        drop_item_id_02 = (int)dbReader[off++],
                                        drop_min_count_02 = (long)dbReader[off++],
                                        drop_max_count_02 = (long)dbReader[off++],
                                        drop_percentage_02 = (decimal)dbReader[off++],
                                        drop_item_id_03 = (int)dbReader[off++],
                                        drop_min_count_03 = (long)dbReader[off++],
                                        drop_max_count_03 = (long)dbReader[off++],
                                        drop_percentage_03 = (decimal)dbReader[off++],
                                        drop_item_id_04 = (int)dbReader[off++],
                                        drop_min_count_04 = (long)dbReader[off++],
                                        drop_max_count_04 = (long)dbReader[off++],
                                        drop_percentage_04 = (decimal)dbReader[off++],
                                        drop_item_id_05 = (int)dbReader[off++],
                                        drop_min_count_05 = (long)dbReader[off++],
                                        drop_max_count_05 = (long)dbReader[off++],
                                        drop_percentage_05 = (decimal)dbReader[off++],
                                        drop_item_id_06 = (int)dbReader[off++],
                                        drop_min_count_06 = (long)dbReader[off++],
                                        drop_max_count_06 = (long)dbReader[off++],
                                        drop_percentage_06 = (decimal)dbReader[off++],
                                        drop_item_id_07 = (int)dbReader[off++],
                                        drop_min_count_07 = (long)dbReader[off++],
                                        drop_max_count_07 = (long)dbReader[off++],
                                        drop_percentage_07 = (decimal)dbReader[off++],
                                        drop_item_id_08 = (int)dbReader[off++],
                                        drop_min_count_08 = (long)dbReader[off++],
                                        drop_max_count_08 = (long)dbReader[off++],
                                        drop_percentage_08 = (decimal)dbReader[off++],
                                        drop_item_id_09 = (int)dbReader[off++],
                                        drop_min_count_09 = (long)dbReader[off++],
                                        drop_max_count_09 = (long)dbReader[off++],
                                        drop_percentage_09 = (decimal)dbReader[off++],
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

            ConsoleUtils.ShowNotice("{0} entries loaded from DropGroupResource", DropGroupResource.Count);
        }

        internal static void LoadEventArea()
        {
            EventAreaResource = new List<DB_EventArea>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM EventAreaResource"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                EventAreaResource.Add(
                                    new DB_EventArea
                                    {
                                         id = (int)dbReader[off++],
                                         begin_time = (int)dbReader[off++],
                                         end_time = (int)dbReader[off++],
                                         min_level = (int)dbReader[off++],
                                         max_level = (int)dbReader[off++],
                                         race_job_limit = (long)dbReader[off++],
                                         activate_condition1 = (int)dbReader[off++],
                                         activate_value1_1 = (int)dbReader[off++],
                                         activate_value1_2 = (int)dbReader[off++],
                                         activate_condition2 = (int)dbReader[off++],
                                         activate_value2_1 = (int)dbReader[off++],
                                         activate_value2_2 = (int)dbReader[off++],
                                         activate_condition3 = (int)dbReader[off++],
                                         activate_value3_1 = (int)dbReader[off++],
                                         activate_value3_2 = (int)dbReader[off++],
                                         activate_condition4 = (int)dbReader[off++],
                                         activate_value4_1 = (int)dbReader[off++],
                                         activate_value4_2 = (int)dbReader[off++],
                                         activate_condition5 = (int)dbReader[off++],
                                         activate_value5_1 = (int)dbReader[off++],
                                         activate_value5_2 = (int)dbReader[off++],
                                         activate_condition6 = (int)dbReader[off++],
                                         activate_value6_1 = (int)dbReader[off++],
                                         activate_value6_2 = (int)dbReader[off++],
                                         count_limit = (int)dbReader[off++],
                                         script = (string)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from EventAreaResource", EventAreaResource.Count);
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

            ConsoleUtils.ShowNotice("{0} entries loaded from ItemEffectResource", ItemEffectResource.Count);
        }

        internal static void LoadMonster()
        {
            MonsterResource = new List<DB_Monster>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM MonsterResource"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                MonsterResource.Add(
                                    new DB_Monster
                                    {
                                        id = (int)dbReader[off++],
                                        monster_group = (int)dbReader[off++],
                                        name_id = (int)dbReader[off++],
                                        location_id = (int)dbReader[off++],
                                        model = (string)dbReader[off++],
                                        motion_file_id = (int)dbReader[off++],
                                        transform_level = (int)dbReader[off++],
                                        walk_type = (byte)dbReader[off++],
                                        slant_type = (byte)dbReader[off++],
                                        size = (decimal)dbReader[off++],
                                        scale = (decimal)dbReader[off++],
                                        target_fx_size = (decimal)dbReader[off++],
                                        camera_x = (int)dbReader[off++],
                                        camera_y = (int)dbReader[off++],
                                        camera_z = (int)dbReader[off++],
                                        target_x = (decimal)dbReader[off++],
                                        target_y = (decimal)dbReader[off++],
                                        target_z = (decimal)dbReader[off++],
                                        level = (int)dbReader[off++],
                                        grp = (int)dbReader[off++],
                                        magic_type = (int)dbReader[off++],
                                        race = (int)dbReader[off++],
                                        visible_range = (int)dbReader[off++],
                                        chase_range = (int)dbReader[off++],
                                        f_first_attack = (byte)dbReader[off++],
                                        f_group_first_attack = (byte)dbReader[off++],
                                        f_response_casting = (byte)dbReader[off++],
                                        f_response_race = (byte)dbReader[off++],
                                        f_response_battle = (byte)dbReader[off++],
                                        monster_type = (byte)dbReader[off++],
                                        stat_id = (int)dbReader[off++],
                                        fight_type = (int)dbReader[off++],
                                        monster_skill_link_id = (int)dbReader[off++],
                                        material = (int)dbReader[off++],
                                        weapon_type = (int)dbReader[off++],
                                        attack_motion_speed = (int)dbReader[off++],
                                        ability = (int)dbReader[off++],
                                        standard_walk_speed = (int)dbReader[off++],
                                        standard_run_speed = (int)dbReader[off++],
                                        walk_speed = (int)dbReader[off++],
                                        run_speed = (int)dbReader[off++],
                                        attack_range = (decimal)dbReader[off++],
                                        hidesense_range = (decimal)dbReader[off++],
                                        hp = (int)dbReader[off++],
                                        mp = (int)dbReader[off++],
                                        attack_point = (int)dbReader[off++],
                                        magic_point = (int)dbReader[off++],
                                        defence = (int)dbReader[off++],
                                        magic_defence = (int)dbReader[off++],
                                        attack_speed  = (int)dbReader[off++],
                                        magic_speed = (int)dbReader[off++],
                                        accuracy = (int)dbReader[off++],
                                        avoid = (int)dbReader[off++],
                                        magic_accuracy = (int)dbReader[off++],
                                        magic_avoid = (int)dbReader[off++],
                                        taming_id = (int)dbReader[off++],
                                        taming_percentage = (decimal)dbReader[off++],
                                        taming_exp_mod = (decimal)dbReader[off++],
                                        exp = (int)dbReader[off++],
                                        jp = (int)dbReader[off++],
                                        gold_drop_percentage = (int)dbReader[off++],
                                        gold_min = (int)dbReader[off++],
                                        gold_max = (int)dbReader[off++],
                                        chaos_drop_percentage = (int)dbReader[off++],
                                        chaos_min = (int)dbReader[off++],
                                        chaos_max = (int)dbReader[off++],
                                        exp_2 = (int)dbReader[off++],
                                        jp_2 = (int)dbReader[off++],
                                        gold_min_2 = (int)dbReader[off++],
                                        gold_max_2 = (int)dbReader[off++],
                                        chaos_min_2 = (int)dbReader[off++],
                                        chaos_max_2 = (int)dbReader[off++],
                                        drop_table_link_id = (int)dbReader[off++],
                                        texture_group = (int)dbReader[off++],
                                        local_flag = (int)dbReader[off++],
                                        script_on_dead = (string)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from MonsterResource", MonsterResource.Count);
        }

        internal static void LoadMonsterDropTable()
        {
            MonsterDropTableResource = new List<DB_MonsterDropTable>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM MonsterDropTableResource ORDER BY id, sub_id"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                MonsterDropTableResource.Add(
                                    new DB_MonsterDropTable
                                    {
                                        id = (int)dbReader[off++],
                                        sub_id = (int)dbReader[off++],
                                        drop_item_id_00 = (int)dbReader[off++],
                                        drop_percentage_00 = (decimal)dbReader[off++],
                                        drop_min_count_00 = (short)dbReader[off++],
                                        drop_max_count_00 = (short)dbReader[off++],
                                        drop_min_level_00 = (short)dbReader[off++],
                                        drop_max_level_00 = (short)dbReader[off++],
                                        drop_item_id_01 = (int)dbReader[off++],
                                        drop_percentage_01 = (decimal)dbReader[off++],
                                        drop_min_count_01 = (short)dbReader[off++],
                                        drop_max_count_01 = (short)dbReader[off++],
                                        drop_min_level_01 = (short)dbReader[off++],
                                        drop_max_level_01 = (short)dbReader[off++],
                                        drop_item_id_02 = (int)dbReader[off++],
                                        drop_percentage_02 = (decimal)dbReader[off++],
                                        drop_min_count_02 = (short)dbReader[off++],
                                        drop_max_count_02 = (short)dbReader[off++],
                                        drop_min_level_02 = (short)dbReader[off++],
                                        drop_max_level_02 = (short)dbReader[off++],
                                        drop_item_id_03 = (int)dbReader[off++],
                                        drop_percentage_03 = (decimal)dbReader[off++],
                                        drop_min_count_03 = (short)dbReader[off++],
                                        drop_max_count_03 = (short)dbReader[off++],
                                        drop_min_level_03 = (short)dbReader[off++],
                                        drop_max_level_03 = (short)dbReader[off++],
                                        drop_item_id_04 = (int)dbReader[off++],
                                        drop_percentage_04 = (decimal)dbReader[off++],
                                        drop_min_count_04 = (short)dbReader[off++],
                                        drop_max_count_04 = (short)dbReader[off++],
                                        drop_min_level_04 = (short)dbReader[off++],
                                        drop_max_level_04 = (short)dbReader[off++],
                                        drop_item_id_05 = (int)dbReader[off++],
                                        drop_percentage_05 = (decimal)dbReader[off++],
                                        drop_min_count_05 = (short)dbReader[off++],
                                        drop_max_count_05 = (short)dbReader[off++],
                                        drop_min_level_05 = (short)dbReader[off++],
                                        drop_max_level_05 = (short)dbReader[off++],
                                        drop_item_id_06 = (int)dbReader[off++],
                                        drop_percentage_06 = (decimal)dbReader[off++],
                                        drop_min_count_06 = (short)dbReader[off++],
                                        drop_max_count_06 = (short)dbReader[off++],
                                        drop_min_level_06 = (short)dbReader[off++],
                                        drop_max_level_06 = (short)dbReader[off++],
                                        drop_item_id_07 = (int)dbReader[off++],
                                        drop_percentage_07 = (decimal)dbReader[off++],
                                        drop_min_count_07 = (short)dbReader[off++],
                                        drop_max_count_07 = (short)dbReader[off++],
                                        drop_min_level_07 = (short)dbReader[off++],
                                        drop_max_level_07 = (short)dbReader[off++],
                                        drop_item_id_08 = (int)dbReader[off++],
                                        drop_percentage_08 = (decimal)dbReader[off++],
                                        drop_min_count_08 = (short)dbReader[off++],
                                        drop_max_count_08 = (short)dbReader[off++],
                                        drop_min_level_08 = (short)dbReader[off++],
                                        drop_max_level_08 = (short)dbReader[off++],
                                        drop_item_id_09 = (int)dbReader[off++],
                                        drop_percentage_09 = (decimal)dbReader[off++],
                                        drop_min_count_09 = (short)dbReader[off++],
                                        drop_max_count_09 = (short)dbReader[off++],
                                        drop_min_level_09 = (short)dbReader[off++],
                                        drop_max_level_09 = (short)dbReader[off++],
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

            ConsoleUtils.ShowNotice("{0} entries loaded from MonsterDropTableResource", MonsterDropTableResource.Count);
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

            ConsoleUtils.ShowNotice("{0} entries loaded from MonsterSkillResource", MonsterSkillResource.Count);
        }

        internal static void LoadSetItemEffect()
        {
            SetItemEffectResource = new List<DB_SetItemEffect>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM SetItemEffectResource"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                SetItemEffectResource.Add(
                                    new DB_SetItemEffect
                                    {
                                         set_id = (int)dbReader[off++],
                                         set_part_id = (int)dbReader[off++],
                                         text_id = (int)dbReader[off++],
                                         tooltip_id = (int)dbReader[off++],
                                         base_type_0 = (short)dbReader[off++],
                                         base_var1_0 = (decimal)dbReader[off++],
                                         base_var2_0 = (decimal)dbReader[off++],
                                         base_type_1 = (short)dbReader[off++],
                                         base_var1_1 = (decimal)dbReader[off++],
                                         base_var2_1 = (decimal)dbReader[off++],
                                         base_type_2 = (short)dbReader[off++],
                                         base_var1_2 = (decimal)dbReader[off++],
                                         base_var2_2 = (decimal)dbReader[off++],
                                         base_type_3 = (short)dbReader[off++],
                                         base_var1_3 = (decimal)dbReader[off++],
                                         base_var2_3 = (decimal)dbReader[off++],
                                         opt_type_0 = (short)dbReader[off++],
                                         opt_var1_0 = (decimal)dbReader[off++],
                                         opt_var2_0 = (decimal)dbReader[off++],
                                         opt_type_1 = (short)dbReader[off++],
                                         opt_var1_1 = (decimal)dbReader[off++],
                                         opt_var2_1 = (decimal)dbReader[off++],
                                         opt_type_2 = (short)dbReader[off++],
                                         opt_var1_2 = (decimal)dbReader[off++],
                                         opt_var2_2 = (decimal)dbReader[off++],
                                         opt_type_3 = (short)dbReader[off++],
                                         opt_var1_3 = (decimal)dbReader[off++],
                                         opt_var2_3 = (decimal)dbReader[off++],
                                         effect_id = (int)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from SetItemEffectResource", SetItemEffectResource.Count);
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

            ConsoleUtils.ShowNotice("{0} entries loaded from StringResource", StringResource.Count);
        }

        internal static void LoadSummon()
        {
            SummonResource = new List<DB_Summon>();

            using (DBManager dbManager = new DBManager(conType, conString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM SummonResource"))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                SummonResource.Add(
                                    new DB_Summon
                                    {
                                        id = (int)dbReader[off++],
                                        model_id = (int)dbReader[off++],
                                        name_id = (int)dbReader[off++],
                                        type = (int)dbReader[off++],
                                        magic_type = (int)dbReader[off++],
                                        rate = (byte)dbReader[off++],
                                        stat_id = (int)dbReader[off++],
                                        size = (decimal)dbReader[off++],
                                        scale = (decimal)dbReader[off++],
                                        target_fx_size = (decimal)dbReader[off++],
                                        standard_walk_speed = (int)dbReader[off++],
                                        standard_run_speed = (int)dbReader[off++],
                                        riding_speed = (int)dbReader[off++],
                                        run_speed = (int)dbReader[off++],
                                        is_riding_only = (byte)dbReader[off++],
                                        riding_motion_type = (int)dbReader[off++],
                                        attack_range = (decimal)dbReader[off++],
                                        walk_type = (int)dbReader[off++],
                                        slant_type = (int)dbReader[off++],
                                        material = (int)dbReader[off++],
                                        weapon_type = (int)dbReader[off++],
                                        attack_motion_speed = (int)dbReader[off++],
                                        form = (int)dbReader[off++],
                                        evolve_target = (int)dbReader[off++],
                                        camera_x = (int)dbReader[off++],
                                        camera_y = (int)dbReader[off++],
                                        camera_z = (int)dbReader[off++],
                                        target_x = (decimal)dbReader[off++],
                                        target_y = (decimal)dbReader[off++],
                                        target_z = (decimal)dbReader[off++],
                                        model = (string)dbReader[off++],
                                        motion_file_id = (int)dbReader[off++],
                                        face_id = (int)dbReader[off++],
                                        face_file_name = (string)dbReader[off++],
                                        card_id = (int)dbReader[off++],
                                        script_text = (string)dbReader[off++],
                                        illust_file_name = (string)dbReader[off++],
                                        text_feature_id = (int)dbReader[off++],
                                        text_name_id = (int)dbReader[off++],
                                        skill1_id = (int)dbReader[off++],
                                        skill1_text_id = (int)dbReader[off++],
                                        skill2_id = (int)dbReader[off++],
                                        skill2_text_id = (int)dbReader[off++],
                                        skill3_id = (int)dbReader[off++],
                                        skill3_text_id = (int)dbReader[off++],
                                        skill4_id = (int)dbReader[off++],
                                        skill4_text_id = (int)dbReader[off++],
                                        skill5_id = (int)dbReader[off++],
                                        skill5_text_id = (int)dbReader[off++],
                                        texture_group = (int)dbReader[off++],
                                        local_flag = (int)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from SummonResource", SummonResource.Count);
        }

        #endregion

        public static void Initialize(int _conType, string _conString)
        {
            conType = _conType;
            conString = _conString;

            LoadAuctionCategory();
            LoadCreatureBonus();
            LoadDropGroup();
            LoadEventArea();
            LoadItemEffect();
            LoadMonster();
            LoadMonsterDropTable();
            LoadMonsterSkill();
            LoadSetItemEffect();
            LoadString();
            LoadSummon();
        }
    }
}
