Option Strict Off
Option Explicit On
Public Class Tab_Prov_Group
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Prov_Group.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla INSUDB.Tab_Prov_Group al 10-14-2002 11:59:47
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nProvider As Integer ' NUMBER     22   0     5    N
	Public nProv_group As Integer ' NUMBER     22   0     5    N
	Public dInpdate As Date ' DATE       7    0     0    S
	Public dOutdate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdTab_Prov_Group: Se encarga de actualizar la tabla Tab_Prov_Group
	Private Function InsUpdTab_Prov_Group(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdtab_prov_group As eRemoteDB.Execute
		On Error GoTo insUpdtab_prov_group_Err
		
		lrecinsUpdtab_prov_group = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdtab_prov_group al 10-14-2002 12:32:49
		'+
		With lrecinsUpdtab_prov_group
			.StoredProcedure = "insUpdtab_prov_group"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProv_group", nProv_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOutdate", dOutdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTab_Prov_Group = .Run(False)
		End With
		
insUpdtab_prov_group_Err: 
		If Err.Number Then
			InsUpdTab_Prov_Group = False
		End If
		lrecinsUpdtab_prov_group = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTab_Prov_Group(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTab_Prov_Group(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTab_Prov_Group(3)
	End Function
	
	'%InsValMSI019: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MSI019)
	Public Function InsValMSI019(ByVal sCodispl As String, ByVal sAction As String, ByVal nProvider As Integer, ByVal dInpdate As Date, ByVal dOutdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_Provider As Tab_Provider
		
		On Error GoTo InsValMSI019_Err
		lclsErrors = New eFunctions.Errors
		lclsTab_Provider = New Tab_Provider
		
		With lclsErrors
			
			If sAction = "Update" Then
				.ErrorMessage(sCodispl, 10917)
			End If
			'+ Valida fecha de ingreso
			If dInpdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 9013)
			Else
				lclsTab_Provider.FindDatesProvider(nProvider)
				If lclsTab_Provider.dInpdate > dInpdate Then
					.ErrorMessage(sCodispl, 10918)
				End If
			End If
			
			'+ Valida fecha de egreso
			If sAction = "Update" Then
				If dOutdate = eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 10920)
				Else
					If dOutdate < dInpdate Then
						.ErrorMessage(sCodispl, 10919)
					End If
				End If
			End If
			
			InsValMSI019 = .Confirm
		End With
		
InsValMSI019_Err: 
		If Err.Number Then
			InsValMSI019 = "InsValMSI019: " & Err.Description
		End If
		lclsTab_Provider = Nothing
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMSI019: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MSI019)
	Public Function InsPostMSI019(ByVal sAction As String, ByVal nProvider As Integer, ByVal nProv_group As Integer, ByVal dInpdate As Date, ByVal dOutdate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMSI019_Err
		
		With Me
			.nProvider = nProvider
			.nProv_group = nProv_group
			.dInpdate = dInpdate
			.dOutdate = dOutdate
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMSI019 = Add
			Case "Update"
				InsPostMSI019 = Update
			Case "Del"
				InsPostMSI019 = Delete
		End Select
		
InsPostMSI019_Err: 
		If Err.Number Then
			InsPostMSI019 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nProvider = eRemoteDB.Constants.intNull
		nProv_group = eRemoteDB.Constants.intNull
		dInpdate = eRemoteDB.Constants.dtmNull
		dOutdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






