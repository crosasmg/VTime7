Option Strict Off
Option Explicit On
Public Class TDetail_pres
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: TDetail_pres.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 29/10/04 4:18p                               $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Local variable to hold collection
	Private mCol As Collection
	
	Private mdblManTotCapital As Double
	Private mdblManTotPremium As Double
	Private mdblManTotCommision As Double
	Private mdblManTotPremio As Double
	Private mblnManCalc As Boolean
	
	'**% Add: Adds a new instance of the TDetail_pre class to the collection
	'% Add: Añade una nueva instancia de la clase TDetail_pre a la colección
	Public Function Add(ByVal lclsTDetail_pre As TDetail_pre) As TDetail_pre
		mCol.Add(lclsTDetail_pre)
		
		Add = lclsTDetail_pre
		'UPGRADE_NOTE: Object lclsTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTDetail_pre = Nothing
	End Function
	
	'**% Find: Restores a collection of objetcs of the TDetail_pre type.
	'% Find: Devuelve una coleccion de objetos de tipo TDetail_pre
	Public Function FindManReceipt(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal nCurrency As Integer = 0, Optional ByVal nGroup_insu As Integer = 0, Optional ByVal sKey As String = "", Optional ByVal sReload As String = "", Optional ByVal nReceipt As Double = 0, Optional ByVal sAdjust As String = "", Optional ByVal nAdjReceipt As Double = 0, Optional ByVal nAdjAmount As Double = 0) As Boolean
		'**- Variable definition lrecinsreaCover_Disc_Pol
		'- Se define la variable lrecinsreaCover_Disc_Pol
		
		Dim lrecinsreaCover_Disc_Pol As eRemoteDB.Execute
		Dim lclsTDetail_pre As ePolicy.TDetail_pre
		
		On Error GoTo FindManReceipt_Err
		
		lrecinsreaCover_Disc_Pol = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.insreaCover_Disc_Pol'
		'+Definición de parámetros para stored procedure 'insudb.insreaCover_Disc_Pol'
		'**+ Information read on January 24,2001  11:40:30
		'+Información leída el 24/01/2001 11:40:30
		
		With lrecinsreaCover_Disc_Pol
			.StoredProcedure = "insreaCover_Disc_Pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_Insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddData", sReload, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdjust", sAdjust, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdjReceipt", nAdjReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdjAmount", nAdjAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				mblnManCalc = False
				Do While Not .EOF
					lclsTDetail_pre = New ePolicy.TDetail_pre
					lclsTDetail_pre.nItem = .FieldToClass("nCode")
					lclsTDetail_pre.sType_detai = .FieldToClass("sType_detai")
					lclsTDetail_pre.nType = CShort(lclsTDetail_pre.sType_detai)
					lclsTDetail_pre.sShort_des = .FieldToClass("sShort_des")
					lclsTDetail_pre.sCacalili = .FieldToClass("sCacalili")
					lclsTDetail_pre.nBill_item = .FieldToClass("nBill_item")
					lclsTDetail_pre.nBranch_est = .FieldToClass("nBranch_est")
					lclsTDetail_pre.nBranch_led = .FieldToClass("nBranch_led")
					lclsTDetail_pre.nBranch_rei = .FieldToClass("nBranch_rei")
					lclsTDetail_pre.sAddsuini = .FieldToClass("sAddsuini")
					lclsTDetail_pre.nModulec = .FieldToClass("nModulec")
					lclsTDetail_pre.sCommissi_i = .FieldToClass("sCommissi_i")
					lclsTDetail_pre.sAddtaxin = .FieldToClass("sAddtaxin")
					lclsTDetail_pre.sClient = .FieldToClass("sClient")
					lclsTDetail_pre.nCapital = .FieldToClass("nCapital")
					lclsTDetail_pre.sAddtax = .FieldToClass("sAddtax")
					lclsTDetail_pre.nCommission = .FieldToClass("nCommision")
					lclsTDetail_pre.nCommi_rate = .FieldToClass("nCommi_rate")
					lclsTDetail_pre.nPremiumA = .FieldToClass("nPremiumA")
					lclsTDetail_pre.nPremiumE = .FieldToClass("nPremiumE")
					lclsTDetail_pre.nPremium = .FieldToClass("nPremium")
					lclsTDetail_pre.nPrem_det = .FieldToClass("nPrem_det")
					lclsTDetail_pre.sPrem_det = .FieldToClass("sPrem_det")
					lclsTDetail_pre.nDet_premium = .FieldToClass("nDet_premium")
					lclsTDetail_pre.nDet_commision = .FieldToClass("nDet_commision")
					lclsTDetail_pre.nDet_commi_rate = .FieldToClass("nDet_commi_rate")
					
					Call Add(lclsTDetail_pre)
					'UPGRADE_NOTE: Object lclsTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTDetail_pre = Nothing
					
					.RNext()
				Loop 
				.RCloseRec()
				FindManReceipt = True
			End If
		End With
		
