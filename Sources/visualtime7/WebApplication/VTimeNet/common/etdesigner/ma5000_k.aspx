<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eJobs" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		.AddNumericColumn(0, "Código", "tcnJob", 10, "", True, "Número que identifica la tarea", False, 0,  ,  ,  , True)
		.AddDateColumn(0, "Fecha", "tcdNext_date",  , False, "Fecha de ejecución",  ,  ,  , False)
		.AddTextColumn(0, "Tarea", "tctWhat", 30, "", False, "Tarea a ejecutar",  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MA5000_k"
		.Top = 100
		.Height = 224
		.Width = 350
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = Request.QueryString.Item("nMainAction") = "401"
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnJob").EditRecord = True
		.sDelRecordParam = "nJob='+ marrArray[lintIndex].tcnJob + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMA5000: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMA5000()
	'--------------------------------------------------------------------------------------------
	Dim lcolUser_jobss As eJobs.User_jobss
	Dim lclsUser_jobs As Object
	lcolUser_jobss = New eJobs.User_jobss
	With mobjGrid
		If lcolUser_jobss.Find() Then
			For	Each lclsUser_jobs In lcolUser_jobss
				.Columns("tcnJob").DefValue = lclsUser_jobs.nJob
				.Columns("tcdNext_date").DefValue = lclsUser_jobs.dNext_date
				.Columns("tctWhat").DefValue = lclsUser_jobs.sWhat
				Response.Write(mobjGrid.DoRow())
			Next lclsUser_jobs
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	'UPGRADE_NOTE: Object lclsUser_jobs may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsUser_jobs = Nothing
	'UPGRADE_NOTE: Object lcolUser_jobss may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolUser_jobss = Nothing
End Sub

'% insPreMA5000Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMA5000Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsUser_jobs As eJobs.User_jobs
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsUser_jobs = New eJobs.User_jobs
			If Session("nusercode") = 1807 Then
				Response.Write("<SCRIPT>")
				Response.Write("alert(""" & mobjValues.StringToType(.QueryString.Item("nJob"), eFunctions.Values.eTypeData.etdDouble) & """);")
				Response.Write("</" & "Script>")
			End If
			Call lclsUser_jobs.InsPostMA5000(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nJob"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, vbNullString)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMA500_K.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	'UPGRADE_NOTE: Object lclsUser_jobs may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsUser_jobs = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
%>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->


<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    switch (llngAction){
        case 302:
        case 305:
        case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction            
            break;
    }
}
</SCRIPT> 
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
End If
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MA5000_k.aspx", 1, ""))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
	End If
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmMA5000" ACTION="valMA500_K.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMA5000()
Else
	Call insPreMA5000Upd()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




