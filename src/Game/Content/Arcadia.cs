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
        public static List<DB_AutoAuction> AutoAuctionResource;
        public static List<DB_AuctionCategory> AuctionCategoryResource;
        public static List<DB_CreateLevelBonus> CreatureLevelBonus;
        public static List<DB_EventArea> EventAreaResource;
        public static List<DB_DropGroup> DropGroupResource;
        public static List<DB_Item> ItemResource;
        public static List<DB_JobLevel> JobLevelBonus;
        public static List<DB_Job> JobResource;
        public static List<DB_ItemEffect> ItemEffectResource;
        public static List<DB_Level> LevelResource;
        public static List<DB_Market> MarketResource;
        public static List<DB_Monster> MonsterResource;
        public static List<DB_MonsterDropTable> MonsterDropTableResource;
        public static List<DB_MonsterSkill> MonsterSkillResource;
        public static List<DB_QuestLink> QuestLinkResource;
        public static List<DB_Quest> QuestResource;
        public static List<DB_RandomPool> RandomPoolResource;
        public static List<DB_SetItemEffect> SetItemEffectResource;
        public static List<DB_Skill> SkillResource;
        public static List<DB_SkillJP> SkillJPResource;
        public static List<DB_SkillTree> SkillTreeResource;
        public static List<DB_Stat> StatResource;
        public static List<DB_SummonName> SummonDefaultNameResource;
        public static List<DB_SummonLevel> SummonLevelResource;
        public static List<DB_SummonName> SummonUniqueNameResource;
        public static List<DB_Summon> SummonResource;
        public static List<DB_State> StateResource;
        public static List<DB_String> StringResource;

        #region Load Methods

        internal static void LoadAuctionCategory()
        {
            AuctionCategoryResource = new List<DB_AuctionCategory>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(0))
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

        internal static void LoadAutoAuctionResource()
        {
            AutoAuctionResource = new List<DB_AutoAuction>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(1))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                AutoAuctionResource.Add(
                                    new DB_AutoAuction
                                    {
                                        id = (int)dbReader[off++],
                                        item_id = (int)dbReader[off++],
                                        auctionseller_id = (int)dbReader[off++],
                                        price = (long)dbReader[off++],
                                        secroute_apply = Convert.ToChar(dbReader[off++]),
                                        local_flag = (int)dbReader[off++],
                                        auction_enrollment_time = (DateTime)dbReader[off++],
                                        repeat_apply = Convert.ToChar(dbReader[off++]),
                                        repeat_term = (int)dbReader[off++],
                                        auctiontime_type = (short)dbReader[off++],
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

            ConsoleUtils.ShowNotice("{0} entries loaded from AutoAuctionResource", AutoAuctionResource.Count);
        }

        internal static void LoadCreatureBonus()
        {
            CreatureLevelBonus = new List<DB_CreateLevelBonus>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(2))
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

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(3))
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

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(4))
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

        internal static void LoadItem()
        {
            ItemResource = new List<DB_Item>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(5))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                ItemResource.Add(
                                    new DB_Item
                                    {
                                        id = (int)dbReader[off++],
                                        name_id = (int)dbReader[off++],
                                        tooltip_id = (int)dbReader[off++],
                                        type = (int)dbReader[off++],
                                        Group = (int)dbReader[off++],
                                        Class = (int)dbReader[off++],
                                        wear_type = (int)dbReader[off++],
                                        Set_id = (int)dbReader[off++],
                                        Set_part_flag = (int)dbReader[off++],
                                        grade = (byte)dbReader[off++],
                                        rank = (int)dbReader[off++],
                                        level = (int)dbReader[off++],
                                        enhance = (int)dbReader[off++],
                                        socket = (int)dbReader[off++],
                                        status_flag = (int)dbReader[off++],
                                        limit_deva = Convert.ToChar(dbReader[off++]),
                                        limit_asura = Convert.ToChar(dbReader[off++]),
                                        limit_gaia = Convert.ToChar(dbReader[off++]),
                                        limit_fighter = Convert.ToChar(dbReader[off++]),
                                        limit_hunter = Convert.ToChar(dbReader[off++]),
                                        limit_magician = Convert.ToChar(dbReader[off++]),
                                        limit_summoner = Convert.ToChar(dbReader[off++]),
                                        use_min_level = (int)dbReader[off++],
                                        use_max_level = (int)dbReader[off++],
                                        target_min_level = (int)dbReader[off++],
                                        target_max_level = (int)dbReader[off++],
                                        range = (decimal)dbReader[off++],
                                        weight = (decimal)dbReader[off++],
                                        price = (int)dbReader[off++],
                                        huntaholic_point = (int)dbReader[off++],
                                        ethereal_durability = (int)dbReader[off++],
                                        endurance = (int)dbReader[off++],
                                        material = (int)dbReader[off++],
                                        summon_id = (int)dbReader[off++],
                                        item_use_flag = (int)dbReader[off++],
                                        available_period = (int)dbReader[off++],
                                        decrease_type = (byte)dbReader[off++],
                                        throw_range = (decimal)dbReader[off++],
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
                                        effect_id = (int)dbReader[off++],
                                        enhance_0_id = (short)dbReader[off++],
                                        enhance_0_01 = (decimal)dbReader[off++],
                                        enhance_0_02 = (decimal)dbReader[off++],
                                        enhance_0_03 = (decimal)dbReader[off++],
                                        enhance_0_04 = (decimal)dbReader[off++],
                                        enhance_1_id = (short)dbReader[off++],
                                        enhance_1_01 = (decimal)dbReader[off++],
                                        enhance_1_02 = (decimal)dbReader[off++],
                                        enhance_1_03 = (decimal)dbReader[off++],
                                        enhance_1_04 = (decimal)dbReader[off++],
                                        skill_id = (int)dbReader[off++],
                                        state_id = (int)dbReader[off++],
                                        state_level = (int)dbReader[off++],
                                        state_time = (int)dbReader[off++],
                                        state_type = (int)dbReader[off++],
                                        cool_time = (int)dbReader[off++],
                                        cool_time_group = (short)dbReader[off++],
                                        model_type_dem = (int)dbReader[off++],
                                        model_type_def = (int)dbReader[off++],
                                        model_type_asm = (int)dbReader[off++],
                                        model_type_asf = (int)dbReader[off++],
                                        model_type_gam = (int)dbReader[off++],
                                        model_type_gaf = (int)dbReader[off++],
                                        deco_model_change = (byte)dbReader[off++],
                                        model_00 = (string)dbReader[off++],
                                        model_01 = (string)dbReader[off++],
                                        model_02 = (string)dbReader[off++],
                                        model_03 = (string)dbReader[off++],
                                        model_04 = (string)dbReader[off++],
                                        model_05 = (string)dbReader[off++],
                                        model_06 = (string)dbReader[off++],
                                        model_07 = (string)dbReader[off++],
                                        model_08 = (string)dbReader[off++],
                                        model_09 = (string)dbReader[off++],
                                        model_10 = (string)dbReader[off++],
                                        model_11 = (string)dbReader[off++],
                                        model_12 = (string)dbReader[off++],
                                        model_13 = (string)dbReader[off++],
                                        model_14 = (string)dbReader[off++],
                                        model_15 = (string)dbReader[off++],
                                        model_16 = (string)dbReader[off++],
                                        model_17 = (string)dbReader[off++],
                                        texture_filename = (string)dbReader[off++],
                                        drop_type = (string)dbReader[off++],
                                        icon_id = (int)dbReader[off++],
                                        icon_file_name = (string)dbReader[off++],
                                        script_text = (string)dbReader[off++]
                                    });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ConsoleUtils.ShowSQL(ex.ToString());
                        return;
                    }
                    finally { dbCmd.Connection.Close(); }
                }
            }

            ConsoleUtils.ShowNotice("{0} entries loaded from ItemResource", ItemResource.Count);
        }

        internal static void LoadItemEffect()
        {
            ItemEffectResource = new List<DB_ItemEffect>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(6))
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

        internal static void LoadJobLevel()
        {
            JobLevelBonus = new List<DB_JobLevel>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(7))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                JobLevelBonus.Add(
                                    new DB_JobLevel
                                    {
                                        job_id = (int)dbReader[off++],
                                        str_1 = (decimal)dbReader[off++],
                                        vit_1 = (decimal)dbReader[off++],
                                        dex_1 = (decimal)dbReader[off++],
                                        agi_1 = (decimal)dbReader[off++],
                                        int_1 = (decimal)dbReader[off++],
                                        men_1 = (decimal)dbReader[off++],
                                        luk_1 = (decimal)dbReader[off++],
                                        str_2 = (decimal)dbReader[off++],
                                        vit_2 = (decimal)dbReader[off++],
                                        dex_2 = (decimal)dbReader[off++],
                                        agi_2 = (decimal)dbReader[off++],
                                        int_2 = (decimal)dbReader[off++],
                                        men_2 = (decimal)dbReader[off++],
                                        luk_2 = (decimal)dbReader[off++],
                                        str_3 = (decimal)dbReader[off++],
                                        vit_3 = (decimal)dbReader[off++],
                                        dex_3 = (decimal)dbReader[off++],
                                        agi_3 = (decimal)dbReader[off++],
                                        int_3 = (decimal)dbReader[off++],
                                        men_3 = (decimal)dbReader[off++],
                                        luk_3 = (decimal)dbReader[off++],
                                        default_str = (decimal)dbReader[off++],
                                        default_vit = (decimal)dbReader[off++],
                                        default_dex = (decimal)dbReader[off++],
                                        default_agi = (decimal)dbReader[off++],
                                        default_int = (decimal)dbReader[off++],
                                        default_men = (decimal)dbReader[off++],
                                        default_luk = (decimal)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from JobLevelBonus", JobLevelBonus.Count);
        }

        internal static void LoadJob()
        {
            JobResource = new List<DB_Job>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(8))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                JobResource.Add(
                                    new DB_Job
                                    {
                                        id = (int)dbReader[off++],
                                        text_id = (int)dbReader[off++],
                                        stat_id = (int)dbReader[off++],
                                        job_class = (int)dbReader[off++],
                                        job_depth = Convert.ToChar(dbReader[off++]),
                                        up_lv = (short)dbReader[off++],
                                        up_jlv = (short)dbReader[off++],
                                        available_job_0 = (short)dbReader[off++],
                                        available_job_1 = (short)dbReader[off++],
                                        available_job_2 = (short)dbReader[off++],
                                        available_job_3 = (short)dbReader[off++],
                                        icon_id = (int)dbReader[off++],
                                        icon_file_name = (string)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from JobResource", JobResource.Count);
        }

        internal static void LoadLevel()
        {
            LevelResource = new List<DB_Level>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(9))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                LevelResource.Add(
                                    new DB_Level
                                    {
                                        level = (int)dbReader[0],
                                        normal_exp = (long)dbReader[1],
                                        jl1 = (int)dbReader[2],
                                        jl2 = (int)dbReader[3],
                                        jl3 = (int)dbReader[4],
                                        jl4 = (int)dbReader[0]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from LevelResource", LevelResource.Count);
        }

        internal static void LoadMarket()
        {
            MarketResource = new List<DB_Market>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(10))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                MarketResource.Add(
                                    new DB_Market
                                    {
                                        sort_id = (int)dbReader[0],
                                        name = (string)dbReader[1],
                                        code = (int)dbReader[2],
                                        price_ratio = (decimal)dbReader[3],
                                        huntaholic_ratio = (decimal)dbReader[4]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from MarketResource", MarketResource.Count);
        }

        internal static void LoadMonster()
        {
            MonsterResource = new List<DB_Monster>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(11))
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

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(12))
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

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(13))
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

        internal static void LoadQuestLink()
        {
            QuestLinkResource = new List<DB_QuestLink>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(14))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                QuestLinkResource.Add(
                                    new DB_QuestLink
                                    {
                                        npc_id = (int)dbReader[off++],
                                        quest_id = (int)dbReader[off++],
                                        flag_start = Convert.ToChar(dbReader[off++]),
                                        flag_progress = Convert.ToChar(dbReader[off++]),
                                        flag_end = Convert.ToChar(dbReader[off++]),
                                        text_id_start = (int)dbReader[off++],
                                        text_id_in_progress = (int)dbReader[off++],
                                        text_id_end = (int)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from QuestLinkResource", QuestLinkResource.Count);
        }

        internal static void LoadQuest()
        {
            QuestResource = new List<DB_Quest>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(15))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                QuestResource.Add(
                                    new DB_Quest
                                    {
                                        id = (int)dbReader[off++],
                                        text_id_quest = (int)dbReader[off++],
                                        text_id_summary = (int)dbReader[off++],
                                        text_id_status = (int)dbReader[off++],
                                        limit_begin_time = (int)dbReader[off++],
                                        limit_end_time = (int)dbReader[off++],
                                        limit_level = (int)dbReader[off++],
                                        limit_job_level = (int)dbReader[off++],
                                        limit_max_level = (int)dbReader[off++],
                                        limit_max_job_level = (int)dbReader[off++],
                                        limit_deva = Convert.ToChar(dbReader[off++]),
                                        limit_asura = Convert.ToChar(dbReader[off++]),
                                        limit_gaia = Convert.ToChar(dbReader[off++]),
                                        limit_fighter = Convert.ToChar(dbReader[off++]),
                                        limit_hunter = Convert.ToChar(dbReader[off++]),
                                        limit_magician = Convert.ToChar(dbReader[off++]),
                                        limit_summoner = Convert.ToChar(dbReader[off++]),
                                        limit_job = (int)dbReader[off++],
                                        limit_favor_group_id = (int)dbReader[off++],
                                        limit_favor = (int)dbReader[off++],
                                        repeatable = Convert.ToChar(dbReader[off++]),
                                        invoke_condition = (int)dbReader[off++],
                                        invoke_value = (int)dbReader[off++],
                                        time_limit_type = Convert.ToChar(dbReader[off++]),
                                        time_limit = (int)dbReader[off++],
                                        type = (int)dbReader[off++],
                                        value1 = (int)dbReader[off++],
                                        value2 = (int)dbReader[off++],
                                        value3 = (int)dbReader[off++],
                                        value4 = (int)dbReader[off++],
                                        value5 = (int)dbReader[off++],
                                        value6 = (int)dbReader[off++],
                                        value7 = (int)dbReader[off++],
                                        value8 = (int)dbReader[off++],
                                        value9 = (int)dbReader[off++],
                                        value10 = (int)dbReader[off++],
                                        value11 = (int)dbReader[off++],
                                        value12 = (int)dbReader[off++],
                                        drop_group_id = (int)dbReader[off++],
                                        quest_difficulty = (int)dbReader[off++],
                                        favor_group_id = (int)dbReader[off++],
                                        hate_group_id = (int)dbReader[off++],
                                        favor = (int)dbReader[off++],
                                        exp = (long)dbReader[off++],
                                        jp = (int)dbReader[off++],
                                        holicpoint = (int)dbReader[off++],
                                        gold = (int)dbReader[off++],
                                        default_reward_id = (int)dbReader[off++],
                                        default_reward_level = (int)dbReader[off++],
                                        default_reward_quantity = (int)dbReader[off++],
                                        optional_reward_id1 = (int)dbReader[off++],
                                        optional_reward_level1 = (int)dbReader[off++],
                                        optional_reward_quantity1 = (int)dbReader[off++],
                                        optional_reward_id2 = (int)dbReader[off++],
                                        optional_reward_level2 = (int)dbReader[off++],
                                        optional_reward_quantity2 = (int)dbReader[off++],
                                        optional_reward_id3 = (int)dbReader[off++],
                                        optional_reward_level3 = (int)dbReader[off++],
                                        optional_reward_quantity3 = (int)dbReader[off++],
                                        optional_reward_id4 = (int)dbReader[off++],
                                        optional_reward_level4 = (int)dbReader[off++],
                                        optional_reward_quantity4 = (int)dbReader[off++],
                                        optional_reward_id5 = (int)dbReader[off++],
                                        optional_reward_level5 = (int)dbReader[off++],
                                        optional_reward_quantity5 = (int)dbReader[off++],
                                        optional_reward_id6 = (int)dbReader[off++],
                                        optional_reward_level6 = (int)dbReader[off++],
                                        optional_reward_quantity6 = (int)dbReader[off++],
                                        forequest1 = (int)dbReader[off++],
                                        forequest2 = (int)dbReader[off++],
                                        forequest3 = (int)dbReader[off++],
                                        or_flag = Convert.ToChar(dbReader[off++]),
                                        script_start_text = (string)dbReader[off++],
                                        script_end_text = (string)dbReader[off++],
                                        script_text = (string)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from QuestResource", QuestResource.Count);
        }

        internal static void LoadRandomPool()
        {
            RandomPoolResource = new List<DB_RandomPool>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(16))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                RandomPoolResource.Add(
                                    new DB_RandomPool
                                    {
                                        group_id = (int)dbReader[0],
                                        quest_target_id = (int)dbReader[1],
                                        target_level = (int)dbReader[2]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from RandomPoolResource", RandomPoolResource.Count);
        }

        internal static void LoadSetItemEffect()
        {
            SetItemEffectResource = new List<DB_SetItemEffect>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(17))
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

        internal static void LoadSkill()
        {
            SkillResource = new List<DB_Skill>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(18))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                SkillResource.Add(
                                    new DB_Skill
                                    {
                                        id = (int)dbReader[off++],
                                        text_id = (int)dbReader[off++],
                                        desc_id = (int)dbReader[off++],
                                        tooltip_id = (int)dbReader[off++],
                                        is_valid = (byte)dbReader[off++],
                                        elemental = Convert.ToChar(dbReader[off++]),
                                        is_passive = Convert.ToChar(dbReader[off++]),
                                        is_physical_act = Convert.ToChar(dbReader[off++]),
                                        is_harmful = Convert.ToChar(dbReader[off++]),
                                        is_need_target = Convert.ToChar(dbReader[off++]),
                                        is_corpse = Convert.ToChar(dbReader[off++]),
                                        is_toggle = Convert.ToChar(dbReader[off++]),
                                        toggle_group = (int)dbReader[off++],
                                        casting_type = Convert.ToChar(dbReader[off++]),
                                        casting_level = Convert.ToChar(dbReader[off++]),
                                        cast_range = (int)dbReader[off++],
                                        valid_range = (int)dbReader[off++],
                                        cost_hp = (int)dbReader[off++],
                                        cost_hp_per_skl = (int)dbReader[off++],
                                        cost_mp = (int)dbReader[off++],
                                        cost_mp_per_skl = (int)dbReader[off++],
                                        cost_mp_per_enhance = (int)dbReader[off++],
                                        cost_hp_per = (decimal)dbReader[off++],
                                        cost_hp_per_skl_per = (decimal)dbReader[off++],
                                        cost_mp_per = (decimal)dbReader[off++],
                                        cost_mp_per_skl_per = (decimal)dbReader[off++],
                                        cost_havoc = (int)dbReader[off++],
                                        cost_havoc_per_skl = (int)dbReader[off++],
                                        cost_energy = (decimal)dbReader[off++],
                                        cost_energy_per_skl = (decimal)dbReader[off++],
                                        cost_exp = (int)dbReader[off++],
                                        cost_exp_per_enhance = (int)dbReader[off++],
                                        cost_jp = (int)dbReader[off++],
                                        cost_jp_per_enhance = (int)dbReader[off++],
                                        cost_item = (int)dbReader[off++],
                                        cost_item_count = (int)dbReader[off++],
                                        cost_item_count_per_skl = (int)dbReader[off++],
                                        need_level = (int)dbReader[off++],
                                        need_hp = (int)dbReader[off++],
                                        need_mp = (int)dbReader[off++],
                                        need_havoc = (int)dbReader[off++],
                                        need_havoc_burst = (int)dbReader[off++],
                                        need_state_id = (int)dbReader[off++],
                                        need_state_level = (byte)dbReader[off++],
                                        need_state_exhaust = (byte)dbReader[off++],
                                        vf_one_hand_sword = Convert.ToChar(dbReader[off++]),
                                        vf_two_hand_sword = Convert.ToChar(dbReader[off++]),
                                        vf_double_sword = Convert.ToChar(dbReader[off++]),
                                        vf_dagger = Convert.ToChar(dbReader[off++]),
                                        vf_double_dagger = Convert.ToChar(dbReader[off++]),
                                        vf_spear = Convert.ToChar(dbReader[off++]),
                                        vf_axe = Convert.ToChar(dbReader[off++]),
                                        vf_one_hand_axe = Convert.ToChar(dbReader[off++]),
                                        vf_double_axe = Convert.ToChar(dbReader[off++]),
                                        vf_one_hand_mace = Convert.ToChar(dbReader[off++]),
                                        vf_two_hand_mace = Convert.ToChar(dbReader[off++]),
                                        vf_lightbow = Convert.ToChar(dbReader[off++]),
                                        vf_heavybow = Convert.ToChar(dbReader[off++]),
                                        vf_crossbow = Convert.ToChar(dbReader[off++]),
                                        vf_one_hand_staff = Convert.ToChar(dbReader[off++]),
                                        vf_two_hand_staff = Convert.ToChar(dbReader[off++]),
                                        vf_shield_only = Convert.ToChar(dbReader[off++]),
                                        vf_is_not_need_weapon = Convert.ToChar(dbReader[off++]),
                                        delay_cast = (decimal)dbReader[off++],
                                        delay_cast_per_skl = (decimal)dbReader[off++],
                                        delay_cast_mode_per_enhance = (decimal)dbReader[off++],
                                        delay_common = (decimal)dbReader[off++],
                                        delay_cooltime = (decimal)dbReader[off++],
                                        delay_cooltime_mode_per_enhance = (decimal)dbReader[off++],
                                        cool_time_group_id = (int)dbReader[off++],
                                        uf_self = Convert.ToChar(dbReader[off++]),
                                        uf_party = Convert.ToChar(dbReader[off++]),
                                        uf_guild = Convert.ToChar(dbReader[off++]),
                                        uf_neutral = Convert.ToChar(dbReader[off++]),
                                        uf_purple = Convert.ToChar(dbReader[off++]),
                                        uf_enemy = Convert.ToChar(dbReader[off++]),
                                        tf_avatar = Convert.ToChar(dbReader[off++]),
                                        tf_summon = Convert.ToChar(dbReader[off++]),
                                        tf_monster = Convert.ToChar(dbReader[off++]),
                                        target = (short)dbReader[off++],
                                        effect_type = (short)dbReader[off++],
                                        state_id = (int)dbReader[off++],
                                        state_level_base = (int)dbReader[off++],
                                        state_level_per_skl = (decimal)dbReader[off++],
                                        state_level_per_enhance = (decimal)dbReader[off++],
                                        state_second = (decimal)dbReader[off++],
                                        state_second_per_level = (decimal)dbReader[off++],
                                        state_second_per_enhance = (decimal)dbReader[off++],
                                        state_type = Convert.ToChar(dbReader[off++]),
                                        probability_on_hit = (int)dbReader[off++],
                                        probability_inc_by_slv = (int)dbReader[off++],
                                        hit_bonus = (short)dbReader[off++],
                                        hit_bonus_per_enhance = (short)dbReader[off++],
                                        percentage = (short)dbReader[off++],
                                        hate_mod = (decimal)dbReader[off++],
                                        hate_basic = (short)dbReader[off++],
                                        hate_per_skl = (decimal)dbReader[off++],
                                        hate_per_enhance = (decimal)dbReader[off++],
                                        critical_bonus = (int)dbReader[off++],
                                        critical_bonus_per_skl = (int)dbReader[off++],
                                        var1 = (decimal)dbReader[off++],
                                        var2 = (decimal)dbReader[off++],
                                        var3 = (decimal)dbReader[off++],
                                        var4 = (decimal)dbReader[off++],
                                        var5 = (decimal)dbReader[off++],
                                        var6 = (decimal)dbReader[off++],
                                        var7 = (decimal)dbReader[off++],
                                        var8 = (decimal)dbReader[off++],
                                        var9 = (decimal)dbReader[off++],
                                        var10 = (decimal)dbReader[off++],
                                        var11 = (decimal)dbReader[off++],
                                        var12 = (decimal)dbReader[off++],
                                        var13 = (decimal)dbReader[off++],
                                        var14 = (decimal)dbReader[off++],
                                        var15 = (decimal)dbReader[off++],
                                        var16 = (decimal)dbReader[off++],
                                        var17 = (decimal)dbReader[off++],
                                        var18 = (decimal)dbReader[off++],
                                        var19 = (decimal)dbReader[off++],
                                        var20 = (decimal)dbReader[off++],
                                        icon_id = (int)dbReader[off++],
                                        icon_file_name = (string)dbReader[off++],
                                        is_projectile = (byte)dbReader[off++],
                                        projectile_speed = (decimal)dbReader[off++],
                                        projectile_acceleration = (decimal)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from SkillResource", SkillResource.Count);
        }

        internal static void LoadSkillJP()
        {
            SkillJPResource = new List<DB_SkillJP>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(19))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                SkillJPResource.Add(
                                    new DB_SkillJP
                                    {
                                        skill_id = (int)dbReader[off++],
                                        jp_01 = (int)dbReader[off++],
                                        jp_02 = (int)dbReader[off++],
                                        jp_03 = (int)dbReader[off++],
                                        jp_04 = (int)dbReader[off++],
                                        jp_05 = (int)dbReader[off++],
                                        jp_06 = (int)dbReader[off++],
                                        jp_07 = (int)dbReader[off++],
                                        jp_08 = (int)dbReader[off++],
                                        jp_09 = (int)dbReader[off++],
                                        jp_10 = (int)dbReader[off++],
                                        jp_11 = (int)dbReader[off++],
                                        jp_12 = (int)dbReader[off++],
                                        jp_13 = (int)dbReader[off++],
                                        jp_14 = (int)dbReader[off++],
                                        jp_15 = (int)dbReader[off++],
                                        jp_16 = (int)dbReader[off++],
                                        jp_17 = (int)dbReader[off++],
                                        jp_18 = (int)dbReader[off++],
                                        jp_19 = (int)dbReader[off++],
                                        jp_20 = (int)dbReader[off++],
                                        jp_31 = (int)dbReader[off++],
                                        jp_32 = (int)dbReader[off++],
                                        jp_33 = (int)dbReader[off++],
                                        jp_34 = (int)dbReader[off++],
                                        jp_35 = (int)dbReader[off++],
                                        jp_36 = (int)dbReader[off++],
                                        jp_37 = (int)dbReader[off++],
                                        jp_38 = (int)dbReader[off++],
                                        jp_39 = (int)dbReader[off++],
                                        jp_40 = (int)dbReader[off++],
                                        jp_41 = (int)dbReader[off++],
                                        jp_42 = (int)dbReader[off++],
                                        jp_43 = (int)dbReader[off++],
                                        jp_44 = (int)dbReader[off++],
                                        jp_45 = (int)dbReader[off++],
                                        jp_46 = (int)dbReader[off++],
                                        jp_47 = (int)dbReader[off++],
                                        jp_48 = (int)dbReader[off++],
                                        jp_49 = (int)dbReader[off++],
                                        jp_50 = (int)dbReader[off++],
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

            ConsoleUtils.ShowNotice("{0} entries loaded from SkillJPResource", SkillJPResource.Count);
        }

        internal static void LoadSkillTree()
        {
            SkillTreeResource = new List<DB_SkillTree>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(20))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                SkillTreeResource.Add(
                                    new DB_SkillTree
                                    {
                                        job_id = (int)dbReader[off++],
                                        skill_id = (int)dbReader[off++],
                                        min_skill_lv = (int)dbReader[off++],
                                        max_skill_lv = (int)dbReader[off++],
                                        lv = (int)dbReader[off++],
                                        job_lv = (int)dbReader[off++],
                                        jp_ratio = (decimal)dbReader[off++],
                                        need_skill_id_1 = (int)dbReader[off++],
                                        need_skill_lv_1 = (int)dbReader[off++],
                                        need_skill_id_2 = (int)dbReader[off++],
                                        need_skill_lv_2 = (int)dbReader[off++],
                                        need_skill_id_3 = (int)dbReader[off++],
                                        need_skill_lv_3 = (int)dbReader[off++],
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

            ConsoleUtils.ShowNotice("{0} entries loaded from SkillTreeResource", SkillTreeResource.Count);
        }

        internal static void LoadStat()
        {
            StatResource = new List<DB_Stat>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(21))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                StatResource.Add(
                                    new DB_Stat
                                    {
                                        id = (int)dbReader[off++],
                                        STR = (int)dbReader[off++],
                                        VIT = (int)dbReader[off++],
                                        DEX = (int)dbReader[off++],
                                        AGI = (int)dbReader[off++],
                                        INT = (int)dbReader[off++],
                                        MEN = (int)dbReader[off++],
                                        LUK = (int)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from StatResource", StatResource.Count);
        }

        internal static void LoadSummonDefaultName()
        {
            SummonDefaultNameResource = new List<DB_SummonName>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(22))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                SummonDefaultNameResource.Add(
                                    new DB_SummonName
                                    {
                                        id = (int)dbReader[0],
                                        kind = (int)dbReader[1],
                                        text_id = (int)dbReader[2]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from SummonDefaultNameResource", SummonDefaultNameResource.Count);
        }

        internal static void LoadSummonLevel()
        {
            SummonLevelResource = new List<DB_SummonLevel>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(23))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                SummonLevelResource.Add(
                                    new DB_SummonLevel
                                    {
                                        level = (int)dbReader[0],
                                        normal_exp = (long)dbReader[1],
                                        growth_exp = (long)dbReader[2],
                                        evolve_exp = (long)dbReader[3]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from SummonLevelResource", SummonLevelResource.Count);
        }

        internal static void LoadSummonUniqueName()
        {
            SummonUniqueNameResource = new List<DB_SummonName>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(24))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                SummonDefaultNameResource.Add(
                                    new DB_SummonName
                                    {
                                        id = (int)dbReader[0],
                                        kind = (int)dbReader[1],
                                        text_id = (int)dbReader[2]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from SummonUniqueNameResource", SummonUniqueNameResource.Count);
        }

        internal static void LoadString()
        {
            StringResource = new List<DB_String>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(25))
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

        internal static void LoadState()
        {
            StateResource = new List<DB_State>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(27))
                {
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader dbReader = dbCmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                int off = 0;

                                StateResource.Add(new DB_State
                                {
                                    state_id = (int)dbReader[off++],
                                    text_id = (int)dbReader[off++],
                                    tooltip_id = (int)dbReader[off++],
                                    is_harmful = Convert.ToChar(dbReader[off++]),
                                    state_time_type = (int)dbReader[off++],
                                    state_group = (int)dbReader[off++],
                                    duplicate_group_1 = (int)dbReader[off++],
                                    duplicate_group_2 = (int)dbReader[off++],
                                    duplicate_group_3 = (int)dbReader[off++],
                                    uf_avatar = Convert.ToChar(dbReader[off++]),
                                    uf_summon = Convert.ToChar(dbReader[off++]),
                                    uf_monster = Convert.ToChar(dbReader[off++]),
                                    base_effect_id = (int)dbReader[off++],
                                    fire_interval = (int)dbReader[off++],
                                    elemental_type = (int)dbReader[off++],
                                    amplify_base = (decimal)dbReader[off++],
                                    amplify_per_skl = (decimal)dbReader[off++],
                                    add_damage_base = (int)dbReader[off++],
                                    add_damage_per_skl = (int)dbReader[off++],
                                    effect_type = (int)dbReader[off++],
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
                                    icon_id = (int)dbReader[off++],
                                    icon_file_name = (string)dbReader[off++],
                                    fx_id = (int)dbReader[off++],
                                    pos_id = (int)dbReader[off++],
                                    cast_skill_id = (int)dbReader[off++],
                                    cast_fx_id = (int)dbReader[off++],
                                    cast_fx_pos_id = (int)dbReader[off++],
                                    hit_fx_id = (int)dbReader[off++],
                                    hit_fx_pos_id = (int)dbReader[off++],
                                    special_output_timining_id = (int)dbReader[off++],
                                    special_output_fx_pos_id = (int)dbReader[off++],
                                    special_output_fx_delay = (int)dbReader[off++],
                                    state_fx_id = (int)dbReader[off++],
                                    state_fx_pos_id = (int)dbReader[off++]
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

            ConsoleUtils.ShowNotice("{0} entries loaded from StateResource", StateResource.Count);          
        }

        internal static void LoadSummon()
        {
            SummonResource = new List<DB_Summon>();

            using (DBManager dbManager = new DBManager(Databases.Game))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(26))
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

        public static void Init(int _conType, string _conString)
        {
            conType = _conType;

            conString = _conString;

            LoadAuctionCategory();
            LoadAutoAuctionResource();
            LoadCreatureBonus();
            LoadDropGroup();
            LoadEventArea();
            LoadItem();
            LoadItemEffect();
            LoadJobLevel();
            LoadJob();
            LoadLevel();
            LoadMarket();
            LoadMonster();
            LoadMonsterDropTable();
            LoadMonsterSkill();
            LoadQuestLink();
            LoadQuest();
            LoadRandomPool();
            LoadSetItemEffect();
            LoadSkill();
            LoadSkillJP();
            LoadSkillTree();
            LoadStat();
            LoadSummonDefaultName();
            LoadSummonLevel();
            LoadSummonUniqueName();
            LoadString();
            LoadState();
            LoadSummon();
        }
    }
}
