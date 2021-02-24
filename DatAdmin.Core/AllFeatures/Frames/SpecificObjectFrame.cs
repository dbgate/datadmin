using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SpecificObjectFrame : ContentFrame
    {
        IDatabaseSource m_conn;
        ISpecificObjectStructure m_obj;
        ISpecificRepresentation m_repr;
        string m_objtype;
        ObjectEditorPars m_pars;
        ReformatToolkit m_reformat;

        public SpecificObjectFrame(IDatabaseSource conn, ISpecificObjectStructure obj, ObjectEditorPars pars)
        {
            InitializeComponent();
            m_pars = pars;
            m_conn = conn;
            m_obj = obj;
            m_objtype = obj.ObjectType;
            if (m_conn != null)
            {
                codeEditor1.Dialect = m_conn.Dialect;
                m_reformat = new ReformatToolkit(m_conn.Dialect, codeEditor1);
            }
            else
            {
                m_reformat = new ReformatToolkit(new GenericDialect(), codeEditor1);
            }
            m_repr = SpecificRepresentationAddonType.Instance.FindRepresentation(m_obj.ObjectType);
            FillFromStructure();
        }

        public SpecificObjectFrame(IDatabaseSource conn, string objtype, string initsql, ObjectEditorPars pars)
        {
            InitializeComponent();
            m_conn = conn;
            m_objtype = objtype;
            m_pars = pars;
            m_repr = SpecificRepresentationAddonType.Instance.FindRepresentation(m_objtype);
            if (initsql != null) codeEditor1.Text = initsql;
        }

        [PopupMenu("s_reformat", Shortcut = Keys.Control | Keys.R)]
        public void Reformat()
        {
            m_reformat.Reformat();
        }

        [PopupMenu("s_find", Shortcut = Keys.Control | Keys.F, ImageName = CoreIcons.findName)]
        public void ShowFindDialog()
        {
            codeEditor1.ShowFindDialog();
        }

        [PopupMenu("s_replace", Shortcut = Keys.Control | Keys.H)]
        public void ShowReplaceDialog()
        {
            codeEditor1.ShowReplaceDialog();
        }

        private void FillFromStructure()
        {
            if (m_obj != null)
            {
                codeEditor1.Text = m_obj.CreateSql;
                tbxComment.Text = m_obj.Comment;
            }
            else
            {
                codeEditor1.Text = "";
                tbxComment.Text = "";
            }
            codeEditor1.Modified = false;
        }

        public bool Modified
        {
            get { return codeEditor1.Modified; }
        }

        public override bool AllowClose()
        {
            if (Modified)
            {
                DialogResult dr = MessageBox.Show(Texts.Get("s_close_object_q"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
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

        public override bool Save()
        {
            if (m_obj == null)
            {
                return SaveAs();
            }
            else
            {
                try
                {
                    ISpecificObjectStructure newStructure = CreateStructure(m_obj.ObjectName);
                    if (m_conn.DatabaseCaps.ExecuteSql)
                    {
                        string sql = Dialect.GenerateScript(dmp => { dmp.DropSpecificObject(m_obj); dmp.CreateSpecificObject(newStructure); });
                        if (!SqlConfirmForm.Run(sql, Dialect)) return false;
                    }

                    m_conn.AlterObject(m_obj, newStructure);
                    if (m_pars != null && m_pars.SavedCallback != null) m_pars.SavedCallback();

                    m_obj = m_conn.InvokeLoadSpecificObjectDetail(m_obj.ObjectType, m_obj.ObjectName);
                    FillFromStructure();
                    return true;
                }
                catch (Exception e)
                {
                    Errors.Report(e);
                    return false;
                }
            }
        }

        public override bool SupportsSaveAs { get { return true; } }

        ISpecificObjectStructure CreateStructure(NameWithSchema name)
        {
            SpecificObjectStructure res = new SpecificObjectStructure();
            if (m_obj != null) res.GroupId = m_obj.GroupId;
            res.ObjectType = m_objtype;
            res.CreateSql = codeEditor1.Text;
            res.ObjectName = name;
            res.Comment = tbxComment.Text;
            res.SpecificDialect = Dialect.DialectName;
            return res;
        }

        private ISqlDialect Dialect { get { return m_conn.Dialect; } }

        public override bool SaveAs()
        {
            string name = InputBox.Run(Texts.Get("s_enter_object_name"), "new_table");
            if (name != null)
            {
                try
                {
                    ISpecificObjectStructure newStructure = CreateStructure(new NameWithSchema(name));

                    if (m_conn.DatabaseCaps.ExecuteSql)
                    {
                        string sql = Dialect.GenerateScript(dmp => { dmp.CreateSpecificObject(newStructure); });
                        if (!SqlConfirmForm.Run(sql, Dialect)) return false;
                    }

                    m_conn.CreateObject(newStructure);
                    if (m_pars != null && m_pars.SavedCallback != null) m_pars.SavedCallback();
                    m_obj = m_conn.InvokeLoadSpecificObjectDetail(newStructure.ObjectType, newStructure.ObjectName);
                    FillFromStructure();
                    UpdateTitle();
                    return true;
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                }
            }
            return false;
        }

        public override string MenuBarTitle
        {
            get { return m_repr.TitleSingular; }
        }

        public override string PageTitle
        {
            get
            {
                if (m_obj != null) return m_obj.ObjectName.ToString();
                return m_repr.TitleSingular;
            }
        }

        public override Bitmap Image
        {
            get { return m_repr.Icon; }
        }
    }
}
