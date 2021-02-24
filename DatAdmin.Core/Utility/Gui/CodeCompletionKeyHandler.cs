using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using System.IO;

namespace DatAdmin
{
    public class CodeCompletionKeyHandler
    {
        Form m_parentForm;
        CodeEditor m_editor;
        CodeCompletionWindow m_codeCompletionWindow;
        IDatabaseSource m_conn;
        bool m_handlingCtrlSpace;
        string m_lastText;
        string m_lastAnalysedText;
        SqlEditorAnalyser m_anal;

        private CodeCompletionKeyHandler(Form parentForm, CodeEditor editor)
        {
            m_parentForm = parentForm;
            m_editor = editor;
        }

        public static CodeCompletionKeyHandler Attach(Form parentForm, CodeEditor editor)
        {
            CodeCompletionKeyHandler h = new CodeCompletionKeyHandler(parentForm, editor);

            editor.ActiveTextAreaControl.TextArea.KeyEventHandler += h.TextAreaKeyEventHandler;
            editor.ActiveTextAreaControl.TextArea.KeyDown += h.TextArea_KeyDown;
            editor.Document.DocumentChanged += h.Document_DocumentChanged;

            // When the editor is disposed, close the code completion window
            editor.Disposed += h.CloseCodeCompletionWindow;

            return h;
        }

        void Document_DocumentChanged(object sender, ICSharpCode.TextEditor.Document.DocumentEventArgs e)
        {
            InvalidateCompletion();
        }

        public void InvalidateCompletion()
        {
            m_editor.CallDatabaseConnectionNeeded();
            InvalidateCompletion(m_editor.Connection);
        }

        void TextArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && e.Control)
            {
                // show code completion
                e.SuppressKeyPress = true;
                m_handlingCtrlSpace = true;
                try
                {
                    ShowCompletationWindow('\0');
                }
                finally
                {
                    m_handlingCtrlSpace = false;
                }
                //m_editor.CallOnKeyDown(e);
            }
            if (e.KeyCode == Keys.J && e.Control)
            {
                // insert join
                ShowInsertJoin();
            }
        }

        public void ShowCompletationWindow()
        {
            ShowCompletationWindow('\0');
        }

        public bool CompletationEnabled
        {
            get { return m_anal != null; }
        }

        void ShowCompletationWindow(char key)
        {
            var anal = m_anal;
            if (anal == null) return;

            ICompletionDataProvider completionDataProvider = new CodeCompletionProvider(m_parentForm, m_editor.Connection, anal, m_editor.ImageCache, key, m_editor.ActiveTextAreaControl, m_editor.Connection.Settings.CodeCompletion());

            m_codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(
                m_parentForm,					// The parent window for the completion window
                m_editor, 					// The text editor to show the window for
                "",		// Filename - will be passed back to the provider
                completionDataProvider,		// Provider to get the list of possible completions
                key							// Key pressed - will be passed to the provider
            );
            if (m_codeCompletionWindow != null)
            {
                // ShowCompletionWindow can return null when the provider returns an empty list
                m_codeCompletionWindow.Closed += new EventHandler(CloseCodeCompletionWindow);
            }
        }

        /// <summary>
        /// Return true to handle the keypress, return false to let the text area handle the keypress
        /// </summary>
        bool TextAreaKeyEventHandler(char key)
        {
            if (m_handlingCtrlSpace) return true;
            if (m_codeCompletionWindow != null)
            {
                // If completion window is open and wants to handle the key, don't let the text area
                // handle it
                if (m_codeCompletionWindow.ProcessKeyEvent(key))
                    return true;
            }
            if (key == '.')
            {
                ShowCompletationWindow(key);
            }
            else
            {
                //if (m_codeCompletionWindow == null && (Char.IsLetterOrDigit(key) || key == '_'))
                //{
                //    ShowCompletationWindow('\0');
                //}
            }
            return false;
        }

        private bool EqualConnections(IDatabaseSource conn1, IDatabaseSource conn2)
        {
            if (conn1.Connection.PhysicalFactory.GetConnectionKey() != conn2.Connection.PhysicalFactory.GetConnectionKey()) return false;
            if (conn1.DatabaseName != conn2.DatabaseName) return false;
            return true;
        }


        private void InvalidateCompletion(IDatabaseSource conn)
        {
            if (m_conn != null && !EqualConnections(m_conn, conn))
            {
                m_conn.Connection.BeginClose(EmptyCallback);
                m_conn = null;
            }
            if (m_conn == null && conn != null && conn.Settings.CodeCompletion().UseCompletion)
            {
                m_conn = conn.CloneSource();
                var cpack = new CachePack
                {
                    ReuseAnalyserCache = true
                };
                m_conn.Connection.Cache = cpack.Connection(m_conn.Connection.GetConnKey());
                m_conn.Connection.BeginOpen(EmptyCallback);
            }
            if (m_conn != null)
            {
                m_lastText = m_editor.CodeText;
                m_conn.Connection.BeginInvoke((Action)AnalyseLastText, EmptyCallback);
            }
        }

        private void AnalyseLastText()
        {
            if (m_lastAnalysedText == m_lastText) return;
            string text = m_lastText;
            var dialect = m_editor.Connection.Dialect;
            var strm = dialect.GetAntlrTokenStream(new StringReader(text));
            var anal = new SqlEditorAnalyser(strm, dialect);
            if (strm == null) return;
            if (!anal.ParseInput()) return;
            if (!anal.LoadCatalogs(m_conn)) return;
            m_lastAnalysedText = text;
            m_anal = anal;
        }

        private void EmptyCallback(IAsyncResult async)
        {
            try
            {
                ((IStandaloneAsyncResult)async).EndInvoke();
            }
            catch (Exception err)
            {
                Errors.LogError(err);
            }
        }

        void CloseCodeCompletionWindow(object sender, EventArgs e)
        {
            if (m_codeCompletionWindow != null)
            {
                m_codeCompletionWindow.Closed -= new EventHandler(CloseCodeCompletionWindow);
                m_codeCompletionWindow.Dispose();
                m_codeCompletionWindow = null;
            }
            if (m_conn != null)
            {
                m_conn.Connection.BeginClose(EmptyCallback);
                m_conn = null;
            }
        }

        public void ShowInsertJoin()
        {
            var anal = m_anal;
            if (anal != null && anal.UsedTables.Count > 0)
            {
                m_editor.CallDatabaseConnectionNeeded();
                InsertJoinForm.Run(anal, m_editor, m_editor.Connection.Settings.CodeCompletion());
            }
        }
    }
}
