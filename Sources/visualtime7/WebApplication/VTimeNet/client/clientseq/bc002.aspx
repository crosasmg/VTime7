<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objetos genericos de valores, menu y grilla    
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader: Se define la estructura del grid
'------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	With mobjGrid.Columns
		.AddClientColumn(40381, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", vbNullString,  , GetLocalResourceObject("tctClientColumnToolTip"),  ,  , "tctClieName")
		.AddPossiblesColumn(40380, GetLocalResourceObject("cbeRelationshipColumnCaption"), "cbeRelationship", "Table15", 1,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRelationshipColumnToolTip"))
		.AddHiddenColumn("nOriginalRelaship", vbNullString)
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Height = 200
		.Width = 600
		.ActionQuery = Session("bQuery")
		
		.Codispl = "BC002"
		.Columns("Sel").GridVisible = True
		
		mobjGrid.sDelRecordParam = "sClientr='+ marrArray[lintIndex].tctClient + " & "'&nRelaship=' + marrArray[lintIndex].nOriginalRelaship + '"
		'+ Permite continuar si el check está marcado        
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Function

'%InsPreBC002 : Lee los nexos que corresponden a un cliente en particular.
'--------------------------------------------------------------------------------------------
Private Sub insPreBC002()
	'--------------------------------------------------------------------------------------------
	'- Objetos con datos de relaciones 
	Dim lcolRelations As eClient.Relations
	Dim lobjRelation As Object
	
	lcolRelations = New eClient.Relations
	
	With mobjGrid
		'+ Se buscan las relaciones del cliente
		If lcolRelations.Find(Session("sClient")) Then
			If lcolRelations.count > 0 Then
				'+ Si la secuencia fue invocada desde otra transaccion (variable de sesión "sOriginalForm") 
				'+ y existen registros en el Grid, entonces se esconde el botón de agregar y se inhabilita el Grid
				If CStr(Session("sOriginalForm")) <> vbNullString Then
					.AddButton = False
					.DeleteButton = False
					.ActionQuery = True
				End If
				
				For	Each lobjRelation In lcolRelations
					.Columns("tctClient").DefValue = lobjRelation.sClientr
					.Columns("cbeRelationship").DefValue = lobjRelation.nRelaship
					.Columns("nOriginalRelaship").DefValue = lobjRelation.nRelaship
					Response.Write(.DoRow)
				Next lobjRelation
			End If
		End If
		
		Response.Write(.closeTable)
		
	End With
	
	lcolRelations = Nothing
	lobjRelation = Nothing
End Sub

'%inspreBC002Upd: Se efectúa las acciones de la ventana PopUp
'------------------------------------------------------------------------
Private Function inspreBC002Upd() As Object
	'------------------------------------------------------------------------
	'- Objeto de cliente
	Dim lclsClientSeq As eClient.ClientSeq
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsClientSeq = New eClient.ClientSeq
		
		Response.Write(mobjValues.ConfirmDelete())
		
		With Request
			Call lclsClientSeq.insPostBC002("Delete", Session("sClient"), .QueryString.Item("sClientr"), .QueryString.Item("sClieName"), .QueryString.Item("nRelaship"), Session("nUserCode"), .QueryString.Item("nRelaship"))
		End With
		lclsClientSeq = Nothing
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValClientSeq.aspx", "BC002", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	
	lclsClientSeq = Nothing
	
End Function

</script>
<%Response.Expires = 0

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
%> 
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
    <%=mobjValues.StyleSheet()%>
    <TITLE>Nexos del cliente</TITLE>

<SCRIPT>
//- Tipo de accion principal
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;

//+ Variable para el control de versiones
	document.VssVersion="$$Author: Iusr_llanquihue $|$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();"> 
<%If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "BC002", "BC002.aspx"))
End If
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="POST" NAME="frmBC002" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreBC002()
Else
	inspreBC002Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>
</TABLE>
</FORM>
</BODY>
</HTML>





