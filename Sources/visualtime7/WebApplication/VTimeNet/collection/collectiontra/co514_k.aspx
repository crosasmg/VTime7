<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
    </SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">



<SCRIPT>

//% ShowChangeValues: Se cargan los valores de acuerdo al número de buletin
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	if (sField.value != '' && sField.value != '0')
    	
 		ShowPopUp("/VTimeNet/Collection/CollectionTra/ShowDefValues.aspx?Field=Bulletins" + "&nBulletins=" + self.document.forms[0].tctBulletins.value, "ShowDefValuesBulletin", 1, 1,"no","no",2000,2000);

}
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
function insStateZone()
//-------------------------------------------------------------------------------------------
{
    with (self.document.forms[0])
    { tctBulletins.disabled=false;
      tctClient.disabled=true;
      tctCurrency.disabled=true;
      tctWayPay.disabled=true;
      tctAmoun_pa.disabled=true;
      tctStatus.disabled=true;
	}
    
}
//% insCancel: Controla la acción cancelar de la página
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
	return (true);
}
</SCRIPT>

<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CO514", "CO514_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmPayReject" ACTION="ValCollectionTra.aspx?mode=1">
<BR></BR>
      
    <TABLE WIDTH="100%" BORDER=0>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tctBulletinsCaption") %></LABEL></TD>		
            <TD><%=mobjValues.NumericControl("tctBulletins", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tctBulletinsToolTip"),  ,  ,  ,  ,  , "ShowChangeValues(this)", True)%></TD>		    		    
        </TR>        
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="HighLighted"><LABEL ID=40509><A NAME="Boletin"><%= GetLocalResourceObject("AnchorBoletinCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>                                     
        <TR>    
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>		
            <TD><%=mobjValues.TextControl("tctClient", 46, " ",  , GetLocalResourceObject("tctClientToolTip"),  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctCurrencyCaption") %></LABEL></TD>		
            <TD><%=mobjValues.TextControl("tctCurrency", 18, " ",  , GetLocalResourceObject("tctCurrencyToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>              
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctWayPayCaption") %></LABEL></TD>		
            <TD><%=mobjValues.TextControl("tctWayPay", 14, " ",  , GetLocalResourceObject("tctWayPayToolTip"),  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctAmoun_paCaption") %></LABEL></TD>		
            <TD><%=mobjValues.TextControl("tctAmoun_pa", 16, " ",  , GetLocalResourceObject("tctAmoun_paToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>          
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctStatusCaption") %></LABEL></TD>	
            <TD><%=mobjValues.TextControl("tctStatus", 20, " ",  , GetLocalResourceObject("tctStatusToolTip"),  ,  ,  ,  , True)%></TD>                        
        </TR>            
</FORM>
</BODY>
</HTML>





