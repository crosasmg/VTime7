<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "crl005_k"
%>
<HTML>
<HEAD>

	<%=mobjValues.StyleSheet()%>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}
function insStateZone(){
//--------------------------------------------------------------------------------------------------

}
function insCessionType(Field){
//--------------------------------------------------------------------------------------------------
    if (Field.value == 1 || 
        Field.value == 4)

        with(document.FORM)
		{
			cbeBranchRei.disabled = true;
			cbeBranchRei.value = 0;
		}
    else
        document.FORM.cbeBranchRei.disabled = false;
}
</SCRIPT>

    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL005", "CRL005_K.aspx", 1, ""))
mobjMenu = Nothing
%>

</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCRL005" ACTION="valCoReinsuranRep.aspx?X=1">
    <BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=101681><A NAME="Periodo"><%= GetLocalResourceObject("AnchorPeriodoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HORLine"></TD>
        </TR>
        <TR>            
            <TD><LABEL ID=101682><%= GetLocalResourceObject("tcdDateFromCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdDateFrom", CStr(Today),  , GetLocalResourceObject("tcdDateFromToolTip"))%></TD>
            <TD><LABEL ID=101683><%= GetLocalResourceObject("tcdDateToCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdDateTo", CStr(Today),  , GetLocalResourceObject("tcdDateToToolTip"))%></TD>
        </TR>
        <TR></TR>
        <TR></TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="20%"></TD>
            <TD><LABEL ID=101684><%= GetLocalResourceObject("cbeCompReiCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCompRei", "company", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCompReiToolTip"))%></TD>
            <TD WIDTH="20%"></TD>
        </TR>
        <TR>
            <TD></TD>
            <TD><LABEL ID=101685><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD></TD>
            <TD><LABEL ID=101686><%= GetLocalResourceObject("cbeCessTypeCaption") %></LABEL></TD>
            <TD><%
mobjValues.BlankPosition = True
mobjValues.List = "0,3,4"
mobjValues.TypeList = 2
Response.Write(mobjValues.PossiblesValues("cbeCessType", "table534", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , "insCessionType(this)",  ,  , GetLocalResourceObject("cbeCessTypeToolTip")))%></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD></TD>
            <TD><LABEL ID=101687><%= GetLocalResourceObject("cbeBranchReiCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranchRei", "table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranchReiToolTip"))%></TD>
            <TD></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




