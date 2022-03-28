Option Strict Off
Option Explicit On
Imports eFunctions.Extensions
Public Class ValCoReinsuranRep
	'%-------------------------------------------------------%'
	'% $Workfile:: ValCoReinsuranRep.cls                    $%'
	'% $Author:: Vvera                                      $%'
	'% $Date:: 5/07/06 22:24                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Generation of cessions of premiums.
	'- Generación de cesiones de primas.
	
	Const clngGenCessPremium As Short = 42
	
	'**- Generation of cessions of wrecks.
	'- Generación de cesiones de siniestros.
	
	Const clngGenCessClaim As Short = 43
	
	'**- Generation of technical accounts of obligatory reinsurance.
	'- Generación de cuentas técnicas de reaseguro obligatorio.
	
	Const clngGenTechMand As Short = 27
	
	'**- Generation of cessions of wrecks of nonproportional reinsurance.
	'- Generación de cesiones de siniestros de reaseguro no proporcional.
	
	Const clngGenCessClaimNonPro As Short = 28
	
	'**- Generation of current accounts of co-insurance.
	'+ Generación de cuentas corrientes de coaseguro.
	
	Const clngGenAccCoin As Short = 29
	
	'**- Generation of current accounts of obligatory reinsurance.
	'- Generación de cuentas corrientes de reaseguro obligatorio.
	
	Const clngGenAccReinO As Short = 30
	
	'**- Generation of technical accounts of facultative reinsurance.
    '- Generación de cuentas técnicas de reaseguro facultativoReacrl005
	
	Const clngGenTechFacul As Short = 31
	
	'**- Generation of current accounts of facultative reinsurance
	'- Generación de cuentas corrientes de reaseguro facultativo.
	
	Const clngGenAccReinF As Short = 32
	
	'- Co/Reaseguro.
	Const clngGenCoRein As Short = 4

    '**-Generation of cessions of premiums of nonproportional reinsuran.
    '- Generación de cesiones de primas de reaseguro no proporcional.

    Const clngGenCessPremiumNonPro As Short = 110


    '**- Generation of current accounts of nonproportional reinsurance
    '- Generación de cuentas corrientes de reaseguro no proporcional.

    Const clngGenAccReinNonPro As Short = 111

    '- Libro de Reaseguro
    Const clngReinBook As Short = 204

	Public dLastExecuteDate As Date
	Public sKey As String
	Public P_SKEY As String
	'**% UpdateCRL001: It allows to make the call to the SP of update of Cessions of premiums.
	'% UpdateCRL001: Permite realizar el llamado al SP de actualización de Cesiones de primas.
	Public Function UpdateCRL001(ByVal ldtmDateFrom As Date, ByVal dDateStart As Date, ByVal dDateTo As Date, ByVal lintUsercode As Integer, ByVal lintCompany As Integer, ByVal nInsur_area As Integer, Optional ByVal sExecute As String = "") As Boolean
		Dim lclsCession_pr As eRemoteDB.Execute
		
		On Error GoTo UpdateCRL001_Err
		
		lclsCession_pr = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.insUpdCRL001'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.insUpdCRL001 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsCession_pr
			.StoredProcedure = "insUpdCRL001"
			.Parameters.Add("dDateStart", dDateStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dDateInit", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeProc", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateCRL001 = .Run(False)
		End With
		
UpdateCRL001_Err: 
		If Err.Number Then
			UpdateCRL001 = False
		End If
		'UPGRADE_NOTE: Object lclsCession_pr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCession_pr = Nothing
		On Error GoTo 0
		
	End Function
	
	'**% UpdateCRL002: It allows to make the call to the SP of update of Cessions of premiums.
	'% UpdateCRL002: Permite realizar el llamado al SP de actualización de Cesiones de wrecks.
	Public Function UpdateCRL002(ByVal ldtmDateFrom As Date, ByVal dDateTo As Date, ByVal dDateStart As Date, ByVal lintUsercode As Integer, ByVal lintCompany As Integer, ByVal sExecute As String, ByVal nInsur_area As Integer) As Boolean
		Dim lclsClaim_ces As eRemoteDB.Execute
		
		lclsClaim_ces = New eRemoteDB.Execute
		
		On Error GoTo UpdateCRL002_Err
		
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.insUpdCRL002 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsClaim_ces
			.StoredProcedure = "insUpdCRL002_2"
			.Parameters.Add("dDateFrom", ldtmDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateStart", dDateStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				UpdateCRL002 = True
				Me.sKey = .Parameters("sKey").Value
			End If
		End With
		
UpdateCRL002_Err: 
		If Err.Number Then
			UpdateCRL002 = False
		End If
		'UPGRADE_NOTE: Object lclsClaim_ces may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClaim_ces = Nothing
		On Error GoTo 0
	End Function
	'% insPostCRL004: Llena la tabla tmp_crl004, para el reporte CRL004
	Public Function insPostCRL004(ByVal dEffecdate As Date, ByVal dEndDate As Date, ByVal sExecute As String, Optional ByVal nCompany As Integer = 0, Optional ByVal nBranchRei As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Integer = 0, Optional ByVal sClient As String = "") As Boolean
		Dim lrecinsPostCRL004 As eRemoteDB.Execute
		
		lrecinsPostCRL004 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_crl004'
		With lrecinsPostCRL004
			.StoredProcedure = "insCrl004"
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranchRei", nBranchRei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				sKey = .Parameters("Skey").Value
				insPostCRL004 = True
			Else
				insPostCRL004 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCRL004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCRL004 = Nothing
		
	End Function
	
	'**% UpdateCRL007: Obligatory reinsurance allows to make the call to the SP of update of
	'**% Generation technical accounts of obligatory reinsurance.
	'% UpdateCRL007: Permite realizar el llamado al SP de actualización de Generación cuentas técnicas de
	'% reaseguro obligatorio.
	Public Function UpdateCRL007(ByVal lintMonth As Integer, ByVal lintYear As Integer, ByVal lintUsercode As Integer, ByVal lintBranchRei As Integer, ByVal lintCurrency As Integer, ByVal lintType_con As Integer) As Boolean
		Dim lclsCessions As eRemoteDB.Execute
		
		lclsCessions = New eRemoteDB.Execute
		
		On Error GoTo err_Renamed
		
		'**+ Define all parameters for the stored procedures 'insudb.reaCessions'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.reaCessions '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsCessions
			.StoredProcedure = "reaCessions"
			
			.Parameters.Add("dEnd_date", Me.dLastExecuteDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", CDate("01/01/" & CStr(lintYear)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", lintMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", lintYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", lintBranchRei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", lintCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeCont", lintType_con, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateCRL007 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclsCessions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCessions = Nothing
		
err_Renamed: 
		If Err.Number Then
			UpdateCRL007 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%UpdCRL008:
	'**%???
	'% UpdateCRL008: Permite realizar el llamado al SP de actualización de La generación de la
	'% Cuentas tecnicas de reaseguro facultativo.
	Public Function UpdateCRL008(ByVal nMonth As Integer, ByVal nYear As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsCRL008 As eRemoteDB.Execute
		
		lclsCRL008 = New eRemoteDB.Execute
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.insCRR009'. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsCRL008
			.StoredProcedure = "insCreUpdCRL008"
			.Parameters.Add("dDateFrom", dLastExecuteDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateCRL008 = .Run(False)
		End With
		'UPGRADE_NOTE: Object lclsCRL008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCRL008 = Nothing
	End Function
	
	'**% UpdateCRL009: It allows to make the call to the SP of update of Generation of cessions of wrecks
	'**% in nonproportional reinsurance (for exeso of lost exclusively).
	'% UpdateCRL009: Permite realizar el llamado al SP de actualización de Generación de cesiones de siniestros
	'% en reaseguro no proporcional (para exeso de perdidas exclusivamente).
    Public Function UpdateCRL009(ByVal dDateTo As Date, ByVal lintUsercode As Integer, ByVal sExecute As String, ByVal sKey As String) As Boolean
        Dim lclsCRL009 As eRemoteDB.Execute

        lclsCRL009 = New eRemoteDB.Execute

        '**+ Define all parameters for the stored procedures 'insudb.insCRR009'. Generated on 18/12/2001 02:28:01 p.m.
        '+ Defina todos los parámetros para los procedimientos salvados 'insudb.insCRR009'. Generado en 18/12/2001 02:28:01 P.M..

        With lclsCRL009
            .StoredProcedure = "insCRR009"

            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnd_date", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateCRL009 = .Run(False)
        End With

        'UPGRADE_NOTE: Object lclsCRL009 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCRL009 = Nothing
    End Function
	
	'**% UpdateCRL011: It allows to make the call to the SP of update of Generation of current accounts of co-insurance.
	'% UpdateCRL011: Permite realizar el llamado al SP de actualización de Generación de cuentas corrientes de coaseguro.
	Public Function UpdateCRL011(ByVal lintUsercode As Integer) As Boolean
		Dim lclsCRL011 As eRemoteDB.Execute
		
		lclsCRL011 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.insCreUpdCRL011'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.insCreUpdCRL011 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsCRL011
			.StoredProcedure = "insCreUpdCRL011"
			
			.Parameters.Add("dDateto", dLastExecuteDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateCRL011 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclsCRL011 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCRL011 = Nothing
	End Function
	
	'**% UpdateCRL012: It allows to make the call to the SP of update of Generation of current accounts of reinsurance.
	'% UpdateCRL012: Permite realizar el llamado al SP de actualización de Generación de cuentas corrientes de reaseguro.
	Public Function UpdateCRL012(ByVal lintMonth As Integer, ByVal lintYear As Integer, ByVal llngCess_type As Integer, ByVal lintUsercode As Integer, ByVal lintCompany As Integer) As Boolean
		Dim lclsCRL012 As eRemoteDB.Execute
		
		lclsCRL012 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.insCreUpdCRL012'. Generated on 18/12/2001 02:28:01 p.m.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.insCreUpdCRL012 '. Generado en 18/12/2001 02:28:01 P.M..
		
		With lclsCRL012
			.StoredProcedure = "insCreUpdCRL012"
			
			.Parameters.Add("dDateFrom", dLastExecuteDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", lintMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", lintYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCess_Type", llngCess_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateCRL012 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclsCRL012 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCRL012 = Nothing
	End Function

    '**% UpdateCRL013: It allows to make the call to the SP of update of Cessions of premiums RNP.
    '% UpdateCRL013: Permite realizar el llamado al SP de actualización de Cesiones de primas RNP.
    Public Function UpdateCRL013(ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal lintUsercode As Integer, Optional ByVal sExecute As String = "") As Boolean
        Dim lclsCession_pr As eRemoteDB.Execute

        On Error GoTo UpdateCRL013_Err

        lclsCession_pr = New eRemoteDB.Execute

        '**+ Define all parameters for the stored procedures 'insudb.insUpdCRL013'. Generated on 18/12/2001 02:28:01 p.m.
        '+ Defina todos los parámetros para los procedimientos salvados 'insudb.insUpdCRL013 '. Generado en 18/12/2001 02:28:01 P.M..

        With lclsCession_pr
            .StoredProcedure = "insUpdCRL013"
            .Parameters.Add("dDateIni", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateCRL013 = .Run(False)
        End With

UpdateCRL013_Err:
        If Err.Number Then
            UpdateCRL013 = False
        End If
        lclsCession_pr = Nothing
        On Error GoTo 0

    End Function

	'**%insValCRL001: Function that makes the validation of the data introducidor in the section
	'**% %de details of window CRL001 - Generation of cessions of premiums.
	'%insValCRL001: Función que realiza la validacion de los datos introducidor en la sección
	'%de detalles de la ventana CRL001 - Generación de cesiones de primas.
	Public Function insValCRL001(ByVal sCodispl As String, ByVal dDateStart As Date, ByVal dDateTo As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date

		On Error GoTo insValCRL001_Err

		lclsErrors = New eFunctions.Errors
		
		'**+ The validations of the date are even made.
		'+ Se realizan las validaciones de la fecha hasta.
		
		If dDateStart = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 6025)
		Else
			
			'**+ Validation for the date of execution with respect to the control file.
			'+ Validación para la fecha de ejecución con respecto al archivo de control.
			
            lclsCtrolDate = New eGeneral.Ctrol_date
            Call lclsCtrolDate.Find(clngGenCessPremium)
            If dDateStart < lclsCtrolDate.dEffecdate Then
				Call lclsErrors.ErrorMessage(sCodispl, 6024,  ,  , CStr(CDate(lclsCtrolDate.dEffecdate)))
			Else
				If dDateStart > Today Then
					Call lclsErrors.ErrorMessage(sCodispl, 7161,  ,  , CStr(CDate(lclsCtrolDate.dEffecdate)))
            End If
			End If
			
			'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCtrolDate = Nothing
        End If


		'**+ The validations of the date are even made.
        '+ Se realizan las validaciones de la fecha hasta.
		
        If dDateTo = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 300018)
        Else

			'**+ Validation for the date of execution with respect to the control file.
			'+ Validación para la fecha de ejecución con respecto al archivo de control.

                    lclsCtrolDate = New eGeneral.Ctrol_date
                    Call lclsCtrolDate.Find(clngGenCessPremium)
			If dDateTo > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 7161,  ,  , CStr(CDate(lclsCtrolDate.dEffecdate)))
			Else
				If dDateTo <= dDateStart Then
					Call lclsErrors.ErrorMessage(sCodispl, 6024,  ,  , CStr(CDate(lclsCtrolDate.dEffecdate)))
                    End If
                End If
			'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCtrolDate = Nothing
        End If

        insValCRL001 = lclsErrors.Confirm

insValCRL001_Err: 
		If Err.Number Then
			insValCRL001 = insValCRL001 & Err.Description
		End If

		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
    End Function
	
	'**%insValCRL002: Function that makes the validation of the data introducidor in the section
	'**% %de details of window CRL002 - Generation of cessions of wrecks.
	'%insValCRL002: Función que realiza la validacion de los datos introducidor en la sección
	'%de detalles de la ventana CRL002 - Generación de cesiones de siniestros.
	Public Function insValCRL002(ByVal sCodispl As String, ByVal dDateStart As Date, ByVal dDateTo As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		
		On Error GoTo insValCRL002_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ The validations of the date are even made.
		'+ Se realizan las validaciones de la fecha hasta.
		
		If dDateStart = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 6025)
		Else
			
			'**+ Validation for the date of execution with respect to the control file.
			'+ Validación para la fecha de ejecución con respecto al archivo de control.
			
			lclsCtrolDate = New eGeneral.Ctrol_date
			Call lclsCtrolDate.Find(clngGenCessClaim)
			
			If dDateStart < lclsCtrolDate.dEffecdate Then
				Call lclsErrors.ErrorMessage(sCodispl, 6024,  ,  , CStr(CDate(lclsCtrolDate.dEffecdate)))
			Else
				If dDateStart > Today Then
					Call lclsErrors.ErrorMessage(sCodispl, 7161,  ,  , CStr(CDate(lclsCtrolDate.dEffecdate)))
				End If
			End If
			'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsCtrolDate = Nothing
		End If
		
		'**+ The validations of the date are even made.
		'+ Se realizan las validaciones de la fecha hasta.
		
		If dDateTo = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 300018)
		Else
			
			'**+ Validation for the date of execution with respect to the control file.
			'+ Validación para la fecha de ejecución con respecto al archivo de control.
			
			lclsCtrolDate = New eGeneral.Ctrol_date
			Call lclsCtrolDate.Find(clngGenCessClaim)
			If dDateTo <= lclsCtrolDate.dEffecdate Then
				Call lclsErrors.ErrorMessage(sCodispl, 6024,  ,  , CStr(CDate(lclsCtrolDate.dEffecdate)))
			Else
				If dDateTo > Today Then
					Call lclsErrors.ErrorMessage(sCodispl, 7161,  ,  , CStr(CDate(lclsCtrolDate.dEffecdate)))
				End If
			End If
			'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsCtrolDate = Nothing
		End If
		
		insValCRL002 = lclsErrors.Confirm
		
insValCRL002_Err: 
		If Err.Number Then
			insValCRL002 = insValCRL002 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'**% insValCRL006: Function that makes the validation of the data introducidor in section
	'**% of details of window CRL006 - Printing of cessions of wrecks.
	'% insValCRL006: Función que realiza la validacion de los datos introducidor en la sección
	'% de detalles de la ventana CRL006 - Impresión de cesiones de siniestros.
	Public Function insValCRL006(ByVal sCodispl As String, ByVal ldtmDateFrom As Date, ByVal dDateTo As Date, ByVal llngCessType As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValCRL006_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ The validations of the date are made from.
		'+ Se realizan las validaciones de la fecha desde.
		
		If ldtmDateFrom = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 6128)
		Else
			If ldtmDateFrom > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 7161)
			End If
		End If
		
		'**+ The validations of the date are even made.
		'+ Se realizan las validaciones de la fecha hasta.
		
		If dDateTo = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 6129)
		Else
			If dDateTo > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 7161)
			End If
		End If
		
		If ldtmDateFrom <> eRemoteDB.Constants.dtmNull And dDateTo <> eRemoteDB.Constants.dtmNull And dDateTo < ldtmDateFrom Then
			Call lclsErrors.ErrorMessage(sCodispl, 6130)
		End If
		
		If (llngCessType = 0 Or llngCessType = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 6058)
		End If
		
		insValCRL006 = lclsErrors.Confirm
		
insValCRL006_Err: 
		If Err.Number Then
			insValCRL006 = insValCRL006 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insValCRL007: Función que realiza la validacion de los datos introducidor en la sección
	'% de detalles de la ventana CRL007 - Generación de cuentas técnicas de reaseguro obligatorio.
	Public Function insValCRL007(ByVal sCodispl As String, ByVal lintMonth As Integer, ByVal lintYear As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		Dim lintDays As Integer
        Dim lstrDate As String
		
		On Error GoTo insValCRL007_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se realizan las validaciones del campo Mes.
		If lintMonth = 0 Or lintMonth = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1114)
		Else
			If (lintMonth < 1 Or lintMonth > 12) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1115)
			End If
		End If
		
		'+ Se realizan las validaciones del campo Año.
		If lintYear = 0 Or lintYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1116)
		End If
		
		If (lintMonth <> 0 And lintMonth <> eRemoteDB.Constants.intNull) And (lintYear <> 0 And lintYear <> eRemoteDB.Constants.intNull) Then
			'+ Se extraen los últimos días para el mes en curso introducido en el campo mes.
			
			lintDays = insCalc_days(lintMonth, lintYear)
			
			'+ Se arma la fecha con el mes extraído y con los días introducidos en el campo días.
            lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(lintMonth)) & "/" & Trim(Str(lintYear))
			
            Me.dLastExecuteDate = CDate(lstrDate)
			
            If IsDate(CDate(lstrDate)) Then
                lclsCtrolDate = New eGeneral.Ctrol_date

                '+ Se trae la última fecha de ejecución del proceso de generación de cesiones de primas y
                '+ se valida que la fecha armada no sea mayor a la fecha de ejecución del procesos de generación de primas.
                Call lclsCtrolDate.Find(clngGenCessPremium)
                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6132, , , CStr(lclsCtrolDate.dEffecdate))
                End If

                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull

                '+ Se extrae la fecha de generación de los procesos de generación de siniestros.
                Call lclsCtrolDate.Find(clngGenCessClaim)
                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6133, , , CStr(lclsCtrolDate.dEffecdate))
                End If

                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull

                '+ Se trae la última fecha de ejecución del proceso de generación de cesiones de primas RNP y
                '+ se valida que la fecha armada no sea mayor a la fecha de ejecución del procesos de generación de primas RNP.
                'Call lclsCtrolDate.Find(clngGenCessPremiumNonPro)
                'If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                '    Call lclsErrors.ErrorMessage(sCodispl, 90000507, , , CStr(lclsCtrolDate.dEffecdate))
                'End If

                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull

                '+ Se extrae la fecha de generación de los procesos de generación de siniestros RNP.
                'Call lclsCtrolDate.Find(clngGenCessClaimNonPro)
                'If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                '    Call lclsErrors.ErrorMessage(sCodispl, 90000508, , , CStr(lclsCtrolDate.dEffecdate))
                'End If

                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull

                '+ Se valida que la fecha armada no sea mayor a la fecha de ejecución de generación de cuentas
                '+ técnicas de reaseguro obligatorio.
                Call lclsCtrolDate.Find(clngGenTechMand)
                If CDate(lstrDate) <= lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6134, , , CStr(lclsCtrolDate.dEffecdate))
                End If
            End If
		End If
		
		insValCRL007 = lclsErrors.Confirm
		
