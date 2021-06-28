<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insPreDP19BP: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP19BP()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_covrol As eProduct.Tab_covrol
	Dim lclsProduct As eProduct.Product
	Dim lintApplycc As Object
	Dim lblnProduct_li As Boolean
	Dim lblnProduct_li_apl As Boolean
	Dim lblnFoundTab_covrol As Boolean
	
	lclsTab_covrol = New eProduct.Tab_covrol
	lclsProduct = New eProduct.Product
	
	Call lclsProduct.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))
	lblnProduct_li = Not lclsProduct.nProdclas = 7
	lblnProduct_li_apl = Not lclsProduct.nProdclas = 7
	lblnFoundTab_covrol = lclsTab_covrol.valExistsTab_covrol(Session("nBranch"), Session("nProduct"), Session("nCover"), Session("nRole"), Session("dEffecdate"))
	Call lclsTab_covrol.Find(Session("nBranch"), Session("nProduct"), Session("nModulec"), Session("nCover"), Session("nRole"), Session("dEffecdate"))
	With lclsTab_covrol
		If .sApplycc <> vbNullString Then
			lintApplycc = mobjValues.StringToType(.sApplycc, eFunctions.Values.eTypeData.etdDouble)
		End If
		
		If .nAmountCC <> eRemoteDB.Constants.intNull Or lclsProduct.nProdclas <> 7 Then
			lblnProduct_li_apl = True
		Else
			lblnProduct_li_apl = False
		End If
		
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=""100101"">" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD></TD>    " & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=""0"">" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        <TD></TD>    " & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14732"">" & GetLocalResourceObject("valCover_inCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("        ")

		
		With mobjValues
			.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverMax", Session("nCover"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", lclsTab_covrol.nRolprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrenRole", mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeCover", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.ReturnValue("nRole",  ,  , True)
			Response.Write(.PossiblesValues("valCover_in", "tabTab_covrol2", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsTab_covrol.nCover_in), True,  ,  ,  ,  ,  , Not lblnFoundTab_covrol,  , GetLocalResourceObject("valCover_inToolTip"),  , 1))
		End With
		
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("chkChange_typAdd", GetLocalResourceObject("chkChange_typAddCaption"), .sChangetypAdd, CStr(1),  ,  , 4, GetLocalResourceObject("chkChange_typAddToolTip")))

Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnRatepreadd", 6, CStr(.nRatepreadd),  , GetLocalResourceObject("tcnRatepreaddToolTip"),  , 2,  ,  ,  ,  ,  , 5))


