<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Se define la variable para el manejo de las opciones de la página	
Dim mclsSequence As eFunctions.Sequence
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1

mclsSequence = New eFunctions.Sequence
mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
	<META NAME="ProgId" CONTENT="FrontPage.Editor.Document">
<TITLE>Información general</TITLE>
<BASE TARGET="fraFolder">
<SCRIPT>
    var pintZone;
    var plngMainAction = 0;
    var mintZone = 0;
    var pstrOnSeq = '2';
    	
    function insGetCurrZone(){
    	return mintZone
    }		
    function insLetCurrZone(lintZone){
    	mintZone = lintZone
    }	
</SCRIPT>
</HEAD>
<!-- <BODY <%=mclsSequence.BODYParameters()%> STYLE="background-repeat: repeat-x;background-color:RGB(0,128,177)"> -->
<BODY id="left_frame">
    <!--<img src="../images/Logos/CompanyLogo.gif" alt="" hspace="3" vspace="5"/>-->
	<IMG NAME=logo SRC="/VTimeNet/images/logo.gif" ALT="Logo de la empresa" WIDTH="121" HEIGHT="96">
	<SCRIPT>document.logo.style.filter="Alpha(opacity=80)";</SCRIPT>
</BODY>
</HTML>
<%
mclsSequence = Nothing%>




