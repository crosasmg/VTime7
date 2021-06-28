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

Dim lintBatch As Object
Dim lintUser As Object
Dim ldtmProcDate As Object
Dim lintSheet As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBatchColumnCaption"), "tcnBatch", 5, CStr(0),  , GetLocalResourceObject("tcnBatchColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnSheetColumnCaption"), "tcnSheet", 5, CStr(0),  , GetLocalResourceObject("tcnSheetColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescBatchColumnCaption"), "tctDescBatch", 30, "",  , GetLocalResourceObject("tctDescBatchColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctKeyColumnCaption"), "tctKey", 20, "",  , GetLocalResourceObject("tctKeyColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctUserColumnCaption"), "tctUser", 12, "",  , GetLocalResourceObject("tctUserColumnToolTip"),  ,  ,  , True)
		
		'+Se supone que sólo se activa PopUp cuando esta en estado Deshabilitado
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chkActiveColumnCaption"), "chkActive", "", CShort("1"), "1")
		Else
			Call .AddAnimatedColumn(0, GetLocalResourceObject("btnStatusColumnCaption"), "btnStatus", "", GetLocalResourceObject("btnStatusColumnToolTip"),  ,  , True)
		End If
		
		Call .AddTextColumn(0, GetLocalResourceObject("tctSubmitColumnCaption"), "tctSubmit", 20, "",  , GetLocalResourceObject("tctSubmitColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctRunColumnCaption"), "tctRun", 20, "",  , GetLocalResourceObject("tctRunColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctStopColumnCaption"), "tctStop", 20, "",  , GetLocalResourceObject("tctStopColumnToolTip"),  ,  ,  , True)
		
		'Call .AddNumericColumn (0, "Porcentaje avance", "tcnPercent", 3, 0, , "Porcentaje de avance del proceso")
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "BTC001"
		.sCodisplPage = "BTC001"
		.AddButton = False
		.ActionQuery = mobjValues.ActionQuery
		.Height = 380
		.Width = 340
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	'+Se almacenan los parametros ingresados por el usuario en la primera página    
	Response.Write(mobjValues.HiddenControl("hddBatch", lintBatch))
	Response.Write(mobjValues.HiddenControl("hddUser", lintUser))
	Response.Write(mobjValues.HiddenControl("hddProcDate", ldtmProcDate))
	Response.Write(mobjValues.HiddenControl("hddnsheet", lintSheet))
	
	
End Sub