insValCRL007_Err: 
		If Err.Number Then
			insValCRL007 = insValCRL007 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'**% insCalc_days: It returns the amount of days of the month introduced in the form.
	'% insCalc_days: Retorna la cantidad de días del mes introducido en la forma.
	Private Function insCalc_days(ByVal lintMonthCalc As Integer, ByVal lintYearCalc As Integer) As Integer
		Select Case lintMonthCalc
			
			Case 1, 3, 5, 7, 8, 10, 12
				insCalc_days = 31
				
			Case 4, 6, 9, 11
				insCalc_days = 30
				
			Case 2
				If (lintYearCalc Mod 4) = 0 Then
					insCalc_days = 29
				Else
					insCalc_days = 28
				End If
		End Select
	End Function
	
	'**% insValCRL008:  Function that makes the validation of the data introducidor in section
	'**% of details of window CRL008 - Generation of technical accounts of facultative reinsurance.
	'% insValCRL008: Función que realiza la validacion de los datos introducidor en la sección
	'% de detalles de la ventana CRL008 - Generación de cuentas técnicas de reaseguro facultativo.
	Public Function insValCRL008(ByVal sCodispl As String, ByVal lintMonth As Integer, ByVal lintYear As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		Dim lintDays As Integer
        Dim lstrDate As String
		
		On Error GoTo insValCRL008_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ They are made the validations of the field Month.
		'+ Se realizan las validaciones del campo Mes.
		
		If lintMonth = 0 Or lintMonth = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1114)
		Else
			If (lintMonth < 1 Or lintMonth > 12) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1115)
			End If
		End If
		
		'+ Se realizan las validaciones del campo Año.
		If lintYear = 0 Or lintYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1116)
		End If
		
		If (lintMonth <> 0 And lintMonth <> eRemoteDB.Constants.intNull) And (lintMonth >= 1 And lintMonth <= 12) And (lintYear <> 0 And lintYear <> eRemoteDB.Constants.intNull) Then
			
			'+ Se extraen los últimos días para el mes en curso introducido en el campo mes.
			
			lintDays = insCalc_days(lintMonth, lintYear)
			
			'+ Se arma la fecha con el mes extraído y con los días introducidos en el campo días.
            lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(lintMonth)) & "/" & Trim(Str(lintYear))
			
            dLastExecuteDate = CDate(lstrDate)
			
            If IsDate(CDate(lstrDate)) Then
                lclsCtrolDate = New eGeneral.Ctrol_date

                '+ Se trae la última fecha de ejecución del proceso de generación de cesiones de primas y
                '+ se valida que la fecha aramada no sea mayor a la fecha de ejecución del procesos de generación de primas.
                Call lclsCtrolDate.Find(clngGenCessPremium)
                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6132, , , CStr(lclsCtrolDate.dEffecdate))
                End If

                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                '+ Se extrae la fecha de generación de los procesos de generación de siniestros.
                Call lclsCtrolDate.Find(clngGenCessClaim)

                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6133, , , CStr(lclsCtrolDate.dEffecdate))
                End If

                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                '+ Se valida que la fecha armada no sea mayor a la fecha de ejecución de generación de cuentas
                '+ técnicas de reaseguro facultativo.
                Call lclsCtrolDate.Find(clngGenTechFacul)

                If CDate(lstrDate) <= lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6134, , , CStr(lclsCtrolDate.dEffecdate))
                End If
            End If
		End If
		
		insValCRL008 = lclsErrors.Confirm
		
