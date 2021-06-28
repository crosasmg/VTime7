<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de la información de la página
Dim mobjError As eErrors.ErrorTyp


</script>
<%
Response.Expires = -1441

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mobjError = New eErrors.ErrorTyp
End With

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "erl001_k"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 6 $|$$Date: 15/09/04 11:33a $|$$Author: Calvarez $"

    //% insStateZone: habilita/deshabilita los campos de la ventana
    //------------------------------------------------------------------------------------------
    function insStateZone() {
        //------------------------------------------------------------------------------------------
    }

    //% insFinish: controla la acción de finalizar de la página.
    //------------------------------------------------------------------------------------------
    function insFinish() {
        //------------------------------------------------------------------------------------------
        return true;
    }

    //% insCancel: controla la acción cancelar de la página
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }

    //% ChangeValues: Habilita y deshabilita los campos
    //------------------------------------------------------------------------------------------
    function ChangeValues(Field) {
        //------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (Field.checked)
                chkTransfer.value = "1";
            else
                chkTransfer.value = "2";
        }
    }

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sCodispl") & "_K.aspx", 1, Request.QueryString.Item("sCodispl")))
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmErrorsRep" ACTION="ValErrors.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>    
            <TD><LABEL ID=6803>Error</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnErrorNum", 5, "",  ,"Número de error a consultar",  , 0)%></TD>
            <TD></TD>
            <TD><LABEL ID=6804>Transacción</LABEL></TD>
            <TD>
            <%
If mobjValues.ActionQuery = True Then
	Response.Write(mobjValues.TextControl("tctCodisp", 10, mobjError.sCodisp,  ,"Código de la transacción a consultar"))
Else
	Response.Write(mobjValues.PossiblesValues("tctCodisp", "Windows", 2, Session("sCodispl_log"),  ,  ,  ,  ,  ,  ,  , 10,"Código de la transacción", eFunctions.Values.eTypeCode.eString, 1,  , True))
