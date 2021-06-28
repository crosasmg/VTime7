// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using Dropthings.Widget.Widgets.Flickr;
using Dropthings.Widget.Framework;
using Dropthings.Web.Framework;


namespace Dropthings.Widgets 
{

public partial class FlickrWidgetUserControl : System.Web.UI.UserControl, IWidget
{

    private const string FLICKR_API_KEY = "c705bfbf75e8d40f584c8a946cf0834c";
    private const string MOST_RECENT ="http://www.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key="+FLICKR_API_KEY;
    private const string INTERESTING = "http://www.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key="+FLICKR_API_KEY;
    private const string ENTER_TAG ="http://www.flickr.com/services/rest/?method=flickr.photos.search&api_key="+FLICKR_API_KEY+"&tags=";
    private const string FIND_BY_USERNAME = "http://www.flickr.com/services/rest/?method=flickr.people.findByUsername&api_key="+FLICKR_API_KEY+"&username=";
    private const string FIND_BY_EMAIL = "http://www.flickr.com/services/rest/?method=flickr.people.findByEmail&api_key="+FLICKR_API_KEY+"&find_email=";
    private const string PHOTOS_FROM_FLICKR_USER = "http://www.flickr.com/services/rest/?method=flickr.people.getPublicPhotos&api_key="+FLICKR_API_KEY+"&user_id=";

    private int Columns = 3;
    private int Rows = 3;
    
    private IWidgetHost _Host;

    private int PageIndex
    {
        get 
        { 
            return (int)(ViewState[this.ClientID + "_PageIndex"] ?? 0);
        }
        set { ViewState[this.ClientID + "_PageIndex"] = value; }
    }

    private XElement _State;
    private XElement State
    {
        get
        {
            if( _State == null )
            {
                string stateXml = this._Host.GetState();
                if (string.IsNullOrEmpty(stateXml))
                {
                    //stateXml = "<state><type>MostPopular</type><tag /></state>";
                    _State = new XElement("state",
                        new XElement("type", "MostPopular"),
                        new XElement("tag", ""));
                }
                else
                {
                    _State = XElement.Parse(stateXml);
                }
            }
            return _State;
        }
    }



    public EnumPhotoType TypeOfPhoto
    {
        get { return (EnumPhotoType)Enum.Parse( typeof( EnumPhotoType ), State.Element("type").Value ); }
        set { State.Element("type").Value = value.ToString(); }
    }
    public string PhotoTag
    {
        get { return State.Element("tag").Value; }
        set { State.Element("tag").Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this._Host.IsFirstLoad || ProxyAsync.IsUrlInCache(Cache, this.GetPhotoUrl()))
            this.LoadPhotoView(this, e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!this._Host.IsFirstLoad)
            this.ShowPictures(this.PageIndex);
    }

    protected void LoadPhotoView(object sender, EventArgs e)
    {
        this.ShowPictures(this.PageIndex);

        this.FlickrWidgetMultiview.ActiveViewIndex = 1;
        this.FlickrWidgetTimer.Enabled = false;
    }

    private void LoadState()
    {
        if (this.TypeOfPhoto == EnumPhotoType.MostPopular)
        {
            mostInterestingRadioButton.Checked = true;
            mostRecentRadioButton.Checked = false;
            customTagRadioButton.Checked = false;
        }
        else if (this.TypeOfPhoto == EnumPhotoType.MostRecent)
        {
            mostRecentRadioButton.Checked = true;
            mostInterestingRadioButton.Checked = false;
            customTagRadioButton.Checked = false;
        }
        else
        {
            mostRecentRadioButton.Checked = false;
            mostInterestingRadioButton.Checked = false;
            customTagRadioButton.Checked = true;
            CustomTagTextBox.Text = this.PhotoTag;
        }
    }

    void IWidget.Init(IWidgetHost host)
    {
        this._Host = host;
    }

