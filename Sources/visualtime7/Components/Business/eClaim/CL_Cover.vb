Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Cl_Cover_NET.Cl_Cover")> Public Class Cl_Cover
	'%-------------------------------------------------------%'
	'% $Workfile:: CL_Cover.cls                             $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 2/12/11 10:04a                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	'**- All the main properties of the corresponding class from the table CL_Cover are defined
	'-Se definen las propiedades principales de la clase correspondientes a la tabla CL_Cover
	'Column_name                    Type                           Length Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------------ ------------------------------ ------ ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
	Public nClaim As Double 'int                             4      10    0     no                                  (n/a)                               (n/a)
	Public nModulec As Integer
	Public nCover As Integer 'smallint                        2      5     0     no                                  (n/a)                               (n/a)
	Public nCase_num As Integer 'smallint                        2      5     0     no                                  (n/a)                               (n/a)
	Public nDeman_type As Integer 'smallint                        2      5     0     no                                  (n/a)                               (n/a)
	Public nCurrency As Integer 'smallint                        2      5     0     no                                  (n/a)                               (n/a)
	Public nBranch_est As Integer 'smallint                        2      5     0     no                                  (n/a)                               (n/a)
	Public nBranch_led As Integer 'smallint                        2      5     0     no                                  (n/a)                               (n/a)
	Public nBranch_rei As Integer 'smallint                        2      5     0     yes                                 (n/a)                               (n/a)
	Public nCost_recu As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nDamages As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nExp_amount As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nFra_amount As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public sFrantype As String 'char                            1                  yes                                 no                                  yes
	Public nGroup As Integer 'smallint                        2      5     0     yes                                 (n/a)                               (n/a)
	Public nLoc_cos_re As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nLoc_pay_am As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nLoc_rec_am As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nLoc_Reserv As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nPay_amount As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nQuantity As Integer 'smallint                        2      5     0     yes                                 (n/a)                               (n/a)
	Public nRec_amount As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nReserve As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
    Public nInitialReserve As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public sReservstat As String 'char                            1                  yes                                 no                                  yes
	Public nUserCode As Integer 'smallint                        2      5     0     yes                                 (n/a)                               (n/a)
	Public nDamprof As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public nTax_amo As Double 'decimal                         9      14    2     yes                                 (n/a)                               (n/a)
	Public sClient As String
	Public sBill_ind As String
    Public nScreSI021 As Short
    Public nAmount2 As Double    
    Public nRasa As Double
    Public nDepreciatebase As Double
    Public nDepreciateamount As Double
    Public nDepreciaterate As Double
    Public nDaydedamount As Double
    Public nFrancdays As Double
	'**+ Key or Primary key of the CL_COVER table
	'+ KEY O LLAVE PRIMARIA DE LA TABLA CL_COVER
	'+ nClaim, nCase_num, nDeman_type, nCover, nCurrency, sClient
	
	'-Variables auxiliares
	Public nCurrency_an As Integer
	Public nReserve_o As Double
	Public nOpt_claityp As Integer
	Public dEffecdate As Date
	Public dPosted As Date
	Public nExchange As Double
	Public nExchange_o As Double
	Public nAmount As Double
	Public sAutomrep As String
	Public sShowInd As String
	Public nPay_amountT As Double
	Public nTot_locam As Double
	Public nTot_locam_s As Double
	Public nTotal As Double
	Public nAmount_adjus As Double
	Public nTransact As Integer
	Public nOpt_curr As Integer
	Public sDescover As String
	Public nFixamount As Double
	Public nMaxamount As Double
	Public nMinamount As Double
	Public nRate As Double
	Public nCapital As Double
	Public sRoureser As String
	Public nMedreser As Integer
	Public sInsurini As String
	Public sCacalili As String
	Public sCaren_type As String
	Public nCaren_quan As Integer
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public nGroup_insu As Integer
	Public sFrancapl As String
	Public sRec_fra As String
	Public sbrancht As String
	Public nFrandeda As Double
	Public sReservstat2 As String
	Public nPayconre As Integer
	Public sResmaypa As String
	Public sCoverDescript As String
	Public dDecladate As Date
	Public nSel As Integer
	Public nCaren_diff As Integer
	Public mintCountCover As Integer
    Public nRasaAnnual As Double

	'+Variables auxiliares para ventana SI007.
	Public sDesStatusCov As String
	Public sDesFrantype As String
	Public sDesCurrency As String
	Public sCliename As String
	Public sDigit As String
	Public nExcess As Double
	Public nCurrency_Excess As Integer
	Public ParamnResult As Double
	Public dCoverDate As Date
	
	'+ Variables de la table Cover
	
	'+ Otras variables
	Public dOccurdat As Date
	
	'**+ Recalculation indicator of the franchise
	'+ Indicador de rec�lculo de franquicia
	Public sFran_Ind As String
	
	'**+ Indicates if there is a declaration limit
	'+ Indica si hay l�mite de declaraci�n
	Public mblnExcess As Boolean
	
	Public sCldeathi As String
	
    Property nAmountUsed As Double

	'&Update_Cl_coverSI007: Actualiza las tablas correspondientes en el calculo de las provisiones
	Public Function Update_Cl_coverSI007() As Boolean
		Dim lrecinsUpdCl_coverSI007 As eRemoteDB.Execute
		
		On Error GoTo Update_Cl_coverSI007_Err
		
		lrecinsUpdCl_coverSI007 = New eRemoteDB.Execute
		
		'**% Parameters definition for the stored procedure 'insudb.insUpedCl_coverSI007'
		'%Definici�n de par�metros para stored procedure 'insudb.insUpdCl_coverSI007'
		'**%Data read on 17/01/2001
		'%Informaci�n le�da el 17/01/2001 2:18:44 PM
		
		With lrecinsUpdCl_coverSI007
			.StoredProcedure = "insUpdCl_coverSI007"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency_an", nCurrency_an, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOpt_claityp", nOpt_claityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", dPosted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamages", nDamages, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nin_amount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("In_nin_pay_amount", nPay_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFra_amount", nFra_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDam_prof", nDamprof, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReservstat", sReservstat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrantype", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomRep", sAutomrep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShowInd", sShowInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_amountT", nPay_amountT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTot_locam", nTot_locam, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTot_locam_s", nTot_locam_s, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotal", nTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_adjus", nAmount_adjus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDecladate", dDecladate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBill_ind", sBill_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update_Cl_coverSI007 = .Run(sShowInd = "1")
			If Update_Cl_coverSI007 Then
				If Not .EOF Then
					If sShowInd = "1" Then
						nPay_amountT = .FieldToClass("nPay_AmountT")
						nTot_locam = .FieldToClass("nTot_locam")
						nTot_locam_s = .FieldToClass("nTot_locam_s")
						nTotal = .FieldToClass("ntotal")
						nTransact = .FieldToClass("nTransact")
						nOpt_curr = .FieldToClass("nOpt_curr")
						nAmount = .FieldToClass("nAmount")
						nReserve = .FieldToClass("nReserve")
						.RCloseRec()
					End If
				End If
			End If
		End With
		lrecinsUpdCl_coverSI007 = Nothing
		
Update_Cl_coverSI007_Err: 
		If Err.Number Then
			Update_Cl_coverSI007 = False
		End If
		On Error GoTo 0
	End Function
	
	'%CalReserve: Realiza el calculo de la provisi�n del siniestro
	Public Function CalReserve() As Boolean
		Dim lrecinsCalReserve As eRemoteDB.Execute
		
		On Error GoTo CalReserve_Err
		
		lrecinsCalReserve = New eRemoteDB.Execute
		
'**% Parameters definition for the stored procedure 'insudb.insUpedCl_coverSI007'
'%Definici�n de par�metros para stored procedure 'insudb.insCalReserve'
		
		With lrecinsCalReserve
			.StoredProcedure = "insCalReserve"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFixAmount", nFixamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamages", nDamages, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrandedi", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrancapl", sFrancapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_amount", nPay_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFra_amount", nFra_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomrep", sAutomrep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMedreser", nMedreser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOpt_claityp", nOpt_claityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRec_fra", sRec_fra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sbrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShowInd", sShowInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrandeda", nFrandeda, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReserve", nReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeq", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCldeathi", String.Empty , eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancdays", nFrancdays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDaydeamount", nDaydedamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				CalReserve = True
				If sShowInd = "1" Or _
				   sShowInd = "3" Then
					nReserve = .FieldToClass("nReserve")
					nFrandeda = .FieldToClass("nFrandeda")
					.RCloseRec()
				End If
			Else
				CalReserve = False
			End If
		End With
		lrecinsCalReserve = Nothing
		
CalReserve_Err: 
		If Err.Number Then
			CalReserve = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValSI007Upd: the validations for the fields of the grid are performed
	'%insValSI007Upd: se realizan las validaciones de los campos del grid
    Public Function insValSI007Upd(ByVal sCodispl As String, Optional ByVal nCover As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal sReservstat As String = "", Optional ByVal nDamages As Double = 0, Optional ByVal nFra_amount As Double = 0, Optional ByVal nCapital As Double = 0, Optional ByVal sIndCapIliCover As String = "", Optional ByVal sSchema As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nReserve As Double = 0, Optional ByVal nPayAmount As Double = 0, Optional ByVal nFrandeda As Double = 0, Optional ByVal nDamprof As Double = 0, Optional ByVal nExchange As Double = 0, Optional ByVal nExchange_o As Double = 0, Optional ByVal nTransaction As Integer = 0, Optional ByVal nClaim As Double = 0, Optional ByVal sCaren_type As String = "", Optional ByVal nCaren_quan As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nProduct As Integer = 0, Optional ByVal dCoverDate As Date = #12:00:00 AM#, Optional ByVal sFrancapl As String = "", Optional ByVal nFrancdays As Double = 0, Optional ByVal nDaydedamount As Double = 0) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lsecTime As eSecurity.Secur_sche
        Dim lclsClaim As Claim
        Dim lstrError As String = ""
        Dim lstrSeparate As String

        Dim lintPosition1 As Integer
        Dim lbytPosition2 As Byte
        Dim lstrCacalili As String
        Dim lstrField As String = ""

        Dim lclsObject As Tab_am_excprod
        Dim lclsHealth As ePolicy.Health

        On Error GoTo InsValSI007Upd_Err

        lsecTime = New eSecurity.Secur_sche
        lclsClaim = New Claim

        '**+ Validation for the "sCaren_Type/nCaren_Quan" field
        '**+ Validacion del "Plazo de Espera"
        If sCaren_type <> "1" Then
            If lclsClaim.Find(nClaim) Then
                If sCaren_type = "2" Then
                    nCaren_diff = DateDiff(Microsoft.VisualBasic.DateInterval.Hour, dCoverDate, lclsClaim.dOccurdat)
                Else
                    If sCaren_type = "3" Then
                        nCaren_diff = DateDiff(Microsoft.VisualBasic.DateInterval.Day, dCoverDate, lclsClaim.dOccurdat)
                    Else
                        nCaren_diff = DateDiff(Microsoft.VisualBasic.DateInterval.Month, dCoverDate, lclsClaim.dOccurdat)
                    End If
                End If
                If nCaren_quan > nCaren_diff Then
                    lstrError = lstrError & "||" & "55512"
                End If
            End If
        End If

        mblnExcess = False
        '**+ Validation for the "Reserve Status" field
        '+Validacion del "Estado de la reserva"
        If sReservstat = String.Empty Or sReservstat = "0" Then
            lstrError = lstrError & "||" & "4091"
        Else
            If sReservstat = "2" Then
                mblnExcess = True
            End If
        End If


        If nReserve < 0 Then
            lstrError = lstrError & "||" & "9000123"
        End If


        '**+ Validation of the "Damage" field
        '+Validacion del "Estimado de da�os"
        'Si el estimado en da�os es menor o igual a cero y la operacion es distinta de rechazo
        If nDamages <= 0 And nTransaction <> 15 Then
            If sReservstat <> "10" Then
            lstrError = lstrError & "||" & "4104"
            End If
        Else
            lstrField = "3"
            If nDamages > nCapital Then
                '**+ If the coverages are from free capital then it is searched the string that saves the coverages
                '**+in relation with the indicator
                '+Se busca en la cadena que guarda si las coberturas son de capital libre la primera aparici�n de la cobertura en
                '+cuesti�n con dicho indicador
                lintPosition1 = InStr(1, sIndCapIliCover, CStr(nCover))


                '**+ Once the position of the first character of the code is obtained it proceeds to search for the first
                '**+ occurence of a hyphen after the first character in the code.
                '+Una vez obtenida la primera posici�n del c�digo de la cobertura se procede a buscar el primer gui�n luego del c�digo
                lbytPosition2 = InStr(lintPosition1 + 1, sIndCapIliCover, "-")

                '**+ In the hyphen position + 1 the indicator for the coverage of free capital will be found
                '+En la posici�n del gui�n + 1 se encontrar� el indicador del capital libre para la cobertura
                lstrCacalili = Mid(sIndCapIliCover, lbytPosition2 + 1, 1)
            End If

            '**+ The declaration of a claim is validated
            '+ Se valida l�mite de declaraci�n de siniestro.
            If lsecTime.Reload(eSecurity.Secur_sche.eTypeTable.Limits, sSchema) Then
                If Not lsecTime.valLimits(eSecurity.Secur_sche.eTypeLimits.clngLimitsClaimDec, sSchema, nBranch, nCurrency, CDec(nReserve), nProduct) Then

                    '**+ Pending status of approval is assigned due to the surpass of the declaration limit
                    '+ Se asigna estado pendiente de aprobaci�n ya que fue sobrepasado el l�mite de declaraci�n.
                    sReservstat = "2"
                    mblnExcess = True
                    lstrError = lstrError & "||" & "12117"
                End If
            End If

            If nDamages < (nPayAmount + nFrandeda) And nPayAmount > 0 Then
                lstrError = lstrError & "||" & "4036"
            End If
        End If


        '**+ If the coverage is selected, the content of the field must be equal to the one shown in the system
        '+Si la cobertura esta seleccionada, el contenido del campo debe ser igual al mostrado por el sistema
        If lstrField = "5" Then
            If nFra_amount <> nFrandeda Then
                lstrError = lstrError & "||" & "4217"
            End If
        End If

        '**+ If the coverage is selected, the content of the field must be equal to the damage amount
        '+ Si la cobertura esta seleccionada, el contenido del campo debe ser menor o igual
        '+ al monto de da�os (estimado)

        If nReserve > nDamages And nDamages >= 0 Then
            lstrError = lstrError & "||" & "4037"
        End If


        If nDamages > nReserve Then
            lstrError = lstrError & "||" & "784037"
        End If

        '**+ Validation of "Estimate according to professional"
        '+Validacion de  "Estimacion segun profesional"
        '**+ If the coverage is selected, the content of the field must be equal to the one shown in the system
        '+Si la cobertura esta seleccionada, el contenido del campo debe ser igual al mostrado por el sistema

        If nDamprof <= 0 And sReservstat <> "10" Then
            lstrError = lstrError & "||" & "4309"
        End If
        '**+ Validation of the " Factor of change"
        '+Validacion del "Factor de cambio "
        '**+ If the coverage is selected, the content of the field must be equal to the one shown in the system
        '+Si la cobertura esta seleccionada, el contenido del campo debe ser igual al mostrado por el sistema
        If nExchange <> nExchange_o Then
            lstrError = lstrError & "||" & "4279"
        End If

        If nTransaction = Claim_win.eClaimTransac.clngClaimAmendment Then
            If lclsClaim.Find(nClaim) Then
                '**+ If the claim is already paid , the reserve can not  be modified
                '+ Si el siniestro ya fu� pagado, no se puede modificar la reserva
                If lclsClaim.sStaclaim = Claim.Estatclaim.ePay Then
                    lstrError = lstrError & "||" & "4345"
                End If
            End If
        End If

        '+ Si la reserva es mayor al valor de contrato de reaseguro envia mensaje
        If Find_AmountExcess(nClaim, nReserve, nCurrency, dEffecdate) Then
            lstrError = lstrError & "||" & "100135"
        End If


        If nDamages > 0 Then
            If CalcAmountLoc(nDamages, nCurrency, dEffecdate) Then
                lstrError = lstrError & "||" & "80503"
            End If
        End If

        If nReserve > 0 Then
            If CalcAmountLoc(nReserve, nCurrency, dEffecdate) Then
                lstrError = lstrError & "||" & "80504"
            End If
        ElseIf sReservstat <> "10" Then
            lstrError = lstrError & "||" & "4058"
        End If

        If nDamprof > 0 Then
            If CalcAmountLoc(nDamprof, nCurrency, dEffecdate) Then
                lstrError = lstrError & "||" & "80505"
            End If
        End If

        If sFrancapl = "5" Then
            If nDaydedamount = 0 Or nFrancdays = 0 Or nDaydedamount = eRemoteDB.Constants.dblNull Or nFrancdays = eRemoteDB.Constants.dblNull Then
                lstrError = lstrError & "||" & "3285"
            End If
        End If

        If lstrError <> String.Empty Then
            lstrError = Mid(lstrError, 3)
            lobjErrors = New eFunctions.Errors
            With lobjErrors
                .ErrorMessage(sCodispl, , , , , , lstrError)
                insValSI007Upd = .Confirm()
            End With
            lobjErrors = Nothing
        Else
            insValSI007Upd = lstrError
        End If

        lsecTime = Nothing
        lclsClaim = Nothing

InsValSI007Upd_Err:
        If Err.Number Then
            insValSI007Upd = insValSI007Upd & Err.Description
        End If
        On Error GoTo 0
    End Function
	
	'%Find_sReservstat:
	Public Function Find_sReservstat(ByVal nClaim As Double) As Boolean
		Dim lrecinsreaCl_Cover_3 As eRemoteDB.Execute
		
		On Error GoTo Find_sReservstat_err
		
		lrecinsreaCl_Cover_3 = New eRemoteDB.Execute
		
		'**% Parameters definition for stored procedure 'insudb.insreaCl_Cover_3'
		'%Definici�n de par�metros para stored procedure 'insudb.insreaCl_Cover_3'
		'**%Data read on 19/01/2001
		'%Informaci�n le�da el 19/01/2001 3:12:18 PM
		
		Find_sReservstat = False
		With lrecinsreaCl_Cover_3
			.StoredProcedure = "insreaCl_Cover_3"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_sReservstat = True
				sReservstat = .FieldToClass("sReservstat")
				.RCloseRec()
			End If
		End With
		lrecinsreaCl_Cover_3 = Nothing
		
Find_sReservstat_err: 
		If Err.Number Then
			Find_sReservstat = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insPosSI007: The data for the claim reserve frame is updated
	'% insPostSI007: Se actualizan los datos para el frame de Reservas de siniestro.
    Public Function insPostSI007(ByVal nClaim As Double, ByVal nTransaction As Integer, ByVal sClient As String, ByVal nLast_mov As Integer, ByVal dEffecdate As Date, ByVal sBase As String, ByVal nUserCode As Integer, ByVal nCurrency As Integer, ByVal nCurrency_an As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal blnCreateInfo As Integer, ByVal dPosted As Date, ByVal nExchange As Double, ByVal nDamages As Double, ByVal nAmount As Double, ByVal nPay_amount As Double, ByVal nFra_amount As Double, ByVal nFrandeda As Double, ByVal nDamprof As Double, ByVal nBranch_est As Integer, ByVal nBranch_rei As Integer, ByVal nBranch_led As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nGroup As Integer, ByVal sReservstat As String, ByVal sFrantype As String, ByVal sAutomrep As String, ByVal sShowInd As String, ByVal nPay_amountT As Double, ByVal nTot_locam As Double, ByVal nTot_locam_s As Double, ByVal nTotal As Double, ByVal nAmount_adjus As Double, ByVal dDecladate As Date, ByVal sBill_ind As String, ByVal sSession As String, ByVal nScreSI021 As Short, Optional ByVal nFrancdays As Double = 0, Optional ByVal nDaydedamount As Double = 0) As Boolean
        Dim lrecinsPostsi007 As eRemoteDB.Execute

        On Error GoTo insPostsi007_Err

        lrecinsPostsi007 = New eRemoteDB.Execute

        '+ Definici�n de store procedure insPostsi007 al 07-18-2003 12:58:33

        With lrecinsPostsi007
            .StoredProcedure = "inssi007pkg.insPostsi007"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBase", sBase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency_an", nCurrency_an, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPosted", dPosted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDamages", nDamages, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_amount", nPay_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFra_amount", nFra_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrandeda", nFrandeda, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDamprof", nDamprof, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReservstat", sReservstat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFrantype", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAutomrep", sAutomrep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShowind", sShowInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_amountt", nPay_amountT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTot_locam", nTot_locam, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTot_locam_s", nTot_locam_s, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTotal", nTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_adjus", nAmount_adjus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDecladate", dDecladate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBill_ind", sBill_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContent", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransact", nTransact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOpt_curr", nOpt_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_out", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReserve", nReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSession", sSession, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nScreSI021", nScreSI021, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancdays", nFrancdays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDaydedamount", nDaydedamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)

            insPostSI007 = .Parameters("nContent").Value = 1

            If insPostSI007 Then
                nScreSI021 = .Parameters("nScreSI021").Value
                If sShowInd = "1" Then
                    nPay_amountT = .Parameters("nPay_AmountT").Value
                    nTot_locam = .Parameters("nTot_locam").Value
                    nTot_locam_s = .Parameters("nTot_locam_s").Value
                    nTotal = .Parameters("ntotal").Value
                    nTransact = .Parameters("nTransact").Value
                    nOpt_curr = .Parameters("nOpt_curr").Value
                    nAmount = .Parameters("nAmount_out").Value
                    nReserve = .Parameters("nReserve").Value
                End If
            End If
        End With

insPostsi007_Err:
        If Err.Number Then
            insPostSI007 = False
        End If
        lrecinsPostsi007 = Nothing
        On Error GoTo 0

    End Function
	'% Update_sReservstat_Case: Se actualizan los estados de la reserva por caso.
	Function Update_sReservstat_Case(ByRef nClaim As Double, ByRef nCase_num As Integer, ByRef nDeman_type As Integer, ByRef sReservstat As String, ByRef nUserCode As Integer) As Boolean
		Dim lrecupdCl_cover As eRemoteDB.Execute
		
		On Error GoTo Update_sReservstat_case_Err
		
		lrecupdCl_cover = New eRemoteDB.Execute
		
		With lrecupdCl_cover
			.StoredProcedure = "updCl_cover_sReservstat_Case"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReservstat", sReservstat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_sReservstat_Case = .Run(False)
		End With
		lrecupdCl_cover = Nothing
		
Update_sReservstat_case_Err: 
		If Err.Number Then
			Update_sReservstat_Case = False
		End If
		On Error GoTo 0
	End Function
	
	'%Update_sReservstat_all:
	Function Update_sReservstat_all() As Boolean
		
		Dim lrecupdCl_cover_sReservstat_all As eRemoteDB.Execute
		
		On Error GoTo Update_sReservstat_all_Err
		
		lrecupdCl_cover_sReservstat_all = New eRemoteDB.Execute
		
		
		'**% Parameters definition for stored procedure 'insudb.updCl_cover_sReservstat_all'
		'%Definici�n de par�metros para stored procedure 'insudb.updCl_cover_sReservstat_all'
		'**%Data read on 19/01/2001
		'%Informaci�n le�da el 19/01/2001 3:12:18 PM
		
		With lrecupdCl_cover_sReservstat_all
			.StoredProcedure = "updCl_cover_sReservstat_all"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReservstat", sReservstat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_sReservstat_all = .Run
		End With
		lrecupdCl_cover_sReservstat_all = Nothing
		
Update_sReservstat_all_Err: 
		If Err.Number Then
			Update_sReservstat_all = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Funcion Find_Policy: This function is in charge of obtaining the data for the coverages of a claim
	'%Funcion Find_Policy. Esta funcion se encarge de obtener los datos de las coberturas de un siniestro
	Public Function Find_Policy(ByVal nClaim As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Object) As Boolean
		Dim lrecinsReaCl_cover As eRemoteDB.Execute
		
		On Error GoTo Find_Policy_Err
		lrecinsReaCl_cover = New eRemoteDB.Execute
		
		'**% Parameters definition for stored procedure 'insudb.insReaCl_cover'
		'%Definici�n de par�metros para stored procedure 'insudb.insReaCl_cover'
		'**%Data read on 30/01/2001
		'%Informaci�n le�da el 30/01/2001 9:33:43 AM
		With lrecinsReaCl_cover
			.StoredProcedure = "insReaCl_cover"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_Policy = True
				Me.nPayconre = .FieldToClass("nPayconre")
				Me.nModulec = .FieldToClass("nModulec")
				Me.nGroup_insu = .FieldToClass("nGroup_insu")
				Me.sResmaypa = .FieldToClass("sResmaypa")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nReserve = .FieldToClass("nReserve")
				Me.nPay_amount = .FieldToClass("nPay_amount")
				Me.sCoverDescript = .FieldToClass("sConceptDescript")
				Me.sReservstat = .FieldToClass("sReservstat")
                Me.nLoc_Reserv = .FieldToClass("nLoc_reserv")
                Me.nAmount2 = .FieldToClass("nAmount")
                Me.nFra_amount = .FieldToClass("nFra_amount")
                .RCloseRec()
			Else
				Find_Policy = False
			End If
		End With
		lrecinsReaCl_cover = Nothing
Find_Policy_Err: 
		If Err.Number Then
			Find_Policy = False
		End If
		On Error GoTo 0
	End Function
	
	'%Funcion Find_SI008: Rescata datos de cl_cover
	Public Function Find_SI008(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal nCover As Integer = 0) As Boolean
		Dim lrecinsReaCl_cover_SI008 As eRemoteDB.Execute
		Dim nRecordCount As Integer

		On Error GoTo Find_SI008_Err
		Find_SI008 = False
		lrecinsReaCl_cover_SI008 = New eRemoteDB.Execute
		mintCountCover = 0
		'**% Parameters definition fot the stored procedure 'insudb.insReaCl_cover_SI008'
		'Definici�n de par�metros para stored procedure 'insudb.insReaCl_cover_SI008'
		'**+ Data read on 29/01/2001
		'Informaci�n le�da el 29/01/2001 4:01:32 PM
		With lrecinsReaCl_cover_SI008
			.StoredProcedure = "insReaCl_cover_SI008"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
                nRecordCount = .FieldToClass("nRecordCount")
				If nRecordCount = 1 Then
					Find_SI008 = True
					Me.nCurrency = .FieldToClass("nCurrency")
					Me.nCover = .FieldToClass("nCover")
					Me.sDescover = .FieldToClass("sShort_des")
					Me.nPayconre = .FieldToClass("nPayconre")
					Me.nModulec = .FieldToClass("nModulec")
					Me.nGroup_insu = .FieldToClass("nGroup_insu")
					Me.nReserve = .FieldToClass("nReserve")
					Me.nPay_amount = .FieldToClass("nPay_amount")
					Me.nLoc_Reserv = .FieldToClass("nLoc_reserv")
					Me.nDamages = .FieldToClass("nDamages")
                    Me.nFra_amount = .FieldToClass("nFra_amount")
                    Me.nInitialReserve = .FieldToClass("nInitialReserve")
                    Me.nLoc_pay_am = .FieldToClass("nLoc_pay_am")
                Else
					mintCountCover = 1
				End If
				.RCloseRec()
			End If
		End With
		lrecinsReaCl_cover_SI008 = Nothing
Find_SI008_Err: 
		If Err.Number Then
			Find_SI008 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValSI007: The validations for the frame are performed
	'%insValSI007: se realizan las validaciones del frame
	Public Function insValSI007(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As String
		
		Dim lerrTime As eFunctions.Errors
		Dim lclsValidate As ClaimBenef
		Dim lstrError As String
		On Error GoTo insValSI007_err
		
		lerrTime = New eFunctions.Errors
		lclsValidate = New ClaimBenef
		'**+ Reserve for the claims must exist
		'+ Deben existir reservas para el siniestro
		lstrError = lclsValidate.insValidate(nClaim, 2)
		
		If lstrError <> String.Empty Then
			With lerrTime
				.ErrorMessage("SI007",  ,  ,  ,  ,  , lstrError)
				insValSI007 = lerrTime.Confirm
			End With
		End If
		
insValSI007_err: 
		If Err.Number Then
			insValSI007 = "insValSI007: " & Err.Description
		End If
		On Error GoTo 0
		lclsValidate = Nothing
		lerrTime = Nothing
	End Function
	'**%Funcion Find_Policy: This function is in charge of obtaining the data for the coverages of a claim
	'%Funcion Find_Policy. Esta funcion se encarge de obtener los datos de las coberturas de un siniestro
	Public Function Find_CoverProvider(ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer) As String
		Dim lrecinsReaCl_cover As eRemoteDB.Execute
		
		On Error GoTo Find_CoverProvider_Err
		
		Find_CoverProvider = String.Empty
		
		lrecinsReaCl_cover = New eRemoteDB.Execute
		
		With lrecinsReaCl_cover
			.StoredProcedure = "TABCOVER_POLPKG.TABCOVER_POL"
			.Parameters.Add("sShowNum", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SCondition", "Cover.nCover = " & nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_CoverProvider = .FieldToClass("sProvider", String.Empty)
				.RCloseRec()
			End If
		End With
		
		lrecinsReaCl_cover = Nothing
		
Find_CoverProvider_Err: 
		If Err.Number Then
			Find_CoverProvider = String.Empty
		End If
		On Error GoTo 0
	End Function
	'%DelReserv(). Esta funcion se encarga de eliminar los registros asociados a la reserva.
	Public Function DelReserv(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nCurrency As Integer, ByVal nUserCode As Integer) As Boolean
		Dim lrecCl_cover As eRemoteDB.Execute
		
		On Error GoTo DelReserv_Err
		
		
		lrecCl_cover = New eRemoteDB.Execute
		
		With lrecCl_cover
			.StoredProcedure = "INSDELRESERV"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelReserv = .Run(False)
		End With
		
DelReserv_Err: 
		If Err.Number Then
			DelReserv = CBool(String.Empty)
		End If
		On Error GoTo 0
		lrecCl_cover = Nothing
		
	End Function
	
	'%FindClientCl_Cover: Verifica la existencia de provisiones asociadas al siniestro-caso y cliente en particular
	Public Function FindClientCl_Cover(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String) As Boolean
		
		Dim lrecCl_coverClient As eRemoteDB.Execute
		Dim nInd As Integer
		
		On Error GoTo FindClientCl_Cover_Err
		
		lrecCl_coverClient = New eRemoteDB.Execute
		nInd = 0
		
		With lrecCl_coverClient
			.StoredProcedure = "ReaCl_CoverClient"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd", nInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			FindClientCl_Cover = .Run(False)
			
			If .Parameters("nInd").Value = 1 Then
				FindClientCl_Cover = True
			Else
				FindClientCl_Cover = False
			End If
			
		End With
		
FindClientCl_Cover_Err: 
		If Err.Number Then
			FindClientCl_Cover = False
		End If
		On Error GoTo 0
		lrecCl_coverClient = Nothing
		
	End Function
	
	'%insGetFra_Amount: Obtiene el monto del deducible a devolver (s�lo para vida)Tabla CL_COVER
	Public Function insGetFra_Amount(ByRef nClaim As Object, ByRef nCase_num As Object, ByRef nDeman_type As Object, ByRef nModulec As Object, ByRef nCover As Object, ByRef nCurrency As Object, ByRef sClient As Object) As Double
		Dim lrecinsGetFra_Amount As eRemoteDB.Execute
		
		On Error GoTo insGetFra_Amount_Err
		
		lrecinsGetFra_Amount = New eRemoteDB.Execute
		
		With lrecinsGetFra_Amount
			.StoredProcedure = "insGetFra_Amount"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFra_amount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insGetFra_Amount = .Parameters("nFra_amount").Value
			End If
		End With
		
		lrecinsGetFra_Amount = Nothing
		
insGetFra_Amount_Err: 
		If Err.Number Then
			insGetFra_Amount = 0
		End If
		On Error GoTo 0
	End Function
	
    '%Find: Devuelve si existen provisiones para el caso de siniestros
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lrecreaCl_cover_exists As eRemoteDB.Execute
		
		On Error GoTo reaCl_cover_exists_Err
		
		lrecreaCl_cover_exists = New eRemoteDB.Execute
		
		'+ Definici�n de store procedure reaCl_cover_exists al 07-18-2003 09:37:53
		
		With lrecreaCl_cover_exists
			.StoredProcedure = "reaCl_cover_exists"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReserve", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFra_Amount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            Me.nFra_amount = .Parameters("nFra_Amount").Value
            Find = .Parameters("nReserve").Value > 0
		End With
		
reaCl_cover_exists_Err: 
		If Err.Number Then
			Find = False
		End If
        lrecreaCl_cover_exists = Nothing
		On Error GoTo 0
		
	End Function

    '%Find: Devuelve la prima deducible (nFra_Amount) y sClient
    Public Function Find_nFra_Amount(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
        Dim lrecreaCl_cover_nFra_Amount As eRemoteDB.Execute

        On Error GoTo reaCl_cover_nFra_Amount_Err

        lrecreaCl_cover_nFra_Amount = New eRemoteDB.Execute

        '+ Definici�n de store procedure reaCl_cover_nFra_Amount al 08-10-14 11:37:53

        With lrecreaCl_cover_nFra_Amount
            .StoredProcedure = "reaCl_cover_nFra_Amount"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReserve", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFra_Amount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            Me.nFra_amount = .Parameters("nFra_Amount").Value
            Me.sClient = .Parameters("sClient").Value
            Me.nCurrency = .Parameters("nCurrency").Value
            Find_nFra_Amount = .Parameters("nReserve").Value >= 0
        End With

reaCl_cover_nFra_Amount_Err:
        If Err.Number Then
            Find_nFra_Amount = False
        End If
        lrecreaCl_cover_nFra_Amount = Nothing
        On Error GoTo 0

    End Function
	'%Findkey: Devuelve si ecisten provisiones para el caso de siniestros
	Public Function Findkey(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal sClient As String) As Boolean
		Dim lrecreaCl_cover As eRemoteDB.Execute
		
		On Error GoTo reaCl_cover_Err
		
		lrecreaCl_cover = New eRemoteDB.Execute
		
		Findkey = False
		'+ Definici�n de store procedure reaCl_cover_exists al 07-18-2003 09:37:53
		
		With lrecreaCl_cover
			.StoredProcedure = "reaCl_cover"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Findkey = True
				nClaim = .FieldToClass("nclaim")
				nCase_num = .FieldToClass("ncase_num")
				nDeman_type = .FieldToClass("ndeman_type")
				nModulec = .FieldToClass("nmodulec")
				nCover = .FieldToClass("ncover")
				nCurrency = .FieldToClass("ncurrency")
				sClient = .FieldToClass("sclient")
				nBranch_est = .FieldToClass("nbranch_est")
				nBranch_led = .FieldToClass("nbranch_led")
				nBranch_rei = .FieldToClass("nbranch_rei")
				nCost_recu = .FieldToClass("ncost_recu")
				nDamages = .FieldToClass("ndamages")
				nExp_amount = .FieldToClass("nexp_amount")
				nFra_amount = .FieldToClass("nfra_amount")
				sFrantype = .FieldToClass("sfrantype")
				nGroup = .FieldToClass("ngroup")
				nLoc_cos_re = .FieldToClass("nloc_cos_re")
				nLoc_pay_am = .FieldToClass("nloc_pay_am")
				nLoc_rec_am = .FieldToClass("nloc_rec_am")
				nLoc_Reserv = .FieldToClass("nloc_reserv")
				nPay_amount = .FieldToClass("npay_amount")
				nQuantity = .FieldToClass("nquantity")
				nRec_amount = .FieldToClass("nrec_amount")
				nReserve = .FieldToClass("nreserve")
				sReservstat = .FieldToClass("sreservstat")
				nDamprof = .FieldToClass("ndamprof")
				nTax_amo = .FieldToClass("ntax_amo")
				sBill_ind = .FieldToClass("sbill_ind")
			End If
		End With
		
reaCl_cover_Err: 
		If Err.Number Then
			Findkey = False
		End If
		On Error GoTo 0
		lrecreaCl_cover = Nothing
	End Function
	
	'% insPostSI007_total: Actualiza los totales de reservas por siniestro
	Public Function insPostSI007_total(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nOperation As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nTot_locam As Double, ByVal nTotal As Double, ByVal nUserCode As Integer) As Boolean
		Dim lrecinsPostsi007 As eRemoteDB.Execute
		
		On Error GoTo insPostsi007_Err
		
		lrecinsPostsi007 = New eRemoteDB.Execute
		
		'+ Definici�n de store procedure insPostsi007 al 07-18-2003 13:43:59
		
		With lrecinsPostsi007
			.StoredProcedure = "inssi007pkg.insPostsi007_total"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOperation", nOperation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTot_locam", nTot_locam, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotal", nTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContent", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			insPostSI007_total = .Parameters("nContent").Value = 1
		End With
		
insPostsi007_Err: 
		If Err.Number Then
			insPostSI007_total = False
		End If
		lrecinsPostsi007 = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Find_AmountExcess: Busca el monto en exceso del contrato de reaseguro
	Public Function Find_AmountExcess(ByVal nClaim As Double, ByVal nReserve As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecFind_AmountExcess As eRemoteDB.Execute
		Dim ParamExchange As Integer
		Dim nReserve_aux As Double
		
		ParamExchange = VariantType.Null
		
		On Error GoTo Find_AmountExcess_Err
		
		lrecFind_AmountExcess = New eRemoteDB.Execute
		Find_AmountExcess = False
		'+ Definici�n de store procedure insPostsi007 al 07-18-2003 13:43:59
		
		With lrecFind_AmountExcess
			.StoredProcedure = "Reanexcess"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nExcess = .FieldToClass("nExcess")
				nCurrency_Excess = .FieldToClass("nCurrency")
			Else
				nExcess = VariantType.Null
				nCurrency_Excess = VariantType.Null
			End If
		End With
		
		If nExcess <> VariantType.Null And nExcess > 0 And nCurrency_Excess <> VariantType.Null Then
			If nCurrency_Excess <> nCurrency Then
				With lrecFind_AmountExcess
					.StoredProcedure = "Inscalconvertexchange2"
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("ParamnExchange", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nReserve", nReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurrency_Excess", nCurrency_Excess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("Paramseffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("ParamnResult", ParamnResult, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If .Run(False) Then
						ParamnResult = .Parameters("ParamnResult").Value
						'ParamnResult = .FieldToClass("ParamnResult")
					End If
				End With
				If ParamnResult >= nExcess Then
					Find_AmountExcess = True
				Else
					Find_AmountExcess = False
				End If
			Else
				If nReserve >= nExcess Then
					Find_AmountExcess = True
				Else
					Find_AmountExcess = False
				End If
			End If
		End If
		
Find_AmountExcess_Err: 
		If Err.Number Then
			Find_AmountExcess = False
		End If
		lrecFind_AmountExcess = Nothing
		On Error GoTo 0
		
	End Function
	
	'% CalcAmountLoc: Calcula el monto local de los montos utilizados en la transaccion SI007
	Public Function CalcAmountLoc(ByVal nReserve As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecCalcAmountLoc As eRemoteDB.Execute
		Dim ParamExchange As Integer
		Dim nLoc_Amount As Double
		Dim nCurrency_opt As Integer
		
		nLoc_Amount = 1000000000000#
		
		On Error GoTo CalcAmountLoc_Err
		
		lrecCalcAmountLoc = New eRemoteDB.Execute
		
		CalcAmountLoc = False
		
		With lrecCalcAmountLoc
			.StoredProcedure = "reaOpt_sinies"
			If .Run Then
				nCurrency_opt = .FieldToClass("nCurrency")
			End If
		End With
		
		With lrecCalcAmountLoc
			.StoredProcedure = "Inscalconvertexchange2"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("ParamnExchange", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ParamnAmount", nReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ParamnCurOri", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ParamnCurDes", nCurrency_opt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Paramseffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ParamnResult", ParamnResult, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
				ParamnResult = .Parameters("ParamnResult").Value
			End If
		End With
		If ParamnResult > nLoc_Amount Then
			CalcAmountLoc = True
		Else
			CalcAmountLoc = False
		End If
		
CalcAmountLoc_Err: 
		If Err.Number Then
			CalcAmountLoc = False
		End If
		lrecCalcAmountLoc = Nothing
		On Error GoTo 0
		
    End Function


    '% insPostSI007_GM: Se actualizan los datos para el frame de Reservas de siniestro.
    Public Function InsPostSI007_GM(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nCover As Integer, ByVal sClient As String) As Boolean

        Dim lrecinsPostSI007_GM As eRemoteDB.Execute

        On Error GoTo insPostSI007_GM_Err

        lrecinsPostSI007_GM = New eRemoteDB.Execute

        '+ Definici�n de store procedure insPostSI007_GM al 07-18-2003 12:58:33

        With lrecinsPostSI007_GM
            .StoredProcedure = "INSI007_GM"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)

            insPostSI007_GM = .Parameters("nContent").Value = 1

            'If insPostSI007_GM() Then
            nScreSI021 = .Parameters("nScreSI021").Value
            If sShowInd = "1" Then
                nPay_amountT = .Parameters("nPay_AmountT").Value
                nTot_locam = .Parameters("nTot_locam").Value
                nTot_locam_s = .Parameters("nTot_locam_s").Value
                nTotal = .Parameters("ntotal").Value
                nTransact = .Parameters("nTransact").Value
                nOpt_curr = .Parameters("nOpt_curr").Value
                nAmount = .Parameters("nAmount_out").Value
                nReserve = .Parameters("nReserve").Value
            End If
            ' End If
        End With

insPostSI007_GM_Err:
        If Err.Number Then
            insPostSI007_GM = False
        End If
        lrecinsPostSI007_GM = Nothing
        On Error GoTo 0

    End Function

    '% insRoutineRASA: Rutina maestra para el calculo de RASA
    Public Function insCalSI008(ByVal nClaim As Double, ByVal nCase_num As Double, ByVal nDeman_type As Double, ByVal nModulec As Double, ByVal nGroup_insu As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal nDepreciateamount As Double, ByVal nDepreciatebase As Double, ByVal nDepreciaterate As Double, ByVal nFra_amount As Double, ByVal nAmountPayedCover As Double, ByVal nAmountPayCover As Double, ByVal sRASA_routine As String, ByVal sOrigin As String) As Boolean
        Dim lrecCalcAmountLoc As eRemoteDB.Execute

        Dim nRasa As Integer


        On Error GoTo insCalSI008_Err

        lrecCalcAmountLoc = New eRemoteDB.Execute

        insCalSI008 = False

        With lrecCalcAmountLoc
            .StoredProcedure = "insCalSI008"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_Insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDepreciateamount", nDepreciateamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDepreciaterate", nDepreciaterate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDepreciatebase", nDepreciatebase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFra_amount", nFra_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountPayCover", nAmountPayCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountPayedCover", nAmountPayedCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRASA_routine", sRASA_routine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOrigin", sOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRasaAnnual", Me.nRasaAnnual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRasa", nRasa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Me.nRasa = .Parameters("nRasa").Value
                Me.nRasaAnnual = .Parameters("nRasaAnnual").Value
                Me.nFra_amount = .Parameters("nFra_amount").Value
                Me.nDepreciateamount = .Parameters("nDepreciateamount").Value
                Me.nDepreciatebase = .Parameters("nDepreciatebase").Value
                Me.nDepreciaterate = .Parameters("nDepreciaterate").Value
                insCalSI008 = True
            End If
        End With
insCalSI008_Err:
        If Err.Number Then
            insCalSI008 = False
        End If        
        On Error GoTo 0

    End Function
End Class






