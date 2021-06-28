<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenu As eFunctions.Menues


    '% insDefineHeader: Se definen los campos del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
		
            Call .AddNumericColumn(0, "Recibo", "tcnReceipt", 10, CStr(0))
            Call mobjGrid.Columns.AddNumericColumn(0, "Contrato", "tcnContrat", 10, CStr(0), , "Contrato de financiamiento", False, , , , , True)
            Call mobjGrid.Columns.AddNumericColumn(0, "Cuota", "tcnDraft", 5, CStr(0), , "Cuota o giro del contrato de financiamiento", False, , , , , True)
            Call .AddPossiblesColumn(0, "Moneda", "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType)
            Call .AddNumericColumn(0, "Prima", "tcnBalance", 18, CStr(0), , "Importe de la prima pendiente", True, 6, , , , True)
		
            Call .AddHiddenColumn("hddSel", "")
            Call .AddHiddenColumn("hddReceipt", "")
            Call .AddHiddenColumn("hddBalance", "")
            Call .AddHiddenColumn("hddContrat", "")
            Call .AddHiddenColumn("hddDraft", "")
            Call .AddHiddenColumn("hddEffecdate", "")
		
		
		
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            '.Columns("Sel").OnClick = "insUpdateSelection(this,marrArray[this.value].hddEffecdate,marrArray[this.value].hddBalance)"
		
            .Codispl = "SI762"
            .DeleteButton = False
            .AddButton = False
            .Columns("Sel").GridVisible = True
        End With
    End Sub

    '% insPreSI762: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreSI762()
        '--------------------------------------------------------------------------------------------
        Dim ldblAmount As Object
        Dim lintIndex As Short
        Dim lclsT_ConcilClaim As eClaim.T_ConcilClaim
        Dim lcolT_ConcilClaims As eClaim.T_ConcilClaims
	
        lintIndex = 0
        With Server
            lclsT_ConcilClaim = New eClaim.T_ConcilClaim
            lcolT_ConcilClaims = New eClaim.T_ConcilClaims
        End With
	
        If CStr(Session("nCurrPaySI008")) <> "" Then
            If lcolT_ConcilClaims.Find(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), CDate(Session("dPayDate")), CInt(Session("nCurrPaySI008")), CInt(Session("nUserCode"))) Then
                ldblAmount = mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble)
                For Each lclsT_ConcilClaim In lcolT_ConcilClaims
                    With mobjGrid
                        .Columns("Sel").OnClick = "insUpdateSelection(this,marrArray[this.value].hddEffecdate,marrArray[this.value].hddBalance)"
                        .Columns("tcnReceipt").DefValue = CStr(lclsT_ConcilClaim.nReceipt)
                        .Columns("tcnContrat").DefValue = CStr(lclsT_ConcilClaim.nContrat)
                        .Columns("tcnDraft").DefValue = CStr(lclsT_ConcilClaim.nDraft)
                        .Columns("cbeCurrency").DefValue = CStr(lclsT_ConcilClaim.nCurrency)
                        .Columns("tcnBalance").DefValue = CStr(lclsT_ConcilClaim.nBalance)
                        .Columns("hddReceipt").DefValue = CStr(lclsT_ConcilClaim.nReceipt)
                        .Columns("hddBalance").DefValue = CStr(lclsT_ConcilClaim.nBalance)
                        .Columns("hddContrat").DefValue = CStr(lclsT_ConcilClaim.nContrat)
                        .Columns("hddDraft").DefValue = CStr(lclsT_ConcilClaim.nDraft)
                        .Columns("hddEffecdate").DefValue = CStr(lclsT_ConcilClaim.dEffecdate)
					
                        '+ Si el tipo de siniestro es perdida total se marcan todos los registros y se inhabilita la columna sel
                        '	                If lclsT_ConcilClaim.sClaimTyp = "1" Then
                        '	                   .Columns("Sel").Checked  = 1
                        '	                   .Columns("Sel").Disabled = True
                        '	                   .Columns("hddSel").DefValue = "1"
                        '	                   .Columns("Sel").DefValue    = "1"
                        '	                   lclsT_ConcilClaim.sMark = "1"
                        '	                End If
                        '
                        If lclsT_ConcilClaim.sMark = "1" Then
                            .Columns("Sel").Checked = 1
                           ' .Columns("Sel").Disabled = True
                            .Columns("hddSel").DefValue = "1"
                            .Columns("Sel").DefValue = "1"
                            ldblAmount = mobjValues.StringToType(ldblAmount, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(CStr(lclsT_ConcilClaim.nBalance), eFunctions.Values.eTypeData.etdDouble)
                        Else
                            .Columns("Sel").Checked = 2
                            ' .Columns("Sel").Disabled = False
                            .Columns("hddSel").DefValue = "0"
                            .Columns("Sel").DefValue = "0"
                        End If
					
                        Response.Write(.DoRow)
                    End With
                    lintIndex = lintIndex + 1
                Next lclsT_ConcilClaim
            End If
            Response.Write(mobjValues.HiddenControl("hddBalanceTotal", ldblAmount))
            Session("SI008_Premium") = ldblAmount
            Response.Write("<script>mintAmountTotal=insConvertNumber('" & ldblAmount & "') ;</" & "Script>")
        End If
        Response.Write(mobjGrid.closeTable())
	
        Response.Write("" & vbCrLf)
        Response.Write("  <TABLE  align=""right"">" & vbCrLf)
        Response.Write("	<TR >" & vbCrLf)
        Response.Write("		<TD><LABEL ID=0 >Total prima a descontar: </LABEL></TD>" & vbCrLf)
        Response.Write("		<TD>")


        Response.Write(mobjValues.NumericControl("tcnTotal", 18, ldblAmount, , "Importe de prima a descontar", True, 6, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("	</TR>" & vbCrLf)
        Response.Write("	</TABLE>")

	
        'UPGRADE_NOTE: Object lclsT_ConcilClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsT_ConcilClaim = Nothing
        'UPGRADE_NOTE: Object lcolT_ConcilClaims may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolT_ConcilClaims = Nothing
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si762")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si762"
    mobjGrid = New eFunctions.Grid
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjGrid.sSessionID = Session.SessionID
    mobjGrid.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjGrid.sCodisplPage = "si762"
    Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
%>
<html>
<head>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/Constantes.js"></script>
    <script>
        var mintAmountTotal = 0;

        //---------------------------------------------------------------------------------------------------------
        function insUpdateSelection(lobj, dEffecdate, nBalance) {
            //---------------------------------------------------------------------------------------------------------
            var nIndicErr = 0;


            if (mintArrayCount > 0) {
                if (lobj.checked == false) {
                    self.document.forms[0].hddSel[lobj.value].value = "0";
                    marrArray[lobj.value].Sel = false;
                }
                else {
                    self.document.forms[0].hddSel[lobj.value].value = "1";
                    marrArray[lobj.value].Sel = true;
                }
            }
            else {
                if (lobj.checked == false) {
                    self.document.forms[0].hddSel.value = "0";
                    marrArray[lobj.value].Sel = false;
                }
                else {
                    self.document.forms[0].hddSel.value = "1";
                    marrArray[lobj.value].Sel = true;
                }
            }
            //Se Valida que el recibo/cuota seleccionado sea el mas antigua

            if (lobj.checked) {
                for (var i = 0; i < marrArray.length; i++) {
                    if ((GetDateYYYYMMDD(marrArray[i].hddEffecdate) < GetDateYYYYMMDD(dEffecdate)) && (!marrArray[i].Sel))
                        nIndicErr = 1;
                }
                if (nIndicErr == 1) {
                    alert('Existen recibos/cuotas mas antiguas no seleccionadas');
                    //lobj.checked = !lobj.checked;
                    //if (mintArrayCount>0)
                    //   self.document.forms[0].hddSel[lobj.value].value = "0"
                    //else   
                    //   self.document.forms[0].hddSel.value = "0";
                }
                //	else{
                mintAmountTotal = mintAmountTotal + insConvertNumber(nBalance);
                //		}
            }
            else {
                for (var i = 0; i < marrArray.length; i++) {
                    if ((GetDateYYYYMMDD(dEffecdate) < GetDateYYYYMMDD(marrArray[i].hddEffecdate)) && (marrArray[i].Sel))
                        nIndicErr = 1;
                }

                if (nIndicErr == 1) {
                    alert('Existen recibos/cuotas mas recientes ya seleccionadas');
                    //lobj.checked = !lobj.checked;
                    //if (mintArrayCount>0)
                    //   self.document.forms[0].hddSel[lobj.value].value = "1";
                    //else   
                    //   self.document.forms[0].hddSel.value = "1";
                }
                //else	
                mintAmountTotal = mintAmountTotal - insConvertNumber(nBalance);
            }

            marrArray[lobj.value].Sel = lobj.checked;
            self.document.forms[0].tcnTotal.value = VTFormat(mintAmountTotal, "", "", "", 6, true);
            self.document.forms[0].hddBalanceTotal.value = VTFormat(mintAmountTotal, "", "", "", 6, true);
            insDefValues('Premium', 'nAmount=' + VTFormat(mintAmountTotal, "", "", "", 6, true), '/VTimeNet/Claim/PaySeq');
        }
    
    </script>
    <%
        With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjValues.WindowsTitle("SI762", Request.QueryString("sWindowDescript")))
            If Request.QueryString("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, "SI762", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
            End If
	
        End With
        'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjMenu = Nothing
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="frmPenApprCla" action="valPaySeq.aspx?nMainAction=<%=Request.QueryString("nMainAction")%>">
    <%
        Response.Write(mobjValues.ShowWindowsName("SI762", Request.QueryString("sWindowDescript")))

        Call insDefineHeader()
        Call insPreSI762()
    %>
    </form>
</body>
</html>
<%
    'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjValues = Nothing
    'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Call mobjNetFrameWork.FinishPage("si762")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
