<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA600"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>        
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("MVA600", "MVA600_K.aspx", 1, vbNullString))
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        tcdEffecdate.disabled = false;
        btn_tcdEffecdate.disabled = false;
        cbeBranch.disabled = false;
        cbeIntermtyp.disabled = false;
    }
}

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

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        valModulec.Parameters.Param1.sValue = cbeBranch.value;
        valModulec.Parameters.Param2.sValue = valProduct.value;
        valModulec.Parameters.Param3.sValue = tcdEffecdate.value;
        valModulec.value = '';
        UpdateDiv('valModulecDesc','');
        valModulec.disabled = cbeBranch.value    == '0' ||
                              valProduct.value   == ''  ||
                              tcdEffecdate.value == '';
        btnvalModulec.disabled = valModulec.disabled;
        if (!valModulec.disabled)
            insDefValues('MVA600', 'nBranch=' + cbeBranch.value  + '&nProduct=' + valProduct.value + '&dEffecdate=' + tcdEffecdate.value, '/VTimeNet/Maintenance/MantLife');
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVA600" ACTION="valMantLife.aspx?sMode=2">
<BR>
<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "InsChangeField()", True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeIntermtypCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeIntermtyp", "tabInter_typ", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeIntermtypToolTip"))%> </TD>
        </TR>

        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "InsChangeField()", True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  ,  ,  ,  ,  ,  , "InsChangeField()")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valModulecToolTip")))
End With
%>
            </TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>




