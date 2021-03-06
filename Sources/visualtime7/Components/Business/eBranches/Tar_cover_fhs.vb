Option Strict Off
Option Explicit On
Public Class Tar_cover_fhs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_cover_fhs.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlosCamposLlave As Object
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colecci?n
	Public Function Add(ByRef lclsTarCoverfh As Tar_cover_fh) As Tar_cover_fh
		mCol.Add(lclsTarCoverfh)
		'+Devolver el objeto creado
		Add = lclsTarCoverfh
	End Function
	
	'%Item: Devuelve un elemento de la colecci?n (segun ?ndice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_cover_fh
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el n?mero de elementos que posee la colecci?n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colecci?n para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colecci?n
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Find : Esta funci?n se encarga de de buscar la colecci?n de datos de acuerdo
	'%a el ramo, producto, modulo, cobertura y fecha
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTar_cover_fh As eRemoteDB.Execute
		Dim lclstar_cover_fh As Tar_cover_fh
		
		On Error GoTo lrecreaTar_cover_fh_Err
		
		lrecreaTar_cover_fh = New eRemoteDB.Execute
		
		'+
		'+ Definici?n de store procedure reaTar_cover_fh al 04-04-2002 13:14:42
		'+
		With lrecreaTar_cover_fh
			.StoredProcedure = "reaTar_cover_fh"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclstar_cover_fh = New Tar_cover_fh
					lclstar_cover_fh.nBranch = nBranch
					lclstar_cover_fh.nProduct = nProduct
					lclstar_cover_fh.nModulec = nModulec
					lclstar_cover_fh.nCover = nCover
					lclstar_cover_fh.nCurrency = nCurrency
					lclstar_cover_fh.dEffecdate = dEffecdate
					lclstar_cover_fh.nConstcat = .FieldToClass("nConstcat")
					lclstar_cover_fh.nProvince = .FieldToClass("nProvince")
					lclstar_cover_fh.nMunicipality = .FieldToClass("nMunicipality")
					lclstar_cover_fh.nCap_initial = .FieldToClass("nCap_initial")
					lclstar_cover_fh.nCap_end = .FieldToClass("nCap_end")
					lclstar_cover_fh.nRate = .FieldToClass("nRate")
					lclstar_cover_fh.nPremium = .FieldToClass("nPremium")
					lclstar_cover_fh.nUsercode = .FieldToClass("nUsercode")
					Call Add(lclstar_cover_fh)
					'UPGRADE_NOTE: Object lclstar_cover_fh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclstar_cover_fh = Nothing
					.RNext()
				Loop 
				
			Else
				Find = False
			End If
		End With
		
lrecreaTar_cover_fh_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaTar_cover_fh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_cover_fh = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Controla la creaci?n de una instancia de la colecci?n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucci?n de una instancia de la colecci?n
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






