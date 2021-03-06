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

    public partial class WeatherWidgetUserControl : System.Web.UI.UserControl, IWidget
    {
        private string weatherLocation = "http://xml.weather.yahoo.com/forecastrss?p=";
        private string zipCode = "22202";
        private IWidgetHost _Host;

        public IWidgetHost Host
        {
            get { return _Host; }
            set { _Host = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this._Host.IsFirstLoad) this.LoadContentView(sender, e);
        }

        protected void LoadContentView(object sender, EventArgs e)
        {
            this.Multiview.ActiveViewIndex = 1;
            this.MultiviewTimer.Enabled = false;
            zipCode = this.Host.GetState();

            //if (this.Host.IsFirstLoad)
            //{
            //    if (this.Host.GetState().Trim().Length == 0)
            //    {
            //        //WeatherLabel.Text = GetWeatherData();
            //    }
            //    else
            //    {
            //        //WeatherLabel.Text = this.Host.GetState();
            //        zipCode = this.Host.GetState();
            //    }
            //}
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            WeatherLabel.Text = GetWeatherData();
        }

        void IWidget.Init(IWidgetHost host)
        {
            this.Host = host;
        }

        void IWidget.ShowSettings()
        {
            SettingsPanel.Visible = true;
            txtZipCode.Text = zipCode;
        }
        void IWidget.HideSettings()
        {
            zipCode = txtZipCode.Text;
            WeatherLabel.Text = GetWeatherData();
            this.Host.SaveState(zipCode);            
            SettingsPanel.Visible = false;
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

        public string GetWeatherData()
        {
            string url = weatherLocation + zipCode;

            XmlDocument doc = Cache[url] as XmlDocument ?? (new XmlDocument());
            try
            {
                if (!doc.HasChildNodes) doc.Load(url);
            }
            catch 
            {
                return string.Empty;
            }
            if (null == Cache[url]) Cache[url] = doc;

            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/rss/channel/item");
            string data = "";
            foreach (XmlNode node in nodes)
            {
                data = data + node["title"].InnerText;
                data = data + node["description"].InnerText;
            }
            return data;
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            zipCode = txtZipCode.Text;
            WeatherLabel.Text = GetWeatherData();
            this.Host.SaveState(zipCode);
            (this as IWidget).HideSettings();
        }
    }
}
