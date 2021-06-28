<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQuote As String


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SGC001_K"

mobjMenu = New eFunctions.Menues

mstrQuote = """"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SGC001", "SGC001_k.aspx", 1, ""))
End With

mobjMenu = Nothing%>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false
			       
    for (lintIndex=0; lintIndex < document.images.length; lintIndex++){
        if (document.images[lintIndex].belongtoolbar!=true)
         document.images[lintIndex].disabled = false
    }         
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SGC001" ACTION="valSecurityQue.aspx?Mode=1">
	<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
            <TD><LABEL ID=14979><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip"),  , 1)%></TD>
                        
            <TD><LABEL ID=14977><%= GetLocalResourceObject("cbeDepartmenCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeDepartmen", "Table84", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeDepartmenToolTip"),  , 2)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14978><%= GetLocalResourceObject("valSchemaCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valSchema", "tab_Schema", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  , True, 6, GetLocalResourceObject("valSchemaToolTip"), eFunctions.Values.eTypeCode.eString, 3)%></TD>
		</TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





