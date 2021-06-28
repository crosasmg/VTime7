<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As Object


'% insDefineHeader: Se definen los campos de la pantalla PopUp
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcnAmount_fiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnAmount_fi", 18, Session("nAuxAmount_fi"),  , GetLocalResourceObject("tcnAmount_fiToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcnInterestCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnInterest", 4, Session("nAuxInterest"),  , GetLocalResourceObject("tcnInterestToolTip"),  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("cbeInterestCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeInterest", "table250", eFunctions.Values.eValuesType.clngComboType, Session("nAuxFrequency"),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInterestToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcnQ_draftCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnQ_draft", 5, Session("nAuxQ_draft"),  , GetLocalResourceObject("tcnQ_draftToolTip"),  ,  , 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcnInitialCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnInitial", 18, Session("AmountInitial"),  , GetLocalResourceObject("tcnInitialToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcnAmountQDraCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnAmountQDra", 18, Session("nAmountQDraft"),  , GetLocalResourceObject("tcnAmountQDraToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.AnimatedButtonControl("btn_calculate", "/VTimeNet/Images/batchStat06.png", GetLocalResourceObject("btn_calculateToolTip"),  , "self.document.forms[0].submit();"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.ButtonAcceptCancel("ShowDefValues();",  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	Response.Write(mobjValues.HiddenControl("hddFirst_draf", Session("hddFirst_draf")))
	Response.Write(mobjValues.HiddenControl("hdddEffecdate", Session("dEffecdate")))
Response.Write("" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>")

	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

If Request.QueryString.Item("nOrigen") = "1" Then
	Session("nAuxInterest") = ""
	Session("nAuxQ_draft") = ""
	Session("nAuxFrequency") = ""
	Session("AmountInitial") = ""
	Session("nAuxAmount_fi") = ""
	Session("nAmountQDraft") = ""
End If

With Request
	If Request.QueryString.Item("continue") = "No" Then
		Session("nAuxInterest") = .QueryString.Item("nInterest")
		Session("nAuxQ_draft") = .QueryString.Item("nQ_draft")
		Session("nAuxFrequency") = .QueryString.Item("nFrequency")
		Session("AmountInitial") = .QueryString.Item("nInitial")
		Session("dEffecdate") = .QueryString.Item("dEffecdate")
		Session("hddFirst_draf") = .QueryString.Item("hddFirst_draf")
	End If
End With

mobjValues.sCodisplPage = "fi010"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<%
Response.Write("<SCRIPT>")
Response.Write(" function ShowDefValues()")
Response.Write("{")
Response.Write("opener.document.forms[0].tcnInitial.value='" & Session("AmountInitial") & "';")
Response.Write("opener.document.forms[0].tcnInterest.value='" & Session("nAuxInterest") & "';")
Response.Write("opener.document.forms[0].tcnQ_draft.value='" & Session("nAuxQ_draft") & "';")
Response.Write("opener.document.forms[0].cbeFrequency.value='" & Session("nAuxFrequency") & "';")
Response.Write("opener.document.forms[0].tcdFirst_draf.value=self.document.forms[0].hddFirst_draf.value;")
Response.Write("opener.document.forms[0].hddFirst_draf.value=self.document.forms[0].hddFirst_draf.value;")
Response.Write("insDefValues('Accept','nada=nada');")
Response.Write("window.close()")
Response.Write("}")
Response.Write("</Script>")
%>



	
<%
With Response
	.Write(mobjValues.StyleSheet())
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="FI010" ACTION="valFinanceSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&sCodispl=FI010&nZone=1&Continue=Yes">
<%
With Response
	.Write(mobjValues.ShowWindowsName("FI010"))
	.Write("<BR>")
End With

Call insDefineHeader()
%>
</FORM>
</BODY>
</HTML>




