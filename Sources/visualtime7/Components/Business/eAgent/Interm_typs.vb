Option Strict Off
Option Explicit On
Public Class Interm_typs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Interm_typs.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 5/12/03 15.57                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase Interm_typ a la colección
	Public Function Add(ByRef lclsInterm_typ As Interm_typ) As Interm_typ
		With lclsInterm_typ
			mCol.Add(lclsInterm_typ)
			
		End With
		
		'return the object created
		Add = lclsInterm_typ
		
	End Function
	
	'% Find: Devuelve una coleccion de objetos de tipo Interm_typ
	'------------------------------------------------------------
	Public Function Find() As Boolean
		'------------------------------------------------------------
		'- Se define la variable lrecInterm_typ que se utilizará como cursor.
		
		Dim lrecInterm_typ As eRemoteDB.Execute
		Dim lclsInterm_typ As Interm_typ
		
		On Error GoTo Find_err
		
		lrecInterm_typ = New eRemoteDB.Execute
		
		'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
		
		With lrecInterm_typ
			.StoredProcedure = "reaInterm_typ_a"
			If .Run Then
				Do While Not .EOF
					lclsInterm_typ = New Interm_typ
					lclsInterm_typ.nStatusInstance = eRemoteDB.Constants.intNull
					lclsInterm_typ.nInterTyp = .FieldToClass("nInterTyp")
					lclsInterm_typ.sDescript = .FieldToClass("sDescript")
					lclsInterm_typ.sParticin = .FieldToClass("sParticin")
					lclsInterm_typ.sShort_des = .FieldToClass("sShort_des")
					lclsInterm_typ.sStatregt = .FieldToClass("sStatregt")
					lclsInterm_typ.nTyp_acco = .FieldToClass("nTyp_Acco")
					lclsInterm_typ.sInd_FECU = .FieldToClass("sInd_FECU")
					lclsInterm_typ.sGen_certif = .FieldToClass("sGen_certif")
					Call Add(lclsInterm_typ)
					
					'UPGRADE_NOTE: Object lclsInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsInterm_typ = Nothing
					.RNext()
				Loop 
				Find = True
			End If
		End With
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInterm_typ = Nothing
	End Function
	
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Interm_typ
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
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






