using System;

namespace Game.Database.Structures
{
	public class DB_SummonLevel
	{
		public int level { get; set; }
		public long normal_exp { get; set; }
		public long growth_exp { get; set; }
		public long evolve_exp { get; set; }
	}
}