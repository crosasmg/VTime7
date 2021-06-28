<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eProduct" %>
<%@ Import Namespace="eClaim" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Se define la variable mobjGrid para el manejo del Grid de la ventana
    Dim mobjGrid As eFunctions.Grid

    '- Objeto para el manejo de las zonas de la página
    Dim mobjMenu As eFunctions.Menues
    Dim clsProduct_li As eProduct.Product


    '%insDefineHeader: Se definen las columnas del grid
    '------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        clsProduct_li = New eProduct.Product
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
	
        mobjGrid.sCodisplPage = "si749"
        Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            'Call .AddPossiblesColumn(0, "Compañía", "valCompany","tabcompany",eFunctions.Values.eValuesType.clngWindowType,,,,,,,False,4,"Compañía reaseguradora participante", eFunctions.Values.eTypeCode.eNumeric)
            Call .AddTextColumn(0, "Compañía", "tctCompany", 30, " ", , "Compañía participante en el contrato.", , , , True)
            Call .AddTextColumn(0, "Tipo de Contrato", "valType_rein", 30, " ", , "Compañía participante en el contrato.", , , , True)
            Call .AddNumericColumn(0, "Reserva", "tcnReserve", 18, "0", False, "Monto de reserva con el que participa la compañia reaseguradora", True, 6, , , , True)
            Call .AddNumericColumn(0, "Reserva pendiente", "tcnReserve_Pend", 18, "0", False, "Monto de reserva pendiente con el que participa la compañia reaseguradora", True, 6, , , , True)
            Call .AddNumericColumn(0, "Pagado", "tcnAmount_Pay", 18, "0", False, "Monto pagado con el que participa la compañia reaseguradora", True, 6, , , , True)
            Call .AddNumericColumn(0, "Recuperado", "tcnAmount_Recu", 18, "0", False, "Monto de recuperación con el que participa la compañia reaseguradora", True, 6, , , , True)
            Call .AddNumericColumn(0, "Gastos de recuperación", "tcnAmount_C_Recu", 18, "0", False, "Monto de gastos de recuperación con el que participa la compañia reaseguradora", True, 6, , , , True)
            Call .AddNumericColumn(0, "% Participación", "tcnShare", 9, "0", False, "Porcentaje del siniestro con el que participa la compañía reaseguradora", True, 6, , , "CalculateAmount(this.value)", False)
		
            Call .AddHiddenColumn("hddSel", "")
            Call .AddHiddenColumn("hddReser", "0")
            Call .AddHiddenColumn("hddReserPend", "0")
            Call .AddHiddenColumn("hddAmountPay", "0")
            Call .AddHiddenColumn("hddAmountR", "0")
            Call .AddHiddenColumn("hddAmount_C_R", "0")
            Call .AddHiddenColumn("hddShare", "0")
            Call .AddHiddenColumn("hddClient", Request.QueryString("sClient"))
            Call .AddHiddenColumn("hddModulec", Request.QueryString("nModulec"))
            Call .AddHiddenColumn("hddBranch_Rei", Request.QueryString("nBranch_rei"))
            Call .AddHiddenColumn("hddType_Rein", Request.QueryString("nType_Rein"))
            Call .AddHiddenColumn("hddAccedate", "")
            Call .AddHiddenColumn("hddCompany", "")
            Call .AddHiddenColumn("hddCapital", "0")
            Call .AddHiddenColumn("hddCommissi", "0")
            Call .AddHiddenColumn("hddCurrency", Request.QueryString("nCurrency"))
            Call .AddHiddenColumn("hddHeap_code", "")
            Call .AddHiddenColumn("hddInter_Rate", "0")
            Call .AddHiddenColumn("hddNumber", "")
            Call .AddHiddenColumn("hddReser_Rate", "0")
            Call .AddHiddenColumn("hddChange", "")
            Call .AddHiddenColumn("hddAcep_Code", "")
            Call .AddHiddenColumn("hddShareTotal", "0")
		
        End With
	
        With mobjGrid
            .nMainAction = Request.QueryString("nMainAction")
            .Codispl = "SI749"
            .Codisp = "SI749"
            .Top = 100
            .Height = 400
            .Width = 400
            .AddButton = False
            .DeleteButton = False
		
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery
            .Columns("Sel").GridVisible = Not .ActionQuery
            Call clsProduct_li.FindProduct_li(CInt(Session("nbranch")), CInt(Session("nproduct")), CDate(Session("deffecdate")))
            If CDbl(Session("nPay_Type")) = 4 Or clsProduct_li.nProdClas = 2 Then
                '.Columns("valCompany").EditRecord = True
                .Columns("tctCompany").EditRecord = True
            Else
                '.Columns("valCompany").EditRecord = False
                .Columns("tctCompany").EditRecord = False
            End If
		
            .sDelRecordParam = "nCompany='+ marrArray[lintIndex].hddCompany + '&nCover=' + self.document.forms[0].valCover.value + '&nBranch_Rei=' + marrArray[lintIndex].hddBranch_Rei + '&nModulec=' + marrArray[lintIndex].hddModulec + '&nType_Rein=' + marrArray[lintIndex].hddType_Rein + '&sClient=' + marrArray[lintIndex].hddClient + '"
            .sEditRecordParam = "nCover='+ document.forms[0].valCover.value + '&nModulec=' + document.forms[0].valCover_nModulec.value + '&sClient=' + document.forms[0].valCover_sClient.value + '&nCurrency=' + document.forms[0].valCover_nCurrency.value + '&nBranch_rei=' + '' + '&nType_Rein='+ '' + '"
            If Request.QueryString("Reload") = "1" Then
                .sReloadIndex = Request.QueryString("ReloadIndex")
            End If
        End With
    End Sub

    '%insPreSI749. Se crea la ventana madre (Principal)
    '------------------------------------------------------------------------------
    Private Sub insPreSI749()
        '------------------------------------------------------------------------------
        Dim lcolcl_Reinsurans As eClaim.cl_Reinsurans
        Dim lclsCl_Reinsuran As eClaim.cl_Reinsuran
        Dim nAssig As Byte
        Dim lintIndex As Short
        Dim lintCover As Integer
        Dim lintModule As Integer
        Dim lstrClient As String
        Dim lintCurrency As Integer
	
	
        lclsCl_Reinsuran = New eClaim.cl_Reinsuran
        lcolcl_Reinsurans = New eClaim.cl_Reinsurans
	
        Call lclsCl_Reinsuran.Find_Cl_Reinsuran_Cover(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), CDate(Session("dEffecdate")))
	
	
        Response.Write("" & vbCrLf)
        Response.Write("	<BR>" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR> " & vbCrLf)
        Response.Write("            <TD><LABEL ID=0> Coberturas</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

	
        With mobjValues.Parameters
            .Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nCase_num", Session("nCase_num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nDeman_type", Session("nDeman_type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .ReturnValue("nModulec", False, "", True)
            .ReturnValue("nCurrency", False, "", True)
            .ReturnValue("sClient", False, "", True)
        End With
	
        If Request.QueryString("nCover") <> vbNullString Then
            Response.Write(mobjValues.PossiblesValues("valCover", "tabCl_cover", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString("nCover"), True, , , , , "insSubmitPage();", False, , "Coberturas afectadas del siniestro"))
        Else
            Response.Write(mobjValues.PossiblesValues("valCover", "tabCl_cover", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsCl_Reinsuran.nCover), True, , , , , "insSubmitPage();", False, , "Coberturas afectadas del siniestro"))
        End If
	
        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR> " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0> Reserva</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnReserve", 18, CStr(0), True, "Monto de la reserva de la cobertura en tratamiento", True, 6, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0> Reserva pendiente</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnReservePend", 18, CStr(0), True, "Monto de la reserva pendiente de la cobertura en tratamiento", True, 6, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0> Pagado</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnPayAmount", 18, CStr(0), True, "Monto pagado de la cobertura en tratamiento", True, 6, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0> Recuperado</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnRecuAmount", 18, CStr(0), True, "Monto recuperado de la cobertura en tratamiento", True, 6, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("    </TABLE>" & vbCrLf)
        Response.Write("    ")

	
        If Request.QueryString("sClient") > "" Then
            lstrClient = Request.QueryString("sClient")
        Else
            lstrClient = lclsCl_Reinsuran.sClient
        End If
	
        If Request.QueryString("nCurrency") > "" Then
            lintCurrency = Request.QueryString("nCurrency")
        Else
            lintCurrency = lclsCl_Reinsuran.nCurrency
        End If
	
        If Request.QueryString("nCover") > "" Then
            lintCover = Request.QueryString("nCover")
        Else
            lintCover = lclsCl_Reinsuran.nCover
        End If
	
        If Request.QueryString("nModulec") > "" Then
            lintModule = Request.QueryString("nModulec")
        Else
            lintModule = lclsCl_Reinsuran.nModulec
        End If
	
	
        'UPGRADE_NOTE: Object lclsCl_Reinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsCl_Reinsuran = Nothing
	
        If lintCover > 0 Then
            If lcolcl_Reinsurans.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), lintModule, lintCover, lstrClient, lintCurrency, mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble)) Then
                nAssig = 1
                mobjGrid.Columns("Sel").OnClick = "insUpdateSelection(this)"
			
                For Each lclsCl_Reinsuran In lcolcl_Reinsurans
                    With mobjGrid
                        lintIndex = 0
                        If nAssig = 1 Then
                            .Columns("hddReser").DefValue = CStr(lclsCl_Reinsuran.nLoc_reserv)
                            .Columns("hddReserPend").DefValue = CStr(lclsCl_Reinsuran.nReserv_Pend)
                            .Columns("hddAmountPay").DefValue = CStr(lclsCl_Reinsuran.nPay_amount)
                            .Columns("hddAmountR").DefValue = CStr(lclsCl_Reinsuran.nPay_amount)
                            .Columns("hddAmount_C_R").DefValue = CStr(lclsCl_Reinsuran.nLoc_rec_am)
						
                            Response.Write("<script>with(top.frames['fraFolder'].document.forms[0]){" & "          tcnReserve.value='" & FormatNumber(lclsCl_Reinsuran.nLoc_reserv, 6) & "';" & "          tcnReservePend.value='" & FormatNumber(lclsCl_Reinsuran.nReserv_Pend, 6) & "';" & "          tcnPayAmount.value='" & FormatNumber(lclsCl_Reinsuran.nPay_amount, 6) & "';" & "          tcnRecuAmount.value='" & FormatNumber(lclsCl_Reinsuran.nLoc_rec_am, 6) & "'};</" & "Script>")
                            nAssig = 0
                        End If
                        '	    			.Columns("valCompany").DefValue	      = lclscl_Reinsuran.nCompany
                        .Columns("tctCompany").DefValue = lclsCl_Reinsuran.sCompany
                        .Columns("valType_rein").DefValue = lclsCl_Reinsuran.sDesType_Rein
                        .Columns("tcnReserve").DefValue = CStr(lclsCl_Reinsuran.nloc_Reserv_p)
                        .Columns("tcnReserve_Pend").DefValue = CStr(lclsCl_Reinsuran.nReserv_pend_p)
                        .Columns("tcnAmount_Pay").DefValue = CStr(lclsCl_Reinsuran.nPay_amount_p)
                        .Columns("tcnAmount_Recu").DefValue = CStr(lclsCl_Reinsuran.nLoc_rec_am_p)
                        .Columns("tcnAmount_C_Recu").DefValue = CStr(lclsCl_Reinsuran.nLoc_cos_re_p)
                        .Columns("tcnShare").DefValue = CStr(lclsCl_Reinsuran.nShare)
                        .Columns("hddShare").DefValue = CStr(lclsCl_Reinsuran.nShare)
                        .Columns("hddClient").DefValue = lclsCl_Reinsuran.sClient
                        .Columns("hddModulec").DefValue = CStr(lclsCl_Reinsuran.nModulec)
                        .Columns("hddBranch_Rei").DefValue = CStr(lclsCl_Reinsuran.nBranch_Rei)
                        .Columns("hddType_Rein").DefValue = CStr(lclsCl_Reinsuran.nType_Rein)
                        .Columns("hddAccedate").DefValue = CStr(lclsCl_Reinsuran.dAcceDate)
                        .Columns("hddCompany").DefValue = CStr(lclsCl_Reinsuran.nCompany)
                        .Columns("hddCapital").DefValue = CStr(lclsCl_Reinsuran.nCapital)
                        .Columns("hddCommissi").DefValue = CStr(lclsCl_Reinsuran.nCommissi)
                        .Columns("hddCurrency").DefValue = CStr(lclsCl_Reinsuran.nCurrency)
                        .Columns("hddHeap_code").DefValue = lclsCl_Reinsuran.sHeap_code
                        .Columns("hddInter_Rate").DefValue = CStr(lclsCl_Reinsuran.nInter_rate)
                        .Columns("hddNumber").DefValue = CStr(lclsCl_Reinsuran.nNumber)
                        .Columns("hddReser_Rate").DefValue = CStr(lclsCl_Reinsuran.nReser_rate)
                        .Columns("hddChange").DefValue = CStr(lclsCl_Reinsuran.nChange)
                        .Columns("hddAcep_Code").DefValue = CStr(lclsCl_Reinsuran.nAcep_code)
					
                        .Columns("hddSel").DefValue = lclsCl_Reinsuran.sSel
                        .Columns("Sel").DefValue = lclsCl_Reinsuran.sSel
					
                        If lclsCl_Reinsuran.sSel = "1" Then
                            .Columns("Sel").Checked = CShort("1")
                        Else
                            .Columns("Sel").Checked = CShort("2")
                        End If
					
                        .sEditRecordParam = "nCover=" & lintCover & "&nModulec=" & lintModule & "&sClient=" & lstrClient & "&nCurrency=" & lintCurrency & "&nBranch_rei=" & lclsCl_Reinsuran.nBranch_Rei & "&nType_Rein=" & lclsCl_Reinsuran.nType_Rein
                        lintIndex = lintIndex + 1
					
                        Response.Write(.DoRow)
                    End With
                Next lclsCl_Reinsuran
            End If
        End If
        'UPGRADE_NOTE: Object lclsCl_Reinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsCl_Reinsuran = Nothing
        Response.Write(mobjGrid.closeTable())
    End Sub

    '% insPreSI749Upd. Se define esta funcion para contruir el contenido de la 
    '%                     ventana UPD de los archivos de datos particulares
    '------------------------------------------------------------------------------
    Private Sub insPreSI749Upd()
        '------------------------------------------------------------------------------
        Dim lclsCl_Reinsuran As eClaim.cl_Reinsuran
	
        If Request.QueryString("Action") = "Del" Or Request.QueryString("Action") = "Delete" Then
		
            lclsCl_Reinsuran = New eClaim.cl_Reinsuran
            Response.Write(mobjValues.ConfirmDelete())
            Call lclsCl_Reinsuran.InsPostSI749(Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("nCompany"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(Request.QueryString("nBranch_Rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nType_Rein"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
        End If
	
        Response.Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valPaySeq.aspx", "SI749", Request.QueryString("nMainAction"), mobjGrid.ActionQuery, Request.QueryString("Index")))
	
        'UPGRADE_NOTE: Object lclsCl_Reinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsCl_Reinsuran = Nothing
	
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si749")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si749"
%>
<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<html>
<head>
    <meta name="GENERATOR" content="eTransaction Designer for Visual TIME">
    <%
        mobjValues.ActionQuery = (Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery)
        With Response
            .Write(mobjValues.StyleSheet())
            .Write("<script>var	nMainAction	= " & CShort("0" & Request.QueryString("nMainAction")) & "</script>")
            If Request.QueryString("Type") <> "PopUp" Then
                mobjMenu = New eFunctions.Menues
                '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
                mobjMenu.sSessionID = Session.SessionID
                mobjMenu.nUsercode = Session("nUsercode")
                '~End Body Block VisualTimer Utility
                .Write(mobjMenu.setZone(2, "SI749", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
            End If
        End With
    %>
    <script>
        //%	insSubmitPage: recarga la página para mostrar la información del grid
        //-------------------------------------------------------------------------------------------
        function insSubmitPage() {
            //-------------------------------------------------------------------------------------------
            var lstrAction;
            var lstraux;
            var frm = self.document.forms[0];

            lstrAction = self.document.location.href;
            lstraux = lstrAction;
            lstrAction = lstrAction.replace(/\?.*/, '') +
        '?sCodispl=SI749' +
        '&nModulec=' + frm.valCover_nModulec.value +
        '&nCover=' + frm.valCover.value +
        '&sClient=' + frm.valCover_sClient.value +
        '&nCurrency=' + frm.valCover_nCurrency.value +
        '&nMainAction=' + lstraux.replace(/http.*\&nMainAction=/, '');
            self.document.location.href = lstrAction;
        }


        //% CalculateAmount: Calcula el monto de la participación del reaseguro
        //----------------------------------------------------------------------------------------
        function CalculateAmount(nPercentage) {
            //----------------------------------------------------------------------------------------
            var frm = self.document.forms[0];

            if (nPercentage != "0" && nPercentage != "") {
                frm.tcnReserve.value = parseFloat((parseFloat(frm.hddReser.value) * parseFloat(nPercentage)) / 100);
                frm.tcnReserve_Pend.value = parseFloat((parseFloat(frm.hddReserPend.value) * parseFloat(nPercentage)) / 100);
                frm.tcnAmount_Pay.value = parseFloat((parseFloat(frm.hddAmountPay.value) * parseFloat(nPercentage)) / 100);

                frm.tcnAmount_Recu.value = parseFloat((parseFloat(frm.hddAmountR.value) * parseFloat(nPercentage)) / 100);
                frm.tcnAmount_C_Recu.value = parseFloat((parseFloat(frm.hddAmount_C_R.value) * parseFloat(nPercentage)) / 100);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        function insUpdateSelection(lobj) {
            //---------------------------------------------------------------------------------------------------------
            if (mintArrayCount > 0) {
                if (lobj.checked == false) {
                    self.document.forms[0].hddSel[lobj.value].value = "0";
                } else {
                    self.document.forms[0].hddSel[lobj.value].value = "1";
                }
            } else {
                if (lobj.checked == false) {
                    self.document.forms[0].hddSel.value = "0";
                } else {
                    self.document.forms[0].hddSel.value = "1";
                }
            }
        }

    </script>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="frmSI749" action="valPaySeq.aspx?sMode=2&nMainAction=<%=Request.QueryString("nMainAction")%>">
    <%
        Response.Write(mobjValues.ShowWindowsName("SI749", Request.QueryString("sWindowDescript")))

        Call insDefineHeader()
        If Request.QueryString("Type") <> "PopUp" Then
            Call insPreSI749()
        Else
            Call insPreSI749Upd()
        End If
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
        'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjGrid = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Call mobjNetFrameWork.FinishPage("si749")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
