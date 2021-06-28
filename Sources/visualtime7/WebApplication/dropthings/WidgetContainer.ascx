<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WidgetContainer.ascx.cs" Inherits="WidgetContainer" %>
<%@ Register Assembly="CustomDragDrop" Namespace="CustomDragDrop" TagPrefix="cdd" %>
<asp:Panel ID="WidgetPanel" CssClass="widget" runat="server" onmouseover="this.className='widget widget_hover'" onmouseout="this.className='widget'">        
    <asp:Panel id="WidgetHeaderPanel" CssClass="widget_header" runat="server">
        <asp:UpdatePanel ID="WidgetHeaderUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>        
            <table class="widget_header_table" cellspacing="0" cellpadding="0">
            <tbody>
            <tr>
            <td class="widget_title"><asp:Label ID="WidgetTitleLabel" runat="Server" Text="Widget Title" Visible="false" ></asp:Label> <asp:LinkButton ID="WidgetTitleButton" runat="Server" Text="Widget Title" OnClick="WidgetTitleButton_Click" /><asp:TextBox ID="WidgetTitleTextBox" runat="Server" Visible="False" /><asp:Button ID="SaveWidgetTitleButton" runat="Server" OnClick="SaveWidgetTitleButton_Click" Visible="False" Text="OK" /></td>
            <td class="widget_edit"><asp:LinkButton ID="EditWidgetButton" runat="Server" Text="<%$ Resources:Resource, Edit %>" OnClick="EditWidgetButton_Click" /><asp:LinkButton ID="CancelEditWidgetButton" runat="Server" Text="<%$ Resources:Resource, Close %>" OnClick="EditWidgetButton_Click" Visible="false" /></td>
            <td class="widget_button"><asp:LinkButton ID="CollapseWidgetButton" runat="Server" Text="" OnClick="CollapseWidgetButton_Click" CssClass="widget_min widget_box" /><asp:LinkButton ID="ExpandWidgetButton" runat="Server" Text="" CssClass="widget_max widget_box" OnClick="ExpandWidgetButton_Click" /></td>
            <td class="widget_button"><asp:LinkButton ID="CloseWidgetButton" runat="Server" Text="" CssClass="widget_close widget_box" OnClick="CloseWidgetButton_Click" /></td>
            </tr>
            </tbody>
            </table>            
        </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="WidgetHeaderUpdatePanel" >
    <ProgressTemplate><center><asp:Label ID="WorkingLabel" runat="Server" Text="<%$ Resources:Resource, Working %>"/></center></ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="WidgetBodyUpdatePanel" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
        <asp:Panel ID="WidgetBodyPanel" runat="Server" CssClass="widget_body"></asp:Panel>
        </ContentTemplate>        
    </asp:UpdatePanel>    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="WidgetBodyUpdatePanel" >
    <ProgressTemplate><center><asp:Label ID="WorkingLabelA" runat="Server" Text="<%$ Resources:Resource, Working %>"/></center></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Panel>
<% /*<cdd:CustomFloatingBehaviorExtender ID="WidgetFloatingBehavior" DragHandleID="WidgetHeader" TargetControlID="Widget" runat="server" />*/ %>