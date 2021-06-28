<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
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
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "GI1404"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tcsNameColumnCaption"), "tcsName", 5, vbNullString,  , GetLocalResourceObject("tcsNameColumnCaption"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsValueColumnCaption"), "tcsValue", 5, vbNullString,  , GetLocalResourceObject("tcsValueColumnCaption"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsHomologColumnCaption"), "tcsHomolog", 5, vbNullString,  , GetLocalResourceObject("tcsHomologColumnCaption"),  ,  ,  , False)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "GI1404"
		.Left = 200
		.Width = 570
		.Height = 230
		
		If Request.QueryString.Item("Action") <> "Update" And Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tcsName").EditRecord = True
		End If
		
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		'.sDelRecordParam = "nId=' + marrArray[lintIndex].tcnid + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreGI1404: Esta función se encarga de cargar los datos en la forma "Folder" de la 
'------------------------------------------------------------------------------
Private Sub insPreGI1404()
	'------------------------------------------------------------------------------
	
End Sub

'% insPreGI1404Upd. Se define esta función para contruir el contenido de la ventana UPD
'---------------------------------------------------------------------------------------
Private Sub insPreGI1404_Upd()
	'---------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valinterfaceseq.aspx", "GI1404", Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "GI1404"
%>
<html>
<head>
   <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	 <%=mobjValues.WindowsTitle("GI1404")%>
	
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>



    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "GI1404", "GI1404.aspx"))
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

<form METHOD="POST" ID="FORM" NAME="GI1404" ACTION="valinterfaceseq.aspx?">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("GI1404"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreGI1404()
Else
	Call insPreGI1404_Upd()
End If
mobjValues = Nothing
%>
</form>
</body>
</html>




