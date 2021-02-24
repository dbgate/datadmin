using System;
using System.Collections.Generic;

using System.Text;
using ICSharpCode.TextEditor;
using System.Xml;
using ICSharpCode.TextEditor.Document;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    public class CodeEditor : TextEditorControl
    {
        bool m_modified;
        CodeLanguage m_language;
        ISqlDialect m_dialect;
        IDatabaseSource m_conn;
        ImageList m_imageList;
        ImageCache m_imgCache;
        CodeCompletionKeyHandler m_complHandler;
        ToolTipProvider m_tipProvider;
        //FindAndReplaceForm m_findForm;

        public CodeEditor()
        {
            Disposed += new EventHandler(CodeEditor_Disposed);

            HSettings.ReloadSettings += HSettings_ReloadSettings;
            HSettings_ReloadSettings();
            ActiveTextAreaControl.TextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(TextArea_KeyDown);
            m_imageList = new ImageList();
            m_imgCache = new ImageCache(m_imageList, Color.White);
            if (MainWindow.Instance != null && CodeCompletionFeature.Allowed)
            {
                m_complHandler = CodeCompletionKeyHandler.Attach(MainWindow.Instance.Window, this);
                m_tipProvider = ToolTipProvider.Attach(MainWindow.Instance.Window, this);
            }
        }

        public CodeCompletionKeyHandler CompletationHandler
        {
            get { return m_complHandler; }
        }

        void TextArea_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        public void CallDatabaseConnectionNeeded()
        {
            if (DatabaseConnectionNeeded != null) DatabaseConnectionNeeded(this, EventArgs.Empty);
        }

        void HSettings_ReloadSettings()
        {
            if (GlobalSettings.Pages == null) return;
            var opt = GlobalSettings.Pages.Editor();
            TabIndent = opt.TabWidth;
            ConvertTabsToSpaces = !opt.UseTabs;
            ShowLineNumbers = opt.ShowLineNumbers;
            IndentStyle = opt.AutoIndent ? IndentStyle.Auto : IndentStyle.None;
            ShowTabs = opt.ShowTabs;
            ShowSpaces = opt.ShowSpaces;
            ShowEOLMarkers = opt.ShowEOLMarkers;
            LineViewerStyle = opt.HighlightCurrentLine ? LineViewerStyle.FullRow : LineViewerStyle.None;
            ShowMatchingBracket = opt.ShowMatchingBracket;
        }

        void CodeEditor_Disposed(object sender, EventArgs e)
        {
            FindAndReplaceForm.DisposedEditor(this);
            HSettings.ReloadSettings -= HSettings_ReloadSettings;
            //if (m_findForm != null) m_findForm.Dispose();
        }

        public bool Modified
        {
            get { return m_modified; }
            set { m_modified = value; }
        }

        public bool ReadOnly
        {
            get { return Document.ReadOnly; }
            set { Document.ReadOnly = value; }
        }

        public string CodeText { get { return Document.TextContent; } }

        public void SetCodeText(string value, bool undoPossible)
        {
            if (undoPossible && !String.IsNullOrEmpty(Document.TextContent))
            {
                Document.Replace(0, Document.TextContent.Length, value);
            }
            else
            {
                Document.TextContent = value;
            }
            Refresh();
            if (CompletationHandler != null) CompletationHandler.InvalidateCompletion();
        }

        public void SetXML()
        {
            SetHighlighting("XML");
        }

        public void SetResourceHighlight(string name, Func<string> loader, bool forceLoad)
        {
            if (!HighlightingManager.Manager.HighlightingDefinitions.ContainsKey(name) || forceLoad)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(loader());

                using (XmlNodeReader reader = new XmlNodeReader(doc.DocumentElement))
                {
                    var strategy = HighlightingDefinitionParser.Parse(
                        new SyntaxMode(name + ".xml", name, new string[] { }), reader);
                    HighlightingManager.Manager.HighlightingDefinitions[name] = strategy;
                    strategy.ResolveReferences();
                    Document.HighlightingStrategy = strategy;
                }
            }
            else
            {
                Document.HighlightingStrategy = (IHighlightingStrategy)HighlightingManager.Manager.HighlightingDefinitions[name];
            }
        }

        public ISqlDialect Dialect
        {
            get { return m_dialect; }
            set
            {
                m_dialect = value;
                if (m_language == CodeLanguage.Sql) LoadSqlHighlight();
            }
        }

        public IDatabaseSource Connection
        {
            get { return m_conn; }
            set
            {
                m_conn = value;
            }
        }

        public ImageCache ImageCache { get { return m_imgCache; } }

        public CodeLanguage Language
        {
            get { return m_language; }
            set
            {
                m_language = value;
                switch (m_language)
                {
                    case CodeLanguage.Python:
                        SetResourceHighlight("Python", () => CoreRes.Python_Mode, false);
                        break;
                    case CodeLanguage.None:
                        SetHighlighting("");
                        break;
                    case CodeLanguage.Template:
                        SetResourceHighlight("Template", () => CoreRes.Template_Mode, false);
                        break;
                    case CodeLanguage.Sql:
                        LoadSqlHighlight();
                        break;
                }
            }
        }

        private void LoadSqlHighlight()
        {
            if (m_dialect != null && m_dialect.SpecificSyntaxName != null)
            {
                SetResourceHighlight(m_dialect.SpecificSyntaxName, () => m_dialect.GetSyntaxDef(), false);
            }
            else
            {
                SetResourceHighlight("GenericSql", () => CoreRes.GenericSql_Mode, false);
            }
        }

        public string GetSelectedText()
        {
            return ActiveTextAreaControl.SelectionManager.SelectedText;
        }

        public string GetSelectedLine()
        {
            return Document.GetText(Document.GetLineSegment(ActiveTextAreaControl.Caret.Line));
        }

        public string GetTextBeforeCursor()
        {
            return Document.GetText(0, ActiveTextAreaControl.Caret.Offset);
        }

        public string GetTextAfterCursor()
        {
            return Document.GetText(ActiveTextAreaControl.Caret.Offset, Document.TextLength - ActiveTextAreaControl.Caret.Offset);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            m_modified = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // textAreaPanel
            // 
            this.textAreaPanel.Size = new System.Drawing.Size(338, 223);
            // 
            // CodeEditor
            // 
            this.Name = "CodeEditor";
            this.Size = new System.Drawing.Size(338, 223);
            this.ResumeLayout(false);

        }

        public void InsertTextOnCursor(string text)
        {
            Document.Insert(ActiveTextAreaControl.Caret.Offset, text);
        }

        public void ForEachLine(Func<string, string> lineFunc)
        {
            foreach (var sel in ActiveTextAreaControl.SelectionManager.SelectionCollection)
            {
                for (int line = sel.StartPosition.Line; line < sel.EndPosition.Line; line++)
                {
                    var seg = Document.GetLineSegment(line);
                    string newline = lineFunc(Document.GetText(seg));
                    Document.Replace(seg.Offset, seg.Length, newline);
                }
            }
        }

        public int GetLine()
        {
            return ActiveTextAreaControl.Caret.Line;
        }

        public int GetColumn()
        {
            return ActiveTextAreaControl.Caret.Column;
        }

        public void GoToLine(int linenum)
        {
            ActiveTextAreaControl.Caret.Line = linenum;
        }

        public void ShowFindDialog()
        {
            FindAndReplaceForm.ShowForEditor(this, false);
            //if (m_findForm == null) m_findForm = new FindAndReplaceForm();
            //m_findForm.ShowFor(this, false);
        }

        public void ShowReplaceDialog()
        {
            FindAndReplaceForm.ShowForEditor(this, true);
            //if (m_findForm == null) m_findForm = new FindAndReplaceForm();
            //m_findForm.ShowFor(this, true);
        }

        public void FindNext()
        {
            FindAndReplaceForm.FindNext(this);
        }

        public event EventHandler DatabaseConnectionNeeded;
    }
}
