<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% inspreVIL7003: se definen los campos de la forma
'--------------------------------------------------------------------------------------------
Private Sub inspreVIL7003()
	'--------------------------------------------------------------------------------------------
	
Response.Write("	" & vbCrLf)
Response.Write("<BR><BR>" & vbCrLf)
Response.Write("	")

	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <BR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD WIDTH=""35%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdFromDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdFromDate", CStr(Today),  , GetLocalResourceObject("tcdFromDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""35%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD WIDTH=""35%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdToDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdToDate", CStr(Today),  , GetLocalResourceObject("tcdToDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""35%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "VIL7003_k.aspx", 1, ""))
	.Write(mobjMenu.setZone(1, "VIL7003", "VIL7003_k.aspx"))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VIL7003" ACTION="valPolicyRep.aspx?sMode=1">
<%Call inspreVIL7003()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






