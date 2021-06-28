<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolCoinsuran As ePolicy.Coinsurans


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCompanyColumnCaption"), "valCompany", "tabCompanyClient", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("valCompanyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnShareColumnCaption"), "tcnShare", 4, vbNullString,  , GetLocalResourceObject("tcnShareColumnToolTip"),  , 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "Codispl"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("valCompany").EditRecord = True
		.Height = 200
		.Width = 500
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valCompany").Parameters.Add("nCompany", Session("nCompanyUser"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.sDelRecordParam = "nCompany='+ marrArray[lintIndex].valCompany + '"
		.sEditRecordParam = "nOwnShare='+ self.document.forms[0].tcnOwnShare.value + '&nExpenses='+ self.document.forms[0].tcnExpenses.value + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCA020: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA020()
	'--------------------------------------------------------------------------------------------
	Dim lclsCoinsuran As ePolicy.Coinsuran
	Dim ldblShare As Object
	Dim ldblExpenses As Object
	Dim lintCount As Object
	
	lclsCoinsuran = New ePolicy.Coinsuran
	mcolCoinsuran = New ePolicy.Coinsurans
	
	ldblShare = Request.QueryString.Item("nOwnShare")
	ldblExpenses = Request.QueryString.Item("nExpenses")
	
	If mcolCoinsuran.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		If mobjValues.ActionQuery Then
			ldblShare = mcolCoinsuran("CI" & Session("sCertype") & Session("nBranch") & Session("nPolicy") & Session("nProduct") & Session("nCompanyUser") & Session("dEffecdate")).nShare
			ldblExpenses = mcolCoinsuran("CI" & Session("sCertype") & Session("nBranch") & Session("nPolicy") & Session("nProduct") & Session("nCompanyUser") & Session("dEffecdate")).nExpenses
		End If
	End If
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13119>" & GetLocalResourceObject("tcnOwnShareCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnOwnShare", 4, ldblShare,  , GetLocalResourceObject("tcnOwnShareToolTip"),  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13117>" & GetLocalResourceObject("tcnExpensesCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnExpenses", 4, ldblExpenses,  , GetLocalResourceObject("tcnExpensesToolTip"),  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<BR>")

	
	lintCount = 0
	For	Each lclsCoinsuran In mcolCoinsuran
		With mobjGrid
			If lclsCoinsuran.nCompany = Session("nCompanyUser") Then
				If Request.QueryString.Item("nOwnShare") = vbNullString Then
					ldblShare = lclsCoinsuran.nShare
				End If
				If Request.QueryString.Item("nExpenses") = vbNullString Then
					ldblExpenses = lclsCoinsuran.nExpenses
					If lclsCoinsuran.nExpenses = eRemoteDB.Constants.intNull Then
						ldblExpenses = vbNullString
					End If
				End If
			Else
				lintCount = lintCount + 1
				.Columns("valCompany").DefValue = CStr(lclsCoinsuran.nCompany)
				.Columns("tcnShare").DefValue = CStr(lclsCoinsuran.nShare)
				Response.Write(.DoRow)
			End If
		End With
	Next lclsCoinsuran
	
	With Response
		.Write(mobjGrid.closeTable())
		.Write(mobjValues.HiddenControl("hddRecordCount", lintCount))
		If Not mobjValues.ActionQuery Then
			.Write("<SCRIPT>")
			.Write("self.document.forms[0].tcnOwnShare.value = '" & ldblShare & "';")
			.Write("self.document.forms[0].tcnExpenses.value = '" & ldblExpenses & "';")
			.Write("</" & "Script>")
		End If
	End With
	mcolCoinsuran = Nothing
	lclsCoinsuran = Nothing
End Sub

'% insPreCA020Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA020Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsCoinsuran As ePolicy.Coinsuran
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsCoinsuran = New ePolicy.Coinsuran
			Response.Write(mobjValues.ConfirmDelete())
			If lclsCoinsuran.insPostCA020("PopUp", "Delete", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nOwnShare"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA020", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If .QueryString.Item("Action") <> "Del" Then
			Response.Write(mobjValues.HiddenControl("hddOwnShare", .QueryString.Item("nOwnShare")))
			Response.Write(mobjValues.HiddenControl("hddExpenses", .QueryString.Item("nExpenses")))
		End If
	End With
	
	lclsCoinsuran = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA020")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:48 $|$$Author: Nvaplat61 $"    
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA020", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA020" ACTION="valPolicySeq.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("CA020", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA020Upd()
Else
	Call insPreCA020()
End If
mobjValues = Nothing
mobjGrid = Nothing

%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA020")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




