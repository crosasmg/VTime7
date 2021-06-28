<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim lclsLed_Compan As eLedge.Led_compan


'% insLoadCP001: Dibuja los campos no repetitivos de la pantalla, con sus respectivos
' valores segùn sea el caso.
'------------------------------------------------------------------------------------------
Private Sub insLoadCP001()
	'------------------------------------------------------------------------------------------
	
	Call lclsLed_Compan.insPreCP001(CInt(Request.QueryString.Item("nMainAction")), Session("nLedCompan"))
	
	
Response.Write("" & vbCrLf)
Response.Write("     <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""43%"">")

	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsLed_Compan.nCurrency),  ,  ,  ,  ,  ,  , lclsLed_Compan.EnacbeCurrency,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 1))
Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""15%""><LABEL>" & GetLocalResourceObject("gmnYearCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnYear", 4, CStr(lclsLed_Compan.nYear),  , GetLocalResourceObject("gmnYearToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnYear, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnNumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnNum", 8, CStr(lclsLed_Compan.nVoucher),  , GetLocalResourceObject("gmnNumToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnNum, 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	 </TABLE>" & vbCrLf)
Response.Write("	 <TABLE WIDTH=""100%"" COLS=6 WIDTH=""15%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""31%""><LABEL>" & GetLocalResourceObject("tcdFromLedCompanCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">")


Response.Write(mobjValues.DateControl("tcdFromLedCompan", CStr(lclsLed_Compan.valtcdFromLedCompan), True, GetLocalResourceObject("tcdFromLedCompanToolTip"),  ,  ,  ,  , lclsLed_Compan.EnatcdFromLedCompan, 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then
Response.Write("" & vbCrLf)
Response.Write("				<TD ALIGN=""LEFT"" WIDTH=""15%""><LABEL>" & GetLocalResourceObject("valLedCompanAuxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("valLedCompanAux", "tabled_compan", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , "DisabledAll(this.value);", lclsLed_Compan.EnachkCopy, 4, GetLocalResourceObject("valLedCompanAuxToolTip"),  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkUpdate", GetLocalResourceObject("chkUpdateCaption"), lclsLed_Compan.sBal_actu,  ,  , lclsLed_Compan.EnachkUpdate, 6, GetLocalResourceObject("chkUpdateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.CheckControl("chkClose", GetLocalResourceObject("chkCloseCaption"), lclsLed_Compan.sClose_mont,  ,  ,  , 7, GetLocalResourceObject("chkCloseToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>   " & vbCrLf)
Response.Write("     </TABLE>" & vbCrLf)
Response.Write("     <TABLE BORDER=0 WIDTH=""100%"" COLS=15 WIDTH=""5%"">  " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""15"" CLASS=""HighLighted""><LABEL><A NAME=""Estructura Contable"">" & GetLocalResourceObject("AnchorEstructura ContableCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""15""><HR></TD>" & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnCode1", 1, Mid(lclsLed_Compan.sStructure, 1, 1),  , GetLocalResourceObject("gmnCode1ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnCode0, 8))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnCode2", 1, Mid(lclsLed_Compan.sStructure, 2, 1),  , GetLocalResourceObject("gmnCode2ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnCode1, 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnCode3", 1, Mid(lclsLed_Compan.sStructure, 3, 1),  , GetLocalResourceObject("gmnCode3ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnCode2, 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnCode4", 1, Mid(lclsLed_Compan.sStructure, 4, 1),  , GetLocalResourceObject("gmnCode4ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnCode3, 11))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode5Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnCode5", 1, Mid(lclsLed_Compan.sStructure, 5, 1),  , GetLocalResourceObject("gmnCode5ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnCode4, 12))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode6Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnCode6", 1, Mid(lclsLed_Compan.sStructure, 6, 1),  , GetLocalResourceObject("gmnCode6ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnCode5, 13))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode7Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnCode7", 1, Mid(lclsLed_Compan.sStructure, 7, 1),  , GetLocalResourceObject("gmnCode7ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnCode6, 14))


Response.Write("</TD>" & vbCrLf)
Response.Write("         </TR>" & vbCrLf)
Response.Write("         <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnUnit1", 1, Mid(lclsLed_Compan.sStruct_uni, 1, 1),  , GetLocalResourceObject("gmnUnit1ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnUnit0, 15))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnUnit2", 1, Mid(lclsLed_Compan.sStruct_uni, 2, 1),  , GetLocalResourceObject("gmnUnit2ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnUnit1, 16))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("gmnCode3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("gmnUnit3", 1, Mid(lclsLed_Compan.sStruct_uni, 3, 1),  , GetLocalResourceObject("gmnUnit3ToolTip"),  , 0,  ,  ,  , "insValNumField(this)", lclsLed_Compan.EnagmnUnit2, 17))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("         </TR>   " & vbCrLf)
Response.Write("      </TABLE>" & vbCrLf)
Response.Write("      <TABLE BORDER=0 WIDTH=""100%"" COLS=5 WIDTH=""30%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HighLighted"" COLSPAN=""2""><LABEL><A NAME=""Año contable"">" & GetLocalResourceObject("AnchorAño contableCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=15%>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL><A NAME=""Mes contable"">" & GetLocalResourceObject("AnchorMes contableCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2""><HR></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2""><HR></TD>" & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("tcdInitLedDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdInitLedDate", CStr(lclsLed_Compan.valtcdInitLedDate),  , GetLocalResourceObject("tcdInitLedDateToolTip"),  ,  ,  ,  , lclsLed_Compan.EnatcdInitLedDate, 18))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("tcdInitLedDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdFrom", CStr(lclsLed_Compan.valtcdFrom),  , GetLocalResourceObject("tcdFromToolTip"),  ,  ,  ,  , lclsLed_Compan.EnatcdFrom, 20))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("lblEndLedDate1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("lblEndLedDate1", CStr(lclsLed_Compan.vallblEndLedDate1),  , GetLocalResourceObject("lblEndLedDate1ToolTip"),  ,  ,  ,  , True, 19))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("lblEndLedDate1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("lblTo1", CStr(lclsLed_Compan.vallblTo1),  , GetLocalResourceObject("lblTo1ToolTip"),  ,  ,  ,  , True, 21))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("     </TABLE>" & vbCrLf)
Response.Write("     <TABLE WIDTH=""100%"" COLS=4 WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL><A NAME=""Cuenta de resultado""> " & GetLocalResourceObject("AnchorCuenta de resultadoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4""><HR></TD>" & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("           <TD><LABEL>" & GetLocalResourceObject("gmtLossProfitCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("           <TD>")


Response.Write(mobjValues.TextControl("gmtLossProfit", 25,  ,  , GetLocalResourceObject("gmtLossProfitToolTip"),  ,  ,  ,  , True, 22))


Response.Write("</TD>" & vbCrLf)
Response.Write("           <TD><LABEL>" & GetLocalResourceObject("gmtGenBalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("           <TD>")


Response.Write(mobjValues.TextControl("gmtGenBal", 25,  ,  , GetLocalResourceObject("gmtGenBalToolTip"),  ,  ,  ,  , True, 23))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("     </TABLE>")

	
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
lclsLed_Compan = New eLedge.Led_compan
If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionCut) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "CP001"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "CP001", "CP001.aspx"))
		mobjMenu = Nothing
	End If
End With%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
// % insValNumField:coloca en 0 los campos numericos que dejen en blanco
//-------------------------------------------------------------------------------------------
function insValNumField(field){
//-------------------------------------------------------------------------------------------
	if (field.value.replace(/ */,'') == '')
	    field.value = 0
}

// % DisabledAll: Deshabilita todos los campos de la forma.
//-------------------------------------------------------------------------------------------
function DisabledAll(Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (Field>''){
            gmnCode1.disabled=true;
            gmnCode2.disabled=true;
            gmnCode3.disabled=true;
            gmnCode4.disabled=true;
            gmnCode5.disabled=true;
            gmnCode6.disabled=true;
            gmnCode7.disabled=true;

//se realiza la busqueda de la estructura de la compañia de la que se quiere copiar el catalogo
			insDefValues('Copy_Struct', 'nLed_Compan=' + Field, '/VTimeNet/GeneralLedGer/LedGerTra');

		}else{
            gmnCode1.disabled=false;
            gmnCode2.disabled=false;
            gmnCode3.disabled=false;
            gmnCode4.disabled=false;
            gmnCode5.disabled=false;
            gmnCode6.disabled=false;
            gmnCode7.disabled=false;
            gmnUnit1.disabled=false;
            gmnUnit2.disabled=false;
            gmnUnit3.disabled=false;
            
            gmnCode1.value=0;
            gmnCode2.value=0;
            gmnCode3.value=0;
            gmnCode4.value=0;
            gmnCode5.value=0;
            gmnCode6.value=0;
            gmnCode7.value=0;
            gmnUnit1.value=0;
            gmnUnit2.value=0;
            gmnUnit3.value=0;
		}
	}
}

//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $" 
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("CP001"))%>
<FORM METHOD="POST" ID="FORM" NAME="frmInsLedCompan" ACTION="ValLedGerTra.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%">
        <%
Call insLoadCP001()
%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>




