<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
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
function insStateZone(){
//--------------------------------------------------------------------------------------------------

}
/* ChangeRepCondi: Habilita/Deshabilita los controles dependientes del formato del reporte
/-------------------------------------------------------------------------------------------*/
function ChangeRepCondi(control){
/*-------------------------------------------------------------------------------------------*/
    switch (control){
        case "Zone" :
            UpdateDiv("valIntermedDesc","")
            UpdateDiv("lblnCliename","")
			with(self.document.forms[0]){
				valIntermed.value=""
				valClient.value=""

                cbeZone.disabled=false
				valIntermed.disabled=true
				self.document.btnvalIntermed.disabled=true
				valClient.disabled=true
				self.document.btnvalClient.disabled=true
			}
		    break;

        case "Intermed" :
            UpdateDiv("lblnCliename","")
			with(self.document.forms[0]){
				cbeZone.value=""
				valClient.value=""

                cbeZone.disabled=true
				valIntermed.disabled=false
				self.document.btnvalIntermed.disabled=false
				valClient.disabled=true
				self.document.btnvalClient.disabled=true
			}
		    break;

        case "Client" :
            UpdateDiv("valIntermedDesc","")
			with(self.document.forms[0]){
				cbeZone.value=""
				valIntermed.value=""

                cbeZone.disabled=true
				valIntermed.disabled=true
				self.document.btnvalIntermed.disabled=true
				valClient.disabled=false
				self.document.btnvalClient.disabled=false
			}
			break;

        default :
            UpdateDiv("valIntermedDesc","")
            UpdateDiv("lblnCliename","")
			with(self.document.forms[0]){
				cbeZone.value=""
				valIntermed.value=""
				valClient.value=""

                cbeZone.disabled=true
				valIntermed.disabled=true
				self.document.btnvalIntermed.disabled=true
				valClient.disabled=true
				self.document.btnvalClient.disabled=true
			}
			break;
	}	    
}
</SCRIPT>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("AGL003", "AGL003_K.aspx", 1, ""))
mobjMenu = Nothing
%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAGL003" ACTION="valAgentRep.aspx?X=1">
    <BR><BR>
    <TABLE WIDTH="100%" border="0">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101099><A NAME="Fechas"><%= GetLocalResourceObject("AnchorFechasCaption") %></A></LABEL></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101100><A NAME="Tipo de listado"><%= GetLocalResourceObject("AnchorTipo de listadoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HORLine"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HORLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101101><%= GetLocalResourceObject("tcdIniDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdIniDate", CStr(Today),  , GetLocalResourceObject("tcdIniDateToolTip"))%></TD>
            <TD WIDTH="10%"></TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101104, "OptList", GetLocalResourceObject("OptList_0Caption"), "1", "0")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="3"></TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101105, "OptList", GetLocalResourceObject("OptList_1Caption"),  , "1")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101102><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEndDate", CStr(Today),  , GetLocalResourceObject("tcdEndDateToolTip"))%></TD>
            <TD WIDTH="10%"></TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101106, "OptList", GetLocalResourceObject("OptList_2Caption"),  , "2")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=101103><A NAME="Intermediarios"><%= GetLocalResourceObject("AnchorIntermediariosCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HORLine"></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(101107, "optClient", GetLocalResourceObject("optClient_0Caption"),  , "0", "ChangeRepCondi(""Zone"");")%></TD>
            <TD COLSPAN="4"><%=mobjValues.PossiblesValues("cbeZone", "table9", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeZoneToolTip"))%></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(101108, "optClient", GetLocalResourceObject("optClient_1Caption"),  , "1", "ChangeRepCondi(""Intermed"");")%></TD>
            <TD COLSPAN="4"><%=mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valIntermedToolTip"))%></TD>
        </TR>
            <TD><%=mobjValues.OptionControl(101109, "optClient", GetLocalResourceObject("optClient_2Caption"),  , "2", "ChangeRepCondi(""Client"");")%></TD>
            <TD COLSPAN="4"><%=mobjValues.ClientControl("valClient", CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("valClientToolTip"),  , True, "lblnCliename")%></TD>
        </TR>
            <TD COLSPAN="5"><%=mobjValues.OptionControl(101110, "optClient", GetLocalResourceObject("optClient_3Caption"), "1", "3", "ChangeRepCondi();")%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





