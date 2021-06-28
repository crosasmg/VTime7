<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjFinanceQue As Object

'- Contador para uso genérico.

Dim mintCount As Object

'- Variable auxiliar para pase de valores del encabezado al folder

Dim mstrString As String

'- Auxiliar que contiene el número del elemento seleccionado de la colección.	

Dim mintAux As Object

'- Se define la contante para el manejo de errores en caso de advertencias

Dim mstrCommand As String


'% insValFinanceQue: Se realizan las validaciones de las formas.
'--------------------------------------------------------------------------------------------
Function insValFinanceQue() As String
	'--------------------------------------------------------------------------------------------
	Dim lstrPerType As Object
	Dim lstrInforType As Object
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ FIC006: Consulta de búsqueda de contratos.
		
		Case "FIC006"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 2 Then
					mobjFinanceQue = New eFinance.financeCO
					
					insValFinanceQue = mobjFinanceQue.insValFIC006(Request.QueryString.Item("sCodispl"), .Form.Item("tctContrat"), .Form.Item("tctClient"), .Form.Item("tctCliename"), .Form.Item("tctDate"), mobjValues.StringToType(.Form.Item("cbeStat_contr"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ FIC008: Consulta de operaciones de financiamiento.
			
		Case "FIC008"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjFinanceQue = New eFinance.DraftHist
					
					insValFinanceQue = mobjFinanceQue.insValFIC008_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcdInit_Date"), eFunctions.Values.eTypeData.etdDate))
					
					mobjFinanceQue = Nothing
				Else
					insValFinanceQue = ""
				End If
			End With
			
		Case Else
			insValFinanceQue = "insValFinanceQue: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostFinanceQue: Se realizan las actualizaciones a las tablas.
'--------------------------------------------------------------------------------------------
Function insPostFinanceQue() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ FIC006: Consulta de búsqueda de contratos.
		
		Case "FIC006"
			lblnPost = True
			
			With Request
				Session("sContrat") = .Form.Item("tctContrat")
				Session("sClient") = .Form.Item("tctClient")
				Session("sCliename") = .Form.Item("tctCliename")
				Session("sEffecDate") = .Form.Item("tctDate")
				Session("nStat_contr") = .Form.Item("cbeStat_contr")
				Session("sql") = "1"
			End With
			
			'+ FIC008: Consulta de Operaciones de financiamiento.
			
		Case "FIC008"
			lblnPost = True
			
			With Request
				Session("dInit_Date") = .Form.Item("tcdInit_Date")
				Session("nType") = .Form.Item("cbeType")
				Session("nCurrency") = .Form.Item("cbeCurrency")
			End With
	End Select
	
	insPostFinanceQue = lblnPost
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
mstrCommand = "&sModule=Finance&sProject=Financing&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValFinanceQue
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""FincancingeError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostFinanceQue() Then
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
				Case "FIC006"
					Response.Write("<SCRIPT>opener.document.location.href='FIC006_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sql=" & Server.URLEncode(Session("Sql")) & "'</SCRIPT>")
			End Select
		End If
	End If
End If

mobjValues = Nothing
mobjFinanceQue = Nothing

mobjFinanceQue = Nothing

%>
</BODY>
</HTML>




