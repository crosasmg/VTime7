<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class statementInfo
    
    Private accountNoField As String
    
    Private currencyField As Integer

    Private statementDateField As String
    
    Private newCommisionsField As Decimal
    
    Private commisionsRefoundsField As Decimal
    
    Private paymentsAndCreditsField As Decimal
    
    Private closingBalanceField As Decimal
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute()>  _
    Public Property accountNo() As String
        Get
            Return Me.accountNoField
        End Get
        Set
            Me.accountNoField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute()>  _
    Public Property currency() As Integer
        Get
            Return Me.currencyField
        End Get
        Set
            Me.currencyField = value
        End Set
    End Property

    '''<remarks/>
    Public Property statementDate() As String
        Get
            Return Me.statementDateField
        End Get
        Set
            Me.statementDateField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property newCommisions() As Decimal
        Get
            Return Me.newCommisionsField
        End Get
        Set
            Me.newCommisionsField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property commisionsRefounds() As Decimal
        Get
            Return Me.commisionsRefoundsField
        End Get
        Set
            Me.commisionsRefoundsField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property paymentsAndCredits() As Decimal
        Get
            Return Me.paymentsAndCreditsField
        End Get
        Set
            Me.paymentsAndCreditsField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property closingBalance() As Decimal
        Get
            Return Me.closingBalanceField
        End Get
        Set
            Me.closingBalanceField = value
        End Set
    End Property
End Class
