Option Strict Off
Option Explicit On
Public Class TCovers
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: TCovers.cls                              $%'
	'% $Author:: Jsarabia                                   $%'
	'% $Date:: 7-08-09 12:23                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'- Tipo de ramo: 1)Vida 2)No vida
	Public Enum eBranchTypes
		clngLife = 1
		clngNoLife = 2
	End Enum
	
	Public objtCover As TCover
	
	Private mCol As Collection
	
	Public mcolTCovers As TCovers
	
	'- Variables auxiliares
	Public nCoverWindow As Integer
	Public nLegAmount As Double
	
	'- Variables para manejo de errores.
	Public nError As Integer
	Public bError As Boolean
	
	'-Variable que indica si existe información en la tabla de tratamiento de coberturas
	'-(COVER, COVER_CO_P, COVER_CO_G)
	Public bDataFound As Boolean
	
	'% Add: Añade una nueva instancia de la clase TCover a la colección
	Public Function Add(ByRef objClass As TCover) As TCover
		If objClass Is Nothing Then
			objClass = New TCover
		End If
		
		With objClass
			mCol.Add(objClass)
		End With
		
		'retorna el elemento creado
		Add = objClass
	End Function
	
	'% CalProductCover: Llama al procedimiento que realiza el cálculo de coberturas
    Public Function CalProductCover(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal sProcess_type As String, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal sKey As String, ByVal nCurrency As Integer, ByVal dNulldate As Date, ByVal nBranchType As eBranchTypes, ByVal nRole As Integer, ByVal sClient As String, ByVal sDelTCover As String, ByVal bUpdCover As Boolean, ByVal nType_amend As Integer) As Boolean
        Dim lrecAux As eRemoteDB.Execute
        Dim lclsTCover As TCover

        On Error GoTo CalProductCover_err
        bUpdCover = True
        lrecAux = New eRemoteDB.Execute

        With lrecAux
            .StoredProcedure = "InsCalCover_Proccesspkg.InsCalCover_Proccess"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDeltcover", sDelTCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcess_type", sProcess_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpdcover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInd_charge", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_Mas", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                If sProcess_type = "1" Then
                    Do While Not .EOF
                        lclsTCover = New TCover
                        lclsTCover.sCertype = .FieldToClass("sCertype")
                        lclsTCover.sChange = .FieldToClass("sChange")
                        lclsTCover.sFrandedi = .FieldToClass("sFrandedi")
                        lclsTCover.sWait_type = .FieldToClass("sWait_Type")
                        lclsTCover.sFrancApl = .FieldToClass("sFrancApl")
                        lclsTCover.sFree_premi = .FieldToClass("sFree_premi")
                        lclsTCover.sDescript = .FieldToClass("sDescript")
                        lclsTCover.sExist = .FieldToClass("sExist")
                        lclsTCover.sRequired = .FieldToClass("sRequire")
                        lclsTCover.sDefaulti = .FieldToClass("sDefaulti")
                        If Not bDataFound Then
                            If lclsTCover.sExist = "1" Then
                                bDataFound = True
                            End If
                        End If
                        lclsTCover.sCacalili = .FieldToClass("sCacalili")
                        lclsTCover.sCh_typ_cap = .FieldToClass("sCh_typ_cap")
                        lclsTCover.sChange_typ = .FieldToClass("sChange_typ")
                        lclsTCover.sFdrequire = .FieldToClass("sFDRequire")
                        lclsTCover.sRoupremi = .FieldToClass("sRouPremi")
                        lclsTCover.dEffecdate = .FieldToClass("dEffecdate")
                        lclsTCover.nCapital = .FieldToClass("nCapital")
                        lclsTCover.nDiscount = .FieldToClass("nDiscount")
                        lclsTCover.nFixamount = .FieldToClass("nFixAmount")
                        lclsTCover.nMaxamount = .FieldToClass("nMaxAmount")
                        lclsTCover.nRate = .FieldToClass("nRate")
                        lclsTCover.nMinamount = .FieldToClass("nMinAmount")
                        lclsTCover.nPremium = .FieldToClass("nPremium")
                        lclsTCover.nRatecove = .FieldToClass("nRateCove")
                        lclsTCover.nCapitali = .FieldToClass("nCapitali")
                        lclsTCover.nRatecapadd = .FieldToClass("nRateCapAdd")
                        lclsTCover.nRatecapsub = .FieldToClass("nRateCapSub")
                        lclsTCover.nRatepreadd = .FieldToClass("nRatePreAdd")
                        lclsTCover.nRatepresub = .FieldToClass("nRatePreSub")
                        lclsTCover.nDisc_Amoun = .FieldToClass("nDisc_amoun")
                        lclsTCover.npremirat = .FieldToClass("nPremiRat")
                        lclsTCover.nPremimin = .FieldToClass("nPremimin")
                        lclsTCover.nPremimax = .FieldToClass("nPremiMax")
                        lclsTCover.nPolicy = .FieldToClass("nPolicy")
                        lclsTCover.nCertif = .FieldToClass("nCertif")
                        lclsTCover.nBranch = .FieldToClass("nBranch")
                        lclsTCover.nProduct = .FieldToClass("nProduct")
                        lclsTCover.nGroup = .FieldToClass("nGroup")
                        lclsTCover.nModulec = .FieldToClass("nModulec")
                        lclsTCover.nCover = .FieldToClass("nCover")
                        lclsTCover.nCurrency = .FieldToClass("nCurrency")
                        lclsTCover.nWait_quan = .FieldToClass("nWait_quan")
                        lclsTCover.nGroup_insu = .FieldToClass("nGroup_insu")
                        lclsTCover.nCover_in = .FieldToClass("nCover_in")
                        lclsTCover.nCoverapl = .FieldToClass("nCoverApl")
                        lclsTCover.sKey = .FieldToClass("sKey")
                        lclsTCover.nPremifix = .FieldToClass("nPremifix")
                        lclsTCover.sCacalfri = .FieldToClass("sCacalfri")
                        lclsTCover.nChcaplev = .FieldToClass("nChCapLev")
                        lclsTCover.nChprelev = .FieldToClass("nChPreLev")
                        lclsTCover.nFduserlev = .FieldToClass("nFDUserLev")
                        lclsTCover.sFdchantyp = .FieldToClass("sFDChantyp")
                        lclsTCover.nFdrateadd = .FieldToClass("nFDRateAdd")
                        lclsTCover.nFdratesub = .FieldToClass("nFDRateSub")
                        lclsTCover.nCacalcov = .FieldToClass("nCacalcov")
                        lclsTCover.nCacalper = .FieldToClass("nCacalper")
                        lclsTCover.sPfrandedi = .FieldToClass("spFrandedi")
                        lclsTCover.nCacalmax = .FieldToClass("nCacalmax")
                        lclsTCover.nCacalmin = .FieldToClass("nCacalmin")
                        lclsTCover.sAddsuini = .FieldToClass("sAddsuini")
                        lclsTCover.nTarifcurr = .FieldToClass("nTarifCurr")
                        lclsTCover.sRouchaca = .FieldToClass("sRouchaca")
                        lclsTCover.nCacalfix = .FieldToClass("nCacalfix")
                        lclsTCover.nCapital_wait = .FieldToClass("nCapital_wait")
                        lclsTCover.nTyp_AgeMinM = .FieldToClass("nTyp_AgeMinM")
                        lclsTCover.nTyp_AgeMinF = .FieldToClass("nTyp_AgeMinF")
                        lclsTCover.nAgeminins = .FieldToClass("nAgeminins")
                        lclsTCover.nAgemaxins = .FieldToClass("nAgemaxins")
                        lclsTCover.nAgemaxper = .FieldToClass("nAgemaxper")
                        lclsTCover.nTypDurins = .FieldToClass("nTypdurins")
                        lclsTCover.nDurinsur = .FieldToClass("nDurinsur")
                        lclsTCover.nTypDurpay = .FieldToClass("nTypdurpay")
                        lclsTCover.nDurpay = .FieldToClass("nDurpay")
                        lclsTCover.nRole = .FieldToClass("nRole")
                        lclsTCover.sClient = .FieldToClass("sClient")
                        lclsTCover.dAniversary = .FieldToClass("dAniversary")
                        lclsTCover.dSeektar = .FieldToClass("dSeektar")
                        lclsTCover.nRetarif = .FieldToClass("nRetarif")
                        lclsTCover.nAgemininsf = .FieldToClass("nAgemininsf")
                        lclsTCover.nAgemaxinsf = .FieldToClass("nAgemaxinsf")
                        lclsTCover.nAgemaxperf = .FieldToClass("nAgemaxperf")
                        lclsTCover.sRequirec = .FieldToClass("sRequirec")
                        lclsTCover.sDefaultic = .FieldToClass("sDefaultic")
                        lclsTCover.nBranch_rei = .FieldToClass("nBranch_rei")
                        lclsTCover.nApply_Perc = .FieldToClass("nApply_Perc")
                        lclsTCover.dFer = .FieldToClass("dFer")
                        lclsTCover.nCauseupd = .FieldToClass("nCauseupd")
                        lclsTCover.sBas_sumins = .FieldToClass("sBas_sumins")
                        lclsTCover.nCapital_o = .FieldToClass("nCapital_o")
                        lclsTCover.nPremium_o = .FieldToClass("nPremium_o")
                        lclsTCover.nRateCove_o = .FieldToClass("nRatecove_o")
                        lclsTCover.nRolcap = .FieldToClass("nRolcap")

                        '-Variable nuevas se agregaron a la tabla TCOVER
                        lclsTCover.sRoucapit = .FieldToClass("sRoucapit")
                        lclsTCover.nCamaxcov = .FieldToClass("nCamaxcov")
                        lclsTCover.nCamaxper = .FieldToClass("nCamaxper")
                        lclsTCover.nCamaxrol = .FieldToClass("nCamaxrol")
                        lclsTCover.nCacalmul = .FieldToClass("nCacalmul")
                        lclsTCover.nGenCurrency = .FieldToClass("nGenCurrency")

                        lclsTCover.sdesc_t5559 = .FieldToClass("sdesc_t5559")
                        lclsTCover.sdesc_t64 = .FieldToClass("sdesc_t64")
                        lclsTCover.sdesc_t33 = .FieldToClass("sdesc_t33")
                        lclsTCover.sdesc_t5589 = .FieldToClass("sdesc_t5589")
                        lclsTCover.sdesc_t_pay = .FieldToClass("sdesc_t_pay")
                        lclsTCover.sdesc_t52 = .FieldToClass("sdesc_t52")
                        lclsTCover.sdesc_t5547 = .FieldToClass("sdesc_t5547")
                        lclsTCover.sdesc_t5000 = .FieldToClass("sdesc_t5000")

                        '+ Variable agregada para el manejo del capital solicitado por el asegurado
                        lclsTCover.nCapital_req = .FieldToClass("nCapital_req")

                        lclsTCover.nRateCla = .FieldToClass("nRateCla")
                        lclsTCover.nFixAmoCla = .FieldToClass("nFixAmoCla")
                        lclsTCover.nMinAmoCla = .FieldToClass("nMinAmoCla")
                        lclsTCover.nMaxAmoCla = .FieldToClass("nMaxAmoCla")
                        lclsTCover.nDiscCla = .FieldToClass("nDiscCla")
                        lclsTCover.nDisc_AmoCla = .FieldToClass("nDisc_AmoCla")

                        lclsTCover.nFrancDays = .FieldToClass("nFrancDays")

                        Call Add(lclsTCover)
                        'UPGRADE_NOTE: Object lclsTCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsTCover = Nothing
                        .RNext()
                    Loop
                    .RCloseRec()
                Else
                    bUpdCover = .Parameters("nUpdcover").Value = 1
                End If
                CalProductCover = True
            End If
        End With

