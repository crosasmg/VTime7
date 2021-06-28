Option Strict Off
Option Explicit On
Public Class Product
	'%-------------------------------------------------------%'
	'% $Workfile:: Product.cls                              $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 2-09-09 19:47                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Public Enum pmBrancht
		pmlife = 1
		pmNotTraditionalLife = 2
		pmAuto = 3
		pmGenerals = 4
		'+ Combinado(generales y vida)
		pmMixed = 5
		pmSegurosProvisionales = 6
		pmMedicalAtention = 7
		pmTransporte = 8
	End Enum
	
	Public Enum pmStatregt
		pmActivo = 1
		pmEnProcesoDeInstalacion = 2
		pmAccesoRestringido = 3
	End Enum
	
	Public Enum pmRealind
		pmReal = 1
		pmSimulacion = 2
	End Enum
	
	'- Variables definidas para llevar el control de un producto ya leido
	Dim nBranch1 As Integer
	Dim nProduct1 As Integer
	Public dEffecDate1 As Date
	Public dEffecDate_pro As Date
	Public dEffecdateProduct_li As Date
	'local variable to hold collection
	Private mCol As Collection
	'- Propiedades basadas en la definición de la tabla "Product" al 22/06/99
	' Column_name                  Type                Length   Prec  Scale Nullable   TrimTrailingBlanks                  FixedLenNullInSource
	'-------------------------- ---------------------  -------- ----- ----- --------- ----------------------------------- -----------------------------------
	Public nBranch As Integer 'smallint    2      5     0     no               (n/a)                               (n/a)
	Public nProduct As Integer 'smallint    2      5     0     no               (n/a)                               (n/a)
	Public dEffecdate As Date 'datetime    8                  no               (n/a)                               (n/a)
	Public nCancnoti As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public sColinvot As String 'char        1                  yes              yes                                 yes
	Public nClaim_pres As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public nClaim_Notice As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public nClaim_Pay As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public nCopies As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public sCumreint As String 'char        1                  yes              yes                                 yes
	Public nQ_certif As Integer 'int         4      10    0     yes              (n/a)                               (n/a)
	Public sCumultyp As String 'char        1                  yes              yes                                 yes
	Public sDeclaaut As String 'char        1                  yes              yes                                 yes
	Public sTyp_clause As String 'char        1                  yes              yes                                 yes
	Public sTyp_discxp As String 'char        1                  yes              yes                                 yes
	Public sTyp_module As String 'char        1                  yes              yes                                 yes
	Public nDuration As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public sGroupind As String 'char        1                  yes              yes                                 yes
	Public sIndivind As String 'char        1                  yes              yes                                 yes
	Public sMultiind As String 'char        1                  yes              yes                                 yes
	Public dNulldate As Date 'datetime    8                  yes              (n/a)                               (n/a)
	Public nPayFreq As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public sPolitype As String 'char        1                  yes              yes                                 yes
	Public nQmaxcurr As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public sReintype As String 'char        1                  yes              yes                                 yes
	Public sRenewal As String 'char        1                  yes              yes                                 yes
	Public sRevalapl As String 'char        1                  yes              yes                                 yes
	Public nRevalrat As Double 'decimal     4      5     2     yes              (n/a)                               (n/a)
	Public sRevaltyp As String 'char        1                  yes              yes                                 yes
	Public sStyle_comm As String 'char        1                  yes              yes                                 yes
	Public nRehabperiod_aut As Integer 'smallint
	Public sStyle_prem As String 'char        1                  yes              yes                                 yes
	Public sStyle_tax As String 'char        1                  yes              yes                                 yes
	Public nTariff As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public sTimeren As String 'char        1                  yes              yes                                 yes
	Public nUsercode As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public sWin_declar As String 'char        8                  yes              yes                                 yes
	Public nInsmaxiq As Double 'int         4      10    0     yes              (n/a)                               (n/a)
	Public nInsminiq As Double 'int         4      10    0     yes              (n/a)                               (n/a)
	Public sHolder As String 'char        1                  yes              yes                                 yes
	Public nQdays_pro As Integer 'number      22     5     0     yes
	Public nQuota As Integer 'smallint    2      5     0     yes              (n/a)                               (n/a)
	Public sGroupsi As String 'char        1                  yes              yes                                 yes
	Public nWay_pay As Integer 'smallint
	Public nBill_day As Integer 'smallint
	Public nRehabperiod As Integer 'smallint
	Public sTyp_dom As String 'char        1                  yes              yes                                 yes
	Public sLeg As String 'char        1                  yes              yes                                 yes
	Public sReinst As String 'char        1                  yes              yes                                 yes
	Public sDatecoll As String 'char        1                  yes              yes                                 yes
	Public sFirst_pay As String 'char        1                  yes              yes                                 yes
	Public nQdays_quo As Integer 'number      22     0     5     yes
	Public nMonth_surr As Integer 'number      22     0     5     yes
	Public nNotCancelDay As Integer 'number      5
	Public nRepInsured As Integer 'number      5
	Public sNumprop As String 'char        1                  yes              yes                                 yes
	Public sCondSVS As String 'char       30
	Public nQDays_DifQuo As Integer
	Public nDay_bmg As Integer 'number      5
	Public nAge_bmg As Integer 'number      5
	Public nYear_bmg As Integer 'number      5
	Public sApv As String
	Public nCostRe As Double
	Public sRetarif As String
	Public sSetprem As String 'char        1
	Public nMonth_Setpr As Integer 'number      3
	Public sRecSec As String
	Public sMassive As String
	Public sTarQuo_Ind As String
	Public nPayable As Integer
	Public nAdvance As Integer
	Public sRoutineSurr As String
    Public sApplyRouSurr As String
    Public sResolutionSBS As String
    
    Public sAssociated_Policy_Required As String
    Public nAssociatedBranch As Integer

    Public nTypeAccount As Integer
	
	'- Propiedades basadas en la definición de la tabla "ProdMaster" al 22/06/99
	Public sDescript As String 'char        30                 yes               yes                                 yes
	Public sShort_des As String 'char        12                 yes               yes                                 yes
	Public sStatregt As pmStatregt 'char        1                  yes               yes                                 yes
	Public sBrancht As pmBrancht 'char        1                  no                yes                                 no
	Public sRealind As pmRealind 'char        1                  yes               yes                                 yes
    Public nCompany As Integer 'smallint    2      5     0     yes               (n/a)                               (n/a)

    '- Prefijo agregado para la numeración de pólizas corpvida
    Public sPreffix As String
    '- Rutina para mostrar mensajes de advertencias en generacion de cargos
    Public sRou_warning_charg As String

    Public sRou_cover As String
    Public nCurr_receipt As String
	
	'- Propiedades basadas en la definición de la tabla "Process" al 15/11/2000
	Public nReference As Integer 'int         4      10    0     no                (n/a)                               (n/a)
	Public nCode_activ As Integer 'smallint    2      5     0     no                (n/a)                               (n/a)
	Public nCode_proce As Integer 'smallint    2      5     0     no                (n/a)                               (n/a)
	Public sKey_process As String 'char        12                 no                no                                  no
	Public sCodispl As String 'char        8                  yes               no                                  yes
	Public sStartHour As String 'char        8                  yes               no                                  yes
	Public nStatus_pro As Integer 'smallint    2      5     0     yes               (n/a)                               (n/a)
	Public sRoutaut_r As String ' CHAR       12   0     0    S
	'- Propiedades según la tabla en el sistema 24/11/1999
    '- Product_li "Características de los productos de vida".

    Public sAutomaticBill As String
	
	' Column_name                  Type                   Computed   Length     Prec  Scale Nullable   TrimTrailingBlanks                  FixedLenNullInSource
	'---------------------------- ---------------------  ---------- ----------- ----- ----- --------- ----------------------------------- -----------------------------------
	Public sAccounti As String 'char         no           1                      yes      yes                                 yes
	Public nAnlifint As Integer
	Public nAnnualap As Double 'decimal      no           9          12    0     yes      (n/a)                               (n/a)
	Public sAssociai As String 'char         no           1                      yes      yes                                 yes
	Public nBenefapl As Integer
	Public nBenefexc As Double 'decimal      no           9          12    0     yes      (n/a)                               (n/a)
	Public nBenefitr As Double 'decimal      no           5          4     2     yes      (n/a)                               (n/a)
	Public nBenexcra As Double 'decimal      no           5          4     2     yes      (n/a)                               (n/a)
	Public nCharge As Double 'decimal      no           5          4     2     yes      (n/a)                               (n/a)
	Public sClallpre As String 'char         no           1                      yes      yes                                 yes
	Public sClannpei As String 'char         no           12                     yes      yes                                 yes
	Public sCllifeai As String 'char         no           12                     yes      yes                                 yes
	Public sClnoprei As String 'char         no           1                      yes      yes                                 yes
	Public sClpaypri As String 'char         no           1                      yes      yes                                 yes
	Public sClsimpai As String 'char         no           1                      yes      yes                                 yes
	Public sClsurrei As String 'char         no           1                      yes      yes                                 yes
	Public sCltransi As String 'char         no           1                      yes      yes                                 yes
	Public nCurrency As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nDedufreq As Integer
	Public nEntrance As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nSurrfreq As Double
	Public nIdurafix As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nTypdurins As Integer 'smallint     no           2          5     0     yes      yes                                 yes
	Public sIdurvari As String 'char         no           1                      yes      yes                                 yes
	Public nInterest As Double 'decimal      no           5          4     2     yes      (n/a)                               (n/a)
	Public sMethagav As String 'char         no           1                      yes      yes                                 yes
	Public sMethprav As String 'char         no           1                      yes      yes                                 yes
	Public sMethprin As String 'char         no           1                      yes      yes                                 yes
	Public sMorcapii As String 'char         no           1                      yes      yes                                 yes
	Public sNoperiod As String 'char         no           1                      yes      yes                                 yes
	Public nNpemulti As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nNpenumai As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nNpenunmi As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public sNpeunifa As String 'char         no           1                      yes      yes                                 yes
	Public nPayiniti As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nPayinter As Integer
	Public nPdurafix As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public sPdurvari As String 'char         no           1                      yes      yes                                 yes
	Public sPeriodic As String 'char         no           1                      yes      yes                                 yes
	Public nPermulti As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nPernopay As Double 'decimal      no           5          4     2     yes      (n/a)                               (n/a)
	Public nPernumai As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nPernunmi As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nPerrevfa As Double 'decimal      no           5          4     2     yes      (n/a)                               (n/a)
	Public sPerunifa As String 'char         no           1                      yes      yes                                 yes
	Public nProdClas As Integer 'smallint     no           2         10     0     yes      (n/a)                               (n/a)
	Public nQbonusma As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nQbonusmi As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nReagemax As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public sRouadvan As String 'char         no           12                     yes      yes                                 yes
	Public sRoureddc As String 'char         no           12                     yes      yes                                 yes
	Public sRoureduc As String 'char         no           12                     yes      yes                                 yes
	Public sRousurre As String 'char         no           12                     yes      yes                                 yes
	Public nSuagemax As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nSuagemin As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nSurcashv As Double 'decimal      no           9          12    0     yes      (n/a)                               (n/a)
	Public sSurrenpi As String 'char         no           1                      yes      yes                                 yes
	Public sSurrenti As String 'char         no           1                      yes      yes                                 yes
	Public sUlfchani As String 'char         no           1                      yes      yes                                 yes
	Public nUlfmaxqu As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nUlrcharg As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nUlredper As Integer
	Public nUlrmaxqu As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nUlrschar As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nUlscharg As Double 'decimal      no           9          10    2     yes      (n/a)                               (n/a)
	Public nUlsmaxqu As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public sAssototal As String 'char         no           1                      yes      yes                                 yes
	Public nUlsschar As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public sBenres As String 'char         no           1                      yes      yes                                 yes
	Public nUlswiper As Integer
	Public nValuebon As Double 'decimal      no           9          12    0     yes      (n/a)                               (n/a)
	Public nYearmins As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nYearminw As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public nYearmors As Integer 'smallint     no           2          5     0     yes      (n/a)                               (n/a)
	Public sPremiumtype As String 'char         no           1                      yes      no                                  yes
	Public nTaxsmoke As Double 'decimal      no           5          4     2     yes      (n/a)                               (n/a)
	Public nTaxnsmoke As Double 'decimal      no           5          4     2     yes      (n/a)                               (n/a)
	Public nAdminrate As Double
	Public nChargeamo As Double
	Public nMonthamo As Double
	Public nMinrent As Double
	Public nMaxrent As Double
	Public nQmeploans As Integer 'number                    22   0     5    S
	Public nQmmloans As Integer 'number                    22   0     5    S
	Public nQmyloans As Integer 'number                    22   0     5    S
	Public nAminloans As Double 'number                    22   2     10   S
	Public nAmaxloans As Double 'number                    22   2     10   S
	Public nPervsloans As Single 'number                    22   2     5    S
	Public nPerctol As Single 'number                    22   2     5    S
	Public nTaxes As Single 'number                    22   2     5    S
	Public sRouinterest As String 'char                      12   0     0    S
	Public nBill_item As Integer 'number                    22   0     5    S
	Public nQmepsurr As Integer 'number                    5   0     5    S
	Public nQmmsurr As Integer 'number                    5   0     5    S
	Public nQmysurr As Integer 'number                    5   0     5    S
	Public nAmaxsurr As Double 'number                    5   0     5    S
	Public nAminsurr As Double 'number                    5   0     5    S
	Public nPervssurr As Double 'number                    5   0     5    S
	Public nCapminsurr As Double 'number                    5   0     5    S
	Public nBmg As Integer
	Public sRoutinevpn As String
	Public nQMMPSurr As Integer 'number                    5   0     5    S
	Public nBalminsurr As Double 'number                    5   0     5    S
	
	Public nPremmin As Double 'number                    22   2     10   S
	Public nQmonVPN As Integer 'number                    22   0     5    S
	Public nQmonToVPN As Integer 'number                    22   0     5    S
	Public nRateReh As Single 'number                    22   3     6    S
	Public sRoutine_C As String 'Char
	Public sRoutinsu As String 'Char
	Public nTypdurpay As Integer
	Public sRoutpay As String
	Public sFracReceip As String
	Public sAuto_susc As String
	Public sIndCl_Pay As String
    '- Nuevo campo
    Public sNo_Holidays As String
    Public nType_Rateproy As String
	Public blnError As Boolean
	
	'- Variables auxiliares
	Public nAmelevel As Integer
	Public sCodisp As String
	Public nImg_index As Integer
	Public sCodmen As String
	Public sDescript_win As String
	Public sDirectgo As String
	Public nG_identi As Integer
	Public nInqlevel As Integer
	Public nModules As Integer
	Public sPseudo As String
	Public Nsequence As Integer
	Public sShort_des_win As String
	Public sStatregt_win As String
	Public nWindowty As Integer
	Public nPolicy As Integer
	Public bCumreintDisabled As Boolean
	
	
	'- Propiedades utilizadas en DP036 (Elementos de Protección 17/04/2001)
	Public nElement As Integer
	Public nDiscount As Double
	Public nDismaxim As Double
	Public nDisminin As Double
	Public nDisrate As Double
	Public sRoutine As String
	
	'- Se definen las propiedades auxiliares utilizadas en DP002.
	Public nStatusInstance As Integer
	
	
	'- Propiedades utilizadas en la VI001 para establecer los valores iniciales
	'- del los campos de la página.
	'new
	Public nInsur_Time As Integer
	Public bInsur_time As Boolean
	Public bPay_time As Boolean
	'new
	
	Public nInsurTimeAgeLimit As Integer
	Public nInsurTimeAge As Integer
	Public sInsurTimeRoutine As String
	
	Public dexpirdat As Date
	Public nrentamount As Double
	Public ncurrrent As Integer
	Public ncount_insu As Integer
	Public nperc_cap As Double
	
	Public nInsurPayTimeAgeLimit As Integer
	Public nInsurPayTimeAge As Integer
	Public nAgeLimit As Integer
	Public nAge As Integer
	Public nAge_reinsu As Integer
	Public nPremium_ca As Double
	Public nCapital_ca As Double
	Public nXprem_time As Integer
	Public bInsurTimeAge As Boolean
	Public bInsurTimeAgeLimit As Boolean
	
	Public bexpirdat As Boolean
	Public brentamount As Boolean
	Public bcurrrent As Boolean
	Public bcount_insu As Boolean
	Public bperc_cap As Boolean
	
	Public bInsurPayTimeAge As Boolean
	Public bInsurPayTimeAgeLimit As Boolean
	Public sBranch_Rent As String
	
	Public nGroup As Integer
	Public nSituation As Integer
	
	'- Variables para establecer el estado de los campos de la duración de los pagos del seguro
	'- cuando se emite la póliza
	Public bYearpay As Boolean
	Public bAgepay As Boolean
	Public bExpirdatpay As Boolean
	
	'- Variables para establecer el valor de los campos de la duración de los pagos del seguro
	'- cuando se emite la póliza
	Public dDate_pay As Date
	Public nPay_time As Integer
	Public nAgepay_time As Integer
	
	'- [APV2] Inclusión de la ventana Reglas de capitalización (DP7001) DBLANCO 11-08-2003
	'- Propiedades correspondientres a los campos de la ventana de Reglas de
	'- Capitalización
	Public nSaving_pct As Integer
	Public nIndex_table As Integer
	Public nWarrn_table As Integer
	Public sS_allwchng As String
	Public sIx_allwchng As String
	Public sW_allwchng As String
	Public nInfType As Integer
	
	'- Propiedades correspondientes a los campos de la ventana de Interes asegurable
	Public nSaving_pct_L As Integer
	Public nIndex_table_L As Integer
	Public nWarrn_table_L As Integer
	Public nDisc_save_pct_L As Integer
	Public nDisc_unit_pct_L As Integer
	Public bSaving_pct_L As Boolean
	Public bIndex_table_L As Boolean
	Public bWarrn_table_L As Boolean
	Public bDisc_save_pct_L As Boolean
	Public bDisc_unit_pct_L As Boolean
	Public dStartdate As Date
	Public sCurrency As String
	Public nOption As Integer
	Public bOption As Boolean
	Public nPremdeal As Double
	Public nPremdeal_anu As Double
	Public nIntwarr As Double
	Public bIntwarr As Boolean
	Public bPremdeal As Boolean
	Public nRatepayf As Double
	Public nUlsmin As Double
	
	Public nOrigin_surr As Integer
	Public nOrigin_loan As Integer
	Public nULmmsw As Integer
	Public nULmmrd As Integer
	Public nULswmaxper As Integer
	Public nULrdmaxper As Integer
	Public nULSwmqt As Integer
	Public nULrdmqt As Integer
	Public nULswmqtper As Integer
	Public nULrdmqtper As Integer
	Public nULswchPerc As Double
	Public nUlrdchperc As Double
	Public nDayBuyUnit As Integer
	Public nMaxchargsurr As Double
	Public nDayIssue As Integer
	Public nPerc_secur As Double
	Public sReactivation As String
	Public nReactPeriod As Short
	Public nReactPeriod_Aut As Short
	Public sRoutReact As String
	Public sAccount_mirror As String
	Public nwarrn_table_mirror As Short
	Public nChUserLev As Short
    Public sRatingServiceUsing As String 
    Public nModuleMin As Integer
	
	' Variables Privadas
	'
	
	'- Variable para comparar la fecha de efecto del producto con el parametro de entrada
	'- para evitar una segunda lectura de product_li
	Private mdtmDate As Date
	
	Private mclsProduct As eProduct.Product
	Private mclsCliallopro As eProduct.Cliallopro
	Private mblnProduct_liExist As Boolean
	Private mstrSelected As String
	Private mstrInit_sel As String
	Private mintExist As Integer
	
	Private Structure udtCustomers
		Dim sSel As String
		Dim sPolitype As String
		Dim sCompon As String
		Dim nCodigInt As Integer
		Dim sDescript As String
		Dim sRequired As String
		Dim sDefaulti As String
		Dim nMax_role As Integer
		Dim nSelected As Integer
		Dim nRol As Integer
	End Structure
	
	Private arrCustomersAllowed() As udtCustomers
	
	Private Structure udtProtection
		Dim nBranch As Integer
		Dim nElement As Integer
		Dim nProduct As Integer
		Dim dEffecdate As Date
		Dim nCurrency As Integer
		Dim sDescript As String
		Dim nDiscount As Double
		Dim nDismaxim As Double
		Dim nDisminin As Double
		Dim nDisrate As Double
		Dim dNulldate As Date
		Dim sShort_des As String
		Dim sStatregt As String
		Dim nUsercode As Integer
		Dim sRoutine As String
	End Structure
	
	Private arrProtectionElements() As udtProtection
	
	'**%Objective: The initial values of page VI7001 are initialize.
	'**%Parameters:
	'**%    sCertype   - Type or Record. Sole values:     1-  Proposal     2 - Policy     3 - Quotation
	'**%    nBranch    - Code of the Line of Business. The possible values as per table 10.
	'**%    nProduct   - Code of the product.
	'**%    nPolicy    - Number identifying the policy/ quotation/ proposal
	'**%    nCertif    - Number identifying the Certificate
	'**%    dEffecdate - Date which from the record is valid.
	'%Objetivo: Se establecen los valores iniciales de la página VI001.
	'%Parámetros:
	'%      sCertype   - Tipo de registro. Valores únicos:    1 - Solicitud    2 - Póliza    3 - Cotización
	'%      nBranch    - Código del ramo comercial. Valores posibles según tabla 10.
	'%      nProduct   - Código del producto.
	'%      nPolicy    - Número identificativo de la póliza/ cotización/ solicitud
	'%      nCertif    - Número identificativo del certificado
	'%      dEffecdate - Fecha de efecto del registro.
	Public Function insInitialVI7001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Boolean
		Dim lclsCertificat As Object
		Dim lclsLife As Object
		Dim lclsProduct As eProduct.Product
		
		
		
		
		'-  Variable que determinara si se deberan mostrar los valores del diseñador
		'- o los asociados a la póliza en tratamiento
		Dim bGetProduct As Boolean
		
		On Error GoTo insInitialVI7001_err
		
		lclsLife = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Life")
		lclsProduct = New eProduct.Product
		
		'+ Obtiene los datos de la póliza
		Call FindVI7001(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTransaction)
		
		If lclsLife.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, True) Then
			If lclsLife.insReaAge(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, True) Then
				insInitialVI7001 = True
				
				With lclsProduct
					
					'+ Se obtienen los datos de vida del producto
					
					Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate, True)
					
					'+ Se transfieren los valores desde la clase Life a las variables públicas de eProduct
					nPremium_ca = IIf(lclsLife.nPremium_ca = eRemoteDB.Constants.intNull, 0, lclsLife.nPremium_ca)
					nXprem_time = IIf(lclsLife.nXprem_time = eRemoteDB.Constants.intNull, 0, lclsLife.nXprem_time)
					Me.nCapital_ca = IIf(Me.nCapital_ca = eRemoteDB.Constants.intNull, lclsLife.nCapital_ca, Me.nCapital_ca)
					Me.nPermulti = IIf(Me.nPermulti = eRemoteDB.Constants.intNull, 0, Me.nPermulti)
					Me.nProdClas = .nProdClas
					Me.nTypdurins = IIf(.nTypdurins = eRemoteDB.Constants.intNull, 1, .nTypdurins)
					Me.nAgeLimit = IIf(lclsLife.nAge_Limit = eRemoteDB.Constants.intNull Or lclsLife.nAge_Limit = 0, lclsProduct.nSuagemax, lclsLife.nAge_Limit)
					Me.nTypdurpay = .nTypdurpay
					
					Me.nPremdeal = lclsLife.nPremdeal
					Me.nPremdeal_anu = lclsLife.nPremdeal_anu
					Me.nPremmin = lclsLife.nPremmin
					
					If lclsLife.nAge <> eRemoteDB.Constants.intNull Then
						With lclsLife
							
							'**+ The field age/real age is loaded
							'+ Se carga el campo Edad/edad real
							
							nAge = .nAge
							
							'**+ The field age/actuarial age is loaded
							'+ Se carga el campo Edad/edad actuarial
							
							nAge_reinsu = .nAge_reinsu
						End With
					End If
					
					Select Case Me.nTypdurins
						
						'+Si la duración del seguro es de forma abierta
						
						Case 5 : nInsurTimeAgeLimit = 99
							
							'+Si la duración del seguro es por años
							
						Case 2
							nInsurTimeAge = IIf(.nIdurafix = eRemoteDB.Constants.intNull, lclsLife.nInsur_Time, .nIdurafix)
							
							'+Si es variable; es decir el usuario puede modificar el contenido del campo.
							'+Si la duración del seguro es por edad alcanzada
							
						Case 1
							nInsurTimeAgeLimit = IIf(.nIdurafix = eRemoteDB.Constants.intNull, lclsLife.nInsur_Time, .nIdurafix)
							
							'+Si es variable; es decir el usuario puede modificar el contenido del campo.
							'+Si la duración del seguro es Libre
							
						Case 6 : lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
							Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True)
							dexpirdat = lclsCertificat.dexpirdat
							nInsurTimeAge = IIf(nIdurafix = eRemoteDB.Constants.intNull, lclsLife.nInsur_Time, nIdurafix)
							
							'+Si la duración del seguro es por rutina se llama a la rutina para obtener el número de años
							
						Case 4 : sInsurTimeRoutine = .sRoutinsu
							Call insRoutineDuration(sCertype, nBranch, nProduct, nPolicy, nCertif, Me.dStartdate, dEffecdate, .sRoutinsu, True)
							nInsurTimeAge = IIf(nIdurafix = eRemoteDB.Constants.intNull, lclsLife.nInsur_Time, nIdurafix)
					End Select
					
					'+ PROD00068530 CC.
					Select Case Me.nTypdurpay
						'+Si la duración de los pagos es por edad alcanzada
						Case 1
							nPay_time = IIf(.nPdurafix = eRemoteDB.Constants.intNull, lclsLife.nPay_time, .nPdurafix)
							'nPay_time = lclsLife.nPay_time
							'+Si la duración de los pagos es por años
						Case 2, 8, 9
							nPay_time = IIf(.nPdurafix = eRemoteDB.Constants.intNull, lclsLife.nPay_time, .nPdurafix)
							'nPay_time = lclsLife.nPay_time
							'+Si la duración de los pagos es por rutina se llama a la rutina para obtener el número de años
						Case 4 : sRoutpay = .sRoutpay
							Call insRoutineDuration(sCertype, nBranch, nProduct, nPolicy, nCertif, Me.dStartdate, dEffecdate, .sRoutpay, False)
							nPay_time = IIf(nPdurafix = eRemoteDB.Constants.intNull, lclsLife.nPay_time, nPdurafix)
							'+Si la duración de los pagos es abierta
						Case 5 : nPay_time = 99
							'dDate_pay = dtmNull
							'+Si la duración de los pagos es Libre
						Case 6 'dDate_pay = IIf(lclsLife.dDate_pay = dtmNull, lclsCertificat.dexpirdat, lclsLife.dDate_pay)
							nPay_time = 0
							'+Si la duración de los pagos es por años/edad alcanzada
						Case 7
							nPay_time = IIf(.nPdurafix = eRemoteDB.Constants.intNull, lclsLife.nPay_time, .nPdurafix)
					End Select
					
					
					'+ [APV2] HAD 1022. VI7001 - Interes Asegurable - Unit Linked DBLANCO 14-08-2003
					'+ La primera vez que se accede a la página (nDisc_save_pct=Null y nDisc_unit_pct=Null)
					'+ se muestran los valores cargados en el diseñador de producto.
					If lclsLife.nDisc_save_pct = eRemoteDB.Constants.intNull And lclsLife.nDisc_unit_pct = eRemoteDB.Constants.intNull Then
						bGetProduct = True
					End If
					
					If bGetProduct Then
						nSaving_pct_L = .nSaving_pct
						nIndex_table_L = .nIndex_table
						nWarrn_table_L = .nWarrn_table
					Else
						nSaving_pct_L = lclsLife.nSaving_pct
						nIndex_table_L = lclsLife.nIndex_table
						nWarrn_table_L = lclsLife.nWarrn_table
					End If
					
					bSaving_pct_L = IIf(.sS_allwchng = "1", False, True)
					
					'+ Los campos "Indice de capitalización" y "Tasa garantizada" y solo
					'+ se habilitan sólo si el campo "% de inversión en cuentas de  ahorro"
					'+ es mayor a 0 y el producto así lo indica
					
					bIndex_table_L = True
					bWarrn_table_L = True
					
					If nSaving_pct_L > 0 Then
						bIndex_table_L = IIf(.sIx_allwchng = "1", False, True)
						bWarrn_table_L = IIf(.sW_allwchng = "1", False, True)
					End If
					
					Select Case nSaving_pct_L
						
						'+ Si el porcentaje de participación en cuenta de ahorro es es cero, el sistema
						'+ muestra en el campo "% A descontar de cuenta de ahorro" el valor cero (0),
						'+ y el usuario no puede cambiar su contenido.
						'+ Si el porcentaje de participación en cuenta de ahorro es cero, el sistema
						'+ muestra en el campo % A desconar de cuenta de unidades el valor 100
						'+ y el usuario no puede cambiar su contenido.
						
						Case 0 : nDisc_save_pct_L = 0
							nDisc_unit_pct_L = 100
							bDisc_save_pct_L = True
							bDisc_unit_pct_L = True
							
							'+ Si el porcentaje de participación en cuenta de ahorro es 100, el sistema
							'+ muestra en el campo "% A descontar de cuenta de ahorro" el usuario no
							'+ puede cambiar su contenido.
							'+ Si el porcentaje de participación en cuenta de ahorro es 100, el sistema muestra
							'+ muestra en el campo % A desconar de cuenta de unidades el valor 0 y el usuario
							'+ no puede cambiar su contenido
							
						Case 100 : nDisc_save_pct_L = 100
							nDisc_unit_pct_L = 0
							bDisc_save_pct_L = True
							bDisc_unit_pct_L = True
						Case Else
							nDisc_save_pct_L = lclsLife.nDisc_save_pct
							nDisc_unit_pct_L = lclsLife.nDisc_unit_pct
					End Select
					
				End With
			End If
		End If
		
		If insInitialVI7001 Then
			With lclsProduct
				Select Case Me.nTypdurins
					
					'+Si la duración del seguro es de forma abierta
					
					Case 5
						bInsurTimeAge = True
						bInsurTimeAgeLimit = True
						bexpirdat = True
						
						'+Si la duración del seguro es por años
						
					Case 2
						
						'+Si es variable; es decir el usuario puede modificar el contenido del campo.
						
						If IIf(.sIdurvari = String.Empty, "2", .sIdurvari) = "1" Then
							bInsurTimeAge = False
						Else
							bInsurTimeAge = True
						End If
						
						bInsurTimeAgeLimit = True
						bexpirdat = True
						
						'+Si la duración del seguro es por edad alcanzada
						
					Case 1
						bInsurTimeAge = True
						
						'+Si es variable; es decir el usuario puede modificar el contenido del campo.
						
						If IIf(.sIdurvari = String.Empty, "2", .sIdurvari) = "1" Then
							bInsurTimeAgeLimit = False
						Else
							bInsurTimeAgeLimit = True
						End If
						
						bexpirdat = True
						
						'+si la duración del seguro es libre
						
					Case 6
						bInsurTimeAge = False
						bInsurTimeAgeLimit = True
						bexpirdat = True
				End Select
				
				
				Select Case Me.nTypdurpay
					'+Si la duración del seguro es por edad alcanzada
					Case 1 : bPay_time = True
						
						'+Si es variable; es decir el usuario puede modificar el contenido del campo.
						If IIf(.sPdurvari = String.Empty, "2", .sPdurvari) = "1" Then
							bPay_time = False
						Else
							bPay_time = True
						End If
						'+Si la duración del seguro es por años
					Case 2, 8, 9
						'+Si es variable; es decir el usuario puede modificar el contenido del campo.
						If IIf(.sPdurvari = String.Empty, "2", .sPdurvari) = "1" Then
							bPay_time = False
						Else
							bPay_time = True
						End If
						'+Si la duración del seguro es de forma abierta
					Case 5 : bPay_time = True
						
						'+ Si la duración del seguro es libre
					Case 6 : bPay_time = True
						
						'+ Si la duración del seguro es por años/edad alcanzada
					Case 7
						'+ Si es variable, se puede modificar el contenido del campo
						If IIf(.sPdurvari = String.Empty, "2", .sPdurvari) = "1" Then
							bPay_time = False
						Else
							bPay_time = True
						End If
				End Select
				
				Me.nTypdurins = IIf(lclsLife.nTypdurins = eRemoteDB.Constants.intNull, .nTypdurins, lclsLife.nTypdurins)
				Me.nTypdurpay = IIf(lclsLife.nTypdurpay = eRemoteDB.Constants.intNull, .nTypdurpay, lclsLife.nTypdurpay)
				
			End With
		End If
		
