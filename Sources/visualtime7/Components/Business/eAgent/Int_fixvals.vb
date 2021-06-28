Option Strict Off
Option Explicit On
Public Class Int_fixvals
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Int_fixvals.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private dAuxEffecdate As Date
	
	
	'**% Add: adds a new instance to the Int_fixval class to the collection
	'% Add: Añade una nueva instancia de la clase Int_fixval a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nCode As Integer, ByVal dEffecdate As Date, ByVal sDescript As String, ByVal nAmount As Double, ByVal nRate As Double, ByVal nUsercode As Integer) As Int_fixval
		'create a new object
		
		Dim objNewMember As Int_fixval
		objNewMember = New Int_fixval
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nCode = nCode
			.dEffecdate = dEffecdate
			.sDescript = sDescript
			.nAmount = nAmount
			.nRate = nRate
			.nUsercode = nUsercode
		End With
		
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'**% FindMAG008: Restores a collection of objects of Int_fixval type
	'% FindMAG008: Devuelve una coleccion de objetos de tipo Int_fixval
	'------------------------------------------------------------
	Public Function Find(ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'**- Variable definition lrecInt_fixval that will be used as a cursor.
		'- Se define la variable lrecInt_fixval que se utilizará como cursor.
		Dim lrecInt_fixval As eRemoteDB.Execute
		
		lrecInt_fixval = New eRemoteDB.Execute
		On Error GoTo Find_Err
		
		If dAuxEffecdate = dEffecdate And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's transactions
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecInt_fixval
				.StoredProcedure = "reaInt_fixval_a"
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If Not .Run Then
					Find = False
					dAuxEffecdate = dtmNull
				Else
					dAuxEffecdate = dEffecdate
					Find = True
					Do While Not .EOF
						Call Add(eRemoteDB.Constants.intNull, .FieldToClass("nCode"), dEffecdate, .FieldToClass("sDescript"), .FieldToClass("nAmount"), .FieldToClass("nRate"), .FieldToClass("nUsercode"))
						.RNext()
					Loop 
				End If
			End With
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecInt_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInt_fixval = Nothing
		On Error GoTo 0
	End Function
	
	'**%Update: This method is in charge of making the table "Int_fixval" update for each element of the collection
	'%Update. este metodo se encarga de realizar la actualizacion de la tabla Int_fixval,
	'%por cada elemento de la coleccion.
	Public Function Update() As Boolean
		Dim lclsInt_fixval As Int_fixval
		For	Each lclsInt_fixval In mCol
			Select Case lclsInt_fixval.nStatusInstance
				
				'**+ Add
				'+Agregar
				
				Case 1
					Update = lclsInt_fixval.Update()
					'**+ Update
					'+Actualizar
					
				Case 2
					Update = lclsInt_fixval.Update()
					'**+ Delete
					'+ Eliminar
					
				Case 3
					Update = lclsInt_fixval.Delete()
			End Select
			If Update = False Then
				Exit For
			End If
		Next lclsInt_fixval
	End Function
	
	'*** Item: Restores an element of the collection (according to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Int_fixval
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
	
	'*** NewEnum: Allows to enumerate the collection for using it in a cycle For Each...Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Deletes an element form the collection
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		dAuxEffecdate = dtmNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the delete of an instance of the collection
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






