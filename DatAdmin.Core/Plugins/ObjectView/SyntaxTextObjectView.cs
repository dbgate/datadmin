using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DatAdmin
{
    public interface ISyntaxTextWidget : IWidget
    {
        CodeLanguage ViewLanguage { get;}
        ISqlDialect GetDialect(AppObject appobj, ConnectionPack connpack);
        string CreateText(AppObject appobj, ConnectionPack connpack);
    }

    public abstract class SyntaxTextWidgetBase : WidgetBase, ISyntaxTextWidget
    {
        [Browsable(false)]
        public virtual CodeLanguage ViewLanguage
        {
            get { return CodeLanguage.Sql; }
        }

        protected override WidgetBaseFrame CreateControl()
        {
            return new SyntaxWidgetFrame(this);
        }

        public override Type GetControlType()
        {
            return typeof(SyntaxWidgetFrame);
        }

        public abstract string CreateText(AppObject appobj, ConnectionPack connpack);

        public virtual ISqlDialect GetDialect(AppObject appobj, ConnectionPack connpack)
        {
            var dbobj = appobj as DatabaseFieldsAppObject;
            if (dbobj == null) return GenericDialect.Instance;
            IDatabaseSource db = dbobj.GetDatabaseConnection(connpack);
            if (db != null && db.Dialect != null) return db.Dialect;
            IPhysicalConnection conn = connpack.GetConnection(appobj.GetConnection(), false);
            if (conn == null) return GenericDialect.Instance;
            return conn.Dialect ?? GenericDialect.Instance;
        }
    }
}
