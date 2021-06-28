Option Strict Off
Option Explicit On
Public Class SchemaFolder
	Public sScheCode As String
	Public nFolder As Integer
	Public nInqlevel As Integer
	Public sPermitted As String
	Public nUsercode As Integer
	
	
	
	
	'PostSG855:
	Public Function PostSG855(ByVal sScheCode As String, ByVal nFolder As Integer, ByVal nInqlevel As String, ByVal sPermitted As String, ByVal nUsercode As Integer, ByVal sAction As String) As Boolean
		Dim lrecPostSG855 As eRemoteDB.Execute
		On Error GoTo err_h
		
		lrecPostSG855 = New eRemoteDB.Execute
		
		With lrecPostSG855
			
			.StoredProcedure = "INSPOSTSG855"
			.Parameters.Add("SSCHE_CODE", sScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFolder", nFolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInqlevel", nInqlevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPermitted", sPermitted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NUSERCODE", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			PostSG855 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecPostSG855 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPostSG855 = Nothing
		
		Exit Function
err_h: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("SchemaFolder.PostSG855(sScheCode, nFolder, nInqlevel, sPermitted, nUserCode, sAction)", New Object(){sScheCode, nFolder, nInqlevel, sPermitted, nUsercode, sAction})
	End Function
	
	'ValSG021:
	Public Function ValSG855(ByVal sScheCode As String, ByVal nFolder As Integer, ByVal nInqlevel As String, ByVal sPermitted As String, ByVal nUsercode As Integer, ByVal sAction As String) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo err_h
		lerrTime = New eFunctions.Errors
		
		
		If nFolder <= 0 Then
			Call lerrTime.ErrorMessage("SG855", 197806)
		End If
		
		If CDbl(nInqlevel) < 0 Or CDbl(nInqlevel) > 9 Then
			Call lerrTime.ErrorMessage("SG855", 197807)
		End If
		
		ValSG855 = lerrTime.Confirm
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
		Exit Function
err_h: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("SchemaFolder.PostSG855(sScheCode, nFolder, nInqlevel, sPermitted, nUserCode, sAction)", New Object(){sScheCode, nFolder, nInqlevel, sPermitted, nUsercode, sAction})
		
	End Function
End Class






