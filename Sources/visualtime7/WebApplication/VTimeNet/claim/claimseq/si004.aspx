
<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenu As eFunctions.Menues
    Dim mclsClaim As eClaim.Claim


    '% insDefineHeader: Se definen las propiedades del grid
    '----------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '----------------------------------------------------------------------------------------------
        Dim lstrQueryString As String

        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        mobjGrid.sCodisplPage = "si004"
        Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        Else
            mobjGrid.ActionQuery = False
            Session("bQuery") = False
        End If
        With mobjGrid.Columns
            .AddNumericColumn(9480, "Número", "tcnCaseNum", 4, CStr(0), True, "Número de caso", False, 0, , , "ChangeValues('Casenum',this);", Request.QueryString("Action") <> "Add")
            .AddPossiblesColumn(9481, "Tipo de reclamo", "cboRtype", "Table692", eFunctions.Values.eValuesType.clngComboType, CStr(0), , , , , "self.document.forms[0].nDeman_type.value = this.value", Request.QueryString("Action") <> "Add", , "Razón por la cual está realizando el reclamo el cliente.")
            .AddPossiblesColumn(9482, "Estado", "cbeStatReserv", "Table135", eFunctions.Values.eValuesType.clngComboType, CStr(6), , , , , , True, , "Estado actual en que se encuentran las provisiones asociadas al caso.")
            .AddButtonColumn(0, "Notas", "SCA2-S", 0, True, Request.QueryString("Type") <> "PopUp" Or Session("bQuery"), , , , , "btnNotenum")

            '+ Si se encuentra en la PopUp, muestra un check, en caso contrario, un combo para mostrar la descripción
            'If Request.QueryString("Type") <> "PopUp" Then
            '        .AddPossiblesColumn(40303, "Siniestrado", "cbeReclaim", "Table23", eFunctions.Values.eValuesType.clngComboType, CStr(1))
            'Else
            .AddCheckColumn(40304, "Siniestrado", "cbeReclaim", "", "2", "1", , Request.QueryString("Type") <> "PopUp")
            'End If
            '+ Si se encuentra en la PopUp, muestra un check para indicar si el cliente es asegurado
            If Request.QueryString("Type") = "PopUp" Then
                .AddCheckColumn(0, "Búsqueda de asegurados", "chkInsured", vbNullString, CShort("1"), "1", "InsShowPopUp(this)", Request.QueryString("Action") <> "Add", "Si esta seleccionado permite realizar la busqueda de clientes asociados a la poliza")
            End If

            mobjValues.TypeList = 2
            mobjValues.ClientRole = "1,13,16,25"
            lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dOccurdate_l") & "&sForm=SI004"

            .AddClientColumn(0, "Cliente", "tctClientCode", Request.QueryString("sClient"), , "Código Cliente", "ChangeValues('Client', this)", Request.QueryString("Action") <> "Add", , True, , , , , , lstrQueryString, True, eFunctions.Values.eTypeClient.SearchClientPolicy)
            .AddTextColumn(0, "Apellido paterno", "tctLastName", 19, "", , "Apellido paterno del cliente.", , , , True)
            .AddTextColumn(0, "Apellido materno", "tctLastName2", 19, "", , "Apellido materno del cliente.", , , , True)

            If Request.QueryString("Type") <> "PopUp" Then
                .AddTextColumn(0, "Nombres", "tctFirstName", 60, "", , "Nombres del cliente.", , , , True)
            Else
                .AddTextAreaColumn(0, "Nombres", "tctFirstName", "", 3, 20, True, "Nombres del cliente.", True)
            End If
            .AddPossiblesColumn(0, "Parentesco", "cbeRelaship", "Table15", eFunctions.Values.eValuesType.clngComboType, , , , , , , True, , "Parentesco entre el cliente introducido y el asegurado.", eFunctions.Values.eTypeCode.eString)
            .AddCheckColumn(0, "Contingente", "chkConti", "", , , , True, "Indicador de beneficiario contingente")
            .AddPossiblesColumn(9490, "Figura", "cbeRole", "tabCliallocla", eFunctions.Values.eValuesType.clngComboType, , True, , , , "DisabledRelaship();insRoleChanged();", , , "Tipo de papel/rol que representa el reclamante con respecto al siniestro en tratamiento.", eFunctions.Values.eTypeCode.eString)
            .AddAnimatedColumn(0, "Preexistencias", "btnPreexistencias", "/VTimeNet/images/btn_ValuesOff.png", , , "JAVASCRIPT: insOpenPreexistencia()")
            If Request.QueryString("Type") = "PopUp" Then
                .AddAnimatedColumn(100693, "Información adicional del cliente", "btnQuery", "/VTimeNet/Images/clfolder.png", "Secuencia de clientes")
                .Item("btnQuery").HRefScript = "ShowClientSequence();"
                .AddTextColumn(0, "Nav", "tctClientNavigation", 60, "", , "Nav", , , "ChangeValues(""Client"", $(""[name=tctClientCode]"").get()[0]);")
            End If


            .AddHiddenColumn("dDecladat", "")
            .AddHiddenColumn("dOccurdat", "")
            .AddHiddenColumn("nHour", "")
            .AddHiddenColumn("dPrescdat", "")
            .AddHiddenColumn("dLimit_pay", "")
            .AddHiddenColumn("nOffice_pay", "")
            .AddHiddenColumn("nOfficeAgen_pay", "")
            .AddHiddenColumn("nAgency_pay", "")
            .AddHiddenColumn("nRelaship", "")
            .AddHiddenColumn("nClaimCause", "")
            .AddHiddenColumn("nTotalLoss", "")
            .AddHiddenColumn("sClientInsured", "")
            .AddHiddenColumn("sStatReserv", "")
            .AddHiddenColumn("sParam", "")
            .AddHiddenColumn("nTypeCN", "")
            .AddHiddenColumn("dBegin", "")
            .AddHiddenColumn("dEnd", "")
            .AddHiddenColumn("nCase_num", "")
            .AddHiddenColumn("nDeman_type", "")
            .AddHiddenColumn("tcnNoteNum_aux", vbNullString)
            .AddHiddenColumn("hdnId", "")
            .AddHiddenColumn("nClaimParent", "")

        End With

        With mobjGrid
            .Codispl = "SI004"
            .Top = 50
            .Left = 200
            .Width = 500
            .Height = 500
            .AddButton = True
            .DeleteButton = True
            .WidthDelete = 435
            .Columns("Sel").OnClick = "MarkRecord(this);ChangeValues(""FindChildren"",this)"
            .Columns("cboRtype").EditRecord = True
            .sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
            .sEditRecordParam = "sTotalLoss='       + self.document.forms[0].hddTotalLoss.value + '" & "&nOffice_pay='     + self.document.forms[0].cbeOffice.value + '" & "&nOfficeAgen_pay=' + self.document.forms[0].cbeOfficeAgen.value + '" & "&nAgency_pay='     + self.document.forms[0].cbeAgency.value + '"

            If Request.QueryString("Type") = "PopUp" Then
                .sQueryString = "nCase_num=' + (typeof(self.document.forms[0].nCase_num[0])!='undefined'?self.document.forms[0].nCase_num[0].value:self.document.forms[0].nCase_num.value) + '"
                .sQueryString = mobjGrid.sQueryString & "&nDeman_type=' + (typeof(self.document.forms[0].nDeman_type[0])!='undefined'?self.document.forms[0].nDeman_type[0].value:self.document.forms[0].nDeman_type.value) + '"
            End If

            .Columns("cbeRole").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeRole").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cboRtype").BlankPosition = False
            .Columns("cboRtype").List = mclsClaim.Deman_typeList(CInt(Session("nBranch")), CInt(Session("nProduct")))
            .Columns("cboRtype").TypeList = mclsClaim.DTypeList
            .Columns("chkConti").PopUpVisible = False

            If Request.QueryString("Reload") = "1" Then
                .sReloadIndex = Request.QueryString("ReloadIndex")
            End If
        End With
    End Sub

    '% insPreSI004: Se realiza el manejo del grid y se cargan los datos del Folder
    '----------------------------------------------------------------------------------------------
    Private Sub insPreSI004()
        '----------------------------------------------------------------------------------------------
        Dim lcolClaimCases As eClaim.Claim_cases
        Dim lclsClaim_case As eClaim.Claim_case
        Dim lclsClaim_caus As eClaim.Claim_caus
        Dim lintCount As Byte
        Dim lintOffice_pay As Object
        Dim lintOfficeAgen_pay As Object
        Dim lintAgency_pay As Object
        Dim lstrClaim_typ As Object
        Dim lclnclaimparent As Integer
        If mclsClaim.nOffice_pay <> eRemoteDB.Constants.intNull Then
            Session("nOffice_pay") = mclsClaim.nOffice_pay
        Else
            Session("nOffice_pay") = Session("nOffice_pol")
        End If
        If mclsClaim.nOfficeAgen_pay <> eRemoteDB.Constants.intNull Then
            Session("nOfficeAgen_pay") = mclsClaim.nOfficeAgen_pay
        Else
            Session("nOfficeAgen_pay") = Session("nOfficeAgen_pol")
        End If
        If mclsClaim.nAgency_pay <> eRemoteDB.Constants.intNull Then
            Session("nAgency_pay") = mclsClaim.nAgency_pay
        Else
            Session("nAgency_pay") = Session("nAgency_pol")
        End If

        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9487>Fecha denuncio</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("gmdDeclaDat", CStr(mclsClaim.dDecladat),  , "Fecha en la que el cliente ha declarado el siniestro",  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9489>Entrega de doc.</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("gmdPrescDat", CStr(mclsClaim.dPrescdat), , "Indica hasta que día se tiene oportunidad de traer los documentos"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("           </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9488>Fecha ocurrencia</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("gmdOccurDat", CStr(mclsClaim.dOccurdat),  , "Fecha en la que ha ocurrido el siniestro.",  ,  ,  , "ChangeValues(""Occurdat"", this)", True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9483>Hora</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.TextControl("gmnHour", 5, mclsClaim.sHour,  , "Indica la hora en que ha ocurrido el siniestro.",  ,  ,  , "insFormatTime(this.value);"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9485>Causa</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        Response.Write(mobjValues.PossiblesValues("cbeClaimCaus", "tabclaim_caus", eFunctions.Values.eValuesType.clngComboType, CStr(mclsClaim.nCausecod), True, , , , , "ChangeValues(""ClaimCaus"",this)", , , "Indica el motivo que ocasionó el siniestro."))
        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            ")


        If mclsClaim.nCauseCod <> eRemoteDB.Constants.intNull Then
            lclsClaim_caus = New eClaim.Claim_caus
            If lclsClaim_caus.Find(CInt(Session("nBranch")), CInt(Session("nProduct")), mclsClaim.nCauseCod) Then
                lstrClaim_typ = lclsClaim_caus.sClaimtyp
            End If
        Else
            lstrClaim_typ = 3
        End If
        If Request.QueryString("nOffice_pay") <> vbNullString Then
            lintOffice_pay = Request.QueryString("nOffice_pay")
        Else
            lintOffice_pay = Session("nOffice_pay")
        End If
        'ok
        If Request.QueryString("nOfficeAgen_pay") <> vbNullString Then
            lintOfficeAgen_pay = Request.QueryString("nOfficeAgen_pay")
        Else
            lintOfficeAgen_pay = Session("nOfficeAgen_pay")
        End If
        'ok
        If Request.QueryString("nAgency_pay") <> vbNullString Then
            lintAgency_pay = Request.QueryString("nAgency_pay")
        Else
            lintAgency_pay = Session("nAgency_pay")
        End If
        Select Case mclsClaim.sClaimtyp
            Case "1", "3" '+ Pérdida Parcial o Ambas
                Response.Write("" & vbCrLf)
                Response.Write("                    <TD COLSPAN=""2"">")


                Response.Write(mobjValues.CheckControl("chkTotalLoss", "Pérdida total", CStr(2), CStr(2), "ChangeValues(""TotalLoss"",this)", lstrClaim_typ <> "3"))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("                ")

            Case "2" '+ Pérdida Total
                Response.Write("" & vbCrLf)
                Response.Write("                    <TD COLSPAN=""2"">")


                Response.Write(mobjValues.CheckControl("chkTotalLoss", "Pérdida total", CStr(1), CStr(1), "ChangeValues(""TotalLoss"",this)", lstrClaim_typ <> "3"))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("            ")

        End Select
        Response.Write("" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9489>Fecha límite</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("gmdLimit_pay", CStr(mclsClaim.dLimit_pay),  , "Fecha máxima para la liquidación del siniestro en tratamiento."))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9489>Destino del cheque</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        With mobjValues
            '.Parameters.Add("nUsercode", Session("nUsercode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(.PossiblesValues("cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType, CStr(Session("nOffice_pay")),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1,0)", False))
        End With

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Oficina</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        With mobjValues
            .Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.ReturnValue("nBran_off",  ,  , True)
            Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, CStr(Session("nOfficeAgen_pay")), True,  ,  ,  ,  , "BlankAgencyDepend();insInitialAgency(2,0)", False,  , "Oficina donde el beneficiario del pago del siniestro retirará el cheque"))
        End With

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Agencia</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        With mobjValues
            .Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.ReturnValue("nBran_off",  ,  , True)
            .Parameters.ReturnValue("nOfficeAgen",  ,  , True)
            .Parameters.ReturnValue("sDesAgen",  ,  , True)
            Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType, CStr(Session("nAgency_pay")), True,  ,  ,  ,  , "insInitialAgency(3,0)", False,  , "Agencia a la que pertenece el siniestro en tratamiento"))
        End With

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Siniestro padre</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        With mobjValues
            .TypeList = Values.ecbeTypeList.Exclution
            .List = Session("nClaim")
            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(.PossiblesValues("cbeClaimParent", "TABCLAIM_POLICY", eFunctions.Values.eValuesType.clngComboType, mclsClaim.nClaimParent, True, , , , , "", False, , "Siniestro padre"))
        End With

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD>")
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted"" ALIGN=""RIGHT""><LABEL ID=40302><A NAME=""Casos"">Casos</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        ")

        Response.Write(mobjValues.HiddenControl("hddTotalLoss", mclsClaim.sClaimtyp))
        Response.Write(mobjValues.HiddenControl("hddOffice_pay", lintOffice_pay))
        Response.Write(mobjValues.HiddenControl("hddOfficeAgen_pay", lintOfficeAgen_pay))
        Response.Write(mobjValues.HiddenControl("hddAgency_pay", lintAgency_pay))

        Response.Write("" & vbCrLf)
        Response.Write("    </TABLE>")


        Response.Write(mobjValues.HiddenControl("tctClient", mclsClaim.sClient))
        lclnclaimparent = mclsClaim.nClaimParent
        lcolClaimCases = mclsClaim.Claim_cases
        lintCount = 0
        Dim lintIndex As Short
        lintIndex = 0
        For Each lclsClaim_case In lcolClaimCases
            With mobjGrid
                '+ Se "arma" un QueryString con los parámetros de borrado. Estos valores serán pasados a la
                '+ función insPostSI004 cuando se eliminen los registros seleccionados
                .Columns("sParam").DefValue = "nClaim=" & Session("nClaim") & "&nStatusInstance=1" & "&nCase_num=" & CStr(lclsClaim_case.nCase_num) & "&nDeman_type=" & CStr(lclsClaim_case.nDeman_type) & "&nId=" & CStr(lclsClaim_case.nId) & "&cbeStatReserv=" & lclsClaim_case.sStaCase & "&nNotes=" & "0" & "&cbeInsured=" & lclsClaim_case.sClient & "&tctClientCode=" & CStr(lclsClaim_case.sClient) & "&cbeReclaim=" & lclsClaim_case.sDemandant & "&cbeRole=" & lclsClaim_case.nBene_type & "&lstrAction" & "Delete"

                .Columns("tcnCaseNum").DefValue = CStr(lclsClaim_case.nCase_num)
                .Columns("cboRType").DefValue = CStr(lclsClaim_case.nDeman_type)
                .Columns("cbeStatReserv").Descript = lclsClaim_case.sStaReserve
                .Columns("btnNotenum").nNotenum = lclsClaim_case.nNoteDama
                .Columns("cbeReclaim").Checked = lclsClaim_case.sDemandant
                .Columns("tctClientCode").DefValue = lclsClaim_case.sClient
                .Columns("tctClientCode").Descript = lclsClaim_case.sCliename
                .Columns("tctClientCode").Digit = lclsClaim_case.sDigit
                .Columns("dDecladat").DefValue = CStr(mclsClaim.dDecladat)
                .Columns("dOccurdat").DefValue = CStr(mclsClaim.dOccurdat)
                .Columns("nHour").DefValue = vbNullString
                .Columns("dPrescdat").DefValue = CStr(mclsClaim.dPrescdat)
                .Columns("dLimit_pay").DefValue = CStr(mclsClaim.dLimit_pay)
                .Columns("nOffice_pay").DefValue = CStr(mclsClaim.nOffice_pay)
                .Columns("nClaimCause").DefValue = CStr(mclsClaim.nCausecod)
                .Columns("nClaimParent").DefValue = CStr(mclsClaim.nClaimParent)
                .Columns("nTotalLoss").DefValue = mclsClaim.sClaimtyp
                .Columns("nOffice_pay").DefValue = lintOffice_pay
                .Columns("nOfficeAgen_pay").DefValue = lintOfficeAgen_pay
                .Columns("nAgency_pay").DefValue = lintAgency_pay
                If lclsClaim_case.sFirstname <> vbNullString Then
                    .Columns("tctFirstName").DefValue = lclsClaim_case.sFirstname
                Else
                    .Columns("tctFirstName").DefValue = lclsClaim_case.sCliename
                End If
                .Columns("tctLastName").DefValue = lclsClaim_case.sLastname
                .Columns("tctLastName2").DefValue = lclsClaim_case.sLastName2
                .Columns("sClientInsured").DefValue = lclsClaim_case.sClient
                .Columns("nCase_num").DefValue = CStr(lclsClaim_case.nCase_num)
                .Columns("nDeman_type").Descript = lclsClaim_case.sDeman_type
                .Columns("nDeman_type").DefValue = CStr(lclsClaim_case.nDeman_type)
                .sQueryString = "nCase_num=" & lclsClaim_case.nCase_num
                .sQueryString = mobjGrid.sQueryString & "&nDeman_type=" & lclsClaim_case.nDeman_type

                .Columns("chkConti").Checked = CShort(lclsClaim_case.sConting)
                .Columns("chkConti").DefValue = lclsClaim_case.sConting
                .Columns("cbeRole").DefValue = CStr(lclsClaim_case.nBene_type)
                .Columns("cbeRole").Descript = lclsClaim_case.sBene_type
                .Columns("cbeRelaship").DefValue = CStr(lclsClaim_case.nRelation)
                .Columns("cbeRelaship").Descript = lclsClaim_case.sRelation
                .Columns("hdnId").DefValue = CStr(lclsClaim_case.nId)
                .Columns("btnPreexistencias").HRefScript = "insOpenPreexistencia(" & lintIndex & ")"

                If CStr(lclsClaim_case.nId) = "0" Then
                    .Columns("Sel").Disabled = True
                Else
                    .Columns("Sel").Disabled = False
                End If

                Response.Write(mobjGrid.DoRow())

                If lclsClaim_case.sBene_type = "Siniestrado" Then
                    Session("Siniestrado") = lclsClaim_case.sClient
                    Session("NombreSiniestrado") = lclsClaim_case.sCliename
                ElseIf lclsClaim_case.sBene_type = "Beneficiario" Then
                    Session("Beneficiario") = lclsClaim_case.sClient
                    Session("NombreBeneficiario") = lclsClaim_case.sCliename
                ElseIf lclsClaim_case.sBene_type = "Reclamante" Then
                    Session("Reclamante") = lclsClaim_case.sClient
                    Session("NombreReclamante") = lclsClaim_case.sCliename
                End If

            End With
            lintIndex = lintIndex + 1
        Next lclsClaim_case
        Response.Write(mobjValues.HiddenControl("hddnncountcol", lcolClaimCases.Count()))
        With Response
            .Write(mobjGrid.CloseTable())
            .Write(mobjValues.BeginPageButton)
        End With
        'UPGRADE_NOTE: Object lcolClaimCases may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolClaimCases = Nothing
        'UPGRADE_NOTE: Object mclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mclsClaim = Nothing
        'UPGRADE_NOTE: Object lclsClaim_caus may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim_caus = Nothing
    End Sub
    '% insPreSI004Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '----------------------------------------------------------------------------------------------
    Private Sub insPreSI004Upd()
        '----------------------------------------------------------------------------------------------
        Dim lclsPost As eClaim.Claim
        With Request
            Select Case .QueryString("Action")
                Case "Del"
                    lclsPost = New eClaim.Claim

                    'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                    If lclsPost.insPostSI004(mobjValues.StringToType(.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nId"), eFunctions.Values.eTypeData.etdDouble), .QueryString("cbeStatReserv"), mobjValues.StringToType(.QueryString("nNotes"), eFunctions.Values.eTypeData.etdDouble), vbNullString, .QueryString("tctClientCode"), vbNullString, .QueryString("cbeReclaim"), mobjValues.StringToType(.QueryString("cbeRole"), eFunctions.Values.eTypeData.etdDouble), Today, Today, Today, 0, 0, 0, 0, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), 0, vbNullString, vbNullString, vbNullString, "Delete", "PopUp") Then
                        Response.Write(mobjValues.ConfirmDelete())
                    End If
                    'UPGRADE_NOTE: Object lclsPost may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsPost = Nothing

                Case Else
                    mobjGrid.Columns("nTotalLoss").DefValue = .Form("hddTotalLoss")
                    mobjGrid.Columns("nOffice_pay").DefValue = .Form("hddOffice_pay")
                    mobjGrid.Columns("nOfficeAgen_pay").DefValue = .Form("hddOfficeAgen_pay")
                    mobjGrid.Columns("nAgency_pay").DefValue = .Form("hddAgency_pay")
            End Select
            Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaimSeq.aspx", "SI004", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))

            If .QueryString("Action") <> "Del" Then
                '+ Se genera el número de caso automáticamente 
                If .QueryString("Action") = "Add" Then
                    'Response.Write("<SCRIPT>ChangeValues('Casenum');</" & "Script>")
                    Response.Write("<SCRIPT>changecheck();</" & "Script>")
                    'Response.Write "<NOTSCRIPT>self.document.forms[0].btnNotenum.disabled = true</" & "Script>"  
                End If

                If .QueryString("Action") = "Update" Then
                    Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
                    Response.Write("<SCRIPT>InsShowPopUp(self.document.forms[0].chkInsured)</" & "Script>")
                End If

                Response.Write("" & vbCrLf)
                Response.Write("            <SCRIPT>" & vbCrLf)
                Response.Write("                with(self.document.forms[0]){" & vbCrLf)
                Response.Write("                    dDecladat.value        = top.opener.document.forms[0].elements[""gmdDeclaDat""].value" & vbCrLf)
                Response.Write("                    dOccurdat.value        = top.opener.document.forms[0].elements[""gmdOccurDat""].value" & vbCrLf)
                Response.Write("                    nHour.value            = top.opener.document.forms[0].elements[""gmnHour""].value" & vbCrLf)
                Response.Write("                    dPrescdat.value        = top.opener.document.forms[0].elements[""gmdPrescDat""].value" & vbCrLf)
                Response.Write("                    dLimit_pay.value       = top.opener.document.forms[0].elements[""gmdLimit_pay""].value" & vbCrLf)
                Response.Write("                    nClaimCause.value      = top.opener.document.forms[0].elements[""cbeClaimCaus""].value" & vbCrLf)
                Response.Write("                    nClaimParent.value     = top.opener.document.forms[0].elements[""cbeClaimParent""].value" & vbCrLf)
                Response.Write("                    nOffice_pay.value      = top.opener.document.forms[0].elements[""cbeOffice""].value" & vbCrLf)
                Response.Write("                    nTotalLoss.value       = top.opener.document.forms[0].elements[""hddTotalLoss""].value" & vbCrLf)
                Response.Write("                    nOffice_pay.value      = top.opener.document.forms[0].elements[""hddOffice_pay""].value" & vbCrLf)
                Response.Write("                    nOfficeAgen_pay.value  = top.opener.document.forms[0].elements[""hddOfficeAgen_pay""].value" & vbCrLf)
                Response.Write("                    nAgency_pay.value      = top.opener.document.forms[0].elements[""hddAgency_pay""].value                                                                                " & vbCrLf)
                Response.Write("                    if(typeof(top.opener.document.forms[0].cbeTypeCN)!='undefined'){" & vbCrLf)
                Response.Write("                        nTypeCN.value = top.opener.document.forms[0].cbeTypeCN.value" & vbCrLf)
                Response.Write("                        dBegin.value  = top.opener.document.forms[0].tcdBegin.value" & vbCrLf)
                Response.Write("                        dEnd.value    = top.opener.document.forms[0].tcdEnd.value" & vbCrLf)
                Response.Write("                    }" & vbCrLf)
                Response.Write("                }" & vbCrLf)
                Response.Write("            </" & "SCRIPT>")


            End If
        End With
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si004")

