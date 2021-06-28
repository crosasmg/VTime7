Option Strict Off
Option Explicit On
Public Class Tab_covrol
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_covrol.cls                           $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 6/02/06 11:00                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema el 27/12/2000
	'- Column_Name                                 Type          Length  Prec  Scale Nullable
	'------------------------- --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nModulec As Integer ' NUMBER        22     5      0 No
	Public nCover As Integer ' NUMBER        22     5      0 No
	Public nRolcap As Integer ' NUMBER        22     5      0 Yes
	Public nRole As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public sRequired As String ' CHAR           1              No
	Public sDefaulti As String ' CHAR           1              Yes
	Public sRoupremi As String ' CHAR          12              Yes
	Public nAgemininsm As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxinsm As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxperm As Integer ' NUMBER        22     5      0 Yes
	Public nAgemininsf As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxinsf As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxperf As Integer ' NUMBER        22     5      0 Yes
	Public dNulldate As Date ' DATE           7              Yes
	Public nusercode As Integer ' NUMBER        22     5      0 No
	Public nCacalcov As Integer ' NUMBER        22     5      0 Yes
	Public nCacalfix As Double ' NUMBER        22    12      0 Yes
	Public nCacalmul As Integer ' NUMBER        22     5      0 Yes
	Public nCapbaspe As Double ' NUMBER        22     5      2 Yes
	Public nCapmaxim As Double ' NUMBER        22    12      0 Yes
	Public nCapminim As Double ' NUMBER        22    12      0 Yes
	Public nCover_in As Integer ' NUMBER        22     5      0 Yes
	Public nRolprem As Integer ' NUMBER        22     5      0 Yes
	Public nPremirat As Double ' NUMBER        22     9      6 Yes
	Public nNotenum As Integer ' NUMBER        22    10      0 Yes
	Public nDuratInd As Integer ' NUMBER        22     5      0 Yes
	Public sRechapri As String ' CHAR           1              Yes
	Public sRenewali As String ' CHAR           1              Yes
	Public sRouchaca As String ' CHAR          12              Yes
	Public sRouchapr As String ' CHAR          12              Yes
	Public nDuratPay As Integer ' NUMBER        22     5      0 Yes
	Public sRevIndex As String ' CHAR           1              Yes
	Public sRouprcal As String ' CHAR          12              Yes
	Public nFrancFix As Double ' NUMBER        22    10      0 Yes
	Public sFrancApl As String ' CHAR           1              Yes
	Public nFrancMax As Double ' NUMBER        22    10      0 Yes
	Public nFrancMin As Double ' NUMBER        22    10      0 Yes
	Public nFrancrat As Single ' NUMBER        22     4      2 Yes
	Public sRoufranc As String ' CHAR          12              Yes
	Public sFrantype As String ' CHAR           1              Yes
	Public sFDRequire As String ' CHAR           1              Yes
	Public sFDChantyp As String ' CHAR           1              Yes
	Public nFDUserLev As Integer ' NUMBER        22     5      0 Yes
	Public nFDRateAdd As Double ' NUMBER        22     6      2 Yes
	Public nFDRateSub As Double ' NUMBER        22     6      2 Yes
	Public sCacaltyp As String ' CHAR           1              Yes
	Public nCamaxper As Double ' NUMBER        22     5      2 Yes
	Public nCamaxcov As Integer ' NUMBER        22     5      0 Yes
	Public sRoutineCC As String ' CHAR          12              Yes
	Public nRateCC As Double ' NUMBER        22     5      2 Yes
	Public nAmountCC As Double ' NUMBER        22    10      2 Yes
	Public sApplyCC As String ' CHAR           1              Yes
	Public nChPreLev As Double ' NUMBER        22     5      0 Yes
	Public nChCapLev As Double ' NUMBER        22     5      0 Yes
	Public nRateCapAdd As Double ' NUMBER        22     6      2 Yes
	Public nRateCapSub As Double ' NUMBER        22     6      2 Yes
	Public sChtypcap As String ' CHAR           1              Yes
	Public nRatePreAdd As Double ' NUMBER        22     6      2 Yes
	Public nRatePreSub As Double ' NUMBER        22     6      2 Yes
	Public sChangetyp As String ' CHAR           1              Yes
	Public sStatregt As String ' CHAR           1              Yes
	Public sRout_pay As String ' CHAR          12              Yes
	Public nTypdurpay As Integer ' NUMBER        22     5      0 Yes
	Public sCaren_type As String ' CHAR           1              Yes
	Public nTypdurins As Integer ' NUMBER        22     5      0 Yes
	Public nCaren_quan As Integer ' NUMBER        22     5      0 Yes
	Public sClaccidi As String ' CHAR          12              Yes
	Public sCldeathi As String ' CHAR          12              Yes
	Public sClincapi As String ' CHAR          12              Yes
	Public sClinvali As String ' CHAR          12              Yes
	Public sClvehaci As String ' CHAR          12              Yes
	Public sCliIllness As String ' CHAR          12              Yes
	Public sClsurvii As String ' CHAR          12              Yes
	Public nCamaxrol As Integer ' NUMBER        22     5      0 Yes
	Public nMax_role As Integer ' NUMBER        22     5      0 Yes
	Public nMaxrent As Double ' NUMBER        12     0      0 Yes
	Public nPremifix As Double ' NUMBER        22    10      2 Yes
	Public nRolActiv_rel As Integer ' NUMBER        22     5      0 Yes
	Public nCovActiv_rel As Integer ' NUMBER        22     5      0 Yes
	Public nPremimax As Double
	Public nId_table As Double
	Public sLeg As String
	Public nQmonth_vig As Integer
	Public nQbetweenmod As Integer
	Public nQmax_mod As Integer
	Public nTyp_AgeMinM As Integer
	Public nTyp_AgeMinF As Integer
	Public SROU_COND_CAP As String
	Public nPercCostFP As Double
	Public nRecCostFP As Double
    Public sRourate As String

	'- Se define la constante para los codispl en la subsecuencia de aseg/cobertura
	Private Const CS_WINDOWS As String = "DP19AP  DP19BP  DP50AP  DP035B  DP8003  "
	Private Const CN_FRAMESNUM As Integer = 5
	
	'- Se define la variable que contiene la imagen a asociar a la página en la secuencia
	Private mintPageImage As eFunctions.Sequence.etypeImageSequence
	
	'-Variables auxiliares
	'-Se declara la variable que guarda la descripción del role
	Public sDescrole As String
	
	'-Se declara la variable que indica si el registro esta seleccionado
	Public sSel As String
	
	Public nAge_init As Integer
	
	Public sTypeAge As String
	
	
	
	'%InsUpdTab_covrol: Realiza la actualización de la tabla
	Private Function InsUpdTab_covrol(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTab_covrol As eRemoteDB.Execute
		
		On Error GoTo InsUpdTab_covrol_Err
		
		lrecInsUpdTab_covrol = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdTab_covrol'
		'+Información leída el 31/10/01
		With lrecInsUpdTab_covrol
			.StoredProcedure = "InsUpdTab_covrol"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRolcap", nRolcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequired", sRequired, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoupremi", sRoupremi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgemininsm", nAgemininsm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgemaxinsm", nAgemaxinsm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgemaxperm", nAgemaxperm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgemininsf", nAgemininsf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgemaxinsf", nAgemaxinsf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgemaxperf", nAgemaxperf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalcov", nCacalcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalfix", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalmul", nCacalmul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapbaspe", nCapbaspe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapmaxim", nCapmaxim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapminim", nCapminim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_in", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRolprem", nRolprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremirat", nPremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuratind", nDuratInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRechapri", sRechapri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRenewali", sRenewali, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouchaca", sRouchaca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouchapr", sRouchapr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuratpay", nDuratPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRevindex", sRevIndex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouprcal", sRouprcal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancfix", nFrancFix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrancapl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancmax", nFrancMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancmin", nFrancMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancrat", nFrancrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoufranc", sRoufranc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrantype", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFdrequire", sFDRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFdchantyp", sFDChantyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFduserlev", nFDUserLev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFdrateadd", nFDRateAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFdratesub", nFDRateSub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacaltyp", sCacaltyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCamaxper", nCamaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCamaxcov", nCamaxcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCamaxrol", nCamaxrol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutinecc", sRoutineCC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatecc", nRateCC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountcc", nAmountCC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApplycc", sApplyCC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChprelev", nChPreLev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChcaplev", nChCapLev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatecapadd", nRateCapAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatecapsub", nRateCapSub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChtypcap", sChtypcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatepreadd", nRatePreAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatepresub", nRatePreSub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChangetyp", sChangetyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRout_pay", sRout_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaccidi", sClaccidi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCldeathi", sCldeathi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClincapi", sClincapi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClinvali", sClinvali, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClsurvii", sClsurvii, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClvehaci", sClvehaci, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliillness", sCliIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurpay", nTypdurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurins", nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCaren_type", sCaren_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCaren_quan", nCaren_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_role", nMax_role, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxrent", nMaxrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremifix", nPremifix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimax", nPremimax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRolActiv_rel", nRolActiv_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovActiv_rel", nCovActiv_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLeg", sLeg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nid_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonth_vig", nQmonth_vig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQbetweenmod", nQbetweenmod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmax_mod", nQmax_mod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_AgeMinM", nTyp_AgeMinM, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_AgeMinF", nTyp_AgeMinF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SROU_COND_CAP", SROU_COND_CAP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercCostFP", nPercCostFP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRecCostFP", nRecCostFP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not IsNumeric(sTypeAge) Then
				sTypeAge = CStr(eRemoteDB.Constants.intNull)
			End If
			.Parameters.Add("nTypeAge", CShort(sTypeAge), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRourate", sRourate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			InsUpdTab_covrol = .Run(False)
		End With
		
InsUpdTab_covrol_Err: 
		If Err.Number Then
			InsUpdTab_covrol = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTab_covrol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTab_covrol = Nothing
		On Error GoTo 0
	End Function
	
	'% Find: Lee los datos de un registro en la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaTab_covrol As eRemoteDB.Execute
		
		On Error GoTo ReaTab_covrol_Err
		
		lrecReaTab_covrol = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaTab_covrol'
		'+Información leída el 2/11/01
		With lrecReaTab_covrol
			.StoredProcedure = "ReaTab_covrol"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nModulec = .FieldToClass("nModulec")
				Me.nCover = .FieldToClass("nCover")
				Me.nRole = .FieldToClass("nRole")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				nRole = .FieldToClass("nRole")
				sDescrole = .FieldToClass("sDescrole")
				sSel = .FieldToClass("sSel")
				nRolcap = .FieldToClass("nRolcap")
				sRequired = .FieldToClass("sRequired")
				sDefaulti = .FieldToClass("sDefaulti")
				sRoupremi = .FieldToClass("sRoupremi")
				nAgemininsm = .FieldToClass("nAgemininsm")
				nAgemaxinsm = .FieldToClass("nAgemaxinsm")
				sRout_pay = .FieldToClass("sRout_pay")
				nAgemaxperm = .FieldToClass("nAgemaxperm")
				nAgemininsf = .FieldToClass("nAgemininsf")
				nAgemaxinsf = .FieldToClass("nAgemaxinsf")
				nAgemaxperf = .FieldToClass("nAgemaxperf")
				dNulldate = .FieldToClass("dNulldate")
				nCacalcov = .FieldToClass("nCacalcov")
				nCacalfix = .FieldToClass("nCacalfix")
				nCacalmul = .FieldToClass("nCacalmul")
				nCapbaspe = .FieldToClass("nCapbaspe")
				nCapmaxim = .FieldToClass("nCapmaxim")
				nCapminim = .FieldToClass("nCapminim")
				nCover_in = .FieldToClass("nCover_in")
				nRolprem = .FieldToClass("nRolprem")
				nPremirat = .FieldToClass("nPremirat")
				nNotenum = .FieldToClass("nNotenum")
				nDuratInd = .FieldToClass("nDuratind")
				sRechapri = .FieldToClass("sRechapri")
				sRenewali = .FieldToClass("sRenewali")
				sRouchaca = .FieldToClass("sRouchaca")
				sRouchapr = .FieldToClass("sRouchapr")
				nDuratPay = .FieldToClass("nDuratpay")
				sRevIndex = .FieldToClass("sRevindex")
				sRouprcal = .FieldToClass("sRouprcal")
				nFrancFix = .FieldToClass("nFrancfix")
				sFrancApl = .FieldToClass("sFrancapl")
				nFrancMax = .FieldToClass("nFrancmax")
				nFrancMin = .FieldToClass("nFrancmin")
				nFrancrat = .FieldToClass("nFrancrat")
				sRoufranc = .FieldToClass("sRoufranc")
				sFrantype = .FieldToClass("sFrantype")
				sFDRequire = .FieldToClass("sFdrequire")
				sFDChantyp = .FieldToClass("sFdchantyp")
				nFDUserLev = .FieldToClass("nFduserlev")
				nFDRateAdd = .FieldToClass("nFdrateadd")
				nFDRateSub = .FieldToClass("nFdratesub")
				sCacaltyp = .FieldToClass("sCacaltyp")
				nCamaxper = .FieldToClass("nCamaxper")
				nCamaxcov = .FieldToClass("nCamaxcov")
				nCamaxrol = .FieldToClass("nCamaxrol")
				sRoutineCC = .FieldToClass("sRoutinecc")
				nRateCC = .FieldToClass("nRatecc")
				nAmountCC = .FieldToClass("nAmountcc")
				sApplyCC = .FieldToClass("sApplycc")
				nChPreLev = .FieldToClass("nChprelev")
				nChCapLev = .FieldToClass("nChcaplev")
				nRateCapAdd = .FieldToClass("nRatecapadd")
				nRateCapSub = .FieldToClass("nRatecapsub")
				sChtypcap = .FieldToClass("sChtypcap")
				nRatePreAdd = .FieldToClass("nRatepreadd")
				nRatePreSub = .FieldToClass("nRatepresub")
				sChangetyp = .FieldToClass("sChangetyp")
				sStatregt = .FieldToClass("sStatregt")
				sClaccidi = .FieldToClass("sClaccidi")
				sCldeathi = .FieldToClass("sCldeathi")
				sClincapi = .FieldToClass("sClincapi")
				sClinvali = .FieldToClass("sClinvali")
				sClsurvii = .FieldToClass("sClsurvii")
				sClvehaci = .FieldToClass("sClvehaci")
				sCliIllness = .FieldToClass("sCliillness")
				nTypdurpay = .FieldToClass("nTypdurpay")
				nTypdurins = .FieldToClass("nTypdurins")
				sCaren_type = .FieldToClass("sCaren_type")
				nCaren_quan = .FieldToClass("nCaren_quan")
				nMax_role = .FieldToClass("nMax_role")
				nMaxrent = .FieldToClass("nMaxrent")
				nPremifix = .FieldToClass("nPremifix")
				nPremimax = .FieldToClass("nPremimax")
				nRolActiv_rel = .FieldToClass("nRolActiv_rel")
				nCovActiv_rel = .FieldToClass("nCovActiv_rel")
				sLeg = .FieldToClass("sLeg")
				nId_table = .FieldToClass("nid_table")
				nQmonth_vig = .FieldToClass("nQmonth_vig")
				nQbetweenmod = .FieldToClass("nQbetweenmod")
				nQmax_mod = .FieldToClass("nQmax_mod")
				nTyp_AgeMinM = .FieldToClass("nTyp_AgeMinM")
				nTyp_AgeMinF = .FieldToClass("nTyp_AgeMinF")
				SROU_COND_CAP = .FieldToClass("SROU_COND_CAP")
				nPercCostFP = .FieldToClass("nPercCostFP")
				nRecCostFP = .FieldToClass("nRecCostFP")
				sTypeAge = CStr(.FieldToClass("nTypeAge", String.Empty))
                sRourate = .FieldToClass("sRourate")

				.RCloseRec()
			End If
		End With
		
ReaTab_covrol_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaTab_covrol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_covrol = Nothing
		On Error GoTo 0
	End Function
	
	'% valExistsTab_covrol: Funcion que valida la effecdate
	Public Function valExistsTab_covrol(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date) As Boolean
		'UPGRADE_NOTE: Tab_covrol was upgraded to Tab_covrol_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Tab_covrol_Renamed As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valExistsTab_covrol_Err
		
		Tab_covrol_Renamed = New eRemoteDB.Execute
		
		'+ Definición de store procedure valExistsTab_covrol
		With Tab_covrol_Renamed
			.StoredProcedure = "valExistsTab_covrol"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistsTab_covrol = .Parameters("nExists").Value = 1
		End With
		
valExistsTab_covrol_Err: 
		If Err.Number Then
			valExistsTab_covrol = False
		End If
		'UPGRADE_NOTE: Object Tab_covrol_Renamed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Tab_covrol_Renamed = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTab_covrol(1)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTab_covrol(3)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdTab_covrol(2)
	End Function
	
	'%Count:Permite determinar la existencia de coberturas para el producto
	Public Function Count(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Integer
		Dim lrecReaTab_covrol_count As eRemoteDB.Execute
		
		On Error GoTo Count_Err
		lrecReaTab_covrol_count = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.ReaTab_covrol_count'
		'Información leída el 14/11/2001
		With lrecReaTab_covrol_count
			.StoredProcedure = "ReaTab_covrol_count"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Count = .FieldToClass("nCount")
				.RCloseRec()
			End If
		End With
		
Count_Err: 
		If Err.Number Then
			Count = 0
		End If
		'UPGRADE_NOTE: Object lrecReaTab_covrol_count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_covrol_count = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostDP705_K: Actualiza el indicador de contenido de la DP705
	Public Function InsPostDP705_K(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nusercode As Integer) As Boolean
		On Error GoTo InsPostDP705_K_Err
		
		Dim lclsProd_win As Prod_win
		Dim lstrContent As String
		
		lstrContent = "1"
		If Count(nBranch, nProduct, dEffecdate) > 0 Then
			lstrContent = "2"
		End If
		lclsProd_win = New Prod_win
		InsPostDP705_K = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP705", lstrContent, nusercode)
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		
InsPostDP705_K_Err: 
		If Err.Number Then
			InsPostDP705_K = False
		End If
		
	End Function
	'% InsPostDP705: Realiza la actualización de la BD según especificaciones funcionales
	Public Function InsPostDP705(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String, ByVal sBrancht As String, ByVal sRequired As String, ByVal sDefaulti As String, ByVal nExist As Integer, ByVal nMax_role As Integer, ByVal nRolActiv_rel As Integer, ByVal nCovActiv_rel As Integer, ByVal nusercode As Integer) As Boolean
		On Error GoTo InsPostDP705_Err
		
		With Me
			InsPostDP705 = True
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.nRole = nRole
			.dEffecdate = dEffecdate
			.nusercode = nusercode
			
			Select Case sAction
				Case "Add", "Update"
					'+Si el registro existe se actualiza
					If nExist = 1 Then
						If Find(nBranch, nProduct, nModulec, nCover, nRole, dEffecdate) Then
							.sStatregt = sStatregt
							.sRequired = IIf(sRequired = String.Empty, "2", sRequired)
							.sDefaulti = IIf(sDefaulti = String.Empty, "2", sDefaulti)
							.nMax_role = nMax_role
							.nRolActiv_rel = nRolActiv_rel
							.nCovActiv_rel = nCovActiv_rel
							.Update()
						End If
						
						'+Si el registro no existe se crea
					ElseIf nExist = 2 Then 
						.sStatregt = sStatregt
						.sRequired = IIf(sRequired = String.Empty, "2", sRequired)
						.sDefaulti = IIf(sDefaulti = String.Empty, "2", sDefaulti)
						.nMax_role = nMax_role
						.nRolActiv_rel = nRolActiv_rel
						.nCovActiv_rel = nCovActiv_rel
						InsPostDP705 = InsCreTab_covrol(sBrancht)
					End If
					
				Case "Del"
					InsPostDP705 = .Delete
					
			End Select
		End With
		
InsPostDP705_Err: 
		If Err.Number Then
			InsPostDP705 = False
		End If
	End Function
	
	'%InsCreTab_covrol: Este procedimiento asigna los valores por defecto de la tabla
	Private Function InsCreTab_covrol(ByVal sBrancht As String) As Boolean
		Dim lclsCover As Object
		Dim lclsTabCover As Object
		
		With Me
			If sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) Then
				
				lclsCover = New Gen_cover
				If lclsCover.Find(.nBranch, .nProduct, .nModulec, .nCover, .dEffecdate) Then
					lclsTabCover = New Tab_gencov
					Call lclsTabCover.Find(lclsCover.nCovergen)
					.nCacalfix = lclsTabCover.nCacalfix
					.sCacaltyp = lclsTabCover.sCacalfri
					.nCover_in = lclsTabCover.nCover_in
					.sFrancApl = lclsTabCover.sFrancApl
					.nFrancFix = lclsTabCover.nFrancFix
					.nFrancMax = lclsTabCover.nFrancMax
					.nFrancMin = lclsTabCover.nFrancMin
					.nFrancrat = lclsTabCover.nFrancrat
					.sFrantype = lclsTabCover.sFrantype
					.nPremirat = lclsTabCover.nPremirat
					.sRoufranc = lclsTabCover.sRoufranc
					.sRoupremi = lclsTabCover.sRoupremi
				End If
				
			Else
				lclsCover = New Life_cover
				If lclsCover.Find(.nBranch, .nProduct, .nModulec, .nCover, .dEffecdate) Then
					lclsTabCover = New Tab_lifcov
					Call lclsTabCover.Find(lclsCover.nCovergen)
					.nCacalfix = lclsTabCover.nCacalfix
					.nCover_in = lclsTabCover.nCover_in
					.nPremirat = lclsTabCover.nPremirat
					.sRechapri = lclsTabCover.sRechapri
					.sRenewali = lclsTabCover.sRenewali
					.sRouchaca = lclsTabCover.sRouchaca
					.sRouchapr = lclsTabCover.sRouchapr
					.sRouprcal = lclsTabCover.sRouprcal
					.nDuratInd = lclsTabCover.nDuratInd
					.nDuratPay = lclsTabCover.nDuratPay
					.sRevIndex = lclsTabCover.sRevIndex
				End If
				
			End If
			
			InsCreTab_covrol = .Add
		End With
		'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCover = Nothing
		'UPGRADE_NOTE: Object lclsTabCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTabCover = Nothing
	End Function
	
	'% LoadTabs: Genera la secuencia de ventanas a mostrar por cob/asegurado
	Public Function LoadTabs(ByVal bQuery As Boolean, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As String
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsSequence As eFunctions.Sequence
		Dim lcolCapital_ages As eProduct.Capital_ages
		
		Dim lintCount As Integer
		Dim lintAux As Integer
		Dim lstrHTMLCode As String
		Dim lintAction As Integer
		
		On Error GoTo LoadTab_err
		
		lclsQuery = New eRemoteDB.Query
		lclsSequence = New eFunctions.Sequence
		lcolCapital_ages = New eProduct.Capital_ages
		
		'-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
		'-extraído de la constante cstrWindows
		
		Dim lstrCodispl As String
		
		Me.Find(nBranch, nProduct, nModulec, nCover, nRole, dEffecdate)
		
		lstrHTMLCode = lclsSequence.makeTable
		
		lintAux = 1
		lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
		
		With Me
			For lintCount = 1 To CN_FRAMESNUM
				
				'+ Se extrae el código de la ventana
				lstrCodispl = Mid(CS_WINDOWS, lintAux, 8)
				lintAux = lintAux + 8
				
				Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
				
				'+ Se obtiene por cada transacción un campo (requerido) de la misma para identificar
				'+ si tiene o no contenido
				mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
				Select Case Trim(lstrCodispl)
					Case "DP19AP"
						If .sCacaltyp <> String.Empty Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
						
					Case "DP19BP"
						If .nCover_in <> eRemoteDB.Constants.intNull Or .sRoupremi <> String.Empty Or .nPremirat <> eRemoteDB.Constants.intNull Or .nPremifix <> eRemoteDB.Constants.intNull Or .nId_table <> eRemoteDB.Constants.intNull Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
						
					Case "DP50AP"
						If .nTypdurins <> eRemoteDB.Constants.intNull Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
						
					Case "DP035B"
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
						If .sFrantype <> String.Empty Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
						
					Case "DP8003"
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
						If lcolCapital_ages.FindCapital_age(nBranch, nProduct, dEffecdate, nModulec, nCover, nRole) Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
						
				End Select
				
				lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, lintAction, lclsQuery.FieldToClass("sShort_des"), mintPageImage)
				
			Next lintCount
		End With
		LoadTabs = lstrHTMLCode & lclsSequence.closeTable()
		
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		'UPGRADE_NOTE: Object lcolCapital_ages may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCapital_ages = Nothing
		
LoadTab_err: 
		If Err.Number Then
			LoadTabs = "LoadTabs: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%InsValDP19AP: En esta rutina se realizan las validaciones según especificaciones funcionales
	Public Function InsValDP19AP(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal sCacaltyp As String, ByVal nCacalfix As Double, ByVal nCapbaspe As Double, ByVal nCacalcov As Integer, ByVal nCapminim As Double, ByVal nCapmaxim As Double, ByVal sRouprcal As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValDP19AP_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Se la válida la forma de cálculo
			'+ Si la forma de cálculo es fijo
			If sCacaltyp = "2" And nCacalfix = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11321)
				
				'+ Si la forma de cálculo es %Otra cobertura
			ElseIf sCacaltyp = "3" Then 
				
				If nCapbaspe = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11311)
				End If
				
				If nCacalcov = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11312)
				End If
				'+ Si la forma de cálculo es Rutina
			ElseIf sCacaltyp = "4" Then 
				If sRouprcal = String.Empty Then
					.ErrorMessage(sCodispl, 1925)
				End If
			End If
			
			'+ Se valida el monto máximo
			If nCapmaxim <= nCapminim And nCapminim <> eRemoteDB.Constants.intNull And nCapmaxim <> eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11048)
			End If
			
			InsValDP19AP = .Confirm
		End With
		
