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
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MRO001_K")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MRO001_K"
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
        valProduct.disabled=false;
        btnvalProduct.disabled=false;
        cbeCover.disabled=false;
       // cbeCurrency.disabled=false;
        tcdEffecdate.disabled=false;
        btn_tcdEffecdate.disabled=false;
    }

}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
//% InsParamValue: Asigna Cobertura, Causa, Modulo
//---------------------------------------------------------------------------------------------------
function InsParamValue(){
//---------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeCover.Parameters.Param1.sValue = cbeBranch.value;
		cbeCover.Parameters.Param2.sValue = valProduct.value;
		cbeCover.Parameters.Param3.sValue = 0;
				
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

//-------------------------------------------------------------------------------------------------------------
function InsShowCurrency(){
//-------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
        insDefValues('ShowCurrency',"nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nCover=" + cbeCover.value + "&dEffecdate=" + tcdEffecdate.value,'/VTimeNet/maintenance/manttheft');    
    }    
} 
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
    Response.Write(mobjValues.StyleSheet())
    mobjMenu = New eFunctions.Menues
    Response.Write(mobjMenu.MakeMenu("MRO001", "MRO001_k.aspx", 1, vbNullString))
    mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MRO001" ACTION="valmanttheft.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8599><%= GetLocalResourceObject("cbeBranchCaption")%></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("cbeBranch", "TABLE10", eFunctions.Values.eValuesType.clngComboType, Session("nBranch"), , , , , , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value", True, 2, GetLocalResourceObject("cbeBranchToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=8600><%= GetLocalResourceObject("valProductCaption")%></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
                <%= mobjValues.PossiblesValues("valProduct", "tabProdMaster1", eFunctions.Values.eValuesType.clngWindowType, Session("nProduct"), True, , , , , "InsParamValue();InsValueInit();", True, 5, GetLocalResourceObject("valProductToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=8603><%= GetLocalResourceObject("tcdEffecdateCaption")%></LABEL></TD>
            <TD><%= mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"), True, GetLocalResourceObject("tcdEffecdateToolTip"), , , , , True)%></TD>
            <TD>&nbsp;</TD>
        
            <TD><LABEL ID=8601><%= GetLocalResourceObject("cbeCoverCaption")%></LABEL></TD>
            <%  
                With mobjValues
                    .Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
                %>
            
            <TD><%= mobjValues.PossiblesValues("cbeCover", "tab_cover", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , "InsShowCurrency();", True, , GetLocalResourceObject("cbeCoverToolTip"))%></TD>
            <TD>&nbsp;</TD>
                  
        </TR>
        <TR>
            <TD><LABEL ID=8602><%= GetLocalResourceObject("cbeCurrencyCaption")%></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("cbeCurrency", "TABLE11", eFunctions.Values.eValuesType.clngComboType, Session("nCurrency"), , , , , , , True, 2, GetLocalResourceObject("cbeCurrencyToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("MRO001_K")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








