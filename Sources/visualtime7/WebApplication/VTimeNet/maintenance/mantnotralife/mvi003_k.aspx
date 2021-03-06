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
    Dim mcolTab_req_docs As Object

    '**% insDefineHeader: This function defined the GRID fields.
    '% insDefineHeader: Configura los datos del grid.
    '--------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------

        '**+ The columns of the GRID are defined
        '+ Se definen las columnas del grid    

        With mobjGrid.Columns
            If Request.QueryString.Item("Action") = "Add" Then
                .AddNumericColumn(0, GetLocalResourceObject("tcnFundsColumnCaption"), "tcnFunds", 4, CStr(0), , GetLocalResourceObject("tcnFundsColumnToolTip"))
            Else
                .AddNumericColumn(0, GetLocalResourceObject("tcnFundsColumnCaption"), "tcnFunds", 4, CStr(0), , GetLocalResourceObject("tcnFundsColumnToolTip"), , , , , , True)
            End If
            .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "", , GetLocalResourceObject("tctDescriptColumnToolTip"))
            .AddNumericColumn(0, GetLocalResourceObject("tcnQuan_minColumnCaption"), "tcnQuan_min", 22, CStr(0), , GetLocalResourceObject("tcnQuan_minColumnToolTip"), True, 6)
            .AddNumericColumn(0, GetLocalResourceObject("tcnQuan_maxColumnCaption"), "tcnQuan_max", 22, CStr(0), , GetLocalResourceObject("tcnQuan_maxColumnToolTip"), True, 6)
            .AddNumericColumn(0, GetLocalResourceObject("tcnQuan_availColumnCaption"), "tcnQuan_avail", 22, CStr(0), , GetLocalResourceObject("tcnQuan_availColumnToolTip"), True, 6)
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeStatregtColumnToolTip"))
            .AddPossiblesColumn(0, GetLocalResourceObject("cbenCountryColumnCaption"), "cbenCountry", "Table66", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbenCountryColumnToolTip"))
            .AddNumericColumn(0, GetLocalResourceObject("tcnSeries_ColumnCaption"), "tcnSeries", 10, CStr(0), , GetLocalResourceObject("tcnSeries_ColumnToolTip"), True, )
            .AddNumericColumn(0, GetLocalResourceObject("tcnRun_ColumnCaption"), "tcnRun", 10, CStr(0), , GetLocalResourceObject("tcnRun_ColumnToolTip"), True, )
            .AddDateColumn(0, GetLocalResourceObject("tcdDinpdate_ColumnCaption"), "tcdDinpdate", , , GetLocalResourceObject("tcdDinpdate_ColumnToolTip"), , , , )
            .AddTextColumn(0, GetLocalResourceObject("tctRoutine_ColumnCaption"), "tctRoutine", 30, "", , GetLocalResourceObject("tctRoutine_ColumnToolTip"))
            .AddCheckColumn(0, GetLocalResourceObject("chkGuaranteed_ColumnCaption"), "chkGuaranteed", "", , , , Request.QueryString("Type") <> "PopUp")
            .AddTextColumn(0, GetLocalResourceObject("tctTicker_ColumnCaption"), "tctTicker", 15, "", , GetLocalResourceObject("tctTicker_ColumnToolTip"))
            .AddTextColumn(0, GetLocalResourceObject("tctISIN_Code_ColumnCaption"), "tctISIN_Code", 15, "", , GetLocalResourceObject("tctISIN_Code_ColumnTollTip"))


        End With

        '**+ The properties of the GRID are defined
        '+ Se definen las propiedades generales del grid

        With mobjGrid
            .Codispl = "MVI003"
            .Codisp = "MVI003_K"
            .sCodisplPage = "MVI003"
            If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
                mobjGrid.ActionQuery = True
                mobjGrid.Columns("Sel").GridVisible = False
            Else
                .Columns("tctDescript").EditRecord = True
            End If
            .Top = 200
            .Left = 300
            .Height = 500
            .Width = 350
            .sDelRecordParam = "nFunds='+ marrArray[lintIndex].tcnFunds + '"
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '**% insPreMVI003: Get the information of the investment funds
    '% insPreMVI003: Obtiene los datos de los fondos de inversión
    '--------------------------------------------------------------------------------------
    Private Sub insPreMVI003()
        '--------------------------------------------------------------------------------------
        Dim lclsFund_inv As ePolicy.Fund_inv
        Dim lcolFund_invs As ePolicy.Fund_invs

        With Server
            lclsFund_inv = New ePolicy.Fund_inv
            lcolFund_invs = New ePolicy.Fund_invs
        End With

        '**+ Search the investment funds related to the plan if the action is different an adding
        '+ Se buscan los fondos de inversión asociados al plan siempre y cuando la acción sea
        '+ diferente a una inserción.

        If lcolFund_invs.Find(True) Then
            With mobjGrid
                For Each lclsFund_inv In lcolFund_invs
                    .Columns("tcnFunds").DefValue = CStr(lclsFund_inv.nFunds)
                    .Columns("tctDescript").DefValue = lclsFund_inv.sDescript
                    .Columns("tcnQuan_min").DefValue = CStr(lclsFund_inv.nQuan_min)
                    .Columns("tcnQuan_max").DefValue = CStr(lclsFund_inv.nQuan_max)
                    .Columns("tcnQuan_avail").DefValue = CStr(lclsFund_inv.nQuan_avail)
                    .Columns("cbeStatregt").DefValue = lclsFund_inv.sStatregt
                    .Columns("cbenCountry").DefValue = CStr(lclsFund_inv.nCountry)
                    .Columns("tcnSeries").DefValue = CStr(lclsFund_inv.nSeries)
                    .Columns("tcnRun").DefValue = CStr(lclsFund_inv.nRun)
                    .Columns("tcdDinpdate").DefValue = CStr(lclsFund_inv.dInpdate)
                    .Columns("tctRoutine").DefValue = lclsFund_inv.sRoutine
                    .Columns("chkGuaranteed").DefValue = lclsFund_inv.sGuaranteed
                    If lclsFund_inv.sGuaranteed = "1" Then
                        .Columns("chkGuaranteed").Checked = 1
                    Else
                        .Columns("chkGuaranteed").Checked = 2
                    End If
                    .Columns("tctTicker").DefValue = lclsFund_inv.sTicker
                    .Columns("tctISIN_code").DefValue = lclsFund_inv.sISIN_code
                    Response.Write(.DoRow)
                Next lclsFund_inv
            End With
        End If

        Response.Write(mobjGrid.closeTable)

        lcolFund_invs = Nothing
        lclsFund_inv = Nothing
    End Sub

    '**% insPreMVI003Upd: This function allows to make the reading of the table.
    '% insPreMVI003Upd: Esta función permite realizar la lectura de la tabla.
    '------------------------------------------------------------------------------
    Private Sub insPreMVI003Upd()
        '------------------------------------------------------------------------------
        Dim lclsFund_inv As ePolicy.Fund_inv
        Dim lclsErrors As eFunctions.Errors
        With Server
            lclsFund_inv = New ePolicy.Fund_inv
            lclsErrors = New eFunctions.Errors
        End With
        If Request.QueryString.Item("Action") = "Del" Then
            If Not lclsFund_inv.FindFunds(CInt(Request.QueryString.Item("nFunds"))) Then
                With lclsFund_inv
                    .nFunds = mobjValues.StringToType(Request.QueryString.Item("nFunds"), eFunctions.Values.eTypeData.etdDouble)
                    .Delete()
                    Response.Write(mobjValues.ConfirmDelete())
                End With
            Else
                lclsErrors.Highlighted = True
                Response.Write(lclsErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 10047,  ,  ,  , True))
            End If
        End If
        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
        lclsFund_inv = Nothing
        lclsErrors = Nothing
    End Sub
</script>
<%
    Response.Expires = -1
    mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
    mobjValues.sCodisplPage = "MVI003"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
  

<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
	   <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\mantnotralife\VTime\Scripts\tmenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%	
End If
%>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MVI003_K.aspx", 1, ""))
	Response.Write("<BR></BR>")
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 31/10/03 11:38 $"
    
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insPreZone: Define ubicacion de documento
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
 }
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDBVehicle" ACTION="valMantNoTraLife.aspx?mode=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("MVI003"))

mobjGrid = New eFunctions.Grid
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI003Upd()
Else
	Call insPreMVI003()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>






