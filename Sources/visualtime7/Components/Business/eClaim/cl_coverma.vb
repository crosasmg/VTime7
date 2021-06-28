Option Strict Off
Option Explicit On
Public Class cl_coverma
	'%-------------------------------------------------------%'
	'% $Workfile:: cl_coverma.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.cl_coverma al 04-25-2002 15:56:33
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nServ_Order As Double ' NUMBER     22   0    10    N
	Public nCon_earthquake As Integer ' NUMBER     22   0     5    N
	Public nDamage As Integer ' NUMBER     22   0     5    N
	Public nContainrisk As Integer ' NUMBER     22   0     5    N
	Public sRiverbed As String ' CHAR       1    0     0    N
	Public nDist_river As Double ' NUMBER     22   2     7    S
	Public sInflu_risk As String ' CHAR       1    0     0    N
	Public nInundat As Integer ' NUMBER     22   0     5    N
	Public sStratobj As String ' CHAR       1    0     0    N
	Public sTerrefy As String ' CHAR       1    0     0    N
	Public nWaterpipe As Integer ' NUMBER     22   0     5    S
	Public nDam_waterpipe As Integer ' NUMBER     22   0     5    S
	Public nSewerpipe As Integer ' NUMBER     22   0     5    S
	Public nDam_sewerpipe As Integer ' NUMBER     22   0     5    S
	Public nStatroof As Integer ' NUMBER     22   0     5    N
	Public nDamroof As Integer ' NUMBER     22   0     5    N
	Public sStorm As String ' CHAR       1    0     0    N
	Public sSnow As String ' CHAR       1    0     0    N
	Public sShockauto As String ' CHAR       1    0     0    N
	Public sFallplane As String ' CHAR       1    0     0    N
	Public sWind As String ' CHAR       1    0     0    N
	Public sAirport As String ' CHAR       1    0     0    N
	Public nDistair As Double ' NUMBER     22   2     7    S
	Public sSea As String ' CHAR       1    0     0    N
	Public nDistsea As Double ' NUMBER     22   2     7    S
	
	
	'+
	'+ Estructura de tabla TEMP_TRESERVE al 05-02-2005 15:00:47
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sKey As String ' VARCHAR2   20   0     0    S
	Public nCover As Integer ' NUMBER     22   0     5    S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public sResstat As String ' CHAR       1    0     0    S
	Public nConcept As Integer ' NUMBER     22   0     5    S
	Public nLimit As Integer ' NUMBER     22   0     12   S
	Public nExces As Integer ' NUMBER     22   0     10   S
	Public nQuantity As Integer ' NUMBER     22   0     5    S
	Public nAmount As Double ' NUMBER     22   2     14   S
	Public nDeduc As Double ' NUMBER     22   2     14   S
	Public nIndemrate As Double ' NUMBER     22   2     5    S
	Public nReserve As Integer ' NUMBER     22   0     12   S
	Public sDescob As String ' VARCHAR2   12   0     0    S
	Public nModulec As Integer ' NUMBER     22   0     5    S
	Public sDescon As String ' VARCHAR2   30   0     0    S
	Public nClcover As Integer ' NUMBER     22   0     5    S
	Public nBranch_est As Integer ' NUMBER     22   0     5    S
	Public nBranch_led As Integer ' NUMBER     22   0     5    S
	Public nBranch_rei As Integer ' NUMBER     22   0     5    S
	Public sAuto_resist As String ' CHAR       1    0     0    S
	Public nExchange As Double ' NUMBER     22   6     10   S
	Public nIndiclimcon As Integer ' NUMBER     22   0     12   S
	Public sBudget As String ' VARCHAR2   10   0     0    S
	Public dDate_bud As Date ' DATE       7    0     0    S
	Public nSub_provider As Integer ' NUMBER     22   0     5    S
	Public sCliename As String ' VARCHAR2   30   0     0    S
	
	'+
	'+ Estructura de tabla timetmp.SI007_TABLETMP al 05-09-2005 08:30:49
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nGroup As Integer ' NUMBER     22   0     5    S
	Public sDescover As String ' CHAR       120  0     0    S
	Public sReservstat As String ' CHAR       1    0     0    S
	Public nDamages As Double ' NUMBER     22   6     18   S
	Public nFra_amount As Double ' NUMBER     22   6     18   S
	Public nDamprof As Double ' NUMBER     22   6     18   S
	Public nCapital As Double ' NUMBER     22   6     18   S
	Public sFrandedi As String ' CHAR       1    0     0    S
	Public sFrancapl As String ' CHAR       1    0     0    S
	Public nLoc_pay_am As Double ' NUMBER     22   6     18   S
	Public nPay_amount As Double ' NUMBER     22   6     18   S
	Public sAutomrep As String ' CHAR       1    0     0    S
	Public nFixamount As Integer ' NUMBER     22   0     10   S
	Public nMaxamount As Integer ' NUMBER     22   0     10   S
	Public nMinamount As Integer ' NUMBER     22   0     10   S
	Public nRate As Double ' NUMBER     22   2     4    S
	Public nMedreser As Double ' NUMBER     22   6     18   S
	Public sRoureser As String ' CHAR       12   0     0    S
	Public sCacalili As String ' CHAR       1    0     0    S
	Public sCaren_type As String ' CHAR       1    0     0    S
	Public sInsurini As String ' CHAR       1    0     0    S
	Public nCaren_quan As Integer ' NUMBER     22   0     5    S
	Public sClient As String ' CHAR       14   0     0    S
	Public sBill_ind As String ' CHAR       1    0     0    S
	Public nPay_concep As Integer ' NUMBER     22   0     5    S
	Public nPrestac As Integer ' NUMBER     22   0     5    S
	Public nAmoun_used As Double ' NUMBER     22   6     18   S
	Public nDed_amount As Double ' NUMBER     22   6     18   S
	Public nDed_percen As Double ' NUMBER     22   2     4    S
	Public nDed_quanti As Integer ' NUMBER     22   0     5    S
	Public nDed_type As Integer ' NUMBER     22   0     5    S
	Public nIndem_rate As Integer ' NUMBER     22   0     5    S
	Public nPunish As Double ' NUMBER     22   2     5    S
	Public nLimit_h As Double ' NUMBER     22   6     18   S
	Public nLimit_exe As Double ' NUMBER     22   6     18   S
	Public nCount As Integer ' NUMBER     22   0     5    S
	Public nTyplim As Integer ' NUMBER     22   0     5    S
	Public nQuant_used As Double ' NUMBER     22   6     18
	
	
	
	
	
	
	'%InsUpdcl_coverma: Se encarga de actualizar la tabla cl_coverma
	Private Function InsUpdcl_coverma(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdcl_coverma As eRemoteDB.Execute
		Dim lclsinsUpdcl_coverma As cl_coverma
		
		On Error GoTo insUpdcl_coverma_Err
		
		lrecinsUpdcl_coverma = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdcl_coverma al 04-25-2002 16:04:41
		'+
		With lrecinsUpdcl_coverma
			.StoredProcedure = "insUpdcl_coverma"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCon_earthquake", nCon_earthquake, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage", nDamage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContainrisk", nContainrisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRiverbed", sRiverbed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDist_river", nDist_river, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInflu_risk", sInflu_risk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInundat", nInundat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStratobj", sStratobj, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTerrefy", sTerrefy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWaterpipe", nWaterpipe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDam_waterpipe", nDam_waterpipe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSewerpipe", nSewerpipe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDam_sewerpipe", nDam_sewerpipe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatroof", nStatroof, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamroof", nDamroof, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStorm", sStorm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSnow", sSnow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShockauto", sShockauto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFallplane", sFallplane, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWind", sWind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAirport", sAirport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDistair", nDistair, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSea", sSea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDistsea", nDistsea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdcl_coverma = .Run(False)
		End With
		
insUpdcl_coverma_Err: 
		If Err.Number Then
			InsUpdcl_coverma = False
		End If
		lrecinsUpdcl_coverma = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdcl_coverma(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdcl_coverma(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdcl_coverma(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreacl_coverma As eRemoteDB.Execute
		Dim lclscl_coverma As cl_coverma
		
		On Error GoTo reacl_coverma_Err
		
		lrecreacl_coverma = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reacl_coverma al 04-25-2002 16:02:43
		'+
		With lrecreacl_coverma
			.StoredProcedure = "reacl_coverma"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				nServ_Order = nServ_Order
				nCon_earthquake = .FieldToClass("nCon_earthquake")
				nDamage = .FieldToClass("nDamage")
				nContainrisk = .FieldToClass("nContainrisk")
				sRiverbed = .FieldToClass("sRiverbed")
				nDist_river = .FieldToClass("nDist_river")
				sInflu_risk = .FieldToClass("sInflu_risk")
				nInundat = .FieldToClass("nInundat")
				sStratobj = .FieldToClass("sStratobj")
				sTerrefy = .FieldToClass("sTerrefy")
				nWaterpipe = .FieldToClass("nWaterpipe")
				nDam_waterpipe = .FieldToClass("nDam_waterpipe")
				nSewerpipe = .FieldToClass("nSewerpipe")
				nDam_sewerpipe = .FieldToClass("nDam_sewerpipe")
				nStatroof = .FieldToClass("nStatroof")
				nDamroof = .FieldToClass("nDamroof")
				sStorm = .FieldToClass("sStorm")
				sSnow = .FieldToClass("sSnow")
				sShockauto = .FieldToClass("sShockauto")
				sFallplane = .FieldToClass("sFallplane")
				sWind = .FieldToClass("sWind")
				sAirport = .FieldToClass("sAirport")
				nDistair = .FieldToClass("nDistair")
				sSea = .FieldToClass("sSea")
				nDistsea = .FieldToClass("nDistsea")
			Else
				Find = False
			End If
		End With
		
reacl_coverma_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecreacl_coverma = Nothing
		On Error GoTo 0
		
	End Function
	'%InsPostOS592_4: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(OS592_4)
	Public Function InsPostOS592_4(ByVal sAction As String, ByVal nServ_Order As Double, ByVal nCon_earthquake As Integer, ByVal nDamage As Integer, ByVal nContainrisk As Integer, ByVal sRiverbed As String, ByVal nDist_river As Double, ByVal sInflu_risk As String, ByVal nInundat As Integer, ByVal sStratobj As String, ByVal sTerrefy As String, ByVal nWaterpipe As Integer, ByVal nDam_waterpipe As Integer, ByVal nSewerpipe As Integer, ByVal nDam_sewerpipe As Integer, ByVal nStatroof As Integer, ByVal nDamroof As Integer, ByVal sStorm As String, ByVal sSnow As String, ByVal sShockauto As String, ByVal sFallplane As String, ByVal sWind As String, ByVal sAirport As String, ByVal nDistair As Double, ByVal sSea As String, ByVal nDistsea As Double) As Boolean
		
		On Error GoTo InsPostOS592_4_Err
		
		With Me
			.nServ_Order = nServ_Order
			.nCon_earthquake = nCon_earthquake
			.nDamage = nDamage
			.nContainrisk = nContainrisk
			.sRiverbed = IIf(sRiverbed = "1", sRiverbed, "2")
			.nDist_river = nDist_river
			.sInflu_risk = IIf(sInflu_risk = "1", sInflu_risk, "2")
			.nInundat = nInundat
			.sStratobj = IIf(sStratobj = "1", sStratobj, "2")
			.sTerrefy = IIf(sTerrefy = "1", sTerrefy, "2")
			.nWaterpipe = nWaterpipe
			.nDam_waterpipe = nDam_waterpipe
			.nSewerpipe = nSewerpipe
			.nDam_sewerpipe = nDam_sewerpipe
			.nStatroof = nStatroof
			.nDamroof = nDamroof
			.sStorm = IIf(sStorm = "1", sStorm, "2")
			.sSnow = IIf(sSnow = "1", sSnow, "2")
			.sShockauto = IIf(sShockauto = "1", sShockauto, "2")
			.sFallplane = IIf(sFallplane = "1", sFallplane, "2")
			.sWind = IIf(sWind = "1", sWind, "2")
			.sAirport = IIf(sAirport = "1", sAirport, "2")
			.nDistair = nDistair
			.sSea = IIf(sSea = "1", sSea, "2")
			.nDistsea = nDistsea
		End With
		
		Select Case sAction
			Case "Add"
				InsPostOS592_4 = Add
			Case "Update"
				InsPostOS592_4 = Update
			Case "Del"
				InsPostOS592_4 = Delete
		End Select
		
InsPostOS592_4_Err: 
		If Err.Number Then
			InsPostOS592_4 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_Order = eRemoteDB.Constants.intNull
		nCon_earthquake = eRemoteDB.Constants.intNull
		nDamage = eRemoteDB.Constants.intNull
		nContainrisk = eRemoteDB.Constants.intNull
		sRiverbed = CStr(eRemoteDB.Constants.intNull)
		nDist_river = eRemoteDB.Constants.intNull
		sInflu_risk = CStr(eRemoteDB.Constants.intNull)
		nInundat = eRemoteDB.Constants.intNull
		sStratobj = CStr(eRemoteDB.Constants.intNull)
		sTerrefy = CStr(eRemoteDB.Constants.strNull)
		nWaterpipe = eRemoteDB.Constants.intNull
		nDam_waterpipe = eRemoteDB.Constants.intNull
		nSewerpipe = eRemoteDB.Constants.intNull
		nDam_sewerpipe = eRemoteDB.Constants.intNull
		nStatroof = eRemoteDB.Constants.intNull
		nDamroof = eRemoteDB.Constants.intNull
		sStorm = CStr(eRemoteDB.Constants.strNull)
		sSnow = CStr(eRemoteDB.Constants.strNull)
		sShockauto = CStr(eRemoteDB.Constants.strNull)
		sFallplane = CStr(eRemoteDB.Constants.strNull)
		sWind = CStr(eRemoteDB.Constants.strNull)
		sAirport = CStr(eRemoteDB.Constants.strNull)
		nDistair = eRemoteDB.Constants.intNull
		sSea = CStr(eRemoteDB.Constants.strNull)
		nDistsea = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Public Function insPostSI025(ByVal nClaim As Double, ByVal nTransaction As Integer, ByVal sClient As String, ByVal nLast_mov As Integer, ByVal dEffecdate As Date, ByVal sBase As String, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal nCurrency_an As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal blnCreateInfo As Integer, ByVal dPosted As Date, ByVal nExchange As Double, ByVal nDamages As Double, ByVal nAmount As Double, ByVal nPay_amount As Double, ByVal nFra_amount As Double, ByVal nFrandeda As Double, ByVal nDamprof As Double, ByVal nBranch_est As Integer, ByVal nBranch_rei As Integer, ByVal nBranch_led As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nGroup As Integer, ByVal sReservstat As String, ByVal sFrantype As String, ByVal sAutomrep As String, ByVal sShowInd As String, ByVal nPay_amountT As Double, ByVal nTot_locam As Double, ByVal nTot_locam_s As Double, ByVal nTotal As Double, ByVal nAmount_adjus As Double, ByVal dDecladate As Date, ByVal sBill_ind As String, ByVal sSession As String, ByVal nScreSI021 As Short, ByVal nPrestac As Double, ByVal nDed_amount As Double, ByVal nImport As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecinsPostSI025 As eRemoteDB.Execute
        Dim nOpt_curr As Object = New Object
        Dim nError As Object = New Object
        Dim sErrordesc As Object = New Object

        Dim nNull As Object
		nNull = 0
		If sSession = String.Empty Then
			sSession = CStr(0)
		End If
		If nOpt_curr = String.Empty Then
			nOpt_curr = 0
		End If
		
		
		
		
		On Error GoTo insPostSI025_Err
		
		lrecinsPostSI025 = New eRemoteDB.Execute
		
		'+ Definición de store procedure insPostSI025 al 07-18-2003 12:58:33
		
		With lrecinsPostSI025
			.StoredProcedure = "INSPOSTGIL300"
			.Parameters.Add("sKey", "SI025", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", 300, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nPrestac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nImport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuant_used", nDamages, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReserve", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nError", nError, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sErrordesc", sErrordesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			'        .StoredProcedure = "insSI025pkg.insPostSI025"
			'        .Parameters.Add "nClaim", nClaim, rdbParamInput, rdbDouble, 22, 0, 10, rdbParamNullable
			'        .Parameters.Add "nTransaction", nTransaction, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "sClient", sClient, rdbParamInput, rdbVarChar, 14, 0, 0, rdbParamNullable
			'        .Parameters.Add "dEffecdate", dEffecdate, rdbParamInput, rdbDBTimeStamp, 0, 0, 0, rdbParamNullable
			'        .Parameters.Add "sBase", sBase, rdbParamInput, rdbVarChar, 10, 0, 0, rdbParamNullable
			'        .Parameters.Add "nUsercode", nUsercode, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nCurrency", nCurrency, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nCurrency_an", nCurrency_an, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nCase_num", nCase_num, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nDeman_type", nDeman_type, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "dPosted", dPosted, rdbParamInput, rdbDBTimeStamp, 0, 0, 0, rdbParamNullable
			'        .Parameters.Add "nExchange", nExchange, rdbParamInput, rdbDouble, 22, 6, 11, rdbParamNullable
			'        .Parameters.Add "nDamages", nDamages, rdbParamInput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nAmount", nAmount, rdbParamInput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nPay_amount", nPay_amount, rdbParamInput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nFra_amount", nFra_amount, rdbParamInput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nFrandeda", nFrandeda, rdbParamInput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nDamprof", nDamprof, rdbParamInput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nBranch_est", nBranch_est, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nBranch_rei", nBranch_rei, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nBranch_led", nBranch_led, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nModulec", nModulec, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nCover", nCover, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nGroup", nGroup, rdbParamInput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "sReservstat", sReservstat, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			'        .Parameters.Add "sFrantype", sFrantype, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			'        .Parameters.Add "sAutomrep", sAutomrep, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			'        .Parameters.Add "sShowind", sShowInd, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			'        .Parameters.Add "nPay_amountt", nPay_amountT, rdbParamInputOutput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nTot_locam", nTot_locam, rdbParamInputOutput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nTot_locam_s", nTot_locam_s, rdbParamInputOutput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nTotal", nTotal, rdbParamInputOutput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nAmount_adjus", nAmount_adjus, rdbParamInput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "dDecladate", dDecladate, rdbParamInput, rdbDBTimeStamp, 0, 0, 0, rdbParamNullable
			'        .Parameters.Add "sBill_ind", sBill_ind, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			'        .Parameters.Add "nContent", nNull, rdbParamInputOutput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nTransact", nTransaction, rdbParamInputOutput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nOpt_curr", nOpt_curr, rdbParamInputOutput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nAmount_out", nNull, rdbParamInputOutput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "nReserve", nReserve, rdbParamInputOutput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "sSession", sSession, rdbParamInput, rdbVarChar, 8, 0, 0, rdbParamNullable
			'        .Parameters.Add "nScreSI021", nScreSI021, rdbParamInputOutput, rdbDouble, 22, 0, 1, rdbParamNullable
			'        .Parameters.Add "nPrestac", nPrestac, rdbParamInputOutput, rdbDouble, 22, 6, 18, rdbParamNullable
			'        .Parameters.Add "Nded_Amount", nDed_amount, rdbParamInputOutput, rdbDouble, 22, 0, 5, rdbParamNullable
			'        .Parameters.Add "nImport", nImport, rdbParamInputOutput, rdbDouble, 22, 6, 18, rdbParamNullable
			insPostSI025 = .Run(False)
			
			'        insPostSI025 = .Parameters("nContent").Value = 1
			'
			'        If insPostSI025 Then
			'            nScreSI021 = .Parameters("nScreSI021").Value
			'            If sShowInd = "1" Then
			'                    nPay_amountT = .Parameters("nPay_AmountT").Value
			'                    nTot_locam = .Parameters("nTot_locam").Value
			'                    nTot_locam_s = .Parameters("nTot_locam_s").Value
			'                    nTotal = .Parameters("ntotal").Value
			'                    nTransaction = .Parameters("nTransact").Value
			'                    nOpt_curr = .Parameters("nOpt_curr").Value
			'                    nAmount = .Parameters("nAmount_out").Value
			'                    nReserve = .Parameters("nReserve").Value
			'            End If
			'        End If
		End With
		
insPostSI025_Err: 
		If Err.Number Then
			insPostSI025 = False
		End If
		lrecinsPostSI025 = Nothing
		On Error GoTo 0
		
	End Function
	
	Public Function insValSI025(ByVal nPrestac As Double, ByVal nReserve As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValSI025_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ Validation of the field "Claim"
		'+Validacion del campo "Siniestro"
		
		If nPrestac = 0 Or nPrestac = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("SI025", 4006)
		End If
		If nReserve = 0 Or nReserve = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("SI025", 767028)
		End If
		
		lclsErrors = Nothing
		
insValSI025_Err:
        If Err.Number Then
            insValSI025 = ""
            insValSI025 = insValSI025 & Err.Description
        End If

        On Error GoTo 0
		
	End Function
End Class






