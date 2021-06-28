<%@ Control Language="VB" AutoEventWireup="true" CodeFile="DNENoteControl.ascx.vb" Inherits="Controls_DNENoteControl" %>

<script src='<%= ResolveClientUrl("~/Scripts/DNENoteControl.js") %>' type="text/javascript"></script>
<asp:UpdatePanel ID="upMainNotecontrol" UpdateMode="Conditional" OnUnload="updatepanel_unload" runat="server">
    <ContentTemplate>
        <dxlp:ASPxLoadingPanel ID="lpNoteControl" runat="server" ClientInstanceName="lpNoteControl" Modal="True" meta:resourcekey="lpNoteControlResource1" />
        <div id="Notes" class="contenedorGrilla" runat="server">
            <div class="DNEFilterBox">
                <asp:Label ID="lblFilter" runat="server" Text="Etiquetas: " Style="display: inline;" Height="26px" meta:resourcekey="lblFilter" />
                <asp:UpdatePanel ID="tagFilterContainer" OnUnload="UpdatePanel_Unload" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:PlaceHolder ID="phTags" runat="server"></asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:LinkButton ID="lbtnFilter" runat="server" OnClick="FilterButton" Style="display: inline;" Text="Filtrar" Height="26px" meta:resourcekey="lbtnFilterResource1" />
            </div>
            <dxe:ASPxButton ID="btnNewNote" runat="server" AutoPostBack="False" Cursor="pointer" meta:resourceKey="btnNewNoteResource" Text="New Note">
                <ClientSideEvents Click="function(s, e) { gvNotes.AddNewRow(); }" />
            </dxe:ASPxButton>
            <div>
                <asp:ObjectDataSource ID="odsNotes" runat="server" TypeName="Inmotiongit.Datosnoestruct.Proxy.DNE.OperationContracts"></asp:ObjectDataSource>
                <dxwgv:ASPxGridView ID="gvNotes" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvNotes" KeyFieldName="SequenceId;ConsequenceId" meta:resourceKey="gvNotes" OnCustomUnboundColumnData="GridView_CustomUnboundColumnData" OnRowDeleting="GridView_rowdeleting" OnRowInserting="GridView_rowinserting" OnRowUpdating="GridView_rowupdating" Width="100%">
                    <Columns>
                        <dxwgv:GridViewCommandColumn Caption=" " meta:resourcekey="GridViewCommandColumnResource1" ShowInCustomizationForm="True" VisibleIndex="11">
                            <EditButton Visible="True">
                            </EditButton>
                            <DeleteButton Visible="True">
                            </DeleteButton>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ConsequenceId" meta:resourcekey="GridViewDataTextColumnResource1" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SequenceId" meta:resourcekey="GridViewDataTextColumnResource2" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Note" FieldName="Name" meta:resourceKey="gvNotesColumnNameResource" Name="resource.name" ShowInCustomizationForm="True" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Content" FieldName="Content" meta:resourceKey="gvNotesColumnContentResource" ShowInCustomizationForm="True" VisibleIndex="5">
                            <EditFormSettings RowSpan="50" />
                            <DataItemTemplate>
                                <dxhe:ASPxHtmlEditor ID="heEditContent" runat="server" ActiveView="Preview" EnableDefaultAppearance="False" Height="38px" Html='<%#Eval("Note.content") %>' meta:resourceKey="heEditContentResource" Width="488px">
                                    <Styles EnableDefaultAppearance="False">
                                        <ViewArea>
                                            <Border BorderWidth="0px" />
                                        </ViewArea>
                                    </Styles>
                                    <Settings AllowContextMenu="False" AllowDesignView="False" AllowHtmlView="False" AllowInsertDirectImageUrls="False" />
                                    <Border BorderWidth="0px" />
                                </dxhe:ASPxHtmlEditor>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Expiration Date" FieldName="ExpirationDate" meta:resourceKey="gvNotesColumnExpirationdateResource" ShowInCustomizationForm="True" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Creation Date" FieldName="CreationDate" meta:resourceKey="gvNotesColumnCreationDateResource" Name="resource.CreationDate" ShowInCustomizationForm="True" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="User Code" FieldName="CreatorUserCode" meta:resourceKey="gvNotesColumnCreatorUserCodeResource" Name="resource.CreatorUserCode" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Creator User Name" FieldName="CreatorUserName" meta:resourceKey="gvNotesColumnCreatorUserNameResource" Name="CreatorUserName" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="8" Width="80%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Codigo Usuario Modificador" FieldName="UpdateUserCode" meta:resourceKey="gvNotesColumnUpdateUserCodeResource" Name="CreatorUserName" ShowInCustomizationForm="True" UnboundType="String" Visible="False" VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Usuario Modificador" FieldName="UpdateUserName" meta:resourceKey="gvNotesColumnUpdateUserNameResource" Name="CreatorUserName" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="10">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior ConfirmDelete="True" SortMode="Value" />
                    <SettingsPager PageSize="5">
                    </SettingsPager>
                    <SettingsEditing EditFormColumnCount="3" Mode="EditForm" />
                    <Settings ShowHeaderFilterButton="True" ShowTitlePanel="True" />
                    <SettingsText ConfirmDelete="Are you sure you want to delete the selected records?" />
                    <Templates>
                        <EditForm>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 122px; height: 24px;">
                                        <asp:Label ID="lblName" runat="server" meta:resourceKey="lblNameResource" Text="Note"></asp:Label>
                                    </td>
                                    <td style="width: 275px; height: 24px;">
                                        <dxe:ASPxTextBox ID="tbName" runat="server" EnableClientSideAPI="True" MaxLength="30" meta:resourceKey="tbNameResource" Text='<%#Eval("Name") %>' ValidationSettings-ValidationGroup="<%#  Container.ValidationGroup  %>" Width="170px">
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Name cannot be empty" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td style="width: 144px; height: 24px;">
                                        <asp:Label ID="lblExpirationDate" runat="server" meta:resourceKey="lblExpirationDateResource" Text="Expiration Date"></asp:Label>
                                    </td>
                                    <td style="height: 24px">
                                        <dxe:ASPxDateEdit ID="deExpirationDate" runat="server" AllowUserInput="False" ClientInstanceName="deExpirationDate" EditFormat="DateTime" meta:resourceKey="deExpirationDateResource" ValidationSettings-ValidationGroup="<%#  Container.ValidationGroup  %>" Value='<%# eval("ExpirationDate") %>'>
                                            <TimeSectionProperties Visible="True">
                                            </TimeSectionProperties>
                                            <ClientSideEvents Validation="function(s,e){e.isValid = (VerifySelectedDateIsGreaterThanToday(deExpirationDate.GetDate()))}" />
                                            <ValidationSettings CausesValidation="True">
                                            </ValidationSettings>
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 122px">&nbsp;</td>
                                    <td style="width: 275px">&nbsp;</td>
                                    <td style="width: 144px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <dxhe:ASPxHtmlEditor ID="heNoteContent" runat="server" ActiveView="Html" Height="199px" Html='<%#Bind("Note.Content") %>' meta:resourcekey="heNoteContentResource1" OnInit="Notes_Callback" SettingsValidation-ValidationGroup="<%#  Container.ValidationGroup  %>" Width="99%">
                                <Settings AllowDesignView="False" />
                            </dxhe:ASPxHtmlEditor>
                            <dxwgv:ASPxGridViewTemplateReplacement ID="GridViewTemplateReplacementUpdate" runat="server" ColumnID="" ReplacementType="EditFormUpdateButton" />
                            <dxwgv:ASPxGridViewTemplateReplacement ID="GridViewTemplateReplacementCancel" runat="server" ColumnID="" ReplacementType="EditFormCancelButton" />
                        </EditForm>
                    </Templates>
                </dxwgv:ASPxGridView>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<%--        </dxp:PanelContent>
    </PanelCollection>
</dxcp:ASPxCallbackPanel>--%>
