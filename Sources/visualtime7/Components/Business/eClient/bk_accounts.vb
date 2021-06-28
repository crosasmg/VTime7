Option Strict Off
Option Explicit On
Public Class bk_accounts
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: bk_accounts.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'- Se define una variable auxiliar para forzar la búsqueda de los datos en la tabla
	Private mAuxClient As String
	
	
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal sClient As String, ByVal nBankExt As Integer, ByVal sAccount As String, ByVal sStatregt As String, ByVal nTyp_acc As Integer, Optional ByVal sIndDirDebit As String = "", Optional ByVal sDeposit As String = "") As bk_account
		
		
		Dim objNewMember As bk_account
		objNewMember = New bk_account
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.sClient = sClient
			.nBankExt = nBankExt
			.sAccount = sAccount
			.sStatregt = sStatregt
			.nTyp_acc = nTyp_acc
			.sIndDirDebit = sIndDirDebit
			.sDeposit = sDeposit
		End With
		
		mCol.Add(objNewMember, "BK" & sClient & nBankExt & sAccount)
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% Item: Se usa para referenciar un elemento de la colección
	'------------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As bk_account
		Get
			'------------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Se usa para obtener el numero de elementos de la colección
	'------------------------------------------------------------
	Public ReadOnly Property Count() As Integer
		Get
			'------------------------------------------------------------
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Obtiene un item de la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'------------------------------------------------------------
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Se usa para remover elementos de la colección
	'------------------------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'------------------------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Crea la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'------------------------------------------------------------
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Destruye la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'------------------------------------------------------------
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: busca los datos correspondientes a un cliente
	Public Function Find(ByVal sClient As String) As Boolean
		Dim lrecreabk_account As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreabk_account = New eRemoteDB.Execute
		
		If sClient = mAuxClient Then
			Find = True
		Else
			With lrecreabk_account
				.StoredProcedure = "reabk_account"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBankExt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sAccount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						Call Add(1, sClient, .FieldToClass("nBankext"), .FieldToClass("sAccount"), .FieldToClass("sStatregt"), .FieldToClass("nTyp_acc"), .FieldToClass("sIndDirDebit"), .FieldToClass("sDeposit"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
					mAuxClient = sClient
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreabk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreabk_account = Nothing
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






