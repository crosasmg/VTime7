Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Agencie_NET.Agencie")> Public Class Agencie
	'%-------------------------------------------------------%'
	'% $Workfile:: Agencies.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Defines the principal properties of the corresponding class to the Agencies  table (05/12/2001)
	'-Se definen las propiedades principales de la clase+ correspondientes a la tabla Agencies (05/12/2001)
	'Column_name                        Type              Computed      Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nAgency As Integer 'Integer          no            2           5     0     no                                  (n/a)                               (n/a)
	Public nOfficeAgen As Integer 'Integer          no            2           5     0     no                                  (n/a)                               (n/a)
	Public nBran_Off As Integer 'Integer          no            2           5     0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'Integer          no            8                       no                                  (n/a)                               (n/a)
	Public sPay As String 'Integer          no            8                       no                                  (n/a)                               (n/a)
	Public sOfficeAgenDesc As String
	Public sAgencyDesc As String
	
	Private mvarAgencies As Agencies
	
	
	Public Property Agencies() As Agencies
		Get
			
			If mvarAgencies Is Nothing Then
				mvarAgencies = New Agencies
			End If
			
			Agencies = mvarAgencies
		End Get
		Set(ByVal Value As Agencies)
			
			mvarAgencies = Value
		End Set
	End Property
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mvarAgencies = New Agencies
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mvarAgencies may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarAgencies = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Update: It allows to update registries in the Agencies table.
	'% Update: Permite actualizar registros en la tabla Agencies.
	Public Function insUpdAgencies(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdAgencies As eRemoteDB.Execute
		
		On Error GoTo insUpdAgencies_Err
		
		lrecinsUpdAgencies = New eRemoteDB.Execute
		
		With lrecinsUpdAgencies
			.StoredProcedure = "insUpdAgencies"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBran_Off", nBran_Off, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay", sPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insUpdAgencies = True
			Else
				insUpdAgencies = False
			End If
		End With
		
insUpdAgencies_Err: 
		If Err.Number Then
			insUpdAgencies = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdAgencies may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdAgencies = Nothing
	End Function
	
	'% insUpdUser_Office: It allows to update registries in the User_Office table.
	'% insUpdUser_Office: Permite actualizar registros en la tabla User_Office.
	Public Function insUpdUser_Office(ByVal nUser As Integer, ByVal sSel As String, ByVal sOfficeAgen As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsUpdAgencies As eRemoteDB.Execute
		
		On Error GoTo insUpdUser_Office_Err
		
		lrecinsUpdAgencies = New eRemoteDB.Execute
		
		With lrecinsUpdAgencies
			.StoredProcedure = "insUser_Office"
			.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOffice", sOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insUpdUser_Office = True
			Else
				insUpdUser_Office = False
			End If
		End With
		
insUpdUser_Office_Err: 
		If Err.Number Then
			insUpdUser_Office = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdAgencies may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdAgencies = Nothing
	End Function
	
	
	'%insPostMS5552: Función que realiza el llamado a los métodos de actualización, borrado e inserción de registros
	Public Function insPostMS5577(ByVal nAgency As Integer, ByVal nOfficeAgen As Integer, ByVal nUsercode As Integer, ByVal nBran_Off As Integer, ByVal sPay As String, ByVal sAction As String) As Boolean
		On Error GoTo insPostMS5577_err
		
		sAction = Trim(sAction)
		
		With Me
			.nAgency = nAgency
			.nOfficeAgen = nOfficeAgen
			.nBran_Off = nBran_Off
			.nUsercode = nUsercode
			.sPay = sPay
		End With
		
		Select Case sAction
			
			'**+If the selected option is Register
			'+Si la opción seleccionada es Registrar
			Case "Add"
				insPostMS5577 = insUpdAgencies(1)
				
				'**+If the selected option is Modify
				'+Si la opción seleccionada es Modificar
			Case "Update"
				insPostMS5577 = insUpdAgencies(2)
				
				'**+If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
			Case "Del"
				insPostMS5577 = insUpdAgencies(3)
				
		End Select
		
insPostMS5577_err: 
		If Err.Number Then
			insPostMS5577 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% insValMS5552_k: Realiza la validación de los campos
	Private Function valDuplicatedPaymentAgencies(ByVal nAgency As Integer, ByVal nBran_Off As Integer) As Boolean
		Dim lrecRS As eRemoteDB.Execute
		
		On Error GoTo Err_h
		
		lrecRS = New eRemoteDB.Execute
		
		With lrecRS
			.StoredProcedure = "valDuplicatedPaymentAgencies"
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBran_Off", nBran_Off, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				valDuplicatedPaymentAgencies = .FieldToClass("nValid") = 1
				.RCloseRec()
			End If
		End With
		
Err_h: 
		If Err.Number Then
			valDuplicatedPaymentAgencies = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRS may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRS = Nothing
	End Function
	
	
	'% insValMS5552_k: Realiza la validación de los campos
	Public Function insValMS5577_k(ByVal nAgency As Integer, ByVal nOfficeAgen As Integer, ByVal nBran_Off As Integer, ByVal sPay As String, ByVal sAction As String, ByVal sCodispl As String) As String
		
		Dim lobjErrors As eFunctions.Errors
		
		insValMS5577_k = String.Empty
		
		On Error GoTo insValMS5577_k_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nAgency = eRemoteDB.Constants.intNull Or nAgency = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 55518)
		End If
		
		If nOfficeAgen = eRemoteDB.Constants.intNull Or nOfficeAgen = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 55519)
		Else
			If nBran_Off <> eRemoteDB.Constants.intNull And nBran_Off <> 0 Then
				If Not insValOffice(nBran_Off, nOfficeAgen) Then
					Call lobjErrors.ErrorMessage(sCodispl, 60427)
				End If
			End If
		End If
		
		If nBran_Off = eRemoteDB.Constants.intNull Or nBran_Off = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 55520)
		End If
		
		If sAction = "Add" And nAgency <> eRemoteDB.Constants.intNull Then
			If insValAgencyExist(nAgency) = True Then
				Call lobjErrors.ErrorMessage(sCodispl, 55521)
			End If
		End If
		
		If sPay = "1" Then
			If Not valDuplicatedPaymentAgencies(nAgency, nBran_Off) Then
				Call lobjErrors.ErrorMessage(sCodispl, 80002)
			End If
		End If
		
		insValMS5577_k = lobjErrors.Confirm
		
