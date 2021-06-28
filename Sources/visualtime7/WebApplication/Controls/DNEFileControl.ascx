<%@ Control Language="VB" AutoEventWireup="true" CodeFile="DNEFileControl.ascx.vb" Inherits="Controls_DNEFileControl" %>
<script src='<%= ResolveClientUrl("~/Scripts/DNEFileControl.js") %>' type="text/javascript"></script>

<asp:UpdatePanel ID="upMainFileControl" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload" runat="server">
    <ContentTemplate>       
            <dxlp:ASPxLoadingPanel ID="lpFileControl" runat="server" ClientInstanceName="lpFileControl" Modal="True" />
            <div id="NewResources" class="contenedorGrilla" runat="server" visible="false" style="width: 600px; margin: auto; border: 1px solid #A8A8A8">
                <h4 title="New Resources" runat="server" meta:resourcekey="lblNewResourcesResource" />
                <div class="controlesDeActualizacion">
                    <asp:Label ID="lblFilesToUpload" runat="server" Text="Files to Upload" meta:resourcekey="lblFilesToUploadResource" CssClass="align=right" Font-Bold="True"></asp:Label>
                    <dx:ASPxUploadControl ID="FileUploader" runat="server" UploadMode="Advanced" ClientIDMode="Static" CssClass="FileUploaderDNEFiles">
                        <AdvancedModeSettings EnableMultiSelect="True"></AdvancedModeSettings>
                    </dx:ASPxUploadControl>
                    <asp:UpdatePanel ID="upSubtmitFile" OnUnload="UpdatePanel_Unload" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubmitFileUpload" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Button ID="btnSubmitFileUpload" runat="server" Text="Submit" ClientIDMode="Static" Style="display: none" CssClass="ButtonSubmitFileUploadDNEFiles" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br>
                <div>
                    <dx:ASPxGridView ID="gvTemporalResources" Width="100%" runat="server" AutoGenerateColumns="False" KeyFieldName="SequenceId;ConsequenceId" meta:resourcekey="gvTemporalResourcesResource">
                        <%--<ClientSideEvents EndCallback="function(s, e) { lpFileControl.Hide(); }" BeginCallback="function(s, e) { lpFileControl.Show(); }" />--%>
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="10" Caption=" ">
                                <EditButton Visible="True">
                                </EditButton>
                                <DeleteButton Visible="True">
                                </DeleteButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="SequenceId" Visible="false" Caption="Sequence Id" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ConsequenceId" Visible="false" Caption="Consequence Id" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" VisibleIndex="3" EditFormSettings-Visible="False" meta:resourcekey="gvTemporalResourcesColumnNameResource">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" PropertiesTextEdit-MaxLength="100" VisibleIndex="4" meta:resourcekey="gvTemporalResourcesColumnDescriptionResource">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ResourceTypeId" Caption="Resource Type Id" VisibleIndex="5" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn FieldName="ExpirationDate" Caption="Expiration Date" VisibleIndex="6" Visible="false" meta:resourcekey="gvTemporalResourcesColumnExpirationDateResource">
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn FieldName="ClientAssociatedPerson" Caption="Client Associated Person" VisibleIndex="7" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ClientAssociatedCompany" Caption="Client Associated Company" VisibleIndex="8" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="LocationId" Caption="Location Id" VisibleIndex="9" Visible="false">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsPager Visible="False">
                        </SettingsPager>
                        <SettingsText ConfirmDelete="Are you sure you want to delete the selected records?" />
                    </dx:ASPxGridView>
                </div>
                <div>
                    <asp:Label ID="lblUserMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <br>
                <div class="controlesDeActualizacion">
                    <asp:UpdatePanel ID="upTemporalResourceButtons" OnUnload="UpdatePanel_Unload" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSaveResources" />
                            <asp:PostBackTrigger ControlID="btnCancelSaveResources" />
                        </Triggers>
                        <ContentTemplate>
                            <dxe:ASPxButton ID="btnSaveResources" runat="server" Text="Add Files" ClientIDMode="Static" Style="display: inline;" meta:resourcekey="btnSaveResourcesResource" />
                            <dxe:ASPxButton ID="btnCancelSaveResources" runat="server" Text="Cancel" CausesValidation="false" ClientIDMode="static" Style="display: inline;" meta:resourcekey="btnCancelSaveResourcesResource" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%--<div class="overlayRecursos">
					<div class="mensajeCargandoRecursos"></div>
				</div>--%>
            </div>
            <br>
            <div id="DataBaseResources" runat="server" class="contenedorGrilla" style="width: 600px; margin: auto; border: 1px solid #A8A8A8">
                <div id="DNEFilterBox" runat="server" class="DNEFilterBox" visible="false">
                    <asp:Label ID="lblFilterLabel" runat="server" Text="Etiquetas: " Style="display: inline;" Height="26px" meta:resourcekey="lblFilterLabelResource" />
                    <asp:UpdatePanel ID="tagFilterContainer" OnUnload="UpdatePanel_Unload" CssClass="tagFilterContainer" style="display: inline;" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="PlaceholderControls" runat="server"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:LinkButton ID="lbtnFilter" runat="server" OnClick="FilterButton" Style="display: inline;" Text="Filtrar" Height="26px" meta:resourcekey="btnFilterResource" />
                </div>
                <asp:Label ID="lblUploadedFiles" runat="server" Font-Bold="True" Text="Uploaded Files" Visible="True" meta:resourcekey="lblUploadedFilesResource" />
                <asp:Label ID="lblNoFiles" runat="server" Font-Bold="True" Text="No Files Uploaded" Visible="False" meta:resourcekey="lblNoFilesResource" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;			
				<dx:ASPxButton ID="lbtnNewItems" Enabled="true" runat="server" Text="New File(s)" meta:resourcekey="lbtnNewItemsResource" />
                <asp:ObjectDataSource ID="odsActiveResources" runat="server" TypeName="InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts">
                    <%--<SelectParameters>
						<asp:Parameter DefaultValue="132" Name="sequenceId" Type="Int32" />
						<asp:Parameter DefaultValue="asd" Name="tags" Type="Object" />
						<asp:Parameter DefaultValue="asdg" Name="accessToken" Type="String" />
					</SelectParameters>--%>
                </asp:ObjectDataSource>
                <dx:ASPxGridView ID="gvActiveResources" runat="server" Width="100%" KeyFieldName="SequenceId;ConsequenceId" AutoGenerateColumns="False" OnCustomUnboundColumnData="gvActiveResources_CustomUnboundColumnData" DataSourceID="odsActiveResources" OnRowDeleting="gvActiveResources_RowDeleting" OnRowUpdating="gvActiveResources_RowUpdating" ClientIDMode="Predictable" Enabled="True" meta:resourcekey="gvActiveResourcesResource">
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                    <SettingsBehavior ProcessFocusedRowChangedOnServer="True" AllowFocusedRow="True" />
                    <SettingsEditing EditFormColumnCount="3" Mode="EditForm" />
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="13" Caption=" ">
                            <EditButton Visible="True">
                            </EditButton>
                            <DeleteButton Visible="True">
                            </DeleteButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="SequenceId" VisibleIndex="1" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ConsequenceId" VisibleIndex="2" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" EditFormSettings-Visible="False" VisibleIndex="3" meta:resourcekey="gvActiveResourcesColumnNameResource">
                            <DataItemTemplate>
                                <asp:UpdatePanel ID="upDownloadItem" OnUnload="UpdatePanel_Unload" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lbtnName" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lbtnName" runat="server" OnClick="gvActiveResources_FocusedRowChanged" Text='<%# Eval("Name") %>'></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" PropertiesTextEdit-MaxLength="100" VisibleIndex="4" meta:resourcekey="gvActiveResourcesColumnDescriptionResource">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ResourceTypeId" VisibleIndex="5" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="ExpirationDate" Caption="Expiration Date" VisibleIndex="6" Visible="false" meta:resourcekey="gvActiveResourcesColumnExpirationDateResource">
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="ClientAssociatedPerson" VisibleIndex="7" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ClientAssociatedCompany" VisibleIndex="8" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="LocationId" VisibleIndex="9" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CreationDate" Caption="Fecha de Creación" VisibleIndex="10" Name="resource.CreationDate" meta:resourcekey="gvActiveResourcesColumnCreationDateResource">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CreatorUserCode" Caption="Código Usuario Creador" VisibleIndex="11" Visible="false" Name="resource.CreatorUserCode" meta:resourcekey="gvActiveResourcesColumnCreatorUserCodeResource">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CreatorUserName" Caption="Usuario Creador" VisibleIndex="12" Name="CreatorUserName" UnboundType="String" meta:resourcekey="gvActiveResourcesColumnCreatorUserNameResource">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior ConfirmDelete="True" />
                    <SettingsPager Visible="true">
                    </SettingsPager>
                    <SettingsText ConfirmDelete="Are you sure you want to delete the selected records?" />
                </dx:ASPxGridView>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
