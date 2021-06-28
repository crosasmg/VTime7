Option Strict Off
Option Explicit On
Public Class tab_am_lims
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: tab_am_lims.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcoltab_am_lim As Collection
	
	'Add: Agrega un elemento a la colección.
	Public Function Add(ByRef lclsTab_am_lim As tab_am_lim) As tab_am_lim
		With lclsTab_am_lim
			mcoltab_am_lim.Add(lclsTab_am_lim, "MAM" & .nBranch & .nProduct & .dEffecdate & .nCover & .nPay_concep & .sIllness & .nLimit_per)
		End With
		
		'+ Devuelve el elemento creado
		Add = lclsTab_am_lim
		'UPGRADE_NOTE: Object lclsTab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_lim = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'tab_am_lim'
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal nModulec As Integer) As Boolean
		Dim lclsTab_am_lim As tab_am_lim
		Dim lrecTab_am_lim As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecTab_am_lim = New eRemoteDB.Execute
		
		With lrecTab_am_lim
			.StoredProcedure = "reaTab_am_lim_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					lclsTab_am_lim = New tab_am_lim
					lclsTab_am_lim.nBranch = nBranch
					lclsTab_am_lim.nProduct = nProduct
					lclsTab_am_lim.dEffecdate = dEffecdate
					lclsTab_am_lim.nCover = nCover
					lclsTab_am_lim.nPay_concep = nPay_concep
					lclsTab_am_lim.sIllness = .FieldToClass("sillness")
					lclsTab_am_lim.nLimit_per = .FieldToClass("nLimit_per")
					
					Call Add(lclsTab_am_lim)
					'UPGRADE_NOTE: Object lclsTab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_am_lim = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_lim = Nothing
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As tab_am_lim
		Get
			Item = mcoltab_am_lim.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcoltab_am_lim.Count()
		End Get
	End Property
	
	'* NewEnum: permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcoltab_am_lim._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcoltab_am_lim.GetEnumerator
	End Function
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcoltab_am_lim.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: controla la creación de la instancia del objeto de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcoltab_am_lim = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla la destrucción de la instancia del objeto de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcoltab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcoltab_am_lim = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






