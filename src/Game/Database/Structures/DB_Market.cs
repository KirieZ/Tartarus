using System;

namespace Game.Database.Structures
{
	public class DB_Market
	{
		public string name { get; set; }
		public int code { get; set; }
		public decimal price_ratio { get; set; }
		public decimal huntaholic_ratio { get; set; }
	}
}