Option Strict Off
Option Explicit On
Public Class Age_Product
	'**+Objective: Class that supports the Machine Antiquity Maintenance
	'**+Version: $$Revision: 1 $
	'+Objetivo: Clase que le da soporte al Mantenimiento de Antigüedad de Maquinaria
	'+Version: $$Revision: 1 $
	
	'**+Objective: Properties according to the table 'Age_Product' in the system 10/06/2005 12:42pm
	'+Objetivo: Propiedades según la tabla 'Age_Product' en el sistema 10/06/2005 12:42pm
	Public nBranch As Integer
	Public nProduct As Integer
	Public nMachineCode As Integer
	Public nAge As Short
	Public dCompdate As Date
	Public nUsercode As Short
	
	'**%Objective: Updates a registry to the table "Age_Product" using the key for this table.
	'**%Parameters:
	'               sCodispl     - Logical code of the window (MRM9000)
	'               nAction      - Indicates the Action to apply to the register (1: Add, 2: Update, 3: Delete)
	'               nBranch      - Branch code
	'               nProduct     - Product code
	'               nMachineCode - Machine Code
	'               nAge         - Antiquity in years asssigned to the machine
	'               nUsercode    - Code of the user that registers information
	'
	'%Objetivo: Actualiza un registro a la tabla "Age_Product" usando la clave para dicha tabla.
	'%Parámetros:
	'               sCodispl     - Código lógico de la ventana (MRM9000)
	'               nAction      - Indica la accion a aplicar al registro(1: Add, 2: Update, 3: Delete)
	'               nBranch      - Código del Ramo
	'               nProduct     - Código del Producto
	'               nMachineCode - Código de Maquinaria
	'               nAge         - Antigüedad en años asignada a la maquinaria
	'               nUsercode    - Código del usuario que registra la información
	Private Function Update(ByVal sCodispl As String, ByVal nAction As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMachineCode As Integer, ByVal nAge As Short, ByVal nUsercode As Integer) As Boolean
		Dim lclsAge_Product As eRemoteDB.Execute
		

		lclsAge_Product = New eRemoteDB.Execute
		'+ Define all parameters for the stored procedures 'insudb.updAge_Product'. Generated on 10/06/2005 12:42pm
		With lclsAge_Product
			.StoredProcedure = "InsUpdAge_Product"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMachineCode", nMachineCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lclsAge_Product = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "Age_Product" using the key of this table.
	'**%Parameters:
	'               nBranch      - Branch code
	'               nProduct     - Product code
	'               nMachineCode - Machine code
	'
	'%Objetivo: Verifica la existencia de un registro en la tabla "Age_Product" usando la clave de dicha tabla.
	'%Parámetros:
	'               nBranch      - Código del Ramo
	'               nProduct     - Código del Producto
	'               nMachineCode - Código de Maquinaria
	Private Function IsExist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMachineCode As Integer) As Boolean
		''------------------------------------------------------------------------------------------------------------------------
		Dim lclsAge_Product As eRemoteDB.Execute
		

		lclsAge_Product = New eRemoteDB.Execute
		'+ Define all parameters for the stored procedures 'insudb.valAge_ProductExist'. Generated on 10/06/2005 12:42pm
		With lclsAge_Product
			.StoredProcedure = "reaAge_Product_v1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMachineCode", nMachineCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		lclsAge_Product = Nothing
		
		Exit Function
	End Function
	'**%Objective: Validation of the data for the page of the headed one.
	'**%Parameters:
	'               sCodispl     - Logical code of the window
	'               nMainAction  - Indicates the main action of the window (Add = 301, Update = 302, Query = 401)
	'               sAction      - Indicates the action to apply to the register ("Add","Del","Update")
	'               nBranch      - Branch code
	'               nProduct     - Product code
	'               nMachineCode - Machine code
	'
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'               sCodispl     - Código lógico de la ventana
	'               nMainAction  - Indica la acción principal de la ventana (Registrar = 301, Actualizar = 302, Consultar = 401)
	'               sAction      - Indica la acción a realizar sobre el registro ("Add","Del","Update")
	'               nBranch      - Código del Ramo
	'               nProduct     - Código del Producto
	'               nMachineCode - Código de Maquinaria
	Public Function InsValMRM9000_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMachineCode As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		

		lclsErrors = New eFunctions.Errors
		
		InsValMRM9000_k = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'               sCodispl     - Logical code of the window
	'               nMainAction  - Indicates the main action of the window (Add = 301, Update = 302, Query = 401)
	'               sAction      - Indicates the action to apply to the register ("Add","Del","Update")
	'               nBranch      - Branch code
	'               nProduct     - Product code
	'               nMachineCode - Machine code
	'               nAge         - Antiquity in years asssigned to the machine
	'
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'               sCodispl     - Código lógico de la ventana
	'               nMainAction  - Indica la acción principal de la ventana (Registrar = 301, Actualizar = 302, Consultar = 401)
	'               sAction      - Indica la acción a realizar sobre el registro ("Add","Del","Update")
	'               nBranch      - Código del Ramo
	'               nProduct     - Código del Producto
	'               nMachineCode - Código de Maquinaria
	'               nAge         - Antigüedad en años asignada a la maquinaria
	Public Function InsValMRM9000(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMachineCode As Integer, ByVal nAge As Short) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lbValid As Boolean
		

		lclsErrors = New eFunctions.Errors
		
		lbValid = True
		'+ Valida que se haya ingresado un ramo
		If nBranch = numNull Or nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94088)
			lbValid = False
		End If
		
		'Cuando la maquinaria no se ingresa se está ingresando el código de maquinaria: 998 - Todas las maquinarias
		'+ Valida que se haya ingresado un código de maquinaria
		'    If nMachineCode = numNull Then
		'       Call lclsErrors.ErrorMessage(sCodispl, 94101)
		'       lbValid = False
		'    End If
		
		'+ Valida que se haya ingresado la antigüedad
		If nAge = numNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 94089)
		Else
			'+ Valida que la antigüedad ingresada sea mayor que cero
			If nAge = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 94090)
			End If
		End If
		
		If lbValid Then
			If sAction = "Add" Then
				'+ Se valida que el registro a insertar no se haya registrado en la tabla Age_Product
				If IsExist(nBranch, nProduct, nMachineCode) Then
					Call lclsErrors.ErrorMessage(sCodispl, 94100)
				End If
			End If
		End If
		
		InsValMRM9000 = lclsErrors.Confirm
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Passes of the information through the layers of business rules and data access
	'**%Parameters:
	'               sCodispl     - Logical code of the window
	'               nMainAction  - Indicates the main action of the window (Add = 301, Update = 302, Query = 401)
	'               sAction      - Indicates the action to apply to the register ("Add","Del","Update")
	'               nBranch      - Branch code
	'               nProduct     - Product code
	'               nMachineCode - Machine code
	'               nAge         - Antiquity in years asssigned to the machine
	'               nUsercode    - Code of the user that registers the information
	'
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'               sCodispl     - Código lógico de la ventana
	'               nMainAction  - Indica la acción principal de la ventana (Registrar = 301, Actualizar = 302, Consultar = 401)
	'               sAction      - Indica la acción a realizar sobre el registro ("Add","Del","Update")
	'               nBranch      - Código del Ramo
	'               nProduct     - Código del Producto
	'               nMachineCode - Código de Maquinaria
	'               nAge         - Antigüedad en años asignada a la maquinaria
	'               nUsercode    - Código del usuario que registra la información
	Public Function InsPostMRM9000(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMachineCode As Integer, ByVal nAge As Short, ByVal nUsercode As Integer) As Boolean
		Dim nAction As Short
		
		Select Case sAction
			Case "Add"
				nAction = 1
			Case "Update"
				nAction = 2
			Case "Del"
				nAction = 3
		End Select
		InsPostMRM9000 = Update(sCodispl, nAction, nBranch, nProduct, nMachineCode, nAge, nUsercode)
		
		Exit Function
	End Function
End Class











