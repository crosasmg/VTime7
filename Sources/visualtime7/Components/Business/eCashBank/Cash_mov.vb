Option Strict Off
Option Explicit On
Public Class Cash_mov
	'%-------------------------------------------------------%'
	'% $Workfile:: Cash_mov.cls                             $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 7/04/04 15.59                                $%'
	'% $Revision:: 115                                      $%'
	'%-------------------------------------------------------%'
	
	'**-Possible global values to be used in the cash and bank module as a type
	'**-of cash movement (table 78)
	'-Posibles valores globales a usar en el módulo de caja y banco como tipo
	'-de movimiento de caja. (table78)
	
	Enum CashTypMov
		clngMCCash = 1
		clngMCCheq = 2
		clngMCTrans = 3
		clngMCcn = 4
		clngMCCredCard = 5
		clngMCCheqDep = 6
		clngMCCashDep = 7
		clngMCRetCheq = 8
		clngMCCreCardDep = 9
		clngMCCDifCheq = 10
	End Enum
	
	'Column_name                   Type       Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nAcc_cash As Integer 'smallint     2           5     0     no                                  (n/a)                               (n/a)
	Public nCurrency As Integer 'smallint     2           5     0     no                                  (n/a)                               (n/a)
	Public nOffice As Integer 'smallint     2           5     0     no                                  (n/a)                               (n/a)
	Public nTransac As Integer 'smallint     2           5     0     no                                  (n/a)                               (n/a)
	Public dEffecdate As Date 'datetime     8                       no                                  (n/a)                               (n/a)
	Public nAmount As Double 'decimal      9           14    2     yes                                 (n/a)                               (n/a)
	Public nAcc_bank As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nBank_code As Integer 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public nBranch_Led As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public dCard_expir As Date 'datetime     8                       yes                                 (n/a)                               (n/a)
	Public sCard_num As String 'char         20                      yes                                 no                                  yes
	Public nCard_typ As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nCl_transac As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nClaim As Double 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public nCompanyc As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nVoucher_le As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nVoucher As Integer 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public nConcept As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nContrat As Double 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public dDat_return As Date 'datetime     8                       yes                                 (n/a)                               (n/a)
	Public sDep_number As String 'char         12                      yes                                 no                                  yes
	Public dDoc_date As Date 'datetime     8                       yes                                 (n/a)                               (n/a)
	Public sDocnumbe As String 'char         10                      yes                                 no                                  yes
	Public nDraft As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nExchange As Double 'decimal      9           10    6     yes                                 (n/a)                               (n/a)
	Public nIntermed As Integer 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public nMov_type As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public sNull_movem As String 'char         1                       yes                                 no                                  yes
	Public sNull_recor As String 'char         1                       yes                                 no                                  yes
	Public dNulldate As Date 'datetime     8                       yes                                 (n/a)                               (n/a)
	Public nPaynumbe As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public nReceipt As Integer 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public nYear_month As Integer 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public nBordereaux As Double 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public nTyp_acco As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public sClient As String 'char         14                      yes                                 no                                  yes
	Public sNumForm As String 'char         12                      yes                                 no                                  yes
	Public sType_acc As String 'char         1                       yes                                 no                                  yes
	Public sDescript As String 'char         60                      yes                                 no                                  yes
	Public nNoteNum As Integer 'int          4           10    0     yes                                 (n/a)                               (n/a)
	Public sInter_pay As String 'char         14                      yes                                 no                                  yes
	Public dLedger_dat As Date 'datetime     8                       yes                                 (n/a)                               (n/a)
	Public nUser_sol As Integer 'smallint     2           5     0     no                                  (n/a)                               (n/a)
	Public nCompany As Integer 'smallint     2           5     0     yes                                 (n/a)                               (n/a)
	Public sProcess As String 'char         1                       yes                                 no                                  yes
	Public nCashNum As Integer 'Smallint     2           5           no                                  no                                  no
	Public dRealDep As Date
	Public nChequeLocat As Integer
	Public nInsur_area As Integer
	Public dPosted As Date
	Public nCod_Agree As Integer
	Public nInputChannel As Integer
	Public dValDate As Date
	Public Sr_DepNum As String
	Public nOri_Curr As Integer
	Public nOri_Amount As Double
	Public nSupport_Id As Integer
	Public nTypesupport As Integer
	Public nDocSupport As Double
	Public dCollection As Date
	Public nCheque_Stat As Integer
	Public nCash_id As Integer
	Public nFin_Int As Double
	Public nBranch As Integer
	Public nProduct As Integer
	Public nBank_Agree As Integer
	Public nProponum As Double
	Public nBulletins As Double
	Public nCase_Num As Integer
	Public nDeman_Type As Integer
	Public nOfficeAgen As Integer
	Public nAgency As Integer
    Public dCompdate As Date
    Public nPolicy As Double
	
	'- Variables de descripcion
	Public sDes_Bank As String
	Public sDes_Cheloc As String
	Public sDes_Chestat As String
	Public sDes_Concep As String
	Public sDes_Office As String
	Public sDes_Ori_Curr As String
	Public sDesCard_type As String
	
	'**-Auxiliary variables
	'-Variables auxiliares
	Public nResponse As Integer
	Public nUpdate As Integer
	Public sMov_typeDes As String
	Public nAction As Integer
	Public nTransacNull As Integer
	Public nCount As Integer
	Public sBank_descript As String
	Public sCard_descript As String
	Public sLocal_descript As String
	Public nRequest_nu As Double
	Public sBenef_name As String
	Public sInter_name As String
	Public sUser_name As String
	Public dIssue_Dat As Date
	Public nNullcode As Integer
	Public sPay_freq As String
	Public nSta_cheque As Integer
	Public nCheque_bordereaux As Integer
	Public nQ_pays As Integer
	Public dStat_date As Date
	Public nUpdAvailable As Integer
	Public nCurrencyPay As Integer
	Public nCollect_P As Integer
	
	'**-Variables to be used in the Execution function routines (Post)
	'-Utilizadas en las rutinas de las funciones de Ejecución (Post)
	
	Public nAmountND As Double
	Public sCurr_descript As String
	Public nCash_Amount As Double
	Private mclsCash_mov As eCashBank.Cash_mov
	
	'**-Variables to be used in the method FindCashMovInterchange
	'-Utilizadas en el método FindCashMovInterchange
	Public nInterCount As Integer
	Public nReplacedCount As Integer
	
	Public sEffecdate As String
	Public sTransac As String
	Public nOptToDeposit As Integer
	Public sSel As String
	Public sDigit As String
	Public sCliename As String
	Public sConcept As String
	Public sCurrAcc As String
	Public sProduct As String
    Public sCompany As String
    Public sCashnum As String
    Public sOffice As String
	
	Public sKey As String
	'**%ADD: This method is in charge of adding new records to the table "Cash_mov".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Cash_mov". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreCash_mov As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lreccreCash_mov = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.creCash_mov'
		'**+Information read on November 21, 2000  9:19:14 a.m.
		'+Definición de parámetros para stored procedure 'insudb.creCash_mov'
		'+Información leída el 21/11/2000 9:19:14 AM
		
		With lreccreCash_mov
			.StoredProcedure = "creCash_mov"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCard_expir", dCard_expir, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCard_num", sCard_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCard_typ", nCard_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCl_transac", nCl_transac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompanyc", nCompanyc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher_le", nVoucher_le, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDep_number", sDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDoc_date", dDoc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNull_movem", sNull_movem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNull_recor", sNull_recor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_month", nYear_month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_pay", sInter_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_sol", nUser_sol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteNum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUpdate", nUpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChequeLocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInputChannel", nInputChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_Agree", nBank_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollection", dCollection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypesupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSupport_Id", nSupport_Id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOri_Curr", nOri_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOri_Amount", nOri_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFin_Int", nFin_Int, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCheque_Stat", nCheque_Stat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nCase_Num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_Type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_id", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCash_mov = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**%AddIncommingCash: This method creates a new cash entry
	'%AddIncommingCash: Este metodo realiza una nueva entrada de dinero en caja
	Public Function AddIncommingCash(ByVal nAcc_cash As Integer, ByVal nAcc_bank As Integer, ByVal nCurrency As Integer, ByVal nOffice As Integer, ByVal dEffecdate As Date, ByVal nAmount As Double, ByVal nBank_code As Integer, ByVal dCard_expir As Date, ByVal sCard_num As String, ByVal nCard_typ As Integer, ByVal nClaim As Double, ByVal nCompanyc As Integer, ByVal nVoucher_le As Integer, ByVal nVoucher As Integer, ByVal nConcept As Integer, ByVal nContrat As Double, ByVal dDoc_date As Date, ByVal sDocnumbe As String, ByVal nDraft As Integer, ByVal nIntermed As Integer, ByVal nMov_type As Integer, ByVal nUsercode As Integer, ByVal nBordereaux As Double, ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nProponum As Double, ByVal nAction As Integer, ByVal nTransacNull As Integer, ByVal nCashNum As Integer, ByVal nCompany As Integer, ByVal nChequeLocat As Integer, ByVal nInputChannel As Integer, ByVal nCod_Agree As Integer, ByVal nBank_Agree As Integer, ByVal dCollection As Date, ByVal nTypesupport As Integer, ByVal nSupport_Id As Integer, ByVal nBulletin As Double, ByVal dValDate As Date, ByVal nOri_Curr As Integer, ByVal nOri_Amount As Double, ByVal nFin_Int As Double, ByVal nCash_id As Integer, Optional ByVal nCase_Num As Integer = 0, Optional ByVal nDeman_Type As Integer = 0, Optional ByVal nNoteNum As Double = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByRef nInsur_area As Integer = 0) As Boolean
		'**-The variable lrecinsCreIncommingCash is declared
		'-Se define la variable lrecinsCreIncommingCash
		Dim lrecinsCreIncommingCash As eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.insCreIncommingCash'
		'**+Information read on February 06, 2001 15:59:14
		'+Definición de parámetros para stored procedure 'insudb.insCreIncommingCash'
		'+Información leída el 06/02/2001 15:59:14
		On Error GoTo AddIncommingCash_Err
		lrecinsCreIncommingCash = New eRemoteDB.Execute
		With lrecinsCreIncommingCash
			.StoredProcedure = "insCreIncommingCash"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("In_nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCard_expir", dCard_expir, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCard_num", sCard_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCard_typ", nCard_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompanyc", nCompanyc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher_le", nVoucher_le, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDoc_date", dDoc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("In_sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransacNull", nTransacNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChequelocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInputChannel", nInputChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_Agree", nBank_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollection", dCollection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSupport_id", nSupport_Id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletin", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOri_Curr", nOri_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOri_Amount", nOri_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFin_Int", nFin_Int, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_Type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteNum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nTransac = .FieldToClass("nTransac")
				Me.nVoucher = .FieldToClass("nVoucher")
				Me.nCash_id = .FieldToClass("nCash_Id")
				AddIncommingCash = True
				.RCloseRec()
			End If
		End With
		
AddIncommingCash_Err: 
		If Err.Number Then
			AddIncommingCash = False
		End If
		'UPGRADE_NOTE: Object lrecinsCreIncommingCash may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCreIncommingCash = Nothing
		On Error GoTo 0
	End Function
	
	'**%FindByDeposit: This method returns TRUE or FALSE depending if the records exists in the table "Cash_mov"
	'%FindByDeposit: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Cash_mov"
	Public Function FindByDeposit(ByVal nMov_type As Integer, ByVal sDep_number As String, ByVal nAcc_bank As Integer, ByVal sDocnumbe As String) As Boolean
		Dim lrecreaCash_mov_v2 As eRemoteDB.Execute
		
		On Error GoTo FindByDeposit_Err
		
		lrecreaCash_mov_v2 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaCash_mov_v2'
		'+Información leída el 05/03/2001 04:07:42 p.m.
		
		With lrecreaCash_mov_v2
			.StoredProcedure = "reaCash_mov_v2"
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDep_number", sDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nMov_type = .FieldToClass("nMov_type")
				nTransac = .FieldToClass("nTransac")
				dDoc_date = .FieldToClass("dDoc_date")
				nCompany = .FieldToClass("nCompany")
				nChequeLocat = .FieldToClass("nChequelocat")
				nBank_code = .FieldToClass("nBank_code")
				sBank_descript = .FieldToClass("sdescript")
				nCash_Amount = .FieldToClass("nCash_Amoun")
				dRealDep = .FieldToClass("dRealDep")
				.RCloseRec()
				FindByDeposit = True
			Else
				FindByDeposit = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCash_mov_v2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_mov_v2 = Nothing
		
FindByDeposit_Err: 
		If Err.Number Then
			FindByDeposit = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindByDocument: This method returns TRUE or FALSE depending if the records exists in the table "Cash_mov"
	'%FindByDocument: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Cash_mov"
	Public Function FindByDocument(ByVal nMov_type As Integer, ByVal sDocnumbe As String, ByVal nBank_code As Integer) As Boolean
		
		'**-The variable lrecreaCash_mov_v1 is declared
		'-Se define la variable lrecreaCash_mov_v1
		
		Dim lrecreaCash_mov_v1 As eRemoteDB.Execute
		
		On Error GoTo FindByDocument_Err
		
		lrecreaCash_mov_v1 = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.reaCash_mov_v1'
		'**+Information read on March 05, 2001  03:39:52 p.m.
		'+Definición de parámetros para stored procedure 'insudb.reaCash_mov_v1'
		'+Información leída el 05/03/2001 03:39:52 p.m.
		
		With lrecreaCash_mov_v1
			.StoredProcedure = "reaCash_mov_v1"
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nbank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nAcc_cash = .FieldToClass("nAcc_cash")
				nOffice = .FieldToClass("nOffice")
				nCurrency = .FieldToClass("nCurrency")
				dEffecdate = .FieldToClass("dEffecdate")
				nTransac = .FieldToClass("nTransac")
				dDoc_date = .FieldToClass("dDoc_date")
				nAmount = .FieldToClass("nAmount")
				sCurr_descript = .FieldToClass("sDescript")
				sDep_number = .FieldToClass("sDep_number")
				nAcc_bank = .FieldToClass("nAcc_bank")
				nCurrency = .FieldToClass("nCurrency")
				
				.RCloseRec()
				FindByDocument = True
			Else
				FindByDocument = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCash_mov_v1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_mov_v1 = Nothing
		
FindByDocument_Err: 
		If Err.Number Then
			FindByDocument = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindChequeNumber: Returns a check number from the table "Cash_mov"
	'%FindChequeNumber: Devuelve un número de cheque de la tabla "Cash_mov"
	Public Function FindChequeNumber(ByVal strCheque As String) As Boolean
		
		'**-The variable lrecreaChequeNumber is declared
		'-Se define la variable lrecreaChequeNumber
		
		Dim lrecreaChequeNumber As eRemoteDB.Execute
		
		On Error GoTo FindChequeNumber_Err
		
		
		lrecreaChequeNumber = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.reaChequeNumber'
		'**+Information read on February 07, 2001  08:47:47
		'+Definición de parámetros para stored procedure 'insudb.reaChequeNumber'
		'+Información leída el 07/02/2001 8:47:47
		
		With lrecreaChequeNumber
			.StoredProcedure = "reaChequeNumber"
			.Parameters.Add("sCheque", strCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nAcc_cash = .FieldToClass("nAcc_cash")
				nCurrency = .FieldToClass("nCurrency")
				nOffice = .FieldToClass("nOffice")
				nTransac = .FieldToClass("nTransac")
				dEffecdate = .FieldToClass("dEffecdate")
				nAmount = .FieldToClass("nAmount")
				nAcc_bank = .FieldToClass("nAcc_bank")
				nBank_code = .FieldToClass("nBank_code")
				nBranch_Led = .FieldToClass("nBranch_led")
				dCard_expir = .FieldToClass("dCard_expir")
				sCard_num = .FieldToClass("sCard_num")
				nCard_typ = .FieldToClass("nCard_type")
				nCl_transac = .FieldToClass("NCL_TRANSAC")
				nClaim = .FieldToClass("nClaim")
				nCompanyc = .FieldToClass("nCompanyc")
				nVoucher_le = .FieldToClass("nVoucher_le")
				nVoucher = .FieldToClass("nVoucher")
				nConcept = .FieldToClass("nConcept")
				nContrat = .FieldToClass("nContrat")
				dDat_return = .FieldToClass("DDOC_DATE")
				sDep_number = .FieldToClass("sDep_number")
				dDoc_date = .FieldToClass("dDoc_date")
				sDocnumbe = .FieldToClass("sDocnumbe")
				nDraft = .FieldToClass("nDraft")
				nExchange = .FieldToClass("nExchange")
				nIntermed = .FieldToClass("nIntermed")
				nMov_type = .FieldToClass("nMov_type")
				sNull_movem = .FieldToClass("sNull_movem")
				sNull_recor = .FieldToClass("sNull_recor")
				dNulldate = .FieldToClass("dNulldate")
				nPaynumbe = .FieldToClass("nPaynumbe")
				nReceipt = .FieldToClass("nReceipt")
				nYear_month = .FieldToClass("nYear_month")
				nBordereaux = .FieldToClass("nBordereaux")
				nTyp_acco = .FieldToClass("nTyp_acco")
				sClient = .FieldToClass("sClient")
				sNumForm = .FieldToClass("sNumForm")
				sType_acc = .FieldToClass("sType_acc")
				sDescript = .FieldToClass("sDescript")
				nNoteNum = .FieldToClass("nNotenum")
				sInter_pay = .FieldToClass("sInter_pay")
				dLedger_dat = .FieldToClass("dLedger_dat")
				nUser_sol = .FieldToClass("nUser_sol")
				nCompany = .FieldToClass("nCompany")
				sProcess = .FieldToClass("sProcess")
				FindChequeNumber = True
				
				.RCloseRec()
			Else
				FindChequeNumber = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaChequeNumber may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaChequeNumber = Nothing
		
FindChequeNumber_Err: 
		If Err.Number Then
			FindChequeNumber = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindCashMovInfo: Reads the the pay order information when its about cash.
	'%FindCashMovInfo: Leer la información de la orden de pago cuando se trata de efectivo.
	Public Function FindCashMovInfo(ByVal nAcc_cash As Integer, ByVal nCurrency As Integer, ByVal nOffice As Integer, ByVal dEffecdate As Date, ByVal nRequest_nu As Double) As Boolean
		
		'**-The variable lrecreaCash_movOP006 is declared
		'-Se define la variable lrecreaCash_movOP006
		
		Dim lrecreaCash_movOP006 As eRemoteDB.Execute
		lrecreaCash_movOP006 = New eRemoteDB.Execute
		On Error GoTo FindCashMovInfo_Err
		'**+Parameter definition for stored procedures 'insudb.reaCash_movOP006'
		'**+Information read on February 13, 2001  11:17:59 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaCash_movOP006'
		'+Información leída el 13/02/2001 11:17:59 a.m.
		
		With lrecreaCash_movOP006
			.StoredProcedure = "reaCash_movOP006"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nAcc_cash = .FieldToClass("nAcc_cash")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nOffice = .FieldToClass("nOffice")
				nTransac = .FieldToClass("nTransac")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				nConcept = .FieldToClass("nConcept")
				sDescript = .FieldToClass("sDescript")
				nAmount = .FieldToClass("nAmount")
				sClient = .FieldToClass("sClient")
				sInter_pay = .FieldToClass("sInter_pay")
				nUser_sol = .FieldToClass("nUser_sol")
				dLedger_dat = .FieldToClass("dLedger_dat")
				nBordereaux = .FieldToClass("nBordereaux")
				sBenef_name = .FieldToClass("sBenefName")
				sInter_name = .FieldToClass("sInterName")
				sUser_name = .FieldToClass("sUserName")
				nNoteNum = .FieldToClass("nNotenum")
				Me.nRequest_nu = .FieldToClass("nRequest_nu")
				dIssue_Dat = .FieldToClass("dIssue_dat")
				nVoucher = .FieldToClass("nVoucher")
				nVoucher_le = .FieldToClass("nVoucher_le")
				nBranch_Led = .FieldToClass("nBranch_led")
				nClaim = .FieldToClass("nClaim")
				nNullcode = .FieldToClass("nNullcode")
				dNulldate = .FieldToClass("dNulldate")
				sPay_freq = .FieldToClass("sPay_freq")
				nReceipt = .FieldToClass("nReceipt")
				nSta_cheque = .FieldToClass("nSta_cheque")
				nTransac = .FieldToClass("nTransac")
				nYear_month = .FieldToClass("nYear_month")
				nCheque_bordereaux = .FieldToClass("nBordereaux")
				nQ_pays = .FieldToClass("nQ_pays")
				dStat_date = .FieldToClass("dStat_date")
				FindCashMovInfo = True
				.RCloseRec()
			Else
				FindCashMovInfo = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaCash_movOP006 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_movOP006 = Nothing
		
FindCashMovInfo_Err: 
		If Err.Number Then
			FindCashMovInfo = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%NullVoucher: Voids the book keeper seats by creating another one with inverse balances
	'%NullVoucher: Anula un asiento contable creando otro de saldos contrarios
	Public Function NullVoucher(ByVal nLed_compan As Integer, ByVal nVoucher As Integer, ByVal nUsercode As Integer) As Boolean
		
		'**-The variable lrecinsNullVoucher is declared
		'-Se define la variable lrecinsNullVoucher
		
		Dim lrecinsNullVoucher As eRemoteDB.Execute
		lrecinsNullVoucher = New eRemoteDB.Execute
		On Error GoTo NullVoucher_Err
		
		'**+Parameter definition for stored procedure 'insudb.insNullVoucher'
		'**+Information read on February 14, 2001  02:25:10 p.m.
		'+Definición de parámetros para stored procedure 'insudb.insNullVoucher'
		'+Información leída el 14/02/2001 02:25:10 p.m.
		
		With lrecinsNullVoucher
			.StoredProcedure = "insNullVoucher"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			NullVoucher = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsNullVoucher may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsNullVoucher = Nothing
		
NullVoucher_Err: 
		If Err.Number Then
			NullVoucher = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Cash_mov".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Cash_mov". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecupdCash_mov As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecupdCash_mov = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.updCash_mov'
		'**+Information read on November 21, 2000 9:23:44 a.m.
		'+Definición de parámetros para stored procedure 'insudb.updCash_mov'
		'+Información leída el 21/11/2000 9:23:44 AM
		
		With lrecupdCash_mov
			.StoredProcedure = "updCash_mov"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_Type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDep_number", sDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_Bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTransac", sTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sEffecdate", sEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOptToDeposit", nOptToDeposit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRealDep", dRealDep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCashnum", sCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOffice", sOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCash_mov = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'%DeleteDeposit: Este método se encarga de eliminar un depósito
    Public Function DeleteDeposit(ByVal nCashNum As Integer, ByVal dDepDate As Date, ByVal sDep_number As String, ByVal optToDeposit As Integer, ByVal nCurrency As Integer, ByVal nOffice As Integer, ByVal nUsercode As Integer, ByVal nIntermed As Integer) As Boolean
        Dim lrecupdCash_mov As eRemoteDB.Execute

        On Error GoTo DeleteDeposit_Err
        lrecupdCash_mov = New eRemoteDB.Execute

        '**+Parameter definition for stored procedure 'insudb.updCash_mov'
        '**+Information read on November 21, 2000 9:23:44 a.m.
        '+Definición de parámetros para stored procedure 'insudb.updCash_mov'
        '+Información leída el 21/11/2000 9:23:44 AM

        With lrecupdCash_mov
            .StoredProcedure = "InsReverseDeposit"
            .Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dDepDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDep_number", sDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("optToDeposit", optToDeposit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            DeleteDeposit = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecupdCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdCash_mov = Nothing

DeleteDeposit_Err:
        If Err.Number Then
            DeleteDeposit = False
        End If
        On Error GoTo 0
    End Function
	
	'**%UpdateByPayOrder: Updates the cash movements table "Cash_mov" from the Pay Order form
	'%UpdateByPayOrder: Actualiza la tabla de movimientos de caja "Cash_mov" desde la forma Orden de Pago
	Public Function UpdateByPayOrder() As Boolean
		
		'**-The variable lrecupdCash_movOP006 is declared
		'-Se define la variable lrecupdCash_movOP006
		
		Dim lrecupdCash_movOP006 As eRemoteDB.Execute
		lrecupdCash_movOP006 = New eRemoteDB.Execute
		On Error GoTo UpdateByPayOrder_Err
		
		'**+Parameter definition for stored procedure 'insudb.updCash_movOP006'
		'**+Information read on February 14, 2001 12:22:40 p.m.
		'+Definición de parámetros para stored procedure 'insudb.updCash_movOP006'
		'+Información leída el 14/02/2001 12:22:40 p.m.
		
		With lrecupdCash_movOP006
			.StoredProcedure = "updCash_movOP006"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_pay", sInter_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_sol", nUser_sol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteNum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUpdAvailable", nUpdAvailable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher_le", nVoucher_le, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_Led", nBranch_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateByPayOrder = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdCash_movOP006 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCash_movOP006 = Nothing
		
UpdateByPayOrder_Err: 
		If Err.Number Then
			UpdateByPayOrder = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%UpdateCheqReturned: Register the movements of returned checks in cash and bank
	'%UpdateCheqReturned: Registra los movimientos de cheque devuelto en caja y banco
	Public Function UpdateCheqReturned() As Boolean
		
		'**-The variable lrecinsCas_mov_chd is declared
		'-Se define la variable lrecinsCash_mov_chd
		
		Dim lrecinsCash_mov_chd As eRemoteDB.Execute
		
		On Error GoTo UpdateCheqReturned_Err
		
		lrecinsCash_mov_chd = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.insCash_mov_chd'
		'**+Information read on March 05, 2001 01:46:53 p.m.
		'+Definición de parámetros para stored procedure 'insudb.insCash_mov_chd'
		'+Información leída el 05/03/2001 01:46:53 p.m.
		
		With lrecinsCash_mov_chd
			.StoredProcedure = "insCash_mov_chd"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDat_return", dDat_return, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountND", nAmountND, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateCheqReturned = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsCash_mov_chd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCash_mov_chd = Nothing
		
UpdateCheqReturned_Err: 
		If Err.Number Then
			UpdateCheqReturned = False
		End If
		On Error GoTo 0
	End Function
	
	'**%valDep_number: Validates if are cash, checks or credit card vouchers associated
	'**%to a deposit number and an account number.
	'%valDep_number: Valida si existen efectivo, cheques o vouchers de tarjetas de crédito
	'%asociadas a un número de depósito y cuenta
	Public Function valDep_number(ByVal dtmEffecdate As Date, ByVal intAcc_bank As Integer, ByVal strDep_number As String) As Boolean
		
		'**-The variable lrecValCash_mov-Dep-number is declared
		'-Se define la variable lrecValCash_mov_Dep_number
		
		Dim lrecValCash_mov_Dep_number As eRemoteDB.Execute
		lrecValCash_mov_Dep_number = New eRemoteDB.Execute
		On Error GoTo valDep_number_Err
		
		'**+Parameter definition for stored procedures 'insudb.ValCash_mov_Dep_number'
		'**+Information read in February 07, 2001  17:54:51
		'+Definición de parámetros para stored procedure 'insudb.ValCash_mov_Dep_number'
		'+Información leída el 07/02/2001 17:14:51
		
		With lrecValCash_mov_Dep_number
			.StoredProcedure = "ValCash_mov_Dep_number"
			.Parameters.Add("dEffecdate", dtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", intAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDep_number", strDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nCount = .FieldToClass("nCount")
				valDep_number = True
				.RCloseRec()
			Else
				valDep_number = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecValCash_mov_Dep_number may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValCash_mov_Dep_number = Nothing
		
valDep_number_Err: 
		If Err.Number Then
			valDep_number = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%ValCash_mov_Acc: This method validate the existence of records in the table "Cash_mov"
	'%ValCash_mov_Acc: Este metodo valida la existencia de registros en la tabla "Cash_mov"
	Public Function ValCash_mov_Acc() As Boolean
		Dim lrecValCash_mov_Acc As eRemoteDB.Execute
		
		On Error GoTo ValCash_mov_Acc_Err
		lrecValCash_mov_Acc = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.ValCash_mov_Acc'
		'**+Information read on Novemeber 21, 2000  9:38:51 a.m.
		'+Definición de parámetros para stored procedure 'insudb.ValCash_mov_Acc'
		'+Información leída el 21/11/2000 9:38:51 AM
		
		With lrecValCash_mov_Acc
			.StoredProcedure = "ValCash_mov_Acc"
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAccount", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nResponse = .FieldToClass("nResponse")
				ValCash_mov_Acc = True
				.RCloseRec()
			Else
				ValCash_mov_Acc = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecValCash_mov_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValCash_mov_Acc = Nothing
		
ValCash_mov_Acc_Err: 
		If Err.Number Then
			ValCash_mov_Acc = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Class_Initialize: Controls the creation of an instance of the class
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.nAcc_cash = eRemoteDB.Constants.intNull
		Me.nCurrency = eRemoteDB.Constants.intNull
		Me.nOffice = eRemoteDB.Constants.intNull
		Me.nTransac = eRemoteDB.Constants.intNull
		Me.dEffecdate = dtmNull
		Me.nAmount = eRemoteDB.Constants.intNull
		Me.nAcc_bank = eRemoteDB.Constants.intNull
		Me.nBank_code = eRemoteDB.Constants.intNull
		Me.nBranch_Led = eRemoteDB.Constants.intNull
		Me.dCard_expir = dtmNull
		Me.sCard_num = strNull
		Me.nCard_typ = eRemoteDB.Constants.intNull
		Me.nCl_transac = eRemoteDB.Constants.intNull
		Me.nClaim = eRemoteDB.Constants.intNull
		Me.nCompanyc = eRemoteDB.Constants.intNull
		Me.nVoucher_le = eRemoteDB.Constants.intNull
		Me.nVoucher = eRemoteDB.Constants.intNull
		Me.nConcept = eRemoteDB.Constants.intNull
		Me.nContrat = eRemoteDB.Constants.intNull
		Me.dDat_return = dtmNull
		Me.sDep_number = strNull
		Me.dDoc_date = dtmNull
		Me.sDocnumbe = strNull
		Me.nDraft = eRemoteDB.Constants.intNull
		Me.nExchange = eRemoteDB.Constants.intNull
		Me.nIntermed = eRemoteDB.Constants.intNull
		Me.nMov_type = eRemoteDB.Constants.intNull
		Me.sNull_movem = strNull
		Me.sNull_recor = strNull
		Me.dNulldate = dtmNull
		Me.nPaynumbe = eRemoteDB.Constants.intNull
		Me.nReceipt = eRemoteDB.Constants.intNull
		Me.nUsercode = eRemoteDB.Constants.intNull
		Me.nYear_month = eRemoteDB.Constants.intNull
		Me.nBordereaux = eRemoteDB.Constants.intNull
		Me.nTyp_acco = eRemoteDB.Constants.intNull
		Me.sClient = strNull
		Me.sNumForm = strNull
		Me.sType_acc = strNull
		Me.sDescript = strNull
		Me.nNoteNum = eRemoteDB.Constants.intNull
		Me.sInter_pay = strNull
		Me.dLedger_dat = dtmNull
		Me.nUser_sol = eRemoteDB.Constants.intNull
		Me.nCompany = eRemoteDB.Constants.intNull
		Me.sProcess = strNull
		Me.nResponse = eRemoteDB.Constants.intNull
		Me.nUpdate = eRemoteDB.Constants.intNull
		Me.sMov_typeDes = strNull
		Me.nAction = eRemoteDB.Constants.intNull
		Me.nTransacNull = eRemoteDB.Constants.intNull
		Me.nCount = eRemoteDB.Constants.intNull
		Me.sBank_descript = strNull
		Me.sCard_descript = strNull
		Me.sLocal_descript = strNull
		Me.nRequest_nu = eRemoteDB.Constants.intNull
		Me.sBenef_name = strNull
		Me.sInter_name = strNull
		Me.sUser_name = strNull
		Me.dIssue_Dat = dtmNull
		Me.nNullcode = eRemoteDB.Constants.intNull
		Me.sPay_freq = strNull
		Me.nSta_cheque = eRemoteDB.Constants.intNull
		Me.nCheque_bordereaux = eRemoteDB.Constants.intNull
		Me.nQ_pays = eRemoteDB.Constants.intNull
		Me.dStat_date = dtmNull
		Me.nUpdAvailable = eRemoteDB.Constants.intNull
		Me.nCashNum = eRemoteDB.Constants.intNull
		Me.nChequeLocat = eRemoteDB.Constants.intNull
		Me.nInsur_area = eRemoteDB.Constants.intNull
		Me.dPosted = dtmNull
		Me.nCod_Agree = eRemoteDB.Constants.intNull
		Me.nInputChannel = eRemoteDB.Constants.intNull
		Me.dValDate = dtmNull
		Me.Sr_DepNum = String.Empty
		Me.nOri_Curr = eRemoteDB.Constants.intNull
		Me.nOri_Amount = eRemoteDB.Constants.intNull
		Me.nSupport_Id = eRemoteDB.Constants.intNull
		Me.nTypesupport = eRemoteDB.Constants.intNull
		Me.nDocSupport = eRemoteDB.Constants.intNull
		Me.dCollection = dtmNull
		Me.nCheque_Stat = eRemoteDB.Constants.intNull
		Me.nCash_id = eRemoteDB.Constants.intNull
		Me.nFin_Int = eRemoteDB.Constants.intNull
		Me.nBranch = eRemoteDB.Constants.intNull
		Me.nProduct = eRemoteDB.Constants.intNull
		Me.nBank_Agree = eRemoteDB.Constants.intNull
		Me.nProponum = eRemoteDB.Constants.intNull
		Me.nBulletins = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'@@@@@@@@@@@@@@@@@@@@ FUNCIONES DE VALIDACIÓN Y EJECUCIÓN (VAL Y POST) @@@@@@@@@@@@@@@@@@@@
	
	'**%insPostOP002: This method updates the database (as described in the functional specifications)
	'**%for the page "OP002"
	'%insPostOP002: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OP002"
    Public Function insPostOP002(ByVal sDep_number As String, ByVal dDepDate As Date, ByVal optToDeposit As Integer, ByVal nAcc_cash As Integer, ByVal nCurrency As Integer, ByVal nOffice As Integer, ByVal nTransac As Integer, ByVal dEffecdate As Date, ByVal nMov_type As Integer, ByVal nAcc_bank As Integer, ByVal nUsercode As Integer, ByVal nTotDeposit As Double, ByVal nCashNum As Integer, ByVal nCompany As Integer, ByVal dRealDep As Date, ByVal nAction As Integer, Optional ByVal sTransac As String = "", Optional ByVal sEffecdate As String = "", Optional ByVal sSel As String = "", Optional ByVal nIntermed As Integer = 0, Optional ByVal sCashnum As String = "", Optional ByVal sOffice As String = "") As Boolean
        Dim lclsBank_acc As Bank_acc
        Dim lclsBank_mov As Bank_mov
        Dim lintBank_code As Integer

        lclsBank_acc = New Bank_acc
        lclsBank_mov = New Bank_mov

        On Error GoTo insPostOP002_Err

        insPostOP002 = True
        If Not IsNothing(sTransac) Then
            sTransac = sTransac.Replace(",", ", ")
        End If

        If Not IsNothing(sEffecdate) Then
            sEffecdate = sEffecdate.Replace(",", ", ")
        End If

        If Not IsNothing(sSel) Then
            sSel = sSel.Replace(",", ", ")
        End If

        If Not IsNothing(sCashnum) Then
            sCashnum = sCashnum.Replace(",", ", ")
        End If


        If nAction <> eFunctions.Menues.TypeActions.clngActioncut Then
            If lclsBank_acc.Find(nAcc_bank) Then
                lintBank_code = IIf(lclsBank_acc.nBank_code = eRemoteDB.Constants.intNull, 0, lclsBank_acc.nBank_code)
            End If

            With Me
                .nAcc_cash = nAcc_cash
                .nCurrency = nCurrency
                .nOffice = nOffice
                .nTransac = nTransac
                .dEffecdate = dEffecdate
                .nMov_type = nMov_type
                .sDep_number = sDep_number
                .nAcc_bank = nAcc_bank
                .nUsercode = nUsercode
                .nCashNum = nCashNum
                .nCompany = nCompany
                .sTransac = sTransac
                .sEffecdate = sEffecdate
                .nOptToDeposit = optToDeposit
                .sSel = sSel
                .dRealDep = dRealDep
                .nIntermed = nIntermed
                .nAmount = nTotDeposit
                .sCashnum = sCashnum
                .sOffice = sOffice

                insPostOP002 = .Update
            End With

            'Si se trata de un depósito en cheque, se actualiza premium
            ' y premium_mo
            If insPostOP002 Then
                If optToDeposit = 2 Then
                    insPostOP002 = Me.UpdatePremium_MoOP002(nAcc_cash, nOffice, nCurrency, nCashNum, dEffecdate, nTransac, nUsercode, sDep_number, dDepDate, sTransac, sEffecdate, sSel, sOffice)

                End If
            End If
            '**+It's about a cash deposit
            '+Se trata de un depósito en efectivo

            If insPostOP002 Then
                With lclsBank_mov
                    .sDep_number = sDep_number
                    .nAcc_bank = nAcc_bank
                    .dDepDate = dDepDate
                    .nOffice = nOffice
                    .nCurrency = nCurrency
                    .nMov_type = nMov_type
                    .nAmount = nTotDeposit
                    .nCash_Amount = eRemoteDB.Constants.intNull
                    .nUsercode = nUsercode
                    .nBank_code = lintBank_code
                    .nCashNum = nCashNum
                    .nCompanyc = nCompany
                    .dRealDep = dRealDep
                    .nIntermed = nIntermed
                    Call lclsBank_mov.AddBankDeposit()
                End With
            End If
        Else
            insPostOP002 = Me.DeleteDeposit(nCashNum, dDepDate, sDep_number, optToDeposit, nCurrency, nOffice, nUsercode, nIntermed)
        End If

        'UPGRADE_NOTE: Object lclsBank_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBank_mov = Nothing
        'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBank_acc = Nothing

insPostOP002_Err:
        If Err.Number Then
            insPostOP002 = False
        End If
        On Error GoTo 0
    End Function
	
	'**%insValOP002: This function validates the data entered in the detail section of the form
	'%insValOP002: Esta función se encarga de validar los datos introducidos en la zona de detalle para forma
	Public Function insValOP002(ByVal optToDeposit As Integer, ByVal nTotCash As Double, ByVal nAvailable As Double, ByVal nTotDeposit As Double, ByVal nMinAmount As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValIOP002_Err
		lclsErrors = New eFunctions.Errors
		insValOP002 = strNull
		
		'**+Validation of the field "Cash Deposit"
		'+Validación del campo "Depósito Efectivo"
		If optToDeposit = 1 Then
			If nTotCash > CDec(nAvailable) - CDec(nMinAmount) Then
				lclsErrors.ErrorMessage("OP002", 60220,  , eFunctions.Errors.TextAlign.RigthAling, CStr(nMinAmount))
			End If
		End If
		
		'**+Validation of the field "Total Deposit"
		'+Validación del campo "Depósito Total"
		If nTotDeposit = 0 Or nTotDeposit = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage("OP002", 7258)
		End If
		
		insValOP002 = lclsErrors.Confirm
		
insValIOP002_Err: 
		If Err.Number Then
			insValOP002 = insValOP002 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValOP002_K:This function validates the data entered in the header section of the form
	'%insValOP002_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma
    Public Function insValOP002_k(ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal sDepositNum As String, ByVal nAccCash As Integer, ByVal nCompany As Integer, ByVal nCash As Integer, ByVal nToDeposit As Integer, ByVal sSelection As String, ByVal nIntermed As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lvalTime As eFunctions.valField
        Dim lclsBank_acc As Bank_acc
        Dim lclsSecur_sche As eSecurity.Secur_sche
        Dim lclsCash_mov As Cash_mov
        Dim lblnValidDate As Boolean
        Dim lobjCash_Stat As eCashBank.Cash_stat

        lclsCash_mov = New Cash_mov
        lclsErrors = New eFunctions.Errors

        insValOP002_k = ""

        On Error GoTo insValOP002_k_Err

        '**+Validation of the field "Date"
        '+Validación del CAMPO "Fecha"

        lblnValidDate = True
        '**+The date of the deposit can not be earlier than the actual date of the server.
        '+La fecha de depósito no puede ser mayor que la fecha actual del servidor

        If dEffecdate = dtmNull Then
            insValOP002_k = lclsErrors.ErrorMessage("OP002", 7010)
            lblnValidDate = False
        Else
            lvalTime = New eFunctions.valField
            lvalTime.objErr = lclsErrors
            lvalTime.ErrEmpty = 1001
            If Not lvalTime.ValDate(dEffecdate) Then
                lblnValidDate = False
            Else
                If dEffecdate > Today Then
                    insValOP002_k = lclsErrors.ErrorMessage("OP002", 7011)
                    lblnValidDate = False
                End If
            End If
        End If

        '**+Validation of the field "Deposito"
        '+Validación del CAMPO "Depósito"
        If sDepositNum = strNull Then
            insValOP002_k = lclsErrors.ErrorMessage("OP002", 7003)
        ElseIf (nAccCash <> intNull Or nAccCash <> 0) And lblnValidDate Then

            If lclsCash_mov.valDep_number(dEffecdate, nAccCash, sDepositNum) Then
                If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                    '**+If the action is register, the deposit number should NOT be registered in the system
                    '+Si la acción es registrar, el Nro. de depósito NO debe estar registrado en el sistema
                    If lclsCash_mov.nCount > 0 Then
                        insValOP002_k = lclsErrors.ErrorMessage("OP002", 7105)
                    End If
                Else
                    '**+If the action is inquiry, the deposit number SHOULD be registered in the system
                    '+Si la acción es consultar, el Nro. de depósito DEBE estar registrado en el sistema
                    If lclsCash_mov.nCount = 0 Then
                        insValOP002_k = lclsErrors.ErrorMessage("OP002", 7235)
                    End If
                End If
            End If
        End If

        '**+Validation of the field "Account"
        '+Validación del CAMPO "Cuenta"
        If nAccCash = intNull Or nAccCash = 0 Then
            '**+The field "Account" should not be null
            '+El campo cuenta debe estar lleno
            insValOP002_k = lclsErrors.ErrorMessage("OP002", 7029)
        Else
            If nAccCash = 9998 Or nAccCash = 9996 Or nAccCash = 9999 Or nAccCash = 9997 Then
                '**+It must not correspond to a cash account (it must be different from 9998 and 9999)
                '+No debe corresponder a una cuenta de caja(debe ser distinta de 9998 y 9999)
                insValOP002_k = lclsErrors.ErrorMessage("OP002", 7031)
            Else
                lclsBank_acc = New Bank_acc
                If Not lclsBank_acc.Find_O(nAccCash) Then
                    '**+Should be registered in the bank accounts file
                    '+Debe estar registrado en el archivo de cuentas bancarias
                    insValOP002_k = lclsErrors.ErrorMessage("OP002", 60809)
                Else
                    '**+The currency associated with the account being processed is stored in the modular variable
                    '+Se almacena en la variable modular, la moneda relacionada a la cuenta en tratamiento
                    lclsSecur_sche = New eSecurity.Secur_sche
                    If Not lclsSecur_sche.valCurrency_Schema(nUsercode, lclsBank_acc.nCurrency) Then
                        insValOP002_k = lclsErrors.ErrorMessage("OP002", 99024)
                    End If
                End If
            End If
        End If

        '+Validación del Campo Compañia
        If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
            If nCompany = eRemoteDB.Constants.intNull Then
                insValOP002_k = lclsErrors.ErrorMessage("OP002", 1046)
            End If
        End If

        '+Validación del Número de Caja
        If nToDeposit <> 4 And nToDeposit <> 3 And sSelection = "1" Then
            If nCash = eRemoteDB.Constants.intNull Then
                insValOP002_k = lclsErrors.ErrorMessage("OP002", 60007)
            Else
                If nAction = eFunctions.Menues.TypeActions.clngActioncut Then
                    lobjCash_Stat = New eCashBank.Cash_stat

                    If lobjCash_Stat.valCash_statClosed(nCash, dEffecdate) Then
                        insValOP002_k = lclsErrors.ErrorMessage("OP002", 56028)
                    End If

                    'UPGRADE_NOTE: Object lobjCash_Stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjCash_Stat = Nothing
                End If
            End If
        End If

        'If sSelection = "2" Then
        'If nIntermed = eRemoteDB.Constants.intNull Then
        'insValOP002_k = lclsErrors.ErrorMessage("OP002", 3272)
        'End If
        'End If

        insValOP002_k = lclsErrors.Confirm


insValOP002_k_Err:
        If Err.Number Then
            insValOP002_k = insValOP002_k & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCash_mov = Nothing
        'UPGRADE_NOTE: Object lclsSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSecur_sche = Nothing
        'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBank_acc = Nothing
        'UPGRADE_NOTE: Object lvalTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lvalTime = Nothing
        On Error GoTo 0
    End Function
	
	'**%insValOP001: This method validates the page "OP001" as described in the functional specifications
	'%insValOP001: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "OP001"
	Public Function insValOP001(ByVal sCodispl As String, ByVal lintAction As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nMov_type As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nConcept As Integer = 0, Optional ByVal nTyp_acco As Integer = 0, Optional ByVal sType_acc As String = "", Optional ByVal sDocnumbe As String = "", Optional ByVal dDoc_date As Date = #12:00:00 AM#, Optional ByVal nBank_code As Integer = 0, Optional ByVal nAcc_bank As Integer = 0, Optional ByVal nCard_typ As Integer = 0, Optional ByVal sCard_num As String = "", Optional ByVal dCard_expir As Date = #12:00:00 AM#, Optional ByVal sClient As String = "", Optional ByVal nIntermed As Integer = 0, Optional ByVal nCompanyc As Integer = 0, Optional ByVal nProponum As Double = 0, Optional ByVal nBordereaux As Double = 0, Optional ByVal nClaim As Double = 0, Optional ByVal nContrat As Double = 0, Optional ByVal nDraft As Integer = 0, Optional ByVal nCompany_user As Integer = 0, Optional ByVal nCashNum As Integer = 0, Optional ByVal nCompany As Integer = 0, Optional ByVal dValDate As Date = #12:00:00 AM#, Optional ByVal nCurrencyIng As Integer = 0, Optional ByVal nAmountIng As Double = 0, Optional ByVal nAgree As Integer = 0, Optional ByVal nBank_Agree As Integer = 0, Optional ByVal dDateCollect As Date = #12:00:00 AM#, Optional ByVal nChequeLocat As Integer = 0, Optional ByVal nInputChanel As Integer = 0, Optional ByVal nBulletins As Double = 0, Optional ByVal nTypesupport As Integer = 0, Optional ByVal nSupport_Id As Integer = 0, Optional ByVal nFin_Int As Double = 0, Optional ByVal nCase_Num As Integer = 0) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjcash_bank As Object
		Dim lstrErrors As String
		
		On Error GoTo InsValOP001_Err
		lclsErrors = New eFunctions.Errors
		'**+Validation of the "Date of effect"
		'+Validacion de la "Fecha de efecto"
		If lintAction <> eFunctions.Menues.TypeActions.clngActionCondition Then
			If dEffecdate = dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 21006)
				
			ElseIf Not IsDate(dEffecdate) Then 
				Call lclsErrors.ErrorMessage(sCodispl, 1001)
				
			ElseIf dEffecdate > Today Then 
				Call lclsErrors.ErrorMessage(sCodispl, 7009)
			End If
		ElseIf dEffecdate <> dtmNull Then 
			If Not IsDate(dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1001)
				
			ElseIf dEffecdate > Today Then 
				Call lclsErrors.ErrorMessage(sCodispl, 7009)
			End If
		End If
		
		'**+Validation of the field "Mov_Type"
		'+Validaciones del campo "Tipo de movimiento"
		If lintAction <> eFunctions.Menues.TypeActions.clngActionCondition Then
			If nMov_type = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 9018)
			End If
		End If
		
		If lintAction <> eFunctions.Menues.TypeActions.clngActionCondition And lintAction <> eFunctions.Menues.TypeActions.clngActioncut Then
			'+Validaciones del campo "Canal de Ingreso"
			If nInputChanel = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60394)
			End If
		End If
		
		If lintAction <> eFunctions.Menues.TypeActions.clngActionCondition Then
			'**+Validations of the field "Zone"
			'+Validaciones del campo "Zona"
			If nMov_type <> eRemoteDB.Constants.intNull And nMov_type <> 4 Then
				If nOffice = eRemoteDB.Constants.intNull Or nOffice = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 1040)
				End If
			End If
			
			
			'+Validaciones del campo "Moneda de Origen"
			If nCurrency = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60122)
			End If
			
			'+Validaciones del campo "Monto Origen"
			If nAmount = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60123)
			End If
			
			'**+Validations of the field "Concept"
			'+Se realizan las validaciones sobre el campo "Concepto"
			If nConcept = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 7005)
			End If
			
			'**+Validations of the field "Current Account"
			'+Se realizan las validaciones sobre el campo "Cuenta corriente"
			If (nConcept = 10 Or nConcept = 2 Or nConcept = 3) And nTyp_acco = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 7107)
			End If
			
			'**+Validation of the field "Type of Business"
			'+Validacion del campo "Tipo de Negocio"
			If (nTyp_acco = 2 Or nTyp_acco = 3 Or nTyp_acco = 8) And sType_acc = "0" Then
				Call lclsErrors.ErrorMessage(sCodispl, 7250)
			End If
			
			'**+Validations of the field "document- Number"
			'+Se realizan las validaciones sobre el campo "Documento-Número"
			If nMov_type <> eRemoteDB.Constants.intNull And nMov_type <> 1 Then
				If sDocnumbe = strNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 7003)
				End If
			End If
			
			'**+Validations of the field "Document- Date"
			'+Se realizan las validaciones sobre el campo "Documento-Fecha"
			If nMov_type <> eRemoteDB.Constants.intNull Then
				If nMov_type <> 1 Then
					If dDoc_date = dtmNull Then
						Call lclsErrors.ErrorMessage(sCodispl, 7010)
					End If
				End If
				
				If nMov_type <> 10 And dDoc_date <> dtmNull Then
					'**+If the type of movement is different from postdated check, the date must be earlier or the same as today's date
					'+Si el tipo de movimiento es  diferente de cheque diferido, la fecha debe ser anterior o igual a la fecha del dia
					If dDoc_date > Today Then
						Call lclsErrors.ErrorMessage(sCodispl, 7011)
					End If
					
				ElseIf nMov_type = 10 And dDoc_date <> dtmNull Then 
					'**+ If the type of movement is the same as postdated check, the date must be earlier than today's date
					'+ Si el tipo de movimiento es igual a cheque diferido, la fecha debe ser posterior a la fecha del dia
					If dDoc_date <= Today Then
						Call lclsErrors.ErrorMessage(sCodispl, 7233)
					Else
						If dDoc_date > CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 30, Today)) Then
							Call lclsErrors.ErrorMessage(sCodispl, 7234)
						End If
					End If
				End If
			End If
			
			'**+Validations of the field "Document- Bank"
			'+Se realizan las validaciones sobre el campo "Documento-Banco"
			If (nMov_type = 2 Or nMov_type = 16 Or nMov_type = 10 Or nMov_type = 5) And nBank_code = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 7004)
			End If
			
			'**+Validations of the field "Document-Account"
			'+Se realizan las validaciones sobre el campo "Documento-Cuenta"
			If (nMov_type = 4 Or nMov_type = 6 Or nMov_type = 7 Or nMov_type = 8 Or nMov_type = 9 Or nMov_type = 3) Then
				If nAcc_bank = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 7002)
				End If
			End If
			
			'**+Validations of the field "Credit card-Type"
			'+Se realizan las validaciones sobre el campo "Tarjeta de credito-Tipo"
			If nMov_type = 5 And nCard_typ = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 5047)
			End If
			
			'**+Validation of the field "CreditCard-Number"
			'+Se realizan las validaciones sobre el campo "Tarjeta de credito-Número"
			If nMov_type = 5 And sCard_num = strNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 3865)
			End If
			
			'**+Validation of the field "Credit Card-Expiring"
			'+Se realizan las validaciones sobre el campo "Tarjeta de credito-Vencimiento"
			If nMov_type = 5 Then
				If dCard_expir = dtmNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 3876)
				Else
					If dCard_expir <= Today Then
						Call lclsErrors.ErrorMessage(sCodispl, 3937)
					End If
				End If
			End If
			
			'**+Validation of the field "Support-Client"
			'+Se realizan las validaciones sobre el campo "Soporte - Cliente"
			'+Nota: Por el error 60329 este campo siempre es obligatorio (Guber)
			If sClient = strNull Then
				If ((nTyp_acco <> 1 And nTyp_acco <> 2 And nTyp_acco <> 3 And nTyp_acco <> 8 And nTyp_acco <> 10) Or nConcept = 26 Or nConcept = 33) Then
					
					Call lclsErrors.ErrorMessage(sCodispl, 21118)
				End If
			End If
			
			'**+Validation of the field "Support-Intermediary"
			'+Se realizan las validaciones correspondientes al campo "Soporte-Intermediario"
			If nIntermed = eRemoteDB.Constants.intNull Then
				If nConcept = 2 Or nConcept = 25 Then
					Call lclsErrors.ErrorMessage(sCodispl, 7020)
				Else
					If nTyp_acco = 1 Or nTyp_acco = 10 Then
						Call lclsErrors.ErrorMessage(sCodispl, 7020)
					End If
				End If
			End If
			
			'**+Validations of the field "Support-Co/Reinssurance"
			'+Se realizan las validaciones correspondientes al campo "Soporte-Co/Reaseguro"
			If nTyp_acco <> eRemoteDB.Constants.intNull Then
				If nConcept <> eRemoteDB.Constants.intNull Then
					If (nTyp_acco = 2 Or nTyp_acco = 3 Or nTyp_acco = 8 Or nConcept = 3) And nCompanyc = eRemoteDB.Constants.intNull Then
						Call lclsErrors.ErrorMessage(sCodispl, 7024)
					End If
				End If
			End If
			
			'**+Validations of the field "Support-Proposal"
			'+Se realizan las validaciones correspondientes al campo "Soporte-Propuesta"
			If nConcept = 26 Then
				If nProponum = 0 Or nProponum = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 3789)
				End If
			End If
			
			'**+Validations of the field "Support-Relation"
			'+Se realizan las validaciones correspondientes al campo "Soporte-Relación"
			If (nConcept = 1 Or nConcept = 2 Or nConcept = 33) Then
				If nBordereaux = eRemoteDB.Constants.intNull Then
					If nConcept <> 33 Then
						Call lclsErrors.ErrorMessage(sCodispl, 7008)
					End If
				End If
			End If
			
			'**+Validations of the field "Support-Claim"
			'+Se realizan las validaciones correspondientes al campo "Soporte-Siniestro"
			If nConcept = 4 Or nConcept = 30 Or nConcept = 31 Or nConcept = 32 Then
				If nClaim = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 7022)
				End If
			End If
			
			'**+Validations of the field "Support-Finance-Contract"
			'**+Validations of the field "Support-Finance-Draft"
			'+Se realizan las validaciones correspondientes al campo "Soporte-Financiamiento-Giro"
			'+Se realizan las validaciones correspondientes al campo "Soporte-Financiamiento-Contrato"
			If nConcept = 6 Or nConcept = 7 Then
				If (nContrat = eRemoteDB.Constants.intNull) Then
					Call lclsErrors.ErrorMessage(sCodispl, 3357)
				End If
				If (nDraft = eRemoteDB.Constants.intNull) Then
					Call lclsErrors.ErrorMessage(sCodispl, 21063)
				End If
			End If
			
			'**+Validation of the field "Company"
			'+Validación del campo "Compañia"
			If nCompany = eRemoteDB.Constants.intNull Or nCompany = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1046)
			End If
			
			'+Validación del campo "Fecha de Valoración"
			If lintAction <> eFunctions.Menues.TypeActions.clngActionCondition And lintAction <> eFunctions.Menues.TypeActions.clngActioncut Then
				If dValDate = dtmNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 55527)
				Else
					If lintAction = eFunctions.Menues.TypeActions.clngActionadd Then
						lobjcash_bank = eRemoteDB.NetHelper.CreateClassInstance("eCashBank.Valdatconditions")
						If lobjcash_bank.InsFind_ValdatconditionCollect(nConcept, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dEffecdate) Then
							If lobjcash_bank.nChangesDat = 3 Or lobjcash_bank.nChangesDat = 4 Then
								If lobjcash_bank.dChangesdate <> dValDate And lobjcash_bank.dValueDate <> dValDate Then
									Call lclsErrors.ErrorMessage(sCodispl, 56031)
								End If
							End If
						End If
						'UPGRADE_NOTE: Object lobjcash_bank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lobjcash_bank = Nothing
					End If
				End If
			End If
			
			'+Validaciones del campo "Moneda de Ingreso"
			If nCurrencyIng = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60130)
			End If
			
			'+Validaciones del campo "Monto Ingreso"
			If nAmountIng = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60131)
			End If
			
			'+Validaciones del campo "Plaza"
			If nChequeLocat = eRemoteDB.Constants.intNull And (nMov_type = 2 Or nMov_type = 16 Or nMov_type = 10) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60393)
			End If
			
			'+ Si el concepto es igual a descuento por ventanillas,pago en ventanillas de bancos o ingreso por PAC
			If nConcept = 38 Or nConcept = 36 Or nConcept = 29 Then
				'+Validaciones del campo "Convenio"
				If nAgree = eRemoteDB.Constants.intNull Or nAgree = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 60117)
				End If
				
				'+Validaciones del campo "Fecha Cobranza"
				If dDateCollect = dtmNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 60133)
				End If
			End If
			
			'+Validaciones que se realizan el la BD
			lstrErrors = InsValOP001DB(nConcept, nContrat, nDraft, lintAction, nCompany_user, dEffecdate, nUsercode, nCurrency, nCurrencyIng, nMov_type, nOffice, nCashNum, nAcc_bank, sDocnumbe, nBank_code, nAgree, nBordereaux, nClaim, nCase_Num, nTyp_acco, sClient, nBulletins)
			
			Call lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrors)
		End If
		insValOP001 = lclsErrors.Confirm
		
