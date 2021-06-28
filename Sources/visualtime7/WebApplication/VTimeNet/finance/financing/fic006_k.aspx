<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "fic006_k"
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctContratColumnCaption"), "tctContrat", 10, vbNullString,  , GetLocalResourceObject("tctContratColumnCaption"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctClientColumnCaption"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctClienameColumnCaption"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDateColumnCaption"), "tctDate", 15, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctDateColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStat_contrColumnCaption"), "cbeStat_contr", "Table278", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStat_contrColumnCaption"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "FIC006_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 250
		.Width = 400
		.Top = 10
		.Left = 10
	End With
End Sub

'% insPreFIC006: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreFIC006()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanceCo As eFinance.financeCO
	Dim lcolFinanceCos As eFinance.FinanceCos
	Dim lintCount As Short
	Dim lclsacc As Object
	
	lclsFinanceCo = New eFinance.financeCO
	
	lcolFinanceCos = New eFinance.FinanceCos
	
	If Not IsNothing(Request.QueryString.Item("Sql")) Then
		If lcolFinanceCos.insConstructFinance_co(Session("sContrat"), Session("sClient"), Session("sCliename"), Session("sEffecDate"), mobjValues.StringToType(Session("nStat_contr"), eFunctions.Values.eTypeData.etdDouble)) Then
			lintCount = 0
			
			For	Each lclsFinanceCo In lcolFinanceCos
				With lclsFinanceCo
					mobjGrid.Columns("tctContrat").DefValue = CStr(.nContrat)
					mobjGrid.Columns("tctClient").DefValue = .sClient
					mobjGrid.Columns("tctCliename").DefValue = .sClientName
					mobjGrid.Columns("tctDate").DefValue = CStr(.dEffecdate)
					mobjGrid.Columns("cbeStat_contr").DefValue = CStr(.nStat_contr)
					
					Response.Write(mobjGrid.DoRow())
				End With
				
				lintCount = lintCount + 1
				
				If lintCount = 200 Then
					Exit For
				End If
			Next lclsFinanceCo
		End If
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lclsFinanceCo = Nothing
	lcolFinanceCos = Nothing
End Sub

'-----------------------------------------------------------------------------
Private Sub insPreFIC006Upd()
	'-----------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValFinanceQue.aspx", "FIC006", Request.QueryString.Item("nMainAction"), False, CShort(Request.QueryString.Item("nIndex"))))
End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "fic006_k"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
	<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\finance\financing\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%	
End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


    <%'=mobjValues.StyleSheet()%>
    <SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    EditRecord(-1, nMainAction,'Add')
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("FIC006"))
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "FIC006.aspx"))
		.Write(mobjMenu.MakeMenu("FIC006", "FIC006_k.aspx", 2, ""))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="FIC006_k" ACTION="ValFinanceQue.aspx?Zone=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName("FIC006"))

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreFIC006()
Else
	Call insPreFIC006Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>




