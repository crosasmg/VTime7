<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UnderwritingPanel.aspx.vb"
    Inherits="Underwriting_UnderwritingPanel" UICulture="auto" 
    Culture="auto" meta:resourcekey="PageResource8"  %>
<%@ Register Src="Controls/GeneralInformation.ascx" TagName="GeneralInformation" TagPrefix="uc1" %>
<%@ Register Src="Controls/Requirements.ascx" TagName="Requirements" TagPrefix="uc2" %>
<%@ Register Src="Controls/Restrictions.ascx" TagName="Restrictions" TagPrefix="uc3" %>
<%@ Register Src="Controls/History.ascx" TagName="History" TagPrefix="uc4" %>
<%@ Register Src="Controls/RequirementsGeneralInformation.ascx" TagName="RequirementsGeneralInformation" TagPrefix="uc6" %>
<%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="HeaderUserControl" %>
<%@ Register Src="Controls/Decision.ascx" TagName="Decision" TagPrefix="uc7" %>
<%@ Register Src="Controls/Payments.ascx" TagName="Payments" TagPrefix="uc8" %>
<%@ Register Src="Controls/Communication.ascx" TagName="Communication" TagPrefix="uc9" %>
<%@ Register Src="Controls/PolicyHistory.ascx" TagName="PolicyHistory" TagPrefix="uc10" %>
<%@ Register Src="Controls/CaseAttachments.ascx" TagName="CaseAttachments" TagPrefix="uc11" %>
<%@ Register Src="Controls/Notes.ascx" TagName="Notes" TagPrefix="uc12" %>
<%@ Register Src="Controls/CaseInformation.ascx" TagName="CaseInformation" TagPrefix="uc13" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    	<script type="text/javascript">
        console.log("Token:" + '<% Response.Write(Session("AccessToken")) %>');
		console.log("Anonimo:" + '<% Response.Write(Session("AnonymousAccessToken")) %>');

 	</script>
    <% If ConfigurationManager.AppSettings.Get("NBEnableHTML5") IsNot Nothing AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5") Then %>

        <link href="/Styles/bootstrap.min.css" rel="stylesheet" />
        <link href="/Styles/font-awesome.min.css" rel="stylesheet" />
        <link href="/Styles/jquery-ui.min-1.11.4.css" rel="stylesheet" />
        <link href="/Styles/ui.jqgrid-bootstrap.css" rel="stylesheet" />
         <!--JQuery Toast-->
        <link href="/Styles/jquery.toast.css" rel="stylesheet" />

        <script src="/Scripts/jquery.min.js"></script>
        <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>
        <script src="/Scripts/bootstrap.min.js"></script>
        <script src="/Scripts/jquery-ui.js"></script>
        <script src="/Scripts/jquery.numeric.min.js"></script>
        <script src="/Scripts/jquery.validate.min.js"></script>
        <script src="/Scripts/jquery.validate.messages-es.js"></script>
        <script src="/Scripts/additional-methods.min.js"></script>
        <!--JQuery Toast-->
        <script src="/Scripts/jquery.toast.js"></script>
        <script src="/Scripts/fasi.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Scripts\fasi.js").ToString("yyyyMMddHHmmss")%>"></script>   
    <% Else %>
        <link href="/Styles/font-awesome.min.css" rel="stylesheet" />
        <link href="/Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
        <link href="Underwriting/Styles/NewBusiness.css" rel="stylesheet" />    
        <script src="/Scripts/grid.icons.extend.js" charset="utf-8"></script>    
        <script src="/Scripts/grid.locale-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js" charset="utf-8"></script>
        <script src="/Scripts/grid.locale.extend-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js" charset="utf-8"></script>
        <script src="/Scripts/jquery.free.jqgrid.min.js"></script>
        <script src="/Scripts/jquery.ext.js"></script>
        <script src="/Scripts/jquery-ui.js"></script>
        <script src="/Scripts/jquery.ui.datepicker-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js"></script>
        <script src="/Scripts/moment.min.js"></script>

        <script src="/Underwriting/scripts/UnderwritingPanel.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\UnderwritingPanel.js").ToString("yyyyMMddHHmmss")%>"></script>
        <script src="/Underwriting/scripts/Header.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\Header.js").ToString("yyyyMMddHHmmss")%>"></script>
	    <script src="/Underwriting/scripts/GeneralInformation.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\GeneralInformation.js").ToString("yyyyMMddHHmmss")%>"></script>
        <script src="/Underwriting/scripts/Requirements.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\Requirements.js").ToString("yyyyMMddHHmmss")%>"></script>
        <script src="/Underwriting/scripts/Restrictions.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\Restrictions.js").ToString("yyyyMMddHHmmss")%>"></script>
        <script src="/Underwriting/scripts/Decision.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\Decision.js").ToString("yyyyMMddHHmmss")%>"></script>
        <script src="/Underwriting/scripts/History.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\History.js").ToString("yyyyMMddHHmmss")%>"></script>
        <script src="/Underwriting/scripts/HistoryPremium.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\HistoryPremium.js").ToString("yyyyMMddHHmmss")%>"></script>
	    <script src="/Underwriting/scripts/CaseInformation.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\CaseInformation.js").ToString("yyyyMMddHHmmss")%>"></script>
	    <script src="/Underwriting/scripts/_attachments.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "/Underwriting/scripts/_attachments.js").ToString("yyyyMMddHHmmss")%>"></script>
    <% End If %>
    
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
<script type="text/javascript">
</script>
<div>
    <HeaderUserControl:Header ID="HeaderUC" runat="server" />
    <div id="tabsGeneral">
        <ul class="nav nav-tabs" role="tablist">
			<li role="presentation" class="active"><a href="#CaseInformationTab" onclick="javascript:sessionStorage.setItem('SelectedTab','CaseInformationTab')" aria-controls="CaseInformationTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("CaseInformationTab"))%>"><i class="fa fa-pencil-square-o fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("CaseInformationTab"))%></a></li>
            <li role="presentation"><a href="#GeneralInformationTab" onclick="javascript:sessionStorage.setItem('SelectedTab','GeneralInformationTab')" aria-controls="GeneralInformationTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("GeneralInformationTab"))%>"><i class="fa fa-user fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("GeneralInformationTab"))%></a></li>
            <li role="presentation"><a href="#RequirementsTab" onclick="javascript:sessionStorage.setItem('SelectedTab','RequirementsTab')" aria-controls="RequirementsTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("RequirementsTab"))%>"><i class="fa fa-users fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("RequirementsTab"))%></a></li>
            <li role="presentation"><a href="#DecisionTab" onclick="javascript:sessionStorage.setItem('SelectedTab','DecisionTab')" aria-controls="DecisionTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("DecisionTab"))%>"><i class="fa fa-arrows fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("DecisionTab"))%></a></li>
            <li role="presentation"><a href="#HistoryTab" onclick="javascript:sessionStorage.setItem('SelectedTab','HistoryTab')" aria-controls="HistoryTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("HistoryTab"))%>"><i class="fa fa-clock-o fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("HistoryTab"))%></a></li>
            <li role="presentation"><a href="#PolicyHistoryTab" onclick="javascript:sessionStorage.setItem('SelectedTab','PolicyHistoryTab')" aria-controls="PolicyHistoryTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("PolicyHistoryTab"))%>"><i class="fa fa-clock-o fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("PolicyHistoryTab"))%></a></li>
            <% If ConfigurationManager.AppSettings.Get("EnableUnderwritingAttachmentsTab") IsNot Nothing AndAlso ConfigurationManager.AppSettings.Get("EnableUnderwritingAttachmentsTab") Then %>
            <li role="presentation"><a href="#CaseAttachmentsTab" onclick="javascript:sessionStorage.setItem('SelectedTab','CaseAttachmentsTab')" aria-controls="CaseAttachmentsTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("CaseAttachmentsTab"))%>"><i class="fa fa-folder-o fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("CaseAttachmentsTab"))%></a></li>
            <% End If %>
            <% If ConfigurationManager.AppSettings.Get("EnableUnderwritingNotesTab") IsNot Nothing AndAlso ConfigurationManager.AppSettings.Get("EnableUnderwritingNotesTab") Then %>
            <li role="presentation"><a href="#NotesTab" onclick="javascript:sessionStorage.setItem('SelectedTab','NotesTab')" aria-controls="NotesTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("NotesTab"))%>"><i class="fa fa-sticky-note-o fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("NotesTab"))%></a></li>
            <% End If %>
        </ul>
        <div class="tab-content master" id="tabContent">
			<div role="tabpanel" class="tab-pane active" id="CaseInformationTab">
                <uc13:CaseInformation ID="CaseInformation1" runat="server" />
            </div>
            <div role="tabpanel" class="tab-pane active" id="GeneralInformationTab">
                <uc1:GeneralInformation ID="GeneralInformation1" runat="server" />
            </div>
            <div role="tabpanel" class="tab-pane" id="RequirementsTab">
                <uc2:Requirements ID="Requirements1" runat="server" />
            </div>
            <div role="tabpanel" class="tab-pane" id="RestrictionsTab">
               <uc3:Restrictions ID="Restrictions1" runat="server" />
            </div>
            <div role="tabpanel" class="tab-pane" id="DecisionTab">
                <uc7:Decision ID="Decision1" runat="server" />
            </div>
             <div role="tabpanel" class="tab-pane" id="HistoryTab">
               <uc4:History ID="History1" runat="server" />
            </div>
             <div role="tabpanel" class="tab-pane" id="PolicyHistoryTab">
                <uc10:PolicyHistory ID="PolicyHistoryTab1" runat="server" />
            </div>
             <div role="tabpanel" class="tab-pane" id="CaseAttachmentsTab">
                <uc11:CaseAttachments ID="CaseAttachmentsTab1" runat="server" />
            </div>
            <div role="tabpanel" class="tab-pane" id="NotesTab">
                <uc12:Notes ID="Notes1" runat="server" />
            </div>
        </div>
    </div>
