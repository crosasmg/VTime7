<%@ WebHandler Language="C#" Class="download" %>

using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using InMotionGIT.Common.Extensions;
using System.IO.Compression;

public class download : IHttpHandler

{

    public void ProcessRequest(HttpContext context)
    {
        string filename = context.Request.QueryString["File"];
        string directory = context.Request.QueryString["Directory"];

        if (!string.IsNullOrEmpty(directory))
            filename = "~/" + directory + "/" + filename;

        if (context.Request.QueryString["path"].IsEmpty())
        {
            //Validate the file name and make sure it is one that the user may access
            context.Response.Buffer = true;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            context.Response.ContentType = "octet/stream";

            context.Response.WriteFile(filename);
        }else
        {
            bool IsFolder =bool.Parse(context.Request.QueryString["IsFolder"]);
            if (IsFolder)
            {
                string startPath = InMotionGIT.Common.Helpers.CryptSupportNew.DecryptString( context.Request.QueryString["path"]);
                string zipPath = System.IO.Path.GetTempPath()+ System.IO.Path.GetFileName(startPath) + ".zip";
                var zip = new Ionic.Zip.ZipFile();
                zip.AddDirectory(startPath);
                zip.Save(zipPath);
                filename = zipPath;
               
            }
            else
            {
                filename =InMotionGIT.Common.Helpers.CryptSupportNew.DecryptString( context.Request.QueryString["path"]);
            }

            context.Response.Buffer = true;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=" + System.IO.Path.GetFileName(filename));
            context.Response.ContentType = "octet/stream";

            context.Response.WriteFile(filename);

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
