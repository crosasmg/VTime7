﻿<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**- Object for the managing of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'**- Object for the managing of the zones of the page.
'- Objeto para el manejo de las zonas de la página.

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>

//**% insStateZone: Enabled the fiels of the form.
//% insStateZone: Habilita los campos de la forma.
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    if (typeof(document.forms[0])!='undefined'){		
		document.forms[0].elements["cbeBranch"].disabled = false;
	}
}

//**% insCancel: This procedure to cancel the page.
//% insCancel: Este procedimiento permite cancelar la página.
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("vil8033", "vil8033_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
<SCRIPT>

//**+ For the Source Safe control "DO NOT REMOVE"
//+ Para Control de Versiones "NO REMOVER"

    document.VssVersion="$$Revision: 2 $|$$Date: 10-05-06 12:08 $" 
</SCRIPT>
     
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VIL7012" ACTION="valPolicyRep.aspx?sMode=1">
    <BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))%>
    
    <BR><BR>

    <TABLE WIDTH="40%">
       <TR>
		
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
			<TD><%
mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType, CStr(Month(Today)),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeMonthToolTip")))
%></TD>
	            <TD><LABEL ID=LABEL3><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.NumericControl("tcnYear", 4, CStr(Year(Today)),  , GetLocalResourceObject("tcnYearToolTip"))%></TD>
			<TD></TD>	        
		</TR>
	    
    </TABLE>
    <BR>
    <table WIDTH="40%">
        <TR>
				<TD  ><LABEL ID=LABEL14><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			    		  
		 <tr>
             <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_1Caption"),  , "1",  , False)%> </TD> 
              <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_4Caption"),  , "4",  , False)%> </TD>             
		</tr>
		<tr>
             <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"),  , "2",  , False)%> </TD> 
              <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_5Caption"),  , "5",  , False)%> </TD>        
		</tr>
		<tr>
             <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_3Caption"),  , "3",  , False)%> </TD> 
             <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_0Caption"), CStr(1), "0",  , False)%> </TD>     
		</tr>
		
    </table>
      <BR> 
    <table WIDTH="20%">
        <TR>
			<TD ><LABEL ID=LABEL2><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			
		</TR>
        <TR>
             <TD><%=mobjValues.OptionControl(0, "optTypeCover", GetLocalResourceObject("optTypeCover_1Caption"), CStr(1), "1",  , False)%> </TD> 
        </TR>
        <TR>
             <TD><%=mobjValues.OptionControl(0, "optTypeCover", GetLocalResourceObject("optTypeCover_2Caption"),  , "2",  , False)%> </TD> 
        </TR>
    </table>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>






