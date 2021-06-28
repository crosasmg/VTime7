<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As New eFunctions.Values

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
        mobjValues = New eFunctions.Values
	
        mobjGrid.sCodisplPage = "CR783"
	
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeInsur_areaCaption"), "cbeInsur_area", "table5001", eFunctions.Values.eValuesType.clngComboType, Session("nInsur_area"), , , , , "ChangeArea(this)", , , GetLocalResourceObject("cbeInsur_areaToolTip"))
            Call .AddCompanyColumn(0, GetLocalResourceObject("cbeCompanyCaption"), "cbeCompany", "", , GetLocalResourceObject("cbeCompanyToolTip"), "ClearDescCompany();", , "tctCompanyName")
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeBranchReiCaption"), "cbeBranchRei", "table5000", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeBranchReiToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeContraTypeCaption"), "cbeContraType", "table173", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeContraTypeToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnNumberCaption"), "tcnNumber", 4, CStr(0), , GetLocalResourceObject("tcnNumberToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valCovergenToolTip"), "valCovergen", "TABCOVERGEN", Values.eValuesType.clngWindowType, , True)

            With mobjGrid
                .Columns("valCovergen").Parameters.Add("nInsur_area", Session("nInsur_area"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With

            
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeValCaption"), "cbeTypeVal", "table5749", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeTypeValToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnFromValueCaption"), "tcnFromValue", 9, vbNullString, , GetLocalResourceObject("tcnFromValueToolTip"), False, 0)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnToValueCaption"), "tcnToValue", 9, vbNullString, , GetLocalResourceObject("tcnToValueToolTip"), False, 0)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountFixCaption"), "tcnAmountFix", 9, vbNullString, , GetLocalResourceObject("tcnAmountFixToolTip"), False, 6)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyCaption"), "cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeCurrencyToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentCaption"), "tcnPercent", 9, vbNullString, , GetLocalResourceObject("tcnPercentToolTip"), False, 6)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeComCaption"), "cbeTypeCom", "table5750", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeTypeComToolTip"), Values.eTypeCode.eString)
        End With
	
        '+ Se definen las propiedades generales del grid
        '+ Se definen las propiedades generales del grid
	
        With mobjGrid
            .Codispl = "CR783"
            .ActionQuery = mobjValues.ActionQuery
            .Height = 500
            .Width = 500
            .WidthDelete = 500
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("tcnNumber").EditRecord = True
            If Request.QueryString.Item("Action") = "Add" Then
                .Columns("tcnNumber").Disabled = False
            Else
                If Request.QueryString.Item("Action") = "Update" Then
                    .Columns("cbeInsur_area").Disabled = True
                    .Columns("cbeCompany").Disabled = True
                    .Columns("cbeBranchRei").Disabled = True
                    .Columns("cbeContraType").Disabled = True
                    .Columns("tcnNumber").Disabled = True
                    .Columns("valCovergen").Disabled = True
                    .Columns("cbeTypeVal").Disabled = True
                    .Columns("tcnFromValue").Disabled = True
                    
                End If
            End If
		
            .sEditRecordParam = "dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
		
            .sDelRecordParam = "nCovergen= '+ marrArray[lintIndex].valCovergen + '&nInsur_area='+ marrArray[lintIndex].cbeInsur_area + '&nTypeVal=' + marrArray[lintIndex].cbeTypeVal + '&nNumber='+ marrArray[lintIndex].tcnNumber + '&nBranch='+ marrArray[lintIndex].cbeBranchRei + '&nType='+ marrArray[lintIndex].cbeContraType + '" & _
                "&nCompany='+ marrArray[lintIndex].cbeCompany + '&nFromValue='+ marrArray[lintIndex].tcnFromValue + '" & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
		
        End With
      
    End Sub
    '% insPreCR783: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCR783()
        '--------------------------------------------------------------------------------------------
        Dim i As Integer
        Dim lblnFind As Boolean
        Dim lclsCommiss_contr As eCoReinsuran.Commiss_contr
        Dim lcolCommiss_contr As eCoReinsuran.Commiss_contrs

	
        i = 0
        lclsCommiss_contr = New eCoReinsuran.Commiss_contr
        lcolCommiss_contr = New eCoReinsuran.Commiss_contrs
	
        lblnFind = lcolCommiss_contr.Find(mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
        If lblnFind Then
            For i = 1 To lcolCommiss_contr.Count
                With mobjGrid
                    .Columns("cbeInsur_area").DefValue = CStr(lcolCommiss_contr.Item(i).nInsur_area)
                    .Columns("cbeCompany").DefValue = CStr(lcolCommiss_contr.Item(i).nCompany)
                    .Columns("cbeBranchRei").DefValue = CStr(lcolCommiss_contr.Item(i).nBranch_rei)
                    .Columns("cbeContraType").DefValue = CStr(lcolCommiss_contr.Item(i).nType)
                    .Columns("tcnNumber").DefValue = CStr(lcolCommiss_contr.Item(i).nNumber)
                    With mobjGrid.Columns("valCovergen").Parameters
                        .Add("nInsur_area", lcolCommiss_contr.Item(i).nInsur_area, eFunctions.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                    .Columns("valCovergen").DefValue = CStr(lcolCommiss_contr.Item(i).nCovergen)
                    .Columns("cbeTypeVal").DefValue = CStr(lcolCommiss_contr.Item(i).nTypeVal)
                    .Columns("tcnFromValue").DefValue = CStr(lcolCommiss_contr.Item(i).nFromValue)
                    .Columns("tcnToValue").DefValue = CStr(lcolCommiss_contr.Item(i).nToValue)
                    .Columns("tcnAmountFix").DefValue = CStr(lcolCommiss_contr.Item(i).nAmountfix)
                    .Columns("cbeCurrency").DefValue = CStr(lcolCommiss_contr.Item(i).nCurrency)
                    .Columns("tcnPercent").DefValue = CStr(lcolCommiss_contr.Item(i).nPercent)
                    .Columns("cbeTypeCom").DefValue = CStr(lcolCommiss_contr.Item(i).sTypecom)
                    Response.Write(.DoRow)
                End With
            Next
        End If
	
        Response.Write(mobjGrid.closeTable())
    End Sub

    '% insPreCR783Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCR783Upd()
        '--------------------------------------------------------------------------------------------
        Dim lobjCoReinsuranTra As eCoReinsuran.Commiss_contr
	
        lobjCoReinsuranTra = New eCoReinsuran.Commiss_contr
	
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
			
                If lobjCoReinsuranTra.InspostCR783Upd(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), _
                                                      mobjValues.StringToType(.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nTypeVal"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(.QueryString.Item("nFromValue"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                                      mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble), _
                                                                                      mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble), _
                                                                                      "", _
                                                                                      mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then

				
                End If
            End If
		
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValCoReinsuranTra.aspx", "CR783", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
    End Sub
    

</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues

    mobjValues.sCodisplPage = "CR783"

%>

<SCRIPT LANGUAGE=javascript>
    function ChangeArea(sField) {
        with (self.document.forms[0]) {
            valCovergen.Parameters.Param1.sValue = sField.value //Corresponde a tipo de documento
        }
    }
</SCRIPT>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0" />
    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
        Response.Write(mobjValues.StyleSheet())
        If Request.QueryString.Item("Type") <> "PopUp" Then
            Response.Write(mobjMenu.setZone(2, "CR783", "CR783.aspx"))
            mobjMenu = Nothing
            Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
        End If
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" name="CR783" action="valCoReinsuranTra.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("CR783"))
        Response.Write("<BR>")
        Call insDefineHeader()
        If Request.QueryString.Item("Type") = "PopUp" Then
            Call insPreCR783Upd()
        Else
            Call insPreCR783()
        End If
    %>
    <script language="JavaScript">
        //+ Esta línea guarda la versión procedente de VSS 
        document.VssVersion = "$$Revision: 2 $|$$Date: 30/03/06 13:24 $" 
    </script>
    </form>
</body>
</html>
