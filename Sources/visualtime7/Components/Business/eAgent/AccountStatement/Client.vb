<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class client
    
    Private idField As String
    
    Private nameField As String
    
    Private adressField As String
    
    Private telephoneField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property id() As String
        Get
            Return Me.idField
        End Get
        Set
            Me.idField = value
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
    Public Property adress() As String
        Get
            Return Me.adressField
        End Get
        Set
            Me.adressField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property telephone() As String
        Get
            Return Me.telephoneField
        End Get
        Set
            Me.telephoneField = value
        End Set
    End Property
End Class
