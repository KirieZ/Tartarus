// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Content
{
	public static class GObjectManager
	{
        private static uint PlayerHandleCount = 0x8006;

        private static List<uint> PlayerHandlePool = new List<uint>();

        private static Dictionary<uint, Player> Players { get; set; }

        public static void Init()
        {
            Players = new Dictionary<uint, Player>();
        }

        /// <summary>
        /// Allocates a new handle
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Create(GameObject gameObject)
        {
            uint handle = 0;
            switch (gameObject.ObjType)
            {
                case ObjectType.Player:
                    lock(PlayerHandlePool)
                    {
                        if (PlayerHandlePool.Count > 0)
                        {
                            handle = PlayerHandlePool[0];
                            PlayerHandlePool.RemoveAt(0);
                        }
                        else
                        {
                            handle = PlayerHandleCount++;
                        }
                        Players.Add(handle, (Player)gameObject);
                    }
                    break;
                case ObjectType.Npc:
                    break;
                case ObjectType.Item:
                    break;
                case ObjectType.Mob:
                    break;
                case ObjectType.Summon:
                    break;
                case ObjectType.SkillProp:
                    break;
                case ObjectType.FieldProp:
                    break;
                case ObjectType.Pet:
                    break;
                default:
                    break;
            }

            gameObject.Handle = handle;

            return handle > 0;
        }

        /// <summary>
        /// Gets a GameObject by its handle and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static GameObject Get(ObjectType type, uint handle)
        {
            switch (type)
            {
                case ObjectType.Player:
                    {
                        Player result;
                        if (Players.TryGetValue(handle, out result))
                            return result;
                        return null;
                    }
                case ObjectType.Npc:
                    break;
                case ObjectType.Item:
                    break;
                case ObjectType.Mob:
                    break;
                case ObjectType.Summon:
                    break;
                case ObjectType.SkillProp:
                    break;
                case ObjectType.FieldProp:
                    break;
                case ObjectType.Pet:
                    break;
                default:
                    break;
            }

            return null;
        }
    }
}
