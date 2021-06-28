<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid



'% insDefineHeader: Configura los datos del grid.
'%--------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'%--------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "MVI7300"
	
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, "Tipo de SA-APV", "cbeType_saapv", "table5742", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString("Action") <> "Add",  , "Tipo de SA-APV para el cual se establece el plazo.")
		Call .AddPossiblesColumn(0, "Tipo de valoración", "cbeValuesty", "table125", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "Indica la acción a tomar con respecto al cálculo del plazo para ejecutar la operación de SA-APV.")
		Call .AddNumericColumn(0, "Día", "tcnDayadd", 2, "",  , "Indica el día del mes en que se debe ejecutar la operación de SA-APV.", False)
		Call .AddPossiblesColumn(0, "Mes", "cbeValuesmo", "table126", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "Indicador de mes de la valorización del aporte.")
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "MVI7300"
		.sCodisplPage = "MVI7300"
		'.Columns("Sel").GridVisible = False
		.Height = 280
		.Width = 350
		.nMainAction = Request.QueryString("nMainAction")
		.ActionQuery = mobjValues.ActionQuery
		.Columns("cbeType_saapv").EditRecord = True
		
		'If Request.QueryString("Action") = "Add" Then
		'	.Columns("tcnFund").disabled = False
		'Else
		'	.Columns("tcnFund").disabled = True
		'End If
		
		'.AddButton=True
		'.DeleteButton=False
		
		Call .Splits_Renamed.AddSplit(0, vbNullString, 2)
		
		Call .Splits_Renamed.AddSplit(0, "Fecha efectiva", 2)
		
		.sDelRecordParam = "nType_saapv=' + marrArray[lintIndex].cbeType_saapv + '"
		
		'+ Permite continuar si el check está marcado.
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI7300: Obtiene los datos de los fondos de inversión.
'%--------------------------------------------------------------------------------------
Private Sub insPreMVI7300()
	'%--------------------------------------------------------------------------------------
	Dim lclsUl_Legal_Terms As eSaapv.Ul_Legal_Terms
	Dim lcolUl_Legal_Termss As eSaapv.Ul_Legal_Termss
	
	lclsUl_Legal_Terms = New eSaapv.Ul_Legal_Terms
	lcolUl_Legal_Termss = New eSaapv.Ul_Legal_Termss
	
	If lcolUl_Legal_Termss.Find(mobjValues.StringToDate(CStr(Session("dEffecdate")))) Then
		With mobjGrid
			For	Each lclsUl_Legal_Terms In lcolUl_Legal_Termss
				
				.Columns("cbeType_saapv").DefValue = CStr(lclsUl_Legal_Terms.nType_saapv)
				.Columns("cbeValuesmo").DefValue = CStr(lclsUl_Legal_Terms.nValuesmo)
				.Columns("cbeValuesty").DefValue = CStr(lclsUl_Legal_Terms.nValuesty)
				.Columns("tcnDayadd").DefValue = CStr(lclsUl_Legal_Terms.nDayadd)
				
				Response.write(.DoRow)
			Next lclsUl_Legal_Terms
		End With
	End If
	
	Response.write(mobjGrid.closeTable)
	
	'UPGRADE_NOTE: Object lclsUl_Legal_Terms may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsUl_Legal_Terms = Nothing
	'UPGRADE_NOTE: Object lcolUl_Legal_Termss may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolUl_Legal_Termss = Nothing
End Sub

'% insPreMVI002Upd: Muestra la ventana Popup para las actualizaciones.
'%--------------------------------------------------------------------------------------
Private Function insPreMVI7300Upd() As Object
	'%------------------------------------------------------------------------
	Dim lclsUl_Legal_Terms As eSaapv.Ul_Legal_Terms
	lclsUl_Legal_Terms = New eSaapv.Ul_Legal_Terms
	
	With Request
		If Request.QueryString("Action") = "Del" Then
			Response.write(mobjValues.ConfirmDelete)
			
			Call lclsUl_Legal_Terms.insPostMVI7300(Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("nType_saapv"), eFunctions.Values.eTypeData.etdLong), 0, 0, 0, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))
			
		End If
	End With
	
	Response.write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valMantNoTraLife.aspx", "MVI7300", Request.QueryString("nMainAction"), mobjValues.ActionQuery, Request.QueryString("Index")))
	
	'UPGRADE_NOTE: Object lclsUl_Legal_Terms may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsUl_Legal_Terms = Nothing
End Function

</script>
<%Response.Expires = -1

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MVI7300")

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

If Request.QueryString("nMainAction") = 401 Then
	mobjGrid.ActionQuery = True
	mobjValues.ActionQuery = True
End If
mobjValues.sCodisplPage = "MVI7300"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->


<SCRIPT LANGUAGE="JavaScript">
    var nMainAction = <%=Request.QueryString("nMainAction")%>;

//+ Para Control de Versiones "NO REMOVER"
//------------------------------------------------------------------------------
	document.VssVersion="$$Revision: 1 $|$$Date: 12/11/11 16:10 $"
//------------------------------------------------------------------------------

//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------
	return true
}
</SCRIPT>    
        <%With Response
	.write(mobjValues.StyleSheet() & vbCrLf)
	
	If Request.QueryString("Type") <> "PopUp" Then
		.write(mobjMenu.setZone(2, "MVI7300", "MVI7300.aspx"))
	End If
End With

'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmUl_Legal_Terms" ACTION="valMantNoTraLife.aspx?mode=1&nMainAction=<%=Request.QueryString("nMainAction")%>">
            <%=mobjValues.ShowWindowsName("MVI7300")%>
            <BR>
            <%
Call insDefineHeader()

If Request.QueryString("Type") <> "PopUp" Then
	Call insPreMVI7300()
Else
	Call insPreMVI7300Upd()
End If

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
        </FORM>
    </BODY>
</HTML>
<%
'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("MVI7300")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer

%>





