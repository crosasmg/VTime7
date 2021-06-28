Option Strict Off
Option Explicit On
Public Class Crit_sort
	'%-------------------------------------------------------%'
	'% $Workfile:: Crit_sort.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	'+ Definición de la tabla CRIT_SORT tomada el 17/09/2002 10:22
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	' ------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nCrthecni As Integer ' NUMBER        22     5      0 No
	Public nRandom As Integer ' NUMBER        22     5      0 Yes
	Public sSolic As String ' CHAR           1              Yes
	Public nCount As Integer ' NUMBER        22     5      0 Yes
	Public sStatregt As String ' CHAR           1              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	'- Propiedades auxiliares
	Public nExist As Integer
	Public sDescript As String
	Public lintExist As Integer
	
	Private Const cintActionAdd As Short = 1
	Private Const cintActionUpdate As Short = 2
	Private Const cintActionDel As Short = 3
	
	
	'% Find: Busca la información de un determinado exámen solicitado aleatoriamente
	Public Function Find(ByVal nCrthecni As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaCrit_sort As eRemoteDB.Execute
		Find = True
		On Error GoTo Find_Err
		
		If nCrthecni <> Me.nCrthecni Or bFind Then
			
			lrecreaCrit_sort = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaCrit_sort'
			'+ Información leída el 17/09/2002
			With lrecreaCrit_sort
				.StoredProcedure = "reaCrit_sort"
				.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nCrthecni = .FieldToClass("nCrthecni")
					Me.nRandom = .FieldToClass("nRandom")
					Me.sSolic = .FieldToClass("sSolic")
					Me.nCount = .FieldToClass("nCount")
					Me.sStatregt = .FieldToClass("sStatregt")
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaCrit_sort may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCrit_sort = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdCrit_sort(cintActionAdd)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdCrit_sort(cintActionUpdate)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdCrit_sort(cintActionDel)
	End Function
	
	'%InsValCrit_sort: Lee los datos de la tabla, valida la existencia de una fila
	Public Function InsValCrit_sort(ByVal nCrthecni As Integer, Optional ByVal nExist As Integer = 0) As Boolean
		Dim lrecreaCrit_sort_v As eRemoteDB.Execute
		
		On Error GoTo reaCrit_sort_v_Err
		
		lrecreaCrit_sort_v = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaCrit_sort 17-09-2002 19:42:00
		With lrecreaCrit_sort_v
			.StoredProcedure = "reaCrit_sort_v"
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValCrit_sort = .Parameters("nExist").Value = 1
			Else
				InsValCrit_sort = False
			End If
		End With
		
reaCrit_sort_v_Err: 
		If Err.Number Then
			InsValCrit_sort = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaCrit_sort_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCrit_sort_v = Nothing
		On Error GoTo 0
	End Function
	
	'% insValMVI816: Esta función se encarga de validar los datos del Form
	'% exámenes solicitados aleatoriamente
	Public Function insValMVI816(ByVal sCodispl As String, ByVal sAction As String, ByVal nCrthecni As Integer, ByVal nRandom As Integer, ByVal sSolic As String, ByVal nCount As Integer, ByVal sStatregt As String) As String
		
		'- Se define el objeto para el manejo de las clases
		Dim lobjErrors As eFunctions.Errors
		Dim lbError As Boolean
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValMVI816_Err
		lbError = False
		
		'+ Validación de Criterio
		With lobjErrors
			
			'+ Si el campo Criterio no esta lleno, ninguna de los campos debe estar lleno
			If nCrthecni = eRemoteDB.Constants.intNull Or nCrthecni = 0 Then
				If (nRandom <> 0 And nRandom <> eRemoteDB.Constants.intNull) Or (sStatregt <> String.Empty And sStatregt <> "0") Then
					Call .ErrorMessage(sCodispl, 1084)
				End If
			End If
			
			'+ Si la acción es registrar el campo criterio debe estar lleno
			If sAction = "Add" Then
				If (nCrthecni = eRemoteDB.Constants.intNull Or nCrthecni = 0) Then
					Call .ErrorMessage(sCodispl, 55875)
				Else
					If Find(nCrthecni) Then
						Call .ErrorMessage(sCodispl, 11171)
					End If
				End If
			End If
			
			'+ Si la acción es registrar el campo criterio debe estar lleno
			If nRandom = eRemoteDB.Constants.intNull Or nRandom = 0 Then
				Call .ErrorMessage(sCodispl, 55876)
			End If
			
			If nCrthecni <> eRemoteDB.Constants.intNull And nCrthecni <> 0 Then
				If sStatregt = String.Empty Or CDbl(sStatregt) = 0 Then
					Call .ErrorMessage(sCodispl, 9089)
				End If
			End If
		End With
		
		insValMVI816 = lobjErrors.Confirm
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValMVI816_Err: 
		If Err.Number Then
			insValMVI816 = "insValMVI816: " & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'%InsPostMVI816Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (MVI816)
	Public Function InsPostMVI816Upd(ByVal sAction As String, ByVal nCrthecni As Integer, ByVal nRandom As Integer, ByVal sSolic As String, ByVal nCount As Integer, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		
		On Error GoTo InsPostMVI816Upd_Err
		With Me
			.nCrthecni = nCrthecni
			.nRandom = nRandom
			.sSolic = sSolic
			.sStatregt = sStatregt
			.nCount = nCount
			.nUsercode = nUsercode
			
			If sAction = "Del" Then
				lintAction = cintActionDel
			Else
				If sAction = "Update" Then
					lintAction = cintActionUpdate
				Else
					If sAction = "Add" Then
						lintAction = cintActionAdd
					End If
				End If
			End If
			
			Select Case lintAction
				Case cintActionAdd
					
					'+ Se crea el registro
					InsPostMVI816Upd = .Add
					
					'+ Se modifica el registro
				Case cintActionUpdate
					InsPostMVI816Upd = .Update
					
					'+ Se elimina el registro
				Case cintActionDel
					InsPostMVI816Upd = .Delete
					
			End Select
		End With
		
InsPostMVI816Upd_Err: 
		If Err.Number Then
			InsPostMVI816Upd = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%InsUpdCrit_sort: Realiza la actualización de la tabla
	Private Function InsUpdCrit_sort(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdCrit_sort As eRemoteDB.Execute
		
		On Error GoTo InsUpdCrit_sort_Err
		
		lrecInsUpdCrit_sort = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'InsUpdCrit_sort'
		'+ Información leída el 17/09/2002
		With lrecInsUpdCrit_sort
			.StoredProcedure = "InsUpdCrit_sort"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRandom", nRandom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSolic", sSolic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", nCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdCrit_sort = .Run(False)
		End With
		
InsUpdCrit_sort_Err: 
		If Err.Number Then
			InsUpdCrit_sort = False
		End If
		
		'UPGRADE_NOTE: Object lrecInsUpdCrit_sort may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdCrit_sort = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nUsercode = eRemoteDB.Constants.intNull
		nCrthecni = eRemoteDB.Constants.intNull
		nRandom = eRemoteDB.Constants.intNull
		sSolic = CStr(eRemoteDB.Constants.intNull)
		sStatregt = CStr(dtmNull)
		nCount = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






