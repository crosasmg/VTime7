<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MSI001"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MSI001_K.aspx", 1, ""))
End With
%>
<SCRIPT>
//% insStateZone: se controla el estado de los campos de la página
//---------------------------------------------------------------------------------------
function insStateZone(){
//---------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        optBussines[0].disabled = false;
        optBussines[1].disabled = false;
        optBussines[2].disabled = false;
        cbeBrancht.disabled = false;
        cbeTranType.disabled = false;
    }
}

//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true
}

//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $" 
</SCRIPT>
</HEAD>
<BODY>
<BR></BR>
<FORM METHOD="post" ID="FORM" ACTION="valmantclaim.aspx?mode=1">
	<TABLE WIDTH="100%"> 
		<TR>
			<TD COLSPAN=2>&nbsp;</TD>
			<TD CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR> 
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchtCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBrancht", "Table37", 1,  , False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchtToolTip"))%> </TD>
			<TD><%=mobjValues.OptionControl(100805, "optBussines", GetLocalResourceObject("optBussines_CStr1Caption"), CStr(1), CStr(1),  , True)%></TD>
		</TR>
		<TR> 
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeTranTypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeTranType", "Table192", 1,  , False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTranTypeToolTip"))%> </TD>
			<TD><%=mobjValues.OptionControl(100806, "optBussines", GetLocalResourceObject("optBussines_CStr2Caption"), CStr(2), CStr(2),  , True)%></TD>
		</TR>
		<TR>
			<TD COLSPAN=2>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100807, "optBussines", GetLocalResourceObject("optBussines_CStr3Caption"),  , CStr(3),  , True)%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
%>




