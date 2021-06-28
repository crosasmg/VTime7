Option Strict Off
Option Explicit On
Public Class Users
	Implements System.Collections.IEnumerable
	
	'- Local variable to hold collection
	
	Private mCol As Collection
	'- The auxiliaries variables used in the SGC001 are defined - User consultation window
	'- Se definen las variables auxiliares utilizadas en la ventana SGC001 - Consulta de usuarios.
	
	
	Private mintOffice As Integer
	Private mintDepartmen As Integer
	Private mstrSche_code As String
	'- Properties defintion to be used in the SGC001 - User consultation  of the system
	'- Se definen las propiedades a ser utilizadas en la ventana SGC001 - Consulta de usuarios del sistema.
	
	Private pstrQueryUsers As String
	
	Private mstrCondition As String
	
	'%AddUsers: Adds the records in the table "Actions" to the collection
	'%AddUsers: Permite añadir a la colección los registros de la tabla Actions.
	Public Function AddUsers(ByVal nUsercode As Integer, ByVal sSche_code As String, ByVal sCliename As String, ByVal sOffice As String, ByVal sDepartme As String) As User
		
		'+ Create a new object.
		
		Dim objNewMember As eSecurity.User
		objNewMember = New eSecurity.User
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		'+ Set the properties passed into the method.
		
		With objNewMember
			.nUsercode = nUsercode
			.sSche_code = sSche_code
			.sCliename = sCliename
			.sOffice = sOffice
			.sDepartme = sDepartme
		End With
		
		mCol.Add(objNewMember, "A" & CStr(nUsercode))
		
		'+ Return the object created.
		
		AddUsers = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Item: Returns an element of the collection (according to the index)
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As User
		Get
			'Used when referencing an element in the collection vntIndexKey contains either the Index or
			'Key to the collection, this is why it is declared as a Variant Syntax: Set foo = x.Item(xyz) or
			'Set foo = x.Item(5).
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Returns the number of elements that the collection has
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'Used when retrieving the number of elements in the collection. Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Enumerates the collection for use in a For Each...Next loop
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'This property allows you to enumerate this collection with the For...Each syntax.
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'Used when removing an element from the collection vntIndexKey contains either the Index or Key,
		'which is why it is declared as a Variant Syntax: x.Remove(xyz).
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'Creates the collection when this class is created.
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'Destroys collection when this class is terminated
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%FindPrepareQueryUsers: Function that reads the table "Users" depending on the include condition
	'%FindPrepareQueryUsers: Función que permite realizar la lectura de la tabla Users dependiendo de la
	'%                       condición incluída.
	Public Function FindPrepareQueryUsers(ByVal sCondition As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaUsers As eRemoteDB.Execute
		lrecReaUsers = New eRemoteDB.Execute
		
		FindPrepareQueryUsers = True
		
		On Error GoTo FindPrepareQueryUsers_Err
		
		If sCondition <> mstrCondition Or lblnFind Then
			
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'+ Definición de parámetros para stored procedure 'insudb.reaUsersQuery'
			'+ Información leída el 15/01/2001 15.29.55
			With lrecReaUsers
				.StoredProcedure = "reaUsersQuerypkg.reaUsersQuery"
				
				If sCondition <> String.Empty Then
					.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					.Parameters.Add("sCondition", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				If .Run(True) Then
					mstrCondition = sCondition
					
					Do While Not .EOF
						Call AddUsers(.FieldToClass("nUsercode"), .FieldToClass("sSche_code"), .FieldToClass("sCliename"), .FieldToClass("sOffice"), .FieldToClass("sDepartme"))
						
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindPrepareQueryUsers = False
					
					mstrCondition = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaUsers = Nothing
		End If
		
FindPrepareQueryUsers_Err: 
		If Err.Number Then
			FindPrepareQueryUsers = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%insConstructUsers: Constructs the search condition included in the window.
	'%insConstructUsers: Permite construir la condición de búsqueda incluída en la ventana.
	Public Function insConstructUsers(ByVal nOffice As Integer, ByVal nDepartmen As Integer, ByVal sSche_code As String) As Boolean
		Dim lclsUser As eSecurity.User
		Dim lstrCondition As String
		
		lclsUser = New eSecurity.User
		
		insConstructUsers = True
		
		lstrCondition = String.Empty
		pstrQueryUsers = String.Empty
		
		'+ Validates the field "Zone"
		'+ Se realizan las validaciones del campo "Zona".
		If nOffice <> 0 And nOffice <> eRemoteDB.Constants.intNull Then
			If lclsUser.InsConstruct("U.nOffice", CStr(nOffice), User.eTypValConst.ConstNumeric, lstrCondition) Then
				pstrQueryUsers = Trim(pstrQueryUsers) & " AND " & lstrCondition
			End If
		End If
		
		'+ Validates the field "Department"
		'+ Se realizan las validaciones del campo "Departamento".
		If nDepartmen <> 0 And nDepartmen <> eRemoteDB.Constants.intNull Then
			If lclsUser.InsConstruct("U.nDepartme", CStr(nDepartmen), User.eTypValConst.ConstNumeric, lstrCondition) Then
				pstrQueryUsers = Trim(pstrQueryUsers) & " AND " & lstrCondition
			End If
		End If
		
		'+ Validates the field "scheme"
		'+ Se realizan las validaciones del campo "Esquema"
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sSche_code) And Not IsNothing(sSche_code) And Trim(sSche_code) <> String.Empty And Trim(sSche_code) <> "0" Then
			If lclsUser.InsConstruct("U.sSche_code", sSche_code, User.eTypValConst.ConstString, lstrCondition) Then
				pstrQueryUsers = Trim(pstrQueryUsers) & " AND " & lstrCondition
			End If
		End If
		
		insConstructUsers = FindPrepareQueryUsers(pstrQueryUsers, True)
	End Function
End Class






