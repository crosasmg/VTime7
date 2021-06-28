<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

mobjValues.sCodisplPage = "col684_k"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
</SCRIPT>    
<SCRIPT>

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("COL684"))
	.Write(mobjMenu.MakeMenu("COL684", "COL684_K.aspx", 1, ""))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "COL684_K.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmModCollect" ACTION="valCollectionRep.aspx?mode=1">
<BR><BR><BR>
<%Response.Write(mobjValues.ShowWindowsName("COL684"))%>    
    <TABLE WIDTH="100%">
    <BR>        
        <TR>
            <TD WIDTH="15%"><LABEL ID="10295"><%= GetLocalResourceObject("cbeInsurAreaCaption") %></LABEL></TD>
            <%mobjValues.BlankPosition = False%>      			
            <TD WIDTH="35%"><%=mobjValues.PossiblesValues("cbeInsurArea", "table5001", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInsurAreaToolTip"))%></TD>                
            <TD WIDTH="15%"><LABEL ID="10528"><%= GetLocalResourceObject("tcdProcessDateCaption") %></LABEL></TD>
		    <TD WIDTH="35%"><%=mobjValues.DateControl("tcdProcessDate", CStr(Today))%></TD>            
		</TR>		
		<TR>
		   <TD COLSPAN="4">&nbsp;</TD>
		</TR>
		<TR>		    
		    <TD><LABEL ID="10295"><%= GetLocalResourceObject("valCollectorPreCaption") %></LABEL></TD>
			<TD> <%=mobjValues.PossiblesValues("valCollectorPre", "tabCollector_Cliname", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valCollectorPreToolTip"))%></TD>
			<TD><LABEL ID="10295"><%= GetLocalResourceObject("valCollectorNewCaption") %></LABEL></TD>	
			<TD> <%=mobjValues.PossiblesValues("valCollectorNew", "tabCollector_Cliname", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valCollectorNewToolTip"))%></TD>			
		</TR>		
    </TABLE>    
</FORM>
</BODY>
</HTML>





