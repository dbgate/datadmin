using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    [Favorite(Name = "run_job", Title = "Run job", RequiredFeature = JobsFeature.Test)]
    public class JobFavorite : FavoriteBase
    {
        [XmlElem]
        public string JobFile { get; set; }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.job; }
        }

        public override void Open()
        {
            var job = Job.LoadFromFile(Path.Combine(Core.JobsDirectory, JobFile));
            job.CreateProcess(new Dictionary<string, string>()).Start();
        }

        public override string Description
        {
            get { return "s_call_job"; }
        }

        public override void DisplayProps(Action<string, string> display)
        {
            base.DisplayProps(display);
            display("s_job_file", JobFile);
        }
    }
}
