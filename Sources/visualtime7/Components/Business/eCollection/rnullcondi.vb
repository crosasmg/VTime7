Option Strict Off
Option Explicit On
Public Class rnullcondi
	'%-------------------------------------------------------%'
	'% $Workfile:: rnullcondi.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:29p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**+Properties according to the table 'rnullcondi' in the system 30/05/2002 04:53:31 p.m.
	'+ Propiedades según la tabla 'rnullcondi' en el sistema 30/05/2002 04:53:31 p.m.
	
	Public dEffecdate As Date
	Public nNullcode As Short
	Public nBranch As Integer
	Public nProduct As Integer
	Public sPolitype As String
	Public sPolicy As String
	Public sCertif As String
	Public nTratypei As Short
	
	'**- The variables are defined that will contain the maximum date of cancellation
	'**- and the Maximum date of effect for validation 11199
	'- Se definenn las variables que contendrán la maxima fecha de anulación y
	'- la máxima fecha de efecto para la validación 11199
	
	Public dMaxdEffecDate As Date
	Public dMaxdNullDate As Date
	
	'**- The variables are defined that will contain the maximum date of cancellation
	'**- and the Maximum date of effect for validation 11199
	'- Se definenn las variables que contendrán la maxima fecha de anulación y
	'- la máxima fecha de efecto para la validación 11199
	
	Private ldteMaxdEffecDate As Date
	Private ldteMaxdNullDate As Date
	
	
	
	'*%Add: Add a record to the table "rnullcondi"
	'% Add: Agrega un registro a la tabla "rnullcondi"
	Public Function Add(ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNullcode As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPolitype As String, ByVal sPolicy As String, ByVal sCertif As String, ByVal nTratypei As Short) As Boolean
		Dim lclsrnullcondi As eRemoteDB.Execute
		
		On Error GoTo AddMCO002_Err
		
		lclsrnullcondi = New eRemoteDB.Execute
		
		
		With lclsrnullcondi
			.StoredProcedure = "crernullcondi"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolicy", sPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertif", sCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
AddMCO002_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsrnullcondi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsrnullcondi = Nothing
	End Function
	
	'*%Update: updates a registry to the table "rnullcondi" using the key for this table.
	'% Update: Actualiza un registro a la tabla "rnullcondi" usando la clave para dicha tabla.
	Public Function Update(ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNullcode As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPolitype As String, ByVal sPolicy As String, ByVal sCertif As String, ByVal nTratypei As Short) As Boolean
		Dim lclsrnullcondi As eRemoteDB.Execute
		
		On Error GoTo UpdateMCO002_Err
		
		lclsrnullcondi = New eRemoteDB.Execute
		
		With lclsrnullcondi
			.StoredProcedure = "updrnullcondi"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolicy", sPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertif", sCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
UpdateMCO002_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsrnullcondi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsrnullcondi = Nothing
	End Function
	
	'*%Delete: Delete a registry the table "rnullcondi" using the key for this table.
	'% Delete: Elimina un registro a la tabla "rnullcondi" usando la clave para dicha tabla.
	Public Function Delete(ByVal dEffecdate As Date, ByVal nNullcode As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPolitype As String, ByVal nTratypei As Short, ByVal nUsercode As Integer) As Boolean
		Dim lclsrnullcondi As eRemoteDB.Execute
		
		On Error GoTo DeleteMCO002_Err
		
		lclsrnullcondi = New eRemoteDB.Execute
		
		
		With lclsrnullcondi
			.StoredProcedure = "delrnullcondi"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
DeleteMCO002_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsrnullcondi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsrnullcondi = Nothing
	End Function
	
	'*%IsExist: It verifies the existence of a registry in table "rnullcondi" using the key of this table.
	'% IsExist: Verifica la existencia de un registro en la tabla "rnullcondi" usando la clave de dicha tabla.
	Public Function IsExist(ByVal dEffecdate As Date, ByVal nNullcode As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPolitype As String, ByVal nTratypei As Short) As Boolean
		Dim lclsrnullcondi As eRemoteDB.Execute
		Dim lintExist As Short
		
		On Error GoTo IsExistMCO002_Err
		
		lclsrnullcondi = New eRemoteDB.Execute
		lintExist = 0
		
		With lclsrnullcondi
			.StoredProcedure = "rearnullcondi_v"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
