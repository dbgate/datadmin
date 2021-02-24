using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class SaveJobForm : FormEx
    {
        public SaveJobForm()
        {
            InitializeComponent();
            foreach (string file in Directory.GetFiles(Core.JobsDirectory, "*.djb", SearchOption.AllDirectories))
            {
                lbxJobs.Items.Add(IOTool.RelativePathTo(Core.JobsDirectory, file));
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbtCreateNewJob.Checked)
            {
                if (tbxJob.Text.IsEmpty())
                {
                    StdDialog.ShowError("s_please_specify_job_name");
                    return;
                }
            }
            if (rbtAppendToExistingJob.Checked)
            {
                if (lbxJobs.SelectedIndex < 0)
                {
                    StdDialog.ShowError("s_please_choose_job");
                    return;
                }
            }
            Usage["jobname"] = tbxJob.Text;
            Usage["addtofavorite"] = chbAddToFavorites.Checked ? "1" : "0";
            Usage["result"] = "ok";

            DialogResult = DialogResult.OK;
            Close();
        }

        private void rbtCreateNewJob_CheckedChanged(object sender, EventArgs e)
        {
            tbxJob.Enabled = rbtCreateNewJob.Checked;
            lbxJobs.Enabled = rbtAppendToExistingJob.Checked;
            chbAddToFavorites.Enabled = rbtCreateNewJob.Checked;
            addToFavoritesFrame1.Enabled = chbAddToFavorites.Enabled && chbAddToFavorites.Checked;
        }

        public static SaveJobResult Run(Func<Job> createJob)
        {
            if (!LicenseTool.FeatureAllowedMsg(JobsFeature.Test)) return null;
            //if (!Licenseto. Registration.TryCheckEdition(SoftwareEdition.Professional, "export to job")) return null;
            SaveJobForm win = new SaveJobForm();
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                if (win.rbtCreateNewJob.Checked)
                {
                    string fn = Path.Combine(Core.JobsDirectory, win.tbxJob.Text + ".djb");
                    if (File.Exists(fn))
                    {
                        if (!StdDialog.ReallyOverwriteFile(fn)) return null;
                    }
                    try
                    {
                        Job job = createJob();
                        job.SaveToFile(fn);
                        if (win.chbAddToFavorites.Checked)
                        {
                            if (String.IsNullOrEmpty(win.addToFavoritesFrame1.FavoriteName))
                            {
                                win.addToFavoritesFrame1.FavoriteName = Path.GetFileNameWithoutExtension(fn);
                            }
                            win.addToFavoritesFrame1.Favorite = new JobFavorite { JobFile = fn };
                            Favorites.AddLast(win.addToFavoritesFrame1.GetHolder());
                            Favorites.NotifyChanged();
                        }
                        //UsageStats.Usage("export_as_job", "jobname", job.ToString(), "addtofavorite", win.chbAddToFavorites.Checked ? "1" : "0");
                        return new SaveJobResult
                        {
                            Commands = new List<JobCommand>(job.Root.m_commands),
                            JobConn = new JobConnection(fn),
                        };
                    }
                    catch (Exception err)
                    {
                        Errors.Report(err);
                    }
                }
                if (win.rbtAppendToExistingJob.Checked)
                {
                    string fn = Path.Combine(Core.JobsDirectory, win.lbxJobs.Items[win.lbxJobs.SelectedIndex].ToString());
                    Job job = Job.LoadFromFile(fn);
                    Job job2 = createJob();
                    job.Root.m_commands.AddRange(job2.Root.m_commands);
                    job.SaveToFile(fn);
                    return new SaveJobResult
                    {
                        Commands = new List<JobCommand>(job2.Root.m_commands),
                        JobConn = new JobConnection(fn),
                    };
                }
            }
            return null;
        }

        private void chbAddToFavorites_CheckedChanged(object sender, EventArgs e)
        {
            addToFavoritesFrame1.Enabled = chbAddToFavorites.Checked;
        }
    }

    public class SaveJobResult
    {
        public List<JobCommand> Commands;
        public JobConnection JobConn;
    }
}
