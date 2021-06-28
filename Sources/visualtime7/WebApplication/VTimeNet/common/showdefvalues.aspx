<%@ Page Language="VB" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values



'% ReaZip_codeDefault: Busca la ciudad dado el codigo postal
'--------------------------------------------------------------------------------------------
Sub ReaZip_codeDefault()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_locat As eGeneralForm.Tab_locat
	lclsTab_locat = New eGeneralForm.Tab_locat
	With lclsTab_locat
		If .Find_Default(CShort(Request.QueryString.Item("nZipCode"))) Then
			Response.Write("opener.document.forms[0].valLocal.value='" & .nLocal & "';")
			Response.Write("opener.document.forms[0].tctProvince.value='" & .sDescript & "';")
			Response.Write("opener.document.forms[0].tcnProvince.value='" & .nProvince & "';")
			Response.Write("opener.$('#valLocal').change();")
		Else
			Response.Write("opener.document.forms[0].valLocal.value='';")
			Response.Write("opener.document.forms[0].tctProvince.value='';")
			Response.Write("opener.document.forms[0].tcnProvince.value='';")
		End If
	End With
	lclsTab_locat = Nothing
End Sub

'% ReaMunicipalityDefault: Busca la ciudad y la región dada la comuna
'--------------------------------------------------------------------------------------------
Sub ReaMunicipalityDefault()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_locat As eGeneralForm.Tab_locat
	lclsTab_locat = New eGeneralForm.Tab_locat
	With lclsTab_locat
		
		If Not IsNothing(Request.QueryString.Item("nMunicipality")) Then
			If .Find_by_municipality(mobjValues.StringToType(Request.QueryString.Item("nMunicipality"), eFunctions.Values.eTypeData.etdDouble)) Then
				
				Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
				Response.Write("    valLocal.value='" & .nLocal & "';")
				Response.Write("    top.frames['fraFolder'].$('#valLocal').change();")
				Response.Write("    cbeProvince.value='" & .nProvince & "';")
				Response.Write("}")
			Else
				
				Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
				Response.Write("    valLocal.value='';")
				Response.Write("    cbeProvince.value='';")
				Response.Write("}")
				Response.Write("    top.frames['fraFolder'].UpdateDiv('valLocalDesc','');")
			End If
		End If
	End With
	lclsTab_locat = Nothing
End Sub

</script>
<%Response.Expires = -1%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>
</BODY>
<BODY>
</BODY>
</HTML>
<%mobjValues = New eFunctions.Values
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "ZipCode"
		ReaZip_codeDefault()
	Case "Municipality"
		ReaMunicipalityDefault()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>