InsValDP19AP_Err: 
		If Err.Number Then
			InsValDP19AP = "InsValDP19AP: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostDP19AP: Esta función realiza los cambios de BD según especificaciones funcionales
	'%               de la transacción Condiciones del capital asegurado(DP19AP)
	Public Function InsPostDP19AP(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal sCacaltyp As String, ByVal nCacalfix As Double, ByVal nCapbaspe As Double, ByVal nCacalcov As Integer, ByVal nRolcap As Integer, ByVal sAddSuini As String, ByVal sRouprcal As String, ByVal nCapminim As Double, ByVal nCapmaxim As Double, ByVal nCacalmul As Double, ByVal nChCapLev As Integer, ByVal sChtypcapAdd As String, ByVal sChtypcapSub As String, ByVal nRateCapAdd As Double, ByVal nRateCapSub As Double, ByVal nCamaxper As Double, ByVal nCamaxcov As Integer, ByVal nCamaxrol As Integer, ByVal nusercode As Integer, ByVal sLeg As String, ByVal nQmonth_vig As Integer, ByVal nQbetweenmod As Integer, ByVal nQmax_mod As Integer, ByVal SROU_COND_CAP As String) As Boolean
		On Error GoTo InsPostDP19AP_Err
		
		
		
		With Me
			If .Find(nBranch, nProduct, nModulec, nCover, nRole, dEffecdate) Then
				.dEffecdate = dEffecdate
				.sCacaltyp = sCacaltyp
				.nCacalfix = nCacalfix
				.nCapbaspe = nCapbaspe
				.nCacalcov = nCacalcov
				.nRolcap = nRolcap
				.sRouprcal = sRouprcal
				.nCapminim = nCapminim
				.nCapmaxim = nCapmaxim
				.nCacalmul = nCacalmul
				.nChCapLev = nChCapLev
				
				'+ Se valida el tipo de cambio permitido para el capital
				If sChtypcapAdd = String.Empty Then
					sChtypcapAdd = "0"
				End If
				
				If sChtypcapSub = String.Empty Then
					sChtypcapSub = "0"
				End If
				
				Select Case sChtypcapAdd & sChtypcapSub
					'+Ninguno
					Case "00"
						.sChtypcap = "1"
						'+Aumentar
					Case "10"
						.sChtypcap = "2"
						'+Disminuir
					Case "01"
						.sChtypcap = "3"
						'+Ambas
					Case "11"
						.sChtypcap = "4"
				End Select
				
				.nRateCapAdd = nRateCapAdd
				.nRateCapSub = nRateCapSub
				.nCamaxper = nCamaxper
				.nCamaxcov = nCamaxcov
				.nCamaxrol = nCamaxrol
				.nusercode = nusercode
				.sLeg = sLeg
				.nQmonth_vig = nQmonth_vig
				.nQbetweenmod = nQbetweenmod
				.nQmax_mod = nQmax_mod
				.SROU_COND_CAP = SROU_COND_CAP
				InsPostDP19AP = .Update
			End If
		End With
		
