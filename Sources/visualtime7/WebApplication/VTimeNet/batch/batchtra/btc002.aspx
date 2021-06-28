<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSchedule" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object
Dim lintBatch As Integer
Dim mintParCount As Integer


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Function insDefineHeader() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lobjParamInfo As eSchedule.Batch_param
	Dim k As Double
	Dim sValue As String
	Dim varrSplit() As Short
	Dim nAreaPrev As Integer
	Dim sSplit As Short
	
	mobjGrid = New eFunctions.Grid
	
	lobjParamInfo = New eSchedule.Batch_param
	
	'+Se obtienen los nombres de las columnas    
	If lobjParamInfo.Find_Name(lintBatch) Then
		
		Call mobjGrid.Columns.AddTextColumn(0, GetLocalResourceObject("tctKeyColumnCaption"), "tctKey", 20, "0", False, GetLocalResourceObject("tctKeyColumnToolTip"))
		
		'+ Se definen las columnas del grid    
		mintParCount = lobjParamInfo.CountName
		nAreaPrev = 0
		For k = 1 To mintParCount
			sValue = lobjParamInfo.Name(k)
			'+Si es una nueva area de parametros
			If nAreaPrev <> lobjParamInfo.nArea Then
				'+Se crea una nueva casilla para almacenar la cantidad de columnas de esa area
				nAreaPrev = lobjParamInfo.nArea
				ReDim Preserve varrSplit(nAreaPrev)
				varrSplit(nAreaPrev) = 0
			End If
			varrSplit(nAreaPrev) = varrSplit(nAreaPrev) + 1
			
      Call mobjGrid.Columns.AddTextColumn(0, lobjParamInfo.sName, "tctCol" & k, 20, "", False, lobjParamInfo.sDescript)
		Next 
		
		'+Se crean los splits
		'+Se inicializa en cero porque arreglo varrSplit se creo con la 
		'+primera casilla nula.
		'+Luego, la casilla 0 está nula, la 1 tiene la cantidad de columnas del area 1
		'+la 2 tiene la cantidad de columnas del area 2, etc.
		k = 0
		For	Each sSplit In varrSplit
			'+Se pueden ir agregando más areas si se requiere        
			If k = 0 Then
				Call mobjGrid.Splits_Renamed.AddSplit(0, "", 1)
			ElseIf k = 1 Then 
				Call mobjGrid.Splits_Renamed.AddSplit(0, GetLocalResourceObject("ProcessColumnCaption"), sSplit)
			Else
				Call mobjGrid.Splits_Renamed.AddSplit(0, GetLocalResourceObject("ResultColumnCaption"), sSplit)
			End If
			k = k + 1
		Next sSplit
		
		'+ Se definen las propiedades generales del grid
		With mobjGrid
			.sCodisplPage = "BTC002"
			.AddButton = False
			.DeleteButton = False
			.ActionQuery = mobjValues.ActionQuery
			.Height = 350
			.Width = 280
			.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
			.Columns("Sel").GridVisible = False
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
		End With
		
		insDefineHeader = True
		
	Else
		
		Response.Write("<LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL>")
		insDefineHeader = False
	End If
	
	lobjParamInfo = Nothing
	
End Function

'% insPreCodispl: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreBTC002()
	'--------------------------------------------------------------------------------------------
	Dim lclsParams As eSchedule.Batch_param
	Dim t As Integer
	Dim lintCountValue As Integer
	Dim lintCol As Double
	Dim lstrKey As Object
	Dim sValue As Object
	Dim bFirstCol As Object
	
	lclsParams = New eSchedule.Batch_param
	
	If lclsParams.Find_Value("", lintBatch, eRemoteDB.Constants.intNull) Then
		lintCountValue = lclsParams.CountValue
		lintCol = 1
		For t = 1 To lintCountValue
			sValue = lclsParams.Value(t)
			
			With mobjGrid
				
				'+Si se pasó a sgte fila, se escribe fila de datos
				If lintCol = mintParCount + 1 Then
					Response.Write(.DoRow)
					lintCol = 1
				End If
				
				'+En la primera columna se carga la llave
				If lintCol Mod mintParCount = 1 Then
					.Columns("tctKey").DefValue = lclsParams.sKey
				End If
				
				.Columns("tctCol" & lintCol).DefValue = lclsParams.sValue
				.sDelRecordParam = "nBatch=' + marrArray[lintIndex].tcnBatch " & " + '&sKey=' + marrArray[lintIndex].tctKey + '"
				lintCol = lintCol + 1
			End With
		Next 
		Response.Write(mobjGrid.DoRow())
	End If
	lclsParams = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

</script>
<%Response.Expires = 0


mobjValues = New eFunctions.Values

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

lintBatch = mobjValues.StringToType(Request.QueryString.Item("nBatch"), eFunctions.Values.eTypeData.etdLong)

%>
<html>
<head>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%Response.Write(mobjValues.StyleSheet())%>	


<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write(mobjMenu.setZone(2, "BTC002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	mobjMenu = Nothing
End If
%>
<script>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 15/03/04 12:05 $|$$Author: Nvaplat7 $"

</script>
</head>
<body ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("BTC002", Request.QueryString.Item("sWindowDescript")))%>
<form METHOD="POST" NAME="BTC002" ACTION="valBatch.aspx?sMode=2">
<%If insDefineHeader() Then
	Call insPreBTC002()
End If
%>
</form> 
</body>
</html>





