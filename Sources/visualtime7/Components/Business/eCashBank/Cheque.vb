Option Strict Off
Option Explicit On
Public Class Cheque
	'%-------------------------------------------------------%'
	'% $Workfile:: Cheque.cls                               $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 27/10/04 15.51                               $%'
	'% $Revision:: 99                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on February 13,2001
	'+ Propiedades según la tabla en el sistema al 13/02/2001.
	'**+ The key field in the table correspond to:nRequest_nu, sCheque and nConsec.
	'+ El campo llave de la tabla corresponde a: nRequest_nu, sCheque y nConsec.
	
	'   Column_name                    Type      Computed Length      Prec  Scale Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nRequest_nu As Double 'int         no        4           10    0     no           (n/a)               (n/a)
	Public sCheque As String 'char        no       10                       no           no                  no
	Public nConsec As Integer 'smallint    no        2           5     0     no           (n/a)               (n/a)
	Public nAmount As Double 'decimal     no        9           14    2     yes          (n/a)               (n/a)
	Public nConcept As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public sClient As String 'char        no       14                       no           no                  no
	Public nBranch_Led As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public nClaim As Double 'int         no        4           10    0     yes          (n/a)               (n/a)
	Public nVoucher_le As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public nVoucher As Integer 'int         no        4           10    0     yes          (n/a)               (n/a)
	Public dDat_propos As Date 'datetime    no        8                       yes          (n/a)               (n/a)
	Public sDescript As String 'char        no       60                       yes          no                  yes
	Public dIssue_Dat As Date 'datetime    no        8                       yes          (n/a)               (n/a)
	Public dLedger_dat As Date 'datetime    no        8                       yes          (n/a)               (n/a)
	Public nNullcode As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public dNulldate As Date 'datetime    no        8                       yes          (n/a)               (n/a)
	Public sPay_freq As String 'char        no        1                       yes          no                  yes
	Public nQ_pays As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public nReceipt As Integer 'int         no        4           10    0     yes          (n/a)               (n/a)
	Public sRequest_ty As String 'char        no        1                       yes          no                  yes
	Public nSta_cheque As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public dStat_date As Date 'datetime    no        8                       yes          (n/a)               (n/a)
	Public nTransac As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public nUser_sol As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public nUsercode As Integer 'int         no        4           10    0     yes          (n/a)               (n/a)
	Public nYear_month As Integer 'int         no        4           10    0     yes          (n/a)               (n/a)
	Public nAcc_bank As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	Public nBordereaux As Double 'int         no        4           10    0     yes          (n/a)               (n/a)
	Public sInter_pay As String 'char        no       14                       yes          no                  yes
	Public nNoteNum As Integer 'int         no        4           10    0     yes          (n/a)               (n/a)
	Public nCashNum As Integer 'int         no        2            5    0     yes          (n/a)               (n/a)
	
	Public nCompany As Integer 'int         no        2            5    0     yes          (n/a)               (n/a)
	Public nTypesupport As Integer 'int         no        2            5    0     yes          (n/a)               (n/a)
	Public nDocSupport As Double 'int         no        2            10    0     yes          (n/a)               (n/a)
	Public nTax_code As Double 'decimal     no        9           14    2     yes          (n/a)               (n/a)
	Public nTax_Percent As Double 'decimal     no        9           14    2     yes          (n/a)               (n/a)
	Public nTax_Amount As Double 'decimal     no        9           14    2     yes          (n/a)               (n/a)
	Public nAfect As Double 'decimal     no        9           14    2     yes          (n/a)               (n/a)
	Public nExcent As Double 'decimal     no        9           14    2     yes          (n/a)               (n/a)
	Public nCurrencyPay As Integer 'int         no        2            5    0     yes          (n/a)               (n/a)
	Public nCurrencyOri As Integer 'int         no        2            5    0     yes          (n/a)               (n/a)
	Public nAmountPay As Double 'decimal     no        9           14    2     yes          (n/a)               (n/a)
	Public nOfficePay As Integer 'int         no        2            5    0     yes          (n/a)               (n/a)
	Public nInsur_area As Integer 'int         no        2            5    0     yes          (n/a)               (n/a)
	Public nTaxCode As Integer 'int         no        2            5    0     yes          (n/a)               (n/a)
	Public nAmount_Local As Double 'decimal     no        9           14    2     yes          (n/a)               (n/a)
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public sClientInter As String
	Public sClientUser As String
    Public sCliename As String 
    Public sDigit As String 
	
	'**- Auxiliary Variables
	'- Variables auxiliares
	Public nExternal_Concept As Integer 
	Public nAcc_type As Integer
	Public sAcco_num As String
	Public nBank_code As Integer
	Public nBk_agency As Integer
	Public sN_Aba As String
	Public sBenef_name As String
	Public sInter_name As String
	Public sUser_name As String
	Public sBank_name As String
	Public nBank_curr As Integer
	Public sAcc_number As String
	Public nStatusPay As Integer
	Public nClieop As Integer
	Public nCount_emited As Integer
	Public nCount_canceled As Integer
	Public nCount_pend As Integer
	Public nCount_numered As Integer
	Public nPay_Order As Integer
	Public nCurrency As Integer
	Public nOffice As Integer '+Sucursal
	Public nOfficeAgen As Integer '+Oficina
	Public nAgency As Integer '+Agencia
	Public sAccountHolder As String 
	Public nBankExt As Integer 
    Public sBankAccount As String 
	Public dRescuedate As Date
	Private mclsCheque As Cheque
	Public nTotalPay As Double
	Public sClient_Digit As String
	Public sDesOffice As String
	Public sDesOfficeAgen As String
	Public sDesAgency As String
	Public sBank_code As String
	Public sCurrency As String
    Public sMessage_sts As String
	Public nAvailable As Double
	Public nTransit_1 As Double
	Public nTransit_2 As Double
	Public nTransit_3 As Double
	Public nTransit_4 As Double
    Public nTransit_5 As Double

    Public sMessage As String
    Public nId_ExternalSystem As Integer 
	
	'**- Used in the routines that are in insPostOP008
	'- Utilizadas en las rutinas que se encuentran dentro de insPostOP008
	
	Private mintNullcode As Integer
	Private mdtmNulldate As Date
	Private mintUsercode As Integer
	
	'**- Used in the routines that are in insValOp008_K
	'- Utilizadas en las rutinas que se encuentran dentro de insValOP008_K
	
	Private mlngRequest_nu As Integer
	Private mintOptNull As Integer
	Private mstrCheque As String
	
	'**- Used in the routines that are in insPostOP007
	'- Utilizadas en las rutinas que se encuentran dentro de insPostOP007
	
	Private mdblAmount As Double
	Private mintConcept As Integer
	Private mstrClient As String
	Private mdtmDat_propos As Date
	Private mstrDescript As String
	Private mdtmIssue_dat As Date
	Private mdtmLedger_dat As Date
	Private mstrPay_freq As String
	Private mintQ_pays As Integer
	Private mintUser_sol As Integer
	Private mintAcc_bank As Integer
	Private mstrInter_pay As String
	Private mlngNotenum As Integer
	Private mintCurrenAcc As Integer
	
	'**+ DEfine the following variables for handling the window OP006 (Payment orders)
	'+Se definen las siguientes variables para el manejo de la ventana OP006 (Ordenes de pagos)
	
	Private mstrCodispl As String
	Private mintAction As Integer
	Private mintPayOrderTyp As Integer
	Private mintCurrency As Integer
	Private mIntCompany As Integer
	Private mdtmChequeDate As Date
	Private mdblRequest_nu As Double
	
	'**% Add: creates a new registration inside the checks apply table (Checks)
	'% Add: Crea un nuevo registro dentro de la tabla de solicitud de cheques (Cheques)
	Public Function Add() As Boolean
		
		'**- Variable definition lreccreCheques
		'- Se define la variable lreccreCheques
		
		Dim lreccreCheques As eRemoteDB.Execute
		lreccreCheques = New eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		'**+Parameter definition for stored procedure 'insudb.creCheques'
		'+ Definición de parámetros para stored procedure 'insudb.creCheques'
		'**+ Data of February 13,2001 08:59:29 a.m.
		'+ Información leída el 13/02/2001 08:59:29 a.m.
		
		With lreccreCheques
			.StoredProcedure = "creCheques"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher_le", nVoucher_le, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDat_propos", dDat_propos, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 650, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssue_dat", dIssue_Dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_freq", sPay_freq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_pays", nQ_pays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequest_ty", sRequest_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_cheque", nSta_cheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_sol", nUser_sol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_month", nYear_month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_pay", sInter_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_type", nAcc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcco_num", sAcco_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sN_Aba", sN_Aba, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDocSupport", nDocSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_percent", nTax_Percent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyOri", nCurrencyOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyPay", nCurrencyPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountPay", nAmountPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficepay", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxCode", nTaxCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAfect", nAfect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExcent", nExcent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_amount", nTax_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_local", nAmount_Local, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccountHolder", sAccountHolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBankAccount", sBankAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExternal_Concept", nExternal_Concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreCheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCheques = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**% CountCanceled: Restores the cuantity of canceled checks (damaged)
	'** of an associated chekbook to a bank account
	'% CountCanceled: Devuelve la cantidad de cheques cancelados (dañados)
	'  de una chequera asociada a una cuenta bancaria
	Public Function CountCanceled(ByVal Acc_bank As Integer, ByVal sCheque_sta As String, ByVal sCheque_end As String) As Boolean
		
		'**- Variable definition lrecreaCheques_CountCanceled
		'- Se define la variable lrecreaCheques_CountCanceled
		
		Dim lrecreaCheques_CountCanceled As eRemoteDB.Execute
		
		On Error GoTo CountCanceled_Err
		
		lrecreaCheques_CountCanceled = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaCheques_CountCanceled'
		'+ Definición de parámetros para stored procedure 'insudb.reaCheques_CountCanceled'
		'**+ Data of March 1st,2001  02:32:10
		'+ Información leída el 01/03/2001 02:32:10 p.m.
		
		With lrecreaCheques_CountCanceled
			.StoredProcedure = "reaCheques_CountCanceled"
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque_sta", sCheque_sta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque_end", sCheque_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nCount_canceled = .FieldToClass("nResponse")
				.RCloseRec()
				CountCanceled = True
			Else
				CountCanceled = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCheques_CountCanceled may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCheques_CountCanceled = Nothing
		
CountCanceled_Err: 
		If Err.Number Then
			CountCanceled = False
		End If
		On Error GoTo 0
	End Function
	
	'**% CountEmited: Restores the amount of written checks from
	'**% an associated checkbook to a bank account
	'% CountEmited: Devuelve la cantidad de cheques emitidos
	'  de una chequera asociada a una cuenta bancaria
	Public Function CountEmited(ByVal Acc_bank As Integer, ByVal sCheque_sta As String, ByVal sCheque_end As String) As Boolean
		
		'**- Variable definition lrecreaCheques_CountEmited
		'- Se define la variable lrecreaCheques_CountEmited
		
		Dim lrecreaCheques_CountEmited As eRemoteDB.Execute
		
		On Error GoTo CountEmited_Err
		
		lrecreaCheques_CountEmited = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaCheques_CountEmited'
		'+ Definición de parámetros para stored procedure 'insudb.reaCheques_CountEmited'
		'**+ Data of March 1st,2001  02:09:33 p.m.
		'+ Información leída el 01/03/2001 02:09:33 p.m.
		
		With lrecreaCheques_CountEmited
			.StoredProcedure = "reaCheques_CountEmited"
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque_sta", sCheque_sta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque_end", sCheque_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nCount_emited = .FieldToClass("nResponse")
				.RCloseRec()
				CountEmited = True
			Else
				CountEmited = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCheques_CountEmited may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCheques_CountEmited = Nothing
		
CountEmited_Err: 
		If Err.Number Then
			CountEmited = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindByPayOrder: Restores the values of the checks application
	'% FindByPayOrder: Devuelve los valores de las solicitudes de cheques
	Public Function FindByPayOrder(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nConsec As Integer, Optional ByVal nBordereaux As Double = 0) As Boolean
		
		'**- Variable definition lrecreachequesOP006
		'- Se define la variable lrecreaChequesOP006
		
		Dim lrecreaChequesOP006 As eRemoteDB.Execute
		lrecreaChequesOP006 = New eRemoteDB.Execute
		
		On Error GoTo FindByPayOrder_Err
		
		'**+ Parameter definition for stored procedure 'insud.reahequeOP006'
		'+ Definición de parámetros para stored procedure 'insudb.reaChequesOP006'
		'**+ Data of February 13,2001  04:13:49 p.m.
		'+ Información leída el 13/02/2001 04:13:49 p.m.
		
		With lrecreaChequesOP006
			.StoredProcedure = "reaChequesOP006"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nRequest_nu = .FieldToClass("nRequest_nu")
				Me.sCheque = .FieldToClass("sCheque")
				Me.nConsec = .FieldToClass("nConsec")
				nAmount = .FieldToClass("nAmount", 0)
				nConcept = .FieldToClass("nConcept")
				sClient = .FieldToClass("sClient")
				dDat_propos = .FieldToClass("dDat_propos")
				sDescript = .FieldToClass("sDescript")
				dIssue_Dat = .FieldToClass("dIssue_dat")
				dLedger_dat = .FieldToClass("dLedger_dat")
				nAcc_bank = .FieldToClass("nAcc_bank")
				sInter_pay = .FieldToClass("sInter_pay")
				nUser_sol = .FieldToClass("nUser_sol")
				nBranch_Led = .FieldToClass("nBranch_led")
				nClaim = .FieldToClass("nClaim")
				nVoucher_le = .FieldToClass("nVoucher_le")
				nVoucher = .FieldToClass("nVoucher")
				nNullcode = .FieldToClass("nNullcode")
				sRequest_ty = .FieldToClass("sRequest_ty")
				dNulldate = .FieldToClass("dNulldate")
				sPay_freq = .FieldToClass("sPay_freq")
				nQ_pays = .FieldToClass("nQ_pays", 0)
				nReceipt = .FieldToClass("nReceipt")
				nSta_cheque = .FieldToClass("nSta_cheque")
				dStat_date = .FieldToClass("dStat_date")
				nTransac = .FieldToClass("nTransac")
				nYear_month = .FieldToClass("nYear_month")
				nBordereaux = .FieldToClass("nBordereaux")
				sBenef_name = .FieldToClass("sBenefName")
				sInter_name = .FieldToClass("sInterName")
				sUser_name = .FieldToClass("sUserName")
				sBank_name = .FieldToClass("sBankName")
				nBank_curr = .FieldToClass("nCurrency")
				nNoteNum = .FieldToClass("nNotenum")
				sAcc_number = .FieldToClass("sAcc_number")
				nCompany = .FieldToClass("nCompany")
				FindByPayOrder = True
				.RCloseRec()
			Else
				FindByPayOrder = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaChequesOP006 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaChequesOP006 = Nothing
		
