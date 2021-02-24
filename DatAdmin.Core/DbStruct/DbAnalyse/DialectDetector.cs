using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public abstract class DialectDetectorBase : AddonBase, IDialectDetector
    {
        #region IDialectDetector Members

        public virtual ISqlDialect DetectDialect(DbConnection conn)
        {
            return null;
        }

        public virtual ISqlDialect DetectDialect(string displayName)
        {
            return null;
        }

        public virtual ISqlDialect ApproximateDialect
        {
            get { return null; }
        }

        #endregion

        public override AddonType AddonType
        {
            get { return DialectAutoDetectorAddonType.Instance; }
        }
    }

    public abstract class TypicalDialectDetectorBase : DialectDetectorBase
    {
        public override ISqlDialect DetectDialect(DbConnection conn)
        {
            var res = CreateDialect(conn);
            if (res == null) return null;
            res.ParseVersion(conn.ServerVersion);
            return res;
        }

        public abstract ISqlDialect CreateDialect(DbConnection conn);
    }

    public class FixedDialectDetector : TypicalDialectDetectorBase
    {
        ISqlDialect m_dialect;
        public FixedDialectDetector(ISqlDialect dialect)
        {
            m_dialect = dialect;
        }

        public override ISqlDialect CreateDialect(DbConnection conn)
        {
            return m_dialect.CloneDialect();
        }

        public override string ToString()
        {
            return m_dialect.ToString();
        }

        public override ISqlDialect ApproximateDialect
        {
            get { return m_dialect; }
        }
    }

    public class DialectAutoDetector : TypicalDialectDetectorBase
    {
        public override ISqlDialect CreateDialect(DbConnection conn)
        {
            foreach (var holder in DialectAutoDetectorAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var det = holder.InstanceModel as IDialectDetector;
                if (det == null) continue;
                var res = det.DetectDialect(conn);
                if (res != null) return res;
            }
            return new GenericDialect();
        }

        public override string ToString()
        {
            return "(autodetect)";
        }
    }

    public abstract class DialectAutoDetectorBase : TypicalDialectDetectorBase
    {
        public abstract ISqlDialect GetDialect();
        public abstract bool TestCommand(DbConnection conn);

        public override ISqlDialect CreateDialect(DbConnection conn)
        {
            try
            {
                if (!TestCommand(conn)) return null;
            }
            catch
            {
                return null;
            }
            return GetDialect();
        }

        public override ISqlDialect ApproximateDialect
        {
            get { return GetDialect(); }
        }
    }
}
