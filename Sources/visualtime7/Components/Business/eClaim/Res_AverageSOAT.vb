Option Strict Off
Option Explicit On
Public Class Res_AverageSOAT
	'**+Objective: Class that supports the table 'Res_AverageSOAT'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Clase que le da soporte a la tabla 'Res_AverageSOAT'.
	'+Version: $$Revision: 4 $
	
	'**-Objective: Client category - SOAT Claim
	'-Objetivo: Categorias de clientes - Siniestros SOAT
	Public nCli_category As Integer
	
	'**- Objective: Code of the currency
	'- Objetivo: Código de la moneda
	Public nCurrency As Integer
	
	'**- Objective: Code of the illness
	'- Objetivo: Código de la enfermedad
	Public sIllness As String
	
	'**- Objective: Initial reserve average clinics
	'- Objetivo: Reserva inicial promedio en clínicas
	Public nResaveclin As Double
	
	'**- Objective: Initial reserve average hospitals
	'- Objetivo: Reserva inicial promedio en hospitales
	Public nResavehosp As Double
	
	'**- Objective: Initial reserve average without intitution
	'- Objetivo: Reserva inicial promedio sin tipo de proveedor
	Public nRes_average As Double
	
	'**- Objective: Initial reserve average temporary disablement
	'- Objetivo: Reserva inicial promedio incapacidad temporal
	Public nResavetdis As Double
	
	'**- Objective: Days averages of diablement
	'- Objetivo: Días promedios de incapacidad
	Public nDaysavedis As Integer
	
	'**- Objective: General status of the record.
	'- Objetivo: Estado general del registro.
	Public sStatregt As String
	
	'**%Objective: This method updates or adds a record into the table "<__TABLE__>"
	'**%Parameters:
	'**%    sAction       - The type of action to be executed for the record ("Add" or "Update")
	'**%    nUsercode     - Code of the user that creates or updates the record
	'**%    nCli_Category - Client category - SOAT Claim
	'**%    nCurrency     - Code of the currency
	'**%    sIllness      - Code of the illness
	'**%    nResaveClin   - Initial reserve average clinics
	'**%    nResaveHosp   - Initial reserve average hospitals
	'**%    nRes_Average  - Initial reserve average without intitution
	'**%    nResavetdis   - Initial reserve average temporary disablement
	'**%    nDaysavedis   - Days averages of diablement
	'**%    sStatregt     - General status of the record
	'%Objetivo: Este método permite agregar o actualizar un registro en la tabla "<__TABLE__>"
	'%Parámetros:
	'%    sAction       - Indica el tipo de acción a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
	'%    nUsercode     - Código del usuario que crea o actualiza el registro
	'%    nCli_Category - Categorias de clientes - Siniestros SOAT
	'%    nCurrency     - Código de la moneda
	'%    sIllness      - Código de la enfermedad
	'%    nResaveClin   - Reserva inicial promedio en clínicas
	'%    nResaveHosp   - Reserva inicial promedio en hospitales
	'%    nRes_Average  - Reserva inicial promedio sin tipo de proveedor
	'%    nResavetdis   - Reserva inicial promedio incapacidad temporal
	'%    nDaysavedis   - Días promedios de incapacidad
	'%    sStatregt     - Estado general del registro
	Private Function AddUpdate(ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCli_category As Integer, ByVal nCurrency As Integer, ByVal sIllness As String, ByVal nResaveclin As Double, ByVal nResavehosp As Double, ByVal nRes_average As Double, ByVal nResavetdis As Double, ByVal nDaysavedis As Integer, ByVal sStatregt As String) As Boolean
		Dim lclsRes_AverageSOAT As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsRes_AverageSOAT = New eRemoteDB.Execute
		
		With lclsRes_AverageSOAT
			.StoredProcedure = "creupdRes_AverageSOAT"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCli_category", nCli_category, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResaveclin", nResaveclin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResavehosp", nResavehosp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRes_average", nRes_average, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResavetdis", nResavetdis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysavedis", nDaysavedis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddUpdate = .Run(False)
		End With
		lclsRes_AverageSOAT = Nothing
		Exit Function
