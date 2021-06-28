<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'    response.buffer=true
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

  
<SCRIPT LANGUAGE=JavaScript>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"

    //% insCancel: se controla la acción Cancelar de la página
    //%------------------------------------------------------------------------------------------
    function insCancel() {
        //%------------------------------------------------------------------------------------------
        return true;
    }
</SCRIPT>
    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("CAL665"))
	.Write(mobjMenu.MakeMenu("CAL665", "CAL665_K.aspx", 1, vbNullString))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" NAME="CAL665_K" ACTION="valPolicyRep.aspx?sMode=2" ENCTYPE="multipart/form-data">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("CAL665"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD>&nbsp;</TD>
        </TR>
	    <TR>
            <TD><LABEL ID=LABEL2><%= GetLocalResourceObject("AnchorCaptionProp") %></LABEL></TD>
            <TD><%=mobjValues.FileControl("tctFileProp", 40,  , False, , "insSelectFile(this);")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("AnchorCaptionRoles") %></LABEL></TD>
            <TD><%=mobjValues.FileControl("tctFileRoles", 40,  , False, , "insSelectFile(this);")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaptionBenef") %></LABEL></TD>
            <TD><%=mobjValues.FileControl("tctFileBenef", 40,  , False, , "insSelectFile(this);")%></TD>
        </TR>
    </TABLE>
    <%=mobjValues.HiddenControl("hdsFileNameProp", "") %>
    <%=mobjValues.HiddenControl("hdsFileNameRoles", "") %>
    <%=mobjValues.HiddenControl("hdsFileNameBenef", "") %>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>

<SCRIPT>
    //% insStateZone: se controla el estado de los campos de la página
    //%----------------------------------------------------------------------------------------
    function insStateZone() {
        //%----------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            tctFileProp.disabled = false;
            tctFileRoles.disabled = false;
            tctFileBenef.disabled = false;
        }
    }

    //%insChangeDef: 
    //--------------------------------------------------------------------------------------------------
    function insSelectFile(Field) {
        //--------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            var fullPath = Field.value;
            var filename;

            if (fullPath) {
                var startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/'));
                var filename = fullPath.substring(startIndex);
                if (filename.indexOf('\\') === 0 || filename.indexOf('/') === 0) {
                    filename = filename.substring(1);
                }
                switch (Field.name) {
                    case 'tctFileProp':
                        hdsFileNameProp.value = filename;
                        break;
                    case 'tctFileRoles':
                        hdsFileNameRoles.value = filename;
                        break;
                    case 'tctFileBenef':
                        hdsFileNameBenef.value = filename;
                        break;
                }
            }
        }
    }
</SCRIPT>