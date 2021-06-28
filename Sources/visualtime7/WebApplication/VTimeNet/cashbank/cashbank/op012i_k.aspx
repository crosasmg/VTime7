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
Dim mstrCodisplOri As Object
Dim mstrQueryString As String


'% insPreOP012I_K: se definen los campos de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreOP012I_K()
	'--------------------------------------------------------------------------------------------
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
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=8605>" & GetLocalResourceObject("tcdTransDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""50%"">")


Response.Write(mobjValues.DateControl("tcdTransDate", ldtmTransdate, True, GetLocalResourceObject("tcdTransDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD CLASS=""HighLighted""><LABEL ID=40089>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("			<TD CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=8596>" & GetLocalResourceObject("valOriAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("valOriAccount", "tabBank_acc", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Session("nOriAccount"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "InsChangeAccount();",  ,  , GetLocalResourceObject("valOriAccountToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(40090, "optTrans", GetLocalResourceObject("optTrans_CStr1Caption"), mstrInternalTransfer, CStr(1),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=8578>" & GetLocalResourceObject("tcnAmountTransfCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("				<TABLE>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.NumericControl("tcnAmountTransf", 18, mobjValues.StringToType(Session("nAmountTransf"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnAmountTransfToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.DIVControl("lblCurrency",  , mobjValues.getMessage(Session("nCurrencyOri"), "Table11")))


Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("                </TABLE>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(40091, "optTrans", GetLocalResourceObject("optTrans_CStr0Caption"), mstrExternalTransfer, CStr(0), CStr(0), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=2>")

	'=mobjvalues.CheckControl("chkPrintTransf", GetLocalResourceObject("chkPrintTransfCaption"),"1",,,true)
Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>		        ")

	
	Response.Write(mobjValues.HiddenControl("cboCurrency", lintCurrency))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mstrCodisplOri = Session("OP006_sCodispl")
With Request
	mstrQueryString = "nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&sProcessType=" & .QueryString.Item("sProcessType") & "&nAmount=" & .QueryString.Item("nAmount") & "&nInterest=" & .QueryString.Item("nInterest") & "&sClient=" & .QueryString.Item("sClient") & "&nPayOrderTyp=" & .QueryString.Item("nPayOrderTyp") & "&nAmoTax=" & .QueryString.Item("nAmoTax") & "&nAgency=" & .QueryString.Item("nAgency") & "&nRequestNu=" & .QueryString.Item("nRequestNu")
End With

mobjValues.sCodisplPage = "op012i_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
<%If mstrCodisplOri <> vbNullString Then%>
        top.document.location.href='/VTimeNet/Common/GoTo.aspx?sCodispl=<%=mstrCodisplOri%>'
<%Else%>
        return (true);
<%End If%>  
}
//% InsChangeAccount:
//------------------------------------------------------------------------------------------
function InsChangeAccount(){
//------------------------------------------------------------------------------------------
	var lintTransf
    lintTransf = <% =mobjValues.StringToType(Session("TypTransf"), eFunctions.Values.eTypeData.etdDouble)%>;
    if (typeof(self.document.forms[0].valOriAccount.DoIt)=='undefined')
        self.document.forms[0].valOriAccount.DoIt = true
    if (self.document.forms[0].valOriAccount.DoIt)
		    if (lintTransf !=2) 
			    ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=Account&sAccount=" + self.document.forms[0].valOriAccount.value, "ShowDefValuesAccBankCash", 1, 1,"no","no",2000,2000);
}
//% insStateZone: se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if(typeof(tcdTransDate)!='undefined'){
			tcdTransDate.disabled = false;
			tcdTransDate.value = GetDateSystem();
			btn_tcdTransDate.disabled = tcdTransDate.disabled;
			valOriAccount.disabled = false;
			valOriAccount.value = '';
			UpdateDiv('valOriAccountDesc', '', 'Normal');
			btnvalOriAccount.disabled = valOriAccount.disabled;
			tcnAmountTransf.disabled = false;
			tcnAmountTransf.value = '';
		}
	}
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("OP012I", "OP012I_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTransBank" ACTION="ValCashBank.aspx?<%=mstrQueryString%>">
	<BR><BR>
<%
If CStr(Session("TypTransf")) = "2" Then
	With Response
		.Write("<SCRIPT>")
		'+ Se habilitan los botones de Aceptar y Cancelar	
		.Write("ClientRequest(301)")
		.Write("</SCRIPT>")
	End With
End If
Call insPreOP012I_K()
mobjValues = Nothing
%>
</BODY>
</FORM>
</HTML>




