// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Game
{
	/// <summary>
	/// Packets exchanged between Client and Auth
	/// </summary>
	public class ClientPackets
	{
		public static readonly ClientPackets Instance = new ClientPackets();
		
		private delegate void PacketAction(GameClient client, PacketStream stream);

		private Dictionary<ushort, PacketAction> PacketsDb;

		public ClientPackets()
		{
			// Loads PacketsDb
			PacketsDb = new Dictionary<ushort, PacketAction>();

			#region Packets
			//PacketsDb.Add(0x07D5, 
			#endregion
		}


		/// <summary>
		/// Called whenever a packet is received from a game client
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		public void PacketReceived(GameClient client, PacketStream stream)
		{
			// Is it a known packet ID
			if (!PacketsDb.ContainsKey(stream.GetId()))
			{
				ConsoleUtils.ShowWarning("Unknown packet Id: {0}", stream.GetId());
				return;
			}

			// Calls this packet parsing function
			Task.Factory.StartNew(() => { PacketsDb[stream.GetId()].Invoke(client, stream); });
		}


		#region Client Packets
		
		#endregion

		#region Server Packets
		
		#endregion
	}
}
