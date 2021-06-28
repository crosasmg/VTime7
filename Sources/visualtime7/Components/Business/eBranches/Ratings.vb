Option Strict Off
Option Explicit On
Public Class Ratings
	'%-------------------------------------------------------%'
	'% $Workfile:: Ratings.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'*-Propiedades según la tabla en el sistema el 16/10/2001
	'Column_name                 Type                  Nulleable
	'---------------------   ------------------------ ---------------
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public nAge_ini As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public nAge_end As Integer 'Number(5)       No
	Public dNulldate As Date 'Date            Yes
	Public nRating As Integer 'Number(5)       No
	Public nUsercode As Integer 'Number(5)       No
	
	'%InsUpdRatings: Actualiza la tabla
	Private Function InsUpdRatings(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdRatings As eRemoteDB.Execute
		
		On Error GoTo InsUpdRatings_Err
		
		lrecInsUpdRatings = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdRatings'
		'+Información leída el 16/10/2001
		With lrecInsUpdRatings
			.StoredProcedure = "InsUpdRatings"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRating", nRating, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdRatings = .Run(False)
		End With
		
InsUpdRatings_Err: 
		If Err.Number Then
			InsUpdRatings = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdRatings may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdRatings = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdRatings(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdRatings(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdRatings(3)
	End Function
	
	'%InsValRange: Valida que el rango indicado no este dentro de otro rango
	Public Function InsValRange(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_ini As Integer, ByVal nAge_end As Integer) As Boolean
		Dim lrecRatings As eRemoteDB.Execute
		
		On Error GoTo InsValRange_Err
		
		InsValRange = True
		lrecRatings = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsValRange'
		With lrecRatings
			.StoredProcedure = "InsValRangeInRatings"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nRating = .FieldToClass("nRating")
				.RCloseRec()
			Else
				InsValRange = False
			End If
		End With
		
InsValRange_Err: 
		If Err.Number Then
			InsValRange = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRatings may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRatings = Nothing
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción, según error 10869
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaRatings As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecReaRatings = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+Definición de parámetros para stored procedure 'ReaRatings'
		With lrecReaRatings
			.StoredProcedure = "InsValEffecdate_Ratings"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsValEffecdate = False
				.RCloseRec()
			End If
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecReaRatings may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRatings = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA740_K: Validaciones de la transacción(Header)
	'%                Rating por productos(MVA740)
	Public Function InsValMVA740_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVA740_K_Err
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
			
			InsValMVA740_K = .Confirm
		End With
		
InsValMVA740_K_Err: 
		If Err.Number Then
			InsValMVA740_K = "InsValMVA740_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA740: Validaciones de la transacción
	'%              Rating por productos(MVA740)
	Public Function InsValMVA740(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_ini As Integer, ByVal nAge_end As Integer, ByVal nRating As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo InsValMVA740_Err
		lblnError = False
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Se valida el campo Edad inicial
			If nAge_ini < 0 Then
				.ErrorMessage(sCodispl, 55573)
				lblnError = True
			End If
			
			'+Se valida el campo Edad Final
			If nAge_end <= nAge_ini Then
				.ErrorMessage(sCodispl, 55574)
				lblnError = True
			End If
			
			'+Se valida el campo Rating
			If nRating = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55575)
			End If
			
			'+Se valida que se dupliquen registros
			If sAction = "Add" And Not lblnError Then
				If InsValRange(nBranch, nProduct, dEffecdate, nAge_ini, nAge_end) Then
					.ErrorMessage(sCodispl, 55572)
				End If
			End If
			
			InsValMVA740 = .Confirm
		End With
		
InsValMVA740_Err: 
		If Err.Number Then
			InsValMVA740 = "InsValMVA740: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVA740: Ejecuta el post de la transacción
	'%               Rating por productos(MVA740)
	Public Function InsPostMVA740(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_ini As Integer, ByVal nAge_end As Integer, ByVal nRating As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMVA740_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nAge_ini = nAge_ini
			.nAge_end = nAge_end
			.nRating = nRating
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVA740 = Add
			Case "Update"
				InsPostMVA740 = Update
			Case "Del"
				InsPostMVA740 = Delete
		End Select
		
InsPostMVA740_Err: 
		If Err.Number Then
			InsPostMVA740 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nAge_ini = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nAge_end = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nRating = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






