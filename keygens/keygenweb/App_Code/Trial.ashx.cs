using System;
using System.Web;

public class Trial : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        try
        {
            string email = context.Request.Params["email"];
            string name = context.Request.Params["name"];
            string check = context.Request.Params["check"];
            string lang = context.Request.Params["lang"];
            string product = context.Request.Params["product"];

            if (check != "vertex5L34") throw new Exception("Bad code");
            string text = Resources.EmailTexts.trial_en;
            if (lang == "cs" || lang == "cz") text = Resources.EmailTexts.trial_cz;
            SendTool.SendLicense(name, email, email, text, product + "-eval");
            context.Response.Write("OK");
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