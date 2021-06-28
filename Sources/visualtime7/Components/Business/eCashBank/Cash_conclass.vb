Option Strict Off
Option Explicit On
Public Class Cash_conclass
	'%-------------------------------------------------------%'
	'% $Workfile:: Cash_conclass.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:35p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Public nClass_concept As Integer
	Public nConcept As Integer
	Public nUsercode As Integer
	Public sStatregt As String
	Public sDescript As String
	
	'% Add
	Public Function Add() As Boolean
		Dim lclscash_conclass As eRemoteDB.Execute
		
		lclscash_conclass = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.crecash_conclass'.
		
		With lclscash_conclass
			.StoredProcedure = "crecash_conclass"
			.Parameters.Add("nClass_concept", nClass_concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclscash_conclass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscash_conclass = Nothing
	End Function
	
	'% Update
	Public Function Update() As Boolean
		Dim lclscash_conclass As eRemoteDB.Execute
		
		lclscash_conclass = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updcash_conclass'.
		With lclscash_conclass
			.StoredProcedure = "updcash_conclass"
			.Parameters.Add("nClass_concept", nClass_concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclscash_conclass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscash_conclass = Nothing
	End Function
	
	
	'%Delete: Eliminate a record of the cash_conclass table
	Public Function Delete() As Boolean
		Dim lclscash_conclass As eRemoteDB.Execute
		
		lclscash_conclass = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delcash_conclass'.
		With lclscash_conclass
			.StoredProcedure = "delcash_conclass"
			.Parameters.Add("nClass_concept", nClass_concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclscash_conclass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscash_conclass = Nothing
	End Function
	
	
	'valExist: Función que realiza la busqueda en la tabla 'insudb.cash_conclass'
	Public Function valExist() As Boolean
		Dim lclscash_conclass As eRemoteDB.Execute
		Dim lstrExist As String
		
		lclscash_conclass = New eRemoteDB.Execute
		lstrExist = "0"
		
		'+ Define all parameters for the stored procedures 'insudb.reacash_conclass_v'.
		With lclscash_conclass
			.StoredProcedure = "reacash_conclass_v"
			.Parameters.Add("nClass_concept", nClass_concept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("strExist", lstrExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("strExist").Value = "1" Then
					valExist = True
				Else
					valExist = False
				End If
			Else
				valExist = False
			End If
		End With
		'UPGRADE_NOTE: Object lclscash_conclass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscash_conclass = Nothing
	End Function
	
	
	'ValidateMOP702_k: Función que realiza la validacion de los datos introducidos en el
	'    encabezado de la ventana
	Public Function ValidateMOP702_k(ByVal sCodispl As String, ByVal nClass_concept As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo ValidateMOP702_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		If nClass_concept = 0 Or nClass_concept = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60121)
		End If
		
		ValidateMOP702_k = lclsErrors.Confirm
		
ValidateMOP702_k_Err: 
		If Err.Number Then
			ValidateMOP702_k = ValidateMOP702_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'ValidateMOP702: Función que realiza la validacion de los datos introducidor en la sección
	'    de detalles de la ventana
	Public Function ValidateMOP702(ByVal sCodispl As String, ByVal sAction As String, ByVal nClass_concept As Integer, ByVal nConcept As Integer, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo ValidateMOP702_Err
		
		lclsErrors = New eFunctions.Errors
		
		Me.nClass_concept = nClass_concept
		Me.nConcept = nConcept
		Me.sStatregt = sStatregt
		
		If sAction = "Add" Then
			If (nClass_concept > 0 Or nClass_concept <> eRemoteDB.Constants.intNull) And (nConcept > 0 Or nConcept <> eRemoteDB.Constants.intNull) Then
				If valExist() Then
					Call lclsErrors.ErrorMessage(sCodispl, 10004)
				End If
			End If
		End If
		
		If (nConcept = 0 Or nConcept = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 7005)
		End If
		
		If sStatregt = "0" Then
			Call lclsErrors.ErrorMessage(sCodispl, 9089)
		End If
		
		ValidateMOP702 = lclsErrors.Confirm
		
ValidateMOP702_Err: 
		If Err.Number Then
			ValidateMOP702 = ValidateMOP702 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'insPostMOP702: Función que realiza la validación de los datos introducidos en la ventana
	Public Function insPostMOP702(ByVal bHeader As Boolean, ByVal sAction As String, ByVal nClass_concept As Integer, ByVal nConcept As Integer, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		
		Me.nClass_concept = nClass_concept
		Me.nConcept = nConcept
		Me.sStatregt = sStatregt
		Me.nUsercode = nUsercode
		
		If bHeader Then
			insPostMOP702 = True
			
		Else
			If sAction = "Add" Then
				insPostMOP702 = Add()
				
			ElseIf sAction = "Update" Then 
				insPostMOP702 = Update()
				
			ElseIf sAction = "Del" Then 
				insPostMOP702 = Delete()
				
			End If
		End If
		
	End Function
End Class






