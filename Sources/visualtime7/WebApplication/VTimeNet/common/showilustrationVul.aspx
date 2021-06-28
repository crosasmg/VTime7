<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo     
Dim lclsNull_condi As ePolicy.Null_condi
Dim mclsPolicy As ePolicy.Policy
Dim nGuaranty As Byte
Dim nIntwarr2 As Double
Dim nIntwarrSav2 As Double
Dim lclsProduct As eProduct.Plan_IntWar

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid



'% InsDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub InsDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, vbNullString,  , GetLocalResourceObject("tcnYearColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAge_reinsuColumnCaption"), "tcnAge_reinsu", 3, vbNullString,  , GetLocalResourceObject("tcnAge_reinsuColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmodepacumColumnCaption"), "tcnAmodepacum", 18, vbNullString,  , GetLocalResourceObject("tcnAmodepacumColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmodepacum2ColumnCaption"), "tcnAmodepacum2", 18, vbNullString,  , GetLocalResourceObject("tcnAmodepacum2ColumnToolTip"), True, 2)
		
		If Request.QueryString.Item("sCertype") = "3" And nGuaranty = 1 Then
			.AddNumericColumn(0, GetLocalResourceObject("tcnValpolig2ColumnCaption"), "tcnValpolig2", 18, vbNullString,  , GetLocalResourceObject("tcnValpolig2ColumnToolTip"), True, 2)
			'.AddHiddenColumn "tcnValpoliga2",""
			.AddNumericColumn(0, GetLocalResourceObject("tcnValpoliga2ColumnCaption"), "tcnValpoliga2", 18, vbNullString,  , GetLocalResourceObject("tcnValpoliga2ColumnToolTip"), True, 2)
			.AddNumericColumn(0, GetLocalResourceObject("tcnValsurig2ColumnCaption"), "tcnValsurig2", 18, vbNullString,  , GetLocalResourceObject("tcnValsurig2ColumnToolTip"), True, 2)
			'.AddHiddenColumn "tcnProdeathig2",""
			.AddNumericColumn(0, GetLocalResourceObject("tcnProdeathig2ColumnCaption"), "tcnProdeathig2", 18, vbNullString,  , GetLocalResourceObject("tcnProdeathig2ColumnToolTip"), True, 2)
		End If
		
		.AddNumericColumn(0, GetLocalResourceObject("tcnValpoligColumnCaption"), "tcnValpolig", 18, vbNullString,  , GetLocalResourceObject("tcnValpoligColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValpoligaColumnCaption"), "tcnValpoliga", 18, vbNullString,  , GetLocalResourceObject("tcnValpoligaColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValsurigColumnCaption"), "tcnValsurig", 18, vbNullString,  , GetLocalResourceObject("tcnValsurigColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnProdeathigColumnCaption"), "tcnProdeathig", 18, vbNullString,  , GetLocalResourceObject("tcnProdeathigColumnToolTip"), True, 2)
		
		If Request.QueryString.Item("sCertype") = "3" And nGuaranty = 0 Then
			.AddNumericColumn(0, GetLocalResourceObject("tcnValpolig2ColumnCaption"), "tcnValpolig2", 18, vbNullString,  , GetLocalResourceObject("tcnValpolig2ColumnToolTip"), True, 2)
			.AddNumericColumn(0, GetLocalResourceObject("tcnValpoliga2ColumnCaption"), "tcnValpoliga2", 18, vbNullString,  , GetLocalResourceObject("tcnValpoliga2ColumnToolTip"), True, 2)
			.AddNumericColumn(0, GetLocalResourceObject("tcnValsurig2ColumnCaption"), "tcnValsurig2", 18, vbNullString,  , GetLocalResourceObject("tcnValsurig2ColumnToolTip"), True, 2)
			.AddNumericColumn(0, GetLocalResourceObject("tcnProdeathig2ColumnCaption"), "tcnProdeathig2", 18, vbNullString,  , GetLocalResourceObject("tcnProdeathig2ColumnToolTip"), True, 2)
		End If
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI1410"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.AddButton = False
		.DeleteButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("sCertype") = "3" Then
			.Splits_Renamed.AddSplit(0, "", 4)
			If Request.QueryString.Item("sCertype") = "3" And nGuaranty = 1 Then
				.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
				.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
			Else
				.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
				.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
			End If
		End If
	End With
	
