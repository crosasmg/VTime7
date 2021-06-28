<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="ePolicy" %>
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
    '    Session("nTotalAmount") = 10000000
    Dim mclsCl_Coinsuran As eClaim.Cl_Coinsuran
    Dim mclsClaim As eClaim.Claim


    '%insDefineHeader: Se definen las columnas del grid
    '------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
	
        mobjGrid.sCodisplPage = "si754"
        Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            Call .AddPossiblesColumn(0, "Compañía", "valCompany", "tabCompany", eFunctions.Values.eValuesType.clngWindowType, , , , , , , True, 4, "Compañía coaseguradora participante", eFunctions.Values.eTypeCode.eNumeric)
            Call .AddNumericColumn(0, "Monto de participación", "tcnAmount_Share", 18, "", False, "Monto del siniestro con el que participa la compañía coaseguradora", True, 6, , , , True)
            Call .AddNumericColumn(0, "% Participación", "tcnShare_Percentage", 5, "", False, "Porcentaje del siniestro con el que participa la compañía coaseguradora", True, 2, , , "CalculateAmount(this.value, self.document.forms[0].elements['tcnClaim_Total_AUX'].value)", False)
            Call .AddHiddenColumn("tcnShare_Percentage_AUX", CStr(0))
            Call .AddHiddenColumn("valCompany_AUX", CStr(0))
            Call .AddHiddenColumn("tcnClaim_Total_AUX", CStr(0))
            Call .AddHiddenColumn("hddExpenses", "")
            Call .AddHiddenColumn("hddSel", "")
        End With
	
        With mobjGrid
            .nMainAction = Request.QueryString("nMainAction")
            .Codispl = "SI754"
            .Codisp = "SI754"
            .Top = 100
            .Height = 230
            .Width = 400
            .AddButton = False
            .DeleteButton = False
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("valCompany").EditRecord = True

            '.sDelRecordParam = CStr(CDbl("nOwn_Share=" + Request.QueryString("nOwn_Share") + "&nClaim_Total=") + Request.QueryString("nClaim_Total") + CDbl("&nAdmin_Expenses=") + Request.QueryString("nAdmin_Expenses") + CDbl("&nClaim_number=") + Request.QueryString("nClaim_number") + CDbl("&nCompany='+ marrArray[lintIndex].valCompany + '"))
            '.sEditRecordParam = CStr(CDbl("nOwn_Share=' + self.document.forms[0].tcnOwn_Share.value + '" & "&nClaim_Total=") + Request.QueryString("nClaim_Total") + CDbl("&nAdmin_Expenses=") + Request.QueryString("nAdmin_Expenses") + CDbl("&nClaim_number=") + Request.QueryString("nClaim_number"))
            If Request.QueryString("Reload") = "1" Then
                .sReloadIndex = Request.QueryString("ReloadIndex")
            End If
        End With
    End Sub

    '%insPreSI754. Se crea la ventana madre (Principal)
    '------------------------------------------------------------------------------
    Private Sub insPreSI754()
        '------------------------------------------------------------------------------
        Dim lintCount As Byte
        Dim ldblPremium As Object
        Dim ldblOwn_Share As Double
        Dim ldblExpenses As Double
        Dim lclsCoinsuran As ePolicy.Coinsuran
        Dim lclscl_Coinsuran As eClaim.Cl_Coinsuran
        Dim lcolcl_Coinsuran As eClaim.cl_Coinsurans
        Dim llngCompanyFirst As Integer
	
        lclsCoinsuran = New ePolicy.Coinsuran
        lclscl_Coinsuran = New eClaim.Cl_Coinsuran
        lcolcl_Coinsuran = New eClaim.cl_Coinsurans
	
        ldblPremium = 0
        ldblOwn_Share = 0
        ldblExpenses = 0
        lintCount = 0
	
        llngCompanyFirst = Session("nCompanyUser")
	
        Call mclsClaim.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble))
        Call lclsCoinsuran.Find("2", mclsClaim.nBranch, mclsClaim.nProduct, mclsClaim.nPolicy, llngCompanyFirst, mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), True)
	
        ldblPremium = mclsClaim.nLoc_Reserv
	
        If lcolcl_Coinsuran.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble)) Then
            lintCount = 1
        End If
	
        Response.Write("" & vbCrLf)
        Response.Write("	<BR>" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>% Participación propia</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnOwn_Share", 4, CStr(0), True, "Porcentaje de participación propia en el riesgo", , 2, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Total del siniestro</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnClaim_Total", 18, ldblPremium, True, "Monto total de reserva en moneda local", True, 6, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Gastos administrativos</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnAdmin_Expenses", 4, CStr(0), True, "Porcentaje sobre la prima que se cobra por gastos administrativos", , 2, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("    </TABLE>")

	
	
        If lintCount = 1 Then
            mobjGrid.Columns("Sel").OnClick = "insUpdateSelection(this)"
            For Each lclscl_Coinsuran In lcolcl_Coinsuran
                With mobjGrid
                    .Columns("valCompany").DefValue = CStr(lclscl_Coinsuran.nCompany)
                    .Columns("valCompany").Descript = lclscl_Coinsuran.sCompany
				
                    If ldblPremium <> 0 And ldblPremium <> "" And ldblPremium <> eRemoteDB.Constants.intNull Then
                        .Columns("tcnAmount_Share").DefValue = CStr((mobjValues.StringToType(ldblPremium, eFunctions.Values.eTypeData.etdDouble) * mobjValues.StringToType(CStr(lclscl_Coinsuran.nShare), eFunctions.Values.eTypeData.etdDouble)) / 100)
                    Else
                        .Columns("tcnAmount_Share").DefValue = CStr(0)
                    End If
                    .Columns("tcnShare_Percentage").DefValue = CStr(lclscl_Coinsuran.nShare)
                    .Columns("tcnShare_Percentage_AUX").DefValue = CStr(lclscl_Coinsuran.nShare)
                    .Columns("valCompany_AUX").DefValue = CStr(lclscl_Coinsuran.nCompany)
                    .Columns("hddExpenses").DefValue = CStr(lclscl_Coinsuran.nExpenses)
                    .Columns("hddSel").DefValue = lclscl_Coinsuran.sSel
				
                    If lclscl_Coinsuran.sSel = "1" Then
                        .Columns("Sel").Checked = CShort("1")
                    Else
                        .Columns("Sel").Checked = CShort("2")
                    End If
				
                    Response.Write(mobjGrid.DoRow())
                    If lclscl_Coinsuran.nCompany = llngCompanyFirst Then
                        ldblExpenses = lclscl_Coinsuran.nExpenses
                        ldblOwn_Share = lclscl_Coinsuran.nShare
					
                        Response.Write("<script>top.frames['fraFolder'].document.forms[0].tcnOwn_Share.value='" & lclscl_Coinsuran.nShare & "';</" & "Script>")
                        Response.Write("<script>top.frames['fraFolder'].document.forms[0].tcnAdmin_Expenses.value='" & lclscl_Coinsuran.nExpenses & "';</" & "Script>")
					
                    End If
                End With
            Next lclscl_Coinsuran
            Response.Write(mobjGrid.closeTable())
        End If
        Response.Write(mobjGrid.closeTable())
	
        If ldblPremium = eRemoteDB.Constants.intNull Then
            ldblPremium = 0
        End If
	
        Response.Write(mobjValues.HiddenControl("nClaim_Total_AUX", ldblPremium))
	
        'UPGRADE_NOTE: Object lintCount may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lintCount = Nothing
        'UPGRADE_NOTE: Object lclsCoinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsCoinsuran = Nothing
        'UPGRADE_NOTE: Object lclscl_Coinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclscl_Coinsuran = Nothing
        'UPGRADE_NOTE: Object lcolcl_Coinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolcl_Coinsuran = Nothing
        'UPGRADE_NOTE: Object mclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mclsClaim = Nothing
    End Sub

    '% insPreSI754Upd. Se define esta funcion para contruir el contenido de la 
    '%                     ventana UPD de los archivos de datos particulares
    '------------------------------------------------------------------------------
    Private Sub insPreSI754Upd()
        '------------------------------------------------------------------------------
        If Request.QueryString("Action") = "Del" Or Request.QueryString("Action") = "Delete" Then
            Response.Write(mobjValues.ConfirmDelete())
            Call mclsCl_Coinsuran.insPostSI754(Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("nCompany"), eFunctions.Values.eTypeData.etdDouble), "0", "", mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), "")
        End If
        Response.Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valPaySeq.aspx", "SI754", Request.QueryString("nMainAction"), mobjGrid.ActionQuery, Request.QueryString("Index")))
        If Request.QueryString("Action") = "Update" Then
            Response.Write("<script>self.document.forms[0].elements['valCompany'].disabled=true;</" & "Script>")
        End If
	
        If Request.QueryString("Action") = "Update" Or Request.QueryString("Action") = "Add" Then
            Response.Write("<script>self.document.forms[0].elements['tcnClaim_Total_AUX'].value = top.opener.document.forms[0].elements['nClaim_Total_AUX'].value;</" & "Script>")
        End If
	
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si754")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si754"

    mclsCl_Coinsuran = New eClaim.Cl_Coinsuran
    mclsClaim = New eClaim.Claim


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
                .Write(mobjMenu.setZone(2, "SI754", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
            End If
        End With
    %>
    <script>
        //% CalculateAmount: Calcula el monto de la participación del coaseguro
        //----------------------------------------------------------------------------------------
        function CalculateAmount(nShare, nTotalAmount)
        //----------------------------------------------------------------------------------------
        {
            var Amount = 0;
            if (nShare != "0" && nShare != "") {
                Amount = ((parseFloat(nShare) * parseFloat(nTotalAmount)) / 100);
            }
            self.document.forms[0].elements["tcnAmount_Share"].value = parseFloat(Amount);
        }

        //---------------------------------------------------------------------------------------------------------
        function insUpdateSelection(lobj) {
            //---------------------------------------------------------------------------------------------------------
            if (mintArrayCount > 0) {
                if (lobj.checked == false) {
                    self.document.forms[0].hddSel[lobj.value].value = "0";
                }
                else {
                    self.document.forms[0].hddSel[lobj.value].value = "1";
                }
            }
            else {
                if (lobj.checked == false) {
                    self.document.forms[0].hddSel.value = "0";
                }
                else {
                    self.document.forms[0].hddSel.value = "1";
                }
            }
        }


    </script>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="frmSI754" action="valPaySeq.aspx?sZone=2&nMainAction=<%=Request.QueryString("nMainAction")%>">
    <%
        Response.Write(mobjValues.ShowWindowsName("SI754", Request.QueryString("sWindowDescript")))

        Call insDefineHeader()
        If Request.QueryString("Type") <> "PopUp" Then
            Call insPreSI754()
        Else
            Call insPreSI754Upd()
        End If
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
        'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjGrid = Nothing
    %>
    </form>
</body>
</html>
<%
    'UPGRADE_NOTE: Object mclsCl_Coinsuran may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mclsCl_Coinsuran = Nothing

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Call mobjNetFrameWork.FinishPage("si754")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