FindByPayOrder_Err: 
		If Err.Number Then
			FindByPayOrder = False
		End If
		On Error GoTo 0
	End Function
	
	'**% FindCheqToPrint: Restores the amount of pending checks for printing
	'** from a given checks application
	'% FindCheqToPrint: Devuelve la cantidad de cheques pendientes por impresión
	' pertenecientes a una solicitud de cheques dada
	Public Function FindCheqToPrint(ByVal nRequest_nu As Double) As Boolean
		
		'**- Variable definition lrecreaCheques_c2
		'- Se define la variable lrecreaCheques_c2
		
		Dim lrecreaCheques_c2 As eRemoteDB.Execute
		
		On Error GoTo FindCheqToPrint_Err
		
		lrecreaCheques_c2 = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaCheques_c2'
		'+ Definición de parámetros para stored procedure 'insudb.reaCheques_c2'
		'**+ Data of March 08,2001  02:31:55 p.m.
		'+ Información leída el 08/03/2001 02:31:55 p.m.
		
		With lrecreaCheques_c2
			.StoredProcedure = "reaCheques_c2"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				nCount_pend = .Parameters("nCount").Value
				FindCheqToPrint = True
			Else
				FindCheqToPrint = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCheques_c2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCheques_c2 = Nothing
		
FindCheqToPrint_Err: 
		If Err.Number Then
			FindCheqToPrint = False
		End If
		On Error GoTo 0
	End Function
	
	'**% FindChqWithNumber: Restores the amount of checks that have Check Number,
	'** from a given checks application
	'% FindChqWithNumber: Devuelve la cantidad de cheques que poseen Número de Cheque,
	'  pertenecientes a una solicitud de cheques dada
	Public Function FindChqWithNumber(ByVal nRequest_nu As Double) As Boolean
		
		'**- Variable definition lrecreaCheques_c
		'- Se define la variable lrecreaCheques_c
		
		Dim lrecreaCheques_c As eRemoteDB.Execute
		
		On Error GoTo FindChqWithNumber_Err
		
		lrecreaCheques_c = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaCheques_c'
		'+ Definición de parámetros para stored procedure 'insudb.reaCheques_c'
		'**+ Data of March 13,2001 09:25:05
		'+ Información leída el 13/03/2001 09:25:05 a.m.
		
		With lrecreaCheques_c
			.StoredProcedure = "reaCheques_c"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nCount_numered = .FieldToClass("nCount")
				FindChqWithNumber = True
				.RCloseRec()
			Else
				FindChqWithNumber = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCheques_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCheques_c = Nothing
		
