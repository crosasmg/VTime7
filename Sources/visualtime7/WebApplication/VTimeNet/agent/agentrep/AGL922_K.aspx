<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjNetFrameWork As eNetFrameWork.Layout


'%insPreAGL922: Se cargan los controles de la ventana.
'--------------------------------------------------------------------------------------------
Private Sub insPreAGL922()
	'--------------------------------------------------------------------------------------------
	Dim lclsCtrol_date As eGeneral.Ctrol_date
	Dim lclsT_com_prod As eAgent.ValAgentRep
	
	lclsCtrol_date = New eGeneral.Ctrol_date
	lclsT_com_prod = New eAgent.ValAgentRep
	
	Call lclsCtrol_date.Find(78)
	Call lclsT_com_prod.Find_FECUS_range()
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%""><LABEL ID=0>" & GetLocalResourceObject("tcdInit_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdInit_date", CStr(lclsT_com_prod.dMin_pay_date),  , GetLocalResourceObject("tcdInit_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEnd_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEnd_date", CStr(lclsCtrol_date.dEffecdate),  , GetLocalResourceObject("tcdEnd_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lclsCtrol_date = Nothing
	lclsT_com_prod = Nothing
End Sub

</script>
<%Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGL922_K")

Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = "AGL922_K"
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 19/12/03 17:57 $|$$Author: Nvaplat18 $"

//%insStateZone: Se habilita/deshabilita los campos de la ventana.
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
}

//%insCancel: Acciones a efectuar al cancelar la transacción.
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return true;
}

//%insFinish: Acciones a efectuar al finalizar la transacción.
//-------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>	
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AGL922_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	'+ Se agrega zona para dejar des-habilitado el botón aceptar
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="AGL922" ACTION="ValAgentRep.aspx?Mode=1">
<BR><BR><BR>
<%
Call insPreAGL922()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("AGL922_K")
mobjNetFrameWork = Nothing
%>




