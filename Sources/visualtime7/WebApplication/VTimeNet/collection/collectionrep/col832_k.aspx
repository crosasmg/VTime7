<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility
Dim miniYear As Object
Dim miniMonth As Object
Dim mperYear As Object
Dim mperMonth As Object


</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("COL832_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "COL832_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
miniYear = DatePart(Microsoft.VisualBasic.DateInterval.Year, Today)
miniMonth = DatePart(Microsoft.VisualBasic.DateInterval.Month, Today)
mperYear = DatePart(Microsoft.VisualBasic.DateInterval.Year, Today)
mperMonth = DatePart(Microsoft.VisualBasic.DateInterval.Month, Today)

%>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 7/01/04 16:40 $|$$Author: Nvaplat15 $"
    </SCRIPT>


<HTML>
<HEAD>
<SCRIPT>

//% insStateZone: se manejan los campos de la página
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("COL832", "COL832_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAutoAnnulment" ACTION="valCollectionRep.aspx?mode=1">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL832"))%>
    <TABLE WIDTH="100%">
        <BR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <BR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD WIDTH="20%"></TD>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="Horline"></TD>
            <TD></TD>
            <TD COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        <TR><TD>&nbsp;</TD></TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeIniMonthCaption") %></LABEL></TD>
			<TD><%mobjValues.TypeOrder = 1
mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeIniMonth", "table7013", eFunctions.Values.eValuesType.clngComboType, miniMonth,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeIniMonthToolTip")))%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnIniYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnIniYear", 4, miniYear,  , GetLocalResourceObject("tcnIniYearToolTip"))%></TD>
            <TD></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeIniMonthCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeOrder = 1
mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbePerMonth", "table7013", eFunctions.Values.eValuesType.clngComboType, mperMonth,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbePerMonthToolTip")))%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnIniYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPerYear", 4, mperYear,  , GetLocalResourceObject("tcnPerYearToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("COL832_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




