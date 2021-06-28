Option Strict Off
Option Explicit On
Public Class Cover
	'%-------------------------------------------------------%'
	'% $Workfile:: Cover.cls                                $%'
	'% $Author:: Jsarabia                                   $%'
	'% $Date:: 7-08-09 12:23                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema al 28/11/2000.
	'Column_Name                                 Type      Length  Prec  Scale Nullable
	'------------------------- --------------- - -------- ------- ----- ------ --------
	Public sCertype As String ' CHAR           1              No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Double ' NUMBER        22    10      0 No
	Public nCertif As Double ' NUMBER        22    10      0 No
	Public nGroup_insu As Integer ' NUMBER        22     5      0 No
	Public nModulec As Integer ' NUMBER        22     5      0 No
	Public nCover As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public sClient As String ' CHAR          14              No
	Public nCapital As Double ' NUMBER        22    18      6 Yes
	Public nCapitali As Double ' NUMBER        22    18      6 Yes
	Public sChange As String ' CHAR           1              Yes
	Public sFrandedi As String ' CHAR           1              Yes
	Public nCurrency As Integer ' NUMBER        22     5      0 Yes
	Public nDiscount As Single ' NUMBER        22     4      2 Yes
	Public nFixamount As Integer ' NUMBER        22    10      0 Yes
	Public nMaxamount As Integer ' NUMBER        22    10      0 Yes
	Public sFree_premi As String ' CHAR           1              Yes
	Public nMinamount As Integer ' NUMBER        22    10      0 Yes
	Public dNulldate As Date ' DATE           7              Yes
	Public nPremium As Double ' NUMBER        22    10      2 Yes
	Public nPremium_tot As Double ' NUMBER        22    10      2 Yes
	Public nRate As Single ' NUMBER        22     4      2 Yes
	Public nWait_quan As Integer ' NUMBER        22     5      0 Yes
	Public nRatecove As Double ' NUMBER        22     9      6 Yes
	Public nRatecove_b As Double ' NUMBER        22     9      6 Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 Yes
	Public sWait_type As String ' CHAR           1              Yes
	Public sFrancApl As String ' CHAR           1              Yes
	Public nDisc_Amoun As Double ' NUMBER        22     8      2 Yes
	Public nTypDurins As Integer ' NUMBER        22     5      0 Yes
	Public nDurinsur As Integer ' NUMBER        22     5      0 Yes
	Public nAgeminins As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxins As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxper As Integer ' NUMBER        22     5      0 Yes
	Public nTypDurpay As Integer ' NUMBER        22     5      0 Yes
	Public nDurpay As Integer ' NUMBER        22     5      0 Yes
	Public nCauseupd As Integer ' NUMBER        22     5      0 Yes
	Public nCapital_wait As Double ' NUMBER        22    18      6 Yes
	Public nAgelimit As Integer ' NUMBER        22     5      0 Yes
	Public nAge_per As Integer ' NUMBER        22     5      0 Yes
	Public dAniversary As Date ' DATE           7              Yes
	Public dSeektar As Date ' DATE           7              Yes
	Public dFer As Date ' DATE           7              Yes
	Public nBranch_rei As Integer ' NUMBER        22     5      0 Yes
	Public nRole As Integer ' NUMBER        22     5      0 Yes
	Public nRetarif As Integer ' NUMBER        22     5      0 Yes
	Public nApply_Perc As Double
	Public ncommi_an As Double
	Public nClaim As Double
	Public nCapital_req As Double
    Public nRateCla As Double
    Public nFixAmoCla As Double
    Public nMinAmoCla As Double
    Public nMaxAmoCla As Double
    Public nDiscCla As Double
    Public nDisc_AmoCla As Double    
    Public nFrancDays As Double
    '- Propiedades auxiliares

    '- Arreglo para la carga de las monedas de las coberturas de la poliza
    Private marrCurr() As Integer

    Public sTyp_module As String

    Public sLeg As String

    Public sTypenom As String

    Public sPolitype As String

    Public sCodispl As String

    Public nGroup As Integer

    Public nAction As Integer

    Public sTransaction As String

    Public sBrancht As String

    '- Indica si el arreglo se cargo o no
    Private mblnCharge As Boolean

    '- Propiedades Axiliares por Find_Query
    Public sExist As String
    Public sDefaulti As String
    Public sDescript As String
    Public sRequired As String
    Public sCacalili As String
    Public sCh_typ_cap As String
    Public nRatecapadd As Double
    Public nRatecapsub As Double
    Public nCover_in As Integer
    Public nRatepreadd As Double
    Public nRatepresub As Double
    Public sChange_typ As String
    Public sFdrequire As String
    Public nPremifix As Double
    Public npremirat As Double
    Public nCoverapl As Integer
    Public nPremimin As Double
    Public nPremimax As Double
    Public sRoupremi As String
    Public sCacaltyp As String
    Public nCacalcov As Integer
    Public nCacalper As Double
    Public nChcaplev As Integer
    Public nChprelev As Integer
    Public sFdchantyp As String
    Public nFduserlev As Integer
    Public nFdrateadd As Double
    Public nFdratesub As String
    Public sPfrandedi As String
    Public nCacalmax As Double
    Public nCacalmin As Double
    Public sAddsuini As String
    Public nTarifcurr As Integer
    Public nRolcap As Integer
    Public sDepend As String
    Public nActionCov As Integer

    '-Propiedades Axiliares por CalPremium
    Public nProcess As Integer
    Public gennCurrency As Integer
    Public sKey As String
    Public conCapital As Double
    Public sMessage As String

    '-Propiedades Axiliares para la forma VI009
    Public nAge_reinsu As Integer
    Public nSalvage As Integer
    Public nAmount As Double
    Public nCharge As Double
    Public nRescue_Charge As Double
    Public dRescuedate As Date

    '- Objetos para el manejo de la CA014
    Public mcolTCovers As TCovers
    Public mclsRoles As Roles
    Public mclsCurren_pol As Curren_pol
    Public bFindGroup As Boolean
    Public nCountGroup As Integer
    Public nCountCurrency As Integer
    Public nProdClas As Integer
    Public nLegAmount As Double
    Public bNopayroll As Boolean
    Public nBalance As Double
    Public nSalvage_curr As Double

    '- Variables para la CA014A
    Public bError As Boolean

    Public nError As Integer
    Public nAgemininsf As Integer
    Public nAgemaxinsf As Integer
    Public nAgemaxperf As Integer
    Public bTransaction As Boolean

    '+Arreglo para la carga de la coberturas con valores de rescate
    Private Structure udtCover_surr
        Dim sDescript As String
        Dim nAge_reinsu As Integer
        Dim dEffecdate As Date
        Dim nCurrency As Integer
        Dim nCapital As Double
        Dim nSalvage As Double
        Dim nCover As Integer
        Dim sAddsuini As String
        Dim nCharge As Double
        Dim nBalance As Double
        Dim nSalvage_curr As Double
    End Structure

    Private Structure udtCover
        Dim nCurrency As Integer
        Dim nGroup As Integer
        Dim nGroup_insu As Integer
        Dim nModulec As Double
        Dim nCover As Double
        Dim sDescript As String
        Dim nCapital As Double
        Dim nCapital_wait As Double
        Dim nRatecove As Double
        Dim nRatecove_b As Double
        Dim nPremium As Double
        Dim nPremium_tot As Double
        Dim ncommi_an As Double
    End Structure

    Public bModulec As Boolean
    '- Arreglo para la carga de recibos
    Private marrCover() As udtCover

    '- Descripciones para la CA014

    Public sdesc_t5559 As String
    Public sdesc_t64 As String
    Public sdesc_t33 As String
    Public sdesc_t5589 As String
    Public sdesc_t_pay As String
    Public sdesc_t52 As String
    Public sdesc_t5547 As String
    Public sdesc_t5000 As String

    '- Descripción de la moneda por cobertura covdatasi001
    Public sShort_des As String

    Private arrCover_surr() As udtCover_surr

    Public bDisabledByLevels As Boolean

    '% CountCovers: Permite identificar si existe información en  las tablas de Coberturas Cover, Cover_co_g, Cover_co_p
    Public Function CountCovers(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String, ByVal sTyp_module As String) As Boolean
        Dim lrecReaCoverCount As eRemoteDB.Execute

        On Error GoTo CountCovers_Err
        lrecReaCoverCount = New eRemoteDB.Execute
        '+ Definición de store procedure ReaCoverCount al 08-20-2002 17:42:28
        With lrecReaCoverCount
            .StoredProcedure = "ReaCoverCount"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                CountCovers = .Parameters("nCount").Value > 0
            End If
        End With

