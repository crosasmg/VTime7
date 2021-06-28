<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.57
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values



'% insShowAgency: Se muestran la oficina y sucursal asociadas a la agencia en tratamiento
'--------------------------------------------------------------------------------------------
Sub insShowAgency()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsAgencies As eGeneralForm.Agencies
	Dim lblvalor As Boolean
	lclsAgencies = New eGeneralForm.Agencies
	
	mobjValues.Parameters.Add("nOfficeAgen", mobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	
	If mobjValues.IsValid("TabAgencies_T5555", Request.QueryString.Item("nAgency"), True) Then
		lblvalor = lclsAgencies.Find(Request.QueryString.Item("nAgency"))
		If lclsAgencies.nOfficeagen > 0 Then
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeOffice.value='" & lclsAgencies.nBran_off & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsAgencies.nBran_off & ";")
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.value='" & lclsAgencies.nOfficeagen & "';")
			Response.Write("top.frames['fraFolder'].$('#cbeOfficeAgen').change();")
		End If
	End If
	
	lclsAgencies = Nothing
	
End Sub

'insDisableIntallments: Habilita y deshabilita los campos "Cuotas" y "Módulo" de acuerdo a una condición dada
'-------------------------------------------------------------------------------------------------------------
Sub insDisableIntallments()
	'-------------------------------------------------------------------------------------------------------------
	Dim lclsValues As eFunctions.Values
	Dim lclsIntermedia As eAgent.Intermedia
	Dim lclsProduct As eProduct.Product
	
	lclsValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
	lclsValues.sSessionID = Session.SessionID
	lclsValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	lclsValues.sCodisplPage = "showdefvalues"
	lclsIntermedia = New eAgent.Intermedia
	lclsProduct = New eProduct.Product
	
	'+ Se llama al método "Find" de la clase "Intermedia" para obtener el esquema de pago de comisiones.
	'+ Valores: 1= Producido  2= Recaudado
	Call lclsIntermedia.Find(lclsValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
	
	'+ Se obtiene el ramo técnico
	Call lclsProduct.insValProdMaster(lclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble))
	
	'+ Se habilitan los campos "Cuotas", "Vigencia desde" y Vigencia hasta" 
	'+ sólo si los esquemas son en base a lo "Recaudado"(2) para el Ramo-Producto en tratamiento.
	If Request.QueryString.Item("nProduct") <> "0" And Request.QueryString.Item("nProduct") <> vbNullString Then
		If CStr(lclsProduct.sBrancht) <> "1" And lclsIntermedia.nGen_sche = CDbl("2") Then
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnInstallments.disabled=false;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnStartMonth.disabled=false;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnEndMonth.disabled=false;")
		ElseIf CStr(lclsProduct.sBrancht) <> "1" And lclsIntermedia.nGen_sche <> CDbl("2") Then 
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnInstallments.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnStartMonth.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnEndMonth.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnInstallments.value=0;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnStartMonth.value=0;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnEndMonth.value=0;")
		ElseIf CStr(lclsProduct.sBrancht) = "1" And lclsIntermedia.nLife_sche = CDbl("2") Then 
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnInstallments.disabled=false;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnStartMonth.disabled=false;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnEndMonth.disabled=false;")
		ElseIf CStr(lclsProduct.sBrancht) = "1" And lclsIntermedia.nLife_sche <> CDbl("2") Then 
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnInstallments.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnStartMonth.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnEndMonth.disabled=true;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnInstallments.value=0;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnStartMonth.value=0;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnEndMonth.value=0;")
		End If
	End If
	
	lclsIntermedia = Nothing
	lclsProduct = Nothing
	lclsValues = Nothing
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "showdefvalues"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

	
<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.22 $|$$Author: Nvaplat60 $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Agencies"
		Call insShowAgency()
	Case "Installments"
		Call insDisableIntallments()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.57
Call mobjNetFrameWork.FinishPage("showdefvalues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




