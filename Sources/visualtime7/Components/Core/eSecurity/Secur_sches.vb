Option Strict Off
Option Explicit On
Public Class Secur_sches
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Secur_sches.cls                          $%'
	'% $Author:: Jrivero                                    $%'
	'% $Date:: 1/03/06 12:46p                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Local variable to contain the collection
	'-Variable local para contener colección
	
	Private mCol As Collection
	'**-Variables defintion
	'-Se definen las variables
	
	Private mstrSche_code As String
	Private mlngAction As Integer
	
	'**%AddsChema_cur: Adds the records found to the collection
	'%AddsChema_cur: Añade a la colección los registros encontr
	Public Function AddsChema_cur(ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nSel As Integer, ByVal sStatregt As String) As Secur_sche
		'**+Creates a new object
		'+crear un nuevo objeto
		
		Dim objNewMember As Secur_sche
		objNewMember = New Secur_sche
		
		'**+Establish the properties that transfer to the method
		'+Establecer las propiedades que se transfieren al método.
		
		With objNewMember
			.sSche_code = sSche_code
			.nCurrency = nCurrency
			.nSel = nSel
			.sStatregt = sStatregt
		End With
		
		mCol.Add(objNewMember, "A" & sSche_code & CStr(nCurrency))
		'**+Returns the created object
		'+Devolver el objeto creado.
		
		AddsChema_cur = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**%AddLimits: Adds the records found to the collection
	'%AddLimits: Añade a la colección los registros encontrados.
	Public Function AddLimits(ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nClaim_d As Double, ByVal nClaim_p As Double, ByVal nIssuelim As Double, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal nProduct As Integer) As Secur_sche
		'**+Create a new object
		'+crear un nuevo objeto
		
		Dim objNewMember As eSecurity.Secur_sche
		objNewMember = New eSecurity.Secur_sche
		'**+Establish the properties that transfers to the method.
		'+Establecer las propiedades que se transfieren al método.
		
		With objNewMember
			.sSche_code = sSche_code
			.nCurrency = nCurrency
			.nBranch = nBranch
			.nClaim_dec = nClaim_d
			.nClaim_pay = nClaim_p
			.nIssuelimit = nIssuelim
			.sStatregtLim = sStatregt
			.nUsercode = nUsercode
			.nProduct = nProduct
		End With
		
		mCol.Add(objNewMember, "A" & sSche_code & CStr(nCurrency) & CStr(nBranch) & CStr(nProduct))
		'**+Returns the created object.
		'+Devolver el objeto creado.
		
		AddLimits = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**%AddOff_acc: Adds the records found to the collection
	'% AddOff_acc: Añade a la colección los registros encontrados.
	Public Function AddOff_acc(ByVal sSche_code As String, ByVal nOffice As Integer, ByVal nSel As Integer, ByVal sInd_inqu As String, ByVal sInd_upda As String, ByVal sStatregt As String, ByVal sDescript As String) As Secur_sche
		'**+Create a new object
		'+crear un nuevo objeto
		
		Dim objNewMember As eSecurity.Secur_sche
		objNewMember = New eSecurity.Secur_sche
		'**+Establish the properties that transfers to the method.
		'+Establecer las propiedades que se transfieren al método.
		
		With objNewMember
			.sSche_code = sSche_code
			.nOffice = nOffice
			.nSel = nSel
			.sInd_inqu = sInd_inqu
			.sInd_upda = sInd_upda
			.sStatregtOff = sStatregt
			.sDesOffice = sDescript
		End With
		
		mCol.Add(objNewMember, "A" & sSche_code & CStr(nOffice))
		'**+Returns the created object
		'+Devolver el objeto creado.
		
		AddOff_acc = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**%AddLevels: Adds the records found to the collection
	'%AddLevels: Añade a la colección los registros encontrados.
	Public Function AddLevels(ByVal objClass As Secur_sche) As Secur_sche
		'**+Create a new object
		'+crear un nuevo objeto
		
		If objClass Is Nothing Then
			objClass = New Secur_sche
		End If
		
		'**+Establish the properties that transfers to the method.
		'+Establecer las propiedades que se transfieren al método.
		
		With objClass
			mCol.Add(objClass, "A" & .sSche_code & .sInd_Type & .sCode_mt)
		End With
		'**+Returns the created object
		'+Devolver el objeto creado.
		
		AddLevels = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'**%AddSche_pcon: Adds the records found to the collection
	'%AddSche_pcon: Añade a la colección los registros encontrados.
	Public Function AddSche_pcon(ByVal sSche_code As String, ByVal nConcept As Integer, ByVal nSel As Integer, ByVal sStatregt As String) As Secur_sche
		'**+Create a new object.
		'+crear un nuevo objeto
		
		Dim objNewMember As eSecurity.Secur_sche
		objNewMember = New eSecurity.Secur_sche
		'**+Establish the properties that transfers to the method.
		'+Establecer las propiedades que se transfieren al método.
		
		With objNewMember
			.sSche_code = sSche_code
			.nConcept = nConcept
			.nSel = nSel
			.sStatregtCon = sStatregt
		End With
		
		mCol.Add(objNewMember, "A" & sSche_code & CStr(nConcept))
		'**+Returns the created object
		'+Devolver el objeto creado.
		
		AddSche_pcon = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Secur_sche
		Get
			'**+ Used when making reference to a collection element.
			'**+vntIndexKey contain the  collection index or key
			'**+ for this it is declared as a Variant
			'+ Se usa al hacer referencia a un elemento de la colección
			'+ vntIndexKey contiene el índice o la clave de la colección,
			'+ por lo que se declara como un Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'**+Use to get the elements number of the collection. Syntax: Debyg.Print x. Count.
			'+Se usa al obtener el número de elementos de la colección. Sintaxis: Debug.Print x.Count.
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'**+Use to delete a vntIndexKey collection element contain the index or key,
		'**+for this it is declarated as a Variant Syntaxis: x.Remove(xyz).
		'+Se usa al quitar un elemento de la colección vntIndexKey contiene el índice o la clave,
		'+por lo que se declara como un Variant Sintaxis: x.Remove(xyz).
		
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
	
	'**%FindSchema_cur: Method that reads the table "sChema_cur" - Currencies table permitted in a schema
	'%FindSchema_cur: Función que permite realizar la lectura de la tabla "sChema_cur" - Monedas
	'%permitidas en un esquema.
	Public Function FindSchema_cur(ByVal sSche_code As String, ByVal nAction As Integer, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReasChema_cur As eRemoteDB.Execute
		lrecReasChema_cur = New eRemoteDB.Execute
		
		FindSchema_cur = True
		
		On Error GoTo FindSchema_cur_Err
		
		If sSche_code <> mstrSche_code Or nAction <> mlngAction Or lblnFind Then
			
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+Parameters definition to stored procedure 'insudb.reaSchema_curSG014'
			'**+Data read on 01/15/2001 15.29.55
			'+Definición de parámetros para stored procedure 'insudb.reaSchema_curSG014'
			'+Información leída el 15/01/2001 15.29.55
			
			With lrecReasChema_cur
				.StoredProcedure = "reaSchema_curSG014"
				
				.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrSche_code = sSche_code
					mlngAction = nAction
					
					Do While Not .EOF
						Call AddsChema_cur(sSche_code, .FieldToClass("nCurrency"), .FieldToClass("nSel"), .FieldToClass("sStatregt"))
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindSchema_cur = False
					
					mstrSche_code = CStr(Nothing)
					mlngAction = 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReasChema_cur may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReasChema_cur = Nothing
		End If
		
FindSchema_cur_Err: 
		If Err.Number Then
			FindSchema_cur = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%FindLimits: Method that reads the table "Limits"  - Limits of claims and subscription
	'%FindLimits: Función que permite realizar la lectura de la tabla "Limits" - Límites
	'%de suscripción y siniestros.
	Public Function FindLimits(ByVal sSche_code As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaLimits As eRemoteDB.Execute
		lrecReaLimits = New eRemoteDB.Execute
		
		FindLimits = True
		
		On Error GoTo FindLimits_Err
		
		If sSche_code <> mstrSche_code Or lblnFind Then
			
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+Parameters definition to stored procedure 'insudb.realimitsSG003'
			'**+Data read on 15/01/2001 15.29.55
			'+Definición de parámetros para stored procedure 'insudb.realimitsSG003'
			'+Información leída el 15/01/2001 15.29.55
			
			With lrecReaLimits
				.StoredProcedure = "realimitsSG003"
				
				.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrSche_code = sSche_code
					
					Do While Not .EOF
						'se agrego nproduct
						Call AddLimits(.FieldToClass("sSche_code"), .FieldToClass("nCurrency"), .FieldToClass("nBranch"), .FieldToClass("nClaim_d"), .FieldToClass("nClaim_p"), .FieldToClass("nIssuelim"), .FieldToClass("sStatregt"), .FieldToClass("nUsercode"), .FieldToClass("nProduct"))
						
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindLimits = False
					
					mstrSche_code = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaLimits may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaLimits = Nothing
		End If
		
FindLimits_Err: 
		If Err.Number Then
			FindLimits = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%FindOff_acc: Method that reads the table "Off_acc" - Access to Branch office
	'%FindOff_acc: Función que permite realizar la lectura de la tabla "Off_acc" - Acceso a sucursales.
	Public Function FindOff_acc(ByVal sSche_code As String, ByVal nAction As Integer, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaOff_acc As eRemoteDB.Execute
		lrecReaOff_acc = New eRemoteDB.Execute
		
		FindOff_acc = True
		
		On Error GoTo FindOff_acc_Err
		
		If sSche_code <> mstrSche_code Or nAction <> mlngAction Or lblnFind Then
			
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+Parameters defintion to stored procedure 'insudb.reaOff_accSG017'
			'**+Data read on 01/15/2001 15.29.55
			'+Definición de parámetros para stored procedure 'insudb.reaOff_accSG017'
			'+Información leída el 15/01/2001 15.29.55
			
			With lrecReaOff_acc
				.StoredProcedure = "reaOff_accSG017"
				.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrSche_code = sSche_code
					mlngAction = nAction
					
					Do While Not .EOF
						Call AddOff_acc(sSche_code, .FieldToClass("nOffice"), .FieldToClass("nSel"), .FieldToClass("sInd_inqu"), .FieldToClass("sInd_upda"), .FieldToClass("sStatregt"), .FieldToClass("sDescript"))
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindOff_acc = False
					
					mstrSche_code = CStr(Nothing)
					mlngAction = 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaOff_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaOff_acc = Nothing
		End If
		
FindOff_acc_Err: 
		If Err.Number Then
			FindOff_acc = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%FindLevels: Method that reads the table "Levels" - Levels of security
	'%FindLevels: Función que permite realizar la lectura de la tabla "Levels" - Niveles de seguridad.
	Public Function FindLevels(ByVal sSche_code As String, Optional ByVal lblnFind As Boolean = True, Optional ByVal nRow As Short = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecreaLevels As eRemoteDB.Execute
		Dim lclsSecur_sche As eSecurity.Secur_sche
		
		lrecreaLevels = New eRemoteDB.Execute
		
		FindLevels = True
		
		On Error GoTo FindLevels_Err
		
		If sSche_code <> mstrSche_code Or lblnFind Then
			
			'+Se inicializa la coleccion de datos
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+Parameters definition to stored procedure 'insudb.reaLevels_SG002'
			'**+Data read on 01/15/2001 15.229.55
			'+Definición de parámetros para stored procedure 'insudb.reaLevels_SG002'
			'+Información leída el 15/01/2001 15.29.55
			
			With lrecreaLevels
				.StoredProcedure = "reaLevels_SG002"
				.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mstrSche_code = sSche_code
					
					Do While Not .EOF
						lclsSecur_sche = New Secur_sche
						lclsSecur_sche.sSche_code = .FieldToClass("sSche_code")
						lclsSecur_sche.sInd_Type = .FieldToClass("sInd_type")
						lclsSecur_sche.sCode_mt = .FieldToClass("sCode_mt")
						lclsSecur_sche.nAmelevel = .FieldToClass("nAmelevel")
						lclsSecur_sche.nInqlevel = .FieldToClass("nInqlevel")
						lclsSecur_sche.sSupervis = .FieldToClass("sSupervis")
						lclsSecur_sche.sPermitted = .FieldToClass("sPermitted")
						lclsSecur_sche.nUsercode = .FieldToClass("nUsercode")
						lclsSecur_sche.sDescCode_mt = .FieldToClass("sDescCode_mt")
						Call AddLevels(lclsSecur_sche)
						
						'UPGRADE_NOTE: Object lclsSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsSecur_sche = Nothing
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindLevels = False
					
					mstrSche_code = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecreaLevels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaLevels = Nothing
		End If
		
FindLevels_Err: 
		If Err.Number Then
			FindLevels = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%FindSche_pcon: Method that reads the table "Sche_pcon" - solicitude concept
	'**%of the authorize payment
	'%FindSche_pcon: Función que permite realizar la lectura de la tabla "Sche_pcon" - Conceptos de
	'%solicitud de pagos autorizados.
	Public Function FindSche_pcon(ByVal sSche_code As String, ByVal nAction As Integer, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaSche_pcon As eRemoteDB.Execute
		lrecReaSche_pcon = New eRemoteDB.Execute
		
		FindSche_pcon = True
		
		On Error GoTo FindSche_pcon_Err
		
		If sSche_code <> mstrSche_code Or nAction <> mlngAction Or lblnFind Then
			
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+Parameters definition to stored procedure 'insudb.reaSche_pconSG100'
			'**+Data read on 01/15/2001 15.29.55
			'+Definición de parámetros para stored procedure 'insudb.reaSche_pconSG100'
			'+Información leída el 15/01/2001 15.29.55
			
			With lrecReaSche_pcon
				.StoredProcedure = "reaSche_pconSG100"
				
				.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("Action", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrSche_code = sSche_code
					mlngAction = nAction
					
					Do While Not .EOF
						Call AddSche_pcon(sSche_code, .FieldToClass("nConcept"), .FieldToClass("nSel"), .FieldToClass("sStatregt"))
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindSche_pcon = False
					
					mstrSche_code = CStr(Nothing)
					mlngAction = 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaSche_pcon may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaSche_pcon = Nothing
		End If
		
FindSche_pcon_Err: 
		If Err.Number Then
			FindSche_pcon = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%FindSche_Transac: Función que permite realizar la lectura de la tabla "Sche_Transac" - Niveles de seguridad por transacción/operación
	Public Function FindSche_Transac(ByVal sSche_code As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaSche_Transac As eRemoteDB.Execute
		lrecReaSche_Transac = New eRemoteDB.Execute
		
		FindSche_Transac = True
		
		On Error GoTo FindSche_Transac_Err
		
		If sSche_code <> mstrSche_code Or lblnFind Then
			
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			With lrecReaSche_Transac
				.StoredProcedure = "SG020PKG.reaSche_TransacSG020"
				.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mstrSche_code = sSche_code
					
					Do While Not .EOF
						Call AddSche_Transac(sSche_code, .FieldToClass("sCodispl"), .FieldToClass("nTransac"), .FieldToClass("sDesc_tx"))
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindSche_Transac = False
					mstrSche_code = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaSche_Transac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaSche_Transac = Nothing
		End If
		
FindSche_Transac_Err: 
		If Err.Number Then
			FindSche_Transac = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%AddSche_Transac: Adds the records found to the collection
	'%AddSche_Transac: Añade a la colección los registros encontr
	Public Function AddSche_Transac(ByVal sSche_code As String, ByVal sCodispl As String, ByVal nTransac As Integer, ByVal sDesc_tx As String) As Secur_sche
		'**+Creates a new object
		'+crear un nuevo objeto
		
		Dim objNewMember As Secur_sche
		objNewMember = New Secur_sche
		
		'**+Establish the properties that transfer to the method
		'+Establecer las propiedades que se transfieren al método.
		
		With objNewMember
			.sSche_code = sSche_code
			.sCodispl = sCodispl
			.nTransac = nTransac
			.sDesc_tx = sDesc_tx
		End With
		
		mCol.Add(objNewMember)
		'**+Returns the created object
		'+Devolver el objeto creado.
		
		AddSche_Transac = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
End Class






