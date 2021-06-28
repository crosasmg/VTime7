Option Strict Off
Option Explicit On
Imports System.IO
Imports System.Text
Imports VB = Microsoft.VisualBasic

Public Class FileSupport
    '**+Objective:
    '**+Version: $$Revision: 3 $
    '+Objetivo:
    '+Version: $$Revision: 3 $


    '%Objetivo: .
    '%Parámetros:
    '%    sFileName -
    '%    sBuffer -
    Public Shared Sub AddBufferToFile(ByVal sBuffer As String, Optional ByVal sFileName As String = "c:\NetFrameWork.log")
        Dim clsConfig As eRemoteDB.VisualTimeConfig
        Dim strFilename As String
        Dim lngHan As Integer

        ''On Error GoTo ErrorHandler
        If Trim(sFileName) > String.Empty Then

            clsConfig = New eRemoteDB.VisualTimeConfig
            strFilename = clsConfig.LoadSetting("VisualTIMERLog", "C:\VisualTIMERLog", "Paths")
            clsConfig = Nothing
            strFilename = strFilename & "\" & sFileName & "_VisualTIMER.log"

            lngHan = FreeFile()
            FileOpen(lngHan, strFilename, OpenMode.Append)
            sBuffer = TimeMilliSec() & "|" & sBuffer
            PrintLine(lngHan, sBuffer)
            FileClose(lngHan)
        End If

        Exit Sub
