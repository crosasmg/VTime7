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
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"

//% insCancel: se controla la acción Cancelar de la página
//%------------------------------------------------------------------------------------------
function insCancel(){
//%------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL585"))
	.Write(mobjMenu.MakeMenu("COL585", "COL585_K.aspx", 1, vbNullString))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COL585_K" ACTION="valCollectionRep.aspx?sMode=2" ENCTYPE="multipart/form-data">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL585"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD>&nbsp;</TD>
        </TR>
	    <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdProcDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdProcDate", CStr(Today),  , GetLocalResourceObject("tcdProcDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valBankCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valBank", "Table7", 2,  ,  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("valBankToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%=mobjValues.FileControl("tctFile", 40,  , True, , "insSelectFile(this);")%></TD>
        </TR>
    </TABLE>
    <%=mobjValues.HiddenControl("hdsFileName", "") %>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>

<SCRIPT>
//% insStateZone: se controla el estado de los campos de la página
//%----------------------------------------------------------------------------------------
function insStateZone(){
//%----------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		tcdProcDate.disabled=false;
		btn_tcdProcDate.disabled=false;
		valBank.disabled=false;
		btnvalBank.disabled=false;
		tctFile.disabled = false;				
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
            hdsFileName.value = filename;
        }
    }
}
</SCRIPT>




