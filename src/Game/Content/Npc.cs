// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public class Npc : Creature
	{
        public int NpcId { get; private set; }

        public Npc()
            : base(ObjectType.Npc)
        {

        }
	}
}
