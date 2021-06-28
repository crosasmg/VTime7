<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mclsProvider As eClaim.Tab_Provider

Dim mclsClient As eClient.Client

Dim mobjGrid As eFunctions.Grid


'% insDefineHeader(): Definición del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeGroupColumnCaption"), "cbeGroup", "Table642", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeGroupColumnToolTip"))
		Call .AddDateColumn(1, GetLocalResourceObject("tcdInpdateColumnCaption"), "tcdInpdate",  ,  , GetLocalResourceObject("tcdInpdateColumnToolTip"))
		Call .AddDateColumn(2, GetLocalResourceObject("tcdOutdateColumnCaption"), "tcdOutdate",  ,  , GetLocalResourceObject("tcdOutdateColumnToolTip"))
		Call .AddHiddenColumn("blnSel", "")
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "MSI019"
		.Codisp = "MSI019_K"
		.sCodisplPage = "MSI019"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
		.Top = 50
		.Height = 300
		.Width = 400
		.MoveRecordScript = "insDisabledField();"
		.sEditRecordParam = "nProvider=" & Request.QueryString.Item("nProvider")
		.Columns("cbeGroup").EditRecord = False
		.Columns("cbeGroup").Disabled = True
		If Request.QueryString.Item("Type") = "PopUp" Then
			If CBool(Request.QueryString.Item("Sel")) Then
				.Columns("tcdOutdate").Disabled = True
			Else
				.Columns("tcdInpdate").Disabled = True
			End If
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMSI019_K(): Se cargan los Valores del Grid
'------------------------------------------------------------------------------
Private Sub insPreMSI019_K()
	'------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolTab_Prov_Groups As eClaim.Tab_Prov_Groups
	Dim lclsTab_Prov_Group As eClaim.Tab_Prov_Group
	lintIndex = 0
	
	lclsTab_Prov_Group = New eClaim.Tab_Prov_Group
	lcolTab_Prov_Groups = New eClaim.Tab_Prov_Groups
	If lcolTab_Prov_Groups.Find(mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsTab_Prov_Group In lcolTab_Prov_Groups
			mobjGrid.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ",""nProvider=" & Request.QueryString.Item("nProvider") & """);"
			lintIndex = lintIndex + 1
			With mobjGrid
				If lclsTab_Prov_Group.dInpdate <> eRemoteDB.Constants.dtmNull And lclsTab_Prov_Group.dOutdate = eRemoteDB.Constants.dtmNull Then
					.Columns("Sel").Checked = CShort("1")
					.Columns("cbeGroup").DefValue = CStr(lclsTab_Prov_Group.nProv_group)
					.Columns("tcdInpdate").DefValue = CStr(lclsTab_Prov_Group.dInpdate)
					.Columns("tcdOutdate").DefValue = CStr(lclsTab_Prov_Group.dOutdate)
					.Columns("blnSel").DefValue = CStr(False)
				Else
					.Columns("Sel").Checked = CShort("2")
					.Columns("cbeGroup").DefValue = CStr(lclsTab_Prov_Group.nProv_group)
					.Columns("tcdInpdate").DefValue = CStr(lclsTab_Prov_Group.dInpdate)
					.Columns("tcdOutdate").DefValue = CStr(lclsTab_Prov_Group.dOutdate)
					.Columns("blnSel").DefValue = CStr(True)
				End If
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsTab_Prov_Group
	End If
	Response.Write(mobjGrid.closeTable())
	lcolTab_Prov_Groups = Nothing
	lclsTab_Prov_Group = Nothing
End Sub

'% insPreMSI019_KUpd(): Se realiza el manejo de la ventana PopUp asociada al grid
'------------------------------------------------------------------------------
Private Sub insPreMSI019_KUpd()
	'------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantClaim.aspx", "MSI019", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If Request.QueryString.Item("Type") = "PopUp" Then
			If CBool(Request.QueryString.Item("Sel")) Then
				Response.Write(("<SCRIPT>insCleanDate();</" & "Script>"))
			End If
		End If
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mclsProvider = New eClaim.Tab_Provider
mclsClient = New eClient.Client

If mclsProvider.FindProvider(mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble)) Then
	Call mclsClient.Find(mclsProvider.sClient)
End If

mobjValues.sCodisplPage = "MSI019"
%>
<HTML>
<HEAD>
	<SCRIPT>
	//- Variable para el control de versiones
	    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"
	</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<%=mobjValues.StyleSheet()%>
<SCRIPT> 
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insCheckSelClick: Se llama ventana PopUp para actualizar los datos
//------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex,sQueryString){
//-------------------------------------------------------------------------------------------
    var lstrQueryString = sQueryString + '&sel=' + Field.checked 
    Field.checked = !Field.checked
    EditRecord(lintIndex,nMainAction,'Update',lstrQueryString);
}
//% insCleanDate : limpia el campo fecha de egreso cuando se está seleccionando un grupo
//------------------------------------------------------------------------------------------
function insCleanDate(){
//------------------------------------------------------------------------------------------
	document.forms[0].tcdOutdate.value = '';
}
//% insDisabledField: habilita o deshabilita los campos según el caso
//------------------------------------------------------------------------------------------
function insDisabledField(){
//------------------------------------------------------------------------------------------
	with (document.forms[0]) {
		if (tcdInpdate.value != '' && tcdOutdate.value != ''){
			tcdInpdate.disabled = false;
			btn_tcdInpdate.disabled = false;
			tcdOutdate.disabled = true;
			btn_tcdOutdate.disabled = true;
			tcdOutdate.value = '';
			blnSel.value = 'true';
		}
		else{
			if (tcdInpdate.value != '' && tcdOutdate.value == ''){
				tcdInpdate.disabled = true;
				btn_tcdInpdate.disabled = true;
				tcdOutdate.disabled = false;
				btn_tcdOutdate.disabled = false;
				blnSel.value = 'false';
			}
		    else{
				tcdInpdate.disabled = false;
				btn_tcdInpdate.disabled = false;
				tcdOutdate.disabled = true;
				btn_tcdOutdate.disabled = true;
				blnSel.value = 'true';
			}
		}
	}
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmProvGroup" ACTION="valmantclaim.aspx?sCodispl=MSI019&nProvider=<%=Request.QueryString.Item("nProvider")%>&sel=<%=Request.QueryString.Item("Sel")%>">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=101809><%= GetLocalResourceObject("tcnProviderCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tcnProvider", 5, Request.QueryString.Item("nProvider"),  ,  , True)%></TD>
            <TD><%=mobjValues.TextControl("tctProviderName", 40, mclsClient.sCliename,  ,  , True)%></TD>
        </TR>
    </TABLE>
    <BR>
<%
Response.Write(mobjValues.ShowWindowsName("MSI019"))
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMSI019_KUpd()
Else
	Call insPreMSI019_K()
End If
If Request.QueryString.Item("Type") <> "PopUp" Then
	
	With Response
		.Write("<TABLE>")
		.Write("<TR>")
		.Write("<TD>")
		.Write(mobjValues.ButtonAbout("MSI019"))
		.Write("</TD>")
		.Write("<TD>")
		.Write(mobjValues.ButtonHelp("MSI019"))
		.Write("</TD>")
		.Write("<TD WIDTH=""95%""></TD>")
		.Write("<TD ALIGN=""RIGHT"">")
		mobjValues.ActionQuery = False
		.Write(mobjValues.ButtonAcceptCancel("window.close();",  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
		.Write("</TD>")
		.Write("</TR>")
		.Write("<TR>")
		.Write("<TD></TD>")
		.Write("<TD COLSPAN=""2"">")
		.Write(mobjValues.BeginPageButton)
		.Write("</TD>")
		.Write("<TD></TD>")
		.Write("</TR>")
		.Write("</TABLE>")
		
	End With
End If
%>
</FORM>
</BODY>
</HTML>
<%
mclsProvider = Nothing
mclsClient = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>




