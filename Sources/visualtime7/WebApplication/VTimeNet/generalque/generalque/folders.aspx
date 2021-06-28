<%@ Page Language="VB" %>
<%@ Import namespace="eRemoteDB" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">


'%LoadMainMenu. Esta función se encarga de mostrar los módulos del sistema
'---------------------------------------------------------------------------------------------
Private Sub LoadMainMenu()
	'---------------------------------------------------------------------------------------------
	Dim lrecWindows As eRemoteDB.Query
	Dim lobjValues As eFunctions.Values
	Dim lstrButton As String
	Dim lstrInitial As String
	lrecWindows = New eRemoteDB.Query
	lobjValues = New eFunctions.Values
	
	With lrecWindows
		
Response.Write("     <TABLE>")

		
		lstrInitial = ""
		If .OpenQuery("Windows", "*", "sCodmen = 'MENU' and sStatregt = '1' ", "nSequence") Then
			Do While Not .EndQuery
				lstrButton = "MenuName.aspx?sModule=" & .FieldToClass("sCodispl")
				If lstrInitial = "" Then
					lstrInitial = lstrButton
				End If
				lstrButton = lobjValues.AnimatedButtonControl(.FieldToClass("sCodispl"), "/VTimeNet/images/" & .FieldToClass("sCodispl") & ".gif", .FieldToClass("sCodispl"), lstrButton)
				'                lstrButton = lobjValues.AnimatedButtonControl(.FieldToClass("sCodispl"), '                                                              "/VTimeNet/images/" & .FieldToClass("sCodispl") & ".gif", '                                                              .FieldToClass("sCodispl"),,"insSelectModule('" & "')")
				Response.Write("<TR><TD ALIGN=CENTER><LA" & "BEL >" & lstrButton & "<BR>" & .FieldToClass("sDescript") & "</LA" & "BEL></TD></TR>")
				'                y.IconIndex = ailModules.ItemIndex(.FieldToClass("sCodispl"))
				.NextRecord()
			Loop 
			.CloseQuery()
		End If
		
Response.Write("     </TABLE>")

		
	End With
	If lstrInitial <> vbNullString Then
		Response.Write("<SCRIPT> if (typeof(top.FraHeader)!='undefined') top.FraHeader.document.location.href='" & lstrInitial & "';</" & "Script>")
	End If
	'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lrecWindows = Nothing
End Sub

</script>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <BASE TARGET="FraHeader">
</HEAD>
<%If CStr(Session("SessionId")) <> vbNullString Then
	Response.Write("<BODY background=""/VTimeNet/images/FrameModules.jpg"" bgproperties=""fixed"">")
	Call LoadMainMenu()
Else
	Response.Write("<BODY background=""/VTimeNet/images/FrameSequence.jpg"" bgproperties=""fixed"">")
End If
%>
</BODY>
</HTML>




