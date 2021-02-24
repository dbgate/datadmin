using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class TextFormatterExtension
    {
        public static ITextFormatter Cell(this ITextFormatter fmt, params object[] data)
        {
            fmt.BeginCell(false);
            foreach (var param in data) fmt.Write(param);
            fmt.WriteNBR();
            fmt.EndCell(false);
            return fmt;
        }
        public static ITextFormatter HeadingCell(this ITextFormatter fmt, params object[] data)
        {
            fmt.BeginCell(true);
            foreach (var param in data) fmt.Write(param);
            fmt.EndCell(true);
            return fmt;
        }
        public static ITextFormatter Li(this ITextFormatter fmt, string s)
        {
            fmt.BeginLi();
            fmt.Write(s);
            fmt.EndLi();
            return fmt;
        }

        public static ITextFormatter H1(this ITextFormatter fmt, object heading) { return fmt.Heading(heading, 1); }
        public static ITextFormatter H2(this ITextFormatter fmt, object heading) { return fmt.Heading(heading, 2); }
        public static ITextFormatter H3(this ITextFormatter fmt, object heading) { return fmt.Heading(heading, 3); }
        public static ITextFormatter H4(this ITextFormatter fmt, object heading) { return fmt.Heading(heading, 4); }
        public static ITextFormatter H5(this ITextFormatter fmt, object heading) { return fmt.Heading(heading, 5); }
        public static ITextFormatter H6(this ITextFormatter fmt, object heading) { return fmt.Heading(heading, 6); }
    }
}
