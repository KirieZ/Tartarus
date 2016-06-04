using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Game.Database
{
    public class Statements
    {
        public static void Init()
        {
            Dictionary<int,string> Game = new Dictionary<int, string>();
            switch (Settings.SqlEngine)
            {
                case SqlEngine.MsSql:
                    Game.Add(0, "SELECT [category_id],[sub_category_id],[name_id],[local_flag],[item_group],[item_class] FROM [dbo].[AuctionCategoryResource] WHERE [local_flag] & @localflag = 0 OR [local_flag] = 0 ORDER BY [category_id],[sub_category_id]");
                    Game.Add(1, "SELECT [id],[item_id],[auctionseller_id],[price],[secroute_apply],[local_flag],[auction_enrollment_time],[repeat_apply],[repeat_term],[auctiontime_type] FROM [dbo].[AutoAuctionResource] WHERE [local_flag] & @localflag = 0 OR [local_flag] = 0");
                    Game.Add(2, "SELECT [id],[str],[vit],[dex],[agi],[int],[men],[luk] FROM [dbo].[CreatureLevelBonus]");
                    Game.Add(3, "SELECT [id],[drop_item_id_00],[drop_min_count_00],[drop_max_count_00],[drop_percentage_00],[drop_item_id_01],[drop_min_count_01],[drop_max_count_01],[drop_percentage_01],[drop_item_id_02],[drop_min_count_02],[drop_max_count_02],[drop_percentage_02],[drop_item_id_03],[drop_min_count_03],[drop_max_count_03],[drop_percentage_03],[drop_item_id_04],[drop_min_count_04],[drop_max_count_04],[drop_percentage_04],[drop_item_id_05],[drop_min_count_05],[drop_max_count_05],[drop_percentage_05],[drop_item_id_06],[drop_min_count_06],[drop_max_count_06],[drop_percentage_06],[drop_item_id_07],[drop_min_count_07],[drop_max_count_07],[drop_percentage_07],[drop_item_id_08],[drop_min_count_08],[drop_max_count_08],[drop_percentage_08],[drop_item_id_09],[drop_min_count_09],[drop_max_count_09],[drop_percentage_09] FROM [dbo].[DropGroupResource]");
                    Game.Add(4, "SELECT [id],[begin_time],[end_time],[min_level],[max_level],[race_job_limit] ,[activate_condition1],[activate_value1_1],[activate_value1_2],[activate_condition2],[activate_value2_1],[activate_value2_2],[activate_condition3],[activate_value3_1],[activate_value3_2],[activate_condition4],[activate_value4_1],[activate_value4_2],[activate_condition5],[activate_value5_1],[activate_value5_2],[activate_condition6],[activate_value6_1],[activate_value6_2],[count_limit],[script] FROM [dbo].[EventAreaResource]");
                    Game.Add(5, "SELECT [id],[name_id],[tooltip_id],[type],[group],[class],[wear_type],[set_id],[set_part_flag],[grade],[rank],[level],[enhance],[socket],[status_flag],[limit_deva],[limit_asura],[limit_gaia],[limit_fighter],[limit_hunter],[limit_magician],[limit_summoner],[use_min_level],[use_max_level],[target_min_level],[target_max_level],[range],[weight],[price],[huntaholic_point],[ethereal_durability],[endurance],[material],[summon_id],[item_use_flag],[available_period],[decrease_type],[throw_range],[base_type_0],[base_var1_0],[base_var2_0],[base_type_1],[base_var1_1],[base_var2_1],[base_type_2],[base_var1_2],[base_var2_2],[base_type_3],[base_var1_3],[base_var2_3],[opt_type_0],[opt_var1_0],[opt_var2_0],[opt_type_1],[opt_var1_1],[opt_var2_1],[opt_type_2],[opt_var1_2],[opt_var2_2],[opt_type_3],[opt_var1_3],[opt_var2_3],[effect_id],[enhance_0_id],[enhance_0_01],[enhance_0_02],[enhance_0_03],[enhance_0_04],[enhance_1_id],[enhance_1_01],[enhance_1_02],[enhance_1_03],[enhance_1_04],[skill_id],[state_id],[state_level],[state_time],[state_type],[cool_time],[cool_time_group],[model_type_dem],[model_type_def],[model_type_asm],[model_type_asf],[model_type_gam],[model_type_gaf],[deco_model_change],[model_00],[model_01],[model_02],[model_03],[model_04],[model_05],[model_06],[model_07],[model_08],[model_09],[model_10],[model_11],[model_12],[model_13],[model_14],[model_15],[model_16],[model_17],[texture_filename],[drop_type],[icon_id],[icon_file_name],[script_text] FROM [dbo].[ItemResource]");
                    Game.Add(6, "SELECT [id],[ordinal_id],[tooltip_id],[effect_type],[effect_id],[effect_level],[value_0],[value_1],[value_2],[value_3],[value_4],[value_5],[value_6],[value_7],[value_8],[value_9],[value_10],[value_11],[value_12],[value_13],[value_14],[value_15],[value_16],[value_17],[value_18],[value_19] FROM [dbo].[ItemEffectResource] ORDER BY [id],[ordinal_id]");
                    Game.Add(7, "SELECT [job_id],[str_1],[vit_1],[dex_1],[agi_1],[int_1],[men_1],[luk_1],[str_2],[vit_2],[dex_2],[agi_2],[int_2],[men_2],[luk_2],[str_3],[vit_3],[dex_3],[agi_3],[int_3],[men_3],[luk_3],[default_str],[default_vit],[default_dex],[default_agi],[default_int],[default_men],[default_luk] FROM [dbo].[JobLevelBonus]");
                    Game.Add(8, "SELECT [id],[text_id],[stati_id],[job_class],[job_depth],[up_lv],[up_jlv],[available_job_0],[available_job_1],[available_job_2],[available_job_3],[icon_id],[icon_file_name] FROM [dbo].[JobResource]");
                    Game.Add(9, "SELECT [level],[normal_exp],[jl1],[jl2],[jl3],[jl4] FROM [dbo].[LevelResource]");
                    Game.Add(10, "SELECT [sort_id],[name],[code],[price_ratio],[huntaholic_ratio] FROM [dbo].[MarketResource] ORDER BY [name],[sort_id]");
                    Game.Add(11, "SELECT [id],[monster_group],[name_id],[location_id],[model],[motion_file_id],[transform_level],[walk_type],[slant_type],[size],[scale],[target_fx_size],[camera_x],[camera_y],[camera_z],[target_x],[target_y],[target_z],[level],[grp],[magic_type],[race],[visible_range],[chase_range],[f_fisrt_attack],[f_group_first_attack],[f_response_casting],[f_response_race],[f_response_battle],[monster_type],[stat_id],[fight_type],[monster_skill_link_id],[material],[weapon_type],[attack_motion_speed],[ability],[standard_walk_spped],[standard_run_spped],[walk_speed],[run_speed],[attack_range],[hidesense_range],[hp],[mp],[attack_point],[magic_point],[defence],[magic_defence],[attack_speed],[magic_speed],[accuracy],[avoid],[magic_accuracy],[magic_avoid],[taming_id],[taming_percentage],[taming_exp_mod],[exp],[jp],[gold_drop_percentage],[gold_min],[gold_max],[chaos_drop_percentage],[chaos_min],[chaos_max],[exp_2],[jp_2],[gold_min_2],[gold_max_2],[chaos_min_2],[chaos_max_2],[drop_table_link_id],[texture_group],[local_flag],[script_on_dead] FROM [dbo].[MonsterResource] WHERE [local_flag] & @localflag = 0 OR [local_flag] = 0");

                    Game.Add(12, "SELECT * FROM MonsterDropTableResource ORDER BY id, sub_id");
                    Game.Add(13, "SELECT * FROM MonsterSkillResource ORDER BY id, sub_id");
                    Game.Add(14, "SELECT * FROM QuestLinkResource");
                    Game.Add(15, "SELECT * FROM QuestResource");
                    Game.Add(16, "SELECT * FROM RandomPoolResource");
                    Game.Add(17, "SELECT * FROM SetItemEffectResource");
                    Game.Add(18, "SELECT * FROM SkillResource");
                    Game.Add(19, "SELECT * FROM SkillJPResource");
                    Game.Add(20, "SELECT * FROM SkillTreeResource");
                    Game.Add(21, "SELECT * FROM StatResource");
                    Game.Add(22, "SELECT * FROM SummonDefaultNameResource");
                    Game.Add(23, "SELECT * FROM SummonLevelResource");
                    Game.Add(24, "SELECT * FROM SummonUniqueNameResource");
                    Game.Add(25, "SELECT * FROM StringResource");
                    Game.Add(26, "SELECT * FROM SummonResource");
                    Game.Add(27, "SELECT * FROM StateResource");
                    break;
                case SqlEngine.MySql:
                    Game.Add(0, "SELECT * FROM AuctionCategoryResource WHERE `localflag` & @localflag = @localflag ORDER BY category_id, sub_category_id");
                    Game.Add(1, "SELECT * FROM AutoAuctionResource WHERE `localflag` & @localflag = @localflag");
                    Game.Add(2, "SELECT * FROM CreatureLevelBonus");
                    Game.Add(3, "SELECT * FROM DropGroupResource");
                    Game.Add(4, "SELECT * FROM EventAreaResource");
                    Game.Add(5, "SELECT * FROM ItemResource");
                    Game.Add(6, "SELECT * FROM ItemEffectResource ORDER BY id, ordinal_id");
                    Game.Add(7, "SELECT * FROM JobLevelBonus");
                    Game.Add(8, "SELECT * FROM JobResource");
                    Game.Add(9, "SELECT * FROM LevelResource");
                    Game.Add(10, "SELECT * FROM MarketResource");

                    Game.Add(11, "SELECT `id`, `monster_group`, `name_id`, `location_id`, `model`, `motion_file_id`, `transform_level`, `walk_type`, `slant_type`, `size`, `scale`, `target_fx_size`, `camera_x`, `camera_y`, `camera_z`, `target_x`, `target_y`, `target_z`, `level`, `grp`, `magic_type`, `race`, `visible_range`, `chase_range`, `f_fisrt_attack`, `f_group_first_attack`, `f_response_casting`, `f_response_race`, `f_response_battle`, `monster_type`, `stat_id`, `fight_type`, `monster_skill_link_id`, `material`, `weapon_type`, `attack_motion_speed`, `ability`, `standard_walk_spped`, `standard_run_spped`, `walk_speed`, `run_speed`, `attack_range`, `hidesense_range`, `hp`, `mp`, `attack_point`, `magic_point`, `defence`, `magic_defence`, `attack_speed`, `magic_speed`, `accuracy`, `avoid`, `magic_accuracy`, `magic_avoid`, `taming_id`, `taming_percentage`, `taming_exp_mod`, `exp`, `jp`, `gold_drop_percentage`, `gold_min`, `gold_max`, `chaos_drop_percentage`, `chaos_min`, `chaos_max`, `exp_2`, `jp_2`, `gold_min_2`, `gold_max_2`, `chaos_min_2`, `chaos_max_2`, `drop_table_link_id`, `texture_group`, `local_flag`, `script_on_dead` FROM `MonsterResource` WHERE `local_flag` & @localflag = 0 || `local_flag` = 0");

                    Game.Add(12, "SELECT * FROM MonsterDropTableResource ORDER BY id, sub_id");
                    Game.Add(13, "SELECT * FROM MonsterSkillResource ORDER BY id, sub_id");
                    Game.Add(14, "SELECT * FROM QuestLinkResource");
                    Game.Add(15, "SELECT * FROM QuestResource");
                    Game.Add(16, "SELECT * FROM RandomPoolResource");
                    Game.Add(17, "SELECT * FROM SetItemEffectResource");
                    Game.Add(18, "SELECT * FROM SkillResource");
                    Game.Add(19, "SELECT * FROM SkillJPResource");
                    Game.Add(20, "SELECT * FROM SkillTreeResource");
                    Game.Add(21, "SELECT * FROM StatResource");
                    Game.Add(22, "SELECT * FROM SummonDefaultNameResource");
                    Game.Add(23, "SELECT * FROM SummonLevelResource");
                    Game.Add(24, "SELECT * FROM SummonUniqueNameResource");
                    Game.Add(25, "SELECT * FROM StringResource");
                    Game.Add(26, "SELECT * FROM SummonResource");
                    Game.Add(27, "SELECT * FROM StateResource");
                    break;
                default:
                    break;
            }

            Dictionary<int, string>  User = new Dictionary<int, string>();
            // Lobby
            User.Add(0, "SELECT * FROM Characters WHERE account_id = @accId AND delete_date > @now ORDER BY char_id ASC LIMIT "+Globals.MaxCharacters);
            User.Add(1, "SELECT char_id FROM Characters WHERE name = @name AND delete_date > @now");
            if (Settings.SqlEngine == SqlEngine.MySql) // MySQL
                User.Add(2, "INSERT INTO Characters (account_id, name, race, sex, job, level, x, y, texture_id, hair_id, face_id, body_id, hands_id, feet_id, skin_color) VALUES (@accId, @name, @race, @sex, @job, @level, @x, @y, @textureId, @hairId, @faceId, @bodyId, @handsId, @feetId, @skinColor); SELECT last_insert_id()");
            else // SqlSrv
                User.Add(2, "INSERT INTO Characters (account_id, name, race, sex, job, level, x, y, texture_id, hair_id, face_id, body_id, hands_id, feet_id, skin_color) VALUES (@accId, @name, @race, @sex, @job, @level, @x, @y, @textureId, @hairId, @faceId, @bodyId, @handsId, @feetId, @skinColor)");
            User.Add(3, "UPDATE Characters SET delete_date = @now WHERE account_id = @accId AND name = @name");
            User.Add(4, "DELETE FROM Characters WHERE account_id = @accId AND name = @name");
            User.Add(5, "SELECT * FROM Characters WHERE account_id = @accId AND delete_date > @now AND name = @name");
            User.Add(6, "SELECT wear_info, code, enhance, level, elemental_effect_type FROM Item WHERE owner_id = @charId AND wear_info >=  0");
            User.Add(7, "UPDATE Characters SET login_time = NOW(), login_count = login_count + 1 WHERE char_id = @cid");

            // Item
            User.Add(20, "SELECT * FROM Item WHERE owner_id = @charId");
            User.Add(21, "INSERT INTO Item (owner_id, idx, code, cnt, level, enhance, durability, endurance, flag, gcode, wear_info, socket_0, socket_1, socket_2, socket_3, remain_time, elemental_effect_type, elemental_effect_expire_time, elemental_effect_attack_point, elemental_effect_magic_point, create_time) VALUES (@owner_id, @idx, @code, @cnt, @level, @enhance, @durability, @endurance, @flag, @gcode, @wear_info, @socket_0, @socket_1, @socket_2, @socket_3, @remain_time, @elemental_effect_type, @elemental_effect_expire_time, @elemental_effect_attack_point, @elemental_effect_magic_point, @create_time)");

            DBManager.SetStatements(null, Game, User);
        }
    }
}
