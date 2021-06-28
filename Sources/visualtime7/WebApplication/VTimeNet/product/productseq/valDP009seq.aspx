<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ADODB" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de funciones generales
Dim mobjValues As eFunctions.Values

'- Variable para el manejo del querystring
Dim mstrQueryString As Object

'- Se define la constante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim mstrFileName As String
Dim mstrUseFile As Object
Dim mstrDefault As Object
Dim mstrContinue As String
Dim mstrmodif As Object

'- Objeto para localización de archivos
Dim mstrPath As String

Dim mobjUploadRequest As Object

Dim mstrErrors As String
Dim mobjProductSeq As Object
Dim mobjClient_req As Object
Dim mstrLocationBC003 As String
'- Contador para uso general    
Dim mintCount As Object

'- Esta variable es para indicar cuando debe pasarse a la siguiente ventana de la secuencia
'- al aceptar.  Para uso de casos particulares.
Dim lstrGoToNext As String

'- Cadena para pase de parametros    
Dim mstrString As Object

Dim mobjGeneral As eProduct.Tab_Clause


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	Dim lstrError As String=String.Empty
	
	insvalSequence = vbNullString
	
	Dim lclsTab_Clause As eProduct.Tab_Clause
	
	With Request
		If .QueryString.Item("WindowType") = "PopUp" Then
			lclsTab_Clause = New eProduct.Tab_Clause
			
			
			If Not insUpLoadFile(mstrPath) Then
				lstrError = "1977"
			End If
			
			If Not IsNothing(Request.Form("chkDefaulti"))  Then
				mstrDefault = Request.Form("chkDefaulti")
			Else
				mstrDefault = 2
			End If
			
			If Not IsNothing(Request.Form("tctFile"))  Then
				mstrFileName = Request.Form("tctFile")
			End If
			
            If Not IsNothing(Request.Form("chkType_Clause"))  Then
				mstrUseFile = Request.Form("chkType_Clause")
			Else
				mstrUseFile = 2
			End If

            If Not IsNothing(Request.Form("chkmodified"))  Then			
				mstrmodif = Request.Form("chkmodified")
			Else
				mstrmodif = 2
			End If
			
			If .QueryString.Item("Action") = "Update" Then
				If lclsTab_Clause.Find_Exist(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnClause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Request.Form("valModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("valCover"), eFunctions.Values.eTypeData.etdLong, True)) Then
					If mstrFileName = "" Then
						mstrFileName = lclsTab_Clause.sDoc_attach
						mstrUseFile = lclsTab_Clause.sType_clause
					End If
				End If
			End If
			
			insvalSequence = lclsTab_Clause.insValDP009("DP009", mobjValues.StringToType(Request.Form("tcnClause"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctDescript"), Request.Form("tctShort_des"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Request.Form("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("valCover"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form("cbeType"), eFunctions.Values.eTypeData.etdLong, True), mstrUseFile, mstrFileName, mobjValues.StringToType(Request.Form("tcnOrden"), eFunctions.Values.eTypeData.etdDouble))
			
			lclsTab_Clause = Nothing
			
		End If
	End With
	
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lclsProductWin As Object
	Dim lclsErrors As Object
	
	lblnPost = True
	
	Dim lclsTab_Clause As eProduct.Tab_Clause
	
	With Request
		If .QueryString.Item("WindowType") = "PopUp" Then
			lclsTab_Clause = New eProduct.Tab_Clause
			
			lblnPost = lclsTab_Clause.insPostDP009(mobjValues.StringToType(.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnClause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mstrDefault, Request.Form("tctDescript"), Request.Form("tctShort_des"), mobjValues.StringToType(Request.Form("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("valCover"), eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType(Request.Form("cbeType"), eFunctions.Values.eTypeData.etdLong, True), mstrUseFile, mstrFileName, mobjValues.StringToType(Request.Form("tcnOrden"), eFunctions.Values.eTypeData.etdDouble), mstrmodif)

			lclsTab_Clause = Nothing
		End If
	End With
	
	insPostSequence = lblnPost
End Function

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	insFinish = True
	
	mstrLocationBC003 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=DP003_K&sModule=Product&sProject=ProductSeq&sProduct=ProductSeq'"
End Function

'% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
'% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
'--------------------------------------------------------------------------------------------
Function insUpLoadFile(ByRef FilePath As String) As Boolean
	'--------------------------------------------------------------------------------------------
	
	Dim llngForWriting As Integer
	Dim llngLenBinary As Integer
	Dim lstrBoundry As String
	Dim llngBoundryPos As Integer
	Dim lstrFileName As String
	Dim lbytByteCount As Integer
	Dim lbytRequestBin() As Byte
	Dim lbytboundary As Object
	Dim llngPosFile As Object
	Dim mobjFormFile As eCollection.FormFile
	Dim llngBoundryPosaux As Integer
	Dim oFile as System.IO.File
    Dim oWrite as System.IO.StreamWriter
	
	llngForWriting = 2
	llngBoundryPos = 0
	llngBoundryPosaux = 0
	lbytByteCount = Request.TotalBytes
	lbytRequestBin = Request.BinaryRead(lbytByteCount)
	lstrBoundry = Request.ServerVariables.Item("HTTP_CONTENT_TYPE")
	llngBoundryPos = InStr(1, lstrBoundry, "boundary=") + 8
	
	If llngBoundryPos <> 8 Then
		llngBoundryPosaux = InStr(llngBoundryPos, lstrBoundry, "boundary=") + 8
	End If
	
	If llngBoundryPosaux <> 8 Then
		lstrBoundry = "--" & Right(lstrBoundry, Len(lstrBoundry) - llngBoundryPosaux)
	Else
		lstrBoundry = "--" & Right(lstrBoundry, Len(lstrBoundry) - llngBoundryPos)
	End If

    If True Then
		mobjFormFile = New eCollection.FormFile
		mobjFormFile.iBoundary = lstrBoundry
		mobjFormFile.iStreamBuffer = lbytRequestBin.Clone()
		
		If mobjFormFile.Request("tctFile") = vbCrLf Or mobjFormFile.Request("tctFile") = VbNullString Then
			lstrFileName = vbNullString
		Else
			'lstrFileName = mobjFormFile.getRandomFilename(Session("NUSERCODE"), CStr(False))
            lstrFileName = Request.Form("hdsFileName")
			oWrite = oFile.CreateText(mstrPath & lstrfilename)
			oWrite.Write(mobjFormFile.Request("tctFile"))
            oWrite.Close() 
		End If

		mstrFileName = lstrFileName
		mobjFormFile = Nothing
	End If

    insUpLoadFile = lstrFileName <> vbNullString

End Function

'% getConfigPath: Conversión de los datos de String a Byte
'--------------------------------------------------------------------------------------------
Function getConfigPath() As String
	'--------------------------------------------------------------------------------------------
    Dim lclsVisualTimeConfig As New eRemoteDB.VisualTimeConfig

    With lclsVisualTimeConfig
        getConfigPath = lclsVisualTimeConfig.LoadSetting("CDoc_Path", "", "Paths")
    End With

End Function




</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mstrCommand = "sModule=Product&sProject=ProductSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")

%>
<HTML>
<HEAD>
    <LINK REL="StyleSheet" TYPE="text/css" HREF="../../Common/Custom.css">  
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 29/06/06 5:41p $|$$Author: Fmendoza $"
</SCRIPT>
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<%
mstrLocationBC003 = "'/VTimeNet/Common/GoTo.aspx?sCodispl=DP003_K'"

Response.Write(mobjValues.StyleSheet())

mobjGeneral = New eProduct.Tab_Clause

mstrPath = getConfigPath() 'mobjGeneral.GetLoadFile(True)

mobjGeneral = Nothing

'+ Si no se han validado los campos de la página
If Request.QueryString.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
End If

Session("sErrorTable") = mstrErrors
Session("sForm") = vbNullString

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ProductSeqErrors"",660,330);self.document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
				lstrGoToNext = "Yes"
				
				If Request.QueryString.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=" & lstrGoToNext & "&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=" & lstrGoToNext & "&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			Else
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				'+ Se recarga la página que invocó la PopUp
				Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & mstrContinue & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				
			End If
		End If
	End If
Else
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Session("bQuery") Then
		Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
	Else
		
		'+ Se recarga la página principal de la secuencia            
		If insFinish() Then
			Response.Write("<SCRIPT>top.opener.top.document.location=" & mstrLocationBC003 & ";</SCRIPT>")
		End If
	End If
End If
mobjProductSeq = Nothing
mobjValues = Nothing
mobjUploadRequest = Nothing

%>
</FORM>
</BODY>
</HTML>




