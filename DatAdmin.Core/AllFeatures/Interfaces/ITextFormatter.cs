using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public class TextFormatterAttribute : RegisterAttribute { }

    public class TableStyle
    {
        public int? WidthPercent = null;
        public int Border = 0;
        public string Class = null;
    }

    public interface ITextFormatter
    {
        ITextFormatter Heading(object heading, int level);
        ITextFormatter Anchor(object name);
        ITextFormatter BeginTable(TableStyle style);
        ITextFormatter BeginRow(bool heading);
        ITextFormatter EndRow(bool heading);
        ITextFormatter EndTable();
        ITextFormatter BeginCell(bool heading);
        ITextFormatter EndCell(bool heading);
        ITextFormatter BeginUl();
        ITextFormatter EndUl();
        ITextFormatter Write(object data);
        ITextFormatter Write(string format, params object[] args);
        ITextFormatter InetLink(object href, object text);
        ITextFormatter LocalLink(object anchor, object text);
        ITextFormatter BeginLi();
        ITextFormatter EndLi();
        ITextFormatter Bold(object text);
        ITextFormatter Italics(object text);
        ITextFormatter BoldItalics(object text);
        ITextFormatter BeginFile(string title);
        ITextFormatter EndFile();
        ITextFormatter WriteNBR();

        string FileExtension { get; }

        string Language { get; set; }
        TextWriter Writer { get; set; }
    }

    [AddonType]
    public class TextFormatterAddonType : AddonType
    {
        public override Type InterfaceType
        {
            get { return typeof(ITextFormatter); }
        }
        public override string Name
        {
            get { return "textformat"; }
        }
        public override Type RegisterAttributeType
        {
            get { return typeof(TextFormatterAttribute); }
        }
        public static readonly TextFormatterAddonType Instance = new TextFormatterAddonType();
    }
}
