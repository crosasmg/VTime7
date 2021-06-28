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
mobjValues.sCodisplPage = "MGSL004"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Personalización VTime">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 4/12/03 17:04 $|$$Author: Nvaplat15 $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
   return true
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MGSL004", "MGSL004_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MGSL004" ACTION="valmarginrep.aspx?sMode=1">
    <BR><BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%" >
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInitDateCaption") %> </LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdInitDate",  ,  , GetLocalResourceObject("tcdInitDateToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %> </LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<Script>
    var today = new Date();
    var month = today.getMonth()+1;
    var year = today.getYear();
    var montharr = new Array;
    montharr[0]  = 31;
    montharr[1]  = 28;
    montharr[2]  = 31;
    montharr[3]  = 30;
    montharr[4]  = 31;
    montharr[5]  = 30;
    montharr[6]  = 31;
    montharr[7]  = 31;
    montharr[8]  = 30;
    montharr[9]  = 31;
    montharr[10] = 30;
    montharr[11] = 31;
    if (((year % 4 ==0) && 
         (year % 100 != 0)) || 
         (year % 400 == 0))
        montharr[1] = 29;
    if (month <= 9){
        self.document.forms[0].tcdInitDate.value='01/'+'0'+month+'/'+year;
        self.document.forms[0].tcdEndDate.value=montharr[month-1]+'/'+'0'+month+'/'+year;
    }
    else{
        self.document.forms[0].tcdInitDate.value='01/'+month+'/'+year;    
        self.document.forms[0].tcdEndDate.value=montharr[month-1]+'/'+month+'/'+year;
    }
</Script>
<%
mobjMenu = Nothing
mobjValues = Nothing
%>




