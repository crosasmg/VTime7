Option Strict Off
Option Explicit On
Public Class Retention
	'%-------------------------------------------------------%'
	'% $Workfile:: Retention.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on 06/11/2001
	'+ Propiedades según la tabla en el sistema el 11/06/2001
	
	'   Column_name                 Type              Computed  Length  Prec  Scale  Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nConsec As Integer 'smallint        no        2      5     0     no              (n/a)               (n/a)
	Public dEffecdate As Date 'datetime        no        8                  no              (n/a)               (n/a)
	Public nType_rel As Integer 'smallint        no        2      5     0     no              (n/a)               (n/a)
	Public nNumber As Integer 'smallint        no        2      5     0     no              (n/a)               (n/a)
	Public nType As Integer 'smallint        no        2      5     0     no              (n/a)               (n/a)
	Public nBranch As Integer 'smallint        no        2      5     0     no              (n/a)               (n/a)
	Public sExclusion As String 'char            no        1                  yes              no                  yes
	Public nLines_pct As Double 'decimal         no        5      5     2     yes             (n/a)               (n/a)
	Public nMax_Capita As Double 'decimal         no        9      12    0     yes             (n/a)               (n/a)
	Public nMax_rate As Double 'decimal         no        5      4     2     yes             (n/a)               (n/a)
	Public nMin_Capita As Double 'decimal         no        9      12    0     yes             (n/a)               (n/a)
	Public nMin_rate As Double 'decimal         no        5      4     2     yes             (n/a)               (n/a)
	Public nNew_retent As Double 'decimal         no        9      12    0     yes             (n/a)               (n/a)
	Public dNulldate As Date 'datetime        no        8                  yes             (n/a)               (n/a)
	Public nRisk_type As Integer 'smallint        no        2      5     0     yes             (n/a)               (n/a)
	Public nUsercode As Integer 'smallint        no        2      5     0     yes             (n/a)               (n/a)
	
	'**+ Auxiliary properties
	'+ Propiedades auxiliares
	
	Public nSel As Integer
	Public sExist As Integer
	Public nPercentCed As Double
	
	Private Structure udtRetention
		Dim nSel As Integer
		Dim sExist As String
		Dim nRisk_type As Integer
		Dim nMin_Capita As Double
		Dim nMax_Capita As Double
		Dim nMin_rate As Double
		Dim nMax_rate As Double
		Dim sExclusion As String
		Dim nNew_retent As Double
		Dim nLines_pct As Double
		Dim nPercentCed As Double
		Dim nConsec As Integer
	End Structure
	
	Private arrRetention() As udtRetention
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(arrRetention)
		End Get
	End Property
	Public Function ItemRetention(ByVal lintIndex As Integer) As Boolean
		If lintIndex <= UBound(arrRetention) Then
			With arrRetention(lintIndex)
				nSel = .nSel
				sExist = CInt(.sExist)
				nRisk_type = .nRisk_type
				nMin_Capita = .nMin_Capita
				nMax_Capita = .nMax_Capita
				nMin_rate = .nMin_rate
				nMax_rate = .nMax_rate
				sExclusion = .sExclusion
				nNew_retent = .nNew_retent
				nLines_pct = .nLines_pct
				nPercentCed = .nPercentCed
				nConsec = .nConsec
			End With
			ItemRetention = True
		Else
			ItemRetention = False
		End If
	End Function
	
	'**% FInd: This routine is incharge to make the reading about the  "Retention" table
	'%Find: Esta rutina se encarga de realizar la lectura sobre la tabla "Retention"
	Public Function Find(ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaRetention As eRemoteDB.Execute
		Dim lclsValues As eFunctions.Values
		Dim lintCount As Integer
		
		lrecreaRetention = New eRemoteDB.Execute
		lclsValues = New eFunctions.Values
		
		On Error GoTo Find_Err
		
		Find = True
		
		'**+ Parameters definition for the stored procedure 'insudb.reaRetention'
		'**+ Data read on 06/11/2001 03:45:16 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaRetention'
		'+ Información leída el 11/06/2001 03:45:16 p.m.
		
		With lrecreaRetention
			.StoredProcedure = "reaRetention"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				ReDim arrRetention(500)
				lintCount = 0
				Do While Not .EOF
					arrRetention(lintCount).nSel = 0
					arrRetention(lintCount).sExist = "1"
					arrRetention(lintCount).nRisk_type = lclsValues.StringToType(.FieldToClass("nRisk_Type"), eFunctions.Values.eTypeData.etdInteger)
					arrRetention(lintCount).nMin_Capita = lclsValues.StringToType(.FieldToClass("nMin_Capita"), eFunctions.Values.eTypeData.etdDouble)
					arrRetention(lintCount).nMax_Capita = lclsValues.StringToType(.FieldToClass("nMax_Capita"), eFunctions.Values.eTypeData.etdDouble)
					arrRetention(lintCount).nMin_rate = lclsValues.StringToType(.FieldToClass("nMin_rate"), eFunctions.Values.eTypeData.etdDouble)
					arrRetention(lintCount).nMax_rate = lclsValues.StringToType(.FieldToClass("nMax_rate"), eFunctions.Values.eTypeData.etdDouble)
					
					If .FieldToClass("sExclusion") = "2" Then
						arrRetention(lintCount).sExclusion = "0"
					Else
						arrRetention(lintCount).sExclusion = "1"
					End If
					
					arrRetention(lintCount).nNew_retent = lclsValues.StringToType(.FieldToClass("nNew_retent"), eFunctions.Values.eTypeData.etdDouble)
					
					If nType = 5 Or nType = 6 Or nType = 7 Or nType = 8 Then
						arrRetention(lintCount).nLines_pct = lclsValues.StringToType(.FieldToClass("nLines_pct"), eFunctions.Values.eTypeData.etdDouble)
						arrRetention(lintCount).nPercentCed = 0
					End If
					
					If nType = 2 Or nType = 3 Then
						arrRetention(lintCount).nLines_pct = 0
						arrRetention(lintCount).nPercentCed = lclsValues.StringToType(.FieldToClass("nLines_pct"), eFunctions.Values.eTypeData.etdDouble)
					End If
					
					arrRetention(lintCount).nConsec = lclsValues.StringToType(.FieldToClass("nConsec"), eFunctions.Values.eTypeData.etdInteger)
					lintCount = lintCount + 1
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve arrRetention(lintCount)
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaRetention may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaRetention = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
	
	'**%reaRetention_last_consec: This function is in charge to find the last consecutive
	'**% assigned inside the lines table and limit retention
	'%reaRetention_last_consec: Esta función se encarga de encontrar el último consecutivo
	'% asignado dentro de la tabla de plenos y límites de retención
	Public Function Last_consec() As Integer
		Dim lrecreaRetention_last_consec As eRemoteDB.Execute
		
		lrecreaRetention_last_consec = New eRemoteDB.Execute
		
		On Error GoTo 0
		'**+ Parameters definition for the stored procedure 'insudb.reaRetention_last_consec'
		'**+ Data read on 06/11/2001 04:16:20 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaRetention_last_consec'
		'+ Información leída el 11/06/2001 04:16:20 p.m.
		
		With lrecreaRetention_last_consec
			.StoredProcedure = "reaRetention_last_consec"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Last_consec = (.FieldToClass("LastConsec")) + 1
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaRetention_last_consec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaRetention_last_consec = Nothing
		
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nType_rel = 1
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%insValCR020: In this function are make the correspondent validation to the 'CR020' form
	'%insValCR020:En esta funcion se realizan las validaciones correspondientes a la forma 'CR020'
	Public Function insValCR020(ByVal sCodispl As String, ByVal sWindowsType As String, ByVal nRisk_type As Integer, ByVal nMin_Capita As Double, ByVal nMax_Capita As Double, ByVal nMin_rate As Double, ByVal nMax_rate As Double, ByVal sExclusion As String, ByVal nNew_retent As Double, ByVal nPercentCed As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValNum As eFunctions.valField
		Dim lblnValCR020 As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsValNum = New eFunctions.valField
		lclsValNum.objErr = lclsErrors
		
		On Error GoTo insValCR020_Err
		
		'**+ Validate that one of hte fields "Risk type" , "Minimum sum" or "minimum rate" are filled
		'+Se valida que unos de los campos "Tipo de riesgo", "Cápital mínimo" o "Tasa mínima" esten llenos
		
		If nRisk_type = eRemoteDB.Constants.intNull And nMin_Capita = eRemoteDB.Constants.intNull And nMin_rate = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 6114)
		End If
		
		'+Validación del campo capital máximo
		If nMin_Capita = eRemoteDB.Constants.intNull Then
			If nMax_Capita <> eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 6115)
			End If
		Else
			If nMax_Capita = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 6116)
			Else
				If nMax_Capita <= nMin_Capita Then
					Call lclsErrors.ErrorMessage(sCodispl, 6016,  , eFunctions.Errors.TextAlign.RigthAling, " (Capital)")
				End If
			End If
		End If
		
		'+Validación del campo tasa máxima
		If nMin_rate = eRemoteDB.Constants.intNull Then
			If nMax_rate <> eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 6117)
			End If
		Else
			If nMax_rate = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 6118)
			Else
				If nMax_rate <= nMin_rate Then
					Call lclsErrors.ErrorMessage(sCodispl, 6016,  , eFunctions.Errors.TextAlign.RigthAling, " (Tasa)")
				End If
			End If
		End If
		
		'**+ Validate the retention limit field according to the excluted field of the contract
		'+Se valida el campo limite de retención con respecto al campo excluido del contrato
		
		With lclsValNum
			If .ValNumber(nNew_retent,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If sExclusion = "1" And nNew_retent <> 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 6119)
				End If
			End If
			
			'**+ Validate the limit field of % ceded
			'+Se valida el campo limite de porcentaje cedido
			
			If nPercentCed <> 0 And nPercentCed <> eRemoteDB.Constants.intNull Then
				.EqualMax = True
				.EqualMin = True
				.Min = 0
				.Max = 999.99
				.Descript = "Porcentaje Cedido"
				Call .ValNumber(nPercentCed,  , eFunctions.valField.eTypeValField.onlyvalid)
			End If
		End With
		
		insValCR020 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValNum = Nothing
		
		
