using System;

namespace Game.Database.Structures
{
	public class DB_Quest
	{
		public int id { get; set; }
		public int text_id_quest { get; set; }
		public int text_id_summary { get; set; }
		public int text_id_status { get; set; }
		public int limit_begin_time { get; set; }
		public int limit_end_time{ get; set; }
		public int limit_level { get; set; }
		public int limit_job_level { get; set; }
		public int limit_max_level { get; set; }
		public int limit_max_job_level { get; set; }
		public char limit_deva { get; set; }
		public char limit_asura { get; set; }
		public char limit_gaia { get; set; }
		public char limit_fighter { get; set; }
		public char limit_hunter { get; set; }
		public char limit_magician { get; set; }
		public char limit_summoner { get; set; }
		public int limit_job { get; set; }
		public int limit_favor_group_id { get; set; }
		public int limit_favor { get; set; }
		public char repeatable { get; set; }
		public int invoke_condition { get; set; }
		public int invoke_value { get; set; }
		public char time_limit_type { get; set; }
		public int time_limit { get; set; }
		public int type { get; set; }
		public int value1 { get; set; }
		public int value2 { get; set; }
		public int value3 { get; set; }
		public int value4 { get; set; }
		public int value5 { get; set; }
		public int value6 { get; set; }
		public int value7 { get; set; }
		public int value8 { get; set; }
		public int value9 { get; set; }
		public int value10 { get; set; }
		public int value11 { get; set; }
		public int value12 { get; set; }
		public int drop_group_id { get; set; }
		public int quest_difficulty { get; set; }
		public int favor_group_id { get; set; }
		public int hate_group_id { get; set; }
		public int favor { get; set; }
		public long exp { get; set; }
		public int jp { get; set; }
		public int holicpoint { get; set; }
		public int gold { get; set; }
		public int default_reward_id { get; set; }
		public int default_reward_level { get; set; }
		public int default_reward_quantity { get; set; }
		public int optional_reward_id1 { get; set; }
		public int optional_reward_level1 { get; set; }
		public int optional_reward_quantity1 { get; set; }
		public int optional_reward_id2 { get; set; }
		public int optional_reward_level2 { get; set; }
		public int optional_reward_quantity2 { get; set; }
		public int optional_reward_id3 { get; set; }
		public int optional_reward_level3 { get; set; }
		public int optional_reward_quantity3 { get; set; }
		public int optional_reward_id4 { get; set; }
		public int optional_reward_level4 { get; set; }
		public int optional_reward_quantity4 { get; set; }
		public int optional_reward_id5 { get; set; }
		public int optional_reward_level5 { get; set; }
		public int optional_reward_quantity5 { get; set; }
		public int optional_reward_id6 { get; set; }
		public int optional_reward_level6 { get; set; }
		public int optional_reward_quantity6 { get; set; }
		public int forequest1 { get; set; }
		public int forequest2 { get; set; }
		public int forequest3 { get; set; }
		public char or_flag { get; set; }
		public string script_start_text { get; set; }
		public string script_end_text { get; set; }
		public string script_text { get; set; }
	}
}