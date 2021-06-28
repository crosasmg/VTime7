using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class fasi_widgets_RxListNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static Dictionary<string, string> LoadNews(int limit)
    {
        limit = limit == 0 ? 10 : limit;
        int count = 0;
        Dictionary<string, string> result = new Dictionary<string, string>();
        string url = "https://www.rxlist.com/rss/rxlist_news.xml";
        XmlTextReader reader = new XmlTextReader(url);
        reader.WhitespaceHandling = WhitespaceHandling.None;
        while (reader.ReadToFollowing("item"))
        {
            if (count == limit)
                break;
            reader.ReadToFollowing("title");
            reader.Read();
            string title = reader.Value.ToString();
            reader.ReadToFollowing("link");
            reader.Read();
            string link = reader.Value.ToString();
            result.Add(title, link);
            count++;
        }
        return result;
    }
}