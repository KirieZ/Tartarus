using System;

namespace Game.Database.Structures
{
	public class DB_QuestLink
	{
		public int quest_id { get; set; }
		public char flag_start { get; set; }
		public char flag_progress { get; set; }
		public char flag_end { get; set; }
		public int text_id_start { get; set; }
		public int text_id_in_progress { get; set; }
		public int text_id_end { get; set; }
	}
}