ErrorHandler: 
        If Err.Number Then
            AddUpdate = False
        End If
	End Function
	
	'**%Objective: Verifies the existence of a record in table "Res_AverageSOAT" using the key.
	'**%Parameters:
	'**%    nCli_category - Client category - SOAT Claim
	'**%    nCurrency     - Code of the currency
	'%Objetivo: Esta función verifica la existencia de un registro en la tabla "Res_AverageSOAT" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nCli_category - Categorias de clientes - Siniestros SOAT
	'%    nCurrency     - Código de la moneda
	'%    sIllness      - Código de la enfermedad
	Private Function IsExist(ByVal nCli_category As Integer, ByVal nCurrency As Integer, ByVal sIllness As String) As Boolean
		Dim lclsRes_AverageSOAT As eRemoteDB.Execute
		Dim lintExist As Short
		
        On Error GoTo ErrorHandler
		
		lclsRes_AverageSOAT = New eRemoteDB.Execute
		lintExist = 0
		
		With lclsRes_AverageSOAT
			.StoredProcedure = "reares_averagesoat_v"
			.Parameters.Add("nCli_category", nCli_category, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclsRes_AverageSOAT = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            IsExist = False
        End If
	End Function
	
	
	'**%Objective: Verifies the existence of a record in table "Res_AverageSOAT" using the key.
	'**%Parameters:
	'**%    sIllness - Code of the illness
	'%Objetivo: Esta función verifica la existencia de un registro en la tabla "Res_AverageSOAT" usando la clave de dicha tabla.
	'%Parámetros:
	'%    sIllness - Código de la enfermedad
	Private Function IsExistNivel(ByVal sIllness As String) As Boolean
		Dim lclsRes_AverageSOAT As eRemoteDB.Execute
		Dim lintExist As Short
		
        On Error GoTo ErrorHandler
		
		lclsRes_AverageSOAT = New eRemoteDB.Execute
		lintExist = 0
		
		With lclsRes_AverageSOAT
			.StoredProcedure = "reatab_am_ill_v"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExistNivel = (.Parameters("nExist").Value = 1)
			Else
				IsExistNivel = False
			End If
		End With
		
		lclsRes_AverageSOAT = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            IsExistNivel = False
        End If
	End Function
	
	
	'**%Objective: Validates the data from the header section of the page being processed.
	'**%Parameters:
	'**%    sCodispl      - Code of the window (logical code).
	'**%    nCli_Category - Client category - SOAT Claim
	'**%    nCurrency     - Code of the currency
	'%Objetivo: Esta función valida los datos del encabezado de la página en tratamiento.
	'%Parámetros:
	'%    sCodispl      - Código de la ventana (lógico).
	'%    nCli_Category - Categorias de clientes - Siniestros SOAT
	'%    nCurrency     - Código de la moneda
	Public Function InsValMSI6002_K(ByVal sCodispl As String, ByVal nCli_category As Integer, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
        On Error GoTo ErrorHandler
		
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			
			'+ El campo Categoría debe estar lleno
			'+ The field nCli_category should be full.
			If nCli_category = eRemoteDB.Constants.intNull Or nCli_category = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 91005)
			End If
			
			'+ El campo Moneda debe estar lleno
			'+ The field Currency should be full.
			If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 750024)
			End If
			
			InsValMSI6002_K = .Confirm
		End With
		lclsErrors = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsValMSI6002_K = InsValMSI6002_K & Err.Description
        End If
	End Function
	
	'**%Objective: Validates the data from the detail section of the page being processed.
	'**%Parameters:
	'**%    sCodispl      - Code of the window (logical code)
	'**%    sAction       - Actions of the transaction
	'**%    nCli_Category - Client category - SOAT Claim
	'**%    nCurrency     - Code of the currency
	'**%    sIllness      - Code of the illness
	'**%    nResaveClin   - Initial reserve average clinics
	'**%    nResaveHosp   - Initial reserve average hospitals
	'**%    nRes_Average  - Initial reserve average without intitution
	'**%    nResavetdis   - Initial reserve average temporary disablement
	'**%    nDaysavedis   - Days averages of diablement
	'%Objetivo: Esta función permite validar los datos del detalle de la página en tratamiento.
	'%Parámetros:
	'%    sCodispl      - Código de la ventana (lógico)
	'%    sAction       - Acción de la transacción
	'%    nCli_Category - Categorias de clientes - Siniestros SOAT
	'%    nCurrency     - Código de la moneda
	'%    sIllness      - Código de la enfermedad
	'%    nResaveClin   - Reserva inicial promedio en clínicas
	'%    nResaveHosp   - Reserva inicial promedio en hospitales
	'%    nRes_Average  - Reserva inicial promedio sin tipo de proveedor
	'%    nResavetdis   - Reserva inicial promedio incapacidad temporal
	'%    nDaysavedis   - Días promedios de incapacidad
	Public Function InsValMSI6002(ByVal sCodispl As String, ByVal sAction As String, ByVal nCli_category As Integer, ByVal nCurrency As Integer, ByVal sIllness As String, ByVal nResaveclin As Double, ByVal nResavehosp As Double, ByVal nRes_average As Double, ByVal nResavetdis As Double, ByVal nDaysavedis As Integer) As String
		Dim lclsErrors As eFunctions.Errors

        On Error GoTo ErrorHandler
		
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			
			If sAction = "Add" Then
				If sIllness = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 90163)
				End If
			End If
			
			If sIllness <> String.Empty Then
				
				If sAction = "Add" Then
					If IsExist(nCli_category, nCurrency, sIllness) Then
						Call lclsErrors.ErrorMessage(sCodispl, 90255)
					End If
					If IsExistNivel(sIllness) Then
						Call lclsErrors.ErrorMessage(sCodispl, 90256)
					End If
				End If
				
				If nResaveclin = eRemoteDB.Constants.intNull Or nResaveclin = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 90207)
				End If
				
				If nResavehosp = eRemoteDB.Constants.intNull Or nResavehosp = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 90208)
				End If
				
				If nRes_average = eRemoteDB.Constants.intNull Or nRes_average = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 90209)
				End If
				
				If nResavetdis = eRemoteDB.Constants.intNull Or nResavetdis = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 90210)
				End If
				
				If nDaysavedis = eRemoteDB.Constants.intNull Or nDaysavedis = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 90211)
				End If
			End If
			
			InsValMSI6002 = .Confirm
		End With
		lclsErrors = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsValMSI6002 = InsValMSI6002 & Err.Description
        End If
	End Function
	
	
	'**%Objective: Sends the information necessary to update the records in the database.
	'**%Parameters:
	'**%    sAction       - The type of action to be executed for the record ("Add" or "Update")
	'**%    nUsercode     - Code of the user that creates or updates the record
	'**%    nCli_Category - Client category - SOAT Claim
	'**%    nCurrency     - Code of the currency
	'**%    sIllness      - Code of the illness
	'**%    nResaveClin   - Initial reserve average clinics
	'**%    nResaveHosp   - Initial reserve average hospitals
	'**%    nRes_Average  - Initial reserve average without intitution
	'**%    nResavetdis   - Initial reserve average temporary disablement
	'**%    nDaysavedis   - Days averages of diablement
	'**%    sStatregt     - General status of the record
	'%Objetivo: Esta función permite enviar la información necesaria de los registros en tratamiento a la base de datos para su
	'% posterior actualización.
	'%Parámetros:
	'%    sAction       - Indica el tipo de acción a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
	'%    nUsercode     - Código del usuario que crea o actualiza el registro
	'%    nCli_Category - Categorias de clientes - Siniestros SOAT
	'%    nCurrency     - Código de la moneda
	'%    sIllness      - Código de la enfermedad
	'%    nResaveClin   - Reserva inicial promedio en clínicas
	'%    nResaveHosp   - Reserva inicial promedio en hospitales
	'%    nRes_Average  - Reserva inicial promedio sin tipo de proveedor
	'%    nResavetdis   - Reserva inicial promedio incapacidad temporal
	'%    nDaysavedis   - Días promedios de incapacidad
	'%    sStatregt     - Estado general del registro
	Public Function InsPostMSI6002(ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCli_category As Integer, ByVal nCurrency As Integer, ByVal sIllness As String, Optional ByVal nResaveclin As Double = 0, Optional ByVal nResavehosp As Double = 0, Optional ByVal nRes_average As Double = 0, Optional ByVal nResavetdis As Double = 0, Optional ByVal nDaysavedis As Integer = 0, Optional ByVal sStatregt As String = "") As Boolean
        On Error GoTo ErrorHandler
		
		Select Case sAction
			Case "Add", "Update", "Del"
				InsPostMSI6002 = AddUpdate(sAction, nUsercode, nCli_category, nCurrency, sIllness, nResaveclin, nResavehosp, nRes_average, nResavetdis, nDaysavedis, sStatregt)
		End Select
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsPostMSI6002 = False
        End If
	End Function
End Class






