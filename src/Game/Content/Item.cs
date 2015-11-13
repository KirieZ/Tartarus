// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
    /// <summary>
    /// A game item
    /// </summary>
	public class Item : GameObject
	{
        public int Code { get; set; }
        public long UId { get; set; }
        public long Count { get; set; }
        public int Level { get; set; }
        public int Enhance { get; set; }
        public int Durability { get; set; }
        public int Endurance { get; set; }
        public int Flag { get; set; }
        public int GCode { get; set; }
        public int WearInfo { get; set; }
        public int[] Socket { get; set; }
        public int RemainTime { get; set; }

        public short ElementalEffectType { get; set; }
        public DateTime ElementalEffectExpireTime { get; set; }
        public int ElementalEffectAttackPoint { get; set; }
        public int ElementalEffectMagicPoint { get; set; }

        //public DateTime CreateTime { get; set; }
        //public DateTime DeleteTime { get; set; }

        public Item(long uid) : base(ObjectType.Item)
        {
            this.UId = uid;

            this.Socket = new int[4];
        }

        public Item(long uid, int code) : this(uid)
        {
            this.Code = code;
        }
	}
}