InsPostDP19AP_Err: 
		If Err.Number Then
			InsPostDP19AP = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsValDP19BP: Validaciones según especificaciones funcionales de la transacción DP19BP
	Public Function InsValDP19BP(ByVal sCodispl As String, ByVal nCover_in As Integer, ByVal sRoupremi As String, ByVal nPremirat As Double, ByVal sCldeathi As String, ByVal sClaccidi As String, ByVal sClvehaci As String, ByVal sClsurvii As String, ByVal sClincapi As String, ByVal sClinvali As String, ByVal sCliIllness As String, ByVal nAmountCC As Double, ByVal nRateCC As Double, ByVal sRoutineCC As String, ByVal sApplyCC As String, ByVal dEffecdate As Date, ByVal nPremifix As Double, Optional ByRef nBranch As Integer = 0, Optional ByRef nProduct As Integer = 0, Optional ByVal nId_table As Integer = 0) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		
		On Error GoTo InsValDP19BP_Err
		lclsErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		
		With lclsErrors
			
			'+ Se valida las condiciones de cálculo de prima
			If nCover_in = eRemoteDB.Constants.intNull Then
				If sRoupremi = String.Empty And nPremirat = eRemoteDB.Constants.intNull And nPremifix = eRemoteDB.Constants.intNull And nId_table = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11172)
				End If
			Else
				If sRoupremi <> String.Empty Or nPremirat <> eRemoteDB.Constants.intNull Or nPremifix <> eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11153)
				End If
			End If
			
			'+ Se valida los siniestros permitidos
			If sCldeathi = String.Empty And sClaccidi = String.Empty And sClvehaci = String.Empty And sClsurvii = String.Empty And sClincapi = String.Empty And sCliIllness = String.Empty And sClinvali = String.Empty Then
				.ErrorMessage(sCodispl, 11045)
			End If
			
			'+ Se valida los costos por coberturas, solo para clase de productos = 7
			Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
			If nAmountCC = eRemoteDB.Constants.intNull And nRateCC = eRemoteDB.Constants.intNull And sRoutineCC = String.Empty And lclsProduct.nProdClas = 7 Then
				.ErrorMessage(sCodispl, 11032)
			End If
			
			If nAmountCC <> eRemoteDB.Constants.intNull And nRateCC <> eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 6063)
			End If
			
			If (nAmountCC <> eRemoteDB.Constants.intNull Or nRateCC <> eRemoteDB.Constants.intNull) And sRoutineCC <> String.Empty Then
				.ErrorMessage(sCodispl, 55674)
			End If
			
			InsValDP19BP = .Confirm
		End With
		
