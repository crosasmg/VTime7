Option Strict Off
Option Explicit On
Public Class WinMessags
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	'**% Add: Adds the records to the win_message table
	'% Add: Añade los Registros de la tabla win_messag
	Public Function Add(ByVal sCodispl As String, ByVal nErrorNum As Integer, ByVal sAction_err As String, ByVal sErrorTyp As String, ByVal nLevel As Integer, ByVal sStatregt As String, ByVal nUsercode As Integer) As WinMessag
        Dim dCompdate As Object = New Object

        'create a new object
        Dim objNewMember As WinMessag
		objNewMember = New WinMessag
		
		With objNewMember
			.sCodispl = sCodispl
			.nErrorNum = nErrorNum
			.sAction_err = sAction_err
			.dCompdate = dCompdate
			.sErrorTyp = sErrorTyp
			.nLevel = nLevel
			.sStatregt = sStatregt
			.nUsercode = nUsercode
		End With
		
		mCol.Add(objNewMember, "WinMessag" & nErrorNum)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As WinMessag
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
	
	'**% Find: This routine searches a record in the win_messag table.
	'%Find: Esta rutina Busca un registro en la tabla win_messag.
	Public Function Find(ByVal sCodispl As String) As Boolean
		
		'**- Variable definition for the treatment of the parameters and the run of the SP.
		'-Se definbe la variable para el tratamiento de los parámetros y la corrida del SP
		
		Dim lrecWinMessage As eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		lrecWinMessage = New eRemoteDB.Execute
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		mCol = New Collection
		
		Find = True
		
		With lrecWinMessage
			.StoredProcedure = "reaWinMessag"
			.Parameters.Add("scodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				While Not .EOF
					Call Add(.FieldToClass("sCodispl"), .FieldToClass("nErrornum"), .FieldToClass("sAction_err"), .FieldToClass("sErrortyp"), .FieldToClass("nLevel"), .FieldToClass("sStatregt"), .FieldToClass("nUsercode"))
					
					.RNext()
				End While
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecWinMessage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWinMessage = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
End Class