FindManReceipt_Err: 
		If Err.Number Then
			FindManReceipt = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsreaCover_Disc_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsreaCover_Disc_Pol = Nothing
		'UPGRADE_NOTE: Object lclsTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTDetail_pre = Nothing
	End Function
	
	'% inspreCA028_1: Se buscan los datos a mostrar en la transacción
	Public Sub inspreCA028_1(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date, ByVal sProc_data As String, ByVal nSessionId As String, ByVal nUsercode As Integer, ByVal nMainAction As Short)
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsTDetail_pre As TDetail_pre
		Dim lstrKey As String
		
		On Error GoTo inspreCA028_1_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		lstrKey = sKey(nUsercode, nSessionId, False)
		If nMainAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			sProc_data = "1"
		End If
		
		With lclsRemote
			.StoredProcedure = "insreaReceipt_CA028_1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProc_data", sProc_data, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					lclsTDetail_pre = New ePolicy.TDetail_pre
					lclsTDetail_pre.nItem = .FieldToClass("nCode")
					lclsTDetail_pre.sType_detai = .FieldToClass("sType_detai")
					lclsTDetail_pre.nType = CShort(lclsTDetail_pre.sType_detai)
					lclsTDetail_pre.sShort_des = .FieldToClass("sShort_des")
					lclsTDetail_pre.sCacalili = .FieldToClass("sCacalili")
					lclsTDetail_pre.nBill_item = .FieldToClass("nBill_item")
					lclsTDetail_pre.nBranch_est = .FieldToClass("nBranch_est")
					lclsTDetail_pre.nBranch_led = .FieldToClass("nBranch_led")
					lclsTDetail_pre.nBranch_rei = .FieldToClass("nBranch_rei")
					lclsTDetail_pre.sAddsuini = .FieldToClass("sAddsuini")
					lclsTDetail_pre.nModulec = .FieldToClass("nModulec")
					lclsTDetail_pre.sCommissi_i = .FieldToClass("sCommissi_i")
					lclsTDetail_pre.sAddtaxin = .FieldToClass("sAddtaxin")
					lclsTDetail_pre.sClient = .FieldToClass("sClient")
					lclsTDetail_pre.nCapital = .FieldToClass("nCapital")
					lclsTDetail_pre.sAddtax = .FieldToClass("sAddtax")
					lclsTDetail_pre.nCommission = .FieldToClass("nCommision")
					lclsTDetail_pre.nCommi_rate = .FieldToClass("nCommi_rate")
					lclsTDetail_pre.nPremiumA = .FieldToClass("nPremiumA")
					lclsTDetail_pre.nPremiumE = .FieldToClass("nPremiumE")
					lclsTDetail_pre.nPremium = .FieldToClass("nPremium")
					lclsTDetail_pre.nPremium = IIf(lclsTDetail_pre.nPremium = eRemoteDB.Constants.intNull, 0, lclsTDetail_pre.nPremium)
					lclsTDetail_pre.sPrem_det = .FieldToClass("sPrem_det")
					lclsTDetail_pre.nPrem_det = .FieldToClass("nPrem_det")
					lclsTDetail_pre.nAplic_code = .FieldToClass("nAplic_code")
					lclsTDetail_pre.nAplication = .FieldToClass("nAplication")
					lclsTDetail_pre.nId_Bill = .FieldToClass("nId_Bill")
					Call Add(lclsTDetail_pre)
					'UPGRADE_NOTE: Object lclsTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTDetail_pre = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
inspreCA028_1_Err: 
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object lclsTDetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTDetail_pre = Nothing
		On Error GoTo 0
	End Sub
	
	'**% Premio. This property restores the premium total of the records contained in the collection.
	'%Premio: Esta propiedad devuelve el total de las primas de los registros contenidos en la
	'%coleccion
	Public ReadOnly Property Premio() As Double
		Get
			If Not mblnManCalc Then
				Call insCalTotAmo()
			End If
			Premio = mdblManTotPremio
		End Get
	End Property
	
	'**% TotPremium: This property restores the premium total of the records containes in the collection.
	'%TotPremium: Esta propiedad devuelve el total de las primas de los registros contenidos en la
	'%coleccion
	Public ReadOnly Property TotPremium() As Double
		Get
			If Not mblnManCalc Then
				Call insCalTotAmo()
			End If
			TotPremium = mdblManTotPremium
		End Get
	End Property
	
	'**% Capital: This property restores the sum total of the records coantined in the collection.
	'%Capital: Esta propiedad devuelve el total del capital de los registros contenidos en la
	'%coleccion
	Public ReadOnly Property Capital() As Double
		Get
			If Not mblnManCalc Then
				Call insCalTotAmo()
			End If
			Capital = mdblManTotCapital
		End Get
	End Property
	
	'**% Premium: This property restores the total of the premium of the records in the collection.
	'%Premium: Esta propiedad devuelve el total de la prima de los registros contenidos en la
	'%coleccion
	Public ReadOnly Property Premium() As Double
		Get
			If Not mblnManCalc Then
				Call insCalTotAmo()
			End If
			Premium = mdblManTotPremium
		End Get
	End Property
	
	'**% Commission. This property restores the commission total of the records contained in the collection.
	'%Comisión: Esta propiedad devuelve el total de la comisión de los registros contenidos en la
	'%coleccion
	Public ReadOnly Property Commission() As Double
		Get
			If Not mblnManCalc Then
				Call insCalTotAmo()
			End If
			Commission = mdblManTotCommision
		End Get
	End Property
	'% sKey: Devuelve la llave de lectura del registro de recibo manual
	Public ReadOnly Property sKey(ByVal nUsercode As Integer, ByVal nSessionId As String, Optional ByVal bDelTmp As Boolean = True) As String
		Get
			Dim lclsGeneralFunctions As eGeneral.GeneralFunction
			Dim lstrKey As String
			
			On Error GoTo sKey_err
			
			'lstrKey = "MR" & CStr(nSessionId) & "-" & CStr(nUsercode)
			
			lclsGeneralFunctions = New eGeneral.GeneralFunction
			
			With lclsGeneralFunctions
				lstrKey = .getsKey(nUsercode)
			End With
			
			'    If bDelTmp Then
			'       Set lclsGeneralFunctions = New TDetail_pre
			'      Call lclsTDetail_pre.Delete(lstrKey)
			' End If
			
			sKey = lstrKey
			
