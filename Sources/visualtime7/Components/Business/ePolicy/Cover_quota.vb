Option Strict Off
Option Explicit On
Public Class Cover_quota
	'%-------------------------------------------------------%'
	'% $Workfile:: Cover_quota.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 29/10/04 4:12p                               $%'
	'% $Revision:: 39                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades de la tabla en el sistema al 21/02/2002
	'+ Los campos llave correponden a: sCertype, nBranch, nProduct, nPolicy, nGroup, nModulec, nRole, nCover
	
	'+ Name                    Type                      Nullable
	'+ ----------------------- ------------------------- --------
	Public sCertype As String 'CHAR(1)       No
	Public nBranch As Integer 'NUMBER(5)     No
	Public nProduct As Integer 'NUMBER(5)     No
	Public nPolicy As Double 'NUMBER(10)    No
	Public nGroup As Integer 'NUMBER(5)     No
	Public nGroup_Initial As Integer 'NUMBER(5)     No
	Public nModulec As Integer 'NUMBER(5)     No
	Public nRole As Integer 'NUMBER(5)     No
	Public nCover As Integer 'NUMBER(5)     No
	Public nCapital As Double 'Number(12)    Yes
	Public nTaxIVA As Double 'Number(4, 2)  Yes
	Public nTax As Double 'Number(4, 2)  Yes
	Public nTaxOrig As Double 'Number(4, 2)  Yes
	Public nPremium As Double 'Number(10, 2) Yes
	Public nPremiumOrig As Double 'Number(10, 2) Yes
	Public nInsucount As Integer 'Number(10)    Yes
	Public nInsured As Integer 'Number(10)    Yes
	Public nUsercode As Integer 'NUMBER(5)     No
	
	'+ Variable auxiliares
	
	'- Se definen las variables para asignar el estado de los campos en la página
	Public bExistGroups As Boolean
	Public bExistModulec As Boolean
	Public sErrors As String
	Public nTaxMar As Double
	Public nExcInsured As Integer
	
	Public mcolCover_quotas As Cover_quotas
	Private mstrKey As String
	
	'% inspreVI666: Se controla el acceso a la ventana
	Public Sub inspreVI666(ByVal bQuery As Boolean, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nSessionId As String, ByVal nUsercode As Integer, ByVal sReloadPage As String)
		Dim lclsGroups As Groups
		Dim lclsPolicy As Policy
		Dim lclsTab_moduls As eProduct.Tab_moduls
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo inspreVI666_err
		
		lclsPolicy = New Policy
		lclsTab_moduls = New eProduct.Tab_moduls
		
		nGroup_Initial = eRemoteDB.Constants.intNull
		If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
			'+ Si se definieron las coberturas por grupo
			If lclsPolicy.sTyp_module = "3" Then
				lclsGroups = New Groups
				If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
					bExistGroups = True
					nGroup_Initial = lclsGroups.nGroup_Initial
				End If
			ElseIf lclsPolicy.sTyp_module = "4" Then 
				lclsErrors = New eFunctions.Errors
				'+ Se envía un mensaje de error, ya que las coberturas no pueden definirse por certificado
				sErrors = lclsErrors.ErrorMessage("VI999", 3932,  ,  ,  , True)
			End If
		End If
		
		If sErrors = String.Empty Then
			'+ Si el producto es modular
			If lclsTab_moduls.Find(nBranch, nProduct, dEffecdate) Then
				bExistModulec = True
			End If
			
			nGroup = IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup)
			nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
			
			'+ Si se trata de una consulta se realiza la búsqueda en la tabla
			If bQuery Then
				Call mcolCover_quotas.Find(sCertype, nBranch, nProduct, nPolicy, nGroup, nModulec, dEffecdate)
			Else
				Call insKey(nSessionId, nUsercode)
				Call mcolCover_quotas.calCover_quota(sCertype, nBranch, nProduct, nPolicy, dEffecdate, nTransaction, nGroup, nModulec, nUsercode, mstrKey, sReloadPage)
			End If
		End If
		
