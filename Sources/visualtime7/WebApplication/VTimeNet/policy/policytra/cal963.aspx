<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "CAL963"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate", 20, vbNullString,  , GetLocalResourceObject("tcdStartdateColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", 20, vbNullString,  , GetLocalResourceObject("tcdExpirdatColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcnPremium_realColumnCaption"), "tcnPremium_real", 20, vbNullString,  , GetLocalResourceObject("tcnPremium_realColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcnPremium_tmpColumnCaption"), "tcnPremium_tmp", 20, vbNullString,  , GetLocalResourceObject("tcnPremium_tmpColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcnPremium_AjuColumnCaption"), "tcnPremium_Aju", 20, vbNullString,  , GetLocalResourceObject("tcnPremium_AjuColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CAL963"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreCAL963: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCAL963()
	'--------------------------------------------------------------------------------------------
	Dim lclssum_insur As Object
	Dim lcolsum_insurs As ePolicy.Sum_insurs
	Dim lintCount As Object
	Dim lintTransaction As Object
	Dim intTotAjust As Double
	
	lcolsum_insurs = New ePolicy.Sum_insurs
	
	If lcolsum_insurs.Find_Cal963(mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		For	Each lclssum_insur In lcolsum_insurs
			With mobjGrid
				.Columns("tcdStartdate").DefValue = lclssum_insur.dStartdate
				.Columns("tcdExpirdat").DefValue = lclssum_insur.dExpirdate
				.Columns("tcnPremium_real").DefValue = lclssum_insur.nPremium_real
				.Columns("tcnPremium_tmp").DefValue = lclssum_insur.nPremium_tmp
				.Columns("tcnPremium_Aju").DefValue = lclssum_insur.nPremium_ajust
				intTotAjust = intTotAjust + lclssum_insur.nPremium_ajust
				Response.Write(.DoRow)
			End With
		Next lclssum_insur
	End If
	
Response.Write("" & vbCrLf)
Response.Write("	<TD COLSPAN=3 CLASS=HIGHLIGHTED></TD>" & vbCrLf)
Response.Write("	<TD CLASS=HIGHLIGHTED><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	<TD CLASS=HIGHLIGHTED><LABEL>")


Response.Write(intTotAjust)


Response.Write("</LABEL></TD>")

	
	Response.Write(mobjGrid.closeTable())
	'	End With
	
	mcolClass = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "CAL963"
mobjMenu = New eFunctions.Menues

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 11/05/04 19:20 $|$$Author: Nvaplat7 $"

</SCRIPT>


<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CAL963", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CAL963" ACTION="ValPolicyTra.aspx?x=1">
    <%Response.Write(mobjValues.ShowWindowsName("CAL963", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCAL963()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>

</FORM> 
</BODY>
</HTML>






