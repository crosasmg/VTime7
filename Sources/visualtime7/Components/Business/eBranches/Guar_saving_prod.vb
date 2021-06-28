Option Strict Off
Option Explicit On
Public Class Guar_saving_prod
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_riskInsu.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	Public nBranch As Integer
	Public nProduct As Integer
	Public dEffecdate As Date
	Public nGuarSav_Max As Integer
	Public nLower_min As Integer
	Public nValDate_Issue As Integer
	Public nValDate_last As Integer
	Public nQmin_prem As Integer
	Public sIndRenewal As String
	Public sRouReserve As String
	Public sRouGuarSave As String
	
	
	
	'%  Find: Busca un registron dentro de la tabla Disc_riskInsu
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsObject As eRemoteDB.Execute
		
		lclsObject = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSDP8005PKG.READP8005"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				nGuarSav_Max = .FieldToClass("nGuarSav_Max")
				nLower_min = .FieldToClass("nLower_min")
				nValDate_Issue = .FieldToClass("nValDate_Issue")
				nValDate_last = .FieldToClass("nValDate_Last")
				nQmin_prem = .FieldToClass("nQmin_prem")
				sIndRenewal = .FieldToClass("sIndRenewal")
				sRouReserve = .FieldToClass("sRouReserve")
				sRouGuarSave = .FieldToClass("sRouGuarSave")
				
				Find = True
				.RCloseRec()
			End If
		End With
Find_Err: 
		If Err.Number Then Find = False
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
	End Function
	
	
	Public Function insValDP8005(ByVal nGuarSav_Max As Integer, ByVal nLower_min As Double, ByVal nValDate_Issue As Integer, ByVal nValDate_last As Integer, ByVal nQmin_prem As Integer, ByVal sIndRenewal As String, ByVal sRouReserve As String, ByVal sRouGuarSave As String) As String
		Dim lclsObject As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
		
        Dim lstrErrorAll As String = String.Empty
		
		lclsObject = New eRemoteDB.Execute
		
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSDP8005PKG.VALDP8005"
			
			.Parameters.Add("nGuarSav_Max", nGuarSav_Max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLower_min", nLower_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValDate_Issue", nValDate_Issue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValDate_last", nValDate_last, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmin_prem", nQmin_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndRenewal", sIndRenewal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouReserve", sRouReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouGuarSave", sRouGuarSave, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayErrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrorAll = .Parameters("sArrayerrors").Value
			End If
			
		End With
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage("DP8005",  ,  ,  ,  ,  , lstrErrorAll)
			End If
			insValDP8005 = .Confirm
		End With
		
Find_Err: 
		If Err.Number Then insValDP8005 = Err.Description
		
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	Public Function insPostDP8005(ByVal nAction As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nGuarSav_Max As Integer, ByVal nLower_min As Double, ByVal nValDate_Issue As Integer, ByVal nValDate_last As Integer, ByVal nQmin_prem As Integer, ByVal sIndRenewal As String, ByVal sRouReserve As String, ByVal sRouGuarSave As String, ByVal nUsercode As Integer) As Boolean
		'---------------------------- --------------------------------------------------------
		Dim lclsObject As eRemoteDB.Execute
		
		lclsObject = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSDP8005PKG.UPDDP8005"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nGuarSav_Max", nGuarSav_Max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLower_min", nLower_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValDate_Issue", nValDate_Issue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValDate_last", nValDate_last, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmin_prem", nQmin_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndRenewal", sIndRenewal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouReserve", sRouReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouGuarSave", sRouGuarSave, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostDP8005 = .Run(False)
			
		End With
Find_Err: 
		If Err.Number Then insPostDP8005 = False
		
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		On Error GoTo 0
	End Function
End Class






