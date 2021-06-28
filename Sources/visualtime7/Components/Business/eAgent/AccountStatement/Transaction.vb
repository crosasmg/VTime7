Imports System.Xml.Serialization

<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class transaction

    Private billItemField As List(Of billItem)

    Private idField As Integer

    Private dateField As String

    Private nameField As String

    Private categoryField As String

    Private lineofbusinessField As Integer

    Private productField As String

    Private amountField As Decimal

    Private clientIdField As String

    Private numberField As Integer

    Private dateBeginField As String

    Private dateEndField As String

    Private insuredCapitalField As Decimal

    Private detailsField As String

    Private billIdField As Integer

    Private issueDateField As String

    Private paymentDateField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("billItem")> _
    Public Property billItem() As List(Of billItem)
        Get
            Return Me.billItemField
        End Get
        Set(value As List(Of billItem))
            Me.billItemField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property id() As Integer
        Get
            Return Me.idField
        End Get
        Set(value As Integer)
            Me.idField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property [date]() As String
        Get
            Return Me.dateField
        End Get
        Set(value As String)
            Me.dateField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property category() As String
        Get
            Return Me.categoryField
        End Get
        Set(value As String)
            Me.categoryField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property lineofbusiness() As Integer
        Get
            Return Me.lineofbusinessField
        End Get
        Set(value As Integer)
            Me.lineofbusinessField = value
        End Set
    End Property


    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property product() As String
        Get
            Return Me.productField
        End Get
        Set(value As String)
            Me.productField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property amount() As Decimal
        Get
            Return Me.amountField
        End Get
        Set(value As Decimal)
            Me.amountField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property clientId() As String
        Get
            Return Me.clientIdField
        End Get
        Set(value As String)
            Me.clientIdField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")> _
    Public Property number() As String
        Get
            Return Me.numberField.ToString
        End Get
        Set(value As String)
            Me.numberField = CInt(value)
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property dateBegin() As String
        Get
            Return Me.dateBeginField
        End Get
        Set(value As String)
            Me.dateBeginField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property dateEnd() As String
        Get
            Return Me.dateEndField
        End Get
        Set(value As String)
            Me.dateEndField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property insuredCapital() As Decimal
        Get
            Return Me.insuredCapitalField
        End Get
        Set(value As Decimal)
            Me.insuredCapitalField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property details() As String
        Get
            Return Me.detailsField
        End Get
        Set(value As String)
            Me.detailsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")> _
    Public Property billId() As String
        Get
            Return Me.billIdField.ToString
        End Get
        Set(value As String)
            Me.billIdField = CInt(value)
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property issueDate() As String
        Get
            Return Me.issueDateField
        End Get
        Set(value As String)
            Me.issueDateField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property paymentDate() As String
        Get
            Return Me.paymentDateField
        End Get
        Set(value As String)
            Me.paymentDateField = value
        End Set
    End Property

    <XmlAttribute()>
    Public Property share As Decimal

    <XmlAttribute()>
    Public Property description As String

    <XmlAttribute()>
    Public Property endingdate As String

    <XmlAttribute()>
    Public Property Phone As String

    <XmlAttribute()>
    Public Property completeaddress As String
End Class
