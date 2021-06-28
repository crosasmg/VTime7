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
Dim mlngGroup As Object
Dim mlngModulec As Object
Dim mlngCurrency As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("nCodeColumnCaption"), "nCode", 9, vbNullString,  , GetLocalResourceObject("nCodeColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddTextColumn(0, GetLocalResourceObject("tctcodeColumnCaption"), "tctcode", 30, vbNullString,  , GetLocalResourceObject("tctcodeColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(0), True, GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnCapitalsolColumnCaption"), "tcnCapitalsol", 18, CStr(0), True, GetLocalResourceObject("tcnCapitalsolColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 18, CStr(0), True, GetLocalResourceObject("tcnRateColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnRate_BColumnCaption"), "tcnRate_B", 18, CStr(0), True, GetLocalResourceObject("tcnRate_BColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0), True, GetLocalResourceObject("tcnPremiumColumnCaption"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcPremium_BColumnCaption"), "tcPremium_B", 18, CStr(0), True, GetLocalResourceObject("tcPremium_BColumnCaption"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnComssionColumnCaption"), "tcnComssion", 18, CStr(0), True, GetLocalResourceObject("tcnComssionColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CA829"
		.ActionQuery = True
		.Height = 200
		.Width = 350
		.nMainAction = 401
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCA829: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA829()
	'--------------------------------------------------------------------------------------------
	Dim lclsCover As ePolicy.Cover
	Dim lintIndex As Integer
	Dim lblnFound As Boolean
	Dim lblnModules As Boolean
	Dim lblnGroup As Boolean
	
	lclsCover = New ePolicy.Cover
	lblnFound = lclsCover.FindCa829(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mlngGroup, mlngModulec, mobjValues.StringToType(Request.QueryString.Item("Reload"), eFunctions.Values.eTypeData.etdDouble), mlngCurrency)
	
	
	mlngCurrency = lclsCover.nCurrency
	lblnModules = lclsCover.bModulec
	lblnGroup = True
	If lclsCover.sTyp_module = "3" Then
		lblnGroup = False
	End If
	Response.Write(mobjValues.HiddenControl("hddnGroup", mlngGroup))
	Response.Write(mobjValues.HiddenControl("hddnModulec", mlngModulec))
	Response.Write(mobjValues.HiddenControl("hddsTyp_module", lclsCover.sTyp_module))
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">")

	
	'+ Si las especificaciones son por grupo
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TD WIDTH=""10%""><LABEL ID=13038>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    <TD>" & vbCrLf)
Response.Write("    ")

	
	mobjValues.TypeList = 1
	mobjValues.List = lclsCover.mclsCurren_pol.Charge_Combo
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, mlngCurrency,  ,  ,  ,  ,  , "ReloadPage()", True))
	
Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD WIDTH=""25%""><LABEL ID=""13043"">" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("        ")

	
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	mobjValues.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("valGroup", "tabGroups", eFunctions.Values.eValuesType.clngComboType, mlngGroup, True,  ,  ,  ,  , "ReloadPage()", lblnGroup,  , GetLocalResourceObject("valGroupToolTip")))
	
Response.Write("</TD>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD WIDTH=""25%""><LABEL ID=""13043"">" & GetLocalResourceObject("valModulecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		")

	
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nGroup", mlngGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	mobjValues.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("valModulec", "TABTABMODUL_CO_PG", eFunctions.Values.eValuesType.clngComboType, mlngModulec, True,  ,  ,  ,  , "ReloadPage()", lclsCover.bModulec,  , GetLocalResourceObject("valModulecToolTip")))
	
Response.Write("</TD>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	
	'+ Si existe información pata procesar
	If lblnFound Then
		For lintIndex = 0 To lclsCover.CountCover
			If lclsCover.CoverItem(lintIndex) Then
				With mobjGrid
					.Columns("nCode").DefValue = CStr(lclsCover.nCover)
					.Columns("tctcode").DefValue = lclsCover.sDescript
					If lclsCover.nCapital > 0 Then
						.Columns("tcnCapital").DefValue = CStr(lclsCover.nCapital)
					Else
						.Columns("tcnCapital").DefValue = ""
					End If
					If lclsCover.nCapital_wait > 0 Then
						.Columns("tcnCapitalsol").DefValue = CStr(lclsCover.nCapital_wait)
					Else
						.Columns("tcnCapitalsol").DefValue = ""
					End If
					If lclsCover.nRatecove > 0 Then
						.Columns("tcnRate").DefValue = CStr(lclsCover.nRatecove)
					Else
						.Columns("tcnRate").DefValue = ""
					End If
					If lclsCover.nRatecove_b > 0 Then
						.Columns("tcnRate_B").DefValue = CStr(lclsCover.nRatecove_b)
					Else
						.Columns("tcnRate_B").DefValue = ""
					End If
					If lclsCover.nPremium_tot > 0 Then
						.Columns("tcnPremium").DefValue = CStr(lclsCover.nPremium_tot)
					Else
						.Columns("tcnPremium").DefValue = ""
					End If
					If lclsCover.nPremium > 0 Then
						.Columns("tcPremium_B").DefValue = CStr(lclsCover.nPremium)
					Else
						.Columns("tcPremium_B").DefValue = ""
					End If
					If lclsCover.ncommi_an > 0 Then
						.Columns("tcnComssion").DefValue = CStr(lclsCover.ncommi_an)
					Else
						.Columns("tcnComssion").DefValue = ""
					End If
					
				End With
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	Else
		Response.Write("<SCRIPT>")
		Response.Write("alert(""Err. no existen coberturas a mostrar"");")
		Response.Write("</" & "Script>")
	End If
	Response.Write(mobjGrid.closeTable())
	'+ Se liberan de memoria las instancias creadas de los objetos utilizados en esta ventana - ACM - 15/12/2000    
	lclsCover = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
If Request.QueryString.Item("nGroup") <> vbNullString Then
	mlngGroup = mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)
Else
	mlngGroup = 0
End If
mlngModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)
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
    var lstrURL = self.document.location.href
    
    lstrURL = lstrURL.replace(/&nGroup=.*/,'');
    lstrURL = lstrURL.replace(/&nModulec=.*/,'');
    with(self.document.forms[0]){        
        nGroup = (typeof(valGroup)=='undefined')?"0":valGroup.value
        nModul = (typeof(valModulec)=='undefined')?"0":valModulec.value        
        self.document.location.href = lstrURL + 
                                      "&nGroup=" + nGroup +
                                      "&nModulec=" + nModul
    }                                                                 
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sCodispl") & ".aspx"))
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmCA829" ACTION="ValPolicySeq.aspx?x=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
Call insPreCA829()

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>




