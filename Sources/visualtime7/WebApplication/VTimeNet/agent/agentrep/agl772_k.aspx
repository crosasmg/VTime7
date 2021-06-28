<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl772_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.sCodisplPage = "agl772_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
        document.VssVersion="$$Revision: 1 $|$$Date: 4/09/03 12:22 $|$$Author: Nvaplat34 $"  

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
//-No se crea código de habilitación de campos ya que 
//-por tipo de ventana se genera error si se incluye
}
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
//%insChangeField: Control de cambio de parámetros
//--------------------------------------------------------------------------------------------
function insChangeField(oField){
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        switch(oField.name){
			case 'cbeIntertyp':
			    valIntermed.Parameters.Param1.sValue=oField.value;
			    valIntermed.value=''
			    UpdateDiv('valIntermedDesc', '');
			    break;
        }
    }
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AGL772_K.aspx", 1, ""))
	'+Se agrega zona para dejar habilitado el botón finalizar
	Response.Write(mobjMenu.setZone(1, "AGL772", ""))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="AGL772_K" ACTION="ValAgentRep.aspx?sMode=2">
    <TABLE WIDTH="100%" BORDER="0">
	    <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
	    </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeIntertypCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.PossiblesValues("cbeIntertyp", "tabinter_typ_annu", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  , "insChangeField(this)", False,  , GetLocalResourceObject("cbeIntertypToolTip"))%> </TD>
        </TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
            <TD COLSPAN="4">
                <%With mobjValues
	.Parameters.Add("nIntertyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10)
	Response.Write(.PossiblesValues("valIntermed", "tabintermed_typ_annu", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, False,  ,  ,  ,  , False,  , GetLocalResourceObject("valIntermedToolTip")))
End With%> 
            </TD>
		</TR>
		<TR>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateFromCaption") %></LABEL></TD>
	        <TD><%=mobjValues.DateControl("tcdDateFrom", vbNullString, True, GetLocalResourceObject("tcdDateFromToolTip"),  ,  ,  ,  , False)%> </TD>
	        <TD>&nbsp;</TD>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateToCaption") %></LABEL></TD>
	        <TD><%=mobjValues.DateControl("tcdDateTo", vbNullString, True, GetLocalResourceObject("tcdDateToToolTip"),  ,  ,  ,  , False)%> </TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl772_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




