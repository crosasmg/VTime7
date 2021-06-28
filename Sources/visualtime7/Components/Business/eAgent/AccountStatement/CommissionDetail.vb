<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class CommissionDetail
    
    Private codeofProducerField As String
    
    Private hierarchylevelField As System.Nullable(Of Short)
    
    Private hierarchylevelFieldSpecified As Boolean
    
    Private typeField As String
    
    Private commissionPercentageField As System.Nullable(Of Decimal)
    
    Private commissionPercentageFieldSpecified As Boolean
    
    Private commissionField As Decimal
    
    '''<remarks/>
    Public Property CodeofProducer() As String
        Get
            Return Me.codeofProducerField
        End Get
        Set
            Me.codeofProducerField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(IsNullable:=true)>  _
    Public Property Hierarchylevel() As System.Nullable(Of Short)
        Get
            Return Me.hierarchylevelField
        End Get
        Set
            Me.hierarchylevelField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()>  _
    Public Property HierarchylevelSpecified() As Boolean
        Get
            Return Me.hierarchylevelFieldSpecified
        End Get
        Set
            Me.hierarchylevelFieldSpecified = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(IsNullable:=true)>  _
    Public Property Type() As String
        Get
            Return Me.typeField
        End Get
        Set
            Me.typeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(IsNullable:=true)>  _
    Public Property CommissionPercentage() As System.Nullable(Of Decimal)
        Get
            Return Me.commissionPercentageField
        End Get
        Set
            Me.commissionPercentageField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()>  _
    Public Property CommissionPercentageSpecified() As Boolean
        Get
            Return Me.commissionPercentageFieldSpecified
        End Get
        Set
            Me.commissionPercentageFieldSpecified = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property Commission() As Decimal
        Get
            Return Me.commissionField
        End Get
        Set
            Me.commissionField = value
        End Set
    End Property
End Class
