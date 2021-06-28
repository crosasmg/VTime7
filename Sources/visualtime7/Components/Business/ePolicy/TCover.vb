Option Strict Off
Option Explicit On
Public Class TCover
	'%-------------------------------------------------------%'
	'% $Workfile:: TCover.cls                               $%'
	'% $Author:: Jsarabia                                   $%'
	'% $Date:: 7-08-09 12:23                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema al 30/11/2000.
	
	'   Column_name                        Type         Computed   Length  Prec  Scale  Nullable   TrimTrailingBlanks    FixedLenNullInSource
	Public sCertype As String 'char            no         1                   no               no                     no
	Public sChange As String 'char            no         1                   yes              no                     yes
	Public sFrandedi As String 'char            no         1                   yes              no                     yes
	Public sWait_type As String 'char            no         1                   yes              no                     yes
	Public sFrancApl As String 'char            no         1                   yes              no                     yes
	Public sFree_premi As String 'char            no         1                   yes              no                     yes
	Public sDescript As String 'char            no        30                   yes              no                     yes
	Public sExist As String 'char            no         1                   yes              no                     yes
	Public sRequired As String 'char            no         1                   yes              no                     yes
	Public sDefaulti As String 'char            no         1                   yes              no                     yes
	Public sCacalili As String 'char            no         1                   yes              no                     yes
	Public sCh_typ_cap As String 'char            no         1                   yes              no                     yes
	Public sChange_typ As String 'char            no         1                   yes              no                     yes
	Public sFdrequire As String 'char            no         1                   yes              no                     yes
	Public sRoupremi As String 'char            no        12                   yes              no                     yes
	Public dEffecdate As Date 'datetime        no         8                   no               (n/a)                  (n/a)
	Public nCapital As Double 'decimal         no         9       12    0     yes              (n/a)                  (n/a)
	Public nDiscount As Double 'decimal         no         5        4    2     yes              (n/a)                  (n/a)
	Public nFixamount As Double 'decimal         no         9       10    0     yes              (n/a)                  (n/a)
	Public nMaxamount As Double 'decimal         no         9       10    0     yes              (n/a)                  (n/a)
	Public nRate As Double 'decimal         no         5        4    2     yes              (n/a)                  (n/a)
	Public nMinamount As Double 'decimal         no         9       10    0     yes              (n/a)                  (n/a)
	Public nPremium As Double 'decimal         no         9       10    2     yes              (n/a)                  (n/a)
	Public nRatecove As Double 'decimal         no         5        9    6     yes              (n/a)                  (n/a)
	Public nCapitali As Double 'decimal         no         9       12    0     yes              (n/a)                  (n/a)
	Public nRatecapadd As Double 'decimal         no         5        6    2     yes              (n/a)                  (n/a)
	Public nRatecapsub As Double 'decimal         no         5        6    2     yes              (n/a)                  (n/a)
	Public nRatepreadd As Double 'decimal         no         5        6    2     yes              (n/a)                  (n/a)
	Public nRatepresub As Double 'decimal         no         5        6    2     yes              (n/a)                  (n/a)
	Public nDisc_Amoun As Double 'decimal         no         5        8    2     yes              (n/a)                  (n/a)
	Public npremirat As Double 'decimal         no         5        9    6     yes              (n/a)                  (n/a)
	Public nPremimin As Double 'decimal         no         9       10    2     yes              (n/a)                  (n/a)
	Public nPremimax As Double 'decimal         no         9       10    2     yes              (n/a)                  (n/a)
	Public nPolicy As Double 'int             no         4       10    0     no               (n/a)                  (n/a)
	Public nCertif As Double 'int             no         4       10    0     yes              (n/a)                  (n/a)
	Public nBranch As Integer 'smallint        no         2        5    0     no               (n/a)                  (n/a)
	Public nProduct As Integer 'smallint        no         2        5    0     no               (n/a)                  (n/a)
	Public nGroup As Integer 'smallint        no         2        5    0     no               (n/a)                  (n/a)
	Public nModulec As Integer 'smallint        no         2        5    0     no               (n/a)                  (n/a)
	Public nCover As Integer 'smallint        no         2        5    0     no               (n/a)                  (n/a)
	Public nCurrency As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public nWait_quan As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public nGroup_insu As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public nCover_in As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public nCoverapl As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public sKey As String 'varchar         no        20                   yes              no                     no
	Public nPremifix As Double 'decimal         no         9       10    2     yes              (n/a)                  (n/a)
	Public sCacalfri As String 'char            no         1                   yes              no                     yes
	Public nChcaplev As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public nChprelev As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public nFduserlev As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public sFdchantyp As String 'char            no         1                   yes              no                     yes
	Public nFdrateadd As Double 'decimal         no         5        6    2     yes              (n/a)                  (n/a)
	Public nFdratesub As Double 'decimal         no         5        6    2     yes              (n/a)                  (n/a)
	Public nCacalcov As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public nCacalper As Double 'decimal         no         5        5    2     yes              (n/a)                  (n/a)
	Public sPfrandedi As String 'char            no         1                   yes              no                     yes
	Public nCacalmax As Double 'decimal         no         9       12    0     yes              (n/a)                  (n/a)
	Public nCacalmin As Double 'decimal         no         9       12    0     yes              (n/a)                  (n/a)
	Public sAddsuini As String 'char            no         1                   yes              no                     yes
	Public nTarifcurr As Integer 'smallint        no         2        5    0     yes              (n/a)                  (n/a)
	Public sRouchaca As String 'char            no        12                   yes              no                     yes
	Public nCacalfix As Double 'decimal         no         9       12    0     yes              (n/a)                  (n/a)
	Public nCapital_o As Double
	Public nRateCove_o As Double
	Public nPremium_o As Double
	Public sFrancApl_o As String
	Public sFrandedi_o As String
	Public nDisc_amoun_o As Double
	Public nRate_o As Double
	Public nCapital_req As Double
	
	Public sClient As String ' CHAR          14              Yes
	Public nTypDurins As Integer ' NUMBER        22     5      0 Yes
	Public nDurinsur As Integer ' NUMBER        22     5      0 Yes
	Public nTyp_AgeMinM As Integer
	Public nTyp_AgeMinF As Integer
	
	Public nAgeminins As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxins As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxper As Integer ' NUMBER        22     5      0 Yes
	Public nTypDurpay As Integer ' NUMBER        22     5      0 Yes
	Public nDurpay As Integer ' NUMBER        22     5      0 Yes
	Public nCauseupd As Integer ' NUMBER        22     5      0 Yes
	Public nCapital_wait As Double ' NUMBER        22    12      0 Yes
	Public nAgelimit As Integer ' NUMBER        22     5      0 Yes
	Public nAge_per As Integer ' NUMBER        22     5      0 Yes
	Public dAniversary As Date ' DATE           7              Yes
	Public dSeektar As Date ' DATE           7              Yes
	Public dFer As Date ' DATE           7              Yes
	Public nBranch_rei As Integer ' NUMBER        22     5      0 Yes
	Public nRole As Integer
	Public nRolcap As Integer
	Public nRolprem As Integer
	Public sSexclien As String
	Public nRetarif As Integer
	Public nAgemininsf As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxinsf As Integer ' NUMBER        22     5      0 Yes
	Public nAgemaxperf As Integer ' NUMBER        22     5      0 Yes
	Public sRequirec As String
	Public sDefaultic As String
	Public nApply_Perc As Double
	Public sDepend As String
	Public nActionCov As Integer
	Public sBas_sumins As String
	
	'-Variable nuevas se agregaron a la tabla TCOVER
	Public sRoucapit As String
	Public nCamaxcov As Integer
	Public nCamaxper As Double
	Public nCamaxrol As Integer
	Public nCacalmul As Integer
	Public nGenCurrency As Integer
	Public nPremfreq1 As Integer
	Public nPremfreq2 As Integer
    Public nPremfreq3 As Integer
    Public nRateCla As Double
    Public nFixAmoCla As Double
    Public nMinAmoCla As Double
    Public nMaxAmoCla As Double
    Public nDiscCla As Double
    Public nDisc_AmoCla As Double
    Public nFrancDays As Double
    '-Variable auxiliar para verificar la transacción
    Public sCodispl As String

    '-Variable auxiliar para la informacion de los asegurados
    Public ncount As Integer

    '- Descripciones para la CA014

    Public sdesc_t5559 As String
    Public sdesc_t64 As String
    Public sdesc_t33 As String
    Public sdesc_t5589 As String
    Public sdesc_t_pay As String
    Public sdesc_t52 As String
    Public sdesc_t5547 As String
    Public sdesc_t5000 As String


    '%nSel. Indica si la cobertura viene seleccionada por defecto
    Public ReadOnly Property nSel(ByVal bDataFound As Boolean) As Byte
        Get
            '+Registro queda seleccionado por omision si estaba en la tabla y no es eliminado por el
            '+usuario o si es seleccionado por el usuario o viene seleccionado por defecto
            '+y NO existian datos en la tabla
            If (sExist = "1" And sDefaulti <> "9") Or sDefaulti = "3" Or (sDefaulti = "1" And Not bDataFound) Then
                nSel = 1
            Else
                nSel = 2
            End If
        End Get
    End Property

    '%Update. Este metodo se encarga de creaar y/o actualizar el registro de tCover
    Public Function Update() As Boolean
        sChange = InsChange
        Update = Add()
    End Function

    '%InsChange. Esta funcion se encarga de retornar el valor del campo sChange
    Private Function InsChange() As String
        Dim lstrChange As String

        lstrChange = IIf(sChange = String.Empty, "1", sChange)

        '+Si la Prima cambió, se agrega el cambio de Prima
        If nPremium <> nPremium_o Then
            lstrChange = IIf(lstrChange = "4", "6", "2")
        End If

        '+Si la tasa cambio, se agrega el cambio de tasa
        If nRatecove <> nRateCove_o Then
            lstrChange = IIf(lstrChange = "4", "5", "3")
        End If

        '+ Si el capital cambio, se agrega cambio de capital
        If nCapital <> nCapital_o Then
            If lstrChange = "1" Then
                lstrChange = "4"
            Else
                If lstrChange = "3" Then
                    lstrChange = "5"
                ElseIf lstrChange = "2" Then
                    lstrChange = "6"
                End If
            End If
        End If

        InsChange = lstrChange
    End Function

    '%Add. Este metodo se encarga de creaar y/o actualizar el registro de tCover
    Public Function Add() As Boolean
        Dim lreccrecovert As eRemoteDB.Execute

        On Error GoTo Add_err

        lreccrecovert = New eRemoteDB.Execute

        With lreccrecovert
            .StoredProcedure = "Crecovert"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("gEnnmodulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("gEnncover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChange", sChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("cOndiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFrandedi", sFrandedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFixamount", nFixamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("cOnwait_quan", nWait_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("cOswait_type", sWait_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecove", nRatecove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFrancapl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("cOvngroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("cOvncapitali", nCapitali, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFree_premi", sFree_premi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("tAbgsdescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 120, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExist", sExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequire", sRequired, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCacalili", sCacalili, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCh_typ_cap", sCh_typ_cap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecapadd", nRatecapadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecapsub", nRatecapsub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover_in", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatepreadd", nRatepreadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatepresub", nRatepresub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChange_typ", sChange_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFdrequire", sFdrequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisc_amoun", nDisc_Amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremirat", npremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCoverapl", nCoverapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremimin", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremimax", nPremimax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoupremi", sRoupremi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremifix", nPremifix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCacalfri", sCacalfri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nChcaplev", nChcaplev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nChprelev", nChprelev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFdchantyp", sFdchantyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFduserlev", nFduserlev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFdrateadd", nFdrateadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFdratesub", nFdratesub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalcov", nCacalcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalper", nCacalper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPfrandedi", sPfrandedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalmax", nCacalmax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalmin", nCacalmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAddsuini", sAddsuini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTarifcurr", nTarifcurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRouchaca", sRouchaca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalfix", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRolcap", nRolcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRolprem", nRolprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemininsm", nAgeminins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxinsm", nAgemaxins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemininsf", nAgemininsf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxinsf", nAgemaxinsf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxperm", nAgemaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgemaxperf", nAgemaxperf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurins", nTypDurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdurpay", nTypDurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDurinsur", nDurinsur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDurpay", nDurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_wait", nCapital_wait, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("tCrnwait_quan", nWait_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("tCrswait_type", sWait_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAniversary", dAniversary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dSeektar", dSeektar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRetarif", nRetarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequirec", sRequirec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDefaultic", sDefaultic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nApply_perc", nApply_Perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCauseupd", nCauseupd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dFer", dFer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBas_sumins", sBas_sumins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_o", nCapital_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium_o", nPremium_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRatecove_o", nRateCove_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            '-Variable nuevas se agregaron a la tabla TCOVER
            .Parameters.Add("sRoucapit", sRoucapit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCamaxcov", nCamaxcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCamaxper", nCamaxper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCamaxrol", nCamaxrol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCacalmul", nCacalmul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGenCurrency", nGenCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_AgeMinM", nTyp_AgeMinM, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_AgeMinF", nTyp_AgeMinF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_req", nCapital_req, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nRateCla", nRateCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 4, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFixAmoCla", nFixAmoCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinAmoCla", nMinAmoCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaxAmoCla", nMaxAmoCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDiscCla", nDiscCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 4, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisc_AmoCla", nDisc_AmoCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 18, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancDays", nFrancDays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 18, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then
            Add = False
        End If
        'UPGRADE_NOTE: Object lreccrecovert may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccrecovert = Nothing
        On Error GoTo 0
    End Function
	
	'%Find: Obtiene la información de la cobertura
	Public Function Find(ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal sClient As String, Optional ByVal bAll As Boolean = False) As Boolean
		Dim lrecreatcover As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.sKey <> sKey Or Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or bAll Then
			
			lrecreatcover = New eRemoteDB.Execute
			
			With lrecreatcover
				.StoredProcedure = "reatCover"
				.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					sCertype = .FieldToClass("sCertype")
					sChange = .FieldToClass("sChange")
					sFrandedi = .FieldToClass("sFrandedi")
					sWait_type = .FieldToClass("sWait_type")
					sFrancApl = .FieldToClass("sFrancapl")
					sFree_premi = .FieldToClass("sFree_premi")
					sDescript = .FieldToClass("sDescript")
					sExist = .FieldToClass("sExist")
					sRequired = .FieldToClass("sRequire")
					sDefaulti = .FieldToClass("sDefaulti")
					sCacalili = .FieldToClass("sCacalili")
					sCh_typ_cap = .FieldToClass("sCh_typ_cap")
					sChange_typ = .FieldToClass("sChange_typ")
					sFdrequire = .FieldToClass("sFdrequire")
					sRoupremi = .FieldToClass("sRoupremi")
					dEffecdate = .FieldToClass("dEffecdate")
					nCapital = .FieldToClass("nCapital")
					nDiscount = .FieldToClass("nDiscount")
					nFixamount = .FieldToClass("nFixamount")
					nMaxamount = .FieldToClass("nMaxamount")
					nRate = .FieldToClass("nRate")
					nMinamount = .FieldToClass("nMinamount")
					nPremium = .FieldToClass("nPremium")
					nRatecove = .FieldToClass("nRatecove")
					nCapitali = .FieldToClass("nCapitali")
					nRatecapadd = .FieldToClass("nRatecapadd")
					nRatecapsub = .FieldToClass("nRatecapsub")
					nRatepreadd = .FieldToClass("nRatepreadd")
					nRatepresub = .FieldToClass("nRatepresub")
					nDisc_Amoun = .FieldToClass("nDisc_amoun")
					npremirat = .FieldToClass("nPremirat")
					nPremimin = .FieldToClass("nPremimin")
					nPremimax = .FieldToClass("nPremimax")
					nPolicy = .FieldToClass("nPolicy")
					nCertif = .FieldToClass("nCertif")
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					nGroup = .FieldToClass("nGroup")
					nModulec = .FieldToClass("nModulec")
					nCover = .FieldToClass("nCover")
					nCurrency = .FieldToClass("nCurrency")
					nWait_quan = .FieldToClass("nWait_quan")
					nGroup_insu = .FieldToClass("nGroup_insu")
					nCover_in = .FieldToClass("nCover_in")
					nCoverapl = .FieldToClass("nCoverapl")
					sKey = .FieldToClass("sKey")
					nPremifix = .FieldToClass("nPremifix")
					sCacalfri = .FieldToClass("sCacalfri")
					nChcaplev = .FieldToClass("nChcaplev")
					nChprelev = .FieldToClass("nChprelev")
					nFduserlev = .FieldToClass("nFduserlev")
					sFdchantyp = .FieldToClass("sFdchantyp")
					nFdrateadd = .FieldToClass("nFdrateadd")
					nFdratesub = .FieldToClass("nFdratesub")
					nCacalcov = .FieldToClass("nCacalcov")
					nCacalper = .FieldToClass("nCacalper")
					sPfrandedi = .FieldToClass("sPfrandedi")
					nCacalmax = .FieldToClass("nCacalmax")
					nCacalmin = .FieldToClass("nCacalmin")
					sAddsuini = .FieldToClass("sAddsuini")
					nTarifcurr = .FieldToClass("nTarifcurr")
					sRouchaca = .FieldToClass("sRouchaca")
					nCacalfix = .FieldToClass("nCacalfix")
					sClient = .FieldToClass("sClient")
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
					nRole = .FieldToClass("nRole")
					nRolcap = .FieldToClass("nRolCap")
					nRolprem = .FieldToClass("nRolprem")
					dAniversary = .FieldToClass("dAniversary")
					dSeektar = .FieldToClass("dSeektar")
					nRetarif = .FieldToClass("nRetarif")
					sRequirec = .FieldToClass("sRequirec")
					sDefaultic = .FieldToClass("sDefaultic")
					nBranch_rei = .FieldToClass("nBranch_rei")
					nRole = .FieldToClass("nRole")
					nRolcap = .FieldToClass("nRolcap")
					nRolprem = .FieldToClass("nRolprem")
					nRetarif = .FieldToClass("nRetarif")
					nAgemininsf = .FieldToClass("nAgemininsf")
					nAgemaxinsf = .FieldToClass("nAgemaxinsf")
					nAgemaxperf = .FieldToClass("nAgemaxperf")
					sDefaultic = .FieldToClass("sDefaultic")
					sRequirec = .FieldToClass("sRequirec")
					sDepend = .FieldToClass("sDepend")
					nActionCov = .FieldToClass("nActioncov")
					nApply_Perc = .FieldToClass("nApply_perc")
					sBas_sumins = .FieldToClass("sBas_sumins")
					nTyp_AgeMinM = .FieldToClass("nTyp_AgeMinM")
					nTyp_AgeMinF = .FieldToClass("nTyp_AgeMinF")
					
					'-Variable nuevas se agregaron a la tabla TCOVER
					sRoucapit = .FieldToClass("sRoucapit")
					nCamaxcov = .FieldToClass("nCamaxcov")
					nCamaxper = .FieldToClass("nCamaxper")
					nCamaxrol = .FieldToClass("nCamaxrol")
					nCacalmul = .FieldToClass("nCacalmul")
					nGenCurrency = .FieldToClass("nGenCurrency")
					
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
		'UPGRADE_NOTE: Object lrecreatcover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatcover = Nothing
	End Function
	
	'%Find_Role: Se verifica si el asegurado princ. tiene dependencia con asegurados adicionales
	Public Function Find_Role(ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreatcover As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.sKey <> sKey Or Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or bFind Then
			
			lrecreatcover = New eRemoteDB.Execute
			
			With lrecreatcover
				.StoredProcedure = "ReaValCoverCount"
				.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					Me.ncount = .Parameters("nCount").Value
					Find_Role = True
				End If
			End With
		Else
			Find_Role = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find_Role = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreatcover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatcover = Nothing
	End Function
End Class






