<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class dataList
    
    Private provinceListField As List(Of LookUp)
    
    Private municipalityListField As List(Of LookUp)
    
    Private countryListField As List(Of LookUp)
    
    Private telephoneTypeListField As List(Of LookUp)
    
    Private lineOfBusinessListField As List(Of LookUpLineOfBusiness)
    
    Private productListField As List(Of LookUpProduct)
    
    Private vehiclesListField As List(Of LookUpAlphaNumeric)
    
    Private paymentFrequencyListField As List(Of LookUp)
    
    Private sexListField As List(Of LookUpAlphaNumeric)

    Private CurrencyField As List(Of LookUp)

    <System.Xml.Serialization.XmlArrayItemAttribute("LookUp", IsNullable:=False)>
    Public Property CityList As List(Of LookUp)


    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUp", IsNullable:=false)>  _
    Public Property ProvinceList() As List(Of LookUp)
        Get
            Return Me.provinceListField
        End Get
        Set
            Me.provinceListField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUp", IsNullable:=false)>  _
    Public Property MunicipalityList() As List(Of LookUp)
        Get
            Return Me.municipalityListField
        End Get
        Set
            Me.municipalityListField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUp", IsNullable:=false)>  _
    Public Property CountryList() As List(Of LookUp)
        Get
            Return Me.countryListField
        End Get
        Set
            Me.countryListField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUp", IsNullable:=false)>  _
    Public Property TelephoneTypeList() As List(Of LookUp)
        Get
            Return Me.telephoneTypeListField
        End Get
        Set
            Me.telephoneTypeListField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUp", IsNullable:=False)> _
    Public Property LineOfBusinessList() As List(Of LookUpLineOfBusiness)
        Get
            Return Me.lineOfBusinessListField
        End Get
        Set(value As List(Of LookUpLineOfBusiness))
            Me.lineOfBusinessListField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUpProduct", IsNullable:=False)> _
    Public Property ProductList() As List(Of LookUpProduct)
        Get
            Return Me.productListField
        End Get
        Set(value As List(Of LookUpProduct))
            Me.productListField = Value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUpAlphaNumeric", IsNullable:=false)>  _
    Public Property VehiclesList() As List(Of LookUpAlphaNumeric)
        Get
            Return Me.vehiclesListField
        End Get
        Set
            Me.vehiclesListField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUp", IsNullable:=false)>  _
    Public Property PaymentFrequencyList() As List(Of LookUp)
        Get
            Return Me.paymentFrequencyListField
        End Get
        Set
            Me.paymentFrequencyListField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUpAlphaNumeric", IsNullable:=false)>  _
    Public Property SexList() As List(Of LookUpAlphaNumeric)
        Get
            Return Me.sexListField
        End Get
        Set
            Me.sexListField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("LookUp", IsNullable:=False)> _
    Public Property Currency() As List(Of LookUp)
        Get
            Return Me.CurrencyField
        End Get
        Set(value As List(Of LookUp))
            Me.CurrencyField = value
        End Set
    End Property

End Class
