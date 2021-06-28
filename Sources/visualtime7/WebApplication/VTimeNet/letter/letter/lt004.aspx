<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues



'**% insDefineHeader: This function allows to load the fields of the header
'%   insDefineHeader: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
	mobjGrid.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "LT004"
	
	With mobjGrid
		If CStr(Session("sClient")) <> vbNullString Then
			.Columns.AddNumericColumn(7264,"Solicitud", "nLettRequest", 4, CStr(0),  ,"Número que identifica la solicitud de envío a ser procesada",  ,  ,  ,  ,  , True)
			.Columns("nLettRequest").EditRecord = True
		Else
			.Columns.AddClientColumn(7265,"Cliente", "sClient", vbNullString,  ,"Código  que identifica la cliente en el sistema",  , True)
			.Columns("sClient").EditRecord = True
		End If
		
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
			.ActionQuery = True
		End If
		
		With .Columns
			.AddDateColumn(7266,"Fecha de solicitud", "dInpDate",  ,  ,"Fecha de la solicitud de envío de la correspondencia",  ,  ,  , True)
			.AddTextColumn(7267,"Modelo de Carta", "sDescript", 30, vbNullString,  ,"Descripción del Modelo de Carta asociado a la solicitud",  ,  ,  , True)
			.AddDateColumn(7268,"Fecha de impresión", "dPrintDate",  ,  ,"Fecha de impresión de la correspondencia",  ,  ,  , True)
			.AddDateColumn(7269,"Fecha de entrega", "dToHandOver",  ,  ,"Fecha de entrega de la correspondencia a su destinatario")
			.AddDateColumn(7270,"Fecha de respuesta", "dAnswerDate",  ,  ,"Fecha de respuesta del destinatario")
			.AddHiddenColumn("nTypeLetter", CStr(0))
			.AddHiddenColumn("nStatLetter", CStr(0))
			
			If Request.QueryString.Item("Type") = "PopUp" Then
				Call .AddButtonColumn(7271,"Nota", "SCA2-19", 0,  , False)
			Else
				Call .AddButtonColumn(7271,"Nota", "SCA2-19", 0,  , True)
			End If
			If Request.QueryString.Item("Type") = "PopUp" Then
				.AddFileColumn(7272,"Ubicación", "tAnswer")
			End If
		End With
		
		.Height = 400
		.Width = 450
		.Codispl = "LT004"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'-----------------------------------------------------------------------------------------------------------------------
Private Sub inspreLT004upd()
	'-----------------------------------------------------------------------------------------------------------------------
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valLetter.aspx", "LT004", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

'%inspreLT004: 
'-----------------------------------------------------------------------------------------------------------------------
Private Sub inspreLT004()
	'-----------------------------------------------------------------------------------------------------------------------
	Dim lcolLettAccuses As eLetter.LettAccuses
	Dim lclsLettAccuse As eLetter.LettAccuse
	
	lcolLettAccuses = New eLetter.LettAccuses
	lclsLettAccuse = New eLetter.LettAccuse
	
	'**% Define	the general properties of the grid
	'+ Se definen las propiedades generales del grid
	
	If CStr(Session("nLettRequest")) = vbNullString Then
		Session("nLettRequest") = 0
	End If
	
	If lcolLettAccuses.Find(mobjValues.StringToType(Session("nLettRequest"), eFunctions.Values.eTypeData.etdInteger), Session("sClient")) Then
		With mobjGrid
			For	Each lclsLettAccuse In lcolLettAccuses
				
				If CStr(Session("sClient")) <> vbNullString Then
					.Columns("nLettRequest").DefValue = CStr(lclsLettAccuse.nLettRequest)
				Else
					.Columns("sClient").DefValue = lclsLettAccuse.sClient
				End If
				.Columns("dInpdate").DefValue = CStr(lclsLettAccuse.oLettRequest.dInpDate)
				.Columns("sDescript").DefValue = lclsLettAccuse.sDescript
				.Columns("dPrintDate").DefValue = CStr(lclsLettAccuse.dPrintDate)
				.Columns("dToHandOver").DefValue = CStr(lclsLettAccuse.dToHandOver)
				.Columns("dAnswerDate").DefValue = CStr(lclsLettAccuse.dAnswerDate)
				.Columns("btnNotenum").nNoteNum = lclsLettAccuse.nNoteNum
				.Columns("nTypeLetter").DefValue = CStr(lclsLettAccuse.nTypeLetter)
				.Columns("nStatLetter").DefValue = CStr(lclsLettAccuse.nStatLetter)
				
				Response.Write(.DoRow)
			Next lclsLettAccuse
		End With
	End If
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
	
	lcolLettAccuses = Nothing
	lclsLettAccuse = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LT004")
%>

<HTML>
<HEAD>
<%
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "LT004"

Response.Write(mobjValues.StyleSheet())

mobjMenues = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
mobjMenues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "LT004", Request.QueryString.Item("sWindowDescript"), mobjValues.StringToType(Request.QueryString.Item("nWindowTy"), eFunctions.Values.eTypeData.etdInteger)))
End If
mobjMenues = Nothing
%>

<%="<SCRIPT LANGUAGE=""JavaScript""> "%>
        var nMainAction = 304;
</SCRIPT>
    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM method=post action="valLetter.aspx?Time=1" id=form1 name=form1 ENCTYPE="multipart/form-data">
<%
insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreLT004()
Else
	inspreLT004upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%If Request.QueryString.Item("Type") = "PopUp" Then%>
<script>self.document.forms[0].tcnNotenum.value = self.document.forms[0].btnNotenum.value </script>
<%End If%>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
Call mobjNetFrameWork.FinishPage("LT004")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








