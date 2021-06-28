<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
'**+ Si la ventana pertenece al encabezado de la transacción colocar después de la referencia a GenFunctions.js:
'**+ <% %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		

	Response.Write mobjValues.StyleSheet()
	If Request.QueryString("Type") <> "PopUp" Then
'**+ Si se trata de una ventana que no forma parte del encabezado de la transacción colocar:
		Response.Write mobjMenu.setZone(2,"Codispl","Nombre_de_la_página.aspx") 

'**+ Si la ventana pertenece al encabezado de la transacción colocar:
		Response.Write mobjMenu.MakeMenu("Codispl", "Nombre_de_la_página.aspx", 1, vbNullString)

		Set mobjMenu = Nothing
		Response.Write "<NOTSCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>"
	End If
    %>
<SCRIPT LANGUAGE=JavaScript>
//**+ Las siguientes funciones deben colocarse sólo si la página corresponde al encabezado de la transacción

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="Nombre_de_la_página" ACTION="Página_de_validaciones.aspx?sMode=2">
<%=mobjValues.ShowWindowsName("Codispl")%>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnFieldCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnField", 10, vbNullString)%></TD>
			<TD WIDTH=10%>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdFieldCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdField", CStr(Today),  , GetLocalResourceObject("tcdFieldToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("[val][cbe]FieldCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("[val][cbe]Field", "Table10", eFunctions.Values.eValuesType.clngComboType)%> </TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctFieldCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctField", 10, vbNullString)%></TD> 
        </TR>
        <TR>
		    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>
		<TR>
		    <TD><%=mobjValues.OptionControl(0, "optField", GetLocalResourceObject("optField_1Caption"), "1", "1")%> </TD>
	        <TD><%=mobjValues.CheckControl("chkField", GetLocalResourceObject("chkFieldCaption"), "1", "1")%> </TD>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("dtcFieldCaption") %></LABEL></TD>
	        <TD COLSPAN="2"><%=mobjValues.ClientControl("dtcField", vbNullString,  , GetLocalResourceObject("dtcFieldToolTip"))%> </TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>





