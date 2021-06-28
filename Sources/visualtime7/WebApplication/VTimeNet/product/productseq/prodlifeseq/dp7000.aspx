<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

    '**- The object to handling the general function for the loads of values is defined.
    '- Objeto para el manejo de las funciones generales de carga de valores.

    Dim mobjValues As eFunctions.Values

    '**- The variable mobjGrid to handling the Grid of the window is defined.
    '- Se define la variable mobjGrid para el manejo del Grid de la ventana.

    Dim mobjGrid As eFunctions.Grid

    '**- The object to control the page zones is defined.
    '- Objeto para el manejo de las zonas de la página.

    Dim mobjMenu As eFunctions.Menues



    '**% insDefineHeader: The Grid columns are defined.
    '% insDefineHeader: Se definen las columnas del grid.
    '------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '------------------------------------------------------------------------------

        '**+ The Grid columns are defined
        '+ Se definen todas las columnas del Grid
        Call mobjGrid.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbecuentaColumnCaption"), "nOrigin", "table5633", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeCuentaColumnCaption"), eFunctions.Values.eTypeCode.eNumeric)
        Call mobjGrid.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeReasonColumnCaption"), "cbeReason", "table5635", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeReasonColumnCaption"), eFunctions.Values.eTypeCode.eNumeric)
        Call mobjGrid.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeTbtColumnCaption"), "nTyp_profitworker", "table950", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeTbtColumnCaption"), eFunctions.Values.eTypeCode.eNumeric)
        Call mobjGrid.Columns.AddPossiblesColumn(0, GetLocalResourceObject("MonedaColumnCaption"), "nCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("MonedaColumnCaption"), eFunctions.Values.eTypeCode.eNumeric)
        Call mobjGrid.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnRetentionColumnCaption"), "tcnRetention", 4, CStr(0), , GetLocalResourceObject("tcnRetentionColumnCaption"), False, 2, , , , False)
        Call mobjGrid.Columns.AddNumericColumn(0, GetLocalResourceObject("MontoTopeColumnCaption"), "nAmountfree", 4, CStr(0), , GetLocalResourceObject("MontoTopeColumnCaption"), False, 0, , , , False)

        With mobjGrid
            .Codispl = "DP7000"
            .Codisp = "DP7000"
            .Top = 100
            .Height = 300
            .Width = 400

            .bOnlyForQuery = Session("bQuery")
            .ActionQuery = Session("bQuery")

            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("cbeReason").EditRecord = True
            .Columns("nOrigin").EditRecord = True
            .Columns("nTyp_profitworker").EditRecord = True
            .Columns("tcnRetention").EditRecord = True
            .Columns("nAmountfree").EditRecord = True
            .Columns("nCurrency").EditRecord = True


            .Columns("cbeReason").Disabled = Request.QueryString.Item("Action") = "Update"

            .Columns("cbeReason").BlankPosition = True
            .Columns("nOrigin").BlankPosition = True
            .Columns("nTyp_profitworker").BlankPosition = True
            .Columns("tcnRetention").BlankPosition = True
            .Columns("nAmountfree").BlankPosition = True
            .Columns("nCurrency").BlankPosition = True



            '.sDelRecordParam = "nSurr_reason='+ marrArray[lintIndex].cbeReason + ' & "&nOrigin=' + marrArray[lintIndex].nOrigin + '"
            .sDelRecordParam = "nSurr_reason='+ marrArray[lintIndex].cbeReason + '&nOrigin=' + marrArray[lintIndex].nOrigin + '&nTyp_profitworker=' + marrArray[lintIndex].nTyp_profitworker+ '&nSurr_Ret=' + marrArray[lintIndex].tcnRetention + '&nAmountfree=' + marrArray[lintIndex].nAmountfree+ '&nCurrency=' + marrArray[lintIndex].nCurrency + '"
            .sReloadAction = Request.QueryString.Item("ReloadAction")

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '**% insPreDP7000: The mother window is create (Main window).
    '% insPreDP7000: Se crea la ventana madre (Principal).
    '------------------------------------------------------------------------------
    Private Sub insPreDP7000()
        '------------------------------------------------------------------------------
        Dim lclsSurr_retention As Object
        Dim lcolSurr_retention As eProduct.Surr_retentions

        Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

        lcolSurr_retention = New eProduct.Surr_retentions

        With mobjGrid


            If lcolSurr_retention.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

                For Each lclsSurr_retention In lcolSurr_retention

                    .Columns("cbeReason").DefValue = lclsSurr_retention.nSurr_reason
                    .Columns("nOrigin").DefValue = lclsSurr_retention.nOrigin
                    .Columns("nTyp_profitworker").DefValue = lclsSurr_retention.nTyp_profitworker
                    .Columns("tcnRetention").DefValue = lclsSurr_retention.nSurr_ret
                    .Columns("nAmountfree").DefValue = lclsSurr_retention.nAmountfree
                    .Columns("nCurrency").DefValue = lclsSurr_retention.nCurrency

                    Response.Write(mobjGrid.DoRow())
                Next lclsSurr_retention
            End If

        End With

        Response.Write(mobjGrid.CloseTable())

        lclsSurr_retention = Nothing
        lcolSurr_retention = Nothing
    End Sub

    '**% insPreDP7000Upd: Its defines this function to constructs the Pop Up window 
    '**% when the action is update or delete
    '% insPreDP7000Upd: Se define esta funcion para contruir la ventana Pop Up
    '% Cuando la acción es actualizar o borrar
    '------------------------------------------------------------------------------
    Private Sub insPreDP7000Upd()
        '------------------------------------------------------------------------------
        Dim lclsSurr_retention As eProduct.Surr_retention

        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())

                lclsSurr_retention = New eProduct.Surr_retention

                Call lclsSurr_retention.insPostDP7000(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nSurr_reason"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nSurr_Ret"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTyp_profitworker"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAmountfree"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                lclsSurr_retention = Nothing
            Else
                Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")) & "<BR>")
            End If

            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProdLifeSeq.aspx", "DP7000", CStr(301), Session("bQuery"), CShort(.QueryString.Item("Index"))))
        End With
    End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Session("bQuery")
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "dp7000"
mobjGrid.sCodisplPage = "dp7000"
%> 
<SCRIPT LANGUAGE="JavaScript">
//**+ For the Source Safe control.
//+ Para Control de Versiones. 
//------------------------------------------------------------------------------
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:08 $"
//------------------------------------------------------------------------------
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "DP7000", "DP7000.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
 <FORM METHOD="POST"	ID="FORM" NAME="frmDP7000" ACTION="valProdLifeSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP7000()
Else
	Call insPreDP7000Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





