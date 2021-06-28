<%@ Page Title="Operations" Language="VB" UICulture="auto" MasterPageFile="~/DropthingsMasterPage.master"
    AutoEventWireup="false" CodeFile="Operations.aspx.vb" Inherits="OperationsWebForm" %>

<%@ MasterType TypeName="DropthingsMasterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table class="NoBorderNoBackground" style="width: 1300px;">
        <tr style="height: 40px; vertical-align: middle">
            <td style="width: 275px">
                <b>&nbsp;<dxe:ASPxImage ID="ASPxImage3" runat="server" Height="16px" Width="16px"
                    ImageUrl="~/images/16x16/Operations/security.png" />
                    &nbsp;<dxe:ASPxLabel ID="UsersASPxLabel" runat="server" Text="Security" meta:resourcekey="SecurityASPxLabel"
                        Font-Bold="True">
                    </dxe:ASPxLabel>
                </b>
            </td>
            <td style="width: 275px">
                <b>
                    <dxe:ASPxImage ID="ASPxImage1" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/workflow.png" />
                    &nbsp;<dxe:ASPxLabel ID="WorkflowsASPxLabel" runat="server" Text="Workflows" Font-Bold="True"
                        meta:resourcekey="WorkflowsASPxLabel">
                    </dxe:ASPxLabel>
                </b>
            </td>
            <td style="width: 275px">
                <b>
                    <dxe:ASPxImage ID="ASPxImage2" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/widget.png" />
                    &nbsp;
                    <dxe:ASPxLabel ID="WidgetsASPxLabel" runat="server" Font-Bold="True" Text="Widgets"
                        meta:resourcekey="WidgetsASPxLabel">
                    </dxe:ASPxLabel>
                </b>
            </td>
            <td style="width: 275px">
                <b>&nbsp;<dxe:ASPxImage ID="ASPxImage4" runat="server" Height="16px" Width="16px"
                    ImageUrl="~/images/16x16/Operations/config.png" />
                    &nbsp;<dxe:ASPxLabel ID="ConfigurationASPxLabel" runat="server" Font-Bold="True"
                        Text="Configuration" meta:resourcekey="ConfigurationASPxLabel">
                    </dxe:ASPxLabel>
                </b>
            </td>
			<td style="width: 275px">
                <b>&nbsp;<dxe:ASPxImage ID="ASPxImage17" runat="server" Height="16px" Width="16px"
                    ImageUrl="~/images/16x16/Operations/config.png" />
                    &nbsp;<dxe:ASPxLabel ID="STSASPxLabel" runat="server" Font-Bold="True"
                        Text="STS & Accesos" meta:resourcekey="STSASPxLabel">
                    </dxe:ASPxLabel>
                </b>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 200px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage10" runat="server" Height="16px"
                Width="16px" ImageUrl="~/images/16x16/Operations/userEdit.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="UserASPxHyperLink" runat="server" Text="Users Manager" NavigateUrl="~/dropthings/Admin/UsersManager.aspx"
                    meta:resourcekey="UserASPxHyperLink">
                </dxe:ASPxHyperLink>
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage15" runat="server" Height="16px"
                    Width="16px" ImageUrl="~/images/16x16/Operations/userList.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="UserListASPxHyperLink" meta:resourcekey="UserListASPxHyperLink"
                    runat="server" Text="User list" NavigateUrl="~/dropthings/Admin/UserListViewer.aspx" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage23" runat="server" Height="16px"
                    Width="16px" ImageUrl="~/images/16x16/Operations/userTrace.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="UsersSecurityTraceHyperLink" meta:resourcekey="UsersSecurityTraceHyperLinkResource"
                    runat="server" Text="Users Security Trace" NavigateUrl="~/dropthings/Admin/UsersSecurityTrace.aspx" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage18" runat="server" Height="16px"
                    Width="16px" ImageUrl="~/images/16x16/Operations/emailTrace.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink1" meta:resourcekey="EmailTraceHyperLinkResource"
                    runat="server" Text="E-mail Trace" NavigateUrl="~/dropthings/Admin/EmailTrace.aspx" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage7" runat="server" Height="16px"
                    Width="16px" ImageUrl="~/images/16x16/Operations/relation.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="RelationshipASPxHyperLink" runat="server" Text="Relationship between users"
                    meta:resourcekey="RelationshipASPxHyperLink" NavigateUrl="~/dropthings/Admin/UserRelationship.aspx" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage5" runat="server" Height="16px"
                    Width="16px" ImageUrl="~/images/16x16/Operations/groups.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="GroupsASPxHyperLink" runat="server" Text="Groups" meta:resourcekey="GroupsASPxHyperLink"
                    NavigateUrl="~/dropthings/Admin/GroupsManager.aspx" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage9" runat="server" Height="16px"
                    Width="16px" ImageUrl="~/images/16x16/Operations/usergroup.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="UsersGroupsASPxHyperLink" runat="server" Text="Users by group"
                    meta:resourcekey="UsersGroupsASPxHyperLink" NavigateUrl="~/dropthings/Admin/UsersByGroupManager.aspx" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage6" runat="server" Height="16px"
                    Width="16px" ImageUrl="~/images/16x16/Operations/approved.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ApprovedUsersHyperLink" runat="server" Text="Approved Users"
                    NavigateUrl="~/Authentication/ApprovedUsersManager.aspx" meta:resourcekey="ApprovedUsersHyperLinkResource" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;
                <dxe:ASPxImage ID="ASPxImage11" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/roles.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink2" runat="server" Text="Roles Manager" NavigateUrl="~/dropthings/Admin/RolesManager.aspx"
                    meta:resourcekey="RolesASPxHyperLink">
                </dxe:ASPxHyperLink>
                <%--  <br />
                   <br />
               &nbsp;&nbsp;&nbsp;
                <dxe:ASPxImage ID="ASPxImage19" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/log.png" />
                &nbsp;&nbsp;
              <dxe:ASPxHyperLink ID="ASPxHyperLink3" runat="server" Target="_blank" Text="Event Log" NavigateUrl="~/dropthings/Admin/EventLog.aspx"
                    meta:resourcekey="HistoricalEventLogASPxLabelResource">
                </dxe:ASPxHyperLink> --%>
            </td>
            <td style="vertical-align: top; width: 275px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage14" runat="server" Height="16px"
                Width="16px" ImageUrl="~/images/16x16/Operations/workflowMan.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="WorkFlowASPxHyperLink" runat="server" Text="Workflow Manager"
                    NavigateUrl="~/dropthings/Admin/WorkFlowManager.aspx" meta:resourcekey="WorkFlowASPxHyperLink">
                </dxe:ASPxHyperLink>
            </td>
            <td style="vertical-align: top; width: 275px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <dxe:ASPxImage ID="ASPxImage20" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/widgetMan.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink8" runat="server" Text="Widgets Manager" NavigateUrl="~/dropthings/Admin/WidgetsManager.aspx"
                    meta:resourcekey="WidgetsManagerASPxHyperLink">
                </dxe:ASPxHyperLink>
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage13" runat="server"
                    Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/rolesMan.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink9" runat="server" Text="Widgets In Roles Manager"
                    NavigateUrl="~/dropthings/Admin/WidgetsInRolesManager.aspx" meta:resourcekey="WidgetsASPxHyperLink">
                </dxe:ASPxHyperLink>
            </td>
            <td style="vertical-align: top; width: 275px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage8" runat="server"
                Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/init.png" />
                &nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink11" runat="server" Text="Initialization" NavigateUrl="~/dropthings/Admin/Initialization.aspx"
                    meta:resourcekey="InitializationASPxHyperLink">
                </dxe:ASPxHyperLink>
            </td>
			<td style="vertical-align: top; width: 275px">
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage21" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/widgetMan.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink12" runat="server" Text="Access by Group" NavigateUrl="~/generated/crud/AccesoXGrupo.aspx"
                    meta:resourcekey="AccessByGroupASPxHyperLink">
                </dxe:ASPxHyperLink>
                <br />
                <br />
				
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage22" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/widgetMan.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink13" runat="server" Text="Access" NavigateUrl="~/generated/crud/Acceso.aspx"
                    meta:resourcekey="AccessASPxHyperLink">
                </dxe:ASPxHyperLink>
                <br />
                <br />
				
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage26" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/widgetMan.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink14" runat="server" Text="Access Group" NavigateUrl="~/generated/crud/Grupo_Acceso.aspx"
                    meta:resourcekey="AccessGroupASPxHyperLink">
                </dxe:ASPxHyperLink>
                <br />
                <br />
				
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage24" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/widgetMan.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink15" runat="server" Text="Roles by Group" NavigateUrl="~/generated/crud/GrupoXRol.aspx"
                    meta:resourcekey="RolesByGroupASPxHyperLink">
                </dxe:ASPxHyperLink>
                <br />
                <br />
				
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dxe:ASPxImage ID="ASPxImage25" runat="server" Height="16px" Width="16px" ImageUrl="~/images/16x16/Operations/widgetMan.png" />
                &nbsp;&nbsp;
                <dxe:ASPxHyperLink ID="ASPxHyperLink16" runat="server" Text="Access token time by Client" NavigateUrl="~/generated/crud/Consumidor.aspx"
                    meta:resourcekey="AccessTokenTimeASPxHyperLink">
                </dxe:ASPxHyperLink>
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>