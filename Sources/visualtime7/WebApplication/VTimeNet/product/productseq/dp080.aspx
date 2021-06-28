<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga de datos del Grid de la ventana		
Dim mcolProduct As eProduct.Lend_Agree_Prods
Dim mclsProduct As eProduct.Lend_Agree_Prod


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeAplyColumnCaption"), "cbeAply", "TABPRESCONV", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeAplyColumnToolTip"))
	End With
	
	With mobjGrid
		
		.Codispl = "DP080"
		.Codisp = "DP080"
		.Top = 150
		.Left = 100
		.Width = 600
		.Height = 200
		.WidthDelete = 600
		.bCheckVisible = Request.QueryString.Item("Action") <> "Add"
		.Columns("Sel").GridVisible = Not Session("bQuery")
		
		.sDelRecordParam = "Ncod_Agree='+ marrArray[lintIndex].cbeAply + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreDP080: Obtiene los cargos de los aportes
'-----------------------------------------------------------------------------
Private Sub insPreDP080()
	'-----------------------------------------------------------------------------
	If mcolProduct.FindLend_agree_Prod(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
		
		If mcolProduct.Count > 0 Then
			mobjGrid.DeleteButton = True
			For	Each mclsProduct In mcolProduct
				With mobjGrid
					.Columns("cbeAply").DefValue = CStr(mclsProduct.Ncod_Agree)
				End With
				Response.Write(mobjGrid.DoRow())
			Next mclsProduct
		End If
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP080Upd: Realiza la eliminación de cargos
'-----------------------------------------------------------------------------
Private Sub insPreDP080Upd()
	'-----------------------------------------------------------------------------
	'- Objeto para manejo de los cargos de contribuciones
	Dim mclsLend_Agree_Prod As eProduct.Lend_Agree_Prod
	
	Dim lblnPost As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
		mclsLend_Agree_Prod = New eProduct.Lend_Agree_Prod
		'+ Muestra el mensaje para eliminar registros
		Response.Write(mobjValues.ConfirmDelete())
		
		With mclsLend_Agree_Prod
			.nBranch = Session("nBranch")
			.nProduct = Session("nProduct")
			.Ncod_Agree = CInt(Request.QueryString.Item("Ncod_Agree"))
			.dEffecdate = Session("dEffecdate")
			.nUsercode = Session("nUsercode")
			.Delete()
		End With
	End If
	mclsLend_Agree_Prod = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valProductSeq.aspx", "DP080", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

'- Se crean las instancias de las variables modulares
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mcolProduct = New eProduct.Lend_Agree_Prods
mclsProduct = New eProduct.Lend_Agree_Prod
mobjGrid = New eFunctions.Grid

mobjGrid.sCodisplPage = "DP080"
mobjValues.sCodisplPage = "DP080"

'+ Cambios en la lógica de descuento de los costos coberturas. 
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE=javascript>
//+ Esta línea guarda la version procedente de VSS
    document.VssVersion="$$Revision: 3 $|$$Date: 13/02/06 11:28 $"
</SCRIPT>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP080"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP080", "DP080.aspx"))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP080" ACTION="valProductSeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("DP080"))

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP080()
Else
	Call insPreDP080Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mcolProduct = Nothing

%> 
</FORM>
</BODY>
</HTML>




