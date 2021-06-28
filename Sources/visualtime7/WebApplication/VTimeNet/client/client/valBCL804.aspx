<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="ADODB" %>
<script language="VB" runat="Server">
Dim nAction As Object
Dim mstrFileName As String
Dim mstrKey As String


'- Objeto para localización de archivos
Dim mstrPath As String

'- Objeto para el manejo de Reporte
Dim mobjUploadRequest As Dictionary(Of String, String)
Dim mobjDocuments As eReports.Report

'- Variable para el manejo de Errores

Dim mstrErrors As String

'- Variables para el recorrido del grid
Dim lintCount As Object

Dim mstrCommand As String

Dim mobjValues As eFunctions.Values

Dim mobjBatch As Object
Dim mstrLocationCAL013 As Object

Dim mobjGeneral As eBatch.MasiveCharge

'- Esta variable es para indicar cuando debe pasarse a la siguiente ventana de la secuencia
'- al aceptar.  Para uso de casos particulares.
Dim lstrGoToNext As Object


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	Dim lstrError As String=String.Empty
	Dim lclsTMP_BCL804 As eClient.tmp_BCL804
	
	insvalSequence = vbNullString
	
	If Not insUpLoadFile(mstrPath) Then
		lstrError = "1977"
	End If
	
	lclsTMP_BCL804 = New eClient.tmp_BCL804
	
	mstrFileName = Request.Form("tctFile")
	
	insvalSequence = lclsTMP_BCL804.insValBCL804(mstrFileName)
	
	lclsTMP_BCL804 = Nothing
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lclsBatch_param As eSchedule.Batch_param
	Dim lclsTMP_BCL804 As eClient.tmp_BCL804
	
	lclsBatch_param = New eSchedule.Batch_param
	
	mstrKey = lclsBatch_param.sKey
	
	lclsBatch_param = Nothing
	
	lclsTMP_BCL804 = New eClient.tmp_BCL804
	
	lblnPost = lclsTMP_BCL804.insImportExcel(mstrFileName, mstrKey, Session("nUsercode"), Request.QueryString.Item("sCodispl"))
	If Not lblnPost Then
		Response.Write("<SCRIPT>alert('Error en el formato del archivo');</" & "Script>")
	End If
	
	lclsTMP_BCL804 = Nothing
	
	insPostSequence = lblnPost
	
	Call insPrintClientRep()
	
End Function

'% insPrintClientRep: Se encarga de generar el reporte correspondiente.  
'--------------------------------------------------------------------------------------------  
Private Sub insPrintClientRep()
	'--------------------------------------------------------------------------------------------  
	Dim lstrdtmProcDate As Object
	mobjDocuments = New eReports.Report
	
	Dim lclsTMP_BCL804 As eClient.tmp_BCL804
	lclsTMP_BCL804 = New eClient.tmp_BCL804
	Dim lbnRead As Boolean
	
	With mobjDocuments
		
		.ReportFilename = "BCL804_RES.rpt"
		.sCodispl = "BCL804"
		.setStorProcParam(1, mstrKey)
		Response.Write((.Command))
		.Reset()
		
		lbnRead = lclsTMP_BCL804.find(mstrKey)
		
		If lbnRead Then
			.ReportFilename = "BCL804.rpt"
			.sCodispl = "BCL804"
			.setStorProcParam(1, mstrKey)
			Response.Write((.Command))
			.Reset()
		End If
		
		lclsTMP_BCL804 = Nothing
	End With
	mobjDocuments = Nothing
	
	mobjDocuments = Nothing
End Sub

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

'% getString: Conversión de los datos de Byte a String
'--------------------------------------------------------------------------------------------
Function getString(ByRef sStringBin As String) As String
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	
	getString = vbNullString
	
	For lintCount = 1 To Len(sStringBin)
		getString = getString & Chr(Asc(Mid(sStringBin, lintCount, 1)))
	Next 
	
End Function

'% getByteString: Conversión de los datos de String a Byte
'--------------------------------------------------------------------------------------------
Function getByteString(ByRef sStringStr As String) As String
	'--------------------------------------------------------------------------------------------
	Dim linCount As Integer
	Dim lstrchar As String
	For linCount = 1 To Len(sStringStr)
		lstrchar = Mid(sStringStr, linCount, 1)
		getByteString = getByteString & Chr(Asc(lstrchar))
	Next 
End Function

</script>
<%Response.Expires = -1

%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="../../Common/Custom.css">  
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 9 $|$$Date: 5/06/06 6:09p $|$$Author: Clobos $"

//% CancelErrors: Regresa a la Página Anterior
//------------------------------------------------------------------------------
function CancelErrors()
//------------------------------------------------------------------------------
{
self.history.go(-1)
}

//% NewLocation: Establece la Localizacion de la Pagina que se este trabajando.
//------------------------------------------------------------------------------
function NewLocation(Source,Codisp)
//------------------------------------------------------------------------------
{
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>

</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<FORM id=form1 name=form1>
<%
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "valBCL804"

Response.Write(mobjValues.StyleSheet())

mobjGeneral = New eBatch.MasiveCharge

mstrPath = mobjGeneral.GetLoadFile(True)

mobjGeneral = Nothing

mstrCommand = "&sModule=Client&sProject=Client&sCodisplReload=" & Request.QueryString.Item("sCodispl")

'+ Si no se han validado los campos de la página
If Request.QueryString.Item("sCodispl") <> "BCL804" Then
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insvalSequence
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
Else
	If Request.QueryString.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insvalSequence
		Session("sErrorTable") = mstrErrors
		Session("sForm") = vbNullString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
End If


If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ClientErrors"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostSequence Then
		Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
	Else
		Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
	End If
End If

mobjBatch = Nothing
mobjValues = Nothing
mobjUploadRequest = Nothing

%>
</FORM>
</BODY>
</HTML>




