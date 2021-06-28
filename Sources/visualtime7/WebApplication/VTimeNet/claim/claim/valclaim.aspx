<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eSchedule" %>
<script language="VB" runat="Server">

    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    Dim mstrCodispl As String
    Dim mobjValues As eFunctions.Values
    Dim mstrErrors As String
    Dim mobjClaim As Object
    Dim mintCase_num As String
    Dim mintDeman_type As String
    Dim mobjGeneralFunction As eGeneral.GeneralFunction
    Dim mstrKey As String
    Dim lstrKey683 As Object

    '- Variable auxiliar para el pase de valores del encabezado al folder.
    Dim mstrQueryString As String

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String
    Dim  mstServ_Order_new As integer


    '% insvalClaim: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insvalClaim() As String
        Dim lstrFirstCase As String
        Dim lintCounter As Integer
        Dim insValClaim_AUX As String
        Dim lblnSelected As Boolean
        Dim lstrCase() As String
        Dim lblnChecked As Boolean
        Dim lintnSelCount As String
        Dim lintAction As String
        '--------------------------------------------------------------------------------------------
        Dim lintCount As Integer
	Dim lstrTotalLoss As Object

        '^Begin Header Block VisualTimer Utility
        Call mobjNetFrameWork.BeginProcess("insvalClaim")
        '~End Header Block VisualTimer Utility

        Dim mobjProf_ord As eClaim.Prof_ord
        Dim lclsClaim_Master As eClaim.Claim_Master
        Dim lclsQuot_parts As eClaim.Quot_parts
        Dim lclsClaim_his As eClaim.Claim_his
        Dim lclsBuy_Ord As eClaim.Buy_ord
        Dim lclsQuot_auto As eClaim.Quot_auto
        Dim lclsBuy_Auto As eClaim.Buy_Auto
        Select Case Request.QueryString("sCodispl")
            '+ SI738 : Pagos masivos de siniestros.
            Case "SI738"
                mobjClaim = New eClaim.T_PayCla
                With Request
                    If .QueryString("nZone") = 1 Then
                        insvalClaim = mobjClaim.insValSI738_K(Request.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcdPayDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCod_Agree"), eFunctions.Values.eTypeData.etdDouble), .Form("tctClientCollect"))
                        Session("SI738_nBranch") = .Form("cbeBranch")
                        Session("SI738_nProduct") = .Form("valProduct")
                        Session("SI738_nPolicy") = .Form("tcnPolicy")
                        Session("SI738_nCertif") = .Form("tcnCertif")
                        Session("SI738_nCod_Agree") = .Form("tcnCod_Agree")
                        Session("SI738_nUser") = .Form("valUsercod")
                        Session("SI738_sClientCont") = .Form("tctClientCollect")

                        'Else                
                        'If .Form("tcnCheck")= "1" Then                    
                        '    insvalClaim = mobjClaim.insValSI738(Request.QueryString("sCodispl"),                     '                                        mobjValues.StringToType(.Form("cbeWayPay"),eFunctions.Values.eTypeData.etdInteger),                     '                                        mobjValues.StringToType(.Form("cbePayType"),eFunctions.Values.eTypeData.etdInteger),                     '                                        mobjValues.StringToType(.Form("cbeCurrency"),eFunctions.Values.eTypeData.etdInteger),                     '                                        mobjValues.StringToType(.Form("tcdValdate"),eFunctions.Values.eTypeData.etdDate),                     '                                        mobjValues.StringToType(.Form("tcnClaim_Aux"),eFunctions.Values.eTypeData.etdDouble),                     '                                        .Form("tctClientCode"))

                        ' If insvalClaim <> vbNullstring Then		    		        
                        '     Response.Write "<NOTSCRIPT>"
                        '     Response.Write "var lintIndex;"
                        '     Response.Write "lintIndex=" & .Form("tcnIndex_Aux") & ";"
                        '     Response.Write "top.fraFolder.document.forms[0].tcnCheck.value=0;"
                        '     Response.Write "if(top.fraFolder.document.forms[0].Sel.checked)"
                        '     Response.Write "    top.fraFolder.document.forms[0].Sel.checked=false;"
                        '     Response.Write "else"
                        '     Response.Write "    top.fraFolder.document.forms[0].Sel[lintIndex].checked=false;"
                        '     Response.Write "</" & "Script>"                            
                        ' End If
                        'End If		        		    
                    End If
                End With

                '+ SI775 : Ingreso de presupuesto.
            Case "SI775"
                mobjClaim = New eClaim.Auto_Budget
                With Request
                    If .QueryString("nZone") = 1 Then
                        insvalClaim = mobjClaim.insValSI775_K(Request.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdInteger))
                    Else
                        If  .QueryString("nMainAction")  <> 401 Then
                            insvalClaim = mobjClaim.insValSI775(.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdBudget_Date"), eFunctions.Values.eTypeData.etdDate), .Form("cbeWorkshClient"), mobjValues.StringToType(.Form("tcnNum_Budget"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount_Labor"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount_Paint"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount_Mechan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount_Part"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeduc_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("optAction"), eFunctions.Values.eTypeData.etdInteger))

                            If String.IsNullOrEmpty(insvalClaim) Then
                                Dim nReturnCapitalDisponible As Double = 0
                                insvalClaim = mobjClaim.insValSI775_FromDB(.QueryString("scodispl"), mobjValues.StringToType(.Form("tcnServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), nReturnCapitalDisponible)
                                Session("SI775_CAPITALDISPONIBLE") = nReturnCapitalDisponible
                            End If
                         End If
                        End If
                End With

                '+ SI775_A: Ingreso de presupuesto de incendio.
            Case "SI775_A"
                mobjClaim = New eClaim.Fire_budget
                With Request
                    insvalClaim = mobjClaim.insValSI775_A(Request.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nServ_order"), eFunctions.Values.eTypeData.etdDouble), .Form("tctItem"), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("optAction"), eFunctions.Values.eTypeData.etdInteger))
                End With

            Case "SI010"
                mobjClaim = New eClaim.Claim_his
                If Request.QueryString("nZone") = 1 Then
                    insvalClaim = mobjClaim.insValSI010_k(mobjValues.StringToType(Request.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("nCase_num"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("nDeman_type"), eFunctions.Values.eTypeData.etdInteger))

                    mstrQueryString = "&nClaim=" & Request.Form("tcnClaim") & "&nCase_num=" & Request.Form("nCase_num") & "&nDeman_type=" & Request.Form("nDeman_type") & "&dEffecdate=" & Request.Form("tcdEffecdate")

                Else

				lblnSelected = False
				'For lintCount = 1 To Request.Form("CaseNum").Count
                For lintCount = 0 To Request.Form.GetValues("CaseNum").Count - 1
					If Request.Form("Selected").Count > 0 Then
						If Request.Form("Selected")(lintCount) = "1" Then
                            lblnSelected = True
							insvalClaim = mobjClaim.insValSI010(mobjValues.StringToType(Request.Form("Selected")(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("Movement")(lintCount), eFunctions.Values.eTypeData.etdDouble), lblnSelected, mobjValues.StringToType(Request.Form("hddnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddnCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("hddnDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("nTypMov")(lintCount), eFunctions.Values.eTypeData.etdDouble))
						Else
							lblnSelected = False
                        End If

						If insvalClaim <> vbNullString Or lblnSelected Then
                            Exit For
                        End If
					End If
                    Next
                    If lblnSelected = False Then
                        insvalClaim = mobjClaim.insValSI010(2, eRemoteDB.Constants.intNull, lblnSelected, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
                    End If

                    'UPGRADE_NOTE: Object lintCount may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lintCount = Nothing
                    'UPGRADE_NOTE: Object lblnSelected may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lblnSelected = Nothing
                End If

                '+SI051: Siniestros pendientes de pago
            Case "SI051"
                mobjClaim = New eClaim.Claim
                If Request.QueryString("nZone") = 1 Then
                    With Request
                        insvalClaim = mobjClaim.insValSI051_K("SI051", mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdInteger, True))
                    End With
                Else
                    With Request
                        If Not String.IsNullOrEmpty(.Form("tcnAuxClaim")) Then
                            If .Form("tcnAuxClaim").Count > 1 Then
                                'For lintCount = 1 To .Form("tcnAuxClaim").Count
                                For lintCount = 0 To Request.Form.GetValues("tcnAuxClaim").Count - 1
							    If .Form.GetValues("chkAuxStatus").GetValue(lintCount) = "1" Then
                                        insvalClaim = mobjClaim.insValSI051("SI051", mobjValues.StringToType(.Form.GetValues("tcnAuxClaim").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.GetValues("tcnAuxProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
                                    End If
                                Next
                            ElseIf .Form("tcnAuxClaim").Count = 1 Then
                                If .Form("chkAuxStatus") = 1 Then
                                    insvalClaim = mobjClaim.insValSI051("SI051", mobjValues.StringToType(.Form("tcnAuxClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAuxBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnAuxProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
                                End If
                            End If
                        Else
                            insvalClaim = String.Empty
                        End If
                    End With
                End If

                '+SI021_k: Control de ordenes de servicios
            Case "SI021"
                mobjProf_ord = New eClaim.Prof_ord
                insvalClaim = vbNullString
                With Request
                    If .QueryString("nZone") = 1 Then
                        '+SI021: Control de ordenes de servicio (Header)
                        insvalClaim = mobjProf_ord.insValSI021_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form("valProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOrderType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeStatus_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdFec_prog"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        '+SI021: COntrol de ordenes de servicio (Folder)

                        If .QueryString("WindowType") = "PopUp" Then
                            If .Form("hddnTypeProcess") <> 1 Then
                                insvalClaim = mobjProf_ord.insValSI021(.QueryString("sCodispl"), mobjValues.StringToType(.Form("tcnOrderServ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdMade_date"), eFunctions.Values.eTypeData.etdDate), .Form("tctMade_time"), mobjValues.StringToType(.Form("tctStaClaim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tctStaReserve"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcdDateDone"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnOrderType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeStatus_ord"), eFunctions.Values.eTypeData.etdDouble))
                            End If
                        Else
                            insvalClaim = vbNullString
                        End If
                    End If
                End With
                'UPGRADE_NOTE: Object mobjProf_ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjProf_ord = Nothing

            Case "SI091"
                'UPGRADE_NOTE: The 'ePolicy.CreditInf' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mobjClaim = Server.CreateObject("ePolicy.CreditInf")
                With Request
                    If .QueryString("nZone") = 1 Then
                        insvalClaim = mobjClaim.insValSI091(mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valproduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcddate"), eFunctions.Values.eTypeData.etdDate), .Form("valCarDealer"))
                    End If
                End With

                '+ Consultas de un siniestro de un cliente.
            Case "SIC001"
                mobjClaim = New eClaim.Claim

                With Request
                    If .QueryString("nZone") = 1 Then
                        insvalClaim = mobjClaim.insValSIC001_K(.QueryString("sCodispl"), .Form("valClient"), mobjValues.StringToType(.Form("tcdOccurdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

                '+ Consultas de un siniestro de una Póliza.
            Case "SIC002"
                mobjClaim = New eClaim.Claim

                With Request
                    If .QueryString("nZone") = 1 Then
                        insvalClaim = mobjClaim.insValSIC002_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdOccurdate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                End With

                '**+ Consultation of the movi./cobertura removal.  
                '+ Consulta del desglose de movi./cobertura.

            Case "SIC004"

                insvalClaim = ""

                mobjClaim = New eClaim.Cl_m_cover

                With Request
                    If .QueryString("nZone") = 1 Then
                        insvalClaim = mobjClaim.insValSIC004_K(Request.QueryString("sCodispl"), mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnCase_num"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("valMovement"), eFunctions.Values.eTypeData.etdInteger))

                    End If
                End With

                '+ SIC005: Consultas de operaciones de siniestros.
            Case "SIC005"
                insvalClaim = ""
                mobjClaim = New eClaim.Claim
                With Request
                    If .QueryString("nZone") = 1 Then
                        insvalClaim = mobjClaim.insValSIC005_K(Request.QueryString("sCodispl"), mobjValues.StringToDate(Request.Form("tcdInitdate")))

                    End If
                End With


                '+ SI737: Denuncios masivos
            Case "SI737"
                lclsClaim_Master = New eClaim.Claim_Master
                If Request.QueryString("nZone") = 1 Then
                    With Request
					insvalClaim = lclsClaim_Master.insValSI737_k("SI737", mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnRelat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdLedgerdat"), eFunctions.Values.eTypeData.etdDate), .Form("tctClientCollect"), CInt(Session("nCompanyUser")))
                    End With
                Else
                    With Request
                        If Request.QueryString("WindowType") = "PopUp" Then
                            If mobjValues.StringToType(.Form("chkTotalLoss"), eFunctions.Values.eTypeData.etdInteger) = eRemoteDB.Constants.intNull Then
                                lstrTotalLoss = 2
                            Else
                                lstrTotalLoss = 1
                            End If
                        insvalClaim = lclsClaim_Master.InsValSI737("SI737", mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnRelat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnRelation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cboRtype"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCredit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("Account"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeRole"), eFunctions.Values.eTypeData.etdDouble), .Form("tctClient"), mobjValues.StringToType(.Form("tcdOccurDate"), eFunctions.Values.eTypeData.etdDate), lstrTotalLoss, mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeState"), eFunctions.Values.eTypeData.etdDouble))

                            Session("nFindBenef") = lclsClaim_Master.nFind_ben
                        End If
                    End With
                End If

                '+ SI774: Cotización de repuestos - ACM - 21/06/2002
            Case "SI774"
                lclsQuot_parts = New eClaim.Quot_parts
                If Request.QueryString("nZone") = 1 Then
                    If Request.QueryString("sOriginalForm") <> vbNullString And Request.QueryString("sOriginalForm") = "SI011" Then
                        insvalClaim = vbNullString
                    Else
                        With Request
                            lintAction = mobjValues.StringToType(Request.Form("tcnActionAUX"), eFunctions.Values.eTypeData.etdInteger)

                            insvalClaim = lclsQuot_parts.insValSI774_K("SI774_K", mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnClaimNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnServiceOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnTypeOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tctStateOrder"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("cbeCaseNumber_AUX"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeDemantype_AUX"), eFunctions.Values.eTypeData.etdDouble))
                        End With
                    End If
                Else
                    If Request.QueryString("WindowType") <> "PopUp" Then
                        If Request.QueryString("sOriginalForm") <> vbNullString And Request.QueryString("sOriginalForm") = "SI011" Then
                            insvalClaim = vbNullString
                        Else


                            If Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngActionUpdate Or Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then

                                lblnChecked = False
                                If Request.Form("tcnUnitValue_AUX") <> String.Empty Then
                                    '   If Request.Form.("tcnUnitValue_AUX").Count > 0 Then
                                    If Request.Form("tcnUnitValue_AUX").Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries).Count > 0 Then
                                        lblnChecked = True
                                    End If
                                End If
                            Else
                                lblnChecked = True
                            End If

                            If Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish And Request.Form("tcnUnitValue_AUX") = String.Empty Then


                                insvalClaim = lclsQuot_parts.insValSI774("SI774", lblnChecked, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CInt(Session("nAction")), Request.QueryString("Action"), Request.QueryString("WindowType"), mobjValues.StringToType(Request.Form("tcnNum_Budget"), Values.eTypeData.etdInteger))

                            Else



                                'For lintCounter = 1 To Request.Form("tcnUnitValue_AUX").Count
                                For lintCounter = 0 To Request.Form.GetValues("tcnUnitValue_AUX").Count - 1

                                    insvalClaim = lclsQuot_parts.insValSI774("SI774", lblnChecked, CDbl(Request.Form.GetValues("tcnQuantity_AUX").GetValue(lintCounter)), Request.Form.GetValues("cbeSpareParts_AUX").GetValue(lintCounter), Request.Form.GetValues("tcnUnitValue_AUX").GetValue(lintCounter), CInt(Session("nAction")), Request.QueryString("Action"), Request.QueryString("WindowType"), mobjValues.StringToType(Request.Form("tcnNum_Budget"), Values.eTypeData.etdInteger))
                                Next
                            End If
                        End If
                    Else
                        insvalClaim = lclsQuot_parts.insValSI774("SI774", lblnChecked, mobjValues.StringToType(Request.Form("tcnQuantity"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("cbeSpareParts"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("tcnUnitValue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nAction")), eFunctions.Values.eTypeData.etdInteger), "Add", Request.QueryString("WindowType"), mobjValues.StringToType(Request.Form("tcnNum_Budget"), Values.eTypeData.etdInteger))

                        If String.IsNullOrEmpty(insvalClaim) Then
                            Dim nReturnCapitalDisponible As Double = 0
                            insvalClaim = lclsQuot_parts.insValSI774_FromDB("SI774", lblnChecked, mobjValues.StringToType(Request.Form("tcnQuantity"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("cbeSpareParts"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("tcnUnitValue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nAction")), eFunctions.Values.eTypeData.etdInteger), "Add", Request.QueryString("WindowType"), mobjValues.StringToType(CStr(Session("nClaimNumber")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCaseNumber")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDemandantType_SI774")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nModulec_SI774")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCover_SI774")), eFunctions.Values.eTypeData.etdDouble), nReturnCapitalDisponible)
                            Session("SI774_CAPITALDISPONIBLE") = nReturnCapitalDisponible
                        End If

                    End If
                End If
                'UPGRADE_NOTE: Object lclsQuot_parts may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsQuot_parts = Nothing
                lintAction = Nothing

                '+SI773: Pago de rentas	
            Case "SI773"
                mobjClaim = New eClaim.T_PayCla
                If Request.QueryString("nZone") = 1 Then
                    With Request
                        insvalClaim = mobjClaim.insValSI773_K("SI773", mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), .Form("optProcess"), mobjValues.StringToType(.Form("cbePayForm"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                Else
                    With Request
                        If .QueryString("WindowType") = "PopUp" Then
                            insvalClaim = mobjClaim.insValSI773Upd("SI773", mobjValues.StringToType(.Form("cbePayForm"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmountPay"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End With
                End If

                '+ SI777: Control de órdenes de pagos para siniestros - ACM - 25/06/2002
            Case "SI777"

                lclsClaim_his = New eClaim.Claim_his
                If Request.QueryString("nZone") = 1 Then
                    With Request
					insvalClaim = lclsClaim_his.insValSI777_k("SI777_K", mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdInitial_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcdFinal_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmountApp"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                Else

                    insValClaim_AUX = vbNullString
                    For lintCounter = 0 To Request.Form.GetValues("tcnChecked").Count - 1
                        If Request.Form("tcnChecked")(lintCounter) = "1" Then
                            insValClaim_AUX = lclsClaim_his.insValSI777(mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(CStr(Session("nCurrency")), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.GetValues("tcnAmountOrder_AUX").GetValue(lintCounter), eFunctions.Values.eTypeData.etdDouble), CStr(Session("sSche_code")))
                        End If
                    Next
                    insvalClaim = insValClaim_AUX
                End If
                'UPGRADE_NOTE: Object lclsClaim_his may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsClaim_his = Nothing

                '+ SI776: Órdenes de compras de repuestos
            Case "SI776"

                lstrFirstCase = vbNullString
                lstrFirstCase = Request.Form("valCaseNumber")

                If lstrFirstCase <> vbNullString Then
                    lstrCase = lstrFirstCase.Split("/")
                    mintCase_num = lstrCase(0)
                    mintDeman_type = lstrCase(1)
                End If

                lclsBuy_Ord = New eClaim.Buy_ord
                If Request.QueryString("nZone") = 1 Then
                    insvalClaim = lclsBuy_Ord.insValSI776(mobjValues.StringToType(Request.QueryString("nZone"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintCase_num, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintDeman_type, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnServiceOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnTypeOrder"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    insvalClaim = lclsBuy_Ord.insValSI776(mobjValues.StringToType(Request.QueryString("nZone"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(mintCase_num, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintDeman_type, eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Request.Form("tctClientCode"), Request.Form("tctName_Cont"), Request.Form("tctAdd_Contact"))
                End If
                'UPGRADE_NOTE: Object lclsBuy_Ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsBuy_Ord = Nothing

                '+ SI830: Cotización de reposición de vehículo 
            Case "SI830"
                lclsQuot_auto = New eClaim.Quot_auto
                If Request.QueryString("nZone") = 1 Then
                    With Request
                        insvalClaim = lclsQuot_auto.InsValSI830_K("SI830_K", mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCase_Num"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("cbeDeman_type"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("tcdQuot_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("valServ_Ord"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                Else
                    With Request
                        If .QueryString("WindowType") = "PopUp" Then
						insvalClaim = lclsQuot_auto.insValSI830Upd("SI830", mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble), .Form("tctDescript"), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            lintnSelCount = Request.Form("chksel").Count
						insvalClaim = lclsQuot_auto.insValSI830("SI830", mobjValues.StringToType(Request.Form("tcnOperat"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(lintnSelCount, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("cbeVehbrand"), eFunctions.Values.eTypeData.etdLong), .Form("tctVehmodel"), mobjValues.StringToType(.Form("tcnyear"), eFunctions.Values.eTypeData.etdLong))
                        End If
                    End With
                End If
                'UPGRADE_NOTE: Object lclsQuot_auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsQuot_auto = Nothing

                '+ SI831: Cotización de reposición de vehículo 
            Case "SI831"
                lclsBuy_Auto = New eClaim.Buy_Auto
                If Request.QueryString("nZone") = 1 Then
                    With Request
                        insvalClaim = lclsBuy_Auto.InsValSI831_K("SI831_K", mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCase_Num"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("cbeDeman_type"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnServiceOrder"), eFunctions.Values.eTypeData.etdDouble))
                    End With
                End If
                'UPGRADE_NOTE: Object lclsBuy_Auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsBuy_Auto = Nothing

                '+SI957: Reservas matemáticas de siniestros
            Case "SI957"
                mobjClaim = New eClaim.ValClaimRep
                With Request
                    insvalClaim = mobjClaim.insValSI957_K("SI957", mobjValues.StringToType(.Form("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                End With

            Case Else
                insvalClaim = "insvalClaim: Código lógico no encontrado (" & Request.QueryString("sCodispl") & ")"
        End Select

        '^Begin Header Block VisualTimer Utility
        Call mobjNetFrameWork.FinishProcess("insvalClaim")
        '~End Header Block VisualTimer Utility		

    End Function

    '% insPostClaim: Se realizan las actualizaciones a las tablas
    '--------------------------------------------------------------------------------------------
    Function insPostClaim() As Boolean
        Dim lstrRequest_ty As Byte
        Dim lnclaim As Object
        Dim tcnServ_Ord As Object
        Dim lstrconsecutive As Object
        Dim lstrSel As String
        Dim lstrmovement As Object
        Dim lintAction As Object
        Dim nId As String
        Dim lstrclaim As Object
    Dim lstrClientBenef As String 
	Dim ldblClaimNumber As Object
        Dim lstrtransactio As Object
    Dim nCountAux As Integer 
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lintPay_Type As Object
        Dim lintCount As Integer
        Dim lstrTotalLoss As Object
        Dim lstrMessage As String
        Dim sExecute As String


        '^Begin Header Block VisualTimer Utility
        Call mobjNetFrameWork.BeginProcess("insPostClaim")
        '~End Header Block VisualTimer Utility		

        lblnPost = False


        Dim lclsFire_budget As eClaim.Fire_budget
        Dim mobjProf_ord As eClaim.Prof_ord
        Dim lclsClaim_Master As eClaim.Claim_Master
        Dim lclsClaim As eClaim.Claim
        Dim lclsQuot_parts As eClaim.Quot_parts
        Dim mobjDocuments As eReports.Report
        Dim lclsClaim_h As eClaim.Claim_his
        Dim lclsClaim_his As eClaim.Claim_his
        Dim lclsBuy_Ord As eClaim.Buy_ord
        Dim lclsQuot_auto As eClaim.Quot_auto
        Dim lclsBuy_Auto As eClaim.Buy_Auto
        Dim mobjDocuments1 As eReports.Report
	Dim lclsBatch_param As eSchedule.Batch_param
        Select Case Request.QueryString("sCodispl")

            '+ SI738 : Pagos masivos de siniestros
            Case "SI738"
                With Request
                    If .QueryString("nZone") = 1 Then
                        Session("dPayDate") = .Form("tcdPayDate")
                        mstrQueryString = "&nBordereaux_cl=" & .Form("tcnBordereaux_cl") & "&dPayDate=" & .Form("tcdPayDate")
                        lblnPost = True
                    Else
                        '+Invoca la ventana secuencia para pagar el sinestro-beneficiario en tratamiento

                        mobjGeneralFunction = New eGeneral.GeneralFunction
                        mstrKey = mobjGeneralFunction.getsKey(CInt(Session("nUsercode")))

                        mobjClaim = New eClaim.T_PayCla
                        If .Form("tcnAuxClaim").Count > 1 Then
                            'For lintCount = 1 To .Form("tcnAuxClaim").Count
                            For lintCount = 0 To Request.Form.GetValues("tcnAuxClaim").Count - 1

                                If .Form("chkAuxStatus")(lintCount) = "1" Then

                                    lblnPost = mobjClaim.insPostSI738(mobjValues.StringToType(.Form("tcnAuxClaim")(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbePayType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("cbeAuxCurrencyOrig").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form("tctClientCode"), mobjValues.StringToType(.Form("tcdValdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nCertif")), eFunctions.Values.eTypeData.etdDouble), mstrKey)
                                End If

                            Next
                            Call insPrintDocuments()
                        End If
                        lblnPost = True
                    End If
                End With

                '+ SI775 : Ingreso de presupuesto.
            Case "SI775"
                mobjClaim = New eClaim.Auto_Budget
                With Request
                    If .QueryString("nZone") = 1 Then
                        mstrQueryString = "&nClaim=" & .Form("tcnClaim") & "&nServ_Order=" & .Form("valServ_Order")
                        lclsFire_budget = New eClaim.Fire_budget
                        If lclsFire_budget.InsValBranch_prof_ord(mobjValues.StringToType(.Form("valServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
                            mstrCodispl = "SI775_A"
                        Else
                            mstrCodispl = "SI775"
                        End If
                        lblnPost = True
                    Else

                        '*+ Se determina el monto de ajuste para la orden segun la disponibilida del capital de la cobertura.
                        Dim nSI775_Monto As Double = mobjValues.StringToType(.Form("tcnAmount"), Values.eTypeData.etdDouble)
                        Dim nSi775_CapitalDisponible As Double = mobjValues.StringToType(Session("SI775_CAPITALDISPONIBLE"), Values.eTypeData.etdDouble)
                        Dim nAmont_Ajus_Ord As Double

                        If nSI775_Monto > nSi775_CapitalDisponible And nSi775_CapitalDisponible > 0 Then
                            nAmont_Ajus_Ord = IIf((nSI775_Monto - nSi775_CapitalDisponible) < 0, 0, nSI775_Monto - nSi775_CapitalDisponible)
                            nSI775_Monto = nSi775_CapitalDisponible
                        End If
                        If  .QueryString("nMainAction")  <> 401 Then
                            lblnPost = mobjClaim.insPostSI775(Request.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("optAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnServ_Order"), eFunctions.Values.eTypeData.etdDouble), .Form("cbeWorkshClient"), mobjValues.StringToType(.Form("tcnNum_Budget"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdBudget_Date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnAmount_Labor"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount_Paint"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount_Mechan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAmount_Part"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), nSI775_Monto, mobjValues.StringToType(.Form("tcnIVA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeduc_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeprec_amount"), eFunctions.Values.eTypeData.etdDouble), nAmont_Ajus_Ord)
                        End If
                            Call insPrintDocuments()
                        End If
                End With

                '+ SI775_A : Ingreso de presupuesto para incendio.
            Case "SI775_A"
                mobjClaim = New eClaim.Fire_budget
                With Request
                    mstrQueryString = "&nServ_order=" & .QueryString("nServ_order") & "&nClaim=" & .Form("tcnClaim")
                    'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                    lblnPost = mobjClaim.insPostSI775_A(.Form("tctAction"), mobjValues.StringToType(.QueryString("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Today), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnImagenum"), eFunctions.Values.eTypeData.etdDouble), .Form("tctItem"), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnIVA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnTotal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("optAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnMat_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnHand_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeduc_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnDeprec_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdInteger))
                End With

            Case "SI010"
                If Request.QueryString("nZone") = 2 Then
                    mobjClaim = New eClaim.Claim_his
				'For lintCount = 1 To Request.Form("CaseNum").Count
                For lintCount = 0 To Request.Form.GetValues("CaseNum").Count - 1
					If Request.Form("Selected")(lintCount) = "1" Then
                            lblnPost = mobjClaim.insPostSI010(mobjValues.StringToType(Request.Form("hddnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("CaseNum").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.GetValues("DemanType").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.GetValues("Movement").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdInteger))
                        End If
                    Next
                    'UPGRADE_NOTE: Object lintCount may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lintCount = Nothing
                    'UPGRADE_NOTE: Object mobjClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mobjClaim = Nothing
                Else
                    lblnPost = True
                End If

                '+SI051: Siniestros pendientes de pago
            Case "SI051"
                mobjClaim = New eClaim.Claim

                If Request.QueryString("nZone") = 1 Then
                    Session("nBranch") = mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("nProduct") = mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("nPolicy") = mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True)
                    Session("dInitial_date") = mobjValues.StringToType(Request.Form("tcdInitial_date"), eFunctions.Values.eTypeData.etdDate)
                    Session("dFinal_date") = mobjValues.StringToType(Request.Form("tcdFinal_date"), eFunctions.Values.eTypeData.etdDate)
                    Session("nClaim") = mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble, True)
                    lblnPost = True
                Else
                    lblnPost = True
                    With Request
                        If Not String.IsNullOrEmpty(.Form("tcnAuxClaim")) Then
                            If .Form("tcnAuxClaim").Count > 1 Then
                                'For lintCount = 1 To .Form("tcnAuxClaim").Count
                                For lintCount = 0 To Request.Form.GetValues("tcnAuxClaim").Count - 1
							    If .Form.GetValues("chkAuxStatus").GetValue(lintCount) = "1" Then
                                        'lblnPost = mobjClaim.insPostSI051("SI051", mobjValues.StringToType(.Form("tcnAuxClaim")(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAuxBranch")(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnAuxProduct")(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnAuxPolicy")(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAuxCertif")(lintCount), eFunctions.Values.eTypeData.etdDouble), .Form("tctClaimtyp")(lintCount), mobjValues.StringToType(.Form("tcnMovement")(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToDate(CStr(Session("dEffecdate"))), .Form("tctClient")(lintCount), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), "2")
                                        lblnPost = mobjClaim.insPostSI051("SI051", mobjValues.StringToType(.Form.GetValues("tcnAuxClaim").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.GetValues("tcnAuxProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.GetValues("tcnAuxPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("tcnAuxCertif").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("tctClaimtyp").GetValue(lintCount), mobjValues.StringToType(.Form.GetValues("tcnMovement").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToDate(CStr(Session("dEffecdate"))), .Form.GetValues("tctClient").GetValue(lintCount), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), "2")
                                    End If
                                Next
                            ElseIf .Form("tcnAuxClaim").Count = 1 Then
                                If .Form("chkAuxStatus") = 1 Then
                                    lblnPost = mobjClaim.insPostSI051("SI051", mobjValues.StringToType(.Form("tcnAuxClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAuxBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnAuxProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnAuxPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnAuxCertif"), eFunctions.Values.eTypeData.etdDouble), .Form("tctClaimtyp"), mobjValues.StringToType(.Form("tcnMovement"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToDate(CStr(Session("dEffecdate"))), .Form("tctClient"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), "2")
                                End If
                            End If
                        End If
                    End With
                End If

            Case "SI021"
                mobjProf_ord = New eClaim.Prof_ord
                lblnPost = True
                With Request
                    If .QueryString("nZone") = 1 Then
                        '+SI021_k: Ordenes de Servicios profesionales(Header)
                        mstrQueryString = "&nProvider=" & mobjValues.StringToType(.Form("valProvider"), eFunctions.Values.eTypeData.etdDouble) & "&nBranch=" & mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble) & "&nProduct=" & mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble) & "&nPolicy=" & mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble) & "&nProponum=" & mobjValues.StringToType(.Form("tcnProponum"), eFunctions.Values.eTypeData.etdDouble) & "&nCertif=" & mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble) & "&nClaim=" & mobjValues.StringToType(.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble) & "&nOffice=" & mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble) & "&nOrderType=" & mobjValues.StringToType(.Form("cbeOrderType"), eFunctions.Values.eTypeData.etdDouble) & "&nStatus_ord=" & mobjValues.StringToType(.Form("cbeStatus_ord"), eFunctions.Values.eTypeData.etdDouble) & "&dFec_prog=" & .Form("tcdFec_prog")
                    Else
                        '+SI021: Ordenes de Servicios profesionales(Folder)
                        If .QueryString("WindowType") = "PopUp" Then
                            mstrQueryString = "&nProvider=" & mobjValues.StringToType(.Form("hddheadProvider"), eFunctions.Values.eTypeData.etdDouble) & "&nBranch=" & mobjValues.StringToType(.Form("hddheadBranch"), eFunctions.Values.eTypeData.etdDouble) & "&nProduct=" & mobjValues.StringToType(.Form("hddheadProduct"), eFunctions.Values.eTypeData.etdDouble) & "&nPolicy=" & mobjValues.StringToType(.Form("hddheadPolicy"), eFunctions.Values.eTypeData.etdDouble) & "&nProponum=" & mobjValues.StringToType(.Form("hddheadProponum"), eFunctions.Values.eTypeData.etdDouble) & "&nCertif=" & mobjValues.StringToType(.Form("hddheadCertif"), eFunctions.Values.eTypeData.etdDouble) & "&nClaim=" & mobjValues.StringToType(.Form("hddheadClaim"), eFunctions.Values.eTypeData.etdDouble) & "&nOffice=" & mobjValues.StringToType(.Form("hddheadOffice"), eFunctions.Values.eTypeData.etdDouble) & "&nOrderType=" & mobjValues.StringToType(.Form("hddheadOrderType"), eFunctions.Values.eTypeData.etdDouble) & "&nStatus_ord=" & mobjValues.StringToType(.Form("hddheadStatus_ord"), eFunctions.Values.eTypeData.etdDouble) & "&dFec_prog=" & .Form("hddheadFec_prog") & "&Type=strnull"
                            lblnPost = mobjProf_ord.insPostSI021("PopUp", mobjValues.StringToType(.Form("hddnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnNumCase"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hddnTypDemand"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("hddnTransac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnOrderServ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdMade_date"), eFunctions.Values.eTypeData.etdDate), .Form("tctMade_time"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeStatus_ord"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
                'UPGRADE_NOTE: Object mobjProf_ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjProf_ord = Nothing

            Case "SI091"
                lblnPost = True
                With Request
                    Session("nBranch") = mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdInteger)
                    Session("nProduct") = mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdInteger)
                    Session("nPolicy") = mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
                    Session("nCertif") = mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
                    Session("dEffecdate") = .Form("tcdDate")
                    Session("sCarDealer") = .Form("valCarDealer")
                End With

                '+ Consulta del desglose de movi./cobertura.
            Case "SIC004"

                Session("nClaim") = Request.Form("tcnClaim")
                Session("nCase_num") = Request.Form("tcnCase_num")
                Session("nDeman_type") = Request.Form("tcnDeman_type")
                Session("nOper_type") = Request.Form("valMovement")
                lblnPost = True

                '+Consultas  de siniestros de un cliente
            Case "SIC001"

                Session("sClient") = Request.Form("valClient")
                Session("nRol") = mobjValues.StringToType(Request.Form("valRol"), eFunctions.Values.eTypeData.etdDouble)
                Session("dOccurdate") = mobjValues.StringToType(Request.Form("tcdOccurdate"), eFunctions.Values.eTypeData.etdDate)

                lblnPost = True

                '+Consultas de siniestros de una Póliza
            Case "SIC002"
                Session("nBranch") = mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True)
                Session("nProduct") = mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                Session("nPolicy") = mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
                Session("nCertif") = mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
                Session("dOccurdate") = mobjValues.StringToType(Request.Form("tcdOccurdate"), eFunctions.Values.eTypeData.etdDate)

                lblnPost = True

                '+ SIC005: Consultas de operaciones de siniestros.
            Case "SIC005"

                Session("dInitdate") = mobjValues.StringToType(Request.Form("tcdInitdate"), eFunctions.Values.eTypeData.etdDate)
                Session("nOffice") = mobjValues.StringToType(Request.Form("cbeOffice"), eFunctions.Values.eTypeData.etdInteger)
                Session("nBranch") = mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                Session("nProduct") = mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                Session("nOper_type") = mobjValues.StringToType(Request.Form("valMoveType"), eFunctions.Values.eTypeData.etdDouble)
                Session("nCurrency") = mobjValues.StringToType(Request.Form("valCurrency"), eFunctions.Values.eTypeData.etdDouble)

                lblnPost = True


                '+ Denuncios masivos
            Case "SI737"

                With Request
                    If .QueryString("nZone") = 1 Then
                        If Not IsNothing(Session("mobjCollecRelation")) Then
                            Session("mobjCollecRelation").RemoveAll()
                        End If
                        mstrQueryString = "&tcdEffecdate=" & .Form("tcdEffecdate") & "&cbeOffice=" & mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble) & "&cbeOfficeAgen=" & mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble) & "&cbeAgency=" & mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble) & "&cbeBranch=" & mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdInteger) & "&valProduct=" & mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdInteger) & "&tcnPolicyHeader=" & mobjValues.StringToType(.Form("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble) & "&valCover=" & mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdInteger) & "&cbeCurrency=" & mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger) & "&tcnRelat=" & mobjValues.StringToType(.Form("tcnRelat"), eFunctions.Values.eTypeData.etdInteger) & "&tcdLedgerdat=" & mobjValues.StringToType(.Form("tcdLedgerdat"), eFunctions.Values.eTypeData.etdDate) & "&tctClientCollect=" & .Form("tctClientCollect") & "&hddPolitype=" & .Form("hddPolitype") & "&hddBrancht=" & .Form("hddBrancht")
                        lblnPost = True
                    Else
                        If IsNothing(Session("mobjCollecRelation")) Then
                            Session("mobjCollecRelation") = New Scripting.Dictionary
                        End If
                        If Request.QueryString("WindowType") = "PopUp" Then
                            If mobjValues.StringToType(.Form("chkTotalLoss"), eFunctions.Values.eTypeData.etdInteger) = eRemoteDB.Constants.intNull Then
                                lstrTotalLoss = 1 '+ Pérdida Parcial
                            Else
                                lstrTotalLoss = 2 '+ Pérdida Total
                            End If

                            ldblClaimNumber = 0
                            lclsClaim = New eClaim.Claim

                            ldblClaimNumber = lclsClaim.calClaimNumber(mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), CInt(Session("nCompanyUser")), mobjValues.StringToType(CStr(Session("nUserCode")), eFunctions.Values.eTypeData.etdDouble))

                            ldblClaimNumber = mobjValues.StringToType(ldblClaimNumber, eFunctions.Values.eTypeData.etdDouble)

                            lclsClaim_Master = New eClaim.Claim_Master
                            lblnPost = lclsClaim_Master.InsPostSI737_Upd("SI737", mobjValues.StringToType(.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form("tctClientCollect"), mobjValues.StringToType(.Form("tcnRelat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeCause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnRelation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cboRtype"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form("tcnGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("cbeRole"), eFunctions.Values.eTypeData.etdDouble), .Form("tctClient"), mobjValues.StringToType(.Form("tcdOccurDate"), eFunctions.Values.eTypeData.etdDate), lstrTotalLoss, mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), ldblClaimNumber, mobjValues.StringToType(.Form("cbeState"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), .Form("valIllness"), .Form("cbeState"), CShort(Session("nFindBenef")))

                            lclsClaim_Master = Nothing
                            If lblnPost Then
                                Session("mobjCollecRelation").Add(ldblClaimNumber, .Form("tcnGroup"))
                            End If
                        Else
                            lblnPost = True
                            Session("mobjCollecRelation").RemoveAll()
                        End If
					mstrQueryString = CDbl("&tcdEffecdate=") + .Form("tcdEffecdate") + CDbl("&cbeOffice=") + .Form("cbeOffice") + CDbl("&cbeOfficeAgen=") + .Form("cbeOfficeAgen") + CDbl("&cbeAgency=") + .Form("cbeAgency") + CDbl("&cbeBranch=") + .Form("cbeBranch") + CDbl("&valProduct=") + .Form("valProduct") + CDbl("&tcnPolicyHeader=") + .Form("tcnPolicyHeader") + CDbl("&valCover=") + .Form("valCover") + CDbl("&cbeCurrency=") + .Form("cbeCurrency") + CDbl("&tcnRelat=") + .Form("tcnRelat") + CDbl("&hddBrancht=") + .Form("hddBrancht") + CDbl("&hddPolitype=") + .Form("hddPolitype") + CDbl("&tctClientCollect=") & .Form("tctClientCollect")
                    End If
                    'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsClaim = Nothing
                    'UPGRADE_NOTE: Object ldblClaimNumber may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    ldblClaimNumber = Nothing

                End With

                '+ SI774: Cotización de repuestos - ACM - 21/06/2002
            Case "SI774"

                If Request.QueryString("nZone") = 1 Then
                    Dim lreProf_ord As New eClaim.Prof_ord

                    With Request
                        Session("dEffecdate") = .Form("tcdEffecdate")
                        Session("nClaimNumber") = .Form("tcnClaimNumber")
                        Session("nCaseNumber") = .Form("cbeCaseNumber_AUX")
                        Session("nServiceOrder") = .Form("tcnServiceOrder")
                        Session("nTypeOrder") = .Form("tcnTypeOrder")
                        Session("sStateOrder") = .Form("tctStateOrder")
                        Session("nDemandantType") = .Form("tcnDemandantType")
                        Session("nTransaction_SI774") = .Form("tcnTransaction")
                        Session("sOriginalForm") = .Form("tctOriginalForm")
                        Session("nAction") = .Form("tcnActionAUX")
                        Session("nMark") = .Form("cbeMark")
                        Session("sModel") = .Form("cbeModel")
                        Session("nYear") = .Form("tcnYear")
                        Session("nChassisCode") = .Form("tctChasisCode")
                        Session("SI774_tcnNum_Budget") = ""
                        'Session("Option")
                        If Request.QueryString("sOriginalForm") <> vbNullString And Request.QueryString("sOriginalForm") = "SI011" Then
                            Session("sOriginalForm") = "SI011"
                        End If
                        '+ Llamada que actualiza los valores de Audatex
                        '  lreProf_ord.receiveInspectionResult( Session("nUsercode"), Session("dEffecdate")  , ,  Session("nServiceOrder") )
                        lblnPost = True
                    End With

                Else
                    lclsQuot_parts = New eClaim.Quot_parts
                    If Request.QueryString("Action") = "Update" Or Request.QueryString("Action") = "Delete" Then
                        nId = mobjValues.StringToType(Request.Form("tcnID"), eFunctions.Values.eTypeData.etdDouble)
                    End If

                    If Request.QueryString("WindowType") = "PopUp" Then
                        Select Case Request.QueryString("Action")
                            Case "Add"
                                Session("nAction") = 1
                            Case "Update"
                                Session("nAction") = 2
                            Case "Del", "Delete"
                                Session("nAction") = 3
                        End Select


                        lblnPost = lclsQuot_parts.insPostSI774(mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nClaimNumber")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCaseNumber")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nServiceOrder")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nTypeOrder")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("sStateOrder")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDemandantType")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nAction")), eFunctions.Values.eTypeData.etdInteger), "1", mobjValues.StringToType(Request.Form("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("cbeSpareParts"), eFunctions.Values.eTypeData.etdDouble), Request.Form("chkSpare"), mobjValues.StringToType(Request.Form("tcnUnitValue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nMark")), eFunctions.Values.eTypeData.etdDouble), CStr(Session("sModel")), mobjValues.StringToType(CStr(Session("nYear")), eFunctions.Values.eTypeData.etdDouble), CStr(Session("sChassisCode")), mobjValues.StringToType(nId, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("WindowType"), mobjValues.StringToType(CStr(Session("nAction")), eFunctions.Values.eTypeData.etdDouble), Request.Form("tcnIVA"), Request.Form("tcnShipping"), Request.Form("tcnCharter"), ,, mobjValues.StringToType(Session("SI774_CAPITALDISPONIBLE"), Values.eTypeData.etdDouble))
                    Else
                        If Request.QueryString("sOriginalForm") <> vbNullString And Request.QueryString("sOriginalForm") = "SI011" Then
                            lblnPost = True
                        Else

                            Select Case Session("nMainAction")
                                Case 301
                                    Session("nAction") = 1
                                    'Case 302
                                    '    Session("nAction") = 2
                            End Select

                            If CDbl(Session("nMainAction")) <> 401 Then



                                lblnPost = lclsQuot_parts.insPostSI774(mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("nServiceOrder")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("nAction")), eFunctions.Values.eTypeData.etdInteger), vbNullString, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(Session("sModel")), eRemoteDB.Constants.intNull, CStr(Session("sChassisCode")), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("WindowType"), mobjValues.StringToType(CStr(Session("nTransaction_SI774")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnIVA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnShipping"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnCharter"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnNum_Budget"), eFunctions.Values.eTypeData.etdDouble), Session("nMainAction"))
                                If lblnPost Then
                                    '+ Se indica el nro. de propuesta generado
                                    'lstrMessage = "  Tome nota del numero de orden generada: " & lclsQuot_parts.nServ_Order_new
                                    mstServ_Order_new = mobjValues.StringToType(Request.Form("tcnNum_Budget"), Values.eTypeData.etdInteger)
                                    'Response.Write("<script>alert(""" & lstrMessage & """);</" & "Script>")

                                    Call insPrintDocuments()
                                End If
                            Else
                                     Call insPrintDocuments()
							lblnPost = True
						End If
					End If
				End If
				'UPGRADE_NOTE: Object lclsQuot_parts may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
				lclsQuot_parts = Nothing
			End If
			
			'+SI773: Pago de rentas
		Case "SI773"
			With Request
				If Request.QueryString("nZone") = 1 Then
					Session("nBranch") = mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("nProduct") = mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("nClaim") = mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble)
					Session("dStartDate") = mobjValues.StringToType(Request.Form("tcdStartDate"), eFunctions.Values.eTypeData.etdDate)
					Session("dEndDate") = mobjValues.StringToType(Request.Form("tcdEndDate"), eFunctions.Values.eTypeData.etdDate)
					Session("nProcess") = Request.Form("optProcess")
					Session("nPayForm") = mobjValues.StringToType(Request.Form("cbePayForm"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("sKey") = "SIL773" & Session("SessionId") & Session("nUsercode")
					lblnPost = True
				Else
					mobjClaim = New eClaim.T_PayCla
					If .QueryString("WindowType") <> "PopUp" Then
						'+Si el Proceso no es puntual				    						
						If CDbl(Session("nProcess")) <> 1 Then
							Select Case Session("nPayForm")
								'1 -> Solicitud automática de cheque
								Case "1"
									lstrRequest_ty = 2
									'4 -> Orden de pago Efectivo
								Case "4"
									lstrRequest_ty = 1
								Case "5"
									lstrRequest_ty = 3
									'8->Depósito en cuenta bancaria
								Case 8
									lstrRequest_ty = 4
									'9->Sin emisión de orden de pago
								Case 9
									lstrRequest_ty = 9
									'Otros
								Case Else
									lstrRequest_ty = 1
							End Select
							
							
							'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
							lblnPost = mobjClaim.insPostSI773(.Form("nClaim"), .Form("nCase_num"), .Form("nDeman_type"), .Form("sClient"), .Form("nId"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.StrNull, mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.StrNull, mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.StrNull, mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate), mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate), lstrRequest_ty, Session("nUserCode"), Session("nUsercode"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("sKey"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.StrNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.StrNull, .Form("nOfficeAgen"), .Form("nAgency"))
						Else
							lblnPost = True
						End If
						If Request.Form("chkPrint") = "1" Then
							mobjDocuments = New eReports.Report
							With mobjDocuments
								.sCodispl = "SIL773"
								.ReportFilename = "SIL773.rpt"
								.setParamField(1, "nBranch", Session("nBranch"))
								.setParamField(2, "nProduct", Session("nProduct"))
								.setParamField(3, "nClaim", Session("nClaim"))
								.setParamField(4, "dStartDate", Session("dStartDate"))
								.setParamField(5, "dEndDate", Session("dEndDate"))
								.SetStorProcParam(1, Session("sKey"))
								Response.Write(.Command)
							End With
							'UPGRADE_NOTE: Object mobjDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
							mobjDocuments = Nothing
						Else
							Call mobjClaim.deleteTempSI773(Session("sKey"))
						End If
					Else
						' Para el caso que sea pop-up 
						'						Session("nPayForm") = .Form("cbePayForm")
						Select Case .Form("cbePayForm")
							Case 1
								'Solicitud automática de orden de pago					        
								mstrCodispl = "OP06-2"
								lintPay_Type = 2
							Case 4
								'Orden de pago efectivo
								mstrCodispl = "OP06-4"
								lintPay_Type = 1
								'Transferencia					            
							Case 8
								mstrCodispl = "OP06-6"
								lintPay_Type = 4
								'Sin emisión de orden de pago
							Case 9
								mstrCodispl = "SI773"
								lintPay_Type = 9
								'Otros
							Case Else
								mstrCodispl = "SI773"
								lintPay_Type = 1
						End Select
						
						'+Se escriben las variables de Session para la conexión con ordenes de pago
						Session("OP006_sKey") = Session("sKey")
						Session("OP006_nCurrency") = mobjValues.StringToType(.Form("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger)
						Session("OP006_nCurrencyPay") = mobjValues.StringToType(.Form("cbeCurrencyPay"), eFunctions.Values.eTypeData.etdInteger)
						Session("OP006_sBenef") = .Form("valClient_Rep")
						Session("OP006_nConcept") = 6
						'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
						Session("OP006_dReqDate") = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
						Session("OP006_sCodispl") = .QueryString("sCodispl")
						Session("OP006_nAmount") = mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nAmountPay") = mobjValues.StringToType(.Form("tcnAmountPayCurrPay"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nPayOrderTyp") = mobjValues.StringToType(lintPay_Type, eFunctions.Values.eTypeData.etdInteger)
						Session("OP006_nClaim") = .Form("tcnClaim")
						Session("OP006_nCase_Num") = .Form("nCase_Num")
						Session("OP006_nDeman_type") = .Form("nDeman_type")
						Session("OP006_sClient") = .Form("valBenef")
						Session("OP006_nId") = .Form("nId")
						Session("OP006_dReqDate") = .Form("tcdPaydate")
						Session("OP006_nOffice_Pay") = mobjValues.StringToType(.Form("cbeOffice"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nOfficeAgen") = mobjValues.StringToType(.Form("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble)
						Session("OP006_nAgency") = mobjValues.StringToType(.Form("cbeAgency"), eFunctions.Values.eTypeData.etdDouble)
						
						If .Form("cbePayForm") <> 9 Then
							mstrQueryString = "&sCodispl=" & mstrCodispl & "&nCurrencypay=" & Session("OP006_nCurrencyPay") & "&nCurrency=" & Session("OP006_nCurrency") & "&nOffice=" & Session("OP006_nOffice_Pay") & "&nOfficeAgen=" & Session("OP006_nOfficeAgen") & "&nAgency=" & Session("OP006_nAgency") & "&nAmount=" & Session("OP006_nAmount") & "&nAmountPay=" & Session("OP006_nAmountPay")
							
							Response.Write("<SCRIPT>opener.top.fraFolder.document.location.href='/VTimeNet/Common/GoTo.aspx?sCodisp=OP006" & mstrQueryString & "';</" & "Script>")
                                lblnPost = True
                            Else

                                'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
							lblnPost = mobjClaim.insPostSI773(Session("OP006_nClaim"), Session("OP006_nCase_Num"), Session("OP006_nDeman_type"), Session("OP006_sClient"), Session("OP006_nId"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.StrNull, mobjValues.StringToType(CStr(Session("OP006_nAmount")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("OP006_nConcept")), eFunctions.Values.eTypeData.etdDouble, True), Session("OP006_sClient"), mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.StrNull, mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate), mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate), Session("OP006_nPayOrderTyp"), Session("nUserCode"), Session("nUserCode"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("OP006_nCurrency")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("OP006_nCurrencyPay")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("OP006_nAmountPay")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("OP006_nOffice_Pay")), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("OP006_nOfficeAgen")), eFunctions.Values.eTypeData.etdDouble, True), Session("OP006_sKey"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.StrNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.StrNull, Session("OP006_nOfficeAgen"), Session("OP006_nAgency"))

                                If lblnPost Then
                                    If Request.Form("sPrint") = "1" Then
                                        mobjDocuments = New eReports.Report
                                        With mobjDocuments
                                            .sCodispl = "SIL773"
                                            .ReportFilename = "SIL773.rpt"
                                            .setParamField(1, "nBranch", Session("nBranch"))
                                            .setParamField(2, "nProduct", Session("nProduct"))
                                            .setParamField(3, "nClaim", Session("nClaim"))
                                            .setParamField(4, "dStartDate", Session("dStartDate"))
                                            .setParamField(5, "dEndDate", Session("dEndDate"))
										.SetStorProcParam(1, Session("sKey"))
                                            Response.Write(.Command)
                                        End With
                                        'UPGRADE_NOTE: Object mobjDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                                        mobjDocuments = Nothing
                                    Else
                                        Call mobjClaim.deleteTempSI773(Session("sKey"))
                                    End If
                                End If
                            End If
                        End If
                    End If
                End With

                '+ SI777: Control de órdenes de pagos para siniestros - ACM - 25/06/2002
            Case "SI777"
                If Request.QueryString("nZone") = 1 Then
                    With Request
                        Session("nBranch") = mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nProduct") = mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nPolicy") = mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
                        Session("nAmountAp") = mobjValues.StringToType(.Form("tcnAmountApp"), eFunctions.Values.eTypeData.etdDouble)
                        Session("schek_rel") = .Form("chkRelation")
                        Session("dInitial_date") = mobjValues.StringToType(.Form("tcdInitial_date"), eFunctions.Values.eTypeData.etdDate)
                        Session("dFinal_date") = mobjValues.StringToType(.Form("tcdFinal_date"), eFunctions.Values.eTypeData.etdDate)
                        Session("nStatus_Payment") = mobjValues.StringToType(.Form("optPayment"), eFunctions.Values.eTypeData.etdDouble)
                        Session("sClient") = .Form("tctClient")
                        lblnPost = True
                    End With
                Else

                    If Request.Form("tcnChecked").Count > 0 Then
                        lstrclaim = 0
                        lstrmovement = 0
                        lstrconsecutive = 0
                        lstrtransactio = 0
                    lstrClientBenef = vbNullString 
                    nCountAux = 0
                        mobjGeneralFunction = New eGeneral.GeneralFunction
                        mstrKey = mobjGeneralFunction.getsKey(CInt(Session("nUsercode")))
                        'UPGRADE_NOTE: Object mobjGeneralFunction may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                        mobjGeneralFunction = Nothing

                        If CDbl(Session("nStatus_Payment")) = 3 Then
                            '+ Se Buscan Los Siniestros seleccionados para pagarlos
                            'For lintCount = 1 To Request.Form("tcnChecked").Count
                            For lintCount = 0 To Request.Form.GetValues("tcnChecked").Count - 1
							If Request.Form.GetValues("hdsSel").GetValue(lintCount) = "1" Then
                                If nCountAux = 0 then    
								    lstrclaim = Request.Form.GetValues("tcnClaimNumber_AUX").GetValue(lintCount)
								    lstrmovement = Request.Form.GetValues("tcnMovementNumber_AUX").GetValue(lintCount)
								    lstrconsecutive = Request.Form.GetValues("tcnConsecutive").GetValue(lintCount)
								    lstrtransactio = Request.Form.GetValues("tcnTransactio").GetValue(lintCount)
                                    lstrClientBenef = Request.Form.GetValues("hddClient").GetValue(lintCount)
                                Else
								    lstrclaim = lstrclaim & "," & Request.Form.GetValues("tcnClaimNumber_AUX").GetValue(lintCount)
								    lstrmovement = lstrmovement & "," & Request.Form.GetValues("tcnMovementNumber_AUX").GetValue(lintCount)
								    lstrconsecutive = lstrconsecutive & "," & Request.Form.GetValues("tcnConsecutive").GetValue(lintCount)
								    lstrtransactio = lstrtransactio & "," & Request.Form.GetValues("tcnTransactio").GetValue(lintCount)
                                    lstrClientBenef = vbNullString 
                                End If
                                nCountAux = nCountAux + 1    
							End If
                            Next

                            Session("lnclaim") = lnclaim
                            Session("lstrclaim") = lstrclaim
                            Session("lstrmovement") = lstrmovement
                            Session("lstrconsecutive") = lstrconsecutive
                            Session("lstrtransactio") = lstrtransactio
                            Session("mstrKey") = mstrKey

                            lclsClaim_h = New eClaim.Claim_his
                            Call lclsClaim_h.inpostreaop(lstrclaim)

						
                            Session("OP006_nAmountPay") = Request.Form("tcnTotalAmount")
                            Session("OP006_nConcept") = 6
                            Session("OP006_nAmount") = Request.Form("hddTotalAmountO")
                            Session("OP006_sCodispl") = "SI777"
                            Session("OP006_nPayOrderTyp") = 2
                            
                        If String.IsNullOrEmpty(Session("sClient")) then
                            Session("OP006_sBenef") = lstrClientBenef   
                        Else         
                            If String.IsNullOrEmpty(lstrClientBenef) Then
                            Session("OP006_sBenef") = Session("sClient")
                            Else 
                                Session("OP006_sBenef") = lstrClientBenef     
                            End If        
                        End If
						
                            Session("OP006_nCurrency") = Request.Form("hddCurrencyO")

                            '+ Se llama a la orden de Pago con los parametros arriba seteados						
						Response.Write("<SCRIPT>top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=OP06-2&nCurrencypay=" & Session("OP006_nCurrencyPay") & "&nCurrency=" & Session("OP006_nCurrency") & "&nOfficepay=" & Session("OP006_nOffice_Pay") & "&nAmountpay=" & Session("OP006_nAmountPay") & "&nAmount=" & Session("OP006_nAmount") & "&nOffice=" & Session("nOffice") & "&nOfficeAgen=" & Session("nOfficeAgen") & "&nAgency=" & Session("nAgency") & "&nPayOrderTyp=" & Session("OP006_nPayOrderTyp") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nTypesupport=" & Session("SI008_cbeDoc_Type") & "&nDoc_support=" & Session("SI008_tcnInvoice") & "';</" & "Script>")
                            'UPGRADE_NOTE: Object lclsClaim_h may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                            lclsClaim_h = Nothing
                        Else
                            lstrSel = "0"
                            'For lintCount = 1 To Request.Form("tcnChecked").Count
                            For lintCount = 0 To Request.Form.GetValues("tcnChecked").Count - 1
                                If Request.Form.GetValues("tcnChecked").GetValue(lintCount) = "1" Then
                                    lclsClaim_his = New eClaim.Claim_his
                                    lblnPost = lclsClaim_his.insPostSI777(mobjValues.StringToType(Request.Form.GetValues("tcnClaimNumber_AUX").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nStatus_Payment")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("tcnMovementNumber_AUX").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.GetValues("tcnConsecutive").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), Request.Form.GetValues("tctCheque").GetValue(lintCount), mobjValues.StringToType(Request.Form.GetValues("tcnTransactio").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), CStr(Session("schek_rel")), mstrKey)

                                    lstrSel = "1"
                                    'UPGRADE_NOTE: Object lclsClaim_his may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                                    lclsClaim_his = Nothing
                                Else
                                    lblnPost = True
                                End If
                            Next
                        End If
                        If CStr(Session("nStatus_Payment")) = "1" And lstrSel = "1" And lblnPost Then
                            Call insPrintDocuments()
                        End If
                    Else
                        lblnPost = True
                    End If
                End If

                '+ SI776: Órdenes de compras de repuestos
            Case "SI776"
                lclsBuy_Ord = New eClaim.Buy_ord

                If Request.QueryString("nZone") = 1 Then
                    mstrQueryString = "&nClaim=" & Request.Form("tcnClaim") & "&nCase=" & mintCase_num & "&nDeman_type=" & mintDeman_type & "&nServiceOrder=" & Request.Form("tcnServiceOrder") & "&dQuot_date=" & Request.Form("tcdEffecdate") & "&nBranch_Fire=" & Request.Form("tcnBranch_Fire")
                    lblnPost = True
                Else
                    If Request.Form("tcnQuantity_parts_AUX").Count > 0 Then
                        lblnPost = lclsBuy_Ord.insPostSI776(mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnCase"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("dOrder_date"), eFunctions.Values.eTypeData.etdDate), Request.Form("tctClientCode"), Request.Form("nID_AUX"), Request.Form("tcnQuantity_parts_AUX"), Request.Form("valSpare_parts_AUX"), Request.Form("chkOriginal_spare_AUX"), Request.Form("tcnUnit_value_AUX"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctName_Cont"), Request.Form("tctPhone_Cont"), Request.Form("tctAdd_Contact"), mobjValues.StringToType(Request.Form("valMunicipality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnIva"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnSendCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnFreightage"), eFunctions.Values.eTypeData.etdDouble, True))
                        If lblnPost Then
                            '+ Se indica el nro. de propuesta generado
                            lstrMessage = "  Tome nota del numero de orden generada: " & lclsBuy_Ord.nServ_Order
						Response.Write("<SCRIPT>alert(""" & lstrMessage & """);</" & "Script>")
                        End If
                    Else
                        lblnPost = True
                    End If
                End If

                'UPGRADE_NOTE: Object lclsBuy_Ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsBuy_Ord = Nothing
                'UPGRADE_NOTE: Object lintCount may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lintCount = Nothing

                '+ SI830: Cotización de reposición de vehículo
            Case "SI830"
                If Request.QueryString("nZone") = 1 Then
                    mstrQueryString = "&nClaim=" & Request.Form("tcnClaim") & "&nCase_Num=" & Request.Form("tcnCase_Num") & "&nDeman_type=" & Request.Form("cbeDeman_type") & "&dQuot_date=" & Request.Form("tcdQuot_date") & "&nServ_Ord=" & Request.Form("valServ_Ord")
                    lblnPost = True
                Else
                    lclsQuot_auto = New eClaim.Quot_auto
                    With Request
                        If .QueryString("WindowType") = "PopUp" Then
                            Select Case Request.QueryString("Action")
                                Case "Add"
                                    lintAction = 1
                                Case "Update"
                                    lintAction = 2
                                Case "Del", "Delete"
                                    lintAction = 3
                            End Select

                            lblnPost = lclsQuot_auto.InsPostSI830Upd(mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("tcnServ_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnId"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("tcdQuot_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctdescript"), mobjValues.StringToType(Request.Form("cbeVehbrand"), eFunctions.Values.eTypeData.etdLong), Request.Form("tctVehmodel"), mobjValues.StringToType(Request.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnyear"), eFunctions.Values.eTypeData.etdLong), Request.Form("tctCliename"), Request.Form("chkSel"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
                            If lblnPost Then
                                mstrQueryString = "&nClaim=" & Request.Form("tcnClaim") & "&nCase_Num=" & Request.Form("tcnCase_Num") & "&nDeman_type=" & Request.Form("cbeDeman_type") & "&dQuot_date=" & Request.Form("tcdQuot_date") & "&nServ_Ord=" & Request.Form("tcnServ_ord") & "&nMainAction=" & Request.QueryString("nMainAction") & "&sAtention=" & Request.Form("tctCliename") & "&nVehbrand=" & Request.Form("cbeVehbrand") & "&sVehmodel=" & Request.Form("tctVehmodel") & "&nYear=" & Request.Form("tcnyear")
                            End If
                        Else
                            lblnPost = lclsQuot_auto.InsPostSI830(mobjValues.StringToType(Request.Form("tcnOperat"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form("tcnServ_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcdQuot_date"), eFunctions.Values.eTypeData.etdDate), Request.Form("tctAtention"), mobjValues.StringToType(Request.Form("cbeVehbrand"), eFunctions.Values.eTypeData.etdLong), Request.Form("tctVehmodel"), mobjValues.StringToType(Request.Form("tcnyear"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End With
                    'UPGRADE_NOTE: Object lclsQuot_auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsQuot_auto = Nothing
                End If

                '+ SI831: Órdenes de compras de repuestos
            Case "SI831"
                lclsBuy_Auto = New eClaim.Buy_Auto

                If Request.QueryString("nZone") = 1 Then
                    mstrQueryString = "&nClaim=" & Request.Form("tcnClaim") & "&nCase_num=" & Request.Form("tcnCase_num") & "&nDeman_type=" & Request.Form("tcnDeman_type") & "&nServiceOrder=" & Request.Form("tcnServiceOrder") & "&dBuyDate=" & Request.Form("tcdEffecdate")
                    lblnPost = True
                Else
                    If Request.Form("nActionOrd") = 301 Then
                        If Request.Form("chkSel").Count > 0 Then
                            lblnPost = lclsBuy_Auto.insPostSI831(mobjValues.StringToType(Request.Form("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnCase_Num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcdBuyDate"), eFunctions.Values.eTypeData.etdDate), Request.Form("tctClientCode"), Request.Form("tctCondic"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnTotalIva"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctClientCon"), Request.Form("tctNombreCon"), Request.Form("tctPhone_Cont"), Request.Form("tctAdd_Contact"), mobjValues.StringToType(Request.Form("valMunicipality"), eFunctions.Values.eTypeData.etdDouble))

                            '+ Se indica el nro. de propuesta generado
                            If lblnPost Then
							lstrMessage = "  Tome nota del numero de orden generada: " & lclsBuy_Auto.nServ_Ord
							Response.Write("<SCRIPT>alert(""" & lstrMessage & """);</" & "Script>")
							tcnServ_Ord = lclsBuy_Auto.nServ_Ord
                            End If
                        Else
                            lblnPost = True
                        End If
                    Else
                        tcnServ_Ord = Request.Form("tcnServ_Order")
                        lblnPost = True
                    End If
                    '+ Genera reporte de salida 
                    mobjDocuments1 = New eReports.Report
                    With mobjDocuments1
                        .sCodispl = "SI831"
                        .ReportFilename = "SI831.rpt"
					.SetStorProcParam(1, mobjValues.StringToType(tcnServ_Ord, eFunctions.Values.eTypeData.etdDouble))
                        Response.Write(.Command)
                    End With
                    'UPGRADE_NOTE: Object mobjDocuments1 may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mobjDocuments1 = Nothing
                End If
                'UPGRADE_NOTE: Object lclsBuy_Auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsBuy_Auto = Nothing
                'UPGRADE_NOTE: Object lintCount may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lintCount = Nothing

                '+Reserva matemática de siniestros			
            Case "SI957"
                If Request.Form("optEjecucion") = "1" Then 'Forma definitiva 
                    sExecute = "1"
                Else
                    'Forma preliminar
                    sExecute = "2"
                End If
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mobjClaim = New eClaim.ValClaimRep
                    If mobjClaim.insPostSI957(mobjValues.StringToType(Request.Form("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUserCode")), eFunctions.Values.eTypeData.etdLong), Session("SessionId"), sExecute) Then
                        lblnPost = True
                        lstrKey683 = mobjClaim.sKey
                        Call insPrintDocuments()
                    End If
                Else
				lclsBatch_param = New eSchedule.Batch_param
                    With lclsBatch_param
                        .nBatch = 151
                        .nUsercode = mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble)
                        '+Parametros de entrada 
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(CStr(Session("nUserCode")), eFunctions.Values.eTypeData.etdLong))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, sExecute)
                        '+Parametros de salida
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, sExecute)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                        .Save()
                    End With

				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    'UPGRADE_NOTE: Object lclsBatch_param may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsBatch_param = Nothing
                    lblnPost = True
                End If
        End Select

        insPostClaim = lblnPost

        '^Begin Header Block VisualTimer Utility
        Call mobjNetFrameWork.FinishProcess("insPostClaim")
        '~End Header Block VisualTimer Utility		

    End Function

    '**% insPrintDocuments: Document printing
    '%   insPrintDocuments: Impresión de los documentos
    '-----------------------------------------------------------------------------------------
    Private Sub insPrintDocuments()
        '-----------------------------------------------------------------------------------------
        Dim mobjDocuments As eReports.Report
        Dim sExecute As String
        Dim lblnExportReport As Object
        Dim lobjCrystalExport As Object

        mobjDocuments = New eReports.Report

        With mobjDocuments
            Select Case Request.QueryString("sCodispl")
                '+ Impresión de órdenes de pago aprobadas
                Case "SI777"
                    .sCodispl = "OPL714"
                    .ReportFilename = "OPL714.rpt"
                    If CStr(Session("dInitial_date")) = "" Then
                    .setParamField(1, "Desde", "01/01/2000")
                    Else
                    .setParamField(1, "Desde", Session("dInitial_date"))
                    End If
                    If CStr(Session("dFinal_date")) = "" Then
                    .setParamField(2, "Hasta", "01/01/2000")
                Else
                    .setParamField(2, "Hasta", Session("dFinal_date"))
                    End If
                    If Session("nPolicy") = eRemoteDB.Constants.intNull Then
                    .setParamField(3, "Poliza", "9999999999")
                    Else
                    .setParamField(3, "Poliza", Session("nPolicy"))
                    End If
                .setStorProcParam(1, mstrKey)
                Response.Write((.Command))
                    
                Case "SI957"
                    If Request.Form("optEjecucion") = "1" Then 'Forma definitiva 
                        sExecute = "1"
                    Else
                        'Forma preliminar
                        sExecute = "2"
                    End If
                    .sCodispl = "SI957"
                    .ReportFilename = "SIL957.rpt"
                    .setStorProcParam(1, lstrKey683)
                    .setStorProcParam(2, sExecute)
                    .setStorProcParam(3, .setdate(Request.Form("tcdEffecDate")))
                    Response.Write(.Command)
                Case "SI738"
                    .sCodispl = "SI738"
                    .ReportFilename = "SI738.rpt"
                    .setStorProcParam(1, mstrKey)
                    Response.Write(.Command)
                Case "SI774"
                    .sCodispl = "SI774"
                    .ReportFilename = "SI774.rpt"
                    .setStorProcParam(1, .setdate(Session("dEffecdate")))
                    .setStorProcParam(2, Session("nClaimNumber"))
                    .setStorProcParam(3, Session("nCaseNumber"))
                    .setStorProcParam(4, Session("nServiceOrder"))
                    .setStorProcParam(5, Session("nTypeOrder"))
                    .setStorProcParam(6, Session("nDemandantType"))
                    .setStorProcParam(7, mstServ_Order_new)
                    .setStorProcParam(8, Session("nUsercode"))

                    Response.Write(.Command)
                Case "SI775"
                    .sCodispl = "SI775"
                    .ReportFilename = "SI775.rpt"
                    .setStorProcParam(1, Request.Form("tcnServ_Order"))
                    .setStorProcParam(2, Session("nUsercode"))
                    Response.Write(.Command)


            End Select
        End With
        'UPGRADE_NOTE: Object mobjDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjDocuments = Nothing
    End Sub

</script>
<%Response.Expires = -1
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("valclaim")

    mobjValues = New eFunctions.Values

    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.31
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility


%>
<%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="MSHTML 6.00.2713.1100"  >
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/ConstBatch.aspx" -->

<SCRIPT SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
    If Request.QueryString("nZone") = 1 Then
        Response.Write("<body>")
    Else
        Response.Write("<BODY CLASS=""Header"">")
    End If
%>
<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 2 $|$$Date: 15-11-11 10:30 $|$$Author: Gletelier $"

    //-----------------------------------------------------------------------------
    function CancelErrors() { self.history.go(-1) }
    //-----------------------------------------------------------------------------

    //-----------------------------------------------------------------------------
    function NewLocation(Source, Codisp) {
        //-----------------------------------------------------------------------------
        var lstrLocation = "";
        lstrLocation += Source.location;
        lstrLocation = lstrLocation.replace(/&OPENER=.*/, "") + "&OPENER=" + Codisp;
        Source.location = lstrLocation;
    }
</script>
<%
mstrCommand = "&sModule=Claim&sProject=Claim&sCodisplReload=" & Request.QueryString("sCodispl")

'+ Si no se han validado los campos de la página
If Request.Form("sCodisplReload") = vbNullString Then
	mstrErrors = insvalClaim
        Session("sErrorTable") = mstrErrors
        Session("sForm") = Request.Form.ToString
    Else
        Session("sErrorTable") = vbNullString
        Session("sForm") = vbNullString
    End If

    If mstrErrors > vbNullString Then
        With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
            '.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.QueryString.ToString) & """,""ClaimErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
        .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.QueryString.ToString) & """, ""ClaimError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")

            .Write(mobjValues.StatusControl(False, Request.QueryString("nZone"), Request.QueryString("WindowType")))
		.Write("</SCRIPT>")
        End With
    Else
	If insPostClaim Then
            If Request.QueryString("WindowType") <> "PopUp" Then
			If Request.QueryString("nAction") = eFunctions.Menues.TypeActions.clngAcceptdataFinish Then

                    '+ Al finalizar, si la variable de sesión "sOriginalForm" es distinta de blanco pero igual a "SI011" y
                    '+ si el parámetro del QueryString "sCodispl" es igual a "SI774" se cierra la ventana de la transacción,
                    '+ de lo contrario se recarga la ventana de la transacción - ACM - 07/08/2002
                    If CStr(Session("sOriginalForm")) = "SI011" And Request.QueryString("sCodispl") = "SI774" Then
                        Session("sOriginalForm") = vbNullString
					Response.Write("<SCRIPT>top.close();</SCRIPT>")
                    Else

                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<script> top.document.location.reload(); </script>")
                        Else
                            Response.Write("<script>window.close();top.opener.top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & "';</script>")
                        End If

                    End If
                Else
                    If Request.QueryString.Item("sCodispl") = "SI091" Then
                        Response.Write("<script>top.fraFolder.document.location=""/VTimeNet/Policy/PolicySeq/VI002.aspx?sCodispl=VI002&sSource=SI091&nMainAction=" & Request.QueryString.Item("nMainAction") & """</script>")
                    Else
                        If Request.Form("sCodisplReload") = vbNullString Then
                            If Request.QueryString("sCodispl") = "SI051" Then
                                If Request.QueryString("nZone") = 1 Then
                                    Response.Write("<script>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString("nMainAction") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & """;</script>")
                                Else
                                    Response.Write("<script>top.document.location.reload();</script>")
                                End If
                            Else
                                If Request.QueryString("sCodispl") = "SI775" Then
                                    If mstrCodispl = "SI775_A" Then
                                        Response.Write("<script>top.fraFolder.document.location=""" & mstrCodispl & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & mstrCodispl & mstrQueryString & """;</script>")
                                    Else
                                        Response.Write("<script>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</script>")
                                    End If
                                Else
                                    Response.Write("<script>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</script>")
                                End If
                            End If
                        Else
                            Response.Write("<script>top.close();self.history.go(-1);top.opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString("nMainAction") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & """;</script>")
                        End If
                    End If
                End If
            Else

                '+ Se recarga la página que invocó la PopUp
                Select Case Request.QueryString("sCodispl")
                    '+ Control de ordenes de servicios				
                    Case "SI021"
                        Response.Write("<script>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=0" & Request.QueryString("ReloadIndex") & mstrQueryString & "'</script>")
                    Case "SI737"
                        If Request.Form("sCodisplReload") = vbNullString Then
                            Response.Write("<script>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "'</script>")
                        Else
                            Response.Write("<script>top.close();top.opener.top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "'</script>")
                        End If
                    Case "SI774"
                        If Request.Form("sCodisplReload") = vbNullString Then
                            Response.Write("<script>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "&sOriginalForm=" & Request.Form("sOriginalForm") & "'</script>")
                        Else
                            Response.Write("<script>top.close();top.opener.top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "&sOriginalForm=" & Request.Form("sOriginalForm") & "'</script>")
                        End If
                    Case "SI775_A"
                        If Request.Form("sCodisplReload") = vbNullString Then
                            Response.Write("<script>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "&sOriginalForm=" & Request.Form("sOriginalForm") & "'</script>")
                        Else
                            Response.Write("<script>top.close();top.opener.top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "&sOriginalForm=" & Request.Form("sOriginalForm") & "'</script>")
                        End If
                    Case "SI830"
                        If Request.Form("sCodisplReload") = vbNullString Then
                            Response.Write("<script>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "&sOriginalForm=" & Request.Form("sOriginalForm") & "'</script>")
                        Else
                            Response.Write("<script>top.close();top.opener.top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "&sOriginalForm=" & Request.Form("sOriginalForm") & "'</script>")
                        End If
                    Case "SI773"
                        ' 	                    If Request.Form("sCodisplReload") <> vbNullString Then
                        '                            Response.Write "<NOTSCRIPT>top.opener.top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "&sOriginalForm=" & Request.Form("sOriginalForm") & "'</script>"
                        '						    Response.Write "<NOTSCRIPT>top.close();</script>"
                        '						    Response.Write "<NOTSCRIPT>top.opener.top.frames['fraHeader'].setPointer('');</script>"
                        '						Else 
                        If Request.Form("cbePayForm") <> 9 Then
                            Response.Write("<script>window.close();</script>")
                        Else
                            Response.Write("<script>top.close();top.opener.top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & mstrQueryString & "&sOriginalForm=" & Request.Form("sOriginalForm") & "'</script>")
                        End If
                End Select
            End If
        End If
    End If
    'UPGRADE_NOTE: Object mobjClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjClaim = Nothing
    'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjValues = Nothing

%>
</HEAD>
<body>
</BODY>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.31
                            Call mobjNetFrameWork.FinishPage("valpolicytra")
                            'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                            mobjNetFrameWork = Nothing
                            '^End Footer Block VisualTimer%>
