// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Network.Packets.CS
{   
	public struct MoveRequest
	{
        public struct MoveInfo
        {
            public float ToX { get; set; }
            public float ToY { get; set; }
        }

        public uint Handle { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public uint CurrentTime { get; set; }
        public bool SpeedSync { get; set; }
        public ushort Count { get; set; }
        public MoveInfo[] Points { get; set; }
        
    }
}
