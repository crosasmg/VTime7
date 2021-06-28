using InMotionGIT.Common.Proxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fasi_utils_logs2 : System.Web.UI.Page
{

    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]

    [WebMethod(EnableSession = true)]
    public static object test()
    {

        return new List<object>
        {
            new
            {
                ID = 1,
                CompanyName = "Super Mart of the West",
                Address = "702 SW 8th Street",
                City = "Bentonville",
                State = "Arkansas",
                Zipcode = 72716,
                Phone = "(800) 555-2797",
                Fax = "(800) 555-2171",
                Website = "http=//www.nowebsitesupermart.com"
            }
        };

    }

    [WebMethod(EnableSession = true)]
    public static object test2()
    {
        DataTable result = null;

        using (DataManagerFactory dbFactory = new DataManagerFactory("SELECT EFFECTDATE, IPADDRESS, USERSSECURITYTRACE.EMAIL,  USERSSECURITYTRACE.STATE, LOOKUP.DESCRIPTION FROM USERSSECURITYTRACE LEFT JOIN USERMEMBER ON USERMEMBER.EMAIL= USERSSECURITYTRACE.EMAIL LEFT JOIN LOOKUP LOOKUP ON LOOKUP.CODE = USERSSECURITYTRACE.STATE AND  LOOKUP.LOOKUPID = 8 AND LOOKUP.LANGUAGEID = 2 WHERE TRUNC(USERSSECURITYTRACE.EFFECTDATE) > (SYSDATE - 3) ORDER BY EFFECTDATE DESC", 
                                                                     "WorkflowInstance", "Linked.FrontOffice"))
        {
            result = dbFactory.QueryExecuteToTable(true);
        }
        return JsonConvert.SerializeObject(result);
    }

}