using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public interface IJobReportFactory
    {
        IJobReportConfiguration CreateConfig();
        JobCommand RelatedCommand { get; }
    }

    public class JobReportConfigurationAttribute : RegisterAttribute { }

    public interface IJobReportConfiguration : IAddonInstance
    {
        IJobReportConfiguration Clone();
        IJobReportProcessor CreateProcessor(JobCommand cmd);
        IFilePlace FilePlace { get; }
    }

    public interface IJobReportProcessor : IDisposable
    {
        void OnStart();
        void OnFinish();

    }

    [AddonType]
    public class JobReportConfigurationAddonType : AddonType
    {
        public override string Name
        {
            get { return "jobreportconfig"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IJobReportConfiguration); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(JobReportConfigurationAttribute); }
        }

        public static readonly JobReportConfigurationAddonType Instance = new JobReportConfigurationAddonType();
    }

    //public interface IJobReportInstance
    //{
    //    /// <summary>
    //    /// where data is stored
    //    /// </summary>
    //    string FileName { get; }

    //    IJobReportConfiguration Source { get; }
    //}

    //public class JobReportOutputAttribute : RegisterAttribute { }

    //public interface IJobReportOutput : IAddonInstance
    //{
    //    IJobReportOutput GetEditor();
    //    void Run(List<IJobReportInstance> reports);
    //}

    //[AddonType]
    //public class JobReportOutputAddonType : AddonType
    //{
    //    public override string Name
    //    {
    //        get { return "jobreportoutput"; }
    //    }

    //    public override Type InterfaceType
    //    {
    //        get { return typeof(IJobReportOutput); }
    //    }

    //    public override Type RegisterAttributeType
    //    {
    //        get { return typeof(JobReportOutputAttribute); }
    //    }

    //    public static readonly JobReportOutputAddonType Instance = new JobReportOutputAddonType();
    //}

    //public interface IJobReportOutputEditor
    //{
    //    Control Editor { get; }
    //    void SetAvailableReports(List<IJobReportConfiguration> usages);
    //}
}
