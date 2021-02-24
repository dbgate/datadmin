using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;

namespace DatAdmin
{
    public abstract class DatabaseWriterBase : AddonBase, IDatabaseWriter
    {
        #region IDatabaseWriter Members

        public virtual void WriteStructureBeforeData(IDatabaseStructure db) { }
        public virtual void WriteStructureAfterData(IDatabaseStructure db) { }
        public virtual void BeforeFillData() { }
        public virtual void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts) { }
        public virtual void AfterFillData() { }
        [Browsable(false)]
        public virtual DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AcceptStructure = true,
                };
            }
        }
        [Browsable(false)]
        public virtual bool ConfigurationNeeded { get { return false; } }
        public virtual void OpenConnection() { }
        public virtual void CloseConnection() { }
        public virtual void CheckConfiguration(IDatabaseSource source) { }
        [Browsable(false)]
        public IProgressInfo ProgressInfo { get; set; }
        [Browsable(false)]
        public virtual ISqlDialect Dialect { get { return null; } }
        public virtual IDatabaseStructure InvokeLoadStructure(DatabaseStructureMembers members, IProgressInfo progress) { return null; }
        public virtual void InitializeFromInput(IDatabaseSource input) { }
        public virtual bool DirectCopy(IDatabaseSource source) { return false; }
        public virtual void RunDirectCopy(IDatabaseSource source, DatabaseCopyOptions copyOpts)
        {
            throw new NotImplementedError("DAE-00077");
        }
        public virtual IDatabaseWriter GetRedirectedWriter() { return null; }
        public virtual void SetSourceInfo(DatabaseWriterSourceInfo info) { }
        public virtual void ProcessFailed() { }

        #endregion

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return DatabaseWriterAddonType.Instance; }
        }
    }
}
