// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public abstract class Creature : GameObject
	{
		public uint Status { get; set; }
		public float FaceDirection { get; set; }
		public int Hp { get; set; }
		public int MaxHp { get; set; }
		public short Mp { get; set; }
		public short MaxMp { get; set; }
		public byte Race { get; set; }
		public uint SkinColor { get; set; }

		public Creature (ObjectType objType)
			: base(objType)
		{
		}
	}
}
