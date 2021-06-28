Option Strict Off
Option Explicit On
Public Class Curr_accs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Curr_accs.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'**-local variable to hold collection
	'+Se define la variable mCol para contener la coleccion
	
	Private mCol As Collection
	Public sCliename As String
	
	'**%Add: adds a new instance of the "Curr_acc" class to the collection
	'%Add: Añade una nueva instancia de la clase "Curr_acc" a la colección
	Public Function Add(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nBalance As Integer, ByVal nCredit As Integer, ByVal nDebit As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal nLed_compan As Integer, ByVal sAccount As String, ByVal sCertype As String, ByVal nCompany As Integer, ByVal nProduct As Integer, ByVal sAux_accoun As String, ByVal nBranch As Integer, ByVal nCertif As Double, ByVal nPolicy As Double) As Curr_acc
		'**+ create a new object "Curr_acc"
		'+ crea un  nuevo objeto "Curr_acc"
		Dim objNewMember As Curr_acc
		objNewMember = New Curr_acc
		
		With objNewMember
			.nTyp_acco = nTyp_acco
			.sType_acc = sType_acc
			.sClient = sClient
			.nCurrency = nCurrency
			.nBalance = nBalance
			.nCredit = nCredit
			.nDebit = nDebit
			.dEffecdate = dEffecdate
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.nLed_compan = nLed_compan
			.sAccount = sAccount
			.sCertype = sCertype
			.nCompany = nCompany
			.nProduct = nProduct
			.sAux_accoun = sAux_accoun
			.nBranch = nBranch
			.nCertif = nCertif
			.nPolicy = nPolicy
		End With
		'set the properties passed into the method
		'If Len(sKey) = 0 Then
		mCol.Add(objNewMember)
		'Else
		'    mCol.Add objNewMember, sKey
		'End If
		
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		
	End Function
	'%FindComm_det_vOPC013: Este procedimiento verifica la existencia de los registros
	'                       en la tabla de "detalles de movimientos de cuentas corrientes
	'                       de intermediarios" (comm_det) para una cuenta corriente (dada como
	'                       parametro) y que correspondan a movimientos de primas.(nreceipt con valor)
	Public Function FindComm_det_vOPC013(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dOperdate As Date) As Boolean
		'**-variable definition: lrec_Curr_acc is going to be used as a cursor.
		'-Se define la variable lrec_Curr_acc que se utilizará como cursor.
		Dim lrec_Curr_acc As eRemoteDB.Execute
		Dim lclsCurr_acc As eCashBank.Curr_acc
		
		On Error GoTo FindComm_det_vOPC013_Err
		
		lclsCurr_acc = New eCashBank.Curr_acc
		lrec_Curr_acc = New eRemoteDB.Execute
		
		'**+Excecute the store procedure that found how many currency has the client.
		'+Se ejecuta el store procedure que busca cuantas monedas tiene el cliente
		With lrec_Curr_acc
			.StoredProcedure = "reaComm_det_vOPC013"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindComm_det_vOPC013 = True
				
				Do While Not .EOF
					lclsCurr_acc = Add(eRemoteDB.Constants.intNull, String.Empty, .FieldToClass("sclient"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dtmNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, .FieldToClass("nbranch"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
					
					lclsCurr_acc.Move_Acc_Renamed = New Move_Acc
					
					lclsCurr_acc.Move_Acc_Renamed.dOperdate = .FieldToClass("dOperdate")
					lclsCurr_acc.Move_Acc_Renamed.sDescript = .FieldToClass("sDescript")
					lclsCurr_acc.Move_Acc_Renamed.nAmount = .FieldToClass("nAmount")
					lclsCurr_acc.Move_Acc_Renamed.sProductDes = .FieldToClass("sProductDes")
					lclsCurr_acc.Move_Acc_Renamed.nReceipt = .FieldToClass("nReceipt")
					lclsCurr_acc.Move_Acc_Renamed.nPolicy = .FieldToClass("nPolicy")
					lclsCurr_acc.Move_Acc_Renamed.nCertif = .FieldToClass("nCertif")
					lclsCurr_acc.Move_Acc_Renamed.sBranchDes = .FieldToClass("sBranchDes")
					
					lclsCurr_acc.Comm_det_Renamed = New Comm_det
					
					lclsCurr_acc.Comm_det_Renamed.nTyp_amount = .FieldToClass("nTyp_amount")
					lclsCurr_acc.Comm_det_Renamed.sInd_credeb = .FieldToClass("sInd_credeb")
					
					.RNext()
				Loop 
			Else
				FindComm_det_vOPC013 = False
			End If
			
		End With
		
		
FindComm_det_vOPC013_Err: 
		If Err.Number Then
			FindComm_det_vOPC013 = False
		End If
		'UPGRADE_NOTE: Object lclsCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurr_acc = Nothing
		'UPGRADE_NOTE: Object lrec_Curr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_Curr_acc = Nothing
		On Error GoTo 0
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Curr_acc
		Get
			'used when referencing an element in the collection
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
			'**+used when retrieving the number of elements in the
			'+collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'**+this property allows you to enumerate
			'**+this collection with the For...Each syntax
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
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'**+destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






