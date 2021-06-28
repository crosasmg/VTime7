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
mobjValues.sCodisplPage = "MVA619"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("MVA619", "MVA619_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        cbeBranch.disabled = false;
        valProduct.disabled = false;
        btnvalProduct.disabled = valProduct.disabled;
        cbeTypeInvest.disabled = false;
        valModulec.disabled = false;
        btnvalModulec.disabled = valModulec.disabled;
    }
}

//% InsChangeField: Se setean los parámetros del campo módulo
//--------------------------------------------------------------------------------------------
function InsChangeField(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        valModulec.Parameters.Param1.sValue = cbeBranch.value;
        valModulec.Parameters.Param2.sValue = valProduct.value;
        valModulec.value = '';
        UpdateDiv('valModulecDesc','');
        valModulec.disabled = cbeBranch.value    == '0' ||
                              valProduct.value   == ''  ||
                              hddDate.value == '';
        btnvalModulec.disabled = valModulec.disabled;
        if (!valModulec.disabled)
            insDefValues('MVA600', 'nBranch=' + cbeBranch.value  + '&nProduct=' + valProduct.value + '&dEffecdate=' + hddDate.value, '/VTimeNet/Maintenance/MantLife');
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
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVA619_K" ACTION="valMantLife.aspx?sMode=2">
    <BR><BR>
    <TABLE WIDTH="100%">
    <TR>
        <TD><LABEL ID=13791><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
        <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "InsChangeField()", True)%></TD>
        <TD><LABEL ID=13804><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
        <TD>
        <%
Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  ,  ,  ,  ,  ,  , "InsChangeField()"))
Response.Write(mobjValues.HiddenControl("hddDate", CStr(Today)))
%>
        </TD>
    </TR>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
        <TD>
        <%
With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valModulec", "tabtab_modul1", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valModulecToolTip")))
End With
%> 
        </TD>
        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeTypeInvestCaption") %></LABEL></TD>
        <TD><%=mobjValues.PossiblesValues("cbeTypeInvest", "Table5520", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypeInvestToolTip"))%></TD>
    </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>




