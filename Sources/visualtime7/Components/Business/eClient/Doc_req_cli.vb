Option Strict Off
Option Explicit On
Public Class Doc_req_cli
	'%-------------------------------------------------------%'
	'% $Workfile:: Doc_req_cli.cls                          $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 3/05/04 5:44p                                $%'
	'% $Revision:: 33                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.doc_req_cli al 09-26-2002 19:02:44
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nEval As Double ' NUMBER     22   0     10   N
	Public nId As Double ' NUMBER     22   0     10   N
	Public nTypedoc As Integer ' NUMBER     22   0     5    S
	Public nStatusdoc As Integer ' NUMBER     22   0     5    N
	Public sDescript As String ' VARCHAR2   40   0     0    S
	Public dDocreq As Date ' DATE       7    0     0    N
	Public dDocrec As Date ' DATE       7    0     0    S
	Public dDocdate As Date ' DATE       7    0     0    N
	Public dExpirdat As Date ' DATE       7    0     0    S
	Public nCapital As Double ' NUMBER     22   6     18   N
	Public nNotenum As Double ' NUMBER     22   0     10   S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public dDateto As Date ' DATE       7    0     0    S
	Public dDatefree As Date ' DATE       7    0     0    S
	Public sRequest As String
	Public nCount As Integer
	Public sRequire As String ' CHAR       1    0     0    N
	
	Public nExists As Double
	
	'% InsUpdDoc_req_cli: Se encarga de actualizar la tabla Doc_req_cli
	Private Function InsUpdDoc_req_cli(ByVal nAction As Integer) As Boolean
		
		Dim lrecinsUpddoc_req_cli As eRemoteDB.Execute
		
		On Error GoTo insUpddoc_req_cli_Err
		lrecinsUpddoc_req_cli = New eRemoteDB.Execute
		
		With lrecinsUpddoc_req_cli
			.StoredProcedure = "insUpddoc_req_cli"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEval", nEval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypedoc", nTypedoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatusdoc", nStatusdoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDocreq", dDocreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDocrec", dDocrec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDocdate", dDocdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateto", dDateto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDatefree", dDatefree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequest", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdDoc_req_cli = .Run(False)
		End With
		
