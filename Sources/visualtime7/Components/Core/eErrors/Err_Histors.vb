Option Strict Off
Option Explicit On
Public Class Err_Histors
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Err_Histors.cls                          $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 22/08/03 16:07                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	Private mlngErrornum As Integer
	
	
	
	Public Function Add(ByVal nErrorNum As Integer, ByVal nConsecut As Integer, ByVal dDate As Date, ByVal sHour As String, ByVal sUser As String, ByVal sStat_error As String, ByVal sHour_user As String, ByVal nDays_user As Integer, ByVal sDescript As String) As err_histor
		
		'create a new object
		Dim objNewMember As err_histor
		
		If Not IsIDEMode Then
		End If
		objNewMember = New err_histor
		
		With objNewMember
			.nErrorNum = nErrorNum
			.nConsecut = nConsecut
			.dDate = dDate
			.sHour = sHour
			.sUser = sUser
			.sStat_error = sStat_error
			.sHour_user = sHour_user
			.nDays_user = nDays_user
			.sDescript = sDescript
		End With
		
		mCol.Add(objNewMember, "ErrHist" & nErrorNum & nConsecut)
		
		Add = objNewMember
		
		objNewMember = Nothing
		
		Exit Function
	End Function
	
	'%Find:Levanta el Recordset con todos los registros del Historico de un error
	Public Function Find(ByVal nErrorNum As Integer) As Boolean
		Dim lrecreaErr_Histor As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecreaErr_Histor = New eRemoteDB.Execute
		
        mCol = Nothing
		mCol = New Collection
		
		With lrecreaErr_Histor
			.StoredProcedure = "reaErr_Histor"
            .Parameters.Add("NERRORNUM", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			
			If .Run Then
				Find = True
				While Not .EOF
					Call Add(.FieldToClass("nErrorNum"), .FieldToClass("nConsecut"), .FieldToClass("dDate"), .FieldToClass("sHour"), .FieldToClass("sUser"), .FieldToClass("sStat_error"), .FieldToClass("sHour_user"), .FieldToClass("nDays_user"), .FieldToClass("sDescript"))
					.RNext()
				End While
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lrecreaErr_Histor = Nothing
		
		Exit Function
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As err_histor
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
		End Get
	End Property
	
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
			Exit Property
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Err_Histors.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		If Not IsIDEMode Then
		End If
		
		mCol = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		If Not IsIDEMode Then
		End If
		
		mCol = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











