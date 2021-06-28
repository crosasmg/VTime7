<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim sCodispl As String
Dim sCodisplPage As String
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%
sCodispl = Trim(Request.QueryString.Item("sCodispl"))
sCodisplPage = LCase(sCodispl) & "_k"

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(sCodisplPage)

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = sCodisplPage
'~End Body Block VisualTimer Utility

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

//%insDefValue:Permite asignarle "0,00" al control en caso de no haber indicado
//%valor numerico al campo
//------------------------------------------------------------------------------------------
function insDefValue(Field){
//------------------------------------------------------------------------------------------
    if(Field.value=='')
        self.document.forms[0].tcnExcess.value='0'
}

</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="Policy" action="valPolicyRep.aspx?mode=1">
    <BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))%>
    <BR><BR>
	<table width="40%">
       <tr>
            <td><LABEL ID="LABEL1"><%= GetLocalResourceObject("tcdEndDateCaption") %> </LABEL></td>
<td><% %>
<%=mobjValues.DateControl("tcdEndDate", CStr(Today),  , GetLocalResourceObject("tcdEndDateToolTip"))%></td>
        </tr>
	</table>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




