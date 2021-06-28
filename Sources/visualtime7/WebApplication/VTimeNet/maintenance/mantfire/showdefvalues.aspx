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
		lblnModulec = lclsProduct.IsModule(.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(CStr(Today), eFunctions.Values.eTypeData.etdDate))
	End With
	With Response
		.Write("with(top.frames['fraHeader'].document.forms[0]){")
		
		If lblnModulec Then
			.Write("valModulec.disabled=false;")
		Else
			.Write("valModulec.value='0';")
			If Request.QueryString.Item("Field") = "MIN651" Or Request.QueryString.Item("Field") = "MIN652" Then
				.Write("top.frames['fraHeader'].ChangeModulec(valModulec);")
			End If
			.Write("valModulec.disabled=true;")
			.Write("top.frames['fraHeader'].UpdateDiv('valModulecDesc','');")
		End If
		.Write("btnvalModulec.disabled=valModulec.disabled;")
		.Write("}")
	End With
	lclsProduct = Nothing
End Sub

</script>
<%Response.Expires = -1
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
<BODY>
</BODY>
</HTML>
<%mobjValues = New eFunctions.Values

Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	
	Case "MIN651"
		Call InsModulec()
	Case "MIN652"
		Call InsModulec()
		
End Select
Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>




