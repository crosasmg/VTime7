Option Strict Off
Option Explicit On
Public Class cash_concepts
	'%-------------------------------------------------------%'
	'% $Workfile:: cash_concepts.cls                        $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 21/08/03 11:16a                              $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla cash_concepts al 03-04-2002 16:11:45
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nConcept As Integer ' NUMBER     22   0     5    N
	Public nCompany As Integer ' NUMBER     22   0     5    N
	Public sStatregt As String ' CHAR       1    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'% Update the links for a specific client
	Private Function insUpdcash_concepts(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdcash_concepts As eRemoteDB.Execute
		
		On Error GoTo insUpdcash_concepts_Err
		
		lrecinsUpdcash_concepts = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdcash_concepts al 03-04-2002 16:05:19
		'+
		With lrecinsUpdcash_concepts
			.StoredProcedure = "insUpdcash_concepts"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpdcash_concepts = .Run(False)
		End With
		
insUpdcash_concepts_Err: 
		If Err.Number Then
			insUpdcash_concepts = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdcash_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdcash_concepts = Nothing
		On Error GoTo 0
	End Function
	
	'% Add:
	Public Function Add() As Boolean
		Add = insUpdcash_concepts(1)
	End Function
	
	'%Update:
	Public Function Update() As Boolean
		Update = insUpdcash_concepts(2)
	End Function
	
	'%Delete:
	Public Function Delete() As Boolean
		Delete = insUpdcash_concepts(3)
	End Function
	
	'IsExist: Función que realiza la busqueda en la tabla 'insudb.cash_concepts'
	Public Function IsExist(ByVal nCompany As String, ByVal nConcept As Integer) As Boolean
		Dim lclscash_concepts As eRemoteDB.Execute
		Dim lstrExist As String
		
		lclscash_concepts = New eRemoteDB.Execute
		lstrExist = "0"
		
		'+ Define all parameters for the stored procedures 'insudb.valcash_conceptsExist'. Generated on 20/12/2001 10:43:14 a.m.
		With lclscash_concepts
			.StoredProcedure = "reacash_concepts_v"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("strExist", lstrExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("strExist").Value = "1" Then
					IsExist = True
				Else
					IsExist = False
				End If
			Else
				IsExist = False
			End If
		End With
		'UPGRADE_NOTE: Object lclscash_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscash_concepts = Nothing
	End Function
	
	'insValMOP699_k: Función que realiza la validacion de los datos introducidos en el
	'    encabezado de la ventana
	Public Function insValMOP699_k(ByVal nUsercode As Integer, ByVal nCompany As String, ByVal sCodispl As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMOP699_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		If nCompany = String.Empty Or nCompany = "0" Then
			Call lclsErrors.ErrorMessage(sCodispl, 1046)
		End If
		
		insValMOP699_k = lclsErrors.Confirm
		
insValMOP699_k_Err: 
		If Err.Number Then
			insValMOP699_k = insValMOP699_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insValMOP699: Función que realiza la validacion de los datos introducidor en la sección
	'    de detalles de la ventana
	Public Function insValMOP699(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nCompany As String, ByVal nConcept As Integer, ByVal sDescript As String, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMOP699_Err
		
		lclsErrors = New eFunctions.Errors
		
		If sAction = "Add" Then
			If nConcept > 0 Or nConcept <> eRemoteDB.Constants.intNull Then
				If IsExist(nCompany, nConcept) Then
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
		
		insValMOP699 = lclsErrors.Confirm
		
insValMOP699_Err: 
		If Err.Number Then
			insValMOP699 = insValMOP699 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insPostMOP699: Función que realiza la validacion de los datos introducidos por la ventana
	Public Function insPostMOP699(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCompany As String, ByVal nConcept As Integer, ByVal sStatregt As String) As Boolean
		
		With Me
			.nCompany = CInt(nCompany)
			.nConcept = nConcept
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			
			
			If pblnHeader Then
				insPostMOP699 = True
			Else
				Select Case sAction
					
					'+ Acción: Agregar
					Case "Add"
						
						insPostMOP699 = Add()
						
						'+ Acción: Actualizar
					Case "Update"
						insPostMOP699 = Update()
						
						'+ Acción: Borrar
					Case "Del"
						
						insPostMOP699 = Delete()
				End Select
			End If
		End With
		
	End Function
End Class