InsValOP001_Err: 
		If Err.Number Then
			insValOP001 = "insValOP001: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValOP001: Este metodo se encarga de realizar las validaciones que son accesando la BD
	'%             descritas en el funcional de la ventana "OP001"
	Private Function InsValOP001DB(ByVal nConcept As Integer, ByVal nContrat As Integer, ByVal nDraft As Integer, ByVal nAction As Integer, ByVal nLed_compan As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal nCurrencyIng As Integer, ByVal nMov_type As Integer, ByVal nOffice As Integer, ByVal nCashNum As Integer, ByVal nAcc_bank As Integer, ByVal sDocnumbe As String, ByVal nBank_code As Integer, ByVal nCod_Agree As Integer, ByVal nBordereaux As Integer, ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nTyp_acco As Integer, ByVal sClient As String, ByVal nBulletins As Integer) As String
		Dim lrecInsValOP001 As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsValOP001'
		'+Información leída el 10/04/2003
		On Error GoTo InsValOP001_Err
		lrecInsValOP001 = New eRemoteDB.Execute
		With lrecInsValOP001
			.StoredProcedure = "InsValOP001"
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencying", nCurrencyIng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValOP001DB = .Parameters("Arrayerrors").Value
			End If
		End With
		
InsValOP001_Err: 
		If Err.Number Then
			InsValOP001DB = "InsValOP001DB: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecInsValOP001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValOP001 = Nothing
		On Error GoTo 0
	End Function
	
	'**%insPostOP001: This method updates the database (as described in the functional specifications)
	'**%for the page "OP001"
	'%insPostOP001: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OP001"
	Public Function InsPostOP001(ByVal nAction As Integer, ByVal nTransact As Integer, ByVal dEffecdate As Date, ByVal nMov_type As Integer, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nAmount As Double, ByVal nConcept As Integer, ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sDocnumbe As String, ByVal dDoc_date As Date, ByVal nBank_code As Integer, ByVal nAcc_bank As Integer, ByVal nCard_typ As Integer, ByVal sCard_num As String, ByVal dCard_expir As Date, ByVal sClient As String, ByVal nIntermed As Integer, ByVal nCompanyc As Integer, ByVal nProponum As Double, ByVal nBordereaux As Double, ByVal nClaim As Double, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nCompany_user As Integer, ByVal nCashNum As Integer, ByVal nCompany As Integer, ByVal dValordate As Date, ByVal nCurrencyIng As Integer, ByVal nAmountIng As Double, ByVal nCod_Agree As Integer, ByVal nBank_Agree As Integer, ByVal dDateCollect As Date, ByVal nBulletins As Double, ByVal nChequeLocat As Integer, ByVal nInputChannel As Integer, ByVal nTypesupport As Integer, ByVal nSupport_Id As Integer, ByVal nFin_Int As Double, ByVal nVoucher As Integer, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal nCash_id As Integer, ByVal nNoteNum As Double, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nInsur_area As Integer = 0) As Boolean
		Dim lintAcc_cash As Integer
		Dim lintAction As Integer
		
		On Error GoTo InsPostOP001_Err
		If nBranch <= 0 Then
			nBranch = eRemoteDB.Constants.intNull
		End If
		
		If nProduct <= 0 Then
			nProduct = eRemoteDB.Constants.intNull
		End If
		
		If nInsur_area <= 0 Then
			nInsur_area = eRemoteDB.Constants.intNull
		End If
		
		Select Case nMov_type
			'**+Cash
			'+Efectivo
			Case 1
				lintAcc_cash = 9998
				
				'**+Check
				'+Cheque, Vale Vista
			Case 2, 16
				lintAcc_cash = 9999
				
				'**+Postdated check
				'+Cheque diferido
			Case 10
				lintAcc_cash = 9997
				
				'**+Credit card
				'+Tarjeta de credito
			Case 5
				lintAcc_cash = 9996
				
			Case Else
				lintAcc_cash = eRemoteDB.Constants.intNull
				
		End Select
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			lintAction = 1
			nTransac = eRemoteDB.Constants.intNull
		Else
			lintAction = 2
		End If
		
		InsPostOP001 = AddIncommingCash(lintAcc_cash, nAcc_bank, nCurrency, nOffice, dEffecdate, nAmountIng, nBank_code, dCard_expir, sCard_num, nCard_typ, nClaim, nCompanyc, nVoucher_le, nVoucher, nConcept, nContrat, dDoc_date, sDocnumbe, nDraft, nIntermed, nMov_type, nUsercode, nBordereaux, nTyp_acco, sType_acc, sClient, nProponum, lintAction, nTransac, nCashNum, nCompany, nChequeLocat, nInputChannel, nCod_Agree, nBank_Agree, dDateCollect, nTypesupport, nSupport_Id, nBulletins, dValordate, nCurrencyIng, nAmount, nFin_Int, nCash_id, nCase_Num, nDeman_Type, nNoteNum, nBranch, nProduct, nInsur_area)
		
