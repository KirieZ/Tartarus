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
	/// Stores a Creature basic stats
	/// </summary>
	public class CreatureStat
	{
		public short StatId { get; set; }
		public short STR { get; set; }
		public short VIT { get; set; }
		public short DEX { get; set; }
		public short AGI { get; set; }
		public short INT { get; set; }
		public short MEN { get; set; }
		public short LUK { get; set; }
	}

	/// <summary>
	/// Stores a Creature special stats
	/// </summary>
	public class CreatureAttribute
	{
		public short Critical { get; set; }
		public short CriticalPower { get; set; }
		public short PAttackRight { get; set; }
		public short PAttackLeft { get; set; }
		public short Defense { get; set; }
		public short BlockDefense { get; set; }
		public short MAttack { get; set; }
		public short MDefense { get; set; }
		public short AccuracyRight { get; set; }
		public short AccuracyLeft { get; set; }
		public short MagicAccuracy { get; set; }
		public short Evasion { get; set; }
		public short MagicEvasion { get; set; }
		public short BlockChance { get; set; }
		public short MoveSpeed { get; set; }
		public short AttackSpeed { get; set; }
		public short AttackRange { get; set; }
		public short MaxWeight { get; set; }
		public short CastingSpeed { get; set; }
		public short CoolTimeSpeed { get; set; }
		public short ItemChance { get; set; }
		public short HPRegenPercentage { get; set; }
		public short HPRegenPoint { get; set; }
		public short MPRegenPercentage { get; set; }
		public short MPRegenPoint { get; set; }
	}
}
