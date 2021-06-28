<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mlngNotenum As String
Dim mstrUserName As String
Dim mintRectype As String
Dim mlngIndexNotenum As Object


'% insPreSI021: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSIL961()
	'--------------------------------------------------------------------------------------------
	Dim lclsObject As Object
	
	Call insDefineHeader()
	
	If Request.QueryString("Type") <> "PopUp" Then
		Call insreaNotes()
	Else
		Call insreaNotesUpd()
	End If
End Sub

'% insDefineHeader : Configura las columnas del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------   
	mobjGrid.sCodisplPage = "SIL961"
	
	'+ Si la acción es consulta no se establece la propiedad ActionQuery sobre el objeto del
	'+ grid con la variable de sesión bquery, ya que es necesario que aparezcan los links
	'+ sobre las notas para lograr acceder a su descripción.
	If Not Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
		mobjGrid.ActionQuery = Session("bQuery")
	End If
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddNumericColumn(19653, "Número de nota", "tcnNotenum", 4, mlngNotenum,  , "Número de la nota",  ,  ,  ,  ,  , True)
		.AddNumericColumn(40558, "Consecutivo", "tcnConsec", 4, "",  , "Número consecutivo de la nota generado en forma automática",  ,  ,  ,  ,  , True)
		.AddTextColumn(40560, "Descripción", "tctDescript", 60, "",  , "Breve descripción del contenido de la nota")
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		.AddDateColumn(40562, "Fecha creación", "tcdCompdate", mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate),  , "Fecha en que se crea la nota",  ,  ,  , True)
		.AddDateColumn(40563, "Fecha límite", "tcdNulldate",  ,  , "Fecha hasta la cual se conserva la nota en el sistema")
		.AddNumericColumn(40559, "Usuario", "nUsercode", 4, CStr(Session("nUsercode")),  , "Código del usuario que registra o actualiza la nota",  ,  ,  ,  ,  , True)
		.AddTextColumn(40561, "Nombre", "sCliename", 40, mstrUserName,  , "Nombre del usuario que registra o actualiza la nota",  ,  ,  , True)
		.AddTextAreaColumn(19655, "Detalle", "tcttDs_text", "", 11, 63)
		.AddHiddenColumn("nRectype", mintRectype)
		.AddHiddenColumn("sCodispl", Request.QueryString("sCodispl"))
		.AddHiddenColumn("sOnSeq", Request.QueryString("sOnSeq"))
		.AddHiddenColumn("nClause", Request.QueryString("nClause"))
		.AddHiddenColumn("nID", Request.QueryString("nID"))
		.AddHiddenColumn("sLicense_ty", Request.QueryString("sLicense_ty"))
		.AddHiddenColumn("sRegist", Request.QueryString("sRegist"))
		If LCase(Request.QueryString("Type")) <> "popup" Then
			mobjGrid.Columns("tcnNotenum").GridVisible = False
			mobjGrid.Columns("tcttDs_text").GridVisible = False
		Else
			mobjGrid.Columns("tcttDs_text").GridVisible = True
			mobjGrid.Columns("tcttDs_text").Disabled = True
			mobjGrid.Columns("tctDescript").Disabled = True
		End If
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString("sCodispl")
		.Codisp = "SIL009"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.bCheckVisible = False
		
		.nMainAction = Request.QueryString("nMainAction")
		.Columns("tctDescript").EditRecord = True
		
		'+ Tamaño de la ventana popup
		.Width = 650
		.Height = 550
		.Top = 5
		
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		.EditRecordQuery = mobjGrid.ActionQuery
	End With
