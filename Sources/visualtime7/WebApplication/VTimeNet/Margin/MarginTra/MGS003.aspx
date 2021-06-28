<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<script language="VB" runat="Server">

Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values


'% insDefineHeader: se definen las Carac. del grid
'--------------------------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid.Columns
		Call .AddDateColumn(0, GetLocalResourceObject("tcdInitDateColumnCaption"), "tcdInitDate", vbNullString,  , GetLocalResourceObject("tcdInitDateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEndDateColumnCaption"), "tcdEndDate", vbNullString,  , GetLocalResourceObject("tcdEndDateColumnToolTip"))
		'Call .AddHiddenColumn("hddIdTable", vbNullString)
	End With
	With mobjGrid
		.Codispl = "MGS003"
		.sCodisplPage = "MGS003"
		.Columns("Sel").GridVisible = False
		.AltRowColor = True
		.AddButton = False
		.DeleteButton = False
	End With
End Function

'% inspreMGS003: se cargan los valores de la ventana
'--------------------------------------------------------------------------------------------
Private Function inspreMGS003() As Object
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolMargin_master As eMargin.Margin_masters
	Dim lclsMargin_master As Object
	lcolMargin_master = New eMargin.Margin_masters
	Response.Write("<DIV ID=""Scroll"" STYLE=""width:305;height:225;overflow:auto;outset gray"">")
	If lcolMargin_master.Find_Period(mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintIndex = 0
		For	Each lclsMargin_master In lcolMargin_master
			With mobjGrid
				.Columns("tcdInitDate").DefValue = lclsMargin_master.dInitDate
				.Columns("tcdEndDate").DefValue = lclsMargin_master.dEndDate
				.Columns("tcdInitDate").HRefScript = "insAccept(" & lintIndex & ")"
				'.Columns("hddIdTable").DefValue = lclsMargin_master.nIdTable
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsMargin_master
	End If
	With Response
		.Write(mobjGrid.closeTable)
		.Write("</DIV>")
	End With
	
Response.Write("  <BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("MGS003"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonHelp("MGS003"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel( ,  , False,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lcolMargin_master = Nothing
	lclsMargin_master = Nothing
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGS003"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 12 $|$$Date: 26/11/03 13:10 $|$$Author: Nvaplat15 $"

//% insAccept: se realizan las acciones al seleccionar un período
//-------------------------------------------------------------------------------------------
function insAccept(nIndex){
//-------------------------------------------------------------------------------------------
	var lstrHREF = top.opener.document.location.href;
	//lstrHREF = lstrHREF.replace(/&dInitDate.*/,'') + '&dInitDate=' + marrArray[nIndex].tcdInitDate + '&dEndDate=' + marrArray[nIndex].tcdEndDate + '&nInsur_area=' + <%=Request.QueryString.Item("nInsur_area")%> + '&nMainAction=' + <%=Request.QueryString.Item("nMainAction")%> + '&sReload=MGS003' + '&nIdTable=' + marrArray[nIndex].hddIdTable;
	lstrHREF = lstrHREF.replace(/&dInitDate.*/,'') + '&dInitDate=' + marrArray[nIndex].tcdInitDate + '&dEndDate=' + marrArray[nIndex].tcdEndDate + '&nInsur_area=' + <%=Session("nInsur_area")%> + '&nMainAction=' + <%=Request.QueryString.Item("nMainAction")%> + '&sReload=MGS003';
	top.opener.document.location.href = lstrHREF;
}
</SCRIPT>
</HEAD>
<BODY>
<%
Response.Write(mobjValues.ShowWindowsName("MGS003"))
Response.Write(mobjValues.WindowsTitle("MGS003"))
Call insDefineHeader()
Call inspreMGS003()
mobjValues = Nothing
mobjGrid = Nothing
%>
</BODY ONLOAD="window.focus()">
</HTML>




