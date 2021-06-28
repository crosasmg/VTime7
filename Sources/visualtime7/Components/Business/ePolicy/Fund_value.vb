Option Strict Off
Option Explicit On
Public Class Fund_value
	'%-------------------------------------------------------%'
	'% $Workfile:: Fund_value.cls                           $%'
	'% $Author:: Mpalleres                                  $%'
	'% $Date:: 30-09-09 12:42                               $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**- properties according the table in the system 09/04/2001
	'**- Fund value " nominal values of the units of a fund"
	'- Propiedades según la tabla en el sistema 09/04/2001
	'- Fund_value "Valor nominal de las unidades de un fondo"
	
	'Column_name                  Type              Computed        Length      Prec  Scale Nullable       TrimTrailingBlanks     FixedLenNullInSource
	'--------------------------- -------------------------------------------------------------------------------------------------------------------------
	Public nFunds As Integer 'smallint           no              2           5     0     no                 (n/a)                   (n/a)
	Public nAmount As Double 'decimal            no              9           12    6     yes                (n/a)                   (n/a)
	Public nCurrency As Integer 'smallint           no              2           5     0     yes                (n/a)                   (n/a)
	Public dEffecdate As Date 'datetime           no              8                       no                 (n/a)                   (n/a)
	Public dNulldate As Date 'datetime           no              8                       yes                (n/a)                   (n/a)
	Public nUsercode As Integer 'smallint           no              2           5     0     no                 (n/a)                   (n/a)
	
	'**- Variable definition that contain the description fund
	'- Se define la variable que contiene la descripción del fondo
	
	Public sFoundDescript As String
	
	'**- Variable definition that contain the ready units of the fund
	'- Se define la variable que contiene las unidades disponibles del fondo
	
	Public nQuan_avail As Double
	
	'**- Variable definition that determinate the class status
	'- Se define la variable que determina el estado de la clase
	
    Public nStatInstanc As eBranches.Insured_he.eStatusInstance
	
	'**- Defines the enumerate that contain the error type when the record
	'**- It is duplicated
	'- Se define el enumerado que contiene el tipo de error cuando el registro se
	'- encuentre duplicado
	
	Private Enum DupError
		DupTable = 1
		DupGrid = 2
	End Enum
	
	'**- Variable definition that will contain the error type when the record
	'**- it is duplicated
	'- Se define la variable que contendrá el tipo de error cuando el registro se
	'- encuentre duplicado
	
	Private mintError As DupError
	
	'**% insPostMVI002: Updates the data of the form.
	'% insPostMVI002: Actualiza los datos de la forma.
	Public Function insPostMVI002(ByVal sAction As String, ByVal nFunds As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMVI002_err
		
		Dim lclsFund_value As ePolicy.Fund_value
		lclsFund_value = New ePolicy.Fund_value
		
		With lclsFund_value
			.nFunds = nFunds
			.dEffecdate = dEffecdate
			.nAmount = nAmount
			.nCurrency = nCurrency
			.nUsercode = nUsercode
			
			Select Case sAction
				
				'**+ If the selected option is Record.
				'+ Si la opción seleccionada es Registrar
				
				Case "Add"
					.dNulldate = eRemoteDB.Constants.dtmNull
					insPostMVI002 = .Add
					
					'**+ If the selected option is Modify
					'+ Si la opción seleccionada es Modificar
					
				Case "Update"
					insPostMVI002 = .Update
			End Select
		End With
		
insPostMVI002_err: 
		If Err.Number Then insPostMVI002 = False
		
		'UPGRADE_NOTE: Object lclsFund_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_value = Nothing
	End Function
	
	'**% insValMVI002: Allows to make the proper validates of the transaction
	'% insValMVI002: Realiza las validaciones propias de la transacción.
	Public Function insValMVI002(ByVal sCodispl As String, ByVal nFunds As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal sSchema_code As String, ByVal dFundDate As Date, ByVal dEffecdate As Date) As String
		On Error GoTo insValMVI002_Err
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsScheCur As eSecurity.Secur_sche
		Dim lclsFund_values As ePolicy.Fund_values
		
		lclsErrors = New eFunctions.Errors
		lclsScheCur = New eSecurity.Secur_sche
		lclsFund_values = New ePolicy.Fund_values
		
		'+ Validación del campo "Fondo".
		If nFunds <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012)
		End If
		
		'+ Validación del campo "Moneda".
		If nCurrency <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10827)
		Else
			If Not lclsScheCur.valCurrency(sSchema_code, nCurrency) Then
				Call lclsErrors.ErrorMessage(sCodispl, 99024)
			End If
		End If
		
		'+ Validación del "Valor de la unidad de fondos".
		If nAmount = 0 Or nAmount = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1935)
		End If
		
		'+ Se verifica que la fecha sea posterior a la de la última transacción
		If Not FindDateVal(dEffecdate, nFunds) Then
			lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
			Call lclsErrors.ErrorMessage(sCodispl, 80500)
		Else
			'+ No debe permitir ingresar valor cuota a una fecha futura, si el día hábil precedente
			'+ no tiene valor cuota ingresado
			If Not ValHollidayExist(dEffecdate, nFunds) Then
				Call lclsErrors.ErrorMessage(sCodispl, 80501)
			End If
		End If
		
		insValMVI002 = lclsErrors.Confirm
		
