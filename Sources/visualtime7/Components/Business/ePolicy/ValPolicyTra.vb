Option Strict Off
Option Explicit On
Option Compare Text
Public Class ValPolicyTra
	'%-------------------------------------------------------%'
	'% $Workfile:: ValPolicyTra.cls                         $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 29/10/09 5:07p                               $%'
	'% $Revision:: 28                                       $%'
	'%-------------------------------------------------------%'
	
	'- Opciòn de indemnizaciòn A
	Const C_OPTION_A As Short = 1
	
	Public mdtmIssuedat As Date
	'+ This variables will be used in the CA032
	'+ Variables a ser utilizadas en la CA032
	Public dTransDate As Date
	Public nTransactio As Integer
	Public bNullReceipt As Boolean
	Public sPolitype As String
	Public sColtimre As String
	Public sColinvot As String
	Public nNullOutMov As Integer
	Public sReverCertif As String
	Public nIntermed As Integer
	Public sInterName As String
	Public sClient As String
	Public sClientName As String
	
	'- Variables a ser utilizadas en la CA034
	Public sBrancht As String
	Public nProdClas As Double
	Public nProposal As Double
	
	'- Variables a ser utilizadas en la PREVAL633
	Public nPayfreq As Integer
	Public sCliDigit As String
	Public nNegVPMonths As Integer
	Public dNextReceip As Date
	
	Public dOperDate As Date
	Public nType_Move As Integer
	Public nAmount As Double
	Public nQMonths As Integer
	Public nPending_cost As Double
	Public nCurr_pending_cost As Double
	Public nRequired_pending_cost As Double
	
	'-Clave de procesos batch
	Public sKey As String
	
	'+ Variables a ser utilizadas en la CA034 Y CA033
	Private mstrReh_lrec As String
	Private mstrNull_rec As String
	Private mdtmEffecdate As Date
	
	'+ Variables a ser utilizadas en la CA088
	Public dRecepInsu As Date
	Public dRecepInsu_Comp As Date
	Public dRecepInt As Date
	Public dRecepInt_Comp As Date
	
	'- Valor Poliza
	Private mdblVP As Double
	
	'- Monto de Rescate
	Private mdblSurr As Double
	Private mdblRoutSurr As Double
	Private mdblSurrPrtlTran As Double
	Private mdblOrigin As Double
	
	'- Costo de Rescate
	Private mdblSurrCost As Double
	'- Costo de Rescate (Sobre el total disponible)
	Private mdblSurrCostIni As Double
	'Retencion
	Private mdblRetention As Double
	'Retencion %
	Private mdblnRet_Pct As Double
	'% de cargo para rescates parciales
	Private mdblPct_charge As Double
	'Monto fijo de cargo para rescates parciales
	Private mdblFix_charge As Double
	'Monto Máximo de cargo para rescates parciales
	Private mdblMaxChargSurr As Double
	
	'- Indicador de VidActiva
	Private mblnProdAL As Boolean
	'- Suma de cheques a fecha
	Private mdblSumCheque As Double
	'- Capital de saldado
	Private mdblSaldCapital As Double
	'- Prima de saldado
	Private mdblSaldPremium As Double
	'- Tasa para calculo de moneda local
	Private mdblExchange As Double
	Public nExchange_aux As Double
	'- Monto de los recibos pendientes de origen "Intereses por préstamo"
	Public mdblIntLoans As Double
	Public mdblLoans As Double
	
	'-Monto del balance
	Private mdblBalance As Double
	'-Moneda de la poliza
	Private mlngCurrency As Integer
	'-Indicador de producto con APV
	Private mstrApv As String
	
	'- Indicador rescate total es valido por el producto
	Private mstrSurrenTi As String
	
	'-Indica si se marca la opción de generar propuesta
	Private mstrRequest As String
	
	'-Variables para obtener los valores por defectos de la cabezera de la VI009
	'-Tipo de rescate
	Private mintTyp_surr As Integer
	
	'-Script para la acción cancelar
	Private mstrScriptCancel As String
	
	'-Tipo de proceso
	Private mintProcessType As Integer
	
	'-Datos de la poliza a pagar el rescate
	Private mstrCertPaySurr As String
	Private mlngBraPaySurr As Integer
	Private mlngProPaySurr As Integer
	Private mlngPolPaySurr As Integer
	Private mlngCerPaySurr As Integer
	
	'-Nota asociada al cliente
	Private mlngNotenum As Integer
	
	'-Monto de los rescates anteriores
	Private mdblAmosurren As Double
	
	'-Rescate total o parcial
	Private mstrSurrtotal As String
	
	'-Institucion financiera destino de fondos
	Private mlngInstitution As Integer
	'-Razon del rescate
	Private mlngSurr_reason As Integer
	'-Tipo de orden de pago, segun tabla 5636
	Private mlngType_payment As Integer
	
	'-Valor del rescate
	Private mdblSurrValue As Double
	
	'-Valor del rescate
	Private mdblAmorescpar As Double
	
	'-Rescate (Definitivo)
	Private mdblRescDef As Double
	
	'-Costo del rescate parcial
	Private mdblSurrCostPar As Double
	
	'-Monto maximo del prestamo
	Private mdblAmomaxloans As Double
	
	'-Interes anual del prestamo
	Private mdblInterest As Double
	
    '-Costo cobertura a devolver
    Private mdblCost_cov_dev As Double
    '-Rentabilidad a abonar
    Private mdblRentability As Double
    '-Monto de recibos a devolver
    Private mdblAmount_rec_dev As Double
    '-Otros montos  a devolver
    Private mdblAmount_dev As Double


	'-Monto maximo del prestamo en moneda local
	Private mdblAmomaxloans_loc As Double
	
	'-Valor del rescate en moneda local
	Private mdblSurrvalue_loc As Double
	
	'- porcentaje de impuesto aplicado
	Private mdblTax As Double
	
	'- Insidicador de si aplica el cálculo de retención al monto del rescate
	Private mdblTax_Rent As Double
	
	'- Fecha estimada de pago
	Public dPaymentdate As Date
	
	'- Años transcurridos
	Public nYear As Short
	
	'- Meses transcurridos
	Public nMonth As Short
	
	'- Meses transcurridos
	Public nPremium As Double
	
	'- Arreglo para la carga de data VI770 [APV2] - ACM - 01/09/2003
	Private Structure udtVI770
		Dim dOperDate As Date
		Dim nType_Move As Integer
		Dim nAmount As Double
	End Structure
	
	Private marrVI770() As udtVI770
	
	Private Enum ePeriodType
		perTypAño = 1
		perTypSem = 2
		perTypTrim = 3
		perTypBim = 4
		perTypMes = 5
		perTypPol = 6
	End Enum
	
	Public sUser_dRecepInsu As String
	Public sUser_dRecepInsu_Comp As String
	Public sUser_dRecepInt As String
	Public sUser_dRecepInt_Comp As String
	
	Public nPolicyDuration As Short
	Public nWDCount As Short
	Public nSaapv As Double
    Public sClientInstitution As String 
    Public nRet_Pct As Double



    Private Sub getInterval(ByVal p_PeriodType As Integer)

        Dim lstrInterval As String

        Select Case p_PeriodType
            Case ePeriodType.perTypAño
                lstrInterval = "y"
            Case ePeriodType.perTypSem
                lstrInterval = ""
            Case ePeriodType.perTypTrim
                lstrInterval = ""
            Case ePeriodType.perTypBim
                lstrInterval = ""
            Case ePeriodType.perTypMes
                lstrInterval = "m"
            Case ePeriodType.perTypPol
                lstrInterval = ""
        End Select
    End Sub

    '%InsValVI7000: Validaciones de la cabecera de forma VI7000
    Public Function InsValVI7000(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sCodispl As String, ByVal nSurrReas As Integer, ByVal nTotal_Surr As Double, ByVal nPmtOrd As Integer, ByVal dRetirement As Date, ByVal dBirthdate As Date, ByVal dEffecdate As Date, ByVal nSurr_Avail As Double, ByVal sSurrType As String, ByVal sApv As String, ByVal nType_exec As Integer, ByVal nCurrency As Double, ByVal nTotal As Double, ByVal schkSurrTot As String, ByVal nOrigin As Integer, ByVal sClient_dest As String, ByVal sSche_Code As String, Optional ByVal bIsCancelling As Boolean = False, Optional ByVal nSaapv As Double = 0) As String
		Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
        Dim lrecInsValVI7000 As eRemoteDB.Execute
        Dim lclsSecurity As eSecurity.SecurScheSurr
        Dim nTypeResc As Short
        On Error GoTo InsValVI7000_Err

        '+Se leeen las condiciones de seguridad de acuerdo al perfil
        lclsSecurity = New eSecurity.SecurScheSurr

        If lclsSecurity.Find(sSche_Code, False) Then
            nTypeResc = lclsSecurity.nTypeResc
        Else
            nTypeResc = 5
        End If

        'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSecurity = Nothing
        '+Se valida si existen valores cuotas a la fecha del rescate

        lrecInsValVI7000 = New eRemoteDB.Execute

        '+ Definición de store procedure InsValVI7000
        With lrecInsValVI7000
            .StoredProcedure = "InsValVI7000"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", nSurrReas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_total", nTotal_Surr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_payment", nPmtOrd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRetirement", dRetirement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_Avail", nSurr_Avail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSurrType", sSurrType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sApv", sApv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_exec", nType_exec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTotal", nTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("schkSurrTot", schkSurrTot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_dest", sClient_dest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIsCancelling", IIf(bIsCancelling, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeResc", nTypeResc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSaapv", nSaapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With
        'UPGRADE_NOTE: Object lrecInsValVI7000 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValVI7000 = Nothing

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            InsValVI7000 = .Confirm
        End With

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

