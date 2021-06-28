<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MTR001_K")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MTR001_K"
'~End Body Block VisualTimer Utility

%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]) {
        cbeBranch.disabled=false;
        valProduct.disabled=false;
        btnvalProduct.disabled=false;
        cbeCurrency.disabled=false;
        tcdEffecDate.disabled=false;
        btn_tcdEffecDate.disabled=false;
    }

}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MTR001", "MTR001_k.aspx", 1, vbNullString))
    mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MTR001" ACTION="valmanttransport.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=15991><%= GetLocalResourceObject("cbeBranchCaption")%></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("cbeBranch", "TABLE10", eFunctions.Values.eValuesType.clngComboType, Session("nBranch"), , , , , , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value", True, 2, GetLocalResourceObject("cbeBranchToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=15992><%= GetLocalResourceObject("valProductCaption")%></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
                <%= mobjValues.PossiblesValues("valProduct", "tabProdMaster1", eFunctions.Values.eValuesType.clngWindowType, Session("nProduct"), True, , , , , , True, 5, GetLocalResourceObject("valProductToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=15993><%= GetLocalResourceObject("cbeCurrencyCaption")%></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("cbeCurrency", "TABLE11", eFunctions.Values.eValuesType.clngComboType, Session("nCurrency"), , , , , , , True, 2, GetLocalResourceObject("cbeCurrencyToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=15994><%= GetLocalResourceObject("tcdEffecDateCaption")%></LABEL></TD>
            <TD><%= mobjValues.DateControl("tcdEffecDate", Session("dEffecDate"), True, GetLocalResourceObject("tcdEffecDateToolTip"), , , , , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    mobjValues = Nothing
Call mobjNetFrameWork.FinishPage("MTR001_K")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




