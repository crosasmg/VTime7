Option Strict Off
Option Explicit On
Public Class Cash_stats
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cash_stats.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 27/10/03 11:05a                              $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	Public Function Add(ByVal nCashNum As Integer, ByVal dStatDate As Date, ByVal nStatus As Integer, ByVal nCash_id As Double, ByVal nOfficeAgen As Integer, ByVal sDescript As String, ByVal sClient As String, ByVal sDigit As String, ByVal sCliename As String) As Cash_stat
		
		'create a new object
		Dim lclsCash_stat As Cash_stat
		lclsCash_stat = New Cash_stat
		
		With lclsCash_stat
			.nCashNum = nCashNum
			.dStatDate = dStatDate
			.nStatus = nStatus
			.nCash_id = nCash_id
			.nOfficeAgen = nOfficeAgen
			.sDescript = sDescript
			.sCliename = sCliename
			.sClient = sClient
			.sDigit = sDigit
		End With
		
		'set the properties passed into the method
		mCol.Add(lclsCash_stat)
		
		'return the object created
		Add = lclsCash_stat
		'UPGRADE_NOTE: Object lclsCash_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_stat = Nothing
	End Function
	Public Function AddOPC720(ByVal nCashNum As Integer, ByVal dStartDate As Date, ByVal nStatus As Integer, ByVal nCash_id As Double, ByVal nOfficeAgen As Integer, ByVal sDescript As String, ByVal sClient As String, ByVal sDigit As String, ByVal sCliename As String, ByVal dInitCloseCash As Date, ByVal dEndCloseCash As Date, ByVal dCloseOkCash As Date, ByVal sClientSup As String, ByVal sDigitSup As String, ByVal sClienameSup As String, ByVal sClientHeadSup As String, ByVal sDigitHeadSup As String, ByVal sClienameHeadSup As String, ByVal sDes_Status As String) As Cash_stat
		
		'create a new object
		Dim lclsCash_stat As Cash_stat
		lclsCash_stat = New Cash_stat
		
		With lclsCash_stat
			.nCashNum = nCashNum
			.dStartDate = dStartDate
			.nStatus = nStatus
			.nCash_id = nCash_id
			.nOfficeAgen = nOfficeAgen
			.sDescript = sDescript
			.sCliename = sCliename
			.sClient = sClient
			.sDigit = sDigit
			.dInitCloseCash = dInitCloseCash
			.dEndCloseCash = dEndCloseCash
			.dCloseOkCash = dCloseOkCash
			.sClientSup = sClientSup
			.sDigitSup = sDigitSup
			.sClienameSup = sClienameSup
			.sClientHeadSup = sClientHeadSup
			.sDigitHeadSup = sDigitHeadSup
			.sClienameHeadSup = sClienameHeadSup
			.sDsp_Status = sDes_Status
		End With
		
		'set the properties passed into the method
		mCol.Add(lclsCash_stat)
		
		'return the object created
		AddOPC720 = lclsCash_stat
		'UPGRADE_NOTE: Object lclsCash_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_stat = Nothing
	End Function
	
	'**%Find: This method fills the collection with records from the table "Cash_stat" returing TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Cash_stat" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal nCashNum As Integer, ByVal dStatDate As Date, ByVal nStatus As Integer, ByVal nCash_id As Double) As Boolean
		Dim lblnWhereInd As Boolean
		Dim lstrQuery As String
		Dim lclsConstruct As eRemoteDB.ConstructSelect
		Dim lrecCash_stat As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecCash_stat = New eRemoteDB.Execute
		lclsConstruct = New eRemoteDB.ConstructSelect
		
		lclsConstruct.SelectClause("Cash_stat.nCashnum, Cash_stat.dStatdate, Cash_stat.nCash_id, Cash_stat.nStatus ")
		lclsConstruct.NameFatherTable("Cash_stat", "Cash_stat")
		
		lblnWhereInd = False
		If nCashNum > 0 Then
			lblnWhereInd = True
		End If
		
		If dStatDate <> dtmNull Then
			lblnWhereInd = True
		End If
		
		If nStatus > 0 Then
			lblnWhereInd = True
		End If
		
		If nCash_id > 0 Then
			lblnWhereInd = True
		End If
		
		If lblnWhereInd Then
			
			If nCashNum > 0 Then
				Call lclsConstruct.WhereClause("Cash_stat.nCashnum", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, CStr(nCashNum))
			End If
			
			If dStatDate <> dtmNull Then
				Call lclsConstruct.WhereClause("$DATE(Cash_stat.dStatdate)", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, CStr(dStatDate), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			If nCash_id > 0 Then
				Call lclsConstruct.WhereClause("Cash_stat.nCash_id", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, CStr(nCash_id), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			If nStatus > 0 Then
				Call lclsConstruct.WhereClause("Cash_stat.nStatus", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, CStr(nStatus), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
		End If
		lclsConstruct.OrderBy(" ORDER BY Cash_stat.nCash_id")
		
		lstrQuery = lclsConstruct.Answer
		
		'UPGRADE_NOTE: Object lclsConstruct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsConstruct = Nothing
		
		'**+ Prepare and execute the "store procedure"
		'+Se prepara y ejecuta el "store procedure"
		
		With lrecCash_stat
			If Trim(lstrQuery) <> String.Empty Then
				.Sql = lstrQuery
			End If
			
			If .Run() Then
				
				'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				mCol = Nothing
				mCol = New Collection
				Do While Not .EOF
					Call Add(.FieldToClass("nCashnum"), .FieldToClass("dStartdate"), .FieldToClass("nCash_id"), .FieldToClass("nStatus"), .FieldToClass("nOfficeAgen"), .FieldToClass("sDescript"), .FieldToClass("sClient"), .FieldToClass("sDigit"), .FieldToClass("sCliename"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecCash_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCash_stat = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cash_stat
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'FindOP720: Función que realiza la busqueda en la tabla Cash_Stat de acuerdo a unos parámetros
	
	Public Function FindOPC720(ByVal nOfficeAgen As Integer, ByVal nCashNum As Integer, ByVal dStartDate As Date, ByVal nCash_id As Double, ByVal nStatus As Integer, ByVal dInitCloseCash As Date, ByVal dEndCloseCash As Date, ByVal dCloseOkCash As Date) As Boolean
		On Error GoTo FindOPC720_Err
		
		Dim lstrClient As String
		Dim lstrDigit As String
		Dim lstrCliename As String
		Dim lstrClientSup As String
		Dim lstrDigitSup As String
		Dim lstrClienameSup As String
		Dim lstrClientHeadSup As String
		Dim lstrDigitHeadSup As String
		Dim lstrClienameHeadSup As String
		Dim lstrArray_txt() As String
		
		Dim lclsCash_stat As eRemoteDB.Execute
		
		lclsCash_stat = New eRemoteDB.Execute
		
		'+Se definen los parámetros para el store procedure ReaCash_Stat
		With lclsCash_stat
			.StoredProcedure = "ReaCash_Stat"
			.Parameters.Add("nOfficeagen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_Id", nCash_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitCloseCash", dInitCloseCash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndCloseCash", dEndCloseCash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCloseOkCash", dCloseOkCash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					lstrArray_txt = Microsoft.VisualBasic.Split(.FieldToClass("sClient"), "|")
					lstrClient = lstrArray_txt(0)
					lstrDigit = lstrArray_txt(1)
					lstrCliename = lstrArray_txt(2)
					lstrArray_txt = Microsoft.VisualBasic.Split(.FieldToClass("sClientSup"), "|")
					lstrClientSup = lstrArray_txt(0)
					lstrDigitSup = lstrArray_txt(1)
					lstrClienameSup = lstrArray_txt(2)
					lstrArray_txt = Microsoft.VisualBasic.Split(.FieldToClass("sClientHeadSup"), "|")
					lstrClientHeadSup = lstrArray_txt(0)
					lstrDigitHeadSup = lstrArray_txt(1)
					lstrClienameHeadSup = lstrArray_txt(2)
					
					Call AddOPC720(.FieldToClass("nCashNum"), .FieldToClass("dStartDate"), .FieldToClass("nStatus"), .FieldToClass("nCash_Id"), .FieldToClass("nOfficeagen"), .FieldToClass("sDescript"), lstrClient, lstrDigit, lstrCliename, .FieldToClass("dInitCloseCash"), .FieldToClass("dEndCloseCash"), .FieldToClass("dCloseOkCash"), lstrClientSup, lstrDigitSup, lstrClienameSup, lstrClientHeadSup, lstrDigitHeadSup, lstrClienameHeadSup, .FieldToClass("sDes_Status"))
					.RNext()
				Loop 
				FindOPC720 = True
				.RCloseRec()
			Else
				FindOPC720 = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsCash_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_stat = Nothing
		
FindOPC720_Err: 
		If Err.Number Then
			FindOPC720 = False
		End If
		On Error GoTo 0
	End Function
End Class






