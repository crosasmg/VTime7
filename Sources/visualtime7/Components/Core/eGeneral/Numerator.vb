Option Strict Off
Option Explicit On
Public Class Numerator
	'%-------------------------------------------------------%'
	'% $Workfile:: Numerator.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	Public nIndicator As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                                                                                                                         int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nNumerator As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                                                                                                                         int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public ntipo As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nTypenum As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nOrd_num As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public dCompdate As Date
	Public nEnd_num As Double 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                                                                                                                         int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nInitial As Double 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                                                                                                                         int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nLastnumb As Double 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                                                                                                                         int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)                                                                                                                         smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                                                                                                                         int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public sShort_des As String 'char       no       1                    yes                                 yes                                 yes
	Public sShort_des2 As String 'char       no       1                    yes                                 yes                                 yes
	
	'**% Validate: reads the numerator table and verifies that the Numerator table can be created
	'%Validate: lee de la tabla numerator y Verifica que se pueda crear en la tabla Numerator
	Public Function Validate(ByVal nNumerator As Integer) As Boolean
		
		'**- Variable definition for the execution and the handle of the SP
		'-Se define la variable para la ejecución y manejo del SP
		
		Dim ltempReaTable10 As eRemoteDB.Execute
		
		On Error GoTo Validate_err
		
		ltempReaTable10 = New eRemoteDB.Execute
		
		Validate = True
		
		With ltempReaTable10
			.StoredProcedure = "ValNumerator"
			.Parameters.Add("nNumerator", nNumerator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not .Run Then
				Validate = False
			Else
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object ltempReaTable10 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempReaTable10 = Nothing
		
Validate_err: 
		If Err.Number Then
			Validate = False
		End If
		On Error GoTo 0
		
	End Function
	'**% Add: Adds a record to the Numerator table
	'%Add: Añade un Registro a la tabla Numerator
	Public Function Add() As Boolean
		
		Dim ltempCreNumerator As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		ltempCreNumerator = New eRemoteDB.Execute
		
		With ltempCreNumerator
			
			.StoredProcedure = "creNumerator"
			
			.Parameters.Add("nTypenum", nTypenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrd_num", nOrd_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInitial", nInitial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd_num", nEnd_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLastnumb", nLastnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Add = True
			Else
				Add = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object ltempCreNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempCreNumerator = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Update: updates records on the Numerator table.
	'%Update: Actualiza un Registro a la tabla Numerator
	Public Function Update() As Boolean
		
		Dim ltempUpdNumerator As eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		ltempUpdNumerator = New eRemoteDB.Execute
		
		With ltempUpdNumerator
			
			.StoredProcedure = "UPDNUMERATOR_1"
			.Parameters.Add("nTypenum", nTypenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrd_num", nOrd_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLastnumb", nLastnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd_num", nEnd_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInitial", nInitial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Update = True
			Else
				Update = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object ltempUpdNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempUpdNumerator = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Delete: deletes a record from the Numerator table
	'%Delete: Borra un Registro a la tabla Numerator
	Public Function Delete() As Boolean
		
		Dim ltempDelNumerator As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		ltempDelNumerator = New eRemoteDB.Execute
		
		Delete = True
		
		With ltempDelNumerator
			.StoredProcedure = "DelNumerator"
			.Parameters.Add("nTypenum", nTypenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrd_num", nOrd_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not .Run(False) Then
				Delete = False
			End If
		End With
		
		'UPGRADE_NOTE: Object ltempDelNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempDelNumerator = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Find: locates a record in the numerator table
	'%Find: Localiza un Registro a la tabla Numerator
	Function Find(ByRef nTypenum As Integer, ByRef nOrd_num As Integer) As Boolean
		
		'**- Variable definition for the esecution and the handle of the SP
		'-Se define la variable para la ejecución y manejo del SP
		
		Dim ltempValnumerator As eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		ltempValnumerator = New eRemoteDB.Execute
		
		Find = True
		
		With ltempValnumerator
			.StoredProcedure = "reaNumerator"
			.Parameters.Add("ntypenum", nTypenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrd_num", nOrd_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nLastnumb = .FieldToClass("nLastnumb", 0)
				.RCloseRec()
			Else
				nLastnumb = 0
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object ltempValnumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempValnumerator = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	'**% MISSING
	'% insValMS003_K:
	Public Function insValMS003_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nTypenum As Integer, ByVal nOrd_num As Integer, ByVal nInitial As Double, ByVal nEnd_num As Double, ByVal nLastnumb As Double, ByVal nUsercode As Integer) As String
        'Se retorna Nothing para solucionar una advertencia de compilación mientras la funcion no este siendo utilizada al no tener declaraciones ni refrencias
        Return Nothing
    End Function
	'**%insPostMS003_K: This method updates the database (as described in the functional specifications)
	'**%for the page "Numerator"
	'%insPostMS003_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "Numerator"
	Public Function insPostMS003_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nTypenum As Integer, ByVal nOrd_num As Integer, ByVal nInitial As Double, ByVal nEnd_num As Double, ByVal nLastnumb As Double, ByVal nUsercode As Integer) As Boolean
		
	End Function
	
	'**% insValMS011_K: Validates the Numerator
	'% insValMS011_K: Valida el Numerator
	Public Function insValMS011_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nTypenum As Integer, ByVal nOrd_num As Integer, ByVal nInitial As Double, ByVal nEnd_num As Double, ByVal nLastnumb As Double, ByVal nUsercode As Integer) As String
		Dim lclsNumerator As eGeneral.GeneralFunction
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo insValMS011_K_err
		lclsNumerator = New eGeneral.GeneralFunction
		lclsErrors = New eFunctions.Errors
		
		If (nTypenum <> 7 And nTypenum <> 16) Then
			nOrd_num = IIf(nOrd_num = eRemoteDB.Constants.intNull, 0, nOrd_num)
		End If
		
		sAction = Trim(sAction)
		
		If sAction = "Add" Then
			If Find(nTypenum, nOrd_num) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10225)
			Else
				If (nTypenum = 7 Or nTypenum = 16) Then
					If nOrd_num = eRemoteDB.Constants.intNull Then
						Call lclsErrors.ErrorMessage(sCodispl, 3470)
					Else
						If nTypenum = 7 Then
							If Not lclsNumerator.Find_Table10(nOrd_num) Then
								Call lclsErrors.ErrorMessage(sCodispl, 10215)
							End If
						Else
							If Not lclsNumerator.Find_Table9(nOrd_num) Then
								Call lclsErrors.ErrorMessage(sCodispl, 10215)
							End If
						End If
					End If
				End If
			End If
		End If
		If sAction <> "Del" Then
			If nEnd_num = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 10296)
			Else
				If (nLastnumb > nEnd_num Or nLastnumb < nInitial) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10297)
				Else
					If nLastnumb >= (nEnd_num - 100) Then
						Call lclsErrors.ErrorMessage(sCodispl, 99139)
					End If
				End If
			End If
			If (nEnd_num < nInitial) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10216)
			End If
		End If
		insValMS011_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNumerator = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMS011_K_err: 
		If Err.Number Then
			insValMS011_K = insValMS011_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPostMS011_K: Updates the Numerator window
	'% insPostMS011_K: Actualiza la Ventana de Numerator
	Public Function insPostMS011_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nTypenum As Integer, ByVal nOrd_num As Integer, ByVal nInitial As Double, ByVal nEnd_num As Double, ByVal nLastnumb As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMS011_K_err
		
		sAction = Trim(sAction)
		
		With Me
			.nTypenum = nTypenum
			.nOrd_num = IIf(nOrd_num = eRemoteDB.Constants.intNull, 0, nOrd_num)
			.nInitial = IIf(nInitial = eRemoteDB.Constants.intNull, 0, nInitial)
			.nEnd_num = IIf(nEnd_num = eRemoteDB.Constants.intNull, 0, nEnd_num)
			.nLastnumb = IIf(nLastnumb = eRemoteDB.Constants.intNull, 0, nLastnumb)
			.nUsercode = nUsercode
		End With
		Select Case sAction
			
			'**- If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMS011_K = Add
				
				'**- If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMS011_K = Update
				
				'**+ If the selected option is Delete.
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMS011_K = Delete
				
		End Select
		
insPostMS011_K_err: 
		If Err.Number Then
			insPostMS011_K = False
		End If
		On Error GoTo 0
		
	End Function
End Class