End Sub
'% insreaNotes: Lee las notas asociadas a un ente
'----------------------------------------------------------------------------
Private Sub insreaNotes()
	'--------------------------------------------------------------------------------------------
	'- Variable para almacenar modo de accion mientras se cambia temporalmente
	Dim lblnQuery As Boolean
	'- Clase y coleccion para manejo de notas
	Dim lclsNote As eGeneralForm.Notes
	Dim lcolNotes As eGeneralForm.Notess
	
	lcolNotes = New eGeneralForm.Notess
	
	'+ Se almacena el modo de ejecucion actual, para permitir edicion
	lblnQuery = mobjValues.ActionQuery
	mobjValues.ActionQuery = True
	
	With mobjGrid
		If CStr(Session("sOriginalForm")) <> vbNullString Then
			.AddButton = False
			.DeleteButton = False
			.ActionQuery = False
		End If
	End With
	'+ Busca la nota y los consecutivos
	If lcolNotes.Find(mobjValues.StringToType(CStr(Session("nNotenum")), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write(mobjValues.HiddenControl("hddCountNote", CStr(lcolNotes.Count)))
		If lcolNotes.Count > 0 Then
			
			For	Each lclsNote In lcolNotes
				With mobjGrid
					.Columns("tcnConsec").DefValue = CStr(lclsNote.nConsec)
					.Columns("tcnNotenum").DefValue = CStr(lclsNote.nNotenum)
					.Columns("tctDescript").DefValue = lclsNote.sDescript
					.Columns("tcdCompdate").DefValue = CStr(lclsNote.dCompdate)
					.Columns("tcdNulldate").DefValue = CStr(lclsNote.dNulldate)
					.Columns("nUsercode").DefValue = CStr(lclsNote.nUsercode)
					.Columns("sCliename").DefValue = lclsNote.sCliename
					.Columns("tcttDs_text").DefValue = lclsNote.tDs_text
					'+ Columnas ocultas                    
					.Columns("nRectype").DefValue = CStr(lclsNote.nRectype)
					.Columns("sCodispl").DefValue = Request.QueryString("sCodispl")
					.Columns("sOnSeq").DefValue = Request.QueryString("sOnSeq")
					
					Response.Write(.DoRow)
				End With
			Next lclsNote
		End If
		Response.Write(mobjValues.HiddenControl("hddNoteNum", mlngNotenum))
	Else
		Response.Write(mobjValues.HiddenControl("hddNoteNum", "0"))
		Response.Write(mobjValues.HiddenControl("hddCountNote", "0"))
	End If
	Response.Write(mobjGrid.closeTable)
	
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	
	'+ Se retorna a modo ejecucion original    
	mobjValues.ActionQuery = lblnQuery
	
	'UPGRADE_NOTE: Object lcolNotes may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolNotes = Nothing
	'UPGRADE_NOTE: Object lclsNote may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsNote = Nothing
End Sub
'% insreaNotesUpd : Permite realizar las actualizaciones sobre la nota en selección
'-------------------------------------------------------------------------------------------
Private Sub insreaNotesUpd()
	'-------------------------------------------------------------------------------------------
	'- Variables para menejo de clases    
	Dim lclsGeneralNotes As eGeneralForm.GeneralForm
	Dim lcolNotes As Object
	
	lclsGeneralNotes = New eGeneralForm.GeneralForm
	
	'+ Borrar nota
	
	'UPGRADE_NOTE: Object lclsGeneralNotes may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsGeneralNotes = Nothing
	'UPGRADE_NOTE: Object lcolNotes may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolNotes = Nothing
	
	'+ Actualizar nota
	If Request.QueryString("Action") = "Update" Then
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valGeneralForm.aspx", Request.QueryString("sCodispl"), Request.QueryString("nMainAction"), True, Request.QueryString("Index")))
	End If
	
	'+ Según la acción se actualizan los valores de la página luego de diseñada.
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "SIL961"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 10/12/03 17:28 $|$$Author: $"

</SCRIPT>
<%
With Response
	If Request.QueryString("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "SIL961", "SIL961.aspx"))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SIL961"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIL961" ACTION="ValClaimrep.aspx?x=1&nTransacio=SIL961&sOriginalForm=<%=Session("sOriginalForm")%>">
<BR>
<%=mobjValues.ShowWindowsName("SIL961", Request.QueryString("sWindowDescript"))%>
<BR><BR>
<%
Call insPreSIL961()

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

   






