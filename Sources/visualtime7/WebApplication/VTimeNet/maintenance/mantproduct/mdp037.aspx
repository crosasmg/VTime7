<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga del Grid de la ventana 'MDP037'
Dim mclsTab_Short As eProduct.Tab_short

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(102077, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 5, CStr(0),  , GetLocalResourceObject("tcnMonthColumnToolTip"))
		Call .AddNumericColumn(102078, GetLocalResourceObject("tcnDaysColumnCaption"), "tcnDays", 5, CStr(0),  , GetLocalResourceObject("tcnDaysColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(102079, GetLocalResourceObject("tcnPremCashColumnCaption"), "tcnPremCash", 5, CStr(0), False, GetLocalResourceObject("tcnPremCashColumnToolTip"), True, 2)
		Call .AddNumericColumn(102080, GetLocalResourceObject("tcnPremDevColumnCaption"), "tcnPremDev", 5, CStr(0), False, GetLocalResourceObject("tcnPremDevColumnToolTip"), True, 2)
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = "MDP037"
		.Top = 200
		.Left = 140
		.Width = 350
		.Height = 250
		If Request.QueryString.Item("nMainAction") = "301" Then
			.Columns("tcnMonth").EditRecord = False
		Else
			.Columns("tcnMonth").EditRecord = True
		End If
		.DeleteButton = True
		.AddButton = True
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		If Session("bQuery") Then
			.ActionQuery = True
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% insPreMDP037: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMDP037()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	
	If mclsTab_Short.FindMDP037(mobjValues.StringToType(Session("dEffecdate_MDP037"), eFunctions.Values.eTypeData.etdDate)) Then
		lintCount = 0
		For lintCount = 1 To mclsTab_Short.CountItemMDP037
			If mclsTab_Short.ItemMDP037(lintCount) Then
				With mobjGrid
					.Columns("tcnMonth").DefValue = CStr(mclsTab_Short.nMonthmax)
					.Columns("tcnDays").DefValue = CStr(mclsTab_Short.nDaysmax)
					.Columns("tcnPremCash").DefValue = CStr(mclsTab_Short.nRatedevo)
					.Columns("tcnPremDev").DefValue = CStr(mclsTab_Short.nRateprem)
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostMDP037 cuando se eliminen los registros seleccionados 08/11/2001
					.Columns("sParam").DefValue = "nMonthmax=" & mclsTab_Short.nMonthmax & "&nDaysmax=" & mclsTab_Short.nDaysmax & "&nRatedevo=" & mclsTab_Short.nRatedevo & "&nRateprem=" & mclsTab_Short.nRateprem & "&dEffecdate=" & Session("dEffecdate_MDP037") & "&nUserCode=" & Session("nUsercode")
					
				End With
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreMDP037Upd. Se define esta funcion para contruir el contenido de la ventana UPD de la tabla de corto plazo
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreMDP037Upd()
	'------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		With mobjGrid
			.Width = 800
			.Height = 600
		End With
		
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantProduct.aspx", "MDP037", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
		
		With Request
			lblnPost = mclsTab_Short.insPostMDP037("MDP037", "Del", 0, CInt(.QueryString.Item("nMonthmax")), CInt(.QueryString.Item("nDaysmax")), CDbl(.QueryString.Item("nRatedevo")), CDbl(.QueryString.Item("nRateprem")), CDate(.QueryString.Item("dEffecdate")), CInt(.QueryString.Item("nUsercode")))
			
		End With
	Else
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantProduct.aspx", "MDP037", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
	End If
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mclsTab_Short = New eProduct.Tab_short
mobjMenu = New eFunctions.Menues

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
With Response
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "MDP037", "MDP037.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTabShort" ACTION="valMantProduct.aspx?Validate=1">
<%
Response.Write(mobjValues.ShowWindowsName("MDP037"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMDP037()
Else
	Call insPreMDP037Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
mclsTab_Short = Nothing
mobjMenu = Nothing
%>
</FORM>
</BODY>
</HTML>
			




