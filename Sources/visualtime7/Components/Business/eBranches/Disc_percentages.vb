Option Strict Off
Option Explicit On
Public Class Disc_percentages
	Implements System.Collections.IEnumerable
	
	'+ Variable local para mantener la colección.
	
	Private mCol As Collection
	
	'**% Add: Adds a new instance of the Disc_percentage class to the collection.
	'% Add: Añade una nueva instancia de la clase Disc_percentage a la colección.
	Public Function Add(ByRef objClass As Disc_percentage) As Disc_percentage
		'+ Crea un nuevo proyecto.
		
		If objClass Is Nothing Then
			objClass = New Disc_percentage
		End If
		
		With objClass
			mCol.Add(objClass, .nBranch & .nProduct & .nModulec & .nCover & .nRole & .dEffecdate & .nAge_ini & .nAge_End & .nQprempayed & .nDisc_percentage & .sExtraprem)
		End With
		
		'+ Retorna el objeto creado.
		
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'*** Item: Returns an element of the collection (acording to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Disc_percentage
		Get
			'+ Used when referencing an element in the collection vntIndexKey contains either the Index
			'+ or Key to the collection, this is why it is declared as a Variant Syntax: Set foo = x.Item(xyz)
			'+ or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Returns the number of elements that the collection has
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'+ Used when retrieving the number of elements in the collection. Syntax: Debug.Print x.Count.
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Enumerates the collection for use in a For Each...Next loop
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+ This property allows you to enumerate this collection with the For...Each syntax.
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colección.
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ Used when removing an element from the collection vntIndexKey contains either the Index or
		'+ Key, which is why it is declared as a Variant Syntax: x.Remove(xyz).
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+ Creates the collection when this class is created.
		
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
		'+ Destroys collection when this class is terminated.
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Se buscan los datos de la tabla Disc_percentage.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecinsMVI8001 As Object
		Dim lclsDisc_percentage As Disc_percentage
		
		On Error GoTo Find_Err
		
		lrecinsMVI8001 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		With lrecinsMVI8001
			
			.StoredProcedure = "INSMVI8001PKG.INSREAMVI8001"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsDisc_percentage = New Disc_percentage
					
					lclsDisc_percentage.nBranch = nBranch
					lclsDisc_percentage.nProduct = nProduct
					lclsDisc_percentage.nModulec = nModulec
					lclsDisc_percentage.nCover = nCover
					lclsDisc_percentage.nRole = nRole
					lclsDisc_percentage.dEffecdate = dEffecdate
					lclsDisc_percentage.nAge_ini = .FieldToClass("nAge_ini")
					lclsDisc_percentage.nAge_End = .FieldToClass("nAge_end")
					lclsDisc_percentage.nQprempayed = .FieldToClass("nQprempayed")
					lclsDisc_percentage.nDisc_percentage = .FieldToClass("nDisc_percentage")
					lclsDisc_percentage.sExtraprem = .FieldToClass("sExtraprem")
					
					Call Add(lclsDisc_percentage)
					'UPGRADE_NOTE: Object lclsDisc_percentage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDisc_percentage = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsMVI8001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsMVI8001 = Nothing
		'UPGRADE_NOTE: Object lclsDisc_percentage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisc_percentage = Nothing
		
	End Function
End Class






