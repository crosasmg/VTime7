<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim lobjConstruction As eClaim.Construction
Dim lintSta_local As Integer


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "os592_1"
%>
<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"
</SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">

//% InsClickField: 
//---------------------------------------------------------------------------------------------------
function InsClickField(objField)
//---------------------------------------------------------------------------------------------------
{	
	if (objField.checked == true)
		objField.value = "1"
	else
		objField.value = "2"
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	'+ Si se trata de una ventana que no forma parte del encabezado de la transacción colocar:
	Response.Write(mobjMenu.setZone(2, "OS592_1", "OS592_1.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="OS592_1" ACTION="valProf_ordseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%=mobjValues.ShowWindowsName("OS592_1")%>
<%lobjConstruction = New eClaim.Construction
lobjConstruction.Find(Session("Nserv_order"))
If lobjConstruction.nSta_local = eRemoteDB.Constants.intNull Then
	lintSta_local = 1
Else
	lintSta_local = lobjConstruction.nSta_local
End If
%>
    <TABLE WIDTH="100%">
    <TR>&nbsp</TR>    
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAreaCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnArea", 7, CStr(lobjConstruction.nArea),  , GetLocalResourceObject("tcnAreaToolTip"),  , 2)%></TD>			
			<TD WIDTH="15%">&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnOldnessCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnOldness", 5, CStr(lobjConstruction.nOldness),  , GetLocalResourceObject("tcnOldnessToolTip"))%></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"></TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Estado"><%= GetLocalResourceObject("AnchorEstadoCaption") %></A></LABEL></TD>
			<TD>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeStructure_wallCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStructure_wall", "table5536", eFunctions.Values.eValuesType.clngComboType, CStr(lobjConstruction.nStructure_wall),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStructure_wallToolTip"))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
			<TD WIDTH="100%" COLSPAN="2"></TD>
		</TR>
        <TR>
   			<TD COLSPAN="2">
   				<TABLE WIDTH="100%">
   					<TR>
   						<TD><%=mobjValues.OptionControl(0, "optSta_local", GetLocalResourceObject("optSta_local_1Caption"), CStr(2 - lintSta_local), "1")%></TD>
						<TD><%=mobjValues.OptionControl(0, "optSta_local", GetLocalResourceObject("optSta_local_2Caption"), CStr(3 - lintSta_local), "2")%></TD>
						<TD><%=mobjValues.OptionControl(0, "optSta_local", GetLocalResourceObject("optSta_local_3Caption"), CStr(4 - lintSta_local), "3")%></TD>
   					</TR>
   				</TABLE>
   			</TD>
   			<TD>&nbsp</TD>
   			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeStruct_wallintCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStruct_wallint", "table5536", eFunctions.Values.eValuesType.clngComboType, CStr(lobjConstruction.nStruct_wallint),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStruct_wallintToolTip"))%></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeRoofTypeCaption") %></LABEL></TD>
	        <TD><%=mobjValues.PossiblesValues("cbeRoofType", "table7038", eFunctions.Values.eValuesType.clngComboType, CStr(lobjConstruction.nRooftype),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRoofTypeToolTip"))%></TD>
	        <TD>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeStructure_typeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStructure_type", "table5538", eFunctions.Values.eValuesType.clngComboType, CStr(lobjConstruction.nStructure_type),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStructure_typeToolTip"))%></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"></TD>
        </TR>
		<TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeStruct_mezzCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStruct_mezz", "table5538", eFunctions.Values.eValuesType.clngComboType, CStr(lobjConstruction.nStruct_mezz),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStruct_mezzToolTip"))%></TD>
            <TD>&nbsp</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeSideCloseTypeCaption") %></LABEL></TD>
	        <TD><%=mobjValues.PossiblesValues("cbeSideCloseType", "table7037", eFunctions.Values.eValuesType.clngComboType, CStr(lobjConstruction.nSideclosetype),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSideCloseTypeToolTip"))%></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
	        <TD>
	        <%If lobjConstruction.sSubway = "1" Then
	Response.Write(mobjValues.CheckControl("chkSubway", "", "1", "1", "InsClickField(this)",  ,  , GetLocalResourceObject("chkSubwayToolTip")))
Else
	Response.Write(mobjValues.CheckControl("chkSubway", "", "2", "2", "InsClickField(this)",  ,  , GetLocalResourceObject("chkSubwayToolTip")))
End If
%>
			</TD>
			<TD>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnFloorCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnFloor", 5, CStr(lobjConstruction.nFloor),  , GetLocalResourceObject("tcnFloorToolTip"))%></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"></TD>
        </TR>
        <TR>
   			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnTotalFloorCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTotalFloor", 5, CStr(lobjConstruction.nTotalfloor),  , GetLocalResourceObject("tcnTotalFloorToolTip"))%></TD>
   			<TD></TD>
   			<TD></TD>
		</TR>
    </TABLE>
<%
lobjConstruction = Nothing%>    
</FORM> 
</BODY>
</HTML>




