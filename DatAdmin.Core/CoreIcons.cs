using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public class CoreIcons
    {
        public static readonly Dictionary<string, Bitmap> IconTable = new Dictionary<string, Bitmap>(); 

        static Bitmap m_bigfolder = StdIcons.bigfolder;
        static Bitmap m_dbbackup = StdIcons.dbbackup;
        static Bitmap m_dbdef = StdIcons.dbdef;
        static Bitmap m_img_database = StdIcons.img_database;
        static Bitmap m_img_folder = StdIcons.img_folder;
        static Bitmap m_img_folder_expanded = StdIcons.img_folder_expanded;
        static Bitmap m_treenode = StdIcons.treenode;
        static Bitmap m_column = StdIcons.column;
        static Bitmap m_command32 = StdIcons.command32;
        static Bitmap m_database = StdIcons.database;
        static Bitmap m_database_disconnected = StdIcons.database_disconnected;
        static Bitmap m_dbserver = StdIcons.dbserver;
        static Bitmap m_dbserver_disconnected = StdIcons.dbserver_disconnected;
        static Bitmap m_foreign_key = StdIcons.foreign_key;
        static Bitmap m_primary_key = StdIcons.primary_key;
        static Bitmap m_storedproc = StdIcons.storedproc;
        static Bitmap m_table = StdIcons.table;
        static Bitmap m_view = StdIcons.view;
        static Bitmap m_open = StdIcons.open;
        static Bitmap m_save = StdIcons.save;
        static Bitmap m_add = StdIcons.add;
        static Bitmap m_remove = StdIcons.remove;
        static Bitmap m_hex = StdIcons.hex;
        static Bitmap m_picture = StdIcons.picture;
        static Bitmap m_text = StdIcons.text;
        static Bitmap m_about = StdIcons.about;
        static Bitmap m_find = StdIcons.find;
        static Bitmap m_query_cancel = StdIcons.query_cancel;
        static Bitmap m_query_execute = StdIcons.query_execute;
        static Bitmap m_query_explain = StdIcons.query_explain;
        static Bitmap m_trigger = StdIcons.trigger;
        static Bitmap m_unique = StdIcons.unique;
        static Bitmap m_user = StdIcons.user;
        static Bitmap m_paging = StdIcons.paging;
        static Bitmap m_filter = StdIcons.filter;
        static Bitmap m_close = StdIcons.close;
        static Bitmap m_closeall = StdIcons.closeall;
        static Bitmap m_next = StdIcons.next;
        static Bitmap m_previous = StdIcons.previous;
        static Bitmap m_down1 = StdIcons.down1;
        static Bitmap m_left1 = StdIcons.left1;
        static Bitmap m_right1 = StdIcons.right1;
        static Bitmap m_up1 = StdIcons.up1;
        static Bitmap m_command = StdIcons.command;
        static Bitmap m_objectview = StdIcons.objectview;
        static Bitmap m_objectview32 = StdIcons.objectview32;
        static Bitmap m_delete = StdIcons.delete;
        static Bitmap m_diagram = StdIcons.diagram;
        static Bitmap m_diagram32 = StdIcons.diagram32;
        static Bitmap m_detail = StdIcons.detail;
        static Bitmap m_datastore32 = StdIcons.datastore32;
        static Bitmap m_alone_window = StdIcons.alone_window;
        static Bitmap m_main_window = StdIcons.main_window;
        static Bitmap m_schema = StdIcons.schema;
        static Bitmap m_properties = StdIcons.properties;
        static Bitmap m_xml = StdIcons.xml;
        static Bitmap m_text2 = StdIcons.text2;
        static Bitmap m__goto = StdIcons._goto;
        static Bitmap m_question = StdIcons.question;
        static Bitmap m_system = StdIcons.system;
        static Bitmap m_saveas = StdIcons.saveas;
        static Bitmap m_save2 = StdIcons.save2;
        static Bitmap m_sql = StdIcons.sql;
        static Bitmap m_transaction = StdIcons.transaction;
        static Bitmap m_index = StdIcons.index;
        static Bitmap m_index32 = StdIcons.index32;
        static Bitmap m_dataarchive = StdIcons.dataarchive;
        static Bitmap m_refresh = StdIcons.refresh;
        static Bitmap m_connect = StdIcons.connect;
        static Bitmap m__new = StdIcons._new;
        static Bitmap m_browse = StdIcons.browse;
        static Bitmap m_browse32 = StdIcons.browse32;
        static Bitmap m_history = StdIcons.history;
        static Bitmap m_fulljoin = StdIcons.fulljoin;
        static Bitmap m_innerjoin = StdIcons.innerjoin;
        static Bitmap m_leftjoin = StdIcons.leftjoin;
        static Bitmap m_rightjoin = StdIcons.rightjoin;
        static Bitmap m_design = StdIcons.design;
        static Bitmap m_order_asc = StdIcons.order_asc;
        static Bitmap m_order_desc = StdIcons.order_desc;
        static Bitmap m_altertable = StdIcons.altertable;
        static Bitmap m_stop = StdIcons.stop;
        static Bitmap m_stop32 = StdIcons.stop32;
        static Bitmap m_busy01 = StdIcons.busy01;
        static Bitmap m_busy02 = StdIcons.busy02;
        static Bitmap m_busy03 = StdIcons.busy03;
        static Bitmap m_busy04 = StdIcons.busy04;
        static Bitmap m_busy05 = StdIcons.busy05;
        static Bitmap m_busy06 = StdIcons.busy06;
        static Bitmap m_busy07 = StdIcons.busy07;
        static Bitmap m_busy08 = StdIcons.busy08;
        static Bitmap m_summary = StdIcons.summary;
        static Bitmap m_reset = StdIcons.reset;
        static Bitmap m_check = StdIcons.check;
        static Bitmap m_unique2 = StdIcons.unique2;
        static Bitmap m_commit = StdIcons.commit;
        static Bitmap m_copy = StdIcons.copy;
        static Bitmap m_disconnect = StdIcons.disconnect;
        static Bitmap m_export = StdIcons.export;
        static Bitmap m_import = StdIcons.import;
        static Bitmap m_rollback = StdIcons.rollback;
        static Bitmap m_rename = StdIcons.rename;
        static Bitmap m_import_sql_dump = StdIcons.import_sql_dump;
        static Bitmap m_backup = StdIcons.backup;
        static Bitmap m_restore = StdIcons.restore;
        static Bitmap m_clock = StdIcons.clock;
        static Bitmap m_job = StdIcons.job;
        static Bitmap m_run = StdIcons.run;
        static Bitmap m_winexplore = StdIcons.winexplore;
        static Bitmap m_error_big = StdIcons.error_big;
        static Bitmap m_swap = StdIcons.swap;
        static Bitmap m_warning = StdIcons.warning;
        static Bitmap m_big_loading_icon = StdIcons.big_loading_icon;
        static Bitmap m_infobox = StdIcons.infobox;
        static Bitmap m_settings = StdIcons.settings;
        static Bitmap m_addrow = StdIcons.addrow;
        static Bitmap m_deleterow = StdIcons.deleterow;
        static Bitmap m_datastore = StdIcons.datastore;
        static Bitmap m_domain = StdIcons.domain;
        static Bitmap m_debug = StdIcons.debug;
        static Bitmap m_error = StdIcons.error;
        static Bitmap m_fatal = StdIcons.fatal;
        static Bitmap m_info = StdIcons.info;
        static Bitmap m_trace = StdIcons.trace;
        static Bitmap m_first = StdIcons.first;
        static Bitmap m_last = StdIcons.last;
        static Bitmap m_versiondb = StdIcons.versiondb;
        static Bitmap m_versiondb32 = StdIcons.versiondb32;
        static Bitmap m_deny48 = StdIcons.deny48;
        static Bitmap m_datadmin32 = StdIcons.datadmin32;
        static Bitmap m_feedback = StdIcons.feedback;
        static Bitmap m_data = StdIcons.data;
        static Bitmap m_log = StdIcons.log;
        static Bitmap m_dependency = StdIcons.dependency;
        static Bitmap m_paste = StdIcons.paste;
        static Bitmap m_duplicate = StdIcons.duplicate;
        static Bitmap m_foreignkey_10 = StdIcons.foreignkey_10;
        static Bitmap m_primarykey_10 = StdIcons.primarykey_10;
        static Bitmap m_sort_ascending = StdIcons.sort_ascending;
        static Bitmap m_sort_descending = StdIcons.sort_descending;
        static Bitmap m_windows = StdIcons.windows;
        static Bitmap m_sequence = StdIcons.sequence;
        static Bitmap m_variable = StdIcons.variable;
        static Bitmap m_cancel = StdIcons.cancel;
        static Bitmap m_ok = StdIcons.ok;
        static Bitmap m_anchor = StdIcons.anchor;
        static Bitmap m_anchor_gray = StdIcons.anchor_gray;
        static Bitmap m_perspective = StdIcons.perspective;
        static Bitmap m_export2 = StdIcons.export2;
        static Bitmap m_querydesign = StdIcons.querydesign;
        static Bitmap m_internet = StdIcons.internet;
        static Bitmap m_resize = StdIcons.resize;
        static Bitmap m_dropdown = StdIcons.dropdown;
        static Bitmap m_autoscroll = StdIcons.autoscroll;
        static Bitmap m_sum = StdIcons.sum;
        static Bitmap m_favorite = StdIcons.favorite;
        static Bitmap m_favorite_add = StdIcons.favorite_add;
        static Bitmap m_favorite_remove = StdIcons.favorite_remove;
        static Bitmap m_report = StdIcons.report;
        static Bitmap m_email = StdIcons.email;
        static Bitmap m_person = StdIcons.person;
        static Bitmap m_example = StdIcons.example;
        static Bitmap m_tunnel32 = StdIcons.tunnel32;
        static Bitmap m_variant = StdIcons.variant;
        static Bitmap m_dashboard = StdIcons.dashboard;
        static Bitmap m_toolbox = StdIcons.toolbox;
        static Bitmap m_calendar = StdIcons.calendar;
        static Bitmap m_busy_loop = StdIcons.busy_loop;
        static Bitmap m_advanced = StdIcons.advanced;
        static Bitmap m_checkall = StdIcons.checkall;
        static Bitmap m_unknownfile = StdIcons.unknownfile;
        static Bitmap m_references = StdIcons.references;
        static Bitmap m_table_add = StdIcons.table_add;
        static Bitmap m_table_data = StdIcons.table_data;
        static Bitmap m_table_delete = StdIcons.table_delete;
        static Bitmap m_table_edit = StdIcons.table_edit;
        static Bitmap m_save_all = StdIcons.save_all;
        static Bitmap m_function = StdIcons.function;
        static Bitmap m_generate_sql = StdIcons.generate_sql;
        static Bitmap m_pen = StdIcons.pen;
        static Bitmap m_procedure = StdIcons.procedure;
        static Bitmap m_deleted_file = StdIcons.deleted_file;
        static Bitmap m_recycle_bin = StdIcons.recycle_bin;
        static Bitmap m_undo = StdIcons.undo;
        static Bitmap m_autodetect = StdIcons.autodetect;
        static Bitmap m_delete_column = StdIcons.delete_column;
        static Bitmap m_insert_column = StdIcons.insert_column;
        static Bitmap m_windowlist = StdIcons.windowlist;
        static Bitmap m_zoom = StdIcons.zoom;
        static Bitmap m_more_down = StdIcons.more_down;
        static Bitmap m_tick_overlay = StdIcons.tick_overlay;
        static Bitmap m_less_up = StdIcons.less_up;
        static Bitmap m_delete_overlay = StdIcons.delete_overlay;
        static Bitmap m_checkall_no = StdIcons.checkall_no;
        static Bitmap m_primary_key_no = StdIcons.primary_key_no;
        static Bitmap m_table_data_no = StdIcons.table_data_no;
        static Bitmap m_equals = StdIcons.equals;
        static Bitmap m_facebook = StdIcons.facebook;
        static Bitmap m_facebook32 = StdIcons.facebook32;
        static Bitmap m_twitter = StdIcons.twitter;
        static Bitmap m_twitter32 = StdIcons.twitter32;
        static Bitmap m_refresh2 = StdIcons.refresh2;
        static Bitmap m_refresh3 = StdIcons.refresh3;
        static Bitmap m_star_blue = StdIcons.star_blue;
        static Bitmap m_chart = StdIcons.chart;
        static Bitmap m_backward = StdIcons.backward;
        static Bitmap m_fast_backward = StdIcons.fast_backward;
        static Bitmap m_fast_forward = StdIcons.fast_forward;
        static Bitmap m_forward = StdIcons.forward;
        static Bitmap m_skip_backward = StdIcons.skip_backward;
        static Bitmap m_skip_forward = StdIcons.skip_forward;
        static Bitmap m_compare = StdIcons.compare;
        static Bitmap m_hourglass = StdIcons.hourglass;
        static Bitmap m_source = StdIcons.source;
        static Bitmap m_target = StdIcons.target;

        static CoreIcons() {
            IconTable["bigfolder"] = m_bigfolder;
            IconTable["dbbackup"] = m_dbbackup;
            IconTable["dbdef"] = m_dbdef;
            IconTable["img_database"] = m_img_database;
            IconTable["img_folder"] = m_img_folder;
            IconTable["img_folder_expanded"] = m_img_folder_expanded;
            IconTable["treenode"] = m_treenode;
            IconTable["column"] = m_column;
            IconTable["command32"] = m_command32;
            IconTable["database"] = m_database;
            IconTable["database_disconnected"] = m_database_disconnected;
            IconTable["dbserver"] = m_dbserver;
            IconTable["dbserver_disconnected"] = m_dbserver_disconnected;
            IconTable["foreign_key"] = m_foreign_key;
            IconTable["primary_key"] = m_primary_key;
            IconTable["storedproc"] = m_storedproc;
            IconTable["table"] = m_table;
            IconTable["view"] = m_view;
            IconTable["open"] = m_open;
            IconTable["save"] = m_save;
            IconTable["add"] = m_add;
            IconTable["remove"] = m_remove;
            IconTable["hex"] = m_hex;
            IconTable["picture"] = m_picture;
            IconTable["text"] = m_text;
            IconTable["about"] = m_about;
            IconTable["find"] = m_find;
            IconTable["query_cancel"] = m_query_cancel;
            IconTable["query_execute"] = m_query_execute;
            IconTable["query_explain"] = m_query_explain;
            IconTable["trigger"] = m_trigger;
            IconTable["unique"] = m_unique;
            IconTable["user"] = m_user;
            IconTable["paging"] = m_paging;
            IconTable["filter"] = m_filter;
            IconTable["close"] = m_close;
            IconTable["closeall"] = m_closeall;
            IconTable["next"] = m_next;
            IconTable["previous"] = m_previous;
            IconTable["down1"] = m_down1;
            IconTable["left1"] = m_left1;
            IconTable["right1"] = m_right1;
            IconTable["up1"] = m_up1;
            IconTable["command"] = m_command;
            IconTable["objectview"] = m_objectview;
            IconTable["objectview32"] = m_objectview32;
            IconTable["delete"] = m_delete;
            IconTable["diagram"] = m_diagram;
            IconTable["diagram32"] = m_diagram32;
            IconTable["detail"] = m_detail;
            IconTable["datastore32"] = m_datastore32;
            IconTable["alone_window"] = m_alone_window;
            IconTable["main_window"] = m_main_window;
            IconTable["schema"] = m_schema;
            IconTable["properties"] = m_properties;
            IconTable["xml"] = m_xml;
            IconTable["text2"] = m_text2;
            IconTable["_goto"] = m__goto;
            IconTable["question"] = m_question;
            IconTable["system"] = m_system;
            IconTable["saveas"] = m_saveas;
            IconTable["save2"] = m_save2;
            IconTable["sql"] = m_sql;
            IconTable["transaction"] = m_transaction;
            IconTable["index"] = m_index;
            IconTable["index32"] = m_index32;
            IconTable["dataarchive"] = m_dataarchive;
            IconTable["refresh"] = m_refresh;
            IconTable["connect"] = m_connect;
            IconTable["_new"] = m__new;
            IconTable["browse"] = m_browse;
            IconTable["browse32"] = m_browse32;
            IconTable["history"] = m_history;
            IconTable["fulljoin"] = m_fulljoin;
            IconTable["innerjoin"] = m_innerjoin;
            IconTable["leftjoin"] = m_leftjoin;
            IconTable["rightjoin"] = m_rightjoin;
            IconTable["design"] = m_design;
            IconTable["order_asc"] = m_order_asc;
            IconTable["order_desc"] = m_order_desc;
            IconTable["altertable"] = m_altertable;
            IconTable["stop"] = m_stop;
            IconTable["stop32"] = m_stop32;
            IconTable["busy01"] = m_busy01;
            IconTable["busy02"] = m_busy02;
            IconTable["busy03"] = m_busy03;
            IconTable["busy04"] = m_busy04;
            IconTable["busy05"] = m_busy05;
            IconTable["busy06"] = m_busy06;
            IconTable["busy07"] = m_busy07;
            IconTable["busy08"] = m_busy08;
            IconTable["summary"] = m_summary;
            IconTable["reset"] = m_reset;
            IconTable["check"] = m_check;
            IconTable["unique2"] = m_unique2;
            IconTable["commit"] = m_commit;
            IconTable["copy"] = m_copy;
            IconTable["disconnect"] = m_disconnect;
            IconTable["export"] = m_export;
            IconTable["import"] = m_import;
            IconTable["rollback"] = m_rollback;
            IconTable["rename"] = m_rename;
            IconTable["import_sql_dump"] = m_import_sql_dump;
            IconTable["backup"] = m_backup;
            IconTable["restore"] = m_restore;
            IconTable["clock"] = m_clock;
            IconTable["job"] = m_job;
            IconTable["run"] = m_run;
            IconTable["winexplore"] = m_winexplore;
            IconTable["error_big"] = m_error_big;
            IconTable["swap"] = m_swap;
            IconTable["warning"] = m_warning;
            IconTable["big_loading_icon"] = m_big_loading_icon;
            IconTable["infobox"] = m_infobox;
            IconTable["settings"] = m_settings;
            IconTable["addrow"] = m_addrow;
            IconTable["deleterow"] = m_deleterow;
            IconTable["datastore"] = m_datastore;
            IconTable["domain"] = m_domain;
            IconTable["debug"] = m_debug;
            IconTable["error"] = m_error;
            IconTable["fatal"] = m_fatal;
            IconTable["info"] = m_info;
            IconTable["trace"] = m_trace;
            IconTable["first"] = m_first;
            IconTable["last"] = m_last;
            IconTable["versiondb"] = m_versiondb;
            IconTable["versiondb32"] = m_versiondb32;
            IconTable["deny48"] = m_deny48;
            IconTable["datadmin32"] = m_datadmin32;
            IconTable["feedback"] = m_feedback;
            IconTable["data"] = m_data;
            IconTable["log"] = m_log;
            IconTable["dependency"] = m_dependency;
            IconTable["paste"] = m_paste;
            IconTable["duplicate"] = m_duplicate;
            IconTable["foreignkey_10"] = m_foreignkey_10;
            IconTable["primarykey_10"] = m_primarykey_10;
            IconTable["sort_ascending"] = m_sort_ascending;
            IconTable["sort_descending"] = m_sort_descending;
            IconTable["windows"] = m_windows;
            IconTable["sequence"] = m_sequence;
            IconTable["variable"] = m_variable;
            IconTable["cancel"] = m_cancel;
            IconTable["ok"] = m_ok;
            IconTable["anchor"] = m_anchor;
            IconTable["anchor_gray"] = m_anchor_gray;
            IconTable["perspective"] = m_perspective;
            IconTable["export2"] = m_export2;
            IconTable["querydesign"] = m_querydesign;
            IconTable["internet"] = m_internet;
            IconTable["resize"] = m_resize;
            IconTable["dropdown"] = m_dropdown;
            IconTable["autoscroll"] = m_autoscroll;
            IconTable["sum"] = m_sum;
            IconTable["favorite"] = m_favorite;
            IconTable["favorite_add"] = m_favorite_add;
            IconTable["favorite_remove"] = m_favorite_remove;
            IconTable["report"] = m_report;
            IconTable["email"] = m_email;
            IconTable["person"] = m_person;
            IconTable["example"] = m_example;
            IconTable["tunnel32"] = m_tunnel32;
            IconTable["variant"] = m_variant;
            IconTable["dashboard"] = m_dashboard;
            IconTable["toolbox"] = m_toolbox;
            IconTable["calendar"] = m_calendar;
            IconTable["busy_loop"] = m_busy_loop;
            IconTable["advanced"] = m_advanced;
            IconTable["checkall"] = m_checkall;
            IconTable["unknownfile"] = m_unknownfile;
            IconTable["references"] = m_references;
            IconTable["table_add"] = m_table_add;
            IconTable["table_data"] = m_table_data;
            IconTable["table_delete"] = m_table_delete;
            IconTable["table_edit"] = m_table_edit;
            IconTable["save_all"] = m_save_all;
            IconTable["function"] = m_function;
            IconTable["generate_sql"] = m_generate_sql;
            IconTable["pen"] = m_pen;
            IconTable["procedure"] = m_procedure;
            IconTable["deleted_file"] = m_deleted_file;
            IconTable["recycle_bin"] = m_recycle_bin;
            IconTable["undo"] = m_undo;
            IconTable["autodetect"] = m_autodetect;
            IconTable["delete_column"] = m_delete_column;
            IconTable["insert_column"] = m_insert_column;
            IconTable["windowlist"] = m_windowlist;
            IconTable["zoom"] = m_zoom;
            IconTable["more_down"] = m_more_down;
            IconTable["tick_overlay"] = m_tick_overlay;
            IconTable["less_up"] = m_less_up;
            IconTable["delete_overlay"] = m_delete_overlay;
            IconTable["checkall_no"] = m_checkall_no;
            IconTable["primary_key_no"] = m_primary_key_no;
            IconTable["table_data_no"] = m_table_data_no;
            IconTable["equals"] = m_equals;
            IconTable["facebook"] = m_facebook;
            IconTable["facebook32"] = m_facebook32;
            IconTable["twitter"] = m_twitter;
            IconTable["twitter32"] = m_twitter32;
            IconTable["refresh2"] = m_refresh2;
            IconTable["refresh3"] = m_refresh3;
            IconTable["star_blue"] = m_star_blue;
            IconTable["chart"] = m_chart;
            IconTable["backward"] = m_backward;
            IconTable["fast_backward"] = m_fast_backward;
            IconTable["fast_forward"] = m_fast_forward;
            IconTable["forward"] = m_forward;
            IconTable["skip_backward"] = m_skip_backward;
            IconTable["skip_forward"] = m_skip_forward;
            IconTable["compare"] = m_compare;
            IconTable["hourglass"] = m_hourglass;
            IconTable["source"] = m_source;
            IconTable["target"] = m_target;
        }
        public static Bitmap bigfolder {get {return m_bigfolder;} }
        public static Bitmap dbbackup {get {return m_dbbackup;} }
        public static Bitmap dbdef {get {return m_dbdef;} }
        public static Bitmap img_database {get {return m_img_database;} }
        public static Bitmap img_folder {get {return m_img_folder;} }
        public static Bitmap img_folder_expanded {get {return m_img_folder_expanded;} }
        public static Bitmap treenode {get {return m_treenode;} }
        public static Bitmap column {get {return m_column;} }
        public static Bitmap command32 {get {return m_command32;} }
        public static Bitmap database {get {return m_database;} }
        public static Bitmap database_disconnected {get {return m_database_disconnected;} }
        public static Bitmap dbserver {get {return m_dbserver;} }
        public static Bitmap dbserver_disconnected {get {return m_dbserver_disconnected;} }
        public static Bitmap foreign_key {get {return m_foreign_key;} }
        public static Bitmap primary_key {get {return m_primary_key;} }
        public static Bitmap storedproc {get {return m_storedproc;} }
        public static Bitmap table {get {return m_table;} }
        public static Bitmap view {get {return m_view;} }
        public static Bitmap open {get {return m_open;} }
        public static Bitmap save {get {return m_save;} }
        public static Bitmap add {get {return m_add;} }
        public static Bitmap remove {get {return m_remove;} }
        public static Bitmap hex {get {return m_hex;} }
        public static Bitmap picture {get {return m_picture;} }
        public static Bitmap text {get {return m_text;} }
        public static Bitmap about {get {return m_about;} }
        public static Bitmap find {get {return m_find;} }
        public static Bitmap query_cancel {get {return m_query_cancel;} }
        public static Bitmap query_execute {get {return m_query_execute;} }
        public static Bitmap query_explain {get {return m_query_explain;} }
        public static Bitmap trigger {get {return m_trigger;} }
        public static Bitmap unique {get {return m_unique;} }
        public static Bitmap user {get {return m_user;} }
        public static Bitmap paging {get {return m_paging;} }
        public static Bitmap filter {get {return m_filter;} }
        public static Bitmap close {get {return m_close;} }
        public static Bitmap closeall {get {return m_closeall;} }
        public static Bitmap next {get {return m_next;} }
        public static Bitmap previous {get {return m_previous;} }
        public static Bitmap down1 {get {return m_down1;} }
        public static Bitmap left1 {get {return m_left1;} }
        public static Bitmap right1 {get {return m_right1;} }
        public static Bitmap up1 {get {return m_up1;} }
        public static Bitmap command {get {return m_command;} }
        public static Bitmap objectview {get {return m_objectview;} }
        public static Bitmap objectview32 {get {return m_objectview32;} }
        public static Bitmap delete {get {return m_delete;} }
        public static Bitmap diagram {get {return m_diagram;} }
        public static Bitmap diagram32 {get {return m_diagram32;} }
        public static Bitmap detail {get {return m_detail;} }
        public static Bitmap datastore32 {get {return m_datastore32;} }
        public static Bitmap alone_window {get {return m_alone_window;} }
        public static Bitmap main_window {get {return m_main_window;} }
        public static Bitmap schema {get {return m_schema;} }
        public static Bitmap properties {get {return m_properties;} }
        public static Bitmap xml {get {return m_xml;} }
        public static Bitmap text2 {get {return m_text2;} }
        public static Bitmap _goto {get {return m__goto;} }
        public static Bitmap question {get {return m_question;} }
        public static Bitmap system {get {return m_system;} }
        public static Bitmap saveas {get {return m_saveas;} }
        public static Bitmap save2 {get {return m_save2;} }
        public static Bitmap sql {get {return m_sql;} }
        public static Bitmap transaction {get {return m_transaction;} }
        public static Bitmap index {get {return m_index;} }
        public static Bitmap index32 {get {return m_index32;} }
        public static Bitmap dataarchive {get {return m_dataarchive;} }
        public static Bitmap refresh {get {return m_refresh;} }
        public static Bitmap connect {get {return m_connect;} }
        public static Bitmap _new {get {return m__new;} }
        public static Bitmap browse {get {return m_browse;} }
        public static Bitmap browse32 {get {return m_browse32;} }
        public static Bitmap history {get {return m_history;} }
        public static Bitmap fulljoin {get {return m_fulljoin;} }
        public static Bitmap innerjoin {get {return m_innerjoin;} }
        public static Bitmap leftjoin {get {return m_leftjoin;} }
        public static Bitmap rightjoin {get {return m_rightjoin;} }
        public static Bitmap design {get {return m_design;} }
        public static Bitmap order_asc {get {return m_order_asc;} }
        public static Bitmap order_desc {get {return m_order_desc;} }
        public static Bitmap altertable {get {return m_altertable;} }
        public static Bitmap stop {get {return m_stop;} }
        public static Bitmap stop32 {get {return m_stop32;} }
        public static Bitmap busy01 {get {return m_busy01;} }
        public static Bitmap busy02 {get {return m_busy02;} }
        public static Bitmap busy03 {get {return m_busy03;} }
        public static Bitmap busy04 {get {return m_busy04;} }
        public static Bitmap busy05 {get {return m_busy05;} }
        public static Bitmap busy06 {get {return m_busy06;} }
        public static Bitmap busy07 {get {return m_busy07;} }
        public static Bitmap busy08 {get {return m_busy08;} }
        public static Bitmap summary {get {return m_summary;} }
        public static Bitmap reset {get {return m_reset;} }
        public static Bitmap check {get {return m_check;} }
        public static Bitmap unique2 {get {return m_unique2;} }
        public static Bitmap commit {get {return m_commit;} }
        public static Bitmap copy {get {return m_copy;} }
        public static Bitmap disconnect {get {return m_disconnect;} }
        public static Bitmap export {get {return m_export;} }
        public static Bitmap import {get {return m_import;} }
        public static Bitmap rollback {get {return m_rollback;} }
        public static Bitmap rename {get {return m_rename;} }
        public static Bitmap import_sql_dump {get {return m_import_sql_dump;} }
        public static Bitmap backup {get {return m_backup;} }
        public static Bitmap restore {get {return m_restore;} }
        public static Bitmap clock {get {return m_clock;} }
        public static Bitmap job {get {return m_job;} }
        public static Bitmap run {get {return m_run;} }
        public static Bitmap winexplore {get {return m_winexplore;} }
        public static Bitmap error_big {get {return m_error_big;} }
        public static Bitmap swap {get {return m_swap;} }
        public static Bitmap warning {get {return m_warning;} }
        public static Bitmap big_loading_icon {get {return m_big_loading_icon;} }
        public static Bitmap infobox {get {return m_infobox;} }
        public static Bitmap settings {get {return m_settings;} }
        public static Bitmap addrow {get {return m_addrow;} }
        public static Bitmap deleterow {get {return m_deleterow;} }
        public static Bitmap datastore {get {return m_datastore;} }
        public static Bitmap domain {get {return m_domain;} }
        public static Bitmap debug {get {return m_debug;} }
        public static Bitmap error {get {return m_error;} }
        public static Bitmap fatal {get {return m_fatal;} }
        public static Bitmap info {get {return m_info;} }
        public static Bitmap trace {get {return m_trace;} }
        public static Bitmap first {get {return m_first;} }
        public static Bitmap last {get {return m_last;} }
        public static Bitmap versiondb {get {return m_versiondb;} }
        public static Bitmap versiondb32 {get {return m_versiondb32;} }
        public static Bitmap deny48 {get {return m_deny48;} }
        public static Bitmap datadmin32 {get {return m_datadmin32;} }
        public static Bitmap feedback {get {return m_feedback;} }
        public static Bitmap data {get {return m_data;} }
        public static Bitmap log {get {return m_log;} }
        public static Bitmap dependency {get {return m_dependency;} }
        public static Bitmap paste {get {return m_paste;} }
        public static Bitmap duplicate {get {return m_duplicate;} }
        public static Bitmap foreignkey_10 {get {return m_foreignkey_10;} }
        public static Bitmap primarykey_10 {get {return m_primarykey_10;} }
        public static Bitmap sort_ascending {get {return m_sort_ascending;} }
        public static Bitmap sort_descending {get {return m_sort_descending;} }
        public static Bitmap windows {get {return m_windows;} }
        public static Bitmap sequence {get {return m_sequence;} }
        public static Bitmap variable {get {return m_variable;} }
        public static Bitmap cancel {get {return m_cancel;} }
        public static Bitmap ok {get {return m_ok;} }
        public static Bitmap anchor {get {return m_anchor;} }
        public static Bitmap anchor_gray {get {return m_anchor_gray;} }
        public static Bitmap perspective {get {return m_perspective;} }
        public static Bitmap export2 {get {return m_export2;} }
        public static Bitmap querydesign {get {return m_querydesign;} }
        public static Bitmap internet {get {return m_internet;} }
        public static Bitmap resize {get {return m_resize;} }
        public static Bitmap dropdown {get {return m_dropdown;} }
        public static Bitmap autoscroll {get {return m_autoscroll;} }
        public static Bitmap sum {get {return m_sum;} }
        public static Bitmap favorite {get {return m_favorite;} }
        public static Bitmap favorite_add {get {return m_favorite_add;} }
        public static Bitmap favorite_remove {get {return m_favorite_remove;} }
        public static Bitmap report {get {return m_report;} }
        public static Bitmap email {get {return m_email;} }
        public static Bitmap person {get {return m_person;} }
        public static Bitmap example {get {return m_example;} }
        public static Bitmap tunnel32 {get {return m_tunnel32;} }
        public static Bitmap variant {get {return m_variant;} }
        public static Bitmap dashboard {get {return m_dashboard;} }
        public static Bitmap toolbox {get {return m_toolbox;} }
        public static Bitmap calendar {get {return m_calendar;} }
        public static Bitmap busy_loop {get {return m_busy_loop;} }
        public static Bitmap advanced {get {return m_advanced;} }
        public static Bitmap checkall {get {return m_checkall;} }
        public static Bitmap unknownfile {get {return m_unknownfile;} }
        public static Bitmap references {get {return m_references;} }
        public static Bitmap table_add {get {return m_table_add;} }
        public static Bitmap table_data {get {return m_table_data;} }
        public static Bitmap table_delete {get {return m_table_delete;} }
        public static Bitmap table_edit {get {return m_table_edit;} }
        public static Bitmap save_all {get {return m_save_all;} }
        public static Bitmap function {get {return m_function;} }
        public static Bitmap generate_sql {get {return m_generate_sql;} }
        public static Bitmap pen {get {return m_pen;} }
        public static Bitmap procedure {get {return m_procedure;} }
        public static Bitmap deleted_file {get {return m_deleted_file;} }
        public static Bitmap recycle_bin {get {return m_recycle_bin;} }
        public static Bitmap undo {get {return m_undo;} }
        public static Bitmap autodetect {get {return m_autodetect;} }
        public static Bitmap delete_column {get {return m_delete_column;} }
        public static Bitmap insert_column {get {return m_insert_column;} }
        public static Bitmap windowlist {get {return m_windowlist;} }
        public static Bitmap zoom {get {return m_zoom;} }
        public static Bitmap more_down {get {return m_more_down;} }
        public static Bitmap tick_overlay {get {return m_tick_overlay;} }
        public static Bitmap less_up {get {return m_less_up;} }
        public static Bitmap delete_overlay {get {return m_delete_overlay;} }
        public static Bitmap checkall_no {get {return m_checkall_no;} }
        public static Bitmap primary_key_no {get {return m_primary_key_no;} }
        public static Bitmap table_data_no {get {return m_table_data_no;} }
        public static Bitmap equals {get {return m_equals;} }
        public static Bitmap facebook {get {return m_facebook;} }
        public static Bitmap facebook32 {get {return m_facebook32;} }
        public static Bitmap twitter {get {return m_twitter;} }
        public static Bitmap twitter32 {get {return m_twitter32;} }
        public static Bitmap refresh2 {get {return m_refresh2;} }
        public static Bitmap refresh3 {get {return m_refresh3;} }
        public static Bitmap star_blue {get {return m_star_blue;} }
        public static Bitmap chart {get {return m_chart;} }
        public static Bitmap backward {get {return m_backward;} }
        public static Bitmap fast_backward {get {return m_fast_backward;} }
        public static Bitmap fast_forward {get {return m_fast_forward;} }
        public static Bitmap forward {get {return m_forward;} }
        public static Bitmap skip_backward {get {return m_skip_backward;} }
        public static Bitmap skip_forward {get {return m_skip_forward;} }
        public static Bitmap compare {get {return m_compare;} }
        public static Bitmap hourglass {get {return m_hourglass;} }
        public static Bitmap source {get {return m_source;} }
        public static Bitmap target {get {return m_target;} }
        public const string bigfolderName = "bigfolder";
        public const string dbbackupName = "dbbackup";
        public const string dbdefName = "dbdef";
        public const string img_databaseName = "img_database";
        public const string img_folderName = "img_folder";
        public const string img_folder_expandedName = "img_folder_expanded";
        public const string treenodeName = "treenode";
        public const string columnName = "column";
        public const string command32Name = "command32";
        public const string databaseName = "database";
        public const string database_disconnectedName = "database_disconnected";
        public const string dbserverName = "dbserver";
        public const string dbserver_disconnectedName = "dbserver_disconnected";
        public const string foreign_keyName = "foreign_key";
        public const string primary_keyName = "primary_key";
        public const string storedprocName = "storedproc";
        public const string tableName = "table";
        public const string viewName = "view";
        public const string openName = "open";
        public const string saveName = "save";
        public const string addName = "add";
        public const string removeName = "remove";
        public const string hexName = "hex";
        public const string pictureName = "picture";
        public const string textName = "text";
        public const string aboutName = "about";
        public const string findName = "find";
        public const string query_cancelName = "query_cancel";
        public const string query_executeName = "query_execute";
        public const string query_explainName = "query_explain";
        public const string triggerName = "trigger";
        public const string uniqueName = "unique";
        public const string userName = "user";
        public const string pagingName = "paging";
        public const string filterName = "filter";
        public const string closeName = "close";
        public const string closeallName = "closeall";
        public const string nextName = "next";
        public const string previousName = "previous";
        public const string down1Name = "down1";
        public const string left1Name = "left1";
        public const string right1Name = "right1";
        public const string up1Name = "up1";
        public const string commandName = "command";
        public const string objectviewName = "objectview";
        public const string objectview32Name = "objectview32";
        public const string deleteName = "delete";
        public const string diagramName = "diagram";
        public const string diagram32Name = "diagram32";
        public const string detailName = "detail";
        public const string datastore32Name = "datastore32";
        public const string alone_windowName = "alone_window";
        public const string main_windowName = "main_window";
        public const string schemaName = "schema";
        public const string propertiesName = "properties";
        public const string xmlName = "xml";
        public const string text2Name = "text2";
        public const string _gotoName = "_goto";
        public const string questionName = "question";
        public const string systemName = "system";
        public const string saveasName = "saveas";
        public const string save2Name = "save2";
        public const string sqlName = "sql";
        public const string transactionName = "transaction";
        public const string indexName = "index";
        public const string index32Name = "index32";
        public const string dataarchiveName = "dataarchive";
        public const string refreshName = "refresh";
        public const string connectName = "connect";
        public const string _newName = "_new";
        public const string browseName = "browse";
        public const string browse32Name = "browse32";
        public const string historyName = "history";
        public const string fulljoinName = "fulljoin";
        public const string innerjoinName = "innerjoin";
        public const string leftjoinName = "leftjoin";
        public const string rightjoinName = "rightjoin";
        public const string designName = "design";
        public const string order_ascName = "order_asc";
        public const string order_descName = "order_desc";
        public const string altertableName = "altertable";
        public const string stopName = "stop";
        public const string stop32Name = "stop32";
        public const string busy01Name = "busy01";
        public const string busy02Name = "busy02";
        public const string busy03Name = "busy03";
        public const string busy04Name = "busy04";
        public const string busy05Name = "busy05";
        public const string busy06Name = "busy06";
        public const string busy07Name = "busy07";
        public const string busy08Name = "busy08";
        public const string summaryName = "summary";
        public const string resetName = "reset";
        public const string checkName = "check";
        public const string unique2Name = "unique2";
        public const string commitName = "commit";
        public const string copyName = "copy";
        public const string disconnectName = "disconnect";
        public const string exportName = "export";
        public const string importName = "import";
        public const string rollbackName = "rollback";
        public const string renameName = "rename";
        public const string import_sql_dumpName = "import_sql_dump";
        public const string backupName = "backup";
        public const string restoreName = "restore";
        public const string clockName = "clock";
        public const string jobName = "job";
        public const string runName = "run";
        public const string winexploreName = "winexplore";
        public const string error_bigName = "error_big";
        public const string swapName = "swap";
        public const string warningName = "warning";
        public const string big_loading_iconName = "big_loading_icon";
        public const string infoboxName = "infobox";
        public const string settingsName = "settings";
        public const string addrowName = "addrow";
        public const string deleterowName = "deleterow";
        public const string datastoreName = "datastore";
        public const string domainName = "domain";
        public const string debugName = "debug";
        public const string errorName = "error";
        public const string fatalName = "fatal";
        public const string infoName = "info";
        public const string traceName = "trace";
        public const string firstName = "first";
        public const string lastName = "last";
        public const string versiondbName = "versiondb";
        public const string versiondb32Name = "versiondb32";
        public const string deny48Name = "deny48";
        public const string datadmin32Name = "datadmin32";
        public const string feedbackName = "feedback";
        public const string dataName = "data";
        public const string logName = "log";
        public const string dependencyName = "dependency";
        public const string pasteName = "paste";
        public const string duplicateName = "duplicate";
        public const string foreignkey_10Name = "foreignkey_10";
        public const string primarykey_10Name = "primarykey_10";
        public const string sort_ascendingName = "sort_ascending";
        public const string sort_descendingName = "sort_descending";
        public const string windowsName = "windows";
        public const string sequenceName = "sequence";
        public const string variableName = "variable";
        public const string cancelName = "cancel";
        public const string okName = "ok";
        public const string anchorName = "anchor";
        public const string anchor_grayName = "anchor_gray";
        public const string perspectiveName = "perspective";
        public const string export2Name = "export2";
        public const string querydesignName = "querydesign";
        public const string internetName = "internet";
        public const string resizeName = "resize";
        public const string dropdownName = "dropdown";
        public const string autoscrollName = "autoscroll";
        public const string sumName = "sum";
        public const string favoriteName = "favorite";
        public const string favorite_addName = "favorite_add";
        public const string favorite_removeName = "favorite_remove";
        public const string reportName = "report";
        public const string emailName = "email";
        public const string personName = "person";
        public const string exampleName = "example";
        public const string tunnel32Name = "tunnel32";
        public const string variantName = "variant";
        public const string dashboardName = "dashboard";
        public const string toolboxName = "toolbox";
        public const string calendarName = "calendar";
        public const string busy_loopName = "busy_loop";
        public const string advancedName = "advanced";
        public const string checkallName = "checkall";
        public const string unknownfileName = "unknownfile";
        public const string referencesName = "references";
        public const string table_addName = "table_add";
        public const string table_dataName = "table_data";
        public const string table_deleteName = "table_delete";
        public const string table_editName = "table_edit";
        public const string save_allName = "save_all";
        public const string functionName = "function";
        public const string generate_sqlName = "generate_sql";
        public const string penName = "pen";
        public const string procedureName = "procedure";
        public const string deleted_fileName = "deleted_file";
        public const string recycle_binName = "recycle_bin";
        public const string undoName = "undo";
        public const string autodetectName = "autodetect";
        public const string delete_columnName = "delete_column";
        public const string insert_columnName = "insert_column";
        public const string windowlistName = "windowlist";
        public const string zoomName = "zoom";
        public const string more_downName = "more_down";
        public const string tick_overlayName = "tick_overlay";
        public const string less_upName = "less_up";
        public const string delete_overlayName = "delete_overlay";
        public const string checkall_noName = "checkall_no";
        public const string primary_key_noName = "primary_key_no";
        public const string table_data_noName = "table_data_no";
        public const string equalsName = "equals";
        public const string facebookName = "facebook";
        public const string facebook32Name = "facebook32";
        public const string twitterName = "twitter";
        public const string twitter32Name = "twitter32";
        public const string refresh2Name = "refresh2";
        public const string refresh3Name = "refresh3";
        public const string star_blueName = "star_blue";
        public const string chartName = "chart";
        public const string backwardName = "backward";
        public const string fast_backwardName = "fast_backward";
        public const string fast_forwardName = "fast_forward";
        public const string forwardName = "forward";
        public const string skip_backwardName = "skip_backward";
        public const string skip_forwardName = "skip_forward";
        public const string compareName = "compare";
        public const string hourglassName = "hourglass";
        public const string sourceName = "source";
        public const string targetName = "target";
    }
}

