Option Strict Off
Option Explicit On
Public Class ValAgentRep
    '%-------------------------------------------------------%'
    '% $Workfile:: ValAgentRep.cls                          $%'
    '% $Author:: Gletelier                                  $%'
    '% $Date:: 17/08/09 4:49p                               $%'
    '% $Revision:: 3                                        $%'
    '%-------------------------------------------------------%'

    '+ Propiedades según la tabla en el sistema el 28/01/2000
    '+ El campo llave corresponde a nIntermed.

    '+  Column name               Type                  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
    '+  ------------------------- --------------------- ------ ----- ----- -------- ------------------ ---------------------

    Public sCertype As String 'char     14                 yes      yes                yes
    Public nBranch As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
    Public nProduct As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
    Public nPolicy As Double 'smallint 2      5     0     yes      (n/a)              (n/a)
    Public nPremium As Double 'smallint 2      5     0     yes      (n/a)              (n/a)
    Public nAmoucomm As Double 'smallint 2      5     0     yes      (n/a)              (n/a)
    Public P_SKEY As String
    Public sKey As String
    Public sIndOk As String
    Public sFile_name As String
    Public dMin_pay_date As Date
    Public dMax_pay_date As Date
    Public sMessage As String


    '% insValAGL955_K: se validan los campos de la página
    Public Function insValAGL955_K(ByVal sOptInfo As String, ByVal nYear As Integer, ByVal nMonth As Integer, Optional ByRef nContrat_Pay As Integer = 0, Optional ByRef nBranch As Integer = 0, Optional ByRef nProduct As Integer = 0, Optional ByRef nPolicy As Double = 0) As String
        Dim lclsCertificat As Object
        Dim lclsPolicy As Object
        Dim lobjErrors As eFunctions.Errors
        Dim lclsProduct As eProduct.Product

        Dim lblnValid As Boolean

        On Error GoTo insValAGL955_K_Err

        lobjErrors = New eFunctions.Errors
        lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
        lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
        lclsProduct = New eProduct.Product

        lblnValid = True

        If nYear = eRemoteDB.Constants.intNull Or nYear = 0 Then
            Call lobjErrors.ErrorMessage("AGL955", 1012, , , ": Año")
            lblnValid = False
        End If

        If nMonth = eRemoteDB.Constants.intNull Or nMonth = 0 Then
            Call lobjErrors.ErrorMessage("AGL955", 1012, , , ": Mes")
            lblnValid = False
        Else
            If nMonth > 12 Then
                Call lobjErrors.ErrorMessage("AGL955", 60290)
                lblnValid = False
            End If
        End If

        '+si ya se encuentra calculado el monto para el mes en ejecución
        If Find_Contrat_Pay(nMonth, nYear) Then
            Call lobjErrors.ErrorMessage("AGL955", 100148)
        End If

        '+ Opción por póliza
        If sOptInfo = "1" Then
            '+ Validación del Campo Ramo.
            If nBranch = eRemoteDB.Constants.intNull Then
                '+ El campo Ramo debe estar lleno
                Call lobjErrors.ErrorMessage("AGL955", 1022)
                lblnValid = False
            End If

            '+ Validación del Campo Producto.
            If nProduct = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage("AGL955", 1014)
                lblnValid = False
            End If

            '+ Validación del Campo Póliza.
            If nPolicy = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage("AGL955", 3003)
                lblnValid = False
            Else
                '+ Debe ser una póliza válida
                If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
                    Call lobjErrors.ErrorMessage("AGL955", 3001)
                    lblnValid = False
                End If
            End If

            '+ Validación de Póliza/Certificado
            If lblnValid Then
                With lclsCertificat
                    If .Find("2", nBranch, nProduct, nPolicy, 0) Then
                        '+Si el Certificado esta invalido o en captura incompleta
                        If .sStatusva = "2" Or .sStatusva = "3" Then
                            Call lobjErrors.ErrorMessage("AGL955", 750044)
                            lblnValid = False
                        Else
                            '+ La póliza no puede estar anulada
                            If .dNulldate <> dtmNull Then
                                Call lobjErrors.ErrorMessage("AGL955", 5090)
                                lblnValid = False
                                '+ La póliza no puede estar suspendida
                            Else
                                If .nSuspCount > 0 Then
                                    Call lobjErrors.ErrorMessage("AGL955", 3881)
                                    lblnValid = False
                                End If
                            End If
                        End If
                    Else
                        '+ Certificado debe estar registrado en el archivo Certificat
                        Call lobjErrors.ErrorMessage("AGL955", 3010)
                        lblnValid = False
                    End If
                End With
            End If
            'Else
            '    If nContrat_Pay = eRemoteDB.Constants.intNull Then
            '        '+ El campo Ramo debe estar lleno
            '        Call lobjErrors.ErrorMessage("AGL955", 1012, , , ": Contrato de estipendio")
            '        lblnValid = False
            '    End If
        End If

        insValAGL955_K = lobjErrors.Confirm

