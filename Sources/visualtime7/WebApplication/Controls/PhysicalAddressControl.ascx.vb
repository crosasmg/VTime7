#Region "Imports"

Imports System.Globalization
Imports InMotionGIT.AddressManager.Contract
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports System.Web.Services
Imports System.Web.Script.Services
Imports DevExpress.Web.ASPxPanel
Imports InMotionGIT.AddressManager.Contract.General
Imports System.ComponentModel

#End Region

Partial Public Class Controls_PhysicalAddressControl
    Inherits System.Web.UI.UserControl

#Region "Event"

    Public Sub setValuesAll()

        If _Value.IsEmpty Then
            _Value = New InMotionGIT.AddressManager.Contract.Models.AddressPhysicalAddressDLI
            With _Value
                .Address = New InMotionGIT.AddressManager.Contract.General.Address
                .PhysicalAddress = New InMotionGIT.AddressManager.Contract.General.PhysicalAddress
            End With
        End If

        If _Value.Address.IsNotEmpty Then
            With _Value.Address
                chbSendProblemAddress.Checked = .AddressHadProblemsForDelivery
                chbCorrespondenceAddress.Checked = .UseAsMailingAddress
                chbCollectionAddress.Checked = .UseAsBillingAddress
                ddlLastContact.Value = .DateOfLastSuccesfulContactAtThisAddress
            End With
        End If

        If _Value.PhysicalAddress.IsNotEmpty Then
            With _Value.PhysicalAddress
                If Not IsNothing(.Country) AndAlso .Country.Code.IsNotEmpty AndAlso Not .Country.Code.Equals("0") Then
                    hfCountryCodeDefault.Value = .Country.Code
                    If ddlCountry.Items.IsNotEmpty AndAlso ddlCountry.Items.FindByValue(hfCountryCodeDefault.Value).IsNotEmpty Then
                        ddlCountry.SelectedIndex = ddlCountry.Items.FindByValue(hfCountryCodeDefault.Value).Index
                    End If
                Else
                    If Me.CountryCodeShow Then
                        hfCountryCodeDefault.Value = ConfigurationManager.AppSettings("CountryCode")
                    Else
                        hfCountryCodeDefault.Value = Me.CountryCodeDefaultValue
                    End If

                End If

                If ddlTypeRoute.ClientVisible Then
                    If ddlTypeRoute.Items.Count <> 0 Then
                        ddlTypeRoute.SelectedIndex = ddlTypeRoute.Items.FindByValue(DirectCast(.TypeOfRoute, Integer).ToString).Index
                        ddlTypeRoute.ClientSideEvents.Init = String.Empty
                    End If
                Else
                    _Value.PhysicalAddress.TypeOfRoute = _TypeOfRouteDefaultValue
                End If

                'Time zone

                If ddlTimeZone.ClientVisible AndAlso ddlTimeZone.Items.Count <> 0 Then
                    If ddlTimeZone.Items.FindByValue(.TimeZone.ToString).IsNotEmpty Then
                        ddlTimeZone.SelectedIndex = ddlTimeZone.Items.FindByValue(.TimeZone.ToString).Index
                    End If
                    ddlTimeZone.ClientSideEvents.Init = String.Empty
                End If

                'Set Type of TypePhyTicalAddress
                If ddlTypePhyTicalAddress.ClientVisible AndAlso ddlTypePhyTicalAddress.Items.FindByValue(DirectCast(.TypeOfPhysicalAddress, Integer).ToString).IsNotEmpty Then
                    ddlTypePhyTicalAddress.SelectedIndex = ddlTypePhyTicalAddress.Items.FindByValue(DirectCast(.TypeOfPhysicalAddress, Integer).ToString).Index

                End If

                'set zip code
                If .ZipCode.IsNotEmpty Then
                    If ddlPostalCode.ClientVisible Then
                        If ddlPostalCode.Items.IsEmpty Then
                            ddlPostalCode.DataSource = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfZipCodeTable(hfCountryCodeDefault.Value, .ZipCode)
                            ddlPostalCode.DataBind()
                        End If
                        If ddlPostalCode.Items.FindByValue(.ZipCode).IsNotEmpty Then
                            ddlPostalCode.SelectedIndex = ddlPostalCode.Items.FindByValue(.ZipCode).Index
                        End If
                    End If
                End If

                'initial years

                If .InitialYearAtThisAddress <> 0 Then
                    If ddlInitialYear.ClientVisible Then
                        If ddlInitialYear.Items.IsEmpty Then
                            ddlInitialYear.DataSource = ConfiguionsInitialYears()
                            ddlInitialYear.DataBind()
                        End If
                        If ddlInitialYear.Items.FindByValue(.InitialYearAtThisAddress.ToString).IsNotEmpty Then
                            ddlInitialYear.SelectedIndex = ddlInitialYear.Items.FindByValue(.InitialYearAtThisAddress.ToString).Index
                        End If
                    End If
                End If

                ''last contact
                chbResidentialAddress.Checked = .CurrentLocationIndicator

                If .GeographicZones.IsNotEmpty Then
                    setGeographicZones(hfCountryCodeDefault.Value, .GeographicZones)
                Else
                    GeographicZonesCleanEventInit()
                End If

                If .PartsOfTheAddress.IsNotEmpty Then
                    setPartsOfTheAddres(hfCountryCodeDefault.Value, .PartsOfTheAddress, .TypeOfRoute)
                Else
                    PartRouteCleanEventInit()
                End If

            End With
        End If
    End Sub

    Public Sub getValuesAll()
        If _Value.IsEmpty Then
            _Value = New InMotionGIT.AddressManager.Contract.Models.AddressPhysicalAddressDLI
            With _Value
                .Address = New InMotionGIT.AddressManager.Contract.General.Address
                .PhysicalAddress = New InMotionGIT.AddressManager.Contract.General.PhysicalAddress
            End With
        Else
            If _Value.Address.IsEmpty Then
                _Value.Address = New InMotionGIT.AddressManager.Contract.General.Address
            End If
            If _Value.PhysicalAddress.IsEmpty Then
                _Value.PhysicalAddress = New InMotionGIT.AddressManager.Contract.General.PhysicalAddress
            End If
        End If

        With _Value.Address
            With _Value.Address
                .AddressHadProblemsForDelivery = chbSendProblemAddress.Checked
                .UseAsMailingAddress = chbCorrespondenceAddress.Checked
                .UseAsBillingAddress = chbCollectionAddress.Checked
                .DateOfLastSuccesfulContactAtThisAddress = ddlLastContact.Value
            End With
        End With

        With _Value.PhysicalAddress

            If Me.CountryCodeShow Then
                If ddlCountry.ClientVisible Then
                    If ddlCountry.Items.Count <> 0 AndAlso ddlCountry.SelectedItem.Value <> -1 Then
                        .Country = New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = ddlCountry.SelectedItem.Value}
                    End If
                End If

            Else
                .Country = New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = Me.CountryCodeDefaultValue}
            End If

            If ddlTypePhyTicalAddress.ClientVisible Then
                ddlTypePhyTicalAddress.Validate()

                If ddlTypePhyTicalAddress.Items.Count <> 0 AndAlso ddlTypePhyTicalAddress.SelectedItem.Value <> -1 Then
                    .TypeOfPhysicalAddress = ddlTypePhyTicalAddress.SelectedItem.Value
                End If
            Else
                .TypeOfPhysicalAddress = Me._TypeDefaultValue
            End If

            If ddlTypeRoute.ClientVisible Then
                If ddlTypeRoute.Items.Count <> 0 AndAlso ddlTypeRoute.SelectedItem.Value <> -1 Then
                    .TypeOfRoute = ddlTypeRoute.SelectedItem.Value
                End If
            Else
                .TypeOfRoute = Me._TypeOfRouteDefaultValue
            End If

            If ddlPostalCode.ClientVisible Then
                If ddlPostalCode.SelectedItem.IsNotEmpty Then
                    .ZipCode = ddlPostalCode.SelectedItem.Value
                End If
            Else
                .ZipCode = Me._PostalCodeDefaultValue
            End If

            If ddlTimeZone.ClientVisible Then
                If ddlTimeZone.SelectedItem.IsNotEmpty Then
                    .TimeZone = ddlTimeZone.SelectedItem.Value
                End If
            Else
                .TimeZone = Me._TimeZoneDefaultValue
            End If

            If ddlInitialYear.ClientVisible Then
                If ddlInitialYear.SelectedItem.IsNotEmpty AndAlso ddlInitialYear.SelectedItem.Value.ToString.IsNotEmpty Then
                    .InitialYearAtThisAddress = ddlInitialYear.SelectedItem.Value
                End If
            Else
                .InitialYearAtThisAddress = Me._InitialYearDefaultValue
            End If

            If chbResidentialAddress.ClientVisible Then
                .CurrentLocationIndicator = chbResidentialAddress.Checked
            Else
                .CurrentLocationIndicator = False
            End If

            .PartsOfTheAddress = New InMotionGIT.AddressManager.Contract.General.PartsOfTheAddressCollection

            For index = 1 To 8
                Dim prefixCombo As String = "cbxPartRoute"
                Dim prefixText As String = "txtPartRoute"
                Dim prefixTd As String = "tdPartRoute"

                Dim rowTable As HtmlTableRow = FindControlPage(prefixTd, index)
                Dim cbxValue As ASPxComboBox = FindControlPage(prefixCombo, index)
                Dim txtValue As ASPxTextBox = FindControlPage(prefixText, index)

                If txtValue.IsNotEmpty Then
                    Dim partContent As String = txtValue.Text
                    Dim PartTypeCode As String = cbxValue.ClientValue
                    Dim PartTypeDescription As String = cbxValue.Text
                    Dim PartLevel As Integer = index
                End If

                If txtValue.Text.IsNotEmpty Then
                    With .PartsOfTheAddress
                        .Add(New InMotionGIT.AddressManager.Contract.General.PartsOfTheAddress With {.PartContent = txtValue.Text,
                                                                                                     .PartType = New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = IIf(Not IsNothing(cbxValue.ClientValue), cbxValue.ClientValue, cbxValue.Value),
                                                                                                                                                                         .Description = cbxValue.Text},
                                                                                                     .PartLevel = index})
                    End With
                End If
            Next

            .GeographicZones = New InMotionGIT.AddressManager.Contract.General.GeographicZoneCollection

            For index = 1 To 8
                Dim prefixCombo As String = "cbxGeographicZone"

                Dim cbxValue As ASPxComboBox = FindControlPage(prefixCombo, index)

                If cbxValue.Visible Then
                    If cbxValue.SelectedItem.IsNotEmpty Then
                        With .GeographicZones
                            Dim temporalZone As New InMotionGIT.AddressManager.Contract.General.GeographicZone
                            With temporalZone
                                .ZoneID = New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = cbxValue.SelectedItem.Value, .Description = cbxValue.SelectedItem.Text}
                                .ZoneLevel = New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = index, .Description = index}
                            End With
                            .Add(temporalZone)
                        End With
                    End If

                End If
            Next

        End With

    End Sub