'% insPreCodispl: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreBTC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsJobs As eSchedule.Batch_job
	Dim t As Double
	
	lclsJobs = New eSchedule.Batch_job
	
	If lclsJobs.Find_Batch_Job(lintBatch, lintUser, mobjValues.StringToType(ldtmProcDate,eFunctions.Values.eTypeData.etdDate), lintSheet) Then
		t = 1
		While lclsJobs.ItemBatchJob(t)
			With mobjGrid
				.Columns("tctKey").DefValue = lclsJobs.sKey
				.Columns("tctUser").DefValue = lclsJobs.sUsercodeDesc
				.Columns("tcnBatch").DefValue = CStr(lclsJobs.nBatch)
				.Columns("tcnSheet").DefValue = CStr(lclsJobs.nSheet)
				'.Columns("tcnSheet).DefValue = lclsJobs.nSheet
				'.Columns("tctDescBatch").DefValue = lclsJobs.sDescBatch
				.Columns("tctDescBatch").DefValue = lclsJobs.sDescSheet
				If lclsJobs.dSubmit <> eRemoteDB.Constants.dtmNull Then
					.Columns("tctSubmit").DefValue = lclsJobs.dSubmit & " "
				Else
					.Columns("tctSubmit").DefValue = ""
				End If
				If lclsJobs.dStart <> eRemoteDB.Constants.dtmNull Then
					.Columns("tctRun").DefValue = lclsJobs.dStart & " " 
				Else
					.Columns("tctRun").DefValue = ""
				End If
				If lclsJobs.dEnd <> eRemoteDB.Constants.dtmNull Then
					.Columns("tctStop").DefValue = lclsJobs.dEnd & " "
				Else
					.Columns("tctStop").DefValue = ""
				End If
				.Columns("btnStatus").Src = "/VTimeNet/images/btcStat0" & lclsJobs.nStatus & ".gif"
				.Columns("btnStatus").sAlias = lclsJobs.sStatusDesc
				
				'+Solo se permite editar registro cuando proceso tiene estado Deshabilitado (para habilitarlo)
				.Columns("tctKey").HRefScript = ""
				.Columns("tctKey").EditRecord = CBool(lclsJobs.nStatus = eSchedule.Batch_job.enmBatchStatus.batchStatusDisabled)
				
				'+Se debe habilitar el mostrar los resultados
				.Columns("btnStatus").HRefScript = ""
				If (lclsJobs.nStatus = eSchedule.Batch_job.enmBatchStatus.batchStatusOk) Then
					.Columns("btnStatus").sAlias = "Mostrar resultados"
					.Columns("btnStatus").Disabled = False
					.Columns("btnStatus").HRefScript = "insShowResult('" & lclsJobs.sKey & "', '" & lclsJobs.sDescBatch & "', '" & lclsJobs.nBatch & "', '" & lclsJobs.nSheet & "');"
				End If
				If (lclsJobs.nStatus = eSchedule.Batch_job.enmBatchStatus.batchStatusErr) Then
					.Columns("btnStatus").sAlias = "Mostrar errores"
					.Columns("btnStatus").Disabled = False
					.Columns("btnStatus").HRefScript = "insShowError('" & lclsJobs.sKey & "', '" & lclsJobs.sDescBatch & "', '" & lclsJobs.nBatch & "');"
				End If
				
				'.Columns("tcnPercent").DefValue = lclsJobs.nRunpercent
				.sDelRecordParam = "nBatch=" & lintBatch & "&nBatchDel=' + marrArray[lintIndex].tcnBatch " & " + '&sKey=' + marrArray[lintIndex].tctKey + '"
				.sEditRecordParam = "nBatch=" & lintBatch & "&nUser=" & lintUser & "&dProcDate=" & ldtmProcDate & "&nSheet=" & lintSheet
				Response.Write(.DoRow)
			End With
			t = t + 1
		End While
	End If
	lclsJobs = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCodisplUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreBTC001Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsBatch_job As eSchedule.Batch_job
	
	lclsBatch_job = New eSchedule.Batch_job
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsBatch_job.DeleteResult(CInt(.QueryString.Item("nBatchDel")), .QueryString.Item("sKey")) Then
			End If
		End If
		
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valBatch.aspx", "BTC001", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	lclsBatch_job = Nothing
End Sub

</script>
<%Response.Expires = 0

Response.Buffer = False
Server.ScriptTimeOut = 3000

mobjValues = New eFunctions.Values

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

lintBatch = mobjValues.StringToType(Request.QueryString.Item("nBatch"), eFunctions.Values.eTypeData.etdLong)
lintUser = mobjValues.StringToType(Request.QueryString.Item("nUser"), eFunctions.Values.eTypeData.etdLong)
ldtmProcDate = Request.QueryString.Item("dProcDate")
lintSheet = mobjValues.StringToType(Request.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdLong)

%>
<html>
<head>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%Response.Write(mobjValues.StyleSheet())%>	




<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>

<script LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 9-09-09 19:38 $|$$Author: Mpalleres $"


//% insShowResult: Invoca a página que muestra resultados de proceso
//--------------------------------------------------------------------------------------------
function insShowResult(sKey, sDescBatch, nBatch,nsheet){
//--------------------------------------------------------------------------------------------
    insDefValues('BatchResult', 'nBatch=' + nBatch + 
                                '&sKey='   + sKey +
                                '&sDescBatch=' + sDescBatch +
                                '&nsheet=' + nsheet,
                                '/VTimeNet/batch/batchtra', 'resBatch');
    //ShowPopUp('/VTimeNet/interface/InterfaceSeq/GI1407.aspx?sCodispl=GI1407', 'EndProcess',1000,500);
    
}

//% insShowError: Invoca a página que muestra errores de proceso
//--------------------------------------------------------------------------------------------
function insShowError(sKey, sDescBatch, nBatch){
//--------------------------------------------------------------------------------------------
    insDefValues('BatchError',  'sDescBatch=' + sDescBatch + 
                                '&sKey='   + sKey + 
                                '&nBatch=' + nBatch, 
                                '/VTimeNet/batch/batchtra', 'resBatch');
}
</script>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write(mobjMenu.setZone(2, "BTC001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	mobjMenu = Nothing
End If
%>
</head>
<body ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("BTC001", Request.QueryString.Item("sWindowDescript")))%>
<form METHOD="POST" NAME="BTC001" ACTION="valBatch.aspx?sMode=2">
<%Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreBTC001Upd()
Else
	Call insPreBTC001()
End If
%>
</form> 
</body>
</html>





