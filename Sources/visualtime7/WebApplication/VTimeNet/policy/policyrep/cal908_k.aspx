<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.05
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% inspreCAL908: se definen los campos de la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreCAL908()
	'--------------------------------------------------------------------------------------------
	
Response.Write("	" & vbCrLf)
Response.Write("<BR><BR>" & vbCrLf)
Response.Write("	")

	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <BR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""15%""><LABEL ID=8735>" & GetLocalResourceObject("valClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ClientControl("valClient", "",  , GetLocalResourceObject("valClientToolTip"),  , False, "lblCliename", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""15%""> <LABEL ID=40281>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL> </TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkBatch", GetLocalResourceObject("chkBatchCaption")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	</TABLE>")

End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CAL908_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CAL908_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 20/07/04 13:57 $|$$Author: Nvaplat53 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL908_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, "CAL908", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAL908" ACTION="valPolicyRep.aspx?sMode=1">
<%Call insPreCAL908()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.05
Call mobjNetFrameWork.FinishPage("CAL908_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




