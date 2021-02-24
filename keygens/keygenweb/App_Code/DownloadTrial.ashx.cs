using System;
using System.Collections.Generic;
using System.Web;
using keygenlib;


public class DownloadTrial : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        try
        {
            string email = context.Request.Params["email"];
            string name = context.Request.Params["name"];
            string check = context.Request.Params["check"];
            string product = context.Request.Params["product"];

            if (check != "vertex5L34") throw new Exception("Bad code");

            LicenseData licdata = new LicenseData();
            string license = LicenseTool.CreateLicense(name, email, "name", product + "-eval", null, licdata);

            context.Response.Write(license);
        }
        catch (Exception err)
        {
            context.Response.Write(err.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
