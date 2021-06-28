<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" ValidateRequest="false" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mobjError As eErrors.ErrorTyp


</script>
<%Response.Expires = -1
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjError = New eErrors.ErrorTyp

Call mobjError.inspreER001(Session("nErrorNum"), Session("nusercode"))

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 And mobjError.nModule_Err = 0 Then
	mobjError.nModule_Err = 9999
End If

'If Request.QueryString("nMainAction") = 301 And 	 '  (mobjError.sCrit_Err = "0" Or 	 '   mobjError.sCrit_Err = "") Then
''mobjError.sCrit_Err = "99"
'End If

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 And mobjError.nModule_Err = 0 Then
	mobjError.nModule_Err = 9999
End If

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 And (CStr(mobjError.sStat_error) = "0" Or CStr(mobjError.sStat_error) = "") Then
	mobjError.sStat_error = CShort("1")
End If

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "er001"

%>
<%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">

	<%=mobjValues.StyleSheet()%>
    <%=mobjMenu.setZone(2, "ER001", "ER001.aspx")%>
<SCRIPT>
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
            //hddSeverity.value = cbeSeverity.value;
            //if (cbeSeverity.value == "4")
            //	hddSeverity.value = "0";
            //if (cbeSeverity.value == "0" ||
            //    cbeSeverity.value == "")
            //	hddSeverity.value = "99";
        }
    }
    //---------------------------------------------------------------------------------------------
    //%Función que se encarga de cambiar el estado del error de acuerdo a la procedencia
    function ChangeSource(Source) {
        //---------------------------------------------------------------------------------------------
        with (document.forms[0]) {
            //if (nMainAction == 301){
            cbeStaterr.disabled = false;

            switch (Source) {
                case "1":
                    cbeStaterr.value = 1
                    cbeStaterr.disabled = false;
                    //cbeSeverity.value = "99";
                    //hddSeverity.value = "0";
                    cbeSeverity.disabled = false;
                    break;
                case "2":
                    cbeStaterr.value = 1
                    cbeStaterr.disabled = true
                    //cbeSeverity.value = "4";
                    //hddSeverity.value = "0";
                    cbeSeverity.disabled = true;
                    break;
                case "3":
                    cbeStaterr.value = 1;
                    cbeStaterr.disabled = true
                    //cbeSeverity.value = "4";
                    //hddSeverity.value = "0";
                    //cbeSeverity.disabled = true;
                    break;
                case "4":
                    cbeStaterr.value = 7;
                    cbeStaterr.disabled = true
                    //cbeSeverity.value = "4";
                    //hddSeverity.value = "0";
                    //cbeSeverity.disabled = true;
                    break;
                case "7":
                    cbeStaterr.value = 1;
                    cbeStaterr.disabled = true
                    //cbeSeverity.value = "4";
                    //hddSeverity.value = "0";
                    //cbeSeverity.disabled = true;
                    break;
                case "9":
                    cbeStaterr.value = 1
                    cbeStaterr.disabled = false;
                    //cbeSeverity.value = "99";
                    //hddSeverity.value = "0";
                    cbeSeverity.disabled = false;
                    break;
                case "10":
                    cbeStaterr.value = 1
                    cbeStaterr.disabled = false;
                    //cbeSeverity.value = "99";
                    //hddSeverity.value = "0";
                    cbeSeverity.disabled = false;
                    break;
                default:
                    cbeStaterr.value = 1;
                    cbeStaterr.disabled = false;
                    //cbeSeverity.value = "4";
                    //hddSeverity.value = "0";
                    //cbeSeverity.disabled = true;
                    break;

            }
        }
        //	}
    }
    //---------------------------------------------------------------------------------------------
    function insShowHeader() {
        //---------------------------------------------------------------------------------------------
        var lblnContinue = true
        if (typeof (top.fraHeader.document) != 'undefined') {
            if (typeof (top.fraHeader.document.forms[0]) != 'undefined') {
                if (typeof (top.fraHeader.document.forms[0].tcnErrorNum) != 'undefined') {
                    top.fraHeader.document.forms[0].tcnErrorNum.value = '<%=Session("nErrorNum")%>'
                    lblnContinue = false
                }
            }
        }
        if (lblnContinue)
            setTimeout("insShowHeader()", 50);
    }
    setTimeout("insShowHeader()", 50)

    //---------------------------------------------------------------------------------------------
    function ReloadPage() {
        //---------------------------------------------------------------------------------------------
        self.document.location.href = self.document.location.href + '&Reload=1';
    }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmErroUpd" ACTION="valerrors.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%=mobjValues.ShowWindowsName("ER001")%>
	<TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=6726>Transacción</LABEL></TD>
            <TD COLSPAN="3">

			<%
'+ Si la acción es 'Consultar' o 'Modificar' se toma el código de la venta proveniente de la búsqueda
'+ sino, si la acción es 'Agregar' se coloca el código de la ventana que llamó al Módulo de Errores
If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionAdd) Then
	Response.Write(mobjValues.PossiblesValues("tctCodisp", "Windows", 2, mobjError.sCodisp,  ,  ,  ,  ,  ,  ,  , 10,"Código de la transacción", eFunctions.Values.eTypeCode.eString))
