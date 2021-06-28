<%@ Page Language="VB" Inherits="MU700_Massive_Fidelity_aspx" CodeFile="~/VTimeNet/policy/policyseq/MU700.aspx_files/MU700_Massive_Fidelity.aspx.vb"
    EnableViewState="false" %>

<%@ Import Namespace="System.IO" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="eFunctions.Values" %>
<%@ Import Namespace="eRemoteDB.Parameter" %>
<%@ Import Namespace="eProduct" %>
<%@ Import Namespace="ePolicy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%
    Response.Write(mobjValues.StyleSheet())
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
</head>
<body>
    <form id="form1" method="POST"  action="MU700_Massive_Fidelity.aspx?MassiveFidelity=1" enctype="multipart/form-data">
    <div>
    <table width="100%">
        <tr>
            <td colspan="2" class="HighLighted">
                <label> Carga masiva fidelidad privada </label>
            </td>
        </tr>    
        <tr>
            <td colspan="2"  class="Horline">
            </td>
        </tr>
        <tr>
        <td>
            <label> Archivo </label>
        </td>
        <td>
       <% 
            Response.Write(mobjValues.FileControl("tctFile", 40, , False )) 
       %>
       </td>
       </tr>
       <tr>
       <td colspan="2" style="text-align:right;">
        <%
            '+ Incluye el botón de aceptar y cancelar.
                Response.Write(mobjValues.ButtonAcceptCancel(  ,   , True, , Values.eButtonsToShow.All))
                mobjValues = Nothing
        %>
       </td>

       </tr>
    </table>
    </div>

    </form>
</body>
</html>