insValAGL955_K_Err:
        If Err.Number Then
            insValAGL955_K = "insValAGL955_K: " & Err.Description
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


    '% insPostAGL955_K: Se ejecuta el proceso de cálculo de estipendios
    Public Function insPostAGL955_K(ByVal sOptInfo As String, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nUsercode As Integer, ByVal sKey As String, Optional ByRef nContrat_Pay As Integer = 0, Optional ByRef nBranch As Integer = 0, Optional ByRef nProduct As Integer = 0, Optional ByRef nPolicy As Double = 0, Optional ByRef dEffecdate As Date = #12:00:00 AM#, Optional ByVal sProccess As String = "2") As Boolean
        Dim lrecpostagl955_k As eRemoteDB.Execute

        On Error GoTo insPostAGL955_k_Err

        lrecpostagl955_k = New eRemoteDB.Execute

        insPostAGL955_K = True

        '+Se asigna llave de proceso
        Me.sKey = Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode.ToString("000000")

        '+ Definición de parámetros para stored procedure 'insudb.inscalvactiva'
        '+ Información leída el 16/12/2001

        With lrecpostagl955_k
            .StoredProcedure = "inscalestipendio"
            .Parameters.Add("sOptInfo", sOptInfo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", Me.sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nexcp_Return", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProccess", sProccess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostAGL955_K = IIf(.Parameters("nexcp_Return").Value = 1, True, False)
            End If

            If insPostAGL955_K Then
                sKey = .Parameters("sKey").Value
            End If

        End With

insPostAGL955_k_Err:
        If Err.Number Then
            insPostAGL955_K = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecpostagl955_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecpostagl955_k = Nothing
    End Function

    '% insValAG008_K: Función de validación de campos básicos para la emisión del reporte - ACM - 16/01/2002
    '**% insValAG008_K: Basic fields Validation function. These validations are for the report execution - ACM - Jan-16-2002
    Public Function insValAGL008_K(ByVal sCodispl As String, ByVal dDateProcess As Date, ByVal nIntermediaOld As Integer, ByVal nIntermediaNew As Integer) As String
        Dim lclsErrors As New eFunctions.Errors
        Dim lclsIntermedia As eAgent.Intermedia = New eAgent.Intermedia


        On Error GoTo insValAGL008_K_err

        '+ Validación #9068: Debe estar lleno
        '**+ Validation #9068: It must be filled
        If dDateProcess = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9068)
        End If

        '+ Validación #9073: Debe estar lleno
        '**+ Validation #9073: It must be filled
        If nIntermediaOld = 0 Or nIntermediaOld = intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9073)
        End If

        '+ Validation #8068: Si este campo está lleno, debe estar registrado en el archivo de intermediarios
        '**+ Validation #8068: If this field is filled, it must be registered in the intermedaries file
        If Not lclsIntermedia.Find(nIntermediaOld) Then
            Call lclsErrors.ErrorMessage(sCodispl, 8068)
        End If

        'Si este campo está lleno, debe ser diferente al indicado como intermediario anterior  09005

        '+ Validación #9074: Debe estar lleno
        '**+ Validation #9074: It must be filled
        If nIntermediaNew = 0 Or nIntermediaNew = intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9074)
        End If

        '+ Validation #8068: Si este campo está lleno, debe estar registrado en el archivo de intermediarios
        '**+ Validation #8068: If this field is filled, it must be registered in the intermedaries file
        If Not lclsIntermedia.Find(nIntermediaNew) Then
            Call lclsErrors.ErrorMessage(sCodispl, 8068)
        End If

        '+ Validación #9005: Si los parámetros "nIntermediaOld" y "nIntermediaNew" están llenos, éstos
        '+                   no pueden ser iguales
        '**+ Validation #9005: If the parameters "nIntermediaOld" and "nIntermediaNew" are filled,
        '**+                   these parameters can not have the same value

        If nIntermediaOld = nIntermediaNew Then
            Call lclsErrors.ErrorMessage(sCodispl, 9005)
        End If

        insValAGL008_K = lclsErrors.Confirm

insValAGL008_K_err:
        If Err.Number Then
            insValAGL008_K = "insValAGL008_K: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsIntermedia = Nothing

        On Error GoTo 0

    End Function

    '% insPostAGL_008_K:
    Public Function insPostAGL008_K(ByVal dDateProcess As String, ByVal nIntermediaOld As Integer, ByVal nIntermediaNew As Integer, ByVal nFlag_CurrentAccount As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lrecRemoteDataBase As New eRemoteDB.Execute

        On Error GoTo insPostAGL008_K_err

        If nFlag_CurrentAccount = -32768 Then
            nFlag_CurrentAccount = 1
        End If

        With lrecRemoteDataBase
            .StoredProcedure = "reaCommiPol"
            .Parameters.Add("InterBefore", nIntermediaOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("InterNew", nIntermediaNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dDateProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("FlagCurr", nFlag_CurrentAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                insPostAGL008_K = True
            Else
                insPostAGL008_K = False
            End If
        End With

insPostAGL008_K_err:
        If Err.Number Then
            insPostAGL008_K = False
        End If

        'UPGRADE_NOTE: Object lrecRemoteDataBase may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecRemoteDataBase = Nothing
        On Error GoTo 0

    End Function

    Public Function insValAGL014_k(ByVal sCodispl As String, ByVal nOption As Integer, ByVal nIntermed As Integer, ByVal sClientCode As String, ByVal dStardate As Date, ByVal dEnddate As Date, ByVal nPolicy As Double) As String
        Dim lclsIntermedia As eAgent.Intermedia = New eAgent.Intermedia
        Dim lclsClient As New eClient.Client
        Dim lclsErrors As New eFunctions.Errors

        On Error GoTo insvalAGL014_k_err

        Select Case nOption
            Case 1
                '+ Se seleccionó listar préstamos de intermediarios:
                If nIntermed = 0 Or nIntermed = intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 21038)
                Else
                    If Not lclsIntermedia.Find(nIntermed) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 3634)
                    End If
                End If

            Case 2
                '+ Se seleccionó listar préstamos de un cliente:
                If sClientCode = String.Empty Then
                    Call lclsErrors.ErrorMessage(sCodispl, 4122)
                Else
                    If Not lclsClient.Find(sClientCode) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7050)
                    End If

                    Call lclsIntermedia.Find_ClientInter(sClientCode)
                    If Not lclsIntermedia.FindTypeInterm_Client(sClientCode, lclsIntermedia.nIntertyp) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 9121)
                    End If
                End If
        End Select

        If nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0 Then
            If dStardate = dtmNull Then
                '+ No se indicó número de póliza ni fecha de inicio
                Call lclsErrors.ErrorMessage(sCodispl, 9071)
            End If

            If dEnddate = dtmNull Then
                '+ No se indicó número de póliza ni fecha de fin
                Call lclsErrors.ErrorMessage(sCodispl, 9072)
            Else
                If dStardate <> dtmNull Then
                    If dStardate > dEnddate Then
                        '+ Fecha de inicio mayor a fecha fin
                        Call lclsErrors.ErrorMessage(sCodispl, 3240)
                    End If
                End If
            End If
        End If

        insValAGL014_k = lclsErrors.Confirm

