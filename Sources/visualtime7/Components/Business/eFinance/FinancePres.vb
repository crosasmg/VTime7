Option Strict Off
Option Explicit On
Public Class FinancePres
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: FinancePres.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 15/04/04 4:50p                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	
	'- Variable definition. This variables holds the total amount to be financed and the total of commission of a premium invoice
	'- Se declara las variables que almacenaran tanto el total  a financiar como el
	'- de comisión de un recibo
	
	Public nTotalAmount As Double
	Public nTotalCommision As Double
	
	
	'- local variable to hold collection
	Private mCol As Collection
	
	'- Variable definition. This variable is used to force the read in the table
	'- Se declara la variable para forzar la búsqueda de los datos en la tabla
	
	Private lAuxContrat As Double
	
	'% Add: adds a new instance of the "FinancePre" class to the collection
	'% Add: Añade una nueva instancia de la clase "FinancePre" a la colección
	Public Function Add(ByVal nStatInstanc As FinanceDraft.eStatusInstance, ByVal sCurrency As String, ByVal sCliename As String, ByVal sClient As String, ByVal sStatregt As String, ByVal sStat_finpr As String, ByVal nReceipt As Double, ByVal nPremium As Double, ByVal nPolicy As Double, ByVal nOffice As Integer, ByVal nIntermed As Integer, ByVal dExpirdat As Date, ByVal nExchange As Double, ByVal dStartdate As Date, ByVal nCurrency As Integer, ByVal nContrat As Double, ByVal nCommission As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCompany As Integer, ByVal sProduct As String, ByVal sExtReceipt As String, ByVal sCompCliename As String, ByVal sOffice As String, ByVal sIntermed As String, ByVal ncreFinanc_com As Integer) As FinancePre
		Dim objNewMember As FinancePre
		objNewMember = New FinancePre
		
		'+ set the properties passed into the method
		With objNewMember
			.nStatInstanc = nStatInstanc
			.sCurrency = sCurrency
			.sCliename = sCliename
			.sClient = sClient
			.sStatregt = sStatregt
			.sStat_finpr = sStat_finpr
			.nReceipt = nReceipt
			.nPremium = nPremium
			.nPolicy = nPolicy
			.nOffice = nOffice
			.nIntermed = nIntermed
			.dExpirdat = dExpirdat
			.nExchange = nExchange
			.dStartdate = dStartdate
			.nCurrency = nCurrency
			.nContrat = nContrat
			.nCommission = nCommission
			.nBranch = nBranch
			.nProduct = nProduct
			.sProduct = sProduct
			.nCompany = nCompany
			.ncreFinanc_com = ncreFinanc_com
			.sExtReceipt = sExtReceipt
			.sCompCliename = sCompCliename
			.sOffice = sOffice
			.sIntermed = sIntermed
			
		End With
		
		mCol.Add(objNewMember, "R" & nReceipt & nContrat)
		
		'+ return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% Find: This method fills the collection with records from the table "Financ_pre" returning TRUE or FALSE
	'%       depending on the existence of the records
	'% Find: Este metodo carga la coleccion de elementos de la tabla "Financ_pre" devolviendo Verdadero o
	'%       falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal Contrat As Double, Optional ByVal bOnlyToVal As Boolean = False) As Boolean
		Dim lrecinsreaFinanc_pre As eRemoteDB.Execute
		If Contrat = lAuxContrat Then
			Find = True
		Else
			
			lrecinsreaFinanc_pre = New eRemoteDB.Execute
			
			'+ Stored procedure parameters definition 'insudb.insreaFinanc_pre'
			'+ Data of 09/13/1999 02:19:32 PM
			'+ Definición de parámetros para stored procedure 'insudb.insreaFinanc_pre'
			'+ Información leída el 13/09/1999 02:19:32 PM
			
			With lrecinsreaFinanc_pre
				.StoredProcedure = "insreaFinanc_prepkg.insreafinanc_pre"
				.Parameters.Add("nContrat", Contrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReceipt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					If Not bOnlyToVal Then
						Do While Not .EOF
							Call Add(FinanceDraft.eStatusInstance.eftQuery, .FieldToClass("Currency_sDescript"), .FieldToClass("sCliename"), .FieldToClass("sClient"), .FieldToClass("sStatregt"), .FieldToClass("sStat_finpr"), .FieldToClass("nReceipt"), .FieldToClass("nPremium"), .FieldToClass("nPolicy"), .FieldToClass("nOffice"), .FieldToClass("nIntermed"), .FieldToClass("dExpirdat"), .FieldToClass("nExchange"), .FieldToClass("dStartdate"), .FieldToClass("nCurrency"), .FieldToClass("nContrat"), .FieldToClass("nCommission"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nCompany"), .FieldToClass("Product_sDescript"), .FieldToClass("sExtReceipt"), .FieldToClass("sCompCliename"), .FieldToClass("Office_sDescript"), .FieldToClass("sIntCliename"), 1)
							.RNext()
						Loop 
					End If
					.RCloseRec()
					Find = True
					lAuxContrat = Contrat
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecinsreaFinanc_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecinsreaFinanc_pre = Nothing
			
		End If
	End Function
	'% Update: This method updates the records of the collection
	'% Update: Permite actualizar los registros de la colección
	Public Function UpDate() As Boolean
		Dim lclsFinancePre As FinancePre
		
		UpDate = True
		
		For	Each lclsFinancePre In mCol
			With lclsFinancePre
				If lAuxContrat = 0 Then
					lAuxContrat = .nContrat
				End If
				Select Case .nStatInstanc
					Case FinanceDraft.eStatusInstance.eftNew
						UpDate = .Add(.ncreFinanc_com)
						.nStatInstanc = FinanceDraft.eStatusInstance.eftQuery
					Case FinanceDraft.eStatusInstance.eftUpDate
						UpDate = .UpDate(.ncreFinanc_com)
					Case FinanceDraft.eStatusInstance.eftDelete
						UpDate = .Delete
						mCol.Remove(("R" & .nReceipt))
				End Select
			End With
		Next lclsFinancePre
		
	End Function
	'% FindReceipt: Search into the collection a specific premium invoice
	'% FindReceipt: busca dentro de la colección un recibo determinado
	Public Function FindReceipt(ByVal nReceipt As Double, ByVal sExtReceipt As String) As Boolean
		Dim lclsFinancePre As FinancePre
		
		FindReceipt = False
		
		For	Each lclsFinancePre In mCol
			With lclsFinancePre
				If .nReceipt = nReceipt And .sExtReceipt <> sExtReceipt Then
					FindReceipt = True
					Exit For
				End If
			End With
		Next lclsFinancePre
		
		'UPGRADE_NOTE: Object lclsFinancePre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinancePre = Nothing
	End Function
	'% FindPolicy: busca dentro de la colección si existe mas de una poliza
	Public Function FindPolicy(ByVal nPolicy As Double) As Boolean
		Dim lclsFinancePre As FinancePre
		
		FindPolicy = False
		
		For	Each lclsFinancePre In mCol
			With lclsFinancePre
				If .nPolicy <> nPolicy Then
					FindPolicy = True
					Exit For
				End If
			End With
		Next lclsFinancePre
		
		'UPGRADE_NOTE: Object lclsFinancePre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinancePre = Nothing
	End Function
	'% Intermed: This function calculates the intermediary code
	'% Intermed: Esta función se encarga de calcular el código del intermediario
	Public Function Intermed() As Integer
        Dim lclsFinancePre As FinancePre = New FinancePre

        Intermed = lclsFinancePre.nIntermed
		
		For	Each lclsFinancePre In mCol
			With lclsFinancePre
				If Intermed <> .nIntermed Then
					Intermed = 0
				End If
			End With
		Next lclsFinancePre
		'UPGRADE_NOTE: Object lclsFinancePre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinancePre = Nothing
		
	End Function
	
	'% Commission: This function calculates the amount of commission associate to each draft
	'% Commission: Esta función se encarga de calcular el importe de coisión asociado  a cada uno  de los giros.
	Public Function Commission(ByRef Contrat As Double) As Double
		Dim lclsFinancePre As FinancePre
		
		Commission = 0
		
		For	Each lclsFinancePre In mCol
			With lclsFinancePre
				Commission = Commission + (.nCommission * .nExchange)
			End With
		Next lclsFinancePre
		
	End Function
	
	'% Commission2: This function calculates the amount of commission associate to each draft
	'% Commission2: Esta función se encarga de calcular el importe de coisión asociado  a cada uno  de los giros.
	Public Function Commission2(ByRef Contrat As Double) As Double
		Dim lclsFinancePre As FinancePre
		
		Commission2 = 0
		
		For	Each lclsFinancePre In mCol
			With lclsFinancePre
				Commission2 = Commission2 + .nCommission
			End With
		Next lclsFinancePre
		
	End Function
	
	'% FoundReceipt: Search a specific premium invoice into the collection
	'% FoundReceipt: Permite comprobar la existencia de un recibo determinado dentro de la colección
	Public Function FoundReceipt(ByVal nReceipt As Double, ByVal nContrat As Double) As Boolean
		Dim lclsFinancePre As FinancePre
		lclsFinancePre = New FinancePre
		
		On Error GoTo FoundReceipt_Err
		
		lclsFinancePre = mCol.Item("R" & nReceipt & nContrat)
		
		FoundReceipt = True
		
		'UPGRADE_NOTE: Object lclsFinancePre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinancePre = Nothing
		
FoundReceipt_Err: 
		If Err.Number Then
			FoundReceipt = False
		End If
	End Function
	
	'%Find_DataReceipt: Gets the data of the financed premium invoices of a contract FI002
	'%Find_DataReceipt: Permite obtener la información de los recibos finaciados
	'%                  de a un contrato (FI002)
	Public Function Find_DataReceipt(ByVal nContrat As Double, ByVal dEffecdate As Date, Optional ByVal bOnlyToVal As Boolean = False) As Boolean
		Dim lintCurrency As Integer
		Dim ldblCommission As Double
		Dim ldblExchange As Double
		Dim lintCOCurrency As Integer
		Dim ldblCOCommision As Double
		Dim ldlbPremium As Double
		Dim ldtmCOEffecdate As Date
		
		Dim lrecinsreaFinanc_pre As eRemoteDB.Execute
		Dim lclsFinanceCO As eFinance.financeCO
		Dim lclsExchange As eGeneral.Exchange
		Dim lclsPremium As Object
		
		On Error GoTo Find_DataReceipt_Err
		
		lrecinsreaFinanc_pre = New eRemoteDB.Execute
		lclsFinanceCO = New financeCO
		
		nTotalAmount = 0
		nTotalCommision = 0
		
		With lclsFinanceCO
			If .Find(nContrat, dEffecdate) Then
				lintCOCurrency = .nCurrency
				ldtmCOEffecdate = .dEffecdate
				ldblCOCommision = .nCommision
				
			End If
		End With
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		
		'+ Stored procedure parameters definition 'insudb.insreaFinanc_pre'
		'+ Data of 09/13/1999 02:19:32 PM
		'+ Definición de parámetros para stored procedure 'insudb.insreaFinanc_pre'
		'+ Información leída el 13/09/1999 02:19:32 PM
		With lrecinsreaFinanc_pre
			.StoredProcedure = "insreaFinanc_prepkg.insreafinanc_pre"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_DataReceipt = True
				
				'+Si no es sólo para validar si existe informacion, se requiere cargar los registros
				If Not bOnlyToVal Then
					
					lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
					
					Do While Not .EOF
						lintCurrency = .FieldToClass("nCurrency")
						ldblCommission = .FieldToClass("nCommission")
						ldlbPremium = .FieldToClass("nPremium")
						Call lclsPremium.Find("2", .FieldToClass("nReceipt"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), 0, 0)
						
						'+ Calculates the equivalent in the contract currency
						'+ Se calcula el equivalente en la moneda del contrato
						
						If lintCOCurrency <> lintCurrency Then
							
							''+ Calculates the equivalent of the total commission in the contract currency
							''+ Se consigue el equivalente del Total de comisión a la moneda del contrato
							lclsExchange = New eGeneral.Exchange
							
							If ldblCommission = 0 Then
								Call lclsExchange.Convert(eRemoteDB.Constants.intNull, lclsPremium.nComamou, lintCurrency, lintCOCurrency, ldtmCOEffecdate, 0)
							Else
								Call lclsExchange.Convert(eRemoteDB.Constants.intNull, ldblCommission, lintCurrency, lintCOCurrency, ldtmCOEffecdate, 0)
							End If
							
							ldblCOCommision = ldblCOCommision + lclsExchange.pdblResult
							ldblExchange = lclsExchange.pdblExchange
							
							''+ Calculates the equivalent of the financed total in the contract currency
							''+ Se consigue el equivalente del Total a financiar a la moneda del contrato
							
							Call lclsExchange.Convert(eRemoteDB.Constants.intNull, ldlbPremium, lclsPremium.nCurrency, lintCOCurrency, lclsPremium.dEffecdate, 0)
							
							nTotalAmount = nTotalAmount + lclsExchange.pdblResult
							
							''+ Calculates the equivalent of the total commission in the contract currency
							''+ Se consigue el equivalente del Total de comisión a la moneda del contrato
							
							Call lclsExchange.Convert(eRemoteDB.Constants.intNull, lclsPremium.nComamou, lclsPremium.nCurrency, lintCOCurrency, lclsPremium.dEffecdate, 0)
							
							nTotalCommision = nTotalCommision + lclsExchange.pdblResult
							
							'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsExchange = Nothing
						Else
							ldblCOCommision = ldblCOCommision + ldblCommission
							ldblExchange = lclsPremium.nExchange
							
							nTotalAmount = nTotalAmount + ldlbPremium
							nTotalCommision = nTotalCommision + lclsPremium.nComamou
						End If
						
						Call Add(FinanceDraft.eStatusInstance.eftQuery, .FieldToClass("Currency_sDescript"), .FieldToClass("sCliename"), .FieldToClass("sClient"), .FieldToClass("sStatregt"), .FieldToClass("sStat_finpr"), .FieldToClass("nReceipt"), ldlbPremium, .FieldToClass("nPolicy"), .FieldToClass("nOffice"), .FieldToClass("nIntermed"), .FieldToClass("dExpirdat"), ldblExchange, .FieldToClass("dStartdate"), lintCurrency, .FieldToClass("nContrat"), ldblCOCommision, .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nCompany"), .FieldToClass("Product_sDescript"), .FieldToClass("sExtReceipt"), .FieldToClass("sCompCliename"), .FieldToClass("Office_sDescript"), .FieldToClass("sIntCliename"), 1)
						.RNext()
					Loop 
				End If
				.RCloseRec()
				'+Se calcula el monto a financiar, aplicando factor de interes a prima de recibo
				'+(proceso opcional ya que lo importante es el monto de refinan_dra)
				'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsPremium = Nothing
			End If
		End With
		
Find_DataReceipt_Err: 
		If Err.Number Then
			Find_DataReceipt = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsreaFinanc_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsreaFinanc_pre = Nothing
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
	End Function
	
	'*Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As FinancePre
		Get
			'+ used when referencing an element in the collection
			'+ vntIndexKey contains either the Index or Key to the collection,
			'+ this is why it is declared as a Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)1
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Returns the number of elements that the collection has
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'+ used when retrieving the number of elements in the
			'+ collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	'% NewEnum: Enumerates the collection for use in a For Each...Next loop
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+ this property allows you to enumerate
			'+ this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ used when removing an element from the collection
		'+ vntIndexKey contains either the Index or Key, which is why
		'+ it is declared as a Variant
		'+ Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	'% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+ creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'% Class_Terminate: Controls the destruction of an instance of the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'+ destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






