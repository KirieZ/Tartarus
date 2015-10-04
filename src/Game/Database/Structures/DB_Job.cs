using System;

namespace Game.Database.Structures
{
	public class DB_Job
	{
		public int id { get; set; }
		public int text_id { get; set; }
		public int stat_id { get; set; }
		public int job_class { get; set; }
		public char job_depth { get; set; }
		public short up_lv { get; set; }
		public short up_jlv { get; set; }
		public short available_job_0 { get; set; }
		public short available_job_1 { get; set; }
		public short available_job_2 { get; set; }
		public short available_job_3 { get; set; }
		public int icon_id { get; set; }
		public string icon_file_name { get; set; }
	}
}