insValMVI002_Err: 
		If Err.Number Then insValMVI002 = insValMVI002 & Err.Description
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsScheCur may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsScheCur = Nothing
		'UPGRADE_NOTE: Object lclsFund_values may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_values = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insValMVI002: Realiza las validaciones correspondientes, según lo indica el funcional de
	'% la transacción.
	Public Function insValMVI002_k(ByVal sCodispl As String, ByVal dEffecdate As Date) As String
		On Error GoTo insValMVI002_k_err
		
		Dim lclsError As eFunctions.Errors
		
		lclsError = New eFunctions.Errors
		
		'+ Se verifica que la fecha sea válida
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsError.ErrorMessage(sCodispl, 4003)
		End If
		
		insValMVI002_k = lclsError.Confirm
		
insValMVI002_k_err: 
		If Err.Number Then insValMVI002_k = "insValMVI002_k: " & Err.Description
		
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
		
		On Error GoTo 0
	End Function
	
	'**% Find:Allows search the nominal value of the fund units
	'% Find: Busca el valor nominal de las unidades de un fondo
	Public Function Find() As Boolean
		Dim lrecreaFund_value As eRemoteDB.Execute
		
		lrecreaFund_value = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+ Parameters definition to stored procedure ' insudb.reaFund_value'
		'**+ Data read on 04/09/2001 04:33:59 PM
		'+ Definición de parámetros para stored procedure 'insudb.reaFund_value'
		'+ Información leída el 09/04/2001 04:33:59 PM
		
		With lrecreaFund_value
			.StoredProcedure = "reaFund_value"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nCurrency = .FieldToClass("nCurrency")
				nAmount = .FieldToClass("nAmount")
				dEffecdate = .FieldToClass("dEffecDate")
				
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then Find = False
		
		'UPGRADE_NOTE: Object lrecreaFund_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_value = Nothing
	End Function
	
	'**% Add: Allows to create a record in the nominal value table of the fund units.
	'% Add: Permite crear un registro en la tabla de Valor nominal de las unidades de un fondo.
	Public Function Add() As Boolean
		Dim lreccreFund_value As eRemoteDB.Execute
		
		lreccreFund_value = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		Add = True
		
		'**+ Parameters definition to stored procedure 'insudb.creFund_value'
		'**+ Data read on 04/09/2001 15:08:39
		'+ Definición de parámetros para stored procedure 'insudb.creFund_value'
		'+ Información leída el 09/04/2001 15:08:39
		
		With lreccreFund_value
			.StoredProcedure = "creFund_value"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then Add = False
		
		'UPGRADE_NOTE: Object lreccreFund_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFund_value = Nothing
	End Function
	
	'**% Update: Allows to update a record in the nominal value table of the fund units.
	'% Update: Permite actualizar un registro en la tabla de Valor nominal de las unidades de un fondo.
	Public Function Update() As Boolean
		Dim lrecupdFund_value As eRemoteDB.Execute
		
		lrecupdFund_value = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		Update = True
		
		'**+ Parameters definition to stored procedure ' insudb.upFund_value'
		'**+ Data read on 04/06/2001 15:11:25
		'+ Definición de parámetros para stored procedure 'insudb.updFund_value'
		'+ Información leída el 06/04/2001 15:11:25
		
		With lrecupdFund_value
			.StoredProcedure = "updFund_value"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then Update = False
		
		'UPGRADE_NOTE: Object lrecupdFund_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFund_value = Nothing
	End Function
	
	'**% Delete: Allows to delete a record of the nominal value table of the fund units.
	'% Delete : Permite eliminar un registro de la tabla de Valor nominal de las unidades de un fondo.
	Public Function Delete() As Boolean
		Dim lrecdelFund_value As eRemoteDB.Execute
		
		lrecdelFund_value = New eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		Delete = True
		
		'**+ Parameters definition to stored procedure 'insudb.delFund_value'
		'**+ Data read on 04/09/2001  15:13:24
		'+ Definición de parámetros para stored procedure 'insudb.delFund_value'
		'+ Información leída el 09/04/2001 15:13:24
		
		With lrecdelFund_value
			.StoredProcedure = "delFund_value"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then Delete = False
		
		'UPGRADE_NOTE: Object lrecdelFund_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFund_value = Nothing
	End Function
	
	'**% insValMVIC001: Allows to make the corresponding validates, according to the funcional
	'**% transaction
	'% insValMVIC001: Realiza las validaciones correspondientes, según lo indica el funcional de
	'% la transacción.
	Public Function insValMVIC001_k(ByVal sCodispl As String, ByVal nFunds As Integer, ByVal nCurrency As Integer) As String
		
		On Error GoTo insValMVIC001_k_err
		
		Dim lclsErrors As eFunctions.Errors
        Dim lclsValues As eFunctions.Values

        lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		
		If nFunds <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(258) & ":")
        End If
		
		If nCurrency <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(35) & ":")
        End If
		
		insValMVIC001_k = lclsErrors.Confirm
		