CountCovers_Err:
        If Err.Number Then
            CountCovers = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecReaCoverCount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaCoverCount = Nothing
    End Function

    '% Find: Obtiene la información de una cobertura
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nRole As String, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaCover_o As eRemoteDB.Execute

        On Error GoTo Find_Err

        If sCertype <> Me.sCertype Or nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nPolicy <> Me.nPolicy Or nCertif <> Me.nCertif Or nGroup_insu <> Me.nGroup_insu Or nModulec <> Me.nModulec Or nCover <> Me.nCover Or sClient <> Me.sClient Or CDbl(nRole) <> Me.nRole Or dEffecdate <> Me.dEffecdate Or bFind Then

            '+ Definición de store procedure reaCover_o al 12-02-2002 13:32:34
            lrecreaCover_o = New eRemoteDB.Execute
            With lrecreaCover_o
                .StoredProcedure = "ReaCover_o"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Find = True
                    Me.sCertype = .FieldToClass("sCertype")
                    Me.nBranch = .FieldToClass("nBranch")
                    Me.nProduct = .FieldToClass("nProduct")
                    Me.nPolicy = .FieldToClass("nPolicy")
                    Me.nCertif = .FieldToClass("nCertif")
                    Me.nGroup_insu = .FieldToClass("nGroup_insu")
                    Me.nModulec = .FieldToClass("nModulec")
                    Me.nCover = .FieldToClass("nCover")
                    Me.dEffecdate = .FieldToClass("dEffecdate")
                    Me.sClient = .FieldToClass("sClient")
                    nRole = .FieldToClass("nRole")
                    nCapital = .FieldToClass("nCapital")
                    nCapitali = .FieldToClass("nCapitali")
                    sChange = .FieldToClass("sChange")
                    sFrandedi = .FieldToClass("sFrandedi")
                    nCurrency = .FieldToClass("nCurrency")
                    nDiscount = .FieldToClass("nDiscount")
                    nFixamount = .FieldToClass("nFixamount")
                    nMaxamount = .FieldToClass("nMaxamount")
                    sFree_premi = .FieldToClass("sFree_premi")
                    nMinamount = .FieldToClass("nMinamount")
                    dNulldate = .FieldToClass("dNulldate")
                    nPremium = .FieldToClass("nPremium")
                    nRate = .FieldToClass("nRate")
                    nWait_quan = .FieldToClass("nWait_quan")
                    nRatecove = .FieldToClass("nRatecove")
                    sWait_type = .FieldToClass("sWait_type")
                    sFrancApl = .FieldToClass("sFrancapl")
                    nDisc_Amoun = .FieldToClass("nDisc_amoun")
                    nTypDurins = .FieldToClass("nTypdurins")
                    nDurinsur = .FieldToClass("nDurinsur")
                    nAgeminins = .FieldToClass("nAgeminins")
                    nAgemaxins = .FieldToClass("nAgemaxins")
                    nAgemaxper = .FieldToClass("nAgemaxper")
                    nTypDurpay = .FieldToClass("nTypdurpay")
                    nDurpay = .FieldToClass("nDurpay")
                    nCauseupd = .FieldToClass("nCauseupd")
                    nCapital_wait = .FieldToClass("nCapital_wait")
                    nAgelimit = .FieldToClass("nAgelimit")
                    nAge_per = .FieldToClass("nAge_per")
                    dAniversary = .FieldToClass("dAniversary")
                    dSeektar = .FieldToClass("dSeektar")
                    dFer = .FieldToClass("dFer")
                    nBranch_rei = .FieldToClass("nBranch_rei")
                    nRetarif = .FieldToClass("nRetarif")
                    .RCloseRec()
                End If
            End With
        Else
            Find = True
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaCover_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCover_o = Nothing
        On Error GoTo 0
    End Function

    '% InsReaCertif_SumCapital: Obtiene la suma de los capitales de las coberturas asociadas al certificado
    Public Function InsReaCertif_SumCapital(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Double
        Dim lrecreaCertifcover_sumcapital As eRemoteDB.Execute

        On Error GoTo InsReaCertif_SumCapital_Err

        lrecreaCertifcover_sumcapital = New eRemoteDB.Execute
        With lrecreaCertifcover_sumcapital
            .StoredProcedure = "reaCertifcover_sumcapital"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                InsReaCertif_SumCapital = .Parameters("nCapital").Value
            End If
        End With

InsReaCertif_SumCapital_Err:
        If Err.Number Then
            InsReaCertif_SumCapital = 0
        End If
        'UPGRADE_NOTE: Object lrecreaCertifcover_sumcapital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertifcover_sumcapital = Nothing
        On Error GoTo 0
    End Function

    '%insValPolicyLimits: Función que realiza la validación de los límites de una póliza.
    Public Function insValPolicyLimits(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecValLimits As eRemoteDB.Execute

        On Error GoTo insValPolicyLimits_Err

        lrecValLimits = New eRemoteDB.Execute

        insValPolicyLimits = True

        With lrecValLimits
            .StoredProcedure = "reaLimitsPolicy"

            '+ Ojo se debe asignar el valor del código de seguridad del sistema según el usuario. Falta por desarrollarlo.

            .Parameters.Add("sSche_code", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                If .ErrorNumber <> eRemoteDB.Execute.ErrorDB.clngOK Then
                    If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound Then
                        insValPolicyLimits = False
                    End If
                Else
                    If .FieldToClass("lblnOk") = 1 Then
                        insValPolicyLimits = False
                    End If
                End If
                .RCloseRec()
            End If
        End With

insValPolicyLimits_Err:
        If Err.Number Then
            insValPolicyLimits = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecValLimits may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecValLimits = Nothing
    End Function

    '%LoadCurr: Lee la moneda de las coberturas de la poliza
    Public Function LoadCurr(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal sTyp_module As String, ByVal sPolitype As String, Optional ByVal bFind As Boolean = False) As Boolean
        Dim llngIndex As Integer
        Dim lrecreaCover_Curr As eRemoteDB.Execute

        On Error GoTo LoadCurr_err

        lrecreaCover_Curr = New eRemoteDB.Execute

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.dEffecdate <> dEffecdate Or Me.nCurrency <> nCurrency Or Me.sTyp_module <> sTyp_module Or Me.sPolitype <> sPolitype Or bFind Then

            With lrecreaCover_Curr
                .StoredProcedure = "reaCover_Curr"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sTyp_module", sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    llngIndex = -1
                    LoadCurr = True
                    mblnCharge = True

                    ReDim marrCurr(50)

                    Do While Not .EOF
                        llngIndex = llngIndex + 1
                        marrCurr(llngIndex) = .FieldToClass("nCurrency")
                        .RNext()
                    Loop

                    .RCloseRec()
                    ReDim Preserve marrCurr(llngIndex)
                Else
                    LoadCurr = False
                End If
            End With

        End If

LoadCurr_err:
        If Err.Number Then
            LoadCurr = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaCover_Curr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCover_Curr = Nothing
    End Function

    '% CurrItem: Carga en las variables de la clase las monedas de las coberturas de la poliza
    Public Function CurrItem(ByVal llngIndex As Integer) As Boolean
        If mblnCharge Then
            If llngIndex <= UBound(marrCurr) Then
                nCurrency = marrCurr(llngIndex)
                CurrItem = True
            Else
                CurrItem = False
            End If
        End If
    End Function

    '% CountCurr: Devuelve el número de las monedas de las coberturas de la poliza
    Public ReadOnly Property CountCurr() As Integer
        Get
            If mblnCharge Then
                CountCurr = UBound(marrCurr)
            Else
                CountCurr = -1
            End If
        End Get
    End Property

    '%bDisabledFer: Verifica si se habilita los campos de modificación
    Public ReadOnly Property bIsAmendment(ByVal nTransaction As Object) As Boolean
        Get
            bIsAmendment = nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent Or nTransaction = Constantes.PolTransac.clngQuotAmendConvertion Or nTransaction = Constantes.PolTransac.clngPropAmendConvertion Or nTransaction = Constantes.PolTransac.clngQuotRenewalConvertion Or nTransaction = Constantes.PolTransac.clngPropRenewalConvertion Or nTransaction = Constantes.PolTransac.clngQuotPropAmendentConvertion Or nTransaction = Constantes.PolTransac.clngQuotPropRenewalConvertion
        End Get
    End Property

    '+ Agregada para vi009, enviado de caracas
    Public ReadOnly Property Count() As Integer
        Get
            Count = UBound(arrCover_surr)
        End Get
    End Property

    '% CountCover: Devuelve el número de coberturas que se encuentran en el arreglo
    Public ReadOnly Property CountCover() As Integer
        Get
            If mblnCharge Then
                CountCover = UBound(marrCover)
            Else
                CountCover = -1
            End If
        End Get
    End Property

    '% InsCalPremium: Esta función cálcula la prima de la cobertura.
    Public Function InsCalPremium(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nTransaction As Integer, ByVal nRetarif As Integer, ByVal nCover_in As Integer, ByVal sRoupremi As String, ByVal nCurrencyOri As Integer, ByVal nCurrencyDes As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal sKey As String, ByVal nPremifix As Double, ByVal npremirat As Double, ByVal nCoverapl As Integer, ByVal dSeektar As Date, ByVal sBrancht As String, ByVal nApply_Perc As Double, ByVal nPremimin As Double, ByVal nPremimax As Double, ByVal nCapital As Double, ByVal nRatecove As Double, ByVal nPremium As Double, ByVal nTypDurins As Integer, ByVal nTypDurpay As Integer, ByVal sExist As String, ByVal sBas_sumins As String, ByVal nDurpay As Integer, ByVal nDurinsur As Integer, ByVal nRateCove_o As Double, ByVal nType_amend As Integer) As Boolean
        Dim lrecInsCalPremium As eRemoteDB.Execute

        On Error GoTo InsCalPremium_Err
        lrecInsCalPremium = New eRemoteDB.Execute
        '+ Definición de store procedure InsCalPremium al 10-24-2002 18:31:32
        With lrecInsCalPremium
            .StoredProcedure = "InsCalPremium"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRetarif", nRetarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover_in", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoupremi", sRoupremi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrencyori", nCurrencyOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrencydes", nCurrencyDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremifix", nPremifix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremirat", npremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCoverapl", nCoverapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dSeektar", dSeektar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nApply_perc", nApply_Perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBas_sumins", sBas_sumins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurins", nTypDurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurpay", nTypDurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExist", sExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDurpay", nDurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDurinsur", nDurinsur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremimin", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremimax", nPremimax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecove", nRatecove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecove_o", nRateCove_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_Table", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInd_Charge", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremirat_m", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_Rat", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeAge", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChange", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChange_user", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsCalPremium = .Run(False)
            Me.nCapital = .Parameters("nCapital").Value
            Me.nRatecove = .Parameters("nRatecove").Value
            Me.nPremium = .Parameters("nPremium").Value
        End With

InsCalPremium_Err:
        If Err.Number Then
            InsCalPremium = False
        End If
        'UPGRADE_NOTE: Object lrecInsCalPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsCalPremium = Nothing
        On Error GoTo 0
    End Function

    '% InsCalCapital: Esta función cálcula el capital de la cobertura.
    Public Function InsCalCapital(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal nCacalfix As Double, ByVal sCacalfri As String, ByVal sCacalili As String, ByVal nCacalcov As Integer, ByVal nCacalper As Integer, ByVal sKey As String, ByVal nRolcap As Integer, ByVal sRoucapit As String, ByVal nRole As Integer, ByVal sClient As String, ByVal sBrancht As String, ByVal nCurrencyOri As Integer, ByVal nCamaxcov As Integer, ByVal nCamaxper As Double, ByVal nCamaxrol As Integer, ByVal nCacalmul As Integer, ByVal nCurrencyDes As Integer, ByVal nGroup As Integer, ByVal nAgeminins As Integer, ByVal nAgemaxins As Integer, ByVal sBas_sumins As String, ByVal nTypDurins As Integer, ByVal nTypDurpay As Integer, ByVal nTransaction As Integer, ByVal nPremium As Double, ByVal nCapital_wait As Double, ByVal nCacalmin As Double, ByVal nCacalmax As Double, ByVal nCapital As Double) As Boolean

        Dim lrecInsCalCapital As eRemoteDB.Execute

        On Error GoTo InsCalCapital_Err
        lrecInsCalCapital = New eRemoteDB.Execute
        '+ Definición de store procedure InsCalPremium al 10-24-2002 18:31:32
        With lrecInsCalCapital
            .StoredProcedure = "InsCalCapital"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalfix", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCacalfri", sCacalfri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCacalili", sCacalili, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalcov", nCacalcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalper", nCacalper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRolcap", nRolcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoucapit", sRoucapit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrencyori", nCurrencyOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCamaxcov", nCamaxcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCamaxper", nCamaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCamaxrol", nCamaxrol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalmul", nCacalmul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrencydes", nCurrencyDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgeminins", nAgeminins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxins", nAgemaxins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBas_sumins", sBas_sumins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurins", nTypDurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurpay", nTypDurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_wait", nCapital_wait, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalmin", nCacalmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalmax", nCacalmax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsCalCapital = .Run(False)
            Me.nCapital = .Parameters("nCapital").Value

        End With

InsCalCapital_Err:
        If Err.Number Then
            InsCalCapital = False
        End If
        'UPGRADE_NOTE: Object lrecInsCalCapital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsCalCapital = Nothing
        On Error GoTo 0
    End Function

    '% Find_Query:
    Public Function Find_Query(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal sClient As String, ByVal sBrancht As String, ByVal nGroup_insu As Integer) As Collection
        Dim lrecreaCoverQuery As eRemoteDB.Execute
        Dim lclsCover As Cover

        On Error GoTo Find_Query_err
        lrecreaCoverQuery = New eRemoteDB.Execute
        Find_Query = New Collection
        With lrecreaCoverQuery
            .StoredProcedure = "reaCoverQuery"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Do While Not .EOF
                    lclsCover = New Cover
                    lclsCover.sExist = .FieldToClass("sExist")
                    lclsCover.sCertype = .FieldToClass("sCertype")
                    lclsCover.nBranch = .FieldToClass("nBranch")
                    lclsCover.nProduct = .FieldToClass("nProduct")
                    lclsCover.nPolicy = .FieldToClass("nPolicy")
                    lclsCover.nCertif = .FieldToClass("nCertif")
                    lclsCover.nGroup_insu = .FieldToClass("nGroup_insu")
                    lclsCover.nModulec = .FieldToClass("nModulec")
                    lclsCover.nCover = .FieldToClass("nCover")
                    lclsCover.dEffecdate = .FieldToClass("dEffecdate")
                    lclsCover.sClient = .FieldToClass("sClient")
                    lclsCover.nRole = .FieldToClass("nRole")
                    lclsCover.nCapital = .FieldToClass("nCapital")
                    lclsCover.nCapitali = .FieldToClass("nCapitali")
                    lclsCover.sChange = .FieldToClass("sChange")
                    lclsCover.sFrandedi = .FieldToClass("sFrandedi")
                    lclsCover.nCurrency = .FieldToClass("nCurrency")
                    lclsCover.nDiscount = .FieldToClass("nDiscount")
                    lclsCover.nFixamount = .FieldToClass("nFixamount")
                    lclsCover.nMaxamount = .FieldToClass("nMaxamount")
                    lclsCover.sFree_premi = .FieldToClass("sFree_premi")
                    lclsCover.nMinamount = .FieldToClass("nMinamount")
                    lclsCover.dNulldate = .FieldToClass("dNulldate")
                    lclsCover.nPremium = .FieldToClass("nPremium")
                    lclsCover.nRate = .FieldToClass("nRate")
                    lclsCover.nWait_quan = .FieldToClass("nWait_quan")
                    lclsCover.nRatecove = .FieldToClass("nRatecove")
                    lclsCover.sWait_type = .FieldToClass("sWait_type")
                    lclsCover.sFrancApl = .FieldToClass("sFrancapl")
                    lclsCover.nDisc_Amoun = .FieldToClass("nDisc_amoun")
                    lclsCover.nTypDurins = .FieldToClass("nTypdurins")
                    lclsCover.nDurinsur = .FieldToClass("nDurinsur")
                    lclsCover.nAgeminins = .FieldToClass("nAgeminins")
                    lclsCover.nAgemaxins = .FieldToClass("nAgemaxins")
                    lclsCover.nAgemaxper = .FieldToClass("nAgemaxper")
                    lclsCover.nTypDurpay = .FieldToClass("nTypdurpay")
                    lclsCover.nDurpay = .FieldToClass("nDurpay")
                    lclsCover.nCauseupd = .FieldToClass("nCauseupd")
                    lclsCover.nCapital_wait = .FieldToClass("nCapital_wait")
                    lclsCover.dAniversary = .FieldToClass("dAniversary")
                    lclsCover.dSeektar = .FieldToClass("dSeektar")
                    lclsCover.dFer = .FieldToClass("dFer")
                    lclsCover.nBranch_rei = .FieldToClass("nBranch_rei")
                    lclsCover.nRetarif = .FieldToClass("nRetarif")
                    lclsCover.nAgeminins = .FieldToClass("nAgemininsm")
                    lclsCover.nAgemaxins = .FieldToClass("nAgemaxinsm")
                    lclsCover.nAgemaxper = .FieldToClass("nAgemaxperm")
                    lclsCover.nAgemininsf = .FieldToClass("nAgemininsf")
                    lclsCover.nAgemaxinsf = .FieldToClass("nAgemaxinsf")
                    lclsCover.nAgemaxperf = .FieldToClass("nAgemaxperf")
                    lclsCover.sDefaulti = .FieldToClass("sDefaulti")
                    lclsCover.sRequired = .FieldToClass("sRequired")
                    lclsCover.sDescript = .FieldToClass("sDescript")

                    lclsCover.sdesc_t5559 = .FieldToClass("sdesc_t5559")
                    lclsCover.sdesc_t64 = .FieldToClass("sdesc_t64")
                    lclsCover.sdesc_t33 = .FieldToClass("sdesc_t33")
                    lclsCover.sdesc_t5589 = .FieldToClass("sdesc_t5589")
                    lclsCover.sdesc_t_pay = .FieldToClass("sdesc_t_pay")
                    lclsCover.sdesc_t52 = .FieldToClass("sdesc_t52")
                    lclsCover.sdesc_t5547 = .FieldToClass("sdesc_t5547")
                    lclsCover.sdesc_t5000 = .FieldToClass("sdesc_t5000")

                    lclsCover.nCapital_req = .FieldToClass("nCapital_req")

                    lclsCover.nRateCla = .FieldToClass("nRateCla")
                    lclsCover.nFixAmoCla = .FieldToClass("nFixAmoCla")
                    lclsCover.nMinAmoCla = .FieldToClass("nMinAmoCla")
                    lclsCover.nMaxAmoCla = .FieldToClass("nMaxAmoCla")
                    lclsCover.nDiscCla = .FieldToClass("nDiscCla")
                    lclsCover.nDisc_AmoCla = .FieldToClass("nDisc_AmoCla")
                    lclsCover.nFrancDays = .FieldToClass("nFrancDays")


                    Find_Query.Add(lclsCover)
                    'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsCover = Nothing
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With

Find_Query_err:
        If Err.Number Then
            'UPGRADE_NOTE: Object Find_Query may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            Find_Query = Nothing
        End If
        'UPGRADE_NOTE: Object lrecreaCoverQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCoverQuery = Nothing
        On Error GoTo 0
    End Function

    'InsPreAM002: Esta función busca si la poliza es por grupo InsPreAM002
    Public Function insPreAM002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer) As Boolean
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsGroups As ePolicy.Groups
        Dim lclsRoles As ePolicy.Roles
        '    Dim lstrClientGE As String
        '    Dim lblnCalcLEG  As Boolean
        '    Dim lstrKey      As String

        On Error GoTo insPreAM002_Err

        insPreAM002 = True
        lclsPolicy = New ePolicy.Policy
        With lclsPolicy
            If .Find(sCertype, nBranch, nProduct, nPolicy) Then
                Me.sTyp_module = .sTyp_module
                Me.sLeg = .sLeg
                Me.nLegAmount = .nLegAmount
                Me.sTypenom = .sTypenom
                If .sPolitype <> "1" And nCertif = 0 Then
                    '+ Si las coberturas son por certificado
                    If .sTyp_module = "4" Or .sTyp_module = "1" Then
                        If .sTyp_module = "4" Then
                            Me.nError = 3932
                            Me.bError = True
                        End If
                        insPreAM002 = False
                    Else
                        '+ Si la especificación es por grupo
                        If .sTyp_module = "3" Then
                            lclsGroups = New ePolicy.Groups
                            Me.nCountGroup = lclsGroups.getCountGroups(sCertype, nBranch, nProduct, nPolicy)
                            '+ Si existen grupos asociados
                            If Me.nCountGroup > 0 Then
                                Me.bFindGroup = True
                            Else
                                '+ Si no existen
                                insPreAM002 = False
                                '+ 3309: Grupo asegurado, no está registrado en la póliza
                                Me.nError = 3887
                                Me.bError = True
                            End If
                            'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lclsGroups = Nothing
                        End If
                    End If
                Else
                    insPreAM002 = False
                End If
            End If
        End With

insPreAM002_Err:
        If Err.Number Then
            insPreAM002 = False
        End If
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        On Error GoTo 0
    End Function

    '%InsPreCA014A: Esta función obtiene los valores iniciales de la CA014A
    Public Function InsPreCA014A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nCurrency As Integer, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal sBrancht As String, ByVal sKey As String, ByVal nUsercode As Integer, ByVal nSessionId As String, ByVal sDelTCover As String, ByVal bQuery As Boolean, ByVal nType_amend As Integer) As Boolean
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsGroups As ePolicy.Groups
        Dim lclsRoles As ePolicy.Roles
        Dim lstrClientGE As String = ""
        Dim lblnCalcLEG As Boolean
        Dim lstrKey As String

        On Error GoTo InsPreCA014A_Err

        InsPreCA014A = True
        lclsPolicy = New ePolicy.Policy
        With lclsPolicy
            If .Find(sCertype, nBranch, nProduct, nPolicy) Then
                Me.sTyp_module = .sTyp_module
                Me.sLeg = .sLeg
                Me.nLegAmount = .nLegAmount
                Me.sTypenom = .sTypenom
                If .sPolitype <> "1" And nCertif = 0 Then
                    '+ Si las coberturas son por certificado
                    If .sTyp_module = "4" Or .sTyp_module = "1" Then
                        If .sTyp_module = "4" Then
                            Me.nError = 3932
                            Me.bError = True
                        End If
                        InsPreCA014A = False
                    Else
                        '+ Si la la especificación es por grupo
                        If .sTyp_module = "3" Then
                            lclsGroups = New ePolicy.Groups
                            Me.nCountGroup = lclsGroups.getCountGroups(sCertype, nBranch, nProduct, nPolicy)
                            '+ Si existen grupos asociados
                            If Me.nCountGroup > 0 Then
                                '+ Si no se indicó un grupo asegurado.
                                If nGroup <= 0 Then
                                    '+ Se obtiene el primero que consiga (información por omisión)
                                    If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                                        Me.nGroup = lclsGroups.nGroup
                                        nGroup = lclsGroups.nGroup
                                    End If
                                Else
                                    Me.nGroup = nGroup
                                End If
                                Me.bFindGroup = True
                            Else
                                '+ Si no existen
                                InsPreCA014A = False
                                '+ 3309: Grupo asegurado, no está registrado en la póliza
                                Me.nError = 3887
                                Me.bError = True
                            End If
                            'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lclsGroups = Nothing
                        End If
                    End If
                Else
                    InsPreCA014A = False
                End If
            End If
        End With

        If InsPreCA014A Then
            InsPreCA014A = InsPreCA014(sCertype, nBranch, nProduct, nPolicy, 0, dEffecdate, nCurrency, nGroup, "CA014A", nUsercode, dNulldate, nTransaction, eRemoteDB.Constants.intNull, String.Empty, sBrancht, sKey, nSessionId, sDelTCover, lclsPolicy, String.Empty, String.Empty, nType_amend)

            If InsPreCA014A And Not bQuery Then
                '+Se valida que se calcule el LEG
                If lclsPolicy.sLeg = "1" And (lclsPolicy.nLegAmount = 0 Or lclsPolicy.nLegAmount = eRemoteDB.Constants.intNull Or nTransaction = Constantes.PolTransac.clngPolicyQuotRenewal Or nTransaction = Constantes.PolTransac.clngPolicyPropRenewal) Then
                    lblnCalcLEG = True
                End If

                '+Se valida que la póliza pertenezca a un grupo empresarial
                lclsRoles = New Roles
                If lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, 0, Roles.eRoles.eRolEnterpriseGroup, String.Empty, dEffecdate) Then

                    '+Si la transacción es cotización de emisión o emisión
                    lstrClientGE = lclsRoles.sClient
                    If nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngPolicyIssue Then

                        '+Si no es la primera póliza del grupo
                        If lclsPolicy.Find_PolicyGE(sCertype, nBranch, nProduct, nPolicy, lstrClientGE, dEffecdate, lclsPolicy.sTyp_module) Then

                            '+Se llama al procedimiento que hereda las condiciones de asegurabilidad de la primera
                            '+póliza del grupo
                            'UPGRADE_NOTE: Object mcolTCovers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            mcolTCovers = Nothing
                            mcolTCovers = New TCovers
                            If sKey = String.Empty Then
                                sKey = mcolTCovers.sKey(nUsercode, nSessionId)
                            End If
                            Me.sKey = sKey
                            InsPreCA014A = mcolTCovers.InsHereditCover_p_g(sCertype, nBranch, nProduct, nPolicy, lclsPolicy.nPolhered, dEffecdate, sKey, lclsPolicy.sTyp_module)

                            Me.nLegAmount = lclsPolicy.nLegAmount
                            lblnCalcLEG = False

                            '+Se valida si aplica el cálculo del LEG para la póliza
                        ElseIf lblnCalcLEG Then
                            lstrKey = sKey
                        End If

                        '+Si la transacción es cotización/propuesta de renovación
                    ElseIf nTransaction = Constantes.PolTransac.clngPolicyQuotRenewal Or nTransaction = Constantes.PolTransac.clngPolicyPropRenewal Then

                        '+Se valida si aplica el cálculo del LEG para la póliza
                        If lblnCalcLEG Then
                            lblnCalcLEG = False
                            If lclsPolicy.InsCalLegAmount(sCertype, nBranch, nProduct, nPolicy, lclsPolicy.sTypenom, lclsRoles.sClient, dEffecdate, mclsCurren_pol.nCurrency, Me.sKey) Then
                                Me.nLegAmount = lclsPolicy.nLegAmount
                            End If
                        End If
                    End If
                End If
                '+Se valida si aplica el cálculo del LEG para la póliza, cuando no tiene grupo empresarial
                If lblnCalcLEG Then
                    If lclsPolicy.InsCalLegAmount(sCertype, nBranch, nProduct, nPolicy, lclsPolicy.sTypenom, lstrClientGE, dEffecdate, mclsCurren_pol.nCurrency, Me.sKey) Then
                        Me.nLegAmount = lclsPolicy.nLegAmount
                    End If
                End If
            End If
        End If

        If nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngRecuperation Or nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngPolicyReissue Then
            Me.bTransaction = True
        End If