insValCRL008_Err: 
		If Err.Number Then
			insValCRL008 = insValCRL008 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'**% insValCRL009: Function that exclusively makes the validation of the data introducidor in the section
	'**% de details of window CRL009 - Generation of cessions of wrecks in nonproportional reinsurance
	'**%(para exeso of lost).
	'%insValCRL009: Función que realiza la validacion de los datos introducidor en la sección
	'%de detalles de la ventana CRL009 - Generación de cesiones de siniestros en reaseguro no proporcional
	'%(para exeso de perdidas exclusivamente).
	Public Function insValCRL009(ByVal sCodispl As String, ByVal dDateTo As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		
		On Error GoTo insValCRL009_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ The validations of the date are even made.
		'+ Se realizan las validaciones de la fecha hasta.
		
		If dDateTo = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 6129)
		Else
			
			'**+ Validation for the date of execution with respect to the control file.
			'+ Validación para la fecha de ejecución con respecto al archivo de control.
			
			lclsCtrolDate = New eGeneral.Ctrol_date
			Call lclsCtrolDate.Find(clngGenCessClaimNonPro)
			If dDateTo < lclsCtrolDate.dEffecdate Then
				Call lclsErrors.ErrorMessage(sCodispl, 6024,  ,  , CStr(lclsCtrolDate.dEffecdate))
			End If
			
			'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsCtrolDate = Nothing
		End If
		
		insValCRL009 = lclsErrors.Confirm
		
