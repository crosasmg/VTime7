Option Strict Off
Option Explicit On
Public Class Tarif_val_cols
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tarif_val_cols.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclsTarif_val_col As Tarif_val_col) As Tarif_val_col
		mCol.Add(lclsTarif_val_col)
		
		'+ Devolver el objeto creado
		Add = lclsTarif_val_col
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tarif_val_col
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
	
	'%Find : Esta función se encarga de de buscar la colección de la tabla tarif_value
	Public Function Find_Value(ByVal nId_table As Integer) As Boolean
		Dim lrecreaTarif_value As eRemoteDB.Execute
		Dim lclsTarif_value As Tarif_val_col
		
		On Error GoTo reaTarif_value_Err
		
		lrecreaTarif_value = New eRemoteDB.Execute
		
		With lrecreaTarif_value
			.StoredProcedure = "InsDP8002pkg.reaTarif_value"
			.Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsTarif_value = New Tarif_val_col
					lclsTarif_value.nId_table = nId_table
					lclsTarif_value.nRow = .FieldToClass("nRow")
					lclsTarif_value.nRate = .FieldToClass("nRate")
					lclsTarif_value.nAmount = .FieldToClass("nAmount")
					lclsTarif_value.nType_tar = .FieldToClass("nType_tar")
					Call Add(lclsTarif_value)
					'UPGRADE_NOTE: Object lclsTarif_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTarif_value = Nothing
					.RNext()
				Loop 
				Find_Value = True
			Else
				Find_Value = False
			End If
		End With
		
reaTarif_value_Err: 
		If Err.Number Then
			Find_Value = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTarif_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTarif_value = Nothing
	End Function
	
	'%Find : Esta función se encarga de de buscar la colección de la tabla tarif_val_col
	Public Function Find(ByVal nId_table As Integer, ByVal nRow As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTarif_val_col As eRemoteDB.Execute
		Dim lclsTarif_val_col As Tarif_val_col
		
		On Error GoTo reaTarif_val_col_Err
		
		lrecreaTarif_val_col = New eRemoteDB.Execute
		
		With lrecreaTarif_val_col
			.StoredProcedure = "InsDP8002pkg.reaTarif_val_col"
			.Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsTarif_val_col = New Tarif_val_col
					lclsTarif_val_col.nId_table = nId_table
					lclsTarif_val_col.nId_column = .FieldToClass("nId_column")
					lclsTarif_val_col.dEffecdate = .FieldToClass("dEffecdate")
					lclsTarif_val_col.nRow = .FieldToClass("nRow")
					lclsTarif_val_col.sValue = .FieldToClass("sValue")
					lclsTarif_val_col.nValue = .FieldToClass("nValue")
					lclsTarif_val_col.dValue = .FieldToClass("dValue")
					lclsTarif_val_col.dNulldate = .FieldToClass("dNulldate")
					Call Add(lclsTarif_val_col)
					'UPGRADE_NOTE: Object lclsTarif_val_col may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTarif_val_col = Nothing
					.RNext()
				Loop 
				Find = True
			Else
				Find = False
			End If
		End With
		
reaTarif_val_col_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTarif_val_col may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTarif_val_col = Nothing
	End Function
End Class