FindChqWithNumber_Err: 
		If Err.Number Then
			FindChqWithNumber = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**% insValChequeExist: This routine allows to verify if the check exists
	'%insValChequeExist: Esta rutina permite verificar si el cheque existe
	Public Function valChequeExists(ByRef sCheque As String) As Boolean
		'- Variable definition lrec_chequesExist that will be used as a cursor.
		'-Se define la variable lrec_chequesExist que se utilizará como cursor.
		Dim lrec_chequesExist As eRemoteDB.Execute
		
		'** Parameter definition for stored procedure 'insudb.insValCheques'
		'Definición de parámetros para stored procedure 'insudb.insValCheques'
		'** Data of December 06,1999 04:40:19 p.m.
		'Información leída el 06/12/1999 04:40:19 p.m.
		valChequeExists = True
		lrec_chequesExist = New eRemoteDB.Execute
		With lrec_chequesExist
			.StoredProcedure = "insValCheques"
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProce", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			valChequeExists = .Run(False)
			If valChequeExists Then
				valChequeExists = (CDbl(0 & .Parameters("nProce").Value) > 0)
			End If
		End With
		'UPGRADE_NOTE: Object lrec_chequesExist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_chequesExist = Nothing
	End Function
	
	
	Public Function GenerateClieop(ByVal Branch As Integer, ByVal Acc_bank As Integer, ByVal Date_when_sent As Date, ByVal Date_proccess As Date, ByVal Batch_text As String, ByVal Payment_date As Date, ByVal Date_now As Date, ByVal Company As Integer) As Boolean
		
		Dim lrecInsClieopDebit As eRemoteDB.Execute
		
		On Error GoTo GenerateClieop_Err
		
		lrecInsClieopDebit = New eRemoteDB.Execute
		
		'** Parameter definition for stored procedure 'insudb.InsClienopDebit'
		'Definición de parámetros para stored procedure 'insudb.InsClieopDebit'
		'** Data of January 10, 2001  2:35:38 p.m.
		'Información leída el 10/01/2001 2:35:38 PM
		
		With lrecInsClieopDebit
			.StoredProcedure = "InsClieopDebit"
			.Parameters.Add("nBranch", Branch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", Acc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_when_sent", Date_when_sent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_proccess", Date_proccess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBatch_text", Batch_text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 45, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPayment_date", Payment_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_now", Date_now, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", Company, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nClieop", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				GenerateClieop = True
				nClieop = .Parameters("nClieop").Value
			Else
				GenerateClieop = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecInsClieopDebit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsClieopDebit = Nothing
		
GenerateClieop_Err: 
		If Err.Number Then
			GenerateClieop = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update: updates the registration in the checks application table
	'% Update: Actualiza los registros dentro de la tabla de solicitud de cheques (Cheques).
	Public Function Update() As Boolean
		
		'**- Variable definition lrecupdCheques
		'- Se define la variable lrecupdCheques
		
		Dim lrecUpdCheques As eRemoteDB.Execute
		lrecUpdCheques = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+ Parameter definition for stored procedure 'insudb.updChques'
		'+ Definición de parámetros para stored procedure 'insudb.updCheques'
		'**+ Data of February 13, 2001   06:21:07 p.m.
		'+ Información leída el 13/02/2001 06:21:09 p.m.
		
		With lrecUpdCheques
			.StoredProcedure = "updCheques"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher_le", nVoucher_le, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDat_propos", dDat_propos, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 650, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssue_dat", dIssue_Dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_freq", sPay_freq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_pays", nQ_pays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequest_ty", sRequest_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_cheque", nSta_cheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_sol", nUser_sol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_month", nYear_month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_pay", sInter_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDocSupport", nDocSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_percent", nTax_Percent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyOri", nCurrencyOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyPay", nCurrencyPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountPay", nAmountPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOfficePay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_Code", nTax_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccountHolder", sAccountHolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankExt", nBankExt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAcc_type", nAcc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBankAccount", sBankAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExternal_Concept", nExternal_Concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdCheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdCheques = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**% UpdChequeStat: updates the registration (the field Check Status)
	'** in the table of Checks Applications (Checks)
	'% UpdChequeStat: Actualiza registros (el campo Estado del Cheque)
	'  dentro de la tabla de Solicitud de Cheques (Cheques)
	Public Function UpdChequeStat() As Boolean
		
		'**- Variable definition lrecupdCheques_stat
		'- Se define la variable lrecupdCheques_stat
		
		Dim lrecupdCheques_stat As eRemoteDB.Execute
		lrecupdCheques_stat = New eRemoteDB.Execute
		
		On Error GoTo UpdChequeStat_Err
		
		'**+ Parameter definition for stored procedure 'insudb.updCheques_stat'
		'+ Definición de parámetros para stored procedure 'insudb.updCheques_stat'
		'**+ Data of February 22,2001  10:34:10   a.m.
		'+ Información leída el 22/02/2001 10:34:10 a.m.
		
		With lrecupdCheques_stat
			.StoredProcedure = "updCheques_stat"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_cheque", nSta_cheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdChequeStat = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdCheques_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCheques_stat = Nothing
		
UpdChequeStat_Err: 
		If Err.Number Then
			UpdChequeStat = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insCreCheques: This functions is in charge of adding the information in treat of the
	'**% pricipal table for the transaction.
	'%insCreCheques: Esta función se encarga de agregar la información en tratamiento de la
	'%tabla principal para la transacción.
	Private Function insCreCheques(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal sDescript As String, ByVal nCurrencyOri As Integer, ByVal nAmount As Double, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nCurrencyPay As Integer, ByVal nAmountPay As Double, ByVal nTypesupport As Integer, ByVal nDocSupport As Double, ByVal nTax_code As Integer, ByVal nTax_Percent As Double, ByVal nTax_Amount As Double, ByVal nAfect As Double, ByVal nExcent As Double, ByVal sInter_pay As String, ByVal dDat_propos As Date, ByVal dLedger_dat As Date, ByVal nUser_sol As Integer, ByVal sRequest_ty As String, ByVal dIssue_Dat As Date, ByVal nUsercode As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nBranch_Led As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal sCodispl As String = "", Optional ByVal nAcc_bank As Integer = 0, Optional ByVal tcnAmounttotal As Double = 0, Optional ByVal sAccountHolder As String = "", Optional ByVal nBankExt As Integer = 0, Optional nAcc_Type As Integer = 0, Optional ByVal sBankAccount As String = "", Optional ByVal nExternal_Concept As Integer = 0) As Boolean
		Dim lclsCash_mov As Cash_mov
		Dim lclsCash_Num As User_cashnum
		Dim lintSta_cheque As Byte
		Dim lintCashNum As Integer
		Dim lclsExchange As eGeneral.Exchange
		
		lclsCash_mov = New Cash_mov
		lclsExchange = New eGeneral.Exchange
		
		On Error GoTo insCreCheques_Err
		
		insCreCheques = True
		
		lclsCash_Num = New User_cashnum
		If lclsCash_Num.Find_nUser(nUsercode, True) Then
			lintCashNum = lclsCash_Num.nCashNum
		Else
			lintCashNum = eRemoteDB.Constants.intNull
		End If
		'UPGRADE_NOTE: Object lclsCash_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_Num = Nothing
		
		
		If sRequest_ty = "1" Then
			With lclsCash_mov
				.nAcc_cash = 9998
				.nCurrency = nCurrencyPay
				.nOffice = nOffice
				.nTransac = eRemoteDB.Constants.intNull
				.dEffecdate = dDat_propos
				.nAmount = nAmountPay
				.nBank_code = eRemoteDB.Constants.intNull
				.nBranch_Led = nBranch_Led
				.dCard_expir = dtmNull
				.sCard_num = String.Empty
				.nCard_typ = eRemoteDB.Constants.intNull
				.nCl_transac = eRemoteDB.Constants.intNull
				.nClaim = eRemoteDB.Constants.intNull
				.nCompanyc = eRemoteDB.Constants.intNull
				.nVoucher_le = eRemoteDB.Constants.intNull
				.nVoucher = eRemoteDB.Constants.intNull
				If sCodispl <> "OP006" Then
					.nConcept = nConcept
				Else
					.nConcept = eRemoteDB.Constants.intNull
				End If
				.nContrat = eRemoteDB.Constants.intNull
				.sDep_number = String.Empty
				.dDoc_date = dtmNull
				.sDocnumbe = CStr(nDocSupport)
				.nDraft = eRemoteDB.Constants.intNull
				.nExchange = eRemoteDB.Constants.intNull
				.nIntermed = eRemoteDB.Constants.intNull
				.nMov_type = 1
				.sNull_movem = String.Empty
				.sNull_recor = String.Empty
				.dNulldate = dtmNull
				.nPaynumbe = eRemoteDB.Constants.intNull
				.nReceipt = eRemoteDB.Constants.intNull
				.nUsercode = nUsercode
				.nYear_month = eRemoteDB.Constants.intNull
				.nBordereaux = eRemoteDB.Constants.intNull
				.nTyp_acco = eRemoteDB.Constants.intNull
				.sType_acc = String.Empty
				.sClient = sInter_pay
				.sNumForm = String.Empty
				.nAcc_bank = eRemoteDB.Constants.intNull
				.sDescript = sDescript
				.nUser_sol = nUser_sol
				.dLedger_dat = dLedger_dat
				.nNoteNum = eRemoteDB.Constants.intNull
				.nUpdate = eRemoteDB.Constants.intNull
				.nCashNum = lintCashNum
				.nCompany = nCompany
				.nChequeLocat = eRemoteDB.Constants.intNull
				.nCod_Agree = eRemoteDB.Constants.intNull
				.nBank_Agree = eRemoteDB.Constants.intNull
				.dCollection = dtmNull
				.nInputChannel = 1
				.nTypesupport = nTypesupport
				.nBulletins = eRemoteDB.Constants.intNull
				.dValDate = dIssue_Dat
				.nOri_Curr = nCurrencyOri
				.nOri_Amount = nAmount
				.nFin_Int = eRemoteDB.Constants.intNull
				.nCheque_Stat = eRemoteDB.Constants.intNull
				.nBranch = nBranch
				.nProduct = nProduct
				insCreCheques = .Add
			End With
		End If
		
		With Me
			.nRequest_nu = nRequest_nu
			.sCheque = sCheque
			.nCompany = nCompany
			.nConcept = nConcept
			.nConsec = 0
			.sDescript = sDescript
			.nCurrencyOri = nCurrencyOri
			.nAmount = nAmount
			.nOffice = nOffice
			.nOfficeAgen = nOfficeAgen
			.nAgency = nAgency
			.nCurrencyPay = nCurrencyPay
			If tcnAmounttotal <> eRemoteDB.Constants.intNull Then
				.nAmountPay = tcnAmounttotal
			Else
				.nAmountPay = nAmountPay
			End If
			.nTypesupport = nTypesupport
			.nDocSupport = nDocSupport
			.nTaxCode = nTax_code
			.nTax_Percent = nTax_Percent
			.nTax_Amount = nTax_Amount
			.nAfect = nAfect
			.nExcent = nExcent
			.sClient = sInter_pay
			.sInter_pay = sInter_pay
			.dDat_propos = dDat_propos
			.dLedger_dat = dLedger_dat
			.nUser_sol = nUser_sol
			.sRequest_ty = sRequest_ty
			.dIssue_Dat = dIssue_Dat
			.nUsercode = nUsercode
			.nCashNum = lintCashNum
			.nBranch_Led = nBranch_Led
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			If nAcc_bank <> eRemoteDB.Constants.intNull And nAcc_bank <> 0 Then
				.nAcc_bank = nAcc_bank
			End If
			If CDbl(sRequest_ty) = 3 Then
				.nSta_cheque = 8
			End If
			
			If nCurrencyPay <> 1 Then
				If lclsExchange.Find(nCurrencyPay, dDat_propos, True) Then
					.nAmount_Local = nAmountPay * lclsExchange.nExchange
				End If
			Else
				.nAmount_Local = nAmountPay
			End If
			If sRequest_ty = "3" Then
				'+ El estado del cheque es impreso
				.nSta_cheque = 2
			Else
				'+ El estado del cheque es pendiente
				.nSta_cheque = 1
			End If
			.dStat_date = dDat_propos
			.nOfficePay = nOffice
            .sAccountHolder = sAccountHolder 
            .nBankExt = nBankExt
            .nAcc_Type = nAcc_Type
            .sBankAccount = sBankAccount
            .nExternal_Concept = nExternal_Concept
            
			If insCreCheques = True Then
				insCreCheques = .Add
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_mov = Nothing
		'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExchange = Nothing
		
insCreCheques_Err: 
		If Err.Number Then
			insCreCheques = False
		End If
		On Error GoTo 0
	End Function
	
	'%insCreCheques_VI009 :
	Public Function insCreCheques_VI009() As Boolean
		Dim lprmParameters(34) As Object
		Dim lreccreCheques As eRemoteDB.Execute
		Dim lreccreCash_mov As eRemoteDB.Execute
		
		lreccreCheques = New eRemoteDB.Execute
		
		If nPay_Order = 1 Then
			
			'**+ The cash egress movement is created
			'+ Se crea el movimiento de egreso de caja
			lreccreCash_mov = New eRemoteDB.Execute
			
			'** Parameter definition for sotered procedure 'insudb.creCash_mov'
			'Definición de parámetros para stored procedure 'insudb.creCash_mov'
			'** Data of July 10, 2000 10:01:48
			'Información leída el 10/07/2000 10:01:48
			With lreccreCash_mov
				.StoredProcedure = "creCash_mov"
				.Parameters.Add("nAcc_cash", 9998, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nTransac", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dRescuedate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBank_code", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBranch_led", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dCard_expir", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sCard_num", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nCard_typ", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nCl_transac", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nClaim", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nCompanyc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nVoucher_le", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nVoucher", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nContrat", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dDat_return", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sDep_number", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dDoc_date", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sDocnumbe", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nDraft", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nExchange", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nIntermed", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nMov_type", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sNull_movem", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sNull_recor", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nPaynumbe", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nReceipt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nYear_month", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBordereaux", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nTyp_acco", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sType_acc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sNumForm", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sInter_pay", sInter_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUser_sol", nUser_sol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nNoteNum", lprmParameters(34), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUpdate", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Run(False)
			End With
			'UPGRADE_NOTE: Object lreccreCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lreccreCash_mov = Nothing
		End If
		
		'** Parameter definition for stored procedure 'insudb.creCheques'
		'Definición de parámetros para stored procedure 'insudb.creCheques'
		'** Data of January 21, 2000  11:01:06
		'Información leída el 21/01/2000 11:01:06
		With lreccreCheques
			.StoredProcedure = "creCheques"
			.Parameters.Add("nRequest_nu", lprmParameters(0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", lprmParameters(1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", lprmParameters(2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", lprmParameters(3), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", lprmParameters(4), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", lprmParameters(5), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", lprmParameters(6), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", lprmParameters(7), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher_le", lprmParameters(8), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", lprmParameters(9), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDat_propos", lprmParameters(10), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", lprmParameters(11), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssue_dat", lprmParameters(12), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", lprmParameters(13), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", lprmParameters(14), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", lprmParameters(15), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_freq", lprmParameters(16), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_pays", lprmParameters(17), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", lprmParameters(18), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequest_ty", lprmParameters(19), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_cheque", lprmParameters(20), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStat_date", lprmParameters(21), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", lprmParameters(22), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_sol", lprmParameters(23), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lprmParameters(24), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_month", lprmParameters(25), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", lprmParameters(26), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", lprmParameters(27), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_pay", lprmParameters(28), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_type", lprmParameters(29), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcco_num", lprmParameters(30), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", lprmParameters(31), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBk_agency", lprmParameters(32), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sN_Aba", lprmParameters(33), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", lprmParameters(34), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDocSupport", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_percent", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyOri", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyPay", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountPay", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficepay", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAfect", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExcent", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_amount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_local", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccountHolder", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankExt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBankAccount", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				insCreCheques_VI009 = True
			End If
		End With
		'UPGRADE_NOTE: Object lreccreCheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCheques = Nothing
	End Function
	
	'%insCreCheques_VI011 :
	Public Function insCreCheques_VI011() As Boolean
		Dim lreccreCheques As eRemoteDB.Execute
		
		lreccreCheques = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.creCheques'
		'+ Definición de parámetros para stored procedure 'insudb.creCheques'
		'**+ Data of January 21, 2000  11:01:06
		'+ Información leída el 21/01/2000 11:01:06
		
		With lreccreCheques
			.StoredProcedure = "creCheques"
			.Parameters.Add("nRequest_nu", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher_le", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDat_propos", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssue_dat", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_freq", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_pays", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequest_ty", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_cheque", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStat_date", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_sol", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_month", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_pay", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_type", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAcco_num", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBk_agency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sN_Aba", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDocSupport", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_percent", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyOri", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyPay", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountPay", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficepay", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAfect", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExcent", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_amount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_local", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccountHolder", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankExt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBankAccount", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				insCreCheques_VI011 = True
			End If
		End With
		'UPGRADE_NOTE: Object lreccreCheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCheques = Nothing
	End Function
	
	'**% insUpdCheques: This function is in charge of updatin the information in treat of the
	'**% principal table for the transaction
	'% insUpdCheques: Esta función se encarga de actualizar la información en tratamiento de la
	'% tabla principal para la transacción.
	Private Function insUpdCheques(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal sDescript As String, ByVal nCurrencyOri As Integer, ByVal nAmount As Double, ByVal nCurrencyPay As Integer, ByVal nAmountPay As Double, ByVal nTypesupport As Integer, ByVal nDocSupport As Double, ByVal nTax_code As Integer, ByVal nTax_Percent As Double, ByVal nTax_Amount As Double, ByVal nAfect As Double, ByVal nExcent As Double, ByVal sInter_pay As String, ByVal dDat_propos As Date, ByVal dLedger_dat As Date, ByVal nUser_sol As Integer, ByVal sRequest_ty As String, ByVal dIssue_Dat As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nUsercode As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nBranch_Led As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nAcc_bank As Integer = 0, Optional ByRef nAmounttotal As Double = 0, Optional ByVal sAccountHolder As String = "", Optional ByVal nBankExt As Integer = 0, Optional nAcc_Type As Integer = 0, Optional ByVal sBankAccount As String = "", Optional ByVal nExternal_Concept As Integer = 0) As Boolean
		Dim lclsCash_mov As eCashBank.Cash_mov
		Dim lclsOpt_system As eGeneral.Opt_system
		Dim lclsCash_Num As User_cashnum
		Dim lblnFound As Boolean
		
		On Error GoTo insUpdCheques_Err
		lclsCash_mov = New eCashBank.Cash_mov
		lclsOpt_system = New eGeneral.Opt_system
		
		lblnFound = False
		insUpdCheques = True
		
		If sRequest_ty = "3" Then
			lblnFound = Me.FindByPayOrder(eRemoteDB.Constants.intNull, sCheque, eRemoteDB.Constants.intNull)
		ElseIf sRequest_ty <> "1" Then 
			lblnFound = Me.FindByPayOrder(nRequest_nu, String.Empty, 0)
		End If
		
		If lblnFound Then
			'**+ If the amount of the pay order was modified, the book keeper voucher is annuled
			'**+ generating a new one with negatives values.
			'+ Si el monto de la orden de pago fue modificado,
			'+ se anula el comprobante contable generando uno con montos negativos
			If (Me.nAmount <> nAmountPay) And Me.nVoucher > 0 Then
				
				If lclsOpt_system.Find Then
					lclsCash_mov.NullVoucher(lclsOpt_system.nCompany, Me.nVoucher, nUsercode)
				End If
			End If
		End If
		
		lclsCash_Num = New User_cashnum
		If lclsCash_Num.Find_nUser(nUser_sol, True) Then
			Me.nCashNum = lclsCash_Num.nCashNum
		Else
			Me.nCashNum = eRemoteDB.Constants.intNull
		End If
		'UPGRADE_NOTE: Object lclsCash_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_Num = Nothing
		
		If sRequest_ty = "1" Then
			With lclsCash_mov
				.nAcc_cash = 9998
				.nCurrency = nCurrency
				.nOffice = nOffice
				.nTransac = eRemoteDB.Constants.intNull
				.dEffecdate = dDat_propos
				.nAmount = nAmountPay
				.nConcept = nConcept
				.nMov_type = 1
				.nUsercode = nUsercode
				.sClient = sInter_pay
				.sDescript = sDescript
				.nUser_sol = nUser_sol
				.dLedger_dat = dLedger_dat
				.nUpdAvailable = 1
				.nCashNum = Me.nCashNum
				.nBranch_Led = nBranch_Led
				.nBranch = nBranch
				.nProduct = nProduct
				insUpdCheques = .UpdateByPayOrder
			End With
		End If
		
		With Me
			.nRequest_nu = nRequest_nu
			.sCheque = sCheque
			.nCompany = nCompany
			.nConcept = nConcept
			.sDescript = sDescript
			.nCurrencyOri = nCurrencyOri
			.nAmount = nAmount
			.nOfficePay = nOffice
			.nCurrencyPay = nCurrencyPay
			If nAmounttotal <> eRemoteDB.Constants.intNull Then
				.nAmountPay = nAmounttotal
			Else
				.nAmountPay = nAmountPay
			End If
			.nTypesupport = nTypesupport
			.nDocSupport = nDocSupport
			.nTax_code = nTax_code
			.nTax_Percent = nTax_Percent
			.nTax_Amount = nTax_Amount
			.nAfect = nAfect
			.nExcent = nExcent
			.sClient = sInter_pay
			.dDat_propos = dDat_propos
			.dLedger_dat = dLedger_dat
			.nUser_sol = nUser_sol
			.sRequest_ty = sRequest_ty
			.dIssue_Dat = dIssue_Dat
			.nUsercode = nUsercode
			.nBranch_Led = nBranch_Led
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nOffice = nOffice
			.nOfficeAgen = nOfficeAgen
			.nAgency = nAgency
            .sAccountHolder = sAccountHolder
            .nBankExt = nBankExt
            .nAcc_Type = nAcc_Type 
            .sBankAccount = sBankAccount
            .nExternal_Concept = nExternal_Concept
			If nAcc_bank <> eRemoteDB.Constants.intNull And nAcc_bank <> 0 Then
				.nAcc_bank = nAcc_bank
			End If
			If CDbl(sRequest_ty) = 3 Then
				.nSta_cheque = 8
			End If
			
			If insUpdCheques = True Then
				insUpdCheques = .Update
			End If
		End With
		
insUpdCheques_Err: 
		If Err.Number Then
			insUpdCheques = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsOpt_system may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsOpt_system = Nothing
		'UPGRADE_NOTE: Object lclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_mov = Nothing
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.nRequest_nu = eRemoteDB.Constants.intNull
		Me.sCheque = String.Empty
		Me.nConsec = eRemoteDB.Constants.intNull
		Me.nAmount = eRemoteDB.Constants.intNull
		Me.nConcept = eRemoteDB.Constants.intNull
		Me.sClient = String.Empty
		Me.nBranch_Led = eRemoteDB.Constants.intNull
		Me.nClaim = eRemoteDB.Constants.intNull
		Me.nVoucher_le = eRemoteDB.Constants.intNull
		Me.nVoucher = eRemoteDB.Constants.intNull
		Me.dDat_propos = dtmNull
		Me.sDescript = String.Empty
		Me.dIssue_Dat = dtmNull
		Me.dLedger_dat = dtmNull
		Me.nNullcode = eRemoteDB.Constants.intNull
		Me.dNulldate = dtmNull
		Me.sPay_freq = String.Empty
		Me.nQ_pays = eRemoteDB.Constants.intNull
		Me.nReceipt = eRemoteDB.Constants.intNull
		Me.sRequest_ty = String.Empty
		Me.nSta_cheque = eRemoteDB.Constants.intNull
		Me.dStat_date = dtmNull
		Me.nTransac = eRemoteDB.Constants.intNull
		Me.nUser_sol = eRemoteDB.Constants.intNull
		Me.nUsercode = eRemoteDB.Constants.intNull
		Me.nYear_month = eRemoteDB.Constants.intNull
		Me.nAcc_bank = eRemoteDB.Constants.intNull
		Me.nBordereaux = eRemoteDB.Constants.intNull
		Me.sInter_pay = String.Empty
		Me.nNoteNum = eRemoteDB.Constants.intNull
		Me.nCashNum = eRemoteDB.Constants.intNull
		Me.nCompany = eRemoteDB.Constants.intNull
		Me.nTypesupport = eRemoteDB.Constants.intNull
		Me.nDocSupport = eRemoteDB.Constants.intNull
		Me.nTax_code = eRemoteDB.Constants.intNull
		Me.nTax_Percent = eRemoteDB.Constants.intNull
		Me.nTax_Amount = eRemoteDB.Constants.intNull
		Me.nAfect = eRemoteDB.Constants.intNull
		Me.nExcent = eRemoteDB.Constants.intNull
		Me.nCurrencyPay = eRemoteDB.Constants.intNull
		Me.nCurrencyOri = eRemoteDB.Constants.intNull
		Me.nAmountPay = eRemoteDB.Constants.intNull
		Me.nOfficePay = eRemoteDB.Constants.intNull
		Me.nInsur_area = eRemoteDB.Constants.intNull
		Me.nTaxCode = eRemoteDB.Constants.intNull
		Me.nAmount_Local = eRemoteDB.Constants.intNull
		Me.nBranch = eRemoteDB.Constants.intNull
		Me.nProduct = eRemoteDB.Constants.intNull
		Me.nPolicy = eRemoteDB.Constants.intNull
		Me.nOffice = eRemoteDB.Constants.intNull
		
		'**- Auxiliary Variables
		'- Variables auxiliares
		
		Me.nAcc_type = eRemoteDB.Constants.intNull
		Me.sAcco_num = String.Empty
		Me.nBank_code = eRemoteDB.Constants.intNull
		Me.nBk_agency = eRemoteDB.Constants.intNull
		Me.sN_Aba = String.Empty
		Me.sBenef_name = String.Empty
		Me.sInter_name = String.Empty
		Me.sUser_name = String.Empty
		Me.sBank_name = String.Empty
		Me.nBank_curr = eRemoteDB.Constants.intNull
		Me.sAcc_number = String.Empty
		Me.nStatusPay = eRemoteDB.Constants.intNull
		Me.nClieop = eRemoteDB.Constants.intNull
		Me.nCount_emited = eRemoteDB.Constants.intNull
		Me.nCount_canceled = eRemoteDB.Constants.intNull
		Me.nCount_pend = eRemoteDB.Constants.intNull
		Me.nCount_numered = eRemoteDB.Constants.intNull
		Me.nPay_Order = eRemoteDB.Constants.intNull
		Me.nCurrency = eRemoteDB.Constants.intNull
		Me.nOffice = eRemoteDB.Constants.intNull
		Me.dRescuedate = dtmNull
		Me.sClient_Digit = String.Empty
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'@@@@@@@@@@@@@@@@@@@@ FUNCIONES DE VALIDACIÓN Y EJECUCIÓN (VAL Y POST) @@@@@@@@@@@@@@@@@@@@
	
	'**%insValOP009_K: Makes the validation of the field to be actualized in the window OP009
	'** (Checks control)(Header)
	'% insValOP009_K: Realiza la validación de los campos a actualizar en la ventana OP009.
	'  (Control de Cheques)(Header)
    Public Function insValOP009_K(ByVal sCodispl As String, Optional ByVal dStartDate As Date = #12:00:00 AM#, Optional ByVal dEndDate As Date = #12:00:00 AM#, _
                                  Optional ByVal sChequeStat As String = "", Optional ByVal nConcept As Integer = 0, Optional ByVal sClient As String = "") As String
        Dim lclsErrors As eFunctions.Errors
        Dim lcolCheques As eCashBank.Cheques
        Dim lclsClient As eClient.Client
        Dim lintSta_cheque As Integer


        On Error GoTo insValOP009_K_Err

        lclsErrors = New eFunctions.Errors
        lcolCheques = New eCashBank.Cheques
        lclsClient = New eClient.Client

        '**+ Validation of the field "Date"
        '+ Validación del campo "Fecha de Inicio"

        If dStartDate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 3237)
        End If

        '+Si se indicó Beneficiario debe existir en el archivo de clientes
        If sClient <> strNull Then
            If Not lclsClient.Find(sClient) Then
                Call lclsErrors.ErrorMessage(sCodispl, 8067)
            End If
        End If

        If sChequeStat = "1" Then
            lintSta_cheque = 2
        ElseIf sChequeStat = "2" Then
            lintSta_cheque = 3
        End If

        '**+ Validation when ther is information to be shown
        '+ Validación de si existe información que mostrar

        If Not lcolCheques.Find(dStartDate, dEndDate, lintSta_cheque, nConcept, sClient) Then
            Call lclsErrors.ErrorMessage(sCodispl, 1073)
        End If

        insValOP009_K = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lcolCheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolCheques = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing

insValOP009_K_Err:
        If Err.Number Then
            insValOP009_K = insValOP009_K & Err.Description
        End If
        On Error GoTo 0
    End Function
	
	'**% insPostOP009_K: validates all the introduced data in the form (Header part)
	'% insPostOP009_K: Valida todos los datos introducidos en la forma (parte Header)
	Public Function insPostOP009_K() As Boolean
		
		insPostOP009_K = True
	End Function
	
	'**%insValOP009: Makes the validation of the fields to be updated in the window OP009
	'** (Check control)(Folder)
	'% insValOP009: Realiza la validación de los campos a actualizar en la ventana OP009.
	'  (Control de Cheques)(Folder)
	Public Function insValOP009() As String
		insValOP009 = String.Empty
	End Function
	
	'**% insPostOP009: validates all the introduced data in the form (Folder part)
	'% insPostOP009: Valida todos los datos introducidos en la forma (parte Folder)
    Public Function insPostOP009(Optional ByVal nAction As Integer = 0, Optional ByVal sOptChequeStat As String = "", Optional ByVal nRequest_nu As Double = 0, _
                                 Optional ByVal sCheque As String = "", Optional ByVal nConsec As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
        Dim lclsCheque As eCashBank.Cheque
        Dim lintSta_cheque As Integer

        On Error GoTo insPostOP009_Err
        lclsCheque = New eCashBank.Cheque
        If nAction = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
            '**+ Goes over the arrengement of the clients associated to the policy
            '+ Se recorre el arreglo de los clientes asociados a la póliza
            If sOptChequeStat = "1" Then
                lintSta_cheque = 3
            ElseIf sOptChequeStat = "2" Then
                lintSta_cheque = 4
            End If

            With lclsCheque
                .nRequest_nu = nRequest_nu
                .sCheque = sCheque
                .nConsec = nConsec
                .nNullcode = eRemoteDB.Constants.intNull
                .dNulldate = dtmNull
                .nSta_cheque = lintSta_cheque
                .nUsercode = nUsercode

                If Not .UpdChequeStat Then
                    insPostOP009 = False
                Else
                    insPostOP009 = True
                End If
            End With
        Else
            insPostOP009 = True
        End If

        'UPGRADE_NOTE: Object lclsCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCheque = Nothing

insPostOP009_Err:
        If Err.Number Then
            insPostOP009 = False
        End If
        On Error GoTo 0
    End Function
	
	'%insValOP006: Este método se encarga de validar los datos introducidos en la OP006
    Public Function insValOP006(ByVal nAction As Integer, ByVal sCodispl As String, ByVal nPayOrderTyp As Integer, ByVal nOffice As Integer, _
                                ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, Optional ByVal dReqDate As Date = #12:00:00 AM#, _
                                Optional ByVal nCurrency As Integer = 0, Optional ByVal sChequeNum As String = "", Optional ByVal nRequestNum As Double = 0, _
                                Optional ByVal nAccountNum As Integer = 0, Optional ByVal nConcept As Integer = 0, Optional ByVal nConceptEnabled As Boolean = False, _
                                Optional ByVal sDescript As String = "", Optional ByVal nAmount As Double = 0, Optional ByVal sBenefClient As String = "", _
                                Optional ByVal sIntermClient As String = "", Optional ByVal dChequeDate As Date = #12:00:00 AM#, _
                                Optional ByVal dAccDate As Date = #12:00:00 AM#, Optional ByVal nReqUser As Integer = 0, _
                                Optional ByVal nUsercode As Integer = 0, Optional ByVal nCompany As Integer = 0, Optional ByVal nCurrencyPay As Integer = 0, _
                                Optional ByVal nAmountPay As Double = 0, Optional ByVal nTypesupport As Integer = 0, Optional ByVal nDocSupport As Double = 0, _
                                Optional ByVal nTax_code As Integer = 0, Optional ByVal nTax_Percent As Double = 0, Optional ByVal nTax_Amount As Double = 0, _
                                Optional ByVal nAfect As Double = 0, Optional ByVal nExcent As Double = 0, Optional ByVal nBranch As Integer = 0, _
                                Optional ByVal nBranch_Led As Integer = 0, Optional ByVal nAcc_bank As Integer = 0, Optional ByVal nProponum As Double = 0, _
                                Optional ByVal sAccountHolder As String = "", Optional ByVal nbankExt As Integer = 0, Optional ByVal nAcc_Type As Integer = 0, _
                                Optional ByVal sBankAccount As String = "") As String

        Dim lclsErrors As eFunctions.Errors
        Dim lcliTime As eClient.Client
        Dim lclsLedge As eLedge.Led_compan
        Dim lclsOpt_system As eGeneral.Opt_system
        Dim lclsUser As eSecurity.User
        Dim lclsCash_acc As Cash_acc
        Dim lclsUser_cashnum As User_cashnum
        Dim lclsProvider As Object
        Dim lblnAccValid As Boolean
        Dim ldtmDateInitLed As Date
        Dim lintCashNum As Integer
        Dim lclspay_ord_concepts As pay_ord_concepts
        Dim lclsMove_Acc As Move_Acc
        Dim lclsPolicy As Object
        Dim lclsCertificat As Object
        Dim lclsCurr_acc As Curr_acc

        On Error GoTo insValOP006_Err
        lblnAccValid = True
        lclsErrors = New eFunctions.Errors
        lcliTime = New eClient.Client
        lclsLedge = New eLedge.Led_compan
        lclsCash_acc = New Cash_acc
        lclsOpt_system = New eGeneral.Opt_system
        lclsUser = New eSecurity.User
        lclspay_ord_concepts = New pay_ord_concepts
        lclsMove_Acc = New Move_Acc
        lclsProvider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Tab_Provider")
        lclsCurr_acc = New Curr_acc
        lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
        lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")

        If sCodispl = "OP06-1" And nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
            If (nRequestNum = 0 Or nRequestNum = eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 7044)
            Else
                If Not FindByPayOrder(nRequestNum, sChequeNum, eRemoteDB.Constants.intNull) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7045)
                End If
            End If
        Else
            If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                '**+ Validation of the pay order type
                '+ Validacion del tipo de orden de pago
                If nPayOrderTyp = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 9104)
                End If

                '**+ Validation of the application's date
                '+ Validacion de la fecha de la solicitud
                If dReqDate = dtmNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7043)
                End If

                '+Validación del campo Sucursal
                If nOffice <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 9120)
                End If

                '+Validación del campo Oficina
                If nOfficeAgen <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 55519)
                End If

                '+Validación del campo Agencia
                If nAgency <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1080)
                End If

                '+Validación del campo Moneda de Pago
                If nCurrencyPay <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 55547)
                End If

                '**+ Validation of the pay order type (if it is cash)
                '+ Validacion del tipo de orden de pago(Si es efectivo)
                If nPayOrderTyp = 1 Then
                    '**+ If a brach office exists and the currency was selected, you verifies if the currency is associated to cash.
                    '+ Si existe sucursal y la moneda fue seleccionada, se verifica que este asociada la moneda a efectivo.
                    If nCurrencyPay <> eRemoteDB.Constants.intNull And nOffice <> eRemoteDB.Constants.intNull Then
                        If sCodispl = "OP06-1" Then
                            If Not lclsCash_acc.Find(9998, nOffice, nCurrencyPay, lintCashNum) Then
                                Call lclsErrors.ErrorMessage(sCodispl, 7137)
                            End If
                        End If
                    End If
                End If

                '**+ Validation of the check number
                '+ Validacion del Numero de cheque
                If nPayOrderTyp = 3 Then
                    If sChequeNum = strNull Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7040)
                        '                Else
                        '                    If nAction = clngActionadd Then
                        '                        If FindByPayOrder(NumNull, sChequeNum, NumNull) Then
                        '                            Call lclsErrors.ErrorMessage(sCodispl, 7052)
                        '                        End If
                        '                    End If
                    End If
                    If nAcc_bank = eRemoteDB.Constants.intNull Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7002)
                    End If
                End If

                '**+ Validation of Bank transfer
                '+ Validacion de la transaferencia bancaria
                If nPayOrderTyp = 5 Then
                    If sAccountHolder = strNull Then
                        Call lclsErrors.ErrorMessage(sCodispl, 90000023)
                    End If

                    If nbankExt <= 0 Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7004)
                    End If

                    If sBankAccount = strNull Then
                        Call lclsErrors.ErrorMessage(sCodispl, 3058)
                    End If

                    If nAcc_Type <= 0 Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7030)
                    End If

                End If

                '+ Validación de la Compañía
                If nCompany = eRemoteDB.Constants.intNull Or nCompany = 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1046)
                End If


                '**+ Validation fo the pay concept
                '+ Validacion de concepto de pago
                If nConcept = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7046)

                Else

                    '**+ If the logic code of the window to be validated is Pay Orders (from main menu), you valid that the concept
                    '**+ is found in the manual concepts
                    '+ Si el código lógico de la ventana a validar es Ordenes de pago (La del menu principal), se valida que el concepto
                    '+se encuentre dentro de los conceptos manuales

                    If UCase(sCodispl) = "OP006" Then
                        If nConcept < 13 Then
                            Call lclsErrors.ErrorMessage(sCodispl, 7061)
                        End If
                    End If

                    '+ Si el concepto no existe para la compañia en tratamiento

                    If Not lclspay_ord_concepts.Find(nCompany, nConcept) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 55888)
                    End If

                    '+ Si el concepto corresponde a "Gastos de Suscripción se deben indicar los campos "Ramo Comercial" y "Ramo Contable".
                    If nConcept = 20 Then
                        If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
                            Call lclsErrors.ErrorMessage(sCodispl, 9064)
                        End If
                        If nBranch_Led = eRemoteDB.Constants.intNull Or nBranch_Led = 0 Then
                            Call lclsErrors.ErrorMessage(sCodispl, 11309)
                        End If
                    End If

                End If

                '**+ alidation of the description
                '+ Validacion de la descripcion
                If sDescript = strNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7048)
                End If

                '**+ Validation of the amount
                '+ Validacion del monto
                If nAmount = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7051)
                Else
                    If nAmountPay = eRemoteDB.Constants.intNull Or nAmountPay = 0 Then
                        Call lclsErrors.ErrorMessage(sCodispl, 11237)
                    End If
                End If


                '**+ Validation of the beneficiary
                '+ Validacion del Beneficiario
                If sBenefClient = strNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7049)
                Else
                    '**+ Validate that the client exists in the data base (in the end)
                    '+ Se valida que el cliente exista en la base de datos (al finalizar)
                    If Not lcliTime.Find(sBenefClient) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7050)
                    End If

                    '+ Si el concepto corresponde a "Gastos de Suscripción el beneficiario debe existir en la tabla de proveedores.
                    If nConcept = 20 Then
                        If Not lclsProvider.FindClient(sBenefClient, 0) Then
                            Call lclsErrors.ErrorMessage(sCodispl, 4315)
                        End If
                    End If
                End If

                '**+ Validation of the issue date
                '+ Validacion de la fecha del emision
                '+        If nPayOrderTyp <> 1 Then
                If dChequeDate = dtmNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7053)
                Else
                    If nAction = eFunctions.Menues.TypeActions.clngActionadd And nPayOrderTyp = 2 Then
                        If dChequeDate < Today Then
                            Call lclsErrors.ErrorMessage(sCodispl, 7054)
                        End If
                    End If
                End If
                '+        End If

                '**+ Valiation of the bookkeeping movement date
                '+ Validacion de la fecha de contabilizacion del movimiento
                ldtmDateInitLed = System.DateTime.FromOADate(0)
                If lclsOpt_system.find Then
                    If lclsLedge.Find(lclsOpt_system.nCompany) Then
                        ldtmDateInitLed = lclsLedge.dIniLedDat
                    End If
                End If

                '**+ If it is empty, assume the issue date
                '+ Si está vacía, se asume la fecha de emisión
                '            dAccDate = IIf(dAccDate = dtmNull And dChequeDate <> dtmNull, dChequeDate, dAccDate)

                If dAccDate = dtmNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7056)
                Else
                    If dAccDate < ldtmDateInitLed Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7057)
                    End If
                End If

                '**+ Validation of the user that is making the application
                '+ Validacion del usaurio que hace la solicitud
                If nReqUser = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7058)
                Else
                    If Not lclsUser.Find(nReqUser) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7059)
                    End If
                End If


                '+ Validación de la moneda Origen
                If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 55547)
                End If

                '+ Validación de la Número de Documento
                '+ Debe estar lleno si el tipo de documento es diferente a "No tiene"
                If nTypesupport <> 4 And nTypesupport <> 0 Then
                    If nDocSupport = eRemoteDB.Constants.intNull Or nDocSupport = 0 Then
                        Call lclsErrors.ErrorMessage(sCodispl, 3379)
                    End If
                End If

            ElseIf nAction = eFunctions.Menues.TypeActions.clngActionQuery Then

                '**+ Validation of the Application number
                '+ Validacion del Numero de solicitud
                If nRequestNum = eRemoteDB.Constants.intNull And nPayOrderTyp <> 3 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7044)
                End If

                '**+ Validation of the Check number
                '+ Validacion del Numero de cheque
                If nPayOrderTyp = 3 Then
                    If sChequeNum = strNull Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7040)
                    Else
                        If Not FindByPayOrder(eRemoteDB.Constants.intNull, sChequeNum, eRemoteDB.Constants.intNull) Then
                            Call lclsErrors.ErrorMessage(sCodispl, 7045)
                        End If
                    End If
                End If
            End If
        End If
        If sCodispl = "OP06-1" And (nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd) And nConcept = 19 Then
            '+ Si el concepto corresponde a "Rechazo de propuesta".
            If nProponum <= 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 55677)
            Else
                If lclsPolicy.FindPolicybyPolicy("1", nProponum) Then
                    If lclsCertificat.Find("1", lclsPolicy.nBranch, lclsPolicy.nProduct, nProponum, 0, True) Then
                        If lclsCertificat.nstatquota <> 3 And lclsCertificat.nstatquota <> 4 Then
                            Call lclsErrors.ErrorMessage(sCodispl, 56190)
                        Else
                            Call lclsMove_Acc.Find_nProponum_o(lclsPolicy.nBranch, lclsPolicy.nProduct, nProponum)
                            Call lclsCurr_acc.FindClientCurr_acc(lclsMove_Acc.nTyp_acco, lclsMove_Acc.sType_acc, lclsMove_Acc.sClient, lclsMove_Acc.nCurrency, True)
                            If lclsCurr_acc.nBalance <= 0 Then
                                Call lclsErrors.ErrorMessage(sCodispl, 56189)
                            End If
                            If lclsCertificat.srefundprem <> "1" Then
                                Call lclsErrors.ErrorMessage(sCodispl, 55145)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        insValOP006 = lclsErrors.Confirm