insValCRL009_Err: 
		If Err.Number Then
			insValCRL009 = insValCRL009 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'**% insValCRL011: Function that makes the validation of the data introducidor in section
	'**% of details of window CRL011 - Generation of current accounts of co-insurance.
	'% insValCRL011: Función que realiza la validacion de los datos introducidor en la sección
	'% de detalles de la ventana CRL011 - Generación de cuentas corrientes de coaseguro.
	Public Function insValCRL011(ByVal sCodispl As String, ByVal lintMonth As Integer, ByVal lintYear As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		Dim lintDays As Integer
        Dim lstrDate As String
		
		On Error GoTo insValCRL011_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ They are made the validations of the field Month.
		'+ Se realizan las validaciones del campo Mes.
		
		If lintMonth = 0 Or lintMonth = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1114)
		Else
			If (lintMonth < 1 Or lintMonth > 12) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1115)
			End If
		End If
		
		'**+ They are made the validations of the field Year.
		'+ Se realizan las validaciones del campo Año.
		
		If lintYear = 0 Or lintYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1116)
		End If
		
		If (lintMonth <> 0 And lintMonth <> eRemoteDB.Constants.intNull) And (lintMonth >= 1 And lintMonth <= 12) And (lintYear <> 0 And lintYear <> eRemoteDB.Constants.intNull) Then
			
			'**+ They are extracted the last days for the month in course introduced in the field month.
			'+ Se extraen los últimos días para el mes en curso introducido en el campo mes.
			
			lintDays = insCalc_days(lintMonth, lintYear) 
			
			'**+ One arms to the date with the extracted month and the days introduced
			'**+ in the field days.
			'+ Se arma la fecha con el mes extraído y con los días introducidos en el campo días.
			
            lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(lintMonth)) & "/" & Trim(Str(lintYear))
			
            dLastExecuteDate = CDate(lstrDate)
			
            If IsDate(CDate(lstrDate)) Then
                lclsCtrolDate = New eGeneral.Ctrol_date

                '**+ Se brings the last date of execution of the process of generation of cessions of premiums and
                '**+ se been worth that the aramada date is not greater to the date of execution of the processes of generation of premiums.
                '+ Se trae la última fecha de ejecución del proceso de generación de cesiones de primas y
                '+ se valida que la fecha aramada no sea mayor a la fecha de ejecución del procesos de generación de primas.

                Call lclsCtrolDate.Find(clngGenCessPremium)

                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6132, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If

                '**+ The date of generation of the processes of generation of wrecks is extracted.
                '+ Se extrae la fecha de generación de los procesos de generación de siniestros.
                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                Call lclsCtrolDate.Find(clngGenCessClaim)

                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6133, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If

                '**+ been worth + that the armed date is not greater to the date of execution of generation of accounts
                '**+ techniques of facultative reinsurance.
                '+ Se valida que la fecha armada no sea mayor a la fecha de ejecución de generación de cuentas
                '+ técnicas de reaseguro facultativo.
                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                Call lclsCtrolDate.Find(clngGenTechFacul)

                If CDate(lstrDate) < lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6140, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If

                '**+ Se valida que la fecha armada no sea mayor a la fecha de ejecución de Generación de cuentas corrientes de coaseguro.
                '+ Se valida que la fecha armada no sea mayor a la fecha de ejecución de Generación de cuentas corrientes de coaseguro.

                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                Call lclsCtrolDate.Find(clngGenAccCoin)

                If CDate(lstrDate) <= lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6134, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If

            End If
		End If
		
		insValCRL011 = lclsErrors.Confirm
		
