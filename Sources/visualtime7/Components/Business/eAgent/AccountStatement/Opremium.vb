<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class opremium
    
    Private billItemField As list (of billItem)
    
    Private idField As integer
    
    Private dateField As String
    
    Private nameField As String
    
    Private categoryField As String
    
    Private productField As String
    
    Private commisionField As Decimal
    
    Private clientIdField As String
    
    Private numberField As integer
    
    Private dateBeginField As String
    
    Private dateEndField As String
    
    Private insuredCapitalField As Decimal
    
    Private detailsField As String
    
    Private billIdField As Integer
    
    Private issueDateField As String
    
    Private paymentDateField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("billItem")>  _
    Public Property billItem() As List (Of billItem)
        Get
            Return Me.billItemField
        End Get
        Set
            Me.billItemField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property id() As string
        Get
            Return Me.idField.ToString
        End Get
        Set
            Me.idField = CInt(Value)
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property [date]() As String
        Get
            Return Me.dateField
        End Get
        Set
            Me.dateField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set
            Me.nameField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property category() As String
        Get
            Return Me.categoryField
        End Get
        Set
            Me.categoryField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property product() As String
        Get
            Return Me.productField
        End Get
        Set
            Me.productField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property commision() As Decimal
        Get
            Return Me.commisionField
        End Get
        Set
            Me.commisionField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property clientId() As String
        Get
            Return Me.clientIdField
        End Get
        Set
            Me.clientIdField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property number() As string
        Get
            Return Me.numberField.ToString
        End Get
        Set
            Me.numberField = CInt(Value)
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property dateBegin() As String
        Get
            Return Me.dateBeginField
        End Get
        Set
            Me.dateBeginField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property dateEnd() As String
        Get
            Return Me.dateEndField
        End Get
        Set
            Me.dateEndField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property insuredCapital() As Decimal
        Get
            Return Me.insuredCapitalField
        End Get
        Set
            Me.insuredCapitalField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property details() As String
        Get
            Return Me.detailsField
        End Get
        Set
            Me.detailsField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property billId() As string
        Get
            Return Me.billIdField.ToString
        End Get
        Set
            Me.billIdField = CInt(Value)
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property issueDate() As String
        Get
            Return Me.issueDateField
        End Get
        Set
            Me.issueDateField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property paymentDate() As String
        Get
            Return Me.paymentDateField
        End Get
        Set
            Me.paymentDateField = value
        End Set
    End Property
End Class
