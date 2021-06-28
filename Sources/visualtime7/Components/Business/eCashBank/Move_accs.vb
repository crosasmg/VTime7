Option Strict Off
Option Explicit On
Public Class Move_Accs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Move_Accs.cls                            $%'
	'% $Author:: Nvaplat53                                  $%'
	'% $Date:: 22/03/04 7:43p                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Auxiliaries variables
	'- Variables auxiliares
	
	Dim mintTyp_acc As Integer
	Dim mstrClient As String
	Dim mstrType_acc As String
	Dim mintCurrency As Integer
	Dim mdtmEffecdate As Date
	
	'**%Add: adds a new instance of the "Move_Acc" class to the collection
	'%Add: Añade una nueva instancia de la clase "Move_Acc" a la colección
	Public Function Add(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dOperdate As Date, ByVal nIdconsec As Integer, ByVal nIntermed As Integer, ByVal nAmount As Double, ByVal nBankext As Integer, ByVal nBranch As Integer, ByVal nCertif As Double, ByVal sCheque As String, ByVal nClaim As Double, ByVal nCredit As Double, ByVal nDebit As Double, ByVal sDescript As String, ByVal sManualMov As String, ByVal nPaynumbe As Integer, ByVal nPolicy As Double, ByVal nReceipt As Integer, ByVal sStatregt As String, ByVal nTransac As Integer, ByVal nTransactio As Integer, ByVal nType_move As Integer, ByVal nType_pay As Integer, ByVal nType_tran As Integer, ByVal nProvince As Integer, ByVal nIdDocument As Integer, ByVal nRequest_nu As Double, ByVal nBordereaux As Double, ByVal sProcess As String, ByVal sNumForm As String, ByVal nOrigCurr As Integer, ByVal nExchange As Double, ByVal sAutoriza As String, ByVal dValueDate As Date, ByVal nProduct As Integer, ByVal sNull_recor As String, ByVal sShort_des As String, ByVal nBalance As Double, ByVal nCreditot As Double, ByVal nDebitot As Double, Optional ByVal nIdreturn As Integer = 0, Optional ByVal nProcess As Integer = 0, Optional ByVal nSta_cheque As Integer = 0, Optional ByVal sProductDes As String = "", Optional ByVal sAcc_number As String = "", Optional ByVal sBank_des As String = "", Optional ByVal nNoteNum As Integer = 0, Optional ByVal nProponum As Integer = 0) As Move_Acc
		
		Dim objNewMember As Move_Acc
		objNewMember = New Move_Acc
		On Error GoTo Add_Err
		
		With objNewMember
			.nTyp_acco = nTyp_acco
			.sType_acc = sType_acc
			.sClient = sClient
			.nCurrency = nCurrency
			.dOperdate = dOperdate
			.nIdconsec = nIdconsec
			.nIntermed = nIntermed
			.nAmount = nAmount
			.nBankext = nBankext
			.nBranch = nBranch
			.nCertif = nCertif
			.sCheque = sCheque
			.nClaim = nClaim
			.nCredit = nCredit
			.nDebit = nDebit
			.sDescript = sDescript
			.sManualMov = sManualMov
			.nPaynumbe = nPaynumbe
			.nPolicy = nPolicy
			.nReceipt = nReceipt
			.sStatregt = sStatregt
			.nTransac = nTransac
			.nTransactio = nTransactio
			.nType_move = nType_move
			.nType_pay = nType_pay
			.nType_tran = nType_tran
			.nProvince = nProvince
			.nIdDocument = nIdDocument
			.nRequest_nu = nRequest_nu
			.nBordereaux = nBordereaux
			.sProcess = sProcess
			.sNumForm = sNumForm
			.nOrigCurr = nOrigCurr
			.nExchange = nExchange
			.sAutoriza = sAutoriza
			.dValueDate = dValueDate
			.nProduct = nProduct
			.sNull_recor = sNull_recor
			.sShort_des = sShort_des
			.nBalance = nBalance
			.nCreditot = nCreditot
			.nDebitot = nDebitot
			.nIdreturn = nIdreturn
			.nProcess = nProcess
			.nSta_cheque = nSta_cheque
			.sProductDes = sProductDes
			.sAcc_number = sAcc_number
			.sBank_des = sBank_des
			.nNoteNum = nNoteNum
			.nProponum = nProponum
		End With
		
		mCol.Add(objNewMember)
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
Add_Err: 
		On Error GoTo 0
	End Function
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Move_Acc" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindIntermedia(ByVal nIntermed As Integer) As Boolean
		Dim lrecReaMove_Acc As eRemoteDB.Execute
		
		
		On Error GoTo FindIntermedia_Err
		lrecReaMove_Acc = New eRemoteDB.Execute
		With lrecReaMove_Acc
			.StoredProcedure = "REAMove_AccINTER"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					Call Add(eRemoteDB.Constants.intNull, .FieldToClass("sType_acc"), .FieldToClass("sClient"), .FieldToClass("nCurrency"), System.Date.FromOADate(eRemoteDB.Constants.intNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nAmount"), eRemoteDB.Constants.intNull, .FieldToClass("nBranch"), .FieldToClass("nCertif"), String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nPolicy"), eRemoteDB.Constants.intNull, String.Empty, .FieldToClass("nTransac", 0), eRemoteDB.Constants.intNull, .FieldToClass("nType_move"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, .FieldToClass("dValueDate"), .FieldToClass("nProduct"), String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty)
					.RNext()
				Loop 
				.RCloseRec()
				FindIntermedia = True
				
			End If
		End With
		
		
FindIntermedia_Err: 
		If Err.Number Then
			FindIntermedia = False
		End If
		'UPGRADE_NOTE: Object lrecReaMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaMove_Acc = Nothing
		On Error GoTo 0
	End Function
	'**%Find: This method fills the collection with records from the table "Move_Acc" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Move_Acc" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByRef nTyp_acco As Integer, ByRef sType_acc As String, ByRef sClient As String, Optional ByRef nCurrency As Integer = 0, Optional ByRef nType_move As Integer = 0, Optional ByRef nCertif As Double = -1, Optional ByRef dOperdate As Date = #12:00:00 AM#) As Boolean
		Find = False
		
		Dim lrecReaMove_Acc As eRemoteDB.Execute
		
		lrecReaMove_Acc = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+ Parameter definitions for the stored procedure 'insudb.ReaMove_Acc'
		'**+ Data of 11/02/1999 04:17:48 PM
		'+ Definición de parámetros para stored procedure 'insudb.ReaMove_Acc'
		'+ Información leída el 02/11/1999 04:17:48 PM
		
		With lrecReaMove_Acc
			.StoredProcedure = "ReaMove_Acc1"
			
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If nCurrency <> 0 Then
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If nType_move <> 0 Then
				.Parameters.Add("nType_Move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nType_Move", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If nCertif <> -1 Then
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nCertif", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If Not IsNothing(dOperdate) Then
				.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dOperdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nTyp_acco"), .FieldToClass("sType_acc"), .FieldToClass("sClient"), .FieldToClass("nCurrency"), .FieldToClass("dOperdate"), .FieldToClass("nIdConsec"), intNull, .FieldToClass("nAmount"), intNull, .FieldToClass("nBranch"), .FieldToClass("nCertif"), String.Empty, intNull, .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), String.Empty, intNull, .FieldToClass("nPolicy"), .FieldToClass("nReceipt"), String.Empty, .FieldToClass("nTransac", 0), .FieldToClass("nTransactio"), .FieldToClass("nType_move"), .FieldToClass("nType_pay"), .FieldToClass("nType_tran", 0), intNull, intNull, intNull, intNull, String.Empty, String.Empty, .FieldToClass("nOrigCurr"), .FieldToClass("nExchange"), String.Empty, .FieldToClass("dValueDate"), .FieldToClass("nProduct"), String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty)
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			End If
			
		End With
		'UPGRADE_NOTE: Object lrecReaMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaMove_Acc = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
	End Function
	
	'**%FindMoveAcc_OPC012: This method fills the collection with records from the table "Move_Acc" returning TRUE or FALSE
	'**%depending on the existence of the records. This method is used for the page "OPC012"
	'%FindMoveAcc_OPC012: Este metodo carga la coleccion de elementos de la tabla "Move_Acc" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros. Este metodo es utlizado por la pagina "OPC012"
	Public Function FindMoveAcc_OPC012(ByVal nType_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dOperdate As Date) As Boolean
		
		On Error GoTo FindMoveAcc_OPC012_Err
		
		Dim lrecreaMove_Acc_vOPC012 As eRemoteDB.Execute
		
		lrecreaMove_Acc_vOPC012 = New eRemoteDB.Execute
		
		'**+ Parameter definitions for the stored procedure 'insudb.reaMove_Acc_vOPC012'
		'**+ Data of 03/30/2001 11:46:11 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaMove_Acc_vOPC012'
		'+Información leída el 30/03/2001 11:46:11 a.m.
		
		FindMoveAcc_OPC012 = False
		With lrecreaMove_Acc_vOPC012
			.StoredProcedure = "reaMove_Acc_vOPC012"
			.Parameters.Add("nTyp_acco", nType_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindMoveAcc_OPC012 = True
				Do While Not .EOF
					Call Add(.FieldToClass("nTyp_acco"), .FieldToClass("sType_acc"), .FieldToClass("sClient"), .FieldToClass("nCurrency"), .FieldToClass("dOperdate"), .FieldToClass("nIdConsec"), .FieldToClass("nIntermed"), .FieldToClass("nAmount"), .FieldToClass("nBankext"), .FieldToClass("nBranch"), .FieldToClass("nCertif"), .FieldToClass("sCheque"), .FieldToClass("nClaim"), .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), .FieldToClass("sManualMov"), .FieldToClass("nPaynumbe"), .FieldToClass("nPolicy"), .FieldToClass("nReceipt"), .FieldToClass("sStatregt"), .FieldToClass("nTransac"), .FieldToClass("nTransactio"), .FieldToClass("nType_move"), .FieldToClass("nType_pay"), .FieldToClass("nType_tran"), .FieldToClass("nProvince"), .FieldToClass("nIdDocument"), .FieldToClass("nRequest_nu"), .FieldToClass("nBordereaux"), .FieldToClass("sProcess"), .FieldToClass("sNumForm"), .FieldToClass("nOrigCurr"), .FieldToClass("nExchange"), .FieldToClass("sAutoriza"), .FieldToClass("dValueDate"), .FieldToClass("nProduct"), .FieldToClass("sNull_recor"), strNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull,  ,  ,  , .FieldToClass("sProductDes"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaMove_Acc_vOPC012 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Acc_vOPC012 = Nothing
		
FindMoveAcc_OPC012_Err: 
		If Err.Number Then
			FindMoveAcc_OPC012 = False
		End If
	End Function
	
	
	'**% Find_CurrAccInq: Query a current account in the transactions table of a
	'**%current account (Move_Acc) by product, branch, policy and certificate or by client code
	'% Find_CurrAccInq: Realiza una consulta de una cuenta corriente en la tabla de "movimientos de
	'% cuentas corrientes" (Move_Acc) por Producto, ramo, poliza y certificado, o por codigo
	'% cliente
	Public Function Find_CurrAccInq(ByVal dOperdate As Date, ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCurrency As Integer) As Boolean
		
		Dim lrecreaMove_Acc_OPC010_a As eRemoteDB.Execute
		
		lrecreaMove_Acc_OPC010_a = New eRemoteDB.Execute
		
		On Error GoTo Find_CurrAccInq_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.reaMove_Acc_OPC010_a'
		'+Definición de parámetros para stored procedure 'insudb.reaMove_Acc_OPC010_a'
		'**+ Data of 03/20/2001 13:23:24
		'+Información leída el 20/03/2001 13:23:24
		
		With lrecreaMove_Acc_OPC010_a
			.StoredProcedure = "reaMove_Acc_OPC010_a"
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					If nPolicy = 0 Or nPolicy = eRemoteDB.Constants.intNull Then
						Call Add(nTyp_acco, sType_acc, sClient, nCurrency, .FieldToClass("dOperdate"), .FieldToClass("nIdConsec"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nBranch, nCertif, String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nPolicy"), eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nType_move"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nBordereaux"), String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, dtmNull, nProduct, String.Empty, .FieldToClass("sShort_des"), .FieldToClass("nBalance"), .FieldToClass("Creditot"), .FieldToClass("Debitot"),  ,  ,  ,  ,  ,  , .FieldToClass("nNotenum"), .FieldToClass("nProponum"))
					Else
						Call Add(nTyp_acco, sType_acc, .FieldToClass("sClient"), nCurrency, .FieldToClass("dOperdate"), .FieldToClass("nIdConsec"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nBranch, nCertif, String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), String.Empty, eRemoteDB.Constants.intNull, nPolicy, eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nType_move"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, dtmNull, nProduct, String.Empty, .FieldToClass("sShort_des"), .FieldToClass("nBalance"), .FieldToClass("Creditot"), .FieldToClass("Debitot"),  ,  ,  ,  ,  ,  , .FieldToClass("nNotenum"))
					End If
					.RNext()
				Loop 
				.RCloseRec()
				Find_CurrAccInq = True
			Else
				Find_CurrAccInq = False
			End If
		End With
		
		
Find_CurrAccInq_Err: 
		If Err.Number Then
			Find_CurrAccInq = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_Acc_OPC010_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Acc_OPC010_a = Nothing
		On Error GoTo 0
	End Function
	
	'**%FindMoveAcc_OPC011: This method fills the collection with records from the table "Move_Acc" returning TRUE or FALSE
	'**%depending on the existence of the records. This method is used for the page "OPC011"
	'%FindMoveAcc_OPC011: Este metodo carga la coleccion de elementos de la tabla "Move_Acc" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros. Este metodo es utlizado por la pagina "OPC011"
	Public Function FindMoveAcc_OPC011(ByVal nTyp_acc As Integer, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		'**- Variable that contain the result of a records search
		'- Variable que contiene el resultado de la busqueda de registros
		
		Static lblnRead As Boolean
		
		On Error GoTo FindMoveAcc_OPC011_Err
		If lblnFind Or mintTyp_acc <> nTyp_acc Or mstrClient <> sClient Or mintCurrency <> nCurrency Or mdtmEffecdate <> dEffecdate Then
			
			mintTyp_acc = nTyp_acc
			mstrClient = sClient
			mintCurrency = nCurrency
			mdtmEffecdate = dEffecdate
			
			lrecMove_Acc = New eRemoteDB.Execute
			With lrecMove_Acc
				.StoredProcedure = "reaMove_Acc_vOPC011"
				.Parameters.Add("nTyp_acco", nTyp_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_acc", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dOperdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mCol = Nothing
					mCol = New Collection
					Do While Not .EOF
						Call Add(.FieldToClass("nTyp_acco"), .FieldToClass("sType_acc"), .FieldToClass("sClient"), .FieldToClass("nCurrency"), .FieldToClass("dOperdate"), .FieldToClass("nIdConsec"), .FieldToClass("nIntermed"), .FieldToClass("nAmount"), .FieldToClass("nBankext"), .FieldToClass("nBranch"), .FieldToClass("nCertif"), .FieldToClass("sCheque"), .FieldToClass("nClaim"), .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), .FieldToClass("sManualMov"), .FieldToClass("nPaynumbe"), .FieldToClass("nPolicy"), .FieldToClass("nReceipt"), .FieldToClass("sStatregt"), .FieldToClass("nTransac"), .FieldToClass("nTransactio"), .FieldToClass("nType_move"), .FieldToClass("nType_pay"), .FieldToClass("nType_tran"), .FieldToClass("nProvince"), .FieldToClass("nIdDocument"), .FieldToClass("nRequest_nu"), .FieldToClass("nBordereaux"), .FieldToClass("sProcess"), .FieldToClass("sNumForm"), .FieldToClass("nOrigCurr"), .FieldToClass("nExchange"), .FieldToClass("sAutoriza"), .FieldToClass("dValueDate"), .FieldToClass("nProduct"), .FieldToClass("sNull_recor"), String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sProductDes"))
						.RNext()
					Loop 
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecMove_Acc = Nothing
		End If
		
		FindMoveAcc_OPC011 = lblnRead
		
FindMoveAcc_OPC011_Err: 
		If Err.Number Then
			FindMoveAcc_OPC011 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindMoveAcc_OPC015: This method fills the collection with records from the table "Move_Acc" returning TRUE or FALSE
	'**%depending on the existence of the records. This method is used for the page "OPC015"
	'%FindMoveAcc_OPC015: Este metodo carga la coleccion de elementos de la tabla "Move_Acc" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros. Este metodo es utlizado por la pagina "OPC015"
	Public Function FindMoveAcc_OPC015(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		Dim lrecMove_Acc As eRemoteDB.Execute
		
		'**- Variable that contains the result of the records search
		'- Variable que contiene el resultado de la busqueda de registros
		Static lblnRead As Boolean
		
		If lblnFind Or mintTyp_acc <> nTyp_acco Or mstrType_acc <> sType_acc Or mstrClient <> sClient Or mintCurrency <> nCurrency Or mdtmEffecdate <> dEffecdate Then
			
			mintTyp_acc = nTyp_acco
			mstrType_acc = sType_acc
			mstrClient = sClient
			mintCurrency = nCurrency
			mdtmEffecdate = dEffecdate
			
			lrecMove_Acc = New eRemoteDB.Execute
			With lrecMove_Acc
				.StoredProcedure = "reaMove_Acc_vOPC015"
				.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dOperdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_move", 5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mCol = Nothing
					mCol = New Collection
					Do While Not .EOF
						Call Add(.FieldToClass("nTyp_acco"), .FieldToClass("sType_acc"), .FieldToClass("sClient"), .FieldToClass("nCurrency"), .FieldToClass("dOperdate"), .FieldToClass("nIdConsec"), .FieldToClass("nIntermed"), .FieldToClass("nAmount"), .FieldToClass("nBankext"), .FieldToClass("nBranch"), .FieldToClass("nCertif"), .FieldToClass("sCheque"), .FieldToClass("nClaim"), .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), .FieldToClass("sManualMov"), .FieldToClass("nPaynumbe"), .FieldToClass("nPolicy"), .FieldToClass("nReceipt"), .FieldToClass("sStatregt"), .FieldToClass("nTransac"), .FieldToClass("nTransactio"), .FieldToClass("nType_move"), .FieldToClass("nType_pay"), .FieldToClass("nType_tran"), .FieldToClass("nProvince"), .FieldToClass("nIdDocument"), .FieldToClass("nRequest_nu"), .FieldToClass("nBordereaux"), .FieldToClass("sProcess"), .FieldToClass("sNumForm"), .FieldToClass("nOrigCurr"), .FieldToClass("nExchange"), .FieldToClass("sAutoriza"), .FieldToClass("dValueDate"), .FieldToClass("nProduct"), .FieldToClass("sNull_recor"), String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty)
						.RNext()
					Loop 
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
				
			End With
			
			'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecMove_Acc = Nothing
			
		End If
		FindMoveAcc_OPC015 = lblnRead
		
	End Function
	
	'**% Find_QPayOrderMov:  Performs a query of a current account in the transactions table of
	'**% current accounts (Move_Acc) by product, branch, policy and certificate or by code
	'% Find_QPayOrderMov: Realiza una consulta de una cuenta corriente en la tabla de "movimientos de
	'% cuentas corrientes" (Move_Acc) por Producto, ramo, poliza y certificado, o por codigo
	'% cliente
	Public Function Find_QPayOrderMov(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dOperdate As Date) As Boolean
		
		Dim lrecreaMove_Acc_vOPC014 As eRemoteDB.Execute
		
		lrecreaMove_Acc_vOPC014 = New eRemoteDB.Execute
		
		On Error GoTo Find_QPayOrderMov_Err
		
		'**+ Parameter definitions for the stored procedure 'insudb.reaMove_Acc_vOPC014'
		'**+ Data of 03/23/2001 14:42:17
		'+Definición de parámetros para stored procedure 'insudb.reaMove_Acc_vOPC014'
		'+Información leída el 23/03/2001 14:42:17
		
		With lrecreaMove_Acc_vOPC014
			.StoredProcedure = "reaMove_Acc_vOPC014"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(nTyp_acco, sType_acc, sClient, nCurrency, .FieldToClass("dOperdate"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, System.Math.Abs(.FieldToClass("nAmount")), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sCheque"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nDebit"), String.Empty, .FieldToClass("sManualMov"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, IIf(.FieldToClass("nRequest_nu") = 0, eRemoteDB.Constants.intNull, .FieldToClass("nRequest_nu")), eRemoteDB.Constants.intNull, String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, dtmNull, eRemoteDB.Constants.intNull, String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull,  ,  , .FieldToClass("nSta_cheque"),  , .FieldToClass("sAcc_number"), .FieldToClass("sBank_des"))
					.RNext()
				Loop 
				.RCloseRec()
				Find_QPayOrderMov = True
			Else
				Find_QPayOrderMov = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaMove_Acc_vOPC014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Acc_vOPC014 = Nothing
		
Find_QPayOrderMov_Err: 
		If Err.Number Then
			Find_QPayOrderMov = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find_OPC013: Este procedimiento verifica la existencia de los registros
	'               en la tabla de "detalles de movimientos de cuentas corrientes
	'               de intermediarios" (comm_det) para una cuenta corriente (dada como
	'               parametro) y que correspondan a movimientos de primas.(nreceipt con valor)
	Public Function Find_OPC013(ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, ByVal dOperdate As Date) As Boolean
		
		Dim lclsRemote As eRemoteDB.Execute
		Dim mclsMove_Acc As eCashBank.Move_Acc
		
		On Error GoTo Find_OPC013_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "rea_opc013"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					mclsMove_Acc = New Move_Acc
					mclsMove_Acc.dOperdate = .FieldToClass("dOperdate")
					mclsMove_Acc.sDescript = .FieldToClass("sDescript")
					mclsMove_Acc.nAmount = .FieldToClass("nAmount")
					mclsMove_Acc.sProductDes = .FieldToClass("sProductDes")
					mclsMove_Acc.nReceipt = .FieldToClass("nReceipt")
					mclsMove_Acc.nPolicy = .FieldToClass("nPolicy")
					mclsMove_Acc.nCertif = .FieldToClass("nCertif")
					mclsMove_Acc.nType_move = .FieldToClass("nType_Move")
					Call Add(nTyp_acco, sType_acc, sClient, nCurrency, .FieldToClass("dOperdate"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, System.Math.Abs(.FieldToClass("nAmount")), eRemoteDB.Constants.intNull, .FieldToClass("nBranch"), .FieldToClass("nCertif"), String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sDescript"), String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nPolicy"), .FieldToClass("nReceipt"), String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nType_Move"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, CStr(eRemoteDB.Constants.intNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, dtmNull, eRemoteDB.Constants.intNull, String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull,  ,  ,  , .FieldToClass("sProductDes"))
					'UPGRADE_NOTE: Object mclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mclsMove_Acc = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find_OPC013 = True
			End If
		End With
		
Find_OPC013_Err: 
		If Err.Number Then
			Find_OPC013 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object mclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsMove_Acc = Nothing
	End Function
	
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Move_Acc
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
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
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
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
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






