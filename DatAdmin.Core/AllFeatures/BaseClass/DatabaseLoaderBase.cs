using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    public abstract class DatabaseLoaderBase : AddonBase, IDatabaseLoader
    {
        #region IDatabaseLoader Members

        [XmlElem]
        [Browsable(false)]
        public virtual string Filename { get; set; }

        [Browsable(false)]
        public IProgressInfo ProgressInfo { get; set; }

        public abstract void LoadDatabase(IDatabaseSource dst);

        [Browsable(false)]
        public virtual string Filter
        {
            get { return String.Format("SQL {0}|*.sql", Texts.Get("s_files")); }
        }

        public virtual void CheckConfiguration(IDatabaseSource dst) { }

        [Browsable(false)]
        public virtual bool IsSqlDumpLoader { get { return false; } }

        public virtual bool SuitableFor(IDatabaseSource dst)
        {
            return false;
        }

        public abstract string GetTitle();

        #endregion

        public override string ToString()
        {
            return System.IO.Path.GetFileName(Filename);
        }

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return DatabaseLoaderAddonType.Instance; }
        }
    }
}
