Option Strict Off
Option Explicit On
Public Class Tab_am_exc
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_exc.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Public sCerType As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public nTariff As Integer
	Public sIllness As String
	Public dEffecdate As Date
	Public sClient As String
	Public nExc_code As Integer
	Public dInit_date As Date
	Public dNulldate As Date
	Public nUsercode As Integer
	Public dEnd_date As Date
	
	Public nStatusInstance As Integer
	
	'% FindExc_Code:
	Public Function FindExc_Code(ByVal sIllness As String, ByVal nPolicy As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_am_Exection As eRemoteDB.Execute
		
		On Error GoTo FindExc_Code_err
		
		lrecreaTab_am_Exection = New eRemoteDB.Execute
		
		FindExc_Code = False
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_am_Exection'
		'+ Información leída el 14/07/2001 04:10:30 p.m.
		
		With lrecreaTab_am_Exection
			.StoredProcedure = "reaTab_am_Exection"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nExc_code = .FieldToClass("nExc_code")
				FindExc_Code = True
				.RCloseRec()
			End If
		End With
		
FindExc_Code_err: 
		If Err.Number Then
			FindExc_Code = False
		End If
		On Error GoTo 0
		
		lrecreaTab_am_Exection = Nothing
	End Function
End Class






