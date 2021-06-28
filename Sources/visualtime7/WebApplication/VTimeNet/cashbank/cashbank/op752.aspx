<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
'- Variable para almacenar el QueryString a pasar a la  página de validaciones 
Dim mstrQueryString As String


'% InsPreOP752: se realiza el manejo de los campos si el tipo de movimiento es diferente a
'%				depósito/redepósito
'--------------------------------------------------------------------------------------------
Private Sub InsPreOP752()
	'--------------------------------------------------------------------------------------------
	Dim lobjCash_mov As eCashBank.Cash_mov
	Dim nBank_Code As Object
	Dim sDocnumbe As Object
	
	lobjCash_mov = New eCashBank.Cash_mov
	
	nBank_Code = Session("nBank_Code")
	sDocnumbe = Session("sChequeNum")
	
	Call lobjCash_mov.FindChequeOP752(nBank_Code, sDocnumbe)
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=""0"">" & GetLocalResourceObject("valCompanyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("valCompany", "company", eFunctions.Values.eValuesType.clngWindowType, CStr(lobjCash_mov.nCompany),  ,  ,  ,  ,  , "ChangeCompany(this.value)", CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301,  , GetLocalResourceObject("valCompanyToolTip"),  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=""0"">" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(lobjCash_mov.nCurrency),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnAmount", 18, CStr(lobjCash_mov.nAmount),  , GetLocalResourceObject("tcnAmountToolTip"), True, 6,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301, 7))


Response.Write("</TD>			" & vbCrLf)
Response.Write("			<TD><LABEL ID=""0"">" & GetLocalResourceObject("tcdDateDocCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdDateDoc", CStr(lobjCash_mov.dDoc_date),  , GetLocalResourceObject("tcdDateDocToolTip"),  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301, 8))


