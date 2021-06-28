<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'%InsShowDocreq: Se obtiene la fecha del documento requerido.
'--------------------------------------------------------------------------------------------
Sub InsShowDocreq()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_req_doc As eClient.Tab_req_doc
	Dim lclsvalues As eFunctions.Values
	Dim ldtmDate As Date
	
	With Server
		lclsvalues = New eFunctions.Values
		lclsTab_req_doc = New eClient.Tab_req_doc
	End With
	
	If lclsTab_req_doc.Find(CInt(Request.QueryString.Item("nTypedoc"))) Then
		
		If lclsTab_req_doc.nQDays <> eRemoteDB.Constants.intNull Then
			ldtmDate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, lclsTab_req_doc.nQDays, lclsvalues.StringToType(Request.QueryString.Item("dDocdate"), eFunctions.Values.eTypeData.etdDate))
		Else
			ldtmDate = Today
		End If
	End If
	
	Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirdat.value='" & lclsvalues.TypeToString(ldtmDate, eFunctions.Values.eTypeData.etdDate) & "';")
	
	lclsTab_req_doc = Nothing
	lclsvalues = Nothing
End Sub

'%InsShowDocum: Muestra la información del documento.
'--------------------------------------------------------------------------------------------
Sub InsShowDocum()
	'--------------------------------------------------------------------------------------------
	Dim lclsClient As eClient.Client
	Dim lclsvalues As eFunctions.Values
	Dim lstrClient As String
	
	With Server
		lclsvalues = New eFunctions.Values
		lclsClient = New eClient.Client
	End With
	
	lstrClient = Request.QueryString.Item("sClient")
	
	If lclsClient.Find(lstrClient) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].tctFirstname.value='" & lclsClient.sFirstname & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctLastname.value='" & lclsClient.sLastname & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctLastname2.value='" & lclsClient.sLastName2 & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdBirthdat.value='" & lclsvalues.TypeToString(lclsClient.dBirthdat, eFunctions.Values.eTypeData.etdDate) & "';")
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tctFirstname.value='" & "" & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctLastname.value='" & "" & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctLastname2.value='" & "" & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdBirthdat.value='" & "" & "';")
	End If
	
	lclsClient = Nothing
	lclsvalues = Nothing
End Sub

'% InsShowEval: Muestra la información de las evaluaciones
'--------------------------------------------------------------------------------------------
Sub InsShowEval()
	'--------------------------------------------------------------------------------------------
	Dim lclsEval_master As eClient.eval_master
	Dim lclsvalues_e As eFunctions.Values
	
	With Server
		lclsvalues_e = New eFunctions.Values
		lclsEval_master = New eClient.eval_master
	End With
	
	If lclsEval_master.Find_eval(lclsvalues_e.StringToType(Request.QueryString.Item("nEval"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient")) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeEval.value='" & lclsvalues_e.TypeToString(lclsEval_master.nEval, eFunctions.Values.eTypeData.etdDouble) & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeStatus_eval.value='" & lclsvalues_e.TypeToString(lclsEval_master.nStatus_eval, eFunctions.Values.eTypeData.etdDouble) & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCapital.value='" & lclsvalues_e.TypeToString(lclsEval_master.nCapital, eFunctions.Values.eTypeData.etdDouble) & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeCurrency.value='" & lclsvalues_e.TypeToString(lclsEval_master.nCurrency, eFunctions.Values.eTypeData.etdDouble) & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCumul.value='" & lclsvalues_e.TypeToString(lclsEval_master.nCumul, eFunctions.Values.eTypeData.etdDouble) & "';")
	End If
	
	lclsEval_master = Nothing
	lclsvalues_e = Nothing
End Sub

'% InsShowDataPolPo: Muestra la información de las evaluaciones
'--------------------------------------------------------------------------------------------
Sub InsShowDataPolPo()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsvalues As eFunctions.Values
	
	lclsvalues = New eFunctions.Values
	lclsPolicy = New ePolicy.Policy
	
	If lclsPolicy.Find(Request.QueryString.Item("sCertype"), lclsvalues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), lclsvalues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), lclsvalues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		'+Asignación del Tipo de póliza
		Response.Write("with(top.frames['fraFolder'].document.forms[0]){")
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
			Case "2"
				Response.Write("tcnCertif.disabled=false;")
			Case "3"
				Response.Write("tcnCertif.disabled=false;")
		End Select
		Response.Write("tcnCertif.value=""0"";")
		Response.Write("}")
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.disabled=true;")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.value="""";")
	End If
	
	lclsPolicy = Nothing
	lclsvalues = Nothing
End Sub
'% InsShowDataPol: Muestra la información de las evaluaciones
'--------------------------------------------------------------------------------------------
Sub InsShowDataPol()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsvalues As eFunctions.Values
	
	lclsvalues = New eFunctions.Values
	lclsPolicy = New ePolicy.Policy
	
	If lclsPolicy.Find(Request.QueryString.Item("sCertype"), lclsvalues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), lclsvalues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), lclsvalues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		'+Asignación del Tipo de póliza
		Response.Write("with(top.frames['fraHeader'].document.forms[0]){")
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
			Case "2"
				Response.Write("tcnCertif.disabled=false;")
			Case "3"
				Response.Write("tcnCertif.disabled=false;")
		End Select
		Response.Write("tcnCertif.value=""0"";")
		Response.Write("}")
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value="""";")
	End If
	
	lclsPolicy = Nothing
	lclsvalues = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values


%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM NAME="ShowDefValues">
</FORM>
</BODY>
</HTML>
<%Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Client"
		InsShowDocum()
	Case "Eval"
		InsShowEval()
	Case "Docreq"
		InsShowDocreq()
	Case "DataPol"
		InsShowDataPol()
	Case "DataPolPo"
		InsShowDataPolPo()
		
		
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>




