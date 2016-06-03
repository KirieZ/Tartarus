// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using Game.Content.Structures;
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

        public byte MoveSpeed { get; set; }

        public Position Position { get; set; }
        public List<Position> PositionsToMove { get; set; }

        public Creature (ObjectType objType)
			: base(objType)
		{
            this.Position = new Position();
            this.LastUpdate = Globals.GetTime();
		}

        public void Move()
        {
            if (this.PositionsToMove.Count == 0)
                return;

            uint deltaTime = Globals.GetTime() - this.LastUpdate;
            float moveDistance = this.MoveSpeed * deltaTime;
            int count = 0, maxPositions = this.PositionsToMove.Count;

            while (moveDistance > 0 && maxPositions > count)
            {
                Position nextPos = PositionsToMove[count];
                float deltaX = (nextPos.X - this.Position.X);
                float deltaY = (nextPos.Y - this.Position.Y);
                float pointsDistance = (float)Math.Sqrt(((double)(deltaX * deltaX + deltaY * deltaY)));

                if (moveDistance >= pointsDistance)
                {
                    moveDistance -= pointsDistance;
                    ++count;
                    this.Position = nextPos;
                }
                else
                {
                    double angle = Math.Atan((deltaY / deltaX));
                    this.Position.X += ((float)(moveDistance * Math.Cos(angle)));
                    this.Position.Y += ((float)(moveDistance * Math.Sin(angle)));
                }
            }

            this.PositionsToMove.RemoveRange(0, count);

            // TODO : Broadcast position update
        }
    }
}
