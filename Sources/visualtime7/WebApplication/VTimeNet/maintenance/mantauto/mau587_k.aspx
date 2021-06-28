<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

Dim mstrMarca As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAU587"
%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
   with (document.forms[0]) {
        cbeBranch.disabled=false;
        valProduct.disabled=false;
        tcdEffecdate.disabled=false;
        valCurrency.disabled=false;
		btn_tcdEffecdate.disabled=false;        
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//% insChangeField: Se recargan los valores cuando cambia el campo
//----------------------------------------------------------------
function insChangeField(Field){
//----------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
            case "valVehcode":
                self.document.forms[0].cbeVehbrand.value = valVehcode_nVehbrand.value
                self.document.forms[0].tctVehmodel.value = valVehcode_sVehmodel.value
                break;
		}
	}
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content= "Microsoft Visual Studio 6.0">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MAU587", "MAU587_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MAU587" ACTION="valMantAuto.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>        
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , True)%> </TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True)%></TD>
        </TR>
        <TR>        
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valCurrencyCaption") %></LABEL></TD>            
            <TD><%=mobjValues.PossiblesValues("valCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, Session("nCurrency"),  ,  ,  ,  ,  ,  , True, 2, GetLocalResourceObject("valCurrencyToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML><%
mobjValues = Nothing
%>