</div>
 <%   If ConfigurationManager.AppSettings.Get("NBEnableHTML5") IsNot Nothing AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5") Then %>
  <form runat="server">
    <div>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupExpiredHtml5"
             ClientInstanceName="popupExpiredHtml5" Modal="True" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
            CssPostfix="SoftOrange" EnableHotTrack="False" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
            meta:resourcekey="popupExpiredResource1">
            <ModalBackgroundStyle>
                <BackgroundImage HorizontalPosition="center"></BackgroundImage>
            </ModalBackgroundStyle>
            <HeaderTemplate>
                <div>
                    Sesión Expirada</div>
            </HeaderTemplate>
            <HeaderStyle>
                <Paddings PaddingRight="6px" />
            </HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2Html5" runat="server"
                    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" CssPostfix="Office2003_Olive"
                    EnableHotTrack="False" ImageFolder="~/App_Themes/Office2003 Olive/{0}/" meta:resourcekey="PopupControlContentControl2Resource1">
                    
                    <div style="width: 350px">
                        <table width="100%">
                            <tr>
                                <td>
                                    <dxe:ASPxImage ID="ASPxImage1Html5" runat="server" ImageUrl="Images/exclamation.png" meta:resourcekey="ASPxImage1Resource1">
                                    </dxe:ASPxImage>
                                </td>
                                <td colspan="2">
                                    <dxe:ASPxLabel ID="ASPxLabel1Html5" runat="server" Text="Su sesión ha expirado y la misma será re-establecida"
                                        meta:resourcekey="ASPxLabel1Resource1">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="width: 100%">
                                </td>
                                <td>
                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnYesHtml5" EnableDefaultAppearance="False"
                                        Width="50px" ID="btnYesHtml5" meta:resourcekey="btnYesResource1">
                                        <Image UrlChecked="Images/btnAcceptOn.png" UrlPressed="Images/btnAcceptOn.png" Url="Images/btnAcceptOff.png">
                                        </Image>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </div>