insValOP006_Err:
        If Err.Number Then
            insValOP006 = insValOP006 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lcliTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcliTime = Nothing
        'UPGRADE_NOTE: Object lclsLedge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLedge = Nothing
        'UPGRADE_NOTE: Object lclsCash_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCash_acc = Nothing
        'UPGRADE_NOTE: Object lclsOpt_system may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsOpt_system = Nothing
        'UPGRADE_NOTE: Object lclsUser may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsUser = Nothing
        'UPGRADE_NOTE: Object lclsProvider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProvider = Nothing
        'UPGRADE_NOTE: Object lclspay_ord_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclspay_ord_concepts = Nothing
        'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsMove_Acc = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsCurr_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCurr_acc = Nothing

    End Function
	
	'%insPreOP006: Función que asigna los valores a los campos de la ventana OP006
    Public Function insPreOP006(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nPayOrderTyp As Integer, ByVal nCurrency As Integer, ByVal sClient As String, _
                                ByVal nConcept As String, ByVal sDescript As String, ByVal dChequeDate As Date, ByVal nAmount As Double, ByVal nRequest_nu As Double) As Boolean
        mstrCodispl = sCodispl
        mintAction = nAction
        mintPayOrderTyp = nPayOrderTyp
        mintCurrency = nCurrency
        mstrClient = sClient
        mintConcept = CInt(nConcept)
        mstrDescript = sDescript
        mdtmChequeDate = dChequeDate
        mdblAmount = nAmount
        mdblRequest_nu = nRequest_nu
    End Function

    '**% DefaultValueOP006. This function is in charge of making the fields in the window OP006 able or desable.
    '%DefaultValueOP006. Esta función se encarga de realizar la habilitación o des-habilitación de los
    '%campos de la ventana OP006.
    Public Function DefaultValueOP006(ByVal sField As String, Optional ByVal nUsercode As Integer = 0) As Object
        Dim lvarReturnValue As Object
        Dim lclsNumerator As eGeneral.GeneralFunction

        Select Case sField
            '**+ Bank account number
            '+ Número de la cuenta bancaria
            Case "valAccountNum"
                Select Case mstrCodispl
                    Case "OP06-4"
                        lvarReturnValue = 9998
                    Case Else
                        lvarReturnValue = String.Empty
                End Select
                '**+ Type of order
                '+ Tipo de orden
            Case "cbePayOrderTyp"
                Select Case mstrCodispl
                    Case "OP06-2"
                        lvarReturnValue = 2
                    Case "OP06-3"
                        lvarReturnValue = 3
                    Case "OP06-4"
                        lvarReturnValue = 1
                    Case "OP06-6"
                        lvarReturnValue = 4
                    Case Else
                        lvarReturnValue = mintPayOrderTyp
                End Select

                '**+ Currency
                '+ Moneda
            Case "cbeCurrency"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4", "OP06-6"
                        lvarReturnValue = mintCurrency
                    Case Else
                        lvarReturnValue = String.Empty
                End Select

                '**+ Beneficiary, intermediary
                '+ Beneficiario, intermediario
            Case "dtcBenef", "dtcInterm", "dtcAccountHolder"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4", "OP06-5", "OP06-6"
                        lvarReturnValue = mstrClient
                    Case Else
                        lvarReturnValue = String.Empty
                End Select

                '**+ Concept
                '+ Concepto
            Case "cbeConcept"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4", "OP06-5", "OP06-6"
                        lvarReturnValue = mintConcept
                    Case Else
                        lvarReturnValue = String.Empty
                End Select

                '**+ Description
                '+ Descripción
            Case "tctDescript"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4"
                        lvarReturnValue = String.Empty ' mobjValues.getMessage(Session("OP006_nConcept"),"table293")
                    Case Else
                        lvarReturnValue = String.Empty
                End Select

                '**+ Check date
                '+ Fecha del cheque
            Case "tcdChequeDate"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4", "OP06-5", "OP06-6"
                        lvarReturnValue = mdtmChequeDate
                    Case Else
                        lvarReturnValue = String.Empty
                End Select

                '**+ Amount of the transaction
                '+ Monto de la transacción
            Case "tcnAmount"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4", "OP06-6"
                        lvarReturnValue = mdblAmount
                    Case Else
                        lvarReturnValue = 0
                End Select

                '**+ Application number
                '+ Número de la solicitud
            Case "tcnRequestNu"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-4", "OP06-3", "OP06-5", "OP06-6"
                        lclsNumerator = New eGeneral.GeneralFunction
                        lvarReturnValue = lclsNumerator.Find_Numerator(10, 0, nUsercode,  ,  ,  ,  ,  ,  ,  , "0", 0)
                        'UPGRADE_NOTE: Object lclsNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsNumerator = Nothing
                    Case Else
                        lvarReturnValue = 0
                End Select

                '**+ Unable the fields
                '+Deshabilitación de los campos
            'Case "cbePayOrderTyp_disabled", "tcnAmount_disabled", "dtcBenef_disabled", "cbeConcept_disabled"
            Case "tcnAmount_disabled", "dtcBenef_disabled", "cbeConcept_disabled"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4", "OP06-5", "OP06-6"
                        lvarReturnValue = "true"
                    Case Else
                        lvarReturnValue = "false"
                End Select

            Case "cbePayOrderTyp_disabled"
                lvarReturnValue = "false"

            Case "tctChequeNum_disabled"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4", "OP06-6"
                        lvarReturnValue = IIf(mintPayOrderTyp = 3, "false", "true")
                    Case Else
                        lvarReturnValue = "true"
                End Select

            Case "valAccountNum_disabled"
                Select Case mstrCodispl
                    Case "OP06-2", "OP06-3", "OP06-4"
                        lvarReturnValue = IIf(mintPayOrderTyp = 1, "true", "false")
                    Case Else
                        lvarReturnValue = "true"
                End Select

            Case "tcdAccDate_disabled"
                lvarReturnValue = IIf(mstrCodispl = "OP06-1", "true", "false")

        End Select

        DefaultValueOP006 = lvarReturnValue

    End Function

    '**% insPostOP006: This function is in charge of regist/update all the introduced data in the form
    '%insPostOP006: Esta función se encaga de registrar/actualizar todos los datos introducidos en la OP06-1
    Public Function insPostOP006(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nCompany As Integer, _
                                 ByVal nConcept As Integer, ByVal sDescript As String, ByVal nCurrencyOri As Integer, ByVal nAmount As Double, ByVal nOffice As Integer, _
                                 ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nCurrencyPay As Integer, ByVal nAmountPay As Double, ByVal nTypesupport As Integer, _
                                 ByVal nDocSupport As Double, ByVal nTax_code As Integer, ByVal nTax_Percent As Double, ByVal nTax_Amount As Double, ByVal nAfect As Double, _
                                 ByVal nExcent As Double, ByVal sInter_pay As String, ByVal dDat_propos As Date, ByVal dLedger_dat As Date, ByVal nUser_sol As Integer, _
                                 ByVal sRequest_ty As String, ByVal dIssue_Dat As Date, ByVal nUsercode As Integer, Optional ByVal nBranch As Integer = 0, _
                                 Optional ByVal nBranch_Led As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, _
                                 Optional ByVal nAcc_bank As Integer = 0, Optional ByVal sCodispl_aux As String = "", Optional ByVal nAmounttotal As Double = 0, _
                                 Optional ByVal nProponum As Double = 0, Optional ByVal sAccountHolder As String = "", Optional ByVal nBankExt As Integer = 0, _
                                 Optional ByVal nAcc_Type As Integer = 0, Optional ByVal sBankAccount As String = "", Optional ByVal nExternal_Concept As Integer = 0) As Boolean

        Dim lclsMove_Acc As Move_Acc
        Dim nDevolution As Double

        On Error GoTo insPostOP006_Err
        If sCodispl = "OP06-1" Then 'And insPostOP006 Then
            Select Case nAction
                '**+ If the selected option is register
                '+Si la opción seleccionada es Registrar
                Case eFunctions.Menues.TypeActions.clngActionadd
                    insPostOP006 = insCreCheques(nRequest_nu, sCheque, nCompany, nConcept, sDescript, nCurrencyOri, nAmount, nOffice, nOfficeAgen, nAgency, nCurrencyPay, nAmountPay, nTypesupport, nDocSupport, nTax_code, nTax_Percent, nTax_Amount, nAfect, nExcent, sInter_pay, dDat_propos, dLedger_dat, nUser_sol, sRequest_ty, dIssue_Dat, nUsercode, nBranch, nBranch_Led, nProduct, nPolicy, "OP006", nAcc_bank, nAmounttotal, sAccountHolder, nBankExt, nAcc_Type, sBankAccount, nExternal_Concept)


                    '**+ If the selected option is Modify
                    '+Si la opción seleccionada es Modificar

                Case eFunctions.Menues.TypeActions.clngActionUpdate
                    insPostOP006 = insUpdCheques(nRequest_nu, sCheque, nCompany, nConcept, sDescript, nCurrencyOri, nAmount, nCurrencyPay, nAmountPay, nTypesupport, nDocSupport, nTax_code, nTax_Percent, nTax_Amount, nAfect, nExcent, sInter_pay, dDat_propos, dLedger_dat, nUser_sol, sRequest_ty, dIssue_Dat, nOffice, nOfficeAgen, nAgency, nUsercode, nBranch, nBranch_Led, nProduct, nPolicy, nAcc_bank, nAmounttotal, sAccountHolder, nBankExt, nAcc_Type, sBankAccount, nExternal_Concept)


            End Select
        End If

        If sCodispl_aux = "OP06-1" And nConcept = 19 Then
            lclsMove_Acc = New Move_Acc
            Call lclsMove_Acc.UpdMove_Acc_rev(nProponum, nUsercode, nDevolution)
            'UPGRADE_NOTE: Object lclsMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsMove_Acc = Nothing
        End If

