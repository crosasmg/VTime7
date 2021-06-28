<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjLetter As eLetter.GroupParams

'+ Se define la contante para el manejo de errores en caso de advertencias

Dim mstrCommand As String


'% insValMantLetter: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantLetter() As String
	Dim eIniVal As Object
	Dim eEndVal As Integer
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:41:09 p.m.
	'Call insCommonFunction("valmantletter", Request.QueryString.Item("sCodispl"), eIniVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+CP001: Instalación de Compañía Contable
		
		Case "MLT001"
			mobjLetter = New eLetter.GroupParams
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantLetter = mobjLetter.insValMLT001_K("MLT001", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdInteger))
				Else
					If Request.QueryString.Item("WindowType") <> "PopUp" Then
						insValMantLetter = mobjLetter.insValMLT001("MLT001", .Form.Item("tctDescript"), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), 1)
					Else
						insValMantLetter = mobjLetter.insValMLT001("MLT001", .Form.Item("tctLettDescript"), .QueryString.Item("Action"), .Form.Item("tctVariable"), .Form.Item("tctDescript"), .Form.Item("tctTablename"), .Form.Item("tctColumName"), mobjValues.StringToType(.Form.Item("chkTypVariable"), eFunctions.Values.eTypeData.etdInteger))
					End If
				End If
			End With
		'Case Else
			'insValMantLetter = "insValMantLetter: " & C_MNOTFOUNDCODE & " (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	'^^Begin Trace Block 08/09/2005 05:41:09 p.m.
	'Call insCommonFunction("valmantletter", Request.QueryString.Item("sCodispl"), eEndVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function



'% insPostLetter: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostLetter() As Boolean
	Dim eIniPost As Object
	Dim eEndPost As Integer
	Dim lclsGeneral As Object
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:41:09 p.m.
	'Call insCommonFunction("valmantletter", Request.QueryString.Item("sCodispl"), eIniPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	
	Dim lblnPost As Boolean
	Dim lclsGroupVariables As eLetter.GroupVariables
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		
		'+CP001: Instalacion de Compañia Contable		
		
		Case "MLT001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger) = 303 Then
						lblnPost = mobjLetter.insPostMLT001(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdInteger))
						If lblnPost Then
							lblnPost = False
						Else
							Response.Write("<SCRIPT>alert('Err. 36308 " & lclsGeneral.insLoadMessage(36308) & "');</" & "Script>")
							lblnPost = True
						End If
					Else
						lblnPost = True
					End If
					Session("nGroup") = Request.Form.Item("valGroup")
				Else
					lclsGroupVariables = New eLetter.GroupVariables
					If Request.QueryString.Item("WindowType") <> "PopUp" Then
						lblnPost = lclsGroupVariables.insPostMLT001(vbNullString, Session("nGroup"), .Form.Item("tctDescript"), .Form.Item("tctParameters"), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), eRemoteDB.Constants.intNull, Session("nUsercode"), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull))
					Else
						lblnPost = lclsGroupVariables.insPostMLT001(.QueryString.Item("Action"), Session("nGroup"), .Form.Item("tctLettDescript"), .Form.Item("tctParameters"), .Form.Item("tctVariable"), .Form.Item("tctDescript"), .Form.Item("tctTableName"), .Form.Item("tctColumName"), mobjValues.StringToType(.Form.Item("chkTypVariable"), eFunctions.Values.eTypeData.etdInteger), Session("nUsercode"), .Form.Item("tctAliasTable"), .Form.Item("tctAliasColumn"))
					End If
					lclsGroupVariables = Nothing
				End If
			End With
		Case Else
			'Response.Write(C_MNOTFOUNDCODE)
			lblnPost = False
	End Select
	insPostLetter = lblnPost
	'^^Begin Trace Block 08/09/2005 05:41:09 p.m.
	'Call insCommonFunction("valmantletter", Request.QueryString.Item("sCodispl"), eEndPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mstrCommand = "&sModule=Product&sProject=Product&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT SRC="/VTimeNet/Scripts/GenFunctions.js"> </SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/ConstLanguage.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
	
</HEAD>
<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
//%CancelErrors: Va a la ventana anterior si se produce un error.
//------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------
	self.history.go(-1)}

//%NewLocation: Se posiciona en la página seleccionada. 
//------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%
If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) And Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
Else
	'+ Si no se han validado los campos de la página.
	
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValMantLetter
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantLetterErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostLetter Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					If Request.QueryString.Item("sCodisplReload") = vbNullString Then
						Select Case Request.QueryString.Item("sCodispl")
							Case "MLT001"
								Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
							Case "MLT101"
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sRecType=" & Request.Form.Item("tctRecType") & "&nLanguage=" & Request.Form.Item("cbeLanguage") & """;</SCRIPT>")
						End Select
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					End If
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "MLT001"
						Response.Write("<SCRIPT>top.opener.document.location.href='MLT001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & "&nMainAction=302'</SCRIPT>")
					Case "MLT001"
						Response.Write("<SCRIPT>opener.document.location.href='MLT001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
End If
mobjValues = Nothing
mobjLetter = Nothing


%>
</BODY>
</HTML>







