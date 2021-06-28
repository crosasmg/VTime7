<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de la información de las compañías
Dim mclsCompany As eGeneral.Company
Dim mobjGrid As eFunctions.Grid



'%insDefineHeaderBroker.Esta funcion se encarga de mantener las compañias de seguros asociadas a la compañia broker
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeaderBroker()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MS110"
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCompany_detColumnCaption"), "cbeCompany_det", "TABCOMPANY", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "ShowChangeValues()")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnCompanyTypeColumnCaption"), "tcnCompanyType", "Table219", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnCompanyTypeColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnClasificColumnCaption"), "tcnClasific", "Table5563", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnClasificColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
	End With
	
	With mobjGrid
		.Codispl = "MS110"
		.Height = 200
		.Width = 520
		.AddButton = True
		.DeleteButton = True
		
		.ActionQuery = Session("bQuery")
		
		.Columns("Sel").GridVisible = True
		
		.sDelRecordParam = "nCompany_det=' + marrArray[lintIndex].cbeCompany_det + '"
		
	End With
End Sub

'% insPreMS110: Se cargan los valores de la grilla.
'--------------------------------------------------------------------------------------------
Private Sub insPreMS110()
	'--------------------------------------------------------------------------------------------
	
	Dim lclscompany As Object
	Dim lcolcompanys As eGeneral.Companys
	
	lcolcompanys = New eGeneral.Companys
	
	If lcolcompanys.Find_Broker_det(mobjValues.StringToType(Session("nCompany"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclscompany In lcolcompanys
			With mobjGrid
				.Columns("cbeCompany_det").DefValue = lclscompany.nCompany_det
				.Columns("tcnCompanyType").DefValue = lclscompany.sType
				.Columns("tcnClasific").DefValue = lclscompany.nClasific
				
				Response.Write(.DoRow)
			End With
		Next lclscompany
	End If
	Response.Write(mobjGrid.closeTable)
	lcolcompanys = Nothing
	lclscompany = Nothing
End Sub

'% insPreMS110Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMS110Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclscompany As eGeneral.Company
	
	lclscompany = New eGeneral.Company
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclscompany.InsPostMS110Upd(.QueryString.Item("Action"), Session("nCompany"), CInt(.QueryString.Item("nCompany_det")), Session("nUsercode"))
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantGeneral.aspx", "MS110", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
	End With
	lclscompany = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsCompany = New eGeneral.Company
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

Call mclsCompany.Find(mobjValues.StringToType(Session("nCompany"), eFunctions.Values.eTypeData.etdDouble))
mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
mobjValues.sCodisplPage = "MS110"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MS110", "MS110.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
mobjGrid = Nothing
Response.Write(mobjValues.StyleSheet())
%>
<SCRIPT>
//% insChangeValues: se controla el cambio de valor de los campos
//-------------------------------------------------------------------------------------------
function insChangeValues(){
//-------------------------------------------------------------------------------------------
	var lintMainAction = <%=Request.QueryString.Item("nMainAction")%>
//+ Si se está agregando la compañía, y la fecha de ingreso está vacía, se coloca como
//+ valor por defecto la fecha de ingreso del cliente 
	if(lintMainAction==301 &&
	   self.document.forms[0].tcdInputDate.value=='')
		insDefValues('Date', 'sClient='+self.document.forms[0].tcnClient.value)
}

//% ShowChangeValues: Se muestran los datos asociados a las compañias detalle 
//%					  cuando la cia es broker 
//-------------------------------------------------------------------------------------------
function ShowChangeValues(){
//-------------------------------------------------------------------------------------------

//alert('compañia ' + self.document.forms[0].cbeCompany_det.value);
	if (self.document.forms[0].cbeCompany_det.value!='')
	    insDefValues("Company", "nCompany=" + self.document.forms[0].cbeCompany_det.value, '/VTimeNet/Maintenance/Mantgeneral');

}


</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MS110" ACTION="valMantGeneral.aspx?Mode=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
	
	<%If Request.QueryString.Item("Type") <> "PopUp" Then%>

	<TABLE WIDTH="100%">
	    <TR>
	        <TD><LABEL ID=11934><%= GetLocalResourceObject("tcnClientCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.ClientControl("tcnClient", mclsCompany.sClient,  , GetLocalResourceObject("tcnClientToolTip"), "insChangeValues()",  , "tctClient",  ,  ,  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
	        <TD><LABEL ID=11937><%= GetLocalResourceObject("tcdInputDateCaption") %></LABEL></TD>
	        <TD COLSPAN="3"><%=mobjValues.DateControl("tcdInputDate", CStr(mclsCompany.dInpdate),  , GetLocalResourceObject("tcdInputDateToolTip"))%></TD>
	    </TR>    
		<TR>
	        <TD><LABEL ID=11938><%= GetLocalResourceObject("valStatusCaption") %></LABEL></TD>
	        <TD><%=mobjValues.PossiblesValues("valStatus", "Table26", eFunctions.Values.eValuesType.clngComboType, mclsCompany.sStatregt,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valStatusToolTip"))%></TD>
	        <TD><LABEL ID=11936><%= GetLocalResourceObject("valCompanyTypeCaption") %></LABEL></TD>
	        <TD><%=mobjValues.PossiblesValues("valCompanyType", "Table219", eFunctions.Values.eValuesType.clngComboType, mclsCompany.sType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCompanyTypeToolTip"))%></TD>
	    </TR>	            
		<TR>
	        <TD><LABEL ID=11939><%= GetLocalResourceObject("opnCountryCaption") %></LABEL></TD>
	        <TD><%=mobjValues.PossiblesValues("opnCountry", "Table66", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCompany.nCountry),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("opnCountryToolTip"))%></TD>
			<TD><LABEL ID=11933><%= GetLocalResourceObject("tcsRegsvsCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tcsRegsvs", 10, mclsCompany.sRegsvs,  , GetLocalResourceObject("tcsRegsvsToolTip"))%></TD>
	    </TR>
	    <TR>
	        <TD><LABEL ID=11932><%= GetLocalResourceObject("opnClassificCaption") %></LABEL></TD> 
	        <TD><%=mobjValues.PossiblesValues("opnClassific", "Table5563", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCompany.nClassific),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("opnClassificToolTip"))%></TD>
	        <TD><LABEL ID=11939><%= GetLocalResourceObject("tcnTaxRateCaption") %></LABEL></TD>
	        <TD><%=mobjValues.NumericControl("tcnTaxRate", 4, CStr(mclsCompany.nTaxrate),  , GetLocalResourceObject("tcnTaxRateToolTip"),  , 2)%></TD>
	    </TR>
		<TR>
		    <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Convenio de pago"><%= GetLocalResourceObject("AnchorConvenio de pagoCaption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD WIDTH="100%" COLSPAN="4"><HR></TD>
		</TR>
	    <TR>
	        <TD><LABEL ID=11933><%= GetLocalResourceObject("tctBankCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctBank", 30, mclsCompany.sBankname,  , GetLocalResourceObject("tctBankToolTip"))%></TD>
	        <TD><LABEL ID=11932><%= GetLocalResourceObject("tctAccountCaption") %></LABEL></TD>
	        <TD><%=mobjValues.TextControl("tctAccount", 25, mclsCompany.sAccount,  , GetLocalResourceObject("tctAccountToolTip"))%></TD>
	    </TR>
	    <TR>
		</TR>
	</TABLE>
<%	
End If

If mclsCompany.sType = "5" Then
	Call insDefineHeaderBroker()
	'			Response.Write "<NOTSCRIPT> alert('"&Request.QueryString("Type")&"')</script>"
	If Request.QueryString.Item("Type") = "PopUp" Then
		Call insPreMS110Upd()
	Else
		Call insPreMS110()
	End If
End If
%>
</FORM>
</BODY>
</HTML>
<%
mclsCompany = Nothing
mobjValues = Nothing

%>




