<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mobjError As eErrors.ErrorTyp


</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjError = New eErrors.ErrorTyp

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "er002_k"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

    <%=mobjValues.StyleSheet()%>
    <%=mobjMenu.MakeMenu("ER002", "ER002_K.aspx", 1, "")%>
 <SCRIPT>
     //------------------------------------------------------------------------------------------
     function insCancel() {
         //------------------------------------------------------------------------------------------
         return true;
     }
     //------------------------------------------------------------------------------------------
     function insFinish() {
         //------------------------------------------------------------------------------------------
         return true;
     }
     //------------------------------------------------------------------------------------------
     function insStateZone() {
         //------------------------------------------------------------------------------------------
         var lintIndex = 0;
         for (lintIndex = 0; lintIndex < document.forms[0].length; lintIndex++)
             document.forms[0].elements[lintIndex].disabled = false
         document.btntctCodisp.disabled = false
     }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmErroquer" ACTION="valerrors.aspx?sTime=1">
    <BR><BR>
    <TABLE WIDTH="100%"> 
       <TR>
            <TD WIDTH=25%><LABEL ID=6748>Transacción</LABEL></TD>
            <TD WIDTH=75% colspan="3">
<%If mobjValues.ActionQuery = True Then
	Response.Write(mobjValues.PossiblesValues("tctCodisp", "Windows", 2, mobjError.sCodisp,  ,  ,  ,  ,  ,  , True, 10,"Código de la transacción", eFunctions.Values.eTypeCode.eString,  ,  , True))
Else
	Response.Write(mobjValues.PossiblesValues("tctCodisp", "Windows", 2, Session("sCodispl_log"),  ,  ,  ,  ,  ,  , True, 10,"Código de la transacción", eFunctions.Values.eTypeCode.eString,  ,  , True))
End If
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=6750>Estado del error</LABEL></TD>
            <TD WIDTH=25%><%=mobjValues.PossiblesValues("cbeStaterr", "Table999", 1, CStr(mobjError.sStat_error_Initial),  ,  ,  ,  ,  ,  , True,  ,"Estado en que se encuentra el error")%></TD>
        
            <TD WIDTH=25%><LABEL ID=15985>Origen del error</LABEL></TD>
            <TD WIDTH=25%><%=mobjValues.PossiblesValues("cbeSrcerr", "Table531",  1, 1,  ,  ,  ,  ,  ,  , True,  ,"Versión de VisualTime asociada al error")%></TD>
        </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
mobjError = Nothing
%>