insInitialVI7001_err: 
		If Err.Number Then
			insInitialVI7001 = False
		End If
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		On Error GoTo 0
	End Function
	'% FindVI7001: Lee datos necesarios para transacción Inters asegurable (VI7001)
	Public Function FindVI7001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Boolean
		Dim lrecReaVi7001 As eRemoteDB.Execute
		
		On Error GoTo FindVI7001_Err
		
		lrecReaVi7001 = New eRemoteDB.Execute
		
		With lrecReaVi7001
			.StoredProcedure = "ReaVi7001"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.dStartdate = .FieldToClass("dStartdate")
				Me.nPayFreq = .FieldToClass("nPayFreq")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.sCurrency = .FieldToClass("sCurrency")
				Me.nIntwarr = .FieldToClass("nIntwarr")
				If .FieldToClass("sModif") = "1" Then
					Me.bIntwarr = True
				Else
					Me.bIntwarr = False
				End If
				Me.nOption = .FieldToClass("nOption")
				If .FieldToClass("sOption") = "1" Then
					Me.bOption = True
				Else
					Me.bOption = False
				End If
				Me.nCapital_ca = .FieldToClass("nCapital")
				Me.nModules = .FieldToClass("nModulec")
				If .FieldToClass("sPremdeal") = "1" Then
					bPremdeal = True
				Else
					bPremdeal = False
				End If
				Me.nRatepayf = .FieldToClass("nRatepayf")
				.RCloseRec()
				FindVI7001 = True
			End If
		End With
		
FindVI7001_Err: 
		If Err.Number Then
			FindVI7001 = False
		End If
		'UPGRADE_NOTE: Object lrecReaVi7001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaVi7001 = Nothing
		On Error GoTo 0
	End Function
	
	'% insInitialVI001 : Se establecen los valores iniciales de la página VI001
	Public Function insInitialVI001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsPolicy As Object
		Dim lclsCertificat As Object
		Dim lclsLife As Object
		Dim lclsProduct As eProduct.Product
		On Error GoTo insInitialVI001_err
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		lclsLife = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Life")
		lclsProduct = New eProduct.Product
		
		'+ Obtiene los datos de la póliza
		Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy, True)
		'+ Obtiene los datos del certificado
		Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True)
		If lclsLife.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, True) Then
			If Not lclsLife.insReaAge(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, True) Then
			Else
				With lclsProduct
					'+ Se obtienen los datos de vida del producto
					If .FindProduct_li(CShort(nBranch), CShort(nProduct), dEffecdate, True) Then
                        '+ Se transfieren los valores desde la clase Life a las variables públicas de eProduct
                        nPremium_ca = IIf(lclsLife.nPremium_ca = eRemoteDB.Constants.intNull, 0, lclsLife.nPremium_ca)
                        nXprem_time = IIf(lclsLife.nXprem_time = eRemoteDB.Constants.intNull, 0, lclsLife.nXprem_time)
                        nCapital_ca = IIf(lclsLife.nCapital_ca = eRemoteDB.Constants.intNull, 0, lclsLife.nCapital_ca)
                        Me.nPermulti = IIf(Me.nPermulti = eRemoteDB.Constants.intNull, 0, Me.nPermulti)
                        Me.nProdClas = .nProdClas
                        nrentamount = IIf(lclsLife.nrentamount = eRemoteDB.Constants.intNull, 0, lclsLife.nrentamount)
                        ncurrrent = IIf(lclsLife.ncurrrent = eRemoteDB.Constants.intNull, 0, lclsLife.ncurrrent)
                        ncount_insu = IIf(lclsLife.ncount_insu = eRemoteDB.Constants.intNull, 0, lclsLife.ncount_insu)
                        nperc_cap = IIf(lclsLife.nperc_cap = eRemoteDB.Constants.intNull, 0, lclsLife.nperc_cap)
                        Select Case .nTypdurins
                            '+Si la duración del seguro es por edad alcanzada
                            Case 1
                                nInsur_Time = IIf(.nIdurafix = eRemoteDB.Constants.intNull, lclsLife.nInsur_Time, .nIdurafix)
                                '+Si la duración del seguro es por años
                            Case 2, 8, 9
                                nInsur_Time = IIf(.nIdurafix = eRemoteDB.Constants.intNull, lclsLife.nInsur_Time, .nIdurafix)
                                '+Si la duración del seguro es por rutina se llama a la rutina para obtener el número de años
                            Case 4 : sInsurTimeRoutine = .sRoutinsu
                                Call insRoutineDuration(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsPolicy.dStartdate, dEffecdate, .sRoutinsu, True)
                                nInsur_Time = IIf(nIdurafix = eRemoteDB.Constants.intNull, lclsLife.nInsur_Time, nIdurafix)
                                '+Si la duración del seguro es de forma abierta
                            Case 5 : nInsur_Time = 99
                                '+Si la duración del seguro es Libre
                                nInsur_Time = eRemoteDB.Constants.intNull
                            Case 6
                                dexpirdat = IIf(lclsLife.dexpirdat = eRemoteDB.Constants.dtmNull, lclsCertificat.dexpirdat, lclsLife.dexpirdat)
                                nInsur_Time = 0
                                '+Si la duración del seguro es por años/edad alcanzada
                            Case 7
                                nInsur_Time = IIf(.nIdurafix = eRemoteDB.Constants.intNull, lclsLife.nInsur_Time, .nIdurafix)
                        End Select

                        Select Case .nTypdurpay
                            '+Si la duración de los pagos es por edad alcanzada
                            Case 1
                                nPay_time = IIf(.nPdurafix = eRemoteDB.Constants.intNull, lclsLife.nPay_time, .nPdurafix)
                                'nPay_time = lclsLife.nPay_time
                                '+Si la duración de los pagos es por años
                            Case 2, 8, 9
                                nPay_time = IIf(.nPdurafix = eRemoteDB.Constants.intNull, lclsLife.nPay_time, .nPdurafix)
                                'nPay_time = lclsLife.nPay_time
                                '+Si la duración de los pagos es por rutina se llama a la rutina para obtener el número de años
                            Case 4 : sRoutpay = .sRoutpay
                                Call insRoutineDuration(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsPolicy.dStartdate, dEffecdate, .sRoutpay, False)
                                nPay_time = IIf(nPdurafix = eRemoteDB.Constants.intNull, lclsLife.nPay_time, nPdurafix)
                                '+Si la duración de los pagos es abierta
                            Case 5 : nPay_time = 99
                                dDate_pay = eRemoteDB.Constants.dtmNull
                                nPay_time = eRemoteDB.Constants.intNull
                                '+Si la duración de los pagos es Libre
                            Case 6
                                dDate_pay = IIf(lclsLife.dDate_pay = eRemoteDB.Constants.dtmNull, lclsCertificat.dexpirdat, lclsLife.dDate_pay)
                                nPay_time = 0
                                '+Si la duración de los pagos es por años/edad alcanzada
                            Case 7
                                nPay_time = IIf(.nPdurafix = eRemoteDB.Constants.intNull, lclsLife.nPay_time, .nPdurafix)
                        End Select
                    End If
					insInitialVI001 = True
				End With
			End If
		End If
		If insInitialVI001 Then
			With lclsProduct
				Select Case .nTypdurins
					'+Si la duración del seguro es por edad alcanzada
					Case 1 : bInsur_time = True
						bexpirdat = True
						'+Si es variable; es decir el usuario puede modificar el contenido del campo.
						If IIf(.sIdurvari = String.Empty, "2", .sIdurvari) = "1" Then
							bInsur_time = False
						Else
							bInsur_time = True
						End If
						'+Si la duración del seguro es por años
					Case 2, 8, 9 : bexpirdat = True
						'+Si es variable; es decir el usuario puede modificar el contenido del campo.
						If IIf(.sIdurvari = String.Empty, "2", .sIdurvari) = "1" Then
							bInsur_time = False
						Else
							bInsur_time = True
						End If
						'+Si la duración del seguro es de forma abierta
					Case 5 : bInsur_time = True
						bexpirdat = True
						'+ Si la duración del seguro es libre
					Case 6 : bInsur_time = True
						bexpirdat = False
						'+ Si la duración del seguro es por años/edad alcanzada
					Case 7 : bexpirdat = True
						'+ Si es variable, se puede modificar el contenido del campo
						If IIf(.sIdurvari = String.Empty, "2", .sIdurvari) = "1" Then
							bInsur_time = False
						Else
							bInsur_time = True
						End If
				End Select
				
				Select Case .nTypdurpay
					'este codigo se deja igual que la validacion de seguros
					'+Si la duración de los pagos es por edad alcanzada, se habilita la edad
					'                Case 1: bPay_time = False
					'+Si la duración de los pagos es por años, se habilitan los años
					'                Case 2: bPay_time = False
					'+ Si la duración del seguro es libre, se habilita la fecha
					'                Case 6: bExpirdatpay = False
					'+Si la duración de los pagos es años/edad alcanzada, se habilita la edad
					'                Case 7: bPay_time = False
					
					'+Si la duración del seguro es por edad alcanzada
					Case 1 : bPay_time = True
						bExpirdatpay = True
						'+Si es variable; es decir el usuario puede modificar el contenido del campo.
						If IIf(.sPdurvari = String.Empty, "2", .sPdurvari) = "1" Then
							bPay_time = False
						Else
							bPay_time = True
						End If
						'+Si la duración del seguro es por años
					Case 2, 8, 9 : bExpirdatpay = True
						'+Si es variable; es decir el usuario puede modificar el contenido del campo.
						If IIf(.sPdurvari = String.Empty, "2", .sPdurvari) = "1" Then
							bPay_time = False
						Else
							bPay_time = True
						End If
						'+Si la duración del seguro es de forma abierta
					Case 5 : bPay_time = True
						bExpirdatpay = True
						'+ Si la duración del seguro es libre
					Case 6 : bPay_time = True
						bExpirdatpay = False
						'+ Si la duración del seguro es por años/edad alcanzada
					Case 7 : bExpirdatpay = True
						'+ Si es variable, se puede modificar el contenido del campo
						If IIf(.sPdurvari = String.Empty, "2", .sPdurvari) = "1" Then
							bPay_time = False
						Else
							bPay_time = True
						End If
				End Select
				Me.nTypdurins = IIf(lclsLife.nTypdurins = eRemoteDB.Constants.intNull, .nTypdurins, lclsLife.nTypdurins)
				Me.nTypdurpay = lclsLife.nTypdurpay
				Me.nGroup = lclsLife.nGroup
				Me.nSituation = lclsLife.nSituation
			End With
		End If
		
