<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class bonusPoints
    
    Private typeField As Integer
    
    Private valueField As Integer
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property type() As Integer
        Get
            Return Me.typeField
        End Get
        Set
            Me.typeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()>  _
    Public Property Value() As Integer
        Get
            Return Me.valueField
        End Get
        Set
            Me.valueField = value
        End Set
    End Property
End Class
