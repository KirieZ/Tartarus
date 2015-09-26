using Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Game;

namespace Game.Database
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
            List<long> expt = new List<long>();
            List<int> jp0t = new List<int>();
            List<int> jp1t = new List<int>();
            List<int> jp2t = new List<int>();
            List<int> jp3t = new List<int>();

            using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
            {
                using (DbConnection dbCon = dbManager.CreateConnection())
                {
                    using (DbCommand dbCmd = dbCon.CreateCommand())
                    {
                        dbCmd.CommandText = "SELECT * FROM LevelResource";

                        try
                        {
                            dbCon.Open();

                            using (DbDataReader reader = dbCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int level = (int)reader[0];

                                    long exp = (long)reader[1];
                                    int jp0 = (int)reader[2];
                                    int jp1 = (int)reader[3];
                                    int jp2 = (int)reader[4];
                                    int jp3 = (int)reader[5];

                                    if (exp > 0)
                                        expt.Add(exp);
                                    if (jp0 > 0)
                                        jp0t.Add(jp0);
                                    if (jp1 > 0)
                                        jp1t.Add(jp1);
                                    if (jp2 > 0)
                                        jp2t.Add(jp2);
                                    if (jp3 > 0)
                                        jp3t.Add(jp3);
                                }
                            }
                        }
                        catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); return; }
                        finally { dbCon.Close(); }
                    }
                }

                expt.Add(0);
                jp0t.Add(0);
                jp1t.Add(0);
                jp2t.Add(0);
                jp3t.Add(0);

                ExpTable = expt.ToArray();
                Jp0Table = jp0t.ToArray();
                Jp1Table = jp1t.ToArray();
                Jp2Table = jp2t.ToArray();
                Jp3Table = jp3t.ToArray();

                ConsoleUtils.ShowStatus("Player EXP Table Loaded.");
            }
        }

        static internal void LoadJobDB()
        {
            ConsoleUtils.ShowStatus("Loading Job Table...");

            JobDB = new Dictionary<int, JobDBEntry>();

            using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
            {
                //while (reader.Read())
                //{
                //    JobDBEntry job = new JobDBEntry();
                //    int jobId = (int)reader[0];
                //    job.JobDepth = (JobDepth)(byte)reader[1];
                //    job.StrMult = (float)reader[2];
                //    job.VitMult = (float)reader[3];
                //    job.DexMult = (float)reader[4];
                //    job.AgiMult = (float)reader[5];
                //    job.IntMult = (float)reader[6];
                //    job.WisMult = (float)reader[7];
                //    job.LuckMult = (float)reader[8];

                //    if (JobDB.ContainsKey(jobId))
                //    {
                //        ConsoleUtils.ShowWarning("Duplicated job ID {0} at dbo.JobResource", jobId);
                //    }
                //    else
                //    {
                //        JobDB.Add(jobId, job);
                //    }
                //}
            }
        }

        /// <summary>
        /// Loads the Player related tables into memory
        /// </summary>
        public static void Start()
        {
            LoadLvDB();
            //TODO : Implement LoadJobDB
            //LoadJobDB();
        }
    }
}