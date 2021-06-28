<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insPreDP19AP: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP19AP()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_covrol As eProduct.Tab_covrol
	Dim lintCacaltyp As Object
	Dim lblnTab_covrol As Boolean
	Dim lblnsLegDisabled As Boolean
	Dim lclsLife_cover As eProduct.Life_cover
	
	lclsTab_covrol = New eProduct.Tab_covrol
	lblnTab_covrol = lclsTab_covrol.valExistsTab_covrol(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	If lblnTab_covrol Then
		lblnTab_covrol = False
	Else
		lblnTab_covrol = True
	End If
	
	lclsLife_cover = New eProduct.Life_cover
	
	Call lclsLife_cover.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	If lclsLife_cover.sCoveruse <> "1" Then
		lblnsLegDisabled = True
	Else
		lblnsLegDisabled = False
	End If
	
	lclsLife_cover = Nothing
	
	lintCacaltyp = 1
	Call lclsTab_covrol.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	With lclsTab_covrol
		If mobjValues.StringToType(.sCacaltyp, eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
			lintCacaltyp = mobjValues.StringToType(.sCacaltyp, eFunctions.Values.eTypeData.etdDouble)
		End If
		
Response.Write("" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" border =0>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=100099><A NAME=""Forma de cálculo"">" & GetLocalResourceObject("AnchorForma de cálculoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Condiciones"">" & GetLocalResourceObject("AnchorCondicionesCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			 <TD COLSPAN=""3"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			 <TD></TD>" & vbCrLf)
Response.Write("			 <TD COLSPAN=""3"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(100102, "OptCalc", GetLocalResourceObject("OptCalc_CStr1Caption"), lintCacaltyp, CStr(1), "Disabled()",  ,  , GetLocalResourceObject("OptCalc_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14695>" & GetLocalResourceObject("tcnCacalmulCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCacalmul", 5, CStr(.nCacalmul),  , GetLocalResourceObject("tcnCacalmulToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "OptCalc", GetLocalResourceObject("OptCalc_CStr5Caption"), 6 - lintCacaltyp, CStr(5), "Disabled()",  ,  , GetLocalResourceObject("OptCalc_CStr5ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=14698>" & GetLocalResourceObject("tcnCapminimCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCapminim", 18, CStr(.nCapminim),  , GetLocalResourceObject("tcnCapminimToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(100104, "OptCalc", GetLocalResourceObject("OptCalc_CStr2Caption"), 3 - lintCacaltyp, CStr(2), "Disabled()",  ,  , GetLocalResourceObject("OptCalc_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT><LABEL ID=14694>" & GetLocalResourceObject("tcnCacalfixCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD WIDTH=""20%"">")


Response.Write(mobjValues.NumericControl("tcnCacalfix", 18, CStr(.nCacalfix),  , GetLocalResourceObject("tcnCacalfixToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""3%"">&nbsp;</TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=14697>" & GetLocalResourceObject("tcnCapmaximCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCapmaxim", 18, CStr(.nCapmaxim),  , GetLocalResourceObject("tcnCapmaximToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">")


Response.Write(mobjValues.OptionControl(100106, "OptCalc", GetLocalResourceObject("OptCalc_CStr3Caption"), 4 - lintCacaltyp, CStr(3), "Disabled()",  ,  , GetLocalResourceObject("OptCalc_CStr3ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT><LABEL ID=14696>" & GetLocalResourceObject("tcnCapbaspeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCapbaspe", 5, CStr(.nCapbaspe),  , GetLocalResourceObject("tcnCapbaspeToolTip"), True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnCaMaxPerCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCaMaxPer", 5, CStr(.nCaMaxPer),  , GetLocalResourceObject("tcnCaMaxPerToolTip"),  , 2,  ,  ,  ,  , lblnTab_covrol))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>			" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT WIDTH=""10%""><LABEL ID=14693>" & GetLocalResourceObject("valCacalcovCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("			")

		
		With mobjValues
			.Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverMax", mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", lclsTab_covrol.nRolcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrenRole", mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeCover", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.ReturnValue("nRole",  ,  , True)
			Response.Write(.PossiblesValues("valCacalcov", "tabTab_covrol2", 2, CStr(lclsTab_covrol.nCacalcov), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCacalcovToolTip")))
		End With
		
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valCaMaxCovCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("			")

		
		With mobjValues
			.Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverMax", mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", lclsTab_covrol.nCamaxrol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrenRole", mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeCover", "3", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.ReturnValue("nRole",  ,  , True)
		End With
		Response.Write(mobjValues.PossiblesValues("valCaMaxCov", "tabTab_covrol2", 2, CStr(.nCaMaxCov), True,  ,  ,  ,  ,  , lblnTab_covrol,  , GetLocalResourceObject("valCaMaxCovToolTip")))
		
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(100104, "OptCalc", GetLocalResourceObject("OptCalc_CStr4Caption"), 5 - lintCacaltyp, CStr(4), "Disabled()",  ,  , GetLocalResourceObject("OptCalc_CStr4ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT><LABEL ID=0>" & GetLocalResourceObject("tctRouprcalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


            'Response.Write(mobjValues.TextControl("tctRouprcal", 12, .sRouprcal,  , GetLocalResourceObject("tctRouprcalToolTip")))
            mobjValues.Parameters.Add("NROUTINETYPE", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("tctRouprcal", "TABTAB_ROUTINE", eFunctions.Values.eValuesType.clngWindowType, .sRouprcal, True, , , , , , , 12, GetLocalResourceObject("tctRouprcalToolTip"), eFunctions.Values.eTypeCode.eString))

Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT>")


Response.Write(mobjValues.CheckControl("chkLeg", "", .sLeg, CStr(1),  , lblnsLegDisabled,  , GetLocalResourceObject("chkLegToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=LEFT><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT><LABEL ID=0>" & GetLocalResourceObject("tctRouprcalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("tctROU_COND_CAP", 12, .SROU_COND_CAP,  , GetLocalResourceObject("tctROU_COND_CAPToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=100101><A NAME=""Cambios"">" & GetLocalResourceObject("AnchorCambiosCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		 	<TD COLSPAN=""6"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>	" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.CheckControl("chkCh_typ_capAdd", GetLocalResourceObject("chkCh_typ_capAddCaption"), .sChtypcapAdd, CStr(1),  ,  ,  , GetLocalResourceObject("chkCh_typ_capAddToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        <TD>" & vbCrLf)
Response.Write("				<LABEL ID=0>" & GetLocalResourceObject("tcnRatecapaddCaption") & "</LABEL>" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.NumericControl("tcnRatecapadd", 6, CStr(.nRatecapadd),  , GetLocalResourceObject("tcnRatecapaddToolTip"),  , 2))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("	        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnChcaplevCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.NumericControl("tcnChcaplev", 5, CStr(.nChcaplev),  , GetLocalResourceObject("tcnChcaplevToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.CheckControl("chkCh_typ_capSub", GetLocalResourceObject("chkCh_typ_capSubCaption"), .sChtypcapSub, CStr(1),  ,  ,  , GetLocalResourceObject("chkCh_typ_capSubToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        <TD>" & vbCrLf)
Response.Write("				<LABEL ID=0>" & GetLocalResourceObject("tcnRatecapaddCaption") & "</LABEL>" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.NumericControl("tcnRatecapsub", 6, CStr(.nRatecapsub),  , GetLocalResourceObject("tcnRatecapsubToolTip"),  , 2))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=100101><A NAME=""Cambios"">" & GetLocalResourceObject("AnchorCambios2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	 	<TD COLSPAN=""6"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcnQmonth_vigCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnQmonth_vig", 5, CStr(.nQmonth_vig),  , GetLocalResourceObject("tcnQmonth_vigToolTip")))


Response.Write("<TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcnQbetweenmodCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnQbetweenmod", 5, CStr(.nQbetweenmod),  , GetLocalResourceObject("tcnQbetweenmodToolTip")))


Response.Write("<TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcnQmax_modCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnQmax_mod", 5, CStr(.nQmax_mod),  , GetLocalResourceObject("tcnQmax_modToolTip")))


Response.Write("<TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	</TABLE>")

		
	End With
	lclsTab_covrol = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP19AP", "DP19AP.aspx"))
		mobjMenu = Nothing
	End If
End With

mobjValues.sCodisplPage = "DP19AP"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 4 $|$$Date: 15-02-06 12:51 $"
       
//---------------------------------------------------------------------------
// Inhabilita todos los campos del frame Franquicia/Deducible y Cambios
function Disabled()
//---------------------------------------------------------------------------
{   with (self.document.forms[0])
    {
		tcnCacalfix.disabled = !OptCalc[2].checked;
		if (tcnCacalfix.disabled) 
			tcnCacalfix.value = '';
		tcnCapbaspe.disabled = !OptCalc[3].checked;
		valCacalcov.disabled = tcnCapbaspe.disabled;
		if (tcnCapbaspe.disabled)
		{
			tcnCapbaspe.value = '';
			valCacalcov.value = '';
			valCacalcov_nRole.value = '';
			UpdateDiv('valCacalcovDesc','');
		}
		btnvalCacalcov.disabled = valCacalcov.disabled;
		tctRouprcal.disabled = !OptCalc[4].checked;
		btntctRouprcal.disabled = tctRouprcal.disabled;

		if (tctRouprcal.disabled) {
		    tctRouprcal.value = '';
		    UpdateDiv('tctRouprcalDesc', '');
		}
	}
}
</SCRIPT>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP19AP" ACTION="valRolesseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP19AP"))
Call insPreDP19AP()
If Not mobjValues.ActionQuery Then
	Response.Write("<SCRIPT>Disabled()</SCRIPT>")
End If
%>
</FORM>
</BODY>
</HTML>




