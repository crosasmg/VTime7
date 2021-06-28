<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenu As eFunctions.Menues
    Dim mclsRecover As eClaim.Recover
    Dim mclsClaim_win As eClaim.Claim_win
    Dim lclsRecover As eClaim.Recover
     

    '%insDefineHeader : Define las columnas del grid.
    '%----------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '%----------------------------------------------------------------------------------------------
          lclsRecover = New eClaim.Recover
        With mobjGrid
            With .Columns
                .AddTextColumn(0, "Cobertura", "tctCoverDescript", 70, "", , "Coberturas a las que se le puede realizar un recobro.", , , , True)
                .AddNumericColumn(0, "Monto recobrado", "tcnRecamount", 18, , , "Cantidad recobrada por cada cobertura.", True, 6)
                .AddHiddenColumn("tcnCostrecu" , 0)
                .AddHiddenColumn("nCover", "")
                .AddHiddenColumn("nModuleC", "")
                .AddHiddenColumn("sClient", "")
            End With
		
            .Codispl = "SI013"
            .Width = 580
            .Height = 180
            .AddButton = False
            .DeleteButton = False
            .Height = 230
		    
            If lclsRecover.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCasenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDemantype"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransac"), eFunctions.Values.eTypeData.etdDouble)) Then
                If lclsRecover.nStatus = 1 Then
                        .Columns("tctCoverDescript").EditRecord = false 
                Else
                    .Columns("tctCoverDescript").EditRecord = True
                End If
            End If 
                .Columns("Sel").GridVisible = False
		
                Response.Write(mobjValues.HiddenControl("hddnTransac", ""))
                Response.Write(mobjValues.HiddenControl("hddnBordereaux", ""))
		
                If Request.QueryString("Type") = "PopUp" Then
                    Response.Write("<script>self.document.forms[0].hddnTransac.value = top.opener.document.forms[0].cbeTransac.value;</" & "Script>")
                    Response.Write("<script>self.document.forms[0].hddnBordereaux.value = top.opener.document.forms[0].tcnBordereaux.value;</" & "Script>")
                End If
		
                If Request.QueryString("Reload") = "1" Then
                    .sReloadIndex = Request.QueryString("ReloadIndex")
                End If
                .DeleteScriptName = vbNullString
                .sEditRecordParam = "sDescriptCurrency=' + self.document.forms[0].hddtctCurrency.value + '&nPreviousAmou=' + self.document.forms[0].hddtcnPreviousAmou.value +'&nPreviousExpense=' + self.document.forms[0].hddtcnPreviousExpense.value +'"
                .ActionQuery = Session("bQuery")
        End With
    End Sub

    '%insPreSI013 : Realiza la carga inicial de los datos del grid
    '%----------------------------------------------------------------------------------------------
    Private Sub insPreSI013()
        '%----------------------------------------------------------------------------------------------
        Dim lclsRecover As eClaim.Recover
        Dim lclsRecovers As eClaim.Recovers
        lclsRecover = New eClaim.Recover
        lclsRecovers = New eClaim.Recovers
        Dim Rownum As Integer = 0
        If lclsRecovers.Find_RecoverCover(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("RecoveryTransac")), eFunctions.Values.eTypeData.etdInteger )) Then
            With mobjGrid
                For Each lclsRecover In lclsRecovers
                    .Columns("nCover").DefValue = CStr(lclsRecover.nCover)
                    .Columns("tctCoverDescript").DefValue = lclsRecover.sCoverDescript
                    .Columns("nModuleC").DefValue = CStr(lclsRecover.nModulec)
                    .Columns("sClient").DefValue = lclsRecover.sClient
				
                    Call lclsRecover.FindTCLRecover(CStr(Session("sKey")), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), lclsRecover.nCover, lclsRecover.sClient, mobjValues.StringToType(CStr(Session("RecoveryTransac")), eFunctions.Values.eTypeData.etdInteger))
				
                    .Columns("tcnRecamount").DefValue = CStr(lclsRecover.nRecoverAmou)
                    .Columns("tcnCostrecu").DefValue = CStr(lclsRecover.nExpensesAmou)
                    Rownum = Rownum + 1
                    Response.Write(.DoRow)
                Next lclsRecover
            End With
        End If
        Response.Write(mobjGrid.closeTable)
        Response.Write(mobjValues.BeginPageButton)
	
        Response.Write("<script>insShowCurrentAmount();</" & "Script>")
	
	
        '+ Actualiza el estado de la ventana y se recarga el Frame de la secuencia.
        '	Call mclsClaim_win.Add_Claim_win(Session("nClaim"), "SI013", "1", Session("nUserCode"))    
        '	Response.Write "<NOTSCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Claim/ClaimSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&sGoToNext=NO" & "';</" & "Script>"
	
        'UPGRADE_NOTE: Object lclsRecover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRecover = Nothing
        'UPGRADE_NOTE: Object lclsRecovers may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRecovers = Nothing
        'UPGRADE_NOTE: Object mclsClaim_win may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mclsClaim_win = Nothing
    End Sub

    '%insPreSI013Upd : Permite la actualización de los elementos puntuales sobre el grid.
    '%----------------------------------------------------------------------------------------------
    Private Sub insPreSI013Upd()
        '%----------------------------------------------------------------------------------------------
        With Request
            Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valClaimSeq.aspx", "SI013", .QueryString("nMainAction"), False, .QueryString("Index")))
        End With
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si013")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si013"
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    mobjGrid = New eFunctions.Grid
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
    mobjGrid.sSessionID = Session.SessionID
    mobjGrid.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjGrid.sCodisplPage = "si013"
    Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
    mclsRecover = New eClaim.Recover
    mclsClaim_win = New eClaim.Claim_win

    mobjValues.ActionQuery = Session("bQuery")

    Response.Write("<script>var nClaim = " & Session("nClaim") & "</script>")

    '+ Se establece el valor del Key en caso de no existir    
    If CStr(Session("sKey")) = vbNullString Then
        Session("sKey") = mclsRecover.sKey(mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
    End If

    'UPGRADE_NOTE: Object mclsRecover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mclsRecover = Nothing
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script>
        // insFindRecover : Consulta de los ingresos por recobro que se han realizado
        //------------------------------------------------------------------------------------
        function insFindRecover() {
            //------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                if (cbeTransac.value != '')
                    insDefValues('Recover', 'nClaim=' + nClaim + '&nTransac=' + cbeTransac.value, '/VTimeNet/Claim/ClaimSeq')
                else {
                    UpdateDiv('tctCurrency', '', '');
                    UpdateDiv('tcnPreviousExpense', '', '');
                    UpdateDiv('tcnPreviousAmou', '', '');
                }
            }
        }

        // insShowCurrentAmount : Muestra los valores actuales de los montos del recobro (agregados en el grid)
        //------------------------------------------------------------------------------------
        function insShowCurrentAmount() {
            //------------------------------------------------------------------------------------
            insDefValues('CurrentAmount', '', '/VTimeNet/Claim/ClaimSeq');
        }
    </script>
    <%With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjValues.WindowsTitle("SI013", Request.QueryString("sWindowDescript")))
            If Request.QueryString("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, "SI013", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
                .Write("<script>var nMainAction=top.frames['fraSequence'].plngMainAction;</script>")
            End If
        End With
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="frmSI013" action="ValClaimSeq.aspx?sMode=1">
    <%If Request.QueryString("Type") <> "PopUp" Then%>
    <a name="BeginPage"></a>
    <p align="Center">
        <label id="0">
            <a href="#Montos de gastos y recobros">Montos de gastos y recobros</a></label>
    </p>
    <% Response.Write(mobjValues.ShowWindowsName("SI013", Request.QueryString("sWindowDescript")))%>
    <table width="100%">
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <label id="0">
                    Trámite de recobro</label>
            </td>
            <td>
                <label id="0">
                    <%	
                        mobjValues.Parameters.Add("nClaim", mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        mobjValues.Parameters.Add("nCase_num", mobjValues.StringToType(Session("nCaseNum"), Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(mobjValues.PossiblesValues("cbeTransac", "tabrecover", eFunctions.Values.eValuesType.clngWindowType, Session("RecoveryTransac"),  True,  , , , , "insFindRecover();",true , , "Identificativo del trámite de recobro al que se le realiza el ingreso por recobro." ))%></label>
            </td>
            <td>
            </td>
            <td>
                <label id="0">
                    Número de relación</label>
            </td>
            <td>
                <label id="0">
                    <%	
                        Response.Write(mobjValues.NumericControl("tcnBordereaux", 10, Request.QueryString("nBordereaux"), , "Número de relación asociado al recobro.", , , , , , , True))%></label>
            </td>
        </tr> 
    </table>
    <table width="100%">
        <tr>
            <td colspan="5" class="HighLighted" align="RIGHT">
                <label id="40302">
                    <a name="Montos de gastos y recobros">Monto de gastos y recobros</a></label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td width="25%">
                <label id="0">
                    Moneda</label>
            </td>
            <td align="RIGHT">
                <% Response.Write(mobjValues.DIVControl("tctCurrency", , Request.QueryString("sDescriptCurrency")))
                    Response.Write(mobjValues.HiddenControl("hddtctCurrency", ""))
                %>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="25%">
                <label id="0">
                    Recobros anteriores</label>
            </td>
            <td width="20%" align="RIGHT">
                <% Response.Write(mobjValues.DIVControl("tcnPreviousAmou", , Request.QueryString("nPreviousAmou")))
                    Response.Write(mobjValues.HiddenControl("hddtcnPreviousAmou", ""))
                %>
            </td>
            <td>
                &nbsp;
            </td>
            <td width="20%">
                <label id="0">
                    Recobros actuales</label>
            </td>
            <td>
                <%=mobjValues.DIVControl("tcnCurrentAmou")%>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <label id="0">
                    Gastos anteriores</label>
            </td>
            <td width="20%" align="RIGHT">
                <% Response.Write(mobjValues.DIVControl("tcnPreviousExpense", , Request.QueryString("nPreviousExpense")))
                    Response.Write(mobjValues.HiddenControl("hddtcnPreviousExpense", ""))
                %>
            </td>
            <td>
                &nbsp;
            </td>
            <td width="20%">
                <label id="0">
                    Gastos actuales</label>
            </td>
            <td>
                <%=mobjValues.DIVControl("tcnCurrentExpense")%>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <label id="0">
                    Notas</label>
            </td>
            <td width="20%" align="RIGHT">
                <%=mobjValues.ButtonNotes("SCA2-K", eRemoteDB.Constants.intNull, False, CBool(Session("bquery")))%>
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
    </table>
    <%	
    End If

    Call insDefineHeader()
    If Request.QueryString("Type") = "PopUp" Then
        Call insPreSI013Upd()
    Else
        Call insPreSI013()
    End If

    'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjGrid = Nothing
    'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjValues = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.39
    Call mobjNetFrameWork.FinishPage("si013")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
