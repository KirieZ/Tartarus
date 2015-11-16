// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Players.Structures
{
	/// <summary>
	/// Holds data of a character previous job
	/// (JobNId , JobNLevel) from user.Characters
	/// </summary>
	public class PreviousJobData
	{
		public int Id { get; set; }
		public int Level { get; set; }
	}

	/// <summary>
	/// Holds data about a belt slot
	/// </summary>
	public class BeltSlotData
	{
        public uint Handle { get; set; }
		public long Id { get; set; }
	}

	/// <summary>
	/// Holds data about a summon
	/// </summary>
	public class SummonData
	{
        public uint CardHandle { get; set; }
		public int Id { get; set; }
	}

	/// <summary>
	/// Holds data about huntaholic
	/// </summary>
	public class HuntaholicData
	{
		public int Points { get; set; }
		public int EnterCount { get; set; }
	}

	/// <summary>
	/// Holds position
	/// </summary>
	public class Position
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public byte Layer { get; set; }
	}
}
