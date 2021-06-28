<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
    </SCRIPT>


    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>
// insStateZone :
//-----------------------------------------------------------------------------------
function insStateZone(){
}

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return (true);
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
<%
    With Response
        .Write(mobjValues.StyleSheet())
        .Write(mobjMenu.MakeMenu("COL502", "COL502_K.aspx", 1, ""))
    End With
    mobjMenu = Nothing
    
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmImputeAutomatic" ACTION="valCollectionRep.aspx?mode=1">
<BR><BR><BR>    
    <TABLE WIDTH="100%">   
        <TR>
            <TD WIDTH="23%"><LABEL ID=4639><%= GetLocalResourceObject("AnchorCaption")%></LABEL></TD>
            <TD WIDTH="25%"><%= mobjValues.PossiblesValues("cbeInsur_area", "table5001", eFunctions.Values.eValuesType.clngComboType, Session("nInsur_area"), , , , , , , , , GetLocalResourceObject("AnchorToolTip"))%></TD>                  
            <TD WIDTH="4%">&nbsp;</TD>            
            <TD WIDTH="23%"><LABEL ID=4640><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
            <TD WIDTH="25%"><% mobjValues.TypeList = 1
                                mobjValues.List = "1,2"
    
                                Response.Write(mobjValues.PossiblesValues("cbeWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType, "1", , , , , , , , , GetLocalResourceObject("Anchor3ToolTip")))%></TD>
        </TR>         
        <TR><TD COLSPAN="5">&nbsp;</TD></TR>
        <TR>
            <TD><LABEL ID=9913><%= GetLocalResourceObject("tcdLimit_payCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdLimit_pay", CStr(Today),  , GetLocalResourceObject("tcdLimit_payToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=9914><%= GetLocalResourceObject("tcdPayDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdPayDate", CStr(Today),  , GetLocalResourceObject("tcdPayDateToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