insValMVIC001_k_err: 
		If Err.Number Then insValMVIC001_k = "insValMVIC001_k: " & Err.Description
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		
		On Error GoTo 0
	End Function
	
	'ValHollidayExist: Función que valida la existencia de valor cuota para un día hábil precedente a la fecha ingresada
	Public Function ValHollidayExist(ByVal dEffecdate As Date, ByVal nFunds As Integer) As Boolean
		Dim lclsFund As eRemoteDB.Execute
		Dim lblnExist As Boolean
		
		On Error GoTo ValHollidayExist_Err
		lclsFund = New eRemoteDB.Execute
		
		With lclsFund
			.StoredProcedure = "Valfundholliday"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("nExist") = 1 Then
					ValHollidayExist = True
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lclsFund may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund = Nothing
		
ValHollidayExist_Err: 
		If Err.Number Then
			ValHollidayExist = False
		End If
		On Error GoTo 0
	End Function


    '% FindDateVal: Selecciona la última fecha en lacual se permite la modificaion
    ' de valores par aun fondo
    Public Function FindDateVal(ByVal dEffecdate As Date, ByVal nFunds As Integer) As Boolean
        Dim lrecFindDateVal As eRemoteDB.Execute
        lrecFindDateVal = New eRemoteDB.Execute

        With lrecFindDateVal
            .StoredProcedure = "FINDDATEVAL"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                FindDateVal = .Parameters("nExist").Value = 1
            End If
        End With


        'UPGRADE_NOTE: Object lrecFindDateVal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecFindDateVal = Nothing
    End Function
End Class