InsPostOP001_Err: 
		If Err.Number Then
			InsPostOP001 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostOPC001_K: This method updates the database (as described in the functional specifications)
	'**%for the page "OPC001_K"
	'%insPostOPC001_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OPC001_K"
	Public Function insPostOPC001_K(ByVal nAction As Integer) As Boolean
		insPostOPC001_K = True
	End Function
	
	'**%insValOP005_K: This method validates the header section of the page "OP005_K" as described in the
	'**%functional specifications
	'%InsValOP005_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OP005_K"
	Public Function insValOP005_K(ByVal sCodispl As String, Optional ByVal nAction As Integer = 0, Optional ByVal nBank_code As Integer = 0, Optional ByVal sDocnumbe As String = "") As String
		'**+Indicates the existence of the basic data
		'+Indica la existencia de los datos básicos.
		Dim lblnDataComplete As Boolean
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValOP005_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		lblnDataComplete = True
		
		'**+Validates the existence of a bank code
		'+Valida que exista un código de banco.
		If nBank_code <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7004)
			lblnDataComplete = False
		End If
		
		'**+Validates de existence a check number
		'+Valida que exista un número de cheque
		If sDocnumbe = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 7040)
			If lblnDataComplete Then lblnDataComplete = False
		End If
		
		'**+The stored procedure that inquires about the information of the check is prepared and executed
		'+Se prepara y ejecuta el "stored procedure" de consulta de la información del cheque
		
		If lblnDataComplete Then
			
			If Not FindByDocument(CashTypMov.clngMCCheq, sDocnumbe, nBank_code) Then
				lclsErrors.ErrorMessage(sCodispl, 7042)
			Else
				If sDep_number = String.Empty Then
					lclsErrors.ErrorMessage(sCodispl, 7041)
				End If
				
				'**+Validates to verify that there are no returned checks
				'+Si no tiene cheques devueltos
				'            If sCheque_dev = "1" Then
				'                If nAction = clngActionadd Then
				'                    lclsErrors.ErrorMessage sCodispl, 7237
				'                End If
				'            Else
				'                If nAction = clngActionQuery Then
				'                    lclsErrors.ErrorMessage sCodispl, 7236
				'                End If
				'            End If
			End If
		End If
		
		insValOP005_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP005_K_Err: 
		If Err.Number Then
			insValOP005_K = insValOP005_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValOP005: This method validates the page "OP005" as described in the functional specifications
	'%InsValOP005: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "OP005"
	Public Function insValOP005(ByVal sCodispl As String, ByVal dDat_return As Date, ByVal dDoc_date As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lblnDateValid As Boolean
		
		On Error GoTo insValOP005_Err
		
		lclsErrors = New eFunctions.Errors
		lblnDateValid = True
		
		'**+Validation of the returned check's date
		'+Validación del fecha de devolución del cheque
		If dDat_return = dtmNull Then
			lblnDateValid = False
			lclsErrors.ErrorMessage(sCodispl, 7039)
		End If
		
		If lblnDateValid Then
			If dDat_return > Today Then
				lclsErrors.ErrorMessage(sCodispl, 7092)
			Else
				If dDat_return < dDoc_date Then
					lclsErrors.ErrorMessage(sCodispl, 7231)
				End If
			End If
		End If
		
		insValOP005 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP005_Err: 
		If Err.Number Then
			insValOP005 = insValOP005 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostOP005_K: This method updates the database (as described in the functional specifications)
	'**%for the page "OP005_K"
	'%insPostOP005_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OP005_K"
	Public Function insPostOP005_K() As Boolean
		
		insPostOP005_K = True
	End Function
	
	'**%insPostOP005: This method updates the database (as described in the functional specifications)
	'**%for the page "OP005"
	'%insPostOP005: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OP005"
	Public Function insPostOP005(ByVal nAction As Integer, ByVal nAcc_cash As Integer, ByVal nCurrency As Integer, ByVal nOffice As Integer, ByVal nTransac As Integer, ByVal dDat_return As Date, ByVal nCash_Amount As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostOP005_Err
		
		Dim mclsCash_mov As eCashBank.Cash_mov
		Dim lclsCash_Num As eCashBank.User_cashnum
		Dim lintCashNum As Integer
		
		mclsCash_mov = New eCashBank.Cash_mov
		
		'+ Se obtiene el número de caja asociada al usuario.
		lclsCash_Num = New eCashBank.User_cashnum
		If lclsCash_Num.Find_nUser(nUsercode, True) Then
			lintCashNum = lclsCash_Num.nCashNum
		End If
		'UPGRADE_NOTE: Object lclsCash_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_Num = Nothing
		
		insPostOP005 = True
		
		Select Case nAction
			
			'**+If the selected option is Register
			'+Si la opción seleccionada es Registrar
			
			Case eFunctions.Menues.TypeActions.clngActionadd
				With mclsCash_mov
					.nAcc_cash = nAcc_cash
					.nCurrency = nCurrency
					.nOffice = nOffice
					.nTransac = nTransac
					.dDat_return = dDat_return
					.nAmountND = IIf(nCash_Amount < 0, 0, nCash_Amount)
					.dEffecdate = dEffecdate
					.nUsercode = nUsercode
					.nCashNum = lintCashNum
					
					insPostOP005 = .UpdateCheqReturned
				End With
		End Select
		
		'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCash_mov = Nothing
		
insPostOP005_Err: 
		If Err.Number Then
			insPostOP005 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValOPC001_K: This method validates the header section of the page "OPC001_K" as described in the
	'**%functional specifications
	'%InsValOPC001_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OPC001_K"
	Public Function insValOPC001_K(ByVal sCodispl As String, ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal nCashNum As Integer, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal nMov_type As Integer, ByVal nUsercode As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsUser_cashnum As eCashBank.User_cashnum
		
		On Error GoTo insValOPC001_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsUser_cashnum = New eCashBank.User_cashnum
		
		'+Validación de la fecha inicial
		If dDate_ini = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		End If
		
		If dDate_end = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1097)
		Else
			If dDate_end < dDate_ini Then
				Call lclsErrors.ErrorMessage(sCodispl, 4159)
			End If
		End If
		
		'+Se valida que el número de caja este lleno
		If nCashNum = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60007)
		Else
			'+Se valida que el usuario esté autorizado para consultar la caja
			If lclsUser_cashnum.Find(nCashNum) Then
				If lclsUser_cashnum.nUser <> nUsercode And lclsUser_cashnum.nCashSup <> nUsercode And lclsUser_cashnum.nHeadSup <> nUsercode Then
					Call lclsErrors.ErrorMessage(sCodispl, 60459)
				End If
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 60803)
			End If
		End If
		
		If nOffice = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1040)
		End If
		
		If nCurrency = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10107)
		End If
		
		insValOPC001_K = lclsErrors.Confirm
		
