using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Dropthings.Widget.Framework;
using InMotionGIT.Core.Configuration;
using InMotionGIT.Common.Proxy;

namespace Dropthings.Widgets
{
    public partial class NavigationDirectory : System.Web.UI.UserControl, IWidget
    {
        private IWidgetHost _Host;

        private XElement _State;

        private XElement State
        {
            get
            {
                if (_State == null) _State = XElement.Parse(this._Host.GetState());
                return _State;
            }
        }

        private string Category
        {
            get { return State.Element("Category").Value; }
            set { State.Element("Category").Value = value; }
        }

        private bool ShowDescriptions
        {
            get { return bool.Parse(State.Element("ShowDescriptions").Value); }
            set { State.Element("ShowDescriptions").Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int languageId = (new InMotionGIT.Membership.Providers.MemberContext()).LanguageId;
            String query = string.Format("SELECT " +
                            " LOOKUP.CODE, " +
                            "     LOOKUP.DESCRIPTION " +
                            " FROM " +
                            "            " +
                            "     LOOKUP " +
                            " WHERE " +
                            "                        " +
                            "     LOOKUP.LOOKUPID = (" +
                            "         SELECT " +
                            "                      " +
                            "             LOOKUPID " +
                            "         FROM " +
                            "                          " +
                            "             LOOKUPMASTER " +
                            "         WHERE " +
                            "               " +
                            "             KEY = 'NavigationDirectory' " +
                            " 	) " +
                            " AND LOOKUP.LANGUAGEID = {0} " +
                            " AND LOOKUP.RECORDSTATUS = 1 " +
                            " ORDER BY " +
                            "     LOOKUP.QUERYORDER, " +
                            " 	LOOKUP.DESCRIPTION ", languageId);
            var _clienFactory = new DataManagerFactory(query, "ROLE", "FrontOfficeConnectionString");
            var vloDataTable = _clienFactory.QueryExecuteToTable(true);
            ddCategories.DataSource = vloDataTable;
            ddCategories.DataBind();
        }

        #region IWidget Members

        public new void Init(IWidgetHost host)
        {
            hdnLang.Value  = (new InMotionGIT.Membership.Providers.MemberContext()).LanguageId.ToString() ;

            _Host = host;
            if (this._Host.IsFirstLoad)
            {
                SetDataSource();
            }
        }

        public void ShowSettings()
        {
            ddCategories.SelectedValue = Category;
            chkDetails.Checked = ShowDescriptions;
            pnlEdit.Visible = true;
        }

        public void HideSettings()
        {
            SaveState();

            pnlEdit.Visible = false;
            SetDataSource();
        }

        public void Minimized()
        {
            //throw new NotImplementedException();
        }

        public void Maximized()
        {
            //throw new NotImplementedException();
        }

        public void Closed()
        {
            //throw new NotImplementedException();
        }

        #endregion IWidget Members

        private void SaveState()
        {
            this.Category = ddCategories.SelectedValue.ToString();
            this.ShowDescriptions = chkDetails.Checked;

            var xml = this.State.Xml();
            this._Host.SaveState(xml);
        }

        private void SetDataSource()
        {
            int _Category = int.Parse(State.Element("Category").Value);
            VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");

            DataTable vloDataTable = new DataTable();
            int language =    (new InMotionGIT.Membership.Providers.MemberContext()).LanguageId;

            string SessionRole = (string)Session["sSche_code"];

            if (string.IsNullOrEmpty(SessionRole))
                SessionRole = config.Security.DefaultRole;

            if (_Category == 1)
            {
                var _clienFactory = new DataManagerFactory("SELECT NavigationDirectory.Id, NavigationDirectoryDesc.Description, NavigationDirectory.URLPath, " +
                                                     "NavigationDirectory.CategoryCode, NavigationDirectory.IMAGEFILE, NavigationDirectoryDesc.Title" +
                                              " FROM NavigationDirectory " +
                                             " INNER JOIN NavigationDirectoryDesc " +
                                                 "ON NavigationDirectory.Id = NavigationDirectoryDesc.Id " +
                                                "AND NavigationDirectoryDesc.LanguageID = " + language +
                                             " WHERE (NavigationDirectory.AllowRoles LIKE ('%" + SessionRole + "%')" +
                                                " OR (UPPER(RTRIM(LTRIM(NavigationDirectory.AllowRoles)))) = 'ALL')", "NavigationDirectory", "FrontOfficeConnectionString");
                vloDataTable = _clienFactory.QueryExecuteToTable(true);
            }
            else
            {
                var _clienFactory = new DataManagerFactory("SELECT NavigationDirectory.Id, NavigationDirectoryDesc.Description, NavigationDirectory.URLPath, " +
                                                   "NavigationDirectory.CategoryCode, NavigationDirectory.IMAGEFILE, NavigationDirectoryDesc.Title " +
                                             " FROM NavigationDirectory " +
                                            " INNER JOIN NavigationDirectoryDesc ON NavigationDirectory.Id = NavigationDirectoryDesc.Id AND NavigationDirectoryDesc.LanguageID = " + language +
                                            " WHERE NavigationDirectory.CategoryCode=" + Category +
                                              " AND (NavigationDirectory.AllowRoles LIKE ('%" + SessionRole + "%')" +
                                               " OR (UPPER(RTRIM(LTRIM(NavigationDirectory.AllowRoles)))) = 'NavigationDirectory')", "ROLE", "FrontOfficeConnectionString");
                vloDataTable = _clienFactory.QueryExecuteToTable(true);
            }

            ListGridView.DataSource = vloDataTable;
            ListGridView.DataBind();
        }

        public int LanguageID(string LanguageDesc)
        {
            switch (LanguageDesc)
            {
                case "EN-US":
                    return 1;

                case "ES-CR":
                    return 2;

                default:
                    return 1;
            }
        }

        protected void ListGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ListGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ShowDescriptions)
                {
                    ((Label)e.Row.FindControl("lblDetails")).Visible = true;
                }
                else
                {
                    ((Label)e.Row.FindControl("lblDetails")).Visible = false;
                }
            }
        }

        protected void chkDetails_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void ddCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ListGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //if (ListGridView.EditIndex != -1)
            //{
            ListGridView.PageIndex = e.NewPageIndex;
            SetDataSource();
            //}
        }




    }
}