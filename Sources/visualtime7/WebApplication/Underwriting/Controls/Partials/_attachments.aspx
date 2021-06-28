<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_attachments.aspx.vb" Inherits="Underwriting_Controls_Partials_attachments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Styles/font-awesome.css" rel="stylesheet" />
    <link href="../../../Styles/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../../Styles/bootstrap.min.css" rel="stylesheet" />
	<asp:PlaceHolder runat="server" >
		<link href="../../../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />  
	</asp:PlaceHolder>
       <style>
	           .modal-header {
	               border-bottom: 3px solid #428bca  ;
	           }
	           .dxeEditArea_Office2010Silver.dxeEditAreaSys {
			    font: 12px helvetica,arial,sans-serif !important;
			    color: #333 !important;
			}
       </style>
    <script src="../../../Scripts/jquery.min.js"></script>
        <script src="../../../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">       
        var index = -1;

        function OnInitAccept(s, e) {
            s.GetTextContainer().className += " fa fa-pencil-square-o";
        }

        function OnInit(s, e) {
            s.GetTextContainer().className += " glyphicon glyphicon-remove-circle";
        }

        function Delete(s, e) {
            popup.Hide();
            if (index > -1) {
                GridViewActiveResources.DeleteRow(index);
                index = -1;
            }
        }
        function OnClickNo(s, e) {
            popup.Hide();
        }
        function showMessage(message) {
            $('#errorMessage').append(message);
            popupMessage.Show();
        }
        function OnClickAccept(s, e) {
            popupMessage.Hide();
        }
    </script>
</head>

