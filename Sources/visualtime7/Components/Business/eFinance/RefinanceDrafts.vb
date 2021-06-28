Option Strict Off
Option Explicit On
Public Class RefinanceDrafts
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: RefinanceDrafts.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 15/04/04 5:02p                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+local variable to hold collection
	Private mCol As Collection
	
	'**%Add: adds a new instance of the "RefinanceDraft" class to the collection
	'%Add: Añade una nueva instancia de la clase "RefinanceDraft" a la colección
	Public Function Add(ByVal objClass As RefinanceDraft) As RefinanceDraft
		If objClass Is Nothing Then
			objClass = New RefinanceDraft
		End If
		
		With objClass
			mCol.Add(objClass, .nContrat & .nContrat_d & .nDraft_d)
		End With
		'+ Entrega objeto creado
		Add = objClass
	End Function
	
	
	'**%Find: This method fills the collection with records from the table "ReFinan_dra_Contrat" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "ReFinan_dra_Contrat" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal nContrat As Double, Optional ByVal lblnFind As Boolean = False, Optional ByVal bOnlyToVal As Boolean = False) As Boolean
        Dim Contrat As Object = New Object
        Dim lrecreaReFinan_dra_Contrat As eRemoteDB.Execute
		Dim lclsRefinanceDraft As RefinanceDraft
		
		Dim nTotamount As Double
		Dim nTotCommission As Double
		
		If Contrat <> nContrat Or lblnFind Then
			
			lrecreaReFinan_dra_Contrat = New eRemoteDB.Execute
			
			'Definición de parámetros para stored procedure 'insudb.reaReFinan_dra_Contrat'
			'Información leída el 11/06/2000 05:36:45 p.m.
			
			With lrecreaReFinan_dra_Contrat
				.StoredProcedure = "reaReFinan_dra_Contrat"
				.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					'+Si no sólo se debe validar existencia, se debe carga la coleccion
					If Not bOnlyToVal Then
						Contrat = nContrat
						Do While Not .EOF
							lclsRefinanceDraft = New RefinanceDraft
							lclsRefinanceDraft.nStatInstanc = FinanceDraft.eStatusInstance.eftQuery
							lclsRefinanceDraft.sCurrency = .FieldToClass("sDescript")
							lclsRefinanceDraft.sCliename = .FieldToClass("sCliename")
							lclsRefinanceDraft.sClient = .FieldToClass("sClient")
							lclsRefinanceDraft.sStatregt = .FieldToClass("sStatregt")
							lclsRefinanceDraft.sStat_finpr = .FieldToClass("sStat_finpr")
							lclsRefinanceDraft.nDraft_d = .FieldToClass("nDraft_d")
							lclsRefinanceDraft.nPremium = .FieldToClass("nPremium")
							lclsRefinanceDraft.dExpirdat = .FieldToClass("dExpirdat")
							lclsRefinanceDraft.nExchange = .FieldToClass("nExchange")
							lclsRefinanceDraft.dStartdate = .FieldToClass("dStartdate")
							lclsRefinanceDraft.nCurrency = .FieldToClass("nCurrency")
							lclsRefinanceDraft.nContrat = nContrat
							lclsRefinanceDraft.nContrat_d = .FieldToClass("nContrat_d")
							lclsRefinanceDraft.nCommission = .FieldToClass("nCommission")
							lclsRefinanceDraft.nBranch = .FieldToClass("nBranch")
							lclsRefinanceDraft.nOpt_draft = .FieldToClass("nOpt_draft")
							
							nTotamount = nTotamount + lclsRefinanceDraft.nPremium
							lclsRefinanceDraft.nTotamount = nTotamount
							nTotCommission = nTotCommission + lclsRefinanceDraft.nCommission
							lclsRefinanceDraft.nTotCommission = nTotCommission
							
							Call Add(lclsRefinanceDraft)
							'UPGRADE_NOTE: Object lclsRefinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsRefinanceDraft = Nothing
							.RNext()
						Loop 
					End If
					.RCloseRec()
					
					Find = True
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
		'UPGRADE_NOTE: Object lrecreaReFinan_dra_Contrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaReFinan_dra_Contrat = Nothing
	End Function
	
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As RefinanceDraft
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
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
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
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
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






