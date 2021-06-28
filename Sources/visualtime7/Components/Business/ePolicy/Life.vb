Option Strict Off
Option Explicit On
Public Class Life
	'%-------------------------------------------------------%'
	'% $Workfile:: Life.cls                                 $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 2-09-09 19:36                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	'   Column_name                  Type            Computed   Length  Prec  Scale  Nullable  TrimTrailingBlanks  FixedLenNullInSource
	'----------------------  ---------------------   ---------  ------  ----- ------ --------  ------------------  --------------------
	Public sCertype As String 'char       no         1                     no           no                  no
	Public nProduct As Integer 'smallint   no         2      5     0        no          (n/a)               (n/a)
	Public nBranch As Integer 'smallint   no         2      5     0        no          (n/a)               (n/a)
	Public nPolicy As Double 'int        no         4      10    0        no          (n/a)               (n/a)
	Public nCertif As Double 'int        no         4      10    0        no          (n/a)               (n/a)
	Public dEffecdate As Date 'datetime   no         8                     no          (n/a)               (n/a)
	Public nAge As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public sClient As String 'char       no         14                    yes          no                  yes
	Public nAge_limit As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public nAge_reinsu As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public sAmorti_way As String 'char       no         1                     yes          no                  yes
	Public nCapital As Double 'decimal    no         9      12    0        yes         (n/a)               (n/a)
	Public nCapital_ca As Double 'decimal    no         9      12    0        yes         (n/a)               (n/a)
	Public dCompdate As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nEnd_num As Integer 'int        no         4      10    0        yes         (n/a)               (n/a)
	Public nEnt_right As Integer 'int        no         4      10    0        yes         (n/a)               (n/a)
	Public nExa_amount As Double 'decimal    no         9      10    0        yes         (n/a)               (n/a)
	Public nExam_type As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public dExpirdat As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public sIduraind As String 'char       no         1                     yes          no                  yes
	Public nInit_num As Integer 'int        no         4      10    0        yes         (n/a)               (n/a)
	Public nInsur_time As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public dIssuedat As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public sLoan_numbe As String 'char       no         10                    yes          no                  yes
	Public nNullcode As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public dNulldate As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nPay_time As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public sPduraind As String 'char       no         1                     yes          no                  yes
	Public nPermulti As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nPernumai As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nPernunmi As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nPremium As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nPremium_ca As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nReceipt As Integer 'int        no         4      10    0        yes         (n/a)               (n/a)
	Public nSald_amoun As Double 'decimal    no         9      12    0        yes         (n/a)               (n/a)
	Public sSald_prog As String 'char       no         1                     yes          no                  yes
	Public dStartdate As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nTitles_sub As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public nUsercode As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public nWar_int_ex As Double 'decimal    no         5      4     2        yes         (n/a)               (n/a)
	Public nWar_intere As Double 'decimal    no         5      4     2        yes         (n/a)               (n/a)
	Public nXprem_time As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public nYears_old As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public nTransactio As Integer 'smallint   no         2      5     0        yes         (n/a)               (n/a)
	Public dProg_date As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public dDate_pay As Date
	Public nTypDurpay As Integer
	Public nTypDurins As Integer
	Public nRatedesg As Double
	Public dDate_end As Date 'datetime   no         8                     no          (n/a)               (n/a)
	
	'+ Requerimiento según hoja de analisis n° 301
	Public nRentamount As Double ' NUMBER        22    12      2 Yes
	Public nPerc_cap As Double ' NUMBER        22    12      2 Yes
	Public nCurrrent As Integer ' NUMBER        22     5      0 Yes
	Public sCreditnum As String ' CHAR          20              Yes
	Public nCred_pro As Integer ' NUMBER        22     5      0 Yes
	Public dInit_cre As Date ' DATE           7              Yes
	Public dEnd_cre As Date ' DATE           7              Yes
	Public nCurren_cre As Integer ' NUMBER        22     5      0 Yes
	Public nAmount_cre As Double ' NUMBER        22    12      0 Yes
	Public nAmount_act As Double ' NUMBER        22    12      0 Yes
	Public nCount_insu As Integer ' NUMBER        22     5      0 Yes
	Public sAccnum As String ' VARCHAR2      20              Yes
	Public nCalcapital As Integer ' NUMBER        22     5      0 Yes
	Public nTyppremium As Integer ' NUMBER        22     5      0 Yes
	Public nGroup As Integer ' NUMBER        22     5      0 Yes
	Public nSituation As Integer ' NUMBER        22     5      0 Yes
	Public nCapitalmax As Double ' NUMBER        22    12      0 Yes
	Public nQ_quot As Short ' Number        22     3      0 Yes
	
	'- VI7001 Modificación interés asegurable
	Public nSaving_pct As Integer
	Public nDisc_save_pct As Integer
	Public nDisc_unit_pct As Integer
	Public nIndex_table As Integer
	Public nWarrn_table As Integer
	Public nOption As Integer
	Public nPremiumbas As Double
	Public nModulec As Integer
	Public nPremDeal As Double
	Public nPremDeal_anu As Double
	Public nPremMin As Double
	Public nIntwarr As Double
	Public nIntwarrVar As Double
	Public nIntwarrExc As Double
	Public nIntwarrExcVar As Double
	Public nIntwarrMin As Double
	
	'-VI1410 Ilustración del valor póliza VUL
	Public nCurrency As Integer
	Public nAmountcontr As Double
	Public nPremdif As Double
	Public nRatepayf As Double
	Public nVPprdeal As Double
	Public nVpi As Double
	Public dEffecdate_to As Date
	Public dBirthdate As Date
	Public sOption As String
	Public sPayfreq As String
	Public nError As Integer
	Public nFreqProy As Short
	Public nInitialPayment As Double
	Public sApv As String
	
	
	'-Propiedades Auxiliares
	'    Public nGroup      As long
	'    Public nSituation  As long
	Public dRescuedate As Date
	Public sTabname As String
	
	'-Se define la variabble que cntendrá los años a sumar por ser fumador
	Public pintAgeSmoke As Integer
	
	'-Se define la variable que contendrá los años a restar por ser no fumador
	Public pintAgeNsmoke As Integer
	
	'-Se define la variabble que contendrá los años a restar por ser mujer
	Public pintAgeWom As Integer
	
	Public nExists As Integer
	Public nCert_er As Double
	
	Public oBillingItems As BillingItems
	Public nDivide As Double
	Public nMultiply As Double
        Public nType_Rateproy As Double
	
	
	
	'% Find: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Fire'
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaCertificnn As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecreaCertificnn = New eRemoteDB.Execute

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.dEffecdate <> dEffecdate Or bFind Then

            With lrecreaCertificnn
                .StoredProcedure = "reaCertificnn"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find = .Run
                If Find Then
                    Me.sCertype = .FieldToClass("sCertype")
                    Me.nProduct = .FieldToClass("nProduct")
                    Me.nBranch = .FieldToClass("nBranch")
                    Me.nPolicy = .FieldToClass("nPolicy")
                    Me.nCertif = .FieldToClass("nCertif")
                    Me.dEffecdate = .FieldToClass("dEffecdate")
                    Me.nAge = .FieldToClass("nAge")
                    Me.sClient = .FieldToClass("sClient")
                    Me.nAge_limit = .FieldToClass("nAge_limit")
                    Me.nAge_reinsu = .FieldToClass("nAge_reinsu")
                    Me.sAmorti_way = .FieldToClass("sAmorti_way")
                    Me.nCapital = .FieldToClass("nCapital")
                    Me.nCapital_ca = .FieldToClass("nCapital_ca")
                    Me.dCompdate = .FieldToClass("dCompdate")
                    Me.nEnd_num = .FieldToClass("nEnd_num")
                    Me.nEnt_right = .FieldToClass("nEnt_right")
                    Me.nExa_amount = .FieldToClass("nExa_amount")
                    Me.nExam_type = .FieldToClass("nExam_type")
                    Me.dExpirdat = .FieldToClass("dExpirdat")
                    Me.sIduraind = .FieldToClass("sIduraind")
                    Me.nInit_num = .FieldToClass("nInit_num")
                    Me.nInsur_time = .FieldToClass("nInsur_time")
                    Me.dIssuedat = .FieldToClass("dIssuedat")
                    Me.sLoan_numbe = .FieldToClass("sLoan_numbe")
                    Me.nNullcode = .FieldToClass("nNullcode")
                    Me.dNulldate = .FieldToClass("dNulldate")
                    Me.nPay_time = .FieldToClass("nPay_time")
                    Me.sPduraind = .FieldToClass("sPduraind")
                    Me.nPermulti = .FieldToClass("nPermulti")
                    Me.nPernumai = .FieldToClass("nPernumai")
                    Me.nPernunmi = .FieldToClass("nPernunmi")
                    Me.nPremium = .FieldToClass("nPremium")
                    Me.nPremium_ca = .FieldToClass("nPremium_ca")
                    Me.nReceipt = .FieldToClass("nReceipt")
                    Me.nSald_amoun = .FieldToClass("nSald_amoun")
                    Me.sSald_prog = .FieldToClass("sSald_prog")
                    Me.dStartdate = .FieldToClass("dStartdate")
                    Me.nTitles_sub = .FieldToClass("nTitles_sub")
                    Me.nUsercode = .FieldToClass("nUsercode")
                    Me.nWar_int_ex = .FieldToClass("nWar_int_ex")
                    Me.nWar_intere = .FieldToClass("nWar_intere")
                    Me.nXprem_time = .FieldToClass("nXprem_time")
                    Me.nYears_old = .FieldToClass("nYears_old")
                    Me.nTransactio = .FieldToClass("nTransactio")
                    Me.dProg_date = .FieldToClass("dProg_date")
                    Me.nRentamount = .FieldToClass("nRentamount")
                    Me.nCurrrent = .FieldToClass("nCurrrent")
                    Me.sCreditnum = .FieldToClass("sCreditnum")
                    Me.nCred_pro = .FieldToClass("nCred_pro")
                    Me.dInit_cre = .FieldToClass("dInit_cre")
                    Me.dEnd_cre = .FieldToClass("dEnd_cre")
                    Me.nCurren_cre = .FieldToClass("nCurren_cre")
                    Me.nAmount_cre = .FieldToClass("nAmount_cre")
                    Me.nAmount_act = .FieldToClass("nAmount_act")
                    Me.nCount_insu = .FieldToClass("nCount_insu")
                    Me.sAccnum = .FieldToClass("sAccnum")
                    Me.nCalcapital = .FieldToClass("nCalcapital")
                    Me.nTyppremium = .FieldToClass("nTyppremium")
                    Me.nGroup = .FieldToClass("nGroup")
                    Me.nSituation = .FieldToClass("nSituation")
                    Me.nCapitalmax = .FieldToClass("nCapitalmax")
                    Me.nPerc_cap = .FieldToClass("nPerc_cap")
                    Me.dDate_pay = .FieldToClass("dDate_pay")
                    Me.nTypDurpay = .FieldToClass("nTypDurpay")
                    Me.nTypDurins = .FieldToClass("nTypDurins")
                    Me.nRatedesg = .FieldToClass("nRateDesg")
                    Me.nQ_quot = .FieldToClass("nQ_Quot")

                    '+ [APV2] HAD 1022. VI7001 Modificación interés asegurable DBLANCO 13-08-2003
                    nSaving_pct = .FieldToClass("nSaving_pct")
                    nDisc_save_pct = .FieldToClass("nDisc_save_pct")
                    nDisc_unit_pct = .FieldToClass("nDisc_unit_pct")
                    nIndex_table = .FieldToClass("nIndex_table")
                    nWarrn_table = .FieldToClass("nWarrn_table")
                    nOption = .FieldToClass("nOption")
                    .RCloseRec()
                End If
            End With
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaCertificnn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificnn = Nothing
    End Function
	
	
	'%insReaAge:lee los indicadores de duración para el pago y el seguro de la tabla Age
    Public Function insReaAge(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaAge As eRemoteDB.Execute
        Dim lclsProduct As eProduct.Product

        '-Se define la variable que contendrá la edad del asegurado
        Dim ldatAge As Date

        '-Se deifne la variable que contendrá el sexo del asegurado
        Dim lstrSex As String

        '-Se define la variable que contendrá el promedio de edades totales
        Dim lintTotalAge As Integer

        '-Se define la variable que contendrá el prom,edio de las edades actuariales totales
        Dim lintTotalAgeReinsu As Integer

        '-Se define la variable que contendrá la edad del cliente en tratamiento
        Dim llngAge As Integer

        '-Se define la variable que contendrá el número de clientes
        Dim lintClientNumber As Integer

        '-Se define la variable que contendrá el valor del indicador de fumadores
        Dim lstrSmoker As String

        '-Se define la variable que contendrá la edad real del asegurado
        Dim lintAgeTmp As Integer

        '-Se define la variable que contendrá el objeto del asegurado
        Dim lobjRoles As ePolicy.Roles

        On Error GoTo insReaAge_Err

        lrecreaAge = New eRemoteDB.Execute
        lclsProduct = New eProduct.Product

        Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)

        pintAgeNsmoke = IIf(lclsProduct.nYearmins = eRemoteDB.Constants.intNull, 0, lclsProduct.nYearmins)
        pintAgeSmoke = IIf(lclsProduct.nYearmors = eRemoteDB.Constants.intNull, 0, lclsProduct.nYearmors)
        pintAgeWom = IIf(lclsProduct.nYearminw = eRemoteDB.Constants.intNull, 0, lclsProduct.nYearminw)

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing

        With lrecreaAge
            .StoredProcedure = "reaAge"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insReaAge = .Run
            If insReaAge Then
                ldatAge = .FieldToClass("dBirthdat")

                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If Not IsDBNull(.FieldToClass("sSexclien")) Then
                    lstrSex = CStr(.FieldToClass("sSexclien"))
                Else
                    lstrSex = "3"
                End If

                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If Not IsDBNull(.FieldToClass("sSmoking")) Then
                    lstrSmoker = CStr(.FieldToClass("sSmoking"))
                Else
                    lstrSmoker = "2"
                End If
                lobjRoles = New ePolicy.Roles

                Call lobjRoles.CalInsuAge(nBranch, nProduct, dEffecdate, ldatAge, lstrSex, lstrSmoker, 2)


                '+Se muestran los valores de las edades reales y actuariales promediadas
                nAge = lobjRoles.mintAge
                nAge_reinsu = lobjRoles.mintInsuAge
                'UPGRADE_NOTE: Object lobjRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjRoles = Nothing

                .RCloseRec()
            End If
        End With

