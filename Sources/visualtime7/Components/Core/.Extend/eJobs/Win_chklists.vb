Option Strict Off
Option Explicit On
Public Class Win_chklists
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Win_chklists.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:19p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Win_chklist) As Win_chklist
		If objClass Is Nothing Then
			objClass = New Win_chklist
		End If
		
		With objClass
			mCol.Add(objClass, .sCodispl & .nId)
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Win_chklist
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
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
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal sCodispl As Object) As Boolean
		Dim lrecReaWin_chklist As eRemoteDB.Execute
		Dim lclsWin_chklist As Win_chklist
		
		On Error GoTo Find_Err
		lrecReaWin_chklist = New eRemoteDB.Execute
		'+Definición de parámetros para stored procedure 'ReaWin_chklist_a'
		With lrecReaWin_chklist
			.StoredProcedure = "ReaWin_chklist_a"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsWin_chklist = New Win_chklist
					lclsWin_chklist.sCodispl = .FieldToClass("sCodispl")
					lclsWin_chklist.nModules = .FieldToClass("nModules")
					lclsWin_chklist.sComments = .FieldToClass("sComments")
					lclsWin_chklist.sObject_type = .FieldToClass("sObject_type")
					lclsWin_chklist.sObject_name = .FieldToClass("sObject_name")
					lclsWin_chklist.sPath = .FieldToClass("sPath")
					lclsWin_chklist.nId = .FieldToClass("nId")
					lclsWin_chklist.nSequence = .FieldToClass("nSequence")
					lclsWin_chklist.sAction = .FieldToClass("sAction")
					lclsWin_chklist.sDescript = .FieldToClass("sDescript")
					Call Add(lclsWin_chklist)
					.RNext()
					'UPGRADE_NOTE: Object lclsWin_chklist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsWin_chklist = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaWin_chklist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaWin_chklist = Nothing
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
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






