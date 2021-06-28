<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'-Objeto para el manejo del menú	
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'- Objeto para obtener la información de condiciones del recargo/descuento
Dim mclsDsex_condi As eProduct.Dsex_condi
Dim mclsDisco_expr As eProduct.Disco_expr
Dim mcolDsex_condis As eProduct.Dsex_condis

'- Variables auxiliares
Dim mintOptCap As Object
Dim mintOptPrem As Object
Dim mintOptPro As Object
Dim mintOptPol As Object
Dim nRow As Integer


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.ActionQuery = Session("bQuery")
	With mobjGrid
		Call .Columns.AddPossiblesColumn(CInt("100170"), GetLocalResourceObject("cbeConceptColumnCaption"), "cbeConcept", "table298", eFunctions.Values.eValuesType.clngComboType, "0",  ,  ,  ,  ,  , True, 12, GetLocalResourceObject("cbeConceptColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .Columns.AddTextColumn(CInt("100170"), GetLocalResourceObject("cbeModuleColumnCaption"), "cbeModule", 25, "")
			.Columns("cbeModule").GridVisible = False
			Call .Columns.AddTextColumn(CInt("100170"), GetLocalResourceObject("tctModuleColumnCaption"), "tctModule", 25, "")
		Else
			Call .Columns.AddHiddenColumn("tctModule", "")
			Call .Columns.AddPossiblesColumn(CInt("100170"), GetLocalResourceObject("cbeModuleColumnCaption"), "cbeModule", "Tabtab_Modul", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nModulec"), True,  ,  ,  , "insOnChangeModulec();", True, 10, GetLocalResourceObject("cbeModuleColumnToolTip"))
			.Columns("cbeModule").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("cbeModule").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("cbeModule").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		Call .Columns.AddTextColumn(CInt("100172"), GetLocalResourceObject("tctItemColumnCaption"), "tctItem", 30, "",  , GetLocalResourceObject("tctItemColumnToolTip"),  ,  ,  , True)
		Call .Columns.AddNumericColumn(CInt("100171"), GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 6, CStr(0),  , GetLocalResourceObject("tcnRateColumnToolTip"), True, 2)
		Call .Columns.AddHiddenColumn("tcnExist", CStr(0))
		Call .Columns.AddHiddenColumn("tcnDisexprc", CStr(0))
		Call .Columns.AddHiddenColumn("tcnCode", CStr(0))
		Call .Columns.AddHiddenColumn("nRole", CStr(0))
		Call .Columns.AddHiddenColumn("hddRate", CStr(0))
	End With
	
	'+Se guardan los valores de los campos puntuales de la forma en campos escondidos
	With Response
		.Write(mobjValues.HiddenControl("tcnCurrency", Request.QueryString.Item("nCurrency")))
		.Write(mobjValues.HiddenControl("tcnDisRate", Request.QueryString.Item("nDisXpreRate")))
		.Write(mobjValues.HiddenControl("tcnPreFix", Request.QueryString.Item("nDisXPreFix")))
		.Write(mobjValues.HiddenControl("tcnPreMax", Request.QueryString.Item("nPreMax")))
		.Write(mobjValues.HiddenControl("tcnPreMin", Request.QueryString.Item("nPreMin")))
		.Write(mobjValues.HiddenControl("tctPreRou", Request.QueryString.Item("sDisxPreRou")))
		.Write(mobjValues.HiddenControl("optCapital", Request.QueryString.Item("optCapitalAplied")))
		.Write(mobjValues.HiddenControl("tctPreComm", Request.QueryString.Item("sPreComm")))
	End With
	
	With mobjGrid
		.Codispl = "DP08B2"
		.Codisp = "DP08B2"
		.Top = 135
		.Left = 100
		.Width = 350
		.Height = 250
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
		.Columns("tctItem").EditRecord = True
		.Columns("cbeConcept").BlankPosition = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		'+ Si el concepto es cobertura y el producto maneja módulo se habilita el campo módulo
		.Columns("cbeModule").Disabled = Not (CDbl(Request.QueryString.Item("nAplication")) = 1 And CDbl(Request.QueryString.Item("nModulec")) <> 0)
	End With
End Sub

'%insPreDP08B2: Carga los datos de la forma
'-------------------------------------------------------------------
Private Sub insPreDP08B2()
	'-------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lintIndexFind As Integer
	
	Call mclsDisco_expr.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	If Request.QueryString.Item("nOptCapApl") <> vbNullString Then
		If Request.QueryString.Item("nOptCapApl") = "1" Then
			mintOptCap = 1
			mintOptPrem = 2
		Else
			mintOptCap = 2
			mintOptPrem = 1
		End If
	Else
		If mclsDisco_expr.sEdperapl = "1" Then
			mintOptCap = 1
			mintOptPrem = 2
		Else
			mintOptCap = 2
			mintOptPrem = 1
		End If
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%	ChangeCapPremAplied: Ejecuta la busqueda con la opción de aplica sobre capital o prima" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function ChangeCapPremAplied(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	var nValue;" & vbCrLf)
Response.Write("	var lstrHref = '';" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	nValue = (self.document.forms[0].optCapitalAplied[0].checked?1:2)" & vbCrLf)
Response.Write("	if (mintOptCapPrem != nValue)" & vbCrLf)
Response.Write("	{" & vbCrLf)
Response.Write("		lstrHref += self.document.location.href" & vbCrLf)
Response.Write("		lstrHref = lstrHref.replace(/&nOptCapApl.*/, """");" & vbCrLf)
Response.Write("		lstrHref = lstrHref.replace(/&nCurrencyD.*/, """");" & vbCrLf)
Response.Write("		lstrHref = lstrHref.replace(/&nDisXpreFix.*/, """");" & vbCrLf)
Response.Write("		lstrHref = lstrHref.replace(/&nDisXpreRate.*/, """");" & vbCrLf)
Response.Write("		lstrHref = lstrHref.replace(/&nDisXpreMin.*/, """");" & vbCrLf)
Response.Write("		lstrHref = lstrHref.replace(/&nDisXpreMax.*/, """");" & vbCrLf)
Response.Write("		lstrHref = lstrHref.replace(/&sDisXpreRou.*/, """");" & vbCrLf)
Response.Write("		lstrHref = lstrHref.replace(/&sPreComm.*/, """");" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		lstrHref = lstrHref + ""&nOptCapApl="" + nValue;" & vbCrLf)
Response.Write("		lstrHref = lstrHref + ""&nCurrencyD="" + self.document.forms[0].elements[""cbeCurrencyD""].value;" & vbCrLf)
Response.Write("		lstrHref = lstrHref + ""&nDisXpreFix="" + self.document.forms[0].elements[""tcnDisXpreFix""].value;" & vbCrLf)
Response.Write("		lstrHref = lstrHref + ""&nDisXpreRate="" + self.document.forms[0].elements[""tcnDisXpreRate""].value;" & vbCrLf)
Response.Write("		lstrHref = lstrHref + ""&nDisXpreMin="" + self.document.forms[0].elements[""tcnDisXpreMin""].value;" & vbCrLf)
Response.Write("		lstrHref = lstrHref + ""&nDisXpreMax="" + self.document.forms[0].elements[""tcnDisXpreMax""].value;" & vbCrLf)
Response.Write("		lstrHref = lstrHref + ""&sDisXpreRou="" + self.document.forms[0].elements[""tctDisxPreRou""].value;" & vbCrLf)
Response.Write("		if (self.document.forms[0].elements[""chkDisXpreComm""].checked)" & vbCrLf)
Response.Write("			{lstrHref = lstrHref + ""&sPreComm=1"";}" & vbCrLf)
Response.Write("		else" & vbCrLf)
Response.Write("			{lstrHref = lstrHref + ""&sPreComm=0"";}" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		self.document.location.href = lstrHref" & vbCrLf)
Response.Write("		mintOptCapPrem = nValue" & vbCrLf)
Response.Write("	}	" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCheckSelClick(Field,lintIndex){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lstrParam=''" & vbCrLf)
Response.Write("    if (!Field.checked){" & vbCrLf)
Response.Write("		with (self.document.forms [0]){" & vbCrLf)
Response.Write("        lstrParam = ""nDisexprc=""+marrArray[lintIndex].tcnDisexprc + " & vbCrLf)
Response.Write("					""&nAplication="" + marrArray[lintIndex].cbeConcept + " & vbCrLf)
Response.Write("					""&nCode="" + marrArray[lintIndex].tcnCode + " & vbCrLf)
Response.Write("					""&nPercent="" + marrArray[lintIndex].tcnRate + " & vbCrLf)
Response.Write("					""&nModulec="" + marrArray[lintIndex].cbeModule + " & vbCrLf)
Response.Write("					""&nRole="" + marrArray[lintIndex].nRole + " & vbCrLf)
Response.Write("					""&nDisXPreFix=""+ tcnDisXpreFix.value + " & vbCrLf)
Response.Write("					""&nDisXPreRate=""+ tcnDisXpreRate.value + " & vbCrLf)
Response.Write("					""&sDisxPreRou="" + tctDisxPreRou.value + " & vbCrLf)
Response.Write("					""&optCapitalAplied="" + (optCapitalAplied[0].checked?1:2) + " & vbCrLf)
Response.Write("					""&nCurrency="" + cbeCurrencyD.value + " & vbCrLf)
Response.Write("					""&nPreMax="" + tcnDisXpreMax.value + " & vbCrLf)
Response.Write("					""&nPreMin="" + tcnDisXpreMin.value + " & vbCrLf)
Response.Write("					""&sPreComm="" + (chkDisXpreComm.checked?1:2)" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("        EditRecord(lintIndex,nMainAction,""Del"",lstrParam)" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    else{" & vbCrLf)
Response.Write("		with (self.document.forms [0]){" & vbCrLf)
Response.Write("			lstrParam=	""nDisXPreFix=""+ tcnDisXpreFix.value + " & vbCrLf)
Response.Write("						""&nDisXPreRate=""+ tcnDisXpreRate.value + " & vbCrLf)
Response.Write("						""&sDisxPreRou="" + tctDisxPreRou.value + " & vbCrLf)
Response.Write("						""&optCapitalAplied="" + (optCapitalAplied[0].checked?1:2) + " & vbCrLf)
Response.Write("						""&nCurrency="" + cbeCurrencyD.value + " & vbCrLf)
Response.Write("						""&nPreMax="" + tcnDisXpreMax.value + " & vbCrLf)
Response.Write("						""&nPreMin="" + tcnDisXpreMin.value + " & vbCrLf)
Response.Write("						""&sPreComm="" + (chkDisXpreComm.checked?1:2)" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("        EditRecord(lintIndex,nMainAction,""Update"",lstrParam)" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    Field.checked = !Field.checked" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Call Header()
	lintIndex = 0
	
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		nRow = 1
	Else
		nRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	For lintIndexFind = 1 To 2
		mcolDsex_condis = New eProduct.Dsex_condis
		If mcolDsex_condis.Find(Session("sBrancht"), Session("nBranch"), Session("nProduct"), Session("nDisexprc"), Session("dEffecdate"), Session("nOrderApl"), mobjValues.StringToType(mintOptCap, eFunctions.Values.eTypeData.etdDouble), lintIndexFind, nRow) Then
			
			For	Each mclsDsex_condi In mcolDsex_condis
				With mobjGrid
					.Columns("tcnDisexprc").DefValue = mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble)
					.Columns("cbeConcept").DefValue = CStr(mclsDsex_condi.nAplication)
					If Request.QueryString.Item("Type") <> "PopUp" Then
						.Columns("tctModule").DefValue = mclsDsex_condi.sDescriptModulec
						.Columns("cbeModule").DefValue = CStr(mclsDsex_condi.nModulec)
					Else
						If mclsDsex_condi.nModulec = 0 Or mclsDsex_condi.nModulec = eRemoteDB.Constants.intNull Then
							.Columns("cbeModule").DefValue = CStr(0)
						Else
							.Columns("cbeModule").DefValue = CStr(mclsDsex_condi.nModulec)
						End If
					End If
					'+ Si el producto es vida
					If CStr(Session("sBrancht")) = "1" Then
						'+ Si el tipo es cobertura
						If mclsDsex_condi.nAplication = 1 Then
							.Columns("tctItem").DefValue = mclsDsex_condi.sDescriptRol & " " & mclsDsex_condi.sDescript
						Else
							.Columns("tctItem").DefValue = mclsDsex_condi.sDescript
						End If
					Else
						.Columns("tctItem").DefValue = mclsDsex_condi.sDescript
					End If
					.Columns("tcnRate").DefValue = CStr(mclsDsex_condi.nRate)
					.Columns("tcnCode").DefValue = CStr(mclsDsex_condi.nCode)
					.Columns("tcnExist").DefValue = CStr(mclsDsex_condi.nExist)
					.Columns("nRole").DefValue = CStr(mclsDsex_condi.nRole)
					.Columns("hddRate").DefValue = CStr(mclsDsex_condi.nRate)
					
					.sEditRecordParam = "nDisXPreFix='+ self.document.forms[0].tcnDisXpreFix.value + '&sDisxPreRou=' + self.document.forms[0].tctDisxPreRou.value + '&optCapitalAplied=' + (self.document.forms [0].optCapitalAplied[0].checked?1:2) + '&nCurrency=' + self.document.forms[0].cbeCurrencyD.value + '&nPreMax='+ self.document.forms[0].tcnDisXpreMax.value + '&nPreMin='+ self.document.forms[0].tcnDisXpreMin.value +'&nDisXpreRate='+ self.document.forms[0].tcnDisXpreRate.value + '&sPreComm=' + (self.document.forms[0].chkDisXpreComm.checked?1:2) + " & "'&nAplication=" & mclsDsex_condi.nAplication & "&nModulec=" & mclsDsex_condi.nModulec & "&nRole=" & mclsDsex_condi.nRole & "&nPercent=" & mclsDsex_condi.nRate
					If mclsDsex_condi.nExist = 1 Then
						.Columns("Sel").Checked = 1
					Else
						.Columns("Sel").Checked = 2
					End If
					.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				End With
				Response.Write(mobjGrid.DoRow())
				lintIndex = lintIndex + 1
			Next mclsDsex_condi
		End If
		mcolDsex_condis = Nothing
	Next 
	Response.Write(mobjGrid.CloseTable())
End Sub

'% insPreDP08B2Upd: Realiza la eliminación de una condición de aplicación de recargo/descuento
'----------------------------------------------------------------------------------------------
Private Sub insPreDP08B2Upd()
	'----------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		lblnPost = mclsDisco_expr.insPostDP08B2Upd("Del", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nAplication"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDisXPreFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPreMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPreMin"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDisxPreRou"), Request.QueryString.Item("optCapitalAplied"), Request.QueryString.Item("sPreComm"), "", mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDisXpreRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), Session("sBrancht"))
		
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valDiscoExprSeq.aspx", "DP08B2", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	If Request.QueryString.Item("Action") <> "Del" Then
		
Response.Write("	" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("	if(self.document.forms[0].cbeModule.disabled)" & vbCrLf)
Response.Write("		document.forms[0].action= document.forms[0].action + '&nAcceptModule=2'" & vbCrLf)
Response.Write("	else" & vbCrLf)
Response.Write("		document.forms[0].action= document.forms[0].action + '&nAcceptModule=1'" & vbCrLf)
Response.Write("</" & "SCRIPT>		")

		
	End If
End Sub

'% Header: Muestra el encabezado
'----------------------------------------------------------------------------------------------
Private Sub Header()
	'----------------------------------------------------------------------------------------------
	mobjValues.ActionQuery = Session("bQuery")
	If CStr(Session("sBrancht")) = "1" Then
		If mclsDisco_expr.sDefpol <> vbNullString Then
			If mclsDisco_expr.sDefpol = "1" Then
				mintOptPro = 2
				mintOptPol = 1
			Else
				mintOptPro = 1
				mintOptPol = 2
			End If
		End If
		
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%""> " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=100166><A NAME=""Definición"">" & GetLocalResourceObject("AnchorDefiniciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>   " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		</TR>   " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		")

		If mintOptPro = vbNullString And mintOptPol = vbNullString Then
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optDefpol", GetLocalResourceObject("optDefpol_CStr2Caption"), CStr(1), CStr(2),  ,  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optDefpol", GetLocalResourceObject("optDefpol_CStr1Caption"), CStr(2), CStr(1),  ,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		Else
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optDefpol", GetLocalResourceObject("optDefpol_CStr2Caption"), mintOptPro, CStr(2),  ,  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optDefpol", GetLocalResourceObject("optDefpol_CStr1Caption"), mintOptPol, CStr(1),  ,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		End If
Response.Write("		    " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%""> 		" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=100166><A NAME=""Monto"">" & GetLocalResourceObject("AnchorMontoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"">&nbsp;</TD>	" & vbCrLf)
Response.Write("        </TR>   " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3""></TD>	" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>        " & vbCrLf)
Response.Write("            <TD><LABEL ID=14671>" & GetLocalResourceObject("cbeCurrencyDCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		If Request.QueryString.Item("nCurrencyD") <> vbNullString Then
			Response.Write(mobjValues.PossiblesValues("cbeCurrencyD", "TabCur_allow", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(Request.QueryString.Item("nCurrencyD"), eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCurrencyDToolTip"),  , 3))
		Else
			Response.Write(mobjValues.PossiblesValues("cbeCurrencyD", "TabCur_allow", eFunctions.Values.eValuesType.clngComboType, CStr(mclsDisco_expr.nCurrency), True,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCurrencyDToolTip"),  , 3))
		End If
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""10%"">&nbsp;</TD>	" & vbCrLf)
Response.Write("			<TD><LABEL ID=14675>" & GetLocalResourceObject("tctDisxPreRouCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	If Request.QueryString.Item("sDisxPreRou") <> vbNullString Then
		Response.Write(mobjValues.TextControl("tctDisxPreRou", 12, Request.QueryString.Item("sDisxPreRou"),  , GetLocalResourceObject("tctDisxPreRouToolTip"),  ,  ,  ,  ,  , 7))
	Else
		Response.Write(mobjValues.TextControl("tctDisxPreRou", 12, mclsDisco_expr.sRoutine,  , GetLocalResourceObject("tctDisxPreRouToolTip"),  ,  ,  ,  ,  , 7))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>	" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14672>" & GetLocalResourceObject("tcnDisXpreFixCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	If Request.QueryString.Item("nDisXpreFix") <> vbNullString Then
		Response.Write(mobjValues.NumericControl("tcnDisXpreFix", 18, Request.QueryString.Item("nDisXpreFix"),  , GetLocalResourceObject("tcnDisXpreFixToolTip"), True, 6,  ,  ,  ,  ,  , 4))
	Else
		Response.Write(mobjValues.NumericControl("tcnDisXpreFix", 18, CStr(mclsDisco_expr.nDisexpra),  , GetLocalResourceObject("tcnDisXpreFixToolTip"), True, 6,  ,  ,  ,  ,  , 4))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""10%"">&nbsp;</TD>	" & vbCrLf)
Response.Write("			<TD><LABEL ID=14672>" & GetLocalResourceObject("tcnDisXpreRateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	If Request.QueryString.Item("nDisXpreRate") <> vbNullString Then
		Response.Write(mobjValues.NumericControl("tcnDisXpreRate", 5, Request.QueryString.Item("nDisXpreRate"),  , GetLocalResourceObject("tcnDisXpreRateToolTip"), True, 2,  ,  ,  ,  , False, 8))
	Else
		Response.Write(mobjValues.NumericControl("tcnDisXpreRate", 5, CStr(mclsDisco_expr.nRate),  , GetLocalResourceObject("tcnDisXpreRateToolTip"), True, 2,  ,  ,  ,  , False, 8))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>  " & vbCrLf)
Response.Write("			<TD><LABEL ID=14674>" & GetLocalResourceObject("tcnDisXpreMinCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	If Request.QueryString.Item("nDisXpreMin") <> vbNullString Then
		Response.Write(mobjValues.NumericControl("tcnDisXpreMin", 18, Request.QueryString.Item("nDisXpreMin"),  , GetLocalResourceObject("tcnDisXpreMinToolTip"), True, 6,  ,  ,  ,  ,  , 5))
	Else
		Response.Write(mobjValues.NumericControl("tcnDisXpreMin", 18, CStr(mclsDisco_expr.nDisexmin),  , GetLocalResourceObject("tcnDisXpreMinToolTip"), True, 6,  ,  ,  ,  ,  , 5))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>         " & vbCrLf)
Response.Write("			<TD><LABEL ID=19657>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(100166, "optCapitalAplied", GetLocalResourceObject("optCapitalAplied_CStr1Caption"), mintOptCap, CStr(1), "ChangeCapPremAplied(this.value)",  , 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14673>" & GetLocalResourceObject("tcnDisXpreMaxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	If Request.QueryString.Item("nDisXpreMax") <> vbNullString Then
		Response.Write(mobjValues.NumericControl("tcnDisXpreMax", 18, Request.QueryString.Item("nDisXpreMax"),  , GetLocalResourceObject("tcnDisXpreMaxToolTip"), True, 6,  ,  ,  ,  ,  , 6))
	Else
		Response.Write(mobjValues.NumericControl("tcnDisXpreMax", 18, CStr(mclsDisco_expr.nDisexmax),  , GetLocalResourceObject("tcnDisXpreMaxToolTip"), True, 6,  ,  ,  ,  ,  , 6))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(100168, "optCapitalAplied", GetLocalResourceObject("optCapitalAplied_CStr2Caption"), mintOptPrem, CStr(2), "ChangeCapPremAplied(this.value)",  , 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>	" & vbCrLf)
Response.Write("		<TR>      " & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">" & vbCrLf)
Response.Write("				")

	
	If Request.QueryString.Item("sPreComm") <> vbNullString Then
		If CDbl(Request.QueryString.Item("sPreComm")) = 1 Then
			Response.Write(mobjValues.CheckControl("chkDisXpreComm", GetLocalResourceObject("chkDisXpreCommCaption"), CStr(1), "1",  ,  , 11))
		Else
			Response.Write(mobjValues.CheckControl("chkDisXpreComm", GetLocalResourceObject("chkDisXpreCommCaption"),  , "1",  ,  , 11))
		End If
	Else
		Response.Write(mobjValues.CheckControl("chkDisXpreComm", GetLocalResourceObject("chkDisXpreCommCaption"), mclsDisco_expr.sCommissi_i, "1"))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>        " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsDisco_expr = New eProduct.Disco_expr
mclsDsex_condi = New eProduct.Dsex_condi

mobjGrid = New eFunctions.Grid
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "dp08b2"
mobjGrid.sCodisplPage = "dp08b2"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 10/11/04 14:17 $|$$Author: Nvaplat15 $"

	var mintOptCapPrem=<%="'" & Request.QueryString.Item("nOptCapApl") & "'"%>; 
	if (mintOptCapPrem == '') mintOptCapPrem=0

//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}
	self.document.location.href = lstrURL;
}	
</SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP08B2"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP08B2", "DP08B2.aspx"))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP08B2" ACTION="valDiscoExprSeq.aspx?Time=2">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP08B2()
Else
	Call insPreDP08B2Upd()
End If
%>
<%=mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow")))%>
<%=mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')")%>
<%
mobjGrid = Nothing
mobjValues = Nothing
mclsDsex_condi = Nothing
mclsDisco_expr = Nothing
%>
</FORM>
</BODY>
</HTML>
   





