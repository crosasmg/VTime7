Option Strict Off
Option Explicit On
Public Class Tab_lifcov
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_lifcov.cls                           $%'
	'% $Author:: Nvaplat26                                  $%'
	'% $Date:: 21/10/03 16.39                               $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Columnas segun tabla en el sistema al 04/04/2001
	'+ El campo llave corresponde a nCovergen
	
	'+  Column_name                  Type                 Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------------- -------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- --------------------
	Public nCovergen As Integer 'smallint 2           5     0     no                                  (n/a)                               (n/a)
	Public nAgemaxi As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public nBranch_est As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public nBranch_led As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public nBranch_gen As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public nBranch_rei As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public nCacalfix As Double 'decimal  9           12    0     yes                                 (n/a)                               (n/a)
	Public sCacalfri As String 'char     1                       yes                                 no                                  yes
	Public sCapiprem As String 'char     1                       yes                                 no                                  yes
	Public sClaccidi As String 'char     12                      yes                                 no                                  yes
	Public sCldeathi As String 'char     12                      yes                                 no                                  yes
	Public sClincapi As String 'char     12                      yes                                 no                                  yes
	Public sClinvali As String 'char     12                      yes                                 no                                  yes
	Public sClsurvii As String 'char     12                      yes                                 no                                  yes
	Public sClvehaci As String 'char     12                      yes                                 no                                  yes
	Public dCompdate As Date 'datetime 8                       yes                                 (n/a)                               (n/a)
	Public sCoveruse As String 'char     1                       yes                                 no                                  yes
	Public nCurrency As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public sDescript As String 'char     30                      yes                                 no                                  yes
	Public sIduraage As String 'char     1                       yes                                 no                                  yes
	Public sIdurayear As String 'char     1                       yes                                 no                                  yes
	Public sIduropei As String 'char     1                       yes                                 no                                  yes
	Public sInsurini As String 'char     1                       yes                                 no                                  yes
	Public nCover_in As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public nPremirat As Double 'decimal  5           4     2     yes                                 (n/a)                               (n/a)
	Public sPduraage As String 'char     1                       yes                                 no                                  yes
	Public nDuratInd As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public nDuratPay As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public sRevIndex As String 'char     1                       yes                                 no                                  yes
	Public sPduropei As String 'char     1                       yes                                 no                                  yes
	Public sPduryear As String 'char     1                       yes                                 no                                  yes
	Public sPremcapi As String 'char     1                       yes                                 no                                  yes
	Public sRechapri As String 'char     1                       yes                                 no                                  yes
	Public sRenewali As String 'char     1                       yes                                 no                                  yes
	Public sRouchaca As String 'char     12                      yes                                 no                                  yes
	Public sRouchapr As String 'char     12                      yes                                 no                                  yes
	Public sRouprcal As String 'char     12                      yes                                 no                                  yes
	Public sRoureser As String 'char     12                      yes                                 no                                  yes
	Public sRousurre As String 'char     12                      yes                                 no                                  yes
	Public sShort_des As String 'char     12                      yes                                 no                                  yes
	Public sStatregt As String 'char     1                       yes                                 no                                  yes
	Public nusercode As Integer 'smallint 2           5     0     yes                                 (n/a)                               (n/a)
	Public sClillness As String 'char     12                      yes                                 no                                  yes
	Public sCondSVS As String 'char       10                      no
	Public sInforProv As String 'char       1                       yes
	Public sProvider As String 'char       13                      yes
	Public nCla_li_typ As Integer
	
	'+ Variables auxiliares
	Private sSource As String
	
	'% Find: Función que lee los datos de la tabla
	Public Function Find(ByVal nCovergen As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTab_LifCov2 As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nCovergen <> Me.nCovergen Or bFind Then
			
			lrecreaTab_LifCov2 = New eRemoteDB.Execute
			
			With lrecreaTab_LifCov2
				.StoredProcedure = "reaTab_LifCov2"
				.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nCovergen = .FieldToClass("nCovergen")
					nAgemaxi = .FieldToClass("nAgemaxi")
					nBranch_est = .FieldToClass("nBranch_est")
					nBranch_led = .FieldToClass("nBranch_led")
					nBranch_gen = .FieldToClass("nBranch_gen")
					nBranch_rei = .FieldToClass("nBranch_rei")
					nCacalfix = .FieldToClass("nCacalfix")
					sCacalfri = .FieldToClass("sCacalfri")
					sCapiprem = .FieldToClass("sCapiprem")
					sClaccidi = .FieldToClass("sClaccidi")
					sCldeathi = .FieldToClass("sCldeathi")
					sClincapi = .FieldToClass("sClincapi")
					sClinvali = .FieldToClass("sClinvali")
					sClsurvii = .FieldToClass("sClsurvii")
					sClvehaci = .FieldToClass("sClvehaci")
					sClillness = .FieldToClass("sClillness")
					sCoveruse = .FieldToClass("sCoveruse")
					nCurrency = .FieldToClass("nCurrency")
					sDescript = .FieldToClass("sDescript")
					sIduraage = .FieldToClass("sIduraage")
					sIdurayear = .FieldToClass("sIdurayear")
					sIduropei = .FieldToClass("sIduropei")
					sInsurini = .FieldToClass("sInsurini")
					nCover_in = .FieldToClass("nCover_in")
					nPremirat = .FieldToClass("nPremirat")
					sPduraage = .FieldToClass("sPduraage")
					nDuratInd = .FieldToClass("nDuratInd")
					nDuratPay = .FieldToClass("nDuratPay")
					sRevIndex = .FieldToClass("sRevIndex")
					sPduropei = .FieldToClass("sPduropei")
					sPduryear = .FieldToClass("sPduryear")
					sPremcapi = .FieldToClass("sPremcapi")
					sRechapri = .FieldToClass("sRechapri")
					sRenewali = .FieldToClass("sRenewali")
					sRouchaca = .FieldToClass("sRouchaca")
					sRouchapr = .FieldToClass("sRouchapr")
					sRouprcal = .FieldToClass("sRouprcal")
					sRoureser = .FieldToClass("sRoureser")
					sRousurre = .FieldToClass("sRousurre")
					sShort_des = .FieldToClass("sShort_des")
					sStatregt = .FieldToClass("sStatregt")
					sCondSVS = .FieldToClass("sCondSVS")
					sInforProv = .FieldToClass("sInforProv")
					sProvider = .FieldToClass("sProvider")
					nCla_li_typ = .FieldToClass("nCla_li_typ")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_LifCov2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_LifCov2 = Nothing
	End Function
	
	'% Add: Se agrega un registro en la tabla
	Public Function Add() As Boolean
		Dim lreccreTab_LifCov As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreTab_LifCov = New eRemoteDB.Execute
		
		With lreccreTab_LifCov
			.StoredProcedure = "creTab_LifCov"
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 120, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRousurre", sRousurre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInsurini", sInsurini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCoveruse", sCoveruse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_gen", nBranch_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCldeathi", sCldeathi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_in", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaccidi", sClaccidi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClincapi", sClincapi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClinvali", sClinvali, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouprcal", sRouprcal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClsurvii", sClsurvii, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClvehaci", sClvehaci, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalfix", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremirat", nPremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalfri", sCacalfri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCapiprem", sCapiprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPremcapi", sPremcapi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouchapr", sRouchapr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouchaca", sRouchaca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRechapri", sRechapri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRenewali", sRenewali, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRevIndex", sRevIndex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgemaxi", nAgemaxi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuratInd", nDuratInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuratPay", nDuratPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIduropei", sIduropei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPduropei", sPduropei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIduraage", sIduraage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIdurayear", sIdurayear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPduraage", sPduraage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPduryear", sPduryear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClillness", sClillness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondSVS", sCondSVS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInforProv", sInforProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProvider", sProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreTab_LifCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_LifCov = Nothing
		On Error GoTo 0
	End Function
	
	'% ClientExist_death: Indica la existencia o no del cliente muerto
	Public ReadOnly Property ClientExist_death(ByVal sClient As String) As Boolean
		Get
			Dim lobjTab_lifcov As eRemoteDB.Execute
			
			On Error GoTo ClientExist_death_err
			
			lobjTab_lifcov = New eRemoteDB.Execute
			
			With lobjTab_lifcov
				.StoredProcedure = "reaClient_death"
				.Parameters.Add("sClient", sProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				ClientExist_death = .Run
			End With
			
ClientExist_death_err: 
			If Err.Number Then
				ClientExist_death = False
			End If
			On Error GoTo 0
			'UPGRADE_NOTE: Object lobjTab_lifcov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjTab_lifcov = Nothing
		End Get
	End Property
	
	'% Update: Se actualizan los campos de la tabla
	Public Function Update() As Boolean
		Dim lrecupdTab_LifCov As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdTab_LifCov = New eRemoteDB.Execute
		
		With lrecupdTab_LifCov
			.StoredProcedure = "InsUpdTab_LifCov"
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSource", sSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 120, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRousurre", sRousurre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInsurini", sInsurini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCoveruse", sCoveruse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_gen", nBranch_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCldeathi", sCldeathi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_in", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaccidi", sClaccidi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClincapi", sClincapi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClinvali", sClinvali, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouprcal", sRouprcal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClsurvii", sClsurvii, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClvehaci", sClvehaci, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClillness", sClillness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalfix", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremirat", nPremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalfri", sCacalfri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCapiprem", sCapiprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPremcapi", sPremcapi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouchapr", sRouchapr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouchaca", sRouchaca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRechapri", sRechapri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRenewali", sRenewali, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRevIndex", sRevIndex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgemaxi", nAgemaxi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuratInd", nDuratInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuratPay", nDuratPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIduropei", sIduropei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPduropei", sPduropei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIdurayear", sIdurayear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPduryear", sPduryear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIduraage", sIduraage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPduraage", sPduraage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondSVS", sCondSVS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInforProv", sInforProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProvider", sProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCla_li_typ", nCla_li_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdTab_LifCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_LifCov = Nothing
	End Function
	
	'%Delete
	Public Function Delete(ByVal nCover As Integer) As Boolean
		Dim lrecdelTab_LifCov As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecdelTab_LifCov = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.delTab_LifCov'
		'Información leída el 03/07/2001 3:42:20 PM
		
		With lrecdelTab_LifCov
			.StoredProcedure = "delTab_LifCov"
			.Parameters.Add("nCovergen", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelTab_LifCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_LifCov = Nothing
		On Error GoTo 0
	End Function
	'% FindCountLifCov: Busca la cantidad de productos de una cobertura diferente a la que se esta tratando
	Public Function FindCountLifCov(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCovernoShow As Integer) As Boolean
		Dim lrectabLife_Cover As eRemoteDB.Execute
		
		On Error GoTo FindCountLifCov_Err
		lrectabLife_Cover = New eRemoteDB.Execute
		FindCountLifCov = False
		
		'Definición de parámetros para stored procedure 'insudb.tabLife_Cover'
		'Información leída el 14/06/2001 01:18:37 p.m.
		With lrectabLife_Cover
			.StoredProcedure = "tabLife_Coverpkg.tabLife_Cover"
			.Parameters.Add("sShowNum", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sCondition", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovernoShow", nCovernoShow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCoverMax", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindCountLifCov = True
				.RCloseRec()
			End If
		End With
		
FindCountLifCov_Err: 
		If Err.Number Then
			FindCountLifCov = False
		End If
		'UPGRADE_NOTE: Object lrectabLife_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectabLife_Cover = Nothing
		On Error GoTo 0
	End Function
	
	'%UpdateStatus
	Public Function UpdateStatus(ByVal nCover As Integer, ByVal sStatregt As String) As Boolean
		Dim lrecupdTab_LifCov_Statregt As eRemoteDB.Execute
		
		On Error GoTo UpdateStatus_Err
		lrecupdTab_LifCov_Statregt = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updTab_LifCov_Statregt'
		'Información leída el 06/07/2001 2:03:55 PM
		With lrecupdTab_LifCov_Statregt
			.StoredProcedure = "updTab_LifCov_Statregt"
			.Parameters.Add("nCovergen", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateStatus = .Run(False)
		End With
		
UpdateStatus_Err: 
		If Err.Number Then
			UpdateStatus = False
		End If
		'UPGRADE_NOTE: Object lrecupdTab_LifCov_Statregt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_LifCov_Statregt = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValDP018_K: se realizan las validaciones de la página
	Public Function InsValDP018G_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nCover As Integer, Optional ByVal bDupCover As Boolean = False) As String
		Dim lclsErrors As eFunctions.Errors
        Dim lclsLife_cover As Life_cover
        Dim lclsTab_lifcov As eProduct.Tab_lifcov
		Dim lblnFound As Boolean
		
		On Error GoTo InsValDP018G_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nCover = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11163)
			Else
				lblnFound = Me.Find(nCover, True)
				Select Case nAction
					Case eFunctions.Menues.TypeActions.clngActionadd
						If lblnFound Then
							.ErrorMessage(sCodispl, 11104)
						End If
						
					Case eFunctions.Menues.TypeActions.clngActionUpdate, eFunctions.Menues.TypeActions.clngActionQuery
						If Not lblnFound Then
							.ErrorMessage(sCodispl, 11007)
						End If
						
					Case eFunctions.Menues.TypeActions.clngActionDuplicate
						If bDupCover Then
							If lblnFound Then
								.ErrorMessage(sCodispl, 11104)
							End If
						Else
							If Not lblnFound Then
								.ErrorMessage(sCodispl, 11007)
							End If
						End If
						
					Case eFunctions.Menues.TypeActions.clngActioncut
						If Not lblnFound Then
							.ErrorMessage(sCodispl, 11007)
						Else
							lclsLife_cover = New Life_cover
							If lclsLife_cover.CoverInProduct(nCover) Then
								.ErrorMessage(sCodispl, 11410)
                            End If
                            If nCover <> eRemoteDB.Constants.intNull Then
                                lclsTab_lifcov = New eProduct.Tab_lifcov
                                If lclsTab_lifcov.Count_CoverGen_ContratoII(nCover) > 0 Then
                                    .ErrorMessage(sCodispl, 11447)
                                End If
                            End If
						End If
				End Select
			End If
			InsValDP018G_K = .Confirm
		End With
		
InsValDP018G_K_Err: 
		If Err.Number Then
			InsValDP018G_K = "InsValDP018G_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_cover = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% InsPostDP018G_K: se actualizan los datos de la ventana
	Public Function InsPostDP018G_K(ByVal nAction As Integer, ByVal nCover As Integer, ByVal nusercode As Integer, Optional ByVal nNewCover As Integer = 0) As Boolean
		On Error GoTo InsPostDP018_K_Err
		
		InsPostDP018G_K = True
		With Me
			If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				'+ Se asignan los valores por defecto
				.nCovergen = nCover
				.sCoveruse = "2"
				.nCurrency = 1
				.sInforProv = "2"
				.sCacalfri = "1"
				.sRechapri = "2"
				.sRenewali = "2"
				.sRevIndex = "2"
				.sIduropei = "1"
				.sIdurayear = "2"
				.sIduraage = "2"
				.sPduropei = "1"
				.sPduryear = "2"
				.sPduraage = "2"
				.sInsurini = "1"
				.nusercode = nusercode
				InsPostDP018G_K = .Add
			ElseIf nAction = eFunctions.Menues.TypeActions.clngActionDuplicate And nNewCover > 0 Then 
				If .Find(nCover, True) Then
					.nCovergen = nNewCover
					.nusercode = nusercode
					InsPostDP018G_K = .Add
				End If
			End If
		End With
		
InsPostDP018_K_Err: 
		If Err.Number Then
			InsPostDP018G_K = False
		End If
		On Error GoTo 0
	End Function
	
	'% insValDP018G: se realizan las validaciones de la página
    Public Function insValDP018G(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nCurrency As Integer, ByVal sInsurini As String, ByVal sStatregt As String, ByVal nBranch_est As Integer, ByVal nBranch_gen As Integer, ByVal nBranch_led As Integer, ByVal nBranch_rei As Integer, ByVal sCondSVS As String, ByVal sProvider As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsTab_lifcov As eProduct.Tab_lifcov

        On Error GoTo InsValDP018G_Err

        lclsErrors = New eFunctions.Errors
        If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
            With lclsErrors
                '+ Se valida que el campo descripción de la cobertura este lleno
                If sDescript = String.Empty Then
                    .ErrorMessage(sCodispl, 10010)
                End If

                '+ Se valida el campo Descripcion Corta
                If sShort_des = String.Empty Then
                    .ErrorMessage(sCodispl, 10011)
                End If

                '+ Se valida que el campo moneda
                If nCurrency = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 1351)
                End If

                '+ Se valida que el campo Estado
                If sStatregt = "0" Then
                    .ErrorMessage(sCodispl, 10826)
                End If

                '+ Se valida que el tipo de cobertura
                If sInsurini = String.Empty Then
                    .ErrorMessage(sCodispl, 11427)
                End If

                '+ Se valida que el ramo contable
                If nBranch_led = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11309)
                End If

                '+ Se valida que el ramo de reaseguro
                If nBranch_rei = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 13235)
                End If

                '+ Se valida que el ramo de estadística
                If nBranch_est = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11320)
                End If

                '+ Se valida el ramo generico
                If nBranch_gen = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11310)
                End If

                '+ Se valida que el Condicionado esté lleno
                If sCondSVS = String.Empty Then
                    .ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, "Condicionado:")
                End If

                '+ se valida si existe el cliente muerto
                If sProvider <> String.Empty Then
                    lclsTab_lifcov = New eProduct.Tab_lifcov
                    lclsTab_lifcov.sProvider = sProvider
                    If lclsTab_lifcov.ClientExist_death(sProvider) Then
                        .ErrorMessage(sCodispl, 2051)
                    End If
                End If

                insValDP018G = .Confirm
            End With
        End If

