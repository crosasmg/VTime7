Option Strict Off
Option Explicit On
Public Class Exchange
	'%-------------------------------------------------------%'
	'% $Workfile:: Exchange.cls                             $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 13/11/03 13.58                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Define the variables that take the Conversion values
	'- Se definen las variables que toman los valores de Convert
	Public pdblExchange As Double
	Public pdblResult As Double
	'**- Define the variables that take the Find values
	'- Se definen las variables que toman los valores de Find
	Public nCurrency As Integer
	Public dEffecdate As Date
	Public nExchange As Double
	Public dNulldate As Date
	Public nUsercode As Integer
	
	'-Variables privadas para cálculo del factor de cambio
	Private mdblAmount As Double
	Private mlngCurOrig As Integer
	Private mlngCurDes As Integer
	Private mdtmEffecdate As Date
	
	'**% Convert: This function converts the amount from one currency to another using the
	'**% Exchange conversion table, when the conversiion is applied for the same currencies
	'**% you restore -1 to indicate that the change rate is the same.
	'% Convert: esta función realiza la conversion de montos de una moneda a otra
	'%          mediante la utilización de la tabla de conversiones "Exchange",
	'%          cuando la conversión se solicita para monedas iguales se devuelve
	'%          -1 para indicar que el factor de cambio es el mismo
	Public Sub Convert(ByVal nExchange As Double, ByVal nAmount As Double, ByVal nCurOrig As Integer, ByVal nCurDes As Integer, ByVal dEffecdate As Date, ByVal nResult As Double, Optional ByVal bFind As Boolean = False)
		Dim lrecinsExchange As eRemoteDB.Execute
		
		On Error GoTo Convert_Err
		'**+ Parameter definition for the stored procedure 'insCalConvertExchange'
		'+ Definición de parámetros para stored procedure 'insCalConvertExchange'
		'**+ Information read on August 26, 199 10:36:19 a.m.
		'+ Información leída el 26/08/1999 10:36:19 AM
		If mdblAmount <> nAmount Or mlngCurOrig <> nCurOrig Or mlngCurDes <> nCurDes Or mdtmEffecdate <> dEffecdate Or bFind Then
			lrecinsExchange = New eRemoteDB.Execute
			With lrecinsExchange
				.StoredProcedure = "insCalConvertExchange2"
				.Parameters.Add("ParamnExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("ParamnAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("ParamnCurOri", nCurOrig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("ParamnCurDes", nCurDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("ParamsEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("ParamnResult", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					pdblExchange = .Parameters("ParamnExchange").Value
					pdblResult = .Parameters("ParamnResult").Value
					mdblAmount = nAmount
					mlngCurOrig = nCurOrig
					mlngCurDes = nCurDes
					mdtmEffecdate = dEffecdate
				End If
			End With
		End If
		
Convert_Err: 
		If Err.Number Then
			pdblExchange = -1
			pdblResult = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsExchange = Nothing
	End Sub
	
	'**% Find: Restores information of a record in the Exchange table.
	'% Find: Devuelve información de un regisrto en la tabla Exchange
	Public Function Find(ByVal nCurrency As Integer, ByVal dEffecdate As Date, Optional ByRef bFind As Boolean = False) As Boolean
		'**- Variable definition for the stored procedure lrecreaExchange_o
		'- Se define la variable lrecreaExchange_o
		Dim lrecreaExchange_o As eRemoteDB.Execute
		
		On Error GoTo Find_err
		If Me.nCurrency <> nCurrency Or Me.dEffecdate <> dEffecdate Or bFind Then
			
			'**+ Parameter definition for the stored procedure 'insudb.reaExchange_o'
			'+ Definición de parámetros para stored procedure 'insudb.reaExchange_o'
			'**+ Information read on December 21,2000 16:23:22
			'+ Información leída el 21/12/2000 16:23:22
			lrecreaExchange_o = New eRemoteDB.Execute
			With lrecreaExchange_o
				.StoredProcedure = "reaExchange_o"
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nCurrency = .FieldToClass("nCurrency")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					nExchange = .FieldToClass("nExchange")
					dNulldate = .FieldToClass("dNulldate")
					.RCloseRec()
					Me.nCurrency = nCurrency
					Me.dEffecdate = dEffecdate
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaExchange_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaExchange_o = Nothing
	End Function
	
	'**% Add: add records to the Exchange table.
	'%Add: Esta rutina se encarga de Añadir registro en la tabla Exchange.
	Public Function Add() As Boolean
		'**- Variable definition for the use of the SP and the parameters sent to the same.
		'-Se define la variable para el uso del SP y de los parámetros enviados al mismo
		Dim lrecupdExchange As eRemoteDB.Execute
		
		
		'+Definición de parámetros para stored procedure 'insudb.updExchange1'
		'+Información leída el 27/09/2001 11:45:23 a.m.
		On Error GoTo Add_err
		lrecupdExchange = New eRemoteDB.Execute
		With lrecupdExchange
			.StoredProcedure = "updExchange1"
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCompDate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lrecupdExchange = New eRemoteDB.Execute
				'+Definición de parámetros para stored procedure 'insudb.creExchange'
				'+Información leída el 27/09/2001 11:48:58 a.m.
				With lrecupdExchange
					.StoredProcedure = "creExchange"
					.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Add = .Run(False)
				End With
			End If
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdExchange = Nothing
	End Function
	
	'**% Update: updates records in the Exchange table.
	'%Update: Esta rutina se encarga de actualizar los registros de la tabla Exchange.
	Public Function Update() As Boolean
		'**- Variable definition for the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		Dim lrecupdExchangeEffecdate As eRemoteDB.Execute
		
		
		On Error GoTo Update_err
		'**+ Parameter definition for the stored procedure 'insudb.updExchangeEffecdate'
		'Definición de parámetros para stored procedure 'insudb.updExchangeEffecdate'
		'**+ Information read on September 21,2001 11:17:41 a.m.
		'Información leída el 27/09/2001 11:17:41 a.m.
		lrecupdExchangeEffecdate = New eRemoteDB.Execute
		With lrecupdExchangeEffecdate
			.StoredProcedure = "updExchangeEffecdate"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdExchangeEffecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdExchangeEffecdate = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValMS004_K: Function in order to validate the fields from the header
	'% insValMS004_K: Función para la validación de los campos del encabezado
	Public Function insValMS004_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nCurrency As Integer) As String
		'*- Variables declaration to control the error messages
		'-Se define la variable para controlar los mensajes de error
		Dim lclsErrors As eFunctions.Errors
		
		
		'*+ Validation of the Currency field
		'+ Se valida el campo Moneda
		On Error GoTo insValMS004_K_Err
		lclsErrors = New eFunctions.Errors
		If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10827)
		End If
		
		insValMS004_K = lclsErrors.Confirm
		
insValMS004_K_Err: 
		If Err.Number Then
			insValMS004_K = "insValMS004_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'*% insValDateExchange: Function in order to validate the exchange effective date
	'%insValDateExchange: Función que valida el día de cambio
	Private Function insValDateExchange(ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecreaExchangeEffecdate As eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insudb.reaExchangeEffecdate'
		'Definición de parámetros para stored procedure 'insudb.reaExchangeEffecdate'
		'**+ Information read on September 26, 2001 06:45:32 p.m.
		'Información leída el 26/09/2001 06:45:32 p.m.
		On Error GoTo insValDateExchange_Err
		lrecreaExchangeEffecdate = New eRemoteDB.Execute
		With lrecreaExchangeEffecdate
			.StoredProcedure = "reaExchangeEffecdate"
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insValDateExchange = .FieldToClass("dEffecdate") < dEffecdate
				.RCloseRec()
			Else
				insValDateExchange = True
			End If
		End With
		
insValDateExchange_Err: 
		If Err.Number Then
			insValDateExchange = False
		End If
		'UPGRADE_NOTE: Object lrecreaExchangeEffecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaExchangeEffecdate = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMS004: Esta función se encarga de validar los datos introducidos en la parte repetitiva
	'%de la forma.
	Public Function insValMS004(ByVal sCodispl As String, ByVal sAction As String, ByVal nSeleted As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nExchange As Double, ByVal nExchange_old As Double) As String
		
		'-Se define la variable encargada de controlar los errores masivos
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS004_Err
		lclsErrors = New eFunctions.Errors
		
		'+ Se valida el campo de Fecha de Efecto
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 99121)
		End If
		
		'+Se verifica que el campo pasado como parámetro Cambio
		If nExchange = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10054)
		Else
			If nExchange <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10076)
			ElseIf nExchange_old <> nExchange And sAction = "Update" Then 
				Call lclsErrors.ErrorMessage(sCodispl, 56055)
			ElseIf nExchange < 1 Or nExchange > 99999 Then 
				Call lclsErrors.ErrorMessage(sCodispl, 1935)
			End If
		End If
		
		If sAction <> "Update" Then
			If Not insValDateExchange(nCurrency, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10915)
			End If
		End If
		
		insValMS004 = lclsErrors.Confirm
		
insValMS004_Err: 
		If Err.Number Then
			insValMS004 = "insValMS004: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**% insPostMS004: Updates the Exchange table.
	'% insPostMS004: Actualiza la tabla Cambio de Monedas (Exchange)
	Public Function insPostMS004(ByVal sCodispl As String, ByVal sAction As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nExchange As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMS004_Err
		With Me
			.nCurrency = nCurrency
			.dEffecdate = dEffecdate
			.nExchange = nExchange
			.nUsercode = nUsercode
			sAction = Trim(sAction)
			Select Case sAction
				'**+ If the selected option is Add
				'+Si la opción seleccionada es Registrar
				Case "Add"
					insPostMS004 = .Add
					
					'**+ If the selected option is Modify
					'+Si la opción seleccionada es Modificar
				Case "Update"
					insPostMS004 = .Update
			End Select
		End With
		
insPostMS004_Err: 
		If Err.Number Then
			insPostMS004 = False
		End If
		On Error GoTo 0
	End Function
End Class






