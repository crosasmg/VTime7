<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'%InsPreBC015: Se carga el contenido del grid según la información ingresada en la ventana.
'--------------------------------------------------------------------------------------------
Private Sub InsPreBC015()
	'--------------------------------------------------------------------------------------------
	Dim lclsDir_debit_cli As eClient.Dir_debit_cli
	Dim lstrClient As String
	Dim lstrTable_Account As String
	Dim lintAction As Object
	
	lstrClient = Session("sClient")
	lintAction = Request.QueryString.Item("nMainAction")
	lclsDir_debit_cli = New eClient.Dir_debit_cli
	With lclsDir_debit_cli
		.InsPreBC015(lstrClient, lintAction)
		lstrTable_Account = .sTableAccount
		If .bDisabledForm Then
			mobjValues.ActionQuery = True
		End If
		Response.Write("<SCRIPT> mstrType_Debit=""" & .sTyp_dirdeb & """</" & "Script>")
		Response.Write(mobjValues.HiddenControl("dEffecdate", CStr(.dEffecdate)))
		
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(0, "optType_Dir", GetLocalResourceObject("optType_Dir_1Caption"), CStr(2 - CDbl(.sTyp_dirdeb)), "1", "insChangeType_Debit(this);", .bDisabledOpt_Bk, 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(0, "optType_Dir", GetLocalResourceObject("optType_Dir_2Caption"), CStr(3 - CDbl(.sTyp_dirdeb)), "2", "insChangeType_Debit(this);", .bDisabledOpt_Cred, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnBill_DayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnBill_Day", 4, CStr(.nBill_Day),  , GetLocalResourceObject("tcnBill_DayToolTip"),  ,  ,  ,  ,  ,  ,  , 6))


Response.Write("</TD>            " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valBankCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

		mobjValues.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjValues.Parameters.Add("sTyp_dirdeb", .sTyp_dirdeb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valBank", "Tabtable7Typ_dir", eFunctions.Values.eValuesType.clngWindowType, CStr(.nBankExt), True,  ,  ,  ,  , "InsChange_Bank(this);",  ,  , GetLocalResourceObject("valBankToolTip"),  , 3))
		
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            ")

		mobjValues.bNumericText = True
Response.Write("" & vbCrLf)
Response.Write("            <TD>")

		mobjValues.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjValues.Parameters.Add("nBankExt", .nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valAccount", CStr(lstrTable_Account), eFunctions.Values.eValuesType.clngWindowType, .sAccount, True,  ,  ,  ,  , "InsChangeAccount();", .nBankExt = eRemoteDB.Constants.intNull, 25, GetLocalResourceObject("valAccountToolTip"), eFunctions.Values.eTypeCode.eString, 4, False))
		
Response.Write(" " & vbCrLf)
Response.Write("			</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tctBankAuthCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			")

		mobjValues.bNumericText = True
Response.Write("		    " & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.TextControl("tctBankAuth", 15, .sBankAuth,  , GetLocalResourceObject("tctBankAuthToolTip"),  ,  ,  ,  ,  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeTyp_cardCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeTyp_card", "table183", 1, CStr(.nCard_Type),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTyp_cardToolTip"),  , 8))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdExpirDatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdExpirDat", mobjValues.TypeToString(.dCardexpir, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdExpirDatToolTip"),  ,  ,  ,  , True, 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.CheckControl("chkDelDir_debit", GetLocalResourceObject("chkDelDir_debitCaption"), "2", "1",  , .bDisabledChk_Del, 10))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

		
		Response.Write(mobjValues.HiddenControl("bDisabledForm", CStr(.bDisabledForm)))
		
	End With
	Response.Write(mobjValues.BeginPageButton)
	lclsDir_debit_cli = Nothing
End Sub

</script>
<%Response.Expires = 0

With Server
	mobjMenu = New eFunctions.Menues
	mobjValues = New eFunctions.Values
End With

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"
</SCRIPT>
    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "BC015", "BC015.aspx"))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmBC015"  ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call InsPreBC015()
%>
</FORM>
<%="<SCRIPT>"%>
	var mstrType_Debit
	
//%insChangeType_Debit: Habilita o deshabilita los campos dependiendo del tipo de domiciliación.
//-----------------------------------------------------
function insChangeType_Debit(Field){
//-----------------------------------------------------
	if (mstrType_Debit != Field.value) {
		mstrType_Debit = Field.value;
		with (self.document.forms[0]){
			valAccount.value = "";
			valBank.value = "";
			UpdateDiv("valBankDesc", "");
			if (Field.value == "1") {
				cbeTyp_card.value = "";
				tcdExpirDat.value = "";
				tctBankAuth.value = "";
				tctBankAuth.disabled = false;
                tcnBill_Day.value = "";
				tcnBill_Day.disabled = false;
				valAccount.sTabName = "tabbk_account";
			}
			else {
				tctBankAuth.value = "";
                tcnBill_Day.value = "";
				tcnBill_Day.disabled = false;				
				valAccount.sTabName = "tabcred_card";
			}
			valBank.Parameters.Param2.sValue=Field.value;
		}
	}	
}

//%InsChange_Bank: Se habilita/deshabilita los campos al cambiar el valor del banco.
//-----------------------------------------------------
function InsChange_Bank(Field){
//-----------------------------------------------------
	with (self.document.forms[0]){
		if (Field.value == "0"  ||  Field.value.replace(/^\s*/, "").replace(/\s*$/, "") == ""  ) {
			valAccount.disabled = true;
			valAccount.value = "";
			btnvalAccount.disabled = valAccount.disabled;
		}
		else {
			valAccount.disabled = false;
			btnvalAccount.disabled = valAccount.disabled;
			valAccount.Parameters.Param2.sValue="0"+Field.value;
		}
	}
}

//%InsChangeAccount:  Se habilita/deshabilita los campos a la hora de cambiar la cuenta.
//--------------------------------------------------------------------------------------------
function InsChangeAccount(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (valBank.value != "" && valAccount.value != "" &&
		    mstrType_Debit != "1") {
			ShowPopUp("/VTimeNet/Client/ClientSeq/ShowDefValues.aspx?sField=Account&nBank_code=" + valBank.value + "&sAccount=" + valAccount.value + "&sType_debit=" + mstrType_Debit, "ShowDefValues", 1, 1,"no","no",2000,2000);
		}
		else {
			cbeTyp_card.value = "";
			tcdExpirDat.value = "";
			tctBankAuth.value = "";
		}
	}
}
<%="</SCRIPT>"%>
</BODY>
</HTML>




