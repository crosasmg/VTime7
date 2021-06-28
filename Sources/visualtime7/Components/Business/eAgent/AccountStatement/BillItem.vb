Imports System.Xml.Serialization

Public Class billItem

    Private billingItemField As String

    Private DescriptionField As String

    Private typeofDetailRecordField As String

    Private detailItemCodeField As String

    Private premiumAmountFieldSpecified As Boolean

    Private commisionAmountFieldSpecified As Boolean

    Private commissionDetailField As List(Of CommissionDetail)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(IsNullable:=True)> _
    Public Property BillingItem() As String
        Get
            Return Me.billingItemField
        End Get
        Set(value As String)
            Me.billingItemField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(IsNullable:=True)> _
    Public Property Description() As String
        Get
            Return Me.DescriptionField
        End Get
        Set(value As String)
            Me.DescriptionField = value
        End Set
    End Property

    '''<remarks/>
    Public Property TypeofDetailRecord() As String
        Get
            Return Me.typeofDetailRecordField
        End Get
        Set(value As String)
            Me.typeofDetailRecordField = value
        End Set
    End Property

    '''<remarks/>
    Public Property DetailItemCode() As String
        Get
            Return Me.detailItemCodeField
        End Get
        Set(value As String)
            Me.detailItemCodeField = value
        End Set
    End Property



    ' '''<remarks/>
    '<System.Xml.Serialization.XmlIgnoreAttribute()> _
    'Public Property PremiumAmountSpecified() As Boolean
    '    Get
    '        Return Me.premiumAmountFieldSpecified
    '    End Get
    '    Set(value As Boolean)
    '        Me.premiumAmountFieldSpecified = value
    '    End Set
    'End Property



    ' '''<remarks/>
    '<System.Xml.Serialization.XmlIgnoreAttribute()> _
    'Public Property CommisionAmountSpecified() As Boolean
    '    Get
    '        Return Me.commisionAmountFieldSpecified
    '    End Get
    '    Set(value As Boolean)
    '        Me.commisionAmountFieldSpecified = value
    '    End Set
    'End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("CommissionDetail")> _
    Public Property CommissionDetail() As List(Of CommissionDetail)
        Get
            Return Me.commissionDetailField
        End Get
        Set(value As List(Of CommissionDetail))
            Me.commissionDetailField = value
        End Set
    End Property

    '''<remarks/>
    <XmlElement()>
    Public Property CommissionPercentage() As Decimal

    '''<remarks/>
    <XmlElement()>
    Public Property CommisionAmount() As Decimal

    '''<remarks/>
    <XmlElement()>
    Public Property PremiumAmount() As Decimal


End Class
