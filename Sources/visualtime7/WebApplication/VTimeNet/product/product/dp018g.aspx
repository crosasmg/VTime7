<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de la página.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Dim mobjGeneral As eGeneral.GeneralFunction

Dim mclsTab_lifcov As eProduct.Tab_lifcov
Dim mstrError As String

Dim nFields As String
    Dim lclsGeneral As eGeneral.GeneralFunction
    
    Dim lstrAction As Object


</script>
<%Response.Expires = -1
    mobjGeneral = New eGeneral.GeneralFunction
    mclsTab_lifcov = New eProduct.Tab_lifcov

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues

    mstrError = mobjGeneral.insLoadMessage(55892)

    lstrAction = Request.QueryString.Item("nMainAction")

    mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or
                             Not (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Or
                             Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut) Or
                             Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Or
                             Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionadd))

    Session("nMainAction") = Request.QueryString.Item("nMainAction")

    Call mclsTab_lifcov.Find(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))

    mobjValues.sCodisplPage = "dp018g"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "DP018G", "DP018G.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 21/10/03 15:39 $|$$Author: Nvaplat26 $"

//% ValidateLength: Se encarga de enviar un error en caso de que se ingrese una cantidad mayor
//% de caracteres para el campo descripción de la que está definida en la B.D.
//--------------------------------------------------------------------------------------------
function ValidateLength(Field, nMaxAllowed){
//--------------------------------------------------------------------------------------------
	if (self.document.forms[0].tctDescript.value.length > nMaxAllowed) {
		alert("55892: " + "<%=mstrError%>")
		Field.focus();
	}
}

//% EnabledStatus: se maneja el estado del campo "Estado"
//--------------------------------------------------------------------------------------------
function EnabledStatus(){
//--------------------------------------------------------------------------------------------
	var bValid = false;
	var Array = top.frames['fraSequence'].sequence;
	for(var lintIndex=0; lintIndex<Array.length; lintIndex++){
		if(Array[lintIndex].Require=="2" ||
		   Array[lintIndex].Require=="5")
			bValid=true;
	}

   self.document.forms[0].cbeStatregt.disabled = bValid;

}

