<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values

'+ El uso de la variable mclsClaim es temporal.
'+ Es para el uso del personal de Caracas.
'+ No eliminar.  Rhony Mujica

Dim mclsClaim As eClaim.Claim


'%LoadMainMenu. Esta función se encarga de mostrar los módulos del sistema
'---------------------------------------------------------------------------------------------
Private Sub LoadMainMenu()
	'---------------------------------------------------------------------------------------------
	Dim lrecWindows As eRemoteDB.Query
	Dim lstrCodmen As String
	Dim lobjValues As eFunctions.Values
	Dim lstrButton As String
	Dim lstrInitial As String
	Dim lstrString As String
	lrecWindows = New eRemoteDB.Query
	lobjValues = New eFunctions.Values
	lstrString = vbNullString
	With lrecWindows
		Response.Write("<TABLE>")
		lstrInitial = ""
		
		'+ Se carga el menu de la ultima transaccion de la cual proviene el usuario
		
		If CStr(Session("sHistory")) <> "" And Mid(Session("sHistory"), 1, 2) <> "ER" Then
			lstrInitial = Mid(Session("sHistory"), 1, 8)
			If lstrInitial <> "" Then
				If .OpenQuery("Windows", "sCodmen", "sCodispl = '" & Trim(lstrInitial) & "'") Then
					lstrCodmen = .FieldToClass("sCodmen")
					Do 
						If .OpenQuery("Windows", "sCodmen", "sCodispl = '" & Trim(lstrCodmen) & "'") Then
							If .FieldToClass("sCodmen") <> "MENU" Then
								lstrCodmen = .FieldToClass("sCodmen")
							Else
								lstrInitial = lstrCodmen
								lstrCodmen = vbNullString
							End If
						Else
							lstrCodmen = vbNullString
							lstrInitial = vbNullString
						End If
					Loop Until lstrCodmen = vbNullString
				Else
					lstrInitial = ""
				End If
			End If
			If lstrInitial <> vbNullString Then
				Response.Cookies.Item("sOldModule").Value = lstrInitial
				'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
				Response.Cookies.Item("sOldModule").Expires = DateAdd(Microsoft.VisualBasic.DateInterval.Day, Today.ToOADate, System.Date.FromOADate(7))
				lstrInitial = "MenuName.aspx?sModule=" & lstrInitial
			End If
		End If
		
		If lstrInitial = vbNullString Then
			If IIf(IsNothing(Response.Cookies.Item("sOldModule").Value), "", Request.Cookies.Item("sOldModule").Value) <> vbNullString Then
				lstrInitial = "MenuName.aspx?sModule=" & IIf(IsNothing(Response.Cookies.Item("sOldModule").Value), "", Request.Cookies.Item("sOldModule").Value)
			End If
		End If
		
		If .OpenQuery("Windows", "*", "sCodmen = 'MENU' and sStatregt = '1' ", "nSequence") Then
			Do While Not .EndQuery
				lstrButton = "MenuName.aspx?sModule=" & .FieldToClass("sCodispl")
				If lstrInitial = "" Then
					lstrInitial = lstrButton
				End If
				If lstrString = vbNullString Then
					lstrString = lobjValues.GetMessage(224)
				End If
				If lstrString <> vbNullString Then
					lstrButton = lobjValues.AnimatedButtonControl(.FieldToClass("sCodispl"), "/VTimeNet/images/" & .FieldToClass("sCodispl") & ".gif", Trim(lstrString) & " " & LCase(.FieldToClass("sDescript")), lstrButton)
				Else
					lstrButton = lobjValues.AnimatedButtonControl(.FieldToClass("sCodispl"), "/VTimeNet/images/" & .FieldToClass("sCodispl") & ".gif", .FieldToClass("sDescript"), lstrButton)
				End If
				Response.Write("<TR><TD ALIGN=CENTER><LABEL ID=-1 CLASS=TINY>" & lstrButton & "<BR>" & .FieldToClass("sDescript") & "</></LABEL></TD></TR>")
				.NextRecord()
			Loop 
			.CloseQuery()
		End If
		Response.Write("</TABLE>")
	End With
	If lstrInitial <> vbNullString Then
		Response.Write("<SCRIPT> if (typeof(top.FraHeader)!='undefined') top.FraHeader.document.location.href='" & lstrInitial & "';</" & "Script>")
	End If
	'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lrecWindows = Nothing
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mclsClaim = New eClaim.Claim

Call mclsClaim.Find(1)

'UPGRADE_NOTE: Object mclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsClaim = Nothing
%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <BASE TARGET="FraHeader">
</HEAD>
<%If CStr(Session("SessionId")) <> vbNullString Then
	Response.Write("<BODY BACKGROUND=""/VTimeNet/images/FrameModules.jpg"" BGPROPERTIES=""fixed"">")
	Call LoadMainMenu()
Else
	Response.Write("<BODY BACKGROUND=""/VTimeNet/images/FrameSequence.jpg"" BGPROPERTIES=""fixed"">")
End If
Response.Write("</BODY></HTML>")
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>




