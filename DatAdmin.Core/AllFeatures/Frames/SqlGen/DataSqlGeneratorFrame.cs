using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DataSqlGeneratorFrame : UserControl
    {
        IDataSqlGenerator m_generator;
        DataSqlGenColumnsFrameBase m_valuesFrame, m_whereFrame;

        public DataSqlGeneratorFrame()
        {
            InitializeComponent();

            m_valuesFrame = new DataSqlGenColumnsFrame();
            m_whereFrame = new DataSqlGenColumnsFrame();

            AddFrames();
        }

        private void AddFrames()
        {
            Controls.Add(m_valuesFrame);
            m_valuesFrame.Hide();
            m_valuesFrame.GroupBoxTitle = "VALUES";

            Controls.Add(m_whereFrame);
            m_whereFrame.Hide();
            m_whereFrame.GroupBoxTitle = "WHERE";

            m_valuesFrame.SettingsChanged += m_valuesFrame_SettingsChanged;
            m_whereFrame.SettingsChanged += m_valuesFrame_SettingsChanged;

            m_valuesFrame.InitializeMode(DataSqlGeneratorColumnSet.ModeEnum.SelectedColumns);
            m_whereFrame.InitializeMode(DataSqlGeneratorColumnSet.ModeEnum.PrimaryKey);
        }

        void m_valuesFrame_SettingsChanged(object sender, EventArgs e)
        {
            UpdateGeneratorColumns();
            if (SettingsChanged != null) SettingsChanged(this, e);
        }

        void UpdateGeneratorColumns()
        {
            if (Generator != null)
            {
                Generator.ValueColumns = m_valuesFrame.GetColumnSet();
                Generator.WhereColumns = m_whereFrame.GetColumnSet();
            }
        }

        public event EventHandler SettingsChanged;

        public IDataSqlGenerator Generator
        {
            get { return m_generator; }
            set
            {
                m_generator = value;
                OnChangeGenerator();
            }
        }

        private void OnChangeGenerator()
        {
            m_valuesFrame.Hide();
            m_whereFrame.Hide();
            if (m_generator == null) return;
            int acty = 5;
            if (m_generator.NeedsValueColumns)
            {
                m_valuesFrame.Top = acty;
                m_valuesFrame.Visible = true;
                m_valuesFrame.Width = Width;
                m_valuesFrame.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                acty += m_valuesFrame.Height + 5;
            }
            if (m_generator.NeedsWhereColumns)
            {
                m_whereFrame.Top = acty;
                m_whereFrame.Visible = true;
                m_whereFrame.Width = Width;
                m_whereFrame.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                acty += m_whereFrame.Height + 5;
            }
            UpdateGeneratorColumns();
        }

        public void SetHintStructure(ITableStructure table, string[] selcolumns)
        {
            m_valuesFrame.SettingsChanged -= m_valuesFrame_SettingsChanged;
            m_whereFrame.SettingsChanged -= m_valuesFrame_SettingsChanged;

            m_whereFrame.Dispose();
            m_valuesFrame.Dispose();

            m_whereFrame = new DataSqlGenColumnsFrame2();
            m_valuesFrame = new DataSqlGenColumnsFrame2();

            m_whereFrame.SetHintStructure(table, selcolumns);
            m_valuesFrame.SetHintStructure(table, selcolumns);

            AddFrames();
            OnChangeGenerator();
        }
    }
}
