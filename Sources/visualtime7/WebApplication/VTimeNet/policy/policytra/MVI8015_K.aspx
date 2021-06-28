<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MVI8015_K")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "MVI8015_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MVI8015", "MVI8015_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
<SCRIPT> 
	//- Variable para el control de versiones
		document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

	//% insStateZone: se controla el estado de los controles de la página
	//-----------------------------------------------------------------------------
	function insStateZone(){
	//-----------------------------------------------------------------------------
		with (self.document.forms[0]){
			cbeBranch.disabled=false;
			tcdEffecdate.disabled=false;
			btn_tcdEffecdate.disabled=false;
		}	
	}
	    
	function insCancel(){
		return true;
	}
	function insFinish(){
	    return true;
	}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="MVI8015" ACTION="valPolicyTra.aspx?x=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=13848><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD>
            <%
Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , True))
%>
			</TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=13852><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True)%></TD>
        </TR>
        <TR>    
            <TD><LABEL ID=13837><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>        
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%> 

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("MVI8015_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




