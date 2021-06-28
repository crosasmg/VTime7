<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co003_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co003_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>




<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<SCRIPT>

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
	return true
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//% Bloqueo y desbloqueo de campos
//---------------------------------------------------
function insUpdateAction(field){
//---------------------------------------------------
    switch (field.value){
        case '1':
            self.document.forms[0].tcnReceipt.disabled = false;
            self.document.forms[0].tcdEffecdate.disabled = false;
            self.document.forms[0].btn_tcdEffecdate.disabled = false;
            self.document.forms[0].tcnRate.disabled = false;
            self.document.forms[0].valPay_form.disabled = true;
            self.document.forms[0].btnvalPay_form.disabled = true;
            break;
  
        case '2':
            self.document.forms[0].tcnReceipt.disabled = false;
            self.document.forms[0].tcdEffecdate.disabled = false;
            self.document.forms[0].btn_tcdEffecdate.disabled = false;
            self.document.forms[0].tcnRate.disabled = true;
            self.document.forms[0].valPay_form.disabled = false;
            self.document.forms[0].btnvalPay_form.disabled = false;
            break;
  
        case '3':
            self.document.forms[0].tcnReceipt.disabled = false;
            self.document.forms[0].tcdEffecdate.disabled = false;
            self.document.forms[0].btn_tcdEffecdate.disabled = false;            
            self.document.forms[0].tcnRate.disabled = true;
            self.document.forms[0].valPay_form.disabled = true;
            self.document.forms[0].btnvalPay_form.disabled = true;
            break;
  
        case '4':
            self.document.forms[0].tcnReceipt.disabled = false;
            self.document.forms[0].tcdEffecdate.disabled = false;
            self.document.forms[0].btn_tcdEffecdate.disabled = false;            
            self.document.forms[0].tcnRate.disabled = true;
            self.document.forms[0].valPay_form.disabled = true;
            self.document.forms[0].btnvalPay_form.disabled = true;
            break;
  
        case '5':
            self.document.forms[0].tcnReceipt.disabled = false;
            self.document.forms[0].tcdEffecdate.disabled = false;
            self.document.forms[0].btn_tcdEffecdate.disabled = false;
            self.document.forms[0].tcnRate.disabled = true;
            self.document.forms[0].valPay_form.disabled = true;
            self.document.forms[0].btnvalPay_form.disabled = true;
            break;
  
        case '6':
            self.document.forms[0].tcnReceipt.disabled = false;
            self.document.forms[0].tcdEffecdate.disabled = false;
            self.document.forms[0].btn_tcdEffecdate.disabled = false;
            self.document.forms[0].tcnRate.disabled = false;
            self.document.forms[0].valPay_form.disabled = true;
            self.document.forms[0].btnvalPay_form.disabled = true;
            break;
  
        case '7':
            self.document.forms[0].tcnReceipt.disabled = false;
            self.document.forms[0].tcdEffecdate.disabled = false;
            self.document.forms[0].btn_tcdEffecdate.disabled = false;
            self.document.forms[0].tcnRate.disabled = true;
            self.document.forms[0].valPay_form.disabled = true;
            self.document.forms[0].btnvalPay_form.disabled = true;
            break;

    }
}
//%	ShowDefValues: Condiciona el recargo por el cambio en el patrón de búsqueda
//-------------------------------------------------------------------------------------------
function ShowDefValues(Field){
//-------------------------------------------------------------------------------------------

    with (document.forms[0]){
        if (Field.value != 0 )insDefValues("ShowDataCO003","sField=getBalance" + "&nReceipt=" + Field.value,"/VTimeNet/Collection/CollectionTra");
        
	}
}
</SCRIPT>

    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCO003_k" ACTION="valCollectionTra.aspx?x=1">
    <BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("CO003", Request.QueryString.Item("sWindowDescript")))%>
    <BR><BR>
      <TABLE WIDTH="100%">    
       <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valActionCaption") %></LABEL></TD>
            <TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;
            <%=mobjValues.PossiblesValues("valAction", "TABLE636", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "insUpdateAction(this)",  , 1, "", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnReceiptCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnReceipt", 6, "", True, GetLocalResourceObject("tcnReceiptToolTip"),  ,  ,  ,  ,  , "ShowDefValues(this)", True)%></TD>
            <TD WIDTH=105><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
      </TABLE>   
      <TABLE WIDTH="100%">  
        <TR>
            <TD WIDTH=180><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL>&nbsp;&nbsp;<%=mobjValues.NumericControl("tcnRate", 4, "", True, "Interés a Aplicar",  , True,  ,  ,  ,  , True)%>&nbsp;&nbsp;</TD>
            <TD WIDTH=100><LABEL ID=0><%= GetLocalResourceObject("valPay_formCaption") %></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD WIDTH=280><%=mobjValues.PossiblesValues("valPay_form", "TABLE182", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True, 2, GetLocalResourceObject("valPay_formToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPremiumCaption") %></LABEL></TD>
            <TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;<%=mobjValues.NumericControl("tcnPremium", 18, Request.QueryString.Item("nPremium"), True, GetLocalResourceObject("tcnPremiumToolTip"), True, 6,  ,  ,  ,  , True)%></TD>                      
            <TD>&nbsp;</TD>
        </TR>        
      </TABLE>
      
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co003_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




