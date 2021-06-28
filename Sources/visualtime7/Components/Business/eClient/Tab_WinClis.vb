Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Tab_Winclis_NET.Tab_Winclis")> Public Class Tab_Winclis
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_WinClis.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'% Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tab_Wincli) As Tab_Wincli
		If objClass Is Nothing Then
			objClass = New Tab_Wincli
		End If
		
		With objClass
			mCol.Add(objClass, .sType_Clie & .sType_seq & .nSequence & .nIndex)
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	'% Update: recorre la colección y actualiza los datos en la tabla
	Public Function Update() As Boolean
		Dim lclsTab_wincli As Tab_Wincli
		Update = True
		For	Each lclsTab_wincli In mCol
			With lclsTab_wincli
				Update = .Update()
			End With
		Next lclsTab_wincli
	End Function
	
	'% Find: Busca las ventanas asociadas a la secuencia de clientes
	Public Function Find(ByVal sType_Clie As String, ByVal sType_seq As String) As Boolean
		
		'- Se define variable para realizar operaciones a la BD
		Dim lrecreaTab_wincli_a As eRemoteDB.Execute
		Dim lclsTab_wincli As Tab_Wincli
		Dim lintIndex As Integer
		
		On Error GoTo reaTab_wincli_a_Err
		
		lrecreaTab_wincli_a = New eRemoteDB.Execute
		
		With lrecreaTab_wincli_a
			.StoredProcedure = "reaTab_wincli_a"
			.Parameters.Add("sType_clie", sType_Clie, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_seq", sType_seq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTab_wincli = New Tab_Wincli
					lclsTab_wincli.sType_Clie = sType_Clie
					lclsTab_wincli.sType_seq = sType_seq
					lclsTab_wincli.sExist = .FieldToClass("sExist")
					lclsTab_wincli.sCodispl = .FieldToClass("sCodispl")
					lclsTab_wincli.sDescript = .FieldToClass("sDescript")
					lclsTab_wincli.nSequence = .FieldToClass("nSequence")
					lclsTab_wincli.sDefaulti = .FieldToClass("sDefaulti")
					lclsTab_wincli.sRequire = .FieldToClass("sRequire")
					lclsTab_wincli.nIndex = lintIndex
					Call Add(lclsTab_wincli)
					'UPGRADE_NOTE: Object lclsTab_wincli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_wincli = Nothing
					lintIndex = lintIndex + 1
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaTab_wincli_a_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_wincli_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_wincli_a = Nothing
		On Error GoTo 0
	End Function
	
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Wincli
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
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






