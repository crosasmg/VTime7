<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class LookUp
    
    Private parentIdField As String
    
    Private codeField As String
    
    Private descriptionField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property ParentId() As String
        Get
            Return Me.parentIdField
        End Get
        Set
            Me.parentIdField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property Code() As String
        Get
            Return Me.codeField
        End Get
        Set
            Me.codeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property Description() As String
        Get
            Return Me.descriptionField
        End Get
        Set
            Me.descriptionField = value
        End Set
    End Property
End Class
