Imports System.Configuration
Imports System.Data.Common
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Report.Crystal.Designs
Imports InMotionGIT.Report.Crystal.Designs.ReportClientInformation
Imports InMotionGIT.Report.Crystal.Designs.ReportCargo
Public Class ClientInformationMapper

    Public Function ReportMapping(Client As Client.Entity.Contracts.Client, SampleMode As Boolean, TraceMode As Boolean) As ContractsCargoCollection

        'Si es que se ejecuta el reporte en Sample Mode
        'se ejecutará un método que llenará de forma default con data el contrato de datos del reporte
        'y retornará la data lista para pruebas
        'Esta acción se incorpora por dos razones
        '1.- Permite poder probar el reporte de la forma sin tener que depender del mapeo
        '2.- Permite poder aplicar rápidamente reportes ficticios que podrían ser reales para presentaciones u otras actividades de venta.
        If SampleMode Then
            Return SampleDataReport()
        Else
            'Si se requiere saber la información que está llegando de la forma hacia el reporte
            'se le debe indicar al análista que active el TraceMode para poder ver el XML del objeto
            'que se está mapeando en la carpeta Temp/Logs
            If TraceMode And Not IsNothing(Client) Then
                InMotionGIT.Common.Helpers.Serialize.SerializeToFile(Client, String.Format("{0}\Report.ClientInformation.{1}.xml", ConfigurationManager.AppSettings("Path.Logs"), Client.ClientID, True))
            End If

            Return ReportMap(Client)
        End If
    End Function

    Public Function ReportMap(Client As Client.Entity.Contracts.Client) As ContractsCargoCollection
        'Clase que transporta la info desde el code activity "reportDNE" hacia el servicio
        Dim ResultCargo As New ContractsCargoCollection
        'Contrato de datos del reporte. Incluye el diseño y la estructura del reporte PRINCIPAL.
        Dim ClientReport As New ClientInformationMap
        'Contratode datos del subreporte "hobbies"
        Dim clientHobby As ClientInformationHobbies
        'Clase con herramientas para serializar y buscar datos particulares en los reportes.
        Dim GeneralTools As New General
        'Contratode datos del subreporte "sports"
        Dim clientSport As ClientInformationSports

        'Mapeo de los datos.
        'Se toma la información que viene de la forma (Clase Client) y se adhiere al contrato de datos del reporte.
        ClientReport.ClientID = Client.ClientIDFormated
        ClientReport.CompleteName = Client.CompleteClientName
        ClientReport.Birthdate = Client.BirthDate

        'reportCargo
        'Se carga la clase que contiene la colección con los objetos serializados y configurados para el servicio de reportes.
        ResultCargo.Add(New Cargo With {.XMLContract = GeneralTools.SerializeReportContract(ClientReport), .Name = "ClientInformationMap"}) 'Main Report

        'Subreport objects cargo
        'Se hace el mapeo y carga del sub reporte "hobbies"
        If Not IsNothing(Client.ClientHobbies) Then
            If Client.ClientHobbies.Count > 0 Then
                For Each hobby In Client.ClientHobbies
                    clientHobby = New ClientInformationHobbies
                    clientHobby.CodigoHobby = hobby.Hobby
                    clientHobby.Descripcion = GetDescription("TABLE5506", hobby.Hobby)
                    'SubReport cargo
                    ResultCargo.Add(New Cargo With {.XMLContract = New General().SerializeReportContract(clientHobby), .Name = "ClientInformationHobbies"}) 'subreportname
                    clientHobby = Nothing
                Next
            Else
                'Si es que el cliente o la colección que se mapea tiene menos de 1 valor, se asigna por defecto un valor con el nombre del subreporte y el contrato de datos vacio.
                ResultCargo.Add(New Cargo With {.XMLContract = String.Empty, .Name = "ClientInformationHobbies"}) 'Setup dataset empty for subreport
            End If
        Else
            'Si es que el cliente o el objeto mapeado no tiene datos, se asigna por defecto un valor con el nombre del subreporte y el contrato de datos vacio.
            ResultCargo.Add(New Cargo With {.XMLContract = String.Empty, .Name = "ClientInformationHobbies"}) 'Setup dataset empty for subreport
        End If


        'Subreport objects cargo
        If Not IsNothing(Client.ClientSports) Then
            If Client.ClientSports.Count > 0 Then
                For Each sport In Client.ClientSports
                    clientSport = New ClientInformationSports
                    clientSport.CodigoDeporte = sport.Sport
                    clientSport.Descripcion = GetDescription("TABLE512", sport.Sport)
                    'SubReport cargo
                    ResultCargo.Add(New Cargo With {.XMLContract = New General().SerializeReportContract(clientSport), .Name = "ClientInformationSports"}) 'subreportname
                    clientSport = Nothing
                Next
            Else
                ResultCargo.Add(New Cargo With {.XMLContract = String.Empty, .Name = "ClientInformationSports"}) 'Setup dataset empty for subreport
            End If
        Else
            ResultCargo.Add(New Cargo With {.XMLContract = String.Empty, .Name = "ClientInformationSports"}) 'Setup dataset empty for subreport
        End If

        Return ResultCargo
    End Function

    Private Function SampleDataReport() As ContractsCargoCollection
        Dim ResultCargo As New ContractsCargoCollection
        Dim GeneralTools As New General

        'Main report - Sample data
        Dim ClientReport As New ClientInformationMap
        With ClientReport
            .ClientID = "17400978-3"
            .CompleteName = "Alejandro Andrés Luza Catalán"
            .Birthdate = New Date(1990, 6, 13)
        End With
        'Add Sample data to CargoCollection
        ResultCargo.Add(New Cargo With {.Name = "Main-ClientInformation", .XMLContract = GeneralTools.SerializeReportContract(ClientReport)})

        'Sub report hobbies - Sample data
        Dim clientHobby As New ClientInformationHobbies
        With clientHobby
            .CodigoHobby = 1
            .Descripcion = "Pintura en Oleo"
        End With
        'Add Sample data to CargoCollection
        ResultCargo.Add(New Cargo With {.Name = "hobbies", .XMLContract = GeneralTools.SerializeReportContract(clientHobby)})

        'Sub report sports - Sample data
        Dim clientSport As New ClientInformationSports
        With clientSport
            .Descripcion = "Surf"
            .CodigoDeporte = 1
        End With

        'Add Sample data to CargoCollection
        ResultCargo.Add(New Cargo With {.Name = "sports", .XMLContract = GeneralTools.SerializeReportContract(clientSport)})

        'Return Sample data
        Return ResultCargo
    End Function

    Public Function GetDescription(tableName As String, code As String) As String
        Return InMotionGIT.General.Proxy.General.TableValueDescription(tableName, code)
    End Function

End Class