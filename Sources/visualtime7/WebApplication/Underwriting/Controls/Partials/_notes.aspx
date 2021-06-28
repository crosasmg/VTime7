<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_notes.aspx.vb" Inherits="Underwriting_Controls_Partials_notes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Styles/font-awesome.css" rel="stylesheet" />
    <link href="../../../Styles/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../../Styles/bootstrap.min.css" rel="stylesheet" />
               <style>
                   .modal-header {
                       border-bottom: 3px solid #428bca  ;
                   }
               </style>
    <script src="../../../Scripts/jquery.min.js"></script>
    <script src="../../../Scripts/jquery-ui.min.js"></script>
    <script src="../../../Scripts/bootstrap.min.js"></script>
	<asp:PlaceHolder runat="server" >
		<link href="../../../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
		<script src="../../../Scripts/fasi.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Scripts\fasi.js").ToString("yyyyMMddHHmmss")%>"></script>
	</asp:PlaceHolder>
    <script type="text/javascript">
        var index = -1;
        var editMode = 0;


        function OnInitAccept(s, e) {
            s.GetTextContainer().className += " fa fa-pencil-square-o";
        }

        function OnInit(s, e) {
            s.GetTextContainer().className += " fa fa-times-circle-o";
        }


        function parseToHtml(str) {
            html = $.parseHTML(str);
            return html;
        }

        function showModalContentNote(value, title) {
            //console.log(title);
            valueHtml = $("#" + value).html()
            $('#textBodyNotes').html(valueHtml);
            $('#textTitleNotes').html(title);
            $('#myModalNotes').modal('show');
        }

        function ValidationHandler(s, e) {
            if (s.GetHtml().length > 5000) {
                e.isValid = false;
                e.errorText = "Argument was out of range of valid values. top 5000/ La cantidad de caracteres de la nota excede el máximo. 5000"
            } else {
                e.isValid = true;
            }
        }
        function HtmlChangedHandler(s, e) {
            //ContentLength.SetText(s.GetHtml().length);
        }

        function OnInit(s, e) {
            s.GetTextContainer().className += " glyphicon glyphicon-remove-circle";
        }

        function initEdit(s, e) {
            if (s.GetValue() != null) {
                editMode = 1;
                s.SetEnabled(false);
            }
        }

        function dateInit(s, e) {
            if (editMode == 1) {
                s.SetEnabled(false);
            }
            // Fix "01/01/0100" Date
            // Copy of Original "ToggleTextDecoration"
            s.ToggleTextDecoration = function () {
                if (this.readOnly) return;
                if (!this.HasTextDecorators()) return;
                if (this.GetDate() == null) return; // <-- Fix on NULL value
                if (this.focused) {
                    var input = this.GetInputElement();
                    var oldValue = input.value;
                    var sel = _aspxGetSelectionInfo(input);
                    this.ToggleTextDecorationCore();
                    if (oldValue != input.value) {
                        if (sel.startPos == 0 && sel.endPos == oldValue.length)
                            sel.endPos = input.value.length;
                        else
                            sel.endPos = sel.startPos;
                        _aspxSetInputSelection(input, sel.startPos, sel.endPos);
                    }
                } else {
                    this.ToggleTextDecorationCore();
                }
            };
        }

        function Delete(s, e) {
            popup.Hide();
            if (index > -1) {
                GridViewNotes.DeleteRow(index);
                index = -1;
            }
        }
        function OnClickNo(s, e) {
            popup.Hide();
        }


        function VerifySelectedDateIsGreaterThanToday(selectedDate) {
            var currentDate = new Date();
            //selectedDate = ExpirationDateTextId.GetDate();
            if (((selectedDate != null) && (selectedDate > currentDate)) || (selectedDate == null)) {
                return true;
            }
            return false;
        }

    </script>
