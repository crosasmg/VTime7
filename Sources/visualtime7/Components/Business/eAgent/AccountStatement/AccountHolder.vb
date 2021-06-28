<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class accountHolder
    
    Private nameField As String
    
    Private clientIdField As String
    
    Private keyToAddressRecordField As String

    Private RecordOwnerField As Integer 
    
    Private addressField As String
    
    Private addressFirstLineField As String
    
    Private addressSecondLineField As String
    
    Private populationField As String
    
    Private municipalityField As String
    
    Private cityField As String
    
    Private stateField As String
    
    Private departmentNumberField As String
    
    Private buildField As String
    
    Private countryField As String
    
    Private zipField As String
    
    Private emailField As String
    
    Private homeField As String
    
    Private mobileField As String
    
    Private workField As String
    
    Private phonesField As List (Of phone)
    
    '''<remarks/>
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set
            Me.nameField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property clientId() As String
        Get
            Return Me.clientIdField
        End Get
        Set
            Me.clientIdField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property KeyToAddressRecord() As String
        Get
            Return Me.keyToAddressRecordField
        End Get
        Set
            Me.keyToAddressRecordField = value
        End Set
    End Property

    

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property RecordOwner() As String
        Get
            Return Me.RecordOwnerField.ToString
        End Get
        Set
            Me.RecordOwnerField = CInt(Value)
        End Set
    End Property

    
    '''<remarks/>
    Public Property address() As String
        Get
            Return Me.addressField
        End Get
        Set
            Me.addressField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property AddressFirstLine() As String
        Get
            Return Me.addressFirstLineField
        End Get
        Set
            Me.addressFirstLineField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property AddressSecondLine() As String
        Get
            Return Me.addressSecondLineField
        End Get
        Set
            Me.addressSecondLineField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property population() As String
        Get
            Return Me.populationField
        End Get
        Set
            Me.populationField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property municipality() As String
        Get
            Return Me.municipalityField
        End Get
        Set
            Me.municipalityField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property city() As String
        Get
            Return Me.cityField
        End Get
        Set
            Me.cityField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property state() As String
        Get
            Return Me.stateField
        End Get
        Set
            Me.stateField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute()>  _
    Public Property departmentNumber() As String
        Get
            Return Me.departmentNumberField
        End Get
        Set
            Me.departmentNumberField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute()>  _
    Public Property build() As String
        Get
            Return Me.buildField
        End Get
        Set
            Me.buildField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property country() As String
        Get
            Return Me.countryField
        End Get
        Set
            Me.countryField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property zip() As String
        Get
            Return Me.zipField
        End Get
        Set
            Me.zipField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property email() As String
        Get
            Return Me.emailField
        End Get
        Set
            Me.emailField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property home() As String
        Get
            Return Me.homeField
        End Get
        Set
            Me.homeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property mobile() As String
        Get
            Return Me.mobileField
        End Get
        Set
            Me.mobileField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>  _
    Public Property work() As String
        Get
            Return Me.workField
        End Get
        Set
            Me.workField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("phone", IsNullable:=false)>  _
    Public Property phones() As List (of phone)
        Get
            Return Me.phonesField
        End Get
        Set
            Me.phonesField = value
        End Set
    End Property
End Class