insInitialVI001_err: 
		If Err.Number Then
			insInitialVI001 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'% insExecuteDP999: Ejecuta toda la secuencia del post de la DP999
	Public Function insExecuteDP999(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sStatus As pmStatregt) As Boolean
		insExecuteDP999 = False
		If FindProdMasterActive(nBranch, nProduct) Then
			insExecuteDP999 = True
			If sStatregt <> sStatus Then
				sStatregt = sStatus
				Call UpdateProdmaster()
			End If
		End If
	End Function
	
	'* CreationDate: Busca la fecha de creación del producto
	Public ReadOnly Property CreationDate() As Date
		Get
			Dim lrecreaProduct_CreaDate As eRemoteDB.Execute
			
			lrecreaProduct_CreaDate = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaProduct_CreaDate'
			'+ Información leída el 26/01/1999 09:49:43 AM
			
			With lrecreaProduct_CreaDate
				.StoredProcedure = "reaProduct_CreaDate"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					CreationDate = .FieldToClass("dEffecdate")
					.RCloseRec()
				Else
					CreationDate = eRemoteDB.Constants.dtmNull
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaProduct_CreaDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaProduct_CreaDate = Nothing
		End Get
	End Property
	
	'%CountItemDP036: propiedad que indica el número de registros que se encuentra en determinado momento en el arreglo de la clase
	Public ReadOnly Property CountItemDP036() As Integer
		Get
			CountItemDP036 = UBound(arrProtectionElements)
		End Get
	End Property
	
	'% Find : Lee las características del producto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByRef bFind As Boolean = False) As Boolean
		Dim lrecReaProduct As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Static lblnRead As Boolean
		
		If nBranch1 <> nBranch Or nProduct1 <> nProduct Or dEffecDate1 <> dEffecdate Or bFind Then
			
			nBranch1 = nBranch
			nProduct1 = nProduct
			dEffecDate1 = dEffecdate
			
			lrecReaProduct = New eRemoteDB.Execute
			
			With lrecReaProduct
                .StoredProcedure = "reaProductGeneral"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					dEffecDate_pro = .FieldToClass("dEffecdate")
					nCancnoti = .FieldToClass("nCancnoti")
					sColinvot = .FieldToClass("sColinvot")
					nClaim_pres = .FieldToClass("nClaim_pres")
					nCopies = .FieldToClass("nCopies")
					sCumreint = .FieldToClass("sCumreint")
					nQ_certif = .FieldToClass("nQ_certif")
					sCumultyp = .FieldToClass("sCumultyp")
					sDeclaaut = .FieldToClass("sDeclaaut")
					nRehabperiod_aut = .FieldToClass("nRehabperiod_aut")
					sTyp_clause = .FieldToClass("sTyp_clause")
					sTyp_discxp = .FieldToClass("sTyp_discxp")
					sTyp_module = .FieldToClass("sTyp_module")
					nDuration = .FieldToClass("nDuration")
					sGroupind = .FieldToClass("sGroupind")
					sIndivind = .FieldToClass("sIndivind")
					sMultiind = .FieldToClass("sMultiind")
					dNulldate = .FieldToClass("dNulldate")
					nPayFreq = .FieldToClass("nPayfreq")
					sPolitype = .FieldToClass("sPolitype")
					nQmaxcurr = .FieldToClass("nQmaxcurr")
					sReintype = .FieldToClass("sReintype")
					sRenewal = .FieldToClass("sRenewal")
					sRevalapl = .FieldToClass("sRevalapl")
					nRevalrat = .FieldToClass("nRevalrat")
					sRevaltyp = .FieldToClass("sRevaltyp")
					sStyle_comm = .FieldToClass("sStyle_comm")
					sStyle_prem = .FieldToClass("sStyle_prem")
					sStyle_tax = .FieldToClass("sStyle_tax")
					nTariff = .FieldToClass("nTariff")
					sTimeren = .FieldToClass("sTimeren")
					sWin_declar = .FieldToClass("sWin_declar")
					nInsmaxiq = .FieldToClass("nInsmaxiq")
					nInsminiq = .FieldToClass("nInsminiq")
					sHolder = .FieldToClass("sHolder")
					nQdays_pro = .FieldToClass("nQdays_pro")
					nQuota = .FieldToClass("nQuota")
					sGroupsi = .FieldToClass("sGroupsi")
					sDescript = .FieldToClass("sDescript")
					sShort_des = .FieldToClass("sShort_des")
					sStatregt = .FieldToClass("sStatregt")
					sBrancht = .FieldToClass("sBrancht")
					sRealind = .FieldToClass("sRealind")
					nCompany = .FieldToClass("nCompany")
					nWay_pay = .FieldToClass("nWay_Pay")
					nBill_day = .FieldToClass("nBill_day")
					sTyp_dom = .FieldToClass("sTyp_dom")
					sLeg = .FieldToClass("sLeg")
					nRehabperiod = .FieldToClass("nRehabperiod")
					sReinst = .FieldToClass("sReinst")
					sDatecoll = .FieldToClass("sDatecoll")
					sFirst_pay = .FieldToClass("sFirst_pay")
					nQdays_quo = .FieldToClass("nQdays_quo")
					nMonth_surr = .FieldToClass("nMonth_surr")
					nClaim_Notice = .FieldToClass("nClaim_Notice")
					nClaim_Pay = .FieldToClass("nClaim_Pay")
					nNotCancelDay = .FieldToClass("nNotCancelDay")
					sRoutaut_r = .FieldToClass("sRoutaut_r")
					nRepInsured = .FieldToClass("nRepInsured")
					sNumprop = IIf(.FieldToClass("sNumprop") = "1", "1", "2")
					sFracReceip = IIf(.FieldToClass("sFracReceip") = "1", "1", "2")
					sCondSVS = .FieldToClass("sCondSVS")
					sAuto_susc = IIf(.FieldToClass("sAuto_susc") = "1", "1", "2")
					nQDays_DifQuo = .FieldToClass("nQdays_difquo")
					sSetprem = IIf(.FieldToClass("sSetprem") = "1", "1", "2")
					nMonth_Setpr = .FieldToClass("nMonth_Setpr")
					sRetarif = IIf(.FieldToClass("sRetarif") = "1", "1", "2")
					sRecSec = IIf(.FieldToClass("sRecSec") = "1", "1", "2")
					sMassive = IIf(.FieldToClass("sMassive") = "1", "1", "2")
					sTarQuo_Ind = IIf(.FieldToClass("sTarQuo_Ind") = "1", "1", "2")
					nPayable = .FieldToClass("nPayable")
					nAdvance = .FieldToClass("nAdvance")
					sReactivation = .FieldToClass("sReactivation")
					nReactPeriod = .FieldToClass("nReactPeriod")
					nReactPeriod_Aut = .FieldToClass("nReactPeriod_Aut")
					sRoutReact = .FieldToClass("sRoutReact")
					nChUserLev = .FieldToClass("nChUserLev")
                    sRatingServiceUsing = IIf(.FieldToClass("sRatingServiceUsing") = "1", "1", "2")
                    Me.sPreffix = .FieldToClass("sPreffix")
                    Me.sRou_warning_charg = .FieldToClass("sRou_warning_charg")
                    Me.sRou_cover = .FieldToClass("sRou_cover")
                    Me.nCurr_receipt = .FieldToClass("nCurr_receipt")
                    Me.sAssociated_Policy_Required = .FieldToClass("sAssociated_Policy_Required")
                    Me.nAssociatedBranch = .FieldToClass("nAssociatedBranch")
                    Me.nTypeAccount  = .FieldToClass("nTypeAccount")
                    Me.nModuleMin = .FieldToClass("nModuleMin")
                    sAutomaticBill = .FieldToClass("sAutomaticBill")
                    sResolutionSBS = .FieldToClass("sResolutionSBS")
                    .RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaProduct = Nothing
	End Function
	
	'% FindProdMasterActive: Permite leer los datos generales del producto activo.
	Public Function FindProdMasterActive(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		Static lblnRead As Boolean
		Dim lrecreaProdmaster3 As eRemoteDB.Execute
		
		lrecreaProdmaster3 = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaProdmaster3'
		'+ Información leída el 15/11/2000 16:31:39
		
		On Error GoTo FindProdMasterActive_Err
		
		If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or lblnFind Then
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			
			With lrecreaProdmaster3
				.StoredProcedure = "reaProdmaster3"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					sDescript = .FieldToClass("sDescript")
					sShort_des = .FieldToClass("sShort_des")
					sStatregt = .FieldToClass("sStatregt")
					nUsercode = .FieldToClass("nUsercode")
					sBrancht = .FieldToClass("sBrancht")
					sRealind = .FieldToClass("sRealind")
					nCompany = .FieldToClass("nCompany")
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		FindProdMasterActive = lblnRead
		'UPGRADE_NOTE: Object lrecreaProdmaster3 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProdmaster3 = Nothing
		
FindProdMasterActive_Err: 
		If Err.Number Then
			FindProdMasterActive = False
		End If
		On Error GoTo 0
	End Function
	
	'% FindProduct_li : Lee las caracteríticas de un producto de vida
	Public Function FindProduct_li(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaProduct_li As eRemoteDB.Execute
		
		On Error GoTo FindProduct_li_Err
		If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or dEffecdate <> mdtmDate Or bFind Then
			
			'+ Definición de parámetros para stored procedure 'reaProduct_li'
			lrecreaProduct_li = New eRemoteDB.Execute
			With lrecreaProduct_li
				.StoredProcedure = "reaProduct_li"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					dEffecdateProduct_li = .FieldToClass("dEffecdate")
					sAccounti = .FieldToClass("sAccounti")
					nAnlifint = .FieldToClass("nAnlifint")
					nAnnualap = .FieldToClass("nAnnualap")
					sAssociai = .FieldToClass("sAssociai")
					nBenefapl = .FieldToClass("nBenefapl")
					nBenefexc = .FieldToClass("nBenefexc")
					nBenefitr = .FieldToClass("nBenefitr")
					nBenexcra = .FieldToClass("nBenexcra")
					nCharge = .FieldToClass("nCharge")
					sClallpre = .FieldToClass("sClallpre")
					sClannpei = .FieldToClass("sClannpei")
					sCllifeai = .FieldToClass("sCllifeai")
					sClnoprei = .FieldToClass("sClnoprei")
					sClpaypri = .FieldToClass("sClpaypri")
					sClsimpai = .FieldToClass("sClsimpai")
					sClsurrei = .FieldToClass("sClsurrei")
					sCltransi = .FieldToClass("sCltransi")
					nCurrency = .FieldToClass("nCurrency")
					nDedufreq = .FieldToClass("nDedufreq")
					nEntrance = .FieldToClass("nEntrance")
					nSurrfreq = .FieldToClass("nSurrFreq")
					nIdurafix = .FieldToClass("nIdurafix")
					nTypdurins = .FieldToClass("nTypdurins")
					sIdurvari = .FieldToClass("sIdurvari")
					nInterest = .FieldToClass("nInterest")
					sMethagav = .FieldToClass("sMethagav")
					sMethprav = .FieldToClass("sMethprav")
					sMethprin = .FieldToClass("sMethprin")
					sMorcapii = .FieldToClass("sMorcapii")
					sNoperiod = .FieldToClass("sNoPeriod", "2")
					nNpemulti = .FieldToClass("nNpemulti")
					nNpenumai = .FieldToClass("nNpenumai")
					nNpenunmi = .FieldToClass("nNpenunmi")
					sNpeunifa = .FieldToClass("sNpeunifa", "2")
					nPayiniti = .FieldToClass("nPayiniti")
					nPayinter = .FieldToClass("nPayinter")
					nPdurafix = .FieldToClass("nPdurafix")
					sPdurvari = .FieldToClass("sPdurvari")
					sPeriodic = .FieldToClass("sPeriodic", "2")
					nPermulti = .FieldToClass("nPermulti")
					nPernopay = .FieldToClass("nPernopay")
					nPernumai = .FieldToClass("nPernumai")
					nPernunmi = .FieldToClass("nPernunmi")
					nPerrevfa = .FieldToClass("nPerrevfa")
					sPerunifa = .FieldToClass("sPerunifa")
					nProdClas = .FieldToClass("nProdClas")
					nQbonusma = .FieldToClass("nQbonusma")
					nQbonusmi = .FieldToClass("nQbonusmi")
					nReagemax = .FieldToClass("nReagemax")
					sRouadvan = .FieldToClass("sRouadvan")
					sRoureddc = .FieldToClass("sRoureddc")
					sRoureduc = .FieldToClass("sRoureduc")
					sRousurre = .FieldToClass("sRousurre")
					nSuagemax = .FieldToClass("nSuagemax")
					nSuagemin = .FieldToClass("nSuagemin")
					nSurcashv = .FieldToClass("nSurcashv")
					sSurrenpi = .FieldToClass("sSurrenpi")
					sSurrenti = .FieldToClass("sSurrenti")
					sUlfchani = .FieldToClass("sUlfchani")
					nUlfmaxqu = .FieldToClass("nUlfmaxqu")
					nUlrcharg = .FieldToClass("nUlrcharg")
					nUlredper = .FieldToClass("nUlredper")
					nUlrmaxqu = .FieldToClass("nUlrmaxqu")
					nUlrschar = .FieldToClass("nUlrschar")
					nUlscharg = .FieldToClass("nUlscharg")
					nUlsmaxqu = .FieldToClass("nUlsmaxqu")
					sAssototal = .FieldToClass("sAssoTotal")
					nUlsschar = .FieldToClass("nUlsschar")
					sBenres = .FieldToClass("sBenRes")
					nUlswiper = .FieldToClass("nUlswiper")
					nValuebon = .FieldToClass("nValuebon")
					nYearmins = .FieldToClass("nYearmins")
					nYearminw = .FieldToClass("nYearminw")
					nYearmors = .FieldToClass("nYearmors")
					sPremiumtype = .FieldToClass("sPremiumType")
					nTaxsmoke = .FieldToClass("nTaxsmoke")
					nTaxnsmoke = .FieldToClass("nTaxnsmoke")
					dNulldate = .FieldToClass("dNulldate")
					nChargeamo = .FieldToClass("nChargeAmo")
					nMinrent = .FieldToClass("nMinrent")
					nMaxrent = .FieldToClass("nMaxrent")
					nQmeploans = .FieldToClass("nQmeploans")
					nQmmloans = .FieldToClass("nQmmloans")
					nQmyloans = .FieldToClass("nQmyloans")
					nAminloans = .FieldToClass("nAminloans")
					nAmaxloans = .FieldToClass("nAmaxloans")
					nPervsloans = .FieldToClass("nPervsloans")
					nPerctol = .FieldToClass("nPerctol")
					nTaxes = .FieldToClass("nTaxes")
					sRouinterest = .FieldToClass("sRouinterest")
					nBill_item = .FieldToClass("nBill_item")
					nQmepsurr = .FieldToClass("nQmepsurr")
					nQmmsurr = .FieldToClass("nQmmsurr")
					nQmysurr = .FieldToClass("nQmysurr")
					nAmaxsurr = .FieldToClass("nAmaxsurr")
					nAminsurr = .FieldToClass("nAminsurr")
					nPervssurr = .FieldToClass("nPervssurr")
					nCapminsurr = .FieldToClass("nCapminsurr")
					nPremmin = .FieldToClass("nPremMin")
					nQmonVPN = .FieldToClass("nQmonVPN")
					nQmonToVPN = .FieldToClass("nQmonToVPN")
					nRateReh = .FieldToClass("nRateReh")
					sRoutine_C = .FieldToClass("sRoutine_C")
					sRoutinsu = .FieldToClass("sRoutinsu")
					sRoutpay = .FieldToClass("sRoutpay")
					sRevaltyp = .FieldToClass("sRevaltyp")
					nMonthamo = .FieldToClass("nMonthamo")
					nAdminrate = .FieldToClass("nAdminrate")
					nTypdurpay = .FieldToClass("nTypDurpay")
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.dEffecdate = .FieldToClass("dEffecdate")
					mdtmDate = dEffecdate
					nSaving_pct = .FieldToClass("nSaving_pct")
					nIndex_table = .FieldToClass("nIndex_table")
					nWarrn_table = .FieldToClass("nWarrn_table")
					sS_allwchng = .FieldToClass("sS_allwchng")
					sIx_allwchng = .FieldToClass("sIx_allwchng")
					sW_allwchng = .FieldToClass("sW_allwchng")
					nInfType = .FieldToClass("nInfType")
					nDay_bmg = .FieldToClass("nDay_bmg")
					nYear_bmg = .FieldToClass("nYear_bmg")
					nAge_bmg = .FieldToClass("nAge_bmg")
					sApv = .FieldToClass("sApv")
					nUlsmin = .FieldToClass("nUlsmin")
					nCostRe = .FieldToClass("nCostRe", 0)
					nOrigin_surr = .FieldToClass("nOrigin_surr")
					nOrigin_loan = .FieldToClass("nOrigin_loan")
					nULmmsw = .FieldToClass("nULmmsw")
					nULmmrd = .FieldToClass("nULmmrd")
					nULswmaxper = .FieldToClass("nULswmaxper")
					nULrdmaxper = .FieldToClass("nULrdmaxper")
					nULSwmqt = .FieldToClass("nULSwmqt")
					nULrdmqt = .FieldToClass("nULrdmqt")
					nULswmqtper = .FieldToClass("nULswmqtper")
					nULrdmqtper = .FieldToClass("nULrdmqtper")
					nULswchPerc = .FieldToClass("nULswchPerc")
					nUlrdchperc = .FieldToClass("nUlrdchperc")
					nDayBuyUnit = .FieldToClass("nDayBuyUnit")
					nMaxchargsurr = .FieldToClass("nMaxchargsurr")
					sIndCl_Pay = .FieldToClass("sIndCl_Pay")
					nDayIssue = .FieldToClass("nDayIssue")
					nPerc_secur = .FieldToClass("nPerc_secur")
					sRoutineSurr = .FieldToClass("sRoutineSurr")
					sApplyRouSurr = .FieldToClass("sApplyRouSurr")
					sAccount_mirror = .FieldToClass("sAccount_mirror")
					nwarrn_table_mirror = .FieldToClass("nwarrn_table_mirror")
					nBmg = .FieldToClass("nBmg")
					sRoutinevpn = .FieldToClass("sRoutinevpn")
					nQMMPSurr = .FieldToClass("nQMMPSurr")
					nBalminsurr = .FieldToClass("nBalminsurr")
                    sNo_Holidays = .FieldToClass("sNo_Holidays")
                    nType_Rateproy = .FieldToClass("nType_Rateproy")
					
					.RCloseRec()
					FindProduct_li = True
				End If
			End With
		Else
			FindProduct_li = True
		End If
		
FindProduct_li_Err: 
		If Err.Number Then
			FindProduct_li = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProduct_li = Nothing
	End Function
	
	'% UpdateProduct_li : Actualiza las caracteríticas de un producto de vida
	Public Function UpdateProduct_Li() As Boolean
		Dim lrecupdProduct_li As eRemoteDB.Execute
		
		On Error GoTo UpdateProduct_Li_err
		
		lrecupdProduct_li = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updProduct_li'
		'+ Información leída el 09/02/2000 02:37:01 PM
		With lrecupdProduct_li
			.StoredProcedure = "updProduct_li"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProdClas", nProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouadvan", sRouadvan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnlifint", nAnlifint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayInter", nPayinter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRousurre", sRousurre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSurrenti", sSurrenti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSurrenpi", sSurrenpi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurcashv", nSurcashv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurrFreq", nSurrfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCharge", nCharge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChargeAmo", nChargeamo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMorCapii", sMorcapii, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureduc", sRoureduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureddc", sRoureddc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAssociai", sAssociai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAssoTotal", sAssototal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefitr", nBenefitr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefApl", nBenefapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefexc", nBenefexc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenexcra", nBenexcra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBenRes", sBenres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurins", nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIdurvari", sIdurvari, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIduraFix", nIdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPdurvari", sPdurvari, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPduraFix", nPdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuageMin", nSuagemin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuageMax", nSuagemax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReageMax", nReagemax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearminw", nYearminw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMors", nYearmors, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMins", nYearmins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClSimpai", sClsimpai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClnoprei", sClnoprei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClsurrei", sClsurrei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClpaypri", sClpaypri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClAllpre", sClallpre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClTransi", sCltransi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCllifeai", sCllifeai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClannpei", sClannpei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayiniti", nPayiniti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnnualap", nAnnualap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodic", sPeriodic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDedufreq", nDedufreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPerunifa", sPerunifa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPermulti", nPermulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPernunmi", nPernunmi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPernumai", nPernumai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRevaltyp", sRevaltyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerrevfa", nPerrevfa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNoPeriod", sNoperiod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNpeunifa", sNpeunifa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethagav", sMethagav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethprav", sMethprav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethprin", sMethprin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPremiumType", sPremiumtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxSmoke", nTaxsmoke, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxNSmoke", nTaxnsmoke, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlfmaxqu", nUlfmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUlfchani", sUlfchani, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlsmaxqu", nUlsmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlswiper", nUlswiper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlsschar", nUlsschar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlscharg", nUlscharg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrmaxqu", nUlrmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlredper", nUlredper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrschar", nUlrschar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrcharg", nUlrcharg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthAmo", nMonthamo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdminRate", nAdminrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinrent", nMinrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxrent", nMaxrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmeploans", nQmeploans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmmloans", nQmmloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmyloans", nQmyloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAminloans", nAminloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmaxloans", nAmaxloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPervsloans", nPervsloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerctol", nPerctol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxes", nTaxes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouinterest", sRouinterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmepsurr", nQmepsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmmsurr", nQmmsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmysurr", nQmysurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAminsurr", nAminsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmaxsurr", nAmaxsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPervssurr", nPervssurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapminsurr", nCapminsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremMin", nPremmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonVPN", nQmonVPN, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonToVPN", nQmonToVPN, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateReh", nRateReh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 3, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine_c", sRoutine_C, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutinsu", sRoutinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNpemulti", nNpemulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNpenunmi", nNpenunmi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNpenumai", nNpenumai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurpay", nTypdurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutpay", sRoutpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_pct", nSaving_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndex_table", nIndex_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWarrn_table", nWarrn_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sS_allwchng", sS_allwchng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIx_allwchng", sIx_allwchng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sW_allwchng", sW_allwchng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInfType", nInfType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay_bmg", nDay_bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_bmg", nYear_bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_bmg", nAge_bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApv", sApv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlsmin", nUlsmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCostRe", nCostRe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nOrigin_surr", nOrigin_surr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin_loan", nOrigin_loan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULmmsw", nULmmsw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULmmrd", nULmmrd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULswmaxper", nULswmaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULrdmaxper", nULrdmaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULSwmqt", nULSwmqt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULrdmqt", nULrdmqt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULswmqtper", nULswmqtper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULrdmqtper", nULrdmqtper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nULswchPerc", nULswchPerc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUlrdchperc", nUlrdchperc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDayBuyUnit", nDayBuyUnit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxchargsurr", nMaxchargsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndCl_Pay", sIndCl_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutineSurr", sRoutineSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApplyRouSurr", sApplyRouSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_mirror", sAccount_mirror, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nwarrn_table_mirror", nwarrn_table_mirror, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBmg", nBmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutinevpn", sRoutinevpn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQMMPSurr", nQMMPSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalminsurr", nBalminsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("sNo_Holidays", sNo_Holidays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("nType_rateproy", nType_Rateproy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateProduct_Li = .Run(False)
		End With
		
UpdateProduct_Li_err: 
		If Err.Number Then
			UpdateProduct_Li = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdProduct_li = Nothing
	End Function
	
	'% insValProdMaster: Valida si el producto esta registrado
	Public Function insValProdMaster(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		insValProdMaster = FindProdMaster(nBranch, nProduct)
	End Function
	
	'% FindProdMaster: Esta rutina permite leer los datos generales del producto.
	Public Function FindProdMaster(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByRef bFind As Boolean = False) As Boolean
		Dim lrec_ProdMaster As eRemoteDB.Execute
		
		On Error GoTo FindProdMaster_Err
		
		If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or bFind Then
			lrec_ProdMaster = New eRemoteDB.Execute
			With lrec_ProdMaster
				.StoredProcedure = "reaProdmaster2"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					FindProdMaster = True
					blnError = True
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.sBrancht = .FieldToClass("sBrancht")
					Me.sDescript = .FieldToClass("sDescript")
					Me.sRealind = .FieldToClass("sRealind")
					Me.sShort_des = .FieldToClass("sShort_des")
					Me.sStatregt = .FieldToClass("sStatregt")
					.RCloseRec()
				End If
			End With
		Else
			FindProdMaster = True
		End If
		
FindProdMaster_Err: 
		If Err.Number Then
			FindProdMaster = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrec_ProdMaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_ProdMaster = Nothing
	End Function
	
	'% ValTab_short_a: Verifica si existe algun registro de corto plazo para un ramo-producto determinado
	Public Function ValTab_short_a(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		
		'- Se define la variable lrecTab_Short
		
		Dim lrecTab_short As eRemoteDB.Execute
		lrecTab_short = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.valTab_short_a'
		'+ Información leída el 21/03/2001 01:29:24 p.m.
		
		ValTab_short_a = False
		
		With lrecTab_short
			.StoredProcedure = "valTab_short_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				ValTab_short_a = lrecTab_short.FieldToClass("lCount") > 0
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecTab_short may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_short = Nothing
	End Function
	
	
	'% FindWin_info: Devuelve información referente a una tabla,
	'  según un código de ventana dado
	Public Function FindWin_info(ByVal sCodispl As String) As Boolean
		
		Dim lrecreaWindows As eRemoteDB.Query
		Dim lstrFields As String
		Dim lstrCondition As String
		
		On Error GoTo FindWin_info_Err
		
		lrecreaWindows = New eRemoteDB.Query
		
		'+ Definición de parámetros para stored procedure 'insudb.reaWindows'
		'+ Información leída el 15/03/2001 11:27:47 a.m.
		
		With lrecreaWindows
			lstrFields = "sCodispl, nAmelevel, sCodisp, nImg_index, sCodmen, dCompdate, " & "sDescript, sDirectgo, nG_identi, nInqlevel, nModules, sPseudo, " & "nSequence, sShort_des, sStatregt, nUsercode, nWindowty"
			lstrCondition = "Windows.sCodispl = '" & sCodispl & "' AND Windows.sStatregt < '4'"
			
			If .OpenQuery("Windows", lstrFields, lstrCondition) Then
				sCodispl = .FieldToClass("sCodispl")
				nAmelevel = .FieldToClass("nAmelevel")
				sCodisp = .FieldToClass("sCodisp")
				nImg_index = .FieldToClass("nImg_index")
				sCodmen = .FieldToClass("sCodmen")
				sDescript_win = .FieldToClass("sDescript")
				sDirectgo = .FieldToClass("sDirectgo")
				nG_identi = .FieldToClass("nG_identi")
				nInqlevel = .FieldToClass("nInqlevel")
				nModules = .FieldToClass("nModules")
				sPseudo = .FieldToClass("sPseudo")
				Nsequence = .FieldToClass("nSequence")
				sShort_des_win = .FieldToClass("sShort_des")
				sStatregt_win = .FieldToClass("sStatregt")
				nUsercode = .FieldToClass("nUsercode")
				nWindowty = .FieldToClass("nWindowty")
				
				.CloseQuery()
				FindWin_info = True
			Else
				FindWin_info = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaWindows = Nothing
		
FindWin_info_Err: 
		If Err.Number Then
			FindWin_info = False
		End If
		On Error GoTo 0
	End Function
	
	'% UpdateProduct : Actualiza las caracteríticas de un producto
	'--------------------------------------------------------------
	Public Function UpdateProduct() As Boolean
		'--------------------------------------------------------------
		Dim lrecinsProduct As eRemoteDB.Execute
		
		On Error GoTo UpdateProduct_Err
		
		lrecinsProduct = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insProduct'
		'+ Información leída el 10/11/2000 11.23.14
		With lrecinsProduct
			.StoredProcedure = "insProduct"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCancnoti", nCancnoti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim_pres", nClaim_pres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCopies", nCopies, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCumreint", sCumreint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCumultyp", sCumultyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDeclaaut", sDeclaaut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndivind", sIndivind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sGroupind", sGroupind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMultiind", sMultiind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmaxcurr", nQmaxcurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReintype", sReintype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRenewal", sRenewal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRevalapl", sRevalapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRevalrat", nRevalrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRevaltyp", sRevaltyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStyle_comm", sStyle_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStyle_prem", sStyle_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStyle_tax", sStyle_tax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTimeren", sTimeren, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColinvot", sColinvot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_certif", nQ_certif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_clause", sTyp_clause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_discxp", sTyp_discxp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWin_declar", sWin_declar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsmaxiq", nInsmaxiq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHolder", sHolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsminiq", nInsminiq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQdays_pro", nQdays_pro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuota", nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sGroupsi", sGroupsi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_day", nBill_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_dom", sTyp_dom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLeg", sLeg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRehabperiod", nRehabperiod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReinst", sReinst, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirst_pay", sFirst_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDatecoll", sDatecoll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQdays_quo", nQdays_quo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_surr", nMonth_surr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim_Notice", nClaim_Notice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim_Pay", nClaim_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotcancelday", nNotCancelDay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRepInsured", nRepInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNumprop", sNumprop, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutaut_r", sRoutaut_r, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFracReceip", sFracReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondSVS", sCondSVS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAuto_susc", sAuto_susc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQdays_difquo", nQDays_DifQuo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSetprem", sSetprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_Setpr", nMonth_Setpr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRetarif", sRetarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRecSec", sRecSec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRehabperiod_aut", nRehabperiod_aut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMassive", sMassive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTarQuo_Ind", sTarQuo_Ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayable", nPayable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdvance", nAdvance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReactivation", sReactivation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReactPeriod", nReactPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReactPeriod_Aut", nReactPeriod_Aut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutReact", sRoutReact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChUserLev", nChUserLev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRatingServiceUsing", sRatingServiceUsing, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRou_warning_charg", sRou_warning_charg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRou_cover", sRou_cover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurr_receipt", nCurr_receipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAssociated_Policy_Required", sAssociated_Policy_Required, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAssociatedBranch", nAssociatedBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntypeAccount", nTypeAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModuleMin", nModuleMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAutomaticBill", sAutomaticBill, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sResolutionSBS", sResolutionSBS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



            UpdateProduct = .Run(False)
		End With
		
UpdateProduct_Err: 
		If Err.Number Then
			UpdateProduct = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsProduct = Nothing
	End Function
	
	'% UpdateProdmaster: Actualiza la tabla Prodmaster
	'----------------------------------------------------
	Public Function UpdateProdmaster() As Boolean
		'----------------------------------------------------
		Dim lrecupdProdmaster As eRemoteDB.Execute
		
		On Error GoTo UpdateProdmaster_Err
		
		lrecupdProdmaster = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updProdmaster'
		'+ Información leída el 16/03/2001 03:20:18 p.m.
		With lrecupdProdmaster
			.StoredProcedure = "updProdmaster"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRealind", sRealind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPreffix", sPreffix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			UpdateProdmaster = .Run(False)
		End With
		
UpdateProdmaster_Err: 
		If Err.Number Then
			UpdateProdmaster = False
		End If
		'UPGRADE_NOTE: Object lrecupdProdmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdProdmaster = Nothing
		On Error GoTo 0
	End Function
	
	'% UpdProdmaster_Desc: Actualiza la descripción de un producto en proceso de instalación
	'  en la tabla Prodmaster
	Public Function UpdProdmaster_Desc() As Boolean
		
		'- Se define la variable lrecupdProdmaster_sDesc
		
		Dim lrecupdProdmaster_sDesc As eRemoteDB.Execute
		
		On Error GoTo UpdProdmaster_Desc_Err
		
		lrecupdProdmaster_sDesc = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updProdmaster_sDesc'
		'+ Información leída el 20/03/2001 02:17:04 p.m.
		
		With lrecupdProdmaster_sDesc
			.StoredProcedure = "updProdmaster_sDesc"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdProdmaster_Desc = .Run(False)
		End With
		
UpdProdmaster_Desc_Err: 
		If Err.Number Then
			UpdProdmaster_Desc = False
		End If
		'UPGRADE_NOTE: Object lrecupdProdmaster_sDesc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdProdmaster_sDesc = Nothing
		On Error GoTo 0
	End Function
	
	'% AddProdmaster: Actualiza la descripción de un producto en la tabla Prodmaster
	Public Function AddProdmaster() As Boolean
		'- Se define la variable lreccreProdmaster
		
		Dim lreccreProdmaster As eRemoteDB.Execute
		
		On Error GoTo AddProdmaster_Err
		
		lreccreProdmaster = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creProdmaster'
		'+ Información leída el 20/03/2001 03:30:07 p.m.
		
		With lreccreProdmaster
			.StoredProcedure = "creProdmaster"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRealind", sRealind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			AddProdmaster = .Run(False)
		End With
		
AddProdmaster_Err: 
		If Err.Number Then
			AddProdmaster = False
		End If
		'UPGRADE_NOTE: Object lreccreProdmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreProdmaster = Nothing
		On Error GoTo 0
	End Function
	
	'% updProdmasterDesc_byproduct: Actualiza la descripción de un producto en la tabla Prodmaster
	'  (la descripción es pasada como parámetro)
	Public Function updProdmasterDesc_byproduct() As Boolean
		
		'- Se define la variable lrecupdProdmaster_Desc1
		
		Dim lrecupdProdmaster_Desc1 As eRemoteDB.Execute
		
		On Error GoTo updProdmasterDesc_byproduct_Err
		
		lrecupdProdmaster_Desc1 = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updProdmaster_Desc1'
		'+ Información leída el 21/03/2001 10:14:24 a.m.
		
		With lrecupdProdmaster_Desc1
			.StoredProcedure = "updProdmaster_Desc1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			updProdmasterDesc_byproduct = .Run(False)
		End With
		
updProdmasterDesc_byproduct_Err: 
		If Err.Number Then
			updProdmasterDesc_byproduct = False
		End If
		'UPGRADE_NOTE: Object lrecupdProdmaster_Desc1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdProdmaster_Desc1 = Nothing
		On Error GoTo 0
	End Function
	
	'% FindProdmasterDescript: Devuelve la descripción de un determinado producto
	Public Function FindProdmasterDescript(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		
		'- Se define la variable lrecreaProdmaster_sdescript
		
		Dim lrecreaProdmaster_sdescript As eRemoteDB.Execute
		
		On Error GoTo FindProdmasterDescript_Err
		
		lrecreaProdmaster_sdescript = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaProdmaster_sdescript'
		'+ Información leída el 21/03/2001 11:15:31 a.m.
		
		With lrecreaProdmaster_sdescript
			.StoredProcedure = "reaProdmaster_sdescript"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				sDescript = .FieldToClass("sDescript")
				sBrancht = .FieldToClass("sBrancht")
				.RCloseRec()
				FindProdmasterDescript = True
			End If
		End With
		
FindProdmasterDescript_Err: 
		If Err.Number Then
			FindProdmasterDescript = False
		End If
		'UPGRADE_NOTE: Object lrecreaProdmaster_sdescript may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProdmaster_sdescript = Nothing
		On Error GoTo 0
	End Function
	
	'% FindCreateDate: Devuelve la fecha de efecto de creación del producto
	Public Function FindCreateDate(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		
		'- Se define la variable lrecreaMinDateProd
		
		Dim lrecreaMinDateProd As eRemoteDB.Execute
		
		On Error GoTo FindCreateDate_Err
		
		lrecreaMinDateProd = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaMinDateProd'
		'+ Información leída el 21/03/2001 11:58:41 a.m.
		
		With lrecreaMinDateProd
			.StoredProcedure = "reaMinDateProd"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				dEffecdate = .FieldToClass("dEffecdate")
				.RCloseRec()
				FindCreateDate = True
			End If
		End With
		
FindCreateDate_Err: 
		If Err.Number Then
			FindCreateDate = False
		End If
		'UPGRADE_NOTE: Object lrecreaMinDateProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMinDateProd = Nothing
		On Error GoTo 0
	End Function
	
	'% FindLastDate: Devuelve la última fecha de efecto
	Public Function FindLastDate(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecreaProduct_v As eRemoteDB.Execute
		
		On Error GoTo FindLastDate_Err
		
		lrecreaProduct_v = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaProduct_v'
		'+ Información leída el 21/03/2001 01:29:24 p.m.
		
		With lrecreaProduct_v
			.StoredProcedure = "reaProduct_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				dEffecdate = .FieldToClass("dEffecdate")
				.RCloseRec()
				FindLastDate = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProduct_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProduct_v = Nothing
		
FindLastDate_Err: 
		If Err.Number Then
			FindLastDate = False
		End If
		'UPGRADE_NOTE: Object lrecreaProduct_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProduct_v = Nothing
		On Error GoTo 0
	End Function
	
	'% valPolicyExist: Valida si no se han emitido pólizas para el producto
	Public Function valPolicyExist(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		
		'- Se define la variable lrecreaProduct_1
		
		Dim lrecreaProduct_1 As eRemoteDB.Execute
		
		On Error GoTo valPolicyExist_Err
		
		lrecreaProduct_1 = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaProduct_1'
		'+ Información leída el 21/03/2001 02:07:13 p.m.
		
		With lrecreaProduct_1
			.StoredProcedure = "reaProduct_1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nPolicy = .FieldToClass("nPolicy")
				.RCloseRec()
				valPolicyExist = True
			End If
		End With
		
valPolicyExist_Err: 
		If Err.Number Then
			valPolicyExist = False
		End If
		'UPGRADE_NOTE: Object lrecreaProduct_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProduct_1 = Nothing
		On Error GoTo 0
	End Function
	
	'% AddProduct_li: Añade registros a la tabla product_li
	'-------------------------------------------------------
	Public Function AddProduct_li() As Boolean
		'-------------------------------------------------------
		Dim lreccreProduct_li As eRemoteDB.Execute
		
		On Error GoTo AddProduct_li_Err
		
		lreccreProduct_li = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creProduct_li'
		'+ Información leída el 20/11/2000 9.41.35
		With lreccreProduct_li
			.StoredProcedure = "creProduct_li"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProdClas", nProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouadvan", sRouadvan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnlifint", nAnlifint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayInter", nPayinter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRousurre", sRousurre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSurrenti", sSurrenti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSurrenpi", sSurrenpi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurcashv", nSurcashv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurrFreq", nSurrfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCharge", nCharge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChargeAmo", nChargeamo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMorCapii", sMorcapii, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureduc", sRoureduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureddc", sRoureddc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAssociai", sAssociai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAssoTotal", sAssototal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefitr", nBenefitr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefApl", nBenefapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefexc", nBenefexc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenexcra", nBenexcra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBenRes", sBenres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurins", nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIdurvari", sIdurvari, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIduraFix", nIdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPdurvari", sPdurvari, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPduraFix", nPdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuageMin", nSuagemin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuageMax", nSuagemax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReageMax", nReagemax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearminw", nYearminw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMors", nYearmors, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMins", nYearmins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClSimpai", sClsimpai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClnoprei", sClnoprei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClsurrei", sClsurrei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClpaypri", sClpaypri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClAllpre", sClallpre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClTransi", sCltransi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCllifeai", sCllifeai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClannpei", sClannpei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayiniti", nPayiniti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnnualap", nAnnualap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodic", sPeriodic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDedufreq", nDedufreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPerunifa", sPerunifa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPermulti", nPermulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPernunmi", nPernunmi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPernumai", nPernumai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRevaltyp", sRevaltyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerrevfa", nPerrevfa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNoPeriod", sNoperiod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("NNPEMULTI", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NNPENUMAI", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NNPENUNMI", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("sNpeunifa", sNpeunifa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethagav", sMethagav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethprav", sMethprav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethprin", sMethprin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPremiumType", sPremiumtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxSmoke", nTaxsmoke, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxNSmoke", nTaxnsmoke, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlfmaxqu", nUlfmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUlfchani", sUlfchani, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlsmaxqu", nUlsmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlswiper", nUlswiper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlsschar", nUlsschar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlscharg", nUlscharg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrmaxqu", nUlrmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlredper", nUlredper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrschar", nUlrschar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrcharg", nUlrcharg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthAmo", nMonthamo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAdminRate", nAdminrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NMINRENT", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NMAXRENT", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQMEPLOANS", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQMMLOANS", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQMYLOANS", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NAMINLOANS", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NAMAXLOANS", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPERVSLOANS", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPERCTOL", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NTAXES", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SROUINTEREST", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBILL_ITEM", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQMEPSURR", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQMMSURR", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQMYSURR", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NAMINSURR", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NAMAXSURR", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPERVSSURR", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCAPMINSURR", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPREMMIN", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQMONVPN", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NQMONTOVPN", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NRATEREH", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 3, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SROUTINE_C", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SROUTINSU", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurpay", nTypdurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutpay", sRoutpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			AddProduct_li = .Run(False)
		End With
		
AddProduct_li_Err: 
		If Err.Number Then
			AddProduct_li = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreProduct_li = Nothing
	End Function
	
	'% UpdProduct_liDPost: Permite crear un registro válido para un producto de vida
	Public Function UpdProduct_liDPost() As Boolean
		Dim lrecinsProduct_liDPost As eRemoteDB.Execute
		
		On Error GoTo UpdProduct_liDPost_Err
		
		lrecinsProduct_liDPost = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insProduct_liDPost'
		'+ Información leída el 20/11/2000 11.36.43
		With lrecinsProduct_liDPost
			.StoredProcedure = "insProduct_liDPost"
			.Parameters.Add("dCurrDate", dEffecdateProduct_li, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProdClas", nProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouadvan", sRouadvan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnlifint", nAnlifint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayInter", nPayinter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRousurre", sRousurre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSurrenti", sSurrenti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSurrenpi", sSurrenpi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurcashv", nSurcashv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurrFreq", nSurrfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCharge", nCharge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChargeAmo", nChargeamo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMorCapii", sMorcapii, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureduc", sRoureduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureddc", sRoureddc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAssociai", sAssociai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAssoTotal", sAssototal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefitr", nBenefitr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefApl", nBenefapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenefexc", nBenefexc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenexcra", nBenexcra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBenRes", sBenres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurins", nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIdurvari", sIdurvari, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIduraFix", nIdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPdurvari", sPdurvari, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPduraFix", nPdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuageMin", nSuagemin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuageMax", nSuagemax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReageMax", nReagemax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearminw", nYearminw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMors", nYearmors, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMins", nYearmins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClSimpai", sClsimpai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClnoprei", sClnoprei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClsurrei", sClsurrei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClpaypri", sClpaypri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClAllpre", sClallpre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClTransi", sCltransi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCllifeai", sCllifeai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClannpei", sClannpei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayiniti", nPayiniti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnnualap", nAnnualap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodic", sPeriodic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDedufreq", nDedufreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPerunifa", sPerunifa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPermulti", nPermulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPernunmi", nPernunmi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPernumai", nPernumai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRevaltyp", sRevaltyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerrevfa", nPerrevfa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNoPeriod", sNoperiod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNpemulti", nNpemulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNpenumai", nNpenumai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNpenunmi", nNpenunmi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNpeunifa", sNpeunifa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethagav", sMethagav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethprav", sMethprav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMethprin", sMethprin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPremiumType", sPremiumtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxSmoke", nTaxsmoke, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxNSmoke", nTaxnsmoke, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlfmaxqu", nUlfmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUlfchani", sUlfchani, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlsmaxqu", nUlsmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 2, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlswiper", nUlswiper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlsschar", nUlsschar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlscharg", nUlscharg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrmaxqu", nUlrmaxqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlredper", nUlredper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrschar", nUlrschar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlrcharg", nUlrcharg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthAmo", nMonthamo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdminRate", nAdminrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinrent", nMinrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxrent", nMaxrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmeploans", nQmeploans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmmloans", nQmmloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmyloans", nQmyloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAminloans", nAminloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmaxloans", nAmaxloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPervsloans", nPervsloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerctol", nPerctol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxes", nTaxes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouinterest", sRouinterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmepsurr", nQmepsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmmsurr", nQmmsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmysurr", nQmysurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAminsurr", nAminsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmaxsurr", nAmaxsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPervssurr", nPervssurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapminsurr", nCapminsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremMin", nPremmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonVPN", nQmonVPN, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonToVPN", nQmonToVPN, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateReh", nRateReh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 3, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine_c", sRoutine_C, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutinsu", sRoutinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurpay", nTypdurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutpay", sRoutpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_pct", nSaving_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndex_table", nIndex_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWarrn_table", nWarrn_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sS_allwchng", sS_allwchng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIx_allwchng", sIx_allwchng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sW_allwchng", sW_allwchng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInfType", nInfType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay_bmg", nDay_bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_bmg", nYear_bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_bmg", nAge_bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApv", sApv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUlsmin", nUlsmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCostRe", nCostRe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nOrigin_surr", nOrigin_surr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin_loan", nOrigin_loan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULmmsw", nULmmsw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULmmrd", nULmmrd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULswmaxper", nULswmaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULrdmaxper", nULrdmaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULSwmqt", nULSwmqt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULrdmqt", nULrdmqt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULswmqtper", nULswmqtper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nULrdmqtper", nULrdmqtper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nULswchPerc", nULswchPerc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUlrdchperc", nUlrdchperc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDayBuyUnit", nDayBuyUnit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxchargsurr", nMaxchargsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDayIssue", nDayIssue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerc_Secur", nPerc_secur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 3, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutineSurr", sRoutineSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApplyRouSurr", sApplyRouSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_mirror", sAccount_mirror, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nwarrn_table_mirror", nwarrn_table_mirror, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBmg", nBmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutinevpn", sRoutinevpn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQMMPSurr", nQMMPSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalminsurr", nBalminsurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("nType_rateproy", nType_Rateproy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdProduct_liDPost = .Run(False)
		End With
		
UpdProduct_liDPost_Err: 
		If Err.Number Then
			UpdProduct_liDPost = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsProduct_liDPost may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsProduct_liDPost = Nothing
	End Function
	
	'% insValDP003_K: Realiza la validación de los campos a actualizar en la ventana principal
	'                 de la secuencia de Productos
	Public Function insValDP003_K(ByVal sCodispl As String, Optional ByVal nAction As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sBrancht As String = "") As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		Dim lclsValField As eFunctions.valField
		Dim lblnContinue As Boolean
		
		On Error GoTo insValDP003_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		lclsValField = New eFunctions.valField
		
		lblnContinue = True
		
		'+ Validaciones sobre el campo Ramo
		
		If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
			lblnContinue = False
		End If
		
		'+ Validaciones sobre el campo Código del producto
		
		If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11009)
			lblnContinue = False
		ElseIf nBranch > 0 Then 
			lclsValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lclsValues.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If lclsValues.IsValid("tabProdmaster", CStr(nProduct), True) Then
				If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If FindProdMasterActive(nBranch, nProduct) Then
						Call lclsErrors.ErrorMessage(sCodispl, 11024)
						lblnContinue = False
					End If
				Else
					If Not FindProdMasterActive(nBranch, nProduct) Then
						Call lclsErrors.ErrorMessage(sCodispl, 1011)
						lblnContinue = False
					End If
				End If
			Else
				If nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
					Call lclsErrors.ErrorMessage(sCodispl, 1011)
					lblnContinue = False
				End If
			End If
		End If
		
		'+ Validaciones sobre el campo de Fecha de Efecto
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				Call lclsErrors.ErrorMessage(sCodispl, 1103)
			End If
		Else
			lclsValField.objErr = lclsErrors
			If Not lclsValField.ValDate(dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1001)
				lblnContinue = False
			End If
		End If
		
		'+ Validación de que la fecha de efecto no sea menor a la de la última modificación del producto.
		
		If lblnContinue Then
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				If dEffecdate <> eRemoteDB.Constants.dtmNull Then
					If FindLastDate(nBranch, nProduct) Then
						If dEffecdate < Me.dEffecdate Then
							Call lclsErrors.ErrorMessage(sCodispl, 11178)
							lblnContinue = False
						End If
					End If
				End If
				
				'+ Se verifica que no se hayan emitido pólizas para el producto si la póliza es de vida.
				
				If lblnContinue Then
                    'If (sBrancht = CStr(pmBrancht.pmlife) Or sBrancht = CStr(pmBrancht.pmNotTraditionalLife)) Then
                    If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
                        If valPolicyExist(nBranch, nProduct) Then
                            Call lclsErrors.ErrorMessage(sCodispl, 11337)
                        End If
                    End If
                End If
            ElseIf nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
                If nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull Then
                    If dEffecdate = eRemoteDB.Constants.dtmNull Then
                        Call FindLastDate(nBranch, nProduct)
                    Else
                        If FindCreateDate(nBranch, nProduct) Then
                            If dEffecdate < Me.dEffecdate Then
                                Call lclsErrors.ErrorMessage(sCodispl, 11394)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        '+ Validaciones sobre el campo de Tipo de Producto solo si la acción no es consulta

        If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
            If sBrancht = String.Empty Or sBrancht = "0" Then
                Call lclsErrors.ErrorMessage(sCodispl, 36090)
            End If
        End If

        insValDP003_K = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValField = Nothing
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValDP003_K_Err:
        If Err.Number Then
            insValDP003_K = insValDP003_K & Err.Description
        End If
        On Error GoTo 0
	End Function
	
	'% insPostDP003_K: Valida todos los datos introducidos en la forma (parte Header)
	Public Function insPostDP003_K(ByVal nAction As Integer, Optional ByVal sBrancht As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As Boolean
		mclsProduct = New eProduct.Product
		
		Dim mclsProduct_ge As eProduct.Product_ge
		On Error GoTo insPostDP003_K_Err
		mclsProduct_ge = New eProduct.Product_ge
		
		'+ Esta asignación es para utilizar la información entrante en todas
		'+ las funciones llamadas dentro de insPostDP003_K, sin tener que pasarla como parámetro
		Dim mblnProduct_geExist As Object
		
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.sBrancht = CShort(sBrancht)
		Me.nUsercode = nUsercode
		Me.dEffecdate = dEffecdate
		
		insPostDP003_K = True
		mblnProduct_liExist = False
		mblnProduct_geExist = False
		
		Select Case nAction
			
			'+ Si la opción seleccionada es Registrar
			
			Case eFunctions.Menues.TypeActions.clngActionadd
                'If (sBrancht = CStr(pmBrancht.pmlife) Or sBrancht = CStr(pmBrancht.pmNotTraditionalLife)) Then
                If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
                    mblnProduct_liExist = mclsProduct.FindProduct_li(Me.nBranch, Me.nProduct, dEffecdate)
                End If
                Call insCreProdmaster()

                '+ Si la opción seleccionada es Consultar

            Case eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate
                insPostDP003_K = True
                If dEffecdate = eRemoteDB.Constants.dtmNull Then
                    insPostDP003_K = FindLastDate(nBranch, nProduct)
                End If
                If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                    If mclsProduct.Find(nBranch, nProduct, dEffecdate) Then
                        mclsProduct.dEffecdate = Me.dEffecdate
                        insPostDP003_K = mclsProduct.UpdateProduct
                        'If Me.sBrancht <> pmBrancht.pmlife Then
                        If (sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmMedicalAtention)) Then
                            mblnProduct_geExist = mclsProduct_ge.Find(nBranch, nProduct, dEffecdate)
                            mclsProduct_ge.dEffecdate = dEffecdate
                            mclsProduct_ge.nUsercode = nUsercode
                            insPostDP003_K = mclsProduct_ge.Update
                        Else
                            mblnProduct_liExist = mclsProduct.FindProduct_li(Me.nBranch, Me.nProduct, dEffecdate)
                            If mclsProduct.dEffecdate < dEffecdate Then
                                mclsProduct.dEffecdate = dEffecdate
                                mclsProduct.nUsercode = nUsercode
                                insPostDP003_K = mclsProduct.UpdProduct_liDPost
                            End If
                        End If
                    End If
                End If
        End Select
		
insPostDP003_K_Err: 
		If Err.Number Then
			insPostDP003_K = False
		End If
		
		'UPGRADE_NOTE: Object mclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsProduct = Nothing
		'UPGRADE_NOTE: Object mclsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsProduct_ge = Nothing
		On Error GoTo 0
		
	End Function
	'% insValDP003: Realiza la validación de los campos a actualizar en la ventana DP003.
	'  (Información General)
    Public Function insValDP003(ByVal sCodispl As String, Optional ByVal nAction As Integer = 0, Optional ByVal sStatregt As String = "", Optional ByVal nChkSimulator As Integer = 0, Optional ByVal sWin_declar As String = "", Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal nReference As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nVersion As Integer = 0, Optional ByVal nChkIndividual As Integer = 0, Optional ByVal nChkGroups As Integer = 0, Optional ByVal nChkMulti As Integer = 0, Optional ByVal nTypeHeap As Integer = 0, Optional ByVal nReinHeap As Integer = 0, Optional ByVal nCurrencyQ As Integer = 0, Optional ByVal sPreffix As String = "", Optional ByVal sAssociated_Policy_Required As String = "", Optional nAssociatedBranch As Integer = 0) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclsProduct As Product
        Dim lstrKeyProcess As String

        On Error GoTo insValDP003_Err

        lclsErrors = New eFunctions.Errors
        lclsProduct = New Product

        Call lclsProduct.Find(nBranch, nProduct, dEffecdate)

        '+ Validación sobre el campo de simulación
        '+ Si el producto es real y tiene pólizas asociadas, no puede pasar a ser de simulación.  11370 

        If Not String.IsNullOrEmpty(sStatregt) And sStatregt <> "0" Then
            If sStatregt = "1" AndAlso _
                CStr(lclsProduct.sRealind) = "1" AndAlso _
                nChkSimulator = eRemoteDB.Constants.intNull AndAlso _
                valPolicyExist(nBranch, nProduct) Then
                Call lclsErrors.ErrorMessage(sCodispl, 11370)
            End If
        End If

        '+ Validación sobre el campo de Declaraciones

        If sWin_declar <> "0" And sWin_declar <> String.Empty Then
            If Not FindWin_info(sWin_declar) Then
                Call lclsErrors.ErrorMessage(sCodispl, 99003)
            End If
        End If

        '+ Validación sobre la descripción del producto

        If sDescript = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 10071)
        End If

        '+ Validación sobre la descripción corta del producto

        If sShort_des = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 10006)
        End If

        '+ Validación sobre la referencia caso de que esté vacía y
        '+ se trata de una modificación o registro de un producto con información incompleta

        If nReference = 0 Or nReference = eRemoteDB.Constants.intNull Then
            If nAction = eFunctions.Menues.TypeActions.clngActionadd Or (nAction = eFunctions.Menues.TypeActions.clngActionUpdate And sStatregt = "2") Then
                Call lclsErrors.ErrorMessage(sCodispl, 97004)
            End If
        Else
            lstrKeyProcess = CStr(nBranch) & CStr(nProduct)
            If nAction = eFunctions.Menues.TypeActions.clngActionadd Or (nAction = eFunctions.Menues.TypeActions.clngActionUpdate And sStatregt = "2") Then
                If Not insReaProcess(nReference, 0, 1, lstrKeyProcess, True) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 97043)
                End If
            End If

            If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                If insReaProcess(nReference, 3, 1, lstrKeyProcess, False) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 97044)
                End If
            End If

            If nAction = eFunctions.Menues.TypeActions.clngActionUpdate And sStatregt = "2" Then
                If Not insReaProcess(nReference, 3, 1, lstrKeyProcess, False) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 97045)
                End If
            End If
        End If

        '+ Validación sobre la Versión del producto

        If nVersion = 0 Or nVersion = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 11291)
        End If

        '+ Validación sobre el Estado del Producto

        If sStatregt = String.Empty Or sStatregt = "0" Then
            sStatregt = "2"
        Else
            If (CStr(lclsProduct.sStatregt) = "1" Or CStr(lclsProduct.sStatregt) = "3") And sStatregt = "2" Then
                Call lclsErrors.ErrorMessage(sCodispl, 11218)
            End If
        End If

        '+ Validación sobre el tipo de póliza del producto

        If nChkIndividual <> 1 And nChkGroups <> 1 And nChkMulti <> 1 Then
            Call lclsErrors.ErrorMessage(sCodispl, 11026)
        End If

        '+ Validación sobre el tipo de cúmulo del producto

        If nTypeHeap = 0 Or nTypeHeap = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 11292)
        End If

        '+ Validación sobre el reaseguro del cúmulo

        If nReinHeap = 0 Or nReinHeap = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 11293)
        End If

        '+ Validación sobre la cantidad de monedas

        If nCurrencyQ = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 11368)
        ElseIf nCurrencyQ = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 11148)
        End If

        '+ Validación sobre poliza asociada
        If sAssociated_Policy_Required = "1" AndAlso (nAssociatedBranch = 0 OrElse nAssociatedBranch = eRemoteDB.Constants.intNull)  Then
            Call lclsErrors.ErrorMessage(sCodispl, 9000105)
        End If

        insValDP003 = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValDP003_Err:
        If Err.Number Then
            insValDP003 = "insValDP003: " & Err.Description
        End If
        On Error GoTo 0
    End Function
	
	'% insPreDP003: Esta rutina realiza la lectura de los campos para en el frame DP003
	Public Function insPreDP003(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal bQuery As Boolean) As Boolean
		Dim lclsProduct As Product
		Dim lclsProcess As eGeneral.Process
		
		On Error GoTo insPreDP003_Err
		insPreDP003 = True
		
		lclsProcess = New eGeneral.Process
		lclsProduct = New Product
		
		If Find(nBranch, nProduct, dEffecdate) Then
			With lclsProcess
				.nBranch = nBranch
				.nProduct = nProduct
				.nCode_proce = 1
				.nCode_activ = 3
				If .FindProcess_v Then
					nReference = .nReference
				Else
					nReference = 0
				End If
			End With
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If IsDbNull(sCumultyp) Or Trim(sCumultyp) = String.Empty Then
				sCumreint = "3"
				bCumreintDisabled = True
			Else
				'+ Si en el campo Tipo de Cúmulo hay el valor "No tiene",
				'+ entonces no se habilita el campo Reaseguro
				If Trim(sCumultyp) <> String.Empty And sCumultyp <> String.Empty Then
					If sCumultyp <> "4" Then
						If bQuery = True Then
							bCumreintDisabled = True
						Else
							bCumreintDisabled = False
						End If
					Else
						sCumreint = "3"
						bCumreintDisabled = True
					End If
				Else
					sCumreint = "3"
					bCumreintDisabled = True
				End If
			End If
		Else
			insPreDP003 = False
		End If
		
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsProcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProcess = Nothing
		
