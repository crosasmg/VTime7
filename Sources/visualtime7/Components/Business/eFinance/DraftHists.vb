Option Strict Off
Option Explicit On
Public Class DraftHists
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: DraftHists.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:25p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**+Local variable to hold the collection.
	'+ Variable local para contener colección.
	
	Private mCol As Collection
	
	'**-Variable definition. This varibles are going to be used to make the search
	'-Se definen las variables que se van a utilizar para la búsqueda.
	
	Private dStatDate As Date
	Private nTypeMov As Integer
	Private nCurrencyCont As Integer
	
	'**%Add: adds a new instance of the "DraftHist" class to the collection
	'%Add: Añade una nueva instancia de la clase "DraftHist" a la colección
	Public Function Add(ByRef nUsercode As Integer, ByRef nType As Integer, ByRef dStat_date As Date, ByRef sIntermei As String, ByRef nInterest As Double, ByRef nExpensive As Double, ByRef nDscto_pag As Double, ByRef nDraft As Integer, ByRef nCurrency As Integer, ByRef nContrat As Double, ByRef dCompdate As Date, ByRef nAmount As Double, ByRef nTransac As Integer, ByRef sDescript As String, ByRef mDescript As String) As DraftHist
		
		'**-Variable definition. This variable will hold the instance that is going to be added
		'- Se define la variable que contendrá la instancia a añadir
		Dim objNewMember As DraftHist
		objNewMember = New DraftHist
		
		With objNewMember
			.nUsercode = nUsercode
			.nAmount = nAmount
			.nType = nType
			.dStat_date = dStat_date
			.sIntermei = sIntermei
			.nInterest = nInterest
			.nExpensive = nExpensive
			.nDscto_pag = nDscto_pag
			.nDraft = nDraft
			.nCurrency = nCurrency
			.nContrat = nContrat
			.dCompdate = dCompdate
			.nTransac = nTransac
			.sDescript = sDescript
			.mDescript = mDescript
		End With
		
		mCol.Add(objNewMember, "D" & nDraft & nTransac)
		
		'**+ Returns the created object
		'+ Retorna el objeto creado.
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As DraftHist
		Get
			
			'**+It is used to make reference to an element from the collection, vntIndexKey contains the key or
			'**+the index of  the collection so it is declared as a variant variable. Syntax: Set foo = x.Item(xyz) or
			'**+ Set foo = x.Item(5)
			'+ Se usa al hacer referencia a un elemento de la colección vntIndexKey contiene el índice o
			'+ la clave de la colección, por lo que se declara como un Variant Syntax: Set foo = x.Item(xyz) or
			'+ Set foo = x.Item(5).
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'**+It is used to get the number of elements of the collection. Syntax: Debug.Print x.Count.
			'+ Se usa al obtener el número de elementos de la colección. Sintaxis: Debug.Print x.Count.
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'**+This property enumerates this collection with the syntax For...Each.
			'+ Esta propiedad permite enumerar esta colección con la sintaxis For...Each.
			'
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
		'**+It's used to delete an element of the collection, vntIndexKey contains thje index or the key so it
		'**+is declared as a Variant variable. Syntax: x.Remove(xyz).
		'+ Se usa al quitar un elemento de la colección vntIndexKey contiene el índice o la clave, por lo que se
		'+ declara como un Variant Sintaxis: x.Remove(xyz).
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'**+Creates the collection when the class is created
		'+ Crea la colección cuando se crea la clase.
		
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
		
		'**+Destroys the collection when the class is finished
		'+ Destruye la colección cuando se termina la clase.
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Draft_hist"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Draft_hist"
	Public Function Find(ByVal dStat_date As Date, Optional ByVal nType As Object = -1, Optional ByVal nCurrency As Object = -1, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecinsReaDraft_hist As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If dStat_date <> dStatDate Or nType <> nTypeMov Or nCurrency <> nCurrencyCont Or lblnFind Then
			
			lrecinsReaDraft_hist = New eRemoteDB.Execute
			'Definición de parámetros para stored procedure 'insudb.insReaDraft_hist'
			'Información leída el 06/10/1999 02:08:05 PM
			
			With lrecinsReaDraft_hist
				.StoredProcedure = "insReaDraft_hist"
				
				.Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If nType > 0 Then
					.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nType", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				If nCurrency > 0 Then
					.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				If .Run Then
					dStatDate = dStat_date
					nTypeMov = nType
					nCurrencyCont = nCurrency
					
					Do While Not .EOF
						Call Add(.FieldToClass("nUsercode"), .FieldToClass("nType"), .FieldToClass("dStat_date"), .FieldToClass("sIntermei"), .FieldToClass("nInterest"), .FieldToClass("nExpensive"), .FieldToClass("nDscto_pag"), .FieldToClass("nDraft"), .FieldToClass("nCurrency"), .FieldToClass("nContrat"), .FieldToClass("dCompdate"), .FieldToClass("nAmount"), .FieldToClass("nTransac"), .FieldToClass("sDescript"), .FieldToClass("mDescript"))
						.RNext()
					Loop 
					
					Find = True
					
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecinsReaDraft_hist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecinsReaDraft_hist = Nothing
		Else
			Find = True
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
	End Function
End Class






