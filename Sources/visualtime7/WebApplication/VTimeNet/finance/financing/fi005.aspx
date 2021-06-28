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
	Dim lclsFinanceCO As eFinance.financeCO
	lclsFinanceCO = New eFinance.financeCO
	
	Call lclsFinanceCO.insPreFI005(session("ncontrat"))
	With lclsFinanceCO
		'Response.Write "namount:" & .nAmount	
		
Response.Write("" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"" BORDER = 0>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN = 5 CLASS=""HighLighted""><LABEL><A NAME=""Datos del contrato"">" & GetLocalResourceObject("AnchorDatos del contratoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN = 5><HR></TD>	" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>            " & vbCrLf)
Response.Write("				<TD><LABEL ID=11199>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "table9", 1, .DefaultValueFI005("cbeOffice"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("				<TD width = ""5%"">&nbsp;</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=11188>" & GetLocalResourceObject("cbeCurr_contCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurr_cont", "Table11", 1, .DefaultValueFI005("cbeCurrency"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurr_contToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=11192>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClient", .DefaultValueFI005("dtcClient"),  , GetLocalResourceObject("dtcClientToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=11198>" & GetLocalResourceObject("tcnInterestCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.NumericControl("tcnInterest", 5, .DefaultValueFI005("tcnInterest"),  ,  ,  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN = 5 CLASS=""HighLighted""><LABEL><A NAME=""Información de pago"">" & GetLocalResourceObject("AnchorInformación de pagoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN = 5><HR></TD>	" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11196>" & GetLocalResourceObject("tcnInitialCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.NumericControl("tcnInitial", 18, .DefaultValueFI005("nInitial"),  ,  , True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11193>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", 1, .DefaultValueFI005("cbeCurrency"),  ,  ,  ,  ,  , "ShowDefVal(""Exchange"");",  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11197>" & GetLocalResourceObject("tcnIntAmouCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.NumericControl("tcnIntAmou", 18, CStr(.nIntAmount),  ,  , True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11195>" & GetLocalResourceObject("tcnExchangeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.NumericControl("tcnExchange", 18, "",  ,  , True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11189>" & GetLocalResourceObject("tcnAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.NumericControl("tcnAmount", 18, CStr(.nAmountDraft),  ,  , True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		        <TD><LABEL ID=11200>" & GetLocalResourceObject("cbePayment_wayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")


Response.Write(mobjValues.PossiblesValues("cbePayment_way", "table258", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbePayment_wayToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("		</TABLE>")

		
	End With
	Response.Write("<SCRIPT>")
	Response.Write("ShowDefVal('Exchange');")
	Response.Write("</" & "Script>")
	lclsFinanceCO = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues


mobjValues.ActionQuery = session("bQuery")

mobjValues.sCodisplPage = "fi005"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
function insStateZone(){
}
function ShowDefVal(sField)
{	
	ShowPopUp("/VTimeNet/Finance/Financing/ShowDefValues.aspx?Field=" + sField +  "&nCurrency=" + self.document.forms[0].cbeCurrency.value + "&nCurr_cont=" + self.document.forms[0].cbeCurr_cont.value, "ShowDefValuesFinance" , 1, 1,"no","no",2000,2000);
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
        <%
With Response
	.Write(mobjValues.ShowWindowsName("FI005"))
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "FI005", "FI005.aspx"))
End With
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmInitialCollect" ACTION="valFinancing.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
%>
</FORM>
</BODY>
</HTML>





