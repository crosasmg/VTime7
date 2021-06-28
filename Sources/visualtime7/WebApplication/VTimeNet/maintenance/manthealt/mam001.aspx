<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	Response.Write(mobjValues.ShowWindowsName("MAM001"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(104860, GetLocalResourceObject("valIllnessColumnCaption"), "valIllness", "Tab_am_ill", 2,  ,  ,  ,  ,  ,  ,  , 8, GetLocalResourceObject("valIllnessColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		Call .AddNumericColumn(104861, GetLocalResourceObject("tcnLimit_perColumnCaption"), "tcnLimit_per", 5, "", False, GetLocalResourceObject("tcnLimit_perColumnToolTip"), False, 2,  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = "MAM001"
		.Codisp = "MAM001"
		.sCodisplPage = "MAM001"
		.Top = 100
		.Height = 192
		.Width = 400
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valIllness").EditRecord = True
		.Columns("valIllness").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nCover=" & Request.QueryString.Item("nCover") & "&nPay_concept=" & Request.QueryString.Item("nPay_concept") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&sIllness=' + marrArray[lintIndex].valIllness + '"
		
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nCover=" & Request.QueryString.Item("nCover") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nPay_concept=" & Request.QueryString.Item("nPay_concept")
		
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAM001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMAM001()
	'------------------------------------------------------------------------------
	Dim lcoltab_am_lims As eBranches.tab_am_lims
	Dim lclstab_am_lim As eBranches.tab_am_lim
	
	'    Response.Write mobjValues.ShowWindowsName(Request.QueryString("sCodispl"))
	lclstab_am_lim = New eBranches.tab_am_lim
	lcoltab_am_lims = New eBranches.tab_am_lims
	
	With mobjGrid
		If lcoltab_am_lims.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPay_concept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			For	Each lclstab_am_lim In lcoltab_am_lims
				.Columns("valIllness").DefValue = lclstab_am_lim.sIllness
				.Columns("tcnLimit_per").DefValue = CStr(lclstab_am_lim.nLimit_per)
				Response.Write(mobjGrid.DoRow())
			Next lclstab_am_lim
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclstab_am_lim = Nothing
	lcoltab_am_lims = Nothing
End Sub

'% insPreMAM001Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMAM001Upd()
	'------------------------------------------------------------------------------
	Dim lclstab_am_lim As eBranches.tab_am_lim
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclstab_am_lim = New eBranches.tab_am_lim
			Call lclstab_am_lim.insPostMAM001(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPay_concept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sIllness"), 0)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantHealt.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclstab_am_lim = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
mobjValues.sCodisplPage = "MAM001"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:02 $|$$Author: Nvaplat61 $"
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MAM001", "MAM001.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle("MAM001"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMAM001" ACTION="valMantHealt.aspx?sZone=2">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAM001()
Else
	Call insPreMAM001Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





