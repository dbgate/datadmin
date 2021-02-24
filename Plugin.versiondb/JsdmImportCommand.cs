using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;

namespace Plugin.versiondb
{
    [CommandLineCommand(Name = "jsdmimport", Description = "Imports versioned database into JSDM")]
    public class JsdmImportCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        class Instance : CommandLineCommandInstanceBase
        {
            string m_login;

            [CommandLineParameter(Name = "login", Description = "Login to JSDM")]
            public string Login
            {
                get { return m_login; }
                set { m_login = value; }
            }

            string m_password;

            [CommandLineParameter(Name = "password", Description = "Password to JSDM")]
            public string Password
            {
                get { return m_password; }
                set { m_password = value; }
            }

            string m_model;

            [CommandLineParameter(Name = "model", Description = "Name of new model")]
            public string Model
            {
                get { return m_model; }
                set { m_model = value; }
            }

            string m_url;

            [CommandLineParameter(Name = "url", Description = "URL of JSDM WEB service")]
            public string Url
            {
                get { return m_url; }
                set { m_url = value; }
            }

            string m_vdb;

            [CommandLineParameter(Name = "vdb", Description = "VDB file name")]
            public string Vdb
            {
                get { return m_vdb; }
                set { m_vdb = value; }
            }

            public override void RunCommand()
            {
                if (!LicenseTool.FeatureAllowedMsg(VersionedDbFeature.Test))
                {
                    Logging.Error("Database Versioning edition required");
                    return;
                }

                var vdb = new VersionDb(Vdb);

                var api = new jsdm.Api();
                var create = new jsdm.CreateModelProps();
                api.Url = String.Format("{0}?login={1}&password={2}", Url, Login, Password);
                create.Name = Model;
                create.Dialect = vdb.Dialect.DialectName;
                create.Versioned = true;
                string model = api.CreateModel(create);
                for (int i = 0; i < vdb.Versions.Count; i++)
                {
                    var ver = vdb.Versions[i];
                    using (var sr = new StreamReader(ver.GetFile()))
                    {
                        Logging.Info("Uploading version " + ver.Name);
                        string content = sr.ReadToEnd();
                        if (i > 0) api.AddModelVersion(model, new Plugin.versiondb.jsdm.CreateVersionProps
                        {
                            Name = ver.Name,
                            Ordinal = i + 1
                        });
                        api.ImportDbStructure(model, i + 1, content, true);
                    }
                }
                Logging.Info("Created mode with key " + model);
            }
        }

    }
}