insValCRL011_Err: 
		If Err.Number Then
			insValCRL011 = insValCRL011 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insValCRL012: Función que realiza la validacion de los datos introducidor en la sección
	'% de detalles de la ventana CRL012 - Generación de cuentas corrientes de reaseguro.
	Public Function insValCRL012(ByVal sCodispl As String, ByVal lintMonth As Integer, ByVal lintYear As Integer, ByVal llngCess_type As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		Dim lintDays As Integer
        Dim lstrDate As String
		
		On Error GoTo insValCRL012_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se realizan las validaciones del campo Mes.
		
		If lintMonth = 0 Or lintMonth = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1114)
		Else
			If (lintMonth < 1 Or lintMonth > 12) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1115)
			End If
		End If
		
		'+ Se realizan las validaciones del campo Año.
		
		If lintYear = 0 Or lintYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1116)
		End If
		
		'+ Se realizan las validaciones del campo Tipo de Reaseguro.
		
		If llngCess_type = 0 Or llngCess_type = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 6135)
		End If
		
		If (lintMonth <> 0 And lintMonth <> eRemoteDB.Constants.intNull) And (lintMonth >= 1 And lintMonth <= 12) And (lintYear <> 0 And lintYear <> eRemoteDB.Constants.intNull) Then
			
			'+ Se extraen los últimos días para el mes en curso introducido en el campo mes.
			lintDays = insCalc_days(lintMonth, lintYear)
			
			'+ Se arma la fecha con el mes extraído y con los días introducidos en el campo días.
            lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(lintMonth)) & "/" & Trim(Str(lintYear))
			
            dLastExecuteDate = CDate(lstrDate)
			
            If IsDate(CDate(lstrDate)) Then
                lclsCtrolDate = New eGeneral.Ctrol_date

                '+ Se trae la última fecha de ejecución del proceso de generación de cesiones de primas y
                '+ se valida que la fecha armada no sea mayor a la fecha de ejecución del procesos de generación de primas.

                Call lclsCtrolDate.Find(clngGenCessPremium)

                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6132, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If

                '+ Se extrae la fecha de generación de los procesos de generación de siniestros.
                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                Call lclsCtrolDate.Find(clngGenCessClaim)

                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6133, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If

                '+ Se trae la última fecha de ejecución del proceso de generación de cesiones de primas RNP y
                '+ se valida que la fecha armada no sea mayor a la fecha de ejecución del procesos de generación de primas RNP.
                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                Call lclsCtrolDate.Find(clngGenCessPremiumNonPro)
                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 9000035, , , CStr(lclsCtrolDate.dEffecdate))
                End If

                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull

                '+ Se extrae la fecha de generación de los procesos de generación de siniestros RNP.
                Call lclsCtrolDate.Find(clngGenCessClaimNonPro)
                If CDate(lstrDate) > lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 9000036, , , CStr(lclsCtrolDate.dEffecdate))
                End If


                '+ Se valida que la fecha armada no sea mayor a la fecha de ejecución de generación de cuentas
                '+ técnicas de reaseguro facultativo.
                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                Call lclsCtrolDate.Find(clngGenTechFacul)

                If CDate(lstrDate) < lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6144, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If

                '+ Se valida que la fecha armada no sea mayor a la fecha de ejecución de generación de cuentas
                '+ técnicas de reaseguro obligatorio.
                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                Call lclsCtrolDate.Find(clngGenTechMand)
                If CDate(lstrDate) < lclsCtrolDate.dEffecdate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6044, , , CStr(lclsCtrolDate.dEffecdate))
                End If


                '+ Se valida que la fecha armada no sea mayor a la fecha de ejecución de Generación de cuentas corrientes de reaseguro facultativo.
                lclsCtrolDate.dEffecdate = eRemoteDB.Constants.dtmNull
                If llngCess_type = 2 Then
                    Call lclsCtrolDate.Find(clngGenAccReinF)

                    If CDate(lstrDate) <= lclsCtrolDate.dEffecdate Then
                        Call lclsErrors.ErrorMessage(sCodispl, 6134, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                    End If
                ElseIf llngCess_type = 3 Then
                    Call lclsCtrolDate.Find(clngGenAccReinO)
                    If CDate(lstrDate) <= lclsCtrolDate.dEffecdate Then
                        Call lclsErrors.ErrorMessage(sCodispl, 6134, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                    End If
                ElseIf llngCess_type = 4 Then
                    Call lclsCtrolDate.Find(clngGenAccReinNonPro)
                    If CDate(lstrDate) <= lclsCtrolDate.dEffecdate Then
                        Call lclsErrors.ErrorMessage(sCodispl, 6134, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                    End If
                End If
            End If
		End If
		
		insValCRL012 = lclsErrors.Confirm
		
insValCRL012_Err: 
		If Err.Number Then
			insValCRL012 = insValCRL012 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function

    '**%insValCRL013: Function that makes the validation of the data introducidor in the section
    '**% %de details of window CRL013 - Generation of cessions of premiums RNP.
    '%insValCRL013: Función que realiza la validacion de los datos introducidor en la sección
    '%de detalles de la ventana CRL013 - Generación de cesiones de primas RNP.
    Public Function insValCRL013(ByVal sCodispl As String, ByVal dDateStart As Date, ByVal dDateTo As Date) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsCtrolDate As eGeneral.Ctrol_date

        On Error GoTo insValCRL013_Err

        lclsErrors = New eFunctions.Errors

        '**+ The validations of the date are even made.
        '+ Se realizan las validaciones de la fecha hasta.

        If dDateStart = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 6025)
        Else

            '**+ Validation for the date of execution with respect to the control file.
            '+ Validación para la fecha de ejecución con respecto al archivo de control.

            lclsCtrolDate = New eGeneral.Ctrol_date
            Call lclsCtrolDate.Find(clngGenCessPremiumNonPro)
            If dDateStart < lclsCtrolDate.dEffecdate Then
                Call lclsErrors.ErrorMessage(sCodispl, 6024, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
            Else
                If dDateStart > Today Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7161, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If
            End If

            lclsCtrolDate = Nothing
        End If


        '**+ The validations of the date are even made.
        '+ Se realizan las validaciones de la fecha hasta.

        If dDateTo = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 300018)
        Else

            '**+ Validation for the date of execution with respect to the control file.
            '+ Validación para la fecha de ejecución con respecto al archivo de control.

            lclsCtrolDate = New eGeneral.Ctrol_date
            Call lclsCtrolDate.Find(clngGenCessPremium)
            If dDateTo > Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 7161, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
            Else
                If dDateTo <= dDateStart Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6024, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If
            End If

            lclsCtrolDate = Nothing
        End If

        insValCRL013 = lclsErrors.Confirm

insValCRL013_Err:
        If Err.Number Then
            insValCRL013 = insValCRL013 & Err.Description
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function
    '% insValCRL847_1: Función que realiza la validacion de los datos introducidor en la sección
    '% de Cabecera de la ventana CRL847_1 - Resumen consolidado de Cúmulo.
    Public Function insValCRL046(ByVal nBranch As Double, ByVal nProduct As Double, ByVal nPolicy As Double) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsCtrolDate As eGeneral.Ctrol_date
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo insValCRL046_Err

        lclsErrors = New eFunctions.Errors

        '+ Se realizan las validaciones del campo Póliza.
        If nPolicy > 0 Then
            If nProduct <= 0 Then
                Call lclsErrors.ErrorMessage("CRL046", 11009)
            End If
            If nBranch <= 0 Then
                Call lclsErrors.ErrorMessage("CRL046", 11135)
            End If
            If nBranch > 0 And nProduct > 0 And nPolicy > 0 Then
                lclsPolicy = New ePolicy.Policy
                If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
                    Call lclsErrors.ErrorMessage("CRL046", 8071)
                End If
            End If
        End If

 

        insValCRL046 = lclsErrors.Confirm

insValCRL046_Err:
        If Err.Number Then
            insValCRL046 = insValCRL046 & Err.Description
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function
	'% insValCRL663: Función que realiza la validación de los datos introducidos en la sección
	'% de detalles de la ventana CRL663 - Generación de ordenes de pago de una cuenta técnica.
	Public Function insValCRL663(ByVal sCodispl As String, ByVal nMonth As Integer, ByVal nYear As Integer, ByVal dProcessdate As Date, ByVal nCompany_own As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		Dim lclsPayOrdConcepts As eCashBank.pay_ord_concepts
		
		On Error GoTo insValCRL663_Err
		
		lclsErrors = New eFunctions.Errors
		lclsCtrolDate = New eGeneral.Ctrol_date
		lclsPayOrdConcepts = New eCashBank.pay_ord_concepts
		
		'+ Si el concepto no existe para la compañia en tratamiento
		If lclsPayOrdConcepts.Find(nCompany_own, 5) Then
			If lclsPayOrdConcepts.sStatregt <> "1" Then
				Call lclsErrors.ErrorMessage(sCodispl, 55888)
			End If
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 55888)
		End If
		
		'+ Se realizan las validaciones del campo Mes.
		If nMonth = 0 Or nMonth = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1114)
		Else
			If (nMonth < 1 Or nMonth > 12) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1115)
			End If
		End If
		
		'+ Se realizan las validaciones del campo Año.
		If nYear = 0 Or nYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1116)
		End If
		
		'+ Se realizan las validaciones del campo fecha de proceso.
		If (dProcessdate = eRemoteDB.Constants.dtmNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 7056)
		Else
			Call lclsCtrolDate.Find(clngGenCoRein)
			
			If CDate(dProcessdate) <= CDate(lclsCtrolDate.dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 7057)
			End If
		End If
		
		insValCRL663 = lclsErrors.Confirm
		
