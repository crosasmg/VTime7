<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insPreDP035B: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP035B()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_covrol As eProduct.Tab_covrol
	
	lclsTab_covrol = New eProduct.Tab_covrol
	
	Call lclsTab_covrol.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")))
	With lclsTab_covrol
		If .sCaren_type = vbNullString Then
			.sCaren_type = "1"
		End If
		
Response.Write("" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("chkFDRequire", GetLocalResourceObject("chkFDRequireCaption"), .sFDRequire, CStr(1),  ,  , 1, GetLocalResourceObject("chkFDRequireToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("       	<TD><LABEL ID=14505>" & GetLocalResourceObject("cbeFrantypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("       	<TD>")

		mobjValues.BlankPosition = False
		If .sFrantype = vbNullString Then
			Response.Write(mobjValues.PossiblesValues("cbeFrantype", "table64", 1, CStr(1),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeFrantypeToolTip"),  , 2))
		Else
			Response.Write(mobjValues.PossiblesValues("cbeFrantype", "table64", 1, .sFrantype,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeFrantypeToolTip"),  , 2))
		End If
		
Response.Write("</TD>	      " & vbCrLf)
Response.Write("    </TR>           " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("       	<TD><LABEL ID=14497>" & GetLocalResourceObject("cbeFrancAplCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("       	<TD>")


Response.Write(mobjValues.PossiblesValues("cbeFrancApl", "table33", 1, .sFrancApl,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeFrancAplToolTip"),  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14503>" & GetLocalResourceObject("tctRoufrancCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctRoufranc", 12, .sRoufranc,  , GetLocalResourceObject("tctRoufrancToolTip"),  ,  ,  ,  ,  , 4))


Response.Write("</TD> " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14502>" & GetLocalResourceObject("tcnFrancratCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnFrancrat", 4, CStr(.nFrancrat),  , GetLocalResourceObject("tcnFrancratToolTip"),  , 2,  ,  ,  ,  ,  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14498>" & GetLocalResourceObject("tcnFrancFixCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnFrancFix", 18, CStr(.nFrancFix),  , GetLocalResourceObject("tcnFrancFixToolTip"), True, 6,  ,  ,  ,  ,  , 6))


Response.Write("</TD> " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14501>" & GetLocalResourceObject("tcnFrancMinCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnFrancMin", 18, CStr(.nFrancMin),  , GetLocalResourceObject("tcnFrancMinToolTip"), True, 6,  ,  ,  ,  ,  , 7))


Response.Write("</TD> " & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14500>" & GetLocalResourceObject("tcnFrancMaxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnFrancMax", 18, CStr(.nFrancMax),  , GetLocalResourceObject("tcnFrancMaxToolTip"), True, 6,  ,  ,  ,  ,  , 8))


Response.Write("</TD> " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=1448>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        <TD></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>	" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.CheckControl("chkFdChantypAdd", GetLocalResourceObject("chkFdChantypAddCaption"), .sFDChantypAdd,  , "InsChangeFdChantyp();",  , 9, GetLocalResourceObject("chkFdChantypAddToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.NumericControl("tcnFDRateAdd", 6, CStr(.nFDRateAdd),  , GetLocalResourceObject("tcnFDRateAddToolTip"), True, 2,  ,  ,  ,  , True, 10))


Response.Write("" & vbCrLf)
Response.Write("			<LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL>" & vbCrLf)
Response.Write("		</TD> " & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("       	<TD><LABEL ID=0>" & GetLocalResourceObject("cbeCaren_typeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("       	<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCaren_type", "table52", 1, .sCaren_type,  ,  ,  ,  ,  , "InsChangecbeCaren_type()",  ,  , GetLocalResourceObject("cbeCaren_typeToolTip"),  , 14))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.CheckControl("chkFdChantypSub", GetLocalResourceObject("chkFdChantypSubCaption"), .sFDChantypSub,  , "InsChangeFdChantyp();",  , 11, GetLocalResourceObject("chkFdChantypSubToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.NumericControl("tcnFDRateSub", 6, CStr(.nFDRateSub),  , GetLocalResourceObject("tcnFDRateSubToolTip"), True, 2,  ,  ,  ,  , True, 12))


Response.Write("" & vbCrLf)
Response.Write("			<LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL>" & vbCrLf)
Response.Write("		</TD> " & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("       	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCaren_quanCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnCaren_quan", 5, CStr(.nCaren_quan),  , GetLocalResourceObject("tcnCaren_quanToolTip"),  ,  ,  ,  ,  ,  ,  , 15))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14499>" & GetLocalResourceObject("tcnFDUserLevCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnFDUserLev", 5, CStr(.nFDUserLev),  , GetLocalResourceObject("tcnFDUserLevToolTip"),  ,  ,  ,  ,  ,  ,  , 13))


Response.Write("</TD> " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>	")

		
	End With
	If Not mobjValues.ActionQuery Then
		With Response
			.Write("<SCRIPT>")
			.Write("InsChangeFdChantyp();")
			.Write("InsChangecbeCaren_type();")
			.Write("</" & "Script>")
		End With
	End If
	lclsTab_covrol = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "DP035B"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">



<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP035B", "DP035B.aspx"))
		mobjMenu = Nothing
	End If
End With

%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:08 $|$$Author: Nvaplat61 $"

//%InsChangeFdChantyp: Habilita los campos de % Aumento y Disminución si se indica
//-------------------------------------------------------------------------------------------
function InsChangeFdChantyp()
//-------------------------------------------------------------------------------------------
{
	with (self.document.forms[0]){
		tcnFDRateAdd.disabled = !chkFdChantypAdd.checked;
		if (tcnFDRateAdd.disabled) tcnFDRateAdd.value = '';
		tcnFDRateSub.disabled = !chkFdChantypSub.checked;
		if (tcnFDRateSub.disabled) tcnFDRateSub.value = '';
	}
}

//%InsChangecbeCaren_type: Habilita campo Duración Plazo de espera si se indica Tipo 
//--------------------------------------------------------------------------------------------
function InsChangecbeCaren_type()
//--------------------------------------------------------------------------------------------
{	
	with (self.document.forms[0])
	{
     	tcnCaren_quan.disabled = cbeCaren_type.value == 1;
		tcnCaren_quan.disabled = tcnCaren_quan.disabled;
		if (tcnCaren_quan.disabled==true) 
		{
	        tcnCaren_quan.value = '';
		}
	}
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP035B" ACTION="valRolesSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%
Response.Write(mobjValues.ShowWindowsName("DP035B"))
Call insPreDP035B()
%>
</FORM>
</BODY>
</HTML>




