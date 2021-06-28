<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

Dim mstrQueryString As String


'% insPreOP012E : Se cargan los datos de la transferencia externa.
'----------------------------------------------------------------------------
Private Sub insPreOP012E()
	Dim lblnActionQuery As Boolean
	'----------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName("OP012E"))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=8580>" & GetLocalResourceObject("dtcClient1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			")

	
	If CStr(Session("TypTransf")) = "0" Then
		lblnActionQuery = mobjValues.ActionQuery
		mobjValues.ActionQuery = True
Response.Write("" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClient1", Session("sClientName"),  , GetLocalResourceObject("dtcClient1ToolTip"),  ,  , "lblCliename1",  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			   ")

		mobjValues.ActionQuery = lblnActionQuery
	Else
Response.Write("" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.TextControl("lblName", 30, "",  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8579>" & GetLocalResourceObject("cbeBankCaption") & "</LABEL>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBank", "Table7", 1,  ,  ,  ,  ,  ,  , "document.forms[0].valAgency.Parameters.Param1.sValue=this.value",  ,  , GetLocalResourceObject("cbeBankToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8575>" & GetLocalResourceObject("valAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("   			<TD>")

	With mobjValues
		.Parameters.Add("nBank_code", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valAgency", "tabtab_bk_age", 2,  , True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valAgencyToolTip")))
	End With