InsPreCA014A_Err:
        If Err.Number Then
            InsPreCA014A = False
        End If
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        On Error GoTo 0
    End Function

    '%InsPreCA014: Esta función obtiene los valores iniciales de la CA014
    Public Function InsPreCA014(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nGroup As Integer, ByVal sCodispl As String, ByVal nUsercode As Integer, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal sBrancht As String, ByVal sKey As String, ByVal nSessionId As String, ByVal sDelTCover As String, ByVal lclsPolicy As Policy, ByVal sRecPopup As String, ByVal sSche_Code As String, ByVal nType_amend As Integer) As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsGroups As ePolicy.Groups
        Dim lclsLife As ePolicy.Life
        Dim lclsSecur_sche As eSecurity.Secur_sche
        Dim lblnQuery As Boolean
        Dim lclsCertif As ePolicy.Certificat

        On Error GoTo InsPreCA014_Err

        InsPreCA014 = True
        lblnQuery = nTransaction = Constantes.PolTransac.clngPolicyQuery Or nTransaction = Constantes.PolTransac.clngCertifQuery Or nTransaction = Constantes.PolTransac.clngQuotationQuery Or nTransaction = Constantes.PolTransac.clngProposalQuery Or nTransaction = Constantes.PolTransac.clngQuotAmendentQuery Or nTransaction = Constantes.PolTransac.clngPropAmendentQuery Or nTransaction = Constantes.PolTransac.clngQuotRenewalQuery Or nTransaction = Constantes.PolTransac.clngPropRenewalQuery

        Me.nGroup = nGroup

        If Not lblnQuery Then
            If lclsPolicy Is Nothing Then
                lclsPolicy = New Policy
                With lclsPolicy
                    If .Find(sCertype, nBranch, nProduct, nPolicy) Then
                        Me.sTyp_module = .sTyp_module
                        Me.sLeg = .sLeg
                        Me.sTypenom = .sTypenom
                        bNopayroll = .sNopayroll = "1"
                        If .sPolitype <> "1" And nCertif > 0 Then
                            '+ Si la especificación es por grupo
                            If .sTyp_module = "3" Then
                                lclsGroups = New ePolicy.Groups
                                Me.nCountGroup = lclsGroups.getCountGroups(sCertype, nBranch, nProduct, nPolicy)
                                '+ Si existen grupos asociados
                                If Me.nCountGroup > 0 Then
                                    '+ Si no se indicó un grupo asegurado.
                                    If nGroup <= 0 Then
                                        '+ Se obtiene el primero que consiga (información por omisión)
                                        If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                                            Me.nGroup = lclsGroups.nGroup
                                            nGroup = lclsGroups.nGroup
                                        End If
                                    Else
                                        Me.nGroup = nGroup
                                    End If
                                    Me.bFindGroup = True
                                End If
                                'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                lclsGroups = Nothing
                            End If
                        End If
                    Else
                        InsPreCA014 = False
                    End If
                End With
            End If
        Else
            'Si es consulta se obtiene el Grupo según la fecha de la misma.
            If sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Then
                lclsLife = New Life
                If lclsLife.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                    Me.nGroup = lclsLife.nGroup
                End If
                'UPGRADE_NOTE: Object lclsLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsLife = Nothing
            Else
                lclsCertif = New ePolicy.Certificat
                Call lclsCertif.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
                Me.nGroup = lclsCertif.nGroup
                'UPGRADE_NOTE: Object lclsCertif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsCertif = Nothing
            End If
        End If

        '+ Se obtiene las monedas asociadas a la póliza
        mclsCurren_pol = New Curren_pol
        With mclsCurren_pol
            If .Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
                If nCurrency > 0 Then
                    Me.nCurrency = nCurrency
                    .nCurrency = nCurrency
                Else
                    Me.nCurrency = 0
                    If .IsLocal Then
                        Me.nCurrency = 1
                    Else
                        Call .Val_Curren_pol(0)
                    End If
                    Me.nCurrency = .nCurrency
                End If
                Me.nCountCurrency = .CountCurrenPol + 1
            Else
                InsPreCA014 = False
                '+ 3738: La póliza no tiene monedas asignadas
                Me.nError = 3738
                Me.bError = True
            End If
        End With

        '+ Si las condiciones mínimas se cumplen se efectúa la búsqueda de la información a procesar
        mclsRoles = New Roles
        If InsPreCA014 Then
            If sCodispl = "CA014" Then
                '+Si la póliza no es de vida se busca el asegurado
                If sBrancht <> CStr(eProduct.Product.pmBrancht.pmlife) And nRole = eRemoteDB.Constants.intNull Then
                    nRole = 2
                End If

                '+ Se obtienen los datos asociados al cliente
                With mclsRoles
                    If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nRole, sClient, dEffecdate) Then

                        '+Si la póliza es de vida se calcula la edad real y actuarial del cliente
                        If sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Then
                            Call .CalInsuAge(nBranch, nProduct, dEffecdate, .dBirthdate, .sSexclien, .sSmoking, .nRole)
                        Else
                            '+Si la póliza no es de vida se asocia al cliente el código del asegurado
                            If sClient = String.Empty Then
                                sClient = .sClient
                            End If
                        End If
                    End If
                End With

                If Not lblnQuery Then
                    If sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Then
                        '+ Se obtiene la clase del producto de vida
                        lclsProduct = New eProduct.Product
                        If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
                            nProdClas = lclsProduct.nProdClas

                        End If
                        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsProduct = Nothing
                    End If
                End If

                If Not lblnQuery Then
                    '+ Se obtiene el estado de los campos según nivel del usuario
                    bDisabledByLevels = False
                    lclsProduct = New eProduct.Product
                    lclsSecur_sche = New eSecurity.Secur_sche
                    If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
                        If lclsSecur_sche.GetLevelsByTransac(sSche_Code, "2", sCodispl) Then
                            If lclsSecur_sche.nAmelevel <= lclsProduct.nChUserLev Then
                                bDisabledByLevels = True
                            End If
                        End If
                    End If
                    'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsProduct = Nothing
                    'UPGRADE_NOTE: Object lclsSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsSecur_sche = Nothing

                End If
            End If

            '+ Se llama el procedimiento que realiza el cálculo de las coberturas
            If mcolTCovers Is Nothing Then
                mcolTCovers = New TCovers
            End If
            If sKey = String.Empty Then
                sKey = mcolTCovers.sKey(nUsercode, nSessionId)
            End If
            Me.sKey = sKey
            InsPreCA014 = mcolTCovers.FindCoverPolicy(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, Me.nCurrency, IIf(Me.nGroup = eRemoteDB.intNull, nGroup, Me.nGroup), sCodispl, nUsercode, dNulldate, nTransaction, nRole, sClient, sBrancht, nProdClas, Me.sKey, sDelTCover, lclsPolicy, sRecPopup, , nType_amend)
            Me.nLegAmount = mcolTCovers.nLegAmount
        End If

