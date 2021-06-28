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

mobjValues.sCodisplPage = "opc012_k"
%>
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}   

//------------------------------------------------------------------------------------------   
//% insStateZone() : Se habilitan los campos que correspondan con el tipo de acción a 
//% realizar.
//------------------------------------------------------------------------------------------
function insStateZone(){
    switch (top.frames['fraSequence'].plngMainAction){
        case 401:
            self.document.forms[0].tcdOperdate.disabled = false
            self.document.btn_tcdOperdate.disabled = false
            
            self.document.forms[0].cboTypeAccount.disabled = false
            self.document.forms[0].tctClient.disabled = false
            
            self.document.forms[0].cboCurrency.disabled = false
            document.images["btncboCurrency"].disabled = false
            break;
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
    return true;
}    


//% SetCurrency: Determina las acciones a tomar en la moneda según el tipo de cuenta.
//------------------------------------------------------------------------------------------
function SetCurrency(){
//------------------------------------------------------------------------------------------
	if( self.document.forms[0].tctClient.value != "" && self.document.forms[0].cboTypeAccount.value)
    ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=Curren&scodispl=OPC012&nType_acc=" + self.document.forms[0].cboTypeAccount.value + "&sClient=" + self.document.forms[0].tctClient.value, "ShowDefValuesCashBank", 1, 1,"no","no",2000,2000);
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
    <HEAD>
        <%=mobjValues.WindowsTitle("OPC012")%>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
        <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("OPC012", "OPC012_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmQClaimMov" ACTION="valCashBank.aspx?x=1">
           	<TD><BR></TD>
          	<TD><BR></TD>
          	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
            <TABLE WIDTH="100%">
                <TR>
                    <TD><LABEL ID=8829><%= GetLocalResourceObject("tcdOperdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdOperdate", CStr(Today),  , GetLocalResourceObject("tcdOperdateToolTip"),  ,  ,  ,  , True)%></TD>
                    <TD><LABEL ID=8826><%= GetLocalResourceObject("cboTypeAccountCaption") %></LABEL></TD>
                    <TD><%=mobjValues.PossiblesValues("cboTypeAccount", "Table400", 1,  ,  ,  ,  ,  ,  , "SetCurrency();", True,  , GetLocalResourceObject("cboTypeAccountToolTip"))%></TD>
                </TR>
                <TR>
                    <TD><LABEL ID=8827><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
                    <TD WIDTH = 40%><%=mobjValues.ClientControl("tctClient", "",  , GetLocalResourceObject("tctClientToolTip"), "SetCurrency();", True, "tctClieName", False)%></TD>
                    <TD><LABEL ID=8828><%= GetLocalResourceObject("cboCurrencyCaption") %></LABEL></TD>
                        
<%
mobjValues.Parameters.Add("nTyp_acco", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sType_acc", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nIntermed", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
%>
                <TD><%=mobjValues.PossiblesValues("cboCurrency", "TabCurr_Cli_Inter", 2, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboCurrencyToolTip"))%></TD>
                </TR>
            </TABLE>
            <%
mobjValues = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>
        </FORM>
    </BODY>
</HTML>






