using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using DatAdmin;

namespace Plugin.dbmodel
{
    public class DbDefTreeNode : Database_SourceConnectionTreeNode
    {
        public DbDefTreeNode(ITreeNode parent, IFileHandler fhandler)
            : base(new DbDefSource(fhandler.FileObject.DataDiskPath), parent, fhandler, null, true)
        {
        }

        //private string PropsFile { get { return GetPrivateSubFolder("props"); } }

        public override bool AllowExpand()
        {
            Async.SafeOpen(m_conn.Connection);
            return true;
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.dbdef; }
        }

        public override void DataRefresh()
        {
            Reload();
        }

        public override void BeforeDataRefreshChilds()
        {
            Reload();
        }

        private void Reload()
        {
            ((DbDefSource)m_conn).Reload();
        }

        [PopupMenu("s_model_properties", ImageName = CoreIcons.propertiesName, Weight = MenuWeights.PROPERTIES)]
        public void EditProperties()
        {
            DbDefSource conn = (DbDefSource)m_conn;
            DbDefProperties props = conn.LoadProps();
            if (EditPropertiesForm.Run(props, true))
            {
                conn.SaveProps(props);
            }
        }

        [PopupMenu("s_transformation", ImageName = CoreIcons.jobName, Weight = MenuWeights.SQLGEN)]
        public void TransformatinGroup() { }

        [PopupMenu("s_transformation/s_remove_schema")]
        public void RemoveSchemaNames()
        {
            ((DbDefSource)m_conn).RunNameTransformation(new SetSchemaNameTransformation(null));
        }

        [PopupMenu("s_transformation/s_lower_case")]
        public void SetLowerCase()
        {
            ((DbDefSource)m_conn).RunNameTransformation(new LowerCaseNameTransformation());
        }

        [PopupMenu("s_transformation/s_upper_case")]
        public void SetUpperCase()
        {
            ((DbDefSource)m_conn).RunNameTransformation(new UpperCaseNameTransformation());
        }

        [PopupMenu("s_transformation/s_normalize_constraint_names")]
        public void NormalizeConstraintNames()
        {
            ((DbDefSource)m_conn).RunNameTransformation(new NormalizeConstraintNamesNameTransformation());
        }

        [PopupMenu("s_transformation/s_cleanup")]
        public void CleanUpStructure()
        {
            ((DbDefSource)m_conn).RunTransformation(db => db.CleanUp());
        }

        //protected override void BeforeGenerateSql()
        //{
        //    base.BeforeGenerateSql();
        //    Async.SafeOpen(m_conn.Connection);
        //}
    }

    public class DbDefReadonlyTreeNode : Database_SourceTreeNode
    {
        public DbDefReadonlyTreeNode(ITreeNode parent, IPhysicalConnectionFactory fact, string name)
            : base(new DbDefSource(fact, DbDefSource.ReadOnly.Flag), parent, name, false)
        {
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.dbdef; }
        }

        public override void DataRefresh()
        {
            Reload();
        }

        public override void BeforeDataRefreshChilds()
        {
            Reload();
        }

        private void Reload()
        {
            ((DbDefSource)m_conn).Reload();
        }

        //protected override void BeforeGenerateSql()
        //{
        //    base.BeforeGenerateSql();
        //    Async.SafeOpen(m_conn.Connection);
        //}

        public override bool AutoConnect
        {
            get { return true; }
        }

        public override bool AllowExpand()
        {
            Async.SafeOpen(m_conn.Connection);
            return base.AllowExpand();
        }

        protected override bool ExplicitShowSqlGenerators()
        {
            return true;
        }
    }

    //[NodeFactory(Name = "dbdef")]
    //public class DbDefNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        string dbfile = file;
    //        if (IOTool.FileIsLink(file)) dbfile = IOTool.GetLinkContent(file);
    //        if (dbfile.ToLower().EndsWith(".ddf"))
    //        {
    //            try
    //            {
    //                return new DbDefTreeNode(parent, file, dbfile);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}
}
