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
	
	mobjGrid.sCodisplPage = "dp607c"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQMonthIniColumnCaption"), "tcnQMonthIni", 5, vbNullString,  , GetLocalResourceObject("tcnQMonthIniColumnToolTip"),  ,  ,  ,  ,  , lblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQMonthEndColumnCaption"), "tcnQMonthEnd", 5, vbNullString,  , GetLocalResourceObject("tcnQMonthEndColumnToolTip"),  ,  ,  ,  ,  , lblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, vbNullString,  , GetLocalResourceObject("tcnPercentColumnToolTip"),  , 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP607C"
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		.Columns("tcnQMonthIni").EditRecord = True
		.Height = 250
		.Width = 280
		.sEditRecordParam = "nModulec=" & Request.QueryString.Item("nModulec")
		.sDelRecordParam = "nModulec=" & Request.QueryString.Item("nModulec") & "&nQMonthIni=' + marrArray[lintIndex].tcnQMonthIni + '"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
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
	Response.Write(mobjValues.PossiblesValues("cbeModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "ReloadPage();",  ,  , GetLocalResourceObject("cbeModulecToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
End Sub

'% insPreDP607C: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607C()
	'--------------------------------------------------------------------------------------------
	Dim lclsLoad_Surr As eProduct.Load_surr
	Dim lcolLoad_Surr As eProduct.Load_surrs
	
	lclsLoad_Surr = New eProduct.Load_surr
	lcolLoad_Surr = New eProduct.Load_surrs
	
	If lcolLoad_Surr.Find(mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsLoad_Surr In lcolLoad_Surr
			With mobjGrid
				.Columns("tcnQMonthIni").DefValue = CStr(lclsLoad_Surr.nQMonthIni)
				.Columns("tcnQMonthEnd").DefValue = CStr(lclsLoad_Surr.nQMonthEnd)
				.Columns("tcnPercent").DefValue = CStr(lclsLoad_Surr.nPercent)
				Response.Write(.DoRow)
			End With
		Next lclsLoad_Surr
	End If
	
	lclsLoad_Surr = Nothing
	lcolLoad_Surr = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP607CUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607CUpd()
	'--------------------------------------------------------------------------------------------
	Dim lobjTab_Activelife As eProduct.Tab_ActiveLife
	
	lobjTab_Activelife = New eProduct.Tab_ActiveLife
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjTab_Activelife.InsPostDP607C("Del", mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nQMonthIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, mobjValues.StringToType(session("nUserCode"), eFunctions.Values.eTypeData.etdDate), 0,0,0,0,0) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdActLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
				
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProdActLifeSeq.aspx", "DP607C", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	lobjTab_Activelife = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "dp607c"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%--'--%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP607C", "DP607C.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:06 $|$$Author: Nvaplat61 $"

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
<FORM METHOD="POST" NAME="DP607C" ACTION="valProdActLifeSeq.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("DP607C"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP607CUpd()
Else
	Call insPreDP607B_K()
	Call insPreDP607C()
End If
%>
</FORM> 
</BODY>
</HTML>





