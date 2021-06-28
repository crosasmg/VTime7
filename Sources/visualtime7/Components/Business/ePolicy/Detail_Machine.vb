Option Strict Off
Option Explicit On
Public Class Detail_Machine
	'**+Objective: Class that supports the table Detail_Machine it's content is:
	'**+Version: $$Revision: 1 $
	'+Objetivo: Clase que le da soporte a la tabla Detail_Machine cuyo contenido es:
	'+Version: $$Revision: 1 $
	
	'**+Objective: Properties according to the table 'Detail_Machine' in the system 06/06/2005
	'+Objetivo: Propiedades según la tabla 'Detail_Machine' en el sistema 06/06/2005
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public dEffecdate As Date
	Public dCompdate As Date
	Public dNulldate As Date
	Public nUsercode As Integer
	Public nMachineCode As Short
	Public sDescript As String
	Public nFabYear As Short
	Public nQuantityMachine As Short
	'**%Objective: Updates a registry to the table "Detail_Machine" using the key for this table.
	'%Objetivo: Actualiza un registro a la tabla "Detail_Machine" usando la clave para dicha tabla.
	Private Function Update(ByVal sCodispl As String, ByVal nAction As Short, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nMachineCode As Short, ByVal nFabYear As Short, ByVal nQuantityMachine As Short, ByVal nUsercode As Integer) As Boolean
		Dim lclsDetail_Machine As eRemoteDB.Execute
		

        lclsDetail_Machine = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'InsUpdDetail_Machine'. Generated on 06/06/2005
		With lclsDetail_Machine
            .StoredProcedure = "InsUpdDetail_Machine"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMachineCode", nMachineCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFabYear", nFabYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantityMachine", nQuantityMachine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lclsDetail_Machine = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "Detail_Machine" using the key of this table.
	'%Objetivo: Verifica la existencia de un registro en la tabla "Detail_Machine" usando la clave de dicha tabla.
	Private Function IsExist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nMachineCode As Short, ByVal nFabYear As Short, ByVal dEffecdate As Date) As Boolean
		Dim lclsDetail_Machine As eRemoteDB.Execute
		Dim lintExist As Short
		

        lclsDetail_Machine = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedure 'reaDetail_Machine_v'. Generated on 06/06/2005
		With lclsDetail_Machine
			.StoredProcedure = "reaDetail_Machine_v"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMachineCode", nMachineCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFabYear", nFabYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		lclsDetail_Machine = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page details.
	'%Objetivo: Validación de los datos para la página detalle.
	Public Function InsValDetail_Machine(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nMachineCode As Short, ByVal nFabYear As Short, ByVal nQuantityMachine As Short) As String
		Dim lclsErrors As eFunctions.Errors
		

        lclsErrors = New eFunctions.Errors
		
		'+ Valida que esté lleno el Código de Maquinaria
		If nMachineCode = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 94045)
		End If
		
		'+ Si se ingresó el Año de Fabricación valida la antiguedad de la máquina
		If nFabYear > 0 Then
			If nFabYear < 1900 Or nFabYear > Year(Today) Then 'Valida que el Año de Fabricación no sea menor a 1900
				Call lclsErrors.ErrorMessage(sCodispl, 94092)
			Else
				If Not AgeValidation(nBranch, nProduct, nMachineCode, nFabYear) Then
					Call lclsErrors.ErrorMessage(sCodispl, 94047)
				End If
			End If
		End If
		
		If nMachineCode <> eRemoteDB.Constants.intNull Then
			If sAction = "Add" Then
				'+ Se valida que el registro a insertar no se haya registrado en la tabla Detail_Machine
				If IsExist(sCertype, nBranch, nProduct, nPolicy, nCertif, nMachineCode, nFabYear, dEffecdate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 94085)
				End If
			End If
		End If
		
		'+ Valida que este lleno el campo Cantidad
		If nQuantityMachine = eRemoteDB.Constants.intNull Or nQuantityMachine = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94046)
		End If
		
		InsValDetail_Machine = lclsErrors.Confirm
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	Public Function InsPostDetail_Machine(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nMachineCode As Short, ByVal nFabYear As Short, ByVal nQuantityMachine As Short, ByVal nUsercode As Integer) As Boolean
		Dim nAction As Short
		

        Select Case sAction
            Case "Add"
                nAction = 1
            Case "Update"
                nAction = 2
            Case "Del"
                nAction = 3
        End Select
		InsPostDetail_Machine = Update(sCodispl, nAction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nMachineCode, nFabYear, nQuantityMachine, nUsercode)
		
		Exit Function
	End Function
	
	'**%Objective: Validates the machine antiquity for the specified branch and product
	'%Objetivo: Valida la antiguedad de maquinaria según el ramo y producto especificados
	Private Function AgeValidation(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMachineCode As Short, ByVal nFabYear As Short) As Boolean
		Dim lclsDetail_Machine As eRemoteDB.Execute
		Dim lnAgeMachine As Short
		

		lnAgeMachine = Year(Today) - nFabYear
		AgeValidation = True
		If lnAgeMachine >= 0 Then
			lclsDetail_Machine = New eRemoteDB.Execute
			'+ Definición de parámetros para stored procedure 'ReaAge_Product_v'. Generated on 03/06/2005
			With lclsDetail_Machine
				.StoredProcedure = "ReaAge_Product_v"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMachineCode", nMachineCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAge", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Run(False)
				If .Parameters("nAge").Value > -1 Then
					AgeValidation = (lnAgeMachine <= .Parameters("nAge").Value)
				End If
			End With
			lclsDetail_Machine = Nothing
		End If
		
		Exit Function
	End Function
End Class











