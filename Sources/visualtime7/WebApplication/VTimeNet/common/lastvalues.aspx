<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values


'% insDefineHeader: se definen las Carac. del grid
'--------------------------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "lastvalues"
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString, False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctValueColumnCaption"), "tctValue", 15, vbNullString, False)
	End With
	With mobjGrid
		.Columns("Sel").GridVisible = False
		.bOnlyForQuery = True
		.AltRowColor = True
		.ActionQuery = True
	End With
End Function

'% insPreLastValues: se cargan los valores de la ventana
'--------------------------------------------------------------------------------------------
Private Function insPreLastValues() As Object
	'--------------------------------------------------------------------------------------------
	Dim lcolGlobals As eGeneral.TimeGlobals
	Dim lclsGlobal As Object
	lcolGlobals = New eGeneral.TimeGlobals
	Response.Write("<DIV ID=""Scroll"" STYLE=""width:305;height:225;overflow:auto;outset gray"">")
	If lcolGlobals.Find() Then
		For	Each lclsGlobal In lcolGlobals
			If CStr(Session(lclsGlobal.sCode)) <> vbNullString And CStr(Session(lclsGlobal.sCode)) <> "-32768" Then
				With lclsGlobal
					If UCase(lclsGlobal.sCode) = "NPOLICY" Then
						If CStr(Session("sCertype")) <> vbNullString And CStr(Session("sCertype")) <> "2" Then
							Select Case Session("sCertype")
								Case "1", "6", "7", "8"
									mobjGrid.Columns("tctDescript").DefValue = "Propuesta"
								Case "3", "4", "5"
									mobjGrid.Columns("tctDescript").DefValue = "Cotización"
								Case Else
									mobjGrid.Columns("tctDescript").DefValue = lclsGlobal.sDescript
							End Select
						Else
							mobjGrid.Columns("tctDescript").DefValue = lclsGlobal.sDescript
						End If
					Else
						If UCase(lclsGlobal.sCode) <> "NPOLICY_OLD" Then
							mobjGrid.Columns("tctDescript").DefValue = lclsGlobal.sDescript
						End If
					End If
					
					If UCase(lclsGlobal.sCode) <> "NPOLICY_OLD" Then
						mobjGrid.Columns("tctValue").DefValue = Session(lclsGlobal.sCode)
						Response.Write(mobjGrid.DoRow())
					Else
						If Session("nTransaction") <> eRemoteDB.Constants.intNull And Session("nPolicy_old") <> eRemoteDB.Constants.intNull Then
							Select Case Session("nTransaction")
								Case 16, 23, 33, 35, 37, 38
									mobjGrid.Columns("tctDescript").DefValue = "Cotización"
									mobjGrid.Columns("tctValue").DefValue = Session(lclsGlobal.sCode)
									Response.Write(mobjGrid.DoRow())
								Case 17, 34, 36
									mobjGrid.Columns("tctDescript").DefValue = "Propuesta"
									mobjGrid.Columns("tctValue").DefValue = Session(lclsGlobal.sCode)
									Response.Write(mobjGrid.DoRow())
								Case 24, 25, 26, 27, 28, 29, 30, 31
									mobjGrid.Columns("tctDescript").DefValue = "Póliza"
									mobjGrid.Columns("tctValue").DefValue = Session(lclsGlobal.sCode)
									Response.Write(mobjGrid.DoRow())
							End Select
						End If
					End If
					
				End With
			End If
		Next lclsGlobal
	End If
	With Response
		.Write(mobjGrid.closeTable)
		.Write("</DIV>")
	End With
	
Response.Write("  <BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("GE812"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel( ,  , False,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lcolGlobals = Nothing
End Function

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "lastvalues"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 3/11/03 11:46 $"
</SCRIPT>
</HEAD>
<BODY>
<%
Response.Write(mobjValues.ShowWindowsName("GE812"))
Response.Write(mobjValues.WindowsTitle("GE812"))
Call insDefineHeader()
Call insPreLastValues()
mobjValues = Nothing
mobjGrid = Nothing
%>
</BODY ONLOAD="window.focus()">
</HTML>




