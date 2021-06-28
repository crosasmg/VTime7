Option Strict Off
Option Explicit On
Public Class CashBankAccMovs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: CashBankAccMovs.cls                      $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 18/02/04 3:55p                               $%'
	'% $Revision:: 31                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Local variable to hold collection
	'-Variables locales para la coleccion
	
	Private mCol As Collection
	
	'**-CO008 transaction variables
	'-Variables propias de la transacción CO008
	
	Public nTypPay As Integer '1
	Public dDoc_date As Date '2
	Public nBankAcc As Integer '3
	Public nBank As Double '8
	Public nTypCreCard As Integer '10
	Public nIntermed As Double '11
	Public nLed_compan As Integer '16
	Public sAccount As String '17
	Public sAux_accoun As String '18
	Public nCodAseg As Integer '21
	Public nTransac As Integer
	Public nChequeLocat As Integer
	Public nCash_Id As Integer
	
	'**-CO010 transaction variables
	'-Variables propias de la transacción CO010
	Public nTypDev As Integer
	Public nAccBankO As Integer
	Public nBankDes As Double
	Public nBk_agency As Integer
	Public nTypAcc As Integer
	Public sAccBankD As String
	
	'**-Common variables to CO008 and CA010 transactions
	'-Variables comunes a ambas transacciones (CO008 y CO010).
	
	'Public nExchange   As Double   '5
	'Public nExchangeUF As Double
	'Public nAmount     As Double   '6
	'Public nAmountLoc  As Double   '7
	'Public nAmountUF   As Double
	'Public sDocNumber  As String   '9
	'Public sClient     As String   '14
	
	Public nTotal As Double
	Public nCount As Integer
	Public nPaidAmount As Double
	Public nTotalAmount As Double
	Public nTotalAmountGen As Double
	Public nTotalAmountGenDec As Double
	Public nExchangeUF As Double
	Public sTable5008 As String
	Public nOperational As Short
    Public sDocument_old As String

	
	'**%Add: adds a new instance of the "CashBankAccMov" class to the collection
	'%Add: Añade una nueva instancia de la clase "CashBankAccMov" a la colección
	Public Function Add(ByVal objClass As CashBankAccMov) As CashBankAccMov
		If objClass Is Nothing Then
			objClass = New CashBankAccMov
		End If
        mCol.Add(objClass) ', "AG" & objClass.sType & objClass.nTypPay & objClass.nSequence & objClass.nTypPay & objClass.sClient & objClass.nTypPay & objClass.nIntermed & objClass.nBank & objClass.nAmount)
		Add = objClass
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As CashBankAccMov
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
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%Find: This method fills the collection with records from the collection schedule returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la relacion de cobros devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
    Public Function Find(ByVal sCodispl As String, ByVal sWinType As String, ByVal nRelation As Double, ByVal nAction As Integer, ByVal sColFormStatus As String, ByVal dCollect As Date, ByVal dValueDate As Date, ByVal sRelOrigi As String) As Boolean
        Dim lclsCashBankAccMov As CashBankAccMov
        Dim lrecinsReaCO008_10 As eRemoteDB.Execute
        Dim sTable As String
        Dim llngIndex As Integer

        lrecinsReaCO008_10 = New eRemoteDB.Execute

        With lrecinsReaCO008_10

            If nAction = ColformRef.TypeActionsSeqColl.cstrQuery And sColFormStatus = CStr(CollectionSeq.TypeStatusSeq.cstrComplete) Then
                .StoredProcedure = IIf(sCodispl = "CO008", "insReaCO008Finals", "insReaCO010Finals")
            ElseIf (nAction = ColformRef.TypeActionsSeqColl.cstrQuery And sColFormStatus = CStr(CollectionSeq.TypeStatusSeq.cstrNotComplete)) Or nAction = ColformRef.TypeActionsSeqColl.cstrUpdate Or nAction = ColformRef.TypeActionsSeqColl.cstrAdd Or nAction = ColformRef.TypeActionsSeqColl.cstrModify Then
                .StoredProcedure = IIf(sCodispl = "CO008", "insReaCO008Temp", "insReaCO010Temp")
            End If

            .Parameters.Add("sWinType", IIf(sWinType = String.Empty, "Normal", sWinType), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValuedate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                llngIndex = 0
                Do While Not .EOF
                    sTable = .FieldToClass("sTable")
                    '+ Se procesan los valores especiales del query. Siempre por lo menos devuelve un registro

                    'ESTO FALLABA 
                    If sTable = String.Empty Then
                        'If String.IsNullOrEmpty(String.Empty) Then

                        nPaidAmount = .FieldToClass("nPaidAmount", 0)
                        nTotalAmount = .FieldToClass("nTotalAmount", 0)
                        nTotalAmountGen = .FieldToClass("nAmount", 0)
                        nTotalAmountGenDec = .FieldToClass("nAmountDec", 0)
                        nExchangeUF = .FieldToClass("nExchange")
                        If sCodispl = "CO008" Then
                            sTable5008 = .FieldToClass("sTable5008")
                            nOperational = .FieldToClass("nOperational")
                        Else
                            sDocument_old = .FieldToClass("sDocument_old")
                        End If
                    Else
                        llngIndex = llngIndex + 1
                        lclsCashBankAccMov = New CashBankAccMov
                        lclsCashBankAccMov.sType = .FieldToClass("sTable")
                        On Error Resume Next
                        lclsCashBankAccMov.nSequence = .FieldToClass("nSequence")
                        On Error GoTo 0

                        Select Case sCodispl
                            Case "CO008"
                                '+De que tabla es la información
                                Select Case .FieldToClass("sTable")
                                    Case "CASH"
                                        Select Case .FieldToClass("nTypPay")
                                            '+Efectivo
                                            Case 1
                                                lclsCashBankAccMov.nTypPay = 1
                                                '+Cheque corriente
                                            Case 2
                                                lclsCashBankAccMov.nTypPay = 2
                                                '+Tarjeta de crédito
                                            Case 5
                                                lclsCashBankAccMov.nTypPay = 5
                                                '+Cheque diferido
                                            Case 7
                                                lclsCashBankAccMov.nTypPay = 7
                                                '+tarjeta de debito
                                            Case 10
                                                lclsCashBankAccMov.nTypPay = 10
                                                '+Bono Jujuy
                                            Case 11
                                                lclsCashBankAccMov.nTypPay = 7
                                                '+Bono Tucumán/Bono Jujuy
                                            Case 12
                                                lclsCashBankAccMov.nTypPay = 8
                                                '+Bono Cecor
                                            Case 13
                                                lclsCashBankAccMov.nTypPay = 9
                                                '+Vale Vista
                                            Case 28
                                                lclsCashBankAccMov.nTypPay = 28
                                                '+Bono de reconocimiento
                                            Case 29
                                                lclsCashBankAccMov.nTypPay = 29
                                                '+Complemento bono reconocimiento
                                            Case 30
                                                lclsCashBankAccMov.nTypPay = 30
                                                '+Bono exonerado político y adicional
                                            Case 31
                                                lclsCashBankAccMov.nTypPay = 31
                                                '+Primera renta privada
                                            Case 32
                                                lclsCashBankAccMov.nTypPay = 32
                                        End Select

                                    Case "BANK"
                                        Select Case .FieldToClass("nTypPay")
                                            '+Boleta de depósito
                                            Case 1, 3
                                                lclsCashBankAccMov.nTypPay = 3
                                        End Select

                                    Case "MOVE"
                                        Select Case .FieldToClass("nTypPay")
                                            '+Cargo a cta corriente del intermediario
                                            Case 3
                                                lclsCashBankAccMov.nTypPay = 11
                                                '+Cargo a cta. corriente del cliente
                                            Case 16
                                                lclsCashBankAccMov.nTypPay = 12
                                                '+Utilización de sobrantes de cobro
                                            Case 46
                                                lclsCashBankAccMov.nTypPay = 16
                                        End Select
                                End Select

                                lclsCashBankAccMov.sTypPay = .FieldToClass("sTypPay")
                                lclsCashBankAccMov.sClient = .FieldToClass("sClient")
                                lclsCashBankAccMov.sCliename = .FieldToClass("sCliename")

                                lclsCashBankAccMov.nIntermed = .FieldToClass("nIntermed")
                                lclsCashBankAccMov.sIntermed = .FieldToClass("sIntermed")

                                lclsCashBankAccMov.nBankAcc = .FieldToClass("nAcc_bank")
                                lclsCashBankAccMov.sAcc_bank = .FieldToClass("sAcc_bank")

                                lclsCashBankAccMov.nCurrency = .FieldToClass("nCurrency")
                                lclsCashBankAccMov.sCurrency = .FieldToClass("sCurrency")

                                lclsCashBankAccMov.nExchange = .FieldToClass("nExchange")
                                '+Fecha del documento
                                lclsCashBankAccMov.dDoc_date = .FieldToClass("dDoc_date")
                                '+Monto
                                lclsCashBankAccMov.nAmount = .FieldToClass("nAmount", 0)
                                lclsCashBankAccMov.nAmountDec = .FieldToClass("nAmountDec", 0)
                                '+Monto en moneda local
                                lclsCashBankAccMov.nAmountLoc = System.Math.Round(lclsCashBankAccMov.nAmount * lclsCashBankAccMov.nExchange, 0)
                                'lclsCashBankAccMov.nAmountUF = lclsCashBankAccMov.nAmount / nExchangeUF
                                lclsCashBankAccMov.nAmountUF = lclsCashBankAccMov.nAmountDec / nExchangeUF
                                '+Banco
                                lclsCashBankAccMov.nBank = .FieldToClass("nBank")
                                lclsCashBankAccMov.sBank = .FieldToClass("sBank")
                                '+No. del documento
                                lclsCashBankAccMov.sDocNumber = IIf(.FieldToClass("sDocNumbe") = "0", String.Empty, .FieldToClass("sDocNumbe"))
                                '+Tipo de tarjeta
                                lclsCashBankAccMov.nTypCreCard = .FieldToClass("nTypCreCard")
                                '+Compañía contable
                                lclsCashBankAccMov.nLed_compan = .FieldToClass("nLed_compan")
                                lclsCashBankAccMov.sLed_compan = .FieldToClass("sLed_compan")
                                '+Cuenta contable
                                lclsCashBankAccMov.sAccount = .FieldToClass("sAccount")
                                '+Cuenta auxiliar contable
                                lclsCashBankAccMov.sAux_accoun = .FieldToClass("sAux_accoun")
                                '+Código del Asegurador
                                lclsCashBankAccMov.nCodAseg = .FieldToClass("nCodAseg")
                                lclsCashBankAccMov.sCodAseg = .FieldToClass("sCodAseg")

                                lclsCashBankAccMov.nTransac = .FieldToClass("nTransac", 0)

                                lclsCashBankAccMov.nChequeLocat = .FieldToClass("nChequeLocat")
                                lclsCashBankAccMov.sChequeLocat = .FieldToClass("sChequeLocat")
                                lclsCashBankAccMov.nCash_Id = .FieldToClass("nCash_Id")

                            Case "CO010"
                                lclsCashBankAccMov.nTypDev = .FieldToClass("nTypDev")
                                lclsCashBankAccMov.sTypDev = .FieldToClass("sTypDev")

                                '+De que tabla es la información
                                Select Case .FieldToClass("sTable")
                                    Case "BANK"

                                    Case "CHECK"

                                    Case "MOVE"
                                        Select Case .FieldToClass("nTypDev")
                                            Case 19
                                                '+Abono a cta corriente del cliente
                                                lclsCashBankAccMov.nTypDev = 5
                                        End Select
                                End Select

                                lclsCashBankAccMov.nExchange = .FieldToClass("nExchange")
                                lclsCashBankAccMov.nAccBankO = .FieldToClass("nAcc_bank")
                                lclsCashBankAccMov.sAccBankO = .FieldToClass("sAcc_bank")
                                lclsCashBankAccMov.nBankDes = .FieldToClass("nBank_code")
                                lclsCashBankAccMov.sBank = .FieldToClass("sBank")
                                lclsCashBankAccMov.nBk_agency = .FieldToClass("nBk_agency")
                                lclsCashBankAccMov.sBk_agency = .FieldToClass("sBk_agency")
                                lclsCashBankAccMov.nTypAcc = .FieldToClass("nAcc_type")
                                lclsCashBankAccMov.sTypAcc = .FieldToClass("sAcc_type")
                                lclsCashBankAccMov.sAccBankD = .FieldToClass("sAcco_num")
                                lclsCashBankAccMov.nCurrency = .FieldToClass("nCurrency")
                                lclsCashBankAccMov.sCurrency = .FieldToClass("sCurrency")
                                lclsCashBankAccMov.sDocNumber = IIf(.FieldToClass("sCheque") = "0", String.Empty, .FieldToClass("sCheque"))
                                '+Monto
                                lclsCashBankAccMov.nAmount = .FieldToClass("nAmount", 0)
                                '+Monto en moneda local
                                lclsCashBankAccMov.nAmountLoc = lclsCashBankAccMov.nAmount * lclsCashBankAccMov.nExchange
                                lclsCashBankAccMov.nAmountLoc = System.Math.Round(lclsCashBankAccMov.nAmountLoc, 0)
                                lclsCashBankAccMov.nExchangeUF = nExchangeUF
                                lclsCashBankAccMov.nAmountUF = lclsCashBankAccMov.nAmount / lclsCashBankAccMov.nExchangeUF
                                '+sClient
                                lclsCashBankAccMov.sClient = .FieldToClass("sClient")
                                lclsCashBankAccMov.sCliename = .FieldToClass("sCliename")
                        End Select

                        Call Add(lclsCashBankAccMov)
                        lclsCashBankAccMov = Nothing
                    End If
                    .RNext()
                Loop
                .RCloseRec()
                Find = True
            Else
                Find = False
            End If
        End With

        nCount = llngIndex

        lrecinsReaCO008_10 = Nothing
    End Function
End Class






