using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;

namespace Plugin.textio
{
    [TextFormatter(Name = "doku_wiki", Title = "DokuWIKI")]
    public class DokuWikiFormatter : TextFormatterBase
    {
        protected override void WriteRaw(string data)
        {
            m_stream.Write(data);
        }

        public override ITextFormatter Heading(object heading, int level)
        {
            Write("\n");
            if (level < 1) level = 1;
            if (level > 5) level = 5;
            for (int i = 0; i < (7 - level); i++) m_stream.Write('=');
            Write(heading);
            for (int i = 0; i < (7 - level); i++) m_stream.Write('=');
            Write("\n");
            return this;
        }

        public override ITextFormatter Bold(object text)
        {
            WriteRaw("**");
            Write(text);
            WriteRaw("**");
            return this;
        }

        public override ITextFormatter Italics(object text)
        {
            WriteRaw("//");
            Write(text);
            WriteRaw("//");
            return this;
        }

        public override ITextFormatter BoldItalics(object text)
        {
            WriteRaw("**//");
            Write(text);
            WriteRaw("//**");
            return this;
        }
        public override ITextFormatter BeginTable(TableStyle style)
        {
            m_stream.Write("\n");
            return this;
        }
        public override ITextFormatter BeginRow(bool heading)
        {
            m_stream.Write(heading ? "^" : "|");
            return this;
        }
        public override ITextFormatter EndRow(bool heading)
        {
            m_stream.Write("\n");
            return this;
        }
        public override ITextFormatter EndTable()
        {
            return this;
        }

        public override ITextFormatter BeginCell(bool heading)
        {
            return this;
        }

        public override ITextFormatter EndCell(bool heading)
        {
            m_stream.Write(heading ? "^" : "|");
            return this;
        }

        public override ITextFormatter WriteNBR()
        {
            WriteRaw(" ");
            return this;
        }
    }
}