insvalAGL014_k_err:
        If Err.Number Then
            insValAGL014_k = "insValAGL014_k: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsIntermedia = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing

        On Error GoTo 0

    End Function


    Public Function insPostAGL014_k(ByVal nIntermediary As Integer, ByVal sClientCode As String, ByVal nLoanType As Integer, ByVal nPayForm As Integer, ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sStatLoan As String, ByVal dStardate As Date, ByVal dEnddate As Date, ByVal nLoan As Integer) As Boolean
        Dim lrecRemoteDataBase As New eRemoteDB.Execute

        On Error GoTo insPostAGL014_k_err

        With lrecRemoteDataBase
            .StoredProcedure = "reaLoans_int_agl014"
            .Parameters.Add("nIntermed", nIntermediary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClientCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeLoan", nLoanType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayForm", nPayForm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                insPostAGL014_k = True
            Else
                insPostAGL014_k = False
            End If
        End With

insPostAGL014_k_err:
        If Err.Number Then
            insPostAGL014_k = False
        End If

        'UPGRADE_NOTE: Object lrecRemoteDataBase may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecRemoteDataBase = Nothing

        On Error GoTo 0

    End Function

    '% insPostAGL858:
    Public Function insPostAGL858(ByVal dDatefrom As Date, ByVal dDateTo As Date, ByVal nTypeIntermed As Integer, ByVal lintUsercode As Integer, ByVal lintCompany As Integer) As Boolean

        Dim lrecinsPostAGL858 As eRemoteDB.Execute

        lrecinsPostAGL858 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agl858'
        With lrecinsPostAGL858
            .StoredProcedure = "rea_agl858"
            .Parameters.Add("dDateFrom", dDatefrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeIntermed", nTypeIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                P_SKEY = .Parameters("P_SKEY").Value
                insPostAGL858 = True
            Else
                insPostAGL858 = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGL858 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGL858 = Nothing

    End Function

    '% insPostAGL703:
    Public Function insPostAGL703(ByVal dDateIni As Date, ByVal dDateEnd As Date, ByVal sKey As String) As Boolean

        Dim lrecinsPostAGL703 As eRemoteDB.Execute

        lrecinsPostAGL703 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agl703'
        With lrecinsPostAGL703
            .StoredProcedure = "rea_agl703"
            .Parameters.Add("dDateFrom", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateTo", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostAGL703 = True
            Else
                insPostAGL703 = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGL703 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGL703 = Nothing

    End Function
    '% insPostAGL703_A:
    Public Function insPostAGL703_A(ByVal nIntertyp As Double, ByVal sIntertyp As String, ByVal nPay_Comm As Double, ByVal dPay_date As Date, ByVal dProcSup As Date, ByVal dVal_Date As Date, ByVal dCompdate As Date, ByVal sKey As String) As Boolean

        Dim lrecinsPostAGL703 As eRemoteDB.Execute

        lrecinsPostAGL703 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agl703'
        With lrecinsPostAGL703
            .StoredProcedure = "rea_agl703A"
            .Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_Comm", nPay_Comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPay_date", dPay_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dProcSup", dProcSup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dVal_Date", dVal_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCompdate", dCompdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostAGL703_A = True
            Else
                insPostAGL703_A = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGL703 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGL703 = Nothing

    End Function


    Public Function insPostAGC816(ByVal nAgency As Integer, ByVal nIntermed As Double, ByVal sType_Infor As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nYear As Integer) As Boolean

        Dim lrecinsPostAGC816 As eRemoteDB.Execute

        lrecinsPostAGC816 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agc816'
        With lrecinsPostAGC816
            .StoredProcedure = "rea_agc816"
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType_Infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                P_SKEY = .Parameters("P_SKEY").Value
                insPostAGC816 = True
            Else
                insPostAGC816 = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGC816 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGC816 = Nothing

    End Function

    '% insvalAGL919_K: se realizan las validaciones de la transacción
    Public Function insvalAGL919_K(ByVal sCodispl As String, ByVal dInit_Date As Date, ByVal dEnd_Date As Date) As String
        Dim lclsErrors As New eFunctions.Errors

        On Error GoTo insvalAGL919_K_err

        With lclsErrors
            '+ La fecha inicial debe estar llena
            If dInit_Date = dtmNull Then
                Call .ErrorMessage(sCodispl, 8336)
            Else
                '+ La fecha inicial debe ser mayor a la última fecha procesada
                If Find_FECUS_range() Then
                    If dInit_Date <= dMax_pay_date Then
                        Call .ErrorMessage(sCodispl, 56175)
                    End If
                End If
            End If

            '+ La fecha final debe estar llena
            If dEnd_Date = dtmNull Then
                Call .ErrorMessage(sCodispl, 9072)
            Else
                '+ La fecha final debe ser mayor o igual a la feha inicial
                If dEnd_Date < dInit_Date Then
                    Call .ErrorMessage(sCodispl, 55006)
                End If
            End If
            insvalAGL919_K = .Confirm
        End With

insvalAGL919_K_err:
        If Err.Number Then
            insvalAGL919_K = "insvalAGL919_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% Find_FECUS_range: se obtiene el rango de fechas de los registros para el proceso FECU
    Public Function Find_FECUS_range() As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo Find_FECUS_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "reaDateT_com_prod"
            .Parameters.Add("dMin_pay_date", dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dMax_pay_date", dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                dMin_pay_date = .Parameters("dMin_pay_date").Value
                dMax_pay_date = .Parameters("dMax_pay_date").Value
                Find_FECUS_range = dMin_pay_date <> dtmNull
            End If
        End With

Find_FECUS_Err:
        If Err.Number Then
            Find_FECUS_range = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Function

    '% Find_FECUS_range: se obtiene el rango de fechas de los registros para el proceso FECU
    Public Function Find_Contrat_Pay(ByVal nMonth As Integer, ByVal nYear As Integer) As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo Find_Contrat_Pay_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "Readateestipendio"
            .Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                If .Parameters("nCount").Value > 0 Then
                    Find_Contrat_Pay = True
                Else
                    Find_Contrat_Pay = False
                End If
            Else
                Find_Contrat_Pay = False
            End If
        End With

Find_Contrat_Pay_Err:
        If Err.Number Then
            Find_Contrat_Pay = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Function

    '% inspostAGL919_K: se ejecuta el proceso para obtener las primas recaudadas y las comisiones
    '%                  pagadas para aquellos intermediarios que participen en los informes FECU
    Public Function inspostAGL919_K(ByVal dInit_Date As Date, ByVal dEnd_Date As Date, ByVal nUsercode As Integer) As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo inspostAGL919_K_err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insAGL919"
            .Parameters.Add("dInit_Date", dInit_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnd_Date", dEnd_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                inspostAGL919_K = True
            End If
        End With

inspostAGL919_K_err:
        If Err.Number Then
            inspostAGL919_K = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Function

    '% insvalAGL920_K: se realizan las validaciones de la transacción
    Public Function insvalAGL920_K(ByVal sCodispl As String, ByVal dInit_Date As Date, ByVal dEnd_Date As Date) As String
        Dim lclsErrors As New eFunctions.Errors

        On Error GoTo insvalAGL920_K_err

        With lclsErrors
            '+ La fecha inicial debe estar llena
            If dInit_Date = dtmNull Then
                Call .ErrorMessage(sCodispl, 8336)
            End If

            '+ La fecha final debe estar llena
            If dEnd_Date = dtmNull Then
                Call .ErrorMessage(sCodispl, 9072)
            Else
                '+ La fecha final debe ser mayor o igual a la feha inicial
                If dEnd_Date < dInit_Date Then
                    Call .ErrorMessage(sCodispl, 55006)
                End If
            End If
            insvalAGL920_K = .Confirm
        End With

insvalAGL920_K_err:
        If Err.Number Then
            insvalAGL920_K = "insvalAGL920_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insvalAGL921_K: se realizan las validaciones de la transacción
    Public Function insvalAGL921_K(ByVal sCodispl As String, ByVal dInit_Date As Date, ByVal dEnd_Date As Date) As String
        Dim lclsErrors As New eFunctions.Errors

        On Error GoTo insvalAGL921_K_err

        With lclsErrors
            '+ La fecha inicial debe estar llena
            If dInit_Date = dtmNull Then
                Call .ErrorMessage(sCodispl, 8336)
            End If

            '+ La fecha final debe estar llena
            If dEnd_Date = dtmNull Then
                Call .ErrorMessage(sCodispl, 9072)
            Else
                '+ La fecha final debe ser mayor o igual a la feha inicial
                If dEnd_Date < dInit_Date Then
                    Call .ErrorMessage(sCodispl, 55006)
                End If
            End If
            insvalAGL921_K = .Confirm
        End With

insvalAGL921_K_err:
        If Err.Number Then
            insvalAGL921_K = "insvalAGL921_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insvalAGL922_K: se realizan las validaciones de la transacción
    Public Function insvalAGL922_K(ByVal sCodispl As String, ByVal dInit_Date As Date, ByVal dEnd_Date As Date) As String
        Dim lclsErrors As New eFunctions.Errors

        On Error GoTo insvalAGL922_K_err

        With lclsErrors
            '+ La fecha inicial debe estar llena
            If dInit_Date = dtmNull Then
                Call .ErrorMessage(sCodispl, 8336)
            End If

            '+ La fecha final debe estar llena
            If dEnd_Date = dtmNull Then
                Call .ErrorMessage(sCodispl, 9072)
            Else
                '+ La fecha final debe ser mayor o igual a la feha inicial
                If dEnd_Date < dInit_Date Then
                    Call .ErrorMessage(sCodispl, 55006)
                End If
            End If
            insvalAGL922_K = .Confirm
        End With

insvalAGL922_K_err:
        If Err.Number Then
            insvalAGL922_K = "insvalAGL922_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% inspostAGL922_K: se generan los archivos planos sobre comisiones pagadas y primas
    '%                  recaudadas en un período
    Public Function inspostAGL922_K(ByVal dInit_Date As Date, ByVal dEnd_Date As Date, ByVal nSessionId As String, ByVal nUsercode As Integer) As Boolean
        Dim lclsRemote As eRemoteDB.Execute
        Dim lobjGeneral As eGeneral.GeneralFunction
        Dim lstrLoadFile As String
        Dim lstrDirFile As String
        Dim lclsValue As eFunctions.Values
        Dim llngFileNum As Integer
        Dim lstrFileName As String
        Dim lintFile As Short

        On Error GoTo inspostAGL922_K_err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insAGL922PKG.insAGL922"
            .Parameters.Add("dInit_Date", dInit_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnd_Date", dEnd_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSessionID", nSessionId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                lobjGeneral = New eGeneral.GeneralFunction
                lclsValue = New eFunctions.Values
                '+ Se busca la ruta en la que se guardará el archivo de texto
                lstrLoadFile = lobjGeneral.GetLoadFile()
                '+ Se busca el directorio virtual del archivo a crear
                lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))

                lintFile = .FieldToClass("nFile")
                lstrFileName = lstrLoadFile & .FieldToClass("sFile_name") & ".txt"
                llngFileNum = FreeFile
                FileOpen(llngFileNum, lstrFileName, OpenMode.Output)

                Do While Not .EOF
                    If lintFile <> .FieldToClass("nFile") Then
                        '+ Se crea el segundo archivo, para procesarlo
                        FileClose(llngFileNum)
                        lintFile = .FieldToClass("nFile")
                        lstrFileName = lstrLoadFile & .FieldToClass("sFile_name") & ".txt"
                        llngFileNum = FreeFile
                        FileOpen(llngFileNum, lstrFileName, OpenMode.Output)
                    End If
                    '+ El último caracter del registro (*), no corresponde al formato, se coloca, porque al hacer
                    '+ referencia al registro, eRemoteDB elimina los espacios en blanco a la derecha, que si
                    '+ corresponden al formato
                    PrintLine(llngFileNum, Mid(.FieldToClass("sRecord"), 1, Len(.FieldToClass("sRecord")) - 1))
                    .RNext()
                Loop
                FileClose(llngFileNum)
                inspostAGL922_K = True
            End If
        End With

inspostAGL922_K_err:
        If Err.Number Then
            inspostAGL922_K = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
        'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValue = Nothing
        'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjGeneral = Nothing
    End Function

    Public Function insPostAGL845(ByVal dDatefrom As Date, ByVal dDateTo As Date, ByVal nBranch As Double, ByVal nOffice As Double, ByVal nIntertyp As Double, ByVal ntyploan As Double) As Boolean

        Dim lrecinsPostAGL845 As eRemoteDB.Execute

        lrecinsPostAGL845 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agc816'
        With lrecinsPostAGL845
            .StoredProcedure = "rea_agl845"
            .Parameters.Add("dDatefrom", dDatefrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateto", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntyploan", ntyploan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                P_SKEY = .Parameters("P_SKEY").Value
                insPostAGL845 = True
            Else
                insPostAGL845 = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGL845 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGL845 = Nothing

    End Function


    Public Function insPostAGL918(ByVal dDateIni As Date, ByVal dDateEnd As Date) As Boolean

        Dim lrecinsPostAGL918 As eRemoteDB.Execute

        lrecinsPostAGL918 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agl703'
        With lrecinsPostAGL918
            .StoredProcedure = "rea_agl918"
            .Parameters.Add("dDateIni", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                P_SKEY = .Parameters("P_SKEY").Value
                insPostAGL918 = True
            Else
                insPostAGL918 = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGL918 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGL918 = Nothing

    End Function
    Public Function insPostAGL918B(ByVal dDateIni As Date, ByVal dDateEnd As Date) As Boolean

        Dim lrecinsPostAGL918B As eRemoteDB.Execute

        lrecinsPostAGL918B = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agl703'
        With lrecinsPostAGL918B
            .StoredProcedure = "rea_agl918b"
            .Parameters.Add("dDateIni", dDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                P_SKEY = .Parameters("P_SKEY").Value
                insPostAGL918B = True
            Else
                insPostAGL918B = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGL918B may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGL918B = Nothing

    End Function


    '% insValAGL786_k: Se valida cálculo de interes por prestamo
    Public Function insValAGL786_k(ByVal sCodispl As String, ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        On Error GoTo insValAGL786_k_Err
        With lobjErrors
            '+ Se valida el campo fecha de inicio
            If dDate_ini = dtmNull Then
                Call lobjErrors.ErrorMessage("AGL786", 60217)
            Else
                '+ Se valida el campo fecha de termino
                If dDate_end = dtmNull Then
                    Call lobjErrors.ErrorMessage("AGL786", 60218)
                Else
                    '+ Se valida el campo fecha de inicio se menor a Fecha de termino
                    If dDate_ini >= dDate_end Then
                        Call lobjErrors.ErrorMessage("AGL786", 60205)
                    End If
                End If
            End If
            If nBranch = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage("AGL786", 1022)
            End If

            If nProduct = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage("AGL786", 1014)
            End If
            insValAGL786_k = True
        End With

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValAGL786_k_Err:
        If Err.Number Then
            insValAGL786_k = CShort(insValAGL786_k) + CDbl(Err.Description)
            insValAGL786_k = False
        End If
        On Error GoTo 0
    End Function


    Public Function insValAGL8001_k(ByVal sCodispl As String, ByVal nYear As Short, ByVal nMonth As Short, ByVal sPreliminary As String, ByVal nUsercode As Integer) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalAGL8001_k As eRemoteDB.Execute

        On Error GoTo insValAGL8001_k_Err

        lrecinsvalAGL8001_k = New eRemoteDB.Execute

        With lrecinsvalAGL8001_k
            .StoredProcedure = "InsValAGL8001_K"
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPreliminary", sPreliminary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage("AGL8001", , , , , , lstrErrorAll)
            End If
            insValAGL8001_k = .Confirm
        End With

insValAGL8001_k_Err:
        If Err.Number Then
            insValAGL8001_k = "insValAGL8001_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalAGL8001_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalAGL8001_k = Nothing
    End Function

    Public Function insPostAGL786_k(ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        Dim lrecinsPostAGL786_k As eRemoteDB.Execute

        lrecinsPostAGL786_k = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_cal1078'
        With lrecinsPostAGL786_k
            .StoredProcedure = "insreatmp_postagl786"
            .Parameters.Add("dDate_ini", dDate_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostAGL786_k = True
                P_SKEY = .Parameters("sKey").Value
                sFile_name = insGenFilesAGL786(P_SKEY)
                If sFile_name <> " " Then
                    insPostAGL786_k = True
                Else
                    insPostAGL786_k = False
                End If
            Else
                insPostAGL786_k = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGL786_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGL786_k = Nothing

    End Function


    Public Function insPostAGL8001_k(ByVal nYear As Short, ByVal nMonth As Short, ByVal sPreliminary As String, ByVal nUsercode As Integer) As Boolean
        Dim lrecRS As eRemoteDB.Execute

        lrecRS = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_cal1078'
        With lrecRS
            .StoredProcedure = "INSAGL8001PKG.INSAGL8001"
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPreliminary", sPreliminary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMessage", sMessage, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostAGL8001_k = True
                sMessage = .Parameters("sMessage").Value
            End If
        End With

        'UPGRADE_NOTE: Object lrecRS may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecRS = Nothing

    End Function


    '%insGenFilesAGL786: Crea los archivos del proceso agl786
    Public Function insGenFilesAGL786(ByVal sKey As String) As String
        Dim lrecTime As eRemoteDB.Execute
        Dim lrecinsReatmp_agl786 As eRemoteDB.Execute
        'Dim lrecinsReatmp_agl786res As eRemoteDB.Execute

        Dim lobjGeneral As eGeneral.GeneralFunction
        'Dim lobjClient As eClient.Client
        'Dim lobjCompany As eGeneral.Company

        'Dim llngRecCounter As Integer
        'Dim ljdblAmountTot As Double
        Dim lstrLoadFile As String
        Dim lstrDirFile As String
        'Dim lstrCompany As Object

        'Dim lstrWritTitle As String
        Dim lstrWritTxt As String
        Dim FileName As String
        'Dim FileNameCityDet As String
        Dim FileNum As Integer
        'Dim lProduct As Integer
        'Dim lBranch As Integer
        'Dim lControl As Short
        'Dim lTotal As Integer
        Dim nCount As Integer

        insGenFilesAGL786 = CStr(True)

        lrecTime = New eRemoteDB.Execute

        With lrecTime
            .StoredProcedure = "insReatmp_agl786a"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With

        Dim lclsValue As eFunctions.Values
        If lrecTime.Run() Then

            lobjGeneral = New eGeneral.GeneralFunction
            '+ Se busca la ruta en la que se guardará el archivo de texto
            lstrLoadFile = lobjGeneral.GetLoadFile()
            '+ Se busca el directorio virtual del archivo a crear
            lclsValue = New eFunctions.Values
            lstrDirFile = Trim(lclsValue.insGetSetting("VirtualRootLoad", String.Empty, "Paths"))
            'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsValue = Nothing
            '+ ------------------------------------------
            If Not lrecTime.EOF Then
                insGenFilesAGL786 = "agl786_" & sKey & ".xls"
                FileName = lstrLoadFile & "agl786_" & sKey & ".xls"
                FileNum = FreeFile
                FileOpen(FileNum, FileName, OpenMode.Output)
                PrintLine(FileNum, "CONSORCIO")
                PrintLine(FileNum, Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & "REPORTE DE VALIDACION DE PORCENTAJE DE COMISIONES ")
                PrintLine(FileNum, " ")
                PrintLine(FileNum, " ")
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                PrintLine(FileNum, Chr(9) & "PERIODO :" & Chr(9) & CStr(IIf(IsDbNull(lrecTime.FieldToClass("dDateini")), "00/00/0000", Format(lrecTime.FieldToClass("dDateini"), "yyyy/MM/dd"))) & " AL " & CStr(IIf(IsDbNull(lrecTime.FieldToClass("dDateend")), "00/00/0000", Format(lrecTime.FieldToClass("dDateend"), "yyyy/MM/dd"))))
                PrintLine(FileNum, " ")
                PrintLine(FileNum, " ")
                lstrWritTxt = ""
                lstrWritTxt = lstrWritTxt & "RAMO" & Chr(9)
                lstrWritTxt = lstrWritTxt & "PRODUCTO" & Chr(9)
                lstrWritTxt = lstrWritTxt & "NRO. PÓLIZA" & Chr(9)
                lstrWritTxt = lstrWritTxt & "ESTADO PÓLIZA" & Chr(9)
                lstrWritTxt = lstrWritTxt & "FEC. INI. VIG." & Chr(9)
                lstrWritTxt = lstrWritTxt & "MONEDA POL." & Chr(9)
                lstrWritTxt = lstrWritTxt & "MTO. PRIMA RECAUDADA" & Chr(9)
                lstrWritTxt = lstrWritTxt & "MTO. PRIMA NETA" & Chr(9)
                lstrWritTxt = lstrWritTxt & "FEC. ING. CAJA" & Chr(9)
                lstrWritTxt = lstrWritTxt & "MTO. PRIMA BÁSICA" & Chr(9)
                lstrWritTxt = lstrWritTxt & "MTO. PRIMAS PAGADAS" & Chr(9)
                lstrWritTxt = lstrWritTxt & "% COMISIÓN" & Chr(9)
                lstrWritTxt = lstrWritTxt & "COD. INTERMED." & Chr(9)
                lstrWritTxt = lstrWritTxt & "RUT  INTERMED." & Chr(9)
                lstrWritTxt = lstrWritTxt & "NOMBRE INTERMED." & Chr(9)
                lstrWritTxt = lstrWritTxt & "TIPO INTERMED" & Chr(9)
                lstrWritTxt = lstrWritTxt & "ESTADO VIGENCIA" & Chr(9)
                lstrWritTxt = lstrWritTxt & "COD. AGENCIA" & Chr(9)
                lstrWritTxt = lstrWritTxt & "COD. JEFE 1" & Chr(9)
                lstrWritTxt = lstrWritTxt & "COD. JEFE 2" & Chr(9)
                PrintLine(FileNum, lstrWritTxt)

                lrecinsReatmp_agl786 = New eRemoteDB.Execute

                With lrecinsReatmp_agl786
                    .StoredProcedure = "insReatmp_agl786"
                    .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If .Run() Then
                        lstrWritTxt = ""
                        Do While Not .EOF
                            lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sBranch"), " ", 19, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sProduct"), " ", 19, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPolicy") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPolicy")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sStatus_Pol"), " ", 40, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dStartdate") = dtmNull, "", "'" & Format(.FieldToClass("dStartDate"), "yyyy/MM/dd") & "'")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("scurren_pol"), " ", 19, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPremium") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPremium")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("npremiumn") = eRemoteDB.Constants.intNull, "", .FieldToClass("npremiumn")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & CStr(IIf(.FieldToClass("dDate_Cash") = dtmNull, "", "'" & Format(.FieldToClass("dDate_Cash"), "yyyy/MM/dd") & "'")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPremiumBas") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPremiumBas")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPremiumPay") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPremiumPay")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nPerComm") = eRemoteDB.Constants.intNull, "", .FieldToClass("nPerComm")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nIntermed") = eRemoteDB.Constants.intNull, "", .FieldToClass("nIntermed")) & Chr(9)
                            lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sClient"), " ", 40, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & FormatData(.FieldToClass("sCliename"), " ", 40, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nIntertyp") = eRemoteDB.Constants.intNull, "", .FieldToClass("nIntertyp")) & " " & FormatData(.FieldToClass("sIntertyp"), " ", 19, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nInt_Status") = eRemoteDB.Constants.intNull, "", .FieldToClass("nInt_Status")) & " " & FormatData(.FieldToClass("sInt_Status"), " ", 19, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nAgency") = eRemoteDB.Constants.intNull, "", .FieldToClass("nAgency")) & " " & FormatData(.FieldToClass("sAgency"), " ", 19, "Left", "Left") & Chr(9)
                            lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nSupervis") = eRemoteDB.Constants.intNull, "", .FieldToClass("nSupervis")) & Chr(9)

                            nCount = nCount + 1

                            If (nCount Mod 10) = 0 Then
                                lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nSuper") = eRemoteDB.Constants.intNull, "", .FieldToClass("nSuper")) & Chr(9)
                            Else
                                lstrWritTxt = lstrWritTxt & IIf(.FieldToClass("nSuper") = eRemoteDB.Constants.intNull, "", .FieldToClass("nSuper")) & Chr(9) & vbCrLf
                            End If



                            If (nCount Mod 10) = 0 Then
                                PrintLine(FileNum, lstrWritTxt)
                                lstrWritTxt = ""
                            End If

                            .RNext()
                        Loop

                        If (lstrWritTxt <> "") Then
                            PrintLine(FileNum, lstrWritTxt)
                        End If

                        'UPGRADE_NOTE: Object lrecinsReatmp_agl786 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lrecinsReatmp_agl786 = Nothing
                    End If
                End With
                FileClose(FileNum)
            End If
        Else

            sFile_name = " "
            insGenFilesAGL786 = " "
        End If

    End Function


    '%FormatData: Esta función se encarga de dar formato a los datos a enviar a archivos de texto.
    Private Function FormatData(ByVal sValue As Object, ByVal sChar As String, ByVal nposition As Integer, Optional ByVal sTrunc As String = "Right", Optional ByVal sAlign As String = "Right") As String

        Dim nLength As Integer
        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
        If Not IsDbNull(sValue) Then
            sValue = Trim(sValue)
            nLength = Len(sValue)
            If nLength > nposition Then
                If sTrunc = "Right" Then
                    FormatData = Right(sValue, nposition)
                Else
                    FormatData = Left(sValue, nposition)
                End If
            Else
                If sAlign = "Right" Then
                    FormatData = New String(sChar, nposition - nLength) & sValue
                Else
                    FormatData = sValue & New String(sChar, nposition - nLength)
                End If
            End If
        Else
            FormatData = New String(sChar, nposition)
        End If
    End Function


    Public Function insPostAGL787(ByVal nMonth As Integer, ByVal nYear As Integer, ByVal sOptprocess As String) As Boolean

        Dim lrecinsPostAGL787 As eRemoteDB.Execute

        lrecinsPostAGL787 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agc816'
        With lrecinsPostAGL787
            .StoredProcedure = "rea_agl787"
            .Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptprocess", sOptprocess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                P_SKEY = .Parameters("P_SKEY").Value
                insPostAGL787 = True
            Else
                insPostAGL787 = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGL787 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGL787 = Nothing

    End Function


    '% insValAGL786_k: Se valida cálculo de interes por prestamo
    Public Function insValAGL918_K(ByVal dDate_ini As Date, ByVal dDate_end As Date) As Boolean
        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        On Error GoTo insValAGL918_K_Err
        With lobjErrors
            '+ Se valida el campo fecha de inicio
            If dDate_ini = dtmNull Then
                Call lobjErrors.ErrorMessage("AGL918", 60217)
            Else
                '+ Se valida el campo fecha de termino
                If dDate_end = dtmNull Then
                    Call lobjErrors.ErrorMessage("AGL918", 60218)
                Else
                    '+ Se valida el campo fecha de inicio se menor a Fecha de termino
                    If dDate_ini >= dDate_end Then
                        Call lobjErrors.ErrorMessage("AGL918", 60205)
                    End If
                End If
            End If
            insValAGL918_K = True
        End With

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValAGL918_K_Err:
        If Err.Number Then
            insValAGL918_K = CShort(insValAGL918_K) + CDbl(Err.Description)
            insValAGL918_K = False
        End If
        On Error GoTo 0
    End Function


    Public Function insPostAGC817(ByVal nIntermed_Ori As Double, ByVal nIntermed_Act As Double) As Boolean

        Dim lrecinsPostAGC817 As eRemoteDB.Execute

        lrecinsPostAGC817 = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.rea_agc816'
        With lrecinsPostAGC817
            .StoredProcedure = "rea_agc817"
            .Parameters.Add("nIntermed_Ori", nIntermed_Ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed_Act", nIntermed_Act, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                P_SKEY = .Parameters("P_SKEY").Value
                insPostAGC817 = True
            Else
                insPostAGC817 = False
            End If

        End With

        'UPGRADE_NOTE: Object lrecinsPostAGC817 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostAGC817 = Nothing

    End Function

    '%insValAGL776_K: Esta función se encarga de realizar las respectivas validaciones de la transacción.
    Public Function insValAGL776_K(ByVal sCodispl As String, ByVal dDateIni As Date, ByVal dDateEnd As Date) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValAGL776_K_Err

        lobjErrors = New eFunctions.Errors

        With lobjErrors

            If dDateIni = dtmNull Then
                .ErrorMessage(sCodispl, 9071)
            End If

            If dDateEnd = dtmNull Then
                .ErrorMessage(sCodispl, 9072)
            End If

            If dDateIni > dDateEnd Then
                .ErrorMessage(sCodispl, 60113)
            End If

            insValAGL776_K = .Confirm
        End With

insValAGL776_K_Err:
        If Err.Number Then
            insValAGL776_K = "InsValAGL776_K: " & insValAGL776_K & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function
    Public Function insValAGL960_K(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dValor_date As Date) As String
        Dim lobjErrors As eFunctions.Errors

        Dim lblnValid As Boolean

        On Error GoTo insValAGL690_K_Err

        lobjErrors = New eFunctions.Errors

        '+ Área de seguro debe estar llena

        'If nInsur_area = 0 Or nInsur_area = eRemoteDB.Constants.intNull Then
        'Call lobjErrors.ErrorMessage(sCodispl, 55031)
        'End If

        '+ Fecha de Valorización debe estar llena

        If dValor_date = dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 55527)
        End If

        insValAGL960_K = lobjErrors.Confirm


