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

<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<html>
<head>
    <meta NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("LT010", "LT010_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</head>

<script> 
//----------------------------------------------------------------------------------------------------------------------
function insLoadProduct()
{
		if document.forms[0].cbeBranch.value != ""
		{
			document.forms[0].valProduct.disabled = false
			document.forms[0].valProduct.Parameters.Param1.sValue = document.forms[0].cbeBranch.value
		else
			document.forms[0].valProduct.disabled = true
		}

}
//----------------------------------------------------------------------------------------------------------------------
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
</script>

<body ONUNLOAD="closeWindows();">
<form METHOD="post" ID="FORM" NAME="LT010_K" ACTION="LT010.aspx">
     <table> 
           <tr> 
              <td><LABEL ID=7279>RAMO</LABEL></td>
              <td><%=mobjValues.PossiblesValues("cbeBranch", "table10", 1,  , False,  ,  ,  ,  , "insLoadProduct()",  ,  ,"xdddd")%></td>
           </tr> 
           <tr> 
              <td><LABEL ID=7280>PRODUCTO</LABEL></td>
              <td><%
'UPGRADE_WARNING: Use of Null/IsNull() detected. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
mobjValues.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valProduct", "tabprodmaster1", 2,  , True,  ,  ,  ,  ,  ,  ,  ,"Producto"))
%>
			  </td>
           </tr> 
           <tr> 
              <td><LABEL ID=7281>FECHA</LABEL></td>
              <td><%=mobjValues.DateControl("tcdEffecdate",,,vbNullString)%></td>
           </tr> 
           
     </table> 
<p>&nbsp;</p>
mobjValues = Nothing%>
</form>
</body>
</html>