InsPreCA014_Err:
        If Err.Number Then
            InsPreCA014 = False
        End If
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        On Error GoTo 0
    End Function


    '%insValCa014: Este metodo se encarga realizar las validaciones masivas correspondientes a la
    '%ventana de coberturas (CA014).
    Public Function InsValCA014(ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nGroup As Integer, ByVal nTransaction As Integer, ByVal sCodispl As String, ByVal nRole As Integer, ByVal sClient As String, ByVal sBrancht As String, ByVal nAge As Integer, ByVal nProdClas As Integer, ByVal sSche_code_user As String, ByVal sCopy As String) As String
        Dim lrecinsValca014 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        On Error GoTo insValca014_Err

        lrecinsValca014 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecinsValca014
            .StoredProcedure = "insca014pkg.insValca014"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdclas", nProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSche_code_user", sSche_code_user, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCharge", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCopy", sCopy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , , , , , lstrError)
                    InsValCA014 = .Confirm()
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing

            End If
        End With

insValca014_Err:
        If Err.Number Then
            InsValCA014 = "insValca014: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lrecinsValca014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValca014 = Nothing
        On Error GoTo 0

    End Function

    '%InsValCA014DB01: Este metodo se encarga de realizar las validaciones que son accesando la BD
    '%                 descritas en el funcional de la ventana "CA014"
    Private Function InsValCA014DB01(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer) As String
        Dim lrecInsValCA014DB01 As eRemoteDB.Execute
        On Error GoTo InsValCA014DB01_Err
        lrecInsValCA014DB01 = New eRemoteDB.Execute
        With lrecInsValCA014DB01
            .StoredProcedure = "InsValCA014DB01"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                InsValCA014DB01 = .Parameters("Arrayerrors").Value
            End If
        End With

