<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(40590, "Código del cobrador", "tcnCollector", 10, CStr(eRemoteDB.Constants.intNull),  , "Identificador definido por el sistema o el usuario",  ,  ,  ,  ,  , True)
		Call .AddClientColumn(40590, "RUT del cobrador", "dtcClient", "",  , "RUT del cobrador",  , True)
		Call .AddDateColumn(40590, "Fecha de ingreso", "dtInputDate",  , False, "Fecha de inngreso del cobrador")
		Call .AddPossiblesColumn(40590, "Tipo de cobrador", "tcnColType", "Table5551", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , "Tipo de cobrador (Telecobrador|Cobrador)")
		Call .AddPossiblesColumn(40590, "Tipo de contrato", "tcnConType", "Table5557", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , "Modalidad en que es contratado el cobrador.")
		Call .AddPossiblesColumn(40590, "Área de seguro", "tcnInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , "Área al que se asiganará el cobrador")
		
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Columns("tcnCollector").EditRecord = True
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "CO685"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sDelRecordParam = "nCollector='+ marrArray[lintIndex].tcnCollector + '"
		.Height = 350
		.Width = 350
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub
'------------------------------------------------------------------------------
Private Sub insPreCO685()
	'------------------------------------------------------------------------------
	Dim lcolCollectors As eCollection.Collectors
	Dim lclsCollector As Object
	Dim lcounter As Short
	lcolCollectors = New eCollection.Collectors
	
	lcounter = 0
	If lcolCollectors.Find() Then
		For	Each lclsCollector In lcolCollectors
			With mobjGrid
				.Columns("tcnCollector").DefValue = lclsCollector.nCollector
				.Columns("dtcClient").DefValue = lclsCollector.sClient
				.Columns("dtInputDate").DefValue = lclsCollector.dInputDate
				.Columns("tcnColType").DefValue = lclsCollector.nCollectorType
				.Columns("tcnConType").DefValue = lclsCollector.nContype
				.Columns("tcnInsur_area").DefValue = lclsCollector.nInsur_area
				lcounter = lcounter + 1
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsCollector
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'------------------------------------------------------------------------------
Private Sub insPreCO685Upd()
	'------------------------------------------------------------------------------
	
	Dim lclsCollector As eCollection.Collector
	
	lclsCollector = New eCollection.Collector
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Response.Write(.QueryString.Item("nCollector"))
			
			If lclsCollector.insPostCO685(CInt(.QueryString.Item("nCollector")), eFunctions.Values.eTypeData.etdDouble) Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCollectionTra.aspx", Request.QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

%>

<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>


<HTML>
<HEAD>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
    <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\performance\Vtime\Scripts\tMenu.js#%>
<%	'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%	'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%End If%>    
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

<SCRIPT>
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
</SCRIPT>
<%Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CO685", "CO685.aspx", 1, vbNullString))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

%>    

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
<BR><BR>
<%End If%>
<FORM METHOD="POST" ID="FORM" NAME="frmTabCollector" ACTION="valCollectionTra.aspx?mode=1">
 <%
Response.Write(mobjValues.ShowWindowsName("CO685"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCO685()
Else
	Call insPreCO685Upd()
End If
%>
</FORM>
</BODY>
</HTML>




