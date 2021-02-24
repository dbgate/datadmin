using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JobCommandAttribute : RegisterAttribute
    {
    }

    public interface IJobRunEnv
    {
        object this[string name] { get; set; }
        JobReportEnvBase GetReportEnv(string command);
        void SetReportEnv(string command, JobReportEnvBase repenv);
    }

    [AddonType]
    public class JobCommandAddonType : AddonType
    {
        public override string Name
        {
            get { return "jobcommand"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(JobCommand); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(JobCommandAttribute); }
        }

        public static readonly JobCommandAddonType Instance = new JobCommandAddonType();
    }
}
