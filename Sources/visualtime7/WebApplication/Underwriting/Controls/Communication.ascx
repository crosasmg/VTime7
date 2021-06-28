<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Communication.ascx.vb"
    Inherits="Underwriting_Controls_Communication" %>
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
            <dxwgv:ASPxGridView ID="gvCommunication" runat="server" AutoGenerateColumns="False"
                ClientInstanceName="gvCommunication" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" DataSourceID="CommunicationCollectionDS" KeyFieldName="EntryId"
                meta:resourceKey="gvCommunicationResource" Width="100%" PropertiesDateEdit-DisplayFormatString=" dd/MM/yyyy hh:mm tt">
                <Columns>
                    <dxwgv:GridViewDataDateColumn Caption="Entry Date" FieldName="EntryDate" VisibleIndex="0"
                        Width="13%" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm tt" meta:resourceKey="EntryDateResource">
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Entry Type" FieldName="EntryType" UnboundType="Integer"
                        meta:resourceKey="EntryTypeResource" VisibleIndex="1" Width="7%" CellStyle-HorizontalAlign="Center">
                        <DataItemTemplate>
                            <a onclick="javascript:LetterViewPopupControl.SetContentUrl('/Underwriting/LetterView.aspx?jobid=<%#GetRowValue(Container)%>');LetterViewPopupControl.Show();"
                                href="javascript:void(0);">
                                <dxe:ASPxImage ID="entryImage" runat="server" ClientInstanceName="entryImage" IsPng="True"
                                    ImageAlign="Middle" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Role" FieldName="Role" UnboundType="Integer"
                        Width="10%" VisibleIndex="2" meta:resourceKey="RoleResource">
                        <PropertiesComboBox ClientInstanceName="cmbRoles" TextField="Description" ValueField="Code"
                            ValueType="System.Int32">
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Client Id" FieldName="ClientId" VisibleIndex="3"
                        Width="10%" meta:resourceKey="ClientIdResource" />
                    <dxwgv:GridViewDataTextColumn Caption="Client Name" FieldName="ClientName" VisibleIndex="4"
                        Width="25%" meta:resourceKey="ClientNameResource" />
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="5"
                        Width="35%" meta:resourceKey="DescriptionResource" />
                    <dxwgv:GridViewDataTextColumn Caption="UnderwritingCaseID" FieldName="UnderwritingCaseID"
                        VisibleIndex="-1" Visible="false" />
                    <dxwgv:GridViewDataTextColumn Caption="EntryId" FieldName="EntryId" VisibleIndex="-1"
                        Visible="false" />
                    <dxwgv:GridViewDataTextColumn Caption="JobId" FieldName="JobId" VisibleIndex="-1"
                        Visible="false" />
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
                ID="CommunicationCollectionDS"
                runat="server" 
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="SelectAll" 
                TypeName="InMotionGIT.Underwriting.Proxy.Helpers.Communication"
                DataObjectTypeName="InMotionGIT.Underwriting.Contracts.Communication"></asp:ObjectDataSource>
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="LetterViewPopupControl" runat="server" Modal="True" meta:resourceKey="LetterViewPopupControlResource"
    ClientInstanceName="LetterViewPopupControl" ContentUrl="" HeaderText="Letter View"
    ShowPageScrollbarWhenModal="False" EnableClientSideAPI="True" Height="500px"
    Width="600" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    EnableViewState="True" ShowFooter="False" ShowHeader="true">
    <HeaderImage Url="..//Images/emailOpen.png" />
    <ContentStyle Paddings-Padding="0px" />
</dxpc:ASPxPopupControl>
