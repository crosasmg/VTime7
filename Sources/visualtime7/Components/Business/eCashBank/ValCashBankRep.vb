Option Strict Off
Option Explicit On
Public Class ValCashBankRep
	Public P_SKEY As String
	Public P_DIS_CONC As Short
	
	'**% insValOPL004_K: Validates all the introduced data in the form OPL004_K
	'%insValOPL004_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma OPL004.
	Public Function insValOPL004_K(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nTypeList As Integer, ByVal nRequest_nu As Double, ByVal nZone As Integer, ByVal nBank As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCheque As eCashBank.Cheque
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValOPL004_K_Err
		
		'*+Validation of the field Initial Date is performed
		'+Se realiza la validacion del campo Fecha de Inicio
		If dInitDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7163)
		End If
		
		'*+Validation of Final Date is performed
		'+Se valida la fecha final
		If dEndDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7164)
		End If
		
		'*+The Initial Date does not should be bigger than Final Date
		'+Se valida que la fecha inicial no sea mayor que la fecha final
		If Not dInitDate = dtmNull And Not dEndDate = dtmNull Then
			If dInitDate > dEndDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 7165)
			End If
		End If
		
		'*+Validation of the field Request is performed
		'+Validacion del campo "Solicitud"
		If nTypeList = 1 Then
			
			If nRequest_nu = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 8052)
			Else
				lclsCheque = New eCashBank.Cheque
				If Not lclsCheque.insReaCheques(nRequest_nu) Then
					Call lclsErrors.ErrorMessage(sCodispl, 7152)
				End If
				'UPGRADE_NOTE: Object lclsCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsCheque = Nothing
			End If
		End If
		
		'*+Validation of the field Branch office
		'+Validacion del campo "Zona"
		If nTypeList = 2 Then
			If nZone = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 10831)
			End If
		End If
		
		'*+Validation of the field Bank
		'+Validacion del campo "Banco"
		If nTypeList = 3 Then
			If nBank = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 10828)
			End If
		End If
		
		insValOPL004_K = lclsErrors.Confirm
		
		
insValOPL004_K_Err: 
		If Err.Number Then
			insValOPL004_K = "insValOPL004_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValOPL005_K: Validates all the introduced data in the form OPL005_K
	'%insValOPL005_K: Esta función se encarga de validar los datos introducidos
	'%en la ventana de la forma OPL005.
	Public Function insValOPL005_K(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValOPL005_K_Err
		
		'*+Validation of the field Initial Date is performed
		'+Se realiza la validacion del campo Fecha de Inicio
		If dInitDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3237)
		End If
		
		'*+Validation of Final Date is performed
		'+Se valida la fecha final
		If dEndDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3239)
		End If
		
		'*+The Initial Date does not should be bigger than Final Date
		'+Se valida que la fecha inicial no sea mayor que la fecha final
		If Not dInitDate = dtmNull And Not dEndDate = dtmNull Then
			If dInitDate > dEndDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 12120)
			End If
		End If
		
		insValOPL005_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOPL005_K_Err: 
		If Err.Number Then
			insValOPL005_K = insValOPL005_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insValOPL003: Esta función se encarga de validar los datos introducidos
	'%en la ventana de la forma OPL003.
	Public Function insValOPL003_K(ByVal sCodispl As String) As String
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValOPL003_K_Err
		
		'+Se llama a la advertencia para que el usuario acepte o no la ejecución de la transacción
		Call lclsErrors.ErrorMessage(sCodispl, 7310)
		
		insValOPL003_K = lclsErrors.Confirm
		
insValOPL003_K_Err: 
		If Err.Number Then
			insValOPL003_K = insValOPL003_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostOPL003: Esta función se encarga de actualizar los saldos bancarios.
	Public Function insPostOPL003_K(ByVal nUsercode As Integer) As Boolean
		Dim lrecinsOPL003 As eRemoteDB.Execute
		
		lrecinsOPL003 = New eRemoteDB.Execute
		
		On Error GoTo insPostOPL003_K_Err
		
		With lrecinsOPL003
			.StoredProcedure = "insOPL003"
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostOPL003_K = True
			Else
				insPostOPL003_K = False
			End If
		End With
		
		
