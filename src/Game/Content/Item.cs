// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
    public enum Wear : int
    {
        None = -1,
        Weapon = 0,
        LeftHand = 1,
        Armor = 2,
        Helm = 3,
        Glove = 4,
        Boots = 5,
        Belt = 6,
        Mantle = 7,
        Armulet = 8,
        Ring1 = 9,
        Ring2 = 10,
        Ear = 11,
        Face = 12,
        Hair = 13,
        DecoWeapon = 14,
        DecoLeftHand = 15,
        DecoArmor = 16,
        DecoHelm = 17,
        DecoGlove = 18,
        DecoBoots = 19,
        DecoMantle = 20,
        DecoShoulder = 21,
        Ride = 22,
        Bag = 23,

        Max, // This must always be after the last equip slot

        // TwoFingerRing = 94,
        TwoHand = 99,
        Skill = 100,
    }
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
        public uint Endurance { get; set; }
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
