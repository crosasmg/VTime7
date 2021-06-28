Option Strict Off
Option Explicit On
Public Class Ctrol_premin
	'%-------------------------------------------------------%'
	'% $Workfile:: Ctrol_premin.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on December 27,2000.
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	
	'Column_Name                                Type      Length  Prec  Scale  Nullable
	'------------------------- --------------- --------   ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0    No
	Public nProduct As Integer ' NUMBER        22     5      0    No
	Public nMonth As Integer ' NUMBER        22     5      0    No
	Public dEffecdate As Date ' DATE           7                 No
	Public nRate As Double ' NUMBER        22     9      6    Yes
	Public nAmount As Double ' NUMBER        22    10      2    Yes
	Public dNulldate As Date ' DATE           7                 Yes
	Public nUsercode As Integer ' NUMBER        22     5      0    No
	
	'%InsUpdCtrol_premin: Se encarga de actualizar la tabla
	Private Function InsUpdCtrol_premin(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdCtrol_premin As eRemoteDB.Execute
		
		On Error GoTo InsUpdCtrol_premin_Err
		
		lrecInsUpdCtrol_premin = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdCtrol_premin'
		'+Información leída el 25/10/01
		With lrecInsUpdCtrol_premin
			.StoredProcedure = "InsUpdCtrol_premin"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdCtrol_premin = .Run(False)
		End With
		
InsUpdCtrol_premin_Err: 
		If Err.Number Then
			InsUpdCtrol_premin = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdCtrol_premin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdCtrol_premin = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdCtrol_premin(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdCtrol_premin(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdCtrol_premin(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaCtrol_premin As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nMonth <> nMonth Or Me.dEffecdate <> dEffecdate Or bFind Then
			
			lrecReaCtrol_premin = New eRemoteDB.Execute
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nMonth = nMonth
			Me.dEffecdate = dEffecdate
			
			'+Definición de parámetros para stored procedure 'ReaCtrol_premin'
			'+Información leída el 25/10/01
			With lrecReaCtrol_premin
				.StoredProcedure = "ReaCtrol_premin"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nMonth = nMonth
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nRate = .FieldToClass("nRate")
					Me.nAmount = .FieldToClass("nAmount")
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
		'UPGRADE_NOTE: Object lrecReaCtrol_premin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCtrol_premin = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción, según error 10869
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaCtrol_premin As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecReaCtrol_premin = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+Definición de parámetros para stored procedure 'ReaCtrol_premin'
		With lrecReaCtrol_premin
			.StoredProcedure = "InsValEffecdate_Ctrol_premin"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValEffecdate = Not .Run
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecReaCtrol_premin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCtrol_premin = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA695_K: Validaciones de la transacción(Header)
	'%                Tabla de control de prima mínima(MVA695)
	Public Function InsValMVA695_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVA695_K_Err
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
			
			InsValMVA695_K = .Confirm
		End With
		
InsValMVA695_K_Err: 
		If Err.Number Then
			InsValMVA695_K = "InsValMVA695_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA695: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MVA695)
	Public Function InsValMVA695(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal dEffecdate As Date, ByVal nRate As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVA695_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Se valida el campo Mes de vigencia
			If nMonth = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55596)
			Else
				
				'+Validar que no se dupliquen registros
				If sAction = "Add" Then
					If Find(nBranch, nProduct, nMonth, dEffecdate) Then
						.ErrorMessage(sCodispl, 55595)
					End If
				End If
				
			End If
			
			'+Se valida el campo Factor
			If nRate = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55597)
			End If
			
			InsValMVA695 = .Confirm
		End With
		
InsValMVA695_Err: 
		If Err.Number Then
			InsValMVA695 = "InsValMVA695: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVA695: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MVA695)
	Public Function InsPostMVA695(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth As Integer, ByVal dEffecdate As Date, ByVal nRate As Double, ByVal nAmount As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMVA695_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nMonth = nMonth
			.dEffecdate = dEffecdate
			.nRate = nRate
			.nAmount = nAmount
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVA695 = Add
			Case "Update"
				InsPostMVA695 = Update
			Case "Del"
				InsPostMVA695 = Delete
		End Select
		
InsPostMVA695_Err: 
		If Err.Number Then
			InsPostMVA695 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nMonth = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nRate = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		dNulldate = System.Date.FromOADate(eRemoteDB.Constants.intNull)
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