insReaAge_Err:
        If Err.Number Then
            insReaAge = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaAge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAge = Nothing
    End Function
	
	'%InsUpdNullLife: Esta función se encarga de anular el registro asociado al certificado en la tabla de vida
	Public Function InsUpdNullLife() As Boolean
		Dim lrecupdLife_null As eRemoteDB.Execute
		
		On Error GoTo InsUpdNullLife_Err
		lrecupdLife_null = New eRemoteDB.Execute
		With lrecupdLife_null
			.StoredProcedure = "UpdLife_null"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdNullLife = .Run(False)
		End With
		
InsUpdNullLife_Err: 
		If Err.Number Then
			InsUpdNullLife = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdLife_null may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLife_null = Nothing
	End Function
	
	'%Update_O: Actualización de los campos de la tabla de vida.
	Public Function Update_O() As Boolean
		Dim lrecupdlife As eRemoteDB.Execute
		
		On Error GoTo updlife_Err
		
		lrecupdlife = New eRemoteDB.Execute
		
		With lrecupdlife
			.StoredProcedure = "updlife"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_limit", nAge_limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAmorti_way", sAmorti_way, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_ca", nCapital_ca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCompdate", dCompdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_num", nEnd_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnt_right", nEnt_right, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExa_amount", nExa_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExam_type", nExam_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'        .Parameters.Add "sIduraind", sIduraind, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			.Parameters.Add("nInit_num", nInit_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_time", nInsur_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssuedat", dIssuedat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLoan_numbe", sLoan_numbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_time", nPay_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'        .Parameters.Add "sPduraind", sPduraind, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			.Parameters.Add("nPermulti", nPermulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPernumai", nPernumai, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPernunmi", nPernunmi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium_ca", nPremium_ca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSald_amoun", nSald_amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSald_prog", sSald_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTitles_sub", nTitles_sub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWar_int_ex", nWar_int_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWar_intere", nWar_intere, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nXprem_time", nXprem_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYears_old", nYears_old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProg_date", dProg_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRentamount", nRentamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrrent", nCurrrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCreditnum", sCreditnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCred_pro", nCred_pro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_cre", dInit_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_cre", dEnd_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurren_cre", nCurren_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_cre", nAmount_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_act", nAmount_act, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_insu", nCount_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccnum", sAccnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCalcapital", nCalcapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyppremium", nTyppremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitalmax", nCapitalmax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateDesg", nRatedesg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_Quot", nQ_quot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update_O = True
			End If
		End With
		
updlife_Err: 
		If Err.Number Then
			Update_O = False
		End If
		'UPGRADE_NOTE: Object lrecupdlife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdlife = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Terminate: controla la destrucción de la instancia del objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% ClearFields: se inicializa el valor de las variables de la clase
	Public Function ClearFields() As Boolean
		ClearFields = True
		sCertype = String.Empty
		nProduct = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nAge = eRemoteDB.Constants.intNull
		sClient = String.Empty
		nAge_limit = eRemoteDB.Constants.intNull
		nAge_reinsu = eRemoteDB.Constants.intNull
		sAmorti_way = String.Empty
		nCapital = eRemoteDB.Constants.intNull
		nCapital_ca = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nEnd_num = eRemoteDB.Constants.intNull
		nEnt_right = eRemoteDB.Constants.intNull
		nExa_amount = eRemoteDB.Constants.intNull
		nExam_type = eRemoteDB.Constants.intNull
		dExpirdat = eRemoteDB.Constants.dtmNull
		sIduraind = String.Empty
		nInit_num = eRemoteDB.Constants.intNull
		nInsur_time = eRemoteDB.Constants.intNull
		dIssuedat = eRemoteDB.Constants.dtmNull
		sLoan_numbe = String.Empty
		nNullcode = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nPay_time = eRemoteDB.Constants.intNull
		sPduraind = String.Empty
		nPermulti = eRemoteDB.Constants.intNull
		nPernumai = eRemoteDB.Constants.intNull
		nPernunmi = eRemoteDB.Constants.intNull
		nPremium = eRemoteDB.Constants.intNull
		nPremium_ca = eRemoteDB.Constants.intNull
		nReceipt = eRemoteDB.Constants.intNull
		nSald_amoun = eRemoteDB.Constants.intNull
		sSald_prog = String.Empty
		dStartdate = eRemoteDB.Constants.dtmNull
		nTitles_sub = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nWar_int_ex = eRemoteDB.Constants.intNull
		nWar_intere = eRemoteDB.Constants.intNull
		nXprem_time = eRemoteDB.Constants.intNull
		nYears_old = eRemoteDB.Constants.intNull
		nTransactio = eRemoteDB.Constants.intNull
		dProg_date = eRemoteDB.Constants.dtmNull
		nRentamount = eRemoteDB.Constants.intNull
		nCurrrent = eRemoteDB.Constants.intNull
		sCreditnum = String.Empty
		nCred_pro = eRemoteDB.Constants.intNull
		dInit_cre = eRemoteDB.Constants.dtmNull
		dEnd_cre = eRemoteDB.Constants.dtmNull
		nCurren_cre = eRemoteDB.Constants.intNull
		nAmount_cre = eRemoteDB.Constants.intNull
		nAmount_act = eRemoteDB.Constants.intNull
		nCount_insu = eRemoteDB.Constants.intNull
		sAccnum = String.Empty
		nCalcapital = eRemoteDB.Constants.intNull
		nTyppremium = eRemoteDB.Constants.intNull
		nGroup = eRemoteDB.Constants.intNull
		nSituation = eRemoteDB.Constants.intNull
		nCapitalmax = eRemoteDB.Constants.intNull
		nPerc_cap = eRemoteDB.Constants.intNull
		nRatedesg = eRemoteDB.Constants.intNull
		nQ_quot = eRemoteDB.Constants.intNull
	End Function
	
	'% insValVI701: Valida los campo de la transaccion "VI701"
    Public Function insValVI701(ByVal sCodispl As String, ByVal nTransactio As Integer, ByVal sCertype As String, _
                                ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, _
                                ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, _
                                ByVal nAmount_cre As Double, ByVal nAmount_act As Double, ByVal nCurren_cre As Integer, _
                                ByVal nCalcapital As Integer, ByVal nTyppremium As Integer, ByVal nQ_quot As Short, _
                                ByVal dEnd_cre As Date, ByVal sCreditnum As String) As String
        '-Objeto con mensajes de error
        Dim lobjErrors As eFunctions.Errors
        '-Objeto para validacion de grupos
        Dim lclsGroups As ePolicy.Groups
        '-Objeto de validacion de poliza
        Dim lclsPolicy As ePolicy.Policy
        '-Objeto de validacion del certificado
        Dim lclsCertificat As ePolicy.Certificat

        Dim lclsRoles As ePolicy.Roles

        lobjErrors = New eFunctions.Errors

        lclsPolicy = New ePolicy.Policy
        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)

        lclsCertificat = New ePolicy.Certificat
        Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)

        With lobjErrors

            '+ No se indico grupo...
            If (nGroup = eRemoteDB.Constants.intNull) Then
                '+ ...pero se trata de una cotización o un certificado de colectivo
                If (lclsPolicy.sPolitype <> "1" And nCertif <> 0) Or (sCertype = "3") Then
                    '+ ...y se indicaron grupos colectivos
                    lclsGroups = New ePolicy.Groups
                    If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                        '+ ...este campo debe estar lleno.
                        Call .ErrorMessage(sCodispl, 10152)
                    End If
                    'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsGroups = Nothing
                End If
            Else
                '+ Debe ser un grupo válido para la póliza.
                lclsGroups = New ePolicy.Groups
                If Not lclsGroups.valGroupExistByStatus(sCertype, nBranch, nProduct, nPolicy, nGroup, "1", dEffecdate) Then
                    Call .ErrorMessage(sCodispl, 3136)
                End If
                'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsGroups = Nothing

                Dim lblnGroupChange As Boolean

                If Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                    With Me
                        '+Se determina si cambio el grupo
                        lblnGroupChange = .nGroup <> nGroup And .nGroup <> eRemoteDB.Constants.intNull

                        If lblnGroupChange Then
                            '+ Validacion que se eliminara la informacion de modulos/coberturas
                            Call lobjErrors.ErrorMessage(sCodispl, 4)
                        End If

                    End With
                End If
            End If

            '+ Moneda: Debe estar lleno.
            If (nCurren_cre = eRemoteDB.Constants.intNull) Then
                Call .ErrorMessage(sCodispl, 10107)
            End If

            '+ Tipo de cálculo de capital: Debe estar lleno
            If (nCalcapital = eRemoteDB.Constants.intNull) Then
                Call .ErrorMessage(sCodispl, 11331)
            End If

            '+ Tipo de cálculo de prima: Debe estar lleno
            If (nTyppremium = eRemoteDB.Constants.intNull) Then
                Call .ErrorMessage(sCodispl, 11172)
            End If

            '+Si no es la matriz del colectivo 
            '+Debe ingresar cantidad de cuotas
            If (lclsPolicy.sPolitype = "1" Or nCertif > 0) Then
                '+ Monto inicial: Debe estar lleno.
                If (nAmount_cre = eRemoteDB.Constants.intNull) Then
                    Call .ErrorMessage(sCodispl, 55686)
                End If

                '+ Saldo insoluto: Debe estar lleno.
                If (nAmount_act = eRemoteDB.Constants.intNull) Then
                    Call .ErrorMessage(sCodispl, 55687)
                End If

                '+Fecha de vencimiento debe ser mayor a la de inicio de vigencia
                If dEnd_cre < lclsCertificat.dStartdate Then
                    Call .ErrorMessage(sCodispl, 56021)
                End If

                If nQ_quot = eRemoteDB.Constants.intNull Then
                    Call .ErrorMessage(sCodispl, 56020)
                End If

                If sCreditnum = "" Then
                    Call .ErrorMessage(sCodispl, 9000045)
                End If

                If Val_nRepInsured(sCertype, nBranch, nProduct, nPolicy, nCertif, "", dEffecdate, sCreditnum) Then
                    If Me.nExists = 3 Then
                        Call .ErrorMessage(sCodispl, 750099, , , "(" & Me.nCert_er & ")")
                    ElseIf Me.nExists = 4 Then
                        Call .ErrorMessage(sCodispl, 56027, , , "(" & Me.nCert_er & ")")
                    End If
                End If
            End If

            lclsRoles = New ePolicy.Roles

            If lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, 2, vbNullString, dEffecdate) Then

                If Not INSCALMAXAGEPERM(nBranch, nProduct, nPolicy, dEffecdate, lclsRoles.dBirthdate, lclsRoles.SCLIENT, nQ_quot, lclsRoles.sSexclien) Then
                    Call .ErrorMessage(sCodispl, 2)
                End If

            End If


            lclsRoles = Nothing

            insValVI701 = .Confirm
        End With

