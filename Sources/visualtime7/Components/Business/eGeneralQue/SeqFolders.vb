Option Strict Off
Option Explicit On
Public Class SeqFolders
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: SeqFolders.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**- Define the auxiliary variables to avoid an unnecessary search.
	'- Se definen las variables auxiliares para evitar una búsqueda innecesaria
	
	Private lauxQueryType As Integer
	
	
	'**% Add: adds a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nQueryType As Integer, ByVal nParent As Integer, ByVal nFolder As Integer, ByVal nSequence As Integer) As SeqFolder
		
		'**+ Create a new object
		
		Dim objNewMember As SeqFolder
		
		objNewMember = New SeqFolder
		
		'**+ Set the properties passed into the method
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nQueryType = nQueryType
			.nParent = nParent
			.nFolder = nFolder
			.nSequence = nSequence
		End With
		
		mCol.Add(objNewMember, "A" & nParent & nFolder)
		
		'**+ Return the object created
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Find: searches the belonging data to a budget.
	'% Find: busca los datos pertenecientes a un presupuesto
	Public Function Find(ByVal QueryType As Integer, Optional ByVal Parent As Object = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecreaSeqFolder_All As eRemoteDB.Execute
		Dim lobjSeqfolder As SeqFolder
		lrecreaSeqFolder_All = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaSeqFolder_All'
		'+ Definición de parámetros para stored procedure 'insudb.reaSeqFolder_All'
		'**+ Information read on Decemeber 30,1999  15:17:05
		'+ Información leída el 30/12/1999 15:17:05
		
		With lrecreaSeqFolder_All
			.StoredProcedure = "reaSeqFolderAll"
			.Parameters.Add("QueryType", QueryType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nParent", IIf(Parent = eRemoteDB.Constants.intNull, System.DBNull.Value, Parent), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nFolder", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				Do While Not .EOF
					lobjSeqfolder = Add(0, .FieldToClass("nQueryType"), .FieldToClass("nParent"), .FieldToClass("nfolder"), .FieldToClass("nSequence"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSeqFolder_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSeqFolder_All = Nothing
	End Function
	
	'**% Update: processes each instace of the class in the collection.
	'% Update: realiza el tratamiento de cada instancia de la clase en la colección
	Public Function Update() As Boolean
		Dim lclsSeqFolder As SeqFolder
		Update = True
		For	Each lclsSeqFolder In mCol
			With lclsSeqFolder
				
				If lauxQueryType = 0 Then
					lauxQueryType = .nQueryType
				End If
				
				Select Case .nStatusInstance
					
					'**+ If the action is Add
					'+ Si la acción es Agregar
					Case 1
						Update = .Add()
						
						'**+ If the action is Update
						'+ Si la acción es Actualizar
					Case 2
						Update = .Update()
						
						'**+ If the action is Delete
						'+ Si la acción es Eliminar
					Case 3
						Update = .Delete()
				End Select
				
				If Update Then
					.nStatusInstance = 0
				End If
			End With
		Next lclsSeqFolder
	End Function
	
	'**% insPostMGE003. This funtion updates the table
	'%insPostMGE003. Esta funcion se encarga de realizar las actualización de la
	Public Function insPostMGE003(ByVal nQueryType As Integer, ByVal sSequence As String, ByVal nUsercode As Integer) As Boolean
		Dim llngIndex As Integer
		Dim llngIndex2 As Integer
		Dim lstrExtract As String
		Dim lintParent As Integer
		Dim lintFolder As Integer
        Dim lclsSeqFolder As SeqFolder = New SeqFolder
        sSequence = Trim(sSequence)
		If Len(sSequence) > 0 Then
			llngIndex = 1
		Else
			llngIndex = 0
			insPostMGE003 = True
		End If
		Do While llngIndex > 0
			llngIndex2 = InStr(llngIndex + 1, sSequence, ",")
			If llngIndex2 > 0 Then
				lstrExtract = Mid(sSequence, llngIndex, llngIndex2 - llngIndex)
				lintParent = CInt(Mid(lstrExtract, 1, InStr(1, lstrExtract, "-") - 1))
				lintFolder = CInt(Mid(lstrExtract, InStr(1, lstrExtract, "-") + 1, Len(lstrExtract)))
				If lclsSeqFolder Is Nothing Then
					lclsSeqFolder = New SeqFolder
				End If
				With lclsSeqFolder
					.nSequence = eRemoteDB.Constants.intNull
					.nParent = lintParent
					.nFolder = lintFolder
					.nUsercode = nUsercode
					.nQueryType = nQueryType
					.nStatusInstance = 1
					insPostMGE003 = .Add()
				End With
			End If
			If llngIndex2 = 0 Then
				llngIndex = 0
			Else
				llngIndex = llngIndex2 + 1
			End If
		Loop 
	End Function
	
	'*** Item: takes an element from the collection.
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As SeqFolder
		Get
			'**+ Used when referencing an element in the collection.
			'**+ vntIndexKey contains either the Index or Key to the collection,
			'**+ this is why it is declared as a Variant
			'**+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: counts the elements of the collection
	'* Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			'**+ Used when retrieving the number of elements in the collection.
			'**+ Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: enumerates the elements of the collection
	'* NewEnum: enumera los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'**+This property allows you to enumerate this collection with the For...Each syntax
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'*** Remove: removes an element from the collection
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'**+ Used when removing an element from the collection.
		'**+ vntIndexKey contains either the Index or Key, which is why
		'**+ it is declared as a Variant
		'**+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'*** Class_Initialize: controls the opening of the collection.
	'* Class_Initialize: controla la apertura de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'**+ Creates the collection when this class is created
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*** Class_Terminate: controls the end of the collection
	'* Class_Terminate: controla el fin de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'**+ Destroys collection when this class is terminated
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