InsValCA014DB01_Err:
        If Err.Number Then
            InsValCA014DB01 = "InsValCA014DB01: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecInsValCA014DB01 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValCA014DB01 = Nothing
        On Error GoTo 0
    End Function


    '%InsValCA014DB02: Este metodo se encarga de realizar las validaciones que son accesando la BD
    '%                 descritas en el funcional de la ventana "CA014"
    Private Function InsValCA014DB02(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal nCapital As Double, ByVal ldblCaCalMax As Integer) As String
        Dim lrecInsValCA014DB02 As eRemoteDB.Execute

        On Error GoTo InsValCA014DB02_Err

        lrecInsValCA014DB02 = New eRemoteDB.Execute

        With lrecInsValCA014DB02
            .StoredProcedure = "InsValCA014DB02"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ldblCaCalMax", ldblCaCalMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                InsValCA014DB02 = .Parameters("Arrayerrors").Value
            End If
        End With

InsValCA014DB02_Err:
        If Err.Number Then
            InsValCA014DB02 = "InsValCA014DB02: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecInsValCA014DB02 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValCA014DB02 = Nothing
    End Function

    '%InsValCA014DB03: Este metodo se encarga de realizar las validaciones que son accesando la BD
    '%                 descritas en el funcional de la ventana "CA014"
    Private Function InsValCA014DB03(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
        Dim lrecInsValCA014DB03 As eRemoteDB.Execute
        On Error GoTo InsValCA014DB03_Err
        lrecInsValCA014DB03 = New eRemoteDB.Execute
        With lrecInsValCA014DB03
            .StoredProcedure = "InsValCA014DB03"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                InsValCA014DB03 = .Parameters("Arrayerrors").Value
            End If
        End With

InsValCA014DB03_Err:
        If Err.Number Then
            InsValCA014DB03 = "InsValCA014DB03: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecInsValCA014DB03 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValCA014DB03 = Nothing
        On Error GoTo 0
    End Function





    '%insValCal712: Valida los datos para el reporte de polizas con coberturas de servicios a terceros
    Public Function InsValCAL712(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dEffecdate As Date, ByVal nCovergen As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsValues As eFunctions.Values

        On Error GoTo InsValCAL712_err

        lobjErrors = New eFunctions.Errors
        lclsValues = New eFunctions.Values

        If nInsur_area = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 55031)
        End If

        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 2056)
        End If

        If nCovergen = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 55537, , eFunctions.Errors.TextAlign.LeftAling, "Cobertura genérica")
        Else
            With lclsValues
                .Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If Not .IsValid("tabCoverProvider", nCovergen, True) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 11007)
                End If
            End With
        End If

        InsValCAL712 = lobjErrors.Confirm()

InsValCAL712_err:
        If Err.Number Then
            InsValCAL712 = Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        On Error GoTo 0
    End Function

    '%GetCapitalOther: Obtiene el capital de la cobertura cuando depende de otra
    Public Function GetCapitalOther(ByVal nCacalcov As Integer, ByVal nCacalper As Double, ByVal npremirat As Double, ByVal nProcess As Integer, ByVal nRatecove As Double, ByVal sKey As String, ByVal nRolcap As Integer, ByVal nPremium As Double, ByVal nCapital As Double, ByVal nModulec As Integer) As Boolean
        Dim lrecReaCoverOther As eRemoteDB.Execute
        On Error GoTo reaCoverother_Err

        '+ Definición de store procedure reaCoverother al 11-05-2002 11:39:40
        lrecReaCoverOther = New eRemoteDB.Execute
        With lrecReaCoverOther
            .StoredProcedure = "reaCoverother"
            .Parameters.Add("nCacalcov", nCacalcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalper", nCacalper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremirat", npremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProcess", nProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecove", nRatecove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRolcap", nRolcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            GetCapitalOther = .Run(False)
            nCapital = .Parameters("nCapital").Value
            nPremium = .Parameters("nPremium").Value
        End With

reaCoverother_Err:
        If Err.Number Then
            GetCapitalOther = False
        End If
        'UPGRADE_NOTE: Object lrecReaCoverOther may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaCoverOther = Nothing
        On Error GoTo 0
    End Function

    '%InsValCA014: Esta rutina realiza la validacion de los datos de frecuencia de pago permitidas
    Public Function InsValCA014Upd(ByVal sCodispl As String,
                                   ByVal sCertype As String,
                                   ByVal nBranch As Integer,
                                   ByVal nProduct As Integer,
                                   ByVal nPolicy As Double,
                                   ByVal nCertif As Double,
                                   ByVal dEffecdate As Date,
                                   ByVal nModulec As Integer,
                                   ByVal nCover As Integer,
                                   ByVal nCapital As Double,
                                   ByVal nRatecove As Double,
                                   ByVal nPremium As Double,
                                   ByVal nCurrency As Integer,
                                   ByVal nGroup As Integer,
                                   ByVal nTransaction As Integer,
                                   ByVal sFrandedi As String,
                                   ByVal sFrancApl As String,
                                   ByVal nRate As Double,
                                   ByVal nFixamount As Double,
                                   ByVal nMinamount As Double,
                                   ByVal nWait_Type As Integer,
                                   ByVal nCapital_o As Double,
                                   ByVal nRateCove_o As Double,
                                   ByVal nPremium_o As Double,
                                   ByVal nMaxamount As Double,
                                   ByVal nDiscount As Double,
                                   ByVal nDisc_Amoun As Double,
                                   ByVal nRole As Integer,
                                   ByVal sBrancht As String,
                                   ByVal nWait_quan As Integer,
                                   ByVal nAge As Integer,
                                   ByVal nAgeminins As Integer,
                                   ByVal nAgemaxins As Integer,
                                   ByVal nAgemaxper As Integer,
                                   ByVal sClient As String,
                                   ByVal nCauseupd As Integer,
                                   ByVal nProdClas As Integer,
                                   ByVal sKey As String,
                                   ByVal nAgemininsf As Integer,
                                   ByVal nAgemaxinsf As Integer,
                                   ByVal nAgemaxperf As Integer,
                                   ByVal nBranch_rei As Integer,
                                   ByVal nDurinsur As Integer,
                                   ByVal nTypDurins As Integer,
                                   ByVal sExist As String,
                                   ByVal nRateCla As Double,
                                   ByVal nFixAmoCla As Double,
                                   ByVal nMinAmoCla As Double,
                                   ByVal nMaxAmoCla As Double,
                                   ByVal nDiscCla As Double,
                                   ByVal nDisc_AmoCla As Double,
                                   ByVal nFrancDays As Double,
                                   Optional ByVal llngLine As Integer = 0,
                                   Optional ByRef lclsErrors As eFunctions.Errors = Nothing,
                                   Optional ByVal bMassive As Boolean = False,
                                   Optional ByVal nDataFound As Integer = 0,
                                   Optional ByVal sAction As String = "",
                                   Optional ByVal sChange As String = "",
                                   Optional ByVal sSche_code_user As String = "",
                                   Optional ByVal sSexclien As String = "",
                                   Optional ByVal nTyperisk As Integer = 0,
                                   Optional ByVal nTyp_AgeMinM As Integer = 0,
                                   Optional ByVal nTyp_AgeMinF As Integer = 0,
                                   Optional ByVal nPremimax As Double = 0,
                                   Optional ByVal nPremimin As Double = 0,
                                   Optional ByVal nCacalmax As Double = 0,
                                   Optional ByVal nCacalmin As Double = 0) As String
        Dim lrecinsValca014upd As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        On Error GoTo insValca014upd_Err

        lrecinsValca014upd = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014upd al 06-27-2003 15:30:42
        '+
        With lrecinsValca014upd
            .StoredProcedure = "insca014pkg.insValca014upd"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecove", nRatecove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFrandedi", sFrandedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFrancapl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFixamount", nFixamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWait_type", nWait_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_o", nCapital_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecove_o", nRateCove_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium_o", nPremium_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisc_amoun", nDisc_Amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWait_quan", nWait_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgeminins", nAgeminins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxins", nAgemaxins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxper", nAgemaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCauseupd", nCauseupd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdclas", nProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemininsf", nAgemininsf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxinsf", nAgemaxinsf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxperf", nAgemaxperf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDurinsur", nDurinsur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurins", nTypDurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExist", sExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("lLngline", llngLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDatafound", nDataFound, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChange", sChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSche_code_user", sSche_code_user, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("aRrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCharge", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_AgeMinM", nTyp_AgeMinM, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_AgeMinF", nTyp_AgeMinF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nPremimax", nPremimax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremimin", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalmax", nCacalmax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalmin", nCacalmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nRateCla", nRateCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 4, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFixAmoCla", nFixAmoCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinAmoCla", nMinAmoCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaxAmoCla", nMaxAmoCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDiscCla", nDiscCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 4, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisc_AmoCla", nDisc_AmoCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 18, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancDays", nFrancDays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 18, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)

            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage(sCodispl, , llngLine, , , , lstrError)
                    If Not bMassive Then
                        InsValCA014Upd = .Confirm()
                    End If
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing

            End If

        End With

insValca014upd_Err:
        If Err.Number Then
            InsValCA014Upd = "insValca014upd: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lrecinsValca014upd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValca014upd = Nothing
        On Error GoTo 0

    End Function

    '%InsPostCA014Upd: Funcion que realiza las actualización en la BD según especificaciones
    '%                 funcionales
    Public Function InsPostCA014Upd(ByVal sCodispl As String, ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCurrency As Integer, ByVal nCapital As Double, ByVal nRatecove As Double, ByVal nPremium As Double, ByVal nCover As Integer, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal sFrandedi As String, ByVal sWait_type As String, ByVal sFrancApl As String, ByVal nDisc_Amoun As Double, ByVal nRate As Double, ByVal sChange As String, ByVal nCapitali As Double, ByVal nDiscount As Double, ByVal nFixamount As Double, ByVal nMaxamount As Double, ByVal nMinamount As Double, ByVal nWait_quan As Integer, ByVal nCapital_wait As Double, ByVal nAgeminins As Integer, ByVal nAgemaxins As Integer, ByVal nAgemaxper As Integer, ByVal nTypDurins As Integer, ByVal nDurinsur As Integer, ByVal nTypDurpay As Integer, ByVal nDurpay As Integer, ByVal sDefaulti As String, ByVal nRole As Integer, ByVal sClient As String, ByVal nCapital_o As Double, ByVal nRateCove_o As Double, ByVal nPremium_o As Double, ByVal nAgemininsf As Integer, ByVal nAgemaxinsf As Integer, ByVal nAgemaxperf As Integer, ByVal sRequirec As String, ByVal sDefaultic As String, ByVal nBranch_rei As Integer, ByVal nRetarif As Integer, ByVal nCauseupd As Integer, ByVal dFer As Date, ByVal sPolitype As String, ByVal nCapital_req As Double, ByVal nRateCla As Double, ByVal nFixAmoCla As Double, ByVal nMinAmoCla As Double, ByVal nMaxAmoCla As Double, ByVal nDiscCla As Double, ByVal nDisc_AmoCla As Double, ByVal nFrancDays As Double, Optional ByVal sAction As String = "", Optional ByVal nPremimax As Double = 0, Optional ByVal nPremimin As Double = 0, Optional ByVal nCacalmax As Double = 0, Optional ByVal nCacalmin As Double = 0, Optional ByVal nTyp_AgeMinM As Double = 0, Optional ByVal nTyp_AgeMinF As Double = 0) As Boolean
        Dim lclsTCover As TCover

        On Error GoTo InsPostCA014Upd_Err
        lclsTCover = New TCover

        With lclsTCover
            If .Find(sKey, sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, nModulec, nCover, nRole, sClient) Then
                .sKey = sKey
                .sCertype = sCertype
                .nBranch = nBranch
                .nProduct = nProduct
                .nPolicy = nPolicy
                .nCertif = nCertif
                .nCurrency = nCurrency
                .nCapital = nCapital
                .nRatecove = nRatecove
                .nModulec = IIf(nModulec <= 0, 0, nModulec)
                .nGroup = IIf(nGroup <= 0, 0, nGroup)
                .nPremium = nPremium
                .nCover = nCover
                .nRole = nRole
                .sClient = sClient
                .sFrandedi = IIf(sFrandedi = "0", String.Empty, sFrandedi)
                .sWait_type = IIf(sWait_type = "0", String.Empty, sWait_type)
                .sFrancApl = IIf(sFrancApl = "0", String.Empty, sFrancApl)
                .nDisc_Amoun = nDisc_Amoun
                .nRate = nRate
                .sChange = sChange
                .nCapitali = nCapitali
                .nDiscount = nDiscount
                .nFixamount = nFixamount
                .nMaxamount = nMaxamount
                .nMinamount = nMinamount
                .nWait_quan = nWait_quan
                .nCapital_wait = nCapital_wait
                .nAgeminins = nAgeminins
                .nAgemaxins = nAgemaxins
                .nAgemaxper = nAgemaxper
                If sPolitype = "1" Or nCertif > 0 Then
                    .nAgemininsf = nAgeminins
                    .nAgemaxinsf = nAgemaxins
                    .nAgemaxperf = nAgemaxper
                Else
                    .nAgemininsf = nAgemininsf
                    .nAgemaxinsf = nAgemaxinsf
                    .nAgemaxperf = nAgemaxperf
                End If
                .nTypDurins = nTypDurins
                .nDurinsur = nDurinsur
                .nTypDurpay = nTypDurpay
                .nDurpay = nDurpay
                If sAction = "Del" Then
                    .sDefaulti = "9"
                Else
                    .sDefaulti = sDefaulti
                End If
                .nCapital_o = nCapital_o
                .nRateCove_o = nRateCove_o
                .nPremium_o = nPremium_o
                .sCodispl = sCodispl
                .sRequirec = IIf(sRequirec = "", "2", sRequirec)
                .sDefaultic = sDefaultic
                .nBranch_rei = nBranch_rei
                .nRetarif = nRetarif
                .nCauseupd = nCauseupd
                .dFer = dFer
                .nPremimax = nPremimax
                .nPremimin = nPremimin
                .nCacalmax = nCacalmax
                .nCacalmin = nCacalmin
                .nTyp_AgeMinM = nTyp_AgeMinM
                .nTyp_AgeMinF = nTyp_AgeMinM
                .nCapital_req = nCapital_req
                .nRateCla = nRateCla
                .nFixAmoCla = nFixAmoCla
                .nMinAmoCla = nMinAmoCla
                .nMaxAmoCla = nMaxAmoCla
                .nDiscCla = nDiscCla
                .nDisc_AmoCla = nDisc_AmoCla
                .nFrancDays = nFrancDays
                InsPostCA014Upd = .Update
            End If
        End With

InsPostCA014Upd_Err:
        If Err.Number Then
            InsPostCA014Upd = False
        End If
        'UPGRADE_NOTE: Object lclsTCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTCover = Nothing
        On Error GoTo 0
    End Function

    '%InsUpdCA014. Esta funcion se encarga de realizar la actualización de la tabla de coberturas
    Private Function InsUpdCA014(ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nTransaction As Integer, ByVal dNulldate As Date, ByVal nRole As Integer, ByVal sClient As String, ByVal sBrancht As String, ByVal nProdClas As Integer, ByVal nUsercode As Integer, ByVal nLegAmount As Double, ByVal nDataFound As Byte, ByVal bUpdCover As Boolean) As Boolean
        Dim lrecInsPostCA014 As eRemoteDB.Execute

        On Error GoTo InsUpdCA014_Err
        '+ Definición de store procedure InsPostCA014 al 11-07-2002 15:34:02
        lrecInsPostCA014 = New eRemoteDB.Execute
        With lrecInsPostCA014
            .StoredProcedure = "InsPostCA014"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdclas", nProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLegamount", nLegAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDataFound", nDataFound, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDelTCover", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpdcover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSequence", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndexcover", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCharge", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsUpdCA014 = .Run(False)
            bUpdCover = .Parameters("nUpdcover").Value = 1
        End With

InsUpdCA014_Err:
        If Err.Number Then
            InsUpdCA014 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostCA014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCA014 = Nothing
        On Error GoTo 0
    End Function

    '%InsPostCA014. Esta funcion se encarga de realizar la actualización de la tabla de coberturas
    Public Function InsPostCA014(ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nTransaction As Integer, ByVal dNulldate As Date, ByVal nRole As Integer, ByVal sClient As String, ByVal sBrancht As String, ByVal nProdClas As Integer, ByVal nUsercode As Integer, ByVal sCodispl As String, Optional ByVal sIndexCover As String = "", Optional ByVal nLegAmount As Double = 0, Optional ByVal nDataFound As Integer = 0, Optional ByVal nTyp_AgeMinM As Integer = 0, Optional ByVal nTyp_AgeMinF As Integer = 0) As Boolean
        Dim lblnUpdCover As Boolean
        Dim lrecInsPostCA014 As eRemoteDB.Execute
        Dim lclsCertificat As ePolicy.Certificat = New ePolicy.Certificat
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsPolicyWin As ePolicy.Policy_Win
        On Error GoTo InsPostCA014_Err

        nGroup = IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup)

        lrecInsPostCA014 = New eRemoteDB.Execute
        With lrecInsPostCA014
            .StoredProcedure = "InsPostCA014"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdclas", nProdClas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLegamount", nLegAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDataFound", nDataFound, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDelTCover", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpdcover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSequence", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndexCover", sIndexCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCharge", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_AgeMinM", nTyp_AgeMinM, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_AgeMinF", nTyp_AgeMinF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            InsPostCA014 = .Parameters("nUpdcover").Value = 1

            If InsPostCA014 Then
                lclsPolicyWin = New ePolicy.Policy_Win
                lclsPolicy = New ePolicy.Policy
                lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
                lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
                If lclsCertificat.sInd_Multiannual = "1" And lclsCertificat.nDepreciationTable <> 0 And ((lclsPolicy.sPolitype = "2" And nCertif > 0) Or lclsPolicy.sPolitype = "1") Then
                    InsPostCA014 = lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA054", "3")
                End If
            End If
        End With

