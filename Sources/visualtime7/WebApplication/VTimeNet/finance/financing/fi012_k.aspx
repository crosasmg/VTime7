<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "fi012_k"
%>
<SCRIPT>
//% insCancel: 
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
    var lintIndex;
    var error;
    try {
		for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){
			self.document.forms[0].elements[lintIndex].disabled=false;
			if(self.document.images.length>0)
			    if(typeof(self.document.images["btn" + self.document.forms[0].elements[lintIndex].name])!='undefined')
			       self.document.images["btn" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled 
		}
	} catch(error){}		
}
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("FI012", "FI012_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="frmDrafCollection" ACTION="valFinancingVCVG.aspx?sMode=1">
    <TABLE WIDTH="100%">
            
        <TR>
            <TD WIDTH="10%"><LABEL ID=11151><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD>
            <TD WIDTH="10%"><%=mobjValues.NumericControl("tcnContrat", 8, Session("nContrat"),  ,  ,  , 0,  ,  ,  ,  , True)%></TD> 
            <TD WIDTH="5%">&nbsp</TD>            
            <TD WIDTH="10%"><LABEL ID=11154><%= GetLocalResourceObject("tcnQ_DraftCaption") %></LABEL></TD>
            <TD WIDTH="10%"><%=mobjValues.NumericControl("tcnQ_Draft", 4, Session("nQ_Draft"),  ,  ,  , 0,  ,  ,  ,  , True)%></TD>
        </TR>    
        <TR>
            <TD><LABEL ID=11162><%= GetLocalResourceObject("tcdStat_dateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdStat_date", Session("dStat_date"),  , GetLocalResourceObject("tcdStat_dateToolTip"),  ,  ,  ,  , True)%></TD>            
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





