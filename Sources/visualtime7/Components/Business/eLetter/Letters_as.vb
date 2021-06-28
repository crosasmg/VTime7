Option Strict Off
Option Explicit On
Public Class Letters_as
	'**+Objetive: Clase generada a partir de la tabla 'LETTERS_AS' Modelos de cartas asociados a una ventana.Un registro por cada modelo de carta asociado a una ventana.
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'LETTERS_AS' Letter formats associated with a transaction window.A record per every letter format associated
	'+Version: $$Revision: 9 $
	
	'**-Objective: Description of the process.
	'-Objetivo: Descripción del proceso.
	Public ProcessDesc As String
	
	'**-Objective: Description of the branch.
	'-Objetivo: Descripción del ramo.
	Public BranchDesc As String
	
	'**-Objective: Description of the Sub-type coverage.
	'-Objetivo: Descripción del sub-tipo de covertura.
	'    Public Sub_typeDesc     As String
	
	'**-Objective: Description of the letter.
	'-Objetivo: Descripción de la carta.
	Public LetterDesc As String
	
	'**-Objective: Number consecutive.
	'-Objetivo: Número consecutivo de modelos de cartas asociados al elemento
	Public nConsec As Integer
	
	'**-Objective: Code of the window (logic).
	'-Objetivo: Código identificativo de la ventana (lógico).
	Public sCodispl As String
	
	'**-Objective: Process code Possible values according with the transaction window:CA001_k Table221, SI001 Table195
	'-Objetivo: Código del proceso.Valores posibles de acuerdo a la ventana indicada:CA001_k Table221, SI001 Table195
	Public nProcess As Short
	
	'**-Objective: Code of the Line of Business.The possible values as per table 10.
	'-Objetivo: Código del ramo comercial.Valores posibles según tabla 10.
	Public nBranch As Short
	
	'**-Objective: Number identifying the letter template.
	'-Objetivo: Código del modelo de carta.
	Public nLetterNum As Short
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Integer
	
	'**-Objective: Code of the status of the instance.
	'-Objetivo: Código del estado de la instancia.
	Public nStatusInstance As Short
	
	'**-Objective: indica si el letters template is requerido.
	'-Objetivo: indicated that the letter template is required.
	Public sRequired As String
	
	
	'**-Objective: Temporary variable, code that identifies the window (logical).
	'-Objetivo: Variable temporal, código identificativo de la ventana (lógico).
	Private mstrCodispl As String
	
	'**-Objective: Temporary variable, code of the branch
	'-Objetivo: Variable temporal, código del ramo
	Private mintBranch As Short
	
	'**-Objective: Temporary variable, code of the branch
	'-Objetivo: Variable temporal, código del proceso a la cual se asocia la correspondencia
	Private mintProcess As Short
	
	'**-Objective: Temporary variable, code of the model of of the correspondence
	'-Objetivo: Variable temporal, código del modelo de de la correspondencia
	Private mintLetterNum As Short
	
	'**-Objective: Temporary variable, indicates that the letter template is required for the transaction (logical).
	'-Objetivo: Variable temporal, que determina si el templates letter es requrido para la transacción (lógico).
	Private mstrRequired As String
	
	'**-Objective: Temporary variable, indicates that the letter template is required for the transaction (logical).
	'-Objetivo: Variable temporal, que determina si el templates letter es requrido para la transacción (lógico).
	Private mstrsDescrip As String
	
	
	'MODS
	Public sDescript As String
	Public nProduct As Short
	Public sRoutine As String
	'VT MODS:Added another variable sDescript for correspondence screen SI119
	Public nLanguage As Short
	
	
	'**%Objetivo: This method is in charge to make the search of the corresponding data for
	'**%          table "Letters_as". Giving back true or false depending on the existence or not on the data
	'**%Parameters:
	'**%  sCodispl    - Code of the window (logic).
	'**%  nBranch     - Code of the Line of Business.The possible values as per table 10.
	'**%  nProcess    - Process code Possible values according with the transaction window:CA001_k Table221, SI001 Table195
	'**%  nLetternum  - Number identifying the letter template
	'**%  nProduct    - Code of the product
	'**%  lblnAll     - Variable of boolean condition, it belongs to the conditional block
	'%Objective: Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%           tabla "Letters_as". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	'%Parámetros:
	'%  sCodispl      - Código identificativo de la ventana (lógico).
	'%  nBranch       - Código del ramo comercial.Valores posibles según tabla 10.
	'%  nProcess      - Código del proceso.Valores posibles de acuerdo a la ventana indicada:CA001_k Table221, SI001 Table195
	'%  nLetternum    - Código del modelo de carta
	'%  nProduct      - Código del producto
	'%  lblnAll       - Variable de condición booleana, pertenece al bloque condicional
	Private Function Find(ByVal sCodispl As String, ByVal nBranch As Short, ByVal nProcess As Short, ByVal nLetterNum As Short, ByVal nProduct As Short, Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecLetters_as As New eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecLetters_as = New eRemoteDB.Execute
		
		If mstrCodispl <> sCodispl Or mintBranch <> nBranch Or mintProcess <> nProcess Or mintLetterNum <> nLetterNum Or lblnAll Then
			Find = False
			With lrecLetters_as
				.StoredProcedure = "reaLetters_as"
				.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nConsec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProcess", nProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nConsec = .FieldToClass("nConsec")
					sCodispl = .FieldToClass("sCodispl")
					nProcess = .FieldToClass("nProcess")
					nBranch = .FieldToClass("nBranch")
					nLetterNum = .FieldToClass("nLetterNum")
					nUsercode = .FieldToClass("nUsercode")
					sRequired = .FieldToClass("sRequired")
					sDescript = .FieldToClass("sDescript")
					Find = True
					.RCloseRec()
					mstrCodispl = Me.sCodispl
					mintBranch = Me.nBranch
					mintProcess = Me.nProcess
					mintLetterNum = Me.nLetterNum
					mstrRequired = Me.sRequired
					mstrsDescrip = Me.sDescript
				End If
			End With
		Else
			Find = True
		End If
		
		lrecLetters_as = Nothing
		
		Exit Function
		lrecLetters_as = Nothing
	End Function
	
	'**%Objective: Adds a new registry to the Letters_As table
	'%Objetivo: Adiciona un nuevo registro a la tabla Letters_As
	Public Function Add() As Boolean
		
		If Not IsIDEMode Then
		End If
		
		Add = insUpdLetters_as(1)
		
		Exit Function
	End Function
	
	'**%Objective: Update a registry to the Letters_As table
	'%Objetivo: Actualiza un registro a la tabla Letters_As
	Public Function Update() As Boolean
		If Not IsIDEMode Then
		End If
		
		Update = insUpdLetters_as(2)
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry to the Letters_As table
	'%Objetivo: Elimina un registro a la tabla Letters_As
	Public Function Delete() As Boolean
		If Not IsIDEMode Then
		End If
		
		Delete = insUpdLetters_as(3)
		
		Exit Function
	End Function
	
	'**%Objective: This function is in charge to heighten the validations of window LT002.(Only the headed)
	'**%Parameters:
	'**%  sCodispl     - Code of the window (logic).
	'**%  sTransaction - Description of the transaction
	'%Objetivo: Esta funcion se encarga de realzar las validaciones de la ventana LT002.(Solo el encabezado)
	'%Parámetros:
	'%  sCodispl      - Código identificativo de la ventana (lógico).
	'%  sTransaction  - Descripción de la transacción
	Public Function insValMLT002_K(ByVal sCodispl As String, ByVal sTransaction As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		
		If Not IsIDEMode Then
		End If
		
		insValMLT002_K = String.Empty
		
		lobjErrors = New eFunctions.Errors
		If sTransaction = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 8021)
		Else
			lobjValues = New eFunctions.Values
			If Not lobjValues.IsValid("TabWindows", sTransaction) Then
				Call lobjErrors.ErrorMessage(sCodispl, 12014)
			End If
			lobjValues = Nothing
		End If
		
		insValMLT002_K = lobjErrors.Confirm
		lobjErrors = Nothing
		
		Exit Function
		lobjErrors = Nothing
	End Function
	
	'**%Objective: This function is in charge to heighten the validations of window LT002.
	'**%Parameters:
	'**%  sCodispl      - Code of the window (logic).
	'**%  sAction       - Description of the action to execute.
	'**%  sTransaction  - Description of the transaction
	'**%  nProcess      - Process code Possible values according with the transaction window:CA001_k Table221, SI001 Table195
	'**%  nBranch       - Code of the Line of Business.The possible values as per table 10.
	'**%  nLetternum    - Number identifying the letter template.
	'%Objetivo: Esta funcion se encarga de realzar las validaciones de la ventana LT002.
	'%Parámetros:
	'%  sCodispl       - Código identificativo de la ventana (lógico).
	'%  sAction        - Descripción de la acción a ejecutarse.
	'%  sTransaction   - Descripción de la transacción
	'%  nProcess       - Código del proceso.Valores posibles de acuerdo a la ventana indicada:CA001_k Table221, SI001 Table195
	'%  nBranch        - Código del ramo comercial.Valores posibles según tabla 10.'
	'%  nLetternum     - Código del modelo de carta.
	Public Function insValMLT002(ByVal sCodispl As String, ByVal sAction As String, ByVal sTransaction As String, ByVal nProcess As Short, ByVal nBranch As Short, ByVal nLetterNum As Short, ByVal nProduct As Short) As String
		Dim lobjErrors As eFunctions.Errors

		If Not IsIDEMode Then
		End If
		
		insValMLT002 = String.Empty
		
		
		'LAS VALIDACIONES 3786 Y 8046 FUERON ELIMINADAS EN EL FUNCIONAL
		
		
		lobjErrors = New eFunctions.Errors
		If nLetterNum = intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 8075)
		Else
			If UCase(sAction) = "ADD" Then
				If Find(sTransaction, nBranch, nProcess, nLetterNum, nProduct) Then
					Call lobjErrors.ErrorMessage(sCodispl, 8049)
				End If
			End If
			If UCase(sAction) = "UPD" Then
				If Find(sTransaction, nBranch, nProcess, nLetterNum, nProduct) Then
					Call lobjErrors.ErrorMessage(sCodispl, 8049)
				End If
			End If
		End If
		
		insValMLT002 = lobjErrors.Confirm
		lobjErrors = Nothing
		
		Exit Function
		lobjErrors = Nothing
	End Function
	
	'**%Objective: This function is in charge to make the update of window LT002.
	'**%Parameters:
	'**%  sAction       - Description of the action to execute.
	'**%  sTransaction  - Description of the transaction
	'**%  nProcess      - Process code Possible values according with the transaction window:CA001_k Table221, SI001 Table195
	'**%  nBranch       - Code of the Line of Business.The possible values as per table 10.
	'**%  nLetternum    - Number identifying the letter template.
	'**%  nConsec       - Consecutive number of window LT002
	'**%  nUsercode     - Code of users
	'%Objetivo: Esta funcion se encarga de realizar la actualizacion de la ventana LT002
	'%Parámetros:
	'%  sAction        - Descripción de la acción a ejecutarse.
	'%  sTransaction   - Descripción de la transacciónn
	'%  nProcess       - Código del proceso.Valores posibles de acuerdo a la ventana indicada:CA001_k Table221, SI001 Table195
	'%  nBranch        - Código del ramo comercial.Valores posibles según tabla 10.
	'%  nLetternum     - Código del modelo de carta.
	'%  nConsec        - Número consecutivo de la ventana LT002
	'%  nUsercode      - Código del usuario
    Public Function insPostLT002(ByVal sAction As String, _
                                 ByVal sTransaction As String, _
                                 ByVal nProcess As Short, _
                                 ByVal nBranch As Short, _
                                 ByVal nProduct As Short, _
                                 ByVal nLetterNum As Short, _
                                 ByVal sRoutine As String, _
                                 ByVal nConsec As Integer, _
                                 ByVal nUsercode As Integer, _
                                 ByVal sRequired As String) As Boolean

        If Not IsIDEMode() Then
        End If

        With Me
            .nUsercode = nUsercode
            .sCodispl = sTransaction
            .nProcess = nProcess
            'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            If IsNothing(nBranch) Then
                .nBranch = intNull
            Else
                .nBranch = nBranch
            End If
            'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            If IsNothing(nProduct) Or nProduct <= 0 Then
                .nProduct = intNull
            Else
                .nProduct = nProduct
            End If
            .nLetterNum = nLetterNum
            'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            If IsNothing(sRoutine) Then
                .sRoutine = String.Empty
            Else
                .sRoutine = sRoutine
            End If
            .nConsec = nConsec
            If sRequired = "" Then
                .sRequired = CStr(2)
            Else
                .sRequired = sRequired
            End If

            Select Case UCase(sAction)
                Case "ADD"
                    .nStatusInstance = 1
                    insPostLT002 = Add()
                Case "UPD"
                    .nStatusInstance = 2
                    insPostLT002 = Update()
                Case "DEL"
                    .nStatusInstance = 3
                    insPostLT002 = Delete()
            End Select
        End With

        Exit Function
    End Function
	
	'**%Objective: It has like objective the one to update or the registries of the Letters_As table.
	'**%Parameters:
	'**%  lintAction - Number of the action that this executing.
	'%Objetivo: Tiene como objetivo la de actualizar el o los registros de la tabla Letters_As.
	'%Parámetros:
	'%  lintAction  - Número de la acción que se esta ejecutando.
	Private Function insUpdLetters_as(ByVal lintAction As Short) As Boolean
		Dim lrecLetters_as As New eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecLetters_as = New eRemoteDB.Execute
		
		With lrecLetters_as
			.StoredProcedure = "insUpdLetters_as"
			.Parameters.Add("nAction", lintAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProcess", nProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequired", sRequired, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdLetters_as = .Run(False)
		End With
		lrecLetters_as = Nothing
		
		Exit Function
		lrecLetters_as = Nothing
	End Function
End Class











