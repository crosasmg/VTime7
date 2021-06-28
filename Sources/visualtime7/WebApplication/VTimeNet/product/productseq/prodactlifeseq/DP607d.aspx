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
	
	lblnDisabled = Request.QueryString.Item("Action") = "Update"
	
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeInvestColumnCaption"), "cbeTypeInvest", "Table5520", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypeInvestColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIntWarrColumnCaption"), "tcnIntWarr", 5, vbNullString,  , GetLocalResourceObject("tcnIntWarrColumnToolTip"),  , 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIntWarrMinColumnCaption"), "tcnIntWarrMin", 5, vbNullString,  , GetLocalResourceObject("tcnIntWarrMinColumnToolTip"),  , 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIntWarrClearColumnCaption"), "tcnIntWarrClear", 5, vbNullString,  , GetLocalResourceObject("tcnIntWarrClearColumnToolTip"),  , 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP607D"
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		.Columns("cbeTypeInvest").EditRecord = True
		.Height = 250
		.Width = 280
		.sEditRecordParam = "nModulec=" & Request.QueryString.Item("nModulec")
		.sDelRecordParam = "nModulec=" & Request.QueryString.Item("nModulec") & "&nTypeInvest=' + marrArray[lintIndex].cbeTypeInvest + '"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	With mobjValues
		Response.Write(.HiddenControl("hdnModulec", Request.QueryString.Item("nModulec")))
	End With
End Sub

'% insPreDP607B_K: Crea campos que van antes de tabla
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607B_K()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("cbeModulecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	
	With mobjValues.Parameters
		.Add("nBranch", mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "ReloadPage();", False,  , GetLocalResourceObject("cbeModulecToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
End Sub

'% insPreDP607d: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607d()
	'--------------------------------------------------------------------------------------------
	Dim lclsPlan_IntWar As eProduct.Plan_IntWar
	Dim lcolPlan_IntWar As eProduct.Plan_IntWarrs
	
	lclsPlan_IntWar = New eProduct.Plan_IntWar
	lcolPlan_IntWar = New eProduct.Plan_IntWarrs
	
	If lcolPlan_IntWar.Find(mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsPlan_IntWar In lcolPlan_IntWar
			With mobjGrid
				.Columns("cbeTypeInvest").DefValue = CStr(lclsPlan_IntWar.nTypeInvest)
				.Columns("tcnIntWarr").DefValue = CStr(lclsPlan_IntWar.nIntWarr)
				.Columns("tcnIntWarrMin").DefValue = CStr(lclsPlan_IntWar.nIntWarrMin)
				.Columns("tcnIntWarrClear").DefValue = lclsPlan_IntWar.nIntWarrClear
				
				Response.Write(.DoRow)
			End With
		Next lclsPlan_IntWar
	End If
	
	lclsPlan_IntWar = Nothing
	lcolPlan_IntWar = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP607dUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607dUpd()
	'--------------------------------------------------------------------------------------------
	Dim lobjTab_Activelife As eProduct.Tab_ActiveLife
	
	lobjTab_Activelife = New eProduct.Tab_ActiveLife
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
                If lobjTab_Activelife.InsPostDP607D("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nTypeInvest"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 0) Then
                    Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdActLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
				
                End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProdActLifeSeq.aspx", "DP607D", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	lobjTab_Activelife = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "DP607D"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP607D", "DP607D.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 3/07/06 7:40p $|$$Author: Gazuaje $"

/*% ReloadPage: Recarga la página tras  cambiar valores en combos
/*---------------------------------------------------------------------------------------------------------*/
function ReloadPage(){
/*---------------------------------------------------------------------------------------------------------*/
	var lstrstring = "";
	
	with (self.document.forms[0]) {
		lstrstring += document.location;
		lstrstring = lstrstring.replace(/&nModulec=.*/, "");
		lstrstring = lstrstring + "&nModulec="+ cbeModulec.value + "&reload=2";
	}
	document.location = lstrstring;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="DP607D" ACTION="valProdActLifeSeq.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("DP607D"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP607dUpd()
Else
	Call insPreDP607B_K()
	mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
	Call insPreDP607d()
End If
%>
</FORM> 
</BODY>
</HTML>





