<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQueryString As String

'- Objeto para el manejo de las rutinas genéricas
Dim mclsValPolicyTra As ePolicy.ValPolicyTra


</script>
<%response.Expires = 0

'- Variables que contendrán la información que está en las variables de Sesión
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsValPolicyTra = New ePolicy.ValPolicyTra
End With

With Request
	mstrQueryString = "nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif")
End With
%>

<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.setZone(2, "CAL848", "CAL848.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmNullPolicy" ACTION="valPolicyRep.aspx?<%=mstrQueryString%>">
<%
response.Write(mobjValues.ShowWindowsName("CAL848"))
%>
    <BR>
    <TABLE WIDTH="100%">
        <TR>
           <TD COLSPAN="1" width=100%>
           <CENTER><LABEL>
            <%If CStr(session("sFile_name")) <> "../../tFiles/True" Then
	response.Write(mobjValues.AnimatedButtonControl("btnFile", "../../images/dmerea.gif", GetLocalResourceObject("btnFileToolTip"),  , "ShowPopUp('" & session("sFile_name") & "','FileData',600,400,'yes','yes')") & GetLocalResourceObject("btnFileToolTip"))
Else%>
                    No se encontro información asociada
            <%End If%>
            </LABEL></CENTER>
            </TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mclsValPolicyTra = Nothing
mobjValues = Nothing
%>