insPostOP006_Err:
        If Err.Number Then
            insPostOP006 = False
        End If
        On Error GoTo 0

    End Function
	
	'**% insValOP008_K: Makes the validation of the fields to be updated in the window OP008.
	'** (Checks annulment/applications) (Header)
	'% insValOP008_K: Realiza la validación de los campos a actualizar en la ventana OP008.
	'  (Anulación de cheques/solicitudes)(Header)
    Public Function insValOP008_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nRequest_nu As Double = 0, Optional ByVal nOptNull As Integer = 0, _
                                  Optional ByVal sCheque As String = "", Optional ByVal nBordereaux As Double = 0) As String

        Dim lnConsec As Integer

        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValOP008_K_Err

        lclsErrors = New eFunctions.Errors

        If (nRequest_nu <= 0 And nOptNull = 1) Or (sCheque = String.Empty And nOptNull = 2) Or (nBordereaux <= 0 And nOptNull = 3) Then
            Call lclsErrors.ErrorMessage(sCodispl, 7063)
        Else
            If Not Me.FindByPayOrder(nRequest_nu, sCheque, lnConsec, nBordereaux) Then
                If nOptNull = 1 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7045)
                ElseIf nOptNull = 2 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7065)
                Else
                    Call lclsErrors.ErrorMessage(sCodispl, 2016)
                End If
            Else
                If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                    If Me.nSta_cheque = 5 Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7252)
                    Else
                        '**+ If it's about an application
                        '+ Si se trata de una solicitud
                        If nOptNull = 1 Then
                            FindCheqToPrint(nRequest_nu)
                            If Me.nCount_pend < 1 Then
                                Call lclsErrors.ErrorMessage(sCodispl, 7064)
                            End If
                        ElseIf nOptNull = 2 Then
                            FindCheqToPrint(nRequest_nu)
                            If Me.nCount_pend < 1 Then
                                Call lclsErrors.ErrorMessage(sCodispl, 7066)
                            End If
                        Else
                            '**+ If the status is "Delivered"
                            '+ Si el estado es "Entregado"
                            If Me.nSta_cheque = 4 Then
                                Call lclsErrors.ErrorMessage(sCodispl, 7066)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        insValOP008_K = lclsErrors.Confirm

