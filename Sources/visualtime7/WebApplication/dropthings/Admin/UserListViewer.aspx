<%@ Page Title="Listado de Usuarios" Language="VB" MasterPageFile="~/DropthingsMasterPage.master"
    AutoEventWireup="false" CodeFile="UserListViewer.aspx.vb" Inherits="dropthings_Admin_UserListViewer" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.1.Web, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table width="100%" border="0" cellspacing="10">
        <tbody>
            <tr>
                <td>
                    <dx:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False" Width="100%"
                        ReportViewerID="ReportViewer1">
                        <Items>
                            <dx:ReportToolbarButton ItemKind="Search" meta:resourcekey="ReportToolbarButtonResource1" />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton ItemKind="PrintReport" meta:resourcekey="ReportToolbarButtonResource2" />
                            <dx:ReportToolbarButton ItemKind="PrintPage" meta:resourcekey="ReportToolbarButtonResource3" />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" meta:resourcekey="ReportToolbarButtonResource4" />
                            <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" meta:resourcekey="ReportToolbarButtonResource5" />
                            <dx:ReportToolbarLabel ItemKind="PageLabel" meta:resourcekey="ReportToolbarLabelResource1" />
                            <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px" meta:resourcekey="ReportToolbarComboBoxResource1">
                            </dx:ReportToolbarComboBox>
                            <dx:ReportToolbarLabel ItemKind="OfLabel" meta:resourcekey="ReportToolbarLabelResource2" />
                            <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" meta:resourcekey="ReportToolbarTextBoxResource1" />
                            <dx:ReportToolbarButton ItemKind="NextPage" meta:resourcekey="ReportToolbarButtonResource6" />
                            <dx:ReportToolbarButton ItemKind="LastPage" meta:resourcekey="ReportToolbarButtonResource7" />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton ItemKind="SaveToDisk" meta:resourcekey="ReportToolbarButtonResource8" />
                            <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px"  meta:resourcekey="ReportToolbarComboBoxResource2">
                                <Elements>
                                    <dx:ListElement Value="pdf" />
                                    <dx:ListElement Value="xls" />
                                    <dx:ListElement Value="xlsx" />
                                    <dx:ListElement Value="rtf" />
                                    <dx:ListElement Value="mht" />
                                    <dx:ListElement Value="html" />
                                    <dx:ListElement Value="txt" />
                                    <dx:ListElement Value="csv" />
                                    <dx:ListElement Value="png" />
                                </Elements>
                            </dx:ReportToolbarComboBox>
                        </Items>
                        <Styles>
                            <LabelStyle>
                                <Margins MarginLeft='3px' MarginRight='3px' />
                            </LabelStyle>
                        </Styles>
                    </dx:ReportToolbar>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="margin: 0 auto;width: 1000px;">
                        <dx:ReportViewer ID="ReportViewer1" runat="server" AutoSize="True" Report="<%# new InMotionGIT.FrontOffice.Reports.UserList() %>"
                            ReportName="InMotionGIT.FrontOffice.Reports.UserList" Width="100%" >
                            </dx:ReportViewer>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>