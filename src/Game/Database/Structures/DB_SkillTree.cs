using System;

namespace Game.Database.Structures
{
	public class DB_SkillTree
	{
		public int skill_id { get; set; }
		public int min_skill_lv { get; set; }
		public int max_skill_lv { get; set; }
		public int lv { get; set; }
		public int job_lv { get; set; }
		public decimal jp_ratio { get; set; }
		public int need_skill_id_1 { get; set; }
		public int need_skill_lv_1 { get; set; }
		public int need_skill_id_2 { get; set; }
		public int need_skill_lv_2 { get; set; }
		public int need_skill_id_3 { get; set; }
		public int need_skill_lv_3 { get; set; }
	}
}