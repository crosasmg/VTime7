<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

Dim mobjMantFinance As eFinance.Tab_winFin
Dim mstrErrors As String
Dim mstrString As String
Dim mobjValues As eFunctions.Values

'**- The contante for the error handling in case of warnings is defined.  
'- Se define la contante para el manejo de errores en caso de advertencias.

Dim mstrCommand As String



'**%insValMantFinance: The massive validations of the form are made.  
'% insValMantFinance: Se realizan las validaciones masivas de la forma.
'--------------------------------------------------------------------------------------------
Function insValMantFinance() As String
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lintCheck As Object
	Dim lintCount As Integer
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'**+ MFI001 - Sequence of Windows For Contract Processes.
		'+ MFI001 - Sec. de Vent. Para Proc de Contratos.	
		
		Case "MFI001"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantFinance = New eFinance.Tab_winFin
				
				With Request
					insValMantFinance = mobjMantFinance.insValMFI001_K(Request.QueryString.Item("sCodispl"), Request.Form.Item("cbeTratypec"))
				End With
			Else
				mobjMantFinance = New eFinance.Tab_winFin
				
				If Request.QueryString.Item("nZone") = "2" And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
					lintCount = 0
					lintIndex = 0
					If Not IsNothing(Request.Form.Item("hddsSel")) Then
						For	Each lintCheck In Request.Form.GetValues("hddsSel")
							lintIndex = lintIndex + 1
							If lintCheck <> eRemoteDB.Constants.intNull Or lintCheck <> 0 Then
								If Request.Form.GetValues("hddsSel").GetValue(lintIndex - 1) = "1" Then
									lintCount = lintCount + 1
								End If
							End If
							
						Next lintCheck
					End If
					
					With Request
						insValMantFinance = mobjMantFinance.insValMFI001(Request.QueryString.Item("sCodispl"), lintCount)
					End With
				End If
			End If
			
		Case Else
			insValMantFinance = "insValMantFinance: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'**% insPostMantFinance: The updates to the tables are made.  
'% insPostMantFinance: Se realizan las actualizaciones a las tablas.
'--------------------------------------------------------------------------------------------
Function insPostMantFinance() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lintIndex As Short
	Dim lintCheck As Object
	
	lblnPost = False
	
	With Request
		Select Case Request.QueryString.Item("sCodispl")
			'+MSI001: Secuencia de Ventanas para Financiamiento
			Case "MFI001"
				With Request
					If .QueryString.Item("nMainAction") <> "401" And CDbl(.QueryString.Item("nZone")) <> 1 Then
						lintIndex = 0
						If Not IsNothing(.Form.Item("hddsSel")) Then
							For	Each lintCheck In .Form.GetValues("hddsSel")
								lintIndex = lintIndex + 1
								If lintCheck <> eRemoteDB.Constants.intNull Or lintCheck <> 0 Then
									lblnPost = mobjMantFinance.insPostMFI001(mobjValues.StringToType(.QueryString.Item("nTraTypec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnSequence").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsCodispl").GetValue(lintIndex - 1), Session("nUsercode"), "1", .Form.GetValues("hddsRequire").GetValue(lintIndex - 1), .Form.GetValues("hddsExist").GetValue(lintIndex - 1), .Form.GetValues("hddsSel").GetValue(lintIndex - 1), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
									
								End If
							Next lintCheck
						End If
					Else
						mstrString = "&nTraTypec=" & Request.Form.Item("cbeTratypec")
						lblnPost = True
					End If
				End With
		End Select
	End With
	
	insPostMantFinance = lblnPost
End Function

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mstrCommand = "sModule=Maintenance&sProject=MantFinance&sCodisplReload=" & Request.QueryString.Item("sCodispl")

%>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE002"))
End With
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>



	 
</HEAD>
<BODY>
<FORM id=form1 name=form1>
<%

'**+ If the fields of the page have not been validated.  
'+ Si no se han validado los campos de la página.

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantFinance
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantFinanceError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantFinance Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			End If
		Else
			
			'**+ The page is recharged that invoked the PopUp.  
			'+ Se recarga la página que invocó la PopUp.
			
			Select Case Request.QueryString.Item("sCodispl")
				Case "MFI001"
					Response.Write("<SCRIPT>top.opener.document.location.href='MFI001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
			End Select
		End If
	End If
End If
mobjMantFinance = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
</BODY>
</HTML>





