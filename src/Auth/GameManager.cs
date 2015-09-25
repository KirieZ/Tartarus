using Common;
// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth
{
	/// <summary>
	/// Handles the connection between Auth and Game Servers
	/// </summary>
	public class GameManager
	{
		public static readonly GameManager Instance = new GameManager();

		private CancellationToken _CancelToken;

		/// <summary>
		/// Starts the game-server listener
		/// </summary>
		/// <returns>true on success, false otherwise</returns>
		public bool Start()
		{
			Socket listener = new Socket(SocketType.Stream, ProtocolType.Tcp);

			try
			{
				IPAddress ip;
				if (!IPAddress.TryParse(Settings.ServerIP, out ip))
				{
					ConsoleUtils.ShowFatalError("Failed to parse Server IP ({0})", Settings.ServerIP);
					return false;
				}
				listener.Bind(new IPEndPoint(ip, Settings.Port));
				listener.Listen(100);
				listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
			}
			catch (Exception e)
			{
				ConsoleUtils.ShowError(e.Message);
				ConsoleUtils.ShowError("At GameManager.Start()");

				listener.Close();
				return false;
			}

			return true;
		}

		/// <summary>
		/// Called when an entire packet is received
		/// </summary>
		/// <param name="gs"></param>
		/// <param name="packetStream"></param>
		private void PacketReceived(GameServer gameServer, PacketStream packetStream)
		{
			ConsoleUtils.HexDump(packetStream.ToArray(), "Received from GameServer");
			// TODO : Packet Parser
		}

		/// <summary>
		/// Sends a packet to a game server
		/// </summary>
		/// <param name="server"></param>
		/// <param name="packet"></param>
		public void Send(GameServer server, PacketStream packet)
		{
			byte[] data = packet.GetPacket().ToArray();

			ConsoleUtils.HexDump(data, "Sent to GameServer");

			server.ClSocket.BeginSend(data, 0, data.Length,  SocketFlags.None, new AsyncCallback(SendCallback), server);
		}

		#region Internal
		/// <summary>
		/// Triggered when a client tries to connect
		/// </summary>
		/// <param name="ar"></param>
		private void AcceptCallback(IAsyncResult ar)
		{
			Socket listener = (Socket)ar.AsyncState;
			Socket client = listener.EndAccept(ar);

			// Starts to accept another connection
			listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

			GameServer gs = new GameServer(client);

			client.BeginReceive(
				gs.Buffer, 0, Globals.MaxBuffer, SocketFlags.None,
				new AsyncCallback(ReadCallback), gs
			);
		}

		/// <summary>
		/// Receives data and split them into packets
		/// </summary>
		/// <param name="ar"></param>
		private void ReadCallback(IAsyncResult ar)
		{
			GameServer gs = (GameServer)ar.AsyncState;

			try
			{
				int bytesRead = gs.ClSocket.EndReceive(ar);
				if (bytesRead > 0)
				{
					int curOffset = 0;
					int bytesToRead = 0;

					do
					{
						if (gs.PacketSize == 0)
						{
							if (gs.Offset + bytesRead > 3)
							{
								bytesToRead = (4 - gs.Offset);
								gs.Data.Write(gs.Buffer, curOffset, bytesToRead);
								curOffset += bytesToRead;
								gs.Offset = bytesToRead;
								gs.PacketSize = BitConverter.ToInt32(gs.Data.ReadBytes(4, 0, true), 0);
							}
							else
							{
								gs.Data.Write(gs.Buffer, 0, bytesRead);
								gs.Offset += bytesRead;
								curOffset += bytesRead;
							}
						}
						else
						{
							int needBytes = gs.PacketSize - gs.Offset;

							// If there's enough bytes to complete this packet
							if (needBytes <= (bytesRead - curOffset))
							{
								gs.Data.Write(gs.Buffer, curOffset, needBytes);
								curOffset += needBytes;
								// Packet is done, send to server to be parsed
								// and continue.
								PacketReceived(gs, gs.Data);
								// Do needed clean up to start a new packet
								gs.Data = new PacketStream();
								gs.PacketSize = 0;
								gs.Offset = 0;
							}
							else
							{
								bytesToRead = (bytesRead - curOffset);
								gs.Data.Write(gs.Buffer, curOffset, bytesToRead);
								gs.Offset += bytesToRead;
								curOffset += bytesToRead;
							}
						}
					} while (bytesRead - 1 > curOffset);

				}
				else
				{
					ConsoleUtils.ShowInfo("Server disconected.");
					return;
				}
			}
			catch (Exception e)
			{
				ConsoleUtils.ShowError(e.Message);
			}
		}

		/// <summary>
		/// Sends a packet to a game-server
		/// </summary>
		/// <param name="ar"></param>
		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.
				Socket handler = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = handler.EndSend(ar);
			}
			catch (Exception)
			{
				ConsoleUtils.ShowNotice("Failed to send packet to game-server.");
			}
		}
		#endregion
	}
}