insUpddoc_req_cli_Err: 
		If Err.Number Then
			InsUpdDoc_req_cli = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpddoc_req_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpddoc_req_cli = Nothing
		On Error GoTo 0
	End Function
	
	'% Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdDoc_req_cli(1)
	End Function
	
	'% Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdDoc_req_cli(2)
	End Function
	
	'% Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdDoc_req_cli(3)
	End Function
	
	'% InsValBC803: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(BC803)
	Public Function InsValBC803(ByVal sAction As String, ByVal nEval As Double, ByVal nTypedoc As Integer, ByVal nStatusdoc As Integer, ByVal dDocreq As Date, ByVal dDocrec As Date, ByVal dDocdate As Date, ByVal dExpirdat As Date, ByVal nNotenum As Double, ByVal dDateto As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValBC803_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Fecha de solicitud debe estar llena
			If dDocreq = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage("BC803", 55951)
			End If
			
			'+Fecha documento debe estar llena
			If dDocdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage("BC803", 55952)
			End If
			
			'+Fecha de solicitud >= vigencia desde
			If dDocreq <> eRemoteDB.Constants.dtmNull And dDocdate <> eRemoteDB.Constants.dtmNull And dDocreq < dDocdate Then
				.ErrorMessage("BC803", 55630)
			End If
			
			'+Fecha de recepcion dentro vigencia
			If dDocrec <> eRemoteDB.Constants.dtmNull And dDocdate <> eRemoteDB.Constants.dtmNull And dExpirdat <> eRemoteDB.Constants.dtmNull And (dDocrec < dDocdate Or dDocrec > dExpirdat) Then
				.ErrorMessage("BC803", 55631)
			End If
			
			'+Fecha de prorroga dentro vigencia
			If dDateto <> eRemoteDB.Constants.dtmNull And dDocdate <> eRemoteDB.Constants.dtmNull And dExpirdat <> eRemoteDB.Constants.dtmNull And (dDateto < dDocdate Or dDateto > dExpirdat) Then
				.ErrorMessage("BC803", 55801)
			End If
			
			'+Valida el estado del documento
			If nStatusdoc = eRemoteDB.Constants.intNull Then
				.ErrorMessage("BC803", 55633)
			Else
				
				'+Valida la nota segun el estado del documento
				If (nStatusdoc = 3 Or nStatusdoc = 4) And nNotenum = eRemoteDB.Constants.intNull Then
					.ErrorMessage("BC803", 55634)
				End If
				
				'+Valida si el documento es aprobado y la fecha de recepción no esta llena
				If nStatusdoc = 2 And dDocrec = eRemoteDB.Constants.dtmNull Then
					.ErrorMessage("BC803", 4101)
				End If
				
				If sAction <> "Update" Then
					If valDocreqcli_uni(nEval, nTypedoc) Then
						.ErrorMessage("BC803", 55734)
					End If
				End If
				
			End If
			
			InsValBC803 = .Confirm
		End With
		
InsValBC803_Err: 
		If Err.Number Then
			InsValBC803 = "InsValBC803: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValBCL805: Validaciones de la transacción(Folder)
	Public Function InsValBCL805(ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValBC803_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Fecha de proceso debe estar llena
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage("BCL805", 1967)
			Else
				'+ Fecha de proceso debe ser menor o igual al dia en curso
				If dEffecdate > Today Then
					.ErrorMessage("BCL805", 1965)
				End If
			End If
			
			InsValBCL805 = .Confirm
		End With
		
InsValBC803_Err: 
		If Err.Number Then
			InsValBCL805 = "InsValBCL805: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'% InsPostBC803: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(BC803)
	Public Function InsPostBC803(ByVal sAction As String, ByVal nEval As Double, ByVal nId As Double, ByVal nTypedoc As Integer, ByVal nStatusdoc As Integer, ByVal sDescript As String, ByVal dDocreq As Date, ByVal dDocrec As Date, ByVal dDocdate As Date, ByVal dExpirdat As Date, ByVal nCapital As Double, ByVal nNotenum As Double, ByVal nUsercode As Integer, ByVal dDateto As Date, ByVal dDatefree As Date) As Boolean
		Dim lstrContent As String
		On Error GoTo InsPostBC803_Err
		
		With Me
			.nEval = nEval
			.nId = nId
			.nTypedoc = nTypedoc
			.nStatusdoc = nStatusdoc
			.sDescript = sDescript
			.dDocreq = dDocreq
			.dDocrec = dDocrec
			.dDocdate = dDocdate
			.dExpirdat = dExpirdat
			.nCapital = nCapital
			.nNotenum = nNotenum
			.nUsercode = nUsercode
			.dDateto = dDateto
			.dDatefree = dDatefree
		End With
		
		Select Case sAction
			Case "Add"
				InsPostBC803 = Add
			Case "Update"
				InsPostBC803 = Update
			Case "Del"
				InsPostBC803 = Delete
		End Select
		
InsPostBC803_Err: 
		If Err.Number Then
			InsPostBC803 = False
		End If
		On Error GoTo 0
	End Function
	
	'% InsPostBCL805: Ejecuta el post de la transacción(BCL805)
	Public Function InsPostBCL805(ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecInsPostBCL805 As eRemoteDB.Execute
		
		On Error GoTo InsPostBCL805_Err
		lrecInsPostBCL805 = New eRemoteDB.Execute
		With lrecInsPostBCL805
			.StoredProcedure = "Upddoc_req_cli"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", nCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPostBCL805 = .Run(False)
			nCount = .Parameters("nCount").Value
		End With
		
InsPostBCL805_Err: 
		If Err.Number Then
			InsPostBCL805 = False
		End If
		'UPGRADE_NOTE: Object lrecInsPostBCL805 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostBCL805 = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nEval = eRemoteDB.Constants.intNull
		nId = eRemoteDB.Constants.intNull
		nTypedoc = eRemoteDB.Constants.intNull
		nStatusdoc = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		dDocreq = eRemoteDB.Constants.dtmNull
		dDocrec = eRemoteDB.Constants.dtmNull
		dDocdate = eRemoteDB.Constants.dtmNull
		dExpirdat = eRemoteDB.Constants.dtmNull
		nCapital = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		dDateto = eRemoteDB.Constants.dtmNull
		dDatefree = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Find: Inicializa las propiedades cuando se instancia la clase
	Public Function Find_O(ByVal nEval As Double, ByVal nId As Double) As Object
		Dim lrecreaDoc_req_cli_o As eRemoteDB.Execute
		Dim lclsreaDoc_req_cli_o As Doc_req_cli
		
		On Error GoTo reaDoc_req_cli_o_Err
		lrecreaDoc_req_cli_o = New eRemoteDB.Execute
		
		With lrecreaDoc_req_cli_o
			.StoredProcedure = "reaDoc_req_cli_o"
			.Parameters.Add("nEval", nEval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find_O = True
				Me.nEval = .FieldToClass("nEval")
				Me.nId = .FieldToClass("nId")
				Me.nTypedoc = .FieldToClass("nTypedoc")
				Me.nStatusdoc = .FieldToClass("nStatusdoc")
				Me.sDescript = .FieldToClass("sDescript")
				Me.dDocreq = .FieldToClass("dDocreq")
				Me.dDocrec = .FieldToClass("dDocrec")
				Me.dDocdate = .FieldToClass("dDocdate")
				Me.dExpirdat = .FieldToClass("dExpirdat")
				Me.nCapital = .FieldToClass("nCapital")
				Me.nNotenum = .FieldToClass("nNotenum")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.dDateto = .FieldToClass("dDateto")
				Me.dDatefree = .FieldToClass("dDatefree")
				Me.sRequest = .FieldToClass("sRequest")
				Me.sRequire = .FieldToClass("sRequire")
			Else
				Find_O = False
			End If
		End With
		
reaDoc_req_cli_o_Err: 
		If Err.Number Then
			Find_O = False
		End If
		'UPGRADE_NOTE: Object lrecreaDoc_req_cli_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDoc_req_cli_o = Nothing
		On Error GoTo 0
	End Function
	'% valExist_Eval_Client: Valida si existen registros en la tabla EVAL_MASTER, DOC_REQ_CLI
	Public Function valExist_Eval_Client(ByVal sClient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double, ByVal ncertif As Double) As Boolean
		Dim lrecvalExist_Eval_Client As eRemoteDB.Execute
		Dim lintExists As Integer
		On Error GoTo valExist_Eval_Client_Err
		lrecvalExist_Eval_Client = New eRemoteDB.Execute
		
		With lrecvalExist_Eval_Client
			.StoredProcedure = "valExist_Eval_Client"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", npolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", ncertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valExist_Eval_Client = True
			Else
				valExist_Eval_Client = False
			End If
		End With
		
valExist_Eval_Client_Err: 
		If Err.Number Then
			valExist_Eval_Client = False
		End If
		
		'UPGRADE_NOTE: Object lrecvalExist_Eval_Client may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalExist_Eval_Client = Nothing
		On Error GoTo 0
		
	End Function
	
	'% valExist_Eval_Client: Valida si existen registros en la tabla DOC_REQ_CLI
	Public Function valDocreqcli_uni(ByVal nEval As Double, ByVal nTypedoc As Integer) As Boolean
		Dim lrecvalDocreqcli_uni As eRemoteDB.Execute
		On Error GoTo valDocreqcli_uni_Err
		lrecvalDocreqcli_uni = New eRemoteDB.Execute
		
		With lrecvalDocreqcli_uni
			.StoredProcedure = "VALDOCREQCLI_UNI"
			.Parameters.Add("nEval", nEval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypedoc", nTypedoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valDocreqcli_uni = True
			Else
				valDocreqcli_uni = False
			End If
		End With
		
valDocreqcli_uni_Err: 
		If Err.Number Then
			valDocreqcli_uni = False
		End If
		
		'UPGRADE_NOTE: Object lrecvalDocreqcli_uni may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalDocreqcli_uni = Nothing
		On Error GoTo 0
		
	End Function
End Class






