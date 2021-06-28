Option Strict Off
Option Explicit On
Public Class Doc_req_clis
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Doc_req_clis.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'% Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Doc_req_cli) As Doc_req_cli
		If objClass Is Nothing Then
			objClass = New Doc_req_cli
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & Count + 1)
			
		End With
		
		Add = objClass
	End Function
	
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Doc_req_cli
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: enumera los elementos dentro de la colección
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
	
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Find: Lee los datos de la tabla
	Public Function Find(ByVal nEval As Double, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lintAction As Object
		Dim lrecreaDoc_req_cli As eRemoteDB.Execute
		Dim lclsDoc_req_cli As Doc_req_cli
		
		On Error GoTo reaDoc_req_cli_Err
		lrecreaDoc_req_cli = New eRemoteDB.Execute
		
		With lrecreaDoc_req_cli
			.StoredProcedure = "reaDoc_req_cli"
			.Parameters.Add("nEval", nEval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsDoc_req_cli = New Doc_req_cli
					lclsDoc_req_cli.nEval = .FieldToClass("nEval")
					lclsDoc_req_cli.nId = .FieldToClass("nId")
					lclsDoc_req_cli.nTypedoc = .FieldToClass("nTypedoc")
					lclsDoc_req_cli.nStatusdoc = .FieldToClass("nStatusdoc")
					lclsDoc_req_cli.sDescript = .FieldToClass("sDescript")
					lclsDoc_req_cli.dDocreq = .FieldToClass("dDocreq")
					lclsDoc_req_cli.dDocrec = .FieldToClass("dDocrec")
					lclsDoc_req_cli.dDocdate = .FieldToClass("dDocdate")
					lclsDoc_req_cli.dExpirdat = .FieldToClass("dExpirdat")
					lclsDoc_req_cli.nCapital = .FieldToClass("nCapital")
					lclsDoc_req_cli.nNotenum = .FieldToClass("nNotenum")
					lclsDoc_req_cli.nUsercode = .FieldToClass("nUsercode")
					lclsDoc_req_cli.dDateto = .FieldToClass("dDateto")
					lclsDoc_req_cli.dDatefree = .FieldToClass("dDatefree")
					lclsDoc_req_cli.sRequest = .FieldToClass("sRequest")
					lclsDoc_req_cli.sRequire = .FieldToClass("sRequire")
					
					Call Add(lclsDoc_req_cli)
					'UPGRADE_NOTE: Object lclsDoc_req_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDoc_req_cli = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaDoc_req_cli_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaDoc_req_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDoc_req_cli = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: elimina la colección
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






