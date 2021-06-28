Option Strict Off
Option Explicit On
Option Compare Text
Public Class Index_Cover
	'%-------------------------------------------------------%'
	'% $Workfile:: Index_Cover.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Public nYear As Integer
	Public dEffecdate As Date
	Public nIndexLiab As Double
	Public nIndexAssets As Double
	Public nUserCode As Integer
	
	'% insValMSI020_k: se realizan las validaciones del encabezado de la tx SI020
	Public Function insValMSI020_K(ByVal sCodispl As String, ByVal sAction As String, ByVal dEffecdate As Date) As String
        Dim lstrErrorAll As String = ""
        Dim lclsErrors As eFunctions.Errors
		Dim lrecinsValMSI020 As eRemoteDB.Execute
		
		On Error GoTo insValMSI020_Err
		
		lrecinsValMSI020 = New eRemoteDB.Execute
		
		'+ Se invoca el SP para validar los campos de la transacción
		
		With lrecinsValMSI020
			.StoredProcedure = "INSMSI020PKG.insValMsi020_K"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrorAll = .Parameters("sArrayerrors").Value
			End If
		End With
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage("MSI020",  ,  ,  ,  ,  , lstrErrorAll)
			End If
			insValMSI020_K = .Confirm
		End With
		
insValMSI020_Err: 
		If Err.Number Then
			insValMSI020_K = "insValMSI020: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lrecinsValMSI020 = Nothing
	End Function
	'% insValMSI020: se realizan las validaciones de la popup de la tx SI020
	Public Function insValMSI020Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nYear As Integer, ByVal dEffecdate As Date, ByVal nIndexLiab As Double, ByVal nIndexAssets As Double) As String
        Dim lstrErrorAll As String = ""
        Dim lclsErrors As eFunctions.Errors
		Dim lrecinsValMSI020Upd As eRemoteDB.Execute
		
		On Error GoTo insValMSI020Upd_Err
		
		lrecinsValMSI020Upd = New eRemoteDB.Execute
		
		'+ Se invoca el SP para validar los campos de la transacción
		
		With lrecinsValMSI020Upd
			.StoredProcedure = "INSMSI020PKG.insValMsi020Upd"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndexLiab", nIndexLiab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 8, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndexAssets", nIndexAssets, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 8, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrorAll = .Parameters("sArrayerrors").Value
			End If
		End With
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage("MSI020",  ,  ,  ,  ,  , lstrErrorAll)
			End If
			insValMSI020Upd = .Confirm
		End With
		
insValMSI020Upd_Err: 
		If Err.Number Then
			insValMSI020Upd = "insValMSI020Upd: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lrecinsValMSI020Upd = Nothing
	End Function
	
	'% insPostMSI020Upd: Se realiza la actualización de los datos
	Public Function insPostMSI020Upd(ByVal sAction As String, ByVal nYear As Integer, ByVal dEffecdate As Date, ByVal nIndexLiab As Double, ByVal nIndexAssets As Double, ByVal nUserCode As Integer) As Boolean
		Dim lrecinsPostMSI020Upd As eRemoteDB.Execute
		
		On Error GoTo insPostMSI020Upd_Err
		lrecinsPostMSI020Upd = New eRemoteDB.Execute
		
		With lrecinsPostMSI020Upd
			
			.StoredProcedure = "INSMSI020PKG.insPostMsi020Upd"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndexLiab", nIndexLiab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 8, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndexAssets", nIndexAssets, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 8, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostMSI020Upd = .Run(False)
			
		End With
		
		lrecinsPostMSI020Upd = Nothing
		
insPostMSI020Upd_Err: 
		If Err.Number Then
			insPostMSI020Upd = False
		End If
		On Error GoTo 0
	End Function
End Class






