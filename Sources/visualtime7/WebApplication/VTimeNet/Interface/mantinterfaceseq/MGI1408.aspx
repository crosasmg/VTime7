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
	Dim lobjCalend As eInterface.calend
	
	mobjGrid = New eFunctions.Grid
	
	
	mobjGrid.sCodisplPage = "MGI1408"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnidColumnCaption"), "tcnid", 5, "", True, GetLocalResourceObject("tcnidColumnToolTip"), False, 0,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcndayColumnCaption"), "tcnday", 5, "", True, GetLocalResourceObject("tcndayColumnToolTip"), False, 0,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("ddateprocColumnCaption"), "ddateproc", "",  , GetLocalResourceObject("ddateprocColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tcshourColumnCaption"), "tcshour", 5, vbNullString,  , GetLocalResourceObject("tcshourColumnToolTip"),  ,  ,  , True)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "MGI1408"
		.Left = 200
		.Width = 570
		.Height = 230
		
		If Request.QueryString.Item("Action") <> "Update" And Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tcnid").EditRecord = True
		End If
		'+ Si la interfaz no es automatica, no se define calendario.
		If CStr(session("nperiod")) = "" Then
			.AddButton = False
		End If
		
		'+ Si es popup e ingreso, trae correlativo automatico por nSheet
		If Request.QueryString.Item("Type") = "PopUp" Then
			lobjCalend = New eInterface.calend
			.Columns("tcnId").DefValue = CStr(lobjCalend.InsCalIdCalend(session("nsheet")))
			lobjCalend = Nothing
			
			'+ Si es PopUp reviso periodicidad para habilitar campos correspondiente.
			If CStr(session("nperiod")) = "1" Or CStr(session("nperiod")) = "3" Then
				.Columns("tcshour").Disabled = False
			ElseIf CStr(session("nperiod")) = "2" Then 
				.Columns("tcnday").Disabled = False
				.Columns("tcshour").Disabled = False
			ElseIf CStr(session("nperiod")) = "4" Then 
				.Columns("ddateproc").Disabled = False
				.Columns("tcshour").Disabled = False
			End If
		End If
		
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nId=' + marrArray[lintIndex].tcnid + '"
		
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("tcnId").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMGI1408_K: Esta función se encarga de cargar los datos en la forma "Folder" de la SEC3
'------------------------------------------------------------------------------
Private Sub insPreMGI1408_K()
	'------------------------------------------------------------------------------
	Dim lcolcalend As eInterface.Calends
	Dim lclscalend As eInterface.calend
	Dim lblnFind As Object
	
	lcolcalend = New eInterface.Calends
	lclscalend = New eInterface.calend
	
	If lcolcalend.Find(session("nSheet")) Then
		For	Each lclscalend In lcolcalend
			With mobjGrid
				.Columns("tcnid").DefValue = CStr(lclscalend.nId)
				.Columns("ddateproc").DefValue = CStr(lclscalend.dDateProc)
				.Columns("tcnday").DefValue = CStr(lclscalend.nDay)
				.Columns("tcshour").DefValue = lclscalend.sHour
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclscalend
	End If
	Response.Write(mobjGrid.closeTable())
	lcolcalend = Nothing
	lclscalend = Nothing
End Sub

'% insPreMGI1408Upd. Se define esta función para contruir el contenido de la ventana UPD
'---------------------------------------------------------------------------------------
Private Sub insPreMGI1408_K_Upd()
	'---------------------------------------------------------------------------------------
	Dim lclscalend As eInterface.calend
	lclscalend = New eInterface.calend
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		Call lclscalend.insPostMGI1408("Del", mobjValues.StringToType(session("nSheet"), eFunctions.Values.eTypeData.etdDouble), CInt(Request.QueryString.Item("nid")), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strNull), session("nUsercode"))
	End If
	
	lclscalend = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valmantinterfaceseq.aspx", "MGI1408", Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGI1408"
%>
<html>
<head>
   <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	 <%=mobjValues.WindowsTitle("MGI1408")%>
	
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>



    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MGI1408", "MGI1408.aspx"))
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

<form METHOD="POST" ID="FORM" NAME="MGI1408" ACTION="valmantinterfaceseq.aspx?Type=<%=Request.QueryString.Item("Type")%>">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MGI1408"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMGI1408_K()
Else
	Call insPreMGI1408_K_Upd()
End If
mobjValues = Nothing
%>
</form>
</body>
</html>