insPreDP003_Err: 
		If Err.Number Then
			insPreDP003 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% insPostDP003: Valida los datos introducidos en la zona de contenido para "frame" especifico
    Public Function insPostDP003(ByVal nAction As Integer, ByVal sCodispl As String, Optional ByVal nReference As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal sBrancht As String = "", Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sStatregt As String = "", Optional ByVal sRealind As String = "", Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nChkIndividual As Integer = 0, Optional ByVal nChkGroups As Integer = 0, Optional ByVal nChkMulti As Integer = 0, Optional ByVal nTariff As Integer = 0, Optional ByVal sCumultyp As String = "", Optional ByVal sCumreint As String = "", Optional ByVal sWin_declar As String = "", Optional ByVal nQmaxcurr As Integer = 0, Optional ByVal sNumprop As String = "", Optional ByVal sCondSVS As String = "", Optional ByVal sAuto_susc As String = "", Optional ByVal sMassive As String = "", Optional ByVal sRatingServiceUsing As String = "", Optional ByVal sPreffix As String = "", Optional ByVal sAssociated_Policy_Required As String = "", Optional ByVal nAssociatedBranch As Integer = 0, Optional ByVal nTypeAccount As Integer = 0, Optional ByVal nModuleMin As Integer = 0, Optional ByVal sAutomaticBill As String = "2", Optional ByVal sResolutionSBS As String = "") As Boolean
        Dim lclsProcess As Object

        insPostDP003 = Find(nBranch, nProduct, dEffecdate)

        If insPostDP003 Then
            lclsProcess = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Process")

            '+ Esta asignación es para utilizar la información entrante en todas
            '+ las funciones llamadas dentro de insPostDP003, sin tener que pasarla como parámetro

            Me.nReference = nReference
            Me.nBranch = nBranch
            Me.nProduct = nProduct
            Me.sCodispl = sCodispl
            Me.nUsercode = nUsercode
            Me.sBrancht = CShort(sBrancht)
            Me.sDescript = sDescript
            Me.sShort_des = sShort_des
            Me.sStatregt = CShort(sStatregt)
            Me.sRealind = IIf(sRealind = CStr(eRemoteDB.Constants.intNull), "2", sRealind)
            Me.dEffecdate = dEffecdate
            Me.sIndivind = IIf(nChkIndividual = eRemoteDB.Constants.intNull, "2", nChkIndividual)
            Me.sGroupind = IIf(nChkGroups = eRemoteDB.Constants.intNull, "2", nChkGroups)
            Me.sMultiind = IIf(nChkMulti = eRemoteDB.Constants.intNull, "2", nChkMulti)
            Me.nTariff = nTariff
            Me.sCumultyp = sCumultyp
            Me.sCumreint = sCumreint
            Me.sWin_declar = sWin_declar
            Me.nQmaxcurr = nQmaxcurr
            Me.sNumprop = IIf(sNumprop = String.Empty, "2", sNumprop)
            Me.sCondSVS = sCondSVS
            Me.sAuto_susc = IIf(sAuto_susc = String.Empty, "2", sAuto_susc)
            Me.sMassive = IIf(sMassive = String.Empty, "2", sMassive)
            Me.sRatingServiceUsing = IIf(sRatingServiceUsing = String.Empty, "2", sRatingServiceUsing)
            Me.sPreffix = sPreffix

            Me.sAssociated_Policy_Required = sAssociated_Policy_Required
            Me.nAssociatedBranch = nAssociatedBranch
            Me.nTypeAccount = nTypeAccount
            Me.nModuleMin = nModuleMin
            Me.sAutomaticBill = sAutomaticBill
            Me.sResolutionSBS = sResolutionSBS

            insPostDP003 = True

            If Me.nReference <> 0 And Me.nReference <> eRemoteDB.Constants.intNull Then
                If Not lclsProcess.Find(Me.nReference, 3, 1, CStr(Me.nBranch) & CStr(Me.nProduct)) Then
                    insPostDP003 = insCreProcess()
                End If
            End If

            insPostDP003 = Update_DP003()
            If insPostDP003 Then
                insPostDP003 = insUpdProdmaster()
            End If
        End If
        'UPGRADE_NOTE: Object lclsProcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProcess = Nothing
    End Function
	
	'% insValDP005: Realiza la validación de los campos a actualizar en el frame (ventana) DP005
	'  (Respuestas automáticas en la Emisión)
	Public Function insValDP005(ByVal sCodispl As String, Optional ByVal nCopies As Integer = 0, Optional ByVal nDuration As Integer = 0, Optional ByVal nPolitype As Integer = 0, Optional ByVal sIndivind As String = "", Optional ByVal sGroupind As String = "", Optional ByVal sMultiind As String = "", Optional ByVal nPayFreq As Integer = 0, Optional ByVal nQuota As Integer = 0, Optional ByVal nReintype As Integer = 0, Optional ByVal nRevalapl As Integer = 0, Optional ByVal nRevaltyp As Integer = 0, Optional ByVal nRevalrat As Double = 0, Optional ByVal nHolder As Integer = 0, Optional ByVal nTimeren As Integer = 0, Optional ByVal nStyle_prem As Integer = 0, Optional ByVal nStyle_tax As Integer = 0, Optional ByVal nStyle_comm As Integer = 0) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP005_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se valida el campo de las copias
		
		If nCopies = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 70000)
		Else
			If nCopies <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11356)
			End If
		End If
		
		'+ Se valida el campo de meses de duración
		
		If nDuration = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 70001)
		ElseIf nDuration < 0 Then 
			Call lclsErrors.ErrorMessage(sCodispl, 55874)
		End If
		
		'+ Se valida el campo del tipo de póliza que debe estar lleno
		
		Select Case nPolitype
			Case 0
				Call lclsErrors.ErrorMessage(sCodispl, 70002)
			Case 1
				If sIndivind <> "1" Then
					Call lclsErrors.ErrorMessage(sCodispl, 11200)
				End If
			Case 2
				If sGroupind <> "1" Then
					Call lclsErrors.ErrorMessage(sCodispl, 11200)
				End If
			Case 3
				If sMultiind <> "1" Then
					Call lclsErrors.ErrorMessage(sCodispl, 11200)
				End If
		End Select
		
		'+ Se valida la frecuencia de pago que debe estar lleno
		
		If nPayFreq <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70003)
		End If
		
		'+ Se valida el campo cuotas que esté lleno si la frecuencia de pago es por cuotas
		
		If nPayFreq = 8 And nQuota <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3799)
		Else
			If nPayFreq = 8 And nQuota = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 3799)
			End If
		End If
		
		
		
		'+ Se valida el campo de reaseguro que debe estar lleno
		
		If nReintype <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70004)
		End If
		
		'+ Se valida el campo de revalorización (forma) que debe estar lleno
		
		If nRevalapl <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70005)
		Else
			If nRevalapl <> 3 And nRevaltyp = 4 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11367)
			End If
		End If
		
		'+ Se valida el Campo de revalorización (tipo) que debe estar lleno
		
		If nRevaltyp <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70006)
		End If
		
		'+ Se valida el campo del factor de revalorización que debe estar lleno si en el campo
		'+ De tipo de revalorización se encuentra la opción de Factor fijo
		
		If nRevaltyp = 3 Then
			If nRevalrat = eRemoteDB.Constants.intNull Or nRevalrat = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 3025)
			End If
		End If
		
		'+ Se valida el campo de titular de recibo que debe estar lleno
		
		If nHolder <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70007)
		End If
		
		'+ Se valida el campo de colectivo que debe estar lleno
		
		If nTimeren <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70008)
		End If
		
		'+ Se valida moneda del recibo prima que debe estar lleno
		
		If nStyle_prem <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70009)
		End If
		
		'+ Se valida moneda del recibo impuesto que debe estar lleno
		
		If nStyle_tax <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70010)
		End If
		
		'+ Se valida moneda del recibo comisión que debe estar lleno
		
		If nStyle_comm <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70011)
		End If
		
		insValDP005 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValDP005_Err: 
		If Err.Number Then
			insValDP005 = insValDP005 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insPostDP005: Valida los datos introducidos en la zona de contenido para "frame" especifico
	'+Cambio de aplicacion segun  requerimiento de la hoja de analisis n°9
	'autor: Hans Alvarez - (19-10-2001)  
    Public Function insPostDP005(ByVal nAction As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nCopies As Integer = 0, Optional ByVal nDuration As Integer = 0, Optional ByVal nCancnoti As Integer = 0, Optional ByVal sPolitype As String = "", Optional ByVal nPayFreq As Integer = 0, Optional ByVal sReintype As String = "", Optional ByVal sDeclaaut As String = "", Optional ByVal sRevalapl As String = "", Optional ByVal sRevaltyp As String = "", Optional ByVal nRevalrat As Double = 0, Optional ByVal sRenewal As String = "", Optional ByVal sTimeren As String = "", Optional ByVal sStyle_prem As String = "", Optional ByVal sStyle_tax As String = "", Optional ByVal sStyle_comm As String = "", Optional ByVal sHolder As String = "", Optional ByVal nQuota As Integer = 0, Optional ByVal nWay_pay As Integer = 0, Optional ByVal nBill_day As Integer = 0, Optional ByVal sTyp_dom As String = "", Optional ByVal sLeg As String = "", Optional ByVal nRehabperiod As Integer = 0, Optional ByVal sReinst As String = "", Optional ByVal sDatecoll As String = "", Optional ByVal sFirst_pay As String = "", Optional ByVal nQdays_quo As Integer = 0, Optional ByVal nQdays_pro As Integer = 0, Optional ByVal nMonth_surr As Integer = 0, Optional ByVal nNotCancelDay As Integer = 0, Optional ByVal sRehaut_r As String = "", Optional ByVal sFracReceip As String = "", Optional ByVal nQDays_DifQuo As Integer = 0, Optional ByVal sSetprem As String = "", Optional ByVal nMonth_Setpr As Integer = 0, Optional ByVal sRetarif As String = "", Optional ByVal sRecSec As String = "", Optional ByVal nRehabperiod_aut As Integer = 0, Optional ByVal sTarQuo_Ind As String = "", Optional ByVal nPayable As Integer = 0, Optional ByVal nAdvance As Integer = 0, Optional ByVal sReactivation As String = "", Optional ByVal nReactPeriod As Short = 0, Optional ByVal nReactPeriod_Aut As Short = 0, Optional ByVal sRoutReact As String = "", Optional ByVal nChUserLev As Short = 0, Optional ByVal sRou_warning_charg As String = "", Optional ByVal sRou_cover As String = "", Optional ByVal nCurr_receipt As Integer = 0) As Boolean
        Dim lobjValues As eFunctions.Values
        Dim lclsCliallopro As Cliallopro
        Dim lclsProd_win As eProduct.Prod_win
        Dim lblnDP042 As Boolean
        Dim lclsPolicy As Object


        On Error GoTo insPostDP005_err
        lobjValues = New eFunctions.Values
        lclsCliallopro = New Cliallopro
        lclsProd_win = New eProduct.Prod_win

        If Find(nBranch, nProduct, dEffecdate) Then
            '+ Esta asignación es para utilizar la información entrante en todas
            '+ las funciones llamadas dentro de insPostDP005, sin tener que pasarla como parámetro
            With Me
                .nBranch = nBranch
                .nProduct = nProduct
                .dEffecdate = dEffecdate
                .nUsercode = nUsercode
                .nCopies = nCopies
                .nDuration = nDuration
                .nCancnoti = nCancnoti
                .sPolitype = sPolitype
                .nPayFreq = nPayFreq
                .sReintype = sReintype
                .sDeclaaut = IIf(Trim(sDeclaaut) = String.Empty, "2", sDeclaaut)
                .sRevalapl = sRevalapl
                .sRevaltyp = sRevaltyp
                .nRevalrat = nRevalrat
                .sRenewal = IIf(Trim(sRenewal) = String.Empty, "2", sRenewal)
                .sTimeren = sTimeren
                .sStyle_prem = sStyle_prem
                .sStyle_tax = sStyle_tax
                .sStyle_comm = sStyle_comm
                .sHolder = sHolder
                .nQuota = lobjValues.StringToType(CStr(nQuota), eFunctions.Values.eTypeData.etdInteger)
                .nWay_pay = nWay_pay
                .nBill_day = nBill_day
                .sTyp_dom = sTyp_dom
                .sLeg = IIf(sLeg = String.Empty, "2", sLeg)
                .nRehabperiod = nRehabperiod
                .nRehabperiod_aut = nRehabperiod_aut
                .sReinst = IIf(sReinst = String.Empty, "2", sReinst)
                .sDatecoll = IIf(sDatecoll = String.Empty, "2", sDatecoll)
                .sFirst_pay = IIf(sFirst_pay = String.Empty, "2", sFirst_pay)
                .nQdays_quo = nQdays_quo
                .nQdays_pro = nQdays_pro
                .nMonth_surr = nMonth_surr
                .nNotCancelDay = nNotCancelDay
                .sRoutaut_r = sRehaut_r
                .sFracReceip = IIf(sFracReceip = String.Empty, "2", sFracReceip)
                .nQDays_DifQuo = nQDays_DifQuo
                .sSetprem = IIf(sSetprem = String.Empty, "2", sSetprem)
                .nMonth_Setpr = nMonth_Setpr
                .sRetarif = IIf(sRetarif = String.Empty, "2", sRetarif)
                .sRecSec = IIf(sRecSec = String.Empty, "2", sRecSec)
                .sTarQuo_Ind = IIf(sTarQuo_Ind = String.Empty, "2", sTarQuo_Ind)
                .nPayable = nPayable
                .nAdvance = nAdvance
                .sReactivation = sReactivation
                .nReactPeriod = nReactPeriod
                .nReactPeriod_Aut = nReactPeriod_Aut
                .sRoutReact = sRoutReact
                .nChUserLev = nChUserLev
                .sRou_warning_charg = sRou_warning_charg
                .sRou_cover = sRou_cover
                .nCurr_receipt = nCurr_receipt

                Select Case nAction
                    '+ Si la opción seleccionada es Registrar o Modificar
                    Case eFunctions.Menues.TypeActions.clngActionadd, eFunctions.Menues.TypeActions.clngActionUpdate
                        insPostDP005 = Update_DP005()
                End Select

                If insPostDP005 Then
                    If Me.sIndivind = "1" Then
                        If Not lclsCliallopro.Find(.nBranch, .nProduct, "1", "1", lobjValues.StringToType(IIf(.sHolder = "3", "25", .sHolder), eFunctions.Values.eTypeData.etdInteger)) Then
                            lclsCliallopro.nBranch = .nBranch
                            lclsCliallopro.nProduct = .nProduct
                            lclsCliallopro.sPolitype = "1"
                            lclsCliallopro.sCompon = "1"
                            lclsCliallopro.nRole = lobjValues.StringToType(IIf(.sHolder = "3", "25", .sHolder), eFunctions.Values.eTypeData.etdInteger)
                            lclsCliallopro.sDefaulti = "1"
                            lclsCliallopro.sRequire = "1"
                            lclsCliallopro.nMax_role = 1
                            lclsCliallopro.nusercode = nUsercode
                            If lclsCliallopro.Add Then
                                lblnDP042 = True
                            End If
                        End If
                    End If

                    If Me.sGroupind = "1" Or Me.sMultiind = "1" Then
                        If .sHolder = "2" Then
                            If Not lclsCliallopro.Find(.nBranch, .nProduct, "2", "2", lobjValues.StringToType(IIf(.sHolder = "3", "25", .sHolder), eFunctions.Values.eTypeData.etdInteger)) Then
                                lclsCliallopro.nBranch = .nBranch
                                lclsCliallopro.nProduct = .nProduct
                                lclsCliallopro.sPolitype = "2"
                                lclsCliallopro.sCompon = "2"
                                lclsCliallopro.nRole = lobjValues.StringToType(IIf(.sHolder = "3", "25", .sHolder), eFunctions.Values.eTypeData.etdInteger)
                                lclsCliallopro.sDefaulti = "1"
                                lclsCliallopro.sRequire = "1"
                                lclsCliallopro.nMax_role = 1
                                lclsCliallopro.nusercode = nUsercode
                                If lclsCliallopro.Add Then
                                    lblnDP042 = True
                                End If
                            End If
                        Else
                            If Not lclsCliallopro.Find(.nBranch, .nProduct, "2", "1", lobjValues.StringToType(IIf(.sHolder = "3", "25", .sHolder), eFunctions.Values.eTypeData.etdInteger)) Then
                                lclsCliallopro.nBranch = .nBranch
                                lclsCliallopro.nProduct = .nProduct
                                lclsCliallopro.sPolitype = "2"
                                lclsCliallopro.sCompon = "1"
                                lclsCliallopro.nRole = lobjValues.StringToType(IIf(.sHolder = "3", "25", .sHolder), eFunctions.Values.eTypeData.etdInteger)
                                lclsCliallopro.sDefaulti = "1"
                                lclsCliallopro.sRequire = "1"
                                lclsCliallopro.nMax_role = 1
                                lclsCliallopro.nusercode = nUsercode
                                If lclsCliallopro.Add Then
                                    lblnDP042 = True
                                End If
                            End If
                        End If
                    End If

                    If lblnDP042 Then
                        Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP042", "2", nUsercode)
                    End If

                    If nPayFreq <> 0 And nPayFreq <> eRemoteDB.Constants.intNull Then
                        lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Pay_Fracti")
                        If Not lclsPolicy.Find(.nBranch, .nProduct, .nPayFreq, .nQuota, .dEffecdate) Then
                            Call lclsPolicy.insPostDP010(1, .nBranch, .nProduct, .nPayFreq, .dEffecdate, 0, "1", .nQuota, .nUsercode)
                        End If
                        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsPolicy = Nothing
                    End If
                End If
            End With
        End If

