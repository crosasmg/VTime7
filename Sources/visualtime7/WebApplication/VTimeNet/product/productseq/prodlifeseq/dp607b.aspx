<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
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
	Dim lblnDisabled As Boolean
	
	
	lblnDisabled = Request.QueryString.Item("Action") <> "Add"
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "dp607b"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInitMonthColumnCaption"), "tcnInitMonth", 5, vbNullString,  , GetLocalResourceObject("tcnInitMonthColumnToolTip"),  ,  ,  ,  ,  , lblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndMonthColumnCaption"), "tcnEndMonth", 5, vbNullString,  , GetLocalResourceObject("tcnEndMonthColumnToolTip"),  ,  ,  ,  ,  , lblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapStartColumnCaption"), "tcnCapStart", 18, vbNullString,  , GetLocalResourceObject("tcnCapStartColumnToolTip"), True, 6,  ,  ,  , lblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapEndColumnCaption"), "tcnCapEnd", 18, vbNullString,  , GetLocalResourceObject("tcnCapEndColumnToolTip"), True, 6,  ,  ,  , lblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAddMonthColumnCaption"), "tcnAddMonth", 5, vbNullString,  , GetLocalResourceObject("tcnAddMonthColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 9, vbNullString,  , GetLocalResourceObject("tcnPercentColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
	End With
	
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP607B"
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		.Columns("tcnInitMonth").EditRecord = True
		.Height = 315
		.Width = 280
		.sEditRecordParam = "nModulec=" & Request.QueryString.Item("nModulec") & "&nTypeLoad=" & Request.QueryString.Item("nTypeLoad")
		.sDelRecordParam = .sEditRecordParam & "&nInitMonth=' + marrArray[lintIndex].tcnInitMonth + '&nEndMonth=' + marrArray[lintIndex].tcnEndMonth + '&nCapStart=' + marrArray[lintIndex].tcnCapStart + '&nCapEnd=' + marrArray[lintIndex].tcnCapEnd + '"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	With mobjValues
		Response.Write(.HiddenControl("hdnModulec", Request.QueryString.Item("nModulec")))
		Response.Write(.HiddenControl("hdnTypeLoad", Request.QueryString.Item("nTypeLoad")))
	End With
	
End Sub

'% insPreDP607B_K: Crea campos que van antes de tabla
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607B_K()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""4""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("valModulecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""30%"">" & vbCrLf)
Response.Write("			")

	
	With mobjValues.Parameters
		.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valModulecToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("cbeTypeLoadCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.PossiblesValues("cbeTypeLoad", "Table5545", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(Request.QueryString.Item("nTypeLoad"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "ReloadPage();",  , 5, GetLocalResourceObject("cbeTypeLoadToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
End Sub

'% insPreDP607B: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607B()
	'--------------------------------------------------------------------------------------------
	Dim lclsPlan_Loads As eProduct.Plan_Loads
	Dim lcolPlan_Loadss As eProduct.Plan_Loadss
	
	lclsPlan_Loads = New eProduct.Plan_Loads
	lcolPlan_Loadss = New eProduct.Plan_Loadss
	
	If lcolPlan_Loadss.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTypeLoad"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsPlan_Loads In lcolPlan_Loadss
			With mobjGrid
				.Columns("tcnInitMonth").DefValue = CStr(lclsPlan_Loads.nInitMonth)
				.Columns("tcnEndMonth").DefValue = CStr(lclsPlan_Loads.nEndMonth)
				.Columns("tcnCapStart").DefValue = CStr(lclsPlan_Loads.nCapStart)
				.Columns("tcnCapEnd").DefValue = CStr(lclsPlan_Loads.nCapEnd)
				.Columns("tcnPercent").DefValue = CStr(lclsPlan_Loads.nPercent)
				.Columns("tcnAmount").DefValue = CStr(lclsPlan_Loads.nAmount)
				.Columns("tcnAddMonth").DefValue = CStr(lclsPlan_Loads.nMonths)
				Response.Write(.DoRow)
			End With
		Next lclsPlan_Loads
	End If
	
	With mobjValues
		Response.Write(.HiddenControl("hdnModulec", Request.QueryString.Item("nModulec")))
		Response.Write(.HiddenControl("hdnTypeLoad", Request.QueryString.Item("nTypeLoad")))
	End With
	
	lclsPlan_Loads = Nothing
	lcolPlan_Loadss = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP607BUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607BUpd()
	'--------------------------------------------------------------------------------------------
	Dim lobjTab_ActiveLife As eProduct.Tab_ActiveLife
	
	lobjTab_ActiveLife = New eProduct.Tab_ActiveLife
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjTab_ActiveLife.InsPostDP607B("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypeLoad"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nInitMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nEndMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapStart"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, 0, mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), 0) Then
				
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProdActLifeSeq.aspx", "DP607B", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjTab_ActiveLife = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "dp607b"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP607B", "DP607B.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:06 $|$$Author: Nvaplat61 $"

//% ReloadPage: Recarga la página tras  cambiar valores en combos
//---------------------------------------------------------------------------------------------------------
function ReloadPage(){
//---------------------------------------------------------------------------------------------------------
	var lstrstring = "";
	
	with (self.document.forms[0]) {
		lstrstring += document.location;
		lstrstring = lstrstring.replace(/&nModulec=.*/, "");
		lstrstring = lstrstring.replace(/&nTypeLoad=.*/, "");
		lstrstring = lstrstring + "&nModulec="+ valModulec.value + "&nTypeLoad="+ cbeTypeLoad.value  + "&reload=2";
	}
	document.location = lstrstring;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="DP607B" ACTION="valProdActLifeSeq.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("DP607B"))



Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP607BUpd()
Else
	Call insPreDP607B_K()
	Call insPreDP607B()
End If
%>
</FORM> 
</BODY>
</HTML>





