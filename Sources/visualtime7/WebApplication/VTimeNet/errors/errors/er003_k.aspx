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
If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
	Session("nErrorNum") = vbNullString
End If
mobjError.tDs_text = vbNullString
mobjError.tAuxDs_text = vbNullString
Call mobjError.Find_UserName(Session("nUsercode"))
Session("sInitials_User") = mobjError.sUser_Sesion

mobjValues.sCodisplPage = "er003_k"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">

<%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("ER003", "ER003_K.aspx", 1, vbNullString))
If Request.QueryString.Item("nErrorNum") <> vbNullString Then
	Session("nErrorNum") = Request.QueryString.Item("nErrorNum")
End If
%>
<SCRIPT>
    //% insStateZone: se controla el estado de los campos de la transacción
    //------------------------------------------------------------------------------------------
    function insStateZone() {
        //------------------------------------------------------------------------------------------
    }
    //% insCancel: se controla la acción Cancelar de la transacción 
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }
    //% insFinish: se controla la acción Finalizar de la transacción 
    //------------------------------------------------------------------------------------------
    function insFinish() {
        //------------------------------------------------------------------------------------------
        return true;
    }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmUpdStaErr" ACTION="valerrors.aspx?sTime=1">
    <BR><BR>
    <TABLE WIDTH="100%"> 
       <TR>
            <TD WIDTH=15%><LABEL ID=6761>Error</LABEL></TD>
            <TD WIDTH=15%><%=mobjValues.NumericControl("tcnErrorNum", 7, Session("nErrorNum"),  ,"Código del error que se actualizará",  , 0)%></TD>
			<TD WIDTH=20%><LABEL ID=6762>Estado actual</LABEL></TD>
            <TD><%=mobjValues.DIVControl("cbeStaterr",  , vbNullString)%></TD>
	   </TR>
       <TR>
			<TD WIDTH=15%><LABEL ID=6763>Transacción</LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.DIVControl("tctCodisp",  , vbNullString)%></TD>
       </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
If Request.QueryString.Item("sLinkSpecial") <> vbNullString Then
	If Session("Query") Then
		Response.Write("<SCRIPT>top.document.frames['fraFolder'].location='ER003.aspx?nMainAction=302&sCodispl=ER003&nError=" & Request.QueryString.Item("nErrorNum") & "';</SCRIPT>")
	End If
Else
	Session("Query") = False
	Session("sCallForm") = vbNullString
End If
%>










