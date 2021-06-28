<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página.

Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú.

Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página.

Dim mclsDisc_perc_year As eBranches.Disc_perc_year
Dim mcolDisc_perc_years As eBranches.Disc_perc_years


'% insDefineHeader: Se definen las propiedades del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid.
	
	
	With mobjGrid.Columns
		'+ Estructura del GRID modificada debido a cambios en el funcional de la transacción - ACM - 06/08/2003
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_iniColumnCaption"), "tcnAge_ini", 4, vbNullString,  , GetLocalResourceObject("tcnAge_iniColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_endColumnCaption"), "tcnAge_end", 4, vbNullString,  , GetLocalResourceObject("tcnAge_endColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonth_initColumnCaption"), "tcnMonth_init", 5, vbNullString,  , GetLocalResourceObject("tcnMonth_initColumnToolTip"), True, 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonth_endColumnCaption"), "tcnMonth_end", 5, vbNullString,  , GetLocalResourceObject("tcnMonth_endColumnToolTip"), True, 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDisc_percentageColumnCaption"), "tcnDisc_percentage", 5, vbNullString,  , GetLocalResourceObject("tcnDisc_percentageColumnToolTip"), True, 2)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcndisc_perc_year_excColumnCaption"), "tcndisc_perc_year_exc", 5, vbNullString, , GetLocalResourceObject("tcndisc_perc_year_excColumnCaptionToolTip"), True, 2)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnDisc_perc_year_nrecColumnCaption"), "tcnDisc_perc_year_nrec", 5, vbNullString, , GetLocalResourceObject("tcnDisc_perc_year_nrecColumnCaptionToolTip"), True, 2)
         
        End With
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "MVI8003"
		.Codisp = "MVI8003"
		.sCodisplPage = "MVI8003"
		.ActionQuery = mobjValues.ActionQuery
		If Request.QueryString.Item("Action") <> "Del" Then
			.Top = 250
			.Height = 320
			.Width = 320
		Else
			.WidthDelete = 500
		End If
		.Splits_Renamed.AddSplit(0, "", 2)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		
		.Columns("tcnAge_ini").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nRole=" & Request.QueryString.Item("nRole")
		
		.sDelRecordParam = .sEditRecordParam & "&tcnAge_ini=' + marrArray[lintIndex].tcnAge_ini+ '" & "&tcnAge_end=' + marrArray[lintIndex].tcnAge_end+ '" & "&tcnMonth_init=' + marrArray[lintIndex].tcnMonth_init+ '" & "&tcnMonth_end=' + marrArray[lintIndex].tcnMonth_end+ '" & "&tcnDisc_percentage=' + marrArray[lintIndex].tcnDisc_percentage+ '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI8003: Se realiza el manejo del grid.
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8003()
	'--------------------------------------------------------------------------------------------
        If mcolDisc_perc_years.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTyperisk"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
            For Each mclsDisc_perc_year In mcolDisc_perc_years
                With mobjGrid
                    .Columns("tcnAge_ini").DefValue = CStr(mclsDisc_perc_year.nAge_ini)
                    .Columns("tcnAge_end").DefValue = CStr(mclsDisc_perc_year.nAge_End)
                    .Columns("tcnMonth_init").DefValue = CStr(mclsDisc_perc_year.nMonth_init)
                    .Columns("tcnMonth_end").DefValue = CStr(mclsDisc_perc_year.nMonth_end)
                    .Columns("tcnDisc_percentage").DefValue = CStr(mclsDisc_perc_year.nDisc_perc_year)
                    
                    .Columns("tcndisc_perc_year_exc").DefValue = CStr(mclsDisc_perc_year.ndisc_perc_year_exc)
                    .Columns("tcnDisc_perc_year_nrec").DefValue = CStr(mclsDisc_perc_year.nDisc_perc_year_nrec)
                    
                    Response.Write(.DoRow)
                End With
            Next mclsDisc_perc_year
        End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVI8003Upd: Se realiza el manejo de la ventana PopUp asociada al grid.
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8003Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call mclsDisc_perc_year.insPostMVI8003(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnMonth_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnMonth_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnDisc_percentage"), eFunctions.Values.eTypeData.etdDouble, True))
			
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantNoTraLife.aspx", "MVI8003", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsDisc_perc_year = New eBranches.Disc_perc_year
mcolDisc_perc_years = New eBranches.Disc_perc_years

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI8003"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:10 $|$$Author: Nvaplat61 $"
</SCRIPT>

<%
Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI8003", "MVI8003.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI8003" ACTION="valMantNoTraLife.aspx?sMode=2">

<%
Response.Write(mobjValues.ShowWindowsName("MVI8003", "Descuentos porcentuales"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI8003Upd()
Else
	Call insPreMVI8003()
End If

mobjGrid = Nothing
mobjMenu = Nothing
mobjValues = Nothing
mclsDisc_perc_year = Nothing
mcolDisc_perc_years = Nothing
%>
</FORM> 
</BODY>
</HTML>





