Option Strict Off
Option Explicit On
Public Class Messages
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	'**% Add: Adds the records of the Error Message
	'% Add: Añade los Registros de Mensaje de Error
	Public Function Add(ByVal ntipo As Integer, ByVal nErrorNum As Integer, ByVal sMessaged As String) As Message
		
		'create a new object
		Dim objNewMember As Message
		objNewMember = New Message
		
		With objNewMember
			.ntipo = ntipo
			.nErrorNum = nErrorNum
			.sMessaged = sMessaged
			
		End With
		
		mCol.Add(objNewMember, "Message" & nErrorNum)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Message
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
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
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
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
	
	'**% Find: Reads the Recordset with all the records of the History of an error
	'%Find:Levanta el Recordset con todos los registros del Historico de un error
	Public Function Find(ByVal nErrorNum As Integer, ByVal sMessaged As String) As Boolean
		
		Dim ltempReaMessage As New eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		ltempReaMessage = New eRemoteDB.Execute
		
		'** Parameter definition for the stored procedure 'insudb.reaErr_Histor'
		'Definición de parámetros para stored procedure 'insudb.reaErr_Histor'
		'** Information read on June 06,2001  11:29:15
		'Información leída el 06/06/2001 11:29:15
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		mCol = New Collection
		
		With ltempReaMessage
			.StoredProcedure = "reaMessage1"
			
			'**+ If a search condition exists by error code, send the same to the
			'**+ Stored Procedure.
			'+ Si existe una condicion de busqueda por codigo de error envia
			'+ la misma al Stored Procedure
			
			If nErrorNum = 0 Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nErrornum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			'**+ If a search condition exists bye error message send
			'**+ the same to the Stored Procedure
			'+ Si existe una condicion de busqueda por mensaje de error envia
			'+ la misma al Stored Procedure
			
			If sMessaged = String.Empty Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sMessaged", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sMessaged", sMessaged, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If .Run Then
				Find = True
				While Not .EOF
					Call Add(0, .FieldToClass("nErrornum"), .FieldToClass("sMessaged"))
					.RNext()
				End While
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object ltempReaMessage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempReaMessage = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Find_WinMessage: verify if there are associated windows to the error number sent as a parameter.
	'%Find_WinMessage: función que se encarga de verificar si existen ventanas asociadas al
	'%número de error enviado cómo parámetro
	Public Function Find_WinMessage(ByVal nErrorNum As Integer) As Boolean
		
		'**- Variable definition for the execution and the handle of the SP
		'-Se define la variable para la ejecución y manejo del SP
		
		Dim ltempValWinMessage As eRemoteDB.Execute
		
		On Error GoTo Find_winMessage_err
		
		ltempValWinMessage = New eRemoteDB.Execute
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		mCol = New Collection
		'**- Variable definition for the field treatment
		'-Se define la variable para el tratamiento de los campos
		
		With ltempValWinMessage
			
			.StoredProcedure = "reaWinMessage_nerrornum"
			
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_WinMessage = False
				While Not .EOF
					Call Add_sCodispl(.FieldToClass("sCodispl"))
					.RNext()
				End While
				.RCloseRec()
			Else
				Find_WinMessage = False
			End If
		End With
		
		'UPGRADE_NOTE: Object ltempValWinMessage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempValWinMessage = Nothing
		
Find_winMessage_err: 
		If Err.Number Then
			Find_WinMessage = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Add: Adds the records of associated window to an Error number.
	'% Add: Añade los Registros de ventanas asociadas a un numero de Error
	Public Function Add_sCodispl(ByVal sCodispl As String) As Message
		
		'create a new object
		Dim objNewMember As Message
		objNewMember = New Message
		
		With objNewMember
			.sCodispl = sCodispl
		End With
		
		mCol.Add(objNewMember, "Message" & sCodispl)
		
		Add_sCodispl = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
End Class






