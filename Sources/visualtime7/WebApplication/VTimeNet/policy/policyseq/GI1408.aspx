﻿<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As Object
    '%insDefineHeader. Definición de columnas del GRID
    '------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lobjParam As eInterface.ValInterfaceSeq
	lobjParam = New eInterface.ValInterfaceSeq
        Response.Write(lobjParam.GetParamGI1408(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nsheet"), Session("dEffecdate"), Session("nTransaction")))
	
End Sub

</script>
<%Response.Expires = -1
    Dim sCodispl As String
    Session("sCodispl") = Request.QueryString.Item("sCodispl")
    sCodispl = Session("sCodispl")
    Session("nsheet") = sCodispl.Substring(3)
    mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = "GI1408" 'Session("sCodispl")
%>
<HTML>
<HEAD>
   <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	 <%=mobjValues.WindowsTitle(Session("sCodispl"))%>
	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
            .Write(mobjMenu.setZone(2, "GI1408", "GI1408.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 31/10/03 17:16 $"
 
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}

//-------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
	
}
</SCRIPT>		

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>
<FORM METHOD="POST" ID="FORM" NAME="GI1408" ACTION="valpolicyseq.aspx?nlevel=1">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName(Session("sCodispl")))
Call insDefineHeader()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>







