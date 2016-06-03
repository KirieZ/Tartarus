// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
    public class Monster : Creature
    {
        public int MonsterId { get; private set; }
        public bool IsTamed { get; private set; }

        public Monster()
            : base(ObjectType.Mob)
        {
            this.MonsterId = 30001;
            this.Position = new Structures.Position(168356, 55399, 0, 0);

            // TODO : Monsters should be registered to regions and not to update list
            GObjectManager.Instance.AddToUpdateList(this);
        }

        internal override void Update()
        {
            base.Update();
            this.Move();
            if (this.PositionsToMove.Count == 0)
            {
                Random rnd = new Random();
                this.PositionsToMove.Add(new Structures.Position(
                    ((float)(this.Position.X + rnd.NextDouble() * 5f)),
                    ((float)(this.Position.Y + rnd.NextDouble() * 5f)),
                    0,
                    this.Position.Layer
                    ));
            }

            Console.WriteLine("UPDATE Monster");
        }
    }
}
