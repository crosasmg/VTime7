<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "opl001_k"
%>
<HTML>
<HEAD>

<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   

//% insCancel: Ejecuta rutinas necesarias en el momento de Finalizar la página
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}

//% insStateZone: se manejan los campos de la página
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
	var lintIndex;
    var error;
    try {
		for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){
			self.document.forms[0].elements[lintIndex].disabled=false;
			if(self.document.images.length>0)
			    if(typeof(self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name])!='undefined')
					self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled;
		}
		self.document.forms[0].elements["btn_tcdEnddate"].disabled = false;
		self.document.forms[0].elements["btn_tcdIniTDate"].disabled = false;
		self.document.forms[0].elements["btnvalOriAccount"].disabled = false;
	} catch(error){}	
	
}	   
</SCRIPT>

<META http-equiv="Content-Language" content="es">
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("OPL001", "OPL001_K.aspx", 1, ""))
        mobjMenu = Nothing
%>
    <BR>
</HEAD>

<BODY Class="Header" VLink="white" LINK="white" alink="white">
<BR>
<form METHOD="post" ID="FORM" NAME="frmChequesReport" ACTION="ValCashBankRep.aspx?X=1">
    <table WIDTH="100%">
   <TR>
   </TR>
   <TR>
       <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=101020><A NAME="Fecha"><%= GetLocalResourceObject("AnchorFechaCaption") %></A></LABEL></TD>
   </TR>
   
   <TR>
       <TD COLSPAN="5"><HR></TD>
   </TR>
   <TR> </TR>
   <TR>
       <TD><LABEL ID=8993><%= GetLocalResourceObject("tcdIniTDateCaption") %></LABEL></TD>
       <TD><%=mobjValues.DateControl("tcdIniTDate", mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdIniTDateToolTip"),  ,  ,  ,  , True, 1)%></TD>
       <TD>&nbsp;&nbsp;</TD>
       <TD><LABEL ID=8993><%= GetLocalResourceObject("tcdEnddateCaption") %></LABEL></TD>
       <TD><%=mobjValues.DateControl("tcdEnddate", mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdEnddateToolTip"),  ,  ,  ,  , True, 2)%></TD>
   </TR>
   <TR>
       <TD COLSPAN="5"><HR></TD>
   </TR>
   </TABLE>
   <TABLE WIDTH="100%">   
   <TR>
   	   <TD><LABEL ID=8596><%= GetLocalResourceObject("valOriAccountCaption") %></LABEL></TD>
       <TD><%=mobjValues.PossiblesValues("valOriAccount", "tabBank_acc", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Session("nOriAccount"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valOriAccountToolTip"))%></TD>
   </TR>
   <TR>
       <TD><LABEL ID=9031><%= GetLocalResourceObject("cbeStatusCheckCaption") %></LABEL></TD>
	   <TD><%=mobjValues.PossiblesValues("cbeStatusCheck", "Table187", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nStatusCheck"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatusCheckToolTip"),  , 10)%></TD>
   </TR>
   </TABLE>
   <%mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>




