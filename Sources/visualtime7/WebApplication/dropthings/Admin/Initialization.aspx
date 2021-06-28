<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="Initialization.aspx.vb" Inherits="dropthings_Admin_Initialization"
    meta:resourcekey="PageResource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <% If (DesignMode) Then%>
    <script src="../../Script/ASPxScriptIntelliSense.js" type="text/javascript"></script>
    <% End If%>
    <script type="text/javascript">

        // BEGIN CONFIRMDELETE
        var origin;
        var fasiURL = "<%=fasiURL%>";
        var InitAccessToken = "";

        GetAccessToken();

        function GetAccessToken() {
            $.ajax({
                url: "Initialization.aspx/GetAccessToken",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json"
            }).done(function (response) {
                InitAccessToken = response.d;
            });
        }

        function CleanButton_Click(s, e) {
            origin = 'Clean';

            if (!WorkflowsCheckBox.GetChecked() && !TaskCheckBox.GetChecked() && !RolesAndUsersCheckBox.GetChecked() && !DocumentCacheCheckBox.GetChecked() && !NavigationDirectoryCheckBox.GetChecked() && !AnonymousUsersCheckBox.GetChecked()) {
                popupMessage.Show();
                CloseButton.Focus();
            }
            else {
                popupDelete.Show();
                btnYes.Focus();
            }
        }

        function InitializeButton_Click(s, e) {
            origin = 'Initialize';
            if (!RolesConfigCheckBox.GetChecked() && !RolesInitCheckBox.GetChecked() && !UsersInitCheckBox.GetChecked() && !OneUsersInitCheckBox.GetChecked() && !WidgetsInRolesInitCheckBox.GetChecked() && !CopyUserConfigurationCheckBox.GetChecked()) {
                popupMessage.Show();
                CloseButton.Focus();
            }
            else {
                popupDelete.Show();
                btnYes.Focus();
            }
        }

        function btnYes_Click(s, e) {
            popupDelete.Hide();

            if (origin == 'Clean') {
                CleanFASI();
            }
            else if (origin == 'Initialize') {
                InitializeRoleAndUsers();
            }
        }

        function btnNo_Click(s, e) {
            popupDelete.Hide();
        }

        function CloseButton_Click(s, e) {
            popupMessage.Hide();
        }

        // END CONFIRMDELETE

        function InitializeRoleAndUsers() {
            if (RolesConfigCheckBox.GetChecked() || RolesInitCheckBox.GetChecked() || WidgetsInRolesInitCheckBox.GetChecked())
                InitializingRoles();

            if (UsersInitCheckBox.GetChecked())
                InitializingAllBOUsers();
            else if (OneUsersInitCheckBox.GetChecked())
                InitializingSomeBOUsers();

            if (CopyUserConfigurationCheckBox.GetChecked())
                CopyUserConfiguration();
        }

        function InitializingRoles(){
            InitializeButton.SetEnabled(false);
            HideResultImagesRoles();
            if(RolesConfigCheckBox.GetChecked() || RolesInitCheckBox.GetChecked() || WidgetsInRolesInitCheckBox.GetChecked()){
                
                if(RolesInitCheckBox.GetChecked()){
                    RolesInitLabel.SetVisible(true);
                    RolesInitLabel.SetText('<% Response.Write(GetLocalResourceObject("MessageRoleInitProcess"))%>');
                    RolesInitImageWarning.SetVisible(true);
                }

                var callParameters = "initBasicAppRoles=" + RolesConfigCheckBox.GetChecked();
                callParameters += "&initSecuritySchema=" + (RolesInitCheckBox.GetChecked() && !chkCreateRoleBackOffice.GetChecked());
                callParameters += "&initWidgetConfiguration=" + WidgetsInRolesInitCheckBox.GetChecked();
                
				var allroleslist="";
                if (chkCreateRoleBackOffice.GetChecked()) {
                    
                    chkBoxlistRoleBackOfficeInter.GetSelectedItems().forEach(function (element, i) {
                        allroleslist += element.value.trim()+";";
                    });
                }	
				callParameters += "&rolesToInitialize=" + allroleslist.slice(0, -1);
            
                if(fasiURL!=null && fasiURL!=""){
                    $.ajax({
                        url: fasiURL + "/api/initialization/v1/InitializingRoles?"+callParameters,
                        type: 'PUT',
                        headers: {
                            'Authorization': 'Bearer '+InitAccessToken
                        }
                    }).always(function (data) {
                        if (!UsersInitCheckBox.GetChecked())
                            InitializeButton.SetEnabled(true);
                        RolesInitImageWarning.SetVisible(false);
                    }).done(function(response){
                        console.log("InitializingRoles Successfully="+response.Successfully);
                        if(response.Successfully){
                            if(RolesConfigCheckBox.GetChecked()){
                                RolesConfigImageOK.SetVisible(response.Data.BasicAppRolesCompleted);
                                RolesConfigImageFail.SetVisible(!response.Data.BasicAppRolesCompleted);
                            }

                            if(RolesInitCheckBox.GetChecked()){
                                RolesInitLabel.SetVisible(response.Data.BORolesCompleted);
                                RolesInitLabel.SetText('<% Response.Write(GetLocalResourceObject("MessageRoleInitProcessCorrectly"))%>');
                                RolesInitImageOK.SetVisible(response.Data.BORolesCompleted);
                                RolesInitImageFail.SetVisible(!response.Data.BORolesCompleted);
                            }

                            if(WidgetsInRolesInitCheckBox.GetChecked()){
                                WidgetsInRolesInitImageOK.SetVisible(response.Data.WidgetConfigurationCompleted);
                                WidgetsInRolesInitImageFail.SetVisible(!response.Data.WidgetConfigurationCompleted);
                            }
                        }
                        else{
                            RolesConfigImageFail.SetVisible(RolesConfigCheckBox.GetChecked());
                            WidgetsInRolesInitImageFail.SetVisible(WidgetsInRolesInitCheckBox.GetChecked());
                            RolesInitImageFail.SetVisible(RolesInitCheckBox.GetChecked());
                        }
                    }).fail(function(data){
                        console.log("InitializingRoles fail");
                        RolesConfigImageFail.SetVisible(RolesConfigCheckBox.GetChecked());
                        WidgetsInRolesInitImageFail.SetVisible(WidgetsInRolesInitCheckBox.GetChecked());
                        RolesInitImageFail.SetVisible(RolesInitCheckBox.GetChecked());
                    });
                }
            }
            else
                InitializeButton.SetEnabled(true);
        }

        function CleanFASI() {
            HideResultImagesCleaning();
            CleanButton.SetEnabled(false);
            
            if (TaskCheckBox.GetChecked() || WorkflowsCheckBox.GetChecked() ||
                RolesAndUsersCheckBox.GetChecked() || DocumentCacheCheckBox.GetChecked() ||
                NavigationDirectoryCheckBox.GetChecked() || AnonymousUsersCheckBox.GetChecked()) {

                var callParameters = "cleanWorkflows=" + WorkflowsCheckBox.GetChecked();
                callParameters += "&cleanRoleAndUsers=" + RolesAndUsersCheckBox.GetChecked();
                callParameters += "&cleanPendingDoc=" + DocumentCacheCheckBox.GetChecked();
                callParameters += "&cleanNavigationDirectory=" + NavigationDirectoryCheckBox.GetChecked();
                callParameters += "&cleanAnonimousUsers=" + AnonymousUsersCheckBox.GetChecked();
                callParameters += "&cleanTasks=" + TaskCheckBox.GetChecked();

                var rolesAndUsersCallbackMessage = "<% Response.Write(GetLocalResourceObject("RolesAndUsersCallbackPanelMessage"))%>";
                var rolesAndUsersCallbackMessageAdmin = "<% Response.Write(GetLocalResourceObject("RolesAndUsersCallbackPanelMessageAdmin"))%>";

                if(fasiURL!=null && fasiURL!=""){
                    $.ajax({
                        url: 'Initialization.aspx/InitialCleaning?' + callParameters, //fasiURL + "/api/initialization/v1/InitialCleaning?" + callParameters,
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json"
                    }).done(function (response) {
                        response = JSON.parse(response.d);
                        if(response.Successfully){
                            if (RolesAndUsersCheckBox.GetChecked()) {
                                if (response.Data.AdminPass!="")
                                    RolesAndUsersLabel.SetText(rolesAndUsersCallbackMessageAdmin.replace("{pass}", response.Data.AdminPass));
                                else
                                    RolesAndUsersLabel.SetText(rolesAndUsersCallbackMessage);

                                RolesAndUsersImageOK.SetVisible(response.Data.RoleAndUsersCleaned);
                                RolesAndUsersImageFail.SetVisible(!response.Data.RoleAndUsersCleaned);
                                RedirectTimer.SetEnabled(true);
                                CountdownLabel.SetVisible(true);
                                CountdownInit();
                            }
                            
                            if(WorkflowsCheckBox.GetChecked()){
                                WorkflowsImageOK.SetVisible(response.Data.WorkflowsCleaned);
                                WorkflowsImageFail.SetVisible(!response.Data.WorkflowsCleaned);
                            }
                            if(DocumentCacheCheckBox.GetChecked()){
                                DocumentCacheImageOK.SetVisible(response.Data.PendingDocCleaned);
                                DocumentCacheImageFail.SetVisible(!response.Data.PendingDocCleaned);
                            }

                            if(NavigationDirectoryCheckBox.GetChecked()){
                                NavigationDirectoryImageOK.SetVisible(response.Data.NavigationDirectoryCleaned);
                                NavigationDirectoryImageFail.SetVisible(!response.Data.NavigationDirectoryCleaned);
                            }

                            if(AnonymousUsersCheckBox.GetChecked()){
                                AnonymousUsersImageOK.SetVisible(response.Data.AnonimousUsersCleaned);
                                AnonymousUsersImageFail.SetVisible(!response.Data.AnonimousUsersCleaned);
                            }
                            if (TaskCheckBox.GetChecked()) {
                                TaskImageOK.SetVisible(response.Data.TasksCleaned);
                                TaskImageFail.SetVisible(!response.Data.TasksCleaned);
                            }
                        } 
                        else{
                            WorkflowsImageFail.SetVisible(WorkflowsCheckBox.GetChecked());
                            RolesAndUsersImageFail.SetVisible(RolesAndUsersCheckBox.GetChecked());
                            DocumentCacheImageFail.SetVisible(DocumentCacheCheckBox.GetChecked());
                            NavigationDirectoryImageFail.SetVisible(NavigationDirectoryCheckBox.GetChecked());
                            AnonymousUsersImageFail.SetVisible(AnonymousUsersCheckBox.GetChecked());
                            TaskImageFail.SetVisible(TaskCheckBox.GetChecked());
                        }
                    }).fail(function(data){
                        WorkflowsImageFail.SetVisible(WorkflowsCheckBox.GetChecked());
                        RolesAndUsersImageFail.SetVisible(RolesAndUsersCheckBox.GetChecked());
                        DocumentCacheImageFail.SetVisible(DocumentCacheCheckBox.GetChecked());
                        NavigationDirectoryImageFail.SetVisible(NavigationDirectoryCheckBox.GetChecked());
                        AnonymousUsersImageFail.SetVisible(AnonymousUsersCheckBox.GetChecked());
                    }).always(function(){
                        CleanButton.SetEnabled(true);
                    });
                }
            }
            else
                CleanButton.SetEnabled(true);
        }

        function HideResultImagesCleaning(){
            WorkflowsImageFail.SetVisible(false);
            RolesAndUsersImageFail.SetVisible(false);
            DocumentCacheImageFail.SetVisible(false);
            NavigationDirectoryImageFail.SetVisible(false);
            AnonymousUsersImageFail.SetVisible(false);
            WorkflowsImageOK.SetVisible(false);
            RolesAndUsersImageOK.SetVisible(false);
            DocumentCacheImageOK.SetVisible(false);
            NavigationDirectoryImageOK.SetVisible(false);
            AnonymousUsersImageOK.SetVisible(false);
            TaskImageOK.SetVisible(false);
            TaskImageFail.SetVisible(false);
        }

        function HideResultImagesRoles(){
            RolesConfigImageFail.SetVisible(false);
            WidgetsInRolesInitImageFail.SetVisible(false);
            RolesInitImageFail.SetVisible(false);
            RolesConfigImageOK.SetVisible(false);
            RolesInitImageOK.SetVisible(false);
            WidgetsInRolesInitImageOK.SetVisible(false);
        }

        function CreateUsersInSecurity(){
            var syncButton = document.getElementById('<%= btnCreateUsersInSecurity.ClientID %>');
            $(syncButton).attr("disabled", true).css("color", "#cccccc");
            if(fasiURL!=null && fasiURL!=""){
                $.ajax({
                    url: fasiURL + "/api/initialization/v1/InitializationSecuritySync",
                    type: 'PUT',
                    headers: {
                        'Authorization': 'Bearer '+InitAccessToken
                    }
                }).done(function(data){
                    console.log("CreateUsersInSecurity Successfully ="+data.Successfully);
                }).fail(function(data){
                    console.log("CreateUsersInSecurity fail");
                }).always(function(){
                    $(syncButton).attr("disabled", false).css("color", "");
                });
            }
            else
                $(syncButton).attr("disabled", false).css("color", "");
        }

        function CopyUserConfiguration() {
            InitializeButton.SetEnabled(false);
            CopyUserConfigurationImageFail.SetVisible(false);
            CopyUserConfigurationImageOK.SetVisible(false);

            var usersToInitialize = "";
            chkBoxlistTargetUserInter.GetSelectedItems().forEach(function (element, i) {
                usersToInitialize += element.value.trim() + ";";
            });

            if (CopyUserConfigurationCheckBox.GetChecked() && CopyUserConfigurationComboBox.GetSelectedItem() != null && usersToInitialize != "") {
                var callParameters = "baseUserCode=" + CopyUserConfigurationComboBox.GetSelectedItem().value;
                callParameters += "&copyConfigurationOption=" + CopyUserConfigurationrbl.GetSelectedItem().value;
                callParameters += "&targetUsers=" + usersToInitialize.slice(0, -1);


                if (fasiURL != null && fasiURL != "") {
                    $.ajax({
                        url: fasiURL + "/api/initialization/v1/CopyUserConfigurationToAll?" + callParameters,
                        type: 'PUT',
                        headers: {
                            'Authorization': 'Bearer ' + InitAccessToken
                        }
                    }).done(function (data) {
                        console.log("CopyUserConfiguration Successfully=" + data.Successfully);
                        if (data.Successfully)
                            CopyUserConfigurationImageOK.SetVisible(true);
                        else
                            CopyUserConfigurationImageFail.SetVisible(true);
                    }).fail(function (data) {
                        console.log("CopyUserConfiguration fail");
                        CopyUserConfigurationImageFail.SetVisible(true);
                    }).always(function () {
                        InitializeButton.SetEnabled(true);
                    });
                }
            }
            else {
                CopyUserConfigurationImageFail.SetVisible(true);
                InitializeButton.SetEnabled(true);
            }   
        }

        function InitializingSomeBOUsers() {
            InitializeButton.SetEnabled(false);
            OneUsersInitImageOK.SetVisible(false);
            OneUsersInitImageFail.SetVisible(false);
            if (!UsersInitCheckBox.GetChecked() && OneUsersInitCheckBox.GetChecked() && OneUsersInitListBox.GetSelectedItems().length > 0) {
                var callParameters = "inicializeAll=false&notifyByEmail=" + SendCredentialsSomeUsersCheckBox.GetChecked();

                var usersToInitialize="";
                OneUsersInitListBox.GetSelectedItems().forEach(function (element, i) {
                    usersToInitialize += element.text.trim()+";";
                });

                callParameters += "&usersToInitialize=" + usersToInitialize.slice(0, -1);

                if (fasiURL != null && fasiURL != "") {
                    
                    $.ajax({
                        url: fasiURL + "/api/initialization/v1/InitializeBackOfficeUsers?" + callParameters,
                        type: 'PUT',
                        headers: {
                            'Authorization': 'Bearer ' + InitAccessToken
                        }
                    }).always(function () {
                        CopyUserConfigurationCallbackPanel.PerformCallback();
                        InitializeButton.SetEnabled(true);
                        console.log("InitializingSomeBOUsers completed");
                    }).done(function (response) {
                        console.log("InitializingSomeBOUsers Successfully=" + response.Successfully);
                        OneUsersInitImageOK.SetVisible(response.Successfully);
                        OneUsersInitImageFail.SetVisible(!response.Successfully);
                    }).fail(function (data) {
                        console.log("InitializingSomeBOUsers fail");
                        OneUsersInitImageFail.SetVisible(true);
                    });
                }
            }
            else
                InitializeButton.SetEnabled(true);
        }

        function InitializingAllBOUsers() {
            InitializeButton.SetEnabled(false);
            UsersInitImageOK.SetVisible(false);
            UsersInitImageFail.SetVisible(false);
            if (UsersInitCheckBox.GetChecked() && !OneUsersInitCheckBox.GetChecked()) {
                var callParameters = "inicializeAll=true&notifyByEmail=" + SendCredentialsAllUsersCheckBox.GetChecked()+"&usersToInitialize=";

                if (fasiURL != null && fasiURL != "") {
                    $.ajax({
                        url: fasiURL + "/api/initialization/v1/InitializeBackOfficeUsers?" + callParameters,
                        type: 'PUT',
                        headers: {
                            'Authorization': 'Bearer ' + InitAccessToken
                        }
                    }).always(function () {
                        CopyUserConfigurationCallbackPanel.PerformCallback();
                        InitializeButton.SetEnabled(true);
                    }).done(function (response) {
                        UsersInitImageOK.SetVisible(response.Successfully);
                        UsersInitImageFail.SetVisible(!response.Successfully);
                    }).fail(function (data) {
                        UsersInitImageFail.SetVisible(true);
                    });
                }
            }
            else
                InitializeButton.SetEnabled(true);
        }

        var CountdownCounter;
        function CountdownInit() {
            CountdownCounter = 10;
            CountdownUpdate();
        }
        function CountdownTick() {
            CountdownCounter -= 1;
            CountdownUpdate();
        }
        function CountdownUpdate() {
            if (CountdownCounter > 0) {
                CountdownLabel.SetText(CountdownCounter);
            }
            else {
                RedirectCallback.PerformCallback();
                RedirectTimer.SetEnabled(false);
            }
        }

        function chkRolesInitCheckBox_CheckedChanged() {
            if (RolesInitCheckBox.GetChecked()) {
                chkCreateRoleBackOffice.SetEnabled(true);
            } else {
                chkCreateRoleBackOffice.SetEnabled(false);
            }
        }

        function chkCreateRoleBackOffice_CheckedChanged() {
            if (chkCreateRoleBackOffice.GetChecked()) {
                chkBoxlistRoleBackOffice.SetEnabled(true);
            } else {
                chkBoxlistRoleBackOffice.SetEnabled(false);
            }
        }

        function UsersInitCheckBox_CheckedChanged() {
            // Si el check de Inicializacion de TODOS los usuarios está seleccionado
            // no se premite crear un solo usuario
            if (UsersInitCheckBox.GetChecked()) {
                OneUsersInitCheckBox.SetEnabled(false);
                OneUsersInitCheckBox.SetChecked(false);
                OneUsersInitDropDownEdit.SetEnabled(false);
                SendCredentialsAllUsersCheckBox.SetEnabled(true);
            }
            else {
                OneUsersInitCheckBox.SetEnabled(true);
                OneUsersInitDropDownEdit.SetEnabled(true);
                SendCredentialsAllUsersCheckBox.SetEnabled(false);
                SendCredentialsAllUsersCheckBox.SetChecked(false);
            }

            setEnabledCopyUserConfiguration();
        }

        function OneUsersInitCheckBox_CheckedChanged() {
            if (OneUsersInitCheckBox.GetChecked()) {
                UsersInitCheckBox.SetEnabled(false);
                UsersInitCheckBox.SetChecked(false);
                SendCredentialsSomeUsersCheckBox.SetEnabled(true);
            }
            else {
                UsersInitCheckBox.SetEnabled(true);
                SendCredentialsSomeUsersCheckBox.SetEnabled(false);
                SendCredentialsSomeUsersCheckBox.SetChecked(false);
            }
            setEnabledCopyUserConfiguration();
        }

        function RolesConfigCheckBox_CheckedChanged() {
            setEnabledCopyUserConfiguration();
        }

        function RolesInitCheckBox_CheckedChanged() {
            setEnabledCopyUserConfiguration();
        }

        function WidgetsInRolesInitCheckBox_CheckedChanged() {
            setEnabledCopyUserConfiguration();

        }

        function CopyUserConfigurationCheckBox_CheckedChanged() {
            var bEnabled = !CopyUserConfigurationCheckBox.GetChecked();

            RolesConfigCheckBox.SetEnabled(bEnabled);
            RolesInitCheckBox.SetEnabled(bEnabled);
            WidgetsInRolesInitCheckBox.SetEnabled(bEnabled);
            UsersInitCheckBox.SetEnabled(bEnabled);
            OneUsersInitCheckBox.SetEnabled(bEnabled);
            OneUsersInitDropDownEdit.SetEnabled(bEnabled);


            chkBoxlistTargetUser.SetEnabled(!bEnabled);
            CopyUserConfigurationrbl.SetEnabled(!bEnabled);
            CopyUserConfigurationComboBox.SetEnabled(!bEnabled);
        }

        function setEnabledCopyUserConfiguration() {
            if (!RolesConfigCheckBox.GetChecked() && !RolesInitCheckBox.GetChecked() && !UsersInitCheckBox.GetChecked() && !OneUsersInitCheckBox.GetChecked() && !WidgetsInRolesInitCheckBox.GetChecked()) {
                CopyUserConfigurationCheckBox.SetEnabled(true);
                CopyUserConfigurationComboBox.SetEnabled(true);
                chkBoxlistTargetUser.SetEnabled(true);
            }
            else {
                CopyUserConfigurationCheckBox.SetEnabled(false);
                CopyUserConfigurationComboBox.SetEnabled(false);
                CopyUserConfigurationCheckBox.SetChecked(false);
                CopyUserConfigurationrbl.SetEnabled(false);
                chkBoxlistTargetUser.SetEnabled(false);
            }
        }

        function RolesConfigCallbackPanel_EndCallback() {
            if (WidgetsInRolesInitCheckBox.GetChecked()) {
                if (!RolesInitCallbackPanel.InCallback()) {
                    WidgetsInRolesInitCallbackPanel.PerformCallback();
                }
            }
        }

        function RolesInitCallbackPanel_EndCallback() {
            if (WidgetsInRolesInitCheckBox.GetChecked()) {
                if (!RolesConfigCallbackPanel.InCallback()) {
                    WidgetsInRolesInitCallbackPanel.PerformCallback();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id='tableBegin' runat='server' style='width: 100%;'>
                <tr valign='top'>
                    <td></td>
                </tr>
                <tr valign='top'>
                    <td style='width: 100%'>
                        <dxrp:ASPxRoundPanel ID="zonePrincipal" runat="server" Width="100%" Visible="true"
                            meta:resourcekey="ASPxRoundPanel1Resource">
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                    <table style='width: 100%;'>
                                        <tr valign='top'>
                                            <td colspan='2'>
                                                <h5 id='zoneInstructionLabel'><%Response.Write(GetLocalResourceObject("zoneInstructionLabelResource.Text")) %>
                                                </h5>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style="width: 0%">&nbsp;
                                            </td>
                                            <td style="width: 0%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 20%' align='Left'>
                                                <dxe:ASPxCheckBox ID='WorkflowsCheckBox' ClientInstanceName='WorkflowsCheckBox' runat='server'
                                                    ToolTip='' Visible='true' Enabled='True' meta:resourcekey="WorkflowsCheckBoxResource"
                                                    Text="Workflows">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 20%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="WorkflowsCallbackPanel" ClientInstanceName="WorkflowsCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent2" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="WorkflowsImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20" ClientInstanceName="WorkflowsImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="WorkflowsImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20" ClientInstanceName="WorkflowsImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="WorkflowsLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 20%' align='Left'>
                                                <dxe:ASPxCheckBox ID='TaskCheckBox' ClientInstanceName='TaskCheckBox' runat='server'
                                                    ToolTip='' Visible='true' Enabled='True' meta:resourcekey="TaskCheckBoxResource"
                                                    Text="Tareas de Agenda">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 80%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="TaskCallbackPanel" ClientInstanceName="TaskCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent3" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="TaskImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20" ClientInstanceName="TaskImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="TaskImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20" ClientInstanceName="TaskImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="TaskLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 20%' align='Left'>
                                                <dxe:ASPxCheckBox ID='RolesAndUsersCheckBox' ClientInstanceName='RolesAndUsersCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="RolesAndUsersCheckBoxResource"
                                                    Text="Roles y Usuarios del Portal">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 80%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="RolesAndUsersCallbackPanel" ClientInstanceName="RolesAndUsersCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent4" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="RolesAndUsersImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20" ClientInstanceName="RolesAndUsersImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="RolesAndUsersImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20" ClientInstanceName="RolesAndUsersImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="RolesAndUsersLabel" runat="server" Width="100%" ClientInstanceName="RolesAndUsersLabel">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;<dxe:ASPxLabel ID="CountdownLabel" runat="server" Text="" ClientInstanceName="CountdownLabel"
                                                                        ClientVisible="false" Font-Bold="True" ForeColor="#CC3300">
                                                                    </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 20%' align='Left'>
                                                <dxe:ASPxCheckBox ID='DocumentCacheCheckBox' ClientInstanceName='DocumentCacheCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="DocumentCacheCheckBoxResource"
                                                    Text="Document Cache">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 80%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="DocumentCacheCallbackPanel" ClientInstanceName="DocumentCacheCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent9" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="DocumentCacheImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20"  ClientInstanceName="DocumentCacheImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="DocumentCacheImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20"  ClientInstanceName="DocumentCacheImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="DocumentCacheLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 20%' align='Left'>
                                                <dxe:ASPxCheckBox ID='NavigationDirectoryCheckBox' ClientInstanceName='NavigationDirectoryCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="NavigationDirectoryCheckBoxResource"
                                                    Text="Navigation Directory">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 80%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="NavigationDirectoryCallbackPanel" ClientInstanceName="NavigationDirectoryCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent11" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="NavigationDirectoryImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20"  ClientInstanceName="NavigationDirectoryImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="NavigationDirectoryImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20"  ClientInstanceName="NavigationDirectoryImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="NavigationDirectoryLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 20%' align='Left'>
                                                <dxe:ASPxCheckBox ID='AnonymousUsersCheckBox' ClientInstanceName='AnonymousUsersCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="AnonymousUsersCheckBoxResource"
                                                    Text="Anonymous Users">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 80%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="AnonymousUsersCallbackPanel" ClientInstanceName="AnonymousUsersCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent12" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="AnonymousUsersImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20"  ClientInstanceName="AnonymousUsersImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="AnonymousUsersImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20"  ClientInstanceName="AnonymousUsersImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="AnonymousUsersLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style="width: 20%">&nbsp;
                                            </td>
                                            <td style="width: 80%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 100%' colspan='2' align='Center'>
                                                <dxe:ASPxButton ID='CleanButton' runat='server' ToolTip='' Text='Limpiar' Visible='true' ClientInstanceName="CleanButton"
                                                    Enabled='True' meta:resourcekey="CleanButtonResource" AutoPostBack='false'>
                                                    <ClientSideEvents Click="CleanButton_Click" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style="width: 20%">&nbsp;
                                                <dxt:ASPxTimer ID="RedirectTimer" runat="server" Interval="1000" ClientInstanceName="RedirectTimer"
                                                    Enabled="false">
                                                    <ClientSideEvents Tick="function(s, e) { CountdownTick(); }" />
                                                </dxt:ASPxTimer>
                                            </td>
                                            <td style="width: 100%">&nbsp;
                                            </td>
                                        </tr>
                                        </table>
                                    <hr />
                                    <table style='width: 100%;'>
                                        <tr valign='top'>
                                            <td colspan='2'>
                                                <h5 id='GenerateInstructionLabel'><%Response.Write(GetLocalResourceObject("GenerateInstructionLabelResource.Text")) %>
                                                </h5>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style="width: 0%">&nbsp;
                                            </td>
                                            <td style="width: 0%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 30%' align='Left'>
                                                <dxe:ASPxCheckBox ID='RolesConfigCheckBox' ClientInstanceName='RolesConfigCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="RolesConfigCheckBoxResource"
                                                    Text="Crear Roles de Configuración del Portal">
                                                    <ClientSideEvents ValueChanged="RolesConfigCheckBox_CheckedChanged" />
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 70%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="RolesConfigCallbackPanel" ClientInstanceName="RolesConfigCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <ClientSideEvents EndCallback="RolesConfigCallbackPanel_EndCallback" />
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent5" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="RolesConfigImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20"  ClientInstanceName="RolesConfigImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="RolesConfigImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20"  ClientInstanceName="RolesConfigImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="RolesConfigLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 30%' align='Left'>
                                                <dxe:ASPxCheckBox ID='RolesInitCheckBox' ClientInstanceName='RolesInitCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="RolesInitCheckBoxResource"
                                                    Text="Crear Roles de VisualTIME en el Portal">

                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	chkRolesInitCheckBox_CheckedChanged();
}" />
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 70%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="RolesInitCallbackPanel" ClientInstanceName="RolesInitCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <ClientSideEvents EndCallback="RolesInitCallbackPanel_EndCallback" />
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent7" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxCheckBox ID='chkCreateRoleBackOffice' ClientInstanceName='chkCreateRoleBackOffice'
                                                                            runat='server' ToolTip='' Visible='true' ClientEnabled='False' meta:resourcekey="chkCreateRoleBackOffice"
                                                                            Text="Specifically select the role">
                                                                            <ClientSideEvents CheckedChanged="chkCreateRoleBackOffice_CheckedChanged" />
                                                                        </dxe:ASPxCheckBox>
                                                                    </td>
                                                                    <td>&nbsp; &nbsp;</td>
                                                                    <td>
                                                                        <dxe:ASPxDropDownEdit ClientInstanceName="chkBoxlistRoleBackOffice" ID="chkBoxlistRoleBackOffice"
                                                                            Width="150px" runat="server" ClientEnabled="false" EnableAnimation="False" SkinID="CheckComboBox">
                                                                            <DropDownWindowTemplate>
                                                                                <dxe:ASPxListBox ID="chkBoxlistRoleBackOfficeInter" runat="server" SelectionMode="CheckColumn"
                                                                                    Width="100%" TextField="SSCHE_CODE" ValueField="SSCHE_CODE" ValueType="System.String"
                                                                                    SkinID="CheckComboBoxListBox" ClientInstanceName="chkBoxlistRoleBackOfficeInter">
                                                                                </dxe:ASPxListBox>
                                                                            </DropDownWindowTemplate>
                                                                        </dxe:ASPxDropDownEdit>
                                                                    </td>
                                                                    <td>
                                                                         <dxe:ASPxImage ID="RolesInitImageWarning" EnableClientSideAPI="true" ClientEnabled="true" ClientInstanceName="RolesInitImageWarning" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/WarningIcon.png"
                                                                            Height="20" Width="20">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="RolesInitImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20"  ClientInstanceName="RolesInitImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="RolesInitImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20"  ClientInstanceName="RolesInitImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="RolesInitLabel" EnableClientSideAPI="true" ClientEnabled="true" ClientInstanceName="RolesInitLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 30%' align='Left'>
                                                <dxe:ASPxCheckBox ID='WidgetsInRolesInitCheckBox' ClientInstanceName='WidgetsInRolesInitCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="WidgetsInRolesInitCheckBoxResource"
                                                    Text="Crear configuración inicial de los widgets por roles">
                                                    <ClientSideEvents CheckedChanged="WidgetsInRolesInitCheckBox_CheckedChanged" />
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 70%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="WidgetsInRolesInitCallbackPanel" ClientInstanceName="WidgetsInRolesInitCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent10" runat="server">
                                                            <table>
                                                                <tr>

                                                                    <td>
                                                                        <dxe:ASPxImage ID="WidgetsInRolesInitImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20"  ClientInstanceName="WidgetsInRolesInitImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="WidgetsInRolesInitImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20"  ClientInstanceName="WidgetsInRolesInitImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="WidgetsInRolesInitLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 30%' align='Left'>
                                                <dxe:ASPxCheckBox ID='UsersInitCheckBox' ClientInstanceName='UsersInitCheckBox' runat='server'
                                                    ToolTip='' Visible='true' Enabled='True' meta:resourcekey="UsersInitCheckBoxResource"
                                                    Text="Crear Usuarios de VisualTIME en el Portal">
                                                    <ClientSideEvents CheckedChanged="UsersInitCheckBox_CheckedChanged" />
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style='width: 10%' align='Left'>
                                                <dxe:ASPxCheckBox ID='SendCredentialsAllUsersCheckBox' ClientInstanceName='SendCredentialsAllUsersCheckBox'
                                                    runat='server' ToolTip='' Visible='true' ClientEnabled='False' meta:resourcekey="SendCredentialsAllUsersCheckBoxResource"
                                                    Text="Send credentials by e-mail">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 70%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="UsersInitCallbackPanel" ClientInstanceName="UsersInitCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent6" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="UsersInitImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20" ClientInstanceName="UsersInitImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="UsersInitImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20" ClientInstanceName="UsersInitImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="UsersInitLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 30%' align='Left'>
                                                <dxe:ASPxCheckBox ID='OneUsersInitCheckBox' ClientInstanceName='OneUsersInitCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="OneUsersInitCheckBoxResource"
                                                    Text="Crear Usuarios especificos de VisualTIME en el Portal">
                                                    <ClientSideEvents CheckedChanged="OneUsersInitCheckBox_CheckedChanged" />
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style='width: 10%' align='Left'>
                                                <dxe:ASPxCheckBox ID='SendCredentialsSomeUsersCheckBox' ClientInstanceName='SendCredentialsSomeUsersCheckBox'
                                                    runat='server' ToolTip='' Visible='true' ClientEnabled='False' meta:resourcekey="SendCredentialsSomeUsersCheckBoxResource"
                                                    Text="Send credentials by e-mail">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 70%" align='Left'>
                                                <dxcp:ASPxCallbackPanel ID="OneUsersInitCallbackPanel" ClientInstanceName="OneUsersInitCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent8" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxDropDownEdit ClientInstanceName="OneUsersInitDropDownEdit" ID="OneUsersInitDropDownEdit"
                                                                            Width="150px" runat="server" EnableAnimation="False" SkinID="CheckComboBox">
                                                                            <DropDownWindowTemplate>
                                                                                <dxe:ASPxListBox ID="OneUsersInitListBox" runat="server" SelectionMode="CheckColumn"
                                                                                    Width="100%" TextField="SINITIALS" ValueField="NUSERCODE" ValueType="System.Int32"
                                                                                    SkinID="CheckComboBoxListBox" ClientInstanceName="OneUsersInitListBox">
                                                                                    <%--<ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />--%>
                                                                                </dxe:ASPxListBox>
                                                                            </DropDownWindowTemplate>
                                                                        </dxe:ASPxDropDownEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="OneUsersInitImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20" ClientInstanceName="OneUsersInitImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="OneUsersInitImageFail" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/cross.png"
                                                                            Height="20" Width="20" ClientInstanceName="OneUsersInitImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="OneUsersInitLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 20%' align='Left'>
                                                <dxe:ASPxCheckBox ID='CopyUserConfigurationCheckBox' ClientInstanceName='CopyUserConfigurationCheckBox'
                                                    runat='server' ToolTip='' Visible='true' Enabled='True' meta:resourcekey="CopyUserConfigurationCheckBoxResource"
                                                    Text="Copiar configuración de un usuario especifico">
                                                    <ClientSideEvents CheckedChanged="CopyUserConfigurationCheckBox_CheckedChanged" />
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td style="width: 20%" align='Left'>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxRadioButtonList ID="CopyUserConfigurationRadioButtonList" runat="server"
                                                                Enabled="True" ClientInstanceName="CopyUserConfigurationrbl">
                                                                <Border BorderWidth="0px" />
                                                                <Items>
                                                                    <dxe:ListEditItem Value="1" Selected="true" Text="Respetar la configuracion del Rol/Esquema en el usuario destino"
                                                                        meta:resourcekey="CopyUserConfigurationOption1" />
                                                                    <dxe:ListEditItem Value="2" Text="Copiar Widgets al Rol/Esquema asociado al Usuario Destino"
                                                                        meta:resourcekey="CopyUserConfigurationOption2" />
                                                                </Items>
                                                            </dxe:ASPxRadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxcp:ASPxCallbackPanel ID="CopyUserConfigurationCallbackPanel" ClientInstanceName="CopyUserConfigurationCallbackPanel"
                                                    runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent13" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Literal ID="originUsersLabel" runat="server" Text="Usuario de origen" meta:resourcekey="originUsersLabelResource"></asp:Literal>
                                                                    </td>
                                                                    <td Width="30"></td>
                                                                    <td>
                                                                        <asp:Literal ID="targetUsersLabel" runat="server" Text="Usuarios de destino" meta:resourcekey="targetUsersLabelResource"></asp:Literal>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="CopyUserConfigurationComboBox" runat="server" ClientInstanceName="CopyUserConfigurationComboBox"
                                                                            ValueType="System.String" TextField="UserName" ValueField="ProviderUserKey" AutoPostBack="false"
                                                                            Width="150px" SkinID="CheckComboBox">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxDropDownEdit ClientInstanceName="chkBoxlistTargetUser" ID="chkBoxlistTargetUser"
                                                                            Width="150px" runat="server" ClientEnabled="true" EnableAnimation="False" SkinID="CheckComboBox">
                                                                            <DropDownWindowTemplate>
                                                                                <dxe:ASPxListBox ID="chkBoxlistTargetUserInter" runat="server" SelectionMode="CheckColumn"
                                                                                    Width="100%" TextField="UserName" ValueField="ProviderUserKey" ValueType="System.String"
                                                                                    SkinID="CheckComboBoxListBox" ClientInstanceName="chkBoxlistTargetUserInter">
                                                                                </dxe:ASPxListBox>
                                                                            </DropDownWindowTemplate>
                                                                        </dxe:ASPxDropDownEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="CopyUserConfigurationImageOK" runat="server" ClientVisible="false" ImageUrl="~/images/dropthings/tick.png"
                                                                            Height="20" Width="20"  ClientInstanceName="CopyUserConfigurationImageOK">
                                                                        </dxe:ASPxImage>
                                                                        <dxe:ASPxImage ID="CopyUserConfigurationImageFail" runat="server" ClientVisible="false"
                                                                            ImageUrl="~/images/dropthings/cross.png" Height="20" Width="20"  ClientInstanceName="CopyUserConfigurationImageFail">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="CopyUserConfigurationLabel" runat="server" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style="width: 20%">&nbsp;
                                            </td>
                                            <td style="width: 80%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td style='width: 100%' colspan='2' align='Center'>
                                                <dxe:ASPxButton ID='InitializeButton' runat='server' ToolTip='' Text='Inicializar' ClientInstanceName="InitializeButton"
                                                    Visible='true' Enabled='True' meta:resourcekey="InitializeButtonResource" AutoPostBack='false'>
                                                    <ClientSideEvents Click="InitializeButton_Click" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <table style='width: 100%;'>
                                        <tr>
                                            <td>
                                                <h5><%Response.Write(GetLocalResourceObject("SecuritySection.Header")) %></h5>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="col-md-6">
                                                    <div class="alert alert-danger fade in" role="alert"> 
                                                        <p><%Response.Write(GetLocalResourceObject("SecuritySection.Alert")) %></p>
                                                    </div>
                                                 </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-center">
                                                <asp:Button ID="btnCreateUsersInSecurity" runat="server" Text="" meta:resourcekey="SecuritySection" OnClientClick="CreateUsersInSecurity(); return false;"/>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
            </table>
            <br />
            <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Modal="true">
                <HeaderTemplate>
                    <div>
                        <asp:Literal ID="popupMessageHeader" runat="server" Text="Confirmación de Borrado"
                            meta:resourcekey="popupMessageHeaderResource"></asp:Literal>
                    </div>
                </HeaderTemplate>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <div style="width: 350px">
                            <table>
                                <tr>
                                    <td rowspan="2">
                                        <dxe:ASPxImage ID="ASPxImage" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/Question.png">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                        <dxe:ASPxLabel ID="ASPxLabel" runat="server" meta:resourcekey="ASPxLabelResource"
                                            Text="">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table>
                                <tr>
                                    <td></td>
                                    <td style="width: 100%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="btnYes" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnYes"
                                            EnableDefaultAppearance="False" EnableTheming="False">
                                            <Image Url="~/images/generaluse/ConfirmDelete/btnacceptoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnaccepton.gif"
                                                UrlPressed="~/images/generaluse/ConfirmDelete/btnaccepton.gif" />
                                            <ClientSideEvents Click="btnYes_Click" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btnNo" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnNo"
                                            EnableDefaultAppearance="False" EnableTheming="False">
                                            <Image Url="~/images/generaluse/ConfirmDelete/btncanceloff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btncancelon.gif"
                                                UrlPressed="~/images/generaluse/ConfirmDelete/btncancelon.gif" />
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
                ID="popupMessage" runat="server" ClientInstanceName="popupMessage" Modal="true">
                <HeaderTemplate>
                    <div>
                        <asp:Literal ID="popupMessageTextHeader" runat="server" Text="Mensaje" meta:resourcekey="popupMessageTextHeaderResource"></asp:Literal>
                    </div>
                </HeaderTemplate>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <div style="width: 350px">
                            <table>
                                <tr>
                                    <td rowspan="2">
                                        <dxe:ASPxImage ID="ASPxImageM" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/exclamation.png"
                                            Width="32px">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="MessageLabel" runat="server" meta:resourcekey="MessageLabelResource"
                                            Text="">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table>
                                <tr>
                                    <td></td>
                                    <td style="width: 100%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="CloseButton" runat="server" Width="50px" AutoPostBack="False"
                                            ClientInstanceName="CloseButton" EnableDefaultAppearance="False" EnableTheming="False">
                                            <Image Url="~/images/generaluse/ConfirmDelete/btnacceptoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnaccepton.gif"
                                                UrlPressed="~/images/generaluse/ConfirmDelete/btnaccepton.gif" />
                                            <ClientSideEvents Click="CloseButton_Click" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <dxcb:ASPxCallback ID="RedirectCallback" runat="server" ClientInstanceName="RedirectCallback">
                            </dxcb:ASPxCallback>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>