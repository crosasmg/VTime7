<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader. Definición de columnas del GRID
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lobjhomolog_table As Object
	
	mobjGrid = New eFunctions.Grid
	
	'EFR aqui voy estoy modificando la DEFINICION DE LAS COLUMNAS DE LA GRID DE DETALLE.
	
	mobjGrid.sCodisplPage = "MGI1406"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tcstableColumnCaption"), "tcstable", 40, vbNullString,  , GetLocalResourceObject("tcstableColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcsaliasColumnCaption"), "tcsalias", 5, vbNullString,  , GetLocalResourceObject("tcsaliasColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnorderColumnCaption"), "tcnorder", 5, "", True, GetLocalResourceObject("tcnorderColumnToolTip"), False, 0)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "MGI1406"
		.Left = 200
		.Width = 570
		.Height = 230
		
		If Request.QueryString.Item("Action") <> "Update" And Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tcstable").EditRecord = True
		End If
		
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		
		.sDelRecordParam = "sTable=' + marrArray[lintIndex].tcstable + '"
		
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("tcstable").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMGI1406_K: Esta función se encarga de cargar los datos en la forma "Folder" de la SEC1
'------------------------------------------------------------------------------
Private Sub insPreMGI1406_K()
	'------------------------------------------------------------------------------
	Dim lcoltablesheet As eInterface.TableSheets
	Dim lclstablesheet As eInterface.tablesheet
	Dim lblnFind As Object
	
	lcoltablesheet = New eInterface.TableSheets
	lclstablesheet = New eInterface.tablesheet
	
	If lcoltablesheet.Find(session("nSheet")) Then
		For	Each lclstablesheet In lcoltablesheet
			With mobjGrid
				.Columns("tcstable").DefValue = lclstablesheet.sTable
				.Columns("tcsalias").DefValue = lclstablesheet.sAlias
				.Columns("tcnorder").DefValue = CStr(lclstablesheet.nOrder)
				
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclstablesheet
	End If
	Response.Write(mobjGrid.closeTable())
	lcoltablesheet = Nothing
	lclstablesheet = Nothing
End Sub

'% insPreMGI1406Upd. Se define esta función para contruir el contenido de la ventana UPD
'---------------------------------------------------------------------------------------
Private Sub insPreMGI1406_K_Upd()
	'---------------------------------------------------------------------------------------
	Dim lclstablesheet As eInterface.tablesheet
	lclstablesheet = New eInterface.tablesheet
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		Call lclstablesheet.insPostMGI1406("Del", mobjValues.StringToType(session("nSheet"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sTable"), CStr(eRemoteDB.Constants.strnull), eRemoteDB.Constants.intNull, session("nUsercode"))
	End If
	
	lclstablesheet = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valmantinterfaceseq.aspx", "MGI1406", Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGI1406"
%>
<html>
<head>
   <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	 <%=mobjValues.WindowsTitle("MGI1406")%>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>



    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MGI1406", "MGI1406.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<script>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 31/10/03 17:16 $"
 
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}

//-------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
	
}
</script>		

</head>
<body ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>

<form METHOD="POST" ID="FORM" NAME="MGI1406" ACTION="valmantinterfaceseq.aspx?Type=<%=Request.QueryString.Item("Type")%>">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MGI1406"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMGI1406_K()
Else
	Call insPreMGI1406_K_Upd()
End If
mobjValues = Nothing
%>
</form>
</body>
</html>




