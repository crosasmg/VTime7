<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Payments.ascx.vb" Inherits="Underwriting_Controls_Payments" %>
<script type="text/javascript">
    //The scripts from this WebUserControl are on UnderwritingPanel.aspx page
</script>
<table width="100%">
    <tr>
        <td style="text-align: right">
        </td>
    </tr>
    <tr>
        <td>
            <dxwgv:ASPxGridView ID="gvPayments" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPayments"
                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange"
                DataSourceID="PaymentCollectionDS" KeyFieldName="RequirementID" meta:resourceKey="gvPaymentsResource"
                Width="100%">
                <Columns>
                    <dxwgv:GridViewDataDateColumn Caption="Fecha del Solicitud" FieldName="RequirementDate" Width="15%"
                        meta:resourceKey="RequirementDateResource" VisibleIndex="0">
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Descripción" FieldName="Description" meta:resourceKey="DescriptionResource" Width="45%"
                        VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Costo" FieldName="Cost" Name="Cost" VisibleIndex="2" Width="20%"
                        meta:resourcekey="CostResource">
                        <PropertiesTextEdit>
                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999999999g&gt;.&lt;00..99&gt;" />
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Falta por pagar" FieldName="CostDueAmount" Width="20%"
                        meta:resourceKey="CostDueAmountResource" VisibleIndex="3">
                        <PropertiesTextEdit>
                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999999999g&gt;.&lt;00..99&gt;" />
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="RequirementID" meta:resourceKey="GridViewDataTextColumnResource14"
                        Visible="False" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Images SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css">
                    <LoadingPanelOnStatusBar Url="~/App_Themes/SoftOrange/GridView/gvLoadingOnStatusBar.gif">
                    </LoadingPanelOnStatusBar>
                    <LoadingPanel Url="~/App_Themes/SoftOrange/GridView/Loading.gif">
                    </LoadingPanel>
                </Images>
                <ImagesFilterControl>
                    <LoadingPanel Url="~/App_Themes/SoftOrange/Editors/Loading.gif">
                    </LoadingPanel>
                </ImagesFilterControl>
                <Styles CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange">
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                    <LoadingPanel ImageSpacing="10px">
                    </LoadingPanel>
                </Styles>
                <StylesEditors>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
            </dxwgv:ASPxGridView>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ObjectDataSource 
                ID="PaymentCollectionDS" 
                runat="server" 
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="SelectAll" 
                TypeName="InMotionGIT.Underwriting.Proxy.Helpers.Payment"
                DataObjectTypeName="InMotionGIT.Underwriting.Proxy.Helpers.Payment"></asp:ObjectDataSource>
        </td>
    </tr>
</table>

