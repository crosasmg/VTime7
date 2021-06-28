Option Strict Off
Option Explicit On
Public Class Margin_Adjs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Margin_Adjs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:13p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private llngInsur_area As Integer
	Private ldtmInitdate As Date
	Private ldblIdtable As Double
	Private ldblIdrec As Double
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Margin_Adj) As Margin_Adj
		If objClass Is Nothing Then
			objClass = New Margin_Adj
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nInsur_area & Format(.dInitdate, "yyyyMMdd") & .nIdtable & .nIdrec & .nMovement)
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Margin_Adj
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
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
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal nIdtable As Double, ByVal nIdrec As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaMargin_Adj As eRemoteDB.Execute
		Dim lclsMargin_Adj As Margin_Adj
		
		On Error GoTo Find_Err
		Find = True
		
		If llngInsur_area <> nInsur_area Or ldtmInitdate <> dInitdate Or ldblIdtable <> nIdtable Or ldblIdrec <> nIdrec Or lblnFind Then
			lrecReaMargin_Adj = New eRemoteDB.Execute
			'+Definición de parámetros para stored procedure 'ReaMargin_Adj_a'
			With lrecReaMargin_Adj
				.StoredProcedure = "ReaMargin_Adj_a"
				.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dInitdate", dInitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIdtable", nIdtable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIdrec", nIdrec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					llngInsur_area = nInsur_area
					ldtmInitdate = dInitdate
					ldblIdtable = nIdtable
					ldblIdrec = nIdrec
					Do While Not .EOF
						lclsMargin_Adj = New Margin_Adj
						lclsMargin_Adj.nInsur_area = .FieldToClass("nInsur_area")
						lclsMargin_Adj.dInitdate = .FieldToClass("dInitdate")
						lclsMargin_Adj.nIdtable = .FieldToClass("nIdtable")
						lclsMargin_Adj.nIdrec = .FieldToClass("nIdrec")
						lclsMargin_Adj.nMovement = .FieldToClass("nMovement")
						lclsMargin_Adj.nAdjustamoloc = .FieldToClass("nAdjustamoloc")
						lclsMargin_Adj.nAdjustamoori = .FieldToClass("nAdjustamoori")
						lclsMargin_Adj.sDescript = .FieldToClass("sDescript")
						lclsMargin_Adj.dCompdate = .FieldToClass("dCompdate")
						Call Add(lclsMargin_Adj)
						.RNext()
						'UPGRADE_NOTE: Object lclsMargin_Adj may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsMargin_Adj = Nothing
					Loop 
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaMargin_Adj may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaMargin_Adj = Nothing
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
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






