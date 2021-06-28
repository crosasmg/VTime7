<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


Sub LoadHeader()
	Dim mstrString As String
	
Response.Write("" & vbCrLf)
Response.Write("	<BR><BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("cbeInsur_areaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeInsur_area", "table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeInsur_areaToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_1Caption"), CStr(1), "1", "insChangeDocType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optBillsType", GetLocalResourceObject("optBillsType_1Caption"), CStr(1), "1", "insChangeBillsType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), CStr(1), "1",  , True))


Response.Write(" </TD>		    " & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_1Caption"), CStr(1), "1",  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</td>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_2Caption"),  , "2", "insChangeDocType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optBillsType", GetLocalResourceObject("optBillsType_2Caption"),  , "2", "insChangeBillsType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2",  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_2Caption"),  , "2",  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"">&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optBillsType", GetLocalResourceObject("optBillsType_3Caption"),  , "3", "insChangeBillsType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE  WIDTH=""100%"" Border=1>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""4%""><LABEL>" & GetLocalResourceObject("tcdDateIniCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""5%"">")


Response.Write(mobjValues.DateControl("tcdDateIni",  ,  , GetLocalResourceObject("tcdDateIniToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""4%""><LABEL>" & GetLocalResourceObject("tcdDateEndCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""5%"">")


Response.Write(mobjValues.DateControl("tcdDateEnd",  ,  , GetLocalResourceObject("tcdDateEndToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""5%""><LABEL>" & GetLocalResourceObject("tcdDatePrintCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""5%"">")


Response.Write(mobjValues.DateControl("tcdDatePrint", CStr(Today),  , GetLocalResourceObject("tcdDatePrintToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	    ")

	mstrString = Request.Params.Get("Query_String")
	With Response
		.Write("<SCRIPT>")
		.Write("top.fraSequence.plngMainAction=" & Request.QueryString.Item("nMainAction") & ";top.fraFolder.document.location =""COL700A.aspx?sCodispl=COL700A&" & Request.Params.Get("Query_String") & """;")
		.Write("</" & "Script>")
	End With
Response.Write("" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	")

End Sub
Sub LoadFolder()
	
Response.Write("" & vbCrLf)
Response.Write("	<BR><BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("cbeInsur_areaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeInsur_area", "table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeInsur_areaToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_1Caption"), CStr(1), "1", "insChangeDocType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optBillsType", GetLocalResourceObject("optBillsType_1Caption"), CStr(1), "1", "insChangeBillsType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), CStr(1), "1",  , True))


Response.Write(" </TD>		    " & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_1Caption"), CStr(1), "1",  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</td>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_2Caption"),  , "2", "insChangeDocType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optBillsType", GetLocalResourceObject("optBillsType_2Caption"),  , "2", "insChangeBillsType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2",  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_2Caption"),  , "2",  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"">&nbsp;</td>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optBillsType", GetLocalResourceObject("optBillsType_3Caption"),  , "3", "insChangeBillsType()", True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE  WIDTH=""100%"" Border=1>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""4%""><LABEL>" & GetLocalResourceObject("tcdDateIniCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""5%"">")


Response.Write(mobjValues.DateControl("tcdDateIni",  ,  , GetLocalResourceObject("tcdDateIniToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""4%""><LABEL>" & GetLocalResourceObject("tcdDateEndCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""5%"">")


Response.Write(mobjValues.DateControl("tcdDateEnd",  ,  , GetLocalResourceObject("tcdDateEndToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""5%""><LABEL>" & GetLocalResourceObject("tcdDatePrintCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""5%"">")

Response.Write(mobjValues.DateControl("tcdDatePrint", CStr(Today),  , GetLocalResourceObject("tcdDatePrintToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" BORDER=1>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor9Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""6"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcnBillCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnBill", 15, vbNullString,  ,  ,  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3""><LABEL>" & GetLocalResourceObject("tcnLastNumDocCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnLastNumDoc", 15, vbNullString,  , GetLocalResourceObject("tcnLastNumDocToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9689>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ClientControl("dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD> <LABEL ID=41200>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=40010>" & GetLocalResourceObject("valProductCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13381>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 8, Request.Form.Item("tcnPolicy"),  , "", False,  ,  ,  ,  , "LockControl(""Policy"");ShowChangeValues(""Policy"")", True))


Response.Write("</td>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13372>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeAgency", "table5555", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
    </SCRIPT>		

<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

// insChangeDocType: Actualiza los objetos de la forma, según el tipo de via de pago.
//-------------------------------------------------------------------------------------------
function insChangeDocType() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
//+ Si el tipo de documento corresponde a factura.
		if (optDocType[0].checked){
			optBillsType[0].disabled = false;
			optBillsType[1].disabled = false;
			optBillsType[2].disabled = false;
			optBillsType[0].checked = true;
			optProcess[0].disabled = false;
			optProcess[1].disabled = false;
			tcnBill.disabled = true;
		}
		else {
//+ Si el tipo de documento corresponde a nota de crédito.
			if (optDocType[1].checked){
				optBillsType[0].checked = false;
				optBillsType[1].checked = false;
				optBillsType[2].checked = false;
				optBillsType[0].disabled = true;
				optBillsType[1].disabled = true;
				optBillsType[2].disabled = true;
				optProcess[0].disabled = true;
				optProcess[1].disabled = true;
				optMode[0].checked = true;
				optMode[0].disabled = true;
				optMode[1].disabled = true;
				tcnBill.disabled = false;
			}
		}
//+ Si el processo es puntual.
		if (optProcess[0].checked){
			optMode[1].checked = true;
			optMode[0].disabled = true;
			optMode[1].disabled = true;
			cbeAgency.disabled = true;
		}
		else {
			optMode[0].disabled = false;
			optMode[1].disabled = false;
			cbeAgency.disabled = false;
			
		}
//+ Si el tipo de factura es afecta o exenta.
		if (optBillsType[0].checked || optBillsType[1].checked){
			tcdDatePrint.disabled = true;
		}
		else {
			tcdDatePrint.disabled = false;
		}
	}
}

// insChangeBillsType: Actualiza los objetos de la forma, según el tipo de via de pago.
//-------------------------------------------------------------------------------------------
function insChangeBillsType() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
//+ Si el tipo de documento corresponde a factura.
		if (optBillsType[0].checked || optBillsType[1].checked){
			tcdDatePrint.disabled = true;
		}
		else {
			tcdDatePrint.disabled = false;
		}
	}
}

// insChangeBillsType: Actualiza los objetos de la forma, según el tipo de via de pago.
//-------------------------------------------------------------------------------------------
function insChangeProcess() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
//+ Si el tipo de documento corresponde a factura.
		if (optProcess[1].checked){
			cbeAgency.disabled = false;
		}
		else {
			cbeAgency.disabled = true;
		}
	}
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("COL700", "COL700_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COL700" ACTION="valCollectionRep.aspx?sMode=2">
<%
If Request.QueryString.Item("sConfig") = "InSequence" Then
	'Call LoadHeader()
Else
	Call LoadFolder()
End If%>
</FORM> 
</BODY>
</HTML>




