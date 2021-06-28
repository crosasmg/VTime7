<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    Call mobjNetFrameWork.BeginPage("MSO8500_K")
'~End Header Block VisualTimer Utility

    mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "MSO8500_K"
'~End Body Block VisualTimer Utility

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
        cbecurrency.disabled=false;
        tcdEffecDate.disabled=false;
        btn_tcdEffecDate.disabled=false;
        cbeBranch.disabled=false;
        valProduct.disabled=false;
        btnvalProduct.disabled=false;
    }
}
//**% insChargeProduct: The fields product and ncurrency are enabled.
//% insChargeProduct: Se habilitan los campos producto.
//------------------------------------------------------------------------------------------
function insChargeProduct(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=0) {
		with(self.document.forms[0]){
			valProduct.disabled=false;
			btnvalProduct.disabled=false;
			valProduct.value="";
			UpdateDiv("valProductDesc", "")
			valProduct.Parameters.Param1.sValue=lobject.value;
			valProduct.Parameters.Param2.sValue=0;
		}
    }
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<HTML>
<HEAD>
    <SCRIPT>
	//+ Variable para el control de versiones
	        document.VssVersion="$$Revision: 8 $|$$Date: 01/01/09 10:21a $"
    </SCRIPT>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
    Response.Write(mobjValues.StyleSheet())
    mobjMenu = New eFunctions.Menues
    Response.Write(mobjMenu.MakeMenu("MSO8500", "MSO8500_k.aspx", 1, vbNullString))
    mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MSO8500" ACTION="valmantauto.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="8%">
            
            <LABEL ID=9216>Ramo</LABEL></TD>
            <TD WIDTH="12%"><%=mobjValues.PossiblesValues("cbeBranch", "tabbranchsoat", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  , "insChargeProduct(this)", True,  , "Código del ramo al que pertenece la tarifa")%></TD>
            <TD>&nbsp;</TD>
            
            <TD WIDTH="8%"><LABEL ID=9217>Producto</LABEL></TD>
            <% With mobjValues
	                .Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                .Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		       End With
		    %>
		    <TD WIDTH="12%"><%=mobjValues.PossiblesValues("valProduct", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , , , True, 4, "Código del producto al que corresponde la tarifa")%></TD>
            <TD WIDTH="0%">&nbsp;</TD>
        </TR>
        <TR>
            <TD WIDTH="8%"><LABEL ID=9218>Moneda</LABEL></TD>
            <TD WIDTH="12%"><%=mobjValues.PossiblesValues("cbecurrency", "TABLE11",eFunctions.Values.eValuesType.clngComboType, Session("ncurrency"),,,,,,, True,2,"Moneda a la que corresponde la tarifa")%></TD>
            <TD WIDTH="0%">&nbsp;</TD>
            <TD WIDTH="8%"><LABEL ID=9219>Fecha de efecto</LABEL></TD>
            <TD WIDTH="12%"><%=mobjValues.DateControl("tcdEffecDate", Session("dEffecDate"), True, "Fecha a partir de la cual se desea realizar la acción incluida",  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("MSO8500_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>
