using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.advjobs
{
    public class ScheduleDef
    {
        ScheduleArray m_minute, m_hour, m_dayOfWeek, m_dayOfMonth, m_month;
        public ScheduleDef(string minute, string hour, string dayOfMonth, string month, string dayOfWeek)
        {
            m_minute = ScheduleArray.Parse(minute, 0, 59);
            m_hour = ScheduleArray.Parse(hour, 0, 23);
            m_dayOfMonth = ScheduleArray.Parse(dayOfMonth, 1, 31);
            m_month = ScheduleArray.Parse(month, 1, 12);
            m_dayOfWeek = ScheduleArray.Parse(dayOfWeek, 0, 6);
        }

        public bool Match(DateTime date)
        {
            if (!m_minute[date.Minute]) return false;
            if (!m_hour[date.Hour]) return false;
            if (!m_dayOfMonth[date.Day]) return false;
            if (!m_month[date.Month]) return false;
            if (!m_dayOfWeek[(int)date.DayOfWeek]) return false;
            return true;
        }

        private DateTimeEx? GetFirstExecuteAfter_NoWeekDay(DateTimeEx dt)
        {
            dt.Minute += 1;
            dt.Second = 0;
            dt.Nanosecond = 0;
        before_month:
            if (!m_month[dt.Month])
            {
                dt.Minute = 0;
                dt.Hour = 0;
                dt.Day = 1;
                bool monthis1 = dt.Month == 1;
                while (dt.Month <= 12 && !m_month[dt.Month]) dt.Month++;
                if (dt.Month == 13)
                {
                    if (monthis1) return null;
                    dt.Month = 1;
                    goto before_month;
                }
            }
        before_day:
            if (!m_dayOfMonth[dt.Day] || !dt.DatePart.IsValid)
            {
                dt.Minute = 0;
                dt.Hour = 0;
                bool dayis1 = dt.Day == 1;
                while (dt.Day <= 31 && !m_dayOfMonth[dt.Day]) dt.Day++;

                if (dt.Day == 32 || !dt.DatePart.IsValid)
                {
                    if (dayis1) return null;
                    dt.Minute = 0;
                    dt.Hour = 0;
                    dt.Day = 1;
                    dt.Month += 1;
                    goto before_month;
                }
            }
        before_hour:
            if (!m_hour[dt.Hour])
            {
                dt.Minute = 0;
                bool houris0 = dt.Hour == 0;
                while (dt.Hour <= 23 && !m_hour[dt.Hour]) dt.Hour++;
                if (dt.Hour == 24)
                {
                    if (houris0) return null;
                    dt.Minute = 0;
                    dt.Hour = 0;
                    dt.Day += 1;
                    goto before_day;
                }
            }
            if (!m_minute[dt.Minute])
            {
                bool minuteis0 = dt.Minute == 0;
                while (dt.Minute <= 59 && !m_minute[dt.Minute]) dt.Minute++;
                if (dt.Minute == 60)
                {
                    if (minuteis0) return null;
                    dt.Minute = 0;
                    dt.Hour += 1;
                    goto before_hour;
                }
            }
            return dt;
        }

        public DateTime? GetFirstExecuteAfter(DateTime lastex)
        {
            DateTimeEx dt = DateTimeEx.FromDateTime(lastex);
            for (int i = 0; i < 20; i++)
            {
                var res = GetFirstExecuteAfter_NoWeekDay(dt);
                if (res == null) return null;
                var rescand = res.Value.AsDateTime;
                if (m_dayOfWeek[(int)rescand.DayOfWeek]) return rescand;
                dt = res.Value;
                dt.Hour = 0;
                dt.Minute = 0;
                dt = DateTimeEx.FromDateTime(dt.AsDateTime.AddDays(1));
            }
            return null;
        }

        public DateTime? GetExecuteBetween(DateTime lastex, DateTime now)
        {
            var ex = GetFirstExecuteAfter(lastex);
            if (ex == null) return null;
            if (ex.Value <= now) return ex.Value;
            return null;
        }
    }
}
