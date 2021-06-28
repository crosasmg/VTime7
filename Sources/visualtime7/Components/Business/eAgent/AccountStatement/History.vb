<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class history
    
    Private rewardField As List (Of reward)
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("reward")>  _
    Public Property reward() As List (Of reward)
        Get
            Return Me.rewardField
        End Get
        Set
            Me.rewardField = value
        End Set
    End Property
End Class
