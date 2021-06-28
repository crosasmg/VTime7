<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjError As eErrors.ErrorTyp


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjError = New eErrors.ErrorTyp

mobjValues.sCodisplPage = "er007_k"
%>
<%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

    <%=mobjValues.StyleSheet()%>
    <%=mobjMenu.MakeMenu("ER007", "ER007_K.aspx", 1, "")%>
<SCRIPT>
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }
    //------------------------------------------------------------------------------------------
    function insFinish() {
        //------------------------------------------------------------------------------------------
        return true;
    }
    //------------------------------------------------------------------------------------------
    function insStateZone() {
        //------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            tctCodisp.disabled = false;
            btntctCodisp.disabled = false;
            cbeStaterr.disabled = false;
            cbeSource.disabled = false;
            cbePriority.disabled = false;
            cbeSeverity.disabled = false;
            cbeModuleError.disabled = false;
        }
    }
    //---------------------------------------------------------------------------------------------
    //%Función que se encarga de cambiar el estado numero del modulo
    function ChangeModule(Source) {
        //---------------------------------------------------------------------------------------------
        with (document.forms[0])
            if (Source == "0")
                hddModuleError.value = 9998;
            else
                hddModuleError.value = cbeModuleError.value;
    }
    //---------------------------------------------------------------------------------------------
    //%Función que se encarga de cambiar el estado del error de acuerdo a la procedencia
    function ChangeCritic(Source) {
        //---------------------------------------------------------------------------------------------
        with (document.forms[0]) {
            hddErrorCrit.value = cbeSeverity.value;
            if (cbeSeverity.value == "4")
                hddErrorCrit.value = "0";
            if (cbeSeverity.value == "0" ||
		    cbeSeverity.value == "")
                hddErrorCrit.value = "99";
        }
    }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmErroUpd" ACTION="valerrors.aspx?sTime=1">
<BR><BR>
	<TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=6797>Ventana</LABEL></TD>
            <TD COLSPAN="3">
			<%Response.Write(mobjValues.PossiblesValues("tctCodisp", "Windows", 2,  ,  ,  ,  ,  ,  ,  , True, 10,"Código de la transacción", eFunctions.Values.eTypeCode.eString))%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=6798>Estado actual</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStaterr", "table999", 1,  ,  ,  ,  ,  ,  ,  , True,  ,"Estado en que se encuentra el error")%>
            <TD><LABEL ID=6799>Procedencia</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSource", "Table531", 1,  ,  ,  ,  ,  ,  ,  , True,  ,"Procedencia del error")%></TD>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=6801>Severidad</LABEL></TD>
            <TD><%Response.Write(mobjValues.PossiblesValues("cbeSeverity", "table6014", 1, CStr(mobjError.nSeverity),  ,  ,  ,  ,  ,  ,  ,  ,"Indica el grado de importancia en que debe ser corregido los errores de prioridad 1, 2, y 3."))%>
            	<%Response.Write("<SCRIPT>self.document.forms[0].cbeSeverity.disabled=true;</script>")%>
            </TD>
            <TD><LABEL ID=6800>Prioridad</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbePriority", "Table1006", 1,  ,  ,  ,  ,  ,  ,  , True,  ,"Prioridad del Error")%></TD>
            
        </TR>
        <TR>
            <TD><LABEL ID=6802>Módulo afectado</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeModuleError", "Table997", 1, CStr(9999),  ,  ,  ,  ,  , "ChangeModule(this.value);", True,  ,"Módulo en el que se produce el error")%></TD>
        </TR>
        <%=mobjValues.HiddenControl("hddErrorCrit", "99")%>
        <%=mobjValues.HiddenControl("hddModuleError", CStr(9999))%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
mobjError = Nothing
%>










