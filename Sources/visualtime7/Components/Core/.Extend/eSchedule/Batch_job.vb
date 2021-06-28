Option Strict Off
Option Explicit On
Public Class Batch_job
	'%-------------------------------------------------------%'
	'% $Workfile:: Batch_job.cls                            $%'
	'% $Author:: Mpalleres                                  $%'
	'% $Date:: 9-09-09 19:22                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Public sKey As String
	Public nBatch As Integer
	Public sDescBatch As String
	Public nGroup As Integer
	Public dSubmit As Date
	Public sSubmitHour As String
	Public dStart As Date
	Public sStartHour As String
	Public dEnd As Date
	Public sEndHour As String
	Public sStatusDesc As String
	Public nRunpercent As Integer
	Public nUsercode As Integer
	Public nStatus As enmBatchStatus
	Public sUsercodeDesc As String
	Public sSheet As String
	Public sOutputFile As String
	Public sStatus As String
	Public sView_Interface As String
	Public sDirOut As String
	Public nSheet As Integer
    Public sDescSheet As String
    Public sFile As String
	
	Public Enum enmBatchStatus
		batchStatusDisabled = 0
		batchStatusActive = 1
		batchStatusSend = 2
		batchStatusRun = 3
		batchStatusErr = 4
		batchStatusOk = 5
    End Enum

    '-Area de parametros
    Public Enum enmAreaParameters
        batchParAreaProc = 1 'Parametros del proceso masivo
        batchParAreaRes = 2 'Parametros para procesar resultados
    End Enum


	Private Structure udtBatchJob
		Dim sKey As String
		Dim nBatch As Integer
		Dim sDescBatch As String
		Dim nGroup As Integer
		Dim sCommand As String
		Dim dSubmit As Date
		Dim sSubmitHour As String
		Dim dStart As Date
		Dim sStartHour As String
		Dim dEnd As Date
		Dim sEndHour As String
		Dim nStatus As Integer
		Dim sStatusDesc As String
		Dim nRunpercent As Integer
		Dim nUsercode As Integer
		Dim sUsercodeDesc As String
		Dim sSheet As String
		Dim sOutputFile As String
		Dim sStatus As String
		Dim sView_Interface As String
		Dim sDirOut As String
		Dim nSheet As Integer
		Dim sDescSheet As String
	End Structure
	
	Private marrBatchJob() As udtBatchJob
	Private mlngCount As Integer
	
	'%Count: Retorna la cantidad de procesos creados para una transaccion
	'-------------------------------------
	Public ReadOnly Property Count() As Integer
		Get
			'-------------------------------------
			
			Count = mlngCount
			
		End Get
	End Property
	
	
	
	'%Find_Batch_Job: Busca los procesos ejecutados para una transaccion batch
	Public Function Find_Batch_Job(ByVal nBatch As Integer, Optional ByVal nUser As Integer = 0, Optional ByVal dProc As Date = #12:00:00 AM#, Optional ByVal nSheet As Integer = 0) As Boolean
		Const BLOCK_SIZE As Short = 50
		Dim lrecreaBatch_job As eRemoteDB.Execute
		
		On Error GoTo reaBatch_job_Err
		
		lrecreaBatch_job = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaBatch_job al 05-30-2003 19:10:04
		'+
		With lrecreaBatch_job
			.StoredProcedure = "reaBatch_job"
			.Parameters.Add("nBatch", nBatch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProc", dProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_Batch_Job = .Run(True)
			If Find_Batch_Job Then
				'UPGRADE_WARNING: Lower bound of array marrBatchJob was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim marrBatchJob(BLOCK_SIZE)
				mlngCount = 0
				Do While Not .EOF
					mlngCount = mlngCount + 1
					marrBatchJob(mlngCount).sKey = .FieldToClass("sKey")
					marrBatchJob(mlngCount).nBatch = .FieldToClass("nBatch")
					marrBatchJob(mlngCount).sDescBatch = .FieldToClass("sBatch")
					'marrBatchJob(mlngCount).sCommand = .FieldToClass("sCommand")
					marrBatchJob(mlngCount).dSubmit = .FieldToClass("dSubmit")
					marrBatchJob(mlngCount).sSubmitHour = .FieldToClass("sSubmitHour")
					marrBatchJob(mlngCount).dStart = .FieldToClass("dStart")
					marrBatchJob(mlngCount).sStartHour = .FieldToClass("sStartHour")
					marrBatchJob(mlngCount).dEnd = .FieldToClass("dEnd")
					marrBatchJob(mlngCount).sEndHour = .FieldToClass("sEndHour")
					marrBatchJob(mlngCount).nStatus = .FieldToClass("nStatus")
					marrBatchJob(mlngCount).sStatusDesc = .FieldToClass("sStatusdesc")
					'marrBatchJob(mlngCount).nRunpercent = .FieldToClass("nRunpercent")
					marrBatchJob(mlngCount).nUsercode = .FieldToClass("nUsercode")
					marrBatchJob(mlngCount).sUsercodeDesc = .FieldToClass("sUsercodeDesc")
					marrBatchJob(mlngCount).nSheet = .FieldToClass("nSheet")
					marrBatchJob(mlngCount).sDescSheet = .FieldToClass("sDescSheet")
					
					If mlngCount Mod BLOCK_SIZE = 0 Then
						'UPGRADE_WARNING: Lower bound of array marrBatchJob was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
						ReDim Preserve marrBatchJob(mlngCount + BLOCK_SIZE)
					End If
					
					.RNext()
				Loop 
				.RCloseRec()
				'UPGRADE_WARNING: Lower bound of array marrBatchJob was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim Preserve marrBatchJob(mlngCount)
			End If
		End With
		
reaBatch_job_Err: 
		If Err.Number Then
			Find_Batch_Job = False
		End If
		'UPGRADE_NOTE: Object lrecreaBatch_job may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBatch_job = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_Interface_Batch_Job: Busca los procesos ejecutados para una transaccion batch
	Public Function Find_Interface_Batch_Job(ByVal sKey As String) As Boolean
		Const BLOCK_SIZE As Short = 20
		Dim lrecreaBatch_job As eRemoteDB.Execute
        Dim sPath As String = ""
        Dim NEXCELGENERATIONSIDE As Integer
		On Error GoTo reaBatch_job_Err
        lrecreaBatch_job = New eRemoteDB.Execute
        With lrecreaBatch_job
            .StoredProcedure = "Reaopt_interface"
            If .Run(True) Then
                sPath = .FieldToClass("sDirview")
                NEXCELGENERATIONSIDE = .FieldToClass("NEXCELGENERATIONSIDE")
                .RCloseRec()
            End If
        End With

        lrecreaBatch_job = Nothing

        If sPath <> vbNullString Then
            lrecreaBatch_job = New eRemoteDB.Execute

            '+
            '+ Definición de store procedure reaBatch_job al 05-30-2003 19:10:04
            '+
            With lrecreaBatch_job
                .StoredProcedure = "REAINTERFACE_BATCH_JOB"
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find_Interface_Batch_Job = .Run(True)
                If Find_Interface_Batch_Job Then
                    'UPGRADE_WARNING: Lower bound of array marrBatchJob was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                    ReDim marrBatchJob(BLOCK_SIZE)
                    mlngCount = 0
                    Do While Not .EOF
                        mlngCount = mlngCount + 1
                        marrBatchJob(mlngCount).sKey = .FieldToClass("sKey")
                        marrBatchJob(mlngCount).sSheet = .FieldToClass("sSheet")
                        marrBatchJob(mlngCount).sOutputFile = .FieldToClass("sOutputFile")
                        marrBatchJob(mlngCount).sStatus = .FieldToClass("sStatus")
                        marrBatchJob(mlngCount).sView_Interface = .FieldToClass("sView_Interface")
                        marrBatchJob(mlngCount).sDirOut = .FieldToClass("sDirOut")

                        If .FieldToClass("nFormat") = 2 Then
                            If NEXCELGENERATIONSIDE = 2 Then
                                Call Create_file(.FieldToClass("sOutputFile"), sPath, sKey)
                            End If
                        End If

                        If mlngCount Mod BLOCK_SIZE = 0 Then
                            'UPGRADE_WARNING: Lower bound of array marrBatchJob was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                            ReDim Preserve marrBatchJob(mlngCount + BLOCK_SIZE)
                        End If

                        .RNext()
                    Loop
                    .RCloseRec()
                    'UPGRADE_WARNING: Lower bound of array marrBatchJob was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                    ReDim Preserve marrBatchJob(mlngCount)
                End If
            End With
        End If
reaBatch_job_Err:
        If Err.Number Then
            Find_Interface_Batch_Job = False
        End If
        'UPGRADE_NOTE: Object lrecreaBatch_job may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaBatch_job = Nothing
        On Error GoTo 0
	End Function
	
	
	'%ItemBatchJob: Carga un registro de transaccion en las
	'%              propiedades de la clase
	'----------------------------------------------------------
	Public Function ItemBatchJob(ByVal nIdx As Integer) As Boolean
		'----------------------------------------------------------
		If nIdx <= mlngCount Then
			With marrBatchJob(nIdx)
				Me.sKey = .sKey
				Me.nBatch = .nBatch
				Me.sDescBatch = .sDescBatch
				Me.nGroup = .nGroup
				Me.dSubmit = .dSubmit
				Me.sSubmitHour = .sSubmitHour
				Me.dStart = .dStart
				Me.sStartHour = .sStartHour
				Me.dEnd = .dEnd
				Me.sEndHour = .sEndHour
				Me.nStatus = .nStatus
				Me.sStatusDesc = .sStatusDesc
				Me.nRunpercent = .nRunpercent
				Me.nUsercode = .nUsercode
				Me.sUsercodeDesc = .sUsercodeDesc
				Me.sSheet = .sSheet
				Me.sOutputFile = .sOutputFile
				Me.sStatus = .sStatus
				Me.sView_Interface = .sView_Interface
				Me.sDirOut = .sDirOut
				Me.nSheet = .nSheet
				Me.sDescSheet = .sDescSheet
			End With
			ItemBatchJob = True
		Else
			ItemBatchJob = False
		End If
	End Function
	
	'%DeleteResult: Limpia las tablas temporales de un proceso
	'----------------------------------------------------------
	Public Function DeleteResult(ByVal nBatch As Integer, ByVal sKey As String) As Boolean
		'----------------------------------------------------------
		Dim lrecinsDelbatch_job_result As eRemoteDB.Execute
		On Error GoTo DeleteResult_Err
		
		lrecinsDelbatch_job_result = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insDelbatch_job_result al 12-03-2003 16:49:46
		'+
		With lrecinsDelbatch_job_result
			.StoredProcedure = "insDelbatch_job_result"
			.Parameters.Add("nBatch", nBatch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			DeleteResult = .Run(False)
		End With
		
DeleteResult_Err: 
		If Err.Number Then
			DeleteResult = False
		End If
		'UPGRADE_NOTE: Object lrecinsDelbatch_job_result may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelbatch_job_result = Nothing
		On Error GoTo 0
		
	End Function
	
	
	
	'%Update_status: Actualiza el estado de un batch_job
	'-----------------------------------------------
	Public Function Update_status(ByVal sKey As String, ByVal nStatus As enmBatchStatus, ByVal nUsercode As Integer) As Boolean
		'-----------------------------------------------
		Dim lrecupdBatch_job_status As eRemoteDB.Execute
		On Error GoTo Update_status_Err
		
		lrecupdBatch_job_status = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure updBatch_job_status al 12-10-2003 13:49:37
		'+
		With lrecupdBatch_job_status
			.StoredProcedure = "updBatch_job_status"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update_status = .Run(False)
		End With
		
Update_status_Err: 
		If Err.Number Then
			Update_status = False
		End If
		'UPGRADE_NOTE: Object lrecupdBatch_job_status may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBatch_job_status = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValBtc001: Este metodo se encarga de realizar las validaciones de la transaccion Btc001"
	Public Function InsValBtc001(ByVal sKey As String, ByVal nBatch As Integer) As String
		'-----  ----------------------------------------------------------------------------------------------------------
        Dim lstrErrorAll As String = String.Empty
		Dim lrecInsValBtc001 As eRemoteDB.Execute
		
		Dim lclsErrors As Object
		
		On Error GoTo InsValBtc001_Err
		
		lrecInsValBtc001 = New eRemoteDB.Execute
		With lrecInsValBtc001
			.StoredProcedure = "InsValBtc001"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBatch", nBatch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrorAll = .Parameters("Arrayerrors").Value
			End If
		End With
		
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				.ErrorMessage("BTC001",  ,  ,  ,  ,  , lstrErrorAll)
			End If
			InsValBtc001 = .Confirm
		End With
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
InsValBtc001_Err: 
		If Err.Number Then
			InsValBtc001 = "InsValBtc001: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecInsValBtc001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValBtc001 = Nothing
		On Error GoTo 0
    End Function



    Public Function FindBySkey(ByVal sKey As String) As Boolean
        '--------------------------------------------------------------------------
        Dim lrecreaBatch_job As eRemoteDB.Execute
        Dim lclsBatch_job As Batch_job

        On Error GoTo reaBatch_job_Err

        lrecreaBatch_job = New eRemoteDB.Execute

        With lrecreaBatch_job
            .StoredProcedure = "reaBatch_jobSkey"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                FindBySkey = True
                sKey = sKey
                nBatch = .FieldToClass("nBatch")
                dStart = .FieldToClass("dStart")
                dEnd = .FieldToClass("dEnd")
                nStatus = .FieldToClass("nStatus")
                nUsercode = .FieldToClass("nUsercode")
                dSubmit = .FieldToClass("dSubmit")
                nSheet = .FieldToClass("nSheet")
                sFile = .FieldToClass("sFile")
            Else
                FindBySkey = False
            End If
        End With

reaBatch_job_Err:
        If Err.Number > 0 Then
            FindBySkey = False
        End If
        lrecreaBatch_job = Nothing
        On Error GoTo 0

    End Function

    Private Function Create_file(ByVal sName As String, ByVal sPath As String, ByVal sKey As String)
        '--------------------------------------------------------------------------------------------

        Dim lrecreaVal_eval_doc As eRemoteDB.Execute
        Dim mvarSalidaExcel As Microsoft.Office.Interop.Excel.Application
        Dim totalcampos As Integer
        Dim i As Integer
        Dim recArray As Object
        Dim strDB As String
        Dim fldCount As Integer
        Dim recCount As Long
        Dim iCol As Integer
        Dim iRow As Integer
        Dim sValue As String


        Dim paso As String
        On Error GoTo Create_file_Err
        mvarSalidaExcel = New Microsoft.Office.Interop.Excel.Application

        Call mvarSalidaExcel.Workbooks.Add()
        mvarSalidaExcel.Cells.Select()
        With mvarSalidaExcel.Selection.Font
            .Name = "Arial"
            .Size = 10
            .Strikethrough = False
            .Superscript = False
            .Subscript = False
            .OutlineFont = False
            .Shadow = False
            .Underline = -4142
            .ColorIndex = -4105
        End With
        mvarSalidaExcel.Selection.Font.Bold = True
        mvarSalidaExcel.DisplayAlerts = False
        'Call a.Workbooks(1).Worksheets.Add
        lrecreaVal_eval_doc = New eRemoteDB.Execute

        With lrecreaVal_eval_doc
            .StoredProcedure = "ReaQuery"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                totalcampos = .FieldsCount
                For i = 1 To totalcampos
                    mvarSalidaExcel.Workbooks(1).Worksheets(1).Cells(1, i) = .FieldName(i - 1)
                Next
                mvarSalidaExcel.Selection.Font.Bold = False
                mvarSalidaExcel.Rows("1:1").Select()
                mvarSalidaExcel.Selection.Borders(5).LineStyle = -4142
                mvarSalidaExcel.Selection.Borders(6).LineStyle = -4142
                With mvarSalidaExcel.Selection.Borders(7)
                    .LineStyle = 1
                    .Weight = 2
                    .ColorIndex = -4105
                End With
                With mvarSalidaExcel.Selection.Borders(8)
                    .LineStyle = 1
                    .Weight = 2
                    .ColorIndex = -4105
                End With
                With mvarSalidaExcel.Selection.Borders(9)
                    .LineStyle = 1
                    .Weight = 2
                    .ColorIndex = -4105
                End With
                With mvarSalidaExcel.Selection.Borders(10)
                    .LineStyle = 1
                    .Weight = 2
                    .ColorIndex = -4105
                End With
                With mvarSalidaExcel.Selection.Borders(11)
                    .LineStyle = 1
                    .Weight = 2
                    .ColorIndex = -4105
                End With
                ''ESTA CONDICION SE DEBE REVISAR PORQUE YA ADO.NET NO TIENE OBJETOS DE TIPO RECORDSET
                If False AndAlso Val(Mid(mvarSalidaExcel.Version, 1, InStr(1, mvarSalidaExcel.Version, ".") - 1)) > 8 Then
                    'Call mvarSalidaExcel.Workbooks(1).Worksheets(1).Range("A2").CopyFromRecordset(.Recordset)
                Else
                    iRow = 0
                    Do While Not .EOF
                        For iCol = 1 To totalcampos
                            sValue = ""
                            On Error Resume Next
                            sValue = .FieldToClass(.FieldName(iCol - 1))
                            mvarSalidaExcel.Workbooks(1).Worksheets(1).Cells(iRow + 2, iCol + 1) = sValue
                        Next iCol 'next field
                        iRow = iRow + 1
                        .RNext()
                    Loop 'next record
                    '               mvarSalidaExcel.Workbooks(1).Worksheets(1).Cells(2, 1).Resize(recCount - 1, totalcampos - 1).Value = recArray
                    .RCloseRec()
                End If
                mvarSalidaExcel.Cells.Select()
                mvarSalidaExcel.Cells.EntireColumn.AutoFit()
                mvarSalidaExcel.Range("L4").HorizontalAlignment = -4131
                mvarSalidaExcel.Range("A1").Select()
                mvarSalidaExcel.Rows("1:1").Select()
                mvarSalidaExcel.Selection.Font.Bold = True
                With mvarSalidaExcel.Selection.Interior
                    .ColorIndex = 33
                End With
            Else
                mvarSalidaExcel.Workbooks(1).Worksheets(1).Cells(2, 1) = "No existen Datos para esta consulta"
            End If
            Call mvarSalidaExcel.Workbooks(1).SaveAs(sPath & "\" & sName)
            Call mvarSalidaExcel.Quit()
            mvarSalidaExcel = Nothing
        End With

Create_file_Err:
        If Err.Number > 0 Then
            mvarSalidaExcel.Workbooks(1).Worksheets(1).Cells(2, 1) = "Existe un problema con la consulta"
            mvarSalidaExcel.Workbooks(1).Worksheets(1).Cells(3, 1) = Err.Description
            Call mvarSalidaExcel.Workbooks(1).SaveAs(sPath & "\" & sName)
            Call mvarSalidaExcel.Quit()
            mvarSalidaExcel = Nothing

        End If
        lrecreaVal_eval_doc = Nothing
        On Error GoTo 0
    End Function
End Class





