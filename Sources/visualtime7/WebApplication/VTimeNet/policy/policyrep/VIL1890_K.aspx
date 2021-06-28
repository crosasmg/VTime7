<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.05
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues


    '% inspreVIL1890: se definen los campos de la forma
    '--------------------------------------------------------------------------------------------
    Private Sub insPreVIL1890()
        '--------------------------------------------------------------------------------------------

        Response.Write("	" & vbCrLf)
        Response.Write("<BR><BR>" & vbCrLf)
        Response.Write("	")

        Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript")))
        Response.Write("" & vbCrLf)
        Response.Write("	<TABLE WIDTH=""100%"" border=0>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Fecha de impresión</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdPrintDate", vbNullString,  , "Fecha de impresión"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        ' Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Año</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.NumericControl("tcnYear", 4,  ,  , "Corresponde al año del período a procesar",  , 0))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        'Response.Write("		    <TD></TD>" & vbCrLf)
        'Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Tipo de declaración</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.ComboControl("cbeDecType", "1|Original,2|Rectificada", CStr(1), False,  ,  , "insEnabledRectif(this)"))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        'Response.Write("		    <TD></TD>" & vbCrLf)
        'Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("            <TD><LABEL hidden ID=0>Rectificatoria</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD hidden>")


        Response.Write(mobjValues.NumericControl("tcnRectif", 5, CStr(0),  , "Número de rectificación",  , 0,  ,  ,  ,  , True))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD CLASS=""HighLighted""><LABEL ID=0><A NAME=""Proceso"">Tipo de Proceso</A></LABEL></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD CLASS=""HORLINE"" width=""40%""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.OptionControl(0, "optProcessType", "Masivo", CStr(1), CStr(1), "insEnabledFields(this)"))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=4>&nbsp;</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=4 CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(0, "optProcessType", "Puntual", CStr(2), CStr(2), "insEnabledFields(this)"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Cliente</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.ClientControl("valClient", "",  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>")


    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("VIL1890_k")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "VIL1890_k"
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    ' ~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion = "$$Revision: 2 $|$$Date: 20-01-15 19:15 $|$$Author: Mgonzalez $"
</SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//%   insEnabledFields: Permite habilitar e inhabilitar los campos de la página.
//------------------------------------------------------------------------------------------
function insEnabledFields(lobject){
//------------------------------------------------------------------------------------------
    
	if (lobject.value!=2) {
		
		with(self.document.forms[0]){
		    valClient.disabled=true;
		    btnvalClient.disabled=true;
		    valClient.value="";
		    valClient_Digit.value="";
		    UpdateDiv("valClient_Name", "")
		}
		
    }
    else {
        
	    self.document.forms[0].valClient.disabled=false;
	    self.document.forms[0].btnvalClient.disabled=false;
    }    
}

//%   insEnabledRectif: Permite habilitar e inhabilitar los campos de la página.
//------------------------------------------------------------------------------------------
function insEnabledRectif(lobject){
//------------------------------------------------------------------------------------------
    
	if (lobject.value!=2) {
		
		with(self.document.forms[0]){
		    tcnRectif.disabled=true;
		    tcnRectif.value="0";
		}
		
    }
    else {
        
	    with(self.document.forms[0]){
		    tcnRectif.disabled=false;
		    tcnRectif.value="";
		}
    }    
}
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'Vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjMenu.MakeMenu(Request.QueryString("sCodispl"), "VIL1890_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
            .Write(mobjMenu.setZone(1, "VIL1890", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
        End With
        'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VIL1890" ACTION="valPolicyRep.aspx?sMode=1">
<%Call insPreVIL1890()
    'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.05
    Call mobjNetFrameWork.FinishPage("VIL1890_k")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>





