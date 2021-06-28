<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de la página.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim lstrAction As Object
    
</script>    

<%
    Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    
    mobjValues.sCodisplPage = "dp030a"
    
    lstrAction = Request.QueryString.Item("nMainAction")
    
    mobjValues.ActionQuery = lstrAction = eFunctions.Menues.TypeActions.clngActionQuery Or lstrAction = eFunctions.Menues.TypeActions.clngActionDuplicate Or lstrAction = eFunctions.Menues.TypeActions.clngActioncut
    
 %>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>

<%
    Response.Write(mobjValues.StyleSheet())
    Response.Write(mobjMenu.setZone(2, "DP030A", "DP030A.aspx"))
    Response.Write(mobjValues.ShowWindowsName("DP030A"))

%>

<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:56 $"
       
//InsChangeOptPay : se controla la activación del monto
//------------------------------------------------------------------------------------------
function InsChangeoptCapital(Field){
	with (self.document.forms[0]){
		if (Field.value == '1' ) {
			tcnCapitalFix.value = '';
			tcnCapitalFix.disabled = true;
		}
		else
		if (Field.value == '2' ) {
			tcnCapitalFix.value = '';
			tcnCapitalFix.disabled = true;
		}
		else tcnCapitalFix.disabled = false;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP030A" ACTION="valCoverSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%  
    
    Dim lclsTab_GenCov As eProduct.Tab_gencov
    Dim lstrCapitalFix As String
    
    lclsTab_GenCov = New eProduct.Tab_gencov
    lclsTab_GenCov.Find(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))
    
    If lclsTab_GenCov.nCacalfix <> eRemoteDB.Constants.intNull And lclsTab_GenCov.nCacalfix <> eRemoteDB.Constants.dblNull And lclsTab_GenCov.nCacalfix <> 0 Then
        lstrCapitalFix = "1"
    End If
%>  
        <TABLE WIDTH="100%">
            <TR>
                <TD>&nbsp;</TD>
                <TD>
                    <%=mobjValues.CheckControl("chkIndex", GetLocalResourceObject("chkIndexCaption"), lclsTab_GenCov.sCacalrei, , , , , GetLocalResourceObject("chkIndexToolTip"))%>
                </TD>
                <TD><LABEL ID=14143><%=GetLocalResourceObject("tctCapitalRouCaption")%></LABEL></TD>
                <TD>
                    <%= mobjValues.TextControl("tctCapitalRou", 12, lclsTab_GenCov.sRoucapit, , GetLocalResourceObject("tctCapitalRouToolTip"))%>
                </TD>
            </TR>
            <TR>
                <TD>&nbsp;</TD>
                <TD>
                    <%= mobjValues.OptionControl(100372, "optCapital", GetLocalResourceObject("optCapital_1Caption"), lclsTab_GenCov.sCacalfri, "1", "InsChangeoptCapital(this);", , , GetLocalResourceObject("optCapital_1ToolTip")) %>
                </TD>
            </TR>
            <TR>
                <TD>&nbsp;</TD>
                <TD>
                    <%= mobjValues.OptionControl(100373, "optCapital", GetLocalResourceObject("optCapital_2Caption"), lclsTab_GenCov.sCacalili, "2", "InsChangeoptCapital(this);", , , GetLocalResourceObject("optCapital_2ToolTip")) %>
                </TD>
            </TR>
            <TR>
                <TD>&nbsp;</TD>
                <TD>
                    <%= mobjValues.OptionControl(100374, "optCapital", GetLocalResourceObject("optCapital_3Caption"), lstrCapitalFix, "3", "InsChangeoptCapital(this);", , , GetLocalResourceObject("optCapital_3ToolTip")) %>
                </TD>
                <TD><LABEL ID=14142><%=GetLocalResourceObject("tcnCapitalFixCaption") %></LABEL></TD>
                <TD>
                    <%= mobjValues.NumericControl("tcnCapitalFix", 12, CStr(lclsTab_GenCov.nCacalfix), , GetLocalResourceObject("tcnCapitalFixToolTip"), True, 2) %>
                </TD>
            </TR>
        </TABLE>
    </FORM>
</BODY>
</HTML>
        
<%        
    mobjMenu = Nothing
    mobjValues = Nothing
    lclsTab_GenCov = Nothing
%>




