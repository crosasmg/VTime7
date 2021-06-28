<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mstrString As String

Dim mobjValues As eFunctions.Values
Dim mstrErrors As String

Dim mobjLedgerQue As eLedge.LedgerAcc


'% insValLedgerQue: Se realizan las validaciones de las páginas
'--------------------------------------------------------------
Function insValLedgerQue() As String
	'--------------------------------------------------------------
	
	With Request
		Select Case .QueryString.Item("sCodispl")
			
			'+ CPC001: Datos para consulta de Catálogo de Cuentas - NDCB - 30/05/2001.
			
			Case "CPC001"
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValLedgerQue = mobjLedgerQue.insValCPC001_K("CPC001_K", mobjValues.StringToType(.Form.Item("tcnLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBalDate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				Else
					insValLedgerQue = vbNullString
				End If
				
				'+ CPC002: Datos para consulta de Asientos a una cuenta - NDCB - día/mes/2001.
				
			Case "CPC002"
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValLedgerQue = mobjLedgerQue.insValCPC002_K("CPC002_K", mobjValues.StringToType(.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valAccount"), .Form.Item("valAux"), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate))
				Else
					insValLedgerQue = vbNullString
				End If
			Case Else
				insValLedgerQue = "insValLedgerQue: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
		End Select
	End With
End Function

'% insPostLedgerQue: Se realizan las actualizaciones a las tablas
'----------------------------------------------------------------
Function insPostLedgerQue() As Boolean
	'----------------------------------------------------------------
	
	Dim lblnPost As Boolean
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CPC001: Datos para consulta de Catálogo de Cuentas - NDCB - día/mes/2001.
		
		Case "CPC001"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&sIndent=" & .Form.Item("chkIndent") & "&nTypeQuery=" & .Form.Item("cbeBalDate") & "&nlevelQuant=" & .Form.Item("cbeLevels") & "&dEffecdate=" & .Form.Item("tcdEndDate") & "&nLed_compan=" & .Form.Item("tcnLedCompan")
					Session("nLedCompan") = .Form.Item("valLedCompan")
				End If
			End With
			
			'+ CPC002: Datos para consulta de Asientos a una Cuentas - NDCB - día/mes/2001.
			
		Case "CPC002"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nLed_compan") = .Form.Item("cbeLedCompan")
					Session("dEffecdate") = .Form.Item("tcdInitDate")
					mstrString = "&sAccount=" & .Form.Item("valAccount") & "&sAux_accoun=" & .Form.Item("valAux")
					lblnPost = True
				Else
					Session("dEffecdate") = ""
					lblnPost = True
				End If
			End With
	End Select
	
	insPostLedgerQue = lblnPost
End Function

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjLedgerQue = New eLedge.LedgerAcc

mstrCommand = "&sModule=GeneralLedger&sProject=LedgerQue&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>

<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"></SCRIPT>



		
<SCRIPT>
//------------------------------------------
function CancelErrors(){self.history.go(-1)}
//------------------------------------------

//----------------------------------
function NewLocation(Source,Codisp){
//----------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValLedgerQue
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""LedgerQueError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostLedgerQue() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
				End If
			Else
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "&sCodispl=" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & """" & ";</SCRIPT>")
				Else
					Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
				End If
			End If
		End If
	End If
End If

mobjValues = Nothing
mobjLedgerQue = Nothing
%>
</BODY>
</HTML>





