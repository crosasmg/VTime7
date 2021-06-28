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

mobjValues.sCodisplPage = "op005_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}   

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
	self.document.forms[0].tcnBankCode.disabled = false
	self.document.btntcnBankCode.disabled = false
	self.document.forms[0].tctChequeNum.disabled = false
}

function insFinish(){
    return true;
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
	.Write(mobjMenu.MakeMenu("OP005", "OP005_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmReturnedCheque" ACTION="valCashBank.aspx?x=1">
           	<TD><BR></TD>
        	<TD><BR></TD>
            <TABLE WIDTH="100%">
                <TR>
                    <TD><LABEL ID=8891><%= GetLocalResourceObject("tcnBankCodeCaption") %></LABEL></TD>
                    <TD WIDTH=20%><%=mobjValues.PossiblesValues("tcnBankCode", "Table7", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , "")%></TD>
                    <TD><LABEL ID=8895><%= GetLocalResourceObject("tctChequeNumCaption") %></LABEL></TD>
                    <TD><%=mobjValues.NumericControl("tctChequeNum", 10, "",  , "",  ,  ,  ,  ,  ,  , True)%></TD>
                </TR>
            </TABLE>
            <%
mobjValues = Nothing
mobjMenu = Nothing
%>
        </FORM>
    </BODY>
</HTML>





