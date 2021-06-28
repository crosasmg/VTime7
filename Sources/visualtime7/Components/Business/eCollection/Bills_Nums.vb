Option Strict Off
Option Explicit On
Public Class Bills_Nums
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Bills_Nums.cls                           $%'
	'% $Author:: Nvaplat19                                  $%'
	'% $Date:: 25/08/03 6:46p                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'% Find: Permite cargar en la colección los datos de la tabla bills_num
	Public Function Find(ByVal nInsur_area As Integer, ByVal sBillType As String) As Boolean
		
		Dim lreaBills_Num As eRemoteDB.Execute
		
		lreaBills_Num = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'Definición de parámetros para stored procedure 'insudb.REABILLS_NUMAREATYPE'
		With lreaBills_Num
			.StoredProcedure = "REABILLS_NUMAREATYPE"
			' Parametros de entrada a la StoreProcedure
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
                    Call Add(.FieldToClass("nInitNumb"), _
                             .FieldToClass("nEndNumb"), _
                             .FieldToClass("nLastBill"), _
                             .FieldToClass("dLastClosed"), _
                             .FieldToClass("dCompDate"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lreaBills_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaBills_Num = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: añade un nuevo elemento a la colección
    Public Function Add(ByVal nInitnumb As Double, _
                        ByVal nEndnumb As Double, _
                        ByVal nLastbill As Double, _
                        ByVal dLastclosed As Date, _
                        ByVal dCompdate As Date) As Bills_Num
        'create a new object
        Dim objNewMember As Bills_Num
        objNewMember = New Bills_Num

        On Error GoTo Add_err

        'set the properties passed into the method
        With objNewMember
            .nInitnumb = nInitnumb
            .nEndnumb = nEndnumb
            .nLastbill = nLastbill
            .dLastclosed = dLastclosed
            .dCompdate = dCompdate
        End With
        mCol.Add(objNewMember, "T_F" & nInitnumb)

        '    If Len(sKey) = 0 Then
        '        mCol.Add objNewMember
        '    Else
        '        mCol.Add objNewMember, sKey
        '    End If

        'return the object created
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

Add_err:
        On Error GoTo 0

    End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Bills_Num
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
End Class