InsValDP19BP_Err: 
		If Err.Number Then
			InsValDP19BP = "InsValDP19BP: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostDP19BP: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                de la transacción Condiciones de prima y siniestros (DP19BP)
    Public Function InsPostDP19BP(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nCover_in As Integer, ByVal nRolprem As Integer, ByVal nPremirat As Double, ByVal sRoupremi As String, ByVal sChangetypAdd As String, ByVal sChangetypSub As String, ByVal nRatePreAdd As Double, ByVal nRatePreSub As Double, ByVal nChPreLev As Integer, ByVal sClaccidi As String, ByVal sCldeathi As String, ByVal sClincapi As String, ByVal sClinvali As String, ByVal sClsurvii As String, ByVal sClvehaci As String, ByVal sCliIllness As String, ByVal nAmountCC As Double, ByVal nRateCC As Double, ByVal sRoutineCC As String, ByVal sApplyCC As String, ByVal nusercode As Integer, ByVal nMaxrent As Double, ByVal nPremifix As Double, ByVal nPremimax As Double, ByVal nId_table As Double, ByVal nPercCostFP As Double, ByVal nRecCostFP As Double, ByVal sTypeAge As String, ByVal sRourate As String) As Boolean
        On Error GoTo InsPostDP19BP_Err

        With Me
            If .Find(nBranch, nProduct, nModulec, nCover, nRole, dEffecdate) Then
                .dEffecdate = dEffecdate
                .nCover_in = nCover_in
                .nRolprem = nRolprem
                .nPremirat = nPremirat
                .sRoupremi = sRoupremi

                '+ Se valida el tipo de cambio permitido para la prima
                If sChangetypAdd = String.Empty Then
                    sChangetypAdd = "0"
                End If

                If sChangetypSub = String.Empty Then
                    sChangetypSub = "0"
                End If

                Select Case sChangetypAdd & sChangetypSub
                    '+ Ninguno
                    Case "00"
                        .sChangetyp = "1"
                        '+ Aumentar
                    Case "10"
                        .sChangetyp = "2"
                        '+ Disminuir
                    Case "01"
                        .sChangetyp = "3"
                        '+ Ambas
                    Case "11"
                        .sChangetyp = "4"
                End Select

                .nRatePreAdd = nRatePreAdd
                .nRatePreSub = nRatePreSub
                .nChPreLev = nChPreLev
                .sClaccidi = sClaccidi
                .sCldeathi = sCldeathi
                .sClincapi = sClincapi
                .sClinvali = sClinvali
                .sClsurvii = sClsurvii
                .sClvehaci = sClvehaci
                .sCliIllness = sCliIllness
                .nAmountCC = nAmountCC
                .nRateCC = nRateCC
                .sRoutineCC = sRoutineCC
                .sApplyCC = sApplyCC
                .nusercode = nusercode
                .nMaxrent = nMaxrent
                .nPremifix = nPremifix
                .nPremimax = nPremimax
                .nId_table = nId_table
                .nPercCostFP = nPercCostFP
                .nRecCostFP = nRecCostFP
                .sTypeAge = IIf(sTypeAge <> String.Empty, "1", "2")
                .sRourate = sRourate

                InsPostDP19BP = .Update
            End If
        End With

