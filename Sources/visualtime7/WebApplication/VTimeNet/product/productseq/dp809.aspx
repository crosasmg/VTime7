<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

Dim mintCount As Object


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid.
	
        With mobjGrid.Columns
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddPossiblesColumn(0, GetLocalResourceObject("valCodisplColumnCaption"), "valCodispl", "TABWINDOWS_SAUTOREP", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , , 8, GetLocalResourceObject("valCodisplColumnCaption"), eFunctions.Values.eTypeCode.eString)
                mobjGrid.Columns("valCodispl").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCodispl").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'Call .AddTextColumn(0, GetLocalResourceObject("valCodCodisplColumnCaption"), "valCodCodispl", 8, CStr(0), False, GetLocalResourceObject("valCodCodisplColumnToolTip"), , , , True)
                'Call .AddTextColumn(0, "Transacion", "cbeTransaction", 50, "", False, "Código", , , , True)
                Call .AddPossiblesColumn(0, "Transacion", "cbeTransaction", "Table5588", eFunctions.Values.eValuesType.clngComboType, eRemoteDB.Constants.intNull, , , , , "", , , "Tipo de Transacion")
                'Call .AddTextColumn(0, "Tipo Rep.", "cbeReptype", 50, "", False, "Código", , , , True)
                Call .AddPossiblesColumn(0, "Tipo Rep.", "cbeReptype", "Table8030", eFunctions.Values.eValuesType.clngComboType, eRemoteDB.Constants.intNull, , , , , , , , "Tipo de Reporte")
                Call .AddTextColumn(0, "Reporte", "tctReport", 50, "", False, "Código", , , , True)
                .AddAnimatedColumn(0, GetLocalResourceObject("sLinkColumnCaption"), "sLink", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("sLinkColumnToolTip"))
            Else
                Call .AddPossiblesColumn(0, GetLocalResourceObject("valCodisplColumnCaption"), "valCodispl", "TABWINDOWS_SAUTOREP", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , , 8, GetLocalResourceObject("valCodisplColumnCaption"), eFunctions.Values.eTypeCode.eString)
                mobjGrid.Columns("valCodispl").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCodispl").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Call .AddPossiblesColumn(0, "Transacion", "cbeTransaction", "Table5588", eFunctions.Values.eValuesType.clngComboType, eRemoteDB.Constants.intNull, , , , , "", , , "Tipo de Transacion")
                Call .AddPossiblesColumn(0, "Tipo Rep.", "cbeReptype", "Table8030", eFunctions.Values.eValuesType.clngComboType, eRemoteDB.Constants.intNull, , , , , , , , "Tipo de Reporte")
                Call .AddTextColumn(0, "Reporte", "tctReport", 30, "", , "Código", , , , False)
                
            End If
            Call .AddHiddenColumn("hddvalCodispl", "")
            Call .AddHiddenColumn("hddTransaction", "")
            
        End With
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.AddButton = True
		.DeleteButton = True
            .Height = 300
            .Width = 450
        .WidthDelete = 450
		.Codispl = "DP809"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .Columns("cbeTransaction").EditRecord = True
            .Columns("Sel").GridVisible = Not Session("bQuery")
		.sDelRecordParam = "sDelCodispl='+ marrArray[lintIndex].hddvalCodispl + '"
	End With
End Sub
'% insPreDP809: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP809()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lclsreport_prod As Object
	Dim lcolreport_prods As eProduct.report_prods
	
	lcolreport_prods = New eProduct.report_prods
	
        If lcolreport_prods.FindReport_prod(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then            
            lintIndex = 0
            For Each lclsreport_prod In lcolreport_prods
                With mobjGrid
                    .Columns("valCodispl").DefValue = lclsreport_prod.sCodCodispl 'lclsreport_prod.sCodCodispl & " - " & lclsreport_prod.sDescript
                    .Columns("cbeTransaction").DefValue = lclsreport_prod.nTratypep ' lclsreport_prod.sDesTratypep
                    .Columns("cbeReptype").DefValue = lclsreport_prod.nRepType 'lclsreport_prod.sDesReptype
                    .Columns("tctReport").DefValue = lclsreport_prod.sReport
                    .Columns("hddvalCodispl").DefValue = lclsreport_prod.sCodCodispl
                    If lclsreport_prod.nType_Report = 1 Then
                        .Columns("sLink").HRefScript = "ShowSubSequence(" & lintIndex & ")"
                    Else
                        .Columns("sLink").HRefScript = ""
                    End If
                    .Columns("hddTransaction").DefValue = lclsreport_prod.nTratypep
                    Response.Write(.DoRow)
                End With
                lintIndex = lintIndex + 1
            Next lclsreport_prod
        End If
	
	Response.Write(mobjGrid.closeTable())
	lcolreport_prods = Nothing
	lclsreport_prod = Nothing
End Sub

'% insPreDP017Upd: Permite realizar el llamado a la ventana PopUp, cuando se está eliminando
'% un registro. 
'-----------------------------------------------------------------------------------------
Private Sub insPreDP809Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsreport_prod As eProduct.report_prod
	lclsreport_prod = New eProduct.report_prod
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		If lclsreport_prod.insPostDP809("Delete", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "DP809", Request.QueryString.Item("sDelCodispl"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
		End If
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProductSeq.aspx", "DP809", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	lclsreport_prod = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP809"

mobjMenu = New eFunctions.Menues

mobjGrid = New eFunctions.Grid
mobjGrid.sCodisplPage = "DP809"

mobjGrid.ActionQuery = Session("bQuery")
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




 
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP809", "DP809.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Response.Write(mobjValues.StyleSheet())
%>
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $"
</SCRIPT>
<SCRIPT>
//% ShowSubSequence: abre el pop up del cuadro de pólizas
//--------------------------------------------------------------------------------------------
function ShowSubSequence(Index){
    //--------------------------------------------------------------------------------------------
    //TODO : probar con 1 registro en el grid y con mas de uno... ambas no deberian funcionar
//    alert(Index);
    ShowPopUp('DP809B.aspx?scodispl_orig=' + marrArray[Index].hddvalCodispl + '&nTratypep=' + marrArray[Index].hddTransaction, 'DP809', 580, 500, 'yes', 'no', 200, 80);
//	ShowPopUp('DP809A.aspx?scodispl_orig=' + marrArray[Index].hddvalCodispl + '&Type=PopUp','DP809A',580,500,'no','no',200,80);
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP809" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP809"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP809Upd()
Else
	Call insPreDP809()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





