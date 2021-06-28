<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mobjBranchQue As ePolicy.Auto_db

'- Contador para uso genérico.
Dim mintCount As Object

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrString As String

'+ Auxiliar que contiene el número del elemento seleccionado de la colección.	
Dim mintAux As Object


'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insvalBranchQue: Se realizan las validaciones de las formas
'--------------------------------------------------------------------------------------------
Function insvalBranchQue() As String
	Dim etdDoube As Object
	'--------------------------------------------------------------------------------------------
	Dim lstrPerType As Object
	Dim lstrInforType As Object
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ BVC001: Consulta de Base de datos de vehiculos
		Case "BVC001"
			mobjBranchQue = New ePolicy.Auto_db
			With Request
				If CDbl(.QueryString.Item("nZone")) = 2 Then
					'				Response.Write "<NOTSCRIPT>alert('Chassis..." & .Form("tctChassis") & " Motor..." & .Form("tctMotor") & " RegistType..." & .Form("cboRegistType") & " Regist..." & .Form("tcnRegist") & " Cliente..." & .Form("tcnClient") & " Cod.Vehiculo..." & .Form("tctVehcode") & " Descript..." & .Form("tctDescript") & " Modelo..." & .Form("tctVehmodel") & " Color..." & .Form("tctColor") & " Year..." & mobjValues.StringToType(.Form("tcnYear"),eFunctions.Values.eTypeData.etdDouble) & " Status..." & .Form("cbonVestatus") & " Status2..." & mobjValues.StringToType(.Form("cbonVestatus"),eFunctions.Values.eTypeData.etdDouble) & " Accion..." & mobjValues.StringToType(Request.QueryString("nMainAction"),eFunctions.Values.eTypeData.etdDouble) & "')</" & "Script>"
					insvalBranchQue = mobjBranchQue.insValBVC001("BVC001", .Form.Item("tctChassis"), .Form.Item("tctMotor"), .Form.Item("cboDescLyctype"), .Form.Item("tcnRegist"), .Form.Item("tcnClient"), .Form.Item("cboVehCode"), .Form.Item("cboDescBrand"), .Form.Item("tctVehmodel"), .Form.Item("tctColor"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbonVestatus"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), etdDoube))
					'Response.Write "<NOTSCRIPT>alert('Val..." & insvalBranchQue & "')</" & "Script>"
				End If
			End With
			
		Case Else
			insvalBranchQue = "insvalBranchQue: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostBranchQue: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostBranchQue() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		'+ BVC001: Consulta de Base de datos de vehiculos
		Case "BVC001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 2 Then
					'Response.Write(mobjBranchQue.sCodition)
					Session("SQL") = mobjBranchQue.sCodition
				End If
				lblnPost = True
				
			End With
	End Select
	insPostBranchQue = lblnPost
End Function

</script>
<%Response.Expires = 0
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
		
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
function CancelErrors(){self.history.go(-1)}
function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%mstrCommand = "&sModule=Client&sProject=Client&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalBranchQue
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """);")
		.Write("self.history.go(-1)")
		.Write("</SCRIPT>")
	End With
Else
	If insPostBranchQue() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("sCodispl") <> "OP004" Then
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "BVC001"
					Response.Write("<SCRIPT>opener.document.location.href='BVC001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sql=" & Server.URLEncode(Session("Sql")) & "'</SCRIPT>")
			End Select
		End If
	End If
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjBranchQue may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjBranchQue = Nothing
%>
</BODY>
</HTML>




