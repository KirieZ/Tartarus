// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public class FieldProp : GameObject
    {
        public int PropId { get; private set; }

        public FieldProp() 
            : base(ObjectType.FieldProp)
        {

        }
    }
}