InsValVI7000_Err:
        If Err.Number Then
            InsValVI7000 = "InsValVI7000: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '%InsValVI7000_K: Validaciones de la cabecera de forma VI7000
    Public Function InsValVI7000_k(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal sSurrType As String, ByVal nProponum As Double, ByVal sSche_Code As String, ByVal sCodisplori As String, ByVal sExecType As String, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nSurrReas As Integer, Optional ByVal sInsur As String = "") As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsProduct As eProduct.Product = New eProduct.Product
        Dim lclsPolicy As Policy
        Dim lclsCertificat As Certificat = New Certificat
        Dim lclsFund_values As Fund_values
        Dim lclsFund_value As Fund_value
        Dim lclsLife As Life
        Dim lrecinsCalPolAccBalance As eRemoteDB.Execute
        Dim lrecvalGuar_saving As eRemoteDB.Execute
        Dim lrecinsValSavigAccPol As eRemoteDB.Execute
        Dim lrecinsAmount_Pend As eRemoteDB.Execute
        Dim lobjGeneral As eGeneral.GeneralFunction
        Dim lclsWindows As eSecurity.Windows
        Dim lclsSecurity As eSecurity.SecurScheSurr
        Dim lclsMenues As eFunctions.Menues
        Dim lclsSurr_originss As Surr_originss
        Dim lclsPremium As Object
        Dim lintyear As Integer
        Dim lintMonth As Integer
        Dim lblnSurrTotal As Boolean
        Dim lblnError As Boolean
        Dim dDateLastPay As Date
        Dim nSaving_pct As Double
        Dim nTypeResc As Short
        Dim dLast_date_APV As Date

        On Error GoTo InsValVI7000_k_Err


        lobjErrors = New eFunctions.Errors

        lblnSurrTotal = sSurrType = "1"

        lclsPolicy = New ePolicy.Policy

        '+ Se valida que el campo "Ramo" tenga información
        If nBranch = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 70108)
            lblnError = True
        End If

        '+ Se valida que el campo "Producto" tenga información
        If nProduct = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 70109)
            lblnError = True
        End If

        If Not lblnError Then
            lclsProduct = New eProduct.Product
            With lclsProduct
                If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                    If .nProdClas <> 3 And .nProdClas <> 4 Then
                        lobjErrors.ErrorMessage(sCodispl, 70123)
                        lblnError = True
                    End If
                    If .sSurrenpi <> "1" And .sSurrenti <> "1" Then
                        lobjErrors.ErrorMessage(sCodispl, 70043)
                        'lblnError = True
                    Else
                        If (lblnSurrTotal And .sSurrenti <> "1") Or (Not lblnSurrTotal And .sSurrenpi <> "1") Then
                            lobjErrors.ErrorMessage(sCodispl, 3424)
                            lblnError = True
                        End If
                    End If

                    If sCodispl = "VI7000" Then
                        If .sApv = "1" Then
                            lobjErrors.ErrorMessage(sCodispl, 3406, , eFunctions.Errors.TextAlign.RigthAling, " : No debe ser apv")
                            lblnError = True
                        End If
                    Else
                        If (.sApv = "2" Or .sApv = "") Then
                            lobjErrors.ErrorMessage(sCodispl, 70177)
                            lblnError = True
                        End If
                    End If
                End If
            End With
        End If

        '+ Se valida que el campo "Póliza" tenga información
        If nPolicy = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 70038)
            lblnError = True
        ElseIf Not lblnError Then
            '+ Se valida que sea una póliza válida

            With lclsPolicy
                If Not .Find(CStr(2), nBranch, nProduct, nPolicy) Then
                    lobjErrors.ErrorMessage(sCodispl, 70039)
                    lblnError = True
                Else
                    If .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrInvalid) Then
                        lobjErrors.ErrorMessage(sCodispl, 70039)
                        lblnError = True
                    ElseIf .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrAnnuled) Then
                        lobjErrors.ErrorMessage(sCodispl, 70041)
                        lblnError = True
                    ElseIf .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Then
                        lobjErrors.ErrorMessage(sCodispl, 70042)
                        lblnError = True
                    End If
                End If
            End With
        End If

        '+ Se verifica que el certificado sea válido
        If nCertif = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 70044)
            lblnError = True
        ElseIf Not lblnError Then
            lclsCertificat = New Certificat
            With lclsCertificat
                If Not .Find(CStr(2), nBranch, nProduct, nPolicy, nCertif) Then
                    lobjErrors.ErrorMessage(sCodispl, 70047)
                    lblnError = True
                Else
                    If .sStatusva = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Then
                        lobjErrors.ErrorMessage(sCodispl, 70046)
                        lblnError = True
                    End If
                    If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                        lobjErrors.ErrorMessage(sCodispl, 70045)
                        lblnError = True
                    End If
                    '+ Se verifica que la póliza no tenga exención de cobertura
                    If .sExemption = "1" Then
                        lobjErrors.ErrorMessage(sCodispl, 38017)
                    End If
                End If

                '+ Se valida que la poliza/certificado no tenga propuestas especiales/de endoso pendientes
                If nProponum = eRemoteDB.Constants.intNull Then
                    If .existsModProposal(nBranch, nProduct, nPolicy, nCertif, True) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 60303, , eFunctions.Errors.TextAlign.RigthAling, "(" & .sMessage & ")")
                    End If
                End If
            End With
        End If

            If sCodisplori <> "CA767" And sExecType = "2" Then
                lclsMenues = New eFunctions.Menues
                '+ Rescata el módulo al cual pertenece la transacción
                lclsWindows = New eSecurity.Windows
                If lclsWindows.reaWindows("VI7000") Then
                    Call lclsMenues.ValActionLevel("VI7000", CShort("2"), sSche_Code, lclsWindows.nInqlevel, lclsWindows.nAmelevel)
                    If Not lclsMenues.mblnAmeAcces Then
                        'lclsWindows.nAmelevel < 5 Then
                        lobjErrors.ErrorMessage(sCodispl, 80097)
                    End If
                End If
            End If

            '+ Validacion de la Fecha de rescate
            dDateLastPay = getDateLastPay(nBranch, nProduct, nPolicy, nCertif)

            dLast_date_APV = lclsPolicy.GetLast_date_APV("VI7002", nBranch, nProduct)

            '+ si la opcion es definitivo y no se puede aprobar el rescate si la fecha del pui es anterior a la fecha del rescate
            If dEffecdate <> eRemoteDB.Constants.dtmNull And sExecType = "2" And dLast_date_APV <> dEffecdate And nSurrReas <> 5 Then
				lobjErrors.ErrorMessage(sCodispl, 50002, , eFunctions.Errors.TextAlign.RigthAling, dLast_date_APV & " no se puede aprobar un rescate con una fecha distinta")
            End If

            If dEffecdate = eRemoteDB.Constants.dtmNull Then
                lobjErrors.ErrorMessage(sCodispl, 3404)
				lblnError = True	
            ElseIf dEffecdate > Today Then
                lobjErrors.ErrorMessage(sCodispl, 7161)
				lblnError = True
            ElseIf dEffecdate < dDateLastPay Then
                lobjErrors.ErrorMessage(sCodispl, 5027, , eFunctions.Errors.TextAlign.RigthAling, "(Recaudacion fecha " & dDateLastPay & ")")
            ElseIf Month(dEffecdate) <> Month(Today) Then
                lobjErrors.ErrorMessage(sCodispl, 1115, , eFunctions.Errors.TextAlign.RigthAling, "(No corresponde a mes en curso)")
            ElseIf Not lblnError Then
                lclsLife = New Life
                '+Se lee la tabla de datos particulares para validar si el producto es Vida Universal
                If lclsLife.Find(CStr(2), nBranch, nProduct, nPolicy, nCertif, dEffecdate, True) Then
                    nSaving_pct = lclsLife.nSaving_pct
                Else
                    nSaving_pct = 0
                End If
                'UPGRADE_NOTE: Object lclsLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsLife = Nothing
                If nSaving_pct < 100 And nSurrReas <> 5 Then

                    '+Se leeen las condiciones de seguridad de acuerdo al perfil
                    lclsSecurity = New eSecurity.SecurScheSurr
                    If lclsSecurity.Find(sSche_Code, False) Then
                        nTypeResc = lclsSecurity.nTypeResc
                    Else
                        nTypeResc = 5
                    End If
                    'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsSecurity = Nothing
                    '+Se valida si existen valores cuotas a la fecha del rescate
                    lclsSurr_originss = New Surr_originss
                    If Not lclsSurr_originss.insDateVal_Surr("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTypeResc, nSaving_pct, sExecType) Then
                        lobjErrors.ErrorMessage(sCodispl, 71102)
                    End If
                    'UPGRADE_NOTE: Object lclsSurr_originss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsSurr_originss = Nothing

                End If
            End If
			If dEffecdate <> eRemoteDB.Constants.dtmNull And Not lblnError Then
            If dEffecdate <= lclsCertificat.dDate_Origi Then
                lobjErrors.ErrorMessage(sCodispl, 3405)
            End If

            '+ Se valida que la diferencia de años entre el efecto del certificado y la realización de la transacción
            lobjGeneral = New eGeneral.GeneralFunction
            Call lobjGeneral.getYearMonthDiff(lclsCertificat.dDate_Origi, dEffecdate, lintyear, lintMonth)
            'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjGeneral = Nothing

            If Not lblnSurrTotal Then
                '+ Se validan meses minimos de vigencia segun producto para Rescates Parciales
                If (lintyear * 12 + lintMonth) < lclsProduct.nQMMPSurr Then
                    lobjErrors.ErrorMessage(sCodispl, 60301)
                End If

                '+ Verifica que la cantidad de rescates en el mes sea el permitido
                If lclsProduct.nQmmsurr > 0 Then
                    If lclsCertificat.InsCalQSurr("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate) >= lclsProduct.nQmmsurr Then
                        lobjErrors.ErrorMessage(sCodispl, 60306)
                    End If
                End If

                '+ Verifica que la cantidad de rescates en el año sea el permitido
                If lclsProduct.nQmysurr > 0 Then
                    If lclsCertificat.InsCalQSurr("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate, "Y") >= lclsProduct.nQmysurr Then
                        lobjErrors.ErrorMessage(sCodispl, 60307)
                    End If
                End If
            Else
                '+ Se validan meses minimos de vigencia segun producto para Rescates Totales
                If (lintyear * 12 + lintMonth) < lclsProduct.nQmepsurr Then
                    lobjErrors.ErrorMessage(sCodispl, 60301)
                End If
            End If

            '+ Se valida que la póliza no tenga recibos pendientes por intereses por préstamo
            lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
            If lclsPremium.GetLoansInterest("2", nBranch, nProduct, nPolicy, nCertif) > 0 Then
                lobjErrors.ErrorMessage(sCodispl, 60465)
            End If
            'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsPremium = Nothing
        If nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull And nCertif <> eRemoteDB.Constants.intNull Then
            lrecinsCalPolAccBalance = New eRemoteDB.Execute
            With lrecinsCalPolAccBalance
                .StoredProcedure = "ins_cal_pol_acc_bal"
                .Parameters.Add("scertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, , , , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("norigin", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nresult", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 10, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nerror", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 10, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nbalance", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Run(False)

                If .Parameters("nbalance").Value < 0 Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70040)
                End If
            End With

            lrecinsValSavigAccPol = New eRemoteDB.Execute
            With lrecinsValSavigAccPol
                .StoredProcedure = "insValSavingAccPol"
                .Parameters.Add("scertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nResult", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 10, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                .Run(False)

                If .Parameters("nResult").Value > 0 Then
                    Call lobjErrors.ErrorMessage(sCodispl, 900017)
                End If
            End With

            lrecinsAmount_Pend = New eRemoteDB.Execute
            With lrecinsAmount_Pend
                .StoredProcedure = "reaAmount_Invest"
                .Parameters.Add("scertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("norigin", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("ninvested", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 10, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("namount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Run(False)

                If .Parameters("namount").Value > 0 Then
							If Not lblnSurrTotal Then
                    Call lobjErrors.ErrorMessage(sCodispl, 1000)
							Else
								Call lobjErrors.ErrorMessage(sCodispl, 1138)
                End If
						End If
            End With
        End If
			End If

        If nOffice = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 9120)
        End If
        If nOfficeAgen = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 55519)
        End If
        If nAgency = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1080)
        End If

        If nSurrReas <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 70054)
            '+Si es un Retiro Total de fondos para una póliza de APV,
            '+no debe existir saldo en la cuenta Depósito Convenido (Origen = 3)
        ElseIf nSurrReas = 1 And lclsProduct.sApv = "1" And lblnSurrTotal Then
            If nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull And nCertif <> eRemoteDB.Constants.intNull Then
                lrecinsCalPolAccBalance = New eRemoteDB.Execute
                With lrecinsCalPolAccBalance
                    .StoredProcedure = "INSVAL_DEP_CONV"
                    .Parameters.Add("scertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, , , , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("saction", sExecType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTypeResc", nTypeResc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nresult", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 10, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nerror", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 10, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nbalance", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Run(False)

                    If .Parameters("nbalance").Value > 0 Then
                        Call lobjErrors.ErrorMessage(sCodispl, 56208)
                    Else
                        If .Parameters("nerror").Value > 0 Then
                            Call lobjErrors.ErrorMessage(sCodispl, .Parameters("nerror").Value)
                        End If
                    End If
                End With
            End If
        End If

        '+Si la razón de rescate es "Compra Ahorro Garantizado"
        If nSurrReas = 5 Then
            lrecvalGuar_saving = New eRemoteDB.Execute
            With lrecvalGuar_saving
                .StoredProcedure = "REAPROP_GUAR_SAVING_POL"
                .Parameters.Add("scertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, , , , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Run(False)

                If .Parameters("nExist").Value = 0 Then
                    Call lobjErrors.ErrorMessage(sCodispl, 900019)
                End If
            End With
        End If
        '+Si no se indica Antecedentes Asegurabilidad y es un rescate preliminar
        '+ se verificva si puede pedir los antecedentes de asegurabilidad para mostrar mensaje de advertencia
        If sInsur <> "1" And sSurrType = "2" And nSurrReas <> 2 And nSurrReas <> 5 Then
            If Not lclsPolicy.insDisabledInsurRecord("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                lobjErrors.ErrorMessage(sCodispl, 80170)
            End If
        End If

        lobjErrors.ErrorMessage(sCodispl, 70037)

        InsValVI7000_k = lobjErrors.Confirm

InsValVI7000_k_Err:
        If Err.Number Then
            InsValVI7000_k = "InsValVI7000_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsWindows = Nothing
        'UPGRADE_NOTE: Object lclsMenues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsMenues = Nothing
        'UPGRADE_NOTE: Object lrecinsCalPolAccBalance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCalPolAccBalance = Nothing
        'UPGRADE_NOTE: Object lrecinsValSavigAccPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValSavigAccPol = Nothing
        'UPGRADE_NOTE: Object lrecvalGuar_saving may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecvalGuar_saving = Nothing
    End Function

    '% InsValCA032_K: Make the validation of the fields to be updated in the window ca032.
    '  (Reverse of policy renewal/amendment )
    '% InsValCA032_K: Realiza la validación de los campos a actualizar en la ventana ca032.
    '  (Reverso de renovación/modificación de Póliza)
    Public Function InsValCA032_K(ByVal sCodispl As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal sCompanyType As String = "") As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsValPolicySeq As ePolicy.ValPolicySeq
        Dim lclsPolicy_his = New Policy_his
        Dim lclsOut_moveme = New Out_moveme


        Dim lblnError As Boolean
        Dim llngError As Integer
        Dim lstrDescript As String = String.Empty

        On Error GoTo InsValCA032_K_Err
        lclsErrors = New eFunctions.Errors
        With lclsErrors
            '+ Validate the field Line of Business
            '+ Se valida el campo Ramo
            If nBranch = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 1022)
                lblnError = True
            End If

            '+ The Product field will be validated
            '+ Se va a validar el campo producto
            If nProduct = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 1014)
                lblnError = True
            End If

            '+ The Policy field will be validated.
            '+ Se va a validar el Campo de poliza
            If nPolicy = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 3003)
                lblnError = True
            Else
                If Not lblnError Then
                    lclsPolicy = New ePolicy.Policy
                    If Not lclsPolicy.ValExistPolicyRec(nBranch, nProduct, nPolicy, sCompanyType) Then
                        .ErrorMessage(sCodispl, 3001)
                        lblnError = True
                    Else
                        'If lclsPolicy.sPolitype = "2" Then
                        '.ErrorMessage sCodispl, 4112, , RigthAling, "(transacción no habilitada para colectivos)"
                        'Else
                        If lclsPolicy.sStatus_pol <> "3" And lclsPolicy.sStatus_pol <> "2" Then
                            If lclsPolicy.nNullcode > 0 Then
                                .ErrorMessage(sCodispl, 3098)
                                lblnError = True
                            End If
                        Else
                            .ErrorMessage(sCodispl, 3882)
                            lblnError = True
                        End If
                        mdtmIssuedat = lclsPolicy.dStartdate
                        'End If
                    End If
                End If
            End If

            '+ The Certificate field will be validated
            '+ Se va a validar el campo de Certificado
            If nCertif = eRemoteDB.Constants.intNull And nPolicy > 0 Then
                .ErrorMessage(sCodispl, 3006)
                lblnError = True

            ElseIf nCertif <> 0 Then
                If Not lblnError Then
                    lclsCertificat = New ePolicy.Certificat
                    If Not lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                        .ErrorMessage(sCodispl, 13908)
                        lblnError = True
                    Else
                        If lclsCertificat.sStatusva = "3" Or lclsCertificat.sStatusva = "2" Then
                            .ErrorMessage(sCodispl, 3883)
                            lblnError = True
                        ElseIf lclsCertificat.nNullcode > 0 Then
                            .ErrorMessage(sCodispl, 3099)
                            lblnError = True
                        End If
                    End If
                End If
            End If

            '+ It check if the policy has a movement of amendment or renewal to be reversed
            '+ Se valida que la póliza tenga algún movimiento de modificación o renovación a reversar
            If Not lblnError Then
                llngError = insvalPrevMov("2", nBranch, nProduct, nPolicy, nCertif)
                If llngError > 0 Then
                    lblnError = True
                    .ErrorMessage(sCodispl, llngError)
                End If
            End If

            If lclsPolicy_his.FindLastMovement("2", nBranch, nProduct, nPolicy, nCertif) Then
                Select Case lclsPolicy_his.nType
                    Case 11, 12, 54, 55, 61
                        lclsOut_moveme.Find_Receipt("2", nBranch, nProduct, nPolicy, nCertif, lclsPolicy_his.nMovement)
                        If lclsOut_moveme.nReceipt > 0 Then
                            .ErrorMessage(sCodispl, 3099)
                        End If
                End Select

                If lclsPolicy_his.nType = 11 Or lclsPolicy_his.nType = 12 Then
                    If lclsPolicy_his.nType_amend <> 1001 Then
                        .ErrorMessage(sCodispl, 900038)
                    End If
                End If
            End If
            '+ Se valida que la poliza no posea propuestas pendientes
            If Not lblnError Then
                lclsValPolicySeq = New ePolicy.ValPolicySeq
                If lclsValPolicySeq.ReaPolicy_QuotProp("2", nBranch, nProduct, nPolicy, nCertif, 0, Certificat.Stat_quot.esqPending, lstrDescript) Then
                    Call .ErrorMessage(sCodispl, 55649, , , "(" & lstrDescript & ")")
                End If
                'UPGRADE_NOTE: Object lclsValPolicySeq may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsValPolicySeq = Nothing
            End If
            InsValCA032_K = .Confirm
        End With

InsValCA032_K_Err:
        If Err.Number Then
            InsValCA032_K = "InsValCA032_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% InsValCA032: Make the validation of the fields to be updated in the window ca032.
    ' (Reverse of policy renewal/amendment )
    '% InsValCA032: Realiza la validación de los campos a actualizar en la ventana ca032.
    '  (Reverso de renovación/modificación de Póliza)
    Public Function InsValCA032(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransac As Integer) As String
        Dim lrecinsValCAO32 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
        Dim sCodispl As Object

        sCodispl = "CA032"

        On Error GoTo insValCA032_Err

        lrecinsValCAO32 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure
        '+
        With lrecinsValCAO32
            .StoredProcedure = "InsValCA032"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("sArrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    InsValCA032 = .Confirm()
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing
            End If
        End With

insValCA032_Err:
        If Err.Number Then
            InsValCA032 = "insValCA033: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lrecinsValCAO32 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValCAO32 = Nothing
        On Error GoTo 0
    End Function
    '% insPreCA032: Performed necessary routines to load default values of the page
    '% insPreCA032: Ejecuta las rutinas necesarias para la carga de la página
    Public Function insPreCA032(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean

        Dim lclsPolicy As Policy

        lclsPolicy = New Policy

        With lclsPolicy
            Call .Find("2", nBranch, nProduct, nPolicy)
            Me.sPolitype = .sPolitype
            Me.sColtimre = .sColtimre
            Me.sColinvot = .sColinvot
            Me.mdtmIssuedat = .dStartdate
        End With

        '+ Se busca el intermediario de la póliza
        If lclsPolicy.nIntermed <> eRemoteDB.Constants.intNull Then
            Me.nIntermed = lclsPolicy.nIntermed
            Me.sInterName = lclsPolicy.FindName(CStr(Me.nIntermed), False)
        Else
            Me.nIntermed = eRemoteDB.Constants.intNull
            Me.sInterName = String.Empty
        End If

        '+ Se busca el cliente de la póliza
        If lclsPolicy.sClient <> String.Empty Then
            Me.sClient = lclsPolicy.sClient
            Me.sClientName = lclsPolicy.FindName(Me.sClient, True)
        Else
            Me.sClient = String.Empty
            Me.sClientName = String.Empty
        End If

        '+ Se buscan los datos de la transacción a reversar
        Call insvalPrevMov("2", nBranch, nProduct, nPolicy, nCertif)
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
    End Function

    '% InsPostCA032: This function is in charge of validate all the introduced data in the form.
    '% InsPostCA032: Esta función se encaga de validar todos los datos introducidos en la forma
    Public Function InsPostCA032(ByVal sCodispl As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nNullReceipt As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nNullOutMov As Integer = 0, Optional ByVal sReverCertif As String = "", Optional ByVal nNullQuotProp As Integer = 0) As Boolean
        Dim lclsCertificat As ePolicy.Certificat
        lclsCertificat = New ePolicy.Certificat
        InsPostCA032 = lclsCertificat.ReverseRenModPol("2", nBranch, nProduct, nPolicy, IIf(sReverCertif = "1", eRemoteDB.Constants.intNull, nCertif), nNullReceipt, nUsercode, nNullOutMov, nNullQuotProp, "1")
InsPostCA032_err:
        If Err.Number Then
            InsPostCA032 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function

    '% insPostCA033: Esta función se encarga de validar todos los datos introducidos en la forma
    Public Function insPostCA033(ByVal nAction As Integer, ByVal sCodispl As String, ByVal sCodisplori As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal dNulldate As Date = #12:00:00 AM#, Optional ByVal nNullcode As Integer = 0, Optional ByVal nTransacion As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nTyp_rec As Integer = 0, Optional ByVal sOptExecute As String = "", Optional ByVal sChkNullRequest As String = "", Optional ByVal sNull_rec As String = "", Optional ByVal nOperat As Integer = 0, Optional ByVal nNotenum As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal nAgency As Integer = 0, Optional ByVal sReturn_ind As String = "", Optional ByVal nReturn_Rat As Integer = 0, Optional ByVal sProcess_Num As String = "") As Boolean
        Dim lrecAnnulment As eRemoteDB.Execute

        On Error GoTo insPostCA033Err
        lrecAnnulment = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.inspostca033'
        With lrecAnnulment
            .StoredProcedure = "inspostca033"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodisplOri", sCodisplori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransacion", nTransacion, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_rec", nTyp_rec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptExecute", sOptExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChkNullRequest", sChkNullRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNull_rec", sNull_rec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOperat", nOperat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReturn_ind", sReturn_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReturn_Rat", nReturn_Rat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCA033 = .Run(False)
        End With

insPostCA033Err:
        If Err.Number Then
            insPostCA033 = CBool("CA033 " & Err.Description)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecAnnulment may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecAnnulment = Nothing
    End Function

    '% insPostCA033_k: Esta función se encaga de validar todos los datos introducidos en la forma (Parte Header)
    Public Function insPostCA033_k() As Boolean
        insPostCA033_k = True
    End Function

    '% insValCA033_k: Realiza la validación de los campos a actualizar en la ventana CA033.
    '  (Anulación de Póliza y/o certificado)
    Public Function insValCA033_k(ByVal sCodispl As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal sCompanyType As String = "", Optional ByVal nOffice As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal nProponum As Double = 0) As String
        Dim lobjValues As eFunctions.Values
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lobjExistProp As ValPolicySeq
        Dim lobjClaim As Object
        Dim lstrDescript As String = ""

        On Error GoTo insValCA033_k_Err

        lobjValues = New eFunctions.Values
        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lobjExistProp = New ValPolicySeq
        lobjClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")

        '+ Se valida el campo Ramo
        If nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1022)
        End If

        '+ Se va a validar el campo producto
        If nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        Else
            lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If Not lobjValues.IsValid("tabProdMaster1", CStr(nProduct), True) Then
                Call lobjErrors.ErrorMessage(sCodispl, 1011)
       		End If
        End If

        '+ Se va a validar el Campo de poliza
        If nPolicy = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3003)
        Else
            If Not (nBranch = eRemoteDB.Constants.intNull) Then
                With lclsPolicy
                    If Not .ValExistPolicyRec(nBranch, nProduct, nPolicy, sCompanyType) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3001)
                    Else
                        If (Trim(.sStatus_pol) <> "3") And (Trim(.sStatus_pol) <> "2") Then
                            If Trim(.sStatus_pol) = CStr(Policy.TypeStatus_Pol.cstrRansom) Then
                                Call lobjErrors.ErrorMessage("CA034", 60486)
                            Else
                                If (Not (.nNullcode = eRemoteDB.Constants.intNull)) And Trim(CStr(.nNullcode)) <> "0" Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3098)
                                End If
                            End If
                        Else
                            Call lobjErrors.ErrorMessage(sCodispl, 3882)
                        End If
                    End If
                End With
            End If
        End If

        '+ Se va a validar el campo de Certificado
        If (Not nCertif = eRemoteDB.Constants.intNull) Then
            If Not (nBranch = eRemoteDB.Constants.intNull) And Not (nProduct = eRemoteDB.Constants.intNull) And Not (nPolicy = eRemoteDB.Constants.intNull) Then
                With lclsCertificat
                    If Not .FindCertificatToNull("2", nBranch, nProduct, nPolicy, nCertif, eRemoteDB.Constants.intNull) Then
                        If nCertif <> 0 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 13908)
                        End If
                    Else
                        If nCertif <> 0 Then
                            If .sStatusva = "3" Or .sStatusva = "2" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3883)
                            ElseIf .nNullcode <> eRemoteDB.Constants.intNull Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3099)
                            End If
                        End If
                    End If
                End With
            End If
        End If
        If nOffice = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CA033", 9120)
        End If
        If nOfficeAgen = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CA033", 55519)
        End If
        If nAgency = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CA033", 1080)
        End If

        '+ se valida que la poliza no posea otras propuestas pendientes
        If lobjExistProp.ReaPolicy_QuotProp("2", nBranch, nProduct, nPolicy, nCertif, nProponum, Certificat.Stat_quot.esqPending, lstrDescript) Then
            Call lobjErrors.ErrorMessage("CA033", 55649, , , "(" & lstrDescript & ")")
        End If
        '+ se valida si poliza/certificado posee una declaración de siniestro
        If lobjClaim.FindNumberOfClaims("2", nBranch, nProduct, nPolicy, nCertif) > 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 55778, , eFunctions.Errors.TextAlign.RigthAling, lobjClaim.sClaims)
        End If

        insValCA033_k = lobjErrors.Confirm

insValCA033_k_Err:
        If Err.Number Then
            insValCA033_k = insValCA033_k & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lobjClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjClaim = Nothing
        'UPGRADE_NOTE: Object lobjExistProp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjExistProp = Nothing
    End Function
    '%insValCA034_k: Esta función se encarga de validar los datos introducidos en la cabecera de la
    '%forma.
    Public Function insValCA034_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nProponum As Double, Optional ByVal nServ_order As Double = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0, Optional ByVal nAgency As Integer = 0) As String

        '- Se define el objeto para el manejo tanto de la clase  Certificat
        '- como de la clase Policy
        Dim lclsPolicy As Policy
        Dim lclsCertificat As Certificat
        Dim lclsRoles As Roles
        Dim lobjExistProp As ValPolicySeq
        Dim lobjErrors As eFunctions.Errors
        Dim lobjValues As eFunctions.Values
        Dim lobjProf_ord As Object
        Dim lstrDescript As String = String.Empty
        Dim lstrValReq As String = String.Empty
        Dim lobjLife As ePolicy.Life

        lclsPolicy = New Policy
        lclsCertificat = New Certificat
        lobjErrors = New eFunctions.Errors
        lobjValues = New eFunctions.Values
        lobjExistProp = New ValPolicySeq
        lobjProf_ord = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")
        lclsRoles = New Roles


        On Error GoTo insValCA034_k_Err

        '+Se valida el campo de Ramos
        If nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CA034", 1022)
        End If

        '+Se va a validar el campo de product
        If nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CA034", 1014)
        End If

        '+Se va a validar el Campo de poliza
        With lclsPolicy
            If nPolicy = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage("CA034", 3003)
            ElseIf nCertif = 0 Or nCertif = eRemoteDB.Constants.intNull Then

                If nBranch <> eRemoteDB.Constants.intNull Then
                    If Not .FindPolicyOfficeName("2", nBranch, nProduct, nPolicy, "1") Then
                        Call lobjErrors.ErrorMessage("CA034", 3001)
                    Else
                        If .sPolitype = "1" Then
                            nCertif = 0
                        End If

                        If (Trim(.sStatus_pol) <> CStr(Policy.TypeStatus_Pol.cstrInvalid)) And (Trim(.sStatus_pol) <> CStr(Policy.TypeStatus_Pol.cstrIncomplete)) Then
                            If Trim(.sStatus_pol) = CStr(Policy.TypeStatus_Pol.cstrRansom) Then
                                Call lobjErrors.ErrorMessage("CA034", 55872)
                            Else
                                If (.nNullcode = eRemoteDB.Constants.intNull) Then
                                    If .sPolitype = "1" Or (.sPolitype <> "1" And nCertif = 0) Then
                                        Call lobjErrors.ErrorMessage("CA034", 3102)
                                    End If
                                Else
                                    If (.nNullcode <> 0) Then
                                        If .sPolitype = "1" Then
                                            nCertif = 0
                                        End If
                                    ElseIf (.sPolitype = "1" Or (.sPolitype <> "1" And nCertif = 0)) Then
                                        Call lobjErrors.ErrorMessage("CA034", 3102)
                                    End If
                                End If
                            End If
                        Else
                            If nCertif = 0 Then
                                Call lobjErrors.ErrorMessage("CA034", 3882)
                            End If
                        End If
                    End If

                    If nCertif = eRemoteDB.Constants.intNull Then
                        nCertif = 0
                    End If
                End If
            End If
        End With

        '+Se va a validar el campo de Certificado
        With lclsCertificat
            If nCertif = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage("CA034", 3006)
            ElseIf nCertif <> 0 Then
                If nBranch <> eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull Then
                    If Not .FindCertificatToNull("2", nBranch, nProduct, nPolicy, nCertif, eRemoteDB.Constants.intNull) Then
                        Call lobjErrors.ErrorMessage("CA034", 13908)
                    Else
                        If .sStatusva = "2" Or .sStatusva = "3" Then
                            Call lobjErrors.ErrorMessage("CA034", 3883)
                        Else
                            If (.nNullcode = eRemoteDB.Constants.intNull) Then
                                Call lobjErrors.ErrorMessage("CA034", 3103)
                            Else
                                Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)

                                Call lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, 2, "", lclsCertificat.dStartdate)

                                lobjLife = New ePolicy.Life

                                Call lobjLife.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsCertificat.dStartdate)

                                If lobjLife.Val_nRepInsured(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsRoles.sClient, lclsCertificat.dStartdate, lobjLife.sCreditnum) Then
                                    If lobjLife.nExists = 1 Then
                                        Call lobjErrors.ErrorMessage(sCodispl, 750100, , , "(" & lobjLife.nCert_er & ")")
                                    ElseIf lobjLife.nExists = 2 Then
                                        Call lobjErrors.ErrorMessage(sCodispl, 56023, , , "(" & lobjLife.nCert_er & ")")
                                    ElseIf lobjLife.nExists = 3 Then
                                        Call lobjErrors.ErrorMessage(sCodispl, 750099, , , "(" & lobjLife.nCert_er & ")")
                                    ElseIf lobjLife.nExists = 4 Then
                                        Call lobjErrors.ErrorMessage(sCodispl, 56027, , , "(" & lobjLife.nCert_er & ")")
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End With

        '+ Se valida el número de la orden de servicio
        If nServ_order <> eRemoteDB.Constants.intNull Then
            If lobjProf_ord.Find_nServ(nServ_order) Then
                If lobjProf_ord.nStatus_ord <> 3 And lobjProf_ord.nStatus_ord <> 4 Then
                    Call lobjErrors.ErrorMessage("CA034", 55704)
                End If
            Else
                Call lobjErrors.ErrorMessage("CA034", 4056)
            End If
        End If

        If nOffice = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CA034", 9120)
        End If
        If nOfficeAgen = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CA034", 55519)
        End If
        If nAgency = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CA034", 1080)
        End If
        '+ se valida que la poliza no posea otras propuestas pendientes
        If lobjExistProp.ReaPolicy_QuotProp(sCertype, nBranch, nProduct, nPolicy, nCertif, nProponum, Certificat.Stat_quot.esqPending, lstrDescript) Then
            Call lobjErrors.ErrorMessage("CA034", 55649, , , "(" & lstrDescript & ")")
        End If

        insValCA034_K = lobjErrors.Confirm