Else
	Response.Write(mobjValues.PossiblesValues("tctCodisp", "Windows", 2, Session("sCodispl_log"),  ,  ,  ,  ,  ,  ,  , 10,"Código de la transacción", eFunctions.Values.eTypeCode.eString))
End If
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=6728>Versión</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctVersion", 10, mobjError.sVersion,  ,"Versión que se le asignará el error")%></TD>
			<%
If mobjValues.ActionQuery Then
	Response.Write("<TD><LABEL ID=6729>Número de error</LABEL></TD><TD>" & mobjValues.NumericControl("tcnErrorNum", 9, Session("nErrorNum")) & "</TD>")
End If
%>
           
        </TR>
        <TR>
            
            <TD><LABEL ID=15279>Severidad</LABEL></TD>
                       	
			<TD><%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
	Response.Write(mobjValues.PossiblesValues("cbeSeverity", "table6014", 1, CStr(mobjError.nSeverity),  ,  ,  ,  ,  ,  ,  ,  ,"Indica el grado de importancia en que debe ser corregido los errores de prioridad 1, 2, y 3."))
Else
	Response.Write(mobjValues.PossiblesValues("cbeSeverity", "table6014", 1, CStr(mobjError.nSeverity),  ,  ,  ,  ,  ,  , True,  ,"Indica el grado de importancia en que debe ser corregido los errores de prioridad 1, 2, y 3."))
End If
%></TD>
			
            <TD><LABEL ID=6732>Tipo de error</LABEL></TD>
            <TD><%mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeErrorType", "tab_typerr", 1, CStr(mobjError.nType_err),  , False,  ,  ,  ,  ,  ,  ,"Tipo de error a asignar"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6733>Fecha de detección</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateDetect", CStr(mobjError.dDat_assign),  ,"Fecha en que se detecta el error", True)%></TD>
             <%=mobjValues.HiddenControl("tdatedetect", CStr(mobjError.dDat_assign))%>
            <TD><LABEL ID=6734>Hora de detección</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctHourDetect", 10, mobjError.sHour,  ,"Hora en la cual se detecta", True)%></TD>
            <%=mobjValues.HiddenControl("tHourDetect", mobjError.sHour)%>
        </TR>
        <TR>
            <TD><LABEL ID=6735>Detectado por</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserDetect", 10, mobjError.sUser,  ,"Persona que detecta el error", True)%></TD>
            <%=mobjValues.HiddenControl("tUserDetect", mobjError.sUser)%>
            <TD ><%=mobjValues.TextControl("tctUserName", 30, mobjError.sUserName,  ,"Nombre del Usuario que detecta el error", True,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6737>Procedencia</LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeSource", "Table531", 1, CStr(mobjError.nSource),  ,  ,  ,  ,  , "ChangeSource(this.value);",  ,  ,"Procedencia del error"))%></TD>
               
            <TD><LABEL ID=6738>Estado del error</LABEL></TD>
            <TD><%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
	mobjValues.TypeList = 1
	mobjValues.List = "1,7"
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeStaterr", "Table999", 1, CStr(mobjError.sStat_error),  ,  ,  ,  ,  ,  , False,  ,"Estado en que se encuentra el error"))
Else
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeStaterr", "Table999", 1, CStr(mobjError.sStat_error),  ,  ,  ,  ,  ,  , True,  ,"Estado en que se encuentra el error"))
End If
%>	  
            </TD>
        </TR>
        <TR>
               
            	
			<TD><LABEL ID=6731>Prioridad</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbePriority", "Table1006", 1, CStr(mobjError.nPriority),  ,  ,  ,  ,  ,  ,  ,  ,"Prioridad del error")%></TD>
            
            <TD><LABEL ID=6739>Módulo afectado</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeModuleError", "Table997", 1, CStr(mobjError.nModule_Err),  ,  ,  ,  ,  , "ChangeModule(this.value);",  ,  ,"Módulo en el que se produce el error")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=6740><A NAME="Descripción">Descripción</A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH=100%>
        <TR>
			<TD WIDTH=100pcx><LABEL ID=6741>Breve</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctShortDesc", 60, mobjError.sDescript,  ,"Descripción breve del error")%></TD>
        </TR>
        <TR>
			<TD VALIGN="TOP"><LABEL ID=6742>Detallada</LABEL></TD>
			<TD><%If mobjValues.ActionQuery Then
	Response.Write(mobjValues.TextAreaControl("txtDescript", 10, 55, Replace(mobjError.tDs_text, vbCrLf, "<BR>"),,"Descripción completa del error"))
Else
	Response.Write(mobjValues.TextAreaControl("txtDescript", 10, 55, mobjError.tDs_text,  ,"Descripción completa del error"))
End If%>
			</TD>
		</TR>
        <%Response.Write(mobjValues.HiddenControl("hddSeverity", CStr(mobjError.nSeverity)))%>
        <%Response.Write(mobjValues.HiddenControl("hddModuleError", CStr(mobjError.nModule_Err)))%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
mobjError = Nothing

'+Codigo temporal que recarga la página para resolver el problema del cambio de numero de errores
'+por error 65197
If Request.QueryString.Item("Reload") = vbNullString Then
	Response.Write("<SCRIPT>ReloadPage();</SCRIPT>")
End If
%>










