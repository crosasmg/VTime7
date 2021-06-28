<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eTarif" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As Object

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolTarif_tab_col As Object
Dim mobjTarif_val_col As eTarif.Tarif_val_col


'% insPreDP8002: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8002()
	'--------------------------------------------------------------------------------------------
	Dim lclsTarif_value As Object
	Dim lcolTarif_value As eTarif.Tarif_val_cols
	lcolTarif_value = New eTarif.Tarif_val_cols
	
	Dim lclsTarif_val_col As Object
	Dim lcolTarif_val_col As eTarif.Tarif_val_cols
	lcolTarif_val_col = New eTarif.Tarif_val_cols
	Dim lstrNameField As String
	
	If lcolTarif_value.Find_Value(Session("nId_Table")) Then
		For	Each lclsTarif_value In lcolTarif_value
			With mobjTarif_val_col.mobjGrid
				.Columns("tcnRate").DefValue = lclsTarif_value.nRate
				.Columns("cbeType_tar").DefValue = lclsTarif_value.nType_tar
				.Columns("tcnAmount").DefValue = lclsTarif_value.nAmount
				.Columns("hddnRow").DefValue = lclsTarif_value.nRow
				
				If lcolTarif_val_col.Find(Session("nId_Table"), lclsTarif_value.nRow, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
					For	Each lclsTarif_val_col In lcolTarif_val_col
						With mobjTarif_val_col.mobjGrid
							lstrNameField = "Col_" & lclsTarif_val_col.nId_column
							If lclsTarif_val_col.sValue <> "" Then
								.Columns(lstrNameField).DefValue = lclsTarif_val_col.sValue
							Else
								If lclsTarif_val_col.nValue > 0 Then
									.Columns(lstrNameField).DefValue = lclsTarif_val_col.nValue
								Else
									.Columns(lstrNameField).DefValue = lclsTarif_val_col.dValue
								End If
							End If
							
						End With
					Next lclsTarif_val_col
					Response.Write(.DoRow)
				End If
			End With
		Next lclsTarif_value
	End If
	
	Response.Write(mobjTarif_val_col.mobjGrid.closeTable())
End Sub

'% insPreDP8002Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'-------------------------------------------------------------------------------------------- 
Private Sub insPreDP8002Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTarif_val_col As eTarif.Tarif_val_col
	lclsTarif_val_col = New eTarif.Tarif_val_col
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsTarif_val_col.InsPostDP8002(.QueryString.Item("Action"), mobjValues.StringToType(Session("nId_Table"), eFunctions.Values.eTypeData.etdLong), "", "", "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToDate(Session("dEffecdate"))) Then
			End If
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/product/producttarseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=DP8002" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No" & "';</" & "Script>")
		End If
		Response.Write(mobjTarif_val_col.mobjGrid.DoFormUpd(.QueryString("Action"), "valProducttarseq.aspx", "DP8002", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "DP8002"
%>
<HTML>
<HEAD>
	<SCRIPT>
	//+ Variable para el control de versiones
	        document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.00 $"
    </SCRIPT>	
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP8002", "DP8002.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="DP8002" ACTION="valProducttarseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%Response.Write(mobjValues.ShowWindowsName("DP8002"))
mobjTarif_val_col = New eTarif.Tarif_val_col

Response.Write(mobjTarif_val_col.makegrid("DP8002", mobjValues.StringToType(Session("nId_Table"), eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("Type"), mobjValues.ActionQuery, mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("Reload")))

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP8002Upd()
Else
	If Request.QueryString.Item("Action") = "Del" Then
		Call insPreDP8002Upd()
	Else
		Call insPreDP8002()
	End If
End If
mobjTarif_val_col.SetNothing()
mobjTarif_val_col = Nothing
%>
</FORM> 
</BODY>
</HTML>





