using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.Common;
using System.Collections;

namespace DatAdmin
{
    public static class PhysicalConnectionExtension
    {
        //public static List<string> InvokeLoadColumnNames(this IPhysicalConnection conn, string dbname, NameWithSchema table)
        //{
        //    DatabaseStructureMembers dbmem = new DatabaseStructureMembers
        //    {
        //        TableFilter = new List<NameWithSchema>{table},
        //        TableMembers = TableStructureMembers.ColumnNames,
        //    };
        //    IDatabaseStructure dbs = conn.InvokeLoadStructure(dbname, dbmem);
        //    return new List<string>(from c in dbs.Tables[table].Columns select c.ColumnName);
        //}
        public static List<string> InvokeLoadSchemaNames(this IPhysicalConnection conn, string dbname)
        {
            return StructLoader.SchemaNames(dbmem => conn.InvokeLoadStructure(dbname, dbmem, null));
        }

        //public static List<NameWithSchema> InvokeLoadFullTableNames(this IPhysicalConnection conn, string dbname)
        //{
        //    DatabaseStructureMembers dbmem = new DatabaseStructureMembers { TableList = true };
        //    IDatabaseStructure dbs = conn.InvokeLoadStructure(dbname, dbmem);
        //    return dbs.GetTableNames();
        //}

        public static IDatabaseStructure InvokeLoadStructure(this IPhysicalConnection conn, string dbname, DatabaseStructureMembers members, IProgressInfo progress)
        {
            IAsyncResult async = conn.BeginInvoke(
                (Func<IDatabaseStructure>)delegate() { return conn.Dialect.AnalyseDatabase(conn, dbname, members, progress); },
                null);
            Async.WaitFor(async);
            return (IDatabaseStructure)conn.EndInvoke(async);
        }


        public static DXDriver GetDXDriver(this IPhysicalConnection conn, string dbname)
        {
            if (conn.SystemConnection == null) return null;
            if (!LicenseTool.FeatureAllowed(DxDriverFeature.Test)) return null;
            return (DXDriver)conn.Cache.Database(dbname).Get("dxdriver", () => new DXDriver(conn));
        }

        public static bool CacheAvailable(this IPhysicalConnection conn, string key)
        {
            var res = (CacheGetAction)conn.Cache.Get("ccget", key);
            return res != null && res.IsFinished();
        }

        public static object CachePeek(this IPhysicalConnection conn, string key)
        {
            var res = (CacheGetAction)conn.Cache.Get("ccget", key);
            if (res != null) return res.Result;
            return null;
        }

        public static object CacheGet(this IPhysicalConnection conn, string key, Func<object> getFunc, Action guiCallback)
        {
            return conn.CacheGet(PriorityLevel.Normal, false, key, getFunc, guiCallback);
        }

        public static object CacheGet(this IPhysicalConnection conn, PriorityLevel priority, bool behaveAsStack, string key, Func<object> getFunc, Action guiCallback)
        {
            var res = (CacheGetAction)conn.Cache.Get("ccget", key);
            if (res != null)
            {
                return res.GetReturnValue();
            }
            CacheGetAction act = new CacheGetAction { GuiCallback = guiCallback, GetFunc = getFunc, Conn = conn, Key = key };
            conn.BeginInvoke(priority, behaveAsStack, (Func<object>)act.Run, MainWindow.Instance.Invoker.CreateInvokeCallback(act.OnFinish));
            return null;
        }

        //public static void ClearCache(this IPhysicalConnection conn)
        //{
        //    lock (conn.CachedData) conn.CachedData.Clear();
        //}

        public static void SetOnOpenDatabase(this IPhysicalConnection conn, string dbname)
        {
            if (!String.IsNullOrEmpty(dbname)) conn.AfterOpen += ConnTools.ChangeDatabaseCallback(dbname);
        }

        public static void SetOnOpenDatabase(this IPhysicalConnection conn, ObjectPath objpath)
        {
            if (objpath != null) conn.SetOnOpenDatabase(objpath.DbName);
        }

        public static void ReconnectIfBroken(this IPhysicalConnection conn)
        {
            if (conn.IsBroken())
            {
                conn.Reconnect();
                // try 2 times...
                if (conn.IsBroken())
                {
                    conn.Reconnect();
                }
            }
        }

        public static void ReconnectIfBroken(this IPhysicalConnection conn, DbTransaction trans)
        {
            if (conn.IsBroken(trans))
            {
                conn.Reconnect();
                // try 2 times...
                if (conn.IsBroken())
                {
                    conn.Reconnect();
                }
            }
        }

