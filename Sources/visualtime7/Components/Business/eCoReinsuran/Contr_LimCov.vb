Option Strict Off
Option Explicit On
Public Class Contr_LimCov
	'%-------------------------------------------------------%'
	'% $Workfile:: Contr_LimCov.cls                         $%'
	'% $Author:: Pgarin                                     $%'
	'% $Date:: 27/03/06 19:27                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla Contr_limCov al 03-25-2002 12:41:08
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nInsur_area As Integer ' NUMBER     22   0     5    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nAmount As Double ' NUMBER     22   2     10   S
	Public nLines As Double ' NUMBER     22   0     5    S
	Public nQuota_sha As Double ' NUMBER     22   2     4    S
	Public sRoutine As String ' CHAR       12   0     0    S
	Public nCoverApp As Integer ' NUMBER     22   0     5    S
	Public nPercent As Double ' NUMBER     22   2     5    S
	Public nMaxAmount As Double ' NUMBER     22   2     5    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	Public sLimitCov As Object
	'- Tipo registro
	Private Structure udtContr_LimCov
		Dim nNumber As Integer
		Dim nBranch_rei As Integer
		Dim nType As Integer
		Dim nInsur_area As Integer
		Dim nCovergen As Integer
		Dim dEffecdate As Date
		Dim dNulldate As Date
		Dim nAmount As Double
		Dim nLines As Double
		Dim nQuota_sha As Double
		Dim sRoutine As String
		Dim nCoverApp As Integer
		Dim nPercent As Double
		Dim nMaxAmount As Double
		Dim dCompdate As Date
		Dim nUsercode As Integer
		Dim sLimitCov As String
	End Structure
	
	'- Arreglo
	
	Private arrContr_LimCov() As udtContr_LimCov
	
	'+ DelNullContr_limCov :anula o elimina todos los registros asociados a un
	'+ contrato de la tabla de Límites por cobertura de un contrato proporcional
	Function DelNullContr_limCov(ByVal nNumber As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nAction As Integer) As Object
		Dim lrecDelNullContr_limCov As eRemoteDB.Execute
		
		lrecDelNullContr_limCov = New eRemoteDB.Execute
		On Error GoTo DelNullContr_limCov_Err
		
		With lrecDelNullContr_limCov
			.StoredProcedure = "insdelnullContr_limCov"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelNullContr_limCov = .Run(False)
		End With
