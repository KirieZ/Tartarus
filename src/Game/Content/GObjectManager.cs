// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Game.Content
{
	public sealed class GObjectManager
	{
        // TODO : Define the right start of these handles
        private uint ItemHandleCount = 0x00000001;
        private uint MonsterHandleCount = 0x10000600;
        private uint NpcHandleCount = 0x20000600;
        private uint PetHandleCount = 0x30000600;
        private uint SkillPropHandleCount = 0x40000600;
        private uint SummonHandleCount = 0x50000600;
        private uint FieldPropHandleCount = 0x60000600;
        private uint PlayerHandleCount = 0x80000600;

        private List<uint> ItemHandlePool = new List<uint>();
        private List<uint> MonsterHandlePool = new List<uint>();
        private List<uint> NpcHandlePool = new List<uint>();
        private List<uint> PetHandlePool = new List<uint>();
        private List<uint> SkillPropHandlePool = new List<uint>();
        private List<uint> SummonHandlePool = new List<uint>();
        private List<uint> FieldPropHandlePool = new List<uint>();
        private List<uint> PlayerHandlePool = new List<uint>();

        private Dictionary<uint, Item> Items { get; set; }
        private Dictionary<uint, Monster> Monsters { get; set; }
        private Dictionary<uint, Npc> Npcs { get; set; }
        private Dictionary<uint, Pet> Pets { get; set; }
        private Dictionary<uint, SkillProp> SkillProps { get; set; }
        private Dictionary<uint, Summon> Summons { get; set; }
        private Dictionary<uint, FieldProp> FieldProps { get; set; }
        private Dictionary<uint, Player> Players { get; set; }

        private List<GameObject> ObjectsToUpdate = new List<GameObject>();

        private static readonly GObjectManager _Instance = new GObjectManager();

        public static GObjectManager Instance
        {
            get { return _Instance; }
        }

        private GObjectManager()
        {
            Items = new Dictionary<uint, Item>();
            Monsters = new Dictionary<uint, Monster>();
            Npcs = new Dictionary<uint, Npc>();
            Pets = new Dictionary<uint, Pet>();
            SkillProps = new Dictionary<uint, SkillProp>();
            Summons = new Dictionary<uint, Summon>();
            FieldProps = new Dictionary<uint, FieldProp>();
            Players = new Dictionary<uint, Player>();

            Timer updateTimer = new Timer(100);
            updateTimer.AutoReset = true;
            updateTimer.Elapsed += UpdateObjects;
            updateTimer.Start();
        }
        
        /// <summary>
        /// Adds an object to ObjectsToUpdate list
        /// </summary>
        /// <param name="gobject">the object to be added</param>
        public void AddToUpdateList(GameObject gobject)
        {
            if (!ObjectsToUpdate.Contains(gobject))
                this.ObjectsToUpdate.Add(gobject);  
        }

        /// <summary>
        /// Allocates a new handle
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Create(GameObject gameObject)
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
                    lock(NpcHandlePool)
                    {
                        if (NpcHandlePool.Count > 0)
                        {
                            handle = NpcHandlePool[0];
                            NpcHandlePool.RemoveAt(0);
                        }
                        else
                        {
                            handle = NpcHandleCount++;
                        }
                        Npcs.Add(handle, (Npc)gameObject);
                    }
                    break;
                case ObjectType.Item:
                    lock(ItemHandlePool)
                    {
                        if (ItemHandlePool.Count > 0)
                        {
                            handle = ItemHandlePool[0];
                            ItemHandlePool.RemoveAt(0);
                        }
                        else
                        {
                            handle = ItemHandleCount++;
                        }
                        Items.Add(handle, (Item)gameObject);
                    }
                    break;
                case ObjectType.Mob:
                    lock (MonsterHandlePool)
                    {
                        if (MonsterHandlePool.Count > 0)
                        {
                            handle = MonsterHandlePool[0];
                            MonsterHandlePool.RemoveAt(0);
                        }
                        else
                        {
                            handle = MonsterHandleCount++;
                        }
                        Monsters.Add(handle, (Monster)gameObject);
                    }
                    break;
                case ObjectType.Summon:
                    lock (SummonHandlePool)
                    {
                        if (SummonHandlePool.Count > 0)
                        {
                            handle = SummonHandlePool[0];
                            SummonHandlePool.RemoveAt(0);
                        }
                        else
                        {
                            handle = SummonHandleCount++;
                        }
                        Summons.Add(handle, (Summon)gameObject);
                    }
                    break;
                case ObjectType.SkillProp:
                    lock (SkillPropHandlePool)
                    {
                        if (SkillPropHandlePool.Count > 0)
                        {
                            handle = SkillPropHandlePool[0];
                            SkillPropHandlePool.RemoveAt(0);
                        }
                        else
                        {
                            handle = SkillPropHandleCount++;
                        }
                        SkillProps.Add(handle, (SkillProp)gameObject);
                    }
                    break;
                case ObjectType.FieldProp:
                    lock (FieldPropHandlePool)
                    {
                        if (FieldPropHandlePool.Count > 0)
                        {
                            handle = FieldPropHandlePool[0];
                            FieldPropHandlePool.RemoveAt(0);
                        }
                        else
                        {
                            handle = FieldPropHandleCount++;
                        }
                        FieldProps.Add(handle, (FieldProp)gameObject);
                    }
                    break;
                case ObjectType.Pet:
                    lock (PetHandlePool)
                    {
                        if (PetHandlePool.Count > 0)
                        {
                            handle = PetHandlePool[0];
                            PetHandlePool.RemoveAt(0);
                        }
                        else
                        {
                            handle = PetHandleCount++;
                        }
                        Pets.Add(handle, (Pet)gameObject);
                    }
                    break;
                default:
                    ConsoleUtils.ShowError("Trying to create invalid GameObject '{0}'.", gameObject.ObjType);
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
        public GameObject Get(ObjectType type, uint handle)
        {
            if (handle == 0) return null;

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
                    {
                        Npc result;
                        if (Npcs.TryGetValue(handle, out result))
                            return result;
                        return null;
                    }
                case ObjectType.Item:
                    {
                        Item result;
                        if (Items.TryGetValue(handle, out result))
                            return result;
                        return null;
                    }
                case ObjectType.Mob:
                    {
                        Monster monster;
                        if (Monsters.TryGetValue(handle, out monster))
                            return monster;
                        return null;
                    }
                case ObjectType.Summon:
                    {
                        Summon summon;
                        if (Summons.TryGetValue(handle, out summon))
                            return summon;
                        return null;
                    }
                case ObjectType.SkillProp:
                    {
                        SkillProp skillProp;
                        if (SkillProps.TryGetValue(handle, out skillProp))
                            return skillProp;
                        return null;
                    }
                case ObjectType.FieldProp:
                    {
                        FieldProp fieldProp;
                        if (FieldProps.TryGetValue(handle, out fieldProp))
                            return fieldProp;
                        return null;
                    }
                case ObjectType.Pet:
                    {
                        Pet pet;
                        if (Pets.TryGetValue(handle, out pet))
                            return pet;
                        return null;
                    }
                default:
                    ConsoleUtils.ShowFatalError("Trying to Get invalid GameObject type ({0})", type);
                    break;
            }

            return null;
        }

        /// <summary>
        /// Updates objects in ObjectsToUpdate List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateObjects(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < this.ObjectsToUpdate.Count; ++i)
            {
                this.ObjectsToUpdate[i].Update();
            }
        }
    }
}
