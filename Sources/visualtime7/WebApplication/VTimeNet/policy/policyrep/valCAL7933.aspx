<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eInterface" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="ADODB" %>



<script language="VB" runat="Server">
    Dim nAction As Object


    '- Objeto para localización de archivos
    Dim mstrPath As String

    '- Objeto para el manejo de Reporte
    Dim mobjUploadRequest As Scripting.Dictionary

    '- Variable para el manejo de Errores

    Dim mstrErrors As String

    '- Variables para el recorrido del grid
    Dim lintCount As Object

    Dim mstrCommand As String

    Dim mobjValues As eFunctions.Values

    Dim mobjBatch As Object

    Dim mobjGeneral As eBatch.MasiveCharge

    Dim lclsGeneral As eGeneral.GeneralFunction

    Dim mstrKey As String
    Dim mstrFileName As String


    '% insvalSequence: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insValPolicy() As String
        '--------------------------------------------------------------------------------------------

        Dim lstrError As String

        Select Case Request.QueryString("sCodispl")

            Case "CAL7933"
                If Not insUpLoadFile(mstrPath) Then
                    lstrError = "1977"
                End If

                With Request
                    Session("sType") = mobjUploadRequest.Item("cbeType").Item("Value")
                    Session("sFile") = mstrFileName

                    'Session("sFile") = Session("nUsercode") & Session("sFile")

                    insValPolicy = vbNullString
                End With

            Case Else
                insValPolicy = "ValPolicyRep: Código lógico no encontrado (" & Request.QueryString("sCodispl") & ")"
        End Select

    End Function

    '% insPostSequence: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostPolicy() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lclsBatch_param As eSchedule.Batch_param
        Dim lclsPolicy As ePolicy.ValPolicyRep
        Dim lclsInterface As eInterface.ValInterfaceSeq

        Select Case Request.QueryString("sCodispl")

            Case "CAL7933"
                lclsPolicy = New ePolicy.ValPolicyRep
                lclsInterface = New eInterface.ValInterfaceSeq

                lblnPost = lclsInterface.CreT_Param_Interface(mstrKey, 1, mobjValues.StringToType(CStr(Session("sType")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))

                lblnPost = lclsPolicy.InsPostCAL7933(1, CStr(Session("sFile")), 2)



                If lblnPost Then
                    lclsBatch_param = New eSchedule.Batch_param

                    With lclsBatch_param
                        .nBatch = 1402
                        .sKey = mstrKey
                        .nSheet = 7933
                        .nUsercode = mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 1)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 7933)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, lclsPolicy.mstrFile)

                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mstrKey)
                        .Save()
                    End With

                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & mstrKey & "');</" & "Script>")

                    'UPGRADE_NOTE: Object lclsBatch_param may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsBatch_param = Nothing
                End If

                'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsPolicy = Nothing
                'UPGRADE_NOTE: Object lclsInterface may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                lclsInterface = Nothing
        End Select

        insPostPolicy = lblnPost
    End Function

    '% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
    '% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
    '--------------------------------------------------------------------------------------------
    Function insUpLoadFile(ByRef FilePath As String) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim llngForWriting As Integer
        Dim llngVarChar As Byte
        Dim llngLenBinary As Integer
        Dim lstrBoundry As String
        Dim llngBoundryPos As Integer
        Dim llngCurrentBegin As Integer
        Dim llngCurrentEnd As Integer
        Dim lstrData As String
        Dim lstrDataWhole As String
        Dim llngEndFileName As Integer
        Dim lstrFileName As String
        Dim llngBeginPos As Integer
        Dim llngEndPos As Integer
        Dim llngDataLenth As Integer
        Dim lbytByteCount As Object
        Dim lbytRequestBin As String
        Dim llngPosBeg As Double
        Dim llngPosEnd As Integer
        Dim lbytboundary As String
        Dim llngboundaryPos As Integer
        Dim llngPos As Integer
        Dim lstrName As String
        Dim llngPosFile As Integer
        Dim lstrContentType As String
        Dim lstrValue As String
        Dim llngPosBound As Integer
        Dim llngPrevPos As Integer
        Dim llngTmpLng As Integer
        Dim llngCt As Integer
        Dim lstrFileData As String
        Dim lobjRST As ADODB.Recordset
        Dim lobjfso As Scripting.FileSystemObject
        Dim lobjf As Scripting.ITextStream
        Dim nexist As Integer
        Dim UploadControl As Scripting.Dictionary

        mobjUploadRequest = New Scripting.Dictionary

        llngForWriting = 2
        llngVarChar = 201
        lbytByteCount = Request.TotalBytes
        'lbytRequestBin = Request.BinaryRead(lbytByteCount)
        lbytRequestBin = Request.BinaryRead(lbytByteCount).ToString

        lobjRST = New ADODB.Recordset
        'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        llngLenBinary = Len(lbytRequestBin)

        If llngLenBinary > 0 Then

            lobjRST.Fields.Append("myBinary", llngVarChar, llngLenBinary)
            lobjRST.Open()
            lobjRST.AddNew()
            lobjRST.Fields("myBinary").AppendChunk(lbytRequestBin)
            lobjRST.Update()
            lstrDataWhole = IIF(IsDBNull(lobjRST.Fields.Item("myBinary").Value), Nothing, lobjRST.Fields.Item("myBinary").Value)

            '+ Creates a raw data file for with all data sent. Uncomment for debuging. 

            '        Set lobjfso = CreateObject("Scripting.FileSystemObject")
            '        Set lobjf	= lobjfso.OpenTextFile("d:\appserv\InetPub\UpLoad" & "\rawINI.txt", llngForWriting, True)
            '        lobjf.Write lstrDataWhole
            '        set lobjf	= nothing
            '        set lobjfso	= nothing   

        End If
        'UPGRADE_NOTE: Object lobjRST may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lobjRST = Nothing

        '+ Se calcula el número de elementos a evaluar

        llngPosBeg = 1
        'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        ''llngPosEnd = InStr(llngPosBeg, lbytRequestBin, getByteString(Chr(13)))
        'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        lbytboundary = Mid(lbytRequestBin, llngPosBeg, llngPosEnd - llngPosBeg)
        'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        llngboundaryPos = InStr(1, lbytRequestBin, lbytboundary)

        '+Se busca entre todos los elementos que recibe la página, el que corresponde a la imagen
        'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        Do Until (llngboundaryPos = InStr(lbytRequestBin, lbytboundary & getByteString("--")))

            '+Variable para el manejo del diccionario del objeto
            UploadControl = New Scripting.Dictionary

            '+Se toma el nombre del objeto
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            llngPos = InStr(llngboundaryPos, lbytRequestBin, getByteString("Content-Disposition"))
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            llngPos = InStr(llngPos, lbytRequestBin, getByteString("name="))
            llngPosBeg = llngPos + 6
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            ''llngPosEnd = InStr(llngPosBeg, lbytRequestBin, getByteString(Chr(34)))
            'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            lstrName = getString(Mid(lbytRequestBin, llngPosBeg, llngPosEnd - llngPosBeg))
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            llngPosFile = InStr(llngboundaryPos, lbytRequestBin, getByteString("filename="))
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            llngPosBound = InStr(llngPosEnd, lbytRequestBin, lbytboundary)

            '+Se verifica si el objeto corresponde a un <INPUT TYPE=FILE id=FILE1 name=FILE1>
            If llngPosFile <> 0 And (llngPosFile < llngPosBound) Then

                insUpLoadFile = True
                '+Get the boundry indicator
                lstrBoundry = Request.ServerVariables("HTTP_CONTENT_TYPE")
                llngBoundryPos = InStr(1, lstrBoundry, "boundary=") + 8
                lstrBoundry = "--" & Right(lstrBoundry, Len(lstrBoundry) - llngBoundryPos)

                If InStr(2, lstrBoundry, "multipart") Then
                    nexist = InStr(2, lstrBoundry, "multipart")
                    lstrBoundry = Mid(lstrBoundry, 1, nexist - 2)
                End If

                '+Get first file boundry positions.
                llngCurrentBegin = InStr(1, lstrData, lstrBoundry)
                llngCurrentEnd = InStr(llngCurrentBegin + 1, lstrData, lstrBoundry) - 1

                '+Get the data between current boundry and remove it from the whole
                lstrData = Mid(lstrData, llngCurrentBegin, llngCurrentEnd - llngCurrentBegin)
                lstrDataWhole = Replace(lstrDataWhole, lstrData, "")

                '+ Se toma el tipo, nombre y contenido del archivo
                llngPosBeg = llngPosFile + 10
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                ''llngPosEnd = InStr(llngPosBeg, lbytRequestBin, getByteString(Chr(34)))
                'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                lstrFileName = getString(Mid(lbytRequestBin, llngPosBeg, llngPosEnd - llngPosBeg))

                '+Create the file
                llngTmpLng = InStr(1, lstrFileName, "\")

                Do While llngTmpLng > 0

                    llngPrevPos = llngTmpLng
                    llngTmpLng = InStr(llngPrevPos + 1, lstrFileName, "\")

                Loop

                lstrFileName = CreateGUID() & "-" & Right(lstrFileName, Len(lstrFileName) - llngPrevPos)
                mstrFileName = lstrFileName
                If lstrFileName = vbNullString Then

                    insUpLoadFile = False

                End If

                '+ Se añade el nombre al diccionario del objeto
                UploadControl.Add("FileName", lstrFileName)
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                llngPos = InStr(llngPosEnd, lbytRequestBin, getByteString("Content-Type:"))
                llngPosBeg = llngPos + 14
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                ''llngPosEnd = InStr(llngPosBeg, lbytRequestBin, getByteString(Chr(13)))

                '+ Se añade el tipo al diccionario del objeto
                'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                lstrContentType = getString(Mid(lbytRequestBin, llngPosBeg, llngPosEnd - llngPosBeg))
                UploadControl.Add("ContentType", lstrContentType)

                '+ Se toma contenido del archivo
                llngPosBeg = llngPosEnd + 4
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                ''llngPosEnd = InStr(llngPosBeg, lbytRequestBin, lbytboundary) - 2
                'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                lstrValue = Mid(lbytRequestBin, llngPosBeg, llngPosEnd - llngPosBeg)
                llngCt = InStr(1, lstrData, "Content-Type:")

                If llngCt > 0 Then
                    llngBeginPos = InStr(llngCt, lstrData, Chr(13) & Chr(10)) + 4
                Else
                    llngBeginPos = llngEndFileName
                End If

                '+ Get the ending position of the file data sent.

                llngEndPos = Len(lstrData)

                '+ Calculate the file size. 

                llngDataLenth = llngEndPos - llngBeginPos

                '+ Get the file data 

                lstrFileData = Mid(lstrData, llngBeginPos, llngDataLenth)

                '+ En caso de que se haya seleccionado algún archivo.
                If insUpLoadFile Then

                    '+ Create the file. 

                    lobjfso = New Scripting.FileSystemObject

                    lobjf = lobjfso.OpenTextFile(FilePath & lstrFileName, llngForWriting, True)

                    lobjf.Write(lstrFileData)
                    If Not lobjfso.FileExists(FilePath & lstrFileName) Then
                        insUpLoadFile = False
                    End If
                    'UPGRADE_NOTE: Object lobjf may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lobjf = Nothing
                    'UPGRADE_NOTE: Object lobjfso may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lobjfso = Nothing
                End If

            Else

                lstrBoundry = Request.ServerVariables("HTTP_CONTENT_TYPE")

                If InStr(2, lstrBoundry, "multipart") Then
                    nexist = InStr(2, lstrBoundry, "multipart")
                    lstrBoundry = Mid(lstrBoundry, 1, nexist - 2)
                End If

                llngBoundryPos = InStr(1, lstrBoundry, "boundary=") + 8
                lstrBoundry = "--" & Right(lstrBoundry, Len(lstrBoundry) - llngBoundryPos)

                '+ Get first file boundry positions.

                llngCurrentBegin = InStr(1, lstrDataWhole, lstrBoundry)
                llngCurrentEnd = InStr(llngCurrentBegin + 1, lstrDataWhole, lstrBoundry) - 1

                'Get the data between current boundry and remove it from the whole.
                lstrData = Mid(lstrDataWhole, llngCurrentBegin, llngCurrentEnd - llngCurrentBegin)
                lstrDataWhole = Replace(lstrDataWhole, lstrData, "")
                lstrData = lstrDataWhole

                '+ Si el objeto no es una imagen, se toma la información del mismo

                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                llngPos = InStr(llngPos, lbytRequestBin, getByteString(Chr(13)))
                llngPosBeg = llngPos + 4
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                ''llngPosEnd = InStr(llngPosBeg, lbytRequestBin, lbytboundary) - 2
                'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                lstrValue = getString(Mid(lbytRequestBin, llngPosBeg, llngPosEnd - llngPosBeg))

            End If

            '+ Se añade el contenido al diccionario del objeto  
            UploadControl.Add("Value", lstrValue)

            '+ Se añade el objeto al diccionario principal de la página
            mobjUploadRequest.Add(lstrName, UploadControl)

            '+ Se busca el siguiente objeto
            'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            llngboundaryPos = InStr(llngboundaryPos + Len(lbytboundary), lbytRequestBin, lbytboundary)
            'UPGRADE_NOTE: Object UploadControl may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
            UploadControl = Nothing
        Loop

    End Function

    '% getString: Conversión de los datos de Byte a String
    '--------------------------------------------------------------------------------------------
    Function getString(ByRef sStringBin As String) As String
        '--------------------------------------------------------------------------------------------
        Dim lintCount As Integer

        getString = vbNullString

        'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        For lintCount = 1 To Len(sStringBin)
            'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            'UPGRADE_ISSUE: AscB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
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
            'UPGRADE_ISSUE: AscB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            'UPGRADE_ISSUE: ChrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            getByteString = getByteString & Chr(Asc(lstrchar))
        Next
    End Function

    Function CreateGUID() As String
        Dim TypeLib As Object
        'UPGRADE_NOTE: The 'Scriptlet.TypeLib' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        TypeLib = CreateObject("Scriptlet.TypeLib")
        CreateGUID = Mid(TypeLib.Guid, 2, 36)
    End Function

</script>
<%Response.Expires = -1

%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="../../Common/Custom.css">  
 	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
	
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
	
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 3 $|$$Date: 28/03/16 18:33 $|$$Author: Jrengifo $"

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

<%If Request.QueryString("nZone") = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<FORM>
<%
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "valCAL7933"

Response.Write(mobjValues.StyleSheet())

mobjGeneral = New eBatch.MasiveCharge


'mstrPath = mobjGeneral.GetLoadFile(true)

lclsGeneral = New eGeneral.GeneralFunction
mstrKey = lclsGeneral.getsKey(CInt(Session("nUsercode")))
'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lclsGeneral = Nothing

'mstrPath = "C:\Inetpub\UpLoad\"
mstrPath = UCase(mobjValues.insGetSetting("LoadFile", vbNullString, "PATHS"))

If mstrPath = vbNullString Then
	mstrPath = UCase(mobjValues.insGetSetting("LoadFile", vbNullString, "Config"))
End If

'UPGRADE_NOTE: Object mobjGeneral may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGeneral = Nothing

mstrCommand = "&sModule=Policy&sProject=PolicyRep&sCodisplReload=" & Request.QueryString("sCodispl")

'+ Si no se han validado los campos de la página
If Request.QueryString("sCodisplReload") = vbNullString Then
	mstrErrors = insValPolicy
	Session("sErrorTable") = mstrErrors
	Session("sForm") = vbNullString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & "&sValPage=policyrepseq" & """, ""PolicyRepErrors"",660,330);")
            
		.Write(mobjValues.StatusControl(False, Request.QueryString("nZone"), Request.QueryString("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostPolicy Then
		If Request.QueryString("sCodisplReload") = vbNullString Then
			Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
		Else
			Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
		End If
	Else
		Response.Write("<SCRIPT>alert('Problemas en la actualización');</SCRIPT>")
		Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
	End If
End If

'UPGRADE_NOTE: Object mobjBatch may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjBatch = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjUploadRequest may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjUploadRequest = Nothing

%>
</FORM>
</BODY>
</HTML>





