Option Strict Off
Option Explicit On
Public Class Move_Accpol
	'%-------------------------------------------------------%'
	'% $Workfile:: Move_Accpol.cls                          $%'
	'% $Author:: Pgarin                                     $%'
	'% $Date:: 24/08/06 10:56                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'   Column_Name                                 Type      Length  Prec  Scale Nullable
	'   ---------------------- --------------- - -------- ------- ----- ------ --------
	Public sCertype As String ' CHAR           1              No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Double ' NUMBER        22    10      0 No
	Public nCertif As Double ' NUMBER        22    10      0 No
	Public nIdmov As Integer ' NUMBER        22     5      0 No
	Public nTypemove As eMoveAccPolType ' NUMBER        22     5      0 No
	Public nAmount As Double ' NUMBER        22    10      2 No
	Public nCredit As Double ' NUMBER        22    10      2 No
	Public nDebit As Double ' NUMBER        22    10      2 No
	Public dMovDate As Date ' DATE           7              No
	Public sInddetail As String ' CHAR           1              No
	Public sUse As String ' CHAR           1              No
	Public nIdsurr As Integer ' NUMBER        22     5      0 Yes
	Public nCashnum As Integer ' NUMBER        22     5      0 Yes
	Public nBordereaux As Integer ' NUMBER        22    10      0 Yes
	Public nReceipt As Integer ' NUMBER        22    10      0 Yes
	Public nYear As Integer ' NUMBER        22     5      0 No
	Public nMonth As Integer ' NUMBER        22     5      0 No
	Public sAdjustment As String ' CHAR           1              Yes
	Public dPosted As Date ' DATE           7              No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	Public nInterest As Double ' NUMBER        22     8      6 Yes
	Public nCurrency As Integer ' NUMBER        22     5      0 No
	Public sDescript As String ' NUMBER        22     5      0 No
	
	'- Variables auxiliares asociadas a procesos especificos
	Public nCostpay As Integer
	Public nModulec As Integer
	Public sModulec As String
	Public sClient As String
	Public sCliename As String
	Public nCover As Integer
	Public sCover As String
	Public nMov As Integer
	Public nCapital As Integer
	Public nPremium As Double
	Public nPrebaspay As Double
	Public nCommbaspay As Double
	Public nPreexpay As Double
	Public nCommexpay As Double
	
	'- Tipo de movimiento
	Public Enum eMoveAccPolType
		movAccPolValuePolSald = 1 '+ Saldo valor póliza
		movAccPolNetPremium = 2 '+ Prima neta
		movAccPolInterest = 3 '+ Interes
		movAccPolSurren = 4 '+ Rescate
		movAccPolCoverCost = 5 '+ Costo cobertura
		movAccPolFixCharges = 6 '+ Cargos fijos
		movAccPolCapCharges = 7 '+ Cargos por capital
		movAccPolFirtsPremium = 8 '+ Prima primera
		movAccPolPremium = 9 '+ Prima
		movAccPolAdditionalPrem = 10 '+ Prima adiccional
		movAccPolInjectionPrem = 11 '+ Prima de inyeccion
		movAccPolValuePolAdjust = 12 '+ Ajuste de valor póliza
		movAccPolNetPremAdjust = 13 '+ Ajuste de prima neta
		movAccPolInterestAdjust = 14 '+ Ajuste de interés
		movAccPolCoverCostAdjust = 15 '+ Ajuste de valor póliza
		movAccPolFixCharAdjust = 16 '+ Ajustes costo fijos
		movAccPolCapChargesAdjust = 17 '+ Ajuste cargo capital
	End Enum
	
	'- Variable de busqueda de la tabla temporal
	Public sKey As String
	'- Descripcion del tipo de movimiento
	Public sTypemove As String
	
	'- Tipo de dato para informacion de desglose de movimiento
	Private Structure udtMove_Accpol_Det
		Dim nCostpay As Integer
		Dim nModulec As Integer
		Dim sModulec As String
		Dim sClient As String
		Dim sCliename As String
		Dim nCover As Integer
		Dim sCover As String
		Dim nMov As Integer
		Dim nCapital As Integer
		Dim nPremium As Double
		Dim nPrebaspay As Double
		Dim nCommbaspay As Double
		Dim nPreexpay As Double
		Dim nCommexpay As Double
	End Structure
	
	'- Arreglo para almacenar desglose de movimiento
	Private marrDetail() As udtMove_Accpol_Det
	
	'% Find: Permite cargar un registro de movimiento de cuenta de poliza
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nIdmov As Integer) As Boolean
		'- Objeto de coneccion a la base de datos
		Dim lrecreaMove_Accpol As eRemoteDB.Execute
		On Error GoTo reaMove_Accpol_Err
		
		lrecreaMove_Accpol = New eRemoteDB.Execute
		
		With lrecreaMove_Accpol
			.StoredProcedure = "reaMove_Accpol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdmov", nIdmov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				Me.sCertype = sCertype
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nPolicy = nPolicy
				Me.nCertif = nCertif
				Me.nIdmov = nIdmov
				Me.nTypemove = .FieldToClass("nTypemove")
				Me.sTypemove = .FieldToClass("sTypemove")
				Me.nAmount = .FieldToClass("nAmount")
				Me.nCredit = .FieldToClass("nCredit")
				Me.nDebit = .FieldToClass("nDebit")
				Me.dMovDate = .FieldToClass("dMovdate")
				Me.sInddetail = .FieldToClass("sInddetail")
				Me.sUse = .FieldToClass("sUse")
				Me.nIdsurr = .FieldToClass("nIdsurr")
				Me.nCashnum = .FieldToClass("nCashnum")
				Me.nBordereaux = .FieldToClass("nBordereaux")
				Me.nReceipt = .FieldToClass("nReceipt")
				Me.nYear = .FieldToClass("nYear")
				Me.nMonth = .FieldToClass("nMonth")
				Me.sAdjustment = .FieldToClass("sAdjustment")
				Me.dPosted = .FieldToClass("dPosted")
				Me.nInterest = .FieldToClass("nInterest")
			Else
				Find = False
			End If
		End With
		
