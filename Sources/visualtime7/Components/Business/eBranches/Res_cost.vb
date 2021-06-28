Option Strict Off
Option Explicit On
Public Class Res_cost
	'%-------------------------------------------------------%'
	'% $Workfile:: Res_cost.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Properties according to the table 'RES_COST' in the system 15/03/2002 11:32:08 AM
	'+ Propiedades según la tabla 'RES_COST' en el sistema 15/03/2002 11:32:08 AM
	
	'Column_name                 Type                  Nulldeable
	'---------------------   ------------------------ ---------------
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public nPeriod As Integer 'Number(5)       Yes
	Public nRec_comm As Double 'Number(6,4)     Yes
	Public nRec_sale As Double 'Number(6,4)     Yes
	Public dNulldate As Date 'Date            Yes
	Public dCompdate As Date 'Date            No
	Public nUsercode As Integer 'Number(5)       No
	Private mvarRes_costs As Res_costs
	
	
	Public Property Res_costs() As Res_costs
		Get
			If mvarRes_costs Is Nothing Then
				mvarRes_costs = New Res_costs
			End If
			
			Res_costs = mvarRes_costs
		End Get
		Set(ByVal Value As Res_costs)
			mvarRes_costs = Value
		End Set
	End Property
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarRes_costs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarRes_costs = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	'%InsUpdRes_cost: Crea un registro en la tabla
	Private Function InsUpdRes_cost(ByVal nAction As Integer) As Boolean
		Dim lrecinsupdRes_cost As eRemoteDB.Execute
		
		On Error GoTo insupdRes_cost_Err
		
		lrecinsupdRes_cost = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insupdres_cost'
		'+ Información leída el 16/03/2002
		With lrecinsupdRes_cost
			.StoredProcedure = "InsUpdRes_cost"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriod", nPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRec_comm", nRec_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRec_sale", nRec_sale, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdRes_cost = .Run(False)
		End With
		
insupdRes_cost_Err: 
		If Err.Number Then
			InsUpdRes_cost = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsupdRes_cost may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupdRes_cost = Nothing
	End Function
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdRes_cost(1)
	End Function
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdRes_cost(2)
	End Function
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdRes_cost(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPeriod As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaRes_cost As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPeriod <> nPeriod Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			lrecReaRes_cost = New eRemoteDB.Execute
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nPeriod = nPeriod
			Me.dEffecdate = dEffecdate
			
			'+ Definición de parámetros para stored procedure 'reaRes_cost'
			With lrecReaRes_cost
				.StoredProcedure = "reaRes_cost"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPeriod", nPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPeriod = nPeriod
					Me.dEffecdate = dEffecdate
					Me.nRec_sale = .FieldToClass("nRec_sale")
					Me.nRec_comm = .FieldToClass("nRec_comm")
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
		'UPGRADE_NOTE: Object lrecReaRes_cost may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRes_cost = Nothing
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción, según error 55611
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaRes_cost As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecReaRes_cost = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+ Definición de parámetros para stored procedure 'InsValEffecdate_Res_cost'
		With lrecReaRes_cost
			.StoredProcedure = "InsValEffecdate_Res_cost"
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
		'UPGRADE_NOTE: Object lrecReaRes_cost may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRes_cost = Nothing
	End Function
	
	'%InsValMVI807_K: Validaciones de la transacción(Header)
	'%                Tabla de capitales crecientes(MVI807)
	Public Function InsValMVI807_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVI807_K_Err
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
			
			InsValMVI807_K = .Confirm
		End With
		
InsValMVI807_K_Err: 
		If Err.Number Then
			InsValMVI807_K = "InsValMVI807_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%InsValMVI807: Validaciones de la transacción
	'%              (MVI807)
	Public Function InsValMVI807(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPeriod As Integer, ByVal dEffecdate As Date, ByVal nRec_sale As Double, ByVal nRec_comm As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVI807_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Campo Años de período de pago debe estar lleno
			If nPeriod = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Periodo:")
			End If
			
			'+ Valida que no exista registro con la misma clave
			If sAction = "Add" Then
				If Find(nBranch, nProduct, nPeriod, dEffecdate) Then
					.ErrorMessage(sCodispl, 55661)
				End If
			End If
			
			'+ Se valida los campos % gastos ventas, % comisión
			If nRec_comm = eRemoteDB.Constants.intNull And nRec_sale = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55662)
			End If
			
			InsValMVI807 = .Confirm
		End With
InsValMVI807_Err: 
		If Err.Number Then
			InsValMVI807 = "InsValMVI807: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%InsPostMVI807: Ejecuta el post de la transacción
	'%               Tabla de capitales del seguro escolar/universitario(MVI807)
	Public Function InsPostMVI807(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPeriod As Integer, ByVal dEffecdate As Date, ByVal nRec_sale As Double, ByVal nRec_comm As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMVI807_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nPeriod = nPeriod
			.dEffecdate = dEffecdate
			.nRec_sale = nRec_sale
			.nRec_comm = nRec_comm
			.dNulldate = dNulldate
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVI807 = Add
			Case "Update"
				InsPostMVI807 = Update
			Case "Del"
				InsPostMVI807 = Delete
		End Select
		
InsPostMVI807_Err: 
		If Err.Number Then
			InsPostMVI807 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPeriod = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nRec_sale = eRemoteDB.Constants.intNull
		nRec_comm = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






