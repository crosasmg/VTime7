<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    Dim mobjMantGeneral As Object
    Dim mstrErrors As String
    Dim mobjValues As eFunctions.Values
    Dim mstrString As Object

    '- Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String


    '% insValMantGeneral: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insValMantGeneral() As String
        '--------------------------------------------------------------------------------------------
        Select Case Request.QueryString.Item("sCodispl")
        '+MS003: Agencias bancarias
            Case "MS003"
                mobjMantGeneral = New eCashBank.Tab_bk_age
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValMantGeneral = mobjMantGeneral.valMS003_K(.QueryString("nMainAction"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble))
                        Session("nBank_code") = .Form.Item("cbeBank")
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValMantGeneral = mobjMantGeneral.valMS003(.QueryString("Action"), mobjValues.StringToType(Session("nBank_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nBk_agency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sDescript"), .Form.Item("sShort_des"), .Form.Item("sStatregt"))
                        End If
                    End If
                End With

            '+ MS105: Actualización de Códigos Postales - NDCB - 16/7/2001.
            Case "MS105"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjMantGeneral = New eGeneralForm.Zip_code
                    With Request
                        insValMantGeneral = mobjMantGeneral.insValMS105("MS105", .Form.Item("sAction"), mobjValues.StringToType(.Form.Item("tcnZip_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valLocal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuto_zone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                    mobjMantGeneral = Nothing
                Else
                    insValMantGeneral = vbNullString
                End If

            '+ MS108: Actualización de Ciudades - NDCB - 9/7/2001.
            Case "MS108"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjMantGeneral = New eGeneralForm.Tab_locat
                    With Request
                        insValMantGeneral = mobjMantGeneral.insValMS108("MS108", mobjValues.StringToType(.Form.Item("tcnLocal"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), mobjValues.StringToType(.Form.Item("tcnProvince"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctLegal_loc"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), "PopUp")
                    End With
                    mobjMantGeneral = Nothing
                Else
                    insValMantGeneral = vbNullString
                End If

            '+ MS112: Actualización de Comunas
            Case "MS112"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjMantGeneral = New eGeneralForm.Municipality
                    With Request
                        insValMantGeneral = mobjMantGeneral.insValMS112("MS112", mobjValues.StringToType(.Form.Item("tcnMunicipality"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), mobjValues.StringToType(.Form.Item("tcnLocal"), eFunctions.Values.eTypeData.etdLong, True), .QueryString("Action"))
                    End With
                    mobjMantGeneral = Nothing
                Else
                    insValMantGeneral = vbNullString
                End If

            '+ MS109 : Actualización de provincias	
            Case "MS109"
                mobjMantGeneral = New eGeneralForm.Province
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insValMantGeneral = mobjMantGeneral.valMS109_K(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnProvince"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"))
                    End If
                End With

            '+ MS110: Actualización de Compañías - NDCB - 13/7/2001.
            Case "MS110"
                mobjMantGeneral = New eGeneral.Company
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValMantGeneral = mobjMantGeneral.insValMS110_K("MS110", mobjValues.StringToType(.Form.Item("tcnCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValMantGeneral = mobjMantGeneral.insValMS110_Upd("MS110", .QueryString("Action"), Session("nCompany"), .Form.Item("cbeCompany_det"), .Form.Item("tcnCompanyType"), .Form.Item("tcnClasific"))

                        Else
                            If CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) <> 401 Then
                                insValMantGeneral = mobjMantGeneral.insValMS110("MS110", Session("nCompany"), .Form.Item("tcnClient"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInputDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valStatus"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCompanyType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnTaxrate"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctBank"), .Form.Item("tctAccount"), mobjValues.StringToType(.Form.Item("opnCountry"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcsRegsvs"), mobjValues.StringToType(.Form.Item("opnClassific"), eFunctions.Values.eTypeData.etdDouble, True))
                            End If
                        End If
                    End If
                End With
                mobjMantGeneral = Nothing

            '+ MS010_K: Opciones de instalación del sistema	
            Case "MS010_K"
                mobjMantGeneral = New eGeneral.OptionsInstallation

                insValMantGeneral = mobjMantGeneral.insValMS010_K(Request.Form.Item("tctPassword"), "TIME")
                Session("bQuery") = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401

                mobjMantGeneral = Nothing

            '+ MS010: valida las características generales de las opciones de instalación
            Case "MS010"
                mobjMantGeneral = New eGeneral.OptionsInstallation
                With mobjValues
                    insValMantGeneral = mobjMantGeneral.insValMS010(.StringToType(Request.Form.Item("tcddInit_date"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.Form.Item("tcnConpanyUser"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeCountry"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcdInstallDate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("WindowType") = "PopUp", .StringToType(Request.Form.Item("tcddInit_date_aux"), eFunctions.Values.eTypeData.etdDate))
                End With

            '+ MCO001: valida la ventana de las opciones de instalación de cobranza
            Case "MCO001"
                mobjMantGeneral = New eGeneral.OptionsInstallation

                With mobjValues

                    insValMantGeneral = mobjMantGeneral.insValMCO001(.StringToType(Request.Form.Item("valAccount"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("cbeCalInt"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnCalIntFix"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnCollAdd"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnCollSub"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnLower_Agree"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnUpper_Agree"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnUpperPercent"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnUpperPercentAgree"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnLowerPercent"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnLowerPercentAgree"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnUpperPercentAMO"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnUpperPercentAgreeAMO"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnLowerPercentAMO"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("tcnLowerPercentAgreeAMO"), eFunctions.Values.eTypeData.etdDouble, True),
                                                                     .StringToType(Request.Form.Item("cbenTolerCurr"), eFunctions.Values.eTypeData.etdInteger, True))

                End With
                mobjMantGeneral = Nothing
            '+ MOP001: valida la ventana de las opciones de instalación de Caja y Banco
            Case "MOP001"
                insValMantGeneral = vbNullString

            '+ MCA000  : Opciones de instalación de Pólizas
            Case "MCA000"
                mobjMantGeneral = New eGeneral.OptionsInstallation
                With mobjValues
                    insValMantGeneral = mobjMantGeneral.insValMCA000(.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeSalePol"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

            Case "MS013"
                With Request
                    mobjMantGeneral = New eGeneral.Tab_tables
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insValMantGeneral = mobjMantGeneral.insValMS013_K(.QueryString("Action"), .QueryString("sCodispl"), .Form.Item("tcsTab_code"), mobjValues.StringToType(.Form.Item("tcnCount_item"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsCode_item"), .Form.Item("tcsDesc_item"), mobjValues.StringToType(.Form.Item("tcnCount_tabl"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsDescript"), .Form.Item("tcsDs_select"), .Form.Item("tcsQ_value"), Session("nUsercode"), .Form.Item("tcsShowNum"), .Form.Item("tcsInitQuery"), .Form.Item("tcsIndSp"), .Form.Item("tcsKey"))
                    End If
                End With

            '+ MSI017  : valida la ventana de las opciones de instalación de Siniestros
            Case "MSI017"
                mobjMantGeneral = New eGeneral.OptionsInstallation
                With mobjValues
                    insValMantGeneral = mobjMantGeneral.insValMSI017(.StringToType(Request.Form.Item("cbeCurrencyClaim"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("cbeSection"), eFunctions.Values.eTypeData.etdInteger), .StringToType(Request.Form.Item("tcnDaysSection"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnCostMin"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnCostMax"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnPercent2"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+ MS821 : Actualización de dias feriados
            Case "MS821"
                mobjMantGeneral = New eGeneralForm.Hollidays
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insValMantGeneral = mobjMantGeneral.valMS821_K(.QueryString("Action"), mobjValues.StringToType(.Form.Item("valMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDay"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("valCountry"), eFunctions.Values.eTypeData.etdDouble))


                    End If
                End With


            '+ MCR002  : valida la ventana de las opciones de instalación de CoReaseguro
            Case "MCR002"
                insValMantGeneral = vbNullString

            '+ Se valida para Opciones de Financiamiento             
            Case "MFI023"
                mobjMantGeneral = New eGeneral.OptFinance

                With mobjValues
                    insValMantGeneral = mobjMantGeneral.insValMFI023(.StringToType(Request.Form.Item("tcnDefaulti"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnDsctoAmo"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cboCurrency"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

                mobjMantGeneral = Nothing
            '+ Se valida para Opciones de Financiamiento             
            Case "MAG978"
                mobjMantGeneral = New eGeneral.OptionsInstallation

                With mobjValues
                    insValMantGeneral = mobjMantGeneral.insValMAG978(.StringToType(Request.Form.Item("tcnQM_MinDurat"), eFunctions.Values.eTypeData.etdInteger, True), .StringToType(Request.Form.Item("tcnMonth_Expiry"), eFunctions.Values.eTypeData.etdInteger, True), .StringToType(Request.Form.Item("tcnMonth_Punish"), eFunctions.Values.eTypeData.etdInteger, True))
                End With

                mobjMantGeneral = Nothing

            Case Else
                insValMantGeneral = "insValMantGeneral: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostMantGeneral: Se realizan las actualizaciones a las tablas
    '--------------------------------------------------------------------------------------------
    Function insPostMantGeneral() As Boolean
        Dim lintPartial As Byte
        Dim nQuotNumAut As Byte
        Dim nPEP As Byte
        Dim nUsperson As Byte
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean

        lblnPost = False
        Select Case Request.QueryString.Item("sCodispl")

        '+MS003: Agencias bancarias
            Case "MS003"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lblnPost = True
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            mobjMantGeneral = New eCashBank.Tab_bk_age
                            lblnPost = mobjMantGeneral.insPostMS003(.QueryString("Action"), mobjValues.StringToType(Session("nBank_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nBk_agency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sDescript"), .Form.Item("sShort_des"), .Form.Item("sStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            lblnPost = True
                        End If
                    End If
                End With

            '+ MS105: Actualización de Códigos Postales - NDCB - 16/7/2001.
            Case "MS105"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjMantGeneral = New eGeneralForm.Zip_code
                    With Request
                        lblnPost = mobjMantGeneral.insPostMS105(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnZip_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valLocal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAuto_zone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                    mobjMantGeneral = Nothing
                Else
                    lblnPost = True
                End If

            '+ MS108: Actualización de Localidades - NDCB - 9/7/2001.
            Case "MS108"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjMantGeneral = New eGeneralForm.Tab_locat
                    With Request
                        lblnPost = mobjMantGeneral.insPostMS108(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLocal"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), mobjValues.StringToType(.Form.Item("tcnProvince"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctLegal_loc"))
                    End With
                Else
                    lblnPost = True
                End If
                mobjMantGeneral = Nothing

            '+ MS112: Actualización de Comunas
            Case "MS112"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjMantGeneral = New eGeneralForm.Municipality
                    With Request
                        lblnPost = mobjMantGeneral.insPostMS112(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnMunicipality"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), mobjValues.StringToType(.Form.Item("tcnLocal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                Else
                    lblnPost = True
                End If
                mobjMantGeneral = Nothing

            '+MS109: Actualización de provincias	
            Case "MS109"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjMantGeneral = New eGeneralForm.Province
                        lblnPost = mobjMantGeneral.insPostMS109(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnProvince"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With

            '+ MS110: Actualización de Compañías - NDCB - 13/7/2001.
            Case "MS110"
                mobjMantGeneral = New eGeneral.Company
                With Request
                    lblnPost = True
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("nCompany") = .Form.Item("tcnCompany")
                    Else

                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjMantGeneral.InsPostMS110Upd(.QueryString("Action"), Session("nCompany"), .Form.Item("cbeCompany_det"), Session("nUsercode"))

                        Else
                            If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
                                lblnPost = mobjMantGeneral.insPostMS110(.Form.Item("tcnClient"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInputDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("valStatus"), .Form.Item("valCompanyType"), mobjValues.StringToType(.Form.Item("tcnTaxrate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctBank"), .Form.Item("tctAccount"), mobjValues.StringToType(Session("nCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("opnCountry"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsRegsvs"), mobjValues.StringToType(.Form.Item("opnClassific"), eFunctions.Values.eTypeData.etdDouble, True))
                            End If
                            Session("nCompany") = ""
                        End If
                    End If
                End With
                mobjMantGeneral = Nothing

            '+ MS010_K: Opciones de instalación del sistema
            Case "MS010_K"
                lblnPost = True
                mobjMantGeneral = Nothing

            '+ MS010: opciones de instalación generales	
            Case "MS010"
                With mobjValues
                    If Request.QueryString.Item("WindowType") <> "PopUp" Then
                        If Request.Form.Item("chkQuotNumAut") = "1" Then
                            nQuotNumAut = 1
                        Else
                            nQuotNumAut = 2
                        End If

                        If Request.Form.Item("chkPEP") = "1" Then
                            nPEP = 1
                        Else
                            nPEP = 2
                        End If

                        If Request.Form.Item("chkUSPERSON") = "1" Then
                            nUsperson = 1
                        Else
                            nUsperson = 2
                        End If

                        lblnPost = mobjMantGeneral.insPostMSI010(.StringToType(Request.Form.Item("tcddInit_date"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctPersonFor"), Request.Form.Item("tctCompanyFor"), .StringToType(Request.Form.Item("cbeCountry"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnConpanyUser"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True), "MS010", vbNullString, Request.Form.Item("cbeNumPolicy"), Request.Form.Item("cbeNumClaim"), Request.Form.Item("cbeNumReceipt"), Request.Form.Item("cbeSecure"), nQuotNumAut, nPEP, nUsperson)
                    Else
                        Try
                            lblnPost = mobjMantGeneral.insPostMSI010Mod(Request.QueryString.Item("Action"), Session("nUsercode"), .StringToType(Request.Form.Item("tcnModule"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcdInstallDate"), eFunctions.Values.eTypeData.etdDate))
                        Catch ex As Exception
                            lblnPost = True
                        End Try
                    End If
                End With
            'If lblnPost Then Session("sPolicyNum")


            '+MS821: Actualización de dias feriados
            Case "MS821"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjMantGeneral = New eGeneralForm.Hollidays
                        lblnPost = mobjMantGeneral.insPostMS821(.QueryString("Action"), mobjValues.StringToType(.Form.Item("valMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDay"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCountry"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With


            '+ MCO001: opciones de instalación de cobrabza 
            Case "MCO001"
                mobjMantGeneral = New eGeneral.OptionsInstallation

                With mobjValues
                    lblnPost = mobjMantGeneral.insPostMCO001(.StringToType(Request.Form.Item("tcnCollSub"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnCollAdd"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             Request.Form.Item("chkPartialCol"), Request.Form.Item("chkDateFix_Cash"), Request.Form.Item("chkAmountReq"),
                                                             .StringToType(Request.Form.Item("tcnCalIntAdd"), eFunctions.Values.eTypeData.etdDouble, False),
                                                             .StringToType(Request.Form.Item("tcnCalIntSub"), eFunctions.Values.eTypeData.etdDouble, False),
                                                             Session("nUsercode"), .StringToType(Request.Form.Item("valAccount"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             Request.Form.Item("chkNullTransac"), .StringToType(Request.Form.Item("tcnCalIntFix"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnCalIntLev"), eFunctions.Values.eTypeData.etdDouble, False),
                                                             .StringToType(Request.Form.Item("cbePrevReceipt"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("cbeCalInt"), eFunctions.Values.eTypeData.etdDouble, True), Today,
                                                             Request.Form.Item("chkCalIntSub"), Request.Form.Item("chkCalIntAdd"),
                                                             .StringToType(Request.Form.Item("cbeCurrcollectexp"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnCollect_exp"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             Request.Form.Item("tctClient"),
                                                             .StringToType(Request.Form.Item("tcnLower_Agree"), eFunctions.Values.eTypeData.etdDouble),
                                                             .StringToType(Request.Form.Item("tcnUpper_Agree"), eFunctions.Values.eTypeData.etdDouble),
                                                             .StringToType(Request.Form.Item("tcnUpperPercent"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnUpperPercentAgree"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnLowerPercent"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnLowerPercentAgree"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnUpperPercentAMO"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnUpperPercentAgreeAMO"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnLowerPercentAMO"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("tcnLowerPercentAgreeAMO"), eFunctions.Values.eTypeData.etdDouble, True),
                                                             .StringToType(Request.Form.Item("cbenTolerCurr"), eFunctions.Values.eTypeData.etdInteger, True),
                                                             .StringToType(Request.Form.Item("cbenToler"), eFunctions.Values.eTypeData.etdInteger, True))
                End With

            '+ MOP001: opciones de instalación de Caja y Banco               
            Case "MOP001"
                If Request.Form.Item("chkPartialCol") = vbNullString Then
                    lintPartial = 2
                Else
                    lintPartial = 1
                End If
                With mobjValues
                    mobjMantGeneral = New eGeneral.OptionsInstallation
                    lblnPost = mobjMantGeneral.insPostMOP001(.StringToType(Session("dInstalldateCash"), eFunctions.Values.eTypeData.etdDate), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), lintPartial, .StringToType(Request.Form.Item("cbeBalance"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeinsur_area"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnExpenses"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("tcnFinanInt"), eFunctions.Values.eTypeData.etdDouble, True))
                End With
            '+ MCA000  : opciones de instalación de Pólizas
            Case "MCA000"
                With mobjValues
                    lblnPost = mobjMantGeneral.insPostMCA000(.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeCurrencyPol"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("cbeSalePol"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chkPrintClause"), Request.Form.Item("chkSTock_ind"))
                End With

            Case "MS013"
                With Request
                    mobjMantGeneral = New eGeneral.Tab_tables
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        If (.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionCondition)) Then
                            lblnPost = mobjMantGeneral.insPostMS013_K(.QueryString("Action"), .Form.Item("tcsTab_code"), mobjValues.StringToType(.Form.Item("tcnCount_item"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsCode_item"), .Form.Item("tcsDesc_item"), mobjValues.StringToType(.Form.Item("tcnCount_tabl"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tcsDescript"), .Form.Item("tcsDs_select"), .Form.Item("tcsQ_value"), Session("nUsercode"), .Form.Item("tcsShowNum"), .Form.Item("tcsInitQuery"), .Form.Item("tcsIndSp"), .Form.Item("tcsKey"))
                        Else
                            lblnPost = True
                        End If
                    Else
                        lblnPost = True
                    End If
                End With

            '+ MSI017  : opciones de instalación de siniestros
            Case "MSI017"
                With mobjValues
                    lblnPost = mobjMantGeneral.insPostMSI017(Session("nUsercode"), .StringToType(Request.Form.Item("cbeCurrencyClaim"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chkTaxReserve"), .StringToType(Request.Form.Item("cbeSection"), eFunctions.Values.eTypeData.etdInteger), .StringToType(Request.Form.Item("tcnDaysSection"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnCostMin"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnCostMax"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnMaxDays"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("cbeSimplified"), eFunctions.Values.eTypeData.etdInteger), .StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger), .StringToType(Request.Form.Item("cbetransitory"), eFunctions.Values.eTypeData.etdInteger), .StringToType(Request.Form.Item("tcnYear2"), eFunctions.Values.eTypeData.etdInteger))
                End With

            '+ MCR002  : opciones de instalación de CorReaseguro           
            Case "MCR002"
                mobjMantGeneral = New eGeneral.OptionsInstallation
                With mobjValues
                    lblnPost = mobjMantGeneral.insPostMCR002(Session("nUsercode"), Request.Form.Item("chkCesPreCoa"), Nothing, Request.Form.Item("chkCesPreReaFac"), Request.Form.Item("chkCesPreReaObl"), .StringToType(Request.Form.Item("optNetPremium"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

            Case "MFI023"
                mobjMantGeneral = New eGeneral.OptFinance
                With mobjValues

                    lblnPost = mobjMantGeneral.insPostMFI023(.StringToType(Request.Form.Item("cbeOptDra"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchOptDra"), .StringToType(Request.Form.Item("tcnLevelDra"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("cboOptNull"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchOptNull"), .StringToType(Request.Form.Item("tcnLevelNull"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnDefaulti"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchUp"), .StringToType(Request.Form.Item("tcnIntUp"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchDown"), .StringToType(Request.Form.Item("tcnIntDown"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnLevelFin"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSoptIntere"), Request.Form.Item("chkSchOptIntere"), .StringToType(Request.Form.Item("tcnLevelInitial"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("optSInterestE"), Request.Form.Item("optSTimeExa"), .StringToType(Request.Form.Item("tcnIntDelay"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchDelUp"), .StringToType(Request.Form.Item("tcnIntDelUp"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchDelDown"), .StringToType(Request.Form.Item("tcnIntDelDown"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnLevelDelay"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("cboOptComm"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchOptComm"), .StringToType(Request.Form.Item("tcnLevelComm"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnDsctoPag"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnDsctoAmo"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("cboCurrency"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchPayUp"), .StringToType(Request.Form.Item("tcnPayUp"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSchPayDown"), .StringToType(Request.Form.Item("tcnPayDown"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnLevelPay"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))

                End With
                mobjMantGeneral = Nothing

            Case "MAG978"
                mobjMantGeneral = New eGeneral.OptionsInstallation
                With mobjValues

                    lblnPost = mobjMantGeneral.insPostMAG978(.StringToType(Request.Form.Item("tcnQM_MinDurat"), eFunctions.Values.eTypeData.etdInteger, True), .StringToType(Request.Form.Item("tcnMonth_Expiry"), eFunctions.Values.eTypeData.etdInteger, True), .StringToType(Request.Form.Item("tcnMonth_Punish"), eFunctions.Values.eTypeData.etdInteger, True), Session("nUserCode"))
                End With
                mobjMantGeneral = Nothing

        End Select

        insPostMantGeneral = lblnPost
    End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mstrCommand = "sModule=Maintenance&sProject=MantGeneral&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




	<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE002"))
End With
%> 
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 16-03-06 8:53 $|$$Author: Jguajardo $"
	
//% NewLocation: se recalcula la ruta de la página
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>
<BODY>
<FORM id=form1 name=form1>
<%
'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantGeneral
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantGeneralError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantGeneral Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.insReloadTop(true,false);</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.QueryString.Item("sCodispl") = "MS010_K" Then
						Response.Write("<SCRIPT>self.history.go(-1); top.frames['fraSequence'].document.location='/VTimeNet/Maintenance/MantGeneral/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					Else
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
						End If
					End If
				Else
					
					'+ Se realiza un manejo especial pues de tratarse de algún frame de la secuencia, tiene que recargar el fraSequence.
					Select Case Request.QueryString.Item("sCodispl")
						Case "MS010", "MFI023", "MCO001", "MOP001", "MCA000", "MSI017", "MCR002", "MAG978"
							Response.Write("<SCRIPT>self.history.go(-1); top.frames['fraSequence'].document.location='/VTimeNet/Maintenance/MAntGeneral/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sOriginalForm=" & Request.Form.Item("tctOriginalForm") & "';</SCRIPT>")
						Case Else
							Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End Select
				End If
			End If
		Else
			
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "MS003"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS105"
					Response.Write("<SCRIPT>opener.document.location.href='MS105_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS108"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS108_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS112"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS112_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS109"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS109_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS010"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS010.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS013"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS013_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "MCO001"
					Response.Write("<SCRIPT>top.opener.document.location.href='MCO001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS821"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS821_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MS110"
					Response.Write("<SCRIPT>top.opener.document.location.href='MS110.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
			End Select
		End If
	End If
End If

mobjMantGeneral = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




