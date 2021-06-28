<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mintBranch As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "CRL046_k"
%>

<HTML>
<HEAD>
		<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
//------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------------------
}
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}
//--------------------------------------------------------------------------------------------------
function EnabledField(Field)
//--------------------------------------------------------------------------------------------------
{
	if(Field==1 || Field==2 || Field==4){
		self.document.forms[0].elements["cbeBranchRei"].value=0;
		self.document.forms[0].elements["cbeBranchRei"].disabled=true;
	}
	else
		self.document.forms[0].elements["cbeBranchRei"].disabled=false;
}


</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL046", "CRL046_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CRL046" ACTION="valCoReinsuranRep.aspx?sMode=1">
	<BR><BR><BR>
     <%Response.Write(mobjValues.ShowWindowsName("CAL046", Request.QueryString.Item("sWindowDescript")))%>
	<TABLE WIDTH="70%" align="center">

		<TR>
			<TD><LABEL ID=13937><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></TD>
		</TR>
		<TR>	
		    <TD><LABEL ID=13947><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
		    <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
		</TR>
        <TR>
            <TD> <LABEL ID=40281><%=GetLocalResourceObject("tcnPolicyCaption")%></LABEL> </TD>
            <TD> <%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip") )%> </TD>
        </TR>
        
	</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




