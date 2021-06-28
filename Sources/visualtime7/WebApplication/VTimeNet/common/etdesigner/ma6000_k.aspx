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
		.AddPossiblesColumn(0, "Tipo de elemento", "cbeObject_type", "Table9999", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		.AddTextColumn(0, "Nombre", "tctObject_name", 30, vbNullString)
		.AddTextColumn(0, "Ruta", "tctPath", 40, vbNullString)
		.AddNumericColumn(0, "Orden", "tcnSequence", 10, vbNullString,  , "Orden de dependencia", False)
		.AddPossiblesColumn(0, "Acción", "cbeAction", "Table9999", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		.AddHiddenColumn("hddnId", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = "MA6000"
		.Codisp = "MA6000_k"
		.Top = 100
		.Height = 250
		.Width = 600
		.AddButton = Request.QueryString.Item("valCodispl") <> vbNullString
		.Columns("cbeObject_type").TypeList = 2
		.Columns("cbeObject_type").List = "90,91,92,93"
		.Columns("cbeObject_type").EditRecord = True
		.Columns("cbeAction").TypeList = 1
		.Columns("cbeAction").List = "90,91,92,93"
		.Columns("tctObject_name").EditRecord = True
		.sDelRecordParam = "valCodispl=" & Request.QueryString.Item("valCodispl") & "&nId='+ marrArray[lintIndex].hddnId + '"
		
		.sEditRecordParam = "valCodispl=" & Request.QueryString.Item("valCodispl") & "&nModules=" & Request.QueryString.Item("nModules")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%InsPreMA6000: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub InsPreMA6000()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>Transacción</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("        ")

	
	mobjValues.Parameters.ReturnValue("nModules",  ,  , True)
	Response.Write(mobjValues.PossiblesValues("valCodispl", "tabWindows", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("valCodispl"),  ,  ,  ,  ,  , "InsChangeCodispl(this.value);",  , 8, "Código de la transacción", eFunctions.Values.eTypeCode.eString))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>Módulo</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeModules", "Table87", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nModules"),  ,  ,  ,  ,  ,  , True,  , "Módulo asociado a la transacción"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<BR>")

	
	Dim lcolWin_chklists As eJobs.Win_chklists
	Dim lclsWin_chklist As Object
	lcolWin_chklists = New eJobs.Win_chklists
	With mobjGrid
		If lcolWin_chklists.Find(Request.QueryString.Item("valCodispl")) Then
			For	Each lclsWin_chklist In lcolWin_chklists
				.Columns("cbeObject_type").DefValue = lclsWin_chklist.sObject_type
				.Columns("tctObject_name").DefValue = lclsWin_chklist.sObject_name
				.Columns("tctPath").DefValue = lclsWin_chklist.sPath
				.Columns("tcnSequence").DefValue = lclsWin_chklist.nSequence
				.Columns("cbeAction").DefValue = lclsWin_chklist.sAction
				.Columns("hddnId").DefValue = lclsWin_chklist.nId
				Response.Write(mobjGrid.DoRow())
			Next lclsWin_chklist
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.AnimatedButtonControl("btnFile", "..\..\images\Printer.png", "Generar archivo plano",  , "InsGenerateFile()", Not mobjGrid.AddButton))
	'UPGRADE_NOTE: Object lclsWin_chklist may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsWin_chklist = Nothing
	'UPGRADE_NOTE: Object lcolWin_chklists may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolWin_chklists = Nothing
End Sub

'% InsPreMA6000Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub InsPreMA6000Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsWin_chklist As eJobs.Win_chklist
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsWin_chklist = New eJobs.Win_chklist
			Call lclsWin_chklist.InsPostMA6000Upd(.QueryString("Action"), _
                                                  .QueryString("valCodispl"), _
                                                  eRemoteDB.Constants.intNull, _
                                                  vbNullString, _
                                                  vbNullString, _
                                                  vbNullString, _
                                                  mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), _
                                                  vbNullString, _
                                                  eRemoteDB.Constants.intNull, _
                                                  vbNullString)
			lclsWin_chklist = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMA500_K.aspx", "MA6000", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
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
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.34 $|$$Author: Nvaplat60 $"

//%insCancel: Se ejecuta cuando se cancela la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//%insStateZone: Se ejecuta cuando selecciona una acción
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}

//%insCancel: Se ejecuta cuando se cancela la transacción
//-------------------------------------------------------------------------------------------------------------------
function InsChangeCodispl(sCodispl){
//-------------------------------------------------------------------------------------------------------------------
    var lstrstring = '';
    var lstrCodispl = '';

    lstrCodispl = sCodispl;
    if (lstrCodispl != '<%=Request.QueryString.Item("valCodispl")%>'){
        lstrstring += document.location;
        lstrstring = lstrstring.replace(/&valCodispl=.*/, "");
        lstrstring = lstrstring + "&valCodispl=" + lstrCodispl + "&nModules=" + self.document.forms[0].valCodispl_nModules.value;
        document.location.href = lstrstring;
    }
}

//%InsGenerateFile: Llama al proceso que genera el archivo plano
//-------------------------------------------------------------------------------------------------------------------
function InsGenerateFile(){
//-------------------------------------------------------------------------------------------------------------------
	insDefValues('CHK_LIST', 'valCodispl=<%=Request.QueryString.Item("valCodispl")%>&sModules=' + self.document.forms[0].cbeModules.value);
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
		.Write(mobjMenu.MakeMenu("MA6000", "MA6000_k.aspx", 1, ""))
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
Response.Write(mobjValues.ShowWindowsName("MA6000"))
%>
<FORM METHOD="post" ID="FORM" NAME="frmMA6000" ACTION="valMA500_K.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call InsPreMA6000()
Else
	Call InsPreMA6000Upd()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




