<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

'- Variable que contiene la historia
Dim mstrHistory As String

'- Variable del objeto de funciones genericas
Dim mobjValues As eFunctions.Values

'- Variable del objeto de funciones del grid
Dim mobjGrid As eFunctions.Grid

'- Variable del indice
Dim mintIndex As Integer

'- Variable que mantiene el tipo de Transaction
Dim mstrTransaction As String

Dim mclsSecur_sche As eSecurity.Secur_sche


'% insMakeLink: Crea el link de las paginas cuando la ventana es PopUp
'--------------------------------------------------------------------------------------------
Private Function insMakeLink(ByRef sCodispl As String) As Object
	'--------------------------------------------------------------------------------------------
	Dim lobjQuery As eRemoteDB.Query
	lobjQuery = New eRemoteDB.Query
	With mobjGrid
		If lobjQuery.OpenQuery("Windows", "sDescript", "sCodispl='" & sCodispl & "'") Then
			.Columns("tctCodispl").DefValue = sCodispl
			.Columns("tctCodispl").HRefScript = "insNavigationActual('" & sCodispl & "')"
			.Columns("tctDescript").DefValue = lobjQuery.FieldToClass("sDescript")
			.Columns("tctDescript").HRefScript = "insNavigationActual('" & sCodispl & "')"
			Response.Write(.DoRow)
		End If
	End With
	lobjQuery = Nothing
End Function

