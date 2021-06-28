<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eCoReinsuran" %>
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
	
        mobjGrid.sCodisplPage = "CR782"
	
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnNumberCaption"), "tcnNumber", 4, CStr(0), , GetLocalResourceObject("tcnNumberToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeBranchReiCaption"), "cbeBranchRei", "table5000", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeBranchReiToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeContraTypeCaption"), "cbeContraType", "table173", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeContraTypeToolTip"))
            Call .AddCompanyColumn(0, GetLocalResourceObject("cbeCompanyCaption"), "cbeCompany", "", ,GetLocalResourceObject("cbeCompanyToolTip") , "ClearDescCompany();", , "tctCompanyName")

            Call .AddNumericColumn(0, GetLocalResourceObject("tcnIni_PolicyCaption"), "tcnIni_Policy", 9, vbNullString, , GetLocalResourceObject("tcnIni_PolicyToolTip"), False, 0)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnEnd_PolicyCaption"), "tcnEnd_Policy", 9, vbNullString, , GetLocalResourceObject("tcnEnd_PolicyToolTip"), False, 0)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentCaption"), "tcnPercent", 9, vbNullString, , GetLocalResourceObject("tcnPercentToolTip"), False, 6)
        End With
	
        '+ Se definen las propiedades generales del grid
	
        With mobjGrid
            .Codispl = "CR782"
            .ActionQuery = mobjValues.ActionQuery
            .Height = 350
            .Width = 500
            .WidthDelete = 500
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("tcnNumber").EditRecord = True
            If Request.QueryString.Item("Action") = "Add" Then
                .Columns("tcnNumber").Disabled = False
            Else
                If Request.QueryString.Item("Action") = "Update" Then
                    .Columns("tcnNumber").Disabled = True
                    .Columns("cbeBranchRei").Disabled = True
                    .Columns("cbeContraType").Disabled = True
                    .Columns("cbeCompany").Disabled = True
                    .Columns("tcnIni_Policy").Disabled = True
                End If
            End If
		
            .sEditRecordParam = "dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
		
            .sDelRecordParam = "nNumber='+ marrArray[lintIndex].tcnNumber + '&nBranch='+ marrArray[lintIndex].cbeBranchRei + '&nType='+ marrArray[lintIndex].cbeContraType + '" & _
                "&nCompany='+ marrArray[lintIndex].cbeCompany + '&nIni_Policy='+ marrArray[lintIndex].tcnIni_Policy + '" & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") 
		
		
        End With
    End Sub

    '% insPreCR782: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCR782()
        '--------------------------------------------------------------------------------------------
        Dim lclsContr_Rate_Profit As eCoReinsuran.Contr_Rate_Profit
        Dim lcolContr_Rate_Profits As eCoReinsuran.Contr_Rate_Profits
        Dim i As Integer
	
        Dim lblnFind As Boolean
	
        i = 0
        lclsContr_Rate_Profit = New eCoReinsuran.Contr_Rate_Profit
        lcolContr_Rate_Profits = New eCoReinsuran.Contr_Rate_Profits
	
        lblnFind = lcolContr_Rate_Profits.Find(mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
        If lblnFind Then
		
		
            For i = 1 To lcolContr_Rate_Profits.Count
                'For i = 0 To lcolContr_Rate_Profits.count -1
                With mobjGrid
                    .Columns("tcnNumber").DefValue = CStr(lcolContr_Rate_Profits.Item(i).nNumber)
                    .Columns("cbeBranchRei").DefValue = CStr(lcolContr_Rate_Profits.Item(i).nBranch_rei)
                    .Columns("cbeContraType").DefValue = CStr(lcolContr_Rate_Profits.Item(i).nType)
                    .Columns("cbeCompany").DefValue = CStr(lcolContr_Rate_Profits.Item(i).nCompany)
                    .Columns("tcnIni_Policy").DefValue = CStr(lcolContr_Rate_Profits.Item(i).nIni_Policy)
                    .Columns("tcnEnd_Policy").DefValue = CStr(lcolContr_Rate_Profits.Item(i).nEnd_Policy)
                    .Columns("tcnPercent").DefValue = CStr(lcolContr_Rate_Profits.Item(i).nPercent)
                    Response.Write(.DoRow)
                End With
            Next
        End If
	
        Response.Write(mobjGrid.closeTable())
    End Sub

    '% insPreCR782Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCR782Upd()
        '--------------------------------------------------------------------------------------------
        Dim lobjCoReinsuranTra As eCoReinsuran.Contr_Rate_Profit
	
        lobjCoReinsuranTra = New eCoReinsuran.Contr_Rate_Profit
	
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
			
			
                'If lobjCoReinsuranTra.insPostCR782("CR782", .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("valClasrisk"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
                If lobjCoReinsuranTra.InspostCR782Upd(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nIni_Policy"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                                      mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then

				
                End If
            End If
		
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValCoReinsuranTra.aspx", "CR782", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
    End Sub

</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues

    mobjValues.sCodisplPage = "CR782"

%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0" />
    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
        Response.Write(mobjValues.StyleSheet())
        If Request.QueryString.Item("Type") <> "PopUp" Then
            Response.Write(mobjMenu.setZone(2, "CR782", "CR782.aspx"))
            mobjMenu = Nothing
            Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
        End If
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" name="CR782" action="valCoReinsuranTra.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("CR782"))
        Response.Write("<BR>")
        Call insDefineHeader()
        If Request.QueryString.Item("Type") = "PopUp" Then
            Call insPreCR782Upd()
        Else
            Call insPreCR782()
        End If
    %>
    <script language="JavaScript">
        //+ Esta línea guarda la versión procedente de VSS 
        document.VssVersion = "$$Revision: 2 $|$$Date: 30/03/06 13:24 $" 
    </script>
    </form>
</body>
</html>
