Imports InMotionGIT.Common.Helpers
Imports System.Configuration
Imports InMotionGIT.Report.Crystal.Designs
Imports InMotionGIT.Report.Crystal.Designs.ReportClientInformation
Imports InMotionGIT.Report.Crystal.Designs.ReportCargo

Public Class ClientReportMapper

    Public Function ReportMapping(Client As Client.Entity.Contracts.Client, SampleMode As Boolean, TraceMode As Boolean) As ContractsCargoCollection

        'If you setup the mapper in the form on TraceMode, the form contract is going to be serialize and save in the log folder.
        'this allows you get files from QA and production enviroments with the data and validates if there's a problem on the data, the mapper or the form.
        If TraceMode Then
            Serialize.SerializeToFile(Of Client.Entity.Contracts.Client)(Client,
                                                                     String.Format("{0}\ Report.Client.{1}.xml", ConfigurationManager.AppSettings("Path.Logs"), Client.ClientID),
                                                                     True)
        End If

        Dim cargo As New ContractsCargoCollection

        If Not SampleMode Then
            Dim ClientReport As New ClientInformationMap
            Dim clientHobby As ClientInformationHobbies
            Dim GeneralTools As New General
            Dim clientSport As ClientInformationSports

            'Mapped fields
            ClientReport.ClientID = Client.ClientIDFormated
            ClientReport.CompleteName = Client.CompleteClientName
            ClientReport.Birthdate = Client.BirthDate

            'reportCargo
            cargo.Add(New Cargo With {.XMLContract = GeneralTools.SerializeReportContract(ClientReport), .Name = "Main-ClientInformation"}) 'Main Report

            'Subreport objects cargo
            If Not IsNothing(Client.ClientHobbies) Then
                If Client.ClientHobbies.Count > 0 Then
                    For Each hobby In Client.ClientHobbies
                        clientHobby = New ClientInformationHobbies
                        clientHobby.CodigoHobby = hobby.Hobby
                        'If we need operations who can help to others reports mappers, we save them into the General class in the project.
                        clientHobby.Descripcion = General.GetDescription("TABLE5506", hobby.Hobby)
                        'SubReport cargo
                        cargo.Add(New Cargo With {.XMLContract = New General().SerializeReportContract(clientHobby), .Name = "hobbies"}) 'subreport name
                        clientHobby = Nothing
                    Next
                Else
                    cargo.Add(New Cargo With {.XMLContract = Nothing, .Name = "hobbies"}) 'subreport name
                End If
            Else
                'If there's is no going to be data in the subreport, we must assign to the cargo list one empty value 
                'with the subreport name to indicates the main report that the subreport is empty
                cargo.Add(New Cargo With {.XMLContract = Nothing, .Name = "hobbies"}) 'subreportname
            End If

            'Subreport objects cargo
            If Not IsNothing(Client.ClientSports) Then
                If Client.ClientSports.Count > 0 Then
                    For Each sport In Client.ClientSports
                        clientSport = New ClientInformationSports
                        clientSport.CodigoDeporte = sport.Sport
                        clientSport.Descripcion = General.GetDescription("TABLE512", sport.Sport)
                        'SubReport cargo
                        cargo.Add(New Cargo With {.XMLContract = New General().SerializeReportContract(clientSport), .Name = "sports"}) 'subreportname
                        clientSport = Nothing
                    Next
                Else
                    cargo.Add(New Cargo With {.XMLContract = Nothing, .Name = "sports"}) 'subreport name
                End If
            Else
                cargo.Add(New Cargo With {.XMLContract = Nothing, .Name = "sports"}) 'subreport name
            End If
        Else
            cargo = SampleModeMap()
        End If

        Return cargo
    End Function

    Public Function SampleModeMap() As ContractsCargoCollection
        Dim cargoContract As New ReportCargo.ContractsCargoCollection
        Dim ClientReport As New ClientInformationMap

        cargoContract.Add(New Cargo With {.Name = "Main-ClientInformation", .XMLContract = New General().SerializeReportContract(New ClientInformationMap With {.Birthdate = Today, .ClientID = "17400978-3", .CompleteName = "Alejandro Andrés Luza Catalán"})})

        cargoContract.Add(New Cargo With {.Name = "hobbies", .XMLContract = New General().SerializeReportContract(New ClientInformationHobbies With {.CodigoHobby = 1, .Descripcion = "Dibujar"})})
        cargoContract.Add(New Cargo With {.Name = "hobbies", .XMLContract = New General().SerializeReportContract(New ClientInformationHobbies With {.CodigoHobby = 2, .Descripcion = "Pintar"})})
        cargoContract.Add(New Cargo With {.Name = "hobbies", .XMLContract = New General().SerializeReportContract(New ClientInformationHobbies With {.CodigoHobby = 2, .Descripcion = "Yoga"})})

        cargoContract.Add(New Cargo With {.Name = "sports", .XMLContract = New General().SerializeReportContract(New ClientInformationSports With {.CodigoDeporte = 1, .Descripcion = "Surf"})})
        cargoContract.Add(New Cargo With {.Name = "sports", .XMLContract = New General().SerializeReportContract(New ClientInformationSports With {.CodigoDeporte = 2, .Descripcion = "Tenis"})})
        cargoContract.Add(New Cargo With {.Name = "sports", .XMLContract = New General().SerializeReportContract(New ClientInformationSports With {.CodigoDeporte = 3, .Descripcion = "Futbol"})})

        Return cargoContract
    End Function

End Class