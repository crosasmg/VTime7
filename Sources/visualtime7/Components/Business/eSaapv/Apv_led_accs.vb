Option Strict Off
Option Explicit On
Public Class Apv_led_accs
	Implements System.Collections.IEnumerable
	
	
	
	
	Private mCol As Collection
	
	Public nCount As Integer
	
	'% Add: Añade una nueva instancia de la clase Apv_led_acc a la colección
	Public Function Add(ByVal nType_move As Integer, ByVal nTyp_profitworker As Integer, ByVal sLedacc As String, ByVal sDescled As String) As Apv_led_acc
		'create a new object
		
		Dim objNewMember As Apv_led_acc
		objNewMember = New Apv_led_acc
		
		With objNewMember
			.nType_move = nType_move
			.nTyp_profitworker = nTyp_profitworker
			.sLedacc = sLedacc
			.sDescled = sDescled
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	Public Function Find() As Boolean
		'- Se define la variable lrecApv_led_accs que se utilizará como cursor.
		Dim lrecReaApv_led_accs As eRemoteDB.Execute
		
		lrecReaApv_led_accs = New eRemoteDB.Execute
		
		'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
		
		With lrecReaApv_led_accs
			.StoredProcedure = "INSMCA1485PKG.FIND"
			
			If Not .Run Then
				Find = False
			Else
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("nType_move"), .FieldToClass("nTyp_profitworker"), .FieldToClass("sLedacc"), .FieldToClass("sDescled"))
					.RNext()
				Loop 
				
			End If
		End With
		lrecReaApv_led_accs = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Apv_led_acc
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	Private Sub Class_Terminate_Renamed()
		
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






