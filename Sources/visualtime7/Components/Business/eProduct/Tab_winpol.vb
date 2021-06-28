Option Strict Off
Option Explicit On
Public Class Tab_winpol
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_winpol.cls                           $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 6/10/03 17.23                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Property                     Type          DBType       Size Scale Prec Null
	'+ ---------------------------- ------------- ------------ ---- ----- ---- ----
	Public nSequence As Integer ' NUMBER     22   0     5    N
	Public sCodispl As String ' CHAR       8    0     0    N
	Public sDefaulti As String ' CHAR       1    0     0    S
	Public sRequire As String ' CHAR       1    0     0    S
	Public sAutomatic As String ' CHAR       1    0     0    S
	
	'**-Auxiliary variables
	'- Variables auxiliares
	Private mlngUsercode As Integer
	Public sExist As String
	Public sDescript As String
	
	'% insPostMCA001: Se actualizan los datos en la tabla
	Public Function insPostMCA001(ByVal sBussityp As String, ByVal sPolitype As String, ByVal sCompon As String, ByVal sSequence As String, ByVal nTratypep As Integer, ByVal sCodispl As String, ByVal sRequire As String, ByVal sExist As String, ByVal sSelected As String, ByVal sAutomatic As String, ByVal nUsercode As Integer, ByVal sBrancht As String, ByVal nType_amend As Short) As Boolean
		Dim lrecTab_winpol As eRemoteDB.Execute
		
		On Error GoTo insPostMCA001_Err
		lrecTab_winpol = New eRemoteDB.Execute
		With lrecTab_winpol
			.StoredProcedure = "insPostMCA001"
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSequence", sSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomatic", sAutomatic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Amend", IIf(nType_amend = -32768, 0, nType_amend), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostMCA001 = .Run(False)
		End With
		
insPostMCA001_Err: 
		If Err.Number Then
			insPostMCA001 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_winpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_winpol = Nothing
	End Function
End Class






