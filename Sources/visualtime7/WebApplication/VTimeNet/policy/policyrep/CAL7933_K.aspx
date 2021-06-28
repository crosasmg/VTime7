<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- The object to handling the general function to load values is defined
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'**- The object to handling the generic routines is defined
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%
    Response.Expires = -1440

mobjValues = New eFunctions.Values

mobjMenu = New eFunctions.Menues

%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>

//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe

    document.VssVersion="$$Revision: 1 $|$$Date: 17/03/16 9:53p $"

//**% insStateZone: This function enable/disable the fields of the page according to the action 
//**% to be performed
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
}

//**% insCancel: This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%insChangeField : Cambia valores de campos dependientes
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
    var frm = self.document.forms[0]

    switch(objField.name){

	case 'tctFile':
		frm.hdtFileName.value = objField.value;
		break;
    }            
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    
 <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CAL7933", "CAL7933_k.aspx", 1, ""))
End With

'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="CAL7933" ACTION="valpolicyrep.aspx?sMode=1" ENCTYPE="multipart/form-data">
	<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl")))%>
<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0>Tipo de Rentabilidad</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeType", "table8300", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , , "Tipo de Rentabilidad")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Archivo</LABEL></TD>
            <TD><%=mobjValues.FileControl("tctFile", 40,  , False,  , "insChangeField(this)")%></TD>
        </TR>

    </TABLE>
<%
Response.Write(mobjValues.HiddenControl("hdtFileName", ""))
%>    

</FORM>
</BODY>
<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%> 
</HTML>





