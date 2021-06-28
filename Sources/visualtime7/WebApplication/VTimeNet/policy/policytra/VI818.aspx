<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues
Dim mcoltmp_undo_move_accs As eBatch.tmp_undo_move_accs
Dim mclstmp_undo_move_acc As Object


'+ insDefineHeader: Definición del Grid
'-------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------
        Dim lclsCurrency As ePolicy.Curren_pol
        Dim sCurrency As String
        Dim sDate As String
        
        Dim sDate1 As String
        Dim bTaxReg As Boolean
        Dim bManual As Boolean
        Dim soperdatetype As String
        Dim soperdatemanualtype As String
        Dim bOperDateManualDisabled As Boolean
        Dim dLedgerDat As Date
        Dim dLastProcess_date As Date
        
	lclsCurrency = New ePolicy.Curren_pol
	Call lclsCurrency.findCurrency("2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	sCurrency = lclsCurrency.sDescript
	
	lclsCurrency = Nothing
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "VI818"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid
		With .Columns
			If Request.QueryString.Item("Type") <> "PopUp" Then
				Call .AddCheckColumn(0, GetLocalResourceObject("chkAuxSelColumnCaption"), "chkAuxSel", vbNullString, True)
				If mobjValues.StringToType(Session("sPolitype"), eFunctions.Values.eTypeData.etdDouble) = 2 And mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble) = 0 Then
					Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 5, CStr(0),  , GetLocalResourceObject("tcnCertifColumnToolTip"))
				Else
					Call .AddHiddenColumn("tcnCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
				End If
				Call .AddTextColumn(0, GetLocalResourceObject("tcdOperdateColumnCaption"), "tcdOperdate", 10, vbNullString,  , GetLocalResourceObject("tcdOperdateColumnToolTip"))
				Call .AddPossiblesColumn(0, GetLocalResourceObject("tctType_moveColumnCaption"), "tctType_move", "TAB_DESCRIPTIONVI818", 2,  , True,  ,  ,  ,  , False,  , GetLocalResourceObject("tctType_moveColumnToolTip"))
				Call .AddTextColumn(0, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 15, sCurrency,  , GetLocalResourceObject("tctCurrencyColumnToolTip"))
				Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnOriginColumnCaption"), "tcnOrigin", "Table5633", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnOriginColumnToolTip"))
				Call .AddNumericColumn(0, GetLocalResourceObject("tctCreditColumnCaption"), "tctCredit", 18, CStr(0),  , GetLocalResourceObject("tctCreditColumnToolTip"),  , 6)
				Call .AddNumericColumn(0, GetLocalResourceObject("tctDebitColumnCaption"), "tctDebit", 18, CStr(0),  , GetLocalResourceObject("tctDebitColumnToolTip"),  , 6)
				Call .AddNumericColumn(0, GetLocalResourceObject("tctTaxColumnCaption"), "tctTax", 18, CStr(0),  , GetLocalResourceObject("tctTaxColumnToolTip"),  , 6)
                    Call .AddDateColumn(0, GetLocalResourceObject("tcdoperdatemanualColumnCaption"), "tcdoperdatemanual", , , GetLocalResourceObject("tcdoperdatemanualColumnToolTip"), , , , bOperDateManualDisabled)
                    Call .AddDateColumn(0, GetLocalResourceObject("tcddate_originCaption"), "tcddate_origin", , , GetLocalResourceObject("tcddate_originToolTip"), , , , False)
                    
				Call .AddNumericColumn(0, GetLocalResourceObject("tcncreditmanualColumnCaption"), "tcncreditmanual", 18, CStr(0),  , GetLocalResourceObject("tcncreditmanualColumnToolTip"),  , 6)
                    Call .AddNumericColumn(0, GetLocalResourceObject("tcndebitmanualColumnCaption"), "tcndebitmanual", 18, CStr(0), , GetLocalResourceObject("tcndebitmanualColumnToolTip"), , 6)
                    
                    
                    
                    Call .AddHiddenColumn("tcdoperdate_new", "")
                    Call .AddHiddenColumn("cbeoperdatetype", "")
                    Call .AddHiddenColumn("cbeoperdatemanualtype", "")
                    Call .AddHiddenColumn("tcdLedgerdat", "")
                    Call .AddHiddenColumn("tcdLastProcess_date", "")
			Else
				Call .AddHiddenColumn("tcnCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
				Call .AddHiddenColumn("tcdOperdate", "")
				Call .AddTextColumn(0, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 25, sCurrency,  , GetLocalResourceObject("tctCurrencyColumnToolTip"),  ,  ,  , True)
				'Call .AddHiddenColumn("tctCredit","")
				'Call .AddHiddenColumn("tctDebit","")
				Call .AddHiddenColumn("tctTax", "")
				If Request.QueryString.Item("Action") <> "Update" Then
                        sDate = mobjValues.TypeToString(Today, Values.eTypeData.etdDate)
				Else
					sDate = vbNullString
				End If

                    If Request.QueryString.Item("Action") = "Update" Then
                        Call .AddHiddenColumn("tcnOrigin", "")
                    Else
                        Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnOriginColumnCaption"), "tcnOrigin", "TAB_ORIGINPOLVI818", 2, , True, , , , , False, , GetLocalResourceObject("tcnOriginColumnToolTip"))
                    End If

                    ' Se verifica si se trata de un ajuste manual en cuyo caso se asumen valores predeterminados
                    ' para la opción de fecha de movimiento y la opción de fecha de valorización y además se bloquean ambos campos
                    If Session("nOperat") = 1 Then
                        bManual = True
                        soperdatetype = "1"
                        soperdatemanualtype = "2"
                        sDate1 = mobjValues.StringToType(Session("dEffecdate"), Values.eTypeData.etdDate)
                        sDate = sDate1
                    Else
                        bManual = False
                        soperdatetype = vbNullString
                        soperdatemanualtype = vbNullString
                        sDate1 = vbNullString
                    End If

                    ' Se agrega la fecha del movimiento
                    Call .AddComboControl(0, GetLocalResourceObject("cbeoperdatetypeCaption"), "cbeoperdatetype", "1|Fecha del día,2|Fecha próximo PUI", soperdatetype, False, , GetLocalResourceObject("cbeoperdatetypeToolTip"), "insChangeField(this)", bManual)
                    Call .AddDateColumn(0, GetLocalResourceObject("tcdoperdate_newCaption"), "tcdoperdate_new", sDate1, True, GetLocalResourceObject("tcdoperdate_newToolTip"), , , , True)
                    Call .AddComboControl(0, GetLocalResourceObject("cbeoperdatemanualtypeCaption"), "cbeoperdatemanualtype", "1|Fecha valorización mov. origen,2|Fecha del movimiento", soperdatemanualtype, False, , GetLocalResourceObject("cbeoperdatemanualtypeToolTip"), "insChangeField(this)", bManual)

                    ' Se deshabilita la fecha de valorización cuando la operación sea "Reverso / Recálculo cta. cte." (2)
                    If Session("nOperat") = 2 Then
                        bOperDateManualDisabled = True
                    Else
                        bOperDateManualDisabled = False
                    End If
                    
                    Call .AddDateColumn(0, GetLocalResourceObject("tcdoperdatemanualColumnCaption"), "tcdoperdatemanual", sDate, , GetLocalResourceObject("tcdoperdatemanualColumnToolTip"), , , , bOperDateManualDisabled)

                    ' Se agregan las fechas de contabilización y del próximo PUI
                    ' Cuando se trate de un ajuste manual, se buscan los campos de manera explícita
                    ' para colocarlos como su valor predeterminado
                    If Session("nOperat") = 1 Then
                        mclstmp_undo_move_acc = New eBatch.Tmp_undo_Move_Acc
                        If mclstmp_undo_move_acc.FindLedgerProcessDate(Session("nBranch"), _
                                      Session("nProduct")) Then
                            dLedgerDat = mclstmp_undo_move_acc.dLedgerDat
                            dLastProcess_date = mclstmp_undo_move_acc.dLastProcess_date
                        Else
                            dLedgerDat = vbNullString
                            dLastProcess_date = vbNullString
                        End If
                        mclstmp_undo_move_acc = Nothing
                    Else
                        dLedgerDat = vbNullString
                        dLastProcess_date = vbNullString
                    End If
                    Call .AddDateColumn(0, GetLocalResourceObject("tcdLastProcess_dateCaption"), "tcdLastProcess_date", dLastProcess_date, , GetLocalResourceObject("tcdLastProcess_dateToolTip"), , , , True)
                    Call .AddDateColumn(0, GetLocalResourceObject("tcdLedgerdatCaption"), "tcdLedgerdat", dLedgerDat, , GetLocalResourceObject("tcdLedgerdatToolTip"), , , , True)

                    If Request.QueryString("nType_move") = "803" Or _
                       Request.QueryString("nType_move") = "804" Or _
                       Request.QueryString("nType_move") = "817" Or _
                       Request.QueryString("nType_move") = "821" Then
                        Call .AddNumericColumn(0, GetLocalResourceObject("tcncreditmanualColumnCaption"), "tcncreditmanual", 18, CStr(0), , GetLocalResourceObject("tcncreditmanualColumnToolTip"), , 6, , , , True, , False)
                        Call .AddNumericColumn(0, GetLocalResourceObject("tctCreditColumnCaption"), "tctCredit", 18, CStr(0), , GetLocalResourceObject("tctCreditColumnToolTip"), , 6, , , , True, , False)
                        Call .AddPossiblesColumn(0, GetLocalResourceObject("tctType_moveColumnCaption"), "tctType_move", "Table5708", 2, , True, , , , , True, , GetLocalResourceObject("tctType_moveColumnToolTip"))
                        Response.Write("<SCRIPT>changeValues('" & Request.Params.Get("Query_String") & "')</" & "Script>")
                    ElseIf Request.QueryString("nType_move") = "802" Then
                        Call .AddNumericColumn(0, GetLocalResourceObject("tcncreditmanualColumnCaption"), "tcncreditmanual", 18, CStr(0), , GetLocalResourceObject("tcncreditmanualColumnToolTip"), , 6, , , , True, , False)
                        Call .AddNumericColumn(0, GetLocalResourceObject("tctCreditColumnCaption"), "tctCredit", 18, CStr(0), , GetLocalResourceObject("tctCreditColumnToolTip"), , 6, , , , True, , False)
                        Call .AddPossiblesColumn(0, GetLocalResourceObject("tctType_moveColumnCaption"), "tctType_move", "Table5708", 2, , True, , , , , True, , GetLocalResourceObject("tctType_moveColumnToolTip"))
                    Else
                        Call .AddNumericColumn(0, GetLocalResourceObject("tcncreditmanualColumnCaption"), "tctCredit", 18, CStr(0), , GetLocalResourceObject("tcncreditmanualColumnToolTip"), , 6, , , , , , False)
                        Call .AddNumericColumn(0, GetLocalResourceObject("tcndebitmanualColumnCaption"), "tctDebit", 18, CStr(0), , GetLocalResourceObject("tcndebitmanualColumnToolTip"), , 6, , , , , , False)
                        Call .AddPossiblesColumn(0, GetLocalResourceObject("tctType_moveColumnCaption"), "tctType_move", "Table5708", 2, , True, , , , , False, , GetLocalResourceObject("tctType_moveColumnToolTip"))
                    End If
			End If

                If Session("nOperat") = 1 Then
                    bTaxReg = False
                Else
                    bTaxReg = True
                End If
                Call .AddPossiblesColumn(0, GetLocalResourceObject("tctProfitworkerCaption"), "tctProfitworker", "Table950", Values.eValuesType.clngComboType, , False, , , , , bTaxReg, , GetLocalResourceObject("tctProfitworkerToolTip"))
                mobjGrid.Columns("tctProfitworker").TypeList = 1
                mobjGrid.Columns("tctProfitworker").List = "1,2,11,12,4,99"

                Call .AddHiddenColumn("hddManual", vbNullString)
                Call .AddHiddenColumn("hddidconsec", vbNullString)
                Call .AddHiddenColumn("hddInvested", vbNullString)
                Call .AddHiddenColumn("hddId_reverse", vbNullString)

                Call .AddHiddenColumn("hddoperdate_orig", vbNullString)
                Call .AddHiddenColumn("hddvaluedate_orig", vbNullString)
            End With

            .Codispl = "VI818"
            .Width = 460
            .Height = 470
            If mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdInteger) = 1 Then
                .AddButton = True
            Else
                .AddButton = False
            End If
            .Columns("Sel").GridVisible = True
            .Splits_Renamed.AddSplit(0, "Movimiento Origen", 8)
            .Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 5)
	 	
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401
		
            .sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&dOperdate='+ marrArray[lintIndex].tcdOperdate + '" & "&nidconsec='+ marrArray[lintIndex].hddidconsec + '"

            '            .Columns("tctProfitworker").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '            .Columns("tctProfitworker").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '            .Columns("tctProfitworker").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"),Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDate, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Columns("tcnOrigin").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnOrigin").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnOrigin").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