    void IWidget.ShowSettings()
    {
        settingsPanel.Visible = true;

        this.LoadState();
        
    }
    void IWidget.HideSettings()
    {
        settingsPanel.Visible = false;
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
    protected void photoTypeRadio_CheckedChanged(object sender, EventArgs e)
    {
        this.SaveState();
        this.ShowPictures(this.PageIndex);
    }

    private void SaveState()
    {
        if( mostRecentRadioButton.Checked )
            this.TypeOfPhoto = EnumPhotoType.MostRecent;
        else if( mostInterestingRadioButton.Checked )
            this.TypeOfPhoto = EnumPhotoType.MostPopular;
        else if( customTagRadioButton.Checked )
        {
            this.TypeOfPhoto = EnumPhotoType.Tag;
            this.PhotoTag = this.CustomTagTextBox.Text;
        }

        this._Host.SaveState(this.State.Xml());
        this.PageIndex = 0;
        this._State = null;
    }

    private string GetPhotoUrl()
    {
        string url = MOST_RECENT;

        if (this.TypeOfPhoto == EnumPhotoType.Tag)
            url = ENTER_TAG + this.PhotoTag;
        else if (this.TypeOfPhoto == EnumPhotoType.MostPopular)
            url = INTERESTING;
        else
            url = MOST_RECENT;

        return url;
    }

    private string LoadPictures()
    {
        string cachedXml = new ProxyAsync().GetXml(GetPhotoUrl(), 10);
        return cachedXml;
    }

    private void ShowPictures(int pageIndex)
    {
        var xml = this.LoadPictures();
        if( string.IsNullOrEmpty(xml) ) return;
        var xroot = XElement.Parse(xml);
        var photos = (from photo in xroot.Element("photos").Elements("photo")
                    select new PhotoInfo
                    { 
                        Id = (string)photo.Attribute("id"),
                        Owner = (string)photo.Attribute("owner"),
                        Title = (string)photo.Attribute("title"),
                        Secret = (string)photo.Attribute("secret"),
                        Server = (string)photo.Attribute("server"),
                        Farm = (string)photo.Attribute("Farm"),
                        /*IsPublic = (bool)photo.Attribute("ispublic"),
                        IsFriend = (bool)photo.Attribute("isfriend"),
                        IsFamily = (bool)photo.Attribute("isfamily")*/
                    }).Skip(pageIndex*Columns*Rows).Take(Columns*Rows);
        
        HtmlTable table = new HtmlTable();
        table.Align = "center";
        var row = 0;
        var col = 0;
        var count = 0;
        foreach( var photo in photos )
        {
            if( col == 0 )
                table.Rows.Add( new HtmlTableRow() );

            var cell = new HtmlTableCell();


            var div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "preview");

            var img = new HtmlImage();
            img.Src = photo.PhotoUrl(true);
            //img.Width = img.Height = 75;
            img.Border = 0;
            img.Attributes.Add("class", "preview");
            //img.Attributes.Add("onmouseover", "Zoom.larger(this, 150, 150)");
            //img.Attributes.Add("onmouseout", "Zoom.smaller(this, 150, 150)");

            var link = new HtmlGenericControl("a");
            link.Attributes["href"] = photo.PhotoPageUrl;      
            link.Attributes["target"] = "_blank";
            link.Attributes["title"] = photo.Title;
            
            link.Controls.Add(img);
            div.Controls.Add(link);                       
            cell.Controls.Add(div);

            table.Rows[row].Cells.Add(cell);

            col ++;
            if( col == Columns )
            {
                col = 0; row ++;
            }

            count ++;
        }

        photoPanel.Controls.Clear();
        photoPanel.Controls.Add(table);

        if( pageIndex == 0 )
        {
            this.ShowPrevious.Visible = false;
            this.ShowNext.Visible = true;
        }
        else
        {
            this.ShowPrevious.Visible = true;
        }
        if( count < Columns*Rows )
        {
            this.ShowNext.Visible = false;
        }
    }
    protected void ShowPrevious_Click(object sender, EventArgs e)
    {
        this.PageIndex --;        
        this.ShowPictures(this.PageIndex);
    }
    protected void ShowNext_Click(object sender, EventArgs e)
    {
        this.PageIndex ++;
        this.ShowPictures(this.PageIndex);
    }

    protected void ShowTagButton_Clicked(object sender, EventArgs e)
    {
        this.PhotoTag = this.CustomTagTextBox.Text;
        this.SaveState();
    }
}
}