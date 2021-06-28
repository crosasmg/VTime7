<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

'Response.Write "<NOTSCRIPT>alert('Header" & Request.QueryString & "')</script>"

If Request.QueryString.Item("nErrorNum") <> vbNullString Then
	Session("nErrorNum") = Request.QueryString.Item("nErrorNum")
End If

mobjValues.sCodisplPage = "er001_k"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    

    <%=mobjValues.StyleSheet()%>
    <%=mobjMenu.MakeMenu("ER001", "ER001_K.aspx", 1, "")%>
    
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
            if (top.frames['frasequence'].plngMainAction != 301)
                tcnErrorNum.disabled = false;
            else
                tcnErrorNum.disabled = true;
            tcnErrorNum.value = '';
        }
    }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmErroUpd" ACTION="valerrors.aspx?sTime=1">
    <BR><BR>
    <TABLE WIDTH="100%">
            <TD WIDTH=120pcx><LABEL ID=6743>Número de error</LABEL></TD>
            <TD><%=
            mobjValues.NumericControl("tcnErrorNum", 6, Session("nErrorNum"),  ,"Número de error a asignar",  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing

If Not IsNothing(Request.QueryString.Item("sLinkSpecial")) Then
	If Session("Query") Then
		Response.Write("<SCRIPT>top.document.frames['fraFolder'].location='ER001.aspx?nMainAction=302&sCodispl=ER001';</SCRIPT>")
	End If
Else
	Session("Query") = False
	Session("sCallForm") = vbNullString
End If
%>










