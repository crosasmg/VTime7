<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class phone
    
    Private keyToPhoneRecordField As String
    
    Private keyPhoneField As String
    
    Private recordOwnerField As String
    
    Private telephoneTypeField As String
    
    Private phoneNumberField As String
    
    Private areaCodeField As String
    
    Private extension1Field As String
    
    Private extension2Field As String
    
    Private orderField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property KeyToPhoneRecord() As String
        Get
            Return Me.keyToPhoneRecordField
        End Get
        Set
            Me.keyToPhoneRecordField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property KeyPhone() As String
        Get
            Return Me.keyPhoneField
        End Get
        Set
            Me.keyPhoneField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property RecordOwner() As String
        Get
            Return Me.recordOwnerField
        End Get
        Set
            Me.recordOwnerField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property telephoneType() As String
        Get
            Return Me.telephoneTypeField
        End Get
        Set
            Me.telephoneTypeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property phoneNumber() As String
        Get
            Return Me.phoneNumberField
        End Get
        Set
            Me.phoneNumberField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property areaCode() As String
        Get
            Return Me.areaCodeField
        End Get
        Set
            Me.areaCodeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property Extension1() As String
        Get
            Return Me.extension1Field
        End Get
        Set
            Me.extension1Field = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property Extension2() As String
        Get
            Return Me.extension2Field
        End Get
        Set
            Me.extension2Field = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")>  _
    Public Property Order() As String
        Get
            Return Me.orderField
        End Get
        Set
            Me.orderField = value
        End Set
    End Property
End Class
