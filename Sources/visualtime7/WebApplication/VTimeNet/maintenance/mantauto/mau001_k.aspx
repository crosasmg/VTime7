<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAU001"
%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
    self.document.forms[0].valVehcode.disabled = false;
    self.document.forms[0].btnvalVehcode.disabled = false;
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
        .Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAU001_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmAutoTable" ACTION="ValMantAuto.aspx?mode=1">
<BR></BR>
<TABLE WIDTH="100%">
    </TR>
        <TD WIDTH=120pcx><LABEL ID=0><%= GetLocalResourceObject("tctVehcodeCaption") %></LABEL></TD>
        <TD>
        
                <%--<%= mobjValues.TextControl("tctVehcode", 6, vbNullString, , GetLocalResourceObject("tctVehcodeToolTip"), , , , , True)%>--%>
                <% mobjValues.Parameters.Add("SSTATREGT", 1, eFunctions.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, 0)%>
                <%= mobjValues.PossiblesValues("valVehcode", "TABTAB_AU_VEH", Values.eValuesType.clngWindowType, , True, , , , , , True, , GetLocalResourceObject("tctVehcodeToolTip"), , , , True)%>

                
               
        </TD>
    </TR>
</TABLE>
</FORM>
<%
mobjValues = Nothing%>
</BODY>
</HTML>




