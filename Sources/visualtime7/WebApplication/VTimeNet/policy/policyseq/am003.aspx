<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.02
Dim mobjNetFrameWork As eNetFrameWork.Layout


'- Objeto para el manejo de las funciones generales de carga de valores  
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

'- Booleanas para el control de habilitación/deshabilitación de controles
Dim mblnDisabled As Boolean
Dim mblnValues As Boolean
Dim mblnGroup As Boolean
Dim mblnModulec As Boolean

    
'- Variables que contienen los valores por defecto de los campos de la pk.
Dim mintTariff As Object
Dim mintGroup As Object
Dim mintModulec As Object
Dim mlngCover As Object
Dim mintRole As Object
Dim mstrClient As String
Dim mstrIllness As String

Dim mstrType As Object

'- Variables que contienen los valores por defecto de los campos de la pk que se utilizan para verificar si cambio alguno de ellos.    
Dim mintTariffChange As Object
Dim mintGroupChange As Object
Dim mintModulecChange As Object
Dim mlngCoverChange As Object
Dim mintRoleChange As Object
Dim mstrClientChange As Object
Dim mstrIllnessChange As Object
Dim mstrAut_restitChange As Object
Dim mdblLimitHChange As Object


'% insDefaultValues: Se encarga de mostrar la tarifa por defecto seleccionada
'-----------------------------------------------------------------------------------------
Private Sub insDefaultValues()
	'-----------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrMessage_aux As String
	
	mblnValues = False
	mblnDisabled = False
	
	lclsPolicy = New ePolicy.Policy
	If lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
		'Si el campo definición de prestaciones por certificado (4)	y es una poliza matriz se inhabilita la ventana
		If (lclsPolicy.sColtpres = "4" And lclsPolicy.sPolitype <> "1" And Session("nCertif") = 0) Or (lclsPolicy.sColtpres = "2" And lclsPolicy.sPolitype <> "1" And Session("nCertif") > 0) Then
			If lclsPolicy.sColtpres = "2" Then
				lstrMessage_aux = "Póliza"
			Else
				If lclsPolicy.sColtpres = "4" Then
					lstrMessage_aux = "Certificado"
				End If
			End If
			mblnDisabled = True
		End If
	End If
	'+ No se está utilizando validación 55869 impide el ingreso de información de dependiendo de como se definen las prestaciones,
	'+ ya que se heredan las prestaciones definidas a nivel de póliza
	'+ Si se desea realizar la validación eliminar el siguiente código
	mblnDisabled = False
	'+ No se está utilizando validación 55869 impide el ingreso de información de dependiendo de como se definen las prestaciones,
	'+ ya que se heredan las prestaciones definidas a nivel de póliza
	
	
	lclsPolicy = Nothing
	Dim mobjError As eFunctions.Errors
	Dim lclsGroups As ePolicy.Groups
	Dim lclsProduct As eProduct.Product
	Dim lclsTab_am_bab As eBranches.Tab_Am_Bab
	If mblnDisabled Then
		mobjError = New eFunctions.Errors
		'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
		mobjError.sSessionID = Session.SessionID
		mobjError.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		Response.Write(mobjError.ErrorMessage("AM003", 55869,  , 1, lstrMessage_aux, True))
		mobjError = Nothing
		mintGroup = 0
		mintModulec = 0
		mlngCover = 0
		mintTariff = 0
		mintRole = 0
		mstrClient = vbNullString
		mstrIllness = vbNullString
	Else
		lclsGroups = New ePolicy.Groups
		mblnGroup = False
		If lclsGroups.valGroupExist_a(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("Deffecdate")) Then
			mblnGroup = True
		End If
		lclsGroups = Nothing
		
		lclsProduct = New eProduct.Product
		mblnModulec = False
		If lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
			mblnModulec = True
		End If
		lclsProduct = Nothing
		
		'+ Si no hay una tarifa seleccionada se muestra la que se definió por defecto.
		If Request.QueryString.Item("nTariff") = vbNullString Then
			lclsTab_am_bab = New eBranches.Tab_Am_Bab
			'+ Obtiene la información por defecto a mostrar
			If lclsTab_am_bab.FindDeftValues(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), True) Then
				mblnValues = True
				mintGroup = lclsTab_am_bab.nGroup
				mintModulec = lclsTab_am_bab.nModulec
				mlngCover = lclsTab_am_bab.nCover
				mintTariff = lclsTab_am_bab.nTariff
				mintRole = lclsTab_am_bab.nRole
				mstrClient = lclsTab_am_bab.sClient
				mstrIllness = lclsTab_am_bab.sIllness
				If mstrIllness = "0" Then
					mstrIllness = vbNullString
				End If
			End If
			lclsTab_am_bab = Nothing
		End If
		
		If Not mblnValues Then
			mintGroup = mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)
			mintModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)
			mlngCover = mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble)
			mintTariff = mobjValues.StringToType(Request.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble)
			mintRole = mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble)
			mstrClient = Request.QueryString.Item("sClient")
			mstrIllness = Request.QueryString.Item("sIllness")
		End If
	End If
	
	If mintGroup <= 0 Then
		mintGroup = 0
	End If
	
	If mintModulec <= 0 Then
		mintModulec = 0
	End If
	
	If mlngCover <= 0 Then
		mlngCover = 0
	End If
	
	If mintTariff <= 0 Then
		mintTariff = 0
	End If
	
	If mintRole <= 0 Then
		mintRole = 0
	End If
	
