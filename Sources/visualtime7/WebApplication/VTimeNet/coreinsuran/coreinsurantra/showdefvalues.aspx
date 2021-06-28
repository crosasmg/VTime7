<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values


'% insShowDataContr: Se muestran los datos asociados al Numero de contrato introducido
'%                   Se utiliza para el campo Número de la página CR301_k.aspx
'--------------------------------------------------------------------------------------------
Sub insShowDataContr()
	'--------------------------------------------------------------------------------------------
	Dim lclsContrproc As eCoReinsuran.Contrproc
	Dim lobjValues As eFunctions.Values
	
	lclsContrproc = New eCoReinsuran.Contrproc
	lobjValues = New eFunctions.Values
	
	If lclsContrproc.Find(CInt(Request.QueryString.Item("nNumber")), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CDate(Request.QueryString.Item("dEffecdate")), True) Then
		With Response
			.Write("opener.document.forms[0].cbeContraType.value=" & lclsContrproc.nType & ";")
			.Write("opener.document.forms[0].cbeBranch_rei.value=" & lclsContrproc.nBranch & ";")
		End With
	Else
		With Response
			.Write("opener.document.forms[0].cbeContraType.value=0;")
			.Write("opener.document.forms[0].cbeBranch_rei.value=0;")
		End With
	End If
	
	lclsContrproc = Nothing
	lobjValues = Nothing
End Sub

'% insShowDataContr_np: Se muestran los datos asociados al Numero de contrato introducido
'%                      Se utiliza para el campo Número de la página CR304_k.aspx
'--------------------------------------------------------------------------------------------
Sub insShowDataContr_np()
	'--------------------------------------------------------------------------------------------
	Dim lclsContrnpro As eCoReinsuran.Contrnpro
	Dim lobjValues As eFunctions.Values
	
	lclsContrnpro = New eCoReinsuran.Contrnpro
	lobjValues = New eFunctions.Values
	
	If lclsContrnpro.Find(lobjValues.StringToType(Request.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, lobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then
		
		With Response
			.Write("opener.document.forms[0].cboContraType.value=" & lclsContrnpro.nType & ";")
			.Write("opener.document.forms[0].cboBranch.value=" & lclsContrnpro.nBranch & ";")
		End With
	End If
	
	lclsContrnpro = Nothing
	lobjValues = Nothing
End Sub


'% insShowCompanyName: Se muestra el nombre de la compañía seleccionada
'--------------------------------------------------------------------------------------------
Sub insShowCompanyName()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	Dim lclsCompany As eCoReinsuran.Company
	lclsCompany = New eCoReinsuran.Company
	
	lintCount = 0
	
	With lclsCompany
		If .insPreparedQuery(Request.QueryString.Item("nCompany"), vbNullString, vbNullString) Then
			If .ItemCompany(lintCount) Then
				Response.Write("opener.UpdateDiv(""tctCompanyName"",'" & lclsCompany.sCliename & "','Normal');")
			End If
		End If
	End With
End Sub

'% UpdateContrType: Se cambian los valores del tipo de contrato.
'--------------------------------------------------------------------------------------------
Sub UpdateContrType()
	'--------------------------------------------------------------------------------------------
	Dim lclsQuery As eRemoteDB.Query
	
	lclsQuery = New eRemoteDB.Query
	
	If Request.QueryString.Item("nReinsurance") = "1" Then
		Call lclsQuery.OpenQuery("Table173", "sDescript, nType_Rein", "nType_Rein=4")
	Else
		If Request.QueryString.Item("nReinsurance") = "2" Then
			Call lclsQuery.OpenQuery("Table173", "sDescript, nType_Rein", "nType_Rein in(2,3,5,6,7,8,9,10)")
		Else
			Call lclsQuery.OpenQuery("Table173", "sDescript, nType_Rein", "nType_Rein in(683,685,686,687,688)")
		End If
	End If
	Response.Write("top.fraHeader.document.forms[0].cbeContraType.disabled=false;")
	Response.Write("top.fraHeader.document.forms[0].cbeContraType.options.length=0;")
	
	Response.Write("var option = new Option('','0');")
	Response.Write("top.fraHeader.document.forms[0].cbeBranchRei.options.add(option,'0');")
	
	Do While Not lclsQuery.EndQuery
		Response.Write("var option = new Option('" & lclsQuery.FieldToClass("sDescript") & "','" & lclsQuery.FieldToClass("nType_Rein") & "');")
		Response.Write("top.fraHeader.document.forms[0].cbeContraType.options.add(option,'" & lclsQuery.FieldToClass("nType_Rein") & "');")
		If Request.QueryString.Item("nReinsurance") = "1" Then
			Response.Write("top.fraHeader.document.forms[0].cbeContraType.value=" & lclsQuery.FieldToClass("nType_Rein") & ";")
			Response.Write("top.fraHeader.document.forms[0].cbeContraType.disabled=true;")
		End If
		lclsQuery.NextRecord()
	Loop 
	
	If Request.QueryString.Item("DisabledField") = "1" Then
		Response.Write("top.fraHeader.document.forms[0].cbeContraType.disabled=true;")
	End If
	
	If Not CStr(Session("nType")) = "" Then
		If Request.QueryString.Item("nReinsurance") = "2" Or Request.QueryString.Item("nReinsurance") = "3" Then
			Response.Write("top.fraHeader.document.forms[0].cbeContraType.value=" & Session("nType") & ";")
		End If
	End If
	lclsQuery = Nothing
End Sub
'----------------------------------------------------
Sub ShowDefValuesCR781()
	
	
	Dim lclsTar_Hospitaliz As Object
	Dim lobjValues As eFunctions.Values
	
'UPGRADE_NOTE: The 'eCoReinsuran.Tar_Hospitaliz' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsTar_Hospitaliz = Server.CreateObject("eCoReinsuran.Tar_Hospitaliz")
	lobjValues = New eFunctions.Values
	
	
	If lclsTar_Hospitaliz.Find(lobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		With Response
			.Write("opener.document.forms[0].tcnPrem_Aseg.value=" & lclsTar_Hospitaliz.nPrem_Aseg & ";")
			.Write("opener.document.forms[0].tcnPrem_Adic.value=" & lclsTar_Hospitaliz.nPrem_Adic & ";")
		End With
		
	Else
		Response.Write("opener.document.forms[0].tcnPrem_Aseg.value=0;")
		Response.Write("opener.document.forms[0].tcnPrem_Adic.value=0;")
	End If
	
	lclsTar_Hospitaliz = Nothing
	lobjValues = Nothing
	
End Sub

</script>
<%Response.Expires = -1
mclsValues = New eFunctions.Values

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>


<!--% Response.Write "<NOTSCRIPT> alert(""" & Request.QueryString & """);</script>"%-->

<%Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "NumberContr"
		Call insShowDataContr()
	Case "NumberContr_np"
		Call insShowDataContr_np()
	Case "CompanyQuery"
		Call insShowCompanyName()
	Case "UpdateContrType"
		Call UpdateContrType()
	Case "ShowDefValuesCR781"
		Call ShowDefValuesCR781()
		
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")
'----------------------------------------------------
%>





