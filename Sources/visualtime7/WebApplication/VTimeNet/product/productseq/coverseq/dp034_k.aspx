<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para mostrar la descripción de la cobertura
Dim mclsGen_cover As eProduct.Gen_cover

'- Objeto para mostrar la descripción del módulo
Dim mclsTab_modul As eProduct.Tab_modul


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mclsGen_cover = New eProduct.Gen_cover

mobjValues.ActionQuery = True

With Request
	Session("nModulec") = .QueryString.Item("nModulec")
	Session("nCover") = .QueryString.Item("nCover")
	Session("nCovergen") = .QueryString.Item("nCovergen")
End With

mobjValues.sCodisplPage = "dp034_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
//- Variable para indicar cuando se está agregando la cobertura
	var mblnAutomatic = <%=Request.QueryString.Item("bAutomatic")%>
	
//% insStateZone: se controla el estado de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: se controla la acción Cancelar de la ventana
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	if(mblnAutomatic)
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=DP034_K","EndProcess",300,150)
	else
		top.close()
}
//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return(true);
}
</SCRIPT>
    <%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP034_K", "DP034_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP034_K" ACTION="valCoverSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <BR><BR>
    <TABLE WIDTH="100%">
<%If CStr(Session("nModulec")) > "0" Then
	mclsTab_modul = New eProduct.Tab_modul
	Call mclsTab_modul.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	%>
			<TR>
				<TD>&nbsp;</TD>
			    <TD COLSPAN="2"><LABEL ID=14931><%= GetLocalResourceObject("gmtModuleCaption") %></LABEL></TD>
			    <TD><%=mobjValues.TextControl("gmtModule", 30, mclsTab_modul.sDescript)%></TD>
			</TR>
<%End If%>
        <TR>
			<TD WIDTH=25%>&nbsp;</TD>
            <TD WIDTH=10%><LABEL ID=14932><%= GetLocalResourceObject("lblDescCoverCaption") %></LABEL></TD>
            <TD WIDTH=5%>&nbsp;</TD>
            <TD><%=mobjValues.TextControl("lblDescCover", 120, mclsGen_cover.getDescript(Session("sBrancht"), mobjValues.StringToType(Session("nCovergen"), eFunctions.Values.eTypeData.etdDouble)))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<% 
mclsGen_cover = Nothing
mobjValues = Nothing
mclsTab_modul = Nothing
Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='Sequence.aspx?sGoToNext=Yes'</SCRIPT>")
%>




