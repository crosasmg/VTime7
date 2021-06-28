Option Strict Off
Option Explicit On
Public Class Tab_locats
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	
	'% Add: Adds a new instance to the class Tab_locat to the collection.
	'% Add: Añade una nueva instancia de la clase Tab_locat a la colección
	Public Function Add(ByVal objElement As Object) As Tab_locat
		
		Dim objNewMember As Tab_locat
		objNewMember = objElement
		
		mCol.Add(objNewMember)
		
		'+ Returns the created object.
		'+ Retorna el objeto creado
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% Find:Returns the information from all the registered ocations
	'%      in the Locations table (Tab_locat)
	'% Find:Devuelve información de todas las localidades registradas
	'%      en la tabla de Localidades (Tab_locat)
	Public Function Find() As Boolean
		
		Static lblnRead As Boolean
		Dim lrecreaTab_locat_a As eRemoteDB.Execute
		Dim lclstab_locat As Tab_locat
		
		On Error GoTo Find_Err
		
		lrecreaTab_locat_a = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_locat_a'
		'+ Información leída el 03/07/2001 11:33:03 a.m.
		With lrecreaTab_locat_a
			.StoredProcedure = "reaTab_locat_a"
			
			If .Run Then
				Do While Not .EOF
					lclstab_locat = New Tab_locat
					
					lclstab_locat.nLocal = .FieldToClass("nLocal")
					lclstab_locat.sDescript = .FieldToClass("sDescript")
					lclstab_locat.nProvince = .FieldToClass("nProvince")
					lclstab_locat.sShort_des = .FieldToClass("sShort_des")
					lclstab_locat.sLegal_loc = .FieldToClass("sLegal_loc")
					lclstab_locat.sDescript_Prov = .FieldToClass("sDescript_Prov")
					
					Call Add(lclstab_locat)
					
					'UPGRADE_NOTE: Object lclstab_locat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclstab_locat = Nothing
					
					.RNext()
				Loop 
				
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_locat_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_locat_a = Nothing
	End Function
	
	'% Item: restores an element from the collection (according to the index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	'------------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_locat
		Get
			'------------------------------------------------------------
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Restores the number of elements that the collection owns.
	'% Count: Devuelve el numero de elementos que posee la coleccion
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Allows to enumerate the collection for using it in a cylce For Each... Next
	'% NewEnum: Permite enumerar la coleccion para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Removes an element from the collection.
	'% Remove: Elimina un elemento de la coleccion
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: controls the creation of an instance of the collection.
	'% Class_Initialize: Controla la creacion de una instancia de la coleccion
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: controls the delete of an instance of the collection.
	'% Class_Terminate: Controla la destruccion de una instancia de la coleccion
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






