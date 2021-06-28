<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class Clients
    
    Private clientField As List (Of client)
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("client")>  _
    Public Property client() As List (Of client)
        Get
            Return Me.clientField
        End Get
        Set
            Me.clientField = value
        End Set
    End Property
End Class
