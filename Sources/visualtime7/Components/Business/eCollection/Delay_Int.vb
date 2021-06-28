Option Strict Off
Option Explicit On
Public Class Delay_Int
	'%-------------------------------------------------------%'
	'% $Workfile:: Delay_Int.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:29p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	'+     Column name                Type                            Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+     -------------------------  ------------------------------- ------ ----- ----- -------- ------------------ ---------------------
	Public nInit_Range As Double 'number     0      10    2     no       (n/a)              (n/a)
	Public nEnd_Range As Double 'number     0      10    2     no       (n/a)              (n/a)
	Public nPercent As Double 'number     0      5     2     no       (n/a)              (n/a)
	Public dCompdate As Date 'date                          no       (n/a)              (n/a)
	Public nUsercode As Integer 'number     0      5     0     no       (n/a)              (n/a)
	'% Find_Exist: Verifica si existe un rango.
	Public Function Find_Exist(ByVal Init_Range As Double, ByVal End_Range As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lcount As Integer
		Dim lrecreaDelay_Int As eRemoteDB.Execute
		
		lrecreaDelay_Int = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaDelay_Int_Range'
		
		With lrecreaDelay_Int
			.StoredProcedure = "reaDelay_Int_Range"
			.Parameters.Add("nInit_Range", Init_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Range", End_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Exist = True
			Else
				Find_Exist = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find_Exist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaDelay_Int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDelay_Int = Nothing
	End Function
	
	'% Add: Agrega un registro a la tabla de Delay_Int
	Public Function Add() As Boolean
		Dim lcreDelay_Int As eRemoteDB.Execute
		
		On Error GoTo Add_err
		lcreDelay_Int = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creDelay_Int'
		'+ Información leída el 11/10/2001
		
		With lcreDelay_Int
			.StoredProcedure = "creDelay_Int"
			.Parameters.Add("nInit_Range", nInit_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Range", nEnd_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lcreDelay_Int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcreDelay_Int = Nothing
	End Function
	
	'% Delete: Elimina un registro de la tabla Delay_Int
	Public Function Delete() As Boolean
		Dim ldelDelay_Int As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		ldelDelay_Int = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delDelay_Int'
		'+ Información leída el 11/10/2001
		
		With ldelDelay_Int
			.StoredProcedure = "delDelay_Int"
			.Parameters.Add("nInit_Range", nInit_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Range", nEnd_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object ldelDelay_Int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldelDelay_Int = Nothing
	End Function
	
	'% Update: Actualiza un registro de la tabla Delay_Int
	Public Function Update() As Boolean
		Dim lupdDelay_Int As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lupdDelay_Int = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updDelay_Int'
		'+ Información leída el 11/10/2001
		
		With lupdDelay_Int
			.StoredProcedure = "updDelay_Int"
			.Parameters.Add("nInit_Range", nInit_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Range", nEnd_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lupdDelay_Int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lupdDelay_Int = Nothing
	End Function
	
	'%insValMCO734: Esta función se encarga de validar los datos introducidos.
	Public Function insValMCO734(ByVal sCodispl As String, ByVal sAction As String, ByVal nInit_Range As Double, ByVal nEnd_Range As Double, ByVal nPercent As Double) As String
		Dim lerrTime As eFunctions.Errors
		
		lerrTime = New eFunctions.Errors
		
		On Error GoTo insValMCO734_Err
		
		'+Se efectuan las validaciones concernientes al rango inicial
		If nInit_Range = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 10182)
		End If
		
		'+Se realizan las validaciones concernientes al rango final
		If nEnd_Range = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 60003)
		End If
		
		'+Se realizan las validaciones concernientes al porcentaje
		If nPercent = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 60006)
		End If
		
		'+Se realizan las validaciones concernientes al rango final > rango inicial
		If nInit_Range <> eRemoteDB.Constants.intNull And nEnd_Range <> eRemoteDB.Constants.intNull Then
			If nEnd_Range <= nInit_Range Then
				Call lerrTime.ErrorMessage(sCodispl, 60005)
			End If
		End If
		
		'+Se realizan las validaciones concernientes a la existencia del rango o
		' contención en otro rango.
		Dim clsDelay_Int As eCollection.Delay_Int
		If sAction = "Add" Then
			clsDelay_Int = New eCollection.Delay_Int
			If clsDelay_Int.Find_Exist(nInit_Range, nEnd_Range) Then
				Call lerrTime.ErrorMessage(sCodispl, 11138)
			End If
		End If
		
		insValMCO734 = lerrTime.Confirm
		
insValMCO734_Err: 
		If Err.Number Then
			insValMCO734 = "insValMCO734: " & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object clsDelay_Int may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		clsDelay_Int = Nothing
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	'*InsPostMCO734: Esta función se encarga de crear/eliminar/modificar los registros
	'*correspondientes en la tabla Delay_Int
	Public Function insPostMCO734(ByVal sAction As String, Optional ByVal nInit_Range As Double = 0, Optional ByVal nEnd_Range As Double = 0, Optional ByVal nPercent As Double = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMCO734_err
		
		Me.nInit_Range = nInit_Range
		Me.nEnd_Range = nEnd_Range
		Me.nPercent = nPercent
		Me.nUsercode = nUsercode
		
		insPostMCO734 = True
		
		Select Case sAction
			
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMCO734 = Add()
				
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMCO734 = Delete()
				
				'+Si la opción seleccionada es Actualizar
			Case "Update"
				insPostMCO734 = Update()
				
		End Select
		
insPostMCO734_err: 
		If Err.Number Then
			insPostMCO734 = False
		End If
		On Error GoTo 0
		
	End Function
End Class






