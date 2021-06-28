<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'-Variable para el manejo de funciones generales
Dim mobjValues As eFunctions.Values


'% InsModulec: Valida si el producto es modular, si no es modular asigna 0 e inhabilita campo
'--------------------------------------------------------------------------------------------
Sub InsModulec()
	'--------------------------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	Dim lblnModulec As Boolean
	
	lclsProduct = New eProduct.Product
	With mobjValues
		lblnModulec = lclsProduct.IsModule(.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End With
	With Response
		.Write("with(top.frames['fraHeader'].document.forms[0]){")
		If lblnModulec Then
			.Write("valModulec.disabled=false;")
		Else
			.Write("valModulec.value='0';")
			If Request.QueryString.Item("Field") <> "MVI729" Then
				If Request.QueryString.Item("Field") <> "MVA600" Then
					.Write("top.frames['fraHeader'].ChangeValues(valModulec,'Modulec');")
				End If
			Else
				.Write("top.frames['fraHeader'].InsChangeField(""Module"",valModulec.value);")
			End If
			.Write("valModulec.disabled=true;")
			.Write("top.frames['fraHeader'].UpdateDiv('valModulecDesc','');")
		End If
		.Write("btnvalModulec.disabled=valModulec.disabled;")
		.Write("}")
	End With
	lclsProduct = Nothing
End Sub

'% InsCurrency: Actualiza el tipo de moneda segun la cobertura
'--------------------------------------------------------------------------------------------
Sub InsCurrency()
	'--------------------------------------------------------------------------------------------
	'+ Variables para componentes
	Dim lclsProduct As eProduct.Life_cover
	Dim lblnFilterOk As Boolean
	
	With Request
		lblnFilterOk = .QueryString.Item("nBranch") <> vbNullString And CDbl(.QueryString.Item("nBranch")) <> 0 And .QueryString.Item("nProduct") <> vbNullString And CDbl(.QueryString.Item("nProduct")) <> 0 And .QueryString.Item("nModulec") <> vbNullString And .QueryString.Item("nCover") <> vbNullString And CDbl(.QueryString.Item("nCover")) <> 0 And .QueryString.Item("dEffecdate") <> vbNullString
		
		If lblnFilterOk Then
			lclsProduct = New eProduct.Life_cover
			If lclsProduct.Find(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				
				Response.Write("top.frames['fraHeader'].UpdateDiv('valCurrencyDesc','" & mobjValues.getMessage(lclsProduct.nCurrency, "table11") & "');")
			Else
				Response.Write("top.frames['fraHeader'].UpdateDiv('valCurrencyDesc','');")
			End If
			lclsProduct = Nothing
		Else
			Response.Write("top.frames['fraHeader'].UpdateDiv('valCurrencyDesc','');")
		End If
	End With
	
End Sub

</script>
<%Response.Expires = -1
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


</HEAD>
<BODY>
    <FORM NAME="ShowDefValues">
    </FORM>
</BODY>
</HTML>
<%mobjValues = New eFunctions.Values

Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "MVA600", "MVI729", "MVI757", "MVA606_P", "MVI630"
		Call InsModulec()
	Case "MVA606"
		Call InsCurrency()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





