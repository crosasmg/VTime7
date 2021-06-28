<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objetos/Variables para el manejo de la transacción
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid
    Dim mclsCover As ePolicy.Cover
    Dim mclsTCover As ePolicy.TCover
    Dim mblnFound As Boolean
    Dim mstrClient As String
    Dim mstrRole As Object
    Dim mclsGeneral As Object
    Dim mstrError As String
    Dim mintCurrency As String
    Dim mdblLegAmount As Object
    Dim lblnFer As Boolean
    Dim mblnDisabledByLevels As Boolean



    '%insDefineHeader(). Este procedimiento se encarga de definir las líneas del encabezado
    '%del grid.
    '---------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '---------------------------------------------------------------------------------------

        Dim lblnDisabledAge As Boolean
        Dim lintLength As Short
        Dim lblnNopayroll As Boolean
        Dim lclsCertif As ePolicy.Certificat
        Dim lintGroup As Integer
        Dim lobjColumn As eFunctions.Column
        Dim lblnDisabledPreExists  As Boolean
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        lclsCertif = New ePolicy.Certificat
        Call lclsCertif.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
        lintGroup = mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)


        If lintGroup = eRemoteDB.Constants.intNull Then
            lintGroup = lclsCertif.nGroup
        End If

        If CStr(Session("sPoliType")) <> "1" Then
            If CStr(Session("sTyp_module")) <> "3" Then
                If lintGroup > 0 Or mobjValues.StringToType(Session("valGroup"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                    Session("sTyp_module") = "3"
                End If
            Else
                If lintGroup > 0 Or mobjValues.StringToType(Session("valGroup"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                    Session("valGroup") = lintGroup
                End If
            End If
        Else
            Session("sTyp_module") = ""
        End If


        lblnFer = mobjValues.TypeToString(lclsCertif.dFer, eFunctions.Values.eTypeData.etdDate) <> ""

        lintLength = 30

        mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
        Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))


        lintGroup = mobjValues.StringToType(Session("valGroup"), eFunctions.Values.eTypeData.etdDouble)
        Session("valGroup") = vbNullString

        If Request.QueryString.Item("Type") <> "PopUp" Then
            '+ Si se trata de una póliza individual o de un colectivo
            lintLength = 120
            If (CStr(Session("sPoliType")) = "1" And Session("nCertif") = 0) Or Session("nCertif") > 0 Then
                mblnFound = mclsCover.insPreCA014(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), lintGroup, Request.QueryString.Item("sCodispl"), Session("nUsercode"), Session("dNulldate"), Session("nTransaction"), mobjValues.StringToType(mstrRole, eFunctions.Values.eTypeData.etdDouble, True), mstrClient, Session("sBrancht"), Request.QueryString.Item("sKey"), Session("SessionId"), Request.QueryString.Item("sDelTCover"), Nothing, vbNullString, Session("sSche_code"), mobjValues.StringToType(Session("nType_amend"), eFunctions.Values.eTypeData.etdLong))
                If mblnFound Then
                    If mstrRole = vbNullString Then
                        mstrRole = mclsCover.mclsRoles.nRole
                    End If

                    If mstrClient = vbNullString Then
                        mstrClient = mclsCover.mclsRoles.sClient
                    End If
                    mdblLegAmount = mclsCover.nLegAmount

                    mblnDisabledByLevels = mclsCover.bDisabledByLevels


                End If
            End If
        Else
            mblnDisabledByLevels = (Request.QueryString.Item("sDisabledByLevels") = "1")
        End If

        '+Se habilitan los campos de edad si el cliente es VIP
        If Request.QueryString.Item("Vip") = "1" And Not mblnDisabledByLevels Then
            lblnDisabledAge = False
        Else
            lblnDisabledAge = True
        End If

        lblnNopayroll = mclsCover.bNopayroll Or Request.QueryString.Item("sNopayroll") = "1"

        '+ Variable para controlar la actualización de la información de manera puntual (desde el botón de la ventana)
        Response.Write(mobjValues.HiddenControl("hddbPuntual", CStr(False)))
        '+ Campo para indicar si la póliza es innominada y poderlo indicar a la ventana PopUp
        Response.Write(mobjValues.HiddenControl("hddsNopayroll", CStr(2)))

        lblnDisabledPreExists = False
        If (Session("sPoliType") = "1" Or Session("nCertif") > 0) And Session("sBrancht") = "7" Then
            Dim lclsHealth As eBranches.Health
            lclsHealth = New eBranches.Health
            Dim lintDaysCount As Integer
            If lclsHealth.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
                If lclsHealth.sWait_type <> "1" Then
                    Dim lclsPolicy As ePolicy.Policy
                    lclsPolicy = New ePolicy.Policy
                    If lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
                        lintDaysCount = lclsHealth.nWait_quan
                        '+ Si el plazo de espera es Horas 
                        If lclsHealth.sWait_type = "2" Then
                            lintDaysCount = DateDiff(DateInterval.Day, lclsPolicy.dStartdate, DateAdd(DateInterval.Hour, lclsHealth.nWait_quan, lclsPolicy.dStartdate))
                        End If
                        '+ Si el plazo de espera es Meses
                        If lclsHealth.sWait_type = "4" Then
                            lintDaysCount = DateDiff(DateInterval.Day, lclsPolicy.dStartdate, DateAdd(DateInterval.Month, lclsHealth.nWait_quan, lclsPolicy.dStartdate))
                        End If
                        '+ Se debe obtener el Plazo de espera registrado en la póliza matriz y llevarlo a días.                        
                        '+ Para obtener la fecha límite de elegibilidad se debe sumar el número de días del Plazo de espera a la fecha 
                        '+ de inicio de vigencia de la póliza matriz.
                        '+ Si la fecha de inicio del certificado en tratamiento no es mayor que la fecha límite de elegibilidad se debe deshabilitar 
                        '+ el check de pre-existencias en la parte repetitiva de esta ventana.
                        If (lclsPolicy.dStartdate < lclsPolicy.dStartdate.AddDays(lintDaysCount)) Then
                            lblnDisabledPreExists = True
                        End If
                    End If
                    lclsPolicy = Nothing
                End If
                lclsHealth = Nothing
            End If
        End If
        If lblnDisabledPreExists Then
            Response.Write(mobjValues.HiddenControl("hddDisabledPreExists", "1"))
        Else
            Response.Write(mobjValues.HiddenControl("hddDisabledPreExists", "2"))
        End If

        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            Call .AddHiddenColumn("hddnAgeIns", vbNullString)
            lobjColumn = .AddNumericColumn(40789, GetLocalResourceObject("tcnModulecColumnCaption"), "tcnModulec", 5, CStr(0), True, GetLocalResourceObject("tcnModulecColumnToolTip"), True, 0,  ,  ,  , True)
            lobjColumn.EditRecord = True
            lobjColumn = .AddNumericColumn(40790, GetLocalResourceObject("tcnCoverColumnCaption"), "tcnCover", 5, CStr(0), True, GetLocalResourceObject("tcnCoverColumnToolTip"), True, 0,  ,  ,  , True)
            lobjColumn.EditRecord = True
            lobjColumn = .AddTextColumn(40785, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", lintLength, vbNullString,  , GetLocalResourceObject("tctCoverColumnToolTip"),  ,  ,  , True)
            lobjColumn.EditRecord = True
            Call .AddNumericColumn(40791, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(0), True, GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
            If Session("nCertif") > 0 Then
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_waitColumnCaption"), "tcnCapital_wait", 18, CStr(0), True, GetLocalResourceObject("tcnCapital_waitColumnToolTip"), True, 6,  ,  , "self.document.forms[0].hddnCapital_Wait.value=this.value;")
            End If
            Call .AddNumericColumn(40792, GetLocalResourceObject("tcnRatecoveColumnCaption"), "tcnRatecove", 9, CStr(0), True, GetLocalResourceObject("tcnRatecoveColumnToolTip"), True, 6)
            Call .AddNumericColumn(40793, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0), True, GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)
            lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeRetarifColumnCaption"), "cbeRetarif", "Table5559", eFunctions.Values.eValuesType.clngComboType, CStr(8),  ,  ,  ,  ,  , mblnDisabledByLevels,  , GetLocalResourceObject("cbeRetarifColumnToolTip"))
            lobjColumn.GridVisible = False

            If Session("sBrancht") = "3" or Session("sBrancht") = "4" Then
                ' Se solocan en comentario los siguientes campos y se colocan como ocultos
                ' ya que la CA960 sustituye el manejo de franqicia y deducibles
                lobjColumn = .AddPossiblesColumn(40786, GetLocalResourceObject("cbeFrandediCaption"), "cbeFrandedi", "table64", 1, "1", False, , , , "insEnableControls(this)", , , GetLocalResourceObject("cbeFrandediToolTip"))
                lobjColumn.BlankPosition = False
                lobjColumn = .AddNumericColumn(40794, GetLocalResourceObject("tcnFraRateCaption"), "tcnFraRate", 9, 0, False, GetLocalResourceObject("tcnFraRateToolTip"), True, 6, , , "insEnableControls(this)", True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddPossiblesColumn(40787, GetLocalResourceObject("cbeFrancAplCaption"), "cbeFrancApl", "Table33", 1, 0, False, , , , , True, , GetLocalResourceObject("cbeFrancAplToolTip"))
                lobjColumn.BlankPosition = False
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40795, GetLocalResourceObject("tcnFixamountCaption"), "tcnFixamount", 18, 0, False, GetLocalResourceObject("tcnFixamountToolTip"), True, 6, , , "insEnableControls(this)", True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40796, GetLocalResourceObject("ttcnMinAmountCaption"), "tcnMinAmount", 18, 0, False, GetLocalResourceObject("tcnMinAmountToolTip"), True, 6, , , , True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40797, GetLocalResourceObject("tcnMaxAmountCaption"), "tcnMaxAmount", 18, 0, False, GetLocalResourceObject("tcnMaxAmountToolTip"), True, 6, , , , True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40798, GetLocalResourceObject("tcnDiscountCaption"), "tcnDiscount", 9, 0, False, GetLocalResourceObject("tcnDiscountToolTip"), True, 6, , , "insEnableControls(this)", True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40799, GetLocalResourceObject("tcnDisc_amounCaption"), "tcnDisc_amoun", 18, 0, False, GetLocalResourceObject("tcnDisc_amounToolTip"), True, 6, , , "insEnableControls(this)", True)
                lobjColumn.GridVisible = False
            Else
                ' Se solocan en comentario los anteriores campos y se colocan como ocultos a continuación
                ' ya que la CA960 sustituye el manejo de franqicia y deducibles
                Call .AddHiddenColumn("cbeFrandedi", vbNullString)
                Call .AddHiddenColumn("tcnFraRate", vbNullString)
                Call .AddHiddenColumn("cbeFrancApl", vbNullString)
                Call .AddHiddenColumn("tcnFixamount", vbNullString)
                Call .AddHiddenColumn("tcnMinAmount", vbNullString)
                Call .AddHiddenColumn("tcnMaxAmount", vbNullString)
                Call .AddHiddenColumn("tcnDiscount", vbNullString)
                Call .AddHiddenColumn("tcnDisc_amoun", vbNullString)
            End If

            If CStr(Session("sBrancht")) = "1" Then
                If Not lblnNopayroll Then
                    lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemininsColumnCaption"), "tcnAgeminins", 2, "",  , GetLocalResourceObject("tcnAgemininsColumnToolTip"),  ,  ,  ,  ,  , lblnDisabledAge)
                    lobjColumn.GridVisible = False
                    lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemaxinsColumnCaption"), "tcnAgemaxins", 2, "",  , GetLocalResourceObject("tcnAgemaxinsColumnToolTip"),  ,  ,  ,  ,  , lblnDisabledAge)
                    lobjColumn.GridVisible = False
                    lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemaxperColumnCaption"), "tcnAgemaxper", 2, "",  , GetLocalResourceObject("tcnAgemaxperColumnToolTip"),  ,  ,  ,  ,  , lblnDisabledAge)
                    lobjColumn.GridVisible = False
                End If

                '+ Vida: 1)Convencional, 7)Vida Activa
                'If mclsCover.nProdClas = 1 Or CDbl(Request.QueryString.Item("nProdclas")) = 1 Or mclsCover.nProdClas = 7 Or CDbl(Request.QueryString.Item("nProdclas")) = 7 Then

                lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypdurinsColumnCaption"), "cbeTypdurins", "table5589", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypdurinsColumnToolTip"))
                lobjColumn.GridVisible = False
                lobjColumn.TypeList = CShort("1")
                lobjColumn.List = "1,2,3,8,9"
                lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnDurinsurColumnCaption"), "tcnDurinsur", 2, "",  , GetLocalResourceObject("tcnDurinsurColumnCaption"),  ,  ,  ,  ,  , True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypdurpayColumnCaption"), "cbeTypdurpay", "table5589", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypdurpayColumnToolTip"))
                lobjColumn.GridVisible = False
                lobjColumn.TypeList = CShort("1")
                lobjColumn.List = "1,2,3,4,8,9"
                lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnDurpayColumnCaption"), "tcnDurpay", 2, "",  , GetLocalResourceObject("tcnDurpayColumnCaption"),  ,  ,  ,  , "insEnableControls(this)", True)
                lobjColumn.GridVisible = False
                'End If
            End If

            lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeWait_typeColumnCaption"), "cbeWait_type", "Table52", eFunctions.Values.eValuesType.clngComboType, , , , , , "insEnableControls(this)", mblnDisabledByLevels Or lblnDisabledPreExists, , GetLocalResourceObject("cbeWait_typeColumnCaption"))
            lobjColumn.BlankPosition = False
            lobjColumn.GridVisible = False
            lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnWaitQColumnCaption"), "tcnWaitQ", 5, "", , GetLocalResourceObject("tcnWaitQColumnCaption"), , , , , , lblnDisabledPreExists)
            lobjColumn.GridVisible = False
            lobjColumn = .AddDateColumn(0, GetLocalResourceObject("tcdFerColumnCaption"), "tcdFer",  ,  , GetLocalResourceObject("tcdFerColumnToolTip"),  ,  ,  , Not (mclsCover.bIsAmendment(Session("nTransaction")) And lblnFer))
            lobjColumn.GridVisible = False
            lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeCauseupdColumnCaption"), "cbeCauseupd", "table5547", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , Not mclsCover.bIsAmendment(Session("nTransaction")),  , GetLocalResourceObject("cbeCauseupdColumnToolTip"),  ,  , False,  ,  , True)
            lobjColumn.TypeList = CShort("2") '+No incluir
            lobjColumn.List = "12" '+Rescate
            lobjColumn.GridVisible = False
            lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valBranch_reiColumnCaption"), "valBranch_rei", "table5000", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , mblnDisabledByLevels,  , GetLocalResourceObject("valBranch_reiColumnToolTip"))
            lobjColumn.GridVisible = False
            lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_reqColumnCaption"), "tcnCapital_req", 18, CStr(0), False, GetLocalResourceObject("tcnCapital_reqColumnToolTip"), True, 6)
            lobjColumn.GridVisible = False

            Call .AddHiddenColumn("hddsChange", CStr(1))
            Call .AddHiddenColumn("hddnPremifix", vbNullString)
            Call .AddHiddenColumn("hddnPremiRat", vbNullString)
            Call .AddHiddenColumn("hddnCoverApl", vbNullString)
            Call .AddHiddenColumn("hddnCover_in", vbNullString)
            Call .AddHiddenColumn("hddnPremimin", vbNullString)
            Call .AddHiddenColumn("hddnPremiMax", vbNullString)
            Call .AddHiddenColumn("hddnGenCurrency", vbNullString)
            Call .AddHiddenColumn("hddnCapital_o", vbNullString)
            Call .AddHiddenColumn("hddnRatecove_o", vbNullString)
            Call .AddHiddenColumn("hddnPremium_o", vbNullString)
            Call .AddHiddenColumn("hddsKeyGrid", vbNullString)
            Call .AddHiddenColumn("hddsRequire", "2")
            Call .AddHiddenColumn("hddSeekTar", vbNullString)
            Call .AddHiddenColumn("hddsRoupremi", vbNullString)
            Call .AddHiddenColumn("hddsCh_typ_cap", vbNullString)
            Call .AddHiddenColumn("hddsChange_typ", vbNullString)
            Call .AddHiddenColumn("hddnApply_perc", vbNullString)

            '+ Se agregan las columnas ocultas para el manejo de creación sin POPUP
            Call .AddHiddenColumn("hddnCapital", vbNullString)
            Call .AddHiddenColumn("hddnRateCove", vbNullString)
            Call .AddHiddenColumn("hddnPremium", vbNullString)
            Call .AddHiddenColumn("hddnCover", vbNullString)
            Call .AddHiddenColumn("hddnModulec", vbNullString)
            Call .AddHiddenColumn("hddsFrandedi", vbNullString)
            Call .AddHiddenColumn("hddsWait_type", vbNullString)
            Call .AddHiddenColumn("hddsFrancApl", vbNullString)
            Call .AddHiddenColumn("hddnDisc_amoun", vbNullString)
            Call .AddHiddenColumn("hddnFraRate", vbNullString)
            Call .AddHiddenColumn("hddnDiscount", vbNullString)
            Call .AddHiddenColumn("hddnFixAmount", vbNullString)
            Call .AddHiddenColumn("hddnMaxAmount", vbNullString)
            Call .AddHiddenColumn("hddnMinAmount", vbNullString)
            Call .AddHiddenColumn("hddnWaitQ", vbNullString)
            Call .AddHiddenColumn("hddnCapital_Wait", vbNullString)
            Call .AddHiddenColumn("hddnAgeminins", vbNullString)
            Call .AddHiddenColumn("hddnAgemaxins", vbNullString)
            Call .AddHiddenColumn("hddnAgemaxper", vbNullString)
            Call .AddHiddenColumn("hddnAgemininsf", vbNullString)
            Call .AddHiddenColumn("hddnAgemaxinsf", vbNullString)
            Call .AddHiddenColumn("hddnAgemaxperf", vbNullString)
            Call .AddHiddenColumn("hddnTypdurins", vbNullString)
            Call .AddHiddenColumn("hddnDurinsur", vbNullString)
            Call .AddHiddenColumn("hddnTypdurpay", vbNullString)
            Call .AddHiddenColumn("hddnDurpay", vbNullString)
            Call .AddHiddenColumn("hddnBranch_Rei", vbNullString)
            Call .AddHiddenColumn("hddnRetarif", vbNullString)
            Call .AddHiddenColumn("hddnCauseupd", vbNullString)
            Call .AddHiddenColumn("hdddfer", vbNullString)
            Call .AddHiddenColumn("hddsExist", vbNullString)
            Call .AddHiddenColumn("hddnRole", mstrRole)
            Call .AddHiddenColumn("hddsBas_sumins", vbNullString)

            Call .AddHiddenColumn("hddnCacalfix", vbNullString)
            Call .AddHiddenColumn("hddsCacalfri", vbNullString)
            Call .AddHiddenColumn("hddsCacalili", vbNullString)
            Call .AddHiddenColumn("hddnCacalcov", vbNullString)
            Call .AddHiddenColumn("hddnCacalper", vbNullString)
            Call .AddHiddenColumn("hddnRolcap", vbNullString)
            Call .AddHiddenColumn("hddsRoucapit", vbNullString)
            Call .AddHiddenColumn("hddnCamaxcov", vbNullString)
            Call .AddHiddenColumn("hddnCamaxper", vbNullString)
            Call .AddHiddenColumn("hddnCamaxrol", vbNullString)
            Call .AddHiddenColumn("hddnCacalmul", vbNullString)
            Call .AddHiddenColumn("hddnCacalmin", vbNullString)
            Call .AddHiddenColumn("hddnCacalmax", vbNullString)
            Call .AddHiddenColumn("hddnCapital_req", vbNullString)
            .AddHiddenColumn("hddFraRateClaim", String.Empty)
            .AddHiddenColumn("hddFixamountClaim", String.Empty)
            .AddHiddenColumn("hddMinAmountClaim", String.Empty)
            .AddHiddenColumn("hddMaxAmountClaim", String.Empty)
            .AddHiddenColumn("hddDiscountClaim", String.Empty)
            .AddHiddenColumn("hddDisc_amounClaim", String.Empty)
            .AddHiddenColumn("hddFrancdays", String.Empty)
        End With

        '+Se asignan la configuración de la ventana (GRID) 
        With mobjGrid
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .ActionQuery = Session("bQuery")
            .Codispl = Request.QueryString.Item("sCodispl")
            .Width = 750
            .Height = 500
            .FieldsByRow = 2
            .Splits_Renamed.AddSplit(0, "", 1)
            .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
            .Top = 20
            .Left = 25
            .DeleteButton = False
            .AddButton = False
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            .EditRecordQuery = mobjValues.ActionQuery
        End With
    End Sub

    '%insPreCA014. Esta rutina se encarga de realizar las operaciones correspondientes a la
    '%actualizacion de datos de la ventana de Coberturas
    '---------------------------------------------------------------------------------------
    Function insPreCA014() As Object
        '---------------------------------------------------------------------------------------
        Dim lintIndex As Object
        Dim lblnOneCurren As Boolean
        Dim lstrDataFound As String
        Dim lstrDisabledByLevels As String
        Dim nTotalPremium As Double
        lblnOneCurren = (mclsCover.mclsCurren_pol.CountCurrenPol + 1) <= 1

        Response.Write("" & vbCrLf)
        Response.Write("        <TABLE WIDTH=""100%"" COLS=4>" & vbCrLf)
        Response.Write("            <TR>")


        '+ Si se trata de un colectivo
        If CStr(Session("sPoliType")) <> "1" And CStr(Session("sTyp_module")) = "3" Then

            Response.Write("" & vbCrLf)
            Response.Write("                <TD><LABEL ID=13052>" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("                <TD>" & vbCrLf)
            Response.Write("                ")


            With mobjValues.Parameters
                .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                mobjValues.BlankPosition = False

                Response.Write(mobjValues.PossiblesValues("valGroup", "tabgroups_coll", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCover.nGroup), True,  ,  ,  ,  , "ReloadPage()", CStr(Session("nCertif")) > "0",  , GetLocalResourceObject("valGroupToolTip")))
                'Response.Write mobjvalues.PossiblesValues("valGroup","tabGroups",eFunctions.Values.eValuesType.clngWindowType, mclsCover.nGroup,True,,,,,,True,, GetLocalResourceObject("valGroupToolTip"))
                Response.Write(mobjValues.HiddenControl("hddnGroup", CStr(mclsCover.nGroup)))
            End With

            Response.Write("" & vbCrLf)
            Response.Write("                </TD>")


        End If

        Response.Write("" & vbCrLf)
        Response.Write("                <TD><LABEL ID=13050>" & GetLocalResourceObject("cbeCurrencDesCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                <TD>" & vbCrLf)
        Response.Write("                ")


        mobjValues.TypeList = 1
        mobjValues.List = mclsCover.mclsCurren_pol.Charge_Combo
        mobjValues.BlankPosition = False
        Response.Write(mobjValues.PossiblesValues("cbeCurrencDes", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCover.nCurrency),  ,  ,  ,  ,  , "insReload()", lblnOneCurren,  , GetLocalResourceObject("cbeCurrencDesToolTip")))
        Response.Write("<SCRIPT> mintCurrencyChange = '" & mintCurrency & "'; </" & "Script>")
        Response.Write(mobjValues.HiddenControl("hddnProdclas", CStr(mclsCover.nProdClas)))

        Response.Write("" & vbCrLf)
        Response.Write("                </TD>" & vbCrLf)
        Response.Write("            </TR>")


        '+ Se crean los campos que solo aplican en el caso de vida.
        If CStr(Session("sBrancht")) = "1" Then
            Response.Write("" & vbCrLf)
            Response.Write("            <TR>" & vbCrLf)
            Response.Write("                 <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Datos del cliente seleccionado"">" & GetLocalResourceObject("AnchorDatos del cliente seleccionadoCaption") & "</A></LABEL></TD> " & vbCrLf)
            Response.Write("            </TR>" & vbCrLf)
            Response.Write("            <TR>" & vbCrLf)
            Response.Write("                <TD COLSPAN=""5"" CLASS=""Horline""></TD>        " & vbCrLf)
            Response.Write("            </TR>            " & vbCrLf)
            Response.Write("            <TR>" & vbCrLf)
            Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("cbeRoleCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("                <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCover.mclsRoles.nRole),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRoleToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("")

            If Not mclsCover.bNopayroll Then
                Response.Write("" & vbCrLf)
                Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("                <TD>")


                Response.Write(mobjValues.TextControl("tctClient", 14, mclsCover.mclsRoles.SCLIENT, , GetLocalResourceObject("tctClientToolTip"), True, , , , True))
                Response.Write(" - ")
                Response.Write(mobjValues.TextControl("tctClient", 14, mclsCover.mclsRoles.sDigit, , GetLocalResourceObject("tctClientToolTip"), True, , , , True))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("                <TD>")


                Response.Write(mobjValues.TextControl("tctCliename", 40, mclsCover.mclsRoles.sCliename,  , GetLocalResourceObject("tctClienameToolTip"), True,  ,  ,  , True))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("            </TR>" & vbCrLf)
                Response.Write("            <TR>" & vbCrLf)
                Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("tcdBirthdatCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("                <TD>")


                Response.Write(mobjValues.DateControl("tcdBirthdat", CStr(mclsCover.mclsRoles.dBirthdate),  , GetLocalResourceObject("tcdBirthdatToolTip"), True,  ,  ,  , True))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("cbeSexClienCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("                <TD>")


                Response.Write(mobjValues.PossiblesValues("cbeSexClien", "table18", eFunctions.Values.eValuesType.clngComboType, mclsCover.mclsRoles.sSexclien,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSexClienToolTip")))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("            </TR>" & vbCrLf)
                Response.Write("            <TR>" & vbCrLf)
                Response.Write("                <TD>")


                Response.Write(mobjValues.CheckControl("chkSmoking", GetLocalResourceObject("chkSmokingCaption"), mclsCover.mclsRoles.sSmoking,  ,  , True))


                Response.Write("</TD>            " & vbCrLf)
                Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgeCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("                <TD>")


                Response.Write(mobjValues.NumericControl("tcnAge", 2, CStr(mclsCover.mclsRoles.nAge),  , GetLocalResourceObject("tcnAgeToolTip"),  ,  , True,  ,  ,  , True))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgeInsCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("                <TD>")


                Response.Write(mobjValues.NumericControl("tcnAgeIns", 2, CStr(mclsCover.mclsRoles.nAge(True)),  , GetLocalResourceObject("tcnAgeInsToolTip"),  ,  , True,  ,  ,  , True))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("            </TR>" & vbCrLf)
                Response.Write("            <TR>")

            End If
            If Session("nCertif") > 0 Then

                Response.Write("" & vbCrLf)
                Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcnLEGAmountCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD COLSPAN=""2"">")


                Response.Write(mobjValues.NumericControl("tcnLEGAmount", 18, mdblLegAmount,  , GetLocalResourceObject("tcnLEGAmountToolTip"), True, 6, True))


                Response.Write("</TD>")

            End If
            Response.Write("" & vbCrLf)
            Response.Write("            </TR>" & vbCrLf)
            Response.Write("            <TR>" & vbCrLf)
            Response.Write("                <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("            </TR>" & vbCrLf)
            Response.Write("		")


        End If
        Response.Write(mobjValues.HiddenControl("hddnAge", CStr(mclsCover.mclsRoles.nAge)))
        Response.Write(mobjValues.HiddenControl("hddsSexclien", mclsCover.mclsRoles.sSexclien))
        Response.Write(mobjValues.HiddenControl("hddsVIP", mclsCover.mclsRoles.sVIP))
        Response.Write(mobjValues.HiddenControl("hddnTyperisk", CStr(mclsCover.mclsRoles.nTyperisk)))
        Response.Write(mobjValues.HiddenControl("tcnLeg", mdblLegAmount))

        '+ Si no se trata de consulta
        If Not mobjValues.ActionQuery Then
            '+ Si existen más de una moneda a tratar
            If Not lblnOneCurren Then
                Response.Write("<TD COLSPAN=""5"">" & "</TD>")
                Response.Write("<TD WIDTH=""5%"">" & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insAccept()",  , 10) & "</TD>")
            End If
        End If

        Response.Write("" & vbCrLf)
        Response.Write("        </TABLE>")


        Response.Write(mobjValues.HiddenControl("hddsKey", mclsCover.sKey))
        mobjGrid.Columns("hddsKeyGrid").DefValue = mclsCover.sKey

        '+ Si existe información para procesar
        If mblnFound Then
            lstrDataFound = "2"

            If mclsCover.mcolTCovers.bDataFound Then
                lstrDataFound = "1"
            End If
            Response.Write(mobjValues.HiddenControl("hddnDataFound", lstrDataFound))
            '+Si se encontraronn registros
            lintIndex = 0
            '+Se recorren las coberturas encontradas, para mostrarlas en el Grid
            For Each mclsTCover In mclsCover.mcolTCovers
                With mobjGrid
                    .Columns("Sel").Checked = mclsTCover.nSel(mclsCover.mcolTCovers.bDataFound)
                    .Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
                    .Columns("tcnModulec").DefValue = CStr(mclsTCover.nModulec)
                    .Columns("hddnModulec").DefValue = CStr(mclsTCover.nModulec)
                    .Columns("tcnCover").DefValue = CStr(mclsTCover.nCover)
                    .Columns("hddnCover").DefValue = CStr(mclsTCover.nCover)
                    .Columns("tctCover").DefValue = mclsTCover.sDescript
                    '+ Suma asegurada solicitada                
                    .Columns("tcnCapital").DefValue = CStr(mclsTCover.nCapital)
                    .Columns("hddnCapital").DefValue = CStr(mclsTCover.nCapital)
                    '+ Suma asegurada solicitada
                    If Session("nCertif") > 0 Then
                        .Columns("tcnCapital_wait").DefValue = CStr(mclsTCover.nCapital_wait)
                    End If

                    '+ Suma asegurada solicitada por el asegurado cambia solo en la emisión, endosos y recuperación
                    .Columns("tcnCapital_req").DefValue = CStr(mclsTCover.nCapital_req)
                    .Columns("hddnCapital_req").DefValue = CStr(mclsTCover.nCapital_req)

                    .Columns("hddnCapital_Wait").DefValue = CStr(mclsTCover.nCapital_wait)
                    '+ Tasa                
                    .Columns("tcnRateCove").DefValue = CStr(mclsTCover.nRateCove)
                    .Columns("hddnRateCove").DefValue = CStr(mclsTCover.nRateCove)
                    '+ Prima
                    .Columns("tcnPremium").DefValue = CStr(mclsTCover.nPremium)
                    If .Columns("Sel").Checked = CDbl("1") Then
                        nTotalPremium = nTotalPremium + mclsTCover.nPremium
                    End If
                    .Columns("hddnPremium").DefValue = CStr(mclsTCover.nPremium)
                    '+ Retarificación                
                    .Columns("cbeRetarif").DefValue = CStr(mclsTCover.nRetarif)
                    .Columns("hddnRetarif").DefValue = CStr(mclsTCover.nRetarif)
                    '+ Franq/Deduc
                    .Columns("cbeFrandedi").DefValue = mclsTCover.sFrandedi
                    .Columns("hddsFrandedi").DefValue = mclsTCover.sFrandedi

                    If mclsTCover.sFrandedi = vbNullString Then
                        .Columns("cbeFrandedi").DefValue = "1"
                        .Columns("hddsFrandedi").DefValue = "1"
                    End If
                    '+ F/D %
                    .Columns("tcnFraRate").DefValue = CStr(mclsTCover.nRate)
                    .Columns("hddnFraRate").DefValue = CStr(mclsTCover.nRate)
                    '+ F/D Aplica Sobre
                    .Columns("cbeFrancapl").DefValue = mclsTCover.sFrancApl
                    .Columns("hddsFrancApl").DefValue = mclsTCover.sFrancApl
                    '+ F/D Monto fijo        
                    .Columns("tcnFixamount").DefValue = CStr(mclsTCover.nFixamount)
                    .Columns("hddnFixAmount").DefValue = CStr(mclsTCover.nFixamount)
                    '+ F/D Monto minimo                
                    .Columns("tcnMinAmount").DefValue = CStr(mclsTCover.nMinamount)
                    .Columns("hddnMinAmount").DefValue = CStr(mclsTCover.nMinamount)
                    '+ F/D Monto máximo                
                    .Columns("tcnMaxAmount").DefValue = CStr(mclsTCover.nMaxamount)
                    .Columns("hddnMaxAmount").DefValue = CStr(mclsTCover.nMaxamount)
                    '+ Tipo de carencia
                    .Columns("cbeWait_type").DefValue = mclsTCover.sWait_type
                    .Columns("hddsWait_type").DefValue = mclsTCover.sWait_type
                    '+ Duración de carencia                
                    .Columns("tcnWaitQ").DefValue = CStr(mclsTCover.nWait_quan)
                    .Columns("hddnWaitQ").DefValue = CStr(mclsTCover.nWait_quan)

                    .Columns("tcnDisc_amoun").DefValue = CStr(mclsTCover.nDisc_amoun)
                    .Columns("hddnDisc_amoun").DefValue = CStr(mclsTCover.nDisc_amoun)

                    .Columns("tcnDiscount").DefValue = CStr(mclsTCover.nDiscount)
                    .Columns("hddnDiscount").DefValue = CStr(mclsTCover.nDiscount)

                    '+ Fecha de endoso retroactivo
                    .Columns("tcdFer").DefValue = CStr(mclsTCover.dFer)
                    .Columns("hdddfer").DefValue = CStr(mclsTCover.dFer)

                    '+ Motivo de modificación
                    .Columns("cbeCauseupd").DefValue = CStr(mclsTCover.nCauseupd)
                    .Columns("cbeCauseupd").Descript = mclsTCover.sdesc_t5547
                    .Columns("hddnCauseupd").DefValue = CStr(mclsTCover.nCauseupd)

                    '+ Ramo de reaseguro                
                    .Columns("valBranch_rei").DefValue = CStr(mclsTCover.nBranch_rei)
                    .Columns("hddnBranch_Rei").DefValue = CStr(mclsTCover.nBranch_rei)

                    .Columns("hddnAgeIns").DefValue = CStr(mclsCover.mclsRoles.nAge)
                    .Columns("hddnPremifix").DefValue = CStr(mclsTCover.nPremifix)
                    .Columns("hddnPremiRat").DefValue = CStr(mclsTCover.nPremiRat)
                    .Columns("hddnCoverApl").DefValue = CStr(mclsTCover.nCoverApl)
                    .Columns("hddnCover_in").DefValue = CStr(mclsTCover.nCover_in)
                    .Columns("hddnPremimin").DefValue = CStr(mclsTCover.nPremimin)
                    .Columns("hddnPremiMax").DefValue = CStr(mclsTCover.nPremiMax)
                    .Columns("hddnGenCurrency").DefValue = CStr(mclsTCover.nTarifCurr)
                    .Columns("hddnCapital_o").DefValue = CStr(mclsTCover.nCapital_o)
                    .Columns("hddnRatecove_o").DefValue = CStr(mclsTCover.nRateCove_o)
                    .Columns("hddnPremium_o").DefValue = CStr(mclsTCover.nPremium_o)
                    .Columns("hddsChange").DefValue = mclsTCover.sChange
                    .Columns("hddsRequire").DefValue = mclsTCover.sRequired
                    .Columns("hddSeekTar").DefValue = CStr(mclsTCover.dSeekTar)
                    .Columns("hddsRoupremi").DefValue = mclsTCover.sRoupremi
                    .Columns("hddsCh_typ_cap").DefValue = mclsTCover.sCh_typ_cap
                    .Columns("hddsChange_typ").DefValue = mclsTCover.sChange_typ
                    .Columns("hddnApply_perc").DefValue = CStr(mclsTCover.nApply_perc)
                    .Columns("hddsExist").DefValue = mclsTCover.sExist
                    .Columns("hddsBas_sumins").DefValue = mclsTCover.sBas_sumins

                    If CStr(Session("sBrancht")) = "1" Then
                        If Not mclsCover.bNopayroll Then
                            .Columns("tcnAgeminins").DefValue = CStr(mclsTCover.nAgeminins)
                            .Columns("tcnAgemaxins").DefValue = CStr(mclsTCover.nAgemaxins)
                            .Columns("tcnAgemaxper").DefValue = CStr(mclsTCover.nAgemaxper)
                        End If
                        '+ Edad minimo                    
                        .Columns("hddnAgeminins").DefValue = CStr(mclsTCover.nAgeminins)
                        .Columns("hddnAgemininsf").DefValue = CStr(mclsTCover.nAgeminins)
                        '+ Edad máximo
                        .Columns("hddnAgemaxins").DefValue = CStr(mclsTCover.nAgemaxins)
                        .Columns("hddnAgemaxinsf").DefValue = CStr(mclsTCover.nAgemaxins)
                        '+ Edad máxima de permanencia                        
                        .Columns("hddnAgemaxper").DefValue = CStr(mclsTCover.nAgemaxper)
                        .Columns("hddnAgemaxperf").DefValue = CStr(mclsTCover.nAgemaxper)

                        'If mclsCover.nProdClas = 1 Or mclsCover.nProdClas = 7 Then
                        '+ Tipo de duración del seguro                           
                        .Columns("cbeTypdurins").DefValue = CStr(mclsTCover.nTypdurins)
                        .Columns("hddnTypdurins").DefValue = CStr(mclsTCover.nTypdurins)
                        '+ Duración del seguro
                        If mclsTCover.nTypdurins <> 3 Then
                            .Columns("tcnDurinsur").DefValue = CStr(mclsTCover.nDurinsur)
                            .Columns("hddnDurinsur").DefValue = CStr(mclsTCover.nDurinsur)
                        End If

                        '+ Tipo de duración del pago                            
                        .Columns("cbeTypdurpay").DefValue = CStr(mclsTCover.nTypdurpay)
                        .Columns("hddnTypdurpay").DefValue = CStr(mclsTCover.nTypdurpay)
                        '+ Duración del pago                                                       
                        .Columns("tcnDurpay").DefValue = CStr(mclsTCover.nDurpay)
                        .Columns("hddnDurpay").DefValue = CStr(mclsTCover.nDurpay)
                        'End If
                    End If


                    .Columns("hddnCacalfix").DefValue = CStr(mclsTCover.nCacalfix)
                    .Columns("hddsCacalfri").DefValue = mclsTCover.sCacalfri
                    .Columns("hddsCacalili").DefValue = mclsTCover.sCacalili
                    .Columns("hddnCacalcov").DefValue = CStr(mclsTCover.nCacalcov)
                    .Columns("hddnCacalper").DefValue = CStr(mclsTCover.nCacalper)
                    .Columns("hddnRolcap").DefValue = CStr(mclsTCover.nRolcap)
                    .Columns("hddsRoucapit").DefValue = mclsTCover.sRoucapit
                    .Columns("hddnCamaxcov").DefValue = CStr(mclsTCover.nCamaxcov)
                    .Columns("hddnCamaxper").DefValue = CStr(mclsTCover.nCamaxper)
                    .Columns("hddnCamaxrol").DefValue = CStr(mclsTCover.nCamaxrol)
                    .Columns("hddnCacalmul").DefValue = CStr(mclsTCover.nCacalmul)
                    .Columns("hddnCacalmin").DefValue = CStr(mclsTCover.nCacalmin)
                    .Columns("hddnCacalmax").DefValue = CStr(mclsTCover.nCacalmax)


                    If mblnDisabledByLevels Then
                        lstrDisabledByLevels = "1"
                    Else
                        lstrDisabledByLevels = "2"
                    End If

                    .sEditRecordParam = "' + 'nProdclas=' + self.document.forms[0].hddnProdclas.value + " & "'&nAge=' + (typeof(self.document.forms[0].hddnAge)!='undefined'?self.document.forms[0].hddnAge.value:'') + " & "'&nRole=" & mstrRole & "' + " & "'&sClient=" & mstrClient & "' + " & "'&sDisabledByLevels=" & lstrDisabledByLevels & "' + " & "'&nIndexCover=" & Request.QueryString.Item("nIndexCover") & "' + " & "'&nLegAmount=" & mclsCover.nLegAmount & "' + " & "'&VIP=' + (typeof(self.document.forms[0].hddsVIP)!='undefined'?self.document.forms[0].hddsVIP.value:'') + " & "'&sNopayroll=' + self.document.forms[0].hddsNopayroll.value + " & "'&nTypdurpay=" & mclsTCover.nTypdurpay & "'+" & "'&nTyperisk=' + self.document.forms[0].hddnTyperisk.value" & " + '"



                    Response.Write(.doRow)
                End With
                lintIndex = lintIndex + 1
            Next mclsTCover
        Else
            '+ Si existe algún error
            If mclsCover.nError > 0 Then
                mclsGeneral = New eFunctions.Errors
                '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
                mclsGeneral.sSessionID = Session.SessionID
                mclsGeneral.nUsercode = Session("nUsercode")
                '~End Body Block VisualTimer Utility
                Response.Write(mclsGeneral.ErrorMessage(Request.QueryString.Item("sCodispl"), mclsCover.nError,  ,  ,  , True))
                mclsGeneral = Nothing
            End If
        End If

        '+Se cierra el recorrido de la tabla 
        Response.Write(mobjGrid.CloseTable())
        Response.Write(mobjValues.HiddenControl("hddnCount", lintIndex))

        Response.Write("" & vbCrLf)
        Response.Write("<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("    <TD ALIGN=RIGHT><LABEL ID=40784>" & GetLocalResourceObject("AnchorCaption") & "</LABEL>" & vbCrLf)
        Response.Write("    ")


        If mblnFound Then
            Response.Write(mobjValues.DIVControl("tcnTotPremium", True, mobjValues.TypeToString(nTotalPremium, eFunctions.Values.eTypeData.etdDouble, True, 6)))
            Response.Write("<SCRIPT>")
            'Response.Write "InsCalTotalPremium();"
            Response.Write("self.document.forms[0].action = '" & "ValPolicySeq.aspx?nRole=" & mstrRole & "&sClient=" & mstrClient & "&nIndexCover=" & Request.QueryString.Item("nIndexCover") & "';")
            Response.Write("</" & "Script>")
        End If

        Response.Write("</TD>" & vbCrLf)
        Response.Write("</TABLE>")


        Response.Write(mobjValues.BeginPageButton)
        If mclsCover.bNopayroll Then
            Response.Write("<SCRIPT>self.document.forms[0].hddsNopayroll.value=1</" & "Script>")
        End If
    End Function

    '%insPreCA014Upd. Esta ventana se encarga de mostrar el código correspondiente a la
    '%actualización de las coberturas.
    '---------------------------------------------------------------------------------------
    Private Sub insPreCA014Upd()
        '---------------------------------------------------------------------------------------
        If Request.QueryString.Item("Action") <> "Del" Then
            With mobjGrid
                .Columns("tcnPremium").OnChange = "insCalCapital(""1"")"
                .Columns("tcnCapital").OnChange = "insCalPremium(""1"")"
                .Columns("tcnRatecove").OnChange = "insCalPremium(""2"")"
                .Columns("cbeRetarif").OnChange = "insCalPremium(""1"")"
            End With

            With Response
                .Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicySeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
                .Write(mobjValues.HiddenControl("hddnCurrency", CStr(0)))
                .Write(mobjValues.HiddenControl("hddnGroup", vbNullString))
                .Write(mobjValues.HiddenControl("hddnLegAmount", mdblLegAmount))
                .Write(mobjValues.HiddenControl("hddsClient", mstrClient))
                .Write(mobjValues.HiddenControl("hddsIndexCover", Request.QueryString.Item("nIndexCover")))
                .Write(mobjValues.HiddenControl("hddsVIP", Request.QueryString.Item("VIP")))
                .Write(mobjValues.HiddenControl("hddnTyperisk", Request.QueryString.Item("nTyperisk")))
            End With
            If Not mobjValues.ActionQuery Then
                Response.Write("<SCRIPT>" & vbCrLf & "    if(typeof(top.opener.document.forms[0].valGroup)!='undefined')" & vbCrLf & "        top.frames['fraFolder'].document.forms[0].hddnGroup.value = top.opener.document.forms[0].valGroup.value;" & vbCrLf & "    else" & vbCrLf & "        top.frames['fraFolder'].document.forms[0].hddnGroup.value = 0;" & vbCrLf & "    top.frames['fraFolder'].document.forms[0].hddnCurrency.value = top.opener.document.forms[0].cbeCurrencDes.value;" & vbCrLf & "    " & vbCrLf & "</" & "Script>")


                If Not mblnDisabledByLevels Then
                    Response.Write("<SCRIPT>insEnableControls('');</" & "Script>")

                End If
            End If
        End If
    End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA014")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjValues.ActionQuery = Session("bQuery")
	If Not mobjValues.ActionQuery Then
		mclsGeneral = New eGeneral.GeneralFunction
		mstrError = mclsGeneral.insLoadMessage(55963)
		mclsGeneral = Nothing
	End If
End With


mstrClient = Request.QueryString.Item("sClient")
mstrRole = Request.QueryString.Item("nRole")
%>



<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 7-08-09 12:23 $|$$Author: Jsarabia $"

    var mintGroupChange = 0;
    var mintCurrencyChange = 0;

//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        self.document.forms[0].hddbPuntual.value = true;
    }
    top.frames['fraHeader'].ClientRequest(390,2);
}

//% InsCalTotalPremium: Calcula la prima total de las coberturas seleccionadas
//-------------------------------------------------------------------------------------------
function InsCalTotalPremium(){
//-------------------------------------------------------------------------------------------
	var ldblPremium = 0;
	for(var lintIndex=0; lintIndex<marrArray.length;lintIndex++){
		if (marrArray[lintIndex].Sel){
			if (marrArray[lintIndex].tcnPremium == '') marrArray[lintIndex].tcnPremium = 0;
			ldblPremium += insConvertNumber(marrArray[lintIndex].tcnPremium);
		}
	}

	
    UpdateDiv('tcnTotPremium', VTFormat(ldblPremium,'', '', '', 6, true));
}

//% insCheckSelClick: controla la columna Sel, para mostrar la ventana PopUp    
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrQueryString;
    var lstrString;
    var lstrString_Val;
    var lintAge = '';
	var lstrTotPremium = '';
	var lstrAction;
	var lstrChecked;
    var lstrReset = '2';

	if (typeof(mstrDoSubmit) == 'undefined') mstrDoSubmit = '1';
	if (mstrDoSubmit == '1'){
        mstrDoSubmit = '2';
		if (typeof(self.document.forms[0].hddnAge) != 'undefined'){
			lintAge = self.document.forms[0].hddnAge.value;
		}
		lstrQueryString = '&nAge=' + lintAge;

		if (!Field.checked){
			if (marrArray[lintIndex].hddsRequire=="1"){
			    alert("Error 55963:" + "<%=mstrError%>");
			    Field.checked = !Field.checked;
			    marrArray[lintIndex].Sel = 1;
                lstrReset = '1';
			}
			else{
				lstrString = 'sKey=' + marrArray[lintIndex].hddsKeyGrid;
				lstrString = lstrString + '&nModulec=' + marrArray[lintIndex].tcnModulec;
				lstrString = lstrString + '&nCover=' + marrArray[lintIndex].tcnCover;
				lstrString = lstrString + '&nRole=' + '<%=mstrRole%>';
				lstrString = lstrString + '&sClient=' + '<%=mstrClient%>';
				 
				if(typeof(self.document.forms[0].valGroup)!='undefined'){
				    lstrString = lstrString + "&nGroup=" + self.document.forms[0].valGroup.value;
				}
				else{
					lstrString = lstrString + "&nGroup=0"
				}
				lstrString = lstrString + lstrTotPremium
				setPointer('wait');
				if ('<%=Session("sBrancht")%>' == '1'){
			        mstrDoSubmit = '1';
			        lstrAction = 'Del';
			        lstrChecked = '!';
				}
				else{
				    insDefValues("DelTCover", lstrString, '/VTimeNet/Policy/PolicySeq');
				    InsCalTotalPremium();
				}
			}
		}
		else {
			mstrDoSubmit = '1';
			lstrAction   = 'Update';
	        lstrChecked = '';
		}

        if (mstrDoSubmit == '1'){
		    lstrString_Val = '&nRole=' + '<%=mstrRole%>';
		    lstrString_Val = lstrString_Val + '&nGroup=' + 0;
		    lstrString_Val = lstrString_Val + '&nAge=' + lintAge;
		    lstrString_Val = lstrString_Val + '&sClient=' + '<%=mstrClient%>';
		    lstrString_Val = lstrString_Val + '&nModulec=' + marrArray[lintIndex].tcnModulec;
		    lstrString_Val = lstrString_Val + '&nCover=' + marrArray[lintIndex].tcnCover;
		    lstrString_Val = lstrString_Val + '&nCurrency=' + self.document.forms[0].cbeCurrencDes.value;
		    lstrString_Val = lstrString_Val + '&nProdclas=' + self.document.forms[0].hddnProdclas.value;
		    lstrString_Val = lstrString_Val + '&nIndexCover=' + '<%=Request.QueryString.Item("nIndexCover")%>';
		    lstrString_Val = lstrString_Val + '&sChecked=' + lstrChecked;
		    lintIndex+=1;
		  
			mstrDoSubmit = '2';
		    document.forms[0].action ="ValPolicySeq.aspx?nZone=2&sCodispl=CA014&Action=" + lstrAction + "&WindowType=PopUp&nMainAction=304&ActionType=Check&nIndex=" + lintIndex + lstrString_Val
		    top.frames['fraFolder'].document.forms[0].target="fraGeneric";
		    self.document.forms[0].cbeCurrencDes.disabled = false;
		    setPointer('wait');
		    self.document.forms[0].submit(); 
		    self.document.forms[0].cbeCurrencDes.disabled = true;
        }

        // + Cuando se envia la validacion 55963 la variable mstrDoSubmit queda en 2 porque no se ejecuta la página VAL, 
        // + al quedar asi siempre se envia el 
        // + mensaje "Por favor espere" al marcar cualquier cobertura. 
        // + La razon por la que se coloca el en 1 aca, es porque si se hace en el punto donde se envia el alert 55963, se ejecuta el VAL
        // + y es incorrecto
        if(lstrReset == '1')
            mstrDoSubmit = '1';
	}
	else{
	    Field.checked = !Field.checked;
		alert('Por favor espere');
	}
}

//% insDisabled: Se encarga de desabiltar el boton de aceptar.
//-------------------------------------------------------------------------------------------
function insDisabled(){
//-------------------------------------------------------------------------------------------
	top.frames['fraHeader'].document.A390.disabled=false;
}

//% insReload: Se encarga de recargar la página al seleccionar cualquier valor de los campos del encabezado del grid.
//-------------------------------------------------------------------------------------------
function insReload(){
//-------------------------------------------------------------------------------------------
    var lstrQuery
    var lblnChange
            
    with (self.document.forms[0]) {
         lstrQuery = "&sKey=" + hddsKey.value;
//+ Caso en que el grupo esté visible
        if(typeof(valGroup)!='undefined'){
            if (mintGroupChange!=valGroup.value)
                lblnChange = true
                mintGroupChange = valGroup.value;
            lstrQuery = lstrQuery + "&nGroup=" + valGroup.value
        } else
            lstrQuery = lstrQuery + "&nGroup=0"
        
        if (mintCurrencyChange!=cbeCurrencDes.value) {
            mintCurrencyChange = cbeCurrencDes.value;
            lblnChange = true;
        }

//+ Si hubo algún cambio en cuanto al grupo (si corresponde) o la moneda; se recarga la ventana.
        if (lblnChange==true) {
            lstrQuery = lstrQuery + "&nCurrency=" + cbeCurrencDes.value + "&sClient=" + '<%=mstrClient%>' + "&nRole=" + '<%=mstrRole%>' + "&sDelTCover=";
            document.location.href = document.location.href.replace(/&nRole=.*/,'') + lstrQuery
        }
    }
} 

//% insEnableControls: se habilitan/deshabilitan los campos de la página
//---------------------------------------------------------------------------------
function insEnableControls(Field){
//---------------------------------------------------------------------------------
   with (document.forms[0]){
        cbeCauseupd.disabled = (cbeCauseupd.disabled || cbeCauseupd.value == 12) //12-Rescate
        btncbeCauseupd.disabled = (cbeCauseupd.disabled)
        tcnFraRate.disabled = (cbeFrandedi.value == 1)
        cbeFrancApl.disabled = (cbeFrandedi.value == 1)
        cbeFrancApl.value = (cbeFrandedi.value==1?1:cbeFrancApl.value)
        tcnFixamount.disabled = (cbeFrandedi.value == 1)
        tcnMinAmount.disabled = (cbeFrandedi.value == 1)
        tcnMaxAmount.disabled = (cbeFrandedi.value == 1)
        tcnDiscount.disabled = (cbeFrandedi.value == 1)
        tcnDisc_amoun.disabled = (cbeFrandedi.value == 1)
        cbeWait_type.disabled = (hddDisabledPreExists.value == 1)
        tcnWaitQ.disabled = (cbeWait_type.value == 1 || hddDisabledPreExists.value == 1)
        tcnWaitQ.value = (cbeWait_type.value==1?'':tcnWaitQ.value)
        
        if (cbeFrandedi.value==1) {
            tcnFixamount.value = '0';
            tcnFraRate.value = '0,00';
            tcnMaxAmount.value = '0';
            tcnMinAmount.value = '0';
            tcnDiscount.value = '0';
            tcnDisc_amoun.value = '0,00';
        }
        
//+ Si el campo porcentaje de f/d tiene valor se inicializa el monto fijo de f/d
        if (Field.name=='tcnFraRate')
            if (tcnFraRate.value>0)
                tcnFixamount.value = '0'
            
//+ Si el campo monto fijo de f/d tiene valor se inicializa el porcentaje de f/d
        if (Field.name=='tcnFixamount')
            if (tcnFixamount.value>0)
                tcnFraRate.value = '0,00'
        
//+ Si el campo porcentaje de descuento de f/d tiene valor se inicializa el monto de descuento de f/d
        if (Field.name=='tcnDiscount')
            if (tcnDiscount.value>0)
                tcnDisc_amoun.value = '0,00'
        
//+ Si el campo monto de descuento de f/d tiene valor se inicializa el porcentaje de descuento de f/d
        if (Field.name=='tcnDisc_amoun')
            if (tcnDisc_amoun.value>0)
                tcnDiscount.value = '0,00'
                
   }
    
}

//% insCalPremium: se recarga la página en caso que se modifique la prima o tasa,
//%                   para calcular los valores de manera autómatica
//% sOrigen: Es para verificar si el procedimiento se llama de tasa, Capital o Retarifica
//----------------------------------------------------------------------------------------
function insCalPremium(sOrigen){
//----------------------------------------------------------------------------------------
    var lstrQueryString
    var llngCapital
    var llngCapital_o
    var llngRatecove
    var llngRatecove_o
    var llngTypdurins
    var llngTypdurpay
	var llngTransaction
	var lblnamendment
	var lblnRetarif

	llngTransaction = <%=Session("nTransaction")%>
	
    llngTypdurins = '';
    llngTypdurpay = '';
    with (self.document.forms[0]){
        llngCapital    = insConvertNumber(tcnCapital.value);
        llngRatecove   = insConvertNumber(tcnRatecove.value);
		lblnRetarif   = insConvertNumber(hddnRetarif.value);
		//if (lblnRetarif != 9)
		//	lblnRetarif = 8;
		
//+ Si existe modificación en los campos Suma Asegurada y prima (sólo si se ha cambiado)
        llngCapital_o  = insConvertNumber(hddnCapital.value);
        llngRatecove_o = insConvertNumber(hddnRateCove.value);

		if (typeof(cbeTypdurins) != 'undefined'){
			llngTypdurins = cbeTypdurins.value;
			llngTypdurpay = cbeTypdurpay.value;
		}

        if (llngCapital != llngCapital_o || 
            llngRatecove != llngRatecove_o ||
            cbeRetarif.value != hddnRetarif.value){

			if (llngTransaction == 12 ||
			    llngTransaction == 13 ||
			    llngTransaction == 14 ||
			    llngTransaction == 15 ||
			    llngTransaction == 24 ||
			    llngTransaction == 25 ||
			    llngTransaction == 26 ||
			    llngTransaction == 27 )
			    if (cbeRetarif.value == 1)
					if (llngCapital != llngCapital_o || 
						llngRatecove != llngRatecove_o)
						lblnamendment = false;
					else	
						lblnamendment = true;
			    else
					lblnamendment = false;
			else
			    lblnamendment = false;
			        
            if (!lblnamendment) {
            lstrQueryString = 'nCover=' + tcnCover.value + 
                              '&nModulec=' + tcnModulec.value +
                              '&nGroup=' + hddnGroup.value +
                              '&nRetarif='  + lblnRetarif +
                              '&nCover_in=' + hddnCover_in.value +
                              '&sRoupremi=' + hddsRoupremi.value +
                              '&nCurrencyOri=' + hddnCurrency.value +
                              '&nCurrencyDes=' + hddnGenCurrency.value +
                              '&nRole=' + '<%=mstrRole%>' +
                              '&sClient=' + '<%=mstrClient%>' +
                              '&sKey=' + hddsKeyGrid.value +
                              '&nPremifix=' + hddnPremifix.value +
                              '&nPremirat=' + hddnPremiRat.value +
                              '&nCoverapl=' + hddnCoverApl.value +
                              '&dSeektar=' + hddSeekTar.value +
                              '&sBrancht=' + '<%=Session("sBrancht")%>' +
                              '&nApply_perc=' + hddnApply_perc.value +
                              '&nPremimin=' + hddnPremimin.value +
                              '&nPremimax=' + hddnPremiMax.value +
                              '&nCapital=' + tcnCapital.value +
                              '&nRatecove=' + tcnRatecove.value +
                              '&nRatecove_o=' + hddnRatecove_o.value +
                              '&nPremium=' + tcnPremium.value +
                              '&nTypdurins=' + llngTypdurins +
                              '&nTypdurpay=' + llngTypdurpay +
                              '&sOrigen=' + sOrigen +
                              '&sExist=' + hddsExist.value + 
                              '&nDurinsur=' + hddnDurinsur.value + 
                              '&nDurpay=' + hddnDurpay.value + 
                              '&sBas_sumins=' + hddsBas_sumins.value;
            insDefValues("Premium", lstrQueryString, '/VTimeNet/Policy/PolicySeq');
            }
        }
    }
}

//% insCalCapital: se recarga la página en caso que se modifique la prima o tasa,
//%                   para calcular los valores de manera autómatica
//% sOrigen: Es para verificar si el procedimiento se llama de tasa, Capital o Retarifica
//----------------------------------------------------------------------------------------
function insCalCapital(sOrigen){
//----------------------------------------------------------------------------------------
    var lstrQueryString
    var llngCapital
    var llngCapital_o
    var llngTypdurins
    var llngTypdurpay
    var llngAgeminins
    var llngAgemaxins

    llngTypdurins = '';
    llngTypdurpay = '';
    llngAgeminins = '';
    llngAgemaxins = '';
    
    
    with (self.document.forms[0]){
        llngCapital   = insConvertNumber(tcnCapital.value);
//+ Si existe modificación en los campos Suma Asegurada y prima (sólo si se ha cambiado)
        llngCapital_o = insConvertNumber(hddnCapital.value);

		if (typeof(cbeTypdurins) != 'undefined'){
			llngTypdurins = cbeTypdurins.value;
			llngTypdurpay = cbeTypdurpay.value;
		}
		
		if (typeof(tcnAgeminins) != 'undefined') {
		    llngAgeminins = tcnAgeminins.value;
            llngAgemaxins = tcnAgemaxins.value;
		}

        lstrQueryString = 'nCover=' + tcnCover.value + 
                          '&nModulec=' + tcnModulec.value +
                          '&nCacalfix=' + hddnCacalfix.value + // falta esta
                          '&sCacalfri=' + hddsCacalfri.value + // falta esta
                          '&sCacalili=' + hddsCacalili.value + // falta esta
                          '&nCacalcov=' + hddnCacalcov.value + // falta esta
                          '&nCacalper=' + hddnCacalper.value + // falta esta
                          '&sKey=' + hddsKeyGrid.value +
                          '&nRolcap=' + hddnRolcap.value + // falta esta
                          '&sRoucapit=' + hddsRoucapit.value + // falta
                          '&nRole=' + '<%=mstrRole%>' +
                          '&sClient=' + '<%=mstrClient%>' +
                          '&sBrancht=' + '<%=Session("sBrancht")%>' +
                          '&nCurrencyOri=' + hddnCurrency.value +
                          '&nCamaxcov=' + hddnCamaxcov.value + // falta
                          '&nCamaxper=' + hddnCamaxper.value + // falta
						  '&nCamaxrol=' + hddnCamaxrol.value + // falta
                          '&nCacalmul=' + hddnCacalmul.value + // falta
                          '&nCurrencyDes=' + hddnGenCurrency.value +
                          '&nGroup=' + hddnGroup.value +
                          '&nAgeminins=' + llngAgeminins +
                          '&nAgemaxins=' + llngAgemaxins +
                          '&sBas_sumins=' + hddsBas_sumins.value +
                          '&nTypdurins=' + llngTypdurins +
                          '&nTypdurpay=' + llngTypdurpay +
                          '&nPremium=' + tcnPremium.value +
                          '&Capital_wait=' + hddnCapital_Wait.value +
                          '&nCacalmin=' + hddnCacalmin.value + // falta esta
                          '&nCacalmax=' + hddnCacalmax.value + // falta esta
                          '&nCapital=' + tcnCapital.value;
			insDefValues("Capital", lstrQueryString, '/VTimeNet/Policy/PolicySeq');
    }
}
</SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & ";</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CA014" ACTION="ValPolicySeq.aspx?nRole=<%=mstrRole%>&sClient=<%=mstrClient%>&nIndexCover=<%=Request.QueryString.Item("nIndexCover")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
mclsCover = New ePolicy.Cover
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA014Upd()
Else
	Call insPreCA014()
End If
mclsCover = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

<%
'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
Call mobjNetFrameWork.FinishPage("CA014")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>