insValOP008_K_Err:
        If Err.Number Then
            insValOP008_K = insValOP008_K & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function
	
	'**% insPostOP008_K: validates all the introduced data in the form (Header part)
	'% insPostOP008_K: Valida todos los datos introducidos en la forma (parte Header)
	Public Function insPostOP008_K(Optional ByVal nAction As Integer = 0) As Boolean
		insPostOP008_K = True
	End Function
	
	'**% insValOP008: Makes the validation of the fields to be updated in the window OP08.
	'** (Checks annulment/spplications) (Folder)
	'% insValOP008: Realiza la validación de los campos a actualizar en la ventana OP008.
	'  (Anulación de cheques/solicitudes)(Folder)
    Public Function insValOP008(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dNulldate As Date, ByVal nOptNull As Integer, ByVal nNullcode As Integer, _
                                ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nBordereaux As Double) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclsValField As eFunctions.valField

        On Error GoTo insValOP008_Err
        insValOP008 = ""
        If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
            lclsErrors = New eFunctions.Errors
            lclsValField = New eFunctions.valField

            '**+ Validation fo the field of the application
            '+ Validación de la fecha de la solicitud

            If dNulldate = dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 7068)
            Else
                If dNulldate > Today Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7027)
                End If
                If Me.FindByPayOrder(nRequest_nu, sCheque, 0, nBordereaux) Then
                    If nOptNull = 1 Or nOptNull = 3 Then
                        If Me.dDat_propos > dNulldate Then
                            Call lclsErrors.ErrorMessage(sCodispl, 7070)
                        End If
                    Else
                        If Me.dIssue_Dat > dNulldate Then
                            Call lclsErrors.ErrorMessage(sCodispl, 7071)
                        End If
                    End If
                End If
            End If

            '**+ Validation of the Annulment of the application or check Cause
            '+ Validación de la Causa de anulación de la solicitud o cheque

            If nNullcode = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 7067)
            End If

            insValOP008 = lclsErrors.Confirm

            'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsValField = Nothing
            'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsErrors = Nothing
        End If
insValOP008_Err:
        If Err.Number Then
            insValOP008 = insValOP008 & Err.Description
        End If
        On Error GoTo 0
    End Function
	
	'% insPostOP008: Invoca los métodos para las actualizaciones hechas en la ventana OP008
	Public Function insPostOP008(ByVal nAction As Integer, ByVal nNullcode As Integer, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nBordereaux As Double) As Boolean
		
		On Error GoTo insPostOP008_Err
		
		insPostOP008 = True
		
		'**+ This assignment is for using the incoming information in all
		'**+ the functions in insPostOP008, without having to pass it as a paameter
		'+ Esta asignación es para utilizar la información entrante en todas
		'+ las funciones llamadas dentro de insPostOP008, sin tener que pasarla como parámetro
		Select Case nAction
			'**+ If the selected option is Register
			'+ Si la opción seleccionada es Registrar
			Case eFunctions.Menues.TypeActions.clngActionadd
				If insUpdCheques_OP008(nRequest_nu, sCheque, nNullcode, dNulldate, nUsercode, nBordereaux) Then
					insPostOP008 = True
				Else
					insPostOP008 = False
				End If
		End Select
		
insPostOP008_Err: 
		If Err.Number Then
			insPostOP008 = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insValOP007_K: Makes the validation of the fields to be updated in the window OP007.
	'** (Fixed expenses check's applications) (Header)
	'% insValOP007_K: Realiza la validación de los campos a actualizar en la ventana OP007.
	'  (Solicitud de cheques para gastos fijos)(Header)
	Public Function insValOP007_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nRequest_nu As Double = 0) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValOP007_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		mlngRequest_nu = nRequest_nu
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
			If nRequest_nu = 0 Or nRequest_nu = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 7044)
			Else
				If Not insReaCheques_OP007 Then
					Call lclsErrors.ErrorMessage(sCodispl, 7045)
				End If
			End If
		End If
		insValOP007_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOP007_K_Err: 
		If Err.Number Then
			insValOP007_K = insValOP007_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insPostOP007_K: Validates all the introduced data in the form (Header part)
	'% insPostOP007_K: Valida todos los datos introducidos en la forma (parte Header)
	Public Function insPostOP007_K(ByVal nAction As Integer) As Boolean
		insPostOP007_K = True
	End Function
	
	'**% insValOP007: Makes the validation of the fields to be updated in the window OP007.
	'** (Fixed expenses check's application) (Folder)
	'% insValOP007: Realiza la validación de los campos a actualizar en la ventana OP007.
	'  (Solicitud de cheques para gastos fijos)(Folder)
    Public Function insValOP007(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal dDat_propos As Date = #12:00:00 AM#, Optional ByVal nAcc_bank As Integer = 0, _
                                Optional ByVal nConcept As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sClient As String = "", _
                                Optional ByVal sBenef_name As String = "", Optional ByVal sInter_pay As String = "", Optional ByVal nAmount As Double = 0, _
                                Optional ByVal nQ_pays As Integer = 0, Optional ByVal nPay_freq As Integer = 0, Optional ByVal dIssue_Dat As Date = #12:00:00 AM#, _
                                Optional ByVal dLedger_dat As Date = #12:00:00 AM#, Optional ByVal nUser_sol As Integer = 0, Optional ByVal nCompany As Integer = 0) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclsBank_acc As Bank_acc
        Dim lclsvalClient As eClient.ValClient
        Dim lclsClient As eClient.Client
        Dim lstrInter_name As String = ""
        Dim lblnIssueDateValid As Boolean
        Dim lblnLedDateValid As Boolean

        On Error GoTo insValOP007_Err

        lclsErrors = New eFunctions.Errors
        lclsBank_acc = New Bank_acc
        lclsvalClient = New eClient.ValClient
        lclsClient = New eClient.Client

        '**+ Validation of the application date
        '+ Validación de la fecha de la solicitud

        If dDat_propos = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 7043)
        End If

        '**+ validation of the bank account code
        '+ Validación del código de la cuenta bancaria

        If nAcc_bank = 0 Or nAcc_bank = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 7002)
        Else

            '**+ Validation of the bank account existence
            '+ Validación de existencia de la cuenta bancaria

            If Not lclsBank_acc.Find_O(nAcc_bank) Then
                Call lclsErrors.ErrorMessage(sCodispl, 7013)
            End If
        End If

        '+ Validación de la Compañía
        If nCompany = eRemoteDB.Constants.intNull Or nCompany = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1046)
        End If

        '**+ Validation of the payment concept
        '+ Validación de concepto de pago

        If nConcept = 0 Or nConcept = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 7046)
        Else
            If nConcept < 13 Then
                Call lclsErrors.ErrorMessage(sCodispl, 7061)
            End If
        End If

        '**+ Validation of the description
        '+ Validación de la descripción

        If sDescript = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 7048)
        End If

        '**+ Validation of the beneficiary
        '+ Validación del Beneficiario

        If sClient = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 7049)
        Else
            If Len(RTrim(sClient)) = 14 Then
                If Not lclsvalClient.Validate(sClient, nAction) Then
                    If lclsvalClient.Status = eClient.ValClient.eTypeValClientErr.StructInvalid Or lclsvalClient.Status = eClient.ValClient.eTypeValClientErr.TypeNotFound Then
                        Call lclsErrors.ErrorMessage(sCodispl, 2012)
                    End If

                    If lclsvalClient.Status = eClient.ValClient.eTypeValClientErr.FieldEmpty Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7049)
                    End If
                Else
                    If lclsClient.Find(sClient) Then
                        sBenef_name = lclsClient.sCliename
                    End If

                    '**+ Validate that the client exists in the data base (at the end)
                    '+ Se valida que el cliente exista en la base de datos (al finalizar)

                    If sClient <> String.Empty And sBenef_name = String.Empty Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7050)
                    End If
                End If
            Else
                Call lclsErrors.ErrorMessage(sCodispl, 7050)
            End If
        End If

        '+ Validación del "Por medio de"

        If Not sInter_pay = String.Empty Then
            If Len(RTrim(sClient)) = 14 Then
                If Not lclsvalClient.Validate(sInter_pay, nAction) Then
                    If lclsvalClient.Status = eClient.ValClient.eTypeValClientErr.StructInvalid Or lclsvalClient.Status = eClient.ValClient.eTypeValClientErr.TypeNotFound Then
                        Call lclsErrors.ErrorMessage(sCodispl, 2012)
                    End If

                Else
                    '**+ Validate that the client already exists in the data base (at the end)
                    '+ Se valida que el cliente exista en la base de datos (al finalizar)

                    If lclsClient.Find(sInter_pay) Then
                        lstrInter_name = lclsClient.sCliename
                    End If

                    If sInter_pay <> String.Empty And lstrInter_name = String.Empty Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7050)
                    End If
                End If
            Else
                Call lclsErrors.ErrorMessage(sCodispl, 7050)
            End If

        End If

        '**+ Validation of the amount
        '+ Validación del monto

        If nAmount = 0 Or nAmount = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 7051)
        End If

        '**+ Validation of the amount of payments
        '+ Validación de la cantidad de pagos

        If nQ_pays = 0 Or nQ_pays = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 7072)
        ElseIf nQ_pays < 2 Then
            Call lclsErrors.ErrorMessage(sCodispl, 7073)
        End If

        '**+ Validation of the frecuency of payments
        '+ Validación de la frecuencia de pago

        If nPay_freq = eRemoteDB.Constants.intNull Or nPay_freq = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 7074)
        End If

        '**+ Validation of the issue date
        '+ Validación de la fecha del emisión

        If dIssue_Dat = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 7053)
            lblnIssueDateValid = False
        Else
            lblnIssueDateValid = True
            '**+ If its registered, it must not be previous the date of the day
            '+ Si se esta registrando, no debe ser anterior a la fecha del día

            If nAction = eFunctions.Menues.TypeActions.clngActionadd And dIssue_Dat > Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 7054)
            End If

            ''**+ If it is modified, it must not be previous the date of the day
            ''+ Si se esta modificando, no debe ser anterior a la fecha del día

            If nAction = eFunctions.Menues.TypeActions.clngActionUpdate And dIssue_Dat < Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 7055)
            End If
        End If

        '**+ Validation of the movement's bookkeeping date
        '+ Validación de la fecha de contabilización del movimiento

        If dLedger_dat = dtmNull Then
            lblnLedDateValid = False
            Call lclsErrors.ErrorMessage(sCodispl, 1087)
        Else
            lblnLedDateValid = True
        End If

        If lblnIssueDateValid And lblnLedDateValid Then
            If dIssue_Dat > dLedger_dat Then
                Call lclsErrors.ErrorMessage(sCodispl, 7210)
            End If
        End If

        '**+ Validation of the user that is makin the application
        '+ Validación del usuario que hace la solicitud

        If nUser_sol = 0 Or nUser_sol = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 7058)
        Else
            If Not insReaUsers(nUser_sol) Then
                Call lclsErrors.ErrorMessage(sCodispl, 7059)
            End If
        End If

        insValOP007 = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing
        'UPGRADE_NOTE: Object lclsvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsvalClient = Nothing
        'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBank_acc = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValOP007_Err:
        If Err.Number Then
            insValOP007 = insValOP007 & Err.Description
        End If
        On Error GoTo 0
    End Function
	
	'**+ insPostOP007: Validates all the introduced data in the form (Folder part)
	'% insPostOP007: Valida todos los datos introducidos en la forma (parte Folder)
    Public Function insPostOP007(ByVal nAction As Integer, Optional ByVal nRequest_nu As Double = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nConcept As Integer = 0, _
                                 Optional ByVal sClient As String = "", Optional ByVal dDat_propos As Date = #12:00:00 AM#, Optional ByVal sDescript As String = "", _
                                 Optional ByVal dIssue_Dat As Date = #12:00:00 AM#, Optional ByVal dLedger_dat As Date = #12:00:00 AM#, Optional ByVal sPay_freq As String = "", _
                                 Optional ByVal nQ_pays As Integer = 0, Optional ByVal nUser_sol As Integer = 0, Optional ByVal nUsercode As Integer = 0, _
                                 Optional ByVal nAcc_bank As Integer = 0, Optional ByVal sInter_pay As String = "", Optional ByVal nNoteNum As Integer = 0, _
                                 Optional ByVal nVoucher_le As Integer = 0, Optional ByVal nVoucher As Integer = 0, Optional ByVal nCompany As Integer = 0) As Boolean
        Dim lclsBank_account As Object

        lclsBank_account = New eCashBank.Bank_acc

        On Error GoTo insPostOP007_Err


        '**+ This assignment is for using the incoming information in all
        '**+ the functions in insPostOP007, without having to pass it as a parameter
        '+ Esta asignación es para utilizar la información entrante en todas
        '+ las funciones llamadas dentro de insPostOP007, sin tener que pasarla como parámetro
        mlngRequest_nu = nRequest_nu
        mdblAmount = nAmount
        mintConcept = nConcept
        mstrClient = sClient
        mdtmDat_propos = dDat_propos
        mstrDescript = sDescript
        mdtmIssue_dat = dIssue_Dat
        mdtmLedger_dat = dLedger_dat
        mstrPay_freq = sPay_freq
        mintQ_pays = nQ_pays
        mintUser_sol = nUser_sol
        mintUsercode = nUsercode
        mintAcc_bank = nAcc_bank
        mstrInter_pay = sInter_pay
        mlngNotenum = nNoteNum
        mIntCompany = nCompany
        nInsur_area = eRemoteDB.Constants.intNull
        nOffice = eRemoteDB.Constants.intNull

        '**+Se busca el codigo de la moneda asociada a la cuenta bancaria
        Call lclsBank_account.FindCurrency(nAcc_bank)

        mintCurrenAcc = lclsBank_account.nCurrency

        Select Case nAction

            '**+ If the selected option is Register
            '+ Si la opción seleccionada es Registrar

            Case eFunctions.Menues.TypeActions.clngActionadd
                insPostOP007 = insCreCheques_OP007(nVoucher, nVoucher_le)

                '**+ If the selected option is Modify
                '+ Si la opción seleccionada es Modificar

            Case eFunctions.Menues.TypeActions.clngActionUpdate
                insPostOP007 = insUpdCheques_OP007()
        End Select


insPostOP007_Err:
        If Err.Number Then
            insPostOP007 = False
        End If

        'UPGRADE_NOTE: Object lclsBank_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBank_account = Nothing

        On Error GoTo 0
    End Function
	
	'@@@@@@@@@@@@@@@ RUTINAS NECESARIAS PARA LA EJECUCIÓN DE @@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@ DE LAS FUNCIONES VAL Y POST             @@@@@@@@@@@@@@@
	
	'**% insUpdCheques_OP008: Updates the information in treat of the
	'**% main table for the transaction (for check's annulment/applications)
	'% insUpdCheques_OP008: Actualiza la información en tratamiento de la
	'% tabla principal para la transacción. (para Anulación de cheques/solicitudes)
    Private Function insUpdCheques_OP008(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nNullcode As Integer, ByVal dNulldate As Date, ByVal nUsercode As Integer, _
                                         ByVal nBordereaux As Double) As Boolean

        Dim lclsCheque As eCashBank.Cheque

        On Error GoTo insUpdCheques_OP008_Err

        lclsCheque = New eCashBank.Cheque

        insUpdCheques_OP008 = True
        With lclsCheque
            .nRequest_nu = nRequest_nu
            .sCheque = sCheque
            .nConsec = eRemoteDB.Constants.intNull
            .nNullcode = nNullcode
            .dNulldate = dNulldate
            .nSta_cheque = 5
            .nUsercode = nUsercode
            .nBordereaux = nBordereaux

            If Not .UpdChequeStat Then
                insUpdCheques_OP008 = False
            End If
        End With
        'UPGRADE_NOTE: Object lclsCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCheque = Nothing

