<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
<SCRIPT>
//------------------------------------------------------------------------------
// Para Control de Versiones "NO REMOVER"
//------------------------------------------------------------------------------
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	if(top.frames['fraSequence'].pintZone==1){
		with(self.document.forms[0]){
			//valIntermed.value=<%="""" & Session("nIntermed") & """"%>;
			//UpdateDiv("valIntermedDesc","");
		}
	}
	else		
		return true;
}

//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA031_K", "CA031_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="post" ID="FORM" NAME="frmRenewalProcess" ACTION="valPolicyTra.aspx?Zone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101088><a NAME="Ejecución"><%= GetLocalResourceObject("AnchorEjecuciónCaption") %></a></LABEL></td>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101089><a NAME="Información a tomar"><%= GetLocalResourceObject("AnchorInformación a tomarCaption") %></a></LABEL></td>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD WIDTH=10%></TD>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(101090, "optRenewal", GetLocalResourceObject("optRenewal_1Caption"), "1", "1")%></TD>
            <TD><%=mobjValues.OptionControl(101091, "optRenewal", GetLocalResourceObject("optRenewal_2Caption"),  , "2")%></TD>
            <TD></TD>
            <TD><%=mobjValues.OptionControl(101092, "optInfo", GetLocalResourceObject("optInfo_1Caption"), "1", "1")%></TD>
            <TD><%=mobjValues.OptionControl(101093, "optInfo", GetLocalResourceObject("optInfo_2Caption"),  , "2")%></TD>
        </TR>
    </TABLE>
</FORM>
<%
mobjValues = Nothing%>
</BODY>
</HTML>





