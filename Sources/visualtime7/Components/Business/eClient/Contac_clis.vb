Option Strict Off
Option Explicit On
Public Class Contac_clis
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Contac_clis.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables para la coleccion
	Private mCol As Collection
	
	'**- Define an auxiliary variable to search for data in the table.
	'- Se define una variable auxiliar para forzar la búsqueda de los datos en la tabla
	Private mAuxClient As String
	
	'**% Add: Adds a new element to the collection.
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal sClient As String, ByVal sClientr As String, ByVal dEffecdate As Date, ByVal nOrder As Integer, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nPosition As Integer, ByVal sCliename As String) As Contac_cli
		Dim objNewMember As Contac_cli
		
		objNewMember = New Contac_cli
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.sClient = sClient
			.sClientr = sClientr
			.dEffecdate = dEffecdate
			.nOrder = nOrder
			.dNulldate = dNulldate
			.nUsercode = nUsercode
			.nPosition = nPosition
			.sCliename = sCliename
		End With
		
		mCol.Add(objNewMember, "FC" & sClient & sClientr & nPosition & dNulldate & dEffecdate)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		
	End Function
	
	'**% Update: runs the collection and updates the data in the table.
	'% Update: recorre la colección y actualiza los datos en la tabla
	Public Function Update() As Boolean
		Dim lclsContac_cli As Contac_cli
		
		On Error GoTo Update_Err
		
		Update = True
		
		For	Each lclsContac_cli In mCol
			With lclsContac_cli
				If mAuxClient = String.Empty Then
					mAuxClient = .sClient
				End If
				Select Case .nStatusInstance
					Case 0
						Update = .Add
						.nStatusInstance = 1
					Case 2
						Update = .Update
					Case 3
						Update = .Delete
						mCol.Remove(("FC" & .sClient & .sClientr & .nPosition & .dNulldate & .dEffecdate))
				End Select
			End With
		Next lclsContac_cli
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
	End Function
	
	'**% Find: searches for client contacts.
	'% Find: busca los datos correspondientes a un cliente
	Public Function Find(ByVal sClient As String) As Boolean
		Dim lrecreaContac_cli As eRemoteDB.Execute
		
		lrecreaContac_cli = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If sClient = mAuxClient Then
			Find = True
		Else
			With lrecreaContac_cli
				.StoredProcedure = "reaContac_cli"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						Call Add(1, .FieldToClass("sClient"), .FieldToClass("sClientr"), .FieldToClass("dEffecdate"), .FieldToClass("nOrder"), .FieldToClass("dNulldate"), .FieldToClass("nUsercode"), .FieldToClass("nPosition"), .FieldToClass("sCliename"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
					mAuxClient = sClient
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaContac_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaContac_cli = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
	
	'**% Item: get an element of the collection.
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Contac_cli
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% Count: counts the number of elements inside the collection
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: enumerates the elements inside the collection
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
	
	'**% Remove: removes an element inside the collection.
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: controls the opening of each instance of the collection.
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: deletes the collection.
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






