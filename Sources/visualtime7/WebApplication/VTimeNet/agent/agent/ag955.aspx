<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenu As eFunctions.Menues

'-Objeto para recuperar la información de la página
    Dim mclsContrat_Pay As eAgent.Contrat_Pay

'% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid
'-------------------------------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '-------------------------------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
	
        '+ Se definen las columnas del Grid
        'mobjGrid.nMainAction = Request.QueryString("nMainAction")
        mobjGrid.sArrayName = "marrAG955"
        With mobjGrid.Columns
            
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .AddClientColumn(0, GetLocalResourceObject("sClientCaption"), "sClient", mclsContrat_Pay.sClient, , GetLocalResourceObject("sClientToolTip"), , , , , , Request.QueryString.Item("Action") = "Update")
                .AddDateColumn(0, GetLocalResourceObject("dStartDateCaption"), "dStartDate", mclsContrat_Pay.dStartdate, , GetLocalResourceObject("dStartDateToolTip"))
                .AddNumericColumn(0, GetLocalResourceObject("nModulecCaption"), "nModulec", 9, mclsContrat_Pay.NMODULEC, , GetLocalResourceObject("nModulecTooltip"))
                .AddNumericColumn(0, GetLocalResourceObject("nAge_InitCaption"), "nAge_Init", 9, mclsContrat_Pay.NAGE_INIT, , GetLocalResourceObject("nInit_DurColumnToolTip"))
                .AddNumericColumn(0, GetLocalResourceObject("nAge_EndCaption"), "nAge_End", 9, mclsContrat_Pay.NAGE_END, , GetLocalResourceObject("nEnd_DurColumnToolTip"), True, )
                .AddNumericColumn(0, GetLocalResourceObject("nPolicy_DurCaption"), "nPolicy_Dur", 9, mclsContrat_Pay.NPOLICY_DUR, , GetLocalResourceObject("nPolicy_DurToolTip"), True, )
                .AddNumericColumn(0, GetLocalResourceObject("nPercentCaption"), "nPercent", 9, mclsContrat_Pay.nPercent, , GetLocalResourceObject("nPercent_detailColumnToolTip"), True, 6)
                .AddDateColumn(0, GetLocalResourceObject("dEffecDateCaption"), "dEffecdate", mclsContrat_Pay.dEffecdate, , GetLocalResourceObject("dEffecDateToolTip"))
            
            Else
                .AddClientColumn(0, GetLocalResourceObject("sClientCaption"), "sClient", mclsContrat_Pay.sClient, , GetLocalResourceObject("sClientToolTip"), , , , , , Request.QueryString.Item("Action") = "Update")
                .AddDateColumn(0, GetLocalResourceObject("dStartDateCaption"), "dStartDate", mclsContrat_Pay.dStartdate, , GetLocalResourceObject("dStartDateToolTip"))
                'manejo de combo para nmodulec
                .AddPossiblesColumn(0, GetLocalResourceObject("nModulecCaption"), "nModulec", "TabModulec", 0, , True, , , , , True, , GetLocalResourceObject("nModulecTooltip"))
                mobjGrid.Columns("nModulec").Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable, "nBranch")
                mobjGrid.Columns("nModulec").Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable, "nProduct")
                mobjGrid.Columns("nModulec").Parameters.Add("dEffecdate", DateTime.Now, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable, "dEffecdate")
                'fin manejo
                .AddNumericColumn(0, GetLocalResourceObject("nAge_InitCaption"), "nAge_Init", 9, mclsContrat_Pay.NAGE_INIT, , GetLocalResourceObject("nInit_DurColumnToolTip"), , , , , , True)
                .AddNumericColumn(0, GetLocalResourceObject("nAge_EndCaption"), "nAge_End", 9, mclsContrat_Pay.NAGE_END, , GetLocalResourceObject("nEnd_DurColumnToolTip"), True, )
                .AddNumericColumn(0, GetLocalResourceObject("nPolicy_DurCaption"), "nPolicy_Dur", 9, mclsContrat_Pay.NPOLICY_DUR, , GetLocalResourceObject("nPolicy_DurToolTip"), True, , , , , True)
                .AddNumericColumn(0, GetLocalResourceObject("nPercentCaption"), "nPercent", 9, mclsContrat_Pay.nPercent, , GetLocalResourceObject("nPercent_detailColumnToolTip"), True, 6)
                .AddDateColumn(0, GetLocalResourceObject("dEffecDateCaption"), "dEffecdate", mclsContrat_Pay.dEffecdate, , GetLocalResourceObject("dEffecDateToolTip"),,,,True)
            End If
            '.AddHiddenColumn("hddnModulec", vbNullString)
        End With
	
        '+ Se asignan las caracteristicas del Grid
        With mobjGrid
            .AddButton = True
            .DeleteButton = True
            .Codispl = "AG955"
            .Codisp = "AG955"
            .sCodisplPage = "AG955"
            .Columns("sClient").EditRecord = True
            ' .Columns("hddnnUsercode").DefValue = Session("nUsercode")
            '.Columns("hddnsRoutine").DefValue = mclsContrat_Pay.SROUTINE
            
            mobjGrid.ActionQuery = mobjValues.ActionQuery
            '+ Pase de parametros necesarios para la eliminación de registros
            .sDelRecordParam = "&nBranch=" & Request.QueryString.Item("nBranch") &
                                "&nProduct=" & Request.QueryString.Item("nProduct") &
                                "&nContrat_Pay=" & Request.QueryString.Item("nContrat_Pay") &
                                "&nModulec='+marrAG955[lintIndex].nModulec + '" &
                                "&nPolicy_Dur='+marrAG955[lintIndex].nPolicy_Dur + '" &
                                "&nAge_Init='+marrAG955[lintIndex].nAge_Init + '" &
                                "&dEffecdate='+marrAG955[lintIndex].dEffecdate + '"
            
            .sEditRecordParam = "&nBranch=" & Request.QueryString.Item("nBranch") &
                                "&nProduct=" & Request.QueryString.Item("nProduct") &
                                "&nUsercode=" & Session("nUsercode") &
                                "&nContrat_Pay=" & Request.QueryString.Item("nContrat_Pay") &
                                "&dEffecdate=" & Request.QueryString.Item("dEffecdate") &
                                "&sDescript=' + self.document.forms[0].sDescript.value + '" &
                                "&nType_Calc=' + self.document.forms[0].nType_Calc.value + '" &
                                "&nAmount_Ini=' + self.document.forms[0].nAmount_Ini.value + '" &
                                "&nAmount=' + self.document.forms[0].nAmount.value + '" &
                                "&nCurrency=' + self.document.forms[0].nCurrency.value + '" &
                                "&nAply=' + self.document.forms[0].nAply.value + '" &
                                "&sTaxin=' + self.document.forms[0].sTaxin.value + '" &
                                "&sStatregt=' + self.document.forms[0].sStatregt.value + '" &
                                "&nTyp_acco=' + self.document.forms[0].nTyp_acco.value + '" &
                                "&nType_Contrat=' + self.document.forms[0].nType_Contrat.value + '"
            .Height = 350
            .Width = 400
                       
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
		
            ' If Request.QueryString.Item("Type") = "PopUp" Then
            '  Response.Write(mobjValues.HiddenControl("hddnnUsercode", Request.QueryString.Item("nUsercode")))
            ' Response.Write(mobjValues.HiddenControl("hddnsRoutine", Request.QueryString.Item("sRoutine")))
            'Response.Write(mobjValues.HiddenControl("hddStartDate", Request.QueryString.Item("dStartDate")))
            'Response.Write(mobjValues.HiddenControl("hdddEffecdate", Request.QueryString.Item("dEffecdate")))
            'Response.Write(mobjValues.HiddenControl("hddType_Calc", Request.QueryString.Item("nType_Calc")))
            'Response.Write(mobjValues.HiddenControl("hddPercent", Request.QueryString.Item("nPercent")))
            'Response.Write(mobjValues.HiddenControl("hddAmount", Request.QueryString.Item("nAmount")))
            'Response.Write(mobjValues.HiddenControl("hddCurrency", Request.QueryString.Item("nCurrency")))
            'Response.Write(mobjValues.HiddenControl("hddAply", Request.QueryString.Item("nAply")))s
            'Response.Write(mobjValues.HiddenControl("hddTaxin", Request.QueryString.Item("sTaxin")))
            'Response.Write(mobjValues.HiddenControl("hddStatregt", Request.QueryString.Item("sStatregt")))
            '  End If
		
        End With
    End Sub

    '% InsPreAG955: Esta función permite realizar la lectura de la tabla principal de la transacción. 
    '---------------------------------------------------------------------------------------------------
    Private Sub InsPreAG955()
        '---------------------------------------------------------------------------------------------------
        'Call mclsContrat_Pay.InsPreAG955(Session("nContrat_Pay"), Session("nBranch"), Session("nProduct"))
        
        Call mclsContrat_Pay.InsPreAG955(Request.QueryString.Item("nContrat_Pay"), Request.QueryString.Item("nBranch"), Request.QueryString.Item("nProduct"))
        'Response.Write("" & vbCrLf)
        Response.Write("<TABLE WIDTH=""100%"" >" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        'Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("sClientCaption") & "</LABEL></TD>" & vbCrLf)
        'Response.Write("        <TD COLSPAN=""4"">")
        'Response.Write(mobjValues.ClientControl("sClient", mclsContrat_Pay.sClient, , GetLocalResourceObject("sClientToolTip"), , , "lblCliename", False, , , , , , True))
        'Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        'Response.Write(mobjValues.HiddenControl("sClient", mclsContrat_Pay.sClient))
        
        'Descripcion + Tipo_Contrato
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("sDescriptCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.TextControl("sDescript", 60, mclsContrat_Pay.sDescript, , GetLocalResourceObject("sDescriptToolTip")))
        Response.Write("</TD>        " & vbCrLf)
        
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nType_ContratCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.TextControl("nType_Contrat", 10, mclsContrat_Pay.NTYPE_CONTRAT, , GetLocalResourceObject("nType_ContratToolTip")))
        Response.Write("</TD>        " & vbCrLf)
        
        
        'COMMENT FechaInicio
        'Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("dStartDateCap") & "</LABEL></TD>" & vbCrLf)
        'Response.Write("	    <TD> ")
        'Response.Write(mobjValues.DateControl("dStartDate", CStr(mclsContrat_Pay.dStartdate), , GetLocalResourceObject("dStartDateToolTip")))
        'Response.Write(" </TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        
        'Tipo de Calculo + Porcentaje
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nType_CalcCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.PossiblesValues("nType_Calc", "Table8100", eFunctions.Values.eValuesType.clngComboType, CStr(mclsContrat_Pay.nType_Calc), , , , , , , , , GetLocalResourceObject("nType_CalcToolTip")))
        Response.Write("</TD>" & vbCrLf)
        
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nAmount_IniCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.NumericControl("nAmount_Ini", 18, mclsContrat_Pay.NAMOUNT_INI, , GetLocalResourceObject("nAmount_IniToolTip"), , 6))
        Response.Write("</TD>" & vbCrLf)
        
        'COMMENT
        'Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nPercentCaption") & "</LABEL></TD>" & vbCrLf)
        'Response.Write("        <TD>")
        'Response.Write(mobjValues.NumericControl("nPercent", 9, CStr(mclsContrat_Pay.nPercent), , GetLocalResourceObject("nPercentToolTip"), , 6))
        'Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        
        'Moneda + Monto
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.PossiblesValues("nCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsContrat_Pay.nCurrency), , , , , , , , , GetLocalResourceObject("nCurrencyToolTip")))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nAmountCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.NumericControl("nAmount", 18, mclsContrat_Pay.nAmount, , GetLocalResourceObject("nAmountToolTip"), , 6))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        
        'Aplica + impuesto + contrat
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nAplyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.PossiblesValues("nAply", "Table8101", eFunctions.Values.eValuesType.clngComboType, CStr(mclsContrat_Pay.nAply), , , , , , , , , GetLocalResourceObject("nAplyToolTip")))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.CheckControl("sTaxin", GetLocalResourceObject("sTaxinCaption"), mclsContrat_Pay.sTaxin, "1", , , , GetLocalResourceObject("sTaxinToolTip")))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("        <TD>")
        'Response.Write(mobjValues.HiddenControl("nContrat_Pay", Request.QueryString.Item("nContrat_Pay")))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        
        'Tipo cuenta + Estado registro
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nType_accoCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("        ")
        
        'mobjValues.BlankPosition = False
        'mobjValues.TypeList = CShort("2")
        'mobjValues.List = "2"
        
        Response.Write(mobjValues.PossiblesValues("nTyp_acco", "Table400", eFunctions.Values.eValuesType.clngComboType, mclsContrat_Pay.nTyp_acco, , , , , , , , , GetLocalResourceObject("nType_accoToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("        </TD>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("sStatregtCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("        ")
        mobjValues.BlankPosition = False
        mobjValues.TypeList = CShort("2")
        mobjValues.List = "2"
        Response.Write(mobjValues.PossiblesValues("sStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, mclsContrat_Pay.sStatregt, , , , , , , , , GetLocalResourceObject("sStatregtToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("        </TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        
        'Modulo + Año 
        'COMMENT
        Response.Write("    <TR>" & vbCrLf)
        'Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nModulec_Caption") & "</LABEL></TD>" & vbCrLf)
        'Response.Write("        <TD>")
        'Response.Write(mobjValues.NumericControl("nModulec", 9, CStr(mclsContrat_Pay.NMODULEC), , GetLocalResourceObject("nModulecToolTip"), , 0))
        'Response.Write("</TD>" & vbCrLf)
        'Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nPolicy_DurCaption") & "</LABEL></TD>" & vbCrLf)
        'Response.Write("        <TD>")
        'Response.Write(mobjValues.NumericControl("nPolicy_Dur", 9, CStr(mclsContrat_Pay.NPOLICY_DUR), , GetLocalResourceObject("nPolicy_DurToolTip"), , 0))
        'Response.Write("</TD>" & vbCrLf)
        
        'Edad Inicial + Edad Final
        'COMMENT
        Response.Write("    <TR>" & vbCrLf)
        'Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nAge_InitCaption") & "</LABEL></TD>" & vbCrLf)
        'Response.Write("        <TD>")
        'Response.Write(mobjValues.NumericControl("nAge_Init", 9, CStr(mclsContrat_Pay.NAGE_INIT), , GetLocalResourceObject("nAge_InitToolTip"), , 0))
        'Response.Write("</TD>" & vbCrLf)
        'Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nAge_EndCaption") & "</LABEL></TD>" & vbCrLf)
        'Response.Write("        <TD>")
        'Response.Write(mobjValues.NumericControl("nAge_End", 9, CStr(mclsContrat_Pay.NAGE_END), , GetLocalResourceObject("nAge_EndToolTip"), , 0))
        'Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        
        Response.Write("    <TR> </TR>" & vbCrLf)
        Response.Write("</TABLE>" & vbCrLf)
        
        
        Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TR>            " & vbCrLf)
        Response.Write("        <TD CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD CLASS=""Horline""></TD>" & vbCrLf)
        Response.Write("    </TR>  " & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("            ")

	
        Call insPreAG955Grid()
        Response.Write("" & vbCrLf)
        Response.Write("        </TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        Response.Write("</TABLE>")

        mclsContrat_Pay = Nothing
        Response.Write("<SCRIPT>")
        Response.Write("with (document.forms[0]){")
        'Response.Write("if (top.dStartDate) dStartDate.value = top.dStartDate;")
        'Response.Write("if (top.dEffecdate) dEffecdate.value = top.dEffecdate;")
        'Response.Write("if (top.bClient) sClient.value = top.sClient;")
        'Response.Write("if (top.bPercent) nPercent.value = top.nPercent;")
        'Response.Write("if (top.bnModulec) nModulec.value = top.nModulec;")
        'Response.Write("if (top.bnAge_Init) nAge_Init.value = top.nAge_Init;")
        'Response.Write("if (top.bnAge_End) nAge_End.value = top.nAge_End;")
        'Response.Write("if (top.bnPolicy_Dur) nPolicy_Dur.value = top.nPolicy_Dur;")
        'Response.Write("if (top.bDescript) sDescript.value = top.sDescript;")
        'Response.Write("if (top.bType_Calc) nType_Calc.value = top.nType_Calc;")
        'Response.Write("if (top.bAmount) nAmount.value = top.nAmount;")
        'Response.Write("if (top.bCurrency) nCurrency.value = top.nCurrency;")
        'Response.Write("if (top.bAply) nAply.value = top.nAply;")
        Response.Write("if (top.bTaxin) sTaxin.checked = top.sTaxin;")
        'Response.Write("if (top.bStatregt) sStatregt.value = top.sStatregt;")
        Response.Write("}")
        Response.Write("</" & "Script>")
    End Sub
    
    '%insPreAG955: Se cargan los datos iniciales de la página de la parte repetitiva 1
    '-------------------------------------------------------------------------------------------------------------------
    Private Sub insPreAG955Grid()
        '-------------------------------------------------------------------------------------------------------------------
        
        Dim lclsContrat_Pay_Prod As eAgent.Contrat_Pay
        
        If mclsContrat_Pay.blnValues Then
            
            With mobjGrid
                If mclsContrat_Pay.mcolContrat_Pay_Prod.Count > 0 Then
                    For Each lclsContrat_Pay_Prod In mclsContrat_Pay.mcolContrat_Pay_Prod
                        .Columns("dStartDate").DefValue = CDate(lclsContrat_Pay_Prod.dStartdate)
                        .Columns("dEffecdate").DefValue = CDate(lclsContrat_Pay_Prod.dEffecdate)
                        .Columns("nModulec").DefValue = CInt(lclsContrat_Pay_Prod.NMODULEC)
                        .Columns("nAge_Init").DefValue = CInt(lclsContrat_Pay_Prod.NAGE_INIT)
                        .Columns("nAge_End").DefValue = CInt(lclsContrat_Pay_Prod.NAGE_END)
                        .Columns("nPolicy_Dur").DefValue = CInt(lclsContrat_Pay_Prod.NPOLICY_DUR)                     
                        .Columns("sClient").DefValue = CStr(lclsContrat_Pay_Prod.sClient)
                        .Columns("nPercent").DefValue = CInt(lclsContrat_Pay_Prod.nPercent)
                        Response.Write(.DoRow)
                    Next lclsContrat_Pay_Prod
                End If
                Response.Write("<SCRIPT>document.forms[0].action=document.forms[0].action + '&nContrat_Pay=" & mclsContrat_Pay.mcolContrat_Pay_Prod.Count & "'</" & "Script>")
                
            End With
        Else
            
            Response.Write("<SCRIPT>document.forms[0].action=document.forms[0].action + '&nContrat_Pay=0'</" & "Script>")
        End If
        Response.Write(mobjGrid.closeTable())
 
    End Sub

    '% insPreAG955Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '------------------------------------------------------------------------------------------------------------------------------
    Private Sub insPreAG955Upd()
        '------------------------------------------------------------------------------------------------------------------------------
        With Request
            If .Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                'Call mclsContrat_Pay.InsPostAG954Upd(.QueryString.Item("Action"), CInt(.QueryString.Item("nContrat_Pay")), vbNullString, vbNullString, System.DateTime.FromOADate(eRemoteDB.Constants.intNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.intNull), vbNullString, mobjValues.StringToType(.QueryString.Item("nSeq"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"))
                
                Call mclsContrat_Pay.InsPostAG955Upd(.QueryString.Item("Action"), _
                                                        .QueryString.Item("nBranch"), _
                                                        .QueryString.Item("nProduct"), _
                                                        .QueryString.Item("nContrat_Pay"), _
                                                        mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                        eRemoteDB.Constants.dtmNull, _
                                                        eRemoteDB.Constants.strNull, _
                                                        eRemoteDB.Constants.strNull, _
                                                        eRemoteDB.Constants.dtmNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        eRemoteDB.Constants.strNull, _
                                                        eRemoteDB.Constants.strNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        eRemoteDB.Constants.strNull, _
                                                        eRemoteDB.Constants.intNull, _
                                                        mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdInteger, ), _
                                                        mobjValues.StringToType(.QueryString.Item("nPolicy_Dur"), eFunctions.Values.eTypeData.etdInteger, ), _
                                                        mobjValues.StringToType(.QueryString.Item("nAge_Init"), eFunctions.Values.eTypeData.etdInteger, ), _
                                                     eRemoteDB.Constants.intNull)
            End If 'mobjValues.StringToType(.QueryString.Item("nType_Calc"), eFunctions.Values.eTypeData.etdDouble)  <- ESTA ES LA FORMA CORRECTA
            
            
            Response.Write(mobjGrid.DoFormUpd(.Item("Action"), "ValAgent.aspx", .Item("sCodispl"), .Item("nMainAction"), mobjGrid.ActionQuery, CShort(.Item("Index"))))
        End With
	
        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT LANGUAGE=javascript>" & vbCrLf)
        'Response.Write("    var sClient" & vbCrLf)
        'Response.Write("    var sDescript" & vbCrLf)
        'Response.Write("    var dStartDate" & vbCrLf)
        'Response.Write("    var nModulec" & vbCrLf)
        'Response.Write("    var nType_Calc" & vbCrLf)
        'Response.Write("    var nPercent" & vbCrLf)
        'Response.Write("    var nAmount" & vbCrLf)
        'Response.Write("    var nCurrency" & vbCrLf)
        'Response.Write("    var nAply" & vbCrLf)
        Response.Write("    var sTaxin" & vbCrLf)
        'Response.Write("    var sStatregt" & vbCrLf)
        'Response.Write("            " & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].sClient)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.sClient = top.opener.document.forms[0].sClient.value" & vbCrLf)
        'Response.Write("        top.opener.top.bClient = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].sDescript)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.sDescript = top.opener.document.forms[0].sDescript.value" & vbCrLf)
        'Response.Write("        top.opener.top.bDescript = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].dStartDate)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.dStartDate = top.opener.document.forms[0].dStartDate.value" & vbCrLf)
        'Response.Write("        top.opener.top.bStartDate = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].nType_Calc)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.nType_Calc = top.opener.document.forms[0].nType_Calc.value" & vbCrLf)
        'Response.Write("        top.opener.top.bType_Calc = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].nPercent)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.nPercent = top.opener.document.forms[0].nPercent.value" & vbCrLf)
        'Response.Write("        top.opener.top.bPercent = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].nAmount)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.nAmount = top.opener.document.forms[0].nAmount.value" & vbCrLf)
        'Response.Write("        top.opener.top.bAmount = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].nCurrency)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.nCurrency = top.opener.document.forms[0].nCurrency.value" & vbCrLf)
        'Response.Write("        top.opener.top.bCurrency = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].nAply)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.nAply = top.opener.document.forms[0].nAply.value" & vbCrLf)
        'Response.Write("        top.opener.top.bAply = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        Response.Write("    if (typeof(top.opener.document.forms[0].sTaxin)!=""undefined""){    " & vbCrLf)
        Response.Write("        top.opener.top.sTaxin = top.opener.document.forms[0].sTaxin.checked" & vbCrLf)
        Response.Write("        top.opener.top.bTaxin = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        'Response.Write("    if (typeof(top.opener.document.forms[0].sStatregt)!=""undefined""){" & vbCrLf)
        'Response.Write("        top.opener.top.sStatregt = top.opener.document.forms[0].sStatregt.value" & vbCrLf)
        'Response.Write("        top.opener.top.bStatregt = true" & vbCrLf)
        'Response.Write("    }" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("</" & "SCRIPT>    ")

	
    End Sub

</script>
<%  Response.Expires = -1
    mobjValues = New eFunctions.Values
    mclsContrat_Pay = New eAgent.Contrat_Pay
    mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut)
    mobjValues.sCodisplPage = "AG955"
    
    '+Seteo de parámetros traídos de página encabezado
    'Session("nBranch") = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger                                            )
    'Session("nProduct") = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong)
    'Session("nContrat_Pay") = mobjValues.StringToType(Request.QueryString.Item("nContrat_Pay"), eFunctions.Values.eTypeData.etdLong)
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
        Response.Write(mobjMenu.setZone(2, "AG955", ""))
	mobjMenu = Nothing
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:57 $|$$Author: Nvaplat18 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%  Response.Write(mobjValues.ShowWindowsName("AG955", Request.QueryString.Item("sWindowDescript")))%>
<FORM METHOD="POST" ID="FORM" NAME="frmAG955" ACTION="ValAgent.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nContrat_Pay=<%=Request.QueryString.Item("nContrat_Pay")%>">
<%

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
        Call InsPreAG955()
Else
        Call insPreAG955Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
