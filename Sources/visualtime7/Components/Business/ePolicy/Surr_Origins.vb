Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Surr_origins_NET.Surr_origins")> Public Class Surr_origins
	'+Objetivo: Clase que le da soporte a la tabla Surr_origins
	'+          cuyo contenido es: Cuentas origen asociadas a un Rescate de póliza  Un registro por cada cuenta afectada en el Rescate
	'+Version: $$Revision: 12 $
	
	Public sCertype As String '-Tipo de registro. Valores únicos:    1 - Solicitud    2 - Póliza    3 - Cotización
	Public nBranch As Integer '-Código del ramo comercial. Valores posibles según tabla 10.
	Public nProduct As Integer '-Código del producto.
	Public nPolicy As Double '-Número identificativo de la póliza/ cotización/ solicitud
	Public nCertif As Double '-Número identificativo del certificado
	Public dEffecdate As Date '-Fecha de efecto del registro.
	Public nOrigin_apv As Integer '-Cuenta origen de los depositos APV
	Public dNulldate As Date '-Fecha de anulación del registro.
	Public nAvailable As Double '-Monto disponible para rescate por cada cuenta origen
	Public nAmount As Double '-Monto del Rescate
	Public nCost_amo As Double '-Monto de Retención
	Public nRet_amo As Double '-Monto de Retención
	Public nUsercode As Integer '-Código del usuario que crea o actualiza el registro.
	Public nSurr_reason As Integer '-Razón del rescate.
	Public sSurrTotal As String '-Indicador de Rescate Total: 1-Verdadero, 2-Falso
	Public nStatquota As Integer '-Estado de la propuesta de rescate asociada
	Public nProponum As Double '-Número de la propuesta de rescate asociada
	
	Public sSel_origin As String '-Indicador de cuenta origen seleccionada
	Public nWDCost As Double '-Costo asociado al retiro solicitado
	Public nRequestedamount As Double '-Monto del retiro solicitado
	Public nGrossAmount As Double '-Valor póliza antes de los descuentos
	Public nTyp_Profitworker As Integer '-Tipo de beneficio Tributario Table950
	Public nCost_cov As Double '-Costo covertura asociado al retiro solicitado
	Public nLoans As Double '-Monto préstamo asociado al retiro solicitado
	Public nIntLoans As Double '-Monto interés por préstamo asociado al retiro solicitado
	Public dPaymentdate As Date '-Fecha de valorizacion del rescate
	Public nAgency As Integer '-Agencia de pago rescate
	Public nLocal_amount As Integer
	Public nExchange As Double
    Public nCost_cov_dev As Double
    Public nRentability As Double
    Public nAmount_rec_dev As Double
    Public nAmount_dev As Double
	'%Objetivo: Lee todos las cuentas origen asociadas al rescate de póliza
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin_apv As Integer) As Boolean
		Dim lrecreaSurr_origins As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecreaSurr_origins = New eRemoteDB.Execute
		
		Find = True
		
		With lrecreaSurr_origins
			.StoredProcedure = "reaSurr_origins"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin_APV", nOrigin_apv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find = .Run
			
			If Find Then
				nAvailable = .FieldToClass("nAvailable")
				nAmount = .FieldToClass("nAmount")
				nCost_amo = .FieldToClass("nCost_Amo")
				nRet_amo = .FieldToClass("nRet_Amo")
                nCost_cov_dev = .FieldToClass("nCost_cov_dev")
                nRentability = .FieldToClass("nRentability")
                nAmount_rec_dev = .FieldToClass("nAmount_rec_dev")
                nAmount_dev = .FieldToClass("nAmount_dev")
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSurr_origins = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSurr_origins = Nothing
		
		Find = False
	End Function
	
	'%Objetivo: Lee todos las cuentas origen asociadas al rescate de póliza
	Public Function Find_tot(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dPaydate As Date) As Boolean
		Dim lrecreaSurr_origins As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecreaSurr_origins = New eRemoteDB.Execute
		
		Find_tot = True
		
		With lrecreaSurr_origins
			.StoredProcedure = "reaSurr_Orig_tot"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPaydate", dPaydate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_tot = .Run
			
			If Find_tot Then
				nAmount = .FieldToClass("nRequested_amount")
				nRequestedamount = .FieldToClass("nRequested_local")
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSurr_origins = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSurr_origins = Nothing
		
		Find_tot = False
	End Function
	
	
	'**%Objective: Add an element in the table T_Surr_origins
	'%Objetivo: Permite registrar un elemento en la tabla T_Surr_origins
    Public Function CreT_Surr_Origins(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                                      ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, _
                                      ByVal nOrigin_apv As Integer, ByVal nAvailable As Double, ByVal nAmount As Double, _
                                      ByVal nCost_amo As Double, ByVal nRet_amo As Double, ByVal nUsercode As Double, _
                                      ByVal nSurr_reason As Integer, ByVal sSurrTotal As String, Optional ByVal nRequestedSurrAmt As Double = 0, _
                                      Optional ByVal nWDCost As Double = 0, Optional ByVal nTyp_Profitworker As Integer = 0, Optional ByVal nCost_cov As Double = 0, _
                                      Optional ByVal nLoans As Double = 0, Optional ByVal nIntLoans As Double = 0, Optional ByVal dPaymentdate As Date = #12:00:00 AM#, _
                                      Optional ByVal nAgency As Integer = 0, Optional ByVal nCost_cov_dev As Double = 0, Optional ByVal nRentability As Double = 0, _
                                      Optional ByVal nAmount_rec_dev As Double = 0, Optional ByVal nAmount_dev As Double = 0) As Boolean
        Dim lreccreSurr_origins As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lreccreSurr_origins = New eRemoteDB.Execute

        CreT_Surr_Origins = True

        With lreccreSurr_origins
            .StoredProcedure = "InsCreT_Surr_Origins"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin_APV", nOrigin_apv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAvailable", nAvailable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCost_Amo", nCost_amo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRet_Amo", nRet_amo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSurrTotal", sSurrTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPropoNum", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRequested_Amount", nRequestedSurrAmt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWd_cost", nWDCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_Profitworker", nTyp_Profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCost_cov", nCost_cov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLoans", nLoans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntloans", nIntLoans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPaymentDate", dPaymentdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nGross_balance", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExchange", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("scodispl", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRequested_orig", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_pend", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_adjus", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            .Parameters.Add("nCost_cov_dev", nCost_cov_dev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRentability", nRentability, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_rec_dev", nAmount_rec_dev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_dev", nAmount_dev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            CreT_Surr_Origins = .Run(False)
        End With

        'UPGRADE_NOTE: Object lreccreSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreSurr_origins = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lreccreSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreSurr_origins = Nothing

        CreT_Surr_Origins = False
    End Function
	
	'%valAllowedWDPercent: Validaciones del Grid de la Tranzacción VI7000
	Private Sub valAllowedWDPercent(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nSurrAmount As Double, ByVal nAvailable As Double, ByRef lobjErrors As eFunctions.Errors)
		Dim lrecWDLimit As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecWDLimit = New eRemoteDB.Execute
		
		
		With lrecWDLimit
			.StoredProcedure = "valAllowedWDPercent"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAvailable", nAvailable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NSURRAMOUNT", nSurrAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("nValid") <> 1 Then
					lobjErrors.ErrorMessage(sCodispl, 90175,  , eFunctions.Errors.TextAlign.RigthAling, "(" & .FieldToClass("nPercent") & "%)")
				End If
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecWDLimit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWDLimit = Nothing
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecWDLimit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWDLimit = Nothing
		Err.Raise(Err.Number, Err.Source, Err.Description)
		
	End Sub
	
	
	
	'%valAllowedWDPercent: Validaciones del Grid de la Tranzacción VI7000
	Private Sub valAllowedWDLimit(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nRequestedSurrAmount As Double, ByVal nAvailable As Double, ByRef lobjErrors As eFunctions.Errors)
		Dim lclsProduct As eProduct.Product
		
		lclsProduct = New eProduct.Product
		Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
		If nRequestedSurrAmount > lclsProduct.nAmaxsurr And lclsProduct.nAmaxsurr > 0 Then
			lobjErrors.ErrorMessage(sCodispl, 60309,  , eFunctions.Errors.TextAlign.RigthAling, "(" & lclsProduct.nAmaxsurr & " UF)")
		ElseIf nRequestedSurrAmount < lclsProduct.nAminsurr And lclsProduct.nAminsurr >= 0 Then 
			lobjErrors.ErrorMessage(sCodispl, 60309,  , eFunctions.Errors.TextAlign.RigthAling, "(" & lclsProduct.nAminsurr & " UF)")
		End If
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		Err.Raise(Err.Number, Err.Source, Err.Description)
		
	End Sub
	
	'%InsValVI7000_Upd: Validaciones del Grid de la Tranzacción VI7000
	Public Function InsValVI7000_Upd(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nOrigin_apv As Integer, ByVal dEffecdate As Date, ByVal nAvailBal As Double, ByVal nSurrAmount As Double, Optional ByVal sSurrType As String = "", Optional ByVal nRequestedSurrAmt As Double = 0, Optional ByVal nWDCost As Double = 0, Optional ByVal nSurr_reason As Integer = 0, Optional ByVal nLoans As Double = 0, Optional ByVal nIntLoans As Double = 0) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		Dim lclsUl_Move_Acc_pol As ePolicy.ul_Move_Acc_pol
		Dim lrecValVI7000 As eRemoteDB.Execute
        Dim lstrError As String = String.Empty
		
		Dim nAvailBal_aux As Double
		
		lrecValVI7000 = New eRemoteDB.Execute
		
		On Error GoTo InsValVI7000_Err
		
		lobjErrors = New eFunctions.Errors
		lclsUl_Move_Acc_pol = New ePolicy.ul_Move_Acc_pol
		
		
		'+ Se valida el campo Monto del rescate
		If nSurrAmount = 0 Or nSurrAmount = eRemoteDB.Constants.intNull Then
			lobjErrors.ErrorMessage(sCodispl, 70049)
			lblnError = True
		ElseIf nSurrAmount < 0 Then 
			lobjErrors.ErrorMessage(sCodispl, 70050)
			lblnError = True
		Else
			If sSurrType = "2" Then
				
				nAvailBal_aux = nAvailBal
				
				If nLoans > 0 And nLoans <> eRemoteDB.Constants.intNull Then
					nAvailBal_aux = nAvailBal_aux + nLoans
				End If
				If nIntLoans > 0 And nIntLoans <> eRemoteDB.Constants.intNull Then
					nAvailBal_aux = nAvailBal_aux + nIntLoans
				End If
				If nSurrAmount > nAvailBal_aux Then
					lobjErrors.ErrorMessage(sCodispl, 70051)
					lblnError = True
				End If
			End If
		End If
		
		'+ El rescate por traspaso AG no debe validar los limites del producto
		If nSurr_reason <> 5 Then
			If sSurrType <> "1" Then
				Call valAllowedWDLimit(sCodispl, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nRequestedSurrAmt, nAvailBal, lobjErrors)
				'Call valAllowedWDPercent(sCodispl, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nSurrAmount, nAvailBal, lobjErrors)
				lblnError = True
			End If
		End If
		
		With lrecValVI7000
			.StoredProcedure = "INSVALVI7000_UPD"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequested_amount", nRequestedSurrAmt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			lstrError = .Parameters("sArrayerrors").Value
			
			If lstrError <> String.Empty Then
				lobjErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
				lblnError = True
			End If
		End With
		
		InsValVI7000_Upd = lobjErrors.Confirm
		
InsValVI7000_Err: 
		If Err.Number Then
			InsValVI7000_Upd = "InsValVI7000_Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsUl_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUl_Move_Acc_pol = Nothing
	End Function
	
	'**%Objective: Updates the information in the grid of Tx VI7000
	'%Objetivo: Permite actualizar los datos del grid de la Tx VI7000
    Public Function insPostVI7000_Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, _
                                      ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, _
                                      ByVal dEffecdate As Date, ByVal nOrigin_apv As Integer, ByVal nAvailable As Double, _
                                      ByVal nAmount As Double, ByVal nCost_amo As Double, ByVal nRet_amo As Double, _
                                      ByVal nUsercode As Integer, ByVal nSurr_reason As Integer, ByVal sSurrTotal As String, _
                                      Optional ByVal nRequestedSurrAmt As Double = 0, Optional ByVal nWDCost As Double = 0, Optional ByVal dPaymentdate As Date = #12:00:00 AM#, _
                                      Optional ByVal nCost_cov As Double = 0, Optional ByVal nLoans As Double = 0, Optional ByVal nIntLoans As Double = 0, _
                                      Optional ByVal nAgency As Integer = 0, Optional ByVal nCost_cov_dev As Double = 0, Optional ByVal nRentability As Double = 0, _
                                      Optional ByVal nAmount_rec_dev As Double = 0, Optional ByVal nAmount_dev As Double = 0) As Boolean

        On Error GoTo insPostVI7000_Upd_err

        insPostVI7000_Upd = True

        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nOrigin_apv = nOrigin_apv
            .nAvailable = nAvailable
            .nAmount = nAmount
            .nCost_amo = nCost_amo
            .nRet_amo = nRet_amo
            .nUsercode = nUsercode
            .nSurr_reason = nSurr_reason
            .sSurrTotal = sSurrTotal
            .nRequestedamount = nRequestedSurrAmt
            .nWDCost = nWDCost
            .dPaymentdate = dPaymentdate
            .nCost_cov = nCost_cov
            .nLoans = nLoans
            .nIntLoans = nIntLoans
            .nAgency = nAgency

            .nCost_cov_dev = nCost_cov_dev
            .nRentability = nRentability
            .nAmount_rec_dev = nAmount_rec_dev
            .nAmount_dev = nAmount_dev

            '**+ If is an underwritten, recovery o normal modification in the same day
            '+ Si es emisión, recuperación o modificación normal el mismo dia
            If sAction = "Add" Then
                Call CreT_Surr_Origins(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nOrigin_apv, nAvailable, nAmount, nCost_amo, nRet_amo, nUsercode, nSurr_reason, sSurrTotal, nRequestedamount, nWDCost, 0, nCost_cov, nLoans, nIntLoans, dPaymentdate, nAgency, nCost_cov_dev, nRentability, nAmount_rec_dev, nAmount_dev)
            End If
        End With

