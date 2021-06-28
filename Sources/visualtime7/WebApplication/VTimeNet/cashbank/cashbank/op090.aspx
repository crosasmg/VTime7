<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjCurr_acc As eCashBank.Curr_acc


'%insPreOP090: Esta función se encaga de obtener los datos de la cuenta corriente
'--------------------------------------------------------------------------------------------
Private Sub insPreOP090()
	'--------------------------------------------------------------------------------------------
	
	mobjCurr_acc = New eCashBank.Curr_acc
	If Not mobjCurr_acc.findClientCurr_acc(mobjValues.StringToType(Session("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), Session("sBussiType"), Session("sClient"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble)) Then
		mobjCurr_acc.dEffecdate = Today
		mobjCurr_acc.sAccount = vbNullString
		mobjCurr_acc.sAux_accoun = vbNullString
		mobjCurr_acc.nLed_compan = eRemoteDB.Constants.intNull
	End If
	
End Sub

</script>
<%Response.Expires = 0

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

mobjValues.sCodisplPage = "op090"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//-------------------------------------------------------------------------------------------
//%	ChangeValue: Realiza el pase de parámetros para hacer la búsqueda de Cuenta contable o Auxiliar
//-------------------------------------------------------------------------------------------
function ChangeValue(nParamType,nValue){
//-------------------------------------------------------------------------------------------
    if (nParamType == 1)
    {
		document.forms[0].valLedgerAcc.Parameters.Param1.sValue=nValue
		document.forms[0].valLedgerAux.Parameters.Param2.sValue=nValue
    }
    else
	{
		document.forms[0].valLedgerAux.Parameters.Param1.sValue=nValue
	}
}
/*
function insShowHeader(){
    var lblnContinue=true

    if (typeof(top.fraHeader.document)!='undefined') {
	    if (typeof(top.fraHeader.document.forms[0])!='undefined') {
			if (typeof(top.fraHeader.document.forms[0].cbeCurrency)!='undefined'){
				top.fraHeader.document.forms[0].cbeTypeAccount.value= '<%=Session("nTypeAccount")%>'
				top.fraHeader.document.forms[0].cbeBussiType.value=  '<%=Session("sBussiType")%>' 
				top.fraHeader.document.forms[0].valClient.value= '<%=Session("sClient")%>' 
				top.fraHeader.$('#valClient').change(); 
				top.fraHeader.document.forms[0].cbeCurrency.value= '<%=Session("nCurrency")%>' 
				lblnContinue = false
			}
		}
	}
	if (lblnContinue)
		setTimeout("insShowHeader()",50);
}
setTimeout("insShowHeader()",50);
*/
</SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%
    Response.Write(mobjValues.StyleSheet())
    Response.Write(mobjMenu.setZone(2, "OP090", "OP090.aspx"))
    If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
    	mobjValues.ActionQuery = True
    End If
    %>
</HEAD>
<%
Call insPreOP090()
%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="ValCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%" border=0>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=8737><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", mobjValues.TypetoString(mobjCurr_acc.dEffecdate, eFunctions.Values.eTypeData.etdDate), True, GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40102><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD WIDTH="20%"><LABEL ID=8738><%= GetLocalResourceObject("cbeLedCompanCaption") %></LABEL></TD>
            <TD WIDTH="30%" COLSPAN="3"><%=mobjValues.PossiblesValues("cbeLedCompan", "tabLed_compan", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCurr_acc.nLed_compan),  ,  ,  ,  ,  , "ChangeValue(1,this.value)",  ,  , GetLocalResourceObject("cbeLedCompanToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8739><%= GetLocalResourceObject("valLedgerAccCaption") %></LABEL></TD>
            <TD COLSPAN="3">
                <%
                With mobjValues
	                .Parameters.Add("nLed_compan", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                Response.Write(.PossiblesValues("valLedgerAcc", "tabLedger_acc1", eFunctions.Values.eValuesType.clngWindowType, mobjCurr_acc.sAccount, True,  ,  ,  ,  , "ChangeValue(2,this.value)",  , 20, GetLocalResourceObject("valLedgerAccToolTip"), 2))
                End With
                %>
			</TD>				            
        </TR>
        <TR>
			<TD><LABEL ID=8733><%= GetLocalResourceObject("valLedgerAuxCaption") %></LABEL></TD>
			<TD COLSPAN="3">
				<%
                With mobjValues
	                .Parameters.Add("sAccount", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                .Parameters.Add("nLed_compan", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                Response.Write(.PossiblesValues("valLedgerAux", "tabLedger_acc2", eFunctions.Values.eValuesType.clngWindowType, mobjCurr_acc.sAux_accoun, True,  ,  ,  ,  ,  ,  , 20, GetLocalResourceObject("valLedgerAuxToolTip"), 2))
                End With
                %>
			</TD>
        </TR>        
    </TABLE>
    <%'mobjValues.BeginPageButton %>
</FORM>
</BODY>
</HTML>
<%
mobjCurr_acc = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>




