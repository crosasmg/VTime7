<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavigationDirectory.ascx.cs" Inherits="Dropthings.Widgets.NavigationDirectory" %>

<asp:Panel ID="pnlEdit" runat="server" Width="336px" Visible="False"
    meta:resourcekey="pnlEditResource1">
    <asp:HiddenField ID="hdnLang" runat="server" />
    <table style="width: 208px">
        <tr>
            <td>
                Category: </td>
            <td class="style1">
                <asp:DropDownList ID="ddCategories" runat="server" AutoPostBack="True"
                    DataTextField="DESCRIPTION" DataValueField="CODE" meta:resourcekey="ddCategoriesResource1" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style1">
                <asp:CheckBox ID="chkDetails" runat="server" Text="Show Descriptions"
                    meta:resourcekey="chkDetailsResource1" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
    </table>
</asp:Panel>





<asp:GridView ID="ListGridView" runat="server" AutoGenerateColumns="False"
    onrowdatabound="ListGridView_RowDataBound" SkinID="Main" Height="52px"
    Width="204px" onpageindexchanging="ListGridView_PageIndexChanging"
    meta:resourcekey="ListGridViewResource1">
    <Columns>
            <asp:TemplateField  ItemStyle-VerticalAlign="Top"
                ItemStyle-HorizontalAlign="Center" meta:resourcekey="TemplateFieldResource1">
                <ItemTemplate>
                    <asp:Image ID="Image1"  runat="server" ImageUrl='<%# Eval("ImageFile") %>'
                        meta:resourcekey="Image1Resource1"   >
                    </asp:Image>
            </ItemTemplate>

<ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Title" meta:resourcekey="TemplateFieldResource2">
                <ItemTemplate>
                    &nbsp;<a href='<%# Eval("URLPath") %>'><%# Eval("Title")%></a><br />&nbsp;<asp:Label ID="lblDetails" runat="server" Text='<%# Eval("Description") %>'
                        Font-Bold="False" Font-Italic="True" Font-Overline="False"
                        Font-Size="X-Small" ForeColor=GrayText
                        Font-Strikeout="False" meta:resourcekey="lblDetailsResource1" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>



    </Columns>
</asp:GridView>

<%--<asp:SqlDataSource ID="ddlDataSource" runat="server"
    ConnectionString="<%$ ConnectionStrings:FrontOfficeConnectionString %>"
    SelectCommand="SELECT TabCategory.CategoryCode, TransCategory.Description FROM TabCategory INNER JOIN TransCategory ON TabCategory.CategoryCode = TransCategory.CategoryCode WHERE (TransCategory.LanguageID = @Language)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnLang" DefaultValue="1" Name="Language"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>--%>










