<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid



    '**% insDefineHeader: This function defined the GRID fields.
    '% insDefineHeader: Configura los datos del grid.
    '--------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------

        '**+ The columns of the GRID are defined
        '+ Se definen las columnas del grid    

        With mobjGrid.Columns
            If Request.QueryString.Item("Action") = "Add" Then
                .AddNumericColumn(0, GetLocalResourceObject("tcnFundsColumnCaption"), "tcnFunds", 4, CStr(0),  , GetLocalResourceObject("tcnFundsColumnToolTip"))
            Else
                .AddNumericColumn(0, GetLocalResourceObject("tcnFundsColumnCaption"), "tcnFunds", 4, CStr(0),  , GetLocalResourceObject("tcnFundsColumnToolTip"),  ,  ,  ,  ,  , True)
            End If
            .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
            .AddNumericColumn(0, GetLocalResourceObject("tcnQuan_minColumnCaption"), "tcnQuan_min", 22, CStr(0),  , GetLocalResourceObject("tcnQuan_minColumnToolTip"), True, 6)
            .AddNumericColumn(0, GetLocalResourceObject("tcnQuan_maxColumnCaption"), "tcnQuan_max", 22, CStr(0),  , GetLocalResourceObject("tcnQuan_maxColumnToolTip"), True, 6)
            .AddNumericColumn(0, GetLocalResourceObject("tcnQuan_availColumnCaption"), "tcnQuan_avail", 22, CStr(0),  , GetLocalResourceObject("tcnQuan_availColumnToolTip"), True, 6)
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
        End With

        '**+ The properties of the GRID are defined
        '+ Se definen las propiedades generales del grid

        With mobjGrid
            .Codispl = "MVI003"
            .sCodisplPage = "MVI003"
            mobjValues.ActionQuery = Session("bQuery")

            If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
                .AddButton = True
                .DeleteButton = True

                .Columns("cbeStatregt").TypeList = 2
                .Columns("cbeStatregt").List = CStr(2)

                .Columns("Sel").GridVisible = True
                .Columns("Sel").Title = "Sel"
                .Columns("tctDescript").EditRecord = True
                .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
                .sDelRecordParam = "nFunds='+ marrArray[lintIndex].tcnFunds + '"
            Else
                .bOnlyForQuery = True
            End If

            .Height = 290
            .Width = 350

            '**+ Continue if the check mark is checked
            '+ Permite continuar si el check está marcado        

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

        If lcolFund_invs.Find(mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
            With mobjGrid
                For Each lclsFund_inv In lcolFund_invs
                    .Columns("tcnFunds").DefValue = CStr(lclsFund_inv.nFunds)
                    .Columns("tctDescript").DefValue = lclsFund_inv.sDescript
                    .Columns("tcnQuan_min").DefValue = CStr(lclsFund_inv.nQuan_min)
                    .Columns("tcnQuan_max").DefValue = CStr(lclsFund_inv.nQuan_max)
                    .Columns("tcnQuan_avail").DefValue = CStr(lclsFund_inv.nQuan_avail)
                    .Columns("cbeStatregt").DefValue = lclsFund_inv.sStatregt

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
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mobjGrid = New eFunctions.Grid
End With
mobjValues.sCodisplPage = "MVI003"
%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
    <HEAD>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>

//**+ For the Source Safe control "DO NOT REMOVE"
//+ Para Control de Versiones "NO REMOVER"

	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:10 $"
</SCRIPT>

    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "MVI003.aspx"))
		.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	End If
End With

mobjMenu = Nothing%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmDBVehicle" ACTION="valMantNoTraLife.aspx?mode=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
            <%Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMVI003()
Else
	Call insPreMVI003Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing%>
        </FORM>
    </BODY>
</HTML>