insValCRL663_Err: 
		If Err.Number Then
			insValCRL663 = insValCRL663 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrolDate = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPayOrdConcepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPayOrdConcepts = Nothing
	End Function
	'% insValCRL847_1_K: Función que realiza la validacion de los datos introducidor en la sección
	'% de Cabecera de la ventana CRL847_1 - Resumen consolidado de Cúmulo.
	Public Function insValCRL847_1_K(ByVal sDesTipo As Integer, ByVal sDesCover As String, ByVal nBranch As Integer, ByVal ValMaxRet As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		Dim lintDays As Integer
        Dim lstrDate As String
		
		On Error GoTo insValCRL847_1_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se realizan las validaciones del campo Mes.
		If sDesTipo = 0 Then
			Call lclsErrors.ErrorMessage("CRL847_1", 60529)
		End If
		If sDesCover = "" Then
			Call lclsErrors.ErrorMessage("CRL847_1", 60530)
		End If
		
		If ValMaxRet <> eRemoteDB.Constants.intNull Then
			If ValMaxRet <= 0 Then
				Call lclsErrors.ErrorMessage("CRL847_1", 60549)
			End If
		End If
		
		If (dEffecdate = eRemoteDB.Constants.dtmNull) Then
			Call lclsErrors.ErrorMessage("CRL847_1", 4095)
		End If
		
		insValCRL847_1_K = lclsErrors.Confirm
		
insValCRL847_1_K_Err: 
		If Err.Number Then
			insValCRL847_1_K = insValCRL847_1_K & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'% insValCRL893: Función que realiza la validacion de los datos introducidor en la sección
	'% de Cabecera de la ventana CRL893
	Public Function insValCRL893(ByVal sCodispl As String, ByVal nCompany As Integer, ByVal nNumber As Integer, ByVal nType_rel As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dStartdate As Date, ByVal dEndDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lclsContrnpro As eCoReinsuran.Contrnpro
		
		
		On Error GoTo insValCRL893_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación de la compañía
		If nCompany = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 300021)
		Else
			'+ Validación de las fechas de inicio y fin para el proceso
			If (dStartdate = eRemoteDB.Constants.dtmNull Or dEndDate = eRemoteDB.Constants.dtmNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 70122)
			Else
				'+ Validación del número  de contrato
				If nNumber = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 21062)
					'+ Pertenencia del contrato
				Else
					'+ Contrato proporcional
					If nType_rel = 1 Then
						lclsContrproc = New eCoReinsuran.Contrproc
						If Not lclsContrproc.Find(nNumber, nType, nBranch_rei, dEndDate) Then
							Call lclsErrors.ErrorMessage(sCodispl, 6019)
						End If
						'UPGRADE_NOTE: Object lclsContrproc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsContrproc = Nothing
					Else
						lclsContrnpro = New eCoReinsuran.Contrnpro
						If Not lclsContrnpro.Find(nNumber, nType, nBranch_rei, dEndDate) Then
							Call lclsErrors.ErrorMessage(sCodispl, 6090)
						End If
						'UPGRADE_NOTE: Object lclsContrnpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsContrnpro = Nothing
						'+ Contrato  no proporcional
					End If
				End If
			End If
		End If
		
		insValCRL893 = lclsErrors.Confirm
		
