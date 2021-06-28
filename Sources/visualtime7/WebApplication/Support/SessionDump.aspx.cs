using InMotionGIT.Common.Extensions;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public partial class SessionDump : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (Request.QueryString["Parameters"].IsNotEmpty())
        {
            Boolean state = Boolean.Parse(Request.QueryString["Parameters"]);
            Session["Form.Track.Parameters"] = state;
        }

        if (Session["Form.Track.Parameters"].IsNotEmpty() && Session["Form.Track.Parameters"].ToString().ToLower().Equals("true"))
        {
            hplSeccionTraceParameter.NavigateUrl = "SessionDump.aspx?Parameters=false";
            hplSeccionTraceParameter.Text = "Deshabilitar - Tracking en disco para los parametros de las planillas";
        }
        else
        {
            hplSeccionTraceParameter.NavigateUrl = "SessionDump.aspx?Parameters=true";
            hplSeccionTraceParameter.Text = "Habilitar - Tracking en disco para los parametros de las planillas";
        }


        if (Request.QueryString["Track"].IsNotEmpty())
        {
            Boolean state = Boolean.Parse(Request.QueryString["Track"]);
            Session["Form.Track"] = state;
        }

        if (Session["Form.Track"].IsNotEmpty() && Session["Form.Track"].ToString().ToLower().Equals("true"))
        {
            hplSeccionTrace.NavigateUrl = "SessionDump.aspx?Track=false";
            hplSeccionTrace.Text = "Deshabilitar - Tracking para las acciones de las planillas";
        }
        else
        {
            hplSeccionTrace.NavigateUrl = "SessionDump.aspx?Track=true";
            hplSeccionTrace.Text = "Habilitar - Tracking para las acciones de las planillas";
        }

        bool IsAdministratorKey = false;
        if (!IsPostBack)
        {
            if (Request.QueryString.IsNotEmpty() && Request.QueryString["Key"].IsNotEmpty() &&
                InMotionGIT.Common.Helpers.KeyValidator.KeyValidator(Request.QueryString["Key"]))
            {
                IsAdministratorKey = true;
            }

            long sessionSize = 0;

            StringBuilder text = new StringBuilder();
            text.Append("<table>");

            string item = Request.QueryString["item"];
            if (!string.IsNullOrEmpty(item))
            {
                h1Title.InnerText += string.Format(" - {0}", item);
                pFullSessionLink.Visible = true;

                text.Append(ParseObjectType(Session[item], item));
                sessionSize += GetSizeOfObject(Session[item]);
            }
            else
            {
                for (int i = 0; i < Session.Count; i++)
                {
                    if (IsAdministratorKey)
                    {
                        string itemName = string.Format("<a href='?item={0}'>{0}</a>", Session.Keys[i]);
                        text.Append(ParseObjectType(Session[i], itemName));
                        sessionSize += GetSizeOfObject(Session[i]);
                    }
                    else
                    {
                        if (Session.Keys[i].ToLower().StartsWith("form."))
                        {
                            string itemName = string.Format("<a href='?item={0}'>{0}</a>", Session.Keys[i]);
                            text.Append(ParseObjectType(Session[i], itemName));
                            sessionSize += GetSizeOfObject(Session[i]);
                        }
                    }
                }
            }

            text.Append("</table>");

            lblEstimatedSize.Text = string.Format("{0} KB", Math.Round((decimal)sessionSize / 1024M, 2, MidpointRounding.AwayFromZero));

            divContainer.InnerHtml = text.ToString();
        }
    }

    private string AddItem(string key, object item)
    {
        return string.Format("<tr><td><strong>{0}</strong></td><td>{1}</td></tr>", key, item);
    }

    private string ParseObjectType(object item, string name)
    {
        if (item == null)
        {
            return AddItem(name, "[null]");
        }

        Type type = item.GetType();
        if (type == typeof(string) || type == typeof(bool) ||
            type == typeof(byte) || type == typeof(char) ||
            type == typeof(decimal) || type == typeof(double) ||
            type == typeof(float) || type == typeof(int) ||
            type == typeof(long) || type == typeof(sbyte) ||
            type == typeof(short) || type == typeof(uint) ||
            type == typeof(ulong) || type == typeof(ushort) ||
            type == typeof(DateTime))
        {
            return AddItem(name, item);
        }
        else
        {
            long itemSize = GetSizeOfObject(item);
            string itemSizeValue = string.Empty;
            if (itemSize > 0)
            {
                if (itemSize > 1024)
                {
                    itemSizeValue = string.Format("<br><small>{0} KB</small>", Math.Round((decimal)itemSize / 1024M, 2, MidpointRounding.AwayFromZero));
                }
                else
                {
                    itemSizeValue = string.Format("<br><small>{0} B</small>", itemSize);
                }
            }

            return string.Format("<tr><td><strong>{0}</strong>{1}</td><td><table>{2}</table></td></tr>",
                name, itemSizeValue, ParseObject(item));
        }
    }

    private string ParseObject(object item)
    {
        StringBuilder text = new StringBuilder();

        if (item is IEnumerable)
        {
            IEnumerable itemList = (IEnumerable)item;
            int itemNum = 1;
            foreach (object obj in itemList)
            {
                text.Append(ParseObjectType(obj, string.Format("Item {0}", itemNum)));
                itemNum++;
            }
        }
        else
        {
            foreach (PropertyInfo property in item.GetType().GetProperties())
            {
                object value;
                try
                {
                    value = property.GetValue(item, null);
                }
                catch
                {
                    value = string.Empty;
                }

                text.Append(ParseObjectType(value, property.Name));
            }
        }

        return text.ToString();
    }

    private long GetSizeOfObject(object item)
    {
        try
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, item);

                return stream.Length;
            }
        }
        catch
        {
            return 0;
        }
    }
}