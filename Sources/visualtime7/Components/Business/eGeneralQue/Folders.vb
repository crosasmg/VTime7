Option Strict Off
Option Explicit On
Public Class Folders
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Folders.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**- Define the auxiliary variables to avoid an unnecessary search
	'- Se definen las variables auxiliares para evitar una búsqueda innecesaria
	
	Private lauxFolder As Integer
	'**% Add: Adds a new element to the collection.
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nFolder As Integer, ByVal sFolderName As String, ByVal sRootName As String, ByVal nImage As Integer, ByVal sClass As String, ByVal sFolderKey As String, ByVal nFolderKey As Integer, ByVal sOrigi As String) As Folder
		
		'Create a new object
		
		Dim objNewMember As Folder
		
		objNewMember = New Folder
		
		'Set the properties passed into the method
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nFolder = nFolder
			.sFolderName = sFolderName
			.sRootName = sRootName
			.nImage = nImage
			.sClass = sClass
			.sFolderKey = sFolderKey
			.nFolderKey = nFolderKey
			.sOrigi = sOrigi
		End With
		
		mCol.Add(objNewMember, "A" & nFolder)
		
		'Return the object created
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	'**% Find: searches the data that belongs to a budget.
	'% Find: busca los datos pertenecientes a un presupuesto
	Public Function Find() As Boolean
		Dim lrecreaFolder_All As eRemoteDB.Execute
		Dim lobjFolder As Folder
		lrecreaFolder_All = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaFolder_All'
		'+ Definición de parámetros para stored procedure 'insudb.reaFolder_All'
		'**+ Information read on December 30, 1999  15:17:05
		'+ Información leída el 30/12/1999 15:17:05
		
		With lrecreaFolder_All
			.StoredProcedure = "reaFolders"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nFolder", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				Do While Not .EOF
					lobjFolder = Add(0, .FieldToClass("nFolder"), .FieldToClass("sFolderName"), .FieldToClass("sRootName"), .FieldToClass("nImage"), .FieldToClass("sClass"), .FieldToClass("sFolderKey"), .FieldToClass("nFolderKey"), .FieldToClass("sOrigi"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaFolder_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFolder_All = Nothing
	End Function
	
	'**% Update: makes the treatment of each instance of the class in the collection.
	'% Update: realiza el tratamiento de cada instancia de la clase en la colección
	Public Function Update() As Boolean
		Dim lclsFolder As Folder
		Update = True
		For	Each lclsFolder In mCol
			With lclsFolder
				
				If lauxFolder = 0 Then
					lauxFolder = .nFolder
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
		Next lclsFolder
	End Function
	
	'*** Item: takes an elemnt of the collection.
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Folder
		Get
			'Used when referencing an element in the collection.
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: counts the elements of the collection.
	'* Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			'Used when retrieving the number of elements in the collection.
			'Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: enumerates the elements of the collection.
	'* NewEnum: enumera los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'This property allows you to enumerate this collection with the For...Each syntax
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
		'+ Used when removing an element from the collection.
		'+ vntIndexKey contains either the Index or Key, which is why
		'+ it is declared as a Variant
		'+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'*** Class_Initialize: controls the opening of the collection.
	'* Class_Initialize: controla la apertura de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'Creates the collection when this class is created
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*** Class_Termiante: controls the end of the collection.
	'* Class_Terminate: controla el fin de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'Destroys collection when this class is terminated
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






