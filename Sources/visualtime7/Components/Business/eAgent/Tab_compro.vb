Option Strict Off
Option Explicit On
Public Class Tab_compro
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_compro.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on May 22,2001
	'+ Propiedades según la tabla en el sistema el 22/05/2001
	'**+ The key field corresponds to nType_tran,nLine.
	'+ El campo llave corresponde a nType_tran, nLine.
	
	'+ Column_name         Type                 Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nType_tran As Integer 'smallint 2      5     0    no       (n/a)              (n/a)
	Public nLine As Integer 'smallint 2      5     0    no       (n/a)              (n/a)
	Public nTyp_acco As Integer 'smallint 2      5     0    yes      (n/a)              (n/a)
	Public sDebitSide As String 'char     1                 yes      no                 yes
	Public nTyp_amount As Integer 'smallint 2      5     0    yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5     0    no       (n/a)              (n/a)
	
	Public nStatusInstance As Integer
	
	'**% Find: Searches the information of the Current Account automatic transactions table.
	'% Find: Busca la información de la tabla de Movimientos Automáticos de Cta. Cte.
	Public Function Find(ByVal nType_tran As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaTab_compro_v As eRemoteDB.Execute
		
		If nType_tran = Me.nType_tran And Not lblnFind Then
			Find = True
		Else
			
			lrecreaTab_compro_v = New eRemoteDB.Execute
			
			'**+ Parameter definition for the stored procedure 'insudb.reaTab_compro_v'
			'+Definición de parámetros para stored procedure 'insudb.reaTab_compro_v'
			'**+ Information read on May 22,2001 10:46:20 a.m.
			'+Información leída el 22/05/2001 10:46:20 a.m.
			
			With lrecreaTab_compro_v
				.StoredProcedure = "reaTab_compro_v"
				.Parameters.Add("PnType_tran", nType_tran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nType_tran = .FieldToClass("nType_tran")
					Me.nLine = .FieldToClass("nLine")
					Me.nTyp_acco = .FieldToClass("nTyp_acco")
					Me.sDebitSide = .FieldToClass("sDebitSide")
					Me.nTyp_amount = .FieldToClass("nTyp_amount")
					
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaTab_compro_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaTab_compro_v = Nothing
		End If
	End Function
	
	'**% Add: add information to the main table for the transaction
	'%Add: Esta función se encarga de agregar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Add() As Boolean
		
		Dim lreccreTab_compro As eRemoteDB.Execute
		
		lreccreTab_compro = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.CreTab_compr'
		'+Definición de parámetros para stored procedure 'insudb.creTab_compro'
		'**+ Information read on May 22,2001 10:58:11 a.m.
		'+Información leída el 22/05/2001 10:58:11 a.m.
		
		With lreccreTab_compro
			.StoredProcedure = "creTab_compro"
			.Parameters.Add("nType_tran", nType_tran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDebitSide", sDebitSide, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_amount", nTyp_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lreccreTab_compro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_compro = Nothing
		
	End Function
	
	'**% Update: This function updates information in the main table for the transaction.
	'%Update: Esta función se encarga de actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		Dim lrecupdTab_compro As eRemoteDB.Execute
		
		lrecupdTab_compro = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.updTab_compro'
		'+Definición de parámetros para stored procedure 'insudb.updTab_compro'
		'**+ Information read on May 22,2001 11:03:44 a.m.
		'+Información leída el 22/05/2001 11:03:44 a.m.
		
		With lrecupdTab_compro
			.StoredProcedure = "updTab_compro"
			
			.Parameters.Add("nType_tran", nType_tran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLine", nLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDebitSide", sDebitSide, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_amount", nTyp_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
			
		End With
		
		'UPGRADE_NOTE: Object lrecupdTab_compro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_compro = Nothing
		
	End Function
	
	'**% Delete: delete information in the main table for the transaction
	'%Delete: Esta función se encarga de eliminar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Delete() As Boolean
		
		Dim lrecdelTab_compro As eRemoteDB.Execute
		
		lrecdelTab_compro = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.delTab_compro'
		'+Definición de parámetros para stored procedure 'insudb.delTab_compro'
		'**+ Information read may 22,2001 01:50:05 p.m.
		'+Información leída el 22/05/2001 01:50:05 p.m.
		
		With lrecdelTab_compro
			.StoredProcedure = "delTab_compro"
			.Parameters.Add("nType_tran", nType_tran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLine", nLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		
		'UPGRADE_NOTE: Object lrecdelTab_compro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_compro = Nothing
		
	End Function
	
	'**% Find_Exist: validate that the record does not exists.
	'% Find_Exist: Esta funcion se encarga de validar que el registro no exista en la BD.
	Public Function Find_Exist() As Boolean
		Dim lrecreaTab_comproCount As eRemoteDB.Execute
		
		lrecreaTab_comproCount = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaTab_comproCount'
		'+Definición de parámetros para stored procedure 'insudb.reaTab_comproCount'
		'**+ Information read on May 22,2001 11:09:22 a.m.
		'+Información leída el 22/05/2001 11:09:22 a.m.
		
		With lrecreaTab_comproCount
			.StoredProcedure = "reaTab_comproCount"
			
			.Parameters.Add("PnType_tran", nType_tran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("PnTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("PsDebitSide", sDebitSide, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("PnTyp_amount", nTyp_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("PnLine", nLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_Exist = False
			
			If .Run() Then
				If .FieldToClass("nCount") <> 0 Then
					Find_Exist = True
				End If
				.RCloseRec()
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecreaTab_comproCount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_comproCount = Nothing
		
	End Function
	'**% insValMAG005_K: validate the data entered on the header form.
	'%insValMAG005_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMAG005_K(ByVal sCodispl As String, ByVal nAction As eFunctions.Menues.TypeActions, Optional ByVal nSeleted As Integer = 0, Optional ByVal nType_tran As Integer = 0) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lcolTab_compros As eAgent.Tab_compros
		Dim lblnErrors As Boolean
		
		Static lstrValField As String
		
		On Error GoTo insValMAG005_K_Err
		lclsErrors = New eFunctions.Errors
		
		lblnErrors = False
		
		'**+ validation of the Type of transaction
		'+Validación del Tipo de transaccion
		
		If nType_tran = eRemoteDB.Constants.intNull Or nType_tran = 0 Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 7133)
			
		End If
		
		'**+ In the case of query, validate that there is a record to be shown
		'+En el caso de consulta, se valida que existan registros a ser mostrados
		
		If Not lblnErrors And nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			
			If nType_tran = eRemoteDB.Constants.intNull Or nType_tran = 0 Then
				'UPGRADE_NOTE: Object lcolTab_compros may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lcolTab_compros = Nothing
				lcolTab_compros = New eAgent.Tab_compros
				
				If Not lcolTab_compros.Find(nType_tran) Then
					Call lclsErrors.ErrorMessage(sCodispl, 1073)
				End If
				'UPGRADE_NOTE: Object lcolTab_compros may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lcolTab_compros = Nothing
			End If
		End If
		
		insValMAG005_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMAG005_K_Err: 
		If Err.Number Then
			insValMAG005_K = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**%insValMAG005: validate the data entered on the detail zone for the form.
	'%insValMAG005: Esta función se encarga de validar los datos introducidos en la zona de detalle
	'%para la forma.
	Public Function insValMAG005(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nType_tran As Integer = 0, Optional ByVal nLine As Integer = 0, Optional ByVal nTyp_acco As Integer = 0, Optional ByVal sDebitSide As String = "", Optional ByVal nTyp_amount As Integer = 0) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_compro As eAgent.Tab_compro
		
		On Error GoTo insValMAG005_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ Verifies that the field Debit/Credit is filled in
		'+ Se verifica que el campo Débito/Crédito tenga contenido
		If sDebitSide = strNull Or sDebitSide = "0" Then
			Call lclsErrors.ErrorMessage(sCodispl, 9111)
		End If
		
		'**+ Verifies that the field Type of current account is filled in
		'+ Se verifica que el campo Tipo de Cuenta Corriente tenga contenido
		If nTyp_acco = eRemoteDB.Constants.intNull Or nTyp_acco = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 7107)
		End If
		
		'**+ Verifies that the field Type of amount to be assigned is filled in
		'+ Se verifica que el campo Tipo de Importe a Asignar tenga contenido
		If nTyp_amount = eRemoteDB.Constants.intNull Or nTyp_amount = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9008)
		End If
		
		'**+ Validates that the values does not exist in the BD.
		'+Se valida que el valor no se encuentre previamente registrado en la BD.
		If sDebitSide <> strNull And CDbl(sDebitSide) <> 0 And nTyp_acco <> eRemoteDB.Constants.intNull And nTyp_acco <> 0 And nTyp_amount <> eRemoteDB.Constants.intNull And nTyp_amount <> 0 Then
			
			lclsTab_compro = New eAgent.Tab_compro
			
			With lclsTab_compro
				.nLine = IIf(nLine = eRemoteDB.Constants.intNull, 0, nLine)
				.nType_tran = nType_tran
				.nTyp_acco = nTyp_acco
				.sDebitSide = sDebitSide
				.nTyp_amount = nTyp_amount
				
				If .Find_Exist() Then
					Call lclsErrors.ErrorMessage(sCodispl, 8307)
				End If
			End With
			
			'UPGRADE_NOTE: Object lclsTab_compro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsTab_compro = Nothing
		End If
		
		insValMAG005 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMAG005_Err: 
		If Err.Number Then
			insValMAG005 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% InsPostMAG005: This function is in charge of calling the correspondent method to the
	'**% executed action (creat/update/remove) over the Tab_compro table.
	'%InsPostMAG005: Esta función se encarga de llamar al método correspondiente a la acción
	'% ejecutada (crear/actualizar/eliminar) sobre la tabla Tab_compro
	Public Function insPostMAG005(ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nType_tran As Integer = 0, Optional ByVal nLine As Integer = 0, Optional ByVal sDebitSide As String = "", Optional ByVal nTyp_acco As Integer = 0, Optional ByVal nTyp_amount As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMAG005_err
		
		Me.nType_tran = nType_tran
		Me.nLine = nLine
		Me.sDebitSide = sDebitSide
		Me.nTyp_acco = nTyp_acco
		Me.nTyp_amount = nTyp_amount
		Me.nUsercode = nUsercode
		
		insPostMAG005 = True
		
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMAG005 = Add()
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMAG005 = Update()
				
				'**+ If the selected option is Remove
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMAG005 = Delete()
				
		End Select
		
insPostMAG005_err: 
		If Err.Number Then
			insPostMAG005 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'*** Class_Initialize: control the opening of the class
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'nUsercode = GetSetting("TIME", "GLOBALS", "USERCODE", 0)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