insUpdCheques_OP008_Err:
        If Err.Number Then
            insUpdCheques_OP008 = False
        End If
        On Error GoTo 0
    End Function
	
	'**% insReaCheques_OP007: Read the information in treat of the main table for the transaction.
	'% insReaCheques_OP007: Lee la información en tratamiento de la tabla principal para la transacción.
	Private Function insReaCheques_OP007() As Boolean
		
		On Error GoTo insReaCheques_OP007_Err
		
		insReaCheques_OP007 = True
		
		If Not FindByPayOrder(mlngRequest_nu, String.Empty, eRemoteDB.Constants.intNull) Then
			insReaCheques_OP007 = False
		End If
		
insReaCheques_OP007_Err: 
		If Err.Number Then
			insReaCheques_OP007 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insReaUsers:
	Private Function insReaUsers(ByVal lintUsers As Integer) As Boolean
		
		Dim lclsUsers As eGeneral.Users
		
		On Error GoTo insReaUsers_Err
		
		lclsUsers = New eGeneral.Users
		
		insReaUsers = False
		
		If lclsUsers.FindUserName(lintUsers) <> String.Empty Then
			insReaUsers = True
		Else
			insReaUsers = False
		End If
		'UPGRADE_NOTE: Object lclsUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUsers = Nothing
		
insReaUsers_Err: 
		If Err.Number Then
			insReaUsers = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insUpdCheques: Updates the information in treat of the main table for the transaction
	'% insUpdCheques: Actualiza la información en tratamiento de la tabla principal para la transacción
	Private Function insUpdCheques_OP007() As Boolean
		On Error GoTo insUpdCheques_OP007_Err
		
		insUpdCheques_OP007 = True
		
		With Me
			Call .FindByPayOrder(mlngRequest_nu, strNull, eRemoteDB.Constants.intNull)
			.nRequest_nu = mlngRequest_nu
			.sCheque = " "
			.nConsec = 0
			.nAmount = mdblAmount
			.nConcept = mintConcept
			.sClient = mstrClient
			.dDat_propos = mdtmDat_propos
			.sDescript = mstrDescript
			.dIssue_Dat = mdtmIssue_dat
			.dLedger_dat = mdtmLedger_dat
			.sPay_freq = mstrPay_freq
			.nQ_pays = mintQ_pays
			.nUser_sol = mintUser_sol
			.nUsercode = mintUsercode
			.nAcc_bank = mintAcc_bank
			.sInter_pay = mstrInter_pay
			.nNoteNum = mlngNotenum
			insUpdCheques_OP007 = .Update
		End With
		
insUpdCheques_OP007_Err: 
		If Err.Number Then
			insUpdCheques_OP007 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insCreCheques_OP007: Adds the information in treat in the main table for the transaction.
	'% insCreCheques_OP007: Agrega la información en tratamiento de la tabla principal para la transacción
	Private Function insCreCheques_OP007(ByVal lngVoucher As Integer, ByVal intVoucher_le As Integer) As Boolean
		Dim lclsExchanges As Object
		Dim lclsUsers As Object
		
		On Error GoTo insCreCheques_OP007_Err
		
		lclsUsers = New eGeneral.Users
		lclsExchanges = New eGeneral.Exchange
		
		insCreCheques_OP007 = True
		
		If System.Math.Abs(lngVoucher) > 0 Then
			With Me
				.nRequest_nu = mlngRequest_nu
				.sCheque = " "
				.nConsec = 0
				.nAmount = mdblAmount
				.nConcept = mintConcept
				.sClient = mstrClient
				.nBranch_Led = eRemoteDB.Constants.intNull
				.nClaim = eRemoteDB.Constants.intNull
				.nVoucher_le = intVoucher_le
				.nVoucher = lngVoucher
				.dDat_propos = mdtmDat_propos
				.sDescript = mstrDescript
				.dIssue_Dat = mdtmIssue_dat
				.dLedger_dat = mdtmLedger_dat
				.nNullcode = eRemoteDB.Constants.intNull
				.dNulldate = dtmNull
				.sPay_freq = mstrPay_freq
				.nQ_pays = mintQ_pays
				.nReceipt = eRemoteDB.Constants.intNull
				.sRequest_ty = "2"
				.nSta_cheque = 1
				.dStat_date = mdtmDat_propos
				.nTransac = eRemoteDB.Constants.intNull
				.nUser_sol = mintUser_sol
				.nUsercode = mintUsercode
				.nYear_month = eRemoteDB.Constants.intNull
				.nAcc_bank = mintAcc_bank
				.nBordereaux = eRemoteDB.Constants.intNull
				.sInter_pay = mstrInter_pay
				.nAcc_type = 0
				.sAcco_num = ""
				.nBank_code = 0
				.nBk_agency = 0
				.sN_Aba = ""
				.nNoteNum = mlngNotenum
				.nCompany = mIntCompany
				.nInsur_area = eRemoteDB.Constants.intNull
				.nCurrencyOri = mintCurrenAcc
				'+ Se calcula la cantidad en moneda local nAmount_local
				Call lclsExchanges.Convert(eRemoteDB.Constants.intNull, mdblAmount, mintCurrenAcc, 1, Today, 0)
				.nAmount_Local = lclsExchanges.pdblResult
				'+ Se busca la sucursal asociada al usuario
				Call lclsUsers.Find(mintUser_sol)
				.nOfficePay = lclsUsers.nOffice
				insCreCheques_OP007 = .Add
			End With
		Else
			insCreCheques_OP007 = False
		End If
		
insCreCheques_OP007_Err: 
		If Err.Number Then
			insCreCheques_OP007 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExchanges may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExchanges = Nothing
		'UPGRADE_NOTE: Object lclsUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUsers = Nothing
	End Function
	
	'**% insValOPL020_K: Validates all the introduced data in the form OPL020_K
	'%insValOPL020_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma OPL020.
	Public Function insValOPL020_K(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal nAcc_bank As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsBank_account As eCashBank.Bank_acc
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValOPL020_K_Err
		
		'*+Validation of the field Initial Date is performed
		'+Se realiza la validacion del campo Fecha de Inicio
		If dInitDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		Else
			If dInitDate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 7161)
			End If
		End If
		
		'*+Validation of Final Date is performed
		'+Se valida la fecha final
		If Not dEndDate = dtmNull Then
			If dEndDate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 7161)
			End If
		End If
		
		'*+The Initial Date does not should be bigger than Final Date
		'+Se valida que la fecha inicial no sea mayor que la fecha final
		If Not dInitDate = dtmNull And Not dEndDate = dtmNull Then
			If dInitDate > dEndDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 4159)
			End If
		End If
		
		'*+Validation of the field Bank Account is performed
		'+Validacion del campo "Cuenta Bancaria"
		If nAcc_bank <> eRemoteDB.Constants.intNull And nAcc_bank <> 0 Then
			
			'*+ It validate that the Bank Account is neither cash nor check
			'+Se valida que la cuenta no sea ni efectivo ni cheque
			If nAcc_bank = 9998 Or nAcc_bank = 9999 Then
				Call lclsErrors.ErrorMessage(sCodispl, 7031)
			End If
			
			lclsBank_account = New eCashBank.Bank_acc
			If Not lclsBank_account.Find(nAcc_bank) Then
				Call lclsErrors.ErrorMessage(sCodispl, 7013)
			End If
			
		End If
		
		
		insValOPL020_K = lclsErrors.Confirm
		
insValOPL020_K_Err: 
		If Err.Number Then
			insValOPL020_K = "insValOPL020_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsBank_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBank_account = Nothing
		On Error GoTo 0
	End Function
	
	'insValOPL001_K: Valida los valores introducidos en el informe de cheque
	
	Public Function insValOPL001_K(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEndDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValOPL001_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		
		'**+ Validation of the field "Date"
		'+ Validación del campo "Fecha"
		If lclsValField.ValDate(dInitDate) Then
			If lclsValField.ValDate(dEndDate) Then
				If dEndDate < dInitDate Then
					Call lclsErrors.ErrorMessage(sCodispl, 7165)
				End If
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 7079)
			End If
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 7079)
		End If
		
		insValOPL001_K = lclsErrors.Confirm
		
insValOPL001_K_Err: 
		If Err.Number Then
			insValOPL001_K = "insValOPL001_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'insValOPL002_K: Valida los valores introducidos para el Listado de depósito
	Public Function insValOPL002_K(ByVal sCodispl As String, ByVal sDepositNum As String, ByVal nAccCash As Double) As String
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo insValOPL002_K_Err
		lclsErrors = New eFunctions.Errors
		
		'+El campo deposito debe estar lleno
		If sDepositNum = strNull Then
			insValOPL002_K = lclsErrors.ErrorMessage("OPL002", 7003)
		End If
		
		'+El campo cuenta debe estar lleno
		If nAccCash = intNull Or nAccCash = 0 Then
			insValOPL002_K = lclsErrors.ErrorMessage("OPL002", 7029)
		End If
		
		insValOPL002_K = lclsErrors.Confirm
		
insValOPL002_K_Err: 
		If Err.Number Then
			insValOPL002_K = "insValOPL002_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'%insReaCheques: Esta función se encarga de leer la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function insReaCheques(ByVal nRequest_nu As Double) As Boolean
		'**- Variable definition lrecCheques
		'- Se define la variable lrecCheques
		Dim lrecCheques As eRemoteDB.Execute
		
		
		'**+ Parameter definition for stored procedure 'insudb.reaChequesOP006'
		'+ Definición de parámetros para stored procedure 'insudb.reaChequesOP006'
		On Error GoTo insReaCheques_Err
		lrecCheques = New eRemoteDB.Execute
		With lrecCheques
			.StoredProcedure = "reaChequesOP006"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nBordereaux", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insReaCheques = True
			Else
				insReaCheques = False
			End If
		End With
		
insReaCheques_Err: 
		If Err.Number Then
			insReaCheques = False
		End If
		'UPGRADE_NOTE: Object lrecCheques may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCheques = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValOP714_K: Makes the validation of the fields to be updated in the window OP714_K.
	'% insValOP714_K: Realiza la validación de los campos a actualizar en la ventana OP714_K.
    Public Function insValOP714_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nCompany As Integer, ByVal dStartDate As Date, ByVal dEndDate As Date, _
                                  ByVal nAcc_Bank As Double) As String

        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValOP714_K_Err

        lclsErrors = New eFunctions.Errors

        If nAcc_Bank = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 7029)
        End If

        If dStartDate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 3237)
        End If

        If dEndDate <> dtmNull And dEndDate < dStartDate Then
            Call lclsErrors.ErrorMessage(sCodispl, 11425)
        End If

        insValOP714_K = lclsErrors.Confirm

insValOP714_K_Err:
        If Err.Number Then
            insValOP714_K = insValOP714_K & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function
	
	'**% insValOP714: Makes the validation of the fields to be updated in the window OP714.
	'% insValOP714: Realiza la validación de los campos a actualizar en la ventana OP714.
	Public Function insValOP714(ByVal sCodispl As String, ByVal lblnSwitch As Boolean) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValOP714_Err
		
		lclsErrors = New eFunctions.Errors
		
		If lblnSwitch = True Then
			Call lclsErrors.ErrorMessage(sCodispl, 750055)
		End If
		
		insValOP714 = lclsErrors.Confirm
		
insValOP714_Err: 
		If Err.Number Then
			insValOP714 = insValOP714 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostOP714: Invoca el SP que realiza las actualizaciones correspondientes a la ventana OP714
    Public Function insPostOP714(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nConsec As Integer, ByVal nUsercode As Integer, ByVal sKey As String, _
                                 Optional ByVal nAcc_Bank As Double = eRemoteDB.Constants.dblNull) As Boolean

        Dim lrecupdChequesOP714 As eRemoteDB.Execute
        lrecupdChequesOP714 = New eRemoteDB.Execute

        On Error GoTo insPostOP714_Err

        With lrecupdChequesOP714
            .StoredProcedure = "updChequesOP714"
            .Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSta_cheque", 8, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAcc_Bank", nAcc_Bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostOP714 = .Run(False)
        End With

insPostOP714_Err:
        If Err.Number Then
            insPostOP714 = False
        End If
        'UPGRADE_NOTE: Object lrecupdChequesOP714 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdChequesOP714 = Nothing
        On Error GoTo 0
    End Function
	
	'**% insValOP715_K: Makes the validation of the fields to be updated in the window OP715_K.
	'% insValOP715_K: Realiza la validación de los campos a actualizar en la ventana OP715_K.
	Public Function insValOP715_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nPayOrdBord As Integer, ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal dStartDate As Date, ByVal dEndDate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValOP715_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		If nAction = 401 Then
			'+Validación de número de relación
			If nPayOrdBord = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 705001)
			End If
		Else
			'+Validación de código de Compañia
			If nCompany = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 6012)
			End If
			'+Validación de código de Concepto
			If nConcept = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 7005)
			End If
			'+Validación de fecha de inicio
			If dStartDate = dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 3237)
			End If
			'+Validación de fecha fin
			If dEndDate <> dtmNull And dEndDate < dStartDate Then
				Call lclsErrors.ErrorMessage(sCodispl, 11425)
			End If
		End If
		
		insValOP715_K = lclsErrors.Confirm
		
insValOP715_K_Err: 
		If Err.Number Then
			insValOP715_K = insValOP715_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValOP715: Makes the validation of the fields to be updated in the window OP715.
	'% insValOP715: Realiza la validación de los campos a actualizar en la ventana OP715.
	Public Function insValOP715(ByVal sCodispl As String, ByVal lblnSwitch As Boolean) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValOP715_Err
		
		lclsErrors = New eFunctions.Errors
		
		If lblnSwitch = True Then
			Call lclsErrors.ErrorMessage(sCodispl, 750055)
		End If
		
		insValOP715 = lclsErrors.Confirm
		
