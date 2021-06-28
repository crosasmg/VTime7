<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MCP776"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>
//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0])
    {
        valLed_compan.disabled=false;
        btnvalLed_compan.disabled=false;
        tcnLed_year.disabled=false;
        cboLed_Month.disabled=false;
        optShowVoucher[0].disabled=false;
        optShowVoucher[1].disabled=false;
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true;
}

//% getCompleteYear: Esta rutina se encarga de devolver el año completo (4 digitos) cuando se introduce incompleto (2 dígitos).
//----------------------------------------------------------------------------------------------------------------------------
function getCompleteYear(lstrValue){
//------------------------------------------------------------------------------------------------------------------------------   

    var ldtmYear = new Date()
    var lintPos  
    var lstrYear
    var llngValue = 0
    do {
       lstrValue = lstrValue.replace(".","")
    }
    while (lstrValue != lstrValue.replace(".",""))
    if (lstrValue == '') llngValue = 0
    else llngValue = parseFloat(lstrValue)
    if (llngValue<1000)
    {
        if (llngValue<=50)
            llngValue += 2000
        else
            if (llngValue<100)
                llngValue += 1900
            else
                llngValue += 2000;
    }    
    return "" + llngValue;
 }   

// ShowYear: Muestra el año completo (4 digitos)
//-------------------------------------------------------------------------------------------
function ShowYear(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	    tcnLed_year.value = getCompleteYear(self.document.forms[0].tcnLed_year.value);
}

 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MCP776", "MCP776_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MCP776" ACTION="ValMantLedger.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID="0"><%= GetLocalResourceObject("valLed_companCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valLed_compan", "tabled_compan", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valLed_companToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>            
            <TD COLSPAN="2" CLASS=HighLighted><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR> 	    
	       <TD></TD>
	       <TD></TD>
	       <TD></TD>
	       <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>             
        <TR>        
            <TD>&nbsp;</TD>            
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>            
            <TD><%=mobjValues.OptionControl(0, "optShowVoucher", GetLocalResourceObject("optShowVoucher_CStr2Caption"),  , CStr(2),  , True)%></TD>
            <TD><%=mobjValues.OptionControl(0, "optShowVoucher", GetLocalResourceObject("optShowVoucher_CStr1Caption"), CStr(1), CStr(1),  , True)%></TD>            
        </TR>
        <TR>
            <TD>&nbsp;</TD>            
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>            
            <TD COLSPAN="2" CLASS=HighLighted><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
     <TR>
	    <TD></TD>
	    <TD></TD>
	    <TD></TD>     
	    <TD COLSPAN="2" CLASS="Horline"></TD>
     </TR>                     
        <TR>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID="0"><%= GetLocalResourceObject("tcnLed_yearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnLed_year", 4,  , True, GetLocalResourceObject("tcnLed_yearToolTip"),  ,  ,  ,  ,  , "ShowYear();", True)%></TD>
        </TR>
        <TR>            
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID="0"><%= GetLocalResourceObject("cboLed_MonthCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cboLed_Month", "Table7013", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboLed_MonthToolTip"))%></TD>            
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>






