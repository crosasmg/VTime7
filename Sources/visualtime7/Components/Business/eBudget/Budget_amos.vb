Option Strict Off
Option Explicit On
Public Class Budget_amos
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Budget_amos.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:36p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables auxiliares
	
	Private mintLed_Compan As Integer
	Private mintCurrency As Integer
	Private mstrBud_code As String
	Private mintYear As Integer
	Private mintMonth As Integer
	Private mdtmInitDate As Date
	Private mdtmEndDate As Date
	Private mintTypeAmount As Integer
	
	'% AddBudgetQue: Añade una nueva instancia de la clase Budget_amo a la colección
	Public Function AddBudgetQue(ByVal objElement As Object) As Budget_amo
		
		Dim objNewMember As Budget_amo
		objNewMember = objElement
		
		mCol.Add(objNewMember)
		
		'+ Retorna el objeto creado
		
		AddBudgetQue = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% Add: Añade una nueva instancia de la clase Budget_amo a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nLed_compan As Integer, ByVal nCurrency As Integer, ByVal sBud_code As String, ByVal sAccount As String, ByVal sAux_accoun As String, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nBalance As Double, Optional ByVal sCost_cente As String = "") As Budget_amo
		
		Dim objNewMember As Budget_amo
		objNewMember = New Budget_amo
		
		'+ Set the properties passed into the method
		With objNewMember
			.nStatusInstance = CStr(nStatusInstance)
			.nLed_compan = nLed_compan
			.nCurrency = nCurrency
			.sBud_code = sBud_code
			.sAccount = sAccount
			.sAux_accoun = sAux_accoun
			.nYear = nYear
			.nMonth = nMonth
			.nBalance = nBalance
			.sCost_cente = sCost_cente
		End With
		
		'mCol.Add objNewMember
		mCol.Add(objNewMember) ', "B" & sBud_code & nMonth
		
		'+ Retorna el objeto creado
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	
	'% Find: Devuelve información acerca de una ventana
	'------------------------------------------------------------
	Public Function Find(ByVal nLed_compan As Integer, ByVal nCurrency As Integer, ByVal sBud_code As String, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nTypeAmount As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		Dim lrecreaBudgetQue As eRemoteDB.Execute
		Dim lclsBudget_amo As Budget_amo
		
		On Error GoTo Find_Err
		
		lrecreaBudgetQue = New eRemoteDB.Execute
		
		If nLed_compan <> mintLed_Compan Or nCurrency <> mintCurrency Or sBud_code <> mstrBud_code Or nYear <> mintYear Or nMonth <> mintMonth Or dInitDate <> mdtmInitDate Or dEndDate <> mdtmEndDate Or nTypeAmount <> mintTypeAmount Or lblnFind Then
			
			mintLed_Compan = nLed_compan
			mintCurrency = nCurrency
			mstrBud_code = sBud_code
			mintYear = nYear
			mintMonth = nMonth
			mdtmInitDate = dInitDate
			mdtmEndDate = dEndDate
			mintTypeAmount = nTypeAmount
			
			With lrecreaBudgetQue
				.StoredProcedure = "reaBudgetQue"
				.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dInitdate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEnddate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeAmount", IIf(nTypeAmount, "M", "A"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsBudget_amo = New Budget_amo
						
						lclsBudget_amo.nLed_compan = .FieldToClass("nLed_compan")
						lclsBudget_amo.nCurrency = .FieldToClass("nCurrency")
						lclsBudget_amo.sBud_code = .FieldToClass("sBud_code")
						lclsBudget_amo.sAccount = .FieldToClass("sAccount")
						lclsBudget_amo.sAux_accoun = .FieldToClass("sAux_accoun")
						lclsBudget_amo.sCost_cente = .FieldToClass("sCost_cente")
						lclsBudget_amo.nYear = .FieldToClass("nYear")
						lclsBudget_amo.nMonth = .FieldToClass("nMonth")
						lclsBudget_amo.nBalance = .FieldToClass("nBalance")
						lclsBudget_amo.sDescript = .FieldToClass("sDescript")
						lclsBudget_amo.nDebit = .FieldToClass("nDebit")
						lclsBudget_amo.nCredit = .FieldToClass("nCredit")
						
						Call AddBudgetQue(lclsBudget_amo)
						
						'UPGRADE_NOTE: Object lclsBudget_amo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsBudget_amo = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaBudgetQue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBudgetQue = Nothing
	End Function
	
	'% Find_All: Devuelve información acerca de una ventana
	'------------------------------------------------------------
	Public Function Find_All(ByVal nLed_compan As Integer, ByVal nCurrency As Integer, ByVal sBud_code As String, ByVal nYear As Integer, ByVal sAccount As String, ByVal sAux_accoun As String, ByVal sCost_cente As String) As Boolean
		'------------------------------------------------------------
		
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		Dim lclsBudget_amo As Budget_amo
		Dim lrecreaBudget_amo_All As eRemoteDB.Execute
		
		lrecreaBudget_amo_All = New eRemoteDB.Execute
		
		On Error GoTo Find_All_Err
		
		
		With lrecreaBudget_amo_All
			.StoredProcedure = "reaBudget_amo_All"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBud_code", sBud_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If Trim(sCost_cente) = String.Empty Then
					Do While Not .EOF
						lclsBudget_amo = New Budget_amo
						
						lclsBudget_amo.nLed_compan = .FieldToClass("nLed_compan")
						lclsBudget_amo.nCurrency = .FieldToClass("nCurrency")
						lclsBudget_amo.sBud_code = .FieldToClass("sBud_code")
						lclsBudget_amo.sAccount = .FieldToClass("sAccount")
						lclsBudget_amo.sAux_accoun = .FieldToClass("sAux_accoun")
						lclsBudget_amo.nYear = .FieldToClass("nYear")
						lclsBudget_amo.nMonth = .FieldToClass("nMonth")
						lclsBudget_amo.nBalance = .FieldToClass("nBalance")
						
						Call AddBudgetQue(lclsBudget_amo)
						
						'UPGRADE_NOTE: Object lclsBudget_amo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsBudget_amo = Nothing
						.RNext()
					Loop 
				Else
					Do While Not .EOF
						lclsBudget_amo = New Budget_amo
						
						lclsBudget_amo.nLed_compan = .FieldToClass("nLed_compan")
						lclsBudget_amo.nCurrency = .FieldToClass("nCurrency")
						lclsBudget_amo.sBud_code = .FieldToClass("sBud_code")
						lclsBudget_amo.sAccount = .FieldToClass("sAccount")
						lclsBudget_amo.sAux_accoun = .FieldToClass("sAux_accoun")
						lclsBudget_amo.nYear = .FieldToClass("nYear")
						lclsBudget_amo.nMonth = .FieldToClass("nMonth")
						
						Call AddBudgetQue(lclsBudget_amo)
						
						'UPGRADE_NOTE: Object lclsBudget_amo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsBudget_amo = Nothing
						.RNext()
					Loop 
				End If
				.RCloseRec()
				lblnRead = True
			Else
				lblnRead = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaBudget_amo_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBudget_amo_All = Nothing
		Find_All = lblnRead
		
Find_All_Err: 
		If Err.Number Then
			Find_All = False
		End If
		On Error GoTo 0
		
	End Function
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Budget_amo
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
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






