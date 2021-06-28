<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

Dim lobjErrors As eGeneral.GeneralFunction
Dim lstrAlert As String



'%insDefineHeader: Se definen la estructura del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Action") = "Update" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRelashipColumnCaption"), "cbeRelaship", "Table15", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeRelashipToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRel_targetColumnCaption"), "cbeRel_target", "Table15", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeRel_targetToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRelashipColumnCaption"), "cbeRelaship", "Table15", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRelashipToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRel_targetColumnCaption"), "cbeRel_target", "Table15", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRel_targetColumnCaption"))
		End If
		Call .AddPossiblesColumn(0,  GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnCaption"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MBC003_K"
		.sCodisplPage = "MBC003"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("cbeRelaship").EditRecord = True
			.Columns("Sel").Title = "Sel"
		End If
		.sDelRecordParam = "nRelaship='+ marrArray[lintIndex].cbeRelaship + '"
		.Height = 230
		.Width = 310
		.Top = 100
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMBC003: Carga los datos de la grilla
'------------------------------------------------------------------------------
Private Sub insPreMBC003()
	'------------------------------------------------------------------------------
	'- Datos de las tablas relacionadas
	Dim lcolTab_relats As eClient.Tab_relats
	Dim lclsTab_relat As Object
	'- Indicador del item en proceso
	Dim lstrinderr As Byte
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% insCancel: Proceso boton de cancelar" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCancel(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    return true" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insStateZone: habilita los controles correspondientes" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insStateZone(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("//+ Aunque no existen controles que habilitar, " & vbCrLf)
Response.Write("//+ se crea sin codigo por ser una funcion generica invocada desde el menu" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insPreZone: Define ubicacion de documento" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	lstrinderr = 0
	
	With Server
		lcolTab_relats = New eClient.Tab_relats
	End With
	
	If lcolTab_relats.Find Then
		For	Each lclsTab_relat In lcolTab_relats
			With mobjGrid
				.Columns("cbeRelaship").DefValue = lclsTab_relat.nRelaship
				.Columns("cbeRel_target").DefValue = lclsTab_relat.nRel_target
				.Columns("cbeStatregt").BlankPosition = False
				.Columns("cbeStatregt").DefValue = lclsTab_relat.sStatregt
				If lclsTab_relat.sExist = "1" Then
					lstrinderr = 1
				Else
					lstrinderr = 2
				End If
				
				.Columns("Sel").OnClick = "InsChangeSel(this," & lstrinderr & ")"
				
			End With
			
			'+Se crea linea de grilla
			Response.Write(mobjGrid.DoRow())
		Next lclsTab_relat
	End If
	Response.Write(mobjGrid.closeTable())
	lcolTab_relats = Nothing
	lclsTab_relat = Nothing
End Sub

'% insPreMBC003Upd: Despliega pagina de edicion de grilla
'------------------------------------------------------------------------------
Private Sub insPreMBC003Upd()
	'------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lclsTab_relat As eClient.Tab_relat
	
	lclsTab_relat = New eClient.Tab_relat
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		With lclsTab_relat
			.nRelaship = CInt(Request.QueryString.Item("nRelaship"))
			.Delete()
		End With
	End If
	
	lclsTab_relat = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClient.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

lobjErrors = New eGeneral.GeneralFunction
lstrAlert = "Err. 2206 " & lobjErrors.insLoadMessage(2206)
lobjErrors = Nothing

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MBC003"
%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"

//%InsChangeSel:Desmarca item seleccionado
//-----------------------------------------------------
function InsChangeSel(Field, sInd){
//-----------------------------------------------------
	if (Field.checked && (sInd == "1")) {
    	Field.checked = false
    	alert('<%=lstrAlert%>');
	}
}
</SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MBC003_K.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR></BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmTabRelations" ACTION="valMantClient.aspx?mode=1">
 <%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMBC003()
Else
	Call insPreMBC003Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





