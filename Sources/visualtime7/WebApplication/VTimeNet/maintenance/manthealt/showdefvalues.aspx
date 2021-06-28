<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'-Variable para el manejo de funciones generales
Dim mobjValues As eFunctions.Values


'% insShowDataMAM001: Muestra los datos de la transacción de Límites por enfermedad.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataMAM001()
	'--------------------------------------------------------------------------------------------
	Dim lobjValues As eFunctions.Values
	Dim lclsObject As Object
	
	lobjValues = New eFunctions.Values
	
	Dim lobjErrors As eGeneral.GeneralFunction
	Select Case Request.QueryString.Item("sField")
		Case "getCurrency"
			lclsObject = New eBranches.tab_am_lim
			
			If lclsObject.insValGen_cover(lobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "1") Then
				Response.Write("top.frames['fraHeader'].UpdateDiv(""lblCurrency"",'" & lclsObject.sCurrDes & "','Normal');")
			End If
			
		Case "DelIllness"
			lobjErrors = New eGeneral.GeneralFunction
			lclsObject = New eBranches.tab_am_ill
			
			If lclsObject.IsExistLevelInf(Request.QueryString.Item("sIllness")) Then
				Response.Write("alert('" & "Err 10307. " & lobjErrors.insLoadMessage(10307) & "');")
				Response.Write("top.frames['fraHeader'].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "].checked=false;")
				Response.Write("top.frames['fraHeader'].marrArray[" & Request.QueryString.Item("nIndex") & "].Sel=false;")
			End If
			lobjErrors = Nothing
	End Select
	lobjValues = Nothing
	lclsObject = Nothing
End Sub

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
			If Request.QueryString.Item("Field") = "MAM001" Then
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
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:02 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY>
<FORM NAME="ShowValues">
</FORM>
</BODY>
</HTML>
<%mobjValues = New eFunctions.Values

Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "ShowDataMAM001"
		Call insShowDataMAM001()
	Case "MAM001"
		Call InsModulec()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

%>