insValCA034_k_Err:
        If Err.Number Then
            insValCA034_K = "insValCA034_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lobjExistProp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjExistProp = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lobjLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjLife = Nothing
    End Function

    '% insValCA033: Realiza la validación de los campos a actualizar en la ventana CA033.
    '  (Anulación de Póliza y/o certificado)
    Public Function insValCA033(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal sOptDev As String = "", Optional ByVal sOptReceipt As String = "", Optional ByVal nFixPercent As Double = 0, Optional ByVal dPolicyNullDate As Date = #12:00:00 AM#, Optional ByVal nNullcode As Integer = 0, Optional ByVal nUsercode As Integer = 0 ,  Optional ByVal sNull_rec As String = "") As String
        Dim lrecinsValCAO33 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        On Error GoTo insValCA033_Err

        lrecinsValCAO33 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure
        '+
        With lrecinsValCAO33
            .StoredProcedure = "InsValCA033"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptDev", sOptDev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptReceipt", sOptReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFixPercent", nFixPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPolicyNullDate", dPolicyNullDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("sArrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    insValCA033 = .Confirm()
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing
            End If
        End With

insValCA033_Err:
        If Err.Number Then
            insValCA033 = "insValCA033: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lrecinsValCAO33 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValCAO33 = Nothing
        On Error GoTo 0
    End Function
    '%insValCA034: Esta función se encarga de validar los datos introducidos en la zona de detalle para
    Public Function insValCA034(ByVal sCodispl As String, ByVal sAction As String, ByVal nExeMode As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal RescRequest As String, Optional ByVal nLetterNum As Integer = 0, Optional ByVal nNotenum As Integer = 0, Optional ByVal sCodisplori As String = "") As String
        '- Se define el objeto para el manejo tanto de la clase  Certificat
        '- como de la clase Policy
        Dim lobjPoliCert As Object
        '- Se usa enlace tardio en eClaim para evitar referencia circular con ePolicy
        Dim lobjClaimBenefs As Object
        Dim lclsOpt_ledger As eLedge.Opt_ledger
        Dim lclsCtrol_Date As eGeneral.Ctrol_date
        Dim lobjProduct As eProduct.Product
        Dim lobjPolicy As ePolicy.Policy

        Dim lclsNull_condi As Null_condi
        Dim lobjErrors As eFunctions.Errors
        Dim lblnOk As Boolean
        Dim ldDate As Date
        Dim lintRehabPeriod As Integer
        On Error GoTo insValCA034_Err
        lobjProduct = New eProduct.Product
        lobjPolicy = New ePolicy.Policy
        lclsNull_condi = New Null_condi
        lobjErrors = New eFunctions.Errors

        '+ Busqueda del producto relacionado con la poliza para traer el campo nRehabperiod
        Call lobjProduct.Find(nBranch, nProduct, dEffecdate)

        lblnOk = False

        If nBranch <> eRemoteDB.Constants.intNull Then
            If dEffecdate = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 7114)
            Else
                Call lobjPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
                If lobjPolicy.dNulldate <> eRemoteDB.Constants.dtmNull Then
                    If dEffecdate <> lobjPolicy.dNulldate Then
                        Call lobjErrors.ErrorMessage(sCodispl, 55129)
                    End If
                End If

                If lobjProduct.sReinst = "2" Then
                    Call lobjErrors.ErrorMessage(sCodispl, 55502)
                Else
                    '+ Si es preliminar y la rutina de validación no permite rehabilitación y nose indico
                    '+ Generar propuesta se debe enviar una validación
                    'If nExeMode <> 2 And RescRequest <> "1" Then
                    '    If lobjProduct.sRoutaut_r <> String.Empty Then
                    '        If Not insRoutinerehabilitate(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lobjProduct.sRoutaut_r) Then
                    '            Call lobjErrors.ErrorMessage(sCodispl, 56183)
                    '        End If
                    '    End If
                    '    lobjPoliCert = New ePolicy.Certificat
                    '    Call lobjPoliCert.FindCertificatToNull(sCertype, nBranch, nProduct, nPolicy, nCertif, eRemoteDB.Constants.intNull)
                    '    If lclsNull_condi.Find(nBranch, nProduct, lobjPoliCert.nNullcode, dEffecdate) Then
                    '        If lclsNull_condi.sNotrehab = "1" Then
                    '            Call lobjErrors.ErrorMessage(sCodispl, 56183)
                    '        End If
                    '    Else
                    '        Call lobjErrors.ErrorMessage(sCodispl, 56183)
                    '    End If
                    '    'UPGRADE_NOTE: Object lobjPoliCert may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    '    lobjPoliCert = Nothing
                    'End If

                    If lobjProduct.sRoutaut_r <> String.Empty And nExeMode = 2 And sCodisplori <> "CA767" Then
                        If Not insRoutinerehabilitate(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lobjProduct.sRoutaut_r) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 55887)
                        Else
                            If insValidateLifeDocu(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                                Call lobjErrors.ErrorMessage(sCodispl, 56033)
                            End If
                        End If
                    Else
                        '+ Segun HAD-755.
                        If CStr(lobjProduct.sBrancht) = "1" And nExeMode = 1 Then
                            If nNotenum = 0 Then
                                Call lobjErrors.ErrorMessage(sCodispl, 60538)
                            End If
                        End If
                    End If
                End If

                If nCertif = 0 Then
                    lobjPoliCert = New ePolicy.Policy
                    If lobjPoliCert.FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, "1") Then
                        lblnOk = True
                    End If
                Else
                    lobjPoliCert = New ePolicy.Certificat
                    If lobjPoliCert.FindCertificatToNull(sCertype, nBranch, nProduct, nPolicy, nCertif, eRemoteDB.Constants.intNull) Then
                        lblnOk = True
                    End If
                End If

                If lblnOk Then
                    '+ Se valida que condición de anulación pueda ser rehabilitada
                    '+ si la operación es definitiva
                    If nExeMode = 2 And sCodisplori <> "CA767" Then
                        If lclsNull_condi.Find(nBranch, nProduct, lobjPoliCert.nNullcode, dEffecdate) Then
                            If lclsNull_condi.sNotrehab = "1" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 60539)
                            End If
                        Else
                            Call lobjErrors.ErrorMessage(sCodispl, 60539)
                        End If
                    End If

                    If (lobjPoliCert.dChangdat = eRemoteDB.Constants.dtmNull) Then
                        ldDate = lobjPoliCert.dDate_Origi
                    Else
                        ldDate = lobjPoliCert.dChangdat
                    End If

                    '+ Se valida que la fecha no sea nula
                    If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                        '+ Se valida que la fecha de rehabilitación  sea posterior a la fecha
                        '+ de anulación de la póliza o certificado
                        If dEffecdate < lobjPoliCert.dNulldate Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3104)
                        End If
                    Else
                        Call lobjErrors.ErrorMessage(sCodispl, 3825)
                    End If

                    '+ Verifica que la fecha de rehabilitación sea anterior o igual a la fecha de anulación
                    '+ mas los dias de gracia.
                    If nExeMode = 1 Then
                        lintRehabPeriod = lobjProduct.nRehabperiod ' Dias para la rehabilitacion no automatica
                    Else
                        lintRehabPeriod = lobjProduct.nRehabperiod_aut ' Dias para la rehabilitacion automatica
                    End If

                    If lintRehabPeriod = eRemoteDB.Constants.intNull Then lintRehabPeriod = 0
                    If dEffecdate > DateAdd(Microsoft.VisualBasic.DateInterval.Day, lintRehabPeriod, lobjPoliCert.dNulldate) Then
                        If nCertif = 0 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 55615)
                        End If
                    End If

                    '+ Se valida que la fecha de anulacion debe ser posterior al periodo contable en vigor
                    lclsOpt_ledger = New eLedge.Opt_ledger
                    If lclsOpt_ledger.Find Then
                        If dEffecdate < System.Date.FromOADate(lclsOpt_ledger.nInitDay) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 1006)
                        End If
                        '+ Se valida que sea posterior al ultimo proceso de asientos automáticos
                        lclsCtrol_Date = New eGeneral.Ctrol_date
                        If lclsCtrol_Date.Find(1) Then
                            If lclsCtrol_Date.dEffecdate > Today Then
                                Call lobjErrors.ErrorMessage(sCodispl, 1008)
                            End If
                        Else
                            Call lobjErrors.ErrorMessage(sCodispl, 1008)
                        End If
                    End If

                    '+ Se verifica que asegurados no tengan algun siniestro pendiente
                    lobjClaimBenefs = eRemoteDB.NetHelper.CreateClassInstance("eClaim.ClaimBenefs")
                    If lobjClaimBenefs.FindClientOutStandClaim(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then

                        Call lobjErrors.ErrorMessage(sCodispl, 55144)
                    End If

                    '+ Se verifica si algun asegurado tiene documentos solicitados con notas asociadas
                    If insValSpecialEvalCondition(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 60212)
                    End If
                    '+ certificado encontrado
                End If
                '+ fecha valida
            End If
            '+ branch <> null
        End If

        insValCA034 = lobjErrors.Confirm

insValCA034_Err:
        If Err.Number Then
            insValCA034 = "insValCA034: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjClaimBenefs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjClaimBenefs = Nothing
        'UPGRADE_NOTE: Object lobjPoliCert may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjPoliCert = Nothing
        'UPGRADE_NOTE: Object lclsOpt_ledger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsOpt_ledger = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCtrol_Date = Nothing
        'UPGRADE_NOTE: Object lobjProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjProduct = Nothing
        'UPGRADE_NOTE: Object lclsNull_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsNull_condi = Nothing
    End Function

    '%insPostCA034: Esta función se encarga realizar la transacción
    Public Function insPostCA034(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nExeMode As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sNullDevRec As String, ByVal sNullReceipt As String, ByVal optExecute As String, ByVal RescRequest As String, ByVal nOperat As Integer, ByVal nPay_day As Integer, ByVal nNotenum As Double, Optional ByVal nServ_order As Double = 0, Optional ByVal nLetterNum As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal optProcess As Short = 0, Optional ByVal sProcess_Num As String = "") As Boolean
        Dim lrecinsPostca034 As eRemoteDB.Execute
        On Error GoTo insPostca034_Err

        lrecinsPostca034 = New eRemoteDB.Execute
        '+
        '+ 'Definición de store procedure insPostca034 al 09-06-2004 12:34:16
        '+
        With lrecinsPostca034
            .StoredProcedure = "insPostca034"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNulldevrec", sNullDevRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNullreceipt", sNullReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptExecute", optExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRescrequest", RescRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOperat", nOperat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDay_pay", nPay_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nProponum", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdclas", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextreceipt", Me.dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcess", optProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCA034 = .Run(False)
            If insPostCA034 Then
                Me.nProposal = lrecinsPostca034.Parameters("nProponum").Value
                Me.sBrancht = Trim(lrecinsPostca034.Parameters("sBrancht").Value)
                Me.nProdClas = lrecinsPostca034.Parameters("nProdClas").Value
                Me.dNextReceip = lrecinsPostca034.Parameters("dNextreceipt").Value
            End If

        End With

insPostca034_Err:
        If Err.Number Then
            insPostCA034 = False
        End If
        'UPGRADE_NOTE: Object lrecinsPostca034 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostca034 = Nothing
        On Error GoTo 0
    End Function

    '%InsValCA888: Realiza la validación de los campos a actualizar en la ventana CA888
    Public Function insValCA888_k(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal sCompanyType As String = "", Optional ByVal sPoltype As String = "") As String

        Dim lobjErrors As Object
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lblnErrors As Boolean
        Dim lclsPolicy_his As ePolicy.Policy_his

        On Error GoTo insValCA888_k_Err

        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsPolicy_his = New ePolicy.Policy_his

        lblnErrors = False
        '+ Se valida el campo Ramo
        If nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1022)
            lblnErrors = True
        End If
        '+ Se valida el campo Producto
        If nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
            lblnErrors = True
        End If

        '+ Se valida el campo Póliza
        If nPolicy = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3003)
            lblnErrors = True
        Else
            If (nBranch <> eRemoteDB.Constants.intNull) And (nProduct <> eRemoteDB.Constants.intNull) Then
                If Not lclsPolicy.FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, sCompanyType) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3001)
                    lblnErrors = True
                Else
                    If lclsPolicy.dChangdat <> eRemoteDB.Constants.dtmNull Or CStr(lclsPolicy.dChangdat) <> String.Empty Then
                        lclsCertificat.dChangdat = lclsPolicy.dChangdat
                    Else
                        lclsCertificat.dChangdat = lclsPolicy.dStartdate
                    End If
                    If lclsPolicy.nUser_amend = eRemoteDB.Constants.intNull Then
                        lclsCertificat.nUsercode = 0
                    Else
                        lclsCertificat.nUsercode = lclsPolicy.nUser_amend
                    End If
                    Me.sPolitype = lclsPolicy.sPolitype
                    lclsCertificat.sPolitype = lclsPolicy.sPolitype
                    lclsCertificat.dEffecdate = lclsPolicy.dStartdate
                    If Trim(lclsPolicy.sPolitype) = "1" Then
                        nCertif = 0
                    End If
                    If lclsPolicy.nUser_amend = eRemoteDB.Constants.intNull Or lclsPolicy.sStatus_pol <> "3" Then
                        If nCertif = eRemoteDB.Constants.intNull Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3004)
                            lblnErrors = True
                        End If
                    End If
                End If
            End If
        End If

        '+ Se valida el campo Certificado
        If Not lblnErrors Then
            If nCertif = eRemoteDB.Constants.intNull And Trim(lclsPolicy.sPolitype) <> "1" Then
                Call lobjErrors.ErrorMessage(sCodispl, 3006)
            Else
                If (nBranch <> eRemoteDB.Constants.intNull) And (nProduct <> eRemoteDB.Constants.intNull) And (nPolicy <> eRemoteDB.Constants.intNull) And (nCertif <> eRemoteDB.Constants.intNull) Then
                    If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 13908)
                    Else
                        If lclsCertificat.dChangdat <> eRemoteDB.Constants.dtmNull Or CStr(lclsCertificat.dChangdat) <> String.Empty Then
                            lclsPolicy.dChangdat = lclsCertificat.dChangdat
                        Else
                            lclsPolicy.dChangdat = lclsCertificat.dStartdate
                        End If
                        If lclsCertificat.nUser_amend = eRemoteDB.Constants.intNull Or lclsCertificat.sStatusva <> "3" Then
                            If lclsPolicy_his.FindLastMovement(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                                If lclsPolicy_his.nType_Hist <> 11 And lclsPolicy_his.nType_Hist <> 12 Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3007)
                                    lblnErrors = True
                                End If
                            Else
                                Call lobjErrors.ErrorMessage(sCodispl, 3007)
                                lblnErrors = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

        '+ Se valida el campo Usuario
        If nUsercode = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1352)
        Else
            If nUsercode <> lclsCertificat.nUser_amend And lclsCertificat.nUser_amend <> eRemoteDB.Constants.intNull And lclsCertificat.nUser_amend <> 0 And Not lblnErrors Then
                Call lobjErrors.ErrorMessage(sCodispl, 3690)
            End If
        End If

        insValCA888_k = lobjErrors.Confirm

insValCA888_k_Err:
        If Err.Number Then
            insValCA888_k = "insValCA888_k:" & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function


    '% insPostCA888: Esta función realiza la reinstalación de póliza por modificación incompleta
    Public Function insPostCA888_k(ByVal sAction As String, Optional ByVal sCodispl As String = "", Optional ByVal sPolitype As String = "", Optional ByVal sCertype As String = "", Optional ByVal nProduct As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean
        Dim lobjValues As eFunctions.Values
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPremium As eCollection.Premiums

        On Error GoTo insPostCA888_k_Err

        lobjValues = New eFunctions.Values
        lclsCertificat = New ePolicy.Certificat

        insPostCA888_k = True

        With lclsCertificat
            If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                .nUsercode = nUsercode
                .sPolitype = sPolitype
                Select Case sAction
                    '+ Si la opción seleccionada es Modificar
                    Case "Update"
                        '+Esta transaccion sólo se llama por si no existiera una historia de
                        '+endoso, es decir, que el certificado estuviera en captura incompleta
                        '+por alguna condicion no controlada.
                        '+De todas maneras,al reversar también se deja el certificado activo
                        Call .updPolicyCA888()
                        Call .insReverRenModPol(, , , , , , , , 1)
                End Select
            Else
                insPostCA888_k = False
            End If
        End With

        lclsPremium = New eCollection.Premiums
        lclsPremium.insUpdpremium_stat(sCertype, nBranch, nProduct, nPolicy, nCertif)

insPostCA888_k_Err:
        If Err.Number Then
            insPostCA888_k = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPremium = Nothing
    End Function

    '%insValCA038_k: Esta función se encarga de validar los datos introducidos en la zona de detalle de
    '%la forma.
    Public Function insValCA038_k(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal dExpirDate As Date, ByVal dNextReceip As Date) As String
        '- Se define el objeto para el manejo tanto de la clase  Certificat
        '- como de la clase Policy
        Dim lclsObject As ePolicy.Certificat
        Dim lobjErrors As eFunctions.Errors
        Dim lclsQuery As eRemoteDB.Query
        Dim lclsPolicy As ePolicy.Policy
        lobjErrors = New eFunctions.Errors
        lclsQuery = New eRemoteDB.Query

        lclsObject = New ePolicy.Certificat
        lclsPolicy = New ePolicy.Policy

        On Error GoTo insValCA038_k_Err

        '+Validación del Campo Ramo
        If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
            '+El campo Ramo debe estar lleno
            Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, "Ramo:")
        End If

        '+Validación del Campo Producto
        If nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '+Validación del Campo Póliza
        If nPolicy = eRemoteDB.Constants.intNull Then
            '+El campo Póliza debe estar lleno
            With lclsQuery
                If .OpenQuery("Table563", "sDescript", "nCodigint=4") Then
                    Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, .FieldToClass("sDescript") & ": ")
                    .CloseQuery()
                Else
                    Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, "Póliza: ")
                End If
            End With
        ElseIf nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And (nCertif = 0 Or nCertif = eRemoteDB.Constants.intNull) Then
            '+SCertype = 2, ya que se trata de una Póliza
            With lclsObject
                If .Find_CA038("2", nBranch, nProduct, nPolicy, 0, dExpirDate) Then
                    '+Póliza no puede estar anulada
                    If (.dNulldate <> eRemoteDB.Constants.dtmNull) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3098)
                        '+Póliza no puede estar suspendida
                    ElseIf (.nSuspCount > 0) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3101)
                    End If
                    '+El cambio de Fecha de Renovación es permitido sólo a pólizas que vayan a ser renovadas. Las pólizas
                    '+sin renovación deberán ser tratadas con el proceso de anulación de pólizas.
                    If .sPolitype <> "1" Then
                        If .sColtimre = "3" Or .sColtimre = String.Empty Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3892)
                        End If
                    End If
                Else
                    '+Póliza debe estar registrado en el archivo Policy
                    Call lobjErrors.ErrorMessage(sCodispl, 3001)
                End If
            End With
        End If

        '+Validación del campo Certif
        If nCertif = eRemoteDB.Constants.intNull Then
            With lclsQuery
                If .OpenQuery("Table563", "sDescript", "nCodigint=213") Then
                    Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, .FieldToClass("sDescript") & ": ")
                    .CloseQuery()
                Else
                    Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, "Certificado: ")
                End If
            End With
        Else
            If nCertif <> 0 Then
                '+SCertype = 2, ya que se trata de un Certificado
                With lclsObject
                    If .Find_CA038("2", nBranch, nProduct, nPolicy, nCertif, dExpirDate) Then
                        '+Certificado no puede estar anulado
                        If (.dNulldate <> eRemoteDB.Constants.dtmNull) Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3099)
                            '+Certificado no puede estar suspendido
                        ElseIf .nSuspCount > 0 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3881)
                        End If
                        '+El certificado no debe tener siniestros declarados cuya fecha de ocurrencia sea
                        '+posterior a la nueva fecha de renovación
                        If .nDeclaredClaims > 0 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3891)
                        End If
                        '+La póliza debe tener tipo de renovación independiente.
                        '                    If .sColtimre <> "2" Then
                        '                        Call lobjErrors.ErrorMessage(sCodispl, 3654)
                        '                    End If
                    Else
                        '+Certificado debe estar registrado en el archivo Certificat
                        Call lobjErrors.ErrorMessage(sCodispl, 3010)
                    End If
                End With
            End If
        End If

        With lclsObject
            '+Validación del campo Fecha de Renovación
            If dExpirDate = eRemoteDB.Constants.dtmNull Then
                '+El campo Fecha de Renovación debe estar lleno
                With lclsQuery
                    If .OpenQuery("Table563", "sDescript", "nCodigint=214") Then
                        Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, .FieldToClass("sDescript") & ": ")
                        .CloseQuery()
                    Else
                        Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de renovación: ")
                    End If
                End With
            Else
                If nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0 And nPolicy <> eRemoteDB.Constants.intNull And nCertif <> eRemoteDB.Constants.intNull Then
                    '+El campo Fecha de Renovación debe corresponder a un fecha
                    If .dStartdate <> eRemoteDB.Constants.dtmNull Then
                        If dExpirDate <= .dStartdate Then
                            '+El campo Fecha de Renovación debe ser mayor que la fecha de efecto
                            Call lobjErrors.ErrorMessage(sCodispl, 3893)
                        End If
                    End If
                    If .dExpirdat <> eRemoteDB.Constants.dtmNull Then
                        If dExpirDate = .dExpirdat Then
                            '+El campo Fecha de Renovación debe ser diferente a la fecha de renovación actual
                            Call lobjErrors.ErrorMessage(sCodispl, 3905)
                        End If
                    End If
                    '+La nueva fecha de expiración de los certificados distintos de cero debe ser igual a la
                    '+fecha de expiracion de la poliza matriz (certificado 0)
                    If nCertif <> 0 And .sColtimre = "1" Then
                        If lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
                            '+se obtiene fecha de expiración de la poliza matriz
                            If lclsPolicy.dExpirdat <> dExpirDate Then
                                '+El campo fecha de renovación debe coincidir con la fecha de renovación de la póliza matriz
                                Call lobjErrors.ErrorMessage(sCodispl, 100147)
                            End If
                        End If
                    End If
                End If
            End If

            '+Validación del campo Fecha de Próxima Facturación
            If dNextReceip = eRemoteDB.Constants.dtmNull Then
                '+El campo Fecha de Próxima Facturación debe estar lleno
                With lclsQuery
                    If .OpenQuery("Table563", "sDescript", "nCodigint=215") Then
                        Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, .FieldToClass("sDescript") & ": ")
                        .CloseQuery()
                    Else
                        Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de próxima facturación: ")
                    End If
                End With
            Else
                If nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0 And nPolicy <> eRemoteDB.Constants.intNull And nCertif <> eRemoteDB.Constants.intNull And dExpirDate <> eRemoteDB.Constants.dtmNull Then
                    '+El campo Fecha de Próxima Facturación debe encontrarse dentro de la nueva vigencia de la Póliza/Certificado,
                    '+es decir, FechaEfecto <= FechaFacturación <= NuevaFechaExpiración
                    If .dStartdate <> eRemoteDB.Constants.dtmNull Then
                        If dNextReceip > dExpirDate Or dNextReceip < .dStartdate Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3888)
                        End If
                    End If
                End If
            End If
        End With

        insValCA038_k = lobjErrors.Confirm

