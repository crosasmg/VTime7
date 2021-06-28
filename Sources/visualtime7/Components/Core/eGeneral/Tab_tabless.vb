Option Strict Off
Option Explicit On
Public Class Tab_tabless
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_tabless.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	'local variable to hold collection
	Private mCol As Collection
	
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
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_tables
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

    '**% Add: Adds the Numerator record to collection
    '% Add: Añade los Registros de Tab_tables a la coleccion
    Public Sub Add(ByVal sTab_code As String, ByVal nCount_item As Integer, ByVal sCode_item As String, ByVal sDesc_item As String, ByVal nCount_tabl As Integer, ByVal sDescript As String, ByVal sDs_select As String, ByVal sQ_value As String, ByVal nUsercode As Integer, ByVal sShowNum As String, ByVal sInitQuery As String, ByVal sIndSp As String, ByVal sKey As String)
        'create a new object
        Dim objNewMember As Tab_tables
        objNewMember = New Tab_tables
        With objNewMember
            .sTab_code = sTab_code
            .nCount_item = nCount_item
            .sCode_item = sCode_item
            .sDesc_item = sDesc_item
            .nCount_tabl = nCount_tabl
            .sDescript = sDescript
            .sDs_select = sDs_select
            .sQ_value = sQ_value
            .nUsercode = nUsercode
            .sShowNum = sShowNum
            .sInitQuery = sInitQuery
            .sIndSp = sIndSp
            .sKey = sKey
        End With
        'Insert the object in collection using KEY = tab_tablesX
        mCol.Add(objNewMember, "tab_tables" & sTab_code)
        'Free the memory from objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Sub
    '**% Remove:
    '% Remove:
    Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Find: select from the numerator table the record based on a condition and an indicator
	'%Find: Seleciona de la tabla tab_tables los Registros Basados en una Condision y un stab_code
	Public Function Find(ByVal sTab_code As String) As Boolean
		'**- Variable definition for the execution and the handle of the SP
		'-Se define la variable para la ejecución y manejo del SP
		
		Dim lrecreatab_tables As eRemoteDB.Execute
		On Error GoTo Find_err
		lrecreatab_tables = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.reaTab_tables'
		'Información leída el 14/02/2002 11:31:14 a.m.
		With lrecreatab_tables
			.StoredProcedure = "reaTab_tables"
			If sTab_code = String.Empty Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("stab_code", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("stab_code", Trim(sTab_code), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If .Run Then
				While Not .EOF
					Call Add(.FieldToClass("sTab_code"), .FieldToClass("nCount_item"), .FieldToClass("sCode_item"), .FieldToClass("sDesc_item"), .FieldToClass("nCount_tabl"), .FieldToClass("sDescript"), .FieldToClass("sDs_select"), .FieldToClass("sQ_value"), .FieldToClass("nUsercode"), .FieldToClass("sShowNum"), .FieldToClass("sInitQuery"), .FieldToClass("sIndSp"), .FieldToClass("sKey"))
					.RNext()
				End While
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreatab_tables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_tables = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
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






