using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;

namespace DatAdmin
{
    [SettingsPage(Name = "connection", Title = "s_connection", Targets = (SettingsTargets)(SettingsTargets.All - SettingsTargets.Database), ImageName = CoreIcons.connectName)]
    public class ConnectionSettings : SettingsPageBase
    {
        string m_onConnectSql;
        [DisplayName("s_on_connect_sql")]
        [SettingsKey("connection.on_connect")]
        [SyntaxEditorLanguage(CodeLanguage.Sql)]
        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        public string OnConnectSql
        {
            get { return m_onConnectSql; }
            set { m_onConnectSql = value; }
        }

        string m_onDisconnectSql;
        [DisplayName("s_on_disconnect_sql")]
        [SettingsKey("connection.on_disconnect")]
        [SyntaxEditorLanguage(CodeLanguage.Sql)]
        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        public string OnDisconnectSql
        {
            get { return m_onDisconnectSql; }
            set { m_onDisconnectSql = value; }
        }

        int m_commandTimeout = 3600;
        [DisplayName("s_command_timeout")]
        [Description("s_command_timeout_desc")]
        [SettingsKey("connection.command_timeout")]
        public int CommandTimeout
        {
            get { return m_commandTimeout; }
            set { m_commandTimeout = value; }
        }
    }

    public static class SettingsPageCollection_Connection
    {
        public static ConnectionSettings Connection(this SettingsPageCollection col)
        {
            return (ConnectionSettings)col.PageByName("connection");
        }
    }
}