DelNullContr_limCov_Err: 
		If Err.Number Then
			DelNullContr_limCov = False
		End If
		'UPGRADE_NOTE: Object lrecDelNullContr_limCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelNullContr_limCov = Nothing
		On Error GoTo 0
	End Function
	
	'% ReaContr_LimCov: Realiza la lectura de la tabla Contr_LimCov y almecena la data en un arreglo - ACM - 09/09/2002
	Public Function ReaContr_LimCov(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, Optional ByVal nInsur_area As Integer = eRemoteDB.Constants.intNull, Optional ByVal nCovergen As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecReaContr_LimCov As New eRemoteDB.Execute
		Dim lintCounter As Integer
		
		On Error GoTo ReaContr_LimCov_err
		
		lintCounter = 0
		
		With lrecReaContr_LimCov
			.StoredProcedure = "ReaContr_LimCov"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_Rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverGen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				ReDim arrContr_LimCov(50)
				Do While Not .EOF
					lintCounter = lintCounter + 1
					arrContr_LimCov(lintCounter).nNumber = .FieldToClass("nNumber")
					arrContr_LimCov(lintCounter).nBranch_rei = .FieldToClass("nBranch_Rei")
					arrContr_LimCov(lintCounter).nType = .FieldToClass("nType")
					arrContr_LimCov(lintCounter).nInsur_area = .FieldToClass("nInsur_Area")
					arrContr_LimCov(lintCounter).nCovergen = .FieldToClass("nCoverGen")
					arrContr_LimCov(lintCounter).dEffecdate = .FieldToClass("dEffecdate")
					arrContr_LimCov(lintCounter).dNulldate = .FieldToClass("dNulldate")
					arrContr_LimCov(lintCounter).nAmount = .FieldToClass("nAmount")
					arrContr_LimCov(lintCounter).nLines = .FieldToClass("nLines")
					arrContr_LimCov(lintCounter).nQuota_sha = .FieldToClass("nQuota_sha")
					arrContr_LimCov(lintCounter).sRoutine = .FieldToClass("sRoutine")
					arrContr_LimCov(lintCounter).nCoverApp = .FieldToClass("nCoverapp")
					arrContr_LimCov(lintCounter).nPercent = .FieldToClass("nPercent")
					arrContr_LimCov(lintCounter).nMaxAmount = .FieldToClass("nMaxAmount")
					arrContr_LimCov(lintCounter).dCompdate = .FieldToClass("dCompdate")
					arrContr_LimCov(lintCounter).nUsercode = .FieldToClass("nUsercode")
					arrContr_LimCov(lintCounter).sLimitCov = .FieldToClass("sLimitCov")
					.RNext()
				Loop 
				ReDim Preserve arrContr_LimCov(lintCounter)
				ReaContr_LimCov = True
			Else
				ReaContr_LimCov = False
			End If
		End With
		
ReaContr_LimCov_err: 
		If Err.Number Then
			ReaContr_LimCov = False
		End If
		'UPGRADE_NOTE: Object lrecReaContr_LimCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaContr_LimCov = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Count: Obtiene la cantidad de registros habidos en el arreglo - ACM - 09/09/2002
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(arrContr_LimCov)
		End Get
	End Property
	
	'% Count: Obtiene la cantidad de registros habidos en el arreglo - ACM - 09/09/2002
	Public Function Item_CR724(ByVal nIndex As Integer) As Boolean
		
		On Error GoTo Item_CR724_err
		
		If Me.Count > 0 Then
			With arrContr_LimCov(nIndex)
				Me.nNumber = .nNumber
				Me.nBranch_rei = .nBranch_rei
				Me.nType = .nType
				Me.nInsur_area = .nInsur_area
				Me.nCovergen = .nCovergen
				Me.dEffecdate = .dEffecdate
				Me.dNulldate = .dNulldate
				Me.nAmount = .nAmount
				Me.nLines = .nLines
				Me.nQuota_sha = .nQuota_sha
				Me.sRoutine = .sRoutine
				Me.nCoverApp = .nCoverApp
				Me.nPercent = .nPercent
				Me.nMaxAmount = .nMaxAmount
				Me.dCompdate = .dCompdate
				Me.nUsercode = .nUsercode
				Me.sLimitCov = .sLimitCov
				
			End With
			Item_CR724 = True
		Else
			Item_CR724 = False
		End If
		
Item_CR724_err: 
		If Err.Number Then
			Item_CR724 = False
		End If
	End Function
	
	'% ValCR724: Validaciones de la transacción.
	Public Function ValCR724(ByVal sCodispl As String, ByVal nCoverType As Integer, ByVal nCover As Integer, ByVal nContractType As Integer, ByVal nAmount As Double, ByVal nRelatedCover As Integer, ByVal nPercent_RelatedCover As Double, ByVal nMaxAmount_RelatedCover As Double, ByVal nExcess As Double, ByVal nCuota_parte As Double, ByVal sRoutine As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal dEffecdate As Date, ByVal sAction As String, ByVal nAmountQuota As Double) As String
		Dim lclsErrors As New eFunctions.Errors
		Dim lintCount As Integer
		
		On Error GoTo ValCR724_err
		
		'+ Tipo de cobertura Debe estar lleno
		If nCoverType <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60321)
		End If
		
		'+ Cobertura Debe estar lleno
		If nCover <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60315)
		Else
			'+ La cobertura indicada NO debe estar definida en la tabla
			If ReaContr_LimCov(nNumber, nBranch_rei, nContractType, dEffecdate, nCoverType, nCover) Then
				For lintCount = 1 To Me.Count
					If Item_CR724(lintCount) Then
						If Me.nCovergen = nCover And sAction = "Add" Then
							Call lclsErrors.ErrorMessage(sCodispl, 60322)
							Exit For
						End If
					End If
				Next 
			End If
		End If
		
		'+ Facultativo / Stop loss específico - Límite Si el tipo de contrato en tratamiento es del tipo "Facultativo" o "Stop loss específico",
		'y no se tiene información en los campos "Cobertura relacionada" y el "% sobre la cobertura relacionada", debe estar lleno
		If nContractType = 4 Or nContractType = 9 Or nContractType = 10 Then
			If nPercent_RelatedCover <= 0 And nRelatedCover <= 0 Then
				If nAmount <= 0 And sRoutine = "" Then
					Call lclsErrors.ErrorMessage(sCodispl, 60330)
				End If
			End If
		End If
		
		'+ Cuota Parte / Facultativo / Stop loss específico - Cobertura relacionada Si el tipo de contrato en tratamiento es del tipo "Facultativo" o
		'+ "Stop loss específico", y no se tiene información en el campo "Límite", debe estar lleno
		If nContractType = 4 Or nContractType = 9 Or nContractType = 10 Then
			If nAmount <= 0 And nRelatedCover <= 0 And sRoutine = "" Then
				Call lclsErrors.ErrorMessage(sCodispl, 60331)
			End If
		End If
		
		'+ Cuota Parte / Facultativo / Stop loss específico - % sobre la cobertura relacionada Si el campo "Cobertura relacionada" tiene valor,
		'+ debe estar lleno
		If nRelatedCover > 0 Then
			If nPercent_RelatedCover <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 60332)
			End If
		End If
		
		'+ Excedentes - Plenos Si se está procesando un contrato de tipo "excedentes",
		'+ debe estar lleno
		If nContractType = 5 Or nContractType = 6 Or nContractType = 7 Or nContractType = 8 Then
			If nExcess <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 6007)
			End If
		End If
		
		'+ Cuota-Parte - %Cedido Si se está procesando un contrato de tipo "cuota-parte",
		'+ debe estar lleno
		If nContractType = 2 Or nContractType = 3 Then
			If nCuota_parte <= 0 And sRoutine = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 6008)
			End If
		End If
		
		
		'+ Rutina Si se indicó información en alguna de las secciones "Facultativo / Stop loss específico",
		'+ "Excedentes" o "Cuota-Parte", no se debe indicar información
		If nAmount > 0 Or nRelatedCover > 0 Or nPercent_RelatedCover > 0 Or nExcess > 0 Or nCuota_parte > 0 Then
			If sRoutine > String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 60334)
			End If
		End If
		
		'+ Debe incluir el límite del contrato para la cobertura indicada para contratos de cuota-parte
		If (nContractType = 2 Or nContractType = 3) And nAmountQuota <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60330)
		End If
		
		'+ Rutina Si no se indicó información en las secciones "Facultativo / Stop loss específico",
		'+ "Excedentes" o "Cuota-Parte", debe estar lleno
		If nAmount <= 0 And nRelatedCover <= 0 And nPercent_RelatedCover <= 0 And nExcess <= 0 And nCuota_parte <= 0 Then
			If sRoutine = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 60333)
			End If
		End If
		
		ValCR724 = lclsErrors.Confirm
		