insValCRL893_Err: 
		If Err.Number Then
			insValCRL893 = insValCRL893 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'% insValCRL894: Función que realiza la validacion de los datos introducidor en la sección
	'% de Cabecera de la ventana CRL893
	Public Function insValCRL894(ByVal sCodispl As String, ByVal nMonth_ini As Integer, ByVal nYear_ini As Integer, ByVal nMonth_end As Integer, ByVal nYear_end As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		
		
		On Error GoTo insValCRL894_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación del mes de incio
		If nMonth_ini = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36066)
		Else
			If nMonth_ini <= 0 Or nMonth_ini > 13 Then
				Call lclsErrors.ErrorMessage(sCodispl, 300030)
			End If
		End If
		
		'+ Validación del año incio
		If nYear_ini = eRemoteDB.Constants.intNull Or nYear_ini = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 300028)
		End If
		
		'+ Validación del mes hasta
		If nMonth_end = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36067)
		Else
			If nMonth_end <= 0 Or nMonth_end > 13 Then
				Call lclsErrors.ErrorMessage(sCodispl, 300031)
			End If
		End If
		
		'+ Validación del año hasta
		If nYear_end = eRemoteDB.Constants.intNull Or nYear_end = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 300029)
		End If
		
		If (nYear_end < nYear_ini) Or (nMonth_end < nMonth_ini) Then
			Call lclsErrors.ErrorMessage(sCodispl, 300032)
		End If
		
		insValCRL894 = lclsErrors.Confirm
insValCRL894_Err: 
		If Err.Number Then
			insValCRL894 = insValCRL894 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	Public Function insValCRL895_K(ByVal sCodispl As String, ByVal sAction As String, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lerrTime As New eFunctions.Errors
		Dim lintSta_cheque As Integer
		
		On Error GoTo insValCRL895_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lerrTime
		
		'**+ Validation of the field "Date"
		'+ Validación del campo "Fecha"
		If eRemoteDB.Constants.dtmNull = dInitDate Then
			Call lclsErrors.ErrorMessage(sCodispl, 6128)
		End If
		If eRemoteDB.Constants.dtmNull = dEndDate Then
			Call lclsErrors.ErrorMessage(sCodispl, 6129)
		End If
		
		If lclsValField.ValDate(dInitDate) Then
			If lclsValField.ValDate(dEndDate) Then
				If dEndDate < dInitDate Then
					Call lclsErrors.ErrorMessage(sCodispl, 6130)
				End If
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 7079)
			End If
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 7079)
		End If
		
		insValCRL895_K = lclsErrors.Confirm
		