End Sub

'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	If Request.QueryString.Item("nGroup") = vbNullString Then
		mintGroupChange = 0
	Else
		mintGroupChange = Request.QueryString.Item("nGroup")
	End If
	
	If Request.QueryString.Item("nModulec") = vbNullString Then
		mintModulecChange = 0
	Else
		mintModulecChange = Request.QueryString.Item("nModulec")
	End If
	
	If Request.QueryString.Item("nCover") = vbNullString Then
		mlngCoverChange = 0
	Else
		mlngCoverChange = Request.QueryString.Item("nCover")
	End If
	
	If Request.QueryString.Item("nTariff") = vbNullString Then
		mintTariffChange = 0
	Else
		mintTariffChange = Request.QueryString.Item("nTariff")
	End If
	
	If Request.QueryString.Item("nRole") = vbNullString Then
		mintRoleChange = 0
	Else
		mintRoleChange = Request.QueryString.Item("nRole")
	End If
	
	If Request.QueryString.Item("sClient") = vbNullString Then
		mstrClientChange = 0
	Else
		mstrClientChange = Request.QueryString.Item("sClient")
	End If
	
	If Request.QueryString.Item("sIllness") = vbNullString Then
		mstrIllnessChange = 0
	Else
		mstrIllnessChange = Request.QueryString.Item("sIllness")
	End If
	
	If Request.QueryString.Item("sIllness") = vbNullString Then
		mstrIllnessChange = 0
	Else
		mstrIllnessChange = Request.QueryString.Item("sIllness")
	End If
	
	If Request.QueryString.Item("sAutRestit") = vbNullString Then
		mstrAut_restitChange = 0
	Else
		mstrAut_restitChange = Request.QueryString.Item("sAutRestit")
	End If
	
	If Request.QueryString.Item("nLimitH") = vbNullString Then
		mdblLimitHChange = 0
	Else
		mdblLimitHChange = Request.QueryString.Item("nLimitH")
	End If
	
	With Response
		.Write("<SCRIPT>")
		.Write("var mintGroupChange = '" & CStr(mintGroupChange) & "';")
		.Write("var mintModulecChange = '" & CStr(mintModulecChange) & "';")
		.Write("var mlngCoverChange = '" & CStr(mlngCoverChange) & "';")
		.Write("var mintTariffChange = '" & CStr(mintTariffChange) & "';")
		.Write("var mintRoleChange = '" & CStr(mintRoleChange) & "';")
		.Write("var mstrClientChange = '" & CStr(mstrClientChange) & "';")
		.Write("var mstrIllnessChange = '" & CStr(mstrIllnessChange) & "';")
		.Write("var mstrAut_restitChange = '" & CStr(mstrAut_restitChange) & "';")
		.Write("var mdblLimitHChange = '" & CStr(mdblLimitHChange) & "';")
		.Write("</" & "Script>")
	End With
End Sub

