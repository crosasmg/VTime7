Option Strict Off
Option Explicit On
Public Class Tab_intproy
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_intproy.cls                          $%'
	'% $Author:: Mvazquez                                   $%'
	'% $Date:: 10-09-15 8:11                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system on 16/10/2014
	'- Propiedades según la tabla en el sistema 16/10/2014
	
	'Column_name                                    Type        Computed  Length  Prec  Scale Nullable                          TrimTrailingBlanks                  FixedLenNullInSource
	Public dEffecdate As Date 'datetime     no        8                    no                                  (n/a)                               (n/a)
	Public nIntproy_min As Double 'number(18,6) no        2        5     0     no                                  (n/a)                               (n/a)
	Public nIntproy_max As Double 'number(18,6) no        2        5     0     no                                  (n/a)                               (n/a)
	Public dNulldate As Date 'datetime     no        8                    yes                                 (n/a)                               (n/a)
	Public nSvsproy_min As Double 'number(18,6) no        8                    yes                                 (n/a)                               (n/a)
	Public nSvsproy_max As Double 'number(18,6) no        8                    yes                                 (n/a)                               (n/a)
	Public nMonths_min As Integer 'integer      no        8                    yes                                 (n/a)                               (n/a)
	Public nMonths_max As Integer 'integer      no        8                    yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'integer      no        8                    yes                                 (n/a)                               (n/a)
	Public nAction As Integer 'integer      no        8                    yes                                 (n/a)                               (n/a)
	
	'%InsUpdTab_am_exc: Realiza la actualización de la tabla
	Private Function InsUpdTab_intproy(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTab_intproy As eRemoteDB.Execute
		
		On Error GoTo InsUpdTab_intproy_Err
		lrecInsUpdTab_intproy = New eRemoteDB.Execute
		'+ Definición de store procedure InsUpdTab_am_exc al 09-30-2002 17:48:33
		With lrecInsUpdTab_intproy
			.StoredProcedure = "INSMVI8022PKG.INSPOSTMVI8022UPD"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntproy_min", nIntproy_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntproy_max", nIntproy_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSvsproy_min", nSvsproy_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSvsproy_max", nSvsproy_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonths_min", nMonths_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonths_max", nMonths_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTab_intproy = .Run(False)
		End With
		
InsUpdTab_intproy_Err: 
		If Err.Number Then
			InsUpdTab_intproy = False
		End If
		lrecInsUpdTab_intproy = Nothing
		On Error GoTo 0
	End Function
	
	
	'% insValMVI8022: Realiza las validaciones correspondientes al encabezado de la MVI8022
	Public Function insValMVI8022_k(ByVal sCodispl As String, ByVal dEffecdate As Date) As String
		On Error GoTo insValMVI8022_k_err

        Dim lstrErrorAll As String = ""
        Dim lclsError As eFunctions.Errors
		Dim lrecinsValMVI8022_k As eRemoteDB.Execute
		
		lclsError = New eFunctions.Errors
		lrecinsValMVI8022_k = New eRemoteDB.Execute
		
		'+ Se verifica que la fecha sea válida
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsError.ErrorMessage(sCodispl, 4003)
		Else
			
			'+ Se define el store de validación
			With lrecinsValMVI8022_k
				.StoredProcedure = "INSMVI8022PKG.INSVALMVI8022_K"
				.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					lstrErrorAll = .Parameters("sArrayerrors").Value
				End If
			End With
			
			lrecinsValMVI8022_k = Nothing
			
			'+ Se verifica si ha habido errores
			With lclsError
				If Len(lstrErrorAll) > 0 Then
					.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrorAll)
				End If
			End With
		End If
		
		insValMVI8022_k = lclsError.Confirm
		
