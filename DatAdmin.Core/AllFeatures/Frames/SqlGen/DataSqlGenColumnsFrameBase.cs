using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class DataSqlGenColumnsFrameBase : UserControl
    {
        public virtual void SetHintStructure(ITableStructure value, string[] selcolumns) { }

        public virtual string GroupBoxTitle
        {
            get { return null; }
            set { }
        }

        public event EventHandler SettingsChanged;

        protected void OnSettingsChanged()
        {
            if (SettingsChanged != null) SettingsChanged(this, EventArgs.Empty);
        }

        public virtual DataSqlGeneratorColumnSet GetColumnSet()
        {
            throw new NotImplementedError("DAE-00083");
        }

        public virtual void InitializeMode(DataSqlGeneratorColumnSet.ModeEnum value)
        {
            throw new NotImplementedError("DAE-00084");
        }
    }
}
