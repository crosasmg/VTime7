<%@ Page Title="Scheduler" Language="VB" MasterPageFile="~/DropthingsMasterPage.master"
    AutoEventWireup="false" CodeFile="Scheduler.aspx.vb" Inherits="Scheduler" meta:resourcekey="PageResource1" Trace="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        var editingVisibleIndex;
        var OriginType;
        var CompletedAction;
        var TaskID;

        function ShowContextMenu(el, visibleIndex) {
            editingVisibleIndex = visibleIndex;
            popupMenu.ShowAtElement(el);
        }

        function btnNo_Click(s, e) {
            popupMessage.Hide();
        }

        function popupMessageShow(s, e) {
            lblMessage.SetText(TaskGridView.cp_Message);
            popupMessage.Show();
        }

        function OnPopUpCompleteTask() {
            SetButtonsVisible();
        }

        function SetButtonsVisible() {
            ReviewBtn.SetVisible((CompletedAction == 2));
            AcceptBtn.SetVisible((CompletedAction == 3));
            DenyBtn.SetVisible((CompletedAction == 3));
            AcceptButton.SetVisible((CompletedAction == 4));
            CancelBtn.SetVisible((CompletedAction == 4));
        }

        function CompleteTaskButton_Click(sender) {
            var response = MessageTxt.GetText() + '**' + sender
            TaskGridView_PerformCallback(sender, 3, false, response)
            popupCompleteTask.Hide();
        }

        function OnPopup(s, e) {
            if (e.item.index == 0) {
                TaskGridView.cp_Message = "Se está generando el enlace, por favor espere..."
                //TO DO: Revisar por qué se cae cuando se agrega el menu desde código
                //SetItem("itm1", "Loading...", "javascript:popupMessageShow()");
                SetItem('itm1', '<asp:Literal ID="loadingMessageHeader" runat="server" meta:resourcekey="LoadingResource1">Cargando...</asp:Literal>', "javascript:popupMessageShow()");
                TaskGridView.PerformCallback('Link' + '-' + editingVisibleIndex);
            }
            // La ventana para el cometario solo se muestra si el origen de la tarea es por un Workflow
            TaskGridView.GetRowValues(editingVisibleIndex, 'OriginType;CompletedAction;TaskId;', OnGetRowValues);
            MessageTxt.SetText('');
        }

        //Value array contains "OriginType" field values returned from the server
        function OnGetRowValues(values) {
            OriginType = values[0];
            CompletedAction = values[1];
            TaskID = values[2];
        }

        function TaskGridView_PerformCallback(el, status, ShowPopUp, response) {
            //+ Si el estatus a asignar a la tarea es "3-Completa" y el origen de la tarea es "3-WorkFlow"
            //+ se muestra la ventana para colocar el comentario de la tarea
            //+ Si el origen es "3-WorkFlow" y la accion al completar es "1-Ninguna" no se muestra la popup
            if (status == 3) {

                if ((OriginType != 3) || (OriginType == 3 && CompletedAction == 1)) {
                    ShowPopUp = false;
                }

                if (ShowPopUp)
                    popupCompleteTask.ShowAtElement(el);
                else
                    TaskGridView.PerformCallback('Status' + '-' + editingVisibleIndex + '-' + status + '-' + response);
            }
            else
                TaskGridView.PerformCallback('Status' + '-' + editingVisibleIndex + '-' + status + '-' + ' ');
        }

        function TaskGridView_EndCallback() {
            //TO DO: Revisar por qué se cae cuando se agrega el menu desde código
            //SetItem("itm1", "Call Trasaction: " + TaskGridView.cp_VisualTimeTransaction, TaskGridView.cp_link);
            SetItem("itm1", '<asp:Literal ID="executeMessageHeader" runat="server" meta:resourcekey="ExecuteResource1">Ejecutar transacción: </asp:Literal>' + TaskGridView.cp_VisualTimeTransaction, TaskGridView.cp_link);
        }

        function SetItem(name, text, url) {
            var itm = popupMenu.GetItemByName(name);
            itm.SetText(text);
            itm.SetNavigateUrl(url);
        }

        function Uploader_TextChanged() {
            uploader.UploadFile();
        }
        function Uploader_FileUploadStart() {
            scheduler.ShowLoadingPanel();
        }
        function Uploader_FileUploadComplete(s, e) {
            scheduler.HideLoadingPanel();
            scheduler.RaiseCallback("IMPRTAPT|");

            if (e.isValid)
                popupImport.Hide();
        }

        function popupImporterShow(s, e) {
            popupImport.Show();
        }

        function NewTaskButton_Click(s, e) {
            HiddenTODO.Set("NewTask", true);
            scheduler.RaiseCallback("MNUVIEW|NewAppointment");
        }

        function MainScheduler_EndCallBack(s, e) {
            if (HiddenTODO.Get("NewTask") == true && scheduler.cp_CommandSave == true) {
                TaskGridView.PerformCallback('DataBind');
                HiddenTODO.Set("NewTask", false);
                scheduler.cp_CommandSave = false;
            }
        }

        function TaskGridView_DeleteTask(s, e) {
            TaskGridView.PerformCallback('Delete' + '-' + editingVisibleIndex);
        }

        function TaskGridView_UpdateTask(s, e) {
            HiddenTODO.Set("NewTask", true);
            TaskGridView.GetRowValues(editingVisibleIndex, 'OriginType;CompletedAction;TaskId;', OnGetRowValues);
            scheduler.ShowAppointmentFormByClientId(TaskID);
        }

        function TaskGridView_ContextMenu(s, e) {
            if (e.objectType == "header") {
                popupMenuToDo.ShowAtPos(e.htmlEvent.x, e.htmlEvent.y);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table>
        <tr>
            <td width="5%">&nbsp;
            </td>
            <td align="center" colspan="2" width="20%">
                <dxe:ASPxLabel ID="ErrorMsgLabel" runat="server" Visible="False" ForeColor="Red"
                    meta:resourcekey="ErrorMsgLabelResource1" />
            </td>
        </tr>
        <tr style="text-align: left; vertical-align: top">
            <td width="65%">
                <dxwschs:ASPxScheduler ID="MainScheduler" runat="server"
                    OnAppointmentFormShowing="MainScheduler_AppointmentFormShowing"
                    Start="2010-02-05" meta:resourcekey="MainSchedulerResource1"
                    ClientInstanceName="scheduler" ClientIDMode="AutoID">
                    <OptionsForms AppointmentFormTemplateUrl="SchedulerForms\AppointmentGITForm.ascx" />
                    <ClientSideEvents EndCallback="function(s, e) {
	MainScheduler_EndCallBack(s, e);
}"
                        MenuItemClicked="function(s, e) {
	 HiddenTODO.Set('NewTask', false);
}" />
                    <Storage>
                        <Appointments ResourceSharing="True">
                        </Appointments>
                    </Storage>
                    <Views>
                        <DayView>
                            <TimeRulers>
                                <dxschsc:TimeRuler meta:resourcekey="TimeRulerResource1"></dxschsc:TimeRuler>
                            </TimeRulers>
                            <VisibleTime Start="08:00:00" />
                            <WorkTime End="17:00:00" Start="08:00:00" />
                        </DayView>
                        <WorkWeekView>
                            <TimeRulers>
                                <dxschsc:TimeRuler meta:resourcekey="TimeRulerResource2"></dxschsc:TimeRuler>
                            </TimeRulers>
                        </WorkWeekView>
                    </Views>
                </dxwschs:ASPxScheduler>

                <asp:ObjectDataSource ID="EventsDataSource" runat="server" DataObjectTypeName="InMotionGIT.Agenda.Contracts.TaskByOwnerView"
                    DeleteMethod="DeleteEventMethodHandler" InsertMethod="InsertEventMethodHandler"
                    SelectMethod="SelectEventMethodHandler" TypeName="AgendaBinding" UpdateMethod="UpdateEventMethodHandler"
                    OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            </td>
            <td>
                <dxwschs:ASPxDateNavigator ID="ASPxDateNavigator1" runat="server" MasterControlID="MainScheduler"
                    Width="100%" meta:resourcekey="ASPxDateNavigator1Resource1">
                    <Properties ShowTodayButton="False">
                    </Properties>
                </dxwschs:ASPxDateNavigator>
                <table width="100%" cellpadding="8" cellspacing="8">
                    <tr>
                        <td>
                            <dxe:ASPxHyperLink ID="OwnerLink" runat="server" Text="Tareas o eventos asignados por mi" meta:resourcekey="OwnerLink">
                            </dxe:ASPxHyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxHyperLink ID="RelationshipLink" runat="server" Text="Tareas o eventos de mis supervisados" meta:resourcekey="RelationshipLink">
                            </dxe:ASPxHyperLink>
                        </td>
                    </tr>
                </table>
                <table style="width:100%">
                    <tr>
                        <td style="width:60%" >
                            <dxe:ASPxButton ID="NewTask" runat="server" Text="Nueva tarea" Width="100px"
                                AutoPostBack="False" meta:resourcekey="ASPxButton4Resource1">
                                <ClientSideEvents Click="function(s, e) {
  NewTaskButton_Click(s, e)
}" />
                            </dxe:ASPxButton>
                        </td>
                        <td style="width:20%">
                            <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Export" Width="60px" AutoPostBack="False"
                                meta:resourcekey="ASPxButton2Resource1" PostBackUrl="~/dropthings/Scheduler/Scheduler.aspx">
                                <ClientSideEvents Click="function(s, e) {
	_aspxNavigateUrl(&quot;Scheduler.aspx?Scheduler.ics&quot;, &quot;&quot;);
}" />
                            </dxe:ASPxButton>
                        </td>
                        <td style="width:20%">
                            <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Import" Width="60px" AutoPostBack="False"
                                meta:resourcekey="ASPxButton3Resource1">
                                <ClientSideEvents Click="function(s, e) {
	popupImporterShow();
}" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
                <dxwgv:ASPxGridView ID="TaskGridView" runat="server" AutoGenerateColumns="False"
                    Caption="To Do"
                    Width="100%" DataSourceID="TaskDataSource" ClientInstanceName="TaskGridView"
                    meta:resourcekey="TaskGridViewResource1">
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                    <ClientSideEvents EndCallback="function(s, e) { TaskGridView_EndCallback(); }" ContextMenu="function(s, e) {
	TaskGridView_ContextMenu(s, e)
}" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="TaskId" Visible="False" VisibleIndex="0"
                            meta:resourcekey="GridViewDataTextColumnResource1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataImageColumn Caption="!" FieldName="Priority" VisibleIndex="1"
                            meta:resourcekey="GridViewDataImageColumnResource1">
                            <PropertiesImage ImageUrlFormatString="~/images/16x16/Indicators/Priority/{0}.gif">
                            </PropertiesImage>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Task Subject" FieldName="TaskShortDescription"
                            VisibleIndex="2" Width="99%" meta:resourcekey="GridViewDataTextColumnResource2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Name='Status' FieldName='Status' Caption='Status'
                            Visible='True' VisibleIndex="3" meta:resourcekey="GridViewDataComboBoxColumnResource1">
                            <PropertiesComboBox TextField="Description" ValueField="Status"
                                ValueType="System.Int32" Width="200px" ClientInstanceName="Status" DropDownStyle="DropDownList">
                                <Items>
                                    <dxe:ListEditItem Text="Not Initiated" Value="1"
                                        meta:resourcekey="ListEditItemResource4" />
                                    <dxe:ListEditItem Text="Pending" Value="2"
                                        meta:resourcekey="ListEditItemResource5" />
                                    <dxe:ListEditItem Text="Completed" Value="3"
                                        meta:resourcekey="ListEditItemResource6" />
                                    <dxe:ListEditItem Text="Waiting" Value="4"
                                        meta:resourcekey="ListEditItemResource7" />
                                    <dxe:ListEditItem Text="Deferred" Value="5"
                                        meta:resourcekey="ListEditItemResource8" />
                                    <dxe:ListEditItem Text="Cancelled" Value="6"
                                        meta:resourcekey="ListEditItemResource9" />
                                </Items>
                            </PropertiesComboBox>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataColumn VisibleIndex="4" meta:resourcekey="GridViewDataColumnResource1">
                            <DataItemTemplate>
                                <a onclick="ShowContextMenu(this, <%# Container.VisibleIndex %>)" href="#">
                                    <dxe:ASPxImage ID="actionsImage" meta:resourcekey="actionsImageResource1" runat="server" ImageUrl="~/images/16x16/General/action.png" ToolTip="" />
                                </a>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="VisualTimeTransaction" Visible="False" VisibleIndex="5"
                            meta:resourcekey="GridViewDataTextColumnResource3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="VisualTimeTransactionAction" Visible="False"
                            VisibleIndex="6" meta:resourcekey="GridViewDataTextColumnResource4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="OriginType" Visible="False" VisibleIndex="7"
                            meta:resourcekey="GridViewDataTextColumnResource5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CompletedAction" Visible="False" VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
    </table>
    <dx:ASPxHiddenField ID="HiddenTODO" ClientInstanceName="HiddenTODO" runat="server">
    </dx:ASPxHiddenField>
    <asp:ObjectDataSource ID="TaskDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectTaskMethodHandler" TypeName="AgendaBinding"
        DataObjectTypeName="InMotionGIT.Agenda.Contracts.TaskByOwnerView"
        DeleteMethod="DeleteEventMethodHandler" InsertMethod="InsertEventMethodHandler"
        UpdateMethod="UpdateEventMethodHandler"></asp:ObjectDataSource>
    <dxm:ASPxPopupMenu ID="popupMenu" runat="server" ClientInstanceName="popupMenu"
        SeparatorColor="Transparent" SeparatorHeight="14px" SeparatorWidth="2px" ShowPopOutImages="True"
        meta:resourcekey="popupMenuResource1">
        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
        <SubMenuStyle GutterWidth="17px" />
        <RootItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
        <ClientSideEvents PopUp="OnPopup" />
        <Items>
            <dxm:MenuItem Name="itm1" Text="Loading..." NavigateUrl="#" meta:resourcekey="MenuItemResource1">
            </dxm:MenuItem>
            <dxm:MenuItem Name="itm2" Text="Change status" meta:resourcekey="MenuItemResource2">
                <Items>
                    <%--                                     <dxm:MenuItem Text="None" NavigateUrl="javascript:TaskGridView_PerformCallback(0);">
                    </dxm:MenuItem>--%>
                    <dxm:MenuItem Text="NotInitiated" NavigateUrl="javascript:TaskGridView_PerformCallback(this,1);"
                        meta:resourcekey="MenuItemResource3">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="Pending" NavigateUrl="javascript:TaskGridView_PerformCallback(this,2);"
                        meta:resourcekey="MenuItemResource4">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="Completed" NavigateUrl="javascript:TaskGridView_PerformCallback(this,3,true);"
                        meta:resourcekey="MenuItemResource5">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="Waiting" NavigateUrl="javascript:TaskGridView_PerformCallback(this,4);"
                        meta:resourcekey="MenuItemResource6">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="Deferred" NavigateUrl="javascript:TaskGridView_PerformCallback(this,5);"
                        meta:resourcekey="MenuItemResource7">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="Cancelled" NavigateUrl="javascript:TaskGridView_PerformCallback(this,6);"
                        meta:resourcekey="MenuItemResource8">
                    </dxm:MenuItem>
                </Items>
            </dxm:MenuItem>
            <dxm:MenuItem Name="itm3" Text="Update" NavigateUrl="javascript:TaskGridView_UpdateTask(this);"
                meta:resourcekey="MenuItemResource9">
            </dxm:MenuItem>
            <dxm:MenuItem Name="itm4" Text="Delete" NavigateUrl="javascript:TaskGridView_DeleteTask(this);"
                meta:resourcekey="MenuItemResource10">
            </dxm:MenuItem>
        </Items>
    </dxm:ASPxPopupMenu>

    <br />

    <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter"
        ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupMessage"
        runat="server" ClientInstanceName="popupMessage" Modal="True" EnableTheming="False"
        meta:resourcekey="popupMessageResource1">
        <ModalBackgroundStyle>
            <BackgroundImage HorizontalPosition="center"></BackgroundImage>
        </ModalBackgroundStyle>
        <HeaderTemplate>
            <div>
                <asp:Literal ID="popupMessageHeader" runat="server" meta:resourcekey="popupMessageHeaderResource1">Message</asp:Literal></div>
        </HeaderTemplate>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server"
                meta:resourcekey="PopupControlContentControl2Resource1">
                <div style="width: 350px">
                    <table>
                        <tr>
                            <td rowspan="2">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/generaluse/exclamation.png"
                                    meta:resourcekey="ASPxImage1Resource1">
                                </dxe:ASPxImage>
                            </td>
                            <td>
                                <dxe:ASPxLabel ID="lblMessage" runat="server" ClientInstanceName="lblMessage" Text="" EnableClientSideAPI="true"
                                    meta:resourcekey="lblMessageResource1">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td style="width: 100%"></td>
                            <td>
                                <dxe:ASPxButton ID="btnNo" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnNo"
                                    EnableDefaultAppearance="False" meta:resourcekey="btnNoResource1">
                                    <Image Url="../../images/generaluse/btncanceloff.gif" UrlChecked="../../images/generaluse/btncancelon.gif"
                                        UrlPressed="../../images/generaluse/btncancelon.gif" />
                                    <ClientSideEvents Click="btnNo_Click" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupCompleteTask"
        runat="server" ClientInstanceName="popupCompleteTask" Modal="True"
        meta:resourcekey="popupCompleteTaskResource1">
        <ModalBackgroundStyle>
            <BackgroundImage HorizontalPosition="center"></BackgroundImage>
        </ModalBackgroundStyle>
        <HeaderTemplate>
            <div>Complete Task</div>
        </HeaderTemplate>
        <ClientSideEvents PopUp="OnPopUpCompleteTask" />
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"
                EnableHotTrack="False" meta:resourcekey="PopupControlContentControl1Resource1">
                <div style="width: 450px">
                    <table style="width: 100%;">
                        <tr valign="top">
                            <td style="width: 50%">
                                <table align="right">
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="ReviewBtn" runat="server" Text="Revisar" VerticalAlign="Top"
                                                Visible="True" AutoPostBack="False" ClientInstanceName="ReviewBtn" meta:resourcekey="ReviewBtnResource1">
                                                <ClientSideEvents Click="function(s, e) { CompleteTaskButton_Click('ReviewBtn'); }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td align="left">
                                            <dxe:ASPxButton ID="AcceptBtn" runat="server" Text="Aceptar" VerticalAlign="Top"
                                                Visible="True" AutoPostBack="False" ClientInstanceName="AcceptBtn" meta:resourcekey="AcceptBtnResource1">
                                                <ClientSideEvents Click="function(s, e) { CompleteTaskButton_Click('AcceptBtn'); }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="DenyBtn" runat="server" Text="Denegar" VerticalAlign="Top" Visible="True"
                                                AutoPostBack="False" ClientInstanceName="DenyBtn" meta:resourcekey="DenyBtnResource1">
                                                <ClientSideEvents Click="function(s, e) { CompleteTaskButton_Click('DenyBtn'); }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td align="left">
                                            <dxe:ASPxButton ID="AcceptButton" runat="server" Text="Aceptar" VerticalAlign="Top"
                                                Visible="True" AutoPostBack="False" ClientInstanceName="AcceptButton" meta:resourcekey="AcceptButtonResource1">
                                                <ClientSideEvents Click="function(s, e) { CompleteTaskButton_Click('AcceptButton'); }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="CancelBtn" runat="server" Text="Cancelar" VerticalAlign="Top"
                                                Visible="True" AutoPostBack="False" ClientInstanceName="CancelBtn" meta:resourcekey="CancelBtnResource1">
                                                <ClientSideEvents Click="function(s, e) { CompleteTaskButton_Click('CancelBtn'); }" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%">
                                <dxe:ASPxMemo ID="MessageTxt" runat="server" Height="64px" Visible="True" ClientInstanceName="MessageTxt"
                                    Width="250px" meta:resourcekey="MessageTxtResource1">
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                    </table>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="popupImport" runat="server" EnableHotTrack="False" HeaderText="Importar calendario"
        Width="641px" ClientInstanceName="popupImport" EnableClientSideAPI="True" meta:resourcekey="popupImportResource1">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="lblImportFile" runat="server"
                                Text="Seleccione el archivo a importar" meta:resourcekey="lblImportFileResource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxCheckBox ID="chkDelete" runat="server" Text="Eliminar citas existentes antes de eliminar"
                                meta:resourcekey="chkDeleteResource1">
                            </dxe:ASPxCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <dx:ASPxUploadControl ID="ucUploadCalendar" runat="server" ClientInstanceName="uploader">
                                <ClientSideEvents TextChanged="function(s, e) { Uploader_TextChanged(); }" FileUploadComplete="function(s, e) {Uploader_FileUploadComplete(s, e); }" FileUploadStart="function(s, e) {Uploader_FileUploadStart(); }" />
                                <ValidationSettings AllowedFileExtensions=".ics">
                                </ValidationSettings>
                            </dx:ASPxUploadControl>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>

    <dxm:ASPxPopupMenu ID="ASPxPopupMenuToDo" runat="server" ClientInstanceName="popupMenuToDo"
        SeparatorColor="Transparent" SeparatorHeight="14px" SeparatorWidth="2px" ShowPopOutImages="True"
        meta:resourcekey="popupMenuResource1">
        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
        <SubMenuStyle GutterWidth="17px" />
        <RootItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
        <Items>
            <dxm:MenuItem Name="itm1" Text="Nueva Tarea" NavigateUrl="javascript:NewTaskButton_Click();"
                meta:resourcekey="MenuItemResource11">
            </dxm:MenuItem>
        </Items>
    </dxm:ASPxPopupMenu>

    <dxpc:ASPxPopupControl ID="OwnerPopup" runat="server" Height="100px" PopupElementID="OwnerLink"
        Width="1080px" AllowDragging="True" AllowResize="True"
        HeaderText="Tareas o eventos asignados por mi" CloseAction="CloseButton"
        PopupHorizontalAlign="OutsideLeft" PopupHorizontalOffset="20" meta:resourcekey="OwnerPopupPopup">
        <ClientSideEvents Shown="function(s, e) {
	RelationshipTaskGridView2.PerformCallback('Bind');
}" />
        <ContentStyle>
            <Paddings Padding="0px" />
            <Border BorderWidth="0px" />
        </ContentStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server"
                SupportsDisabledAttribute="True">
                <dxwgv:ASPxGridView ID="RelationshipTaskGridView2" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="RelationshipTaskGridView2" EnableViewState="False"
                    Width="100%" meta:resourcekey="RelationshipTaskGridView2Resource1">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="TaskId" ShowInCustomizationForm="True" Visible="False"
                            VisibleIndex="1" meta:resourcekey="GridViewDataTextColumnResource1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="RelationShipUserId" ShowInCustomizationForm="True"
                            Visible="False" VisibleIndex="2"
                            meta:resourcekey="GridViewDataTextColumnResource2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="UserId" ShowInCustomizationForm="True" Visible="False"
                            VisibleIndex="3" meta:resourcekey="GridViewDataTextColumnResource3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Level" ShowInCustomizationForm="True" Visible="False"
                            VisibleIndex="4" meta:resourcekey="GridViewDataTextColumnResource4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataImageColumn Caption="!" FieldName="Priority" ShowInCustomizationForm="True"
                            ToolTip="Prioridad que tiene la tarea" VisibleIndex="5" Width="8px"
                            meta:resourcekey="GridViewDataImageColumnResource1">
                            <PropertiesImage ImageUrlFormatString="~/images/16x16/Indicators/Priority/{0}.gif">
                            </PropertiesImage>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn Caption=" " FieldName="RecordType" ShowInCustomizationForm="True"
                            ToolTip="Tipo de registro, evento o tarea" VisibleIndex="6" Width="8px"
                            meta:resourcekey="GridViewDataImageColumnResource2">
                            <PropertiesImage ImageUrlFormatString="~/images/16x16/Indicators/TaskRecordType/RecordType_{0}.png">
                            </PropertiesImage>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn Caption=" " FieldName="OriginType" ShowInCustomizationForm="True"
                            ToolTip="Tipo de programa o persona que originó el evento o tarea" VisibleIndex="7"
                            Width="8px" meta:resourcekey="GridViewDataImageColumnResource3">
                            <PropertiesImage ImageUrlFormatString="~/images/16x16/Indicators/TaskOrigenType/OriginType_{0}.png">
                            </PropertiesImage>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Creación" FieldName="CreationDate" ShowInCustomizationForm="True"
                            ToolTip="Día y hora en fue creado la tarea o evento" VisibleIndex="8"
                            Width="130px" meta:resourcekey="GridViewDataDateColumnResource1">
                            <PropertiesDateEdit DisplayFormatString="g">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Estado" FieldName="StatusDescript" ShowInCustomizationForm="True"
                            ToolTip="Estado de la actividad o evento" VisibleIndex="9" Width="60px"
                            meta:resourcekey="GridViewDataTextColumnResource5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="%" FieldName="PercentageCompleted" ShowInCustomizationForm="True"
                            ToolTip="Porcentaje que ha sido realizado del total de la actividad o tarea"
                            VisibleIndex="10" Width="20px"
                            meta:resourcekey="GridViewDataTextColumnResource6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Usuario" FieldName="UserName" ShowInCustomizationForm="True"
                            ToolTip="usuario asociado a la actividad" VisibleIndex="11" Width="130px"
                            meta:resourcekey="GridViewDataTextColumnResource7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Descripción" FieldName="TaskShortDescription"
                            ShowInCustomizationForm="True" ToolTip="Descripción breve de la actividad o tarea"
                            VisibleIndex="12" Width="200px"
                            meta:resourcekey="GridViewDataTextColumnResource8">
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataDateColumn Caption="Inicio"
                            FieldName="StartingDateTime" ShowInCustomizationForm="True"
                            ToolTip="Día y hora en que debe comenzar el evento"
                            VisibleIndex="11" Width="130px"
                            meta:resourcekey="GridViewDataDateColumnResource2">
                            <PropertiesDateEdit DisplayFormatString="g">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Fin" FieldName="EndingDateTime" ShowInCustomizationForm="True"
                            ToolTip="Día y hora en que debe terminar el evento"
                            VisibleIndex="12" Width="130px"
                            meta:resourcekey="GridViewDataDateColumnResource3">
                            <PropertiesDateEdit DisplayFormatString="g">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataDateColumn>
                    </Columns>
                    <Templates>
                        <PreviewRow>
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True"
                                Text='<%# Eval("TaskLongDescription") %>'
                                meta:resourcekey="ASPxLabel1Resource1">
                            </dxe:ASPxLabel>
                        </PreviewRow>
                    </Templates>
                </dxwgv:ASPxGridView>
                <table width="100%" cellpadding="15" cellspacing="15">
                    <tr>
                        <td style="width: 1%">
                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Filtro:" Font-Bold="True"
                                meta:resourcekey="ASPxLabel5Resource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 1%">
                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Usuario:"
                                meta:resourcekey="ASPxLabel6Resource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 4%">
                            <dxe:ASPxComboBox ID="UserFilter2" runat="server"
                                EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                TextField="Description" ValueField="Code" ValueType="System.Int32"
                                EnableIncrementalFiltering="True" meta:resourcekey="UserFilter2Resource1">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
    RelationshipTaskGridView2.Refresh();
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="width: 16px">&nbsp;
                        </td>
                        <td style="width: 1%">
                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Estado:"
                                meta:resourcekey="ASPxLabel7Resource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 4%">
                            <dxe:ASPxComboBox ID="StatusFilter2" runat="server"
                                EnableCallbackMode="True" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                TextField="DESCRIPTION" ValueField="STATUS" ValueType="System.Int32"
                                meta:resourcekey="StatusFilter2Resource1">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
    RelationshipTaskGridView2.Refresh();
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td style="width: 90%" align="right">
                            <dxe:ASPxCheckBox ID="ShowPreview2" runat="server" CheckState="Unchecked"
                                Text="Mostrar detalle de la tarea o evento"
                                meta:resourcekey="ShowPreview2Resource1">
                                <ClientSideEvents CheckedChanged="function(s, e) {

    	RelationshipTaskGridView2.Refresh();
}" />
                            </dxe:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="RelationShipPopup" runat="server" Height="100px" PopupElementID="relationshipLink"
        Width="1080px" AllowDragging="True" AllowResize="True"
        HeaderText="Tareas o eventos de mis supervisados" CloseAction="CloseButton"
        PopupHorizontalAlign="OutsideLeft" PopupHorizontalOffset="20"
        meta:resourcekey="RelationShipPopupResource1">
        <ClientSideEvents Shown="function(s, e) {
	RelationshipTaskGridView.PerformCallback('Bind');
}" />
        <ContentStyle>
            <Paddings Padding="0px" />
            <Border BorderWidth="0px" />
        </ContentStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl4"
                runat="server" SupportsDisabledAttribute="True"
                meta:resourcekey="PopupControlContentControl4Resource1">
                <dxwgv:ASPxGridView ID="RelationshipTaskGridView" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="RelationshipTaskGridView" EnableViewState="False"
                    Width="100%" meta:resourcekey="RelationshipTaskGridViewResource1">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="TaskId" ShowInCustomizationForm="True" Visible="False"
                            VisibleIndex="1" meta:resourcekey="GridViewDataTextColumnResource1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="RelationShipUserId" ShowInCustomizationForm="True"
                            Visible="False" VisibleIndex="2"
                            meta:resourcekey="GridViewDataTextColumnResource2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="UserId" ShowInCustomizationForm="True" Visible="False"
                            VisibleIndex="3" meta:resourcekey="GridViewDataTextColumnResource3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Level" ShowInCustomizationForm="True" Visible="False"
                            VisibleIndex="4" meta:resourcekey="GridViewDataTextColumnResource4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataImageColumn Caption="!" FieldName="Priority" ShowInCustomizationForm="True"
                            ToolTip="Prioridad que tiene la tarea" VisibleIndex="5" Width="8px"
                            meta:resourcekey="GridViewDataImageColumnResource1">
                            <PropertiesImage ImageUrlFormatString="~/images/16x16/Indicators/Priority/{0}.gif">
                            </PropertiesImage>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn Caption=" " FieldName="RecordType" ShowInCustomizationForm="True"
                            ToolTip="Tipo de registro, evento o tarea" VisibleIndex="6" Width="8px"
                            meta:resourcekey="GridViewDataImageColumnResource2">
                            <PropertiesImage ImageUrlFormatString="~/images/16x16/Indicators/TaskRecordType/RecordType_{0}.png">
                            </PropertiesImage>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn Caption=" " FieldName="OriginType" ShowInCustomizationForm="True"
                            ToolTip="Tipo de programa o persona que originó el evento o tarea" VisibleIndex="7"
                            Width="8px" meta:resourcekey="GridViewDataImageColumnResource3">
                            <PropertiesImage ImageUrlFormatString="~/images/16x16/Indicators/TaskOrigenType/OriginType_{0}.png">
                            </PropertiesImage>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Creación" FieldName="CreationDate" ShowInCustomizationForm="True"
                            ToolTip="Día y hora en fue creado la tarea o evento" VisibleIndex="8"
                            Width="130px" meta:resourcekey="GridViewDataDateColumnResource1">
                            <PropertiesDateEdit DisplayFormatString="g">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Estado" FieldName="StatusDescript" ShowInCustomizationForm="True"
                            ToolTip="Estado de la actividad o evento" VisibleIndex="9" Width="60px"
                            meta:resourcekey="GridViewDataTextColumnResource5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="%" FieldName="PercentageCompleted" ShowInCustomizationForm="True"
                            ToolTip="Porcentaje que ha sido realizado del total de la actividad o tarea"
                            VisibleIndex="10" Width="20px"
                            meta:resourcekey="GridViewDataTextColumnResource6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Usuario" FieldName="UserName" ShowInCustomizationForm="True"
                            ToolTip="usuario asociado a la actividad" VisibleIndex="11" Width="130px"
                            meta:resourcekey="GridViewDataTextColumnResource7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Descripción" FieldName="TaskShortDescription"
                            ShowInCustomizationForm="True" ToolTip="Descripción breve de la actividad o tarea"
                            VisibleIndex="12" Width="200px"
                            meta:resourcekey="GridViewDataTextColumnResource8">
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataDateColumn Caption="Inicio"
                            FieldName="StartingDateTime" ShowInCustomizationForm="True"
                            ToolTip="Día y hora en que debe comenzar el evento"
                            VisibleIndex="11" Width="130px"
                            meta:resourcekey="GridViewDataDateColumnResource2">
                            <PropertiesDateEdit DisplayFormatString="g">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Fin" FieldName="EndingDateTime" ShowInCustomizationForm="True"
                            ToolTip="Día y hora en que debe terminar el evento"
                            VisibleIndex="12" Width="130px"
                            meta:resourcekey="GridViewDataDateColumnResource3">
                            <PropertiesDateEdit DisplayFormatString="g">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxwgv:GridViewDataDateColumn>

                        <dxwgv:GridViewDataTextColumn Caption="Supervisor" FieldName="RelationShipUserName"
                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="14"
                            Width="130px" meta:resourcekey="GridViewDataTextColumnResource9">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <PreviewRow>
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True"
                                Text='<%# Eval("TaskLongDescription") %>'
                                meta:resourcekey="ASPxLabel1Resource1">
                            </dxe:ASPxLabel>
                        </PreviewRow>
                    </Templates>
                </dxwgv:ASPxGridView>
                <table width="100%" cellpadding="15" cellspacing="15">
                    <tr>
                        <td style="width: 1%">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Filtro:" Font-Bold="true"
                                meta:resourcekey="ASPxLabel4Resource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 1%">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Usuario:"
                                meta:resourcekey="ASPxLabel2Resource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 4%">
                            <dxe:ASPxComboBox ID="UserFilter" runat="server"
                                EnableCallbackMode="true" IncrementalFilteringMode="StartsWith"
                                TextField="Description" ValueField="Code" ValueType="System.Int32"
                                meta:resourcekey="UserFilterResource1">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
    RelationshipTaskGridView.Refresh();
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="width: 16px">&nbsp;
                        </td>
                        <td style="width: 1%">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Estado:"
                                meta:resourcekey="ASPxLabel3Resource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 4%">
                            <dxe:ASPxComboBox ID="StatusFilter" runat="server"
                                EnableCallbackMode="True" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                TextField="DESCRIPTION" ValueField="STATUS" ValueType="System.Int32"
                                meta:resourcekey="StatusFilterResource1">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
    RelationshipTaskGridView.Refresh();
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td style="width: 90%" align="right">
                            <dxe:ASPxCheckBox ID="ShowPreview" runat="server" CheckState="Unchecked"
                                Text="Mostrar detalle de la tarea o evento"
                                meta:resourcekey="ShowPreviewResource1">
                                <ClientSideEvents CheckedChanged="function(s, e) {

    	RelationshipTaskGridView.Refresh();
}" />
                            </dxe:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</asp:Content>