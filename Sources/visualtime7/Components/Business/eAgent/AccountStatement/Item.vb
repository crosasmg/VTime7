Option Strict Off
Option Explicit On

<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class item
    
    Private dataField As data
    
    Private fileNameField As String
    
    Private languageField As String

    Private consumerInformationField As ConsumerInformation 
    
    '''<remarks/>
    Public Property data() As data
        Get
            Return Me.dataField
        End Get
        Set
            Me.dataField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property fileName() As String
        Get
            Return Me.fileNameField
        End Get
        Set
            Me.fileNameField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property language() As String
        Get
            Return Me.languageField
        End Get
        Set
            Me.languageField = value
        End Set
    End Property

    '''<remarks/>
    Public Property consumerInformation() As consumerInformation
        Get
            Return Me.consumerInformationField
        End Get
        Set
            Me.consumerInformationField = value
        End Set
    End Property

End Class
