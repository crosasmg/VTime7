<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eRemoteDB" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">

'-Variable para el manejo de funciones generales
Dim mobjValues As eFunctions.Values


'% inscalExchange: Se calcula el factor de cambio para una fecha-moneda.
'%				   Se invoca desde la MGS001
'--------------------------------------------------------------------------------------------
    Sub inscalExchange()
        '--------------------------------------------------------------------------------------------
        Dim lclsExchange As eGeneral.Exchange
        lclsExchange = New eGeneral.Exchange
        Call lclsExchange.Convert(0, 0, mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0)
        Response.Write("top.frames['fraFolder'].document.forms[0].hddExchange.value=" & lclsExchange.pdblExchange & ";")
        Response.Write("top.frames['fraFolder'].ShowChangeAmount();")
        lclsExchange = Nothing
    End Sub
    '% insShowCotProp: se muestran los datos asociados al número de propuesta
    '--------------------------------------------------------------------------------------------
    Sub insShowPolicy()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy_po As ePolicy.Policy
        
        lclsPolicy_po = New ePolicy.Policy
	
        '+ se agrego este manejo para el numero unico de propuesta/Cotización
        If lclsPolicy_po.FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
            Response.Write("opener.document.forms[0].cbeBranch.value=" & lclsPolicy_po.nBranch & ";")
            Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy_po.nBranch & ";")
            Response.Write("opener.document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
            'Response.Write("opener.document.forms[0].cbeBranch.disabled=true;")
            Response.Write("opener.document.forms[0].valProduct.disabled=false;")
            Response.Write("opener.document.forms[0].btnvalProduct.disabled=false;")
            If lclsPolicy_po.nProduct > 0 Then
                Response.Write("opener.$('#valProduct').change();")
            End If
        Else
            Response.Write("opener.document.forms[0].cbeBranch.value="""";")
            Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue="""";")
            Response.Write("opener.document.forms[0].valProduct.value="""";")
            Response.Write("opener.$('#valProduct').change();")
        End If
	        
        lclsPolicy_po = Nothing
    End Sub
</script>
<%  Response.Expires = -1
   
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "nExchange"
            Call inscalExchange()
            
        Case "Policy"
            Call insShowPolicy()
    End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>





