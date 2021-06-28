Option Strict Off
Option Explicit On
Public Class Tab_winFins
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	'**%Add: adds a new instance of the "Tab_winFin" class to the collection
	'%Add: Añade una nueva instancia de la clase "Tab_winFin" a la colección
	Public Function Add(ByVal nTratypec As Integer, ByVal nSequence As Integer, ByVal sCodispl As String, ByVal dCompdate As Date, ByVal sDefaulti As String, ByVal sRequire As String, ByVal nUsercode As Integer) As Tab_winFin
		
		'create a new object
		Dim objNewMember As Tab_winFin
		objNewMember = New Tab_winFin
		
		
		With objNewMember
			.nTratypec = nTratypec
			.nSequence = nSequence
			.sCodispl = sCodispl
			.dCompdate = dCompdate
			.sDefaulti = sDefaulti
			.sRequire = sRequire
			.nUsercode = nUsercode
		End With
		
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**%Find: This method fills the collection with records from the table "Tab_winFin" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Tab_winFin" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal nTratypec As Integer, ByVal nSequence As Integer) As Boolean
		Dim lrecreaTab_winfin As eRemoteDB.Execute
		Dim lclsTab_winfin As Tab_winFin
		
		lrecreaTab_winfin = New eRemoteDB.Execute
		lclsTab_winfin = New Tab_winFin
		
		'**+Stored procedure parameters definition 'insudb.reaTab_winfin'
		'**+Data of 06/08/2001 03:29:31 p.m.
		'Definición de parámetros para stored procedure 'insudb.reaTab_winfin'
		'Información leída el 08/06/2001 03:29:31 p.m.
		
		With lrecreaTab_winfin
			.StoredProcedure = "reaTab_winfin"
			.Parameters.Add("nTratypec", nTratypec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsTab_winfin = Add(.FieldToClass("nTratypec"), nSequence, .FieldToClass("sCodispl"), .FieldToClass("dCompdate"), String.Empty, .FieldToClass("sRequire"), .FieldToClass("nUsercode"))
					
					
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaTab_winfin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_winfin = Nothing
		'UPGRADE_NOTE: Object lclsTab_winfin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_winfin = Nothing
	End Function
	
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_winFin
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
	
	
	'**% FindTab_winfin: Return the information of the wndows related to financing contract processes.
	'% FindTab_winfin: Devuelve la información de las ventanas relacionadas con el proceso de financiamiento.
	Public Function FindTab_winfin(ByVal nTratypec As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim mintTratypec As Object = New Object
        '**-Declare the variable that determinate the result of the function (True/False).
        '- Se declara la variable que determina el resultado de la funcion (True/False).

        Dim lrecReaTab_winfin_fi As eRemoteDB.Execute
		Dim lclsTab_winfin As Tab_winFin
		Dim lintIndex As Integer
		
		On Error GoTo FindTab_winfin_Err
		
		lrecReaTab_winfin_fi = New eRemoteDB.Execute
		
		If mintTratypec <> nTratypec Or lblnFind Then
			
			mintTratypec = nTratypec
			
			'**+ Parameters definition for the stored procedure 'insudb.reaTab_winfin_fi'
			'**+ Data read on 09/13/2001 02:41:12 p.m.
			
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_winfin_fi'
			'+ Información leída el 13/09/2001 02:41:12 p.m.
			
			With lrecReaTab_winfin_fi
				.StoredProcedure = "reaTab_winfin_fi"
				
				.Parameters.Add("nTratypec", nTratypec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsTab_winfin = New eFinance.Tab_winFin
						lclsTab_winfin.nTratypec = nTratypec
						lclsTab_winfin.sExist = .FieldToClass("sExist")
						lclsTab_winfin.sCodispl = .FieldToClass("sCodispl")
						lclsTab_winfin.sDescript = .FieldToClass("sDescript")
						lclsTab_winfin.nSequence = .FieldToClass("nSequence")
						lclsTab_winfin.sDefaulti = .FieldToClass("sDefaulti")
						lclsTab_winfin.sRequire = .FieldToClass("sRequire")
						lclsTab_winfin.nIndex = lintIndex
						
						Call AddTab_winfin(lclsTab_winfin)
						
						'UPGRADE_NOTE: Object lclsTab_winfin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_winfin = Nothing
						lintIndex = lintIndex + 1
						.RNext()
					Loop 
					
					.RCloseRec()
					FindTab_winfin = True
				Else
					FindTab_winfin = False
				End If
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecReaTab_winfin_fi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_winfin_fi = Nothing
		
FindTab_winfin_Err: 
		If Err.Number Then
			FindTab_winfin = False
		End If
		
	End Function
	
	'**%AddTab_winfin: Adds a new instance of the "Tab_winFin" class to the collection.
	'%AddTab_winfin: Añade una nueva instancia de la clase "Tab_winFin" a la colección
	Public Function AddTab_winfin(ByVal objElement As Object) As Tab_winFin
		Dim objNewMember As Tab_winFin
		objNewMember = objElement
		mCol.Add(objNewMember)
		
		'**+Return the creates object
		'+ Retorna el objeto creado
		
		AddTab_winfin = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
End Class