CalProductCover_err:
        If Err.Number Then
            CalProductCover = False
        End If
        'UPGRADE_NOTE: Object lrecAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecAux = Nothing
        On Error GoTo 0
    End Function

    '%Find: Obtiene los datos de la tabla TCover
    Public Function Find(ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, Optional ByVal nRole As Integer = eRemoteDB.Constants.intNull, Optional ByVal sClient As String = "", Optional ByVal bAll As Boolean = False, Optional ByVal bIsLife As Boolean = False) As Boolean
        Dim lrecreatcover As eRemoteDB.Execute
        Dim lclsTCover As TCover

        On Error GoTo Find_Err
        lrecreatcover = New eRemoteDB.Execute
        mCol = New Collection
        With lrecreatcover
            .StoredProcedure = "reatCover"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", IIf(bIsLife, String.Empty, sClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                Do While Not .EOF
                    lclsTCover = New TCover
                    lclsTCover.sCertype = .FieldToClass("sCertype")
                    lclsTCover.sChange = .FieldToClass("sChange")
                    lclsTCover.sFrandedi = .FieldToClass("sFrandedi")
                    lclsTCover.sWait_type = .FieldToClass("sWait_Type")
                    lclsTCover.sFrancApl = .FieldToClass("sFrancApl")
                    lclsTCover.sFree_premi = .FieldToClass("sFree_premi")
                    lclsTCover.sDescript = .FieldToClass("sDescript")
                    lclsTCover.sExist = .FieldToClass("sExist")
                    lclsTCover.nRole = .FieldToClass("nRole")
                    lclsTCover.sClient = .FieldToClass("sClient")
                    If Not bDataFound Then
                        If Not bIsLife Or lclsTCover.sClient = sClient Then
                            If lclsTCover.sExist = "1" Then
                                bDataFound = True
                            End If
                        End If
                    End If
                    lclsTCover.sRequired = .FieldToClass("sRequire")
                    lclsTCover.sDefaulti = .FieldToClass("sDefaulti")
                    lclsTCover.sCacalili = .FieldToClass("sCacalili")
                    lclsTCover.sCh_typ_cap = .FieldToClass("sCh_typ_cap")
                    lclsTCover.sChange_typ = .FieldToClass("sChange_typ")
                    lclsTCover.sFdrequire = .FieldToClass("sFDRequire")
                    lclsTCover.sRoupremi = .FieldToClass("sRouPremi")
                    lclsTCover.dEffecdate = .FieldToClass("dEffecdate")
                    lclsTCover.nCapital = .FieldToClass("nCapital")
                    lclsTCover.nDiscount = .FieldToClass("nDiscount")
                    lclsTCover.nFixamount = .FieldToClass("nFixamount")
                    lclsTCover.nMaxamount = .FieldToClass("nMaxamount")
                    lclsTCover.nRate = .FieldToClass("nRate")
                    lclsTCover.nMinamount = .FieldToClass("nMinamount")
                    lclsTCover.nPremium = .FieldToClass("nPremium")
                    lclsTCover.nRatecove = .FieldToClass("nRateCove")
                    lclsTCover.nCapitali = .FieldToClass("nCapitali")
                    lclsTCover.nRatecapadd = .FieldToClass("nRateCapAdd")
                    lclsTCover.nRatecapsub = .FieldToClass("nRateCapSub")
                    lclsTCover.nRatepreadd = .FieldToClass("nRatePreAdd")
                    lclsTCover.nRatepresub = .FieldToClass("nRatePreSub")
                    lclsTCover.nDisc_Amoun = .FieldToClass("nDisc_amoun")
                    lclsTCover.npremirat = .FieldToClass("nPremiRat")
                    lclsTCover.nPremimin = .FieldToClass("nPremimin")
                    lclsTCover.nPremimax = .FieldToClass("nPremiMax")
                    lclsTCover.nPolicy = .FieldToClass("nPolicy")
                    lclsTCover.nCertif = .FieldToClass("nCertif")
                    lclsTCover.nBranch = .FieldToClass("nBranch")
                    lclsTCover.nProduct = .FieldToClass("nProduct")
                    lclsTCover.nGroup = .FieldToClass("nGroup")
                    lclsTCover.nModulec = .FieldToClass("nModulec")
                    lclsTCover.nCover = .FieldToClass("nCover")
                    lclsTCover.nCurrency = .FieldToClass("nCurrency")
                    lclsTCover.nWait_quan = .FieldToClass("nWait_quan")
                    lclsTCover.nGroup_insu = .FieldToClass("nGroup_insu")
                    lclsTCover.nCover_in = .FieldToClass("nCover_in")
                    lclsTCover.nCoverapl = .FieldToClass("nCoverApl")
                    lclsTCover.sKey = .FieldToClass("sKey")
                    lclsTCover.nPremifix = .FieldToClass("nPremifix")
                    lclsTCover.sCacalfri = .FieldToClass("sCacalfri")
                    lclsTCover.nChcaplev = .FieldToClass("nChCapLev")
                    lclsTCover.nChprelev = .FieldToClass("nChPreLev")
                    lclsTCover.nFduserlev = .FieldToClass("nFDUserLev")
                    lclsTCover.sFdchantyp = .FieldToClass("sFDChantyp")
                    lclsTCover.nFdrateadd = .FieldToClass("nFDRateAdd")
                    lclsTCover.nFdratesub = .FieldToClass("nFDRateSub")
                    lclsTCover.nCacalcov = .FieldToClass("nCacalcov")
                    lclsTCover.nCacalper = .FieldToClass("nCacalper")
                    lclsTCover.sPfrandedi = .FieldToClass("spFrandedi")
                    lclsTCover.nCacalmax = .FieldToClass("nCacalmax")
                    lclsTCover.nCacalmin = .FieldToClass("nCacalmin")
                    lclsTCover.sAddsuini = .FieldToClass("sAddsuini")
                    lclsTCover.nTarifcurr = .FieldToClass("nTarifCurr")
                    lclsTCover.sRouchaca = .FieldToClass("sRouchaca")
                    lclsTCover.nCacalfix = .FieldToClass("nCacalfix")
                    lclsTCover.nCapital_wait = .FieldToClass("nCapital_wait")
                    lclsTCover.nAgeminins = .FieldToClass("nAgeminins")
                    lclsTCover.nAgemaxins = .FieldToClass("nAgemaxins")
                    lclsTCover.nAgemaxper = .FieldToClass("nAgemaxper")
                    lclsTCover.nTypDurins = .FieldToClass("nTypdurins", 0)
                    lclsTCover.nDurinsur = .FieldToClass("nDurinsur")
                    lclsTCover.nTypDurpay = .FieldToClass("nTypdurpay")
                    lclsTCover.nDurpay = .FieldToClass("nDurpay")
                    lclsTCover.dAniversary = .FieldToClass("dAniversary")
                    lclsTCover.dSeektar = .FieldToClass("dSeektar")
                    lclsTCover.nRetarif = .FieldToClass("nRetarif")
                    lclsTCover.nAgemininsf = .FieldToClass("nAgemininsf")
                    lclsTCover.nAgemaxinsf = .FieldToClass("nAgemaxinsf")
                    lclsTCover.nAgemaxperf = .FieldToClass("nAgemaxperf")
                    lclsTCover.sRequirec = .FieldToClass("sRequirec")
                    lclsTCover.sDefaultic = .FieldToClass("sDefaultic")
                    lclsTCover.nBranch_rei = .FieldToClass("nBranch_rei")
                    lclsTCover.sBas_sumins = .FieldToClass("sBas_sumins")

                    '-Variable nuevas se agregaron a la tabla TCOVER
                    lclsTCover.sRoucapit = .FieldToClass("sRoucapit")
                    lclsTCover.nCamaxcov = .FieldToClass("nCamaxcov")
                    lclsTCover.nCamaxper = .FieldToClass("nCamaxper")
                    lclsTCover.nCamaxrol = .FieldToClass("nCamaxrol")
                    lclsTCover.nCacalmul = .FieldToClass("nCacalmul")
                    lclsTCover.nGenCurrency = .FieldToClass("nGenCurrency")

                    '+ Variable agregada para el manejo del capital requerido por el asegurado
                    lclsTCover.nCapital_req = .FieldToClass("nCapital_req")

                    Call Add(lclsTCover)
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreatcover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreatcover = Nothing
    End Function

    '%FindCoverPolicy. Esta rutina se encarga de realizar el cálculo de las coberturas
    '%de la póliza/certificado.
    Public Function FindCoverPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nGroup As Integer, ByVal sCodispl As String, ByVal nUsercode As Integer, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal sBrancht As String, ByVal nProdClas As Integer, ByVal sKey As String, ByVal sDelTCover As String, ByVal lobjPolicy As Policy, Optional ByVal sRecPopup As String = "", Optional ByVal bUpdCover As Boolean = False, Optional ByVal nType_amend As Integer = 0) As Boolean
        Dim lclsCertificat As Certificat = New Certificat
        Dim lclsPolicy As Policy
        Dim lclsCover As Cover
        Dim lclsTCover As TCover
        Dim lcol As Collection = New Collection
        Dim lclsProduct As eProduct.Product
        Dim lclsModulules As Modules

        Dim llngBranchType As Integer
        Dim lintGroup As Integer
        Dim lintTransaction As Integer
        Dim lintModules As Integer
        Dim ldblCapitalFix As Double
        Dim lintGroup_insu As Integer
        Dim ldblPremiumFix As Double
        Dim lblnFound As Boolean

        Dim lblnQuery As Boolean
        Dim lblnPoliModules As Boolean
        Dim lblnModules As Boolean
        Dim lblnCoverDel As Boolean

        On Error GoTo FindCoverPolicy_err

        lblnQuery = nTransaction = Constantes.PolTransac.clngPolicyQuery Or nTransaction = Constantes.PolTransac.clngCertifQuery Or nTransaction = Constantes.PolTransac.clngQuotationQuery Or nTransaction = Constantes.PolTransac.clngProposalQuery Or nTransaction = Constantes.PolTransac.clngQuotAmendentQuery Or nTransaction = Constantes.PolTransac.clngPropAmendentQuery Or nTransaction = Constantes.PolTransac.clngQuotRenewalQuery Or nTransaction = Constantes.PolTransac.clngPropRenewalQuery

        FindCoverPolicy = True

        If lblnQuery Then
            lclsCover = New Cover
            lcol = lclsCover.Find_Query(sCertype, nBranch, nProduct, nPolicy, nCertif, nCurrency, dEffecdate, sClient, sBrancht, nGroup)
            If lobjPolicy Is Nothing Then
                lclsPolicy = New Policy
                Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
            Else
                lclsPolicy = lobjPolicy
            End If

            nLegAmount = lclsPolicy.nLegAmount

            'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCover = Nothing
        Else
            '+Se leen los datos de la póliza
            If lobjPolicy Is Nothing Then
                lclsPolicy = New Policy
                Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
            Else
                lclsPolicy = lobjPolicy
            End If

            '+Se verifica si el producto es modular
            lclsProduct = New eProduct.Product
            If lclsProduct.IsModule(nBranch, nProduct, dEffecdate) Then
                lblnModules = True

                lclsModulules = New Modules
                '+Se asignan los parámetros para realizar la lectura
                lblnPoliModules = lclsModulules.InsValModulPolicy(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lclsPolicy.sTyp_module)
                'UPGRADE_NOTE: Object lclsModulules may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsModulules = Nothing
            Else
                lblnModules = False
            End If

            '+Se valida que el producto no sea modular, o que sea modular y existan módulos definidos
            If Not lblnModules Or lblnPoliModules Then

                If sBrancht <> CStr(eProduct.Product.pmBrancht.pmlife) Then
                    llngBranchType = 2 'clngNoLife
                Else
                    llngBranchType = 1 'clngLife
                End If

                nLegAmount = lclsPolicy.nLegAmount

                '+ Si las especificaciones son por grupo
                If lclsPolicy.sTyp_module = "3" Then
                    lintGroup = nGroup
                Else
                    lintGroup = 0
                End If

                If nTransaction = Constantes.PolTransac.clngPolicyQuery Or nTransaction = Constantes.PolTransac.clngCertifQuery Or nTransaction = Constantes.PolTransac.clngQuotationQuery Or nTransaction = Constantes.PolTransac.clngProposalQuery Then
                    lintTransaction = 1
                Else
                    lintTransaction = 2
                End If

                If lblnPoliModules Then
                    lintModules = 1
                Else
                    lintModules = 2
                End If

                If sBrancht <> CStr(eProduct.Product.pmBrancht.pmlife) Then
                    ldblCapitalFix = 0
                    lintGroup_insu = 0
                Else
                    If nProdClas = 6 Then
                        ldblCapitalFix = 0
                        ldblPremiumFix = 0
                    ElseIf nProdClas <> 7 And nProdClas <> 9 And nProdClas <> 10 Then
                        If lclsCertificat Is Nothing Then
                            lclsCertificat = New Certificat
                        End If
                        Call lclsCertificat.FindParticularDataLI(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
                        ldblCapitalFix = IIf(lclsCertificat.nCapital_ca <> eRemoteDB.Constants.intNull, lclsCertificat.nCapital_ca, 0)
                        ldblPremiumFix = IIf(lclsCertificat.nPremium_ca <> eRemoteDB.Constants.intNull, lclsCertificat.nPremium_ca, 0)
                    End If
                End If
                If lclsCertificat Is Nothing Then
                    lclsCertificat = New Certificat
                    Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
                End If
                If lclsCertificat.nCertif = 0 And lclsPolicy.sPolitype <> "1" And (lclsPolicy.sTyp_module = "4" Or lclsPolicy.sTyp_module = String.Empty) Then
                    lblnFound = False
                Else
                    If sRecPopup = "1" Then
                        FindCoverPolicy = Find(sKey, sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, nRole, sClient)
                        lblnFound = True
                    Else
                        If CalProductCover(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lintGroup, "1", nUsercode, nTransaction, sKey, nCurrency, dNulldate, llngBranchType, nRole, sClient, sDelTCover, bUpdCover, nType_amend) Then
                            lblnFound = True
                        Else
                            Me.nError = 1073
                            Me.bError = True
                        End If
                    End If
                End If
            Else
                '+ 13022: Póliza-certificado no tiene módulos asociados
                Me.nError = 13022
                Me.bError = True
            End If
        End If

        If Not lblnFound Then
            If lblnQuery Then
                For Each lclsCover In lcol
                    lclsTCover = New TCover
                    With lclsTCover
                        .sCertype = sCertype
                        .sChange = lclsCover.sChange
                        .sFrandedi = lclsCover.sFrandedi
                        .sWait_type = lclsCover.sWait_type
                        .sFrancApl = lclsCover.sFrancApl
                        .sFree_premi = lclsCover.sFree_premi
                        .sDescript = lclsCover.sDescript
                        .sExist = "1"
                        .sRequired = lclsCover.sRequired
                        .sDefaulti = "1"
                        .sCacalili = lclsCover.sCacalili
                        .dEffecdate = dEffecdate
                        .nCapital = lclsCover.nCapital
                        .nDiscount = lclsCover.nDiscount
                        .nFixamount = lclsCover.nFixamount
                        .nMaxamount = lclsCover.nMaxamount
                        .nRate = lclsCover.nRate
                        .nMinamount = lclsCover.nMinamount
                        .nPremium = lclsCover.nPremium
                        .nRatecove = lclsCover.nRatecove
                        .nDisc_Amoun = lclsCover.nDisc_Amoun
                        .nPolicy = nPolicy
                        .nCertif = nCertif
                        .nBranch = nBranch
                        .nProduct = nProduct
                        .nGroup = lclsCover.nGroup
                        .nModulec = lclsCover.nModulec
                        .nCover = lclsCover.nCover
                        .nCurrency = lclsCover.nCurrency
                        .nWait_quan = lclsCover.nWait_quan
                        .nGroup_insu = 0
                        .sKey = sKey
                        .sExist = "1"
                        .nCapital_wait = lclsCover.nCapital_wait
                        .nAgeminins = lclsCover.nAgeminins
                        .nAgemaxins = lclsCover.nAgemaxins
                        .nAgemaxper = lclsCover.nAgemaxper
                        .nTypDurins = lclsCover.nTypDurins
                        .nDurinsur = lclsCover.nDurinsur
                        .nTypDurpay = lclsCover.nTypDurpay
                        .nDurpay = lclsCover.nDurpay
                        .sClient = lclsCover.sClient
                        .nRole = lclsCover.nRole
                        .dAniversary = lclsCover.dAniversary
                        .dSeektar = lclsCover.dSeektar
                        .nAgemininsf = lclsCover.nAgemininsf
                        .nAgemaxinsf = lclsCover.nAgemaxinsf
                        .nAgemaxperf = lclsCover.nAgemaxperf
                        .dFer = lclsCover.dFer
                        .nCauseupd = lclsCover.nCauseupd
                        .nBranch_rei = lclsCover.nBranch_rei
                        .nCapital_req = lclsCover.nCapital_req
                        .nRateCla = lclsCover.nRate
                        .nFixAmoCla = lclsCover.nFixAmoCla
                        .nMinAmoCla = lclsCover.nMinAmoCla
                        .nMaxAmoCla = lclsCover.nMaxAmoCla
                        .nDiscCla = lclsCover.nDisc_AmoCla
                        .nDisc_AmoCla = lclsCover.nDisc_AmoCla
                        .nFrancDays = lclsCover.nFrancDays

                    End With
                    Call Add(lclsTCover)
                    'UPGRADE_NOTE: Object lclsTCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsTCover = Nothing
                Next lclsCover
            Else
                FindCoverPolicy = False
            End If
        End If

FindCoverPolicy_err:
        If Err.Number Then
            FindCoverPolicy = False
        End If
        'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCover = Nothing
        'UPGRADE_NOTE: Object lclsTCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTCover = Nothing
        'UPGRADE_NOTE: Object lcol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcol = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        On Error GoTo 0
    End Function
	
	'%InsHereditCover_p_g: Hereda las condiciones de asegurabilidad de una poliza a otra
	Public Function InsHereditCover_p_g(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPolhered As Double, ByVal dEffecdate As Date, ByVal sKey As String, ByVal sTyp_module As String) As Boolean
		Dim lrecInsHereditCover_p_g As eRemoteDB.Execute
		Dim lclsTCover As TCover
		
		On Error GoTo InsHereditCover_p_g_Err
		
		lrecInsHereditCover_p_g = New eRemoteDB.Execute
		
		With lrecInsHereditCover_p_g
			.StoredProcedure = "InsHereditCover_p_g"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolhered", nPolhered, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsTCover = New TCover
					lclsTCover.sCertype = .FieldToClass("sCertype")
					lclsTCover.sChange = .FieldToClass("sChange")
					lclsTCover.sFrandedi = .FieldToClass("sFrandedi")
					lclsTCover.sWait_type = .FieldToClass("sWait_Type")
					lclsTCover.sFrancApl = .FieldToClass("sFrancApl")
					lclsTCover.sFree_premi = .FieldToClass("sFree_premi")
					lclsTCover.sDescript = .FieldToClass("sDescript")
					lclsTCover.sExist = .FieldToClass("sExist")
					If Not bDataFound Then
						If lclsTCover.sExist = "1" Then
							bDataFound = True
						End If
					End If
					lclsTCover.sRequired = .FieldToClass("sRequire")
					lclsTCover.sDefaulti = .FieldToClass("sDefaulti")
					lclsTCover.sCacalili = .FieldToClass("sCacalili")
					lclsTCover.sCh_typ_cap = .FieldToClass("sCh_typ_cap")
					lclsTCover.sChange_typ = .FieldToClass("sChange_typ")
					lclsTCover.sFdrequire = .FieldToClass("sFDRequire")
					lclsTCover.sRoupremi = .FieldToClass("sRouPremi")
					lclsTCover.dEffecdate = .FieldToClass("dEffecdate")
					lclsTCover.nCapital = .FieldToClass("nCapital")
					lclsTCover.nDiscount = .FieldToClass("nDiscount")
					lclsTCover.nFixamount = .FieldToClass("nFixAmount")
					lclsTCover.nMaxamount = .FieldToClass("nMaxAmount")
					lclsTCover.nRate = .FieldToClass("nRate")
					lclsTCover.nMinamount = .FieldToClass("nMinAmount")
					lclsTCover.nPremium = .FieldToClass("nPremium")
					lclsTCover.nRatecove = .FieldToClass("nRateCove")
					lclsTCover.nCapitali = .FieldToClass("nCapitali")
					lclsTCover.nRatecapadd = .FieldToClass("nRateCapAdd")
					lclsTCover.nRatecapsub = .FieldToClass("nRateCapSub")
					lclsTCover.nRatepreadd = .FieldToClass("nRatePreAdd")
					lclsTCover.nRatepresub = .FieldToClass("nRatePreSub")
					lclsTCover.nDisc_Amoun = .FieldToClass("nDisc_amoun")
					lclsTCover.npremirat = .FieldToClass("nPremiRat")
					lclsTCover.nPremimin = .FieldToClass("nPremimin")
					lclsTCover.nPremimax = .FieldToClass("nPremiMax")
					lclsTCover.nPolicy = .FieldToClass("nPolicy")
					lclsTCover.nCertif = .FieldToClass("nCertif")
					lclsTCover.nBranch = .FieldToClass("nBranch")
					lclsTCover.nProduct = .FieldToClass("nProduct")
					lclsTCover.nGroup = .FieldToClass("nGroup")
					lclsTCover.nModulec = .FieldToClass("nModulec")
					lclsTCover.nCover = .FieldToClass("nCover")
					lclsTCover.nCurrency = .FieldToClass("nCurrency")
					lclsTCover.nWait_quan = .FieldToClass("nWait_quan")
					lclsTCover.nGroup_insu = .FieldToClass("nGroup_insu")
					lclsTCover.nCover_in = .FieldToClass("nCover_in")
					lclsTCover.nCoverapl = .FieldToClass("nCoverApl")
					lclsTCover.sKey = .FieldToClass("sKey")
					lclsTCover.nPremifix = .FieldToClass("nPremifix")
					lclsTCover.sCacalfri = .FieldToClass("sCacalfri")
					lclsTCover.nChcaplev = .FieldToClass("nChCapLev")
					lclsTCover.nChprelev = .FieldToClass("nChPreLev")
					lclsTCover.nFduserlev = .FieldToClass("nFDUserLev")
					lclsTCover.sFdchantyp = .FieldToClass("sFDChantyp")
					lclsTCover.nFdrateadd = .FieldToClass("nFDRateAdd")
					lclsTCover.nFdratesub = .FieldToClass("nFDRateSub")
					lclsTCover.nCacalcov = .FieldToClass("nCacalcov")
					lclsTCover.nCacalper = .FieldToClass("nCacalper")
					lclsTCover.sPfrandedi = .FieldToClass("spFrandedi")
					lclsTCover.nCacalmax = .FieldToClass("nCacalmax")
					lclsTCover.nCacalmin = .FieldToClass("nCacalmin")
					lclsTCover.sAddsuini = .FieldToClass("sAddsuini")
					lclsTCover.nTarifcurr = .FieldToClass("nTarifCurr")
					lclsTCover.sRouchaca = .FieldToClass("sRouchaca")
					lclsTCover.nCacalfix = .FieldToClass("nCacalfix")
					lclsTCover.nCapital_wait = .FieldToClass("nCapital_wait")
					lclsTCover.nAgeminins = .FieldToClass("nAgeminins")
					lclsTCover.nAgemaxins = .FieldToClass("nAgemaxins")
					lclsTCover.nAgemaxper = .FieldToClass("nAgemaxper")
					lclsTCover.nTypDurins = .FieldToClass("nTypdurins")
					lclsTCover.nDurinsur = .FieldToClass("nDurinsur")
					lclsTCover.nTypDurpay = .FieldToClass("nTypdurpay")
					lclsTCover.nDurpay = .FieldToClass("nDurpay")
					lclsTCover.nRole = .FieldToClass("nRole")
					lclsTCover.sClient = .FieldToClass("sClient")
					lclsTCover.dAniversary = .FieldToClass("dAniversary")
					lclsTCover.dSeektar = .FieldToClass("dSeektar")
					lclsTCover.nRetarif = .FieldToClass("nRetarif")
					lclsTCover.nAgemininsf = .FieldToClass("nAgemininsf")
					lclsTCover.nAgemaxinsf = .FieldToClass("nAgemaxinsf")
					lclsTCover.nAgemaxperf = .FieldToClass("nAgemaxperf")
					lclsTCover.sRequirec = .FieldToClass("sRequirec")
					lclsTCover.sDefaultic = .FieldToClass("sDefaultic")
					lclsTCover.nBranch_rei = .FieldToClass("nBranch_rei")
					lclsTCover.sBas_sumins = .FieldToClass("sBas_sumins")
					
					'-Variable nuevas se agregaron a la tabla TCOVER
					lclsTCover.sRoucapit = .FieldToClass("sRoucapit")
					lclsTCover.nCamaxcov = .FieldToClass("nCamaxcov")
					lclsTCover.nCamaxper = .FieldToClass("nCamaxper")
					lclsTCover.nCamaxrol = .FieldToClass("nCamaxrol")
					lclsTCover.nCacalmul = .FieldToClass("nCacalmul")
					lclsTCover.nGenCurrency = .FieldToClass("nGenCurrency")
					
					
					Call Add(lclsTCover)
					'UPGRADE_NOTE: Object lclsTCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTCover = Nothing
					.RNext()
				Loop 
				InsHereditCover_p_g = True
				.RCloseRec()
			End If
		End With
InsHereditCover_p_g_Err: 
		If Err.Number Then
			InsHereditCover_p_g = False
		End If
		'UPGRADE_NOTE: Object lrecInsHereditCover_p_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsHereditCover_p_g = Nothing
		'UPGRADE_NOTE: Object lclsTCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTCover = Nothing
		On Error GoTo 0
	End Function
	
	'%sKey. Esta propiedad se encarga de devolver la llave de lectura del registro de coberturas
	Public ReadOnly Property sKey(ByVal nUsercode As Integer, ByVal nSessionId As String) As String
		Get
			sKey = "Cov" & CStr(nSessionId) & "-" & CStr(nUsercode)
		End Get
	End Property
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	'-----------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TCover
		Get
			'-----------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'%Delete. Esta funcion se encarga de eliminar los registros de la tabla tCovers
    Public Function Delete(ByVal sKey As String) As Boolean
        Dim lrecdeltCover As eRemoteDB.Execute

        On Error GoTo Delete_err
        lrecdeltCover = New eRemoteDB.Execute

        With lrecdeltCover
            .StoredProcedure = "deltCover"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecdeltCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdeltCover = Nothing
    End Function
	
	'%FindItem: Busca un elemento dentro de la colección dado el código de la cobertura
	Public Function FindItem(ByVal nCover As Integer) As Boolean
		Dim lintIndex As Integer
		FindItem = False
		'UPGRADE_NOTE: Object objtCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objtCover = Nothing
		For lintIndex = 1 To mCol.Count()
			If mCol.Item(lintIndex).nCover = nCover Then
				objtCover = mCol.Item(lintIndex)
				FindItem = True
				Exit For
			End If
		Next 
	End Function
	
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find: Obtiene los datos de la tabla TCover para la transacción VI7011
	Public Function InsPreVI7011(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sKey As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecreatcover As eRemoteDB.Execute
		Dim lclsTCover As TCover
		
		On Error GoTo Find_Err
		lrecreatcover = New eRemoteDB.Execute
		mCol = New Collection
		With lrecreatcover
			.StoredProcedure = "INSVI7011PKG.INSPREVI7011"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 10, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				InsPreVI7011 = True
				Do While Not .EOF
					lclsTCover = New TCover
					lclsTCover.sCertype = .FieldToClass("sCertype")
					lclsTCover.sChange = .FieldToClass("sChange")
					lclsTCover.sDescript = .FieldToClass("sDescript")
					lclsTCover.sExist = .FieldToClass("sExist")
					lclsTCover.nRole = .FieldToClass("nRole")
					lclsTCover.sClient = .FieldToClass("sClient")
					lclsTCover.sRequired = .FieldToClass("sRequire")
					lclsTCover.sDefaulti = .FieldToClass("sDefaulti")
					lclsTCover.sCacalili = .FieldToClass("sCacalili")
					lclsTCover.dEffecdate = .FieldToClass("dEffecdate")
					lclsTCover.nCapital = .FieldToClass("nCapital")
					lclsTCover.nPremium = .FieldToClass("nPremium")
					lclsTCover.nModulec = .FieldToClass("nModulec")
					lclsTCover.nCover = .FieldToClass("nCover")
					lclsTCover.nCurrency = .FieldToClass("nCurrency")
					lclsTCover.sKey = .FieldToClass("sKey")
					lclsTCover.nCacalmax = .FieldToClass("nCacalmax")
					lclsTCover.nCacalmin = .FieldToClass("nCacalmin")
					lclsTCover.nAgeminins = .FieldToClass("nAgeminins")
					lclsTCover.nAgemaxins = .FieldToClass("nAgemaxins")
					lclsTCover.nAgemaxper = .FieldToClass("nAgemaxper")
					lclsTCover.nAgemininsf = .FieldToClass("nAgemininsf")
					lclsTCover.nAgemaxinsf = .FieldToClass("nAgemaxinsf")
					lclsTCover.nAgemaxperf = .FieldToClass("nAgemaxperf")
					lclsTCover.sRequirec = .FieldToClass("sRequirec")
					
					'-Variable nuevas se agregaron a la tabla TCOVER
					lclsTCover.nPremfreq1 = .FieldToClass("nPremfreq1")
					lclsTCover.nPremfreq2 = .FieldToClass("nPremfreq2")
					lclsTCover.nPremfreq3 = .FieldToClass("nPremfreq3")
					
					Call Add(lclsTCover)
					.RNext()
				Loop 
				.RCloseRec()
			Else
				InsPreVI7011 = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			InsPreVI7011 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreatcover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatcover = Nothing
	End Function
End Class