insValCA038_k_Err:
        If Err.Number Then
            insValCA038_k = "insValCA038_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsObject = Nothing
        'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsQuery = Nothing
    End Function

    '%insValFolder: Esta función se encarga de validar los datos introducidos en la zona de detalle de
    '%la forma.
    Public Function insValCA037_k(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal dExpirDate As Date, ByVal dNextReceip As Date, ByVal dEffecdate As Date, ByVal dExpirdateNew As Date, ByVal nUsercode As Integer, Optional ByVal sSche_Code As String = "") As String
        '- Se define el objeto para el manejo tanto de la clase  Certificat
        '- como de la clase Policy
        Dim lclsObject As ePolicy.Certificat
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsProduct As eProduct.Product
        Dim lobjErrors As eFunctions.Errors
        Dim lstrErrors As String
        Dim lclsValues As eFunctions.Values
        Dim lclsSecurity As eSecurity.Secur_sche
        Dim sValid As String = ""


        Dim lintBranch As Integer
        Dim llngPolicy As Integer
        Dim lintProduct As Integer
        Dim llngCertif As Integer
        Dim ldtmExpirdate As Date
        Dim ldEffecdate As Date
        Dim ldNextReceip As Date
        Dim ldExpirdateNew As Date
        On Error GoTo insValCA037_k_Err
        lobjErrors = New eFunctions.Errors
        lclsValues = New eFunctions.Values
        lclsSecurity = New eSecurity.Secur_sche

        lintBranch = nBranch
        llngPolicy = nPolicy
        lintProduct = nProduct
        llngCertif = nCertif
        ldtmExpirdate = dExpirDate
        ldEffecdate = dEffecdate
        ldNextReceip = dNextReceip
        ldExpirdateNew = dExpirdateNew

        '+Validación del Campo Ramo.

        If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then

            '+El campo Ramo debe estar lleno

            Call lobjErrors.ErrorMessage(sCodispl, 1022)
        End If

        '+Validación del Campo Producto.

        If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 1014)
        End If

        '+Validación del Campo Certificado.

        If nCertif = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 3006)
        End If

        'Validación que la Póliza existe

        If nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0 Then
            lclsPolicy = New ePolicy.Policy

            If Not lclsPolicy.Find(sCertype, lintBranch, lintProduct, llngPolicy) Then
                Call lobjErrors.ErrorMessage(sCodispl, 3001)
            Else
                With lclsPolicy

                    '+ Si está anulada

                    If .nNullcode <> 0 And .nNullcode <> eRemoteDB.Constants.intNull And .sStatus_pol = "6" And .dNulldate <> eRemoteDB.Constants.dtmNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3098)
                    End If


                    If .sPolitype = "3" And nCertif > 0 Then
                        Call lobjErrors.ErrorMessage(sCodispl, 90374)
                    End If

                    ' Se comenta el codigo ya que no exise este metodo en esta version
                    '                sValid = lclsSecurity.valSchemaOffice(sSche_Code, .nOffice, "1")
                    If sValid = "2" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 90496)
                    ElseIf sValid = "3" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 90497)
                    End If
                    If lclsPolicy.dStartdate = dEffecdate Then
                        Call lobjErrors.ErrorMessage(sCodispl, 50281)
                    End If
                    '+ Si no es válida

                    '                If .sStatus_pol = "3" Then
                    '                    Call lobjErrors.ErrorMessage(sCodispl, 3882)
                    '                End If

                End With
            End If
        End If

        '+El campo Póliza debe estar lleno

        If nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 3003)
        ElseIf nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nProduct <> 0 Then

            '+SCertype = 2, ya que se trata de una Póliza
            '+ Se verifican las condiciones del certificado
            lclsObject = New ePolicy.Certificat

            With lclsObject
                If .insReaCA037("2", lintBranch, lintProduct, llngPolicy, llngCertif, ldEffecdate) Then

                    '+Póliza no puede estar suspendida

                    If .nSuspCount > 0 Then
                        If nCertif > 0 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3881)
                        Else
                            Call lobjErrors.ErrorMessage(sCodispl, 3101)
                        End If
                    End If
                    '+ el certificado no puede estasr anulado
                    If .nNullcode <> 0 And .nNullcode <> eRemoteDB.Constants.intNull And .sStatusva = "6" And .dNulldate <> eRemoteDB.Constants.dtmNull Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3099)
                    End If

                    lclsProduct = New eProduct.Product

                    Call lclsProduct.FindProdMasterActive(nBranch, nProduct)

                    '+ Si es póliza de vida no puede hacerse cambio después del primer año de emisión

                    If System.Date.FromOADate(ldEffecdate.ToOADate - .dDate_Origi.ToOADate) > System.Date.FromOADate(365) And CStr(lclsProduct.sBrancht) = "1" Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3868)
                    End If

                    '+El certificado no debe tener siniestros declarados cuya fecha de ocurrencia sea
                    '+anterior a la nueva fecha de efecto

                    'If (.nDeclaredClaims) > 0 Then
                    '    Call lobjErrors.ErrorMessage(sCodispl, 3869)
                    'End If
                Else

                    '+Certificado debe estar registrado en el archivo Certificat

                    Call lobjErrors.ErrorMessage(sCodispl, 3010)
                End If

                '+Validación del CAMPO Fecha de Próxima Facturación

                If ldNextReceip = eRemoteDB.Constants.dtmNull Then

                    '+El campo Fecha de Próxima Facturación debe estar lleno

                    Call lobjErrors.ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, , 215)
                Else
                    If nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0 And nPolicy <> eRemoteDB.Constants.intNull And nCertif <> eRemoteDB.Constants.intNull Then

                        '+El campo Fecha de Próxima Facturación debe corresponder a una fecha
                        '+El campo Fecha de Próxima Facturación debe encontrarse dentro de la nueva vigencia de la Póliza/Certificado,
                        '+es decir, FechaEfecto <= FechaFacturación <= NuevaFechaExpiración

                        If ldEffecdate <> eRemoteDB.Constants.dtmNull Then
                            If ldNextReceip < ldEffecdate Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3888)
                            Else
                                If ldtmExpirdate <> eRemoteDB.Constants.dtmNull And ldNextReceip > ldtmExpirdate Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3888)
                                End If
                            End If
                        End If
                    End If
                End If

                '+Validación del Campo Fecha de Efecto.

                If ldEffecdate = eRemoteDB.Constants.dtmNull Then

                    '+El campo Fecha de Efecto debe estar lleno

                    Call lobjErrors.ErrorMessage(sCodispl, 4003)
                Else
                    If ldEffecdate <= .dStartdate Then

                        '+El campo Fecha de Efecto debe ser mayor que la fecha de efecto anterior

                        Call lobjErrors.ErrorMessage(sCodispl, 3880)
                    End If
                End If

                '+Validaciones particulares del ramo de SOAT. Algunas son para todos los Ramos

                If lintBranch <> eRemoteDB.Constants.intNull And lintBranch <> 0 And lintProduct <> eRemoteDB.Constants.intNull And llngPolicy <> eRemoteDB.Constants.intNull And llngPolicy <> 0 And ldEffecdate <> eRemoteDB.Constants.dtmNull Then

                    'And _
                    ''ldExpirdateNew <> dtmNull Then

                    lstrErrors = InsValCA037DB(sCertype, lintBranch, lintProduct, llngPolicy, llngCertif, .sBrancht, ldEffecdate, ldExpirdateNew, .sColtimre, nUsercode)

                    Call lobjErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)
                End If
            End With
        End If

        insValCA037_k = lobjErrors.Confirm

insValCA037_k_Err:
        If Err.Number Then
            insValCA037_k = "insValCA037_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsObject = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSecurity = Nothing
    End Function
    '% insValSpecialEvalCondition: Indica si existen condiciones especiales de evaluación
    '%                             es decir, si existen documentos requeridos con notas asociadas
    '%                             para los asegurados de la poliza/certificado
    '%    NOTA: Este funcion debiera dejarse publica en el futuro modulo
    '%    asociado tabla Eval_master
    '%-------------------------------------------------------------------------------------------
    Private Function insValSpecialEvalCondition(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        '%-------------------------------------------------------------------------------------------
        Dim lrecinsExistsSpecialEvalCondition As eRemoteDB.Execute
        Dim nExists As Integer

        On Error GoTo insExistsspecialevalcondition_Err

        lrecinsExistsSpecialEvalCondition = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insExistsspecialevalcondition al 12-26-2001 15:55:24
        '+
        With lrecinsExistsSpecialEvalCondition
            .StoredProcedure = "insExistsspecialevalcondition"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insValSpecialEvalCondition = .Parameters("nExists").Value = 1
            Else
                insValSpecialEvalCondition = False
            End If
        End With

insExistsspecialevalcondition_Err:
        If Err.Number Then
            insValSpecialEvalCondition = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsExistsSpecialEvalCondition may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsExistsSpecialEvalCondition = Nothing
    End Function

    '% insValVAL633_K: se validan los campos de la página
    Public Function insValVAL633_K(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal sOptInfo As String, ByVal dEffecdate As Date, ByVal nNegVPMonths As Integer, Optional ByVal dFromDate As Date = #12:00:00 AM#, Optional ByVal dToDate As Date = #12:00:00 AM#) As String
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPolicy As ePolicy.Policy
        Dim lobjErrors As eFunctions.Errors
        Dim lclsProduct As eProduct.Product

        Dim lblnValid As Boolean

        On Error GoTo insValVAL633_K_Err

        lobjErrors = New eFunctions.Errors
        lclsCertificat = New ePolicy.Certificat
        lclsPolicy = New ePolicy.Policy
        lclsProduct = New eProduct.Product

        lblnValid = True

        '+ Si la generación de recibos es Masivo
        If sOptInfo = "1" Then
            If dFromDate = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 6128)
                lblnValid = False
            End If

            If dToDate = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 6129)
            Else
                If lblnValid Then
                    If dToDate < dFromDate Then
                        Call lobjErrors.ErrorMessage(sCodispl, 6130)
                    End If
                End If
            End If
        Else
            '+ Si la generación de recibos es Puntual

            '+ Validación del Campo Ramo.
            If nBranch = eRemoteDB.Constants.intNull Then
                '+ El campo Ramo debe estar lleno
                Call lobjErrors.ErrorMessage(sCodispl, 1022)
                lblnValid = False
            End If

            '+ Validación del Campo Producto.
            If nProduct = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 1014)
                lblnValid = False
            End If

            '+ Validación del Campo Póliza.
            If nPolicy = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 3003)
                lblnValid = False
            Else
                '+ Debe ser una póliza válida
                If Not lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3001)
                    lblnValid = False
                End If
            End If

            '+ Validación del Campo Certificado.
            If nCertif = eRemoteDB.Constants.intNull Then
                If lblnValid Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3006)
                    lblnValid = False
                End If
            End If

            '+ Validación de Póliza/Certificado
            If lblnValid Then
                With lclsCertificat
                    If .Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                        '+Si el Certificado esta invalido o en captura incompleta
                        If .sStatusva = "2" Or .sStatusva = "3" Then
                            Call lobjErrors.ErrorMessage(sCodispl, 750044)
                            lblnValid = False
                        Else
                            '+ La póliza no puede estar anulada
                            If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                Call lobjErrors.ErrorMessage(sCodispl, 5090)
                                lblnValid = False
                                '+ La póliza no puede estar suspendida
                            Else
                                If .nSuspCount > 0 Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3881)
                                    lblnValid = False
                                End If
                            End If
                        End If
                    Else
                        '+ Certificado debe estar registrado en el archivo Certificat
                        Call lobjErrors.ErrorMessage(sCodispl, 3010)
                        lblnValid = False
                    End If
                End With
            End If

            '+Validación de recibo puntual - meses VP negativo
            '+Se valida que la cantidad de meses que el VP es negativo no se mayor a los meses
            '+permitidos según el producto
            ' dVp_neg es una fecha - se pasan los meses
            If lblnValid Then
                If nNegVPMonths <> eRemoteDB.Constants.intNull Then
                    If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
                        If nNegVPMonths > lclsProduct.nQmonToVPN And lclsProduct.nProdClas = 7 Then
                            Call lobjErrors.ErrorMessage(sCodispl, 55941)
                            lblnValid = False
                        End If
                    End If
                End If
            End If
        End If

        insValVAL633_K = lobjErrors.Confirm

insValVAL633_K_Err:
        If Err.Number Then
            insValVAL633_K = "insValVAL633_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '% insPostVAL633_K: Se ejecuta el proceso de generación de recibos para pólizas de VidActiva
    Public Function insPostVAL633_K(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal sOptInfo As String, ByVal nUsercode As Integer, ByVal nSessionId As String, ByVal dFromDate As Date, ByVal dToDate As Date) As Boolean
        Dim lrecpostval633_k As eRemoteDB.Execute

        On Error GoTo insPostVAL633_k_Err

        lrecpostval633_k = New eRemoteDB.Execute

        insPostVAL633_K = True

        '+Se asigna llave de proceso
        Me.sKey = Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode.ToString("000000")

        '+ Definición de parámetros para stored procedure 'insudb.inscalvactiva'
        '+ Información leída el 16/12/2001

        With lrecpostval633_k
            .StoredProcedure = "inscalVActiva"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextReceip_i", dFromDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNextReceip_e", dToDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", Me.sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nexcp_Return", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostVAL633_K = IIf(.Parameters("nexcp_Return").Value = 0, True, False)
            End If
        End With

insPostVAL633_k_Err:
        If Err.Number Then
            insPostVAL633_K = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecpostval633_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecpostval633_k = Nothing
    End Function

    '% insPreVAL633_K: se rescatan los valores iniciales de la pagina de Generación de la poliza
    Public Function insPreVAL633_K(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsRoles As ePolicy.Roles
        Dim lclsAccount_pol As ePolicy.Account_Pol

        On Error GoTo insPreVAL633_K_Err

        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsRoles = New ePolicy.Roles
        lclsAccount_pol = New ePolicy.Account_Pol

        insPreVAL633_K = True

        With lclsPolicy
            '+ Se buscan los datos de la póliza
            If .Find(sCertype, nBranch, nProduct, nPolicy) Then
                Me.sPolitype = .sPolitype
                Me.sColtimre = .sColtimre
                Me.sColinvot = .sColinvot
                Me.mdtmIssuedat = .dIssuedat

                If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                    Me.nPayfreq = lclsCertificat.nPayfreq
                    Me.dNextReceip = lclsCertificat.dNextReceip
                End If

                '+ Rescata cliente desde roles
                If lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, 1, String.Empty, dEffecdate) Then
                    Me.sClient = lclsRoles.sClient
                    Me.sClientName = lclsRoles.sCliename
                    Me.sCliDigit = lclsRoles.sDigit
                End If

                '+ Se busca el intermediario de la póliza
                If lclsPolicy.nIntermed <> eRemoteDB.Constants.intNull Then
                    Me.nIntermed = lclsPolicy.nIntermed
                    Me.sInterName = lclsPolicy.FindName(CStr(Me.nIntermed), False)
                Else
                    Me.nIntermed = eRemoteDB.Constants.intNull
                    Me.sInterName = String.Empty
                End If

                '+ Rescata meses de valor póliza negativo
                '+ 1 rescata valor poliza desde cuenta coriente de la poliza - account_pol
                '+ si vp negativo - calcula meses
                If lclsAccount_pol.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                    If lclsAccount_pol.dVp_neg <> eRemoteDB.Constants.dtmNull Then
                        If lclsAccount_pol.nValuePol <= 0 Then
                            Me.nNegVPMonths = DateDiff(Microsoft.VisualBasic.DateInterval.Month, lclsAccount_pol.dVp_neg, lclsCertificat.dNextReceip)
                            Me.nNegVPMonths = IIf(Me.nNegVPMonths <= 0, 0, Me.nNegVPMonths)
                        End If
                    Else
                        Me.nNegVPMonths = 0
                    End If
                End If
                '+ Rescata meses de valor póliza negativo
            End If
        End With

insPreVAL633_K_Err:
        If Err.Number Then
            insPreVAL633_K = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lclsAccount_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsAccount_pol = Nothing
    End Function

    '% insValVIL1405: Se realizan las validaciones de la transacción VIL1405
    Public Function insValVIL1405(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
        Dim lrecinsValVIL1405 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty


        On Error GoTo insValVIL1405_Err
        lrecinsValVIL1405 = New eRemoteDB.Execute
        With lrecinsValVIL1405
            .StoredProcedure = "insValVIL1405"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    insValVIL1405 = lobjErrors.Confirm
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing
            End If

        End With
insValVIL1405_Err:
        If Err.Number Then
            insValVIL1405 = "insValVIL1405: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecinsValVIL1405 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValVIL1405 = Nothing
        On Error GoTo 0
    End Function

    '%insValVIL733_k: Esta función se encarga de validar los datos
    '% para el proceso de Aniversario de coberturas (Productos de Vida)
    Public Function insValVIL733_k(ByVal sCodispl As String, ByVal sOptExecute As String, ByVal dEffecdate As Date, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0) As String
        '- Se define el objeto para el manejo de la clase Product

        Dim lobjErrors As eFunctions.Errors
        Dim lclsProduct As eProduct.Product

        lclsProduct = New eProduct.Product
        lobjErrors = New eFunctions.Errors

        On Error GoTo insValVIL733_k_Err

        ' Validación de Producto de Vida
        ' Si ramo producto vienen vacios se ejecutan todos los ramos
        ' Si ramo producto vienen vacios y tiene ramo se ejecuta el proceso solo si el ramo es de vida
        ' Validación de Producto de Vida
        If nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0 Then
            If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
                Call lobjErrors.ErrorMessage(sCodispl, 3635)
            Else
                '+ Se valida que el ramo-producto corresponda a vida o combinado
                With lclsProduct
                    Call .insValProdMaster(nBranch, nProduct)
                    If .blnError Then
                        If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3987)
                        End If
                    End If
                End With
            End If
        End If
        '+Validación del Campo dEffecdate
        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 7116)
        End If

        insValVIL733_k = lobjErrors.Confirm

insValVIL733_k_Err:
        If Err.Number Then
            insValVIL733_k = "insValVIL733_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '%insPostVIL733_k: Esta función se encarga de ejecutar el proceso
    '%de Aniversario de coberturas (Productos de Vida)
    Public Function insPostVIL733_k(ByVal sCodispl As String, ByVal sOptExecute As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0) As Boolean
        '- Se define el objeto para el manejo de conección
        Dim lrecPostVIL733_k As eRemoteDB.Execute
        Dim nExcp_Return As Integer

        lrecPostVIL733_k = New eRemoteDB.Execute

        On Error GoTo insPostVIL733_k_Err

        insPostVIL733_k = True

        nExcp_Return = 0

        With lrecPostVIL733_k
            .StoredProcedure = "insupdcover_birthdat"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptExecute", sOptExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExcp_Return", nExcp_Return, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostVIL733_k = .Run(False)
        End With

