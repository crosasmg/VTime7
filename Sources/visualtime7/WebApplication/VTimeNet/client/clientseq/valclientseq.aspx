<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

    Dim mobjValues As eFunctions.Values
    Dim mobjClientSeq As eClient.ClientSeq
    Dim mobjUserWeb As eClient.UsersWeb
    Dim mobjClient_evalrisk As eClient.Client_evalrisk
    Dim mobjClient_SF As eClient.Client_SF
    Dim lobjDir_debit_cli As eClient.Dir_debit_cli
    Dim lobjCred_card As eClient.cred_card
    Dim lobjBk_Account As eClient.bk_account
    Dim lobjAddress As Object
    Dim lobjPhones As eGeneralForm.GeneralForm
    Dim mstrErrors As String
    Dim mstrQueryString As String
    Dim mblnReload As Object

    '- Se define la contante para el manejo de errores en caso de advertencias   
    Dim mstrCommand As String

    Dim lstrReload As String
    Dim mobjCliDocuments As eClient.CliDocuments

    ''' <summary>
    ''' Se realizan las validaciones masivas de la forma
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function insvalSequence() As String
        Dim lobjClient As eClient.Client
        Dim lstrClient As String
        Dim lstrStatRegt As String
        Dim lstrSdeposit As String
        Dim lclsImages As eGeneralForm.GeneralForm
        Dim lclsNotes As eGeneralForm.GeneralForm

        Select Case Request.QueryString.Item("sCodispl")
            Case "BC003_K"
                lstrClient = insGetNewClient(UCase(Request.Form.Item("tctClient")))

                insvalSequence = mobjClientSeq.insvalBC003_K("BC003_K", vbNullString & lstrClient, 0 + CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))), Session("nUserCode"), mobjValues.StringToType(Request.Form.Item("cbePerson_typ"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctClient_digit"))

                If insvalSequence = vbNullString Then
                    lobjClient = New eClient.Client
                    Session("sClient") = lobjClient.ExpandCode(UCase(lstrClient))
                    Session("nPerson_typ") = Request.Form.Item("cbePerson_typ")
                    Session("Digit") = Request.Form.Item("tctClient_digit")
                    lobjClient = Nothing
                End If

            Case "BC001N"
                insvalSequence = mobjClientSeq.insValBC001N("BC001N", 0, CInt(Request.QueryString.Item("nMainAction")), Session("nPerson_typ"), mobjValues.StringToType(Request.Form.Item("tcdInpDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctLastName"), Request.Form.Item("tctFirstName"), mobjValues.StringToType(Request.Form.Item("cbeCivilsta"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeSex"), mobjValues.StringToType(Request.Form.Item("cbeNationality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOccupat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdBirthDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDriverDat"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctDriverNum"), mobjValues.StringToType(Request.Form.Item("tcdDeathdate"), eFunctions.Values.eTypeData.etdDate), False, Request.Form.Item("tctLastName2"), mobjValues.StringToType(Request.Form.Item("tcdDrivExpDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdWedd"), eFunctions.Values.eTypeData.etdDate))

            Case "BC001J"
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjClientSeq.insValBC001JContac(.QueryString.Item("Action"), "BC001J", 0, Session("sClient"), .Form.Item("tctClientr"), .Form.Item("tcnOrder"), .Form.Item("tcnPosition"))
                    Else

                        insvalSequence = mobjClientSeq.insValBC001J_K("BC001J", 0, CInt(Request.QueryString.Item("nMainAction")), Session("nPerson_typ"), .Form.Item("tcdInpDate"), .Form.Item("tctClieName"), .Form.Item("valOcupat"), .Form.Item("tcdBirthDate"), .Form.Item("tctLegalName"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEmpl_qua"), eFunctions.Values.eTypeData.etdDouble))

                    End If
                End With

            Case "BC002"
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjClientSeq.insvalBC002("BC002", .Form.Item("tctClient"), .Form.Item("cbeRelationship"), Session("sClient"))
                    End If
                End With

            Case "BC007S"
                With Request
                    insvalSequence = mobjClientSeq.insValBC007S(mobjValues.StringToType(.Form.Item("tcnWeight"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnHeight"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

            Case "BC007M"
                insvalSequence = mobjClientSeq.insValBC007M(mobjValues.StringToType(Request.Form.Item("tcnChild"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCars"), eFunctions.Values.eTypeData.etdDouble, True), Session("sclient"))

            Case "BC007P"
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    insvalSequence = mobjClientSeq.insValBC007P(Session("sclient"), mobjValues.StringToType(Request.Form.Item("cbeTypeOfPoliticalOffice"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcdGrantDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("cbePlaceOfBirth"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cbeSecondNationality"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cbeResidentFormer"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("txtAddress"), Request.Form.Item("txtSSN"), Request.Form.Item("txtUsLegalPerson"), Request.Form.Item("txtUsitinnum"), Request.Form.Item("txtUsphone"), Request.Form.Item("chkUsirsind"), Request.Form.Item("txtUsAccount"))
                Else
                    Dim lclsFiscal_Residence As eClient.Fiscal_Residence
                    lclsFiscal_Residence = New eClient.Fiscal_Residence
                    insvalSequence = lclsFiscal_Residence.insValBC007P(Session("sclient"), mobjValues.StringToType(Request.Form.Item("cbeCountry"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("tctsus_Itinnum"), mobjValues.StringToType(Request.Form.Item("cbeNmotive_Itin"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("tctsJurisdiction"))

                End If
                '+ Cuentas bancarias del cliente	
            Case "BC013"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lstrStatRegt = eRemoteDB.Constants.strNull

                        If .Form.Item("cbeStatRegt") <> "0" Then
                            lstrStatRegt = .Form.Item("cbeStatRegt")
                        End If

                        If IsNothing(.Form.Item("chkDeposit")) Then
                            lstrSdeposit = "2"
                        Else
                            lstrSdeposit = "1"
                        End If

                        insvalSequence = lobjBk_Account.insValBC013Upd("BC013", .QueryString.Item("Action"), Session("sClient"), mobjValues.StringToType(.Form.Item("cbeBankext"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctAccount"), lstrStatRegt, mobjValues.StringToType(.Form.Item("tcnTyp_acc"), eFunctions.Values.eTypeData.etdDouble, True), lstrSdeposit)
                    End If
                End With

            Case "BC014"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjClientSeq.insValBC014("BC014", Session("sClient"), mobjValues.StringToType(.Form.Item("tcdFinanDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnits"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeFinanStat"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"))
                    End If
                End With

                '+ Vía de cobro de un cliente		
            Case "BC015"
                With Request
                    If Not CBool(.Form.Item("bDisabledForm")) Then
                        insvalSequence = lobjDir_debit_cli.insValBC015("BC015", Session("sClient"), .Form.Item("optType_Dir"), mobjValues.StringToType(.Form.Item("valBank"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valAccount"), .Form.Item("tctBankAuth"), .Form.Item("chkDelDir_debit"), mobjValues.StringToType(.Form.Item("tcnBill_Day"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End With

            Case "BC016"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = lobjCred_card.insValBC016("BC016", .QueryString.Item("Action"), Session("sClient"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCardType"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctCredi_card"), mobjValues.StringToType(.Form.Item("tcdCardexpir"), eFunctions.Values.eTypeData.etdDate, True), .Form.Item("cbeStatus"))
                    End If
                End With

            Case "SCA101"
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    lobjAddress = New eGeneralForm.GeneralForm

                    insvalSequence = insvalSequence & lobjAddress.insValSCA001(Request.QueryString.Item("sCodispl"), "2", Request.Form.Item("txtAddress"), Request.Form.Item("valZipCode"), Request.Form.Item("valLocal"), Request.Form.Item("cbeCountry"), Request.Form.Item("tcnLonCardinG"), Request.Form.Item("tcnLonCardinM"), Request.Form.Item("tcnLonCardinS"), Request.Form.Item("tcnLatCardinG"), Request.Form.Item("tcnLatCardinM"), Request.Form.Item("tcnlatCardinS"))

                    lobjAddress = Nothing
                Else
                    lobjPhones = New eGeneralForm.GeneralForm

                    insvalSequence = lobjPhones.insValPhones("SCA101", Request.QueryString.Item("nRecowner"), Request.QueryString.Item("sKeyAddress"), Request.QueryString.Item("nOrder"), Request.Form.Item("tcnArea"), CStr(Today), Request.Form.Item("tctPhone"), Request.Form.Item("tcnOrder"), Request.Form.Item("tcnExtensi1"), Request.Form.Item("cbePhoneType"), Request.Form.Item("tcnExtensi2"), Request.QueryString.Item("Action"))
                    lobjPhones = Nothing
                End If

            Case "SCA10-2"
                If Request.QueryString.Item("WindowType") = "PopUp" Then

                    lclsImages = New eGeneralForm.GeneralForm

                    With Request
                        insvalSequence = lclsImages.insValSCA002("SCA10-2", "Image", .Form.Item("sDescript"), .Form.Item("dCompdate"), .Form.Item("dNulldate"), , .Form.Item("sSource"))
                    End With
                    lclsImages = Nothing
                End If

            Case "SCA2-9"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    lclsNotes = New eGeneralForm.GeneralForm
                    With Request
                        insvalSequence = lclsNotes.insValSCA002("SCA002", "Note", .Form.Item("sDescript"), .Form.Item("dCompdate"), .Form.Item("dNulldate"), .Form.Item("tDs_text"))
                    End With
                    lclsNotes = Nothing
                End If

            Case "GE101"
                insvalSequence = ""

            Case "BC801"
                With Request
                    insvalSequence = mobjClientSeq.insValBC801(Session("sClient"), mobjValues.StringToType(.Form.Item("cbeDisability"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeIncapacity"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdIncapacity"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("valIncap_cod"), eFunctions.Values.eTypeData.etdDouble, True))

                End With

            Case "BC008"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then

                        insvalSequence = mobjClientSeq.InsValBC008("BC008", .QueryString.Item("Action"), Session("sClient"), mobjValues.StringToType(.Form.Item("cbeIdDoc_Type"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctIdDoc"), "")
                    End If
                End With

            Case "BC9000"
                With Request
                    insvalSequence = mobjClient_evalrisk.insValBC9000("BC9000", .QueryString.Item("Action"), Session("sClient"), mobjValues.StringToType(.Form.Item("cbeCodRating"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLimitCredit"), eFunctions.Values.eTypeData.etdDouble))
                End With

            Case "BC9001"
                With Request
                    If .QueryString.Item("WindowType") <> "PopUp" Then
                        insvalSequence = mobjUserWeb.valBC9001(Session("sClient"), .Form.Item("tctInitials"), .Form.Item("tctPassword"), mobjValues.StringToType(.Form.Item("cbeRol"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeStatus"), eFunctions.Values.eTypeData.etdDouble), "BC9001")
                    Else
                        insvalSequence = mobjUserWeb.valBC9001_upd(Session("sClient"), mobjValues.StringToType(.Form.Item("cbeBRanch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), "BC9001")
                    End If
                End With

                '+BC6000: Documentos que identifican al cliente			
            Case "BC6000"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjCliDocuments = New eClient.CliDocuments

                        insvalSequence = mobjCliDocuments.InsValBC6000Upd(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), Session("sClient"), mobjValues.StringToType(.Form.Item("cbeTypClientDoc"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctCliNumDocu"), mobjValues.StringToType(.Form.Item("tcdIssueDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDat"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        mobjCliDocuments = New eClient.CliDocuments

                        insvalSequence = mobjCliDocuments.InsValBC6000(.QueryString("sCodispl"), Session("sClient"), Session("nPerson_typ"))
                    End If
                End With

            Case Else
                insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    ''' <summary>
    ''' Se realizan las actualizaciones de las ventanas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function insPostSequence() As Boolean
        Dim clngDelete As Integer
        Dim lstrError As String = String.Empty
        Dim lobjClient As eClient.ClientWin
        Dim lblnPost As Boolean
        Dim lstrCon_win As String
        Dim lintAction As Integer

        '- Variable del objeto de funciones de las paginas      
        Dim lobjValues As eFunctions.Values
        Dim lclsPostNotes As eGeneralForm.GeneralForm
        Dim lclsClient As eClient.Client
        Dim lobjPhone As eGeneralForm.Phone
        Dim lclsClientWin As eClient.ClientWin
        Dim lclsErrors As eGeneralForm.GeneralForm

        Dim lintTypeCompany As Object

        lobjValues = New eFunctions.Values
        lstrCon_win = "2"
        lblnPost = True

        Select Case Request.QueryString.Item("sCodispl")
            Case "BC003_K"
                If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
                    lblnPost = mobjClientSeq.insPostBC003_k(Session("sClient"), CInt(Request.QueryString.Item("nMainAction")), Session("nUserCode"), mobjValues.StringToType(Request.Form.Item("cbePerson_typ"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctClient_digit"))
                End If

            Case "BC007S"
                With Request
                    lblnPost = mobjClientSeq.InsPostBC007S(Session("sClient"), mobjValues.StringToType(.Form.Item("tcnWeight"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnHeight"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sSel"), .Form.Item("sSel_H"), Session("nUsercode"))

                End With

            Case "BC007M"
                lblnPost = mobjClientSeq.insPostBC007M(Session("sClient"), mobjValues.StringToType(Request.Form.Item("cbeLevel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeHouseType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnChild"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCars"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeClass"), eFunctions.Values.eTypeData.etdDouble, True))

            Case "BC007P"
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    lblnPost = mobjClient_SF.InsPostBC007P(CInt(Request.QueryString.Item("nAction")), Session("sClient"), Today, mobjValues.StringToType(Request.Form.Item("cbeTypeOfPoliticalOffice"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("tcdGrantDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate, True), Session("Digit"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbePlaceOfBirth"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cbeSecondNationality"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(Request.Form.Item("cbeResidentFormer"), eFunctions.Values.eTypeData.etdInteger, True), Request.Form.Item("txtAddress"), Request.Form.Item("txtSSN"), Request.Form.Item("txtUsLegalPerson"), Request.Form.Item("txtUsitinnum"), Request.Form.Item("txtUsphone"), Request.Form.Item("chkUsirsind"), Request.Form.Item("txtUsAccount"), Request.Form.Item("txtPlaceOfBirth"))
                Else
                    Dim lclsFiscal_Residence As eClient.Fiscal_Residence
                    lclsFiscal_Residence = New eClient.Fiscal_Residence
                    Dim Ddate_aux As Date
                    Ddate_aux = Date.Now
                    lblnPost = lclsFiscal_Residence.InsPostBC007P(CInt(Request.QueryString.Item("nAction")), Session("sclient"), mobjValues.StringToType(Request.Form.Item("cbeCountry"), eFunctions.Values.eTypeData.etdInteger), Ddate_aux, Request.Form.Item("tctsus_Itinnum"), mobjValues.StringToType(Request.Form.Item("cbeNmotive_Itin"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("tctsJurisdiction"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))


                End If

            Case "BC001N"
                With Request
                    Response.Write("<SCRIPT> top.fraHeader.UpdateDiv('tctCliename', '" & .Form.Item("tctLastName") & " " & .Form.Item("tctLastName2") & " , " & .Form.Item("tctFirstName") & "','');</" & "Script>")
                    lblnPost = mobjClientSeq.insPostBC001N(Session("sClient"), mobjValues.StringToType(.Form.Item("tcdInpDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctCuitP"), .Form.Item("tctLastName"), .Form.Item("tctFirstName"), mobjValues.StringToType(.Form.Item("cbeCivilsta"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeSex"), mobjValues.StringToType(.Form.Item("cbeTitle"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeNationality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOccupat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdBirthDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDriverDat"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctDriverNum"), mobjValues.StringToType(.Form.Item("tcdDeathdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, .Form.Item("chkBlockade"), .Form.Item("tctLastName2"), mobjValues.StringToType(.Form.Item("cbeArea"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDrivExpDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTypDriver"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeLimitDriv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeHealth_Org"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeAfp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdWedd"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkBill_Ind"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcdRetirement"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdIndependant"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDependant"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optSmoking"), .Form.Item("tctFatca"), .Form.Item("chkPEP"), .Form.Item("chkUSPERSON"), .Form.Item("chkCRS"))
                    Dim mobjClient As eClient.Client
                    mobjClient = New eClient.Client
                    mobjClient.insPreBC001(Session("sClient"))
                    Session("chkPEP") = mobjClient.sPEP
                    Session("chkCRS") = mobjClient.sCRS
                    Session("chkUSPERSON") = mobjClient.sUsPerson
                    Session("dInpdate") = .Form.Item("tcdInpDate")
                End With

            Case "BC001J"
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjClientSeq.insPostBC001J(.QueryString.Item("Action"), Session("sClient"), .Form.Item("tctClientr"), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcnPosition"), .Form.Item("tctNewClientr"), Session("nUsercode"))
                        lblnPost = True
                    Else
                        Response.Write("<SCRIPT>if (typeof(top.fraHeader)!='undefined'){")
                        Response.Write("top.fraHeader.UpdateDiv('tctCliename', '" & .Form.Item("tctClieName") & "','');}")
                        Response.Write(" else{opener.top.fraHeader.UpdateDiv('tctCliename', '" & .Form.Item("tctClieName") & "','');}")
                        Response.Write("</" & "Script>")
                        lblnPost = mobjClientSeq.insPostBC001J_K(Session("sClient"), mobjValues.StringToType(.Form.Item("tcdInpDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctClieName"), .Form.Item("tctLegalName"), mobjValues.StringToType(.Form.Item("valOcupat"), eFunctions.Values.eTypeData.etdDouble), vbNullString, mobjValues.StringToType(.Form.Item("tcdBirthDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnEmpl_qua"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeInvoicing"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkBill_ind"), .Form.Item("chkBlockadeJ"), mobjValues.StringToType(.Form.Item("cbeComp_Type"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkPEP"), .Form.Item("chkUSPERSON"))


                    End If
                    Session("dInpdate") = .Form.Item("tcdInpDate")
                End With
                If lblnPost = False Then
                    lstrError = mobjClientSeq.ErrorDescript
                End If

            Case "BC002"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjClientSeq.insPostBC002(.QueryString.Item("Action"), Session("sClient"), .Form.Item("tctClient"), "", .Form.Item("cbeRelationship"), Session("nUserCode"), .Form.Item("nOriginalRelaship"))
                    End With
                End If

            Case "BC013"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        Select Case .QueryString.Item("Action")
                            Case "Add"
                                lintAction = eFunctions.Menues.TypeActions.clngActionadd
                            Case "Update"
                                lintAction = eFunctions.Menues.TypeActions.clngActionUpdate
                            Case "Del"
                                lintAction = clngDelete
                        End Select
                        lblnPost = lobjBk_Account.InsPostBC013Upd(lintAction, Session("sClient"), mobjValues.StringToType(.Form.Item("cbeBankext"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctAccount"), .Form.Item("cbeStatRegt"), mobjValues.StringToType(.Form.Item("tcnTyp_acc"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"), .Form.Item("chkDeposit"))
                    End If
                End With

            Case "BC014"
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjClientSeq.insPostBC014(.QueryString.Item("Action"), Session("sClient"), mobjValues.StringToType(.Form.Item("tcdFinanDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnUnits"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeFinanStat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End With

            Case "BC015"
                With Request
                    If Not CBool(.Form.Item("bDisabledForm")) Then
                        lblnPost = lobjDir_debit_cli.insPostBC015(CInt(.QueryString.Item("nMainAction")), Session("sClient"), mobjValues.StringToType(.Form.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optType_Dir"), mobjValues.StringToType(.Form.Item("valBank"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valAccount"), .Form.Item("tctBankAuth"), .Form.Item("chkDelDir_debit"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnBill_Day"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        lblnPost = True
                    End If
                End With

            Case "BC016"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        Select Case .QueryString.Item("Action")
                            Case "Add"
                                lintAction = eFunctions.Menues.TypeActions.clngActionadd
                            Case "Update"
                                lintAction = eFunctions.Menues.TypeActions.clngActionUpdate
                            Case "Del"
                                lintAction = clngDelete
                        End Select
                        lblnPost = lobjCred_card.InsPostBC016(lintAction, Session("sClient"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCardType"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctCredi_card"), mobjValues.StringToType(.Form.Item("tcdCardexpir"), eFunctions.Values.eTypeData.etdDate, True), .Form.Item("cbeStatus"), Session("nUsercode"))
                    End If
                End With

            Case "SCA2-9"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    lclsPostNotes = New eGeneralForm.GeneralForm
                    With Request
                        lblnPost = lclsPostNotes.insPostNotes(.QueryString.Item("Action"), Session("sClient"), .Form.Item("nNotenum"), .Form.Item("nConsec"), .Form.Item("sDescript"), CDate(.Form.Item("dCompdate")), CDate(.Form.Item("dNulldate")), .Form.Item("tDs_text"), .Form.Item("nUsercode"), .Form.Item("nRectype"))
                        If .QueryString.Item("Action") = "Add" Then
                            lclsClient = New eClient.Client
                            With lclsClient
                                If .Find(Session("sClient")) Then
                                    Session("nNotenum") = .nNotenum
                                End If
                            End With
                        End If
                    End With
                    lclsPostNotes = Nothing
                End If

            Case "SCA101"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    lobjPhone = New eGeneralForm.Phone
                    insPostSequence = False

                    Select Case Request.QueryString.Item("Action")
                        Case "Add"
                            With lobjPhone
                                .nRecowner = CInt(Request.QueryString.Item("nRecowner"))
                                .sKeyAddress = Request.QueryString.Item("sKeyAddress")
                                .nKeyPhones = CInt(Request.Form.Item("tcnOrder"))
                                .nArea_code = CInt(Request.Form.Item("tcnArea"))
                                .dEffecdate = Session("SCA101_dEffecDate")
                                .sPhone = Request.Form.Item("tctPhone")
                                .nOrder = CInt(Request.Form.Item("tcnOrder"))
                                If Trim(Request.Form.Item("tcnExtensi1")) <> vbNullString Then
                                    .nExtens1 = CInt(Request.Form.Item("tcnExtensi1"))
                                End If
                                .nPhone_type = CInt(Request.Form.Item("cbePhoneType"))
                                If Trim(Request.Form.Item("tcnExtensi2")) <> vbNullString Then
                                    .nExtens2 = CInt(Request.Form.Item("tcnExtensi2"))
                                End If
                                .nUsercode = Session("nUserCode")
                                lblnPost = .Add
                            End With

                        Case "Update"
                            With lobjPhone
                                .Find(Request.QueryString.Item("sKeyAddress"), CInt(Request.Form.Item("tcnOrder")), CShort(Request.QueryString.Item("nRecowner")), Session("SCA101_dEffecDate"))
                                .nArea_code = CInt(Request.Form.Item("tcnArea"))
                                .dEffecdate = Session("SCA101_dEffecDate")
                                .sPhone = Request.Form.Item("tctPhone")
                                .nOrder = CInt(Request.Form.Item("tcnOrder"))
                                If Trim(Request.Form.Item("tcnExtensi1")) <> vbNullString Then
                                    .nExtens1 = CInt(Request.Form.Item("tcnExtensi1"))
                                End If
                                .nPhone_type = CInt(Request.Form.Item("cbePhoneType"))
                                If Trim(Request.Form.Item("tcnExtensi2")) <> vbNullString Then
                                    .nExtens2 = CInt(Request.Form.Item("tcnExtensi2"))
                                End If
                                .nUsercode = Session("nUserCode")
                                lblnPost = .Update
                            End With
                    End Select
                    lobjPhone = Nothing
                Else
                    lobjAddress = New eGeneralForm.Address
                    '+ Request.QueryString("WindowType") <> "PopUp"  
                    With lobjAddress
                        .dEffecdate = Today
                        'Este código provoca un error en el manejo historico de la tabla Address
                        If CStr(Session("dInpdate")) <> vbNullString And Request.QueryString.Item("sCodispl") = "SCA101" Then
                            .dEffecdate = Session("dInpdate")
                        End If

                        .nRecowner = Request.Form.Item("tcnRecOwner")
                        .sKeyAddress = Request.Form.Item("tctKeyAddress")
                        .sRecType = Request.Form.Item("tctRecType")
                        .sStreet = Request.Form.Item("txtAddress")
                        .sClient = Session("sClient")
                        .sE_mail = Request.Form.Item("tctE_mail")
                        .nLat_grade = lobjValues.StringToType(Request.Form.Item("tcnLatCardinG"), eFunctions.Values.eTypeData.etdDouble)
                        .nLon_grade = lobjValues.StringToType(Request.Form.Item("tcnLonCardinG"), eFunctions.Values.eTypeData.etdDouble)
                        .nLat_minute = lobjValues.StringToType(Request.Form.Item("tcnLatCardinM"), eFunctions.Values.eTypeData.etdDouble)
                        .nLon_minute = lobjValues.StringToType(Request.Form.Item("tcnLonCardinM"), eFunctions.Values.eTypeData.etdDouble)
                        .nLat_second = lobjValues.StringToType(Request.Form.Item("tcnLatCardinS"), eFunctions.Values.eTypeData.etdDouble)
                        .nLon_second = lobjValues.StringToType(Request.Form.Item("tcnLonCardinS"), eFunctions.Values.eTypeData.etdDouble)

                        .nCountry = Request.Form.Item("cbeCountry")
                        .nLocal = Request.Form.Item("ValLocal")

                        .nZip_code = lobjValues.StringToType(Request.Form.Item("tcnZipCode"), eFunctions.Values.eTypeData.etdDouble)
                        .nProvince = Request.Form.Item("cbeProvince")
                        .nUsercode = Session("nUsercode")
                        .nMunicipality = Request.Form.Item("valMunicipality")

                        If Request.Form.Item("chkInfor") = "1" Then
                            .sInfor = "1"
                        Else
                            .sInfor = "2"
                        End If
                        .sBuild = Request.Form.Item("tctBuild")
                        .nFloor = lobjValues.StringToType(Request.Form.Item("tcnFloor"), eFunctions.Values.eTypeData.etdDouble)
                        .sDepartment = Request.Form.Item("tctDepartment")
                        .sPopulation = Request.Form.Item("tctPopulation")
                        .sPobox = Request.Form.Item("tctPobox")
                        .sDescadd = Request.Form.Item("tctDescadd")
                        .sCostCenter = Request.Form.Item("tctCost")

                        If Request.QueryString.Item("sCodispl") = "SCA101" And Request.Form.Item("chkdeldir") = "1" Then
                            .Delete()
                        Else
                            .Update()
                        End If
                        lobjAddress.UpdatePhones(Request.Form.Item("tctKeyAddress"), Request.Form.Item("tcnRecOwner"), lobjAddress.dEffecdate, lobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End With

                    '+ Se actualiza ventana clientes, Client_Win
                    lobjClient = New eClient.ClientWin
                    lobjClient.insUpdClient_win(Session("sClient"), CStr(Request.QueryString.Item("sCodispl")), "2")

                    lobjClient = Nothing

                End If

                '+ Ventana de Fin de proceso		
            Case "GE101"
                If Request.Form.Item("optElim") = "Delete" Then
                    '+ Se elimina la información relacionada al cliente
                    lblnPost = mobjClientSeq.DeleteSequence(Session("sClient"), Session("nUsercode"))
                Else
                    '+ Se verifica que no existan páginas marcadas como requeridas

                    With Server
                        lclsClientWin = New eClient.ClientWin
                        lclsErrors = New eGeneralForm.GeneralForm
                    End With

                    If lclsClientWin.IsPageRequired(Session("sClient"), eFunctions.Menues.TypeActions.clngActionadd) Then
                        Response.Write(lclsErrors.insValGE101("ClientSeq"))
                        lblnPost = False
                    End If
                    lclsClientWin = Nothing
                    lclsErrors = Nothing
                End If

                '+ Se blanquean las variables de session utilizadas en el llamado de la Base de datos de Cliente desde otra ventana.
                If CStr(Session("sOrigonalForm")) <> vbNullString Or CStr(Session("sLinkSpecial")) <> vbNullString Then
                    Session("sOrigonalForm") = vbNullString
                    Session("sLinkSpecial") = vbNullString
                End If
                Response.Write("<SCRIPT>if(opener.top.location.href.indexOf(""LinkSpecial=1"")!=-1){opener.top.close()}else{opener.top.location.reload();}</" & "Script>")
                Response.Write("<SCRIPT>window.close()</" & "Script>")
                lblnPost = False
                '+ Invalidez   			
            Case "BC801"
                With Request
                    lblnPost = mobjClientSeq.insPostBC801(Session("sClient"), mobjValues.StringToType(.Form.Item("cbeDisability"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeIncapacity"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdIncapacity"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("valIncap_cod"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"))
                End With

            Case "BC008"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With Request
                        lblnPost = mobjClientSeq.InsPostBC008(.QueryString.Item("Action"), Session("sClient"), mobjValues.StringToType(.Form.Item("cbeIdDoc_Type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tctIdDoc"), eFunctions.Values.eTypeData.etdDouble, True), "", Session("nUsercode"))
                    End With
                End If
            Case "BC9000"
                With Request
                    lblnPost = mobjClient_evalrisk.InsPostBC9000(CInt(.QueryString.Item("nAction")), Session("sClient"), Today, mobjValues.StringToType(.Form.Item("tcdOtherDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeSinceYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnNumEmployers"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCntryRisk"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTypeCia"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTypeProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRisk"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeActBus"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRefBank"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRefBus"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRefLaw"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeNumPays"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOldInsurance"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeProPay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCodDicom"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctDesDicom"), mobjValues.StringToType(.Form.Item("cbeCreditReason"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeLiqCurren"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeLiqAcd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRentability"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeGrowSales"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeEconomic"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeFinancial"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCodRating"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDesRating"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCountry"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctNote1"), .Form.Item("tctNote2"), .Form.Item("tctNote3"), .Form.Item("tctNote4"), mobjValues.StringToType(.Form.Item("cbeBranchCia"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnLimitCredit"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
                End With
            Case "BC9001"
                With Request
                    If .QueryString.Item("WindowType") <> "PopUp" Then
                        lblnPost = mobjUserWeb.PostBC9001(Session("sClient"), .Form.Item("tctInitials"), .Form.Item("tctPassword"), mobjValues.StringToType(.Form.Item("cbeRol"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeStatus"), eFunctions.Values.eTypeData.etdDouble), "BC9001", Session("nUsercode"))
                    Else
                        lblnPost = mobjUserWeb.PostBC9001_upd(CShort(.QueryString.Item("nAction")), Session("sClient"), mobjValues.StringToType(.Form.Item("cbeBRanch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))

                        mstrQueryString = "&sInitials=" & Request.QueryString.Item("sInitials") & "&sPassword=" & Request.QueryString.Item("sPassword") & "&sStatus=" & Request.QueryString.Item("sStatus") & "&nRol=" & Request.QueryString.Item("nRol")
                    End If
                End With
                '+BC6000: Documentos que identifican al cliente
            Case "BC6000"
                With Request
                    If .QueryString.Item("nFastRecord") = "1" And (.QueryString.Item("nTypeCompany") = "0" Or .QueryString.Item("nTypeCompany") = vbNullString) Then
                        lintTypeCompany = Session("nTypeCompany")
                    Else
                        lintTypeCompany = .QueryString.Item("nTypeCompany")
                    End If
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjCliDocuments = New eClient.CliDocuments
                        lblnPost = mobjCliDocuments.InsPostBC6000(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sClient"), mobjValues.StringToType(.Form.Item("cbeTypClientDoc"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("tctCliNumDocu"), mobjValues.StringToType(.Form.Item("tcdIssueDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(lintTypeCompany, eFunctions.Values.eTypeData.etdInteger))
                    End If
                End With

        End Select
        insPostSequence = lblnPost
    End Function

    ''' <summary>
    ''' Se activa cuando la acción es Finalizar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function insFinish() As Boolean
        '+ Se verifica que no existan páginas marcadas como requeridas
        Dim lclsClientWin As eClient.ClientWin
        Dim lclsErrors As eGeneralForm.GeneralForm
        Dim mstrErrors As String

        insFinish = True

        With Server
            lclsClientWin = New eClient.ClientWin
            lclsErrors = New eGeneralForm.GeneralForm
        End With

        If lclsClientWin.IsPageRequired(Session("sClient"), CInt(Request.QueryString.Item("nMainAction"))) Then

            mstrErrors = lclsErrors.insValGE101("ClientSeq")

            If (mstrErrors > vbNullString) Then

                Session("sErrorTable") = mstrErrors
                Session("sForm") = Request.Form.ToString
                With Response
                    .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                    .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""ClientSeqError"",660,330);")
                    .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                    .Write("</" & "Script>")
                End With

            End If
            insFinish = False
        End If

        lclsClientWin = Nothing
        lclsErrors = Nothing
    End Function

    ''' <summary>
    ''' Esta función se encarga de conseguir un código de cliente para los clientes nuevos (Provisionales).
    ''' </summary>
    ''' <param name="lstrClient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function insGetNewClient(ByRef lstrClient As String) As String
        Dim lclsClient As eClient.Client

        '+Si la acción es registrar, se busca automáticamente el código de cliente
        If CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 301 Then
            If lstrClient <> vbNullString And Len(Trim(lstrClient)) = 1 Then
                lclsClient = New eClient.Client
                If lclsClient.ValidClientType(Trim(lstrClient)) Then
                    lstrClient = lclsClient.GetNewClientCode()
                End If
                lclsClient = Nothing
            End If
        End If
        insGetNewClient = lstrClient
    End Function

</script>
<%Response.Expires = -1

    With Server
        mobjValues = New eFunctions.Values
        mobjClientSeq = New eClient.ClientSeq
        mobjClient_evalrisk = New eClient.Client_evalrisk
        mobjClient_SF = New eClient.Client_SF
        mobjUserWeb = New eClient.UsersWeb
        lobjDir_debit_cli = New eClient.Dir_debit_cli
        lobjCred_card = New eClient.cred_card
        lobjBk_Account = New eClient.bk_account
    End With

    mstrCommand = "&sModule=Client&sProject=ClientSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 3/02/06 19:02 $"
</SCRIPT>
</HEAD>
<BODY>
<SCRIPT>
//%CancelErrors: Acciones al efectual la cancelación de algún error.
//-----------------------------------------------------------------------------------------
function CancelErrors(){
//-----------------------------------------------------------------------------------------
	self.history.go(-1)
}

//%NewLocation: se recalcula el URL de la página
//-----------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------------------
    var lstrLocation = "";

    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}

//%UpdateOpenerControl: Actualiza el control cliente de la ventana que invoco a la secuencia
//-----------------------------------------------------------------------------------------
function UpdateOpenerControl(sClientControl){
//------------------------------------------------------------------------------------------
var lstrError;
var lstrDigitName
	lstrDigitName = sClientControl + '_Digit';
    try{		
        top.opener.document.forms[0].elements[sClientControl].focus();
        top.opener.document.forms[0].elements[sClientControl].value='<%=Session("sClient")%>';        
        top.opener.$('#' + sClientControl).change();
		if (typeof(top.opener.document.forms[0].elements[lstrDigitName])!='undefined')
			{
			top.opener.document.forms[0].elements[lstrDigitName].focus();
            top.opener.document.forms[0].elements[lstrDigitName].value='<%=Session("Digit")%>';  
            top.opener.document.forms[0].elements[sClientControl].focus();
			}
       }
    catch(lstrError){}    
}
</SCRIPT>
<%

    If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
        '+ Si no se han validado los campos de la página

        If Request.Form.Item("sCodisplReload") = vbNullString Then
            mstrErrors = insvalSequence()
            Session("sErrorTable") = mstrErrors
            Session("sForm") = Request.Form.ToString
            mblnReload = False
        Else
            Session("sErrorTable") = vbNullString
            Session("sForm") = vbNullString
            mblnReload = True
        End If

        If mstrErrors > vbNullString Then
            With Response
                .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""ClientSeqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                .Write("</SCRIPT>")
            End With
        Else
            If insPostSequence() Then

                If Request.QueryString.Item("nFastRecord") = "1" Then
                    lstrReload = Request.QueryString.Item("nFastRecord")
                Else
                    lstrReload = "2"
                End If

                'If 1 = 2 Then
                If Request.QueryString.Item("WindowType") <> "PopUp" Then

                    '+ Si el campo oculto "tctOriginalForm" es distinto a blanco, se pasa su valor como parámetro a
                    '+ la ventana Sequence.aspx - ACM - 31/07/2001
                    If Request.Form.Item("tctOriginalForm") <> vbNullString Then

                        '+ Se mueve automaticamente a la siguiente página
                        Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sOriginalForm=" & Request.Form.Item("tctOriginalForm") & "';</SCRIPT>")
                    Else
                        '+ Se mueve automaticamente a la siguiente página

                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
                        End If
                    End If

                    If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                        Response.Write("<SCRIPT>self.history.go(-1) </SCRIPT>")
                    End If
                Else

                    If Request.QueryString.Item("sCodispl") = "BC6000" Then
                        If mblnReload Then
                            Response.Write("<SCRIPT>top.opener.top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nFastRecord=" & lstrReload & "&sGoToNext=NO" & "';</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nFastRecord=" & lstrReload & "&sGoToNext=NO" & "';</SCRIPT>")
                        End If
                    Else
                        Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
                    End If

                    '+ Se recarga la página que invocó la PopUp
                    Select Case Request.QueryString.Item("sCodispl")
                        Case "BC001J"
                            Response.Write("<SCRIPT>top.opener.document.location.href='BC001J.aspx?sCodispl=BC001J&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&tctCuitP=" & Request.Form.Item("tctCuitP") & "&tcdInpDate=" & Request.Form.Item("tcdInpDate") & "&tctClieName=" & Request.Form.Item("tctClieName") & "&valOcupat=" & Request.Form.Item("valOcupat") & "&tcdBirthDate=" & Request.Form.Item("tcdBirthDate") & "&chkBlockadeJ=" & Request.Form.Item("chkBlockadeJ") & "&tctLegalName=" & Request.Form.Item("tctLegalName") & "&tcnEmpl_qua=" & mobjValues.StringToType(Request.Form.Item("tcnEmpl_qua"), eFunctions.Values.eTypeData.etdDouble) & "&cbeInvoicing=" & mobjValues.StringToType(Request.Form.Item("cbeInvoicing"), eFunctions.Values.eTypeData.etdDouble) & "&chkBill_ind=" & Request.Form.Item("chkBill_ind") & "&cbeComp_Type=" & Request.Form.Item("cbeComp_Type") & "&chkUSPERSON=" & Request.Form.Item("chkUSPERSON") & "&chkPEP=" & Request.Form.Item("chkPEP") & "'</SCRIPT>")
                        Case "BC6000"
                            If mblnReload Then
                                If lstrReload = "2" Then
                                    Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='BC6000.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nFastRecord=" & lstrReload & "' </SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>window.close();top.opener.top.opener.document.location.href='BC6000.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nFastRecord=" & lstrReload & "' </SCRIPT>")
                                End If
                            Else
                                Response.Write("<SCRIPT>top.opener.document.location.href='BC6000.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nFastRecord=" & lstrReload & "' </SCRIPT>")
                            End If
                        Case "BC007P"
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "&sPlace_birth=" & Request.QueryString.Item("sPlace_birth") & "&nPlace_birth=" & Request.QueryString.Item("nPlace_birth") & "&nPosition=" & Request.QueryString.Item("nPosition") & "&dStartcondition=" & Request.QueryString.Item("dStartcondition") & "&dEndcondition=" & Request.QueryString.Item("dEndcondition") & "&nResident_former=" & Request.QueryString.Item("nResident_former") & "&nSecond_nationality=" & Request.QueryString.Item("nSecond_nationality") & "&sUsAdress=" & Request.QueryString.Item("sUsAdress") & "&sSSN=" & Request.QueryString.Item("sSSN") & "&sUsLegal_person=" & Request.QueryString.Item("sUsLegal_person") & "&sUsphone=" & Request.QueryString.Item("sUsphone") & "&sUsAccount=" & Request.QueryString.Item("sUsAccount") & "&sUsIrsind=" & Request.QueryString.Item("sUsIrsind") & "'</SCRIPT>")
                                'Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "&sPlace_birth=" & Request.QueryString.Item("sPlace_birth") & "&nPlace_birth=" & Request.QueryString.Item("nPlace_birth") & "&nPosition=" & Request.QueryString.Item("nPosition") & "'</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames('fraFolder').document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
                            End If
                        Case Else
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
                            End If
                    End Select
                End If
            End If
        End If
    Else
        If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
            If CStr(Session("sOriginalForm")) = "CA025" Then
                Response.Write("<SCRIPT>")
                Response.Write(" UpdateOpenerControl('" & Session("sLinkControl") & "');")
                Response.Write(" top.close();")
                Response.Write("</SCRIPT>")
            Else
                Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
            End If
        Else
            If Session("bQuery") = True Then
                Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
            Else
                If insFinish() Then
                    If CStr(Session("sOriginalForm")) <> vbNullString Or CStr(Session("sLinkSpecial")) <> vbNullString Then
                        Response.Write("<SCRIPT>")
                        Response.Write(" UpdateOpenerControl('" & Session("sLinkControl") & "');")
                        Response.Write(" top.close();")
                        Response.Write("</SCRIPT>")
                    Else
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>window.close();opener.top.location.reload();</SCRIPT>")
                        End If
                    End If
                End If
            End If
        End If
    End If

    mobjClientSeq = Nothing
    mobjClient_evalrisk = Nothing
    mobjClient_SF = Nothing
    mobjUserWeb = Nothing
    mobjValues = Nothing
    lobjDir_debit_cli = Nothing
    lobjCred_card = Nothing
    lobjBk_Account = Nothing
    mobjCliDocuments = Nothing
%>
</BODY>
</HTML>





