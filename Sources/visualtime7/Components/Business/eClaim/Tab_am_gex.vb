Option Strict Off
Option Explicit On
Public Class Tab_am_gex
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_gex.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Public nExc_code As Integer
	Public dEffecdate As Date
	Public dExc_date As Date
	Public sIllness As String
	Public dNulldate As Date
	Public nUsercode As Integer
	
	Public nStatusInstance As Integer
	
	'% FindExc_Code:
	Public Function FindExc_Code(ByVal sIllness As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_am_gex As eRemoteDB.Execute
		
		On Error GoTo FindExc_Code_err
		
		lrecreaTab_am_gex = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_am_gex'
		'+ Información leída el 14/07/2001 04:16:33 p.m.
		
		FindExc_Code = False
		
		With lrecreaTab_am_gex
			.StoredProcedure = "reaTab_am_gex"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nExc_code = .FieldToClass("nExc_code")
				FindExc_Code = True
				.RCloseRec()
			End If
		End With
		lrecreaTab_am_gex = Nothing
		
FindExc_Code_err: 
		If Err.Number Then
			FindExc_Code = False
		End If
		On Error GoTo 0
	End Function
End Class






