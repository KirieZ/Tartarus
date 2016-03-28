// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public class Pet : Creature
	{
        public int MasterHandle { get; private set; }
        public int PetCode { get; private set; }
        public string Name { get; private set; }

        public Pet () :
            base(ObjectType.Pet)
        {

        }
	}
}
