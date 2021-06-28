<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

Dim sEdiParam As String
Dim cSQLS As Object
Dim oPage As Object
Dim oPageContent As Object
Dim oField As Object
Dim oSQL As Object



Private Sub PageSimple()
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "VDATA.aspx"))
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE>")

	
	
	If cSQLS.Count = 0 Then
		oSQL = cSQLS.EmptyRecord
	End If
	
	For	Each oSQL In cSQLS
		For	Each oPageContent In oPage.mcPageContents
			
			If oPageContent.bHeader = 0 Then
				
Response.Write("		" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("     <TD>")


Response.Write(oPageContent.sCaption)


Response.Write("</TD>" & vbCrLf)
Response.Write("     <TD>")

				
				If oPageContent.sLookupTable > vbNullString Or oPageContent.bListValues = True Then
					
					If oPageContent.sLookupTable > vbNullString Then
						Response.Write("<TD>" & mobjValues.PossiblesValues(oPageContent.sFieldName, "TABLE" & oPageContent.sLookupTable, 1, oSQL.cProperty(oPageContent.sFieldName).vValue) & "</TD>")
					Else
						mobjValues.Parameters.Add("sCode", Request.QueryString.Item("sCodispl"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						mobjValues.Parameters.Add("sFieldName", oPageContent.sFieldName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						Response.Write("<TD>" & mobjValues.PossiblesValues(oPageContent.sFieldName, "VDATA_List", 1, oSQL.cProperty(oPageContent.sFieldName).vValue, True) & "</TD>")
					End If
					
				Else
					Select Case oPageContent.nType
						Case 0 'bit
							Response.Write(mobjValues.CheckControl(oPageContent.sFieldName, "", oSQL.cProperty(oPageContent.sFieldName).vValue))
						Case 1 'char
							Response.Write(mobjValues.TextControl(oPageContent.sFieldName, oPageContent.nLength, oSQL.cProperty(oPageContent.sFieldName).vValue, oPageContent.bRequired, oPageContent.sToolTip))
						Case 2 'datetime
							Response.Write(mobjValues.DateControl(oPageContent.sFieldName, oSQL.cProperty(oPageContent.sFieldName).vValue, oPageContent.bRequired, oPageContent.sToolTip))
						Case 3 'decimal
							Response.Write(mobjValues.NumericControl(oPageContent.sFieldName, oPageContent.nLength, oSQL.cProperty(oPageContent.sFieldName).vValue, oPageContent.bRequired, oPageContent.sToolTip))
						Case 4 'int
							Response.Write(mobjValues.NumericControl(oPageContent.sFieldName, 5, oSQL.cProperty(oPageContent.sFieldName).vValue, oPageContent.bRequired, oPageContent.sToolTip))
						Case 5 'smallint
							Response.Write(mobjValues.NumericControl(oPageContent.sFieldName, 2, oSQL.cProperty(oPageContent.sFieldName).vValue, oPageContent.bRequired, oPageContent.sToolTip))
					End Select
				End If
				
Response.Write("			" & vbCrLf)
Response.Write("     </TD>" & vbCrLf)
Response.Write("	</TR>")

				
			End If
			
		Next oPageContent
	Next oSQL
	
	
	Response.Write("</TABLE>")
	
End Sub

Private Sub PageColumnDef()
	
	mobjGrid.ActionQuery = Session("bQuery")
	
	For	Each oPageContent In oPage.mcPageContents
		With mobjGrid.Columns
			If Not oPageContent.bHeader Then
				Select Case oPageContent.nType
					Case 0 'bit
						.AddCheckColumn(0, oPageContent.sCaption, oPageContent.sFieldName, "")
					Case 1 'char
						.AddTextColumn(0, oPageContent.sCaption, oPageContent.sFieldName, oPageContent.nLength, "")
					Case 2 'datetime
						.AddDateColumn(0, oPageContent.sCaption, oPageContent.sFieldName)
					Case 3 'decimal
						.AddNumericColumn(0, oPageContent.sCaption, oPageContent.sFieldName, oPageContent.nLength, "")
					Case 4 'int
						.AddNumericColumn(0, oPageContent.sCaption, oPageContent.sFieldName, oPageContent.nLength, "")
						If oPageContent.bRange = True Then
							.AddNumericColumn(0, oPageContent.sCaption, oPageContent.sFieldName & " Range", oPageContent.nLength, "",  ,  ,  ,  ,  ,  ,  , True)
						End If
					Case 5 'smallint
						.AddNumericColumn(0, oPageContent.sCaption, oPageContent.sFieldName, oPageContent.nLength, "")
						If oPageContent.bRange = True Then
							.AddNumericColumn(0, oPageContent.sCaption, oPageContent.sFieldName & " Range", oPageContent.nLength, "",  ,  ,  ,  ,  ,  ,  , True)
						End If
				End Select
			End If
		End With
	Next oPageContent
	
	sEdiParam = sEdiParam '+ "&" + cSQLS.sFirstColumn + "='marrArray[lintIndex]." + cSQLS.sFirstColumn + "'"
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "VData"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns(cSQLS.sFirstColumn).EditRecord = True
		
		.sDelRecordParam = "dBody1=' + marrArray[lintIndex].dBody1 + '"
		.DeleteButton = (CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401)
		.AddButton = (CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401)
		.Columns("Sel").GridVisible = (CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401)
		.Width = 450
		.Height = 220
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

Private Sub PageColumnPopulate()
	
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "VDATA.aspx"))
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	For	Each oSQL In cSQLS
		For	Each oPageContent In oPage.mcPageContents
			If oPageContent.bHeader = 0 Then
				mobjGrid.Columns(oPageContent.sFieldName).DefValue = oSQL.cProperty(oPageContent.sFieldName).vValue
			End If
		Next oPageContent
		Response.Write(mobjGrid.DoRow())
	Next oSQL
	Response.Write(mobjGrid.closeTable())
End Sub

Private Sub ActionKeys()
	Dim oField As Object
	
	With cSQLS
		For	Each oField In .cKeyProperty
			oField.vValue = Session(oField.sName)
		Next oField
		For	Each oField In .cAuxKeyProperty
			oField.vValue = Request.QueryString.Item(oField.sName)
		Next oField
		
	End With
End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<%Response.Write("<SCRIPT>var	nMainAction	= " & Request.QueryString.Item("nMainAction") & "</SCRIPT>")%>

<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" NAME="frmVData" ACTION="ValVDATA.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
'UPGRADE_NOTE: The 'eVdata.SQLS' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
cSQLS = Server.CreateObject("eVdata.SQLS")
cSQLS.Init(Request.QueryString.Item("sCodispl"))

If Request.QueryString.Item("Type") <> "PopUp" Then
	With cSQLS
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then
			
			sEdiParam = ""
			
			For	Each oField In .cKeyProperty
				oField.vValue = Session(oField.sName)
				If sEdiParam > "" Then
					sEdiParam = sEdiParam & "&"
				End If
				sEdiParam = CStr(CDbl(sEdiParam & "p") + oField.sName + CDbl("='") + oField.vValue + CDbl("'"))
			Next oField
			
			If .FindRecords Then
			End If
		End If
	End With
End If
'UPGRADE_NOTE: The 'eVdata.Page' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
oPage = Server.CreateObject("eVdata.Page")

With oPage
	If .Find(Request.QueryString.Item("sCodispl")) Then
		
		If oPage.bType = 0 Then
			Call PageSimple()
		Else
			
			Call PageColumnDef()
			
			If Request.QueryString.Item("Type") = "PopUp" Then
				If Request.QueryString.Item("Action") = "Del" Then
					Response.Write(mobjValues.ConfirmDelete())
					
					Call ActionKeys()
					
					cSQLS.DeleteRecord()
				End If
				
				Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValVDATA.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
			Else
				Call PageColumnPopulate()
			End If
		End If
	End If
End With

'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object cSQLS may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
cSQLS = Nothing
'UPGRADE_NOTE: Object oPage may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
oPage = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing

%>

</FORM>
</BODY>
</HTML>





