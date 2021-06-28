Option Strict Off
Option Explicit On
Public Class Auto_dbs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Auto_dbs.cls                             $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Assist properties
	'-Propiedades Auxiliares
	
	Private lstrCertype As String
	Private lintBranch As Integer
	Private lintProduct As Integer
	Private llngPolicy As Double
	Private lintCompany As Integer
	Private ldtmEffecdate As Date
	
	'local variable to hold collection
	Private mCol As Collection
	
	'**% Add: Add a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal sLicense_ty As String, ByVal sRegist As String, ByVal sChassis As String, ByVal sMotor As String, ByVal sVeh_own As String, ByVal sClient As String, ByVal sColor As String, ByVal sVehcode As String, ByVal nVestatus As Integer, ByVal nNotenum As Double, ByVal nUsercode As Integer, ByVal nYear As Integer, ByVal nVehType As Integer, ByVal nAnualKm As Double, ByVal nActualKm As Double, ByVal nKeepVeh As Integer, ByVal nRoadType As Integer, ByVal nIndLaw As Integer, ByVal nFuelType As Integer, ByVal nIndAlarm As Integer, ByVal sDigit As String, ByVal nLic_special As Integer, ByVal nControl As Integer) As Auto_db
		Dim objNewMember As ePolicy.Auto_db
		
		objNewMember = New ePolicy.Auto_db
		
		With objNewMember
			.sLicense_ty = sLicense_ty
			.sRegist = sRegist
			.sChassis = sChassis
			.sMotor = sMotor
			.sVeh_own = sVeh_own
			.sClient = sClient
			.sColor = sColor
			.sVehcode = sVehcode
			.nVestatus = nVestatus
			.nNotenum = nNotenum
			.nUsercode = nUsercode
			.nYear = nYear
			.nVehType = nVehType
			.nAnualKm = nAnualKm
			.nActualKm = nActualKm
			.nKeepVeh = nKeepVeh
			.nRoadType = nRoadType
			.nIndLaw = nIndLaw
			.nFuelType = nFuelType
			.nIndAlarm = nIndAlarm
			.sDigit = sDigit
			.nLic_special = nLic_special
		End With
		
		mCol.Add(objNewMember, "A" & sLicense_ty & sRegist & sMotor & sChassis)
		
		'Return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Find: Function that returns TRUE to make the reading of the records in the 'Auto_db' table
	'% Find: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Auto_db'
    Public Function Find(ByVal sRegist As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaAuto_db As eRemoteDB.Execute
        Dim lobjAuto_db As ePolicy.Auto_db

        On Error GoTo Find_Err

        lrecreaAuto_db = New eRemoteDB.Execute

        If lblnFind Then
            With lrecreaAuto_db
                .StoredProcedure = "reaAuto_db"
                .Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find = .Run
                If Find Then
                    Do While Not .EOF
                        lobjAuto_db = Add(.FieldToClass("sLicense_ty"), .FieldToClass("sRegist"), .FieldToClass("sChassis"), .FieldToClass("sMotor"), .FieldToClass("sVeh_own"), .FieldToClass("sClient"), .FieldToClass("sColor"), .FieldToClass("sVehcode"), .FieldToClass("nVestatus"), .FieldToClass("nNotenum"), .FieldToClass("nUsercode"), .FieldToClass("nYear"), .FieldToClass("nVehType"), .FieldToClass("nAnualKm"), .FieldToClass("nActualKm"), .FieldToClass("nKeepVeh"), .FieldToClass("nRoadType"), .FieldToClass("nIndLaw"), .FieldToClass("nFuelType"), .FieldToClass("nIndAlarm"), .FieldToClass("sDigit"), .FieldToClass("nLic_Special"), 0)
                        .RNext()
                    Loop
                    .RCloseRec()
                End If
            End With
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAuto_db = Nothing
        'UPGRADE_NOTE: Object lobjAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjAuto_db = Nothing
    End Function
	
	'**%FindBVC001: Is in charge to make the reading correspond to the Auto_db table,
	'**%to get the valid records for the key passed as a parameter
	'%FindBVC001: Se encarga de realizar la lectura  correspondiente  a  la tabla de Auto_db,
	'%para obtener el registro valido para la llave pasada como parametro
	Public Function FindBVC001(ByVal sRegist As String) As Boolean
		'**-"lrecreaAuto_db" variable definition
		'-Se define la variable lrecreaAuto_db
		
		Dim lrecreaAuto_db As eRemoteDB.Execute
		
		lrecreaAuto_db = New eRemoteDB.Execute
		
		'**+Parameter definition to stored procedure 'insudb.reaAuto_db'
		'**+Data read on 03/30/2001 15:19:23
		'+Definición de parámetros para stored procedure 'insudb.reaAuto_db'
		'+Información leída el 30/03/2001 15:19:23
		
		With lrecreaAuto_db
			.StoredProcedure = "reaAuto_db"
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("sLicense_ty"), .FieldToClass("sRegist"), .FieldToClass("sChassis"), .FieldToClass("sMotor"), .FieldToClass("sVeh_own"), .FieldToClass("sClient"), .FieldToClass("sColor"), .FieldToClass("sVehcode"), .FieldToClass("nVestatus"), .FieldToClass("nNotenum"), .FieldToClass("nUsercode"), .FieldToClass("nYear"), .FieldToClass("nVehType"), .FieldToClass("nAnualKm"), .FieldToClass("nActualKm"), .FieldToClass("nKeepVeh"), .FieldToClass("nRoadType"), .FieldToClass("nIndLaw"), .FieldToClass("nFuelType"), .FieldToClass("nIndAlarm"), .FieldToClass("sDigit"), .FieldToClass("nLic_Special"), 0)
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAuto_db = Nothing
		
	End Function
	
	'**%FindCondition: Execute the SELECT and fill the collection
	'%FindCondition: Ejecuta el SELECT y llena la coleccion
	Public Function FindCondition(ByVal sSQLCondition As String) As Boolean
		Dim lrecSQL As eRemoteDB.Execute
		
		On Error GoTo FindCondition_Err
		
		lrecSQL = New eRemoteDB.Execute
		
		FindCondition = True
		
		With lrecSQL
			.SQL = sSQLCondition
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("sLicense_ty"), .FieldToClass("sRegist"), .FieldToClass("sChassis"), .FieldToClass("sMotor"), .FieldToClass("sVeh_own"), .FieldToClass("sClient"), .FieldToClass("sColor"), .FieldToClass("sVehcode"), .FieldToClass("nVestatus"), .FieldToClass("nNotenum"), .FieldToClass("nUsercode"), .FieldToClass("nYear"), .FieldToClass("nVehType"), .FieldToClass("nAnualKm"), .FieldToClass("nActualKm"), .FieldToClass("nKeepVeh"), .FieldToClass("nRoadType"), .FieldToClass("nIndLaw"), .FieldToClass("nFuelType"), .FieldToClass("nIndAlarm"), .FieldToClass("sDigit"), .FieldToClass("nLic_Special"), 0)
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
FindCondition_Err: 
		If Err.Number Then
			FindCondition = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecSQL may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSQL = Nothing
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Coinsuran
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
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
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
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
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






