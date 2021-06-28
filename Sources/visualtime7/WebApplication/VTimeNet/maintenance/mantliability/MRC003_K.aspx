<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
    
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las zonas de la página    
    Dim mobjMenu As eFunctions.Menues

</script>
<%  Response.Expires = -1441

    mobjValues = New eFunctions.Values
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "MRC003_K"

%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->


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
        btnvalProduct.disabled=false;
        cbeCover.disabled=false;
        tcdEffecDate.disabled=false;
        btn_tcdEffecDate.disabled=false;
    }

}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
//% InsParamValue: Asigna Cobertura
//---------------------------------------------------------------------------------------------------
function InsParamValue(){
//---------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeCover.Parameters.Param1.sValue = cbeBranch.value;
		cbeCover.Parameters.Param2.sValue = valProduct.value;
				
		cbeCover.disabled = (valProduct.value=='')?true:false;			
		btncbeCover.disabled = (valProduct.value=='')?true:false;
		
	}
} 
//% InsValueInit: Limpia valiables de llave de acceso
//---------------------------------------------------------------------------------------------------
function InsValueInit(){
//---------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
				
		cbeCover.value = "";
		UpdateDiv('cbeCoverDesc',"")	
		
	}
} 
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<%
    Response.Write(mobjValues.StyleSheet())
    mobjMenu = New eFunctions.Menues
    Response.Write(mobjMenu.MakeMenu("MRC003", "MRC003_k.aspx", 1, vbNullString))
    mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MRC003" ACTION="valmantliability.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "TABLE10", eFunctions.Values.eValuesType.clngComboType, Session("nBranch"),  ,  ,  ,  ,  , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value", True, 2,GetLocalResourceObject("cbeBranchTooltip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
                <%=mobjValues.PossiblesValues("valProduct", "tabProdMaster1", eFunctions.Values.eValuesType.clngWindowType, Session("nProduct"), True,  ,  ,  ,  , "InsParamValue();InsValueInit();", True, 5, GetLocalResourceObject("valProductTooltip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeCoverCaption") %></LABEL></TD>
            
            <%  With mobjValues
                    .Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	
                End With%>
                                   
            <TD><%=mobjValues.PossiblesValues("cbeCover", "tab_cover", eFunctions.Values.eValuesType.clngWindowType, Session("nCover"), True,  ,  ,  ,  ,  , True,  ,GetLocalResourceObject("cbeCoverTooltip"))%></TD>
                                                
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecDate", Session("dEffecDate"), True, GetLocalResourceObject("tcdEffecDateTooltip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%  mobjValues = Nothing%>
