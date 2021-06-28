<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGen_cover As eProduct.Gen_cover


'% insPreDP035A: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP035A()
	'--------------------------------------------------------------------------------------------
	Dim lclsGen_cover As eProduct.Gen_cover
	Dim lcolGen_cover As Object
	Dim sRequire As Object
	Dim sDefaulti As Object
	Dim sProrate As Object
	Dim sDevoallo As Object
	lclsGen_cover = New eProduct.Gen_cover
	With mobjValues
		Call lclsGen_cover.Find(.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), .StringToDate(Session("dEffecdate")))
	End With
	With lclsGen_cover
		
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("    <TABLE width=""100%"">     " & vbCrLf)
Response.Write("    	<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=14485>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkFranchiseReq", GetLocalResourceObject("chkFranchiseReqCaption"), CStr(False),  ,  ,  ,  , GetLocalResourceObject("chkFranchiseReqToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=14505>" & GetLocalResourceObject("cbeFranchiseTypCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.PossiblesValues("cbeFranchiseTyp", "table64", 1, .sFrantype,  ,  ,  ,  ,  , "DisabledFranchise();",  ,  , GetLocalResourceObject("cbeFranchiseTypToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=14497>" & GetLocalResourceObject("cbeFranchiseAplCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.PossiblesValues("cbeFranchiseApl", "table33", 1, .sFrancApl,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeFranchiseAplToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14503>" & GetLocalResourceObject("tctFranchiseRouCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctFranchiseRou", 12, .sRoufranc,  , GetLocalResourceObject("tctFranchiseRouToolTip"),  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=15%><LABEL ID=14502>" & GetLocalResourceObject("tcnFranchiseRateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnFranchiseRate", 4, CStr(.nFrancrat),  , GetLocalResourceObject("tcnFranchiseRateToolTip"),  , 2,  ,  ,  , "DisabledRate();", True))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD WIDTH = 15%><LABEL ID=14498>" & GetLocalResourceObject("tcnFranchiseFixCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnFranchiseFix", 10, CStr(.nFrancFix),  , GetLocalResourceObject("tcnFranchiseFixToolTip"), True, 0,  ,  ,  , "DisabledFix();", True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14501>" & GetLocalResourceObject("tcnFranchiseMinCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnFranchiseMin", 10, CStr(.nFrancMin),  , GetLocalResourceObject("tcnFranchiseMinToolTip"), True,  ,  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD><LABEL ID=14500>" & GetLocalResourceObject("tcnFranchiseMaxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnFranchiseMax", 10, CStr(.nFrancMax),  , GetLocalResourceObject("tcnFranchiseMaxToolTip"), True,  ,  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH = ""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=1448>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""6"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH = 22%>")


Response.Write(mobjValues.CheckControl("chkFranchiseAdd", GetLocalResourceObject("chkFranchiseAddCaption"), CStr(False),  , "DisabledAddSub();",  ,  , GetLocalResourceObject("chkFranchiseAddToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>%")


Response.Write(mobjValues.NumericControl("tcnFranchiseAdd", 6, CStr(.nFDRateAdd),  , GetLocalResourceObject("tcnFranchiseAddToolTip"), True, 2,  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("			<TD WIDTH = 22%>")


Response.Write(mobjValues.CheckControl("chkFranchiseSub", GetLocalResourceObject("chkFranchiseSubCaption"), CStr(False),  , "DisabledAddSub();",  ,  , GetLocalResourceObject("chkFranchiseSubToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>%")


Response.Write(mobjValues.NumericControl("tcnFranchiseSub", 6, CStr(.nFDRateSub),  , GetLocalResourceObject("tcnFranchiseSubToolTip"), True, 2,  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("			<TD><LABEL ID=14499>" & GetLocalResourceObject("tcnFranchiseLevCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnFranchiseLev", 5, CStr(.nFDUserLev),  , GetLocalResourceObject("tcnFranchiseLevToolTip"),  , 0))


Response.Write("</TD> " & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	</TABLE>	" & vbCrLf)
Response.Write("    <TABLE WIDTH = ""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=14484>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("            <TD WIDTH=45%>")


Response.Write(mobjValues.CheckControl("chkAutomaticRep", GetLocalResourceObject("chkAutomaticRepCaption"), CStr(False),  ,  ,  ,  , GetLocalResourceObject("chkAutomaticRepToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH= 15%><LABEL ID=14506>" & GetLocalResourceObject("tcnMediumValueCaption") & "</LABEL></TD>                           " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnMediumValue", 18, CStr(.nMedreser),  , GetLocalResourceObject("tcnMediumValueToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14507>" & GetLocalResourceObject("tctReserveRouCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctReserveRou", 12, .sRoureser,  , GetLocalResourceObject("tctReserveRouToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>	")

		
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			If Not Session("bQuery") Then
				.Write("Values(" & mobjValues.StringToType(lclsGen_cover.sFDRequire, eFunctions.Values.eTypeData.etdDouble, 0) & "," & mobjValues.StringToType(lclsGen_cover.sFDChantyp, eFunctions.Values.eTypeData.etdDouble, 0) & "," & mobjValues.StringToType(lclsGen_cover.sAutomrep, eFunctions.Values.eTypeData.etdDouble, 0) & ");")
				.Write("DisabledFranchise();")
				.Write("DisabledFix();")
				.Write("DisabledRate();")
				.Write("DisabledAddSub();")
			End If
			.Write("</" & "Script>")
		End With
	End With
	lclsGen_cover = Nothing
	lcolGen_cover = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGen_cover = New eProduct.Gen_cover

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "dp035a"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:04 $"

//+ Inhabilita todos los campos del frame Franquicia/Deducible y Cambios
//---------------------------------------------------------------------------
function DisabledFranchise()
//---------------------------------------------------------------------------
{   with (self.document.forms[0])
    {
        if (cbeFranchiseTyp.value == 1)
        {
//+ Colocar los valores en cero
            cbeFranchiseApl.value  = 0;
            chkFranchiseReq.checked = false;
            tctFranchiseRou.Value = "";
            tcnFranchiseFix.value  = 0;
            tcnFranchiseRate.value  = 0;
            tcnFranchiseMin.value  = 0;
            tcnFranchiseMax.value  = 0;
            chkFranchiseAdd.checked = false;
            chkFranchiseSub.checked = false;
            tcnFranchiseAdd.value  = 0;
            tcnFranchiseSub.value  = 0;
            tcnFranchiseLev.value  = 0;
//+ Inhabilitar los campos
            cbeFranchiseApl.disabled = true;
            chkFranchiseReq.disabled = true;
            tctFranchiseRou.disabled = true;
            tcnFranchiseFix.disabled = true;
            tcnFranchiseRate.disabled = true;
            tcnFranchiseMin.disabled = true;
            tcnFranchiseMax.disabled = true;
            chkFranchiseAdd.disabled = true;
            chkFranchiseSub.disabled = true;
            tcnFranchiseAdd.disabled = true;
            tcnFranchiseSub.disabled = true;
            tcnFranchiseLev.disabled = true;
        }
        if (cbeFranchiseTyp.value != 1)
//+ Inhabilitar los campos
        {
            cbeFranchiseApl.disabled = false;
            chkFranchiseReq.disabled = false;
            tctFranchiseRou.disabled = false;
            tcnFranchiseFix.disabled = false;
            tcnFranchiseRate.disabled = false;
            chkFranchiseAdd.disabled = false;
            chkFranchiseSub.disabled = false;
            tcnFranchiseAdd.disabled = false;
            tcnFranchiseSub.disabled = false;
            tcnFranchiseLev.disabled = false;
        }
    }
}

//+ DisabledFix : Deshabilita o habilita los campos de acuerdo de tcnFranchiseFix
//---------------------------------------------------------------------------
function DisabledFix()
//---------------------------------------------------------------------------
{	
	with (self.document.forms[0])
    {
	    if (tcnFranchiseFix.value==0)
	    {	
	    	tcnFranchiseRate.disabled = false;
	    	tcnFranchiseMin.disabled = true;
	    	tcnFranchiseMax.disabled = true;		
	    	
	    }
	    if (insConvertNumber(tcnFranchiseFix.value) > 0)
	    {	
	    	tcnFranchiseRate.value = 0;
	    	tcnFranchiseMin.value = 0;
	    	tcnFranchiseMax.value = 0;
	    	tcnFranchiseRate.disabled = true;
	    	tcnFranchiseMin.disabled = true;
	    	tcnFranchiseMax.disabled = true;		
	    }
	    if(cbeFranchiseTyp.value==1)
			tcnFranchiseFix.disabled = true
		else
			tcnFranchiseFix.disabled = false;
    }	
}	
//+ DisabledRate : Deshabilita o habilita los campos de acuerdo de tcnFranchiseRate
//---------------------------------------------------------------------------
function DisabledRate(){
//---------------------------------------------------------------------------
	with (self.document.forms[0]){
	    if (insConvertNumber(tcnFranchiseRate.value)>0){	
	    	tcnFranchiseFix.value = 0;
	    	tcnFranchiseFix.disabled = true;
	    	tcnFranchiseMin.disabled = false;
	    	tcnFranchiseMax.disabled = false;		
	    }
	    if ((tcnFranchiseRate.value==0 && 
	        tcnFranchiseFix.value==0)||
	        tcnFranchiseRate.value==''){
			tcnFranchiseMin.value=0;
	    	tcnFranchiseMax.value=0;
	    	tcnFranchiseMin.disabled = true;
	    	tcnFranchiseMax.disabled = true;
	    	tcnFranchiseFix.disabled = false;
	    }
	    if(cbeFranchiseTyp.value==1)
			tcnFranchiseRate.disabled = true
		else
			tcnFranchiseRate.disabled = false;
    }
}
//+ DisabledAddSub : Deshabilita o habilita los campos de acuerdo de chkFranchiseAdd
//---------------------------------------------------------------------------
function DisabledAddSub()	
//---------------------------------------------------------------------------
{   with (self.document.forms[0])
    {
	    if (chkFranchiseAdd.checked == true)
	        {tcnFranchiseAdd.disabled = false;}
	    else
	        {tcnFranchiseAdd.disabled = true;
	         tcnFranchiseAdd.value = "";}
	    if (chkFranchiseSub.checked == true)
	        {tcnFranchiseSub.disabled = false;}
	    else
	        {tcnFranchiseSub.disabled = true;
	         tcnFranchiseSub.value = "";}
	}
}
//+  Values : Deshabilita o habilita los campos de acuerdo a sFDRequire,sFDChantyp,sAutomrep
//---------------------------------------------------------------------------
function Values(sFDRequire,sFDChantyp,sAutomrep)
//---------------------------------------------------------------------------
{	with (self.document.forms[0])
    {
        if (sFDRequire == 1)
	    	{chkFranchiseReq.checked = true;}

	    if (sAutomrep == 1)
	    	{chkAutomaticRep.checked = true;}

	    if (sFDChantyp == 2 || sFDChantyp == 4)
	    	{chkFranchiseAdd.checked = true;}

	    if (sFDChantyp == 3 || sFDChantyp == 4)
	    	{chkFranchiseSub.checked = true;}
    }
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">





    <%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP035A", "DP035A.aspx"))
		mobjMenu = Nothing
	End If
End With

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP035A" ACTION="valCoverseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%
Response.Write(mobjValues.ShowWindowsName("DP035A"))
Call insPreDP035A()
%>
</FORM>
</BODY>
</HTML>