</head>
<body class="newbusiness">
    <form id="form1" runat="server">
        <%--<dx:ASPxLabel ID='IdLabel' EncodeHtml='false' ClientInstanceName='IdLabel' runat='server' ClientIDMode='Static' meta:resourcekey="IdLabelResource" Text="Sequence id" ClientEnabled='true' ClientVisible='true' AssociatedControlID='Id'></dx:ASPxLabel>--%>
        <%-- </td>   
        <br>
        <td style='width: 50%;' align='left'>--%>
        <div class="filterBox" style="margin-top:15px;">
            <asp:Label ID="FilterLabel" runat="server" Text="Etiquetas:" Style="display: inline;" Height="26px" meta:resourcekey="LabelTags" />
            <asp:Panel ID="tagFilterContainer" CssClass="tagFilterContainer" Style="display: inline;" runat="server"></asp:Panel>
            &nbsp;&nbsp;&nbsp; 
            <asp:LinkButton ID="ButtonFilter" runat="server" OnClick="FilterButton" Text="Filtrar" Height="26px" meta:resourcekey="ButtonFilter" />
        </div>  
        <div style="line-height:20px;">
        <br>
        </div>
        <div>
            <%--     <dx:ASPxHyperLink ID="AspxHyperLinkNew" runat="server" Cursor="pointer" Text="New Note" meta:resourcekey="HyperLinkNewResource">
                <ClientSideEvents Click="function(s, e) { GridViewNotes.AddNewRow(); }" />
            </dx:ASPxHyperLink>--%>
            <dx:ASPxButton ID="AspxHyperLinkNew" AutoPostBack="false" runat="server" Cursor="pointer" Text="New Note" meta:resourcekey="HyperLinkNewResource" Visible ="false">
                <ClientSideEvents Click="function(s, e) { GridViewNotes.AddNewRow(); }" />
            </dx:ASPxButton>
        </div>
        <dxlp:ASPxLoadingPanel ID="panel" ClientInstanceName="panel" runat="server" Modal="true"></dxlp:ASPxLoadingPanel>
        <asp:ObjectDataSource ID="DataSourceNotes" runat="server" SelectMethod="" TypeName="Inmotiongit.Datosnoestruct.Proxy.DNE.OperationContracts"></asp:ObjectDataSource>
        <dx:ASPxGridView  ID="GridViewNotes" runat="server" ButtonType EnableCallBacks ="false" AutoGenerateColumns="false" OnCustomUnboundColumnData="GridViewNotes_CustomUnboundColumnData" ClientIDMode="Predictable" DataSourceID="DataSourceNotes" ClientInstanceName="GridViewNotes" Width="100%" KeyFieldName="SequenceId;ConsequenceId" OnRowInserting="GridViewNotes_rowinserting"   OnRowUpdating="GridViewNotes_rowupdating" OnRowDeleting="GridViewNotes_rowdeleting" meta:resourcekey="GridViewNotes"  Enabled="True" Settings-EnableFilterControlPopupMenuScrolling="false">
            <SettingsLoadingPanel Mode="ShowAsPopup"  />
            <ClientSideEvents  
                
                CustomButtonClick="function(s, e) {
                    panel.Show();
		            if (e.buttonID == 'btnDelete')
                                {
                            panel.Hide(); 
                                index = e.visibleIndex;
			            popup.Show();
                                }
                            panel.Hide(); 
		            }" 
                
            />
            <SettingsPager PageSize="5">
            </SettingsPager>
            <SettingsEditing EditFormColumnCount="3" Mode="editform" />
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="11" Width="4%" Caption=" " ButtonType="Image">
                    <EditButton Visible="true"> 
                        <Image Url="../../../VTimeNet/Images/editBootstrap.png"></Image>
                    </EditButton>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Eliminar" meta:resourcekey="DeleteCustomButton" >
                            <image Url="../../../VTimeNet/Images/removeBootstrap.png"></image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="ConsequenceId" Caption="Sequence Id" EditFormSettings-Visible="False" VisibleIndex="2" Visible="false" meta:resourcekey="GridViewDataTextColumnSequenceId">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SequenceId" Caption="Consequence Id" EditFormSettings-Visible="False" VisibleIndex="1" Visible="false" meta:resourcekey="GridViewDataTextColumnConnsequenceId">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Name" Caption="Note" Width="15%" VisibleIndex="3" CellStyle-Wrap="True" EditCellStyle-Wrap="True" EditFormCaptionStyle-Wrap="True" HeaderStyle-Wrap="True" PropertiesTextEdit-Style-Wrap="True" meta:resourcekey="GridViewDataTextColumnName">
                    <DataItemTemplate>                   
                        <div style="word-break: break-all">
                            <%# Container.Text %>
                        </div>      
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>    
                <dx:GridViewDataTextColumn FieldName="Content" Caption="Contenido"  CellStyle-Wrap="True"  Width="35%" EditCellStyle-Wrap="True" VisibleIndex="5" meta:resourcekey="GridViewDataTextColumnContent">
                    <EditFormSettings RowSpan="50" />
                    <DataItemTemplate>
                        <div style="height: 32px; overflow: hidden;">
                            <div style="position: relative; top: 5px">
                                <div style="word-break: break-all">
                                    <a href='javascript:showModalContentNote("Note<%#Eval("Note.ConsequenceId") %>","<% Response.Write(GetLocalResourceObject("GridViewDataTextColumnContent")) %>")' style="cursor:pointer"><div id="Note<%#Eval("Note.ConsequenceId") %>"><font color="#00000"><%#Eval("Note.content") %> </font></div></a>     
                                </div>
                            </div>    
                        </div> 
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ExpirationDate" Caption="Fecha de Expiración" CellStyle-Wrap="True" VisibleIndex="4" Width="5%" HeaderStyle-Wrap="True" meta:resourcekey="GridViewDataTexColumnExpirationdate">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CreationDate" Caption="Fecha de Creación" Width="5%" VisibleIndex="6" CellStyle-Wrap="True" Name="resource.CreationDate" HeaderStyle-Wrap="True" meta:resourcekey="GridViewDataTexColumnCreationDate">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CreatorUserCode" Caption="Código Usuario Creador" VisibleIndex="7" Visible="False" Name="resource.CreatorUserCode" meta:resourcekey="GridViewDataTexColumnCreatorUserCode">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CreatorUserNameUW" Caption="Usuario Creador" VisibleIndex="8" Name="CreatorUserNameUW" CellStyle-Wrap="True" Width="10%" HeaderStyle-Wrap="True" UnboundType="String" meta:resourcekey="GridViewDataTexColumnCreatorUserName">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="UpdateUserCode" Caption="Código Usuario Modificador" VisibleIndex="9" Visible="False" HeaderStyle-Wrap="True" Name="UpdateUserCode" UnboundType="String" meta:resourcekey="GridViewDataTexColumnUpdateUserCode">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="UpdateUserNameUW" Caption="Modificado Por" VisibleIndex="10" Name="UpdateUserNameUW" CellStyle-Wrap="True" Width="10%" HeaderStyle-Wrap="True" UnboundType="String" meta:resourcekey="GridViewDataTexColumnUpdateUserName">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior ConfirmDelete="True" SortMode="value" />
            <Settings ShowTitlePanel="false" ShowHeaderFilterButton="false" />
            <SettingsText ConfirmDelete="Are you sure you want to delete the selected records?" />
            <Templates>
                
                <EditForm>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 122px; height: 24px;">
                                <asp:Label ID="LabelNameId" runat="server" Text="Note" meta:resourcekey="LabelNameNotes"></asp:Label>
                            </td>
                            <td style="width: 275px; height: 24px;">
                                <dx:ASPxTextBox ID="TextBoxNameId" ClientInstanceName="TextBoxNameId" runat="server" MaxLength="30" EnableClientSideAPI="true" Text='<%#Eval("Name") %>' ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' Width="170px" meta:resourcekey="TextBoxNameId">
                                    <ClientSideEvents Init="initEdit" />
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Name cannot be empty" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 144px; height: 24px;">
                                <asp:Label ID="ExpirationDateLabelId" runat="server" Text="Fecha de Expiración" meta:resourcekey="ExpirationDateLabelNotes"></asp:Label>
                            </td>
                            <td style="height: 24px">
                                <dx:ASPxDateEdit ID="ExpirationDateTextId" EnableViewState ="false" ClientInstanceName="ExpirationDateTextId" runat="server" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" EditFormat="datetime" AllowNull="true" AllowUserInput="true" Value='<%# eval("ExpirationDate") %>' meta:resourcekey="ExpirationDateTextId"  UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                    <ClientSideEvents GotFocus="function(s, e) {if (s.GetValue() == null || s.GetValue() == '' || s.GetValue()=='01/01/0100') {var data=new Date(); s.SetDate(data);} }" Init="dateInit" />
                                    <TimeSectionProperties Visible="false">
                                    </TimeSectionProperties> 
                                    <%--<ClientSideEvents Init="function(s,e){ s.SetDate(new Date());}" />--%>
                                    <%-- <ClientSideEvents Init="function(s,e){ 
                                                               var dt1 = new Date();
                                                               var dt2 = new Date(dt1.getFullYear(), dt1.getMonth(), dt1.getDate()+1);    
                                                               ExpirationDateTextId.SetMinDate(new Date(dt2));                                                                                                                 
                                                            }" />    --%>
                                    <ClientSideEvents Validation="function(s,e){e.isValid = (VerifySelectedDateIsGreaterThanToday(ExpirationDateTextId.GetDate()))}" />
                                    <ValidationSettings CausesValidation="true">
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 122px">&nbsp;</td>
                            <td style="width: 275px">&nbsp;</td>
                            <td style="width: 144px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <asp:Label ID="LabelUserMessage" runat="server" ForeColor="Red" />
                    <dxhe:ASPxHtmlEditor ID="HTMLEditorNotes" EnableViewState="false" ClientInstanceName="HTMLEditorNotes" OnInit="notes_Callback" Settings-AllowDesignView="false" SettingsValidation-ValidationGroup="<%# Container.ValidationGroup %>" runat="server" Html='<%# bind("Note.Content") %>' Height="200px" Width="100%" meta:resourcekey="HtmlEditorNotes">
                        <ClientSideEvents Validation="ValidationHandler" />
                    </dxhe:ASPxHtmlEditor>
                    <table>
                        <tr>
                            <td colspan="4" style="padding:0 15px 0 15px;">
                             <dx:ASPxButton CssClass="fm-button btn btn-default" ID="UpdateNote"  EnableTheming="false" Cursor="pointer" EnableDefaultAppearance="false" AutoPostBack = "false" ClientInstanceName="UpdateNote" runat="server" EnableClientSideAPI="true" meta:resourcekey="AspxUpdateButton">
                                 <ClientSideEvents Init="OnInitAccept" Click="function(s, e) {
                                      GridViewNotes.UpdateEdit(); }"/>
                                </dx:ASPxButton>
                            </td>
                            <td>
                            <dx:ASPxLabel runat="server" Text="    "></dx:ASPxLabel>
                            </td>
                            <td colspan="4" style="padding:0 15px 0 15px;">
                                <dx:ASPxButton ID="CancelNote" CssClass="fm-button btn btn-default" EnableTheming="false" Cursor="pointer" EnableDefaultAppearance="false" AutoPostBack ="false" ClientInstanceName="CancelNote" runat="server" EnableClientSideAPI="true" meta:resourcekey="AspxCancelButton">
                                    <ClientSideEvents Init="OnInit" Click="function(s, e) { 
                                      GridViewNotes.CancelEdit(); }"/>
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <!--
                    <dx:ASPxGridViewTemplateReplacement ID="GridViewTemplateReplacementUpdateId" runat="server" ReplacementType="EditFormUpdateButton" ColumnID="UpdateNote" />
                    <dx:ASPxGridViewTemplateReplacement ID="GridViewTemplateReplacementCancelId" runat="server" ReplacementType="editformcancelbutton" ColumnID="CancelNote"/>
                    -->
                </EditForm>
            </Templates>
        </dx:ASPxGridView>
        <dxpc:ASPxPopupControl ID="ASPxPopupControl1" Width="300px" Modal="True" ShowHeader="False" CloseAction="None" runat="server" ClientInstanceName="popup" AllowDragging="True" DragElement="Window" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above">
            <ContentCollection>
                <dxpc:PopupControlContentControl>
                    <div>
                        <div class="ui-jqdialog-titlebar modal-header" style="margin-top: 1%;">
                            <span class="ui-jqdialog-title" style="margin-left: 2%; float: left; font-size: small; font-weight: 700">
                                <% Response.Write(GetLocalResourceObject("Information"))%>
                            </span>
                            <a class="ui-jqdialog-titlebar-close " style="float: right; right: 0.3em; margin-right: 2%; cursor: pointer" onclick="javascript: popup.Hide(); return false;">
                                <span class="glyphicon glyphicon-remove-circle"></span>
                            </a>
                            <br />
                        </div>
                        <div style="margin-left: 7%; margin-top: 2%; margin-bottom: 8%;">

                                <% Response.Write(GetLocalResourceObject("ConfirmDelete"))%>
                            
                            <br />
                        </div>
                        <hr class="" style="margin: 1px" />
                        <div style="margin-left: 42%; margin-bottom: 1%; margin-top: 1%">
                            <table style="height: auto" class="EditTable ui-common-table">
                                <tbody>
                                    <tr>
                                        <td class="EditButton">                                              
                                            <dx:ASPxButton ID="yesButton"  EnableDefaultAppearance="false" runat="server"  EnableTheming="false" CssClass="fm-button btn btn-default" AutoPostBack="false" meta:resourcekey="AspxButtonYes">
                                                <ClientSideEvents Click="Delete" Init="OnInitAccept"/>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="noButton" runat="server"  EnableDefaultAppearance="false" EnableTheming="false" CssClass="fm-button btn btn-default"  AutoPostBack="false" meta:resourcekey="AspxButtonNo">
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
        

    </form>

        <div id="myModalNotes" class="modal fade" tabindex="-1" role="dialog">
	        <div id="myModalDialogNotes" class="modal-dialog" role="document">
		        <div class="modal-content">
			        <div class="modal-header">
				        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span class="glyphicon glyphicon-remove-circle"></span></button>
				        <h4 id="textTitleNotes" class="modal-title"></h4>
			        </div>
			        <div class="modal-body">
				        <p id="textBodyNotes" style="word-break: break-all;white-space: normal;">

				        </p>
			        </div>
			        <div class="modal-footer">
				        <button type="button" class="btn btn-default"  data-dismiss="modal">Cerrar</button>
			        </div>
		        </div>
	        </div>
        </div>
</body>
</html>