End If
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=6806>Estado</LABEL></TD>
            <%mobjValues.typelist = 2
              mobjValues.list = "5,6,11"%>
            <TD><%=mobjValues.PossiblesValues("cbeStaterr", "Table999", 1, CStr(mobjError.sStat_error_Initial),  ,  ,  ,  ,  ,  ,  ,  ,"Estado del error a consultar")%></TD>
            <TD></TD>
            <TD><LABEL ID=6807>Prioridad</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbePriority", "Table1006", 1, CStr(mobjError.nPriority),  ,  ,  ,  ,  ,  ,  ,  ,"Prioridad del error a consultar")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6808>Origen</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSource", "Table531", 1, CStr(mobjError.nSource_Initial),  ,  ,  ,  ,  ,  ,  ,  ,"Procedencia del error")%></TD>
            <TD></TD>
            <TD><LABEL ID=6809>Tipo</LABEL></TD>
            <TD><%mobjValues.TypeOrder = 1
                Response.Write(mobjValues.PossiblesValues("cbeErrorType", "tab_typerr", 1, CStr(mobjError.nType_err),  ,  ,  ,  ,  ,  ,  ,  ,"Tipo de error a consultar"))%></TD>
        </TR>
		<TR>    
			<TD>		
				<%=mobjValues.CheckControl("chkTransfer","Traspasado", "2", "2", "ChangeValues(this)",  ,  ,"Indicador de generación de archivo")%>
			</TD>
        </TR>                   
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=6811><A NAME="Registro">Registro</A></LABEL></TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=6812><A NAME="Pendiente">Pendiente</A></LABEL></TD>
        </TR>
        <TR>
            <TD VALIGN="TOP" COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
            <TD VALIGN="TOP" COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6813>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserRegister", 10, mobjError.sUser,  ,"Usuario que registró el error")%></TD>
            <TD></TD>
            <TD><LABEL ID=6814>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserPending", 10, mobjError.sUser,  ,"Usuario que registró el error como pendiente")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6815>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateRegister", "",  ,"Fecha de registro inicial")%></TD>
            <TD></TD>
            <TD><LABEL ID=6816>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddatePending", "",  ,"Fecha de pendiente inicio")%></TD>
        </TR>
        
        <!--<TR>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateRegisterF", "",  ,"Fecha final de registro")%></TD>
            <TD></TD>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddatePendingF", "",  ,"Fecha final de pendiente")%></TD>
        </TR>-->
        
         <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=6817><A NAME="Aclarado">Aclarado</A></LABEL></TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=6818><A NAME="Nuevo">Nuevo</A></LABEL></TD>
        </TR>
        <TR>
            <TD VALIGN="TOP" COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
            <TD VALIGN="TOP" COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6819>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserClear", 10, mobjError.sUser,  ,"Usuario que registró el error como aclarado")%></TD>
            <TD></TD>
            <TD><LABEL ID=6820>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserNew", 10, mobjError.sUser,  ,"Usuario que registró el error como nuevo")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6821>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateClear", "",  ,"Fecha inicial de aclarado")%></TD>
            <TD></TD>
            <TD><LABEL ID=6822>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateNew", "",  ,"Fecha inicial de nuevo")%></TD>
        </TR>
        
        <!--<TR>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateClearF", "",  ,"Fecha final de registro")%></TD>
            <TD></TD>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateNewF", "",  ,"Fecha final de pendiente")%></TD>
        </TR>-->

         <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=6823><A NAME="Defecto">Defecto</A></LABEL></TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=6824><A NAME="Tiempo">Asignación</A></LABEL></TD>
        </TR>
        <TR>
            <TD VALIGN="TOP" COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
            <TD VALIGN="TOP" COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6825>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserDetect", 10, mobjError.sUser,  ,"Usuario que registró el error como defecto")%></TD>
            <TD></TD>
            <TD><LABEL ID=6826>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserAssig", 10, mobjError.sUser,  ,"Usuario que registró el error como asignado")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6827>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateDetect", "",  ,"Fecha inicial de defecto")%></TD>
            <TD></TD>
            <TD><LABEL ID=6828>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateAssig", "",  ,"Fecha inicial de asignación")%></TD>
        </TR>
        
        <!--<TR>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateDetectF", "",  ,"Fecha final de defecto")%></TD>
            <TD></TD>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateAssigF", "",  ,"Fecha final de asignación")%></TD>
        </TR>    -->    
        
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=6829><A NAME="Estado">Corrección</A></LABEL></TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=6830><A NAME="Tiempo">Conformación</A></LABEL></TD>
        </TR>
        <TR>
            <TD VALIGN="TOP" COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
            <TD VALIGN="TOP" COLSPAN="5" CLASS="HorLine"></TD>
        </TR>

        <TR>
            <TD><LABEL ID=6831>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserCorrec", 10, mobjError.sUser,  ,"Usuario que registró el error como corregido")%></TD>
            <TD></TD>
            <TD><LABEL ID=6832>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserConfir", 10, mobjError.sUser,  ,"Usuario que registró el error como conformado")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6833>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateCorrec", "",  ,"Fecha inicio de corrección")%></TD>
            <TD></TD>
            <TD><LABEL ID=6834>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateConfir", "",  ,"Fecha inicio de conformación")%></TD>
        </TR>
        <!--<TR>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateCorrecF", "",  ,"Fecha final de corrección")%></TD>
            <TD></TD>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateConfirF", "",  ,"Fecha final de conformación")%></TD>
        </TR>--> 
            
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=6835><A NAME="Aceptado">Aceptado</A></LABEL></TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=6836><A NAME="NoAceptado">No aceptado</A></LABEL></TD>
        </TR>
        <TR>
            <TD VALIGN="TOP" COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
            <TD VALIGN="TOP" COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6837>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserAcept", 10, mobjError.sUser,  ,"Usuario que registró el error como aceptado")%></TD>
            <TD></TD>
            <TD><LABEL ID=6838>Usuario</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserNoAcept", 10, mobjError.sUser,  ,"Usuario que registró el error como no aceptado")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=6839>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateAcept", "",  ,"Fecha inicio de aceptación")%></TD>
            <TD></TD>
            <TD><LABEL ID=6840>Fecha inicio</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcddateNoAcept", "",  ,"Fecha inicio de no aceptación")%></TD>
        </TR>
        <!--<TR>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateAceptF", "",  ,"Fecha final de aceptación")%></TD>
            <TD></TD>
            <TD><LABEL ID=15282>Fecha final</LABEL></TD>
            <TD><%'=mobjValues.DateControl("tcddateNoAceptF", "",  ,"Fecha final de no aceptación")%></TD>
        </TR>-->
    </TABLE>
</FORM>
</BODY>
</HTML>











