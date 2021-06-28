<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
Response.Buffer = True
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"/>


    <SCRIPT>
//+ Variable para el control de versiones
	    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
    </SCRIPT>
<SCRIPT>
// insStateZone :
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
}

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL511"))
	.Write(mobjMenu.MakeMenu("COL511", "COL511_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDirPayBank" ACTION="valCollectionRep.aspx?mode=1" ENCTYPE="multipart/form-data">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL511"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valBankCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valBank", "Table7", 2,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valBankToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%=mobjValues.FileControl("tctName", 40)%></TD>
        </TR>

    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




