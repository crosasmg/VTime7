<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues


    '% insPreDP031: Se controla el ingreso a la transacción
    '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub insPreDP031()
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lstrCheckedCapital As String
        Dim lstrCheckedClaim As String
        Dim lstrCheckedNot As String
        Dim lstrCheckedNotAplied As String
        Dim lstrCheckedFran As String
        Dim lstrCheckedDeduc As String
        Dim lclsProduct_ge As eProduct.Product_ge
        Dim lclsProduct As eProduct.Product
        Dim lstrPrescri As Object
	
        lstrCheckedCapital = "2"
        lstrCheckedClaim = "2"
        lstrCheckedNot = "2"
        lstrCheckedNotAplied = "2"
        lstrCheckedFran = "2"
        lstrCheckedDeduc = "2"
	
        lclsProduct_ge = New eProduct.Product_ge
        lclsProduct = New eProduct.Product
	
        Call lclsProduct.Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
        Call lclsProduct_ge.Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
        Response.Write("" & vbCrLf)
        Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14408>" & GetLocalResourceObject("tcnClaimpresCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnClaimpres", 4, CStr(lclsProduct.nClaim_pres), , GetLocalResourceObject("tcnClaimpresToolTip"), , 0))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14408>" & GetLocalResourceObject("tcnClaimnoticeCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnClaimnotice", 4, CStr(lclsProduct.nClaim_Notice), , GetLocalResourceObject("tcnClaimnoticeToolTip"), , 0))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14408>" & GetLocalResourceObject("tcnClaimpayCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnClaimpay", 4, CStr(lclsProduct.nClaim_Pay), , GetLocalResourceObject("tcnClaimpayToolTip"), , 0))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeLineSuscripCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cbeLineSuscrip", "table5009", eFunctions.Values.eValuesType.clngComboType, CStr(lclsProduct_ge.nComerLine), , , , , , , , , GetLocalResourceObject("cbeLineSuscripToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">")


        Response.Write(mobjValues.CheckControl("chkSuspendi", GetLocalResourceObject("chkSuspendiCaption"), lclsProduct_ge.DefaultValueDP031("Suspendi"), "1", , , , GetLocalResourceObject("chkSuspendiToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14409>" & GetLocalResourceObject("cboCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD >")


        Response.Write(mobjValues.PossiblesValues("cboCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsProduct_ge.nCurrency), , , , , , , , , GetLocalResourceObject("cboCurrencyToolTip")))


      
        Response.Write("		</TR>" & vbCrLf)
        
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14409>" & GetLocalResourceObject("cbeDuplicatedTypeCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD >")


        Response.Write(mobjValues.PossiblesValues("cbeDuplicatedType", "TABLE9056", eFunctions.Values.eValuesType.clngComboType, CStr(lclsProduct_ge.nDuplicatedType), , , , , , , , , GetLocalResourceObject("cbeDuplicatedTypeTooltip")))


        Response.Write("</TD>" & vbCrLf)
        
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=41316>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14416>" & GetLocalResourceObject("tcnPreissueCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnPreissue", 18, CStr(lclsProduct_ge.nPre_issue), , GetLocalResourceObject("tcnPreissueToolTip"), True, 6))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14417>" & GetLocalResourceObject("tcnPre_amendCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnPre_amend", 18, CStr(lclsProduct_ge.nPre_amend), , GetLocalResourceObject("tcnPre_amendToolTip"), True, 6))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=41313><A NAME=""Siniestros"">" & GetLocalResourceObject("AnchorSiniestrosCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""2"">")


        Response.Write(mobjValues.CheckControl("chkResmaypa", GetLocalResourceObject("chkResmaypaCaption"), lclsProduct_ge.sResmaypa, , "insEnabledField(this, ""LevelPay"");", , , GetLocalResourceObject("chkResmaypaToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41314>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3""></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14414>" & GetLocalResourceObject("tcnLevelpayCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnLevelpay", 5, CStr(lclsProduct_ge.nLevelPay), , GetLocalResourceObject("tcnLevelpayToolTip"), , 0, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">")


        Response.Write(mobjValues.OptionControl(41320, "optDamage", GetLocalResourceObject("optDamage_1Caption"), lclsProduct_ge.DefaultValueDP031("optDamage"), "1", , , , GetLocalResourceObject("optDamage_1ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14415>" & GetLocalResourceObject("cboPayconreCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cboPayconre", "Table60", eFunctions.Values.eValuesType.clngComboType, lclsProduct_ge.sPayconre, , , , , , , , , GetLocalResourceObject("cboPayconreToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">")


        Response.Write(mobjValues.OptionControl(41321, "optDamage", GetLocalResourceObject("optDamage_2Caption"), lclsProduct_ge.DefaultValueDP031("optCost"), "2", , , , GetLocalResourceObject("optDamage_2ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=41315><A NAME=""Franquicia/Deducible"">" & GetLocalResourceObject("AnchorFranquicia/DeducibleCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(41322, "optNoAplied", GetLocalResourceObject("optNoAplied_1Caption"), lclsProduct_ge.DefaultValueDP031("optNoAplied_Not"), "1", "insEnabledField(this, ""NoAplied"");", , , GetLocalResourceObject("optNoAplied_1ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14410>" & GetLocalResourceObject("tcnFixCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnFix", 18, CStr(lclsProduct_ge.nFrancFix), , GetLocalResourceObject("tcnFixToolTip"), True, 6))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(41323, "optNoAplied", GetLocalResourceObject("optNoAplied_2Caption"), lclsProduct_ge.DefaultValueDP031("optNoAplied_F"), "2", "insEnabledField(this, ""F/D"");", , , GetLocalResourceObject("optNoAplied_2ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=41318>" & GetLocalResourceObject("tcnFrancMinCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnFrancMin", 18, CStr(lclsProduct_ge.nFrancMin), , GetLocalResourceObject("tcnFrancMinToolTip"), True, 6))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(41324, "optNoAplied", GetLocalResourceObject("optNoAplied_3Caption"), lclsProduct_ge.DefaultValueDP031("optNoAplied_D"), "3", "insEnabledField(this, ""F/D"");", , , GetLocalResourceObject("optNoAplied_3ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=14416>" & GetLocalResourceObject("tcnFrancRatCaption") & "</LABEL>")


        Response.Write(mobjValues.NumericControl("tcnFrancRat", 4, CStr(lclsProduct_ge.nFrancrat), , GetLocalResourceObject("tcnFrancRatToolTip"), , 2, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=41319>" & GetLocalResourceObject("tcnFrancmaxCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnFrancmax", 10, CStr(lclsProduct_ge.nFrancmax),  , GetLocalResourceObject("tcnFrancmaxToolTip"), True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>" & vbCrLf)
        Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=41317>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"" WIDTH=""40%"">&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(41325, "optCapApl", GetLocalResourceObject("optCapApl_2Caption"), lclsProduct_ge.DefaultValueDP031("optCapApl_Cap"), "2", , , , GetLocalResourceObject("optCapApl_2ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(41327, "optCapApl", GetLocalResourceObject("optCapApl_3Caption"), lclsProduct_ge.DefaultValueDP031("optCapApl_Cla"), "3", , , , GetLocalResourceObject("optCapApl_3ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">")


        Response.Write(mobjValues.OptionControl(41329, "optCapApl", GetLocalResourceObject("optCapApl_1Caption"), lclsProduct_ge.DefaultValueDP031("optCapApl_Not"), "1", , True, , GetLocalResourceObject("optCapApl_1ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>" & vbCrLf)
        Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
        Response.Write("		")


        Response.Write(mobjValues.BeginPageButton)


        Response.Write("" & vbCrLf)
        Response.Write("    </P>")

	
        mobjValues = Nothing
        lclsProduct_ge = Nothing
    End Sub

</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values

    mobjValues.ActionQuery = Session("bQuery")

    mobjValues.sCodisplPage = "DP031"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"

    //% insEnabledField: habilita/deshabilita los campos que dependen del valor de otros
    //-----------------------------------------------------------------------------------------------------------------------
    function insEnabledField(Field, Option) {
        //-----------------------------------------------------------------------------------------------------------------------
        switch (Option) {
            case "LevelPay":
                self.document.forms[0].tcnLevelpay.disabled = (Field.checked) ? false : true;
                break
            case "NoAplied":
                self.document.forms[0].optCapApl[2].checked = true;
                insDisabledFD(true);
                break;
            case "F/D":
                self.document.forms[0].optCapApl[0].checked = true;
                insDisabledFD(false);
                break;
            case "LoadPage":
                //+ Se habilita/deshabilita el frame de Franquicia al cargar la página
                insDisabledFD((self.document.forms[0].optNoAplied[0].checked) ? true : false);
        }
    }

    //% insDisabledFD: habilita/deshabilita los campos del frame "Franquicia/Deducible"
    //-----------------------------------------------------------------------------------------------------------------------
    function insDisabledFD(bLocked) {
        //-----------------------------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            tcnFrancRat.disabled = bLocked;
            tcnFix.disabled = bLocked;
            tcnFrancMin.disabled = bLocked;
            tcnFrancmax.disabled = bLocked;
            optCapApl[0].disabled = bLocked;
            optCapApl[1].disabled = bLocked;
            if (bLocked) {
                tcnFrancRat.value = "";
                tcnFix.value = "";
                tcnFrancMin.value = "";
                tcnFrancmax.value = "";
            }
        }
    }
</script>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <%
        mobjMenu = New eFunctions.Menues
        With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjValues.WindowsTitle("DP031"))
            .Write(mobjMenu.setZone(2, "DP031", "DP031.aspx"))
        End With
        mobjMenu = Nothing
    %>
</head>
<body onunload="closeWindows();">
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
    <form method="POST" id="FORM" name="frmDP031" action="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <p align="Center">
        <label id="41308">
            <a href="#Siniestros">
                <%= GetLocalResourceObject("AnchorSiniestros2Caption") %></a></label><label id="41309">
                    |
                </label>
        <label id="41308">
            <a href="#Franquicia/Deducible">
                <%= GetLocalResourceObject("AnchorFranquicia/Deducible2Caption") %></a></label>
    </p>
    <%
        Call insPreDP031()
    %>
    </form>
</body>
</html>
<%If Not Session("bQuery") Then%>
<script>    insEnabledField("", "LoadPage")</script>
<%End If%>
