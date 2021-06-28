<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralQue" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias

Dim mstrCommand As String

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjMantGeneralQue As Object


'% insValMantGeneralQue: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantGeneralQue() As String
	'--------------------------------------------------------------------------------------------
	
	Select Case Request.QueryString.Item("sCodispl")
		
		Case "MGE001"
			mobjMantGeneralQue = New eGeneralQue.PropertyLibrary
			With Request
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					insValMantGeneralQue = mobjMantGeneralQue.insValMGE001(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnIdProperty"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctProperty"))
				Else
					insValMantGeneralQue = vbNullString
				End If
			End With
		Case "MGE002"
			mobjMantGeneralQue = New eGeneralQue.Folder
			With Request
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					insValMantGeneralQue = mobjMantGeneralQue.insValMGE002_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnFolder"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctFolderName"), .Form.Item("tctRootName"), mobjValues.StringToType(.Form.Item("tcnImage"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClass"), .Form.Item("cbeFolderKey"))
				Else
					insValMantGeneralQue = vbNullString
				End If
			End With
		Case "MGE003"
			mobjMantGeneralQue = New eGeneralQue.ClassPropertyWin
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantGeneralQue = vbNullString
				End If
			End With
		Case "MGE004"
			mobjMantGeneralQue = New eGeneralQue.ClassPropertyWin
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantGeneralQue = mobjMantGeneralQue.insValMGE004_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeSelFolder"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insValMantGeneralQue = mobjMantGeneralQue.insValMGE004(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nFolder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIdProperty"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkTypVisible"), .Form.Item("tctCaption"), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
		Case Else
			insValMantGeneralQue = "insValMantGeneralQue: Código lógico no encontrado" & Request.QueryString.Item("sCodispl")
	End Select
	
End Function

'% insPostMantTables: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantGeneralQue() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	Select Case Request.QueryString.Item("sCodispl")
		Case "MGE001"
			With Request
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantGeneralQue.insPostMGE001(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnIdProperty"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctProperty"), Request.Form.Item("tctFormat"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = True
				End If
			End With
		Case "MGE002"
			mobjMantGeneralQue = New eGeneralQue.Folder
			With Request
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					
					lblnPost = mobjMantGeneralQue.insPostMGE002_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnFolder"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctFolderName"), .Form.Item("tctRootName"), mobjValues.StringToType(.Form.Item("tcnImage"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClass"), .Form.Item("cbeFolderKey"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					
				Else
					lblnPost = True
				End If
			End With
		Case "MGE003"
			mobjMantGeneralQue = New eGeneralQue.SeqFolders
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
					Session("nQueryTyp") = .Form.Item("cbeQueryType")
				Else
					If Not Session("bQuery") Then
						lblnPost = mobjMantGeneralQue.insPostMGE003(mobjValues.StringToType(Session("nQueryTyp"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("sTree"), Session("nUsercode"))
					End If
				End If
			End With
		Case "MGE004"
			mobjMantGeneralQue = New eGeneralQue.ClassPropertyWin
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
					Session("nFolder") = .Form.Item("cbeSelFolder")
				Else
					If Request.QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantGeneralQue.insPostMGE004(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nFolder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIdProperty"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkTypVisible"), .Form.Item("tctCaption"), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
			End With
	End Select
	
	insPostMantGeneralQue = lblnPost
End Function

</script>
<%Response.Expires = 0

mstrCommand = "&sModule=GeneralQue&sProject=MantGeneralQue&sCodisplReload=" & Request.QueryString.Item("sCodispl")

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
//------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------
	self.history.go(-1)}
	
//------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%

mobjValues = New eFunctions.Values


'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantGeneralQue
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sCommand=" & server.URLEncode(mstrErrors) & "&sForm=" & server.URLEncode(Request.Form.ToString) & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantGeneralQue Then
		
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
			End If
		Else
			
			'+ Se recarga la página que invocó la PopUp
			
			Select Case Request.QueryString.Item("sCodispl")
				Case "MGE001"
					Response.Write("<SCRIPT>opener.document.location.href='MGE001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
				Case "MGE002"
					Response.Write("<SCRIPT>opener.document.location.href='MGE002_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MGE004"
					Response.Write("<SCRIPT>top.opener.document.location.href='MGE004.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
			End Select
		End If
	Else
		If Session("bQuery") Then
			Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
		End If
	End If
End If

mobjValues = Nothing
mobjMantGeneralQue = Nothing
%>
</BODY>
</HTML>





