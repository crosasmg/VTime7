<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

    '**- The constant to handling errors and warning is defined.
    '- Se define la contante para el manejo de errores en caso de advertencias.

    Dim mstrCommand As String

    '**- The auxiliary variable to pass the values from the header to folder is defined.
    '- Variable auxiliar para pase de valores del encabezado al folder.

    Dim mstrQueryString As String

    Dim mstrErrors As Object
    Dim mobjValues As eFunctions.Values
    Dim mobjFundValue As Object
    Dim mobjFundValues As Object
    Dim mobjMantNoTraLife As Object


    '**% insValMantNoTraLife: The massive validation of the page is performed.
    '% insValMantNoTraLife: Se realizan las validaciones masivas de la forma.
    '--------------------------------------------------------------------------------------------
    Function insValMantNoTraLife() As Object
        '--------------------------------------------------------------------------------------------
        Select Case Request.QueryString.Item("sCodispl")

        '+ MVI012: % modalidad de inversion.

            Case "MVI012"
                mobjMantNoTraLife = New eBranches.Plan_intwar_day

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("dEffecdate") = .Form.Item("tcdEffecDate")

                        insValMantNoTraLife = mobjMantNoTraLife.insValMVI012_k(Request.QueryString.Item("sCodispl"), mobjValues.StringToDate(Session("dEffecdate")))
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            insValMantNoTraLife = mobjMantNoTraLife.insValMVI012(Request.QueryString.Item("sCodispl"), _
                                                                                 mobjValues.StringToType(Request.Form.Item("tcnTypeinvest"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                 mobjValues.StringToType(Request.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                 Session("sSche_code"), _
                                                                                 mobjValues.StringToType(Request.Form.Item("tcdFundDate"), eFunctions.Values.eTypeData.etdDate), _
                                                                                 mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        Else
                            insValMantNoTraLife = vbNullString
                        End If
                    End If
                End With
                mobjMantNoTraLife = Nothing
        '**+ MVI002: Nominal values of the funds.
        '+ MVI002: Valor nominal para los fondos.

            Case "MVI002"
                mobjMantNoTraLife = New ePolicy.Fund_value

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("dEffecdate") = .Form.Item("tcdEffecDate")

                        insValMantNoTraLife = mobjMantNoTraLife.insValMVI002_k(Request.QueryString.Item("sCodispl"), mobjValues.StringToDate(Session("dEffecdate")))
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            insValMantNoTraLife = mobjMantNoTraLife.insValMVI002(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcnFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), Session("sSche_code"), mobjValues.StringToType(Request.Form.Item("tcdFundDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        Else
                            insValMantNoTraLife = vbNullString
                        End If
                    End If
                End With

            '**+ MVI003: Investment funds.
            '+ MVI003: Fondos de inversión.

            Case "MVI003"
                mobjMantNoTraLife = New ePolicy.Fund_inv

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&dEffecdate=" & .Form.Item("tcdEffecDate")

                        '					insValMantNoTraLife = mobjMantNoTraLife.insValMVI003_k(Request.QueryString("sCodispl"), '																	       Request.QueryString("nMainAction"), '                                                                           mobjValues.StringToDate(.Form("tcdEffecDate")))
                        insValMantNoTraLife = True
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            insValMantNoTraLife = mobjMantNoTraLife.insValMVI003(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Request.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnQuan_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnQuan_max"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnQuan_avail"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("cbeStatregt"), mobjValues.StringToType(Request.Form.Item("tcdDinpdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(Request.Form.Item("tcnSeries"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRun"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenCountry"), eFunctions.Values.eTypeData.etdLong), .Form("tctRoutine"))
                        Else
                            insValMantNoTraLife = vbNullString
                        End If
                    End If
                End With

            '**+ MVI005: Funds stock movements.
            '+ MVI005: Movimientos de stock de fondos.

            Case "MVI005"
                mobjMantNoTraLife = New ePolicy.Fund_stock

                With Request
                    If CDbl(.QueryString.Item("nMainAction")) <> 401 Then
                        insValMantNoTraLife = mobjMantNoTraLife.insValMVI005_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("valFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMovetype"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdEffecDate")))
                    End If
                End With

            '**+ MVIC001: History of face values.
            '+ MVIC001: Histórico de valores nominales.

            Case "MVIC001"
                mobjMantNoTraLife = New ePolicy.Fund_value

                insValMantNoTraLife = vbNullString

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValMantNoTraLife = mobjMantNoTraLife.insValMVIC001_k(Request.Form.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            '+ MS7000: Tabla de instituciones financieras

            Case "MS7000"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjFundValue = New eBranches.Tab_Fn_Inst

                    insValMantNoTraLife = mobjFundValue.InsValMS7000(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnInstitution"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTypeInstitu"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctName"), mobjValues.StringToType(Request.Form.Item("cbeStatregt"), eFunctions.Values.eTypeData.etdDouble))
                End If

            '+ MVI7000: Tabla de tarifas del seguro de ahorro previsional voluntario (APV).

            Case "MVI7000"
                With Request
                    If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
                        mobjFundValue = New eBranches.Tar_Apv

                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            insValMantNoTraLife = mobjFundValue.insValMVI7000_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valRole"), eFunctions.Values.eTypeData.etdDouble, True))

                        Else
                            If .QueryString.Item("WindowType") = "PopUp" Then
                                insValMantNoTraLife = mobjFundValue.insValMVI7000Upd(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital_init"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapital_end"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFix_Cost"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCalcType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSex"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_year_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_year_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkSmoking"), mobjValues.StringToType(.Form.Item("cbeTypeRisk"), eFunctions.Values.eTypeData.etdDouble, True))
                            End If
                        End If
                    End If
                End With

            '+ MVI7001: Tabla de costos fijos - APV.

            Case "MVI7001"
                mobjFundValue = New eBranches.Tab_Ul_Costs

                With Request
                    If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            insValMantNoTraLife = mobjFundValue.InsValMVI7001_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            If .QueryString.Item("WindowType") = "PopUp" Then
                                insValMantNoTraLife = mobjFundValue.InsValMVI7001("MVI7001", .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth_From"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth_Until"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCost_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTopAmount"), eFunctions.Values.eTypeData.etdDouble))
                            End If
                        End If
                    End If
                End With

            '+ MVI7002: Tabla de orden de uso de las cuentas origen para pagar cargos (APV).
            '+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
            Case "MVI7002"
                mobjFundValue = New eBranches.Tab_Ord_Origin
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    insValMantNoTraLife = mobjFundValue.InsValMVI7002_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        insValMantNoTraLife = mobjFundValue.InsValMVI7002(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End If

            '+ MVI8001: Tabla de porcentaje de descuento por prima recaudada para vida no tradicional.
            Case "MVI8001"
                With Request
                    If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
                        mobjMantNoTraLife = New eBranches.Disc_percentage

                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            insValMantNoTraLife = mobjMantNoTraLife.insValMVI8001(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("nZone"), .QueryString("Action"))
                        Else
                            If .QueryString.Item("WindowType") = "PopUp" Then
                                insValMantNoTraLife = mobjMantNoTraLife.insValMVI8001(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("nZone"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQprempayed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDisc_percentage"), eFunctions.Values.eTypeData.etdDouble, True))
                            End If
                        End If
                    End If
                End With

            '+ MVI8003: Tabla de porcentaje de descuento por poliza para vida no tradicional.
            Case "MVI8003"
                With Request
                    If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
                        mobjMantNoTraLife = New eBranches.Disc_perc_year

                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            insValMantNoTraLife = mobjMantNoTraLife.insValMVI8003(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("nZone"), .QueryString("Action"))
                        Else
                            If .QueryString.Item("WindowType") = "PopUp" Then
                                insValMantNoTraLife = mobjMantNoTraLife.insValMVI8003(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("nZone"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDisc_percentage"), eFunctions.Values.eTypeData.etdDouble, True))
                            End If
                        End If
                    End If
                End With

            Case "MVI7300"
                mobjMantNoTraLife = New eSaapv.Ul_Legal_Terms

                If Request.QueryString("nZone") = 1 Then
                    insValMantNoTraLife = mobjMantNoTraLife.insValMVI7300_K(mobjValues.StringToType(Request.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                Else
                    If Request.QueryString("WindowType") = "PopUp" Then

                        insValMantNoTraLife = mobjMantNoTraLife.insValMVI7300(Request.QueryString("Action"), mobjValues.StringToType(Request.Form("cbeType_saapv"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeValuesmo"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeValuesty"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("tcnDayadd"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate))
                    End If
                End If

                'UPGRADE_NOTE: Object mobjMantNoTraLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMantNoTraLife = Nothing

            Case "MVI7500"
                mobjMantNoTraLife = New eSaapv.Tab_state_saapv

                If Request.QueryString("WindowType") = "PopUp" Then
                    insValMantNoTraLife = mobjMantNoTraLife.insValMVI7500_k(Request.QueryString("Action"), mobjValues.StringToType(Request.Form("cbeType_saapv"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeType_state_origi"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeType_state_end"), eFunctions.Values.eTypeData.etdLong))
                End If

                'UPGRADE_NOTE: Object mobjMantNoTraLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMantNoTraLife = Nothing

            '+ MVI5708 Validación de los campos del mantenimiento de la tabla 5708
            Case "MVI5708"
                With Request
                    mobjMantNoTraLife = New ePolicy.Table5708
                    If .QueryString("WindowType") = "PopUp" Then
                        insValMantNoTraLife = mobjMantNoTraLife.insValMVI5708(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Request.Form("tcnType_Move"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctDescript"), Request.Form("tctShort_Des"), mobjValues.StringToType(Request.Form("cbeType"), eFunctions.Values.eTypeData.etdDouble), Request.Form("chkPb_Bmg"), Request.Form("cbeStatregt"))
                    End If
                End With

            '+ MVI1488 Matriz de trnasacciones Registro histórico
            Case "MVI1488"
                mobjMantNoTraLife = New eSaapv.Tab_matrix_rh

                If Request.QueryString("WindowType") = "PopUp" Then
                    insValMantNoTraLife = mobjMantNoTraLife.insValMVI1488_k(Request.QueryString("Action"), mobjValues.StringToType(Request.Form("cbeType_move"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeOrigin"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeTyp_profitworker"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("tcnTransac"), eFunctions.Values.eTypeData.etdLong))
                End If

                'UPGRADE_NOTE: Object mobjMantNoTraLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMantNoTraLife = Nothing

                '+ MVI70
            Case "MVI70"
                mobjMantNoTraLife = New ePolicy.Fund_distribution
                If Request.QueryString("WindowType") = "PopUp" Then
                    insValMantNoTraLife = mobjMantNoTraLife.insValMVI70_upd(Request.QueryString("sCodispl"), Request.QueryString("Action"), _
                                                                  mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Session("nTypeProfile"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                  mobjValues.StringToType(Request.Form.Item("valFunds"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    If Request.QueryString("nZone") = 1 Then
                        insValMantNoTraLife = mobjMantNoTraLife.insValMVI70(Request.QueryString("sCodispl"), _
                                                                            mobjValues.StringToType(Request.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), _
                                                                      mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                      mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                                      mobjValues.StringToType(Request.Form.Item("cbeTypeProfile"), eFunctions.Values.eTypeData.etdDouble), _
                                                                      mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        insValMantNoTraLife = mobjMantNoTraLife.insValMVI70_upd("VALMAS", Request.QueryString("nMainAction"), _
                                                                      mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                      mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                                      mobjValues.StringToType(Session("nTypeProfile"), eFunctions.Values.eTypeData.etdDouble), _
                                                                      mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                      0, _
                                                                      0, _
                                                                      0)

                    End If
                End If


                'UPGRADE_NOTE: Object mobjMantNoTraLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMantNoTraLife = Nothing
                '+ MVI8022: Tabla de tasas de mercado.
            Case "MVI8022"
                mobjMantNoTraLife = New ePolicy.Tab_intproy

                With Request
                    If .QueryString("nZone") = 1 Then
                        mstrQueryString = "&dEffecdate=" & .Form("tcdEffecDate")

                        insValMantNoTraLife = mobjMantNoTraLife.insValMVI8022_k(Request.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If Request.QueryString("WindowType") = "PopUp" Then
                            mstrQueryString = "&nIntproy_min=" & mobjValues.StringToType(.Form("tcnIntproy_min"), eFunctions.Values.eTypeData.etdDouble) & "&nIntproy_max=" & mobjValues.StringToType(.Form("tcnIntproy_max"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & mobjValues.StringToType(.Form("hddNewEffecDate"), eFunctions.Values.eTypeData.etdDate) & "&nSvsproy_min=" & mobjValues.StringToType(.Form("tcnSvsproy_min"), eFunctions.Values.eTypeData.etdDouble) & "&nSvsproy_max=" & mobjValues.StringToType(.Form("tcnSvsproy_max"), eFunctions.Values.eTypeData.etdDouble) & "&nMonths_min=" & mobjValues.StringToType(.Form("tcnMonths_min"), eFunctions.Values.eTypeData.etdLong) & "&nMonths_max=" & mobjValues.StringToType(.Form("tcnMonths_max"), eFunctions.Values.eTypeData.etdLong)
                            insValMantNoTraLife = mobjMantNoTraLife.insValMVI8022(Request.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnIntproy_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnIntproy_max"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnSvsproy_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnSvsproy_max"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnMonths_min"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("tcnMonths_max"), eFunctions.Values.eTypeData.etdLong))
                        Else
                            insValMantNoTraLife = vbNullString
                        End If
                    End If
                End With
                mobjMantNoTraLife = Nothing
            Case Else
                insValMantNoTraLife = "insValMantNoTraLife: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '**% insPostMantNoTraLife: The updates of tables are performed.
    '% insPostMantNoTraLife: Se realizan las actualizaciones a las tablas.
    '--------------------------------------------------------------------------------------------
    Function insPostMantNoTraLife() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean

        lblnPost = False

        Select Case Request.QueryString.Item("sCodispl")

        '+ MVI012: % rentabilidad modalidad de inversion.

            Case "MVI012"
                mobjMantNoTraLife = New eBranches.Plan_intwar_day
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    lblnPost = True
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then

                        lblnPost = mobjMantNoTraLife.insPostMVI012(Request.QueryString.Item("Action"), _
                                                                   mobjValues.StringToType(Request.Form.Item("tcnTypeinvest"), eFunctions.Values.eTypeData.etdLong), _
                                                                   mobjValues.StringToType(Request.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), _
                                                                   mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                   mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End If
                mobjMantNoTraLife = Nothing

                '**+ MVI002: Nominal values of the funds.
                '+ MVI002: Valor nominal para los fondos.

            Case "MVI002"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjMantNoTraLife.insPostMVI002(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnFund"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

                '**+ MVI003: Investment funds.
                '+ MVI003: Fondos de inversión.

            Case "MVI003"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                        Session("dEffecdate") = .Form.Item("tcdEffecDate")
                    Else
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            mobjMantNoTraLife = New ePolicy.Fund_inv

                            lblnPost = mobjMantNoTraLife.insPostMVI003(Request.QueryString.Item("Action"),
                                                                    mobjValues.StringToType(Request.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(Request.Form.Item("tcnQuan_min"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(Request.Form.Item("tcnQuan_max"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(Request.Form.Item("tcnQuan_avail"), eFunctions.Values.eTypeData.etdDouble),
                                                                    .Form.Item("tctDescript"),
                                                                    .Form.Item("cbeStatregt"),
                                                                    mobjValues.StringToType(Request.Form.Item("tcdDinpdate"), eFunctions.Values.eTypeData.etdDate),
                                                                    Session("nUsercode"),
                                                                    mobjValues.StringToType(Request.Form.Item("tcnSeries"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(Request.Form.Item("tcnRun"), eFunctions.Values.eTypeData.etdDouble),
                                                                    mobjValues.StringToType(.Form.Item("cbenCountry"), eFunctions.Values.eTypeData.etdLong),
                                                                    .Form("tctRoutine"),
                                                                    .Form("chkGuaranteed"),
                                                                    .Form("tctTicker"),
                                                                    .Form("tctISIN_Code"))
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

                '**+ MVI005: Funds stock movements.
                '+ MVI005: Movimientos de stock de fondos.

            Case "MVI005"
                With Request
                    lblnPost = mobjMantNoTraLife.insPostMVI005_K(mobjValues.StringToType(.Form.Item("valFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMovetype"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnit"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToDate(.Form.Item("tcdEffecDate")))
                End With

                '**+ MVIC001: History of face values.
                '+ MVIC001: Histórico de valores nominales.

            Case "MVIC001"
                lblnPost = True

                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nFund") = mobjValues.StringToType(.Form.Item("cbeFund"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nCurrency") = mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
                    End If
                End With

                '+ MS7000: Tabla de instituciones financieras

            Case "MS7000"
                If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        mobjFundValue = New eBranches.Tab_Fn_Inst
                        lblnPost = mobjFundValue.InsPostMS7000Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnInstitution"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTypeInstitu"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctName"), mobjValues.StringToType(Request.Form.Item("cbeStatregt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), String.Empty, String.Empty, String.Empty)
                    Else
                        lblnPost = True
                    End If
                Else
                    lblnPost = True
                End If

                '+ MVI7000: Tabla de tarifas del seguro de ahorro previsional voluntario (APV).

            Case "MVI7000"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nBranch") = .Form.Item("cbeBranch")
                        Session("nProduct") = .Form.Item("valProduct")
                        Session("dEffecdate") = .Form.Item("tcdEffecdate")
                        Session("nModulec") = .Form.Item("valModulec")
                        Session("nCover") = .Form.Item("valCover")
                        Session("nRole") = .Form.Item("valRole")
                    End If
                    If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                        mobjFundValue = New eBranches.Tar_Apv

                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            Session("nBranch") = .Form.Item("cbeBranch")
                            Session("nProduct") = .Form.Item("valProduct")
                            Session("dEffecdate") = .Form.Item("tcdEffecdate")
                            Session("nModulec") = .Form.Item("valModulec")
                            Session("nCover") = .Form.Item("valCover")
                            Session("nRole") = .Form.Item("valRole")

                            mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nModulec=" & .Form.Item("valModulec") & "&nCover=" & .Form.Item("valCover") & "&nRole=" & .Form.Item("valRole")

                            lblnPost = True
                        Else
                            mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nModulec=" & .QueryString.Item("nModulec") & "&nCover=" & .QueryString.Item("nCover") & "&nRole=" & .QueryString.Item("nRole")

                            lblnPost = mobjFundValue.insPostMVI7000(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnFix_Cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnType_tar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCalcType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSex"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_year_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy_year_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkSmoking"), mobjValues.StringToType(.Form.Item("cbeTypeRisk"), eFunctions.Values.eTypeData.etdDouble))


                        End If
                    Else
                        lblnPost = True
                    End If
                End With

                '+ MVI7001: Tabla de costos fijos - APV.

            Case "MVI7001"
                If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    mobjFundValue = New eBranches.Tab_Ul_Costs

                    With Request
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            Session("nBranch") = Request.Form.Item("cbeBranch")
                            Session("nProduct") = Request.Form.Item("valProduct")

                            lblnPost = True
                        Else
                            If Request.QueryString.Item("WindowType") = "PopUp" Then
                                lblnPost = mobjFundValue.InsPostMVI7001(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth_From"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth_Until"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCost_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("nCreDeb"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTopAmount"), eFunctions.Values.eTypeData.etdDouble))
                            Else
                                lblnPost = True
                            End If
                        End If
                    End With
                Else
                    lblnPost = True
                End If

                '+ MVI7002: Tabla de orden de uso de las cuentas origen para pagar cargos (APV).
                '+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
            Case "MVI7002"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    Session("nBranch") = mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nProduct") = mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                    lblnPost = True
                Else
                    If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                        If Request.QueryString.Item("WindowType") = "PopUp" Then
                            mobjFundValue = New eBranches.Tab_Ord_Origin
                            lblnPost = mobjFundValue.InsPostMVI7002Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), vbNullString, Integer.MinValue, vbNullString, mobjValues.StringToDate(Session("dEffecdate")), Integer.MinValue)

                        Else
                            lblnPost = True
                        End If
                    Else
                        lblnPost = True
                    End If
                End If

                '+ MVI8001: Tabla de porcentaje de descuento pro primas para vida no tradicional.

            Case "MVI8001"
                With Request
                    If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then

                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            Session("nBranch") = .Form.Item("cbeBranch")
                            Session("nProduct") = .Form.Item("valProduct")
                            Session("dEffecdate") = .Form.Item("tcdEffecdate")
                            Session("nModulec") = .Form.Item("valModulec")
                            Session("nCover") = .Form.Item("valCover")
                            Session("nRole") = .Form.Item("valRole")

                            mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nModulec=" & .Form.Item("valModulec") & "&nCover=" & .Form.Item("valCover") & "&nRole=" & .Form.Item("valRole")

                            lblnPost = True
                        Else
                            mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nModulec=" & .QueryString.Item("nModulec") & "&nCover=" & .QueryString.Item("nCover") & "&nRole=" & .QueryString.Item("nRole")

                            lblnPost = mobjMantNoTraLife.insPostMVI8001(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQprempayed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDisc_percentage"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkExtraprem"))
                        End If
                    Else
                        lblnPost = True
                    End If
                End With

                '+ MVI8003: Tabla de porcentaje de descuento por poliza para vida no tradicional.

            Case "MVI8003"
                With Request
                    If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then

                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            Session("nBranch") = .Form.Item("cbeBranch")
                            Session("nProduct") = .Form.Item("valProduct")
                            Session("dEffecdate") = .Form.Item("tcdEffecdate")
                            Session("nModulec") = .Form.Item("valModulec")
                            Session("nCover") = .Form.Item("valCover")
                            Session("nRole") = .Form.Item("valRole")
                            Session("nTyperisk") = .Form.Item("cbeTyperisk")
                            mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nModulec=" & .Form.Item("valModulec") & "&nCover=" & .Form.Item("valCover") & "&nRole=" & .Form.Item("valRole") & "&nTyperisk=" & .Form.Item("cbeTyperisk")

                            lblnPost = True
                        Else
                            mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nModulec=" & .QueryString.Item("nModulec") & "&nCover=" & .QueryString.Item("nCover") & "&nRole=" & .QueryString.Item("nRole")

                            lblnPost = mobjMantNoTraLife.insPostMVI8003(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth_init"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMonth_end"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDisc_percentage"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcndisc_perc_year_exc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nTyperisk"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDisc_perc_year_nrec"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If
                    Else
                        lblnPost = True
                    End If
                End With
            Case "MVI7300"
                mobjMantNoTraLife = New eSaapv.Ul_Legal_Terms

                If Request.QueryString("nZone") = 1 Then
                    Session("dEffecdate") = Request.Form("tcdEffecdate")
                    lblnPost = True
                Else
                    If Request.QueryString("WindowType") = "PopUp" Then
                        lblnPost = mobjMantNoTraLife.insPostMVI7300(Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("cbeType_saapv"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeValuesmo"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeValuesty"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("tcnDayadd"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))
                    Else
                        lblnPost = True
                    End If
                End If

                'UPGRADE_NOTE: Object mobjMantNoTraLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMantNoTraLife = Nothing

            Case "MVI7500"
                mobjMantNoTraLife = New eSaapv.Tab_state_saapv

                If Request.QueryString("WindowType") = "PopUp" Then
                    lblnPost = mobjMantNoTraLife.insPostMVI7500(Request.QueryString("Action"), mobjValues.StringToType(Request.Form("cbeType_saapv"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeType_state_origi"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeType_state_end"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))
                Else
                    lblnPost = True
                End If

                'UPGRADE_NOTE: Object mobjMantNoTraLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMantNoTraLife = Nothing

                '+ MVI5708 Actualización de los campos del mantenimiento de la tabla 5708
            Case "MVI5708"
                With Request
                    mobjMantNoTraLife = New ePolicy.Table5708
                    If .QueryString("WindowType") = "PopUp" Then
                        lblnPost = mobjMantNoTraLife.insPostMVI5708(Request.QueryString("Action"), mobjValues.StringToType(Request.Form("tcnType_Move"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctDescript"), Request.Form("tctShort_Des"), mobjValues.StringToType(Request.Form("cbeType"), eFunctions.Values.eTypeData.etdDouble), Request.Form("chkPb_Bmg"), Request.Form("cbeStatregt"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With
                'UPGRADE_NOTE: Object mobjMantNoTraLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMantNoTraLife = Nothing
                '+ MVI1488 Matriz de trnasacciones Registro histórico
            Case "MVI1488"
                mobjMantNoTraLife = New eSaapv.Tab_matrix_rh

                If Request.QueryString("WindowType") = "PopUp" Then
                    lblnPost = mobjMantNoTraLife.insPostMVI1488(Request.QueryString("Action"), mobjValues.StringToType(Request.Form("cbeType_move"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeOrigin"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("cbeTyp_profitworker"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("tcnTransac"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))
                Else
                    lblnPost = True
                End If

                'UPGRADE_NOTE: Object mobjMantNoTraLife may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMantNoTraLife = Nothing

            Case "MVI70"
                mobjMantNoTraLife = New ePolicy.Fund_distribution

                If Request.QueryString("WindowType") = "PopUp" Then
                    lblnPost = mobjMantNoTraLife.InsPostmvi70_Upd(Request.QueryString("sCodispl"), Request.QueryString("Action"), _
                                                                  mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Session("nTypeProfile"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                  mobjValues.StringToType(Request.Form.Item("valFunds"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), _
                                                                  mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))
                Else
                    lblnPost = True
                    Session("nBranch") = Request.Form.Item("cbeBranch")
                    Session("nProduct") = Request.Form.Item("valProduct")
                    Session("nTypeProfile") = Request.Form.Item("cbeTypeProfile")
                    Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
                End If

                '+ MVI8022: Tabla de tasas de mercado.
            Case "MVI8022"
                With Request
                    If .QueryString("nZone") = 1 Then
                        lblnPost = True
                    Else
                        If .QueryString("WindowType") = "PopUp" Then
                            mobjMantNoTraLife = New ePolicy.Tab_intproy
                            lblnPost = mobjMantNoTraLife.insPostMVI8022(.QueryString("Action"), mobjValues.StringToType(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnIntproy_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnIntproy_max"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), mobjValues.StringToType(.Form("tcnSvsproy_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnSvsproy_max"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnMonths_min"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("tcnMonths_max"), eFunctions.Values.eTypeData.etdLong))
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

        End Select

        insPostMantNoTraLife = lblnPost
    End Function

</script>
<%Response.Expires = -1

mstrCommand = "&sModule=Maintenance&sProject=MantNoTraLife&sCodisplReload=" & Request.QueryString.Item("sCodispl")
mstrQueryString = vbNullString
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If

mobjValues = New eFunctions.Values

If Request.Form.Item("sCodisplReload") = vbNullString Then
	
	'**+ The validation routines id performed. 
	'+ Se ejecuta la rutina de validaciones.
	
	mstrErrors = insValMantNoTraLife
	Session("sErrorTable") = mstrErrors
Else
	Session("sErrorTable") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""AgentSeqErrors"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	
	'**+ The updates routines is performed.
	'+ Se ejecuta la rutina de actualizaciones.
	
	If insPostMantNoTraLife Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.QueryString.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
				End If
			Else
				Select Case Request.QueryString.Item("sCodispl")
					Case "MVI005"
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
					Case "MVI002"
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
						End If
	                                           Case "MVI012"
	                                               If Request.Form.Item("sCodisplReload") = vbNullString Then
	                                                   Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
	                                               Else
	                                                   Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
	                                               End If

	                                           Case "MVIC001"
	                                               Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					Case "MVI7000"
						Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					Case "MVI7001"
						Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					Case "MVI7002"
						Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					Case "MVI8001"
						Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					Case "MVI8003"
                        Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
                    Case "MVI8003"
                        Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
                    Case "MVI8022"
                        Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString("nMainAction") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & """;</SCRIPT>")
					Case Else
						Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End Select
			End If
		Else
			
			'**+ The opener of the Pop Up window is reloaded.
			'+ Se recarga la página que invocó la PopUp.
			
			Select Case Request.QueryString.Item("sCodispl")
                Case "MVI012"
                    If Request.Form("sCodisplReload") = vbNullString Then
                        Response.Write("<SCRIPT>top.opener.document.location.href='MVI012.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='MVI012.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
                    End If
                Case "MVI002"
                    Response.Write("<SCRIPT>top.opener.document.location.href='MVI002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
				Case "MVI003"
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.opener.document.location.href='MVI003_K.aspx?sCodispl=MVI003&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='MVI003_K.aspx?sCodispl=MVI003&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
					End If
				Case "MVIC001"
					Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
				Case "MS7000"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS7000_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MVI7000"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI7000.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
				Case "MVI7001"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI7001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MVI7002"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI7002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MVI8001"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI8001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
				Case "MVI8003"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI8003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
				Case "MVI7300"
					If Request.Form("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.opener.document.location.href='MVI7300.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='MVI7300.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
					End If
				Case "MVI7500"
					If Request.Form("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.opener.document.location.href='MVI7500_K.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='MVI7500_K.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
					End If
					'+ MVI5708: Tipo de movimiento
				Case "MVI5708"
					Response.Write("<SCRIPT>top.opener.document.location.href='MVI5708_k.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MVI1488"
					If Request.Form("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.opener.document.location.href='MVI1488_K.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='MVI1488_K.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
					End If
                Case "MVI70"
                    If Request.Form("sCodisplReload") = vbNullString Then
                        Response.Write("<SCRIPT>top.opener.document.location.href='MVI70.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='MVI70.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
                    End If
                Case "MVI8022"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MVI8022.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
                End Select
	                                           
                
		End If
	                           Else
	                               Response.Write("El POST arrojo falso")
	                           End If
End If

mobjValues = Nothing
mobjMantNoTraLife = Nothing
mobjValues = Nothing
mobjFundValue = Nothing

%>
</BODY>
</HTML>





