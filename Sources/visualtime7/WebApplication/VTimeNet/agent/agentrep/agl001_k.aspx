<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsGeneralFunction As eGeneral.Ctrol_date


'%insPreAGL001:Se cargan los controles de la ventana
'----------------------------------------------------------------------------
Private Sub insPreAGL001()
	'----------------------------------------------------------------------------
	mclsGeneralFunction.Find(9)
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		")


Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))


Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=25%><LABEL ID=11288>" & GetLocalResourceObject("tcdInitDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=25%>")


Response.Write(mobjValues.DateControl("tcdInitDate", mclsGeneralFunction.dEffecdate,  , GetLocalResourceObject("tcdInitDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=25%><LABEL ID=100879>" & GetLocalResourceObject("tcdEndDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=25%>")


Response.Write(mobjValues.DateControl("tcdEndDate", "",  , GetLocalResourceObject("tcdEndDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<TABLE width=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD width=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD width=25%>")


Response.Write(mobjValues.CheckControl("chkUpdate_Ind", GetLocalResourceObject("chkUpdate_IndCaption")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD width=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD width=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	mobjValues = Nothing
	mclsGeneralFunction = Nothing
End Sub

</script>
<%Response.Expires = 0
Response.CacheControl = False

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsGeneralFunction = New eGeneral.Ctrol_date
End With
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>

<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//%insStateZone: 
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
	<%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>


<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AGL001_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>

</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRIntermAccount" ACTION="ValAgentRep.aspx?mode=1">
<%
Call insPreAGL001()
%>
</FORM>
</BODY>
</HTML>




