Option Strict Off
Option Explicit On
Public Class Notes
	'%-------------------------------------------------------%'
	'% $Workfile:: Notes.cls                                $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 11/08/03 10:50a                              $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'-Propiedades según la tabla en el sistema el 13/01/2000.
	'-El campo llave corresponde a nNotenum y nConsec
	
	'Column_name                      Type                 Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'-------------------------------- -------------------- -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nNotenum As Integer 'Long     no       4           10    0     no       (n/a)              (n/a)
	Public nConsec As Integer 'Long     no       2           5     0     no       (n/a)              (n/a)
	Public sDescript As String 'char     no       60                      yes      yes                yes
	Public tDs_text As String 'text     no       16                      yes      (n/a)              (n/a)
	Public dCompdate As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public nRectype As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'Long     no       2           5     0     yes      (n/a)              (n/a)
	
	'-Propiedades auxiliares
	
	Public sClient As String
	Public sCliename As String
	
	'- Se define la variable para indicar el estado de cada instancia en la colección
	Public nStatusInstance As Integer
	
	'- Variable para almacenar temporalmente el número de la Nota
	Public mNewNotenum As Integer
	
	'-Numero original de la nota de la instancia en tratamiento
	Public nOldNotenum As Integer
	
	'**- Registration type to which the notes belong.
	'**- Unique values according to the table with ID 62.
	'- Tipo de Registro al que pertenecen las notas.
	'- Valores únicos según tabla con identificativo 62.
	
	Public Enum eTypeNotes
		clngAdendNote = 1 '-Anexos
		clngClientNote = 2 '-Notas del Cliente
		clngBenefNote = 3 '-Beneficiarios de texto libre
		clngNoteClause = 4 '-Cláusulas partic. de la póliza
		clngPolicyNote = 5 '-Notas de la póliza
		clngClauseNote = 6 '-Texto de cláusula
		clngSuspendNote = 7 '-Nota de suspención
		clngClaimNote = 8 '-Nota de Siniestros
		clngCarDamageNote = 9 '-Daños del vehículo
		clngRenCondNote = 10 '-Condiciones de renovación
		clngArtDetNotes = 11 '-Detalle de artículos
		clngReceiptNote = 12 '-Notas de recibos
		clngFinantialNote = 14 '-Contratos de Financiamiento
		clngNoteLedUpd = 16 ' Notas de las los asientos contables
		clngRiskNote = 17 '-Descripción Riesgo asegurado
		clngCovertextNote = 20 '-Texto de Cobertura
		clngNoteProperty = 21 '-Propiedades
		clngCashBankNote = 22 '-Notas de Caja y Banco.
		clngClaimCases = 23 '-Notas de los Casos de siniestros
		clngFinancialNote = 24 '-Notas de Conceptos financieros de un cliente
		clngCarDescriptNote = 25 '-Descripción del vehículo
		clngBudgetNote = 26 '-Definición de presupuestos
		clngClinicHistor = 27 '-Detalle del diagnóstico (Historia Clínica)
		clngNoteTransp = 28 ' Notas de las rutas aseguradas
		clngNoteObsPropo = 29 ' Observaciones de una propuesta
		clngNoteEvaluac = 30 ' Evaluación Restringida
        clngProfOrdNote = 31 ' Notas de solicitud de ordenes de servicio
        clngRecDiscNote = 32 ' Notas de recargos, descuentos e impuestyos
	End Enum
	
	'**-Indicator to know if the instance in the collection exists in the data base.
	'-Indicador para saber si la instancia en la colección existe en la base de datos
	
	Public mblnExists As Boolean
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Notes"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Notes"
	Public Function Find(ByVal NoteNum As Integer, ByVal Consec As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaNotes As eRemoteDB.Execute
		
		lrecreaNotes = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If NoteNum = nNotenum And Consec = nConsec And Not lblnFind Then
			Find = True
		Else
			
			'**+Parameter definition for stored procedure 'insudb.reaNotes'
			'**+Information read on January 13,2000  13:41:45
			'+Definición de parámetros para stored procedure 'insudb.reaNotes'
			'+Información leída el 13/01/2000 13:41:45
			
			With lrecreaNotes
				.StoredProcedure = "reaNote"
				.Parameters.Add("nNotenum", NoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nConsec", Consec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nNotenum = NoteNum
					nConsec = Consec
					sDescript = .FieldToClass("sDescript", strNull)
					tDs_text = .FieldToClass("tDs_text", strNull)
					dNulldate = .FieldToClass("dNulldate", dtmNull)
					nRectype = .FieldToClass("nRectype", numNull)
					dCompdate = .FieldToClass("dCompdate", dtmNull)
					nUsercode = .FieldToClass("nUsercode", numNull)
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecreaNotes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaNotes = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Notes".
	'%Devolviendo verdadero o falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreNote As eRemoteDB.Execute
		lreccreNote = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'+ Definición de parámetros para stored procedure 'insudb.creNote'
		'+ Información leída el 07/06/2000 02:01:32 PM
		
		With lreccreNote
			.StoredProcedure = "creNote"
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tDs_text", tDs_text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRectype", nRectype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNewNote", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				mNewNotenum = .Parameters.Item("nNewNote").Value
				If mNewNotenum <> 0 Then
					nNotenum = mNewNotenum
				End If
				Add = True
			Else
				Add = False
			End If
		End With
		'UPGRADE_NOTE: Object lreccreNote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreNote = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'%Delete: Este método se encarga de eliminar registros en la tabla "Notes".
	'%Devolviendo verdadero o falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecdelNote As eRemoteDB.Execute
		
		lrecdelNote = New eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		'+ Definición de parámetros para stored procedure 'insudb.delNote'
		'+ Información leída el 07/06/2000 02:03:34 PM
		
		With lrecdelNote
			.StoredProcedure = "delNote"
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelNote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelNote = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Notes".
	'%Devolviendo verdadero o falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecupdNote As eRemoteDB.Execute
		
		lrecupdNote = New eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		'+ Definición de parámetros para stored procedure 'Insudb.updNote'
		'+ Información leída el 07/06/2000 02:06:08 PM
		
		With lrecupdNote
			.StoredProcedure = "updNote"
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tDs_text", tDs_text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdNote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdNote = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'% AddUpdate: crea o actualiza los datos de la nota
	Public Function AddUpdate() As Boolean
		Dim lrecinsNotesSCA002 As eRemoteDB.Execute
		
		lrecinsNotesSCA002 = New eRemoteDB.Execute
		
		On Error GoTo AddUpdate_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insNotesSCA002'
		'+ Información leída el 13/01/2000 16:15:46
		
		With lrecinsNotesSCA002
			.StoredProcedure = "insNotesSCA002"
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tDs_text", tDs_text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRectype", nRectype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nNotenum = .Parameters.Item("nNotenum").Value
				AddUpdate = True
			Else
				AddUpdate = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsNotesSCA002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsNotesSCA002 = Nothing
		
AddUpdate_Err: 
		If Err.Number Then
			AddUpdate = False
		End If
		On Error GoTo 0
	End Function
	
	'%CopyNotes: Esta rutina se encarga de copiar el contenido de una nota cambiando el número.
	Public Function CopyNotes(ByVal nNote As Integer, ByVal nRectype As Integer, ByVal nUsercode As Integer) As Integer
		Dim lrecinsCopyNotes As eRemoteDB.Execute
		
		lrecinsCopyNotes = New eRemoteDB.Execute
		
		On Error GoTo CopyNotes_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insCopyNotes'
		'+ Información leída el 28/06/2000 09:53:23 AM
		
		CopyNotes = 0
		
		With lrecinsCopyNotes
			.StoredProcedure = "insCopyNotes"
			.Parameters.Add("nNotenum", nNote, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRectype", nRectype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNewNote", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				CopyNotes = .Parameters.Item("nNewNote").Value
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsCopyNotes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCopyNotes = Nothing
		
CopyNotes_Err: 
		If Err.Number Then
			CopyNotes = 0
		End If
		On Error GoTo 0
	End Function
	
	'%Delete: Este método se encarga de eliminar registros en la tabla "Notes".
	'%Devolviendo verdadero o falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function DeleteNote(ByVal nNotenum As Integer) As Boolean
		Dim lrecdelNotes As eRemoteDB.Execute
		
		On Error GoTo DeleteNote_Err
		'+ Definición de parámetros para stored procedure 'delNotes'
		lrecdelNotes = New eRemoteDB.Execute
		With lrecdelNotes
			.StoredProcedure = "delNotes"
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteNote = .Run(False)
		End With
		
DeleteNote_Err: 
		If Err.Number Then
			DeleteNote = False
		End If
		'UPGRADE_NOTE: Object lrecdelNotes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelNotes = Nothing
		On Error GoTo 0
	End Function
	
	'%InitValues: Se inicializan los valores de las variables públicas de la clase
	Private Sub InitValues()
		nNotenum = numNull
		nConsec = numNull
		sDescript = String.Empty
		tDs_text = String.Empty
		dNulldate = CDate(Nothing)
		nRectype = numNull
		nUsercode = numNull
	End Sub
	
	'%Class_Initialize: Se controla la creación del objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