reaMove_Accpol_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Accpol = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_tmp_Move_Accpol: Permite recuperar un registro de la tabla
	'                        temporal de movimiento de cuenta de poliza
	Public Function Find_Tmp_Move_Accpol(ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		'- Objeto de coneccion a la base de datos
		Dim lrecreaTmp_Move_Accpol As eRemoteDB.Execute
		On Error GoTo reaTmp_Move_Accpol_Err
		
		lrecreaTmp_Move_Accpol = New eRemoteDB.Execute
		
		With lrecreaTmp_Move_Accpol
			.StoredProcedure = "reaTmp_Move_Accpol"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Find_Tmp_Move_Accpol = True
				Me.sCertype = sCertype
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nPolicy = nPolicy
				Me.nCertif = nCertif
				Me.nIdmov = nIdmov
				Me.nTypemove = .FieldToClass("nTypemove")
				Me.nAmount = .FieldToClass("nAmount")
				Me.nCredit = .FieldToClass("nCredit")
				Me.nDebit = .FieldToClass("nDebit")
				Me.dMovDate = .FieldToClass("dMovedate")
				Me.sInddetail = .FieldToClass("sInddetail")
				Me.sUse = .FieldToClass("sUse")
				Me.nReceipt = .FieldToClass("nReceipt")
				Me.nYear = .FieldToClass("nYear")
				Me.nMonth = .FieldToClass("nMonth")
				Me.sAdjustment = .FieldToClass("sAdjustment")
				Me.dPosted = .FieldToClass("dPosted")
			Else
				Find_Tmp_Move_Accpol = False
			End If
		End With
		
reaTmp_Move_Accpol_Err: 
		If Err.Number Then
			Find_Tmp_Move_Accpol = False
		End If
		'UPGRADE_NOTE: Object lrecreaTmp_Move_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTmp_Move_Accpol = Nothing
		On Error GoTo 0
	End Function
	
	'%FindDetail : Busca el detalle del movimiento cargado
	Public Function FindDetail() As Boolean
		'- Cantidad de casillas a llenar en arreglo
		Const C_BLOCKSIZE As Integer = 20
		'- Objeto para coneccion a base de datos
		Dim lrecinsReaMove_Accpol_det As eRemoteDB.Execute
		'- Indice de registros leidos
		Dim llngIndex As Integer
		
		On Error GoTo insReaMove_Accpol_det_Err
		
		lrecinsReaMove_Accpol_det = New eRemoteDB.Execute
		
		With lrecinsReaMove_Accpol_det
			.StoredProcedure = "insReaMove_Accpol_det"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdmov", nIdmov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindDetail = True
				'UPGRADE_WARNING: Lower bound of array marrDetail was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim marrDetail(C_BLOCKSIZE)
				
				Do While Not .EOF
					llngIndex = llngIndex + 1
					'+ Cuando se completa un bloque de datos se agrega uno nuevo bloque al arreglo
					If (llngIndex Mod C_BLOCKSIZE) = 0 Then
						'UPGRADE_WARNING: Lower bound of array marrDetail was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
						ReDim Preserve marrDetail(llngIndex + C_BLOCKSIZE)
					End If
					marrDetail(llngIndex).nCostpay = .FieldToClass("nCostpay")
					marrDetail(llngIndex).nModulec = .FieldToClass("nModulec")
					marrDetail(llngIndex).sModulec = .FieldToClass("sModulec")
					marrDetail(llngIndex).sClient = .FieldToClass("sClient")
					marrDetail(llngIndex).sCliename = .FieldToClass("sCliename")
					marrDetail(llngIndex).nCover = .FieldToClass("nCover")
					marrDetail(llngIndex).sCover = .FieldToClass("sCover")
					marrDetail(llngIndex).nMov = .FieldToClass("nMov")
					marrDetail(llngIndex).nCapital = .FieldToClass("nCapital")
					marrDetail(llngIndex).nPremium = .FieldToClass("nPremium")
					marrDetail(llngIndex).nPrebaspay = .FieldToClass("nPrebaspay")
					marrDetail(llngIndex).nCommbaspay = .FieldToClass("nCommbaspay")
					marrDetail(llngIndex).nPreexpay = .FieldToClass("nPreexpay")
					marrDetail(llngIndex).nCommexpay = .FieldToClass("nCommexpay")
					.RNext()
				Loop 
				.RCloseRec()
				'UPGRADE_WARNING: Lower bound of array marrDetail was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim Preserve marrDetail(llngIndex)
			Else
				FindDetail = False
				Erase marrDetail
			End If
		End With
		
insReaMove_Accpol_det_Err: 
		If Err.Number Then
			FindDetail = False
		End If
		'UPGRADE_NOTE: Object lrecinsReaMove_Accpol_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaMove_Accpol_det = Nothing
		On Error GoTo 0
	End Function
	
	'% CountDetails: Devuelve el número de registro de detalle que se encuentran en el arreglo
	Public ReadOnly Property CountDetails() As Integer
		Get
			On Error GoTo CountDetails_Err
			CountDetails = UBound(marrDetail)
			
CountDetails_Err: 
			If Err.Number Then
				CountDetails = 0
			End If
		End Get
	End Property
	
	'%Typemove: Obtiene el código del tipo de movimiento a mostrar en la página
	Public ReadOnly Property Typemove(ByVal nOption As Integer) As Integer
		Get
			
			'+Si la opción del proceso es prima de inyección
			
			If nOption = 1 Then
				Typemove = nTypemove
			Else
				Select Case nTypemove
					Case eMoveAccPolType.movAccPolValuePolSald
						Typemove = eMoveAccPolType.movAccPolValuePolAdjust
						
					Case eMoveAccPolType.movAccPolNetPremium
						Typemove = eMoveAccPolType.movAccPolNetPremAdjust
						
					Case eMoveAccPolType.movAccPolInterest
						Typemove = eMoveAccPolType.movAccPolInterestAdjust
						
					Case eMoveAccPolType.movAccPolCoverCost
						Typemove = eMoveAccPolType.movAccPolCoverCostAdjust
						
					Case eMoveAccPolType.movAccPolFixCharges
						Typemove = eMoveAccPolType.movAccPolFixCharAdjust
						
					Case eMoveAccPolType.movAccPolCapCharges
						Typemove = eMoveAccPolType.movAccPolCapChargesAdjust
						
					Case Else
						Typemove = nTypemove
						
				End Select
			End If
		End Get
	End Property
	'% DetailItem: Carga la información de un detalle en las variables de la clase
	Public Function DetailItem(ByVal llngIndex As Integer) As Boolean
		
		On Error GoTo DetailItem_Err
		
		With marrDetail(llngIndex)
			nCostpay = .nCostpay
			nModulec = .nModulec
			sModulec = .sModulec
			sClient = .sClient
			sCliename = .sCliename
			nCover = .nCover
			sCover = .sCover
			nMov = .nMov
			nCapital = .nCapital
			nPremium = .nPremium
			nPrebaspay = .nPrebaspay
			nCommbaspay = .nCommbaspay
			nPreexpay = .nPreexpay
			nCommexpay = .nCommexpay
		End With
		
		DetailItem = True
		
DetailItem_Err: 
		If Err.Number Then
			DetailItem = False
		End If
	End Function
	
	'% insMoveAcc_pol: Crea un movimiento en cuenta de poliza
	'----------------------------------------------------------
	Public Function insMoveAcc_pol() As Boolean
		'----------------------------------------------------------
		Dim lrecinsMove_Accpol As eRemoteDB.Execute
		
		On Error GoTo insMove_Accpol_Err
		
		lrecinsMove_Accpol = New eRemoteDB.Execute
		With lrecinsMove_Accpol
			.StoredProcedure = "insMove_Accpol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdmov", nIdmov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypemove", nTypemove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMovdate", dMovDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInddetail", sInddetail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUse", sUse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdsurr", nIdsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdjustment", sAdjustment, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", dPosted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nIdmov = .Parameters("nIdmov").Value
			End If
		End With
		
insMove_Accpol_Err: 
		If Err.Number Then
			insMoveAcc_pol = False
		End If
		'UPGRADE_NOTE: Object lrecinsMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsMove_Accpol = Nothing
		On Error GoTo 0
	End Function
	
	'%getIdMov: Obtiene el número de de movimiento + 1.
	Public Function getIdMov(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Integer
		'- Objeto para busqueda de datos
		Dim lrecMove_Accpol As eRemoteDB.Execute
		'- Numero de movimiento a retornar
		Dim lintIdMov As Integer
		
		On Error GoTo getIdMov_Err
		
		lrecMove_Accpol = New eRemoteDB.Execute
		
		getIdMov = 1
		
		With lrecMove_Accpol
			.StoredProcedure = "reaMove_AccpolMaxIdMov"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdMov", lintIdMov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				getIdMov = CShort(.Parameters("nIdMov").Value)
			End If
			
		End With
		
getIdMov_Err: 
		If Err.Number Then
			getIdMov = 1
		End If
		'UPGRADE_NOTE: Object lrecMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMove_Accpol = Nothing
		On Error GoTo 0
	End Function
	
	'%InsUpdMoveVP: Genera los movimientos de ajustes y de prima de inyeccion de las polizas
	'%              de vida activa
	Public Function InsUpdMoveVP(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTypemove As Integer, ByVal sKey As String, ByVal nUsercode As Integer) As Boolean
		'- Objeto para coneccion a base de datos
		Dim lrecInsUpdMoveVP As eRemoteDB.Execute
		
		On Error GoTo InsUpdMoveVP_Err
		lrecInsUpdMoveVP = New eRemoteDB.Execute
		
		With lrecInsUpdMoveVP
			.StoredProcedure = "InsUpdMoveVP"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypemove", nTypemove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdMoveVP = .Run(False)
		End With
		
InsUpdMoveVP_Err: 
		If Err.Number Then
			InsUpdMoveVP = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdMoveVP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdMoveVP = Nothing
		On Error GoTo 0
	End Function
	
	'patty
	'%UpdProp_Move_Acc: registra el numero de propuesta en Move_Acc con el nBordereaux
	Public Function UpdProp_Move_Acc(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nBordereaux As Double, ByVal sClient As String, ByVal nUsercode As Integer) As Boolean
		'- Objeto para coneccion a base de datos
		Dim lrecUpdProp_Move_Acc As eRemoteDB.Execute
		
		On Error GoTo UpdProp_Move_Acc_Err
		lrecUpdProp_Move_Acc = New eRemoteDB.Execute
		
		With lrecUpdProp_Move_Acc
			.StoredProcedure = "UpdProp_Move_Acc"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdProp_Move_Acc = .Run(False)
		End With
		
UpdProp_Move_Acc_Err: 
		If Err.Number Then
			UpdProp_Move_Acc = False
		End If
		'UPGRADE_NOTE: Object lrecUpdProp_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdProp_Move_Acc = Nothing
		On Error GoTo 0
	End Function
	
	'patty
	
	'% insValVAC610_K: Valida cabecera de transaccion de Desglose de movimiento del valor póliza
	Public Function insValVAC610_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nIdmov As Integer) As String
		'- Objeto para mensajes de error
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValVAC610_KErr
		
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
			End If
			
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
			End If
			
			If nPolicy = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 21033)
			End If
			
			
			If nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull And nCertif = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3006)
			End If
			
			If nIdmov = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 13255)
			End If
			
			insValVAC610_K = .Confirm
			
		End With
		
