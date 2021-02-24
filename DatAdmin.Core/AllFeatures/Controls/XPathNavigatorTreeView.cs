using System;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using System.Xml.XPath;
using System.IO;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DatAdmin
{
    public class XPathNavigatorTreeView : TreeView
    {
        private StringFormat m_stringFormat = new StringFormat(StringFormat.GenericTypographic);
        private bool m_useSyntaxHighlighting = true;
        private bool m_autoExpand = true;

        #region Variables

        /// <summary>
        /// The XPathNavigator that represents the root of the tree.
        /// </summary>
        protected XPathNavigator _navigator;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new XPathNavigatorTreeView.
        /// </summary>
        public XPathNavigatorTreeView()
        {
            // I always end up forgetting to turn this off each time I use a TreeView,
            // so I'll just take care of it here automatically.
            base.HideSelection = false;
            base.DrawMode = TreeViewDrawMode.OwnerDrawText;

            m_stringFormat.FormatFlags |= StringFormatFlags.FitBlackBox | StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
        }

        #endregion

        #region Properties

        public bool ForceHideRoot { get; set; }
        public bool AutoExpand
        {
            get { return m_autoExpand; }
            set { m_autoExpand = value; }
        }

        public XPathNavigator Navigator
        {
            get
            {
                return _navigator;
            }

            set
            {
                _navigator = value;
                this.Initialize();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the initial xml tree node.
        /// </summary>
        protected virtual void Initialize()
        {
            XPathNavigatorTreeNode node = null;

            // suspend drawing of the tree while loading nodes to improve performance
            // as adding each node would normally require an entire redraw of the tree
            base.BeginUpdate();
            try
            {
                // clear all the nodes in the tree
                base.Nodes.Clear();

                // setting the navigator to null should just clear the tree
                // no exception needs thrown
                if (_navigator != null)
                {
                    // if it's the root node of the document, load it's children
                    // we don't display the root
                    if (_navigator.NodeType == XPathNodeType.Root)
                    {
                        if (ForceHideRoot)
                        {
                            foreach (XPathNavigator node2 in _navigator.SelectChildren(XPathNodeType.Element))
                            {
                                this.LoadNodes(node2.SelectChildren(XPathNodeType.Element), base.Nodes);
                            }
                        }
                        else
                        {
                            this.LoadNodes(_navigator.SelectChildren(XPathNodeType.Element), base.Nodes);
                        }
                    }
                    else
                    {
                        // otherwise, create a tree node and load it
                        node = new XPathNavigatorTreeNode(_navigator);
                        base.Nodes.Add(node);
                    }
                }
                if (AutoExpand) ExpandAll();
            }
            finally
            {
                // resume drawing of the tree
                base.EndUpdate();
                if (node != null)
                {
                    node.Expand();
                }
            }
        }

        /// <summary>
        /// Handles the expanding of an XPathNavigatorTreeNode.
        /// </summary>
        /// <param name="node"></param>
        protected virtual void ExpandNode(XPathNavigatorTreeNode treeNode)
        {
            if (treeNode == null)
                return;

            // for better performance opening large files, tree nodes are loaded on demand.
            // return if the node has already been expanded and loaded
            if (treeNode.HasExpanded)
                return;

            treeNode.HasExpanded = true;

            base.BeginUpdate();
            try
            {
                // load the child nodes of the specified xml tree node
                this.LoadNodes(treeNode.Navigator.SelectChildren(XPathNodeType.All), treeNode.Nodes);
            }
            finally
            {
                base.EndUpdate();
            }
        }
        
        /// <summary>
        /// Loads an XPathNavigatorTreeNode for each XPathNavigator in the specified XPathNodeIterator, into the 
        /// specified TreeNodeCollection.
        /// </summary>
        /// <param name="iterator"></param>
        /// <param name="treeNodeCollection"></param>
        public virtual void LoadNodes(XPathNodeIterator iterator, TreeNodeCollection treeNodeCollection)
        {
            // handle null arguments
            if (iterator == null)
                throw new ArgumentNullException("navigator");

            if (treeNodeCollection == null)
                throw new ArgumentNullException("parentNodeCollection");

            // use the wait cursor, in case this takes a while
            this.UseWaitCursor = true;

            try
            {
                treeNodeCollection.Clear();

                // create and add a node for each navigator
		foreach (XPathNavigator navigator in iterator)
		{
			XPathNavigatorTreeNode node = new XPathNavigatorTreeNode(navigator.Clone());
			treeNodeCollection.Add(node);
		}
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        /// <summary>
        /// Loads the specified XML data into the tree.
        /// </summary>
        /// <param name="xml">XML string data to load.</param>
        public void LoadXml(string xml)
        {
            // create a StringReader around the XML data string
            using (StringReader reader = new StringReader(xml))
            {
                // create an XPathDocument around the StringReader
                XPathDocument document = new XPathDocument(reader);

                // load the navigator
                this.Navigator = document.CreateNavigator();
            }
        }

        /// <summary>
        /// Loads the specified XML file into the tree.
        /// </summary>
        /// <param name="filename">The full path of an XML data file to load.</param>
        public void Open(string filename)
        {
            XPathDocument document = new XPathDocument(filename);
            this.Navigator = document.CreateNavigator();
        }

        /// <summary>
        /// Finds and selects an XPathNavigatorTreeNode for the given XPathNavigator.
        /// </summary>
        /// <param name="navigator">An XPathNavigator to find and select in the tree.</param>
        /// <returns></returns>
        private bool SelectXmlTreeNode(XPathNavigator navigator)
        {
            if (navigator == null)
                throw new ArgumentNullException("navigator");

            // we've found a node, so build an ancestor stack
            Stack<XPathNavigator> ancestors = this.GetAncestors(navigator);

            // now find treenodes to match the ancestors
            TreeNode treeNode = this.GetXmlTreeNode(ancestors);

            if (treeNode == null)
                return false;

            // select the node
            this.SelectedNode = treeNode;

            return true;
        }

        /// <summary>
        /// Finds a TreeNode for a given stack of XPathNavigators.
        /// </summary>
        /// <param name="ancestors">A Stack of XPathNavigators with which to find a TreeNode.</param>
        /// <returns></returns>
        private TreeNode GetXmlTreeNode(Stack<XPathNavigator> ancestors)
        {
            XPathNavigator navigator = null;

            // start at the root
            TreeNodeCollection nodes = this.Nodes;

            TreeNode treeNode = null;

            // loop through the ancestor XPathNavigators
            while (ancestors.Count > 0 && (navigator = ancestors.Pop()) != null)
            {
                // loop through the TreeNodes at the current level
                foreach (TreeNode node in nodes)
                {
                    // make sure it's an XPathNavigatorTreeNode
                    XPathNavigatorTreeNode xmlTreeNode = node as XPathNavigatorTreeNode;
                    if (xmlTreeNode == null)
                        continue;

                    // check to see if we've found the correct TreeNode
                    if (xmlTreeNode.Navigator.IsSamePosition(navigator))
                    {
                        // expand the tree node, if it hasn't alreay been expanded
                        if (!node.IsExpanded)
                            node.Expand();

                        // we've taken another step towards the target node
                        treeNode = node;

                        // update the current level
                        nodes = node.Nodes;

                        // handle the next level, if any
                        break;
                    }
                }
            }

            // return the result, if any was found
            return treeNode;
        }

        /// <summary>
        /// Builds and returns a Stack of XPathNavigator ancestors for a given XPathNavigator.
        /// </summary>
        /// <param name="navigator">The XPathNavigator from which to build a stack.</param>
        /// <returns></returns>
        private Stack<XPathNavigator> GetAncestors(XPathNavigator navigator)
        {
            if (navigator == null)
                throw new ArgumentNullException("navigator");

            Stack<XPathNavigator> ancestors = new Stack<XPathNavigator>();

            // navigate up the xml tree, building the stack as we go
            while (navigator != null)
            {
                // push the current ancestor onto the stack
                ancestors.Push(navigator);

                // clone the current navigator cursor, so we don't lose our place
                navigator = navigator.Clone();

                // if we've reached the top, we're done
                if (!navigator.MoveToParent())
                    break;

                // if we've reached the root, we're done
                if (navigator.NodeType == XPathNodeType.Root)
                    break;
            }

            // return the result
            return ancestors;
        }

        /// <summary>
        /// Evaluates the XPath expression and returns the typed result.
        /// </summary>
        /// <param name="xpath">A string representing an XPath expression that can be evaluated.</param>
        /// <returns></returns>
        public object SelectXmlNodes(string xpath)
        {
            // get the selected node
            XPathNavigatorTreeNode node = this.SelectedNode as XPathNavigatorTreeNode;

            // if there is no selected node, default to the root node
            if (node == null && this.Nodes.Count > 0)
                node = this.GetRootXmlTreeNode();

            if (node == null)
                return null;

            // evaluate the expression, return the result
            return node.Navigator.Evaluate(xpath);
        }

        /// <summary>
        /// Returns the root XPathNavigatorTreeNode in the tree.
        /// </summary>
        /// <returns></returns>
        private XPathNavigatorTreeNode GetRootXmlTreeNode()
        {
            foreach (TreeNode node in this.Nodes)
            {
                XPathNavigatorTreeNode xmlTreeNode = node as XPathNavigatorTreeNode;

                if (xmlTreeNode == null || xmlTreeNode.Navigator == null)
                    continue;

                if (xmlTreeNode.Navigator.NodeType != XPathNodeType.Element)
                    continue;

                return xmlTreeNode;
            }

            return this.Nodes[0] as XPathNavigatorTreeNode;
        }

        /// <summary>
        /// Finds and selects an XPathNavigatorTreeNode using an XPath expression.
        /// </summary>
        /// <param name="xpath">An XPath expression.</param>
        /// <returns></returns>
        public bool FindByXpath(string xpath)
        {
            // evaluate the expression
            object result = this.SelectXmlNodes(xpath);

            if (result == null)
                return false;

            // did the expression evaluate to a node set?
            XPathNodeIterator iterator = result as XPathNodeIterator;

            if (iterator != null)
            {
                // the expression evaluated to a node set
                if (iterator == null || iterator.Count < 1)
                    return false;

                if (!iterator.MoveNext())
                    return false;

                // select the first node in the set
                return this.SelectXmlTreeNode(iterator.Current);
            }
            else
            {
                StdDialog.ShowInfo("XPath result:" + result.ToString());
                return true;
            }
        }

        /// <summary>
        /// Returns a string representing the full path of an XPathNavigator.
        /// </summary>
        /// <param name="navigator">An XPathNavigator.</param>
        /// <returns></returns>
        public string GetXmlNodeFullPath(XPathNavigator navigator)
        {
            // create a StringBuilder for assembling the path
            StringBuilder sb = new StringBuilder();

            // clone the navigator (cursor), so the node doesn't lose it's place
            navigator = navigator.Clone();

            // traverse the navigator's ancestry all the way to the top
            while (navigator != null)
            {
                // skip anything but elements
                if (navigator.NodeType == XPathNodeType.Element)
                {
                    // insert the node and a seperator
                    sb.Insert(0, navigator.Name);
                    sb.Insert(0, "/");
                }
                if (!navigator.MoveToParent())
                    break;
            }

            return sb.ToString();
        }

        private bool DrawStrings(string text, Rectangle bounds, Graphics graphics)
        {
            bool flag = false;
            Color controlText = Color.FromArgb(0, 0, 0xff);
            flag = this.DrawStrings(text, bounds, graphics, controlText, RegularExpressions.Xml, "Delimiter") | this.DrawStrings(text, bounds, graphics, controlText, RegularExpressions.Xml, "AttributeValue");
            controlText = SystemColors.ControlText;
            flag |= this.DrawStrings(text, bounds, graphics, controlText, RegularExpressions.Xml, "Text");
            controlText = Color.FromArgb(0xa3, 0x15, 0x15);
            flag |= this.DrawStrings(text, bounds, graphics, controlText, RegularExpressions.Xml, "Name");
            controlText = Color.FromArgb(0xff, 0, 0);
            flag |= this.DrawStrings(text, bounds, graphics, controlText, RegularExpressions.Xml, "AttributeName");
            controlText = Color.FromArgb(0, 0, 0xff);
            flag |= this.DrawStrings(text, bounds, graphics, controlText, RegularExpressions.Comments, "Delimiter");
            controlText = Color.FromArgb(0, 0x80, 0);
            return (flag | this.DrawStrings(text, bounds, graphics, controlText, RegularExpressions.Comments, "Comments"));
        }

        private bool DrawStrings(string text, Rectangle bounds, Graphics graphics, Color color, Regex regex, string groupName)
        {
            if (!regex.IsMatch(text))
            {
                return false;
            }
            MatchCollection matchs = regex.Matches(text);
            bool flag = false;
            foreach (Match match in matchs)
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    foreach (Capture capture in match.Groups[groupName].Captures)
                    {
                        CharacterRange[] ranges = new CharacterRange[] { new CharacterRange(capture.Index, capture.Length) };
                        StringFormat stringFormat = new StringFormat(this.m_stringFormat);
                        stringFormat.SetMeasurableCharacterRanges(ranges);
                        Region[] regionArray = new Region[1];
                        foreach (Region region in graphics.MeasureCharacterRanges(text, this.Font, bounds, stringFormat))
                        {
                            RectangleF layoutRectangle = region.GetBounds(graphics);
                            graphics.DrawString(capture.Value, this.Font, brush, layoutRectangle, m_stringFormat);
                            flag = true;
                        }
                    }
                    continue;
                }
            }
            return flag;
        }

        #endregion

        #region Overrides

        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            // remove the end tag we inserted when the node was expanded
            TreeNode node = e.Node.Tag as TreeNode;
            if (node != null)
            {
                // remove it
                TreeNodeCollection nodes = base.Nodes;
                if (node.Parent != null)
                {
                    nodes = node.Parent.Nodes;
                }
                nodes.Remove(node);
            }

            base.OnBeforeCollapse(e);
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            XPathNavigatorTreeNode node = e.Node as XPathNavigatorTreeNode;

            // expand the node, adding any child xml tree nodes
            this.ExpandNode(node);

            if (node != null)
            {
                if (node.Nodes.Count > 0)
                {
                    TreeNodeCollection nodes = base.Nodes;
                    if (node.Parent != null)
                    {
                        nodes = node.Parent.Nodes;
                    }

                    // add a node for the xml end tag, such as </node>
                    node.Tag = nodes.Insert(e.Node.Index + 1, string.Format("</{0}>", node.Navigator.Name));
                }
            }

            base.OnBeforeExpand(e);
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            try
            {
                if (!m_useSyntaxHighlighting)
                {
                    base.OnDrawNode(e);
                }
                else if (e.Node.IsVisible)
                {
                    Rectangle bounds = e.Bounds;
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                    {
                        e.Graphics.FillRectangle(brush, bounds);
                    }
                    if (!e.Node.IsEditing)
                    {
                        string text = e.Node.Text;
                        if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
                        {
                            e.Graphics.FillRectangle(SystemBrushes.Highlight, bounds);
                            e.Graphics.DrawString(text, this.Font, SystemBrushes.HighlightText, bounds, m_stringFormat);
                        }
                        else
                        {
                            bool flag = false;
                            XPathNavigatorTreeNode node = e.Node as XPathNavigatorTreeNode;
                            if ((node == null) || (node.Navigator.NodeType != XPathNodeType.Text))
                            {
                                flag = this.DrawStrings(text, bounds, e.Graphics);
                            }
                            if (!flag)
                            {
                                using (SolidBrush brush2 = new SolidBrush(this.ForeColor))
                                {
                                    e.Graphics.DrawString(text, this.Font, brush2, bounds, m_stringFormat);
                                }
                            }
                            if ((e.State & TreeNodeStates.Focused) != 0)
                            {
                                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        public static class RegularExpressions
        {
            public static Regex Comments = new Regex(@"(?<Delimiter><!--)\s*(?<Comments>.*)\s*(?<Delimiter>-->)", RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            public static Regex Xml = new Regex("(?<Delimiter></?|<\\?)\\s*(?<Name>\\S+)\\s*(?<Attribute>(?<AttributeName>\\S+)(?<Delimiter>=)(?<Text>\")(?<AttributeValue>.*?)(?<Text>\")\\s*)*?(?<Delimiter>/?>|\\?>)", RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }
}

