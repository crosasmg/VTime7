<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class reward
    
    Private currentTotalField As String
    
    Private monthField As String
    
    Private previousTotalField As Integer
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property currentTotal() As String
        Get
            Return Me.currentTotalField
        End Get
        Set
            Me.currentTotalField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>  _
    Public Property month() As String
        Get
            Return Me.monthField
        End Get
        Set
            Me.monthField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property previousTotal() As Integer
        Get
            Return Me.previousTotalField
        End Get
        Set
            Me.previousTotalField = value
        End Set
    End Property
End Class