insValMS5577_k_Err: 
		If Err.Number Then
			insValMS5577_k = "insValMS5577_k: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insValMOP633_k: Realiza la validación de los campos
	Public Function insValMOP633_k(ByVal nUser As Integer, ByVal nBran_Off As Integer) As String
		
		Dim lobjErrors As eFunctions.Errors
		
		insValMOP633_k = String.Empty
		
		On Error GoTo insValMOP633_k_Err
		
		lobjErrors = New eFunctions.Errors
		
		If nUser <= 0 Then
			Call lobjErrors.ErrorMessage("MOP633", 60008)
		End If
		
		If nBran_Off <= 0 Then
			Call lobjErrors.ErrorMessage("MOP633", 55519)
		End If
		
		insValMOP633_k = lobjErrors.Confirm
		
insValMOP633_k_Err: 
		If Err.Number Then
			insValMOP633_k = "insValMOP633_k: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Valida la existencia de una agencia
	Public Function insValAgencyExist(ByVal nAgency As Integer) As Boolean
		
		Dim lrecVal_Agencie_Exist As eRemoteDB.Execute
		
		insValAgencyExist = False
		
		On Error GoTo insValAgencyExist_Err
		
		lrecVal_Agencie_Exist = New eRemoteDB.Execute
		
		With lrecVal_Agencie_Exist
			.StoredProcedure = "Val_Agencie_Exist"
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("lCount") > 0 Then
					insValAgencyExist = True
				End If
			End If
		End With
		
insValAgencyExist_Err: 
		If Err.Number Then
			insValAgencyExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecVal_Agencie_Exist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecVal_Agencie_Exist = Nothing
	End Function
	
	'%Find: Esta función valida que la oficina ya no esté incluida con otra agencia.
	Public Function insValOffice(ByVal nBran_Off As Integer, ByVal nOfficeAgen As Integer) As Boolean
		Dim lrecRea_Agencies_By_Office As eRemoteDB.Execute
		
		On Error GoTo insValOffice_Err
		
		insValOffice = True
		
		lrecRea_Agencies_By_Office = New eRemoteDB.Execute
		
		With lrecRea_Agencies_By_Office
			.StoredProcedure = "Rea_Agencies_By_Office"
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nBran_Off") = nBran_Off Then
					insValOffice = True
				Else
					insValOffice = False
				End If
				.RCloseRec()
			End If
		End With
		
insValOffice_Err: 
		If Err.Number Then
			insValOffice = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRea_Agencies_By_Office may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRea_Agencies_By_Office = Nothing
	End Function
	
	
	
	'%Valida la existencia de una agencia
	Public Function FindPaymentAgency(ByVal nOffice As Integer) As Boolean
		
		Dim lrecRS As eRemoteDB.Execute
		
		FindPaymentAgency = False
		
		On Error GoTo Err_h
		
		lrecRS = New eRemoteDB.Execute
		
		With lrecRS
			.StoredProcedure = "reaPaymentAgency"
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindPaymentAgency = True
				Me.nBran_Off = .FieldToClass("nBran_Off")
				Me.nOfficeAgen = .FieldToClass("nOfficeAgen")
				Me.nAgency = .FieldToClass("nAgency")
				Me.sOfficeAgenDesc = .FieldToClass("sOfficeAgenDesc")
				Me.sAgencyDesc = .FieldToClass("sAgencyDesc")
				.RCloseRec()
			End If
		End With
		
Err_h: 
		If Err.Number Then
			FindPaymentAgency = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRS may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRS = Nothing
	End Function
End Class






