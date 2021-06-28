Option Strict Off
Option Explicit On
Public Class Worksheet
	'%-------------------------------------------------------%'
	'% $Workfile:: Worksheet.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'Column_name       Type                   Length      Prec  Scale Nullable
	'----------------- ---------------------- ----------- ----- ----- ------------
	Public nProduct As Integer 'smallint   2           5     0     no
	Public nBranch As Integer 'smallint   2           5     0     no
	Public npolicy As Double 'int        4           10    0     no
	Public sValuesList As String 'char       1                       no
	Public nUsercode As Integer 'smallint   2           5     0     no
	Public nId As Integer 'int        4           10    0     no
	Public sDescript As String
	Public dCompdate As Date
	
	
	'%Find(). Esta funcion se encarga de buscar la hoja definida en la tabla WorkSheet
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double) As Boolean
		Dim lrecreaWorkSheet As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaWorkSheet = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaWorkSheet'
		'Información leída el 05/02/2001 02:35:15 p.m.
		
		With lrecreaWorkSheet
			.StoredProcedure = "reaWorkSheet"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", npolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				npolicy = .FieldToClass("nPolicy")
				nId = .FieldToClass("nId")
				sValuesList = .FieldToClass("sValuesList", "2")
				sDescript = .FieldToClass("sDescript")
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaWorkSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaWorkSheet = Nothing
		
	End Function
	
	'%Add(): Añade una Plantilla
	Public Function Add() As Boolean
		Dim lrecCreWorKSheet As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lrecCreWorKSheet = New eRemoteDB.Execute
		
		If sValuesList = String.Empty Then
			sValuesList = "2"
		End If
		
		'Definición de parámetros para stored procedure 'insudb.CreWorKSheet'
		'Información leída el 05/02/2001 02:41:16 p.m.
		
		With lrecCreWorKSheet
			.StoredProcedure = "CreWorKSheet"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", npolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValuesList", sValuesList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecCreWorKSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreWorKSheet = Nothing
		
	End Function
	
	'%Delete(): Elimina una Plantilla
	Public Function Delete() As Boolean
		Dim lrecRemoveWorkSheet As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecRemoveWorkSheet = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaWorkSheet'
		'Información leída el 31/01/2001 10:28:37 a.m.
		
		With lrecRemoveWorkSheet
			.StoredProcedure = "delWorkSheet"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecRemoveWorkSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRemoveWorkSheet = Nothing
	End Function
	
	'%FindWorksheet(). Esta funcion se encarga de mostrar el registro de la tabla WorkSheet
	Public Function FindWorksheet(ByVal nId As Integer) As Boolean
		Dim lrecreaWorkSheet As eRemoteDB.Execute
		
		On Error GoTo FindWorksheet_Err
		
		lrecreaWorkSheet = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaWorkSheet'
		'Información leída el 05/02/2001 02:35:15 p.m.
		
		With lrecreaWorkSheet
			.StoredProcedure = "reaWorkSheet_v"
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindWorksheet = True
				npolicy = .FieldToClass("nPolicy")
				sValuesList = .FieldToClass("sValuesList", "2")
				sDescript = .FieldToClass("sDescript")
				nProduct = .FieldToClass("nProduct")
				nBranch = .FieldToClass("nBranch")
				.RCloseRec()
			End If
		End With
		
FindWorksheet_Err: 
		If Err.Number Then
			FindWorksheet = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaWorkSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaWorkSheet = Nothing
		
	End Function
	
	'% Generate: Genera el Número de Plantilla
	Public Function Generate(ByVal nWorksheet As Integer, ByVal nUsercode As Integer) As Integer
		Dim lgenWorksheet As eGeneral.GeneralFunction
		
		On Error GoTo Generate_Err
		lgenWorksheet = New eGeneral.GeneralFunction
		If nWorksheet = eRemoteDB.Constants.intNull Then
			Generate = lgenWorksheet.Find_Numerator(63, 0, nUsercode)
		Else
			Generate = nWorksheet
		End If
		
Generate_Err: 
		If Err.Number Then
			Generate = -1
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lgenWorksheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lgenWorksheet = Nothing
	End Function
	
	'%Update(): Actualiza una Plantilla
	Public Function Update() As Boolean
		Dim lrecUpdWorKSheet As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecUpdWorKSheet = New eRemoteDB.Execute
		
		If sValuesList = String.Empty Then
			sValuesList = "2"
		End If
		
		'Definición de parámetros para stored procedure 'insudb.CreWorKSheet'
		'Información leída el 05/02/2001 02:41:16 p.m.
		
		With lrecUpdWorKSheet
			.StoredProcedure = "UpdWorKSheet"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", npolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValuesList", sValuesList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecUpdWorKSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdWorKSheet = Nothing
		
	End Function
End Class






