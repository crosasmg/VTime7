Option Strict Off
Option Explicit On
Public Class Commis_hiss
	Implements System.Collections.IEnumerable
	'**- mCol is defined for hold the collection
	'- Se define la variable  mCol, para contener la coleccion de elementos
	
	Private mCol As Collection
	
	'**%Add: adds a new instance of the "commiss_his" class to the collection
	'%Add: Añade una nueva instancia de la clase "commiss_his" a la colección
	Public Function Add(ByVal nIntermed As Integer, ByVal sTyp_comiss As String, ByVal nComtab As Integer, ByVal dEffecdate As Date, ByVal dEffecdate_Old As Date, ByVal sTabComDes As String) As commis_his
		
		
		'create a new object
		Dim objNewMember As commis_his
		objNewMember = New commis_his
		
		With objNewMember
			.nIntermed = nIntermed
			.nComtab = nComtab
			.dEffecdate = dEffecdate
			.dEffecdate_Old = dEffecdate_Old
			.sTyp_comiss = sTyp_comiss
			.sTabComDes = sTabComDes
		End With
		
		
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% FindAGC001: Busca la información de un determinado intermediario
	Public Function FindAGC001(ByVal nIntermed As Integer, ByVal dEffecdate As Date) As Boolean
		'- Se define la variable lrecCommis_his que se utilizará como cursor.
		Dim lrecCommis_his As eRemoteDB.Execute
		Dim lclsCommis_his As commis_his
		
		On Error GoTo FindAGC001_Err
		
		lrecCommis_his = New eRemoteDB.Execute
		lclsCommis_his = New commis_his
		
		'**+ Execute the store procedure that searches an intermediary's transactions
		'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
		With lrecCommis_his
			.StoredProcedure = "reaCommis_his_AGC001"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				FindAGC001 = False
			Else
				FindAGC001 = True
				Do While Not .EOF
					lclsCommis_his = Add(eRemoteDB.Constants.intNull, .FieldToClass("sTyp_comiss"), .FieldToClass("nComtab"), .FieldToClass("dEffecdate"), dtmNull, .FieldToClass("sTabComDes"))
					.RNext()
				Loop 
			End If
		End With
		
FindAGC001_Err: 
		If Err.Number Then
			FindAGC001 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCommis_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCommis_his = Nothing
		'UPGRADE_NOTE: Object lclsCommis_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCommis_his = Nothing
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As commis_his
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