insPostDP005_err:
        If Err.Number Then
            insPostDP005 = False
        End If
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lclsCliallopro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCliallopro = Nothing
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        On Error GoTo 0
    End Function
	
	'% insValDP042: Realiza la validación de los campos a actualizar en el frame (ventana) DP042
	'  (clientes permitidos para todos los tipos de póliza)
	Public Function insValDP042(ByVal sCodispl As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sPolitype As String = "", Optional ByVal sIndivind As String = "", Optional ByVal sGroupind As String = "", Optional ByVal sMultiind As String = "", Optional ByVal sRequire As String = "", Optional ByVal sSelected As String = "", Optional ByVal nCounter As Integer = 0, Optional ByVal sDefaulti As String = "", Optional ByVal nMax_role As Integer = 0, Optional ByVal blnMassive As Boolean = False) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As Product
		Dim lcolCliallopros As Cliallopros
		Dim lcolCliallopro As Cliallopro
        Dim lstrPolitype As String = ""
        Dim lblnExistClient As Boolean
		Dim lblnExistRequired As Boolean
		Dim lintCount As Integer
		Dim lblnRequired As Boolean
		Dim lblnIndividual As Boolean
		Dim lblnColMat As Boolean
		Dim lblnColCer As Boolean
		Dim lblnMulMat As Boolean
		Dim lblnMulCer As Boolean
		Dim lblnError As Boolean
		
		On Error GoTo insValDP042_Err
		
		lclsErrors = New eFunctions.Errors
		lcolCliallopros = New Cliallopros
		lcolCliallopro = New Cliallopro
		
		If Not blnMassive Then
			If sIndivind = "2" And sGroupind = "2" And sMultiind = "2" Then
				'            Call lclsErrors.ErrorMessage(sCodispl, 11349)
			Else
				Select Case sPolitype
					'+ Corresponde a un Póliza Individual
					Case "1"
						lstrPolitype = "Individual."
						'+ Corresponde a una Póliza Colectiva
					Case "2"
						lstrPolitype = "Colectiva-Póliza matriz."
						'+ Corresponde a una Póliza Multilocalidad
					Case "3"
						lstrPolitype = "Multilocalidad-Póliza matriz."
				End Select
				'+ Se valida que al menos haya una figura de cliente seleccionada para los tipos de
				'+ Póliza asociados al producto. Para el cado de Colectivas y Multilocalidad se valida
				'+ también que haya una figura requerida
				If sSelected = "1" Then
					lblnExistClient = True
					If (sPolitype = "1" Or sPolitype = "3") And sRequire = "1" Then
						lblnExistRequired = True
					End If
				End If
				'+ Si la póliza es colectiva o multilocalidad
				'+ debe existir por lo menos una figura requerida para la póliza matriz
				If (sPolitype = "2" Or sPolitype = "3") Then
					If lcolCliallopros.Find(nBranch, nProduct, dEffecdate, "2") Then
						For lintCount = 1 To lcolCliallopros.Count
                            If lcolCliallopros.Item(lintCount).sRequire = "2" Then
                                lblnRequired = False
                            Else
                                lblnRequired = True
                                Exit For
                            End If
                        Next
					End If
					If Not lblnRequired Then
						Call lclsErrors.ErrorMessage(sCodispl, 11286,  ,  , lstrPolitype)
					End If
				End If
			End If
			If sRequire = "1" And sDefaulti <> "1" Then
				Call lclsErrors.ErrorMessage(sCodispl, 11330, nCounter + 1,  , lstrPolitype)
			End If
		Else
			lclsProduct = New eProduct.Product
			If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
				'+ Si la póliza es Individual
				If lclsProduct.sIndivind = "1" Then
					If Not lcolCliallopro.FindDP042(nBranch, nProduct, dEffecdate, "1", "1") Then
						Call lclsErrors.ErrorMessage(sCodispl, 11224,  ,  , " (Individual)")
					End If
				End If
				'+ Si la póliza es colectiva
				If lclsProduct.sGroupind = "1" Then
					'+ Si la póliza es colectiva - matriz
					If Not lcolCliallopro.FindDP042(nBranch, nProduct, dEffecdate, "2", "1") Then
						Call lclsErrors.ErrorMessage(sCodispl, 11224,  ,  , " (Colectiva-Póliza matriz)")
					End If
					'+ Si la póliza es colectiva - certificado
					If Not lcolCliallopro.FindDP042(nBranch, nProduct, dEffecdate, "2", "2") Then
						Call lclsErrors.ErrorMessage(sCodispl, 11224,  ,  , " (Colectiva-Certificado)")
					End If
				End If
				'+ Si la póliza es multilocalidad
				If lclsProduct.sMultiind = "1" Then
					'+ Si la póliza es multilocalidad -póliza
					If Not lcolCliallopro.FindDP042(nBranch, nProduct, dEffecdate, "3", "1") Then
						Call lclsErrors.ErrorMessage(sCodispl, 11224,  ,  , " (Multilocalidad-Póliza matriz)")
					End If
				End If
			End If
		End If
		
		insValDP042 = lclsErrors.Confirm
		
