<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjCash_Mov As eCashBank.Cash_mov

'+ Indica si la búsqueda de los datos del depósito es efectiva.
Dim mblnFindDeposit As Boolean


'%insPreOP005: Esta función se encaga de obtener los datos del cheque
'--------------------------------------------------------------------------------------------
Private Sub insPreOP005()
	'--------------------------------------------------------------------------------------------
	Call mobjCash_Mov.FindByDocument(eCashBank.Cash_mov.CashTypMov.clngMCCheq, Session("sChequeNum"), Session("nBankCode"))
End Sub

'%insFindDeposit: Obtiene los datos de depósito
'--------------------------------------------------------------------------------------------
Private Sub insFindDeposit()
	'--------------------------------------------------------------------------------------------
	If mobjCash_Mov.FindByDeposit(2, mobjCash_Mov.sDep_number, mobjCash_Mov.nAcc_bank, Session("sChequeNum")) Then
		mblnFindDeposit = True
	Else
		mblnFindDeposit = False
	End If
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mobjCash_Mov = New eCashBank.Cash_mov
End With

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "op005"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
function insShowHeader(){
    var lblnContinue=true
    if (typeof(top.fraHeader.document)!='undefined') {
	    if (typeof(top.fraHeader.document.forms[0])!='undefined') {
			if (typeof(top.fraHeader.document.forms[0].tcnBankCode)!='undefined'){
				top.fraHeader.document.forms[0].tcnBankCode.value= '<%=Session("nBankCode")%>'
				top.fraHeader.$('#tcnBankCode').change();
				top.fraHeader.document.forms[0].tctChequeNum.value= '<%=Session("sChequeNum")%>'
				lblnContinue = false
			}
		}
	}
    if (lblnContinue)
		setTimeout("insShowHeader()",50);
}
setTimeout("insShowHeader()",50)
</SCRIPT>




<HTML>
    <HEAD>
        <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
        <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "OP005", "OP005.aspx"))
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmReturnedCheque" ACTION="valCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%
Call insPreOP005()
'+ Obtiene la fecha de efecto de la transacción.                
Response.Write(mobjValues.HiddenControl("hdEffecdate", CStr(mobjCash_Mov.dEffecdate)))
Response.Write(mobjValues.HiddenControl("hnTransac", CStr(mobjCash_Mov.nTransac)))
%>
            
            <TABLE WIDTH=100%>
                <TR>
                    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40063><A NAME="Datos de verificación"><%= GetLocalResourceObject("AnchorDatos de verificaciónCaption") %></A></LABEL></TD>
                </TR>
                <TR>
                    <TD COLSPAN="5">&nbsp;</TD>
                </TR>
                <TR>
                    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40064><A NAME="Asociados al cheque devuelto"><%= GetLocalResourceObject("AnchorAsociados al cheque devueltoCaption") %></A></LABEL></TD>
                </TR>
                <TR>
                    <TD WIDTH="100%" COLSPAN="5"><HR></TD>
                </TR>
                <TR>
                    <TD><LABEL ID=8894><%= GetLocalResourceObject("tcdChequeDateCaption") %></LABEL></TD>
                    <TD><%=mobjValues.DateControl("tcdChequeDate", CStr(mobjCash_Mov.dDoc_date),  , "", True)%></TD>
                    <TD><LABEL ID=8893><%= GetLocalResourceObject("tcnChequeAmountCaption") %></LABEL></TD>
                    <TD><%=mobjValues.NumericControl("tcnChequeAmount", 18, CStr(mobjCash_Mov.nAmount),  , "", True, 6, True)%></TD>
                    <TD><%=mobjValues.PossiblesValues("tcnCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCash_Mov.nCurrency),  , True)%></TD>
                </TR>
                <TR>
                    <TD COLSPAN="5">&nbsp;</TD>
                </TR>
                <TR>
                    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40065><A NAME="Asociados al depósito"><%= GetLocalResourceObject("AnchorAsociados al depósitoCaption") %></A></LABEL></TD>
                </TR>
                <TR>
                    <TD COLSPAN="5"><HR></TD>
                </TR>
                <%
Call insFindDeposit()
%>
                
                <TR>
                    <TD><LABEL ID=8890><%= GetLocalResourceObject("tcnAccountNumCaption") %></LABEL></TD>
                    <TD><%=mobjValues.NumericControl("tcnAccountNum", 12, CStr(mobjCash_Mov.nAcc_bank),  , "",  , 0, True)%></TD>
                    <TD><%=mobjValues.TextControl("tcnBankName", 2, mobjCash_Mov.sDescript,  , "", True)%></TD>
                    <TD COLSPAN = 2></TD>
                </TR>
                <TR>
                    <TD><LABEL ID=8889><%= GetLocalResourceObject("tcdAccountDateCaption") %></LABEL></TD>
                    <TD><%=mobjValues.DateControl("tcdAccountDate", CStr(mobjCash_Mov.dDoc_date),  , "", True)%></TD>
                    <%=mobjValues.HiddenControl("hdAccountDate", CStr(mobjCash_Mov.dDoc_date))%>
                    <%=mobjValues.HiddenControl("hnAccountCash", CStr(mobjCash_Mov.nAcc_cash))%>
                    <%=mobjValues.HiddenControl("hnCurrency", CStr(mobjCash_Mov.nCurrency))%>
                    <%=mobjValues.HiddenControl("hnOffice", CStr(mobjCash_Mov.nOffice))%>
                    
                    <TD><LABEL ID=8896><%= GetLocalResourceObject("tcnDepNumberCaption") %></LABEL></TD>
                    <TD><%=mobjValues.TextControl("tcnDepNumber", 2, CStr(mobjCash_Mov.nTransac),  , "", True)%></TD>
                </TR>
                <TR>
                    <TD COLSPAN="5">&nbsp;</TD>
                </TR>
                <TR>
                    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40066><A NAME="Gastos por cheque devuelto"><%= GetLocalResourceObject("AnchorGastos por cheque devueltoCaption") %></A></LABEL></TD>
                </TR>
                <TR>
                    <TD COLSPAN="5"><HR></TD>
                </TR>
                <TR>
	    	    	<TD><LABEL ID=8892><%= GetLocalResourceObject("tcnCashAmountCaption") %></LABEL></TD>
                    <TD><%=mobjValues.NumericControl("tcnCashAmount", 18, CStr(mobjCash_Mov.nCash_Amount),  , "", True, 6)%></TD>
                    <TD></TD>
                    <TD><LABEL ID=8897><%= GetLocalResourceObject("tcdRetDateCaption") %></LABEL></TD>
                    <TD><%=mobjValues.DateControl("tcdRetDate", CStr(mobjCash_Mov.dDat_return),  , "")%></TD>
                </TR>
            </TABLE>
            <%Response.Write(mobjValues.BeginPageButton)%>
        </FORM>
    </BODY>
</HTML>
<%

mobjValues = Nothing
mobjMenu = Nothing
mobjCash_Mov = Nothing

%>




