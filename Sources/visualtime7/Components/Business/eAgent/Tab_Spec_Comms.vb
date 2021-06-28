Option Strict Off
Option Explicit On
Public Class Tab_Spec_Comms
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Spec_Comms.cls                       $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 5/07/04 10:28p                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para mantener la colección.
	
	Private mCol As Collection
	
	'**% Add: Adds a new instance of the Supervis_commis class to the collection
	'% Add: Añade una nueva instancia de la clase Supervis_commis a la colección
	Public Function Add(ByVal objClass As Tab_Spec_Comm) As Tab_Spec_Comm
		'+ Crea un nuevo proyecto.
		
		If objClass Is Nothing Then
			objClass = New Tab_Spec_Comm
		End If
		
		With objClass
			mCol.Add(objClass, .nBranch & .nProduct & .dEffecdate & .nSlc_Tab_nr & .nType_comm & .nPolicy_year_ini & .nId)
		End With
		
		'+ Retorna el objeto creado.
		
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Spec_Comm
		Get
			'+ Used when referencing an element in the collection
			'+ vntIndexKey contains either the Index or Key to the collection,
			'+ this is why it is declared as a Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'+ Used when retrieving the number of elements in the
			'+ collection. Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+ This property allows you to enumerate this collection with the For...Each syntax.
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
		'+ Used when removing an element from the collection
		'+ vntIndexKey contains either the Index or Key, which is why
		'+ it is declared as a Variant
		'+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
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
	
	'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
	'% Find: Devuelve una coleccion de objetos de tipo Tab_Spec_Comm
	'------------------------------------------------------------
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nSlc_Tab_nr As Integer, ByVal dEffecdate As Date) As Boolean
		'------------------------------------------------------------
		'- Se define la variable lrecReaTab_Spec_Comm que se utilizará como cursor.
		
		Dim lrecReaTab_Spec_Comm As eRemoteDB.Execute
		Dim lclsTab_Spec_Comm As Tab_Spec_Comm
		
		On Error GoTo Find_Err
		
		lrecReaTab_Spec_Comm = New eRemoteDB.Execute
		
		'+ Se ejecuta el Store Procedure que busca los movimientos de la tabla de comisiones
		'+ especiales de vida.
		
		With lrecReaTab_Spec_Comm
			.StoredProcedure = "reaTab_Spec_Comm"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSlc_Tab_nr", nSlc_Tab_nr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				
				Do While Not .EOF
					lclsTab_Spec_Comm = New Tab_Spec_Comm
					
					lclsTab_Spec_Comm.nBranch = nBranch
					lclsTab_Spec_Comm.nProduct = nProduct
					lclsTab_Spec_Comm.dEffecdate = .FieldToClass("dEffecdate")
					lclsTab_Spec_Comm.nSlc_Tab_nr = nSlc_Tab_nr
					lclsTab_Spec_Comm.nCommiss_Pct = .FieldToClass("nCommiss_Pct")
					lclsTab_Spec_Comm.dNulldate = .FieldToClass("dNulldate")
					
					'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
					
					lclsTab_Spec_Comm.nPolicy_year_ini = .FieldToClass("nPolicy_year_ini")
					lclsTab_Spec_Comm.nPolicy_year_end = .FieldToClass("nPolicy_year_end")
					lclsTab_Spec_Comm.nModulec = .FieldToClass("nModulec")
					lclsTab_Spec_Comm.nCover = .FieldToClass("nCover")
					lclsTab_Spec_Comm.nType_comm = .FieldToClass("nType_comm")
					lclsTab_Spec_Comm.nId = .FieldToClass("nId")
					lclsTab_Spec_Comm.nCurrency = .FieldToClass("nCurrency")
					lclsTab_Spec_Comm.nMax_Amount = .FieldToClass("nMax_Amount")
					lclsTab_Spec_Comm.nAge_init = .FieldToClass("nAge_init")
					lclsTab_Spec_Comm.nAge_end = .FieldToClass("nAge_end")
					
					Call Add(lclsTab_Spec_Comm)
					'UPGRADE_NOTE: Object lclsTab_Spec_Comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_Spec_Comm = Nothing
					
					.RNext()
				Loop 
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaTab_Spec_Comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_Spec_Comm = Nothing
	End Function
End Class






