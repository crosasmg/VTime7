<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'-   Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'%   insDefineHeader: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("tcdInitialCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD WIDTH=""15%"">")


Response.Write(mobjValues.DateControl("tcdInitial", "",  , GetLocalResourceObject("tcdInitialToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD WIDTH=""70%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("tcdFinishCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD WIDTH=""15%"">")


Response.Write(mobjValues.DateControl("tcdFinish", "",  , GetLocalResourceObject("tcdFinishToolTip"),  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD WIDTH=""70%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("	</TABLE> " & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("</TABLE>")

End Sub

</script>
<%Response.Expires = -1


mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CAL011_K"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>




<SCRIPT LANGUAGE=JavaScript>


//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%   insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
        
}


</SCRIPT>
<%
'	Response.Write mobjValues.StyleSheet()
'	Response.Write mobjMenu.MakeMenu("CAL011","CAL011_K.aspx",1,vbNullstring)
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL011_k.aspx", 1, ""))
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With
mobjMenu = Nothing
%>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="post" ID="FORM" NAME="CAL011" ACTION="valPolicyRep.aspx?Mode=1">
<BR>
<BR>
<%
Call insDefineHeader()

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>







