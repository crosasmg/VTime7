<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenues As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen las propiedades generales del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	With mobjGrid.Columns
		Call .AddDateColumn(40025, GetLocalResourceObject("tcdEffecdatesColumnCaption"), "tcdEffecdates", CStr(eRemoteDB.Constants.dtmNull),  , GetLocalResourceObject("tcdEffecdatesColumnToolTip"))
		Call .AddPossiblesColumn(40022, GetLocalResourceObject("tctTyp_comissColumnCaption"), "tctTyp_comiss", "Table7504", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctTyp_comissColumnToolTip"))
		Call .AddTextColumn(40024, GetLocalResourceObject("tctTableColumnCaption"), "tctTable", 30, vbNullString,  , GetLocalResourceObject("tctTableColumnToolTip"))
	End With
	
	With mobjGrid
		.Codispl = "AGC001"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 600
		.Width = 600
		.Top = 10
		.Left = 10
		.bOnlyForQuery = True
	End With
End Sub

'% insPreBCC003: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreAGC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsCommis_his As Object
	Dim lcolCommis_hiss As eAgent.Commis_hiss
	
	lcolCommis_hiss = New eAgent.Commis_hiss
	
	Response.Write("<BR>")
	If lcolCommis_hiss.FindAGC001(mobjValues.StringToType(Session("tcnIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		If CStr(Session("insvalAgent")) = vbNullString Then
			For	Each lclsCommis_his In lcolCommis_hiss
				With lclsCommis_his
					mobjGrid.Columns("tcdEffecdates").DefValue = .dEffecdate
					mobjGrid.Columns("tctTyp_comiss").DefValue = .sTyp_comiss
					mobjGrid.Columns("tctTable").DefValue = .sTabComDes
					Response.Write(mobjGrid.DoRow())
				End With
			Next lclsCommis_his
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolCommis_hiss = Nothing
	lclsCommis_his = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenues = New eFunctions.Menues
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenues.setZone(2, "AGC001", ""))
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.15 $|$$Author: Nvaplat60 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGC001" ACTION="valAgent.aspx?sMode=1">
<%
Response.Write(mobjValues.ShowWindowsName("AGC001"))

Call insDefineHeader()
Call insPreAGC001()

mobjValues = Nothing
mobjGrid = Nothing
mobjMenues = Nothing
%>
</FORM>
</BODY>
</HTML>




