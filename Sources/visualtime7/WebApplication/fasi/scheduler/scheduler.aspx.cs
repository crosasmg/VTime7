using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

public partial class fasi_scheduler_scheduler : System.Web.UI.Page
{
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static object GetTransactionLink()
    {
        string url = string.Empty;
        string title = string.Empty;

        try
        {
            string taskId = HttpContext.Current.Request.Params["taskId"];
            string visualTimeTransaction = HttpContext.Current.Request.Params["visualTimeTransaction"];
            string completedAction = HttpContext.Current.Request.Params["completedAction"];
            int languageId = int.Parse(HttpContext.Current.Request.Params["languageId"]);            

            if (!string.IsNullOrEmpty(visualTimeTransaction))
            {
                string parameters = string.Empty;
                List<KeyValuePair<string, string>> elements = new List<KeyValuePair<string, string>>();

                using (var client = new HttpClient())
                {                    
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Request.Headers["Authorization"].Replace("Bearer ", ""));

                    using (HttpResponseMessage response = client.GetAsync(ConfigurationManager.AppSettings["API.FASI.URL"]+ "/api/diary/v1/RetrieveTaskElementsById/" + taskId).Result)
                    {
                        if (response.IsSuccessStatusCode)
                            elements = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(response.Content.ReadAsStringAsync().Result);
                        else
                            throw new Exception("Task elements not loaded.");
                    }
                }

                // Si es un guid se procesa la url del form
                Guid guid;
                if (Guid.TryParse(visualTimeTransaction, out guid))
                {
                    // Se coloca el llamado a la función que abre la ventana del EDW
                    string name = string.Empty;

                    InMotionGIT.Workbench.Deploy.DeploySupport.GetLocalModelInformation(visualTimeTransaction, "form", languageId, ref name, ref title);

                    if (!string.IsNullOrEmpty(name))
                    {
                        foreach (var element in elements)
                            parameters += string.Format("&{0}={1}", element.Key, element.Value);

                        url = "/fasi/dli/forms/" + name + ".aspx?TaskID=" + taskId + "&Action=" + completedAction + parameters + "&Comment=y";
                    }
                    else url = string.Format("Form Id {0} not found", visualTimeTransaction);
                }
                else // VisualTime
                {
                    using (var client = new HttpClient())
                    {                        
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Current.Request.Headers["Authorization"].Replace("Bearer ", ""));

                        using (HttpResponseMessage response = client.GetAsync(ConfigurationManager.AppSettings["API.FASI.URL"] + "/api/BackOffice/v1/MakeURL?windowLogicalCode=" + visualTimeTransaction + "&schemaCode=" + HttpContext.Current.Session["sSche_Code"] + "&companyId=" + HttpContext.Current.Session["CompanyId"]).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var result = response.Content.ReadAsStringAsync().Result;
                                dynamic resultMethod = JsonConvert.DeserializeObject(result);

                                foreach (var element in elements)
                                    parameters += string.Format("&lnk{0}={1}", element.Key, element.Value);

                                url = ConfigurationManager.AppSettings["Url.BackOffice"] + resultMethod.Data.Url + "&LinkFront=1&TaskID=" + taskId + parameters;
                                title = resultMethod.Data.Description;
                            }
                            else throw new Exception("Url not loaded.");
                        }
                    }
                }
            }            
        }
        catch (Exception)
        {
            title = string.Empty;
            url = string.Empty;
        }
        // TODO: tratar errores
        return new { Url = url, Title = title };
    }
}