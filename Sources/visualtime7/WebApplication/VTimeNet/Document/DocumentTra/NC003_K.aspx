<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
'- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As New eFunctions.Values
    Dim mobjGrid As New eFunctions.Grid
    Dim mobjMenues As New eFunctions.Menues
    Dim lclsGeneral As New eGeneral.GeneralFunction

    
Dim mstrKey As String


'% insDefineHeader:Permite definir las columnas del grid, así como también de habilitar o inhabilitar
'% los botones de agregar y cancelar.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	Dim clngActionQuery As String
	'--------------------------------------------------
	mobjGrid.sCodisplPage = "NC003_k"
	'Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	'mobjGrid.ActionQuery = Session("bQuery")
	'+ Se definen las columnas del Grid
	With mobjGrid.Columns
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.AddTextColumn(0, "Accion", "cbAction", 30, vbNullString,  , "Accion a ejecutar")
			.AddTextColumn(0, "Siniestro", "tcnClaim", 15, vbNullString,  , "Numeor de siniestro")
			.AddTextColumn(0, "Orden de servicio", "cbOrdServ", 15, vbNullString,  , "Orden de servicio")
			.AddTextColumn(0, "Proveedor", "cbProvider", 30, vbNullString,  , "Proveedor")
			.AddTextColumn(0, "Documento", "cbDocument", 15, vbNullString,  , "Numero de documento")
		Else
			.AddPossiblesColumn(0, "Accion", "cbAction", "Table996", 2,  ,  ,  ,  ,  , "ChangeValues('Action', this.value)",  , 5)
			.AddNumericColumn(0, "Siniestro", "tcnClaim", 10, vbNullString,  ,  ,  ,  ,  ,  , "ChangeValues('Claim', this.value);")
			.AddPossiblesColumn(0, "Orden de servicio", "cbOrdServ", "tabproford", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "ChangeValues('Order', this.value);", True, 10)
			mobjGrid.Columns("cbOrdServ").Parameters.Add("nClaim", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbOrdServ").Parameters.Add("cbAction", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.AddTextColumn(0, "Proveedor", "cbProvider", 30, "",  ,  ,  ,  ,  , True)
			.AddPossiblesColumn(0, "Documento", "cbDocument", "tabdocuments_1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "ChangeValues('Document', this.value);", True, 10)
			mobjGrid.Columns("cbDocument").Parameters.Add("cbProvider", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbDocument").Parameters.Add("cbAction", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbDocument").Parameters.Add("cbOrdServ", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbDocument").Parameters.ReturnValue("sCliename", True, "Proveedor", True)
		End If
		.AddHiddenColumn("HddTypesupport", vbNullString)
		.AddHiddenColumn("HddProvider", vbNullString)
		.AddHiddenColumn("HddSclient", vbNullString)
		.AddHiddenColumn("HddNstatus", vbNullString)
		
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "NC003_k"
		
		If Request.QueryString.Item("nMainAction") = clngActionQuery Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = True
			.ActionQuery = False
		End If
		.sEditRecordParam = "skey=" & mstrKey
		
		.DeleteButton = False
		
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 0 Then
			
			.Left = 240
			.Top = 250
			.Height = 290
			.AddButton = True
			.Width = 480
			.WidthDelete = 450
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = 1
			End If
		Else
			.AddButton = False
		End If
		
		mobjGrid.Columns("Sel").Disabled = True
	End With
	
End Sub

'% insPreNC003: Se definen los objetos a ser utilizados a lo largo de la transacción.
'-----------------------------------------------------------------------------------------
Private Sub insPreNC003()
	'-----------------------------------------------------------------------------------------
        Dim lcolDocument_Pay As New eClaim.Document_Pay
        Dim lclsDocument_Pays As New eClaim.Document_Pays
	Dim res As Boolean

	
	res = lclsDocument_Pays.FindNC003(Request.QueryString.Item("sKey"))
	
	With mobjGrid
		If res Then
			For	Each lcolDocument_Pay In lclsDocument_Pays
				
				mobjGrid.Columns("cbAction").DefValue = lcolDocument_Pay.sDescript
				mobjGrid.Columns("tcnClaim").DefValue = lcolDocument_Pay.nClaim
				If lcolDocument_Pay.nServ_order > 0 Then
					mobjGrid.Columns("cbOrdServ").DefValue = lcolDocument_Pay.nServ_order
				End If
				mobjGrid.Columns("cbProvider").DefValue = lcolDocument_Pay.sCliename
				mobjGrid.Columns("cbDocument").DefValue = lcolDocument_Pay.nDocument
				Response.Write(mobjGrid.DoRow())
			Next lcolDocument_Pay
		End If
	End With
	
	Response.Write(mobjGrid.closeTable())
	'Response.Write mobjValues.HiddenControl("Hddskey",Request.QueryString("skey"))
	
	'UPGRADE_NOTE: Object lclsDocument_Pays may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsDocument_Pays = Nothing
	'UPGRADE_NOTE: Object lcolDocument_Pay may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolDocument_Pay = Nothing
End Sub

'% insPreNC003Upd: Permite realizar el llamado a la ventana PopUp. Esta transacción posee una serie
'% de validaciones cuando se está eliminando un registro del grid, es por eso que se agregó el manejo
'% de la misma.
'-----------------------------------------------------------------------------------------
Private Sub insPreNC003Upd()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valNC003tra.aspx", "NC003", 302, mobjValues.ActionQuery, -1))
End Sub

</script>
<%Response.Expires = -1



mobjValues.sCodisplPage = "NC003_k"

If Request.QueryString.Item("sKey") = vbNullString Then
'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm

	mstrKey = lclsGeneral.getsKey(Session("nUsercode"))
	'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsGeneral = Nothing
Else
	mstrKey = Request.QueryString.Item("sKey")
End If

%>
<html>
<head>
 <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
		<%	'$$EWI_1012:C:\NetMig2\Result\App\VTimeStep1\Document\DocumentTra\Vtime\Scripts\tMenu.js#%>
<%	'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%	'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></script>
<%	
End If
%>
    <meta NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
    <%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>


<%
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
'UPGRADE_NOTE: The 'eFunctions.Menues' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm

            
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
            .Write(mobjMenues.MakeMenu("NC003", "NC003_k.aspx", 1, ""))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenues = Nothing
	End If
End With
%>
<script>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 30/10/07 12:42 $|$$Author: Ljimenez $"

//% insCancel: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//% insPreZone: Se definen las acciones.
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
//------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//------------------------------------------------------------------------------------------
var lstrQString
	
	switch(Option)
		{
		case "Claim":
			with(self.document.forms[0])
				{
				if(tcnClaim.value != "")
					{
					//cbOrdServ.value = "";
					cbOrdServ.disabled = false;
					btncbOrdServ.disabled = false;
					cbDocument.disabled = false;
					btncbDocument.disabled = false;
					cbOrdServ.Parameters.Param1.sValue = Field;
					cbDocument.Parameters.Param1.sValue = 0;
					}
				else
					{
					cbOrdServ.value = "";
					cbProvider.value = "";
					cbDocument.value = "";
					UpdateDiv('cbDocumentDesc','');
					UpdateDiv('cbOrdServDesc','');
					cbDocument.disabled = true;
					btncbDocument.disabled = true;
					cbOrdServ.disabled = true;
					btncbOrdServ.disabled = true;
					}
				}
			break;
		
		case "Action":
			with(self.document.forms[0])
				{
					tcnClaim.value = "";;
					cbDocument.value = "";
					cbProvider.value = "";
					cbOrdServ.value = "";
					UpdateDiv('cbDocumentDesc','');
					UpdateDiv('cbOrdServDesc','');
					tcnClaim.disabled = true;
					cbOrdServ.disabled = true;
					btncbOrdServ.disabled = true;
					cbDocument.disabled = true;
					btncbDocument.disabled = true;
				if(cbAction.value != 0)
					{
					tcnClaim.disabled = false;
					//tcnClaim.value = "";
					//cbOrdServ.value = "";
					//cbDocument.value = "";
					//cbProvider.value = "";
					cbDocument.Parameters.Param2.sValue = cbAction.value;
					cbOrdServ.Parameters.Param2.sValue = cbAction.value;
					}
			}
			break;
		case "Order":
			with(self.document.forms[0])
				{
				if(cbOrdServ.value != "")
					{
					cbDocument.disabled = false;
					btncbDocument.disabled = false;
					cbDocument.value = "";
					UpdateDiv('cbDocumentDesc','');
					cbDocument.Parameters.Param1.sValue = '';
					cbDocument.Parameters.Param2.sValue = cbAction.value;
					cbDocument.Parameters.Param3.sValue = cbOrdServ.value;
					lstrQString = 'nServ_order='+ cbOrdServ.value;
                    insDefValues('nServ_Order_rep',lstrQString,'/VTimeNet/Document/DocumentTra','showdefNC003');
					}
				else
					{
					cbDocument.value = "";
					cbProvider.value = "";
					HddSclient.value = "";
					HddProvider.value = "";
					UpdateDiv('cbDocumentDesc','');
					}
				}
			break;
		
		case "Document":
			with(self.document.forms[0])
				{
				if(cbDocument.value != "")
					{
					lstrQString = 'nDocument='+ cbDocument.value + '&sClient='+ HddSclient.value;
                    insDefValues('nDocument_rep',lstrQString,'/VTimeNet/Document/DocumentTra','showdefNC003');
					}
				else
				    {
					cbDocument.value = "";
					HddSclient.value = "";
					}
				}
			break;
	}
}
</script>
</head>

<body ONUNLOAD="closeWindows();">
<form METHOD="POST" ID="FORM" NAME="NC003_k" ACTION="valNC003tra.aspx?mode=1">

<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreNC003()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreNC003Upd()
End If

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing

%>
</form>
</body>
</html>





