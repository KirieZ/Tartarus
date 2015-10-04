using Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Game;
using System.Net.Sockets;

namespace Game.Content
{
    [Flags]
    public enum JobDepth
    {
        Basic = 1,
        First = 2,
        Second = 4,
        Master = 8
    }

    public class JobDBEntry
    {
        public int MaxLevel { get; set; }
        public JobDepth JobDepth { get; set; }

        public float StrMult { get; set; }
        public float VitMult { get; set; }
        public float DexMult { get; set; }
        public float AgiMult { get; set; }
        public float IntMult { get; set; }
        public float WisMult { get; set; }
        public float LuckMult { get; set; }
    }

	public partial class Player
	{
		static int sqlConType = Settings.SqlEngine;
        static string sqlConString = "Server="+Settings.SqlGameIp+";Database="+Settings.SqlGameDatabase+";UID="+Settings.SqlGameUsername+";PWD="+Settings.SqlGamePassword+";Connection Timeout=5;";

		/// <summary>
        /// Table storing EXP-TNL (To-Next-Level) values
        /// </summary>
        public static long[] ExpTable;
        /// <summary>
        /// Table storing JP-TNJL (To-Next-JLevel) values for job_0
        /// </summary>
        public static int[] Jp0Table;
        /// <summary>
        /// Table storing JP-TNJL (To-Next-JLevel) values for job_1
        /// </summary>
        public static int[] Jp1Table;
        /// <summary>
        /// Table storing JP-TNJL (To-Next-JLevel) values for job_2
        /// </summary>
        public static int[] Jp2Table;
        /// <summary>
        /// Table storing JP-TNJL (To-Next-JLevel) values for job_3
        /// </summary>
        public static int[] Jp3Table;

        public static Dictionary<int, JobDBEntry> JobDB;

        static internal void LoadLvDB()
        {
        }

        static internal void LoadJobDB()
        {

        }

        /// <summary>
        /// Loads the Player related tables into memory
        /// </summary>
        public static void Start()
        {
        }


		// Network Data
		public NetworkData NetData { get; set; }

		// User Info
		public int AccountId { get; set; }
		public string UserId { get; set; }
		public byte Permission { get; set; }
		public ushort LastServerId { get; set; }

		public byte[] Key { get; set; }

		public Player(Socket socket)
		{
			this.NetData = new NetworkData(socket);
		}
    }
}