insValAGL690_K_Err:
        If Err.Number Then
            insValAGL960_K = "insValAGL960_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

    End Function

    'insPostAGL960_K: Método que realiza el proceso de solicitud de pago de comisiones de estipendio
    Public Function insPostAGL960_K(ByVal nContrat_pay As Integer, ByVal nInsur_area As Integer, ByVal dProcess_date As Date, ByVal dValue_date As Date, ByVal nUsercode As Integer, ByVal sOptprocess As String) As Boolean

        Dim lexeinsAGL960 As eRemoteDB.Execute

        lexeinsAGL960 = New eRemoteDB.Execute

        On Error GoTo insPostAGL960_K_Err

        With lexeinsAGL960
            .StoredProcedure = "insStipends_Pay"
            .Parameters.Add("nContrat_pay", nContrat_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dProcess_date", dProcess_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValue_date", dValue_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInd", IIf(nContrat_pay <> 0 And nContrat_pay <> eRemoteDB.Constants.intNull, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey_Aux", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptProcess", sOptprocess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                sKey = .Parameters("sKey_Aux").Value
                insPostAGL960_K = True
            Else
                insPostAGL960_K = False
            End If
        End With

insPostAGL960_K_Err:
        If Err.Number Then
            insPostAGL960_K = False
        End If
        'UPGRADE_NOTE: Object lexeinsAGL009 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lexeinsAGL960 = Nothing
        On Error GoTo 0
    End Function
End Class






