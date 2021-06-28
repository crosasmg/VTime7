<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co501_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co501_k"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>



<SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 4/08/04 15:59 $|$$Author: Nvaplat40 $"

//% insStateZone: Habilita o deshabilita los campos del header.
//------------------------------------------------------------------------------
function insStateZone() {
//------------------------------------------------------------------------------
}

//% insChangeWay_pay: Si se cambia la via de pago
//------------------------------------------------------------------------------
function insChangeWay_pay(Field){
	with(self.document.forms[0]){	
		cbeBank.value='';
		UpdateDiv('cbeBankDesc', '');
		UpdateDiv('valAgreementDesc', '');
		
		valAgreement.value='';
		UpdateDiv('valAgreementDesc', '');
		
		
		
//+ Si la vía de pago es PAC se habilita el banco
        if (Field.value==1){
			cbeBank.disabled = false;
			btncbeBank.disabled = false; }
		else
		   {
			cbeBank.disabled = true;
			btncbeBank.disabled = true;
     		}
 //+ Si la vía de pago es descuento por planilla e habilita el convenio
        if (Field.value==3){
			valAgreement.disabled = false;
			btnvalAgreement.disabled = false; }
		else
		   {
			valAgreement.disabled = true;
			btnvalAgreement.disabled = true;
     		}    		
     		
	}	
}

//% insCancel: Controla la acción cancelar de la página
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
	return (true);
}
</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CO501", "CO501_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="CO501" NAME="frmPayReject" ACTION="ValCollectionTra.aspx?mode=1">
	<BR><BR>
	<%=mobjValues.ShowWindowsName("CO501")%>
	<BR>
    <TABLE WIDTH="100%">         
		<TR>
            <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("tcdExpiriDateCaption") %></LABEL></TD>		
            <TD WIDTH="15%"><%=mobjValues.DateControl("tcdExpiriDate",  ,  , GetLocalResourceObject("tcdExpiriDateToolTip"))%></TD>
            <TD WIDTH="13%"><LABEL ID=0><%= GetLocalResourceObject("cbeWay_payCaption") %></LABEL></TD> 
                <%mobjValues.TypeList = 1
mobjValues.List = "1,2,3"%>           
            <TD WIDTH="10%"><%=mobjValues.PossiblesValues("cbeWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeWay_pay(this)",  ,  , GetLocalResourceObject("cbeWay_payToolTip"))%></TD>
       	</TR>
       	<TR>
       	    <TD WIDTH="8%"><LABEL ID=9906 ><%= GetLocalResourceObject("cbeBankCaption") %></LABEL></TD> 
                <%mobjValues.Parameters.Add("sType_Bankagree", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
            <TD WIDTH="34%"><%=mobjValues.PossiblesValues("cbeBank", "tabBank_Agree_Banks", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBankToolTip"))%></TD>
		<TD><LABEL ID=12973><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD> 
            <TD><%=mobjValues.PossiblesValues("valAgreement", "tabAgreement", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAgreementToolTip"))%></TD> 
		
		</TR>
	</TABLE>         		
	<BR>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co501_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