mclsClaim = New eClaim.Claim

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si004"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")

    Call mclsClaim.inspreSI004(Request.QueryString("sReload"), mobjValues.ActionQuery, CDbl(Session("nClaim")), mobjValues.StringToType(Request.QueryString("dDecladat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("dPrescdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("dOccurdat"), eFunctions.Values.eTypeData.etdDate), Request.QueryString("nHour"), mobjValues.StringToType(Request.QueryString("nClaimCause"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("nTotalLoss"), mobjValues.StringToType(Request.QueryString("nOffice_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nOfficeAgen_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nAgency_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("dLimit_pay"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("nClaimParent"), eFunctions.Values.eTypeData.etdDouble))
    Session("nServ_Order_GM") = mclsClaim.nServ_Order
%>
<HTML>
<HEAD>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>
<script LANGUAGE="JavaScript" src="/VTimeNet/Scripts/json2.js" type="text/javascript"></script>

    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI004", Request.QueryString("sWindowDescript")))
	If Request.QueryString("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "SI004", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
%>
<SCRIPT>

//ShowClientSequence: 
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ShowClientSequence(){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    var nMainAction = ($("[name='tctFirstName']").attr("disabled")?302:301);
	var sClientCode = $("[name='tctClientCode']").val();;
	var sDigit=$("[name='tctClientCode_Digit']").val();
	var nPerson_typ="1";
    var sFirstName=encodeURIComponent($("[name='tctFirstName']").val());
    var sLastName=encodeURIComponent($("[name='tctLastName']").val());
    var sLastName2=encodeURIComponent($("[name='tctLastName2']").val());

    if (sClientCode==""){
        alert("Ingrese primero el RUT del cliente");
        return;
    }
	$("[name='tctClientCode_Old']").val("");
	
	ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sCodispl=BC003_K&sModule=Client&sProject=ClientSeq&sRoleCode=&LinkParamsClient='+sClientCode+'&sClientCode='+sClientCode+'&nMainAction='+nMainAction+'&LinkSpecialAction='+nMainAction+'&sDigit='+sDigit+'&LinkParamsDigit='+sDigit+'&nPerson_typ='+nPerson_typ+'&sOriginalForm=&sLinkSpecial=1&LinkParamsClientControl=tctClientNavigation&sFirstName='+sFirstName+'&sLastName='+sLastName+'&sLastName2='+sLastName2, 'ClientSeq', 750, 500, 'no', 'yes', 20, 20)
}

//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    var lblnValid = true;
    var nDeman_type = 1;
	var llngMax = 0;
	var strParams; 
    switch(Option){
        case "Client":
            with(self.document.forms[0]){
                if(tctClientCode.value!="")
                    insDefValues('Client','sClient=' + tctClientCode.value,'/VTimeNet/Claim/ClaimSeq')
                else{
                    tctLastName.value="";
                    tctLastName2.value="";
                    tctFirstName.value="";
                }
            }    
            break;        

        case "ClaimCaus":
            if(Field.value!="")
                insDefValues('ClaimCaus','nClaimCaus=' + Field.value,'/VTimeNet/Claim/ClaimSeq')
            break;        

		case "FindChildren":
			if (Field.checked)
			{
    			strParams = "nLength=" + marrArray.length + 
    						"&nIndex=" + Field.value + 
    						"&nCase_num=" + marrArray[Field.value].tcnCaseNum + 
    						"&nDeman_type=" + marrArray[Field.value].cboRtype + 
    						"&sClient=" + marrArray[Field.value].tctClientCode
				insDefValues('FindChildren', strParams,'/VTimeNet/claim/claimseq');
			}
			break;

        case "Occurdat":
            with(self.document.forms[0]){
                if(Field.value!="")
                    if(gmdPrescDat.value=="")
                        insDefValues('DefaultPrescDate','dOccurdat=' + Field.value,'/VTimeNet/Claim/ClaimSeq') 
            }      
            break; 

		case "Casenum":
			with(self.document.forms[0]){
//+ Se genera el número consecutivo del caso (el Nº consecutivo más alto +1)   
				for(var llngIndex = 0;llngIndex<top.opener.marrArray.length;llngIndex++){
					if(top.opener.marrArray[llngIndex].tcnCaseNum==tcnCaseNum.value){
						lblnValid = false;
						nDeman_type.value = top.opener.marrArray[llngIndex].cboRtype;
						cboRtype.value = nDeman_type.value;
					}
					else
						if(top.opener.marrArray[llngIndex].tcnCaseNum>llngMax)
						    llngMax = top.opener.marrArray[llngIndex].tcnCaseNum
				}
			
				if(typeof(Field)=='undefined'){
					tcnCaseNum.value = ++llngMax;
					nCase_num.value = self.document.forms[0].tcnCaseNum.value;
					nDeman_type.value = self.document.forms[0].cboRtype.value;
				}
    			else{
    			    if(Field.value==""){
						tcnCaseNum.value = ++llngMax;
						nCase_num.value = ++llngMax;
				    }
				    else{
//						cbeReclaim.checked=false;
						nCase_num.value = Field.value;
						cboRtype.disabled = !lblnValid
					}
				}
				break;
			}
                    
        case "TotalLoss":
			with(self.document.forms[0]){
				hddTotalLoss.value = (chkTotalLoss.checked)?'2':'1';
			}
    }
}

//insFormatTime: Da formato a la hora introducida por el usuario    
//--------------------------------------------------------------------------------------------
function insFormatTime(Field){
//--------------------------------------------------------------------------------------------
    var lstrTime="";
    var lstrTimeAUX="";
    var lstrString="";

//+ Se pregunta por el valor mayor a 2400 que equivale a que el usuario halla introducido
//+ 24:00, si es mayor a este valor se blanquea el campo y se sale de la función - ACM - 21/05/2001
    if(Field>2359){
        self.document.forms[0].elements["gmnHour"].value = "00:00";
        return(0);
    }

    if(Field.length==2 && 
       Field>23){
        self.document.forms[0].elements["gmnHour"].value = "00:00";
        return(0);
    }    

    lstrTime = Field;
//+ Si la longitud de la hora es menor a 4 dígitos, se toma el primer dígito y se le suman 12
//+ para obtener la hora en formato militar, luego se le añaden los 2 puntos (:) entre las
//+ horas y los minutos - ACM - 21/05/2001
    if(lstrTime.length<4){
//+ Si la longitud del valor introducido es 1, 2 ó 3 se llena con ceros a la derecha para
//+ obtener la longitud ideal y luego formatear la hora en formato militar (24 horas) - ACM - 21/05/2001
        if(lstrTime.length<4 && lstrTime.length==1)
            lstrTime = "0" + lstrTime + "00";

        if(lstrTime.length<4 && lstrTime.length==2)
            lstrTime = lstrTime + "00";

        if(lstrTime.length<4 && lstrTime.length==3){
            if(parseInt(lstrTime.substr(1, 2))>59)
                lstrTime ="0000"            
            else if(parseInt(lstrTime.substr(2, 2))>59)
                lstrTime ="0000"
            else
                lstrTime = "0" + lstrTime;            
        }

//+ Se extrae el valor del primer dígito del valor del campo y se verifica que éste no sea
//+ ni 1 ni cero para concatenarle un cero a la izquierda - ACM - 21/05/2001
        
        lstrTimeAUX = lstrTime.substr(0, 1);
        lstrTimeAUX = lstrTime.substr(0, 2);
        lstrString = lstrTimeAUX + ":" + lstrTime.substr(2, lstrTime.length);
        
    }
//+ Si la longitud del campo es igual a 4, se procede a tomar las 2 primera posiciones, concatenar
//+ los 2 puntos (:) y luego se concatenan los valores restantes - ACM - 21/05/2001
    else{
        if(lstrTime.length==4){
            lstrTimeAUX = lstrTime.substr(0, 2);
            lstrString = lstrTimeAUX + ":" + lstrTime.substr(2, lstrTime.length);
            if(parseInt(lstrTime.substr(2, 2))>59){
                self.document.forms[0].elements["gmnHour"].value = "00:00";
                lstrString="";
            }
        } 
        else{
            if(lstrTime.length==5){
                lstrTimeAUX = lstrTime.substr(0, 2);
                lstrString = lstrTimeAUX + ":" + lstrTime.substr(3, lstrTime.length);
                if(parseInt(lstrTime.substr(3, 2))>59){
                    self.document.forms[0].elements["gmnHour"].value = "00:00";
                    lstrString="";
                }            
            }
        }
    }
    if(lstrString!="")
        self.document.forms[0].elements["gmnHour"].value = lstrString;
        
    //+ INCIDENCIA 6636 : Se verifica si la hora está vacía o es : ... si es asi  equivale a 00:00
    if( (lstrString == "") || (lstrString == ":") )
        self.document.forms[0].elements["gmnHour"].value = "00:00";

}

//%DisabledRelaship: Inhabilita el campo "Parentesco" si la figura incluída es "Asegurado"
//-------------------------------------------------------------------------------------------
function DisabledRelaship(){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if(cbeRole.value==2){
            cbeRelaship.value='';
            cbeRelaship.disabled=true;
        }
        else
            cbeRelaship.disabled=false;
    }
}

//% InsShowPopUp: Llama a las ventanas de Control de cliente/Asegurados de la póliza segun sea el caso
//----------------------------------------------------------------------------------------------------
function InsShowPopUp(Field){
//----------------------------------------------------------------------------------------------------
    if (Field.checked)
        mintTypeForm=2;
    else
        mintTypeForm=1;
}
//--------------------------------------------------------------------------------    
function insOpenPreexistencia(lintIndex){
//--------------------------------------------------------------------------------    
//- Cadena con direccion
    var lstrQueryString;
//- Codigo de la accion
    var lstrAction;

    lstrAction = "<%=Request.QueryString("Action")%>";
<%If Request.QueryString("Type") = "PopUp" Then%>
          lstrQueryString = "/VTimeNet/Common/PreexistSI004.aspx?sCertype=2&sCodispl=<%=Request.QueryString("sCodispl")%>&Type&Action=" + lstrAction + "&sInsured=" + document.forms[0].tctClientCode.value ;
<%Else%>
          lstrQueryString = "/VTimeNet/Common/PreexistSI004.aspx?sCertype=2&sCodispl=<%=Request.QueryString("sCodispl")%>&Type=Qry&Action=" + lstrAction + "&sInsured=" + marrArray[lintIndex].tctClientCode ;
<%End If%>        
     ShowPopUp(lstrQueryString,"Values", 425,400,"no","no", 100, 100);
}

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 31-10-10 4:10 $|$$Author: Ljimenez $"

function changecheck(){
    if (top.opener.document.forms[0].elements["hddnncountcol"].value==0){
        self.document.forms[0].tcnCaseNum.value = 1;
        //self.document.forms[0].cbeReclaim.checked= true;
        //self.document.forms[0].cbeReclaim.disabled= true;
    }
    else{
        self.document.forms[0].tcnCaseNum.value = 1;
        //self.document.forms[0].cbeReclaim.checked= false;
        //self.document.forms[0].cbeReclaim.disabled= true;
    }
}

$(function() {
	if (qs("Type")=="PopUp"){
		$("[name=tctClientNavigation]").parent().parent().toggle();
	}
});


//% insClaimChanged: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insRoleChanged()
//------------------------------------------------------------------------------------------
{
    $.get("/vtimenet/ajax/clialloclabypk.aspx?nbranch=<%=Session("nBranch")%>&nProduct=<%=Session("nProduct")%>&nRole=" + $("[name=cbeRole]").val(),
            function (data) {
                var answer = JSON.parse(data);
                $("[name=cbeReclaim]").prop("checked", answer.SDEFAULT_CLA_IND=="1");
            }
    );
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSI004" ACTION="ValClaimSeq.aspx?sMode=1">
<%If Request.QueryString("Type") <> "PopUp" Then%>    
    <P ALIGN="Center">
        <LABEL ID=40301><A HREF="#Casos">Casos</A></LABEL>
	</P>
<%End If
Response.Write(mobjValues.ShowWindowsName("SI004", Request.QueryString("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI004Upd()
Else
	Call insPreSI004()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("si004")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




