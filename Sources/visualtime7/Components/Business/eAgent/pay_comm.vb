Option Strict Off
Option Explicit On
Public Class pay_comm
	'%-------------------------------------------------------%'
	'% $Workfile:: pay_comm.cls                             $%'
	'% $Author:: Nvaplat53                                  $%'
	'% $Date:: 8/09/04 4:17p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'pay_comm' en el sistema 14/02/2002 09:49:04 a.m.
	
	Private mCol As Collection
	
	Public nIntermed As Double
	Public nId As Double
	Public nPay_Comm As Double
	Public nBranch As Integer
	Public sDesBranch As String
	Public nProduct As Integer
	Public sDesProduct As String
	Public nPolicy As Double
	Public nCertif As Double
	Public nOricurr As Integer
	Public sTitularc As String
	Public nDocnumbe As Double
	Public dPay_date As Date
	Public nCom_Afec As Double
	Public nCom_exen As Double
	Public nTotorigi As Double
	Public nTotlocal As Double
	Public dCompdate As Date
	Public nUsercode As Integer
	Public nDoctype As Integer
	Public dVal_Date As Date
	Public dProcSup As Date
	Public nIntertyp As Integer
	Public sIntertyp As String
	Public P_SKEY As String
	Public nTax As Double
	Public nTaxloc As Double
	
	
	'
	
	'InsValAGC621: Función que realiza la validacion de los datos introducidor en la sección
	'    de detalles de la ventana
	Public Function InsValAGC621(ByVal sCodispl As String, ByVal nIntermed As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date, ByVal nPay_Comm As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo ValidateAGC621_Err
		
		lclsErrors = New eFunctions.Errors
		
		If (nIntermed = eRemoteDB.Constants.intNull Or nIntermed = 0) And dEffecdateIni = dtmNull And dEffecdateEnd = dtmNull And (nPay_Comm = eRemoteDB.Constants.intNull Or nPay_Comm = 0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1068)
			
		End If
		InsValAGC621 = lclsErrors.Confirm
		
ValidateAGC621_Err: 
		If Err.Number Then
			InsValAGC621 = InsValAGC621 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'InsValAGL815: Función que realiza la validacion de los datos introducidor en la sección
	'    de detalles de la ventana
	Public Function InsValAGL815(ByVal sCodispl As String, ByVal nBranch As Double, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo ValidateAGL815_Err
		
		lclsErrors = New eFunctions.Errors
		
		If dEffecdateIni = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60217)
		End If
		
		If dEffecdateEnd = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60218)
		End If
		
		If dEffecdateEnd <> dtmNull And dEffecdateIni <> dtmNull Then
			If dEffecdateIni > dEffecdateEnd Then
				Call lclsErrors.ErrorMessage(sCodispl, 55006)
			End If
		End If
		
		InsValAGL815 = lclsErrors.Confirm
		
ValidateAGL815_Err: 
		If Err.Number Then
			InsValAGL815 = InsValAGL815 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'% Find: Verifica si la comision de la poliza ya fue pagada
	Public Function Find(ByVal nIntermed As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecFindComm_Pol As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecFindComm_Pol = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecFindComm_Pol
			.StoredProcedure = "reaPay_comm"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nCount").Value > 0 Then
					Find = True
				End If
			End If
			
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecFindComm_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindComm_Pol = Nothing
	End Function
	
	
	'**% Rea_AGL815:
	'% Rea_AGL815: Permite realizar el llamado al SP de actualización de Cesiones de wrecks.
	Public Function Rea_AGL815(ByVal ldtmDateFrom As Date, ByVal dDateTo As Date, ByVal nBranch As Integer, ByVal lintUsercode As Integer, ByVal lintCompany As Integer) As Boolean
		Dim lclsComm_pay As eRemoteDB.Execute
		
		
		lclsComm_pay = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.rea_intcomagl815'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.rea_intcomagl815 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsComm_pay
			.StoredProcedure = "rea_intcomagl815"
			
			.Parameters.Add("dDateFrom", ldtmDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				Rea_AGL815 = True
			Else
				Rea_AGL815 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lclsComm_pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsComm_pay = Nothing
	End Function
	
	'% Find_nPayComm: Verifica si el nro de comisión indicado existe, considerando un rango de fechas y
	'                 otros parametros que son opcionales
	Public Function Find_nPayComm(ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal nPay_Comm As Double, ByVal nIntermed As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecFindnPayComm As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecFindnPayComm = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecFindnPayComm
			.StoredProcedure = "reanpay_comm"
			.Parameters.Add("dDateini", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateend", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_Comm", nPay_Comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nCount").Value > 0 Then
					Find_nPayComm = True
				End If
			End If
			
		End With
		
Find_Err: 
		If Err.Number Then
			Find_nPayComm = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecFindnPayComm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindnPayComm = Nothing
	End Function
End Class