End Sub

'% InsPreVI1410: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVI1410()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	Dim lcolProjectvul As ePolicy.Projectvuls
	Dim lclsProjectvul As ePolicy.Projectvul
	Dim llngCount As long
	Dim lblnQuery As Boolean
	Dim nPeriod As Object
	
	If (Request.QueryString.Item("nPeriod") = vbNullString) Or (CDbl(Request.QueryString.Item("nPeriod")) = 0) Then
		nPeriod = 1
	Else
		nPeriod = Request.QueryString.Item("nPeriod")
	End If
	
	lblnQuery = mobjValues.Stringtotype(Request.QueryString.Item("bQuery"), eFunctions.Values.eTypeData.etdBoolean)
	
	lcolProjectvul = New ePolicy.Projectvuls
	If lcolProjectvul.InsShowIlustrationVul(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), 3, mobjValues.StringToType(Request.QueryString.Item("nIntwarr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nIntwarrSav"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVp_initial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dBirthdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dEffecdate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nPremdeal_anu"), eFunctions.Values.eTypeData.etdDouble), True, lblnQuery, nIntwarr2, nIntwarrSav2) Then
		llngCount = 0
		For	Each lclsProjectvul In lcolProjectvul
			With mobjGrid
				.Columns("tcnYear").DefValue = lclsProjectvul.nYear
				.Columns("tcnAge_reinsu").DefValue = lclsProjectvul.nAge
				.Columns("tcnAmodepacum").DefValue = lclsProjectvul.nPremium
				.Columns("tcnAmodepacum2").DefValue = lclsProjectvul.nPremium2
				.Columns("tcnValpolig").DefValue = lclsProjectvul.nVp_npremium
				.Columns("tcnValpoliga").DefValue = lclsProjectvul.nVp_saving
				
				.Columns("tcnValsurig").DefValue = lclsProjectvul.nSurramount
				.Columns("tcnProdeathig").DefValue = lclsProjectvul.nCapital
				If Request.QueryString.Item("sCertype") = "3" Then
					.Columns("tcnValpolig2").DefValue = lclsProjectvul.nVp2_npremium
					.Columns("tcnValpoliga2").DefValue = lclsProjectvul.nVp2_saving
					.Columns("tcnValsurig2").DefValue = lclsProjectvul.nSurramount2
					.Columns("tcnProdeathig2").DefValue = lclsProjectvul.nCapital2
				End If
				
				If (lclsProjectvul.nYear = 1) Or ((lclsProjectvul.nYear - 1) Mod nPeriod) = 0 Then
					Response.Write(.DoRow)
				End If
			End With
			llngCount = llngCount + 1
			If llngCount = 1 Then
				Response.Write("<SCRIPT>InsProcessed()</" & "Script>")
			End If
			If llngCount Mod 5 = 0 Then
				Response.Flush()
			End If
		Next lclsProjectvul
	End If
	lcolProjectvul = Nothing
	lclsProjectvul = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% FindClientName: Busca el cliente del rol asegurado
'--------------------------------------------------------------------------------------------
Private Sub FindClientName()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	
	Call lclsNull_condi.FindClientName(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(2), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
End Sub

'% InsPreVI1410: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsShowVul()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	Dim lcolProjectvul As ePolicy.Projectvuls
	Dim lclsProjectvul As ePolicy.Projectvul
	Dim llngCount As Long
	Dim lblnQuery As Boolean
	Dim nPeriod As Object
	Dim lclsGeneral As eGeneral.GeneralFunction
	lclsGeneral = New eGeneral.GeneralFunction
	Session("sKey") = lclsGeneral.getsKey(Session("nUsercode"))
	lclsGeneral = Nothing
	
	If (Request.QueryString.Item("nPeriod") = vbNullString) Or (CDbl(Request.QueryString.Item("nPeriod")) = 0) Then
		nPeriod = 1
	Else
		nPeriod = Request.QueryString.Item("nPeriod")
	End If
	
    lblnQuery = mobjValues.Stringtotype(Request.QueryString.Item("bQuery"), eFunctions.Values.eTypeData.etdBoolean)

	
	lcolProjectvul = New ePolicy.Projectvuls
	
	If Not lblnQuery Then
		If lcolProjectvul.InsShowIlustrationVul(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), 3, mobjValues.StringToType(Request.QueryString.Item("nIntwarr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nIntwarrSav"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVp_initial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dBirthdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dEffecdate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nPremdeal_anu"), eFunctions.Values.eTypeData.etdDouble), True, lblnQuery, nIntwarr2, nIntwarrSav2) Then
		End If
	End If
	
	Response.Write(lcolProjectvul.MakeVI1410(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sKey")))
	lcolProjectvul = Nothing
	lclsProjectvul = Nothing
	
End Sub

</script>
<%Response.Expires = -1

Response.CacheControl = "private"

Response.Buffer = True

mobjValues = New eFunctions.Values
lclsNull_condi = New ePolicy.Null_condi
mclsPolicy = New ePolicy.Policy

nGuaranty = 0
nIntwarr2 = mobjValues.StringToType(Request.QueryString.Item("nIntwarr2"), eFunctions.Values.eTypeData.etdDouble)
nIntwarrSav2 = mobjValues.StringToType(Request.QueryString.Item("nIntwarrSav2"), eFunctions.Values.eTypeData.etdDouble)

lclsProduct = New eProduct.Plan_IntWar

If lclsProduct.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 1) Then
	
	nIntwarr2 = lclsProduct.nIntWarrMin
	nGuaranty = 1
End If

lclsProduct = Nothing

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

mobjValues.ActionQuery = True
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 10 $|$$Date: 10-05-06 13:01 $|$$Author: Clobos $"

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
<FORM METHOD="POST" NAME="VI1410">
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName("VI1410"))
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
        <TD><LABEL><%= GetLocalResourceObject("tctOptionCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctOption", 40, Request.QueryString.Item("nOption") & " " & Request.QueryString.Item("sOption"),  ,  ,  ,  ,  ,  , False)%></TD>
    </TR>
    </TR>
		<TD><LABEL><%= GetLocalResourceObject("tcnPremdealCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnPremdeal", 18, Request.QueryString.Item("nPremdeal_anu"),  ,  ,  , 6,  ,  ,  ,  , False)%></TD>
        <TD><LABEL><%= GetLocalResourceObject("tcnPremfreqCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnPremfreq", 18, Request.QueryString.Item("nPremfreq"),  ,  ,  , 6,  ,  ,  ,  , False)%></TD>
    </TR>
</TABLE>
<%
Response.Write("<BR>")
'+ Hace aparecer el reloj de espera mien tras se realiza el proceso de calculo
'	Response.Write "<NOTSCRIPT>" 
'    Response.Write "self.document.body.style.cursor = 'wait';"
'    Response.Write "</Script>" 
'+ Envía al browser lo que ya fue cargado en el documento
Response.Flush()
'    Call InsDefineHeader()
'    Call InsPreVI1410()
Call InsShowVul()
Response.Write("<BR>")
'+ Hace desaparecer el reloj de espera terminado el proceso de calculo
'	Response.Write "<NOTSCRIPT>" 
'   Response.Write "self.document.body.style.cursor = 'default';"
'    Response.Write "</Script>" 
mobjValues = Nothing
mobjGrid = Nothing
lclsNull_condi = Nothing
%>
</FORM> 
</BODY>
</HTML>




