Option Strict Off
Option Explicit On
Public Class FPay_AllowClass
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'FPay_AllowClass' in the system 06/01/2005 08:26:55 a.m.
	'+Objetivo: Propiedades según la tabla 'FPay_AllowClass' en el sistema 06/01/2005 08:26:55 a.m.
	Public nBranch As Short
	Public dEffecdate As Date
	Public nProduct As Short
	Public nPayFreq As Short
	Public nSOATClass As Short
	
	'**-Objective: Date when the record is cancelled.
	'-Objetivo: Fecha de anulación del registro.
	Public dNulldate As Date
	
	'**- Variable to indicate when can be brought up to date the registrations of the grid
	'- Variable para indicar cuando se puede actualizar los registros del grid
	Public bEditRecord As Boolean
	Private mvarFPay_AllowClasss As FPay_AllowClasss

	Public Property FPay_AllowClasss() As FPay_AllowClasss
		Get
			If mvarFPay_AllowClasss Is Nothing Then
				mvarFPay_AllowClasss = New FPay_AllowClasss
			End If
			
            FPay_AllowClasss = mvarFPay_AllowClasss

        End Get

		Set(ByVal Value As FPay_AllowClasss)
			mvarFPay_AllowClasss = Value
        End Set

    End Property

	Private Sub Class_Terminate_Renamed()
		mvarFPay_AllowClasss = Nothing
    End Sub

	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
    End Sub
	
	'**%Objective: This method updates or adds a record into the table "tab_quotInt"
	'**%Parameters:
	'**%    sAction    - It indicates the type of action to be applied in the table ("Add", "Update" o "Del")
	'**%    nUsercode  - Code of the user creating or updating the record.
	'**%    nBranch    - Code of the line of business
	'**%    dEffecDate - Date which from the record is valid.
	'**%    nProduct   - Code of the product
	'**%    nPayFreq   - Code of the payment frequency
	'**%    nSoatClass - Classification soat of the client
	'%Objetivo: Este método permite agregar o actualizar un registro en la tabla "tab_quotInt"
	'%Parámetros:
	'%    sAction    - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar", "Actualizar" o "Eliminar").
	'%    nUsercode  - Código del usuario que crea o actualiza el registro.
	'%    nBranch    - Código del ramo comercial
	'%    dEffecDate - Fecha de efecto del registro.
	'%    nProduct   - Código del producto
	'%    nPayFreq   - Código de la frecuencia de pago
	'%    nSoatClass - Clasificación soat del cliente
	Private Function Add(ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal dEffecdate As Date, ByVal nProduct As Short, ByVal nPayFreq As Short, ByVal nSOATClass As Short) As Boolean
		Dim lclsFPay_AllowClass As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsFPay_AllowClass = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creFPay_AllowClass'. Generated on 06/01/2005 08:26:55 a.m.
		
		With lclsFPay_AllowClass
			.StoredProcedure = "insupdfpay_allowclass"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSOATClass", nSOATClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		lclsFPay_AllowClass = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Add = False
        End If
	End Function
	
	'**%Objective: Delete a registry the table "FPay_AllowClass" using the key for this table.
	'**%Parameters:
	'**%    sAction    - It indicates the type of action to be applied in the table ("Add", "Update" o "Del")
	'**%    nUsercode  - Code of the user creating or updating the record.
	'**%    nBranch    - Code of the line of business
	'**%    dEffecDate - Date which from the record is valid.
	'**%    nProduct   - Code of the product
	'**%    nPayFreq   - Code of the payment frequency
	'**%    nSoatClass - Classification soat of the client
	'%Objetivo: Elimina un registro a la tabla "FPay_AllowClass" usando la clave para dicha tabla.
	'%Parámetros:
	'%    sAction    - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar", "Actualizar" o "Eliminar").
	'%    nUsercode  - Código del usuario que crea o actualiza el registro.
	'%    nBranch    - Código del ramo comercial
	'%    dEffecDate - Fecha de efecto del registro.
	'%    nProduct   - Código del producto
	'%    nPayFreq   - Código de la frecuencia de pago
	'%    nSoatClass - Clasificación soat del cliente
	Private Function Delete(ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal dEffecdate As Date, ByVal nProduct As Short, ByVal nPayFreq As Short, ByVal nSOATClass As Short) As Boolean
		Dim lclsFPay_AllowClass As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsFPay_AllowClass = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delFPay_AllowClass'. Generated on 06/01/2005 08:26:55 a.m.
		With lclsFPay_AllowClass
			.StoredProcedure = "insupdFPay_AllowClass"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSOATClass", nSOATClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		lclsFPay_AllowClass = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Delete = False
        End If
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "FPay_AllowClass" using the key of this table.
	'**%Parameters:
	'**%    nBranch    - Code of the line of business
	'**%    dEffecDate - Date which from the record is valid.
	'**%    nProduct   - Code of the product
	'**%    nPayFreq   - Code of the payment frequency
	'**%    nSoatClass - Classification soat of the client
	'%Objetivo: Verifica la existencia de un registro en la tabla "FPay_AllowClass" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nBranch    - Código del ramo comercial
	'%    dEffecDate - Fecha de efecto del registro.
	'%    nProduct   - Código del producto
	'%    nPayFreq   - Código de la frecuencia de pago
	'%    nSoatClass - Clasificación soat del cliente
	Private Function IsExist(ByVal nBranch As Short, ByVal dEffecdate As Date, ByVal nProduct As Short, ByVal nPayFreq As Short, ByVal nSOATClass As Short) As Boolean
		Dim lclsFPay_AllowClass As eRemoteDB.Execute
		Dim lintExist As Short
		
        On Error GoTo ErrorHandler
		
		lclsFPay_AllowClass = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valFPay_AllowClassExist'. Generated on 06/01/2005 08:26:55 a.m.
		With lclsFPay_AllowClass
			.StoredProcedure = "reaFPay_AllowClass_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSOATClass", nSOATClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		lclsFPay_AllowClass = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            IsExist = False
        End If
	End Function
	
	
	
	'**%Objective: Validation of the data for the page of the headed one.
	'**%Parameters:
	'**%    sCodispl    - Logical code identificativo of the window.
	'**%    nMainAction - Action carried out in the transaction.
	'**%    nBranch    - Code of the line of business
	'**%    dEffecDate - Date which from the record is valid.
	'**%    nProduct   - Code of the product
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'%    sCodispl    - Código lógico identificativo de la ventana.
	'%    nMainAction - Acción realizada en la transacción.
	'%    nBranch    - Código del ramo comercial
	'%    dEffecDate - Fecha de efecto del registro.
	'%    nProduct   - Código del producto
	Public Function InsValMSO6003_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal dEffecdate As Date, ByVal nProduct As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
        On Error GoTo ErrorHandler
		
		lclsErrors = New eFunctions.Errors
		
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9064)
        End If

		If dEffecdate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4003)
        Else
            If nMainAction <> 401 And dEffecdate <= Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 10868)
            End If
        End If

        If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1014)
        End If

        InsValMSO6003_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsValMSO6003_k = InsValMSO6003_k & Err.Description
        End If
    End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl    - Logical code identificativo of the window.
	'**%    nMainAction - Action carried out in the transaction.
	'**%    nBranch     - Code of the line of business
	'**%    dEffecDate  - Date which from the record is valid.
	'**%    nProduct    - Code of the product
	'**%    nPayFreq   - Code of the payment frequency
	'**%    nSoatClass - Classification soat of the client
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl    - Código lógico identificativo de la ventana.
	'%    nMainAction - Acción realizada en la transacción.
	'%    nBranch     - Código del ramo comercial
	'%    dEffecDate  - Fecha de efecto del registro.
	'%    nProduct    - Código del producto
	'%    nPayFreq    - Código de la frecuencia de pago
	'%    nSoatClass  - Clasificación soat del cliente
	Public Function InsValMSO6003(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal dEffecdate As Date, ByVal nProduct As Short, ByVal nPayFreq As Short, ByVal nSOATClass As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
        On Error GoTo ErrorHandler
		
		lclsErrors = New eFunctions.Errors
		
        If sAction = "Add" AndAlso IsExist(nBranch, dEffecdate, nProduct, nPayFreq, nSOATClass) Then
            Call lclsErrors.ErrorMessage(sCodispl, 7148)
        End If
		
        If sAction = "Add" AndAlso (nSOATClass = 0 Or nSOATClass = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 90032)
        End If
		
		If nPayFreq = 0 Or nPayFreq = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 56165)
        End If
		
		InsValMSO6003 = lclsErrors.Confirm
		lclsErrors = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsValMSO6003 = InsValMSO6003 & Err.Description
        End If
	End Function
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    sCodispl    - Logical code identificativo of the window.
	'**%    nMainAction - Action carried out in the transaction.
	'**%    nUsercode  - Code of the user creating or updating the record.
	'**%    nBranch     - Code of the line of business
	'**%    dEffecDate  - Date which from the record is valid.
	'**%    nProduct    - Code of the product
	'**%    nPayFreq    - Code of the payment frequency
	'**%    nSoatClass  - Classification soat of the client
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    sCodispl    - Código lógico identificativo de la ventana.
	'%    nMainAction - Acción realizada en la transacción.
	'%    nUsercode  - Código del usuario que crea o actualiza el registro.
	'%    nBranch     - Código del ramo comercial
	'%    dEffecDate  - Fecha de efecto del registro.
	'%    nProduct    - Código del producto
	'%    nPayFreq    - Código de la frecuencia de pago
	'%    nSoatClass  - Clasificación soat del cliente
	Public Function InsPostMSO6003(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal dEffecdate As Date, ByVal nProduct As Short, ByVal nPayFreq As Short, ByVal nSOATClass As Short) As Boolean
        On Error GoTo ErrorHandler
		
		If nHeader Then
			InsPostMSO6003 = True
		Else
			If sAction = "Add" Then
				InsPostMSO6003 = Add(sAction, nUsercode, nBranch, dEffecdate, nProduct, nPayFreq, nSOATClass)
			ElseIf sAction = "Del" Then 
				InsPostMSO6003 = Delete(sAction, nUsercode, nBranch, dEffecdate, nProduct, nPayFreq, nSOATClass)
			End If
		End If
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsPostMSO6003 = False
        End If
	End Function
End Class