sKey_err: 
			On Error GoTo 0
			'UPGRADE_NOTE: Object lclsGeneralFunctions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsGeneralFunctions = Nothing
		End Get
	End Property
	
	'*** Item: takes an element of the collection.
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TDetail_pre
		Get
			'**+ Used when referencing an element in the collection.
			'**+ vntIndexKey contains either the Index or Key to the collection,
			'**+ this is why it is declared as a Variant
			'**+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: counts the elements of the collection
	'* Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			'**+ Used when retrieving the number of elements in the collection.
			'**+ Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: enumerates the elements of the collection.
	'* NewEnum: enumera los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'**+This property allows you to enumerate this collection with the For...Each syntax
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% insCalTotAmo: This function is in charge of calculate the premium totals, commission and sum.
	'%insCalTotAmo. Esta funcion se encarga de calcular los totales de prima, comission y capital
	Private Sub insCalTotAmo()
		Dim lclsDetail_pre As TDetail_pre
		
		mdblManTotCapital = 0
		mdblManTotPremium = 0
		mdblManTotCommision = 0
		mdblManTotPremio = 0
		mblnManCalc = True
		For	Each lclsDetail_pre In mCol
			If lclsDetail_pre.nType <> 4 Then
				mdblManTotCapital = mdblManTotCapital + IIf(lclsDetail_pre.nCapital <> eRemoteDB.Constants.intNull, lclsDetail_pre.nCapital, 0)
				mdblManTotPremium = mdblManTotPremium + IIf(lclsDetail_pre.nPremium <> eRemoteDB.Constants.intNull, lclsDetail_pre.nPremium, 0) + lclsDetail_pre.nDet_premium
			End If
			If lclsDetail_pre.nPremium <> eRemoteDB.Constants.intNull And lclsDetail_pre.nPremium <> 0 Then
				mdblManTotPremio = mdblManTotPremio + (lclsDetail_pre.nPremium)
				If lclsDetail_pre.nCommi_rate <> eRemoteDB.Constants.intNull And lclsDetail_pre.nCommi_rate <> 0 Then
					mdblManTotCommision = mdblManTotCommision + (lclsDetail_pre.nPremium * lclsDetail_pre.nCommi_rate / 100)
				End If
				If lclsDetail_pre.nCommission <> eRemoteDB.Constants.intNull And lclsDetail_pre.nCommission <> 0 Then
					mdblManTotCommision = mdblManTotCommision + lclsDetail_pre.nCommission
				End If
			End If
			mdblManTotPremio = mdblManTotPremio + lclsDetail_pre.nDet_premium
			mdblManTotCommision = mdblManTotCommision + lclsDetail_pre.nDet_commision
			mdblManTotCommision = mdblManTotCommision + lclsDetail_pre.nDet_commi_rate
		Next lclsDetail_pre
	End Sub
	
	'*** Remove: remoes an element from the collection.
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'**+ Used when removing an element from the collection.
		'**+ vntIndexKey contains either the Index or Key, which is why
		'**+ it is declared as a Variant
		'**+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'*** Class_Initialize: controls the opening of the collection.
	'* Class_Initialize: controla la apertura de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'**+ Creates the collection when this class is created
		
		mCol = New Collection
		mblnManCalc = False
		mdblManTotCapital = 0
		mdblManTotPremium = 0
		mdblManTotCommision = 0
		mdblManTotPremio = 0
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*** Class_Terminate: controls the end of the collection.
	'* Class_Terminate: controla el fin de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'**+ Destroys collection when this class is terminated
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