insPostVIL733_k_Err:
        If Err.Number Then
            insPostVIL733_k = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPostVIL733_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPostVIL733_k = Nothing
    End Function
    '%insPostCAL006_k: Esta función se encarga de ejecutar el proceso
    '%de Reservas de Primas
    Public Function insPostCAL006_k(ByVal sCodispl As String, ByVal sOptInsur As String, ByVal sOptDetail As String, ByVal dEffecdate As Date, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
        '- Se define el objeto para el manejo de conección
        Dim lrecPostCAL006_k As eRemoteDB.Execute
        Dim nExcp_Return As Integer
        Dim npBranch As Integer
        Dim npProduct As Integer

        lrecPostCAL006_k = New eRemoteDB.Execute

        On Error GoTo insPostCAL006_k_Err

        insPostCAL006_k = True

        npBranch = nBranch
        npProduct = nProduct
        nExcp_Return = 0

        'Definición de parámetros para stored procedure 'insudb.inscallifereserves'
        'Información leída el 18/01/2002

        With lrecPostCAL006_k
            .StoredProcedure = "inscallifereserves"
            .Parameters.Add("npBranch", npBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("npProduct", npProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExcp_Return", nExcp_Return, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCAL006_k = .Run(False)
        End With

insPostCAL006_k_Err:
        If Err.Number Then
            insPostCAL006_k = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPostCAL006_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPostCAL006_k = Nothing
    End Function

    '%insPostCA037_k: Esta función se encaga de validar todos los datos
    Public Function insPostCA037_k(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal dNextReceip As Date, ByVal dExpirdateNew As Date, ByVal sReceiptType As String, Optional ByVal nTransaction As Integer = 0, Optional ByVal dOldNextReceip As Date = #12:00:00 AM#) As Boolean
        Dim lclsCertif As Certificat
        Dim lclsPolicy As Policy
        Dim lclsProduct As eProduct.Product
        Dim lclsTDetail_pre As TDetail_pre

        Dim ldtmExpirdat As Date

        Dim lintTransacio As Integer

        On Error GoTo insPostCA037_k_Err

        lclsPolicy = New Policy
        If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
            lclsCertif = New Certificat
            If lclsCertif.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                lclsProduct = New eProduct.Product
                If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
                    '+ Se calcula la nueva fecha de vencimiento
                    '                If dExpirdateNew = dtmNull Then
                    'ldtmExpirdat = DateAdd("m", lclsProduct.nDuration, dEffecdate)
                    'Else
                    '   ldtmExpirdat = dExpirdateNew

                    'End If

                    If lclsCertif.insEffecDateChange(lclsPolicy.sPolitype, lclsPolicy.sColtimre, "2", nBranch, nProduct, nPolicy, nCertif, dEffecdate, ldtmExpirdat, dNextReceip, nUsercode, sReceiptType) Then
                        lclsTDetail_pre = New TDetail_pre
                        If lclsPolicy.sPolitype = "2" And lclsPolicy.sColinvot = "2" And lclsPolicy.sColtimre = "1" And nCertif = 0 Then
                            Call lclsTDetail_pre.InsCalReceiptMod(sCertype, nBranch, nProduct, nPolicy, nCertif, dOldNextReceip, nUsercode, nTransaction, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, 11, "2", String.Empty, eRemoteDB.Constants.intNull, CStr(0))
                            Call lclsCertif.insUpdNextreceipt(sCertype, nBranch, nProduct, nPolicy, nCertif, dExpirdateNew, nUsercode)
                        End If
                        'UPGRADE_NOTE: Object lclsTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsTDetail_pre = Nothing
                        insPostCA037_k = True
                    End If
                End If
            End If
        End If

insPostCA037_k_Err:
        If Err.Number Then
            insPostCA037_k = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertif = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '%insPostCA038_k: Esta función se encaga de realizar el cambio de fecha de renovación
    Public Function insPostCA038_k(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dExpirdat As Date, ByVal dNextReceip As Date, ByVal sColtimre As String, ByVal dStartdate As Date, ByVal sPolitype As String, ByVal sOptReceiptType As String, ByVal nUsercode As Integer) As Boolean
        Dim lbytTransac As Byte
        Dim lstrDate As String
        Dim lstrPolitype As String

        Dim mclsProduct As eProduct.Product
        Dim mclsCertificat As ePolicy.Certificat

        mclsProduct = New eProduct.Product
        mclsCertificat = New ePolicy.Certificat

        On Error GoTo insPostCA038_k_Err

        Call mclsProduct.FindProdMasterActive(nBranch, nProduct)

        If mclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
        End If

        With mclsCertificat
            .sPolitype = sPolitype
            .sColtimre = sColtimre
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dExpirdat = dExpirdat
            .dNextReceip = dNextReceip
            .nUsercode = nUsercode
            insPostCA038_k = .Update_RenDate
        End With

insPostCA038_k_Err:
        If Err.Number Then
            insPostCA038_k = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object mclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsProduct = Nothing
        'UPGRADE_NOTE: Object mclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsCertificat = Nothing
    End Function

    '% insValPrevMov: check if policy has previous movements to be reversed
    '% insValPrevMov: verifica si la póliza tiene movimientos previos que reversar
    Private Function insvalPrevMov(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Integer
        Dim lclsPolicy_his As Policy_his

        lclsPolicy_his = New Policy_his

        insvalPrevMov = 3246

        With lclsPolicy_his

            If .FindLastMovement(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                '+ Se verifica si el último movimiento fue una renovación o modificación
                Select Case .nType

                    '+ Renovación de la póliza o certificado
                    Case 44, 45
                        insvalPrevMov = 0
                        Me.nTransactio = 0
                        Me.nNullOutMov = 0
                        If sPolitype <> "1" And sColtimre = "1" Then
                            Me.sReverCertif = "1"
                        Else
                            Me.sReverCertif = "0"
                        End If
                        Me.bNullReceipt = Not (sPolitype = "1" Or (sPolitype <> "1" And sColinvot = "1" And nCertif = 0) Or (sPolitype <> "1" And sColinvot <> "1" And nCertif > 0))

                        '+ Si es modificación de póliza o certificado o
                        '+ Si es una modificación temporal
                    Case 11, 12, 54, 55, 61
                        If mdtmIssuedat < .dEffecdate Or .nType = 61 Then
                            insvalPrevMov = 0
                            Me.nTransactio = 1
                            Me.sReverCertif = "0"
                            Me.bNullReceipt = Not (sPolitype = "1" Or nCertif > 0)
                            If sPolitype <> "1" And sColinvot = "1" Then
                                Me.nNullOutMov = 1
                            Else
                                Me.nNullOutMov = 0
                            End If
                        Else
                            insvalPrevMov = 3925
                        End If

                        '+ Recibo subsecuente de póliza o certificado
                    Case 57, 58
                        insvalPrevMov = 0
                        Me.nTransactio = 2
                        Me.nNullOutMov = 0
                        If sPolitype <> "1" And sColtimre = "1" Then
                            Me.sReverCertif = "1"
                        Else
                            Me.sReverCertif = "0"
                        End If
                        Me.bNullReceipt = Not (sPolitype = "1" Or (sPolitype <> "1" And sColinvot = "1" And nCertif = 0) Or (sPolitype <> "1" And sColinvot <> "1" And nCertif > 0))
                  Case 35
                        insvalPrevMov = 0
                        Me.nTransactio = 3
                        Me.nNullOutMov = 0
                        If sPolitype <> "1" And sColtimre = "1" Then
                            Me.sReverCertif = "1"
                        Else
                            Me.sReverCertif = "0"
                        End If
                        Me.bNullReceipt = Not (sPolitype = "1" Or (sPolitype <> "1" And sColinvot = "1" And nCertif = 0) Or (sPolitype <> "1" And sColinvot <> "1" And nCertif > 0))
                   
                    Case 38, 39
                        insvalPrevMov = 0
                        Me.nTransactio = 1
                        If sPolitype <> "1" And sColtimre = "1" Then
                            Me.sReverCertif = "1"
                        Else
                            Me.sReverCertif = "1"
                        End If
                        If sPolitype <> "1" And sColinvot = "1" Then
                            Me.nNullOutMov = 1
                        Else
                            Me.nNullOutMov = 1
                        End If
                        Me.bNullReceipt = True
                End Select
                Me.dTransDate = .dEffecdate
            End If
        End With

        'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_his = Nothing

    End Function

    '% AddProposal : Agrega una propuesta
    Public Function AddProposal(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nProposal As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nAgency As Integer) As Boolean
        Dim lrecinsCreProposal As eRemoteDB.Execute

        On Error GoTo AddProposal_Err
        lrecinsCreProposal = New eRemoteDB.Execute

        '+ Definición de store procedure insCreProposal al 12-05-2001 20:45:19
        With lrecinsCreProposal
            .StoredProcedure = "insCreProposal"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProposal", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdat", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntype_amend", 13, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            AddProposal = .Run(False)
            Me.nProposal = .Parameters("nProposal").Value
        End With

AddProposal_Err:
        If Err.Number Then
            AddProposal = False
        End If
        'UPGRADE_NOTE: Object lrecinsCreProposal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCreProposal = Nothing
        On Error GoTo 0
    End Function

    '% ' insPostCa035: Realiza la validación de los campos a actualizar en la ventana CA035.
    '  (Suspension de Garantias)
    Public Function insPostCA035(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dExpirDate As Date, ByVal sMailnum As String, ByVal nCode_sus As Integer, ByVal nNotenum As Integer, ByVal dStartdate As Date, ByVal dPolExpirdate As Date, ByVal dNextReceip As Date, ByVal nUsercode As Integer) As Boolean

        Dim lclsSuspend As ePolicy.Suspend

        lclsSuspend = New ePolicy.Suspend

        Select Case nAction
            Case 301
                insPostCA035 = lclsSuspend.Add(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dExpirDate, sMailnum, nCode_sus, nNotenum, "1", dStartdate, dPolExpirdate, dNextReceip, nUsercode)
            Case 302
                insPostCA035 = lclsSuspend.Update(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dExpirDate, sMailnum, nCode_sus, nNotenum, "1", dStartdate, dPolExpirdate, dNextReceip, nUsercode)
            Case Else
                insPostCA035 = True
        End Select

        'UPGRADE_NOTE: Object lclsSuspend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSuspend = Nothing

    End Function

    '% insValCA035: Realiza la validación de los campos a actualizar en la ventana CA035.
    '  (Suspension de Garantias)
    Public Function insValCA035(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dExpirDate As Date, ByVal sMailnum As String, ByVal nCode_sus As Integer, ByVal nNotenum As Integer, ByVal dStartdate As Date, ByVal dPolExpirdate As Date, ByVal dNextReceip As Date, ByVal nUsercode As Integer) As String

        On Error GoTo insValCA035_Err

        '- Se define el objeto para el manejo tanto de la clase  Certificat
        '- como de la clase Policy
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsProduct As eProduct.Product_ge
        Dim lclsSuspend As ePolicy.Suspend
        Dim lobjErrors As eFunctions.Errors
        Dim lobjValues As eFunctions.Values
        Dim lrecreaCtrol_Date As eRemoteDB.Execute
        Dim lrecrealedger As eRemoteDB.Execute
        Dim lrecreaSuspend_date As eRemoteDB.Execute

        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsProduct = New eProduct.Product_ge
        lobjErrors = New eFunctions.Errors
        lobjValues = New eFunctions.Values
        lclsSuspend = New ePolicy.Suspend


        Dim lintBranch As Integer
        Dim llngPolicy As Integer
        Dim lintProduct As Integer
        Dim llngCertif As Integer
        Dim ldtmStartdate As Date

        With lobjValues
            lintBranch = .StringToType(CStr(nBranch), eFunctions.Values.eTypeData.etdLong)
            llngPolicy = .StringToType(CStr(nPolicy), eFunctions.Values.eTypeData.etdLong)
            lintProduct = .StringToType(CStr(nProduct), eFunctions.Values.eTypeData.etdLong)
            llngCertif = .StringToType(CStr(nCertif), eFunctions.Values.eTypeData.etdLong)
        End With

        If llngCertif = 0 Then
            With lclsPolicy
                If Not .FindPolicyOfficeName(sCertype, lintBranch, lintProduct, llngPolicy, "1") Then
                    Call lobjErrors.ErrorMessage("CA035", 3917)
                Else
                    ldtmStartdate = .dStartdate
                End If
            End With
        Else
            With lclsCertificat
                If Not .Find(sCertype, lintBranch, lintProduct, llngPolicy, llngCertif) Then
                    Call lobjErrors.ErrorMessage("CA035", 3010)
                Else
                    ldtmStartdate = .dStartdate
                End If
            End With
        End If

        '+Fecha de fin de suspensión es anterior a fecha de inicio
        If Not (dPolExpirdate = eRemoteDB.Constants.dtmNull) And (dExpirDate = eRemoteDB.Constants.dtmNull) Then
            dExpirDate = dPolExpirdate
        End If

        If Not (dExpirDate = eRemoteDB.Constants.dtmNull) Then
            If dExpirDate < dEffecdate Then
                Call lobjErrors.ErrorMessage("CA035", 3107)
            End If
        End If

        '+Incluya la causa de suspensión de garantías
        If nCode_sus = 0 Then
            Call lobjErrors.ErrorMessage("CA035", 3320)
        End If

        '+Incluya la fecha de efecto
        If dStartdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CA035", 5055)
        Else
            If dStartdate < ldtmStartdate Then
                Call lobjErrors.ErrorMessage("CA035", 3105)
            End If
        End If

        '+Incluya fecha de vencimiento
        If dPolExpirdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CA035", 5050)
        Else
            If Not dStartdate = eRemoteDB.Constants.dtmNull Then
                '+Fecha de vencimiento debe ser posterior a la fecha de efecto
                If dPolExpirdate < dStartdate Then
                    Call lobjErrors.ErrorMessage("CA035", 3059)
                End If
            End If
        End If

        '+Fecha de próxima facturación debe ser posterior a la fecha efecto de la póliza.
        If dNextReceip <> eRemoteDB.Constants.dtmNull Then
            If dNextReceip < dStartdate Then
                Call lobjErrors.ErrorMessage("CA035", 3904)
            End If
        End If

        '+ Se valida que no exista periodo de suspensión anterior
        If lclsSuspend.Validate_date(sCertype, lintBranch, lintProduct, llngPolicy, llngCertif, dExpirDate) Then
            Call lobjErrors.ErrorMessage("CA035", 3429)
        End If

        insValCA035 = lobjErrors.Confirm

insValCA035_Err:
        If Err.Number Then
            insValCA035 = "insValCA035: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lclsSuspend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSuspend = Nothing
    End Function
    '% insValCA035: Realiza la validación de los campos a actualizar en la ventana CA035.
    '  (Suspension de Garantias)
    Public Function insValCal963_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsValues As eFunctions.Values
        Dim lclsPolicy As ePolicy.Policy
        Dim lblnValid As Boolean

        On Error GoTo insValCal963_K_Err

        lclsErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy

        lblnValid = True

        '**+ Validations on the field "Branch".
        '+ Validaciones sobre el campo "Ramo".
        If nBranch = eRemoteDB.Constants.intNull Then
            '+ Debe estar lleno
            Call lclsErrors.ErrorMessage(sCodispl, 1022)
            lblnValid = False
        End If

        '**+ Validations on the field "Code of product".
        '+ Validaciones sobre el campo "Código del producto".

        If nProduct = eRemoteDB.Constants.intNull Then
            '+ Debe estar lleno
            Call lclsErrors.ErrorMessage(sCodispl, 1014)
        End If

        If nPolicy = eRemoteDB.Constants.intNull Then
            '+ Debe estar lleno
            Call lclsErrors.ErrorMessage(sCodispl, 3003)
        Else
            If lblnValid Then
                If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy, True) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 3001)
                Else
                    If lclsPolicy.sPolitype = "1" Then
                        Call lclsErrors.ErrorMessage(sCodispl, 3109)
                    Else
                        '+ La póliza no puede estar anulada
                        If lclsPolicy.nNullcode <> eRemoteDB.Constants.intNull Then
                            Call lclsErrors.ErrorMessage(sCodispl, 3098)
                        End If

                        If lclsPolicy.sStatus_pol = "3" Then
                            '+ La póliza no puede estar en captura incompleta
                            Call lclsErrors.ErrorMessage(sCodispl, 3720)
                        End If
                    End If
                End If
            End If
        End If

        insValCal963_K = lclsErrors.Confirm

insValCal963_K_Err:
        If Err.Number Then
            insValCal963_K = "insValCal963_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing

    End Function

    '%insValCA035_k: Esta función se encarga de validar los datos introducidos en la cabecera de la
    '%forma.
    Public Function insValCA035_k(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nAction As Integer) As String

        '- Se define el objeto para el manejo tanto de la clase  Certificat
        '- como de la clase Policy
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsProduct As eProduct.Product_ge
        Dim lclsSuspend As ePolicy.Suspend
        Dim lobjErrors As eFunctions.Errors
        Dim lobjValues As eFunctions.Values
        Dim lclsCtrol_Date As eGeneral.Ctrol_date
        Dim lrecrealedger As eRemoteDB.Execute

        Dim lintBranch As Integer
        Dim llngPolicy As Integer
        Dim lintProduct As Integer
        Dim llngCertif As Integer
        Dim ldtmStartdate As Date
        Dim ldtmExpirdat As Date
        Dim lblnValidPolicy As Boolean

        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsProduct = New eProduct.Product_ge
        lclsSuspend = New ePolicy.Suspend
        lobjErrors = New eFunctions.Errors
        lobjValues = New eFunctions.Values

        On Error GoTo insValCA035_k_Err

        lblnValidPolicy = True
        lintBranch = nBranch
        llngPolicy = nPolicy
        lintProduct = nProduct
        llngCertif = IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif)

        '+Se valida el campo de Ramos
        If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
            Call lobjErrors.ErrorMessage("CA035", 1022)
        End If

        '+Se va a validar el campo de product
        If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
            Call lobjErrors.ErrorMessage("CA035", 1014)
        End If

        '+Se va a validar el Campo de poliza
        With lclsPolicy
            '+Si la Póliza es igual a cero o de Nula
            If nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0 Then
                Call lobjErrors.ErrorMessage("CA035", 3003)
                lblnValidPolicy = False
            Else
                '+Si se encuentra la Póliza
                If .FindPolicyOfficeName("2", lintBranch, lintProduct, llngPolicy, "1") Then
                    '+Si el Estado de la Póliza no es invalido o en captura incompleta
                    If (Trim(.sStatus_pol) <> "2") And (Trim(.sStatus_pol) <> "3") Then
                        If Not (.nNullcode = eRemoteDB.Constants.intNull) Then
                            '+Si la Poliza está Anulada
                            If CShort(.nNullcode) <> 0 Then
                                Call lobjErrors.ErrorMessage("CA035", 3098)
                            End If
                        End If
                    Else
                        Call lobjErrors.ErrorMessage("CA035", 3882)
                    End If
                    '+Si el certificado es igual a cero y la Póliza es Colectiva
                    If llngCertif = 0 And .sPolitype = "2" Then
                        Call lobjErrors.ErrorMessage("CA035", 3661)
                    End If

                    ldtmStartdate = .dStartdate
                    ldtmExpirdat = .DEXPIRDAT
                Else
                    Call lobjErrors.ErrorMessage("CA035", 3917)
                    lblnValidPolicy = False
                End If
            End If
        End With

        '+Se va a validar el campo de Certificado
        With lclsCertificat
            If llngCertif <> eRemoteDB.Constants.intNull And lblnValidPolicy Then
                '+Si no son nulos el Ramo,el Producto o la Póliza
                If lintBranch <> eRemoteDB.Constants.intNull And llngPolicy <> eRemoteDB.Constants.intNull And lintProduct <> eRemoteDB.Constants.intNull Then
                    '+Si no se encuentra el certificado
                    If Not .Find(sCertype, lintBranch, lintProduct, llngPolicy, llngCertif) Then
                        Call lobjErrors.ErrorMessage("CA035", 3010)
                    Else
                        '+Si el Certificado es Valido
                        If .sStatusva = "2" Or .sStatusva = "3" Then
                            Call lobjErrors.ErrorMessage("CA035", 3883)
                        Else
                            '+Si el Certificado no está anulado
                            If .nNullcode <> 0 And .nNullcode <> eRemoteDB.Constants.intNull Then
                                Call lobjErrors.ErrorMessage("CA035", 3099)
                            End If
                        End If
                        ldtmStartdate = .dStartdate
                        ldtmExpirdat = .dExpirdat
                    End If
                End If
            End If
        End With

        '+Se valida que el producto permita la transacion de suspensión
        With lclsProduct
            If lintBranch <> eRemoteDB.Constants.intNull And lintProduct <> eRemoteDB.Constants.intNull Then
                If .Find(lintBranch, lintProduct, ldtmStartdate) Then
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    If Not IsDBNull(.sSuspendi) Then
                        If Trim(.sSuspendi) = "2" Then
                            Call lobjErrors.ErrorMessage("CA035", 3142)
                        End If
                    Else
                        Call lobjErrors.ErrorMessage("CA035", 3142)
                    End If
                End If
            End If
        End With

        '+Se realiza la validacion de la fecha de inicio de la suspension

        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CA035", 3473)
        Else
            '+Si se está agregando
            With lclsSuspend
                If nAction = 301 Then
                    If ldtmStartdate > dEffecdate Then
                        Call lobjErrors.ErrorMessage("CA035", 3106)
                    End If
                    If ldtmExpirdat <= dEffecdate Then
                        Call lobjErrors.ErrorMessage("CA035", 3575)
                    End If
                    '+ Se valida que no exista periodo de suspensión anterior
                    If .Validate_date(sCertype, lintBranch, lintProduct, llngPolicy, llngCertif, dEffecdate) Then
                        Call lobjErrors.ErrorMessage("CA035", 3429)
                    End If

                Else
                    If nAction = 302 Or nAction = 401 Then
                        If Not .Find(sCertype, lintBranch, lintProduct, llngPolicy, llngCertif, dEffecdate) Then
                            Call lobjErrors.ErrorMessage("CA035", 3430)
                        End If
                    End If
                End If
            End With
        End If

        '+Fecha anterior al último proceso de asientos automáticos

        lclsCtrol_Date = New eGeneral.Ctrol_date

        If dEffecdate <> eRemoteDB.Constants.dtmNull Then
            If lclsCtrol_Date.Find(1) Then

                If lclsCtrol_Date.dEffecdate >= dEffecdate Then
                    Call lobjErrors.ErrorMessage("CA035", 1008)
                End If
            Else
                Call lobjErrors.ErrorMessage("CA035", 1008)
            End If
        End If
        'UPGRADE_NOTE: Object lclsCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCtrol_Date = Nothing

        '+ Fecha corresponde a un periodo contable cerrado
        lrecrealedger = New eRemoteDB.Execute

        If dEffecdate <> eRemoteDB.Constants.dtmNull Then
            With lrecrealedger
                .StoredProcedure = "realedger"
                If .Run Then
                    If .FieldToClass("dStart_date") > dEffecdate Or .FieldToClass("dEnd_date") < dEffecdate Then
                        Call lobjErrors.ErrorMessage("CA035", 1006)
                    End If
                    .RCloseRec()
                Else
                    Call lobjErrors.ErrorMessage("CA035", 1006)
                End If
            End With
        End If

        insValCA035_k = lobjErrors.Confirm

insValCA035_k_Err:
        If Err.Number Then
            insValCA035_k = "insValCA035_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsSuspend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSuspend = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lrecrealedger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecrealedger = Nothing
    End Function

    '%InsCalUpdCash_mov: Se actualiza el estado de los cheques asociados  a una poliza/certificado
    Function InsCalUpdCash_mov(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nProcessType As Integer) As Boolean
        Dim lrecInsCalUpdCash_mov As eRemoteDB.Execute

        On Error GoTo InsCalUpdCash_mov_Err
        '**+ Parameter definition for stored procedure 'InsCalUpdCash_mov'
        '+Definición de parámetros para stored procedure 'InsCalUpdCash_mov'
        '+Información leída el 12/02/2002
        lrecInsCalUpdCash_mov = New eRemoteDB.Execute
        With lrecInsCalUpdCash_mov
            .StoredProcedure = "InsCalUpdCash_mov"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProcesstype", nProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSumcheque", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsCalUpdCash_mov = .Run(False)
            If nProcessType = 2 Then
                mdblSumCheque = .Parameters("nSumcheque").Value
            End If
        End With

InsCalUpdCash_mov_Err:
        If Err.Number Then
            InsCalUpdCash_mov = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecInsCalUpdCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsCalUpdCash_mov = Nothing
    End Function

    '%insValVI009_K: Validaciones de la cabecera de forma VI009
    Public Function InsValVI009_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sSurrType As String, ByVal nPayWay As Integer, Optional ByVal sCompanyType As String = "1", Optional ByVal sProcessType As String = "", Optional ByVal nOffice As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal nProponum As Double = eRemoteDB.Constants.intNull, Optional ByVal nUsercode As Integer = 0) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsProduct As eProduct.Product = New eProduct.Product
        Dim lobjGeneral As eGeneral.GeneralFunction
        Dim lclsPolicy As ePolicy.Policy = New ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat = New ePolicy.Certificat
        Dim lobjClaim As Object
        Dim lintyear As Integer
        Dim lintMonth As Integer
        Dim lblnSurrTotal As Boolean
        Dim lclsPremium As Object
        Dim lblnError As Boolean
        Dim lstrQuotProp As String

        On Error GoTo InsValVI009_K_Err

        lclsErrors = New eFunctions.Errors
        lblnSurrTotal = sSurrType = "1"
        '+ Se valida que el campo "Ramo" tenga información
        If nBranch = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 1022)
            lblnError = True
        End If

        '+ Se valida que el campo "Producto" tenga información
        If nProduct = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 1014)
            lblnError = True
        ElseIf Not lblnError Then
            '+ Se valida que el producto corresponda a vida o combinado
            lclsProduct = New eProduct.Product
            With lclsProduct
                If .FindProdMaster(nBranch, nProduct) Then
                    If .sBrancht <> eProduct.Product.pmBrancht.pmlife And .sBrancht <> eProduct.Product.pmBrancht.pmNotTraditionalLife And .sBrancht <> eProduct.Product.pmBrancht.pmMixed Then
                        lclsErrors.ErrorMessage(sCodispl, 3987)
                        lblnError = True
                    End If
                End If
            End With
        End If

        '+ Se valida que el producto asociado a la póliza debe permitir la transacción
        If Not lblnError Then
            With lclsProduct
                If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                    If (.sSurrenpi = "2" And Not lblnSurrTotal) Or (.sSurrenti = "2" And lblnSurrTotal) Then
                        lclsErrors.ErrorMessage(sCodispl, 3406)
                        lblnError = True
                    End If
                End If
            End With
        End If

        '+ Se valida que el campo "Póliza" tenga información
        If nPolicy = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 3003)
            lblnError = True
        ElseIf Not lblnError Then
            '+ Se valida que sea una póliza válida
            lclsPolicy = New ePolicy.Policy
            With lclsPolicy
                If Not .FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, sCompanyType) Then
                    lclsErrors.ErrorMessage(sCodispl, 3001)
                    lblnError = True
                Else
                    If .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrInvalid) Then
                        lclsErrors.ErrorMessage(sCodispl, 3720)
                        lblnError = True
                    ElseIf .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrSaldProrr) Then
                        lclsErrors.ErrorMessage(sCodispl, 90176)
                        lblnError = True
                    Else
                        If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                            If .nNullcode = 11 Then
                                If DateDiff(Microsoft.VisualBasic.DateInterval.Year, .dNulldate, dEffecdate) < 5 Then
                                    If sSurrType = "1" Then
                                    Else
                                        '+ Pólizas anuladas por falta de pago sólo pueden tener rescate total
                                        lclsErrors.ErrorMessage(sCodispl, 750135)
                                        lblnSurrTotal = False
                                        lblnError = True
                                    End If
                                Else
                                    '+ Pólizas anuladas por falta de pago deben tener menos de 5 años de caducada
                                    lclsErrors.ErrorMessage(sCodispl, 750136)
                                    lblnSurrTotal = False
                                    lblnError = True
                                End If
                            Else
                                '+ Pólizas anuladas para tener rescate, causal de anulación debe ser Falta de Pago
                                lclsErrors.ErrorMessage(sCodispl, 750137)
                                lblnSurrTotal = False
                                lblnError = True
                            End If
                        End If
                    End If
                End If
            End With
        End If

        '+ Se válida que el certificado sea válido
        If nCertif = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 3006)
            lblnError = True
        ElseIf Not lblnError Then
            lclsCertificat = New ePolicy.Certificat
            With lclsCertificat
                If Not .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                    lclsErrors.ErrorMessage(sCodispl, 1978)
                    lblnError = True
                Else
                    If lclsPolicy.sPolitype <> "1" And nCertif > 0 Then
                        If .sStatusva = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Or .sStatusva = CStr(Policy.TypeStatus_Pol.cstrInvalid) Then
                            lclsErrors.ErrorMessage(sCodispl, 750044)
                            lblnError = True
                        Else
                            If .dNulldate <> eRemoteDB.Constants.dtmNull Then
                                lclsErrors.ErrorMessage(sCodispl, 3099)
                                lblnError = True
                            End If
                        End If
                    End If

                    '+ Se valida que la poliza/certificado no tenga propuestas especiales/de endoso pendientes
                    If nProponum = eRemoteDB.Constants.intNull Then
                        lstrQuotProp = lclsCertificat.Proposal_val(nBranch, nProduct, nPolicy, nCertif, 1, eRemoteDB.Constants.intNull)
                        If lstrQuotProp <> "" Then
                            Call lclsErrors.ErrorMessage(sCodispl, 55649, , eFunctions.Errors.TextAlign.RigthAling, "(" & lstrQuotProp & ")")
                        End If
                    End If

                    '+ Se da advertencia si poliza/certificado de vida posee una declaración de siniestro
                    If lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmlife Then
                        lobjClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
                        If lobjClaim.reacountclaim("2", nBranch, nProduct, nPolicy, nCertif) <> 0 Then
                            lclsErrors.ErrorMessage(sCodispl, 55778)
                        End If
                        'UPGRADE_NOTE: Object lobjClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lobjClaim = Nothing
                    End If

                    '+ Se valida que la póliza no tenga recibos pendientes por intereses por préstamo
                    lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
                    If lclsPremium.GetLoansInterest(sCertype, nBranch, nProduct, nPolicy, nCertif) > 0 Then
                        lclsErrors.ErrorMessage(sCodispl, 60465)
                    End If
                    'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsPremium = Nothing

                    '+ Se verifica que la póliza no tenga exención de cobertura
                    If .sExemption = "1" Then
                        lclsErrors.ErrorMessage(sCodispl, 38017)
                    End If
                End If
            End With
        End If

        '+ Validacion de la Fecha de rescate
        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            lclsErrors.ErrorMessage(sCodispl, 3404)
        ElseIf Not lblnError Then
            If dEffecdate <= lclsCertificat.dDate_Origi Then
                lclsErrors.ErrorMessage(sCodispl, 3405)
            Else

                '+ Se valida que la diferencia de años entre el efecto del certificado y la realización de la transacción debe ser superior
                '+ o igual a dos años
                lobjGeneral = New eGeneral.GeneralFunction
                Call lobjGeneral.getYearMonthDiff(lclsCertificat.dDate_Origi, dEffecdate, lintyear, lintMonth)
                'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjGeneral = Nothing

                '+ Se validan meses minimos de vigencia segun producto
                If (lintyear * 12 + lintMonth) < lclsProduct.nQmepsurr Then
                    lclsErrors.ErrorMessage(sCodispl, 60301)
                End If

                '+ Se verifica si la póliza tiene valor de rescate.
                If lclsCertificat.insGetSurrenAmount(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, nCertif, dEffecdate, sCodispl, , lclsProduct.nProdClas, lclsCertificat.dStartdate, lclsProduct.sRousurre, nUsercode, sSurrType, sProcessType) = 0 Then
                    lclsErrors.ErrorMessage(sCodispl, 3408)
                End If

                If Not lblnSurrTotal Then

                    '+ Verifica que la cantidad de rescates en el mes sea el permitido
                    If lclsProduct.nQmmsurr <> eRemoteDB.Constants.intNull Then
                        If lclsCertificat.InsCalQSurr(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) >= lclsProduct.nQmmsurr Then
                            lclsErrors.ErrorMessage(sCodispl, 60306)
                        End If
                    End If

                    '+ Verifica que la cantidad de rescates en el año sea el permitido
                    If lclsProduct.nQmysurr <> eRemoteDB.Constants.intNull Then
                        If lclsCertificat.InsCalQSurr(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "Y") >= lclsProduct.nQmysurr Then
                            lclsErrors.ErrorMessage(sCodispl, 60307)
                        End If
                    End If
                End If
            End If
        End If

        '+ Se valida que el campo Orden de pago tenga información
        If nPayWay = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 9104)
        End If

        If nOffice = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 9120)
        End If
        If nOfficeAgen = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 55519)
        End If
        If nAgency = eRemoteDB.Constants.intNull Then
            lclsErrors.ErrorMessage(sCodispl, 1080)
        End If

        '+ Se envia advertencia al usuario para que determine si desea proseguir el proceso
        If lblnSurrTotal And sProcessType = "2" Then
            lclsErrors.ErrorMessage(sCodispl, 3962)
        End If

        InsValVI009_K = lclsErrors.Confirm

