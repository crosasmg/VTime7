<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo de la Fecha
    Dim mobjDate As eGeneral.GeneralFunction

    '- Objeto para el manejo de periodos
    Dim lclsCtrol_date As eGeneral.Ctrol_date
    
    '- Constante para determinar codigo de acción para obtener ultimo periodo de CTROL_DATE
    Const clngGenBookCollection As Short = 202
    
    'Variables para determinación de fechas
    Dim mdEffecdate As String
    Dim FirstDay As Date
    Dim LastDay As Date


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SIL704_k")

mobjDate = New eGeneral.GeneralFunction
lclsCtrol_date = New eGeneral.Ctrol_date
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "sil704_k"
mobjMenu = New eFunctions.Menues
%>

<%  
    'Se obtiene la variable de fecha de CTROL_DATE para asignar valores a la pagina
    If lclsCtrol_date.Find(clngGenBookCollection) Then
        
        'Se determina la fecha de inicio del periodo
        FirstDay = lclsCtrol_date.dEffecdate.AddDays(1)
    
        'Se determina la fecha de fin del periodo
        LastDay = lclsCtrol_date.dEffecdate.AddMonths(1)
        LastDay = LastDay.AddDays(1)
        
    End If
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Collection.aspx" -->
<SCRIPT LANGUAGE=JavaScript>

    //% insStateZone: se controla el estado de los campos de la página 
    //-------------------------------------------------------------------------------------------- 
    function insStateZone() {
        //-------------------------------------------------------------------------------------------- 
    }

    //% insCancel: se controla la acción Cancelar de la página
    //--------------------------------------------------------------------------------------------
    function insCancel() {
        //--------------------------------------------------------------------------------------------
        return true;
    }

    //% insFinish: se controla la acción Cancelar de la página
    //--------------------------------------------------------------------------------------------
    function insFinish() {
        //--------------------------------------------------------------------------------------------
        return true;
    }
</SCRIPT>
	<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	        .Write(mobjMenu.MakeMenu("sil704", "sil704_K.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
	        .Write(mobjValues.WindowsTitle("sil704", Request.QueryString("sWindowDescript")))
	    End With
	    mobjMenu = Nothing
	    Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%> 


</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="SIL704" ACTION="valClaimRep.aspx?sMode=1">
	<BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
	<BR></BR>
	<table width="100%" border="0">
		<tr>
            <td><LABEL ID="8704">Fecha Desde: </LABEL></td>
            <td><%=mobjValues.DateControl("tcdDateIni", FirstDay, , "Fecha desde la cual se desea listar los siniestros", , , , , False)%></td>

		    <TD>&nbsp</TD>
		    <TD>&nbsp</TD>
            <TD><LABEL ID="LABEL2">Tipo Ejecucion</LABEL></TD>
            <TD><%=mobjValues.OptionControl(0, "optOption", GetLocalResourceObject("optOption_1Caption"), "1", "1")%></TD>
        
        </tr>
        <tr>
            <td><LABEL ID="8703">Fecha Hasta:</LABEL></td>
            <td><%=mobjValues.DateControl("tcdDateEnd", LastDay, , "Fecha hasta la cual se desea listar los siniestros", , , , , False)%></td>        
		    <TD>&nbsp</TD>
		    <TD>&nbsp</TD>
		    <TD>&nbsp</TD>
            <TD><%=mobjValues.OptionControl(0, "optOption", GetLocalResourceObject("optOption_2Caption"), "2", "2")%> </TD>
        </tr>
	</table>

</FORM> 
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("SIL704_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





