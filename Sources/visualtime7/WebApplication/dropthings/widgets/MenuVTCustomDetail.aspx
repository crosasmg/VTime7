<%@ Page Language="VB" uiculture="auto" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="MenuVTCustomDetail.aspx.vb" Inherits="dropthings_MenuVTCustomDetail" meta:resourcekey="MenuVTCustomDetailTitleResource" title="Custom Windows Selector" %>
<%@ MasterType TypeName="DropthingsMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 

<script type="text/jscript" >

function GridView_SelectionChanged(s, e) {  

    GridView.PerformCallback();    
}

function GridView_EndCallback(s, e) {
    
    if(typeof(GridView.cp_sList) != 'undefined'){
        document.getElementById("DivSelectedList").innerHTML = "";    
        document.getElementById("DivSelectedList").innerHTML = GridView.cp_sList;        
    }
    
    var counter = document.getElementById("selCount");
    if(counter != null) {
        setInnerText(counter, "");      
        setInnerText(counter, GridView.GetSelectedRowCount().toString());      
    }

}

function setInnerText(element, text) { 

    if(typeof element.textContent != 'undefined') { 
        element.textContent = text; 
    } 
    else if (typeof element.innerText != 'undefined') { 
        element.innerText = text; 
    } 
    else if (typeof element.removeChild != 'undefined') { 
        while (element.hasChildNodes()) { 
            element.removeChild(element.lastChild); 
        } 
        element.appendChild(document.createTextNode(text)); 
    } 
}
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

    <div id="Div1"> 
        <table width="100%" border="0">                
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Selected items:" meta:resourcekey="Label1Resource"></asp:Label>
                </td>
            </tr>
            <tr>
               <td style="vertical-align:top" width="25%" >                
                    <div id="DivSelectedList" style="font-size:x-small;font-family: Verdana; font-weight: normal; color: green;"></div>
                    <p> 
                    <asp:Label ID="lblselCount" runat="server" Text="Selected count:" meta:resourcekey="lblselCountResource"></asp:Label>                    
                    <a id="selCount" type="text/plain">0</a>                        
                    </p>
               </td>  
                <td width="75%"> 
                    <dxwgv:ASPxGridView ID="GridView" ClientInstanceName="GridView" runat="server" 
                        KeyFieldName="WindowLogicalCode" AutoGenerateColumns="False" Width="90%" 
                        Font-Names="Tahoma" Font-Size="Small">
                        <SettingsPager PageSize="10">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFooter="True" ShowGroupButtons="True" ShowGroupPanel="True" ShowStatusBar="Hidden" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0"></dxwgv:GridViewCommandColumn>                    
                            <dxwgv:GridViewDataColumn FieldName="WindowLogicalCode" VisibleIndex="1" Caption="Código" meta:resourcekey="WindowLogicalCodeResource"></dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn FieldName="Description" VisibleIndex="2" Caption="Descripción" meta:resourcekey="DescriptionResource"></dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn FieldName="ModuleDescription" VisibleIndex="3" Caption="Módulo al que pertenece" meta:resourcekey="ModuleDescriptionResource"></dxwgv:GridViewDataColumn>
                        </Columns>
                        <ClientSideEvents 
                            SelectionChanged="function(s, e) { GridView_SelectionChanged(s, e) }" 
                            EndCallback="function(s, e) { GridView_EndCallback(s, e); }" />
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align= "right">
                    <asp:Button ID="Submmit" runat="server" Text="Procesar" meta:resourcekey="SubmmitResource" SkinID="tabButton" />
                </td>
            </tr>
        </table>           
    </div>
    
</asp:Content>

