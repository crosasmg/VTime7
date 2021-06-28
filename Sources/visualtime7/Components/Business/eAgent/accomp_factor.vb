Option Strict Off
Option Explicit On
Public Class accomp_factor
	'%-------------------------------------------------------%'
	'% $Workfile:: accomp_factor.cls                        $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 26/09/03 12:57                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'accomp_factor' en el sistema 14/12/2001 11:47:11 a.m.
	
	'+       Column name              Type
	'+  ------------------------- ------------
	Public nCurrency As Integer
	Public nAmo_Ini As Double
	Public nAmo_End As Double
	Public nFactor As Double
	Public nUsercode As Integer
	
	'% Actualiza la Tabla de factores de cumplimiento de metas
	Public Function insUpdAccomp_Factor(ByVal nAction As Integer) As Boolean
		Dim lclsaccomp_factor As eRemoteDB.Execute
		
		lclsaccomp_factor = New eRemoteDB.Execute
		
		On Error GoTo insUpdAccomp_Factor_Err
		
		'+ Define all parameters for the stored procedures 'insudb.insUpdAccomp_Factor'. Generated on 14/12/2001 11:47:11 a.m.
		
		With lclsaccomp_factor
			.StoredProcedure = "insUpdAccomp_Factor"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmo_Ini", nAmo_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmo_End", nAmo_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFactor", nFactor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdAccomp_Factor = .Run(False)
		End With
		
insUpdAccomp_Factor_Err: 
		If Err.Number Then
			insUpdAccomp_Factor = False
		End If
		'UPGRADE_NOTE: Object lclsaccomp_factor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsaccomp_factor = Nothing
		On Error GoTo 0
	End Function
	
	'IsExist: Función que verifica la existencia de un determinado registro en la tabla accomp_factor
	Public Function IsExist(ByVal nCurrency As Integer, ByVal nAmo_Ini As Double, ByVal nAmo_End As Double) As Boolean
		Dim lclsaccomp_factor As eRemoteDB.Execute
		Dim lblnExist As Boolean
		
		On Error GoTo IsExist_Err
		lclsaccomp_factor = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.Val_accomp_factor_Exist'.
		With lclsaccomp_factor
			.StoredProcedure = "Val_accomp_factor_Exist"
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmo_Ini", nAmo_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmo_End", nAmo_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nExist") = 1 Then
					IsExist = True
				End If
			End If
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsaccomp_factor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsaccomp_factor = Nothing
	End Function
	
	'insValMAG598_K: Función que realiza la validacion de los datos introducidos en la sección
	'                de detalles de la ventana
	Public Function insValMAG598_K(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal nAmo_Ini As Double, ByVal nAmo_End As Double, ByVal nFactor As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMAG598_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Moneda: Debe estar lleno
		If (nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10107)
		End If
		
		'+ Monto inicial del rango: Debe estar lleno
		If (nAmo_Ini = 0 Or nAmo_Ini = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10247)
		End If
		
		'+ Monto final del rango: Debe estar lleno
		If (nAmo_End = 0 Or nAmo_End = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10248)
		Else
			
			'+ Debe ser mayor al rango inicial
			If nAmo_End < nAmo_Ini Then
				Call lclsErrors.ErrorMessage(sCodispl, 10184)
			End If
		End If
		
		'+  Factor: Debe estar lleno
		If (nFactor = 0 Or nFactor = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1095)
		End If
		
		'+ Registro no debe estar repetido
		If sAction = "Add" Then
			If IsExist(nCurrency, nAmo_Ini, nAmo_End) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60214)
			End If
		End If
		
		insValMAG598_K = lclsErrors.Confirm
		
insValMAG598_K_Err: 
		If Err.Number Then
			insValMAG598_K = insValMAG598_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insPostMAG598_K: Función que realiza la validacion de los datos introducidos por la ventana
	Public Function insPostMAG598_K(ByVal bHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal nAmo_Ini As Double, ByVal nAmo_End As Double, ByVal nFactor As Double) As Boolean
		On Error GoTo insPostMAG598_K_Err
		
		With Me
			.nAmo_Ini = nAmo_Ini
			.nAmo_End = nAmo_End
			.nFactor = nFactor
			.nCurrency = nCurrency
			.nUsercode = nUsercode
			
			If bHeader Then
				insPostMAG598_K = True
			Else
				Select Case sAction
					
					'+ Acción: Agregar
					Case "Add"
						insPostMAG598_K = insUpdAccomp_Factor(1)
						
						'+ Acción: Actualizar
					Case "Update"
						insPostMAG598_K = insUpdAccomp_Factor(2)
						
						'+ Acción: Borrar
					Case "Del"
						insPostMAG598_K = insUpdAccomp_Factor(3)
						
				End Select
			End If
		End With
		
insPostMAG598_K_Err: 
		If Err.Number Then
			insPostMAG598_K = False
		End If
		On Error GoTo 0
	End Function
End Class






