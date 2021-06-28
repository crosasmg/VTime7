<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	
	mobjValues.sCodisplPage = "CPL001_K"
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
}

</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("CPL001", "CPL001_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAccountsplan" ACTION="ValLedGerRep.aspx?mode=1">
    <BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeLedCompanCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nCompany", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeLedCompan", "tabcompanyclient", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  , False, 30, "", eFunctions.Values.eTypeCode.eString, 1))
End With
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeLevelsCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeLevels", "table7007", eFunctions.Values.eValuesType.clngComboType, CStr(7),  ,  ,  ,  ,  ,  , False, 30, GetLocalResourceObject("cbeLevelsToolTip"),  , 2)%></TD>        
        </TR>
        <TR>            
            <TD><%=mobjValues.CheckControl("chkDetail", GetLocalResourceObject("chkDetailCaption"), "2", "1",  , False, 3)%></TD>
        </TR>                
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