InsValVI009_K_Err:
        If Err.Number Then
            InsValVI009_K = "InsValVI009_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjClaim = Nothing
        'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPremium = Nothing
    End Function

    '% SurrenderDate: Obtiene la fecha de rescate de póliza
    Public Function SurrenderDate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Date
        Dim lclsCertificat As Certificat
        Dim ldtmSurrDate As Date
        Dim lclsPremium As Object

        On Error GoTo SurrenderDate_Err
        lclsCertificat = New Certificat
        If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
            ldtmSurrDate = lclsCertificat.dNulldate
            If ldtmSurrDate = eRemoteDB.Constants.dtmNull Then
                lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
                If lclsPremium.FindLastPayDate(sCertype, nBranch, nProduct, nPolicy) Then
                    ldtmSurrDate = lclsPremium.dExpirdat
                End If
                'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsPremium = Nothing
            End If
        End If
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        SurrenderDate = ldtmSurrDate

SurrenderDate_Err:
        If Err.Number Then
            SurrenderDate = CDate(Nothing)
        End If
    End Function

    '% InsPreVI7000 : Permite predefinir los valores utilizados en VI7000
    Public Function InsPreVI7000(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nProponum As Double, ByVal nSurr_reason As Integer, ByVal sSurrType As String, ByVal sCodispl As String, ByVal nOrigin As Integer) As Boolean
        Dim lrecinsPrevi7000 As eRemoteDB.Execute
        On Error GoTo InsPreVI7000_Err

        Me.sClient = New String(" ", 14)

        lrecinsPrevi7000 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insPrevi7000 al 04-28-2003 12:06:58
        '+
        With lrecinsPrevi7000
            .StoredProcedure = "insPrevi7000"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSurrType", sSurrType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then

                InsPreVI7000 = True
                '+Razón de rescate
                mlngSurr_reason = .FieldToClass("nSurr_reason", 0)

                '+Tipo orden de pago
                mlngType_payment = .FieldToClass("nType_payment", 0)
                '+Institución
                mlngInstitution = .FieldToClass("nInstitution", 0)
                '+Cliente
                Me.sClient = Trim(.FieldToClass("sClient"))
                '+Valor póliza
                mdblRoutSurr = .FieldToClass("nRoutSurr", 0)
                '+Recargos (costos), por rescate
                mdblSurrCost = .FieldToClass("nCostEnd", 0)
                '+Recargos (costos), por rescate
                mdblSurrCostIni = .FieldToClass("nCost", 0)
                '+Intereses por préstamos pendientes
                mdblIntLoans = .FieldToClass("nIntLoans", 0)
                '+Préstamos pendientes
                mdblLoans = .FieldToClass("nLoans", 0)
                '+Monto de retención
                mdblRetention = .FieldToClass("nRetention", 0)
                '+% de retención
                mdblnRet_Pct = .FieldToClass("nRetention_pct", 0)
                '+Monto diposnible de rescate para la póliza
                mdblSurr = .FieldToClass("nSurrVal", 0)
                '+Monto solicitado de rescate
                mdblSurrPrtlTran = .FieldToClass("nSurrent", 0)
                '+Moneda de la póliza
                mlngCurrency = .FieldToClass("nCurrency", 0)
                '+Indicador de APV (1.-Si , 2.-No)
                mstrApv = .FieldToClass("sApv")
                '+ % de recargo sobre rescates parciales
                mdblPct_charge = .FieldToClass("nPct_charge")
                '+ Monto fijo de recargo sobre rescates parciales
                mdblFix_charge = .FieldToClass("nFix_charge")
                '+ Monto Máximo de recargo sobre rescates parciales
                mdblMaxChargSurr = .FieldToClass("nMaxChargSurr")
                '+ Factor de cambio a la fecha
                mdblExchange = .FieldToClass("nExchange")
                nExchange_aux = .FieldToClass("nExchange")
                '+ Monto máximo de prestamo
                mdblAmomaxloans = .FieldToClass("nAmomaxloans")
                '+ Monto máximo de prestamo en moneda local
                mdblAmomaxloans_loc = .FieldToClass("nAmomaxloans_loc")
                '+ Interes anual por préstamos
                mdblInterest = .FieldToClass("nInterest")
                mdblOrigin = .Parameters("nOrigin").Value
                dPaymentdate = .FieldToClass("dPaymentDate")
                nPolicyDuration = .FieldToClass("nPolicyDuration")
                nWDCount = .FieldToClass("nWDCount")
                nSaapv = .FieldToClass("nSaapv")
                sClientInstitution = .FieldToClass("sClientInstitution")
                nRet_Pct = .FieldToClass("nRetention_pct", 0)

                mdblCost_cov_dev = .FieldToClass("nCost_cov_dev")
                mdblRentability = .FieldToClass("nRentability")
                mdblAmount_rec_dev = .FieldToClass("nAmount_rec_dev")
                mdblAmount_dev = .FieldToClass("nAmount_dev")



                .RCloseRec()
            Else
                InsPreVI7000 = False
                mlngType_payment = 0
                mlngInstitution = 0
                Me.sClient = ""
                mdblRoutSurr = 0
                mdblSurrCost = 0
                mdblSurrCostIni = 0
                mdblIntLoans = 0
                mdblLoans = 0
                mdblRetention = 0
                mdblnRet_Pct = 0
                mdblSurr = 0
                mdblSurrPrtlTran = 0
                mlngCurrency = 0
                mstrApv = ""
                mdblCost_cov_dev = 0
                mdblRentability = 0
                mdblAmount_rec_dev = 0
                mdblAmount_dev = 0

                mlngSurr_reason = nSurr_reason
                mdblOrigin = nOrigin
                dPaymentdate = eRemoteDB.Constants.dtmNull
                nRet_Pct = 0
                sClientInstitution = ""
            End If
        End With

InsPreVI7000_Err:
        If Err.Number Then
            InsPreVI7000 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPrevi7000 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPrevi7000 = Nothing
    End Function


    '% InsPreVI009_K : Permite predefinir los valores utilizados en VI009(Cabezera)
    Public Function InsPreVI009_K(ByVal sTyp_surr As String, ByVal sCodisplori As String, ByVal nOperat As Integer) As Boolean
        Dim lstrCodispl As String

        On Error GoTo InsPreVI009_K_Err

        If sTyp_surr = String.Empty Then
            mintTyp_surr = 1
        Else
            mintTyp_surr = CShort(sTyp_surr)
        End If

        If sCodisplori = String.Empty Then
            mstrScriptCancel = "return true;"
        Else
            If sCodisplori = "CA767" Then
                lstrCodispl = "CA099"
            Else
                lstrCodispl = sCodisplori
            End If
            mstrScriptCancel = "top.document.location.href='/VTimeNet/common/GoTo.aspx?sCodispl=" & lstrCodispl & "';"
        End If

        mintProcessType = 1
        If sCodisplori = "CA767" Then
            If nOperat = 2 Then
                mintProcessType = 2
            End If
        End If

InsPreVI009_K_Err:
        If Err.Number Then
            InsPreVI009_K = False
        End If
        On Error GoTo 0
    End Function

    '% InsPreVI009 : Permite predefinir los valores utilizados en VI009
    Public Function InsPreVI009(ByVal sTyp_surr As String, ByVal sProctype As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal nOperat As Integer = eRemoteDB.Constants.intNull, Optional ByVal nTypepay As Integer = eRemoteDB.Constants.intNull, Optional ByVal nProponum As Double = eRemoteDB.Constants.intNull, Optional ByVal sCodispl As String = "") As Boolean
        InsPreVI009 = InsGetDataVI009(sTyp_surr, sProctype, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nOperat, nTypepay, nProponum, sCodispl)
    End Function

    '% InsGetDataVI009: Obtiene los datos a mostrar en la ventana de rescate
    Private Function InsGetDataVI009(ByVal sTyp_surr As String, ByVal sProctype As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nOperat As Integer, ByVal nTypepay As Integer, ByVal nProponum As Double, ByVal sCodispl As String) As Boolean
        Dim lrecInsPreVI009 As eRemoteDB.Execute

        On Error GoTo InsPreVI009_Err
        lrecInsPreVI009 = New eRemoteDB.Execute
        '+ Definición de store procedure InsPreVI009 al 02-21-2003 12:39:29
        With lrecInsPreVI009
            .StoredProcedure = "InsPreVI009"
            .Parameters.Add("sTyp_surr", sTyp_surr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProctype", sProctype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOperat", nOperat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypepay", nTypepay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurrValue", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                mblnProdAL = .FieldToClass("nProdClas") = 7
                mdblVP = .FieldToClass("nVp", 0)
                mdblSurrCost = .FieldToClass("nSurrcost", 0)
                mdblSurr = .FieldToClass("nSurr", 0)
                mdblRoutSurr = .FieldToClass("nRoutsurr", 0)
                sClient = .FieldToClass("sClient")
                mdblSaldCapital = .FieldToClass("nSaldcapital", 0)
                mdblSaldPremium = .FieldToClass("nSaldpremium", 0)
                mdblExchange = .FieldToClass("nExchange", 0)
                mdblIntLoans = .FieldToClass("nIntloans", 0)
                mdblLoans = .FieldToClass("nLoans", 0)
                mdblBalance = .FieldToClass("nBalance", 0)
                mlngCurrency = .FieldToClass("nCurrency")
                mstrRequest = .FieldToClass("sRequest")
                mstrCertPaySurr = .FieldToClass("sCertpaysurr")
                mlngBraPaySurr = .FieldToClass("nBrapaysurr")
                mlngProPaySurr = .FieldToClass("nPropaysurr")
                mlngPolPaySurr = .FieldToClass("nPolpaysurr")
                mlngCerPaySurr = .FieldToClass("nCerpaysurr")
                mlngNotenum = .FieldToClass("nNotenum")
                mdblAmosurren = .FieldToClass("nAmosurren", 0)

                mdblSurrValue = .FieldToClass("nSurrValue")
                mdblRescDef = .FieldToClass("nRescDef")
                mdblSurrCostPar = .FieldToClass("nSurrCostPar")
                mdblAmorescpar = .FieldToClass("nAmorescpar")

                mstrReh_lrec = .FieldToClass("sReh_lrec")
                mstrNull_rec = .FieldToClass("sNull_rec")
                mdtmEffecdate = .FieldToClass("dEffecdate")

                mdblAmomaxloans = .FieldToClass("nAmomaxloans")
                mdblInterest = .FieldToClass("nInterest")
                mdblAmomaxloans_loc = .FieldToClass("nAmomaxloans_loc")
                mdblSurrvalue_loc = .FieldToClass("nSurrvalue_loc")

                mdblTax = .FieldToClass("nTax")
                mdblTax_Rent = .FieldToClass("nTax_Rent")
                nYear = .FieldToClass("nYear")
                nMonth = .FieldToClass("nMonth")
                dPaymentdate = .FieldToClass("dPaymentDate")
                nPremium = .FieldToClass("nPremium")
                .RCloseRec()

            End If
        End With

InsPreVI009_Err:
        If Err.Number Then
            InsGetDataVI009 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPreVI009 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPreVI009 = Nothing
        On Error GoTo 0
    End Function

    '% DefaultValueVI7000 : Permite recuperar los valores predefinidos para VI7000
    Public Function DefaultValueVI7000(ByVal strKey As String, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As Object
        Dim varAux As Object = New Object

        Select Case strKey
            Case "tcnTotWthdr"
                varAux = mdblRoutSurr
            Case "tcnCoverCost"
                varAux = mdblSurrCost
            Case "hddnRetention"
                varAux = mdblRetention
            Case "hddnRet_Pct"
                varAux = mdblnRet_Pct
            Case "tcnSurrAmt"
                varAux = mdblSurrPrtlTran
            Case "cbeEntFinDes"
                varAux = mlngInstitution
            Case "dtcClient"
                varAux = sClient
            Case "cbeSurrReas"
                varAux = mlngSurr_reason
            Case "cbePmtOrd"
                varAux = mlngType_payment
            Case "cbeCurrency"
                varAux = mlngCurrency
            Case "tcnAvailBal"
                varAux = mdblRoutSurr
            Case "tcnSurrCost"
                varAux = mdblSurrCost
            Case "tcnSurrCostIni"
                varAux = mdblSurrCostIni
            Case "tcnLoans"
                varAux = mdblLoans
            Case "tcnInterest"
                varAux = mdblIntLoans
            Case "tcnRetention"
                varAux = mdblRetention
            Case "tcnSurrVal"
                varAux = mdblSurr
            Case "sApv"
                varAux = mstrApv
            Case "tcnPct_charge"
                varAux = mdblPct_charge
            Case "tcnFix_charge"
                varAux = mdblFix_charge
            Case "tcnMaxChargSurr"
                varAux = mdblMaxChargSurr
            Case "tcnExchange"
                varAux = mdblExchange
            Case "tcnMaxAmount"
                varAux = mdblAmomaxloans
            Case "tcnMaxAmountLocal"
                varAux = mdblAmomaxloans_loc
            Case "tcnInter_year"
                varAux = mdblInterest
            Case "cbeOrigin"
                varAux = mdblOrigin
            Case "tcnCost_cov_dev"
                varAux = mdblCost_cov_dev
            Case "tcnRentability"
                varAux = mdblRentability
            Case "tcnAmount_rec_dev"
                varAux = mdblAmount_rec_dev
            Case "tcnAmount_dev"
                varAux = mdblAmount_dev
        End Select
        Return varAux
    End Function
    '% DefaultValueVI009 : Permite recuperar los valores predefinidos para VI009
    Public Function DefaultValueVI009(ByVal strKey As String, Optional ByVal nProponum As Double = eRemoteDB.Constants.intNull) As Object
        Dim lblnValidateZero As Boolean
        Dim caseResult As Object

        '+ Por si acaso se dejan todas las variables que llegen en nulo en 0
        If mlngBraPaySurr = eRemoteDB.Constants.intNull Then mlngBraPaySurr = 0
        If mlngProPaySurr = eRemoteDB.Constants.intNull Then mlngProPaySurr = 0
        If mlngPolPaySurr = eRemoteDB.Constants.intNull Then mlngPolPaySurr = 0
        If mlngCerPaySurr = eRemoteDB.Constants.intNull Then mlngCerPaySurr = 0
        If mdblVP = eRemoteDB.Constants.intNull Then mdblVP = 0
        If mdblSurrCost = eRemoteDB.Constants.intNull Then mdblSurrCost = 0
        If mdblLoans = eRemoteDB.Constants.intNull Then mdblLoans = 0
        If mdblIntLoans = eRemoteDB.Constants.intNull Then mdblIntLoans = 0
        If mdblAmosurren = eRemoteDB.Constants.intNull Then mdblAmosurren = 0
        If mdblSurr = eRemoteDB.Constants.intNull Then mdblSurr = 0
        If mdblRoutSurr = eRemoteDB.Constants.intNull Then mdblRoutSurr = 0
        If mdblSaldCapital = eRemoteDB.Constants.intNull Then mdblSaldCapital = 0
        If mdblSaldPremium = eRemoteDB.Constants.intNull Then mdblSaldPremium = 0
        If mdblBalance = eRemoteDB.Constants.intNull Then mdblBalance = 0
        If mdblExchange = eRemoteDB.Constants.intNull Then mdblExchange = 0

        If mdblSurrValue = eRemoteDB.Constants.intNull Then mdblSurrValue = 0
        If mdblAmorescpar = eRemoteDB.Constants.intNull Then mdblAmorescpar = 0
        If mdblRescDef = eRemoteDB.Constants.intNull Then mdblRescDef = 0
        If mdblSurrCostPar = eRemoteDB.Constants.intNull Then mdblSurrCostPar = 0

        If mdblAmomaxloans = eRemoteDB.Constants.intNull Then mdblAmomaxloans = 0
        If mdblInterest = eRemoteDB.Constants.intNull Then mdblInterest = 0
        If mdblAmomaxloans_loc = eRemoteDB.Constants.intNull Then mdblAmomaxloans_loc = 0
        If mdblSurrvalue_loc = eRemoteDB.Constants.intNull Then mdblSurrvalue_loc = 0

        If mdblTax = eRemoteDB.Constants.intNull Then mdblTax = 0
        If mdblTax_Rent = eRemoteDB.Constants.intNull Then mdblTax_Rent = 0

        Select Case strKey
            Case "tcnRescDef"
                caseResult = mdblRescDef

            Case "tcnSurrCostPar"
                caseResult = mdblSurrCostPar

            Case "isActiveLife"
                caseResult = mblnProdAL

            Case "tcnVP"
                caseResult = mdblVP
                lblnValidateZero = True

            Case "tcnSurrCost"
                caseResult = mdblSurrCost

            Case "tcnSurrAmount"
                If nProponum = eRemoteDB.Constants.intNull Then
                    caseResult = mdblAmorescpar
                Else
                    caseResult = mdblSurr
                End If
                lblnValidateZero = True

            Case "tcnSurrVal"
                caseResult = mdblSurrValue
                lblnValidateZero = True

            Case "tcnClient"
                caseResult = sClient

            Case "tcnSaldCap"
                caseResult = mdblSaldCapital

            Case "tcnSaldPrem"
                caseResult = mdblSaldPremium

            Case "tcnExchange"
                caseResult = mdblExchange

            Case "tcnInterest"
                caseResult = mdblIntLoans

            Case "tcnSurrCurr"
                'caseResult = caseResult("tcnRescDef", nProponum) * mdblExchange
                caseResult = mdblRescDef * mdblExchange

                caseResult = System.Math.Round(caseResult, 0)
                lblnValidateZero = True

            Case "hddnBalance"
                caseResult = mdblBalance

            Case "nCurrency"
                caseResult = mlngCurrency

            Case "chkRequest"
                caseResult = mstrRequest

            Case "sTyp_surr"
                caseResult = mintTyp_surr

            Case "sScriptCancel"
                caseResult = mstrScriptCancel

            Case "sProcessType"
                caseResult = mintProcessType

            Case "sCertPaySurr"
                caseResult = mstrCertPaySurr

            Case "nBraPaySurr"
                caseResult = mlngBraPaySurr

            Case "nProPaySurr"
                caseResult = mlngProPaySurr

            Case "nPolPaySurr"
                caseResult = mlngPolPaySurr

            Case "nCerPaySurr"
                caseResult = mlngCerPaySurr

            Case "nNotenum"
                caseResult = mlngNotenum

            Case "dEffecdate"
                caseResult = mdtmEffecdate

            Case "sReh_lrec"
                caseResult = mstrReh_lrec

            Case "sNull_rec"
                caseResult = mstrNull_rec

            Case "nLoans"
                caseResult = mdblLoans
                lblnValidateZero = True

            Case "nSurrBefore"
                caseResult = mdblAmosurren
                lblnValidateZero = True

                '%Monto maximo del prestamo
            Case "tcnMaxAmount"
                caseResult = mdblAmomaxloans

                '%Interes anual del prestamo
            Case "tcnInter_year"
                caseResult = mdblInterest

                '%Monto maximo del prestamo en moneda local
            Case "tcnMaxAmountLocal"
                caseResult = mdblAmomaxloans_loc

                '%Valor del rescate en moneda local
            Case "tcnSurrvalue_loc"
                caseResult = mdblSurrvalue_loc

                '%Valor del impuesto aplicado al rescate
            Case "tcnTaxSurr"
                caseResult = mdblTax

                '%Monto calculado para retener del rescate
            Case "tcnSurrValue_Tax"
                caseResult = mdblTax_Rent

        End Select
        If lblnValidateZero Then
            If caseResult < 0 Then
                caseResult = 0
            End If
        End If
        Return caseResult
    End Function

    '%InsValVI009: Esta función se encarga de validar los datos introducidos en la forma VI009(Folder).
    Public Function InsValVI009(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nSurrAmount As Double, ByVal sSurrType As String, ByVal nPayWay As Integer, Optional ByVal sClientpay As String = "", Optional ByVal nBranchpay As Integer = 0, Optional ByVal nProductpay As Integer = 0, Optional ByVal nPolicypay As Double = 0, Optional ByVal nCertifpay As Double = 0, Optional ByVal sCompanyType As String = "", Optional ByVal nSurrVal As Double = 0, Optional ByVal nPolicyValue As Double = 0) As String
        Dim lclsActivelife As Activelife
        Dim lclsTab_Activelife As eProduct.Tab_ActiveLife
        Dim lclsPolicy As Policy
        Dim lclsCertificat As Certificat
        Dim lobjErrors As eFunctions.Errors
        Dim lclsProduct As eProduct.Product
        Dim lclsClient As eClient.Client
        Dim lblnError As Boolean
        Dim ldblamountmax As Double

        '- Saldo de capital tras rescate
        Dim ldblRemainK As Double

        On Error GoTo InsValVI009_Err
        lobjErrors = New eFunctions.Errors


        '+ Rescate parcial
        If sSurrType = "2" Then

            '+ Monto de rescate debe ser menor a valor de rescate
            If nSurrAmount > nSurrVal Then
                lobjErrors.ErrorMessage(sCodispl, 60308)
            End If

            lclsProduct = New eProduct.Product
            With lclsProduct
                Call .FindProduct_li(nBranch, nProduct, dEffecdate)

                '+ Monto de rescate debe estar entre maximo y minimo permitido
                If (nSurrAmount > .nAmaxsurr And .nAmaxsurr <> eRemoteDB.Constants.intNull) Or (nSurrAmount < .nAminsurr And .nAminsurr <> eRemoteDB.Constants.intNull) Then
                    lobjErrors.ErrorMessage(sCodispl, 60309, , , "(" & Str(.nAminsurr) & " - " & Str(.nAmaxsurr) & " )")
                End If

                '+ Monto de rescate debe ser menor a procentaje de valor rescate definido
                ' se elimina esta validaciónm por que el nSurrVal ya es el monto de porcentaje del producto
                '            If .nPervssurr <> NumNull Then
                '                ldblamountmax = nSurrVal * .nPervssurr / 100
                '                If nSurrAmount > ldblamountmax Then
                '                    lobjErrors.ErrorMessage sCodispl, 60310, , , "(" + Str$(.nPervssurr) + " % correspondiente a un monto máximo = " + Str$(ldblamountmax) + ")"
                '                End If
                '            End If

            End With
            'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsProduct = Nothing
        End If

        '+ Se valida la existencia del cliente si la forma de pago lo require
        If nPayWay = Certificat.eSurrPayWay.eSurrPayOrder Or nPayWay = Certificat.eSurrPayWay.eSurrPayBankAccLoad Or nPayWay = Certificat.eSurrPayWay.eSurrPayClientAccLoad Then
            If sClientpay = String.Empty Then
                lobjErrors.ErrorMessage(sCodispl, 12043)
            Else
                lclsClient = New eClient.Client
                If Not lclsClient.Find(sClientpay) Then
                    lobjErrors.ErrorMessage(sCodispl, 1007)
                Else
                    '+ Se valida que el cliente no este muerte o bloquedo
                    If lclsClient.dDeathdat <> eRemoteDB.Constants.dtmNull Then
                        lobjErrors.ErrorMessage(sCodispl, 2051)
                    End If
                End If
                'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsClient = Nothing
            End If

            '+ Se valida la poliza suministrada si el pago va a la cta cte de esta
        ElseIf nPayWay = Certificat.eSurrPayWay.eSurrPayPolicyAccLoad Then

            If nBranchpay = eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 1022)
                lblnError = True
            End If

            If nProductpay = eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 1014)
                lblnError = True
            End If
            If nPolicypay = eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 3003)
                lblnError = True
            End If

            If Not lblnError Then
                lclsPolicy = New Policy
                With lclsPolicy
                    If Not .FindPolicyOfficeName("2", nBranchpay, nProductpay, nPolicypay, sCompanyType) Then
                        lobjErrors.ErrorMessage(sCodispl, 3001)
                        lblnError = True
                    End If
                End With
                'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsPolicy = Nothing
            End If

            If nCertifpay = eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 3006)
                lblnError = True
            End If

            If Not lblnError Then
                lclsCertificat = New Certificat
                With lclsCertificat
                    If Not .Find("2", nBranchpay, nProductpay, nPolicypay, nCertifpay) Then
                        lobjErrors.ErrorMessage(sCodispl, 3010)
                    End If
                End With
                'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsCertificat = Nothing
            End If
        End If

        InsValVI009 = lobjErrors.Confirm

