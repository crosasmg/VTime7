<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Object for the handling of the general functions of load of values
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de Grid    
Dim mobjGrid As eFunctions.Grid

'- Object for the handling of the areas of the page
'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

Dim lstrString As Object
Dim mstrType_Charge As String


'%insDefineHeader. Definición encabezado del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lclsTables As eFunctions.Tables
	Dim lintCount As Object
	Dim sType_move As String
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "VIL1413"
	
	lclsTables = New eFunctions.Tables
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddCheckColumn(0, GetLocalResourceObject("chkSellColumnCaption"), "chkSell", vbNullString,  ,  ,  , False)
		Call .addTextColumn(0, GetLocalResourceObject("tctType_moveColumnCaption"), "tctType_move", 30, "",  , GetLocalResourceObject("tctType_moveColumnToolTip"))
		Call .AddHiddenColumn("hddnType_move", "")
		Call .AddCheckColumn(0, GetLocalResourceObject("chkSell2ColumnCaption"), "chkSell2", vbNullString,  ,  ,  , False)
		Call .addTextColumn(0, GetLocalResourceObject("tctType_move2ColumnCaption"), "tctType_move2", 30, "",  , GetLocalResourceObject("tctType_move2ColumnToolTip"))
		Call .AddHiddenColumn("hddnType_move2", "")
		Call .AddHiddenColumn("hddsSel", vbNullString)
		Call .AddHiddenColumn("hddsSel2", vbNullString)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "VIL1413"
		.AddButton = False
		.DeleteButton = False
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = False
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
	
	lclsTables.Parameters.Add("sType_charge", mstrType_Charge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	lclsTables.reaTable("TABTYPE_MOVE")
	
	lintCount = 0
	sType_move = "("
	While (Not lclsTables.EOF)
		With mobjGrid
			
			lintCount = lintCount + 1
			.Columns("chkSell").Checked = CShort("1")
			.Columns("hddsSel").Defvalue = CStr(.Columns("chkSell").Checked)
			.Columns("chkSell").OnClick = "insCheckSelClick(this," & CStr(lintCount - 1) & ")"
			
			.Columns("hddnType_move").Defvalue = lclsTables.Fields("nType_move")
			.Columns("tctType_move").Defvalue = lclsTables.Fields("sDescript")
			sType_move = sType_move & .Columns("hddnType_move").Defvalue & ","
			
			lclsTables.NextRecord()
			If (Not lclsTables.EOF) Then
				.Columns("chkSell2").Checked = CShort("1")
				.Columns("hddsSel2").Defvalue = CStr(.Columns("chkSell2").Checked)
				.Columns("chkSell2").OnClick = "insCheckSelClick(this," & CStr(lintCount - 1) & ")"
				
				.Columns("hddnType_move2").Defvalue = lclsTables.Fields("nType_move")
				.Columns("tctType_move2").Defvalue = lclsTables.Fields("sDescript")
				
				sType_move = sType_move & .Columns("hddnType_move2").Defvalue & ","
				
				lclsTables.NextRecord()
			Else
				.Columns("chkSell2").Checked = CShort("2")
				.Columns("hddsSel2").Defvalue = CStr(.Columns("chkSell2").Checked)
				.Columns("chkSell2").Disabled = True
				.Columns("hddnType_move2").Defvalue = " "
				.Columns("tctType_move2").Defvalue = " "
			End If
			
			Response.Write(.DoRow)
		End With
	End While
	Response.Write(mobjValues.HiddenControl("hddnCount", lintCount))
	Response.Write(mobjValues.HiddenControl("hddsType_move", sType_move))
	
	lclsTables = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "VIL1413"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>

//% For the Source Safe control
//% Para control de versiones
//------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 2 $|$$Date: 28/02/06 11:33 $"
//------------------------------------------------------------------------------------------

//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insCancel: It executes necessary routines at the moment for cancelling the page
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}
//% InsChangeValue: 
//------------------------------------------------------------------------------------------
function InsChagenValue(sValue){
//------------------------------------------------------------------------------------------
	var strref
	strref = document.location.href.replace(/&sType_Charge=.*/,'') + '&sType_Charge=' + sValue
	strref = strref.replace(/&nBranch=.*/,'') + '&nBranch=' + self.document.forms[0].cbebranch.value
	strref = strref.replace(/&nProduct=.*/,'') + '&nProduct=' + self.document.forms[0].valproduct.value
	strref = strref.replace(/&nPolicy=.*/,'') + '&nPolicy=' + self.document.forms[0].tcnPolicy.value
	strref = strref.replace(/&dDate_ini=.*/,'') + '&dDate_ini=' + self.document.forms[0].tcdDate_ini.value
	strref = strref.replace(/&dDate_end=.*/,'') + '&dDate_end=' + self.document.forms[0].tcdDate_end.value
	document.location.href = strref
}