'%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
'--------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------------------------------------------------------
	
	Dim lobjCol As eFunctions.Column
	Response.Write(mobjValues.HiddenControl("hddbCreHeader", CStr(False)))
        With mobjGrid
            .Splits_Renamed.AddSplit(0, "", 2)
            .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnPay_ConcepColumnCaption"), "tcnPay_Concep", "table159", eFunctions.Values.eValuesType.clngComboType, , False, , , , , True, , GetLocalResourceObject("tcnPay_ConcepColumnToolTip"))
            lobjCol = .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnPrestacColumnCaption"), "tcnPrestac", "tabcl_cov_bil2", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "ReaGroupPrest();", Request.QueryString.Item("Action") <> "Add", , GetLocalResourceObject("tcnPrestacColumnToolTip"))
            lobjCol.Parameters.ReturnValue("nGroup_Pres", , , True)
		
            .Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
            .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnDed_TypeColumnCaption"), "tcnDed_Type", "Table269", 1, CStr(1), , , , , , , , GetLocalResourceObject("tcnDed_TypeColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnDed_PercenColumnCaption"), "tcnDed_Percen", 4, vbNullString, , GetLocalResourceObject("tcnDed_PercenColumnToolTip"), , 2)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnDed_AmountColumnCaption"), "tcnDed_Amount", 18, vbNullString, , GetLocalResourceObject("tcnDed_AmountColumnToolTip"), True, 6)
		
            .Splits_Renamed.AddSplit(0, GetLocalResourceObject("Caption_Carencia"), 3)
            .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcsCaren_TypeColumnCaption"), "tcsCaren_Type", "Table52", 1, CStr(1), , , , , , , , GetLocalResourceObject("tcsCaren_TypeColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCaren_DurColumnCaption"), "tcnCaren_Dur", 5, vbNullString, , GetLocalResourceObject("tcnCaren_DurColumnToolTip"))
		
            .Splits_Renamed.AddSplit(0, GetLocalResourceObject("7ColumnCaption"), 7)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnDed_QuantiColumnCaption"), "tcnDed_Quanti", 5, vbNullString, , GetLocalResourceObject("tcnDed_QuantiColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnIndem_RateColumnCaption"), "tcnIndem_Rate", 5, CStr(100), , GetLocalResourceObject("tcnIndem_RateColumnToolTip"), , 2)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnLimitColumnCaption"), "tcnLimit", 18, vbNullString, , GetLocalResourceObject("tcnLimitColumnToolTip"), True, 6)
            lobjCol = .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnTyplimColumnCaption"), "tcnTyplim", "Table269", eFunctions.Values.eValuesType.clngComboType, , , , , , "insChangeTyplim(this)", , , GetLocalResourceObject("tcnTyplimColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCountColumnCaption"), "tcnCount", 5, vbNullString, , GetLocalResourceObject("tcnCountColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnLimit_exeColumnCaption"), "tcnLimit_exe", 18, vbNullString, , GetLocalResourceObject("tcnLimit_exeColumnToolTip"), True, 6)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPunishColumnCaption"), "tcnPunish", 4, vbNullString, , GetLocalResourceObject("tcnPunishColumnToolTip"), , 2)
		
            Call .Columns.AddCheckColumn(0, GetLocalResourceObject("chksotherlimColumnCaption"), "chksotherlim", "", , , "insActiveFields()", Request.QueryString.Item("Type") <> "PopUp" Or CBool(mblnDisabled), GetLocalResourceObject("chksotherlimColumnToolTip"))
		    
            .Splits_Renamed.AddSplit(0, GetLocalResourceObject("8ColumnCaption"), 7)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnDed_Quanti_2ColumnCaption"), "tcnDed_Quanti_2", 5, vbNullString, , GetLocalResourceObject("tcnDed_Quanti_2ColumnToolTip"), , , , , , Request.QueryString.Item("sOtherLim") = "2" Or Request.QueryString.Item("Action") = "Add")
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnIndem_Rate_2ColumnCaption"), "tcnIndem_Rate_2", 5, vbNullString, , GetLocalResourceObject("tcnIndem_Rate_2ColumnToolTip"), , 2, , , , Request.QueryString.Item("sOtherLim") = "2" Or Request.QueryString.Item("Action") = "Add")
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnLimit_2ColumnCaption"), "tcnLimit_2", 18, vbNullString, , GetLocalResourceObject("tcnLimit_2ColumnToolTip"), True, 6, , , , Request.QueryString.Item("sOtherLim") = "2" Or Request.QueryString.Item("Action") = "Add")
            lobjCol = .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnTyplim_2ColumnCaption"), "tcnTyplim_2", "Table269", eFunctions.Values.eValuesType.clngComboType, , , , , , "insChangeTyplim(this)", Request.QueryString.Item("sOtherLim") = "2" Or Request.QueryString.Item("Action") = "Add", , GetLocalResourceObject("tcnTyplim_2ColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCount_2ColumnCaption"), "tcnCount_2", 5, vbNullString, , GetLocalResourceObject("tcnCount_2ColumnToolTip"), , , , , , Request.QueryString.Item("sOtherLim") = "2" Or Request.QueryString.Item("Action") = "Add")
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnLimit_exe_2ColumnCaption"), "tcnLimit_exe_2", 18, vbNullString, , GetLocalResourceObject("tcnLimit_exe_2ColumnToolTip"), True, 6, , , , Request.QueryString.Item("sOtherLim") = "2" Or Request.QueryString.Item("Action") = "Add")
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPunish_2ColumnCaption"), "tcnPunish_2", 4, vbNullString, , GetLocalResourceObject("tcnPunish_2ColumnToolTip"), , 2, , , , Request.QueryString.Item("sOtherLim") = "2" Or Request.QueryString.Item("Action") = "Add")
		
            .sEditRecordParam = "nTariff=" & mintTariff & "&nCover=" & mlngCover & "&sAutRestit=' + self.document.forms[0].chkAutRestit.value + '" & "&nLimitH=' + self.document.forms[0].tcnLimitH.value + '" & "&nRole=" & mintRole & "&sClient=" & mstrClient & "&sIllness=" & mstrIllness & "&nGroup=" & mintGroup & "&nModulec=" & mintModulec & "&bCreHeader=' + self.document.forms[0].hddbCreHeader.value + '"
		
		
            .sDelRecordParam = "nTariff=" & mintTariff & "&nCover=" & mlngCover & "&sAutRestit=' + self.document.forms[0].chkAutRestit.value + '" & "&nLimitH=' + self.document.forms[0].tcnLimitH.value + '" & "&nRole=" & mintRole & "&sClient=" & mstrClient & "&sIllness=" & mstrIllness & "&nGroup=" & mintGroup & "&nModulec=" & mintModulec & "&nPay_Concep=' + marrArray[lintIndex].tcnPay_Concep  + '" & "&nPrestac=' + marrArray[lintIndex].tcnPrestac  + '"
		
            .MoveRecordScript = "DisabledItem()"
		
            'With .Columns("tcnPay_Concep").Parameters
            '.Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("nCover", mlngCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("nRole", mintRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'End With
		
            With .Columns("tcnPrestac").Parameters
                .Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCover", mlngCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nRole", mintRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
		
            .Columns("tcnPrestac").EditRecord = True
		
            .Columns("Sel").GridVisible = True
		
            .FieldsByRow = 2
            .Codispl = "AM003"
            '.Top			  = 100
            .Width = 900
            .Height = 400
            .AddButton = (mintTariff > 0 And mlngCover > 0)
            .DeleteButton = (mintTariff > 0 And mlngCover > 0)
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
End Sub

