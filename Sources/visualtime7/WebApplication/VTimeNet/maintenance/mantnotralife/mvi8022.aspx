
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim mclsTab_intproy As ePolicy.Tab_intproy
Dim mcolTab_intproys As ePolicy.Tab_intproys


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim Heigh As Object
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MVI8022"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddDateColumn(0, "Desde", "tcdEffecdate",  ,  , "Fecha de efecto - desde",  ,  , CStr(True))
		Call .AddHiddenColumn("hddNewEffecDate", Request.QueryString("dEffecdate"))
		Call .AddDateColumn(0, "Hasta", "tcdNulldate",  ,  , "Fecha de efecto - hasta",  ,  , CStr(True))
		Call .AddNumericColumn(0, "Mínima", "tcnIntproy_min", 18, CStr(1), True, "Rentabilidad compañía - mínima", False, 6,  ,  ,  ,  , False, True)
		Call .AddNumericColumn(0, "Máxima", "tcnIntproy_max", 18, CStr(1), True, "Rentabilidad compañía - máxima", False, 6,  ,  ,  ,  , False)
		Call .AddNumericColumn(0, "Mínima", "tcnSvsproy_min", 18, CStr(1), True, "Rentabilidad SVS - mínima", False, 6,  ,  ,  ,  , False, True)
		Call .AddNumericColumn(0, "Máxima", "tcnSvsproy_max", 18, CStr(1), True, "Rentabilidad SVS - máxima", False, 6,  ,  ,  ,  , False)
		Call .AddNumericColumn(0, "Mínima", "tcnMonths_min", 5, CStr(1), True, "Meses cálculo rentabilidad - mínima", False, 0,  ,  ,  ,  , False)
		Call .AddNumericColumn(0, "Máxima", "tcnMonths_max", 5, CStr(1), True, "Meses cálculo rentabilidad - máxima", False, 0,  ,  ,  ,  , False)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI8022"
		.Codisp = "MVI8022"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 320
		.Width = 450
		.sEditRecordParam = "dEffecdate=" & Request.QueryString("dEffecdate")
		.sDelRecordParam = "dEffecdate=" & Request.QueryString("dEffecdate")
		.nMainAction = Request.QueryString("nMainAction")
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcdEffecdate").EditRecord = True
		.Columns("tcdEffecdate").Disabled = True
		.Columns("tcdEffecdate").KeyField = CStr(True)
		.Columns("tcdNulldate").Disabled = True
		
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		
            '.Splits_Renamed.AddSplit(0, "Vigencia", 2)
            '.Splits_Renamed.AddSplit(0, "Rentabilidad Compañía", 2)
            '.Splits_Renamed.AddSplit(0, "Rentabilidad SVS", 2)
            '          .Splits_Renamed.AddSplit(0, "Meses cálculo rentabilidad fondo", 2)
            'Call mobjGrid.Splits_Renamed.AddSplit(0, GetLocalResourceObject("ProcessColumnCaption"), sSplit)
            mobjGrid.Splits_Renamed.AddSplit(0, "Vigencia", 2)
            mobjGrid.Splits_Renamed.AddSplit(0, "Rentabilidad Compañía", 2)
            mobjGrid.Splits_Renamed.AddSplit(0, "Rentabilidad SVS", 2)
            mobjGrid.Splits_Renamed.AddSplit(0, "Meses cálculo rentabilidad fondo", 2)
	End With
End Sub

'% insPreMVI8022: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8022()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Long
	lintIndex = 1
	If mcolTab_intproys.Find(Request.QueryString("dEffecdate")) Then
		With mobjGrid
			While lintIndex <= mcolTab_intproys.Count
				mclsTab_intproy = mcolTab_intproys.Item(lintIndex)
				mobjGrid.Columns("tcdEffecdate").DefValue = CStr(mclsTab_intproy.dEffecdate)
				mobjGrid.Columns("tcdNulldate").DefValue = CStr(mclsTab_intproy.dNulldate)
				mobjGrid.Columns("tcnIntproy_min").DefValue = CStr(mclsTab_intproy.nIntproy_min)
				mobjGrid.Columns("tcnIntproy_max").DefValue = CStr(mclsTab_intproy.nIntproy_max)
				mobjGrid.Columns("tcnSvsproy_min").DefValue = CStr(mclsTab_intproy.nSvsproy_min)
				mobjGrid.Columns("tcnSvsproy_max").DefValue = CStr(mclsTab_intproy.nSvsproy_max)
				mobjGrid.Columns("tcnMonths_min").DefValue = CStr(mclsTab_intproy.nMonths_min)
				mobjGrid.Columns("tcnMonths_max").DefValue = CStr(mclsTab_intproy.nMonths_max)
				Response.write(mobjGrid.DoRow())
				lintIndex = lintIndex + 1
			End While
		End With
	End If
	
	Response.write(mobjGrid.closeTable())
End Sub

'% insPreMVI8022Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8022Upd()
	'--------------------------------------------------------------------------------------------
	If Request.QueryString("Action") = "Del" Then
		Response.write(mobjValues.ConfirmDelete())
		Call mclsTab_intproy.insPostMVI8022(Request.QueryString("Action"), Request.QueryString("dEffecdate"), 0, 0, CInt(Session("nUsercode")), 0, 0, 0, 0)
	End If
	Response.write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valMantNoTraLife.aspx", "MVI8022", Request.QueryString("nMainAction"), mobjValues.ActionQuery, Request.QueryString("Index")))
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsTab_intproy = New ePolicy.Tab_intproy
mcolTab_intproys = New ePolicy.Tab_intproys

mobjValues.sCodisplPage = "MVI8022"
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 5/10/15 12:59 $"
</SCRIPT>
	<%
Response.write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.write(mobjMenu.setZone(2, "MVI8022", "MVI8022.aspx"))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI8022" ACTION="valMantNoTraLife.aspx?sMode=1&<%=Request.QueryString%>">
<%Response.write(mobjValues.ShowWindowsName("MVI8022"))
Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreMVI8022Upd()
Else
	Call insPreMVI8022()
End If
%>
</FORM> 
</BODY>
</HTML>
<%
'UPGRADE_NOTE: Object mclsTab_intproy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsTab_intproy = Nothing
'UPGRADE_NOTE: Object mcolTab_intproys may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mcolTab_intproys = Nothing
%>