ErrorHandler:
        ProcError("eRemotedb.FileSupport.AddBufferToFile.eRemotedb.FileSupport.SaveBufferToFile(sFileName,sBuffer)", New Object() {sFileName, sBuffer})
    End Sub

    '%Objetivo: .
    '%Parámetros:
    '%    sFileName -
    '%    sBuffer -
    '%Objetivo: .
    '%Parámetros:
    '%    sFileName -
    '%    sBuffer -
    Public Shared Sub SaveBufferToFile(ByVal sFileName As String, ByVal sBuffer As String, Optional ByRef IsAppend As Boolean = False, Optional ByRef AddTimer As Boolean = False, Optional ByVal nP_Company as integer = intNull)
        Dim objContext As New eRemoteDB.ASPSupport
        Dim nMultiCompany As Integer

        'Dim lngHan As Integer
        'On Error GoTo ErrorHandler
        If Trim(sFileName) > String.Empty Then

            If sFileName.IndexOf("VisualTIMEConfig.xml") <= 0 Then
                nMultiCompany = nP_Company
                If nP_Company = intNull Then
                    nMultiCompany = objContext.GetASPSessionValue("nMultiCompany")
                End If
                Dim sFileNameIni As String = sFileName.Substring(0, sFileName.LastIndexOf("\")+1)
                Dim sFileNameEnd As String = sFileName.Substring(sFileName.LastIndexOf("\")+1)
                sFileName = sFileNameIni & Cstr(nMultiCompany) & "_" & sFileNameEnd
            End If

            'lngHan = FreeFile
            If AddTimer Then
                sBuffer = TimeMilliSec() & " " & sBuffer
            End If
            If IsAppend Then
                'FileOpen(lngHan, sFileName, OpenMode.Append)
                My.Computer.FileSystem.WriteAllText(sFileName, sBuffer, OpenMode.Append, Encoding.Unicode)
            Else
                'FileOpen(lngHan, sFileName, OpenMode.Output)
                My.Computer.FileSystem.WriteAllText(sFileName, sBuffer, False, Encoding.Unicode)
            End If
            'PrintLine(lngHan, sBuffer)
            'FileClose(lngHan)
        End If

        Exit Sub
ErrorHandler:
        ProcError("eRemotedb.FileSupport.SaveBufferToFile(sFileName,sBuffer)", New Object() {sFileName, sBuffer})
    End Sub

    '%Objetivo: .
    '%Parámetros:
    '%    sFileName -
    Public Shared Function LoadFileToBuffer(ByVal sFileName As String) As String
        Dim objContext As New eRemoteDB.ASPSupport
        Dim nMultiCompany As Integer

        ''On Error GoTo ErrorHandler
        If Trim(sFileName) > String.Empty Then

            If sFileName.IndexOf("VisualTIMEConfig.xml") <= 0 Then
                nMultiCompany = objContext.GetASPSessionValue("nMultiCompany")
                Dim sFileNameIni As String = sFileName.Substring(0, sFileName.LastIndexOf("\")+1)
                Dim sFileNameEnd As String = sFileName.Substring(sFileName.LastIndexOf("\")+1)
                sFileName = sFileNameIni & Cstr(nMultiCompany) & "_" & sFileNameEnd
            End If

            LoadFileToBuffer = eRemoteDB.FileSupport.LoadFileToText(sFileName)
        Else
            LoadFileToBuffer = String.Empty
        End If

        Exit Function
ErrorHandler:
        ProcError("FileSupport.LoadFileToBuffer(sFileName)", New Object() {sFileName})
    End Function

    '**%Objective:
    '%Objetivo:
    Public Shared Function TimeMilliSec() As String
        Dim sngTimer As Single
        Dim intValue As Short
        Dim strTime As String

        ''On Error GoTo ErrorHandler
        sngTimer = Microsoft.VisualBasic.Timer()

        If sngTimer >= 3600 Then
            intValue = Int(sngTimer / 3600.0!)
            sngTimer = sngTimer - (3600.0! * intValue)
        Else
            intValue = 0
        End If
        strTime = intValue.ToString("00") & ":"

        If sngTimer >= 60 Then
            intValue = Int(sngTimer / 60)
            sngTimer = sngTimer - (60 * intValue)
        Else
            intValue = 0
        End If
        strTime = strTime & intValue.ToString("00") & ":" & sngTimer.ToString("00.00000")
        If InStr(strTime, ",") > 0 Then
            Mid(strTime, InStr(strTime, ","), 1) = "."
        End If
        TimeMilliSec = strTime

        Exit Function
ErrorHandler:
        ProcError("FileSupport.TimeMilliSec()")
    End Function

    '%Objetivo: .
    '%Parámetros:
    '%    sFileName -
    Public Shared Function LoadFileToText(ByVal sFileName As String) As String
        Dim objContext As New eRemoteDB.ASPSupport
        Dim nMultiCompany As Integer
        ''On Error GoTo ErrorHandler
        
        If Trim(sFileName) > String.Empty And sFileName.IndexOf("VisualTIMEConfig.xml") <= 0 Then
            nMultiCompany = objContext.GetASPSessionValue("nMultiCompany")
            Dim sFileNameIni As String = sFileName.Substring(0, sFileName.LastIndexOf("\")+1)
            Dim sFileNameEnd As String = sFileName.Substring(sFileName.LastIndexOf("\")+1)
            sFileName = sFileNameIni & Cstr(nMultiCompany) & "_" & sFileNameEnd
        End If

        If IO.File.Exists(sFileName) Then
            LoadFileToText = IO.File.ReadAllText(sFileName)
        Else
            LoadFileToText = String.Empty
        End If

        Exit Function
ErrorHandler:
        ProcError("FileSupport.LoadFileToText(sFileName)", New Object() {sFileName})
    End Function

    '**%Objective:
    '**%Parameters:
    '%Objetivo:
    '%Parámetros:
    Public Shared Function Drive() As String
        ''On Error GoTo ErrorHandler
        Dim strDrive As String
        strDrive = My.Application.Info.DirectoryPath
        If strDrive > String.Empty Then
            Drive = Left(strDrive, 2) & "\"
        Else
            Drive = "D:\"
        End If

        Exit Function
ErrorHandler:
        ProcError("FileSupport.Drive()", New Object() {})
    End Function

End Class
