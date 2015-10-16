// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public abstract class CreatureInfo : GameObject
	{
		public uint Status { get; set; }
		public float FaceDirection { get; set; }
		public int Hp { get; set; }
		public int MaxHp { get; set; }
		public int Mp { get; set; }
		public int MaxMp { get; set; }
		public byte Race { get; set; }
		public uint SkinColor { get; set; }

		public CreatureInfo(ObjectType objType)
			: base(objType)
		{
		}
	}
}
