<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mcolClass As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

    
    
    <SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:00 $"   
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
  return true;
}
//% insTypIntermed: se controla el sp a llamar dependiendo del tipo de intermediario
function insTypIntermed(opIntermed){
//--------------------------------------------------------------------------------------------
typinter = opIntermed.value

with(self.document.forms[0]){

    valInterOld.value='';
    UpdateDiv('valInterOldDesc','','Normal');
    
    if (typinter == 5){
       valInterOld.sTabName = 'TabIntermedia_Superv';
       valInterNew.sTabName = 'TabIntermedia_Superv';
    }   
    else	 
       {
       valInterOld.sTabName = 'TabIntermedia_Assist';
       valInterNew.sTabName = 'TabIntermedia_Assist';
       }
  } 
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("AG781"))
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu("AG781", "AG781_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="AG781_k" ACTION="ValAgent.aspx?sMode=2">
	<BR>
    <TABLE WIDTH="100%" >
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD><%=mobjValues.OptionControl(0, "optIntermed", GetLocalResourceObject("optIntermed_9Caption"), "1", "9", "insTypIntermed(this)")%> </TD>
			<TD><%=mobjValues.OptionControl(0, "optIntermed", GetLocalResourceObject("optIntermed_5Caption"), "2", "5", "insTypIntermed(this)")%> </TD>	        
			<TD colspan=2>&nbsp;</TD>
		</TR>

		<TR>&nbsp;</TR>
		<TR>		
        	<TD><LABEL ID=0><%= GetLocalResourceObject("cbeInsur_AreaCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeInsur_Area", "Table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInsur_AreaToolTip"),  , 2)%> </TD>
			<TD COLSPAN=2>&nbsp;</TD>
       </TR>
	  <TR>&nbsp;</TR>
	  <TR>
	   	<TD><LABEL ID=0> <%= GetLocalResourceObject("valInterOldCaption") %></LABEL></TD>
	   	<TD><%=mobjValues.PossiblesValues("valInterOld", "tabIntermedia_assist", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , False,  7, GetLocalResourceObject("valInterOldToolTip"),  ,  ,  , True)%></TD>
		<TD COLSPAN=2>&nbsp;</TD>			   
	  </TR> 
	  <TR>&nbsp;</TR>
	  <TR>
	   	<TD><LABEL ID=0> <%= GetLocalResourceObject("valInterNewCaption") %></LABEL></TD>
	   	<TD ><%=mobjValues.PossiblesValues("valInterNew", "tabIntermedia_assist", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , False, 7, GetLocalResourceObject("valInterNewToolTip"),  ,  ,  , True)%></TD>
	    <TD COLSPAN=2>&nbsp;</TD>		
	  </TR> 	  
    </TABLE>
</FORM> 
</BODY>
</HTML>




