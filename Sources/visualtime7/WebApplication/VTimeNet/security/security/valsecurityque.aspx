<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjSecurityQue As Object

'- Contador para uso genérico.

Dim mintCount As Object

'- Variable auxiliar para pase de valores del encabezado al folder

Dim mstrString As String

'- Auxiliar que contiene el número del elemento seleccionado de la colección.	

Dim mintAux As Object

'- Se define la contante para el manejo de errores en caso de advertencias

Dim mstrCommand As String


'% insValSecurityQue: Se realizan las validaciones de las formas.
'--------------------------------------------------------------------------------------------
Function insValSecurityQue() As String
	'--------------------------------------------------------------------------------------------
	Dim lstrPerType As Object
	Dim lstrInforType As Object
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ SGC001: Consulta de Usuarios del sistema.
		
		Case "SGC001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjSecurityQue = New eSecurity.User
					
					insValSecurityQue = mobjSecurityQue.insValSGC001_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDepartmen"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valSchema"))
					
					mobjSecurityQue = Nothing
				Else
					insValSecurityQue = ""
				End If
			End With
			
			'+ SGC002: Consulta de Transacciones del sistema.
			
		Case "SGC002"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjSecurityQue = New eSecurity.Windows
					
					insValSecurityQue = mobjSecurityQue.insValSGC002_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeModules"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctCodispl"), .Form.Item("tctCodisp"), .Form.Item("tctPseudo"))
					
					
					mobjSecurityQue = Nothing
				Else
					insValSecurityQue = ""
				End If
			End With
			
		Case Else
			insValSecurityQue = "insValSecurityQue: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	
End Function

'% insPostSecurityQue: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostSecurityQue() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ SGC001: Consulta de Usuarios del sistema.
		
		Case "SGC001"
			lblnPost = True
			
			With Request
				Session("nOffice") = .Form.Item("cbeOffice")
				Session("nDepartmen") = .Form.Item("cbeDepartmen")
				Session("sSchema") = .Form.Item("valSchema")
			End With
			
			'+ SGC002: Consulta de Transacciones del sistema.
			
		Case "SGC002"
			lblnPost = True
			
			With Request
				Session("nModules") = .Form.Item("cbeModules")
				Session("sCodispLog") = .Form.Item("tctCodispl")
				Session("sCodisp") = .Form.Item("tctCodisp")
				Session("sPseudo") = .Form.Item("tctPseudo")
			End With
	End Select
	
	insPostSecurityQue = lblnPost
End Function

</script>
<%Response.Expires = 0
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



		
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
//--------------------------------------------------------------------------------------------
function CancelErrors(){self.history.go(-1)}
//--------------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//--------------------------------------------------------------------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%
mstrCommand = "&sModule=Security&sProject=SecurityQue&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValSecurityQue
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""SecurityQueError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostSecurityQue() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("sCodispl") <> "OP004" Then
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
				End If
			End If
			
			'+ Se mueve automáticamente a la siguiente página.
			
		Else
			
			'+ Se recarga la página que invocó la PopUp
			
			Select Case Request.QueryString.Item("sCodispl")
			End Select
		End If
	End If
End If

mobjValues = Nothing
mobjSecurityQue = Nothing
%>
</BODY>
</HTML>