insValOPC001_k_Err: 
		If Err.Number Then
			insValOPC001_K = insValOPC001_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	'**%FindTotal: This method returns TRUE or FALSE depending if the records exists in the table "Cash_mov"
	'%FindTotal: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Cash_mov"
	Public Function FindTotal(ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal nMov_type As Integer, ByVal nCashNum As Integer) As Boolean
		
		'**-The variable lrecreaCash_mov_OPC001_a is declared
		'-Se define la variable lrecreaCash_mov_OPC001_a
		
		Dim lrecreaCash_mov_OPC001_a As eRemoteDB.Execute
		lrecreaCash_mov_OPC001_a = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.reaCash_mov_OPC001_a'
		'**+Information read on march 7, 2001  10.56.53
		'+Definición de parámetros para stored procedure 'insudb.reaCash_mov_OPC001_a'
		'+Información leída el 7/3/01 10.56.53
		
		With lrecreaCash_mov_OPC001_a
			.StoredProcedure = "reaCash_mov_OPC001_a"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nAmount = .FieldToClass("nAmount")
				.RCloseRec()
				FindTotal = True
			Else
				FindTotal = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaCash_mov_OPC001_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_mov_OPC001_a = Nothing
	End Function
	
	
	'**%insValOP015_k: This method validates the header section of the page "OP015_k" as described in the
	'**%functional specifications
	'%InsValOP015_k: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OP015_k"
	Public Function insValOP015_k(ByVal sCodispl As String, ByVal cboBank As Integer, ByVal gmtChekNumber As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCash_mov As eCashBank.Cash_mov
		
		On Error GoTo insValOP015_K_Err
		
		lclsCash_mov = New eCashBank.Cash_mov
		lclsErrors = New eFunctions.Errors
		'Static lstrValField As String
		
		If cboBank = 0 Or cboBank = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10828)
		End If
		
		If gmtChekNumber = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 7040)
		End If
		
		'**+The stored procedure that inquires about the information of the check is prepared and executed
		'+Se prepara y ejecuta el "store procedure" de consulta de la informacion del cheque
		If lclsCash_mov.FindByDocument(CashTypMov.clngMCCDifCheq, gmtChekNumber, cboBank) Then ' Movimiento de cheque diferido
			If Not (lclsCash_mov.sDep_number) = strNull Or Not (lclsCash_mov.sDep_number) = "" Then
				Call lclsErrors.ErrorMessage(sCodispl, 7240)
			End If
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 7247)
		End If
		
		insValOP015_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_mov = Nothing
		
