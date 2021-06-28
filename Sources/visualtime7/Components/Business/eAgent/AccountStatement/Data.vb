<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class data
    
    Private accountHolderField As accountHolder
    
    Private statementInfoField As statementInfo
    
    Private transactionsField As List (Of transaction)
    
    Private opremiumsField As List (Of opremium)
    
    Private rewardsInfoField As List (Of rewardInfo)
    
    '''<remarks/>
    Public Property accountHolder() As accountHolder
        Get
            Return Me.accountHolderField
        End Get
        Set
            Me.accountHolderField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property statementInfo() As statementInfo
        Get
            Return Me.statementInfoField
        End Get
        Set
            Me.statementInfoField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("transaction", IsNullable:=false)>  _
    Public Property transactions() As List (Of transaction)
        Get
            Return Me.transactionsField
        End Get
        Set
            Me.transactionsField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("opremium", IsNullable:=false)>  _
    Public Property opremiums() As List (Of opremium)
        Get
            Return Me.opremiumsField
        End Get
        Set
            Me.opremiumsField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property rewardsInfo() As List (Of rewardInfo)
        Get
            Return Me.rewardsInfoField
        End Get
        Set
            Me.rewardsInfoField = value
        End Set
    End Property
    
End Class
