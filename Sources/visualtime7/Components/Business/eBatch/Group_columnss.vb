Option Strict Off
Option Explicit On
Public Class Group_columnss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Group_columnss.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:39p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'-Variable de la coleccion
	Private mCol As Collection
	
	Private mintSheet As Integer
	Private mstrTable As String
	Private mblnCharge As Boolean
	
	'%Add: Agrega un elemento a la colección
	Public Function Add(ByVal lclsGroup_columns As Group_columns) As Group_columns
		With lclsGroup_columns
			mCol.Add(lclsGroup_columns, "CT" & .nSheet & .sField & .sComment & .sColumnName & .nOrder & .sRequire & .sValuesList & .nIdRec & .sTable)
		End With
		'+ Devuelve el objeto creado.
		Add = lclsGroup_columns
		'UPGRADE_NOTE: Object lclsGroup_columns may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroup_columns = Nothing
	End Function
	
	'% Find: busca los datos correspondientes a las columnas asociadas a una hoja
	Public Function FindMCA006(ByVal nSheet As Integer) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lclsGroup_columns As Group_columns
		
		On Error GoTo FindMCA006_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "ReaGroup_columns"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsGroup_columns = New Group_columns
					With lclsGroup_columns
						.nSheet = lrecTime.FieldToClass("nSheet", 0)
						.sField = lrecTime.FieldToClass("sField", "")
						.sComment = lrecTime.FieldToClass("sComment", "")
						.sColumnName = lrecTime.FieldToClass("sColumnName", "")
						.nOrder = lrecTime.FieldToClass("nOrder", 0)
						.sRequire = lrecTime.FieldToClass("sRequire", 0)
						.sValuesList = lrecTime.FieldToClass("sValuesList", String.Empty)
						.nIdRec = lrecTime.FieldToClass("nIdrec", 0)
						.sTable = lrecTime.FieldToClass("sTable", String.Empty)
					End With
					Call Add(lclsGroup_columns)
					'UPGRADE_NOTE: Object lclsGroup_columns may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsGroup_columns = Nothing
					.RNext()
				Loop 
				mblnCharge = True
			Else
				mblnCharge = False
			End If
		End With
		
		FindMCA006 = mblnCharge
		
FindMCA006_Err: 
		If Err.Number Then
			FindMCA006 = CShort(FindMCA006) + CDbl(Err.Description)
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		
	End Function
	
	'%Item: devuelve un elemento de la colección (según índice, o llave)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Group_columns
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: controla la creación de la instancia del objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: controla la destrucción de la instancia del objeto de la clase
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