insValMVI8022_k_err: 
		If Err.Number Then insValMVI8022_k = "insValMVI8022_k: " & Err.Description
		
		lclsError = Nothing
		
		On Error GoTo 0
	End Function
	
	
	'% insValMVI8022: Realiza las validaciones correspondientes al detalle MVI8022
	Public Function insValMVI8022(ByVal sCodispl As String, ByVal nIntproy_min As Double, ByVal nIntproy_max As Double, ByVal dNulldate As Date, ByVal nSvsproy_min As Double, ByVal nSvsproy_max As Double, ByVal nMonths_min As Integer, ByVal nMonths_max As Integer) As String
		On Error GoTo insValMVI8022_err

        Dim lstrErrorAll As String = ""
        Dim lclsError As eFunctions.Errors
		Dim lrecinsValMVI8022 As eRemoteDB.Execute
		
		lclsError = New eFunctions.Errors
		lrecinsValMVI8022 = New eRemoteDB.Execute
		
		'+ Se valida si el registro está anulado
		If dNulldate <> eRemoteDB.Constants.dtmNull Then
			lclsError.ErrorMessage(sCodispl, 80080)
		Else
			'+ Se verifica que la fecha sea válida
			With lrecinsValMVI8022
				.StoredProcedure = "INSMVI8022PKG.INSVALMVI8022"
				.Parameters.Add("nIntproy_min", nIntproy_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIntproy_max", nIntproy_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSvsproy_min", nSvsproy_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSvsproy_max", nSvsproy_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMonths_min", nMonths_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMonths_max", nMonths_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					lstrErrorAll = .Parameters("sArrayerrors").Value
				End If
			End With
			
			lrecinsValMVI8022 = Nothing
			
			'+ Se verifica si ha habido errores
			With lclsError
				If Len(lstrErrorAll) > 0 Then
					.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrorAll)
				End If
			End With
		End If
		
		insValMVI8022 = lclsError.Confirm
		
insValMVI8022_err: 
		If Err.Number Then insValMVI8022 = "insValMVI8022: " & Err.Description
		
		lclsError = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insPostMVI8022: Actualiza los datos de la forma.
	Public Function insPostMVI8022(ByVal sAction As String, ByVal dEffecdate As Date, ByVal nIntproy_min As Double, ByVal nIntproy_max As Double, ByVal nUsercode As Integer, ByVal nSvsproy_min As Double, ByVal nSvsproy_max As Double, ByVal nMonths_min As Integer, ByVal nMonths_max As Integer) As Boolean
		
		On Error GoTo insPostMVI8022_err
		
		Me.dEffecdate = dEffecdate
		Me.nIntproy_min = nIntproy_min
		Me.nIntproy_max = nIntproy_max
		Me.nSvsproy_min = nSvsproy_min
		Me.nSvsproy_max = nSvsproy_max
		Me.nMonths_min = nMonths_min
		Me.nMonths_max = nMonths_max
		Me.nUsercode = nUsercode
		
		Select Case sAction
			
			'+ Se verifica que la opción seleccionada es Agregar
			Case "Add"
				insPostMVI8022 = Me.Add
				
				'+ Se verifica que la opción seleccionada es Modificar
			Case "Update"
				insPostMVI8022 = Me.Update
				
				'+ Se verifica que la opción seleccionada es Eliminar
			Case "Del"
				insPostMVI8022 = Me.Delete
		End Select
		
insPostMVI8022_err: 
		If Err.Number Then insPostMVI8022 = False
		
	End Function
	
	'%Add: Crea los datos de la tabla
	Public Function Add() As Boolean
		Add = InsUpdTab_intproy(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdTab_intproy(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTab_intproy(3)
	End Function
	
	'*sContent: Obtiene el indicador de contenido de la transacción
	Public ReadOnly Property sContent() As String
		Get
			sContent = mstrContent
		End Get
	End Property
	
	'%Class_Initialize: Se ejecuta cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		dEffecdate = eRemoteDB.Constants.dtmNull
		nIntproy_min = eRemoteDB.Constants.intNull
		nIntproy_max = eRemoteDB.Constants.intNull
		nSvsproy_min = eRemoteDB.Constants.intNull
		nSvsproy_max = eRemoteDB.Constants.intNull
		nMonths_min = eRemoteDB.Constants.intNull
		nMonths_max = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