insValOP015_K_Err: 
		If Err.Number Then
			insValOP015_k = insValOP015_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValOP015: This method validates the page "OP015" as described in the functional specifications
	'%InsValOP015: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "OP015"
	Public Function insValOP015(ByVal sCodispl As String, ByVal gmdNewCollectDate As Date, ByVal gmdOrigCollecDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValOP015_Err
		
		lclsErrors = New eFunctions.Errors
		'Static lstrValField As String
		
		
		'**+Validation of the date when the check was retuned
		'+Validacion del fecha de devolucion del cheque
		If gmdNewCollectDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 21059)
			'**+ IMPORTANT
			'+ OJO
		Else
			If gmdNewCollectDate <= gmdOrigCollecDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 7248)
			End If
		End If
		
		insValOP015 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP015_Err: 
		If Err.Number Then
			insValOP015 = insValOP015 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostOP015: This method updates the database (as described in the functional specifications)
	'**%for the page "OP015"
	'%insPostOP015: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OP015"
	Public Function insPostOP015(ByVal nAction As Integer, ByVal nAcc_cash As Integer, ByVal nCurrency As Integer, ByVal nOffice As Integer, ByVal nTransac As Integer, ByVal gmdNewCollectDate As Date, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		insPostOP015 = True
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			insPostOP015 = insUpdOP015(nAcc_cash, nCurrency, nOffice, nTransac, gmdNewCollectDate, dEffecdate, nUsercode)
		End If
	End Function
	
	'**%insUpdOP15: This method updates the information being processed from the principal table for the transaction
	'%insUpdOP015: Este metodo se encarga de actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function insUpdOP015(ByVal nAcc_cash As Integer, ByVal nCurrency As Integer, ByVal nOffice As Integer, ByVal nTransac As Integer, ByVal gmdNewCollectDate As Date, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsCash_mov_chdif As eRemoteDB.Execute
		
		lrecinsCash_mov_chdif = New eRemoteDB.Execute
		
		On Error GoTo insUpdOP015_Err
		
		insUpdOP015 = True
		
		'**+Parameter definition for stored procedure 'insudb.insCash_mov_chdif'
		'**+Information read on March 15, 2001 11:19:10
		'+Definición de parámetros para stored procedure 'insudb.insCash_mov_chdif'
		'+Información leída el 15/03/2001 11:09:10
		
		With lrecinsCash_mov_chdif
			.StoredProcedure = "insCash_mov_chdif"
			.Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNewDate", gmdNewCollectDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdOP015 = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsCash_mov_chdif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCash_mov_chdif = Nothing
		
insUpdOP015_Err: 
		If Err.Number Then
			insUpdOP015 = False
		End If
		On Error GoTo 0
	End Function
	
	'% FindLastTransaction: Obtiene la última transacción registrada en la tabla Cahs_mov - ACM - 04/09/2001
	'**% FindLastTransaction: Gets the last transaction recorded into table Cash_mov - ACM - Sep-04-2001
	Public Function FindLastTransaction(ByVal ldtmEffecdate As Date, ByVal lintOffice As Integer, ByVal lintCurrency As Integer) As Integer
		Dim lrecFindLastTransaction As eRemoteDB.Execute
		
		On Error GoTo FindLastTransaction_Err
		
		lrecFindLastTransaction = New eRemoteDB.Execute
		With lrecFindLastTransaction
			.StoredProcedure = "FindLastTransaction"
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", lintOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", lintCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				FindLastTransaction = .FieldToClass("nTransaction")
			Else
				FindLastTransaction = 0
			End If
		End With
FindLastTransaction_Err: 
		If Err.Number Then
			FindLastTransaction = 0
		End If
		'UPGRADE_NOTE: Object lrecFindLastTransaction may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindLastTransaction = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValOPC717_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OPC717_K"
	Public Function insValOPC717_K(ByVal sCodispl As String, ByVal dEndDate As Date, ByVal nCurrency As Integer, ByVal nBank_code As Integer, ByVal nChequeLocat As Integer, ByVal nCheque_Stat As Integer, ByVal sDocnumbe As String, ByVal sTypeInfo As String) As String
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo insValOPC717_k_Err
		lclsErrors = New eFunctions.Errors
		
		'+ Validación de la Moneda de la Consulta
		If nCurrency = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10827)
		End If
		
		If sDocnumbe <> String.Empty Then
			If sTypeInfo = "1" Then
				If Not insValChequeExists(sDocnumbe, nBank_code, nChequeLocat, nCheque_Stat, CInt("1")) Then
					Call lclsErrors.ErrorMessage(sCodispl, 60256)
				Else
					If Not insValChequeExists(sDocnumbe, nBank_code, nChequeLocat, nCheque_Stat, CInt("2")) Then
						Call lclsErrors.ErrorMessage(sCodispl, 55161)
					End If
				End If
			End If
		End If
		
		insValOPC717_K = lclsErrors.Confirm
		
