Option Strict Off
Option Explicit On
Public Class Supervis_commiss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Supervis_commiss.cls                     $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 25/09/03 18:39                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxInterTyp As Integer
	Private dAuxEffecdate As Date
	Public nCount As Integer
	
	
	'**% Add: Adds a new instance of the Supervis_commis class to the collection
	'% Add: Añade una nueva instancia de la clase Supervis_commis a la colección
	Public Function Add(ByVal objNewMember As Supervis_commis) As Supervis_commis
		If objNewMember Is Nothing Then
			objNewMember = New Supervis_commis
		End If
		
		With objNewMember
			mCol.Add(objNewMember, "a" & .nBranch & .nProduct & .nInterTyp & .nLower_level & .nTypPort & .dEffecdate)
		End With
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% FindMAG573: Restores a collection of objects of Supervis_commis type
	'% FindMAG573: Devuelve una coleccion de objetos de tipo Supervis_commis
	'------------------------------------------------------------
	Public Function Find(ByVal nInterTyp As Integer, ByVal dEffecdate As Date, ByVal nRow As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'**- Variable definition lrecSupervis_commis that will be used as a cursor
		'- Se define la variable lrecSupervis_commis que se utilizará como cursor.
		Dim lrecSupervis_commis As eRemoteDB.Execute
		Dim lclsSupervis_commis As Supervis_commis
		
		On Error GoTo Find_Err
		
		lrecSupervis_commis = New eRemoteDB.Execute
		
		If nAuxInterTyp = nInterTyp And dAuxEffecdate = dEffecdate And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's movements
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecSupervis_commis
				.StoredProcedure = "reaSupervis_commis_a"
				.Parameters.Add("nInterTyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				nCount = 1
				
				If Not .Run(True) Then
					Find = False
					nAuxInterTyp = eRemoteDB.Constants.intNull
					dAuxEffecdate = dtmNull
				Else
					nAuxInterTyp = nInterTyp
					dAuxEffecdate = dEffecdate
					Find = True
					
					Do While Not .EOF And nCount < nRow
						nCount = nCount + 1
						.RNext()
					Loop 
					
					Do While Not .EOF And nCount < nRow + 50
						nCount = nCount + 1
						
						lclsSupervis_commis = New Supervis_commis
						lclsSupervis_commis.nBranch = .FieldToClass("nBranch")
						lclsSupervis_commis.sBranchDes = .FieldToClass("sBranch")
						lclsSupervis_commis.nProduct = .FieldToClass("nProduct")
						lclsSupervis_commis.sProductDes = .FieldToClass("sProduct")
						lclsSupervis_commis.nInterTyp = nInterTyp
						lclsSupervis_commis.sInterTypDes = .FieldToClass("sInterTyp_L")
						lclsSupervis_commis.nLower_level = .FieldToClass("nLower_level")
						lclsSupervis_commis.dEffecdate = .FieldToClass("dEffecdate")
						lclsSupervis_commis.nCommiss = .FieldToClass("nCommiss")
						lclsSupervis_commis.nUsercode = .FieldToClass("nUsercode")
						lclsSupervis_commis.nTypPort = .FieldToClass("nTypPort")
						
						Call Add(lclsSupervis_commis)
						'UPGRADE_NOTE: Object lclsSupervis_commis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsSupervis_commis = Nothing
						.RNext()
					Loop 
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecSupervis_commis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSupervis_commis = Nothing
		On Error GoTo 0
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Supervis_commis
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
		nAuxInterTyp = eRemoteDB.Constants.intNull
		dAuxEffecdate = dtmNull
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






