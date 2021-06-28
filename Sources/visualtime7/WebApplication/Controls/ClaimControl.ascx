<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ClaimControl.ascx.vb"
    Inherits="Controls_ClaimControl" %>
    <dxe:ASPxComboBox ID="ClaimComboBox" runat="server" EnableCallbackMode="True"
    CallbackPageSize="20" IncrementalFilteringMode="Contains" ValueField="NCLAIM"
    TextFormatString="{0} - {2} - {3}" Width="400px" DropDownStyle="DropDown" DropDownRows="20"
    IncrementalFilteringDelay="300" ClientInstanceName="Claim" EnableClientSideAPI="True"
    DropDownWidth="500px">
    <Columns>
        <dxe:ListBoxColumn Caption="Siniestro"                FieldName="NCLAIM"         Width="70px"     meta:resourcekey="NCLAIMColumnResource" />
        <dxe:ListBoxColumn Caption="Ramo"                     FieldName="NBRANCH"        Visible="False"  meta:resourcekey="NBRANCHColumnResource" />
        <dxe:ListBoxColumn Caption="Ramo"                     FieldName="SBRANCH"        Width="100px"    meta:resourcekey="SBRANCHColumnResource" />
        <dxe:ListBoxColumn Caption="Producto"                 FieldName="NPRODUCT"       Visible="False"  meta:resourcekey="NPRODUCTColumnResource" />
        <dxe:ListBoxColumn Caption="Producto"                 FieldName="SPRODUCT"       Width="150px"    meta:resourcekey="SPRODUCTColumnResource" />
        <dxe:ListBoxColumn Caption="Póliza"                   FieldName="NPOLICY"        Width="70px"     meta:resourcekey="NPOLICYColumnResource"/>
        <dxe:ListBoxColumn Caption="Certificado"              FieldName="NCERTIF"        Visible="False"  meta:resourcekey="NCERTIFColumnResource"/>
        <dxe:ListBoxColumn Caption="Código de Cliente"        FieldName="SCLIENT"        Width="140px"    meta:resourcekey="SCLIENTColumnResource" />
        <dxe:ListBoxColumn Caption="Cliente"                  FieldName="SCLIENAME"      Width="150px"    meta:resourcekey="SCLIENAMEColumnResource" />
        <dxe:ListBoxColumn Caption="Fecha de Declaración"     FieldName="DDECLADAT"      Visible="False"  meta:resourcekey="DDECLADATColumnResource" />
        <dxe:ListBoxColumn Caption="Fecha de Ocurrencia"      FieldName="DOCCURDAT"      Visible="False"  meta:resourcekey="DOCCURDATColumnResource" />
        <dxe:ListBoxColumn Caption="Sucursal"                 FieldName="NOFFICE"        Visible="False"  meta:resourcekey="NOFFICEColumnResource" />
        <dxe:ListBoxColumn Caption="Sucursal"                 FieldName="SOFFICE"        Width="100px"    meta:resourcekey="SOFFICEColumnResource" />
        <dxe:ListBoxColumn Caption="Oficina"                  FieldName="NOFFICEAGEN"    Visible="False"  meta:resourcekey="NOFFICEAGENColumnResource" />
        <dxe:ListBoxColumn Caption="Oficina"                  FieldName="SOFFICEAGEN"    Visible="False"  meta:resourcekey="SOFFICEAGENColumnResource" />
        <dxe:ListBoxColumn Caption="Agencia"                  FieldName="NAGENCY"        Visible="False"  meta:resourcekey="NAGENCYColumnResource" />
        <dxe:ListBoxColumn Caption="Agencia"                  FieldName="SAGENCY"        Visible="False"  meta:resourcekey="SAGENCYColumnResource" />
        <dxe:ListBoxColumn Caption="Monto de reserva actual"  FieldName="NLOC_RESERV"    Visible="False"  meta:resourcekey="NLOC_RESERVColumnResource" />
        <dxe:ListBoxColumn Caption="Monto pagado"             FieldName="NLOC_PAY_AM"    Visible="False"  meta:resourcekey="NLOC_PAY_AMColumnResource" />
        <dxe:ListBoxColumn Caption="Estado del Siniestro"     FieldName="SSTACLAIM"      Visible="False"  meta:resourcekey="SSTACLAIMColumnResource" />
        <dxe:ListBoxColumn Caption="Estado del Siniestro"     FieldName="SSTACLAIMDESC"  Width="150px"    meta:resourcekey="SSTACLAIMDESCColumnResource" />
    </Columns>
    <ClientSideEvents EndCallback="function(s, e) {
if(s.GetItemCount()==1) {
	s.SetSelectedIndex(0);
    }
}" />
</dxe:ASPxComboBox>

