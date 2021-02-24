using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;

namespace Plugin.textio
{
    [TextFormatter(Name = "media_wiki", Title = "Media WIKI")]
    public class MediaWikiFormatter : TextFormatterBase
    {
        protected override void WriteRaw(string data)
        {
            m_stream.Write(data);
        }

        public override ITextFormatter Heading(object heading, int level)
        {
            Write("\n");
            if (level < 2) level = 2;
            if (level > 6) level = 6;
            for (int i = 0; i < level; i++) m_stream.Write('=');
            Write(heading);
            for (int i = 0; i < level; i++) m_stream.Write('=');
            Write("\n");
            return this;
        }

        public override ITextFormatter Bold(object text)
        {
            WriteRaw("'''");
            Write(text);
            WriteRaw("'''");
            return this;
        }

        public override ITextFormatter Italics(object text)
        {
            WriteRaw("''");
            Write(text);
            WriteRaw("''");
            return this;
        }

        public override ITextFormatter BoldItalics(object text)
        {
            WriteRaw("''''");
            Write(text);
            WriteRaw("''''");
            return this;
        }
        public override ITextFormatter BeginTable(TableStyle style)
        {
            m_stream.Write("\n");
            m_stream.Write("{|");
            if (style != null)
            {
                if (style.WidthPercent != null) m_stream.Write(" width='{0}%'", style.WidthPercent);
                if (style.Border > 0) m_stream.Write(" border='{0}'", style.Border);
            }
            m_stream.Write("\n");
            return this;
        }
        public override ITextFormatter BeginRow(bool heading)
        {
            if (!heading) m_stream.Write("|-\n");
            return this;
        }
        public override ITextFormatter EndRow(bool heading)
        {
            //m_stream.Write("\n");
            return this;
        }
        public override ITextFormatter EndTable()
        {
            m_stream.Write("\n|}\n");
            return this;
        }

        public override ITextFormatter BeginCell(bool heading)
        {
            if (heading) m_stream.Write("!");
            else m_stream.Write("|");
            return this;
        }

        public override ITextFormatter EndCell(bool heading)
        {
            m_stream.Write("\n");
            return this;
        }

        public override ITextFormatter WriteNBR()
        {
            WriteRaw(" ");
            return this;
        }
    }
}