ValCR724_err: 
		If Err.Number Then
			ValCR724 = "ValCR724: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% PostCR724: Ejecuta el SP de actualización sobre la BD - ACM - 11/09/2002
	Public Function PostCR724(ByVal nAction As Integer, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nInsur_area As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nAmount As Double, ByVal nLines As Double, ByVal nQuota_sha As Double, ByVal sRoutine As String, ByVal nCoverApp As Integer, ByVal nPercent As Double, ByVal nMaxAmount As Double, ByVal dCompdate As Date, ByVal nUsercode As Integer, ByVal nAmountQuota As Double) As Boolean
		Dim lrecInsUpdContrLimCov As New eRemoteDB.Execute
		
		On Error GoTo PostCR724_err
		
		With lrecInsUpdContrLimCov
			.StoredProcedure = "InsUpdContrLimCov"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_Rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverGen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If nType = 2 Or nType = 3 Then
				.Parameters.Add("nAmount", nAmountQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("nLines", nLines, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuota_sha", nQuota_sha, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverApp", nCoverApp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'patty
			.Parameters.Add("nMaxAmount", nMaxAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 2, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCompdate", dCompdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			PostCR724 = .Run(False)
		End With
		
PostCR724_err: 
		If Err.Number Then
			PostCR724 = False
		End If
		
		'UPGRADE_NOTE: Object lrecInsUpdContrLimCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdContrLimCov = Nothing
		On Error GoTo 0
	End Function
End Class






