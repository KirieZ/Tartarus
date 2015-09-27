using System;

namespace Game.Database.Structures
{
	public class DB_MonsterSkill
	{
		public int id { get; set; }
		public int sub_id { get; set; }
		public int trigger_1_type { get; set; }
		public decimal trigger_1_value_1 { get; set; }
		public decimal trigger_1_value_2 { get; set; }
		public string trigger_1_function { get; set; }
        public int trigger_2_type { get; set; }
		public decimal trigger_2_value_1 { get; set; }
		public decimal trigger_2_value_2 { get; set; }
		public string trigger_2_function { get; set; }
        public int trigger_3_type { get; set; }
		public decimal trigger_3_value_1 { get; set; }
		public decimal trigger_3_value_2 { get; set; }
		public string trigger_3_function { get; set; }
        public int trigger_4_type { get; set; }
		public decimal trigger_4_value_1 { get; set; }
		public decimal trigger_4_value_2 { get; set; }
		public string trigger_4_function { get; set; }
        public int trigger_5_type { get; set; }
		public decimal trigger_5_value_1 { get; set; }
		public decimal trigger_5_value_2 { get; set; }
		public string trigger_5_function { get; set; }
        public int trigger_6_type { get; set; }
		public decimal trigger_6_value_1 { get; set; }
		public decimal trigger_6_value_2 { get; set; }
		public string trigger_6_function { get; set; }
		public int skill1_id { get; set; }
		public int skill1_lv { get; set; }
		public decimal skill1_probability { get; set; }
		public int skill2_id { get; set; }
		public int skill2_lv { get; set; }
        public decimal skill2_probability { get; set; }
		public int skill3_id { get; set; }
		public int skill3_lv { get; set; }
        public decimal skill3_probability { get; set; }
		public int skill4_id { get; set; }
		public int skill4_lv { get; set; }
        public decimal skill4_probability { get; set; }
		public int skill5_id { get; set; }
		public int skill5_lv { get; set; }
        public decimal skill5_probability { get; set; }
		public int skill6_id { get; set; }
		public int skill6_lv { get; set; }
        public decimal skill6_probability { get; set; }
	}
}