'% insPreAM003: hace la lectura de los campos a mostrar en pantalla
'----------------------------------------------------------------------------------------------
Private Sub insPreAM003()
	'----------------------------------------------------------------------------------------------
	
	
	' Se heredan los datos de prestaciones del productos siempre y cuando ls definiciones
	' de la poliza este configurada por poliza y el certificado sea cero
	Dim lclseBranches As eBranches.Tab_Am_Bil
	If Session("nCertif") = 0 Then
		lclseBranches = New eBranches.Tab_Am_Bil
		lclseBranches.CargaTab_am_bil(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mintGroup)
		
		lclseBranches = Nothing
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("   		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeTariffCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("				")

	
	With mobjValues
		.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeTariff", "tabtar_am_bas", eFunctions.Values.eValuesType.clngComboType, mintTariff, True,  ,  ,  ,  , "insReload(this);", mblnDisabled,  , GetLocalResourceObject("cbeTariffToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			")

	If mblnGroup Then
Response.Write("" & vbCrLf)
Response.Write("				<TD WIDTH=""15%""><LABEL ID=13052>" & GetLocalResourceObject("valInsuredGrCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">")

		
		mobjValues.ActionQuery = False
		With mobjValues.Parameters
			.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		If Request.QueryString.Item("nMainAction") = "401" Then
			Response.Write(mobjValues.PossiblesValues("valInsuredGr", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(mintGroup, eFunctions.Values.eTypeData.etdDouble, True), True,  ,  ,  ,  , "insReload(this," & mintTariff & "," & mlngCover & "," & mintRole & "," & mstrIllness & ")", mblnDisabled Or Not mblnGroup,  , GetLocalResourceObject("valInsuredGrToolTip")))
		Else
			Response.Write(mobjValues.PossiblesValues("valInsuredGr", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(mintGroup, eFunctions.Values.eTypeData.etdDouble, True), True,  ,  ,  ,  , "insReload(this)", mblnDisabled Or Not mblnGroup,  , GetLocalResourceObject("valInsuredGrToolTip")))
		End If
		
Response.Write("" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			")

        'Response.Write("<NOTSCRIPT>alert('" & mintModulec & "'); </" & "Script>")
        
	If mblnModulec Then
Response.Write("" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("valModulecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>" & vbCrLf)
Response.Write("					")

        '    mobjValues.ActionQuery = True
		With mobjValues.Parameters
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
            
            

            
            
		Response.Write(mobjValues.PossiblesValues("valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngComboType, mintModulec, True,  ,  ,  ,  , "insReload(this)", mblnDisabled And Not mblnModulec,  , GetLocalResourceObject("valModulecToolTip")))
		
Response.Write("" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valCoverCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("				")

	mobjValues.ActionQuery = Session("bQuery")
	With mobjValues
		.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCoverNoShow", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCoverMax", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valCover", "tablife_cover", eFunctions.Values.eValuesType.clngComboType, mlngCover, True,  ,  ,  ,  , "insReload(this)", mblnDisabled,  , ""))
	
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>	" & vbCrLf)
Response.Write("		    <TD><LABEL ID=13052>" & GetLocalResourceObject("valRoleCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("                ")

	mobjValues.ActionQuery = Session("bQuery")
	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCover", mlngCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valRole", "tabtab_covrol3", eFunctions.Values.eValuesType.clngComboType, mintRole, True,  ,  ,  ,  , "insReload(this)", mblnDisabled And Session("nCertif") = 0,  , GetLocalResourceObject("valRoleToolTip")))
	
	
Response.Write("" & vbCrLf)
Response.Write("            </TD> " & vbCrLf)
Response.Write("            ")

	If Session("nCertif") > 0 Then
Response.Write("" & vbCrLf)
Response.Write("				<TD><LABEL ID=13052>" & GetLocalResourceObject("valClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")

		With mobjValues.Parameters
			.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nGroup", mintGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nCover", mlngCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.ReturnValue("nRole", True,  , True)
		End With
		Response.Write(mobjValues.PossiblesValues("valClient", "tabcoverinsured", eFunctions.Values.eValuesType.clngWindowType, mstrClient, True,  ,  ,  ,  , "ChangeRole(this);insReload(this)", mblnDisabled Or mlngCover <= 0, 14, GetLocalResourceObject("valClientToolTip"), eFunctions.Values.eTypeCode.eString))
		
Response.Write("" & vbCrLf)
Response.Write("				</TD> " & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("		<TR>	     " & vbCrLf)
Response.Write("            <TD><LABEL ID=13052>" & GetLocalResourceObject("valIllnessCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"">")

	mobjValues.ActionQuery = Session("bQuery")
	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("sClientGen", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valIllness", "tabtab_am_ill", eFunctions.Values.eValuesType.clngWindowType, mstrIllness, True,  ,  ,  ,  , "insReload(this)", mblnDisabled, 8, GetLocalResourceObject("valIllnessToolTip"), eFunctions.Values.eTypeCode.eString))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			")

	If Request.QueryString.Item("sAutRestit") = "1" Then
Response.Write("" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">")

		Response.Write(mobjValues.CheckControl("chkAutRestit", GetLocalResourceObject("chkAutRestitCaption"), "1", CStr(1), "insCreHeader()", mblnDisabled))
Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">")

		Response.Write(mobjValues.CheckControl("chkAutRestit", GetLocalResourceObject("chkAutRestitCaption"), "", CStr(2), "insCreHeader()", mblnDisabled))
Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnLimitHCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("				")

	mobjValues.ActionQuery = False
	Response.Write(mobjValues.NumericControl("tcnLimitH", 18, Request.QueryString.Item("nLimitH"),  , GetLocalResourceObject("tcnLimitHToolTip"), True, 6,  ,  ,  , "insCreHeader()", mblnDisabled))
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	")
    
        If Not mblnDisabled Then
            insDefineGrid()
        End If
	
Response.Write("")

	
End Sub

'%insDefineGrid: define el grid según lo leído de las tablas incolucradas
'----------------------------------------------------------------------------------------------
Private Sub insDefineGrid()
	'----------------------------------------------------------------------------------------------
	Dim lstrBoolean As String
	Dim lcolTab_Am_Bil As eBranches.Tab_am_Bils
	Dim lclsTab_Am_Bil As eBranches.Tab_Am_Bil
	Dim lintIndex As Short
	Dim lblnExist As Boolean
	Dim lblnHeader As Boolean
	
	' Se realiza carga de prestaciones si el certificado es cero y 
	' la definicion de prestaciones es por poliza
	
	
	
	lcolTab_Am_Bil = New eBranches.Tab_am_Bils
	lclsTab_Am_Bil = New eBranches.Tab_Am_Bil
	
	lintIndex = 0
	lblnExist = False
	lblnHeader = True
	'Response.Write "<NOTSCRIPT>alert('"& mintGroup  &"'); </" & "Script>"
	If lcolTab_Am_Bil.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mintGroup, mintModulec, mlngCover, mintTariff, mintRole, mstrClient, mstrIllness, Session("dEffecdate")) Then
		lblnExist = True
		For	Each lclsTab_Am_Bil In lcolTab_Am_Bil
			If lblnHeader Then
				lblnHeader = False
				
Response.Write("					    " & vbCrLf)
Response.Write("				<SCRIPT>" & vbCrLf)
Response.Write("					document.forms[0].item(""chkAutRestit"").checked = '(")


Response.Write(lclsTab_Am_Bil.sAuto_resist_h)


Response.Write("==""1""?true:false)';" & vbCrLf)
Response.Write("					document.forms[0].item(""tcnLimitH"").value ='")


Response.Write(mobjValues.StringToType(CStr(lclsTab_Am_Bil.nLimit_h), eFunctions.Values.eTypeData.etdDouble))


Response.Write("';" & vbCrLf)
Response.Write("				</" & "SCRIPT> " & vbCrLf)
Response.Write("				")


                End If

                mobjGrid.Columns("tcnPay_Concep").DefValue = CStr(lclsTab_Am_Bil.nPrestac)
                mobjGrid.Columns("tcnPay_Concep").Descript = lclsTab_Am_Bil.sPrestac
                mobjGrid.Columns("tcnPrestac").DefValue = CStr(lclsTab_Am_Bil.nPay_concep)
                mobjGrid.Columns("tcnPrestac").Descript = lclsTab_Am_Bil.sPay_concept
                mobjGrid.Columns("tcnDed_Type").DefValue = CInt(lclsTab_Am_Bil.nDed_type)
                mobjGrid.Columns("tcnDed_Percen").DefValue = CInt(lclsTab_Am_Bil.nDed_percen)
                mobjGrid.Columns("tcnDed_Amount").DefValue = CInt(lclsTab_Am_Bil.nDed_amount)
                mobjGrid.Columns("tcnDed_Quanti").DefValue = CInt(lclsTab_Am_Bil.nDed_quanti)
                mobjGrid.Columns("tcnIndem_Rate").DefValue = CInt(lclsTab_Am_Bil.nIndem_rate)
                mobjGrid.Columns("tcnLimit").DefValue = CInt(lclsTab_Am_Bil.nLimit)
                mobjGrid.Columns("tcnLimit_exe").DefValue = CInt(lclsTab_Am_Bil.nLimit_exe)
                mobjGrid.Columns("tcnPay_Concep").DefValue = CInt(lclsTab_Am_Bil.nPay_concep)
                mobjGrid.Columns("tcnTyplim").DefValue = CInt(lclsTab_Am_Bil.nTyplim)
                mobjGrid.Columns("tcnCount").DefValue = CInt(lclsTab_Am_Bil.nCount)
                mobjGrid.Columns("tcnPunish").DefValue = CInt(lclsTab_Am_Bil.nPunish)
                mobjGrid.Columns("tcsCaren_Type").DefValue = lclsTab_Am_Bil.sCaren_Type
                mobjGrid.Columns("tcnCaren_Dur").DefValue = CInt(lclsTab_Am_Bil.nCaren_Dur)
                mobjGrid.Columns("tcnDed_Quanti_2").DefValue = CInt(lclsTab_Am_Bil.NDED_QUANTI_2)
                mobjGrid.Columns("tcnIndem_Rate_2").DefValue = CInt(lclsTab_Am_Bil.NINDEM_RATE_2)
                mobjGrid.Columns("tcnLimit_2").DefValue = CInt(lclsTab_Am_Bil.NLIMIT_2)
                mobjGrid.Columns("tcnTyplim_2").DefValue = CInt(lclsTab_Am_Bil.NTYPLIM_2)
                mobjGrid.Columns("tcnCount_2").DefValue = CInt(lclsTab_Am_Bil.NCOUNT_2)
                mobjGrid.Columns("tcnLimit_exe_2").DefValue = CInt(lclsTab_Am_Bil.NLIMIT_EXE_2)
                mobjGrid.Columns("tcnPunish_2").DefValue = CInt(lclsTab_Am_Bil.NPUNISH_2)
                
			If lclsTab_Am_Bil.sOtherLim <> "1" And lclsTab_Am_Bil.sOtherLim <> "2" Then
				lclsTab_Am_Bil.sOtherLim = "2"
			End If
			mobjGrid.Columns("chksOtherLim").Checked = CShort(lclsTab_Am_Bil.sOtherLim)
			
			mobjGrid.sEditRecordParam = "nTariff=" & mintTariff & "&nCover=" & mlngCover & "&sAutRestit=' + self.document.forms[0].chkAutRestit.value + '" & "&nLimitH=' + self.document.forms[0].tcnLimitH.value + '" & "&nRole=" & mintRole & "&sClient=" & mstrClient & "&sIllness=" & mstrIllness & "&nGroup=" & mintGroup & "&nModulec=" & mintModulec & "&bCreHeader=' + self.document.forms[0].hddbCreHeader.value + '" & "&sOtherLim=" & lclsTab_Am_Bil.sOtherLim
			Response.Write(mobjGrid.DoRow)
			
			lintIndex = lintIndex + 1
		Next lclsTab_Am_Bil
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lstrBoolean = "false"
	
	If Not lblnExist Then
		lstrBoolean = "true"
	End If
	
	Response.Write("<SCRIPT>self.document.forms[0].hddbCreHeader.value=" & lstrBoolean & "</" & "Script>")
	
	If Not lblnExist And Not Session("bQuery") Then
		'+ Solamente para el caso de Certificado.
		If Session("nCertif") > 0 Then
			'+ Se verifica si existe información para la póliza matriz (Póliza matriz -> Certificado=0).
			If lclsTab_Am_Bil.valExistsTab_am_bil(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), 0, mintGroup, mintModulec, mlngCover, mintTariff, mintRole, mstrClient, mstrIllness, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("dEffecdate")) Then
				Response.Write(mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "InitialValues()"))
			End If
		End If
	End If
	lcolTab_Am_Bil = Nothing
	lclsTab_Am_Bil = Nothing
End Sub

'% insPreAM003Upd: carga los valores de la página AM003
'--------------------------------------------------------------------------------------------
Private Sub insPreAM003Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsAM003 As ePolicy.ValPolicySeq
	Dim lblnPost As Boolean
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		lclsAM003 = New ePolicy.ValPolicySeq
		
		With Request
			lblnPost = lclsAM003.insPostAM003Upd("Delete", Session("nTransaction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPay_Concep"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("dNulldate"), 0, 0, 0, 0, 0, 0, 0, 0, Session("nUsercode"), vbNullString, mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sClient"), .QueryString.Item("sIllness"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble, True), 0, mobjValues.StringToType(.QueryString.Item("nPrestac"), eFunctions.Values.eTypeData.etdDouble), 0, 0, CStr(0), 0, 0, 0, 0, 0, 0, 0, 0, CStr(0), Session("sPoliType"), False)
		End With
		lclsAM003 = Nothing
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valPolicySeq.aspx", "AM003", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AM003")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

'+ Seteo de los objetos principales y globales de la transacción
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




	
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "AM003", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	End If
End With
mobjMenu = Nothing
Call insReaInitial()
Call insDefaultValues()
%>

	
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:48 $|$$Author: Nvaplat61 $"
	
	var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
//% insReload: Se encarga de recargar la página al seleccionar cualquier valor de los campos del encabezado del grid.
//-------------------------------------------------------------------------------------------
function insReload(Field,ltarif,lCover,lRole,lIllness){
//-------------------------------------------------------------------------------------------
    var lstrAutoRestit = (self.document.forms[0].chkAutRestit.checked==true?'1':'2')
    var lstrQuery
    var lblnChange
    var lstrstring  = ""
		
	lblnChange = false;
	with (self.document.forms[0]) {
		
		if (nMainAction == 401)
			lstrQuery = "&nTariff=" + ltarif;
		else
			lstrQuery = "&nTariff=" + cbeTariff.value;
			
			
//+ Caso en que el cliente esté visible
		if(typeof(valClient)!='undefined'){
			if (mstrClientChange!=valClient.value) {
			    mstrClientChange = valClient.value;
			    lblnChange = true;
			}
			lstrQuery = lstrQuery + "&sClient=" + valClient.value
		}else
			lstrQuery = lstrQuery + "&sClient="
				
//+ Caso en que el grupo esté visible
		if(typeof(valInsuredGr)!='undefined'){			
			if (mintGroupChange!=valInsuredGr.value) {
			    mintGroupChange = valInsuredGr.value;
			    lblnChange = true;
			}
			lstrQuery = lstrQuery + "&nGroup=" + (valInsuredGr.value==''?0:valInsuredGr.value)
		} else
			lstrQuery = lstrQuery + "&nGroup=0"
		
//+ Caso en que el módulo esté visible
		if(typeof(valModulec)!='undefined'){
			if (mintModulecChange!=valModulec.value) {
			    lblnChange = true;
			    mintModulecChange = valModulec.value;
			}
			lstrQuery = lstrQuery + "&nModulec=" + valModulec.value
		} else
			lstrQuery = lstrQuery + "&nModulec=0"
		

		
		if (nMainAction == 401){
			if (mlngCoverChange!=lCover ||
			   mintTariffChange!=ltarif ||
		       mintRoleChange!=lRole ||
		       mstrIllnessChange!=lIllness) {
				lblnChange = true;
				mlngCoverChange = lCover;
				mintTariffChange = ltarif;
				mintRoleChange = lRole;
				mstrIllnessChange = lIllness;
		}
		}
		else {
			if (mlngCoverChange!=valCover.value ||
		       mintTariffChange!=cbeTariff.value ||
		       mintRoleChange!=valRole.value ||
		       mstrIllnessChange!=valIllness.value) {
				lblnChange = true;
				mlngCoverChange = valCover.value;
				mintTariffChange = cbeTariff.value;
				mintRoleChange = valRole.value;
				mstrIllnessChange = valIllness.value;
		}
		}
		
		if (lblnChange==true) {

            self.document.forms[0].target = 'fraGeneric';
            UpdateDiv('lblWaitProcess', '<MARQUEE>Procesando, por favor espere...</MARQUEE>', '');


	        lstrstring += document.location.href;
		    if (nMainAction == 401)
			//lstrQuery = lstrQuery + "&nCover=" + lCover + "&sAutRestit=" + lstrAutoRestit + "&nRole=" + lRole + "&sIllness=" + lIllness + "&nLimitH=" + tcnLimitH.value
			lstrQuery = lstrQuery + "&nCover=" + lCover + "&sAutRestit=" + lstrAutoRestit + "&nRole=" + lRole + "&sIllness=" + "00000" + "&nLimitH=" + tcnLimitH.value
			else
			lstrQuery = lstrQuery + "&nCover=" + valCover.value + "&sAutRestit=" + lstrAutoRestit + "&nRole=" + valRole.value + "&sIllness=" + valIllness.value + "&nLimitH=" + tcnLimitH.value
			lstrstring = lstrstring.replace(/Reload=1*/,'Reload=')
			ltrstring = lstrstring.replace(/&Type=.*/,'')			
            ltrstring = lstrstring.replace(/&sClient=.*/,'')
		    lstrstring = lstrstring.replace(/&nGroup=.*/,'')
		    lstrstring = lstrstring.replace(/&nModulec=.*/,'')
            lstrstring = lstrstring.replace(/&nCover=.*/,'')
            lstrstring = lstrstring.replace(/&nTariff=.*/,'')
		    document.location =  lstrstring + lstrQuery

	    }
    }
}

//%DisabledItem: Deshabilita los campos de cuando es asegurado
//-------------------------------------------------------------------------------------------
function DisabledItem(){
//-------------------------------------------------------------------------------------------
	
	var Type='<%=mstrType%>';
    var sOtherLim = self.document.forms[0].chkOtherLim.value
 	with(self.document.forms[0]){		
		if (Type=="PopUp"){
			if (sOtherLim == '1'){
                tcNDED_QUANTI_2.disabled=false;
                tcNINDEM_RATE_2.disabled=false;
                tcNLIMIT_2.disabled=false;
                tcNTYPLIM_2.disabled=false;
                tcNCOUNT_2.disabled=false;
                tcNLIMIT_EXE_2.disabled=false;
                tcNPUNISH_2.disabled=false;
			}
			else{
                tcNDED_QUANTI_2.disabled=true
                tcNINDEM_RATE_2.disabled=true;
                tcNLIMIT_2.disabled=true;
                tcNTYPLIM_2.disabled=true;
                tcNCOUNT_2.disabled=true;
                tcNLIMIT_EXE_2.disabled=true;
                tcNPUNISH_2.disabled=true;
			}	
		}
	}
}

//% ReaGroupPrest: Lee Agrupación de Prestación para el concepto elegido
//-------------------------------------------------------------------------------------------
function ReaGroupPrest(){
//-------------------------------------------------------------------------------------------
	self.document.forms[0].tcnPay_Concep.value = self.document.forms[0].tcnPrestac_nGroup_Pres.value;    
}

//%ChangeRole: cambia el rol según el asegurado seleccionado
//-----------------------------------------------------------------------------------------------------------------------------------
function ChangeRole(Field){
//-----------------------------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
		if (valClient.value!='')
			valRole.value = valClient_nRole.value
    }    
}

//%insmodulec: Habilitado el campo valCover en caso de que no se hayan recuperado modulos
//-----------------------------------------------------------------------------------------------------------------------------------
function insmodulec(){
//-----------------------------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
//+ Caso en que el módulo esté visible
		if(typeof(valModulec)!='undefined')
			if(self.document.forms[0].valModulec.length==1)
				self.document.forms[0].valCover.disabled = false
	}
}

//%insChangeTyplim: Deshabilitado el campo tcnCount en caso de que el tipo límite no sea "Cantidad de veces"
//-----------------------------------------------------------------------------------------------------------------------------------
function insChangeTyplim(field){
//-----------------------------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
	    if (tcnTyplim.value!= 7){
	        tcnCount.disabled = true
	        tcnCount.value = ''
	    }
	    else
	        tcnCount.disabled = false
	}   
}

//% InitialValues: se inicializa el grid de la transacción, con los datos definidos en el diseñador
//--------------------------------------------------------------------------------------------
function InitialValues(){
//--------------------------------------------------------------------------------------------
	var lstrQuery
	with (document.forms[0]) {
		lstrQuery = "nTariff=" + cbeTariff.value + "&nCover=" + valCover.value + "&nRole=" + valRole.value + "&sIllness=" + valIllness.value;
		
//+ Caso en que el grupo esté visible
			if(typeof(valInsuredGr)!='undefined')				
				lstrQuery = lstrQuery + "&nGroup=" + (valInsuredGr.value==''?0:valInsuredGr.value);
			else
				lstrQuery = lstrQuery + "&nGroup=0";
				
//+ Caso en que el módulo esté visible
			if(typeof(valModulec)!='undefined')
				lstrQuery = lstrQuery + "&nModulec=" + valModulec.value;
			else
				lstrQuery = lstrQuery + "&nModulec=0";
			
//+ Caso en que el cliente esté visible
			if(typeof(valClient)!='undefined')
				lstrQuery = lstrQuery + "&sClient=" + valClient.value
			else
				lstrQuery = lstrQuery + "&sClient="
				
			lstrQuery = lstrQuery + "&bCreHeader=" + hddbCreHeader.value;
	
		insDefValues("Tab_am_bil", lstrQuery, '/VTimeNet/Policy/PolicySeq')
	}
}

//% insActiveFields: Se encarga de activar campos del límite combinado
//--------------------------------------------------------------------------------------------
function insActiveFields(){
//--------------------------------------------------------------------------------------------	
	var lstrotherlim = (self.document.forms[0].chksotherlim.checked==true?'1':'2');

    with (self.document.forms[0]) {        
       tcnDed_Quanti_2.disabled=(lstrotherlim=='2');
       tcnDed_Quanti_2.value='';
       
       tcnIndem_Rate_2.disabled=(lstrotherlim=='2');
       tcnIndem_Rate_2.value='';
       
	   tcnLimit_2.disabled=(lstrotherlim=='2');
	   tcnLimit_2.value='';

       tcnTyplim_2.disabled=(lstrotherlim=='2');
       tcnTyplim_2.value='';

       tcnCount_2.disabled=(lstrotherlim=='2');
       tcnCount_2.value='';

       tcnLimit_exe_2.disabled=(lstrotherlim=='2');
       tcnLimit_exe_2.value='';

       tcnPunish_2.disabled=(lstrotherlim=='2');
       tcnPunish_2.value='';
	    
	 }

}

//% insCreHeader: Se encarga de crear información de la tabla maestra (tab_am_bab)
//--------------------------------------------------------------------------------------------
function insCreHeader(){
//--------------------------------------------------------------------------------------------
	var lstrAutoRestit = (self.document.forms[0].chkAutRestit.checked==true?'1':'2')
	var lstrQuery

	with (self.document.forms[0]) {
		if (cbeTariff.value>0) {
//+ Se verifica si hubo cambio de valores en los campos Restitución automática o límite de pago.
			if (lstrAutoRestit!=mstrAut_restitChange ||
				tcnLimitH.value!=mdblLimitHChange) {
				mstrAut_restitChange = lstrAutoRestit;
				mdblLimitHChange = tcnLimitH.value;
				lstrQuery = "nTariff=" + cbeTariff.value + "&nLimitH=" + tcnLimitH.value + "&sAutRestit=" + lstrAutoRestit + "&nCover=" + valCover.value + "&nRole=" + valRole.value + "&sIllness=" + valIllness.value;
				//lstrQuery = "nTariff=" + cbeTariff.value + "&nLimitH=" + tcnLimitH.value + "&sAutRestit=" + lstrAutoRestit + "&nCover=" + valCover.value + "&nRole=" + valRole.value;
//+ Caso en que el grupo esté visible
				if(typeof(valInsuredGr)!='undefined')
					lstrQuery = lstrQuery + "&nGroup=" + (valInsuredGr.value==''?0:valInsuredGr.value);
				else
					lstrQuery = lstrQuery + "&nGroup=0";
				
//+ Caso en que el módulo esté visible
				if(typeof(valModulec)!='undefined')
					lstrQuery = lstrQuery + "&nModulec=" + valModulec.value;
				else
					lstrQuery = lstrQuery + "&nModulec=0";
			
//+ Caso en que el cliente esté visible
				if(typeof(valClient)!='undefined')
					lstrQuery = lstrQuery + "&sClient=" + valClient.value
				else
					lstrQuery = lstrQuery + "&sClient="
					
//+ Caso en que la enfermedad venga en 0
//+				if(typeof(valIllness)!=0)
//+					lstrQuery = lstrQuery + "&sIllness=" + valIllness.value
//+				else
//+					lstrQuery = lstrQuery + "&sIllness=00000"

				insDefValues("creTab_am_bab", lstrQuery, '/VTimeNet/Policy/PolicySeq')
			}
		}
	}
}

</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ACTION="valPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" ID=AM003 NAME=AM003>
<%
insDefineHeader()
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreAM003()
Else
	insPreAM003Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
If Not mblnDisabled And Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>")
	Response.Write("insmodulec();")
	Response.Write("</SCRIPT>")
End If
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.02
Call mobjNetFrameWork.FinishPage("AM003")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




