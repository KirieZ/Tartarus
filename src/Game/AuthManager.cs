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

namespace Game
{
	/// <summary>
	/// Handles the connection between Auth and Game Servers
	/// </summary>
	public class AuthManager
	{
		public static readonly AuthManager Instance = new AuthManager();

		private AuthServer Auth;

		/// <summary>
		/// Starts the auth-server listener
		/// </summary>
		/// <returns>true on success, false otherwise</returns>
		public bool Start()
		{
			Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

			try
			{
				IPAddress ip;
				if (!IPAddress.TryParse(Settings.AuthServerIP, out ip))
				{
					ConsoleUtils.ShowFatalError("Failed to parse Server IP ({0})", Settings.ServerIP);
					return false;
				}
				socket.BeginConnect(new IPEndPoint(ip, Settings.AuthServerPort), new AsyncCallback(ConnectCallback), socket);
			}
			catch (Exception e)
			{
				ConsoleUtils.ShowError(e.Message);
				ConsoleUtils.ShowError("At AuthManager.Start()");

				socket.Close();
				return false;
			}

			return true;
		}

		/// <summary>
		/// Called when an entire packet is received
		/// </summary>
		/// <param name="gs"></param>
		/// <param name="packetStream"></param>
		private void PacketReceived(AuthServer server, PacketStream packetStream)
		{
			ConsoleUtils.HexDump(packetStream.ToArray(), "Received from AuthServer");
			AuthPackets.Instance.PacketReceived(server, packetStream);
		}

		/// <summary>
		/// Sends a packet to an Auth Server
		/// </summary>
		/// <param name="server"></param>
		/// <param name="packet"></param>
		public void Send(PacketStream packet)
		{
			byte[] data = packet.GetPacket().ToArray();

			ConsoleUtils.HexDump(data, "Sent to AuthServer");

			Auth.ClSocket.BeginSend(data, 0, data.Length,  SocketFlags.None, new AsyncCallback(SendCallback), null);
		}

		#region Internal

		/// <summary>
		/// Triggered when it connects to Auth
		/// </summary>
		/// <param name="ar"></param>
		private void ConnectCallback(IAsyncResult ar)
		{
			Socket socket = (Socket)ar.AsyncState;
			if (!socket.Connected)
			{
				ConsoleUtils.ShowWarning("Could not connect to Auth-Server. Check your settings and try again...");
				return;
			}
			socket.EndConnect(ar);

			Auth = new AuthServer(socket);
			AuthPackets.Instance.Register();

			socket.BeginReceive(Auth.Buffer, 0, Globals.MaxBuffer, SocketFlags.None, new AsyncCallback(ReadCallback), null);
		}

		/// <summary>
		/// Receives data and split them into packets
		/// </summary>
		/// <param name="ar"></param>
		private void ReadCallback(IAsyncResult ar)
		{
			try
			{
				int bytesRead = Auth.ClSocket.EndReceive(ar);
				if (bytesRead > 0)
				{
					int curOffset = 0;
					int bytesToRead = 0;

					do
					{
						if (Auth.PacketSize == 0)
						{
							if (Auth.Offset + bytesRead > 3)
							{
								bytesToRead = (4 - Auth.Offset);
								Auth.Data.Write(Auth.Buffer, curOffset, bytesToRead);
								curOffset += bytesToRead;
								Auth.Offset = bytesToRead;
								Auth.PacketSize = BitConverter.ToInt32(Auth.Data.ReadBytes(4, 0, true), 0);
							}
							else
							{
								Auth.Data.Write(Auth.Buffer, 0, bytesRead);
								Auth.Offset += bytesRead;
								curOffset += bytesRead;
							}
						}
						else
						{
							int needBytes = Auth.PacketSize - Auth.Offset;

							// If there's enough bytes to complete this packet
							if (needBytes <= (bytesRead - curOffset))
							{
								Auth.Data.Write(Auth.Buffer, curOffset, needBytes);
								curOffset += needBytes;
								// Packet is done, send to server to be parsed
								// and continue.
								PacketReceived(Auth, Auth.Data);
								// Do needed clean up to start a new packet
								Auth.Data = new PacketStream();
								Auth.PacketSize = 0;
								Auth.Offset = 0;
							}
							else
							{
								bytesToRead = (bytesRead - curOffset);
								Auth.Data.Write(Auth.Buffer, curOffset, bytesToRead);
								Auth.Offset += bytesToRead;
								curOffset += bytesToRead;
							}
						}
					} while (bytesRead - 1 > curOffset);

					Auth.ClSocket.BeginReceive(
						Auth.Buffer,
						0,
						Globals.MaxBuffer,
						SocketFlags.None,
						new AsyncCallback(ReadCallback),
						null
					);
				}
				else
				{
					ConsoleUtils.ShowWarning("Connection to Auth-Server lost.");
					Auth.ClSocket.Close();
					return;
				}
			}
			catch (SocketException e)
			{
				// 10054 : Socket closed, not a error
				if (!(e.ErrorCode == 10054))
					ConsoleUtils.ShowError(e.Message);

				ConsoleUtils.ShowWarning("Connection to Auth-Server lost.");
				Auth.ClSocket.Close();
			}
			catch (Exception e)
			{
				ConsoleUtils.ShowError(e.Message);
				ConsoleUtils.ShowWarning("Connection to Auth-Server lost.");
				Auth.ClSocket.Close();
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
				// Complete sending the data to the remote device.
				int bytesSent = Auth.ClSocket.EndSend(ar);
			}
			catch (Exception)
			{
				ConsoleUtils.ShowNotice("Failed to send packet to auth-server.");
			}
		}
		#endregion
	}
}
