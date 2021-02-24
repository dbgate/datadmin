using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Plugin.datasyn
{
    public class DataSynIcons
    {
        public static readonly Dictionary<string, Bitmap> IconTable = new Dictionary<string, Bitmap>(); 

        static Bitmap m_add = StdRes.add;
        static Bitmap m_cancel = StdRes.cancel;
        static Bitmap m_checkall = StdRes.checkall;
        static Bitmap m_checkall_no = StdRes.checkall_no;
        static Bitmap m_compare = StdRes.compare;
        static Bitmap m_down1 = StdRes.down1;
        static Bitmap m_favorite_add = StdRes.favorite_add;
        static Bitmap m_foreign_key = StdRes.foreign_key;
        static Bitmap m_help = StdRes.help;
        static Bitmap m_open = StdRes.open;
        static Bitmap m_primary_key = StdRes.primary_key;
        static Bitmap m_properties = StdRes.properties;
        static Bitmap m_refresh = StdRes.refresh;
        static Bitmap m_remove = StdRes.remove;
        static Bitmap m_report = StdRes.report;
        static Bitmap m_right2 = StdRes.right2;
        static Bitmap m_save = StdRes.save;
        static Bitmap m_saveas = StdRes.saveas;
        static Bitmap m_sql = StdRes.sql;
        static Bitmap m_sync = StdRes.sync;
        static Bitmap m_table = StdRes.table;
        static Bitmap m_up1 = StdRes.up1;
        static Bitmap m_view = StdRes.view;

        static DataSynIcons() {
            IconTable["add"] = m_add;
            IconTable["cancel"] = m_cancel;
            IconTable["checkall"] = m_checkall;
            IconTable["checkall_no"] = m_checkall_no;
            IconTable["compare"] = m_compare;
            IconTable["down1"] = m_down1;
            IconTable["favorite_add"] = m_favorite_add;
            IconTable["foreign_key"] = m_foreign_key;
            IconTable["help"] = m_help;
            IconTable["open"] = m_open;
            IconTable["primary_key"] = m_primary_key;
            IconTable["properties"] = m_properties;
            IconTable["refresh"] = m_refresh;
            IconTable["remove"] = m_remove;
            IconTable["report"] = m_report;
            IconTable["right2"] = m_right2;
            IconTable["save"] = m_save;
            IconTable["saveas"] = m_saveas;
            IconTable["sql"] = m_sql;
            IconTable["sync"] = m_sync;
            IconTable["table"] = m_table;
            IconTable["up1"] = m_up1;
            IconTable["view"] = m_view;
        }
        public static Bitmap add {get {return m_add;} }
        public static Bitmap cancel {get {return m_cancel;} }
        public static Bitmap checkall {get {return m_checkall;} }
        public static Bitmap checkall_no {get {return m_checkall_no;} }
        public static Bitmap compare {get {return m_compare;} }
        public static Bitmap down1 {get {return m_down1;} }
        public static Bitmap favorite_add {get {return m_favorite_add;} }
        public static Bitmap foreign_key {get {return m_foreign_key;} }
        public static Bitmap help {get {return m_help;} }
        public static Bitmap open {get {return m_open;} }
        public static Bitmap primary_key {get {return m_primary_key;} }
        public static Bitmap properties {get {return m_properties;} }
        public static Bitmap refresh {get {return m_refresh;} }
        public static Bitmap remove {get {return m_remove;} }
        public static Bitmap report {get {return m_report;} }
        public static Bitmap right2 {get {return m_right2;} }
        public static Bitmap save {get {return m_save;} }
        public static Bitmap saveas {get {return m_saveas;} }
        public static Bitmap sql {get {return m_sql;} }
        public static Bitmap sync {get {return m_sync;} }
        public static Bitmap table {get {return m_table;} }
        public static Bitmap up1 {get {return m_up1;} }
        public static Bitmap view {get {return m_view;} }
        public const string addName = "add";
        public const string cancelName = "cancel";
        public const string checkallName = "checkall";
        public const string checkall_noName = "checkall_no";
        public const string compareName = "compare";
        public const string down1Name = "down1";
        public const string favorite_addName = "favorite_add";
        public const string foreign_keyName = "foreign_key";
        public const string helpName = "help";
        public const string openName = "open";
        public const string primary_keyName = "primary_key";
        public const string propertiesName = "properties";
        public const string refreshName = "refresh";
        public const string removeName = "remove";
        public const string reportName = "report";
        public const string right2Name = "right2";
        public const string saveName = "save";
        public const string saveasName = "saveas";
        public const string sqlName = "sql";
        public const string syncName = "sync";
        public const string tableName = "table";
        public const string up1Name = "up1";
        public const string viewName = "view";
    }
}