InsPostDP19BP_Err:
        If Err.Number Then
            InsPostDP19BP = False
        End If
        On Error GoTo 0
    End Function
	
	'%InsValDP50AP: Validaciones según especificaciones funcionales de la transacción DP50AP
	Public Function InsValDP50AP(ByVal sCodispl As String, ByVal nTypdurins As Integer, ByVal nDuratInd As Integer, ByVal nTypdurpay As Integer, ByVal nDuratPay As Integer, ByVal sRout_pay As String, ByVal nAgemininsm As Integer, ByVal nAgemaxinsm As Integer, ByVal nAgemaxperm As Integer, ByVal nAgemininsf As Integer, ByVal nAgemaxinsf As Integer, ByVal nAgemaxperf As Integer, ByVal nTyp_AgeMinM As Integer, ByVal nTyp_AgeMinF As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValDP50AP_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Se valida la duración del seguro
			If (nTypdurins = 1 Or nTypdurins = 2) Then
				If nDuratInd = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11411,  , eFunctions.Errors.TextAlign.LeftAling, "Seguro: ")
				End If
			End If
			
			'+ Se valida la duración de los pagos
			If (nTypdurpay = 1 Or nTypdurpay = 2) Then
				If nDuratPay = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11411,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos: ")
				End If
			ElseIf nTypdurpay = 4 Then 
				If sRout_pay = String.Empty Then
					.ErrorMessage(sCodispl, 8333)
				End If
			End If
			
			'+ Se valida los topes de edad masculino
			If nAgemaxinsm <> eRemoteDB.Constants.intNull Then
				
				If (nAgemaxinsm < nAgemininsm And nTyp_AgeMinM = 2) Or ((nAgemaxinsm * 365) < nAgemininsm And nTyp_AgeMinM = 9) Then
					.ErrorMessage(sCodispl, 11409,  , eFunctions.Errors.TextAlign.LeftAling, "Masculino: ")
				End If
				
				If nAgemaxperm = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3544,  , eFunctions.Errors.TextAlign.LeftAling, "Masculino: ")
				ElseIf nAgemaxperm < nAgemaxinsm Then 
					.ErrorMessage(sCodispl, 55675,  , eFunctions.Errors.TextAlign.LeftAling, "Masculino: ")
				End If
			End If
			
			'+ Se valida los topes de edad femenino
			If nAgemaxinsf <> eRemoteDB.Constants.intNull Then
				If (nAgemaxinsf < nAgemininsf And nTyp_AgeMinF = 2) Or ((nAgemaxinsf * 365) < nAgemininsf And nTyp_AgeMinF = 9) Then
					.ErrorMessage(sCodispl, 11409,  , eFunctions.Errors.TextAlign.LeftAling, "Femenino: ")
				End If
				
				If nAgemaxperf = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3544,  , eFunctions.Errors.TextAlign.LeftAling, "Femenino: ")
				ElseIf nAgemaxperf < nAgemaxinsf Then 
					.ErrorMessage(sCodispl, 55675,  , eFunctions.Errors.TextAlign.LeftAling, "Femenino: ")
				End If
			End If
			
			InsValDP50AP = .Confirm
		End With
		
