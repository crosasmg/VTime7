<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="UsersManager.aspx.vb" Inherits="dropthings_Admin_UsersManager" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <% If (DesignMode) Then%>
    <script src="../../Script/ASPxScriptIntelliSense.js" type="text/javascript"></script>
    <% End If%>
    <script type="text/javascript">


        function Role_SelectedIndexChanged(s, e) {
            var isCheck = e.isSelected;
            var valueName =s.items[e.index].value;
            var data = JSON.stringify({ roleName: valueName, 'isCheck': isCheck })
                var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/Admin/UsersManager.aspx/IsRoleBackOffice';
                $.ajax({
                    url: urlBase,
                    data: data,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        if (data.d.IsRoleBackOffice ==  true) {
                            if (isCheck == false) {
                                chblRole.SelectIndices([e.index])
                                alert(data.d.Message);
                            }
                        }
                        RefreshRoleAssigned();
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });

        }


        function RefreshRoleAssigned()
        {
            var valueText = '';
            var items = [];
            if (chblRole.GetSelectedItems().length != 0) {
                for (var i = 0; i < chblRole.GetSelectedItems().length; i++) {
                    items[i] = chblRole.GetSelectedItems()[i].value;
                }
                valueText = items.join(',');
            }
            var roleEditor = GridViewUsers.GetEditor("ROLESASSIGNED");
            roleEditor.SetValue(valueText);
        }


        // BEGIN CONFIRMDELETE
        function DeleteButton_Click(s, e) {
            if (GridViewUsers.GetSelectedRowCount() != 0) {
                popupDelete.Show();
                btnYes.Focus();
            }
        }

        function txtSearchButton_Click(s, e) {
            if (ASPxClientUtils.GetKeyCode(e.htmlEvent) === ASPxKey.Enter) {
                btnSeach.DoClick();
            }
            //            return ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
        }

        function btnYes_Click(s, e) {
            popupDelete.Hide();
            GridViewUsers.PerformCallback('delete');
            GridViewUsers.UnselectRows();
        }
        function btnNo_Click(s, e) {
            popupDelete.Hide();
        }
        // END CONFIRMDELETE

        //
        var keyValue;

        function popup_Shown(s, e) {
            callbackPanel.PerformCallback(keyValue);
        }

        function OnMoreInfoClick(element, key) {
            //callbackPanel.SetContentHtml("");
            popup.ShowAtElement(element);
            keyValue = key;
        }

        // <![CDATA[
        var textSeparator = ";";
        function OnListBoxSelectionChanged(listBox, args) {
            UpdateText();
        }
        function UpdateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(GetSelectedItemsText(selectedItems));
        }
        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateText();  // for remove non-existing texts
        }
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                //if(items[i].index != 0)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts) {
            var actualValues = [];
            var value = "";
            for (var i = 0; i < texts.length; i++) {
                value = GetValueByText(texts[i]);
                if (value != null)
                    actualValues.push(value);
            }
            return actualValues;
        }
        function GetValueByText(text) {
            for (var i = 0; i < checkListBox.GetItemCount() ; i++)
                if (checkListBox.GetItem(i).text.toUpperCase() == text.toUpperCase())
                    return checkListBox.GetItem(i).value;
            return null;
        }
        // ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table1" width="100%" runat='server' style="width: 100%;">
                <tr id="Tr1" runat="server">
                    <td id="Td1" style='width: 100%' runat="server">
                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="" Width="100%"
                            EnableDefaultAppearance="False">
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                    <table style='width: 100%;'>
                                        <tr valign="middle" align="center">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="lblSearch" runat="server" meta:resourcekey="lblSearchResource" Text="Buscar" />
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtSearch" Width="250px" runat="server"
                                                                AutoPostBack="false">
                                                                <ClientSideEvents KeyDown="txtSearchButton_Click" />
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btnSeach" ClientInstanceName="btnSeach" EnableDefaultAppearance="False"
                                                                runat="server" AutoPostBack="true" Width="35px" Height="30px" meta:resourcekey="btnSeachResource"
                                                                ToolTip="Eliminar información" EnableTheming="False">
                                                                <Image Url="~/images/generaluse/search.gif" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr valign='top'>
                                            <td>
                                                <dxe:ASPxButton ID="btnDelete" ClientInstanceName="btnDelete" EnableDefaultAppearance="False"
                                                    runat="server" AutoPostBack="False" Width="35px" Height="30px" meta:resourcekey="btnDeleteResource"
                                                    ToolTip="Eliminar información" EnableTheming="False">
                                                    <ClientSideEvents Click="DeleteButton_Click" />
                                                    <Image Url="~/images/generaluse/ConfirmDelete/btnDeleteoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnDeleteon.gif"
                                                        UrlPressed="~/images/generaluse/ConfirmDelete/btnDeleteon.gif" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxwgv:ASPxGridView ID="GridViewUsers" runat="server" AutoGenerateColumns="False"
                                                    KeyFieldName="USERID" ClientInstanceName='GridViewUsers' Width="100%" EnableRowsCache="False"
                                                    meta:resourcekey="GridViewUsersResource">
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsEditing Mode="EditFormAndDisplayRow" PopupEditFormWidth="800px" PopupEditFormModal="true"
                                                        PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormVerticalAlign="WindowCenter"
                                                        PopupEditFormShowHeader="True" />
                                                    <Settings ShowPreview="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="50px" ShowSelectCheckbox="True">
                                                            <EditButton Visible="True">
                                                                <Image Url="~/images/generaluse/edit.gif" />
                                                            </EditButton>
                                                            <CancelButton>
                                                                <Image Url="~/images/generaluse/btncanceloff.gif" />
                                                            </CancelButton>
                                                            <UpdateButton>
                                                                <Image Url="~/images/generaluse/btnacceptoff.gif" />
                                                            </UpdateButton>
                                                            <DeleteButton Visible="false">
                                                                <Image Url="~/images/generaluse/del.gif" />
                                                            </DeleteButton>
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="USERNAME" VisibleIndex="1" Visible="True"
                                                            meta:resourcekey="UserNameColumnResource">
                                                            <EditFormSettings Visible="True" VisibleIndex="1" />
                                                            <PropertiesTextEdit Size="30" MaxLength="256" Width="150px">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="EMAIL" VisibleIndex="2" Visible="True" meta:resourcekey="EmailColumnResource">
                                                            <EditFormSettings Visible="True" VisibleIndex="1" />
                                                            <PropertiesTextEdit Size="30" MaxLength="256" Width="250px">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="CREATIONDATE" VisibleIndex="3" Visible="True"
                                                            meta:resourcekey="CreationDateColumnResource">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm tt" />
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="LASTLOGINDATE" VisibleIndex="4" Visible="True"
                                                            meta:resourcekey="LastLoginDateColumnResource">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm tt" />
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="LASTACTIVITYDATE" VisibleIndex="5" Visible="False"
                                                            meta:resourcekey="LastActivityDateColumnResource">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm tt" />
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="ISLOCKEDOUT" PropertiesCheckEdit-EnableClientSideAPI="True"
                                                            VisibleIndex="6" Visible="False" ReadOnly="False" meta:resourcekey="IsLockedOutColumnResource"
                                                            EditFormCaptionStyle-HorizontalAlign="Left" FooterCellStyle-ForeColor="Red" EditCellStyle-HorizontalAlign="Left"
                                                            CellStyle-HorizontalAlign="Left">
                                                            <EditCellStyle HorizontalAlign="Left">
                                                            </EditCellStyle>
                                                            <EditFormCaptionStyle HorizontalAlign="Left">
                                                            </EditFormCaptionStyle>
                                                            <DataItemTemplate>
                                                                <dxe:ASPxCheckBox ID="chkIsLockedOut" runat="server" Value='<%# Bind("ISLOCKEDOUT") %>' />
                                                            </DataItemTemplate>
                                                            <PropertiesCheckEdit EnableClientSideAPI="True">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings Visible="True" VisibleIndex="5" />
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                            <FooterCellStyle ForeColor="Red">
                                                            </FooterCellStyle>
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="LASTLOCKEDOUTDATE" VisibleIndex="7" Visible="True"
                                                            meta:resourcekey="LastLockoutDateColumnResource">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm tt" />
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="PWDQUESTION" VisibleIndex="8" Visible="False"
                                                            meta:resourcekey="PasswordQuestionColumnResource" EditFormCaptionStyle-ForeColor="#CC3300">
                                                            <EditFormSettings Visible="False" />
                                                            <EditFormCaptionStyle ForeColor="#CC3300">
                                                            </EditFormCaptionStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="ISAPPROVED" VisibleIndex="9" Visible="False"
                                                            meta:resourcekey="IsApprovedColumnResource">
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="ISINACTIVE" VisibleIndex="10" Visible="false"
                                                            meta:resourcekey="IsInactiveColumnResource">
                                                            <EditFormSettings Visible="True" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="ISADMINISTRATOR" VisibleIndex="11" Visible="false"
                                                            meta:resourcekey="IsAdministratorColumnResource">
                                                            <EditFormSettings Visible="True" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="PASSWORDNEVEREXPIRES" VisibleIndex="12" Visible="false"
                                                            meta:resourcekey="PasswordNeverExpiresColumnResource" PropertiesCheckEdit-EnableClientSideAPI="True">
                                                            <PropertiesCheckEdit EnableClientSideAPI="True">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings Visible="True" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="ALLOWSCHEDULER" VisibleIndex="13" Visible="false"
                                                            meta:resourcekey="AllowSchedulerColumnResource" PropertiesCheckEdit-EnableClientSideAPI="True">
                                                            <PropertiesCheckEdit EnableClientSideAPI="True">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings Visible="True" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataTextColumn Name="SECURITYLEVEL" FieldName="SecurityLevel" VisibleIndex="13"
                                                            Visible="False" meta:resourcekey="SecurityLevel" Caption='Nivel Seguridad' ToolTip='Nivel de Seguridad'
                                                            CellStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="True">
                                                            <EditFormSettings Visible="True" VisibleIndex="8" />
                                                            <EditItemTemplate>
                                                                <dx:ASPxTrackBar ID="tkbrSecurityLevel" runat="server" Width="100%" MinValue="1"
                                                                    MaxValue="9" Step="1" SmallTickFrequency="1" LargeTickStartValue="1" LargeTickEndValue="9"
                                                                    LargeTickInterval="1" ValueToolTipPosition="LeftOrTop" ScalePosition="LeftOrTop"
                                                                    Value='<%# Bind("SECURITYLEVEL") %>'>
                                                                </dx:ASPxTrackBar>
                                                            </EditItemTemplate>
                                                            <HeaderStyle VerticalAlign="Bottom" Wrap="True" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="LASTPWDCHANGEDDATE" VisibleIndex="14" Visible="False"
                                                            meta:resourcekey="LastPasswordChangedDateColumnResource">
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="ISONLINE" VisibleIndex="15" Visible="False"
                                                            ReadOnly="True" meta:resourcekey="IsOnlineColumnResource">
                                                            <EditFormSettings Visible="True" VisibleIndex="6" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="ISEMPLOYEE" VisibleIndex="16" Visible="False"
                                                            ReadOnly="True" meta:resourcekey="IsEmployeeColumnResource">
                                                            <EditFormSettings Visible="True" VisibleIndex="7" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Details" VisibleIndex="17" Width="15%" meta:resourcekey="DetailsResource">
                                                            <DataItemTemplate>
                                                                <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.VisibleIndex %>')">
                                                                    <dxe:ASPxLabel ID="InfoLink" runat="server" Text="" meta:resourcekey="InfoLinkResource">
                                                                    </dxe:ASPxLabel>
                                                                </a>
                                                            </DataItemTemplate>
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Código del Cliente" meta:resourcekey="ClientIDColumnResource"
                                                            Visible="False" FieldName="CLIENTID" ShowInCustomizationForm="True" VisibleIndex="18">
                                                            <PropertiesComboBox EnableCallbackMode="true" CallbackPageSize="20" IncrementalFilteringMode="Contains"
                                                                ValueType="System.String" ValueField="SCLIENT" OnItemsRequestedByFilterCondition="ClientID_OnItemsRequestedByFilterCondition"
                                                                OnItemRequestedByValue="ClientID_OnItemRequestedByValue" TextFormatString="{0} {1}"
                                                                DropDownWidth="510px" Width="400px" DropDownStyle="DropDown" DropDownRows="20"
                                                                IncrementalFilteringDelay="500" FilterMinLength="0" ClientInstanceName="ClientId"
                                                                EnableClientSideAPI="True">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn Caption="Código" FieldName="SCLIENT" Width="45" />
                                                                    <dxe:ListBoxColumn Caption="Nombre" FieldName="SCLIENAME" Width="120" />
                                                                    <dxe:ListBoxColumn Caption="Nacimiento" FieldName="SBIRTHDAT" Width="30" />
                                                                </Columns>
                                                                <ClientSideEvents EndCallback="function(s, e) {
                                                                if(s.GetItemCount()==1) {
                                                                	s.SetSelectedIndex(0);}}" />
                                                            </PropertiesComboBox>
                                                            <EditFormSettings Visible="True" VisibleIndex="7" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Código del Productor" meta:resourcekey="ProducerIDColumnResource"
                                                            Visible="False" FieldName="PRODUCERID" ShowInCustomizationForm="True" VisibleIndex="19">
                                                            <PropertiesComboBox EnableCallbackMode="true" CallbackPageSize="20" IncrementalFilteringMode="Contains"
                                                                ValueType="System.String" ValueField="PRODUCERID" OnItemsRequestedByFilterCondition="ProducerID_OnItemsRequestedByFilterCondition"
                                                                OnItemRequestedByValue="ProducerID_OnItemRequestedByValue" TextFormatString="{0} {1}"
                                                                Width="400px" DropDownStyle="DropDown" DropDownRows="20" IncrementalFilteringDelay="500"
                                                                FilterMinLength="0">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn Caption="Code" FieldName="PRODUCERID" Width="80" />
                                                                    <dxe:ListBoxColumn Caption="Name" FieldName="SCLIENAME" Width="140" />
                                                                </Columns>
                                                                <ClientSideEvents Validation="function(s, e) {
                    if (s.GetText() != '' && s.GetText() != '0' &&s.GetSelectedItem() == null) {
                        e.isValid = false;
                        e.errorText = 'The ' + e.value + ' is not valid productor.';
                        e.value = '';
                    }
}" />
                                                            </PropertiesComboBox>
                                                            <EditFormSettings Visible="True" VisibleIndex="8" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataDropDownEditColumn Caption="Roles Assigned" meta:resourcekey="RolesAssignedColumnResource"
                                                            Visible="False" UnboundType="String" FieldName="ROLESASSIGNED" PropertiesDropDownEdit-EnableClientSideAPI="true" ShowInCustomizationForm="True"
                                                            VisibleIndex="20">
                                                            <EditFormSettings Visible="True" VisibleIndex="4" />
                                                            <PropertiesDropDownEdit Height="100%" ShowShadow="false"   ClientInstanceName="RoleId" EnableClientSideAPI="True" Width="400px">
                                                                <DropDownWindowTemplate>
                                                                    <div style="overflow:scroll;  height: 200px">
                                                                        <dx:ASPxCheckBoxList ID="DropDownEditColumnListRole" ClientIDMode="Static" ClientInstanceName="chblRole"
                                                                            runat="server"
                                                                            Width="400px"
                                                                            Height="200px"
                                                                            SelectionMode="Multiple"
                                                                            EnableClientSideAPI="true"
                                                                            TextField="Code"
                                                                            ValueField="Code"
                                                                            ValueType="System.String">
                                                                            <ClientSideEvents SelectedIndexChanged="Role_SelectedIndexChanged"   />
                                                                        </dx:ASPxCheckBoxList>
                                                                    </div>
                                                                </DropDownWindowTemplate>
                                                            </PropertiesDropDownEdit>

                                                        </dxwgv:GridViewDataDropDownEditColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxPager ID="userPager" runat="server" Width="100%" ItemsPerPage="20" ItemCount="5"
                                                    meta:resourcekey="userPagerResource">
                                                    <LastPageButton Visible="True">
                                                    </LastPageButton>
                                                    <FirstPageButton Visible="True">
                                                    </FirstPageButton>
                                                    <Summary Position="Inside" Text="Page {0} of {1} " />
                                                    <PageSizeItemSettings Visible="True">
                                                    </PageSizeItemSettings>
                                                </dx:ASPxPager>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
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
                                                    <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/Question.png">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" meta:resourcekey="ASPxLabel1Resource"
                                                        Text="¿Está seguro de querer eliminar las filas seleccionadas?">
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
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton ID="btnNo" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnNo"
                                                        EnableDefaultAppearance="False" EnableTheming="False">
                                                        <Image Url="~/images/generaluse/ConfirmDelete/btncanceloff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btncancelon.gif"
                                                            UrlPressed="~/images/generaluse/ConfirmDelete/btncancelon.gif" />
                                                        <ClientSideEvents Click="btnNo_Click" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                            <br />
                                        </table>
                                    </div>
                                </dxpc:PopupControlContentControl>
                            </ContentCollection>
                        </dxpc:ASPxPopupControl>
                        <dxpc:ASPxPopupControl ID="popup" ClientInstanceName="popup" runat="server" AllowDragging="True"
                            PopupHorizontalAlign="OutsideRight" HeaderText="Details" meta:resourcekey="popupResource">
                            <ContentCollection>
                                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                    <dxcp:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server"
                                        Width="320px" Height="100px" OnCallback="callbackPanel_Callback" RenderMode="Table">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent2" runat="server">
                                                <table border="1" width="100%">
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Literal ID="Literal6" runat="server" Text="STATUS" meta:resourcekey="Literal6Resource"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="Literal2" runat="server" Text="Is LockedOut" meta:resourcekey="Literal2Resource"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxCheckBox runat="server" ID="CheckIsLockedOut" ReadOnly="true">
                                                            </dxe:ASPxCheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="Literal3" runat="server" Text="Is Online" meta:resourcekey="Literal3Resource"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxCheckBox runat="server" ID="CheckIsOnline" ReadOnly="true">
                                                            </dxe:ASPxCheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Literal ID="Literal7" runat="server" Text="AUDIT" meta:resourcekey="Literal7Resource"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="Literal4" runat="server" Text="Creation Date" meta:resourcekey="Literal4Resource"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="TextCreationDate" runat="server" Text=""></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="Literal1" runat="server" Text="Last Login Date" meta:resourcekey="Literal1Resource"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="TextLastLoginDate" runat="server" Text=""></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="Literal5" runat="server" Text="Last Activity Date" meta:resourcekey="Literal5Resource"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="TextLastActivityDate" runat="server" Text=""></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="Literal8" runat="server" Text="Last Lockout Date" meta:resourcekey="Literal8Resource"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="TextLastLockoutDate" runat="server" Text=""></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="Literal10" runat="server" Text="Last Password Changed" meta:resourcekey="Literal10Resource"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="TextLastPasswordChanged" runat="server" Text=""></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="Literal12" runat="server" Text="Roles Assigned" meta:resourcekey="Literal12Resource"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="TextRolesAssigned" runat="server" Text=""></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Literal ID="PasswordLiteral" runat="server" Text="RECOVER PASSWORD" meta:resourcekey="PasswordLiteralResource"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <dxe:ASPxTextBox ID="EmailTextBox" runat="server" ClientInstanceName="EmailTextBox"
                                                                Width="100%" meta:resourcekey="EmailTextBoxResource" ToolTip="In this email will be sent notice of the change / recovery password, if is empty will be sent to current e-mail" />
                                                            <dxe:ASPxTextBox ID="UserNameTextBox" runat="server" ClientInstanceName="UserNameTextBox"
                                                                ClientVisible="false" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxCheckBox ID="RestorePasswordCheckBox" ClientInstanceName="RestorePasswordCheckBox"
                                                                Text="Send new password" ToolTip="If is true will generate a new password, but will recover the current password."
                                                                runat="server" meta:resourcekey="RestorePasswordCheckBoxResource" />
                                                        </td>
                                                        <td align="right">
                                                            <dxe:ASPxButton ID="GetRecoverPasswordButton" ClientInstanceName="GetRecoverPasswordButton"
                                                                runat="server" Text="Recover" meta:resourcekey="GetRecoverPasswordButtonResource" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>
                                </dxpc:PopupControlContentControl>
                            </ContentCollection>
                            <ClientSideEvents Shown="popup_Shown" />
                        </dxpc:ASPxPopupControl>
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>