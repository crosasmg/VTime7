<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'+ Se define la variable para almacenar el QueryString de los campos que existen en el encabezado de la transacción
Dim mstrQueryString As String

'- Variable para el manejo de los errores de la página, devueltos por insvalSequence
Dim mstrErrors As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMantLife As Object

'- Variables para validaciones y grabado en la tabla
Dim mlngBranch As String
Dim mlngProduct As String
Dim mlngModulec As String
Dim mlngCover As String
Dim mdtmEffecdate As Object
Dim mlngRole As String


'% insValMantLife: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantLife() As String
	Dim nAction As Integer
	'--------------------------------------------------------------------------------------------
	Dim lintModulec As Byte
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ MVI706: Tabla del Tope de capital por evaluacion
		Case "MVI706"
			If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				mobjMantLife = New eBranches.Leg
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.insValMVI706_K(.QueryString("nMainAction"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					Else
						insValMantLife = mobjMantLife.insValMVI706(.QueryString("WindowType"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeAuxCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapitalI"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalF"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("ReloadIndex"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End With
			End If
			
			'+ MVI706: Tabla del Tope de capital por evaluacion
		Case "MVI8000"
			If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				mobjMantLife = New eBranches.Guar_saving_rent
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						
						insValMantLife = mobjMantLife.insValMVI8000(.QueryString("nZone"), 0, 0, 0, 0, 0, 0, mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
						
						If insValMantLife = "" Then
							Session("dEffecdate_MVI8000") = Request.Form.Item("tcdEffecdate")
						End If
						
					Else
						nAction = 1
						If .QueryString.Item("Action") <> "Add" Then
							nAction = 2
						End If
						insValMantLife = mobjMantLife.insValMVI8000(.QueryString("nZone"), mobjValues.StringToType(.Form.Item("tcnBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnGuarSav_ValIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGuarSav_ValEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGuarSav"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGuarSav_Year"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate_MVI8000"), eFunctions.Values.eTypeData.etdDate), nAction)
					End If
				End With
				mobjMantLife = Nothing
			End If
			
			'+ MVA645: Tabla de comisiones de Vida Activa
		Case "MVA645"
			With Request
				mobjMantLife = New eBranches.tab_comm_al
				If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") = "306" Then
					insValMantLife = mobjMantLife.insValMVA645_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valComtabli"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valInterm_typ"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valSellChanel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), 1, mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSellChannel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
						If CDbl(.QueryString.Item("nZone")) = 1 Then
							
							insValMantLife = mobjMantLife.insValMVA645_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valComtabli"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valInterm_typ"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valSellChanel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), 2, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
						Else
							insValMantLife = mobjMantLife.insValMVA645(.QueryString("Action"), mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nSellChannel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQPB"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True))
						End If
					End If
				End If
			End With
			
			'+ Descuentos por primas básicas
		Case "MVA600"
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					mobjMantLife = New eBranches.Disc_pb
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVA600_K("MVA600", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIntermtyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
						
						
					Else
						insValMantLife = mobjMantLife.InsValMVA600("MVA600", .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQPB"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
				End If
			End With
			
			'+ Rating por productos.
		Case "MVA740"
			mobjMantLife = New eBranches.Ratings
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVA740_K("MVA740", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
						
						
					Else
						insValMantLife = mobjMantLife.InsValMVA740("MVA740", .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRating"), eFunctions.Values.eTypeData.etdDouble, True))
						
						
						
					End If
				End If
			End With
			
			'+ Tarifa de vida tradicional
		Case "MVI729"
			mobjMantLife = New eBranches.Tar_tralife
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.insvalMVI729_K("MVI729", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkSmoking"))
					Else
						insValMantLife = mobjMantLife.insvalMVI729("MVI729", .QueryString("Action"), Session("nBranch"), Session("nProduct"), Session("nModulec"), Session("nCover"), Session("dEffecdate"), Session("sSmoking"), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInipercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInipaycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRatewomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremwomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRatemen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremmen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddTyperisk"), eFunctions.Values.eTypeData.etdLong))
					End If
				End If
			End With
			
			'+ Tabla de capitales crecientes
		Case "MVI757"
			mobjMantLife = New eBranches.Cap_crelife
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						If .Form.Item("valModulec") = vbNullString Then
							lintModulec = 0
						Else
							lintModulec = mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble)
						End If
						
						insValMantLife = mobjMantLife.InsValMVI757_K("MVI757", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), lintModulec, mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					Else
						insValMantLife = mobjMantLife.InsValMVI757("MVI757", .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnDuration"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ MVA695: Tabla de control de prima mínima
		Case "MVA695"
			mobjMantLife = New eBranches.Ctrol_premin
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVA695_K("MVA695", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
						
						
					Else
						insValMantLife = mobjMantLife.InsValMVA695("MVA695", .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True))
						
						
						
					End If
				End If
			End With
			
			'+ Tabla de capitales del seguro escolar/universitario
		Case "MVI575"
			mobjMantLife = New eBranches.Cap_educind
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVI575_K("MVI575", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					Else
						insValMantLife = mobjMantLife.InsValMVI575("MVI575", .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapschool"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCaphscho"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+MVI807: Tabla de gastos y comisiones
		Case "MVI807"
			mobjMantLife = New eBranches.Res_cost
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVI807_k("MVI807", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					Else
						insValMantLife = mobjMantLife.InsValMVI807("MVI807", .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPeriod"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnRec_sale"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRec_comm"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
				End If
			End With
			
		Case "MVI771"
			With Request
				mobjMantLife = New eBranches.Tar_schooltrad
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVI771_K("MVI771", mobjValues.StringToType(.QueryString.Item("nmainaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					Else
						insValMantLife = mobjMantLife.InsValMVI771_Upd("MVI771", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAge_insu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_Child"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPeriod_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dNulldate"), eFunctions.Values.eTypeData.etdDate))
					End If
				End If
			End With
			
			'+ Tabla de Tarifas de VidaActiva
		Case "MVA606"
			mobjMantLife = New eBranches.Tar_ActLife
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVA606_K("MVA606", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optTypeTab"), .Form.Item("chkSmoking"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					Else
						insValMantLife = mobjMantLife.InsValMVA606("MVA606", .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble, True), Session("sTypeTab"), Session("sSmoking"), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnRateWomen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremWomen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRateMen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremMen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
				End If
			End With
			
			'+ Tabla de descuento por asegurado superior
		Case "MVI805"
			mobjMantLife = New eBranches.Disc_riskInsu
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVI805_K("MVI805", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					Else
						insValMantLife = mobjMantLife.InsValMVI805_Upd("MVI805", .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapital_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ Tabla de rentabilidad mensual
		Case "MVA619"
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					mobjMantLife = New eBranches.Tab_Interest
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.InsValMVA619_K("MVA619", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTypeInvest"), eFunctions.Values.eTypeData.etdDouble, True), Today)
					Else
						insValMantLife = mobjMantLife.InsValMVA619("MVA619", .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nTypeInvest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnWarInt"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
				End If
			End With
			
			'+ MVI772: Tabla de tarifas del seguro en familia (SEF)
		Case "MVI772"
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					mobjMantLife = New eBranches.Tar_sef
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantLife = mobjMantLife.insvalMVI772_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True))
					Else
						insValMantLife = mobjMantLife.insvalMVI772Upd(.QueryString("sCodispl"), .QueryString("Action"), .QueryString("nBranch"), .QueryString("nProduct"), .QueryString("dEffecdate"), .QueryString("nModulec"), .QueryString("nCover"), .QueryString("nRole"), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapital_init"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapital_end"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
				End If
			End With
			
			'+ MVI630: Tarifa de recargo por actividad
		Case "MVI630"
			mobjMantLife = New eBranches.Tar_activity
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantLife = mobjMantLife.insvalMVI630_K(.QueryString("nMainAction"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantLife = mobjMantLife.insvalMVI630Upd(.QueryString("Action"), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valSpeciality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnTyperec"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
				End If
			End With
			
			'+ Tabla de tramos de edad para la cotización de vida
		Case "MVI693"
			With Request
				mobjMantLife = New eBranches.Age_collect
				If .QueryString.Item("nZone") = "1" Then
					insValMantLife = mobjMantLife.insvalMVI693_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					insValMantLife = mobjMantLife.insvalMVI693(.QueryString("WindowType"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAct_Perc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndAge"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ Parámetros de vida colectivo (Educacional)
		Case "MVI670"
			With Request
				mobjMantLife = New eBranches.Level_param
				If .QueryString.Item("nZone") = "1" Then
					insValMantLife = mobjMantLife.insvalMVI670_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If Request.QueryString.Item("WindowType") = "PopUp" Then
						insValMantLife = mobjMantLife.insvalMVI670(.QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeLevel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_Father"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					End If
				End If
			End With
			
			'+ MVI816 Tabla de exámenes solicitados aleatoriamente 
		Case "MVI816"
			With Request
				mobjMantLife = New eBranches.Crit_sort
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantLife = mobjMantLife.insValMVI816(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Request.Form.Item("cbeCrthecni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRandom"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("hddsSolic"), mobjValues.StringToType(Request.Form.Item("hddnCount"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeStatregt"))
				End If
			End With
			
			'+ MVI772: Tabla de capitales de vida
		Case "MVI773"
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					mobjMantLife = New eBranches.Tab_Capital
					
					mlngBranch = .Form.Item("cbeBranch")
					mlngProduct = .Form.Item("valProduct")
					mlngModulec = .Form.Item("valModulec")
					mlngCover = .Form.Item("valCover")
					mdtmEffecdate = .Form.Item("tcdEffecdate")
					mlngRole = .Form.Item("valRole")
					
					If mlngBranch = "" Then
						mlngBranch = .QueryString.Item("nBranch")
					End If
					
					If mlngProduct = "" Then
						mlngProduct = .QueryString.Item("nProduct")
					End If
					
					If mlngModulec = "" Then
						mlngModulec = .QueryString.Item("nModulec")
					End If
					
					If mlngCover = "" Then
						mlngCover = .QueryString.Item("nCover")
					End If
					
					If mdtmEffecdate = eRemoteDB.Constants.DTMNULL Then
						mdtmEffecdate = .QueryString.Item("dEffecdate")
					End If
					
					If mlngRole = "" Then
						mlngRole = .QueryString.Item("nRole")
					End If
					
					insValMantLife = mobjMantLife.insvalMVI773("MVI773", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nZone"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(mlngBranch, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(mlngProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngModulec, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCover, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mdtmEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(mlngRole, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInipercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndpercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInipaycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndpaycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremanual"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeSexclien"), .Form.Item("chkSmoking"))
					
				End If
			End With
			
			'+ MVI817
		Case "MVI817"
			With Request
				mobjMantLife = New eBranches.Funds_Switch
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantLife = mobjMantLife.InsValMVI817Upd(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valFromFunds"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valToFunds"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeStatregt"), eFunctions.Values.eTypeData.etdLong))
				End If
			End With
			
		Case Else
			insValMantLife = "insValMantLife: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantLife: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantLife() As Boolean
	Dim nAction As Byte
	Dim nProduct As Byte
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ MVI706: Tabla del Tope de capital por evaluacion
		Case "MVI8000"
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				lblnPost = True
			Else
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						lblnPost = True
					Else
						
						mobjMantLife = New eBranches.Guar_saving_rent
						nAction = 1
						
						If .QueryString.Item("Action") <> "Add" Then
							nAction = 2
						End If
						If mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong) = eRemoteDB.Constants.intNull Then
							nProduct = 0
						Else
							nProduct = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong)
						End If
						
						lblnPost = mobjMantLife.insPostMVI8000(nAction, mobjValues.StringToType(.Form.Item("tcnBranch"), eFunctions.Values.eTypeData.etdLong), nProduct, mobjValues.StringToType(.Form.Item("tcnGuarSav_ValIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGuarSav_Valend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGuarSav"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnGuarSav_Year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate_MVI8000"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
						
						mobjMantLife = Nothing
					End If
				End With
			End If
			
			'+ MVI706: Tabla del Tope de capital por evaluacion
		Case "MVI706"
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				lblnPost = True
			Else
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
						lblnPost = True
					Else
						mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
						lblnPost = mobjMantLife.insPostMVI706(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapitalI"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapitalF"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountbas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFact"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountmax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAuxCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End With
			End If
			
			'+ MVA645: Tabla de Comisiones de VidActiva
		Case "MVA645"
			With Request
				'+ Duplicar tabla Popup       
				
				If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") = "306" Then
					
					lblnPost = mobjMantLife.insPostDuplicaMVA645(mobjValues.StringToType(.Form.Item("valComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valInterm_typ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valSellChanel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSellChannel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
						If CDbl(.QueryString.Item("nZone")) = 1 Then
							Session("nComtabli") = .Form.Item("valComtabli")
							Session("nIntertyp") = .Form.Item("valInterm_typ")
							Session("nSellChannel") = .Form.Item("valSellChanel")
							Session("nWay_pay") = .Form.Item("valWay_pay")
							Session("nBranch") = .Form.Item("cbeBranch")
							Session("nProduct") = .Form.Item("valProduct")
							Session("nModulec") = .Form.Item("valModulec")
							Session("nCover") = .Form.Item("valCover")
							Session("dEffecdate") = .Form.Item("tcdEffecdate")
							
							lblnPost = True
						Else
							lblnPost = mobjMantLife.insPostMVA645(.QueryString("Action"), mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSellChannel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQPB"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble),  , mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						End If
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+ Descuentos por primas básicas
		Case "MVA600"
			With Request
				lblnPost = True
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&nIntertyp=" & .Form.Item("cbeIntermtyp") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nModulec=" & .Form.Item("valModulec") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
					Else
						lblnPost = mobjMantLife.InsPostMVA600(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQPB"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						mstrQueryString = "&nIntertyp=" & .QueryString.Item("nIntertyp") & "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nModulec=" & .QueryString.Item("nModulec") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
					End If
				End If
			End With
			
			'+ Rating por productos.
		Case "MVA740"
			lblnPost = True
			With Request
				If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						Session("nBranch") = .Form.Item("cbeBranch")
						Session("nProduct") = .Form.Item("valProduct")
						Session("dEffecdate") = .Form.Item("tcdEffecdate")
					Else
						lblnPost = mobjMantLife.InsPostMVA740(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRating"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ Tabla de tarifa de vida tradicional
		Case "MVI729"
			lblnPost = True
			With Request
				If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						Session("nBranch") = .Form.Item("cbeBranch")
						Session("nProduct") = .Form.Item("valProduct")
						If .Form.Item("valModulec") = vbNullString Then
							Session("nModulec") = 0
						Else
							Session("nModulec") = .Form.Item("valModulec")
						End If
						Session("nCover") = .Form.Item("valCover")
						Session("dEffecdate") = .Form.Item("tcdEffecdate")
						Session("sSmoking") = .Form.Item("chkSmoking")
						mstrQueryString = "&nTyperisk=" & .Form.Item("cbeTyperisk")
					Else
						lblnPost = mobjMantLife.InsPostMVI729(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("sSmoking"), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInipercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInipaycov"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnRatewomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremwomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRatemen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremmen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valType_tar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndpercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndpaycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddTyperisk"), eFunctions.Values.eTypeData.etdLong))
						mstrQueryString = "&nTyperisk=" & .Form.Item("hddTyperisk")
					End If
				End If
			End With
			
			'+ Tabla de capitales crecientes
		Case "MVI757"
			lblnPost = True
			With Request
				If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						Session("nBranch") = .Form.Item("cbeBranch")
						Session("nProduct") = .Form.Item("valProduct")
						If .Form.Item("valModulec") = vbNullString Then
							Session("nModulec") = 0
						Else
							Session("nModulec") = .Form.Item("valModulec")
						End If
						Session("nCover") = .Form.Item("valCover")
						Session("dEffecdate") = .Form.Item("tcdEffecdate")
					Else
						lblnPost = mobjMantLife.InsPostMVI757(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDuration"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ MVA695: Tabla de control de prima mínima
		Case "MVA695"
			lblnPost = True
			With Request
				If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						Session("nBranch") = .Form.Item("cbeBranch")
						Session("nProduct") = .Form.Item("valProduct")
						Session("dEffecdate") = .Form.Item("tcdEffecdate")
					Else
						lblnPost = mobjMantLife.InsPostMVA695(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ Tarifa de capitales del seguro escolar/universitario
		Case "MVI575"
			lblnPost = True
			With Request
				If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
					Else
						lblnPost = mobjMantLife.InsPostMVI575(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapschool"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCaphscho"), eFunctions.Values.eTypeData.etdDouble), Session("dNulldate"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
					End If
				End If
			End With
			
			'+MVI807: Tabla de gastos y comisiones
		Case "MVI807"
			lblnPost = True
			With Request
				If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
					Else
						lblnPost = mobjMantLife.InsPostMVI807(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPeriod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.QueryString.Item("dEffecdate")), mobjValues.StringToType(.Form.Item("tcnRec_sale"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRec_comm"), eFunctions.Values.eTypeData.etdDouble, True), Session("dNulldate"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
						
					End If
				End If
			End With
			
		Case "MVI771"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
				Else
					lblnPost = mobjMantLife.insPostMVI771(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge_insu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_Child"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPeriod_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dNulldate"), eFunctions.Values.eTypeData.etdDate))
					mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate")
				End If
			End With
			
			'+ MVA606: Tabla de Tarifas de Vida Activa
		Case "MVA606"
			
			If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					With Request
						Session("nBranch") = .Form.Item("cbeBranch")
						Session("nProduct") = .Form.Item("valProduct")
						If .Form.Item("valModulec") = vbNullString Then
							Session("nModulec") = 0
						Else
							Session("nModulec") = .Form.Item("valModulec")
						End If
						Session("nCover") = .Form.Item("valCover")
						Session("sTypeTab") = .Form.Item("optTypeTab")
						Session("sSmoking") = .Form.Item("chkSmoking")
						Session("dEffecdate") = .Form.Item("tcdEffecdate")
						
						lblnPost = True
					End With
				Else
					With mobjValues
						lblnPost = mobjMantLife.InsPostMVA606(Request.QueryString.Item("Action"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeTab"), Session("sSmoking"), .StringToType(Request.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.Form.Item("tcnRateWomen"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnPremWomen"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnRateMen"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnPremMen"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End With
				End If
			Else
				lblnPost = True
			End If
			
			'+ Tabla de descuento por asegurado superior
		Case "MVI805"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nBranch") = .Form.Item("cbeBranch")
					Session("nProduct") = .Form.Item("valProduct")
					Session("dEffecdate") = .Form.Item("tcdEffecdate")
				Else
					lblnPost = mobjMantLife.insPostMVI805(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapital_Init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ Tabla de Rentabilidad Mensual de VidaActiva
		Case "MVA619"
			lblnPost = True
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nModulec=" & .Form.Item("valModulec") & "&nTypeInvest=" & .Form.Item("cbeTypeInvest")
					Else
						lblnPost = mobjMantLife.InsPostMVA619(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypeInvest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnWarInt"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						
						mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nModulec=" & .QueryString.Item("nModulec") & "&nTypeInvest=" & .QueryString.Item("nTypeInvest")
					End If
				End If
			End With
			
			'+ MVI772: Tabla de tarifas del seguro en familia (SEF)
		Case "MVI772"
			lblnPost = True
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nModulec=" & .Form.Item("valModulec") & "&nCover=" & .Form.Item("valCover") & "&nRole=" & .Form.Item("valRole")
					Else
						mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nModulec=" & .QueryString.Item("nModulec") & "&nCover=" & .QueryString.Item("nCover") & "&nRole=" & .QueryString.Item("nRole")
						
						lblnPost = mobjMantLife.insPostMVI772(.QueryString("Action"), .QueryString("nBranch"), .QueryString("nProduct"), .QueryString("dEffecdate"), .QueryString("nRole"), .QueryString("nCover"), .QueryString("nModulec"), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnType_tar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTax"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					End If
				End If
			End With
			
			'+ MVI630: Tarifa de recargo por actividad
		Case "MVI630"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nCover=" & .Form.Item("valCover") & "&nTyperec=1"
					
					lblnPost = True
				Else
					lblnPost = True
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantLife.insPostMVI630(.QueryString("Action"), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valSpeciality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnTyperec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ Tabla de tramos de edad para la cotización de vida        
		Case "MVI693"
			lblnPost = True
			With Request
				mstrQueryString = "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nBranch=" & .Form.Item("cbeBranch")
				If .QueryString.Item("WindowType") = "PopUp" Then
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						lblnPost = mobjMantLife.inspostMVI693(.QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnInitAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAct_Perc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ Parámetros de vida colectivo (Educacional)
		Case "MVI670"
			lblnPost = True
			With Request
				mstrQueryString = "&nProduct=" & mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nBranch=" & mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
				
				If .QueryString.Item("WindowType") = "PopUp" Then
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						lblnPost = mobjMantLife.inspostMVI670(.QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeLevel"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcdEffecdate"), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_Father"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ MVI816 Actualización exámenes solicitados aleatoriamente
		Case "MVI816"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantLife.insPostMVI816Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbeCrthecni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRandom"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("hddsSolic"), mobjValues.StringToType(Request.Form.Item("hddnCount"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = True
				End If
			End With
			
			'+ MVI772: Tabla de capitales de vida
		Case "MVI773"
			
			lblnPost = True
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nModulec=" & .Form.Item("valModulec") & "&nCover=" & .Form.Item("valCover") & "&nRole=" & .Form.Item("valRole")
					Else
						mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nModulec=" & .QueryString.Item("nModulec") & "&nCover=" & .QueryString.Item("nCover") & "&nRole=" & .QueryString.Item("nRole")
						
						lblnPost = mobjMantLife.insPostMVI773(.QueryString("Action"), mobjValues.StringToType(mlngBranch, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(mlngProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngModulec, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCover, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mdtmEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(mlngRole, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInipercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndpercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInipaycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndpaycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremanual"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddId"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeSexclien"), .Form.Item("chkSmoking"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						
					End If
				End If
			End With
			'+ MVI817
		Case "MVI817"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantLife.insPostMVI817Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valFromFunds"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valToFunds"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbeStatregt"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
				Else
					lblnPost = True
				End If
			End With
	End Select
	insPostMantLife = lblnPost
End Function

</script>
<%Response.Expires = -1
mstrCommand = "&sModule=Maintenance&sProject=MantLife&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
    <%=mobjValues.StyleSheet()%>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 20/10/03 12:40 $|$$Author: Nvaplat18 $"
</SCRIPT>
</HEAD>
<BODY>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantLife
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantLifeError"",660,330);")
		
		
		'+ Solo si la acción es de duplicar para la tx MVA645
		If Request.QueryString.Item("sCodispl") = "MVA645" And Request.QueryString.Item("nMainAction") = "306" Then
			.Write("self.history.go(-1);")
		Else
			.Write("document.location.href='/VTimeNet/common/blank.htm';")
		End If
		
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantLife Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					'+Si la accion es duplicar, entonces se levanta PopUp 						
					If Request.QueryString.Item("sCodispl") = "MVA645" And Request.QueryString.Item("nMainAction") = "306" Then
						Response.Write("<SCRIPT> ShowPopUp('MVA645Dup.aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&Type=PopUp" & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "','Componentes','600','300','no','','60','80');</SCRIPT>" & vbCrLf)
					Else
						Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>insReloadTop(true);</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
				End If
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "MVI630"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.Form.Item("hddnBranch") & "&nProduct=" & Request.Form.Item("hddnProduct") & "&dEffecdate=" & Request.Form.Item("hdddEffecdate") & "&nCover=" & Request.Form.Item("hddnCover") & "&nTyperec=" & Request.Form.Item("hddnTyperec") & "'</SCRIPT>")
				Case "MVI693"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI693.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nAct_Perc=" & mobjValues.StringToType(Request.Form.Item("tcnAct_Perc"), eFunctions.Values.eTypeData.etdDouble) & "'</SCRIPT>")
				Case "MVI670"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI670.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "'</SCRIPT>")
					'+ MVI816: Tabla de exámenes solicitados aleatoriamente 
				Case "MVI816"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI816_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MVI817"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI817_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302&cbeOriginH=" & Request.QueryString.Item("cbeOriginH") & "&cbeFromFunds=" & Request.QueryString.Item("cbeFromFunds") & "'</SCRIPT>")
				Case "MVA645"
					If Request.QueryString.Item("nMainAction") = "306" Then
						Response.Write("<SCRIPT>top.opener.top.document.location.reload(); top.close();</SCRIPT>")
					Else
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
					End If
				Case Else
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
			End Select
		End If
	End If
End If
mobjValues = Nothing
mobjMantLife = Nothing
%>
</BODY>
</HTML>




