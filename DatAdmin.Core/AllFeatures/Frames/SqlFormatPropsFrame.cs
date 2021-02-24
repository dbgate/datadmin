using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SqlFormatPropsFrame : UserControl
    {
        bool m_machineChanging;

        public SqlFormatPropsFrame()
        {
            InitializeComponent();
            foreach (var item in EncodingTypeConverter.EncodingItems)
            {
                cbxBinaryEncoding.Items.Add(item);
            }
            cbxBinaryEncoding.SelectedIndex = EncodingTypeConverter.GetEncodingIndex(Encoding.UTF8);
        }

        [Browsable(false)]
        public SqlFormatProperties Value
        {
            get
            {
                var res = new SqlFormatProperties();
                res.BinaryStrings = chbBinaryStrings.Checked;
                res.RealBinaryEncoding = ((EncodingItem)cbxBinaryEncoding.SelectedItem).RealEncoding;
                res.IdentifierQuoteMode = chbQuoteIdentifiers.Checked ? SqlIdentifierQuoteMode.Quoted : SqlIdentifierQuoteMode.Plain;
                res.QualifierMode = chbOmitSchema.Checked ? SqlQualifierMode.OmitAll : SqlQualifierMode.Original;
                if (rbtLowerCase.Checked) res.SqlCommandCase = CharacterCase.Lower;
                if (rbtUpperCase.Checked) res.SqlCommandCase = CharacterCase.Upper;
                return res;
            }
            set
            {
                m_machineChanging = true;
                chbBinaryStrings.Checked = value.BinaryStrings;
                cbxBinaryEncoding.SelectedIndex = EncodingTypeConverter.GetEncodingIndex(value.RealBinaryEncoding);
                chbQuoteIdentifiers.Checked = value.IdentifierQuoteMode == SqlIdentifierQuoteMode.Quoted;
                chbOmitSchema.Checked = value.QualifierMode == SqlQualifierMode.OmitAll;
                rbtLowerCase.Checked = value.SqlCommandCase == CharacterCase.Lower;
                rbtUpperCase.Checked = value.SqlCommandCase == CharacterCase.Upper;
                m_machineChanging = false;
            }
        }

        private void Ev_ChangedAnyProperty(object sender, EventArgs e)
        {
            cbxBinaryEncoding.Enabled = chbBinaryStrings.Checked;
            if (m_machineChanging) return;
            if (UserChangedProperties != null) UserChangedProperties(this, EventArgs.Empty);
        }

        public event EventHandler UserChangedProperties;
    }
}
