Option Strict Off
Option Explicit On
Public Class Disex_int_ds
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Disex_int_ds.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxEco_sche As Integer
	Private dAuxEffecdate As Date
	
	
	'**% Add: Adds a new instance of the Disex_int_d class to the collection
	'% Add: Añade una nueva instancia de la clase Disex_int_d a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nEco_sche As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sDisexpri As String, ByVal nPercent As Double) As Disex_int_d
		'create a new object
		'                        ByVal sProductDes As String,
		
		Dim objNewMember As Disex_int_d
		objNewMember = New Disex_int_d
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nEco_sche = nEco_sche
			.dEffecdate = dEffecdate
			.nBranch = nBranch
			.nProduct = nProduct
			.sDisexpri = sDisexpri
			.nPercent = nPercent
			
		End With
		
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'**% FindMAG003: Restores a collection of ocjects of the Disex_int_d class
	'% FindMAG003: Devuelve una coleccion de objetos de tipo Disex_int_d
	'------------------------------------------------------------
	Public Function Find(ByVal nEco_sche As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'**-Variable definition lrecDisex_int_d that will be used as a cursor.
		'- Se define la variable lrecDisex_int_d que se utilizará como cursor.
		Dim lrecDisex_int_d As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecDisex_int_d = New eRemoteDB.Execute
		
		If nAuxEco_sche = nEco_sche And dAuxEffecdate = dEffecdate And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's movements
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecDisex_int_d
				.StoredProcedure = "reaDisex_int_d_a"
				.Parameters.Add("nEco_sche", nEco_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run(True) Then
					Find = False
					nAuxEco_sche = eRemoteDB.Constants.intNull
					dAuxEffecdate = dtmNull
				Else
					nAuxEco_sche = nEco_sche
					dAuxEffecdate = dEffecdate
					Find = True
					Do While Not .EOF
						Call Add(eRemoteDB.Constants.intNull, nEco_sche, dEffecdate, .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("sDisexpri"), .FieldToClass("nPercent"))
						
						.RNext()
					Loop 
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecDisex_int_d may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDisex_int_d = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'**%Update: This method updates the records of the collection in the table "Disex_int_ds"
	'%Update: Permite actualizar los registros de la colección en la tabla "Disex_int_ds"
	Public Function Update() As Boolean
		Dim lclsDisex_int_d As Disex_int_d
		For	Each lclsDisex_int_d In mCol
			Select Case lclsDisex_int_d.nStatusInstance
				
				'**+ Add
				'+Agregar
				
				Case 1
					Update = lclsDisex_int_d.Update()
					'**+ Update
					'+Actualizar
					
				Case 2
					Update = lclsDisex_int_d.Update()
					'**+ Delete
					'+ Eliminar
					
				Case 3
					Update = lclsDisex_int_d.Delete()
			End Select
			If Update = False Then
				Exit For
			End If
		Next lclsDisex_int_d
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Disex_int_d
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
		nAuxEco_sche = eRemoteDB.Constants.intNull
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






