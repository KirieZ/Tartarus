// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Game.Content
{
	public enum EnterType
	{
		StaticObject = 0,
		MovableObject,
		ClientObject
	}

	public enum ObjectType
	{
		Player = 0,
		Npc,
		Item,
		Mob,
		Summon,
		SkillProp,
		FieldProp,
		Pet
	}

	public abstract class GameObject
	{
		public uint Handle { get; set; }
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public byte Layer { get; set; }
		public ObjectType ObjType { get; set; }

		public GameObject(ObjectType objType)
		{
			this.ObjType = objType;

            if (!GObjectManager.Instance.Create(this))
            {
                ConsoleUtils.ShowError("Failed to get a handle to game object. Type: {0}", objType);
            }
		}
	}
}
