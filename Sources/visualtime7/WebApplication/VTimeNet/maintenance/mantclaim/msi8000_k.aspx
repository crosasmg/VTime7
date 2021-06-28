<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401
mobjValues.sCodisplPage = "MSI8000"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("MSI8000", "MSI8000_K.aspx", 1, vbNullString))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
<SCRIPT LANGUAGE="JavaScript">

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 26-12-11 15:27 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcdEffecdate.disabled = false;
		btn_tcdEffecdate.disabled = false;
		cbeBranch.disabled = false;
	}
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insChangeField: se controla la acción Modificar parametros de la pantalla  
//--------------------------------------------------------------------------------------------
function insChangeField(objField) {
    //--------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        switch (objField.name) {
            //Revisa Ramo   
            case 'cbeBranch':
                valProduct.Parameters.Param1.sValue = cbeBranch.value;
                if (cbeBranch.value != 0 && cbeBranch.value != '') {
                    valProduct.disabled = false;
                    btnvalProduct.disabled = false;
                }
                else {
                    valProduct.value = "";
                    UpdateDiv("valProductDesc", "")
                    valProduct.disabled = true;
                    btnvalProduct.disabled = true;
                    tcnPolicy.value = '';
                    tcnPolicy.disabled = true;
                    tcnRec_Beg.value = "";
                    tcnRec_End.value = "";
                    tcnRec_Beg.disabled = true;
                    tcnRec_End.disabled = true;
                    tcnCon_Beg.value = "";
                    tcnCon_End.value = "";
                    tcnCon_Beg.disabled = true;
                    tcnCon_End.disabled = true;
                    tcdStarDate.value = "";
                    tcdEndDate.value = "";
                }
                break;
           
        }
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" NAME="MSI8000" ACTION="valmantclaim.aspx?sMode=2">
	<BR>
	<BR>
	    <TABLE WIDTH="100%">
	        <TR>
	            <TD><LABEL ID="0">Ramo</LABEL></TD>
	            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, , , , , , , "insChangeField(this.value)", True, , "Ramo")%></TD>
	        </TR>
			<TR>
				<TD><LABEL ID="0">Fecha</LABEL></TD>
				<TD><%=mobjValues.DateControl("tcdEffecdate", "",  , "Fecha de efecto",  ,  ,  ,  , True)%></TD>
			</TR>
	    </TABLE>
	</FORM> 
</BODY>
</HTML>




