using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DomainEditFrame : ContentFrame
    {
        IDomainStructure m_oldDomain;
        DomainStructure m_domain;
        ISqlDialect m_dialect;
        ISpecificType m_dataType;
        IDatabaseSource m_conn;

        public DomainEditFrame()
        {
            InitializeComponent();
        }

        public DomainEditFrame(IDatabaseSource conn, IDomainStructure domain)
        {
            InitializeComponent();
            m_conn = conn;
            Init(domain, conn.Dialect);
        }

        public void Init(IDomainStructure domain, ISqlDialect dialect)
        {
            m_oldDomain = domain;
            if (domain == null)
            {
                m_domain = new DomainStructure();
                m_domain.DataType = new DbTypeString();
            }
            else
            {
                m_domain = new DomainStructure(domain);
            }
            m_dialect = dialect;
            cbxDataType.Items.Clear();
            foreach (string code in Enum.GetNames(m_dialect.SpecificTypeEnum).Sorted())
            {
                cbxDataType.Items.Add(code);
            }
            m_dataType = m_dialect.GenericTypeToSpecific(m_domain.DataType);
            propertyGrid1.SelectedObject = m_dataType;

            cbxDataType.SelectedIndex = cbxDataType.Items.IndexOf(((ISpecificType)m_dataType).Code.ToString());
            chbNullable.Checked = m_domain.IsNullable;
        }

        private void cbxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = cbxDataType.Items[cbxDataType.SelectedIndex].ToString();
            object depcode = Enum.Parse(m_dialect.SpecificTypeEnum, value, true);
            m_dataType = m_dialect.CreateSpecificTypeInstance(depcode);
            propertyGrid1.SelectedObject = m_dataType;
        }

        public override bool SupportsSave
        {
            get { return true; }
        }

        public override bool SupportsSaveAs
        {
            get { return true; }
        }

        public override bool Save()
        {
            if (m_domain.FullName == null)
            {
                return SaveAs();
            }
            else
            {
                m_domain.DataType = m_dataType.ToGenericType();
                m_domain.IsNullable = chbNullable.Checked;
                m_conn.AlterObject(m_oldDomain, m_domain);
                Init(m_domain, m_conn.Dialect);
                return true;
            }
        }

        public override bool SaveAs()
        {
            NameWithSchema name = NameWithSchemaForm.Run(
                m_conn.InvokeLoadSchemata(),
                m_conn.DatabaseCaps.MultipleSchema,
                new NameWithSchema(m_conn.DefaultSchema, "new_domain"));
            if (name != null)
            {
                m_domain.FullName = name;
                return Save();
            }
            return false;
        }

        public override Bitmap Image
        {
            get { return CoreIcons.domain; }
        }

        public override string PageTitle
        {
            get { return "s_domain"; }
        }
    }
}
