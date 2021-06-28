Option Strict Off
Option Explicit On
Public Class Tab_docu
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_docu.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Defines the principal properties of the corresponding class to the tab_provider table (01/12/2001)
	'-Se definen las propiedades principales de la clase correspondientes a la tabla tab_provider (12/01/2001)
	'Column_name                        Type              Computed      Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nBranch As Integer 'smallint         no            2           5     0     no                                  (n/a)                               (n/a)
	Public nProduct As Integer 'smallint         no            2           5     0     no                                  (n/a)                               (n/a)
	Public nModulec As Integer 'smallint         no            2           5     0     no                                  (n/a)                               (n/a)
	Public nCover As Integer 'smallint         no            2           5     0     no                                  (n/a)                               (n/a)
	Public nCauscod As Integer 'smallint         no            2           5     0     no                                  (n/a)                               (n/a)
	Public nDoc_code As Integer 'smallint         no            2           5     0     no                                  (n/a)                               (n/a)
	Public dCompdate As Date 'datetime         no            8                       yes                                 (n/a)                               (n/a)
	Public sDescript As String 'char             no            30                      yes                                 no                                  yes
	Public sShort_des As String 'char             no            12                      yes                                 no                                  yes
	Public sStatregt As String 'char             no            1                       yes                                 no                                  yes
	Public nUsercode As Integer 'smallint         no            2           5     0     yes                                 (n/a)                               (n/a)
	Public sClaimpay As String 'char             no            1                       yes                                 no                                  yes
	Public nDays_presc As Integer 'smallint         no            2           5     0     no                                  (n/a)                               (n/a)
	Private mvarTab_docus As Tab_docus
	
	
	Public Property Tab_docus() As Tab_docus
		Get
			If mvarTab_docus Is Nothing Then
				mvarTab_docus = New Tab_docus
			End If
			
			Tab_docus = mvarTab_docus
		End Get
		Set(ByVal Value As Tab_docus)
			mvarTab_docus = Value
		End Set
	End Property
	
	Private Sub Class_Initialize_Renamed()
		mvarTab_docus = New Tab_docus
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		mvarTab_docus = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'*valExistTab_docu: Valida la existencia de causas asociadas a un rammo el cual es pasado como parámetro.
	Private Function valExistTab_docu(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCauscod As Integer) As Boolean
		Dim lrecTab_docu As eRemoteDB.Execute
		valExistTab_docu = False
		
		On Error GoTo valExistTab_docu_Err
		
		lrecTab_docu = New eRemoteDB.Execute
		
		With lrecTab_docu
			.StoredProcedure = "valTab_docu_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCauscod", nCauscod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("lCount") > 0 Then
					valExistTab_docu = True
				End If
			End If
		End With
		lrecTab_docu = Nothing
		
valExistTab_docu_Err: 
		If Err.Number Then
			valExistTab_docu = False
		End If
		On Error GoTo 0
	End Function
	
	'%valDupTab_docu: Permite validar si una causa de siniestro ya está registrada.
	Private Function valDupTab_docu(ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef nModulec As Integer, ByRef nCover As Integer, ByRef nCauscod As Integer, ByRef nDoc_code As Integer) As Boolean
		Dim lexeTime As eRemoteDB.Execute
		
		On Error GoTo valDupTab_docu_Err
		
		lexeTime = New eRemoteDB.Execute
		
		With lexeTime
			.StoredProcedure = "valTab_docu_o"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCauscod", IIf(nCauscod = eRemoteDB.Constants.intNull, 0, nCauscod), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDoc_code", nDoc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("lCount") > 0 Then
					valDupTab_docu = True
				Else
					valDupTab_docu = False
				End If
				.RCloseRec()
			Else
				valDupTab_docu = False
			End If
		End With
		
