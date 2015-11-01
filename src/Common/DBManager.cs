//Written for Tartarus by iSmokeDrow
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Common
{
    public enum Database
    {
        Auth,
        Game,
        User
    }

    /// <summary>
    /// Provides interactibility with MySQL/MSSQL databases
    /// </summary>
	public class DBManager : IDisposable
	{
        private static Dictionary<int, string> AuthStatements;
        private static Dictionary<int, string> GameStatements;
        private static Dictionary<int, string> UserStatements;

        private static int ConType;

        private static string AuthConString;
        private static string GameConString;
        private static string UserConString;

        /// <summary>
        /// Indentifies the current connection type
        /// 0 = MSSQL
        /// 1 = MySQL
        /// </summary>
        readonly string dbConString;
        readonly Database targetDb;
        internal DbProviderFactory dbFactory;
        internal DbCommandBuilder dbBuilder;
        internal string dbParameterMarkerFormat;
		
		void dbProcess() { }

        internal static void SetStatements(Dictionary<int, string> auth, Dictionary<int, string> game, Dictionary<int, string> user)
        {
            AuthStatements = auth;
            GameStatements = game;
            UserStatements = user;
        }

        internal static void SetConnectionData(int engine, string authCon, string gameCon, string userCon)
        {
            ConType = engine;
            AuthConString = authCon;
            GameConString = gameCon;
            UserConString = userCon;
        }

        public DBManager(int conType, string conString)
        {
            
        }

        public DBManager(Database db)
        {
            switch (ConType)
            {
                case 0:
                    dbFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    break;

                case 1:
                    dbFactory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Database type isn't within range.");
            }

            switch (db)
            {
                case Database.Auth:
                    dbConString = AuthConString;
                    break;
                case Database.Game:
                    dbConString = GameConString;
                    break;
                case Database.User:
                    dbConString = UserConString;
                    break;
                default:
                    ConsoleUtils.ShowError("Invalid database type.");
                    break;
            }

            targetDb = db;

            if (dbFactory != null)
            {
                using (DbConnection dbCon = dbFactory.CreateConnection())
                {
                    dbCon.ConnectionString = dbConString;

                    dbBuilder = dbFactory.CreateCommandBuilder();

                    try
                    {
                        dbCon.Open();
                        using (DataTable tbl = dbCon.GetSchema(DbMetaDataCollectionNames.DataSourceInformation))
                        {
                            dbParameterMarkerFormat = tbl.Rows[0][DbMetaDataColumnNames.ParameterMarkerFormat] as string;
                        }
                        dbCon.Close();

                        if (!string.IsNullOrEmpty(dbParameterMarkerFormat)) { dbParameterMarkerFormat = "{0}"; }

                        ConsoleUtils.ShowDebug("DBManager Instance Initialized.");
                    }
                    catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
                }
            }
        }

        public DbCommand CreateCommand(string text)
        {
            DbConnection _dbCon = dbFactory.CreateConnection();
            _dbCon.ConnectionString = dbConString;
            DbCommand _dbCmd = dbFactory.CreateCommand();
            _dbCmd.Connection = _dbCon;
            _dbCmd.CommandText = text;
            return _dbCmd;
        }

        public DbCommand CreateCommand(int idx, int type)
        {
            DbConnection _dbCon = dbFactory.CreateConnection();
            _dbCon.ConnectionString = dbConString;
            DbCommand _dbCmd = dbFactory.CreateCommand();
            _dbCmd.Connection = _dbCon;
            try
            {
                switch (targetDb)
                {
                    case Database.Auth:
                        _dbCmd.CommandText = AuthStatements[idx];
                        break;
                    case Database.Game:
                        _dbCmd.CommandText = GameStatements[idx];
                        break;
                    case Database.User:
                        _dbCmd.CommandText = UserStatements[idx];
                        break;
                    default:
                        ConsoleUtils.ShowError("Invalid statement type.");
                        break;
                }
            }
            catch (Exception ex) { ConsoleUtils.ShowSQL(ex.Message); }
            return _dbCmd;
        }

        public DbParameter CreateInParameter(DbCommand cmd, string name, DbType type, object value)
        {
            DbParameter dbParam = dbFactory.CreateParameter();
            dbParam.ParameterName = (string)typeof(DbCommandBuilder).InvokeMember("GetParameterName",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.InvokeMethod |
                System.Reflection.BindingFlags.NonPublic, null, dbBuilder, new object[] {
                name });
            dbParam.DbType = type;
            dbParam.Direction = ParameterDirection.Input;
            dbParam.Value = value;
            cmd.Parameters.Add(dbParam);
            return dbParam;
        }

        #region Garbage Collection (GC)

        /// <summary>
		/// Free this object from memory
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); // Garbage  Collector doesn't need to finalize this
		}

		/// <summary>
		/// Deconstructor (Finalize)
		/// </summary>
		~DBManager()
		{
			Dispose(false);
		}

		protected void Dispose(bool disposing)
		{
            dbFactory = null;
            dbBuilder = null;
            dbParameterMarkerFormat = null;
        }

        #endregion
    }
}