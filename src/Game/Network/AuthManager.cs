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

namespace Game.Network
{
	/// <summary>
	/// Holds an AuthServer data
	/// </summary>
	public class AuthServer
	{
		// Network Data
		public NetworkData NetData { get; set; }

		public AuthServer(Socket socket)
		{
			this.NetData = new NetworkData(socket);
		}
	}

	/// <summary>
	/// Handles the connection between Auth and Game Servers
	/// </summary>
	public class AuthManager
	{
		/// <summary>
		/// Holds the instance to auth manager
		/// </summary>
		public static readonly AuthManager Instance = new AuthManager();

		/// <summary>
		/// Holds the auth server
		/// </summary>
		private AuthServer Auth;

		/// <summary>
		/// Starts the auth-server connection
		/// </summary>
		/// <returns>true on success, false otherwise</returns>
		public bool Start()
		{
			Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

			try
			{
				// Parse string IP Address
				IPAddress ip;
				if (!IPAddress.TryParse(Settings.AuthServerIP, out ip))
				{
					ConsoleUtils.ShowFatalError("Failed to parse Server IP ({0})", Settings.ServerIP);
					return false;
				}
				// Connect
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
			AuthPackets.Instance.PacketReceived(server, packetStream);
		}

		/// <summary>
		/// Sends a packet to an Auth Server
		/// </summary>
		/// <param name="server"></param>
		/// <param name="data"></param>
		public void Send(byte[] data)
		{
			// Dump and send
			ConsoleUtils.HexDump(data, "Sent to AuthServer");
			Auth.NetData.ClSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
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

			// Initializes a new instance of Auth
			Auth = new AuthServer(socket);
			AuthPackets.Instance.Register();

			// Starts to receive data
			socket.BeginReceive(Auth.NetData.Buffer, 0, Globals.MaxBuffer, SocketFlags.None, new AsyncCallback(ReadCallback), null);
		}

		/// <summary>
		/// Receives data and split them into packets
		/// </summary>
		/// <param name="ar"></param>
		private void ReadCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieves the ammount of bytes received
				int bytesRead = Auth.NetData.ClSocket.EndReceive(ar);
				if (bytesRead > 0)
				{
					// The offset of the current buffer
					int curOffset = 0;
					// Bytes to use in the next write
					int bytesToRead = 0;

					do
					{ // While there's data to read

						if (Auth.NetData.PacketSize == 0)
						{ // If we don't have the packet size yet

							if (Auth.NetData.Offset + bytesRead > 3)
							{ // If we can retrieve the packet size with the received data
								// If yes, we read remaining bytes until we get the packet size
								bytesToRead = (4 - Auth.NetData.Offset);
								Auth.NetData.Data.Write(Auth.NetData.Buffer, curOffset, bytesToRead);
								curOffset += bytesToRead;
								Auth.NetData.Offset = bytesToRead;
								Auth.NetData.PacketSize = BitConverter.ToInt32(Auth.NetData.Data.ReadBytes(4, 0, true), 0);
							}
							else
							{
								// If not, we read everything.
								Auth.NetData.Data.Write(Auth.NetData.Buffer, 0, bytesRead);
								Auth.NetData.Offset += bytesRead;
								curOffset += bytesRead;
							}
						}
						else
						{ // If we have packet size
							// How many bytes we need to complete this packet
							int needBytes = Auth.NetData.PacketSize - Auth.NetData.Offset;

							// If there's enough bytes to complete this packet
							if (needBytes <= (bytesRead - curOffset))
							{
								Auth.NetData.Data.Write(Auth.NetData.Buffer, curOffset, needBytes);
								curOffset += needBytes;
								// Packet is done, send to server to be parsed
								// and continue.
								PacketReceived(Auth, Auth.NetData.Data);
								// Do needed clean up to start a new packet
								Auth.NetData.Data = new PacketStream();
								Auth.NetData.PacketSize = 0;
								Auth.NetData.Offset = 0;
							}
							else
							{
								bytesToRead = (bytesRead - curOffset);
								Auth.NetData.Data.Write(Auth.NetData.Buffer, curOffset, bytesToRead);
								Auth.NetData.Offset += bytesToRead;
								curOffset += bytesToRead;
							}
						}
					} while (bytesRead - 1 > curOffset);

					// Starts to receive more data
					Auth.NetData.ClSocket.BeginReceive(
						Auth.NetData.Buffer,
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
					Auth.NetData.ClSocket.Close();
					return;
				}
			}
			catch (SocketException e)
			{
				// 10054 : Socket closed, not an error
				if (!(e.ErrorCode == 10054))
					ConsoleUtils.ShowError(e.Message);

				ConsoleUtils.ShowWarning("Connection to Auth-Server lost.");
				Auth.NetData.ClSocket.Close();
			}
			catch (Exception e)
			{
				ConsoleUtils.ShowError(e.Message);
				ConsoleUtils.ShowWarning("Connection to Auth-Server lost.");
				Auth.NetData.ClSocket.Close();
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
				int bytesSent = Auth.NetData.ClSocket.EndSend(ar);
			}
			catch (Exception)
			{
				ConsoleUtils.ShowNotice("Failed to send packet to auth-server.");
			}
		}
		#endregion
	}
}
