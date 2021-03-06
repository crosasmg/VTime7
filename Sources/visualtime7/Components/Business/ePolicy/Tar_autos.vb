Option Strict Off
Option Explicit On
Public Class Tar_autos
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_autos.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'% Add: A?ade una nueva instancia de la clase a la colecci?n
	Public Function Add(ByRef objClass As Tar_auto) As Tar_auto
		If objClass Is Nothing Then
			objClass = New Tar_auto
		End If
		
		With objClass
			mCol.Add(objClass, .nBranch & .nProduct & .nCurrency & .nModulec & .nCover & .dEffecdate.ToString("yyyyMMdd") & .nId)
		End With
		
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'% Find: Lee los datos de la tabla para la transacci?n MAU571
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal sVehcode As String) As Boolean
		Dim lrecReaTar_auto_a As eRemoteDB.Execute
		Dim lclsTar_auto As Tar_auto
		
		lrecReaTar_auto_a = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+Definici?n de par?metros para stored procedure 'ReaTar_auto_a'
		'+Informaci?n le?da el 05/03/02
		With lrecReaTar_auto_a
			.StoredProcedure = "ReaTar_auto_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTar_auto = New Tar_auto
					lclsTar_auto.nBranch = .FieldToClass("nBranch")
					lclsTar_auto.nProduct = .FieldToClass("nProduct")
					lclsTar_auto.nCurrency = .FieldToClass("nCurrency")
					lclsTar_auto.nModulec = .FieldToClass("nModulec")
					lclsTar_auto.sDesc_modulec = .FieldToClass("sDesc_modulec")
					lclsTar_auto.nCover = .FieldToClass("nCover")
					lclsTar_auto.sDesc_cover = .FieldToClass("sDesc_cover")
					lclsTar_auto.dEffecdate = .FieldToClass("dEffecdate")
					lclsTar_auto.nId = .FieldToClass("nId")
					lclsTar_auto.sVehcode = .FieldToClass("sVehcode")
					lclsTar_auto.nRate = .FieldToClass("nRate")
					lclsTar_auto.nPrem_fix = .FieldToClass("nPrem_fix")
					
					Call Add(lclsTar_auto)
					.RNext()
					'UPGRADE_NOTE: Object lclsTar_auto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTar_auto = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaTar_auto_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTar_auto_a = Nothing
		On Error GoTo 0
	End Function
	
	'% Item: Devuelve un elemento de la colecci?n (segun ?ndice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_auto
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Devuelve el n?mero de elementos que posee la colecci?n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Permite enumerar la colecci?n para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Elimina un elemento de la colecci?n
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creaci?n de una instancia de la colecci?n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucci?n de una instancia de la colecci?n
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