insValDP042_Err: 
		If Err.Number Then
			insValDP042 = "insValDP042: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lcolCliallopro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCliallopro = Nothing
		'UPGRADE_NOTE: Object lcolCliallopros may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCliallopros = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostDP042: Valida los datos introducidos en la zona de contenido para "frame" especifico
	Public Function insPostDP042(ByVal nAction As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sPolitype As String = "", Optional ByVal sCompon As String = "", Optional ByVal nRole As Integer = 0, Optional ByVal sDefaulti As String = "", Optional ByVal sRequire As String = "", Optional ByVal nMax_role As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal sSelected As String = "", Optional ByVal sInit_sel As String = "", Optional ByVal nExist As Integer = 0, Optional ByVal sOptionalQuo As String = "") As Boolean
		
		On Error GoTo insPostDP042_Err
		
		mclsCliallopro = New eProduct.Cliallopro
		
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.dEffecdate = dEffecdate
		Me.nUsercode = nUsercode
		mstrSelected = sSelected
		mstrInit_sel = sInit_sel
		mintExist = nExist
		
		With mclsCliallopro
			.nBranch = Me.nBranch
			.nProduct = Me.nProduct
			.sPolitype = sPolitype
			.sCompon = sCompon
			.nRole = nRole
			.sDefaulti = IIf(sDefaulti = String.Empty, "2", sDefaulti)
			.sRequire = IIf(sRequire = String.Empty, "2", sRequire)
			.nMax_role = nMax_role
			.sOptionalQuo = sOptionalQuo
			.nUsercode = Me.nUsercode
		End With
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			insPostDP042 = insUpdCliallopro
		Else
			insPostDP042 = True
		End If
		'UPGRADE_NOTE: Object mclsCliallopro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCliallopro = Nothing
		
insPostDP042_Err: 
		If Err.Number Then
			insPostDP042 = False
		End If
		On Error GoTo 0
	End Function
	
	'insValDP023: Esta rutina realiza la validacion de los datos del colectivo
	Public Function insValDP023(ByVal sCodispl As String, ByVal sGroupsi As String, ByVal nInsmaxiq As Double, ByVal nInsminiq As Double, ByVal sColinvot As String, ByVal sTyp_module As String, ByVal sTyp_discxp As String, ByVal sTyp_clause As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP023_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Validación de usar grupos
			If sGroupsi = String.Empty Then
				'+ Si el tipo de tratamiento de coberturas/módulos, recargos/descuentos/impuestos
				'+ o cláusulas es por grupo, se deben indicar "Usar grupos"
				If sTyp_module = "3" Or sTyp_discxp = "3" Or sTyp_clause = "3" Then
					Call .ErrorMessage(sCodispl, 11375)
				End If
			End If
			
			'+ Validación de número máximo de asegurados
			
			If nInsmaxiq <> eRemoteDB.Constants.intNull Then
				If nInsminiq > nInsmaxiq Or nInsminiq = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 11175)
				End If
			End If
			
			'+ Validación global
			'+ Validación del tipo de recibo
			
			If sColinvot = "0" Then
				Call .ErrorMessage(sCodispl, 3037)
			End If
			
			'+ Validación de coberturas
			
			If sTyp_module = "0" Then
				Call .ErrorMessage(sCodispl, 3294)
			End If
			
			'+ Validación de recargos y descuentos
			
			If sTyp_discxp = "0" Then
				Call .ErrorMessage(sCodispl, 3296)
			End If
			
			'+ Validación de tipo de cláusulas
			
			If sTyp_clause = "0" Then
				Call .ErrorMessage(sCodispl, 3295)
			End If
			
			insValDP023 = .Confirm
		End With
		
insValDP023_Err: 
		If Err.Number Then
			insValDP023 = "insValDP023: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostDP023: Valida los datos introducidos en la zona de contenido para "frame" especifico
	Public Function insPostDP023(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sColinvot As String, ByVal sTyp_clause As String, ByVal sTyp_discxp As String, ByVal sTyp_module As String, ByVal nInsmaxiq As Double, ByVal nInsminiq As Double, ByVal sGroupsi As String, ByVal nUsercode As Integer, ByVal sOptPremCalc As String, ByVal sBrancht As String, ByVal nRepInsured As Integer) As Boolean
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo insPostDP023_Err
		
		insPostDP023 = Find(nBranch, nProduct, dEffecdate)
		
		If insPostDP023 Then
			
			'+ Esta asignación es para utilizar la información entrante en todas
			'+ las funciones llamadas dentro de insPostDP023, sin tener que pasarla como parámetro
			
			With Me
				.nBranch = nBranch
				.nProduct = nProduct
				.dEffecdate = dEffecdate
				.sColinvot = sColinvot
				.sTyp_clause = sTyp_clause
				.sTyp_discxp = sTyp_discxp
				.sTyp_module = sTyp_module
				.nInsmaxiq = nInsmaxiq
				.nInsminiq = nInsminiq
				.sGroupsi = IIf(sGroupsi = String.Empty, "2", "1")
				.nUsercode = nUsercode
				.dNulldate = eRemoteDB.Constants.dtmNull
				.nQ_certif = eRemoteDB.Constants.intNull
				.nRepInsured = nRepInsured
			End With
			
			'+ Si la opción seleccionada es Registrar o Modificar
			insPostDP023 = UpdateProduct
			
			If insPostDP023 Then
                'If sBrancht = CStr(pmBrancht.pmlife) Or sBrancht = CStr(pmBrancht.pmNotTraditionalLife) Then
                If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
                    If FindProduct_li(nBranch, nProduct, dEffecdate, True) Then
                        sMethprav = IIf(sOptPremCalc = "1", "1", "2")
                        sMethagav = IIf(sOptPremCalc = "2", "1", "2")
                        sMethprin = IIf(sOptPremCalc = "3", "1", "2")
                        If dEffecdate = Me.dEffecdate Then
                            insPostDP023 = UpdateProduct_Li()
                        Else
                            Me.dEffecdate = dEffecdate
                            insPostDP023 = UpdProduct_liDPost()
                        End If
                    End If
                End If
                lclsProd_win = New eProduct.Prod_win
                '+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parametro.
                insPostDP023 = lclsProd_win.Add_Prod_win(Me.nBranch, Me.nProduct, Me.dEffecdate, "DP023", "2", Me.nUsercode)
            End If
        End If

insPostDP023_Err:
        If Err.Number Then
            insPostDP023 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
	End Function
	
	'% insReaProcess: Valida que exista la referencia en la tabla process para el proceso y actividad dada
	Private Function insReaProcess(ByVal llngReference As Integer, ByVal lintActiv As Integer, ByVal lintProcess As Integer, ByVal lstrKeyProcess As String, ByVal lblnActiv As Boolean) As Boolean
		Dim lclsProcess As eGeneral.Process
		
		On Error GoTo insReaProcess_Err
		
		lclsProcess = New eGeneral.Process
		
		'+ Se envían como parámetros la referencia, código del proceso, código de la actividad
		
		With lclsProcess
			.nReference = llngReference
			.nCode_proce = lintProcess
			.nCode_activ = lintActiv
			.sKey_process = lstrKeyProcess
			
			'+ Si se está validando es la existencia
			
			insReaProcess = .FindProcessByProduct(lblnActiv)
		End With
		'UPGRADE_NOTE: Object lclsProcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProcess = Nothing
		
insReaProcess_Err: 
		If Err.Number Then
			insReaProcess = False
		End If
		On Error GoTo 0
	End Function
	
	'% insCreProcess: Crea la referencia del ramo y producto en la tabla 'Process'
	Private Function insCreProcess() As Boolean
		Dim lclsProcess As eGeneral.Process
		
		On Error GoTo insCreProcess_Err
		
		lclsProcess = New eGeneral.Process
		
		With lclsProcess
			.nReference = Me.nReference
			.nCode_activ = 3
			.nCode_proce = 1
			.sKey_process = CStr(Me.nBranch) & CStr(Me.nProduct)
			.nBranch = Me.nBranch
			.sCodispl = Me.sCodispl
			.nProduct = Me.nProduct
			.sStartHour = Mid(CStr(TimeOfDay), 1, 8)
			.nStatus_pro = 1
			.nUsercode = Me.nUsercode
			insCreProcess = .AddProduct
		End With
		'UPGRADE_NOTE: Object lclsProcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProcess = Nothing
		
insCreProcess_Err: 
		If Err.Number Then
			insCreProcess = False
		End If
		On Error GoTo 0
	End Function
	
	'% insUpdProdmaster: Asigna los valores del encabezado que serán actualizados sobre la tabla 'Prodmaster'
	Private Function insUpdProdmaster() As Boolean
		
		Dim lclsProduct As eProduct.Product
		
		On Error GoTo insUpdProdmaster_Err
		
		lclsProduct = New eProduct.Product
		
		With lclsProduct
			.nBranch = Me.nBranch
			.nProduct = Me.nProduct
			.nUsercode = Me.nUsercode
			.sBrancht = Me.sBrancht
			.sDescript = Me.sDescript
			.sShort_des = Me.sShort_des
			.sStatregt = Me.sStatregt
			.sRealind = Me.sRealind
            .nCompany = eRemoteDB.Constants.intNull
            .sPreffix = Me.sPreffix
			
			insUpdProdmaster = .UpdateProdmaster
		End With
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		
insUpdProdmaster_Err: 
		If Err.Number Then
			insUpdProdmaster = False
		End If
		On Error GoTo 0
	End Function
	
	'% Update_DP003: Realiza la asignación de los valores del frame activo
	'% a los parametros correspondientes del store-procedure que realiza
	'% el mantenimiento de la historia en la estructura 'Product'
	Private Function Update_DP003() As Boolean
		Dim lclsProduct As eProduct.Product
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo Update_DP003_Err
		
		lclsProd_win = New eProduct.Prod_win
		lclsProduct = New eProduct.Product
		
		Update_DP003 = True
		
		'+ Asignación de los campos claves
		If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
			'+ Asignación de parametros para el frame 'DP003'
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				nTariff = eRemoteDB.Constants.intNull
			End If
			dNulldate = eRemoteDB.Constants.dtmNull
			nQ_certif = eRemoteDB.Constants.intNull
		Else
			nQ_certif = eRemoteDB.Constants.intNull
			sIndivind = String.Empty
			sGroupind = String.Empty
			sMultiind = String.Empty
			nTariff = 1
			sCumultyp = String.Empty
			sCumreint = String.Empty
			sWin_declar = String.Empty
			nQdays_pro = eRemoteDB.Constants.intNull
			nQmaxcurr = eRemoteDB.Constants.intNull
			sColinvot = String.Empty
			sTyp_clause = String.Empty
			sTyp_discxp = String.Empty
			sTyp_module = String.Empty
			nRehabperiod_aut = eRemoteDB.Constants.intNull
			nInsmaxiq = eRemoteDB.Constants.intNull
			nInsminiq = eRemoteDB.Constants.intNull
			sGroupsi = String.Empty
			nCopies = eRemoteDB.Constants.intNull
			nDuration = eRemoteDB.Constants.intNull
			nCancnoti = eRemoteDB.Constants.intNull
			sPolitype = String.Empty
			nPayFreq = eRemoteDB.Constants.intNull
			sReintype = String.Empty
			sDeclaaut = String.Empty
			sRevalapl = String.Empty
			sRevaltyp = String.Empty
			nRevalrat = eRemoteDB.Constants.intNull
			sRenewal = String.Empty
			sTimeren = String.Empty
			sStyle_prem = String.Empty
			sStyle_tax = String.Empty
			sStyle_comm = String.Empty
			sHolder = String.Empty
			nQuota = eRemoteDB.Constants.intNull
			nClaim_pres = eRemoteDB.Constants.intNull
			nWay_pay = eRemoteDB.Constants.intNull
			nBill_day = eRemoteDB.Constants.intNull
			sTyp_dom = String.Empty
			sLeg = String.Empty
			nRehabperiod = eRemoteDB.Constants.intNull
			sReinst = String.Empty
			sDatecoll = String.Empty
			sFirst_pay = String.Empty
			nQdays_quo = eRemoteDB.Constants.intNull
			nMonth_surr = eRemoteDB.Constants.intNull
			nClaim_Notice = eRemoteDB.Constants.intNull
			nClaim_Pay = eRemoteDB.Constants.intNull
			nNotCancelDay = eRemoteDB.Constants.intNull
			sSetprem = String.Empty
			nMonth_Setpr = eRemoteDB.Constants.intNull
			sRetarif = "2"
		End If
		Update_DP003 = UpdateProduct
		
		'+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parametro.
		Call lclsProd_win.Add_Prod_win(Me.nBranch, Me.nProduct, Me.dEffecdate, "DP003", "2", Me.nUsercode)
		
Update_DP003_Err: 
		If Err.Number Then
			Update_DP003 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'% insProduct_ge: Actualiza en la tabla Product_ge los valores introducidos
	Private Function insProduct_ge() As Boolean
		
		Dim lclsProduct_ge As eProduct.Product_ge
		
		On Error GoTo insProduct_ge_Err
		
		lclsProduct_ge = New eProduct.Product_ge
		
		insProduct_ge = True
		With lclsProduct_ge
			.nBranch = Me.nBranch
			.nProduct = Me.nProduct
			.dEffecdate = Me.dEffecdate
			.nCurrency = eRemoteDB.Constants.intNull
			.sFrancApl = String.Empty
			.nFrancMax = eRemoteDB.Constants.intNull
			.nFrancMin = eRemoteDB.Constants.intNull
			.nFrancrat = eRemoteDB.Constants.intNull
			.sFrantype = String.Empty
			.nLevelPay = eRemoteDB.Constants.intNull
			.sPayconre = String.Empty
			.nPre_amend = eRemoteDB.Constants.intNull
			.nPre_issue = eRemoteDB.Constants.intNull
			.sResemedi = String.Empty
			.sResmaypa = "2"
			.sSuspendi = "2"
			.nUsercode = Me.nUsercode
			.nFrancFix = eRemoteDB.Constants.intNull
			
			insProduct_ge = .Update
		End With
		'UPGRADE_NOTE: Object lclsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_ge = Nothing
		
insProduct_ge_Err: 
		If Err.Number Then
			insProduct_ge = False
		End If
		On Error GoTo 0
	End Function
	
	'% insCreProdmaster: Ejecuta el store-procedure de creación en la tabla maestra
	'%                   de productos con los parametros colocados en el encabezado
	Private Function insCreProdmaster() As Boolean
		Dim lclsValues As eFunctions.Values
		
		On Error GoTo insCreProdmaster_Err
		
		lclsValues = New eFunctions.Values
		
		With Me
			If .sDescript = String.Empty Then
				.sDescript = lclsValues.getMessage(2, "Table26")
			End If
			If .sShort_des = String.Empty Then
				.sShort_des = Mid(.sDescript, 1, 12)
			End If
			.sRealind = 1
			'+ Se coloca el registro en "Proceso de instalación" (Table26)
			.sStatregt = 2
			
			'+ En el momento que se crea el registro en la tabla maestra de productos 'Prodmaster' se debe
			'+ crear en las tablas 'product', 'product_li' y 'product_ge'
			If .AddProdmaster Then
				Call Update_DP003()
                'If .sBrancht = pmBrancht.pmlife Or .sBrancht = pmBrancht.pmNotTraditionalLife Then
                If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
                    Call insProduct_li()
                Else
                    Call insProduct_ge()
                End If
            End If
        End With
		
insCreProdmaster_Err: 
		If Err.Number Then
			insCreProdmaster = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
	End Function
	
	'% insProduct_li: Realiza la asignación de los valores del
	'% frame activo a los parámetros correspondientes del store-procedure que
	'% realiza el mantenimiento de la historia en la estructura 'product_li'
	Private Function insProduct_li() As Boolean
		
		On Error GoTo insProduct_li_Err
		
		With mclsProduct
			If insLoadParameters() Then
				
				'+ Si no existía información, se crea
				
				If Not mblnProduct_liExist Then
					insProduct_li = .AddProduct_li
				ElseIf mclsProduct.dEffecdate = Me.dEffecdate Then 
					insProduct_li = .UpdateProduct_Li
					
					'+ Si la modificación es una fecha posterior, se anula el registro existente y se crea un nuevo
					
				Else
					insProduct_li = .UpdProduct_liDPost
				End If
			End If
		End With
		
insProduct_li_Err: 
		If Err.Number Then
			insProduct_li = False
		End If
		On Error GoTo 0
	End Function
	
	'% insProduct_li_DP023: Realiza la asignación de los valores de la ventana
	Private Function insProduct_li_DP023() As Boolean
		On Error GoTo insProduct_li_DP023_Err
		
		If mclsProduct Is Nothing Then
			mclsProduct = New eProduct.Product
		End If
		
		With mclsProduct
			If insLoadParameters_DP023() Then
				'+ Si no existía información, se crea
				If Not mblnProduct_liExist Then
					insProduct_li_DP023 = .AddProduct_li
				ElseIf mclsProduct.dEffecdate = Me.dEffecdate Then 
					insProduct_li_DP023 = .UpdateProduct_Li
					'+ Si la modificación es una fecha posterior, se anula el registro existente y se crea un nuevo
				Else
					insProduct_li_DP023 = .UpdProduct_liDPost
				End If
			End If
		End With
		
insProduct_li_DP023_Err: 
		If Err.Number Then
			insProduct_li_DP023 = False
		End If
		On Error GoTo 0
	End Function
	'% insLoadParameters: Carga los parámetros del SP a ejecutar
	'-----------------------------------------------------------
	Private Function insLoadParameters() As Boolean
		'-----------------------------------------------------------
		
		On Error GoTo insLoadParameters_Err
		
		insLoadParameters = True
		With mclsProduct
			
			'+ Se cargan los valores de la llave
			
			.nBranch = Me.nBranch
			.nProduct = Me.nProduct
			.dEffecdate = Me.dEffecdate
			.nUsercode = Me.nUsercode
			.nProdClas = eRemoteDB.Constants.intNull
			.nCurrency = eRemoteDB.Constants.intNull
			
			'+ Anticipos
			
			.sRouadvan = String.Empty
			.nInterest = eRemoteDB.Constants.intNull
			.nAnlifint = eRemoteDB.Constants.intNull
			.nPayinter = eRemoteDB.Constants.intNull
			
			'+ Rescates
			
			.sRousurre = String.Empty
			.sSurrenti = String.Empty
			.sSurrenpi = String.Empty
			.nSurcashv = eRemoteDB.Constants.intNull
			.nSurrfreq = eRemoteDB.Constants.intNull
			.nCharge = eRemoteDB.Constants.intNull
			.nChargeamo = eRemoteDB.Constants.intNull
			
			'+ Información general
			
			.sMorcapii = String.Empty
			.sRoureduc = String.Empty
			.sRoureddc = String.Empty
			.sAssociai = String.Empty
			.sAssototal = String.Empty
			.sPremiumtype = String.Empty
			.nTaxsmoke = eRemoteDB.Constants.intNull
			.nTaxnsmoke = eRemoteDB.Constants.intNull
			
			'+ Beneficios sobre inversiones
			
			.nBenefitr = eRemoteDB.Constants.intNull
			.nBenefapl = eRemoteDB.Constants.intNull
			.nBenefexc = eRemoteDB.Constants.intNull
			.nBenexcra = eRemoteDB.Constants.intNull
			.sBenres = String.Empty
			
			'+ Duración
			
			.nTypdurins = eRemoteDB.Constants.intNull
			.sIdurvari = String.Empty
			.nIdurafix = eRemoteDB.Constants.intNull
			.sPdurvari = String.Empty
			.nPdurafix = eRemoteDB.Constants.intNull
			
			'+ Consideraciones sobre edades
			
			.nSuagemin = eRemoteDB.Constants.intNull
			.nSuagemax = eRemoteDB.Constants.intNull
			.nReagemax = eRemoteDB.Constants.intNull
			.nYearminw = eRemoteDB.Constants.intNull
			.nYearmors = eRemoteDB.Constants.intNull
			.nYearmins = eRemoteDB.Constants.intNull
			
			'+ Opciones de siniestros
			
			.sClsimpai = String.Empty
			.sClnoprei = String.Empty
			.sClsurrei = String.Empty
			.sClpaypri = String.Empty
			.sClallpre = String.Empty
			.sCltransi = String.Empty
			.sCllifeai = String.Empty
			.sClannpei = String.Empty
			
			'+ Opciones de pagos de primas
			
			.nPayiniti = eRemoteDB.Constants.intNull
			.nAnnualap = eRemoteDB.Constants.intNull
			.sPeriodic = String.Empty
			.nDedufreq = eRemoteDB.Constants.intNull
			.sPerunifa = String.Empty
			.nPermulti = eRemoteDB.Constants.intNull
			.nPernunmi = eRemoteDB.Constants.intNull
			.nPernumai = eRemoteDB.Constants.intNull
			.sRevaltyp = String.Empty
			.nPerrevfa = eRemoteDB.Constants.intNull
			.sNoperiod = String.Empty
			.sNpeunifa = String.Empty
			
			If mblnProduct_liExist Then
				On Error Resume Next
			Else
				.sMethagav = String.Empty
				.sMethprav = String.Empty
				.sMethprin = String.Empty
				On Error GoTo 0
			End If
			
			If mblnProduct_liExist Then
				On Error Resume Next
			Else
				.nUlfmaxqu = eRemoteDB.Constants.intNull
				.sUlfchani = String.Empty
				.nUlsmaxqu = eRemoteDB.Constants.intNull
				.nUlswiper = eRemoteDB.Constants.intNull
				.nUlsschar = eRemoteDB.Constants.intNull
				.nUlscharg = eRemoteDB.Constants.intNull
				.nUlrmaxqu = eRemoteDB.Constants.intNull
				.nUlredper = eRemoteDB.Constants.intNull
				.nUlrschar = eRemoteDB.Constants.intNull
				.nUlrcharg = eRemoteDB.Constants.intNull
				On Error GoTo 0
			End If
		End With
		
