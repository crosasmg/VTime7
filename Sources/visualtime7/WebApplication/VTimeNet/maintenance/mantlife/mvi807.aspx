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
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPeriodColumnCaption"), "tcnPeriod", 5, "", True, GetLocalResourceObject("tcnPeriodColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRec_saleColumnCaption"), "tcnRec_sale", 6, "", False, GetLocalResourceObject("tcnRec_saleColumnToolTip"), False, 4,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRec_commColumnCaption"), "tcnRec_comm", 6, "", False, GetLocalResourceObject("tcnRec_commColumnToolTip"), False, 4,  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = "MVI807"
		.Codisp = "MVI807"
		.sCodisplPage = "MVI807"
		.Top = 100
		.Height = 224
		.Width = 240
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnPeriod").EditRecord = True
		.Columns("tcnPeriod").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nPeriod='+ marrArray[lintIndex].tcnPeriod + '"
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMVI807. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMVI807()
	'------------------------------------------------------------------------------
	Dim lcolRes_Costs As eBranches.Res_costs
	Dim lclsRes_Cost As eBranches.Res_cost
	
	lclsRes_Cost = New eBranches.Res_cost
	With Request
		lcolRes_Costs = New eBranches.Res_costs
		With mobjGrid
			If lcolRes_Costs.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				For	Each lclsRes_Cost In lcolRes_Costs
					.Columns("tcnPeriod").DefValue = CStr(lclsRes_Cost.nPeriod)
					.Columns("tcnRec_comm").DefValue = CStr(lclsRes_Cost.nRec_comm)
					.Columns("tcnRec_sale").DefValue = CStr(lclsRes_Cost.nRec_sale)
					Response.Write(mobjGrid.DoRow())
				Next lclsRes_Cost
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsRes_Cost = Nothing
	lcolRes_Costs = Nothing
End Sub

'% insPreMVI807Upd. Se define esta funcion para contruir el contenido de la 
'%                  ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMVI807Upd()
	'------------------------------------------------------------------------------
	Dim lclsRes_Cost As eBranches.Res_cost
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsRes_Cost = New eBranches.Res_cost
			Call lclsRes_Cost.InsPostMVI807(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPeriod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
			lclsRes_Cost = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI807", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "MVI807"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">


<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MVI807", "MVI807.aspx"))
		mobjMenu = Nothing
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="MVI807" ACTION="valMantLife.aspx?nBranch=<%=Request.QueryString.Item("nBranch")%>&nProduct=<%=Request.QueryString.Item("nProduct")%>&dEffecdate=<%=Request.QueryString.Item("dEffecdate")%>">
<%
Response.Write(mobjValues.ShowWindowsName("MVI807"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMVI807()
Else
	Call insPreMVI807Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





