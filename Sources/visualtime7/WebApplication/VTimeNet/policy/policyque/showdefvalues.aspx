<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para manejo generico	
Dim mclsValues As eFunctions.Values


'% FindLocalProv: Se busca la localidad y la provincia una vez incluído el código postal.
'--------------------------------------------------------------------------------------------
Private Sub ReaCurren_pol()
	'--------------------------------------------------------------------------------------------
	'- Objeto para busqueda de datos
	Dim lclsCurren_pol As ePolicy.Curren_pol
	
	lclsCurren_pol = New ePolicy.Curren_pol
	
	If lclsCurren_pol.Find_Currency_Sel(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0, mclsValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		If lclsCurren_pol.nCount = 1 Then
			Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
			Response.Write("    valCurrency.disabled=true;")
			Response.Write("    btnvalCurrency.disabled=true;")
			Response.Write("    valCurrency.value='" & lclsCurren_pol.nCurrency & "';")
			Response.Write("    top.frames['fraHeader'].$('#valCurrency).change();")
			Response.Write("}")
		ElseIf lclsCurren_pol.nCount > 1 Then 
			Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
			Response.Write("    valCurrency.disabled=false;")
			Response.Write("    btnvalCurrency.disabled=false;")
			Response.Write("}")
		End If
	Else
		Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
		Response.Write("    valCurrency.disabled=false;")
		Response.Write("    btnvalCurrency.disabled=false;")
		Response.Write("}")
	End If
	
	lclsCurren_pol = Nothing
End Sub

'% FindLocalProv: Se busca la localidad y la provincia una vez incluído el código postal.
'--------------------------------------------------------------------------------------------
Private Sub FindLocalProv()
	'--------------------------------------------------------------------------------------------
	'- Objeto para busqueda de datos
	Dim lclsTab_locat As eGeneralForm.Tab_locat
	lclsTab_locat = New eGeneralForm.Tab_locat
	If lclsTab_locat.Find_Default(mclsValues.StringToType(Request.QueryString.Item("nZip_code"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("opener.document.forms[0].valLocal.value=" & lclsTab_locat.nLocal & ";")
		Response.Write("opener.$('#valLocal').change();")
		Response.Write("UpdateDiv('tctProvince','" & lclsTab_locat.sDescript & "','PopUp');")
	Else
		Response.Write("opener.document.forms[0].valLocal.value = '';")
		Response.Write("opener.document.forms[0].tctProvince = '';")
	End If
	lclsTab_locat = Nothing
End Sub

'% FindAccPolDat: Se busca los datos de asociadas a la cuenta de la poliza
'--------------------------------------------------------------------------------------------
Private Sub FindAccPolDat()
	'--------------------------------------------------------------------------------------------
	'- Variables para busqueda de datos
	Dim lclsAccount_pol As ePolicy.Account_Pol
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsTables As eFunctions.Tables
	Dim lclsRoles As ePolicy.Roles
	Dim lblnFind As Boolean
	Dim nIntCertif As Object
	
	lclsTables = New eFunctions.Tables
	lclsAccount_pol = New ePolicy.Account_Pol
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	
	With Request
		Response.Write("var errDat;")
		Response.Write("try{")
		Response.Write("with(top.frames['fraHeader']){")
		
		nIntCertif = .QueryString.Item("nCertif")
		
		If nIntCertif = "" Then
			If lclsPolicy.Find(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), True) Then
				
				Select Case lclsPolicy.sPolitype
					Case "1"
						Response.Write("document.forms[0].tcnCertif.disabled=true;")
						Response.Write("document.forms[0].tcnCertif.value=""0"";")
						nIntCertif = 0
					Case "2"
						Response.Write("document.forms[0].tcnCertif.disabled=false;")
					Case "3"
						Response.Write("document.forms[0].tcnCertif.disabled=false;")
				End Select
			End If
		End If
		
		Session("nIntCertif") = nIntCertif
		Session("sPoliType") = lclsPolicy.sPolitype
		
		lblnFind = lclsAccount_pol.Find(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(nIntCertif, eFunctions.Values.eTypeData.etdDouble))
		If lblnFind Then
			Call lclsTables.reaTable("Table11", lclsAccount_pol.nCurrency)
			Call lclsRoles.Find(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(nIntCertif, eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), "", mclsValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
			
			Session("sDivCurrency") = lclsTables.Fields("sDescript")
		End If
	End With
	
	With Response
		If lblnFind Then
			.Write("UpdateDiv('divCurrency', '" & lclsTables.Fields("sDescript") & "');")
			.Write("UpdateDiv('divLastDate', '" & mclsValues.TypeToString(lclsAccount_pol.dLastdate, eFunctions.Values.eTypeData.etdDate) & "');")
			.Write("UpdateDiv('divLastPay', '" & mclsValues.TypeToString(lclsAccount_pol.dLastpay, eFunctions.Values.eTypeData.etdDate) & "');")
			.Write("UpdateDiv('divVP_neg', '" & mclsValues.TypeToString(lclsAccount_pol.dVp_neg, eFunctions.Values.eTypeData.etdDate) & "');")
			.Write("UpdateDiv('divPays', '" & mclsValues.insReturnUserNumber(lclsAccount_pol.nPays, True, 2) & "');")
			.Write("UpdateDiv('divFixCharge', '" & mclsValues.insReturnUserNumber(lclsAccount_pol.nFixcharge, True, 2) & "');")
			.Write("UpdateDiv('divCoverCost', '" & mclsValues.insReturnUserNumber(lclsAccount_pol.nCovercost, True, 2) & "');")
			.Write("UpdateDiv('divNetPays', '" & mclsValues.insReturnUserNumber(lclsAccount_pol.nNetpays, True, 2) & "');")
			.Write("UpdateDiv('divProfit', '" & mclsValues.insReturnUserNumber(lclsAccount_pol.nProfit, True, 2) & "');")
			.Write("UpdateDiv('divAmoSurren', '" & mclsValues.insReturnUserNumber(lclsAccount_pol.nAmosurren, True, 2) & "');")
			.Write("UpdateDiv('divValuePol', '" & mclsValues.insReturnUserNumber(lclsAccount_pol.nValuepol, True, 2) & "');")
			.Write("UpdateDiv('divContracting', '" & lclsRoles.sClient & " " & lclsRoles.sCliename & "');")
		Else
			.Write("UpdateDiv('divCurrency', '');")
			.Write("UpdateDiv('divLastDate', '');")
			.Write("UpdateDiv('divLastPay', '');")
			.Write("UpdateDiv('divVP_neg', '');")
			.Write("UpdateDiv('divPays', '');")
			.Write("UpdateDiv('divFixCharge', '');")
			.Write("UpdateDiv('divCoverCost', '');")
			.Write("UpdateDiv('divNetPays', '');")
			.Write("UpdateDiv('divProfit', '');")
			.Write("UpdateDiv('divAmoSurren', '');")
			.Write("UpdateDiv('divValuePol', '');")
			.Write("UpdateDiv('divContracting', '');")
		End If
		.Write("}}catch(errDat){}")
	End With
	lclsAccount_pol = Nothing
	lclsPolicy = Nothing
	lclsTables = Nothing
	lclsRoles = Nothing
End Sub

'% FindMoveAccPolDat: Busca datos de movimiento de poliza
'--------------------------------------------------------------------------------------------
Private Sub FindMoveAccPolDat()
	'--------------------------------------------------------------------------------------------
	'- Variable para busqueda de datos
	Dim lclsMove_Accpol As ePolicy.Move_accpol
	
        lclsMove_Accpol = New ePolicy.Move_Accpol
	
	With Request
		
		If lclsMove_Accpol.Find(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nIdMov"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("with(top.frames['fraHeader']){")
			Response.Write("    UpdateDiv('divMoveType', '" & lclsMove_Accpol.sTypemove & "');")
			Response.Write("    UpdateDiv('divMoveDate', '" & mclsValues.TypeToString(lclsMove_Accpol.dMovdate, eFunctions.Values.eTypeData.etdDate) & "');")
			Response.Write("    UpdateDiv('divAmount', '" & mclsValues.insReturnUserNumber(lclsMove_Accpol.nAmount, True, 2) & "');")
			Response.Write("    UpdateDiv('divReceipt', '" & mclsValues.TypeToString(lclsMove_Accpol.nReceipt, eFunctions.Values.eTypeData.etdDouble) & "');")
			Response.Write("}")
			Session("sMoveType") = lclsMove_Accpol.sTypemove
			Session("dMoveDate") = mclsValues.TypeToString(lclsMove_Accpol.dMovdate, eFunctions.Values.eTypeData.etdDate)
			Session("nDivAmount") = mclsValues.insReturnUserNumber(lclsMove_Accpol.nAmount, True, 2)
			Session("nDivReceipt") = mclsValues.TypeToString(lclsMove_Accpol.nReceipt, eFunctions.Values.eTypeData.etdDouble)
		Else
			Response.Write("with(top.frames['fraHeader']){")
			Response.Write("    UpdateDiv('divMoveType', '');")
			Response.Write("    UpdateDiv('divMoveDate', '');")
			Response.Write("    UpdateDiv('divAmount', '');")
			Response.Write("    UpdateDiv('divReceipt', '');")
			Response.Write("}")
		End If
	End With
	lclsMove_Accpol = Nothing
End Sub

'% FindCurrMoveDat: Busca datos de moneda y movimiento de poliza
'--------------------------------------------------------------------------------------------
Private Sub FindCurrMoveDat()
	'--------------------------------------------------------------------------------------------
	'- Variables de objeto para busqueda de datos
        Dim lclsMove_Accpol As ePolicy.Move_Accpol
	Dim lclsAccount_pol As ePolicy.Account_Pol
	Dim lclsTables As eFunctions.Tables
	Dim lblnFind As Boolean
	
	lclsAccount_pol = New ePolicy.Account_Pol
	lclsTables = New eFunctions.Tables
	lclsMove_Accpol = New ePolicy.Move_Accpol
	
	With Request
		Response.Write("var errDat;")
		Response.Write("try{")
		Response.Write("with(top.frames['fraHeader']){")
		
		lblnFind = lclsAccount_pol.Find(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
		
		If lblnFind Then
			Call lclsTables.reaTable("Table11", lclsAccount_pol.nCurrency)
			lblnFind = lclsMove_Accpol.Find(.QueryString.Item("sCertype"), mclsValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mclsValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mclsValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mclsValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(.QueryString.Item("nIdMov"), eFunctions.Values.eTypeData.etdDouble))
		End If
	End With
	With Response
		If lblnFind Then
			.Write("    UpdateDiv('divCurrency', '" & lclsTables.Fields("sDescript") & "');")
			.Write("    UpdateDiv('divMoveType', '" & lclsMove_Accpol.sTypemove & "');")
			.Write("    UpdateDiv('divMoveDate', '" & mclsValues.TypeToString(lclsMove_Accpol.dMovdate, eFunctions.Values.eTypeData.etdDate) & "');")
			.Write("    UpdateDiv('divAmount', '" & mclsValues.insReturnUserNumber(lclsMove_Accpol.nAmount, True, 2) & "');")
			.Write("    UpdateDiv('divReceipt', '" & mclsValues.TypeToString(lclsMove_Accpol.nReceipt, eFunctions.Values.eTypeData.etdDouble) & "');")
		Else
			.Write("    UpdateDiv('divCurrency', '');")
			.Write("    UpdateDiv('divMoveType', '');")
			.Write("    UpdateDiv('divMoveDate', '');")
			.Write("    UpdateDiv('divAmount', '');")
			.Write("    UpdateDiv('divReceipt', '');")
		End If
		
		.Write("}}catch(errDat){}")
		
	End With
	
	lclsMove_Accpol = Nothing
	lclsAccount_pol = Nothing
	lclsTables = Nothing
End Sub

'% ReaMunicipalityDefault: Busca la ciudad y la región dada la comuna
'--------------------------------------------------------------------------------------------
Sub ReaMunicipalityDefault()
	Dim mobjValues As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_locat As eGeneralForm.Tab_locat
	lclsTab_locat = New eGeneralForm.Tab_locat
	
	With lclsTab_locat
		If Not IsNothing(Request.QueryString.Item("nMunicipality")) Then
			If .Find_by_municipality(mobjValues.StringToType(Request.QueryString.Item("nMunicipality"), eFunctions.Values.eTypeData.etdDouble)) Then
				
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
'% insValPolitype: valida el tipo de póliza para habilitar/deshabilitar el certificado
'% Debe ser invocada con funcion insDefValues
'--------------------------------------------------------------------------------------------
Sub insValPolitype()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrFrame As String
	
	lclsPolicy = New ePolicy.Policy
	
	lstrFrame = Request.QueryString.Item("sFrame")
	If lstrFrame = vbNullString Then
		lstrFrame = "fraHeader"
	End If
	Response.Write("with(top.frames['" & lstrFrame & "'].document.forms[0]){")
	If lclsPolicy.Find(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		'+Asignación del Tipo de póliza
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=""0"";")
			Case "2"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value=""0"";")
				Response.Write("tcnCertif.focus();")
			Case "3"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.focus();")
		End Select
		If Request.QueryString.Item("sExecCertif") = "1" Then
			Response.Write("if(tcnCertif.disabled)")
			Response.Write("top.frames['" & lstrFrame & "'].$('#tcnCertif').change();")
		End If
	Else
		Response.Write("tcnCertif.disabled=true;")
		Response.Write("tcnCertif.value=""0"";")
	End If
	Response.Write("}")
	lclsPolicy = Nothing
End Sub
'% insValPolitype: valida el tipo de póliza para habilitar/deshabilitar el certificado
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
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=""0"";")
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

'% PrintCac001: Imprime el reporte de la Cac001
'--------------------------------------------------------------------------------------------
Sub PrintCac001()
	'--------------------------------------------------------------------------------------------
	
	Dim mobjDocuments As eReports.Report
	Dim vsCreditnum As String
	Dim vsAccnum As String
	
	If (IsNothing(Request.QueryString.Item("sCreditnum"))) Then
		vsCreditnum = "."
	Else
		vsCreditnum = Request.QueryString.Item("sCreditnum")
	End If
	
	If (IsNothing(Request.QueryString.Item("sAccnum"))) Then
		vsAccnum = "."
	Else
		vsAccnum = Request.QueryString.Item("sAccnum")
	End If
	mobjDocuments = New eReports.Report
	With mobjDocuments
		.ReportFilename = "CACL001.rpt"
		.sCodispl = "CAC001"
		.setParamField(1, "SKEY", Request.QueryString.Item("sKey"))
		.setParamField(2, "SCLIENT", Request.QueryString.Item("sClient"))
		.setParamField(3, "NBRANCH", Request.QueryString.Item("nBranch"))
		.setParamField(4, "NPRODUCT", Request.QueryString.Item("nProduct"))
		.setParamField(5, "NCURRRENT", Request.QueryString.Item("nCurrrent"))
		If Session("Pol_security") Then
			.setParamField(6, "SPOL_SECURITY", "1")
		Else
			.setParamField(6, "SPOL_SECURITY", "2")
		End If
		.setParamField(7, "sCertype", Request.QueryString.Item("sCertype"))
		.setParamField(8, "sState", Request.QueryString.Item("sState"))
		.setParamField(9, "nPolicy", Request.QueryString.Item("nPolicy"))
		.setParamField(10, "dStartdate", Request.QueryString.Item("dStartdate"))
		.setParamField(11, "sCreditnum", vsCreditnum)
		.setParamField(12, "sAccnum", vsAccnum)
		Response.Write((.Command))
		
	End With
	mobjDocuments = Nothing
	
End Sub

'% PrintVIC732: Imprime el reporte de la VIC732
'--------------------------------------------------------------------------------------------
Sub PrintVIC732()
	'--------------------------------------------------------------------------------------------
	Dim lclsDocuments As eReports.Report
	Dim lclsGeneral As eGeneral.GeneralFunction
	
	lclsDocuments = New eReports.Report
	lclsGeneral = New eGeneral.GeneralFunction
	With lclsDocuments
		.ReportFilename = "VIC732.rpt"
		.sCodispl = "VIC732"
		.setParamField(1, "sKey", lclsGeneral.getsKey(Session("nUsercode")))
		.setParamField(2, "sCertype", Request.QueryString.Item("sCertype"))
		.setParamField(3, "nBranch", Request.QueryString.Item("nBranch"))
		.setParamField(4, "nProduct", Request.QueryString.Item("nProduct"))
		.setParamField(5, "nPolicy", Request.QueryString.Item("nPolicy"))
		.setParamField(6, "nCertif", Request.QueryString.Item("nCertif"))
		.setParamField(7, "dEffecdate", .setdate(Request.QueryString.Item("dEffecdate")))
		Response.Write((.Command))
		
	End With
	lclsDocuments = Nothing
	lclsGeneral = Nothing
End Sub

</script>
<%Response.Expires = -1
mclsValues = New eFunctions.Values

Response.Write(mclsValues.StyleSheet())
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 10 $|$$Date: 11/05/04 19:28 $|$$Author: Nvaplat7 $"
</SCRIPT>	
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Select Case Request.QueryString.Item("Field")
	Case "PrintCac001"
		Call PrintCac001()
	Case "PrintVIC732"
		Call PrintVIC732()
End Select

Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "nZip_code"
		Call FindLocalProv()
	Case "AccPolDat"
		Call FindAccPolDat()
	Case "MoveAccDat"
		Call FindMoveAccPolDat()
	Case "CurrMoveAcc"
		Call FindCurrMoveDat()
	Case "Municipality"
		Call ReaMunicipalityDefault()
	Case "Curren_pol"
		Call ReaCurren_pol()
	Case "insValPolitype"
		Call insValPolitype()
	Case "ValPolitype"
		Call ValPolitype()
End Select
Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing
%>




