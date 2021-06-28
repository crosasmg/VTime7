Option Strict Off
Option Explicit On
Public Class Funds_Switch
	
	'-Propiedades según la tabla en el sistema el 08/07/2002
	
	'Column_name               Type
	'------------------------  -----------
	Public nOrigin As Integer
	Public nFromFunds As Integer
	Public sFromFunds As String
	Public nToFunds As Integer
	Public sToFunds As String
	Public nCount_Origin As Integer
	Public sDescript_Origin As String
	Public nStatRegt As Integer
	
	
	
	
	
	
	'% InsValMVI817: Realiza la validación puntual de los campos a actualizar en la ventana MVI817
	Public Function InsValMVI817Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nOrigin As Integer, ByVal nFromFunds As Integer, ByVal nToFunds As Integer, ByVal nStatRegt As Integer) As String
		Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
		Dim lrecMVI817 As eRemoteDB.Execute
		
		On Error GoTo InsValMVI817Upd_Err
		
		lrecMVI817 = New eRemoteDB.Execute
		With lrecMVI817
			.StoredProcedure = "INSMVI817PKG.INSVALMVI817UPD"
			.Parameters.Add("sAction", UCase(sAction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFromFunds", nFromFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nToFunds", nToFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrorAll = .Parameters("sArrayerrors").Value
			End If
		End With
		'UPGRADE_NOTE: Object lrecMVI817 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMVI817 = Nothing
		
		If Len(lstrErrorAll) > 0 Then
			lclsErrors = New eFunctions.Errors
			With lclsErrors
				.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrorAll)
				InsValMVI817Upd = .Confirm
			End With
		End If
		
InsValMVI817Upd_Err: 
		If Err.Number Then
			InsValMVI817Upd = "InsValMVI817Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lrecMVI817 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMVI817 = Nothing
	End Function
	
	'%InsPostMVI817Upd: Se realiza la actualización de los datos en la ventana MVI817
	Public Function InsPostMVI817Upd(ByVal sAction As String, ByVal nOrigin As Integer, ByVal nFromFunds As Integer, ByVal nToFunds As Integer, ByVal nStatRegt As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecMVI817 As eRemoteDB.Execute
		
		On Error GoTo InsPostMVI817Upd_Err
		
		lrecMVI817 = New eRemoteDB.Execute
		With lrecMVI817
			.StoredProcedure = "INSMVI817PKG.INSPOSTMVI817UPD"
			.Parameters.Add("sAction", UCase(sAction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFromFunds", nFromFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nToFunds", nToFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatRegt", nStatRegt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPostMVI817Upd = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecMVI817 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMVI817 = Nothing
		
InsPostMVI817Upd_Err: 
		If Err.Number Then
			InsPostMVI817Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find: Muestra la cantidad de cuentas origenes asociadas a la poliza
	Public Function Find_Origin(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecFunds_pol As eRemoteDB.Execute
		
		On Error GoTo Find_Origin_Err
		
		lrecFunds_pol = New eRemoteDB.Execute
		
		With lrecFunds_pol
			.StoredProcedure = "INSVI010pkg.Reafunds_pol_origin"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", nCount_Origin, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript_Origin, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_Origin = .Run(False)
			nCount_Origin = .Parameters("nCount").Value
			nOrigin = .Parameters("nOrigin").Value
			sDescript_Origin = .Parameters("sDescript").Value
		End With
		
Find_Origin_Err: 
		If Err.Number Then
			Find_Origin = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFunds_pol = Nothing
	End Function
End Class






