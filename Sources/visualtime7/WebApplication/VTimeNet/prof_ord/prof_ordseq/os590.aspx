<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim lobjProf_ord As eClaim.Prof_ord
Dim npoliza As Object
Dim nproposal As Object
Dim dassigndate As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "os590"
%>
<HTML>
<HEAD>
	<SCRIPT>
//+ Variable para el control de versiones
	    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.00 $"
    </SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "OS590", "OS590.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="OS590" ACTION="valProf_ordseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%=mobjValues.ShowWindowsName("OS590")%>
<%lobjProf_ord = New eClaim.Prof_ord

lobjProf_ord.Find_nServ(Session("Nserv_order"))

If lobjProf_ord.dMade_date = eRemoteDB.Constants.dtmNull Then
	dassigndate = lobjProf_ord.dFec_prog
Else
	dassigndate = lobjProf_ord.dMade_date
End If
If lobjProf_ord.nOrdClass = 1 Then
	npoliza = vbNullString
	If lobjProf_ord.nPolicy = eRemoteDB.Constants.intNull Then
		nproposal = vbNullString
	Else
		nproposal = lobjProf_ord.nPolicy
	End If
Else
	If lobjProf_ord.nOrdClass = 2 Then
		npoliza = lobjProf_ord.nPolicy
		nproposal = vbNullString
	Else
		npoliza = vbNullString
		nproposal = vbNullString
	End If
End If
%>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProviderCaption") %></LABEL></TD>
			<TD><%
With mobjValues
	.Parameters.Add("nBranch", lobjProf_ord.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProvider", "tabTab_provider", eFunctions.Values.eValuesType.clngWindowType, CStr(lobjProf_ord.nProvider), True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valProviderToolTip")))
End With
%>
			</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeOrdclassCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOrdclass", "table560", eFunctions.Values.eValuesType.clngComboType, CStr(lobjProf_ord.nOrdClass),  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeOrdclassToolTip"))%></TD>
        </TR>
        <TR>
   			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeOrdertypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOrdertype", "table7100", eFunctions.Values.eValuesType.clngComboType, CStr(lobjProf_ord.nOrderType),  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeOrdertypeToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(lobjProf_ord.nBranch), "valProduct",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
   			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(lobjProf_ord.nBranch), eFunctions.Values.eValuesType.clngWindowType, True, CStr(lobjProf_ord.nProduct))%></TD> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
	        <TD><%=mobjValues.TextControl("tcnPolicy", 10, npoliza,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  , True)%> </TD>
        </TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnProposalCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tcnProposal", 10, nproposal,  , GetLocalResourceObject("tcnProposalToolTip"),  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnClaimCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tcnClaim", 10, mobjValues.TypeToString(lobjProf_ord.nClaim, eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnClaimToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdAssigndateCaption") %></LABEL></TD>
	        <TD><%=mobjValues.DateControl("tcdAssigndate", CStr(lobjProf_ord.dassigndate),  , GetLocalResourceObject("tcdAssigndateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdMadedateCaption") %></LABEL></TD>
	        <TD><%=mobjValues.DateControl("tcdMadedate", dassigndate,  , GetLocalResourceObject("tcdMadedateToolTip"),  ,  ,  ,  , False)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInpdateCaption") %></LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.DateControl("tcdInpdate", CStr(lobjProf_ord.dInpdate),  , GetLocalResourceObject("tcdInpdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctPlaceCaption") %></LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.TextControl("tctPlace", 50, lobjProf_ord.splace,  , GetLocalResourceObject("tctPlaceToolTip"),  ,  ,  ,  , False)%></TD> 
		</TR>
		<TR>    
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeMunicipalityCaption") %></LABEL></TD>
	        <TD><%=mobjValues.PossiblesValues("cbeMunicipality", "tabmunicipality", eFunctions.Values.eValuesType.clngComboType, CStr(lobjProf_ord.nMunicipality), False,  ,  ,  ,  ,  , False, 5, GetLocalResourceObject("cbeMunicipalityToolTip"))%> </TD>	        
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeStatus_ordCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeStatus_ord", "table215", eFunctions.Values.eValuesType.clngComboType, CStr(lobjProf_ord.nStatus_ord), False,  ,  ,  ,  ,  , False, 5, GetLocalResourceObject("cbeStatus_ordToolTip"))%></TD>
		</TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
lobjProf_ord = Nothing%>