InsPostCA014_Err:
        If Err.Number Then
            InsPostCA014 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostCA014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCA014 = Nothing
        On Error GoTo 0
    End Function
    '%InsPostCA014. Esta funcion se encarga de realizar la actualización de la tabla de coberturas
    Public Function InsPostCA014Copy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nUsercode As Integer, ByVal nTransaction As Integer) As Boolean
        Dim lblnUpdCover As Boolean
        Dim lrecInsPostCA014Copy As eRemoteDB.Execute

        On Error GoTo InsPostCA014Copy_Err

        lrecInsPostCA014Copy = New eRemoteDB.Execute
        With lrecInsPostCA014Copy
            .StoredProcedure = "InsCopyCA014"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpdcover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            InsPostCA014Copy = .Parameters("nUpdcover").Value = 1

        End With

InsPostCA014Copy_Err:
        If Err.Number Then
            InsPostCA014Copy = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostCA014Copy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCA014Copy = Nothing
        On Error GoTo 0
    End Function

    '%insFieldValid: Verifica si el valor del campo es válido
    Private Function insFieldValid(ByVal ldblAmount As Double, Optional ByVal onlyNull As Boolean = False) As Boolean
        If onlyNull Then
            insFieldValid = (ldblAmount <> eRemoteDB.Constants.intNull)
        Else
            insFieldValid = (ldblAmount <> 0 And ldblAmount <> eRemoteDB.Constants.intNull)
        End If
    End Function

    '%insCalVar: Procedimiento que calcula la variacion entre dos numeros.
    Private Function InsCalVar(ByVal nMonto100 As Double, ByVal nMontoX As Double) As Double
        If nMonto100 <> 0 Then
            InsCalVar = (nMontoX * 100 / nMonto100) - 100
        Else
            InsCalVar = 0
        End If
    End Function

    '%insUpdNullCover: Esta función se encarga de anular el certificado en caso de que sea una póliza colectiva.
    Public Function insUpdNullCover(ByVal nCover As Integer) As Boolean
        Dim lrecupdCover_null As eRemoteDB.Execute

        lrecupdCover_null = New eRemoteDB.Execute

        On Error GoTo insUpdNullCover_Err

        insUpdNullCover = True

        With lrecupdCover_null
            .StoredProcedure = "updCover_null"
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dRescuedate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdNullCover = .Run(False)

        End With
        'UPGRADE_NOTE: Object lrecupdCover_null may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdCover_null = Nothing

insUpdNullCover_Err:
        If Err.Number Then
            insUpdNullCover = False
        End If
        On Error GoTo 0
    End Function

    '%InsUpdNullLifeCovers: Esta función se encarga de anular las coberturas asociadas a vida de la poliza
    Public Function InsUpdNullLifeCovers() As Boolean
        Dim lrecUpdLifecovers_null As eRemoteDB.Execute

        On Error GoTo InsUpdNullLifeCovers_Err

        lrecUpdLifecovers_null = New eRemoteDB.Execute
        With lrecUpdLifecovers_null
            .StoredProcedure = "UpdLifecovers_null"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsUpdNullLifeCovers = .Run(False)
        End With

InsUpdNullLifeCovers_Err:
        If Err.Number Then
            InsUpdNullLifeCovers = False
        End If
        'UPGRADE_NOTE: Object lrecUpdLifecovers_null may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdLifecovers_null = Nothing
        On Error GoTo 0
    End Function

    '%insCreCover: Esta función se encarga de agregar un registro en la tabla Cover en caso de
    '%             que se haya hecho un rescate parcial y la cobertura no quede totalmente
    '%             anulada.
    Public Function insCreCover(ByVal nCover As Integer, ByVal nCapital As Double) As Boolean
        Dim lreccreCover_2 As eRemoteDB.Execute

        lreccreCover_2 = New eRemoteDB.Execute

        On Error GoTo insCreCover_Err

        With lreccreCover_2
            .StoredProcedure = "creCover_2"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dRescuedate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insCreCover = .Run(False)
        End With
        'UPGRADE_NOTE: Object lreccreCover_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreCover_2 = Nothing

