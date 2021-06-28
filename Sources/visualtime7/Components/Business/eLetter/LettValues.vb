Option Strict Off
Option Explicit On
Public Class LettValues
	'**+Objetive: Clase generada a partir de la tabla 'LETTVALUES' que es Parámetros o condiciones de la solicitud de envío.Un registro por cada parámetro o variable de la condición
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'LETTVALUES' Parameters or conditions of the request.A record per every parameter or variable of the condition
	'+Version: $$Revision: 9 $
	
	'**-Objective: Constant, possible letters models according to its receiver - Beneficiary.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Beneficiario.
	Private Const CN_BENEF As Short = 0
	
	'**-Objective: Constant, possible letters models according to its receiver - Intermediary.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Intermediario.
	Private Const CN_INTERMEDIA As Short = 1
	
	'**-Objective: Constant, possible letters models according to its receiver - Client.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Cliente.
	Private Const CN_CLIENT As Short = 2
	
	'**-Objective: Constant, possible letters models according to its receiver - Policy.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Poliza/Cobertura.
	Private Const CN_POLICY As Short = 3
	
	'**-Objective: Constant, possible letters models according to its receiver - Receiving.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Receptoria.
	Private Const CN_RECEIPT As Short = 4
	
	'**-Objective: Constant, possible letters models according to its receiver - Claim.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Siniestro.
	Private Const CN_CLAIM As Short = 5
	
	'**-Objective: Constant, possible letters models according to its receiver - Intervention professional.
	'-Objetivo: Constante, posibles modelos de carta según su receptor - Intervención profesional.
	Private Const CN_INTERPROF As Short = 6
	
	'**-Objective: Number of the request for remittance of correspondence.
	'-Objetivo: Número de solicitud de envío.
	Public nLettRequest As Short
	
	'**-Objective: Consecutive number identifying the parameter or variable order.
	'-Objetivo: Consecutivo que identifica el orden del parámetro o variable .
	Public nConsec As Short
	
	'**-Objective: Code of the variable group (Correspondence).
	'-Objetivo: Código del grupo de variables (Correspondencia).
	Public nLett_group As Short
	
	'**-Objective: Parameter Code. The possible values as per table 622.
	'-Objetivo: Código del parámetro. Valores posibles según tabla 622.
	Public nParameters As Short
	
	'**-Objective: Name of The Variable used in Correspondence.
	'-Objetivo: Nombre de la variable utilizada en correspondencia.
	Public sVariable As String
	
	'**-Objective: Parameter or variable value.
	'-Objetivo: Valor del parámetro o de la variable .
	Public sValue As String
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Short
	
	'**-Objective: Code of operation symbol Sole values as per table 311.
	'-Objetivo: Código del tipo de operando Valores posibles según tabla 311.
	Public nAritOper As Short
	
	'**-Objective: Description of the algebraic operation
	'-Objetivo: Descripción de la operación aritmetica
	Public sAritOper As String
	
	'**-Objective: Description of the column
	'-Objetivo: Descripción de la columna
	Public sColumName As String
	
	'**-Objective: Status of the instance
	'-Objetivo: Estado de la instancia
	Public nStatusInstance As Short
	
	'**%Objective: This function is in charge of adding information to the LETTVALUES table
	'**%Parameters:
	'**%  nAritOper    - Code of operation symbol sole values as per table 311.
	'**%  sValue       - Parameter or variable value.
	'**%  nParameters  - Parameter Code the possible values as per table 622.
	'**%  nLett_group  - Code of the variable group (Correspondence).
	'**%  nConsec      - Consecutive number identifying the parameter or variable order
	'**%  nUsercode    - Code of the user creating or updating the record.
	'**%  sVariable    - Name of The Variable used in Correspondence.
	'**%  nLettRequest - Number of the request for remittance of  correspondence.
	'%Objetivo: Esta función se encarga de agregar información en la tabla principal de la clase
	'%Parámetros:
	'%    nAritOper    - Código del tipo de operando valores posibles según tabla 311.
	'%    sValue       - Valor del parámetro o de la variable.
	'%    nParameters  - Código del parámetro valores posibles según tabla 622.
	'%    nLett_group  - Código del grupo de variables (Correspondencia).
	'%    nConsec      - Consecutivo que identifica el orden del parámetro o variable.
	'%    nUsercode    - Código del usuario que crea o actualiza el registro.
	'%    sVariable    - Nombre de la variable utilizada en correspondencia..
	'%    nLettRequest - Número de solicitud de envío.
	Public Function Add(Optional ByVal nLettRequest As Short = 0, Optional ByVal nConsec As Short = 0, Optional ByVal nLett_group As Short = 0, Optional ByVal nParameters As Short = 0, Optional ByVal sVariable As String = "", Optional ByVal sValue As String = "", Optional ByVal nUsercode As Short = 0, Optional ByVal nAritOper As Short = 0) As Boolean
		Dim lreccreLettValues As eRemoteDB.Execute
		Dim lstrAlias As String
		
		If Not IsIDEMode Then
		End If
		
		lreccreLettValues = New eRemoteDB.Execute
		
		With Me
			If nLettRequest <> 0 Then .nLettRequest = nLettRequest
			If nConsec <> 0 Then .nConsec = nConsec
			If nLett_group <> 0 Then .nLett_group = nLett_group
			If nParameters <> 0 Then .nParameters = nParameters
			If nParameters = -32768 Then .nParameters = 0
			If sVariable <> String.Empty Then .sVariable = sVariable
			If sValue <> String.Empty Then .sValue = sValue
			If nUsercode <> 0 Then .nUsercode = nUsercode
			If nAritOper <> 0 Then .nAritOper = nAritOper
		End With
		
		Select Case Me.nParameters
			Case CN_BENEF
				lstrAlias = "BENEFICIAR."
			Case CN_CLAIM
				lstrAlias = "CLAIM."
			Case CN_CLIENT
				lstrAlias = "CLIENT."
			Case CN_INTERMEDIA
				lstrAlias = "INTERMEDIA."
			Case CN_INTERPROF
				lstrAlias = "."
			Case CN_POLICY
				lstrAlias = "POLICY."
			Case CN_RECEIPT
				lstrAlias = "PREMIUM."
		End Select
		
		With lreccreLettValues
			.StoredProcedure = "creLettValues"
			.Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", Me.nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLett_group", Me.nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParameters", Me.nParameters, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVariable", Me.sVariable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue", Me.sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAritOper", Me.nAritOper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Me.nConsec = IIf(IsDbNull(.Parameters("nConsec").Value), 0, .Parameters("nConsec").Value)
				Add = True
			Else
				Add = False
			End If
		End With
		
		lreccreLettValues = Nothing
		
		Exit Function
		lreccreLettValues = Nothing
	End Function
	
	'**%Objective: This function is in charge of updating the data in the main table of the class.
	'**%Parameters:
	'**%  nCondition  - Number of the conditions of consultation.
	'**%  sCodispl    - Code of the window (logical code).
	'%Objetivo:Esta función se encarga de actualizar información en la tabla principal de la clase.
	'%Parámetros:
	'%    nCondition  - Número de las condiciones de consulta.
	'%    sCodispl    - Codigo de la ventana (logical code).
	Private Function Update() As Object
		
		Dim lrecUpdLettValues As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecUpdLettValues = New eRemoteDB.Execute
		Update = False
		With lrecUpdLettValues
			.StoredProcedure = "UpdLettValues"
			.Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAritOper", nAritOper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLett_group", nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParameters", nParameters, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVariable", sVariable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
				.RCloseRec()
			End If
		End With
		lrecUpdLettValues = Nothing
		
		Exit Function
		lrecUpdLettValues = Nothing
	End Function
	
	''**%Objective: validate the data entered on the detail zone for the form.
	'**%Parameters:
	'**%   sCodispl    - Code of the window (logical code).
	'**%   nCondition  - Number of the conditions of consultation.
	'%Objetivo: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%          forma.
	'%Parámetros:
	'%    sCodispl     - Code of the window (logical code).
	'%    nCondition   - Número de las condiciones de consulta.
	Public Function insValLT030(ByVal sCodispl As String, ByVal nCondition As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		
		lclsErrors = New eFunctions.Errors
		
		If nCondition = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 8339)
		End If
		insValLT030 = lclsErrors.Confirm
		lclsErrors = Nothing
		
		Exit Function
		lclsErrors = Nothing
	End Function
	
	'**%Objective: validate the data entered on the detail zone for the form.s
	'**%Parameters:
	'**%  sInitial     - Variable of initial value or smaller rank.
	'**%  nAritOper    - Code of operation symbol sole values as per table 311.
	'**%  nLett_group  - Code of the variable group (Correspondence).
	'**%  sVariable    - Name of The Variable used in Correspondence.
	'**%  sCodispl     - Code of the window (logical code).
	'**%  sEnd         - Variable of final value or greater rank.
	'%Objetivo: valida los campos de la forma
	'%Parámetros:
	'%    sInitial     - Variable de valor inicial o rango menor.
	'%    nAritOper    - Código del tipo de operando valores posibles según tabla 311.
	'%    nLett_group  - Código del grupo de variables (Correspondencia).
	'%    sVariable    - Nombre de la variable utilizada en correspondencia.
	'%    sCodispl     - Codigo de la ventana (logical code).
	'%    sEnd         - Variable de valor final o rango mayor.
	Public Function insValLT030Upd(ByVal sCodispl As String, ByVal nLett_group As Short, ByVal sVariable As String, ByVal nAritOper As Short, ByVal sInitial As String, ByVal sEnd As String) As String
        Dim lclsGroupParam As GroupParams = Nothing
        Dim lclsGroupVar As GroupVariables = Nothing
        Dim lclsErrors As eFunctions.Errors = Nothing
        Dim lclsQuery As eRemoteDB.Query = Nothing
        Dim lclsValTime As eFunctions.valField = Nothing
        Dim sAtrib As String = String.Empty
		
		If Not IsIDEMode Then
		End If
		
		lclsGroupParam = New GroupParams
		lclsErrors = New eFunctions.Errors
		lclsGroupVar = New GroupVariables
		lclsQuery = New eRemoteDB.Query
		lclsValTime = New eFunctions.valField
		lclsValTime.objErr = lclsErrors
		
		If nLett_group = intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 90293)
		Else
			If Not lclsGroupParam.Find(nLett_group) Then
				Call lclsErrors.ErrorMessage(sCodispl, 90294)
			Else
				If sVariable = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 90295)
				Else
					If Not lclsGroupVar.Find(nLett_group, sVariable) Then
						Call lclsErrors.ErrorMessage(sCodispl, 90296)
					Else
						If nAritOper = intNull Then
							Call lclsErrors.ErrorMessage(sCodispl, 90297)
						Else
							If Not lclsQuery.OpenQuery("Table311", "sDescript", "nAritOper=" & CStr(nAritOper)) Then
								Call lclsErrors.ErrorMessage(sCodispl, 90298)
							Else
								If sInitial = String.Empty Then
									Call lclsErrors.ErrorMessage(sCodispl, 90299)
								Else
									lclsQuery.CloseQuery()
									lclsQuery = New eRemoteDB.Query
									If lclsQuery.OpenQuery("groupvariables", "sColumName", "nlett_group=" & CStr(nLett_group) & " and sVariable= '" & CStr(sVariable) & "'") Then
										If UCase(Mid(lclsQuery.FieldToClass("sColumName"), 1, 1)) = "N" Then
											sAtrib = "N"
											lclsValTime.ErrEmpty = 11238
											lclsValTime.Min = 1
											If lclsValTime.ValNumber(sInitial) Then
												If nAritOper = 7 Then
													If sEnd = String.Empty Then
														Call lclsErrors.ErrorMessage(sCodispl, 8351)
													Else
														'If nAritOper = 7 Then
														Call lclsValTime.ValNumber(sEnd)
														'End If
													End If
												End If
											End If
										ElseIf UCase(Mid(lclsQuery.FieldToClass("sColumName"), 1, 1)) = "S" Then 
											sAtrib = "S"
										ElseIf UCase(Mid(lclsQuery.FieldToClass("sColumName"), 1, 1)) = "D" Then 
											sAtrib = "D"
											If Not IsDate(sInitial) Then
												Call lclsErrors.ErrorMessage(sCodispl, 1001)
											End If
											If nAritOper = 7 Then
												If sEnd = String.Empty Then
													Call lclsErrors.ErrorMessage(sCodispl, 8351)
												Else
													If Not IsDate(sEnd) Then
														Call lclsErrors.ErrorMessage(sCodispl, 1001)
													End If
												End If
											End If
										End If
									End If
									If lclsErrors.Confirm = String.Empty Then
										If nAritOper = 7 Then
											If sAtrib = "N" Then
												If CShort(sInitial) > CShort(sEnd) Then
													Call lclsErrors.ErrorMessage(sCodispl, 8352)
												End If
											ElseIf sAtrib = "S" Or sAtrib = "D" Then 
												If sInitial > sEnd Then
													Call lclsErrors.ErrorMessage(sCodispl, 8352)
												End If
											End If
										End If
									End If
									
								End If
								'**+ If the operator is Between and the final value must be full
								'+ Si el operador es Between y el valor final debe estar lleno
							End If
						End If
					End If
					
				End If
			End If
		End If
		
		insValLT030Upd = lclsErrors.Confirm
		
		lclsErrors = Nothing
		lclsGroupVar = Nothing
		lclsGroupParam = Nothing
		lclsQuery = Nothing
		lclsValTime = Nothing
		
		Exit Function
		lclsGroupParam = Nothing
		lclsErrors = Nothing
		lclsGroupVar = Nothing
		lclsQuery = Nothing
		lclsValTime = Nothing
	End Function
	
	'**%Objective:  create/update the record in the LettValues table.
	'**%Parameters:
	'**%  nAritOper    - Code of operation symbol sole values as per table 311.
	'**%  sValue       - Parameter or variable value.
	'**%  nParameters  - Parameter Code the possible values as per table 622.
	'**%  nLett_group  - Code of the variable group (Correspondence).
	'**%  nConsec      - Consecutive number identifying the parameter or variable order.
	'**%  nUsercode    - Code of the user creating or updating the record..
	'**%  sVariable    - Name of The Variable used in Correspondence.
	'**%  sAction       - Description of the action to execute.      -
	'**%  nLettRequest - Number of the request for remittance of  correspondence.
	'%Objetivo:Esta función se encarga de crear/actualizar los registros correspondientes en la
	'%         tabla de LettValues
	'%Parámetros:
	'%    nAritOper    - Código del tipo de operando Valores posibles según tabla 311.
	'%    sValue       - Valor del parámetro o de la variable.
	'%    nParameters  - Código del parámetro valores posibles según tabla 622.
	'%    nLett_group  - Código del grupo de variables (Correspondencia).
	'%    nConsec      - Consecutivo que identifica el orden del parámetro o variable.
	'%    nUsercode    - Código del usuario que crea o actualiza el registro.
	'%    sVariable    - Nombre de la variable utilizada en correspondencia.
	'%    sAction       - Descripción de la acción a ejecutarse.      -
	'%    nLettRequest - Número de solicitud de envío.
	Public Function insPostLT030(ByVal nLettRequest As Short, ByVal nConsec As Short, ByVal nLett_group As Short, ByVal nParameters As Short, ByVal sVariable As String, ByVal sValue As String, ByVal nUsercode As Short, ByVal nAritOper As Short, ByVal sAction As String) As Boolean
		Dim lclsLettRequest As LettRequest
		Dim lclsLettParam As LettParam
		Dim lcolLettParam As LettParams
		Dim ArrParam(6) As Short
		Dim nIndex As Short
		Dim lParameters As Short
		
		If Not IsIDEMode Then
		End If
		
		lclsLettRequest = New LettRequest
		lclsLettParam = New LettParam
		lcolLettParam = New LettParams
		
		Call lclsLettRequest.Find(nLettRequest)
		Call lcolLettParam.FindByLetter(lclsLettRequest.nLetterNum, lclsLettRequest.DinpDate)
		
		nIndex = 0
		For	Each lclsLettParam In lcolLettParam
			ArrParam(nIndex) = lclsLettParam.nParameters
			nIndex = nIndex + 1
		Next lclsLettParam
		
		With Me
			.nLettRequest = nLettRequest
			.nConsec = nConsec
			.nLett_group = nLett_group
			.nParameters = nParameters
			.sVariable = sVariable
			.sValue = sValue
			.nUsercode = nUsercode
			.nAritOper = nAritOper
		End With
		
		lParameters = 0
		
		If sAction = "Add" Then
			'**+ Obtain the parameter to which the group corresponds
			'+ Se obtiene el parámetro al cual corresponde el grupo
			For nIndex = 0 To lcolLettParam.Count
				If ArrParam(nIndex) = 0 Then
					If nLett_group = 13 Then
						lParameters = 0
					End If
				ElseIf ArrParam(nIndex) = 1 Then 
					If nLett_group = 60 Then
						lParameters = 1
					End If
				ElseIf ArrParam(nIndex) = 2 Then 
					If nLett_group = 14 Then
						lParameters = 2
					End If
				ElseIf ArrParam(nIndex) = 3 Then 
					If (nLett_group = 11 Or nLett_group = 12 Or nLett_group = 13 Or nLett_group = 20) Then
						lParameters = 3
					End If
				ElseIf ArrParam(nIndex) = 4 Then 
					If (nLett_group = 11 Or nLett_group = 12 Or nLett_group = 20 Or nLett_group = 30) Then
						lParameters = 4
					End If
				ElseIf ArrParam(nIndex) = 5 Then 
					If (nLett_group = 11 Or nLett_group = 12 Or nLett_group = 13 Or nLett_group = 20 Or nLett_group = 50) Then
						lParameters = 5
					End If
				ElseIf ArrParam(nIndex) = 6 Then 
					If (nLett_group = 11 Or nLett_group = 12 Or nLett_group = 13 Or nLett_group = 20 Or nLett_group = 50 Or nLett_group = 51) Then
						lParameters = 6
					End If
				End If
				If lParameters <> 0 Then
					Exit For
				End If
			Next nIndex
			
			Me.nParameters = lParameters
			insPostLT030 = Add
		ElseIf sAction = "Upd" Then 
			insPostLT030 = Update
		Else
			insPostLT030 = Delete
		End If
		
		lclsLettRequest = Nothing
		lclsLettParam = Nothing
		lcolLettParam = Nothing
		
		Exit Function
		lclsLettRequest = Nothing
		lclsLettParam = Nothing
		lcolLettParam = Nothing
	End Function
	
	'**Objective: Delete the information in the main table of the class.
	'%Objetivo: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete() As Boolean
		Dim lrecinsDelLettValues As eRemoteDB.Execute
		If Not IsIDEMode Then
		End If
		lrecinsDelLettValues = New eRemoteDB.Execute
		
		With lrecinsDelLettValues
			.StoredProcedure = "insDelLettValues"
			.Parameters.Add("nLettRequest", nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLett_group", nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		lrecinsDelLettValues = Nothing
		
		Exit Function
	End Function
	
	'**Objective: Initializes the class
	'%Objetivo: Inicializa la clase
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		nLettRequest = intNull
		nConsec = 0
		nLett_group = intNull
		nParameters = intNull
		sVariable = String.Empty
		sValue = String.Empty
		nUsercode = intNull
		nAritOper = intNull
		nStatusInstance = 0
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class











