Option Strict Off
Option Explicit On
Public Class Tab_prov_zone
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_prov_zone.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla TAB_PROV_ZONE tomada el 02/04/2002 19:16
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	' ------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nProvider As Integer ' NUMBER        22     5      0 No
	Public nZone As Integer ' NUMBER        22     5      0 No
	Public nOrder As Integer ' NUMBER        22     2      0 No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	Public lintExist As Integer
	Public nExist As Integer
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTab_prov_zone(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdTab_prov_zone(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTab_prov_zone(3)
	End Function
	
	'%InsValTab_prov_zone: Lee los datos de la tabla
	Public Function InsValTab_prov_zone(ByVal nProvider As Integer, ByVal nZone As Integer, ByVal nOrder As Integer, ByVal nExist As Integer) As Boolean
		Dim lrecreaTab_prov_zone_v As eRemoteDB.Execute
		
		On Error GoTo reaTab_prov_zone_v_Err
		
		lrecreaTab_prov_zone_v = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaTab_prov_zone_val 03-01-2002 17:14:26
		'+
		With lrecreaTab_prov_zone_v
			.StoredProcedure = "reaTab_prov_zone_v"
			With .Parameters
				.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nZone", nZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			If .Run(False) Then
				InsValTab_prov_zone = True
				lintExist = .Parameters("nExist").Value
			End If
		End With
		
reaTab_prov_zone_v_Err: 
		If Err.Number Then
			InsValTab_prov_zone = False
		End If
		lrecreaTab_prov_zone_v = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMSI647: Esta función se encarga de validar los datos del Form
	'%Zonas de un proveedor
	Public Function insValMSI647(ByVal sCodispl As String, ByVal sAction As String, ByVal nProvider As Integer, ByVal nZone As Integer, ByVal nOrder As Integer) As String
		
		'- Se define el objeto para el manejo de las clases
		
		Dim lclsErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lblnError As Boolean
		Dim mobjproduct As eProduct.Product
		
		Dim lintBranch As Integer
		Dim lintProduct As Integer
		
		On Error GoTo insValMSI647_Err
		
		lclsErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		
		lblnError = False
		
		'+ Validación de nZone
		With lclsErrors
			If nZone <= 0 Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 3249)
			End If
			
			'+ Validación de Orden
			If Not lblnError Then
				If nZone > 0 Then
					If nOrder <= 0 Then
						Call .ErrorMessage(sCodispl, 11146)
						lblnError = True
					End If
				End If
			End If
			
			If sAction = "Add" Then
				If Not lblnError Then
					nExist = 0
					Call InsValTab_prov_zone(nProvider, nZone, nOrder, lintExist)
					If lintExist = 1 Then
						Call .ErrorMessage(sCodispl, 700020)
					End If
				End If
			End If
			
			If Not lblnError Then
				lintExist = 9
				Call InsValTab_prov_zone(nProvider, nZone, nOrder, lintExist)
				If lintExist = 2 Then
					Call .ErrorMessage(sCodispl, 4115)
				End If
			End If
			insValMSI647 = .Confirm
		End With
		
insValMSI647_Err: 
		If Err.Number Then
			insValMSI647 = "insValMSI647: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lobjValues = Nothing
	End Function
	'%InsPostMSI647Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (MSI647)
	Public Function InsPostMSI647Upd(ByVal sAction As String, ByVal nProvider As Integer, ByVal nZone As Integer, ByVal nOrder As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		
		Dim lobjValues As eFunctions.Values
		lobjValues = New eFunctions.Values
		
		On Error GoTo InsPostMSI647Upd_Err
		
		With Me
			.nProvider = nProvider
			.nZone = nZone
			.nOrder = nOrder
			
			If .nOrder = eRemoteDB.Constants.intNull Then
				.nOrder = 0
			End If
			
			.nUsercode = nUsercode
			
			If sAction = "Del" Then
				lintAction = 3
			Else
				If sAction = "Update" Then
					lintAction = 2
				Else
					If sAction = "Add" Then
						lintAction = 1
					End If
				End If
			End If
			
			Select Case lintAction
				Case 1
					'+ Se crea el registro
					InsPostMSI647Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InsPostMSI647Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InsPostMSI647Upd = .Delete
					
			End Select
		End With
		
InsPostMSI647Upd_Err: 
		If Err.Number Then
			InsPostMSI647Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsUpdTab_prov_zone: Realiza la actualización de la tabla
	Private Function InsUpdTab_prov_zone(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTab_prov_zone As eRemoteDB.Execute
		
		On Error GoTo InsUpdTab_prov_zone_Err
		
		lrecInsUpdTab_prov_zone = New eRemoteDB.Execute
		
		
		'+ Definición de parámetros para stored procedure 'InsUpdTab_prov_zone'
		'+ Información leída el 15/03/2002
		With lrecInsUpdTab_prov_zone
			.StoredProcedure = "InsUpdTab_prov_zone"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nZone", nZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTab_prov_zone = .Run(False)
		End With
		
InsUpdTab_prov_zone_Err: 
		If Err.Number Then
			InsUpdTab_prov_zone = False
		End If
		lrecInsUpdTab_prov_zone = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: se controla la apertura de la clase
	'---------------------------------------------------------
	Private Sub Class_Initialize_Renamed()
		'---------------------------------------------------------
		nProvider = eRemoteDB.Constants.intNull
		nZone = eRemoteDB.Constants.intNull
		nOrder = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