End Sub

'%inspreVI818upd: Se Actualiza el registro seleccionado en el Grid
'-------------------------------------------------------------------------------------------
Private Sub inspreVI818upd()
	'-------------------------------------------------------------------------------------------
	Dim lclsTmp_undo_move_acc As eBatch.Tmp_undo_move_acc
	lclsTmp_undo_move_acc = New eBatch.Tmp_undo_move_acc
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			If lclsTmp_undo_move_acc.insPostVI818upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dOperdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nidconsec"), eFunctions.Values.eTypeData.etdDouble)) Then
				
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicyTra.aspx", "VI818", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

    '%inspreVI818: Se cargan los Valores en el Grid
    '-------------------------------------------------------------------------------------------
    Private Sub inspreVI818()
        '-------------------------------------------------------------------------------------------
        Dim lintIndex As Short
        Dim lstrvalue As String
        Dim ldatOperdat As Object
        'Dim lintnqresc

        mcoltmp_undo_move_accs = New eBatch.tmp_undo_Move_Accs
        If mcoltmp_undo_move_accs.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                       mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                       mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                       mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 1) Then
            lintIndex = 0
			lstrvalue = "1"
            With mobjGrid
                For Each mclstmp_undo_move_acc In mcoltmp_undo_move_accs
                    .Columns("tcnCertif").DefValue = mclstmp_undo_move_acc.nCertif
                    .Columns("tcdOperdate").DefValue = mclstmp_undo_move_acc.dOperdatemanual
                    .Columns("tctType_move").Descript = mclstmp_undo_move_acc.sType_move
                    .Columns("tctType_move").DefValue = mclstmp_undo_move_acc.nType_move
                    .Columns("tctCurrency").DefValue = mclstmp_undo_move_acc.sCurrency
                    .Columns("tcnOrigin").DefValue = mclstmp_undo_move_acc.nOrigin
                    .Columns("tcnOrigin").Descript = mclstmp_undo_move_acc.sOrigin
                    .Columns("tctProfitworker").DefValue = mclstmp_undo_move_acc.nTyp_profitworker
                    .Columns("tctProfitworker").Descript = mclstmp_undo_move_acc.sProfitworker
                    .Columns("tctCredit").DefValue = mclstmp_undo_move_acc.nCredit
                    .Columns("tctDebit").DefValue = mclstmp_undo_move_acc.nDebit
                    .Columns("tctTax").DefValue = mclstmp_undo_move_acc.nTax
                    .Columns("tcdoperdate_new").DefValue = mclstmp_undo_move_acc.dOperdate_new
                    .Columns("cbeoperdatetype").DefValue = mclstmp_undo_move_acc.nOperDateType
                    .Columns("cbeoperdatemanualtype").DefValue = mclstmp_undo_move_acc.nOperDateManualType
                    .Columns("tcdoperdatemanual").DefValue = mclstmp_undo_move_acc.dOperdatemanual
                    .Columns("tcdLedgerdat").DefValue = mclstmp_undo_move_acc.dLedgerdat
                    .Columns("tcdLastProcess_date").DefValue = mclstmp_undo_move_acc.dLastProcess_date

                    'Se asignan columnas ocultas para el manejo de los valores de fecha originales
                    .Columns("hddoperdate_orig").DefValue = mclstmp_undo_move_acc.dOperdate_new
                    .Columns("hddvaluedate_orig").DefValue = mclstmp_undo_move_acc.dOperdatemanual
                    
                    .Columns("tcncreditmanual").DefValue = mclstmp_undo_move_acc.nCreditmanual
                    .Columns("tcndebitmanual").DefValue = mclstmp_undo_move_acc.nDebitmanual
                    .Columns("hddManual").DefValue = mclstmp_undo_move_acc.sManual
                    .Columns("hddidconsec").DefValue = mclstmp_undo_move_acc.nidconsec
                    .Columns("hddInvested").DefValue = mclstmp_undo_move_acc.nInvested
                    .Columns("hddId_reverse").DefValue = mclstmp_undo_move_acc.nId_reverse

                    .Columns("tcddate_origin").DefValue = mclstmp_undo_move_acc.dDate_origin
                    .Columns("hddManual").DefValue = mclstmp_undo_move_acc.sManual
                    .Columns("hddidconsec").DefValue = mclstmp_undo_move_acc.nidconsec
                    .Columns("hddInvested").DefValue = mclstmp_undo_move_acc.nInvested
                    .Columns("hddId_reverse").DefValue = mclstmp_undo_move_acc.nId_reverse
                    .Columns("chkAuxSel").Checked = mclstmp_undo_move_acc.sSel
                    .Columns("chkAuxSel").Disabled = True
                    .Columns("tctType_move").HRefScript = ""

                    If ((mclstmp_undo_move_acc.nType_move = 1 Or mclstmp_undo_move_acc.nType_move = 5) And mclstmp_undo_move_acc.sSel = 1) Then
                        .Columns("tcncreditmanual").DefValue = mclstmp_undo_move_acc.nCreditmanual
                        .Columns("tcndebitmanual").DefValue = mclstmp_undo_move_acc.nDebitmanual
                    Else
                        .Columns("tcncreditmanual").DefValue = mclstmp_undo_move_acc.nCreditmanual
                        .Columns("tcndebitmanual").DefValue = mclstmp_undo_move_acc.nDebitmanual
                    End If


                    If Session("nOperat") = "1" Then
                        If (mclstmp_undo_move_acc.nType_move = 1 And mclstmp_undo_move_acc.sReverse <> "1") Or
                         (mclstmp_undo_move_acc.nType_move = 2 And mclstmp_undo_move_acc.sReverse <> "1") Or
                         (mclstmp_undo_move_acc.nType_move = 5 And mclstmp_undo_move_acc.nId_reverse <= 0) Or
                         (mclstmp_undo_move_acc.nType_move = 14 And mclstmp_undo_move_acc.sReverse <> "1") Or
                         (mclstmp_undo_move_acc.nType_move = 699 And mclstmp_undo_move_acc.sReverse <> "1") Or
                         (mclstmp_undo_move_acc.nType_move = 700 And mclstmp_undo_move_acc.sReverse <> "1") Or
                         (mclstmp_undo_move_acc.nType_move = 701 And mclstmp_undo_move_acc.sReverse <> "1") Or
                         (mclstmp_undo_move_acc.nType_move = 800 And mclstmp_undo_move_acc.sReverse <> "1") Or
                         (mclstmp_undo_move_acc.nType_move = 801 And mclstmp_undo_move_acc.sReverse <> "1") Or
                         (mclstmp_undo_move_acc.nType_move = 802 And mclstmp_undo_move_acc.sReverse <> "1") Then
                            .Columns("Sel").Disabled = False
                            .Columns("tctType_move").EditRecord = True
                            .Columns("tctType_move").DefValue = ""
                        Else
                            .Columns("Sel").Disabled = True
                            .Columns("tctType_move").EditRecord = False
                        End If
                    Else
                        .Columns("tctType_move").DefValue = ""
                        If ((mclstmp_undo_move_acc.nType = 1 Or
                             mclstmp_undo_move_acc.nType = 5) And
                             mclstmp_undo_move_acc.sReverse <> "1") Then
                            .Columns("Sel").Disabled = False
                            .Columns("tctType_move").EditRecord = True
                            .Columns("tctType_move").DefValue = 802 'Ajuste-Dev. Prima (-)
                        Else
                            .Columns("Sel").Disabled = True
                            .Columns("tctType_move").EditRecord = False
                        End If
                        If mclstmp_undo_move_acc.nType_move = 709 And
                         Session("nOperat") = "2" And
                         mclstmp_undo_move_acc.sSel <> 1 And
                         mclstmp_undo_move_acc.sReverse <> "1" Then
                            .Columns("Sel").Disabled = False
                            .Columns("tctType_move").EditRecord = True
                            .Columns("tctType_move").DefValue = 803 'Reverso de Rescate Parcial (+)
                        End If
                        If mclstmp_undo_move_acc.nType_move = 710 And
                         Session("nOperat") = "2" And
                         mclstmp_undo_move_acc.sSel <> 1 And
                         mclstmp_undo_move_acc.sReverse <> "1" Then
                            .Columns("Sel").Disabled = False
                            .Columns("tctType_move").EditRecord = True
                            .Columns("tctType_move").DefValue = 804 'Reverso de Rescate Total (+)
                        End If
                        If mclstmp_undo_move_acc.nType_move = 719 And
                         Session("nOperat") = "2" And
                         mclstmp_undo_move_acc.sSel <> 1 And
                         mclstmp_undo_move_acc.sReverse <> "1" Then
                            .Columns("Sel").Disabled = False
                            .Columns("tctType_move").EditRecord = True
                            .Columns("tctType_move").DefValue = 817 'Reverso traspaso fondos (+)
                        End If
                        If mclstmp_undo_move_acc.nType_move = 750 And
                         Session("nOperat") = "2" And
                         mclstmp_undo_move_acc.sSel <> 1 And
                         mclstmp_undo_move_acc.sReverse <> "1" Then
                            .Columns("Sel").Disabled = False
                            .Columns("tctType_move").EditRecord = True
                            .Columns("tctType_move").DefValue = 821 'Reverso de devolución B.Fiscal
                        End If
                    End If


                    .sEditRecordParam = "nType_move='      + marrArray[" & CStr(lintIndex) & "].tctType_move+ '" &
                                        "&dOperdat='       + marrArray[" & CStr(lintIndex) & "].tcdoperdatemanual+ '" &
                                        "&nCredit='        + marrArray[" & CStr(lintIndex) & "].tctCredit+ '" &
                                        "&nDebit='         + marrArray[" & CStr(lintIndex) & "].tctDebit+ '" &
                                        "&nOrigin='        + marrArray[" & CStr(lintIndex) & "].tcnOrigin+ '" &
                                        "&nProfitworker= ' + marrArray[" & CStr(lintIndex) & "].tctProfitworker+ '"

                    .Columns("chkAuxSel").Checked = mclstmp_undo_move_acc.sSel
                    .Columns("chkAuxSel").Disabled = True

                    .Columns("tctType_move").HRefScript = ""
                    If mclstmp_undo_move_acc.nType_move = 1 Or mclstmp_undo_move_acc.nType_move = 2 Or (mclstmp_undo_move_acc.nType_move = 5 And mclstmp_undo_move_acc.nId_reverse <= 0) Or
                        mclstmp_undo_move_acc.nType_move = 14 Or mclstmp_undo_move_acc.nType_move = 699 Or mclstmp_undo_move_acc.nType_move = 700 Or mclstmp_undo_move_acc.nType_move = 701 Or
                        mclstmp_undo_move_acc.nType_move = 800 Or mclstmp_undo_move_acc.nType_move = 801 Or mclstmp_undo_move_acc.nType_move = 802 Then
                        .Columns("Sel").Disabled = False
                        .Columns("tctType_move").EditRecord = True
                    Else
                        .Columns("Sel").Disabled = True
                        .Columns("tctType_move").EditRecord = False
                    End If
                    If CStr(Session("nOperat")) = "2" Then
                        If mclstmp_undo_move_acc.nType_move = 709 And CStr(Session("nOperat")) = "2" And mclstmp_undo_move_acc.sSel <> 1 And mclstmp_undo_move_acc.sReverse <> "1" Then
                            .Columns("Sel").Disabled = False
                            .Columns("tctType_move").EditRecord = True
                            .Columns("tctType_move").DefValue = CStr(803) 'Reverso de Rescate Parcial (+)
                        End If
                        If mclstmp_undo_move_acc.nType_move = 710 And CStr(Session("nOperat")) = "2" And mclstmp_undo_move_acc.sSel <> 1 And mclstmp_undo_move_acc.sReverse <> "1" Then
                            .Columns("Sel").Disabled = False
                            .Columns("tctType_move").EditRecord = True
                            .Columns("tctType_move").DefValue = CStr(804) 'Reverso de Rescate Total (+)
                        End If
                    End If

                    .sEditRecordParam = "nType_move=' + marrArray[" & CStr(lintIndex) & "].tctType_move+ '" & "&dOperdat='  + marrArray[" & CStr(lintIndex) & "].tcdoperdatemanual+ '"
                    Response.Write(.DoRow)
                    lintIndex = lintIndex + 1

                Next mclstmp_undo_move_acc
            End With

        End If
        Response.Write(mobjGrid.closeTable)
        Response.Write(mobjValues.BeginPageButton)
    End Sub

