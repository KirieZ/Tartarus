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
        public int Code { get; private set; }
        public long UId { get; private set; }

        public Item(int code, long uid) : base(ObjectType.Item)
        {
            this.Code = code;
            this.UId = uid;
        }
	}
}
