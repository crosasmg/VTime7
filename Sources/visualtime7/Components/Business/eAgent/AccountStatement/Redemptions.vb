<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class redemptions
    
    Private redemptionField As redemption
    
    Private valueTotalField As Integer
    
    '''<remarks/>
    Public Property redemption() As redemption
        Get
            Return Me.redemptionField
        End Get
        Set
            Me.redemptionField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property valueTotal() As Integer
        Get
            Return Me.valueTotalField
        End Get
        Set
            Me.valueTotalField = value
        End Set
    End Property
End Class
