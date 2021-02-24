using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mssql
{
    [Widget(Name = "mssql_backups", Title = "Native backups", Category = "MS SQL")]
    public class MsSqlBackupsWidget : GuiWidget
    {
        public override string DefaultPageTitle
        {
            get { return "s_backups"; }
        }

        protected override WidgetBaseFrame CreateControl()
        {
            return new MsSqlBackupsFrame(this);
        }

        public override Type GetControlType()
        {
            return typeof(MsSqlBackupsFrame);
        }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return CoreIcons.backup; }
        }
    }

    [Widget(Name = "mssql_table_sizes", Title = "Table sizes", Category = "MS SQL")]
    public class MsSqlTableSizesWidget : SqlScriptGridWidget
    {
        public MsSqlTableSizesWidget()
            : base(StdScripts.tableinfo, "s_table_sizes", CoreIcons.sum)
        {
        }
    }
}
