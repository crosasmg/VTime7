<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mobjProduct_li As Object

Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

Dim mobjCertificat As ePolicy.Life
Dim mobjCertificat2 As ePolicy.Life
'- String que envia a control de cliente llave de busca de la poliza 
Dim lstrQueryString As Object

Dim mintError As Integer

Dim nprem1 As Object
Dim nprem2 As Object
Dim nprem3 As Object
Dim nprem4 As Object
Dim mclsErrors As eFunctions.Errors


'% InsDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub InsDefineHeader()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%InsDisPremdel: Valida la cantidad de registros en per_deposit para habilitar o no la prima" & vbCrLf)
Response.Write("//                pactada anual" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function InsDisPremdel(nCount, nPremdeal, nYear_end ){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var sThouSep = '")


Response.Write(mobjValues.msUserThousandSeparator)


Response.Write("';" & vbCrLf)
Response.Write("    var sDecSep  = '")


Response.Write(mobjValues.msUserDecimalSeparator)


Response.Write("';" & vbCrLf)
Response.Write("    var nvalor = 0;" & vbCrLf)
Response.Write("    var ndiv = 0;" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	if (nPremdeal == ''){" & vbCrLf)
Response.Write("		nPremdeal = 0;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	nvalor = insConvertNumber(nPremdeal)" & vbCrLf)
Response.Write("	with (self.document.forms[0]){" & vbCrLf)
Response.Write("        if (nCount ==0){" & vbCrLf)
Response.Write("            InsChangePremdeal(tcnPremdeal.value);" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("        else if (nCount == 1){" & vbCrLf)
Response.Write("            tcnPremdeal.value = VTFormat(nPremdeal, '', '', '', tcnPremdeal.DecimalPlace);" & vbCrLf)
Response.Write("            //InsChangePremdeal(tcnPremdeal.value);" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("        else if (nCount == 2){" & vbCrLf)
Response.Write("            tcnPremiumProy.value = VTFormat(nPremdeal, '', '', '', tcnPremiumProy.DecimalPlace);" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("        else if (nCount == 3){" & vbCrLf)
Response.Write("            tcnPremiumExc.value = VTFormat(nPremdeal, '', '', '', tcnPremiumExc.DecimalPlace);" & vbCrLf)
Response.Write("                }" & vbCrLf)
Response.Write("        else if (nCount == 4){" & vbCrLf)
Response.Write("            tcnValue.value = VTFormat(nPremdeal, '', '', '', tcnValue.DecimalPlace);" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("        else if (nCount == 5){" & vbCrLf)
Response.Write("            tcnValueIni.value = VTFormat(nPremdeal, '', '', '', tcnValueIni.DecimalPlace);" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("        else if (nCount == 6){" & vbCrLf)
Response.Write("            tcnIntwarr2.value = VTFormat(nPremdeal, '', '', '', tcnIntwarr2.DecimalPlace);" & vbCrLf)
Response.Write("        }        " & vbCrLf)
Response.Write("        else if (nCount == 7){" & vbCrLf)
Response.Write("            tcnIntwarr4.value = VTFormat(nPremdeal, '', '', '', tcnIntwarr4.DecimalPlace);" & vbCrLf)
Response.Write("        }        " & vbCrLf)
Response.Write("        else if (nCount == 8){" & vbCrLf)
Response.Write("            tcnAmountdep.value = VTFormat(nPremdeal, '', '', '', tcnAmountdep.DecimalPlace);" & vbCrLf)
Response.Write("            lstrquery=""nType=""+ nCount + ""&nValueFreq="" + nPremdeal;" & vbCrLf)
Response.Write("            insDefValues('PremFreq',lstrquery,'/VTimeNet/Policy/PolicySeq');         " & vbCrLf)
Response.Write("        }        " & vbCrLf)
Response.Write("        else if (nCount == 9){" & vbCrLf)
Response.Write("            tcnAmountdep1.value = VTFormat(nPremdeal, '', '', '', tcnAmountdep1.DecimalPlace);" & vbCrLf)
Response.Write("            lstrquery=""nType=""+ nCount + ""&nValueFreq="" + nPremdeal;            " & vbCrLf)
Response.Write("            insDefValues('PremFreq',lstrquery,'/VTimeNet/Policy/PolicySeq');                     " & vbCrLf)
Response.Write("        }        " & vbCrLf)
Response.Write("        else if (nCount == 10){" & vbCrLf)
Response.Write("            tcnAmountdep2.value = VTFormat(nPremdeal, '', '', '', tcnAmountdep2.DecimalPlace);" & vbCrLf)
Response.Write("            lstrquery=""nType=""+ nCount + ""&nValueFreq="" + nPremdeal;            " & vbCrLf)
Response.Write("            insDefValues('PremFreq',lstrquery,'/VTimeNet/Policy/PolicySeq');                     " & vbCrLf)
Response.Write("        }        " & vbCrLf)
Response.Write("        else if (nCount == 11){" & vbCrLf)
Response.Write("            tcnAmountdep3.value = VTFormat(nPremdeal, '', '', '', tcnAmountdep3.DecimalPlace);" & vbCrLf)
Response.Write("            lstrquery=""nType=""+ nCount + ""&nValueFreq="" + nPremdeal;            " & vbCrLf)
Response.Write("            insDefValues('PremFreq',lstrquery,'/VTimeNet/Policy/PolicySeq');                     " & vbCrLf)
Response.Write("        }        " & vbCrLf)
Response.Write("        else if (nCount == 12){" & vbCrLf)
Response.Write("            tcnAmountdep_aux.value = VTFormat(nPremdeal, '', '', '', tcnAmountdep_aux.DecimalPlace);" & vbCrLf)
Response.Write("        }                " & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnYear_iniColumnCaption"), "tcnYear_ini", 2, vbNullString,  , GetLocalResourceObject("tcnYear_iniColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnYear_endColumnCaption"), "tcnYear_end", 2, vbNullString,  , GetLocalResourceObject("tcnYear_endColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdepColumnCaption"), "tcnAmountdep", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdepColumnToolTip"), True, 2,  ,  , "InsDisPremdel(8, this.value, '')")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdep1ColumnCaption"), "tcnAmountdep1", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdep1ColumnToolTip"), True, 2,  ,  , "InsDisPremdel(9, this.value, '')")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdep2ColumnCaption"), "tcnAmountdep2", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdep2ColumnToolTip"), True, 2,  ,  , "InsDisPremdel(10, this.value, '')")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdep3ColumnCaption"), "tcnAmountdep3", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdep3ColumnToolTip"), True, 2,  ,  , "InsDisPremdel(11, this.value, '')")
		.AddNumericColumn(0, " ", "tcnAmountdep_aux", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdep_auxColumnToolTip"), True, 2,  ,  , "InsDisPremdel(12, this.value, '')")
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI7006"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnYear_ini").EditRecord = True
		.Columns("tcnYear_end").EditRecord = True
		'.AddButton = not mintError <> eRemoteDB.Constants.intNull
		.Height = 300
		.Width = 380
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nYear_ini=' + marrArray[lintIndex].tcnYear_ini +  '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
	End With
	
	
End Sub


'% InsPreVI1410: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVI7006()
	'--------------------------------------------------------------------------------------------
	'- Objetos para el manejo de los datos repetitivos de la página
	Dim lcolPer_deposit As ePolicy.Per_deposits
	Dim lclsPer_deposit As Object
	Dim ldblPrem_deal As Double
	Dim llngYear_end As Object
	
	lcolPer_deposit = New ePolicy.Per_deposits
	If lcolPer_deposit.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
		For	Each lclsPer_deposit In lcolPer_deposit
			With mobjGrid
				.Columns("tcnYear_ini").DefValue = lclsPer_deposit.nYear_ini
				ldblPrem_deal = lclsPer_deposit.nAmountdep
				llngYear_end = lclsPer_deposit.nYear_end
				.Columns("tcnYear_end").DefValue = llngYear_end
				.Columns("tcnAmountdep").DefValue = CStr(ldblPrem_deal)
				.Columns("tcnAmountdep1").DefValue = CStr(ldblPrem_deal / 2)
				.Columns("tcnAmountdep2").DefValue = CStr(ldblPrem_deal / 4)
				.Columns("tcnAmountdep3").DefValue = CStr(ldblPrem_deal / 12)
				.Columns("tcnAmountdep_aux").DefValue = lclsPer_deposit.nAmountdep_aux
				
				Response.Write(.DoRow)
			End With
		Next lclsPer_deposit
	End If
	Response.Write(mobjGrid.closeTable())
	lcolPer_deposit = Nothing
	lclsPer_deposit = Nothing
	
End Sub
'% InsPreVI7006Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVI7006Upd()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% UpdateFields: actualiza los campos ocultos con los campos puntuales de la BC001J" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function UpdateFields(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    with(self.document.forms[0]){" & vbCrLf)
Response.Write("        hddnPremdeal.value = top.opener.document.forms[0].tcnPremdeal.value;" & vbCrLf)
Response.Write("        hddnVPprdeal.value = top.opener.document.forms[0].hddnVPprdeal.value;" & vbCrLf)
Response.Write("        hddnPremimin.value = top.opener.document.forms[0].hddnPremimin.value;" & vbCrLf)
Response.Write("        hddnCurrency.value = top.opener.document.forms[0].hddnCurrency.value;" & vbCrLf)
Response.Write("        hddnPremdep.value = top.opener.document.forms[0].hddnPremdep.value;" & vbCrLf)
Response.Write("        hddnIntwarr.value = top.opener.document.forms[0].hddnIntwarr.value;" & vbCrLf)
Response.Write("        hddnRatepayf.value = top.opener.document.forms[0].hddnRatepayf.value;" & vbCrLf)
Response.Write("        hddPremdeal_anu.value = top.opener.document.forms[0].hddPremdeal_anu.value;" & vbCrLf)
Response.Write("        hddnPremfreq.value = top.opener.document.forms[0].hddnPremfreq.value;" & vbCrLf)
Response.Write("        hddBirthdate.value = top.opener.document.forms[0].hddBirthdate.value;" & vbCrLf)
Response.Write("		hddEffecdate_to.value = top.opener.document.forms[0].hddEffecdate_to.value;" & vbCrLf)
Response.Write("		hddVp_initial.value = top.opener.document.forms[0].hddVp_initial.value;" & vbCrLf)
Response.Write("		hddsPremdeal_Chan.value = top.opener.document.forms[0].hddsPremdeal_Chan.value;" & vbCrLf)
Response.Write("		hddsProcessed.value = top.opener.document.forms[0].hddsProcessed.value;" & vbCrLf)
Response.Write("		hddnPremdeal_old.value = top.opener.document.forms[0].hddnPremdeal_old.value;" & vbCrLf)
Response.Write("		hddnPremiumbas.value = top.opener.document.forms[0].hddnPremiumbas.value;" & vbCrLf)
Response.Write("		hddnOption.value = top.opener.document.forms[0].hddnOption.value;" & vbCrLf)
Response.Write("		hddsOption.value = top.opener.document.forms[0].hddsOption.value;" & vbCrLf)
Response.Write("		hddsPayfreq.value = top.opener.document.forms[0].hddsPayfreq.value;" & vbCrLf)
Response.Write("    }													 " & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lclsPer_deposit As ePolicy.Per_deposit
	
	lclsPer_deposit = New ePolicy.Per_deposit
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsPer_deposit.InsPostVA595Upd(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nYear_ini"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), 0, 0, Session("dNulldate"), Session("nUsercode"), Session("nTransaction"), "VI1410") Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "VI7006", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	With Response
		.Write(mobjValues.HiddenControl("hddnVPprdeal", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremimin", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremdep", vbNullString))
		.Write(mobjValues.HiddenControl("hddnIntwarr", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremdeal", vbNullString))
		.Write(mobjValues.HiddenControl("hddnRatepayf", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremfreq", vbNullString))
		.Write(mobjValues.HiddenControl("hddPremdeal_anu", vbNullString))
		.Write(mobjValues.HiddenControl("hddBirthdate", vbNullString))
		.Write(mobjValues.HiddenControl("hddEffecdate_to", vbNullString))
		.Write(mobjValues.HiddenControl("hddVp_initial", vbNullString))
		.Write(mobjValues.HiddenControl("hddsPremdeal_Chan", vbNullString))
		.Write(mobjValues.HiddenControl("hddsProcessed", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremdeal_old", vbNullString))
		.Write(mobjValues.HiddenControl("hddnCurrency", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremiumbas", vbNullString))
		.Write(mobjValues.HiddenControl("hddnOption", vbNullString))
		.Write(mobjValues.HiddenControl("hddsOption", vbNullString))
		.Write(mobjValues.HiddenControl("hddsPayfreq", vbNullString))
		
		.Write("<SCRIPT>UpdateFields()</" & "Script>")
	End With
	lclsPer_deposit = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
%>   



	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    
<SCRIPT LANGUAGE=javascript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 10-05-06 12:23 $|$$Author: Clobos $"

//+ Cantidad de cuotas 
    var lintcbeQuota


// ChangeSubmit: Cambia la accion de la forma
//-------------------------------------------------------------------------------------------
function ChangeSubmit(Option, Holder) {
//-------------------------------------------------------------------------------------------	
	switch (Option) {
		case "Add":
			document.forms[0].action = "valPolicySeq.aspx?&nMainAction=301&nHolder=" + Holder
	}
}
	
//%InsShowIlustration: Muestra la ventana ValuePolIlustration, para mostrar la Ilustración
//------------------------------------------------------------------------------------------------
function InsShowIlustration(nError){
//------------------------------------------------------------------------------------------------
    var lstrQueryString
    var lstrQuery
    lstrQuery = '<%=Session("bQuery")%>'
	lstrQuery = lstrQuery.toLowerCase()

	if (nError<=0){
		with (self.document.forms[0]){
			lstrQueryString = '&sCertype=<%=Session("sCertype")%>' + '&nBranch=<%=Session("nBranch")%>' + 
			                  '&nProduct=<%=Session("nProduct")%>' + '&nPolicy=<%=Session("nPolicy")%>' + 
			                  '&nCertif=<%=Session("nCertif")%>'   + '&dEffecdate=<%=Session("dEffecdate")%>' + 
			                  '&nVp_initial=' + hddVp_initial.value + 
			                  '&dBirthdate=' + hddBirthdate.value + 
			                  '&dEffecdate_to=' + hddEffecdate_to.value +
			                  '&nOption='	+	hddnOption.value +
			                  '&sOption='	+	hddsOption.value +  
			                  '&bQuery='+ lstrQuery;
			if (lstrQuery=='true' || lstrQuery=='verdadero'){
				lstrQueryString = lstrQueryString +
								  '&nPremdeal_anu=' + hddPremdeal_anu.value +
				                  '&nPremfreq=' + hddPremdeal_anu.value/12 +
				                  '&nIntwarr=' + tcnIntwarr.value+
				                  '&nIntwarrsav=' + tcnIntwarr3.value+
				                  '&nIntwarr2=' + tcnIntwarr2.value+
				                  '&nIntwarrsav2=' + tcnIntwarr4.value+				                  				                  				                  
                                  '&nPeriod=' + tcnPeriod.value;

			}
			else{
				lstrQueryString = lstrQueryString +
								  '&nPremdeal_anu=' + tcnPremdeal.value +  
				                  '&nPremfreq=' + tcnPremdeal.value/12 +
				                  '&nIntwarr=' + tcnIntwarr.value +
				                  '&nIntwarrsav=' + tcnIntwarr3.value+
				                  '&nIntwarr2=' + tcnIntwarr2.value+
				                  '&nIntwarrsav2=' + tcnIntwarr4.value+				                  				                  				                  				                  
				                  '&nPeriod=' + tcnPeriod.value;
			}
		}
		ShowPopUp("../../Common/ShowIlustrationVul.aspx?sCodispl=VI1410" + lstrQueryString, "ValuePolIlustration", 750, 500, 'yes', 'yes', 10, 10) 
	}
} 
//------------------------------------------------------------------------------------------------
	
function InsShowResult(nError){ 
//------------------------------------------------------------------------------------------------


    
    lstrquery='nType='+ nError + '&nIntwarr='+ self.document.forms[0].tcnIntwarr.value;
    lstrquery= lstrquery + '&nIntwarrsav=' +  self.document.forms[0].tcnIntwarr3.value;
    lstrquery= lstrquery + '&nValueCon=' +  self.document.forms[0].tcnPremiumProy.value; 
    lstrquery= lstrquery + '&nValueEx='+  self.document.forms[0].tcnPremiumExc.value;
    lstrquery= lstrquery + '&nValuePol=' +  self.document.forms[0].tcnValue.value;
    lstrquery= lstrquery + '&nAge='+ self.document.forms[0].tcnAge.value;
    lstrquery= lstrquery + '&nAmountini='+ self.document.forms[0].tcnValueIni.value;
    lstrquery= lstrquery + '&nAgePay='+ self.document.forms[0].tcnDurPay.value ;          
    insDefValues('Simula',lstrquery,'/VTimeNet/Policy/PolicySeq');   
} 	

</SCRIPT>

 
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<%

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	mintError = 0
	
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI7006" ACTION = "valPolicySeq.aspx?nMainAction=301&amp;nHolder=1">

<%If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjCertificat = New ePolicy.Life
	
	mobjCertificat2 = New ePolicy.Life
	
	mobjValues.ActionQuery = Session("bQuery")
	
	Call mobjCertificat.InsPreVI7006(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), Session("nTransaction"))
	
	Call mobjCertificat2.insPreVI1410(Request.QueryString.Item("ReloadAction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), Session("nTransaction"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremiumbas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremimin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVpprdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nAmountcontr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nIntwarr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRatepayf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nInsurtime"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVpi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dBirthdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nOption"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sOption"), Request.QueryString.Item("sPayfreq"))
	mintError = mobjCertificat2.nError
	If mintError <> eRemoteDB.Constants.intNull Then
		mclsErrors = New eFunctions.Errors
		Response.Write(mclsErrors.ErrorMessage("VI1410", mintError,  ,  , CStr(mobjCertificat2.nPremDeal_anu), True))
		mclsErrors = Nothing
	End If
	
	
	With mobjCertificat2
		
		
		Response.Write(mobjValues.HiddenControl("hddnVPprdeal", CStr(.nVpprdeal)))
		Response.Write(mobjValues.HiddenControl("hddnPremimin", CStr(.nPremmin)))
		Response.Write(mobjValues.HiddenControl("hddnPremdep", CStr(.nAmountcontr)))
		Response.Write(mobjValues.HiddenControl("hddnCurrency", CStr(.nCurrency)))
		Response.Write(mobjValues.HiddenControl("hddnIntwarr", CStr(.nIntwarr)))
		Response.Write(mobjValues.HiddenControl("hddnRatepayf", CStr(.nRatepayf)))
		Response.Write(mobjValues.HiddenControl("hddnInsurtime", CStr(.nInsur_time)))
		Response.Write(mobjValues.HiddenControl("hddnPremfreq", ""))
		Response.Write(mobjValues.HiddenControl("hddPremdeal_anu", CStr(.nPremDeal_anu)))
		Response.Write(mobjValues.HiddenControl("hddBirthdate", mobjValues.TypeToString(.dBirthdate, eFunctions.Values.eTypeData.etdDate)))
		Response.Write(mobjValues.HiddenControl("hddEffecdate_to", mobjValues.TypeToString(.dEffecdate_to, eFunctions.Values.eTypeData.etdDate)))
		Response.Write(mobjValues.HiddenControl("hddVp_initial", mobjValues.TypeToString(.nVpi, eFunctions.Values.eTypeData.etdDouble)))
		Response.Write(mobjValues.HiddenControl("hddnYear_end", CStr(eRemoteDB.Constants.intNull)))
		Response.Write(mobjValues.HiddenControl("hddsPremdeal_Chan", ""))
		Response.Write(mobjValues.HiddenControl("hddsProcessed", ""))
		Response.Write(mobjValues.HiddenControl("hddnPremdeal_old", CStr(.nPremDeal_anu)))
		Response.Write(mobjValues.HiddenControl("hddnPremiumbas", CStr(.nPremiumBas)))
		Response.Write(mobjValues.HiddenControl("hddnOption", CStr(.nOption)))
		Response.Write(mobjValues.HiddenControl("hddsOption", .sOption))
		Response.Write(mobjValues.HiddenControl("hddsPayfreq", .sPayfreq))
	End With
	%>
    <%	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
        <TABLE WIDTH="100%"><BR>			
		<TR>
	        <TD><LABEL><%= GetLocalResourceObject("tcnPremium1Caption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremium1", 18, CStr(mobjCertificat.nPremiumBas),  , GetLocalResourceObject("tcnPremium1ToolTip"), True, 2,  ,  ,  ,  , True)%></TD></TD>
	        <TD><LABEL><%= GetLocalResourceObject("tcnPremdealCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremdeal", 18, CStr(mobjCertificat.nPremDeal_anu),  , GetLocalResourceObject("tcnPremdealToolTip"), True, 2,  ,  ,  , "InsDisPremdel(1, this.value, '')", False)%></TD></TD>			
		</TR>		
		</TABLE>
       <TABLE WIDTH="100%">	<BR>	
        <TR>
     	    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios"><%= GetLocalResourceObject("AnchorVariosCaption") %></A></LABEL></TD>
     	    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios"></A></LABEL></TD>     	    
     	    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios"><%= GetLocalResourceObject("AnchorVarios2Caption") %></A></LABEL></TD>     	    
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
			<TD COLSPAN="2"></TD>			
			<TD COLSPAN="2" CLASS="Horline"></TD>			
			 			
		</TR>		
		<TR>
	        <TD><LABEL><%= GetLocalResourceObject("tcnIntwarrCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnIntwarr", 10, CStr(mobjCertificat.nIntwarr),  , GetLocalResourceObject("tcnIntwarrToolTip"), True, 2,  ,  ,  ,  , True)%></TD></TD>
	        <TD><LABEL></LABEL></TD>
	        <TD><LABEL></LABEL></TD>	        			
	        <TD><LABEL><%= GetLocalResourceObject("tcnIntwarrCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnIntwarr2", 10, CStr(mobjCertificat.nIntwarrexc),  , GetLocalResourceObject("tcnIntwarr2ToolTip"), True, 2,  ,  ,  , "InsDisPremdel(6, this.value, '')", True)%></TD></TD>			
		</TR>		
     	<TR>
	        <TD><LABEL><%= GetLocalResourceObject("tcnIntwarr3Caption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnIntwarr3", 10, CStr(mobjCertificat.nIntwarrVar),  , GetLocalResourceObject("tcnIntwarr3ToolTip"), True, 2,  ,  ,  ,  , True)%></TD></TD>
	        <TD><LABEL></LABEL>&nbsp;&nbsp</TD>
	        <TD><LABEL></LABEL>&nbsp;&nbsp;</TD>			
	        <TD><LABEL><%= GetLocalResourceObject("tcnIntwarr3Caption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnIntwarr4", 10, CStr(mobjCertificat.nIntwarrExcVar),  , GetLocalResourceObject("tcnIntwarr4ToolTip"), True, 2,  ,  ,  , "InsDisPremdel(7, this.value, '')", True)%></TD></TD>			
		</TR>		
         <TR>
	        <TD><LABEL><%= GetLocalResourceObject("tcnIntwarr5Caption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnIntwarr5", 10, CStr(mobjCertificat.nIntwarrMin),  , GetLocalResourceObject("tcnIntwarr5ToolTip"), True, 2,  ,  ,  ,  , True)%></TD></TD>
	        <TD><LABEL></LABEL>&nbsp;&nbsp</TD>
	        <TD><LABEL></LABEL>&nbsp;&nbsp;</TD>			
	        <TD><LABEL></LABEL>&nbsp;&nbsp</TD>
	        <TD><LABEL></LABEL>&nbsp;&nbsp;</TD>			
		</TR>				
    </TABLE> 	
    <TABLE WIDTH="100%">				
		
    	    <TD COLSPAN="6" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios"><%= GetLocalResourceObject("AnchorVarios3Caption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="6" CLASS="Horline"></TD>
			 			
		</TR>
		<TR>
		</TR>
		<TR>
	     <TD WIDTH="25%" ><LABEL ID=0><%= GetLocalResourceObject("btnCalcCaption") %></LABEL></TD>
         <TD><%=mobjValues.AnimatedButtonControl("btnCalc", "..\..\images\batchStat06.png", GetLocalResourceObject("btnCalcToolTip"),  , "InsShowIlustration(" & mintError & ");")%><TD>
		<TD COLSPAN="3" ><LABEL></LABEL>&nbsp;</TD>
		</TR>
		
    </TABLE>
	<%End If%>    
    <TABLE WIDTH="50%">      
<TR>		                         				                         
		             <TD><LABEL>&nbsp;</LABEL><TD>
            <TD><LABEL>&nbsp;</LABEL><TD>
<%
Call InsDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call InsPreVI7006Upd()
Else
	Call InsPreVI7006()
End If

%>
		 

</TR>

    </TABLE> 		                         		     

<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
    <TABLE WIDTH="100%">
        <TR>
	        <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD> 
		</TR>
		<TR>
			<TD COLSPAN="5" CLASS="Horline"></TD>
		</TR>        	
		
		<TR>
			<TD COLSPAN="5"><LABEL>&nbsp;</LABEL></TD>
		</TR>        	

		<TR>        	
	        <TD><LABEL><%= GetLocalResourceObject("tcnPeriodCaption") %> </LABEL></TD>

			<TD><%=mobjValues.NumericControl("tcnPeriod", 2, CStr(mobjCertificat.nFreqProy),  , GetLocalResourceObject("tcnPeriodToolTip"), True, 0)%></TD></TD>
 	        <TD COLSPAN="5" ><LABEL></LABEL>&nbsp;</TD>
 	     </TR>		
        <TR>
			<TD COLSPAN="5"><LABEL>&nbsp;</LABEL></TD>
		</TR>        	

		
		<TR>
            <TD><%=mobjValues.AnimatedButtonControl("btnCalc1", "..\..\images\A603Off.png", GetLocalResourceObject("btnCalc1ToolTip"),  , "InsShowResult(""1"");")%><TD>
	        <TD><LABEL><%= GetLocalResourceObject("tcnPremiumProyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremiumProy", 18, CStr(mobjCertificat.nPremDeal_anu),  , GetLocalResourceObject("tcnPremiumProyToolTip"), True, 2,  ,  ,  , "InsDisPremdel(2, this.value, '')")%></TD></TD>
		</TR>
		
		<TR>
            <TD><%=mobjValues.AnimatedButtonControl("btnCalc2", "..\..\images\A603Off.png", GetLocalResourceObject("btnCalc2ToolTip"),  , "InsShowResult(""2"");")%><TD>		
	        <TD><LABEL><%= GetLocalResourceObject("tcnPremiumExcCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremiumExc", 18, CStr(mobjCertificat.nPremDif),  , GetLocalResourceObject("tcnPremiumExcToolTip"), True, 2,  ,  ,  , "InsDisPremdel(3, this.value, '')")%></TD></TD>
		</TR>
		
		<TR>
            <TD><%=mobjValues.AnimatedButtonControl("btnCalc3", "..\..\images\A603Off.png", GetLocalResourceObject("btnCalc3ToolTip"),  , "InsShowResult(""3"");")%><TD>		
	        <TD><LABEL><%= GetLocalResourceObject("tcnValueCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnValue", 18,  ,  , GetLocalResourceObject("tcnValueToolTip"), True, 2,  ,  ,  , "InsDisPremdel(4, this.value, '')")%></TD></TD>
		</TR>
		
		<TR>
            <TD><%=mobjValues.AnimatedButtonControl("btnCalc4", "..\..\images\A603Off.png", GetLocalResourceObject("btnCalc4ToolTip"),  , "InsShowResult(""4"");")%><TD>		
	        <TD><LABEL><%= GetLocalResourceObject("tcnAgeCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAge", 3,  ,  , GetLocalResourceObject("tcnAgeToolTip"), True, 0)%></TD></TD>
		</TR>
		
		<TR>
            <TD><%=mobjValues.AnimatedButtonControl("btnCalc5", "..\..\images\A603Off.png", GetLocalResourceObject("btnCalc5ToolTip"),  , "InsShowResult(""5"");")%><TD>		
	        <TD><LABEL><%= GetLocalResourceObject("tcnValueIniCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnValueIni", 18,  ,  , GetLocalResourceObject("tcnValueIniToolTip"), True, 2,  ,  ,  , "InsDisPremdel(5, this.value, '')")%></TD></TD>
		</TR>

		<TR>
            <TD><%=mobjValues.AnimatedButtonControl("btnCalc6", "..\..\images\A603Off.png", GetLocalResourceObject("btnCalc6ToolTip"),  , "InsShowResult(""6"");")%><TD>		
	        <TD><LABEL><%= GetLocalResourceObject("tcnDurPayCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnDurPay", 3,  ,  , GetLocalResourceObject("tcnDurPayToolTip"), True, 0)%></TD></TD>
		</TR>

	</TABLE>
<%End If%>	


</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
mobjGrid = Nothing
mobjValues = Nothing
mobjCertificat = Nothing
mobjProduct_li = Nothing
%>