<dx:ASPxHiddenField ID="hdnUPanelHtml5" runat="server" ClientInstanceName="hdnUPanelHtml5"></dx:ASPxHiddenField>  
</form>
<% End If %>
<% If IsNothing(ConfigurationManager.AppSettings.Get("NBEnableHTML5")) OrElse ConfigurationManager.AppSettings.Get("NBEnableHTML5") = "False" Then %>
    <div>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupExpired"
             ClientInstanceName="popupExpired" Modal="True" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
            CssPostfix="SoftOrange" EnableHotTrack="False" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
            meta:resourcekey="popupExpiredResource1">
            <ClientSideEvents CloseUp="btnYes_Click"></ClientSideEvents>
            <ModalBackgroundStyle>
                <BackgroundImage HorizontalPosition="center"></BackgroundImage>
            </ModalBackgroundStyle>
            <HeaderTemplate>
                <div>
                    Sesión Expirada</div>
            </HeaderTemplate>
            <HeaderStyle>
                <Paddings PaddingRight="6px" />
            </HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server"
                    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" CssPostfix="Office2003_Olive"
                    EnableHotTrack="False" ImageFolder="~/App_Themes/Office2003 Olive/{0}/" meta:resourcekey="PopupControlContentControl2Resource1">
                    
                    <div style="width: 350px">
                        <table width="100%">
                            <tr>
                                <td>
                                    <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="Images/exclamation.png" meta:resourcekey="ASPxImage1Resource1">
                                    </dxe:ASPxImage>
                                </td>
                                <td colspan="2">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Su sesión ha expirado y la misma será re-establecida"
                                        meta:resourcekey="ASPxLabel1Resource1">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="width: 100%">
                                </td>
                                <td>
                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnYes" EnableDefaultAppearance="False"
                                        Width="50px" ID="btnYes" meta:resourcekey="btnYesResource1">
                                        <ClientSideEvents Click="btnYes_Click"></ClientSideEvents>
                                        <Image UrlChecked="Images/btnAcceptOn.png" UrlPressed="Images/btnAcceptOn.png" Url="Images/btnAcceptOff.png">
                                        </Image>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </div>
