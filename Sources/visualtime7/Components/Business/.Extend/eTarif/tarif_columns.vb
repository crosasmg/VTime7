Option Strict Off
Option Explicit On
Public Class tarif_columns
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: tarif_columns.cls                           $%'
	'% $Author:: Pmanzur                                    $%'
	'% $Date:: 9/08/03 1:39p                                $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	
	'-variable local de la coleccion
	Private mCol As Collection
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As tarif_column) As tarif_column
		If objClass Is Nothing Then
			objClass = New tarif_column
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nId_column & .sTable & .sColumn & .sName_col)
		End With
		
		'+Return the object created
		Add = objClass
		
	End Function
	
	'% Item: Recupera un item de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As tarif_column
		Get
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	'%Count: Retorna la cantidad de registros de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Enumardor para operación For..Each
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
			'
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: elimina un registro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		mCol.Remove(vntIndexKey)
		
	End Sub
	
	'%Find: Lee los datos de la tabla segun el campo sKey
	Public Function Find() As Boolean
		Dim lrectarif_column As Object
		
		Dim lclstarif_column As tarif_column
		
		lrectarif_column = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		On Error GoTo Find_Err
		
		'Set lrectarif_column = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		'+
		'+ Definición de store procedure reatarif_column al 09-13-2002 16:13:05
		'+
		With lrectarif_column
			.StoredProcedure = "reatarif_column"
			
			If .Run Then
				Find = Not .EOF
				Do While Not .EOF
					lclstarif_column = New tarif_column
					lclstarif_column.nId_column = .FieldToClass("nId_column")
					lclstarif_column.sTable = .FieldToClass("sTable")
					lclstarif_column.sColumn = .FieldToClass("sColumn")
					lclstarif_column.sName_col = .FieldToClass("sName_col")
					lclstarif_column.nData_type = .FieldToClass("nData_type")
					lclstarif_column.nSize = .FieldToClass("nSize")
					lclstarif_column.nDecimal = .FieldToClass("nDecimal")
					lclstarif_column.sData_type = .FieldToClass("sData_type")
					lclstarif_column.sTablefk = .FieldToClass("sTablefk")
					
					Call Add(lclstarif_column)
					'UPGRADE_NOTE: Object lclstarif_column may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclstarif_column = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrectarif_column may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectarif_column = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las varibales de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'%Find: Lee los datos de la tabla tarif_column asociados a una tabla logica de tarifa
	Public Function Find_ColTab(ByRef nId_Table As Object) As Boolean
		Dim lrectarif_column As Object
		
		Dim lclstarif_column As tarif_column
		
		lrectarif_column = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		On Error GoTo Find_ColTab_Err
		
		'+
		'+ Definición de store procedure reatarif_column al 09-13-2002 16:13:05
		'+
		With lrectarif_column
			.StoredProcedure = "reatarif_column_Table"
			.Parameters.Add("nId_table", nId_Table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_ColTab = Not .EOF
				Do While Not .EOF
					lclstarif_column = New tarif_column
					lclstarif_column.nId_column = .FieldToClass("nId_column")
					lclstarif_column.sTable = .FieldToClass("sTable")
					lclstarif_column.sColumn = .FieldToClass("sColumn")
					lclstarif_column.sName_col = .FieldToClass("sName_col")
					lclstarif_column.nData_type = .FieldToClass("nData_type")
					lclstarif_column.nSize = .FieldToClass("nSize")
					lclstarif_column.nDecimal = .FieldToClass("nDecimal")
					lclstarif_column.sData_type = .FieldToClass("sData_type")
					lclstarif_column.sTablefk = .FieldToClass("sTablefk")
					
					Call Add(lclstarif_column)
					'UPGRADE_NOTE: Object lclstarif_column may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclstarif_column = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find_ColTab = False
			End If
		End With
		
Find_ColTab_Err: 
		If Err.Number Then
			Find_ColTab = False
		End If
		'UPGRADE_NOTE: Object lrectarif_column may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectarif_column = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Class_Terminate: Libera objetos de la colección
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






