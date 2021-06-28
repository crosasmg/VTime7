<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">

Dim mstrErrors As String

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mobjValues As eFunctions.Values


'% insValRentVita: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValRentVita() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		'+ RV001: Bono de reconocimiento
		Case "RV001"
			insValRentVita = ""
			
		Case Else
			insValRentVita = "insValRentVita: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostRentVita: Se efectua el proceso
'--------------------------------------------------------------------------------------------
Private Function insPostRentVita() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ RV001: Bono de reconocimiento
		Case "RV001"
			lblnPost = True
			insPrintDocuments()
	End Select
	
	insPostRentVita = lblnPost
End Function

'**% insPrintDocuments: Document printing
'%   insPrintDocuments: Impresión de los documentos
'-----------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-----------------------------------------------------------------------------------------
	Dim mobjDocuments As eReports.Report
	
	mobjDocuments = New eReports.Report
	With mobjDocuments
		Select Case Request.QueryString.Item("sCodispl")
			'+ RV001: Informe de Cheques
			Case "RV001"
				.sCodispl = "RV001"
				.ReportFilename = "repabr.rpt"
				.setStorProcParam(1, Request.Form.Item("tcnCashnum"))
				.setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
				Response.Write((.Command))
		End Select
	End With
	mobjDocuments = Nothing
	Server.ScriptTimeOut = 90
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



		
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
//---------------------------------------------------------------------------------------
function CancelErrors(){
//---------------------------------------------------------------------------------------
	self.history.go(-1)	
}

//---------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//---------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%
mstrCommand = "&sModule=RentVita&sProject=RentVitaRep&sCodisplReload=" & Request.QueryString.Item("sCodispl")

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValRentVita
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CashBankRepError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostRentVita Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>opener.top.document.location.reload();</SCRIPT>")
				End If
			End If
		End If
	End If
End If

mobjValues = Nothing
%>
</BODY>
</HTML>




