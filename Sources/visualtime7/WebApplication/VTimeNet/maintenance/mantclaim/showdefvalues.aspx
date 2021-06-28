<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'%insShowLocal: Permite mostrar La localidad en que se enncuentra un cliente determinado
'--------------------------------------------------------------------------------------------
Sub insShowLocal()
	'--------------------------------------------------------------------------------------------
	Dim lclsLocal As eGeneralForm.Address
	Dim lobjClient As eClient.Client
	Dim lstrClient As String
	lclsLocal = New eGeneralForm.Address
	lobjClient = New eClient.Client
	lstrClient = lobjClient.ExpandCode(UCase(Request.QueryString.Item("sClient")))
	'If lclsLocal.insReaAddress(lstrClient) Then
	'	Response.Write("opener.document.forms[0].tctZone.value='" & CStr(lclsLocal.sLocal) & "';")
    	Response.Write("opener.document.forms[0].tctZone.value='" & CStr(VbNullString) & "';")
	'End If

	lclsLocal = Nothing
End Sub

'% FindModules: Verifica si un determinado ramo-producto posee o no módulos para habilitar/deshabilitar
'%              el campo cbeModulec en la ventana MSI015 - ACM - 22/08/2002
'--------------------------------------------------------------------------------------------
Sub FindModules()
	'--------------------------------------------------------------------------------------------
	Dim lcolModules As eProduct.Tab_moduls
	lcolModules = New eProduct.Tab_moduls
	
	If lcolModules.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), Today) Then
		Response.Write("opener.document.forms[0].cbeModulec.disabled = false;")
		Response.Write("opener.document.forms[0].btncbeModulec.disabled = false;")
	Else
		Response.Write("opener.document.forms[0].cbeModulec.disabled = true;")
		Response.Write("opener.document.forms[0].btncbeModulec.disabled = true;")
	End If
	
	lcolModules = Nothing
	
End Sub
'% ActProvider: Actualiza el registro de proveedores cuando se esta registrando
'%              la información
'------------------------------------------------------------------------------
Sub ActProvider()
	'------------------------------------------------------------------------------
	Dim lclsProvider As eClaim.Tab_Provider
	lclsProvider = New eClaim.Tab_Provider
	If lclsProvider.FindProvider(mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), True) Then
		session("nExists_reg") = 1
	Else
		session("nExists_reg") = 0
		Call lclsProvider.insPostMSI011_K("MSI011", "Add", mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("3", eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, mobjValues.StringToType("2", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("1", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("1", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("4", eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, "2", session("nUsercode"), session("nExists_reg"))
	End If
	lclsProvider = Nothing
End Sub
'------------------------------------------------------------------------------
'%InsCancelUpdMsi011: Elimina datos grabados al cancelar el Ingreso de datos 
'------------------------------------------------------------------------------
Sub InsCancelUpdMsi011()
	Dim lclsProvider As eClaim.Tab_Provider
	lclsProvider = New eClaim.Tab_Provider
	If lclsProvider.FindProvider(mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), True) Then
		session("nExists_reg") = 1
		Call lclsProvider.insPostMSI011_K("MSI011", "Del", mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("3", eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, mobjValues.StringToType("2", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("1", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("1", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("4", eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, "2", session("nUsercode"), session("nExists_reg"))
	Else
		session("nExists_reg") = 0
	End If
	lclsProvider = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



</HEAD>
</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>

</BODY>
</HTML>
<%Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "sClient"
		Call insShowLocal()
	Case "Modules"
		Call FindModules()
	Case "Provider"
		Call ActProvider()
	Case "CancelUpdMsi011"
		Call InsCancelUpdMsi011()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





