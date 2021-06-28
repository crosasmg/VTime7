Imports System.Text
Imports InMotionGIT.Report.Crystal.Designs.ReportCargo
Imports InMotionGIT.Report.Crystal.Designs.ReportClientInformation
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class ClientInformationSampleTest

    'Corresponde a la IP del servidor con el que estamos trabajando
    Property enviromentIP As String = "http://40.117.79.124"
    'Nombre del reporte
    Property ReportName As String = "ClientInformation.rpt"

    Public Function SampleData() As ContractsCargoCollection
        Dim cargo As New ContractsCargoCollection

        'Contrato principal
        Dim sampleClient = New ClientInformationMap
        With sampleClient
            .ClientID = "17400978-3"
            .Birthdate = Today
            .CompleteName = "Alejandro Andres Luza Catalan"
        End With
        'Adjuntar el objeto serializado en el cargo
        cargo.Add(New Cargo With {.Name = "ClientInformationMap", .XMLContract = New General().SerializeReportContract(sampleClient)})

        'Carga del hobbie
        Dim sampleHobbie As New ClientInformationHobbies
        With sampleHobbie
            .CodigoHobby = 1
            .Descripcion = "Pintura"
        End With
        'Adjuntar el objeto serializado en el cargo
        cargo.Add(New Cargo With {.Name = "ClientInformationHobbies", .XMLContract = New General().SerializeReportContract(sampleHobbie)})

        'Carga de 1 deporte
        Dim sampleSport As New ClientInformationSports
        With sampleSport
            .CodigoDeporte = 1
            .Descripcion = "Futbol"
        End With
        'Adjuntar el objeto serializado en el cargo
        cargo.Add(New Cargo With {.Name = "ClientInformationSports", .XMLContract = New General().SerializeReportContract(sampleSport)})

        'Carga de 2 deporte
        Dim sampleSport2 As New ClientInformationSports
        With sampleSport2
            .CodigoDeporte = 2
            .Descripcion = "Basketball"
        End With
        'Adjuntar el objeto serializado en el cargo
        cargo.Add(New Cargo With {.Name = "ClientInformationSports", .XMLContract = New General().SerializeReportContract(sampleSport2)})

        Return cargo
    End Function

    <TestMethod()>
    Public Sub ReportProxyTest()
        'resultado - ruta del reporte
        Dim result As String = String.Empty
        'Significa si quiero la ruta para descarga o la ruta real
        'ruta descarga - reportdownload.aspx
        'ruta real - C:\VisualTIMENET\Temp\Tfiles\Reports\nombre del reporte
        Dim urlMode As Boolean = True
        'concatena opción para ver el reporte
        Dim viewMode As Boolean = False
        'Ejecutamos el proxy del servicio
        result = InMotionGIT.Report.Crystal.Proxy.Client.ExecuteBuildReport(ReportName, urlMode, viewMode, SampleData)
        'Valida si se crea el reporte
        Assert.AreNotEqual(String.Empty, result)
        'Levanta reporte localmente para revisión
        Process.Start(String.Concat(enviromentIP, result, "&view"))
    End Sub

    <TestMethod()>
    Public Sub ReportMapperTest()
        'Cliente de ejemplo de BD
        Dim clientID As String = "00000013126341"
        Dim atDate As Date = Today
        Dim childFilter As String = "All"
        'Cargo con la información
        Dim cargo As InMotionGIT.Report.Crystal.Designs.ReportCargo.ContractsCargoCollection
        Dim sampleMode As Boolean = False
        Dim traceMode As Boolean = False
        Dim result As String = String.Empty

        Dim client As InMotionGIT.Client.Entity.Contracts.Client = (New InMotionGIT.Client.Proxy.Manager).Retrieve(clientID, atDate, childFilter)

        cargo = (New InMotionGIT.Report.Crystal.Mappers.ClientInformationMapper).ReportMapping(client, sampleMode, traceMode)

        result = InMotionGIT.Report.Crystal.Proxy.Client.ExecuteBuildReport(ReportName, True, False, cargo)

        Assert.AreNotEqual(String.Empty, result)
        Process.Start(String.Concat(enviromentIP, result, "&view"))
    End Sub

End Class