Option Strict Off
Option Explicit On
Public Class Intermed_partics
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Intermed_partics.cls                     $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxIntermed As Integer
	
	
	'**% Add: Adds a new instance of the Intermed_partic class to the collection
	'% Add: Añade una nueva instancia de la clase Intermed_partic a la colección
	Public Function Add(ByVal objNewMember As Intermed_partic) As Intermed_partic
		'create a new object
		
		If objNewMember Is Nothing Then
			objNewMember = New Intermed_partic
		End If
		
		With objNewMember
			mCol.Add(objNewMember, "a" & .nIntermed)
		End With
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'**% FindMAG573: Restores a collection of objects of Intermed_partic type
	'% FindMAG573: Devuelve una coleccion de objetos de tipo Intermed_partic
	'------------------------------------------------------------
	Public Function Find(ByVal nIntermed As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'**- Variable definition lrecIntermed_partic that will be used as a cursor
		'- Se define la variable lrecIntermed_partic que se utilizará como cursor.
		Dim lrecIntermed_partic As eRemoteDB.Execute
		Dim lclsIntermed_partic As Intermed_partic
		
		On Error GoTo Find_Err
		
		lrecIntermed_partic = New eRemoteDB.Execute
		
		If nAuxIntermed = nIntermed And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's movements
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecIntermed_partic
				.StoredProcedure = "reaIntermed_partic"
				.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run(True) Then
					Find = False
					nAuxIntermed = eRemoteDB.Constants.intNull
				Else
					nAuxIntermed = nIntermed
					Find = True
					
					Do While Not .EOF
						lclsIntermed_partic = New Intermed_partic
						lclsIntermed_partic.nIntermed = nIntermed
						lclsIntermed_partic.nSuperin_num = .FieldToClass("nSuperin_num")
						lclsIntermed_partic.dSuperin_num = .FieldToClass("dSuperin_num")
						lclsIntermed_partic.nWarran_pol = .FieldToClass("nWarran_pol")
						lclsIntermed_partic.nUsercode = .FieldToClass("nUsercode")
						Call Add(lclsIntermed_partic)
						.RNext()
						'UPGRADE_NOTE: Object lclsIntermed_partic may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsIntermed_partic = Nothing
					Loop 
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecIntermed_partic may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermed_partic = Nothing
		On Error GoTo 0
	End Function
	
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Intermed_partic
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAuxIntermed = eRemoteDB.Constants.intNull
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
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






