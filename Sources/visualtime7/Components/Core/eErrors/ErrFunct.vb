Option Strict Off
Option Explicit On
Public Class ErrFunct
	
	'Column_name                        Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'---------------------------------- -------------------------------------------------------------------------------------------------------------------------------- ----------------------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- ----------------------------------- --------------------------------------------------------------------------------------------------------------------------------
	Public nErrornum As Integer 'int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)                               NULL
	Public nFunctspec As Short 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nStatrequest As Short 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nUsercode As Short 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public dCompDate As Date 'datetime                                                                                                                         no                                  8                       no                                  (n/a)                               (n/a)                               NULL
	Public sVersion As String 'char                                                                                                                             no                                  6                       yes                                 yes                                 yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sDs_Text As String 'text                                                                                                                             no                                  16                      yes                                 (n/a)                               (n/a)                               SQL_Latin1_General_CP1_CI_AS
	
	
	'%Add: Crea un registro de la versión funcional que debe ser modificada para la corrección del error.
	Public Function Add() As Boolean
		Dim lreccreErrFunct As eRemoteDB.Execute
		
		
		lreccreErrFunct = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.creErrFunct'
		
		With lreccreErrFunct
			.StoredProcedure = "creErrFunct"
			.Parameters.Add("nErrorNum", Me.nErrornum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunctspec", Me.nFunctspec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatRequest", Me.nStatrequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tDs_Text", Me.sDs_Text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVersion", Me.sVersion, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		lreccreErrFunct = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
	End Function
	
	
	'%Update: Actualiza la versión funcional del error en tratamiento.
	Public Function Update() As Boolean
		Dim lrecupdErrFunct As eRemoteDB.Execute
		
		lrecupdErrFunct = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updErrFunct'
		
		With lrecupdErrFunct
			.StoredProcedure = "updErrFunct"
			.Parameters.Add("nErrorNum", Me.nErrornum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunctspec", Me.nFunctspec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatRequest", Me.nStatrequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tDs_Text", Me.sDs_Text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVersion", Me.sVersion, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lrecupdErrFunct = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
	End Function
	
	
	'% Delete: Borra el registro de la tabla ErrFunct
	Public Function Delete() As Boolean
		Dim lrecdelErrFunct As eRemoteDB.Execute
		
		lrecdelErrFunct = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delErrFunct'
		
		With lrecdelErrFunct
			.StoredProcedure = "delErrFunct"
			.Parameters.Add("nErrornum", nErrornum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunctspec", nFunctspec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		lrecdelErrFunct = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
	End Function
	
	
	'% insValER006_K: Valida Los Campos de la Ventana ER006_K
	Public Function insValER006_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nErrornum As Integer, ByVal nFunctspec As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		'+ Si la acción es registrar no debe existir información en la tabla ErrFunct.
		If sAction = "Add" Then
            If nFunctspec <> eRemoteDB.Constants.intNull Then
                If valExistErrFunct(nErrornum, nFunctspec) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 20056)
                End If
            End If
		End If
		
		insValER006_K = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
insValER006_K_err: 
		If Err.Number Then
			insValER006_K = insValER006_K & Err.Description
		End If
	End Function
	
	
	'%valExistErrFunct: Valida la existencia de un registro con la misma clave.
	Public Function valExistErrFunct(ByVal nErrornum As Integer, ByVal nFunctspec As Short) As Boolean
		Dim lrecErrFunct As eRemoteDB.Execute
		
		lrecErrFunct = New eRemoteDB.Execute
		
		valExistErrFunct = False
		
		With lrecErrFunct
			.StoredProcedure = "valErrFunct"
			.Parameters.Add("nErrornum", nErrornum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunctspec", nFunctspec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		If lrecErrFunct.Run(True) Then
			If lrecErrFunct.FieldToClass("lCount") > 0 Then
				valExistErrFunct = True
			End If
            lrecErrFunct.RCloseRec()
		End If
		
		lrecErrFunct = Nothing
		
valExistErrFunct_Err: 
		If Err.Number Then
			valExistErrFunct = False
		End If
	End Function
	
	'*InsPostER006: Esta función se encarga de crear/actualizar registros en la tabla ErrFunct
	Public Function insPostER006(ByVal sAction As String, ByVal nErrornum As Integer, ByVal nFunctspec As Short, ByVal nStatrequest As Short, ByVal sDs_Text As String, ByVal sVersion As String, ByVal nUsercode As Short) As Boolean
		
		Me.nErrornum = nErrornum
		Me.nFunctspec = nFunctspec
		Me.nStatrequest = nStatrequest
		Me.sDs_Text = sDs_Text
		Me.sVersion = sVersion
		Me.nUsercode = nUsercode
		
		insPostER006 = True
		
		Select Case sAction
			
			'**+ If the selected option exists
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostER006 = Add()
				
				'**+  If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostER006 = Update()
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostER006 = Delete()
				
		End Select
		
insPostER006_err: 
		If Err.Number Then
			insPostER006 = False
		End If
	
	End Function
	
	
	'%Find:Levanta el Recordset con el registro encontrado en la tabla ErrFunct.
	Public Function Find(ByVal nErrornum As Integer, ByVal nFunctspec As Short) As Boolean
		Dim lrecreaErrFunct As eRemoteDB.Execute
		
		lrecreaErrFunct = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaErrFunct'
		With lrecreaErrFunct
			.StoredProcedure = "reaErrFunct"
			.Parameters.Add("nErrorNum", nErrornum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunctspec", nFunctspec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				nErrornum = .FieldToClass("nErrorNum")
				nFunctspec = .FieldToClass("nFunctspec")
				nStatrequest = .FieldToClass("nStatrequest")
				sVersion = .FieldToClass("sVersion")
				sDs_Text = .FieldToClass("tDs_text")
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		lrecreaErrFunct = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
	End Function
End Class











