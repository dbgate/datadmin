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
    public partial class MacroRecordForm : FormEx
    {
        MacroRecorder m_recorder;
        public MacroRecordForm(MacroRecorder recorder)
        {
            InitializeComponent();
            m_recorder = recorder;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_recorder.IsRunning)
            {
                m_recorder.Pause();
                btpause.Text = Texts.Get("s_start");
            }
            else
            {
                m_recorder.Start();
                btpause.Text = Texts.Get("s_pause");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_recorder.Close();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_recorder.Close();
            saveFileDialog1.FileName = Path.Combine(Core.MacrosDirectory, "macro.mdx");
            if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                m_recorder.SaveToFile(saveFileDialog1.FileName);
            }
            Close();
        }
    }
}
