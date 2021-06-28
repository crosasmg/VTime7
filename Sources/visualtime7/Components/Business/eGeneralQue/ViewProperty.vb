Option Strict Off
Option Explicit On
Friend Class ViewProperty
	'%-------------------------------------------------------%'
	'% $Workfile:: ViewProperty.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Public Key As String
	
	'**-local variable(s) to hold property value(s)
	'local copy
	Private mvarCaption As String
	
	'**-local variable(s) to hold property value(s)
	'local copy
	Private mvarvisible As String
	
	'**-local variable(s) to hold property value(s)
	'local copy
	Private mvarFormat As String
	
	'**-local variable(s) to hold property value(s)
	'local copy
	Private mvarKey As String
	
	Private mvarAlignment As Integer 'local copy
	
	
	Public Property Alignment() As Integer
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.Alignment
			Alignment = mvarAlignment
		End Get
		Set(ByVal Value As Integer)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.Alignment = 5
			mvarAlignment = Value
		End Set
	End Property
	
	
	'UPGRADE_NOTE: Format was upgraded to Format_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Property Format_Renamed() As String
		Get
			Format_Renamed = mvarFormat
		End Get
		Set(ByVal Value As String)
			mvarFormat = Value
		End Set
	End Property
	
	
	Public Property visible() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.visible
			visible = mvarvisible
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.visible = 5
			mvarvisible = Value
		End Set
	End Property
	
	
	Public Property IsKey() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.visible
			IsKey = mvarKey
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.visible = 5
			mvarKey = Value
		End Set
	End Property
	
	
	Public Property Caption() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.Caption
			Caption = mvarCaption
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.Caption = 5
			mvarCaption = Value
		End Set
	End Property
End Class






