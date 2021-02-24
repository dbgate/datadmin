using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public abstract class TextFormatterBase : AddonBase, ITextFormatter
    {
        protected TextWriter m_stream;

        public TextFormatterBase()
        {
            Language = Texts.Language;
        }

        public override AddonType AddonType
        {
            get { return TextFormatterAddonType.Instance; }
        }

        #region ITextFormatter Members

        public string Language { get; set; }
        protected abstract void WriteRaw(string data);
        public virtual ITextFormatter Write(object data)
        {
            WriteRaw(Texts.LangGet((data ?? "").ToString(), Language));
            return this;
        }
        public virtual ITextFormatter Write(string format, params object[] args)
        {
            WriteRaw(String.Format(format, args));
            return this;
        }
        public abstract ITextFormatter Heading(object heading, int level);
        public virtual ITextFormatter Anchor(object name) { return this; }
        public virtual ITextFormatter BeginTable(TableStyle style) { return this; }
        public virtual ITextFormatter BeginRow(bool heading) { return this; }
        public virtual ITextFormatter EndRow(bool heading) { return this; }
        public virtual ITextFormatter EndTable() { return this; }
        public virtual ITextFormatter BeginCell(bool heading) { return this; }
        public virtual ITextFormatter EndCell(bool heading) { return this; }
        public virtual ITextFormatter Bold(object text)
        {
            Write(text);
            return this;
        }
        public virtual ITextFormatter Italics(object text)
        {
            Write(text);
            return this;
        }
        public virtual ITextFormatter BoldItalics(object text)
        {
            Write(text);
            return this;
        }
        public virtual ITextFormatter BeginUl()
        {
            return this;
        }

        public virtual ITextFormatter EndUl()
        {
            return this;
        }

        public virtual ITextFormatter InetLink(object href, object text)
        {
            Write(text);
            return this;
        }

        public virtual ITextFormatter LocalLink(object anchor, object text)
        {
            Write(text);
            return this;
        }

        public virtual ITextFormatter BeginLi()
        {
            return this;
        }

        public virtual ITextFormatter EndLi()
        {
            return this;
        }

        public virtual ITextFormatter BeginFile(string title)
        {
            return this;
        }

        public virtual ITextFormatter EndFile()
        {
            return this;
        }

        public virtual string FileExtension { get { return "txt"; } }
        public TextWriter Writer
        {
            get { return m_stream; }
            set { m_stream = value; }
        }
        #endregion

        protected string Translate(string text)
        {
            return Texts.LangGet(text, Language);
        }

        public virtual ITextFormatter WriteNBR() { return this; }
    }
}