//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tctsProvider.disabled = !chksInforProv.checked;
		tctsProvider_Digit.disabled = tctsProvider.disabled;
		btntctsProvider.disabled = tctsProvider.disabled;
		if (tctsProvider.disabled==true) {
	       tctsProvider.value = '';
	       tctsProvider_Digit.value = '';
	       UpdateDiv('lblCliename','');
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
lclsGeneral = New eGeneral.GeneralFunction

nFields = lclsGeneral.insPrecision("Tab_lifcov", "sdescript")
%>
<FORM METHOD="post" ID="FORM" NAME="frmDP018G" ACTION="valCoverSeq.aspx?nMainAction= <% = Request.querystring("nMainAction") %>"> 
    <P ALIGN="Center">
        <LABEL ID=100354><A HREF="#Ramos"><%= GetLocalResourceObject("AnchorRamosCaption") %></A></LABEL>
	</P>
	<%=mobjValues.ShowWindowsName("DP018G")%>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=14073><%= GetLocalResourceObject("tctDescriptCaption") %></LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.TextAreaControl("tctDescript", 2, 60, mclsTab_lifcov.sDescript,  , GetLocalResourceObject("tctDescriptToolTip"), , , 1, "ValidateLength(this," & nFields & ");")%></TD>        
        </TR>
        <TR>
            <TD COLSPAN="2"></TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100355><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"></TD>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=14074><%= GetLocalResourceObject("tctShortDesCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctShortDes", 30, mclsTab_lifcov.sShort_des,  , GetLocalResourceObject("tctShortDesToolTip"),  ,  ,  ,  ,  , 2)%></TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(100288, "optClas", GetLocalResourceObject("optClas_1Caption"), CStr(2 - CShort(mclsTab_lifcov.sCoveruse)), "1",  ,  , 5, GetLocalResourceObject("optClas_1ToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=14076><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTab_lifcov.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 3)%></TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(100290, "optClas", GetLocalResourceObject("optClas_3Caption"), CStr(4 - CShort(mclsTab_lifcov.sCoveruse)), "3",  ,  , 6, GetLocalResourceObject("optClas_3ToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14075><%= GetLocalResourceObject("cbeStatregtCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
                    Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, mclsTab_lifcov.sStatregt, , , , , , , , , GetLocalResourceObject("cbeStatregtToolTip"), , 4))
                %>
			</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(100887, "optClas", GetLocalResourceObject("optClas_2Caption"), CStr(3 - CShort(mclsTab_lifcov.sCoveruse)), "2",  ,  , 7, GetLocalResourceObject("optClas_2ToolTip"))%></TD>
		</TR>
        <TR>
			<TD COLSPAN="5">&nbsp;</TD>
		</TR>
        <TR>
            <TD><LABEL ID=14082><%= GetLocalResourceObject("cbeInsuriniCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeInsurini", "table40", eFunctions.Values.eValuesType.clngComboType, mclsTab_lifcov.sInsurini,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInsuriniToolTip"),  , 8)%></TD>
            
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100356><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
        <TR>
            <TD><LABEL ID=14074><%= GetLocalResourceObject("tctsCondSVSCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctsCondSVS", 30, mclsTab_lifcov.sCondSVS,  , GetLocalResourceObject("tctsCondSVSToolTip"),  ,  ,  ,  ,  , 9)%></TD>
            
			<TD><LABEL ID=14081><%= GetLocalResourceObject("tctReserCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctReser", 12, mclsTab_lifcov.sRoureser,  , GetLocalResourceObject("tctReserToolTip"),  ,  ,  ,  ,  , 12)%></TD>
		</TR>
        <TR>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chksInforProv", GetLocalResourceObject("chksInforProvCaption"), mclsTab_lifcov.sInforProv, "1", "InsChangeField()",  , 10, GetLocalResourceObject("chksInforProvToolTip"))%></TD>
            <TD><LABEL ID=14072><%= GetLocalResourceObject("tctRescueCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctRescue", 12, mclsTab_lifcov.sRousurre,  , GetLocalResourceObject("tctRescueToolTip"),  ,  ,  ,  ,  , 13)%></TD>
		</TR>
        <TR>
            <TD COLSPAN="1"><LABEL ID=14074><%= GetLocalResourceObject("tctsProviderCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.ClientControl("tctsProvider", mclsTab_lifcov.sProvider, True, GetLocalResourceObject("tctsProviderToolTip"),  , mclsTab_lifcov.sInforProv = "2", "lblCliename",  ,  ,  ,  ,  , 11, True)%></TD>
		</TR>
    </TABLE>
    <TABLE WIDTH=100%>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100357><A NAME="Ramos"><%= GetLocalResourceObject("AnchorRamos2Caption") %></A></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
        <TR>
            <TD><LABEL ID=14077><%= GetLocalResourceObject("cbeBranch_ledCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch_led", "Table75", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTab_lifcov.nBranch_led),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranch_ledToolTip"),  , 14)%></TD>
            <TD><LABEL ID=14078><%= GetLocalResourceObject("cbeBranch_reiCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch_rei", "Table5000", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTab_lifcov.nBranch_rei),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranch_reiToolTip"),  , 15)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranch_estCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch_est", "Table71", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTab_lifcov.nBranch_est),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranch_estToolTip"),  , 16)%></TD>
            <TD><LABEL ID=14080><%= GetLocalResourceObject("cbeBranch_genCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch_gen", "Table634", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTab_lifcov.nBranch_gen),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranch_genToolTip"),  , 17)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9634><%= GetLocalResourceObject("cbeClaimTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeClaimType", "Table210", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTab_lifcov.nCla_li_typ),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeClaimTypeToolTip"))%></TD>
        </TR>	
    </TABLE>
    
	<%=mobjValues.BeginPageButton%>
</FORM>
</BODY>
</HTML>
<%
    mclsTab_lifcov = Nothing
    If Not mobjValues.ActionQuery Then
        Response.Write("<SCRIPT>EnabledStatus()</SCRIPT>")
    End If
%>






