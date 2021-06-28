Option Strict Off
Option Explicit On
Public Class Branchrs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Branchrs.cls                             $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 29/01/04 18.01                               $%'
	'% $Revision:: 30                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- La variable sManualMovType contiene el indicador de tipo de movimiento manual. Valores Posibles 0.-No tiene, 1.-Contratos, 3.-Facultativos
	Public sManualMovType As String
	
	
	'**% Add: Add a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nCurrency As Integer, ByVal nBranchRei As Integer, ByVal sAddReini As String, ByVal nCapital As Double, ByVal nPremium As Double, ByVal nCapital_max As Double, ByVal nType As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nChange As Integer, ByVal sHeapCode As String, ByVal nCapital_Rei As Double, ByVal nRest As Double, ByVal sCoverDesc As String, ByVal sCliename As String, Optional ByVal nNumber As Integer = 0, Optional ByVal sChangeDes As String = "", Optional ByVal sCurrDes As String = "", Optional ByVal sBranch_Reides As String = "", Optional ByVal sDigit As String = "", Optional ByVal sGridCovDesc As String = "", Optional ByVal sModuDesc As String = "", Optional ByVal nCapital_cov As Double = 0, Optional ByVal nReserve As Double = 0, Optional ByVal nClasific As Integer = 0) As Branchr
		'Create a new object
		
		Dim objNewMember As ePolicy.Branchr

        If nType = eRemoteDB.Constants.intNull Then
            Exit Function
        End If

        objNewMember = New ePolicy.Branchr
		
		'Set the properties passed into the method
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nCurrency = nCurrency
			.nBranchRei = nBranchRei
			.sAddReini = sAddReini
			.nCapital = nCapital_Rei
			.nCapital_Rei = nCapital_Rei
			.nRetention = nCapital
			.nPremium = nPremium
			.nCapital_max = nCapital_max
			.nRest = nRest
			.nType = nType
			.nModulec = nModulec
			.nCover = nCover
			.sClient = sClient
			.sHeapCode = sHeapCode
			.nRest = nRest
			.sCoverDesc = sCoverDesc
			.sCliename = sCliename
			.sChangeDes = sChangeDes
			.sCurrDes = sCurrDes
			.sBranch_Reides = sBranch_Reides
			.sDigit = sDigit
			.sGridCovDesc = sGridCovDesc
			.sModuDesc = sModuDesc
			.nCapital_cov = nCapital_cov
			.nReserve = nReserve
			.nChange = nChange
			.nClasific = nClasific
			
			If nNumber <> 0 And nNumber <> eRemoteDB.Constants.intNull Then
				.nNumber = nNumber
			End If
		End With
		
		mCol.Add(objNewMember, "A" & nModulec & nCover & sClient & nBranchRei)

        'Return the object created
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
	End Function
	
	'%FindReinsuranPol: Este metodo carga la coleccion de elementos de la tabla "tReinsuran" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindReinsuranPol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal sCumReint As String, ByVal sHeapCode As String, ByVal nCoinsushare As Double, ByVal nChange As Integer, Optional ByVal nQueryMode As Integer = 0, Optional ByVal nTransaction As Integer = 0, Optional ByVal nBranchRei As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal nCompany As Integer = 0, Optional ByVal nType As Integer = 0) As Boolean
		'- El parámetro nQueryMode:
		'- 1) Si tiene valor 1, indica que se está en modo de consulta y por lo tanto se ejecutará el SP insCalReinsuran
		'-    ademas de utilizarze cuando se recarga la página al momento de ejecutarse una popup.
		'- 2) Si tiene valor 2 o nulo (String.Empty) NO se ejecutará el SP insCalReinsuran
		
		Dim lrecreaBranchr_all As eRemoteDB.Execute
		Dim lintCoinShare As Integer
		
		If nQueryMode <> 2 Then
			If nCoinsushare <= 0 Then
				lintCoinShare = 100
			Else
				lintCoinShare = nCoinsushare
			End If
			
			'+Definición de parámetros para stored procedure 'insudb.reaBranchr_All'
			'+Información leída el 30/12/1999 15:17:05
			lrecreaBranchr_all = New eRemoteDB.Execute
			With lrecreaBranchr_all
				.StoredProcedure = "insCalReinsuran"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCumReint", sCumReint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sHeapCode", sHeapCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCoinsuShare", lintCoinShare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nChange", nChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCodispl", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nQueryMode", nQueryMode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMasive", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sRenewpol", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				FindReinsuranPol = .Run
				
				If FindReinsuranPol Then
					Call FillCollections(lrecreaBranchr_all, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaBranchr_all may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaBranchr_all = Nothing
		Else
			If nQueryMode = 2 Then
                lrecreaBranchr_all = New eRemoteDB.Execute
                With lrecreaBranchr_all
                    .StoredProcedure = "reaTreinsuran"
                    .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    FindReinsuranPol = .Run()
                    If FindReinsuranPol Then
                        Call FillCollections(lrecreaBranchr_all, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
			End If
                End With
            End If
			
		End If
	End Function
	
	'%FindTReinsuran: Este metodo carga la coleccion de elementos de la tabla "treinsuran" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindTReinsuran(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bLoadData As Boolean = False, Optional ByVal sBrancht As String = "") As Boolean
		Dim lrecreaBranchr_all As eRemoteDB.Execute
		
		On Error GoTo FindTReinsuranErr
		
		lrecreaBranchr_all = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaBranchr_All'
		'+Información leída el 30/12/1999 15:17:05
		With lrecreaBranchr_all
			.StoredProcedure = "reaTreinsuran"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			FindTReinsuran = .Run()
			
			If FindTReinsuran And bLoadData Then
				Call FillCollections(lrecreaBranchr_all, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
			End If
		End With
		
FindTReinsuranErr: 
		If Err.Number Then
			FindTReinsuran = False
		End If
		'UPGRADE_NOTE: Object lrecreaBranchr_all may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBranchr_all = Nothing
	End Function
	
	'%FillCollections: Llena las colecciones correspondientes a la tabla treinsuran
	Private Function FillCollections(ByVal lrecreaBranchr_all As eRemoteDB.Execute, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lobjBranchr As ePolicy.Branchr = New ePolicy.Branchr
        Dim lobjReinsuran As ePolicy.Reinsuran
		Dim lcolReinsuran As ePolicy.Reinsurans
		Dim lintCount As Integer
		
		With lrecreaBranchr_all
			Do While Not .EOF
				lintCount = lintCount + 1
				
				If .FieldToClass("nType") = 1 Then
					
					'+ Se llena la colección para los contratos de retención.
					Call Add(0, .FieldToClass("nCurrency"), .FieldToClass("nBranch_Rei"), .FieldToClass("sAddreini"), .FieldToClass("nCapital"), .FieldToClass("nPremium"), .FieldToClass("nCapitalmax"), .FieldToClass("nType"), .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("sClient"), .FieldToClass("nChange"), .FieldToClass("sHeap_Code"), .FieldToClass("nCapital_rei"), .FieldToClass("nRest"), .FieldToClass("sCoverDesc"), .FieldToClass("sCliename"), .FieldToClass("nNumber"), .FieldToClass("sChangeDes"), .FieldToClass("sCurrdes"), .FieldToClass("sBranch_reides"), .FieldToClass("sDigit"), .FieldToClass("sGridCovDesc"), .FieldToClass("sModuDes"), .FieldToClass("nCapital_cov"), .FieldToClass("nReserve"), .FieldToClass("nClasific"))
					
					'+Se instancia el objeto lobjBranchr.
					lobjBranchr = mCol.Item("A" & .FieldToClass("nModulec") & .FieldToClass("nCover") & .FieldToClass("sClient") & .FieldToClass("nBranch_rei"))
					
				Else
					'+ Se llena la colección para los contratos restantes.
					lcolReinsuran = New Reinsurans
					
                    lobjReinsuran = lcolReinsuran.Add(2, sCertype, nBranch, nProduct, nPolicy, nCertif, .FieldToClass("nBranch_rei"), .FieldToClass("nType"), dEffecdate, .FieldToClass("nCompany"), .FieldToClass("dAccedate"), .FieldToClass("nCapital"), .FieldToClass("nCapitalMax"), .FieldToClass("nCommissi"), .FieldToClass("nCurrency"), .FieldToClass("sHeap_Code"), .FieldToClass("nInter_rate"), .FieldToClass("nNumber"), .FieldToClass("nReser_rate"), .FieldToClass("nQuotasha"), eRemoteDB.Constants.dtmNull, "1", .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("sClient"), .FieldToClass("nChange"), lintCount, .FieldToClass("sContrades"), .FieldToClass("sCompany"), .FieldToClass("nClasific"), .FieldToClass("nCapital_rei"), .FieldToClass("nPremium_Agree"))
					'+Se instancia el objeto lobjReinsuran.
                    lobjReinsuran = lcolReinsuran.Item("A" & nBranch & nProduct & nPolicy & nCertif & .FieldToClass("nType") & .FieldToClass("nNumber") & .FieldToClass("nCompany") & .FieldToClass("nModulec") & .FieldToClass("nCover") & .FieldToClass("sClient"))
					'UPGRADE_NOTE: Object lcolReinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lcolReinsuran = Nothing
				End If
				
                If lobjBranchr.Reinsurans Is Nothing Then
                    lobjBranchr.Reinsurans = New Reinsurans
                End If
				
                lobjReinsuran = lobjBranchr.Reinsurans.Add(1, sCertype, nBranch, nProduct, nPolicy, nCertif, .FieldToClass("nBranch_rei"), .FieldToClass("nType"), dEffecdate, .FieldToClass("nCompany"), .FieldToClass("dAccedate"), .FieldToClass("nCapital"), .FieldToClass("nCapitalMax"), .FieldToClass("nCommissi"), .FieldToClass("nCurrency"), .FieldToClass("sHeap_Code"), .FieldToClass("nInter_rate"), .FieldToClass("nNumber"), .FieldToClass("nReser_rate"), .FieldToClass("nQuotaSha"), eRemoteDB.Constants.dtmNull, "2", .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("sClient"), .FieldToClass("nChange"), eRemoteDB.Constants.intNull, .FieldToClass("sContrades"), .FieldToClass("sCompany"), .FieldToClass("nClasific"), .FieldToClass("nCapital_rei"), .FieldToClass("nPremium_Agree"))
				.RNext()
			Loop 
			.RCloseRec()
		End With
		
		'UPGRADE_NOTE: Object lobjBranchr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjBranchr = Nothing
		'UPGRADE_NOTE: Object lobjReinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjReinsuran = Nothing
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Branchr
		Get
			'Used when referencing an element in the collection.
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'Used when retrieving the number of elements in the collection.
			'Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each...
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'This property allows you to enumerate this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'Used when removing an element from the collection.
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'Creates the collection when this class is created
		mCol = New Collection
		sManualMovType = "1"
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'Destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






