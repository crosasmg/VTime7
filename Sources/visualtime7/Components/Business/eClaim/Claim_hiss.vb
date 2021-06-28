Option Strict Off
Option Explicit On
Public Class Claim_hiss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_hiss.cls                           $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 29/03/04 5:51p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	Private mOldClaim As Double
	Private mOldCase_num As Integer
	Private mOldDeman_type As Integer
	Private mOldOperdate As Date
	
	'**% Add: add a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nTransac As Integer, ByVal nAmount As Double, ByVal nOper_type As Integer, ByVal sCessiCoi As String, ByVal nCurrency As Integer, ByVal nExchange As Double, ByVal sExecuted As String, ByVal nInc_amount As Double, ByVal nIncometax As Double, ByVal sInd_aut As String, ByVal sInd_order As String, ByVal sInd_rev As String, ByVal nLoc_amount As Double, ByVal dOperdate As Date, ByVal sOrder_num As String, ByVal nPay_type As Integer, ByVal dPosted As Date, ByVal nServ_Order As Double, ByVal sClient As String, ByVal nBordereaux As Integer, ByVal nPay_form As Integer, ByVal sKey As String, ByVal sOper_type As String, Optional ByRef nAso As Integer = 0) As Claim_his
		
		Dim objNewMember As Claim_his
		objNewMember = New Claim_his
		
		
		With objNewMember
			'        .nStatusInstance = nStatusInstance
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.nTransac = nTransac
			.nAmount = nAmount
			.nOper_type = nOper_type
			.sCessiCoi = sCessiCoi
			.nCurrency = nCurrency
			.nExchange = nExchange
			.sExecuted = sExecuted
			.nInc_amount = nInc_amount
			.nIncometax = nIncometax
			.sInd_aut = sInd_aut
			.sInd_order = sInd_order
			.sInd_rev = sInd_rev
			.nLoc_amount = nLoc_amount
			.dOperdate = dOperdate
			.sOrder_num = sOrder_num
			.nPay_type = nPay_type
			.dPosted = dPosted
			.nServ_Order = nServ_Order
			.sClient = sClient
			.nBordereaux = nBordereaux
			.nPay_form = nPay_form
			.nAso = nAso
			.sOper_type = sOper_type
		End With
		
		mCol.Add(objNewMember, "CH" & nClaim & nCase_num & nDeman_type & nTransac)
		
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	'**%Update: This method updates the records of the collection in the table "Claim_his"
	'%Update: Permite actualizar los registros de la colección en la tabla "Claim_his"
	Public Function Update() As Boolean
		Dim lclsClaim_his As Claim_his
		
		'**+ Possibles values for nStatusInstance
		'+ Valores posibles para nStatusInstance
		'**+ 0: The record is new
		'+ 0: El registro es nuevo
		'**+ 1: The record exist in the table
		'+ 1: El registro ya existe en la tabla
		'**+ 2: The record exist it has to be actualized
		'+ 2: El registro ya existe, hay que actualizarlo
		'**+ 3: The record exist, it has to deleted
		'+ 3: El registro ya existe, hay que eliminarlo
		Update = True
		
		For	Each lclsClaim_his In mCol
			With lclsClaim_his
				If mOldClaim = VariantType.Null Then
					mOldClaim = .nClaim
				End If
				
				Update = .Update
				Select Case .nStatusInstance
					Case 0
						.nStatusInstance = 1
					Case 3
						If Update Then
							mCol.Remove(("CH" & .nClaim & .nCase_num & .nDeman_type & .nTransac))
						End If
				End Select
			End With
		Next lclsClaim_his
	End Function
	
	'**%Find: This method fills the collection with records from the table "Claim_his" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Claim_his" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find_SI010(ByVal Claim As Double, ByVal Case_num As Integer, ByVal Deman_type As Integer, ByVal Operdate As Date) As Boolean
		Dim lrecClaim_his As eRemoteDB.Execute
		Dim lclsDefField As eGeneral.GeneralFunction
		
		lrecClaim_his = New eRemoteDB.Execute
		lclsDefField = New eGeneral.GeneralFunction
		Dim nAso As Integer
		
		If Claim <> mOldClaim Or Case_num <> mOldCase_num Or Deman_type <> mOldDeman_type Or Operdate <> mOldOperdate Then
			
			With lrecClaim_his
				'** Parameters definition for the stored procedure 'insudb.reaClaim_hidSI010'
				'Definición de parámetros para stored procedure 'insudb.reaClaim_hisSI010'
				'**Data read on 01/30/2001 15.47.38
				'Información leída el 30/01/2001 15.47.38
				.StoredProcedure = "reaClaim_hisSI010"
				.Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", Case_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", Deman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dOperdate", Operdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						If ValMovInit(Claim, lclsDefField.LetValue(InStr(1, .FieldToClass("sKey"), "/", 1) - 1, eGeneral.GeneralFunction.eTypeData.TypNumInt), Deman_type, lclsDefField.LetValue(.FieldToClass("nTransac"), 1)) <> .FieldToClass("nTransac") Then
							nAso = 1
						Else
							nAso = 2
						End If
						
						Call Add(1, .FieldToClass("nClaim"), .FieldToClass("nCase_num"), .FieldToClass("nDeman_type"), .FieldToClass("nTransac"), .FieldToClass("nAmount"), .FieldToClass("nOper_type"), .FieldToClass("sCessicoi"), .FieldToClass("nCurrency"), .FieldToClass("nExchange"), .FieldToClass("sExecuted"), .FieldToClass("nInc_amount"), 0, .FieldToClass("sInd_aut"), .FieldToClass("sInd_order"), .FieldToClass("sInd_rev"), .FieldToClass("nLoc_amount"), .FieldToClass("dOperdate"), .FieldToClass("sOrder_num"), .FieldToClass("nPay_type"), .FieldToClass("dPosted"), .FieldToClass("nServ_order"), .FieldToClass("sClient"), .FieldToClass("nBordereaux"), eRemoteDB.Constants.intNull, .FieldToClass("sKey"), .FieldToClass("sOper_type"), nAso)
						.RNext()
					Loop 
					mOldClaim = Claim
					mOldCase_num = Case_num
					mOldDeman_type = Deman_type
					mOldOperdate = Operdate
					.RCloseRec()
					Find_SI010 = True
				Else
					Find_SI010 = False
				End If
			End With
		Else
			Find_SI010 = True
		End If
		lrecClaim_his = Nothing
	End Function
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Claim_his
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
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'**% ValMovInit: Found if the claim case generate movements
	'% ValMovInit: Busca si el caso de un siniestro genero movimientos
	Function ValMovInit(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nMovement As Integer) As Integer
		
		Dim lrecinsReaClaimMovInit As eRemoteDB.Execute
		
		lrecinsReaClaimMovInit = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insdb.insReaClaimMovInit'
		'Definición de parámetros para stored procedure 'insudb.insReaClaimMovInit'
		'**Data read on 08/02/2001 04:45:54 p.m.
		'Información leída el 02/08/2001 04:45:54 p.m.
		ValMovInit = False
		With lrecinsReaClaimMovInit
			.StoredProcedure = "insReaClaimMovInit"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovResult", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				ValMovInit = .Parameters("nMovResult").Value
			Else
				ValMovInit = 1
			End If
		End With
		lrecinsReaClaimMovInit = Nothing
		
	End Function
	
	'**% ValClaim_HisPay:
	'% ValClaim_HisPay: Busca si el siniestro tiene pagos realizados
	Function ValClaim_HisPay(ByVal nClaim As Double) As Short
		
		Dim lrecinsReaClaim_HisPay As eRemoteDB.Execute
		
		lrecinsReaClaim_HisPay = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insdb.insReaClaim_HisPay'
		'Definición de parámetros para stored procedure 'insudb.insReaClaim_HisPay'
		'**Data read on 08/02/2001 04:45:54 p.m.
		'Información leída el 02/08/2001 04:45:54 p.m.
		With lrecinsReaClaim_HisPay
			.StoredProcedure = "insReaClaim_HisPay"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				ValClaim_HisPay = .Parameters("nExist").Value
			Else
				ValClaim_HisPay = 2
			End If
		End With
		lrecinsReaClaim_HisPay = Nothing
	End Function
	
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