Response.Write("<LABEL ID=""0"">" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14734"">" & GetLocalResourceObject("tctRoupremiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")

            ' PRIMA
            'Response.Write(mobjValues.TextControl("tctRoupremi", 12, .sRoupremi, , GetLocalResourceObject("tctRoupremiToolTip"), , , , , , 2))            
            mobjValues.Parameters.Add("NROUTINETYPE", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)            
            Response.Write(mobjValues.PossiblesValues("tctRoupremi", "TABTAB_ROUTINE", 12, .sRoupremi, True, , , , , , , 12, GetLocalResourceObject("tctRoupremiToolTip"), eFunctions.Values.eTypeCode.eString))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("chkChange_typSub", GetLocalResourceObject("chkChange_typSubCaption"), .sChangetypSub, CStr(1),  ,  , 6, GetLocalResourceObject("chkChange_typSubToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnRatepresub", 6, CStr(.nRatepresub),  , GetLocalResourceObject("tcnRatepresubToolTip"),  , 2,  ,  ,  ,  ,  , 7))


Response.Write("<LABEL ID=""0"">" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14725"">" & GetLocalResourceObject("tcnPremiratCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremirat", 9, CStr(.nPremirat),  , GetLocalResourceObject("tcnPremiratToolTip"),  , 6,  ,  ,  ,  ,  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnChprelevCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">")


Response.Write(mobjValues.NumericControl("tcnChprelev", 5, CStr(.nChprelev),  , GetLocalResourceObject("tcnChprelevToolTip"),  ,  ,  ,  ,  ,  ,  , 8))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>    " & vbCrLf)
Response.Write("        <TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnPremifixCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">")


Response.Write(mobjValues.NumericControl("tcnPremifix", 18, CStr(.nPremifix),  , GetLocalResourceObject("tcnPremifixToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>    " & vbCrLf)
Response.Write("        <TD><LABEL ID=""0"">" & GetLocalResourceObject("valid_tableCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">")


Response.Write(mobjValues.PossiblesValues("valid_table", "table5800", eFunctions.Values.eValuesType.clngWindowType, CStr(.nid_table),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valid_tableToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>        " & vbCrLf)
Response.Write("    <TR>    " & vbCrLf)
Response.Write("        <TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnPremimaxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">")


Response.Write(mobjValues.NumericControl("tcnPremimax", 18, CStr(.nPremimax),  , GetLocalResourceObject("tcnPremimaxToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">&nbsp;</TD>       " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkTypeAge", GetLocalResourceObject("chkTypeAgeCaption"), .sTypeAge, CStr(1),  ,  , 6, GetLocalResourceObject("chkTypeAgeToolTip")))


Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL ID=""0"">" & GetLocalResourceObject("tctRouRateCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD COLSPAN=""2"">")

            mobjValues.Parameters.Add("NROUTINETYPE", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("tctRouRate", "TABTAB_ROUTINE", 12, .sRourate, True, , , , , , , 12, GetLocalResourceObject("tctRouRateToolTip"), eFunctions.Values.eTypeCode.eString))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("		<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">&nbsp;</TD>       " & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=""0"">" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR> " & vbCrLf)
Response.Write("        <TD><LABEL ID=""0"">" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPercCostFP", 5, CStr(.nPercCostFP),  , GetLocalResourceObject("tcnPercCostFPToolTip"),  , 2,  ,  ,  ,  ,  , 5))


Response.Write("<LABEL ID=""0"">" & GetLocalResourceObject("Anchor6Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnRecCostFPCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnRecCostFP", 12, CStr(.nRecCostFP),  , GetLocalResourceObject("tcnRecCostFPToolTip"),  , 0,  ,  ,  ,  ,  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=""100767"">" & GetLocalResourceObject("Anchor7Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14729"">" & GetLocalResourceObject("tctCldeathiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


            ' MUERTE
            'Response.Write(mobjValues.TextControl("tctCldeathi", 12, .sCldeathi, , GetLocalResourceObject("tctCldeathiToolTip"), , , , , , 13))
            mobjValues.Parameters.Add("NROUTINETYPE", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("tctCldeathi", "TABTAB_ROUTINE", 12, .sCldeathi, True, , , , , , , 12 , GetLocalResourceObject("tctCldeathiToolTip"), eFunctions.Values.eTypeCode.eString))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14728"">" & GetLocalResourceObject("tctClaccidiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")

            'DOUBLE I            
            'Response.Write(mobjValues.TextControl("tctClaccidi", 12, .sClAccidi,  , GetLocalResourceObject("tctClaccidiToolTip"),  ,  ,  ,  ,  , 14))
            mobjValues.Parameters.Add("NROUTINETYPE", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("tctClaccidi", "TABTAB_ROUTINE", 12, .sClaccidi, True, , , , , , ,12 , GetLocalResourceObject("tctClaccidiToolTip"), eFunctions.Values.eTypeCode.eString))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14727"">" & GetLocalResourceObject("tctClvehaciCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctClvehaci", 12, .sClVehaci,  , GetLocalResourceObject("tctClvehaciToolTip"),  ,  ,  ,  ,  , 15))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14726"">" & GetLocalResourceObject("tctClsurviiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctClsurvii", 12, .sClSurvii,  , GetLocalResourceObject("tctClsurviiToolTip"),  ,  ,  ,  ,  , 16))


Response.Write("</TD>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14731"">" & GetLocalResourceObject("tctClincapiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctClincapi", 12, .sClIncapi,  , GetLocalResourceObject("tctClincapiToolTip"),  ,  ,  ,  ,  , 17))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14730"">" & GetLocalResourceObject("tctClinvaliCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctClinvali", 12, .sClinvali,  , GetLocalResourceObject("tctClinvaliToolTip"),  ,  ,  ,  ,  , 18))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""14733"">" & GetLocalResourceObject("tctClillnessCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctClillness", 12, .sCliIllness,  , GetLocalResourceObject("tctClillnessToolTip"),  ,  ,  ,  ,  , 19))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnMaxrentCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnMaxrent", 12, CStr(.nMaxrent),  , GetLocalResourceObject("tcnMaxrentToolTip"),  ,  ,  ,  ,  ,  ,  , 20))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>    " & vbCrLf)
Response.Write("</TABLE>")

		
		Response.Write(mobjValues.BeginPageButton)
	End With
	lclsTab_covrol = Nothing
	lclsProduct = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "DP19BP"
%>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>


    
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP19BP", "DP19BP.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%=mobjValues.StyleSheet()%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:08 $"

//+ Disabled : Inhabilita/Habilita los campos del frame Costos Conberturas
//---------------------------------------------------------------------------
function Disabled(){
//---------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (tcnAmountCC.value!=0){
            OptApplyCC[0].checked = false;
            OptApplyCC[1].checked = false;
            OptApplyCC[2].checked = true;
            OptApplyCC[0].disabled = true;
            OptApplyCC[1].disabled = true;
            OptApplyCC[2].disabled = true;
        }
        else{
            OptApplyCC[0].disabled = false;
            OptApplyCC[1].disabled = false;
            OptApplyCC[2].disabled = false;
        }
    }
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP19BP" ACTION="valRolesSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP19BP"))
Call insPreDP19BP()
%>
</FORM>
</BODY>
</HTML>




