using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net.Mail;
using System.Net;
using keygenlib;
using System.IO;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbxLicense.Items.Clear();
            XmlDocument prods = LicenseTool.LoadProducts();
            foreach (XmlElement xml in prods.SelectNodes("//Product"))
            {
                ListItem item = new ListItem();
                item.Text = xml.GetAttribute("text");
                item.Value = xml.GetAttribute("name");
                cbxLicense.Items.Add(item);
            }
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (tbxCheck.Text != "vertex5L34") throw new Exception("Unauthorized");
        SendTool.SendLicense(tbxName.Text, tbxEmail.Text, String.IsNullOrEmpty(tbxSendEmail.Text) ? tbxEmail.Text : tbxSendEmail.Text, tbxEmailBody.Text, cbxLicense.SelectedValue);
        labAction.Text = String.Format("License {0} sent to {1}", cbxLicense.SelectedItem.Text, tbxEmail.Text);
    }
}