insVal_Err:
        If Err.Number Then
            insValVI701 = CStr(False)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
    End Function

    '% insValVI701: Valida la duplicidad del credito "VI701"
    Public Function Val_nRepInsured(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, ByVal sCreditnum As String) As Boolean
        Dim lrecVal_nRepInsured As eRemoteDB.Execute

        On Error GoTo Val_nRepInsured_Err

        lrecVal_nRepInsured = New eRemoteDB.Execute

        With lrecVal_nRepInsured
            .StoredProcedure = "INS_REPINSURED_POL"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCreditnum", sCreditnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCert_er", nCert_er, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Me.nExists = .Parameters("nExists").Value
                Me.nCert_er = .Parameters("nCert_er").Value
                Val_nRepInsured = True
            End If

        End With

Val_nRepInsured_Err:
        If Err.Number Then
            Val_nRepInsured = False
        End If

        'UPGRADE_NOTE: Object lrecVal_nRepInsured may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecVal_nRepInsured = Nothing
        On Error GoTo 0

    End Function

    '%INSCALMAXAGEPERM: Se realiza la actualización de los datos en la ventana VI701, cuando se cambia de grupos
    Public Function INSCALMAXAGEPERM(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, _
                                     ByVal dStartdate As Date, ByVal dBirthdate As Date, ByVal sClient As String, _
                                     ByVal nQuotas As Integer, ByVal sSexclien As String) As Boolean
        Dim lrecLife As eRemoteDB.Execute

        On Error GoTo insCreUpdLife_Err

        lrecLife = New eRemoteDB.Execute

        With lrecLife
            .StoredProcedure = "REAMAXAGEPERM_COVER"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStardate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuotas", nQuotas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                INSCALMAXAGEPERM = .FieldToClass("NOK")
            End If




        End With

