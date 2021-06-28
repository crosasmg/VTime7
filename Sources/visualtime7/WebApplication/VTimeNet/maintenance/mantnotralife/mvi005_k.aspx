<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues



'**% insPreMVI005: he control of the page are loaded
'% insPreMVI005: Se cargan los controles de la ventana
'----------------------------------------------------------------------------
Private Sub insPreMVI005()
	'----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		")

	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("" & vbCrLf)
Response.Write("		<BR></BR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%><LABEL ID=0>" & GetLocalResourceObject("valFundsCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%>")


Response.Write(mobjValues.PossiblesValues("valFunds", "tabFund_inv", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insShowQuan_avail(this)", True,  , GetLocalResourceObject("valFundsToolTip")))


Response.Write("</TD>			" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%><LABEL ID=0>" & GetLocalResourceObject("tcdEffecDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%>")


Response.Write(mobjValues.DateControl("tcdEffecDate", CStr(Today),  , GetLocalResourceObject("tcdEffecDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%><LABEL ID=0>" & GetLocalResourceObject("cbeMovetypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%>")


Response.Write(mobjValues.PossiblesValues("cbeMovetype", "Table415", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeMovetypeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%><LABEL ID=0>" & GetLocalResourceObject("tcnUnitCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnUnit", 14, CStr(0),  , "", True, 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DIVControl("lblQuan_avail",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1


With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With
mobjValues.sCodisplPage = "MVI005"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//**+ For the Source Safe control "DO NOT REMOVE"
//+ Para Control de Versiones "NO REMOVER"
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
	
//**% insStateZone: This function enabled the fields of the form according to the action to execute.
//% insStateZone: Esta función habilita los campos de la forma según la acción a ejecutar.
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
    var lintIndex = 0
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled=false
        
//    document.images["btnvalFunds"].disabled=false
    document.images["btn_tcdEffecDate"].disabled=false
}

//**% insCancel: This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//**% insFinish: This function executes the action to finish of the page.
//% insFinish: Esta función ejecuta la acción finalizar de la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

//**% insShowQuan_avail: this function show the total units availables in the fund
//% insShowQuan_avail: Permite mostrar el total de unidades disponibles en el fondo
//------------------------------------------------------------------------------------------
function insShowQuan_avail(Field){
//------------------------------------------------------------------------------------------
   lstrQueryString = "/VTimeNet/Maintenance/MantNoTraLife/ShowDefValues.aspx?Field=nQuan_avail";
   lstrQueryString = lstrQueryString + "&nFunds=" + Field.value;
   ShowPopUp(lstrQueryString,"Values",1,1,"no","no", 2000, 2000);
}
</SCRIPT>    
	<%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>


<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MVI005_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>

</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmStockfund" ACTION="valMantNoTraLife.aspx?mode=1">
<%
Call insPreMVI005()
%>
</FORM>
</BODY>
</HTML>