insValOP715_Err: 
		If Err.Number Then
			insValOP715 = insValOP715 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostOP715: Invoca el SP que realiza las actualizaciones correspondientes a la ventana OP715
	Public Function insPostOP715(ByVal nPayOrdBord As Integer, ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nConsec As Integer, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecupdChequesOP715 As eRemoteDB.Execute
		lrecupdChequesOP715 = New eRemoteDB.Execute
		
		On Error GoTo insPostOP715_Err
		
		With lrecupdChequesOP715
			.StoredProcedure = "updChequesOP715"
			.Parameters.Add("nPayOrdBord", nPayOrdBord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostOP715 = .Run(False)
		End With
		
insPostOP715_Err: 
		If Err.Number Then
			insPostOP715 = False
		End If
		'UPGRADE_NOTE: Object lrecupdChequesOP715 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdChequesOP715 = Nothing
		On Error GoTo 0
	End Function
	'**% insValOP716_K: Makes the validation of the fields to be updated in the window OP716_K.
	'% insValOP716_K: Realiza la validación de los campos a actualizar en la ventana OP716_K.
    Public Function insValOP716_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dStartDate As Date, _
                                  ByVal dEndDate As Date, ByVal sIndExtensionSTS As String) As String

        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValOP716_K_Err

        lclsErrors = New eFunctions.Errors

        If sIndExtensionSTS.ToUpper.Trim <> "YES" Then
            Call lclsErrors.ErrorMessage(sCodispl, 90000046)
        End If

        If dStartDate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 3237)
        End If

        If dEndDate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1097)
        End If

        If dEndDate <> dtmNull And dEndDate < dStartDate Then
            Call lclsErrors.ErrorMessage(sCodispl, 11425)
        End If

        insValOP716_K = lclsErrors.Confirm

insValOP716_K_Err:
        If Err.Number Then
            insValOP716_K = insValOP716_K & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function
	
	Public Function FindBynrequest_nu(ByVal nRequest_nu As Double) As Boolean
		
		Dim lrecreanrequest_nu As eRemoteDB.Execute
		lrecreanrequest_nu = New eRemoteDB.Execute
		
		On Error GoTo FindBynrequest_nu_Err
		
		'**+ Parameter definition for stored procedure 'rea_cheques_nrequest_nu'
		'+ Definición de parámetros para stored procedure 'rea_cheques_nrequest_nu'
		
		With lrecreanrequest_nu
			.StoredProcedure = "rea_cheques_nrequest_nu"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValida", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("sValida").Value = 1 Then
					FindBynrequest_nu = True
				Else
					FindBynrequest_nu = False
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreanrequest_nu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreanrequest_nu = Nothing
		
FindBynrequest_nu_Err: 
		If Err.Number Then
			FindBynrequest_nu = False
		End If
		On Error GoTo 0
    End Function

    Public Sub insNotificationNewCheque(ByVal EmpEmpresa As String, ByVal Origen As String, ByVal SegnDireccion As String, _
                                        ByVal SegnRutDestinatario As String, ByVal SegnDvDestinatario As String, ByVal SegnNombre As String, _
                                        ByVal SegnRutSolicitante As String, ByVal SegnFechaIngreso As String, ByVal sRequest_Type As String, _
                                        ByVal nBank_Code As Integer, ByVal SegnNumeroCuenta As String, ByVal nAcc_Type As Integer, _
                                        ByVal nOffice As Integer, ByVal SegnTotal As Decimal, ByVal nCurrencyPay As Integer, ByVal nConcept As Integer, _
                                        ByVal SegnObservacion As String, ByVal nExternalConcept As Integer, Optional ByVal SegnId As Decimal = 0, Optional ByVal nUsercode As Integer = 0, _
                                        Optional ByVal RequestNu As Integer = 0, Optional ByVal ChequeNum As String = "", Optional ByVal nId_ExternalSystem As Long = 0, _
                                        Optional Byval nBranch As Integer = 0, Optional Byval nProduct As Integer = 0)

        Dim lclsConfig As New eRemoteDB.VisualTimeConfig
        Dim objContext As New eRemoteDB.ASPSupport
        Dim lclsUsers As New eSecurity.User
        Dim lclsClient As New eClient.Client
        Dim lclsExchange As New eGeneral.Exchange
        Dim nUsercode_Aux As Integer
        Dim SegnRutDestinatario_Aux As String = CStr(CInt(SegnRutDestinatario)).Trim
        Dim SegnNombre_Aux As String
        Dim SegnRutSolicitante_Aux As String
        Dim TraId As String
        Dim nTraId As Integer
        Dim nAmountPay As Decimal
        Dim nStatus As Long

        Dim SegnFormaPago As String = EquivalentFieldToClass("SREQUEST_TY", , , , , , , , , sRequest_Type)
        Dim SegnBanco As String = EquivalentFieldToClass("NBANK_CODE", , , , , , , , nBank_Code)
        Dim SegnTipoCuenta As String = EquivalentFieldToClass("NACC_TYPE", , , , , , , , nAcc_Type)
        Dim SegnSucursal As String = EquivalentFieldToClass("NOFFICE", , , , , , , , nOffice)

        Dim bTrace As Boolean = lclsConfig.LoadSetting("Trace", "Yes", "ExtensionSTS") = "Yes"

        If nExternalConcept <= 0 Then
            TraId = EquivalentFieldToClass("NCONCEPT",nBranch,nProduct, , , , , , nConcept)
        Else
            TraId = EquivalentFieldToClass("NCONCEPT",nBranch,nProduct, , , , , , nExternalConcept)
        End If

        If SegnSucursal = "" Then
            SegnSucursal = "Santiago"
        End If

        'cls.NotifyNewCheque("CORPVIDA", "1", "Principal", "21566593", "3", "Gilmer", "21566593", "20/08/2013", "Deposito", "001", "000116565841", "Corriente", "", 100, 1, "Prueba")
        If lclsClient.Find(SegnRutDestinatario) Then
            SegnNombre_Aux = lclsClient.sCliename.Trim
        Else
            SegnNombre_Aux = ""
        End If

        If String.IsNullOrEmpty(TraId) Then
            nTraId = 0
        Else
            nTraId = TraId
        End If

        If nUsercode <= 0 Then
            nUsercode_Aux = objContext.GetASPSessionValue("nUsercode")
        Else
            nUsercode_Aux = nUsercode
        End If

        If lclsUsers.Find(nUsercode_Aux) Then
            SegnRutSolicitante_Aux = CStr(CInt(lclsUsers.sClient.Trim))
        Else
            SegnRutSolicitante_Aux = ""
        End If

        If nCurrencyPay <> 1 Then
            Call lclsExchange.Convert(0, SegnTotal, nCurrencyPay, 1, Today, 0)
            nAmountPay = lclsExchange.pdblResult
        Else
            nAmountPay = SegnTotal
        End If

        sMessage = "Comenzando Bloque interface STS"
        Try
            sMessage += "Comenzando Bloque STS. "
            Dim asb As System.Reflection.Assembly
            sMessage += "Cargando Assembly. "
            asb = System.Reflection.Assembly.LoadFrom(lclsConfig.LoadSetting("DllFullPath", "", "ExtensionSTS"))
            sMessage += "ok. Instanciando clase "
            Dim cls As Object = asb.CreateInstance("CorpvidaIntegration.STSClient")

            sMessage += "ok. "

            If Not cls Is Nothing Then
                sMessage += "objeto instanciado. "
            Else
                sMessage += "Objeto es nothing. "
            End If
            sMessage += "Asignando EndPoint. "

            cls.RemoteAddress = lclsConfig.LoadSetting("WSEndPoint", "", "ExtensionSTS")
            cls.ProxyAddress = lclsConfig.LoadSetting("ProxyAddress", "", "ExtensionSTS")
            cls.ProxyUserName = lclsConfig.LoadSetting("ProxyUserName", "", "ExtensionSTS")
            cls.ProxyPassword = lclsConfig.LoadSetting("ProxyPassword", "", "ExtensionSTS")
            cls.ProxyCredentialsDomain = lclsConfig.LoadSetting("ProxyCredentialsDomain", "", "ExtensionSTS")
            sMessage += "Ok. "
            sMessage += "Invocando notificacion:" & EmpEmpresa & "," & Origen & "," & SegnDireccion & "," & SegnRutDestinatario_Aux & "," & SegnDvDestinatario & "," & SegnNombre_Aux & "," & SegnRutSolicitante_Aux & "," & SegnFechaIngreso & "," & SegnFormaPago & "," & SegnBanco & "," & SegnNumeroCuenta & "," & SegnTipoCuenta & "," & SegnSucursal & "," & nAmountPay & "," & nTraId & "," & SegnObservacion & "," & SegnId

            If nId_ExternalSystem <= 0 Then

                cls.NotifyNewCheque(EmpEmpresa, Origen, SegnDireccion, _
                                    SegnRutDestinatario_Aux, SegnDvDestinatario, SegnNombre_Aux, _
                                    SegnRutSolicitante_Aux, SegnFechaIngreso, SegnFormaPago, _
                                    SegnBanco, SegnNumeroCuenta, SegnTipoCuenta, _
                                    SegnSucursal, nAmountPay, nTraId, _
                                    SegnObservacion, SegnId)

                If cls.RetornoEstado.Id > 0 Then
                    UpdChequeId_ExternalSystem(RequestNu, cls.RetornoEstado.Id)
                    nId_ExternalSystem = cls.RetornoEstado.Id
                    sMessage_sts = cls.RetornoEstado.Descripcion
                Else
                    sMessage_sts = "No se genero solicitud en el sts. Causa: " & cls.RetornoEstado.Descripcion
                End If
            Else
                nStatus = cls.CheckSTSStatus(EmpEmpresa, Origen, nTraId, nId_ExternalSystem)
                If nStatus = 1 Then
                    sMessage_sts = "La solicitud " & nId_ExternalSystem & " esta en proceso"
                End If
                If nStatus = 5 Then
                    sMessage_sts = "La solicitud " & nId_ExternalSystem & " esta rechazada"
                    nSta_cheque = 5 ' anulado
                End If
                If nStatus = 8 Then
                    nSta_cheque = 6 ' cobrado
                    sMessage_sts = "La solicitud " & nId_ExternalSystem & " esta enviada a tesoreria"
                End If
                If nStatus = -1 Then
                    sMessage_sts = "La solicitud " & nId_ExternalSystem & " no existe"
                End If
                If nStatus = 5 Or nStatus = 8 Then
                    UpdSta_cheque(RequestNu, nSta_cheque, nUsercode)
                End If
            End If

            sMessage += "Fin Invocacion."
            If bTrace Then
                Throw New Exception("Invocacion STS sin problemas.")
            End If
        Catch ex As Exception
            If lclsConfig.LoadSetting("IgnoreError", "Yes", "ExtensionSTS") = "No" Then
                If bTrace Then
                    Throw New Exception(ex.Message & ".Origen:" & ex.Source & ".Traza:" & sMessage)
                Else
                    Throw New Exception(ex.Message & ".Origen:" & ex.Source)
                End If
            End If
        Finally
        End Try
    End Sub

    Public Function EquivalentFieldToClass(ByVal sField As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nRole As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nInValue As Integer = 0, Optional ByVal sInValue As String = "") As String
        Dim lrecTime As eRemoteDB.Execute

        Dim lstrOutValue As String = String.Empty

        On Error GoTo EquivalentFieldToClass_Err

        lrecTime = New eRemoteDB.Execute

        With lrecTime
            .StoredProcedure = "VTINTEGRATION_UTILITIESPKG.GetEquivalentValueToClass"
            .Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInValue", nInValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInValue", sInValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOutValue", lstrOutValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 150, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            EquivalentFieldToClass = .Parameters("sOutValue").Value.ToString.Trim 
  
        End With

EquivalentFieldToClass_Err:
        If Err.Number Then
            EquivalentFieldToClass = String.Empty  
        End If
        On Error GoTo 0
    End Function

	'**% UpdChequeId_ExternalSystem: updates the registration (the field Id_ExternalSystem)
	'** in the table of Checks Applications (Checks)
	'% UpdChequeId_ExternalSystem: Actualiza registros (el campo Id_ExternalSystem)
	'  dentro de la tabla de Solicitud de Cheques (Cheques)
	Public Function UpdChequeId_ExternalSystem(ByVal nRequest_nu As Integer,  nId_ExternalSystem As Integer) As Boolean
		
		'**- Variable definition lrecupdCheques
		'- Se define la variable lrecupdCheques
		
		Dim lrecupdCheques As eRemoteDB.Execute
		lrecupdCheques = New eRemoteDB.Execute
		
		On Error GoTo UpdChequeId_ExternalSystem_Err
		
		'**+ Parameter definition for stored procedure 'insudb.UpdChequeId_ExternalSystem'
		'+ Definición de parámetros para stored procedure 'insudb.UpdChequeId_ExternalSystem'
		'**+ Data of February 22,2001  10:34:10   a.m.
		'+ Información leída el 22/02/2001 10:34:10 a.m.
		With lrecupdCheques
			.StoredProcedure = "UpdChequeId_ExternalSystem"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_ExternalSystem", nId_ExternalSystem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdChequeId_ExternalSystem = .Run(False)
		End With
		
		lrecupdCheques = Nothing
		
UpdChequeId_ExternalSystem_Err: 
		If Err.Number Then
			UpdChequeId_ExternalSystem = False
		End If
		On Error GoTo 0
	End Function

    '**% UpdChequeId_ExternalSystem: updates the registration (the field Id_ExternalSystem)
    '** in the table of Checks Applications (Checks)
    '% UpdChequeId_ExternalSystem: Actualiza registros (el campo Id_ExternalSystem)
    '  dentro de la tabla de Solicitud de Cheques (Cheques)
    Public Function UpdSta_cheque(ByVal nRequest_nu As Integer, ByVal nSta_cheque As Integer, ByVal nusercode As Long) As Boolean

        '**- Variable definition lrecupdCheques
        '- Se define la variable lrecupdCheques

        Dim lrecupdCheques As eRemoteDB.Execute
        lrecupdCheques = New eRemoteDB.Execute

        On Error GoTo UpdSta_cheque_Err

        '**+ Parameter definition for stored procedure 'insudb.UpdChequeId_ExternalSystem'
        '+ Definición de parámetros para stored procedure 'insudb.UpdChequeId_ExternalSystem'
        '**+ Data of February 22,2001  10:34:10   a.m.
        '+ Información leída el 22/02/2001 10:34:10 a.m.
        With lrecupdCheques
            .StoredProcedure = "UpdSta_cheque"
            .Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSta_cheque", nSta_cheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdSta_cheque = .Run(False)
        End With

        lrecupdCheques = Nothing

UpdSta_cheque_Err:
        If Err.Number Then
            UpdSta_cheque = False
        End If
        On Error GoTo 0
    End Function

End Class