inspreVI666_err: 
		If Err.Number Then
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroups = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsTab_moduls may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_moduls = Nothing
	End Sub
	
	'% insvalVI666: Se validan los datos de la ventana
	Public Function insvalVI666(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nTypMar As Double, ByVal nOriginalTypMar As Double, ByVal nPremium As Double, ByVal nOriginalPremium As Double, ByVal sWindowType As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsDisco_expr As eProduct.Disco_expr
		Dim lclsPolicy As Policy
		Dim lclsGroups As Groups
		Dim lblnError As Boolean
		
		On Error GoTo insvalVI666_err
		
		lobjErrors = New eFunctions.Errors
		lclsDisco_expr = New eProduct.Disco_expr
		lclsPolicy = New ePolicy.Policy
		
		lblnError = False
		
		Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
		'+ Si se indicó que la póliza es por grupo de colectivo
		If lclsPolicy.sTyp_module = "3" Then
			lclsGroups = New ePolicy.Groups
			'+ Si la póliza tiene asociado grupos de colectivo
			If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
				If nGroup <= 0 Then
					Call lobjErrors.ErrorMessage("VI666", 3308)
					lblnError = True
				End If
			Else
				Call lobjErrors.ErrorMessage("VI666", 3887)
				lblnError = True
			End If
			'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsGroups = Nothing
		End If
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		
		If Not lblnError And sWindowType = "PopUp" Then
			With lclsDisco_expr
				If .Find_typemar(nBranch, nProduct, dEffecdate) Then
					'+ Se valida que el margen de utilidad esté acorde a lo definido en el Diseñador de productos
					If nTypMar > nOriginalTypMar Then
						If .sChanallo = "2" Or .sChanallo = "0" Then
							'+ El margen no puede aumentarse si no se indica en el Diseñador
							Call lobjErrors.ErrorMessage("VI666", 3307)
						Else
							'+ El margen no puede ser mayor al máximo indicado en el Diseñador
							If .nDisexaddper <> eRemoteDB.Constants.intNull Then
								If nTypMar > nOriginalTypMar * (1 + .nDisexaddper / 100) Then
									Call lobjErrors.ErrorMessage("VI666", 3833,  ,  , " (" & .nDisexaddper & "%)")
								End If
							End If
						End If
					ElseIf nTypMar < nOriginalTypMar Then 
						If .sChanallo = "1" Or .sChanallo = "0" Then
							'+ El margen no puede disminuir si no se indica en el Diseñador
							Call lobjErrors.ErrorMessage("VI666", 3306)
						Else
							'+ El margen no puede ser menor al mínimo indicado en el Diseñador
							If .nDisexsubper <> eRemoteDB.Constants.intNull Then
								If nTypMar < nOriginalTypMar * (1 - .nDisexsubper / 100) Then
									Call lobjErrors.ErrorMessage("VI666", 3834,  ,  , " (" & .nDisexsubper & "%)")
								End If
							End If
						End If
					End If
					
					'+ Se valida que la prima bruta esté acorde a lo definido en el Diseñador de productos
					If nPremium > nOriginalPremium Then
						If .sChanallo = "2" Or .sChanallo = "0" Then
							'+ La prima bruta no puede aumentarse si no se indica en el Diseñador
							Call lobjErrors.ErrorMessage("VI666", 3314)
						Else
							'+ La prima bruta no puede ser mayor al máximo indicado en el Diseñador
							If .nDisexaddper <> eRemoteDB.Constants.intNull Then
								If nPremium > nOriginalPremium * (1 + .nDisexaddper / 100) Then
									Call lobjErrors.ErrorMessage("VI666", 3729,  ,  , " (" & .nDisexaddper & "%)")
								End If
							End If
						End If
					ElseIf nPremium < nOriginalPremium Then 
						If .sChanallo = "1" Or .sChanallo = "0" Then
							'+ La prima bruta no puede disminuir si no se indica en el Diseñador
							Call lobjErrors.ErrorMessage("VI666", 3313)
						Else
							'+ La prima bruta no puede ser menor al mínimo indicado en el Diseñador
							If .nDisexsubper <> eRemoteDB.Constants.intNull Then
								If nPremium < nOriginalPremium * (1 - .nDisexsubper / 100) Then
									Call lobjErrors.ErrorMessage("VI666", 3730,  ,  , " (" & .nDisexsubper & "%)")
								End If
							End If
						End If
					End If
				End If
			End With
		End If
		
		insvalVI666 = lobjErrors.Confirm
		
insvalVI666_err: 
		If Err.Number Then
			insvalVI666 = "insvalVI666:" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroups = Nothing
	End Function
	
	'% inspostVI666: Se actualizan los datos de la ventana en las tablas
	Public Function inspostVI666(ByVal sWindowType As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nRole As Integer, ByVal nCover As Integer, ByVal nTax As Double, ByVal nTaxMar As Double, ByVal nPremium As Double, ByVal nSessionId As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		Dim lcolTDetail_pre As TDetail_pres
		
		On Error GoTo inspostVI666_err
		Call insKey(nSessionId, nUsercode)
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nGroup = IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup)
			.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
			.nCover = nCover
			.nRole = nRole
			.nTax = nTax
			.nPremium = nPremium
			.nTaxMar = nTaxMar
			.nUsercode = nUsercode
		End With
		
		If sWindowType = "PopUp" Then
			inspostVI666 = UpdateTDetail_pre
		Else
			lcolTDetail_pre = New TDetail_pres
			If lcolTDetail_pre.FindManReceipt(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eRemoteDB.Constants.intNull, 0, mstrKey, "1") Then
				If UpdateCover_quota(dEffecdate) Then
					lclsPolicy_Win = New Policy_Win
					inspostVI666 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI666", "2")
				End If
			Else
				inspostVI666 = True
			End If
		End If
		
inspostVI666_err: 
		If Err.Number Then
			inspostVI666 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lcolTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTDetail_pre = Nothing
	End Function
	
	'% UpdateTDetail_pre: actualiza la información en la tabla temporal
	Private Function UpdateTDetail_pre() As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		On Error GoTo UpdateTDetail_pre_Err
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insupdCover_quota'
		'+Información leída el 05/03/2002
		
		With lclsExecute
			.StoredProcedure = "updTDetail_pre_CQ"
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxMar", nTaxMar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", mstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateTDetail_pre = .Run(False)
		End With
		
UpdateTDetail_pre_Err: 
		If Err.Number Then
			UpdateTDetail_pre = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'% UpdateCover_quota: actualiza la información en la tabla temporal
	Private Function UpdateCover_quota(ByVal dEffecdate As Date) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		On Error GoTo UpdateCover_quota_Err
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insupdCover_quota'
		'+Información leída el 05/03/2002
		
		With lclsExecute
			.StoredProcedure = "insCover_quota"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", mstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateCover_quota = .Run(False)
		End With
		
UpdateCover_quota_Err: 
		If Err.Number Then
			UpdateCover_quota = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'%DeleteAll: Elimina la información para la póliza
	Public Function DeleteAll(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecdelCover_quota As eRemoteDB.Execute
		
		On Error GoTo DeleteAll_err
		lrecdelCover_quota = New eRemoteDB.Execute
		'+ Definición de store procedure delCover_quota al 10-01-2002 10:34:36
		With lrecdelCover_quota
			.StoredProcedure = "delCover_quota"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteAll = .Run(False)
		End With
		
DeleteAll_err: 
		If Err.Number Then
			DeleteAll = False
		End If
		'UPGRADE_NOTE: Object lrecdelCover_quota may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCover_quota = Nothing
		On Error GoTo 0
	End Function
	
	'% insKey: se encarga de devolver la llave de lectura del registro de coberturas
	Private Sub insKey(ByVal nSessionId As String, ByVal nUsercode As Integer)
		mstrKey = "CQ" & CStr(nSessionId) & "-" & CStr(nUsercode)
	End Sub
	
	'% InitValues: Se inicializan las variables de la clase
	Private Sub InitValues()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nGroup = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nRole = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nCapital = eRemoteDB.Constants.intNull
		nTaxIVA = eRemoteDB.Constants.intNull
		nTax = eRemoteDB.Constants.intNull
		nPremium = eRemoteDB.Constants.intNull
		nInsucount = eRemoteDB.Constants.intNull
		nInsured = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		bExistGroups = False
		bExistModulec = False
		sErrors = String.Empty
		nTaxMar = eRemoteDB.Constants.intNull
		mcolCover_quotas = New Cover_quotas
	End Sub
	
	'% insExistcover_quota: Verifica que la poliza tenga cotizaciones de coberturas
	Public Function insExistcover_quota(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lreccover_quota As eRemoteDB.Execute
		
		On Error GoTo insExistcover_quota_Err
		
		lreccover_quota = New eRemoteDB.Execute
		
		insExistcover_quota = False
		
		With lreccover_quota
			.StoredProcedure = "reaExistCover_Quota"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insExistcover_quota = .Parameters("nExist").Value > 0
			End If
		End With
		
insExistcover_quota_Err: 
		If Err.Number Then
			insExistcover_quota = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccover_quota may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccover_quota = Nothing
	End Function
	
	'% Class_Initialize: Se controla la creación de cada instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Se controla la destrucción de cada instancia de la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolCover_quotas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolCover_quotas = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






