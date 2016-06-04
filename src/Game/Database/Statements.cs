﻿using System;
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
                    Game.Add(12, "SELECT [id],[sub_id],[drop_item_id_00],[drop_percentage_00],[drop_min_count_00],[drop_max_count_00],[drop_min_level_00],[drop_max_level_00],[drop_item_id_01],[drop_percentage_01],[drop_min_count_01],[drop_max_count_01],[drop_min_level_01],[drop_max_level_01],[drop_item_id_02],[drop_percentage_02],[drop_min_count_02],[drop_max_count_02],[drop_min_level_02],[drop_max_level_02],[drop_item_id_03],[drop_percentage_03],[drop_min_count_03],[drop_max_count_03],[drop_min_level_03],[drop_max_level_03],[drop_item_id_04],[drop_percentage_04],[drop_min_count_04],[drop_max_count_04],[drop_min_level_04],[drop_max_level_04],[drop_item_id_05],[drop_percentage_05],[drop_min_count_05],[drop_max_count_05],[drop_min_level_05],[drop_max_level_05],[drop_item_id_06],[drop_percentage_06],[drop_min_count_06],[drop_max_count_06],[drop_min_level_06],[drop_max_level_06],[drop_item_id_07],[drop_percentage_07],[drop_min_count_07],[drop_max_count_07],[drop_min_level_07],[drop_max_level_07],[drop_item_id_08],[drop_percentage_08],[drop_min_count_08],[drop_max_count_08],[drop_min_level_08],[drop_max_level_08],[drop_item_id_09],[drop_percentage_09],[drop_min_count_09],[drop_max_count_09],[drop_min_level_09],[drop_max_level_09] FROM [dbo].[MonsterDropTableResource] ORDER BY [id], [sub_id]");
                    Game.Add(13, "SELECT [id],[sub_id],[trigger_1_type],[trigger_1_value_1],[trigger_1_value_2],[trigger_1_function],[trigger_2_type],[trigger_2_value_1],[trigger_2_value_2],[trigger_2_function],[trigger_3_type],[trigger_3_value_1],[trigger_3_value_2],[trigger_3_function],[trigger_4_type],[trigger_4_value_1],[trigger_4_value_2],[trigger_4_function],[trigger_5_type],[trigger_5_value_1],[trigger_5_value_2],[trigger_5_function],[trigger_6_type],[trigger_6_value_1],[trigger_6_value_2],[trigger_6_function],[skill1_id],[skill1_lv],[skill1_probability],[skill2_id],[skill2_lv],[skill2_probability],[skill3_id],[skill3_lv],[skill3_probability],[skill4_id],[skill4_lv],[skill4_probability],[skill5_id],[skill5_lv],[skill5_probability],[skill6_id],[skill6_lv],[skill6_probability] FROM [dbo].[MonsterSkillResource] ORDER BY id, sub_id");
                    Game.Add(14, "SELECT [npc_id],[quest_id],[flag_start],[flag_progress],[flag_end],[text_id_start],[text_id_in_progress],[text_id_end] FROM [dbo].[QuestLinkResource] ORDER BY [npc_id]");
                    Game.Add(15, "SELECT [id],[text_id_quest],[text_id_summary],[text_id_status],[limit_begin_time],[limit_end_time],[limit_level],[limit_job_level],[limit_max_level],[limit_max_job_level],[limit_deva],[limit_asura],[limit_gaia],[limit_fighter],[limit_hunter],[limit_magician],[limit_summoner],[limit_job],[limit_favor_group_id],[limit_favor],[repeatable],[invoke_condition],[invoke_value],[time_limit_type],[time_limit],[type],[value1],[value2],[value3],[value4],[value5],[value6],[value7],[value8],[value9],[value10],[value11],[value12],[drop_group_id],[quest_difficulty],[favor_group_id],[hate_group_id],[favor],[exp],[jp],[holicpoint],[gold],[default_reward_id],[default_reward_level],[default_reward_quantity],[optional_reward_id1],[optional_reward_level1],[optional_reward_quantity1],[optional_reward_id2],[optional_reward_level2],[optional_reward_quantity2],[optional_reward_id3],[optional_reward_level3],[optional_reward_quantity3],[optional_reward_id4],[optional_reward_level4],[optional_reward_quantity4],[optional_reward_id5],[optional_reward_level5],[optional_reward_quantity5],[optional_reward_id6],[optional_reward_level6],[optional_reward_quantity6],[forequest1],[forequest2],[forequest3],[or_flag],[script_start_text],[script_end_text],[script_text] FROM [dbo].[QuestResource]");
                    Game.Add(16, "SELECT [group_id],[quest_target_id],[target_level] FROM [dbo].[RandomPoolResource]");
                    Game.Add(17, "SELECT [set_id],[set_part_id],[text_id],[tooltip_id],[base_type_0],[base_var1_0],[base_var2_0],[base_type_1],[base_var1_1],[base_var2_1],[base_type_2],[base_var1_2],[base_var2_2],[base_type_3],[base_var1_3],[base_var2_3],[opt_type_0],[opt_var1_0],[opt_var2_0],[opt_type_1],[opt_var1_1],[opt_var2_1],[opt_type_2],[opt_var1_2],[opt_var2_2],[opt_type_3],[opt_var1_3],[opt_var2_3],[effect_id] FROM [dbo].[SetItemEffectResource]");
                    Game.Add(18, "SELECT [id],[text_id],[desc_id],[tooltip_id],[is_valid],[elemental],[is_passive],[is_physical_act],[is_harmful],[is_need_target],[is_corpse],[is_toggle],[toggle_group],[casting_type],[casting_level],[cast_range],[valid_range],[cost_hp],[cost_hp_per_skl],[cost_mp],[cost_mp_per_skl],[cost_mp_per_enhance],[cost_hp_per],[cost_hp_per_skl_per],[cost_mp_per],[cost_mp_per_skl_per],[cost_havoc],[cost_havoc_per_skl],[cost_energy],[cost_energy_per_skl],[cost_exp],[cost_exp_per_enhance],[cost_jp],[cost_jp_per_enhance],[cost_item],[cost_item_count],[cost_item_count_per_skl],[need_level],[need_hp],[need_mp],[need_havoc],[need_havoc_burst],[need_state_id],[need_state_level],[need_state_exhaust],[vf_one_hand_sword],[vf_two_hand_sword],[vf_double_sword],[vf_dagger],[vf_double_dagger],[vf_spear],[vf_axe],[vf_one_hand_axe],[vf_double_axe],[vf_one_hand_mace],[vf_two_hand_mace],[vf_lightbow],[vf_heavybow],[vf_crossbow],[vf_one_hand_staff],[vf_two_hand_staff],[vf_shield_only],[vf_is_not_need_weapon],[delay_cast],[delay_cast_per_skl],[delay_cast_mode_per_enhance],[delay_common],[delay_cooltime],[delay_cooltime_mode_per_enhance],[cool_time_group_id],[uf_self],[uf_party],[uf_guild],[uf_neutral],[uf_purple],[uf_enemy],[tf_avatar],[tf_summon],[tf_monster],[target],[effect_type],[state_id],[state_level_base],[state_level_per_skl],[state_level_per_enhance],[state_second],[state_second_per_level],[state_second_per_enhance],[state_type],[probability_on_hit],[probability_inc_by_slv],[hit_bonus],[hit_bonus_per_enhance],[percentage],[hate_mod],[hate_basic],[hate_per_skl],[hate_per_enhance],[critical_bonus],[critical_bonus_per_skl],[var1],[var2],[var3],[var4],[var5],[var6],[var7],[var8],[var9],[var10],[var11],[var12],[var13],[var14],[var15],[var16],[var17],[var18],[var19],[var20],[icon_id],[icon_file_name],[is_projectile],[projectile_speed],[projectile_acceleration] FROM [dbo].[SkillResource]");
                    Game.Add(19, "SELECT [skill_id],[jp_01],[jp_02],[jp_03],[jp_04],[jp_05],[jp_06],[jp_07],[jp_08],[jp_09],[jp_10],[jp_11],[jp_12],[jp_13],[jp_14],[jp_15],[jp_16],[jp_17],[jp_18],[jp_19],[jp_20],[jp_21],[jp_22],[jp_23],[jp_24],[jp_25],[jp_26],[jp_27],[jp_28],[jp_29],[jp_30],[jp_31],[jp_32],[jp_33],[jp_34],[jp_35],[jp_36],[jp_37],[jp_38],[jp_39],[jp_40],[jp_41],[jp_42],[jp_43],[jp_44],[jp_45],[jp_46],[jp_47],[jp_48],[jp_49],[jp_50] FROM [dbo].[SkillJPResource] ORDER BY [skill_id]");
                    Game.Add(20, "SELECT [job_id],[skill_id],[min_skill_lv],[max_skill_lv],[lv],[job_lv],[jp_ratio],[need_skill_id_1],[need_skill_lv_1],[need_skill_id_2],[need_skill_lv_2],[need_skill_id_3],[need_skill_lv_3] FROM [dbo].[SkillTreeResource] ORDER BY [job_id],[skill_id]");
                    Game.Add(21, "SELECT [id],[str],[vit],[dex],[agi],[int],[men],[luk] FROM [dbo].[StatResource]");
                    Game.Add(22, "SELECT [id],[kind],[text_id] FROM [dbo].[SummonDefaultNameResource]");
                    Game.Add(23, "SELECT [level],[normal_exp],[growth_exp],[evolve_exp] FROM [dbo].[SummonLevelResource]");
                    Game.Add(24, "SELECT [id],[kind],[text_id] FROM [dbo].[SummonUniqueNameResource]");
                    Game.Add(25, "SELECT [name],[group_id],[code],[value] FROM [dbo].[StringResource]");
                    Game.Add(26, "SELECT [id],[model_id],[name_id],[type],[magic_type],[rate],[stat_id],[size],[scale],[target_fx_size],[standard_walk_speed],[standard_run_speed],[riding_speed],[run_speed],[is_riding_only],[riding_motion_type],[attack_range],[walk_type],[slant_type],[material],[weapon_type],[attack_motion_speed],[form],[evolve_target],[camera_x],[camera_y],[camera_z],[target_x],[target_y],[target_z],[model],[motion_file_id],[face_id],[face_file_name],[card_id],[script_text],[illust_file_name],[text_feature_id],[text_name_id],[skill1_id],[skill1_text_id],[skill2_id],[skill2_text_id],[skill3_id],[skill3_text_id],[skill4_id],[skill4_text_id],[skill5_id],[skill5_text_id],[texture_group],[local_flag] FROM [dbo].[SummonResource]");
                    Game.Add(27, "SELECT [state_id],[text_id],[tooltip_id],[is_harmful],[state_time_type],[state_group],[duplicate_group_1],[duplicate_group_2],[duplicate_group_3],[uf_avatar],[uf_summon],[uf_monster],[base_effect_id],[fire_interval],[elemental_type],[amplify_base],[amplify_per_skl],[add_damage_base],[add_damage_per_skl],[effect_type],[value_0],[value_1],[value_2],[value_3],[value_4],[value_5],[value_6],[value_7],[value_8],[value_9],[value_10],[value_11],[value_12],[value_13],[value_14],[value_15],[value_16],[value_17],[value_18],[value_19],[icon_id],[icon_file_name],[fx_id],[pos_id],[cast_skill_id],[cast_fx_id],[cast_fx_pos_id],[hit_fx_id],[hit_fx_pos_id],[special_output_timing_id],[special_output_fx_id],[special_output_fx_pos_id],[special_output_fx_delay],[state_fx_id],[state_fx_pos_id] FROM [dbo].[StateResource]");
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
