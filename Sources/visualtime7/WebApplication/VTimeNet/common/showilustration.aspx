<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo     
Dim lclsNull_condi As ePolicy.Null_condi
Dim mclsPolicy As ePolicy.Policy

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid


'% InsDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub InsDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		mobjGrid.Splits_Renamed.AddSplit(0, "", 3)
		.AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, vbNullString,  , GetLocalResourceObject("tcnYearColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAge_reinsuColumnCaption"), "tcnAge_reinsu", 3, vbNullString,  , GetLocalResourceObject("tcnAge_reinsuColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmodepacumColumnCaption"), "tcnAmodepacum", 18, vbNullString,  , GetLocalResourceObject("tcnAmodepacumColumnToolTip"), True, 6)
		mobjGrid.Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValpoligColumnCaption"), "tcnValpolig", 18, vbNullString,  , GetLocalResourceObject("tcnValpoligColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValsurigColumnCaption"), "tcnValsurig", 18, vbNullString,  , GetLocalResourceObject("tcnValsurigColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnProdeathigColumnCaption"), "tcnProdeathig", 18, vbNullString,  , GetLocalResourceObject("tcnProdeathigColumnToolTip"), True, 6)
		mobjGrid.Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValpolimgColumnCaption"), "tcnValpolimg", 18, vbNullString,  , GetLocalResourceObject("tcnValpolimgColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValsurimgColumnCaption"), "tcnValsurimg", 18, vbNullString,  , GetLocalResourceObject("tcnValsurimgColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnProdeathimgColumnCaption"), "tcnProdeathimg", 18, vbNullString,  , GetLocalResourceObject("tcnProdeathimgColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VA595"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.AddButton = False
		.DeleteButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% InsPreVA595: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVA595()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	Dim lcolTmp_val669 As ePolicy.Tmp_val669s
	Dim lclsTmp_val669 As Object
	Dim llngCount As Short
	Dim lblnQuery As Object
	
	If Request.QueryString.Item("bQuery") = vbNullString Then
		lblnQuery = False
	Else
		lblnQuery = Request.QueryString.Item("bQuery")
	End If
	
	lcolTmp_val669 = New ePolicy.Tmp_val669s
	If lcolTmp_val669.InsShowIlustration(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nIllusttype"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("SessionId"), mobjValues.StringToType(Request.QueryString.Item("nProjRent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nAddpremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nSurrMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nSurrYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nSurrAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nPremdeal"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sPremdeal"),  , CBool(lblnQuery), mobjValues.StringToType(Request.QueryString.Item("nYear_end"), eFunctions.Values.eTypeData.etdDouble, True)) Then
		llngCount = 0
		For	Each lclsTmp_val669 In lcolTmp_val669
			With mobjGrid
				.Columns("tcnYear").DefValue = lclsTmp_val669.nYear
				.Columns("tcnAge_reinsu").DefValue = lclsTmp_val669.nAge_reinsu
				.Columns("tcnAmodepacum").DefValue = lclsTmp_val669.nAmodepacum
				.Columns("tcnValpolig").DefValue = lclsTmp_val669.nValpolig
				.Columns("tcnValsurig").DefValue = lclsTmp_val669.nValsurig
				.Columns("tcnProdeathig").DefValue = lclsTmp_val669.nProdeathig
				.Columns("tcnValpolimg").DefValue = lclsTmp_val669.nValpolimg
				.Columns("tcnValsurimg").DefValue = lclsTmp_val669.nValsurimg
				.Columns("tcnProdeathimg").DefValue = lclsTmp_val669.nProdeathimg
				Response.Write(.DoRow)
			End With
			llngCount = llngCount + 1
			If llngCount = 1 Then
				Response.Write("<SCRIPT>InsProcessed()</" & "Script>")
			End If
			If llngCount Mod 5 = 0 Then
				Response.Flush()
			End If
		Next lclsTmp_val669
	End If
	lcolTmp_val669 = Nothing
	Response.Write(mobjGrid.closeTable())
End Sub

'% FindClientName: Busca el cliente del rol asegurado
'--------------------------------------------------------------------------------------------
Private Sub FindClientName()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	
	Call lclsNull_condi.FindClientName(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(2), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
End Sub

</script>
<%
Response.Expires = -1

Response.CacheControl = "private"

Response.Buffer = True



mobjValues = New eFunctions.Values
lclsNull_condi = New ePolicy.Null_condi
mclsPolicy = New ePolicy.Policy

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

mobjValues.ActionQuery = True
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 3/11/03 17:31 $|$$Author: Nvaplat11 $"

//%InsProcessed: Actualiza el indicador de procesado
//-----------------------------------------------------------------------------------------
function InsProcessed(){
//-----------------------------------------------------------------------------------------
	if (typeof(top.opener.document.forms[0].hddsProcessed) != 'undefined'){
		top.opener.document.forms[0].hddsProcessed.value = '1';
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VA595">
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName("VA595"))
Response.Write("<BR>")
Call FindClientName()
%>

<TABLE WIDTH="100%">
    <TR>
        <TD><DIV ID="lblPolicyNum"><%=mclsPolicy.TransactionCA001(mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), True)%></DIV></TD>
        <TD><%=mobjValues.NumericControl("lblNumPolicy", 8, Session("nPolicy"),  , "", False,  , True)%></TD>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
        <TD><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
    <TR>
    
    </TR>            
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
        <TD><%=mobjValues.ClientControl("tctClient", lclsNull_condi.sClient,  , GetLocalResourceObject("tctClientToolTip"),  , True)%></TD>
    </TR>
</TABLE>
<%
Response.Write("<BR>")
'+ Hace aparecer el reloj de espera mien tras se realiza el proceso de calculo
Response.Write("<SCRIPT>")
Response.Write("self.document.body.style.cursor = 'wait';")
Response.Write("</Script>")
'+ Envía al browser lo que ya fue cargado en el documento
Response.Flush()
Call InsDefineHeader()
Call InsPreVA595()
Response.Write("<BR>")
'+ Hace desaparecer el reloj de espera terminado el proceso de calculo
Response.Write("<SCRIPT>")
Response.Write("self.document.body.style.cursor = 'default';")
Response.Write("</Script>")
mobjValues = Nothing
mobjGrid = Nothing
lclsNull_condi = Nothing
%>
</FORM> 
</BODY>
</HTML>




