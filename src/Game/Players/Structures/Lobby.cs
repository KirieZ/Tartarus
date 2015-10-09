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
	/// Holds data of a model
	/// </summary>
	public class ModelInfo
	{
		public int Sex { get; set; }
		public int Race { get; set; }
		// hairId, faceId, bodyId, handsId, feetId
		public int[] ModelId { get; set; }
		public int TextureId { get; set; }
		public int[] WearInfo { get; set; }

		public ModelInfo()
		{
			this.ModelId = new int[5];
			this.WearInfo = new int[24];
		}
	};

	/// <summary>
	/// Holds a lobby character data
	/// </summary>
	public class LobbyCharacterInfo
	{
		public ModelInfo ModelInfo { get; set; }
		public int Level { get; set; }
		public int Job { get; set; }
		public int JobLevel { get; set; }
		public int ExpPercentage { get; set; }
		public int Hp { get; set; }
		public int Mp { get; set; }
		public int Permission { get; set; }
		public bool IsBanned { get; set; }
		public string Name { get; set; }
		public uint SkinColor { get; set; }
		public string CreateTime { get; set; }
		public string DeleteTime { get; set; }
		public int[] WearItemEnhanceInfo { get; set; }
		public int[] WearItemLevelInfo { get; set; }
		public byte[] WearItemElementalType { get; set; }

		public LobbyCharacterInfo()
		{
			this.ModelInfo = new ModelInfo();
			this.WearItemEnhanceInfo = new int[24];
			this.WearItemLevelInfo = new int[24];
			this.WearItemElementalType = new byte[24];
		}
	};
}
