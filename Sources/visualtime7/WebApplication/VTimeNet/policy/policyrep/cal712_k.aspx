<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<SCRIPT LANGUAGE="JavaScript">
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Finalizar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insChangeField: Cambia parametros en campos
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (objField.name){
		  case 'cbeInsurArea':
				valCoverGen.Parameters.Param1.sValue=objField.value;
				break;
		  case 'valCoverGen':
				UpdateDiv('divCondSVS', valCoverGen_sCondSVS.value);
				UpdateDiv('divProvider', valCoverGen_sProvider.value + ' - ' + valCoverGen_sCliename.value);
				break;
		}
	}
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.MakeMenu("CAL712", "CAL712.aspx", 1, vbNullString))
		.Write(mobjMenu.setZone(1, "CAL712", vbNullString))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CAL712" ACTION="valPolicyRep.aspx?sMode=2">
	<BR><BR>
	<%=mobjValues.ShowWindowsName("CAL712")%>
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), True, GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valCoverGenCaption") %></LABEL></TD>
            <TD>
				<%
With mobjValues.Parameters
	mobjValues.Parameters.Add("nInsur_Area", session("nInsur_area"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.ReturnValue("sCondSVS", True, "Condicionado", True)
	.ReturnValue("sProvider", False, vbNullString, True)
	.ReturnValue("sCliename", False, vbNullString, True)
End With
Response.Write(mobjValues.PossiblesValues("valCoverGen", "tabCoverProvider", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "insChangeField(this);",  ,  , GetLocalResourceObject("valCoverGenToolTip"),  ,  , True))
%> 
			</TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("divCondSVS", False, vbNullString)%></TD>
        </TR>
        <TR>
			<TD WIDTH="25%"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("divProvider", False, vbNullString)%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjMenu = Nothing
mobjValues = Nothing
%>




