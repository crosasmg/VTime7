using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web.Script.Services;
using System.Web.Services;

public partial class fasi_dli_forms_autologin : System.Web.UI.Page
{
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Object TokenGenerate(String username, String password)
    {
        var urlAcction = "http://34.228.171.165:8082/FrontOffice/UserService.svc/rest/user/TokenGenerate?email=" + username + "&password=" + password + "&companyid=1";
        var client = new HttpClient();

        var Response = client.GetAsync(urlAcction).Result;
		Rootobject resultInformation = null;
        if (Response.IsSuccessStatusCode)
        {
            String body = Response.Content.ReadAsStringAsync().Result;
            resultInformation = JsonConvert.DeserializeObject<Rootobject>(body);

            if (resultInformation != null)
            {

            }
        }

        dynamic result = "";
        return resultInformation;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}


public class Rootobject
{
    public DateTime ExpirateDate { get; set; }
    public bool IsValid { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
}
