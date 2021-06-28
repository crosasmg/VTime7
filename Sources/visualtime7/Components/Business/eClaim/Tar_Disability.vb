Option Strict Off
Option Explicit On
Public Class Tar_Disability
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_Disability.cls                         $%'
	'% $Author:: Jperez                                     $%'
	'% $Date:: 3-01-12 15:14                                $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on December 27,2000.
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	
	'Column_Name                                Type      Length  Prec  Scale  Nullable
	'------------------------- --------------- --------   ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0    No
	Public nCovergen As Integer ' NUMBER        22     5      0    No
	Public dEffecdate As Date ' DATE           7                 No
	Public nDisability As Integer ' NUMBER        22     5      0    Yes
	Public nRate As Double ' NUMBER        22     9      6    Yes
	Public dNulldate As Date ' DATE           7                 Yes
	Public nUsercode As Integer ' NUMBER        22     5      0    No
	Public sShort_Des As String
	
	Private mvarTar_Disabilitys As Tar_Disabilitys
	
	
	Public Property Tar_Disabilitys() As Tar_Disabilitys
		Get
			If mvarTar_Disabilitys Is Nothing Then
				mvarTar_Disabilitys = New Tar_Disabilitys
			End If
			
			Tar_Disabilitys = mvarTar_Disabilitys
		End Get
		Set(ByVal Value As Tar_Disabilitys)
			mvarTar_Disabilitys = Value
		End Set
	End Property
	
	Private Sub Class_Terminate_Renamed()
		mvarTar_Disabilitys = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	'%InsUpdTar_Disability: Se encarga de actualizar la tabla
	Private Function InsUpdTar_Disability(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTar_Disability As eRemoteDB.Execute
		
		On Error GoTo InsUpdTar_Disability_Err
		
		lrecInsUpdTar_Disability = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdTar_Disability'
		'+Información leída el 25/10/01
		With lrecInsUpdTar_Disability
			.StoredProcedure = "InsUpdTar_Disability"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisability", nDisability, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTar_Disability = .Run(False)
		End With
		
InsUpdTar_Disability_Err: 
		If Err.Number Then
			InsUpdTar_Disability = False
		End If
		lrecInsUpdTar_Disability = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTar_Disability(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTar_Disability(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTar_Disability(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nCovergen As Integer, ByVal nDisability As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaTar_Disability As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nCovergen <> nCovergen Or Me.nDisability <> nDisability Or Me.dEffecdate <> dEffecdate Or bFind Then
			
			lrecReaTar_Disability = New eRemoteDB.Execute
			
			Me.nBranch = nBranch
			Me.nCovergen = nCovergen
			Me.nDisability = nDisability
			Me.dEffecdate = dEffecdate
			
			'+Definición de parámetros para stored procedure 'ReaTar_Disability'
			'+Información leída el 25/10/01
			With lrecReaTar_Disability
				.StoredProcedure = "ReaTar_Disability"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDisability", nDisability, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = .FieldToClass("nBranch")
					Me.nCovergen = .FieldToClass("nCovergen")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nDisability = .FieldToClass("nDisability")
					Me.nRate = .FieldToClass("nRate")
					Me.dNulldate = .FieldToClass("dNulldate")
					Find = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecReaTar_Disability = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción, según error 10869
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaTar_Disability As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecReaTar_Disability = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+Definición de parámetros para stored procedure 'ReaTar_Disability'
		With lrecReaTar_Disability
			.StoredProcedure = "InsValEffecdate_Tar_Disability"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValEffecdate = Not .Run
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		lrecReaTar_Disability = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMSI8000_K: Validaciones de la transacción(Header)
	'%                Tabla de control de prima mínima(MSI8000)
	Public Function InsValMSI8000_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMSI8000_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Se valida el Campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 9064)
			End If
			
			'+ Se valida el Campo Fecha
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 1103)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If Not InsValEffecdate(nBranch, dEffecdate) Then
						.ErrorMessage(sCodispl, 55611)
					End If
				End If
			End If
			
			InsValMSI8000_K = .Confirm
		End With
		
InsValMSI8000_K_Err: 
		If Err.Number Then
			InsValMSI8000_K = "InsValMSI8000_K: " & Err.Description
		End If
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMSI8000: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MSI8000)
	Public Function InsValMSI8000(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nDisability As Integer, ByVal nRate As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMSI8000_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Se valida el campo Mes de vigencia
			If nCovergen = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60315)
			Else
				
				'+Validar que no se dupliquen registros
				If sAction = "Add" Then
					If Find(nBranch, nCovergen, nDisability, dEffecdate) Then
						.ErrorMessage(sCodispl, 10284)
					End If
				End If
				
			End If
			
			'+Se valida el campo Factor
			If nRate = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55540)
			ElseIf nRate > 100 Then 
				.ErrorMessage(sCodispl, 1938)
			End If
			
			InsValMSI8000 = .Confirm
		End With
		
InsValMSI8000_Err: 
		If Err.Number Then
			InsValMSI8000 = "InsValMSI8000: " & Err.Description
		End If
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMSI8000: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MSI8000)
	Public Function InsPostMSI8000(ByVal sAction As String, ByVal nBranch As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nDisability As Integer, ByVal nRate As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMSI8000_Err
		
		With Me
			.nBranch = nBranch
			.nCovergen = nCovergen
			.dEffecdate = dEffecdate
			.nDisability = nDisability
			.nRate = nRate
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMSI8000 = Add
			Case "Update"
				InsPostMSI8000 = Update
			Case "Del"
				InsPostMSI8000 = Delete
		End Select
		
InsPostMSI8000_Err: 
		If Err.Number Then
			InsPostMSI8000 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nCovergen = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nDisability = eRemoteDB.Constants.intNull
		nRate = eRemoteDB.Constants.intNull
		dNulldate = System.Date.FromOADate(eRemoteDB.Constants.intNull)
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






