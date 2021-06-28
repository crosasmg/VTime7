<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values


'% insProductMAG003: Habilita o inhabilta los campos asociados al producto
'--------------------------------------------------------------------------------------------
Sub insProductMAG003()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_moduls As eProduct.Tab_moduls
	Dim lclsProduct As eProduct.Product
	
	lclsTab_moduls = New eProduct.Tab_moduls
	lclsProduct = New eProduct.Product
	
	Response.Write("with(top.frames[""fraFolder""].document.forms[0]){")
	
	If lclsProduct.Find(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), Session("dEffecdate")) Then
		If lclsProduct.nDuration = eRemoteDB.Constants.intNull Then
			Response.Write("tcnDuration.value=12;")
		Else
			Response.Write("tcnDuration.value=" & lclsProduct.nDuration & ";")
		End If
		If lclsTab_moduls.Find(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), Session("dEffecdate")) Then
			Response.Write("valModulec.disabled=false;")
			Response.Write("btnvalModulec.disabled=false;")
		Else
			Response.Write("valModulec.disabled=true;")
			Response.Write("btnvalModulec.disabled=true;")
			Response.Write("valModulec.value='';")
			Response.Write("UpdateDiv('valModulecDesc','','PopUp');")
		End If
	End If
	
	Response.Write("}")
	
	lclsTab_moduls = Nothing
End Sub

'% insDisableModulec: Habilita o inhabilta el campo "Modulo" dependiendo si el producto tiene módulos asociados
'--------------------------------------------------------------------------------------------------------------
Sub insDisableModulec()
	'--------------------------------------------------------------------------------------------------------------
	Dim lclsTab_moduls As eProduct.Tab_moduls
	lclsTab_moduls = New eProduct.Tab_moduls
	
	If lclsTab_moduls.Find(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), Session("dEffecdate")) Then
		Response.Write("opener.document.forms[0].valModulec.disabled=false;")
		Response.Write("opener.document.forms[0].btnvalModulec.disabled=false;")
	Else
		Response.Write("opener.document.forms[0].valModulec.disabled=true;")
		Response.Write("opener.document.forms[0].btnvalModulec.disabled=true;")
		Response.Write("opener.document.forms[0].valModulec.value='';")
		Response.Write("opener.UpdateDiv('valModulecDesc','','PopUp');")
	End If
	
	lclsTab_moduls = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
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

Select Case Request.QueryString.Item("Field")
	Case "Modulec"
		Call insDisableModulec()
	Case "ProductMAG003"
		Call insProductMAG003()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>




