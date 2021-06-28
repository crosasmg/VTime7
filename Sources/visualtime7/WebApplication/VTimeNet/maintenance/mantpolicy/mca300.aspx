<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eBranches" %>
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
        
	mobjGrid.sCodisplPage = "MCA300"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCodeColumnCaption"), "tcnCode", 5, "", , GetLocalResourceObject("tcnCodeColumnToolTip"))
            Call .AddDateColumn(0, GetLocalResourceObject("tcdAssign_dateCaption"), "dEffecdate", mobjValues.StringToType(Session("dAssign_date_MCA300"), eFunctions.Values.eTypeData.etdDate), , GetLocalResourceObject("tcdAssign_dateToolTip"))
            Call .AddTextColumn(0, GetLocalResourceObject("tcnDescriptionColumnCaption"), "tcnDesc", 30, "", , GetLocalResourceObject("tcnDescriptionColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", (5.2), "", , GetLocalResourceObject("tcnRateColumnToolTip"), , 2)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", (18.6), "", , GetLocalResourceObject("tcnAmountColumnToolTip"), , 6)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnMinimumColumnCaption"), "tcnMin", (18.6), "", , GetLocalResourceObject("tcnMinimumColumnToolTip"), , 6)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnMaximumColumnCaption"), "tcnMax", (18.6), "", , GetLocalResourceObject("tcnMaximumColumnToolTip"), , 6)
            
        End With
	
	'+ Se definen las propiedades generales del grid
	
        With mobjGrid
            .Codispl = "MCA300"
            .ActionQuery = mobjValues.ActionQuery
            .Height = 350
            .Width = 480
            .AddButton = True
            .DeleteButton = True
            
            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)  Then
                .ActionQuery = True
            End If
		
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Not .ActionQuery
            .sDelRecordParam = "&nCodcost='+ marrArray[lintIndex].tcnCode + '"
            .Columns("tcnCode").EditRecord = True
            If Request.QueryString.Item("Action") = "Update" Then
                .Columns("tcnCode").Disabled = True
                .Columns("dEffecdate").Disabled = True
            End If
            If Request.QueryString.Item("Action") = "Add" Then

                .Columns("dEffecdate").Disabled = True
            End If
        End With
End Sub

'% insPreMCA300: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
    Private Sub insPreMCA300()
        '--------------------------------------------------------------------------------------------

        Dim lclsTab_Rescost As eBranches.Tab_rescost
        Dim lcolTab_Rescosts As eBranches.Tab_rescosts
        
        lcolTab_Rescosts = New eBranches.Tab_rescosts
	
        If lcolTab_Rescosts.Find(mobjValues.StringToType(Session("dAssign_date_MCA300"), eFunctions.Values.eTypeData.etdDate)) Then
		
            For Each lclsTab_Rescost In lcolTab_Rescosts
                With mobjGrid
                    .Columns("tcnCode").DefValue = lclsTab_Rescost.nCodcost
                    .Columns("dEffecdate").DefValue = lclsTab_Rescost.dEffecdate
                    .Columns("tcnDesc").DefValue = lclsTab_Rescost.sDescript
                    .Columns("tcnRate").DefValue = lclsTab_Rescost.nRate
                    .Columns("cbeCurrency").DefValue = lclsTab_Rescost.nCurrency
                    .Columns("tcnAmount").DefValue = lclsTab_Rescost.nAmount
                    .Columns("tcnMin").DefValue = lclsTab_Rescost.nMinimum
                    .Columns("tcnMax").DefValue = lclsTab_Rescost.nMaximum
                    Response.Write(.DoRow)
                End With
            Next lclsTab_Rescost
        End If
	
        Response.Write(mobjGrid.closeTable())
        '	End With
	
        mcolClass = Nothing
    End Sub
    
    '% insPreMCA300Upd: Gestiona lo relacionado a la actualización de un registro del Grid
    '------------------------------------------------------------------------------------
    Private Sub insPreMCA300Upd()
        '------------------------------------------------------------------------------------
        Dim lclsTab_Rescost As eBranches.Tab_rescost
        lclsTab_Rescost = New eBranches.Tab_rescost
	
        With Request
		
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                lclsTab_Rescost = New eBranches.Tab_rescost
                Call lclsTab_Rescost.InsPostMCA300Upd("Del", mobjValues.StringToType(.QueryString.Item("nCodcost"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("dAssign_date_MCA300"), eFunctions.Values.eTypeData.etdDate), "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dblNull, eRemoteDB.Constants.dblNull, eRemoteDB.Constants.dblNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger))
            End If
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantPolicy.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
	
        lclsTab_Rescost = Nothing
    End Sub


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MCA300"
mobjMenu = New eFunctions.Menues

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 11/05/04 19:20 $|$$Author: Nvaplat7 $"

</SCRIPT>
<!-- aca va el include -->

<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MCA300", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MCA300" ACTION="ValMantPolicy.aspx?x=1">
    <%Response.Write(mobjValues.ShowWindowsName("MCA300", Request.QueryString.Item("sWindowDescript")))

        Call insDefineHeader()
        
        If Request.QueryString.Item("Type") <> "PopUp" Then
            Call insPreMCA300()
        Else
            Call insPreMCA300Upd()
        End If

mobjGrid = Nothing
mobjValues = Nothing
%>

</FORM> 
</BODY>
</HTML>






