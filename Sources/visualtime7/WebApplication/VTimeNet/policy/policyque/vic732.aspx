<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.21
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

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
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "vic732"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, vbNullString,  , GetLocalResourceObject("tcnYearColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 2, vbNullString,  , GetLocalResourceObject("tcnMonthColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnResc_valColumnCaption"), "tcnResc_val", 18, vbNullString,  , GetLocalResourceObject("tcnResc_valColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnSald_valColumnCaption"), "tcnSald_val", 18, vbNullString,  , GetLocalResourceObject("tcnSald_valColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPro_yearColumnCaption"), "tcnPro_year", 3, vbNullString,  , GetLocalResourceObject("tcnPro_yearColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDefamountColumnCaption"), "tcnDefamount", 18, vbNullString,  , GetLocalResourceObject("tcnDefamountColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "VIC732"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreVIC732: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVIC732()
	'--------------------------------------------------------------------------------------------
	Dim lclsGuarant_val As Object
	Dim lclsGuarant_valDet As ePolicy.Guarant_val
	
	lclsGuarant_valDet = New ePolicy.Guarant_val
	
	Call lclsGuarant_valDet.insPreVIC732("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HIGHLIGHTED"" COLSPAN=""2""><LABEL ID=""0"">" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(10, "optAut_guarval", GetLocalResourceObject("optAut_guarval_CStr2Caption"), CStr(CDbl(lclsGuarant_valDet.sAut_guarval) - 1), CStr(2), "ChangeValues()",  , 1, GetLocalResourceObject("optAut_guarval_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsGuarant_valDet.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(10, "optAut_guarval", GetLocalResourceObject("optAut_guarval_CStr1Caption"), lclsGuarant_valDet.sAut_guarval, CStr(1),  ,  ,  , GetLocalResourceObject("optAut_guarval_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.ButtonControl("btnPrint", "Imprimir reporte", "printReport(""2"", """ & Request.QueryString.Item("nBranch") & """, """ & Request.QueryString.Item("nProduct") & """, """ & Request.QueryString.Item("nPolicy") & """, """ & Request.QueryString.Item("nCertif") & """, """ & Request.QueryString.Item("dEffecdate") & """)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("	")

	
	For	Each lclsGuarant_val In lclsGuarant_valDet.mcolGuarant_val
		With mobjGrid
			.Columns("tcnYear").DefValue = lclsGuarant_val.nYear
			.Columns("tcnMonth").DefValue = lclsGuarant_val.nMonth
			.Columns("tcnResc_val").DefValue = lclsGuarant_val.nResc_val
			.Columns("tcnSald_val").DefValue = lclsGuarant_val.nSaldvalkm
			.Columns("tcnPro_year").DefValue = lclsGuarant_val.nPro_year
			.Columns("tcnDefamount").DefValue = lclsGuarant_val.nDefamount
			Response.Write(.DoRow)
		End With
	Next lclsGuarant_val
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsGuarant_val = Nothing
	lclsGuarant_valDet = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vic732")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vic732"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = True
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "VIC732", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 18-08-09 11:39 $|$$Author: Mgonzalez $"
    
    function printReport(sCertype, nBranch, nProduct,
                         nPolicy , nCertif, dEffecdate){
		insDefValues('PrintVIC732', 'sCertype=' + sCertype +
		                            '&nBranch=' + nBranch +
		                            '&nProduct=' + nProduct +
		                            '&nPolicy=' + nPolicy +
		                            '&nCertif=' + nCertif +
		                            '&dEffecdate=' + dEffecdate)
	}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VIC732" ACTION="valPolicyQue.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("VIC732", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreVIC732()

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.21
Call mobjNetFrameWork.FinishPage("vic732")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




