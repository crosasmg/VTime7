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

mobjValues.sCodisplPage = "opc014_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("OPC014", "OPC014_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    with (document.forms[0]){
        if (top.fraSequence.plngMainAction == 401)
	        tcdEffecdate.disabled=false
	        cbeTypeAccount.disabled=false
	        cbeCurrency.disabled=false
	        btn_tcdEffecdate.disabled=false
	        dtcClient.disabled=false
	        btndtcClient.disabled=false
    }
}
//% insShowChangeCurrency: Se habilita/deshabilita el campo moneda
//-------------------------------------------------------------------------------------------
function insShowChangeCurrency(){
//-------------------------------------------------------------------------------------------
	with (document.forms[0]){
	if (dtcClient.value != '' &&
		cbeTypeAccount.value != 0)
        ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=BussiType" + "&nTypeAccount=" + cbeTypeAccount.value + "&sBussiType=" + cbeBussType.value+ "&sClient=" + dtcClient.value, "ShowDefValuesCurrency", 1, 1,"no","no",2000,2000);
	}	
}
//insChangLocked: Procedimiento que habilita o deshabilita el campo tipo de negocio
//según el tipo de cuenta seleccionada
//------------------------------------------------------------------------------------------
function  insChangLocked(Field){
//------------------------------------------------------------------------------------------
    with (document.forms[0]){
        if (Field.value==2 || Field.value==3 || Field.value==8)
            cbeBussType.disabled=false
        else{
		    cbeBussType.disabled=true
            cbeBussType.options[3].selected=true
        }
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQPayOrderMov" ACTION="valCashBank.aspx?mode=1">
<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8859><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, Today)),  , "",  ,  ,  ,  , True)%></TD>
			<TD></TD>
            <TD></TD>
        </TR>    
            <TD><LABEL ID=8856><%= GetLocalResourceObject("cbeTypeAccountCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTypeAccount", "Table400", 1,  ,  ,  ,  ,  ,  , "insChangLocked(this);insShowChangeCurrency()", True)%></TD>
			<TD><LABEL ID=8754><%= GetLocalResourceObject("cbeBussTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBussType", "Table20", 1,  ,  ,  ,  ,  ,  ,  , True,  , "")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8857><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
            <TD><%=mobjValues.ClientControl("dtcClient", vbNullString,  , "", "insShowChangeCurrency()", True, "lblClieName")%></TD>
            <TD><LABEL ID=8858><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>