insCreUpdLife_Err:
        If Err.Number Then
            INSCALMAXAGEPERM = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecLife = Nothing

    End Function


    '%insChangeGroups: Se realiza la actualización de los datos en la ventana VI701, cuando se cambia de grupos
    Public Function insChangeGroups(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                                    ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, _
                                    ByVal nGroup As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lrecLife As eRemoteDB.Execute

        On Error GoTo insCreUpdLife_Err

        lrecLife = New eRemoteDB.Execute

        With lrecLife
            .StoredProcedure = "INSCHANGEGROUPS"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insChangeGroups = .Run(False)
        End With

insCreUpdLife_Err:
        If Err.Number Then
            insChangeGroups = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecLife = Nothing

    End Function

    '%insPostVI701: Se realiza la actualización de los datos en la ventana VI701
    Public Function insPostVI701(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                                 ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, _
                                 ByVal nGroup As Integer, ByVal nAmount_cre As Double, ByVal nAmount_act As Double, _
                                 ByVal nCurren_cre As Integer, ByVal nCalcapital As Integer, ByVal nTyppremium As Integer, _
                                 ByVal nSituation As Integer, ByVal sCreditnum As String, ByVal nCred_pro As Integer, _
                                 ByVal dInit_cre As Date, ByVal dEnd_cre As Date, ByVal sAccnum As String, _
                                 ByVal nCapitalmax As Double, ByVal nUsercode As Integer, ByVal nRatedesg As Double, _
                                 ByVal nQ_quot As Short, ByVal nTransaction As Integer, ByVal nPremium_ca As Double) As Boolean
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsRoles As ePolicy.Roles
        Dim lcolRoles As ePolicy.Roleses
        Dim sFracti As String
        '-Indicador que cambio fin de vigencia y/o duracion
        Dim lblnNewEnd As Boolean
        '-Indicador de cambio en alguna infiormacion del credito
        Dim lblnAnyChange As Boolean
        Dim nDurationAux As Integer

        On Error GoTo insPostVI701_Err

        '+Actualización de la Póliza
        lclsPolicy = New ePolicy.Policy
        With lclsPolicy
            Call .Find(sCertype, nBranch, nProduct, nPolicy)
        End With

        Call Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
        With Me

            If .nGroup <> nGroup Then
                '+ Se eliminan los modulos/coberturas.
                insChangeGroups(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, .nGroup, nUsercode)
            End If

            '+Se determina si cambio el fin de vigencia del credito
            lblnNewEnd = .dEnd_cre <> dEnd_cre
            lblnAnyChange = .nGroup <> nGroup OrElse
                            .nAmount_cre <> nAmount_cre OrElse
                            .nAmount_act <> nAmount_act OrElse
                            .nCurren_cre <> nCurren_cre OrElse
                            .nCalcapital <> nCalcapital OrElse
                            .nTyppremium <> nTyppremium OrElse
                            .nSituation <> nSituation OrElse
                            .sCreditnum <> sCreditnum OrElse
                            .nCred_pro <> nCred_pro OrElse
                            .dInit_cre <> dInit_cre OrElse
                            .dEnd_cre <> dEnd_cre OrElse
                            .sAccnum <> sAccnum OrElse
                            .nCapitalmax <> nCapitalmax OrElse
                            .nRatedesg <> nRatedesg OrElse
                            .nQ_quot <> nQ_quot

            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nGroup = nGroup
            .nAmount_cre = nAmount_cre
            .nAmount_act = nAmount_act
            .nCurren_cre = nCurren_cre
            .nCalcapital = nCalcapital
            .nTyppremium = nTyppremium
            .nSituation = nSituation
            .sCreditnum = sCreditnum
            .nCred_pro = nCred_pro
            .dInit_cre = dInit_cre
            .dEnd_cre = dEnd_cre
            .sAccnum = sAccnum
            .nCapitalmax = nCapitalmax
            .nUsercode = nUsercode
            .nRatedesg = nRatedesg
            .nQ_quot = nQ_quot
            .nPremium_ca = nPremium_ca
            If dEnd_cre <> eRemoteDB.Constants.dtmNull And nCertif > 0 Then
                '+SI TIPO DE RENOVACIÓN ES SIMULTANEA
                If lclsPolicy.sColtimre = "1" And dEnd_cre > lclsPolicy.DEXPIRDAT Then
                    .dExpirdat = lclsPolicy.DEXPIRDAT
                Else
                    .dExpirdat = dEnd_cre
                End If
            End If

            '+Llama el procedimiento de actualizacion
            insPostVI701 = .Update_O()
        End With

        '+Actualización de estado de transacciones
        lclsPolicy_Win = New ePolicy.Policy_Win

        '+Si hubo cambio de vigencia se debe actualizar certificat
        If insPostVI701 And (lblnNewEnd Or lblnAnyChange) And nCertif > 0 Then
            lclsCertificat = New ePolicy.Certificat
            With lclsCertificat
                Call .Find(sCertype, nBranch, nProduct, nPolicy, nCertif)

                .nUsercode = nUsercode
                '+Se realiza condicion inversa a la existente en insCertificat_CA004
                If .sProrShort = "9" Then
                    sFracti = CStr(System.Windows.Forms.CheckState.Checked)
                Else
                    sFracti = "2"
                End If
                '+Se deteremina si cambio duracion
                lblnNewEnd = .nDuration <> nQ_quot

                If lclsPolicy.sPolitype <> "1" And lclsPolicy.sColtimre = "1" And nCertif <> 0 Then
                    nDurationAux = .nDuration
                Else
                    nDurationAux = nQ_quot
                End If

                '+ si los recibos son por situacion de riesgo, entonces el rut de los recibos es el certificado y no la matriz
                If Me.nSituation <> 0 Then
                    Dim lclsSituation As New Situation
                    If lclsSituation.FindSituationData(sCertype, nBranch, nProduct, nPolicy, Me.nSituation, True) Then
                        .sClient = lclsSituation.sClient
                    End If
                    lclsSituation = Nothing
                End If

                insPostVI701 = .insCertificat_CA004(.sClient, .nCertif, nTransaction, .sCertype, .nBranch, .nProduct, .nPolicy, .nPayfreq, .nQuota, .dStartdate, dEnd_cre, .dIssuedat, .dPropodat, .sRenewal, sFracti, .sProrShort, .sDirind, .nWay_pay, .nBill_day, .nSendAddr, .nDays_quot, dEffecdate, .sBill_Ind, nDurationAux, .sExemption, .nOrigin, .nAFP_Commiss, .nAFP_Comm_Curr, Me.nGroup, Me.nSituation)
                '+Las sgtes condiciones fueron replicadas desde el post de
                '+la transaccion CA004 (insPostCA004)

                '+Si se cambia los meses de duracion
                If lblnNewEnd Then
                    '+Si es distinto de modificacion o propuesta de modificación se dejan transacciones requerida sin contenido
                    If nTransaction <> Constantes.PolTransac.clngPolicyPropAmendent And nTransaction <> Constantes.PolTransac.clngCertifPropAmendent And nTransaction <> Constantes.PolTransac.clngCertifAmendment And nTransaction <> Constantes.PolTransac.clngPolicyAmendment Then

                        lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI001", "3", , , , False)
                    End If
                End If

            End With
            lclsCertificat = Nothing
        End If

        '+Se actualiza en Policy_Win la ventana con contenido
        If insPostVI701 Then
            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI701", "2")

            '+ Si se cambio alguna informacion del credito se coloca la ventana de coberturas requerida para que se realice el recalculo de prima
            If lblnAnyChange Then
                lcolRoles = New ePolicy.Roleses
                If lcolRoles.Find_Tab_Covrol(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nModulec) Then
                    For Each lclsRoles In lcolRoles
                        Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014" & lclsRoles.nCoverPos, "3")
                    Next
                End If
            End If
        Else
            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI701", "1")
        End If

insPostVI701_Err:
        If Err.Number Then
            insPostVI701 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
    End Function

    '%insCreUpdLife: Rutina que permite leer la información de la tabla de datos básicos de
    '%Cobertura en la Tarifa del Ramo de Atención Médica.
    Public Function insCreUpdLife(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nGroup_comp As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lrecLife As eRemoteDB.Execute

        On Error GoTo insCreUpdLife_Err

        lrecLife = New eRemoteDB.Execute

        With lrecLife
            .StoredProcedure = "insCreUpdLife"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            '+ Si se trata de una emisión
            If nTransaction = 1 Or nTransaction = 2 Or nTransaction = 3 Or nTransaction = 4 Or nTransaction = 5 Or nTransaction = 6 Or nTransaction = 7 Or nTransaction = 18 Or nTransaction = 19 Or nTransaction = 30 Or nTransaction = 31 Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nIndic", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                '+ Si se trata de una modificación normal
                If nTransaction = 12 Or nTransaction = 14 Then
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nIndic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Else
                    '+ Si se trata de una modificación temporal
                    If nTransaction = 15 Or nTransaction = 13 Then
                        .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nIndic", "3", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nIndic", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If
                End If
            End If
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insCreUpdLife = .Run(False)
        End With

insCreUpdLife_Err:
        If Err.Number Then
            insCreUpdLife = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecLife = Nothing
    End Function

    '%Objetivo: Función que retorna VERDADERO al actualizar un registro en la tabla 'Life'
    Public Function UpdateVI7001() As Boolean
        Dim lrecupdLife1 As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecupdLife1 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updLife1'
        '+ Información leída el 21/12/2000 2:38:33 p.m.

        With lrecupdLife1
            .StoredProcedure = "updLife1VI7001"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_Limit", nAge_limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_Reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium_ca", nPremium_ca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsurTime", nInsur_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayTime", nPay_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIduraind", sIduraind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPduraind", sPduraind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSaving_pct", nSaving_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisc_save_pct", nDisc_save_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisc_unit_pct", nDisc_unit_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndex_table", nIndex_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWarrn_table", nWarrn_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremiumBas", nPremiumbas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremdeal", nPremDeal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremdeal_anu", nPremDeal_anu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremmin", nPremMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntwarr", nIntwarr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurins", nTypDurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypDurpay", nTypDurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateVI7001 = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecupdLife1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdLife1 = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lrecupdLife1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdLife1 = Nothing
        UpdateVI7001 = False
    End Function

    '%InsPreVI1410: Obtiene los datos de la VA595
    Public Function InsPreVI1410(ByVal sReload As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransactio As Integer, Optional ByVal nCurrency As Integer = 0, Optional ByVal nPremiumbas As Double = 0, Optional ByVal nPremMin As Double = 0, Optional ByVal nVPprdeal As Double = 0, Optional ByVal nPremDeal As Double = 0, Optional ByVal nPremDeal_anu As Double = 0, Optional ByVal nAmountcontr As Double = 0, Optional ByVal nIntwarr As Double = 0, Optional ByVal nRatepayf As Double = 0, Optional ByVal nInsur_time As Integer = 0, Optional ByVal nVpi As Double = 0, Optional ByVal dEffecdate_to As Date = #12:00:00 AM#, Optional ByVal dBirthdate As Date = #12:00:00 AM#, Optional ByVal nOption As Integer = 0, Optional ByVal sOption As String = "", Optional ByVal sPayfreq As String = "") As Boolean
        On Error GoTo InsPreVI1410_Err
        If sReload = String.Empty Then
            InsPreVI1410 = InsGetDataVI1410(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nTransactio)


        Else
            InsPreVI1410 = True
            Me.nCurrency = nCurrency
            Me.nPremiumbas = nPremiumbas
            Me.nPremMin = nPremMin
            Me.nVPprdeal = nVPprdeal
            Me.nPremDeal = nPremDeal
            Me.nPremDeal_anu = nPremDeal_anu
            Me.nAmountcontr = nAmountcontr
            Me.nIntwarr = nIntwarr
            Me.nRatepayf = nRatepayf
            Me.nInsur_time = nInsur_time
            Me.nVpi = nVpi
            Me.dBirthdate = dBirthdate
            Me.dEffecdate_to = dEffecdate_to
            Me.nOption = nOption
            Me.sOption = sOption
            Me.sPayfreq = sPayfreq
            Me.nError = eRemoteDB.Constants.intNull
        End If

InsPreVI1410_Err:
        If Err.Number Then
            InsPreVI1410 = False
        End If
        On Error GoTo 0
    End Function

    '%InsGetDataVI1410: Obtiene los datos de la VA595
    Private Function InsGetDataVI1410(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransactio As Integer) As Boolean
        Dim lrecInsGetactivedata As eRemoteDB.Execute


        On Error GoTo InsGetDataVI1410_Err
        '+ Definición de store procedure InsGetactivedata al 04-08-2002 16:09:32
        lrecInsGetactivedata = New eRemoteDB.Execute
        With lrecInsGetactivedata
            .StoredProcedure = "InsGetDataVI1410"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                InsGetDataVI1410 = True
                Me.nCurrency = .FieldToClass("nCurrency")
                Me.nPremiumbas = .FieldToClass("nPremiumbas")
                Me.nPremMin = .FieldToClass("nPremmin")
                Me.nVPprdeal = .FieldToClass("nVpprdeal")
                Me.nPremDeal_anu = .FieldToClass("nPremdeal_anu")
                Me.nPremDeal = .FieldToClass("nPremdeal")
                Me.nAmountcontr = .FieldToClass("nAmountcontr")
                Me.nIntwarr = .FieldToClass("nIntwarr")
                Me.nRatepayf = .FieldToClass("nRatepayf")
                Me.nInsur_time = .FieldToClass("nInsur_time")
                Me.nVpi = .FieldToClass("nVpi")
                Me.dEffecdate_to = .FieldToClass("dEffecdate_to")
                Me.dBirthdate = .FieldToClass("dBirthdate")
                Me.nOption = .FieldToClass("nOption")
                Me.sOption = .FieldToClass("sOption")
                Me.sPayfreq = .FieldToClass("sPayfreq")
                Me.nError = .FieldToClass("nError")
                Me.nInitialPayment = .FieldToClass("NINITIAL_PAYMENT")
                Me.sApv = .FieldToClass("SAPV")
                Me.nDivide = .FieldToClass("nDivide")
                Me.nMultiply = .FieldToClass("nMultiply")
                Me.nType_Rateproy = .FieldToClass("nType_Rateproy")
                Call GetBillingItems(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, .FieldToClass("sKey"))
            End If
        End With

InsGetDataVI1410_Err:
        If Err.Number Then
            InsGetDataVI1410 = False
	    Err.Raise(Err.Number)
        End If
        'UPGRADE_NOTE: Object lrecInsGetactivedata may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsGetactivedata = Nothing
        On Error GoTo 0
    End Function


    '%InsPreVI7006: Obtiene los datos de la VA595
    Public Function InsPreVI7006(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransactio As Integer) As Boolean
        Dim lrecInsPreVI7006 As eRemoteDB.Execute

        On Error GoTo InsPreVI7006_Err
        '+ Definición de store procedure InsGetactivedata al 04-08-2002 16:09:32
        lrecInsPreVI7006 = New eRemoteDB.Execute
        With lrecInsPreVI7006
            .StoredProcedure = "INSVI7006PKG.InsPreVI7006"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                InsPreVI7006 = True
                Me.nCurrency = .FieldToClass("nCurrency")
                Me.nPremiumbas = .FieldToClass("nPremiumbas")
                Me.nPremDeal_anu = .FieldToClass("nPremdeal_anu")
                Me.nPremDeal = .FieldToClass("nPremdeal")
                Me.nPremdif = .FieldToClass("nPremdif")
                Me.nPremMin = .FieldToClass("nPremmin")
                Me.nIntwarr = .FieldToClass("nIntwarr")
                Me.nIntwarrVar = .FieldToClass("nIntwarrVar")
                Me.nIntwarrExc = .FieldToClass("nIntwarrExc")
                Me.nIntwarrExcVar = .FieldToClass("nIntwarrExcVar")
                Me.nIntwarrMin = .FieldToClass("nIntwarrMin")
                Me.dEffecdate_to = .FieldToClass("dEffecdate_to")
                Me.nFreqProy = .FieldToClass("nFreqProy")
                Me.dBirthdate = .FieldToClass("dBirthdate")
                Me.nOption = .FieldToClass("nOption")
                Me.sOption = .FieldToClass("sOption")
                Me.sPayfreq = .FieldToClass("sPayfreq")
                Me.nError = .FieldToClass("nError")
            End If
        End With

InsPreVI7006_Err:
        If Err.Number Then
            InsPreVI7006 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPreVI7006 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPreVI7006 = Nothing
        On Error GoTo 0
    End Function

    '%INSPREVI701: Obtiene los datos de la VI701
    Public Function InsPreVI701(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecINSPREVI701 As eRemoteDB.Execute


        On Error GoTo INSPREVI701_Err

        Call Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)


        '+ Se verififica si es un certificado y los valores a heredar estan llenos.
        '+ Con uno solo que no este lleno no va buscar
        If (nCertif > 0) And Not ((Me.nCred_pro > 0) Or (Me.nQ_quot > 0) Or (Me.nCurren_cre > 0) Or (Me.nCapital_ca > 0) Or (Me.nTyppremium > 0) Or (Me.nCapitalmax > 0) Or (Me.nRatedesg > 0)) Then
            lrecINSPREVI701 = New eRemoteDB.Execute
            With lrecINSPREVI701
                .StoredProcedure = "INSPREVI701PKG.INSPREVI701"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("ncred_pro", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nq_quot", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("ncurren_cre", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("ncapital_ca", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("ntyppremium", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("ncapitalmax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nratedesg", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPremium_ca", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run(False) Then
                    InsPreVI701 = True
                    Me.nCred_pro = .Parameters("ncred_pro").Value
                    Me.nQ_quot = .Parameters("nq_quot").Value
                    Me.nCurren_cre = .Parameters("ncurren_cre").Value
                    Me.nCalcapital = .Parameters("ncapital_ca").Value
                    Me.nTyppremium = .Parameters("ntyppremium").Value
                    Me.nCapitalmax = .Parameters("ncapitalmax").Value
                    Me.nRatedesg = .Parameters("nratedesg").Value
                    Me.nPremium_ca = .Parameters("nPremium_ca").Value
                End If
            End With
        End If
INSPREVI701_Err:
        If Err.Number Then
            InsPreVI701 = False
        End If
        'UPGRADE_NOTE: Object lrecINSPREVI701 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecINSPREVI701 = Nothing
        On Error GoTo 0
    End Function

    '+ Se valida la vigencia hasta del credito

    Public Function InsRoutineDuration(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNewDate As Date) As Boolean
        Dim lrecInsRoutineDuration As eRemoteDB.Execute


        On Error GoTo lrecInsRoutineDuration_Err

        lrecInsRoutineDuration = New eRemoteDB.Execute
        With lrecInsRoutineDuration
            .StoredProcedure = "InsRoutineDuration"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_End", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) And (.Parameters("dDate_End").Value <> eRemoteDB.Constants.dtmNull) Then
                InsRoutineDuration = True
                dDate_end = IIf(.Parameters("dDate_End").Value < dNewDate, .Parameters("dDate_End").Value, dNewDate)
            Else
                InsRoutineDuration = False
                dDate_end = dNewDate
            End If
        End With
lrecInsRoutineDuration_Err:
        If Err.Number Then
            InsRoutineDuration = False
        End If
        'UPGRADE_NOTE: Object lrecInsRoutineDuration may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsRoutineDuration = Nothing
        On Error GoTo 0
    End Function

    '-------------------------------------------------------
    Private Function GetBillingItems(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sKey As String) As Boolean
        '-------------------------------------------------------
        Dim lrecBillingItems As eRemoteDB.Execute

        On Error GoTo lrecBillingItems_Err

        lrecBillingItems = New eRemoteDB.Execute
        With lrecBillingItems
            .StoredProcedure = "reaBillingItemsVI1410"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("skey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                GetBillingItems = True
                oBillingItems = New BillingItems
                Do While Not .EOF
                    Call oBillingItems.Add(.FieldToClass("sConcept"), .FieldToClass("nAnnualPremium"), .FieldToClass("nProrratedPremium"), .FieldToClass("stype_detai"))
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With

lrecBillingItems_Err:
        'UPGRADE_NOTE: Object lrecBillingItems may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecBillingItems = Nothing
        If Err.Number Then
            Err.Raise(Err.Number)
        End If
    End Function
End Class