insValOPC717_k_Err: 
		If Err.Number Then
			insValOPC717_K = insValOPC717_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insValChequeExists: Verifica que exista un cheque a fecha
	Public Function insValChequeExists(ByVal sDocnumbe As String, Optional ByRef nBank_code As Integer = 0, Optional ByRef nChequeLocat As Integer = 0, Optional ByRef nCheque_Stat As Integer = 0, Optional ByRef nOption As Integer = 0) As Boolean
		Dim lintExists As Short
		Dim lrecCash_mov As eRemoteDB.Execute
		lrecCash_mov = New eRemoteDB.Execute
		On Error GoTo insValChequeExists_Err
		
		insValChequeExists = False
		
		With lrecCash_mov
			.StoredProcedure = "insValChequeExists"
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChequeLocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCheque_Stat", nCheque_Stat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			insValChequeExists = (.Parameters("nExists").Value = 1)
		End With
		
insValChequeExists_Err: 
		If Err.Number Then
			insValChequeExists = False
		End If
		'UPGRADE_NOTE: Object lrecCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCash_mov = Nothing
		On Error GoTo 0
	End Function
	
	'%UpdatePremium_MoOP002(): Este Método Actualiza el indicador de cheque a fecha pendiente
	'%en  premium y premium_mo, crea un registro en cheque_mov correspondiente al movimiento
	'%del cheque y actualiza en cash_mov el estado del cheque
    Public Function UpdatePremium_MoOP002(ByVal nAcc_cash As Integer, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal nCashNum As Integer, ByVal dEffecdate As Date, ByVal nTransac As Integer, ByVal nUsercode As Integer, ByVal sDepNumbe As String, Optional ByVal dDeposit As Date = #12:00:00 AM#, Optional ByVal sTransac As String = "", Optional ByVal sEffecdate As String = "", Optional ByVal sSel As String = "", Optional ByVal sOffice As String = "") As Boolean
        Dim lrecUpdPremium_MoOP002 As eRemoteDB.Execute

        On Error GoTo UpdatePremium_MoOP002_Err
        lrecUpdPremium_MoOP002 = New eRemoteDB.Execute

        With lrecUpdPremium_MoOP002
            .StoredProcedure = "UpdPremium_MoOP002"
            .Parameters.Add("nAcc_cash", nAcc_cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDepNumbe", sDepNumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDeposit", dDeposit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTransac", sTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sEffecdate", sEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOffice", sOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdatePremium_MoOP002 = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecUpdPremium_MoOP002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdPremium_MoOP002 = Nothing

UpdatePremium_MoOP002_Err:
        If Err.Number Then
            UpdatePremium_MoOP002 = False
        End If
        On Error GoTo 0
    End Function
	
	'**%FindChequeOP752: This method returns TRUE or FALSE depending if the records exists in the table "Cash_mov"
	'%FindChequeOP752: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Cash_mov"
	Public Function FindChequeOP752(ByVal nBank_code As Integer, ByVal sDocnumbe As String) As Boolean
		
		'**-The variable lrecreaChequeOP752 is declared
		'-Se define la variable lrecreaChequeOP752
		
		Dim lrecreaChequeOP752 As eRemoteDB.Execute
		
		On Error GoTo FindChequeOP752_Err
		
		lrecreaChequeOP752 = New eRemoteDB.Execute
		
		With lrecreaChequeOP752
			.StoredProcedure = "reaChequeOP752"
			.Parameters.Add("nBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nCompany = .FieldToClass("nCompany")
				nCurrency = .FieldToClass("nCurrency")
				dDoc_date = .FieldToClass("dDoc_Date")
				nMov_type = .FieldToClass("nMov_type")
				nAmount = .FieldToClass("nAmount")
				sClient = .FieldToClass("sClient")
				nConcept = .FieldToClass("nConcept")
				nCashNum = .FieldToClass("nCashNum")
				nBordereaux = .FieldToClass("nBordereaux")
				nCheque_Stat = .FieldToClass("nCheque_Stat")
				.RCloseRec()
				FindChequeOP752 = True
			Else
				FindChequeOP752 = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaChequeOP752 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaChequeOP752 = Nothing
		
FindChequeOP752_Err: 
		If Err.Number Then
			FindChequeOP752 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValOP752_k: This method validates the header section of the page "OP752_k" as described in the
	'**%functional specifications
	'%InsValOP752_k: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "OP752_k"
	Public Function insValOP752_K(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nMoveType As Integer, ByVal dDateMove As Date, ByVal nBank_code As Integer, ByVal sChequeNum As String, ByVal sDep_number As String, ByVal nAccount As Integer, ByVal dExpirdat As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCash_mov As eCashBank.Cash_mov
		Dim lclsBank_mov As eCashBank.Bank_mov
		
		'- Variable para determinar si se efectúa la búsqueda o no de los datos, para validaciones
		Dim lblnFind As Boolean
		
		On Error GoTo insValOP752_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Tipo de Operación debe estar lleno
			If nMoveType = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 60233)
			End If
			
			'+ Fecha de la Operación debe estar llena
			If dDateMove = dtmNull Then
				Call .ErrorMessage(sCodispl, 60234)
			End If
			
			If nMoveType = 7 Or nMoveType = 8 Then
				'+ El número del depósito/redepósito debe estar lleno
				If sDep_number = String.Empty Then
					Call .ErrorMessage(sCodispl, 7003)
				Else
					lclsBank_mov = New eCashBank.Bank_mov
					If lclsBank_mov.Find_sDep_number(sDep_number, eRemoteDB.Constants.intNull) Then
						If lclsBank_mov.nAcc_bank = nAccount Then
							Call .ErrorMessage(sCodispl, 7105)
						End If
					End If
				End If
				
				'+ El número de cuenta en la que se realiza el depósito debe estar lleno
				If nAccount = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 7029)
				End If
				
				If dExpirdat = dtmNull Then
					Call .ErrorMessage(sCodispl, 21035)
				End If
			Else
				lblnFind = True
				'+ Código del Banco debe estar lleno
				If nBank_code = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 10828)
					lblnFind = False
				End If
				
				'+ Número del Cheque debe estar lleno
				If sChequeNum = strNull Then
					Call .ErrorMessage(sCodispl, 60235)
					lblnFind = False
				End If
				
				If lblnFind Then
					lclsCash_mov = New eCashBank.Cash_mov
					'+ Se busca el cheque en cash_mov para verificar su estado
					If lclsCash_mov.FindChequeOP752(nBank_code, sChequeNum) Then
						'+ Si la operación es reemplazo y el estado del cheque es depositado o reemplazado
						If nMoveType = 3 And (lclsCash_mov.nCheque_Stat = 5 Or lclsCash_mov.nCheque_Stat = 3) Then
							Call .ErrorMessage(sCodispl, 60241)
						End If
						'+ Si la operación es devolución y el estado del cheque no es depositado
						If nMoveType = 2 And lclsCash_mov.nCheque_Stat <> 5 Then
							Call .ErrorMessage(sCodispl, 60242)
						End If
						'+ Si la operación es prorroga y el tipo de cheque no es cheque a fecha
						If nMoveType = 4 And lclsCash_mov.nMov_type <> 10 Then
							Call .ErrorMessage(sCodispl, 60240)
						End If
						'+ Si la operación es prorroga y el estado del cheque no es ingresado ni devuelto
						If nMoveType = 4 And lclsCash_mov.nCheque_Stat <> 1 And lclsCash_mov.nCheque_Stat <> 2 And lclsCash_mov.nCheque_Stat <> 4 Then
							Call .ErrorMessage(sCodispl, 60243)
						End If
						'+ Si la operación es Reemplazo y el tipo de cheque no es cheque a fecha
						If nMoveType = 3 And lclsCash_mov.nMov_type <> 10 Then
							Call .ErrorMessage(sCodispl, 60244)
						End If
						
						If nMoveType = 1 Then
							Call .ErrorMessage(sCodispl, 7052)
						End If
					Else
						If nMoveType <> 1 Then
							If sChequeNum <> String.Empty Then
								Call .ErrorMessage(sCodispl, 60256)
							End If
						End If
					End If
				End If
			End If
			
			insValOP752_K = .Confirm
		End With
		
insValOP752_K_Err: 
		If Err.Number Then
			insValOP752_K = "insValOP752_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_mov = Nothing
		'UPGRADE_NOTE: Object lclsBank_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_mov = Nothing
	End Function
	
	'**%insValOP752: This method validates the folder section of the page "OP752" as described in the
	'**%functional specifications
	'%InsValOP752: Este metodo se encarga de realizar las validaciones del detalle (folder)
	'%descritas en el funcional de la ventana "OP752"
	Public Function insValOP752(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nMoveType As Integer, ByVal dDateMove As Date, ByVal nBank_code As Integer, ByVal sChequeNum As String, ByVal nCompany As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal dDateDoc As Date, ByVal nPostCheque As Integer, ByVal sBeneficiary As String, ByVal nConcept As Integer, ByVal nCashNum As Integer, ByVal nReason As Integer, ByVal nBordereaux As Integer, ByVal nTypeReplace As Integer, ByVal nBankReplace As Integer, ByVal sChequeNumReplace As String, ByVal dDatePro As Date, ByVal nUsercode As Integer, ByVal nChequeLocat As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCash_acc As Cash_acc
		On Error GoTo insValOP752_Err
		
		lclsErrors = New eFunctions.Errors
		lclsCash_acc = New eCashBank.Cash_acc
		
		'+Si el tipo de operación corresponde a ingreso
		If nMoveType = 1 Then
			
			'+Código compañia debe estar lleno
			If nCompany = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 1046)
			End If
			
			'+Código moneda debe estar lleno
			If nCurrency = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 10827)
			End If
			
			'+Fecha del Cheque debe estar lleno
			If dDateDoc = dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60236)
			End If
			
			'+Código Beneficiario debe estar lleno
			If sBeneficiary = strNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 4122)
			End If
			'+Código Concepto debe estar lleno
			If nConcept = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 7005)
			End If
			'+Código Caja debe estar lleno
			If nCashNum = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60007)
			End If
			'+Monto del cheque debe estar lleno
			If nAmount = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60141)
			End If
		Else
			
			'+Si tipo de Operación es igual a devolución o prorroga, Código Causa debe estar lleno
			If nReason = eRemoteDB.Constants.intNull And (nMoveType = 2 Or nMoveType = 4) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60237)
			End If
		End If
		
		'+Si tipo de Operación es prorroga,la fecha de prorroga debe ser mayor a fecha original del cheque
		If nMoveType = 4 And dDatePro <= dDateDoc Then
			Call lclsErrors.ErrorMessage(sCodispl, 55632)
		End If
		
		'+Debe ser Cheque a fecha
		If nMoveType = 4 And nPostCheque <> 1 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55847)
		End If
		
		'+Si tipo de Operación es Reemplazo por otro cheque
		If nMoveType = 3 And nTypeReplace = 1 Then
			
			'+Banco del Cheque que reemplaza debe estar lleno
			If nBankReplace = 0 Or nBankReplace = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 10828)
			End If
			
			'+Número del Cheque que reemplaza debe estar lleno
			If sChequeNumReplace = strNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60235)
			End If
			
			'+Plaza del Cheque que reeplaza debe estar llena
			If nChequeLocat = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60393)
			End If
			
		End If
		'+ Debe existir la cuenta que se desea a la que se crea el movimiento
		If Not lclsCash_acc.Find_move(nCurrency, nCashNum, nUsercode) Then
			Call lclsErrors.ErrorMessage(sCodispl, 60395)
		End If
		
		'+Si tipo de Operación es Reemplazo por efectivo debe existir la cuenta
		If nMoveType = 3 And nTypeReplace = 2 Then
			If Not lclsCash_acc.Find_move(nCurrency, nCashNum, nUsercode) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60396)
			End If
		End If
		
		'+Si tipo de Operación es Devolución y el cheque esta asociado a una relacion
		'+se debe enviar la siguiente validacion
		If nMoveType = 2 And nBordereaux > 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60410)
		End If
		
		insValOP752 = lclsErrors.Confirm
		
