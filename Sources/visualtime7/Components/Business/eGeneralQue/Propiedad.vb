Option Strict Off
Option Explicit On
Public Class Propiedad
	'%-------------------------------------------------------%'
	'% $Workfile:: Propiedad.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'**-local variable(s) to hold property value(s)
	Private mvarValor As Object 'local copy
	'**-local variable(s) to hold property value(s)
	Private mvarCaption As String 'local copy
	'**-local variable(s) to hold property value(s)
	Private mvarvisible As String 'local copy
	
	Private mvarKey As String
	
	Public Key As String
	'**-local variable(s) to hold property value(s)
	Private mvarDataType As Integer 'local copy
	Private mvarFormat As String
	
	
	
	
	Public Property DataType() As Integer
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.DataType
			DataType = mvarDataType
		End Get
		Set(ByVal Value As Integer)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.DataType = 5
			mvarDataType = Value
		End Set
	End Property
	
	
	
	
	Public Property visible() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.Visible
			visible = mvarvisible
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.Visible = 5
			mvarvisible = Value
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
	
    Public Property Valor() As Object
        Get
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.Valor
            If IsReference(mvarValor) Then
                Valor = mvarValor
            Else
                Valor = mvarValor
            End If
        End Get
        Set(ByVal Value As Object)
            If IsReference(Value) And Not TypeOf Value Is String Then
                'used when assigning an Object to the property, on the left side of a Set statement.
                'Syntax: Set x.Valor = Form1
                mvarValor = Value
            Else
                'used when assigning a value to the property, on the left side of an assignment.
                'Syntax: X.Valor = 5
                mvarValor = Value
            End If
        End Set
    End Property
	
	Public ReadOnly Property ValorConFormato() As String
		Get
            If IsDBNull(mvarValor) Then
                ValorConFormato = ""
            ElseIf mvarFormat = "" Then
                ValorConFormato = CStr(mvarValor)
            Else
                ValorConFormato = Format(mvarValor, mvarFormat)
            End If

            If Not String.IsNullOrEmpty(ValorConFormato) Then
                'Cuando viene con -32768
                Try
                    If Convert.ToUInt32(ValorConFormato) = eRemoteDB.Constants.intNull Then
                        ValorConFormato = eRemoteDB.Constants.strNull
                    End If
                Catch ex As Exception
                    ' Do Nothing
                End Try

                'Cuando viene con -32768,00
                Try
                    If Convert.ToDouble(ValorConFormato) = eRemoteDB.Constants.intNull Then
                        ValorConFormato = eRemoteDB.Constants.strNull
                    End If
                Catch ex As Exception
                    ' Do Nothing
                End Try

                'Cuando viene con -32768,3276
                Try
                    If Convert.ToDouble(ValorConFormato) = eRemoteDB.Constants.dblNull Then
                        ValorConFormato = eRemoteDB.Constants.strNull
                    End If
                Catch ex As Exception
                    ' Do Nothing
                End Try

                Try
                    If ValorConFormato = eRemoteDB.Constants.dtmNull Then
                        ValorConFormato = eRemoteDB.Constants.strNull
                    End If
                Catch ex As Exception
                    ' Do Nothing
                End Try

                Try
                    If Convert.ToDateTime(ValorConFormato) = eRemoteDB.Constants.dtmNull Then
                        ValorConFormato = eRemoteDB.Constants.strNull
                    End If
                Catch ex As Exception
                    ' Do Nothing
                End Try

            End If
		End Get
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
	
	
	
	Public Property IsKey() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.DataType
			IsKey = mvarKey
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.DataType = 5
			mvarKey = Value
		End Set
	End Property
End Class






