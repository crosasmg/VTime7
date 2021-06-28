<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mclsValues As eFunctions.Values


'% insValPolitype: valida el tipo de póliza para habilitar/deshabilitar el certificado
'% Debe ser invocada con funcion insDefValues
'--------------------------------------------------------------------------------------------
Sub insShowCertifNum()
	'--------------------------------------------------------------------------------------------
	Dim lstrCertype As Object
	Dim lclsCertificat As ePolicy.Certificat
	lstrCertype = Session("sCertype")
	lclsCertificat = New ePolicy.Certificat
	With lclsCertificat
		If .Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif"))) Then
			Response.Write("with(top.frames['fraFolder'].document.forms[0]){")
			'Response.Write "    tcnCertif.value='" & lclsCertificat.nCertif & "';"
			Response.Write("    tctRenewal.value='" & mclsValues.TypeToString(lclsCertificat.dNextReceip, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("    tctStartDat.value='" & mclsValues.TypeToString(lclsCertificat.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("    tctExpirdat.value='" & mclsValues.TypeToString(lclsCertificat.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
			'Response.Write "    tctClientname.value='" & lclsCertificat.sCliename & "';"
			Response.Write("}")
		Else
			Response.Write("alert('La Propuesta no se encuentra pendiente');")
		End If
	End With
	lclsCertificat = Nothing
End Sub
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
	Dim lclsProduct As eProduct.Product
	
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
	
	If Request.QueryString.Item("sCodispl") = "VI009_K" Then
		
		'+ se agrego este manejo para el numero unico de poliza
		If lclsPolicy_po.FindPolicybyPolicy("2", CDbl(Request.QueryString.Item("nPolicy"))) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy_po.nBranch & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy_po.nBranch & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
			If lclsPolicy_po.nProduct <> CDbl("") Then
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
				If lclsPolicy.nProduct <> "" Then
					Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
				End If
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
			End If
		End With
		
		Response.Write("with(top.frames['" & lstrFrame & "'].document.forms[0]){")
		
		If Request.QueryString.Item("sCodispl") = "CA034" Then
			Response.Write("cbeOfficeAgen.Parameters.Param1.sValue =" & lclsPolicy.nOffice & ";")
			Response.Write("cbeOfficeAgen.Parameters.Param2.sValue =" & eRemoteDB.Constants.intNull & ";")
			Response.Write("cbeAgency.Parameters.Param1.sValue =" & lclsPolicy.nOffice & ";")
			Response.Write("cbeAgency.Parameters.Param2.sValue =" & lclsPolicy.nOfficeagen & ";")
			Response.Write("cbeOffice.value='" & mclsValues.StringToType(lclsPolicy.nOffice, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("cbeOfficeAgen.value='" & mclsValues.StringToType(lclsPolicy.nOfficeagen, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("cbeAgency.value='" & mclsValues.StringToType(lclsPolicy.nAgency, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['" & lstrFrame & "'].$('#cbeOfficeAgen').change();")
			Response.Write("top.frames['" & lstrFrame & "'].$('#cbeAgency').change();")
			lclsProduct = New eProduct.Product
			If lclsProduct.Find(lclsCertificat.nBranch, lclsCertificat.nProduct, lclsCertificat.dStartdate, True) Then
				Response.Write("chksRehab_receipt.value='';")
				Response.Write("chksRehab_receipt.checked=false;")
				
                    If lclsProduct.sReactivation = "1" Then
                        Response.Write("chksRehab_receipt.disabled=false;")
                    Else
                        Response.Write("chksRehab_receipt.disabled=true;")
                    End If
			End If
			lclsProduct = Nothing
		End If
		Response.Write("tcnCertif.value=""0"";")
		'+Asignación del Tipo de póliza
		Session("sPolitype") = lclsPolicy.sPolitype
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
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
	If Request.QueryString.Item("sCodispl") = "CA034" And CStr(Session("sPolitype")) = "2" Then
		Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.value='0';")
		Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.disabled=true;")
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
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & lclsPolicy.dStartdate & "';")
			
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
Sub insShowPolicy()
	'dim eRemoteDB.Constants.intNull As Integer
	Dim lclsOptSystem As Object
	Dim lclsProcess As Object
	Dim llngCodeProce As Byte
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

'% insShowCertificat: se muestran los datos asociados al número de certificado
'%                    Se utiliza para el campo Certificado de la página CA001_K.aspx
'--------------------------------------------------------------------------------------------
Sub insShowCertificat()
	'--------------------------------------------------------------------------------------------
	Dim lclsCertificat As ePolicy.Certificat
	
	lclsCertificat = New ePolicy.Certificat
	With lclsCertificat
		If .Find(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			'+Se muestra,por defecto, la fecha actual de renovación de la póliza
			If .dExpirdat <> eRemoteDB.Constants.dtmNull Then
				Response.Write("opener.document.forms[0].tcdExpirdate.value='" & mclsValues.DateToString(.dExpirdat) & "';")
			Else
				Response.Write("opener.document.forms[0].tcdExpirdate.value=" & eRemoteDB.Constants.strnull)
			End If
			
			'+Se muestra,por defecto, la fecha actual de próxima facturación de la póliza
			If .dNextReceip <> eRemoteDB.Constants.dtmNull Then
				Response.Write("opener.document.forms[0].tcdNextReceip.value='" & mclsValues.DateToString(.dNextReceip) & "';")
			Else
				Response.Write("opener.document.forms[0].tcdNextReceip.value=" & eRemoteDB.Constants.strnull)
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
	'    Dim lclsPolicy
	Dim llngPolicy As Double
	Dim llngCertif As Double
	Dim lstrPolitype As Object
	Dim ldtmEffecdate As Date
	Dim lstrOrigin As String
	'   Dim lclsPolicy_his
	
	llngPolicy = 0
	llngCertif = 0
	lstrPolitype = 1
	lstrOrigin = Trim(Request.QueryString.Item("valOrigin"))
	lclsTConvertions = New ePolicy.TConvertions
	lclsPolicy_po = New ePolicy.Policy
	
	'+ se agrego este manejo para el numero unico de propuesta/Cotización
	If lclsPolicy_po.FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("opener.document.forms[0].cbeBranch.value=" & lclsPolicy_po.nBranch & ";")
		Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy_po.nBranch & ";")
		Response.Write("opener.document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
		Response.Write("opener.document.forms[0].cbeBranch.disabled=true;")
		If lclsPolicy_po.nProduct <> CDbl("") Then
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
		If lstrOrigin = "4" Or lstrOrigin = "5" Or lstrOrigin = "6" Or lstrOrigin = "7" Or lstrOrigin = "8" Or lstrOrigin = "9" Then
			
			If lclsTConvertions.Find_PropSpecial(mclsValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)) Then
				llngPolicy = lclsTConvertions.nPolicy
				llngCertif = lclsTConvertions.nCertif
				lstrPolitype = lclsTConvertions.sPolitype
				ldtmEffecdate = lclsTConvertions.dStartdate
			End If
		ElseIf lstrOrigin = "1" Or lstrOrigin = "2" Or lstrOrigin = "3" Then 
			If lclsTConvertions.Find_Prop_ren(Request.QueryString.Item("sCertype"), mclsValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)) Then
				llngPolicy = lclsTConvertions.nPolicy
				llngCertif = lclsTConvertions.nCertif
				lstrPolitype = lclsTConvertions.sPolitype
				ldtmEffecdate = lclsTConvertions.dStartdate
			End If
		End If
	End With
	
	With Response
		.Write("with(opener.document.forms[0]){")
		.Write("    tcnProponum.value=" & llngPolicy & ";")
		.Write("    tcnCertif.value=" & llngCertif & ";")
		.Write("    tcdEffecdate.value='" & mclsValues.TypeToString(ldtmEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
		'+ si es propuesta el campo npolicy queda deshabilitado en la pantalla ca099
		If lstrOrigin = "1" Then
			.Write("     tcnProponum.disabled=true;")
		End If
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
	
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsPolicy_his As ePolicy.Policy_his
	
	lclsPolicy = New ePolicy.Policy
	Call lclsPolicy.FindPolPropbyPolicy(CDbl(Request.QueryString.Item("nProponum")))
	If lclsPolicy.nPolicy <> eRemoteDB.Constants.intNull Then
		lclsPolicy_his = New ePolicy.Policy_his
		If lclsPolicy_his.reaPolicy_his_typeamend(lclsPolicy.sCertype, lclsPolicy.nBranch, lclsPolicy.nProduct, lclsPolicy.nPolicy, llngCertif, lclsPolicy.nPolicy) Then
			
			Response.Write("opener.document.forms[0].hddnTypeAmend.value = " & lclsPolicy_his.nType_amend & ";")
		End If
		lclsPolicy_his = Nothing
	End If
	
	
	'Set lclsPolicy = Nothing
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
					If lclsDet_comgen.nRate_first = eRemoteDb.Constants.dblNull Then
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
	
	nNumError = lclsPolicy.insValPolicy(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), Session("sTypeCompanyUser"))
	
	If nNumError = 0 Then
		'+ Se actualizan los datos obtenidos sobre los campos de la página
		
		With Response
			.Write("with(top.frames['fraFolder'].document.forms[0]){")
			.Write("    tcnCertif.value='" & lclsPolicy.nCertif & "';")
			.Write("    tctRenewal.value='" & mclsValues.TypeToString(lclsPolicy.dNextReceip, eFunctions.Values.eTypeData.etdDate) & "';")
			.Write("    tctStartDat.value='" & mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
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

'% insShowAdjReceipt: Se muestra la información del recibo a ajustar
'--------------------------------------------------------------------------------------------
Sub insShowAdjReceipt()
	'--------------------------------------------------------------------------------------------
	Dim lclsAdjPremium As eCollection.Premium
	
	lclsAdjPremium = New eCollection.Premium
	With lclsAdjPremium
		
		If .Find("2", CDbl(Request.QueryString.Item("nAdjReceipt")), Session("nBranch"), Session("nProduct"), 0, 0) Then
			If CStr(Session("sPoliType")) = "2" Then
				If .nPremium = .nBalance Then
					Response.Write("with(opener.document.forms[0]){" & "  cbeCurrency.value='" & .nCurrency & "';" & "  tcdStartDateR.value='" & mclsValues.TypeToString(.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';" & "  tcdExpirDateR.value='" & mclsValues.TypeToString(.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';" & "  cbeCurrency.disabled   = true;" & "  tcdStartDateR.disabled = true;" & "  tcdExpirDateR.disabled = true;" & "  tcnPremiumOri.value ='" & mclsValues.TypeToString(.nPremium, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';" & "  tcnBalanceOri.value ='" & mclsValues.TypeToString(.nBalance, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';" & "}")
				Else
					Response.Write("with(opener.document.forms[0]){" & "  cbeCurrency.value='';" & "  tcdStartDateR.value='';" & "  tcdExpirDateR.value='';" & "  tcdStartDateR.disabled=false;" & "  tcdExpirDateR.disabled=false;" & "  cbeCurrency.disabled=false;" & "  tcnPremiumOri.value ='';" & "  tcnBalanceOri.value ='';" & "}")
					Response.Write("alert('El recibo no se puede ajustar posee pagos parciales');")
				End If
			Else
				Response.Write("with(opener.document.forms[0]){" & "  cbeCurrency.value='" & .nCurrency & "';" & "  tcdStartDateR.value='" & mclsValues.TypeToString(.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';" & "  tcdExpirDateR.value='" & mclsValues.TypeToString(.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';" & "  cbeCurrency.disabled   = true;" & "  tcdStartDateR.disabled = true;" & "  tcdExpirDateR.disabled = true;" & "  tcnPremiumOri.value ='" & mclsValues.TypeToString(.nPremium, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';" & "  tcnBalanceOri.value ='" & mclsValues.TypeToString(.nBalance, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';" & "}")
			End If
		Else
			Response.Write("with(opener.document.forms[0]){" & "  cbeCurrency.value='';" & "  tcdStartDateR.value='';" & "  tcdExpirDateR.value='';" & "  tcdStartDateR.disabled=false;" & "  tcdExpirDateR.disabled=false;" & "  cbeCurrency.disabled=false;" & "  tcnPremiumOri.value ='';" & "  tcnBalanceOri.value ='';" & "}")
		End If
	End With
	
End Sub

'**% insShowPolicyData: Show the information of the policy.
'% insShowPolicyData: Muestra los datos de la póliza.
'--------------------------------------------------------------------------------------------
Private Sub insShowPolicyData()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrBranch As Object
	Dim lstrProduct As Object
	Dim lstrPolicy As Object
	
	lstrBranch = Request.QueryString.Item("nBranch")
	lstrProduct = Request.QueryString.Item("nProduct")
	lstrPolicy = Request.QueryString.Item("nPolicy")
	
	If lstrBranch <> vbNullString And lstrProduct <> vbNullString And lstrPolicy <> vbNullString Then
		lclsPolicy = New ePolicy.Policy
		
		If lclsPolicy.Find("2", lstrBranch, lstrProduct, lstrPolicy) Then
			If lclsPolicy.sPolitype = "1" Then
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.value=0;")
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=true;")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=false;")
			End If
			
		End If
		lclsPolicy = Nothing
	Else
		Response.Write("top.fraHeader.document.forms[0].tcnCertif.value=0;")
		Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=true;")
	End If
	
End Sub

'**% insShowCertifData: Show the information of the certificate.
'% insShowCertifData: Muestra los datos del certificado.
'--------------------------------------------------------------------------------------------
Private Sub insShowCertifData()
	'--------------------------------------------------------------------------------------------
	Dim lclsCertificat As ePolicy.Certificat
	Dim lstrCurrency As String
	Dim lclsCurrenPol As ePolicy.Curren_pol
	Dim ldtmEffecdate As Object
	Dim lclsPolicy_his As Object
	Dim lstrBranch As Object
	Dim lstrProduct As Object
	Dim lstrPolicy As Object
	Dim lstrCertif As Object
	Dim lstrProponum As Object
	
	lstrBranch = Request.QueryString.Item("nBranch")
	lstrProduct = Request.QueryString.Item("nProduct")
	lstrPolicy = Request.QueryString.Item("nPolicy")
	lstrCertif = Request.QueryString.Item("nCertif")
	
	Dim lclsRoles As ePolicy.Roles
	If lstrBranch <> vbNullString And lstrProduct <> vbNullString And lstrPolicy <> vbNullString And lstrCertif <> vbNullString Then
		
		lclsCertificat = New ePolicy.Certificat
		
		If lclsCertificat.Find("2", lstrBranch, lstrProduct, lstrPolicy, lstrCertif) Then
			If Request.QueryString.Item("dEffecdate") <> vbNullString Then
				ldtmEffecdate = Request.QueryString.Item("dEffecdate")
			Else
				ldtmEffecdate = Today
			End If
			
			If Request.QueryString.Item("sCod_VI7000") = "VI7000" Then
				lclsRoles = New ePolicy.Roles
				With Request
					If lclsRoles.Find("2", mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 1, "", mclsValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
						
						Response.Write("try{top.fraHeader.document.forms[0].hddClientBenef.value='" & lclsRoles.sClient & "'}catch(x){};")
					Else
						Response.Write("try{top.fraHeader.document.forms[0].hddClientBenef.value=''}catch(x){};")
					End If
				End With
				lclsRoles = Nothing
			Else
				Response.Write("try{top.fraHeader.document.forms[0].hddClientBenef.value='" & lclsCertificat.sClient & "'}catch(x){};")
			End If
			
			lclsCurrenPol = New ePolicy.Curren_pol
			lstrCurrency = lclsCurrenPol.findCurrency("2", lstrBranch, lstrProduct, lstrPolicy, lstrCertif, ldtmEffecdate)
			
			'+Si es multimoneda, se deja moneda local
			If lstrCurrency = "*" Then
				Response.Write("try{top.fraHeader.document.forms[0].cbeCurrency.value='1'}catch(x){};")
			Else
				Response.Write("try{top.fraHeader.document.forms[0].cbeCurrency.value='" & lclsCurrenPol.nCurrency & "'}catch(x){};")
			End If
			Response.Write("try{top.fraHeader.UpdateDiv('divCurrency','" & lclsCurrenPol.sDescript & "', '')}catch(x){};")
			
			lclsCurrenPol = Nothing
			
		End If
		
		lclsCertificat = Nothing
	Else
		Response.Write("try{top.fraHeader.document.forms[0].hddClientBenef.value=''}catch(x){};")
		Response.Write("try{top.fraHeader.document.forms[0].cbeCurrency.value='0'}catch(x){};")
		Response.Write("try{top.fraHeader.UpdateDiv('divCurrency','', '')}catch(x){};")
	End If
	
End Sub

'**% insSwitch_Amount: Calculates the swith amounts
'--------------------------------------------------------------------------------------------
Private Sub insSwitch_Amount()
	'--------------------------------------------------------------------------------------------
	Dim ldblValue As Double
	
	Dim lclsExchange As eGeneral.Exchange
	lclsExchange = New eGeneral.Exchange
	
	If CDbl(Request.QueryString.Item("nInd")) = 1 Then
		ldblValue = mclsValues.StringToType(Request.QueryString.Item("nUnits"), eFunctions.Values.eTypeData.etdDouble) * mclsValues.StringToType(Request.QueryString.Item("nExchange"), eFunctions.Values.eTypeData.etdDouble)
		Call lclsExchange.Convert(0, ldblValue, 1, Session("nCurrency"), Session("dEffecdate"), 0, True)
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnValueChange.value=VTFormat('" & lclsExchange.pdblResult & "', '', '', '', 6, false);")
	Else
		Call lclsExchange.Convert(0, mclsValues.StringToType(Request.QueryString.Item("nValueChange"), eFunctions.Values.eTypeData.etdDouble), Session("nCurrency"), 1, Session("dEffecdate"), 0, True)
		ldblValue = lclsExchange.pdblResult / mclsValues.StringToType(Request.QueryString.Item("nExchange"), eFunctions.Values.eTypeData.etdDouble)
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnUnitsChange.value=VTFormat('" & ldblValue & "', '', '', '', 6, false);")
	End If
	
	lclsExchange = Nothing
	
	If CDbl(Request.QueryString.Item("nSignal")) = 1 Then
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnBuy_cost.value=VTFormat('" & (mclsValues.StringToType(Request.QueryString.Item("nUnits"), eFunctions.Values.eTypeData.etdDouble) * mclsValues.StringToType(Request.QueryString.Item("nExchange"), eFunctions.Values.eTypeData.etdDouble) * (mclsValues.StringToType(Request.QueryString.Item("nBuyCost"), eFunctions.Values.eTypeData.etdDouble) / 100)) & "', '', '', '', 6, true);")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnSell_cost.value=VTFormat('" & 0 & "', '', '', '', 6, true);")
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnBuy_cost.value=VTFormat('" & 0 & "', '', '', '', 6, true);")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnSell_cost.value=VTFormat('" & (mclsValues.StringToType(Request.QueryString.Item("nUnits"), eFunctions.Values.eTypeData.etdDouble) * mclsValues.StringToType(Request.QueryString.Item("nExchange"), eFunctions.Values.eTypeData.etdDouble) * (mclsValues.StringToType(Request.QueryString.Item("nSellCost"), eFunctions.Values.eTypeData.etdDouble) / 100)) & "', '', '', '', 6, true);")
	End If
	
	Response.Write("top.frames['fraFolder'].document.forms[0].tcnDeb_acc.value=VTFormat('" & mclsValues.StringToType(Request.QueryString.Item("nSellCost"), eFunctions.Values.eTypeData.etdDouble) + mclsValues.StringToType(Request.QueryString.Item("nBuyCost"), eFunctions.Values.eTypeData.etdDouble) + mclsValues.StringToType(Request.QueryString.Item("nSwith"), eFunctions.Values.eTypeData.etdDouble) & "', '', '', '', 6, true);")
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
		If lclsPolicy_po.nProduct <> CDbl("") Then
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
			.Write("   tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
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
			
			ldtmNewNextreceip = mclsValues.TypeToString(.DefaultValueCA642("DateNextreceip"), eFunctions.Values.eTypeData.etdDate)
			
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
	If lclsUsers.Find(Session("nUsercode")) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & mclsValues.TypeToString(lclsUsers.nOffice, eFunctions.Values.eTypeData.etdDouble) & "';")
		
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.value='" & mclsValues.TypeToString(lclsUsers.nOfficeagen, eFunctions.Values.eTypeData.etdDouble) & "';")
		
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.value='" & mclsValues.TypeToString(lclsUsers.nAgency, eFunctions.Values.eTypeData.etdDouble) & "';")
		
		Response.Write("top.frames['fraHeader'].$('#cbeOfficeAgen').change();")
		Response.Write("top.frames['fraHeader'].$('#cbeAgency').change();")
		
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
	
	Response.Write("ShowPopUp('/VTimeNet/common/GoTo.aspx?sCodispl=" & Request.QueryString.Item("sForm") & "&nMainAction=' + '" & Request.QueryString.Item("nMainAction") & lstrParams & "','','0','0','yes','yes');")
End Sub

'% insProcessCAL036: Se ejecuta el proceso de facturación de colectivos
'--------------------------------------------------------------------------------------------
Private Function insProcessCAL036() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsOut_moveme As ePolicy.Out_moveme
	
	lclsOut_moveme = New ePolicy.Out_moveme
	If lclsOut_moveme.insProcessCAL036(mclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nCertifCA039"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeMov"), mclsValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nMonth"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nSituation"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nGroup"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nTratypei"), eFunctions.Values.eTypeData.etdDouble), Session("sClient"), mclsValues.StringToType(Session("dLedgerDate"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("dStart"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Session("dEnd"), eFunctions.Values.eTypeData.etdDate)) Then
		Response.Write("insReloadTop(false);")
		insProcessCAL036 = True
	End If
	
	lclsOut_moveme = Nothing
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
	Dim mstrQueryString As String
	Dim mstrCommand As String
	'--------------------------------------------------------------------------
	
	'-Objeto de conversion par eliminar datos
	Dim lclsTConvertions As ePolicy.TConvertions
	Dim insValPolicyTra As String
	'response.Write "alert('dos');"
	'response.Write "alert('"&Request.QueryString&"');"
	'Response.Write "alert('Field 1 = '+'"&Request.QueryString("Field")&"');"
	'Response.Write "alert('sOrigen 1 = '+'"&Request.QueryString("sOrigen")&"');"
	lclsTConvertions = New ePolicy.TConvertions
	Dim mobjValues As eFunctions.Values
	With mclsValues
		insValPolicyTra = vbNullString
		insValPolicyTra = lclsTConvertions.insValCA099(Request.QueryString.Item("nOperat"), .StringToType(Request.QueryString.Item("nNoConvers"), eFunctions.Values.eTypeData.etdDouble, True), .StringToType(Request.QueryString.Item("nStatus"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sCertype"), .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dDate_init"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble), .StringToType(CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
		'Response.Write "alert('sOrigen = '+'"&Request.QueryString("sOrigen")&"');"                                                         
		If Request.QueryString.Item("sOrigen") = "showdefvalues" Then
			insValPolicyTra = vbNullString
		End If
		If insValPolicyTra = vbNullString Then
			'Response.Write "alert('"&"post"&"');"        
			Call lclsTConvertions.insPostCA099("PopUp", "Update", .StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sPen_doc"), .StringToType(Request.QueryString.Item("dDate_init"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nStatus"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dStat_date"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nNoConvers"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dLimit_date"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sObserv"), .StringToType(Request.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nStatus_ord"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nFirst_prem"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPrem_curr"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sPrem_che"), Request.QueryString.Item("sPay_order"), .StringToType(Request.QueryString.Item("nExpenses"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDevolut"), Request.QueryString.Item("sCertype"), .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble), .StringToType("1", eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nWait_Code"), eFunctions.Values.eTypeData.etdDouble))
		Else
			mobjValues = New eFunctions.Values
			
			mstrCommand = "sModule=Policy&sProject=PolicyTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")
			Session("sErrorTable") = insValPolicyTra
			Session("sForm") = Request.Form.ToString
			mstrQueryString = "&sOrigen=" & "showdefvalues"
			Response.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & mstrQueryString & """, ""PolicyTraError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			
		End If
	End With
	
	lclsTConvertions = Nothing
	
	If Request.QueryString.Item("sOrigen") = "showdefvalues" Then
		Response.Write("opener.top.frames['fraFolder'].document.location.reload();")
		Response.Write("window.close();")
	Else
		Response.Write("top.frames['fraFolder'].document.location.reload();")
	End If
	
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
		Response.Write("cbeOfficeAgen.value='" & mclsValues.StringToType(CStr(lclsPolicy.nOfficeagen), eFunctions.Values.eTypeData.etdDouble) & "';")
		Response.Write("cbeAgency.value='" & mclsValues.StringToType(CStr(lclsPolicy.nAgency), eFunctions.Values.eTypeData.etdDouble) & "';")
		
		Response.Write("top.frames['" & lstrFrame & "'].$('#cbeOfficeAgen').change();")
		Response.Write("top.frames['" & lstrFrame & "'].$('#cbeAgency').change();")
		
		If IsNothing(Request.QueryString.Item("sCodisplOri")) Then
			Response.Write("optExecute[0].checked = true;")
			Response.Write("optExecute[1].checked = false;")
			Response.Write("optExecute[1].disabled = true;")
		End If
		
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=""0"";")
				If Request.QueryString.Item("sForm") <> "VI011" Then
					Response.Write("hdddStardate.value='" & mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
				End If
				Session("dStartdate") = mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate)
				
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
		If lclsAgencies.nOfficeagen > 0 Then
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & lclsAgencies.nBran_off & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsAgencies.nBran_off & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & mclsValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.value='" & lclsAgencies.nOfficeagen & "';")
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
				If lclsAgencies.nOfficeagen > 0 Then
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & lclsAgencies.nBran_off & "';")
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsAgencies.nBran_off & ";")
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & .nAgency & ";")
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.Parameters.Param1.sValue =" & lclsAgencies.nBran_off & ";")
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.Parameters.Param2.sValue =" & .nAgency & ";")
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeOfficeAgen.value='" & lclsAgencies.nOfficeagen & "';")
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
		If lclsAgencies.nOfficeagen > 0 Then
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeOffice.value='" & lclsAgencies.nBran_off & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsAgencies.nBran_off & ";")
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & mclsValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.value='" & lclsAgencies.nOfficeagen & "';")
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

'%insUpdVi010: Permite realizar la redistribución de los fondos de una póliza. 
'Es decir, vender TODAS las unidades disponibles y luego hacer la compra según 
'los nuevos porcentajes de participación y/o fondos
'-------------------------------------------------------------------------- 
Private Sub insUpdVi010()
	'-------------------------------------------------------------------------- 
	'-Objeto de conversion par eliminar datos 
        Dim mobjPolicyTra As ePolicy.ValPolicyTra
	mobjPolicyTra = New ePolicy.ValPolicyTra
	
	Dim bResult As Boolean
	
	With mclsValues
		'bResult = mobjPolicyTra.insPostVI010_Distribute(.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nSwi_cost"), eFunctions.Values.eTypeData.etdDouble))
		If Not bResult Then
			Response.Write("alert (""" & "No se pudo realizar la Distribución, intentarlo en forma manual " & """);")
		End If
	End With
	
	mobjPolicyTra = Nothing
	Response.Write("top.frames['fraFolder'].document.location.reload();")
	' 	    insSubmitPage();
End Sub

'%insUpdVi7002: Permite actualizar al realizar un check en la vi010 de todos 
'%             los registros marcados de la grilla. 
'-------------------------------------------------------------------------- 
Private Sub insUpdVi7002()
	Dim mstrCommand As String
	'-------------------------------------------------------------------------- 
	Dim lclsFunds_Pol As ePolicy.Funds_Pol
	Dim sActivFound As String
	Dim insValPolicyTra As String
	
	lclsFunds_Pol = New ePolicy.Funds_Pol
	With Request
		
		If .QueryString.Item("Action") = "Del" Then
			If CBool(.QueryString.Item("sActivFound")) Then
				sActivFound = "1"
			Else
				sActivFound = "2"
			End If
			
			insValPolicyTra = vbNullString
			insValPolicyTra = lclsFunds_Pol.insValVI006(.QueryString.Item("sCodispl"), CStr(1), "NormalDel", mclsValues.StringToType(.QueryString.Item("nFunds"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPartic_min"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 12, mclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "2", sActivFound)
			
			If insValPolicyTra = vbNullString Then
				Call lclsFunds_Pol.insPostVI006(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), CInt(.QueryString.Item("nFunds")), CInt(.QueryString.Item("nParticip")), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nUsercode"), mclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mclsValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), 12, sActivFound, "1")
			Else
				mstrCommand = "sModule=Policy&sProject=PolicyTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")
				Session("sErrorTable") = insValPolicyTra
				Session("sForm") = Request.Form.ToString
				Response.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""PolicyTraError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			End If
		End If
	End With
	lclsFunds_Pol = Nothing
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

'----------------------------------------------------------------------------
Private Sub Find_Type_Amend()
	'----------------------------------------------------------------------------
	
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsPolicy_his As ePolicy.Policy_his
	
	lclsPolicy = New ePolicy.Policy
	Call lclsPolicy.FindPolPropbyPolicy(CDbl(Request.QueryString.Item("nProponum")))
	If lclsPolicy.nPolicy <> eRemoteDB.Constants.intNull Then
		lclsPolicy_his = New ePolicy.Policy_his
		If lclsPolicy_his.reaPolicy_his_typeamend(lclsPolicy.sCertype, lclsPolicy.nBranch, lclsPolicy.nProduct, lclsPolicy.nPolicy, 0, lclsPolicy.nPolicy) Then
			
			Response.Write("opener.document.forms[0].hddnTypeAmend = " & lclsPolicy_his.nType_amend & ";")
		End If
		lclsPolicy_his = Nothing
	End If
End Sub

Private Sub insFindDocumentCA100()
        '-----------------------------------------------------------------------------------
	Dim lobjDocument As ePolicy.Policy
	Dim lobjCertificat As Object
	Dim mobjValues As eFunctions.Values
	
	mobjValues = New eFunctions.Values
	
	Select Case Request.QueryString.Item("sDocument")
		Case "policy"
			lobjDocument = New ePolicy.Policy
			With lobjDocument
				
				If .FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='0';")
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & .nBranch & ";")
					Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & .nBranch & ";")
					Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & .nProduct & ";")
					Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=0;")
					Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='';")
					Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
				End If
			End With
			
		Case "Certif"
                'Call insFindCertificat(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
			
			
	End Select
	lobjDocument = Nothing
	lobjCertificat = Nothing
End Sub
'% FindPolicyVI7006: Obtiene la informción de la póliza en tratamiento
'-------------------------------------------------------------------------- 
Private Sub FindPolicyVI7006()
	'-------------------------------------------------------------------------- 
	Dim lclsPolicy As ePolicy.Policy
	Dim strFrame As String
	lclsPolicy = New ePolicy.Policy
	
	strFrame = Request.QueryString.Item("sFrameCaller")
	Response.Write("with (top.frames['" & strFrame & "'].document.forms[0]){")
	If lclsPolicy.FindPolicybyPolicy("2", mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
		Response.Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
		Response.Write("valProduct.value=" & lclsPolicy.nProduct & ";")
		If lclsPolicy.nProduct <> CDbl("") Then
			Response.Write("top.frames['" & strFrame & "'].$('#valProduct').change();")
		End If
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=0;")
			Case "2", "3"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.focus();")
		End Select
	Else
		Response.Write("cbeBranch.value=0;")
		Response.Write("valProduct.Parameters.Param1.sValue=0;")
		Response.Write("valProduct.value='';")
		Response.Write("top.frames['" & strFrame & "'].UpdateDiv('valProductDesc', '');")
		Response.Write("tcnCertif.value='';")
	End If
	Response.Write("}")
	lclsPolicy = Nothing
End Sub

'% FindPolicyVI7006_Popup: Obtiene la informción de la póliza en tratamiento
'-------------------------------------------------------------------------- 
Private Sub FindPolicyVI7006_Popup()
	'-------------------------------------------------------------------------- 
	Dim lclsPolicy As ePolicy.Policy
	Dim strFrame As String
	lclsPolicy = New ePolicy.Policy
	
	strFrame = Request.QueryString.Item("sFrameCaller")
	Response.Write("with (top.frames['" & strFrame & "'].document.forms[0]){")
	If lclsPolicy.FindPolicybyPolicy("2", mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("cbeBranchNew.value=" & lclsPolicy.nBranch & ";")
		Response.Write("valProductNew.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
		Response.Write("valProductNew.value=" & lclsPolicy.nProduct & ";")
		If lclsPolicy.nProduct <> CDbl("") Then
			Response.Write("top.frames['" & strFrame & "'].$('#valProductNew').change();")
		End If
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertifNew.disabled=true;")
				Response.Write("tcnCertifNew.value=0;")
			Case "2", "3"
				Response.Write("tcnCertifNew.disabled=false;")
				Response.Write("tcnCertifNew.focus();")
		End Select
	Else
		Response.Write("cbeBranchNew.value=0;")
		Response.Write("valProductNew.Parameters.Param1.sValue=0;")
		Response.Write("valProductNew.value='';")
		Response.Write("top.frames['" & strFrame & "'].UpdateDiv('valProductNewDesc', '');")
		Response.Write("tcnCertifNew.value='';")
	End If
	Response.Write("}")
	lclsPolicy = Nothing
End Sub

'% insUpdSelVI7006: Se encarga de actualizar el campo sel de la transacción VI7006.
'--------------------------------------------------------------------------------------------
Private Sub insUpdSelVI7006()
	'--------------------------------------------------------------------------------------------
	Dim lclsUl_Move_Acc_Pol As ePolicy.ul_move_acc_pol
	
	lclsUl_Move_Acc_Pol = New ePolicy.ul_move_acc_pol
	
	'Call lclsUl_Move_Acc_Pol.insPostVI7006Upd(Request.QueryString.Item("sKey"), mclsValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, 2, mclsValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdLong))
	
	Response.Write("top.frames['fraFolder'].document.location.reload();")
	
	lclsUl_Move_Acc_Pol = Nothing
End Sub

'----------------------------------------------------------------------------
Private Sub insRenewExpirdat()
	'----------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsCertificat As ePolicy.Certificat
	Dim ldtmDate As Object
	
	lclsPolicy = New ePolicy.Policy
	lclsCertificat = New ePolicy.Certificat
	
	If lclsCertificat.Find(Request.QueryString.Item("sCertype"), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif"))) Then
		
		ldtmDate = mclsValues.StringToType(Request.QueryString.Item("dRenDate"), eFunctions.Values.eTypeData.etdDate)
            'ldtmDate = lclsPolicy.insCalRenewDate(lclsCertificat.nPayfreq, ldtmDate, lclsCertificat.nDuration, lclsCertificat.dExpirdat)
		
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdNextReceip.value='" & mclsValues.TypeToString(ldtmDate, eFunctions.Values.eTypeData.etdDate) & "';")
	End If
	
End Sub


'--------------------------------------------------------------------------

Private Sub FindPolicycal972()
	'-------------------------------------------------------------------------- 
	Dim lclsPolicy As ePolicy.Policy
	Dim strFrame As String
	lclsPolicy = New ePolicy.Policy
	
	strFrame = Request.QueryString.Item("sFrameCaller")
	Response.Write("with (top.frames['" & strFrame & "'].document.forms[0]){")
	If lclsPolicy.FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
		Response.Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
		Response.Write("valProduct.value=" & lclsPolicy.nProduct & ";")
		If lclsPolicy.nProduct <> CDbl("") Then
			Response.Write("top.frames['" & strFrame & "'].$('#valProduct').change();")
		End If
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=0;")
			Case "2", "3"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.focus();")
		End Select
	Else
		Response.Write("cbeBranch.value=0;")
		Response.Write("valProduct.Parameters.Param1.sValue=0;")
		Response.Write("valProduct.value='';")
		Response.Write("top.frames['" & strFrame & "'].UpdateDiv('valProductDesc', '');")
		Response.Write("tcnCertif.value='';")
	End If
	Response.Write("}")
	lclsPolicy = Nothing
End Sub


'% FindPolicyVI7004: Obtiene la informción de la póliza y contratante en tratamiento
'----------------------------------------------------------------------------------- 
Private Sub FindPolicyVI7004()
	'-----------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim strFrame As String
	
	lclsPolicy = New ePolicy.Policy
	
	strFrame = Request.QueryString.Item("sFrameCaller")
	Response.Write("with (top.frames['" & strFrame & "'].document.forms[0]){")
	Dim lclsRoles As ePolicy.Roles
	If lclsPolicy.FindPolicybyPolicy("2", mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
		Response.Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
		Response.Write("valProduct.value=" & lclsPolicy.nProduct & ";")
		If lclsPolicy.nProduct <> CDbl("") Then
			Response.Write("top.frames['" & strFrame & "'].$('#valProduct').change();")
		End If
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=0;")
			Case "2", "3"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.focus();")
		End Select
		
		lclsRoles = New ePolicy.Roles
		With Request
			If lclsRoles.Find(lclsPolicy.sCertype, mclsValues.StringToType(CStr(lclsPolicy.nBranch), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy.nProduct), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy.nPolicy), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy.nCertif), eFunctions.Values.eTypeData.etdDouble), 1, "", mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
				
				Response.Write("tctClient.value='" & lclsRoles.sClient & "';")
				Response.Write("tctClient_Digit.value='" & lclsRoles.sDigit & "';")
				Response.Write("top.frames['" & strFrame & "'].UpdateDiv('tctClient_Name','" & lclsRoles.sCliename & "');")
			Else
				Response.Write("tctClient.value='';")
				Response.Write("tctClient_Digit.value='';")
				Response.Write("top.frames['" & strFrame & "'].UpdateDiv('tctClient_Name','');")
			End If
		End With
		lclsRoles = Nothing
	Else
		Response.Write("cbeBranch.value=0;")
		Response.Write("valProduct.Parameters.Param1.sValue=0;")
		Response.Write("valProduct.value='';")
		Response.Write("top.frames['" & strFrame & "'].UpdateDiv('valProductDesc', '');")
		Response.Write("tcnCertif.value='';")
		Response.Write("tctClient.value='';")
		Response.Write("tctClient_Digit.value='';")
		Response.Write("top.frames['" & strFrame & "'].UpdateDiv('tctClient_Name','');")
	End If
	Response.Write("}")
	lclsPolicy = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mclsValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.22
mclsValues.sSessionID = Session.SessionID
mclsValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mclsValues.sCodisplPage = "showdefvalues"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT>
    //+ Variable para el control de versiones
        document.VssVersion="$$Revision:   1.18  $|$$Date:   Apr 05 2006 08:42:16  $|$$Author:   jpleteli  $"  
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
		'***************************
		'* PARTNER                 *
		'***************************
	Case "CA100"
		Call insFindDocumentCA100()
		'***************************
		'* PARTNER                 *
		'***************************
	Case "PolicyVI7006"
		Call FindPolicyVI7006()
	Case "PolicyVI7006_Popup"
		Call FindPolicyVI7006_Popup()
	Case "UpdSelVI7006"
		Call insUpdSelVI7006()
	Case "renewExpirdat"
		Call insRenewExpirdat()
	Case "CertifNum"
		Call insShowCertifNum()
	Case "Policycal972"
		Call FindPolicycal972()
	Case "PolicyVI7004"
		Call FindPolicyVI7004()
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing


%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.22
Call mobjNetFrameWork.FinishPage("showdefvalues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





