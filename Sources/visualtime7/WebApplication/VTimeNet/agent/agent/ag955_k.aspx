<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = "AG955"
%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
    self.document.forms[0].nContrat_Pay.disabled = false;
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
// Inschangevalues:
//-----------------------------------------------------------------------------------
function Inschangevalues(Field) {
    //-----------------------------------------------------------------------------------
    if (Field.name == 'cbeBranch')
        self.document.forms[0].nContrat_Pay.Parameters.Param1.sValue = Field.value;


    if (Field.name == 'valProduct')
        self.document.forms[0].nContrat_Pay.Parameters.Param2.sValue = Field.value;
}
</SCRIPT>
<%
    With Response
        .Write(mobjValues.StyleSheet())
        mobjMenu = New eFunctions.Menues
        .Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AG955_k.aspx", 1, ""))
        mobjMenu = Nothing
    End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmAG955_k" ACTION="ValAgent.aspx?mode=1">
<BR></BR>
<TABLE WIDTH="100%">
    </TR>
        <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
		<TD><%= mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), , "valProduct", , , , "Inschangevalues(this)")%></TD>

        <TD><LABEL ID=LABEL2><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
		<TD><%= mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), , , , , , , , "Inschangevalues(this)")%></TD>
        
        <TD WIDTH=120pcx><LABEL ID=0><%= GetLocalResourceObject("nContrat_PayCaption") %></LABEL></TD>
        <TD><%
                mobjValues.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Response.Write(mobjValues.PossiblesValues("nContrat_Pay", "TABCONTRAT_PAY_PROD", eFunctions.Values.eValuesType.clngWindowType,, True, , , , , , , , GetLocalResourceObject("nContrat_PayToolTip")))%></TD>
        
    </TR> <!--8===D-->
</TABLE>
</FORM>
<%
mobjValues = Nothing%>
</BODY>
</HTML>




