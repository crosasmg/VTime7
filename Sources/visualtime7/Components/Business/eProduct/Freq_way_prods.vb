Option Strict Off
Option Explicit On
Public Class Freq_way_prods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Freq_way_prods.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales a la colección
	Private mCol As Collection
	
	'%Add: Agrega una nueva instancia de la clase a la colección
	Public Function Add(ByRef objClass As Freq_way_prod) As Freq_way_prod
		'+ Se crea el objeto
		If objClass Is Nothing Then
			objClass = New Freq_way_prod
		End If
		
		With objClass
			mCol.Add(objClass, .nBranch & .nProduct & .nWay_pay & .nPayFreq & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'+ Se retorna el objeto creado
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
		
	End Function
	
	'%Find: Lee los datos de la tabla para la transacción DP578
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaFreq_way_prod_a As eRemoteDB.Execute
		Dim lclsFreq_way_prod As Freq_way_prod
		lrecReaFreq_way_prod_a = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+Definición de parámetros para stored procedure 'ReaFreq_way_prod_a'
		'+Información leída el 07/05/2002
		With lrecReaFreq_way_prod_a
			.StoredProcedure = "ReaFreq_way_prod_DP578"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsFreq_way_prod = New Freq_way_prod
					lclsFreq_way_prod.nExist = .FieldToClass("bExist")
					lclsFreq_way_prod.nBranch = .FieldToClass("nBranch")
					lclsFreq_way_prod.nProduct = .FieldToClass("nProduct")
					lclsFreq_way_prod.nWay_pay = .FieldToClass("nWay_pay")
					lclsFreq_way_prod.nPayFreq = .FieldToClass("nPayFreq")
					lclsFreq_way_prod.dEffecdate = .FieldToClass("dEffecdate")
					lclsFreq_way_prod.nCurrency = .FieldToClass("nCurrency")
					lclsFreq_way_prod.nPre_issue = .FieldToClass("nPre_issue")
					lclsFreq_way_prod.nPre_amend = .FieldToClass("nPre_amend")
					lclsFreq_way_prod.nQprem = .FieldToClass("nQprem")
					lclsFreq_way_prod.sIva = .FieldToClass("sIva")
                    lclsFreq_way_prod.nLimit_ExcTax = .FieldToClass("nLimit_ExcTax")
                    lclsFreq_way_prod.sNo_sell = .FieldToClass("sNo_sell")
					Call Add(lclsFreq_way_prod)
					.RNext()
					'UPGRADE_NOTE: Object lclsFreq_way_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFreq_way_prod = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaFreq_way_prod_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaFreq_way_prod_a = Nothing
		On Error GoTo 0
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Freq_way_prod
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
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






