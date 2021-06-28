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


</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CAL979_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CAL979_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
//------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------------------
}
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}
//% insSetParam: Se valida que se haya colocado el ramo, producto y fecha de efecto para
//  habilitar el control de módulos
//------------------------------------------------------------------------------------------
function insSetParam() {
//------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        valModulec.disabled = (cbeBranch.value == 0 || valProduct.value == '' || tcdEffecdate.value == '');
        btnvalModulec.disabled = valModulec.disabled;

        valModulec.value = "";
        UpdateDiv("valModulecDesc", "");

        if (!valModulec.disabled) {
            valModulec.Parameters.Param1.sValue = cbeBranch.value;
            valModulec.Parameters.Param2.sValue = valProduct.value;
            valModulec.Parameters.Param3.sValue = tcdEffecdate.value;
        }
    }
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CAL979", "CAL979_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CAL979" ACTION="valPolicyRep.aspx?sMode=1">
	<BR><BR><BR>
	<TABLE WIDTH="70%" align="center">
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40667><a NAME="Título"><%= GetLocalResourceObject("AnchorTítuloCaption") %></a></LABEL></td>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
		<TR>	
			<TD>
				<LABEL ID=101675><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL>
			</TD>
			<TD>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "insSetParam();")%>
			</TD>
		</TR>
        <TR>
            <TD><LABEL ID=9380><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "insSetParam();")%></TD>
        <TR>
        </TR>
			<TD><LABEL ID=9389><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  ,  ,  ,  ,  ,  , "insSetParam();")%></TD>
        </TR>
        <TR> 
			<TD><LABEL ID=9388><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10,  ,  , GetLocalResourceObject("tcnPolicyToolTip"))%></TD>
			<TD>&nbsp;</TD>
		</TR>
        <TR> 
			<TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
			<TD>
                <%
With mobjValues.Parameters
	.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valModulecToolTip")))
%>
			</TD>
			<TD>&nbsp;</TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




