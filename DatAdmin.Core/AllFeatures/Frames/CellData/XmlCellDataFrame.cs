using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml.XPath;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public partial class XmlCellDataFrame : CellDataFrameBase
    {
        public XmlCellDataFrame()
        {
            InitializeComponent();
        }

        public override void ShowCurrentData()
        {
            try
            {
                string sval = GetStringValue();
                if (String.IsNullOrEmpty(sval) || !sval.StartsWith("<"))
                {
                    xPathNavigatorTreeView1.Navigator = null;
                    Controls.ShowError(true);
                }
                else
                {
                    try
                    {
                        xPathNavigatorTreeView1.ForceHideRoot = false;
                        xPathNavigatorTreeView1.Navigator = new XPathDocument(new StringReader(sval)).CreateNavigator();
                    }
                    catch
                    {
                        xPathNavigatorTreeView1.ForceHideRoot = true;
                        xPathNavigatorTreeView1.Navigator = new XPathDocument(new StringReader("<ROOT>" + sval + "</ROOT>")).CreateNavigator();
                    }
                    Controls.ShowError(false);
                }
            }
            catch
            {
                xPathNavigatorTreeView1.Navigator = null;
                Controls.ShowError(true);
            }
        }
    }

    [CellDataEditor(Name = "xml", Title = "Xml")]
    public class XmlCellDataEditor : CellDataEditorBase
    {
        public override string MenuTitle
        {
            get { return "XML"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.xml; }
        }

        protected override CellDataFrameBase CreateControl()
        {
            return new XmlCellDataFrame();
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockRight; }
        }

        public override int SupportLevel(IDataHolder data, IBedValueReader holder)
        {
            string xmlval = CellDataFrameBase.GetStringValue(holder);
            if (String.IsNullOrEmpty(xmlval)) return 0;
            if (!xmlval.StartsWith("<")) return 0;
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml("<ROOT>" + xmlval + "</ROOT>");
                return 10;
            }
            catch
            {
                return 0;
            }
        }
    }
}
