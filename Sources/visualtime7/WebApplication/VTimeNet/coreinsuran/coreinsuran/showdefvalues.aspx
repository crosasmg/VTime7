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

'% insShowDataContr: Se muestran los datos asociados al Numero de contrato introducido
'%                   Se utiliza para el campo Número de la página CR301_k.aspx
'--------------------------------------------------------------------------------------------
Sub insShowDateContr()
	'--------------------------------------------------------------------------------------------
	Dim lclsContrproc As eCoReinsuran.Contrproc
	Dim lobjValues As eFunctions.Values
	
	lclsContrproc = New eCoReinsuran.Contrproc
	lobjValues = New eFunctions.Values
	
	If lclsContrproc.FindLastDate(CInt(Request.QueryString.Item("nNumber"))) Then
		With Response
			.Write("top.fraHeader.document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsContrproc.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
			.Write("top.fraHeader.$('#tcdEffecdate').change();")
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
				Response.Write("opener.OnClasific();")
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


'--------------------------------------------------------------------------------------------
Sub CompanyName()
	'--------------------------------------------------------------------------------------------
	Dim lclsCompany As eCoReinsuran.Company
	Dim lobjValues As eFunctions.Values
	
	lclsCompany = New eCoReinsuran.Company
	lobjValues = New eFunctions.Values
	
	Response.Write("top.frames['fraFolder'].document.forms[0].valCompany.disabled=false;")
	
	If lclsCompany.Find_CompanySystem() Then
		With Response
			.Write("top.frames['fraFolder'].document.forms[0].valCompany.value=" & lclsCompany.nCompany & ";")
			
			.Write("top.frames['fraFolder'].$('#valCompany').change();")
			
			.Write("top.frames['fraFolder'].document.forms[0].valCompany.disabled=true;")
			
		End With
	End If
	
	lclsCompany = Nothing
	lobjValues = Nothing
End Sub

'--------------------------------------------------------------------------------------------
Sub ClasificCompany()
	'--------------------------------------------------------------------------------------------
	Dim lclsCompany As eCoReinsuran.Company
	Dim lobjValues As eFunctions.Values
	
	lclsCompany = New eCoReinsuran.Company
	lobjValues = New eFunctions.Values
	
	Response.Write("top.frames['fraFolder'].document.forms[0].valCompany.disabled=false;")
	Response.Write("top.frames['fraFolder'].document.forms[0].tcnClasific.disabled=false;")
	
	If lclsCompany.Find_ClasificCompany(CInt(Request.QueryString.Item("nCompany"))) Then
		With Response
			.Write("top.frames['fraFolder'].document.forms[0].tcnClasific.value=" & lclsCompany.nClasific & ";")
			.Write("top.frames['fraFolder'].document.forms[0].valCompany.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].tcnClasific.onfocus();")
			.Write("top.frames['fraFolder'].document.forms[0].tcnClasific.disabled=true;")
			
		End With
	End If
	
	lclsCompany = Nothing
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
<%Response.Write("<SCRIPT>")
'Response.Write "alert('"& Request.QueryString("Field") &"->1');"
Select Case Request.QueryString.Item("Field")
	
	Case "NumberContr"
		Call insShowDataContr()
	Case "NumberContr_np"
		Call insShowDataContr_np()
	Case "CompanyQuery"
		Call insShowCompanyName()
	Case "UpdateContrType"
		Call UpdateContrType()
	Case "Company"
		Call CompanyName()
	Case "Clasific"
		Call ClasificCompany()
	Case "DateContr"
		'Response.Write "alert('"&Request.QueryString("dEffecdate")&"');"
		Call insShowDateContr()
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")


%>





