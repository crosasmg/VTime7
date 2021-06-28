<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "fi015_k"
%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<HTML>
<HEAD>
<SCRIPT>

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}

//% ShowDefVal: Actualiza el valor de los campos "Cliente" y "Total giros" 
//------------------------------------------------------------------------------------------
function ShowDefVal(sField){
//------------------------------------------------------------------------------------------
	if (sField == 'Contrat') 
		ShowPopUp("ShowDefValues.aspx?Field=" + sField + "&nContrat=" + self.document.forms[0].tcnContrat.value, "ShowDefValuesFinance" , 1, 1,"no","no",2000,2000);
	if (sField == 'LastDraft')
		ShowPopUp("ShowDefValues.aspx?Field=" + sField +  "&nContrat=" + self.document.forms[0].tcnContrat.value +  "&nFirstDra=" + self.document.forms[0].tcnFirstDra.value + "&nLastDra=" + self.document.forms[0].tcnLastDra.value, "ShowDefValuesFinance", 1, 1,"no","no",2000,2000);
}

</SCRIPT>
        
    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("FI015", "FI015_k.aspx", 1, ""))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmUpdCollectAgent" ACTION="valFinancing.aspx?x=1">
<%With Response
	.Write("<BR><BR>")
End With
%>    
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=11128><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnContrat", 8, "",  ,  ,  , 0,  ,  ,  , "ShowDefVal('Contrat');")%></TD>
            <TD><LABEL ID=101766><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>
	    </TR>
	    <TR>
			<TD><LABEL ID=11192><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.ClientControl("tctClient", "",  , GetLocalResourceObject("tctClientToolTip"),  , True, "tctClieName", False)%></TD>
			
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAgentCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.PossiblesValues("tcnAgent", "TabIntermedia", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnAgentToolTip"))%></TD>
        </TR>
   </TABLE>
   <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="6">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Giros a cobrar"><%= GetLocalResourceObject("AnchorGiros a cobrarCaption") %></A></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Comisión"><%= GetLocalResourceObject("AnchorComisiónCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><HR></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2"><HR></TD>
        </TR>

    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="65%">
				<TABLE WIDTH="100%">
					<TR>
						<TD><LABEL ID=0><%= GetLocalResourceObject("tcnFirstDraCaption") %></LABEL></TD>
						<TD><%=mobjValues.NumericControl("tcnFirstDra", 4, "",  , GetLocalResourceObject("tcnFirstDraToolTip"),  , 0)%></TD>
						<TD><LABEL ID=0><%= GetLocalResourceObject("tcnLastDraCaption") %></LABEL></TD>
						<TD><%=mobjValues.NumericControl("tcnLastDra", 4, "",  , GetLocalResourceObject("tcnLastDraToolTip"),  , 0,  ,  ,  , "ShowDefVal('LastDraft');")%></TD>
					</TR>
				</TABLE>
			</TD>
			<TD>
				<TD><LABEL ID=0><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("tcnInterest", 6, "",  , GetLocalResourceObject("tcnInterestToolTip"),  , 0)%></TD>
			</TD>
	    </TR>
	    <TR>
			<TD WIDTH="65%">
				<TABLE WIDTH="100%">
					<TR>
						<TD WIDTH="50%">&nbsp;</TD>
						<TD><LABEL ID=0><%= GetLocalResourceObject("tcnTotDrafCaption") %></LABEL></TD>
						<TD><%=mobjValues.NumericControl("tcnTotDraf", 18, "",  , GetLocalResourceObject("tcnTotDrafToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
					</TR>
				</TABLE>
			</TD>
			<TD>
				<TD><LABEL ID=0><%= GetLocalResourceObject("tcnComAmoCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("tcnComAmo", 18, "",  , GetLocalResourceObject("tcnComAmoToolTip"), True, 6)%></TD>
			</TD>
	    </TR>
   </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%
mobjMenu = Nothing%>




