<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objetos generales para el manejo de la página
    Dim mobjValues As eFunctions.Values
    Dim mobjSecurity As eSecurity.Menu

    Dim mstrModule As String

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("menuname") 
    
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
    Response.Write("    var mintWidthLogOut = '")
    Response.Write(GetLocalResourceObject("WidthLogOut"))
    Response.Write("';" & vbCrLf)
    
    Response.Write("    var mintHeightLogOut = '")
    Response.Write(GetLocalResourceObject("HeightLogOut"))
    Response.Write("';" & vbCrLf)
    
    Response.Write("    var mintWidthGoTo = '")
    Response.Write(GetLocalResourceObject("WidthGoTo"))
    Response.Write("';" & vbCrLf)
    
    Response.Write("    var mintHeightGoTo = '")
    Response.Write(GetLocalResourceObject("HeightGoTo"))
    Response.Write("';" & vbCrLf)
    

Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>")
    
    If CStr(Session("SessionID")) = "" Then
        Response.Write("<SCRIPT>top.frames['treeFrame'].document.location.href='/VTimeNet/visualtime/Login.aspx';</SCRIPT>")
    End If

    mstrModule = Request.QueryString.Item("sModule")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.09.35
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "menuname"
    mobjSecurity = New eSecurity.Menu

%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/VTimeNet.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 14/03/06 20:08 $|$$Author: Mvazquez $|"

//% initializeTree: se carga el menú asociado al módulo
//-------------------------------------------------------------------------------------------
function initializeTree(){
//-------------------------------------------------------------------------------------------
    generateTree();
    redrawTree();
}
//% insShowGoTo: invoca la ventana "Ir a..."
//-------------------------------------------------------------------------------------------
function insShowGoTo(){
//-------------------------------------------------------------------------------------------
    ShowPopUp("/VTimeNet/Common/GoTo.aspx?sPopUp=1", "GoTo", mintWidthGoTo, mintHeightGoTo, "no", "no", 100, 100); 
    } 
    //% insShowGoTo: invoca la ventana "Ir a proceso batch"
//-------------------------------------------------------------------------------------------
function insShowBatchProcess() {
   //-------------------------------------------------------------------------------------------
    ShowPopUp("/VTimeNet/Common/GoTo.aspx?sPopUp=2&ActionByToolbar=295&sCodispl=BTC001", "BTC001_TB", 0, 0, "no", "no", 0, 0, "no", "no", true);
}
//% Logout: invoca la ventana para salir del sistema
//-------------------------------------------------------------------------------------------
function Logout(){
//-------------------------------------------------------------------------------------------
    ShowPopUp("/VTimeNet/Common/LogOut.aspx", "LogOut", mintWidthLogOut, mintHeightLogOut);
}
//% insGeneralQue: invoca la Consulta general
//-------------------------------------------------------------------------------------------
function insGeneralQue(){
//-------------------------------------------------------------------------------------------
    top.location.href= "/VTimeNet/Common/GoTo.aspx?sPopUp=2&sCodispl=GE099";
}
//% insQuoteInfo: invoca ventana con Información de la versión del Cotizador
//-------------------------------------------------------------------------------------------
function insInfoQuote(){
//-------------------------------------------------------------------------------------------
    ShowPopUp("/VTimeNet/Common/aboutQuote.aspx?sPopUp=1", "InfoQuote", 400, 250, "no", "no", 300, 250); 
}
</SCRIPT>
</HEAD>
<BODY STYLE="margin-bottom:0">
<%
If CStr(Session("sSche_code")) <> vbNullString Then
	mobjSecurity.sSche_code = Session("sSche_code")
	Response.Write(mobjSecurity.insLoadMenu(mstrModule))
	If CStr(Session("sChangeLogin_sCodispl")) <> vbNullString Then
		Response.Write("<SCRIPT>")
		Response.Write("top.frames['treeFrame'].insGoToCodispl(""" & Session("sChangeLogin_sCodispl") & """,'" & Session("sChangeLogin_Parameters") & "');")
		Response.Write("</SCRIPT>")
		Session("sChangeLogin_sCodispl") = vbNullString
	End If
End If

If CStr(Session("sLinkBatch")) = "1" Then
	Response.Write("<SCRIPT>ShowPopUp('/VTimeNet/Batch/BatchTra/btc003_K.aspx','',280,70);</SCRIPT>")
End If

mobjSecurity = Nothing
mobjValues = Nothing

%>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("menuname")
mobjNetFrameWork = Nothing
%>




