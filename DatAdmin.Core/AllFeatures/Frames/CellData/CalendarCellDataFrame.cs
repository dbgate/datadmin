using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class CalendarCellDataFrame : CellDataFrameBase
    {
        public CalendarCellDataFrame()
        {
            InitializeComponent();
        }

        public override void ShowCurrentData()
        {
            if (m_targetType.IsDateRelated())
            {
                Controls.ShowError(false);
                try
                {
                    monthCalendar1.SelectionStart = m_holder.GetDateTimeValue().AsDateTime;
                }
                catch
                {
                    monthCalendar1.SelectionStart = DateTime.UtcNow;
                }
            }
            else
            {
                Controls.ShowError(true);
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (m_loadingData) return;
            try
            {
                if (!m_targetType.IsDateRelated()) return;
                DateTimeEx value = m_holder.GetDateTimeValue();
                value.DatePart = DateTimeEx.FromDateTime(monthCalendar1.SelectionStart).DatePart;
                m_holder.SetDateTimeValue(m_targetType, value);
                m_data.SetData(m_holder);
            }
            catch
            {
            }
        }
    }

    [CellDataEditor(Name = "calendar", Title = "Calendar")]
    public class CellCellDataEditor : CellDataEditorBase
    {
        public override string MenuTitle
        {
            get { return "s_calendar"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.calendar; }
        }

        protected override CellDataFrameBase CreateControl()
        {
            return new CalendarCellDataFrame();
        }

        public override int SupportLevel(IDataHolder data, IBedValueReader holder)
        {
            if (holder.GetFieldType().IsDateRelated()) return 10;
            return 0;
        }
    }
}
