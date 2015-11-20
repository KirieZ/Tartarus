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
        //===== Network
        /// <summary>Packet Header Length (Size + Id + 1 byte)</summary>
        public const int HeaderLength = 7;
        /// <summary>Inter-Server Acceptor Key Max Size</summary>
        public const int AcceptorKeyLength = 20;
		/// <summary>Maximum size of a buffer</summary>
		public const int MaxBuffer = 1024;
        //===== Game
        /// <summary>The Epic version, used for source formulas</summary>
        public const int ServerEpic = 62;
        /// <summary>The Client Epic version, used to send correct packets</summary>
		public const int ClientEpic = 62;

        //===== Keys
        /// <summary>The key used to decrypt passwords sent by the client</summary>
        public static string DESKey = "";
        /// <summary>The key used to encrypt and decrypt packets exchanged between client and server</summary>
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


        /// <summary>Start stamina (the one that a new character has)</summary>
		public const int StartStamina = 500000;
        /// <summary>Start Stamina Recovery (the one that a new character has)</summary>
        public const int StartStaminaRec = 30;
        /// <summary>The size of a cell</summary>
		public const int CellSize = 6;
        /// <summary>Size of a region</summary>
        public const int RegionSize = 180;
        /// <summary>The maximum number of items in the inventory</summary>
		public const short MaxInventory = short.MaxValue;
        /// <summary>Maximum number of characters in an account</summary>
        public const int MaxCharacters = 6;

		// Nulls
		public static object[] NullObjArray = new object[0];
		public static string[] NullStrArray = new string[0];
		public static int[] NullIntArray = new int[0];
		public static byte[] NullByteArray = new byte[0];

        
    }
}
