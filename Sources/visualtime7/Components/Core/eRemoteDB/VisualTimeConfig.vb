Option Strict Off
Option Explicit On

Imports System.Configuration

Public Class VisualTimeConfig

    Public Sub New()
        If Len(eRemoteDB.Common.VisualTIMEConfigContent) = 0 Then
            Dim configFile As String = ConfigurationManager.AppSettings("BackOfficeConfigurationFile")

            If String.IsNullOrEmpty(configFile) Then
                Dim strDrive As String
                strDrive = My.Application.Info.DirectoryPath
                If strDrive > String.Empty Then
                    strDrive = Left(strDrive, 2)
                Else
                    strDrive = "D:"
                End If
                configFile = strDrive & "\VisualTIMENet" & "\Configuration\VisualTIMEConfig.xml"
            End If

            eRemoteDB.Common.VisualTIMEConfigContent = eRemoteDB.FileSupport.LoadFileToText(configFile)
        End If
    End Sub

    '**%Objective:
    '**%Parameters:
    '**%    sKey     -
    '**%    Default  -
    '**%    sGroup   -
    '**%    bDecrypt -
    '%Objetivo:
    '%Parámetros:
    '%      sKey     -
    '%      Default  -
    '%      sGroup   -
    '%      bDecrypt -
    Public Function LoadSetting(ByVal sKey As String, Optional ByVal Default_Renamed As Object = Nothing, Optional ByVal sGroup As String = "Settings", Optional ByVal bDecrypt As Boolean = False) As String
        Dim lstrGroup As String

        'On Error GoTo ErrorHandler

        sGroup = Replace(sGroup, Space(1), String.Empty)
        lstrGroup = GetBlock(eRemoteDB.Common.VisualTIMEConfigContent, sGroup, True)
        If lstrGroup <> String.Empty Then
            If Not bDecrypt Then
                LoadSetting = GetBlock(lstrGroup, sKey, True)
            Else
                LoadSetting = CryptSupport.DecryptString(GetBlock(lstrGroup, sKey, True))
            End If
        Else
            LoadSetting = String.Empty
        End If

        If LoadSetting.Length = 0 And Default_Renamed IsNot Nothing Then
            LoadSetting = Default_Renamed
        End If

        Exit Function
ErrorHandler:
        ProcError("VisualTIMEConfig.LoadSetting(sKey,Default,sGroup,bDecrypt)", New Object() {sKey, Default_Renamed, sGroup, bDecrypt})
    End Function

    '**%Objective:
    '**%Parameters:
    '**%    sSource    -
    '**%    sTag       -
    '**%    bNotDelete -
    '%Objetivo:
    '%Parámetros:
    '%      sSource    -
    '%      sTag       -
    '%      bNotDelete -
    Private Function GetBlock(ByRef sSource As String, ByVal sTag As String, Optional ByVal bNotDelete As Boolean = False) As String
        Dim strLabel As String
        Dim lngIniPosition As Integer
        Dim lngEndPosition As Integer

        'On Error GoTo ErrorHandler

        strLabel = "<" & UCase(sTag) & ">"
        lngIniPosition = InStr(UCase(sSource), strLabel)
        If lngIniPosition > 0 Then
            lngIniPosition = lngIniPosition + Len(strLabel)
            strLabel = "</" & UCase(sTag) & ">"
            lngEndPosition = InStr(lngIniPosition, UCase(sSource), strLabel)
            If lngEndPosition > 0 Then
                GetBlock = Mid(sSource, lngIniPosition, lngEndPosition - lngIniPosition)
                If Not bNotDelete Then
                    sSource = Left(sSource, lngIniPosition + 1) & Mid(sSource, lngEndPosition)
                End If
            Else
                GetBlock = String.Empty
            End If
        Else
            GetBlock = String.Empty
        End If

        Exit Function
ErrorHandler:
        ProcError("VisualTIMEConfig.GetBlock(sSource,sTag,bNotDelete)", New Object() {sSource, sTag, bNotDelete})
    End Function

    '**%Objective:
    '**%Parameters:
    '**%    sStream -
    '%Objetivo:
    '%Parámetros:
    '%      sStream -
    Public Function Encrypt(ByVal sStream As String) As String
        'On Error GoTo ErrorHandler

        Encrypt = CryptSupport.EncryptString(sStream)

        Exit Function
ErrorHandler:
        ProcError("VisualTIMEConfig.Encrypt(sStream)", New Object() {sStream})
    End Function

    '**%Objective:
    '**%Parameters:
    '**%    sStream -
    '%Objetivo:
    '%Parámetros:
    '%      sStream -
    Public Function Decrypt(ByVal sStream As String) As String
        'On Error GoTo ErrorHandler

        Decrypt = CryptSupport.DecryptString(sStream)

        Exit Function
ErrorHandler:
        ProcError("VisualTIMEConfig.Decrypt(sStream)", New Object() {sStream})
    End Function

    Public Function GetCompanySettings(ByVal id As Short, ByRef companyName As String, ByRef companyUser As String, ByRef companyPassword As String) As Boolean
        Dim multiCompanies As String = GetBlock(eRemoteDB.Common.VisualTIMEConfigContent, "MultiCompanies", True)
        Dim indexBegin As Integer
        Dim tags() As String

        companyName = String.Empty
        companyUser = String.Empty
        companyPassword = String.Empty

        indexBegin = multiCompanies.IndexOf("<Company id='" + id.ToString + "'") + ("<Company id='" + id.ToString + "'").Length
        If indexBegin > ("<Company id='" + id.ToString + "'").Length Then
            multiCompanies = multiCompanies.Substring(indexBegin, multiCompanies.IndexOf("/>", indexBegin) - indexBegin).Trim
            multiCompanies = multiCompanies.Replace("name=", "=")
            multiCompanies = multiCompanies.Replace("user=", "=")
            multiCompanies = multiCompanies.Replace("password=", "=")
            multiCompanies = multiCompanies.Replace("'", "")
            tags = multiCompanies.Split("=")

            companyName = tags(1).Trim
            companyUser = tags(2).Trim
            companyPassword = tags(3).Trim
        End If

        GetCompanySettings = (companyName <> String.Empty)
    End Function

End Class
