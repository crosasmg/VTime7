Option Strict Off
Option Explicit On
Public Class Tab_winFin
	'**Column_name                                                                                                                      Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nTratypec As Integer '                                                                                                                   smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nSequence As Integer
	Public sCodispl As String
	Public dCompdate As Date
	Public sDefaulti As String
	Public sRequire As String
	Public nUsercode As Integer
	
	Private mvarTab_winFins As Tab_winFins
	
	'**- The auxiliary properties are defined.
	'- Se definen las propiedades auxiliares.
	
	Public sDescript As String
	Public sShort_des As String
	Public sPseudo As String
	
	Private mstrSelected As String
	Private mstrExist As String
	
	Public sExist As String
	Public nIndex As Integer
	
	
	
	'**% This property returns the content of the object mvarTab_winFins
	'*Esta propiedad devuelve el contenido del objeto mvarTab_winFins
	
	'**% This property updates the content of the object mvarTab_winFins
	'* Esta propiedad actualiza el contenido del objeto mvarTab_winFins
	
	Public Property Tab_winFins() As Tab_winFins
		Get
			If mvarTab_winFins Is Nothing Then
				mvarTab_winFins = New Tab_winFins
			End If
			
			
			Tab_winFins = mvarTab_winFins
		End Get
		Set(ByVal Value As Tab_winFins)
			mvarTab_winFins = Value
		End Set
	End Property
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarTab_winFins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarTab_winFins = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	'**%insValMFI001_K: It makes the validation of the fields of page MBC010 - Sequence of windows for financing.
	'% insValMFI001_K: Realiza la validación de los campos de la página MFI001 - Secuencia de
	'% ventanas para financiamiento.
	Public Function insValMFI001_K(ByVal sCodispl As String, ByVal sTratypec As String) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		insValMFI001_K = String.Empty
		
		On Error GoTo insValMFI001K_Err
		
		'**+ The fields "Transaction" is valid.
		'+ Se valida el campo "Transacción".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sTratypec) Or IsNothing(sTratypec) Or Trim(sTratypec) = String.Empty Or Trim(sTratypec) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 7133)
		End If
		
		insValMFI001_K = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValMFI001K_Err: 
		If Err.Number Then
			insValMFI001_K = insValMFI001_K & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**% insValMFI001: This function is in charge to validate the data introduced in the zone
	'**% of detail of the form.
	'%insValMFI001: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma.
	Public Function insValMFI001(ByVal sCodispl As String, ByVal nSel As Integer) As String
		'**- The variable is defined lclsErrors for the shipment of errors of the window.
		'- Se define la variable lclsErrors para el envío de errores de la ventana.
		
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValMFI001_Err
		
		'**+ Beginning occurs to the cycle of validations.
		'+ Se da inicio al ciclo de validaciones.
		
		If nSel = eRemoteDB.Constants.intNull Or nSel = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 99007)
		End If
		
		insValMFI001 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMFI001_Err: 
		If Err.Number Then
			insValMFI001 = lclsErrors.Confirm & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**% Delete: It eliminates the data corresponding to a transaction.
	'% Delete: Elimina los datos correspondientes a una transacción.
	Public Function Delete() As Boolean
		Dim lrecDelTab_winfin As eRemoteDB.Execute
		lrecDelTab_winfin = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure ' insudbdelTab_winfin'
		'**+ read Information the 10/01/2000 11:48:11 A.M..
		'+ Definición de parámetros para stored procedure 'insudb.delTab_winfin'
		'+ Información leída el 10/01/2000 11:48:11 AM
		
		With lrecDelTab_winfin
			.StoredProcedure = "delTab_winfin"
			
			.Parameters.Add("nTratypec", nTratypec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDelTab_winfin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelTab_winfin = Nothing
	End Function
	
	'**% insPostMFI001: Validate the introduced data in the content zone for an especific "frame".
	'% insPostMFI001: Valida los datos introducidos en la zona de contenido para "frame" especifico.
	Public Function insPostMFI001(ByVal nTratypec As Integer, ByVal nSequence As Integer, ByVal sCodispl As String, ByVal nUsercode As Integer, ByVal sDefaulti As String, ByVal sRequire As String, ByVal sExist As String, ByVal sSelected As String, ByVal nAction As Integer) As Boolean
        Dim sSel As Object = New Object

        insPostMFI001 = True
		
		On Error GoTo insPostMFI001_Err
		
		mstrSelected = sSelected
		
		With Me
			.nTratypec = nTratypec
			.nSequence = nSequence
			.sCodispl = sCodispl
			.nUsercode = nUsercode
			.sDefaulti = sDefaulti
			.sRequire = sRequire
			.dCompdate = Today
			mstrExist = sExist
			
			
			If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				
				'**+ Makes the call to the routine ins UpdTab_winpol that has as a function
				'**+ make the correpondent updates in the table Tab_winpol
				'+ Se hace el llamado a la rutina ins UpdTab_winpol la cual tiene como función
				'+ hacer las actualizaciones correspondientes en la tabla Tab_winpol
				
				If sSel = "2" Then
					insPostMFI001 = .Delete
				Else
					insPostMFI001 = .UpDate
				End If
				
			End If
			
		End With
		
insPostMFI001_Err: 
		If Err.Number Then
			insPostMFI001 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% Update: update one record in the Tab_winfin table
	'**+ (Maintenance of the sequence of windows for financing - MFI001)
	'% Update: Actualiza un registro de la tabla Tab_winfin
	'+ (Mantenimiento de la secuencia de ventanas para financiamiento - MFI001.
	Public Function UpDate() As Boolean
		Dim lrecInsTab_winfin As eRemoteDB.Execute
		lrecInsTab_winfin = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.insUpdTab_winfin'
		'**+ Data read on 09/06/2001 01:54:40
		'+ Definición de parámetros para stored procedure 'insudb.insTab_winfin'
		'+ Información leída el 06/09/2001 01:54:40 p.m.
		
		With lrecInsTab_winfin
			.StoredProcedure = "insTab_winfin"
			
			.Parameters.Add("nTratypec", nTratypec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpDate = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecInsTab_winfin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsTab_winfin = Nothing
		
Update_Err: 
		If Err.Number Then
			UpDate = False
		End If
		
		On Error GoTo 0
	End Function
End Class






