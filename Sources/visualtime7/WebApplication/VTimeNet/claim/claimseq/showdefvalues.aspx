<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="ePolicy" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="eProduct" %>
<%@ Import Namespace="eClient" %>
<script language="VB" runat="Server">
    Dim CalcDigit() As Object
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    Dim mobjValues As eFunctions.Values


    '% ClaimCertif: Busca el cliente del certificado de la tabla roles
    '--------------------------------------------------------------------------------------------
    Sub ClaimCertif()
        '--------------------------------------------------------------------------------------------
        Dim lclsRoles As ePolicy.Roles
        lclsRoles = New ePolicy.Roles

        If Request.QueryString("sClient") = "" Then

            If lclsRoles.Find("2", mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), 2, "", mobjValues.StringToType(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then

                Response.Write("top.fraHeader.document.forms[0].dtcClient.value='" & lclsRoles.sClient & "';")
                Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='" & lclsRoles.sDigit & "';")
                Response.Write("top.fraHeader.$('#dtcClient_Digit').change();")
            End If
        End If

        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRoles = Nothing
    End Sub

    '% ClaimClient: Busca el certificado del asegudao en la tabla roles
    '--------------------------------------------------------------------------------------------
    Sub ClaimClient()
        '--------------------------------------------------------------------------------------------
        Dim lclsRoles As ePolicy.Roles
        lclsRoles = New ePolicy.Roles

        If lclsRoles.Find("2", mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, 2, Request.QueryString("sClient"), mobjValues.StringToType(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then
            Response.Write("top.fraHeader.document.forms[0].dtcClient.value='" & lclsRoles.SCLIENT & "';")
            Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='" & lclsRoles.sDigit & "';")
            Response.Write("top.fraHeader.UpdateDiv('sCliename','" & Replace(lclsRoles.sCliename, "'", "´") & "','Normal');")
            Response.Write("top.fraHeader.document.forms[0].tcdBirthdat.value='" & IIf(lclsRoles.dBirthdate.IsEmpty, String.Empty, lclsRoles.dBirthdate) & "';")


            If mobjValues.StringToType(Request.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble, True) <= 0 Then

                Response.Write("top.fraHeader.document.forms[0].tcnCertificat.value='" & lclsRoles.nCertif & "';")
            End If
            If lclsRoles.sVIP = "1" Then
                Response.Write("top.fraHeader.document.forms[0].tcdContinue.value='" & mobjValues.DateToString(lclsRoles.dContinue) & "';")
            Else
                Response.Write("top.fraHeader.document.forms[0].tcdContinue.value='';")
            End If
        Else
            Response.Write("alert('Adv. El cliente no corresponde a la póliza');")
        End If

        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRoles = Nothing
    End Sub

    '% insShowClient: se muestra la ventana de siniestros del cliente (PolicyClaims.aspx)
    '--------------------------------------------------------------------------------------------
    Sub insShowClient()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim_shw As eClaim.Claim_shw

        lclsClaim_shw = New eClaim.Claim_shw

        With Request
            '+ Se invoca la ventana PopUp que contiene todos los siniestros del cliente.     
            Call lclsClaim_shw.showClaimIns(CStr(session("sCertype")), CInt(session("nBranch")), CInt(session("nProduct")), CInt(session("nPolicy")), CInt(session("nCertif")), CDate(session("dEffecdate")), CDbl(session("nClaim")), mobjValues.StringToType(.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), 2)

            If lclsClaim_shw.bClaimByIns Then
                Response.Write("alert('Adv. 34052 " & C_MESSAGE_34052 & "');")
                Response.Write("ShowPopUp(""/VTimeNet/Claim/ClaimSeq/PolicyClaims.aspx?sClient=" & .QueryString("sClient") & "&nCase_num=" & .QueryString("nCase_num") & "&nDeman_type=" & .QueryString("nDeman_type") & """, ""PolicyClaims"", 520, 300,""no"",""no"",150,100);")
            End If
            '+ Si el cliente no está registrado, se habilitan los campos "Apellidos paterno/materno" y "Nombres", para su inclusión.
            Response.Write("top.fraFolder.document.forms[0].tctLastName.value='';")
            Response.Write("top.fraFolder.document.forms[0].tctLastName2.value='';")
            Response.Write("top.fraFolder.document.forms[0].tctFirstName.value='';")

            If Not lclsClaim_shw.bClient Then
                Response.Write("top.fraFolder.document.forms[0].tctLastName.disabled=false;")
                Response.Write("top.fraFolder.document.forms[0].tctLastName2.disabled=false;")
                Response.Write("top.fraFolder.document.forms[0].tctFirstName.disabled=false;")
            Else

                Response.Write("top.fraFolder.document.forms[0].tctLastName.disabled=true;")
                Response.Write("top.fraFolder.document.forms[0].tctLastName2.disabled=true;")
                Response.Write("top.fraFolder.document.forms[0].tctFirstName.disabled=true;")

                Response.Write("top.fraFolder.document.forms[0].tctLastName.value='" & Replace(lclsClaim_shw.sLastname, "'", "´") & "';")
                Response.Write("top.fraFolder.document.forms[0].tctLastName2.value='" & Replace(lclsClaim_shw.sLastname2, "'", "´") & "';")
                Response.Write("top.fraFolder.document.forms[0].tctFirstName.value='" & Replace(lclsClaim_shw.sFirstname, "'", "´") & "';")
            End If

            '+ Se habilita el campo "Parentesco" sólo si el cliente introducido no corresponde al asegurado.
            If Not lclsClaim_shw.bRelaShip Then
                Response.Write("top.fraFolder.document.forms[0].cbeRelaship.disabled=false;")
            Else
                Response.Write("top.fraFolder.document.forms[0].cbeRelaship.value='';")
                Response.Write("top.fraFolder.document.forms[0].cbeRelaship.disabled=true;")
            End If
        End With

        'UPGRADE_NOTE: Object lclsClaim_shw may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim_shw = Nothing
    End Sub

    '% insShowClaimData: Muestra los datos del siniestro.
    '%                   Se utiliza para el campo Siniestro de la página SI001_K.aspx
    '-------------------------------------------------------------------------------------------- 
    Private Sub insShowClaimData()
        '--------------------------------------------------------------------------------------------   
        Dim lclsClaim_shw As eClaim.Claim_shw
        Dim lclsRoles As ePolicy.Roles
        Dim lclsClaim_hiss As eClaim.Claim_hiss

        lclsRoles = New ePolicy.Roles
        lclsClaim_shw = New eClaim.Claim_shw
        lclsClaim_hiss = New eClaim.Claim_hiss

        Call lclsClaim_shw.showClaimData("2", mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("nType"))
        With Response
            If lclsClaim_shw.bClaim Or lclsClaim_shw.bPolicy Then

                '+ Se Habilita/Deshabilita el campo Fecha de Denuncio si la transacción Modificación y Recuperación de siniestros.
                If Request.QueryString("nTransaction") = 4 Or Request.QueryString("nTransaction") = 6 Then

                    If lclsClaim_hiss.ValClaim_HisPay(mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble)) = 1 Then
                        Response.Write("top.fraHeader.document.forms[0].tcdEffecdate.disabled=true;")
                    Else
                        Response.Write("top.fraHeader.document.forms[0].tcdEffecdate.disabled=false;")
                    End If
                End If

                session("sBrancht") = lclsClaim_shw.sBrancht
                session("sTotalLoss") = lclsClaim_shw.sClaimtyp

                .Write("top.fraHeader.document.forms[0].sCertype.value='" & lclsClaim_shw.sCertype & "';")
                .Write("top.fraHeader.document.forms[0].cbeBranch.value=" & lclsClaim_shw.nBranch & ";")
                .Write("top.fraHeader.document.forms[0].valIdCatas.Parameters.Param1.sValue =" & Request.QueryString("nTransaction") & ";")
                If lclsClaim_shw.nIdCatas > 0 Then
                    .Write("top.fraHeader.document.forms[0].valIdCatas.value=" & lclsClaim_shw.nIdCatas & ";")
                    .Write("top.fraHeader.UpdateDiv('valIdCatasDesc', '" & lclsClaim_shw.sIdCatas & "','Normal');")
                Else
                    .Write("top.fraHeader.document.forms[0].valIdCatas.value='';")
                    .Write("top.fraHeader.UpdateDiv('valIdCatasDesc', '','Normal');")
                End If
                .Write("top.fraHeader.document.forms[0].valProduct.Parameters.Param1.sValue =" & lclsClaim_shw.nBranch & ";")
                .Write("top.fraHeader.document.forms[0].valProduct.value=" & lclsClaim_shw.nProduct & ";")
                .Write("top.fraHeader.UpdateDiv('valProductDesc', '" & lclsClaim_shw.sProduct & "','Normal');")
                .Write("top.fraHeader.document.forms[0].tcnPolicy.value=" & lclsClaim_shw.nPolicy & ";")
                .Write("top.fraHeader.document.forms[0].tcnCertificat.value=" & lclsClaim_shw.nCertif & ";")

                If (Request.QueryString("nOffice") = "0" Or Request.QueryString("nOffice") = "") And Request.QueryString("nOfficeAgen") = "" And Request.QueryString("nAgency") = "" Then
                    If lclsClaim_shw.nAgency > 0 Then
                        .Write("top.fraHeader.document.forms[0].cbeOfficeAgen.value='';")
                        .Write("top.fraHeader.document.forms[0].cbeOffice.value='0';")
                        .Write("top.fraHeader.BlankOfficeDepend();top.fraHeader.insInitialAgency(1,1);")
                        .Write("top.fraHeader.document.forms[0].cbeAgency.value=" & lclsClaim_shw.nAgency & ";")
                        .Write("top.fraHeader.$('#cbeAgency').change();")
                    Else
                        If lclsClaim_shw.nOffice > 0 Then
                            .Write("top.fraHeader.document.forms[0].cbeOffice.value=" & lclsClaim_shw.nOffice & ";")
                            .Write("top.fraHeader.BlankOfficeDepend();top.fraHeader.insInitialAgency(1,1);")
                        End If
                        If lclsClaim_shw.nOfficeAgen > 0 Then
                            .Write("top.fraHeader.document.forms[0].cbeOfficeAgen.value=" & lclsClaim_shw.nOfficeAgen & ";")
                            .Write("top.fraHeader.$('#cbeOfficeAgen').change();")
                        End If
                    End If
                End If

                Session("nOffice_pol") = lclsClaim_shw.nOffice
                Session("nOfficeAgen_pol") = lclsClaim_shw.nOfficeAgen
                Session("nAgency_pol") = lclsClaim_shw.nAgency
            End If

            If Request.QueryString("nType") = "1" Then
                If lclsClaim_shw.bClaim Then
                    If mobjValues.StringToType(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate) < lclsClaim_shw.dDecladat Then
                        .Write("top.fraHeader.document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsClaim_shw.dDecladat, eFunctions.Values.eTypeData.etdDate) & "';")
                    End If

                    .Write("top.fraHeader.document.forms[0].tctRequest_nu.value=""" & mobjValues.TypeToString(lclsClaim_shw.sNumForm, eFunctions.Values.eTypeData.etdDouble) & """;")
                    .Write("top.fraHeader.document.forms[0].tcdOccurrdat.value=""" & mobjValues.TypeToString(lclsClaim_shw.dOccurdat, eFunctions.Values.eTypeData.etdDate) & """;")
                    .Write("if(top.fraHeader.document.forms[0].cbeTransactio.value!=1 && top.fraHeader.document.forms[0].cbeTransactio.value!=4 && top.fraHeader.document.forms[0].cbeTransactio.value!=6) top.fraHeader.document.forms[0].tcdOccurrdat.disabled=true;  else top.fraHeader.document.forms[0].tcdOccurrdat.disabled=false;")

                    If lclsClaim_shw.bProcess Then
                        .Write("top.fraHeader.document.forms[0].tcnReference.value=" & lclsClaim_shw.nReference & ";")
                    End If

                    .Write("top.fraHeader.document.forms[0].tcdLedgerDate.value=top.fraHeader.document.forms[0].tcdEffecdate.value;")
                Else
                    'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                    .Write("top.fraHeader.document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) & "';")
                    .Write("top.fraHeader.document.forms[0].cbeOffice.value=0;")
                    .Write("top.fraHeader.BlankOfficeDepend();")
                    .Write("top.fraHeader.document.forms[0].cbeBranch.value="""";")
                    .Write("top.fraHeader.document.forms[0].tcnPolicy.value="""";")
                    .Write("top.fraHeader.document.forms[0].tcnCertificat.value=0;")
                    .Write("top.fraHeader.document.forms[0].tctRequest_nu.value="""";")
                    .Write("top.fraHeader.document.forms[0].cbeBranch.value=0;")
                    .Write("top.fraHeader.document.forms[0].valProduct.value="""";")
                    .Write("top.fraHeader.document.forms[0].tcnReference.value="""";")
                    'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                    .Write("top.fraHeader.document.forms[0].tcdLedgerDate.value='" & mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) & "';")
                    .Write("top.fraHeader.document.forms[0].sCertype.value="""";")
                End If
            End If

            If lclsClaim_shw.nPolicy > 0 Then
                If lclsClaim_shw.sRole <> vbNullString Then
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valClient','" & Replace(lclsClaim_shw.sRole, "'", "´") & "','Normal');")
                Else
                    Response.Write("top.fraHeader.UpdateDiv('valClient','');")
                End If


                If lclsClaim_shw.bPolicy Then
                    Response.Write("top.fraHeader.document.forms[0].sPoliType.value='" & lclsClaim_shw.sPolitype & "';")
                    Response.Write("top.fraHeader.document.forms[0].sCertype.value='" & lclsClaim_shw.sCertype & "';")

                    If lclsClaim_shw.sPolitype = "1" Then

                        Response.Write("top.fraHeader.document.forms[0].sPoliType.disabled=true;")
                        Response.Write("top.fraHeader.document.forms[0].tcnCertificat.disabled=true;")

                        If lclsRoles.Find("2", mobjValues.StringToType(CStr(lclsClaim_shw.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsClaim_shw.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsClaim_shw.nPolicy), eFunctions.Values.eTypeData.etdDouble), 0, 2, "", lclsClaim_shw.dOccurdat, True) Then


                            Response.Write("top.fraHeader.document.forms[0].dtcClient.value='" & lclsRoles.SCLIENT & "';")
                            Response.Write("top.fraHeader.document.forms[0].dtcClient.disabled=true;")
                            Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='" & lclsRoles.sDigit & "';")
                            Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.disabled=true;")
                            Response.Write("top.fraHeader.UpdateDiv('sCliename','" & Replace(lclsRoles.sCliename, "'", "´") & "','Normal');")
                            Response.Write("top.fraHeader.document.forms[0].tcdBirthdat.value='" & IIf(lclsRoles.dBirthdate.IsEmpty, String.Empty, lclsRoles.dBirthdate) & "';")
                        End If
                    Else
                        If mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                            Response.Write("top.fraHeader.document.forms[0].tcnCertificat.disabled=false;")
                            Response.Write("top.fraHeader.document.forms[0].sPoliType.disabled=false;")
                            Response.Write("top.fraHeader.document.forms[0].dtcClient.disabled=false;")
                            Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.disabled=false;")
                        Else
                            If lclsRoles.Find("2", mobjValues.StringToType(CStr(lclsClaim_shw.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsClaim_shw.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsClaim_shw.nPolicy), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsClaim_shw.nCertif), eFunctions.Values.eTypeData.etdDouble), 2, "", mobjValues.StringToType(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then

                                Response.Write("top.fraHeader.document.forms[0].dtcClient.value='" & lclsRoles.SCLIENT & "';")
                                Response.Write("top.fraHeader.document.forms[0].dtcClient.disabled=true;")
                                Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='" & lclsRoles.sDigit & "';")
                                Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.disabled=true;")
                                Response.Write("top.fraHeader.UpdateDiv('sCliename','" & Replace(lclsRoles.sCliename, "'", "´") & "','Normal');")
                                Response.Write("top.fraHeader.document.forms[0].tcdBirthdat.value='" & IIf(lclsRoles.dBirthdate.IsEmpty, String.Empty, lclsRoles.dBirthdate) & "';")
                            End If
                        End If
                    End If

                    '+ Se localiza y se asigna el valor al campo "Último movimiento" - ACM - 29/05/2002
                    If lclsClaim_shw.sType <> vbNullString Then
                        Response.Write("top.fraHeader.UpdateDiv('tcnLastMovement','" & lclsClaim_shw.sType & "','Normal');")
                    End If

                    Response.Write("top.fraHeader.UpdateDiv('valStatuspol','" & lclsClaim_shw.sStatus_polDes & "','Normal');")

                    If lclsClaim_shw.sIntermed <> vbNullString Then
                        Response.Write("top.fraHeader.UpdateDiv('valIntermedia','" & Replace(lclsClaim_shw.sIntermed, "'", "´") & "','Normal');")
                    End If
                End If
            End If
        End With

        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lclsClaim_shw may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim_shw = Nothing
        'UPGRADE_NOTE: Object lclsClaim_hiss may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim_hiss = Nothing
    End Sub

    '% insChangeTotalLoss: se habilita/deshabilita el campo Pérdida total de la SI004
    '%                       Se utiliza para el campo Causa de la página SI004.aspx
    '--------------------------------------------------------------------------------------------
    Private Sub insChangeTotalLoss()
        'dim ebtLife As Integer
        'dim ebtAuto As Integer
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim_caus As eClaim.Claim_caus
        Dim lclsProduct As eProduct.Product

        lclsClaim_caus = New eClaim.Claim_caus
        lclsProduct = New eProduct.Product


        '+ Si el tipo de ramo es de vida y la causa del siniestro es muerte (1), muerte violenta (2)
        '+ o suicidio o si el tipo de ramo es automóvil y la causa es robo (1), se marca el tipo de pérdida como
        '+ total y no se deja modificar su contenido
        With Response
            If lclsClaim_caus.Find(CInt(session("nBranch")), CInt(session("nProduct")), mobjValues.StringToType(Request.QueryString("nClaimCaus"), eFunctions.Values.eTypeData.etdDouble)) Then

                .Write("top.fraFolder.document.forms[0].hddTotalLoss.value=" & lclsClaim_caus.sClaimtyp & ";")


                If lclsClaim_caus.sClaimtyp = "2" Then
                    .Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=true;")
                    .Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=true;")
                Else
                    If lclsClaim_caus.sClaimtyp = "1" Then
                        .Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=false;")
                        .Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=true;")
                    End If
                End If

                If lclsProduct.Find(CInt(session("nBranch")), CInt(session("nProduct")), CDate(session("dEffecdate"))) Then
                    session("sBrancht") = lclsProduct.sBrancht
                    If lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmlife Or lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmAuto Then
                        If lclsClaim_caus.sClaimtyp = "3" Then
                            .Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=false;")
                            .Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=false;")
                        End If
                    Else
                        If (lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmLife Or lclsProduct.sBrancht = 2) And (Request.QueryString("nClaimCaus") = "3" Or Request.QueryString("nClaimCaus") = "4") Then
                            .Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=true;")
                            .Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=true;")
                        ElseIf lclsProduct.sBrancht <> "6" Then
                            .Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=false;")
                            .Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=false;")
                        End If
                    End If
                End If
            End If
        End With

        'UPGRADE_NOTE: Object lclsClaim_caus may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim_caus = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsProduct = Nothing
    End Sub

    '% DefaultPrescDate: se calcula la fecha de entrega de documentos de acuerdo a la fecha de 
    '%                     ocurrencia del siniestro.
    '%                     Se utiliza para el campo Fecha de ocurrncia de la página SI004.aspx
    '--------------------------------------------------------------------------------------------
    Private Sub DefaultPrescDate()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim As eClaim.Claim
        Dim lclsProduct As eProduct.Product
        Dim lintPrescriptionDays As Integer
        Dim llngPrescriptionDate As Date

        lclsClaim = New eClaim.Claim
        lclsProduct = New eProduct.Product

        Call lclsClaim.Find(mobjValues.StringToType(CStr(session("nClaim")), eFunctions.Values.eTypeData.etdDouble))

        If lclsProduct.Find(mobjValues.StringToType(CStr(session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("dOccurdat"), eFunctions.Values.eTypeData.etdDate)) Then
            If lclsProduct.nClaim_pres = eRemoteDB.Constants.intNull Then
                lintPrescriptionDays = 0
            Else
                lintPrescriptionDays = lclsProduct.nClaim_pres
            End If
        Else
            lintPrescriptionDays = 0
        End If

        '+ Si en el Diseñador de productos no se indica plazo para la entrega de documentos, el campo     
        '+ "Fecha de entrega de documentos", puede quedar vacio pudiendo ser modificado por el usuario.
        If lclsClaim.dOccurdat <> eRemoteDB.Constants.dtmNull And lintPrescriptionDays <> 0 Then
            llngPrescriptionDate = lclsClaim.dOccurdat.AddDays(lintPrescriptionDays)
        Else
            llngPrescriptionDate = eRemoteDB.Constants.dtmNull
        End If

        Response.Write("top.fraFolder.document.forms[0].gmdPrescDat.value='" & mobjValues.TypeToString(llngPrescriptionDate, eFunctions.Values.eTypeData.etdDate) & "';")

        'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsProduct = Nothing
    End Sub

    '% insShowLimitDate: Se calcula la fecha de plazo para liquidar de acuerdo a la fecha de 
    '%                   declaración del siniestro.
    '%                   Se utiliza para el campo Plazo para liquidar de la página SI004.aspx
    '--------------------------------------------------------------------------------------------
    Private Sub insShowLimitDate()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim As eClaim.Claim
        Dim lclsProduct As eProduct.Product
        Dim lintClaimPayDays As Integer
        Dim llngClaimPayDate As Decimal

        lclsClaim = New eClaim.Claim
        lclsProduct = New eProduct.Product

        Call lclsClaim.Find(mobjValues.StringToType(CStr(session("nClaim")), eFunctions.Values.eTypeData.etdDouble))

        ' Se obtiene la cantidad de días para liquidar el siniestro, indicados en el Diseñador de Productos.
        If lclsProduct.Find(mobjValues.StringToType(CStr(session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("dDecladat"), eFunctions.Values.eTypeData.etdDate)) Then
            If lclsProduct.nClaim_pay = eRemoteDB.Constants.intNull Then
                lintClaimPayDays = 0
            Else
                lintClaimPayDays = lclsProduct.nClaim_pay
            End If
        Else
            lintClaimPayDays = 0
        End If

        If lclsClaim.dDecladat <> eRemoteDB.Constants.dtmNull Then
            llngClaimPayDate = CLng(lclsClaim.dOccurdat.ToOADate) + CInt(lintClaimPayDays)
        Else
            'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
            llngClaimPayDate = CLng(Today.ToOADate) + CInt(lintClaimPayDays)
        End If

        Response.Write("top.fraFolder.document.forms[0].gmdLimit_pay.value='" & mobjValues.TypeToString(System.Date.FromOADate(llngClaimPayDate), eFunctions.Values.eTypeData.etdDate) & "';")

        'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsProduct = Nothing
    End Sub

    '% InsUpdSI813: Llama al método que actualiza según la SI813
    '--------------------------------------------------------------------------------------------
    Private Sub InsUpdSI813()
        '--------------------------------------------------------------------------------------------
        Dim lclsCover As ePolicy.Cover
        lclsCover = New ePolicy.Cover
        With Request
            If lclsCover.InsPostSI813Upd("2", CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), mobjValues.StringToType(.QueryString("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble), CDate(Session("dEffecdate")), .QueryString("sClient"), mobjValues.StringToType(.QueryString("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCapital"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, CInt(Session("nUsercode")), CInt(Session("SessionId").GetHashCode()), vbNullString, "2") Then
                Response.Write("top.frames['fraFolder'].document.location.reload();")
            End If
        End With
        'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsCover = Nothing
    End Sub

    '% insShowPrescdate: se muestra la fecha limite del documento solicitado en el siniestro
    '--------------------------------------------------------------------------------------------
    Sub insShowPrescdate()
        '--------------------------------------------------------------------------------------------
        Dim llngClaim As Double
        Dim ldtmPropo_date As Date
        Dim llngDays_Presc As Integer
        Dim lclsDocuments As eClaim.Documents
        Dim ldtmPresc_Date As Object

        llngClaim = Request.QueryString("nClaim")
        ldtmPropo_date = mobjValues.StringToType(Request.QueryString("dPropodate"), eFunctions.Values.eTypeData.etdDate)
        llngDays_Presc = mobjValues.StringToType(Request.QueryString("nDays_Presc"), eFunctions.Values.eTypeData.etdDouble, True)

        lclsDocuments = New eClaim.Documents
        ldtmPresc_Date = lclsDocuments.Find_DocumentPrescDate(llngClaim, ldtmPropo_date, llngDays_Presc)

        If ldtmPresc_Date <> eRemoteDB.Constants.dtmNull Then
            Response.Write("top.fraFolder.document.forms[0].tcdPrescdate.value='" & mobjValues.TypeToString(ldtmPresc_Date, eFunctions.Values.eTypeData.etdDate) & "';")
        Else
            Response.Write("top.fraFolder.document.forms[0].tcdPrescdate.value='';")
        End If
        'UPGRADE_NOTE: Object lclsDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsDocuments = Nothing
    End Sub

    '% insShowRecover: Obtiene los ingresos por recobro que se han realizado
    '--------------------------------------------------------------------------------------------
    Private Sub insShowRecover()
        '--------------------------------------------------------------------------------------------
        Dim lclsRecover As eClaim.Recover
        lclsRecover = New eClaim.Recover

        With Response
            If lclsRecover.FindRecover(mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nTransac"), eFunctions.Values.eTypeData.etdDouble)) Then

                .Write("top.frames['fraFolder'].document.forms[0].hddtctCurrency.value='" & lclsRecover.sCurrencyDescript & "';")
                .Write("top.fraFolder.document.getElementById(""tctCurrency"").innerHTML='" & lclsRecover.sCurrencyDescript & "';")
                .Write("top.fraFolder.document.getElementById(""tcnPreviousAmou"").innerHTML='" & mobjValues.TypeToString(lclsRecover.nRec_amount, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
                .Write("top.frames['fraFolder'].document.forms[0].hddtcnPreviousAmou.value='" & lclsRecover.nRec_amount & "';")
                .Write("top.fraFolder.document.getElementById(""tcnPreviousExpense"").innerHTML='" & mobjValues.TypeToString(lclsRecover.nCost_recu, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
                .Write("top.frames['fraFolder'].document.forms[0].hddtcnPreviousExpense.value='" & lclsRecover.nCost_recu & "';")

                .Write("top.fraFolder.document.forms[0].btnNotenum.value = " & lclsRecover.nNotenum & ";")
                .Write("top.fraFolder.document.forms[0].tcnNotenum.value = " & lclsRecover.nNotenum & ";")

            Else
                .Write("top.fraFolder.document.getElementById(""tctCurrency"").innerHTML='';")
                .Write("top.fraFolder.document.getElementById(""tcnPreviousAmou"").innerHTML='';")
                .Write("top.fraFolder.document.getElementById(""tcnPreviousExpense"").innerHTML='';")

                .Write("top.fraFolder.document.forms[0].btnNotenum.value = " & eRemoteDB.Constants.intNull & ";")
                .Write("top.fraFolder.document.forms[0].tcnNotenum.value = " & eRemoteDB.Constants.intNull & ";")
            End If
        End With

        'UPGRADE_NOTE: Object lclsRecover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRecover = Nothing
    End Sub

    '% insShowCurrentAmount: Obtiene el total de los ingresos y gastos por recobro actuales que se han realizado
    '--------------------------------------------------------------------------------------------
    Private Sub insShowCurrentAmount()
        '--------------------------------------------------------------------------------------------
        Dim lclsRecover As eClaim.Recover
        lclsRecover = New eClaim.Recover

        With Response
            If lclsRecover.FindTCLRecoverSum(CStr(session("sKey")), CDbl(session("nClaim"))) Then
                .Write("top.fraFolder.document.getElementById(""tcnCurrentAmou"").innerHTML='" & lclsRecover.nRec_amount & "';")
                .Write("top.fraFolder.document.getElementById(""tcnCurrentExpense"").innerHTML='" & lclsRecover.nExpensesAmou & "';")
            Else
                .Write("top.fraFolder.document.getElementById(""tcnCurrentAmou"").innerHTML='" & 0 & "';")
                .Write("top.fraFolder.document.getElementById(""tcnCurrentExpense"").innerHTML='" & 0 & "';")
            End If
        End With

        'UPGRADE_NOTE: Object lclsRecover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRecover = Nothing
    End Sub

    '+ Obtiene los valores por defecto asociados al caso-siniestro
    '--------------------------------------------------------------------------------------------
    Private Sub FindRecover()
        '--------------------------------------------------------------------------------------------
        Dim lclsRecover As eClaim.Recover
        Dim lclsClaim As eClaim.Claim

        lclsRecover = New eClaim.Recover
        lclsClaim = New eClaim.Claim

        '+ Recuperar los datos del siniestro
        Call lclsClaim.Find(mobjValues.StringToType(CStr(session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
        session("nCaseNum") = mobjValues.StringToType(Request.QueryString("nCasenum"), eFunctions.Values.eTypeData.etdDouble)

        '+ Verificar si existe información en recover para el siniestro indicado
        With Response
            If lclsRecover.Find(mobjValues.StringToType(CStr(session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCasenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDemantype"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nTransaction"), eFunctions.Values.eTypeData.etdDouble)) Then

                .Write("top.fraFolder.document.forms[0].cbeRecoverTy.value = " & lclsRecover.nRecover_typ & ";")
                .Write("top.fraFolder.document.forms[0].cbeCurrency.value = " & lclsRecover.nCurrency & ";")
                .Write("top.fraFolder.document.forms[0].dEstDate.value=""" & mobjValues.TypeToString(lclsRecover.dEstdate, eFunctions.Values.eTypeData.etdDate) & """;")
                .Write("top.fraFolder.document.forms[0].dPresDate.value=""" & mobjValues.TypeToString(lclsRecover.dPresDate, eFunctions.Values.eTypeData.etdDate) & """;")
                .Write("top.fraFolder.document.forms[0].tcnIncome.value = " & lclsRecover.nEs_inc_re & ";")
                .Write("top.fraFolder.document.forms[0].tcnExpense.value = " & lclsRecover.nEs_cos_re & ";")
                '.Write "top.fraFolder.document.forms[0].cbeTransac.value = " & lclsRecover.nTransac & ";"
                If lclsRecover.sNum_case <> vbNullString Then
                    .Write("top.fraFolder.document.forms[0].tctCourtCase.value = """ & lclsRecover.sNum_case & """;")
                Else
                    .Write("top.fraFolder.document.forms[0].tctCourtCase.value = '';")
                End If

                If lclsRecover.sTribunal <> vbNullString Then
                    .Write("top.fraFolder.document.forms[0].tctThird.value = """ & lclsRecover.sTribunal & """;")
                Else
                    .Write("top.fraFolder.document.forms[0].tctThird.value = '';")
                End If
                .Write("top.fraFolder.document.forms[0].tctClient.value ='" & lclsRecover.sClient & "';")
                .Write("top.fraFolder.document.forms[0].cbeProvider.value = '" & mobjValues.TypeToString(lclsRecover.nProvider, eFunctions.Values.eTypeData.etdLong) & "';")
                .Write("top.fraFolder.$('#cbeProvider').change();")
            Else
                .Write("top.fraFolder.document.forms[0].cbeRecoverTy.value = '';")
                .Write("top.fraFolder.document.forms[0].cbeCurrency.value = 0;")
                .Write("top.fraFolder.document.forms[0].dEstDate.value = '';")
                .Write("top.fraFolder.document.forms[0].tcnIncome.value = 0;")
                .Write("top.fraFolder.document.forms[0].tcnExpense.value = 0;")
                .Write("top.fraFolder.document.forms[0].tctCourtCase.value = '';")
                .Write("top.fraFolder.document.forms[0].tctThird.value = '';")
                .Write("top.fraFolder.document.forms[0].tctClient.value = '';")
                .Write("top.fraFolder.document.forms[0].cbeProvider.value = '';")
            End If
        End With

        'UPGRADE_NOTE: Object lclsRecover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRecover = Nothing
        'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim = Nothing
    End Sub

    '% insShowClient: se muestra la ventana de siniestros del cliente (PolicyClaims.aspx)
    '--------------------------------------------------------------------------------------------
    Sub insShowClient_SI629()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim_shw As eClaim.Claim_shw

        lclsClaim_shw = New eClaim.Claim_shw

        With Request
            '+ Se invoca la ventana PopUp que contiene todos los siniestros del cliente.     
            Call lclsClaim_shw.showClaimIns(CStr(session("sCertype")), CInt(session("nBranch")), CInt(session("nProduct")), CInt(session("nPolicy")), CInt(session("nCertif")), CDate(session("dEffecdate")), CDbl(session("nClaim")), mobjValues.StringToType(.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), 2)

            If lclsClaim_shw.bClaimByIns Then
                Response.Write("alert('Adv. 34052 " & C_MESSAGE_34052 & "');")
                Response.Write("ShowPopUp(""/VTimeNet/Claim/ClaimSeq/PolicyClaims.aspx?sClient=" & .QueryString("sClient") & "&nCase_num=" & .QueryString("nCase_num") & "&nDeman_type=" & .QueryString("nDeman_type") & """, ""PolicyClaims"", 500, 300,""no"",""no"",150,100);")
            End If

            '+ Si el cliente no está registrado, se habilitan los campos "Apellidos paterno/materno" y "Nombres", para su inclusión.
            Response.Write("opener.document.forms[0].tctLastName.value='';")
            Response.Write("opener.document.forms[0].tctLastName2.value='';")
            Response.Write("opener.document.forms[0].tctFirstName.value='';")

            If Not lclsClaim_shw.bClient Then
                If lclsClaim_shw.nPersonTyp <> 1 Then
                    Response.Write("opener.document.forms[0].tctLastName.disabled=true;")
                    Response.Write("opener.document.forms[0].tctLastName2.disabled=true;")
                    Response.Write("opener.document.forms[0].tctFirstName.disabled=false;")
                    Response.Write("opener.document.forms[0].tcdBirthdat.disabled=true;")
                Else
                    Response.Write("opener.document.forms[0].tctLastName.disabled=false;")
                    Response.Write("opener.document.forms[0].tctLastName2.disabled=false;")
                    Response.Write("opener.document.forms[0].tctFirstName.disabled=false;")
                    Response.Write("opener.document.forms[0].tcdBirthdat.disabled=false;")
                End If
            Else
                Response.Write("opener.$('[name=tctLastName]').val(htmlDecode('" & Server.HtmlEncode(lclsClaim_shw.sLastName) & "'));")
                Response.Write("opener.$('[name=tctLastName2]').val(htmlDecode('" & Server.HtmlEncode(lclsClaim_shw.sLastName2) & "'));")
                Response.Write("opener.$('[name=tctFirstName]').val(htmlDecode('" & Server.HtmlEncode(lclsClaim_shw.sFirstName) & "'));")
                Response.Write("opener.document.forms[0].tcdBirthdat.value='" & mobjValues.TypeToString(lclsClaim_shw.dBirthDat, eFunctions.Values.eTypeData.etdDate) & "';")

                Response.Write("opener.document.forms[0].tctLastName.disabled=true;")
                Response.Write("opener.document.forms[0].tctLastName2.disabled=true;")
                Response.Write("opener.document.forms[0].tctFirstName.disabled=true;")
                Response.Write("opener.document.forms[0].tcdBirthdat.disabled=true;")

                Response.Write("opener.document.forms[0].tctRepresentCode.disabled=false;")
                Response.Write("opener.document.forms[0].tctRepresentCode_Digit.disabled=false;")
            End If

            If lclsClaim_shw.nPersonTyp <> 1 Then
                Response.Write("opener.document.forms[0].cbePersonTyp.value='2';")
            Else
                Response.Write("opener.document.forms[0].cbePersonTyp.value='1';")
            End If
            Response.Write("opener.RedrawFieldsByPersonTyp();")

            '+ Se habilita el campo "Parentesco" sólo si el cliente introducido no corresponde al asegurado.
            If Not lclsClaim_shw.bRelaShip Then
                Response.Write("opener.document.forms[0].cbeRelaship.disabled=false;")
            Else
                Response.Write("opener.document.forms[0].cbeRelaship.value='';")
                Response.Write("opener.document.forms[0].cbeRelaship.disabled=true;")
            End If

        End With
        lclsClaim_shw = Nothing
    End Sub

    '% insShowClientRep: se muestra la ventana de siniestros del cliente (PolicyClaims.aspx)
    '--------------------------------------------------------------------------------------------
    Sub insShowClientRep()
        '--------------------------------------------------------------------------------------------
        Dim lclsClient As eClient.Client
        lclsClient = New eClient.Client
        With Request

            '+ Si el cliente no está registrado, se habilitan los campos "Apellidos paterno/materno" y "Nombres", para su inclusión.
            Response.Write("opener.document.forms[0].tctRLastName.value='';")
            Response.Write("opener.document.forms[0].tctRLastName2.value='';")
            Response.Write("opener.document.forms[0].tctRFirstName.value='';")

            If Not lclsClient.Find(.QueryString("sClient")) Then
                Response.Write("opener.document.forms[0].tctRLastName.disabled=false;")
                Response.Write("opener.document.forms[0].tctRLastName2.disabled=false;")
                Response.Write("opener.document.forms[0].tctRFirstName.disabled=false;")
            Else
                Response.Write("opener.document.forms[0].tctRLastName.disabled=true;")
                Response.Write("opener.document.forms[0].tctRLastName2.disabled=true;")
                Response.Write("opener.document.forms[0].tctRFirstName.disabled=true;")

                Response.Write("opener.document.forms[0].tctRLastName.value='" & lclsClient.sLastname & "';")
                Response.Write("opener.document.forms[0].tctRLastName2.value='" & lclsClient.sLastname2 & "';")
                If lclsClient.nPerson_typ <> 1 Then
                    Response.Write("opener.document.forms[0].tctRFirstName.value='" & lclsClient.sCliename & "';")
                    Response.Write("opener.document.forms[0].hddRPersonTyp.value='2';")
                Else
                    Response.Write("opener.document.forms[0].tctRFirstName.value='" & lclsClient.sFirstname & "';")
                    Response.Write("opener.document.forms[0].hddRPersonTyp.value='1';")
                End If
            End If


        End With
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClient = Nothing
    End Sub
    '--------------------------------------------------------------------------------------------
    Private Sub insUpdSI007_total()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim As eClaim.Cl_Cover
        lclsClaim = New eClaim.Cl_Cover
        With Request
            Call lclsClaim.insPostSI007_total(CDbl(session("nClaim")), mobjValues.StringToType(.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nOldCurrency"), eFunctions.Values.eTypeData.etdDouble), CDate(session("dOccurdate_l")), CDbl(session("nTotal")), CDbl(session("nTotal")), CInt(session("nUsercode")))
        End With
        'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim = Nothing
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub insCoverDel()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim As eClaim.Cl_Cover
        Dim lclsClaim_his As eClaim.Claim_his
        Dim lblnDelCover As Object

        lclsClaim = New eClaim.Cl_Cover
        lclsClaim_his = New eClaim.Claim_his

        lblnDelCover = 0
        With Request
            If CDbl(session("nTransaction")) = 1 Or CDbl(session("nTransaction")) = 6 Then

                '+Si se esta emitiendo o recuperando se eliminan los registros en cl_cover, cl_m_cover y cov_used.
                lblnDelCover = lclsClaim.DelReserv(CDbl(session("nClaim")), mobjValues.StringToType(.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble), CInt(session("nUsercode")))
            Else
                '+Si la accion es modificar se debe generar historia indicando que la reserva se llevo a cero
                'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                lblnDelCover = lclsClaim.insPostSI007(CDbl(session("nClaim")), CInt(session("nTransaction")), .QueryString("sClient"), eRemoteDB.Constants.intNull, CDate(session("dEffecdate")), vbNullString, CInt(session("nUsercode")), mobjValues.StringToType(.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCurrency_o"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, CDate(session("dEffecdate")), mobjValues.StringToType(.QueryString("nExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDamages"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nReserve"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nPayAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("tcnFra_amount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nFrandeda"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDamProf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nBranch_led"), eFunctions.Values.eTypeData.etdDouble), .QueryString("nModulec"), .QueryString("nCover"), mobjValues.StringToType(.QueryString("nGroup"), eFunctions.Values.eTypeData.etdDouble), .QueryString("nReservstat"), .QueryString("nFrantype"), .QueryString("sAutomRep"), "1", .QueryString("nPayAmount"), 0, 0, 0, mobjValues.StringToType(.QueryString("nReserve"), eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(Request.QueryString("nReserveAnt"), eFunctions.Values.eTypeData.etdDouble), Today, .QueryString("nBill_ind"), CStr(session("SI007_Codispl")), CShort(session("sProcess_SI021")))

            End If
        End With
        If lblnDelCover Then
            Response.Write("top.frames['fraFolder'].document.location.reload();")
        End If
        'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim = Nothing
    End Sub

    '% FindChildren: se verifica si el caso tiene información relacionada
    '--------------------------------------------------------------------------------------------
    Private Sub FindChildren()
        '--------------------------------------------------------------------------------------------
        Dim lclsClaim_Case As eClaim.Claim_case
        lclsClaim_Case = New eClaim.Claim_case

        '+ Si el caso tiene información relacionada en otras ventanas de la secuencia, 
        '+ el mismo no puede ser eliminado.
        With Response
            If lclsClaim_Case.FindChildren(CDbl(session("nClaim")), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient")) Then
                .Write("alert(""Err 4332: " & C_MESSAGE_4332 & """);")
                If Request.QueryString("nLength") = 1 Then
                    .Write("top.frames['fraFolder'].document.forms[0].Sel.checked=false;")
                    .Write("top.frames['fraFolder'].marrArray[0].Sel = false;")
                Else
                    .Write("top.frames['fraFolder'].document.forms[0].Sel[" & Request.QueryString("nIndex") & "].checked=false;")
                    .Write("top.frames['fraFolder'].marrArray[" & Request.QueryString("nIndex") & "].Sel = false;")
                End If
            End If
        End With
        'UPGRADE_NOTE: Object lclsClaim_Case may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim_Case = Nothing
    End Sub

    '% InsWsDeduc: Busca Deducible 
    '--------------------------------------------------------------------------------------------
    Private Sub InsWsDeduc()
        '--------------------------------------------------------------------------------------------
        Dim lclsCl_Cover As eClaim.Cl_Cover
        lclsCl_Cover = New eClaim.Cl_Cover
        If lclsCl_Cover.Find(CDbl(session("nClaim")), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
            If lclsCl_Cover.nFra_amount > 0 Then
                Response.Write("top.fraFolder.document.forms[0].chkWsDeduc.checked=true;")
                Response.Write("top.fraFolder.document.forms[0].chkWsDeduc.disabled=false;")
            End If
        End If
        'UPGRADE_NOTE: Object lclsCl_Cover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsCl_Cover = Nothing
    End Sub

    '% FindChildren: se verifica si el caso tiene información relacionada
    '--------------------------------------------------------------------------------------------
    Private Sub Find_ProfSoon()
        '--------------------------------------------------------------------------------------------
        Dim lclsProf_ord As eClaim.Prof_ord
        lclsProf_ord = New eClaim.Prof_ord

        '+ Si el caso tiene información relacionada en otras ventanas de la secuencia, 
        '+ el mismo no puede ser eliminado.
        With Response
            If lclsProf_ord.valconstraintsi011(mobjValues.StringToType(Request.QueryString("nProf_ord"), eFunctions.Values.eTypeData.etdDouble)) Then
                If lclsProf_ord.bProf_ordSoon Then
                    .Write("alert(""Err 56168: " & C_MESSAGE_56168 & """);")
                    If Request.QueryString("nLength") = 1 Then
                        .Write("top.frames['fraFolder'].document.forms[0].Sel.checked=false;")
                        .Write("top.frames['fraFolder'].marrArray[0].Sel = false;")
                    Else
                        .Write("top.frames['fraFolder'].document.forms[0].Sel[" & Request.QueryString("nIndex") & "].checked=false;")
                        .Write("top.frames['fraFolder'].marrArray[" & Request.QueryString("nIndex") & "].Sel = false;")
                    End If
                End If
            End If
        End With
        'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsProf_ord = Nothing
    End Sub

    '% FindChildren: se verifica si el caso tiene información relacionada
    '--------------------------------------------------------------------------------------------
    Private Sub Find_CoverSi025()
        '--------------------------------------------------------------------------------------------

        '+ Si el caso tiene información relacionada en otras ventanas de la secuencia, 
        '+ el mismo no puede ser eliminado.
        session("nCover") = mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble)


    End Sub

    '% insShowClaimCliPol: se muestra la ventana de siniestros del cliente o de la póliza (PolicyClaims_a.aspx)
    '---------------------------------------------------------------------------------------------------------
    Sub insShowClaimCliPol()
        '---------------------------------------------------------------------------------------------------------
        Dim lstrParam As String

        With Request
            '+ Se invoca la ventana PopUp que contiene todos los siniestros del cliente o de una póliza     
            lstrParam = "sClient=" & .QueryString("sClient") & "&nBranch=" & .QueryString("nBranch") & "&nProduct=" & .QueryString("nProduct") & "&nPolicy=" & .QueryString("nPolicy") & "&nType=" & .QueryString("nType")

            Response.Write("ShowPopUp(""/VTimeNet/Claim/ClaimSeq/PolicyClaims_a.aspx?" & lstrParam & """, ""PolicyClaims_a"", 570, 300,""no"",""no"",150,100);")
        End With
    End Sub

</script>
<%Response.Expires = -1441
        mobjNetFrameWork = New eNetFrameWork.Layout
        mobjNetFrameWork.sSessionID = Session.SessionID
        mobjNetFrameWork.nUsercode = Session("nUsercode")
        Call mobjNetFrameWork.BeginPage("showdefvalues")
        mobjValues = New eFunctions.Values
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
        mobjValues.sSessionID = Session.SessionID
        mobjValues.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        mobjValues.sCodisplPage = "showdefvalues"
%>
<html>
<head>
    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/ConstLanguage.aspx" -->

    <script>
        //+ Variable para el control de versiones 
        document.VssVersion = "$$Revision: 3 $|$$Date: 26-08-10 22:59 $|$$Author: Ljimenez $"
    </script>
</head>
<body>
    <form name="ShowValues">
    </form>
</body>
</html>
<%
        Response.Write("<script type=""text/javascript"">")

        Select Case Request.QueryString("Field")
            Case "DefaultPrescDate"
                Call DefaultPrescDate()
            Case "ClaimData"
                Call insShowClaimData()
            Case "ClaimCaus"
                Call insChangeTotalLoss()
            Case "Client"
                Call insShowClient()
            Case "Client_SI629"
                Call insShowClient_SI629()
            Case "ClientRep"
                Call insShowClientRep()
            Case "InsUpdSI813"
                Call InsUpdSI813()
            Case "LimitDate"
                Call insShowLimitDate()
            Case "PrescDate"
                Call insShowPrescdate()
            Case "Recover"
                Call insShowRecover()
            Case "CurrentAmount"
                Call insShowCurrentAmount()
            Case "FindRecover"
                Call FindRecover()
            Case "Digit"
                'Call CalcDigit()
            Case "Reser_Total"
                Call insUpdSI007_total()
            Case "CoverDel"
                Call insCoverDel()
            Case "FindChildren"
                Call FindChildren()
            Case "WsDeduc"
                Call InsWsDeduc()
            Case "Find_ProfSoon"
                Call Find_ProfSoon()
            Case "ClaimClient"
                Call ClaimClient()
            Case "ClaimCertif"
                Call ClaimCertif()
            Case "NCover"
                Call Find_CoverSi025()
            Case "ClaimCli"
                Call insShowClaimCliPol()
        End Select

        Response.Write(mobjValues.CloseShowDefValues(Request.QueryString("sFrameCaller")))
        Response.Write("</script>")

        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
        Call mobjNetFrameWork.FinishPage("showdefvalues")
        'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjNetFrameWork = Nothing
        '^End Footer Block VisualTimer%>