</script>
<%Response.Expires = -1441
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
			
<SCRIPT>

function changeValues(strQuerystring){
	with(self.document.forms[0]){
		insDefValues('ChangeValuesRever',strQuerystring,'/VTimeNet/Policy/PolicyTra/');
		}
	}

	//%insChangeField: Función que maneja el cambio de valores de los controles
	//------------------------------------------------------------------------------------------------------
	function insChangeField(oField) {
	    //------------------------------------------------------------------------------------------------------
	    with (self.document.forms[0]) {
	        switch (oField.name) {
	            case 'cbeoperdatetype': 
	                {
	                    // se toma la fecha del día
	                    if (oField.value == '1') {
	                        tcdoperdate_new.value = hddoperdate_orig.value;
	                    }
	                    // se toma la fecha del último pui
	                    else if (oField.value == '2') {
	                        tcdoperdate_new.value = tcdLastProcess_date.value;
	                    }
	                    // se verifica si la fecha 'cbeoperdatemanualtype' tiene dependencia
	                    if (cbeoperdatemanualtype.value == '2') {
	                        tcdoperdatemanual.value = tcdoperdate_new.value;
	                    }
	                    break;
	                }
	            case 'cbeoperdatemanualtype': 
	                {
	                    // se toma la fecha de operación original del movimiento que se reversa
	                    if (oField.value == '1') {
	                        tcdoperdatemanual.value = hddvaluedate_orig.value;
	                    }
	                    // se toma la fecha del movimiento indicada con anterioridad
	                    else if (oField.value == '2') {
	                        tcdoperdatemanual.value = tcdoperdate_new.value;
	                    }
	                    break;
	                }
	        }
	    }
	}
</SCRIPT>
	<%
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI818"

Response.Write(mobjValues.StyleSheet())

mobjMenues = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenues.sSessionID = Session.SessionID
mobjMenues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "VI818", "VI818.aspx"))
End If
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

mobjMenues = Nothing
%>

</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" ID="FORM" NAME="VI818" ACTION="valPolicyTra.aspx?sCodispl=VI818&sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
		<%Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreVI818()
Else
	inspreVI818upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mcoltmp_undo_move_accs = Nothing
mclstmp_undo_move_acc = Nothing
%>
	</FORM>
</BODY>
</HTML>





