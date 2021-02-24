using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class TunnellingDefaults
    {
        public string Engine(string engine)
        {
            return engine ?? "mysql";
        }

        public int Port(string engine, int port)
        {
            engine = Engine(engine);
            switch (engine)
            {
                case "mysql": return 3306;
                case "postgre": return 5432;
            }
            return 0;
        }

        public virtual string[] Engines
        {
            get { return new string[] { "mysql", "postgre" }; }
        }

        public ISqlDialect Dialect(string engine)
        {
            try
            {
                switch (Engine(engine))
                {
                    case "mysql":
                        return (ISqlDialect)DialectAddonType.Instance.FindHolder("mysql").InstanceModel;
                    case "postgre":
                        return (ISqlDialect)DialectAddonType.Instance.FindHolder("postgre").InstanceModel;
                }
            }
            catch
            {
                // generic dialect is returned
            }
            return GenericDialect.Instance;
        }
    }
}