InsValDP018G_Err:
        If Err.Number Then
            insValDP018G = "InsValDP018G: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsTab_lifcov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_lifcov = Nothing
        On Error GoTo 0
    End Function
	
	'% InsPostDP018G: se actualizan los campos en la tabla
	Public Function InsPostDP018G(ByVal nAction As Integer, ByVal nCover As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nCurrency As Integer, ByVal sRousurre As String, ByVal sRoureser As String, ByVal sInsurini As String, ByVal sCoveruse As String, ByVal nBranch_est As Integer, ByVal nBranch_gen As Integer, ByVal nBranch_led As Integer, ByVal nBranch_rei As Integer, ByVal sCondSVS As String, ByVal sInforProv As String, ByVal sProvider As String, ByVal nusercode As Integer, ByVal nCla_li_typ As Integer) As Boolean
		On Error GoTo InsPostDP018G_Err
		InsPostDP018G = True
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			sSource = "1"
			With Me
				.nCovergen = nCover
				.sDescript = sDescript
				.sShort_des = sShort_des
				.nCurrency = nCurrency
				.sRousurre = sRousurre
				.sRoureser = sRoureser
				.sInsurini = sInsurini
				.sCoveruse = sCoveruse
				.nBranch_est = nBranch_est
				.nBranch_gen = nBranch_gen
				.nBranch_led = nBranch_led
				.nBranch_rei = nBranch_rei
				.sCondSVS = sCondSVS
				.sInforProv = IIf(Trim(sInforProv) = "1", "1", "2")
				.sProvider = sProvider
				.nCla_li_typ = nCla_li_typ
				InsPostDP018G = .Update
			End With
		End If
		
