using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DatAdmin;

namespace Plugin.dbmodel
{
    public partial class DatabaseDiff
    {
        DbDiffAction m_actions;
        DatabaseStructure m_src;
        DatabaseStructure m_dst;
        AlterPlan m_plan;
        internal DbDiffOptions m_options;
        internal ISqlDialect m_dialect;
        Dictionary<string, AbstractObjectStructure> srcGroupIds = new Dictionary<string, AbstractObjectStructure>();
        Dictionary<string, AbstractObjectStructure> dstGroupIds = new Dictionary<string, AbstractObjectStructure>();

        HashSetEx<string> alteredObjects = new HashSetEx<string>();
        //Dictionary<string, IAbstractObjectStructure> AlteredSourceObjects = new Dictionary<string, IAbstractObjectStructure>();
        //Dictionary<string, IAbstractObjectStructure> AlteredTargetObjects = new Dictionary<string, IAbstractObjectStructure>();

        internal Dictionary<string, DbDiffAction> IdToAction = new Dictionary<string, DbDiffAction>();

        public event Action<DbDiffAction> ChangedAction;

        public DatabaseDiff(IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions options, ISqlDialect dialect)
        {
            m_dialect = dialect;
            m_src = new DatabaseStructure(src);
            m_dst = new DatabaseStructure(dst);
            if (m_src.Dialect != null && m_src.Dialect.DialectName != dialect.DialectName)
            {
                dialect.MigrateDatabase(m_src, dialect.GetDefaultMigrationProfile(), null);
            }
            m_actions = new DbDiffAction(this);
            //m_actions = new DiffActionDatabase(this, m_src, m_dst);
            m_options = options;
            RebuildGroupIdDictionary();
            if (m_src.GroupId != m_dst.GroupId) CreatePairing();
            CreateActions();
        }

        public IDatabaseStructure Source { get { return m_src; } }
        public IDatabaseStructure Target { get { return m_dst; } }
        public AlterPlan Plan { get { return m_plan; } }

        public DbDiffAction Actions { get { return m_actions; } }

        internal void AddAlteredObject(IAbstractObjectStructure obj)
        {
            if (obj.GroupId != null) alteredObjects.Add(obj.GroupId);
        }

        public bool IsAltered(IAbstractObjectStructure obj)
        {
            return alteredObjects.Contains(obj.GroupId);
        }

        private void RebuildGroupIdDictionary()
        {
            srcGroupIds.Clear();
            dstGroupIds.Clear();
            foreach (AbstractObjectStructure obj in m_src.GetAllObjects())
            {
                srcGroupIds[obj.GroupId] = obj;
            }
            foreach (AbstractObjectStructure obj in m_dst.GetAllObjects())
            {
                dstGroupIds[obj.GroupId] = obj;
            }
        }

        public void CallChangedAction(DbDiffAction action)
        {
            if (ChangedAction != null) ChangedAction(action);
        }
    }

    [AppObject(Name = "dbdiff", Title = "DB Diff")]
    public class DbDiffAppObject : AppObject
    {
        public DatabaseDiff DbDiff;
        public IDatabaseSource TargetDb;

        public override bool SupportSerialize
        {
            get { return false; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return DbModIcons.synchronize; }
        }

        public override string TypeTitle
        {
            get { return "dbdiff"; }
        }

        public override string TypeName
        {
            get { return "dbdiff"; }
        }

        public override ISqlDialect Dialect
        {
            get { return TargetDb.Dialect; }
        }

        public override string GetDatabaseName()
        {
            return TargetDb.DatabaseName;
        }

        public override IPhysicalConnectionFactory GetConnection()
        {
            return TargetDb.Connection.PhysicalFactory;
        }
    }

    [AppObjectSqlGenerator(Name = "db-synchronize",Title = "SYNCHRONIZE")]
    public class DbDiffSyncrhonize : AppObjectSqlGeneratorBase
    {
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is DbDiffAppObject;
        }

        public override void GenerateSql(AppObject appobj, ISqlDumper dmp, ISqlDialect dialect)
        {
            var ao = (DbDiffAppObject)appobj;
            var plan = ao.DbDiff.Actions.GetPlanForThis(ao.TargetDb, true);
            plan.CreateRunner().Run(dmp, ao.DbDiff.m_options);
        }
    }
}
