<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As Object
Dim lclsCollector As eCollection.Collector


Private Sub inspreCO685()
	lclsCollector.Find(mobjValues.StringToType(Request.QueryString.Item("nCollector"), eFunctions.Values.eTypeData.etdDouble, True), "")
	
	Session("nMainAction") = Request.QueryString.Item("nMainAction")
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
lclsCollector = New eCollection.Collector
mobjValues.sCodisplPage = "co685"
%>
<HTML>
<HEAD>

   <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CO685", "CO685.aspx"))
	Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 30/09/03 11:19 $|$$Author: Nvaplat15 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTabCollector" ACTION="valCollectionTra.aspx?">
<%
Response.Write(mobjValues.ShowWindowsName("CO685"))
Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnCollector.value='" & Request.QueryString.Item("nCollector") & "';top.fraHeader.$('#tcnCollector').change()</SCRIPT>")
Call inspreCO685()
%>
<TABLE WIDTH="100%">
	<TD>
	   <TD><LABEL ID=0><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
	           
	   <%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then%>
			<TD COLSPAN=2><%=mobjValues.ClientControl("dtcClient", lclsCollector.sClient,  , GetLocalResourceObject("dtcClientToolTip"),  , False, "lblCliename")%></TD>
	   <%Else%>
	        <TD COLSPAN=2><%=mobjValues.ClientControl("dtcClient", lclsCollector.sClient,  , GetLocalResourceObject("dtcClientToolTip"),  , True, "lblCliename")%></TD>
	   <%End If%>
	</TR>
	        
	<TR>
	    <TD><LABEL ID=0><%= GetLocalResourceObject("dtInputDateCaption") %></LABEL></TD>
	    <%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then%>
				<TD><%=mobjValues.DateControl("dtInputDate", mobjValues.DatetoString(lclsCollector.dInputDate),  , GetLocalResourceObject("dtInputDateToolTip"),  ,  ,  ,  , False)%></TD>
		<%Else%>
		        <TD><%=mobjValues.DateControl("dtInputDate", mobjValues.DatetoString(lclsCollector.dInputDate),  , GetLocalResourceObject("dtInputDateToolTip"),  ,  ,  ,  , True)%></TD>
		<%End If%>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcnColTypeCaption") %></LABEL></TD>	
		<%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then%>
			 <TD COLSPAN=2><% =mobjValues.PossiblesValues("tcnColType", "Table5551", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCollector.nCollectorType),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("tcnColTypeToolTip"))%></TD>
	    <%Else%>			
			<TD COLSPAN=2><% =mobjValues.PossiblesValues("tcnColType", "Table5551", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCollector.nCollectorType),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnColTypeToolTip"))%></TD>
	    <%End If%>
	</TR>
	<TR>
	    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnConTypeCaption") %></LABEL></TD>
	<%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then%>
	        <TD><% =mobjValues.PossiblesValues("tcnConType", "Table5557", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCollector.nConType),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("tcnConTypeToolTip"))%> </TD>
	<%Else%>
			<TD><% =mobjValues.PossiblesValues("tcnConType", "Table5557", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCollector.nConType),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnConTypeToolTip"))%> </TD>	
	<%End If%>
	
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCodeCaption") %></LABEL></TD>
	<%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then%>
	    <TD><%=mobjValues.NumericControl("tcnCode", 5, CStr(lclsCollector.nCode),  , GetLocalResourceObject("tcnCodeToolTip"),  ,  ,  ,  ,  ,  , False)%></TD>
	<%Else%>
	    <TD><%=mobjValues.NumericControl("tcnCode", 5, CStr(lclsCollector.nCode),  , GetLocalResourceObject("tcnCodeToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
	<%End If%>


	</TR>	
	<TR>
	    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnInsur_areaCaption") %></LABEL></TD>
	   <%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then%>
	   	   <TD ><%=mobjValues.PossiblesValues("tcnInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCollector.nInsur_Area),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("tcnInsur_areaToolTip"))%> </TD>
	   <%Else%>
	           <TD ><%=mobjValues.PossiblesValues("tcnInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCollector.nInsur_Area),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnInsur_areaToolTip"))%> </TD>
	   <%End If%>
	   <TD><LABEL ID=0><%= GetLocalResourceObject("tcnLegal_SchCaption") %></LABEL></TD>
	   <%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then%>
	       <TD ><%=mobjValues.PossiblesValues("tcnLegal_Sch", "Table5501", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCollector.nInsur_Area),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("tcnLegal_SchToolTip"))%> </TD> 
	   <%Else%>
	       <TD ><%=mobjValues.PossiblesValues("tcnLegal_Sch", "Table5501", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCollector.nInsur_Area),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnLegal_SchToolTip"))%> </TD> 
	   <%End If%>
	</TR>
</TABLE>
</FORM>
</BODY>
</HTML>