insLoadParameters_Err: 
		If Err.Number Then
			insLoadParameters = False
		End If
		On Error GoTo 0
	End Function
	
	'% insLoadParameters_DP023: Carga los parámetros del Sp a ejecutar
	Private Function insLoadParameters_DP023() As Boolean
		insLoadParameters_DP023 = True
		With mclsProduct
			'+ Se cargan los valores de la llave
			.nBranch = Me.nBranch
			.nProduct = Me.nProduct
			.dEffecdate = Me.dEffecdate
			.nUsercode = Me.nUsercode
			If .FindProduct_li(Me.nBranch, Me.nProduct, dEffecdate) Then
				'+ Opciones de pagos de primas
				If Not (.nProdClas <> 1 And .nProdClas <> 5) Then
					.nPayiniti = eRemoteDB.Constants.intNull
					.nAnnualap = eRemoteDB.Constants.intNull
					.sPeriodic = String.Empty
					.nDedufreq = eRemoteDB.Constants.intNull
					.sPerunifa = String.Empty
					.nPermulti = eRemoteDB.Constants.intNull
					.nPernunmi = eRemoteDB.Constants.intNull
					.nPernumai = eRemoteDB.Constants.intNull
					.sRevaltyp = String.Empty
					.nPerrevfa = eRemoteDB.Constants.intNull
					.sNoperiod = String.Empty
					.sNpeunifa = String.Empty
				End If
			End If
			
			.nProdClas = eRemoteDB.Constants.intNull
			.nCurrency = eRemoteDB.Constants.intNull
			
			'+ Anticipos
			
			.sRouadvan = String.Empty
			.nInterest = eRemoteDB.Constants.intNull
			.nAnlifint = eRemoteDB.Constants.intNull
			.nPayinter = eRemoteDB.Constants.intNull
			
			'+ Rescates
			
			.sRousurre = String.Empty
			.sSurrenti = String.Empty
			.sSurrenpi = String.Empty
			.nSurcashv = eRemoteDB.Constants.intNull
			.nSurrfreq = eRemoteDB.Constants.intNull
			.nCharge = eRemoteDB.Constants.intNull
			.nChargeamo = eRemoteDB.Constants.intNull
			
			'+ Información general
			
			.sMorcapii = String.Empty
			.sRoureduc = String.Empty
			.sRoureddc = String.Empty
			.sAssociai = String.Empty
			.sAssototal = String.Empty
			.sPremiumtype = String.Empty
			.nTaxsmoke = eRemoteDB.Constants.intNull
			.nTaxnsmoke = eRemoteDB.Constants.intNull
			
			'+ Beneficios sobre inversiones
			
			.nBenefitr = eRemoteDB.Constants.intNull
			.nBenefapl = eRemoteDB.Constants.intNull
			.nBenefexc = eRemoteDB.Constants.intNull
			.nBenexcra = eRemoteDB.Constants.intNull
			.sBenres = String.Empty
			
			'+ Duración
			
			.nTypdurins = eRemoteDB.Constants.intNull
			.sIdurvari = String.Empty
			.nIdurafix = eRemoteDB.Constants.intNull
			.sPdurvari = String.Empty
			.nPdurafix = eRemoteDB.Constants.intNull
			
			'+ Consideraciones sobre edades
			
			.nSuagemin = eRemoteDB.Constants.intNull
			.nSuagemax = eRemoteDB.Constants.intNull
			.nReagemax = eRemoteDB.Constants.intNull
			.nYearminw = eRemoteDB.Constants.intNull
			.nYearmors = eRemoteDB.Constants.intNull
			.nYearmins = eRemoteDB.Constants.intNull
			
			'+ Opciones de siniestros
			
			.sClsimpai = String.Empty
			.sClnoprei = String.Empty
			.sClsurrei = String.Empty
			.sClpaypri = String.Empty
			.sClallpre = String.Empty
			.sCltransi = String.Empty
			.sCllifeai = String.Empty
			.sClannpei = String.Empty
			
			'+ Opciones de pagos de primas
			
			.nPayiniti = eRemoteDB.Constants.intNull
			.nAnnualap = eRemoteDB.Constants.intNull
			.sPeriodic = String.Empty
			.nDedufreq = eRemoteDB.Constants.intNull
			.sPerunifa = String.Empty
			.nPermulti = eRemoteDB.Constants.intNull
			.nPernunmi = eRemoteDB.Constants.intNull
			.nPernumai = eRemoteDB.Constants.intNull
			.sRevaltyp = String.Empty
			.nPerrevfa = eRemoteDB.Constants.intNull
			.sNoperiod = String.Empty
			.sNpeunifa = String.Empty
			.sMethagav = IIf(Me.sMethagav = "1", "1", String.Empty)
			.sMethprav = IIf(Me.sMethprav = "1", "1", String.Empty)
			.sMethprin = IIf(Me.sMethprin = "1", "1", String.Empty)
			.nUlfmaxqu = eRemoteDB.Constants.intNull
			.sUlfchani = String.Empty
			.nUlsmaxqu = eRemoteDB.Constants.intNull
			.nUlswiper = eRemoteDB.Constants.intNull
			.nUlsschar = eRemoteDB.Constants.intNull
			.nUlscharg = eRemoteDB.Constants.intNull
			.nUlrmaxqu = eRemoteDB.Constants.intNull
			.nUlredper = eRemoteDB.Constants.intNull
			.nUlrschar = eRemoteDB.Constants.intNull
			.nUlrcharg = eRemoteDB.Constants.intNull
		End With
	End Function
	
	'% Update_DP005: Realiza la asignación de los valores del
	'% frame activo a los parámetros correspondientes del store-procedure que
	'% realiza el mantenimiento de la historia en la estructura 'product'
	Private Function Update_DP005() As Boolean
		
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo Update_DP005_Err
		
		lclsProd_win = New eProduct.Prod_win
		
		Update_DP005 = True
		
		Me.dNulldate = eRemoteDB.Constants.dtmNull
		Me.nQ_certif = eRemoteDB.Constants.intNull
		
		Update_DP005 = Me.UpdateProduct
		
		'+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parámetro
		
		Call lclsProd_win.Add_Prod_win(Me.nBranch, Me.nProduct, Me.dEffecdate, "DP005", "2", Me.nUsercode)
		
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		
Update_DP005_Err: 
		If Err.Number Then
			Update_DP005 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insUpdCliallopro: Crea, actualiza y elimina, en el Registro Cliallopro,
	'% los Clientes Permitidos para todos los Tipos de Póliza
	Private Function insUpdCliallopro() As Boolean
		
		Dim lclsProd_win As eProduct.Prod_win
		Dim lblnDataChange As Boolean
		Dim lblnExecuted As Boolean
		
		On Error GoTo insUpdCliallopro_Err
		
		lclsProd_win = New eProduct.Prod_win
		
		insUpdCliallopro = True
		
		'+ Si no ha habido cambios a nivel de los datos del grid, se verifica la columna sel (0) con el valor inicial de dicha columna (7).
		
		If Not lblnDataChange Then
			If mstrSelected <> mstrInit_sel Then
				lblnDataChange = True
			End If
		End If
		
		If mstrSelected = "1" Or mstrInit_sel = "1" Then
			If mintExist = 2 Then
				If mclsCliallopro.Add Then lblnExecuted = True
			Else
				If mclsCliallopro.Update Then lblnExecuted = True
			End If
			
			If Not lblnExecuted Then
				insUpdCliallopro = False
			Else
				lblnDataChange = True
			End If
		Else
			If mstrSelected = "2" And mintExist = 1 Then
				If Not mclsCliallopro.Delete Then
					insUpdCliallopro = False
				End If
			End If
		End If
		
		'+ Si se presentaron cambios se actualiza, de resto no.
		
		If lblnDataChange Then
			If insUpdCliallopro Then
				Call lclsProd_win.Add_Prod_win(Me.nBranch, Me.nProduct, Me.dEffecdate, "DP042", "2", Me.nUsercode)
				Call lclsProd_win.Add_Prod_win(Me.nBranch, Me.nProduct, Me.dEffecdate, "DP004", "3", Me.nUsercode)
			Else
				Call lclsProd_win.Add_Prod_win(Me.nBranch, Me.nProduct, Me.dEffecdate, "DP042", "1", Me.nUsercode)
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		
insUpdCliallopro_Err: 
		If Err.Number Then
			insUpdCliallopro = False
		End If
		On Error GoTo 0
	End Function
	
	'% insReaDP036: Hace la lectura del Stored Procedure reaTab_protec
	Public Function insReaDP036(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_protec As eRemoteDB.Execute
		Dim lintCount As Integer
		
		'Definición de parámetros para stored procedure 'insudb.reaTab_protec'
		'Información leída el 17/04/2001 02:26:36 PM
		On Error GoTo insReaDP036_err
		lrecreaTab_protec = New eRemoteDB.Execute
		With lrecreaTab_protec
			.StoredProcedure = "reaTab_protec"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReDim arrProtectionElements(50)
				Do While Not .EOF
					lintCount = lintCount + 1
					arrProtectionElements(lintCount).nBranch = .FieldToClass("nBranch")
					arrProtectionElements(lintCount).nElement = .FieldToClass("nElement")
					arrProtectionElements(lintCount).nProduct = .FieldToClass("nProduct")
					arrProtectionElements(lintCount).dEffecdate = .FieldToClass("dEffecdate")
					arrProtectionElements(lintCount).nCurrency = .FieldToClass("nCurrency")
					arrProtectionElements(lintCount).sDescript = .FieldToClass("sDescript")
					arrProtectionElements(lintCount).nDiscount = .FieldToClass("nDiscount")
					arrProtectionElements(lintCount).nDismaxim = .FieldToClass("nDismaxim")
					arrProtectionElements(lintCount).nDisminin = .FieldToClass("nDisminin")
					arrProtectionElements(lintCount).nDisrate = .FieldToClass("nDisrate")
					arrProtectionElements(lintCount).dNulldate = .FieldToClass("dNulldate")
					arrProtectionElements(lintCount).sShort_des = .FieldToClass("sShort_des")
					arrProtectionElements(lintCount).sStatregt = .FieldToClass("sStatregt")
					arrProtectionElements(lintCount).nUsercode = .FieldToClass("nUsercode")
					arrProtectionElements(lintCount).sRoutine = .FieldToClass("sRoutine")
					.RNext()
				Loop 
				.RCloseRec()
				insReaDP036 = True
				ReDim Preserve arrProtectionElements(lintCount)
			Else
				insReaDP036 = False
			End If
		End With
		
insReaDP036_err: 
		If Err.Number Then
			insReaDP036 = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaTab_protec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_protec = Nothing
		On Error GoTo 0
	End Function
	
	'% insValidateElements: verifica la existencia de los elementos de protección en la tabla
	Private Function insValidateElements(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nElement As Integer) As Boolean
		Dim lrecreaTab_protecElement As eRemoteDB.Execute
		
		On Error GoTo insValidateElements_err
		
		lrecreaTab_protecElement = New eRemoteDB.Execute
		
		insValidateElements = True
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_protecElement'
		'+ Información leída el 17/04/2001 04:15:59 PM
		
		With lrecreaTab_protecElement
			.StoredProcedure = "reaTab_protecElement"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nElement", nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nElement") = nElement Then
					insValidateElements = False
				End If
				.RCloseRec()
			End If
		End With
		
insValidateElements_err: 
		If Err.Number Then
			insValidateElements = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaTab_protecElement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_protecElement = Nothing
		On Error GoTo 0
	End Function
	
	'% Se hacen las validaciones respectivas
	Public Function insValDP036(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nElement As Integer, ByVal sDescript As String, ByVal sShortDescription As String, ByVal nState As Integer, ByVal nCurrency As Integer, ByVal nMaxAmount As Double, ByVal nMinAmount As Double, ByVal nSelected As Integer, ByVal nFixAmount As Double, ByVal nDiscount As Double, ByVal nDisrate As Double) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsValidate As eFunctions.valField
		
		On Error GoTo insValDP036_err
		lobjErrors = New eFunctions.Errors
		
		'+ El código debe estar lleno
		If nElement = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage("DP036", 1925,  , eFunctions.Errors.TextAlign.LeftAling, "Código:")
		End If
		
		'+ El código no debe existir en los elementos de protección
		If nSelected = 1 Then
			If Not insValidateElements(nBranch, nProduct, dEffecdate, nElement) Then
				Call lobjErrors.ErrorMessage("DP036", 11118)
			End If
		End If
		
		'+ Se valida el porcentaje de descuento si y sólo si no se ingresó el monto fijo
		If (nFixAmount = 0 Or nFixAmount = eRemoteDB.Constants.intNull) Then
			If (nDisrate <> 0 And nDisrate <> eRemoteDB.Constants.intNull) Then
				lclsValidate = New eFunctions.valField
				With lclsValidate
					.objErr = lobjErrors
					.Min = 0.01
					.Max = 100
					.Descript = "% de descuento:"
					Call .ValNumber(nDisrate,  , eFunctions.valField.eTypeValField.ValAll)
				End With
			End If
		End If
		
		'+ Si el código del elemento está lleno, la descripción debe estar llena
		If nElement <> eRemoteDB.Constants.intNull And sDescript = String.Empty Then
			Call lobjErrors.ErrorMessage("DP036", 11302)
		End If
		
		'+ Si el código del elemento está lleno, la descripción corta debe estar llena
		If nElement <> eRemoteDB.Constants.intNull And sShortDescription = String.Empty Then
			Call lobjErrors.ErrorMessage("DP036", 11303)
		End If
		
		'+ Si el código del elemento está lleno, estado debe estar lleno
		If nElement <> eRemoteDB.Constants.intNull And (nState = eRemoteDB.Constants.intNull Or nState = 0) Then
			Call lobjErrors.ErrorMessage("DP036", 11304)
		End If
		
		'+ Si no existen importes asociados al elemento de protección, Moneda no debe estar lleno
		If (nMinAmount = eRemoteDB.Constants.intNull Or nMinAmount = 0) And (nMaxAmount = eRemoteDB.Constants.intNull Or nMaxAmount = 0) And (nFixAmount = eRemoteDB.Constants.intNull Or nFixAmount = 0) And (nCurrency = eRemoteDB.Constants.intNull Or nCurrency > 0) Then
			Call lobjErrors.ErrorMessage("DP036", 11417)
			'(nDiscount = NumNull Or _
			''        nDiscount = 0) And
		End If
		
		If (nElement <> eRemoteDB.Constants.intNull Or nElement > 0) And (nMaxAmount <> eRemoteDB.Constants.intNull Or nMaxAmount > 0) And nMinAmount > 0 And nMaxAmount < nMinAmount Then
			Call lobjErrors.ErrorMessage("DP036", 11048)
		End If
		
		insValDP036 = lobjErrors.Confirm
		
		
insValDP036_err: 
		If Err.Number Then
			insValDP036 = insValDP036 & " " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsValidate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValidate = Nothing
		On Error GoTo 0
	End Function
	
	'% Se ejecutan las actualizaciones en la BD
	Public Function insPostDP036(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nElement As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal dCompdate As Date, ByVal nCurrency As Integer, ByVal sDescript As String, ByVal nDiscount As Double, ByVal nDismaxim As Double, ByVal nDisminin As Double, ByVal nDisrate As Double, ByVal dNulldate As Date, ByVal sShort_des As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal sRoutine As String, ByVal nInitialSelection As Integer) As Boolean
		Dim lclsProdwin As eProduct.Prod_win = New eProduct.Prod_win
		
		On Error GoTo insPostDP036_Err
		
		With Me
			.nBranch = nBranch
			.nElement = nElement
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nCurrency = nCurrency
			.sDescript = sDescript
			.nDiscount = nDiscount
			.nDismaxim = nDismaxim
			.nDisminin = nDisminin
			.nDisrate = nDisrate
			.dNulldate = dNulldate
			.sShort_des = sShort_des
			.sStatregt = CShort(sStatregt)
			.nUsercode = nUsercode
			.sRoutine = sRoutine
			
			Select Case nAction
				'+Si la opción seleccionada es Registrar
				Case 0
					insPostDP036 = insUpdTab_protec("Add", nInitialSelection)
					If insPostDP036 Then
						If Not insReaDP036(.nBranch, .nProduct, .dEffecdate) Then
							Call lclsProdwin.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP036", "1", .nUsercode)
						Else
							Call lclsProdwin.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP036", "2", .nUsercode)
						End If
					End If
					'+Si la opción seleccionada es Modificar
				Case 1
					insPostDP036 = insUpdTab_protec("Update", nInitialSelection)
					If insPostDP036 Then
						If Not insReaDP036(.nBranch, .nProduct, .dEffecdate) Then
							Call lclsProdwin.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP036", "1", .nUsercode)
						Else
							Call lclsProdwin.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP036", "2", .nUsercode)
						End If
					End If
					'+Si la opción seleccionada es Eliminar - ACM - 18/04/2001
				Case 2
					insPostDP036 = insUpdTab_protec("Delete", nInitialSelection)
					If insPostDP036 Then
						If Not insReaDP036(.nBranch, .nProduct, .dEffecdate) Then
							Call lclsProdwin.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP036", "1", .nUsercode)
						Else
							Call lclsProdwin.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP036", "2", .nUsercode)
						End If
					End If
			End Select
		End With
		'UPGRADE_NOTE: Object lclsProdwin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProdwin = Nothing
		
insPostDP036_Err: 
		If Err.Number Then
			insPostDP036 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'%ItemDP036: Función que tomando en cuenta el valor del index carga en las variables de la clase la información del arreglo
	Public Function ItemDP036(ByVal lintIndex As Integer) As Boolean
		If lintIndex <= UBound(arrProtectionElements) Then
			With arrProtectionElements(lintIndex)
				nBranch = .nBranch
				nElement = .nElement
				nProduct = .nProduct
				dEffecdate = .dEffecdate
				nCurrency = .nCurrency
				sDescript = .sDescript
				nDiscount = .nDiscount
				nDismaxim = .nDismaxim
				nDisminin = .nDisminin
				nDisrate = .nDisrate
				dNulldate = .dNulldate
				sShort_des = .sShort_des
				sStatregt = CShort(.sStatregt)
				nUsercode = .nUsercode
				sRoutine = .sRoutine
			End With
			ItemDP036 = True
		Else
			ItemDP036 = False
		End If
		
	End Function
	
	'%insUpdTab_protec: En esta función se crean, actualizan y eliminan, en el Registro Tab_Protec,
	'%los elementos de Protección modificados por el usuario
	Private Function insUpdTab_protec(ByVal sAction As String, ByVal nInitialSelection As Integer) As Boolean
		'-Se define la variable lRecRecordset que almacena el recordset obtenido al ejecutar el SP
		Dim lrecTab_protec As eRemoteDB.Execute
		
		'-Se definen la variable llngIndex que permitirá el desplazamiento en el Grid
		Dim llngIndex As Integer
		
		'-Se define la variable lblnExistElem que permitirá identificar si existe algún Elemento de Protección para el producto
		Dim lblnExistElem As Boolean
		Dim lblnDeletedElem As Boolean
		
		On Error GoTo insUpdTab_protec_err
		
		insUpdTab_protec = True
		lrecTab_protec = New eRemoteDB.Execute
		'+Se verifica la columna sel del Grid para conocer la acción a realizar (Crear, Modificar o Eliminar)
		Select Case sAction
			'+Si no se especificó acción, es un nuevo elemento o ya existe en la Base de Datos
			Case "Add"
				'+ Definición de parámetros para stored procedure 'insudb.creTab_protec'
				'+ Información leída el 18/04/2001 09:02:56 AM
				With lrecTab_protec
					.StoredProcedure = "creTab_protec"
					.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nElement", Me.nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("ncurrency", Me.nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDiscount", Me.nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDismaxim", Me.nDismaxim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDisminin", Me.nDisminin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDisrate", Me.nDisrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sShort_Des", Me.sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sStatregt", Me.sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutine", Me.sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not .Run(False) Then
						insUpdTab_protec = False
					Else
						insUpdTab_protec = True
						lblnExistElem = True
					End If
				End With
				'+Si se especificó la acción de Modificar
			Case "Update"
				'+ Definición de parámetros para stored procedure 'insudb.updTab_protec'
				'+ Información leída el 18/04/2001 09:08:48 AM
				With lrecTab_protec
					.StoredProcedure = "updTab_protec"
					.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nElement", Me.nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("ncurrency", Me.nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDiscount", Me.nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDismaxim", Me.nDismaxim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDisminin", Me.nDisminin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDisrate", Me.nDisrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sShort_Des", Me.sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sStatregt", Me.sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sRoutine", Me.sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not .Run(False) Then
						insUpdTab_protec = False
					Else
						insUpdTab_protec = True
						lblnExistElem = True
					End If
				End With
				
				'+Si se especificó la acción de Eliminar
			Case "Delete"
				'+ Definición de parámetros para stored procedure 'insudb.delTab_protec'
				'+ Información leída el 18/04/2001 09:11:33 AM
				With lrecTab_protec
					.StoredProcedure = "delTab_protec"
					.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nElement", Me.nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sTransac", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not .Run(False) Then
						insUpdTab_protec = False
					Else
						insUpdTab_protec = True
						lblnDeletedElem = True
					End If
				End With
		End Select
		
insUpdTab_protec_err: 
		If Err.Number Then
			insUpdTab_protec = False
		End If
		'UPGRADE_NOTE: Object lrecTab_protec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_protec = Nothing
		On Error GoTo 0
	End Function
	
	'% insValDP002_K: Realiza la validación de los campos del Header de la ventana DP002 - Productos de un ramo comercial.
	Public Function insValDP002_k(ByVal sCodispl As String, ByVal nAction As String, Optional ByVal nBranch As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValDP002_k_Err
		
		lobjErrors = New eFunctions.Errors
		With lobjErrors
			'+ Se valida el campo Código del Ramo.
			If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1022)
			End If
			
			'+ Validaciones sobre el campo de Fecha de Efecto
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 7116)
			Else
				lclsValField = New eFunctions.valField
				lclsValField.objErr = lobjErrors
				If Not lclsValField.ValDate(dEffecdate) Then
					Call .ErrorMessage(sCodispl, 1001)
				End If
			End If
			
			insValDP002_k = .Confirm
			
		End With
		
insValDP002_k_Err: 
		If Err.Number Then
			insValDP002_k = "insValDP002_k: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		On Error GoTo 0
	End Function
	
	'% insValDP002: Realiza la validación de los campos del Detalle de la ventana DP002 - Productos de un ramo comercial.
	Public Function insValDP002(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sBrancht As String = "", Optional ByVal sStatregt As String = "") As String
		Dim lobjValues As eFunctions.Values
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValDP002_Err
		
		lobjValues = New eFunctions.Values
		lobjErrors = New eFunctions.Errors
		
		insValDP002 = String.Empty
		
		With lobjErrors
			If nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 11009)
			Else
				'+ Se valida el campo Código del Producto.
				If sAction = "Add" Then
					If valExistProdMaster_e(nBranch, nProduct) Then
						Call .ErrorMessage(sCodispl, 11336)
					End If
				End If
				
				'+ Valida la Descripción.
				If Trim(sDescript) = String.Empty Then
					Call .ErrorMessage(sCodispl, 11287)
				End If
				
				'+ Valida la Descripción Breve.
				If Trim(sShort_des) = String.Empty Then
					Call .ErrorMessage(sCodispl, 11288)
				End If
				
				'+ Valida el Tipo de Producto.
				If sBrancht = "0" Or CShort(sBrancht) = 0 Then
					Call .ErrorMessage(sCodispl, 11289)
				End If
				
				'+ Valida el Estado del Producto.
				If sStatregt = "0" Then
					Call .ErrorMessage(sCodispl, 11290)
				End If
			End If
			insValDP002 = .Confirm
		End With
		
insValDP002_Err: 
		If Err.Number Then
			insValDP002 = "insValDP002: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
	End Function
	
	'% insValDP063: Realiza la validación de los campos de la ventana DP063 - Duplicar productos.
	Public Function insValDP063(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValDP063_Err
		
		lobjErrors = New eFunctions.Errors
		With lobjErrors
			'+ Se valida el campo Código del Producto.
			If nAction = eFunctions.Menues.TypeActions.clngActionDuplicateProduct Then
				If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 11009)
				Else
					If valExistProdMaster_e(nBranch, nProduct) Then
						Call .ErrorMessage(sCodispl, 11336)
					End If
				End If
				
				'+ Validaciones sobre el campo de Fecha de Efecto
				If dEffecdate = eRemoteDB.Constants.dtmNull Then
					Call .ErrorMessage(sCodispl, 4003)
				Else
					lclsValField = New eFunctions.valField
					lclsValField.objErr = lobjErrors
					If Not lclsValField.ValDate(dEffecdate) Then
						Call .ErrorMessage(sCodispl, 2084)
					End If
				End If
			End If
			
			insValDP063 = .Confirm
		End With
		
