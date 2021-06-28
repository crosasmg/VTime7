<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MenuVTCustom.ascx.vb" Inherits="Dropthings.Widgets.MenuUserControl" %>

<script type="text/javascript">
    function insGoTo(RefUrl, windowLogicalCodeVal) {

      var param = { windowLogicalCode: windowLogicalCodeVal };
        var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/default.aspx/IsAllowed';
        $.ajax({
            url: urlBase,
            data: JSON.stringify(param),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d.IsAllowed.length != 0) {
                    alert(data.d.IsAllowed);
                }
                else {
                    var lstrURL = RefUrl.substr(RefUrl.indexOf('sCodispl=') + 9);
                    var lintLength = lstrURL.indexOf('&');
                    var lstrCodispl = lstrURL.substr(0, lintLength);
                    var win = open('<%=System.Configuration.ConfigurationManager.AppSettings("Url.BackOffice") %>' + RefUrl, 'Transaccion' + lstrCodispl.replace('-', '_'), 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
                    win.moveTo(0, 0);
                    win.resizeTo(window.screen.availWidth, window.screen.availHeight);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus);
            }
        });


<%--    var lstrURL= RefUrl.substr(RefUrl.indexOf('sCodispl=') + 9);
    var lintLength=lstrURL.indexOf('&');
    var lstrCodispl = lstrURL.substr(0,lintLength);
    var win = open('<%=System.Configuration.ConfigurationManager.AppSettings("Url.BackOffice") %>' + RefUrl, 'Transaccion' + lstrCodispl.replace('-', '_'), 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
    win.moveTo(0, 0);
    win.resizeTo(window.screen.availWidth,window.screen.availHeight);    --%>
}

</script>

<asp:Panel ID="pnlEdit" runat="server" Visible="False" Width="333px"
    meta:resourcekey="pnlEditResource1">
<table style="width: 208px">
        <tr>
            <td>
                <asp:HyperLink ID="HLinkWindows" runat="server"
                    meta:resourcekey="HLinkWindowsResource1"></asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Panel>
<dxe:ASPxLabel ID="MessageLabel" runat="server" ClientInstanceName="MessageLabel" Visible="false"/>
<dxtv:ASPxTreeView ID="MenuTreeView" runat="server" EnableCallBacks="True" ClientIDMode="AutoID"
    meta:resourcekey="TreeViewMenuResource1">
</dxtv:ASPxTreeView>

<dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ID="popupMessage" runat="server" ClientInstanceName="popupMessage" Modal="true">
    <HeaderTemplate>
        <div>
            <asp:Literal ID="popupMessageTextHeader" runat="server" Text="Mensaje" meta:resourcekey="popupMessageTextHeaderResource"></asp:Literal></div>
    </HeaderTemplate>
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <div style="width: 350px">
                <table>
                    <tr>
                        <td rowspan="2">
                            <dxe:ASPxImage ID="ASPxImageM" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/exclamation.png"
                                Width="32px">
                            </dxe:ASPxImage>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="Message1Label" runat="server" meta:resourcekey="Message1LabelResource"
                                Text="">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 100%">
                        </td>
                        <td>
                            <dxe:ASPxButton ID="CloseButton" runat="server" Width="50px" AutoPostBack="False"
                                ClientInstanceName="CloseButton" EnableDefaultAppearance="False" EnableTheming="False">
                                <Image Url="~/images/generaluse/ConfirmDelete/btnacceptoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnaccepton.gif"
                                    UrlPressed="~/images/generaluse/ConfirmDelete/btnaccepton.gif" />
                                <ClientSideEvents Click="function(s, e) {popupMessage.Hide();}" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>




