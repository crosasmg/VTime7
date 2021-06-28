<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página.

Dim mobjMenu As eFunctions.Menues
Dim lclsCtrol_date As eGeneral.Ctrol_date

'+ Generación de cesiones de siniestros.

Const clngGenCessClaim As Short = 43


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
lclsCtrol_date = New eGeneral.Ctrol_date

mobjValues.sCodisplPage = "MGSL010_k"
%>
<HTML>

<HEAD>

    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT> 

//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 14 $|$$Date: 2/12/03 17:15 $" 

</SCRIPT> 
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MGSL010", "MGSL010_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MGSL010" ACTION="valMarginRep.aspx?sMode=1">

<%
'If lclsCtrol_date.Find(clngGenCessClaim) Then
'   Session("dLastExecuteDate") = mobjValues.TypeToString(lclsCtrol_date.dEffecdate,eFunctions.Values.eTypeData.etdDate)
'End If    
%>

<BR></BR>
	<BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>    
	</BR>
	
	<TABLE WIDTH="100%">
		<TR>
			<TD width="10%">&nbsp;</TD>
			<TD width="15%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateFromCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdDateFrom",  ,  , GetLocalResourceObject("tcdDateFromToolTip"))%></TD>
			<TD width="15%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateToCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdDateTo",  ,  , GetLocalResourceObject("tcdDateToToolTip"))%></TD>
            <TD width="10%">&nbsp;</TD>
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
        self.document.forms[0].tcdDateFrom.value='01/'+'0'+month+'/'+year;
        self.document.forms[0].tcdDateTo.value=montharr[month-1]+'/'+'0'+month+'/'+year;
    }
    else{
        self.document.forms[0].tcdDateFrom.value='01/'+month+'/'+year;    
        self.document.forms[0].tcdDateTo.value=montharr[month-1]+'/'+month+'/'+year;
    }
</Script>
<%
mobjValues = Nothing
lclsCtrol_date = Nothing
%>