InsValVI009_Err:
        If Err.Number Then
            InsValVI009 = "InsValVI009: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsActivelife = Nothing
        'UPGRADE_NOTE: Object lclsTab_Activelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_Activelife = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing
    End Function

    '%InsPostVI7000: Se realiza la actualización de los datos en la ventana VI7000 'Rescate de poliza'
    Public Function InsPostVI7000(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sSurrAll As String, ByVal nSurrTot As Double, ByVal nSurrCost As Double, ByVal nRetention As Double, ByVal nCurrency As Integer, ByVal nPmtOrder As Integer, ByVal nUsercode As Integer, ByVal sClient As String, ByVal nEntFinDes As Integer, ByVal nSurrReas As Integer, ByVal sProcessType As String, ByVal nProponum As Double, ByVal nRequest_nu As Integer, ByVal nAgency As Integer, Optional ByVal sRequest As String = "1", Optional ByVal dRetirement As Date = eRemoteDB.Constants.dtmNull, Optional ByVal sPolicyClient As String = "", Optional ByVal nOrigin As Double = eRemoteDB.Constants.intNull, Optional ByVal sInd_Insur As String = "2", Optional ByVal dPaymentdate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal bIsCancelling As Boolean = False) As Boolean

        Dim lclsClient As eClient.Client
        Dim ldtmRetirement As Date

        Dim lclsSurr_origins As Surr_origins

        '+ Si el rescate es definitivo, se registran las cuentas origen afectadas
        If sProcessType = "2" Then
            lclsSurr_origins = New Surr_origins

            Call lclsSurr_origins.Cre_Surr_Origins(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nSurrReas, IIf(sSurrAll = "1", "1", "2"), nProponum, sProcessType)
            'UPGRADE_NOTE: Object lclsSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsSurr_origins = Nothing
        End If

        InsPostVI7000 = InsPolicySurrender(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, IIf(sSurrAll = "1", "1", "2"), sRequest, eRemoteDB.Constants.intNull, nSurrTot, nCurrency, sClient, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nProponum, IIf(nProponum = eRemoteDB.Constants.intNull, "2", "1"), eRemoteDB.Constants.intNull, nUsercode, nAgency, nRequest_nu, sProcessType, eRemoteDB.Constants.intNull, "2", "VI7000", nSurrCost, nRetention, nEntFinDes, nSurrReas, nPmtOrder, nOrigin, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, sInd_Insur, dPaymentdate)

        '+ Si el rescate es Preliminar, se registran las cuentas origen afectadas
        '+ para la propuesta, a partir de la tabla temporal
        If sProcessType = "1" Then
            lclsSurr_origins = New Surr_origins

            Call lclsSurr_origins.Cre_Surr_Origins("8", nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nSurrReas, IIf(sSurrAll = "1", "1", "2"), Me.nProposal, sProcessType)
            'UPGRADE_NOTE: Object lclsSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsSurr_origins = Nothing
        End If

        If dRetirement <> eRemoteDB.Constants.dtmNull Then
            If IsDate(dRetirement) Then
                ldtmRetirement = dRetirement
                lclsClient = New eClient.Client
                With lclsClient
                    Call .Find(sPolicyClient)
                    If .UpdateBC001N(.sClient, .dInpdate, .sCuit, .sLastName, .sFirstName, .nCivilsta, .sSexclien, .nTitle, .nNationality, .nSpeciality, .dBirthdat, .dDriverdat, .sLicense, .sCredit_card, .sBlockade, .dDeathdat, .sLastname2, .nArea, .dDrivexpdat, .nTypDriver, .nLimitdriv, .nHealth_org, .nAfp, .dWedd, .sBill_ind, ldtmRetirement, .dIndependant, .dDependant, .sSmoking, .sFatca, .sPEP, .sUsPerson, nUsercode, .sCRS) Then
                        InsPostVI7000 = True
                    End If
                End With
                'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsClient = Nothing
            End If
        End If

    End Function

    '%InsPostVI7004: Se realiza la actualización de los datos en la ventana VI7004 'Rescate de poliza APV'
    Public Function InsPostVI7004(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sSurrAll As String, ByVal nSurrTot As Double, ByVal nSurrCost As Double, ByVal nRetention As Double, ByVal nCurrency As Integer, ByVal nPmtOrder As Integer, ByVal nUsercode As Integer, ByVal sClient As String, ByVal nEntFinDes As Integer, ByVal nSurrReas As Integer, ByVal sProcessType As String, ByVal nProponum As Double, ByVal nRequest_nu As Integer, ByVal nAgency As Integer, Optional ByVal sRequest As String = "1", Optional ByVal dRetirement As Date = eRemoteDB.Constants.dtmNull, Optional ByVal sPolicyClient As String = "", Optional ByVal nOrigin As Double = eRemoteDB.Constants.intNull, Optional ByVal sInd_Insur As String = "2", Optional ByVal dPaymentdate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal bIsCancelling As Boolean = False, Optional ByVal nTyp_Profitworker As Integer = 0, Optional ByVal nSaapv As Double = 0) As Boolean

        Dim lclsClient As eClient.Client
        Dim ldtmRetirement As Date

        Dim lclsSurr_origins As Surr_origins

        '+ Si el rescate es definitivo, se registran las cuentas origen afectadas
        If sProcessType = "2" Then
            lclsSurr_origins = New Surr_origins

            Call lclsSurr_origins.Cre_Surr_Origins(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nSurrReas, IIf(sSurrAll = "1", "1", "2"), nProponum, sProcessType, nSaapv)
            'UPGRADE_NOTE: Object lclsSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsSurr_origins = Nothing
        End If

        InsPostVI7004 = InsPolicySurrender(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, IIf(sSurrAll = "1", "1", "2"), sRequest, eRemoteDB.Constants.intNull, nSurrTot, nCurrency, sClient, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nProponum, IIf(nProponum = eRemoteDB.Constants.intNull, "2", "1"), eRemoteDB.Constants.intNull, nUsercode, nAgency, nRequest_nu, sProcessType, eRemoteDB.Constants.intNull, "2", "VI7004", nSurrCost, nRetention, nEntFinDes, nSurrReas, nPmtOrder, nOrigin, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, sInd_Insur, dPaymentdate, nTyp_Profitworker)

        '+ Si el rescate es Preliminar, se registran las cuentas origen afectadas
        '+ para la propuesta, a partir de la tabla temporal
        If sProcessType = "1" Then
            lclsSurr_origins = New Surr_origins

            Call lclsSurr_origins.Cre_Surr_Origins("8", nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nSurrReas, IIf(sSurrAll = "1", "1", "2"), Me.nProposal, sProcessType, nSaapv)
            'UPGRADE_NOTE: Object lclsSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsSurr_origins = Nothing
        End If

        If dRetirement <> eRemoteDB.Constants.dtmNull Then
            If IsDate(dRetirement) Then
                ldtmRetirement = dRetirement
                lclsClient = New eClient.Client
                With lclsClient
                    Call .Find(sPolicyClient)
                    If .UpdateBC001N(.sClient, .dInpdate, .sCuit, .sLastName, .sFirstName, .nCivilsta, .sSexclien, .nTitle, .nNationality, .nSpeciality, .dBirthdat, .dDriverdat, .sLicense, .sCredit_card, .sBlockade, .dDeathdat, .sLastname2, .nArea, .dDrivexpdat, .nTypDriver, .nLimitdriv, .nHealth_org, .nAfp, .dWedd, .sBill_ind, ldtmRetirement, .dIndependant, .dDependant, .sSmoking, .sFatca, .sPEP, .sUsPerson, nUsercode, .sCRS) Then
                        InsPostVI7004 = True
                    End If
                End With
                'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsClient = Nothing
            End If
        End If

    End Function

    '%InsPostVI009: Se realiza la actualización de los datos en la ventana VI009 'Rescate de poliza'
    Public Function InsPostVI009(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sSurrType As String, ByVal sProcessType As String, ByVal sRequest As String, ByVal nTypepay As Integer, ByVal nSurrAmount As Double, ByVal nCurrency As Integer, ByVal sClientpay As String, ByVal nBranchpay As Integer, ByVal nProductpay As Integer, ByVal nPolicypay As Double, ByVal nCertifpay As Double, ByVal nProposal As Double, ByVal nBalance As Double, ByVal nOperat As Double, ByVal nUsercode As Integer, ByVal nAgency As Integer, ByVal nRequest_nu As Integer, ByVal nNotenum As Integer, ByVal sAnulReceipt As String, Optional ByVal nTax As Double = 0, Optional ByVal nTax_Rent As Double = 0, Optional ByVal dPaymentdate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal nPremium As Double = 0, Optional ByVal nSurrVal As Double = 0, Optional ByVal nLoans As Double = 0, Optional ByVal nInterest As Double = 0, Optional ByVal nSurrCostPar As Double = 0) As Boolean
        Dim lclsSurr_origins As Surr_origins

        lclsSurr_origins = New Surr_origins

        '+ Si el tipo de rescate el Total o ya existe en la tabla temporal,
        '+ se insertan o actualizan directamente en la tabla temporal.
        Call lclsSurr_origins.CreT_Surr_Origins(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 4, nSurrVal, nSurrAmount, nSurrCostPar, 0, nUsercode, 1, sSurrType, nSurrAmount, nSurrCostPar, eRemoteDB.Constants.intNull, nPremium, nLoans, nInterest, dPaymentdate)

        InsPostVI009 = InsPolicySurrender(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sSurrType, sRequest, nTypepay, nSurrAmount, nCurrency, sClientpay, nBranchpay, nProductpay, nPolicypay, nCertifpay, nProposal, IIf(nOperat = 2, "1", "2"), nBalance, nUsercode, nAgency, nRequest_nu, sProcessType, nNotenum, sAnulReceipt, "VI009", , , , , , , nTax, nTax_Rent, "2", dPaymentdate)
    End Function
    '%InsPolicySurrender: Ejecuta el rescate de polizas
    Private Function InsPolicySurrender(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sSurrType As String, ByVal sRequest As String, ByVal nTypepay As Integer, ByVal nSurrAmount As Double, ByVal nCurrency As Integer, ByVal sClientpay As String, ByVal nBranchpay As Integer, ByVal nProductpay As Integer, ByVal nPolicypay As Double, ByVal nCertifpay As Double, ByVal nProposal As Double, ByVal sAprob As String, ByVal nBalance As Double, ByVal nUsercode As Integer, ByVal nAgency As Integer, ByVal nRequest_nu As Integer, ByVal sProcessType As String, ByVal nNotenum As Integer, ByVal sAnulReceipt As String, ByVal sCodispl As String, Optional ByVal nSurrCost As Double = 0, Optional ByVal nRetention As Double = 0, Optional ByVal nEntFinDes As Integer = 0, Optional ByVal nSurrReas As Integer = 0, Optional ByVal nType_payment As Integer = 0, Optional ByVal nOrigin As Double = 0, Optional ByVal nTax As Double = 0, Optional ByVal nTax_Rent As Double = 0, Optional ByVal sInd_Insur As String = "2", Optional ByVal dPaymentdate As Date = #12:00:00 AM#, Optional ByVal nTyp_Profitworker As Integer = 0) As Boolean
        Dim lrecInsPolicySurrender As eRemoteDB.Execute

        On Error GoTo InsPolicySurrender_Err
        lrecInsPolicySurrender = New eRemoteDB.Execute

        '+ Definición de store procedure InsPolicySurrender al 08-17-2002 14:17:13
        With lrecInsPolicySurrender
            .StoredProcedure = "InsPolicySurrender"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSurrtype", sSurrType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypepay", nTypepay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurramount", nSurrAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientpay", sClientpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranchpay", nBranchpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProductpay", nProductpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicypay", nPolicypay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertifpay", nCertifpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAprob", sAprob, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcesstype", sProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequest", sRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAnulReceipt", sAnulReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProposal", nProposal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurrcost", nSurrCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRetention", nRetention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEntfindes", nEntFinDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurrreas", nSurrReas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_payment", nType_payment, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("norigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTax", nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTax_Rent", nTax_Rent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInd_Insur", sInd_Insur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPaymentDate", dPaymentdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_Profitworker", nTyp_Profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsPolicySurrender = .Run(False)
            Me.nProposal = eRemoteDB.Constants.intNull
            If sRequest = "1" And nProposal = eRemoteDB.Constants.intNull Then
                Me.nProposal = .Parameters("nProposal").Value
            End If
        End With
InsPolicySurrender_Err:
        If Err.Number Then
            InsPolicySurrender = False
        End If
        'UPGRADE_NOTE: Object lrecInsPolicySurrender may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPolicySurrender = Nothing
        On Error GoTo 0
    End Function


    'insValidateLifeDocu: retorna si una poliza puede ser rehabilitada
    Public Function insValidateLifeDocu(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecinsRoutinerehabilitate As eRemoteDB.Execute

        On Error GoTo insValidateLifeDocu_Err

        lrecinsRoutinerehabilitate = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insRoutinerehabilitate al 10-10-2002 16:50:03
        '+
        With lrecinsRoutinerehabilitate
            .StoredProcedure = "insValidateLifeDocu"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sResult", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insValidateLifeDocu = .Parameters("sResult").Value = "1"
            Else
                insValidateLifeDocu = False
            End If
        End With

insValidateLifeDocu_Err:
        If Err.Number Then
            insValidateLifeDocu = False
        End If
        'UPGRADE_NOTE: Object lrecinsRoutinerehabilitate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsRoutinerehabilitate = Nothing
        On Error GoTo 0
    End Function

    'insRoutinerehabilitate: retorna si una poliza puede ser rehabilitada
    Public Function insRoutinerehabilitate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sRoureha As String) As Boolean
        Dim lrecinsRoutinerehabilitate As eRemoteDB.Execute

        On Error GoTo insRoutinerehabilitate_Err

        lrecinsRoutinerehabilitate = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insRoutinerehabilitate al 10-10-2002 16:50:03
        '+
        With lrecinsRoutinerehabilitate
            .StoredProcedure = "insRoutinerehabilitate"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoureha", sRoureha, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sResult", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insRoutinerehabilitate = .Parameters("sResult").Value = "1"
            Else
                insRoutinerehabilitate = False
            End If
        End With

insRoutinerehabilitate_Err:
        If Err.Number Then
            insRoutinerehabilitate = False
        End If
        'UPGRADE_NOTE: Object lrecinsRoutinerehabilitate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsRoutinerehabilitate = Nothing
        On Error GoTo 0
    End Function

    '%insValVI7002_k: Esta función se encarga de validar los datos introducidos en la forma (Header).
    Public Function insValVI7002_K(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCompanyType As String = "") As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalVI7002_K As eRemoteDB.Execute

        On Error GoTo insvalVI7002_K_Err

        lrecinsvalVI7002_K = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalVI7002_K
            .StoredProcedure = "insVI7002PKG.insvalVI7002_K"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompanyType", sCompanyType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            insValVI7002_K = .Confirm
        End With

insvalVI7002_K_Err:
        If Err.Number Then
            insValVI7002_K = "insvalVI7002_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalVI7002_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalVI7002_K = Nothing

    End Function


    '% Count_VI770: Obtiene la cantidad de registros almacenados en el arreglo [APV2] - ACM - 01/09/2003
    Public ReadOnly Property Count_VI770() As Integer
        Get
            Count_VI770 = UBound(marrVI770)
        End Get
    End Property

    '% insPreVI770: Realiza la búsqueda de información a ser mostrada en la ventana VI770 [APV2] - ACM - 01/09/2003
    Public Function insPreVI770(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean

        Dim lrecinsprevi770 As eRemoteDB.Execute
        Dim nQMonths_AUX As Integer
        Dim nPending_cost_AUX As Integer
        Dim nCurr_pending_cost_AUX As Integer
        Dim nRequired_pending_cost_AUX As Integer
        Dim lintCount As Integer

        On Error GoTo insprevi770_Err

        lrecinsprevi770 = New eRemoteDB.Execute

        nQMonths_AUX = 0
        nPending_cost_AUX = 0
        nCurr_pending_cost_AUX = 0
        nRequired_pending_cost_AUX = 0

        '**+ Definition of parameters for stored procedure 'insprevi770'
        '**+ The Information was read on  01/09/2003

        '+ Definición de parámetros para stored procedure 'insprevi770'
        '+ Información leída el: 01/09/2003

        With lrecinsprevi770
            .StoredProcedure = "insprevi770_punctual"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQmonths", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPending_cost", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurr_pending_cost", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRequired_pending_cost", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                nQMonths = IIf(.Parameters("nQmonths").Value = eRemoteDB.Constants.intNull, 0, .Parameters("nQmonths").Value)
                nPending_cost = IIf(.Parameters("nPending_cost").Value = eRemoteDB.Constants.intNull, 0, .Parameters("nPending_cost").Value)
                nCurr_pending_cost = IIf(.Parameters("nCurr_pending_cost").Value = eRemoteDB.Constants.intNull, 0, .Parameters("nCurr_pending_cost").Value)
                nRequired_pending_cost = IIf(.Parameters("nRequired_pending_cost").Value = eRemoteDB.Constants.intNull, 0, .Parameters("nRequired_pending_cost").Value)
                insPreVI770 = True
            Else
                insPreVI770 = False
            End If
        End With

        With lrecinsprevi770
            .StoredProcedure = "insprevi770"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then

                ReDim marrVI770(20)
                lintCount = 1
                For lintCount = 1 To .RecordCount
                    marrVI770(lintCount).dOperDate = .FieldToClass("dOperdate")
                    marrVI770(lintCount).nType_Move = .FieldToClass("nType_move")
                    marrVI770(lintCount).nAmount = .FieldToClass("nAmount")
                    .RNext()
                Next
                ReDim Preserve marrVI770(lintCount)
                insPreVI770 = True
            Else
                insPreVI770 = False
            End If
        End With

insprevi770_Err:
        If Err.Number Then
            insPreVI770 = False
        End If

        'UPGRADE_NOTE: Object lrecinsprevi770 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsprevi770 = Nothing

        On Error GoTo 0

    End Function

    Public Function insValVI770_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValVI770_K_err

        lclsErrors = New eFunctions.Errors

        If nBranch <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1022)
        End If

        If nProduct <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1014)
        End If

        If nPolicy <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 3003)
        End If

        If nCertif < 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 3006)
        End If

        insValVI770_K = lclsErrors.Confirm

