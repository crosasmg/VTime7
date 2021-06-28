
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using Dropthings.Widget.Widgets;
using Dropthings.Widget.Framework;

namespace Dropthings.Widgets
{

    public partial class HoroscopeWidgetUserControl : System.Web.UI.UserControl, IWidget
    {
        private string rssLocation = "http://feeds.astrology.com/dailyextended";
        private IWidgetHost _Host;
        private string[] strHoroscope;
        public IWidgetHost Host
        {
            get { return _Host; }
            set { _Host = value; }
        }
        public void SetHoroscopeData()
        {
            strHoroscope = new string[] { "March 21 - April 19", " May 21 - June 21", "April 20 - May 20", "June 22 - July 22", "July 23 - August 22", "August 23 - September 22", "September 23 - October 22", "October 23 - November 21", "November 22 - December 21", "December 22 - January 19", "January 20 - February 18", "February 19 - March 20" };
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if( this.Host.IsFirstLoad )
            {
                HoroscopeLabel.Text=GetHoroscope();
            }*/
            if (!this._Host.IsFirstLoad) this.LoadContentView(sender, e);
        }

        protected void LoadContentView(object sender, EventArgs e)
        {
            this.Multiview.ActiveViewIndex = 1;
            this.MultiviewTimer.Enabled = false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!this._Host.IsFirstLoad) HoroscopeLabel.Text = GetHoroscope();
        }

        void IWidget.Init(IWidgetHost host)
        {
            this.Host = host;
        }

        void IWidget.ShowSettings()
        {

        }
        void IWidget.HideSettings()
        {

        }
        void IWidget.Minimized()
        {
        }
        void IWidget.Maximized()
        {
        }
        void IWidget.Closed()
        {
        }

        public string GetHoroscope()
        {
            SetHoroscopeData();
            string imgName = Horoscope.SelectedItem.Text.ToLower() + ".gif";
            string data = "";
            data = "<img src='../../images/dropthings/Horoscope_image/" + imgName + "'/><br/><b> " + strHoroscope[Horoscope.SelectedIndex] + "</b>";

            XmlDocument doc = Cache[rssLocation] as XmlDocument ?? (new XmlDocument());
            if (!doc.HasChildNodes) doc.Load(rssLocation);
            if (null == Cache[rssLocation]) Cache[rssLocation] = doc;

            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/rss/channel/item");
            foreach (XmlNode node in nodes)
            {
                string str = node["title"].InnerText;
                if (str.StartsWith(Horoscope.SelectedItem.Text))
                {
                    data = data + node["description"].InnerText;
                    data = data.Remove(data.IndexOf("More horoscopes!"));
                }
            }
            return data;
        }
        protected void Horoscope_SelectedIndexChanged(object sender, EventArgs e)
        {
            HoroscopeLabel.Text = GetHoroscope();
        }
    }
}
