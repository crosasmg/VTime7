<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ClientControl.ascx.vb"
    Inherits="Controls_ClientControl" %>
<dxe:ASPxComboBox ID="ClientIdComboBox" runat="server" EnableCallbackMode="True"
    CallbackPageSize="20" IncrementalFilteringMode="Contains" ValueField="SCLIENT"
    TextFormatString="{0} {1}" Width="400px" DropDownStyle="DropDown" DropDownRows="20"
    IncrementalFilteringDelay="500" ClientInstanceName="ClientId" EnableClientSideAPI="True"
    DropDownWidth="510px">
    <Columns>
        <dxe:ListBoxColumn Caption="Código" FieldName="SCLIENT" Width="45" />
        <dxe:ListBoxColumn Caption="Nombre" FieldName="SCLIENAME" Width="120" />
        <dxe:ListBoxColumn Caption="Nacimiento" FieldName="SBIRTHDAT" Width="30" />
    </Columns>
    <ClientSideEvents EndCallback="function(s, e) {
if(s.GetItemCount()==1) {
	s.SetSelectedIndex(0);
    var ea = new ASPxClientProcessingModeEventArgs();
    s.SelectedIndexChanged.FireEvent(s, ea);
    }
}" />
</dxe:ASPxComboBox>
