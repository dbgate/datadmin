using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.IO;

namespace DatAdmin
{
    public partial class JobReportFrame : ContentFrame
    {
        JobConnection m_jobconn;

        // assigned one of job or command
        Job m_job;
        JobCommand m_cmd;

        public JobReportFrame()
        {
            InitializeComponent();
        }

        public JobReportFrame(string file)
        {
            InitializeComponent();
            m_jobconn = new JobConnection(file);
            m_job = m_jobconn.GetJob();
            RefreshFactories();
            RefreshConfigs();
        }

        public void LoadCommand(string file, JobCommand cmd)
        {
            m_jobconn = new JobConnection(file);
            m_cmd = m_jobconn.GetCommand(cmd.GroupId);
            RefreshFactories();
            RefreshConfigs();
        }

        private void RefreshConfigs()
        {
            List<IJobReportConfiguration> configs;
            if (m_cmd != null) configs = m_cmd.ReportConfigs;
            else configs = m_job.GetAllReportConfigs();

            lbxUsedReports.Items.Clear();
            foreach (var cfg in configs)
            {
                lbxUsedReports.Items.Add(cfg);
            }
        }

        private void RefreshFactories()
        {
            List<IJobReportFactory> facts = new List<IJobReportFactory>();
            if (m_cmd != null) m_cmd.GetReportFactories(facts);
            else m_job.GetReportFactories(facts);

            lbxAvailableReports.Items.Clear();
            foreach (var fact in facts)
            {
                lbxAvailableReports.Items.Add(fact);
            }
        }

        private void btnAddReport_Click(object sender, EventArgs e)
        {
            if (lbxAvailableReports.SelectedItem == null) return;
            var fact = (IJobReportFactory)lbxAvailableReports.SelectedItem;
            var cfg = fact.CreateConfig();
            fact.RelatedCommand.ReportConfigs.Add(cfg);
            RefreshConfigs();
        }

        private void btnRemoveReport_Click(object sender, EventArgs e)
        {
            if (lbxUsedReports.SelectedItem == null) return;
            lbxUsedReports.Items.Remove(lbxUsedReports.SelectedItem);
        }

        private void lbxUsedReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyFrame1.SelectedObject = lbxUsedReports.SelectedItem;
        }

        public override bool SupportsSave
        {
            get { return true; }
        }

        public override bool Save()
        {
            if (m_cmd != null)
            {
                m_jobconn.SaveReports(m_cmd);
                m_cmd = m_jobconn.GetCommand(m_cmd.GroupId);
            }
            else
            {
                m_jobconn.SaveReports(m_job);
                m_job = m_jobconn.GetJob();
            }
            RefreshFactories();
            RefreshConfigs();
            return true;
        }

        public override Bitmap Image
        {
            get { return CoreIcons.report; }
        }

        public override string PageTitle
        {
            get { return m_jobconn.ShortName; }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            var cfg = propertyFrame1.SelectedObject as IJobReportConfiguration;
            cfg.FilePlace.CheckOutput();
        }
    }
}