InsPostDP018G_Err: 
		If Err.Number Then
			InsPostDP018G = False
		End If
		On Error GoTo 0
	End Function
	
	'% InsValDP019G: Se realizan las validaciones de la página
	Public Function InsValDP019G(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sRutin As String, ByVal nRate As Double, ByVal nCoverIn As Integer, ByVal sOptCapital As String, ByVal nPrice As Double, ByVal sDeath As String, ByVal sTriIndem As String, ByVal sInability As String, ByVal sDoubleIndem As String, ByVal sSurvival As String, ByVal sInvalid As String, ByVal sClillness As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValDP019G_Err
		
		lclsErrors = New eFunctions.Errors
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			With lclsErrors
				'+ Se valida el bloque Prima
				If sRutin = String.Empty And nRate = eRemoteDB.Constants.intNull And nCoverIn = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11172)
				End If
				
				'+ Se valida el campo Importe
				If sOptCapital = "2" And nPrice = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11321)
				End If
				
				'+ Se valida que el bloque de siniestros
				If sDeath = String.Empty And sTriIndem = String.Empty And sInability = String.Empty And sDoubleIndem = String.Empty And sSurvival = String.Empty And sInvalid = String.Empty And sClillness = String.Empty Then
					.ErrorMessage(sCodispl, 10304)
				End If
				
				InsValDP019G = .Confirm
			End With
		End If
		