insValOP752_Err: 
		If Err.Number Then
			insValOP752 = "insValOP752: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCash_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_acc = Nothing
		On Error GoTo 0
	End Function
	
	'**%insPostOP752: This method updates the database (as described in the functional specifications)
	'**%for the page "OP752"
	'%insPostOP752: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "OP752"
	Public Function insPostOP752(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nMoveType As Integer, ByVal dDateMove As Date, ByVal nBank_code As Integer, ByVal sChequeNum As String, ByVal nCompany As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal dDateDoc As Date, ByVal nPostCheque As Integer, ByVal sBeneficiary As String, ByVal nConcept As Integer, ByVal nCashNum As Integer, ByVal nReason As Integer, ByVal nBordereaux As Integer, ByVal nTypeReplace As Integer, ByVal nBankReplace As Integer, ByVal sChequeNumReplace As String, ByVal dDatePro As Date, ByVal nUsercode As Integer, ByVal nChequeLocat As Integer) As Boolean
		Dim lrecinsCash_MovOP752 As eRemoteDB.Execute
		On Error GoTo insPostOP752_Err
		
		lrecinsCash_MovOP752 = New eRemoteDB.Execute
		
		sKey = "T" & Format(Now, "yyyyMMddhhmmss") & nUsercode
		
		insPostOP752 = True
		
		With lrecinsCash_MovOP752
			.StoredProcedure = "insCash_MovOP752"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMoveType", nMoveType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateMove", dDateMove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChequeNum", sChequeNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateDoc", dDateDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPostCheque", nPostCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBeneficiary", sBeneficiary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReason", nReason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeReplace", nTypeReplace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankReplace", nBankReplace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChequeNumReplace", sChequeNumReplace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDatePro", dDatePro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChequeLocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostOP752 = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsCash_MovOP752 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCash_MovOP752 = Nothing
		
