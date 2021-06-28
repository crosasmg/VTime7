Option Strict Off
Option Explicit On
Public Class Financ_Clis
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Financ_Clis.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Additional variable definition. This variable is used to force the search in the table
	'- Se define una variable auxiliar para forzar la búsqueda de los datos en la tabla
	
	Private sAuxClient As String
	'**% Add: Adds a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal sClient As String, ByVal dFinanDate As Date, ByVal nConcept As Integer, ByVal nUnits As Double, ByVal nNotenum As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nFinanStat As Integer, ByVal sConcept As String) As Financ_Cli
		Dim objNewMember As Financ_Cli
		
		objNewMember = New Financ_Cli
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.sClient = sClient
			.dFinanDate = dFinanDate
			.nConcept = nConcept
			.nUnits = nUnits
			.nNotenum = nNotenum
			.nCurrency = nCurrency
			.nAmount = nAmount
			.nFinanStat = nFinanStat
			.sConcept = sConcept
		End With
		
		mCol.Add(objNewMember, "FC" & sClient & dFinanDate & nConcept)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Update: Updates the table with the information that is in the collection
	'% Update: recorre la colección y actualiza los datos en la tabla
	Public Function Update() As Boolean
		Dim lclsFinanc_cli As Financ_Cli
		
		Update = True
		
		On Error GoTo Update_Err
		
		For	Each lclsFinanc_cli In mCol
			With lclsFinanc_cli
				If sAuxClient = String.Empty Then
					sAuxClient = .sClient
				End If
				Select Case .nStatusInstance
					Case 0
						Update = .Add
						.nStatusInstance = 1
					Case 2
						Update = .Update
					Case 3
						Update = .Delete
						mCol.Remove(("FC" & .sClient & .dFinanDate & .nConcept))
				End Select
			End With
		Next lclsFinanc_cli
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Find: Search for the data of a client
	'% Find: busca los datos correspondientes a un cliente
	Public Function Find(ByVal sClient As String) As Boolean
		Dim lrecreaFinanc_cli As eRemoteDB.Execute
		
		lrecreaFinanc_cli = New eRemoteDB.Execute
		On Error GoTo Find_Err
		
		If sClient = sAuxClient Then
			Find = True
		Else
			With lrecreaFinanc_cli
				.StoredProcedure = "reaFinanc_cli"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dFinanDate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nConcept", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						Call Add(1, sClient, .FieldToClass("dFinanDate"), .FieldToClass("nConcept"), .FieldToClass("nUnits"), .FieldToClass("nNotenum"), .FieldToClass("nCurrency"), .FieldToClass("nAmount"), .FieldToClass("nFinanStat"), .FieldToClass("sDescript"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
					sAuxClient = sClient
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaFinanc_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaFinanc_cli = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Item: Gets an element from the collection
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Financ_Cli
		Get
            Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% Count: Counts the quantity of elements of the collection
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: Enumerates the elements in the collection
	'% NewEnum: enumera los elementos dentro de la colección
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
	
	'**% Remove: Deletes an element of the collection
	'% Remove: elimina un elemento dentro de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        mCol.Remove(vntIndexKey)
    End Sub
	
	'**% Class_Initialize: Controls the opening of each instance of the collecion
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: deletes the collection
	'* Class_Terminate: elimina la colección
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






