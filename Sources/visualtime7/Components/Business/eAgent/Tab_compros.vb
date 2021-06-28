Option Strict Off
Option Explicit On
Public Class Tab_compros
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_compros.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxType_tran As Integer
	
	
	'**% Add: Adds a new instance of the Tab_compro class to the collection
	'% Add: Añade una nueva instancia de la clase Tab_compro a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nType_tran As Integer, ByVal nLine As Integer, ByVal nTyp_acco As Integer, ByVal sDebitSide As String, ByVal nTyp_amount As Integer, ByVal nUsercode As Integer) As Tab_compro
		'create a new object
		
		Dim objNewMember As Tab_compro
		objNewMember = New Tab_compro
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nType_tran = nType_tran
			.nLine = nLine
			.nTyp_acco = nTyp_acco
			.sDebitSide = sDebitSide
			.nTyp_amount = nTyp_amount
			.nUsercode = nUsercode
		End With
		
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'**% Find: Restores a collection of objects of the Tab_compro type.
	'% Find: Devuelve una coleccion de objetos de tipo Tab_compro
	'------------------------------------------------------------
	Public Function Find(ByVal nType_tran As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'**- Variable definition lrecTab_compro that will be used as a cursor.
		'- Se define la variable lrecTab_compro que se utilizará como cursor.
		Dim lrecTab_compro As eRemoteDB.Execute
		
		lrecTab_compro = New eRemoteDB.Execute
		
		If nAuxType_tran = nType_tran And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's transactions
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecTab_compro
				.StoredProcedure = "reaTab_compro_v"
				.Parameters.Add("nType_tran", nType_tran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If Not .Run Then
					Find = False
					nAuxType_tran = eRemoteDB.Constants.intNull
				Else
					nAuxType_tran = nType_tran
					Find = True
					
					Do While Not .EOF
						Call Add(eRemoteDB.Constants.intNull, .FieldToClass("nType_tran"), .FieldToClass("nLine"), .FieldToClass("nTyp_acco"), .FieldToClass("sDebitSide"), .FieldToClass("nTyp_amount"), .FieldToClass("nUsercode"))
						.RNext()
					Loop 
				End If
			End With
		End If
		'UPGRADE_NOTE: Object lrecTab_compro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_compro = Nothing
	End Function
	
	'** Update: This fuctions is in charge of calling the  correspondent procedure
	'** to the action that it is being done.
	' Update: Esta función se encarga de llamar al procedimiento correspondiente
	' a la acción que se esté realizando.
	Public Function Update() As Boolean
		Dim lclsTab_compro As Tab_compro
		For	Each lclsTab_compro In mCol
			Select Case lclsTab_compro.nStatusInstance
				
				'**+ Add
				'+Agregar
				
				Case 1
					Update = lclsTab_compro.Add()
					'**+ Update
					'+Actualizar
					
				Case 2
					Update = lclsTab_compro.Update()
					
					'**+ Remove
					'+Eliminar
					
				Case 3
					Update = lclsTab_compro.Delete()
					
			End Select
			
			If Update = False Then
				Exit For
			End If
		Next lclsTab_compro
	End Function
	
	'*** Item: Restores an element of the collection (according to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_compro
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Restores a number of elements that the collection
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Allows to enumerate the collection for using it in cycle For Each...Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Removes an element fro the collection
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection.
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAuxType_tran = eRemoteDB.Constants.intNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the delete of an instance of the collection.
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






