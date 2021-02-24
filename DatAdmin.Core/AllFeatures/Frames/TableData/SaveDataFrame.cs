using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SaveDataFrame : UserControl
    {
        SaveDataProgress m_progress;
        bool m_pendingChanges;

        public SaveDataProgress Progress
        {
            get { return m_progress; }
            set
            {
                if (m_progress != null) m_progress.Changed -= new EventHandler(m_progress_Changed);
                m_progress = value;
                if (m_progress != null) m_progress.Changed += new EventHandler(m_progress_Changed);
                ShowData();
            }
        }

        private void ShowData()
        {
            var p = m_progress;
            if (p == null) p = new SaveDataProgress();
            labFinishedDeleted.Text = p.FinishedDeleted.ToString();
            labFinishedInserted.Text = p.FinishedInserted.ToString();
            labFinishedUpdatedRows.Text = p.FinishedUpdatedRows.ToString();
            labFinishedUpdatedFields.Text = p.FinishedUpdatedFields.ToString();
            labAllDeleted.Text = p.AllDeleted.ToString();
            labAllInserted.Text = p.AllInserted.ToString();
            labAllUpdatedRows.Text = p.AllUpdatedRows.ToString();
            labAllUpdatedFields.Text = p.AllUpdatedFields.ToString();
            progressBar1.Maximum = p.AllDeleted + p.AllInserted + p.AllUpdatedRows;
            progressBar1.Value = p.FinishedDeleted + p.FinishedInserted + p.FinishedUpdatedRows;
            btnCancel.Enabled = !p.IsFinished;
            
            m_pendingChanges = false;
        }

        void m_progress_Changed(object sender, EventArgs e)
        {
            m_pendingChanges = true;
        }

        public SaveDataFrame()
        {
            InitializeComponent();
            ShowData();
            this.Disposed += new EventHandler(SaveDataFrame_Disposed);
        }

        void SaveDataFrame_Disposed(object sender, EventArgs e)
        {
            m_progress = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_pendingChanges) ShowData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (m_progress != null) m_progress.IsCanceled = true;
        }
    }

    public class SaveDataProgress : ISaveDataProgress
    {
        public int AllInserted, AllUpdatedFields, AllDeleted, AllUpdatedRows;
        public int FinishedInserted, FinishedUpdatedFields, FinishedDeleted, FinishedUpdatedRows;

        public event EventHandler Changed;

        #region ISaveDataProgress Members

        public void IncrementCount(SaveProgressMeasure measure, int count)
        {
            switch (measure)
            {
                case SaveProgressMeasure.InsertedRows:
                    AllInserted += count;
                    break;
                case SaveProgressMeasure.DeletedRows:
                    AllDeleted += count;
                    break;
                case SaveProgressMeasure.UpdatedFields:
                    AllUpdatedFields += count;
                    break;
                case SaveProgressMeasure.UpdatedRows:
                    AllUpdatedRows += count;
                    break;
            }
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        public void SetCurrent(SaveProgressMeasure measure, int value)
        {
            switch (measure)
            {
                case SaveProgressMeasure.InsertedRows:
                    FinishedInserted = value;
                    break;
                case SaveProgressMeasure.DeletedRows:
                    FinishedDeleted = value;
                    break;
                case SaveProgressMeasure.UpdatedFields:
                    FinishedUpdatedFields = value;
                    break;
                case SaveProgressMeasure.UpdatedRows:
                    FinishedUpdatedRows = value;
                    break;
            }
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        public int GetCurrent(SaveProgressMeasure measure)
        {
            switch (measure)
            {
                case SaveProgressMeasure.InsertedRows:
                    return FinishedInserted;
                case SaveProgressMeasure.DeletedRows:
                    return FinishedDeleted;
                case SaveProgressMeasure.UpdatedFields:
                    return FinishedUpdatedFields;
                case SaveProgressMeasure.UpdatedRows:
                    return FinishedUpdatedRows;
            }
            return 0;
        }

        public bool IsCanceled { get; set; }

        public void NotifyFinished()
        {
            IsFinished = true;
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        #endregion

        public bool IsFinished;
    }
}