InsValDP50AP_Err: 
		If Err.Number Then
			InsValDP50AP = "InsValDP50AP: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsPostDP50AP: Esta función realiza los cambios de BD según especificaciones funcionales
	'%              de la transacción Duración y condiciones de renovación(DP50AP)
	Public Function InsPostDP50AP(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nTypdurins As Integer, ByVal nDuratInd As Integer, ByVal nTypdurpay As Integer, ByVal nDuratPay As Integer, ByVal sRout_pay As String, ByVal nAgemininsm As Integer, ByVal nAgemaxinsm As Integer, ByVal nAgemaxperm As Integer, ByVal nAgemininsf As Integer, ByVal nAgemaxinsf As Integer, ByVal nAgemaxperf As Integer, ByVal sRenewali As String, ByVal sRevIndex As String, ByVal sRechapri As String, ByVal sRouchapr As String, ByVal sRouchaca As String, ByVal nusercode As Integer, ByVal nTyp_AgeMinM As Integer, ByVal nTyp_AgeMinF As Integer) As Boolean
		On Error GoTo InsPostDP50AP_Err
		
		With Me
			If .Find(nBranch, nProduct, nModulec, nCover, nRole, dEffecdate) Then
				.dEffecdate = dEffecdate
				.nTypdurins = nTypdurins
				.nDuratInd = nDuratInd
				.nTypdurpay = nTypdurpay
				.nDuratPay = nDuratPay
				.sRout_pay = sRout_pay
				.nAgemininsm = nAgemininsm
				.nAgemaxinsm = nAgemaxinsm
				.nAgemaxperm = nAgemaxperm
				.nAgemininsf = nAgemininsf
				.nAgemaxinsf = nAgemaxinsf
				.nAgemaxperf = nAgemaxperf
				.sRenewali = IIf(sRenewali = String.Empty, "2", sRenewali)
				.sRevIndex = IIf(sRevIndex = String.Empty, "2", sRevIndex)
				.sRechapri = IIf(sRechapri = String.Empty, "2", sRechapri)
				.sRouchapr = sRouchapr
				.sRouchaca = sRouchaca
				.nusercode = nusercode
				.nTyp_AgeMinM = nTyp_AgeMinM
				.nTyp_AgeMinF = nTyp_AgeMinF
				InsPostDP50AP = .Update
			End If
		End With
		
