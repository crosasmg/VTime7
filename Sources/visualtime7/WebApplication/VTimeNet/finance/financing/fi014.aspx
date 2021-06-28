<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos 
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanceDraft As eFinance.FinanceDraft
	lclsFinanceDraft = New eFinance.FinanceDraft
	
	Call lclsFinanceDraft.Find(session("nContrat"), session("nDraft"))
	With lclsFinanceDraft
		'Response.Write "client: " & .sCliename  & "  cod: " & .sClient
		
Response.Write("" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11131>" & GetLocalResourceObject("dtcCodClieCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD COLSPAN = 3>")


Response.Write(mobjValues.ClientControl("dtcCodClie", .sClient,  , GetLocalResourceObject("dtcCodClieToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>            " & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11132>" & GetLocalResourceObject("cbeCurr_contCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurr_cont", "table11", 1, CStr(.nCurrency),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurr_contToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11134>" & GetLocalResourceObject("tcnDraft_amoCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.NumericControl("tcnDraft_amo", 19, CStr(.nAmount),  ,  , True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=11135>" & GetLocalResourceObject("tcdExpirDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdExpirDate", CStr(.dExpirdat),  , GetLocalResourceObject("tcdExpirDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=11127>" & GetLocalResourceObject("tcdCollectDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.DateControl("tcdCollectDate", CStr(.dStat_date),  , GetLocalResourceObject("tcdCollectDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("		</TABLE>")

		
	End With
	lclsFinanceDraft = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues


mobjValues.ActionQuery = session("bQuery")

mobjValues.sCodisplPage = "fi014"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
        <%
With Response
	.Write(mobjValues.ShowWindowsName("FI014"))
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "FI014", "FI014.aspx"))
End With
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollectReverse" ACTION="valFinancing.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

<%
Call insDefineHeader()
%>

</FORM>
</BODY>
</HTML>




