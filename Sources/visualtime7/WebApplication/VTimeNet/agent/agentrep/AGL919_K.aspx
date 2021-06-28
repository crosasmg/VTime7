<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjNetFrameWork As eNetFrameWork.Layout


'%insPreAGL919: Se cargan los controles de la ventana.
'--------------------------------------------------------------------------------------------
Private Sub insPreAGL919()
	'--------------------------------------------------------------------------------------------
	Dim lclsCtrol_date As eGeneral.Ctrol_date
	lclsCtrol_date = New eGeneral.Ctrol_date
	
	Dim lintYear As Short
	Dim lintMonth As Short
	Dim ldtmInit_date As Object
	
	lintYear = Year(Today)
	lintMonth = Month(Today)
	
	If lclsCtrol_date.Find(78) Then
'UPGRADE_NOTE: Date operands have a different behavior in arithmetical operations. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1023.htm
		ldtmInit_date = System.Date.FromOADate(lclsCtrol_date.dEffecdate.ToOADate + 1)
		lintYear = Year(ldtmInit_date)
		lintMonth = Month(ldtmInit_date)
	Else
		ldtmInit_date = DateSerial(Year(Today), Month(Today), 1)
	End If
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%""><LABEL ID=0>" & GetLocalResourceObject("tcdInit_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdInit_date", ldtmInit_date,  , GetLocalResourceObject("tcdInit_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEnd_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

'UPGRADE_NOTE: Date operands have a different behavior in arithmetical operations. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1023.htm

Response.Write(mobjValues.DateControl("tcdEnd_date", CStr(DateSerial(lintYear, lintMonth, Microsoft.VisualBasic.Day(System.Date.FromOADate(DateSerial(lintYear, lintMonth + 1, 1).ToOADate - 1)))),  , GetLocalResourceObject("tcdEnd_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lclsCtrol_date = Nothing
End Sub

</script>
<%Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGL919_K")

Response.Cache.SetCacheability(HttpCacheability.NoCache) 


mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = "AGL919_K"
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
    document.VssVersion="$$Revision: 2 $|$$Date: 10/12/03 13:14 $|$$Author: Nvaplat18 $"

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
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AGL919_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	'+ Se agrega zona para dejar des-habilitado el botón aceptar
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="AGL919" ACTION="ValAgentRep.aspx?Mode=1">
<BR><BR><BR>
<%
Call insPreAGL919()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("AGL919_K")
mobjNetFrameWork = Nothing
%>




