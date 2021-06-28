Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.ObjectModel
Imports System.Configuration
Imports System.IO
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class SincFiles
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetAssemblyInfo() As DataTable
        Dim filesCollectionTable As New DataTable("FilesData")
        Dim newRow As DataRow
        Dim xmlFileDocument As String = String.Format("{0}\SyncFilesList.xml", ConfigurationManager.AppSettings("RepositoryPath"))
        Dim pathFile As String
        Dim fileInfo As FileInfo

        If File.Exists(xmlFileDocument) Then
            With filesCollectionTable.Columns
                .Add("Name", GetType(String))
                .Add("LastActivityDate", GetType(DateTime))
                .Add("Path", GetType(String))
            End With

            Dim xmlDocument As XDocument = XDocument.Load(xmlFileDocument)

            Dim queryResult = From x In xmlDocument.<Files>.<File> _
                             Select x.@name, x.@path Order By path

            For Each syncFile In queryResult
                pathFile = String.Format("{0}\{1}", ConfigurationManager.AppSettings(syncFile.path), syncFile.name)

                If File.Exists(pathFile) Then
                    newRow = filesCollectionTable.NewRow

                    fileInfo = New FileInfo(pathFile)

                    With fileInfo
                        newRow("Name") = .Name
                        newRow("LastActivityDate") = .LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")
                        newRow("Path") = syncFile.path
                    End With

                    filesCollectionTable.Rows.Add(newRow)
                End If
            Next
        End If

        Return filesCollectionTable
    End Function
End Class
