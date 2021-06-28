<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjOptionInstall As eGeneral.OptionsInstallation


'**********************************************************************************************************
'% insPreMCO001 : define la estructura de la página "pintando" los campos puntuales 
'--------------------------------------------------------------------------------------------------
Private Function insPreMCO001() As Object
	'--------------------------------------------------------------------------------------------------
	
       
Response.Write("" & vbCrLf)
        Response.Write("    <TABLE border=""0"" WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("cbePrevReceiptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.PossiblesValues("cbePrevReceipt", "Table693", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nPreReceiptPrem),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbePrevReceiptToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.CheckControl("chkPartialCol", GetLocalResourceObject("chkPartialColCaption"), mobjOptionInstall.sParCollectPrem, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	    	<TD COLSPAN=""3"">")

	Response.Write(mobjValues.CheckControl("chkAmountReq", GetLocalResourceObject("chkAmountReqCaption"), mobjOptionInstall.sReqAmoPrem, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("	    	<TD COLSPAN=""2"">")
        
        'Nuevo Campo - Fecha Fija de Valorización de Caja
        Response.Write(mobjValues.CheckControl("chkDateFix_Cash", GetLocalResourceObject("chkDateFix_CashCaption"), mobjOptionInstall.sDateFix_Cash, CStr(1)))
        
        Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)

        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""6"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption1") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""6"" CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        
        
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=11735>" & GetLocalResourceObject("cbeCurrcollectexpCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD >")

        Response.Write(mobjValues.PossiblesValues("cbenTolerCurr", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nTolerCurr), , , , , , , CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401, , GetLocalResourceObject("cbeCurrcollectexpToolTip")))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=11735>" & GetLocalResourceObject("cbeTollerCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD >")

        Response.Write(mobjValues.PossiblesValues("cbenToler", "table1000", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCodToler), , , , , , , CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401, , GetLocalResourceObject("cbeTollerToolTip")))
        Response.Write("</TD>" & vbCrLf)
        
        
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        
        
        
        
        
Response.Write("			<TD COLSPAN=""2"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCollAddCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


        Response.Write(mobjValues.NumericControl("tcnCollAdd", 18, CStr(mobjOptionInstall.nUpper_limPrem), , GetLocalResourceObject("tcnCollAddToolTip"), True, 6, , , , , CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCollAddCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.NumericControl("tcnUpper_Agree", 18, CStr(mobjOptionInstall.nUpper_lim_Agree),  , GetLocalResourceObject("tcnUpper_AgreeToolTip"), True, 6,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCollSubCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.NumericControl("tcnCollSub", 18, CStr(mobjOptionInstall.nLower_limPrem),  , GetLocalResourceObject("tcnCollSubToolTip"), True, 6,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))

        
        Response.Write("</TD>" & vbCrLf)
        Response.Write("	    	<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCollSubCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	    	<TD>")

        Response.Write(mobjValues.NumericControl("tcnLower_Agree", 18, CStr(mobjOptionInstall.nLower_lim_Agree), , GetLocalResourceObject("tcnLower_AgreeToolTip"), True, 6, , , , , CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPercAddCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	    	<TD>")
        
        Response.Write("% " & mobjValues.NumericControl("tcnUpperPercent", 3, CStr(mobjOptionInstall.nUpperPercent), , GetLocalResourceObject("tcnCollPercentToolTip"), False, False, , , , "inschangePercent(this)", CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401))
        Response.Write("<LABEL>  " & GetLocalResourceObject("tcnMAXAmount") & "  </LABEL>" & mobjValues.NumericControl("tcnUpperPercentAMO", 18, CStr(mobjOptionInstall.nUpperPercentAMO), , GetLocalResourceObject("tcnCollPercentToolTip"), False, 6, , , , , IIf(mobjOptionInstall.nUpperPercent = 0, True, IIf(CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401, True, False))))
        
        Response.Write("</TD>" & vbCrLf)
        Response.Write("	    	<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPercAddCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	    	<TD>")
    
        Response.Write("% " & mobjValues.NumericControl("tcnUpperPercentAgree", 3, CStr(mobjOptionInstall.nUpperPercentAgree), , GetLocalResourceObject("tcnCollPercentToolTip"), False, False, , , , "inschangePercent(this)", CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401))
        Response.Write("<LABEL>  " & GetLocalResourceObject("tcnMAXAmount") & "  </LABEL>" & mobjValues.NumericControl("tcnUpperPercentAgreeAMO", 18, CStr(mobjOptionInstall.nUpperPercentAgreeAMO), , GetLocalResourceObject("tcnCollPercentToolTip"), False, 6, , , , , IIf(mobjOptionInstall.nUpperPercentAgree = 0, True, IIf(CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401, True, False))))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)

        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPercSubCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	    	<TD>")
    
        Response.Write("% " & mobjValues.NumericControl("tcnLowerPercent", 3, CStr(mobjOptionInstall.nLowerPercent), , GetLocalResourceObject("tcnCollPercentToolTip"), False, False, , , , "inschangePercent(this)", CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401))
        Response.Write("<LABEL>  " & GetLocalResourceObject("tcnMAXAmount") & "  </LABEL>" & mobjValues.NumericControl("tcnLowerPercentAMO", 18, CStr(mobjOptionInstall.nLowerPercentAMO), , GetLocalResourceObject("tcnCollPercentToolTip"), False, 6, , , , , IIf(mobjOptionInstall.nLowerPercent = 0, True, IIf(CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401, True, False))))
        
        Response.Write("</TD>" & vbCrLf)
        Response.Write("	    	<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPercSubCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	    	<TD>")
    
        Response.Write("% " & mobjValues.NumericControl("tcnLowerPercentAgree", 3, CStr(mobjOptionInstall.nLowerPercentAgree), , GetLocalResourceObject("tcnCollPercentToolTip"), False, False, , , , "inschangePercent(this)", CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401))
        Response.Write("<LABEL>  " & GetLocalResourceObject("tcnMAXAmount") & "  </LABEL>" & mobjValues.NumericControl("tcnLowerPercentAgreeAMO", 18, CStr(mobjOptionInstall.nLowerPercentAgreeAMO), , GetLocalResourceObject("tcnCollPercentToolTip"), False, 6, , , , , IIf(mobjOptionInstall.nLowerPercentAgree = 0, True, IIf(CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401, True, False))))
        
Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	")

	If mobjOptionInstall.nAcc_bankPrem = eRemoteDB.Constants.intNull Then
Response.Write("" & vbCrLf)
Response.Write("	    	   <TD WIDTH=""25%"">")


Response.Write(mobjValues.PossiblesValues("valAccount", "TabBank_Acc", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401, 4, GetLocalResourceObject("valAccountToolTip"), eFunctions.Values.eTypeCode.eString))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	")

	Else
Response.Write("" & vbCrLf)
Response.Write("	    	   <TD WIDTH=""25%"">")


Response.Write(mobjValues.PossiblesValues("valAccount", "TabBank_Acc", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjOptionInstall.nAcc_bankPrem),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401, 4, GetLocalResourceObject("valAccountToolTip"), eFunctions.Values.eTypeCode.eString))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	")

	End If
Response.Write("" & vbCrLf)
Response.Write("	    	<TD>&nbsp;</TD>	    " & vbCrLf)
Response.Write("            <TD><LABEL ID=11735>" & GetLocalResourceObject("cbeCurrcollectexpCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD >")


Response.Write(mobjValues.PossiblesValues("cbeCurrcollectexp", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCurrcollectexpPrem),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbeCurrcollectexpToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.ClientControl("tctClient", mobjOptionInstall.sClient,  , GetLocalResourceObject("tctClientToolTip"),  ,  , "lblCliename",  ,  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD>&nbsp;</TD>		    	" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCollect_expCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.NumericControl("tcnCollect_exp", 18, CStr(mobjOptionInstall.nCollect_expPrem),  , GetLocalResourceObject("tcnCollect_expToolTip"), True, 6,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""5"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""5"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD >")


Response.Write(mobjValues.CheckControl("chkNullTransac", GetLocalResourceObject("chkNullTransacCaption"), mobjOptionInstall.sTechAffectPrem, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>			" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""5"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("Anchor6Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""5"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeCalIntCaption") & "</LABEL></TD>	    	" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCalInt", "Table694", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nIntCalcPrem),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbeCalIntToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnCalIntFixCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.NumericControl("tcnCalIntFix", 8, CStr(mobjOptionInstall.nFixIntPrem),  , GetLocalResourceObject("tcnCalIntFixToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.CheckControl("chkCalIntAdd", GetLocalResourceObject("chkCalIntAddCaption"), mobjOptionInstall.sMod_upLimPrem, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCalIntFixCaption") & "</LABEL>" & vbCrLf)
Response.Write("	    	    ")


Response.Write(mobjValues.NumericControl("tcnCalIntAdd", 8, CStr(mobjOptionInstall.nUpperIntPrem),  , GetLocalResourceObject("tcnCalIntAddToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("" & vbCrLf)
Response.Write("	    	</TD>" & vbCrLf)
Response.Write("	    	<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.CheckControl("chkCalIntSub", GetLocalResourceObject("chkCalIntSubCaption"), mobjOptionInstall.sMod_loLimPrem, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnCalIntFixCaption") & "</LABEL>" & vbCrLf)
Response.Write("	    		")


Response.Write(mobjValues.NumericControl("tcnCalIntSub", 8, CStr(mobjOptionInstall.nLowerIntPrem),  , GetLocalResourceObject("tcnCalIntSubToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("" & vbCrLf)
Response.Write("	    	</TD>" & vbCrLf)
Response.Write("        	<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnCalIntLevCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.NumericControl("tcnCalIntLev", 8, CStr(mobjOptionInstall.nAmenLevelPrem),  , GetLocalResourceObject("tcnCalIntLevToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	</TABLE>")

End Function

</script>
<%Response.Expires = -1

'+ Se instancian los objetos necesarios para trabajr las particularidades de creación de la forma por rutinas genéricas

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjOptionInstall = New eGeneral.OptionsInstallation

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MCO001"
%> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%="<SCRIPT LANGUAGE=""JavaScript"">"%>
var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;

function inschangePercent (obj){
    switch (obj.name){
        case 'tcnUpperPercent':
            self.document.forms[0].tcnUpperPercentAMO.disabled=false;
            break;
        case 'tcnUpperPercentAgree':
            self.document.forms[0].tcnUpperPercentAgreeAMO.disabled=false;
            break;
        case 'tcnLowerPercent':
            self.document.forms[0].tcnLowerPercentAMO.disabled=false;
            break;
        case 'tcnLowerPercentAgree':
            self.document.forms[0].tcnLowerPercentAgreeAMO.disabled=false;
            break;
        default:
    }
}


</SCRIPT>
<HTML> 
<HEAD>
	<META NAME = "GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 19/04/04 16:14 $|$$Author: Nvaplat40 $"
	</SCRIPT>


	<%=mobjValues.StyleSheet()%>
<TITLE>Generalidades de las opciones de instalación</TITLE>
</HEAD>
	
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MCO001", "MCO001.aspx"))
End If
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="POST" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" id=form1 name=form1>
<%

'+ Se realiza la lectura de los valores cargados en la tabla de la opciones de instalación cobranza
mobjOptionInstall.insPreMCO001()
Call insPreMCO001()
%>
</BODY>
</HTML>