        public static void InvokeReconnectIfBroken(this IPhysicalConnection conn)
        {
            conn.Invoke((Action)conn.ReconnectIfBroken);
        }

        public static void RunScript(this IPhysicalConnection conn, Action<ISqlDumper> script, DbTransaction trans, IProgressInfo progress)
        {
            ConnectionSqlOutputStream sqlo = new ConnectionSqlOutputStream(conn.SystemConnection, trans, conn.Dialect);
            ISqlDumper fmt = conn.Dialect.CreateDumper(sqlo, SqlFormatProperties.Default);
            fmt.ProgressInfo = progress;
            script(fmt);
        }

        public static void RunScript(this IPhysicalConnection conn, Action<ISqlDumper> script)
        {
            conn.RunScript(script, null, null);
        }

        public static void InvokeScript(this IPhysicalConnection conn, Action<ISqlDumper> script, string dbname)
        {
            conn.Invoke(
             (Action)delegate()
            {
                conn.SystemConnection.SafeChangeDatabase(dbname);
                conn.RunScript(script);
            });
        }

        public static SettingsPageCollection FindSettings(this IPhysicalConnection conn, string dbname)
        {
            if (conn.StoredConnection != null)
            {
                if (dbname != null && conn.StoredConnection.GetDatabaseSettings(dbname) != null) return conn.StoredConnection.GetDatabaseSettings(dbname);
                if (conn.StoredConnection.ConnectionSettings != null) return conn.StoredConnection.ConnectionSettings;
            }
            if (conn.Dialect != null)
            {
                if (GlobalSettings.DialectSettings.ContainsKey(conn.Dialect.DialectName)) return GlobalSettings.DialectSettings[conn.Dialect.DialectName];
            }
            return GlobalSettings.Pages;
        }

        public static ISqlDialect GetAnyDialect(this IPhysicalConnection conn)
        {
            if (conn == null) return GenericDialect.Instance;
            if (conn.Dialect != null) return conn.Dialect;
            return GenericDialect.Instance;
        }

        public static IDialectDataAdapter GetAnyDDA(this IPhysicalConnection conn)
        {
            return conn.GetAnyDialect().CreateDataAdapter();
        }

        public static void FillInfo(this IPhysicalConnection conn, IDictionary data)
        {
            if (conn == null) return;
            if (conn.Dialect != null) conn.Dialect.FillInfo(data);
            if (conn.SystemConnection != null) conn.SystemConnection.FillInfo(data);
        }

        public static bool IsFullAvailable(this IConnectionUsage conn)
        {
            return conn != null && conn.Connection.IsFullAvailable();
        }

        public static bool IsFullAvailable(this IPhysicalConnection conn)
        {
            return conn != null && conn.SystemConnection != null && conn.IsOpened;
        }

        public static string GetConnKey(this IPhysicalConnection conn)
        {
            if (conn == null) return "";
            if (conn.PhysicalFactory == null) return "";
            return conn.PhysicalFactory.GetConnectionKey();
        }
        //public static void EnsureValid(this IPhysicalConnection conn)
        //{
        //    if (conn.Dialect != null && conn.SystemConnection != null)
        //    {
        //        string ping = conn.Dialect.GeneratePing();
        //        try
        //        {
        //            conn.SystemConnection.ExecuteNonQuery(ping);
        //        }
        //        catch
        //        {
        //            try
        //            {
        //                conn.ReconnectIfBroken();
        //            }
        //        }
        //    }
        //}

        public static void SafeChangeConnection<T>(ref T m_conn, IPhysicalConnection value)
            where T : IPhysicalConnection
        {
            if (m_conn.GetConnKey() != value.GetConnKey()) throw new InternalError("DAE-00011 Invalid connection, connection key differs");
            m_conn = (T)value;
        }
    }

    internal class CacheGetAction
    {
        internal Action GuiCallback;
        internal Func<object> GetFunc;
        internal IPhysicalConnection Conn;
        internal object Result;
        internal Exception Error;
        internal string Key;

        internal object Run()
        {
            var res = (CacheGetAction)Conn.Cache.Get("ccget", Key);
            if (res != null) return res.GetReturnValue();
            return GetFunc();
        }

        internal void OnFinish(IAsyncResult async)
        {
            try
            {
                Result = async.StandaloneEndInvoke();
            }
            catch (Exception e)
            {
                Error = e;
            }
            Conn.Cache.Put("ccget", Key, this);
            if (GuiCallback != null) GuiCallback();
        }

        internal object GetReturnValue()
        {
            if (Error != null)
            {
                throw Error;
            }
            return Result;
        }

        internal bool IsFinished()
        {
            return Error != null || Result != null;
        }
    }
}
