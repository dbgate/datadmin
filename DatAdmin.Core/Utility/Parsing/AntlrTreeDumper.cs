using System;
using System.Collections.Generic;
using System.Text;
using Antlr.Runtime.Tree;

namespace DatAdmin
{
    public class AntlrTreeDumper
    {
        ITree m_tree;
        string[] m_lines;
        ISqlDumper m_dmp;

        int m_lastDumpLine;
        int m_nextDumpCharPos;

        AntlrTokens m_tokens;

        public AntlrTreeDumper(ITree tree, string sql, ISqlDumper dmp)
        {
            m_tree = tree;
            m_lines = sql.Split('\n');
            m_dmp = dmp;
            m_tokens = m_dmp.Dialect.GetAntlrTokens();
        }
        public void Run()
        {
            ProcessTree(m_tree);
        }
        private void ProcessTree(ITree tree)
        {
            int type = tree.Type;
            if (type == m_tokens.F_DEC) m_dmp.Put("&<");
            if (type == m_tokens.F_INC) m_dmp.Put("&>");
            if (type == m_tokens.F_NL) m_dmp.Put("&n");
            if (tree.Line > 0 && tree.Text != null)
            {
                if (tree.Line > m_lastDumpLine || tree.CharPositionInLine > m_nextDumpCharPos)
                {
                    m_dmp.Put("&s");
                }
                m_dmp.Put("%s", tree.Text);
                m_lastDumpLine = tree.Line;
                m_nextDumpCharPos = tree.CharPositionInLine + tree.Text.Length;
            }

            for (int i = 0; i < tree.ChildCount; i++)
            {
                var child = tree.GetChild(i);
                ProcessTree(child);
            }
        }
    }
}
