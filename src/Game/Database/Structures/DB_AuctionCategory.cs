using System;

namespace Game.Database.Structures
{
	public class DB_AuctionCategory
	{
		public int sub_category_id { get; set; }
		public int name_id { get; set; }
		public int local_flag { get; set; }
		public int item_group { get; set; }
		public int item_class { get; set; }
	}
}