'% insMakeURL: Crea un URL con las paginas
'--------------------------------------------------------------------------------------------
Private Sub insMakeURL(ByRef sCodisp As String, ByRef lintWindowty As Object, ByRef lstrModule As String, ByRef lstrProject As String, ByRef lintHeight As Object, ByRef sCodispl As String, ByRef sQueryString As String)
	'--------------------------------------------------------------------------------------------
	Dim lstrHref As String
	Dim lintIndex As Integer
	Dim lstrQueryString As String
	Dim lintPosition As Integer
	
	lstrHref = "/VTimeNet/Common/" & insGetBaseName(lintWindowty) & "?sCodispl=" & sCodispl & "&sModule=" & lstrModule & "&sProject=" & lstrProject & "&nHeight=" & lintHeight & "&sCodisp=" & sCodisp & sQueryString
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% Findparameters: ubica los parámetros del QueryString" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function Findparameters() {" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lstrParams = '';" & vbCrLf)
Response.Write("    lstrParams = (self.document.location.href.indexOf('LinkParam')=!-1) ? '&' + self.document.location.href.substr(self.document.location.href.indexOf('LinkParam')) : ''" & vbCrLf)
Response.Write("    return lstrParams" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	If Request.QueryString.Item("LinkSpecial") = "1" Then
		Session("LinkSpecial" & sCodispl) = Request.QueryString.Item("LinkSpecialAction")
		lstrHref = Trim(lstrHref) & "&sLinkSpecial=1"
		Response.Write("<SCRIPT>window.resizeTo(750, 450);window.moveTo(opener.window.screenLeft,opener.window.screenTop);</" & "Script>")
	End If
	
	'For lintIndex = 1 To Request.QueryString.Count
    For lintIndex = 0 To Request.QueryString.Count -1
'UPGRADE_WARNING: Request property Request.QueryString.Key has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup2065.aspx'
		If InStr(1, lstrHref, Request.QueryString.GetKey(lintIndex)) = 0 Then
'UPGRADE_WARNING: Request property Request.QueryString.Key has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup2065.aspx'
			lstrQueryString = lstrQueryString & "&" & Request.QueryString.GetKey(lintIndex) & "=" & Request.QueryString.Item(lintIndex)
		End If
	Next 
	
	Response.Write("<SCRIPT>top.document.location.href='" & lstrHref & lstrQueryString & "'</" & "Script>")
	
	lintPosition = InStr(1, Session("sHistory"), Trim(sCodispl))
	
	If lintPosition = 0 Then
		Session("sHistory") = Trim(sCodispl) & New String(" ", 8 - Len(sCodispl)) & Session("sHistory")
	End If
End Sub

'% insNavigateTo: Navegación destino 
'--------------------------------------------------------------------------------------------
Private Function insNavigateTo() As Object
	'--------------------------------------------------------------------------------------------
	Dim lobjQuery As eRemoteDB.Query
	Dim lintWindowty As Object
	Dim lintModules As Short
	Dim lstrCodispl As String
	Dim lstrCodisp As String
	Dim lintHeight As Object
	
	If Request.QueryString.Item("sCodispl") = "-1" Then
		If Mid(Session("sHistory"), 9, 8) = vbNullString Then
			'CloseWindow
			Response.Write("<SCRIPT> top.close(); </" & "Script>")
			Exit Function
		End If
		lstrCodispl = Trim(Mid(Session("sHistory"), 9, 8))
	Else
		lstrCodispl = UCase(Trim(Request.QueryString.Item("sCodispl")))
	End If
	
	lobjQuery = New eRemoteDB.Query
	
	With lobjQuery
		If .OpenQuery("windows", "nWindowty, nModules, sDescript, nHeight,sCodisp", "sCodispl='" & lstrCodispl & "'") Then
			If IsDbNull(.FieldToClass("nWindowty")) Then
				lintWindowty = 0
			Else
				lintWindowty = CShort(.FieldToClass("nWindowty"))
			End If
			
			If IsDbNull(.FieldToClass("nModules")) Then
				lintModules = 0
			Else
				lintModules = CShort(.FieldToClass("nModules"))
			End If
			
			If IsDbNull(.FieldToClass("nHeight")) Then
				lintHeight = 0
			Else
				lintHeight = CShort(.FieldToClass("nHeight"))
			End If
			
			lstrCodisp = .FieldToClass("sCodisp")
			
			If lintHeight = 0 Then
				lintHeight = 130
			Else
				lintHeight = .FieldToClass("nHeight")
			End If
			
			.Closequery()
			If .OpenQuery("tab_sys_exe",  , "nExe_code=" & lintModules) Then
				If CShort(lintWindowty) <> CShort(eFunctions.Menues.TypeForm.clngGeneralTable) Then
					insMakeURL(lstrCodisp, lintWindowty, .FieldToClass("sFolderName"), .FieldToClass("sExe_name"), lintHeight, lstrCodispl, "")
				Else
					insMakeURL(lstrCodisp, lintWindowty, .FieldToClass("sFolderName"), "MantTables", lintHeight, lstrCodispl, "")
				End If
				.Closequery()
			End If
		Else
			Response.Write("<SCRIPT>" & "alert('" & "No se encontró información sobre la transacción" & "');" & "top.close();" & "</" & "Script>")
		End If
	End With
	lobjQuery = Nothing
End Function

'% insGetBaseName: Obtiene el tipo de ventana
'--------------------------------------------------------------------------------------------
Private Function insGetBaseName(ByRef lintWindowty As Object) As String
	'--------------------------------------------------------------------------------------------
	
	Select Case lintWindowty
		Case eFunctions.Menues.TypeForm.clngSpeWithHeader, eFunctions.Menues.TypeForm.clngRepWithHeader
			insGetBaseName = "SpeWHeader.aspx"
		Case eFunctions.Menues.TypeForm.clngSeqWithHeader, eFunctions.Menues.TypeForm.clngSeqWithOutHeader
			insGetBaseName = "SecWHeader.aspx"
		Case eFunctions.Menues.TypeForm.clngSpeWithOutHeader, eFunctions.Menues.TypeForm.clngRepWithOutHeader
			insGetBaseName = "SpeWOHeader.aspx"
		Case eFunctions.Menues.TypeForm.clngFraSpecific
		Case eFunctions.Menues.TypeForm.clngMenu
		Case eFunctions.Menues.TypeForm.clngFraRepetitive
		Case eFunctions.Menues.TypeForm.clngGeneralTable
			insGetBaseName = "SpeWOHeader.aspx"
		Case eFunctions.Menues.TypeForm.clngWindowsPopUp
	End Select
End Function

'% CloseWindow: Cierra la ventana o recarga la anterior
'--------------------------------------------------------------------------------------------
Private Sub CloseWindow()
	'--------------------------------------------------------------------------------------------
	If Request.QueryString.Item("sPopUp") = "1" Then
		Response.Write("<SCRIPT>top.window.close();</" & "Script>")
	Else
		Response.Write("<SCRIPT>self.history.go(-1);</" & "Script>")
	End If
End Sub

'% insDefineHeader: Se define el Header del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "goto"
	
	With mobjGrid
		Call .Columns.AddTextColumn(40528, GetLocalResourceObject("tctCodisplColumnCaption"), "tctCodispl", 15, vbNullString)
		Call .Columns.AddTextColumn(40529, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString)
		
		.Columns("Sel").GridVisible = False
		.DeleteButton = False
		.AddButton = False
		.AltRowColor = True
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "goto"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.WindowsTitle("GE001")%>    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 27/11/03 12:48 $|$$Author: Nvaplat7 $"


// insNavigationActual: Llama a las paginas dependiendo del codispl
//-------------------------------------------------------------------------------------------
function insNavigationActual(sCodispl){
//-------------------------------------------------------------------------------------------
	var win
    if (sCodispl==''){ 
//+ Al aceptar, el código lógico debe estar lleno
		alert("Err. 99004: <%=eFunctions.Values.GetMessage(99004)%>")
		self.document.forms[0].cmdAccept.disabled = false;
		}
	else{
		if("<%=Request.QueryString.Item("sPopUp")%>"=="1")
//+ Si la ventana fué invocada desde el menú principal, se muestra la transacción 
//+ en otra ventana
			if(typeof(opener.top.frames["FraModules"])!='undefined'){
				win = open('/VTimeNet/Common/GoTo.aspx?sCodispl=' + sCodispl, 'Transaccion','toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
				win.moveTo(0, 0);
				win.resizeTo(window.screen.availWidth, window.screen.availHeight);
			}
			else
				opener.top.location.href = "GoTo.aspx?sCodispl=" + sCodispl + "&nIndic=" + 1;
		else
	       top.location.href = "GoTo.aspx?sCodispl=" + sCodispl;
	
	    self.window.close();
    }
}
</SCRIPT>
<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("sCodispl") = vbNullString Then
	Response.Write(mobjValues.ShowWindowsName("GE001"))
	%>
<FORM ID=form1 NAME=Form1 ACTION="JAVASCRIPT:insNavigationActual(document.forms[0].valTransaction.value)">
<%	
	mstrHistory = Session("sHistory")
	
	mstrTransaction = Mid(mstrHistory, 1, 8)
	
	Call insDefineHeader()
	%>
    <DIV ID="Scroll" STYLE="width:370;height:200;overflow:auto; outset gray">
<%	
	For mintIndex = 1 To 80 Step 8
		mstrTransaction = Mid(mstrHistory, mintIndex, 8)
		If mstrTransaction = vbNullString Then
			Exit For
		End If
		insMakeLink(mstrTransaction)
	Next 
	Response.Write(mobjGrid.closeTable() & "</DIV>")
	mobjGrid = Nothing
	%>
    <TABLE WIDTH=100%>
		<TR>
			<TD WIDTH=20%><LABEL><%= GetLocalResourceObject("valTransactionCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valTransaction", "tabWindowsNotMenu", 2, vbNullString,  ,  ,  ,  , 30,  ,  , 8, GetLocalResourceObject("valTransactionToolTip"), eFunctions.Values.eTypeCode.eString)%></TD>
		</TR>
		<TR>
			<TD CLASS="HorLine" COLSPAN="2"></TD>
		</TR>
		<TR>
			<TD><%=mobjValues.ButtonHelp("GE001")%></TD>
			<TD ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel()%></TD>
		</TR>
    </TABLE>
</FORM>
<%	
Else
	mclsSecur_sche = New eSecurity.Secur_sche
	
	If mclsSecur_sche.valTransAccess(Session("sSche_code"), Request.QueryString.Item("sCodispl"), "2") Then
		insNavigateTo()
	Else
		Response.Write("<SCRIPT>alert(""" & eFunctions.Values.GetMessage(12103) & " (" & Request.QueryString.Item("sCodispl") & ")" & """)</SCRIPT>")
		
		If CDbl(Request.QueryString.Item("nIndic")) = 1 Then
			Response.Write("<SCRIPT>self.history.go(-1)</SCRIPT>")
		Else
			Response.Write("<SCRIPT>window.close();opener.document.location.reload();</SCRIPT>")
		End If
	End If
End If
mclsSecur_sche = Nothing
mobjValues = Nothing
%>
</BODY>
</HTML>




