Option Strict Off
Option Explicit On
Public Class Tab_comrats
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_comrats.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxTable_cod As Integer
	Private nAuxCurrency As Integer
	Private sAuxType_infor As String
	Private dAuxEffecdate As Date
	
	'**% Add: Adds a new instance of the Tab_comrat class to the collection
	'% Add: Añade una nueva instancia de la clase Tab_comrat a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nTable_cod As Integer, ByVal nCurrency As Integer, ByVal sType_Infor As String, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPrem_init As Double, ByVal nComrate As Double, ByVal nPrem_end As Double, ByVal sProductDes As String, ByVal nUsercode As Integer) As Tab_comrat
		
		'+ Se crea un nuevo objeto
		
		Dim objNewMember As Tab_comrat
		objNewMember = New Tab_comrat
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nTable_cod = nTable_cod
			.nCurrency = nCurrency
			.sType_Infor = sType_Infor
			.dEffecdate = dEffecdate
			.nBranch = nBranch
			.nProduct = nProduct
			.nPrem_init = nPrem_init
			.nComrate = nComrate
			.nPrem_end = nPrem_end
			.sProductDes = sProductDes
			.nUsercode = nUsercode
		End With
		
		mCol.Add(objNewMember)
		
		'+ Retorna el objeto creado
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% Find: Devuelve una coleccion de objetos de tipo Tab_comrat
	Public Function Find(ByVal nTable_cod As Integer, ByVal nCurrency As Integer, ByVal sType_Infor As String, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**- Variable definition lrecTab_comrat that will be used as a cursor.
		'- Se define la variable lrecTab_comrat que se utilizará como cursor.
		Dim lrecTab_comrat As eRemoteDB.Execute
		
		lrecTab_comrat = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nAuxTable_cod = nTable_cod And nAuxCurrency = nCurrency And sAuxType_infor = sType_Infor And dAuxEffecdate = dEffecdate And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's transactions.
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecTab_comrat
				.StoredProcedure = "reaTab_comrat_a"
				.Parameters.Add("nTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If Not .Run Then
					Find = False
					nAuxTable_cod = eRemoteDB.Constants.intNull
					nAuxCurrency = eRemoteDB.Constants.intNull
					sAuxType_infor = strNull
					dAuxEffecdate = dtmNull
				Else
					nAuxTable_cod = nTable_cod
					nAuxCurrency = nCurrency
					sAuxType_infor = sType_Infor
					dAuxEffecdate = dEffecdate
					Find = True
					
					Do While Not .EOF
						Call Add(eRemoteDB.Constants.intNull, nTable_cod, nCurrency, sType_Infor, dEffecdate, .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPrem_init"), .FieldToClass("nComrate"), .FieldToClass("nPrem_end"), .FieldToClass("sDescript"), .FieldToClass("nUsercode"))
						.RNext()
					Loop 
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecTab_comrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_comrat = Nothing
	End Function
	
	'** Update: This function is in charge of calling the correspondent procedure
	'** to the action that is being dones.
	' Update: Esta función se encarga de llamar al procedimiento correspondiente
	' a la acción que se esté realizando.
	Public Function Update() As Boolean
		Dim lclsTab_comrat As Tab_comrat
		For	Each lclsTab_comrat In mCol
			Select Case lclsTab_comrat.nStatusInstance
				
				'**+ Add
				'+Agregar
				Case 1
					Update = lclsTab_comrat.Update()
					'**+ Update
					'+Actualizar
				Case 2
					Update = lclsTab_comrat.Update()
					
					'**+ Delete
					'+ Eliminar
				Case 3
					Update = lclsTab_comrat.Delete()
					
			End Select
			
			If Update = False Then
				Exit For
			End If
		Next lclsTab_comrat
	End Function
	
	'***Item: Restores an element to the collection (according to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_comrat
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	'*** Count: Restores the number of elements that the collection owns.
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Allows to enumerate a collection for using it in a cyle For Each...Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: Removes an element from the collection.
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection.
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAuxTable_cod = eRemoteDB.Constants.intNull
		nAuxCurrency = eRemoteDB.Constants.intNull
		sAuxType_infor = strNull
		dAuxEffecdate = dtmNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the destruction of an instance of the collection.
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






