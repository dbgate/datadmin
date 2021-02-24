using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public static class TableSourceExtension
    {
        public static ITableStructure InvokeLoadStructure(this ITableSource table, TableStructureMembers members)
        {
            IPhysicalConnection conn = table.Connection;
            if (conn == null) return table.LoadTableStructure(members);
            IAsyncResult async = conn.BeginInvoke((Func<TableStructureMembers, ITableStructure>)table.LoadTableStructure, null, members);
            Async.WaitFor(async);
            return (ITableStructure)conn.EndInvoke(async);
        }

        public static ITableStructure CGetStructure(this ITableSource table, Action guiCallback)
        {
            return table.CGetStructure(PriorityLevel.Normal, false, guiCallback);
        }

        private static string CGetStructureKey(ITableSource table)
        {
            string key = String.Format("{0}#{1}#CGetStructure", table.Database.DatabaseName, table.FullName.ToString());
            return key;
        }

        public static ITableStructure CGetStructure(this ITableSource table, PriorityLevel priority, bool behaveAsStack, Action guiCallback)
        {
            IPhysicalConnection conn = table.Connection;
            string key = CGetStructureKey(table);
            return (ITableStructure)conn.CacheGet(priority, behaveAsStack, key, () => table.LoadTableStructure(TableStructureMembers.All), guiCallback);
        }

        public static ITableStructure CPeekStructure(this ITableSource table)
        {
            IPhysicalConnection conn = table.Connection;
            string key = CGetStructureKey(table);
            return (ITableStructure)conn.CachePeek(key);
        }

        public static bool CStructureAvailable(this ITableSource table)
        {
            string key = CGetStructureKey(table);
            IPhysicalConnection conn = table.Connection;
            return conn.CacheAvailable(key);
        }

        public static DataTable CGetListData(this ITableSource table, int maxrows, Action guiCallback)
        {
            string key = (table.Database.DatabaseName ?? "") + "#" + table.FullName.ToString() + "#CGetListData";
            return (DataTable)table.Connection.CacheGet(PriorityLevel.Low, true, key, () => DoGetListData(table.Connection, table.Database.DatabaseName, table.FullName, maxrows), guiCallback);
        }

        public static LookupInfo CGetLookupInfo_NoFill(this ITableSource table, string pkcolname, Action guiCallback)
        {
            string key = String.Format("{0}#{1}#CGetLookupInfo_NoFill", table.Database.DatabaseName, table.FullName);
            return (LookupInfo)table.Connection.CacheGet(key, () => new LookupInfo(table, pkcolname), guiCallback);
            //lock (table.Connection.CachedData)
            //{
            //    if (table.Connection.CachedData.ContainsKey(key)) return (LookupInfo)table.Connection.CachedData[key];
            //}
            //ITableStructure ts = table.CGetStructure(PriorityLevel.Low, true, guiCallback);
            //if (ts == null) return null;
            //LookupInfo res = new LookupInfo(ts);
            //lock (table.Connection.CachedData)
            //{
            //    table.Connection.CachedData[key] = res;
            //}
            //return res;
        }

        public static LookupInfo CGetLookupInfo(this ITableSource table, string pkcolname, IEnumerable<string> keys, Action guiCallback)
        {
            LookupInfo lookup = table.CGetLookupInfo_NoFill(pkcolname, guiCallback);
            if (lookup == null) return null;
            if (!lookup.CGetData(table, keys, guiCallback)) return null;
            return lookup;
        }

        private static DataTable DoGetListData(IPhysicalConnection conn, string dbname, NameWithSchema table, int maxrows)
        {
            conn.SystemConnection.SafeChangeDatabase(dbname);
            int count = Int32.Parse(conn.SystemConnection.ExecuteScalar("SELECT COUNT(*) FROM " + conn.Dialect.QuoteFullName(table)).ToString());
            if (count > maxrows) throw new DataToLargeException();
            DataTable tbl = conn.SystemConnection.LoadTableFromQuery("SELECT * FROM " + conn.Dialect.QuoteFullName(table), maxrows);
            return tbl;
        }

        public static void DropTable(this ITableSource table)
        {
            table.Database.DropObject(new TableStructure { FullName = table.FullName });
        }

        public static void RenameTable(this ITableSource table, string newname)
        {
            table.Database.RenameObject(new TableStructure { FullName = table.FullName }, newname);
        }

        public static void ChangeSchema(this ITableSource table, string newschema)
        {
            table.Database.ChangeObjectSchema(new TableStructure { FullName = table.FullName }, newschema);
        }
    }

    public class DataToLargeException : Exception { }
}
