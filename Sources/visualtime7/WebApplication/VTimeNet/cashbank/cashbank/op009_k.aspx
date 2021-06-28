<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de menú        
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "op009_k"
%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcdStartDate.disabled=false;
		btn_tcdStartDate.disabled=false;
		tcdEndDate.disabled=false;
		btn_tcdEndDate.disabled=false;
	    optChequeStat[0].disabled=false;
	    optChequeStat[1].disabled=false;
	    cbeConcept.disabled=false;
	    valClient.disabled=false;	    
	    }	
}

</SCRIPT>

<HTML>
    <HEAD>
        <%=mobjValues.WindowsTitle("OP009")%>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>        
        <%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("OP009", "OP009_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmChequesControl" ACTION="valCashBank.aspx?x=1">
        	<TD><BR></TD>
        	<TD><BR></TD>
            <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
            <TD><BR></TD>
            <TABLE WIDTH="100%">             
                <TR>
					<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Fechas"><%= GetLocalResourceObject("AnchorFechasCaption") %></A></LABEL></TD>
					<TD WIDTH="15%" COLSPAN="1">&nbsp;</TD>
                    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Estado de los cheques"><%= GetLocalResourceObject("AnchorEstado de los chequesCaption") %></A></LABEL></TD>
                </TR>                
                <TR>
                    <TD COLSPAN="2" CLASS="Horline"></TD>
                    <TD></TD> 
                    <TD COLSPAN="2" CLASS="Horline"></TD>
                </TR>
                <TR>
                    <TD><LABEL ID=0><%= GetLocalResourceObject("tcdStartDateCaption") %></LABEL></TD>
                    <TD><%=mobjValues.DateControl("tcdStartDate",  , True, GetLocalResourceObject("tcdStartDateToolTip"),  ,  ,  ,  , True)%></TD>
                    <TD COLSPAN="1">&nbsp;</TD>
                    <TD><%=mobjValues.OptionControl(40081, "optChequeStat", GetLocalResourceObject("optChequeStat_1Caption"), "1", "1",  , True)%></TD>
                 </TR>   
                 <TR>
                    <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
                    <TD><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True)%></TD>
                    <TD COLSPAN="1">&nbsp;</TD>                    
                    <TD><%=mobjValues.OptionControl(40082, "optChequeStat", GetLocalResourceObject("optChequeStat_2Caption"),  , "2",  , True)%></TD>
                </TR>
                <TR>
					<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40080><A NAME="Concepto"><%= GetLocalResourceObject("AnchorConceptoCaption") %></A></LABEL></TD>
					<TD COLSPAN="1">&nbsp;</TD>
                    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40080><A NAME="Beneficiario"><%= GetLocalResourceObject("AnchorBeneficiarioCaption") %></A></LABEL></TD>
                </TR>                
                <TR>
                    <TD COLSPAN="2" CLASS="Horline"></TD>
                    <TD></TD>
                    <TD COLSPAN="2" CLASS="Horline"></TD>
                </TR>
                <TR>
                    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeConceptCaption") %></LABEL></TD>
                    <TD><%=mobjValues.PossiblesValues("cbeConcept", "Table293", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeConceptToolTip"))%></TD>
                    <TD COLSPAN="1">&nbsp;</TD>
                    <TD><LABEL ID=0><%= GetLocalResourceObject("valClientCaption") %></LABEL></TD>
                    <TD><%=mobjValues.ClientControl("valClient", "",  , GetLocalResourceObject("valClientToolTip"),  , True)%></TD>                                        
                </TR>
            </TABLE>
            <%
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>





