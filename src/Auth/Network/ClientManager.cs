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
using Common;

namespace Auth.Network
{
	/// <summary>
	/// Handles the connection between Auth and Game Clients
	/// </summary>
	public class ClientManager
	{
		public static readonly ClientManager Instance = new ClientManager();

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
				ConsoleUtils.ShowError("At ClientManager.Start()");

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
		private void PacketReceived(GameClient gameClient, PacketStream packetStream)
		{
            ClientPackets.Instance.PacketReceived(gameClient, packetStream);
		}

		/// <summary>
		/// Sends a packet to a game server
		/// </summary>
		/// <param name="server"></param>
		/// <param name="packet"></param>
		public void Send(GameClient client, PacketStream packet)
		{
			byte[] data = packet.GetPacket().ToArray();

			ConsoleUtils.HexDump(data, "Sent to Client");

			client.NetData.ClSocket.BeginSend(
				client.NetData.OutCipher.DoCipher(ref data),
				0,
				data.Length,
				0,
				new AsyncCallback(SendCallback),
				client
			);
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

			GameClient gc = new GameClient(client);

			client.BeginReceive(
				gc.NetData.Buffer, 0, Globals.MaxBuffer, SocketFlags.None,
				new AsyncCallback(ReadCallback), gc
			);
		}

		/// <summary>
		/// Receives data and split them into packets
		/// </summary>
		/// <param name="ar"></param>
		private void ReadCallback(IAsyncResult ar)
		{
			GameClient gc = (GameClient)ar.AsyncState;

			try
			{
				int bytesRead = gc.NetData.ClSocket.EndReceive(ar);
				if (bytesRead > 0)
				{
					int curOffset = 0;
					int bytesToRead = 0;
					byte[] decode = gc.NetData.InCipher.DoCipher(ref gc.NetData.Buffer, bytesRead);

					do
					{

						if (gc.NetData.PacketSize == 0)
						{
							if (gc.NetData.Offset + bytesRead > 3)
							{
								bytesToRead = (4 - gc.NetData.Offset);
								gc.NetData.Data.Write(decode, curOffset, bytesToRead);
								curOffset += bytesToRead;
								gc.NetData.Offset = bytesToRead;
								gc.NetData.PacketSize = BitConverter.ToInt32(gc.NetData.Data.ReadBytes(4, 0, true), 0);
							}
							else
							{
								gc.NetData.Data.Write(decode, 0, bytesRead);
								gc.NetData.Offset += bytesRead;
								curOffset += bytesRead;
							}
						}
						else
						{
							int needBytes = gc.NetData.PacketSize - gc.NetData.Offset;

							// If there's enough bytes to complete this packet
							if (needBytes <= (bytesRead - curOffset))
							{
								gc.NetData.Data.Write(decode, curOffset, needBytes);
								curOffset += needBytes;
								// Packet is done, send to server to be parsed
								// and continue.
								PacketReceived(gc, gc.NetData.Data);
								// Do needed clean up to start a new packet
								gc.NetData.Data = new PacketStream();
								gc.NetData.PacketSize = 0;
								gc.NetData.Offset = 0;
							}
							else
							{
								bytesToRead = (bytesRead - curOffset);
								gc.NetData.Data.Write(decode, curOffset, bytesToRead);
								gc.NetData.Offset += bytesToRead;
								curOffset += bytesToRead;
							}
						}
					} while (bytesRead - 1 > curOffset);

					gc.NetData.ClSocket.BeginReceive(
						gc.NetData.Buffer, 0, Globals.MaxBuffer, SocketFlags.None,
						new AsyncCallback(ReadCallback), gc
					);

				}
				else
				{
					ConsoleUtils.ShowInfo("Client disconected.");
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
				GameClient gc = (GameClient)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = gc.NetData.ClSocket.EndSend(ar);
			}
			catch (Exception)
			{
				ConsoleUtils.ShowNotice("Failed to send packet to client.");
			}
		}
		#endregion
	}
}
