// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content.Structures
{
    /// <summary>
    /// Holds an Entity Position in the GameWorld
    /// </summary>
    public class Position
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public byte Layer { get; set; }

        public Position()
        {

        }

        public Position(float x, float y, float z, byte layer)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Layer = layer;
        }
    }
}