insValCR020_Err: 
		If Err.Number Then
			insValCR020 = insValCR020 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: This function makes the update in the involved tables
	'%Update: Esta función realiza las actualizaciones en las tablas involucradas
	Public Function Update() As Boolean
		Dim lrecinsRetention As eRemoteDB.Execute
		
		lrecinsRetention = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		Update = True
		
		'**+ Parameters definition for hte stored procedure 'insudb.insRetention'
		'**+ Data read on 06/12/2001 09:01:54 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.insRetention'
		'+ Información leída el 12/06/2001 09:01:54 a.m.
		
		With lrecinsRetention
			.StoredProcedure = "insRetention"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExclusion", sExclusion, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Select Case nType
				Case 2, 3
					.Parameters.Add("nLines_pct", nPercentCed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 5, 6, 7, 8
					.Parameters.Add("nLines_pct", nLines_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case Else
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nLines_pct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End Select
			
			.Parameters.Add("nMax_capita", nMax_Capita, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_rate", nMax_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_capita", nMin_Capita, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_rate", nMin_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNew_retent", nNew_retent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRisk_type", nRisk_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsRetention may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsRetention = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Delete: deletes the information of the selected row by the user
	'%Delete: Eliminar la inforamción de las filas seleccionadas por el usuario
	Public Function Delete() As Boolean
		Dim lrecdelRetention As eRemoteDB.Execute
		
		lrecdelRetention = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'**+ Parameters definition for the store procedure 'insudb.delRetention'
		'**+ Data read on 06/12/2001 09:14:04 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.delRetention'
		'+ Información leída el 12/06/2001 09:14:04 a.m.
		
		With lrecdelRetention
			.StoredProcedure = "delRetention"
			.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelRetention may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelRetention = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostCR020: This function is incharge to make the update in the differents
	'**% tables involved
	'%insPostCR020: Esta función se encarga de realizar las actualizaciones en las
	'%diferentes tablas involucradas
	Public Function insPostCR020(ByVal sCodispl As String, ByVal nSel As Integer, ByVal sExist As String, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, ByVal nConsec As Integer, ByVal nRisk_type As Integer, ByVal nMin_Capita As Double, ByVal nMax_Capita As Double, ByVal nMin_rate As Double, ByVal nMax_rate As Double, ByVal sExclusion As String, ByVal nLines_pct As Double, ByVal nNew_retent As Double, ByVal nPercentCed As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostCR020_Err
		
		insPostCR020 = True
		
		With Me
			.nType_rel = .nType_rel
			.nNumber = nNumber
			.nType = nType
			.nBranch = nBranch
			.dEffecdate = dEffecdate
			.nRisk_type = nRisk_type
			.nMin_Capita = nMin_Capita
			.nMax_Capita = nMax_Capita
			.nMin_rate = nMin_rate
			.nMax_rate = nMax_rate
			.sExclusion = IIf(sExclusion <> String.Empty And sExclusion <> "0", 1, 2)
			.nLines_pct = nLines_pct
			.nNew_retent = nNew_retent
			.nPercentCed = nPercentCed
			.nUsercode = nUsercode
			
			Select Case nSel
				
				'**+ If the selection is Register
				'+Si la  selección es Registrar
				
				Case 1
					
					If sExist <> "1" Then
						If nConsec = eRemoteDB.Constants.intNull Or nConsec = 0 Then
							.nConsec = .Last_consec()
						Else
							.nConsec = nConsec
						End If
						insPostCR020 = .Update()
					End If
					
					'**+ If the selection is Modify
					'+Si la  seleccion es Modificar
					
				Case 2
					
					If nConsec = eRemoteDB.Constants.intNull Or nConsec = 0 Then
						.nConsec = .Last_consec()
					Else
						.nConsec = nConsec
					End If
					insPostCR020 = .Update()
					
					'**+ Of the selection is Delete
					'+Si la  seleccion es Eliminar
					
				Case 3
					.nConsec = nConsec
					insPostCR020 = .Delete()
					
			End Select
		End With
		
insPostCR020_Err: 
		If Err.Number Then
			insPostCR020 = False
		End If
		On Error GoTo 0
	End Function
End Class






