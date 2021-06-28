<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>

//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 10/11/03 17:22 $|$$Author: Nvaplat11 $"
</SCRIPT>
<HTML>
<HEAD>
<SCRIPT LANGUAGE=JavaScript>
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
// insChangeoptOper: Actualiza los objetos de la forma, según el tipo de Operacion Reimpresión 
//                   o Anulación.
//--------------------------------------------------------------------------------------------
function insChangeoptOper(lobjOper) {
	with (self.document.forms[0]){
		switch (lobjOper.value) {
//          Vía de pago es Planilla 
			case '1':
				cbeCancel_Cod.value = '';
				cbeCancel_Cod.disabled = true;
				break;
			case '2':
				cbeCancel_Cod.disabled = false;
				break;
        } 
    } 
} 
// insChangeWayPay: Actualiza los objetos de la forma, según el tipo de via de pago.
//-------------------------------------------------------------------------------------------
function insChangeWayPay(lobjWayPay) {
//-------------------------------------------------------------------------------------------	
// Cuando la vía de pago se trata de descuento por planilla; se habilita el campo convenio.
// Si se inició vía de pago de habilita campo Fecha de cobranza.
// Si se indica vía de pago se desabilita boletín inicial y final.
//
// Variables                           
// optOper       : OPeración 
// cbeWay_pay    : via de pago 
// valAgreement  : convenio 
// tcdCollDate   : fecha cobranza 
// tcnBullStart  : boletin inicial 
// tcnBullEnd    : boletin final 
// cbeCancel_Cod : Causa anulacion, table5005 

	with (self.document.forms[0]){
		switch (lobjWayPay.value) {
//          Vía de pago es Planilla 
			case '3':
				valAgreement.disabled = false;
				btnvalAgreement.disabled = false;
				tcdCollDate.disabled = false;
				btn_tcdCollDate.disabled = false;
				break;
			case '0':
			    valAgreement.value = '';
				valAgreement.disabled = true;
				btnvalAgreement.disabled = true;
         		UpdateDiv('valAgreementDesc','');
				tcdCollDate.value = '';
				tcdCollDate.disabled = true;
				btn_tcdCollDate.disabled = true;
				break;
			default: 
                valAgreement.value = '';
                valAgreement.disabled = true;
                btnvalAgreement.disabled = true;
         		UpdateDiv('valAgreementDesc','');
				tcdCollDate.disabled = false; 
				btn_tcdCollDate.disabled = false; 
				break;
		} 
    } 
} 
</SCRIPT>

	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("COL594", "COL594_K.aspx", 1, vbNullString))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COL594" ACTION="valCollectionRep.aspx?sMode=1">
<BR></BR>   
    <%=mobjValues.ShowWindowsName("COL594")%>
    
  
      <TABLE WIDTH="100%">
        <TR>
			<TD COLUMN=4 >&nbsp;</TD>
        </TR>        
        <TR> 
          <TD COLUMN=4 >&nbsp;</TD>
          <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL>
          <HR>  
          </TD>            
        </TR>             
        <TR>
		 <TD>&nbsp;</TD>			
         <TD WIDTH="30%">
         <%=mobjValues.OptionControl(0, "optOper", GetLocalResourceObject("optOper_1Caption"), CStr(1), "1", "insChangeoptOper(this);")%></TD>
		 <TD><%=mobjValues.OptionControl(0, "optOper", GetLocalResourceObject("optOper_2Caption"),  , "2", "insChangeoptOper(this);")%></TD>
         <TD>&nbsp;</TD>
        </TR>
               
         <TR>
			<TD COLUMN=4 >&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL>
            <HR>  
            </TD>  			
        </TR> 
        
        <TR> 
			<TD>&nbsp;</TD> 
            <TD><LABEL ID=9906><%= GetLocalResourceObject("cbeWay_payCaption") %></LABEL></TD> 
            <TD><%

mobjValues.TypeList = 2
mobjValues.List = "7,5,6"

Response.Write(mobjValues.PossiblesValues("cbeWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeWayPay(this);",  ,  , GetLocalResourceObject("cbeWay_payToolTip")))%></TD> 
            <TD>&nbsp;</TD> 
        </TR>        
                
		<TR>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=12973><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valAgreement", "tabAgreement", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAgreementToolTip"))%></TD> 
            <TD>&nbsp;</TD>
        </TR>
        
        <TR>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdCollDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdCollDate",  ,  , GetLocalResourceObject("tcdCollDateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        
        <TR>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnBullStartCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnBullStart", 10, vbNullString,  , GetLocalResourceObject("tcnBullStartToolTip"))%></TD>
            <TD>&nbsp;</TD>
        </TR>
        
        <TR>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnBullEndCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnBullEnd", 10, vbNullString,  , GetLocalResourceObject("tcnBullEndToolTip"))%></TD>
            <TD>&nbsp;</TD>
        </TR>
        
        <TR>
			<TD>&nbsp;</TD> 
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCancel_CodCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCancel_Cod", "table5005", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCancel_CodToolTip"))%></TD>
            <TD>&nbsp;</TD>
        </TR>
        
    </TABLE>
    
</FORM> 
</BODY>
</HTML>
<%mobjValues = Nothing%>





