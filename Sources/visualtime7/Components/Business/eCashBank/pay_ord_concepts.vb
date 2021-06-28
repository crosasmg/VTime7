Option Strict Off
Option Explicit On
Public Class pay_ord_concepts
	'%-------------------------------------------------------%'
	'% $Workfile:: pay_ord_concepts.cls                     $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 21/08/03 11:16a                              $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla pay_ord_concepts al 03-04-2002 16:11:14
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nCompany As Integer ' NUMBER     22   0     5    N
	Public nConcept As Integer ' NUMBER     22   0     5    N
	Public sStatregt As String ' CHAR       1    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'% Update the links for a specific client
	Private Function insUpdpay_ord_concepts(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdpay_ord_concepts As eRemoteDB.Execute
		
		On Error GoTo insUpdpay_ord_concepts_Err
		
		lrecinsUpdpay_ord_concepts = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdpay_ord_concepts al 03-04-2002 13:56:17
		'+
		With lrecinsUpdpay_ord_concepts
			.StoredProcedure = "insUpdpay_ord_concepts"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdpay_ord_concepts = .Run(False)
		End With
		
insUpdpay_ord_concepts_Err: 
		If Err.Number Then
			insUpdpay_ord_concepts = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdpay_ord_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdpay_ord_concepts = Nothing
		On Error GoTo 0
	End Function
	
	'% Add all values related to a specific record
	Public Function Add() As Boolean
		Add = insUpdpay_ord_concepts(1)
	End Function
	
	'% Update the links for a specific client
	Public Function Update() As Boolean
		Update = insUpdpay_ord_concepts(2)
	End Function
	
	'%Delete: Eliminate the corresponding information for a client, year and specific concept
	Public Function Delete() As Boolean
		Delete = insUpdpay_ord_concepts(3)
	End Function
	
	'**% Find: Searches for the information in the general commissions table
	'% Find: Busca la información de una tabla de comisiones de generales.
	Public Function Find(ByVal nCompany As Integer, ByVal nConcept As Integer) As Boolean
		Dim lclspay_ord_concepts As eRemoteDB.Execute
		
		lclspay_ord_concepts = New eRemoteDB.Execute
		With lclspay_ord_concepts
			.StoredProcedure = "reapay_ord_concepts"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nCompany = .FieldToClass("nCompany")
				Me.nConcept = .FieldToClass("nConcept")
				Me.sStatregt = .FieldToClass("sStatregt")
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclspay_ord_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclspay_ord_concepts = Nothing
	End Function
	
	'insValMOP711_k: Función que realiza la validacion de los datos introducidos en el
	'    encabezado de la ventana
	Public Function insValMOP711_k(ByVal sCodispl As String, ByVal nCompany As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMOP711_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		If (nCompany = 0 Or nCompany = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1046)
		End If
		
		insValMOP711_k = lclsErrors.Confirm
		
insValMOP711_k_Err: 
		If Err.Number Then
			insValMOP711_k = insValMOP711_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insValMOP711: Función que realiza la validacion de los datos introducidor en la sección
	'    de detalles de la ventana
	Public Function insValMOP711(ByVal sCodispl As String, ByVal sAction As String, ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMOP711_Err
		
		lclsErrors = New eFunctions.Errors
		
		If sAction = "Add" Then
			If Find(nCompany, nConcept) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10004)
			End If
		End If
		
		If (nConcept = 0 Or nConcept = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 4062)
		End If
		
		If (CDbl(sStatregt) = 0 Or sStatregt = String.Empty) Then
			Call lclsErrors.ErrorMessage(sCodispl, 9089)
		End If
		
		insValMOP711 = lclsErrors.Confirm
		
insValMOP711_Err: 
		If Err.Number Then
			insValMOP711 = insValMOP711 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insPostMOP711: Función que realiza la validacion de los datos introducidos por la ventana
	Public Function insPostMOP711(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal sStatregt As String) As Boolean
		
		With Me
			.nCompany = nCompany
			.nConcept = nConcept
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			
			
			If pblnHeader Then
				insPostMOP711 = True
			Else
				Select Case sAction
					
					'+ Acción: Agregar
					Case "Add"
						
						insPostMOP711 = Add()
						
						'+ Acción: Actualizar
					Case "Update"
						insPostMOP711 = Update()
						
						'+ Acción: Borrar
					Case "Del"
						
						insPostMOP711 = Delete()
				End Select
			End If
		End With
		
	End Function
End Class