Response.Write("" & vbCrLf)
Response.Write("        	<TD></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=8598>" & GetLocalResourceObject("cbeAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeAccount", "Table190", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeAccountToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8591>" & GetLocalResourceObject("tctExtAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctExtAccount", 25, "",  , GetLocalResourceObject("tctExtAccountToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=8581>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			")

	If CStr(Session("TypTransf")) = "0" Then
Response.Write("" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClient", Session("sClientName"),  , GetLocalResourceObject("dtcClientToolTip"),  ,  , "lblCliename",  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.ClientControl("dtcClient", "",  , GetLocalResourceObject("dtcClientToolTip"),  ,  , "lblCliename",  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblCliename", 30, "",  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=8573>" & GetLocalResourceObject("tctAbaNumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("tctAbaNum", 20, "",  , GetLocalResourceObject("tctAbaNumToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>    ")

	
	
End Sub

'% insPreOP012I : Se cargan los datos de la transferencia interna.
'----------------------------------------------------------------------------
Private Sub insPreOP012I()
	'----------------------------------------------------------------------------
	Dim lblnPreOP012I As Boolean
	Dim lclsBank_trans As eCashBank.Bank_trans
	lclsBank_trans = New eCashBank.Bank_trans
	If Request.QueryString.Item("nIntAccount") <> vbNullString Then
		Response.Write("<SCRIPT>mintIntAccount = " & Request.QueryString.Item("nIntAccount") & "</" & "Script>")
		
		lblnPreOP012I = lclsBank_trans.insPreOP012I(mobjValues.TypeToString(Session("nOriAccount"), eFunctions.Values.eTypeData.etdInteger), mobjValues.TypeToString(Session("nCurrencyOri"), eFunctions.Values.eTypeData.etdInteger), Session("dTransdate"), mobjValues.TypeToString(Session("nAmountTransf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.TypeToString(Request.QueryString.Item("nIntAccount"), eFunctions.Values.eTypeData.etdInteger))
		
		Session("InsPreOP012I") = lblnPreOP012I
		
		If Not lblnPreOP012I Then
			Response.Write("<script>alert('El importe convertido " & mobjValues.StringToType(CStr(lclsBank_trans.mdblAmountNew), eFunctions.Values.eTypeData.etdDouble) & " no debe ser mayor a 999999999999,99')</" & "Script>")
		End If
	End If
	
Response.Write("" & vbCrLf)
Response.Write("    ")

	If lblnPreOP012I Then
		If lclsBank_trans.mblnFrameVerify Then
Response.Write("" & vbCrLf)
Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
Response.Write("		<LABEL ID=40088><A HREF=""#Datos de verificación de la conversión""> " & GetLocalResourceObject("AnchorDatos de verificación de la conversiónCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("    </P>" & vbCrLf)
Response.Write("    ")

		End If
	End If
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    ")


Response.Write(mobjValues.ShowWindowsName("OP012I"))


Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=8592>" & GetLocalResourceObject("valIntAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("valIntAccount", "tabBank_acc", 2, Request.QueryString.Item("nIntAccount"),  ,  ,  ,  ,  , "LoadFields(this)",  ,  , GetLocalResourceObject("valIntAccountToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8594>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DIVControl("lblOfficeDes",  , lclsBank_trans.sOfficeDesc))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8598>" & GetLocalResourceObject("cbeAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DIVControl("lblAccountDesc",  , lclsBank_trans.sAcc_typeDesc))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        ")

	If lblnPreOP012I Then
		If lclsBank_trans.mblnExcToLoc Then
Response.Write("" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=100670>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>")


Response.Write(mobjValues.DIVControl("lblCurrencyOri",  , lclsBank_trans.sOriCurrencyDesc))


Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=8588> " & GetLocalResourceObject("tcnExchangeToLocalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			    <TD>" & vbCrLf)
Response.Write("					")


Response.Write(mobjValues.NumericControl("tcnExchangeToLocal", 10, CStr(lclsBank_trans.mdblExcToLoc),  , GetLocalResourceObject("tcnExchangeToLocalToolTip"), True, 6,  ,  ,  , "InsChangeExchange('" & Session("nCurrencyOri") & "','" & lclsBank_trans.nCurrency & "',insConvertNumber(this.value), 0);"))


Response.Write("" & vbCrLf)
Response.Write("					")


Response.Write(mobjValues.HiddenControl("tcnExchangeFromLocal", CStr(lclsBank_trans.mdblExcToLoc)))


Response.Write("" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("        ")

		End If
Response.Write("" & vbCrLf)
Response.Write("          " & vbCrLf)
Response.Write("        ")

		If lclsBank_trans.mblnExcToOrig Then
Response.Write("" & vbCrLf)
Response.Write("			<TR><TD COLSPAN=""4"">&nbsp;</TD></TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=8590>" & GetLocalResourceObject("tcnExchangeFromLocalCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DIVControl("lblCurrencyDest",  , lclsBank_trans.sCurrencyDest))


Response.Write("</TD>" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.NumericControl("tcnExchangeFromLocal", 10, CStr(lclsBank_trans.mdblExcFromLoc),  , GetLocalResourceObject("tcnExchangeFromLocalToolTip"), True, 6,  ,  ,  , "InsChangeExchange('" & Session("nCurrencyOri") & "','" & lclsBank_trans.nCurrency & "',0,insConvertNumber(this.value));"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.HiddenControl("tcnExchangeToLocal", CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("        ")

		End If
Response.Write("" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        ")

		If lclsBank_trans.mblnFrameVerify Then
Response.Write("" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD COLSPAN=""4"" CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			        <LABEL ID=100671><A NAME=""Datos de verificación de la conversión"">" & GetLocalResourceObject("AnchorDatos de verificación de la conversión2Caption") & "</A></LABEL>" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("            <TR><TD COLSPAN=""4""><HR></TD></TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcnAmountOldCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.NumericControl("tcnAmountOld", 18, mobjValues.StringToType(Session("nAmountTransf"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnAmountOldToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD><LABEL ID=8587>" & GetLocalResourceObject("tcnExchangeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.NumericControl("tcnExchange", 10, CStr(lclsBank_trans.mdblExchange),  , GetLocalResourceObject("tcnExchangeToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD><LABEL ID=8576>" & GetLocalResourceObject("tcnAmountNewCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.NumericControl("tcnAmountNew", 18, CStr(lclsBank_trans.mdblAmountNew),  , GetLocalResourceObject("tcnAmountNewToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("        ")

		End If
	End If
Response.Write("" & vbCrLf)
Response.Write("	</TABLE> ")

	lclsBank_trans = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjMenu = New eFunctions.Menues

With Request
	mstrQueryString = "nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&sProcessType=" & .QueryString.Item("sProcessType") & "&nAmount=" & .QueryString.Item("nAmount") & "&nInterest=" & .QueryString.Item("nInterest") & "&sClient=" & .QueryString.Item("sClient") & "&nPayOrderTyp=" & .QueryString.Item("nPayOrderTyp") & "&nAmoTax=" & .QueryString.Item("nAmoTax") & "&nAgency=" & .QueryString.Item("nAgency") & "&nRequestNu=" & .QueryString.Item("nRequestNu")
End With

mobjValues.sCodisplPage = "op012i"
%>
<SCRIPT LANGUAGE="JavaScript">
	var mintIntAccount

//% LoadFields:
//-------------------------------------------------------------------------------------------
function LoadFields(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (mintIntAccount != Field.value)
		    self.document.location.href="/VTimeNet/CashBank/CashBank/OP012I.aspx?sCodispl=OP012I&nIntAccount=" + Field.value;
    }
}

//InsChangeExchange:
//------------------------------------------------------------------------------------------
function InsChangeExchange(lintOriCurrency, lintDesCurrency, ldblExchangeToLocal, ldblExchangeFromLocal){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
		if (lintOriCurrency != lintDesCurrency)
			ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=Exchange&nOriCurrency=" + lintOriCurrency + "&nDesCurrency=" + lintDesCurrency + "&nExchangeFromLocal=" + ldblExchangeFromLocal + "&nExchangeToLocal=" + ldblExchangeToLocal, "ShowDefValuesAccBankCash", 1, 1,"no","no",2000,2000);
	}
}

// ClientName : Obtiene el nombre del cliente (Beneficiario) indicado en la orden de pago.
//              Sólo aplica para transferencias externas.
//------------------------------------------------------------------------------------------
function ClientName(sClient){
//------------------------------------------------------------------------------------------
	if (sClient != '') 
		insDefValues("ClientName", "sClient=" + sClient);
}

//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "MICROSOFT VISUAL STUDIO 6.0">
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "OP012I", "OP012I.aspx"))
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTransBank" ACTION="ValCashBank.aspx?<%=mstrQueryString%>">
<%
If CStr(Session("TypTransf")) = "1" Then
	Call insPreOP012I()
Else
	Call insPreOP012E()
End If

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