valDupTab_docu_Err: 
		If Err.Number Then
			valDupTab_docu = False
		End If
		On Error GoTo 0
		lexeTime = Nothing
	End Function
	
	'%valClaim_nDoc_code: Permite validar si una causa de siniestro ya está registrada.
	Private Function valClaim_nDoc_code(ByVal nDoc_code As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCauscod As Integer) As Boolean
		Dim lexeTime As eRemoteDB.Execute
		
		On Error GoTo valClaim_nDoc_code_Err
		
		lexeTime = New eRemoteDB.Execute
		
		With lexeTime
			.StoredProcedure = "valClaim_nDoc_code_a"
			.Parameters.Add("nDoc_code", nDoc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCauscod", IIf(nCauscod = eRemoteDB.Constants.intNull, 0, nCauscod), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				If .FieldToClass("lCount") > 0 Then
					valClaim_nDoc_code = True
				Else
					valClaim_nDoc_code = False
				End If
				.RCloseRec()
			Else
				valClaim_nDoc_code = False
			End If
		End With
		lexeTime = Nothing
		
valClaim_nDoc_code_Err: 
		If Err.Number Then
			valClaim_nDoc_code = False
		End If
		On Error GoTo 0
	End Function
	
	'%Update: Rutina que actualiza la tabla "interm_bud", manteniendo historia.
	Public Function Update() As Boolean
		Dim lexeTime As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lexeTime = New eRemoteDB.Execute
		
		With lexeTime
			.StoredProcedure = "updTab_docu"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCauscod", nCauscod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDoc_code", nDoc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaimPay", sClaimpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDays_presc", nDays_presc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Update = True
			Else
				Update = False
			End If
		End With
		lexeTime = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'%Add: Rutina que actualiza la tabla "interm_bud", manteniendo historia.
	Public Function Add() As Boolean
		Dim lexeTime As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lexeTime = New eRemoteDB.Execute
		
		With lexeTime
			.StoredProcedure = "creTab_docu"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCauscod", nCauscod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDoc_code", nDoc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaimPay", sClaimpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDays_presc", nDays_presc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Add = True
			Else
				Add = False
			End If
		End With
		lexeTime = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Delete: Deletes a record of the Tab_docu table
	'%Delete: Borra un registro de la Tabla Tab_docu
	Public Function Delete() As Boolean
		Dim lexeTime As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lexeTime = New eRemoteDB.Execute
		With lexeTime
			
			.StoredProcedure = "delTab_docu"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCauscod", nCauscod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDoc_code", nDoc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		lexeTime = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'%insValSI015_k: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMSI015_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, Optional ByVal nProduct As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nCauscod As Integer = 0) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMSI015_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+Validación del campo: Ramo.
		If nBranch = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 9064)
		Else
			'**+If the action is Duplicate Table, TAB_DOCU for this branch should be empty
			'+ Si la acción es duplicar no debe existir información registrada en la tabla TAB_DOCU.
			If nAction = 306 Then
				If nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull Then
					'+ se valida que los campos nproduct, nmodulec, ncover y ncausecod vengan vacios
					If nProduct = eRemoteDB.Constants.intNull Then
						nProduct = 0
					End If
					If nModulec = eRemoteDB.Constants.intNull Then
						nModulec = 0
					End If
					If nCover = eRemoteDB.Constants.intNull Then
						nCover = 0
					End If
					If nCauscod = eRemoteDB.Constants.intNull Then
						nCauscod = 0
					End If
					If valExistTab_docu(nBranch, nProduct, nModulec, nCover, nCauscod) Then
						Call lclsErrors.ErrorMessage(sCodispl, 10049)
					End If
				End If
			End If
		End If
		insValMSI015_k = lclsErrors.Confirm
		
