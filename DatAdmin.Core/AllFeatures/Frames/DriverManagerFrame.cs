using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.Common;
using System.IO;

namespace DatAdmin
{
    public partial class DriverManagerFrame : UserControl
    {
        bool m_typesLoaded = false;

        public DriverManagerFrame()
        {
            InitializeComponent();
            openFileDialog1.Filter = String.Format("{0} (*.DLL)|*.dll|{0} (*.EXE)|*.exe", Texts.Get("s_assemblies"));
            Reload();
        }

        void Reload()
        {
            lbdrivers.Items.Clear();
            foreach (DbDriverDefinition drv in DbDriverManager.Instance.Drivers)
            {
                lbdrivers.Items.Add(drv);
            }
            LoadCurrent();
        }

        private DbDriverDefinition SelectedDriver { get { return (DbDriverDefinition)lbdrivers.SelectedItem; } }

        int m_denyLoadCurrent;
        private void edname_TextChanged(object sender, EventArgs e)
        {
            if (SelectedDriver != null)
            {
                SelectedDriver.Name = edname.Text;
                try
                {
                    m_denyLoadCurrent++;
                    lbdrivers.Items[lbdrivers.SelectedIndex] = SelectedDriver;
                }
                finally
                {
                    m_denyLoadCurrent--;
                }
            }
        }

        private void lbdrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_denyLoadCurrent > 0) return;
            LoadCurrent();
        }

        private void LoadCurrent()
        {
            bool enabled = false;
            if (SelectedDriver != null)
            {
                edname.Text = SelectedDriver.Name;
                edfile.Text = SelectedDriver.AssemblyPath;
                edfactory.Text = SelectedDriver.FactoryType;
                edinvname.Text = SelectedDriver.InvariantName;
                m_typesLoaded = false;
                enabled = !SelectedDriver.IsSystem;
            }
            else
            {
                edname.Text = "";
                edfile.Text = "";
                edfactory.Text = "";
                edinvname.Text = "";
            }
            edname.Enabled = enabled;
            edfactory.Enabled = enabled;
            btbrowse.Enabled = enabled;
            edinvname.Enabled = enabled;
        }

        private void edfactory_DropDown(object sender, EventArgs e)
        {
            if (!m_typesLoaded)
            {
                try
                {
                    edfactory.Items.Clear();
                    Assembly a = Assembly.LoadFile(Path.Combine(Core.ProgramDirectory, edfile.Text));
                    foreach (Type t in a.GetTypes())
                    {
                        if (t.IsSubclassOf(typeof(DbProviderFactory))) edfactory.Items.Add(t.FullName);
                    }
                    m_typesLoaded = true;
                }
                catch (Exception err)
                {
                    StdDialog.ShowError(Texts.Get("s_error_loading_assembly$error", "error", err));
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DbDriverDefinition driver = DbDriverManager.Instance.AddDriver();
            Reload();
            lbdrivers.SelectedItem = driver;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (SelectedDriver != null && !SelectedDriver.IsSystem)
            {
                if (MessageBox.Show(Texts.Get("s_really_remove$driver", "driver", SelectedDriver.Name), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DbDriverManager.Instance.RemoveDriver(SelectedDriver);
                    Reload();
                }
            }
        }

        private void btbrowse_Click(object sender, EventArgs e)
        {
            if (SelectedDriver == null) return;
            if (edfile.Text != "")
            {
                openFileDialog1.FileName = Path.Combine(Core.ProgramDirectory, edfile.Text);
            }
            else
            {
                openFileDialog1.InitialDirectory = Core.ProgramDirectory;
            }
            if (openFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                m_typesLoaded = false;
                edfile.Text = IOTool.RelativePathTo(Core.ProgramDirectory, openFileDialog1.FileName);
                SelectedDriver.AssemblyPath = edfile.Text;
            }
        }

        private void edfactory_TextChanged(object sender, EventArgs e)
        {
            if (SelectedDriver != null)
            {
                SelectedDriver.FactoryType = edfactory.Text;
            }
        }

        private void edinvname_TextChanged(object sender, EventArgs e)
        {
            if (SelectedDriver != null)
            {
                SelectedDriver.InvariantName = edinvname.Text;
            }
        }
    }
}
