<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("LT009", "LT009_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>

<SCRIPT> 
//----------------------------------------------------------------------------------------------------------------------
function insStateZone(){
//----------------------------------------------------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------

}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//-----------------------------------------------------------------------------
function insLoadProduct(){
//-----------------------------------------------------------------------------
	if (document.forms[0].cbeBranch.value != "")
		{
		document.forms[0].valProduct.disabled = false;
		document.forms[0].valProduct.Parameters.Param1.sValue = document.forms[0].cbeBranch.value;
		}
	else
		document.forms[0].valProduct.disabled = true;
}

</SCRIPT>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="LT009" ACTION="valLetter.aspx?x=1">
<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="15%"><LABEL ID=7276>Ramo</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insLoadProduct()",  ,  ,"nBranch")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=7277>Producto</LABEL></TD>
            <%mobjValues.Parameters.Add("nBranch", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
            <TD><%=mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , 4,"Producto al cual pertenece la póliza")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=7278>Fecha</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDate",  , True,"Fecha de la operación",  ,  ,  ,  , False)%></TD>
        </TR>
	</TABLE>
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>








