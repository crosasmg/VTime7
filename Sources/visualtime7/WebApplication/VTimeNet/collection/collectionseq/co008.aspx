<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones  generales de carga de valores
    Dim mobjValues As New eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenues As eFunctions.Menues
    Dim mstrDocNumber As Integer
    Dim ldbldefaultUF As Double
    Dim lblnAccess As Boolean
    Dim lobjErrors As eGeneral.GeneralFunction
    Dim lstrAlert As Object
    Dim lclsT_DocTyp As eCollection.T_DocTyp
    Dim mclsCashMovs As eCollection.CashBankAccMovs

    Dim mintDefValuePay As Double
    Dim mlngCount As Integer
    Dim mdblPaidAmount As Double
    Dim mdblTotalAmount As Double
    Dim mdblTotalAmountGen As Double
    Dim mdblTotalAmountGenDec As Double
    Dim mdblExchangeUF As Double
    Dim mstrTable5008 As String


    '%insPrevInf(). Este procedimiento se encarga de cargar los valores a utilizar en la página.
    '---------------------------------------------------------------------------------------
    Private Sub insPrevInf()
        '---------------------------------------------------------------------------------------
        Dim ldblTotals As Double
        Dim ldblTotals_loc As Double

        Call mclsCashMovs.Find("CO008", Request.QueryString.Item("Type"), Session("nBordereaux"), Session("CO001_nAction"), Session("sStatus"), Session("dCollectDate"), Session("dValueDate"), Session("sRelOrigi"))

        mlngCount = mclsCashMovs.nCount
        mdblPaidAmount = System.Math.Round(mclsCashMovs.nPaidAmount)
        mdblTotalAmount = System.Math.Round(mclsCashMovs.nTotalAmount)
        mdblTotalAmountGen = mclsCashMovs.nTotalAmountGen
        mdblTotalAmountGenDec = mclsCashMovs.nTotalAmountGenDec
        mdblExchangeUF = mclsCashMovs.nExchangeUF
        mstrTable5008 = mclsCashMovs.sTable5008

    End Sub

    '%insDefineHeader(). Este procedimiento se encarga de definir las líneas del encabezado
    '%del grid.
    '---------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '---------------------------------------------------------------------------------------
        Dim lobjColumn As eFunctions.Column
        Dim lobjGeneral As eGeneral.Exchange
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.59.53
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        mobjGrid.sCodisplPage = "CO008"
        Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

        mobjGrid.ActionQuery = ((CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery)))
        '+Se define variable para manejar diferencias con Renta Vitalicia
        If CStr(Session("chkRentVital")) = "9" Or CStr(Session("chkRentVital")) = "10" Then
            Response.Write(mobjValues.HiddenControl("hddnRVFlag", "1"))
        Else
            Response.Write(mobjValues.HiddenControl("hddnRVFlag", "2"))
        End If

        '+Se definen todas las columnas del Grid
        If Request.QueryString.Item("Type") <> "PopUp" And Not mobjGrid.ActionQuery Then

            Response.Write("" & vbCrLf)
            Response.Write("    <TABLE WIDTH=""100%"" CELLSPACING=""10"">" & vbCrLf)
            Response.Write("        <TR ALIGN=RIGTH>" & vbCrLf)
            Response.Write("			<TD>")


            Response.Write(mobjValues.CheckControl("chkSelAll", GetLocalResourceObject("chkSelAllCaption"),  , "1", "insCheckAll(this)", (CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery)) Or (CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrModify) And (Session("nCashNum") = 0 Or CStr(Session("nCashNum")) = "")) Or (CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrUpdate) And (Session("nCashNum") = 0 Or CStr(Session("nCashNum")) = ""))))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("    </TABLE>")


        End If

        With mobjGrid.Columns
            Call .AddHiddenColumn("sType", "")
            Call .AddHiddenColumn("nTransac", CStr(0))
            Call .AddHiddenColumn("sOrigiAction", "")
            Call .AddHiddenColumn("nSequence", "0")

            Call .AddNumericColumn(5, GetLocalResourceObject("tcnCashIdColumnCaption"), "tcnCashId", 10,  ,  , GetLocalResourceObject("tcnCashIdColumnToolTip"),  ,  ,  ,  , "insCashNum(this)", True)

            If CStr(Session("Finan_Interest")) = "1" Then
                lobjColumn = .AddPossiblesColumn(1, GetLocalResourceObject("nTypPayColumnCaption"), "nTypPay", "tabTable182_range", eFunctions.Values.eValuesType.clngComboType, mintDefValuePay, True,  ,  ,  , "insParameterCurrency();ChangedEffecdate(" & Session("dCollectDate") & ",this.value);insLockControl(this.value," & mclsCashMovs.nOperational & ");insShowRentValues(this.value);insShowAccount(this.value);",  ,  , GetLocalResourceObject("nTypPayColumnToolTip"))
            Else
                lobjColumn = .AddPossiblesColumn(1, GetLocalResourceObject("nTypPayColumnCaption"), "nTypPay", "tabTable182_range", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  , "insParameterCurrency();ChangedEffecdate(" & Session("dCollectDate") & ",this.value);insLockControl(this.value," & mclsCashMovs.nOperational & ");insShowRentValues(this.value);insShowAccount(this.value);setTimeout('insShowLocalAmount()',700);",  ,  , GetLocalResourceObject("nTypPayColumnToolTip"))
            End If
            If CStr(Application("cstrTypeCompany")) = "3" Then
                lobjColumn.Parameters.Add("sOper_List", "(4,8,9,11,12,13,14,15,16,18,19,20,21,22,23,24,90)", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                lobjColumn.Parameters.Add("sOper_List", "(4,6,8,9,11,13,14,15,16,17,18,19,20,21,22,23,24,25,90)", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            lobjColumn.EditRecord = True
            Call .AddDateColumn(2, GetLocalResourceObject("dDoc_dateColumnCaption"), "dDoc_date",  ,  , GetLocalResourceObject("dDoc_dateColumnToolTip"),  ,  ,  , True)
            Call .AddClientColumn(40590, GetLocalResourceObject("sClientColumnCaption"), "sClient", "",  , GetLocalResourceObject("sClientColumnToolTip"), "insParameterCurrency('Cliente');", True)

            'Call .AddClientColumn(40590, GetLocalResourceObject("sClientColumnCaption"),"sClient","",, GetLocalResourceObject("sClientColumnToolTip"),,True)

            Call .AddPossiblesColumn(3, GetLocalResourceObject("nBankAccColumnCaption"), "nBankAcc", "tabBankAgAccount", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("nBankAccColumnToolTip"))

            lobjColumn = .AddPossiblesColumn(4, GetLocalResourceObject("nCurrencyColumnCaption"), "nCurrency", "TabCurrency_By_Acc", eFunctions.Values.eValuesType.clngWindowType, CStr(1), True,  ,  ,  , "setTimeout('insShowLocalAmount()',700);", False,  , GetLocalResourceObject("nCurrencyColumnToolTip"))
            If Not String.IsNullOrEmpty(Request.QueryString.Item("sClient")) Then
                lobjColumn.Parameters.Add("sField", "CLIENT", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                lobjColumn.Parameters.Add("nTyp_Acco", 5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                lobjColumn.Parameters.Add("sClient", Request.QueryString.Item("sClient"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                lobjColumn.Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                If mobjValues.StringToType(Request.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdInteger) <> eRemoteDB.Constants.intNull Then
                    lobjColumn.Parameters.Add("sField", "INTERMED", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nTyp_Acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nIntermed", Request.QueryString.Item("nIntermed"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Else
                    lobjColumn.Parameters.Add("sField", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nTyp_Acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End If
            End If

            Call mobjGrid.Columns.AddDateColumn(0, GetLocalResourceObject("tcdValDateColumnCaption"), "tcdValDate", Session("dCollectDate"),  , GetLocalResourceObject("tcdValDateColumnToolTip"),  ,  , "insShowLocalAmount()")

            Call .AddNumericColumn(6, GetLocalResourceObject("nAmountColumnCaption"), "nAmount", 18, mobjValues.StringToType(Request.QueryString.Item("nTotalRel_loc"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("nAmountColumnToolTip"), True, 6,  ,  , "insShowLocalAmount()")
            Call .AddNumericColumn(5, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 14, CStr(1),  , GetLocalResourceObject("tcnExchangeColumnToolTip"), True, 6,  ,  ,  , True)
            Call .AddNumericColumn(7, GetLocalResourceObject("tcnAmountLocColumnCaption"), "tcnAmountLoc", 18, mobjValues.StringToType(Request.QueryString.Item("nTotalRel_loc"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnAmountLocColumnToolTip"), True, 0,  ,  , "insShowAmount()")
            lobjGeneral = New eGeneral.Exchange
            ldbldefaultUF = mobjValues.StringToType(Request.QueryString.Item("nTotalRel_locDec"), eFunctions.Values.eTypeData.etdDouble) / mdblExchangeUF
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountUFColumnCaption"), "tcnAmountUF", 18, ldbldefaultUF,  , GetLocalResourceObject("tcnAmountUFColumnToolTip"), True, 6,  ,  ,  , True)
            lobjColumn = .AddPossiblesColumn(8, GetLocalResourceObject("nBankColumnCaption"), "nBank", "table7", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("nBankColumnToolTip"))
            'Call .AddTextColumn(9, GetLocalResourceObject("sDocNumberColumnCaption"), "sDocNumber", 30, "", , GetLocalResourceObject("sDocNumberColumnToolTip"), , , "InsShowValue(this);", True)
            Call .AddNumericColumn(9, GetLocalResourceObject("sDocNumberColumnCaption"), "sDocNumber", 30, "", , GetLocalResourceObject("sDocNumberColumnToolTip"), , , , , "InsShowValue(this);", True)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("nChequeLocatColumnCaption"), "nChequeLocat", "table5553", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("nChequeLocatColumnToolTip"))
            lobjColumn = .AddPossiblesColumn(10, GetLocalResourceObject("nTypCreCardColumnCaption"), "nTypCreCard", "table183", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True)
            lobjColumn.GridVisible = False
            Call .AddPossiblesColumn(12, GetLocalResourceObject("nIntermedColumnCaption"), "nIntermed", "tabIntermedia1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insParameterCurrency('Intermediario');", True,  10, GetLocalResourceObject("nIntermedColumnToolTip"))
            mobjGrid.Columns("nIntermed").Parameters.Add("nIntertyp", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            lobjColumn.GridVisible = False
            lobjColumn = .AddPossiblesColumn(16, GetLocalResourceObject("nLed_companColumnCaption"), "nLed_compan", "tabLed_companAll_1", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("nLed_companColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            lobjColumn.GridVisible = False
            lobjColumn = .AddPossiblesColumn(17, GetLocalResourceObject("sAccountColumnCaption"), "sAccount", "Ledger_acc", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("sAccountColumnToolTip"))
            lobjColumn.GridVisible = False
            lobjColumn = .AddPossiblesColumn(18, GetLocalResourceObject("sAux_accounColumnCaption"), "sAux_accoun", "Ledger_accAux", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("sAux_accounColumnToolTip"))
            lobjColumn.GridVisible = False
            lobjColumn = .AddNumericColumn(19, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 10, CStr(0), True, GetLocalResourceObject("tcnRateColumnToolTip"),  , 2,  ,  ,  , True)
            lobjColumn.GridVisible = False
            lobjColumn = .AddNumericColumn(20, GetLocalResourceObject("tcnNominalValColumnCaption"), "tcnNominalVal", 18, CStr(0), True, GetLocalResourceObject("tcnNominalValColumnToolTip"), True, 0,  ,  ,  , True)
            lobjColumn.GridVisible = False
            lobjColumn = .AddDateColumn(21, GetLocalResourceObject("dtEmiDateColumnCaption"), "dtEmiDate",  ,  , GetLocalResourceObject("dtEmiDateColumnToolTip"),  ,  ,  , True)
            lobjColumn.GridVisible = False
            lobjColumn = .AddDateColumn(22, GetLocalResourceObject("dtExpirDateColumnCaption"), "dtExpirDate",  ,  , GetLocalResourceObject("dtExpirDateColumnToolTip"),  ,  ,  , True)
            lobjColumn.GridVisible = False

            lobjColumn = .AddNumericColumn(6, GetLocalResourceObject("nAmountDecColumnCaption"), "nAmountDec", 18, mobjValues.StringToType(Request.QueryString.Item("nTotalRel_locDec"), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("nAmountDecColumnToolTip"), True, 6)
            lobjColumn.GridVisible = False
        End With

        '+Se asignan la configuración de la ventana (GRID) 
        With mobjGrid
            .Columns("nTypPay").EditRecord = Session("nCashNum") <> 0
            .Codispl = "CO008"
            .Codisp = "CO008"
            .Columns("Sel").GridVisible = Not .ActionQuery
            If .Columns("Sel").GridVisible And Session("sRel_Type") = 3 Then
                .Columns("Sel").Checked = 1
                .Columns("Sel").DefValue = "1"
            End If

            .Columns("nTypPay").TypeList = 2
            .Columns("nTypPay").List = "14,15,18,19,20,21,22,23,24,90,26,27"
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            .sDelRecordParam = "sType=' + marrArray[lintIndex].sType + '&nTypPay=' + marrArray[lintIndex].nTypPay + '&nSequence=' + marrArray[lintIndex].nSequence + '"
            .Width = 700
            .Height = 480
            .FieldsByRow = 2
            .Top = 40
            .Left = 50
            '+ Permite continuar si el check está marcado        
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With

    End Sub

    '%insCO008Upd. Esta ventana se encarga de mostrar el código correspondiente a la
    '---------------------------------------------------------------------------------------
    Private Sub insPreCO008Upd()
        '---------------------------------------------------------------------------------------

        With Response
            If Request.QueryString.Item("Action") = "Del" Then
                insDelItem()
                Response.Write(mobjValues.ConfirmDelete())
            End If
            .Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValCollectionSeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
            .Write("<SCRIPT>ChangedEffecdate('" & Session("dCollectDate") & "','" & Request.QueryString.Item("nTypPay") & "');</" & "Script>")
            If Request.QueryString.Item("Action") <> "Del" Then
                .Write("<SCRIPT>insLockControl(""-1"",0);</" & "Script>")
            End If
            If Request.QueryString.Item("Action") = "Update" Then
                Response.Write("<SCRIPT>self.document.cmdNext.disabled=true;</" & "Script>")
                Response.Write("<SCRIPT>self.document.cmdBack.disabled=true;</" & "Script>")
            End If
            If Request.QueryString.Item("Action") = "Update" Then
                Response.Write("<SCRIPT>insShowRentValues(self.document.forms[0].nTypPay.value);</" & "Script>")
            End If
            If Request.QueryString.Item("Action") = "Del" Then
                Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location.reload();</" & "Script>")
            End If
        End With
    End Sub
    '%insCO008. Esta ventana se encarga de mostrar el código correspondiente a la
    '---------------------------------------------------------------------------------------
    Private Sub insPreCO008()
        '---------------------------------------------------------------------------------------
        Dim lclsCashMov As eCollection.CashBankAccMov
        Dim ldblTotals As Double
        Dim ldblTotals_loc As Double
        Dim ldblTotals_locDec As Double

        ldblTotals = System.Math.Abs(mdblTotalAmountGen)
        ldblTotals_loc = mdblTotalAmountGen
        ldblTotals_locDec = mdblTotalAmountGenDec

        mobjGrid.sEditRecordParam = "nTotalRel=" & mobjValues.TypeToString(ldblTotals, eFunctions.Values.eTypeData.etdDouble, True, 0) & "&nTotalRel_loc=" & mobjValues.TypeToString(ldblTotals_loc, eFunctions.Values.eTypeData.etdDouble, True, 0) & "&nTotalRel_locDec=" & mobjValues.TypeToString(ldblTotals_locDec, eFunctions.Values.eTypeData.etdDouble, True, 6)


        If mclsCashMovs.nCount > 0 Then
            Response.Write(mobjValues.HiddenControl("nItems", CStr(mclsCashMovs.nCount)))


            For Each lclsCashMov In mclsCashMovs

                With mobjGrid

                    .Columns("sType").DefValue = lclsCashMov.sType
                    .Columns("nSequence").DefValue = lclsCashMov.nSequence

                    .Columns("nTypPay").DefValue = lclsCashMov.nTypPay

                    '+ Si el tipo de pago es Cheque (2) o Cheque a fecha (10).

                    '				If (lclsCashMov.nTypPay = 2 Or lclsCashMov.nTypPay = 10) And (CStr(Session("sRelorigi")) = "1" Or mclsCashMovs.nOperational = 1) Then
                    '                    .AddButton = False
                    '				End If

                    mobjGrid.sEditRecordParam = "nTotalRel=" & mobjValues.TypeToString(ldblTotals, eFunctions.Values.eTypeData.etdDouble, True, 0) & "&nTotalRel_loc=" & mobjValues.TypeToString(ldblTotals_loc, eFunctions.Values.eTypeData.etdDouble, True, 0) & "&nTotalRel_locDec=" & mobjValues.TypeToString(ldblTotals_locDec, eFunctions.Values.eTypeData.etdDouble, True, 6) & "&sClient=" & lclsCashMov.sClient & "&nIntermed=" & mobjValues.TypeToString(lclsCashMov.nIntermed, eFunctions.Values.eTypeData.etdLong)

                    .Columns("dDoc_date").DefValue = lclsCashMov.dDoc_date
                    .Columns("sClient").DefValue = lclsCashMov.sClient
                    .Columns("sClient").Descript = lclsCashMov.sCliename
                    .Columns("nBankAcc").DefValue = lclsCashMov.nBankAcc
                    .Columns("nBankAcc").Descript = lclsCashMov.sAcc_bank
                    If lclsCashMov.sClient <> "" Then
                        .Columns("nCurrency").Parameters.Add("sField", "CLIENT", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Columns("nCurrency").Parameters.Add("nTyp_Acco", 5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Columns("nCurrency").Parameters.Add("sClient", lclsCashMov.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Columns("nCurrency").Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        If lclsCashMov.nIntermed <> eRemoteDB.Constants.intNull Then
                            .Columns("nCurrency").Parameters.Add("sField", "INTERMED", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("nCurrency").Parameters.Add("nTyp_Acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("nCurrency").Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("nCurrency").Parameters.Add("nIntermed", lclsCashMov.nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Else
                            .Columns("nCurrency").Parameters.Add("sField", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("nCurrency").Parameters.Add("nTyp_Acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("nCurrency").Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("nCurrency").Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        End If
                    End If
                    .Columns("nCurrency").DefValue = lclsCashMov.nCurrency
                    .Columns("nCurrency").Descript = lclsCashMov.sCurrency
                    .Columns("tcnExchange").DefValue = lclsCashMov.nExchange
                    .Columns("nAmount").DefValue = lclsCashMov.nAmount
                    .Columns("tcnAmountLoc").DefValue = lclsCashMov.nAmountLoc
                    .Columns("tcnAmountUF").DefValue = lclsCashMov.nAmountUF
                    .Columns("nBank").DefValue = lclsCashMov.nBank
                    .Columns("nBank").Descript = lclsCashMov.sBank
                    .Columns("sDocNumber").DefValue = lclsCashMov.sDocNumber
                    .Columns("nTypCreCard").DefValue = lclsCashMov.nTypCreCard
                    .Columns("nIntermed").DefValue = lclsCashMov.nIntermed
                    .Columns("nIntermed").Descript = lclsCashMov.sIntermed

                    .Columns("nLed_compan").DefValue = lclsCashMov.nLed_compan
                    .Columns("nLed_compan").Descript = lclsCashMov.sLed_compan
                    .Columns("sAccount").Parameters.Add("nLed_compan", 1)

                    .Columns("sAccount").DefValue = lclsCashMov.sAccount
                    .Columns("sAux_accoun").Parameters.Add("nLed_compan", lclsCashMov.nLed_compan) '1 
                    .Columns("sAux_accoun").Parameters.Add("sAccount", lclsCashMov.sAccount)

                    .Columns("sAux_accoun").DefValue = lclsCashMov.sAux_accoun
                    .Columns("nTransac").DefValue = lclsCashMov.nTransac
                    .Columns("nChequeLocat").DefValue = lclsCashMov.nChequeLocat
                    .Columns("nChequeLocat").Descript = lclsCashMov.sChequeLocat

                    .Columns("tcnCashId").DefValue = mobjValues.TypeToString(lclsCashMov.nCash_Id, eFunctions.Values.eTypeData.etdDouble)

                    Response.Write(.DoRow)
                End With
            Next lclsCashMov
        End If

        '	If mobjGrid.sEditRecordParam <> "" Then
        '		mobjGrid.sEditRecordParam = mobjGrid.sEditRecordParam & '									"&nTotalRel=" & mobjValues.TypeToString(ldblTotals, eFunctions.Values.eTypeData.etdDouble, True, 0) &  ' 									"&nTotalRel_loc=" & mobjValues.TypeToString(ldblTotals_loc,eFunctions.Values.eTypeData.etdDouble, True,0) & '									"&nTotalRel_locDec=" & mobjValues.TypeToString(ldblTotals_locDec,eFunctions.Values.eTypeData.etdDouble, True,6)
        '   Else									
        '		mobjGrid.sEditRecordParam = "nTotalRel=" & mobjValues.TypeToString(ldblTotals, eFunctions.Values.eTypeData.etdDouble, True, 0) &  ' 									"&nTotalRel_loc=" & mobjValues.TypeToString(ldblTotals_loc,eFunctions.Values.eTypeData.etdDouble, True,0) & '									"&nTotalRel_locDec=" & mobjValues.TypeToString(ldblTotals_locDec,eFunctions.Values.eTypeData.etdDouble, True,6)
        '	End If									

        Response.Write(mobjGrid.closeTable())
        Response.Write("<SCRIPT>")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotCobDev','" & mobjValues.TypeToString(mdblTotalAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotIn','" & mobjValues.TypeToString(mdblPaidAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotSaldo','" & mobjValues.TypeToString(mdblTotalAmountGen, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("</" & "Script>")

        lclsCashMov = Nothing
        mclsCashMovs = Nothing
        lclsT_DocTyp = Nothing

    End Sub

    '%insDelItem: Se elimina la información 
    '------------------------------
    Public Sub insDelItem()
        '------------------------------
        Dim lobjCollection As eCollection.CashBankAccMov

        lobjCollection = New eCollection.CashBankAccMov
        lobjCollection.DelCashBankAccMov(Session("nBordereaux"), Request.QueryString.Item("sType"), CInt(Request.QueryString.Item("nSequence")), "1")

        lobjCollection = Nothing
    End Sub

    '% insReaInitial: Inicialización de variables locales
    '-----------------------------------------------------------------------------------------
    Private Sub insReaInitial()
        '-----------------------------------------------------------------------------------------
        mstrDocNumber = eRemoteDB.Constants.intNull
    End Sub

    '% insOldValues: Inicialización de variables locales (JScript)
    '-----------------------------------------------------------------------------------------
    Private Sub insOldValues()
        '-----------------------------------------------------------------------------------------
        If mstrDocNumber <> eRemoteDB.Constants.intNull Then
            With Response
                .Write("<SCRIPT>")
                .Write("var mstrDocNumber = " & CStr(mstrDocNumber) & "; ")
                .Write("</" & "Script>")
            End With
        Else
            With Response
                .Write("<SCRIPT>")
                .Write("var mstrDocNumber = '';")
                .Write("</" & "Script>")
            End With
        End If
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO008")

lclsT_DocTyp = New eCollection.T_DocTyp
mclsCashMovs = New eCollection.CashBankAccMovs
mobjValues = New eFunctions.Values
lobjErrors = New eGeneral.GeneralFunction

'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CO008"
lblnAccess = True

%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 24 $|$$Date: 21/07/04 16:17 $|$$Author: Nvaplat40 $"

//InsCheckAll: Función que marca o desmarca todos los registros dependiendo del valor del Check
//--------------------------------------------------------------------------------------------
function insCheckAll(Field){
//--------------------------------------------------------------------------------------------
	var lintIndex=0; 	
	var sChecked = Field.checked==true?true:false;
	if (mintArrayCount == 0){
		self.document.forms[0].Sel.checked=sChecked;
		marrArray[lintIndex].Sel=sChecked;
	}
	else
		for(lintIndex=0;(lintIndex<=mintArrayCount) && (!marrArray[lintIndex].Sel);lintIndex++)
		{
			self.document.forms[0].Sel[lintIndex].checked=sChecked;
			marrArray[lintIndex].Sel=sChecked;
		}
}
   
//% insDisableAll: Permite deshabilitar todos los controles de la ventana.
//--------------------------------------------------------------------------------------------
function insDisableAll(){
//--------------------------------------------------------------------------------------------
    var lintIndex = 0;
    with(self.document.forms[0]){
        for (lintIndex=0;lintIndex < document.forms[0].length;lintIndex++)
			elements[lintIndex].disabled=true
		cmdAdd.disabled=true
	}
}
//% insShowLocalAmount:
//-------------------------------------------------------------------------------------------
function insShowLocalAmount(){
//-------------------------------------------------------------------------------------------
	if (document.forms[0].nCurrency.value!=0 && document.forms[0].nAmount.value!='0,000000' && document.forms[0].nAmount.value!=0 && document.forms[0].tcdValDate.value!='')
	    insDefValues("LocalAmount","nCurrency=" + document.forms[0].nCurrency.value + "&sType=Normal&nAmount=" + document.forms[0].nAmount.value + "&sCodispl=CO008" + "&dValDate=" + document.forms[0].tcdValDate.value);
	else{
	    document.forms[0].tcnExchange.value='';
	    document.forms[0].tcnAmountLoc.value='';
	    document.forms[0].tcnAmountUF.value='';
	    }
}

//% insShowLocalAmount:
//-------------------------------------------------------------------------------------------
function insShowAmount(){
//-------------------------------------------------------------------------------------------
	if (document.forms[0].nCurrency.value != 0 && document.forms[0].nAmount.value != '0,000000' && document.forms[0].nAmount.value != 0 && document.forms[0].tcdValDate.value != '')
	    insDefValues("LocalAmount","nCurrency=" + document.forms[0].nCurrency.value + "&sType=Amount&nAmount=" + document.forms[0].tcnAmountLoc.value + "&sCodispl=CO008" + "&dValDate=" + document.forms[0].tcdValDate.value);
	else{
	    document.forms[0].tcnExchange.value='';
	    document.forms[0].tcnAmountLoc.value='';
	    document.forms[0].tcnAmountUF.value='';
	    }
}

//% InsShowValue: Muestra los valores según el tipo de pago.
//-------------------------------------------------------------------------------------------
function InsShowValue(Field){
//-------------------------------------------------------------------------------------------
    var lstrQueryString;
    with(self.document.forms[0]){
//+ Si el tipo de pago es Depósito bancario.
		if (nTypPay.value == 3) {
			if (mstrDocNumber != Field.value){
				mstrDocNumber = Field.value;
				insDefValues("getDocNumber","sDocNumber=" + Field.value +"&nType_mov=1")																	  
			}
		}
	}
}

//%insParameterCurency: Actualiza parametros de la moneda
//---------------------------------------------------------------------------
function insParameterCurrency(Field){
//---------------------------------------------------------------------------
  self.document.forms[0].nCurrency.value='';
  UpdateDiv('nCurrencyDesc','','Normal');   
  <%If Request.QueryString.Item("Action") <> "Update" Then%> 
	if (Field=='Cliente'){
		with(self.document.forms[0]){            
		    if (sClient.value!=''){
				nCurrency.Parameters.Param1.sValue='CLIENT';
				nCurrency.Parameters.Param2.sValue=5;
				nCurrency.Parameters.Param3.sValue=sClient.value;
				nCurrency.Parameters.Param4.sValue=-32768;
				}
			else{
				nCurrency.Parameters.Param1.sValue='';
				nCurrency.Parameters.Param2.sValue=-32768;
				nCurrency.Parameters.Param3.sValue='';
				nCurrency.Parameters.Param4.sValue=-32768;
			}	
		}
	}
	else if (Field=='Intermediario'){
		with(self.document.forms[0]){            
			if (nIntermed.value!=''){
				nCurrency.Parameters.Param1.sValue='INTERMED';
				nCurrency.Parameters.Param2.sValue=-32768;
				nCurrency.Parameters.Param3.sValue='';
				nCurrency.Parameters.Param4.sValue=nIntermed.value;
				}
			else{
				nCurrency.Parameters.Param1.sValue='';				
				nCurrency.Parameters.Param2.sValue=-32768;
				nCurrency.Parameters.Param3.sValue='';
				nCurrency.Parameters.Param4.sValue=-32768;
			}	
		}	
	}
	else{
		with(self.document.forms[0]){            
			nCurrency.Parameters.Param1.sValue='';				
			nCurrency.Parameters.Param2.sValue=-32768;
			nCurrency.Parameters.Param3.sValue='';
			nCurrency.Parameters.Param4.sValue=-32768;
		}
	}
<%End If%>	
}	

//% insCashNum: bloquea los controles cuando se ingresa n° comprobante de caja
//----------------------------------------------------------------------------
function insCashNum(Field)
//----------------------------------------------------------------------------
{ 
   var lblnCashNum = (self.document.forms[0].tcnCashId==''?false:true);

  if  (lblnCashNum) {
  with(self.document.forms[0]){
    
       	nCurrency.disabled	= true;
       	
       	nTypPay.value = '';

		nAmount.disabled	= true;
		nAmount.value='';
		
		tcnAmountLoc.disabled = true;
		tcnAmountLoc.value= '';

		tcnAmountUF.disabled = true;
		tcnAmountUF.value= '';
		
		dDoc_date.disabled		= true;
		dDoc_date.value='';
		btn_dDoc_date.disabled	= true;
		
		nBankAcc.disabled		= true;
		btnnBank.disabled       = true;
		
		nBankAcc.value =''; 
		btnnBankAcc.disabled	= true;
		UpdateDiv('nBankAcc_Name','','Normal');
		
		nBank.disabled			= true;
		btnnBank.disabled       = true;
		nBank.value = '';
		UpdateDiv('nBankDesc','','Normal');
		
		sDocNumber.disabled		= true;
		sDocNumber.value='';
		
		nTypCreCard.disabled	= true;
		nTypCreCard.value='';
		
		nIntermed.disabled		= true;
		nIntermed.value='';
		btnnIntermed.disabled	= true;

		nLed_compan.disabled	= true;
		nLed_compan.value ='';
		btnnLed_compan.disabled = true;
		
		sAccount.disabled		= true;
		sAccount.value = '';
		btnsAccount.disabled	= true;
		
		sAux_accoun.disabled	= true;
		sAux_accoun.value ='';
		btnsAux_accoun.disabled = true;
		
		sClient.disabled        = true;
		sClient.value ='';
		
		
		dtExpirDate.disabled	 = true;
		dtExpirDate.value='';
		btn_dtExpirDate.disabled = true;
		
		dtEmiDate.disabled		 = true;
		dtEmiDate.value='';
		btn_dtEmiDate.disabled	 = true;
  }
   insDefValues("CashNumID","nCashId=" + document.forms[0].tcnCashId.value) 
 }
}

//% insShowRentValues: muestra por default los valores para tipo de pago "primera renta"
//-------------------------------------------------------------------------------------------
function insShowRentValues(nTypePayment){
//-------------------------------------------------------------------------------------------
    if (nTypePayment==29 || nTypePayment==30 || nTypePayment==31 || nTypePayment==32){
        insDefValues("Rent_Values", "nPayment=" + self.document.forms[0].nTypPay.value, '/VTimeNet/Collection/Collectionseq');
    }
}

//% insShowAccount: muestra por defecto el valor de la cuenta seleccionada en la ventana de conceptos si se incluye un tipo de pago
//                  depósito de pago Pac/transbank o Pago en ventanillas de bancos
//-------------------------------------------------------------------------------------------
function insShowAccount(nTypePayment){
//-------------------------------------------------------------------------------------------
    
    if (nTypePayment==3){
        if ('<%=Session("valAccountAgree")%>'>0){
            self.document.forms[0].nBankAcc.value = '<%=Session("valAccountAgree")%>';
            $(self.document.forms[0].nBankAcc).change();
            self.document.forms[0].nBankAcc.disabled=true;
            self.document.forms[0].btnnBankAcc.disabled=true;
        }
    }    
}

//% insLockControl: bloquea los controles que dependen del Tipo de pago
//-------------------------------------------------------------------------------------------
function insLockControl(Field,nOperational){
//-------------------------------------------------------------------------------------------		
	var lblnIsRV    = (self.document.forms[0].hddnRVFlag.value==2?false:true)
	var lblnAdd  = ('<%=Request.QueryString.Item("Action")%>'=='Add'?true:false)
        
    with(self.document.forms[0]){
        nTypPay.disabled	= true;
    	if (lblnAdd) {
    	    if (Field<0 && nTypPay.value > 0) {
    	        Field = nTypPay.value;
    	    }
    	    
    	    nTypPay.disabled	= false;
    	    nCurrency.disabled	= true;
    	    	
    	    nAmount.disabled	= true;
    	    	
    	    dDoc_date.disabled		= true;
    	    dDoc_date.value = '';    	    	
    	    btn_dDoc_date.disabled	= true;
    	    
    	    nBankAcc.value ='';
    	    UpdateDiv('nBankAccDesc','','Normal');
    	    nBankAcc.disabled		= true;
    	    btnnBankAcc.disabled	= true;
    	    
    	    nBank.disabled			= true;
    	    btnnBank.disabled       = true;
    	    nBank.value ='';
    	    UpdateDiv('nBankDesc','','Normal');
    	    
    	    sDocNumber.disabled		= true;
    	    sDocNumber.value ='';
    	    nTypCreCard.disabled	= true;
    	    nTypCreCard.value ='';
    	    	
    	    nIntermed.disabled		= true;
    	    nIntermed.value ='';
    	    btnnIntermed.disabled	= true;

    	    nLed_compan.disabled	= true;
    	    nLed_compan.value='';
    	    	
    	    btnnLed_compan.disabled = true;
    	    sAccount.disabled		= true;
    	    sAccount.value='';
    	    	
    	    btnsAccount.disabled	= true;
    	    sAux_accoun.disabled	= true;
    	    sAux_accoun.value='';
    	    btnsAux_accoun.disabled = true;
    	    	
    	    sClient.disabled        = true;
    	    sClient.value = '';
    	    	
    	    dtExpirDate.disabled	 = true;
    	    dtExpirDate.value='';
    	    	
    	    dtEmiDate.disabled		 = true;
    	    dtEmiDate.value='';
    	    	
    	    btn_dtExpirDate.disabled = true;
    	    btn_dtEmiDate.disabled	 = true;
        } else {
            Field = nTypPay.value;
        }
                
    	tcnCashId.disabled =(Field!=0&&Field!=-1?true:false)
    	
    	if (tcnCashId.disabled){
    	    if (lblnAdd) {
    	        tcnCashId.value ='';
    	    }
    	} 
        
    	tcnRate.disabled       = false;
    	tcnNominalVal.disabled = false;
    	
    	if (nTransac.value<=0) {
    	    if ('<%=Session("Finan_Interest")%>'=='1'){
    	        nTypPay.disabled = false;
    	    }
    		nCurrency.disabled = false;
    		nAmount.disabled   = false;
    	    
    	    if (lblnAdd) {
    		    nIntermed.value = '0';
    		    UpdateDiv('nIntermedDesc', '');
    		}
    		
//+ Fecha del documento	
    		dDoc_date.disabled = (Field!=11 && 
    		                      Field!=12 && 
    		                      Field!=1 && 
    		                      Field!=6?false:true);
    			                      
    		if (dDoc_date.disabled){
    		    if (lblnAdd) {
    		        dDoc_date.value='' 
    		    }
    		}                      
    			                      
//+ Código del Cliente
    		sClient.disabled = (Field==12?false:true);
    		btnsClient.disabled = sClient.disabled;
    		sClient_Digit.disabled = (Field==12?false:true);
    		if (sClient.disabled){
    		    if (lblnAdd) {
    		        sClient.value=''
    		    }
    		}
    			                      			                      
    		btn_dDoc_date.disabled=dDoc_date.disabled;
    			
    		if (Field==3){
    			nCurrency.disabled   = false;
    			nAmount.disabled     = false;
    			dDoc_date.disabled   = false;
    			nBankAcc.disabled    = false;
    			btnnBankAcc.disabled = false;
    			btn_dDoc_date.disabled=dDoc_date.disabled;
    		}	
//+ Banco
    		nBank.disabled = (Field==2  ||
    		                  Field==5  || 
                              Field==7  || 
    		                  Field==10 || 			                  
    		                  Field==28?false:true);
    		btnnBank.disabled = (Field==2  ||
     							 Field==5  || 
                                 Field==7  || 
    							 Field==10 || 			                  
    							 Field==28?false:true);                  
    		if (nBank.disabled){    		    
    		    if (lblnAdd) {
    		        nBank.value='' 
    		    }
    		} 
    			                  
//+ Nº de documento
    		sDocNumber.disabled = (Field==2 ||
    							   Field==10 ||
    							   Field==3 ||
    							   Field==5||
                                   Field==7  || 
    							   Field==29|| 
    							   Field==30|| 
    							   Field==31||  
    							   Field==28?false:true);
    								   
    		if (sDocNumber.disabled) {
    		    if (lblnAdd) {
    		        sDocNumber.value=''
    		    }
    		}					   
//+ Tipo de tarjeta
    		nTypCreCard.disabled = (Field==5?false:true);
    		if (nTypCreCard.disabled){
    		    if (lblnAdd) {
    		        nTypCreCard.value=''
    		    }
    		}
//+ Productor
    		nIntermed.disabled = (Field==11?false:true)&&!lblnIsRV;
    		if (nIntermed.disabled){
    		    if (lblnAdd) {
    		        nIntermed.value=''
    		    }
    		}
    			
    		btnnIntermed.disabled = nIntermed.disabled;
//+ Compañía contable
    		nLed_compan.disabled = (Field==6?false:true)&&!lblnIsRV;
    		if (nLed_compan.disabled){
    		    if (lblnAdd) {
    			    nLed_compan.value=''
    			}
    		}
    		btnnLed_compan.disabled = nLed_compan.disabled;
//+ Cuenta contable
    		sAccount.disabled = (Field==6?false:true)&&!lblnIsRV;
    		if (sAccount.disabled){
    		    if (lblnAdd) {
    		        sAccount.value=''
    		    }
    		}
    			
    		btnsAccount.disabled = sAccount.disabled;
//+ Auxiliar
    		sAux_accoun.disabled = (Field==6?false:true)&&!lblnIsRV;
    		if (sAux_accoun.disabled){
    		    if (lblnAdd) {
    		        sAux_accoun.value='' 
    		    }
    		}
    		btnsAux_accoun.disabled = sAux_accoun.disabled;
//+ Plaza
    		nChequeLocat.disabled = (Field==2 ||
    		                         Field==10?false:true);
    		                         
    		if (nChequeLocat.disabled){
    		    if (lblnAdd) {
    		        nChequeLocat.value=''
    		    }
    		}
    	}
    	if (<%=Session("chkRentVital")%>!='9'||<%=Session("chkRentVital")%>!='10')
		{
    		tcnRate.disabled       = true;
    		tcnNominalVal.disabled = true;
    	    dtExpirDate.disabled   = true;
    	    dtEmiDate.disabled	   = true;

		}
    }
    
//+ Oculta o muestra los campos segun la el tipo de ingreso
/*+ Cliente*/
	document.getElementsByTagName("TD")[6].style.display='none'
	document.getElementsByTagName("TD")[7].style.display='none'                

/*+ Intermediario*/	
	document.getElementsByTagName("TD")[38].style.display='none'
	document.getElementsByTagName("TD")[39].style.display='none'

/*+ Compañia contable*/	
	document.getElementsByTagName("TD")[42].style.display='none'
	document.getElementsByTagName("TD")[43].style.display='none'	

/*+ Cuenta contable*/
	document.getElementsByTagName("TD")[46].style.display='none'
	document.getElementsByTagName("TD")[47].style.display='none'
		
/*+ Auxiliar contable*/
	document.getElementsByTagName("TD")[50].style.display='none'
	document.getElementsByTagName("TD")[51].style.display='none'

/*+ Tasa de descuento*/
	document.getElementsByTagName("TD")[54].style.display='none'
	document.getElementsByTagName("TD")[55].style.display='none'

/*+ Valor nominal*/
	document.getElementsByTagName("TD")[56].style.display='none'
	document.getElementsByTagName("TD")[57].style.display='none'
	
/*+ Fecha de emisión*/
	document.getElementsByTagName("TD")[58].style.display='none'
	document.getElementsByTagName("TD")[59].style.display='none'

/*+ Fecha de vencimiento*/
	document.getElementsByTagName("TD")[60].style.display='none'
	document.getElementsByTagName("TD")[61].style.display='none'
	
/*+ Monto con decimales*/
	document.getElementsByTagName("TD")[62].style.display='none'
	document.getElementsByTagName("TD")[63].style.display='none'	

//+ Ingreso por cargo a cta cte de cliente
	if (Field==12){
        document.getElementsByTagName("TD")[6].style.display=''
        document.getElementsByTagName("TD")[7].style.display=''                
	}
	
//+ Ingreso por cargo a cta cte de Intermediario
	if (Field==11){
        document.getElementsByTagName("TD")[38].style.display=''
        document.getElementsByTagName("TD")[39].style.display=''                
	}
	
//+ Ingreso por canje
	if (Field==6){
	    document.getElementsByTagName("TD")[42].style.display=''
	    document.getElementsByTagName("TD")[43].style.display=''
	    document.getElementsByTagName("TD")[46].style.display=''
	    document.getElementsByTagName("TD")[47].style.display=''
	    document.getElementsByTagName("TD")[50].style.display=''
	    document.getElementsByTagName("TD")[51].style.display=''
	}
	
//+ Ingreso por: bono de reconocimiento,bono exonerado politico,
//+              complemento bono reconocimiento,primera/prima renta privada
	if (Field==29 || Field==30 || Field==31 || Field==32){
	    document.getElementsByTagName("TD")[54].style.display=''
	    document.getElementsByTagName("TD")[55].style.display=''
	    document.getElementsByTagName("TD")[56].style.display=''
	    document.getElementsByTagName("TD")[57].style.display=''	
	    document.getElementsByTagName("TD")[58].style.display=''
	    document.getElementsByTagName("TD")[59].style.display=''
	    document.getElementsByTagName("TD")[60].style.display=''
	    document.getElementsByTagName("TD")[61].style.display=''
	}

}
//%ChangedEffecdate: Cambio de fecha de efecto
//-----------------------------------------------------------------------------
function ChangedEffecdate(nValue,sTypPay)
//-----------------------------------------------------------------------------
{   
    insDefValues('ValCashCO008','sCodispl=CO008&dEffecdate=' + nValue + '&sTypPay=' + sTypPay + '&nPage=CO008','/VTimeNet/Collection/CollectionSeq');
}
</SCRIPT>
<%
mobjMenues = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
mobjMenues.sSessionID = Session.SessionID
mobjMenues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "CO008", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If

With Response
	.Write(mobjValues.WindowsTitle("CO008", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjValues.StyleSheet())
End With

Call insReaInitial()
Call insOldValues()
%>
<%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCO008" ACTION="ValCollectionSeq.aspx?Time=1">
<%
Response.Write(mobjValues.ShowWindowsName("CO008", Request.QueryString.Item("sWindowDescript")))

Call insPrevInf()

mintDefValuePay = lclsT_DocTyp.reaConceptsCO001(Session("nBordereaux"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCO008Upd()
Else
	Call insPreCO008()
End If
%>      
</FORM>
<%
'+ Si el usuario no tiene una caja asociada no se le permite el acceso a la transacción.
If Not lblnAccess Then%>
	<SCRIPT>insDisableAll();</SCRIPT>	
<%End If%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage("CO008")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>