InsValDP019G_Err: 
		If Err.Number Then
			InsValDP019G = "InsValDP019G: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% InsValDP050G: Se realizan las validaciones de la página
	Public Function InsValDP050G(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sOptSecure As String, ByVal nQuantity As Integer, ByVal sOptPay As String, ByVal nQuantityPays As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsMessage As eFunctions.Values
		
		On Error GoTo InsValDP050G_Err
		
		lclsErrors = New eFunctions.Errors
		lclsMessage = New eFunctions.Values
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			With lclsErrors
				'+ Se valida que el campo Seguro-Cantidad esté lleno
				If nQuantity = eRemoteDB.Constants.intNull And sOptSecure <> "1" Then
                    .ErrorMessage(sCodispl, 11411,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(259))
                End If
				
				'+ Se valida el campo Pagos-Cantidad
				If nQuantityPays = eRemoteDB.Constants.intNull And sOptPay <> "1" Then
                    .ErrorMessage(sCodispl, 11411,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(260))
                End If
				
				InsValDP050G = .Confirm
			End With
		End If
		
InsValDP050G_Err: 
		If Err.Number Then
			InsValDP050G = "InsValDP050G: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsMessage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMessage = Nothing
	End Function
	
	'% InsPostDP019G: se realizan las actualizaciones de la página
	Public Function InsPostDP019G(ByVal nAction As Integer, ByVal nCover As Integer, ByVal sCldeathi As String, ByVal nCover_in As Integer, ByVal sClaccidi As String, ByVal sClincapi As String, ByVal sClinvali As String, ByVal sRouprcal As String, ByVal sClsurvii As String, ByVal sClvehaci As String, ByVal nCacalfix As Double, ByVal nPremirat As Double, ByVal sCacalfri As String, ByVal sCapiprem As String, ByVal sPremcapi As String, ByVal sClillness As String, ByVal nusercode As Integer) As Boolean
		On Error GoTo InsPostDP019G_Err
		InsPostDP019G = True
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			sSource = "2"
			With Me
				.nCovergen = nCover
				.sCldeathi = sCldeathi
				.nCover_in = nCover_in
				.sClaccidi = sClaccidi
				.sClincapi = sClincapi
				.sClinvali = sClinvali
				.sRouprcal = sRouprcal
				.sClsurvii = sClsurvii
				.sClvehaci = sClvehaci
				.nCacalfix = nCacalfix
				.nPremirat = nPremirat
				.sCacalfri = sCacalfri
				.sCapiprem = sCapiprem
				.sPremcapi = sPremcapi
				.sClillness = sClillness
				.nusercode = nusercode
				InsPostDP019G = .Update
			End With
		End If
		
InsPostDP019G_Err: 
		If Err.Number Then
			InsPostDP019G = False
		End If
		On Error GoTo 0
	End Function

    '% DefaultValueDP019G: Se manejan los valores por defecto para algunos campos de la ventana
    Public Function DefaultValueDP019G(ByVal sField As String) As Object
        Dim strResultado As Object = New Object
        Try
            Select Case sField
                Case "CapitalFix"
                    strResultado = IIf(sCacalfri = "2", "1", "2")
                Case "Amount_disabled"
                    strResultado = IIf(sCacalfri = "2", False, True)
                Case "Cover"
                    strResultado = IIf(sPremcapi = "1", True, False)
                Case "Rate"
                    strResultado = IIf(sPremcapi = "1", False, True)
            End Select
            Return strResultado
        Catch ex As Exception
            Return strResultado
        End Try
    End Function

    '% InsPostDP050G: Se actualzan los datos de la página
    Public Function InsPostDP050G(ByVal nAction As Integer, ByVal nCover As Integer, ByVal sRouchapr As String, ByVal sRouchaca As String, ByVal sRechapri As String, ByVal sRenewali As String, ByVal sRevIndex As String, ByVal nAgemaxi As Integer, ByVal nDuratInd As Integer, ByVal sOptSecure As String, ByVal nDuratPay As Integer, ByVal sOptPay As String, ByVal nusercode As Integer) As Boolean
		On Error GoTo InsPostDP050G_Err
		InsPostDP050G = True
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			sSource = "3"
			With Me
				.nCovergen = nCover
				.sRouchapr = sRouchapr
				.sRouchaca = sRouchaca
				.sRechapri = IIf(sRechapri = String.Empty, "2", sRechapri)
				.sRenewali = IIf(sRenewali = String.Empty, "2", sRenewali)
				.sRevIndex = IIf(sRevIndex = String.Empty, "2", sRevIndex)
				.sIduropei = IIf(sOptSecure = "1", "1", "2")
				.sIdurayear = IIf(sOptSecure = "2", "1", "2")
				.sIduraage = IIf(sOptSecure = "3", "1", "2")
				.sPduropei = IIf(sOptPay = "1", "1", "2")
				.sPduryear = IIf(sOptPay = "2", "1", "2")
				.sPduraage = IIf(sOptPay = "3", "1", "2")
				.nAgemaxi = nAgemaxi
				.nDuratInd = nDuratInd
				.nDuratPay = nDuratPay
				.nusercode = nusercode
				InsPostDP050G = .Update
			End With
		End If
		
InsPostDP050G_Err: 
		If Err.Number Then
			InsPostDP050G = False
		End If
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla la creación de la instancia del objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nCovergen = eRemoteDB.Constants.intNull
		nAgemaxi = eRemoteDB.Constants.intNull
		nBranch_est = eRemoteDB.Constants.intNull
		nBranch_led = eRemoteDB.Constants.intNull
		nBranch_gen = eRemoteDB.Constants.intNull
		nBranch_rei = eRemoteDB.Constants.intNull
		nCacalfix = eRemoteDB.Constants.intNull
		sCacalfri = String.Empty
		sCapiprem = String.Empty
		sClaccidi = String.Empty
		sCldeathi = String.Empty
		sClincapi = String.Empty
		sClinvali = String.Empty
		sClsurvii = String.Empty
		sClvehaci = String.Empty
		dCompdate = eRemoteDB.Constants.dtmNull
		sCoveruse = String.Empty
		nCurrency = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		sIduraage = String.Empty
		sIdurayear = String.Empty
		sIduropei = String.Empty
		sInsurini = String.Empty
		nCover_in = eRemoteDB.Constants.intNull
		nPremirat = eRemoteDB.Constants.intNull
		sPduraage = String.Empty
		nDuratInd = eRemoteDB.Constants.intNull
		nDuratPay = eRemoteDB.Constants.intNull
		sRevIndex = String.Empty
		sPduropei = String.Empty
		sPduryear = String.Empty
		sPremcapi = String.Empty
		sRechapri = String.Empty
		sRenewali = String.Empty
		sRouchaca = String.Empty
		sRouchapr = String.Empty
		sRouprcal = String.Empty
		sRoureser = String.Empty
		sRousurre = String.Empty
		sShort_des = String.Empty
		sStatregt = String.Empty
		nusercode = eRemoteDB.Constants.intNull
		sClillness = String.Empty
    End Sub
    '% ClientExist_death: Indica la existencia o no del cliente muerto
    Public Function Count_CoverGen_ContratoII(ByVal nCovergen As Integer) As Integer
        Dim lobjTab_lifcov As eRemoteDB.Execute

        On Error GoTo Count_CoverGen_ContratoII_err

        lobjTab_lifcov = New eRemoteDB.Execute

        With lobjTab_lifcov
            .StoredProcedure = "REACONTR_RATE_II_BY_COVERGEN"
            .Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            Count_CoverGen_ContratoII = .Parameters("nExists").Value
        End With

Count_CoverGen_ContratoII_err:
        If Err.Number Then
            Count_CoverGen_ContratoII = 0
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjTab_lifcov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjTab_lifcov = Nothing
    End Function

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub
End Class






