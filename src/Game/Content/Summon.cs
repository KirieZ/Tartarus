// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public class Summon : Creature
    {
        public uint MasterHandle { get; private set; }
        public int SummonCode { get; private set; }
        public string Name { get; private set; }

        public Summon()
            : base(ObjectType.Summon)
        {

        }
    }
}
