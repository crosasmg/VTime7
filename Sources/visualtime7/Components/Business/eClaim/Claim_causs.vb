Option Strict Off
Option Explicit On
Public Class Claim_causs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_causs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxBranch As Integer
	
	'**% Add: Adds a new instance of the Claim_caus class to the collection
	'% Add: Añade una nueva instancia de la clase Claim_caus a la colección
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCausecod As Integer, ByVal sClaimTyp As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal sPartial_loss As String, ByVal sTotal_loss As String) As Claim_caus
		'create a new object
		
		Dim objNewMember As Claim_caus
		objNewMember = New Claim_caus
		
		With objNewMember
			.nBranch = nBranch
			.nProduct = nProduct
			.nCausecod = nCausecod
			.sClaimTyp = sClaimTyp
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.sPartial_loss = sPartial_loss
			.sTotal_loss = sTotal_loss
		End With
		
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	
	'**% Find: Restores a collection of objects of Claim_caus type
	'% Find: Devuelve una coleccion de objetos de tipo Claim_caus
	'------------------------------------------------------------
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'**- Variable definition lrecClaim_caus that will be used as a cursor
		'- Se define la variable lrecClaim_caus que se utilizará como cursor.
		Dim lrecClaim_caus As eRemoteDB.Execute
		
		Dim lstrPartial_loss As String
		Dim lstrTotal_loss As String
		
		lrecClaim_caus = New eRemoteDB.Execute
		
		If nAuxBranch = nBranch And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's movements
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecClaim_caus
				.StoredProcedure = "reaClaim_caus_a"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If Not .Run Then
					Find = False
					nAuxBranch = eRemoteDB.Constants.intNull
				Else
					nAuxBranch = nBranch
					Find = True
					Do While Not .EOF
						If .FieldToClass("sClaimtyp") = "1" Then
							lstrPartial_loss = "1"
							lstrTotal_loss = "0"
						ElseIf .FieldToClass("sClaimtyp") = "2" Then 
							lstrPartial_loss = "0"
							lstrTotal_loss = "1"
						Else
							lstrPartial_loss = "1"
							lstrTotal_loss = "1"
						End If
						
						Call Add(nBranch, nProduct, .FieldToClass("nCausecod"), .FieldToClass("sClaimtyp"), .FieldToClass("sDescript"), .FieldToClass("sShort_des"), .FieldToClass("sStatregt"), lstrPartial_loss, lstrTotal_loss)
						.RNext()
					Loop 
				End If
			End With
		End If
		lrecClaim_caus = Nothing
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Claim_caus
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
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	Private Sub Class_Initialize_Renamed()
		nAuxBranch = eRemoteDB.Constants.intNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






