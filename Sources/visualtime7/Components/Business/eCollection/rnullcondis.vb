Option Strict Off
Option Explicit On
Public Class rnullcondis
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: rnullcondis.cls                          $%'
	'% $Author:: Nvaplat26                                  $%'
	'% $Date:: 30/10/03 18.15                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'**+Local variable to hold collection.
	'+ Variable Local para almacenar la colección.
	
	Private mcolrnullcondi As Collection
	
	Public nCount As Integer
	'**%Add: It adds an element to the collection.
	'% Add: Agrega un elemento a la colección.
	Public Function Add(ByRef lclsrnullcondi As rnullcondi) As rnullcondi
		
		'set the properties passed into the method
		mcolrnullcondi.Add(lclsrnullcondi)
		
		'return the object created
		Add = lclsrnullcondi
		'UPGRADE_NOTE: Object lclsrnullcondi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsrnullcondi = Nothing
	End Function
	
	'**%Find: Function that makes the search in the table 'rnullcondi'.
	'% Find: Función que realiza la busqueda en la tabla 'rnullcondi'.
	Public Function Find(ByVal dEffecdate As Date, ByVal nRow As Integer) As Boolean
		Dim lclsrnullcondi As eRemoteDB.Execute
		Dim lclsrnullcondiItem As rnullcondi
		
		lclsrnullcondi = New eRemoteDB.Execute
		
		With lclsrnullcondi
			.StoredProcedure = "rearnullcondi_a"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			nCount = 1
			
			If .Run Then
				Find = True
				Do While Not .EOF And nCount < nRow
					nCount = nCount + 1
					.RNext()
				Loop 
				
				Do While Not .EOF And nCount < nRow + 12
					nCount = nCount + 1
					lclsrnullcondiItem = New rnullcondi
					lclsrnullcondiItem.dEffecdate = .FieldToClass("dEffecdate")
					lclsrnullcondiItem.nNullcode = .FieldToClass("nNullcode")
					lclsrnullcondiItem.nBranch = .FieldToClass("nBranch")
					lclsrnullcondiItem.nProduct = .FieldToClass("nProduct")
					lclsrnullcondiItem.sPolitype = .FieldToClass("sPolitype")
					lclsrnullcondiItem.sPolicy = .FieldToClass("sPolicy")
					lclsrnullcondiItem.sCertif = .FieldToClass("sCertif")
					lclsrnullcondiItem.nTratypei = .FieldToClass("nTratypei")
					Call Add(lclsrnullcondiItem)
					'UPGRADE_NOTE: Object lclsrnullcondiItem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsrnullcondiItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsrnullcondi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsrnullcondi = Nothing
	End Function
	
	'***Item: This property is used when reference to an element becomes of the collection.
	'* Item: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As rnullcondi
		Get
			Item = mcolrnullcondi.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: It returns the amount of existing elements in the collection.
	'* Count: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolrnullcondi.Count()
		End Get
	End Property
	
	'***NewEnum: This property allows you to enumerate this collection with the "For...Each".
	'* NewEnum: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolrnullcondi._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolrnullcondi.GetEnumerator
	End Function
	
	'***Remove: It allows to remove an element of the collection.
	'* Remove: Permite eliminar un elemento de la colección.
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolrnullcondi.Remove(vntIndexKey)
	End Sub
	
	'***Class_Initialize: Creates the collection when this class is created.
	'* Class_Initialize: Crea la colección cuando se crea esta clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolrnullcondi = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'***Class_Terminate: Destroys collection when this class is terminated.
	'* Class_Terminate: Destruye la colección cuando se termina esta clase.
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolrnullcondi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolrnullcondi = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






