Option Strict Off
Option Explicit On
Public Class err_histor
	'%-------------------------------------------------------%'
	'% $Workfile:: err_histor.cls                           $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 22/08/03 16:07                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla en el sistema 27/06/2000
	'          Column_name                            Type              Length    Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nErrorNum As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)
	Public nConsecut As Integer 'smallint   no       2         5    0     no                                  (n/a)                               (n/a)
	Public sStat_error As String 'char       no       1                    yes                                 yes                                 yes
	Public dDate As Date 'datetime   no       8                    yes                                 (n/a)                               (n/a)
	Public sHour As String 'char       no       5                    yes                                 yes                                 yes
	Public sUser As String 'char       no      12                    yes                                 yes                                 yes
	Public sHour_user As String 'char       no       5                    yes                                 yes                                 yes
	Public nDays_user As Integer 'int        no       4        10    0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint   no       2         5    0     no                                  (n/a)                               (n/a)
	Public sDescript As String
	
	Public nLastConsecut As Integer
	Private mobjGrid As eFunctions.Grid
	
	
	
	'%Find: Busca si el registro Historico del error
	Public Function Find(ByVal nErrorNum As Integer, ByVal nConcecut As Integer) As Boolean
		Dim lrecReaErr_Histor1 As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecReaErr_Histor1 = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.ReaErr_Histor1'
		'Información leída el 27/06/2000 03:30:49 PM
		
		With lrecReaErr_Histor1
			.StoredProcedure = "reaErr_Histor1"
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsecut", nConcecut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			
			If .Run Then
				sStat_error = .FieldToClass("sStat_Error")
				nConsecut = .FieldToClass("nConsecut")
				sUser = .FieldToClass("sUser")
				dDate = .FieldToClass("dDate")
				.RCloseRec()
				Find = True
			End If
		End With
		
		lrecReaErr_Histor1 = Nothing
		
		Exit Function
	End Function
	
	'%FindLastHistor: Busca si el registro Historico del error tiene una transaccion anterior
	Public Function FindLastHistor(ByVal nErrorNum As Integer) As Boolean
		If Not IsIDEMode Then
		End If
		
		FindLastHistor = Find(nErrorNum, -1)
		nLastConsecut = nConsecut
		
		Exit Function
	End Function
	
	'%FReverseLastHistor: Reversa el ultimo movimiento del Error
	Public Function ReverseLastHistor() As Boolean
		Dim lrecdelErr_Histor1 As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecdelErr_Histor1 = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.delErr_Histor1'
		'Información leída el 27/06/2000 05:22:36 PM
		
		With lrecdelErr_Histor1
			.StoredProcedure = "delErr_Histor1"
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsecut", nLastConsecut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			
			ReverseLastHistor = .Run(False)
		End With
		
		lrecdelErr_Histor1 = Nothing
		
		Exit Function
	End Function
	
	'%Add: Crea un registro Historico del error
	Public Function Add() As Boolean
		Dim lreccreErr_Histor2 As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lreccreErr_Histor2 = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.creErr_Histor2'
		'Información leída el 06/06/2001 10:57:20
		
		With lreccreErr_Histor2
			.StoredProcedure = "creErr_Histor2"
			.Parameters.Add("nErrornum", Me.nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsecut", Me.nConsecut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate", Me.dDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour", Me.sHour, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUser", Me.sUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_error", Me.sStat_error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour_user", Me.sHour_user, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDays_User", Me.nDays_user, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			Add = .Run(False)
		End With
		
		lreccreErr_Histor2 = Nothing
		
		Exit Function
	End Function
	
	'% Delete: Borra el Error de las Tablas Err_Histor y de errors
	Public Function Delete() As Boolean
		Dim lrecdelErr_Histor As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecdelErr_Histor = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delErr_Histor'
		'+ Información leída el 07/06/2001 08:58:39 AM
		
		With lrecdelErr_Histor
			.StoredProcedure = "delErr_Histor"
			.Parameters.Add("ErrorNum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			
			Delete = .Run(False)
		End With
		
		lrecdelErr_Histor = Nothing
		
		Exit Function
	End Function
	
	'%ReaErr_Histor: Lee La Historia de un Error y genera el Grid de los registros
	Public Function ReaErr_Histor(ByVal nErrorNum As Integer) As String
		Dim lcolErr_Histor As Err_Histors
		Dim lclsErr_Histor As err_histor
		Dim lobjValues As eFunctions.Values
        Dim lstrstaterr As String
		Dim lstrArray As String
		Dim lintRow As Integer
		
		If Not IsIDEMode Then
		End If
		mobjGrid = New eFunctions.Grid
		lcolErr_Histor = New Err_Histors
		lclsErr_Histor = New err_histor
		lobjValues = New eFunctions.Values
		
		Call insDefineHeader()
		
		lstrstaterr = String.Empty
		lstrArray = String.Empty
		ReaErr_Histor = String.Empty
		lintRow = 0
		
		If lcolErr_Histor.Find(nErrorNum) Then
			For	Each lclsErr_Histor In lcolErr_Histor
				With lclsErr_Histor
					mobjGrid.Columns("sdate").DefValue = CStr(.dDate)
					mobjGrid.Columns("sUser").DefValue = Trim(.sUser)
					mobjGrid.Columns("sStatus").DefValue = Trim(.sDescript)
					ReaErr_Histor = ReaErr_Histor & mobjGrid.DoRow
				End With
			Next lclsErr_Histor
		End If
		ReaErr_Histor = ReaErr_Histor & mobjGrid.closeTable
		
		lcolErr_Histor = Nothing
		lclsErr_Histor = Nothing
		lobjValues = Nothing
		
		Exit Function
	End Function
	
	'% insDefineHeader: Define las Columnas del Grid
	Public Function insDefineHeader() As Boolean
		'+ Se definen las columnas del grid
		If Not IsIDEMode Then
		End If
		
		With mobjGrid.Columns
			Call .AddDateColumn(0, C_DATE, "sdate", String.Empty)
			Call .AddTextColumn(0, C_TSTATUS, "sStatus", 20, String.Empty)
			Call .AddTextColumn(0, C_TRESOURCE, "sUser", 12, String.Empty)
		End With
		
		'+ Se definen las propiedades generales del grid
		With mobjGrid
			.Codispl = "ER003"
			.Codisp = "ER003"
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
		End With
		
		Exit Function
	End Function
End Class











