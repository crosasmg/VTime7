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
	
	mobjGrid.sCodisplPage = "MGI1410"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		.AddPossiblesColumn(0, GetLocalResourceObject("valSheet_ChildColumnCaption"), "valSheet_Child", "TABTABLEMASTERSHEET", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valSheet_ChildColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "MGI1410"
		.Left = 200
		.Width = 570
		.Height = 230
		
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		
		.sDelRecordParam = "nSheet_Child=' + marrArray[lintIndex].valSheet_Child + '"
		
		.Columns("valSheet_Child").Parameters.Add("NINTERTYPE", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valSheet_Child").Parameters.Add("NSYSTEM", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMGI1410_K: Esta función se encarga de cargar los datos en la forma "Folder" de la SEC1
'------------------------------------------------------------------------------
Private Sub insPreMGI1410_K()
	'------------------------------------------------------------------------------
	Dim lcolDepend_Sheet As eInterface.Depend_Sheets
	Dim lclsDepend_Sheet As eInterface.Depend_Sheet
	Dim lblnFind As Object
	
	lcolDepend_Sheet = New eInterface.Depend_Sheets
	lclsDepend_Sheet = New eInterface.Depend_Sheet
	
	If lcolDepend_Sheet.Find(Session("nSheet")) Then
		For	Each lclsDepend_Sheet In lcolDepend_Sheet
			With mobjGrid
				.Columns("valSheet_Child").DefValue = CStr(lclsDepend_Sheet.nSheet_Child)
				.Columns("valSheet_Child").Descript = lclsDepend_Sheet.sSheet_Child
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsDepend_Sheet
	End If
	Response.Write(mobjGrid.closeTable())
	lcolDepend_Sheet = Nothing
	lclsDepend_Sheet = Nothing
End Sub

'% insPreMGI1410Upd. Se define esta función para contruir el contenido de la ventana UPD
'---------------------------------------------------------------------------------------
Private Sub insPreMGI1410_K_Upd()
	'---------------------------------------------------------------------------------------
	Dim lclsDepend_Sheet As eInterface.Depend_Sheet
	lclsDepend_Sheet = New eInterface.Depend_Sheet
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		Call lclsDepend_Sheet.insPostMGI1410Upd("Del", mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nSheet_Child"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
	End If
	
	lclsDepend_Sheet = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valmantinterfaceseq.aspx", "MGI1410", Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGI1410"
%>
<html>
<head>
   <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	 <%=mobjValues.WindowsTitle("MGI1410")%>
	
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>




    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MGI1410", "MGI1410.aspx"))
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

<form METHOD="POST" ID="FORM" NAME="MGI1410" ACTION="valmantinterfaceseq.aspx?Type=<%=Request.QueryString.Item("Type")%>">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MGI1410"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMGI1410_K()
Else
	Call insPreMGI1410_K_Upd()
End If
mobjValues = Nothing
%>
</form>
</body>
</html>




