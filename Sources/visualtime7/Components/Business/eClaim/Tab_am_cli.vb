Option Strict Off
Option Explicit On
Public Class Tab_am_cli
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_cli.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	Public nBranch As Integer
	Public nHospital As Integer
	Public nProduct As Integer
	Public dEffecdate As Date
	Public dNulldate As Date
	Public nUsercode As Integer
	
	Public nStatusInstance As Integer
	
	'% Find: verifica si existen datos en la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nHospital As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreatab_am_cli As New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreatab_am_cli = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_am_cli'
		'+ Información leída el 14/07/2001 04:48:01 p.m.
		
		Find = False
		
		With lrecreatab_am_cli
			.StoredProcedure = "reaTab_am_cli"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHospital", nHospital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Me.nHospital = .FieldToClass("nHospital")
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecreatab_am_cli = Nothing
	End Function
	
	'% Find_Count: retorna el número de registros en la tabla para la condición
	Public Function Find_Count(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_am_cli_2 As eRemoteDB.Execute
		
		On Error GoTo Find_Count_err
		
		lrecreaTab_am_cli_2 = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_am_cli_2'
		'+ Información leída el 14/07/2001 04:50:15 p.m.
		
		Find_Count = False
		
		With lrecreaTab_am_cli_2
			.StoredProcedure = "reaTab_am_cli_2"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Count = True
				nHospital = .FieldToClass("nHospital")
				.RCloseRec()
			End If
		End With
		
Find_Count_err: 
		If Err.Number Then
			Find_Count = False
		End If
		On Error GoTo 0
		lrecreaTab_am_cli_2 = Nothing
	End Function
End Class