#End Region

#Region "Field and Properties"

#Region "Fields"
    Private _Value As InMotionGIT.AddressManager.Contract.Models.AddressPhysicalAddressDLI
    Private _TypeDefaultValue As InMotionGIT.AddressManager.Contract.Enumerations.EnumTypeOfPhysicalAddress
    Private _TypeOfRouteDefaultValue As InMotionGIT.AddressManager.Contract.Enumerations.EnumTypeOfRoute
    Private _CountryCodeDefaultValue As Integer
    Private _PostalCodeDefaultValue As Integer
    Private _TimeZoneDefaultValue As Integer
    Private _InitialYearDefaultValue As Integer
#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad de dirección física / Property physical address
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Address As InMotionGIT.AddressManager.Contract.Models.AddressPhysicalAddressDLI
        Get
            getValuesAll()
            Return _Value
        End Get
        Set(ByVal value As InMotionGIT.AddressManager.Contract.Models.AddressPhysicalAddressDLI)
            _Value = value
            setValuesAll()
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control Type Of Physical Address/ Muestra-oculta el control de Type Of Physical Address
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property TypeShow() As Boolean
        Get
            Return ddlTypePhyTicalAddress.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            ddlTypePhyTicalAddress.ClientVisible = value
            lblTypePhyTicalAddress.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Default value for the property TypeOfPhysicalAddress when TypeShow is true /Valor default para la propiedad de tipo TypeOfPhysicalAddress cuando TypeShow esta en True
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeDefaultValue() As InMotionGIT.AddressManager.Contract.Enumerations.EnumTypeOfPhysicalAddress
        Get
            Return _TypeDefaultValue
        End Get
        Set(ByVal value As InMotionGIT.AddressManager.Contract.Enumerations.EnumTypeOfPhysicalAddress)
            _TypeDefaultValue = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control TypeOfRoute/ Muestra-oculta el control de TypeOfRoute
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property TypeOfRouteShow() As Boolean
        Get
            Return ddlTypeRoute.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            ddlTypeRoute.ClientVisible = value
            lblTypeRoute.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Default value for the property TypeOfRoute when TypeOfRouteShow is true /Valor default para la propiedad de tipo TypeOfRoute cuando TypeOfRouteShow esta en True
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeOfRouteDefaultValue() As InMotionGIT.AddressManager.Contract.Enumerations.EnumTypeOfRoute
        Get
            Return _TypeOfRouteDefaultValue
        End Get
        Set(ByVal value As InMotionGIT.AddressManager.Contract.Enumerations.EnumTypeOfRoute)
            _TypeOfRouteDefaultValue = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control CountryCode/ Muestra-oculta el control de CountryCode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property CountryCodeShow() As Boolean
        Get
            Return ddlCountry.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            ddlCountry.ClientVisible = value
            lblCountry.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Default value for the property CountryCodeDefaultValue when CountryCodeShow is true /Valor default para la propiedad de tipo CountryCodeDefaultValue cuando CountryCodeShow esta en True
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CountryCodeDefaultValue() As Integer
        Get
            If _CountryCodeDefaultValue.IsEmpty Then
                _CountryCodeDefaultValue = ConfigurationManager.AppSettings("CountryCode")
            End If
            Return _CountryCodeDefaultValue
        End Get
        Set(ByVal value As Integer)
            _CountryCodeDefaultValue = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control PostalCode/ Muestra-oculta el control de PostalCode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property PostalCodeShow() As Boolean
        Get
            Return ddlPostalCode.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            ddlPostalCode.ClientVisible = value
            lblPostalCode.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Default value for the property PostalCodeShow when CountryCodeShow is false /Valor default para la propiedad de tipo CountryCodeDefaultValue cuando PostalCodeShow esta en false
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostalCodeDefaultValue() As Integer
        Get
            Return _PostalCodeDefaultValue
        End Get
        Set(ByVal value As Integer)
            _PostalCodeDefaultValue = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control TimeZone / Muestra-oculta el control de TimeZone
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property TimeZoneShow() As Boolean
        Get
            Return ddlTimeZone.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            ddlTimeZone.ClientVisible = value
            lblTimeZone.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    '''  Default value for the property TimeZoneShow when CountryCodeShow is false /Valor default para la propiedad de tipo TimeZoneShow cuando PostalCodeShow esta en false
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeZoneDefaultValue() As Integer
        Get
            Return _TimeZoneDefaultValue
        End Get
        Set(ByVal value As Integer)
            _TimeZoneDefaultValue = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control InitialYear / Muestra-oculta el control de InitialYear
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property InitialYearShow() As Boolean
        Get
            Return ddlInitialYear.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            ddlInitialYear.ClientVisible = value
            lblInitialYear.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Default value for the property InitialYearShow when CountryCodeShow is false /Valor default para la propiedad de tipo TimeZoneShow cuando InitialYearShow esta en false
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialYearDefaultValue() As Integer
        Get
            Return _InitialYearDefaultValue
        End Get
        Set(ByVal value As Integer)
            _InitialYearDefaultValue = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control RiskZone / Muestra-oculta el control de RiskZone
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property RiskZoneShow() As Boolean
        Get
            Return btnRiskZoneAdd.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            btnRiskZoneAdd.ClientVisible = value
            btnRiskZoneRemove.ClientVisible = value
            lblRiskZone.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control Map / Muestra-oculta el control de Map
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property MapShow() As Boolean
        Get
            Return mapa.Visible
        End Get
        Set(ByVal value As Boolean)
            mapa.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control ResidentialAddress / Muestra-oculta el control de ResidentialAddress
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property ResidentialAddressShow() As Boolean
        Get
            Return chbResidentialAddress.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            chbResidentialAddress.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control CorrespondenceAddress / Muestra-oculta el control de CorrespondenceAddress
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property CorrespondenceAddressShow() As Boolean
        Get
            Return chbCorrespondenceAddress.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            chbCorrespondenceAddress.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control CollectionAddress / Muestra-oculta el control de CollectionAddress
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property CollectionAddressShow() As Boolean
        Get
            Return chbCollectionAddress.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            chbCollectionAddress.ClientVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Show-hide control SendProblemAddress / Muestra-oculta el control de SendProblemAddress
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DefaultValue(True)>
    Public Property SendProblemAddressShow() As Boolean
        Get
            Return chbSendProblemAddress.ClientVisible
        End Get
        Set(ByVal value As Boolean)
            chbSendProblemAddress.ClientVisible = value
        End Set
    End Property

#End Region

#End Region

#Region "Events"

    Private _Visible As Boolean = True
    ''' <summary>
    '''Property that makes the controls visible or invisible/Propiedad que hace visible o invisible los controls
    ''' </summary>
    ''' <value>Property condition visible / hidden. /Estado de la propiedad visible/oculto</value>
    ''' <returns>Property condition or not available</returns>
    ''' <remarks></remarks>
    Public Overrides Property Visible As Boolean
        Get
            Return _Visible
        End Get
        Set(value As Boolean)
            SetVisible(value)
            _Visible = value
        End Set
    End Property

    ''' <summary>
    '''Allocation method embodying the controls to make visible or not /Método que realiza la asignación a los controles para hacer visible o no
    ''' </summary>
    ''' <param name="Value">State of the control</param>
    ''' <remarks></remarks>
    Sub SetVisible(Value As Boolean)

        ddlTypePhyTicalAddress.ClientVisible = Value
        lblTypePhyTicalAddress.ClientVisible = Value

        lblTypeRoute.ClientVisible = Value
        ddlTypeRoute.ClientVisible = Value

        chbResidentialAddress.ClientVisible = Value
        chbCorrespondenceAddress.ClientVisible = Value
        chbCollectionAddress.ClientVisible = Value
        chbSendProblemAddress.ClientVisible = Value

        lblCountry.ClientVisible = Value
        ddlCountry.ClientVisible = Value

        lblPostalCode.ClientVisible = Value
        ddlPostalCode.ClientVisible = Value

        lblTimeZone.ClientVisible = Value
        ddlTimeZone.ClientVisible = Value

        lblInitialYear.ClientVisible = Value
        ddlInitialYear.ClientVisible = Value

        lblLastContact.ClientVisible = Value
        ddlLastContact.ClientVisible = Value

        lblRiskZone.ClientVisible = Value

    End Sub

    Private _Enabled As Boolean = True
    ''' <summary>
    '''Property that enables or disables the controls/Propiedad que habilita o deshabilita los controls
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Enabled As Boolean
        Get
            Return _Enabled
        End Get
        Set(value As Boolean)
            SetEnable(value)
            _Enabled = value
        End Set
    End Property

    ''' <summary>
    '''Allocation method that performs the checks for available or not/Método que realiza la asignación a los controles para hacer disponible o no
    ''' </summary>
    ''' <param name="Value">State of the control</param>
    ''' <remarks></remarks>
    Sub SetEnable(Value As Boolean)

        ddlTypePhyTicalAddress.ClientEnabled = Value
        lblTypePhyTicalAddress.ClientEnabled = Value

        lblTypeRoute.ClientEnabled = Value
        ddlTypeRoute.ClientEnabled = Value

        chbResidentialAddress.ClientEnabled = Value
        chbCorrespondenceAddress.ClientEnabled = Value
        chbCollectionAddress.ClientEnabled = Value
        chbSendProblemAddress.ClientEnabled = Value

        lblCountry.ClientEnabled = Value
        ddlCountry.ClientEnabled = Value

        lblPostalCode.ClientEnabled = Value
        ddlPostalCode.ClientEnabled = Value

        lblTimeZone.ClientEnabled = Value
        ddlTimeZone.ClientEnabled = Value

        lblInitialYear.ClientEnabled = Value
        ddlInitialYear.ClientEnabled = Value

        lblLastContact.ClientEnabled = Value
        ddlLastContact.ClientEnabled = Value

        lblRiskZone.ClientEnabled = Value

        PartRouteEnable(Value)

        GeographicEnable(Value)

    End Sub

    Private Sub PartRouteEnable(state As Boolean)
        For index = 1 To 8
            Dim cbxValue As ASPxComboBox = FindControlPage("cbxPartRoute", index)
            Dim txtValue As ASPxTextBox = FindControlPage("txtPartRoute", index)
            cbxValue.ClientEnabled = state
            txtValue.ClientEnabled = state
        Next
    End Sub

    Private newPropertyValue As String
    Public Property NewProperty() As String
        Get
            Return newPropertyValue
        End Get
        Set(ByVal value As String)
            newPropertyValue = value
        End Set
    End Property

    Private Sub GeographicEnable(state As Boolean)
        For index = 1 To 8
            Dim cbxValue As ASPxComboBox = FindControlPage("cbxGeographicZone", index)
            Dim lblValue As ASPxLabel = FindControlPage("lblGeographicZone", index)
            cbxValue.ClientEnabled = state
            lblValue.ClientEnabled = state
        Next
    End Sub

    Private Sub PartRouteVisible(state As Boolean)
        For index = 1 To 8
            Dim cbxValue As ASPxComboBox = FindControlPage("cbxPartRoute", index)
            Dim txtValue As ASPxTextBox = FindControlPage("txtPartRoute", index)
            cbxValue.ClientVisible = state
            txtValue.ClientVisible = state
        Next
    End Sub

    Private Sub GeographicVisible(state As Boolean)
        For index = 1 To 8
            Dim cbxValue As ASPxComboBox = FindControlPage("cbxGeographicZone", index)
            Dim lblValue As ASPxLabel = FindControlPage("lblGeographicZone", index)
            cbxValue.ClientVisible = state
            lblValue.ClientVisible = state
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InicitialConfiguration()
        End If

    End Sub
#End Region

#Region "Methods"

    Public Function ConfiguionsInitialYears() As List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValue)
        Dim dateStart As New Date(1975, 1, 1)
        Dim dateEnd As Date = Date.Now.AddYears(-1)

        While dateStart < dateEnd
            dateStart = dateStart.AddYears(1)
            result.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = dateStart.Year, .Description = dateStart.Year})
        End While

        result = (From entry In result
                          Order By entry.Code Descending).ToList

        result.Insert(0, New InMotionGIT.Common.DataType.LookUpValue With {.Code = "", .Description = ""})
        Return result
    End Function

    Public Sub InicitialConfiguration()

        hfReload.Value = 1

        If Me.CountryCodeShow Then
            If hfCountryCodeDefault.Value.IsEmpty Or hfCountryCodeDefault.Value.Equals("0") Then
                hfCountryCodeDefault.Value = ConfigurationManager.AppSettings("CountryCode")
            End If
        Else
            hfCountryCodeDefault.Value = Me.CountryCodeDefaultValue
        End If


        ddlPostalCode.DataBind()

        Dim listValueYears As List(Of InMotionGIT.Common.DataType.LookUpValue) = ConfiguionsInitialYears()

        ddlInitialYear.DataSource = listValueYears
        ddlInitialYear.DataBind()

        'Set type of route
        ddlTypeRoute.DataSource = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfTypeOfRouteTable
        ddlTypeRoute.DataBind()

        If ddlTypeRoute.Items.Count <> 0 Then
            ddlTypeRoute.SelectedIndex = 0
        End If

        'Set type of phytical
        ddlTypePhyTicalAddress.DataSource = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfTypeOfPhysicalAddressTable()
        ddlTypePhyTicalAddress.DataBind()

        If ddlTypePhyTicalAddress.Items.Count <> 0 Then
            ddlTypePhyTicalAddress.SelectedIndex = 0
        End If

        ''Set country
        ddlCountry.DataSource = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfCountryTable()
        ddlCountry.DataBind()

        If ddlCountry.Items.Count <> 0 Then
            ddlCountry.SelectedIndex = ddlCountry.Items.FindByValue(hfCountryCodeDefault.Value).Index
        End If

        LoadZoneDinamicCountry(hfCountryCodeDefault.Value)

        If Me.TypeOfRouteShow Then
            LoadZoneDinamicTypeRoutePart(hfCountryCodeDefault.Value, ddlTypeRoute.SelectedItem.Value)

        Else
            LoadZoneDinamicTypeRoutePart(hfCountryCodeDefault.Value, Me.TypeOfRouteDefaultValue)

        End If

        'Set time zone
        ddlTimeZone.DataSource = FrontOffice.Controls.PhysicalAddress.GetLookUpPossibleValuesOfTimeZoneTable
        ddlTimeZone.DataBind()

        If ddlTimeZone.Items.Count <> 0 Then
            ddlTimeZone.SelectedIndex = 0
        End If

    End Sub
#End Region

#Region "Configurations"
    Public Sub Initialization(value As InMotionGIT.AddressManager.Contract.Models.AddressPhysicalAddressDLI)
        Me._Value = value
        If Not IsNothing(Me._Value) AndAlso IsNothing(Me._Value.PhysicalAddress) Then
            Me._Value.PhysicalAddress = New InMotionGIT.AddressManager.Contract.General.PhysicalAddress
        End If
    End Sub
#End Region

    Protected Sub ASPxCallbackPanel2_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles ASPxCallbackPanel2.Callback

        lp.ShowImage = True
        Dim countryCode As Integer = 0
        Dim nameControl As String = String.Empty
        Dim vec As String() = e.Parameter.Split(",")
        countryCode = vec(0)
        LoadZoneDinamicCountry(countryCode)
        lp.ShowImage = True
    End Sub

    Private Sub PartRouteCleanEventInit()
        For index = 1 To 8
            Dim cbxValue As ASPxComboBox = FindControlPage("cbxPartRoute", index)
            With cbxValue.ClientSideEvents
                .Init = String.Empty
            End With
        Next
    End Sub

    Private Sub GeographicZonesCleanEventInit()
        For index = 1 To 8
            Dim cbxValue As ASPxComboBox = FindControlPage("cbxGeographicZone", index)
            With cbxValue.ClientSideEvents
                .Init = String.Empty
            End With
        Next
    End Sub

    Public Function FindControlPage(prefix As String, counter As Integer) As Control
        Dim result As Control = Nothing
        If prefix.Contains("PartRoute") Then
            If prefix.Equals("cbxPartRoute") Then
                Select Case counter
                    Case 1
                        result = cbxPartRoute1
                    Case 2
                        result = cbxPartRoute2
                    Case 3
                        result = cbxPartRoute3
                    Case 4
                        result = cbxPartRoute4
                    Case 5
                        result = cbxPartRoute5
                    Case 6
                        result = cbxPartRoute6
                    Case 7
                        result = cbxPartRoute7
                    Case 8
                        result = cbxPartRoute8
                    Case Else

                End Select
            ElseIf prefix.Equals("tdPartRoute") Then
                Select Case counter
                    Case 1
                        result = tdPartRoute1
                    Case 2
                        result = tdPartRoute2
                    Case 3
                        result = tdPartRoute3
                    Case 4
                        result = tdPartRoute4
                    Case 5
                        result = tdPartRoute5
                    Case 6
                        result = tdPartRoute6
                    Case 7
                        result = tdPartRoute7
                    Case 8
                        result = tdPartRoute8
                    Case Else

                End Select
            Else
                Select Case counter
                    Case 1
                        result = txtPartRoute1
                    Case 2
                        result = txtPartRoute2
                    Case 3
                        result = txtPartRoute3
                    Case 4
                        result = txtPartRoute4
                    Case 5
                        result = txtPartRoute5
                    Case 6
                        result = txtPartRoute6
                    Case 7
                        result = txtPartRoute7
                    Case 8
                        result = txtPartRoute8
                    Case Else

                End Select
            End If
        Else
            If prefix.Equals("cbxGeographicZone") Then
                Select Case counter
                    Case 1
                        result = cbxGeographicZone1
                    Case 2
                        result = cbxGeographicZone2
                    Case 3
                        result = cbxGeographicZone3
                    Case 4
                        result = cbxGeographicZone4
                    Case 5
                        result = cbxGeographicZone5
                    Case 6
                        result = cbxGeographicZone6
                    Case 7
                        result = cbxGeographicZone7
                    Case 8
                        result = cbxGeographicZone8
                    Case Else

                End Select
            ElseIf prefix.Equals("tdGeographicZone") Then
                Select Case counter
                    Case 1
                        result = tdGeographicZone1
                    Case 2
                        result = tdGeographicZone2
                    Case 3
                        result = tdGeographicZone3
                    Case 4
                        result = tdGeographicZone4
                    Case 5
                        result = tdGeographicZone5
                    Case 6
                        result = tdGeographicZone6
                    Case 7
                        result = tdGeographicZone7
                    Case 8
                        result = tdGeographicZone8
                    Case Else

                End Select
            Else
                Select Case counter
                    Case 1
                        result = lblGeographicZone1
                    Case 2
                        result = lblGeographicZone2
                    Case 3
                        result = lblGeographicZone3
                    Case 4
                        result = lblGeographicZone4
                    Case 5
                        result = lblGeographicZone5
                    Case 6
                        result = lblGeographicZone6
                    Case 7
                        result = lblGeographicZone7
                    Case 8
                        result = lblGeographicZone8
                    Case Else

                End Select
            End If
        End If
        Return result
    End Function

    Public Sub LoadZoneDinamicTypeRoutePart(countryCode As Integer, TypePhyTicalAddressCode As Integer)

        Dim ListLookUpPossibleValuesOfNamesOfPartsOfAddressTable = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfNamesOfPartsOfAddressTable(countryCode, TypePhyTicalAddressCode)
        ListLookUpPossibleValuesOfNamesOfPartsOfAddressTable = (From a In ListLookUpPossibleValuesOfNamesOfPartsOfAddressTable Order By a.Code Ascending Select a).ToList

        Dim groups = ListLookUpPossibleValuesOfNamesOfPartsOfAddressTable.GroupBy(Function(x) x.ParentCode).OrderBy(Function(c) c.Key)

        If ListLookUpPossibleValuesOfNamesOfPartsOfAddressTable.Count = 0 Then
            PanelTypeRoutePart.Width = New Unit("0px")
            PanelTypeRoutePart.Visible = False
        End If

        Dim counter As Integer = 0

        For Each itemGroup In groups
            Dim counterInternal As Integer = counter + 1
            Dim prefixCombo As String = "cbxPartRoute"
            Dim prefixText As String = "txtPartRoute"
            Dim prefixTd As String = "tdPartRoute"
            Dim defaultValue As String = String.Empty
            Dim defaultIndex As Integer = -1

            Dim rowTable As HtmlTableRow = FindControlPage(prefixTd, counterInternal)
            Dim cbxValue As ASPxComboBox = FindControlPage(prefixCombo, counterInternal)
            Dim txtValue As ASPxTextBox = FindControlPage(prefixText, counterInternal)

            If Not IsNothing(_Value) AndAlso Not IsNothing(_Value.PhysicalAddress) AndAlso Not IsNothing(_Value.PhysicalAddress.PartsOfTheAddress) AndAlso _Value.PhysicalAddress.PartsOfTheAddress.Count <> 0 Then
                Dim temporal As InMotionGIT.AddressManager.Contract.General.PartsOfTheAddress = SearchItem(_Value.PhysicalAddress.PartsOfTheAddress, counterInternal)
                If Not IsNothing(temporal) Then
                    defaultValue = temporal.PartContent
                    defaultIndex = temporal.PartType.Code - 1
                End If
            End If

            rowTable.Visible = True
            cbxValue.Visible = True
            txtValue.Visible = True

            Dim dataSource As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
            dataSource.Add(New InMotionGIT.Common.DataType.LookUpValueExtend With {.Code = -1, .Description = ""})
            For Each inneritem In itemGroup
                dataSource.Add(inneritem)
            Next

            cbxValue.DataSource = dataSource
            cbxValue.DataBind()

            If defaultValue.IsNotEmpty AndAlso defaultIndex <> -1 Then
                cbxValue.SelectedIndex = defaultIndex
                txtValue.Text = defaultValue
            Else
                cbxValue.SelectedIndex = 0
                txtValue.Text = String.Empty
            End If
            counter = counter + 1
        Next

    End Sub

    Public Sub LoadZoneDinamicCountry(countryCode As String)
        Dim listLookUpPosibleValuesOfGeographicTableByLevel = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfGeographicZoneTableByLevel(countryCode)

        Dim resultLookUpPossibleValuesOfGeographicZoneNamesTable As List(Of InMotionGIT.Common.DataType.LookUpValueExtend) = GetListLookUpPossibleValuesOfGeographicZoneNamesTable(countryCode)

        Dim table As New Table
        If resultLookUpPossibleValuesOfGeographicZoneNamesTable.Count = 0 Then
            panelCountry.Width = New Unit("0px")
            panelCountry.Visible = False
        End If

        Dim counterInternal As Integer = 0
        For Each item In resultLookUpPossibleValuesOfGeographicZoneNamesTable

            counterInternal = counterInternal + 1
            Dim prefixCombo As String = "cbxGeographicZone"
            Dim prefixLabel As String = "lblGeographicZone"
            Dim prefixTd As String = "tdGeographicZone"
            Dim defaultValue As String = String.Empty
            Dim defaultIndex As Integer = -1

            Dim rowTable As HtmlTableRow = FindControlPage(prefixTd, counterInternal)
            Dim cbxValue As ASPxComboBox = FindControlPage(prefixCombo, counterInternal)
            Dim txtValue As ASPxLabel = FindControlPage(prefixLabel, counterInternal)

            rowTable.Visible = True
            cbxValue.Visible = True
            txtValue.Visible = True

            txtValue.Text = item.Description

            If counterInternal = 1 Then
                cbxValue.DataSource = listLookUpPosibleValuesOfGeographicTableByLevel
                If listLookUpPosibleValuesOfGeographicTableByLevel.Count <> 0 Then
                    cbxValue.SelectedIndex = 0
                End If
                cbxValue.DataBind()
            End If

            Dim scriptSelectedIndexChange As String = String.Empty

            If counterInternal <> resultLookUpPossibleValuesOfGeographicZoneNamesTable.Count Then
                scriptSelectedIndexChange = "function(s, e) {" & _
                                          String.Format("            GeographicZone_SelectedIndexChanged(s,e, {0}, {1})", String.Format(prefixCombo + "{0}", counterInternal + 1), counterInternal) &
                                                        "}"
            End If

            With cbxValue.ClientSideEvents
                .SelectedIndexChanged = scriptSelectedIndexChange
            End With

        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="countryCode"></param>
    ''' <param name="partsOfTheAddressCollection"></param>
    ''' <param name="enumTypeOfRoute"></param>
    ''' <remarks></remarks>
    Private Sub setPartsOfTheAddres(countryCode As String, partsOfTheAddressCollection As General.PartsOfTheAddressCollection, enumTypeOfRoute As Enumerations.EnumTypeOfRoute)
        Dim counter As Integer = 1

        LoadZoneDinamicTypeRoutePart(countryCode, DirectCast(enumTypeOfRoute, Integer))

        For Each item As General.PartsOfTheAddress In partsOfTheAddressCollection
            Dim prefixCombo As String = "cbxPartRoute"
            Dim prefixText As String = "txtPartRoute"
            Dim prefixTd As String = "tdPartRoute"

            Dim rowTable As HtmlTableRow = FindControlPage(prefixTd, counter)
            Dim cbxValue As ASPxComboBox = FindControlPage(prefixCombo, counter)
            Dim txtValue As ASPxTextBox = FindControlPage(prefixText, counter)

            If cbxValue.Items.Count <> 0 Then
                If cbxValue.Items.FindByValue(item.PartType.Code).IsNotEmpty Then
                    cbxValue.SelectedIndex = cbxValue.Items.FindByValue(item.PartType.Code).Index
                End If
            End If

            rowTable.Visible = True
            cbxValue.Visible = True
            txtValue.Visible = True

            txtValue.Text = item.PartContent

            counter = counter + 1
        Next
    End Sub

    ''' <summary>
    ''' Set Part of the Geographic Zones
    ''' </summary>
    ''' <param name="countryCode"></param>
    ''' <param name="geographicZoneCollection"></param>
    ''' <remarks></remarks>
    Private Sub setGeographicZones(countryCode As String, geographicZoneCollection As General.GeographicZoneCollection)
        Dim sourceFound = (From entry In geographicZoneCollection
                           Order By entry.ZoneLevel.Code Ascending).ToList

        LoadZoneDinamicCountry(countryCode)

        Dim counterInternal As Integer = 1

        For Each item In sourceFound

            Dim prefixCombo As String = "cbxGeographicZone"
            Dim prefixText As String = "lblGeographicZone"
            Dim prefixTd As String = "tdGeographicZone"
            Dim defaultParent As String = String.Empty
            Dim defaultChildren As String = String.Empty
            Dim scriptInit As String = String.Empty
            Dim rowTable As HtmlTableRow = FindControlPage(prefixTd, counterInternal)
            Dim cbxValue As ASPxComboBox = FindControlPage(prefixCombo, counterInternal)
            Dim txtValue As ASPxLabel = FindControlPage(prefixText, counterInternal)

            rowTable.Visible = True
            cbxValue.Visible = True
            txtValue.Visible = True

            defaultChildren = item.ZoneID.Code

            If sourceFound.FirstOrDefault.Equals(item) Then
                Dim listFirst = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfGeographicZoneTableByLevel(countryCode)
                cbxValue.DataSource = listFirst
                cbxValue.DataBind()
                If cbxValue.Items.FindByValue(defaultChildren).IsNotEmpty Then
                    cbxValue.SelectedIndex = cbxValue.Items.FindByValue(defaultChildren).Index
                End If
            Else
                defaultChildren = sourceFound.Item(counterInternal - 1).ZoneID.Code
                defaultParent = sourceFound.Item(counterInternal - 2).ZoneID.Code
                scriptInit = "function(s, e) {" & _
                                      String.Format("            Init_GeographicZoneNew(s,e, {0}, {1}, {2})", defaultParent, defaultChildren, counterInternal - 1) &
                                                             "}"

                With cbxValue.ClientSideEvents
                    .Init = scriptInit
                End With

            End If

            counterInternal = counterInternal + 1

        Next

    End Sub

    Protected Sub CallbackGeneric_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase)

    End Sub

    Protected Sub ddlTimeZone_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles ddlTimeZone.Callback

    End Sub

    Protected Sub CallbackPanelTypeRoutePart_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles CallbackPanelTypeRoutePart.Callback
        Dim countryCode As Integer = 0
        Dim TypePhyTicalAddressCode As Integer
        Dim vec As String() = e.Parameter.Split(",")
        countryCode = vec(0)
        TypePhyTicalAddressCode = vec(1)

        LoadZoneDinamicTypeRoutePart(countryCode, TypePhyTicalAddressCode)
    End Sub

    Public Function GetListLookUpPossibleValuesOfGeographicZoneNamesTable(countryCode As String) As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        Dim result As New List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
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
                .Country = ConfigurationManager.AppSettings("CountryCode")
                .Security = security
                .Site = "VisualTIME"
                .UserInitials = "VTNET"
                .UserPassword = "12345"
                .Version = InMotionGIT.Common.Enumerations.EnumApplicationVersion.USALife
            End With

            result = clien.LookUpPossibleValuesOfGeographicZoneNamesTable(country, consumerInformation)
        End Using
        Return result
    End Function

    Protected Sub ddlPostalCode_OnItemsRequestedByFilterCondition(source As Object, e As ListEditItemsRequestedByFilterConditionEventArgs)
        Dim filter As String = e.Filter.Trim.ToUpper
        ddlPostalCode.DataSource = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfZipCodeTable(hfCountryCodeDefault.Value, filter)
        ddlPostalCode.DataBind()
    End Sub
    Protected Sub ddlPostalCode_OnItemRequestedByValue(source As Object, e As ListEditItemRequestedByValueEventArgs)
        If String.IsNullOrEmpty(e.Value) Then
            Return
        End If
        ddlPostalCode.DataSource = FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfZipCodeTable(hfCountryCodeDefault.Value, e.Value.ToString())
        ddlPostalCode.DataBind()
    End Sub

#Region "Client"

    Public Function ClientCommon() As InMotionGIT.CommonService.Proxy.CommonService.PublicClient
        Dim result As New InMotionGIT.CommonService.Proxy.CommonService.PublicClient("BasicHttpBinding_CommonService")
        Return result
    End Function

    Public Shared Function ClientAddress() As InMotionGIT.AddressManager.Proxy.AddressManager.PublicClient
        Dim result As New InMotionGIT.AddressManager.Proxy.AddressManager.PublicClient("BasicHttpBinding_IPublic")
        Return result
    End Function

#End Region

    Protected Sub btnRiskZoneAdd_Click(sender As Object, e As System.EventArgs) Handles btnRiskZoneAdd.Click

    End Sub

    Private Function SearchItem(partsOfTheAddressCollection As PartsOfTheAddressCollection, counterInternal As Integer) As PartsOfTheAddress
        Dim result As PartsOfTheAddress = Nothing
        For Each item In partsOfTheAddressCollection
            If item.PartLevel = counterInternal Then
                result = item
                Exit For
            End If
        Next
        Return result
    End Function

End Class