<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim mlngCurrency As Object
Dim MintIndex As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsCertif As ePolicy.Certificat
	Dim lclsCurren_pol As ePolicy.Curren_pol
	Dim lintCurrency As Object
	
	lclsCertif = New ePolicy.Certificat
	
	
	lclsCurren_pol = New ePolicy.Curren_pol
	
	mobjGrid = New eFunctions.Grid
	
	Call lclsCertif.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
	
	Call lclsCurren_pol.Find_Currency_Sel(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("deffecdate"))
	
	If lclsCurren_pol.nCount = 1 Then
		lintCurrency = lclsCurren_pol.nCurrency
	End If
	
	If mobjValues.StringToDate(CStr(lclsCertif.dTariffDate)) = CDate("0:00:00") Or Session("nTransaction") = 12 Or Session("nTransaction") = 13 Or Session("nTransaction") = 14 Or Session("nTransaction") = 15 Or Session("nTransaction") = 24 Or Session("nTransaction") = 25 Or Session("nTransaction") = 26 Or Session("nTransaction") = 27 Then
		Session("dTariffDate") = Session("deffecdate")
	Else
		Session("dTariffDate") = lclsCertif.dStartDate
	End If
	
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboGuarSav_yearColumnCaption"), "cboGuarSav_year", "Tab_GuarSavingAll", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "Change_date(this.value);",  ,  , GetLocalResourceObject("cboGuarSav_yearColumnToolTip"))
		With mobjGrid.Columns("cboGuarSav_year").Parameters
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dStartDate", lclsCertif.dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dExpirdat", Session("dExpirdat"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		
		Call .AddDateColumn(0, GetLocalResourceObject("tcdStart_GuarSavColumnCaption"), "tcdStart_GuarSav", vbNullString,  , GetLocalResourceObject("tcdStart_GuarSavColumnToolTip"),  ,  , "insChangeField(this);", True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEnd_GuarSav_toColumnCaption"), "tcdEnd_GuarSav_to", vbNullString,  , GetLocalResourceObject("tcdEnd_GuarSav_toColumnToolTip"),  ,  , "insChangeField(this);", True)
		Call .AddCheckColumn(0, GetLocalResourceObject("chkPayColumnCaption"), "chkPay", "", 1,  , "insCheck();", Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkPayColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "TabCurren_pol", eFunctions.Values.eValuesType.clngComboType, lintCurrency, True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		With mobjGrid.Columns("cbeCurrency").Parameters
			.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGuarSav_valueColumnCaption"), "tcnGuarSav_value", 12, vbNullString,  , GetLocalResourceObject("tcnGuarSav_valueColumnToolTip"), True, 0,  ,  , "insShowVI8000(1);")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRen_guarSavColumnCaption"), "tcnRen_guarSav", 18, vbNullString,  , GetLocalResourceObject("tcnRen_guarSavColumnToolTip"), True, 6,  ,  , "insShowVI8000(2);", True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGuarSav_premColumnCaption"), "tcnGuarSav_prem", 18, vbNullString,  , GetLocalResourceObject("tcnGuarSav_premColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeGuarSav_statColumnCaption"), "cbeGuarSav_stat", "table8001", eFunctions.Values.eValuesType.clngComboType, CStr(1), False,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeGuarSav_statColumnToolTip"))
		
		.AddHiddenColumn("hddnGuarSavid", vbNullString)
		.AddHiddenColumn("hddnPremium", vbNullString)
		.AddHiddenColumn("hddnCost", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.ActionQuery = mobjValues.ActionQuery
		.Columns("cboGuarSav_year").EditRecord = True
		.Top = 100
		.Height = 400
		.Width = 500
		.UpdContent = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sEditRecordParam = "nGuarSav_year=' + (typeof(self.document.forms[0].cboGuarSav_year)!='undefined'?self.document.forms[0].cboGuarSav_year.value:'') + '"
		.sDelRecordParam = "nGuarSavid=' + marrArray[lintIndex].hddnGuarSavid + '" & "&nGuarSav_year=' + marrArray[lintIndex].cboGuarSav_year + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		If Request.QueryString.Item("Type") = "PopUp" Then
			Select Case Session("nTransaction")
				Case "12", "13", "14", "15", "24", "25", "26", "27", "34"
					.Columns("chkPay").Disabled = False
				Case Else
					.Columns("chkPay").Disabled = True
			End Select
		End If
		
	End With
End Sub

'% insPreVI8000: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI8000()
	'--------------------------------------------------------------------------------------------
	Dim lcolGuar_Saving_Pol As ePolicy.Guar_Saving_Pols
	Dim lclsGuar_Saving_Pol As Object
	Dim lintIndex As Short
	Dim lblnFound As Boolean
	Dim linGuarSav_prem As Object
	Dim linGuarsav_cost As Object
	
	lblnFound = False
	
	lcolGuar_Saving_Pol = New ePolicy.Guar_Saving_Pols
	lblnFound = lcolGuar_Saving_Pol.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
	
	'+ Si existe información pata procesar
	If lblnFound Then
		lintIndex = 0
		For	Each lclsGuar_Saving_Pol In lcolGuar_Saving_Pol
			With mobjGrid
				
				.Columns("cboGuarSav_year").DefValue = lclsGuar_Saving_Pol.nGuarSav_year
				.Columns("tcdStart_GuarSav").DefValue = lclsGuar_Saving_Pol.dStart_GuarSav
				.Columns("tcdEnd_GuarSav_to").DefValue = lclsGuar_Saving_Pol.dEnd_GuarSav
				.Columns("chkPay").Checked = lclsGuar_Saving_Pol.sDeppremind
				.Columns("cbeCurrency").DefValue = lclsGuar_Saving_Pol.nCurrency
				.Columns("tcnGuarSav_value").DefValue = lclsGuar_Saving_Pol.nGuarSav_value
				.Columns("tcnRen_guarSav").DefValue = lclsGuar_Saving_Pol.nRen_guarSav
				.Columns("cbeGuarSav_stat").DefValue = lclsGuar_Saving_Pol.nGuarSav_stat
				.Columns("hddnGuarSavid").DefValue = lclsGuar_Saving_Pol.nGuarSavid
				If (lclsGuar_Saving_Pol.nGuarSav_stat = 1 Or lclsGuar_Saving_Pol.nGuarSav_stat = 2) Then
					.Columns("sel").Disabled = False
					.Columns("cboGuarSav_year").EditRecord = True
				Else
					.Columns("sel").Disabled = True
					.Columns("cboGuarSav_year").HRefScript = ""
					.Columns("cboGuarSav_year").EditRecord = False
				End If
				
				If lclsGuar_Saving_Pol.nGuarsav_prem <= 0 Then
					lclsGuar_Saving_Pol.nGuarsav_prem = 0
				End If
				If lclsGuar_Saving_Pol.nGuarsav_cost <= 0 Then
					lclsGuar_Saving_Pol.nGuarsav_cost = 0
				End If
				
				.Columns("tcnGuarSav_prem").DefValue = CStr(lclsGuar_Saving_Pol.nGuarsav_prem + lclsGuar_Saving_Pol.nGuarsav_cost)
				.Columns("hddnPremium").DefValue = lclsGuar_Saving_Pol.nGuarsav_prem
				.Columns("hddnCost").DefValue = lclsGuar_Saving_Pol.nGuarsav_cost
				
				
			End With
			Response.Write(mobjGrid.DoRow())
			lintIndex = lintIndex + 1
		Next lclsGuar_Saving_Pol
	End If
	Response.Write(mobjGrid.closeTable())
	'+ Se liberan de memoria las instancias creadas de los objetos utilizados en esta ventana - ACM - 15/12/2000    
	lcolGuar_Saving_Pol = Nothing
End Sub


'% insPreVI8000Upd: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI8000Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsGuar_Saving_Pol As ePolicy.Guar_Saving_Pol
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsGuar_Saving_Pol = New ePolicy.Guar_Saving_Pol
			If lclsGuar_Saving_Pol.insPostVI8000(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nGuarSavid"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nGuarSav_year"), eFunctions.Values.eTypeData.etdDouble), Today, Today, 0, 0, 0, 0, 0, 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "", "Del") Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "VI8000", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index")), "1"))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = Session("bQuery")
mlngCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//% ReloadPage: se recarga la página cuando se cambia la moneda
//-------------------------------------------------------------------------------------------
function ReloadPage(){
//-------------------------------------------------------------------------------------------
    var nGroup='';    
    
    with(self.document.forms[0]){        
        nGroup = (typeof(valGroup)=='undefined')?"0":valGroup.value
        self.document.location.href = "VI8000.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&sCodisp=<%=Request.QueryString.Item("sCodisp")%>&nMainAction=" + nMainAction +
                                      "&sOnSeq=1&nCurrency=" + (typeof(cbeCurrency) != 'undefined'?cbeCurrency.value:'') +
                                      "&nGroup=" + nGroup
    }                                                                 
}

//------------------------------------------------------------------------------------------
function insCheck(){
//------------------------------------------------------------------------------------------
	if (document.forms[0].chkPay.checked)
	{
		document.forms[0].cbeGuarSav_stat.value ='1';
		document.forms[0].chkPay.value ='1';
	}
	else
	{
		document.forms[0].cbeGuarSav_stat.value ='2';
		document.forms[0].chkPay.value ='2';
	}
	insShowVI8000(1);
}
//-------------------------------------------------------------------------------------------
function Change_date(nValue) {
//-------------------------------------------------------------------------------------------
	var lstrquery='';
		with(self.document.forms[0]){        	
			if (typeof(nValue)!= 'undefined'){
			insDefValues('Calculate_date', "nValue=" + nValue + "&nGuarsav_value="+ tcnGuarSav_value.value + "&nGuarsav_year=" + cboGuarSav_year.value + "&nRen_guarsav=" + tcnRen_guarSav.value + "&sPay=" + chkPay.value + "&nOption=1", '/VTimeNet/Policy/PolicySeq');
		}	
	}	
}
//-------------------------------------------------------------------------------------------
function insShowVI8000(noption) {
//-------------------------------------------------------------------------------------------

	with(self.document.forms[0]){        	
		insDefValues('insShowVI8000', "nGuarsav_value="+ tcnGuarSav_value.value + "&nGuarsav_year=" + cboGuarSav_year.value + "&nRen_guarsav=" + tcnRen_guarSav.value + "&sPay=" + chkPay.value + "&nOption=" + noption , '/VTimeNet/Policy/PolicySeq');
	}
}

</SCRIPT>
<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sCodispl") & ".aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmVI8000" ACTION="ValPolicySeq.aspx?x=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVI8000Upd()
Else
	Call insPreVI8000()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>




