<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddHiddenColumn("hddnExist", "")
		Call .AddHiddenColumn("hddsSel", "")
		Call .AddHiddenColumn("hddBranch", "")
		Call .AddHiddenColumn("hddProduct", "")
		Call .AddHiddenColumn("hddModulec", "")
		Call .AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "",  , GetLocalResourceObject("tctBranchColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 30, "",  , GetLocalResourceObject("tctProductColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctModulecColumnCaption"), "tctModulec", 30, "",  , GetLocalResourceObject("tctModulecColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVA646D"
		.sCodisplPage = "MVA646D"
		.AddButton = False
		.DeleteButton = False
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% InsPreMVA646D: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreMVA646D()
	'--------------------------------------------------------------------------------------------
	Dim lcolPlan_agre As eBranches.Plan_agres
	Dim lclsPlan_agre As Object
	Dim lintIndex As Short
	
	lcolPlan_agre = New eBranches.Plan_agres
	lintIndex = 0
	If lcolPlan_agre.Find(mobjValues.StringToType(Session("nAgreement"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsPlan_agre In lcolPlan_agre
			With mobjGrid
				.Columns("hddnExist").DefValue = lclsPlan_agre.sSel
				.Columns("hddsSel").DefValue = lclsPlan_agre.sSel
				.Columns("Sel").Checked = lclsPlan_agre.sSel
				.Columns("hddBranch").DefValue = lclsPlan_agre.nBranch
				.Columns("hddProduct").DefValue = lclsPlan_agre.nProduct
				.Columns("hddModulec").DefValue = lclsPlan_agre.nModulec
				.Columns("tctBranch").DefValue = lclsPlan_agre.sDesBranch
				.Columns("tctProduct").DefValue = lclsPlan_agre.sDesProduct
				.Columns("tctModulec").DefValue = lclsPlan_agre.sDesModulec
				.Columns("Sel").OnClick = "InsChangeSel(this," & lintIndex & ")"
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsPlan_agre
	End If
	Response.Write(mobjGrid.closeTable())
	lcolPlan_agre = Nothing
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA646D"
%>
<html>
<head>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVA646D", "MVA646D.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</head>
<script>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:39 $|$$Author: Nvaplat61 $"

//%InsChangeSel : Cambia el indicador de seleción
//-------------------------------------------------------------
function InsChangeSel(Field, nIndex){
//-------------------------------------------------------------
    with (self.document.forms[0]) {
        try{
            hddsSel[nIndex].value = (Field.checked?1:2)
        }
        catch(e) {
            hddsSel.value= (Field.checked?1:2);
        }

    }
}
</script>
<body ONUNLOAD="closeWindows();">
<form METHOD="POST" NAME="MVA646D" ACTION="ValAgreementSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("MVA646D"))
Response.Write("<BR>")
Call insDefineHeader()
Call InsPreMVA646D()
mobjValues = Nothing
%>
</form> 
</body>
</html>




