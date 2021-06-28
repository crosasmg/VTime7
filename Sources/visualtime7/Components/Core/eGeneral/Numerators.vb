Option Strict Off
Option Explicit On
Public Class Numerators
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	'**% Add: Adds the Numerator record
	'% Add: Añade los Registros de Numerator
	
	'%Add: Añade un Registro a la tabla Numerators
    Public Function Add(ByRef lclsNumerator As Numerator) As Numerator

        'set the properties passed into the method
        mCol.Add(lclsNumerator)

        'return the object created
        Add = lclsNumerator

        lclsNumerator = Nothing

    End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Numerator
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
	
	'**% Find: select from the numerator table the record based on a condition and an indicator
	'%Find: Seleciona de la tabla numerator los Registros Basados en una Condsion y un Indicador
	Public Function Find(ByVal strWhere As String, ByVal nIndicator As Integer) As Boolean
		
		'**- Variable definition for the execution and the handle of the SP
		'-Se define la variable para la ejecución y manejo del SP
		
        Dim ltempReaNumerator As eRemoteDB.Execute
        Dim lclsNumeratorItem As Numerator
		
		On Error GoTo Find_err
		
		ltempReaNumerator = New eRemoteDB.Execute
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		mCol = New Collection
		
		With ltempReaNumerator
			.StoredProcedure = "reaNumerator1PKG.reaNumerator1"
			If strWhere = String.Empty Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("strWhere", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("strWhere", strWhere, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("nIndicator", nIndicator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                While Not .EOF

                    lclsNumeratorItem = New Numerator

                    lclsNumeratorItem.ntipo = 0
                    lclsNumeratorItem.sShort_des = .FieldToClass("sShort_des")
                    lclsNumeratorItem.sShort_des2 = .FieldToClass("sShort_des2")
                    lclsNumeratorItem.nInitial = .FieldToClass("nInitial")
                    lclsNumeratorItem.nEnd_num = .FieldToClass("nEnd_num")
                    lclsNumeratorItem.nLastnumb = .FieldToClass("nLastnumb")
                    lclsNumeratorItem.nTypenum = .FieldToClass("nTypenum")
                    lclsNumeratorItem.nOrd_num = .FieldToClass("nOrd_num")

                    Call Add(lclsNumeratorItem)

                    'UPGRADE_NOTE: Object lclsReval_factItem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsNumeratorItem = Nothing

                    .RNext()
                End While

                .RCloseRec()
                Find = True
            Else
                Find = False
            End If
		End With
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
End Class






