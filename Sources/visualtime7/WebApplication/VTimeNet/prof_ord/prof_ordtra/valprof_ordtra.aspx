<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mstrCodispl As Object
Dim mobjValues As eFunctions.Values
Dim mstrErrors As String
Dim mobjClaim As eClaim.Prof_ord
Dim mstrQueryString As String
Dim mintCase_num As Object
Dim mintDeman_type As String


'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insvalProd_ordTra: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValProd_ordTra() As String
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Object
	Dim lstrFirstCase As String
	Dim lstrCase() As String
	
	lstrFirstCase = vbNullString
	lstrFirstCase = Request.Form.Item("valCase")
	
	If lstrFirstCase <> vbNullString And lstrFirstCase <> "0" Then
		lstrCase = lstrFirstCase.Split("/")
		mintCase_num = lstrCase(0)
		mintDeman_type = lstrCase(1)
	End If
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ OS001: Solicitud de ordenes de servicio
		Case "OS001_K"
			mobjClaim = New eClaim.Prof_ord
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValProd_ordTra = mobjClaim.insValOS001_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrdClass"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mintCase_num)
				End If
			End With
			
		Case Else
			insValProd_ordTra = "insvalProd_ordTra: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostProd_ordTra: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostProd_ordTra() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Dim lclsProduct As eProduct.Product
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ OS001_K: Solicitud de ordenes de servicio
		Case "OS001_K"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					
					lclsProduct = New eProduct.Product
					
					Call lclsProduct.FindProdMaster(CInt(Request.Form.Item("cbeBranch")), CInt(Request.Form.Item("valProduct")))
					
					mstrQueryString = "&nOrdClass=" & Request.Form.Item("cbeOrdClass") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nPolicy=" & Request.Form.Item("tcnPolicy") & "&nProponum=" & Request.Form.Item("tcnProponum") & "&nCertif=" & Request.Form.Item("tcnCertif") & "&nClaim=" & Request.Form.Item("tcnClaim") & "&nCase_num=" & mintCase_num & "&sCodisplOri=" & Request.Form.Item("tctCodisplOri") & "&sBrancht=" & lclsProduct.sBrancht & "&nDeman_type=" & mintDeman_type
					lblnPost = True
					
				End If
			End With
	End Select
	insPostProd_ordTra = lblnPost
End Function

</script>
<%

Response.Expires = -1

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




</HEAD>
<%
Response.Write(mobjValues.StyleSheet())
If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	Response.Write("<BODY>")
Else
	Response.Write("<BODY CLASS=""Header"">")
End If
%>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.00 $|$$Author: Nvaplat60 $"

//% CancelErrors: Regresa a la Página Anterior     
//-----------------------------------------------------------------------------
function CancelErrors(){
//-----------------------------------------------------------------------------
	self.history.go(-1)
}

//% NewLocation: Establece la Localizacion de la Pagina que se este trabajando.
//-----------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%
mstrCommand = "&sModule=Prof_ord&sProject=Prof_ord&sCodisplReload=" & Request.QueryString.Item("sCodispl")

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValProd_ordTra
	Session("sErrorTable") = mstrErrors
Else
	Session("sErrorTable") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & server.URLEncode(Request.Form.ToString) & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """,""ClaimErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostProd_ordTra Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				Response.Write("<SCRIPT>top.fraFolder.document.location.href = '/VTimeNet/Policy/PolicySeq/OS001.aspx?sCodispl=OS001_K' + '" & mstrQueryString & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				'+ Solicitud de ordenes de servicio                  
				Case "OS001_K"
					Response.Write("<SCRIPT>top.opener.document.location.href='OS001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
			End Select
		End If
	End If
End If
mobjClaim = Nothing
%>
</BODY>
</HTML>




