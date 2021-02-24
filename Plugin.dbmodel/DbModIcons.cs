using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Plugin.dbmodel
{
    public class DbModIcons
    {
        public static readonly Dictionary<string, Bitmap> IconTable = new Dictionary<string, Bitmap>(); 

        static Bitmap m_add = ModRes.add;
        static Bitmap m_big_loading_icon = ModRes.big_loading_icon;
        static Bitmap m_check = ModRes.check;
        static Bitmap m_checkall = ModRes.checkall;
        static Bitmap m_checkall_no = ModRes.checkall_no;
        static Bitmap m_equals = ModRes.equals;
        static Bitmap m_favorite = ModRes.favorite;
        static Bitmap m_favorite_add = ModRes.favorite_add;
        static Bitmap m_favorite_remove = ModRes.favorite_remove;
        static Bitmap m_find = ModRes.find;
        static Bitmap m_generate_sql = ModRes.generate_sql;
        static Bitmap m_left1 = ModRes.left1;
        static Bitmap m_pen = ModRes.pen;
        static Bitmap m_properties = ModRes.properties;
        static Bitmap m_query_execute = ModRes.query_execute;
        static Bitmap m_refresh = ModRes.refresh;
        static Bitmap m_remove = ModRes.remove;
        static Bitmap m_right1 = ModRes.right1;
        static Bitmap m_right2 = ModRes.right2;
        static Bitmap m_run = ModRes.run;
        static Bitmap m_save = ModRes.save;
        static Bitmap m_sql = ModRes.sql;
        static Bitmap m_swap = ModRes.swap;
        static Bitmap m_synchronize = ModRes.synchronize;
        static Bitmap m_trace = ModRes.trace;

        static DbModIcons() {
            IconTable["add"] = m_add;
            IconTable["big_loading_icon"] = m_big_loading_icon;
            IconTable["check"] = m_check;
            IconTable["checkall"] = m_checkall;
            IconTable["checkall_no"] = m_checkall_no;
            IconTable["equals"] = m_equals;
            IconTable["favorite"] = m_favorite;
            IconTable["favorite_add"] = m_favorite_add;
            IconTable["favorite_remove"] = m_favorite_remove;
            IconTable["find"] = m_find;
            IconTable["generate_sql"] = m_generate_sql;
            IconTable["left1"] = m_left1;
            IconTable["pen"] = m_pen;
            IconTable["properties"] = m_properties;
            IconTable["query_execute"] = m_query_execute;
            IconTable["refresh"] = m_refresh;
            IconTable["remove"] = m_remove;
            IconTable["right1"] = m_right1;
            IconTable["right2"] = m_right2;
            IconTable["run"] = m_run;
            IconTable["save"] = m_save;
            IconTable["sql"] = m_sql;
            IconTable["swap"] = m_swap;
            IconTable["synchronize"] = m_synchronize;
            IconTable["trace"] = m_trace;
        }
        public static Bitmap add {get {return m_add;} }
        public static Bitmap big_loading_icon {get {return m_big_loading_icon;} }
        public static Bitmap check {get {return m_check;} }
        public static Bitmap checkall {get {return m_checkall;} }
        public static Bitmap checkall_no {get {return m_checkall_no;} }
        public static Bitmap equals {get {return m_equals;} }
        public static Bitmap favorite {get {return m_favorite;} }
        public static Bitmap favorite_add {get {return m_favorite_add;} }
        public static Bitmap favorite_remove {get {return m_favorite_remove;} }
        public static Bitmap find {get {return m_find;} }
        public static Bitmap generate_sql {get {return m_generate_sql;} }
        public static Bitmap left1 {get {return m_left1;} }
        public static Bitmap pen {get {return m_pen;} }
        public static Bitmap properties {get {return m_properties;} }
        public static Bitmap query_execute {get {return m_query_execute;} }
        public static Bitmap refresh {get {return m_refresh;} }
        public static Bitmap remove {get {return m_remove;} }
        public static Bitmap right1 {get {return m_right1;} }
        public static Bitmap right2 {get {return m_right2;} }
        public static Bitmap run {get {return m_run;} }
        public static Bitmap save {get {return m_save;} }
        public static Bitmap sql {get {return m_sql;} }
        public static Bitmap swap {get {return m_swap;} }
        public static Bitmap synchronize {get {return m_synchronize;} }
        public static Bitmap trace {get {return m_trace;} }
        public const string addName = "add";
        public const string big_loading_iconName = "big_loading_icon";
        public const string checkName = "check";
        public const string checkallName = "checkall";
        public const string checkall_noName = "checkall_no";
        public const string equalsName = "equals";
        public const string favoriteName = "favorite";
        public const string favorite_addName = "favorite_add";
        public const string favorite_removeName = "favorite_remove";
        public const string findName = "find";
        public const string generate_sqlName = "generate_sql";
        public const string left1Name = "left1";
        public const string penName = "pen";
        public const string propertiesName = "properties";
        public const string query_executeName = "query_execute";
        public const string refreshName = "refresh";
        public const string removeName = "remove";
        public const string right1Name = "right1";
        public const string right2Name = "right2";
        public const string runName = "run";
        public const string saveName = "save";
        public const string sqlName = "sql";
        public const string swapName = "swap";
        public const string synchronizeName = "synchronize";
        public const string traceName = "trace";
    }
}

