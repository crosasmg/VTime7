Option Strict Off
Option Explicit On

<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class root
    
    Private itemField As List (Of item)

    Private dataListField As dataList

    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("item")>  _
    Public Property item() As List (Of item)
        Get
            Return Me.itemField
        End Get
        Set
            Me.itemField = value
        End Set
    End Property

    '''<remarks/>
    Public Property dataList() As dataList
        Get
            Return Me.dataListField
        End Get
        Set
            Me.dataListField = value
        End Set
    End Property

End Class
