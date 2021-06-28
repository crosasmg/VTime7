<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	With mobjGrid.Columns
		
		Call .AddPossiblesColumn(7332,"Grupo de variables", "valLett_group", "TABTAB_PARAMLT030", 2, "", True,  ,  ,  , "ParamvalVariables();",  ,  ,"Grupo de variables asociadas a una correspondencia")
		mobjGrid.Columns("valLett_group").Parameters.Add("nLettRequest", Session("nLettRequest"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddPossiblesColumn(7333,"Variables", "valVariables", "TabTab_GroupVar", 2, "", True,  ,  ,  ,  , True, 12,"Variables  asociados al grupo de variables", eFunctions.Values.eTypeCode.eString)
		mobjGrid.Columns("valVariables").Parameters.Add("nLettRequest", Session("nLettRequest"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
		mobjGrid.Columns("valVariables").Parameters.Add("nLettGroup", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			mobjGrid.Columns("valVariables").Parameters.Add("nFirst", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Else
			mobjGrid.Columns("valVariables").Parameters.Add("nFirst", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		
		Call .AddPossiblesColumn(7334,"Operador", "cboOperator", "Table311", 2, "",  ,  ,  ,  , "DisabledUpd();",  ,  ,"Operadores aritméticos")
		Call .AddTextColumn(7335,"Valor inicial o único a ser asignado a la condición de búsqueda de la variable", "tctInitial", 15, "",  ,vbNullString)
		Call .AddTextColumn(7336,"Valor final del rango dado para la condición de búsqueda de la variable", "tctEnd", 15, "",  ,vbNullString)
		
		Call .AddHiddenColumn("tcnConcec", CStr(0))
		Call .AddHiddenColumn("tcnParameters", CStr(0))
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	With mobjGrid
		.Codispl = "LT030"
		.Width = 460
		.Height = 350
		.AddButton = True
		.DeleteButton = False
		.WidthDelete = 400
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		.Columns("valVariables").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = (CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401)
		Call .SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	End With
	
End Sub

'% insPreLT030: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreLT030()
	'--------------------------------------------------------------------------------------------
	Dim lcolLettValues As eLetter.LettValuess
	Dim nIndex As Integer
	Dim sWhere As String
	
	Session("nConditions") = 0
	lcolLettValues = New eLetter.LettValuess
	If lcolLettValues.Find(Session("nLettRequest")) Then
		sWhere = lcolLettValues.sSql
		For nIndex = 1 To lcolLettValues.Count
			Session("nConditions") = Session("nConditions") + 1
			With mobjGrid
				.DeleteButton = True
				.Columns("valLett_group").DefValue = CStr(lcolLettValues.Item(nIndex).nLett_group)
				.Columns("valVariables").DefValue = lcolLettValues.Item(nIndex).svariable
				.Columns("cboOperator").DefValue = CStr(lcolLettValues.Item(nIndex).nAritOper)
				
				If lcolLettValues.Item(nIndex).nAritOper = 7 Then
					.Columns("tctInitial").DefValue = Mid(lcolLettValues.Item(nIndex).sValue, 1, InStr(1, lcolLettValues.Item(nIndex).sValue, " AND ") - 1)
					.Columns("tctEnd").DefValue = Mid(lcolLettValues.Item(nIndex).sValue, InStr(1, lcolLettValues.Item(nIndex).sValue, " AND ") + 5)
				Else
					.Columns("tctInitial").DefValue = CStr(lcolLettValues.Item(nIndex).sValue)
					.Columns("tctEnd").DefValue = vbNullString
				End If
				
				.Columns("tcnConcec").DefValue = CStr(lcolLettValues.Item(nIndex).nConsec)
				.Columns("tcnParameters").DefValue = CStr(lcolLettValues.Item(nIndex).nParameters)
				.Columns("sParam").DefValue = "nLettRequest=" & Session("nLettRequest") & "&nConsec=" & lcolLettValues.Item(nIndex).nConsec & "&nLettGroup=" & lcolLettValues.Item(nIndex).nLett_group
				
			End With
			Response.Write(mobjGrid.DoRow())
		Next 
	End If
	Response.Write(mobjGrid.CloseTable())
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<LABEL ID=7337>Condiciones:</LABEL>")

	
	Response.Write(mobjValues.TextAreaControl("tcn", 5, 73, "Where " & sWhere,  ,vbNullString,  , True))
	
	mobjGrid = Nothing
	lcolLettValues = Nothing
	
End Sub

'% insPreLT030Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreLT030Upd()
	Dim lclsLettValues As eLetter.LettValues
	Dim lblnPost As Boolean
	If Request.QueryString.Item("Action") = "Del" Then
		lclsLettValues = New eLetter.LettValues
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			lblnPost = lclsLettValues.insPostLT030(Session("nLettRequest"), CShort(.QueryString.Item("nConsec")), CShort(.QueryString.Item("nLettGroup")), 0, vbNullString, vbNullString, Session("nUsercode"), eRemoteDB.Constants.intNull, "Del")
		End With
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValLetterSeq.aspx", "LT030", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write("<SCRIPT>DisabledUpd();")
		Response.Write("</" & "Script>")
		Session("sAction") = "Upd"
	ElseIf Request.QueryString.Item("Action") = "Del" Then 
		Session("sAction") = "Del"
	Else
		Session("sAction") = "Add"
	End If
	
	lclsLettValues = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LT030")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "LT030"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjGrid.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "LT030"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

    <%With Response
	.Write(mobjValues.StyleSheet())
	' .Write  mobjValues.ShowWindowsName("LT030",,,Request.QueryString("sWindowDescript"))
	.Write(mobjValues.ShowWindowsName("LT030", Request.QueryString.Item("sWindowDescript")))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "LT030", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
	End If
End With%>
<SCRIPT>
//----------------------------------------------------------------------------------------
function DisabledUpd()
//----------------------------------------------------------------------------------------
{
	with (self.document.forms[0])
	{
		valLett_group.disabled = true
		valVariables.disabled = true
		if (cboOperator.value == 7)
			tctEnd.disabled = false
		else
		{	tctEnd.value = ''
			tctEnd.disabled = true
			}
	}
}
function ParamvalVariables()
{	with (self.document.forms[0])
	{
		valVariables.Parameters.Param2.sValue= valLett_group.value;
		valVariables.value = '';
		
		if (valLett_group.value == '')
		{
			valVariables.disabled = true
			btnvalVariables.disabled = true
		}
		else
		{
			valVariables.disabled = false
			btnvalVariables.disabled = false
		}
	}
}
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmLT030" ACTION="valLetterseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreLT030()
Else
	Call insPreLT030Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing

%>

</FORM>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Call mobjNetFrameWork.FinishPage("LT030")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








