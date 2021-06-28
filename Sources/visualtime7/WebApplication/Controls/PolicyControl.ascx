<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PolicyControl.ascx.vb"
    Inherits="Controls_PolicyControl" %>
<dxe:ASPxComboBox ID="PolicyCertificateComboBox" runat="server" EnableCallbackMode="True"
    CallbackPageSize="20" IncrementalFilteringMode="Contains" ValueField="NPOLICY"
    TextFormatString="{3} - {6}" Width="400px" DropDownStyle="DropDown" DropDownRows="20"
    IncrementalFilteringDelay="300" ClientInstanceName="PolicyCertificate" EnableClientSideAPI="True"
    DropDownWidth="500px">
    <Columns>

        <dxe:ListBoxColumn Caption="Ramo" FieldName="SBRANCH" Width="100px" meta:resourcekey="SBRANCHColumnResource" />
        <dxe:ListBoxColumn Caption="Producto" FieldName="SPRODUCT" Width="180px" meta:resourcekey="SPRODUCTColumnResource" />
        <dxe:ListBoxColumn Caption="Fecha de efecto" FieldName="SSTARTDATE" Width="80px" meta:resourcekey="DSTARTDATEColumnResource" />
        <dxe:ListBoxColumn Caption="Póliza" FieldName="NPOLICY" Width="60px" meta:resourcekey="NPOLICYColumnResource" />
        <dxe:ListBoxColumn Caption="Certificado" FieldName="NCERTIF" Width="60px" meta:resourcekey="NCERTIFColumnResource" />
        <dxe:ListBoxColumn Caption="Cliente" FieldName="SCLIENT" Width="100px"  />
        <dxe:ListBoxColumn Caption="Nombre del Cliente" FieldName="SCLIENAME" Width="160px"  />
        <dxe:ListBoxColumn Caption="Capital Asegurado" FieldName="NCAPITAL" Width="90px" meta:resourcekey="NCAPITALColumnResource" />
        <dxe:ListBoxColumn Caption="Prima" FieldName="NPREMIUM" Width="70px" meta:resourcekey="NPREMIUMColumnResource" />
        <dxe:ListBoxColumn Caption="Sucursal" FieldName="NOFFICEDESC" Width="100px" meta:resourcekey="NOFFICEDESCColumnResource" />
    </Columns>
    <ClientSideEvents EndCallback="function(s, e) {
if(s.GetItemCount()==1) {
	s.SetSelectedIndex(0);
    }
}" />
</dxe:ASPxComboBox>