Response.Write("</TD>									" & vbCrLf)
Response.Write("		</TR>								" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"">")


Response.Write(mobjValues.CheckControl("chkPostCheque", GetLocalResourceObject("chkPostChequeCaption"), CStr(lobjCash_mov.nMov_type - 9),  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301, 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>	" & vbCrLf)
Response.Write("			<TD><LABEL ID=""0"">" & GetLocalResourceObject("tctBeneficiaryCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">")


Response.Write(mobjValues.ClientControl("tctBeneficiary", lobjCash_mov.sClient,  , GetLocalResourceObject("tctBeneficiaryToolTip"),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301, CStr(True),  ,  ,  ,  ,  , 10, True))


Response.Write("</TD>			" & vbCrLf)
Response.Write("		</TR>								" & vbCrLf)
Response.Write("		<TR>	" & vbCrLf)
Response.Write("			<TD><LABEL ID=""0"">" & GetLocalResourceObject("valConceptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			    ")

	
	With mobjValues.Parameters
		.Add("nCompany", lobjCash_mov.nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("			" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("valConcept", "tabconceptscash", eFunctions.Values.eValuesType.clngWindowType, CStr(lobjCash_mov.nConcept), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valConceptToolTip"),  , 11))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnCashNumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCashNum", 5, CStr(lobjCash_mov.nCashnum),  , GetLocalResourceObject("tcnCashNumToolTip"),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301, 12))


Response.Write("</TD>			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>	" & vbCrLf)
Response.Write("			<TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnBordereauxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnBordereaux", 10, CStr(lobjCash_mov.nBordereaux),  , GetLocalResourceObject("tcnBordereauxToolTip"),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301, 13))


Response.Write("</TD>			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		")

	If Session("nMoveType") = 3 Then
Response.Write("		" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD WIDTH=""50%"" COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Reemplazo"">" & GetLocalResourceObject("AnchorReemplazoCaption") & "</A></LABEL></TD>            " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=""4""><HR></TD>			" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("			<TR>         			" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(1, "optTypeReplace", GetLocalResourceObject("optTypeReplace_CStr1Caption"), CStr(1), CStr(1), "ChangeOptTypeReplace(this.value);",  , 14))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=""0"">" & GetLocalResourceObject("cbeBankReplaceCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("cbeBankReplace", "Table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBankReplaceToolTip"),  , 16))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("			<TR>         						" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(2, "optTypeReplace", GetLocalResourceObject("optTypeReplace_CStr2Caption"),  , CStr(2), "ChangeOptTypeReplace(this.value);",  , 15))


Response.Write("</TD>						" & vbCrLf)
Response.Write("				<TD><LABEL ID=""0"">" & GetLocalResourceObject("tctChequeNumReplaceCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.TextControl("tctChequeNumReplace", 10, "",  , GetLocalResourceObject("tctChequeNumReplaceToolTip"),  ,  ,  ,  ,  , 17))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>								" & vbCrLf)
Response.Write("			<TR>         						" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">&nbsp</TD>						" & vbCrLf)
Response.Write("				<TD><LABEL ID=""0"">" & GetLocalResourceObject("cbeChequeLocatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("cbeChequeLocat", "Table5553", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeChequeLocatToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>								" & vbCrLf)
Response.Write("		")

	End If
Response.Write("" & vbCrLf)
Response.Write("		")

	If Session("nMoveType") = 4 Then
Response.Write("		" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD WIDTH=""50%"" COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Prórroga"">" & GetLocalResourceObject("AnchorPrórrogaCaption") & "</A></LABEL></TD>            " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=""4""><HR></TD>			" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("			<TR>         			" & vbCrLf)
Response.Write("				<TD><LABEL ID=""0"">" & GetLocalResourceObject("cbeReasonCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("cbeReason", "Table5577", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeReasonToolTip"),  , 18))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=""0"">" & GetLocalResourceObject("tcdDateProCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdDatePro",  ,  , GetLocalResourceObject("tcdDateProToolTip"),  ,  ,  , "insShowIntAmount(this)",  , 19))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("			<TR>         			" & vbCrLf)
Response.Write("				<TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnIntAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnIntAmount", 18, CStr(0),  , GetLocalResourceObject("tcnIntAmountToolTip"), True, 6,  ,  ,  ,  , True, 20))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("		")

	End If
Response.Write("" & vbCrLf)
Response.Write("		")

	If Session("nMoveType") = 2 Then
Response.Write("		" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD WIDTH=""50%"" COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Devolución"">" & GetLocalResourceObject("AnchorDevoluciónCaption") & "</A></LABEL></TD>            " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=""4""><HR></TD>			" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("			<TR>         			" & vbCrLf)
Response.Write("				<TD><LABEL ID=""0"">" & GetLocalResourceObject("cbeReasonCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("cbeReason", "Table5577", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeReasonToolTip"),  , 18))


Response.Write("</TD>				" & vbCrLf)
Response.Write("			</TR>				" & vbCrLf)
Response.Write("		")

	End If
Response.Write("" & vbCrLf)
Response.Write("	</TABLE>	")

	
	Response.Write("<SCRIPT>insSetState()</" & "Script>")
	lobjCash_mov = Nothing
End Sub

'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		If mobjValues.StringToType(Request.QueryString.Item("nTypeDocu"), eFunctions.Values.eTypeData.etdDouble) = 10 Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctChequeColumnCaption"), "tctCheque", 10, vbNullString,  , GetLocalResourceObject("tctChequeColumnToolTip"))
		Else
			Call .AddTextColumn(0, GetLocalResourceObject("tctChequeColumnCaption"), "tctCheque", 10, vbNullString,  , GetLocalResourceObject("tctChequeColumnToolTip"))
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valBankColumnCaption"), "valBank", "Table7", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valBankColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valChequeLocatColumnCaption"), "valChequeLocat", "Table5553", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valChequeLocatColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnVoucherColumnCaption"), "tcnVoucher", 10, vbNullString,  , GetLocalResourceObject("tcnVoucherColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCard_typeColumnCaption"), "valCard_type", "Table183", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCard_typeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDoc_dateColumnCaption"), "tcdDoc_date",  ,  , GetLocalResourceObject("tcdDoc_dateColumnToolTip"))
		Call .AddHiddenColumn("hddSel", "2")
		Call .AddHiddenColumn("hddBordereaux", vbNullString)
		Call .AddHiddenColumn("hddDoc_date", vbNullString)
		Call .AddHiddenColumn("hddAmount", vbNullString)
		Call .AddHiddenColumn("hddCheque", vbNullString)
		Call .AddHiddenColumn("hddTransac", vbNullString)
		Call .AddHiddenColumn("hddOffice", vbNullString)
		Call .AddHiddenColumn("hddCashnum", vbNullString)
		Call .AddHiddenColumn("hddChequeLocat", vbNullString)
		Call .AddHiddenColumn("hddBank", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "OP752"
		.AddButton = False
		.DeleteButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% InsPreOP752Grid: se realiza el manejo de los campos si el tipo de movimiento es 
'%				depósito/redepósito
'--------------------------------------------------------------------------------------------
Private Sub InsPreOP752Grid()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolCash_mov As eCashBank.Cash_movs
	Dim lclsCash_mov As Object
	lcolCash_mov = New eCashBank.Cash_movs
	
	If lcolCash_mov.FindDepositOP752(mobjValues.StringToType(Session("nMoveType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTypeDocu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nChequeLocat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nBank_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintIndex = 0
		For	Each lclsCash_mov In lcolCash_mov
			With mobjGrid
				.Columns("tctCheque").DefValue = lclsCash_mov.sDocnumbe
				.Columns("valBank").DefValue = lclsCash_mov.nBank_Code
				.Columns("hddBank").DefValue = lclsCash_mov.nBank_Code
				.Columns("valChequeLocat").DefValue = lclsCash_mov.nChequeLocat
				.Columns("hddChequeLocat").DefValue = lclsCash_mov.nChequeLocat
				.Columns("tcnVoucher").DefValue = lclsCash_mov.nVoucher
				.Columns("valCard_type").DefValue = lclsCash_mov.nCard_typ
				.Columns("tcnAmount").DefValue = lclsCash_mov.nAmount
				.Columns("tcdDoc_date").DefValue = lclsCash_mov.dDoc_date
				.Columns("hddDoc_date").DefValue = lclsCash_mov.dDoc_date
				.Columns("hddBordereaux").DefValue = lclsCash_mov.nBordereaux
				.Columns("hddAmount").DefValue = lclsCash_mov.nAmount
				.Columns("hddCheque").DefValue = lclsCash_mov.sDocnumbe
				.Columns("hddTransac").DefValue = lclsCash_mov.nTransac
				.Columns("hddOffice").DefValue = lclsCash_mov.nOffice
				.Columns("hddCashnum").DefValue = lclsCash_mov.nCashnum
				.Columns("Sel").OnClick = "inscalTotal(this, " & lintIndex & ")"
				
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			End With
		Next lclsCash_mov
	End If
	Response.Write(mobjGrid.closeTable)
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=20%><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=5%><LABEL ID=" & GetLocalResourceObject("Anchor2Caption") & " CLASS=""FIELD"" TITLE=""Número de cheques seleccionados para depositar/redepositar""><DIV ID='SelNumber'>" & GetLocalResourceObject("Anchor2Caption") & "</DIV></LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=1%><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "<" & GetLocalResourceObject("Anchor3Caption") & "LABEL></TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0 CLASS=""FIELD"" TITLE=""Total de cheques seleccionados para depositar/redepositar""><DIV ID='TotalAmount'>" & GetLocalResourceObject("Anchor4Caption") & "</DIV></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lcolCash_mov = Nothing
	lclsCash_mov = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "OP752"

mstrQueryString = "&nTypeDocu=" & Request.QueryString.Item("nTypeDocu") & "&sDep_number=" & Request.QueryString.Item("sDep_number") & "&dExpirdat=" & Request.QueryString.Item("dExpirdat") & "&nChequeLocat=" & Request.QueryString.Item("nChequeLocat") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&nBank_code=" & Request.QueryString.Item("nBank_code") & "&nBankAccount=" & Request.QueryString.Item("nBankAccount")
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 7/04/04 17:09 $"

//- Variables para manejar los totales que se muestran en la página
	var mlngSelCount = 0;
	var mlngAmount = 0;	

//% insDefValues: función que hace la llamada a la Página ShowDefValues
//-----------------------------------------------------------------------------------------
function insDefValues(sKey,sParameters,sPath){
//-------------------------------------------------------------------------------------------
    if (typeof(top)!='undefined')
        if (typeof(top.frames)!='undefined')
            if (typeof(top.frames["fraGeneric"])!='undefined'){
                sPath = (typeof(sPath)=='undefined'?'':sPath + '/')
                sParameters = (typeof(sParameters)=='undefined'?'':'&' + sParameters)
                top.frames["fraGeneric"].location.href = sPath + 'ShowDefValues.aspx?Field=' + sKey  + sParameters;
            }
}

//%	inscalTotal: se calcula el total de elementos seleccionados
//-------------------------------------------------------------------------------------------
function inscalTotal(Field, nIndex){
//-------------------------------------------------------------------------------------------
	if(Field.checked){
		mlngSelCount++;
		mlngAmount += insConvertNumber(marrArray[nIndex].tcnAmount);
		if(marrArray.length==1)
			self.document.forms[0].hddSel.value = 1;
		else
			self.document.forms[0].hddSel[nIndex].value = 1;
	}
	else{
		mlngSelCount--;
		mlngAmount -= insConvertNumber(marrArray[nIndex].tcnAmount);
		if(marrArray.length==1)
			self.document.forms[0].hddSel.value = 2;
		else
			self.document.forms[0].hddSel[nIndex].value = 2;
	}
	UpdateDiv('SelNumber', mlngSelCount)
	UpdateDiv('TotalAmount', VTFormat(mlngAmount, '', '', '', 2, true))
}

//%	insShowIntAmount: Cuando se cambia  el monto origen
//-------------------------------------------------------------------------------------------
function insShowIntAmount(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (tcnAmount.value!="" && tcdDateDoc.value!="" && tcdDatePro.value!="")
		    insDefValues('IntAmount','nAmount=' + tcnAmount.value + '&dDatePro=' + Field.value + '&dDateDoc=' + tcdDateDoc.value,'/VTimeNet/CashBank/CashBank'); 		    
    }		

}

//% ChangeCompany: Actualiza el parámetro de el Concepto cuando cambia la compañia
//-------------------------------------------------------------------------------------------------
function ChangeCompany(value){
//-------------------------------------------------------------------------------------------------
	with (document.forms[0]){
		valConcept.Parameters.Param1.sValue=value;
		if (value!=0 && value!=""){		
			valConcept.disabled = valCompany.disabled;
			btnvalConcept.disabled = valCompany.disabled;
		}
		else{
			valConcept.value="";
			$(valConcept).change();
			valConcept.disabled = true;
			btnvalConcept.disabled = true;		
		}				
	}
}

//% ChangeOptTypeReplace: Activa o desactiva el campo banco dependiendo de la opción
//%                       seleccionada en Tipo de Reemplazo
//-------------------------------------------------------------------------------------------------
function ChangeOptTypeReplace(value){
//-------------------------------------------------------------------------------------------------
	with (document.forms[0]){
		if (value == 2){		
			cbeBankReplace.value = 0;
			tctChequeNumReplace.value = "";
			cbeBankReplace.disabled = true;
			tctChequeNumReplace.disabled = true;
			cbeChequeLocat.disabled = true;
			cbeChequeLocat.value = 0;
			}
		else{
		    cbeBankReplace.disabled = false;
			tctChequeNumReplace.disabled = false;
			cbeChequeLocat.disabled = false;
			}
		}
}


//% insSetState: Establece el estado inicial de la página
//-------------------------------------------------------------------------------------------
function insSetState(){
//-------------------------------------------------------------------------------------------
    if ('<%=Session("nMoveType")%>' == '1')
        self.document.forms[0].tcnBordereaux.disabled = true;
    else
       self.document.forms[0].tcnBordereaux.disabled = false;
}

//% insEnabledFields: Inhabilita los campos de la ventana
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
	return true;	
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("OP752"))
	.Write(mobjMenu.setZone(2, "OP752", "OP752.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmOP752" ACTION="ValCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction") & mstrQueryString%>">
<%
Response.Write(mobjValues.ShowWindowsName("OP752"))

If Session("nMoveType") = 7 Or Session("nMoveType") = 8 Then
	Call insDefineHeader()
	Call InsPreOP752Grid()
Else
	Call InsPreOP752()
End If
%>
</FORM>
</BODY>
</HTML>
<%
Response.Write(mobjValues.BeginPageButton)
mobjValues = Nothing
mobjGrid = Nothing
%>




