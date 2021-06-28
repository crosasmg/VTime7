<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'%insPreDP50AP: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP50AP()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_covrol As eProduct.Tab_covrol
	lclsTab_covrol = New eProduct.Tab_covrol
	Call lclsTab_covrol.Find(Session("nBranch"), Session("nProduct"), Session("nModulec"), Session("nCover"), Session("nRole"), Session("dEffecdate"))
	With lclsTab_covrol
		
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Seguro"">" & GetLocalResourceObject("AnchorSeguroCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Pagos"">" & GetLocalResourceObject("AnchorPagosCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        <TD></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeTypDurinsCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

		mobjValues.TypeList = 2
		mobjValues.List = "4,7"
		mobjValues.BlankPosition = False
		Response.Write(mobjValues.PossiblesValues("cbeTypDurins", "table5589", eFunctions.Values.eValuesType.clngComboType, CStr(.nTypDurins),  ,  ,  ,  ,  , "InsDisabledDurat(this);",  ,  , GetLocalResourceObject("cbeTypDurinsToolTip"),  , 1))
		
Response.Write("" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeTypDurinsCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

		mobjValues.BlankPosition = False
		mobjValues.TypeList = 2
		mobjValues.List = "7"
		Response.Write(mobjValues.PossiblesValues("cbeTypDurpay", "table5589", eFunctions.Values.eValuesType.clngComboType, CStr(.nTypDurpay),  ,  ,  ,  ,  , "InsDisabledDurat(this);",  ,  , GetLocalResourceObject("cbeTypDurpayToolTip"),  , 3))
		
Response.Write("" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>    " & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnDuratIndCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnDuratInd", 3, CStr(.nDuratInd),  , GetLocalResourceObject("tcnDuratIndToolTip"),  , 0,  ,  ,  ,  ,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnDuratIndCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnDuratPay", 3, CStr(.nDuratPay),  , GetLocalResourceObject("tcnDuratPayToolTip"),  , 0,  ,  ,  ,  ,  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14551>" & GetLocalResourceObject("tctRout_payCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctRout_pay", 12, .sRout_pay,  , GetLocalResourceObject("tctRout_payToolTip"),  ,  ,  ,  ,  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Masculino"">" & GetLocalResourceObject("AnchorMasculinoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        <TD></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Masculino"">" & GetLocalResourceObject("AnchorMasculino2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        <TD></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>    " & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbenTypAgeMinMCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

		mobjValues.BlankPosition = False
		mobjValues.TypeList = 2
		mobjValues.List = "1,3,4,5,6,7,8"
		Response.Write(mobjValues.PossiblesValues("cbenTypAgeMinM", "table5589", eFunctions.Values.eValuesType.clngComboType, CStr(.nTyp_AgeMinM),  ,  ,  ,  ,  , "InsDisabledDurat(this);",  ,  , GetLocalResourceObject("cbenTypAgeMinMToolTip"),  , 3))
		
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbenTypAgeMinMCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

		mobjValues.BlankPosition = False
		mobjValues.TypeList = 2
		mobjValues.List = "1,3,4,5,6,7,8"
		Response.Write(mobjValues.PossiblesValues("cbenTypAgeMinF", "table5589", eFunctions.Values.eValuesType.clngComboType, CStr(.nTyp_AgeMinF),  ,  ,  ,  ,  , "InsDisabledDurat(this);",  ,  , GetLocalResourceObject("cbenTypAgeMinFToolTip"),  , 3))
		
Response.Write("" & vbCrLf)
Response.Write("		</TD>		" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>    " & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgemininsmCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnAgemininsm", 3, CStr(.nAgemininsm),  , GetLocalResourceObject("tcnAgemininsmToolTip"),  , 0,  ,  ,  ,  ,  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgemininsmCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnAgemininsf", 3, CStr(.nAgemininsf),  , GetLocalResourceObject("tcnAgemininsfToolTip"),  , 0,  ,  ,  ,  ,  , 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgemaxinsmCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnAgemaxinsm", 3, CStr(.nAgemaxinsm),  , GetLocalResourceObject("tcnAgemaxinsmToolTip"),  , 0,  ,  ,  ,  ,  , 7))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgemaxinsmCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnAgemaxinsf", 3, CStr(.nAgemaxinsf),  , GetLocalResourceObject("tcnAgemaxinsfToolTip"),  , 0,  ,  ,  ,  ,  , 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>    " & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgemaxpermCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnAgemaxperm", 3, CStr(.nAgemaxperm),  , GetLocalResourceObject("tcnAgemaxpermToolTip"),  , 0,  ,  ,  ,  ,  , 8))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgemaxpermCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnAgemaxperf", 3, CStr(.nAgemaxperf),  , GetLocalResourceObject("tcnAgemaxperfToolTip"),  , 0,  ,  ,  ,  ,  , 11))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=100130><A NAME=""Renovación"">" & GetLocalResourceObject("AnchorRenovaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=100132><A NAME=""Prima"">" & GetLocalResourceObject("AnchorPrimaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        <TD WIDTH=""3%""></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("chkRenewali", GetLocalResourceObject("chkRenewaliCaption"), .sRenewali, CStr(1),  ,  , 12, GetLocalResourceObject("chkRenewaliToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkRechapri", GetLocalResourceObject("chkRechapriCaption"), .sRechapri, CStr(1),  ,  , 14, GetLocalResourceObject("chkRechapriToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("chkRevIndex", GetLocalResourceObject("chkRevIndexCaption"), .sRevIndex, CStr(1),  ,  , 13, GetLocalResourceObject("chkRevIndexToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=19394>" & GetLocalResourceObject("tctRouchaprCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctRouchapr", 12, .sRouchapr,  , GetLocalResourceObject("tctRouchaprToolTip"),  ,  ,  ,  ,  , 15))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=100131><A NAME=""Capital"">" & GetLocalResourceObject("AnchorCapitalCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14551>" & GetLocalResourceObject("tctRout_payCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctRouchaca", 12, .sRouchaca,  , GetLocalResourceObject("tctRouchacaToolTip"),  ,  ,  ,  ,  , 16))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

		
	End With
	If Not Session("bQuery") Then
		Response.Write("<SCRIPT>")
		Response.Write("InsDisabledDurat(self.document.forms[0].cbeTypDurins);")
		Response.Write("InsDisabledDurat(self.document.forms[0].cbeTypDurpay);")
		Response.Write("</" & "Script>")
	End If
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = Session("bQuery")
mobjValues.sCodisplPage = "DP50AP"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">



<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP50AP"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjValues.ShowWindowsName("DP50AP"))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP50AP.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"
//%InsDisabledDurat: Habilita o deshabilita el campo de cantidad cuando se indica años o edad
//-------------------------------------------------------------------------------------------
function InsDisabledDurat(Field)
//-------------------------------------------------------------------------------------------
{
	with (self.document.forms[0]){
		if (Field.name == 'cbeTypDurins'){
			tcnDuratInd.disabled = (Field.value != '2' && Field.value != '1' && Field.value != '8' && Field.value != '9')
			if (tcnDuratInd.disabled) tcnDuratInd.value = '';
		}
		if (Field.name == 'cbeTypDurpay'){
			tcnDuratPay.disabled = (Field.value != '2' && Field.value != '1' && Field.value != '8' && Field.value != '9' )
			tctRout_pay.disabled = (Field.value != '4')
			if (tcnDuratPay.disabled) tcnDuratPay.value = '';
			if (tctRout_pay.disabled) tctRout_pay.value = '';
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP50AP" ACTION="valRolesSeq.aspx?x=1">
<%Call insPreDP50AP()%>
</FORM>
</BODY>
</HTML>