insPostOPL003_K_Err: 
		If Err.Number Then
			insPostOPL003_K = False
		End If
		'UPGRADE_NOTE: Object lrecinsOPL003 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOPL003 = Nothing
		On Error GoTo 0
		
	End Function
    '+ InsPostOPL004:
    Public Function insPostOPL004(ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nRequest_nu As Double, ByVal nOffice As Integer, ByVal nBank As Integer, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
        Dim lrecinsOPL004 As eRemoteDB.Execute

        lrecinsOPL004 = New eRemoteDB.Execute

        With lrecinsOPL004
            .StoredProcedure = "insPostOPL004"
            .Parameters.Add("dInitDate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBank_code", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostOPL004 = True
            Else
                insPostOPL004 = False
            End If

        End With

        lrecinsOPL004 = Nothing

    End Function
	'**% Ins_PostOPL1059:
	'% Ins_PostOPL1059: Permite realizar el llamado al SP de Recaudación por Distribuir a Pólizas al Día.
	Public Function Ins_PostOPL1059(ByVal ldtmDateRep As Date, ByVal lintUsercode As Integer, ByVal lintCompany As Integer) As Boolean
		Dim lrecinsOPL1059 As eRemoteDB.Execute
		
		lrecinsOPL1059 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsOPL1059
			.StoredProcedure = "rea_opl1059"
			
			.Parameters.Add("dDateRep", ldtmDateRep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				Ins_PostOPL1059 = True
			Else
				Ins_PostOPL1059 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsOPL1059 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOPL1059 = Nothing
	End Function
	
	Public Function InsPostCOL999(ByVal dDateFrom As Date, ByVal dDateTo As Date) As Boolean
		Dim lrecinsCol999 As eRemoteDB.Execute
		
		lrecinsCol999 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsCol999
			.StoredProcedure = "rea_col999"
			
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostCOL999 = True
			Else
				InsPostCOL999 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsCol999 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCol999 = Nothing
	End Function
	
	Public Function InsPostOPL855(ByVal dDateFrom As Date, ByVal dDateTo As Date) As Boolean
		Dim lrecinsOpl855 As eRemoteDB.Execute
		
		lrecinsOpl855 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsOpl855
			.StoredProcedure = "rea_opl855"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostOPL855 = True
			Else
				InsPostOPL855 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsOpl855 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOpl855 = Nothing
	End Function
	
	Public Function InsPostOPC920(ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecinsOpc920 As eRemoteDB.Execute
		
		lrecinsOpc920 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsOpc920
			.StoredProcedure = "rea_opc920"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostOPC920 = True
			Else
				InsPostOPC920 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsOpc920 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOpc920 = Nothing
	End Function
	
	Public Function InsPostOPC965(ByVal nPolicy As Double, ByVal dDateFrom As Date, ByVal dDateTo As Date) As Boolean
		Dim lrecinsOpc965 As eRemoteDB.Execute
		
		lrecinsOpc965 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsOpc965
			.StoredProcedure = "rea_opc965"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostOPC965 = True
			Else
				InsPostOPC965 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsOpc965 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOpc965 = Nothing
	End Function
	
	
	'**% Ins_PostOPL1063:
	'% Ins_PostOPL1063: Permite realizar el llamado al SP de Recaudación por Distribuir a Pólizas al Día.
	Public Function Ins_PostOPL1063(ByVal dInitDate As Date, ByVal dEndDate As Date) As Boolean
		Dim lrecinsOPL1063 As eRemoteDB.Execute
		
		lrecinsOPL1063 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsOPL1063
			.StoredProcedure = "rea_opl1063"
			
			.Parameters.Add("dInitDate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_DIS_CONC", P_DIS_CONC, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				P_DIS_CONC = .Parameters("P_DIS_CONC").Value
				Ins_PostOPL1063 = True
			Else
				Ins_PostOPL1063 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsOPL1063 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOPL1063 = Nothing
	End Function
	
	
	Public Function InsPostOPL700(ByVal dDateFrom As Date, ByVal dDateTo As Date, ByVal nCashNum As Integer) As Boolean
		Dim lrecinsOpl700 As eRemoteDB.Execute
		
		lrecinsOpl700 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsOpl700
			.StoredProcedure = "rea_opl700"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostOPL700 = True
			Else
				InsPostOPL700 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsOpl700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOpl700 = Nothing
	End Function
	
	
	Public Function InsPostOPL945(ByVal dDateFrom As Date, ByVal dDateTo As Date) As Boolean
		Dim lrecinsOpl945 As eRemoteDB.Execute
		
		lrecinsOpl945 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsOpl945
			.StoredProcedure = "rea_opl945"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostOPL945 = True
			Else
				InsPostOPL945 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsOpl945 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOpl945 = Nothing
	End Function
	
	Public Function InsPostOPL823(ByVal dDateFrom As Date, ByVal dDateTo As Date, ByVal nOffice As Integer, ByVal nCashNum As Integer) As Boolean
		Dim lrecinsOpl823 As eRemoteDB.Execute
		
		lrecinsOpl823 = New eRemoteDB.Execute
		
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_opl1059'. Generated on 10/09/2003
		'+ Defina todos los parámetros para los procedimientos 'insudb.rea_opl1059'. Generated on 10/09/2003
		
		With lrecinsOpl823
			.StoredProcedure = "rea_opl823"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostOPL823 = True
			Else
				InsPostOPL823 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsOpl823 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOpl823 = Nothing
		
	End Function
     Public Function insBTC00118(ByVal sKey As String, ByVal nCashnum As Integer,ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsOp719 As eRemoteDB.Execute
		
		On Error GoTo UpdCash_stat_Err
		
		lrecinsOp719 = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.UpdCash_stat'.
		With lrecinsOp719
			.StoredProcedure = "InsBtc00118"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)			
			insBTC00118 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclsCash_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsOp719 = Nothing
		
UpdCash_stat_Err: 
		If Err.Number Then
			insBTC00118 = False
		End If
		On Error GoTo 0
	End Function
End Class






