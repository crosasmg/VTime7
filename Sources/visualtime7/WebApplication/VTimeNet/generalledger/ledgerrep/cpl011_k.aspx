<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'**- Possibles values objects are defined
'-   Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'**% insDefineHeader: This function allows to load the fields of the header
'%   insDefineHeader: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	Dim lclsCtrol_date As eLedge.Ctrol_date
	
	lclsCtrol_date = New eLedge.Ctrol_date
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'**+ Automatic premium entries.
		'+   Asientos automáticos de "Primas".
		
		Case "CPL011"
			Call lclsCtrol_date.Find(1, True)
			
			'**+ Automatic claim entries.
			'+   Asientos automáticos de "Siniestros".
			
		Case "CPL012"
			Call lclsCtrol_date.Find(2, True)
			
			'**+ Automatic Cash entries (premiums)      
			'+   Asientos automáticos de "Caja ingreso".
			
		Case "CPL013"
			Call lclsCtrol_date.Find(5, True)
			
			'**+ Automatic Cash expend entries.
			'+   Asientos automáticos de "Caja egreso".
			
		Case "CPL014"
			Call lclsCtrol_date.Find(6, True)
			
			'**+ Automatic current account entries.
			'+   Asientos automáticos de "Cuentas corrientes".
			
		Case "CPL015"
			Call lclsCtrol_date.Find(3, True)
	End Select
	
Response.Write("" & vbCrLf)
Response.Write("	<BR><BR>" & vbCrLf)
Response.Write("	<BR><BR>" & vbCrLf)
Response.Write("")

	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=25%> </TD>" & vbCrLf)
Response.Write("			<TD WIDTH=11%> </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD WIDTH=8%> </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD  WIDTH=""25%"" </TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD  CLASS=""HighLighted""><LABEL ID=10936><A NAME=""Condicion"">" & GetLocalResourceObject("AnchorCondicionCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=4 CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=11288>" & GetLocalResourceObject("tcdInit_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdInit_date", CStr(lclsCtrol_date.dEffecdate), False, GetLocalResourceObject("tcdInit_dateToolTip"),  ,  ,  ,  , True, 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<TD><LABEL ID=100879>" & GetLocalResourceObject("tcdTo_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdTo_date",  ,  ,  ,  ,  ,  ,  , False, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_CStr1Caption"), CStr(1), CStr(1),  ,  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_CStr2Caption"), CStr(2), CStr(2),  ,  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>	")

End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CPL011_K"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>

<SCRIPT>

//**% insCancel: This function is executed when the page is cancelled
//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//**% insStateZone: This function allows to control the status of the items page
//%   insStateZone: Se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


  <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CPL011_k.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With

mobjMenu = Nothing%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CPL015" ACTION="valLedgerRep.aspx?Mode=1">

<%
Call insDefineHeader()

mobjValues = Nothing
%>

</FORM>
</BODY>
</HTML>






