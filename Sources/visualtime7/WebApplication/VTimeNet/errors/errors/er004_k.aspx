<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mobjError As eErrors.ErrorTyp


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjError = New eErrors.ErrorTyp

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut) Then
	mobjValues.ActionQuery = True
End If
Call mobjError.Find_UserName(Session("nusercode"))
Session("sInitials_User") = mobjError.sUser_Sesion

mobjValues.sCodisplPage = "er004_k"
%>
<HTML>
<HEAD>
<SCRIPT>
    //------------------------------------------------------------------------------------------
    function insStateZone() {
        //------------------------------------------------------------------------------------------
    }
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

    <%=mobjValues.StyleSheet()%>
    <%=mobjMenu.MakeMenu("ER004", "ER004_K.aspx", 1, "")%>
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
         with (self.document.forms[0]) {
             tcnErrorNum.disabled = false
         }

     }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmErrHistor" ACTION="valerrors.aspx?sTime=1">
    <BR><BR>
    <TABLE WIDTH="100%"> 
       <TR>
            <TD WIDTH=15%><LABEL ID=6771>Error</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnErrorNum", 5, Session("nErrorNum"),  ,"Número de error",  , 0,  ,  ,  ,  , True)%></TD>
	   </TR>
       <TR>
			<TD WIDTH=15%><LABEL ID=6772>Transacción</LABEL></TD>
			<TD><%=mobjValues.DIVControl("tctCodisp",  , "")%></TD>
            <!--TD WIDTH=25%><%=mobjValues.TextControl("tctCodisp", 10, mobjError.sCodisp,  ,  , True)%></TD-->
       </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>










