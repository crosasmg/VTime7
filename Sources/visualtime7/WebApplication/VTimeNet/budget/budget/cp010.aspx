<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBudget" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsBudget As eBudget.Budget



'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=4 WIDTH=""20%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8429>" & GetLocalResourceObject("tctDescriptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctDescript", 30, ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8434>" & GetLocalResourceObject("cbeStatregtCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8432>" & GetLocalResourceObject("cbeInit_monthCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeInit_month", "Table7013", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInit_monthToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8430>" & GetLocalResourceObject("cbeEnd_monthCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeEnd_month", "Table7013", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeEnd_monthToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8433>" & GetLocalResourceObject("SCA2-PCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""35%"" >")


Response.Write(mobjValues.ButtonNotes("SCA2-P", 0, True, Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)))


Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>		")

	
End Sub

</script>
<%
Response.Expires = 0



With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	lclsBudget = New eBudget.Budget
End With


If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "cp010"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "CP010", "CP010.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insSelected(Field){
//---------------------------------------------------------------------------
    Field.checked = !Field.checked
}


</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("CP010"))%>
<FORM METHOD="POST" ID="FORM" NAME="frmBudInqUpd" ACTION="ValBudget.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%">
        <%
Call insDefineHeader()

%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




