Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports Thinktecture.IdentityModel.Client
Imports InMotionGIT.Policy.Entity.Contracts
Imports InMotionGIT.Report.Crystal.Designs.ReportCargo
Imports System.Configuration

<TestClass()>
Public Class General

    Public Shared Function LoadCargo(name As String, instance As Object) As Cargo
        Dim cargo As New Cargo
        Dim xml As String = New General().SerializeReportContract(instance)
        With cargo
            .Name = name
            .XMLContract = xml
        End With
        Return cargo
    End Function

    Public Function SerializeReportContract(Info As Object) As String
        Try
            Dim xml_serializer As New XmlSerializer(Info.GetType)
            Dim string_writer As New StringWriter
            xml_serializer.Serialize(string_writer, Info)
            Return string_writer.ToString()
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Solicita al servicio del STS un token de pruebas para el Proxy con un usuario y consumidor válidos.
    ''' </summary>
    Public Shared Function RequestToken() As String
        'TODO: Versión framework >= 4.5
        Dim _tokenClient = New OAuth2Client(New Uri(ConfigurationManager.AppSettings("Path.STS.Request")), "InMotionWF", "BCEBF65F-2BC8-4AB8-B0D0-8247A5DA9983")
        Dim aditionalValues As New Dictionary(Of String, String)
        aditionalValues.Add("usercode", "aluza@grupoinmotion.com")

        Dim tokenResponse = _tokenClient.RequestCustomGrantAsync("customWF", "InMotionScope", aditionalValues).Result

        Return tokenResponse.AccessToken
    End Function

End Class