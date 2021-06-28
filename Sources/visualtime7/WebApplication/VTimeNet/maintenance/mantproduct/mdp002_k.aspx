<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga del Grid de la ventana 'MDP002'
Dim mclsWin_file_g As eProduct.Win_file_g

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	With mobjGrid
		.Codispl = "MDP002"
		.Top = 200
		.Left = 140
		.Width = 350
		.Height = 250
	End With
	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(102089, GetLocalResourceObject("tctCodisplColumnCaption"), "tctCodispl", 7, "",  , GetLocalResourceObject("tctCodisplColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(102090, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnCaption"),  ,  ,  , True)
		Call .AddTextColumn(102091, GetLocalResourceObject("tctTabnameColumnCaption"), "tctTabname", 15, "",  , GetLocalResourceObject("tctTabnameColumnToolTip"))
		Call .AddPossiblesColumn(102088, GetLocalResourceObject("cboBranch_genColumnCaption"), "cboBranch_gen", "Table634", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboBranch_genColumnToolTip"))
	End With
	
	With mobjGrid
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		If Session("bQuery") Then
			.ActionQuery = True
			.bOnlyForQuery = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("tctCodispl").EditRecord = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
	End With
End Sub

'% insPreMDP002: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMDP002()
	'--------------------------------------------------------------------------------------------
	Dim lblnFind As Boolean
	Dim lintCount As Integer
	
	lblnFind = mclsWin_file_g.Find
	
	If lblnFind Then
		lintCount = 0
		For lintCount = 0 To mclsWin_file_g.CountItemMDP002 - 1
			If mclsWin_file_g.ItemMDP002(lintCount) Then
				With mobjGrid
					.Columns("tctCodispl").DefValue = mclsWin_file_g.sCodispl
					.Columns("tctDescript").DefValue = mclsWin_file_g.sDescript
					.Columns("tctTabname").DefValue = mclsWin_file_g.sTabname
					.Columns("cboBranch_gen").DefValue = CStr(mclsWin_file_g.nBranch_gen)
				End With
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMDP002Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMDP002Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantProduct.aspx", "MDP002", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mclsWin_file_g = New eProduct.Win_file_g
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>

    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




	
<SCRIPT> 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
    
//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
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
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se controla la acción finalizar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
}
</SCRIPT> 
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.MakeMenu("MDP002", "MDP002_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAssociaField" ACTION="valMantProduct.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR></BR>")
	Call insPreMDP002()
Else
	Response.Write(mobjValues.ShowWindowsName("MDP002"))
	Call insPreMDP002Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
mclsWin_file_g = Nothing
%>
</FORM>
</BODY>
</HTML>




