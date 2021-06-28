<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ConfirmReplace.ascx.vb" Inherits="VTimeNetLat_maintenance_manttables_ConfirmReplace" %>
<div style="width:350px">
<table>
                <tr>
                    <td rowspan="2">
                        <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/Question.png">
                        </dxe:ASPxImage>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                            Text="Do you want to replace the current document?">
                        </dxe:ASPxLabel>            
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>
<%--                        <dxe:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="Do&nbsp;not&nbsp;ask&nbsp;again" ClientInstanceName="cbDontAsk">
                            <ClientSideEvents CheckedChanged="function(s,e){cbDontAsk_CheckedChanged(cbDontAsk)}" />
                        </dxe:ASPxCheckBox>--%>
                    </td>                
                    <td style="width:100%">
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnOk" runat="server" Width="50px" AutoPostBack="True" Text="Ok"
                            ClientInstanceName="btnOk" >                            
                            <ClientSideEvents Click="btnOk_Click" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnCancel" runat="server" Width="50px" AutoPostBack="False" Text="Cancel"
                            ClientInstanceName="btnCancel" >
                            <ClientSideEvents Click="btnCancel_Click" />
                        </dxe:ASPxButton>
                    </td>
                </tr>            
            </table>
</div>
            