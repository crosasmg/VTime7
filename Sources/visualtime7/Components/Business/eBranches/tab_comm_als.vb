Option Strict Off
Option Explicit On
Public Class tab_comm_als
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: tab_comm_als.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'% Add: se agrega un elemento en la colección
	Public Function Add(ByRef lclsTab_Comm_AL As tab_comm_al) As tab_comm_al
		
		mCol.Add(lclsTab_Comm_AL)
		
		'+ Return the object created
		Add = lclsTab_Comm_AL
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As tab_comm_al
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
	'**%Find: This method fills the collection with records from the
	'**%      table "tab_comm_al" for the MVA645 transaction grid,
	'**%      returning TRUE or FALSE depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la
	'%      tabla "tab_comm_al" para la grilla de la transaccion MVA645,
	'%      devolviendo Verdadero/Falso si hay/no hay registros
	Public Function Find_Agreement(ByVal nComtabli As Integer, ByVal nIntertyp As Integer, ByVal nSellChannel As Integer, ByVal nWay_pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecinsReaTab_comm_al_Agreement As eRemoteDB.Execute
		Dim lclsTab_Comm_AL As tab_comm_al
		
		On Error GoTo Find_Err
		
		Find_Agreement = False
		
		lrecinsReaTab_comm_al_Agreement = New eRemoteDB.Execute
		
		With lrecinsReaTab_comm_al_Agreement
			.StoredProcedure = "reaTab_comm_al_Agreement"
			
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSellChannel", nSellChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsTab_Comm_AL = New tab_comm_al
					lclsTab_Comm_AL.nComtabli = nComtabli
					lclsTab_Comm_AL.nIntertyp = nIntertyp
					lclsTab_Comm_AL.nSellChannel = nSellChannel
					lclsTab_Comm_AL.nWay_pay = nWay_pay
					lclsTab_Comm_AL.nBranch = nBranch
					lclsTab_Comm_AL.nProduct = nProduct
					lclsTab_Comm_AL.nModulec = nModulec
					lclsTab_Comm_AL.nCover = nCover
					lclsTab_Comm_AL.dEffecdate = dEffecdate
					lclsTab_Comm_AL.nAgreement = .FieldToClass("nAgreement")
					lclsTab_Comm_AL.nQPB = .FieldToClass("nQpb")
					lclsTab_Comm_AL.nPercent = .FieldToClass("nPercent")
					lclsTab_Comm_AL.nAmount = .FieldToClass("nAmount")
					lclsTab_Comm_AL.nCurrency = .FieldToClass("nCurrency")
					
					Call Add(lclsTab_Comm_AL)
					
					'UPGRADE_NOTE: Object lclsTab_Comm_AL may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_Comm_AL = Nothing
					.RNext()
				Loop 
				Find_Agreement = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find_Agreement = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsReaTab_comm_al_Agreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaTab_comm_al_Agreement = Nothing
	End Function
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
End Class






