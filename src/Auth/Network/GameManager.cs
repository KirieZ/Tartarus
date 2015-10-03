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

namespace Auth.Network
{
	/// <summary>
	/// Handles the connection between Auth and Game Servers
	/// </summary>
	public class GameManager
	{
		public static readonly GameManager Instance = new GameManager();

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
				listener.Bind(new IPEndPoint(ip, Settings.GameServerPort));
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
			GamePackets.Instance.PacketReceived(gameServer, packetStream);
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

			server.NetData.ClSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), server);
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
				gs.NetData.Buffer, 0, Globals.MaxBuffer, SocketFlags.None,
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
				int bytesRead = gs.NetData.ClSocket.EndReceive(ar);
				if (bytesRead > 0)
				{
					int curOffset = 0;
					int bytesToRead = 0;

					do
					{
						if (gs.NetData.PacketSize == 0)
						{
							if (gs.NetData.Offset + bytesRead > 3)
							{
								bytesToRead = (4 - gs.NetData.Offset);
								gs.NetData.Data.Write(gs.NetData.Buffer, curOffset, bytesToRead);
								curOffset += bytesToRead;
								gs.NetData.Offset = bytesToRead;
								gs.NetData.PacketSize = BitConverter.ToInt32(gs.NetData.Data.ReadBytes(4, 0, true), 0);
							}
							else
							{
								gs.NetData.Data.Write(gs.NetData.Buffer, 0, bytesRead);
								gs.NetData.Offset += bytesRead;
								curOffset += bytesRead;
							}
						}
						else
						{
							int needBytes = gs.NetData.PacketSize - gs.NetData.Offset;

							// If there's enough bytes to complete this packet
							if (needBytes <= (bytesRead - curOffset))
							{
								gs.NetData.Data.Write(gs.NetData.Buffer, curOffset, needBytes);
								curOffset += needBytes;
								// Packet is done, send to server to be parsed
								// and continue.
								PacketReceived(gs, gs.NetData.Data);
								// Do needed clean up to start a new packet
								gs.NetData.Data = new PacketStream();
								gs.NetData.PacketSize = 0;
								gs.NetData.Offset = 0;
							}
							else
							{
								bytesToRead = (bytesRead - curOffset);
								gs.NetData.Data.Write(gs.NetData.Buffer, curOffset, bytesToRead);
								gs.NetData.Offset += bytesToRead;
								curOffset += bytesToRead;
							}
						}
					} while (bytesRead - 1 > curOffset);

					gs.NetData.ClSocket.BeginReceive(
						gs.NetData.Buffer,
						0,
						Globals.MaxBuffer,
						SocketFlags.None,
						new AsyncCallback(ReadCallback),
						gs
					);
				}
				else
				{
					Server.Instance.OnGameServerDisconnects(gs);
					return;
				}
			}
			catch (SocketException e)
			{
				// 10054 : Socket closed, not a error
				if (!(e.ErrorCode == 10054))
					ConsoleUtils.ShowError(e.Message);

				Server.Instance.OnGameServerDisconnects(gs);
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
				GameServer gs = (GameServer)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = gs.NetData.ClSocket.EndSend(ar);
			}
			catch (Exception)
			{
				ConsoleUtils.ShowNotice("Failed to send packet to game-server.");
			}
		}
		#endregion
	}
}