insCreCover_Err:
        If Err.Number Then
            insCreCover = False
        End If
        On Error GoTo 0
    End Function

    '% InsGetOtherPolCover: Valida que el cliente no tenga asociada la cobertura en otra póliza
    Private Function InsGetOtherPolCover(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCover As Integer, ByVal sClient As String, ByVal nModulec As Integer) As String
        Dim lrecReaCoverOther As eRemoteDB.Execute

        On Error GoTo InsGetOtherPolCover_Err
        lrecReaCoverOther = New eRemoteDB.Execute
        With lrecReaCoverOther
            .StoredProcedure = "ReaCoverother_by_client"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolicys", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            InsGetOtherPolCover = .Parameters("sPolicys").Value
        End With

InsGetOtherPolCover_Err:
        If Err.Number Then
            InsGetOtherPolCover = String.Empty
        End If
        'UPGRADE_NOTE: Object lrecReaCoverOther may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaCoverOther = Nothing
        On Error GoTo 0
    End Function

    '% Count_By_Role: Obtiene la cantidad de cliente por role de una póliza
    Public Function Count_By_Role(ByVal sKey As String, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As Integer
        Dim lrecreatcover As eRemoteDB.Execute

        On Error GoTo Count_By_Role_Err

        lrecreatcover = New eRemoteDB.Execute

        With lrecreatcover
            .StoredProcedure = "ReaTcover_Count_by_role"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Count_By_Role = .FieldToClass("nCount")
                .RCloseRec()
            End If
        End With
Count_By_Role_Err:
        If Err.Number Then
            Count_By_Role = 0
        End If
        'UPGRADE_NOTE: Object lrecreatcover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreatcover = Nothing
        On Error GoTo 0
    End Function

    '% InitValues: Se controla la creación de cada instancia de la clase
    Private Sub InitValues()
        sCertype = String.Empty
        nBranch = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nPolicy = eRemoteDB.Constants.intNull
        nCertif = eRemoteDB.Constants.intNull
        nGroup_insu = 0
        nModulec = eRemoteDB.Constants.intNull
        nCover = eRemoteDB.Constants.intNull
        dEffecdate = eRemoteDB.Constants.dtmNull
        sClient = String.Empty
        nCapital = eRemoteDB.Constants.intNull
        nCapitali = eRemoteDB.Constants.intNull
        sChange = String.Empty
        sFrandedi = String.Empty
        nCurrency = eRemoteDB.Constants.intNull
        nDiscount = eRemoteDB.Constants.intNull
        nFixamount = eRemoteDB.Constants.intNull
        nMaxamount = eRemoteDB.Constants.intNull
        sFree_premi = String.Empty
        nMinamount = eRemoteDB.Constants.intNull
        dNulldate = eRemoteDB.Constants.dtmNull
        nPremium = eRemoteDB.Constants.intNull
        nRate = eRemoteDB.Constants.intNull
        nWait_quan = eRemoteDB.Constants.intNull
        nRatecove = eRemoteDB.Constants.intNull
        nUsercode = eRemoteDB.Constants.intNull
        sWait_type = String.Empty
        sFrancApl = String.Empty
        nDisc_Amoun = eRemoteDB.Constants.intNull
        nTypDurins = eRemoteDB.Constants.intNull
        nDurinsur = eRemoteDB.Constants.intNull
        nAgeminins = eRemoteDB.Constants.intNull
        nAgemaxins = eRemoteDB.Constants.intNull
        nAgemaxper = eRemoteDB.Constants.intNull
        nTypDurpay = eRemoteDB.Constants.intNull
        nDurpay = eRemoteDB.Constants.intNull
        nCauseupd = eRemoteDB.Constants.intNull
        nCapital_wait = eRemoteDB.Constants.intNull
        nAgelimit = eRemoteDB.Constants.intNull
        nAge_per = eRemoteDB.Constants.intNull
        dAniversary = eRemoteDB.Constants.dtmNull
        dSeektar = eRemoteDB.Constants.dtmNull
        dFer = eRemoteDB.Constants.dtmNull
        nBranch_rei = eRemoteDB.Constants.intNull
        sTyp_module = String.Empty
        sPolitype = String.Empty
        nGroup = eRemoteDB.Constants.intNull
        nAction = eRemoteDB.Constants.intNull
        sTransaction = String.Empty
        sExist = String.Empty
        sDefaulti = String.Empty
        sDescript = String.Empty
        sRequired = String.Empty
        sCacalili = String.Empty
        sCh_typ_cap = String.Empty
        nRatecapadd = eRemoteDB.Constants.intNull
        nRatecapsub = eRemoteDB.Constants.intNull
        nCover_in = eRemoteDB.Constants.intNull
        nRatepreadd = eRemoteDB.Constants.intNull
        nRatepresub = eRemoteDB.Constants.intNull
        sChange_typ = String.Empty
        sFdrequire = String.Empty
        nPremifix = eRemoteDB.Constants.intNull
        npremirat = eRemoteDB.Constants.intNull
        nCoverapl = eRemoteDB.Constants.intNull
        nPremimin = eRemoteDB.Constants.intNull
        nPremimax = eRemoteDB.Constants.intNull
        sRoupremi = String.Empty
        sCacaltyp = String.Empty
        nCacalcov = eRemoteDB.Constants.intNull
        nCacalper = eRemoteDB.Constants.intNull
        nChcaplev = eRemoteDB.Constants.intNull
        nChprelev = eRemoteDB.Constants.intNull
        sFdchantyp = String.Empty
        nFduserlev = eRemoteDB.Constants.intNull
        nFdrateadd = eRemoteDB.Constants.intNull
        nFdratesub = String.Empty
        sPfrandedi = String.Empty
        nCacalmax = eRemoteDB.Constants.intNull
        nCacalmin = eRemoteDB.Constants.intNull
        sAddsuini = String.Empty
        nTarifcurr = eRemoteDB.Constants.intNull
        nRolcap = eRemoteDB.Constants.intNull
        nProcess = eRemoteDB.Constants.intNull
        gennCurrency = eRemoteDB.Constants.intNull
        sKey = String.Empty
        conCapital = eRemoteDB.Constants.intNull
        sMessage = String.Empty
        nAge_reinsu = eRemoteDB.Constants.intNull
        nSalvage = eRemoteDB.Constants.intNull
        nAmount = eRemoteDB.Constants.intNull
        nCharge = eRemoteDB.Constants.intNull
        nRescue_Charge = eRemoteDB.Constants.intNull
        dRescuedate = eRemoteDB.Constants.dtmNull
        nRetarif = eRemoteDB.Constants.intNull
        nRole = eRemoteDB.Constants.intNull
        nAgemininsf = eRemoteDB.Constants.intNull
        nAgemaxinsf = eRemoteDB.Constants.intNull
        nAgemaxperf = eRemoteDB.Constants.intNull
        nError = eRemoteDB.Constants.intNull
        nCountCurrency = eRemoteDB.Constants.intNull
        nCountGroup = eRemoteDB.Constants.intNull
        sCodispl = "CA014"
    End Sub

    '% Class_Initialize: Se controla la creación de cada instancia de la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        InitValues()
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% Class_Terminate: Se ejecuta cuando se desinstancia la clase
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mcolTCovers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcolTCovers = Nothing
        'UPGRADE_NOTE: Object mclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsRoles = Nothing
        'UPGRADE_NOTE: Object mclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsCurren_pol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
    '%Find_Cover: Obtiene las coberturas que generen valores de rescate
    Public Function Find_Cover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal nPolicy As Double, ByVal sCertype As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lintCount As Integer
        Dim lrecreaCover As eRemoteDB.Execute
        lrecreaCover = New eRemoteDB.Execute

        On Error GoTo Find_Cover_Err

        Find_Cover = True

        With lrecreaCover
            .StoredProcedure = "reaCover"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_Cover = .Run
            If Find_Cover Then
                ReDim arrCover_surr(50)
                lintCount = 0
                Do While Not .EOF
                    arrCover_surr(lintCount).sDescript = .FieldToClass("sDescript")
                    arrCover_surr(lintCount).nAge_reinsu = .FieldToClass("nAge_reinsu")
                    arrCover_surr(lintCount).dEffecdate = .FieldToClass("dEffecdate")
                    arrCover_surr(lintCount).nCurrency = .FieldToClass("nCurrency")
                    arrCover_surr(lintCount).nCapital = .FieldToClass("nCapital")
                    arrCover_surr(lintCount).nSalvage = .FieldToClass("nSalvage")
                    arrCover_surr(lintCount).nCover = .FieldToClass("nCover")
                    arrCover_surr(lintCount).sAddsuini = .FieldToClass("sAddsuini")
                    arrCover_surr(lintCount).nCharge = .FieldToClass("nCharge")
                    arrCover_surr(lintCount).nBalance = .FieldToClass("nBalance")
                    arrCover_surr(lintCount).nSalvage_curr = .FieldToClass("nSalvage_curr")
                    lintCount = lintCount + 1
                    .RNext()
                Loop
                .RCloseRec()
                ReDim Preserve arrCover_surr(lintCount)
            End If
        End With

Find_Cover_Err:
        If Err.Number Then
            Find_Cover = False
        End If
        'UPGRADE_NOTE: Object lrecreaCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCover = Nothing
        On Error GoTo 0
    End Function

    '+ Agregada para vi009, enviado de caracas
    Public Function ItemVI009(ByVal lintIndex As Integer) As Boolean
        If lintIndex <= UBound(arrCover_surr) Then
            With arrCover_surr(lintIndex)
                sDescript = .sDescript
                nAge_reinsu = .nAge_reinsu
                dEffecdate = .dEffecdate
                nCurrency = .nCurrency
                nCapital = .nCapital
                nSalvage = .nSalvage
                nCover = .nCover
                sAddsuini = .sAddsuini
                nCharge = .nCharge
            End With
            ItemVI009 = True
        Else
            ItemVI009 = False
        End If
    End Function

    '%InsValSI813: Validaciones de la transacción SI813, según especificaciones funcionales
    Public Function InsValSI813(ByVal sCodispl As String, ByVal bMassive As Boolean, ByVal ncount As Integer, ByVal nActionCov As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValSI813_Err
        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If bMassive Then
                If ncount <= 0 Then
                    .ErrorMessage(sCodispl, 3679)
                End If
            Else
                If nActionCov = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11298)
                End If
            End If
            InsValSI813 = .Confirm
        End With

InsValSI813_Err:
        If Err.Number Then
            InsValSI813 = "InsValSI813: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '%InsUpdSI813: Actualizaciones de la transacción SI813, según especificaciones funcionales
    Private Function InsUpdSI813() As Boolean
        Dim lrecinsUpdsi813 As eRemoteDB.Execute
        On Error GoTo insUpdsi813_Err

        '+ Definición de store procedure insUpdsi813 al 04-29-2002 12:28:23
        lrecinsUpdsi813 = New eRemoteDB.Execute
        With lrecinsUpdsi813
            .StoredProcedure = "InsUpdSI813"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nActioncov", nActionCov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDepend", sDepend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsUpdSI813 = .Run(False)
        End With

insUpdsi813_Err:
        If Err.Number Then
            InsUpdSI813 = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdsi813 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdsi813 = Nothing
        On Error GoTo 0

    End Function

    '%InsPostSI813Upd: Actualizaciones de la transacción SI813, según especificaciones funcionales
    Public Function InsPostSI813Upd(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sClient As String, ByVal nRole As Integer, ByVal nCapital As Double, ByVal nActionCov As Integer, ByVal nUsercode As Integer, ByVal nSessionId As String, ByVal sDepend As String, ByVal sDefaulti As String) As Boolean
        Dim lcolCovers As TCovers
        Dim lstrKey As String

        On Error GoTo InsPostSI813Upd_Err
        lcolCovers = New TCovers
        lstrKey = lcolCovers.sKey(nUsercode, nSessionId)
        'UPGRADE_NOTE: Object lcolCovers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolCovers = Nothing

        With Me
            .sKey = lstrKey
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nModulec = nModulec
            .nGroup = nGroup
            .nCover = nCover
            .nRole = nRole
            .sClient = sClient
            .nCapital = nCapital
            .nActionCov = nActionCov
            .sDepend = sDepend
            .sDefaulti = sDefaulti
            InsPostSI813Upd = InsUpdSI813()
        End With
InsPostSI813Upd_Err:
        If Err.Number Then
            InsPostSI813Upd = False
        End If
    End Function

    '%insUpdSI813_K: Actualizaciones de la transacción SI813, según especificaciones funcionales
    Public Function insUpdSI813_K() As Boolean

        Dim lrecinsUpdSI813_K As eRemoteDB.Execute

        On Error GoTo insUpdSI813_K_Err

        lrecinsUpdSI813_K = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insupdsi813_k'
        '+Información leída el 02/05/2002
        With lrecinsUpdSI813_K
            .StoredProcedure = "insupdsi813_k"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdSI813_K = .Run(False)

        End With

insUpdSI813_K_Err:
        If Err.Number Then

            insUpdSI813_K = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdSI813_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdSI813_K = Nothing
        On Error GoTo 0
    End Function

    '%InsPostSI813: Actualizaciones de la transacción SI813, según especificaciones funcionales
    Public Function InsPostSI813(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nClaim As Double, ByVal nCapital As Double, ByVal nUsercode As Integer, ByVal nSessionId As String) As Boolean
        Dim lcolCovers As TCovers
        Dim lstrKey As String

        On Error GoTo InsPostSI813_Err

        lcolCovers = New TCovers
        lstrKey = lcolCovers.sKey(nUsercode, nSessionId)
        'UPGRADE_NOTE: Object lcolCovers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolCovers = Nothing

        With Me
            .sKey = lstrKey
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nCapital = nCapital
            .nUsercode = nUsercode
            .nClaim = nClaim

            InsPostSI813 = insUpdSI813_K()

        End With

InsPostSI813_Err:
        If Err.Number Then
            InsPostSI813 = False
        End If
    End Function

    '%InsUpdWinDepend: Actualiza el indicador de las ventanas dependientes de las coberturas
    Public Function InsUpdWinDepend(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sCodispl As String, ByVal sIndexCover As String) As Boolean
        Dim lclsClause As Clause
        Dim lclsinsured_expdis As Insured_expdis
        Dim lclsPolicy_Win As Policy_Win

        On Error GoTo InsUpdWinDepend_Err

        InsUpdWinDepend = True
        lclsClause = New Clause
        lclsinsured_expdis = New Insured_expdis
        lclsPolicy_Win = New Policy_Win

        If Not lclsClause.insExistsPolicy(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            '+Se coloca la de clausulas sin información
            InsUpdWinDepend = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA022", "1", , , , False)
        End If

        If Not lclsinsured_expdis.insExistsPolicy(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            '+Se coloca la de recargos por asegurados sin información
            InsUpdWinDepend = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI681", "1", , , , False)
        End If

        '+Se coloca la de Valor póliza Requerida
        InsUpdWinDepend = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VA595", "3", , , , False)

        '+Se coloca la de Coberturas con información
        InsUpdWinDepend = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl & sIndexCover, "2")
InsUpdWinDepend_Err:
        If Err.Number Then
            InsUpdWinDepend = False
        End If
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
        'UPGRADE_NOTE: Object lclsinsured_expdis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsinsured_expdis = Nothing
        'UPGRADE_NOTE: Object lclsClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClause = Nothing
        On Error GoTo 0
    End Function

    '%InsupCA829: Actualización de coberturas
    Public Function InsupCA829(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecinsReaca829 As eRemoteDB.Execute
        Dim lclsCover As Cover

        On Error GoTo InsupCA829_Err

        '+ Definición de store procedure insReasi813 al 04-26-2002 12:56:13
        lrecinsReaca829 = New eRemoteDB.Execute
        With lrecinsReaca829
            .StoredProcedure = "insUpdcover_co"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsupCA829 = .Run(False)
        End With

InsupCA829_Err:
        If Err.Number Then
            InsupCA829 = False
        End If
        'UPGRADE_NOTE: Object lrecinsReaca829 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsReaca829 = Nothing
        'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCover = Nothing
        On Error GoTo 0
    End Function

    '%FindCa829: Obtiene las coberturas de la póliza para la SI813
    Public Function FindCa829(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nReload As Integer, ByVal nCurrency As Integer) As Boolean
        Dim lrecinsReaca829 As eRemoteDB.Execute
        Dim lintIndex As Integer
        Dim lintTop As Integer
        Dim lclsPolicy As Policy
        Dim lclsTab_Modul As Modules
        On Error GoTo FindCa829_Err
        '+Se buscan las monedas asociadas a la póliza
        If mclsCurren_pol Is Nothing Then
            mclsCurren_pol = New ePolicy.Curren_pol
        End If
        With mclsCurren_pol
            If .Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
                If nCurrency = eRemoteDB.Constants.intNull Then
                    If .IsLocal Then
                        nCurrency = 1
                    Else
                        Call .Val_Curren_pol(0)
                    End If
                    nCurrency = .nCurrency
                End If
            End If
            nCountCurrency = .CountCurrenPol + 1
            Me.nCurrency = nCurrency
            If nCountCurrency <= 0 Then
                nError = 3738
            End If
        End With

        '+Se obtiene la información de la transacción
        If nError < 1 Then
            lclsPolicy = New Policy
            Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
            Me.nGroup_insu = nGroup
            Me.sTyp_module = lclsPolicy.sTyp_module
            Me.sPolitype = lclsPolicy.sPolitype
            lclsTab_Modul = New Modules
            Me.bModulec = Not lclsTab_Modul.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

            '+ Definición de store procedure insReasi813 al 04-26-2002 12:56:13
            lrecinsReaca829 = New eRemoteDB.Execute
            With lrecinsReaca829
                .StoredProcedure = "InsReaca829"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nGroup", IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sTyp_module", Me.sTyp_module, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sPolitype", Me.sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    mblnCharge = True
                    FindCa829 = True
                    lintIndex = -1
                    lintTop = -1
                    Do While Not .EOF
                        If lintTop = lintIndex Then
                            lintTop = lintTop + 50
                            ReDim Preserve marrCover(lintTop)
                        End If
                        lintIndex = lintIndex + 1
                        marrCover(lintIndex).nCurrency = .FieldToClass("nCurrency")
                        marrCover(lintIndex).nGroup = .FieldToClass("ngroup")
                        marrCover(lintIndex).nGroup_insu = .FieldToClass("ngroup")
                        marrCover(lintIndex).nModulec = .FieldToClass("nModulec")
                        marrCover(lintIndex).nCover = .FieldToClass("nCover")
                        marrCover(lintIndex).sDescript = .FieldToClass("sDescript")
                        marrCover(lintIndex).nCapital = .FieldToClass("nCapitaltot")
                        marrCover(lintIndex).nCapital_wait = .FieldToClass("nCapitalwaittot")
                        marrCover(lintIndex).nRatecove = .FieldToClass("nratecove")
                        marrCover(lintIndex).nRatecove_b = .FieldToClass("nratecove_b")
                        marrCover(lintIndex).nPremium = .FieldToClass("npremium_b")
                        marrCover(lintIndex).nPremium_tot = .FieldToClass("npremiumtot")
                        marrCover(lintIndex).ncommi_an = .FieldToClass("ncommi_an")

                        .RNext()
                    Loop
                    .RCloseRec()
                    ReDim Preserve marrCover(lintIndex)
                End If

            End With
        End If
FindCa829_Err:
        If Err.Number Then
            FindCa829 = False
        End If
        'UPGRADE_NOTE: Object lrecinsReaca829 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsReaca829 = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsTab_Modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_Modul = Nothing
        On Error GoTo 0
    End Function

    '% CoverItem: Carga en las variables de la clase la información de la cobertura
    Public Function CoverItem(ByVal lintIndex As Integer) As Boolean
        If mblnCharge Then
            If lintIndex <= UBound(marrCover) Then
                With marrCover(lintIndex)
                    nCurrency = .nCurrency
                    nGroup = .nGroup
                    nGroup_insu = .nGroup_insu
                    nModulec = .nModulec
                    nCover = .nCover
                    sDescript = .sDescript
                    nCapital = .nCapital
                    nCapital_wait = .nCapital_wait
                    nRatecove = .nRatecove
                    nRatecove_b = .nRatecove_b
                    nPremium = .nPremium
                    nPremium_tot = .nPremium_tot
                    ncommi_an = .ncommi_an
                End With
                CoverItem = True
            End If
        End If
    End Function

    '%Find: Obtiene los datos de la tabla TCover para la transacción VI7011
    Public Function InsPreVI7011(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sKey As String, ByVal nUsercode As Integer) As Boolean
        Dim lrecreatcover As eRemoteDB.Execute
        Dim lclsTCover As TCover
        Dim lclsTCovers As TCovers = New TCovers

        On Error GoTo Find_Err
        lrecreatcover = New eRemoteDB.Execute
        'Set mCol = New Collection
        With lrecreatcover
            .StoredProcedure = "INSVI7011PKG.INSPREVI7011"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 10, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

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

                    Call lclsTCovers.Add(lclsTCover)
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

    '%valExistOnlyOneInsured: Valida la existencia de un unico asegurado en la póliza
    Public Function valExistOnlyOneInsured(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrevalExistOnlyOneInsured As eRemoteDB.Execute

        '+ Definición de store procedure reaCover_o al 14-08-2013 10:20:00
        lrevalExistOnlyOneInsured = New eRemoteDB.Execute
        With lrevalExistOnlyOneInsured
            .StoredProcedure = "valExistOnlyOneInsured"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", eRemoteDB.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            If .Parameters("sClient").Value <> "*" Then
                valExistOnlyOneInsured = True
                sClient = .Parameters("sClient").Value
            End If
        End With

        lrevalExistOnlyOneInsured = Nothing
    End Function


    '%insValCa014: Este metodo se encarga realizar las validaciones masivas correspondientes a la
    '%ventana de coberturas (CA014).
    Public Function DelCoverforGroup(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nTransaction As Integer) As Boolean
        Dim lrecinsValca014 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        On Error GoTo insValca014_Err

        lrecinsValca014 = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca014 al 06-28-2003 11:20:07
        '+
        With lrecinsValca014
            .StoredProcedure = "DelCoverforGroup"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            DelCoverforGroup = True

        End With

insValca014_Err:
        If Err.Number Then
            DelCoverforGroup = "DelCoverforGroup: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lrecinsValca014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        DelCoverforGroup = Nothing
        On Error GoTo 0

    End Function
End Class






