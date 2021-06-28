Option Strict Off
Option Explicit On
Public Class Bank_accs
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	Private mclsCheque As eCashBank.Cheque
	Private sBanks As String
	Private sCurrencys As String
	
	Public ReadOnly Property sCurrency() As Object
		Get
			sCurrency = sCurrencys
		End Get
	End Property
	Public ReadOnly Property sBank() As Object
		Get
			sBank = sBanks
		End Get
	End Property
	
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Bank_acc
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
			'
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	'% Add: Añade una nueva instancia de la clase Bank_acc a la colección
	Public Function Add(ByVal nAcc_bank As Integer, ByVal sAcc_ledger As String, ByVal nBank_code As Integer, ByVal nBk_agency As Integer, ByVal sAcc_number As String, ByVal nAcc_type As Integer, ByVal sAux_accoun As String, ByVal nAvail_type As Integer, ByVal nAvailable As Double, ByVal dCompdate As Date, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nLed_compan As Integer, ByVal nOffice As Integer, ByVal nTransit_1 As Double, ByVal nTransit_2 As Double, ByVal nTransit_3 As Double, ByVal nTransit_4 As Double, ByVal nTransit_5 As Double, ByVal nUsercode As Integer, ByVal sStatregt As String, ByVal sShort_des As String, ByVal sAgencyDesc As String, ByVal sCliename As String, ByVal sAccDesc As String, ByVal sAuxDesc As String, ByVal sCheque As String, ByVal nRequest_nu As Double, ByVal nSta_cheque As Integer, ByVal nAmount As Double, ByVal dDat_propos As Date, ByVal dIssue_Dat As Date, ByVal nConcept As Integer, ByVal sClient As String) As Bank_acc
		
		'- Se define la variable que contendra la instancia a añadir
		Dim objNewMember As Bank_acc
		objNewMember = New Bank_acc
		With objNewMember
			.nAcc_bank = nAcc_bank
			.sAcc_ledger = sAcc_ledger
			.nBank_code = nBank_code
			.nBk_agency = nBk_agency
			.sAcc_number = sAcc_number
			.nAcc_type = nAcc_type
			.sAux_accoun = sAux_accoun
			.nAvail_type = nAvail_type
			.nAvailable = nAvailable
			.nCurrency = nCurrency
			.dEffecdate = dEffecdate
			.nLed_compan = nLed_compan
			.nOffice = nOffice
			.nTransit_1 = nTransit_1
			.nTransit_2 = nTransit_2
			.nTransit_3 = nTransit_3
			.nTransit_4 = nTransit_4
			.nTransit_5 = nTransit_5
			.nUsercode = nUsercode
			.sStatregt = sStatregt
			.sShort_des = sShort_des
			.nAcc_type = nAcc_type
			.sAgencyDesc = sAgencyDesc
			.sCliename = sCliename
			.sAccDesc = sAccDesc
			.sAuxDesc = .sAuxDesc
			.mclsCheque = New Cheque
			.mclsCheque.sCheque = sCheque
			.mclsCheque.nRequest_nu = nRequest_nu
			.mclsCheque.nSta_cheque = nSta_cheque
			.mclsCheque.nAmount = nAmount
			.mclsCheque.dDat_propos = dDat_propos
			.mclsCheque.dIssue_Dat = dIssue_Dat
			.mclsCheque.nConcept = nConcept
			.mclsCheque.sClient = sClient
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		
Add_Err: 
		If Err.Number Then
            Add = Nothing
		End If
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		On Error GoTo 0
	End Function
	'% FindOPC002: Devuelve     Se encarga de realizar la lectura  correspondiente  a  la tabla de cuentas
	'%             bancarias, para obtener el registro valido para la llave pasada como  parametro
	Public Function FindOPC002(ByVal sSelect As String) As Boolean
		Dim lclbank_acc As Object
		Dim lclsBank_acc As eCashBank.Bank_acc
		Dim lrecCheque As eRemoteDB.Execute
		
		On Error GoTo FindOPC002_Err
		lclbank_acc = New eCashBank.Bank_acc
		lrecCheque = New eRemoteDB.Execute
		
		lrecCheque.Sql = sSelect
		
		With lrecCheque
			If lrecCheque.Run Then
				FindOPC002 = True
				Do While Not .EOF
					lclsBank_acc = Add(.FieldToClass("nAcc_bank"), String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sAcc_number"), eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nAvailable"), dtmNull, .FieldToClass("ncurrency"), dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nTransit_1"), .FieldToClass("nTransit_2"), .FieldToClass("nTransit_3"), .FieldToClass("nTransit_4"), .FieldToClass("nTransit_5"), eRemoteDB.Constants.intNull, String.Empty, String.Empty, String.Empty, .FieldToClass("sCliename"), String.Empty, String.Empty, .FieldToClass("sCheque"), .FieldToClass("nRequest_nu"), .FieldToClass("nSta_cheque"), .FieldToClass("nAmount"), .FieldToClass("dDat_propos"), .FieldToClass("dIssue_dat"), .FieldToClass("nConcept"), .FieldToClass("sClient"))
					
					lclsBank_acc.mclsCheque = New Cheque
					lclsBank_acc.mclsCheque.sCheque = .FieldToClass("sCheque")
					lclsBank_acc.mclsCheque.nRequest_nu = .FieldToClass("nRequest_nu")
					lclsBank_acc.mclsCheque.nSta_cheque = .FieldToClass("nSta_cheque")
					lclsBank_acc.mclsCheque.nAmount = .FieldToClass("nAmount")
					lclsBank_acc.mclsCheque.dDat_propos = .FieldToClass("dDat_propos")
					lclsBank_acc.mclsCheque.dIssue_Dat = .FieldToClass("dIssue_dat")
					lclsBank_acc.mclsCheque.nConcept = .FieldToClass("nConcept")
					lclsBank_acc.mclsCheque.sClient = .FieldToClass("sClient")
					sBanks = .FieldToClass("banco")
					sCurrencys = .FieldToClass("moneda")
					.RNext()
				Loop 
			Else
				FindOPC002 = False
			End If
		End With
		
FindOPC002_Err: 
		If Err.Number Then
			FindOPC002 = False
		End If
		'UPGRADE_NOTE: Object lclbank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclbank_acc = Nothing
		'UPGRADE_NOTE: Object lrecCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCheque = Nothing
		On Error GoTo 0
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
		'UPGRADE_NOTE: Object mclsCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCheque = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