insValMSI015_k_Err: 
		If Err.Number Then
			insValMSI015_k = "insValMSI015:" & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insValMSI015: Se validan los datos de la página
	Public Function insValMSI015(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCauscod As Integer, ByVal nDoc_code As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal nDays_presc As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMSI015_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se valida la columna 3: Descripción larga.
		If nDoc_code <> eRemoteDB.Constants.intNull Then
			If sDescript = String.Empty Then
				lclsErrors.ErrorMessage(sCodispl, 10005)
			End If
		End If
		
		'+ Se valida la columna 4: Descripción corta.
		If nDoc_code <> eRemoteDB.Constants.intNull Then
			If sShort_des = String.Empty Then
				lclsErrors.ErrorMessage(sCodispl, 10006)
			End If
		End If
		
		'+ Se valida la columna 6: Estado.
		If nDoc_code <> eRemoteDB.Constants.intNull Then
			If sStatregt = String.Empty Or sStatregt = "0" Then
				lclsErrors.ErrorMessage(sCodispl, 9089)
			End If
		End If
		
		'+ Si se está registrando se valida que no exista duplicidad de la información.
		If sAction = "Add" Then
			If nDoc_code = eRemoteDB.Constants.intNull Then
				lclsErrors.ErrorMessage(sCodispl, 10875)
			End If
			If nDoc_code <> eRemoteDB.Constants.intNull Then
				'+ Se valida que el còdigo no exista en la tabla
				If valDupTab_docu(nBranch, nProduct, nModulec, nCover, nCauscod, nDoc_code) Then
					lclsErrors.ErrorMessage(sCodispl, 10004)
				End If
			End If
		End If
		
		'+ Los días de plazo de entrega debe ser mayor que cero
		If nDays_presc <> eRemoteDB.Constants.intNull Then
			If nDays_presc <= 0 Then
				lclsErrors.ErrorMessage(sCodispl, 55723)
			End If
		End If
		
		If sAction = "Del" Then
			'+ Si se está eliminando se verifica que no esté ningún siniestro asociado a dicha causa.
			If valClaim_nDoc_code(nDoc_code, nBranch, nProduct, nModulec, nCover, nCauscod) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10874)
			End If
		End If
		
		insValMSI015 = lclsErrors.Confirm
		
insValMSI015_Err: 
		If Err.Number Then
			insValMSI015 = "insValMSI015:" & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function
	
	'%insPostMSI015: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostMSI015(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCauscod As Integer, ByVal nDoc_code As Integer, ByVal sClaimpay As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal nDays_presc As Integer) As Boolean
		On Error GoTo insPostMSI015_err
		
		sAction = Trim(sAction)
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.nCauscod = nCauscod
			.nDoc_code = nDoc_code
			.nUsercode = nUsercode
			.sClaimpay = sClaimpay
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.nDays_presc = nDays_presc
		End With
		
		Select Case sAction
			
			'**+If the selected option is Register
			'+Si la opción seleccionada es Registrar
			Case "Add"
				insPostMSI015 = Add
				
				'**+If the selected option is Modify
				'+Si la opción seleccionada es Modificar
			Case "Update"
				insPostMSI015 = Update
				
				'**+If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
			Case "Del"
				insPostMSI015 = Delete
				
		End Select
		
insPostMSI015_err: 
		If Err.Number Then
			insPostMSI015 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Duplicar Rutina que actualiza el ramo destino con los datos proveniente del ramo origen.
	Public Function insDuplicarMSI015(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCauscod As Integer, ByVal nLastBranch As Integer, ByVal nLastProduct As Integer, ByVal nLastModulec As Integer, ByVal nLastCover As Integer, ByVal nLastCauscod As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecTab_docu As eRemoteDB.Execute
		
		lrecTab_docu = New eRemoteDB.Execute
		
		On Error GoTo insDuplicarMSI015_Err
		
		insDuplicarMSI015 = False
		
		'+ Duplica el registro correspondiente en TAB_DOCU
		With lrecTab_docu
			.StoredProcedure = "insDuplicateMSI015"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastBranch", nLastBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastProduct", nLastProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastModulec", nLastModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastCover", nLastCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCausecod", nCauscod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastCausecod", nLastCauscod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insDuplicarMSI015 = .Run(False)
		End With
		
		lrecTab_docu = Nothing
		
insDuplicarMSI015_Err: 
		If Err.Number Then
			insDuplicarMSI015 = False
		End If
		On Error GoTo 0
	End Function
End Class






