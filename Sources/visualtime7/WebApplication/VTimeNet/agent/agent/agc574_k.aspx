<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"        

//% InsChangeValues: Se actualizan los parametros de las listas de valores 
//------------------------------------------------------------------------------------------- 
function InsChangeValues(Field)
//------------------------------------------------------------------------------------------- 
{
	var strParams; 
	switch(Field.name){
		case "valIntermed": 
    		strParams = "nIntermed=" + self.document.forms[0].valIntermed.value   
			insDefValues('Intermed',strParams,'/VTimeNet/Agent/Agent'); 
 			break;
	}
}
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
	with (self.document.forms[0])
	{
		valIntermed.disabled = false;
		btnvalIntermed.disabled = valIntermed.disabled;
		tcnYear.disabled = false;
		cbePerType.disabled = false;
		tcnPerNumber.disabled = false;
		cbeInforType.disabled = false;
		cbeCurrency.disabled = false;
		tcdEffecdate.disabled = false;
		btn_tcdEffecdate.disabled = tcdEffecdate.disabled;
	}
}

//------------------------------------------------------------------------------------------
function insSetNumber()
//------------------------------------------------------------------------------------------
{
    with (document.forms[0])
    {
		tcnPerNumber.disabled = (cbePerType.value == "5");
		if (tcnPerNumber.disabled)
		    tcnPerNumber.value = "1";

    }
}
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{
	return true;
}   

</SCRIPT>
	<meta http-equiv="Content-Language" content="es">
    <%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("AGC574", "AGC574_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY Class="Header" VLink=white LINK=white alink=white >
<FORM METHOD="post" ID="FORM" NAME="frmIntermBud" ACTION="valAgent.aspx?Zone=1">
<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8043><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%= mobjValues.PossiblesValues("valIntermed", "Intermedia_o", eFunctions.Values.eValuesType.clngWindowType, Session("nIntermed"), , , , , , "InsChangeValues(this);", True, 10, GetLocalResourceObject("valIntermedToolTip"))%></TD>
        <TR>
        </TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valGoalsCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.PossiblesValues("valGoals", "TabTab_Goals", eFunctions.Values.eValuesType.clngWindowType, Session("nGoals"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valGoalsToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40015><a NAME="Período"><%= GetLocalResourceObject("AnchorPeríodoCaption") %></a></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN = 5><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8046><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear", 4, Session("nYear"),  , "",  , 0,  ,  ,  ,  , True)%></TD>
            <TD WIDTH=5%>&nbsp;</TD>
            <TD><LABEL ID=8042><%= GetLocalResourceObject("cbeInforTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeInforType", "table276", 1, Session("sType_infor"),  ,  ,  ,  ,  ,  , True,  , "")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8045><%= GetLocalResourceObject("cbePerTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeList = 2
mobjValues.List = "6, 7"
Response.Write(mobjValues.PossiblesValues("cbePerType", "table108", 1, Session("sPeriodtyp"),  ,  ,  ,  ,  , "insSetNumber();", True,  , ""))
%>
			</TD>
			<TD>&nbsp;</TD>
            <TD><LABEL ID=8040><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", 1, Session("nCurrency"),  ,  ,  ,  ,  ,  , True,  , "")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8044><%= GetLocalResourceObject("tcnPerNumberCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPerNumber", 2, Session("nPeriodnum"),  , "",  , 0,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=8041><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