<dx:ASPxHiddenField ID="hdnUPanel" runat="server" ClientInstanceName="hdnUPanel"></dx:ASPxHiddenField>  
<% End If %>
<div class="modal fade" id="panel-confirmation-modal" tabindex="0" role="dialog" aria-labelledby="gridSystemModalLabel">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="gridSystemModalLabel"><% Response.Write(GetLocalResourceObject("modal-title"))%></h4>
            </div>
            <div class="modal-body">
                <% Response.Write(GetLocalResourceObject("modal-body"))%>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal"><% Response.Write(GetGlobalResourceObject("Resource", "CancelButtonResource"))%></button>
                <button type="button" id="save-changes-confirmation" class="btn btn-primary"><% Response.Write(GetGlobalResourceObject("Resource", "AcceptBtnResource"))%></button>
            </div>
        </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
<!-- Modal information -->
<div id="myModal" class="modal fade" tabindex="-1" role="dialog">
	<div id="myModalDialog" class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
				<h4 id="textTitle" class="modal-title"></h4>
			</div>
			<div class="modal-body">
				<p id="textBody" style="word-break: break-all;white-space: normal;">

				</p>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
			</div>
		</div>
	</div>
</div>
<!-- Modal information -->
<!-- Modal addRule -->
	<div id="addRule" class="modal fade" role="dialog" data-toggle="modal" data-backdrop="static">
		<div id="addRuleDialog" class="modal-dialog modal-lg" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button id="btnClose" type="button" onclick="CleanAndExit()" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
					<h4 class="modal-title"><% Response.Write(GetLocalResourceObject("modal-addrule-title"))%></h4>
				</div>
				<div id="modal-addrule-content" class="modal-body"></div>
				<div class="modal-footer">
					<div class="col-sm-12 text-right">
						<button id="btn-guardar-regla" type="button" onclick="SaveAndExit()" class="btn btn-default" title="<% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>">
							<i class="fa fa-pencil-square-o fa-lg"></i> <% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>
						</button>
						<button id="btn-cancelar-regla" type="button" onclick="CleanAndExit()" class="btn btn-default" title="<% Response.Write(GetGlobalResourceObject("Resource", "CancelButtonResource"))%>">
							<i class="glyphicon glyphicon-remove-circle"></i> <% Response.Write(GetGlobalResourceObject("Resource", "CancelButtonResource"))%>
						</button>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Modal addRule -->
