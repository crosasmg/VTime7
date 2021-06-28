Option Strict Off
Option Explicit On
Public Class User_jobss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: User_jobss.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:19p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase TCover a la colección
	Public Function Add(ByVal objClass As User_jobs) As User_jobs
		
		If objClass Is Nothing Then
			objClass = New User_jobs
		End If
		
		With objClass
			mCol.Add(objClass, "UJ" & .nJob)
		End With
		
		'return the object created
		Add = objClass
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As User_jobs
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
	
	'%Find: Busca las tareas del usuario
	Public Function Find() As Boolean
		Dim lrecreaUser_jobs As eRemoteDB.Execute
		Dim lclsUser_jobs As User_jobs
		
		On Error GoTo Find_Err
		
		lrecreaUser_jobs = New eRemoteDB.Execute
		
		'+ Definición de Stored Procedure reaUser_jobs al 03-08-2002 17:28:39
		With lrecreaUser_jobs
			.StoredProcedure = "reaUser_jobs"
			If .Run Then
				Do While Not .EOF
					lclsUser_jobs = New User_jobs
					lclsUser_jobs.nJob = .FieldToClass("Job")
					lclsUser_jobs.dNext_date = .FieldToClass("Next_date")
					lclsUser_jobs.sWhat = .FieldToClass("What")
					Call Add(lclsUser_jobs)
					'UPGRADE_NOTE: Object lclsUser_jobs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsUser_jobs = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaUser_jobs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaUser_jobs = Nothing
		On Error GoTo 0
	End Function
End Class






