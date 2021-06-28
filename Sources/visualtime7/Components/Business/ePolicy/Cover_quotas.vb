Option Strict Off
Option Explicit On
Public Class Cover_quotas
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cover_quotas.cls                         $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 18                                       $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'- Totales a mostrar en la ventana
	Public nTotalIVA As Double
	Public nTotalPremium As Double
	
	'% Add: se agrega un elemento en la colección
	Private Function Add(ByRef lclsCover_quota As Cover_quota) As Cover_quota
		With lclsCover_quota
			mCol.Add(lclsCover_quota)
		End With
		'+ Return the object created
		Add = lclsCover_quota
		'UPGRADE_NOTE: Object lclsCover_quota may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCover_quota = Nothing
	End Function
	
	'% calCover_quota: se realiza el cálculo para obtener la 'tasa tarifa' de cada cobertura
	'%                 asociada a la póliza
	Public Function calCover_quota(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nUsercode As Integer, ByVal sKey As String, ByVal sReloadPage As String) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lclsCover_quota As Cover_quota
		
		On Error GoTo calCover_quota_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.calCoverquotapkg.calCover_quota'
		'+Información leída el 07/01/2002 02:00:11 p.m.
		
		With lclsExecute
			.StoredProcedure = "inscalCoverQuotaPKG.inscalCover_Quota"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProctype", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRead", sReloadPage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					lclsCover_quota = New Cover_quota
					With lclsCover_quota
						.sCertype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.nGroup = nGroup
						.nModulec = nModulec
						.nRole = lclsExecute.FieldToClass("nRole")
						.nCover = lclsExecute.FieldToClass("nCover")
						.nCapital = lclsExecute.FieldToClass("nCapital")
						.nTaxIVA = lclsExecute.FieldToClass("nTaxIVA")
						.nTax = lclsExecute.FieldToClass("nTaxTarif")
						.nTaxOrig = lclsExecute.FieldToClass("nTaxTarifOrig")
						.nPremium = lclsExecute.FieldToClass("nPremium")
						.nPremiumOrig = lclsExecute.FieldToClass("nPremiumOrig")
						.nInsucount = IIf(lclsExecute.FieldToClass("nInsucount") = eRemoteDB.Constants.intNull, 0, lclsExecute.FieldToClass("nInsucount"))
						.nInsured = IIf(lclsExecute.FieldToClass("nInsured") = eRemoteDB.Constants.intNull, 0, lclsExecute.FieldToClass("nInsured"))
						.nTaxMar = lclsExecute.FieldToClass("nTaxMar")
						.nExcInsured = .nInsucount - .nInsured
						nTotalIVA = nTotalIVA + IIf(lclsExecute.FieldToClass("nAmountIVA") = eRemoteDB.Constants.intNull, 0, lclsExecute.FieldToClass("nAmountIVA"))
						nTotalPremium = nTotalPremium + IIf(.nPremium = eRemoteDB.Constants.intNull, 0, .nPremium)
					End With
					Call Add(lclsCover_quota)
					'UPGRADE_NOTE: Object lclsCover_quota may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCover_quota = Nothing
					.RNext()
				Loop 
				calCover_quota = True
			End If
		End With
		
calCover_quota_Err: 
		If Err.Number Then
			calCover_quota = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
		'UPGRADE_NOTE: Object lclsCover_quota may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCover_quota = Nothing
	End Function
	
	'% Find: se buscan los elementos asociados a una póliza
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lclsCover_quota As Cover_quota
		
		On Error GoTo Find_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCover_quota_A'
		'+ Información leída el 07/02/2002
		
		With lclsExecute
			.StoredProcedure = "reaCover_quota_A"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsCover_quota = New Cover_quota
					With lclsCover_quota
						.sCertype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.nGroup = nGroup
						.nModulec = nModulec
						.nRole = lclsExecute.FieldToClass("nRole")
						.nCover = lclsExecute.FieldToClass("nCover")
						.nCapital = lclsExecute.FieldToClass("nCapital")
						.nTaxIVA = lclsExecute.FieldToClass("nTaxIVA")
						.nTax = lclsExecute.FieldToClass("nTax")
						.nTaxMar = lclsExecute.FieldToClass("nTaxMar")
						.nPremium = lclsExecute.FieldToClass("nPremium")
						.nInsucount = lclsExecute.FieldToClass("nInsucount")
						.nInsured = lclsExecute.FieldToClass("nInsured")
						.nExcInsured = .nInsucount - .nInsured
						nTotalIVA = nTotalIVA + (.nCapital * .nTaxIVA) / 100
						nTotalPremium = nTotalPremium + .nPremium
					End With
					
					Call Add(lclsCover_quota)
					'UPGRADE_NOTE: Object lclsCover_quota may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCover_quota = Nothing
					.RNext()
				Loop 
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
		'UPGRADE_NOTE: Object lclsCover_quota may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCover_quota = Nothing
	End Function
	
	'* Item: se instancia un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cover_quota
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el Nro. de elementos que tiene la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: permite recorrer los elementos de la colección
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
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: se controla la creación de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nTotalIVA = 0
		nTotalPremium = 0
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: se controla la destrucción de la colección
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






