using DevExpress.Web.ASPxTreeView;
using Dropthings.Widget.Framework;
using InMotionGIT.Common.Extensions;
using InMotionGIT.Common.Proxy;
using InMotionGIT.Core.Configuration;
using System;
using System.Configuration;
using System.Data;
using System.Xml.Linq;

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
            if (this._Host.IsFirstLoad)
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
        }

        #region IWidget Members

        public new void Init(IWidgetHost host)
        {
            hdnLang.Value = (new InMotionGIT.Membership.Providers.MemberContext()).LanguageId.ToString();

            _Host = host;
            if (this._Host.IsFirstLoad)
            {
                SetDataSource();
            }
        }

        public void ShowSettings()
        {
            ddCategories.SelectedValue = Category;
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
            var xml = this.State.Xml();
            this._Host.SaveState(xml);
        }

        private void SetDataSource()
        {
            int _Category = int.Parse(State.Element("Category").Value);
            VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");

            DataTable vloDataTable = new DataTable();
            int language = (new InMotionGIT.Membership.Providers.MemberContext()).LanguageId;

            string SessionRole = (string)Session["sSche_code"];

            if (string.IsNullOrEmpty(SessionRole))
                SessionRole = config.Security.DefaultRole;

            var _clienFactory = new DataManagerFactory("SELECT NavigationDirectory.Id, NavigationDirectoryDesc.Description, NavigationDirectory.URLPath, " +
                                                 "NavigationDirectory.CategoryCode, NavigationDirectory.IMAGEFILE, NavigationDirectoryDesc.Title" +
                                          " FROM NavigationDirectory " +
                                         " INNER JOIN NavigationDirectoryDesc " +
                                             "ON NavigationDirectory.Id = NavigationDirectoryDesc.Id " +
                                            "AND NavigationDirectoryDesc.LanguageID = " + language +
                                         " WHERE NavigationDirectory.CategoryCode=" + Category +
										 " AND NavigationDirectory.Status = 1" + 
										   " AND (NavigationDirectory.AllowRoles LIKE ('%" + SessionRole + "%')" +
                                            " OR (UPPER(RTRIM(LTRIM(NavigationDirectory.AllowRoles)))) = 'ALL')", "NavigationDirectory", "FrontOfficeConnectionString");
            vloDataTable = _clienFactory.QueryExecuteToTable(true);
            MenuTreeView.Nodes.Clear();
            CreateMenu(MenuTreeView, vloDataTable);
        }

        public void CreateMenu(ASPxTreeView menu, DataTable data)
        {
            foreach (DataRow item in data.Rows)
            {
                TreeViewNode node = new TreeViewNode(item.StringValue("Description"), item.StringValue("Id"), item.StringValue("IMAGEFILE"), item.StringValue("URLPath").Replace("../", "/"));
                node.ToolTip = item.StringValue("Description");
                menu.Nodes.Add(node);
            }
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

        protected void chkDetails_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void ddCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}