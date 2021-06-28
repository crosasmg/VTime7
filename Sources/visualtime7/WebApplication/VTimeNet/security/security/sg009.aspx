<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues


'%insDefineHeader(). Este procedimiento se encarga de definir las líneas del encabezado
'%del grid.
'---------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------
	Dim lobjColumn As Object
	
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "SG009"
	
	'+Se definen todas las columnas del Grid.
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SG009"
	End With
	
	With mobjGrid.Columns
		Call .AddTextColumn(100439, GetLocalResourceObject("sHour_StartColumnCaption"), "sHour_Start", 5, "",  , GetLocalResourceObject("sHour_StartColumnToolTip"),  ,  , "ValidateHourFormat(this.value)")
		Call .AddTextColumn(100439, GetLocalResourceObject("sHour_EndColumnCaption"), "sHour_End", 5, "",  , GetLocalResourceObject("sHour_EndColumnToolTip"),  ,  , "ValidateHourFormat(this.value)")
	End With
	
	With mobjGrid
		.Columns("sHour_start").Disabled = (Request.QueryString.Item("Action") = "Update")
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
		End If
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
			.ActionQuery = True
		Else
			.ActionQuery = False
		End If
		
		
		.Columns("sHour_End").EditRecord = True
		
		.sDelRecordParam = "sHour_Start=' + marrArray[lintIndex].sHour_Start + '"
		
		'+ Permite continuar si el check está marcado        
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Height = 200
		.Width = 350
	End With
End Sub

'%insCO008Upd. Esta ventana se encarga de mostrar el código correspondiente a la
'% ventana PopUp.
'---------------------------------------------------------------------------------------
Private Sub insPreSG009Upd()
	'---------------------------------------------------------------------------------------
	With Response
		If Request.QueryString.Item("Action") = "Del" Then
			insDelItem()
			Response.Write(mobjValues.ConfirmDelete())
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Security/Security/Sequence.aspx?nAction=0" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & "SG009" & "&sCodispl=" & "SG009" & """;</" & "Script>")
		End If
		
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValSecuritySeq.aspx", "SG009", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	End With
End Sub

'%insPreSG009: Esta ventana se encarga de mostrar en el grid los valores leídos.
'---------------------------------------------------------------------------------------
Private Sub insPreSG009()
	'---------------------------------------------------------------------------------------
	Dim lclsWindows As Object
	Dim lcolWindowss As eSecurity.Windowss
	
	lcolWindowss = New eSecurity.Windowss
	
	If lcolWindowss.FindWin_hour(Session("sCodispLog"), True) Then
		For	Each lclsWindows In lcolWindowss
			With mobjGrid
				.Columns("sHour_Start").DefValue = lclsWindows.sHour_Start
				.Columns("sHour_End").DefValue = lclsWindows.sHour_End
				
				Response.Write(.doRow)
			End With
		Next lclsWindows
	End If
	
	lclsWindows = Nothing
	lcolWindowss = Nothing
	
	Response.Write(mobjGrid.CloseTable())
End Sub

'%insDelItem
'------------------------------------------------------------------------------------------
Public Sub insDelItem()
	'------------------------------------------------------------------------------------------
	Dim lclsWindows As eSecurity.Windows
	
	lclsWindows = New eSecurity.Windows
	
	If lclsWindows.DeleteWin_Hour(Session("sCodispLog"), Request.QueryString.Item("sHour_Start")) Then
	End If
	
	lclsWindows = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG009"
%>
<HTML>
<HEAD>
<SCRIPT>
//-------------------------------------------------------------------------------------------
function ValidateHourFormat(sValue)
//-------------------------------------------------------------------------------------------
{
	var lintCount = 0;
	var lstrString = "";

	if(sValue.substr(0, 1)!=1 && sValue.substr(0, 1)!=2 && sValue.substr(0, 1)!=0)
	{
		alert('Caracter inválido');
		self.document.forms[0].elements["sHour_Start"].value = "";
		self.document.forms[0].elements["sHour_End"].value = "";
	}
}
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:05 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    




	
<%
mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "SG009", "SG009.aspx"))
End If

With Response
	.Write(mobjValues.WindowsTitle("SG009"))
	.Write(mobjValues.StyleSheet())
End With
%>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SG009" ACTION="ValSecuritySeq.aspx?Time=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

   <%Response.Write(mobjValues.ShowWindowsName("SG009"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreSG009Upd()
Else
	Call insPreSG009()
End If
%>      
</FORM>
</BODY>
</HTML>






