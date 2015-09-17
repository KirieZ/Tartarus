// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public static class Globals
	{
        /// <summary>
        /// Maximum amount of threads that can be running at one time
        /// </summary>
        public const int MaxThreads = 6;

		//===== Internal
		// Packet Header Length (Size + Id + 1 byte)
		public const int HeaderLength = 7;
		// Inter-Server Acceptor Key Max Size
		public const int AcceptorKeyLength = 20;
		// Maximum buffer size
		public const int MaxBuffer = 1024;

		//===== Game
		// The Epic version, used for source formulas
		public const int ServerEpic = 62;
		// The Client Epic version, used to send correct packets
		public const int ClientEpic = 62;
		
		//===== Keys
		// The key used to decrypt passwords sent by the client
		// this is defined at "static Globals"
		public static string DESKey = "";
		// The key used to encrypt and decrypt packets sent and
		// received by the server to a client.
		// this is defined at "stati Globals"
		public static string RC4Key = "";
		
		static Globals()
		{
			// Define the DESKey and RC4Key based on client version
			switch (ClientEpic)
			{
				case 62:
					DESKey = "MERONG";
					RC4Key = "}h79q~B%al;k'y $E";
					break;
			}
		}

		// DON'T TOUCH THINGS AFTER THIS LINE
		// UNLESS YOU KNOW WHAT YOU'RE DOING
		public const int MaxHavoc = 0;
		public const int MaxChaos = 0;
		public const int MaxStamina = 500000;
		public const int CellSize = 6;
		public const short MaxInventory = short.MaxValue;

		// Nulls
		public static object[] NullObjArray = new object[0];
		public static string[] NullStrArray = new string[0];
		public static int[] NullIntArray = new int[0];
		public static byte[] NullByteArray = new byte[0];

	}
}