insValVI770_K_err:
        If Err.Number Then
            insValVI770_K = "insValVI770_K: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% Item_VI770: Asigna a cada propiedad de la clase el valor correspondiente en el arreglo [APV2] - ACM - 01/09/2003
    Public Function Item_VI770(ByVal nIndex As Integer) As Boolean

        On Error GoTo Item_VI770_err

        If nIndex <= UBound(marrVI770) Then
            With marrVI770(nIndex)
                Me.dOperDate = .dOperDate
                Me.nType_Move = .nType_Move
                Me.nAmount = .nAmount
            End With
            Item_VI770 = True
        Else
            Item_VI770 = False
        End If

Item_VI770_err:
        If Err.Number Then
            Item_VI770 = False
        End If
    End Function
    '%InsValCA037DB: Llamado del procedure de la validación de los campos a actualizar en la
    '                ventana CA037
    Public Function InsValCA037DB(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sBrancht As String, ByVal dEffecdate As Date, ByVal dExpirdateNew As Date, ByVal sColtimre As String, ByVal nUsercode As Integer) As String
        Dim lrecInsValCA037 As eRemoteDB.Execute

        On Error GoTo ValCA037_err
        lrecInsValCA037 = New eRemoteDB.Execute

        With lrecInsValCA037
            .StoredProcedure = "valtransca037_soat"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdateNew", dExpirdateNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColtimre", sColtimre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                InsValCA037DB = .Parameters("Arrayerrors").Value
            End If
        End With
ValCA037_err:
        If Err.Number Then
            InsValCA037DB = Err.Description
        End If
        'UPGRADE_NOTE: Object lrecInsValCA037 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValCA037 = Nothing
    End Function



    '**% UpdatePremium: This routine anulls the pending premium invoices of the policy or certificate
    '% UpdatePremium: Anula los recibos pendientes de la póliza o certificado
    Public Function UpdatePartic_data(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nNullcode As Integer, ByVal dNulldate As Date, ByVal nUsercode As Integer) As Boolean

        '**- Variable definition. lrecupdPremiunpre
        '- Se define la variable lrecupdPremiunpre

        Dim lrecUpdatePartic_data As eRemoteDB.Execute
        lrecUpdatePartic_data = New eRemoteDB.Execute

        '**+Stored procedure parameters definition 'insudb.updPremiunpre'
        '**+Data of 01/04/2001 14:17:11
        '+ Definición de parámetros para stored procedure 'insudb.updPremiunpre'
        '+ Información leída el 04/01/2001 14:17:11

        On Error GoTo UpdatePartic_data_Err

        With lrecUpdatePartic_data
            .StoredProcedure = "UPDPART_DATA"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdatePartic_data = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecUpdatePartic_data may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdatePartic_data = Nothing

UpdatePartic_data_Err:
        If Err.Number Then
            UpdatePartic_data = False
        End If
        On Error GoTo 0
    End Function

   
    'insValCA789_k: Esta función se encarga de validar los datos introducidos en la CA789
    Public Function insValCA789_k(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal bFind As Boolean = False) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String
        lobjErrors = New eFunctions.Errors
        Dim lrecreaCertificat_branch As eRemoteDB.Execute
        On Error GoTo insValCA789_k_Err

        lrecreaCertificat_branch = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'reaCertificat_branch'
        With lrecreaCertificat_branch
            .StoredProcedure = "insValCA789"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("sArrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage("CA789", , , , , , lstrError)
                    insValCA789_k = .Confirm()
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing
            End If
        End With

insValCA789_k_Err:
        If Err.Number Then
            insValCA789_k = "insValCA789_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    Public Function inspostCA789(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Double) As Boolean
        Dim lrecreaCertificat_branch As eRemoteDB.Execute

        lrecreaCertificat_branch = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'reaCertificat_branch'
        With lrecreaCertificat_branch
            .StoredProcedure = "inspostCA789"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            inspostCA789 = .Run(False)
        End With


        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaCertificat_branch may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificat_branch = Nothing
    End Function


    '%insPostVIL1405: Esta función se encarga de ejecutar el proceso
    '%Reverso proceso unificado de inversiones
    Public Function insPostVIL1405(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nIdproces As Integer) As Boolean
        '- Se define el objeto para el manejo de conección
        Dim lrecinsPostVIL1405 As eRemoteDB.Execute

        lrecinsPostVIL1405 = New eRemoteDB.Execute

        On Error GoTo insPostVIL1405_Err

        insPostVIL1405 = True

        With lrecinsPostVIL1405
            .StoredProcedure = "insCalVIL1405"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIdproces", nIdproces, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostVIL1405 = .Run(False)
        End With

insPostVIL1405_Err:
        If Err.Number Then
            insPostVIL1405 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPostVIL1405 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostVIL1405 = Nothing
    End Function

    '%insPostCAL815: Esta función se encarga realizar la transacción
    Public Function insPostCAL815(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nExeMode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sNullDevRec As String, ByVal sNullReceipt As String, ByVal optExecute As String, ByVal nPay_day As Integer, Optional ByVal nAgency As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0, Optional ByVal optProcess As Short = 0) As Boolean
        Dim lrecinsPostCAL815 As eRemoteDB.Execute
        On Error GoTo insPostCAL815_Err

        lrecinsPostCAL815 = New eRemoteDB.Execute
        '+
        '+ 'Definición de store procedure insPostCAL815 al 09-06-2004 12:34:16
        '+
        With lrecinsPostCAL815
            .StoredProcedure = "insPostCAL815"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNulldevrec", sNullDevRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNullreceipt", sNullReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptExecute", optExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDay_pay", nPay_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeagen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcess", optProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCAL815 = .Run(False)
            If insPostCAL815 Then
                Me.sKey = lrecinsPostCAL815.Parameters("sKey").Value
            End If

        End With

insPostCAL815_Err:
        If Err.Number Then
            insPostCAL815 = False
        End If
        'UPGRADE_NOTE: Object lrecinsPostCAL815 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCAL815 = Nothing
        On Error GoTo 0
    End Function
    '%insValCAL815: Esta función se encarga de validar los datos introducidos en la zona de detalle para
    Public Function insValCAL815(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
        '- Se define el objeto para el manejo tanto de la clase  Certificat
        '- como de la clase Policy

        Dim lobjErrors As eFunctions.Errors
        On Error GoTo insValCAL815_Err
        lobjErrors = New eFunctions.Errors

        If nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CAL815", 1022)
        End If

        If nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CAL815", 1014)
        End If

        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CAL815", 2056)
        End If

        insValCAL815 = lobjErrors.Confirm

insValCAL815_Err:
        If Err.Number Then
            insValCAL815 = "insValCAL815: " & Err.Description
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

    End Function

    '%insPostVIL1405: Esta función se encarga de ejecutar el proceso
    '%Reverso proceso unificado de inversiones
    Public Function insPostCA088(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dRecepInt As Date, ByVal dRecepInt_Comp As Date, ByVal dRecepInsu As Date, ByVal dRecepInsu_Comp As Date, ByVal nUsercode As Integer) As Boolean
        '- Se define el objeto para el manejo de conección
        Dim lrecinsPostCA088 As eRemoteDB.Execute

        lrecinsPostCA088 = New eRemoteDB.Execute

        On Error GoTo insPostCA088_Err

        insPostCA088 = True

        With lrecinsPostCA088
            .StoredProcedure = "UPDCERTIFICATCA088"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRecepInt", dRecepInt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRecepInt_Comp", dRecepInt_Comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRecepInsu", dRecepInsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRecepInsu_Comp", dRecepInsu_Comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCA088 = .Run(False)
        End With

insPostCA088_Err:
        If Err.Number Then
            insPostCA088 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPostCA088 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCA088 = Nothing
    End Function

    '% InsValCA088_K: Make the validation of the fields to be updated in the window CA088.
    '  (Reverse of policy renewal/amendment )
    '% InsValCA088_K: Realiza la validación de los campos a actualizar en la ventana CA088.
    '  (Reverso de renovación/modificación de Póliza)
    Public Function InsValCA088_K(ByVal sCodispl As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal sCompanyType As String = "") As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsValPolicySeq As ePolicy.ValPolicySeq

        Dim lblnError As Boolean
        Dim llngError As Integer
        Dim lstrDescript As String

        On Error GoTo InsValCA088_K_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors

            '+ Validate the field Line of Business
            '+ Se valida el campo Ramo

            If nBranch = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 1022)
                lblnError = True
            End If

            '+ The Product field will be validated
            '+ Se va a validar el campo producto

            If nProduct = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 1014)
                lblnError = True
            End If

            '+ The Policy field will be validated.
            '+ Se va a validar el Campo de poliza

            If nPolicy = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 3003)
                lblnError = True
            Else
                If Not lblnError Then
                    lclsPolicy = New ePolicy.Policy
                    If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy, True) Then
                        .ErrorMessage(sCodispl, 3001)
                        lblnError = True
                    Else
                        'If lclsPolicy.sPolitype = "2" Then
                        '.ErrorMessage sCodispl, 4112, , RigthAling, "(transacción no habilitada para colectivos)"
                        'Else
                        If lclsPolicy.sStatus_pol = "2" Then
                            .ErrorMessage(sCodispl, 3882)
                            lblnError = True
                        Else
                            If lclsPolicy.sStatus_pol = "3" Or lclsPolicy.sStatus_pol = "4" Then
                                .ErrorMessage(sCodispl, 80129)
                                lblnError = True
                            End If
                        End If
                        mdtmIssuedat = lclsPolicy.dStartdate
                        'End If
                    End If
                End If
            End If

            '+ The Certificate field will be validated
            '+ Se va a validar el campo de Certificado
            If nCertif = eRemoteDB.Constants.intNull And nPolicy > 0 Then
                .ErrorMessage(sCodispl, 3006)
                lblnError = True

            ElseIf nCertif <> 0 Then
                If Not lblnError Then
                    lclsCertificat = New ePolicy.Certificat
                    If Not lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                        .ErrorMessage(sCodispl, 13908)
                        lblnError = True
                    Else
                        If lclsCertificat.sStatusva = "2" Then
                            .ErrorMessage(sCodispl, 3883)
                            lblnError = True
                        ElseIf lclsCertificat.sStatusva = "3" Or lclsCertificat.sStatusva = "4" Then
                            .ErrorMessage(sCodispl, 80129)
                            lblnError = True
                        End If
                    End If
                End If
            End If

            InsValCA088_K = .Confirm
        End With

InsValCA088_K_Err:
        If Err.Number Then
            InsValCA088_K = "InsValCA088_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insValCA088: Realiza la validación de los campos a actualizar en la ventana CA088.
    '  (Suspension de Garantias)
    Public Function insValCA088(ByVal sCodispl As String, ByVal dRecepInt As Date, ByVal dRecepInt_Comp As Date, ByVal dRecepInsu As Date, ByVal dRecepInsu_Comp As Date, ByVal dDate_Origin As Date) As String

        On Error GoTo insValCA088_Err

        Dim lobjErrors As eFunctions.Errors
        Dim lobjValues As eFunctions.Values

        lobjErrors = New eFunctions.Errors
        lobjValues = New eFunctions.Values

        If dRecepInt = eRemoteDB.Constants.dtmNull And dRecepInt_Comp = eRemoteDB.Constants.dtmNull And dRecepInsu = eRemoteDB.Constants.dtmNull And dRecepInsu_Comp = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 7114, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Despacho póliza - Intermediario: ")
        End If

        If dRecepInt_Comp <> eRemoteDB.Constants.dtmNull Then
            If dRecepInt = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 7114, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Despacho póliza - Intermediario: ")
            End If
            If dRecepInsu = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 7114, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Recep. Compañía - Asegurado: ")
            End If
        End If

        If dRecepInsu <> eRemoteDB.Constants.dtmNull Then
            If dRecepInt = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 7114, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Despacho póliza - Intermediario: ")
            End If
            'If dRecepInt_Comp = dtmNull Then
            '    Call lobjErrors.ErrorMessage(sCodispl, 7114, , LeftAling, "Fecha de Recep. asistente - Intermediario: ")
            'End If
        End If

        If dRecepInsu_Comp <> eRemoteDB.Constants.dtmNull Then
            If dRecepInt = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 7114, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Despacho póliza - Intermediario: ")
            End If
            If dRecepInt_Comp = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 7114, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Recep. asistente - Intermediario: ")
            End If
            If dRecepInsu = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 7114, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Recep. Compañía - Asegurado: ")
            End If
        End If

        'If dRecepInt <> dtmNull And dRecepInt_Comp = dtmNull Then
        '    Call lobjErrors.ErrorMessage(sCodispl, 7114, , LeftAling, "Fecha de Recep. asistente - Intermediario: ")
        'End If

        'If dRecepInsu <> dtmNull And dRecepInsu_Comp = dtmNull Then
        '    Call lobjErrors.ErrorMessage(sCodispl, 7114, , LeftAling, "Fecha de Recep. Compañía - Asegurado: ")
        'End If

        'If dRecepInt = dtmNull And dRecepInt_Comp <> dtmNull Then
        '    Call lobjErrors.ErrorMessage(sCodispl, 7114, , LeftAling, "Fecha de Despacho póliza - Intermediario: ")
        'End If

        'If dRecepInsu = dtmNull And dRecepInsu_Comp <> dtmNull Then
        '    Call lobjErrors.ErrorMessage(sCodispl, 7114, , LeftAling, "Fecha de Recepción - Asegurado: ")
        'End If

        If dRecepInt <> eRemoteDB.Constants.dtmNull Then
            If dRecepInt < dDate_Origin Then
                Call lobjErrors.ErrorMessage(sCodispl, 3091, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de Despacho póliza: ")
            End If
        End If

        If dRecepInt_Comp <> eRemoteDB.Constants.dtmNull Then
            If dRecepInt_Comp < dDate_Origin Then
                Call lobjErrors.ErrorMessage(sCodispl, 80130, , , "Fecha de emisión de la póliza/certificado")
            Else
                If dRecepInt_Comp < dRecepInt Then
                    Call lobjErrors.ErrorMessage(sCodispl, 80130, , , "Fecha de Despacho póliza - Intermediario")
                End If
            End If
        End If

        If dRecepInsu <> eRemoteDB.Constants.dtmNull Then
            If dRecepInsu < dDate_Origin Then
                Call lobjErrors.ErrorMessage(sCodispl, 80131, , , "Fecha de emisión de la póliza/certificado")
            Else
                If dRecepInsu < dRecepInt Then
                    Call lobjErrors.ErrorMessage(sCodispl, 80131, , , "Fecha de Despacho póliza - Intermediario")
                End If
            End If
        End If

        If dRecepInsu_Comp <> eRemoteDB.Constants.dtmNull Then
            If dRecepInsu_Comp < dDate_Origin Then
                Call lobjErrors.ErrorMessage(sCodispl, 80132, , , "Fecha de emisión de la póliza/certificado")
            Else
                If dRecepInsu_Comp < dRecepInsu Then
                    Call lobjErrors.ErrorMessage(sCodispl, 80132, , , "Fecha de Recepción - Asegurado")
                Else
                    If dRecepInsu_Comp < dRecepInt Then
                        Call lobjErrors.ErrorMessage(sCodispl, 80132, , , "Fecha de Despacho póliza - Intermediario")
                    Else
                        If dRecepInsu_Comp < dRecepInt_Comp Then
                            Call lobjErrors.ErrorMessage(sCodispl, 80132, , , "Fecha de Recepción asistente - Intermediario")
                        End If
                    End If
                End If
            End If
        End If

        insValCA088 = lobjErrors.Confirm

insValCA088_Err:
        If Err.Number Then
            insValCA088 = "insValCA088: " & Err.Description
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
    End Function
    '%insPreCA088: Esta función se encarga de ejecutar el proceso
    '%Reverso proceso unificado de inversiones
    Public Function insPreCA088(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        '- Se define el objeto para el manejo de conección

        Dim lrecinsPostCA088 As eRemoteDB.Execute

        lrecinsPostCA088 = New eRemoteDB.Execute

        On Error GoTo insPostCA088_Err

        insPreCA088 = True

        With lrecinsPostCA088
            .StoredProcedure = "InsPreCA088"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPreCA088 = .Run(True)

            If insPreCA088 Then
                dRecepInsu = .FieldToClass("DRECEPINSU")
                sUser_dRecepInsu = .FieldToClass("SUSER_RECEPINSU")
                dRecepInsu_Comp = .FieldToClass("DRECEPINSU_COMP")
                sUser_dRecepInsu_Comp = .FieldToClass("SUSER_RECEPINSU_COMP")
                dRecepInt = .FieldToClass("DRECEPINT")
                sUser_dRecepInt = .FieldToClass("SUSER_RECEPINT")
                dRecepInt_Comp = .FieldToClass("DRECEPINT_COMP")
                sUser_dRecepInt_Comp = .FieldToClass("SUSER_RECEPINT_COMP")
            End If
        End With

insPostCA088_Err:
        If Err.Number Then
            insPreCA088 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPostCA088 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCA088 = Nothing
    End Function


    '%insValCAL978: Esta función se encarga de validar los datos introducidos en la zona de detalle para
    Public Function insValCAL978(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String

        '- Se define el objeto para el manejo tanto de la clase  Certificat
        '- como de la clase Policy

        Dim lobjErrors As eFunctions.Errors
        Dim lclsCtrol_Date As eGeneral.Ctrol_date
        On Error GoTo insValCAL978_Err
        lobjErrors = New eFunctions.Errors

        If nBranch = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CAL978", 1022)
        End If

        If nProduct = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage("CAL978", 1014)
        End If

        Dim lclsProduct As eProduct.Product
        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CAL978", 2056)
        ElseIf nBranch > eRemoteDB.Constants.intNull And nProduct > eRemoteDB.Constants.intNull Then
            lclsProduct = New eProduct.Product
            With lclsProduct
                If .Find(nBranch, nProduct, dEffecdate) Then
                    If .sReactivation <> "1" Then
                        Call lobjErrors.ErrorMessage("CAL978", 90178)
                    End If
                End If
            End With
            'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsProduct = Nothing
        End If

        If dEffecdate <> eRemoteDB.Constants.dtmNull Then
            lclsCtrol_Date = New eGeneral.Ctrol_date
            If Not lclsCtrol_Date.InsValdLedgerdat(1, dEffecdate) Then
                Call lobjErrors.ErrorMessage("CAL978", 1006)
            End If
            'UPGRADE_NOTE: Object lclsCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCtrol_Date = Nothing
        End If

        insValCAL978 = lobjErrors.Confirm

insValCAL978_Err:
        If Err.Number Then
            insValCAL978 = "insValCAL978: " & Err.Description
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function


    '%insPostCAL978: Esta función se encarga realizar la transacción
    Public Function insPostCAL978(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nExeMode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal nAgency As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0) As Boolean
        Dim lrecinsPostCAL978 As eRemoteDB.Execute
        On Error GoTo insPostCAL978_Err

        lrecinsPostCAL978 = New eRemoteDB.Execute
        '+
        '+ 'Definición de store procedure insPostCAL978 al 09-06-2004 12:34:16
        '+
        With lrecinsPostCAL978
            .StoredProcedure = "insPostCAL978"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptExecute", nExeMode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeagen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCAL978 = .Run(False)
            If insPostCAL978 Then
                Me.sKey = lrecinsPostCAL978.Parameters("sKey").Value
            End If
        End With

insPostCAL978_Err:
        If Err.Number Then
            insPostCAL978 = False
        End If
        'UPGRADE_NOTE: Object lrecinsPostCAL978 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostCAL978 = Nothing
        On Error GoTo 0
    End Function

    '%getDateLastPay: Busca la fecha del último pago de una póliza
    Public Function getDateLastPay(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Date
        Dim lrecinsgetDateLastPay As eRemoteDB.Execute
        On Error GoTo insgetDateLastPay_Err

        lrecinsgetDateLastPay = New eRemoteDB.Execute
        '+
        '+ 'Definición de store procedure insPostCAL978 al 09-06-2004 12:34:16
        '+
        With lrecinsgetDateLastPay
            .StoredProcedure = "insGetDateLastPay"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateLastPay", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                getDateLastPay = lrecinsgetDateLastPay.Parameters("dDateLastPay").Value
            End If
        End With

insgetDateLastPay_Err:
        If Err.Number Then
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            getDateLastPay = Nothing
        End If
        'UPGRADE_NOTE: Object lrecinsgetDateLastPay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsgetDateLastPay = Nothing
        On Error GoTo 0
    End Function
    '%InsValCA034A_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
    '%forma.
    Public Function InsValCA034A_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
        Dim lrecinsValCA034A As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        sCodispl = "CA034A"
        lrecinsValCA034A = New eRemoteDB.Execute

        On Error GoTo insValCA034A_k_Err

        With lrecinsValCA034A
            .StoredProcedure = "InsCA034APKG.InsValCA034A"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("sArrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    InsValCA034A_K = .Confirm()
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing
            End If
        End With

insValCA034A_k_Err:
        If Err.Number Then
            InsValCA034A_K = "insValCA034A_K: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lrecinsValCA034A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValCA034A = Nothing
        On Error GoTo 0

    End Function
	
	'%insPostCA034A: Esta función se encarga realizar la transacción
	Public Function insPostCA034A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsPostca034A As eRemoteDB.Execute
		On Error GoTo insPostca034A_Err
		lrecinsPostca034A = New eRemoteDB.Execute
		
		With lrecinsPostca034A
			.StoredProcedure = "InsCA034APKG.InsPostca034A"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostCA034A = .Run(False)
			If insPostCA034A Then
				Me.nProposal = .Parameters("nProponum").Value
				Me.sBrancht = Trim(.Parameters("sBrancht").Value)
			End If
			
		End With
		
insPostca034A_Err: 
		If Err.Number Then
			insPostCA034A = False
		End If
		'UPGRADE_NOTE: Object lrecinsPostca034A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostca034A = Nothing
		On Error GoTo 0
	End Function
End Class






