<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "er006_k"
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<SCRIPT LANGUAGE=JavaScript>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 4 $|$$Date: 7/08/04 11:45a $|$$Author: Nsoler $"

    //% insStateZone: se controla el estado de los campos de la página
    //--------------------------------------------------------------------------------------------
    function insStateZone() {
        //--------------------------------------------------------------------------------------------
    }

    //% insCancel: se controla la acción Cancelar de la página
    //--------------------------------------------------------------------------------------------
    function insCancel() {
        //--------------------------------------------------------------------------------------------
        return true;
    }

    //% insFinish: se controla la acción Cancelar de la página
    //--------------------------------------------------------------------------------------------
    function insFinish() {
        //--------------------------------------------------------------------------------------------
        return true;
    }
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("ER006", "ER006.aspx", 1, vbNullString))
	Response.Write(mobjMenu.setZone(1, "ER006", "ER006.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="ER006" ACTION="valErrors.aspx?sMode=2">
<BR><BR>
<TABLE WIDTH="100%">
    <TR>
        <TD><LABEL ID=6781>Transacción</LABEL></TD>
        <TD COLSPAN="3"><%=mobjValues.PossiblesValues("valCodisp", "Windows", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  ,  , 8,"Código de la transacción", eFunctions.Values.eTypeCode.eString)%></TD>
    </TR>
    <TR>
		<TD><LABEL ID=6782>Responsable</LABEL></TD>
		<TD><%=mobjValues.TextControl("tctUserAssign", 12, vbNullString,  ,"Usuario a asignar la transacción")%></TD>
        <TD><LABEL ID=6783>Estado a asignar</LABEL></TD>
        <TD>
        <%
mobjValues.BlankPosition = False
mobjValues.TypeList = CShort("1")
mobjValues.List = "2"
Response.Write(mobjValues.PossiblesValues("cbeStaterr", "Table999", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  ,"Estado a asignar"))
%>
		</TD>
    </TR>
</TABLE>
</FORM> 
</BODY>
</HTML>










