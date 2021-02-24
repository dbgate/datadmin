using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using keygenlib;

public partial class TestTrial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbxProduct.Items.Clear();
            foreach (XmlElement xml in LicenseTool.LoadProducts().SelectNodes("//Product"))
            {
                string name = xml.GetAttribute("name");
                if (name.EndsWith("-eval"))
                {
                    ListItem li = new ListItem();
                    li.Text = xml.GetAttribute("text");
                    li.Value = name.Substring(0, name.Length - 5);
                    cbxProduct.Items.Add(li);
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("Trial.ashx?email={0}&name={1}&check={2}&lang={3}&product={4}",
            Server.UrlEncode(tbxEmail.Text),
            Server.UrlEncode(tbxName.Text),
            tbxCheck.Text,
            cbxLanguage.SelectedValue,
            cbxProduct.SelectedValue));
    }
}
