<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'% DefineHeader: se definen las características del grid
'--------------------------------------------------------------------------------------------
Private Sub DefineHeader()
	'--------------------------------------------------------------------------------------------	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddDateColumn(0, GetLocalResourceObject("dtmOperdateColumnCaption"), "dtmOperdate",  ,  , GetLocalResourceObject("dtmOperdateColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_movColumnCaption"), "cbeType_mov", "Table401", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeType_movColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString, False, GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
	End With
	With mobjGrid
		.Codispl = "CA028"
		.Width = 350
		.Height = 370
		.Top = 100
		.DeleteButton = False
		.AddButton = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.ActionQuery = True
	End With
End Sub

'--------------------------------------------------------------------------------------------
Private Sub insPreVI770()
	'--------------------------------------------------------------------------------------------	
	
	' DECLARAR VARIABLES Y HACER LAS LECTURAS RESPECTIVAS PARA TRAER LA DATA - 29/08/2003
	Dim lclsValPolicyTra As ePolicy.ValPolicyTra
	Dim lintCount As Integer
	Dim lblnFind As Boolean
	
	lclsValPolicyTra = New ePolicy.ValPolicyTra
	
	lblnFind = lclsValPolicyTra.insPreVI770(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Today)
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcnQuantityMonthsCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnQuantityMonths", 5, CStr(lclsValPolicyTra.nQMonths),  , GetLocalResourceObject("tcnQuantityMonthsToolTip"), False, 0, True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcnPendingAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPendingAmount", 18, CStr(lclsValPolicyTra.nPending_cost),  , GetLocalResourceObject("tcnPendingAmountToolTip"), True, 6, True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcnPendingAmountLocalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPendingAmountLocal", 18, CStr(lclsValPolicyTra.nCurr_pending_cost),  , GetLocalResourceObject("tcnPendingAmountLocalToolTip"), True, 6, True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcnRequiredAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnRequiredAmount", 18, CStr(lclsValPolicyTra.nRequired_pending_cost),  , GetLocalResourceObject("tcnRequiredAmountToolTip"), True, 6, True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("</FORM>" & vbCrLf)
Response.Write("</BODY>" & vbCrLf)
Response.Write("</HTML>" & vbCrLf)
Response.Write("")

	
	'+ Se arma el objeto GRID con los valores correspondientes a las columnas [APV2] - ACM - 01/09/2003
	If lblnFind Then
		For lintCount = 1 To lclsValPolicyTra.Count_VI770 - 1
			If lclsValPolicyTra.Item_VI770(lintCount) Then
				mobjGrid.Columns("dtmOperdate").DefValue = CStr(lclsValPolicyTra.dOperdate)
				mobjGrid.Columns("cbeType_mov").DefValue = CStr(lclsValPolicyTra.nType_move)
				mobjGrid.Columns("tcnAmount").DefValue = CStr(lclsValPolicyTra.nAmount)
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7700")
'- Variables que contendrán la información que está en las variables de Sesión
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI7700"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
    var mintBranch_j
    var mintProduct_j
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 8/10/03 19:16 $"

</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "VI7700", "VI7700.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
 <BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VI770" ACTION="ValPolicyTra.aspx?x=1&nTransacion=">
    	<%=mobjValues.ShowWindowsName("VI7700", Request.QueryString.Item("sWindowDescript"))%>
<%
Call DefineHeader()
Call insPreVI770()

'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("VI770")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





