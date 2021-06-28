<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'Dim InsPreCA003AUpd() As Object

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

Dim mjvalues As eFunctions.Values
  
Dim mclsClaim_auto As eClaim.Claim_auto
 
Dim nIdCasualty As Integer

'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
        Dim dCasualtyDate As Object
        Dim dClaimDate As Object
        Dim dBirthDateSinister As Object
        Dim sPasswd As String
        Dim sUserName As String
        Dim nRutPersonSinister As String
        Dim vIn As Char = "0"
        Dim vOut As Integer
        Dim sNameclaimant As String
        Dim sPatent As String
        Dim sPart As String
        
        Dim nIdClaim As String
        ' Dim nIdCasualty As Integer
        
        Dim nCase As Integer
        Dim nPolicy As Integer
        Dim nCobert As Double
        Dim nLimitAmount As Double
        
        Dim sClientSiniestrado As String
        Dim sClientNameSiniestrado As String
        Dim dBirtdateSiniestrado As Date
        Dim sClientReclamante As String
        Dim sClientNameReclamante As String
        Dim SDescritReclamente As String
        Dim sClientBeneficiario As String
        Dim sClientNameBeneficiario As String
        Dim sDescritBeneficiario As String
        Dim sPatente As String
        Dim sDigitoPatente As String
        
        Dim lobjUserValidate As eSecurity.UserValidate
        Dim mclsProdmaster As eProduct.Product
        Dim mclsClaim_auto As eClaim.Claim_auto
        mclsClaim_auto = New eClaim.Claim_auto
        
        lobjUserValidate = New eSecurity.UserValidate
        mjvalues = New eFunctions.Values
        
        
        ''  
        Dim mobjProf_ord As new eClaim.Prof_ord
        Dim mobjProf_ords As New eClaim.Prof_ords
       ' Dim mobjGrid As eFunctions.Grid
        Dim lintIndex As Integer
        Dim sKey As Object
        Dim lclsClaim As eClaim.ClaimBenef
        'Dim TypeProvider As Object
        '  Dim mobjProf_ords As eClaim.Prof_ords
        lclsClaim = New eClaim.ClaimBenef
        mobjValues = New eFunctions.Values
        lintIndex = 0
        
        '' mobjProf_ords.Find(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
        'mobjProf_ords.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
        ''If lcolClaim_cases.Find(CStr(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
                  
        'Session("nServ_Order_GM") = mobjProf_ord.nServ_Order
        'Session("nServ_Order_GM") = lclsProf_orde.nServ_Order
        
        If mobjProf_ords.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble)) Then
            For lintCount = 1 To mobjProf_ords.Count
                mobjProf_ord = mobjProf_ords.Item(lintCount)

                With mobjGrid
                    Session("nServ_Order_GM2") = mobjProf_ord.nServ_Order
                    nIdCasualty = mobjProf_ord.nServ_Order
                    'nIdCasualty = 150273
                End With
            Next
        End If
                  
                    If nIdCasualty = eRemoteDB.intNull Then
                        nIdCasualty = 0
                    End If
                    ''
        
                    dCasualtyDate = Today.ToString("dd/MM/yyyy") & TimeOfDay.ToString(" hh:mm:ss")
                    'dCasualtyDate = Format(dCasualtyDate, "yyyyMMdd hh:mm:ss")
                    dBirthDateSinister = Today.ToString("dd/MM/yyyy")
                    'dBrithDateSinister = Format(dBrithDateSinister, "yyyyMMdd")
                    sPasswd = Session("sAccesswo")
                    'sPasswd = "sAccesswo"
                    'sPasswd = "`d`óìÀ"
                    'sPasswd = lobjUserValidate.StrEncode(sPasswd)
                    sPasswd = HttpUtility.UrlEncode(sPasswd)
                    sUserName = HttpUtility.UrlEncode(Session("sInitials"))
                    'sUserName = "prueba"
                    nIdClaim = Session("nClaim")
                    nRutPersonSinister = Session("sClient") '& Session("sDigit") 
                    vOut = Convert.ToInt32(nRutPersonSinister)
                    sNameclaimant = HttpUtility.UrlEncode(Session("sCliename"))
                    sPatent = Session("sRegistGM")
        
                    dCasualtyDate = Session("dDemand_date")
        
                    nPolicy = Session("nPolicy")
                    Session("nPolicyGM") = nPolicy
                    nCobert = Session("nCoverGM")
                    Session("nCoverGM") = nCobert
                    nLimitAmount = Session("nReserveGM")
                    mclsProdmaster = New eProduct.Product
                    ' Call mclsProdmaster.FindGM(mobjValues.StringToType(nIdClaim, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble))
                    'Session("sBrancht") = mclsProdmaster.sBrancht
        
                    sPart = Session("nFine")
                    nCase = Session("nCase_num")
        
                    Call mclsClaim_auto.Find2(Session("nClaim"))
                    sPart = mclsClaim_auto.nFine
                    nCase = mclsClaim_auto.nCase_num
                    dCasualtyDate = mclsClaim_auto.dDoccurdat
                    dClaimDate = mclsClaim_auto.dDecladat
  
        
                    Call mclsAuto.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
                    sPatente = mclsAuto.sRegist
                    sDigitoPatente = mclsAuto.sDigit
                    Session("sRegistGM") = sPatente & "-" & sDigitoPatente
                    sPatent = Session("sRegistGM")
                    '+ Se asignan los valores del código y nombre de conductor, número de licencia y fecha de la misma
                    '+ tomados de los datos particulares de la póliza
                    If mclsAuto.sClient <> vbNullString Then
                        mstrDriverCode = mclsAuto.sClient
                        mstrDriverName = mclsAuto.sCliename
                    End If
        
                    'Datos del Reclamante'
        
                    mclsAuto = New Automobile
                    mclsClaim_auto = New eClaim.Claim_auto
                    mclsClient = New eClient.Client
                    mclsClient2 = New eClient.Client
                    Call mclsClient.Find_GM_Reclamante(Session("nClaim"))
        
                    'If mclsClient.sClient <> vbNullString Then
                    sClientReclamante = Session("Reclamante") 'mclsClient.sClient
                    sClientNameReclamante = HttpUtility.UrlEncode(Session("NombreReclamante")) 'mclsClient.sCliename
                    SDescritReclamente = HttpUtility.UrlEncode(mclsClient.sSmoking)
                    If SDescritReclamente = eRemoteDB.strNull Then
                    SDescritReclamente = HttpUtility.UrlEncode("Sin Información")
                    End If

                    ' End If
       
                    'Datos del Siniestrado'        
                    Dim mcolCL_siniestrado As eClaim.CL_Covers
                    Dim lblnFindGastos As Boolean
                    mclsAuto = New Automobile
                    mclsClaim_auto = New eClaim.Claim_auto
                    mclsClient = New eClient.Client
                    mclsClient2 = New eClient.Client
                    Call mclsClient.Find_GM_Siniestrado(Session("nClaim"))
        
                    'If mclsClient.sClient <> vbNullString Then
                    sClientSiniestrado = Session("Siniestrado") 'mclsClient.sClient
                    sClientNameSiniestrado =  HttpUtility.UrlEncode(Session("NombreSiniestrado")) 'mclsClient.sCliename
                    dBirtdateSiniestrado = mclsClient.dBirthdat
                    If dBirtdateSiniestrado = eRemoteDB.dtmNull Then
                    dBirtdateSiniestrado = DateTime.Parse("01/01/2000")
                    End If

        
                    ' End If
        
                    'Datos del Beneficiario'      
       
                    Call mclsClient.Find_GM_Beneficiario(Session("nClaim"))
        
                    'If mclsClient.sClient <> vbNullString Then
                    sClientBeneficiario = Session("Beneficiario") 'mclsClient.sClient
                    sClientNameBeneficiario = HttpUtility.UrlEncode(Session("NombreBeneficiario")) 'mclsClient.sCliename
                    sDescritBeneficiario = HttpUtility.UrlEncode(mclsClient.sSmoking)
                    If sDescritBeneficiario = eRemoteDB.strNull Then
                    sDescritBeneficiario = HttpUtility.UrlEncode("Sin Información")
                    End If

        
        
                    'Se asignan los datos obtenidos
                    Response.Write(mjvalues.HiddenControl("nIdClaim", nIdClaim))
                    'Response.Write(mjvalues.HiddenControl("sPatent", "VB0176"))
                    'Response.Write(mjvalues.HiddenControl("sPatent", sPatent & "-1"))
                    Response.Write(mjvalues.HiddenControl("sPatent", sPatent))
                    'Response.Write(mjvalues.HiddenControl("sPart", 123333))
                    Response.Write(mjvalues.HiddenControl("sPart", sPart))
                    'Response.Write(mjvalues.HiddenControl("dCasualtyDate", "22/07/2015"))
                    Response.Write(mjvalues.HiddenControl("dCasualtyDate", dCasualtyDate))
                    'Response.Write(mjvalues.HiddenControl("dClaimDate", "22/07/2015"))
                    Response.Write(mjvalues.HiddenControl("dClaimDate", dClaimDate))
                    'Response.Write(mjvalues.HiddenControl("nRutPersonSinister", 16616916))'-->Asegurado
                    Response.Write(mjvalues.HiddenControl("nRutPersonSinister", sClientSiniestrado))
                    'Response.Write(mjvalues.HiddenControl("sNameSinister", "Prueba2, Prueba1")) '-->Asegurado
                    Response.Write(mjvalues.HiddenControl("sNameSinister", sClientNameSiniestrado)) '
                    'Response.Write(mjvalues.HiddenControl("dBirthDateSinister", dBirthDateSinister))
                    Response.Write(mjvalues.HiddenControl("dBirthDateSinister", dBirtdateSiniestrado)) 'dBirthDateSinister
        
                    'Response.Write(mjvalues.HiddenControl("nRutPersonClaimant", 16616916))
                    Response.Write(mjvalues.HiddenControl("nRutPersonClaimant", sClientReclamante)) '-->Asegurado
                    'Response.Write(mjvalues.HiddenControl("sNameclaimant", "Prueba2, Prueba1"))
                    Response.Write(mjvalues.HiddenControl("sNameclaimant", sClientNameReclamante))
                    'Response.Write(mjvalues.HiddenControl("sRelationshipClaimant", "Hospital"))
                    Response.Write(mjvalues.HiddenControl("sRelationshipClaimant", SDescritReclamente))
        
                    ' Response.Write(mjvalues.HiddenControl("nRutPersonBeneficiary", 16616916)) '-->Asegurado
                    Response.Write(mjvalues.HiddenControl("nRutPersonBeneficiary", sClientBeneficiario)) '-->Asegurado
                    Response.Write(mjvalues.HiddenControl("sNameBeneficiary", sClientNameBeneficiario)) '-->Asegurado
                    'Response.Write(mjvalues.HiddenControl("sRelationshipBeneficiary", "Hospital"))
                    Response.Write(mjvalues.HiddenControl("sRelationshipBeneficiary", sDescritBeneficiario))
        
                    Response.Write(mjvalues.HiddenControl("sUserName", sUserName))
                    Response.Write(mjvalues.HiddenControl("sPassword", sPasswd))
                    'Response.Write(mjvalues.HiddenControl("nCase", 1))
                    Response.Write(mjvalues.HiddenControl("nCase", nCase))
                   'Response.Write("<input type="hidden" disable="disable" id="form1" name="form1" value=nIdCasualty >")=
                    Response.Write(mjvalues.HiddenControl("nIdCasualty", nIdCasualty))
                    'Response.Write(mjvalues.HiddenControl("nIdCasualty", 1))
                    'Response.Write(mjvalues.HiddenControl("nPolicy", 1))
                    Response.Write(mjvalues.HiddenControl("nPolicy", nPolicy))
                    'Response.Write(mjvalues.HiddenControl("nLimitAmount", 300))
                    Response.Write(mjvalues.HiddenControl("nLimitAmount", nLimitAmount))
                    'Response.Write(mjvalues.HiddenControl("nCobert", 1001))
                    Response.Write(mjvalues.HiddenControl("nCobert", nCobert))
        
	
    End Sub
    '- Objeto para el manejo de las funciones generales de carga de valores
    'Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mclsAuto As ePolicy.Automobile
    'Dim mclsClaim_auto As eClaim.Claim_auto
    Dim mclsClient As eClient.Client
    Dim mclsClient2 As eClient.Client
    Dim mstrQueryString As String
    '- Variables auxiliares

    Dim mstrDriverCode As String
    Dim mstrDriverName As String
    Dim mstrLicense As String
    Dim mdtmDriverDate As Object


    '%Procedimiento insPreSI018. Este procedimiento se encarga de cargar los valores de las
    '%tablas en los controles de la ventana
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCASERGM()
        '--------------------------------------------------------------------------------------------
	
        mstrDriverCode = vbNullString
        mstrDriverName = vbNullString
        mstrLicense = vbNullString
        mclsAuto = New Automobile
        mclsClaim_auto = New eClaim.Claim_auto
        mclsClient = New eClient.Client
        mclsClient2 = New eClient.Client
        Call mclsAuto.Find("2", Session("nBranch"), Session("nProduct"),Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
        Session("sRegistGM") = mclsAuto.sRegist
        '+ Se asignan los valores del código y nombre de conductor, número de licencia y fecha de la misma
        '+ tomados de los datos particulares de la póliza
        If mclsAuto.sClient <> vbNullString Then
            mstrDriverCode = mclsAuto.sClient
            mstrDriverName = mclsAuto.sCliename
        End If
	
        If mclsAuto.sLicense <> vbNullString Then
            mstrLicense = mclsAuto.sLicense
        End If
	
        If mclsAuto.dDriverdat <> CStr(eRemoteDB.Constants.dtmNull) Then
            mdtmDriverDate = mclsAuto.dDriverdat
        End If
	
        Call mclsClaim_auto.Find(Session("nClaim"), Session("nCase_num"),Session("nDeman_type"))
	
        '+ Si ya existe información previamente registrada en los datos del auto involucrado en el siniestro,
        '+ se asignan los valores del código y nombre de conductor, número de licencia y fecha de la misma
	
        If mclsClaim_auto.sDriver_cod <> vbNullString Then
            mstrDriverCode = mclsClaim_auto.sDriver_cod
            mstrDriverName = mclsClaim_auto.sCliename
        End If
	
        If mclsClaim_auto.sLicense <> vbNullString Then
            mstrLicense = mclsClaim_auto.sLicense
        End If
	
        If mclsClaim_auto.dDriverDat <> eRemoteDB.Constants.dtmNull Then
            mdtmDriverDate = mclsClaim_auto.dDriverDat
        End If
	
        '+ Se obtienen los datos personales del conductor al momento del siniestro    
        Call mclsClient.Find(mclsClaim_auto.sDriver_claim)
        '+ Se obtienen los datos personales del testigo al momento del siniestro        
        Call mclsClient2.Find(mclsClaim_auto.sWitness)
    End Sub
</script>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<%Response.Expires = -1

    
    'Response.Redirect("http://requestb.in/136o2pw1?param1=1")
    
%>

<%--<BODY ONUNLOAD="closeWindows();">
--%>
</head>
<body onload="cargar_pagina()">
<script type= text/javascript language="Javascript">
    function cargar_pagina() {
    
        frmsendCaseRGM.action = 'http://10.161.113.13:82/Claim/loadclaimvt'; //DESARROLLO
        //frmsendCaseRGM.action = 'http://10.151.9.151:61309/Claim/loadclaimvt'; //desa juan
        //frmsendCaseRGM.action = 'http://52.3.0.107:84/Claim/loadclaimvt'; //QA-INTERNO

        frmsendCaseRGM.submit();
        window.history.back();


        <%Response.Write("top.close();top.opener.document.location.reload();window.close();")%>
         
        window.close();
         
    }
</script>

<FORM METHOD="post" ID="FORM1" NAME="frmsendCaseRGM" target="_blank" ACTION="http://10.161.113.13:82/Claim/loadclaimvt"><%--DESARROLLO--%>
<!--<FORM METHOD="post" ID="FORM1" NAME="frmsendCaseRGM"  target="_blank" ACTION="http://10.151.9.151:61309/Claim/loadclaimvt">--><%--desa juan--%>
<!--<form method="post" id="FORM1" name="frmsendCaseRGM" target="_blank" action="http://52.3.0.107:84/Claim/loadclaimvt">--><%--QAINTERNO--%>


<%  'Response.Write(mobjValues.ShowWindowsName("sendCaseRGM"))
    Call insPreCASERGM()
    Call insDefineHeader()
    
%>
<%--<input type="hidden" id="nIdCasualty"name="sForm" value='<%=nIdCasualty%>'>--%>
<%--<input type="submit" value="Submit">--%>
</FORM> 
</BODY>
</HTML>
