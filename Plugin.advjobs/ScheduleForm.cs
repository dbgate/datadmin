using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.advjobs
{
    public partial class ScheduleForm : FormEx
    {
        JobDbConnection m_conn;
        int m_id;

        public ScheduleForm(JobDbConnection conn, int id)
        {
            InitializeComponent();
            m_conn = conn;
            m_id = id;
            var row = m_conn.ExecuteRow("select Minutes, Hours, DaysOfWeek, DaysOfMonth, Months from Job where ID=" + id.ToString());
            tbxMinute.Text = row[0].ToString();
            tbxHour.Text = row[1].ToString();
            tbxDayOfWeek.Text = row[2].ToString();
            tbxDayOfMonth.Text = row[3].ToString();
            tbxMonth.Text = row[4].ToString();
        }

        public static void Run(JobDbConnection conn, int id)
        {
            var win = new ScheduleForm(conn, id);
            win.ShowDialogEx();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ar = ScheduleArray.Parse(tbxMinute.Text, 0, 59);
            ar = ScheduleArrayForm.Run(6, 10, 60, null, 0, true, ar);
            if (ar != null) tbxMinute.Text = ar.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_conn.ExecuteNonQuery(String.Format(
                "update Job set Minutes='{0}', Hours='{1}', DaysOfWeek='{2}', DaysOfMonth='{3}', Months='{4}' where id={5}",
                tbxMinute.Text, tbxHour.Text, tbxDayOfWeek.Text, tbxDayOfMonth.Text, tbxMonth.Text, m_id));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ar = ScheduleArray.Parse(tbxHour.Text, 0, 23);
            ar = ScheduleArrayForm.Run(2, 12, 24, null, 0, false, ar);
            if (ar != null) tbxHour.Text = ar.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ar = ScheduleArray.Parse(tbxDayOfMonth.Text, 1, 31);
            ar = ScheduleArrayForm.Run(4, 10, 31, null, 1, false, ar);
            if (ar != null) tbxDayOfMonth.Text = ar.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ar = ScheduleArray.Parse(tbxMonth.Text, 1, 12);
            var MONTHS = new string[] { "Jab", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Now", "Dec" };
            ar = ScheduleArrayForm.Run(2, 6, 12, MONTHS, 1, false, ar);
            if (ar != null) tbxMonth.Text = ar.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var ar = ScheduleArray.Parse(tbxDayOfWeek.Text, 0, 6);
            var WEEKDAYS = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            ar = ScheduleArrayForm.Run(1, 7, 7, WEEKDAYS, 0, false, ar);
            if (ar != null) tbxDayOfWeek.Text = ar.ToString();
        }

        private ScheduleDef GetScheduleDef()
        {
            return new ScheduleDef(tbxMinute.Text, tbxHour.Text, tbxDayOfMonth.Text, tbxMonth.Text, tbxDayOfWeek.Text);
        }

        private void OnShowPlan(object sender, EventArgs e)
        {
            try
            {
                var sd = GetScheduleDef();
                var d1 = sd.GetFirstExecuteAfter(DateTime.Now);
                if (d1 != null) tbxPlan.Text = d1.ToString();
                else tbxPlan.Text = "";
            }
            catch
            {
                tbxPlan.Text = "";
            }
        }
    }
}
