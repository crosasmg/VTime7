<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eOptionSystem" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eGeneralForm" %>

<script language="VB" runat="Server">         


    Dim mclsValues As eFunctions.Values


    '% insValPolitype: valida el tipo de póliza para habilitar/deshabilitar el certificado
    '% Debe ser invocada con funcion insDefValues
    '--------------------------------------------------------------------------------------------
    Sub insValPolitype()
        Dim lclsClient As Object
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As Object
        Dim lstrFrame As String
        Dim lstrCertype As String
        Dim lstrClient As String
        Dim lclsAccount_Pol As ePolicy.Account_Pol
        Dim lclsCertificat As ePolicy.Certificat
        Dim lintBranch As Integer
        Dim lintProduct As Integer
        Dim lclsPolicy_po As ePolicy.Policy
        Dim lclsOpt_system As eGeneral.Opt_system
        Dim lstrPolicyNum As String
        Dim lblnExist As Boolean
        Dim lclsProduct As eProduct.Product
        Dim sProduct As String

        lstrFrame = Request.QueryString.Item("sFrame")
        If lstrFrame = vbNullString Then
            lstrFrame = "fraHeader"
        End If
        lstrCertype = Request.QueryString.Item("sCertype")
        If lstrCertype = vbNullString Then
            lstrCertype = "2"
        End If

        lclsPolicy = New ePolicy.Policy
        lclsPolicy_po = New ePolicy.Policy

        If Request.QueryString.Item("sCodispl") = "VI818" Then
            If lclsPolicy_po.FindPolicybyPolicy("2", CDbl(Request.QueryString.Item("nPolicy"))) Then
                lclsProduct = New eProduct.Product
                If lclsProduct.Find(lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, Today) Then
                    sProduct = lclsProduct.sDescript
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='" & lclsPolicy_po.nProduct & "';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','" & sProduct & "','');")
                End If
            Else
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='';")
                Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','','');")
            End If
        End If

        If Request.QueryString.Item("sCodispl") = "VI009_K" Then

            '+ se agrego este manejo para el numero unico de poliza
            If lclsPolicy_po.FindPolicybyPolicy("2", CDbl(Request.QueryString.Item("nPolicy"))) Then
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy_po.nBranch & ";")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy_po.nBranch & ";")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
                If lclsPolicy_po.nProduct >0  Then
                    Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
                End If
                lclsCertificat = New ePolicy.Certificat
                With lclsCertificat
                    If .Find(lstrCertype, lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, lclsPolicy_po.nPolicy, 0) Then
                        If lclsCertificat.nDigit <> eRemoteDB.Constants.intNull Then
                            Response.Write("if(typeof(top.frames['fraHeader'].document.forms[0].tcnPolicy_Digit)!='undefined'){")
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy_Digit.value='" & lclsCertificat.nDigit & "';")
                            Response.Write("}")
                        End If
                    End If
                End With
                Session("sPolitype") = lclsPolicy_po.sPolitype
                Select Case lclsPolicy_po.sPolitype
                    Case "1"
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
                        If Request.QueryString.Item("sCodispl") = "VI009_K" Or Request.QueryString.Item("sCodispl") = "VI011" Then
                            Call insSurrenValue()
                        End If
                        If Request.QueryString.Item("sCodispl") = "VA650_K" Then
                            Call Account_Pol("0")
                        End If
                        If Request.QueryString.Item("sCodispl") = "CA888" Then
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0;")
                        End If
                    Case "2", "3"
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.focus();")
                End Select
            End If
        End If

        If Request.QueryString.Item("sCodispl") = "VI011" Then
            lintBranch = mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
            lintProduct = mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
            If lintBranch = 0 Or lintProduct = 0 Then
                If lclsPolicy.FindPolicybyPolicy("2", mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
                    lintBranch = lclsPolicy.nBranch
                    lintProduct = lclsPolicy.nProduct
                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value='" & lclsPolicy.nBranch & "';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valCode.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valCode.Parameters.Param2.sValue=" & lclsPolicy.nProduct & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valCode.Parameters.Param3.sValue=" & lclsPolicy.nPolicy & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy.nProduct & ";")
                    If lclsPolicy.nProduct > 0 Then
                        Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
                    End If
                End If
            End If
        End If

        If Request.QueryString.Item("sCodispl") = "CA088_K" Then

            lclsOpt_system = New eGeneral.Opt_system
            Call lclsOpt_system.Find()
            lstrPolicyNum = lclsOpt_system.sPolicyNum



            lblnExist = False

            If lstrPolicyNum = "1" Then '+Generales
                If (Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nPolicy") <> "0" And Request.QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
                    lblnExist = True
                End If
            Else
                If lstrPolicyNum = "2" Then '+ Ramo 
                    If (Request.QueryString.Item("nBranch") <> vbNullString And Request.QueryString.Item("nBranch") <> "0" And Request.QueryString.Item("nBranch") <> CStr(eRemoteDB.Constants.intNull) And Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nPolicy") <> "0" And Request.QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
                        lblnExist = True
                    End If
                Else
                    If lstrPolicyNum = "3" Then '+ Producto
                        If (Request.QueryString.Item("nBranch") <> vbNullString And Request.QueryString.Item("nBranch") <> "0" And Request.QueryString.Item("nBranch") <> CStr(eRemoteDB.Constants.intNull) And Request.QueryString.Item("nProduct") <> vbNullString And Request.QueryString.Item("nProduct") <> "0" And Request.QueryString.Item("nProduct") <> CStr(eRemoteDB.Constants.intNull) And Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nPolicy") <> "0" And Request.QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
                            lblnExist = True
                        End If
                    End If
                End If
            End If

            If lblnExist Then

                lstrCertype = Request.QueryString.Item("sCertype")
                If lstrCertype = vbNullString Then
                    lstrCertype = "2"
                End If

                If lclsPolicy.FindPolicyOptSystem(lstrCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then

                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy.nBranch & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy.nProduct & ";")
                    If lclsPolicy.nProduct >0 Then
                        Response.Write("top.frames['fraHeader'].$('#valProduct').change();")

                        If lstrPolicyNum <> "1" Then
                            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled=true;")
                            Response.Write("top.frames['fraHeader'].document.forms[0].btnvalProduct.disabled=true;")
                        End If
                    End If

                    If lclsPolicy.sPolitype = "2" Then
                        Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.disabled=false;")
                        Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.value='0';")
                    Else
                        Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.disabled=true;")
                        Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.value='0';")
                    End If

                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeStatus_pol.value=" & lclsPolicy.sStatus_pol & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcdDate_origin.value='" & mclsValues.StringToType(lclsPolicy.dDate_Origi, eFunctions.Values.eTypeData.etdDate) & "';")
                End If
            End If
        End If

        If lclsPolicy.Find(lstrCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
            '+Asignación del Dígito verificador de la poliza
            lclsCertificat = New ePolicy.Certificat
            With lclsCertificat
                If .Find(lstrCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0) Then
                    If lclsCertificat.nDigit <> eRemoteDB.Constants.intNull Then
                        Response.Write("if(typeof(top.frames['fraHeader'].document.forms[0].tcnPolicy_Digit)!='undefined'){")
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy_Digit.value='" & lclsCertificat.nDigit & "';")
                        Response.Write("}")
                    End If

                    If Request.QueryString.Item("sCodispl") = "CA888" Then
                        If lclsCertificat.nUser_amend >= 0 Then
                            Response.Write("top.frames['fraHeader'].document.forms[0].valUsers.value='" & lclsCertificat.nUser_amend & "';")
                        Else
                            Response.Write("top.frames['fraHeader'].document.forms[0].valUsers.value='';")
                        End If
                        Response.Write("top.frames['fraHeader'].$('#valUsers').change();")
                    End If
                End If
            End With

            Response.Write("with(top.frames['" & lstrFrame & "'].document.forms[0]){")

            If Request.QueryString.Item("sCodispl") = "CA034" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsPolicy.nOffice & ";")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & eRemoteDB.Constants.intNull & ";")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.Parameters.Param1.sValue =" & lclsPolicy.nOffice & ";")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.Parameters.Param2.sValue =" & lclsPolicy.nOfficeAgen & ";")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & mclsValues.StringToType(lclsPolicy.nOffice, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.value='" & mclsValues.StringToType(lclsPolicy.nOfficeAgen, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.value='" & mclsValues.StringToType(lclsPolicy.nAgency, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.frames['fraHeader'].$('#cbeOfficeAgen').change();")
                Response.Write("top.frames['fraHeader'].$('#cbeAgency').change();")
            End If
            Response.Write("tcnCertif.value=""0"";")
            '+Asignación del Tipo de póliza
            Session("sPolitype") = lclsPolicy.sPolitype
            Select Case lclsPolicy.sPolitype
                Case "1"
                    'Response.Write "tcnCertif.disabled=true;"
                    If Request.QueryString.Item("sCodispl") = "VI009_K" Or Request.QueryString.Item("sCodispl") = "VI011" Then
                        If Request.QueryString.Item("sCodispl") = "VI011" Then
                            Response.Write("top.frames['fraHeader'].document.forms[0].valCode.Parameters.Param4.sValue=0;")
                        End If
                        Call insSurrenValue()
                    End If
                    If Request.QueryString.Item("sCodispl") = "VA650_K" Then
                        Call Account_Pol("0")
                    End If
                Case "2", "3"
                    Response.Write("tcnCertif.disabled=false;")
                    Response.Write("tcnCertif.focus();")
            End Select
            If Request.QueryString.Item("nAction") = "401" Then
                If Not IsNothing(Request.QueryString.Item("sCodispl")) Then
                    Response.Write("valCode.disabled=false;")
                    Response.Write("btnvalCode.disabled=false;")
                End If
            End If
            If Request.QueryString.Item("sGetAgency") = "1" Then
                Response.Write("cbeOffice.value='0';")
                Response.Write("top.frames['" & lstrFrame & "'].insInitialAgency(1);")
                Response.Write("cbeAgency.value='" & lclsPolicy.nAgency & "';")
                Response.Write("top.frames['" & lstrFrame & "'].$('#cbeAgency').change();")
            End If
            If Request.QueryString.Item("sExecCertif") = "1" Then
                Response.Write("if(tcnCertif.disabled) top.frames['" & lstrFrame & "'].$('#tcnCertif').change();")
            End If
            Response.Write("}")
        Else
            If lclsPolicy_po.sPolitype = "2" Then
                Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.disabled=false;")
                Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.value='0';")
            Else
                Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.disabled=true;")
                Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.value='0';")
            End If
        End If

        If Request.QueryString.Item("sFindCliename") = "1" Then
            lstrClient = lclsPolicy.sClient
            lclsPolicy = Nothing
            lclsPolicy = New eClient.Client
            If lclsPolicy.FindClientName(lstrClient) Then
                Response.Write("top.frames['fraHeader'].UpdateDiv('tctCliename','" & lclsClient.sCliename & "','');")
            End If
        End If

        If Request.QueryString.Item("sGetAccountPol") = "1" Then
            lclsAccount_Pol = New ePolicy.Account_Pol
            If lclsAccount_Pol.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0) Then
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsAccount_Pol.dLastdate, eFunctions.Values.eTypeData.etdDate) & "';")
            Else
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) & "';")
            End If
        End If
        lclsPolicy = Nothing
        lclsPolicy_po = Nothing
        lclsProduct = Nothing
    End Sub

    '% insShowPolicy: se muestran los datos asociados al número de póliza.
    '%                Se utiliza para el campo Póliza de la página CA001_K.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowPolicyCA789()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsClient As eClient.Client
        Dim eAgen As eAgent.Intermedia
        Dim lclsAgencies As Object
        Dim lclsCertificat As ePolicy.Certificat
        lclsCertificat = New ePolicy.Certificat
        lclsPolicy = New ePolicy.Policy
        lclsClient = New eClient.Client
        eAgen = New eAgent.Intermedia

        If Not IsNothing(Request.QueryString.Item("nPolicy")) Then
            If lclsPolicy.FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then

                '+Asignación del campo Oficina
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value   = " & lclsPolicy.nBranch & " ;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled   = false;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue= " & lclsPolicy.nBranch & " ;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value   = " & lclsPolicy.nProduct & " ;")
                Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
                Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.disabled   = false;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.Parameters.Param1.sValue=0;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.Parameters.Param2.sValue=0;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.value   = " & lclsPolicy.nAgency & " ;")
                Response.Write("top.frames['fraHeader'].$('#valAgency').change();")
                Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.disabled   = true;")
                If eAgen.Find(lclsPolicy.nIntermed) Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].valIntermed.value = " & lclsPolicy.nIntermed & " ;")
                    If lclsClient.Find(eAgen.sClient) Then
                        Response.Write("top.frames['fraHeader'].UpdateDiv('valIntermedDesc','" & lclsClient.sCliename & "','');")
                    End If
                End If
                If lclsClient.Find(lclsPolicy.sClient) Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value   ='" & lclsPolicy.sClient & "';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value ='" & lclsClient.sDigit & "';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','" & lclsClient.sCliename & "','');")
                End If
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & lclsPolicy.dstartdate & "';")

                Call lclsCertificat.Find(Request.QueryString.Item("sCertype"), lclsPolicy.nBranch, lclsPolicy.nProduct, mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0)
                If lclsCertificat.nStatquota <> 1 Then
                    Response.Write("alert('La Propuesta no se encuentra pendiente');")
                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value   ='' ;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled   = true;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value    ='' ;")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','','');")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value ='' ;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valIntermed.value ='' ;")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valIntermedDesc','','');")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value   ='';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value ='';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','','');")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.value   ='' ;")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valAgencyDesc','','');")
                End If
                If lclsCertificat.nWait_code = 1 Then
                    Response.Write("alert('La Propuesta se encuentra con falta de información');")
                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value   ='' ;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled   = true;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value    ='' ;")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','','');")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value ='' ;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valIntermed.value ='' ;")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valIntermedDesc','','');")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value   ='';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value ='';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','','');")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.value   ='' ;")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valAgencyDesc','','');")
                End If
            Else
                Response.Write("alert('Transacción permitida solo para propuestas de suscripción');")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value   ='' ;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled   = true;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value    ='' ;")
                Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','','');")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value ='' ;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valIntermed.value ='' ;")
                Response.Write("top.frames['fraHeader'].UpdateDiv('valIntermedDesc','','');")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value   ='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value ='';")
                Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','','');")
                Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.value   ='' ;")
                Response.Write("top.frames['fraHeader'].UpdateDiv('valAgencyDesc','','');")
            End If
        Else
            Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value   ='' ;")
            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled   = true;")
            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value    ='' ;")
            Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','','');")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value ='' ;")
            Response.Write("top.frames['fraHeader'].document.forms[0].valIntermed.value ='' ;")
            Response.Write("top.frames['fraHeader'].UpdateDiv('valIntermedDesc','','');")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value   ='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value ='';")
            Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','','');")
            Response.Write("top.frames['fraHeader'].document.forms[0].valAgency.value   ='' ;")
            Response.Write("top.frames['fraHeader'].UpdateDiv('valAgencyDesc','','');")
        End If

        lclsPolicy = Nothing
        lclsClient = Nothing
        eAgen = Nothing
        lclsCertificat = Nothing
    End Sub
    ' CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 CA789 
    '------------------------------------------------------------------------------------------------------


    '% insShowPolicy: se muestran los datos asociados al número de póliza.
    '%                Se utiliza para el campo Póliza de la página CA001_K.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowPolicyCA888()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsClient As eClient.Client
        Dim eAgen As eAgent.Intermedia
        'Dim lclsAgencies As Object
        Dim lclsCertificat As ePolicy.Certificat
        lclsCertificat = New ePolicy.Certificat
        lclsPolicy = New ePolicy.Policy
        lclsClient = New eClient.Client
        eAgen = New eAgent.Intermedia
        Dim lclsSecurity As eSecurity.User

        lclsSecurity = New eSecurity.User

        If Not IsNothing(Request.QueryString.Item("nPolicy")) Then
            If lclsPolicy.FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then

                '+Asignación del campo Oficina
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value   = " & lclsPolicy.nBranch & " ;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled   = false;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue= " & lclsPolicy.nBranch & " ;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value   = " & lclsPolicy.nProduct & " ;")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value   = 0;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valUsers.value   = " & lclsPolicy.nUsercode & " ;")
                If lclsSecurity.Find(lclsPolicy.nUsercode) Then
                    Response.Write("top.frames['fraHeader'].UpdateDiv(""valUsersDesc"",'" & lclsSecurity.sCliename & "','Normal');")
                End If
            Else
                'Call lclsCertificat.Find(Request.QueryString.Item("sCertype"), lclsPolicy.nBranch, lclsPolicy.nProduct, mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0)
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.disabled   = true;")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value   = " & lclsPolicy.nBranch & " ;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled   = true;")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue= " & lclsPolicy.nBranch & " ;")
                Response.Write("if(top.frames['fraHeader'].document.forms[0].valProduct.value   = -32768);")
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value   ='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value   = 0;")
                Response.Write("if(top.frames['fraHeader'].document.forms[0].valUsers.value   = '');")
                Response.Write("top.frames['fraHeader'].document.forms[0].valUsers.value   ='';")
            End If
        Else
            Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value   ='' ;")
            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled   = true;")
            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value    ='' ;")
            Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','','');")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value ='' ;")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value   = '';")
            Response.Write("top.frames['fraHeader'].document.forms[0].valUsers.value   = '';")
        End If

        lclsPolicy = Nothing
        lclsClient = Nothing
        eAgen = Nothing
        lclsCertificat = Nothing
        lclsSecurity = Nothing
    End Sub
    '--------------------------------------------------------------------------------------------

    '% insShowPolicy: se muestran los datos asociados al número de póliza.
    '%                Se utiliza para el campo Póliza de la página CA001_K.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowPolicy()
        Dim lclsOptSystem As Object
        Dim lclsProcess As Object
        Dim llngCodeProce As Byte
        'dim eRemoteDB.Constants.intNull As Integer
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        lclsPolicy = New ePolicy.Policy
        If lclsPolicy.Find(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then

            '+Asignación del campo Oficina
            Response.Write("opener.document.forms[0].txtOffice=" & lclsPolicy.nOffice & ";")

            '+Asignación de la Compañía de seguros

            If lclsOptSystem.sTypeCompany = eClient.Client.eType.cstrBrokerOrBrokerageFirm And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPropQuotConvertion Then
                If lclsPolicy.nCompany = eRemoteDB.Constants.intNull Then
                    Response.Write("opener.document.forms[0].valInsuranceCompany.value="""";")
                Else
                    Response.Write("opener.document.forms[0].valInsuranceCompany.value=" & lclsPolicy.nCompany & ";")
                End If
                If lclsPolicy.sOriginal = CStr(eRemoteDB.Constants.strnull) Then
                    If Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngQuotationConvertion Then
                        Response.Write("opener.document.forms[0].tctOriginalPolicy.value="""";")
                    End If
                Else
                    '+ En caso de que sea conversión de cotización a póliza el valor de la póliza original,
                    '+ no se toma de la base de datos porque no tiene valor y en tal caso la blancaría.
                    If Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngQuotationConvertion Then
                        Response.Write("opener.document.forms[0].tctOriginalPolicy.value=" & lclsPolicy.sOriginal & ";")
                    End If
                End If

                Response.Write("opener.document.forms[0].valOriginalOffice.value=" & lclsPolicy.nOfficeIns & ";")
            End If

            '+Asignación del Tipo de negocio
            If lclsPolicy.sBussityp = CStr(eRemoteDB.Constants.strnull) Then
                Response.Write("opener.document.forms[0].optBussines[0].checked=true;")
                Response.Write("opener.document.forms[0].optBussines[0].checked=false;")
                Response.Write("opener.document.forms[0].optBussines[0].checked=false;")
            Else
                Select Case lclsPolicy.sBussityp
                    Case "1"
                        Response.Write("opener.document.forms[0].optBussines[0].checked=true;")
                    Case "2"
                        Response.Write("opener.document.forms[0].optBussines[1].checked=true;")
                    Case "3"
                        Response.Write("opener.document.forms[0].optBussines[2].checked=true;")
                End Select
            End If

            '+Asignación del Tipo de póliza
            If lclsPolicy.sPolitype = vbNullString Then
                Response.Write("opener.document.forms[0].optType[0].checked=true;")
                Response.Write("opener.document.forms[0].optType[1].checked=false;")
                Response.Write("opener.document.forms[0].optType[2].checked=false;")
                Response.Write("opener.document.forms[0].tcnCertificat.disabled=true;")
            Else
                Select Case lclsPolicy.sPolitype
                    Case "1"
                        Response.Write("opener.document.forms[0].optType[0].checked=true;")
                        Response.Write("opener.document.forms[0].tcnCertificat.disabled=true;")
                    Case "2"
                        Response.Write("opener.document.forms[0].optType[1].checked=true;")
                        If Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyIssue And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyQuotation And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyProposal And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyQuery And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyAmendment And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngTempPolicyAmendment Then
                            Response.Write("opener.document.forms[0].tcnCertificat.disabled=false;")
                        End If
                    Case "3"
                        Response.Write("opener.document.forms[0].optType[2].checked=true;")
                        If Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyIssue And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyQuotation And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyProposal And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyQuery And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyAmendment And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngTempPolicyAmendment Then
                            Response.Write("opener.document.forms[0].tcnCertificat.disabled=false;")
                        End If
                End Select
            End If

            '+Asignación del campo Fecha de contabilización
            If CDbl(Request.QueryString.Item("nTransaction")) = 2 Then
                Response.Write("opener.document.forms[0].tcdLedgerDate.value=GetDateSystem();")
            Else
                Response.Write("opener.document.forms[0].tcdLedgerDate.value='" & insreaLedgerDate & "';")
            End If

            '+Asignación del campo Referencia, excluyendo cuando es emisión de certificado.
            If Request.QueryString.Item("nTransaction") <> "2" Then
                If lclsProcess.Find_Policy(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), llngCodeProce, 1) Then
                    With Response
                        .Write("")
                        .Write("if((opener.document.forms[0].tcnReference.value==0)||")
                        .Write("   (opener.document.forms[0].tcnReference.value=='')&&")
                        .Write("   (opener.document.forms[0].tcnReference.value!=" & lclsProcess.nReference & "))")
                        .Write("    opener.document.forms[0].tcnReference.value=0" & lclsProcess.nReference)
                        .Write(";")
                    End With
                Else
                    If Request.QueryString.Item("nTransaction") = "8" Or Request.QueryString.Item("nTransaction") = "9" Or Request.QueryString.Item("nTransaction") = "10" Or Request.QueryString.Item("nTransaction") = "11" Then
                        If llngCodeProce = 4 Then
                            llngCodeProce = 6
                        Else
                            llngCodeProce = 4
                        End If
                        If lclsProcess.Find_Policy(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), llngCodeProce, 1) Then
                            With Response
                                .Write("")
                                .Write("if((opener.document.forms[0].tcnReference.value==0)||")
                                .Write("   (opener.document.forms[0].tcnReference.value=='')&&")
                                .Write("   (opener.document.forms[0].tcnReference.value!=" & lclsProcess.nReference & "))")
                                .Write("    opener.document.forms[0].tcnReference.value=0" & lclsProcess.nReference)
                                .Write(";")
                            End With
                        End If
                    End If
                End If
            End If

            With Response
                .Write("")
                If lclsPolicy.sNumForm = CStr(eRemoteDB.Constants.strnull) Then
                    .Write("opener.document.forms[0].tctRequest_nu.value='';")
                    .Write("opener.document.forms[0].tctRequest_nu.disabled=true;")
                Else
                    .Write("opener.document.forms[0].tctRequest_nu.value=" & lclsPolicy.sNumForm)
                End If
                .Write(";")
            End With
        End If
        lclsPolicy = Nothing
    End Sub

    '% insShowAuto_Regist: Se muestran los datos asociados al auto seleccionado,
    '%                       si el número de placa ya está registrado en el sistema
    '%                       Se utiliza en el campo Matrícula de la ventana AU001.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowAuto_Regist()
        Dim C_MESSAGE_55983 As String = New eGeneral.GeneralFunction().insLoadMessage(55983)
        '--------------------------------------------------------------------------------------------
        Dim lclsAuto As ePolicy.Automobile
        Dim lclsAuto_db As ePolicy.Auto_db
        Dim lclsValpolicyseq As ePolicy.ValPolicySeq
        Dim blnCalDigit As Boolean
        Dim sLicense_ty_old As String
        Dim sRegist_old As String
        Dim lclsPolicyWin As Object

        lclsValpolicyseq = New ePolicy.ValPolicySeq
        lclsAuto = New ePolicy.Automobile
        lclsAuto_db = New ePolicy.Auto_db

        blnCalDigit = True

        If Request.QueryString.Item("Slicense_ty") = "1" And Not String.IsNullOrEmpty(Request.QueryString.Item("sRegist")) Then
            If lclsAuto.InsCalDigitSerie(Request.QueryString.Item("sRegist")) Then
                Response.Write("    if (top.frames['fraHeader'].document.forms[0].tctDigit.value!='' && top.frames['fraHeader'].document.forms[0].tctDigit.value!='" & Trim(lclsAuto.sDigit) & "')" + vbCrLf)
                Response.Write("    {" + vbCrLf)
                Response.Write("        alert('El dígito verificador no es correcto');" + vbCrLf)
                Response.Write("        top.frames['fraHeader'].document.forms[0].tctMistakenDigit.value=top.frames['fraHeader'].document.forms[0].tctDigit.value;")
                Response.Write("        top.frames['fraHeader'].document.forms[0].tctDigit.value=""" & Trim(lclsAuto.sDigit) & """;")
                Response.Write("    }" + vbCrLf)
                Response.Write("    else" + vbCrLf)
                Response.Write("    {" + vbCrLf)
                Response.Write("        top.frames['fraHeader'].document.forms[0].tctMistakenDigit.value='';")
                Response.Write("    }" + vbCrLf)

            Else
                Response.Write("top.frames['fraHeader'].document.forms[0].tctDigit.value='" & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tctRegist.value='" & "';")

                Response.Write("alert(""Err 55983: " & C_MESSAGE_55983  & """);")
                blnCalDigit = False
            End If
        End If

        If blnCalDigit Then
            Call lclsAuto.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
            sLicense_ty_old = lclsAuto.sLicense_ty
            sRegist_old = lclsAuto.sRegist
            If sLicense_ty_old = vbNullString And sRegist_old = vbNullString Then
                'If lclsAuto_db.Find_db1(Request.QueryString.Item("Slicense_ty"), Request.QueryString.Item("sRegist"), True) Then
                If lclsAuto_db.Find_db1("1", Request.QueryString.Item("sRegist"), True) Then
                    If lclsAuto.Find_Tab_au_veh(lclsAuto_db.sVehcode) Then
                        With Response
                            'If lclsAuto_db.sMotor <> "" Then
                            '	.Write("top.frames['fraHeader'].document.forms[0].tctMotor.disabled=true;")
                            'End If
                            'If lclsAuto_db.sChassis <> "" Then
                            '	.Write("top.frames['fraHeader'].document.forms[0].tctChassis.disabled=true;")
                            'End If
                            .Write("top.frames['fraHeader'].document.forms[0].tctDigit.value =""" & Trim(lclsAuto.sDigit) & """;")
                            .Write("top.frames['fraHeader'].document.forms[0].tctMotor.value=""" & lclsAuto_db.sMotor & """;")
                            .Write("top.frames['fraHeader'].document.forms[0].tctChassis.value=""" & lclsAuto_db.sChassis & """;")
                            .Write("top.frames['fraHeader'].document.forms[0].tctColor.value=""" & lclsAuto_db.sColor & """;")
                            '.Write("top.frames['fraHeader'].document.forms[0].valVehcode.value=""" & lclsAuto_db.sVehcode & """;")
                            '.Write("top.frames['fraHeader'].UpdateDiv(""valVehcodeDesc"",'" & Trim(lclsAuto.sDesbrand) & "/" & Trim(lclsAuto.sVehmodel1) & "','Normal');")
                            .Write("top.frames['fraHeader'].document.forms[0].ValVehMark.value=" & lclsAuto.nVehBrand & ";")
                            .Write("top.frames['fraHeader'].document.forms[0].ValVehModel.value='" & lclsAuto_db.sVehcode & "';")
                            .Write("top.frames['fraHeader'].UpdateDiv(""ValVehModelDesc"",'" & lclsAuto.sVehmodel1 & "','Normal');")
                            '.Write("top.frames['fraHeader'].UpdateDiv(""lblType"",'" & lclsAuto.sDesTypeVeh & "','Normal');")
                            '.Write("top.frames['fraHeader'].document.forms[0].tcnType.value=" & lclsAuto.nVehType & ";")
                            '.Write("top.frames['fraHeader'].document.forms[0].tcnVehPlace.value=" & lclsAuto.nVehplace & ";")
                            '.Write("top.frames['fraHeader'].document.forms[0].tcnVehPma.value=" & lclsAuto.nVehpma & ";")
                            '.Write("top.frames['fraHeader'].document.forms[0].tcnCapital.value='" & mclsValues.TypeToString(lclsAuto.nCapital, eFunctions.Values.eTypeData.etdDouble) & "';")
                            .Write("top.frames['fraHeader'].document.forms[0].tcnYear.value='" & mclsValues.TypeToString(lclsAuto_db.nYear, eFunctions.Values.eTypeData.etdDouble) & "';")
                        End With
                    Else
                        With Response
                            'If lclsAuto_db.sMotor <> "" Then
                            '	.Write("top.frames['fraHeader'].document.forms[0].tctMotor.disabled=true;")
                            'End If
                            'If lclsAuto_db.sChassis <> "" Then
                            '		.Write("top.frames['fraHeader'].document.forms[0].tctChassis.disabled=true;")
                            '	End If

                            .Write("top.frames['fraHeader'].document.forms[0].tctMotor.value=""" & lclsAuto_db.sMotor & """;")
                            .Write("top.frames['fraHeader'].document.forms[0].tctChassis.value=""" & lclsAuto_db.sChassis & """;")
                            .Write("top.frames['fraHeader'].document.forms[0].tctColor.value=""" & lclsAuto_db.sColor & """;")
                            '.Write("top.frames['fraHeader'].document.forms[0].valVehcode.value=""" & lclsAuto_db.sVehcode & """;")
                            .Write("top.frames['fraHeader'].UpdateDiv(""lblVehMark"",'" & lclsAuto_db.sVehBrand & "','Normal');")
                            .Write("top.frames['fraHeader'].UpdateDiv(""lblVehModel"",'" & lclsAuto_db.sVehModel & "','Normal');")
                            '.Write("top.frames['fraHeader'].UpdateDiv(""lblType"",'" & lclsAuto_db.sVehType & "','Normal');")
                            .Write("top.frames['fraHeader'].document.forms[0].tcnYear.value='" & mclsValues.TypeToString(lclsAuto_db.nYear, eFunctions.Values.eTypeData.etdDouble) & "';")
                        End With
                    End If
                Else
                    '                    Dim actual As InMotionGIT.LineOfBusiness.Entity.Contracts.AutomobileLineOfBusiness = (New InMotionGIT.Chile.Services.Manager).AACHQueryVTVehicleInformation(Request.QueryString.Item("sRegist"), String.Empty)
                    '                    If Not IsNothing(actual) Then
                    '
                    '                        With Response
                    '                            .Write("top.frames['fraHeader'].document.forms[0].tctDigit.value =""" & Trim(lclsAuto.sDigit) & """;")
                    '                            .Write("top.frames['fraHeader'].document.forms[0].tctMotor.value=""" & actual.EngineSerialNumber & """;")
                    '                            .Write("top.frames['fraHeader'].document.forms[0].tctChassis.value=""" & actual.Chassis & """;")
                    '                            .Write("top.frames['fraHeader'].document.forms[0].tctColor.value=""" & actual.Color & """;")
                    '                            .Write("top.frames['fraHeader'].document.forms[0].ValVehMark.value=""" & actual.VehiclesInAuto.Make & """;")
                    '                            .Write("top.frames['fraHeader'].document.forms[0].ValVehModel.value=""" & actual.VehicleCode & """;")
                    '                            .Write("top.frames['fraHeader'].$('#ValVehModel').change();")
                    '                            .Write("top.frames['fraHeader'].document.forms[0].tctType.value=""" & actual.NVEHGROUP & """;")
                    '                            .Write("top.frames['fraHeader'].document.forms[0].tctColor.value=""" & actual.Color & """;")
                    '                            .Write("top.frames['fraHeader'].document.forms[0].tcnYear.value='" & actual.YearOfManufactured & "';")
                    '                        End With
                    '                    Else
                    With Response
                        .Write("top.frames['fraHeader'].document.forms[0].tctMotor.value=""" & "" & """;")
                        .Write("top.frames['fraHeader'].document.forms[0].tctChassis.value=""" & "" & """;")
                        .Write("top.frames['fraHeader'].document.forms[0].tctColor.value=""" & "" & """;")
                        '.Write("top.frames['fraHeader'].document.forms[0].valVehcode.value=""" & "" & """;")
                        '.Write("top.frames['fraHeader'].UpdateDiv('valVehcodeDesc','','popup');")
                        .Write("top.frames['fraHeader'].document.forms[0].ValVehMark.value=""" & "" & """;")
                        .Write("top.frames['fraHeader'].document.forms[0].ValVehModel.value=""" & "" & """;")
                        .Write("top.frames['fraHeader'].UpdateDiv('ValVehModelDesc','','popup');")
                        '.Write("top.frames['fraHeader'].UpdateDiv('lblType','','popup');")
                        '.Write("top.frames['fraHeader'].document.forms[0].tcnType.value=""" & "" & """;")
                        '.Write("top.frames['fraHeader'].document.forms[0].tcnVehPlace.value=""" & "" & """;")
                        '.Write("top.frames['fraHeader'].document.forms[0].tcnVehPma.value=""" & "" & """;")
                        '.Write("top.frames['fraHeader'].document.forms[0].tcnCapital.value=""" & "" & """;")
                        .Write("top.frames['fraHeader'].document.forms[0].tcnYear.value=""" & "" & """;")
                    End With
                    '                   End If
                End If
            Else

                With Response
                    '.Write("top.frames['fraHeader'].document.forms[0].tctMotor.disabled=false;")
                    '.Write("top.frames['fraHeader'].document.forms[0].tctChassis.disabled=false;")
                End With


            End If

        End If

        lclsAuto = Nothing
        lclsAuto_db = Nothing
        lclsValpolicyseq = Nothing
    End Sub



    '% insShowAuto_Digit: Se muestran los datos asociados al auto seleccionado,
    '%                       si el número de placa ya está registrado en el sistema
    '%                       Se utiliza en el campo Matrícula de la ventana AU001.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowAuto_Digit()
        Dim C_MESSAGE_55983 As String = New eGeneral.GeneralFunction().insLoadMessage(55983)
        '--------------------------------------------------------------------------------------------
        Dim lclsAuto As ePolicy.Automobile
        Dim lclsAuto_db As ePolicy.Auto_db
        Dim lclsValpolicyseq As ePolicy.ValPolicySeq
        Dim blnCalDigit As Boolean
        Dim sLicense_ty_old As String
        Dim sRegist_old As String
        Dim lclsPolicyWin As Object
        lclsValpolicyseq = New ePolicy.ValPolicySeq
        lclsAuto = New ePolicy.Automobile
        lclsAuto_db = New ePolicy.Auto_db

        blnCalDigit = True

        If Request.QueryString.Item("Slicense_ty") = "1" And Not String.IsNullOrEmpty(Request.QueryString.Item("sRegist")) Then
            If lclsAuto.InsCalDigitSerie(Request.QueryString.Item("sRegist")) Then
                Response.Write("    if (top.frames['fraHeader'].document.forms[0].tctDigit.value!='' && top.frames['fraHeader'].document.forms[0].tctDigit.value!='" & Trim(lclsAuto.sDigit) & "')" + vbCrLf)
                Response.Write("    {" + vbCrLf)
                Response.Write("        alert('El dígito verificador no es correcto');" + vbCrLf)
                Response.Write("        top.frames['fraHeader'].document.forms[0].tctMistakenDigit.value=top.frames['fraHeader'].document.forms[0].tctDigit.value;")
                Response.Write("        top.frames['fraHeader'].document.forms[0].tctDigit.value='" & Trim(lclsAuto.sDigit) & "';")
                Response.Write("    }" + vbCrLf)
                Response.Write("    else" + vbCrLf)
                Response.Write("    {" + vbCrLf)
                Response.Write("        top.frames['fraHeader'].document.forms[0].tctMistakenDigit.value='';")
                Response.Write("    }" + vbCrLf)

            Else
                Response.Write("top.frames['fraHeader'].document.forms[0].tctDigit.value='" & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tctRegist.value='" & "';")

                Response.Write("alert(""Err 55983: " & C_MESSAGE_55983 & """);")
                blnCalDigit = False
            End If
        End If

        lclsAuto = Nothing
        lclsAuto_db = Nothing
        lclsValpolicyseq = Nothing
    End Sub


    '% insShowCertificat: se muestran los datos asociados al número de certificado
    '%                    Se utiliza para el campo Certificado de la página CA001_K.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowCertificat()
        '--------------------------------------------------------------------------------------------
        Dim lclsCertificat As ePolicy.Certificat

        lclsCertificat = New ePolicy.Certificat
        With lclsCertificat
            If .Find(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then

                If Request.QueryString.Item("sCodispl") = "CA088_K" Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeStatus_pol.value=" & lclsCertificat.sStatusva & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcdDate_origin.value='" & mclsValues.StringToType(CStr(lclsCertificat.dDate_Origi), eFunctions.Values.eTypeData.etdDate) & "';")
                Else
                    '+Se muestra,por defecto, la fecha actual de renovación de la póliza
                    If .dExpirdat <> eRemoteDB.Constants.dtmNull Then
                        Response.Write("opener.document.forms[0].tcdExpirdate.value='" & mclsValues.DateToString(.dExpirdat) & "';")
                    Else
                        Response.Write("opener.document.forms[0].tcdExpirdate.value=" & eRemoteDB.Constants.strnull)
                    End If

                    '+Se muestra,por defecto, la fecha actual de próxima facturación de la póliza
                    If .dNextreceip <> eRemoteDB.Constants.dtmNull Then
                        Response.Write("opener.document.forms[0].tcdNextReceip.value='" & mclsValues.DateToString(.dNextreceip) & "';")
                    Else
                        Response.Write("opener.document.forms[0].tcdNextReceip.value=" & eRemoteDB.Constants.strnull)
                    End If
                End If
            End If
        End With
        lclsCertificat = Nothing
    End Sub
    '% insShowCotProp: se muestran los datos asociados al número de propuesta
    '--------------------------------------------------------------------------------------------
    Sub insShowCotProp()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy_po As ePolicy.Policy
        Dim lclsTConvertions As ePolicy.TConvertions
        Dim lclsPolicy As Object
        Dim llngPolicy As Double
        Dim llngCertif As Double
        Dim lstrPolitype As Object
        Dim ldtmEffecdate As Date
        Dim lstrOrigin As String
        llngPolicy = 0
        llngCertif = 0
        lstrPolitype = 1
        lstrOrigin = Trim(Request.QueryString.Item("valOrigin"))
        lclsTConvertions = New ePolicy.TConvertions
        lclsPolicy_po = New ePolicy.Policy

        '+ se agrego este manejo para el numero unico de propuesta/Cotización
        If lclsPolicy_po.FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
            Response.Write("opener.document.forms[0].cbeBranch.value=" & lclsPolicy_po.nBranch & ";")
            Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy_po.nBranch & ";")
            Response.Write("opener.document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
            Response.Write("opener.document.forms[0].cbeBranch.disabled=true;")
            If lclsPolicy_po.nProduct > 0 Then
                Response.Write("opener.$('#valProduct').change();")
            End If
        Else
            Response.Write("opener.document.forms[0].cbeBranch.value="""";")
            Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue="""";")
            Response.Write("opener.document.forms[0].valProduct.value="""";")
            Response.Write("opener.$('#valProduct').change();")
        End If

        With Request
            '+ Si origen es Anulacion, Emision, Rehabilitacion, Saldado, Prorrogao, Rescate o Prestamo, 
            '+ entonces es una propuesta especial
            If lstrOrigin = "4" Or lstrOrigin = "5" Or lstrOrigin = "6" Or lstrOrigin = "7" Or lstrOrigin = "8" Or lstrOrigin = "9" Or lstrOrigin = "10" Then

                If lclsTConvertions.Find_PropSpecial(mclsValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)) Then
                    llngPolicy = lclsTConvertions.nPolicy
                    llngCertif = lclsTConvertions.nCertif
                    lstrPolitype = lclsTConvertions.sPolitype
                    ldtmEffecdate = lclsTConvertions.dstartdate
                End If
            ElseIf lstrOrigin = "2" Or lstrOrigin = "3" Then
                If lclsTConvertions.Find_Prop_ren(Request.QueryString.Item("sCertype"), mclsValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)) Then
                    llngPolicy = lclsTConvertions.nPolicy
                    llngCertif = lclsTConvertions.nCertif
                    lstrPolitype = lclsTConvertions.sPolitype
                    ldtmEffecdate = lclsTConvertions.dstartdate
                End If
            End If
        End With

        With Response
            .Write("with(opener.document.forms[0]){")
            .Write("    tcnProponum.value=" & llngPolicy & ";")
            .Write("    tcnCertif.value=" & llngCertif & ";")
            .Write("    tcdEffecdate.value='" & mclsValues.TypeToString(ldtmEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
            '+Asignación del Tipo de póliza
            Select Case lstrPolitype
                Case "1"
                    .Write("    tcnCertif.disabled=true;")
                    .Write("    tcnCertif.value=0;")
                Case "2"
                    If lstrOrigin <> "3" Then
                        .Write("    tcnCertif.disabled=false;")
                        .Write("    tcnCertif.value=0;")
                    Else
                        .Write("    tcnCertif.disabled=true;")
                        .Write("    tcnProponum.disabled=true;")
                    End If
                Case "3"
                    .Write("    tcnCertif.disabled=false;")
            End Select
            .Write("}")
        End With
        lclsTConvertions = Nothing
        lclsPolicy_po = Nothing
    End Sub
    '% insShowProduct: se muestran los datos asociados al número de producto
    '%                 Se utiliza para el campo Producto de la página CA001_K.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowProduct()
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct As eProduct.Product
        lclsProduct = New eProduct.Product
        With lclsProduct
            If .Find(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
                If Request.QueryString.Item("nTransaction") = eCollection.Premium.PolTransac.clngPolicyIssue Or Request.QueryString.Item("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotation Or Request.QueryString.Item("nTransaction") = eCollection.Premium.PolTransac.clngPolicyProposal Then
                    '+ Se habilitan/deshabilitan los tipos de póliza permitidos para el producto
                    If .sIndivind = "1" Then
                        Response.Write("opener.document.forms[0].elements[""optType""][0].disabled=false;")
                    Else
                        Response.Write("opener.document.forms[0].elements[""optType""][0].disabled=true;")
                    End If
                    If .sGroupind = "1" Then
                        Response.Write("opener.document.forms[0].elements[""optType""][1].disabled=false;")
                    Else
                        Response.Write("opener.document.forms[0].elements[""optType""][1].disabled=true;")
                    End If
                    If .sMultiind = "1" Then
                        Response.Write("opener.document.forms[0].elements[""optType""][2].disabled=false;")
                    Else
                        Response.Write("opener.document.forms[0].elements[""optType""][2].disabled=true;")
                    End If
                    '+ Se coloca el valor por defecto
                    Select Case .sPolitype
                        Case "1"
                            Response.Write("opener.document.forms[0].elements[""optType""][0].checked = true;")
                        Case "2"
                            Response.Write("opener.document.forms[0].elements[""optType""][1].checked = true;")
                        Case "3"
                            Response.Write("opener.document.forms[0].elements[""optType""][2].checked = true;")
                    End Select
                End If
            End If
        End With
        lclsProduct = Nothing
    End Sub
    '% insShowAuto: se muestran los datos asociados al auto seleccionado
    '%              Se utiliza para el campo Código del vehiculo de la página AU001.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowAuto()
        '--------------------------------------------------------------------------------------------
        Dim lclsAuto As ePolicy.Automobile
        lclsAuto = New ePolicy.Automobile
        If lclsAuto.Find_Tab_au_veh(Request.QueryString.Item("nVehcode")) Then
            With Response
                .Write("with(opener){")
                .Write("    UpdateDiv('lblVehMark','" & lclsAuto.sDesBrand & "','Normal');")
                .Write("    UpdateDiv('lblVehModel','" & lclsAuto.sVehmodel & "','Normal');")
                .Write("    UpdateDiv('lblType','" & lclsAuto.sDesTypeVeh & "','Normal');")
                .Write("    with(document.forms[0]){")
                .Write("        tcnType.value=" & lclsAuto.nVehType & ";")
                .Write("        tcnVehPlace.value=" & lclsAuto.nVehplace & ";")
                .Write("        tcnVehPma.value=" & lclsAuto.nVehpma & ";")
                If lclsAuto.Find_Tab_au_val(Request.QueryString.Item("nVehcode"), mclsValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble)) Then
                    .Write("    tcnCapital.value=" & lclsAuto.nCapital & ";")
                End If
                .Write("}}")
            End With
        End If
        lclsAuto = Nothing
    End Sub

    '% insShowIntermed: se muestran los datos asociados al intermediario
    '%                    Se utiliza para el campo Código de la página CA024Upd.aspx
    '--------------------------------------------------------------------------------------------
    Function insShowIntermed() As Object
        '--------------------------------------------------------------------------------------------
        Dim llngIntermed As Integer
        Dim lclsDet_comgen As ePolicy.Det_comgen
        Dim lclsIntermedia As eAgent.Intermedia

        lclsDet_comgen = New ePolicy.Det_comgen
        lclsIntermedia = New eAgent.Intermedia

        llngIntermed = mclsValues.StringToType(Request.QueryString.Item("nCodeIntermed"), eFunctions.Values.eTypeData.etdDouble)
        Response.Write("opener.document.forms[0].nShare.disabled=false;")
        Response.Write("opener.document.forms[0].nPercent.disabled=false;")
        Response.Write("opener.document.forms[0].nAmount.disabled=false;")
        '+ Se asignan los valores dependiendo de los datos del intermediario
        If lclsIntermedia.Find(llngIntermed) Then
            Response.Write("opener.document.forms[0].nRole.value=" & lclsIntermedia.nIntertyp & ";")
            Response.Write("opener.UpdateDiv(""sCliename"",""" & lclsIntermedia.sCliename & """,""Normal"");")

            Select Case Request.QueryString.Item("sTypeComm")
                Case "Table"
                    Response.Write("opener.document.forms[0].sType.value=" & ePolicy.Commission.TypeOfIntermediaryCommissionsAccordingToTable & ";")
                    If lclsDet_comgen.Find(lclsIntermedia.nComtabge, mclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), 0) Then
                        If lclsDet_comgen.nRate_first = eRemotedb.Constants.dblNull Then
                            Response.Write("opener.document.forms[0].nPercent.value=0;")
                        Else
                            Response.Write("opener.document.forms[0].nPercent.value=" & lclsDet_comgen.nRate_first & ";")
                        End If
                    Else
                        Response.Write("opener.document.forms[0].nPercent.value="""";")
                    End If
                    Response.Write("opener.document.forms[0].nAmount.value="""";")

                Case "Fix"
                    Response.Write("opener.document.forms[0].sType.value=" & ePolicy.Commission.TypeOfIntermediaryCommissionsFix & ";")
                    Response.Write("opener.document.forms[0].nPercent.value=opener.document.forms[0].nPercent.value;")
                    Response.Write("opener.document.forms[0].nAmount.value="""";")

                Case "WithOut"
                    Response.Write("opener.document.forms[0].sType.value=" & ePolicy.Commission.TypeOfIntermediaryCommissionsNoCommission & ";")
                    Response.Write("opener.document.forms[0].nPercent.value="""";")
                    Response.Write("opener.document.forms[0].nPercent.disabled=true;")
                    Response.Write("opener.document.forms[0].nAmount.value="""";")
            End Select

            If lclsIntermedia.sParticin = "1" Then
                Response.Write("opener.document.forms[0].nPercent.disabled=true;")
                Response.Write("opener.document.forms[0].nAmount.disabled=true;")
            Else
                Response.Write("opener.document.forms[0].nShare.value=0;")
                Response.Write("opener.document.forms[0].nShare.disabled=true;")
                Response.Write("opener.document.forms[0].sType.value=" & ePolicy.Commission.TypeOfIntermediaryCommissionsNoCommission & ";")
                Response.Write("opener.document.forms[0].nPercent.value="""";")
            End If

            If lclsIntermedia.nSupervis <> 0 Then
                If lclsIntermedia.sCol_Agree = "1" Then
                    Response.Write("opener.opener.document.forms[0].chkConColl.checked=true;")
                End If
            End If

            Response.Write("" & vbCrLf)
            Response.Write("" & vbCrLf)
            Response.Write("//+ Se bloquea el campo % si el tipo de comisión es <> de comisión fija" & vbCrLf)
            Response.Write("    if(opener.document.forms[0].sType.value!=""2"" &&" & vbCrLf)
            Response.Write("       opener.document.forms[0].nRole.value!=20){  " & vbCrLf)
            Response.Write("        opener.document.forms[0].nPercent.disabled=true;" & vbCrLf)
            Response.Write("    }" & vbCrLf)
            Response.Write("" & vbCrLf)
            Response.Write("//+ Se bloquea el campo Importe si el tipo de comisión es <> de comisión fija, y participa en las comisiones" & vbCrLf)
            Response.Write("    if(opener.document.forms[0].sType.value!=""2"" &&" & vbCrLf)
            Response.Write("       opener.document.forms[0].nRole.value!=20 &&" & vbCrLf)
            Response.Write("       (sParticin==""1"" ||" & vbCrLf)
            Response.Write("        sParticin=="""")){" & vbCrLf)
            Response.Write("        opener.document.forms[0].nAmount.disabled=true;" & vbCrLf)
            Response.Write("    }        " & vbCrLf)
            Response.Write("        " & vbCrLf)
            Response.Write("    if(opener.document.forms[0].sType.value==""2"" &&" & vbCrLf)
            Response.Write("       sParticin!=""1""){" & vbCrLf)
            Response.Write("        opener.document.forms[0].nAmount.value="""";" & vbCrLf)
            Response.Write("        opener.document.forms[0].nPercent.value="""";" & vbCrLf)
            Response.Write("    }")


        End If
        lclsDet_comgen = Nothing
        lclsIntermedia = Nothing
    End Function

    '% insreaLedgerDate: busca la fecha de contabilización del recibo
    '--------------------------------------------------------------------------------------------
    Function insreaLedgerDate() As String
        '--------------------------------------------------------------------------------------------
        Dim lclsPremium As eCollection.Premium
        Dim lclsPremium_mo As eCollection.Premium_mo

        lclsPremium = New eCollection.Premium
        lclsPremium_mo = New eCollection.Premium_mo

        insreaLedgerDate = mclsValues.DateToString(Today)
        With lclsPremium
            .sCertype = Request.QueryString.Item("sCertype")
            .nBranch = mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
            .nProduct = mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
            .nPolicy = mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
            If .Find_Receipt Then
                If .nReceipt > 0 Then
                    If lclsPremium_mo.Find_dPosted(.nReceipt) Then
                        If lclsPremium_mo.dPosted = eRemoteDB.Constants.dtmNull Then
                            insreaLedgerDate = mclsValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
                        Else
                            insreaLedgerDate = mclsValues.TypeToString(lclsPremium_mo.dPosted, eFunctions.Values.eTypeData.etdDate)
                        End If
                    End If
                End If
            End If
        End With
        lclsPremium = Nothing
        lclsPremium_mo = Nothing
    End Function

    '% insShowData: Se muestran los datos asociados al número de producto
    '%              Se utiliza para el campo Producto de la página VI011_K.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowData()
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct_li As eProduct.Product

        lclsProduct_li = New eProduct.Product
        With lclsProduct_li
            If .FindProduct_li(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
                With Response
                    .Write("opener.UpdateDiv(""lblDesCurrency"",'" & mclsValues.getMessage(lclsProduct_li.nCurrency, "Table11") & "','Normal');")
                    .Write("opener.document.forms[0].tcnCurrency.value='" & lclsProduct_li.nCurrency & "';")
                End With
            End If
        End With
        lclsProduct_li = Nothing
    End Sub

    '% insShowData_loans: Se muestran los datos asociados al número de producto
    '%                    Se utiliza para el campo Producto de la página VI011_K.aspx
    '--------------------------------------------------------------------------------------------
    Sub insShowData_loans()
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct_li As eProduct.Product

        lclsProduct_li = New eProduct.Product
        With lclsProduct_li
            If .FindProduct_li(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

                With Response
                    .Write("top.frames['fraFolder'].document.forms[0].hddTaxes.value='" & lclsProduct_li.nTaxes & "';")
                    .Write("top.frames['fraFolder'].ShowChangeAmount();")
                End With
            End If
        End With
        lclsProduct_li = Nothing
    End Sub

    '% insShowPolicyNum: Obtiene los datos particulares para la transacción CA031
    '% para ser actualizados luego sobre la página
    '--------------------------------------------------------------------------------------------
    Private Sub insShowPolicyNum()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsIntermed As eAgent.Intermedia
        Dim lclsClient As eClient.Client
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim nNumError As Integer
        Dim lstrMessage As String

        lclsGeneral = New eGeneral.GeneralFunction
        lclsPolicy = New ePolicy.Policy
        lclsIntermed = New eAgent.Intermedia
        lclsClient = New eClient.Client

        '+ Se realiza la lectura de los datos de la póliza

        nNumError = lclsPolicy.insValPolicy(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), CDbl(Request.QueryString.Item("nPolicy")), Session("sTypeCompanyUser"))

        If nNumError = 0 Then
            '+ Se actualizan los datos obtenidos sobre los campos de la página

            With Response
                .Write("with(top.frames['fraFolder'].document.forms[0]){")
                .Write("    tcnCertif.value='" & lclsPolicy.nCertif & "';")
                .Write("    tctRenewal.value='" & mclsValues.TypeToString(lclsPolicy.dNextreceip, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("    tctStartDat.value='" & mclsValues.TypeToString(lclsPolicy.dstartdate, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("    tctExpirdat.value='" & mclsValues.TypeToString(lclsPolicy.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("    tctClientname.value='" & lclsPolicy.sCliename & "';")
                If lclsPolicy.sPolitype = "2" Then
                    .Write("    tcnCertif.disabled=false;")
                End If
                If lclsPolicy.sColtimre = "1" Then
                    .Write("    tcnCertif.disabled=true;")
                End If
                If lclsIntermed.Find(lclsPolicy.nIntermed) Then
                    If lclsClient.Find(lclsIntermed.sClient) Then
                        .Write("tctIntername.value='" & lclsClient.sCliename & "';")
                        .Write("hddIntermed.value='" & lclsPolicy.nIntermed & "';")

                    End If
                End If
                .Write("}")
            End With
        Else
            With Response
                .Write("with(top.frames['fraFolder'].document.forms[0]){")
                .Write("    tcnCertif.value='" & " " & "';")
                .Write("    tctRenewal.value='" & " " & "';")
                .Write("    tctStartDat.value='" & " " & "';")
                .Write("    tctExpirdat.value='" & " " & "';")
                .Write("    tctClientname.value='" & " " & "';")
                .Write("tctIntername.value='" & " " & "';")
                .Write("hddIntermed.value='" & " " & "';")
                .Write("}")
            End With

            '+ Debe incluir el número de la póliza
            If nNumError = -2 Then
                lstrMessage = lclsGeneral.insLoadMessage(3003)
                Response.Write("alert(""Err 3003:  " & lstrMessage & """);")
            End If

            '+ Número de póliza no está registrado en el sistema
            If nNumError = -1 Then
                lstrMessage = lclsGeneral.insLoadMessage(3001)
                Response.Write("alert(""Err 3001:  " & lstrMessage & """);")
            End If

            '+ La póliza se encuentra anulada
            If nNumError = 1 Then
                lstrMessage = lclsGeneral.insLoadMessage(3098)
                Response.Write("alert(""Err 3098:  " & lstrMessage & """);")
            End If

            '+ La póliza no tiene estado válido.
            If nNumError = 2 Then
                lstrMessage = lclsGeneral.insLoadMessage(3882)
                Response.Write("alert(""Err 3882:  " & lstrMessage & """);")
            End If

        End If

        lclsIntermed = Nothing
        lclsPolicy = Nothing
        lclsGeneral = Nothing
    End Sub

    '% insShowReceipt: Se muestra el número de Recibo para la forma CA028
    '--------------------------------------------------------------------------------------------
    Sub insShowReceipt()
        '--------------------------------------------------------------------------------------------
        Dim lclsGeneral As eGeneral.GeneralFunction
        If Request.QueryString.Item("nReceipt") = vbNullString Then
            lclsGeneral = New eGeneral.GeneralFunction
            Response.Write("top.frames[""fraFolder""].document.forms[0].tcnReceipt.value=" & lclsGeneral.Find_Numerator(4, 0, Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), 0, 0) & ";")
            lclsGeneral = Nothing
        End If
    End Sub

    '% insShowAdjReceipt: Se muestran las fechas del recibo a ajustar
    '--------------------------------------------------------------------------------------------
    Sub insShowAdjReceipt()
        '--------------------------------------------------------------------------------------------
        Dim lclsAdjPremium As eCollection.Premium

        lclsAdjPremium = New eCollection.Premium
        With lclsAdjPremium
            If .Find("2", CDbl(Request.QueryString.Item("nAdjReceipt")), Session("nBranch"), Session("nProduct"), 0, 0) Then
                Response.Write("with(top.frames[""fraFolder""].document.forms[0]){" & "  cbeCurrency.value='" & .nCurrency & "';" & "  tcdStartDateR.value='" & mclsValues.TypeToString(.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';" & "  tcdExpirDateR.value='" & mclsValues.TypeToString(.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';" & "  cbeCurrency.disabled   = true;" & "  tcdStartDateR.disabled = true;" & "  tcdExpirDateR.disabled = true;" & "  tcnPremiumOri.value ='" & mclsValues.TypeToString(.nPremium, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';" & "  tcnBalanceOri.value ='" & mclsValues.TypeToString(.nBalance, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';" & "}")
            Else
                Response.Write("with(top.frames[""fraFolder""].document.forms[0]){" & "  cbeCurrency.value='';" & "  tcdStartDateR.value='';" & "  tcdExpirDateR.value='';" & "  tcdStartDateR.disabled=false;" & "  tcdExpirDateR.disabled=false;" & "  cbeCurrency.disabled=false;" & "  tcnPremiumOri.value ='';" & "  tcnBalanceOri.value ='';" & "}")
            End If
        End With
    End Sub

    '**% insShowPolicyData: Show the information of the policy.
    '% insShowPolicyData: Muestra los datos de la póliza.
    '--------------------------------------------------------------------------------------------
    Private Sub insShowPolicyData()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCurrenPol As ePolicy.Curren_pol
        Dim lstrCurrency As String
        Dim ldtmEffecdate As Date
        Dim lclsOpt_system As eGeneral.Opt_system
        Dim lstrPolicyNum As String
        Dim lblnExist As Boolean
        Dim lstrCertype As String
        Dim lclsProduct As eProduct.Product
        Dim sProduct As String
        Dim lclsBranches As eBranches.Tab_Ord_Origins
        Dim lclsFunds_Switch As eBranches.Funds_Switch

        lclsPolicy = New ePolicy.Policy
        lclsProduct = New eProduct.Product
        lclsBranches = New eBranches.Tab_Ord_Origins
        lclsFunds_Switch = New eBranches.Funds_Switch

        With Request
            If .QueryString.Item("sCodispl") = "CA642" Then
                If lclsPolicy.FindPolicybyPolicy("2", mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
                    If lclsProduct.Find(CInt(.QueryString.Item("nBranch")), lclsPolicy.nProduct, Today) Then
                        sProduct = lclsProduct.sDescript
                    End If
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='" & lclsPolicy.nProduct & "';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','" & sProduct & "','');")
                End If
            ElseIf .QueryString.Item("sCodispl") = "VI010" Then
                If lclsPolicy.FindPolicybyPolicy("2", mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
                    If lclsProduct.Find(lclsPolicy.nBranch, lclsPolicy.nProduct, Today) Then
                        sProduct = lclsProduct.sDescript
                    End If

                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value='" & lclsPolicy.nBranch & "';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='" & lclsPolicy.nProduct & "';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','" & sProduct & "','');")

                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeOrigin.Parameters.Param1.sValue='" & lclsPolicy.nBranch & "';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeOrigin.Parameters.Param2.sValue='" & lclsPolicy.nProduct & "';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeOrigin.Parameters.Param3.sValue='" & lclsPolicy.nPolicy & "';")

                    Response.Write("top.frames['fraHeader'].document.forms[0].cbeOrigin.disabled = false;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].btncbeOrigin.disabled = false;")

                    If .QueryString.Item("dEffecdate") <> vbNullString Then
                        ldtmEffecdate = mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
                    Else
                        ldtmEffecdate = Today
                    End If
                    lclsCurrenPol = New ePolicy.Curren_pol
                    lstrCurrency = lclsCurrenPol.findCurrency("2", lclsPolicy.nBranch, lclsPolicy.nProduct, mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), ldtmEffecdate)
                    If lstrCurrency = "*" Then
                        Response.Write("try{top.fraHeader.document.forms[0].cbeCurrency.value='1'}catch(x){};")
                    Else
                        Response.Write("try{top.fraHeader.document.forms[0].cbeCurrency.value='" & lclsCurrenPol.nCurrency & "'}catch(x){};")
                    End If
                    Response.Write("try{top.fraHeader.UpdateDiv('divCurrency','" & lclsCurrenPol.sDescript & "', '')}catch(x){};")
                    lclsCurrenPol = Nothing

                    If lclsPolicy.sPolitype = "1" Then
                        Response.Write("top.fraHeader.document.forms[0].tcnCertif.value=0;")
                        Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=true;")
                        insShowCertifData()
                    Else
                        Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='';")
                        Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=false;")
                    End If
                End If
            ElseIf .QueryString.Item("sCodispl") = "VI7002" Then

                lclsOpt_system = New eGeneral.Opt_system
                Call lclsOpt_system.Find()
                lstrPolicyNum = lclsOpt_system.sPolicyNum

                lblnExist = False

                If lstrPolicyNum = "1" Then '+Generales
                    If (.QueryString.Item("nPolicy") <> vbNullString And .QueryString.Item("nPolicy") <> "0" And .QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
                        lblnExist = True
                    End If
                Else
                    If lstrPolicyNum = "2" Then '+ Ramo 
                        If (.QueryString.Item("nBranch") <> vbNullString And .QueryString.Item("nBranch") <> "0" And .QueryString.Item("nBranch") <> CStr(eRemoteDB.Constants.intNull) And .QueryString.Item("nPolicy") <> vbNullString And .QueryString.Item("nPolicy") <> "0" And .QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
                            lblnExist = True
                        End If
                    Else
                        If lstrPolicyNum = "3" Then '+ Producto
                            If (.QueryString.Item("nBranch") <> vbNullString And .QueryString.Item("nBranch") <> "0" And .QueryString.Item("nBranch") <> CStr(eRemoteDB.Constants.intNull) And .QueryString.Item("nProduct") <> vbNullString And .QueryString.Item("nProduct") <> "0" And .QueryString.Item("nProduct") <> CStr(eRemoteDB.Constants.intNull) And .QueryString.Item("nPolicy") <> vbNullString And .QueryString.Item("nPolicy") <> "0" And .QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
                                lblnExist = True
                            End If
                        End If
                    End If
                End If

                If lblnExist Then
                    lstrCertype = "2"
                    If lclsPolicy.FindPolicyOptSystem(lstrCertype, mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then

                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy.nBranch & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy.nProduct & ";")
                        If lclsPolicy.nProduct > 0 Then
                            Response.Write("top.frames['fraHeader'].$('#valProduct').change();")

                            If lstrPolicyNum <> "1" Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled=true;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].btnvalProduct.disabled=true;")
                            End If
                        End If

                        If lclsPolicy.sPolitype = "2" Then
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='0';")
                        Else
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='0';")
                        End If
                    End If
                End If

                lclsOpt_system = Nothing

            Else
                If lclsPolicy.FindPolicybyPolicy("2", mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
                    If lclsProduct.Find(lclsPolicy.nBranch, lclsPolicy.nProduct, Today) Then
                        sProduct = lclsProduct.sDescript
                    End If
                End If
                If lclsPolicy.Find("2", mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
                    Session("nProduct") = lclsPolicy.nProduct
                    If lclsPolicy.sPolitype = "1" Then
                        Response.Write("top.fraHeader.document.forms[0].tcnCertif.value=0;")
                        Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=true;")
                        If .QueryString.Item("sCodispl") = "VI7000" Then
                            insShowCertifData()
                        End If
                    Else
                        Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='';")
                        Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=false;")
                    End If

                    If .QueryString.Item("sCodispl") = "VI7000" Then
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy.nBranch & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy.nProduct & ";")
                        Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
                    End If
                    If .QueryString.Item("sCodispl") = "VI818" Then
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy.nBranch & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy.nProduct & ";")
                        Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
                    End If
                Else
                    If .QueryString.Item("sCodispl") = "VI818" Then
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='';")
                        Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','','');")
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
                    End If
                End If
            End If
            If .QueryString.Item("sCodispl") = "VI010" Or .QueryString.Item("sCodispl") = "VI016" Then
                Call lclsFunds_Switch.Find_Origin("2", lclsPolicy.nBranch, lclsPolicy.nProduct, mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), ldtmEffecdate)

                If lclsFunds_Switch.nCount_Origin = 1 Then
                    Response.Write("top.fraHeader.document.forms[0].cbeOrigin.value= '" & lclsFunds_Switch.nOrigin & "';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv('cbeOrigin','" & lclsFunds_Switch.sDescript_Origin & "','');")
                    Response.Write("top.fraHeader.$('#cbeOrigin').change();")
                End If
            End If
        End With
        lclsPolicy = Nothing
        lclsProduct = Nothing
        lclsBranches = Nothing
        lclsFunds_Switch = Nothing
    End Sub

    '**% insShowCertifData: Show the information of the certificate.
    '% insShowCertifData: Muestra los datos del certificado.
    '--------------------------------------------------------------------------------------------
    Private Sub insShowCertifData()
        '--------------------------------------------------------------------------------------------
        Dim lclsCertificat As ePolicy.Certificat
        Dim lstrCurrency As String
        Dim lclsCurrenPol As ePolicy.Curren_pol
        Dim ldblCertif As Double
        Dim ldtmEffecdate As Date

        lclsCertificat = New ePolicy.Certificat
        Dim lclsRoles As ePolicy.Roles
        With Request
            If .QueryString.Item("nCertif") = vbNullString Then
                ldblCertif = 0
            Else
                ldblCertif = mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)
            End If
            If lclsCertificat.Find("2", mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), ldblCertif) Then

                If .QueryString.Item("dEffecdate") <> vbNullString Then
                    ldtmEffecdate = mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
                Else
                    ldtmEffecdate = Today
                End If

                If Request.QueryString.Item("sCod_VI7000") = "VI7000" Then
                    lclsRoles = New ePolicy.Roles
                    If lclsRoles.Find("2", mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 1, "", mclsValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

                        Response.Write("try{top.fraHeader.document.forms[0].hddClientBenef.value='" & lclsRoles.sClient & "'}catch(x){};")
                    Else
                        Response.Write("try{top.fraHeader.document.forms[0].hddClientBenef.value=''}catch(x){};")
                    End If
                    lclsRoles = Nothing
                Else
                    Response.Write("try{top.fraHeader.document.forms[0].hddClientBenef.value='" & lclsCertificat.sClient & "'}catch(x){};")
                End If

                lclsCurrenPol = New ePolicy.Curren_pol
                lstrCurrency = lclsCurrenPol.findCurrency("2", mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), ldtmEffecdate)

                '+Si es multimoneda, se deja moneda local
                If lstrCurrency = "*" Then
                    Response.Write("try{top.fraHeader.document.forms[0].cbeCurrency.value='1'}catch(x){};")
                Else
                    Response.Write("try{top.fraHeader.document.forms[0].cbeCurrency.value='" & lclsCurrenPol.nCurrency & "'}catch(x){};")
                End If
                Response.Write("try{top.fraHeader.UpdateDiv('divCurrency','" & lclsCurrenPol.sDescript & "', '')}catch(x){};")
                lclsCurrenPol = Nothing
            End If
        End With
        lclsCertificat = Nothing
    End Sub

    '**% insSwitch_Amount: Calculates the swith amounts
    '--------------------------------------------------------------------------------------------
    Private Sub insSwitch_Amount()
        '--------------------------------------------------------------------------------------------
        Dim ldblValue As Double
        Dim ldblUnits As Byte
        Dim ldblValue_ini As Byte

        ldblUnits = 0

        '+Calculo según Unidades 	
        If CDbl(Request.QueryString.Item("nInd")) = 1 Then

            '+Se valorizan unidades (unidades x precio de unidad)
            ldblUnits = mclsValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
            ldblValue = ldblUnits * mclsValues.StringToType(Request.QueryString.Item("nUnitVal"), eFunctions.Values.eTypeData.etdDouble)
            '+Se despliega monto equivalente a unidades
            Response.Write("top.frames['fraFolder'].document.forms[0].tcnValueChange.value=VTFormat('" & ldblValue & "', '', '', '', 6, false);")
            '+almacena en monto en unidas 		
            Response.Write("top.frames['fraFolder'].document.forms[0].tcnValueChange_aux.value=VTFormat('" & ldblValue & "', '', '', '', 6, false);")
            '+Calculo según Monto
        Else
            ldblValue_ini = mclsValues.StringToType(Request.QueryString.Item("nUnitini"), eFunctions.Values.eTypeData.etdDouble)

            '+Monto se divide por valor de unidad
            ldblValue = mclsValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble) / mclsValues.StringToType(Request.QueryString.Item("nUnitVal"), eFunctions.Values.eTypeData.etdDouble)
            '+Se calcula cantidad de unidades
            Response.Write("top.frames['fraFolder'].document.forms[0].tcnUnitsChange.value=VTFormat('" & ldblValue & "', '', '', '', 6, false);")

            If ldblValue_ini > 0 Then
                If System.Math.Abs(ldblValue_ini) - System.Math.Abs(ldblValue) <= 0.000001 Then
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcnUnitsChange.value=VTFormat('" & ldblValue_ini & "', '', '', '', 6, false);")
                End If
            End If

        End If

        If CDbl(Request.QueryString.Item("nSignal")) = 1 Then
            Response.Write("top.frames['fraFolder'].document.forms[0].tcnBuy_cost.value=VTFormat('" & (ldblUnits * mclsValues.StringToType(Request.QueryString.Item("nUnitVal"), eFunctions.Values.eTypeData.etdDouble) * (mclsValues.StringToType(Request.QueryString.Item("nBuyCost"), eFunctions.Values.eTypeData.etdDouble) / 100)) & "', '', '', '', 6, true);")
            Response.Write("top.frames['fraFolder'].document.forms[0].tcnSell_cost.value=VTFormat('" & 0 & "', '', '', '', 6, true);")
        Else
            Response.Write("top.frames['fraFolder'].document.forms[0].tcnBuy_cost.value=VTFormat('" & 0 & "', '', '', '', 6, true);")
            Response.Write("top.frames['fraFolder'].document.forms[0].tcnSell_cost.value=VTFormat('" & (ldblUnits * mclsValues.StringToType(Request.QueryString.Item("nUnitVal"), eFunctions.Values.eTypeData.etdDouble) * (mclsValues.StringToType(Request.QueryString.Item("nSellCost"), eFunctions.Values.eTypeData.etdDouble) / 100)) & "', '', '', '', 6, true);")
        End If

        Response.Write("top.frames['fraFolder'].insCalculateDebit();")

    End Sub

    '% insShowCurren_pol: Muestra la moneda asociada a la poliza/certificado
    '% Debe ser invocada con funcion insDefValues en vez de ShowPopUp
    '--------------------------------------------------------------------------------------------
    Sub insShowCurren_pol()
        '--------------------------------------------------------------------------------------------
        Dim lclsCurren_pol As ePolicy.Curren_pol
        Dim lstrDescript As String
        Dim lintCurrency As Integer

        lclsCurren_pol = New ePolicy.Curren_pol
        With lclsCurren_pol
            '+ Se buscan las monedas de la poliza
            Call .FindOneOrLocal(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

            lstrDescript = .sDescript
            lintCurrency = .nCurrency
        End With

        Response.Write("with (top.frames['fraHeader']){")
        Response.Write("    UpdateDiv('lblDesCurrency','" & lstrDescript & "','Normal');")
        Response.Write("    document.forms[0].tcnCurrency.value='" & lintCurrency & "';")
        Response.Write("}")

        lclsCurren_pol = Nothing
    End Sub

    '%insCalExpirDate : Obtiene la fecha de vigencia del rescate
    '--------------------------------------------------------------------------------------------
    Private Sub insCalExpirDate()
        '--------------------------------------------------------------------------------------------
        Dim lclsClient As ePolicy.Null_condi
        Dim lclsDigitClient As eClaim.Claim
        Dim lclsPolicy_po As ePolicy.Policy
        Dim nCertif As Object
        Dim nBranch As Object
        Dim nProduct As Object

        Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & Today & "';")

        '+ se agrego este manejo para el numero unico de poliza
        lclsClient = New ePolicy.Null_condi
        lclsPolicy_po = New ePolicy.Policy

        If Request.QueryString.Item("nCertif") = vbNullString Then
            nCertif = 0
        Else
            nCertif = Request.QueryString.Item("nCertif")
        End If

        If Request.QueryString.Item("nBranch") = vbNullString Then
            nBranch = 0
        Else
            nBranch = Request.QueryString.Item("nBranch")
        End If

        If Request.QueryString.Item("nProduct") = vbNullString Then
            nProduct = 0
        Else
            nProduct = Request.QueryString.Item("nProduct")
        End If

        If lclsPolicy_po.FindPolicybyPolicy("2", CDbl(Request.QueryString.Item("nPolicy"))) Then
            Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy_po.nBranch & ";")
            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy_po.nBranch & ";")
            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
            If lclsPolicy_po.nProduct > 0 Then
                Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
            End If

            Select Case lclsPolicy_po.sPolitype
                Case "1"
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='0';")
                Case "2", "3"
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.focus();")
            End Select

            nBranch = lclsPolicy_po.nBranch
            nProduct = lclsPolicy_po.nProduct

        End If

        If lclsClient.FindClientName("2", nBranch, nProduct, CDbl(Request.QueryString.Item("nPolicy")), nCertif, 1, eRemoteDB.Constants.dtmNull) Then
            lclsDigitClient = New eClaim.Claim
            Response.Write("top.frames['fraHeader'].document.forms[0].tctClient.value ='" & lclsClient.sClient & "';")
            Response.Write("top.frames['fraHeader'].document.forms[0].tctClient_Digit.value='" & lclsDigitClient.CalcDigit(lclsClient.sClient) & "';")
            Response.Write("top.frames['fraHeader'].UpdateDiv(""tctCliename"",""" & lclsClient.sCliename & """);")
            lclsDigitClient = Nothing
        Else
            Response.Write("top.frames['fraHeader'].document.forms[0].tctClient.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].tctClient_Digit.value='';")
            Response.Write("top.frames['fraHeader'].UpdateDiv(""tctCliename"","""");")
        End If
        lclsClient = Nothing

    End Sub

    '%sClientRole : Recupera el rut del contratante de la póliza
    '--------------------------------------------------------------------------------------------
    Private Sub sClientRole()
        '--------------------------------------------------------------------------------------------
        Dim lclsClient As ePolicy.client_typ
        Dim nCertif As Object

        lclsClient = New ePolicy.client_typ
        If Request.QueryString.Item("nCertif") = vbNullString Then
            nCertif = 0
        Else
            nCertif = Request.QueryString.Item("nCertif")
        End If
        If lclsClient.FindClient_roles(CDbl(Request.QueryString.Item("nPolicy")), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), nCertif) Then
            Response.Write("top.frames['fraHeader'].UpdateDiv(""tctClientRole"",""" & lclsClient.sClientRole & """);")
        End If
        lclsClient = Nothing
    End Sub

    '% insShowVIC005: Muestra los valores de acuerdo a una condición
    '------------------------------------------------------------------------------------------------
    Private Sub insShowVIC005()
        '------------------------------------------------------------------------------------------------
        Dim lclsLife As ePolicy.Life
        Dim lclsPolicy As ePolicy.Policy

        lclsLife = New ePolicy.Life
        lclsPolicy = New ePolicy.Policy
        If lclsLife.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")), CDate(Request.QueryString.Item("dEffecdate")), True) Then

            Call lclsPolicy.ValExistPolicyRec(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), Session("sTypeCompanyUser"))

            With Response
                .Write("with(opener.document.forms[0]){")
                .Write("   cbeBranch.value=" & mclsValues.TypeToString(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble) & ";")
                .Write("   cbeBranch.disabled=true;")
                .Write("   tcnCapital.value='" & mclsValues.TypeToString(lclsLife.nCapital, eFunctions.Values.eTypeData.etdDouble) & "';")
                .Write("   tcnCapital.disabled=true;")
                .Write("   tcnAge.value='" & mclsValues.TypeToString(lclsLife.nAge, eFunctions.Values.eTypeData.etdDouble) & "';")
                .Write("   tcnAge.disabled=true;")
                .Write("   tcnAge_reinsu.value='" & mclsValues.TypeToString(lclsLife.nAge_reinsu, eFunctions.Values.eTypeData.etdDouble) & "';")
                .Write("   tcnAge_reinsu.disabled=true;")
                .Write("   tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy.dstartdate, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("   tcdEffecdate.disabled=true;")
                .Write("   tcdExpirdat.value='" & mclsValues.TypeToString(lclsPolicy.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("   tcdExpirdat.disabled=true;")
                .Write("   cbePayfreq.value=" & mclsValues.TypeToString(lclsPolicy.nPayfreq, eFunctions.Values.eTypeData.etdDouble) & ";")
                .Write("   cbePayfreq.disabled=true;")
                .Write("   tcnPremium.value='" & mclsValues.TypeToString(lclsLife.nPremium, eFunctions.Values.eTypeData.etdDouble) & "';")
                .Write("   tcnPremium.disabled=true;")
                .Write("   valProduct.value=" & mclsValues.TypeToString(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble) & ";")
                Select Case lclsPolicy.sPolitype
                    Case "1"
                        Response.Write("  optTypePol[0].checked=true;")
                    Case "2"
                        Response.Write("  optTypePol[1].checked=true;")
                End Select
                .Write("}")
            End With
        End If
        lclsLife = Nothing
        lclsPolicy = Nothing
    End Sub

    '% insDateNextreceip: Muestra la fecha de próxima facturación de acuerdo a la frecuencia ingresada
    '------------------------------------------------------------------------------------------------
    Private Sub insDateNextreceip()
        '------------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim ldtmNewNextreceip As Date

        lclsPolicy = New ePolicy.Policy
        With lclsPolicy
            '+ Se llama al procedimiento para la búsqueda de la nueva fecha de facturación

            If mclsValues.StringToType(Request.QueryString.Item("dChandat"), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
                Call .ValDate_Nextreceip(mclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPayfreq"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dChandat"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate))
                If mclsValues.StringToType(Request.QueryString.Item("dChandat_ori"), eFunctions.Values.eTypeData.etdDate) > mclsValues.StringToType(Request.QueryString.Item("dChandat"), eFunctions.Values.eTypeData.etdDate) Then
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcdNewChangdat.value='" & Request.QueryString.Item("dChandat_ori") & "';") 'mclsValues.typetostring(.DefaultValueCA642("DateNextreceip"),eFunctions.Values.eTypeData.etdDate) & "';"
                End If

                ldtmNewNextreceip = mclsValues.StringToType(.DefaultValueCA642("DateNextreceip"), eFunctions.Values.eTypeData.etdDate)

                If ldtmNewNextreceip = eRemoteDB.Constants.dtmNull Then
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcdNewNextreceip.value='';")
                Else
                    Response.Write("top.frames['fraFolder'].document.forms[0].tcdNewNextreceip.value='" & ldtmNewNextreceip & "';") 'mclsValues.typetostring(.DefaultValueCA642("DateNextreceip"),eFunctions.Values.eTypeData.etdDate) & "';"
                End If

                '		Response.Write "top.frames['fraFolder'].document.forms[0].tcdNewNextreceip.value='" & mclsValues.typetostring(.DefaultValueCA642("DateNextreceip"),eFunctions.Values.eTypeData.etdDate) & "';"
                If mclsValues.TypeToString(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate) <> vbNullString Then
                    If .DefaultValueCA642("DateNextreceip") > mclsValues.StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate) Then
                        Response.Write("top.frames['fraFolder'].document.forms[0].tcdNewNextreceip.disabled = true;")
                        Response.Write("top.frames['fraFolder'].document.forms[0].btn_tcdNewNextreceip.disabled = true;")
                    End If
                End If
            End If
        End With
        lclsPolicy = Nothing
    End Sub

    '%insShowVidActiva: Se busca si el producto asociado a la póliza es de VidaActiva
    '--------------------------------------------------------------------------------------------
    Sub insShowRefund()
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct_po As eProduct.Product

        lclsProduct_po = New eProduct.Product

        If lclsProduct_po.Find(mclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
            Call lclsProduct_po.FindProduct_li(mclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
            '+ Si es de mayor a menor periodicidad no se marca y se deja deshabilitado 
            If Request.QueryString.Item("sInd") = "1" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].chkRefund.checked=false;")
                Response.Write("top.frames['fraHeader'].document.forms[0].chkRefund.disabled=true;")
                Session("nProdClas") = lclsProduct_po.nProdClas
            Else
                '+ Si es de menor a mayor periodicidad y es Vidactiva se marca y se deshabilita           
                If CStr(lclsProduct_po.sBrancht) = "1" And lclsProduct_po.nProdClas = 7 Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].chkRefund.checked=true;")
                    Response.Write("top.frames['fraHeader'].document.forms[0].chkRefund.disabled=true;")
                    Session("nProdClas") = lclsProduct_po.nProdClas
                End If
            End If
        Else
            If CStr(Session("nUsercode")) = "23" Then
                Response.Write("alert (""" & "No Find: " & """);")
            End If
        End If
        lclsProduct_po = Nothing
    End Sub

    '%insShowWorksheet: Se busca el Rampo Producto, Poliza Descrpición asociado a la Plantilla
    '--------------------------------------------------------------------------------------------
    Sub insShowWorksheet()
        '--------------------------------------------------------------------------------------------
        Dim lclsWorksheet As eBatch.Worksheet

        lclsWorksheet = New eBatch.Worksheet
        If lclsWorksheet.FindWorksheet(mclsValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble)) Then
            With Response
                .Write("with(opener.document.forms[0]){")
                .Write("  cbeBranch.value='" & mclsValues.TypeToString(lclsWorksheet.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
                .Write("  valProduct.value='" & mclsValues.TypeToString(lclsWorksheet.nProduct, eFunctions.Values.eTypeData.etdDouble) & "';")
                .Write("  tcnPolicy.value='" & mclsValues.TypeToString(lclsWorksheet.nPolicy, eFunctions.Values.eTypeData.etdDouble) & "';")
                .Write("  tctDescript.value='" & lclsWorksheet.sDescript & "';")
                .Write("}")
            End With
        End If
        lclsWorksheet = Nothing
    End Sub

    '%insSurrenValue : Obtiene los datos rescate
    '--------------------------------------------------------------------------------------------
    Private Sub insSurrenValue()
        '--------------------------------------------------------------------------------------------
        Dim lclsUsers As eGeneral.Users

        If Request.QueryString.Item("sCodispl") = "VI009_K" Then
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) & "';")
        End If

        lclsUsers = New eGeneral.Users
        If Request.QueryString.Item("sCodispl") <> "VI009_K" Then
            If lclsUsers.Find(Session("nUsercode")) Then
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & mclsValues.TypeToString(lclsUsers.nOffice, eFunctions.Values.eTypeData.etdDouble) & "';")

                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.value='" & mclsValues.TypeToString(lclsUsers.nOfficeAgen, eFunctions.Values.eTypeData.etdDouble) & "';")

                Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.value='" & mclsValues.TypeToString(lclsUsers.nAgency, eFunctions.Values.eTypeData.etdDouble) & "';")

                Response.Write("top.frames['fraHeader'].$('#cbeOfficeAgen').change();")
                Response.Write("top.frames['fraHeader'].$('#cbeAgency').change();")

            End If
        End If
        lclsUsers = Nothing
    End Sub

    '% insSuggestPrem: Calcula la prima proyectada sugerida 
    '%                 Debe usarse con rutina insDefValues
    '--------------------------------------------------------------------------------------------
    Private Sub insSuggestPrem()
        '--------------------------------------------------------------------------------------------
        Dim lclsActivelife As ePolicy.Activelife
        lclsActivelife = New ePolicy.Activelife
        With Request
            Call lclsActivelife.insCalSuggestPrem(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(.QueryString.Item("nTargetPremium"), eFunctions.Values.eTypeData.etdDouble, True), mclsValues.StringToType(.QueryString.Item("nTargetVP"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
            Response.Write("top.frames['fraFolder'].document.forms[0].tcnProposalProjPrem.value='" & mclsValues.TypeToString(lclsActivelife.nPrsugest, eFunctions.Values.eTypeData.etdDouble) & "';")
            Response.Write("top.frames['fraHeader'].setPointer('');")
        End With
        lclsActivelife = Nothing
    End Sub

    '%InsShowClientRole: Muestra la información del rol indicado para la póliza
    '--------------------------------------------------------------------------------------------
    Private Sub InsShowClientRole()
        '--------------------------------------------------------------------------------------------
        Dim lclsRoles As ePolicy.Roles
        lclsRoles = New ePolicy.Roles
        With Request
            Response.Write("with(top.frames['fraFolder'].document.forms[0]){")
            If lclsRoles.Find(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sClient"), mclsValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

                Response.Write("tctClient.value='" & lclsRoles.sClient & "';")
                Response.Write("tctClient_Digit.value='" & lclsRoles.sDigit & "';")
                Response.Write("top.frames['fraFolder'].UpdateDiv('tctClient_Name','" & lclsRoles.sCliename & "');")
                If .QueryString.Item("sCodispl") = "VI009" Then
                    Call insValPolitype()
                End If
            Else
                Response.Write("tctClient.value='';")
                Response.Write("tctClient_Digit.value='';")
                Response.Write("top.frames['fraFolder'].UpdateDiv('tctClient_Name','');")
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.disabled=false;")
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.value='';")
            End If
            Response.Write("}")
        End With
        lclsRoles = Nothing
    End Sub

    '%insCallPayOrder: Realiza llamada a transaccion de Ordenes de Pago
    '--------------------------------------------------------------------------------------------
    Private Sub insCallPayOrder()
        '--------------------------------------------------------------------------------------------

        Dim lclsMove_acc As eCashBank.Move_acc
        Dim lstrParams As String

        '+ Se cargan los parametros de session usados por ordenes de pago    		
        Session("OP006_nConcept") = Request.QueryString.Item("nConcept")
        Session("OP006_sCodispl") = Request.QueryString.Item("sCodisplOri")
        Session("OP006_nCurrency") = Request.QueryString.Item("nCurrency")
        Session("OP006_dReqDate") = Request.QueryString.Item("dEffecdate")
        Session("OP006_nAmountPay") = Request.QueryString.Item("nAmount")
        Session("OP006_sCertype") = Request.QueryString.Item("sCertype")
        Session("OP006_nBranch") = Request.QueryString.Item("nBranch")
        Session("OP006_nProduct") = Request.QueryString.Item("nProduct")
        Session("OP006_nPolicy") = Request.QueryString.Item("nPolicy")
        Session("OP006_nCertif") = Request.QueryString.Item("nCertif")
        Session("OP006_sBenef") = Request.QueryString.Item("sClient")

        If Request.QueryString.Item("sCertype") = "1" Then
            lstrParams = lstrParams & "&nProponum=" & Request.QueryString.Item("nPolicy")

            If Request.QueryString.Item("sCodisplOri") = "CA099A" Then
                lstrParams = lstrParams & "&nOffice=" & Request.QueryString.Item("nOffice") & "&nOfficeAgen=" & Request.QueryString.Item("nOfficeAgen") & "&nAgency=" & Request.QueryString.Item("nAgency")
                lclsMove_acc = New eCashBank.Move_acc
                Call lclsMove_acc.Find_nProponum(mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                If lclsMove_acc.dOperdate <> eRemoteDB.Constants.dtmNull Then
                    lstrParams = lstrParams & "&dEffecdate=" & lclsMove_acc.dOperdate
                End If
                lclsMove_acc = Nothing
            End If
        End If
        'Response.Write "ShowPopUp('/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString("sForm") & "&nMainAction=' + '" &  Request.QueryString("nMainAction") & lstrParams & "');"
        Response.Write("ShowPopUp('/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString.Item("sForm") & "&nMainAction=' + '" & Request.QueryString.Item("nMainAction") & lstrParams & "','',2000, 2000,'no','no',1,1);")

    End Sub

    '% insProcessCAL036: Se ejecuta el proceso de facturación de colectivos
    '--------------------------------------------------------------------------------------------
    Private Function insProcessCAL036() As Boolean
        Dim mclsPolicy As Object
        Dim mstrKey As Object
        '--------------------------------------------------------------------------------------------
        Dim lclsOut_moveme As ePolicy.Out_moveme
        Dim lclsBatch_param As eSchedule.Batch_param
        Dim batchParAreaProc As Integer

        If CStr(Session("BatchEnabled")) <> "1" Then
            lclsOut_moveme = New ePolicy.Out_moveme
            If lclsOut_moveme.insProcessCAL036(mclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nCertifCA039"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeMov"), mclsValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nMonth"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nSituation"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nGroup"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nTratypei"), eFunctions.Values.eTypeData.etdDouble), Session("sClient"), mclsValues.StringToType(Session("dLedgerDate"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("dStart"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Session("dEnd"), eFunctions.Values.eTypeData.etdDate)) Then
                Response.Write("insReloadTop(false);")
                insProcessCAL036 = True
                mstrKey = mclsPolicy.sKey
            End If
            lclsOut_moveme = Nothing

        Else
            lclsBatch_param = New eSchedule.Batch_param
            With lclsBatch_param
                .nBatch = 90
                batchParAreaProc = 1
                .nUsercode = Session("nUsercode")
                .Add(batchParAreaProc, .sKey)
                'Parametros Proceso
                .Add(batchParAreaProc, Session("sTypeMov"))
                .Add(batchParAreaProc, "2")
                .Add(batchParAreaProc, Session("nBranch"))
                .Add(batchParAreaProc, Session("nProduct"))
                .Add(batchParAreaProc, Session("nPolicy"))
                .Add(batchParAreaProc, Session("nCertifCA039"))
                .Add(batchParAreaProc, Session("nCurrency"))
                .Add(batchParAreaProc, Session("nYear"))
                .Add(batchParAreaProc, Session("nMonth"))
                .Add(batchParAreaProc, Session("nSituation"))
                .Add(batchParAreaProc, Session("nGroup"))
                .Add(batchParAreaProc, Session("nTratypei"))
                .Add(batchParAreaProc, Session("sClient"))
                .Add(batchParAreaProc, mclsValues.StringToType(Session("dLedgerDate"), eFunctions.Values.eTypeData.etdDate))
                .Add(batchParAreaProc, Session("nUsercode"))
                .Add(batchParAreaProc, mclsValues.StringToType(Session("dStart"), eFunctions.Values.eTypeData.etdDate))
                .Add(batchParAreaProc, mclsValues.StringToType(Session("dEnd"), eFunctions.Values.eTypeData.etdDate))

                .Save()
            End With
            Response.Write("alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');")
            lclsBatch_param = Nothing
            Response.Write("insReloadTop(false);")
            insProcessCAL036 = True
        End If

    End Function

    '%insDelTConvertions: Permite elimnar un regsitro de tconvertions para 
    '%evitar mostr al usuario la ventana de eliminación
    '--------------------------------------------------------------------------
    Private Sub insDelTConvertions()
        '--------------------------------------------------------------------------
        '-Objeto de conversion par eliminar datos
        Dim lclsTConvertions As ePolicy.TConvertions

        lclsTConvertions = New ePolicy.TConvertions
        With mclsValues
            Call lclsTConvertions.insPostCA099("PopUp", "Delete", .StringToType("", eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), "", .StringToType("", eFunctions.Values.eTypeData.etdDate), .StringToType("", eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDate), .StringToType("", eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDate), .StringToType("", eFunctions.Values.eTypeData.etdDate), .StringToType("", eFunctions.Values.eTypeData.etdDate), "", .StringToType("", eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDouble), "", "", .StringToType("", eFunctions.Values.eTypeData.etdDouble), "", Request.QueryString.Item("sCertype"), .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDouble), "", .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDouble), 1, .StringToType("", eFunctions.Values.eTypeData.etdDouble))
        End With
        lclsTConvertions = Nothing
        Response.Write("top.frames['fraFolder'].document.location.reload();")
    End Sub

    '%insTConvertions: Permite agregar un registro de tconvertions
    '--------------------------------------------------------------------------
    Private Sub insTConvertions()
        Dim mstrCommand As String
        '--------------------------------------------------------------------------

        '-Objeto de conversion par eliminar datos
        Dim lclsTConvertions As ePolicy.TConvertions
        Dim insValPolicyTra As String

        lclsTConvertions = New ePolicy.TConvertions
        With mclsValues
            insValPolicyTra = vbNullString
            insValPolicyTra = lclsTConvertions.insValCA099(Request.QueryString.Item("nOperat"), .StringToType(Request.QueryString.Item("nNoConvers"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.QueryString.Item("nStatus"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sCertype"), .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dDate_init"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dStat_date"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dLimit_date"), eFunctions.Values.eTypeData.etdDate))

            If insValPolicyTra = vbNullString Then
                Call lclsTConvertions.insPostCA099("PopUp", "Update", .StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sPen_doc"), .StringToType(Request.QueryString.Item("dDate_init"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nStatus"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dStat_date"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nNoConvers"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dLimit_date"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sObserv"), .StringToType(Request.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nStatus_ord"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nFirst_prem"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPrem_curr"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sPrem_che"), Request.QueryString.Item("sPay_order"), .StringToType(Request.QueryString.Item("nExpenses"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDevolut"), Request.QueryString.Item("sCertype"), .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble), .StringToType("1", eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nWait_Code"), eFunctions.Values.eTypeData.etdDouble))
            Else
                mstrCommand = "sModule=Policy&sProject=PolicyTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")
                Session("sErrorTable") = insValPolicyTra
                Session("sForm") = Request.Form.ToString
                Response.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""PolicyTraError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
            End If
        End With

        lclsTConvertions = Nothing
        Response.Write("top.frames['fraFolder'].document.location.reload();")
    End Sub

    '% ValPolitype: valida el tipo de póliza para habilitar/deshabilitar el certificado
    '% Debe ser invocada con funcion insDefValues
    '--------------------------------------------------------------------------------------------
    Sub ValPolitype()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim lstrFrame As String

        lclsPolicy = New ePolicy.Policy
        lstrFrame = Request.QueryString.Item("sFrame")
        If lstrFrame = vbNullString Then
            lstrFrame = "fraHeader"
        End If

        If lclsPolicy.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
            '+Asignación del Tipo de póliza
            Response.Write("with(top.frames['" & lstrFrame & "'].document.forms[0]){")
            Response.Write("cbeOffice.value='" & mclsValues.StringToType(CStr(lclsPolicy.nOffice), eFunctions.Values.eTypeData.etdDouble) & "';")
            Response.Write("cbeOfficeAgen.value='" & mclsValues.StringToType(CStr(lclsPolicy.nOfficeAgen), eFunctions.Values.eTypeData.etdDouble) & "';")
            Response.Write("cbeAgency.value='" & mclsValues.StringToType(CStr(lclsPolicy.nAgency), eFunctions.Values.eTypeData.etdDouble) & "';")

            Response.Write("cbeOffice.disabled = 'True';")
            Response.Write("cbeOfficeAgen.disabled = 'True';")
            Response.Write("cbeAgency.disabled = 'True';")

            Select Case lclsPolicy.sPolitype
                Case "1"
                    Response.Write("tcnCertif.disabled=true;")
                    Response.Write("tcnCertif.value=""0"";")
                    If Request.QueryString.Item("sForm") <> "VI011" Then
                        Response.Write("hdddStardate.value='" & mclsValues.TypeToString(lclsPolicy.dstartdate, eFunctions.Values.eTypeData.etdDate) & "';")
                    End If
                    Session("dStartdate") = mclsValues.TypeToString(lclsPolicy.dstartdate, eFunctions.Values.eTypeData.etdDate)

                Case "2"
                    Response.Write("tcnCertif.disabled=false;")
                    Response.Write("tcnCertif.value=""0"";")
                    Response.Write("tcnCertif.focus();")
                Case "3"
                    Response.Write("tcnCertif.disabled=false;")
                    Response.Write("tcnCertif.value=""0"";")
                    Response.Write("tcnCertif.focus();")
            End Select

            If Request.QueryString.Item("sExecCertif") = "1" Then
                Response.Write("if(tcnCertif.disabled)")
                Response.Write("top.frames['" & lstrFrame & "'].$('#tcnCertif').change();")
            End If
            Response.Write("}")
        Else
            Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.disabled=false;")
            Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.value="""";")
        End If
        lclsPolicy = Nothing
    End Sub

    '% insShowAgency: Sub para el manejo de la fecha de la agencia
    '--------------------------------------------------------------------------------------------
    Sub insShowAgency()
        '--------------------------------------------------------------------------------------------
        Dim lclsAgencies As eGeneralForm.Agencies
        Dim lblvalor As Boolean
        lclsAgencies = New eGeneralForm.Agencies
        mclsValues.Parameters.Add("nOfficeAgen", Request.QueryString.Item("nOffice"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mclsValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        If mclsValues.IsValid("TabAgencies_T5555", Request.QueryString.Item("nAgency"), True) Then
            lblvalor = lclsAgencies.Find(Request.QueryString.Item("nAgency"))
            If lclsAgencies.nOfficeAgen > 0 Then
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & lclsAgencies.nBran_Off & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsAgencies.nBran_Off & ";")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & mclsValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble) & ";")
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.value='" & lclsAgencies.nOfficeAgen & "';")
                Response.Write("top.frames['fraHeader'].$('#cbeOfficeAgen').change();")
            End If
        End If
        lclsAgencies = Nothing
    End Sub

    '% insShowDev: Sub para el manejo del valor por defecto para la forma de cálculo tomada del tipo de anulación
    '--------------------------------------------------------------------------------------------
    Sub insShowDev()
        '--------------------------------------------------------------------------------------------
        Dim lclsNull_Condi As ePolicy.Null_condi

        lclsNull_Condi = New ePolicy.Null_condi
        If lclsNull_Condi.Find(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CInt(Request.QueryString.Item("nNullCode")), CDate(Request.QueryString.Item("dNullDate"))) Then
            Select Case lclsNull_Condi.sReturn_ind
                Case "1" 'no tiene
                    Response.Write("top.opener.document.forms[0].elements['optDev'][0].checked=false;")
                    Response.Write("top.opener.document.forms[0].elements['optDev'][0].checked=false;")
                    Response.Write("top.opener.document.forms[0].elements['optDev'][0].checked=false;")
                    Response.Write("top.opener.document.forms[0].elements['tcnPercent'].value='';")
                Case "2" 'a prorrata	
                    Response.Write("top.opener.document.forms[0].elements['optDev'][0].checked=true;")
                    Response.Write("top.opener.document.forms[0].elements['tcnPercent'].value='';")
                Case "3" 'corto plazo
                    Response.Write("top.opener.document.forms[0].elements['optDev'][1].checked=true;")
                    Response.Write("top.opener.document.forms[0].elements['tcnPercent'].value='';")
                Case "4" 'porcentaje
                    Response.Write("top.opener.document.forms[0].elements['optDev'][2].checked=true;")
                    Response.Write("top.opener.document.forms[0].elements['tcnPercent'].value=" & lclsNull_Condi.nReturn_rat & ";")
                Case "9" 'Rutina
                    Response.Write("top.opener.document.forms[0].elements['optDev'][3].checked=true;")
                    Response.Write("top.opener.document.forms[0].elements['tcnPercent'].value=" & lclsNull_Condi.nReturn_rat & ";")

            End Select
        End If
        lclsNull_Condi = Nothing
    End Sub

    '% Account_Pol: Se muestran la fecha de último movimiento de la cuenta valor póliza
    '--------------------------------------------------------------------------------------------
    Sub Account_Pol(ByVal nCertif As Object)
        '--------------------------------------------------------------------------------------------
        Dim lclsAccount_Pol As ePolicy.Account_Pol

        lclsAccount_Pol = New ePolicy.Account_Pol
        With lclsAccount_Pol
            If nCertif <> 0 Then
                If .Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
                End If
            Else
                If .Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdDouble)) Then
                End If
            End If
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(.dLastdate, eFunctions.Values.eTypeData.etdDate) & "';")
        End With
        lclsAccount_Pol = Nothing
    End Sub

    '% insShowLoans: Se muestra la oficina sucursal agencia asociada a un préstamo
    '--------------------------------------------------------------------------------------------
    Sub insShowLoans()
        '--------------------------------------------------------------------------------------------
        Dim lclsLoans As ePolicy.Loans
        Dim lclsAgencies As eGeneralForm.Agencies
        Dim lblvalor As Boolean

        lclsLoans = New ePolicy.Loans

        With lclsLoans
            If .Find(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")), CDbl(Request.QueryString.Item("nLoans"))) Then
                Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.value='" & .nAgency & "';")
                lclsAgencies = New eGeneralForm.Agencies
                mclsValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mclsValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If mclsValues.IsValid("TabAgencies_T5555", CStr(.nAgency), True) Then
                    lblvalor = lclsAgencies.Find(.nAgency)
                    If lclsAgencies.nOfficeAgen > 0 Then
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & lclsAgencies.nBran_Off & "';")
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsAgencies.nBran_Off & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & .nAgency & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.Parameters.Param1.sValue =" & lclsAgencies.nBran_Off & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.Parameters.Param2.sValue =" & .nAgency & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.value='" & lclsAgencies.nOfficeAgen & "';")
                    End If
                End If
                lclsAgencies = Nothing
            End If
        End With
        lclsLoans = Nothing
    End Sub

    '% insShowcbeAgency: Sub para el manejo de la fecha de la agencia
    '--------------------------------------------------------------------------------------------
    Sub insShowcbeAgency()
        '--------------------------------------------------------------------------------------------
        Dim lclsAgencies As eGeneralForm.Agencies
        Dim lblvalor As Boolean
        lclsAgencies = New eGeneralForm.Agencies
        mclsValues.Parameters.Add("nOfficeAgen", Request.QueryString.Item("nOffice"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mclsValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        If mclsValues.IsValid("TabAgencies_T5555", Request.QueryString.Item("nAgency"), True) Then
            lblvalor = lclsAgencies.Find(Request.QueryString.Item("nAgency"))
            If lclsAgencies.nOfficeAgen > 0 Then
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeOffice.value='" & lclsAgencies.nBran_Off & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsAgencies.nBran_Off & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & mclsValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble) & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.value='" & lclsAgencies.nOfficeAgen & "';")
                Response.Write("top.frames['fraFolder'].$('#cbeOfficeAgen').change();")
            End If
        End If
        lclsAgencies = Nothing
    End Sub

    '% SetCertificate_value: Habilita/deshabilita el campo certificado y coloca el valor respectivo
    '--------------------------------------------------------------------------------------------
    Sub SetCertificate_value()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy

        lclsPolicy = New ePolicy.Policy

        If lclsPolicy.Find(Request.QueryString.Item("sCertype"), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy"))) Then
            '+ Si es una póliza individual se asigna cero (0) al campo CERTIFICADO y se deshabilita,
            '+ de lo contrario se deja habilitado - ACM - 02/09/2003
            If lclsPolicy.sPolitype = "1" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value = 0;")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled = true;")
            Else
                Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled = false;")
            End If
        End If
    End Sub

    '%insUpdVi010: Permite actualizar al realizar un check en la vi010 de todos 
    '%             los registros marcados de la grilla. 
    '-------------------------------------------------------------------------- 
    Private Sub insUpdVi010()
        '-------------------------------------------------------------------------- 
        '-Objeto de conversion par eliminar datos 
        Dim insValPolicyTra As String
        Dim mobjPolicyTra As ePolicy.Funds_Pol
        Dim lblnPost As Object
        Dim index As Short

        Dim nCodFund1 As Integer
        Dim nCodFund2 As Integer
        Dim nCodFund As String
        Dim nCodFundl As Double
        Dim nCodFundv As String
        Dim nUnitsv As String
        Dim nUnits As String
        Dim nUnitsl As Double
        Dim nUnits1 As Integer
        Dim nUnits2 As Integer
        Dim nSignal1 As Integer
        Dim nSignal As String
        Dim nSignal2 As Integer
        Dim nSignalv As String
        Dim nSignall As Double
        Dim nUnitsChangel As Double
        Dim nUnitsChange As String
        Dim nUnitsChange1 As Integer
        Dim nUnitsChangev As Object
        Dim nUnitsChange2 As Integer
        Dim nTotal_Amountl As Double
        Dim nTotal_Amount1 As Integer
        Dim nTotal_Amount2 As Integer
        Dim nTotal_Amount As String
        Dim nTotal_Amountv As String
        Dim nUpdatev As String
        Dim nUpdate2 As Integer
        Dim nUpdate1 As Integer
        Dim nUpdatel As Double
        Dim nUpdate As String
        Dim nSell_cost As String
        Dim nSell_cost2 As Integer
        Dim nSell_costv As String
        Dim nSell_costl As Double
        Dim nSell_cost1 As Integer
        Dim nBuy_cost As String
        Dim nBuy_costl As Double
        Dim nBuy_cost1 As Integer
        Dim nBuy_costv As String
        Dim nBuy_cost2 As Integer
        Dim nSwi_costv As String
        Dim nSwi_cost1 As Integer
        Dim nSwi_cost2 As Integer
        Dim nSwi_cost As String
        Dim nSwi_costl As Double
        Dim nDeb_accl As Double
        Dim nDeb_accv As String
        Dim nDeb_acc1 As Integer
        Dim nDeb_acc As String
        Dim nDeb_acc2 As Integer
        Dim nValueChange As String
        Dim nValueChangel As Double
        Dim nValueChange1 As Integer
        Dim nValueChange2 As Integer
        Dim nValueChangev As Object
        Dim nAvailable As String
        Dim nAvailablel As Double
        Dim nAvailable1 As Integer
        Dim nAvailable2 As Integer
        Dim nAvailablev As String
        Dim sActivFoundv As Object
        Dim sActivFoundl As Double
        Dim sActivFound As String
        Dim sActivFound1 As Integer
        Dim sActivFound2 As Integer
        Dim nCount As Object

        nCodFund1 = 1
        nUnits1 = 1
        nSignal1 = 1
        nUnitsChange1 = 1
        nTotal_Amount1 = 1
        nUpdate1 = 1
        nSell_cost1 = 1
        nBuy_cost1 = 1
        nSwi_cost1 = 1
        nDeb_acc1 = 1
        nValueChange1 = 1
        nAvailable1 = 1
        sActivFound1 = 1

        nCodFund = Request.QueryString.Item("nCodFund")
        nUnits = Request.QueryString.Item("nUnits")
        nSignal = Request.QueryString.Item("nSignal")
        nUnitsChange = Request.QueryString.Item("nUnitsChange")
        nTotal_Amount = Request.QueryString.Item("nTotal_Amount")
        nUpdate = Request.QueryString.Item("nUpdate")
        nSell_cost = Request.QueryString.Item("nSell_cost")
        nBuy_cost = Request.QueryString.Item("nBuy_cost")
        nSwi_cost = Request.QueryString.Item("nSwi_cost")
        nDeb_acc = Request.QueryString.Item("nDeb_acc")
        nValueChange = Request.QueryString.Item("nValueChange")
        nAvailable = Request.QueryString.Item("nAvailable")
        sActivFound = Request.QueryString.Item("sActivFound")
        nCount = Request.QueryString.Item("nCount")

        mobjPolicyTra = New ePolicy.Funds_Pol
        index = 1
        Dim lclsExchange As eGeneral.Exchange
        Do While (index < (nCount + 1))
            With mclsValues
                nCodFund2 = InStr(nCodFund1, nCodFund, ";")
                nCodFundl = nCodFund2 - nCodFund1
                nCodFundv = Mid(nCodFund, nCodFund1, nCodFundl)
                nCodFund1 = nCodFund2 + 1

                nUnits2 = InStr(nUnits1, nUnits, ";")
                nUnitsl = nUnits2 - nUnits1
                nUnitsv = Mid(nUnits, nUnits1, nUnitsl)
                nUnits1 = nUnits2 + 1

                nSignal2 = InStr(nSignal1, nSignal, ";")
                nSignall = nSignal2 - nSignal1
                nSignalv = Mid(nSignal, nSignal1, nSignall)
                nSignal1 = nSignal2 + 1

                nUnitsChange2 = InStr(nUnitsChange1, nUnitsChange, ";")
                nUnitsChangel = nUnitsChange2 - nUnitsChange1
                nUnitsChangev = Mid(nUnitsChange, nUnitsChange1, nUnitsChangel)
                nUnitsChange1 = nUnitsChange2 + 1

                nTotal_Amount2 = InStr(nTotal_Amount1, nTotal_Amount, ";")
                nTotal_Amountl = nTotal_Amount2 - nTotal_Amount1
                nTotal_Amountv = Mid(nTotal_Amount, nTotal_Amount1, nTotal_Amountl)
                nTotal_Amount1 = nTotal_Amount2 + 1

                nUpdate2 = InStr(nUpdate1, nUpdate, ";")
                nUpdatel = nUpdate2 - nUpdate1
                nUpdatev = Mid(nUpdate, nUpdate1, nUpdatel)
                nUpdate1 = nUpdate2 + 1

                nSell_cost2 = InStr(nSell_cost1, nSell_cost, ";")
                nSell_costl = nSell_cost2 - nSell_cost1
                nSell_costv = Mid(nSell_cost, nSell_cost1, nSell_costl)
                nSell_cost1 = nSell_cost2 + 1

                nBuy_cost2 = InStr(nBuy_cost1, nBuy_cost, ";")
                nBuy_costl = nBuy_cost2 - nBuy_cost1
                nBuy_costv = Mid(nBuy_cost, nBuy_cost1, nBuy_costl)
                nBuy_cost1 = nBuy_cost2 + 1

                nSwi_cost2 = InStr(nSwi_cost1, nSwi_cost, ";")
                nSwi_costl = nSwi_cost2 - nSwi_cost1
                nSwi_costv = Mid(nSwi_cost, nSwi_cost1, nSwi_costl)
                nSwi_cost1 = nSwi_cost2 + 1

                nDeb_acc2 = InStr(nDeb_acc1, nDeb_acc, ";")
                nDeb_accl = nDeb_acc2 - nDeb_acc1
                nDeb_accv = Mid(nDeb_acc, nDeb_acc1, nDeb_accl)
                nDeb_acc1 = nDeb_acc2 + 1

                nValueChange2 = InStr(nValueChange1, nValueChange, ";")
                nValueChangel = nValueChange2 - nValueChange1
                nValueChangev = Mid(nValueChange, nValueChange1, nValueChangel)
                nValueChange1 = nValueChange2 + 1

                nAvailable2 = InStr(nAvailable1, nAvailable, ";")
                nAvailablel = nAvailable2 - nAvailable1
                nAvailablev = Mid(nAvailable, nAvailable1, nAvailablel)
                nAvailable1 = nAvailable2 + 1

                sActivFound2 = InStr(sActivFound1, sActivFound, ";")
                sActivFoundl = sActivFound2 - sActivFound1
                sActivFoundv = Mid(sActivFound, sActivFound1, sActivFoundl)
                sActivFound1 = sActivFound2 + 1

                If sActivFoundv Then
                    sActivFoundv = "1"
                Else
                    sActivFoundv = "2"
                End If

                nUnitsChangev = mclsValues.StringToType(nUnitsChangev, eFunctions.Values.eTypeData.etdDouble)
                nValueChangev = mclsValues.StringToType(nValueChangev, eFunctions.Values.eTypeData.etdDouble)
                If nUnitsChangev = 0 Then
                    lclsExchange = New eGeneral.Exchange
                    nUnitsChangev = mclsValues.StringToType(nUnitsv, eFunctions.Values.eTypeData.etdDouble)
                    nValueChangev = nUnitsChangev * mclsValues.StringToType(nTotal_Amountv, eFunctions.Values.eTypeData.etdDouble)
                    Call lclsExchange.Convert(0, nValueChangev, 1, Session("nCurrency"), Session("dEffecdate"), 0, True)
                    nValueChangev = lclsExchange.pdblResult
                    lclsExchange = Nothing
                End If

                insValPolicyTra = mobjPolicyTra.insValVI010("VI010", "PopUpSDef", .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(nUnitsv, eFunctions.Values.eTypeData.etdDouble), .StringToType(nSignalv, eFunctions.Values.eTypeData.etdDouble), .StringToType(nUnitsChangev, eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), sActivFoundv, .StringToType(nAvailablev, eFunctions.Values.eTypeData.etdDouble), .StringToType(nValueChangev, eFunctions.Values.eTypeData.etdDouble))

                If insValPolicyTra = vbNullString Then
                    Call mobjPolicyTra.insPostVI010("VI010", CInt(Request.QueryString.Item("nMainAction")), .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(nCodFundv, eFunctions.Values.eTypeData.etdDouble), .StringToType(nUnitsv, eFunctions.Values.eTypeData.etdDouble), .StringToType(nSignalv, eFunctions.Values.eTypeData.etdDouble), .StringToType(nUnitsChangev, eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .StringToType(nSell_costv, eFunctions.Values.eTypeData.etdDouble), .StringToType(nBuy_costv, eFunctions.Values.eTypeData.etdDouble), .StringToType(nSwi_costv, eFunctions.Values.eTypeData.etdDouble), .StringToType(nValueChangev, eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    Response.Write("alert (""" & "Fondo " & nCodFundv & ": No se pudo realizar la venta, Intentarlo en forma puntual " & """);")
                End If
                index = index + 1
            End With
        Loop
        mobjPolicyTra = Nothing
        If nCount = 0 Then
            Response.Write("alert('Debe seleccionar al menos un registro');")
        Else
            Response.Write("top.frames['fraFolder'].document.location.reload();")
        End If
    End Sub

    '%insUpdVi7002: Permite actualizar al realizar un check en la vi010 de todos 
    '%             los registros marcados de la grilla. 
    '-------------------------------------------------------------------------- 
    Private Sub insUpdVi7002()
        '-------------------------------------------------------------------------- 
        Dim lclsFunds_Pol As ePolicy.tmp_Funds_Pol
        Dim sActivFound As Object
        Dim insValPolicyTra As Object
        Dim mobjValues As eFunctions.Values
        mobjValues = New eFunctions.Values
        'Response.Write "alert('"& Request.QueryString &"');"
        lclsFunds_Pol = New ePolicy.tmp_Funds_Pol
        Call lclsFunds_Pol.insPostvi7002upd(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nParticip"), eFunctions.Values.eTypeData.etdDouble), "2", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "Del")
        lclsFunds_Pol = Nothing
        mobjValues = Nothing
        Response.Write("top.frames['fraFolder'].document.location.reload();")
    End Sub

    '% ExpirDateRec: Obtiene la fecha de expiracion del recibo
    '-------------------------------------------------------------------------- 
    Private Sub ExpirDateRec()
        '-------------------------------------------------------------------------- 
        Dim lclsCertificat As ePolicy.Certificat
        Dim ldtmNewNextreceip As Date

        lclsCertificat = New ePolicy.Certificat
        With lclsCertificat
            '+ Se llama al procedimiento para la búsqueda de la nueva fecha de facturación

            If mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
                .sCertype = Request.QueryString.Item("sCertype")
                .nBranch = mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
                .nProduct = mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
                .nPolicy = mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
                .nCertif = mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)
                Call .insCalcPeriodDates(mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull)
                ldtmNewNextreceip = .dEndCurrentPeriod

                Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirDateR.value='" & mclsValues.TypeToString(ldtmNewNextreceip, eFunctions.Values.eTypeData.etdDate) & "';")

            End If
        End With
        lclsCertificat = Nothing
    End Sub
    '% InsCalSurrCost: Calcula los costos por rescates parciales
    '-------------------------------------------------------------------------- 
    Private Sub InsCalSurrCost()
        '-------------------------------------------------------------------------- 
        Dim ldblSurrAmt As Double
        Dim ldblPct_charge As Double
        Dim ldblFix_charge As Double
        Dim ldblMaxChargSurr As Double
        Dim ldblTotalCost As Double
        ldblSurrAmt = mclsValues.StringToType(Request.QueryString.Item("nSurrAmt"), eFunctions.Values.eTypeData.etdDouble)
        ldblPct_charge = mclsValues.StringToType(Request.QueryString.Item("nPct_charge"), eFunctions.Values.eTypeData.etdDouble)
        ldblFix_charge = mclsValues.StringToType(Request.QueryString.Item("nFix_charge"), eFunctions.Values.eTypeData.etdDouble)
        ldblMaxChargSurr = mclsValues.StringToType(Request.QueryString.Item("nMaxChargSurr"), eFunctions.Values.eTypeData.etdDouble)
        ldblTotalCost = (ldblSurrAmt * ldblPct_charge) / 100 + ldblFix_charge
        If ldblMaxChargSurr > 0 Then
            If ldblTotalCost > ldblMaxChargSurr Then
                ldblTotalCost = ldblMaxChargSurr
            End If
        End If
        Response.Write("top.frames['fraFolder'].document.forms[0].tcnSurrCost.value='" & mclsValues.insReturnUserNumber(ldblTotalCost, True, 6) & "';")
    End Sub

    'InsCalPrem_Guar_Saving:
    '--------------------------------------------------------------------------------------------
    Sub InsCalPrem_Guar_Saving()
        '--------------------------------------------------------------------------------------------
        Dim lclsGuar_Saving_Pol As ePolicy.Guar_Saving_Pol
        Dim ldtmEndPeriod As Date

        lclsGuar_Saving_Pol = New ePolicy.Guar_Saving_Pol
        With Request
            If lclsGuar_Saving_Pol.insShowVI8000(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(.QueryString.Item("nGuarsav_value"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nGuarsav_year"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nRen_guarsav"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sDeppremind"), mclsValues.StringToType(Request.QueryString.Item("nOption"), eFunctions.Values.eTypeData.etdLong)) Then
                If lclsGuar_Saving_Pol.nGuarsav_prem < 0 Then
                    lclsGuar_Saving_Pol.nGuarsav_prem = 0
                End If
                ldtmEndPeriod = DateAdd(Microsoft.VisualBasic.DateInterval.Year, mclsValues.StringToType(.QueryString.Item("nGuarsav_year"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("dIniPeriod"), eFunctions.Values.eTypeData.etdDate))
                Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
                Response.Write("    tcnNewPrem.value='" & lclsGuar_Saving_Pol.nGuarsav_prem + lclsGuar_Saving_Pol.nGuarsav_cost & "';")
                Response.Write("    tcnDiference.value='" & mclsValues.StringToType(.QueryString.Item("nCurrentAmount"), eFunctions.Values.eTypeData.etdDouble) - mclsValues.StringToType(.QueryString.Item("nGuarsav_value"), eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("    tcdEndPeriod.value='" & mclsValues.TypeToString(ldtmEndPeriod, eFunctions.Values.eTypeData.etdDate) & "';")
                Response.Write("}")
            End If
        End With
        lclsGuar_Saving_Pol = Nothing
    End Sub
    '% InsShowUser: Carga el código de usuario que estaba emitiendo o modificando la póliza/cotización/propuesta
    '% Debe ser invocada con funcion insDefValues
    '--------------------------------------------------------------------------------------------
    Sub InsShowUser()
        '--------------------------------------------------------------------------------------------
        Dim lstrFrame As String
        Dim lstrCertype As String
        Dim lclsCertificat As ePolicy.Certificat

        lstrFrame = Request.QueryString.Item("sFrame")
        If lstrFrame = vbNullString Then
            lstrFrame = "fraHeader"
        End If
        lstrCertype = Request.QueryString.Item("sCertype")

        If lstrCertype = vbNullString Then
            lstrCertype = "2"
        End If


        lclsCertificat = New ePolicy.Certificat
        With lclsCertificat
            If .Find(lstrCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdLong)) Then
                If Request.QueryString.Item("sCodispl") = "CA888" Then
                    If lclsCertificat.nUser_amend >= 0 Then
                        Response.Write("top.frames['fraHeader'].document.forms[0].valUsers.value='" & lclsCertificat.nUser_amend & "';")
                    Else
                        Response.Write("top.frames['fraHeader'].document.forms[0].valUsers.value='';")
                    End If
                    Response.Write("top.frames['fraHeader'].$('#valUsers').change();")
                End If
            End If
        End With

        lclsCertificat = Nothing
    End Sub
    '-----------------------------------------------------------------------------------------
    '% CalExpirdateNew: Se encarga de calcular la nueva fecha de vencimiento
    '-----------------------------------------------------------------------------------------
    Private Sub CalExpirdateNew()
        '-----------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim ldtmNextReceipt As Object
        Dim ldtmEffecdate As String
        Dim ldtmExpirdat As Object
        Dim lblnCalc As Boolean
        Dim linterval As Object
        Dim lstrExpirdat As Date
        Dim lclsLife As ePolicy.Life
        Dim lintnAge As Double
        lclsCertificat = New ePolicy.Certificat
        lclsPolicy = New ePolicy.Policy
        If lclsPolicy.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
            If (lclsPolicy.sPolitype = "2" Or lclsPolicy.sPolitype = "3") And lclsPolicy.sColtimre = "1" And mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                lblnCalc = False
            Else
                lblnCalc = True
            End If
        Else
            lblnCalc = True
        End If

        If lblnCalc Then
            If lclsCertificat.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
                '+ Se calcula la nueva fecha de vencimiento		

                'UPGRADE_NOTE: Date operands have a different behavior in arithmetical operations. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1023.htm
                'linterval = System.DateTime.FromOADate(CDate(mclsValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)).ToOADate - lclsCertificat.dStartdate.ToOADate)
                linterval = System.Math.Abs(DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mclsValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)), lclsCertificat.dStartdate))
                lstrExpirdat = lclsCertificat.dExpirdat
                If lclsCertificat.dExpirdat = CDate("0:00:00") Then
                    ldtmExpirdat = lclsCertificat.dExpirdat
                Else
                    ldtmExpirdat = DateAdd(Microsoft.VisualBasic.DateInterval.Day, linterval, lclsCertificat.dExpirdat)
                End If

                If mclsValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
                    Response.Write("if(top.frames['fraHeader'].document.forms[0].tcdExpirdateNew!=null) top.frames['fraHeader'].document.forms[0].tcdExpirdateNew.value='';")
                Else
                    Response.Write("if(top.frames['fraHeader'].document.forms[0].tcdExpirdateNew!=null) top.frames['fraHeader'].document.forms[0].tcdExpirdateNew.value='" & mclsValues.TypeToString(ldtmExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
                End If

                ldtmEffecdate = Request.QueryString.Item("dEffecdate")

                '+ Se calcula la próxima fecha de facturación en base a la frecuencia de la pol/cert. y en base a la nueva fecha de efecto introducida				
                ldtmNextReceipt = lclsPolicy.ValDate_Nextreceip(mclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsCertificat.nPayfreq), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(ldtmEffecdate, eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(ldtmExpirdat, eFunctions.Values.eTypeData.etdDate))


                ldtmNextReceipt = mclsValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)
                If lclsCertificat.nPayfreq = 1 Then
                    ldtmNextReceipt = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 12, ldtmNextReceipt)
                End If
                If lclsCertificat.nPayfreq = 2 Then
                    ldtmNextReceipt = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 6, ldtmNextReceipt)
                End If
                If lclsCertificat.nPayfreq = 3 Then
                    ldtmNextReceipt = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 3, ldtmNextReceipt)
                End If
                If lclsCertificat.nPayfreq = 4 Then
                    ldtmNextReceipt = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 2, ldtmNextReceipt)
                End If
                If lclsCertificat.nPayfreq = 5 Then
                    ldtmNextReceipt = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, ldtmNextReceipt)
                End If
                If lclsCertificat.nPayfreq = 6 Then
                    lclsLife = New ePolicy.Life
                    If lclsLife.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), True) Then
                        If lclsLife.nTypDurins = 5 Then 'Duración abierta
                            lintnAge = lclsLife.nAge_limit - lclsLife.nAge_reinsu
                            ldtmNextReceipt = DateAdd(Microsoft.VisualBasic.DateInterval.Year, lintnAge, ldtmNextReceipt)
                        End If
                        If lclsLife.nTypDurins = 2 Then 'Duración en años
                            ldtmNextReceipt = DateAdd(Microsoft.VisualBasic.DateInterval.Year, lclsLife.nInsur_time, ldtmNextReceipt)
                        End If
                    End If
                End If
                'ldtmNextReceipt = ldtmNextReceipt - 1
                ldtmNextReceipt = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, ldtmNextReceipt)
                If lclsCertificat.dNextreceip = eRemoteDB.Constants.dtmNull And lclsCertificat.nPayfreq = 6 Then
                    ldtmNextReceipt = ""
                End If
                If mclsValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
                    Response.Write("if(top.frames['fraHeader'].document.forms[0].tcdNextReceip!=null) top.frames['fraHeader'].document.forms[0].tcdNextReceip.value='';")
                Else
                    Response.Write("if(top.frames['fraHeader'].document.forms[0].tcdNextReceip!=null) top.frames['fraHeader'].document.forms[0].tcdNextReceip.value='" & mclsValues.TypeToString(ldtmNextReceipt, eFunctions.Values.eTypeData.etdDate) & "';")
                End If
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdNextReceip.disabled=true;")
            End If
        Else
            If lclsPolicy.dExpirdat = eRemoteDB.Constants.dtmNull Then
                Response.Write("if(top.frames['fraHeader'].document.forms[0].tcdExpirdateNew!=null) top.frames['fraHeader'].document.forms[0].tcdExpirdateNew.value='';")
            Else
                Response.Write("if(top.frames['fraHeader'].document.forms[0].tcdExpirdateNew!=null) top.frames['fraHeader'].document.forms[0].tcdExpirdateNew.value='" & mclsValues.TypeToString(lclsPolicy.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
            End If

            If lclsPolicy.dNextreceip = eRemoteDB.Constants.dtmNull Then
                Response.Write("if(top.frames['fraHeader'].document.forms[0].tcdNextReceip!=null) top.frames['fraHeader'].document.forms[0].tcdNextReceip.value='';")
            Else
                Response.Write("if(top.frames['fraHeader'].document.forms[0].tcdNextReceip!=null) top.frames['fraHeader'].document.forms[0].tcdNextReceip.value='" & mclsValues.TypeToString(lclsPolicy.dNextreceip, eFunctions.Values.eTypeData.etdDate) & "';")
            End If
        End If
        If CStr(Session("sPolitype")) = "2" And CStr(Session("sColtimre")) = "1" And mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
            Session("CA037_nDevolution1") = 1
        Else
            Session("CA037_nDevolution1") = 2
        End If

        lclsCertificat = Nothing
        lclsPolicy = Nothing
    End Sub

    '% ChangeValuesSOAT: Se habilitan/deshabilitan los controles de acuerdo a lo definido para 
    '%					 el producto de SOAT
    '-----------------------------------------------------------------------------------------
    Private Sub ChangeValuesSOAT()
        '-----------------------------------------------------------------------------------------
        Dim lclsProduct As eProduct.Product
        Dim lbldisabled As Boolean
        Dim lclsPremium As eCollection.Premium
        Dim lblnPayreceipt As Boolean

        lblnPayreceipt = False
        lbldisabled = True

        lclsProduct = New eProduct.Product
        lclsPremium = New eCollection.Premium

        If lclsProduct.FindProdMaster(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
            lbldisabled = CStr(lclsProduct.sBrancht) <> "9"
        End If

        Select Case Request.QueryString.Item("sCodispl")
            Case "CA033"
                If lbldisabled Then
                    Response.Write("top.frames['fraFolder'].document.forms[0].chkNullRequest.checked=false;")
                    Response.Write("top.frames['fraFolder'].document.forms[0].chkNullReport.checked=true;")
                    Response.Write("top.frames['fraFolder'].document.forms[0].optDev[0].checked=false;")
                    'Response.Write "top.frames['fraFolder'].document.forms[0].optReceipt[2].disabled=false;"
                Else
                    Response.Write("top.frames['fraFolder'].document.forms[0].chkNullRequest.checked=false;")
                    Response.Write("top.frames['fraFolder'].document.forms[0].chkNullReport.checked=false;")
                    Response.Write("top.frames['fraFolder'].document.forms[0].optDev[0].checked=true;")
                    Response.Write("top.frames['fraFolder'].document.forms[0].optReceipt[2].disabled=true;")
                End If

            Case "CA033_K"
                If lbldisabled Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].optExecute[0].checked=true;")
                Else
                    Response.Write("top.frames['fraHeader'].document.forms[0].optExecute[1].checked=true;")
                End If

                Call ValPolitype()

                'Case "CA037"	
                '	If lclsPremium.FindCA037(mclsValues.StringToType(Request.QueryString("nBranch"),eFunctions.Values.eTypeData.etdLong), 		'							 mclsValues.StringToType(Request.QueryString("nProduct"),eFunctions.Values.eTypeData.etdLong), 		'							 mclsValues.StringToType(Request.QueryString("nPolicy"),eFunctions.Values.eTypeData.etdDouble), 		'							 mclsValues.StringToType(Request.QueryString("nCertif"),eFunctions.Values.eTypeData.etdDouble)) Then
                '		lblnPayreceipt= True				
                '	Else
                '		lblnPayreceipt = False
                '	End If
                '+ Si la página que invoca la función es la CA037- Cambio de fecha de efecto
                '	With Response
                '		If Not lbldisabled Then
                '			.Write "if(top.frames['fraHeader'].document.forms[0].optReceiptType!=null){ top.frames['fraHeader'].document.forms[0].optReceiptType[0].disabled=true;"
                '			.Write "top.frames['fraHeader'].document.forms[0].optReceiptType[1].disabled=true;"
                '			.Write "top.frames['fraHeader'].document.forms[0].optReceiptType[2].disabled=false;"
                '			.Write "top.frames['fraHeader'].document.forms[0].optReceiptType[2].checked=true;}"
                '		Else
                '			If lblnPayreceipt Then
                '				.Write "if(top.frames['fraHeader'].document.forms[0].optReceiptType!=null){ top.frames['fraHeader'].document.forms[0].optReceiptType[1].disabled=true;"
                '				.Write "top.frames['fraHeader'].document.forms[0].optReceiptType[2].disabled=true;}"
                '			Else
                '				.Write "if(top.frames['fraHeader'].document.forms[0].optReceiptType!=null){ top.frames['fraHeader'].document.forms[0].optReceiptType[0].disabled=true;"
                '				.Write "top.frames['fraHeader'].document.forms[0].optReceiptType[1].disabled=false;"
                '				.Write "top.frames['fraHeader'].document.forms[0].optReceiptType[2].disabled=false;"
                '				.Write "top.frames['fraHeader'].document.forms[0].optReceiptType[2].checked=true;}"
                '			End If
                '		End If			
                '	End With
        End Select

        lclsProduct = Nothing
        lclsPremium = Nothing
    End Sub

    Sub insShowExpenses()
        '--------------------------------------------------------------------------------------------
        Dim lclsNoconvers As ePolicy.Noconvers

        lclsNoconvers = New ePolicy.Noconvers

        If lclsNoconvers.Find_CA099(mclsValues.StringToType(Request.QueryString.Item("nNoConvers"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dStat_date"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Request.QueryString.Item("dLimit_date"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Request.QueryString.Item("dDate_init"), eFunctions.Values.eTypeData.etdDate)) Then
            If lclsNoconvers.nExpenses > 0 Then
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnCollect.value=VTFormat('" & lclsNoconvers.nExpenses & "', '', '', '', 6, false);")
            Else
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnCollect.value=VTFormat((0),'', '', '',6,true);")
            End If
            If lclsNoconvers.nHealthexp > 0 Then
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnGastMed.value=VTFormat('" & lclsNoconvers.nHealthexp & "', '', '', '', 6, false);")
            Else
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnGastMed.value=VTFormat((0),'', '', '',6,true);")
            End If
            If lclsNoconvers.nRoutine > 0 Then
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnGastProv.value=VTFormat('" & lclsNoconvers.nRoutine & "', '', '', '', 6, false);")
            Else
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnGastProv.value=VTFormat((0),'', '', '',6,true);")
            End If
        End If
        lclsNoconvers = Nothing
    End Sub

    '% inssApv: Extrae la condición de APV de un producto (Se valida si es o no APV)
    '--------------------------------------------------------------------------------------------
    Sub inssApv()
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct_li As eProduct.Product

        lclsProduct_li = New eProduct.Product

        With lclsProduct_li

            If .FindProduct_li(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

                Response.Write("top.frames['fraHeader'].document.forms[0].hsApv.value ='" & lclsProduct_li.sAPV & "';")

            End If
        End With
        lclsProduct_li = Nothing
    End Sub

    '% insGetPaymentLocations: 
    '--------------------------------------------------------------------------------------------
    Sub insGetPaymentLocations()
        '--------------------------------------------------------------------------------------------
        Dim oPaymentInfo As eAgent.Agencie

        oPaymentInfo = New eAgent.Agencie

        With oPaymentInfo

            If .FindPaymentAgency(mclsValues.StringToType(Request.QueryString.Item("cbeOffice"), eFunctions.Values.eTypeData.etdLong)) Then

                Response.Write("top.frames['fraHeader'].insPopulatePFields(0" & .nBran_Off & "," & .nOfficeAgen & "," & .nAgency & ",'" & .sOfficeAgenDesc & "','" & .sAgencyDesc & "');")

            Else

                Response.Write("top.frames['fraHeader'].insBlankPFields(" & Request.QueryString.Item("cbeOffice") & ");")

            End If
        End With
        oPaymentInfo = Nothing
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub insDisabledInsurRecord()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        lclsPolicy = New ePolicy.Policy
        With Request
            If lclsPolicy.insDisabledInsurRecord("2", mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
                Response.Write("top.fraHeader.document.forms[0].chkInsur.disabled=true;")
                Response.Write("top.fraHeader.document.forms[0].chkInsur.checked=false;")
                Response.Write("top.fraHeader.nMainInsur = 1 ;")
            Else
                Response.Write("top.fraHeader.document.forms[0].chkInsur.disabled=false;")
                Response.Write("top.fraHeader.document.forms[0].chkInsur.checked=false;")
                Response.Write("top.fraHeader.nMainInsur = 0 ;")
            End If
        End With
        lclsPolicy = Nothing
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub InsNexchangeChange()
        '--------------------------------------------------------------------------------------------
        Dim lclsExchange As eGeneral.Exchange
        Dim nlocalSurrAmount As Double
        lclsExchange = New eGeneral.Exchange
        With lclsExchange
            If lclsExchange.Find(4, mclsValues.StringToType(Request.QueryString.Item("dPaydate"), eFunctions.Values.eTypeData.etdDate)) Then

                nlocalSurrAmount = mclsValues.StringToType(Request.QueryString.Item("nRequestedSurrAmt"), eFunctions.Values.eTypeData.etdDouble) * lclsExchange.nExchange

                Response.Write("top.frames['fraFolder'].document.forms[0].tcnUFValue.value = VTFormat('" & lclsExchange.nExchange & "', '', '', '', 2, false);")
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnLocalSurrAmt.value = VTFormat('" & nlocalSurrAmount & "', '', '', '', 0, false);")
            End If
        End With
        lclsExchange = Nothing
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub InsNexchangeVI009()
        '--------------------------------------------------------------------------------------------
        Dim lclsExchange As eGeneral.Exchange
        Dim nlocalSurrAmount As Double
        lclsExchange = New eGeneral.Exchange
        With lclsExchange
            If lclsExchange.Find(4, mclsValues.StringToType(Request.QueryString.Item("dPaydate"), eFunctions.Values.eTypeData.etdDate)) Then

                nlocalSurrAmount = mclsValues.StringToType(Request.QueryString.Item("nRequestedSurrAmt"), eFunctions.Values.eTypeData.etdDouble) * lclsExchange.nExchange

                Response.Write("top.frames['fraFolder'].document.forms[0].tcnUFValue.value = VTFormat('" & lclsExchange.nExchange & "', '', '', '', 2, false);")
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnSurrCurr.value = VTFormat('" & nlocalSurrAmount & "', '', '', '', 0, false);")
            End If
        End With
        lclsExchange = Nothing
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub InsChangeTyp_Profitworker()
        '--------------------------------------------------------------------------------------------
        Dim lclsRequest As ePolicy.Funds_pols
        lclsRequest = New ePolicy.Funds_pols
        With lclsRequest

            If lclsRequest.Find_Request_VI016("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble)) Then

                Response.Write("top.frames['fraFolder'].document.forms[0].tcnAvailable.value = VTFormat('" & lclsRequest.nSellsTot & "', '', '', '', 6, false);")

            End If
        End With
        lclsRequest = Nothing
    End Sub
    '--------------------------------------------------------------------------------------------
    Private Sub InsSwitch_Del()
        '--------------------------------------------------------------------------------------------
        Dim lclsRequest As ePolicy.Funds_Pol
        lclsRequest = New ePolicy.Funds_Pol
        With lclsRequest

            Call lclsRequest.DelProp_pend("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
            Response.Write("top.frames['fraFolder'].document.location.reload();")
        End With
        lclsRequest = Nothing
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub ChangeValuesRever()
        '--------------------------------------------------------------------------------------------
        Dim lclsRequest As eBatch.Tmp_undo_move_acc
        lclsRequest = New eBatch.Tmp_undo_move_acc
        With lclsRequest
            If lclsRequest.FindValueRequestReverse(Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mclsValues.StringToType(Request.QueryString.Item("dOperdat"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Request.QueryString.Item("nType_move"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProfitworker"), eFunctions.Values.eTypeData.etdDouble)) Then
                Response.Write("top.frames['fraFolder'].document.forms[0].tcncreditmanual.value = VTFormat('" & lclsRequest.nDebit & "', '', '', '', 6, false);")
            End If
        End With
        lclsRequest = Nothing
    End Sub

    '% CheckCreClient: Verifica si el cliente se encuentra previamente registrado para recuperar
    '%                 sus datos; en caso contrario es generado automáticamente.
    '--------------------------------------------------------------------------------------------
    Private Sub CheckCreClient()
        '--------------------------------------------------------------------------------------------
        Dim lclsClient As eClient.Client
        Dim lstrClient As String
        lclsClient = New eClient.Client

        lstrClient = lclsClient.ExpandCode(Request.QueryString.Item("sClient"))

        With Response
            .Write("top.fraHeader.document.forms[0].tctFatherLastName.value='';")
            .Write("top.fraHeader.document.forms[0].tctMotherLastName.value='';")
            .Write("top.fraHeader.document.forms[0].tctNames.value='';")
            '.Write("top.fraHeader.document.forms[0].dtcBirthdayDate.value='';")
            If Not lclsClient.Find(lstrClient) Then
                .Write("top.fraHeader.document.forms[0].tctFatherLastName.disabled=false;")
                .Write("top.fraHeader.document.forms[0].tctMotherLastName.disabled=false;")
                .Write("top.fraHeader.document.forms[0].tctNames.disabled=false;")
                '.Write("top.fraHeader.document.forms[0].dtcBirthdayDate.disabled=false;")
            Else
                .Write("top.fraHeader.document.forms[0].tctFatherLastName.disabled=false;")
                .Write("top.fraHeader.document.forms[0].tctMotherLastName.disabled=false;")
                .Write("top.fraHeader.document.forms[0].tctNames.disabled=false;")
                '.Write("top.fraHeader.document.forms[0].dtcBirthdayDate.disabled=false;")

                .Write("top.fraHeader.document.forms[0].tctFatherLastName.value='" & Replace(lclsClient.sLastName, "'", "´") & "';")
                .Write("top.fraHeader.document.forms[0].tctMotherLastName.value='" & Replace(lclsClient.sLastname2, "'", "´") & "';")
                .Write("top.fraHeader.document.forms[0].tctNames.value='" & Replace(lclsClient.sFirstName, "'", "´") & "';")
                '.Write("top.fraHeader.document.forms[0].dtcBirthdayDate.value='" & mclsValues.TypeToString(lclsClient.dBirthdat, eFunctions.Values.eTypeData.etdDate) & "';")
            End If
        End With
        lclsClient = Nothing

    End Sub

    '% insShowfolio: Ubica los datos de la póliza a partir del número de folio.
    '--------------------------------------------------------------------------------------------
    Public Sub insShowfolio()
        '--------------------------------------------------------------------------------------------
        Dim lclsSoap_entry As ePolicy.Soap_entry
        Dim ldblFolio As Double
        lclsSoap_entry = New ePolicy.Soap_entry
        Dim ef As New eFunctions.Values

        ldblFolio = mclsValues.StringToType(Request.QueryString.Item("nFolio"), eFunctions.Values.eTypeData.etdDouble)

        With Response

            If lclsSoap_entry.FindFolio(ldblFolio) And ldblFolio <> 0 Then

                'Validación de captura incompleta en los folios
                If lclsSoap_entry.nWaitCode = 1 Then
                    lclsSoap_entry.nWaitCode = eRemoteDB.intNull
                End If

                If lclsSoap_entry.sStatusva <> "3" Then
                    .Write("alert('Folio está en estado " & lclsSoap_entry.sStatusDescription & ", verifique');")
                End If

                'Datos del Folio generado
                .Write("top.fraHeader.document.forms[0].cbeBranch.value='" & lclsSoap_entry.nBranch & "';")
                .Write("top.fraHeader.document.forms[0].valProduct.value='" & lclsSoap_entry.nProduct & "';")
                .Write("top.fraHeader.UpdateDiv('valProductDesc','" & lclsSoap_entry.sProduct & "','');")
                .Write("top.fraHeader.document.forms[0].tcnPolicy.value='" & lclsSoap_entry.nPolicy & "';")
                .Write("top.fraHeader.document.forms[0].tcnCertif.value='" & lclsSoap_entry.nCertif & "';")
                .Write("top.fraHeader.document.forms[0].valIntermed.value='" & lclsSoap_entry.nIntermed & "';")
                .Write("top.fraHeader.$('#valIntermed').change();")

                .Write("top.fraHeader.document.forms[0].valAgreement.Parameters.Param1.sValue=" & lclsSoap_entry.nIntermed & ";")



                .Write("top.fraHeader.document.forms[0].valCausal.value='" & lclsSoap_entry.nWaitCode & "';")

                If lclsSoap_entry.sStatusva <> "3" Then

                    .Write("top.fraHeader.document.forms[0].valAgreement.value='" & lclsSoap_entry.nAgreement & "';")
                    .Write("top.fraHeader.$('#valAgreement').change();")


                    .Write("top.fraHeader.document.forms[0].tctType.value='" & lclsSoap_entry.NVEHGROUP & "';")
                    .Write("top.fraHeader.document.forms[0].tcdStartDate.value='" & lclsSoap_entry.dStartDate & "';")
                    'Else

                    '    insShowTypeVeh()
                End If
                .Write("top.fraHeader.document.forms[0].hddYear.value='" & lclsSoap_entry.dStartDate.Year & "';")


                If lclsSoap_entry.nWaitCode > 0 Then
                    If lclsSoap_entry.nWaitCode = 13 Then
                        .Write("top.fraHeader.document.forms[0].chkAcchsend_ind.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].valCausal.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tcnCollectedPremium.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].cbeLicense_ty.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctRegist.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctDigit.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].ValVehMark.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].ValVehModel.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].btnValVehModel.disabled=true;")
                        .Write("top.fraHeader.document.forms[0].tcnYear.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctMotor.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctChassis.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctColor.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].dtcClient.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctFatherLastName.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctMotherLastName.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctNames.disabled = true;")
                    Else
                        .Write("top.fraHeader.document.forms[0].chkAcchsend_ind.disabled = false;")
                        .Write("top.fraHeader.document.forms[0].cbeLicense_ty.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctRegist.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctDigit.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].ValVehMark.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].ValVehModel.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].btnValVehModel.disabled=true;")
                        .Write("top.fraHeader.document.forms[0].tcnYear.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctMotor.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctChassis.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctColor.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].dtcClient.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctFatherLastName.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctMotherLastName.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].tctNames.disabled = true;")
                        .Write("top.fraHeader.document.forms[0].valCausal.disabled = false;")
                        .Write("top.fraHeader.document.forms[0].tcnCollectedPremium.disabled = true;")
                    End If
                ElseIf lclsSoap_entry.nWaitCode <= 0 Or lclsSoap_entry.nWaitCode = eRemoteDB.intNull Then
                    .Write("top.fraHeader.document.forms[0].chkAcchsend_ind.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].cbeLicense_ty.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tctRegist.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tctDigit.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].ValVehMark.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].ValVehModel.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].btnValVehModel.disabled=false;")
                    .Write("top.fraHeader.document.forms[0].tcnYear.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tctMotor.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tctChassis.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tctColor.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].dtcClient.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tctFatherLastName.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tctMotherLastName.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tctNames.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].valCausal.disabled = false;")
                    .Write("top.fraHeader.document.forms[0].tcnCollectedPremium.disabled = false;")
                End If

                .Write("top.fraHeader.document.forms[0].hddStatusva.value='" & lclsSoap_entry.sStatusva & "';")
                .Write("top.fraHeader.document.forms[0].cbeModule.Parameters.Param1.sValue=" & lclsSoap_entry.sCertype & ";")
                .Write("top.fraHeader.document.forms[0].cbeModule.Parameters.Param2.sValue=" & lclsSoap_entry.nBranch & ";")
                .Write("top.fraHeader.document.forms[0].cbeModule.Parameters.Param3.sValue=" & lclsSoap_entry.nProduct & ";")
                .Write("top.fraHeader.document.forms[0].cbeModule.Parameters.Param4.sValue=" & lclsSoap_entry.nPolicy & ";")
                .Write("top.fraHeader.document.forms[0].cbeModule.Parameters.Param5.sValue=" & lclsSoap_entry.nCertif & ";")

                If lclsSoap_entry.sStatusva <> "3" Then
                    .Write("top.fraHeader.document.forms[0].cbeModule.Parameters.Param6.sValue='" & lclsSoap_entry.dStartDate & "';")
                Else
                    .Write("top.fraHeader.document.forms[0].cbeModule.Parameters.Param6.sValue='" & Date.Today & "';")
                End If

                .Write("top.fraHeader.document.forms[0].cbeModule.Parameters.Param7.sValue=0;")
                .Write("top.fraHeader.document.forms[0].cbeModule.value=" & lclsSoap_entry.nModule & ";")
                .Write("top.fraHeader.$('#cbeModule').change();")
                .Write("top.fraHeader.document.forms[0].valStatusva.value='" & lclsSoap_entry.sStatusva & "';")
                .Write("top.fraHeader.document.forms[0].tctMotor.value='" & lclsSoap_entry.sMotor & "';")

                If Not String.IsNullOrEmpty(lclsSoap_entry.SLICENSE_TY) Then
                    .Write("top.fraHeader.document.forms[0].cbeLicense_ty.value='" & lclsSoap_entry.SLICENSE_TY & "';")
                Else
                    .Write("top.fraHeader.document.forms[0].cbeLicense_ty.value='1';")
                End If

                .Write("top.fraHeader.document.forms[0].tctRegist.value='" & lclsSoap_entry.sRegist & "';")
                .Write("top.fraHeader.document.forms[0].dtcClient.value='" & lclsSoap_entry.sClient & "';")
                .Write("top.fraHeader.document.forms[0].tctFatherLastName.value='" & lclsSoap_entry.sLastname & "';")
                .Write("top.fraHeader.document.forms[0].tctMotherLastName.value='" & lclsSoap_entry.sLastname2 & "';")
                .Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='" & lclsSoap_entry.sDigit & "';")
                .Write("top.fraHeader.document.forms[0].tctNames.value='" & lclsSoap_entry.sFirstname & "';")
                .Write("top.fraHeader.document.forms[0].tctChassis.value='" & lclsSoap_entry.sChassis & "';")
                .Write("top.fraHeader.document.forms[0].tctColor.value='" & lclsSoap_entry.sColor & "';")
                .Write("top.fraHeader.document.forms[0].tcnCollectedPremium.value='" & ef.TypeToString(lclsSoap_entry.NCOLLECTED_PREM, Values.eTypeData.etdDouble) & "';")
                .Write("top.fraHeader.document.forms[0].tcnYear.value='" & ef.TypeToString(lclsSoap_entry.nYear, Values.eTypeData.etdInteger) & "';")
                .Write("top.fraHeader.document.forms[0].ValVehMark.value='" & lclsSoap_entry.nVehBrand & "';")
                .Write("top.fraHeader.document.forms[0].ValVehModel.value='" & lclsSoap_entry.sVehcode & "';")
                .Write("top.fraHeader.$('#ValVehModel').change();")

                If lclsSoap_entry.sVehcode = "9999" Then
                    .Write("top.fraHeader.showManualMakeAndModelTR();")
                    .Write("top.fraHeader.document.forms[0].tctMark.value='" & lclsSoap_entry.sVehmake & "';")
                    .Write("top.fraHeader.document.forms[0].tctModel.value='" & lclsSoap_entry.sVehmodel & "';")
                    .Write("top.fraHeader.document.forms[0].btnValVehModel.disabled=true;")
                    .Write("top.fraHeader.document.forms[0].ValVehModel.disabled=true;")
                    .Write("UpdateDiv('ValVehModelDesc', 'Otros');")
                Else
                    .Write("top.fraHeader.document.forms[0].tctMark.value='';")
                    .Write("top.fraHeader.document.forms[0].tctModel.value='';")
                    .Write("top.fraHeader.hideManualMakeAndModelTR();")
                End If

                .Write("top.fraHeader.document.forms[0].tctMistakenDigit.value='" & lclsSoap_entry.SMISTAKENDIGIT & "';")
                .Write("top.fraHeader.document.forms[0].tctDigitalLink.value='" & lclsSoap_entry.SDIGITALIZEDURL & "';")
                .Write("top.fraHeader.document.forms[0].tctDigit.value='" & lclsSoap_entry.SAUTO_DIGIT & "';")



                .Write("top.fraHeader.document.forms[0].cbeModule.value=11;")

                If lclsSoap_entry.sAcchsend_ind = "1" Then

                    .Write("top.fraHeader.document.forms[0].chkAcchsend_ind.checked=true;")
                Else
                    .Write("top.fraHeader.document.forms[0].chkAcchsend_ind.checked=false;")
                End If

                If lclsSoap_entry.sStatusva <> "3" Then
                    Call insChangeStartDate(CStr(lclsSoap_entry.dStartDate))
                End If

            Else
                .Write("alert('Folio no existe');")
                .Write("top.fraHeader.document.forms[0].cbeBranch.value='';")
                .Write("top.fraHeader.document.forms[0].valProduct.value='';")
                .Write("top.fraHeader.UpdateDiv('valProductDesc','');")
                .Write("top.fraHeader.UpdateDiv('ValVehModelDesc','');")
                .Write("top.fraHeader.document.forms[0].tcnPolicy.value='';")
                .Write("top.fraHeader.document.forms[0].tcnCertif.value='';")
                .Write("top.fraHeader.document.forms[0].valIntermed.value='';")
                .Write("top.fraHeader.document.forms[0].valCausal.value='';")
                .Write("top.fraHeader.document.forms[0].tcdStartDate.value='';")
                .Write("top.fraHeader.document.forms[0].tcdExpirDate.value='';")
                .Write("top.fraHeader.document.forms[0].valStatusva.value='';")
                .Write("top.fraHeader.document.forms[0].cbeModule.value='';")
                .Write("top.fraHeader.document.forms[0].tctNames.value='';")
                .Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='';")
                .Write("top.fraHeader.document.forms[0].tctMotherLastName.value='';")
                .Write("top.fraHeader.document.forms[0].tctFatherLastName.value='';")
                .Write("top.fraHeader.document.forms[0].dtcClient.value='';")
                .Write("top.fraHeader.document.forms[0].tctRegist.value='';")
                .Write("top.fraHeader.document.forms[0].tctChassis.value='';")
                .Write("top.fraHeader.document.forms[0].tctColor.value='';")
                .Write("top.fraHeader.document.forms[0].tctDigit.value = '';")
                .Write("top.fraHeader.document.forms[0].tctMistakenDigit.value = '';")
                .Write("top.fraHeader.hideManualMakeAndModelTR();")
                .Write("top.fraHeader.document.forms[0].tcnCollectedPremium.value = '';")
                .Write("top.fraHeader.document.forms[0].tctType.value = '';")
                .Write("top.fraHeader.UpdateDiv('cbeModuleDesc','');")
                .Write("top.fraHeader.UpdateDiv('valIntermedDesc','');")
                .Write("top.fraHeader.document.forms[0].ValVehMark.value = '';")
                .Write("top.fraHeader.document.forms[0].ValVehModel.value = '';")
                .Write("top.fraHeader.document.forms[0].tcnYear.value = '';")
                .Write("top.fraHeader.document.forms[0].tctMotor.value = '';")
                .Write("top.fraHeader.document.forms[0].tctMark.value='';")
                .Write("top.fraHeader.document.forms[0].tctModel.value='';")
                .Write("top.fraHeader.document.forms[0].hddStatusva.value='';")
                .Write("top.fraHeader.document.forms[0].hddStatusva.value='';")
                .Write("top.fraHeader.document.forms[0].hddStatusva.value='';")
                .Write("top.fraHeader.document.forms[0].chkAcchsend_ind.value='';")
                .Write("top.fraHeader.document.forms[0].chkAcchsend_ind.checked=false;")
            End If
        End With
        lclsSoap_entry = Nothing

    End Sub

    '% ReaMunicipalityt: Busca la ciudad y la región dada la comuna
    '--------------------------------------------------------------------------------------------
    Public Sub ReaMunicipality()
        '--------------------------------------------------------------------------------------------
        Dim lclsTab_locat As eGeneralForm.Tab_locat
        lclsTab_locat = New eGeneralForm.Tab_locat
        With lclsTab_locat

            If Not IsNothing(Request.QueryString.Item("nMunicipality")) Then
                If .Find_by_municipality(mclsValues.StringToType(Request.QueryString.Item("nMunicipality"), eFunctions.Values.eTypeData.etdDouble)) Then

                    Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
                    Response.Write("    valLocal.value='" & .nLocal & "';")
                    Response.Write("    top.frames['fraHeader'].$('#valLocal').change();")
                    Response.Write("    cbeProvince.value='" & .nProvince & "';")
                    Response.Write("}")
                Else

                    Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
                    Response.Write("    valLocal.value='';")
                    Response.Write("    cbeProvince.value='';")
                    Response.Write("}")
                    Response.Write("    top.frames['fraHeader'].UpdateDiv('valLocalDesc','');")
                End If
            End If
        End With
        lclsTab_locat = Nothing
    End Sub


    '% insChangeStartDate: Calcula la fecha de vencimiento a partir de la fecha de inicio.
    '--------------------------------------------------------------------------------------------
    Public Sub insChangeStartDate(dStartDate)
        '--------------------------------------------------------------------------------------------
        Dim DateResul As Date
        Dim initDate As Date

        If dStartDate = String.Empty Then
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirDate.value ='';")
        Else
            initDate = mclsValues.SumTypeDate("m", 12, mclsValues.StringToType(dStartDate, eFunctions.Values.eTypeData.etdDate)).AddDays(-1)
            DateResul = New Date(initDate.Year, 3, 31)
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirDate.value ='" & mclsValues.TypeToString(DateResul, eFunctions.Values.eTypeData.etdDate) & "';")
        End If
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub insShowProp()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim mobjValues As eFunctions.Values
        Dim lclsOpt_system As Object
        Dim lstrCertype As String
        Dim lstrPolicy As Object

        lclsPolicy = New ePolicy.Policy
        mobjValues = New eFunctions.Values
        If Request.QueryString.Item("nPolicy") = "" Then
            lstrCertype = "1"
            lstrPolicy = mobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)
        ElseIf Request.QueryString.Item("nProponum") = "" Then
            lstrCertype = "2"
            lstrPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
        End If

        If lclsPolicy.FindPolicyOptSystem(lstrCertype, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), lstrPolicy) Then
            Response.Write("top.frames['fraFolder'].document.forms[0].valProductnew.value=" & lclsPolicy.nProduct & ";")
            If lclsPolicy.nProduct <> CDbl("") Then
                Response.Write("top.frames['fraFolder'].$('#valProductnew').change();")
                Response.Write("top.frames['fraFolder'].document.forms[0].valProductnew.disabled=true;")
                Response.Write("top.frames['fraFolder'].document.forms[0].btnvalProductnew.disabled=true;")
            End If
        Else
            If lstrCertype = "1" Then
                Response.Write("alert('El registro debe corresponder a una propuesta de emisión');")
                Response.Write("top.frames['fraFolder'].document.forms[0].tcnProponumnew.value='';")
                Response.Write("top.frames['fraFolder'].document.forms[0].valProductnew.value='';")
                Response.Write("top.frames['fraFolder'].UpdateDiv('valProductnewDesc','','');")
            ElseIf lstrCertype = "2" Then
                Response.Write("alert('El registro debe corresponder a una póliza');")
            End If
        End If

        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub insShowSaapv()
        '--------------------------------------------------------------------------------------------
        Dim mobjSaapv As eSaapv.Saapv
        Dim mobjValues As eFunctions.Values

        mobjSaapv = New eSaapv.Saapv
        mobjValues = New eFunctions.Values

        '+ se agrego este manejo para el numero unico de poliza
        If mobjSaapv.Find(mobjValues.StringToType(Request.QueryString.Item("ncod_saapv"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nInstitution"), eFunctions.Values.eTypeData.etdLong)) Then

            Response.Write("opener.document.forms[0].cbestatus_saapv.value=" & mobjSaapv.nstatus_saapv & ";")
            Response.Write("opener.document.forms[0].cbeType_saapv.value=" & mobjSaapv.nType_saapv & ";")
            Response.Write("opener.document.forms[0].tcdissue_dat.value='" & mclsValues.TypeToString(mobjSaapv.dissue_dat, eFunctions.Values.eTypeData.etdDate) & "';")
            Response.Write("opener.document.forms[0].tcdLimitDate.value='" & mclsValues.TypeToString(mobjSaapv.dLimitDate, eFunctions.Values.eTypeData.etdDate) & "';")

            If mobjSaapv.ntype_ameapv > 0 Then
                Response.Write("opener.document.forms[0].cbetype_ameapv.value=" & mobjSaapv.ntype_ameapv & ";")
            End If
            If mobjSaapv.nInstitution > 0 Then
                Response.Write("opener.document.forms[0].valInstitution.value=" & mobjSaapv.nInstitution & ";")
            End If

            If mobjSaapv.sCertype <> "2" Then
                Response.Write("opener.document.forms[0].optCertype[0].checked=true;")
            Else
                Response.Write("opener.document.forms[0].optCertype[1].checked=true;")
            End If

            If mobjSaapv.nPolicy > 0 Then

                Response.Write("opener.document.forms[0].tcnPolicy.value=" & mobjSaapv.nPolicy & ";")
                Response.Write("opener.document.forms[0].tcnPolicy.disabled=true;")
                Response.Write("opener.document.forms[0].optCertype[0].disabled=true;")
                Response.Write("opener.document.forms[0].optCertype[1].disabled=true;")
            Else
                Response.Write("opener.document.forms[0].tcnPolicy.disabled=false;")
                Response.Write("opener.document.forms[0].optCertype[0].disabled=false;")
                Response.Write("opener.document.forms[0].optCertype[1].disabled=false;")
            End If

            If mobjSaapv.nProduct > 0 Then
                Response.Write("opener.document.forms[0].cbeBranch.value=" & mobjSaapv.nBranch & ";")
                Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue=" & mobjSaapv.nBranch & ";")
                Response.Write("opener.document.forms[0].valProduct.value=" & mobjSaapv.nProduct & ";")
                Response.Write("opener.$('#valProduct').change();")

                Response.Write("opener.document.forms[0].cbeBranch.disabled=true;")
                Response.Write("opener.document.forms[0].valProduct.disabled=true;")
            Else
                Response.Write("opener.document.forms[0].cbeBranch.disabled=false;")
                Response.Write("opener.document.forms[0].valProduct.disabled=false;")
            End If

            Response.Write("opener.document.forms[0].cbeType_saapv.disabled=true;")
            Response.Write("opener.document.forms[0].tcdissue_dat.disabled=true;")
            Response.Write("opener.document.forms[0].cbetype_ameapv.disabled=true;")
            Response.Write("opener.document.forms[0].valInstitution.disabled=true;")
        Else
            Response.Write("opener.document.forms[0].cbestatus_saapv.value=1;")
            Response.Write("opener.document.forms[0].cbeBranch.value='';")
            Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue='';")
            Response.Write("opener.document.forms[0].valProduct.value='';")
            Response.Write("opener.$('#valProduct').change();")
            'Response.Write "opener.document.forms[0].cbeType_saapv.value='';"
            Response.Write("opener.document.forms[0].tcdissue_dat.value='';")
            Response.Write("opener.document.forms[0].cbetype_ameapv.value='';")
            'Response.Write "opener.document.forms[0].valInstitution.value='';"
            Response.Write("opener.document.forms[0].tcnPolicy.value='';")
            Response.Write("opener.document.forms[0].optCertype[1].checked=true;")

            Response.Write("opener.document.forms[0].cbeBranch.disabled=false;")
            'Response.Write "opener.document.forms[0].valProduct.disabled=false;"
            'Response.Write "opener.document.forms[0].cbeType_saapv.disabled=false;"
            Response.Write("opener.document.forms[0].tcdissue_dat.disabled=false;")

            'Response.Write "opener.document.forms[0].valInstitution.disabled=false;"
            Response.Write("opener.document.forms[0].tcnPolicy.disabled=false;")
            Response.Write("opener.document.forms[0].optCertype[0].disabled=false;")
            Response.Write("opener.document.forms[0].optCertype[1].disabled=false;")

        End If
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
        'UPGRADE_NOTE: Object mobjSaapv may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjSaapv = Nothing
    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub insLimitDate()
        '--------------------------------------------------------------------------------------------
        Dim mobjSaapv As eSaapv.Saapv
        Dim mobjValues As eFunctions.Values
        Dim dLimitDate As Date
        Dim dissue_dat As Date
        Dim nType_saapv As Integer

        mobjValues = New eFunctions.Values
        mobjSaapv = New eSaapv.Saapv

        nType_saapv = mobjValues.StringToType(Request.QueryString.Item("nType_saapv"), eFunctions.Values.eTypeData.etdInteger)
        dissue_dat = mobjValues.StringToType(Request.QueryString.Item("dissue_dat"), eFunctions.Values.eTypeData.etdDate)

        '+ se asigna la fecha limite del saapv
        dLimitDate = mobjSaapv.LimitDate(dissue_dat, nType_saapv)

        Response.Write("top.frames['fraHeader'].document.forms[0].tcdLimitDate.value='" & dLimitDate & "';")

        'UPGRADE_NOTE: Object mobjSaapv may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjSaapv = Nothing
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
    End Sub


    '% InsShowClientData: Muestra los datos del cliente
    '--------------------------------------------------------------------------------------------
    Sub InsShowClientDataA()
        '--------------------------------------------------------------------------------------------
        Dim lclsClient_Saapv As eSaapv.Saapv
        Dim lclsClient As eClient.Client
        Dim lstrClient As Object
        Dim lstrSexClien As Object
        Dim ldtmBirthdat As Object
        Dim lblnOk As Boolean
        Dim mobjValues As eFunctions.Values

        lclsClient = New eClient.Client
        lclsClient_Saapv = New eSaapv.Saapv
        mobjValues = New eFunctions.Values

        lstrClient = Request.QueryString.Item("sClient")
        If Len(CStr(lstrClient)) Then
            lstrClient = lclsClient.ExpandCode(lstrClient)
        End If


        With lclsClient_Saapv
            If .Find_insure(0, "0", 0, 0, 0, 0, lstrClient, 0) Then
                lblnOk = True
                'lstrSexClien = .sSexclien
                'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                ldtmBirthdat = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeSex.value=" & .sSexclien & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].tcdBirthDate.value='" & mobjValues.TypeToString(.dBirthDat, eFunctions.Values.eTypeData.etdDate) & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeCivilsta.value=" & .nCivilSta & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeOccupat.value=" & .nSpeciality & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeNationality.value=" & .nNationality & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctdescadd.value='" & .sDescAdd & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeMunicipality.value=" & .nMunicipality & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeLocal.value=" & .nLocal & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeProvince.value=" & .nProvince & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctEmail.value='" & .sSe_mail & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctPhone1.value='" & .sPhone_pa & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctPhone2.value='" & .sPhone_co & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctPhone3.value='" & .sPhone_ce & "';")
            End If
        End With

        '+ Si se consigió información
        If lblnOk Then
            'Response.Write "top.frames['fraFolder'].document.forms[0].tcdBirthDate.value=" & ldtmBirthdat & ";"


            '			
        End If

        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClient = Nothing
        lclsClient_Saapv = Nothing
    End Sub

    Sub InsShowClientDataB()
        '--------------------------------------------------------------------------------------------
        Dim lclsClient_Saapv As eSaapv.Saapv
        Dim lclsClient As eClient.Client

        Dim lstrClient As Object
        Dim lstrSexClien As Object
        Dim ldtmBirthdat As Object
        Dim lblnOk As Boolean
        Dim mobjValues As eFunctions.Values

        lclsClient = New eClient.Client
        lclsClient_Saapv = New eSaapv.Saapv

        mobjValues = New eFunctions.Values

        lstrClient = Request.QueryString.Item("sClient")

        If Len(CStr(lstrClient)) Then
            lstrClient = lclsClient.ExpandCode(lstrClient)
        End If


        With lclsClient_Saapv
            'If .Find_employ(Session(nCod_saapv"),"0",0,0,0,0,lstrClient) Then
            If .Find_Employ(Request.QueryString.Item("ncod_saapv"), "0", 0, 0, 0, 0, lstrClient, 0) Then
                lblnOk = True
                'lstrSexClien = .sSexclien
                'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                ldtmBirthdat = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
                Response.Write("top.frames['fraFolder'].document.forms[0].tctlegalname.value='" & Replace(.sLegalname, "'", "\'") & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctdescadd.value='" & .sDescAdd & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeMunicipality.value=" & .nMunicipality & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeLocal.value=" & .nLocal & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbeProvince.value=" & .nProvince & ";")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctname.value='" & .sRrhh_name & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctse_mail.value='" & .sRrhh_email & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].tctphone.value='" & .sRrhh_phone & "';")
                Response.Write("top.frames['fraFolder'].document.forms[0].tcdRecepdat.value='" & mobjValues.TypeToString(.dRecepDat, eFunctions.Values.eTypeData.etdDate) & "';")

            End If
        End With

        '+ Si se consigió información
        If lblnOk Then
            'Response.Write "top.frames['fraFolder'].document.forms[0].tcdBirthDate.value=" & ldtmBirthdat & ";"


            '			
        End If

        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClient = Nothing
        lclsClient_Saapv = Nothing

    End Sub

    '--------------------------------------------------------------------------------------------
    Private Sub insShowInstitution()
        '--------------------------------------------------------------------------------------------
        Dim lclsGeneral As eGeneral.OptionsInstallation

        lclsGeneral = New eGeneral.OptionsInstallation

        If Request.QueryString.Item("nType_Saapv") <> "4" Then
            lclsGeneral.FindOptPolicy()
            If lclsGeneral.nInstitution > 0 Then

                Response.Write("opener.document.forms[0].valInstitution.value=" & lclsGeneral.nInstitution & ";")
                Response.Write("opener.document.forms[0].valInstitution.disabled=true;")
                Response.Write("opener.document.forms[0].btnvalInstitution.disabled=true;")
                Response.Write("opener.$('#valInstitution').change();")
            End If
        Else
            Response.Write("opener.document.forms[0].valInstitution.value='';")
            Response.Write("opener.document.forms[0].valInstitution.disabled=false;")
            Response.Write("opener.document.forms[0].btnvalInstitution.disabled=false;")
            Response.Write("opener.UpdateDiv('valInstitutionDesc','','');")
        End If

        lclsGeneral = Nothing

    End Sub

    '% insShowTypeVeh: Ubica los datos de las fechas a partir del tipo de vehículo
    '--------------------------------------------------------------------------------------------
    Public Sub insShowTypeVeh()
        '--------------------------------------------------------------------------------------------
        Dim lclsSoap_TypeVeh As ePolicy.Soap_Sell_Period
        Dim ldblTypeVeh As Double
        Dim ldateSell As Date
        Dim lnYear As Integer
        Dim lstrMessage As String
        lclsSoap_TypeVeh = New ePolicy.Soap_Sell_Period
        Dim ef As New eFunctions.Values
        Dim lclsErrors As eFunctions.Errors
        'On Error GoTo insValCA986_Err
        lclsErrors = New eFunctions.Errors
        Dim lclsGeneral As eGeneral.GeneralFunction
        lclsGeneral = New eGeneral.GeneralFunction

        ldblTypeVeh = mclsValues.StringToType(Request.QueryString.Item("nTypeVeh"), eFunctions.Values.eTypeData.etdDouble)
        ldateSell = Date.Today
        lnYear = mclsValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdInteger)
        With Response

            If lclsSoap_TypeVeh.Find_Date(ldblTypeVeh, 11, ldateSell, lnYear) Then
                If (lclsSoap_TypeVeh.sError) = "" Then
                    lclsSoap_TypeVeh.sError = "NULL"
                End If
                If ((lclsSoap_TypeVeh.sError) <> "NULL") Then
                    lstrMessage = lclsGeneral.insLoadMessage(CInt(lclsSoap_TypeVeh.sError))
                    Response.Write("alert(""Error:  " & lstrMessage & """);")
                    'Response.Write("alert(" & lstrMessage & ");")
                End If
                .Write("top.fraHeader.document.forms[0].tcdStartDate.value='" & mclsValues.TypeToString(lclsSoap_TypeVeh.dStartDatepol, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("top.fraHeader.document.forms[0].tcdExpirDate.value='" & mclsValues.TypeToString(lclsSoap_TypeVeh.dExpireDatepol, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("top.fraHeader.document.forms[0].dStartDateOri.value='" & mclsValues.TypeToString(lclsSoap_TypeVeh.dStartDatepol, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("top.fraHeader.document.forms[0].dStartDatePol.value='" & mclsValues.TypeToString(lclsSoap_TypeVeh.dStartPeriod, eFunctions.Values.eTypeData.etdDate) & "';")
                .Write("top.fraHeader.document.forms[0].dExpirDatePol.value='" & mclsValues.TypeToString(lclsSoap_TypeVeh.dExpirePeriod, eFunctions.Values.eTypeData.etdDate) & "';")

                If lclsSoap_TypeVeh.dStartDatepol < Today And lclsSoap_TypeVeh.dExpireDatepol > Today Then
                    .Write("top.fraHeader.document.forms[0].tcdStartDate.disabled=false;")
                    .Write("top.fraHeader.document.forms[0].btn_tcdStartDate.disabled=false;")
                ElseIf lclsSoap_TypeVeh.dStartDatepol > Today Or lclsSoap_TypeVeh.dExpireDatepol < Today Then
                    .Write("top.fraHeader.document.forms[0].tcdStartDate.disabled=true;")
                    .Write("top.fraHeader.document.forms[0].btn_tcdStartDate.disabled=true;")
                End If
            End If
        End With
        lclsSoap_TypeVeh = Nothing

    End Sub
    Private Sub Switch_UpdPercent()
        '--------------------------------------------------------------------------------------------
        Dim nAction As Integer
        Dim sChecked As String

        Dim lclsBatch As eBatch.tmp_switch
        lclsBatch = New eBatch.tmp_switch

        nAction = Request.QueryString.Item("nAction")
        'Actualización puntual
        If nAction = "1" Then
            Call lclsBatch.insUpdvi017(Session("sKey"), _
                                 mclsValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), _
                                 mclsValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), _
                           mclsValues.StringToType(Request.QueryString.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble), _
                           mclsValues.StringToType(Request.QueryString.Item("nAmountToSell"), eFunctions.Values.eTypeData.etdDouble))
            If mclsValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
                If lclsBatch.nCount_Sell = 0 Then
                    sChecked = "false"
                Else
                    sChecked = "true"
                End If
                Response.Write("opener.opener.document.forms[0].tctCheck1" & Request.QueryString.Item("nId_orig") & ".checked=" & sChecked & ";")
            ElseIf Request.QueryString.Item("nType") = "3" Then
                Response.Write("top.frames['fraFolder'].insCalBuy();")
            ElseIf Request.QueryString.Item("nType") = "4" Then
                Response.Write("top.frames['fraFolder'].UpdateDiv('DivQuot4" & Request.QueryString.Item("nId") & "','" & mclsValues.TypeToString(lclsBatch.nQuan_avail_buy_switch, eFunctions.Values.eTypeData.etdDouble, True, 6) & "', '');")
                Response.Write("top.frames['fraFolder'].document.forms[0].hddUnitPrice4" & Request.QueryString.Item("nId") & ".value = '" & mclsValues.TypeToString(lclsBatch.nQuot_Value_Buy, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
            End If

            'Actualizacion masiva
        ElseIf nAction = "2" Then

            If lclsBatch.insUpdvi017Massive(Session("sKey"), _
                                      Request.QueryString.Item("nId"), _
                                Request.QueryString.Item("nPercent"), _
                                mclsValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble)) Then
                Response.Write("alert('Información actualizada con éxito');")
                If mclsValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
                    Response.Write("opener.opener.document.forms[0].tctCheck1" & Request.QueryString("nId_orig") & ".checked=true;")
                    Response.Write("opener.opener.document.forms[0].hddBuysChanged.value = '1';")
                    Response.Write("opener.window.close();")
                End If
            Else
                Response.Write("alert('Error al actualizar la información');")
            End If

            'Cancelar una operación
        ElseIf nAction = "3" Then
            If lclsBatch.insUpdvi017Changes(Session("sKey"), _
                                      2) Then
                'Se desmarca el check de contenido cuando se cancela el último cambio
                Response.Write("var sContent = '" & lclsBatch.sContent_Sell & "';")
                Response.Write("var arrContent = sContent.split(',');")
                Response.Write("for(var lintIndex=0; lintIndex<arrContent.length;lintIndex++){")
                Response.Write("    if (arrContent[lintIndex]!=''){")
                Response.Write("        var oCheck = eval('top.frames.fraFolder.document.forms[0].tctCheck1' + arrContent[lintIndex]);")
                Response.Write("        if (oCheck) oCheck.checked = false;")
                Response.Write("    }")
                Response.Write("}")
                Response.Write("alert('Información actualizada con éxito');")
            Else
                Response.Write("alert('Error al actualizar la información');")
            End If
        End If

        lclsBatch = Nothing
    End Sub

</script>
<%Response.Expires = -1
mclsValues = New eFunctions.Values

mclsValues.sCodisplPage = "showdefvalues"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>

<SCRIPT>
    //+ Variable para el control de versiones
        document.VssVersion="$$Revision: 23 $|$$Date: 29/10/09 7:20p $|$$Author: Gletelier $"  
</SCRIPT>	
</HEAD>
<BODY>
    <FORM NAME="ShowValues">
    </FORM>
</BODY>
</HTML>
<%
Response.Write(mclsValues.StyleSheet() & vbCrLf)
Response.Write("<SCRIPT>")


Select Case Request.QueryString.Item("Field")
	Case "Policy"
		Call insShowPolicy()
	Case "PolicyNum"
		Call insShowPolicyNum()
	Case "Policy_CA099", "insValsPolitype", "CA035_K", "Policy_VA650"
		Call insValPolitype()
		If Request.QueryString.Item("sCodispl") = "VI011" Then
			Call insShowCurren_pol()
		End If
	Case "Policy_CA789"
        Call insShowPolicyCA789()
            
    Case "Policy_CA888"
        Call insShowPolicyCA888()
                        
	Case "Certificat"
		Call insShowCertificat()
	Case "Currency"
		Call insShowData()
	Case "Switch_Curr_Pol"
		Call insShowPolicyData()
	Case "Switch_Curr_Cer"
		Call insShowCertifData()
        Case "Switch_Amount"
            Call insSwitch_Amount()
	Case "Receipt"
		Call insShowReceipt()
	Case "AdjReceipt"
		Call insShowAdjReceipt()
	Case "CotProp"
		Call insShowCotProp()
	Case "Curren_pol"
		Call insShowCurren_pol()
	Case "insCalExpirDate"
		Call insCalExpirDate()
	Case "sClientRole"
		Call sClientRole()
	Case "VIC005"
		Call insShowVIC005()
	Case "NewNextreceip"
		Call insDateNextreceip()
	Case "Refund"
		Call insShowProduct()
	Case "FindRefund"
		Call insShowRefund()
	Case "nId"
		Call insShowWorksheet()
	Case "SurrenValue"
		Call insSurrenValue()
	Case "SuggestPrem"
		Call insSuggestPrem()
	Case "ValPolitype"
		Call ValPolitype()
	Case "InsShowClientRole"
		Call InsShowClientRole()
	Case "CallPayOrder"
		Call insCallPayOrder()
	Case "ProcessCAL036"
		Call insProcessCAL036()
	Case "DelTConvertions"
		Call insDelTConvertions()
	Case "InsTConvertions"
		Call insTConvertions()
	Case "Agency"
		If IsNothing(Request.QueryString.Item("nOfficeAgen")) Then
			Call insShowAgency()
		End If
	Case "OptDev"
		Call insShowDev()
	Case "Account_Pol"
		Call Account_Pol("-1")
	Case "Loans"
		Call insShowLoans()
	Case "cbeAgency"
		If IsNothing(Request.QueryString.Item("nOfficeAgen")) Then
			Call insShowcbeAgency()
		End If
	Case "Data_loans"
		Call insShowData_loans()
	Case "UpdVi010"
		Call insUpdVi010()
	Case "UpdVi7002"
		Call insUpdVi7002()
	Case "ExpirDateRec"
		Call ExpirDateRec()
	Case "InsCalSurrCost"
		Call InsCalSurrCost()
	Case "InsCalPrem_Guar_Saving"
		InsCalPrem_Guar_Saving()
	Case "ShowUser"
            InsShowUser()
	Case "ExpirdateNew"
		Call CalExpirdateNew()
	Case "ValuesSOAT"
		Call ChangeValuesSOAT()
	Case "CallExpenses"
		Call insShowExpenses()
	Case "inssApv"
		Call inssApv()
	Case "cbeOffice"
		Call insGetPaymentLocations()
	Case "DisabledInsurRecord"
		Call insDisabledInsurRecord()
	Case "InsNexchange"
		Call InsNexchangeChange()
	Case "InsNexchangeVI009"
		Call InsNexchangeVI009()
	Case "InsChangeTyp_Profitworker"
		Call InsChangeTyp_Profitworker()
	Case "Switch_Del"
		Call InsSwitch_Del()
	Case "ChangeValuesRever"
		Call ChangeValuesRever()
	Case "CheckClient"
		Call CheckCreClient()
	Case "Folio"
		Call insShowfolio()
	Case "TypeVeh"
            Call insShowTypeVeh()
	Case "Municipality"
		Call ReaMunicipality()
    Case "ChangeStartDate"
        Call insChangeStartDate(Request.QueryString.Item("dStartDate"))
	Case "Auto_Regist"
		Call insShowAuto_Regist()
	Case "insShowProp"
		Call insShowProp()
	Case "ShowSaapv"
		Call insShowSaapv()
	Case "LimitDate"
		Call insLimitDate()
	Case "ClientVI7501_A"
		Call InsShowClientDataA()
	Case "ClientVI7501_B"
		Call InsShowClientDataB()
	Case "ShowInstitution"
		Call insShowInstitution()
    Case "Auto_Digit"
            insShowAuto_Digit()
        Case "Switch_UpdPercent"
            Call Switch_UpdPercent()

 End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing

%>