<body  class="newbusiness">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="ButtonSubmitFileUpload" />
            </Triggers>
            <ContentTemplate>
                <div id="NewResources" class="contenedorGrilla" runat="server" visible="false">
                    <h4 title="New Resources" runat="server" meta:resourcekey="LabelNewResources" />
                    <div class="controlesDeActualizacion">
                        <asp:Label ID="FilesToUploadLabel" runat="server" Text="Files to Upload" meta:resourcekey="FilesToUploadLabel" Font-Bold="True"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="LabelNewItem" runat="server" Text="Nuevos items" meta:resourcekey="LabelNewItems" Visible="True"></asp:Label>
                        <dx:ASPxUploadControl ID="FileUploaderfiles0"   runat="server" UploadMode="Advanced" ClientIDMode="Static" CssClass="FileUploaderDNEFiles" Enabled="True">
                            <AdvancedModeSettings EnableMultiSelect="True"></AdvancedModeSettings>
                        </dx:ASPxUploadControl>
                        <asp:Button ID="ButtonSubmitFileUpload" runat="server" Text="Submit" ClientIDMode="Static" Style="display: none" CssClass="ButtonSubmitFileUploadDNEFiles" />
                    </div>
                    <div>
                        <dx:ASPxGridView ID="GridViewTemporalResources" Styles-SelectedRow-BackColor="White" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="SequenceId;ConsequenceId" meta:resourcekey="GridViewTemporalResources" Enabled="True">
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="10" Width="5%" Caption=" " ButtonType="Image">
                                    <EditButton Visible="True">
                                        <Image Url="../../../VTimeNet/Images/editBootstrap.png"></Image>
                                    </EditButton>
                                    <DeleteButton Visible="True">
                                        <image Url="../../../VTimeNet/Images/removeBootstrap.png"></image>
                                    </DeleteButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="SequenceId" Visible="false" Caption="Id Secuencia" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ConsequenceId" Visible="false" Caption="Id Consecutivo" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Name" Caption="Nombre" VisibleIndex="3" EditFormSettings-Visible="False" meta:resourcekey="GridViewTemporalResourcesColumnName">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Descripción" PropertiesTextEdit-MaxLength="100" VisibleIndex="4" meta:resourcekey="GridViewTemporalResourcesColumnDescription">
                                    <%--<PropertiesTextEdit>
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" ErrorText="Description cannot be empty" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>--%>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ResourceTypeId" Caption="Resource Type Id" VisibleIndex="5" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="ExpirationDate" Caption="Fecha de Expiración" Visible="false" VisibleIndex="6" meta:resourcekey="GridViewTemporalResourcesColumnExpirationDate">
                                    <%--   <EditItemTemplate>
                                        <dx:ASPxDateEdit ID="ExpirationDate" runat="server" EditFormat="DateTime" AllowUserInput="false" Value='<%# Bind("ExpirationDate") %>'>
                                            <TimeSectionProperties Visible="true">
                                            </TimeSectionProperties>
                                        </dx:ASPxDateEdit>
                                    </EditItemTemplate>--%>
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
                    </br>
                    <div class="controlesDeActualizacion">
                        <asp:LinkButton ID="ButtonCancelSaveResources" runat="server" ClientIDMode="Static" meta:resourcekey="ButtonCancelSaveResources" Style="display: inline;" Enabled="True"><i class="fa fa-times" aria-hidden="true"></i> Cancelar</asp:LinkButton>
                    </div>
                    <div class="overlayRecursos">
                        <div class="mensajeCargandoRecursos"></div>
                    </div>
                    </br>
                </div>
                <div id="DataBaseResources" runat="server" class="contenedorGrilla">
                    <div id="DNEFilterBox" class="DNEFilterBox" runat="server" style="margin-top:15px;">
                        <asp:Label ID="FilterLabel" runat="server" meta:resourcekey="FilterLabel" Style="display: inline; line-height:18px;" Height="15px" Text="Etiquetas: " />
                        <asp:Panel ID="tagFilterContainer" runat="server" CssClass="tagFilterContainer" Style="display:inline;">                         
                        </asp:Panel>      
                        &nbsp;&nbsp;&nbsp;                       
                        <asp:LinkButton ID="ButtonFilter" Height="15px" runat="server" OnClick="FilterButton" Text="Filtrar" meta:resourcekey="ButtonFilter" />
                    </div>
                    <br>
                    <div id="ActiveResources" runat="server">
                    	<div style="line-height:10px;">
	                        <asp:Label ID="UploadedFilesLabel" runat="server" style="padding: 1px;" Font-Bold="True" meta:resourcekey="UploadedFilesLabel" Text="Uploaded Files"></asp:Label>
	                        <br>
	                        <dx:ASPxButton ID="LinkButtonNewItemsId" style="padding: 0px; margin-left:-1px;" runat="server" CssClass="btn" meta:resourcekey="LinkButtonNewItems" Text="Add File(s)" Visible="True" />
                        </div>
                        <asp:ObjectDataSource ID="ObjectDataSourceActiveResources" runat="server" SelectMethod="" TypeName="InMotionGIT.DatosNoEstruct.Proxy.DNE.OperationContracts">
                            <%--<SelectParameters>
                                <asp:Parameter DefaultValue="132" Name="sequenceId" Type="Int32" />
                                <asp:Parameter DefaultValue="" Name="tags" Type="Object" />
                                <asp:Parameter DefaultValue="" Name="accessToken" Type="String" />
                                <asp:Parameter DefaultValue="" Name="provider" Type="String" />
                            </SelectParameters>--%>
                        </asp:ObjectDataSource>
                        <dx:ASPxGridView ID="GridViewActiveResources" CssClass="" Styles-Cell-BackColor="White" Styles-Row-BackColor="White" Styles-AlternatingRow-BackColor="White"  style="margin-top:-1px;"  runat="server" AutoGenerateColumns="False"  OnCustomUnboundColumnData="GridViewActiveResources_CustomUnboundColumnData" OnCustomButtonInitialize="GridViewActiveResources_CustomButtonInitialize" ClientIDMode="Predictable" DataSourceID="ObjectDataSourceActiveResources" Enabled="True" KeyFieldName="SequenceId;ConsequenceId" meta:resourcekey="GridViewActiveResources" OnRowDeleting="GridviewRecursosActivos_RowDeleting" OnRowUpdating="GridviewRecursosActivos_RowUpdating" Width="100%">
                             <ClientSideEvents CustomButtonClick="function(s, e) {
		                            if (e.buttonID == 'btnDelete')
                                                {
                                                index = e.visibleIndex;
			                            popup.Show();
                                                }
		                            }" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
                            <SettingsEditing EditFormColumnCount="1"  Mode="Inline" />
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="13" Width="5%" Caption=" " ButtonType="Image" CellStyle-BackColor="White">
                                    <EditButton Visible="True" >
                                        <Image Url="../../../VTimeNet/Images/editBootstrap.png"></Image>
                                    </EditButton>
                                    <%--<DeleteButton Visible="True">
                                    </DeleteButton>--%>
									<CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Eliminar" meta:resourcekey="DeleteCustomButton">
                                        <Image Url="../../../VTimeNet/Images/removeBootstrap.png"></Image>
                                    </dx:GridViewCommandColumnCustomButton>
	                                </CustomButtons>
                                    <UpdateButton >
                                        <Image AlternateText="Actualizar" Url="../../../VTimeNet/Images/editBootstrap.png"></Image>
                                    </UpdateButton>
                                    <CancelButton Text="Cancelar">
                                        <Image Url="../../../VTimeNet/Images/cancelbootstrapcopy.png"></Image>
                                    </CancelButton>
                                </dx:GridViewCommandColumn>
								<%--<dx:GridViewCommandColumn VisibleIndex="14">

								</dx:GridViewCommandColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="SequenceId" Visible="false" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ConsequenceId" Visible="false" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Nombre"  EditCellStyle-Wrap="True" EditFormSettings-CaptionLocation="None"  Width="25%" FieldName="Name" meta:resourcekey="GridViewActiveResourcesColumnName" VisibleIndex="3">
                                     <DataItemTemplate>
                                        <asp:UpdatePanel runat="server">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="HyperLinkName" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div style="word-break:break-all">
                                                    <asp:LinkButton ID="HyperLinkName" runat="server" OnClick="GridviewRecursosActivos_FocusedRowChanged" Text='<%# Eval("Name") %>'></asp:LinkButton>
                                                </div>
                                                </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                                <div style="word-break:break-all">
                                                     &nbsp;
                                                    <asp:LinkButton ID="HyperLinkName" runat="server" OnClick="GridviewRecursosActivos_FocusedRowChanged" Text='<%# Eval("Name") %>'></asp:LinkButton>
                                                </div>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Descripción" EditCellStyle-Wrap="True" EditFormSettings-RowSpan="4"  FieldName="Description" EditFormSettings-CaptionLocation="None"  Width="35%" meta:resourcekey="GridViewActiveResourcesColumnDescription" PropertiesTextEdit-MaxLength="100" VisibleIndex="4">
                                        <PropertiesTextEdit>
                                            <ClientSideEvents Init="function (s,e) {s.Focus(); }" />
                                        </PropertiesTextEdit>
                                    <%--                                <PropertiesTextEdit>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="True" ErrorText="Description cannot be empty" />
                                    </ValidationSettings> 
                                </PropertiesTextEdit>--%>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ResourceTypeId" Visible="false" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Fecha de Expiración" FieldName="ExpirationDate" meta:resourcekey="GridViewActiveResourcesColumnExpirationDate" Visible="false" VisibleIndex="6">
                                    <%--<EditItemTemplate>
                                    <dx:ASPxDateEdit ID="ExpirationDate" runat="server" EditFormat="DateTime" AllowUserInput="false" Value='<%# Eval("ExpirationDate") %>'>
                                        <TimeSectionProperties Visible="true">
                                        </TimeSectionProperties>
                                    </dx:ASPxDateEdit>
                                </EditItemTemplate>--%>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="ClientAssociatedPerson" Visible="false" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ClientAssociatedCompany" Visible="false" VisibleIndex="8">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="LocationId" Visible="false" VisibleIndex="9">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CreationDate" EditCellStyle-Wrap="True" PropertiesTextEdit-Style-Wrap="True"  EditFormSettings-CaptionLocation="None" Caption="Fecha de Creación" Width="12%" VisibleIndex="10"  CellStyle-Wrap="True" HeaderStyle-Wrap="True" Name="resource.CreationDate" meta:resourcekey="GridViewActiveResourcesColumnCreationDate">
                                            <EditItemTemplate >
                                                <div style="word-break:break-all;">
                                                    &nbsp;
                                                    <asp:Label runat="server" 
                                                        Text='  <%# Eval("CreationDate") %>'>
                                                    </asp:Label>                                            
                                                </div>
                                            </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CreatorUserCode" Caption="Código Usuario Creador" VisibleIndex="11" Visible="false" Name="resource.CreatorUserCode" meta:resourcekey="GridViewActiveResourcesColumnCreatorUserCode">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CreatorUserNameUW" EditCellStyle-Wrap="True" EditFormSettings-CaptionLocation="None" Caption="Usuario Creador" VisibleIndex="12"  Name="CreatorUserNameUW" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Width="18%"  UnboundType="String" meta:resourcekey="GridViewActiveResourcesColumnCreatorUserName">     
                                    <EditItemTemplate>
                                        <div style="word-break:break-all;">
                                            &nbsp;
                                            <asp:Label runat="server" 
                                                Text='  <%# Eval("CreatorUserNameUW") %>'>
                                            </asp:Label>                                            
                                        </div>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>                           
                            </Columns>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsPager Visible="true">
                            </SettingsPager>
                            <SettingsText ConfirmDelete="Are you sure you want to delete the selected records?" />
                            <Templates>     
                                <EditForm>
                                </EditForm>                             
                            </Templates> 
                        </dx:ASPxGridView>
                    </div>
                </div>
                <%--<div id="loadingMessage" style="display:none;">Cargando...</div>--%><%--<div class="loading" align="center">Loading. Please wait.<br /><br /><img src="resources/cargando.gif" alt="" /></div>--%><%--<asp:UpdateProgress ID="prgLoadingStatus" runat="server" DynamicLayout="true">
                    <ProgressTemplate>
                        <div id="overlay">
                            <div id="modalprogress">
                                <div id="theprogress">
                                    <asp:Image ID="imgWaitIcon" runat="server" ImageAlign="AbsMiddle" ImageUrl="/resources/cargando.gif" />
                                    Please wait...
                                </div>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ControlsReBind);

            $(document).ready(function () {
                $(".FileUploaderDNEFiles").change(FileUploaderDNEFilesChange);
                $(".dxgvCommandColumnItem_Office2010Silver dxgv__cci").attr("visible ", "hidden");
                //$(".ButtonSubmitFileUploadDNEFiles").click(MensajeUsuarioRecursoTemporal("Uploading resources, please wait..."));
                //$(".ButtonFilter").click(ShowLoadingMessage());
                //$(".ButtonFilter").click(ShowProgress());
            });

            function ControlsReBind() {
                $(".FileUploaderDNEFiles").bind("change", FileUploaderDNEFilesChange);
                $(".ButtonSubmitFileUploadDNEFiles").bind("click", MensajeUsuarioRecursoTemporal("Uploading resources, please wait..."));
            }

            function FileUploaderDNEFilesChange() {
                $(".ButtonSubmitFileUploadDNEFiles").click();
                MensajeUsuarioRecursoTemporal("Loading resource information...");
            }


            function MensajeUsuarioRecursoTemporal(mensaje) {
                //$(".contenedorGrilla .overlayRecursos .mensajeCargandoRecursos").html(mensaje);
                //$(".contenedorGrilla .overlayRecursos").show();
            }
        </script>
         <dxpc:ASPxPopupControl ID="ASPxPopupControl1" Modal="True"  Width="300px" ShowHeader="False" CloseAction="None" runat="server"  ClientInstanceName="popup" AllowDragging="True" DragElement="Window" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" >
                <ContentCollection>
                    <dxpc:PopupControlContentControl>
                        <div>
                           <div class="ui-jqdialog-titlebar modal-header" style="margin-top:1%;">
                                <span class="ui-jqdialog-title" style=" margin-left:2%; float: left; font-size:small; font-weight:700">
                                <% Response.Write(GetLocalResourceObject("Information"))%>
                                </span>
                                <a class="ui-jqdialog-titlebar-close " style="float: right;right: 0.3em; margin-right:2%; cursor:pointer" onclick="javascript: popup.Hide(); return false;">
                                    <span class="glyphicon glyphicon-remove-circle"></span>
                                </a>
                               <br />
                            </div>
                            <div class="tinfo topinfo"></div>
                                <div style="margin-left:7%; margin-top:2%; margin-bottom:8%;" >
                                    <label>
                                        <% Response.Write(GetLocalResourceObject("ConfirmDelete"))%>
                                    </label>
                                <br/>
                               </div>
                                <hr class="" style="margin:1px"/>
                                <div style="margin-left:42%;margin-bottom:1%;margin-top:1%" >
                                    <table style="height:auto" class="EditTable ui-common-table" >
                                        <tbody>
                                            <tr>
                                                <td class="EditButton">
                                                <dx:ASPxButton ID="yesButton" runat="server" EnableTheming="false"  EnableDefaultAppearance="false"  CssClass="fm-button btn btn-default" AutoPostBack="false"  meta:resourcekey="AspxButtonYes">
                                                     <ClientSideEvents Click="Delete" Init="OnInitAccept"/>
                                                </dx:ASPxButton>
                                                </td>
                                                <td>
                                                <dx:ASPxButton ID="noButton" runat="server"  EnableDefaultAppearance="false" EnableTheming="false" CssClass="fm-button btn btn-default" AutoPostBack="false" meta:resourcekey="AspxButtonNo">
                                                    <ClientSideEvents Click="OnClickNo" Init="OnInit"/>
                                                </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                        </div>

                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>

            <dxpc:ASPxPopupControl ID="ASPxPopupControl2" Modal="True"  Width="300px" ShowHeader="False" CloseAction="None" runat="server"  ClientInstanceName="popupMessage" AllowDragging="True" DragElement="Window" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" >
                <ContentCollection>
                    <dxpc:PopupControlContentControl>
                        <div>
                           <div class="ui-jqdialog-titlebar modal-header" style="margin-top:1%;">
                                <span class="ui-jqdialog-title" style=" margin-left:2%; float: left; font-size:small; font-weight:700">
                                <% Response.Write(GetLocalResourceObject("Information"))%>
                                </span>
                                <a class="ui-jqdialog-titlebar-close " style="float: right;right: 0.3em; margin-right:2%; cursor:pointer" onclick="javascript: popup.Hide(); return false;">
                                    <span class="glyphicon glyphicon-remove-circle"></span>
                                </a>
                               <br />
                            </div>
                            <div class="tinfo topinfo"></div>
                                <div style="margin-left:7%; margin-top:2%; margin-bottom:8%;" >
                                    <label id="errorMessage"></label>
                                <br/>
                               </div>
                                <hr class="" style="margin:1px"/>
                                <div style="margin-left:42%;margin-bottom:1%;margin-top:1%" >
                                    <table style="height:auto" class="EditTable ui-common-table" >
                                        <tbody>
                                            <tr>
                                                <td>
                                                <dx:ASPxButton ID="ASPxButton2" runat="server"  EnableDefaultAppearance="false" EnableTheming="false" CssClass="fm-button btn btn-default" AutoPostBack="false" meta:resourcekey="AspxButtonYes">
                                                    <ClientSideEvents Click="OnClickAccept" Init="OnInit"/>
                                                </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                        </div>

                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
