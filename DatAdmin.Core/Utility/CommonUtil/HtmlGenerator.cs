using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    [TextFormatter(Name = "html", Title = "HTML")]
    public class HtmlGenerator : TextFormatterBase
    {
        StringWriter m_htmlTextData;

        public const string HtmlObjectViewStyle = @"          
            .name { background-color:#CCCCFF; background-gradient:#CCFFCC; background-gradient-angle:0; font-weight: bold; }
            .value { background-color:#DDDDDD; text-align:left; }
            .infobox { border-width:1px; border-style:solid; padding: 5px; }
            body { font:10pt Tahoma }
            td { text-align:center; padding: 2px; corner-radius:3px; border:outset #CCCCCC 1px; }";

        public HtmlGenerator()
        {
            m_htmlTextData = new StringWriter();
            m_stream = m_htmlTextData;
        }
        public HtmlGenerator(TextWriter stream)
        {
            m_stream = stream;
        }
        public string HtmlText
        {
            get
            {
                if (m_htmlTextData == null) throw new Exception("DAE-00282 HtmlText is not available when writing to stream");
                return m_htmlTextData.ToString();
            }
        }
        public override ITextFormatter Heading(object heading, int level)
        {
            m_stream.Write("<h{0}>", level);
            Write(heading);
            m_stream.Write("</h{0}>", level);
            return this;
        }
        public override ITextFormatter Anchor(object name)
        {
            m_stream.Write("<a name=\"{0}\"></a>", name);
            return this;
        }
        public override ITextFormatter BeginTable(TableStyle style)
        {
            m_stream.Write("<table");
            if (style != null)
            {
                if (style.WidthPercent != null) m_stream.Write(" width='{0}%'", style.WidthPercent);
                if (style.Border > 0) m_stream.Write(" border='{0}'", style.Border);
            }
            m_stream.Write(">");
            return this;
        }
        public override ITextFormatter BeginRow(bool heading)
        {
            m_stream.Write("<tr>");
            return this;
        }
        public override ITextFormatter EndRow(bool heading)
        {
            m_stream.Write("</tr>");
            return this;
        }
        public override ITextFormatter EndTable()
        {
            m_stream.Write("</table>");
            return this;
        }

        public override ITextFormatter BeginCell(bool heading)
        {
            m_stream.Write(heading ? "<th>" : "<td>");
            return this;
        }

        public override ITextFormatter EndCell(bool heading)
        {
            m_stream.Write(heading ? "</th>" : "</td>");
            return this;
        }

        public HtmlGenerator PropTableRow(string name, string value)
        {
            m_stream.Write("<tr><td class=\"name\">{0}</td><td class=\"value\">{1}</td></tr>", Texts.Get(name), value == null ? "" : Texts.Get(value));
            return this;
        }

        public HtmlGenerator PropsTableBegin()
        {
            m_stream.Write("<table width=\"100%\">");
            return this;
        }

        public HtmlGenerator PropsTableEnd()
        {
            m_stream.Write("</table>");
            return this;
        }

        public override ITextFormatter BeginUl()
        {
            m_stream.Write("<ul>");
            return this;
        }

        public override ITextFormatter EndUl()
        {
            m_stream.Write("</ul>");
            return this;
        }

        protected override void WriteRaw(string data)
        {
            m_stream.Write(data);
        }
        public override ITextFormatter InetLink(object href, object text)
        {
            m_stream.Write("<a href=\"{0}\">{1}</a>", href, text);
            return this;
        }
        public override ITextFormatter LocalLink(object anchor, object text)
        {
            m_stream.Write("<a href=\"#{0}\">{1}</a>", anchor, text);
            return this;
        }

        public HtmlGenerator BeginHtml(string title)
        {
            return BeginHtml(title, null);
        }

        public HtmlGenerator BeginHtml(string title, string style)
        {
            m_stream.Write("<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">");
            m_stream.Write("<meta name=\"generator\" content=\"DatAdmin, www.datadmin.com\">");
            m_stream.Write("<title>{0}</title>", Texts.LangGet(title, Language));
            if (style != null) m_stream.Write("<style>\n{0}\n</style>", style);
            m_stream.Write("</head><body>");
            return this;
        }

        public HtmlGenerator EndHtml()
        {
            m_stream.Write("</body></html>");
            return this;
        }

        public override ITextFormatter BeginLi()
        {
            m_stream.Write("<li>");
            return this;
        }

        public override ITextFormatter EndLi()
        {
            m_stream.Write("</li>");
            return this;
        }

        public override ITextFormatter Bold(object text)
        {
            m_stream.Write("<b>");
            Write(text);
            m_stream.Write("</b>");
            return this;
        }
        public override ITextFormatter Italics(object text)
        {
            m_stream.Write("<i>");
            Write(text);
            m_stream.Write("</i>");
            return this;
        }
        public override ITextFormatter BoldItalics(object text)
        {
            m_stream.Write("<b><i>");
            Write(text);
            m_stream.Write("</i></b>");
            return this;
        }
        public override ITextFormatter BeginFile(string title)
        {
            BeginHtml(title);
            return this;
        }

        public override ITextFormatter EndFile()
        {
            return EndHtml();
        }

        public override string FileExtension
        {
            get { return "html"; }
        }

        public override ITextFormatter WriteNBR()
        {
            WriteRaw("&nbsp;");
            return this;
        }

        public ITextFormatter HorizontalRule()
        {
            WriteRaw("<hr>");
            return this;
        }
    }
}
