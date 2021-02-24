using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class AddonEditorFrame : ContentFrame
    {
        IAddonInstance m_obj;
        string m_filepath;

        public AddonEditorFrame(IAddonInstance obj, string filepath)
        {
            InitializeComponent();

            m_filepath = filepath;
            m_obj = obj;

            propertyFrame1.SelectedObject = m_obj;

            propertyFrame1.SendToBack();
            toolStrip1.SendToBack();
        }

        public override bool Save()
        {
            m_obj.SaveToFile(m_filepath);
            return true;
        }

        public bool IsModified()
        {
            return false;
        }

        public override bool AllowClose()
        {
            if (IsModified())
            {
                DialogResult dr = MessageBox.Show(Texts.Get("s_file_modified_save$file", "file", m_filepath), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    return Save();
                }
                if (dr == DialogResult.No) return true;
                return false;
            }
            return true;
        }
        public override bool SupportsSave { get { return true; } }
        public override string PageTitle
        {
            get
            {
                return IOTool.RelativePathTo(Core.AddonsDirectory, m_filepath);
            }
        }
        public override string PageTypeTitle
        {
            get { return "s_addon"; }
        }
        public override Bitmap Image
        {
            get { return CoreIcons.command; }
        }
    }
}
