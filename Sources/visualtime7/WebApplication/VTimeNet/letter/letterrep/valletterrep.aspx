<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mstrErrors As String
Dim mclsLettRequest As eLetter.LettRequest
Dim mblnSkipPost As Boolean

'+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
'+ de la página que la invoca.
Dim mstrCommand As String


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValLetter() As String
	Dim eIniVal As Object

	'Dim AfterValidate() As Object
	Dim eEndVal As Integer
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:41:05 p.m.
	Call insCommonFunction("valletterrep", Request.QueryString.Item("sCodispl"), eIniVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'**+ LTL001: Delete of the correspondence.
		'+ LTL001: Eliminación de correspondencia.
		
		Case "LTL001"
			mclsLettRequest = New eLetter.LettRequest
			insValLetter = mclsLettRequest.insvalLTL001("LTL001", Request.Form.Item("tcdEffecdate"))
			'**+ LTL002: Printing Correspondence.
			'+ LTL002: Impresión de correspondencia.
			
		Case "LTL002"
			mclsLettRequest = New eLetter.LettRequest
			If IsNothing(Request.Form.Item("tcnLettRequest")) Then
				Session("tnRequest") = eRemoteDB.Constants.intNull
			Else
				Session("tnRequest") = mobjValues.StringToType(Request.Form.Item("tcnLettRequest"), eFunctions.Values.eTypeData.etdInteger)
			End If
			If IsNothing(Request.Form.Item("tctClient")) Then
				Session("tnClient") = vbNullString
			Else
				Session("tnClient") = Request.Form.Item("tctClient")
			End If
			If IsNothing(Request.Form.Item("tcdEffecdate")) Then
				Session("tdEffectDat") = eRemoteDB.Constants.DtmNull
			Else
				Session("tdEffectDat") = mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
			End If
			insValLetter = mclsLettRequest.insvalLTL002(Session("tdEffectDat"), Session("tnClient"), Session("tnRequest"))
		
		Case "LTL971"
		    mclsLettRequest = New eLetter.LettRequest
			insValLetter = mclsLettRequest.insvalLTL971(mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
		
		'**+ LTL501: Cleaning of the Impresion table
		'+ LTL501: Eliminación de registrso en la tabla de impresion 
			
		Case "LTL501"
			mclsLettRequest = New eLetter.LettRequest
			insValLetter = mclsLettRequest.insvalLTL501("LTL501", Request.Form.Item("tcnDays"))
		    
		Case Else
			insValLetter = "insValLetter: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	
	'If insValLetter = vbNullString Then
	'	Response.Write(AfterValidate(CInt(IIF(insValLetter="",0,insValLetter))))
	'End If
	
	'^^Begin Trace Block 08/09/2005 05:41:05 p.m.
	Call insCommonFunction("valletterrep", Request.QueryString.Item("sCodispl"), eEndVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function


'--------------------------------------------------------------------------------------------
'% insPostLetter: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostLetter() As Boolean
	Dim eIniPost As Object

	Dim eEndPost As Integer
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:41:05 p.m.
	Call insCommonFunction("valletterrep", Request.QueryString.Item("sCodispl"), eIniPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	Select Case Request.QueryString.Item("sCodispl")
		
		'**+ LTL001: Delete of the correspondence.
		'+ LTL001: Eliminación de correspondencia.  
		
		Case "LTL001"
			insPostLetter = True
			insPrintDocuments()
			
			'**+ LTL002: Printing Correspondence.
			'+ LTL002: Impresión de correspondencia.
			
		Case "LTL002"
			insPostLetter = True
		    Call insOpenDocument (Session("tnRequest"),Session("tnClient"),Session("tdEffectDat"))
			
		Case "LTL971"
			insPostLetter = True
			insPrintDocuments()
			
		'**+ LTL501: Delete printing documents
			'+ LTL501: Borrar documentos de impresion
			
		Case "LTL501"
			insPostLetter = True
			mclsLettRequest = New eLetter.LettRequest
			mclsLettRequest.insPostLTL501(mobjValues.StringToType(Request.Form.Item("tcnDays"), eFunctions.Values.eTypeData.etdInteger))
			'UPGRADE_NOTE: Object mclsLettRequest may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			mclsLettRequest = Nothing
			
	End Select
	'^^Begin Trace Block 08/09/2005 05:41:05 p.m.
	Call insCommonFunction("valletterrep", Request.QueryString.Item("sCodispl"), eEndPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function



'%insPrintDocuments : Realiza la ejecución del reporte
'--------------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'--------------------------------------------------------------------------------------------
	Dim mobjDocuments As eReports.Report
	mobjDocuments = New eReports.Report
				
	With mobjDocuments
		Select Case Request.QueryString.Item("sCodispl")
			Case "LTL001"
	    		.sCodispl = "LTL001"
		        ' .nUserCode = Session("nUsercode")
		        .ReportFilename = "LTL001.rpt"
		        '.setStorProcParam(1, .setdate(Request.Form.Item("tcdEffecdate")))
		        .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
		        Response.Write((.Command))
	        
	        Case "LTL971"
	    		.sCodispl = "LTL971"
		        ' .nUserCode = Session("nUsercode")
		        .ReportFilename = "LTL971.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    Response.Write((.Command))
            End Select
	End With
	mobjDocuments = Nothing
End Sub

'**% insOpenDocument: 
'% insOpenDocument: Realiza la impresión de las cartas pendientes de impresión
'----------------------------------------------------------------------------------------------------------------------------------------
Private Function insOpenDocument(ByVal nLettRequest As Integer, ByVal sClient As String, ByVal deffecdate As Date) As Object
'----------------------------------------------------------------------------------------------------------------------------------------
	Dim lobjLetter As eLetter.Letter
    Dim lobjLettAccuse As eLetter.LettAccuse
	Dim lobjLettAccuses As eLetter.LettAccuses
    Dim lobjLettRequest As eLetter.LettRequest

    Dim lstrContent As String
    Dim lintI As Integer
    Dim lintJ As Integer
    Dim lblnUpdated As Boolean
    
    Dim lstrPath As String
    Dim lrecrea_Letter As eRemoteDB.VisualTimeConfig

    lstrContent = ""
    lobjLetter = New eLetter.Letter
    lobjLettAccuse = New eLetter.LettAccuse
    lobjLettAccuses = New eLetter.LettAccuses
    lobjLettRequest = New eLetter.LettRequest

    lrecrea_Letter = New eRemoteDB.VisualTimeConfig
    lstrPath = Replace(lrecrea_Letter.LoadSetting("Correspondence", , "Paths"),"\","\\")
    
    With lobjLettAccuses
		Response.Write("<SCRIPT>" & vbcrlf)
        Response.Write("var mstrFileName;")
        Response.Write("var clsFileSystem;")
        Response.Write("var clsFile;")
        Response.Write("var clsWorkApplication;")
        Response.Write("clsWorkApplication = new ActiveXObject('Word.Application');")
        Response.Write("clsFileSystem = new ActiveXObject('Scripting.FileSystemObject');")
		Response.Write("</" & "SCRIPT>" & vbcrlf)
        Response.Write("<BR>")
        If .FindLTL002(nLettRequest, sClient,deffecdate) Then
            lstrContent = ""
			For lintI = 1 to .Count
                lintJ = lintI - 1
		        Response.Write("<SCRIPT>" & vbcrlf)
		        Response.Write("	if (clsFileSystem.FolderExists('" & lstrPath & "') == false)" & vbcrlf)
		        Response.Write("	{" & vbcrlf)
		        Response.Write("		clsFileSystem.CreateFolder('" & lstrPath & "');" & vbcrlf)
		        Response.Write("	}" & vbcrlf)
                Response.Write("mstrFileName = '" & lstrPath & "' + '\\' + clsFileSystem.GetTempName() + '.rtf';" & vbcrlf)
                Response.Write("</" & "SCRIPT>" & vbcrlf)
				'lobjLetter.nLettRequest =.Item(lintI).nLettRequest
				'lobjLetter.sClient = .Item(lintI).sClient

                If lobjLettRequest.Find(nLettRequest, True) Then
                    Call lobjLettRequest.MergeDocumentLR("LTL002",1,String.Empty,0,0,0,0,0,0,0,0,String.Empty,Today,0,0,0,Session("nUsercode"),lobjLettRequest.nLetternum,nLettRequest,False,Nothing)
                    'Call lobjLetter.MergeDocument(Nothing, Nothing, Today, Session("nUsercode"), False, 2, lobjLettRequest.nLetterNum, lobjLettRequest.nLanguage, String.Empty, nLettRequest, False)
                    lstrContent = lobjLettRequest.sMergeResult
                End if

				'lstrContent = lobjLetter.tLetters

	            Response.Write("<FORM METHOD=" & chr(34) & "POST"& chr(34) & "  ID=" & chr(34) & "FORM" & chr(34) & "  NAME=" & chr(34) & "frm" & chr(34) & "  ACTION=" & chr(34) & "valLetters.asp?time=1" & "&" & Request.QueryString.ToString() & chr(34) & "   ENCTYPE=" & chr(34) & "multipart/form-data" & chr(34) & " >" & vbcrlf)
			    Response.Write(Replace(mobjValues.TextAreaControl("tctLetter",2,2,lstrContent),"&nbsp;","&#032;"))
	            Response.Write("</FORM>" & vbcrlf)

                Response.Write("<SCRIPT>" & vbcrlf)
                Response.Write("clsFileSystem.CreateTextFile(mstrFileName, true);" & vbcrlf)
                Response.Write("clsFile = clsFileSystem.OpenTextFile(mstrFileName, 2, true);" & vbcrlf)
                Response.Write("clsFile.write(self.document.forms[" & lintJ & "].tctLetter.value);" & vbcrlf)
                Response.Write("clsFile.close();" & vbcrlf)
                Response.Write("clsWorkApplication.Documents.open(mstrFileName);" & vbcrlf)
                Response.Write("clsWorkApplication.visible = true;" & vbcrlf)
                Response.Write("clsWorkApplication.activate();" & vbcrlf)
                Response.Write("clsWorkApplication.Application.PrintOut();" & vbcrlf)
        		Response.Write("clsWorkApplication.ActiveDocument.Close(0);" & vbcrlf)
        		Response.Write("clsWorkApplication.activate();" & vbcrlf)
                Response.Write("</" & "SCRIPT>" & vbcrlf)

'+ Actualizando el estado de la solicitud de envío a impresa
                lblnUpdated = lobjLettAccuse.insPostLT002(.Item(lintI).nLettRequest,.Item(lintI).sClient)
            Next
        End If
        Response.Write("<SCRIPT>" & vbcrlf)
		Response.Write("clsWorkApplication.Quit();" & vbcrlf)
        Response.Write("</" & "SCRIPT>"  & vbcrlf)
    End With
    lobjLetter = Nothing
    lobjLettAccuse = Nothing
    lobjLettAccuses = Nothing
    lobjLettRequest = Nothing
    lrecrea_Letter = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("valLetterReP")

mblnSkipPost = False

mstrCommand = "&sModule=Letter&sProject=Letter&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valLetterReP"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Trace.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Collection.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/AfterBlocks.aspx" -->

</HEAD>
<BODY>

<SCRIPT>
//---------------------------------------------------------------------------------------------------
function CancelErrors(){
//---------------------------------------------------------------------------------------------------
    self.history.go(-1)}

//---------------------------------------------------------------------------------------------------    
function NewLocation(Source,Codisp){
//---------------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}
</SCRIPT>
<%
If Not Session("bQuery") Or Request.QueryString.Item("nZone") = "1" Then
	
	If Request.QueryString.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValLetter
		Session("sErrorTable") = mstrErrors
		If Request.QueryString.Item("sCodispl") = "LT001" Then
			Session("sForm") = "FIELDS=BINARYREAD"
		Else
			Session("sForm") = Request.Form.ToString
		End If
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""LetterErrors"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
ElseIf Not mblnSkipPost Then 
	If insPostLetter Then
		Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
		'Response.Write(AfterPost)
	End If
End If

mclsLettRequest = Nothing
mobjValues = Nothing
%>
</BODY>
</HTML>

<%
Call mobjNetFrameWork.FinishPage("valLetterReP")
mobjNetFrameWork = Nothing
%>