insPostOP752_Err: 
		If Err.Number Then
			insPostOP752 = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function GetFinanInt(ByVal nAmount_Cheq As Double, ByVal dDoc_date As Date, ByVal dEffecdate As Date) As Object
		
		Dim insCalFinanInt As eRemoteDB.Execute
		Dim nPercent As Double
		
		On Error GoTo GetFinanInt_Err
		
		insCalFinanInt = New eRemoteDB.Execute
		
		With insCalFinanInt
			.StoredProcedure = "insCalfinanint"
			.Parameters.Add("nAmount_cheq", nAmount_Cheq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDoc_date", dDoc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				GetFinanInt = CStr(.Parameters("nPercent").Value)
			End If
		End With
		'UPGRADE_NOTE: Object insCalFinanInt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		insCalFinanInt = Nothing
		
GetFinanInt_Err: 
		If Err.Number Then
			GetFinanInt = 0
		End If
		On Error GoTo 0
		
	End Function
	
	'Find_optBank: Busca la opcion de instalación cobranzas / caja.
	Public Function Find_optBank() As Boolean
		'**-The variable lreaoptBank is declared
		'-Se define la variable lreaoptBank
		
		Dim lreaoptBank As eRemoteDB.Execute
		
		On Error GoTo Find_optBank_Err
		
		lreaoptBank = New eRemoteDB.Execute
		
		With lreaoptBank
			.StoredProcedure = "reaopt_bank"
			
			If .Run(True) Then
				nCollect_P = .FieldToClass("nCollect_P")
				.RCloseRec()
				
				Find_optBank = True
			Else
				Find_optBank = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lreaoptBank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaoptBank = Nothing
		
Find_optBank_Err: 
		If Err.Number Then
			Find_optBank = False
		End If
		On Error GoTo 0
	End Function
	
	'% insValOP752Msg: se realizan las validaciones de la zona masiva de la ventana, cuando se
	'%                 trabaja con depósito/redepósito
	Public Function insValOP752Msg(ByVal sCodispl As String, ByVal nSelected As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValOP752Msg_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Debe seleccionar al menos una linea de la pantalla
		If nSelected = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 750055)
		End If
		
		insValOP752Msg = lclsErrors.Confirm
		
insValOP752Msg_Err: 
		If Err.Number Then
			insValOP752Msg = "insValOP752Msg: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostOP752Msg: Se realizan las actualizaciones de los depósitos/redepósitos
	Public Function insPostOP752Msg(ByVal nMoveType As Integer, ByVal dEffecdate As Date, ByVal nBank_code As Double, ByVal sDep_number As String, ByVal nAcc_bank As Integer, ByVal nCompany As Integer, ByVal nCheopertyp As Integer, ByVal nCurrency As Integer, ByVal sSels As String, ByVal sAmount As String, ByVal sDocNumber As String, ByVal sBordereaux As String, ByVal sDoc_date As String, ByVal sTransac As String, ByVal sOffice As String, ByVal sCashnum As String, ByVal nUsercode As Integer, ByVal sChequeLocat As String, ByVal sBank As String) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insPostOP752Msg_Err
		sKey = "T" & Format(Now, "yyyyMMddhhmmss") & nUsercode
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insCash_movOP752Massive"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMoveType", nMoveType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDep_number", sDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCheopertyp", nCheopertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSels", sSels, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAmount", sAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocNumber", sDocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBordereaux", sBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDoc_date", sDoc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTransac", sTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOffice", sOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCashnum", sCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChequeLocat", sChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBank", sBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostOP752Msg = .Run(False)
		End With
		
insPostOP752Msg_Err: 
		If Err.Number Then
			insPostOP752Msg = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






