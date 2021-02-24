using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [AppObject(Name = "jobcommand")]
    public class JobCommandAppObject : AppObject
    {
        [XmlElem]
        public string JobFile { get; set; }

        [XmlElem]
        public string CommandGroupId { get; set; }

        public JobCommand GetCommand()
        {
            var conn = new JobConnection(JobFile);
            return conn.GetCommand(CommandGroupId);
        }

        public override string TypeName
        {
            get { return "jobcommand"; }
        }

        public override string TypeTitle
        {
            get { return "s_command"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.command; }
        }
    }
}
