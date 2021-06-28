<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    '- Objeto para el manejo del grid de la página
    Dim mobjGrid As eFunctions.Grid
    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues
    '- Objeto para el manejo particular de los datos de la página
    Dim mcolClass As Object
    
    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        mobjGrid.sCodisplPage = "CA986"
        Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddTextColumn(0, "Año", "tcnYear", 4, "", True, , , , , False)
            Call .AddDateColumn(0, "Periodo de Venta desde", "tcdStartPeriod", "", , GetLocalResourceObject("tcdStartPeriodToolTip"))
            Call .AddDateColumn(0, "Periodo de Venta hasta", "tcdExpiredPeriod", "", , GetLocalResourceObject("tcdExpiredPeriodToolTip"))
            Call .AddDateColumn(0, "Periodo de Vigencia desde", "tcdStartDatePol", "", , GetLocalResourceObject("tcdStartDatePolToolTip"))
            Call .AddDateColumn(0, "Periodo de Vigencia hasta", "tcdExpiredDatePol", "", , GetLocalResourceObject("tcdExpiredDatePolToolTip"))
            Call .AddCheckColumn(0, "Estado", "chkStatus", "Anulado", , "1", , , GetLocalResourceObject("chkStatusToolTip"))
            Call .AddHiddenColumn("chkAuxStatus", CStr(2))
            
        End With
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "CA986"
            .ActionQuery = mobjValues.ActionQuery
            .Height = 320
            .Width = 400
            .AddButton = True
            .DeleteButton = False
            If Request.QueryString.Item("Action") = "Update" Then
                .Columns("tcdStartPeriod").Disabled = True
                .Columns("tcdExpiredPeriod").Disabled = True
                .Columns("tcdStartDatePol").Disabled = True
                .Columns("tcdExpiredDatePol").Disabled = True
                .Columns("tcnYear").Disabled = True
            End If
            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
                .ActionQuery = True
            End If
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = False
            .Columns("tcdStartPeriod").EditRecord = True
        End With
    End Sub

    '% insPreCA985: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA986()
        '----------------------------------------------------------------------------------------
        Dim lclsSoap_Sell_Period As ePolicy.Soap_Sell_Period
        Dim lcolSoap_Sell_Period As ePolicy.Soap_Sell_Periods
        lcolSoap_Sell_Period = New ePolicy.Soap_Sell_Periods
        If lcolSoap_Sell_Period.Find(Session("nVehType_CA986")) Then
            For Each lclsSoap_Sell_Period In lcolSoap_Sell_Period
                With mobjGrid
                    .Columns("tcdStartPeriod").DefValue = lclsSoap_Sell_Period.dStartPeriod
                    .Columns("tcdExpiredPeriod").DefValue = lclsSoap_Sell_Period.dExpirePeriod
                    .Columns("tcdStartDatePol").DefValue = lclsSoap_Sell_Period.dStartDatepol
                    .Columns("tcdExpiredDatePol").DefValue = lclsSoap_Sell_Period.dExpireDatepol
                    If lclsSoap_Sell_Period.sStatus = "2" Then
                        .Columns("chkStatus").Checked = 1
                    Else
                        .Columns("chkStatus").Checked = 0
                    End If
                    '.Columns("chkStatus").DefValue = lclsSoap_Sell_Period.sStatus
                    
                    .Columns("chkStatus").Disabled = True
                    .Columns("tcnYear").DefValue = lclsSoap_Sell_Period.nYear
                    Response.Write(.DoRow)
                End With
            Next lclsSoap_Sell_Period
        End If
        Response.Write(mobjGrid.closeTable())
        mcolClass = Nothing
    End Sub

    '% insPreCA980Upd: Gestiona lo relacionado a la actualización de un registro del Grid
    '------------------------------------------------------------------------------------
    Private Sub insPreCA986Upd()
        '------------------------------------------------------------------------------------
        Dim lclsSoap_Sell_Period As ePolicy.Soap_Sell_Period
        lclsSoap_Sell_Period = New ePolicy.Soap_Sell_Period
        With Request
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valpolicytra.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), , CShort(.QueryString.Item("Index"))))
        End With
        lclsSoap_Sell_Period = Nothing
    End Sub

    </script>

<%Response.Expires = -1
    mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = "CA986"
    mobjMenu = New eFunctions.Menues
%>

<html>
    <head>
	    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0"/>
        <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
        <script type="text/javascript">
        //- Variable para el control de versiones
        document.VssVersion="$$Revision: 9 $|$$Date: 11/05/04 19:20 $|$$Author: Nvaplat7 $"
        </script>
        <%Response.Write(mobjValues.StyleSheet())
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Response.Write(mobjMenu.setZone(2, "CA986", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
                mobjMenu = Nothing
                Response.Write("<script>var nMainAction=top.frames['fraSequence'].plngMainAction</script>")
            End If
        %>
    </head>

    <body onunload="closeWindows();">

        <form method="POST" name="CA986" action="ValPolicyTra.aspx?x=1">
            <%Response.Write(mobjValues.ShowWindowsName("CA986", Request.QueryString.Item("sWindowDescript")))
                Call insDefineHeader()
                If Request.QueryString.Item("Type") <> "PopUp" Then
                    Call insPreCA986()
                Else
                    Call insPreCA986Upd()
                End If
                mobjGrid = Nothing
                mobjValues = Nothing
            %>
        </form> 

    </body>

</html>