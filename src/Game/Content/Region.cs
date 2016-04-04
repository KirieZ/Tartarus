// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public class Region
	{
        #region Structures

        private struct RegionPos
        {
            private uint X;
            private uint Y;
            private byte Layer;

            public RegionPos(uint x, uint y, byte layer)
            {
                this.X = x;
                this.Y = y;
                this.Layer = layer;
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets region based on its rx, ry and layer
        /// </summary>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static Region GetRegion(uint rx, uint ry, byte layer = 0)
        {
            Region region;
            if (Region.Regions.TryGetValue(new RegionPos(rx, ry, layer), out region))
                return region;
            else
                return null;
        }

        #endregion

        #region Variables

        /// <summary>
        /// Stores all Regions on a RegionPos key (rx, ry, layer)
        /// </summary>
        private static Dictionary<RegionPos, Region> Regions = new Dictionary<RegionPos, Region>();

        /// <summary>
        /// Region ID
        /// </summary>
        public uint Id { get; private set; }
        /// <summary>
        /// Region X (RX)
        /// </summary>
        public uint X { get; private set; }
        /// <summary>
        /// Region Y (RY)
        /// </summary>
        public uint Y { get; private set; }
        /// <summary>
        /// Region Layer
        /// </summary>
        public byte Layer { get; private set; }

        #endregion

        #region Constructor
       
        /// <summary>
        /// Creates a Region
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="layer"></param>
        public Region(uint x, uint y, byte layer)
        {
            this.X = x;
            this.Y = y;
            this.Layer = layer;

            RegionPos pos = new RegionPos(x, y, layer);
            Region.Regions.Add(pos, this);
        }

        #endregion
    }
}
