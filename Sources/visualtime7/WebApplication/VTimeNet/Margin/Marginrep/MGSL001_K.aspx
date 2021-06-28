<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>

<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGSL001"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Personalización VTime">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 19/11/03 17:33 $|$$Author: Nvaplat62 $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
   return true
}
</SCRIPT>

<SCRIPT language="VB"  RUNAT=SERVER>
'Esta funcion obtiene el ultimo día del mes, el truco está en que esta función será ejecutada en el
'servidor (RUNAT = SERVER)  y además se aprovecha la funcion DateSerial, que al pasarle
'por ejemplo 31/04/2003 inmediatamente sabe que se refiere a la fecha 01/05/2003
Function GetLastDay(ByRef datTheDate As Date) As Integer
	Dim intMonthNum As Short
	Dim intYearNum As Short
	Dim intResult As Integer
	Dim intLastDay As Integer
	Dim datTestDay As Date
	intMonthNum = Month(datTheDate)
	intYearNum = Year(datTheDate)
	intResult = 28
	Dim counter As Short
	counter = intResult
	For intLastDay = counter To 31
		datTestDay = DateSerial(intYearNum, intMonthNum, intLastDay)
		If Month(datTestDay) = intMonthNum Then
			intResult = intLastDay
		End If
	Next 
	GetLastDay = intResult
End Function

</SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MGSL001", "MGSL001_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MGSL001" ACTION="valmarginrep.aspx?sMode=1">
    <BR><BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%" >
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInitDateCaption") %> </LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdInitDate", CStr(DateSerial(Year(Today), Month(Today), 1)),  , GetLocalResourceObject("tcdInitDateToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %> </LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEndDate", CStr(DateSerial(Year(Today), Month(Today), GetLastDay(Today))),  , GetLocalResourceObject("tcdEndDateToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjMenu = Nothing
mobjValues = Nothing
%>






