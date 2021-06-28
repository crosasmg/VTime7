<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.  
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'**- Object for the handling of the zones of the page.  
'- Objeto para el manejo de las zonas de la página.

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "crl008_k"
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

//% getCompleteYear: Esta rutina se encarga de devolver el año completo (4 digitos) cuando se introduce incompleto (2 dígitos).
//----------------------------------------------------------------------------------------------------------------------------
function getCompleteYear(lstrValue){
//------------------------------------------------------------------------------------------------------------------------------
    var ldtmYear = new Date()
    var lintPos  
    var lstrYear
    var llngValue = 0
    lstrValue = lstrValue.replace(/\./g,'')
    llngValue = (lstrValue ==''?0:parseFloat(lstrValue))
    if (llngValue<1000)
        llngValue+=(llngValue<=50?2000:(llngValue<100?1900:2000))   
    return "" + llngValue    
 }   

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"        

</SCRIPT>

<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL008", "CRL008_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CRL008" ACTION="valCoReinsuranRep.aspx?sMode=1">
<BR></BR>
	<BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>    
	</BR>
	
	<TABLE WIDTH="100%" BORDER=0>
		<TR>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="5%"><LABEL ID=0><%= GetLocalResourceObject("tcnMonthCaption") %></LABEL></TD>
			<TD width="10%"><%=mobjValues.NumericControl("tcnMonth", 2, Session("nMonth"), False, GetLocalResourceObject("tcnMonthToolTip"))%></TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
		</TR>
		<TR>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="5%"><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
			<TD width="10%"><%=mobjValues.NumericControl("tcnYear", 4, Session("nYear"), False, GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  , "this.value= getCompleteYear(this.value)")%></TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




