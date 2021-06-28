<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ConfirmDelete.ascx.vb" Inherits="VTimeNetLat_maintenance_manttables_ConfirmDelete" %>
<div style="width:350px">
<table>
                <tr>
                    <td rowspan="2">
                        <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/Question.png">
                        </dxe:ASPxImage>
                    </td>
                    <td>
                        <%--<dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                            Text="Do you want to delete the selected rows?">
                        </dxe:ASPxLabel>--%>   
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                            Text="¿Desea borrar la fila seleccionada?">
                        </dxe:ASPxLabel>
                                 
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>
                    <%--Text="Do&nbsp;not&nbsp;ask&nbsp;again"--%>
                        <dxe:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="No&nbsp;preguntar&nbsp;nuevamente" ClientInstanceName="cbDontAsk">
                            <ClientSideEvents CheckedChanged="function(s,e){cbDontAsk_CheckedChanged(cbDontAsk)}" />
                        </dxe:ASPxCheckBox>
                    </td>                
                    <td style="width:100%">
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnYes" runat="server" Width="50px" AutoPostBack="False" Text="Aceptar"
                            ClientInstanceName="btnYes" >                            
                            <ClientSideEvents Click="btnYes_Click" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnNo" runat="server" Width="50px" AutoPostBack="False" Text="Cancelar"
                            ClientInstanceName="btnNo" >
                            <ClientSideEvents Click="btnNo_Click" />
                        </dxe:ASPxButton>
                    </td>
                </tr>            
            </table>
</div>
            