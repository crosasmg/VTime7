<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values


'% insdEffecdate: rescata la fecha maxima para los datos ingresados MMGS002
'--------------------------------------------------------------------------------------------
Sub insdEffecdate()
	'--------------------------------------------------------------------------------------------
	Dim lclsMargin_Allow As eMargin.Margin_Allow
	Dim ValMaxEffecdate As Object
	lclsMargin_Allow = New eMargin.Margin_Allow
	
	With mobjValues
		ValMaxEffecdate = lclsMargin_Allow.ValMaxEffecdate(.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nTableTyp"), eFunctions.Values.eTypeData.etdInteger), .StringToType(Request.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdInteger), .StringToType(Request.QueryString.Item("nClaimClass"), eFunctions.Values.eTypeData.etdInteger))
	End With
	
	If ValMaxEffecdate = eRemoteDB.Constants.dtmNull Then
		ValMaxEffecdate = vbNullString
	End If
	
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(ValMaxEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
	
	lclsMargin_Allow = Nothing
End Sub
'% insdEffecdate: rescata la fecha maxima para los datos ingresados MMGS002
'--------------------------------------------------------------------------------------------
Sub insTabModul()
	'--------------------------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	
	lclsProduct = New eProduct.Product
	
	With mobjValues
		If lclsProduct.IsModule(.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			Response.Write("top.frames['fraFolder'].document.forms[0].valModulec.disabled=false;")
			Response.Write("top.frames['fraFolder'].document.forms[0].btnvalModulec.disabled=false;")
			Response.Write("top.frames['fraFolder'].document.forms[0].valCover.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].btnvalCover.disabled=true;")
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].valModulec.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].btnvalModulec.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].valCover.disabled=false;")
			Response.Write("top.frames['fraFolder'].document.forms[0].btnvalCover.disabled=false;")
		End If
	End With
	lclsProduct = Nothing
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:08 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write(mobjValues.StyleSheet() & vbCrLf)
Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "dEffecdate"
		Call insdEffecdate()
	Case "TabModul"
		Call insTabModul()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>




