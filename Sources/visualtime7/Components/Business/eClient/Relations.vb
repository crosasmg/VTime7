Option Strict Off
Option Explicit On
Public Class Relations
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Relations.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**- Auxiliary variable definition. This variable is used to force the read of the data in the table
	'- Se define una variable auxiliar para forzar la búsqueda de los datos en la tabla
	Private mAuxClient As String
	
	'**% Add: Adds a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal sClient As String, ByVal sClientr As String, ByVal nRelaship As Integer, ByVal sCliename As String, ByVal sDigit As String, ByVal sRelashipDesc As String) As Relation
		'- Se define variable que almacena las propiedades y metodos de la clase principal
		Dim objNewMember As Relation
		
		objNewMember = New Relation
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.sClient = sClient
			.sClientr = sClientr
			.sCliename = sCliename
			.nRelaship = nRelaship
			.sDigit = sDigit
			.sRelashipDesc = sRelashipDesc
		End With
		
		mCol.Add(objNewMember, "RS" & sClient & sClientr & nRelaship)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Update: Update the table with the data of the collection
	'% Update: recorre la colección y actualiza los datos en la tabla
	Public Function Update() As Boolean
		Dim lclsRelation As Relation
		
		Update = True
		
		For	Each lclsRelation In mCol
			With lclsRelation
				If mAuxClient = String.Empty Then mAuxClient = .sClient
				Select Case .nStatusInstance
					Case 0
						Update = .Add
						.nStatusInstance = 1
					Case 2
						Update = .Update
					Case 3
						Update = .Delete
						mCol.Remove(("RS" & .sClient & .sClientr & .nRelaship))
				End Select
			End With
		Next lclsRelation
		
	End Function
	
	'**% Find: Searches for the data of  a client
	'% Find: busca los datos correspondientes a un cliente
	Public Function Find(ByVal sClient As String) As Boolean
		Dim lrecreaRelations As eRemoteDB.Execute
		
		lrecreaRelations = New eRemoteDB.Execute
		
		If sClient = mAuxClient Then
			Find = True
		Else
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			With lrecreaRelations
				.StoredProcedure = "reaRelations_a"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mCol = New Collection
					Do While Not .EOF
						Call Add(1, sClient, .FieldToClass("sClientr"), .FieldToClass("nRelaship"), .FieldToClass("sCliename"), .FieldToClass("sDigit"), .FieldToClass("sRelashipDesc"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
					mAuxClient = sClient
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaRelations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaRelations = Nothing
		End If
		
	End Function
	
	'**% Item: Gets an element of the collection
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Relation
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% Count: Counts the number of elements in the collection
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: enumerates the elements of the collection
	'% NewEnum: enumera los elementos dentro de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: deletes an element of the collection
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the opening of each instance of the collection
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: deletes the collection
	'% Class_Terminate: elimina la colección
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






