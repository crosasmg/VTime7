Option Strict Off
Option Explicit On
Public Class Tab_matrix_rhs
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	
	Public nCount As Integer
	
	'% Add: Añade una nueva instancia de la clase Tab_matrix_rh a la colección
	Public Function Add(ByVal nType_move As Integer, ByVal nOrigin As Integer, ByVal nTyp_ProfitWorker As Integer, ByVal nTransac As Integer) As Tab_matrix_rh
		'create a new object
		
		Dim objNewMember As Tab_matrix_rh
		objNewMember = New Tab_matrix_rh
		
		With objNewMember
			.nType_move = nType_move
			.nOrigin = nOrigin
			.nTyp_ProfitWorker = nTyp_ProfitWorker
			.nTransac = nTransac
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	
	Public Function Find() As Boolean
		'- Se define la variable lrecTab_matrix_rhs que se utilizará como cursor.
		Dim lrecReaTab_matrix_rhs As eRemoteDB.Execute
		
		lrecReaTab_matrix_rhs = New eRemoteDB.Execute
		
		'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
		
		With lrecReaTab_matrix_rhs
			.StoredProcedure = "insmvi1488pkg.ReaTab_matrix_rh"
			
			If Not .Run Then
				Find = False
			Else
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("nType_move"), .FieldToClass("nOrigin"), .FieldToClass("nTyp_ProfitWorker"), .FieldToClass("nTransac"))
					.RNext()
				Loop 
			End If
		End With
		lrecReaTab_matrix_rhs = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_matrix_rh
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






