Option Strict Off
Option Explicit On
Public Class Cap_educind
	'%-------------------------------------------------------%'
	'% $Workfile:: Cap_educind.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on October 30,2001.
	'*-Propiedades según la tabla en el sistema el 30/10/2001
	
	'Column_name                 Type                  Nulldeable
	'---------------------   ------------------------ ---------------
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public nAge As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public nCurrency As Integer 'Number(5)       No
	Public nCapschool As Double 'Number(18,6)    Yes
	Public nCaphscho As Double 'Number(18,6)    Yes
	Public dNulldate As Date 'Date            Yes
	Public dCompdate As Date 'Date            No
	Public nUsercode As Integer 'Number(5)       No
	
	Private mvarCap_educinds As Cap_educinds
	
	
	Public Property Cap_educinds() As Cap_educinds
		Get
			If mvarCap_educinds Is Nothing Then
				mvarCap_educinds = New Cap_educinds
			End If
			Cap_educinds = mvarCap_educinds
		End Get
		Set(ByVal Value As Cap_educinds)
			mvarCap_educinds = Value
		End Set
	End Property
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarCap_educinds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarCap_educinds = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%InsUpdCap_educind: Crea un registro en la tabla
	Private Function InsUpdCap_educind(ByVal nAction As Integer) As Boolean
		Dim lrecinsupdcap_educind As eRemoteDB.Execute
		
		On Error GoTo insupdcap_educind_Err
		
		lrecinsupdcap_educind = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'InsUpdCap_educind'
		'**+Information read on October 30,2001 11:58:10 a.m.
		'+Definición de parámetros para stored procedure 'InsUpdCap_educind'
		'+Información leída el 30/10/2001 11:58:10 AM
		With lrecinsupdcap_educind
			.StoredProcedure = "InsUpdCap_educind"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapschool", nCapschool, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCaphscho", nCaphscho, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdCap_educind = .Run(False)
		End With
		
insupdcap_educind_Err: 
		If Err.Number Then
			InsUpdCap_educind = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsupdcap_educind may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupdcap_educind = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdCap_educind(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdCap_educind(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdCap_educind(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nAge As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaCap_educind As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nAge <> nAge Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			lrecReaCap_educind = New eRemoteDB.Execute
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nAge = nAge
			Me.dEffecdate = dEffecdate
			
			'+Definición de parámetros para stored procedure 'reacap_educind'
			With lrecReaCap_educind
				.StoredProcedure = "reacap_educind"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nAge = nAge
					Me.dEffecdate = dEffecdate
					Me.nCurrency = .FieldToClass("nCurrency")
					Me.nCapschool = .FieldToClass("nCapschool")
					Me.nCaphscho = .FieldToClass("nCaphscho")
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
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaCap_educind may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCap_educind = Nothing
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción, según error 55611
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaCap_educind As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecReaCap_educind = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+ Definición de parámetros para stored procedure 'InsValEffecdate_Cap_educind'
		With lrecReaCap_educind
			.StoredProcedure = "InsValEffecdate_Cap_educind"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValEffecdate = Not .Run
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaCap_educind may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCap_educind = Nothing
	End Function
	
	'%InsValMVI575_K: Validaciones de la transacción(Header)
	'%                Tabla de capitales crecientes(MVI575)
	Public Function InsValMVI575_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVI575_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Se valida el Campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 9064)
			End If
			
			'+ Se valida el Campo Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11009)
			End If
			
			
			'+ Se valida el Campo Fecha
			If dEffecdate = dtmNull Then
				.ErrorMessage(sCodispl, 1103)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If Not InsValEffecdate(nBranch, nProduct, dEffecdate) Then
						.ErrorMessage(sCodispl, 55611)
					End If
				End If
			End If
			
			InsValMVI575_K = .Confirm
		End With
		
InsValMVI575_K_Err: 
		If Err.Number Then
			InsValMVI575_K = "InsValMVI575_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%InsValMVI575: Validaciones de la transacción
	'%              Tabla de capitales del seguro escolar/universitario(MVI575)
	Public Function InsValMVI575(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nAge As Integer, ByVal dEffecdate As Date, ByVal nCapschool As Double, ByVal nCaphscho As Double, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValTable As eFunctions.Values
		
		On Error GoTo InsValMVI575_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Se valida el campo Edad del beneficiario
			If nAge = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Edad del beneficiario: ")
			Else
				If sAction = "Add" Then
					If Find(nBranch, nProduct, nAge, dEffecdate, True) Then
						.ErrorMessage(sCodispl, 55610)
					End If
				End If
			End If
			
			'+Se valida los campos Capital escolar/universitario
			If nCapschool = eRemoteDB.Constants.intNull And nCaphscho = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3040)
			End If
			
			'+Se valida el campo Moneda
			If nCurrency = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 10107)
			End If
			
			InsValMVI575 = .Confirm
		End With
		
InsValMVI575_Err: 
		If Err.Number Then
			InsValMVI575 = "InsValMVI575: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTable = Nothing
	End Function
	
	'%InsPostMVI575: Ejecuta el post de la transacción
	'%               Tabla de capitales del seguro escolar/universitario(MVI575)
	Public Function InsPostMVI575(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nAge As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nCapschool As Double, ByVal nCaphscho As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMVI575_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nAge = nAge
			.nCurrency = nCurrency
			.dEffecdate = dEffecdate
			.nCapschool = nCapschool
			.nCaphscho = nCaphscho
			.dNulldate = dNulldate
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVI575 = Add
			Case "Update"
				InsPostMVI575 = Update
			Case "Del"
				InsPostMVI575 = Delete
		End Select
		
InsPostMVI575_Err: 
		If Err.Number Then
			InsPostMVI575 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nAge = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nCurrency = eRemoteDB.Constants.intNull
		nCapschool = eRemoteDB.Constants.intNull
		nCaphscho = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