InsPostDP50AP_Err: 
		If Err.Number Then
			InsPostDP50AP = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsValDP035B: Validaciones según especificaciones funcionales de la transacción DP035B
	Public Function InsValDP035B(ByVal sCodispl As String, ByVal sFrantype As String, ByVal sRoufranc As String, ByVal nFrancrat As Double, ByVal nFrancFix As Double, ByVal sFrancApl As String, ByVal nFrancMin As Double, ByVal nFrancMax As Double, ByVal sCaren_type As String, ByVal nCaren_quan As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValDP035B_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Se válida los campos de franquicia/deducible
			If sFrantype > "1" Then
				
				'+ Se validan los campos de cálculo de franq/deduc.
				If sRoufranc = String.Empty And nFrancrat = eRemoteDB.Constants.intNull And nFrancFix = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11316)
				End If
				
				'+ Se valida el campo Aplica sobre
				If sFrancApl = "1" And nFrancFix = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11318)
				End If
			End If
			
			'+ Se valida el campo Importe fijo
			If nFrancFix <> eRemoteDB.Constants.intNull Then
				If nFrancrat <> eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11075)
				End If
				
				If nFrancMin <> eRemoteDB.Constants.intNull Or nFrancMax <> eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11354)
				End If
			Else
				
				'+ Se valida el campo Importe máximo
				If nFrancMax <> eRemoteDB.Constants.intNull And nFrancMax <= nFrancMin Then
					.ErrorMessage(sCodispl, 11048)
				End If
			End If
			
			'+ Se valida la duración del plazo de espera
			If sCaren_type > "1" And nCaren_quan = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3644)
			End If
			InsValDP035B = .Confirm
		End With
		
InsValDP035B_Err: 
		If Err.Number Then
			InsValDP035B = "InsValDP035B: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostDP035B: Esta función realiza los cambios de BD según especificaciones funcionales
	'%              de la transacción Franquicia/Deducible(DP035B)
	Public Function InsPostDP035B(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal sFDRequire As String, ByVal sFrantype As String, ByVal sRoufranc As String, ByVal nFrancrat As Double, ByVal nFrancFix As Double, ByVal sFrancApl As String, ByVal nFrancMin As Double, ByVal nFrancMax As Double, ByVal sFDChantypAdd As String, ByVal sFDChantypSub As String, ByVal nFDRateAdd As Double, ByVal nFDRateSub As Double, ByVal sCaren_type As String, ByVal nCaren_quan As Integer, ByVal nFDUserLev As Integer, ByVal nusercode As Integer) As Boolean
		On Error GoTo InsPostDP035B_Err
		
		With Me
			If .Find(nBranch, nProduct, nModulec, nCover, nRole, dEffecdate) Then
				.dEffecdate = dEffecdate
				.sFDRequire = IIf(sFDRequire = String.Empty, "2", sFDRequire)
				.sFrantype = IIf(sFrantype = "0", String.Empty, sFrantype)
				.sRoufranc = sRoufranc
				.nFrancrat = nFrancrat
				.nFrancFix = nFrancFix
				.sFrancApl = IIf(sFrancApl = "0", String.Empty, sFrancApl)
				.nFrancMin = nFrancMin
				.nFrancMax = nFrancMax
				
				'+ Se valida el tipo de cambio permitido para la franq/deduc
				If sFDChantypAdd = String.Empty Then
					sFDChantypAdd = "0"
				End If
				
				If sFDChantypSub = String.Empty Then
					sFDChantypSub = "0"
				End If
				
				Select Case sFDChantypAdd & sFDChantypSub
					'+Ninguno
					Case "00"
						.sFDChantyp = "1"
						'+Aumentar
					Case "10"
						.sFDChantyp = "2"
						'+Disminuir
					Case "01"
						.sFDChantyp = "3"
						'+Ambas
					Case "11"
						.sFDChantyp = "4"
				End Select
				
				.nFDRateAdd = nFDRateAdd
				.nFDRateSub = nFDRateSub
				.sCaren_type = IIf(sCaren_type = "0", String.Empty, sCaren_type)
				.nCaren_quan = nCaren_quan
				.nFDUserLev = nFDUserLev
				.nusercode = nusercode
				InsPostDP035B = .Update
			End If
		End With
		
