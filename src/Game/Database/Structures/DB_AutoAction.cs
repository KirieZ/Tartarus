using System;

namespace Game.Database.Structures
{
	public class DB_AutoAuction
	{
		public int id { get; set; }
		public int item_id { get; set; }
		public int auctionseller_id { get; set; }
		public long price { get; set; }
		public char secroute_apply { get; set; }
		public int local_flag { get; set; }
		public DateTime auction_enrollment_time { get; set; }
		public char repeat_apply { get; set; }
		public int repeat_term { get; set; }
		public short auctiontime_type { get; set; }
	}
}