insPostVI7000_Upd_err:
        If Err.Number Then
            insPostVI7000_Upd = False
        End If
        On Error GoTo 0

    End Function
	
	'**%Objective: Updates the information in the grid of Tx VI7004
	'%Objetivo: Permite actualizar los datos del grid de la Tx VI7004
    Public Function insPostVI7004_Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, _
                                      ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, _
                                      ByVal dEffecdate As Date, ByVal nOrigin_apv As Integer, ByVal nAvailable As Double, _
                                      ByVal nAmount As Double, ByVal nCost_amo As Double, ByVal nRet_amo As Double, _
                                      ByVal nUsercode As Integer, ByVal nSurr_reason As Integer, ByVal sSurrTotal As String, _
                                      Optional ByVal nRequestedSurrAmt As Double = 0, Optional ByVal nWDCost As Double = 0, _
                                      Optional ByVal nTyp_Profitworker As Integer = 0, Optional ByVal dPaymentdate As Date = #12:00:00 AM#, _
                                      Optional ByVal nCost_cov As Double = 0, Optional ByVal nLoans As Double = 0, Optional ByVal nIntLoans As Double = 0, _
                                      Optional ByVal nAgency As Integer = 0, Optional ByVal nTypeResc As Integer = 0, _
                                      Optional ByVal nCost_cov_dev As Double = 0, Optional ByVal nRentability As Double = 0, _
                                         Optional ByVal nAmount_rec_dev As Double = 0, Optional ByVal nAmount_dev As Double = 0) As Boolean
        Dim lRec_Surr_originss As Surr_originss
        Dim ldblPercent As Double

        lRec_Surr_originss = New Surr_originss

        On Error GoTo insPostVI7004_Upd_err

        insPostVI7004_Upd = True

        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nOrigin_apv = nOrigin_apv
            .nAvailable = nAvailable
            .nAmount = nAmount
            .nCost_amo = nCost_amo
            .nRet_amo = nRet_amo
            .nUsercode = nUsercode
            .nSurr_reason = nSurr_reason
            .sSurrTotal = sSurrTotal
            .nRequestedamount = nRequestedSurrAmt
            .nWDCost = nWDCost
            .nTyp_Profitworker = nTyp_Profitworker
            .dPaymentdate = dPaymentdate
            .nCost_cov = nCost_cov
            .nLoans = nLoans
            .nIntLoans = nIntLoans
            .nAgency = nAgency
            .nCost_cov_dev = nCost_cov_dev
            .nRentability = nRentability
            .nAmount_rec_dev = nAmount_rec_dev
            .nAmount_dev = nAmount_dev

            If nAvailable > 0 Then
                ldblPercent = (nRequestedSurrAmt / nAvailable) * 100
            Else
                ldblPercent = 0
            End If

            Call lRec_Surr_originss.InsPreVI7004_Origins(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0, nSurr_reason, sSurrTotal, nUsercode, "0", 2, nTypeResc, "2", nOrigin_apv, nRequestedSurrAmt, nTyp_Profitworker, dPaymentdate, , ldblPercent)

        End With

insPostVI7004_Upd_err:
        If Err.Number Then
            insPostVI7004_Upd = False
        End If
        On Error GoTo 0

    End Function
	
	
	'**%Objective: Add an element in the table Surr_origins
	'%Objetivo: Permite registrar un elemento en la tabla Surr_origins
    Public Function Cre_Surr_Origins(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                                     ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, _
                                     ByVal nUsercode As Double, ByVal nSurr_reason As Integer, ByVal sSurrTotal As String, _
                                     ByVal nProponum As Double, ByVal sProcessType As String, Optional ByVal nSaapv As Double = 0) As Boolean
        Dim lreccreSurr_origins As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lreccreSurr_origins = New eRemoteDB.Execute

        Cre_Surr_Origins = True

        With lreccreSurr_origins
            .StoredProcedure = "InsCre_Surr_Origins"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSurrTotal", sSurrTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcessType", sProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSaapv", nSaapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Cre_Surr_Origins = .Run(False)
        End With

        'UPGRADE_NOTE: Object lreccreSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreSurr_origins = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lreccreSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreSurr_origins = Nothing

        Cre_Surr_Origins = False
    End Function
End Class