insValVAC610_KErr: 
		If Err.Number Then
			insValVAC610_K = "insValVAC610_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insValVAC609_K: Valida cabecera de transaccion de Consulta de valor póliza
	Public Function insValVAC609_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As String
		'- Objeto para mensajes de error
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCertificat As Certificat
		Dim lclsPolicy As Policy
		
		On Error GoTo insValVAC609_KErr
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
			End If
			
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
			End If
			
			If nPolicy = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 21033)
			End If
			
			If nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull Then
				lclsPolicy = New Policy
				If lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
					If CDbl(lclsPolicy.sPolitype) <> 1 And nCertif = eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 3006)
					End If
				Else
					.ErrorMessage(sCodispl, 1978)
				End If
			End If
			
			If nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull And nCertif <> eRemoteDB.Constants.intNull Then
				lclsCertificat = New Certificat
				If Not lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
					.ErrorMessage(sCodispl, 1978)
				End If
			End If
			
			insValVAC609_K = .Confirm
		End With
		
insValVAC609_KErr: 
		If Err.Number Then
			insValVAC609_K = "insValVAC609_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	
	'%UpdateTmp: Actualiza la tabla temporal TMP_Move_AccPOL
	Public Function UpdateTmp() As Boolean
		'- Objeto para coneccion a base de datos
		Dim lrecUpdateTmp As eRemoteDB.Execute
		
		On Error GoTo UpdateTmp_Err
		lrecUpdateTmp = New eRemoteDB.Execute
		
		With lrecUpdateTmp
			.StoredProcedure = "UpdTmp_Move_Accpol"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateTmp = .Run(False)
		End With
		
UpdateTmp_Err: 
		If Err.Number Then
			UpdateTmp = False
		End If
		'UPGRADE_NOTE: Object lrecUpdateTmp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdateTmp = Nothing
		On Error GoTo 0
	End Function
End Class






