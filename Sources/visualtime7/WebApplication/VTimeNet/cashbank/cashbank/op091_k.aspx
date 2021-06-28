<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "op091_k"
%>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript">
//% ShowRemNum: Se muestra el número de remesa asignado por el sistema
//-------------------------------------------------------------------------------------------
function ShowRemNum(nTypeTrans){
//-------------------------------------------------------------------------------------------
	if (nTypeTrans == "1" )
	{
		ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=RemNum", "ShowDefValuesRemNum", 1, 1,"no","no",2000,2000);
	}
	else
	{
		self.document.forms[0].gmnRemNum.value = 0;
		self.document.forms[0].gmnRemNum.disabled = false;
	}
		
}

function insStateZone(){

}
	
function insCancel(){
	return true;
}   
function insFinish(){
    return true;
}

	</SCRIPT>
	<%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("OP091"))
	.Write(mobjMenu.MakeMenu("OP091", "OP091_k.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>    
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmPayRemitt" ACTION="ValCashBank.aspx?Zone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=8818><%= GetLocalResourceObject("cbeTypeTransCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTypeTrans", "table403", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "ShowRemNum(this.value)",  ,  , "")%></TD>
            <TD><LABEL ID=8815><%= GetLocalResourceObject("gmnRemNumCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("gmnRemNum", 7, "",  , "",  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
<%
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




