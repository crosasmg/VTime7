<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class rewardInfo
    
    Private contestField As String 
    Private positionField As String 
    Private ticketsField As String 
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property contest() As String 
        Get
            Return Me.contestField
        End Get
        Set
            Me.contestField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property position() As String 
        Get
            Return Me.positionField
        End Get
        Set
            Me.positionField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property tickets() As String 
        Get
            Return Me.ticketsField
        End Get
        Set
            Me.ticketsField = value
        End Set
    End Property
End Class