//% insCheckSelClick: controla la columna Sel, para mostrar la ventana PopUp    
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
	var lintIndex
	var lstrString

   	lstrString='(';

	if (Field.name == 'chkSell'){
		marrArray[lintIndex].hddsSel = (Field.checked?1:2);}
		
	if (Field.name == 'chkSell2'){
		marrArray[lintIndex].hddsSel2 = (Field.checked?1:2);}
		
   	for(var lintIndex=0; lintIndex<marrArray.length;lintIndex++){

		if (marrArray[lintIndex].hddsSel==1){
			lstrString = lstrString + marrArray[lintIndex].hddnType_move + ',';}

		if (marrArray[lintIndex].hddsSel2==1){
			lstrString = lstrString + marrArray[lintIndex].hddnType_move2 + ',';}		
	}
self.document.forms[0].hddsType_move.value =	lstrString;

}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("VIL1413", "VIL1413_k.aspx", 1, vbNullString))
Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
mobjMenu = Nothing
If IsNothing(Request.QueryString.Item("sType_Charge")) Then
	mstrType_Charge = "1"
Else
	mstrType_Charge = Request.QueryString.Item("sType_Charge")
End If
%>    
</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST" ID="FORM" NAME="VIL1413" ACTION="valpolicyrep.aspx?smode=2">
			<BR><BR>
			<TABLE WIDTH="100%">
				<TR>
					<TD><LABEL><%= GetLocalResourceObject("cbebranchCaption") %></LABEL></TD>
					<TD><%=mobjValues.BranchControl("cbebranch", GetLocalResourceObject("cbebranchToolTip"), Request.QueryString.Item("nBranch"), "valproduct")%></TD>
					<TD>&nbsp;</TD>
					<TD><LABEL><%= GetLocalResourceObject("valproductCaption") %></LABEL></TD>
					<TD><%=mobjValues.ProductControl("valproduct", GetLocalResourceObject("valproductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType,  , Request.QueryString.Item("nProduct"))%></TD>
				</TR>
				<TR>
					<TD><LABEL><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
					<TD><%=mobjValues.PolicyControl("tcnPolicy", GetLocalResourceObject("tcnPolicyToolTip"), "cbebranch", Request.QueryString.Item("nBranch"), "valproduct", Request.QueryString.Item("nProduct"),  , Request.QueryString.Item("nPolicy"),  ,  ,  ,  ,  ,  ,  ,  , False)%></TD>
					<TD COLSPAN=5>&nbsp;</TD>
				</TR>
				<TR>
				    <TD><LABEL><%= GetLocalResourceObject("tcdDate_iniCaption") %></LABEL></TD>
				    <TD><%=mobjValues.DateControl("tcdDate_ini", Request.QueryString.Item("dDate_ini"),  , GetLocalResourceObject("tcdDate_iniToolTip"))%></TD>
				    <TD>&nbsp;</TD>
				    <TD><LABEL><%= GetLocalResourceObject("tcdDate_endCaption") %></LABEL></TD>
				    <TD><%=mobjValues.DateControl("tcdDate_end", Request.QueryString.Item("dDate_end"),  , GetLocalResourceObject("tcdDate_endToolTip"))%></TD>
				</TR>
				<TR>
					<TD><LABEL><%= GetLocalResourceObject("cbeTypeMoveCaption") %></LABEL></TD>
				    <TD><%=mobjValues.ComboControl("cbeTypeMove", "1|Pre-Cargo,2|Post-Cargo,3|Otros cargos,4|Aportes,5|Todo", mstrType_Charge, True,  , GetLocalResourceObject("cbeTypeMoveToolTip"), "InsChagenValue(this.value);")%></TD>
				    <TD>&nbsp;</TD>
				</TR>
			</TABLE>
			<TABLE WIDTH="100%" COLS=4 CLASS=grddata>
				
				<%
Call insDefineHeader()
%>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
<%

mobjGrid = Nothing
mobjValues = Nothing
%>




