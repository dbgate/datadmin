using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;

namespace Plugin.versiondb
{
    [AppObjectSqlGenerator(Name = "upgrade-downgrade", Title = "UPGRADE/DOWNGRADE")]
    public class GenSqlUpgradeVersionDb : AppObjectSqlGeneratorBase, ICustomPropertyPage
    {
        internal VersionDb m_vdb;

        public string FromVersion { get; set; }
        public string ToVersion { get; set; }

        public override void GenerateSql(AppObject appobj, ISqlDumper dmp, ISqlDialect dialect)
        {
            var vdb = (VersionDbAppObject)appobj;

            var fromv = vdb.GetVdb().FindVersion(FromVersion);
            var tov = vdb.GetVdb().FindVersion(ToVersion);

            DatabaseStructure fromstruct = null;
            if (fromv != null) fromstruct = DatabaseStructure.Load(fromv.GetFile());
            DatabaseStructure tostruct = null;
            if (tov != null) tostruct = DatabaseStructure.Load(tov.GetFile());
            if (tostruct == null) return;

            DbDiffOptions opts = new DbDiffOptions();
            //if (prev != null) dmp.TargetDb = new DbDefSource(DatabaseStructure.Load(prev.GetFile()), DbDefSource.ReadOnly.Flag);
            opts.AllowRecreateTable = true;
            if (fromstruct == null) dmp.CreateDatabaseObjects(tostruct);
            else dmp.AlterDatabase(fromstruct, tostruct, opts, new Plugin.dbmodel.DbDefSource(fromstruct, Plugin.dbmodel.DbDefSource.ReadOnly.Flag), null);

            tov.SetVersionSql(dmp);
        }

        public override bool SuitableFor(AppObject appobj)
        {
            var ao = appobj as VersionDbAppObject;
            if (ao != null)
            {
                m_vdb = ao.GetVdb();
                return true;
            }
            return false;
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new VdbUpgradeScriptFrame(this);
        }

        #endregion
    }
}
