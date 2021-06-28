Option Strict Off
Option Explicit On
Public Class tmp_switch

    '%-------------------------------------------------------%'
    '% $Workfile:: tmp_switch.vb                            $%'
    '% $Author:: Mgonzalez                                  $%'
    '% $Date:: 15-06-15 19:05                               $%'
    '% $Revision:: 6                                        $%'
    '%-------------------------------------------------------%'

    'Column_name                      Type        Length      Prec  Scale Nullable
    '-------------------------------- ----------- ----------- ----- ----- ---------
    Public nBranch As Long
    Public nProduct As Long
    Public nPolicy As Double
    Public nCertif As Double
    Public dEffecdate As Date
    Public nOrigin As Long
    Public sOrigin As String
    Public nUsercode As Long
    Public sKey As String
    Public nId As Double
    Public nCount_Sell As Long
    Public sTyp_profitworker_sell As String
    Public nFunds_sell As Long
    Public nTyp_profitworker_sell As Long
    Public nPercent_sell As Double
    Public nPercent_buy As Double
    Public sFund_sell As String
    Public nQuan_avail_sell As Double
    Public sFund_buy As String
    Public sTyp_profitworker_buy As String
    Public nQuan_avail_sell_uf As Double
    Public sContent_Sell As String
    Public nAmount_mov As Double
    Public nQuan_avail_buy_switch As Double
    Public nQuot_Value_Buy As Double


    '%insValVI017_k: Validacion de encabezado de la transacción VI017.
    '--------------------------------------------------------------------------------------------
    Public Function insValvi017_k(ByVal nBranch As Long, ByVal nProduct As Long, _
                                  ByVal nPolicy As Double, ByVal nCertif As Double, _
                                  ByVal dEffecdate As Date, ByVal nUsercode As Long) As String
        '--------------------------------------------------------------------------------------------
        Dim lrecVI017 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo insValVI017_k_Err

        lrecVI017 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecVI017
            .StoredProcedure = "insVI017pkg.insValvi017_k"
            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("arrayerrors", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> vbNullString Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage("VI017", , , , , , lstrError)
                    insValvi017_k = .Confirm()
                End With
                lobjErrors = Nothing

            End If
        End With

insValVI017_k_Err:
        If Err.Number Then
            insValvi017_k = "insValVI017_k: " & Err.Description
        End If

        lrecVI017 = Nothing
        On Error GoTo 0

    End Function

    '%insValVI017: Validacion de encabezado de la transacción VI017.
    '--------------------------------------------------------------------------------------------
    Public Function insValVI017(ByVal sKey As String) As String
        '--------------------------------------------------------------------------------------------
        Dim lrecVI017 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo insValVI017_Err

        lrecVI017 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecVI017
            .StoredProcedure = "insVI017pkg.insValvi017"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("arrayerrors", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> vbNullString Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage("VI017", , , , , , lstrError)
                    insValVI017 = .Confirm()
                End With
                lobjErrors = Nothing

            End If
        End With

insValVI017_Err:
        If Err.Number Then
            insValVI017 = "insValVI017: " & Err.Description
        End If

        lrecVI017 = Nothing
        On Error GoTo 0

    End Function

    '%insValVI017_2: Esta función se encarga de validar los objetos del sincronizador del cotizador
    '-------------------------------------------------------------------------------------------
    Public Function insValVI017_2(ByVal sCodispl As String, _
                                  ByVal nTotalSell As Double, _
                                  ByVal nTotalBuy As Double) As String
        '-------------------------------------------------------------------------------------------
        Dim lerrTime As eFunctions.Errors
        Dim nPercentSell As Double
        On Error GoTo insValVI017_2_Err

        lerrTime = New eFunctions.Errors
        With lerrTime
            If nTotalSell <= 0 Then
                .ErrorMessage(sCodispl, 81)
            Else
                If nTotalBuy <= 0 Or _
                   nTotalSell <> nTotalBuy Then
                    nPercentSell = nTotalBuy / nTotalSell
                    .ErrorMessage(sCodispl, 82, , , "( " & Format(nPercentSell, "Percent") & " )")
                End If
            End If
            insValVI017_2 = .Confirm
        End With

insValVI017_2_Err:
        If Err.Number Then
            insValVI017_2 = insValVI017_2 + Err.Description
        End If
        On Error GoTo 0
        lerrTime = Nothing
    End Function

    Public Function insPrevi017(ByVal nBranch As Long, ByVal nProduct As Long, _
                                ByVal nPolicy As Double, ByVal nCertif As Double, _
                                ByVal dEffecdate As Date, ByVal nOrigin As Long, _
                                ByVal nUsercode As Long, _
                                ByVal sChkTransferAll As String, _
                                ByVal sCodispl As String, _
                                ByVal sByOrigin As String) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lrecVI017 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo insPrevi017_Err

        lrecVI017 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecVI017
            .StoredProcedure = "insVI017pkg.insPrevi017"
            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOrigin", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTransferAll", sChkTransferAll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sByOrigin", sByOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            sKey = .Parameters("sKey").Value
            insPrevi017 = True
        End With

insPrevi017_Err:
        If Err.Number Then
            insPrevi017 = False
        End If

        lrecVI017 = Nothing
        On Error GoTo 0

    End Function


    Public Function insUpdvi017(ByVal sKey As String, ByVal nId As Long, _
                                ByVal nType As Long, ByVal nPercent As Double, _
                                Optional ByVal nAmountToSell As Double = 0) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lrecVI017 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo insUpdvi017_Err

        lrecVI017 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecVI017
            .StoredProcedure = "insVI017pkg.Updvi017"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContent", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountToSell", nAmountToSell, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnitToBuy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnit_Price", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            insUpdvi017 = True
            nCount_Sell = .Parameters("nContent").Value
            nQuan_avail_buy_switch = .Parameters("nUnitToBuy").Value
            nQuot_Value_Buy = .Parameters("nUnit_Price").Value
        End With

insUpdvi017_Err:
        If Err.Number Then
            insUpdvi017 = False
        End If

        lrecVI017 = Nothing
        On Error GoTo 0

    End Function

    Public Function insPostvi017(ByVal sKey As String, _
                                 Optional ByVal sChkAll As String = vbNullString) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lrecVI017 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo insPostvi017_Err

        lrecVI017 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecVI017
            .StoredProcedure = "insVI017pkg.insPostvi017"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChkAll", sChkAll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            insPostvi017 = True
        End With

insPostvi017_Err:
        If Err.Number Then
            insPostvi017 = False
        End If

        lrecVI017 = Nothing
        On Error GoTo 0

    End Function

    Public Function insUpdvi017Massive(ByVal sKey As String, _
                                       ByVal sIds As String, _
                                       ByVal sPercents As String, _
                                       ByVal nType As Integer) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lrecVI017 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo insUpdvi017_Err

        lrecVI017 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecVI017
            .StoredProcedure = "insVI017pkg.UPDVI017MASSIVE"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIds", sIds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPercents", sPercents, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdvi017Massive = .Run(False)
        End With

insUpdvi017_Err:
        If Err.Number Then
            insUpdvi017Massive = False
        End If

        lrecVI017 = Nothing
        On Error GoTo 0

    End Function

    '--------------------------------------------------------------------------------------------
    Public Function insUpdvi017Changes(ByVal sKey As String, _
                                       ByVal nAction As Integer) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lrecVI017 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        On Error GoTo insUpdvi017_Err

        lrecVI017 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecVI017
            .StoredProcedure = "insVI017pkg.UPDVI017CHANGES"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sContent", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdvi017Changes = .Run(False)
            sContent_Sell = .Parameters("sContent").Value
        End With

insUpdvi017_Err:
        If Err.Number Then
            insUpdvi017Changes = False
        End If

        lrecVI017 = Nothing
        On Error GoTo 0

    End Function

End Class

