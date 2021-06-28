<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
'- Objeto para mostrar la descripción de la cobertura    
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "DP705_K"

mobjValues.ActionQuery = True

With Request
	Session("nModulec") = .QueryString.Item("nModulec")
	Session("nCover") = .QueryString.Item("nCover")
	Session("nRole") = .QueryString.Item("nRole")
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
//- Variable para indicar cuando se está agregando la cobertura
	var mblnAutomatic = <%=Request.QueryString.Item("bAutomatic")%>
	
//% insCancel: Se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	if(mblnAutomatic)
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=DP705","EndProcess",300,150)
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
<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP705_K", "DP705_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP705_K" ACTION="valRolesSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD>&nbsp;</TD>
	        <%If CStr(Session("nModulec")) > "0" Then%>
				  <TD WIDTH=12%><LABEL ID=14931><%= GetLocalResourceObject("cbeModuleCaption") %></LABEL></TD>
				  <TD><%	
	With mobjValues
		.Parameters.Add("nBranch", .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("cbeModule", "tabTab_modul", eFunctions.Values.eValuesType.clngComboType, Session("nModulec"), True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeModuleToolTip")))
	End With
	%></TD>
	        <%End If%>
            <TD WIDTH=12%><LABEL ID=14932><%= GetLocalResourceObject("cbeCoverCaption") %></LABEL></TD>
            <TD WIDTH=20%><%
With mobjValues
	.Parameters.Add("nBranch", .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nModulec", .StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngComboType, Session("nCover"), True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeCoverToolTip")))
End With
%></TD>
            <TD WIDTH=12%><LABEL ID=14932><%= GetLocalResourceObject("cbeRoleCaption") %></LABEL></TD>
            <TD WIDTH=20%><%=mobjValues.PossiblesValues("cbeRole", "table12", eFunctions.Values.eValuesType.clngComboType, Session("nRole"),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRoleToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='Sequence.aspx?sGoToNext=Yes'</SCRIPT>")
%>