<div class="loadingDiv"></div>
<div id="textNoDataAvailableDiv">
    <label><% Response.Write(GetLocalResourceObject("textNoDataAvailable"))%></label>
</div>
    <script>
        $(function () {
            $('#tabsGeneral, #tabContent').tabs({
                activate: function (event, ui) {
                    var tabName = ui.newPanel.attr('id');

                    if (tabName === 'CaseAttachmentsTab') {
                        document.getElementById('case-attachment-iframe').src += '';
                    }
                    if (tabName === 'NotesTab') {
                        document.getElementById('case-notes-iframe').src += '';
                    }
                }
            });
        });
    </script>
</asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
        <% If ConfigurationManager.AppSettings.Get("NBEnableHTML5") IsNot Nothing AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5") Then %>            
            <link href="/Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
            <link href="/Styles/ui.jqgrid-bootstrap.css" rel="stylesheet" />
            <link href="/Styles/jquery-ui.min-1.11.4.css" rel="stylesheet" />
            <link href="Styles/NewBusiness.css" rel="stylesheet" />        
            <script src="/Scripts/grid.icons.extend.js" charset="utf-8"></script>    
            <script src="/Scripts/grid.locale-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js" charset="utf-8"></script>
            <script src="/Scripts/grid.locale.extend-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js" charset="utf-8"></script>
            <script src="/Scripts/jquery.free.jqgrid.min.js"></script>
            <script src="/Scripts/jquery.ext.js"></script>
            <script src="/Scripts/jquery-ui.js"></script>
            <script src="/Scripts/jquery.numeric.min.js"></script>
            <script src="/Scripts/jquery.ui.datepicker-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js"></script>
            <script src="/Scripts/moment.min.js"></script>

    

            <script src="/Underwriting/scripts/UnderwritingPanel.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\UnderwritingPanel.js").ToString("yyyyMMddHHmmss")%>"></script>
            <script src="/Underwriting/scripts/Header.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\Header.js").ToString("yyyyMMddHHmmss")%>"></script>
	        <script src="/Underwriting/scripts/GeneralInformation.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\GeneralInformation.js").ToString("yyyyMMddHHmmss")%>"></script>
            <script src="/Underwriting/scripts/Requirements.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\Requirements.js").ToString("yyyyMMddHHmmss")%>"></script>
            <script src="/Underwriting/scripts/Restrictions.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\Restrictions.js").ToString("yyyyMMddHHmmss")%>"></script>
            <script src="/Underwriting/scripts/Decision.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\Decision.js").ToString("yyyyMMddHHmmss")%>"></script>
            <script src="/Underwriting/scripts/History.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\History.js").ToString("yyyyMMddHHmmss")%>"></script>
            <script src="/Underwriting/scripts/HistoryPremium.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\HistoryPremium.js").ToString("yyyyMMddHHmmss")%>"></script>
	        <script src="/Underwriting/scripts/CaseInformation.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\CaseInformation.js").ToString("yyyyMMddHHmmss")%>"></script>
	        <script src="/Underwriting/scripts/_attachments.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "/Underwriting/scripts/_attachments.js").ToString("yyyyMMddHHmmss")%>"></script>
        <% End If %>
    </asp:Content>

