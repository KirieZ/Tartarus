// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public class SkillProp : GameObject
	{
        public uint Caster { get; private set; }
        public uint StartTime { get; private set; }
        public int SkillNum { get; private set; }

        public SkillProp()
            : base(ObjectType.SkillProp)
        {

        }

	}
}
