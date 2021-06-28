<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

Dim ldtmTransdate As Object
Dim lintCurrency As Object

Dim mstrInternalTransfer As String
Dim mstrExternalTransfer As String


'----------------------------------------------------------------------------
Private Sub insPreOP012I_K()
	'----------------------------------------------------------------------------
	ldtmTransdate = Today
	lintCurrency = 0
	
	'+ Determina el tipo de transferencia que se está ejecutando
	'+ Transferencia externa.
	If CStr(Session("TypTransf")) = "2" Then
		mstrInternalTransfer = "0"
		mstrExternalTransfer = "1"
		'+ Transferencia interna
	Else
		mstrInternalTransfer = "1"
		mstrExternalTransfer = "0"
	End If
	
	'If Session("dTransDate")  <> vbnullstring Then
	'	ldtmTransdate = mobjValues.TypeToString(Session("dTransDate"),eFunctions.Values.eTypeData.etdDate)
	'End If
	'If Session("nCurrency")  <> vbnullstring Then
	'	lintCurrency = mobjValues.TypeToString(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	'End If	
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=10pcx><LABEL ID=8605>" & GetLocalResourceObject("tcdTransDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdTransDate", ldtmTransdate, True, GetLocalResourceObject("tcdTransDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=8596>" & GetLocalResourceObject("valOriAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("valOriAccount", "tabBank_acc", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Session("nOriAccount"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "InsChangeAccount();", True,  , GetLocalResourceObject("valOriAccountToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=40089>" & GetLocalResourceObject("AnchorCaption") & "</LABEL><HR></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=8578>" & GetLocalResourceObject("tcnAmountTransfCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><TABLE><TR><TD>" & vbCrLf)
Response.Write("                ")


Response.Write(mobjValues.NumericControl("tcnAmountTransf", 19, mobjValues.StringToType(Session("nAmountTransf"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnAmountTransfToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("" & vbCrLf)
Response.Write("                </TD><TD>" & vbCrLf)
Response.Write("                    ")


Response.Write(mobjValues.DIVControl("lblCurrency"))


Response.Write(" " & vbCrLf)
Response.Write("                </TD></TR>" & vbCrLf)
Response.Write("                </TABLE>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("cboCurrency", lintCurrency))


Response.Write("&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(40090, "optTrans", GetLocalResourceObject("optTrans_CStr1Caption"), mstrInternalTransfer, CStr(1),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=2>")


Response.Write(mobjValues.CheckControl("chkPrintTransf", GetLocalResourceObject("chkPrintTransfCaption"), "1",  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(40091, "optTrans", GetLocalResourceObject("optTrans_CStr0Caption"), mstrExternalTransfer, CStr(0), CStr(0), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>")

End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "op012_k"
%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//InsChangeAccount()
//------------------------------------------------------------------------------------------
function InsChangeAccount(){
    if (typeof(self.document.forms[0].valOriAccount.DoIt)=='undefined')
        self.document.forms[0].valOriAccount.DoIt = true
    if (self.document.forms[0].valOriAccount.DoIt)
	    ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=Account&sAccount=" + self.document.forms[0].valOriAccount.value, "ShowDefValuesAccBankCash", 1, 1,"no","no",2000,2000);
}
</SCRIPT>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<SCRIPT> 
		function insStateZone(){
			with (self.document.forms[0]){
				tcdTransDate.disabled = false;
				tcdTransDate.value = GetDateSystem();
				btn_tcdTransDate.disabled = tcdTransDate.disabled;
				valOriAccount.disabled = false;
				valOriAccount.value = '';
				UpdateDiv('valOriAccountDesc', '', 'Normal');
				btnvalOriAccount.disabled = valOriAccount.disabled;
				tcnAmountTransf.disabled = false;
				tcnAmountTransf.value = '';
				chkPrintTransf.disabled = false;
				UpdateDiv('lblCurrency','');
			}
		}
	</SCRIPT>

<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("OP012", "OP012_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTransBank" ACTION="ValCashBank.aspx?x=1">
	<BR>
	<BR>
<%

Call insPreOP012I_K()

If CStr(Session("TypTransf")) = "2" Then
	Session("TypTransf") = "0"
	Session("OP006_sCodispl") = Request.QueryString.Item("sCodispl")
	Response.Write("<SCRIPT>self.document.forms[0].action=self.document.forms[0].action + '&nZone=1'</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].action=self.document.forms[0].action + '&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].action=self.document.forms[0].action + '&nMainAction=301'</SCRIPT>")
	Response.Write("<SCRIPT>document.forms[0].submit();</SCRIPT>")
End If

mobjValues = Nothing
%>
</TABLE>		
</BODY>
</FORM>
</HTML>