InsPostDP035B_Err: 
		If Err.Number Then
			InsPostDP035B = False
		End If
		On Error GoTo 0
	End Function
	
	'%sChtypcapAdd: Propiedad que indica si se marca el check de aumentar el capital de acuerdo
	'%              al valor del campo sChtypcap
	Public ReadOnly Property sChtypcapAdd() As String
        Get
            '+ Se indica aumentar o ambos
            If sChtypcap = "2" Or sChtypcap = "4" Then
                sChtypcapAdd = "1"
            Else sChtypcapAdd = ""
            End If
        End Get
    End Property
	
	'%sChtypcapSub: Propiedad que indica si se marca el check de disminuir el capital de acuerdo
	'%              al valor del campo sChtypcap
	Public ReadOnly Property sChtypcapSub() As String
		Get
            '+ Se indica disminuir o ambos
            If sChtypcap = "3" Or sChtypcap = "4" Then
                sChtypcapSub = "1"
            Else sChtypcapSub = ""
            End If
		End Get
	End Property
	
	'%sChangetypAdd: Propiedad que indica si se marca el check de aumentar la prima de acuerdo
	'%               al valor del campo sChangetyp
	Public ReadOnly Property sChangetypAdd() As String
		Get
            '+ Se indica aumentar o ambos
            If sChangetyp = "2" Or sChangetyp = "4" Then
                sChangetypAdd = "1"
            Else sChangetypAdd = ""
            End If
		End Get
	End Property
	
	'%sChangetypSub: Propiedad que indica si se marca el check de disminuir la prima de acuerdo
	'%               al valor del campo sChangetyp
	Public ReadOnly Property sChangetypSub() As String
		Get
            '+ Se indica disminuir o ambos
            If sChangetyp = "3" Or sChangetyp = "4" Then
                sChangetypSub = "1"
            Else sChangetypSub = ""
            End If
		End Get
	End Property
	
	'%sFDChantypAdd: Propiedad que indica si se marca el check de aumentar la franq/deduc. de
	'%               acuerdo al valor del campo sFDChantyp
	Public ReadOnly Property sFDChantypAdd() As String
		Get
            '+ Se indica aumentar o ambos
            If sFDChantyp = "2" Or sFDChantyp = "4" Then
                sFDChantypAdd = "1"
            Else sFDChantypAdd = ""
            End If
		End Get
	End Property
	
	'%sFDChantypSub: Propiedad que indica si se marca el check de disminuir la franq/deduc. de
	'%               acuerdo al valor del campo sFDChantyp
	Public ReadOnly Property sFDChantypSub() As String
		Get
            '+ Se indica disminuir o ambos
            If sFDChantyp = "3" Or sFDChantyp = "4" Then
                sFDChantypSub = "1"
            Else sFDChantypSub = ""
            End If
		End Get
	End Property
	
	'%InsValSequence: Función que valida que no existan ventanas requeridas sin contenido
	'%                dentro de la secuencia de Asegurados por coberturas
	Public Function insValSequence(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date) As Boolean
		insValSequence = True
		If Find(nBranch, nProduct, nModulec, nCover, nRole, dEffecdate) Then
			If sCacaltyp = String.Empty Or (nCover_in = eRemoteDB.Constants.intNull And sRoupremi = String.Empty And nPremirat = eRemoteDB.Constants.intNull And nId_table = eRemoteDB.Constants.intNull and nPremifix  <=0) Or nTypdurins = eRemoteDB.Constants.intNull Then
				insValSequence = False
			End If
		End If
	End Function
	
	'%InsFinishSequence: Función que finaliza la secuencia de Asegurados por coberturas
	Public Function InsFinishSequence(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nusercode As Integer) As Boolean
		If Find(nBranch, nProduct, nModulec, nCover, nRole, dEffecdate) Then
			InsFinishSequence = True
			If sStatregt = "2" Then
				sStatregt = "1"
				Me.nusercode = nusercode
				InsFinishSequence = Update
			End If
		End If
	End Function
	
	'%Class_Initialize: Inicializa las variables publicas de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nRolcap = eRemoteDB.Constants.intNull
		nRole = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		sRequired = String.Empty
		sDefaulti = String.Empty
		sRoupremi = String.Empty
		nAgemininsm = eRemoteDB.Constants.intNull
		nAgemaxinsm = eRemoteDB.Constants.intNull
		nAgemaxperm = eRemoteDB.Constants.intNull
		nAgemininsf = eRemoteDB.Constants.intNull
		nAgemaxinsf = eRemoteDB.Constants.intNull
		nAgemaxperf = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nusercode = eRemoteDB.Constants.intNull
		nCacalcov = eRemoteDB.Constants.intNull
		nCacalfix = eRemoteDB.Constants.intNull
		nCacalmul = eRemoteDB.Constants.intNull
		nCapbaspe = eRemoteDB.Constants.intNull
		nCapmaxim = eRemoteDB.Constants.intNull
		nCapminim = eRemoteDB.Constants.intNull
		nCover_in = eRemoteDB.Constants.intNull
		nRolprem = eRemoteDB.Constants.intNull
		nPremirat = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		nDuratInd = eRemoteDB.Constants.intNull
		sRechapri = String.Empty
		sRenewali = String.Empty
		sRouchaca = String.Empty
		sRouchapr = String.Empty
		nDuratPay = eRemoteDB.Constants.intNull
		sRevIndex = String.Empty
		sRouprcal = String.Empty
		nFrancFix = eRemoteDB.Constants.intNull
		sFrancApl = String.Empty
		nFrancMax = eRemoteDB.Constants.intNull
		nFrancMin = eRemoteDB.Constants.intNull
		nFrancrat = eRemoteDB.Constants.intNull
		sRoufranc = String.Empty
		sFrantype = String.Empty
		sFDRequire = String.Empty
		sFDChantyp = String.Empty
		nFDUserLev = eRemoteDB.Constants.intNull
		nFDRateAdd = eRemoteDB.Constants.intNull
		nFDRateSub = eRemoteDB.Constants.intNull
		nCamaxper = eRemoteDB.Constants.intNull
		nCamaxcov = eRemoteDB.Constants.intNull
		sRoutineCC = String.Empty
		nRateCC = eRemoteDB.Constants.intNull
		nAmountCC = eRemoteDB.Constants.intNull
		sApplyCC = String.Empty
		nChPreLev = eRemoteDB.Constants.intNull
		nChCapLev = eRemoteDB.Constants.intNull
		nRateCapAdd = eRemoteDB.Constants.intNull
		nRateCapSub = eRemoteDB.Constants.intNull
		sChtypcap = String.Empty
		nRatePreAdd = eRemoteDB.Constants.intNull
		nRatePreSub = eRemoteDB.Constants.intNull
		sChangetyp = String.Empty
		sStatregt = String.Empty
		sRout_pay = String.Empty
		nTypdurpay = eRemoteDB.Constants.intNull
		sCaren_type = String.Empty
		nTypdurins = eRemoteDB.Constants.intNull
		nCaren_quan = eRemoteDB.Constants.intNull
		sClaccidi = String.Empty
		sCldeathi = String.Empty
		sClincapi = String.Empty
		sClinvali = String.Empty
		sClvehaci = String.Empty
		sCliIllness = String.Empty
		sCacaltyp = String.Empty
		sClsurvii = String.Empty
		nCamaxrol = eRemoteDB.Constants.intNull
		nMax_role = eRemoteDB.Constants.intNull
		nMaxrent = eRemoteDB.Constants.intNull
		nPremifix = eRemoteDB.Constants.intNull
		nRolActiv_rel = eRemoteDB.Constants.intNull
		nCovActiv_rel = eRemoteDB.Constants.intNull
		nId_table = eRemoteDB.Constants.intNull
		nPercCostFP = eRemoteDB.Constants.intNull
		nRecCostFP = eRemoteDB.Constants.intNull
        sRourate = String.Empty
    End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