insValCRL895_K_Err: 
		If Err.Number Then
			insValCRL895_K = insValCRL895_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	'% insValCRL847_1: Función que realiza la validacion de los datos introducidor en la sección
	'% de Cabecera de la ventana CRL847_1 - Resumen consolidado de Cúmulo.
	Public Function insValCRL847_1(ByVal sCod_cumulo As String, ByVal nVal_Max_Ces As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		
		On Error GoTo insValCRL847_1_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se realizan las validaciones del campo Mes.
		If sCod_cumulo <> String.Empty And nVal_Max_Ces = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("CRL847_1", 60547)
		Else
			If sCod_cumulo = String.Empty And nVal_Max_Ces <> eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage("CRL847_1", 60548)
			Else
				If nVal_Max_Ces <= 0 Then
					Call lclsErrors.ErrorMessage("CRL847_1", 60550)
				End If
			End If
		End If
		
		
		insValCRL847_1 = lclsErrors.Confirm
		
insValCRL847_1_Err: 
		If Err.Number Then
			insValCRL847_1 = insValCRL847_1 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'**% UpdateCRL012: It allows to make the call to the SP of update of Generation of pay orderes of thecnical accounts.
	'% UpdateCRL663: Permite realizar el llamado al SP de actualización de Generación de ordenes de pagos de cuentas tecnicas.
	Public Function UpdateCRL663(ByVal nMonth As Integer, ByVal nYear As Integer, ByVal dProcessdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsCRL663 As eRemoteDB.Execute
		
		lclsCRL663 = New eRemoteDB.Execute
		
		'**+ Define all parameters for the stored procedures 'insudb.insCreUpdCRL663'.
		'+ Defina todos los parámetros para los procedimientos salvados 'insudb.insCreUpdCRL663 '.
		
		With lclsCRL663
			.StoredProcedure = "insCreUpdCRL663"
			
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProcessdate", dProcessdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateCRL663 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclsCRL663 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCRL663 = Nothing
	End Function
	
	Public Function insPostCRL839(ByVal nMonth As Integer, ByVal nYear As Integer, ByVal nBranch As Integer) As Boolean
		
		Dim lrecinsPostCRL839 As eRemoteDB.Execute
		
		lrecinsPostCRL839 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostCRL839
			.StoredProcedure = "REA_CRL839"
			.Parameters.Add("P_MES", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_ANO", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_COD_RAMO", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCRL839 = True
			Else
				insPostCRL839 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCRL839 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCRL839 = Nothing
		
	End Function
	
	
	Public Function insPostCRL846(ByVal sCumultyp As String, ByVal nCovergen As Integer, ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostCRL846 As eRemoteDB.Execute
		
		lrecinsPostCRL846 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostCRL846
			.StoredProcedure = "rea_crl846"
			.Parameters.Add("sCumultyp", sCumultyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCRL846 = True
			Else
				insPostCRL846 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCRL846 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCRL846 = Nothing
		
	End Function
	Public Function insPostCRL851(ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
		
		Dim lrecinsPostCRL851 As eRemoteDB.Execute
		
		lrecinsPostCRL851 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostCRL851
			.StoredProcedure = "rea_crl851"
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCRL851 = True
			Else
				insPostCRL851 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCRL851 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCRL851 = Nothing
		
	End Function
	Public Function insPostCRL852(ByVal nMonth As Integer, ByVal nYear As Integer, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal nBranch_fecu As Integer, ByVal nCover As Integer) As Boolean
		
		Dim lrecinsPostCRL852 As eRemoteDB.Execute
		
		lrecinsPostCRL852 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostCRL852
			.StoredProcedure = "rea_crl852"
			.Parameters.Add("P_NMONTH", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_NYEAR", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_NNUMBER", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_NBRANCH", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_NBRANCH_FECU", nBranch_fecu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_NCOVER", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCRL852 = True
			Else
				insPostCRL852 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCRL852 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCRL852 = Nothing
		
	End Function
	Public Function InsPostCRL847_1(ByVal nCumultyp As Double, ByVal nCovergen As Integer, ByVal nBranch As Integer, ByVal dDate As Date, ByVal nAmount As Double, ByVal nindbrok As Integer, ByVal sKey As String) As Boolean
		
		Dim lrecinsPostCRL847_1 As eRemoteDB.Execute
		
		lrecinsPostCRL847_1 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPostCRL847_1
			.StoredProcedure = "rea_crl847_1"
			.Parameters.Add("nCumultyp", nCumultyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate", dDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nindbrok", nindbrok, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SKEY", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				sKey = .Parameters("SKEY").Value
				InsPostCRL847_1 = True
			Else
				InsPostCRL847_1 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCRL847_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCRL847_1 = Nothing
		
	End Function
	
	Public Function InsPostCRL515(ByVal dDateProc As Date, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal sNopayroll As String, ByVal sPolitype As String) As Boolean
		
		Dim lrecInsPostCRL515 As eRemoteDB.Execute
		
		lrecInsPostCRL515 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecInsPostCRL515
			.StoredProcedure = "rea_crl515"
			.Parameters.Add("dDateProc", dDateProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNopayroll", sNopayroll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostCRL515 = True
			Else
				InsPostCRL515 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecInsPostCRL515 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostCRL515 = Nothing
		
	End Function
	
	
    'Public Function InsPostCRL706(ByVal P_COD_CIA As String, ByVal P_PERIODO As String) As Boolean
    Public Function InsPostCRL706(ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal sProcess As String, ByVal nUsercode As Double) As Boolean

        Dim lrecInsPostCRL706 As eRemoteDB.Execute

        lrecInsPostCRL706 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_sil838'
        With lrecInsPostCRL706
            .StoredProcedure = "REA_CRL706"
            .Parameters.Add("dDateIni", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            '.StoredProcedure = "REA_LREASEGURO"
            '.Parameters.Add("P_COD_CIA", P_COD_CIA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("P_PERIODO", P_PERIODO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run(False) Then
                'P_SKEY = .Parameters("P_SKEY").Value
                InsPostCRL706 = True
            Else
                InsPostCRL706 = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecInsPostCRL706 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCRL706 = Nothing

    End Function
	
	'% insPostCRL895: Llena la tabla tmp_crl895, para el reporte CRL895
	Public Function insPostCRL895(ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nCompany As Integer, ByVal nBranchRei As Integer, ByVal sExecute As String) As Boolean
		
		Dim lrecinsPostCRL895 As eRemoteDB.Execute
		
		lrecinsPostCRL895 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_crl895'
		With lrecinsPostCRL895
			.StoredProcedure = "rea_crl895"
			
			.Parameters.Add("dInitDate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranchRei", nBranchRei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Skey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				sKey = .Parameters("Skey").Value
				insPostCRL895 = True
			Else
				insPostCRL895 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCRL895 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCRL895 = Nothing
		
    End Function
    '% insPostCRL895: Llena la tabla tmp_crl895, para el reporte CRL895
    Public Function insPostCRL046(ByVal nBranch As Double, ByVal nProduct As Double, ByVal nPolicy As Double, ByVal nUsercode As Double) As Boolean

        Dim lrecinsPostCRL046 As eRemoteDB.Execute

        lrecinsPostCRL046 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_crl895'
        With lrecinsPostCRL046
            .StoredProcedure = "insCRR046"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                sKey = .Parameters("Skey").Value
                insPostCRL046 = True
            Else
                insPostCRL046 = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostCRL895 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCRL046 = Nothing

    End Function

    '%insValCRL706_K: Esta función se encarga de realizar las respectivas validaciones de la transacción.
    Public Function insValCRL706_K(ByVal sCodispl As String, ByVal dDateIni As Date, ByVal dDateEnd As Date) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsCtrolDate As eGeneral.Ctrol_date

        On Error GoTo insValCRL706_K_Err

        lobjErrors = New eFunctions.Errors

        With lobjErrors

            If dDateIni = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 6128)
            Else
                '**+ Validation for the date of execution with respect to the control file.
                '+ Validación para la fecha de ejecución con respecto al archivo de control.

                lclsCtrolDate = New eGeneral.Ctrol_date
                Call lclsCtrolDate.Find(clngReinBook)
                If dDateIni <= lclsCtrolDate.dEffecdate Then
                    Call .ErrorMessage(sCodispl, 90000044, , , CStr(CDate(lclsCtrolDate.dEffecdate)))
                End If

                'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsCtrolDate = Nothing
            End If

            If dDateEnd = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 6129)
            End If

            If dDateIni > dDateEnd Then
                .ErrorMessage(sCodispl, 1132)
            End If

            insValCRL706_K = .Confirm
        End With

insValCRL706_K_Err:
        If Err.Number Then
            insValCRL706_K = "insValCRL706_K: " & insValCRL706_K & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '**% UpdateCRL001: It allows to make the call to the SP of update of Cessions of premiums.
    '% UpdateCRL001: Permite realizar el llamado al SP de actualización de Cesiones de primas.
    Public Function InsCRL005(ByVal dInit_date As Date, ByVal dEnd_date As Date, ByVal nCompany As Integer, ByVal nCurrency As Integer, ByVal nType_cessi As Integer, ByVal nBranch_rei As Integer, ByVal sKey As String, ByVal nUsercode As Integer) As Boolean


        Dim lclsCession_pr As eRemoteDB.Execute

        On Error GoTo UpdateCRL005_Err

        lclsCession_pr = New eRemoteDB.Execute

        '**+ Define all parameters for the stored procedures 'insudb.insUpdCRL001'. Generated on 18/12/2001 02:28:01 p.m.
        '+ Defina todos los parámetros para los procedimientos salvados 'insudb.insUpdCRL001 '. Generado en 18/12/2001 02:28:01 P.M..

        With lclsCession_pr
            .StoredProcedure = "InsCRL005"
            .Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_cessi", nType_cessi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsCRL005 = .Run(False)
            If InsCRL005 Then
                Reacrl005(sKey, nUsercode)
            End If
        End With

UpdateCRL005_Err:
        If Err.Number Then
            InsCRL005 = False
        End If
        'UPGRADE_NOTE: Object lclsCession_pr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCession_pr = Nothing
        On Error GoTo 0

    End Function
    '% Find: Permite cargar en la colección los datos de la tabla Collect_comm
    Public Function Reacrl005(ByVal sKey As String, ByVal nUsercode As Integer) As Boolean

    End Function
End Class






