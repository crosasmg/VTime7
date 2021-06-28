<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
    Dim sCodispl As String
    Dim sCodisplPage As String
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
</script>
<% Response.Expires = -1441
    
	sCodispl = Trim(Request.QueryString("sCodispl"))
	sCodisplPage = LCase(sCodispl) & "_k"

    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
	    
    mobjNetFrameWork = New eNetFrameWork.Layout
	    mobjNetFrameWork.sSessionID = Session.SessionID
	    mobjNetFrameWork.nUsercode = Session("nUsercode")
	    Call mobjNetFrameWork.BeginPage(sCodisplPage)
    '~End Header Block VisualTimer Utility

	'- Objeto para el manejo de las funciones generales de carga de valores
    

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
	    mobjValues.sSessionID = Session.SessionID
	    mobjValues.nUsercode = Session("nUsercode")
		mobjValues.sCodisplPage = sCodisplPage
    '~End Body Block VisualTimer Utility

    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
	    mobjMenu.sSessionID = Session.SessionID
	    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

%>
    
	<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
	
<HTML>
<HEAD>
<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

    //% insStateZone: se manejan los campos de la página
    //------------------------------------------------------------------------------------------
    function insStateZone()
    //------------------------------------------------------------------------------------------
    {
    }

    //% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
    //------------------------------------------------------------------------------------------
    function insCancel()
    //------------------------------------------------------------------------------------------
    {
        return true;
    }

    //%insDefValue:Permite asignarle "0,00" al control en caso de no haber indicado
    //%valor numerico al campo
    //------------------------------------------------------------------------------------------
    function insDefValue(Field) {
        //------------------------------------------------------------------------------------------
        if (Field.value == '')
            self.document.forms[0].tcnExcess.value = '0'
    }

</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%
	    Response.Write(mobjValues.StyleSheet())
	    Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.asp", 1, Request.QueryString("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	    mobjMenu = Nothing
	%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="frmClaim" action="valClaimRep.aspx?mode=1">
    <BR></BR>
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td align="left"><H2 class="WindowsName">Libro de Siniestros Pagados<HR></H2></td>
		</tr>
	</table>
    <BR></BR>
	<table width="60%">
		<tr>
			<td class="HighLighted" align="left" colspan="2">Período a consultar:</td>
		</tr>
		<tr>
			<td colspan="2" class="HorLine" width="100%" align="left"></td>
		</tr>
		<tr>
            <td><LABEL ID=17183>Fecha Desde: </LABEL></td>
            <td><%=mobjvalues.DateControl("tcdIniDate",,,"Fecha desde la cual se desea listar los siniestros")%></td>
        </tr>
        <tr>
            <td><LABEL ID=17184>Fecha Hasta:</LABEL></td>
            <td><%=mobjvalues.DateControl("tcdEndDate",,,"Fecha hasta la cual se desea listar los siniestros")%></td>        
		</tr>
        <tr>
			<td><img height="20" src="/VTimeNet/images/blank.gif"/></td>
		</tr>
		<tr>
			<td COLSPAN="6">&nbsp</td>	        
		</tr>
	</table>
</FORM>
</BODY>
</HTML>
<%  mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
	Call mobjNetFrameWork.FinishPage(sCodisplPage)
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>

