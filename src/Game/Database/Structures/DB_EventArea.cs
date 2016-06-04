using System;

namespace Game.Database.Structures
{
	public class DB_EventArea
	{
		public int begin_time { get; set; }
		public int end_time { get; set; }
		public int min_level { get; set; }
		public int max_level { get; set; }
		public long race_job_limit { get; set; }
		public int activate_condition1 { get; set; }
		public int activate_value1_1 { get; set; }
		public int activate_value1_2 { get; set; }
		public int activate_condition2 { get; set; }
		public int activate_value2_1 { get; set; }
		public int activate_value2_2 { get; set; }
		public int activate_condition3 { get; set; }
		public int activate_value3_1 { get; set; }
		public int activate_value3_2 { get; set; }
		public int activate_condition4 { get; set; }
		public int activate_value4_1 { get; set; }
		public int activate_value4_2 { get; set; }
		public int activate_condition5 { get; set; }
		public int activate_value5_1 { get; set; }
		public int activate_value5_2 { get; set; }
		public int activate_condition6 { get; set; }
		public int activate_value6_1 { get; set; }
		public int activate_value6_2 { get; set; }
		public int count_limit { get; set; }
		public string script { get; set; }
	}
}