IsExistMCO002_Err: 
		If Err.Number Then
			IsExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsrnullcondi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsrnullcondi = Nothing
	End Function
	
	'*%InsValMCO002_k: Validation of the data for the page of the headed one.
	'% InsValMCO002_k: Validación de los datos para la página del encabezado.
	Public Function InsValMCO002_k(ByVal pstrCodispl As String, ByVal plngMainAction As Integer, ByVal pstrAction As String, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim mdteUpdateDate As Date
		
		On Error GoTo InsValMCO002_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 4003)
		Else
			
			'**+ it found the Maximum date of effect, and the Maximum date of cancellation
			'+ Se busca la fecha máxima de efecto y la máxima de anulación
			If FindDateMaxrnullcondi() Then
				ldteMaxdEffecDate = dMaxdEffecDate
				ldteMaxdNullDate = dMaxdNullDate
				
				'**+ The validation becomes of which the date must be later or
				'**+ equal to the date of the last modification of the tariff
				'+ Se Valida que la fecha debe ser posterior o igual a la fecha
				'+ de la última modificación de la tarifa
				If ldteMaxdEffecDate > ldteMaxdNullDate Then
					mdteUpdateDate = ldteMaxdEffecDate
				Else
					mdteUpdateDate = ldteMaxdNullDate
				End If
				
				If dEffecdate < mdteUpdateDate And plngMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					Call lclsErrors.ErrorMessage(pstrCodispl, 11450)
				End If
			End If
		End If
		
		
		InsValMCO002_k = lclsErrors.Confirm
		
InsValMCO002_k_Err: 
		If Err.Number Then
			InsValMCO002_k = InsValMCO002_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'*%InsValMCO002: Validation of the data for the page details.
	'% InsValMCO002: Validación de los datos para la página detalle.
	Public Function InsValMCO002(ByVal pstrCodispl As String, ByVal plngMainAction As Integer, ByVal pstrAction As String, ByVal dEffecdate As Date, ByVal nNullcode As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPolitype As String, ByVal sPolicy As String, ByVal sCertif As String, ByVal nTratypei As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMCO002_Err
		
		lclsErrors = New eFunctions.Errors
		
		If (nNullcode = 0 Or nNullcode = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 10895)
		End If
		If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 9064)
		End If
		If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 1014)
		End If
		If sPolitype = String.Empty Or sPolitype = "0" Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 5565)
		End If
		If sPolicy = String.Empty Or sPolicy = "0" Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 5566)
		End If
		If sCertif = String.Empty Or sCertif = "0" Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 5567)
		End If
		If pstrAction = "Add" And IsExist(dEffecdate, nNullcode, nBranch, nProduct, sPolitype, nTratypei) Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 5564)
		End If
		If (nTratypei = 0 Or nTratypei = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 36202)
		End If
		
		InsValMCO002 = lclsErrors.Confirm
		
InsValMCO002_Err: 
		If Err.Number Then
			InsValMCO002 = InsValMCO002 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'*%InsPostMCO002: Pass of the information introduced towards the layers of rules of business and access of data.
	'% InsPostMCO002: Pase de la información introducida hacia las capas de reglas de negocio y acceso de datos.
	Public Function InsPostMCO002(ByVal pblnHeader As Boolean, ByVal pstrCodispl As String, ByVal plngMainAction As Integer, ByVal pstrAction As String, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNullcode As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPolitype As String, ByVal sPolicy As String, ByVal sCertif As String, ByVal nTratypei As Short) As Boolean
		
		If pblnHeader Then
			InsPostMCO002 = True
		Else
			If pstrAction = "Add" Then
				InsPostMCO002 = Add(nUsercode, dEffecdate, nNullcode, nBranch, nProduct, sPolitype, sPolicy, sCertif, nTratypei)
			ElseIf pstrAction = "Update" Then 
				InsPostMCO002 = Update(nUsercode, dEffecdate, nNullcode, nBranch, nProduct, sPolitype, sPolicy, sCertif, nTratypei)
			ElseIf pstrAction = "Del" Then 
				InsPostMCO002 = Delete(dEffecdate, nNullcode, nBranch, nProduct, sPolitype, nTratypei, nUsercode)
			End If
		End If
		
	End Function
	
	'**% FindDateMax: It selects the last date of modification of rNullcondi
	'% FindDateMax: Selecciona la última fecha de modificación de rNullcondi
	Public Function FindDateMaxrnullcondi() As Boolean
		Dim lrecvalRnullcondi As eRemoteDB.Execute
		
		lrecvalRnullcondi = New eRemoteDB.Execute
		
		FindDateMaxrnullcondi = False
		
		Dim dtmMaxEffecDate As Date
		
		
		With lrecvalRnullcondi
			.StoredProcedure = "valRnullcondi"
			.Parameters.Add("dMaxEffecDate", CStr(dtmMaxEffecDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMaxAnulDate", CStr(dtmMaxEffecDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				dMaxdEffecDate = IIf(IsDbNull(.Parameters.Item("dMaxEffecDate").Value), "1/1/1900", .Parameters.Item("dMaxEffecDate").Value)
				dMaxdNullDate = IIf((.Parameters.Item("dMaxAnulDate").Value) = CDate("01/01/1900"), Nothing, .Parameters.Item("dMaxAnulDate").Value)
				FindDateMaxrnullcondi = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecvalRnullcondi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalRnullcondi = Nothing
		
	End Function
End Class






