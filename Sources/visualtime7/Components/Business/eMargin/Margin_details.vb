Option Strict Off
Option Explicit On
Public Class Margin_details
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Margin_details.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:13p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales a la colección
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase a la colección
	Public Function Add(ByRef oMargin_detail As Margin_detail) As Margin_detail
		mCol.Add(oMargin_detail)
		Add = oMargin_detail
		'UPGRADE_NOTE: Object oMargin_detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		oMargin_detail = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Margin_detail
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
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'* Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: se obtienen los datos de la tabla
	Public Function Find(ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal nTableType As Integer, ByVal nSource As Integer, ByVal nClaimClass As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsMargin_detail As Margin_detail
		
		On Error GoTo Find_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaMargin_detail_all"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitDate", dInitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTableType", nTableType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimClass", nClaimClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsMargin_detail = New Margin_detail
					lclsMargin_detail.nBranch = .FieldToClass("nBranch")
					lclsMargin_detail.nProduct = .FieldToClass("nProduct")
					lclsMargin_detail.nTypeRec = .FieldToClass("nTypeRec")
					lclsMargin_detail.nCurrency = .FieldToClass("nCurrency")
					lclsMargin_detail.dValDate = .FieldToClass("dValDate")
					lclsMargin_detail.nModulec = .FieldToClass("nModulec")
					lclsMargin_detail.nCover = .FieldToClass("nCover")
					lclsMargin_detail.nSVSClass = .FieldToClass("nSVSClass")
					lclsMargin_detail.nInitialAmoOri = .FieldToClass("nInitialAmoOri")
					lclsMargin_detail.nAdjAmoOri = .FieldToClass("nAdjAmoOri")
					lclsMargin_detail.nInitialAmoLoc = .FieldToClass("nInitialAmoLoc")
					lclsMargin_detail.nAdjAmoLoc = .FieldToClass("nAdjAmoLoc")
					lclsMargin_detail.sStaDet = .FieldToClass("sStaDet")
					lclsMargin_detail.nExchange = .FieldToClass("nExchange")
					lclsMargin_detail.nAmountOri = lclsMargin_detail.nInitialAmoOri + lclsMargin_detail.nAdjAmoOri
					lclsMargin_detail.nAmountLoc = lclsMargin_detail.nAmountOri * lclsMargin_detail.nExchange
					lclsMargin_detail.nIdtable = .FieldToClass("nIdTable")
					lclsMargin_detail.nIdrec = .FieldToClass("nIdRec")
					lclsMargin_detail.nCountAdjust = .FieldToClass("nCountAdjust")
					Call Add(lclsMargin_detail)
					'UPGRADE_NOTE: Object lclsMargin_detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsMargin_detail = Nothing
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
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object lclsMargin_detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMargin_detail = Nothing
	End Function
End Class