insValDP063_Err: 
		If Err.Number Then
			insValDP063 = "insValDP063: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		On Error GoTo 0
	End Function
	
	'% valExistProdmaster_e: Permite verificar la existencia del Ramo- Producto en la tabla Prodaster.
	Public Function valExistProdMaster_e(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecProdMaster_e As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo valExistProdMaster_e_Err
		
		If nBranch = Me.nBranch And nProduct = Me.nProduct And Not bFind Then
			valExistProdMaster_e = True
		Else
			lrecProdMaster_e = New eRemoteDB.Execute
			With lrecProdMaster_e
				.StoredProcedure = "valProdMaster_e"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sStatregt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Run(False)
				valExistProdMaster_e = (.Parameters("nExists").Value = 1)
				If valExistProdMaster_e Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
				End If
			End With
		End If
		
valExistProdMaster_e_Err: 
		If Err.Number Then
			valExistProdMaster_e = False
		End If
		'UPGRADE_NOTE: Object lrecProdMaster_e may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProdMaster_e = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostDP002: Esta función se encarga de almacenar los datos en las tablas, en este caso Table10.
	Public Function insPostDP002(ByVal sMainAction As String, ByVal nBranch As Integer, ByVal dEffecdate As Date, ByVal nProduct As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sBrancht As String, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsProduct_ge As eProduct.Product_ge
		
		On Error GoTo insPostDP002_Err
		
		insPostDP002 = Find(nBranch, nProduct, dEffecdate)
		
		If insPostDP002 Then
			With Me
				.nBranch = nBranch
				.dEffecdate = dEffecdate
				.nProduct = nProduct
				.sDescript = sDescript
				.sShort_des = sShort_des
				.sBrancht = CShort(sBrancht)
				.sStatregt = CShort(sStatregt)
				.nUsercode = nUsercode
				
				Select Case sMainAction
					
					'+ Si la opción seleccionada es Registrar.
					Case "Add"
						insPostDP002 = UpdateProduct()
						'+ Si no es vida.
						If CStr(.sBrancht) <> "1" Then
							lclsProduct_ge = New eProduct.Product_ge
							
							With lclsProduct_ge
								.nBranch = nBranch
								.nProduct = nProduct
								.dEffecdate = dEffecdate
								
								insPostDP002 = .Update()
							End With
							
							'UPGRADE_NOTE: Object lclsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsProduct_ge = Nothing
						End If
						
						'+ Si es vida o es un combinado.
						If (.sBrancht = 1 Or .sBrancht = 5 Or .sBrancht = 6) Then
							If Not FindProduct_li(.nBranch, .nProduct, .dEffecdate, True) Then
								insPostDP002 = AddProduct_li
							ElseIf dEffecdate = dEffecdateProduct_li Then 
								insPostDP002 = UpdateProduct_Li
								
								'+ Si la modificación es una fecha posterior, se anula el registro existente y se crea un nuevo.
							Else
								insPostDP002 = UpdProduct_liDPost
							End If
						End If
						
						'+ Si la opción seleccionada es Modificar.
					Case "Update"
						insPostDP002 = UpdateProdmaster()
				End Select
			End With
		End If
		
insPostDP002_Err: 
		If Err.Number Then
			insPostDP002 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_ge = Nothing
	End Function
	
	'%insPostDP063: Esta función se encarga de almacenar los datos en las tablas para duplicar un producto.
	Public Function insPostDP063(ByVal sMainAction As String, ByVal nBranch As Integer, ByVal nOldProduct As Integer, ByVal nNewProduct As Integer, ByVal dOldEffecdate As Date, ByVal dNewEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostDP063_Err
		
		insPostDP063 = True
		
		Select Case sMainAction
			
			'+ Si la opción seleccionada es Duplicar Producto.
			Case CStr(eFunctions.Menues.TypeActions.clngActionDuplicateProduct)
				insPostDP063 = insDuplicateProd(nBranch, nOldProduct, nNewProduct, dOldEffecdate, dNewEffecdate, nUsercode)
				
		End Select
		
insPostDP063_Err: 
		If Err.Number Then
			insPostDP063 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insDuplicateProd: Esta rutina se encarga de duplicar el producto llamando al SP encargado.
	Public Function insDuplicateProd(ByVal nBranch As Integer, ByVal nOldProduct As Integer, ByVal nNewProduct As Integer, ByVal dOldEffecdate As Date, ByVal dNewEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecProduct As New eRemoteDB.Execute
		
		On Error GoTo insDuplicateProd_Err
		
		With lrecProduct
			.StoredProcedure = "insDupProduct"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOldProduct", nOldProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNewProduct", nNewProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dOldEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNewdate", dNewEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insDuplicateProd = .Run(False)
		End With
		
insDuplicateProd_Err: 
		If Err.Number Then
			insDuplicateProd = False
		End If
		'UPGRADE_NOTE: Object lrecProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProduct = Nothing
		On Error GoTo 0
	End Function
	
	'% insProdLifeSeq: Permite aplicar el tipo de actualización requerida en el mantenimiento
	'%                 de la historia en la estructura 'product_li' de acuerdo con el contenido
	'%                 de los frames de la subsecuencia de Caracteristicas de Vida
	Public Function insProdLifeSeq() As Boolean
		'+ Si la modificación es a la misma fecha se actualiza el registro
		If dEffecdate = dEffecdateProduct_li Then
			insProdLifeSeq = UpdateProduct_Li
			
			'+ Si la modificación es una fecha posterior, se anula el registro existente y se crea un nuevo
		Else
			insProdLifeSeq = UpdProduct_liDPost
		End If
	End Function
	
	'% insProdActLifeSeq: Permite aplicar el tipo de actualización requerida en el
	'% mantenimiento de la historia en la estructura 'product_li' de acuerdo con
	'% el contenido de los frames de la subsecuencia de Caracteristicas de VidActiva
	Public Function insProdActLifeSeq() As Boolean
		
		On Error GoTo insProdActLifeSeq_Err
		
		'+ Si la modificación es a la misma fecha
		'+ se actualiza el registro
		
		If dEffecdate = dEffecdateProduct_li Then
			insProdActLifeSeq = UpdateProduct_Li
			
			'+ Si la modificación es una fecha posterior,
			'+ se anula el registro existente y se crea un nuevo
			
		Else
			insProdActLifeSeq = UpdProduct_liDPost
		End If
		
insProdActLifeSeq_Err: 
		If Err.Number Then
			insProdActLifeSeq = False
		End If
		On Error GoTo 0
	End Function
	
	'% IsModule: Indica si el producto es modular
	Public Function IsModule(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecTab_modul As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo IsModule_Err
		
		lrecTab_modul = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure valExiststab_modul al 06-12-2002 18:51:02
		'+
		With lrecTab_modul
			.StoredProcedure = "valExiststab_modul"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nModulec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				IsModule = True
			End If
			
		End With
		
IsModule_Err: 
		If Err.Number Then
			IsModule = False
		End If
		'UPGRADE_NOTE: Object lrecTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_modul = Nothing
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub ClearFields()
		mdtmDate = eRemoteDB.Constants.dtmNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = CDate(Nothing)
		nUsercode = eRemoteDB.Constants.intNull
		nProdClas = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		sRouadvan = String.Empty
		nInterest = eRemoteDB.Constants.intNull
		nAnlifint = eRemoteDB.Constants.intNull
		nPayinter = eRemoteDB.Constants.intNull
		sRousurre = String.Empty
		sSurrenti = String.Empty
		sSurrenpi = String.Empty
		nSurcashv = eRemoteDB.Constants.intNull
		nSurrfreq = eRemoteDB.Constants.intNull
		nCharge = eRemoteDB.Constants.intNull
		nChargeamo = eRemoteDB.Constants.intNull
		sMorcapii = String.Empty
		sRoureduc = String.Empty
		sRoureddc = String.Empty
		sAssociai = String.Empty
		sAssototal = String.Empty
		sPremiumtype = String.Empty
		nTaxsmoke = eRemoteDB.Constants.intNull
		nTaxnsmoke = eRemoteDB.Constants.intNull
		nBenefitr = eRemoteDB.Constants.intNull
		nBenefapl = eRemoteDB.Constants.intNull
		nBenefexc = eRemoteDB.Constants.intNull
		nBenexcra = eRemoteDB.Constants.intNull
		sBenres = String.Empty
		nTypdurins = eRemoteDB.Constants.intNull
		sIdurvari = String.Empty
		nIdurafix = eRemoteDB.Constants.intNull
		sPdurvari = String.Empty
		nPdurafix = eRemoteDB.Constants.intNull
		nSuagemin = eRemoteDB.Constants.intNull
		nSuagemax = eRemoteDB.Constants.intNull
		nReagemax = eRemoteDB.Constants.intNull
		nYearminw = eRemoteDB.Constants.intNull
		nYearmors = eRemoteDB.Constants.intNull
		nYearmins = eRemoteDB.Constants.intNull
		sClsimpai = String.Empty
		sClnoprei = String.Empty
		sClsurrei = String.Empty
		sClpaypri = String.Empty
		sClallpre = String.Empty
		sCltransi = String.Empty
		sCllifeai = String.Empty
		sClannpei = String.Empty
		nPayiniti = eRemoteDB.Constants.intNull
		nAnnualap = eRemoteDB.Constants.intNull
		sPeriodic = String.Empty
		nDedufreq = eRemoteDB.Constants.intNull
		sPerunifa = String.Empty
		nPermulti = eRemoteDB.Constants.intNull
		nPernunmi = eRemoteDB.Constants.intNull
		nPernumai = eRemoteDB.Constants.intNull
		sRevaltyp = String.Empty
		nPerrevfa = eRemoteDB.Constants.intNull
		sNoperiod = String.Empty
		sNpeunifa = String.Empty
		sMethagav = String.Empty
		sMethprav = String.Empty
		sMethprin = String.Empty
		nUlfmaxqu = eRemoteDB.Constants.intNull
		sUlfchani = String.Empty
		nUlsmaxqu = eRemoteDB.Constants.intNull
		nUlswiper = eRemoteDB.Constants.intNull
		nUlsschar = eRemoteDB.Constants.intNull
		nUlscharg = eRemoteDB.Constants.intNull
		nUlrmaxqu = eRemoteDB.Constants.intNull
		nUlredper = eRemoteDB.Constants.intNull
		nUlrschar = eRemoteDB.Constants.intNull
		nUlrcharg = eRemoteDB.Constants.intNull
		nWay_pay = eRemoteDB.Constants.intNull
		nBill_day = eRemoteDB.Constants.intNull
		nRehabperiod = eRemoteDB.Constants.intNull
		sTyp_dom = String.Empty
		sLeg = String.Empty
		sReinst = String.Empty
		sDatecoll = String.Empty
		sFirst_pay = String.Empty
		nQdays_pro = eRemoteDB.Constants.intNull
		nQdays_quo = eRemoteDB.Constants.intNull
		nMonth_surr = eRemoteDB.Constants.intNull
		nNotCancelDay = eRemoteDB.Constants.intNull
		bYearpay = True
		bAgepay = True
		bExpirdatpay = True
		dDate_pay = eRemoteDB.Constants.dtmNull
		nPay_time = eRemoteDB.Constants.intNull
		nAgepay_time = eRemoteDB.Constants.intNull
		nRepInsured = eRemoteDB.Constants.intNull
		sNumprop = String.Empty
		nRehabperiod_aut = eRemoteDB.Constants.intNull
		sSetprem = String.Empty
		nMonth_Setpr = eRemoteDB.Constants.intNull
		sRetarif = "2"
	End Sub
	
	'% insDefaultValueDP043: controla el valor por defecto de la página DP043 (Características
	'%                       de vida)
	Public Function insDefaultValueDP043(ByVal sField As String) As String
		insDefaultValueDP043 = "2"
		Select Case sField
			Case "optAssoTotal1"
				If sAssociai <> "2" And sAssociai <> String.Empty And sAssototal = "1" Then
					insDefaultValueDP043 = "1"
				End If
				
			Case "optAssoTotal2"
				If sAssociai <> "2" And sAssociai <> String.Empty And sAssototal = "2" Then
					insDefaultValueDP043 = "1"
				End If
				
			Case "OptPremiumType1"
				If sPremiumtype = "1" Or sPremiumtype = String.Empty Then
					insDefaultValueDP043 = "1"
				End If
				
			Case "OptPremiumType2"
				If sPremiumtype = "2" Then
					insDefaultValueDP043 = "1"
				End If
		End Select
	End Function
	
	'% getInsur_areaBysBrancht: Devuelve el area de seguros dependiendo del yipo de producto según el ramo y producto pasado como parámetro.
	Public Function getInsur_areaBysBrancht(ByVal nBranch As Integer, ByVal nProduct As Integer) As Integer
		Dim lclsProduct As eProduct.Product
		
		On Error GoTo getInsur_areaBysBrancht_Err
		
		lclsProduct = New eProduct.Product
		
		getInsur_areaBysBrancht = 1 'Area de seguros generales
		
		If lclsProduct.FindProdMasterActive(nBranch, nProduct) Then
            'If (lclsProduct.sBrancht <> pmBrancht.pmlife And lclsProduct.sBrancht <> pmBrancht.pmNotTraditionalLife) Then
            If (sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) And sBrancht <> CStr(Product.pmBrancht.pmMedicalAtention)) Then
                getInsur_areaBysBrancht = 1 'Area de seguros generales
            Else
                getInsur_areaBysBrancht = 2 'Area de seguros vida
            End If
        Else
            getInsur_areaBysBrancht = 1 'Area de seguros generales
        End If

getInsur_areaBysBrancht_Err:
        If Err.Number Then
            getInsur_areaBysBrancht = 1
        End If
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        On Error GoTo 0

    End Function
	
	'% valProdClas: Valida si el producto pasado como parámetro es de una determinada clase de producto de vida.
	'% Devuelve: True -> Si el producto es del mismo tipo de clase pasado como parámetro; False -> No coincide el tipo del producto en tratamiento con el pasado como parámetro.
	Public Function valProdClas(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProdClas As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecProduct As eRemoteDB.Execute
		Dim nExists As Integer
		
		lrecProduct = New eRemoteDB.Execute
		
		On Error GoTo valProdClas_Err
		
		With lrecProduct
			.StoredProcedure = "valExistsProdClas"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProdclas", nProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) '(TABLE124)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters.Item("nExists").Value = 1 Then
					valProdClas = True
				End If
			End If
		End With
		
valProdClas_Err: 
		If Err.Number Then
			valProdClas = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProduct = Nothing
	End Function
	
	'% getProdClas: Valida si el producto pasado como parámetro es de una determinada clase de producto de vida.
	'% Devuelve: True -> Si el producto es del mismo tipo de clase pasado como parámetro; False -> No coincide el tipo del producto en tratamiento con el pasado como parámetro.
	Public Function getProdClas(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Integer
		Dim lrecProduct As eRemoteDB.Execute
		Dim lintProdClas As Integer
		
		On Error GoTo getProdClas_Err
		
		lrecProduct = New eRemoteDB.Execute
		
		With lrecProduct
			.StoredProcedure = "getProdClasSP"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProdclas", lintProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) '(TABLE124)
			.Run(False)
			getProdClas = .Parameters.Item("nProdClas").Value
		End With
		
getProdClas_Err: 
		If Err.Number Then
			getProdClas = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProduct = Nothing
	End Function
	
	
	'% DefaultValueDP024: Se manejan los valores y estados por defecto de los campos de la ventana
	Public Function DefaultValueDP024(ByVal sField As String) As Object
        Dim lstrReturnValue As Object

        '+ Pagos periódicos
        If sPeriodic = "1" Then
			Select Case sField
				Case "chkPerPay"
					lstrReturnValue = "1"
					
				Case "cbePerFreq_disabled", "chkPerUni_disabled", "cbeRevalType_disabled", "chkPerPay_disabled"
					lstrReturnValue = "false"
					
				Case "tcnRevalFact"
					lstrReturnValue = IIf(sRevaltyp = "3", nPerrevfa, 0)
					
				Case "tcnRevalFact_disabled"
					lstrReturnValue = IIf(sRevaltyp = "3", "false", "true")
					
				Case "tcnPerMul"
					lstrReturnValue = IIf(sPerunifa = "1", nPermulti, 0)
					
				Case "tcnPerMul_disabled"
					lstrReturnValue = IIf(sPerunifa = "1", "false", "true")
					
				Case "tcnPerMin"
					lstrReturnValue = IIf(sPerunifa = "1", 0, nPernunmi)
					
				Case "tcnPerMax"
					lstrReturnValue = IIf(sPerunifa = "1", 0, nPernumai)
					
				Case "tcnPerMin_disabled", "tcnPerMax_disabled"
					lstrReturnValue = IIf(sPerunifa = "1", "true", "false")
					
					'+ Se desabilitan lo campos del frame de pagos no periódicos
					
				Case "chkNoPerUni", "tcnNoPerMul", "tcnNoPerMin", "tcnNoPerMax", "chkNoPerPay"
					lstrReturnValue = 0
					
				Case "chkNoPerUni_disabled", "tcnNoPerMul_disabled", "tcnNoPerMin_disabled", "tcnNoPerMax_disabled", "chkNoPerPay_disabled"
					lstrReturnValue = "true"
			End Select
			
			'+ Pagos no periódicos
			
		ElseIf sNoperiod = "1" Then 
			Select Case sField
				Case "chkNoPerPay"
					lstrReturnValue = "1"
					
				Case "chkNoPerUni_disabled"
					lstrReturnValue = "false"
					
				Case "tcnNoPerMul"
					lstrReturnValue = IIf(sNpeunifa = "1", nNpemulti, 0)
					
				Case "tcnNoPerMul_disabled"
					lstrReturnValue = IIf(sNpeunifa = "1", "false", "true")
					
				Case "tcnNoPerMin"
					lstrReturnValue = IIf(sNpeunifa = "1", 0, nNpenunmi)
					
				Case "tcnNoPerMax"
					lstrReturnValue = IIf(sNpeunifa = "1", 0, nNpenumai)
					
				Case "tcnNoPerMin_disabled", "tcnNoPerMax_disabled"
					lstrReturnValue = IIf(sNpeunifa = "1", "true", "false")
					
					'+ Se desabilitan lo campos del frame de pagos periódicos
					
				Case "chkPerPay", "cbePerFreq", "chkPerUni", "tcnPerMin", "tcnPerMax", "tcnPerMul", "cbeRevalType", "tcnRevalFact"
					lstrReturnValue = 0
					
				Case "chkPerPay_disabled", "cbePerFreq_disabled", "chkPerUni_disabled", "tcnPerMin_disabled", "tcnPerMax_disabled", "tcnPerMul_disabled", "cbeRevalType_disabled", "tcnRevalFact_disabled"
					lstrReturnValue = "true"
			End Select
			
			'+ Sin selección previa de tipos de pagos
			
		Else
			Select Case sField
				Case "cbePerFreq", "tcnPerMul", "tcnPerMin", "tcnPerMax", "cbeRevalType", "tcnRevalFact", "chkPerUni", "chkNoPerUni", "tcnNoPerMul", "tcnNoPerMax", "tcnNoPerMin"
					lstrReturnValue = 0
					
				Case "chkNoPerPay_disabled"
					lstrReturnValue = "false"
					
				Case Else
					lstrReturnValue = "true"
			End Select
		End If
		
		DefaultValueDP024 = lstrReturnValue
	End Function
	
	'% insUpdDP064: Hace la actualización de los campos costo mensual operativo y
	'%              cargo administrativo en la tabla Product_li
	Public Function insUpdDP064(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nMonthamo As Double, ByVal nAdminrate As Double, ByVal nUsercode As Integer) As Boolean
		Dim mclsProduct As eProduct.Product
		
		mclsProduct = New eProduct.Product
		
		insUpdDP064 = True
		With mclsProduct
			If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
				.nMonthamo = nMonthamo
				.nAdminrate = nAdminrate
				.nUsercode = nUsercode
				
				insUpdDP064 = mclsProduct.UpdateProduct_Li
			Else
				insUpdDP064 = False
			End If
		End With
		'UPGRADE_NOTE: Object mclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsProduct = Nothing
		
	End Function
	
	'% FindBranch_rent : Lee los Ramos asociados a rentas vitalicias
	Public Function FindBranch_rent(ByVal dEffecdate As Date) As Boolean
		Dim lrecreaProduct_li As eRemoteDB.Execute
		
		On Error GoTo FindBranch_rent_Err
		
		'+ Definición de parámetros para stored procedure 'reaProduct_li_rent'
		lrecreaProduct_li = New eRemoteDB.Execute
		
		With lrecreaProduct_li
			.StoredProcedure = "reaProduct_li_rent"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Me.sBranch_Rent = String.Empty
			If .Run Then
				Do While Not .EOF
					nBranch = .FieldToClass("nBranch")
					If Me.sBranch_Rent <> String.Empty Then
						Me.sBranch_Rent = Me.sBranch_Rent & ","
					End If
					Me.sBranch_Rent = Me.sBranch_Rent & CStr(.FieldToClass("nBranch"))
					.RNext()
				Loop 
				.RCloseRec()
				FindBranch_rent = True
			End If
		End With
		
FindBranch_rent_Err: 
		If Err.Number Then
			FindBranch_rent = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProduct_li = Nothing
	End Function
	
	'% insRoutineDuration : LLama a la rutina para el cálculo de la duración del seguro.
	Public Function insRoutineDuration(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dStartdate As Date, ByVal dEffecdate As Date, ByVal sRoutine As String, ByVal bRoutInsu As Boolean) As Boolean
		Dim lrecreaRoutine As eRemoteDB.Execute
		
		lrecreaRoutine = New eRemoteDB.Execute
		
		On Error GoTo insRoutineDuration_Err
		
		With lrecreaRoutine
			Select Case UCase(sRoutine)
				Case "CAL_VIGSCH"
					.StoredProcedure = "InsCal_VigSchool"
				Case "CAL_VIGHSCH"
					.StoredProcedure = "InsCal_VigHSchool"
				Case "CALDURPAYSCH"
					.StoredProcedure = "CAL_DURPAYSCH"
			End Select
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDurInsur", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				'+ Si se trata de la duración del seguro
				If bRoutInsu Then
					Me.nIdurafix = .Parameters.Item("nDurInsur").Value
				Else
					'+ Si se trata de la duración de los pagos del seguro
					Me.nPdurafix = .Parameters.Item("nDurInsur").Value
				End If
				insRoutineDuration = True
			End If
		End With
		
insRoutineDuration_Err: 
		If Err.Number Then
			insRoutineDuration = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaRoutine may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaRoutine = Nothing
	End Function
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsProduct = Nothing
		'UPGRADE_NOTE: Object mclsCliallopro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCliallopro = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% valProductGeneral: It looks for the corresponding data for a product
	'% valProductGeneral: Verifica la existencia de información de un producto.
	Public Function valProductGeneral(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecProduct As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo valProductGeneral_Err
		
		If nBranch = Me.nBranch And nProduct = Me.nProduct And dEffecdate = Me.dEffecdate And Not bFind Then
			valProductGeneral = True
		Else
			lrecProduct = New eRemoteDB.Execute
			With lrecProduct
				.StoredProcedure = "valProductGeneral"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Run(False)
				valProductGeneral = (.Parameters("nExists").Value = 1)
			End With
		End If
		
valProductGeneral_Err: 
		If Err.Number Then
			valProductGeneral = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProduct = Nothing
	End Function
	
	'% insPostDP064: Actualiza los campos puntuales de la ventana de cargos
	Public Function insPostDP064(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nDayBuyUnit As Integer) As Boolean
		On Error GoTo insPostDP064_Err
		
		insPostDP064 = Find(nBranch, nProduct, dEffecdate)
		
		If insPostDP064 Then
			
			'+ Esta asignación es para utilizar la información entrante en todas
			'+ las funciones llamadas dentro de insPostDP023, sin tener que pasarla como parámetro
			
			With Me
				.nBranch = nBranch
				.nProduct = nProduct
				.dEffecdate = dEffecdate
				.nDayBuyUnit = nDayBuyUnit
			End With
			
			insPostDP064 = FindProduct_li(nBranch, nProduct, dEffecdate, True)
			
			With Me
				.nBranch = nBranch
				.nProduct = nProduct
				.dEffecdate = dEffecdate
				.nDayBuyUnit = nDayBuyUnit
			End With
			
			
			If dEffecdate = Me.dEffecdateProduct_li Then
				insPostDP064 = UpdateProduct_Li
			Else
				Me.dEffecdate = dEffecdate
				insPostDP064 = UpdProduct_liDPost
			End If
		End If
		
insPostDP064_Err: 
		If Err.Number Then
			insPostDP064 = False
		End If
		On Error GoTo 0
		
	End Function

    '% FindGM: Esta rutina permite leer los datos generales del GM.
    Public Function FindGM(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByRef bFind As Boolean = False) As Boolean
        Dim lrec_ProdMaster As eRemoteDB.Execute

        On Error GoTo FindGM_Err

        If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or bFind Then
            lrec_ProdMaster = New eRemoteDB.Execute
            With lrec_ProdMaster
                .StoredProcedure = "reaGM"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    FindGM = True
                    blnError = True
                    Me.nBranch = .FieldToClass("nBranch")
                    Me.nProduct = .FieldToClass("nProduct")
                    Me.sBrancht = .FieldToClass("sBrancht")
                    Me.sDescript = .FieldToClass("sDescript")
                    Me.sRealind = .FieldToClass("sRealind")
                    Me.sShort_des = .FieldToClass("sShort_des")
                    Me.sStatregt = .FieldToClass("sStatregt")
                    .RCloseRec()
                End If
            End With
        Else
            FindGM = True
        End If

FindGM_Err:
        If Err.Number Then
            FindGM = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrec_ProdMaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrec_ProdMaster = Nothing
    End Function

End Class






