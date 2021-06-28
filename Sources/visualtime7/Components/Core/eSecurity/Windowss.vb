Option Strict Off
Option Explicit On
Public Class Windowss
	Implements System.Collections.IEnumerable
	
	'-Variable de la coleccion
	Private mCol As Collection
	
	'**-Auxiliaries properties definition used in the SG009 window
	'**-restricted time of the transactions
	'-Se definen las propiedades auxiliares utilizadas en la ventana
	'-SG009 - Horario restringido de transacciones.
	
	Private mstrCodispl As String
	'**-Auxiliaries properties definition used in the window SG016
	'**-Actions in a window
	'-Se definen las propiedades auxiliares utilizadas en la ventana
	'-SG016 - Acciones de una ventana.
	
	Private mstrType_actio As String
	'**-Properties definition to be used in the page SGC002 -
	'**-Consultation of the system transactions
	'-Se definen las propiedades a ser utilizadas en la página SGC002 -
	'-Consulta de transacciones del sistema.
	
	Private pstrQueryUsers As String
	Private mstrCondition As String
	
	'**%AddWin_Hour: Adds the records of the table "Win_hour" to the collection
	'%AddWin_Hour: Permite añadir a la colección los registros de la tabla "Win_hour"
	Public Function AddWin_Hour(ByVal nStatusInstance As Integer, ByVal sCodispl As String, ByVal sHour_start As String, ByVal sHour_end As String, ByVal nUsercode As Integer) As Windows
		Dim objNewMember As eSecurity.Windows
		objNewMember = New eSecurity.Windows
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		'+Asigna propiedades
		With objNewMember
			.nStatusInstance = nStatusInstance
			.sCodispl = sCodispl
			.sHour_start = sHour_start
			.sHour_end = sHour_end
			.nUsercode = nUsercode
		End With
		
		'+Agrega objeto a colección
		mCol.Add(objNewMember, "A" & sCodispl & sHour_start)
		
		AddWin_Hour = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**%AddActions: Adds the records of the table "Actions" to the collection
	'%AddActions: Permite añadir a la colección los registros de la tabla Actions.
	Public Function AddActions(ByVal nStatusInstance As Integer, ByVal sSel As String, ByVal nAction As Integer, ByVal sControlkey As String, ByVal sDescript As String, ByVal sHel_actio As String, ByVal sShort_acti As String, ByVal sStatregt As String, ByVal sType_actio As String, ByVal nUsercode As Integer, ByVal sPathImage As String) As Windows
		
		Dim objNewMember As eSecurity.Windows
		objNewMember = New eSecurity.Windows
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		'+Asigna propiedades
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.sSel = sSel
			.nAction = nAction
			.sControlkey = sControlkey
			.sDescript = sDescript
			.sHel_actio = sHel_actio
			.sShort_acti = sShort_acti
			.sStatregt = sStatregt
			.sType_actio = sType_actio
			.nUsercode = nUsercode
			.sPathImage = sPathImage
		End With
		
		'+Agrega objeto a colección
		mCol.Add(objNewMember)
		
		AddActions = objNewMember
        objNewMember = Nothing
	End Function
	
	'**%AddWin_actions: Adds the records of the table "Win_actions" to the collection
	'%AddWin_actions: Permite añadir a la colección los registros de la tabla Win_actions.
	Public Function AddWin_actions(ByVal nStatusInstance As Integer, ByVal sCodispl As String, ByVal nAction As Integer, ByVal nSequence As Integer, ByVal nUsercode As Integer) As Windows
		'Create a new object.
		
		Dim objNewMember As eSecurity.Windows
		objNewMember = New eSecurity.Windows
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		'+Asigna propiedades
		With objNewMember
			.nStatusInstance = nStatusInstance
			.sCodispl = sCodispl
			.nAction = nAction
			.nSequence = nSequence
			.nUsercode = nUsercode
		End With
		
		'+Agrega objeto a colección
		mCol.Add(objNewMember, "A" & sCodispl & CStr(nAction))
		
		AddWin_actions = objNewMember
        objNewMember = Nothing
	End Function
	
	'%FindCodMen: Busca una transaccion en windows por el codigo de menu
	Public Function FindCodMen(ByVal sCodmen As String, Optional ByVal nIndValidate As Integer = 2, Optional ByVal sShe_code As String = "") As Boolean
		Dim lrecreaWindows_codmen As eRemoteDB.Execute
		Dim lclsWindows As Windows
		
		On Error GoTo reaWindows_codmen_Err
		
		lrecreaWindows_codmen = New eRemoteDB.Execute
		
		With lrecreaWindows_codmen
			.StoredProcedure = "reaWindows_codmen"
			.Parameters.Add("sCodmen", sCodmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndValidate", nIndValidate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SSCHE_CODE", sShe_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindCodMen = True
				Do While Not .EOF
					lclsWindows = New Windows
					lclsWindows.sCodispl = .FieldToClass("sCodispl")
					lclsWindows.sDescript = .FieldToClass("sDescript")
					lclsWindows.nImg_index = .FieldToClass("nImg_index")
					lclsWindows.nWindowTy = .FieldToClass("nWindowty")
					lclsWindows.nIndPermitted = .FieldToClass("nIndPermitted")
					lclsWindows.sCodisp = .FieldToClass("sCodisp")
					lclsWindows.nHeight = .FieldToClass("nHeight")
					lclsWindows.sExe_name = .FieldToClass("sExe_name")
					lclsWindows.sFoldername = .FieldToClass("sFoldername")
					Call Add(lclsWindows)
					'UPGRADE_NOTE: Object lclsWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsWindows = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindCodMen = False
			End If
		End With
		
reaWindows_codmen_Err: 
		If Err.Number Then
			FindCodMen = False
		End If
        lrecreaWindows_codmen = Nothing
		On Error GoTo 0
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal sCodispl As String) As Windows
		Get
			
			Item = mCol.Item("A" & sCodispl)
			
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'Used when retrieving the number of elements in the collection. Syntax: Debug.Print x.Count.
			
			Count = mCol.Count()
		End Get
	End Property

	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		mCol.Remove(vntIndexKey)
		
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%FindWin_hour: Verify that information exists in the table of the restricted hours
	'**%of transactions - "Win_hour"
	'%FindWin_hour: Verifica que exista información en la tabla de horarios restringidos de
	'%transacciones - Win_hour.
	Public Function FindWin_hour(ByVal sCodispl As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecWin_hour As eRemoteDB.Execute
		
		lrecWin_hour = New eRemoteDB.Execute
		
		On Error GoTo FindWin_hour_Err
		
		FindWin_hour = True
		
		If sCodispl <> mstrCodispl Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+Parameters defintion to stored procedure 'insudb.reaWin_hour'.
			'+Definición de parámetros para stored procedure 'insudb.reaWin_hour'.
			With lrecWin_hour
				.StoredProcedure = "reaWin_hour"
				.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrCodispl = sCodispl
					
					Do While Not .EOF
						Call AddWin_Hour(0, sCodispl, .FieldToClass("sHour_start"), .FieldToClass("sHour_end"), .FieldToClass("nUsercode"))
						
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindWin_hour = False
					
					mstrCodispl = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecWin_hour may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecWin_hour = Nothing
		End If
		
FindWin_hour_Err: 
		If Err.Number Then
			FindWin_hour = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%FindActions: Verify that information exists in the table "Actions" for a window - "Actions"
	'%FindActions: Verifica que exista información en la tabla de acciones de una ventana - Actions
	Public Function FindActions(ByVal sType_actio As String, ByVal sCodispl As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecActions As eRemoteDB.Execute
		On Error GoTo FindActions_Err
		
		lrecActions = New eRemoteDB.Execute
		
		FindActions = True
		
		If sType_actio <> mstrType_actio Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			'**+Parameters defintion to stored procedure "insudb.reaActionsBytype"
			'+Definición de parámetros para stored procedure "insudb.reaActionsBytype"
			
			With lrecActions
				.StoredProcedure = "reaActionsBytype1"
				
				.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_actio", sType_actio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrType_actio = sType_actio
					
					Do While Not .EOF
						Call AddActions(0, .FieldToClass("sSel"), .FieldToClass("nAction"), .FieldToClass("sControlkey"), .FieldToClass("sDescript"), .FieldToClass("sHel_actio"), .FieldToClass("sShort_acti"), .FieldToClass("sStatregt"), .FieldToClass("sType_actio"), .FieldToClass("nUsercode"), .FieldToClass("sPathImage"))
						
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindActions = False
					
					mstrType_actio = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecActions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecActions = Nothing
		End If
		
FindActions_Err: 
		If Err.Number Then
			FindActions = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%FindWin_actions: Verify that information exists in the table of associated actions of a window - "Win_actions"
	'%FindWin_actions: Verifica que exista información en la tabla de acciones asociadas a una ventana - "Win_actions"
	Public Function FindWin_actions(ByVal sCodispl As String, ByRef sType_actio As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecWin_actions As eRemoteDB.Execute
		
		On Error GoTo FindWin_actions_Err
		
		lrecWin_actions = New eRemoteDB.Execute
		
		FindWin_actions = True
		
		If sCodispl <> mstrCodispl Or sType_actio <> mstrType_actio Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			'**+Parameters definition to stored procedure "insudb.reaWin_actionsByType"
			'+Definición de parámetros para stored procedure "insudb.reaWin_actionsByType"
			
			With lrecWin_actions
				.StoredProcedure = "reaWin_actionsByType"
				
				.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_actio", sType_actio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrCodispl = sCodispl
					mstrType_actio = sType_actio
					
					Do While Not .EOF
						Call AddWin_actions(0, .FieldToClass("sCodispl"), .FieldToClass("nAction"), .FieldToClass("nSequence"), .FieldToClass("nUsercode"))
						
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindWin_actions = False
					
					mstrCodispl = CStr(Nothing)
					mstrType_actio = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecWin_actions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecWin_actions = Nothing
		End If
		
FindWin_actions_Err: 
		If Err.Number Then
			FindWin_actions = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insConstructWindows: Constructs the search conditon included in the window SGC002 - Consultation of the
	'**%system transactions
	'%insConstructWindows: Permite construir la condición de búsqueda incluída en la ventana SGC002 - Consulta de
	'%transacciones del sistema
	Public Function insConstructWindows(ByVal nModules As Integer, ByVal sCodispl As String, ByVal sCodisp As String, ByVal sPseudo As String) As Boolean
		Dim lclsUser As eSecurity.User
		Dim lstrCondition As String
		
		lclsUser = New eSecurity.User
		
		insConstructWindows = True
		
		lstrCondition = String.Empty
		pstrQueryUsers = String.Empty
		'**+Validates the field "Module"
		'+Se realizan las validaciones del campo "Módulo".
		
		If nModules <> 0 And nModules <> eRemoteDB.Constants.intNull Then
			If lclsUser.InsConstruct("Windows.nModules", CStr(nModules), User.eTypValConst.ConstNumeric, lstrCondition) Then
				pstrQueryUsers = Trim(pstrQueryUsers) & " AND " & lstrCondition
			End If
		End If
		'**+Validates the field "Logical"
		'+Se realizan las validaciones del campo "Lógico"
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sCodispl) And Not IsNothing(sCodispl) And Trim(sCodispl) <> String.Empty And Trim(sCodispl) <> "0" Then
			If lclsUser.InsConstruct("Windows.sCodispl", sCodispl, User.eTypValConst.ConstString, lstrCondition) Then
				pstrQueryUsers = Trim(pstrQueryUsers) & " AND " & lstrCondition
			End If
		End If
		'**+Validates the field "Physical"
		'+Se realizan las validaciones del campo "Físico"
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sCodisp) And Not IsNothing(sCodisp) And Trim(sCodisp) <> String.Empty And Trim(sCodisp) <> "0" Then
			If lclsUser.InsConstruct("Windows.sCodisp", sCodisp, User.eTypValConst.ConstString, lstrCondition) Then
				pstrQueryUsers = Trim(pstrQueryUsers) & " AND " & lstrCondition
			End If
		End If
		'**+Validates the field "Alias"
		'+Se realizan las validaciones del campo "Pseudónimo"
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sPseudo) And Not IsNothing(sPseudo) And Trim(sPseudo) <> String.Empty And Trim(sPseudo) <> "0" Then
			If lclsUser.InsConstruct("Windows.sPseudo", sPseudo, User.eTypValConst.ConstString, lstrCondition) Then
				pstrQueryUsers = Trim(pstrQueryUsers) & " AND " & lstrCondition
			End If
		End If
		
		insConstructWindows = FindPrepareQueryWindows(pstrQueryUsers, True)
	End Function
	
	'**%+insPrepareQueryWindows: Method that searches the table "Windows" depending of the condition that is included
	'%+insPrepareQueryWindows: Función que permite realizar la lectura de la tabla Windows dependiendo de la
	'%+condición incluída.
	Public Function FindPrepareQueryWindows(ByVal sCondition As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaUsers As eRemoteDB.Execute
		
		On Error GoTo FindPrepareQueryWindows_Err
		lrecReaUsers = New eRemoteDB.Execute
		
		FindPrepareQueryWindows = True
		
		If sCondition <> mstrCondition Or lblnFind Then
            mCol = Nothing
			mCol = New Collection
			
			'**+Parameters definition to stored procedure 'insudb.reaUsersQuery'
			'**+Data read on 01/15/2001 15.29.55
			'+Definición de parámetros para stored procedure 'insudb.reaUsersQuery'
			'+Información leída el 15/01/2001 15.29.55
			
			With lrecReaUsers
				.StoredProcedure = "reaWindowsQuerypkg.reaWindowsQuery"
				
				If sCondition <> String.Empty Then
					.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
                    .Parameters.Add("sCondition", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				If .Run Then
					mstrCondition = sCondition
					
					Do While Not .EOF
						Call AddWindows(.FieldToClass("sDescript"), .FieldToClass("nModules"), .FieldToClass("sCodispl"), .FieldToClass("sCodisp"), .FieldToClass("sPseudo"))
						
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindPrepareQueryWindows = False
					
					mstrCondition = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaUsers = Nothing
		End If
		
FindPrepareQueryWindows_Err: 
		If Err.Number Then
			FindPrepareQueryWindows = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%AddActions: Adds the records of the table "Actions" to the collection
	'%AddActions: Permite añadir a la colección los registros de la tabla Actions.
	Public Function AddWindows(ByVal sDescript As String, ByVal nModules As Integer, ByVal sCodispl As String, ByVal sCodisp As String, ByVal sPseudo As String) As Windows
		Dim objNewMember As eSecurity.Windows
		objNewMember = New eSecurity.Windows
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		'+Asigna variables
		With objNewMember
			.sDescript = sDescript
			.nModules = nModules
			.sCodispl = sCodispl
			.sCodisp = sCodisp
			.sPseudo = sPseudo
		End With
		
		'+Agrega objeto a colección
		mCol.Add(objNewMember, "A" & CStr(sCodispl))
		
		AddWindows = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Add: Agrega un nuevo registro a la colección de Windows
	Public Function Add(ByVal objClass As Windows) As Windows
		
		If objClass Is Nothing Then
			objClass = New Windows
		End If
		
		With objClass
			mCol.Add(objClass, "A" & .sCodispl)
		End With
		
		'+Retorna objeto creado
		Add = objClass
		
	End Function
End Class






