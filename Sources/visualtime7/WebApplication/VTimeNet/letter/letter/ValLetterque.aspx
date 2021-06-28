<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<%@ Import namespace="eRemotedb" %>
<script language="VB" runat="Server">
'Dim insUpLoadFile() As Object
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjLetter As eLetter.LettRequest
Dim mobjValues As eFunctions.Values
Private mstrErrors As String
Private mstrCtroLettInd As String
Private UploadRequest() As Object

'**+ The variable declares itself to store the String in where there are defined the controls HIDDEN
'**+ of the page that invokes it.     
'+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
'+ de la página que la invoca.
Dim mstrCommand As String

Dim lstrPath As Object



'**% insvalSequence: There are realized the massive validations of the form
'% insvalSequence: Se realizan las validaciones masivas de la forma
'----------------------------------------------------------------------------------------------
Function insValLetterQue() As String
	'dim dtmNull As Object
	''Dim Numnull As Object
	'----------------------------------------------------------------------------------------------
	Dim lCondb As Boolean
	lCondb = False
	With Request
		Select Case .QueryString.Item("sCodispl")
			Case "LTC001"
				mobjLetter = New eLetter.LettRequest
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("toptCondition") = .Form.Item("optCondition")
					If IsNothing(.Form.Item("tcnLettRequest")) Then
						Session("tnRequest") = intNull
					Else
						Session("tnRequest") = mobjValues.StringToType(.Form.Item("tcnLettRequest"), eFunctions.Values.eTypeData.etdLong, True)
					End If
					If IsNothing(.Form.Item("tctClient")) Then
						Session("tnClient") = String.Empty
					Else
						Session("tnClient") = .Form.Item("tctClient")
					End If
					If CDbl(.Form.Item("cbeBranch")) = 0 Then
						Session("tcbeBranch") = intNull
					Else
						Session("tcbeBranch") = .Form.Item("cbeBranch")
					End If
					If IsNothing(.Form.Item("valProduct")) Then
						Session("tvalProduct") = intNull
					Else
						Session("tvalProduct") = .Form.Item("valProduct")
					End If
					If IsNothing(.Form.Item("tcnPolicy")) Then
						Session("tnPolicy") = intNull
					Else
						Session("tnPolicy") = .Form.Item("tcnPolicy")
					End If
					If IsNothing(.Form.Item("tcnCertificat")) Then
						Session("tnCertificate") = intNull
					Else
						Session("tnCertificate") = .Form.Item("tcnCertificat")
					End If
					If IsNothing(.Form.Item("tcnClaim")) Then
						Session("tnClaim") = intNull
					Else
						Session("tnClaim") = .Form.Item("tcnClaim")
					End If
					If IsNothing(.Form.Item("tcdEffecdate")) Then
						Session("tdEffectDat1") = DtmNull
					Else
						Session("tdEffectDat1") = .Form.Item("tcdEffecdate")
					End If
					If IsNothing(.Form.Item("tcdEffecdate1")) Then
						Session("tdEffectDat2") = intNull
					Else
						Session("tdEffectDat2") = .Form.Item("tcdEffecdate1")
					End If
					If IsNothing(.Form.Item("lsAplicant")) Then
						Session("lsAplicant") = intNull
					Else
						Session("lsAplicant") = .Form.Item("lsAplicant")
					End If
					insValLetterQue = mobjLetter.insValLTC001_K("2", mobjValues.StringToType(Session("tnRequest"), eFunctions.Values.eTypeData.etdLong), Session("tnClient"), mobjValues.StringToType(Session("lsAplicant"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("tcbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("tvalProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("tnPolicy"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("tnCertificate"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("tnClaim"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("tdEffectDat1"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("tdEffectDat2"), eFunctions.Values.eTypeData.etdDate), .QueryString.Item("sCodispl"))
				Else
					insValLetterQue = String.Empty
				End If
			Case "LTC002"
				insValLetterQue = String.Empty
			Case Else
				insValLetterQue = "insValLetterQue: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
		End Select
	End With
	mobjLetter = Nothing
End Function

'**% insPostLetterQue: The updates of the windows are realized
'% insPostLetterQue: Se realizan las actualizaciones de las ventanas
'----------------------------------------------------------------------------------------------
Function insPostLetterQue() As Boolean
	'----------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		Case "LTC001"
			insPostLetterQue = True
		Case "LTC002"
			insPostLetterQue = True
	End Select
End Function

'**% insFinish: It is activated when the action is to finish
'% insFinish: Se activa cuando la acción es finalizar
'----------------------------------------------------------------------------------------------
Function insFinish() As Object
	'----------------------------------------------------------------------------------------------
	Response.Write("<SCRIPT>insReloadTop(true, false);</" & "Script>")
End Function
'**% AfterPost: This procedure performs processes after the page is posted.
'% AfterPost: Este procedimiento ejecuta procesos después que la página es posteada.
'------------------------------------------------------------------------------------------------
Private Function AfterPost() As String
	'------------------------------------------------------------------------------------------------
	Dim mobjAfterPost As eFunctions.AfterProcess
	Dim sessitem As String
	Dim strSessionVariables As String
	Dim strFormVariables As String
	Dim FormItems As Object
	Dim objArray As String
	
	mobjAfterPost = New eFunctions.AfterProcess
	
	
	strSessionVariables = ""
	For	Each sessitem In Session.Contents
		If Not IsNothing(Session.Contents.Item(sessitem)) Then
			strSessionVariables = strSessionVariables & (sessitem & "=Session object cannot be displayed.&")
		Else
			If IsArray(Session.Contents.Item(sessitem)) Then
				For	Each objArray In Session.Contents.Item(sessitem)
					strSessionVariables = strSessionVariables & "&" & Session.Contents(sessitem) & "(" & sessitem & "):" & Session.Contents.Item(sessitem)(objArray)
				Next objArray
			Else
				strSessionVariables = strSessionVariables & (sessitem & "=" & Session.Contents.Item(sessitem) & "&")
			End If
		End If
	Next sessitem
	
	If Not IsNothing(Request.Form) Then
		For	Each FormItems In Request.Form
			If IsArray(Request.Form.Item(FormItems)) Then
				If Not IsNothing(Request.Form.Item(FormItems)) Then
					For	Each objArray In Request.Form.GetValues(FormItems)
						strFormVariables = strFormVariables & "&" & Request.Form.Item(FormItems) & "(" & FormItems & "):" & Request.Form.GetValues(FormItems).GetValue(objArray - 1)
					Next objArray
				End If
			Else
				strFormVariables = strFormVariables & (FormItems & "=" & Request.Form.Item(FormItems) & "&")
			End If
		Next FormItems
	End If
	
	
	AfterPost = mobjAfterPost.AfterPost(strFormVariables, Request.Params.Get("Query_String"), strSessionVariables)
	
	mobjAfterPost = Nothing
	
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("valletterque")

mstrCommand = "&sModule=Letter&sProject=Letter&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valletterque"

%>
<HTML>
	<HEAD>
	    

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	    

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
		<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
		<%=mobjValues.StyleSheet()%>
		

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

		

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

		

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Collection.aspx" -->
	
	</HEAD>
	<BODY>

		<%
If Not Session("bQuery") Or Request.QueryString.Item("nZone") = "1" Then
	If Request.QueryString.Item("sCodispl") = "LT001" Then
		lstrPath = Application("UpLoadFile")
		'insUpLoadFile(lstrPath)
		On Error Resume Next
		If Not UploadRequest(CInt("chkCtroLettInd")) Is Nothing Then
			If Err.Number = 0 Then
				mstrCtroLettInd = "1"
			Else
				err.Clear()
				mstrCtroLettInd = "2"
			End If
		End If
	End If
	
	If Request.QueryString.Item("sCodisplReload") = String.Empty Then
		mstrErrors = insValLetterQue
		Session("sErrorTable") = mstrErrors
		If Request.QueryString.Item("sCodispl") = "LT001" Then
			Session("sForm") = "FIELDS=BINARYREAD"
		Else
			Session("sForm") = Request.Form.ToString
		End If
	Else
		Session("sErrorTable") = String.Empty
		Session("sForm") = String.Empty
	End If
End If

If mstrErrors > String.Empty Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""LetterErrors"",660,330);")
		.Write("self.history.go(-1);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
		If insPostLetterQue Then
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				If Request.QueryString.Item("nZone") = "1" Or Request.QueryString.Item("sCodispl") = "LT001" Then
					Response.Write("<SCRIPT>opener.top.fraHeader.document.location.reload();window.close();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location.reload();</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("sCodisplReload") = String.Empty Then
					Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
				End If
				If Request.QueryString.Item("nZone") = "1" Then
					Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location.reload();</SCRIPT>")
				End If
				If Request.QueryString.Item("nZone") = "1" Then
					Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>opener.top.fraFolder.document.location.reload();</SCRIPT>")
				End If
			End If
			Response.Write(AfterPost)
		End If
	Else
		If Session("bQuery") = True Then
			Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
		Else
			insFinish()
		End If
	End If
End If
mobjValues = Nothing
%>
	</BODY>
</HTML>


<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
Call mobjNetFrameWork.FinishPage("valletterque")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








