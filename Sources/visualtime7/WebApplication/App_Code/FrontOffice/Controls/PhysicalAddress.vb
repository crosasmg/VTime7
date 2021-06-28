Imports Microsoft.VisualBasic
Imports DevExpress.Web.ASPxEditors

Namespace FrontOffice.Controls
    Public Class PhysicalAddress
#Region "Methods"

        ''' <summary>
        ''' Updates the section/ Actualiza los datos de sección
        ''' </summary>
        ''' <param name="nameSource"></param>
        ''' <param name="valueSource"></param>
        ''' <param name="textSource"></param>
        ''' <param name="indexSource"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdateValuesChache(nameSource As String, valueSource As String, textSource As String, indexSource As Integer, Optional prefix As String = "") As Boolean
            Dim result As Boolean = False
            If Not IsNothing(Web.HttpContext.Current.Session(nameSource)) Then
                Dim valueSeccion = Web.HttpContext.Current.Session(nameSource)

                If valueSeccion.GetType = GetType(ASPxComboBox) Then
                    Dim comboBoxTemporal As ASPxComboBox = DirectCast(valueSeccion, ASPxComboBox)
                    If comboBoxTemporal.IsNotEmpty Then
                        With comboBoxTemporal
                            .Text = textSource
                            .Value = valueSource

                            If .SelectedItem.IsNotEmpty Then
                                .SelectedItem.Text = textSource
                                .SelectedItem.Value = valueSource

                                System.Web.HttpContext.Current.Session.Add(String.Format("{0}_Data", nameSource), New InMotionGIT.Common.DataType.LookUpValue With {.Code = valueSource, .Description = textSource})

                            Else
                                .SelectedItem = New ListEditItem With {.Value = valueSource, .Text = textSource}
                                System.Web.HttpContext.Current.Session.Add(String.Format("{0}_Data", nameSource), New InMotionGIT.Common.DataType.LookUpValue With {.Code = valueSource, .Description = textSource})
                            End If

                            If prefix.IsNotEmpty Then
                                If Not IsNothing(System.Web.HttpContext.Current.Session(String.Format("{0}SourceValue_{1}", prefix, nameSource.Split("_")(1)))) Then
                                    System.Web.HttpContext.Current.Session(String.Format("{0}SourceValue_{1}", prefix, nameSource.Split("_")(1))) = indexSource
                                End If
                            End If

                        End With

                        System.Web.HttpContext.Current.Session(nameSource) = comboBoxTemporal
                        result = True
                    End If
                Else
                    Dim textBoxTemporal As ASPxTextBox = DirectCast(valueSeccion, ASPxTextBox)
                    If textBoxTemporal.IsNotEmpty Then
                        With textBoxTemporal
                            .Text = textSource
                        End With
                        System.Web.HttpContext.Current.Session(nameSource) = textBoxTemporal
                        result = True
                    End If
                End If
            End If
            Return result
        End Function

        ''' <summary>
        ''' Get List LookUp Possible Valúes Of Names Of Parts Of Address Table /Obtener lista de búsqueda posibles valores de nombres de los componentes de la mesa de Dirección
        ''' </summary>
        ''' <param name="countryCode"></param>
        ''' <param name="TypePhyTicalAddressCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListLookUpPossibleValuesOfNamesOfPartsOfAddressTable(countryCode As Integer,
                                                                                       TypePhyTicalAddressCode As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Using clien = FrontOffice.Controls.PhysicalAddress.ClientAddress()
                Dim consumerInformation As New InMotionGIT.Common.Contracts.Process.ConsumerInformation
                Dim security As New InMotionGIT.Common.Contracts.Process.SecurityInformation

                Dim country As New InMotionGIT.Common.DataType.LookUpValueExtend
                With country
                    .Code = countryCode
                    .Description = String.Empty
                End With

                Dim typeOfRoute As New InMotionGIT.Common.DataType.LookUpValueExtend
                With typeOfRoute
                    .Code = TypePhyTicalAddressCode
                End With

                With security
                    .BranchOffice = 1
                    .CompanyType = String.Empty
                    .Schema = String.Empty
                    .SecurityLevel = 1
                    .Usercode = "1821"
                End With

                With consumerInformation
                    .CompanyId = 1
                    .Country = countryCode
                    .Security = security
                    .Site = "VisualTIME"
                    .UserInitials = "VTNET"
                    .UserPassword = "12345"
                    .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
                End With

                result = clien.LookUpPossibleValuesOfNamesOfPartsOfAddressTable(country, typeOfRoute, consumerInformation)
            End Using
            Return result
        End Function

        ''' <summary>
        '''Get List LookUp Possible Values Allowed For Geographic Zone / Obtener lista de búsqueda Valores posibles permitidas para Zona Geográfica
        ''' </summary>
        ''' <param name="countryCode"></param>
        ''' <param name="geographicZoneLevelId"></param>
        ''' <param name="geographicZoneId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListLookUpPossibleValuesAllowedForGeographicZone(countryCode As Integer,
                                                                        geographicZoneLevelId As Integer,
                                                                        geographicZoneId As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim resultCollecction As New InMotionGIT.AddressManager.Contract.General.GeographicZoneCollection
            Using clien = FrontOffice.Controls.PhysicalAddress.ClientAddress()

                If String.IsNullOrEmpty(countryCode) Then
                    countryCode = ConfigurationManager.AppSettings("CountryCode")
                End If

                Dim country As New InMotionGIT.Common.DataType.LookUpValueExtend
                With country
                    .Code = countryCode
                    .Description = String.Empty
                End With

                Dim consumerInformation As New InMotionGIT.Common.Contracts.Process.ConsumerInformation
                Dim security As New InMotionGIT.Common.Contracts.Process.SecurityInformation
                Dim geographicZoneTableId As Integer = 1
                With security
                    .BranchOffice = 1
                    .CompanyType = String.Empty
                    .Schema = String.Empty
                    .SecurityLevel = 1
                    .Usercode = "1821"
                End With

                With consumerInformation
                    .CompanyId = 1
                    .Country = countryCode
                    .Security = security
                    .Site = "VisualTIME"
                    .UserInitials = "VTNET"
                    .UserPassword = "12345"
                    .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
                End With

                resultCollecction = clien.LookUpPossibleValuesAllowedForGeographicZone(country,
                                                                            geographicZoneTableId,
                                                                            geographicZoneLevelId,
                                                                            geographicZoneId,
                                                                            consumerInformation)

            End Using

            If resultCollecction.Count <> 0 Then
                result = (From item In resultCollecction Select item.ZoneLevel).ToList
            End If
            With result
                If result.Count = 0 Then
                    .Add(New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = -1, .Description = "", .ShortDescription = ""})
                Else
                    .Insert(0, New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = -1, .Description = "", .ShortDescription = ""})
                End If
            End With
            Return result
        End Function

        ''' <summary>
        ''' Obtener lista de búsqueda posibles valores de la Tabla País/Get List LookUp Possible Values Of Country Table
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListLookUpPossibleValuesOfCountryTable() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim key As String = "LookUpPossibleValuesOfCountry"
            If InMotionGIT.Common.Helpers.Caching.NotExist(key) Then
                Using clien = FrontOffice.Controls.PhysicalAddress.ClientCommon()
                    Dim consumerInformation As New InMotionGIT.Common.Contracts.Process.ConsumerInformation
                    Dim security As New InMotionGIT.Common.Contracts.Process.SecurityInformation

                    With security
                        .BranchOffice = 1
                        .CompanyType = String.Empty
                        .Schema = String.Empty
                        .SecurityLevel = 1
                        .Usercode = "1821"
                    End With

                    With consumerInformation
                        .CompanyId = 1
                        .Country = ConfigurationManager.AppSettings("CountryCode")
                        .Security = security
                        .Site = "VisualTIME"
                        .UserInitials = "VTNET"
                        .UserPassword = "12345"
                        .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
                    End With

                    result = clien.LookUpPossibleValuesOfCountryTable(consumerInformation)
                    InMotionGIT.Common.Helpers.Caching.SetItem(key, result)
                End Using
            Else
                result = InMotionGIT.Common.Helpers.Caching.GetItem(key)
            End If

            Return result
        End Function

        ''' <summary>
        ''' Obtener las operaciones de búsqueda posibles valores de zona horaria Tabla/Get LookUp Possible Values Of Time Zone Table
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetLookUpPossibleValuesOfTimeZoneTable() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Using clien = FrontOffice.Controls.PhysicalAddress.ClientAddress()

                Dim consumerInformation As New InMotionGIT.Common.Contracts.Process.ConsumerInformation
                Dim security As New InMotionGIT.Common.Contracts.Process.SecurityInformation

                With security
                    .BranchOffice = 1
                    .CompanyType = String.Empty
                    .Schema = String.Empty
                    .SecurityLevel = 1
                    .Usercode = "1821"
                End With

                With consumerInformation
                    .CompanyId = 1
                    .Country = ConfigurationManager.AppSettings("CountryCode")
                    .Security = security
                    .Site = "VisualTIME"
                    .UserInitials = "VTNET"
                    .UserPassword = "12345"
                    .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
                End With

                result = clien.LookUpPossibleValuesOfTimeZoneTable(consumerInformation)

                result.Insert(0, New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = -1, .Description = ""})

            End Using
            Return result

        End Function

        ''' <summary>
        ''' Get possible values of zipcode
        ''' </summary>
        ''' <param name="comparisionValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListLookUpPossibleValuesOfZipCodeTable(countryCode As String, comparisionValue As String) As List(Of InMotionGIT.Common.DataType.LookUpValue)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValue)
            Using clien = ClientAddress()
                Dim country As New InMotionGIT.Common.DataType.LookUpValueExtend
                With country
                    .Code = countryCode
                    .Description = String.Empty
                End With

                Dim consumerInformation As New InMotionGIT.Common.Contracts.Process.ConsumerInformation
                Dim security As New InMotionGIT.Common.Contracts.Process.SecurityInformation
                Dim geographicZoneTableId As Integer = 1
                Dim geographicZoneLevelId As Integer = 1
                With security
                    .BranchOffice = 1
                    .CompanyType = String.Empty
                    .Schema = String.Empty
                    .SecurityLevel = 1
                    .Usercode = "1821"
                End With

                With consumerInformation
                    .CompanyId = 1
                    .Country = countryCode
                    .Security = security
                    .Site = "VisualTIME"
                    .UserInitials = "VTNET"
                    .UserPassword = "12345"
                    .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
                End With

                result = clien.LookUpPossibleValuesOfZipCodeTable(country, comparisionValue, consumerInformation)

                result.Insert(0, New InMotionGIT.Common.DataType.LookUpValue With {.Code = "", .Description = ""})

            End Using
            Return result
        End Function

        ''' <summary>
        ''' Get List LookUp Possible Values Of Type Of Route Table/Obtener lista de búsqueda posibles valores de tipo de tabla de rutas
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListLookUpPossibleValuesOfTypeOfRouteTable() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Using clien = FrontOffice.Controls.PhysicalAddress.ClientAddress()

                Dim consumerInformation As New InMotionGIT.Common.Contracts.Process.ConsumerInformation
                Dim security As New InMotionGIT.Common.Contracts.Process.SecurityInformation

                With security
                    .BranchOffice = 1
                    .CompanyType = String.Empty
                    .Schema = String.Empty
                    .SecurityLevel = 1
                    .Usercode = "1821"
                End With

                With consumerInformation
                    .CompanyId = 1
                    .Country = ConfigurationManager.AppSettings("CountryCode")
                    .Security = security
                    .Site = "VisualTIME"
                    .UserInitials = "VTNET"
                    .UserPassword = "12345"
                    .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
                End With

                result = clien.LookUpPossibleValuesOfTypeOfRouteTable(consumerInformation)

                result.Insert(0, New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = -1, .Description = ""})

            End Using
            Return result
        End Function

        ''' <summary>
        ''' Obtener lista de búsqueda posibles valores de tipo de tabla de dirección física/Get List LookUp Possible Values Of Type Of Physical Address Table
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListLookUpPossibleValuesOfTypeOfPhysicalAddressTable() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Using clien = FrontOffice.Controls.PhysicalAddress.ClientAddress()

                Dim consumerInformation As New InMotionGIT.Common.Contracts.Process.ConsumerInformation
                Dim security As New InMotionGIT.Common.Contracts.Process.SecurityInformation

                With security
                    .BranchOffice = 1
                    .CompanyType = String.Empty
                    .Schema = String.Empty
                    .SecurityLevel = 1
                    .Usercode = "1821"
                End With

                With consumerInformation
                    .CompanyId = 1
                    .Country = ConfigurationManager.AppSettings("CountryCode")
                    .Security = security
                    .Site = "VisualTIME"
                    .UserInitials = "VTNET"
                    .UserPassword = "12345"
                    .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
                End With

                result = clien.LookUpPossibleValuesOfTypeOfPhysicalAddressTable(consumerInformation)

                result.Insert(0, New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = -1, .Description = ""})

            End Using
            Return result
        End Function

        ''' <summary>
        ''' Get List LookUp Possible Values Of Geographic Zone Table By Level/ Obtener lista de búsqueda posibles valores de la tabla Zona Geográfica Por Nivel
        ''' </summary>
        ''' <param name="countryCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListLookUpPossibleValuesOfGeographicZoneTableByLevel(countryCode As String) As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            Using clien = ClientAddress()

                If String.IsNullOrEmpty(countryCode) Then
                    countryCode = ConfigurationManager.AppSettings("CountryCode")
                End If

                Dim country As New InMotionGIT.Common.DataType.LookUpValueExtend
                With country
                    .Code = countryCode
                    .Description = String.Empty
                End With

                Dim consumerInformation As New InMotionGIT.Common.Contracts.Process.ConsumerInformation
                Dim security As New InMotionGIT.Common.Contracts.Process.SecurityInformation
                Dim geographicZoneTableId As Integer = 1
                Dim geographicZoneLevelId As Integer = 1
                With security
                    .BranchOffice = 1
                    .CompanyType = String.Empty
                    .Schema = String.Empty
                    .SecurityLevel = 1
                    .Usercode = "1821"
                End With

                With consumerInformation
                    .CompanyId = 1
                    .Country = countryCode
                    .Security = security
                    .Site = "VisualTIME"
                    .UserInitials = "VTNET"
                    .UserPassword = "12345"
                    .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
                End With

                result = clien.LookUpPossibleValuesOfGeographicZoneTableByLevel(country, geographicZoneTableId, geographicZoneLevelId, consumerInformation)

            End Using

            If result.IsNotEmpty Then
                result = (From a In result Order By a.Code Ascending Select a).ToList
                result.Insert(0, New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = -1, .Description = ""})
            End If

            Return result
        End Function

#End Region

#Region "Clients"
        ''' <summary>
        ''' Get instance client of by Common
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ClientCommon() As InMotionGIT.CommonService.Proxy.CommonService.PublicClient
            Dim result As New InMotionGIT.CommonService.Proxy.CommonService.PublicClient("BasicHttpBinding_CommonService")
            Return result
        End Function

        ''' <summary>
        ''' Get instance client of by address
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ClientAddress() As InMotionGIT.AddressManager.Proxy.AddressManager.PublicClient
            Dim result As New InMotionGIT.AddressManager.Proxy.AddressManager.PublicClient("BasicHttpBinding_IPublic")
            Return result
        End Function
#End Region

    End Class

End Namespace